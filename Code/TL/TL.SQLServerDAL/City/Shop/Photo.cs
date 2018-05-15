using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using TL.Model.City.Shop;

namespace TL.SQLServerDAL.City.Shop
{
    /// <summary>
    /// ������Ƭ
    /// </summary>
    public class Photo : CityDB
    {
        /// <summary>
        /// ��ӵ�����Ƭ
        /// </summary>
        /// <param name="ShopUser">�û�����</param>
        /// <returns>������Ӱ�������</returns>
        public int Add(PhotoInfo ShopPhoto)
        {
            SqlParameter[] MyPar = new SqlParameter[4];
            
            MyPar[0] = new SqlParameter("@Url", SqlDbType.NVarChar, 50);
            MyPar[0].Value = ShopPhoto.Url;
            MyPar[1] = new SqlParameter("@Ext", SqlDbType.NVarChar, 5);
            MyPar[1].Value = ShopPhoto.Ext;
            MyPar[2] = new SqlParameter("@Description", SqlDbType.NVarChar, 50);
            MyPar[2].Value = ShopPhoto.Description;
            MyPar[3] = new SqlParameter("@UserId", SqlDbType.BigInt, 8);
            MyPar[3].Value = ShopPhoto.UserId;

            string sql = "insert into [" + Pre + "_Shop_Photo]([Url],[Ext],[Description],[OrderNum],[UserId]) select @Url,@Ext,@Description,isnull(max([OrderNum]),0)+1,@UserId from [" + Pre + "_Shop_Photo] where UserId=@UserId";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, MyPar);
        }

        /// <summary>
        /// ɾ��������Ƭ ��ɾ����������
        /// </summary>
        /// <param name="Id">��Ƭ���</param>
        /// <param name="UserId">�û����</param>
        /// <param name="PhotoUrls">�������������ɾ����¼�еĵ�����Ƭ��Url</param>
        /// <returns>������Ӱ������</returns>
        public int Del(string Id, int UserId, out string PhotoUrls)
        {
            PhotoUrls = GetPhotoUrl(Id, UserId);

            SqlParameter MyPar = new SqlParameter("@UserId", SqlDbType.BigInt, 8);
            MyPar.Value = UserId;

            string sql = "delete from [" + Pre + "_Shop_Photo] where Id in(" + Id + ") and UserId=@UserId";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, MyPar);
        }

        /// <summary>
        /// ɾ��ȫ����Ƭ
        /// </summary>
        /// <param name="UserId">�û����</param>
        /// <param name="PhotoUrls">�������������ɾ����¼�еĵ�����Ƭ��Url</param>
        /// <returns>������Ӱ�������</returns>
        public int DelAll(int UserId, out string PhotoUrls)
        {
            PhotoUrls = GetPhotoUrl(null, UserId);

            SqlParameter MyPar = new SqlParameter("@UserId", SqlDbType.BigInt, 8);
            MyPar.Value = UserId;

            string sql = "delete from [" + Pre + "_Shop_Photo] where UserId=@UserId";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, MyPar);
        }

        /// <summary>
        /// ��ȡָ��������Ƭ��ŵ�Url
        /// </summary>
        /// <param name="Id">������Ƭ��ţ������ǵ�������</param>
        /// <param name="UserId">�û����</param>
        /// <returns>������ƬUrl</returns>
        private string GetPhotoUrl(string Id, int UserId)
        {
            SqlParameter MyPar = new SqlParameter("@UserId", SqlDbType.BigInt, 8);
            MyPar.Value = UserId;

            string sql;
            if (Id != null)
            {
                sql = "select [Url] from [" + Pre + "_Shop_Photo] where Id in(" + Id + ") and UserId=@UserId";
            }
            else 
            {
                sql = "select [Url] from [" + Pre + "_Shop_Photo] where UserId=@UserId";
            }

            SqlDataReader dr = SqlHelper.ExecuteReader(ConnStr, CommandType.Text, sql, MyPar);

            string PhotoUrls = "";
            while (dr.Read())
            {
                PhotoUrls += PhotoUrls != "" ? "," + Convert.ToString(dr["Url"]) : Convert.ToString(dr["Url"]);
            }
            return PhotoUrls;
        }

        /// <summary>
        /// ���浥����Ƭ��Ϣ
        /// </summary>
        /// <param name="ShopPhoto">������Ƭ����</param>
        /// <returns>������Ӱ������</returns>
        public int Save(PhotoInfo ShopPhoto)
        {
            SqlParameter[] MyPar = new SqlParameter[4];
            MyPar[0] = new SqlParameter("@Id", SqlDbType.BigInt, 8);
            MyPar[0].Value = ShopPhoto.Id;
            MyPar[1] = new SqlParameter("@Description", SqlDbType.NVarChar, 50);
            MyPar[1].Value = ShopPhoto.Description;
            MyPar[2] = new SqlParameter("@OrderNum", SqlDbType.Int, 4);
            MyPar[2].Value = ShopPhoto.OrderNum;
            MyPar[3] = new SqlParameter("@UserId", SqlDbType.BigInt, 8);
            MyPar[3].Value = ShopPhoto.UserId;

            string sql = "update [" + Pre + "_Shop_Photo] set [Description]=@Description,[OrderNum]=@OrderNum where [Id]=@Id and [UserId]=@UserId";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, MyPar);
        }

        /// <summary>
        /// ��ȡָ���û�������Ƭ������
        /// </summary>
        /// <param name="UserId">�û����</param>
        /// <returns>������Ƭ����</returns>
        public int GetTotalPhoto(int UserId) 
        {
            SqlParameter MyPar = new SqlParameter("@UserId", SqlDbType.BigInt, 8);
            MyPar.Value = UserId;

            string sql = "select count(*) from [" + Pre + "_Shop_Photo] where UserId=@UserId";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnStr, CommandType.Text, sql, MyPar));
        }

        /// <summary>
        /// ��DataReader�е�����ת��Ϊ���󼯺�
        /// </summary>
        /// <param name="dr">DataReader����</param>
        /// <returns>���󼯺�</returns>
        public IList<PhotoInfo> DrRead(SqlDataReader dr)
        {
            IList<PhotoInfo> ShopPhotoList = new List<PhotoInfo>();
            while (dr.Read())
            {
                PhotoInfo ShopPhoto = new PhotoInfo();
                ShopPhoto.Id = Convert.ToInt32(dr["Id"]);
                ShopPhoto.Url = Convert.ToString(dr["Url"]);
                ShopPhoto.Ext = Convert.ToString(dr["Ext"]);
                ShopPhoto.Description = Convert.ToString(dr["Description"]);
                ShopPhoto.AddDate = Convert.ToDateTime(dr["AddDate"]);
                ShopPhoto.OrderNum = Convert.ToInt32(dr["OrderNum"]);
                ShopPhoto.UserId = Convert.ToInt32(dr["UserId"]);

                ShopPhotoList.Add(ShopPhoto);
            }
            dr.Close();
            return ShopPhotoList;
        }

        /// <summary>
        /// ��ȡ������Ƭ�б�
        /// </summary>
        /// <param name="UserId">�û����</param>
        /// <returns>������Ƭ�����б�</returns>
        public IList<PhotoInfo> GetList(int UserId)
        {
            string sql = "select * from [" + Pre + "_Shop_Photo] where [UserId]=@UserId order by [OrderNum] desc";
            SqlParameter MyPar = new SqlParameter("@UserId", SqlDbType.BigInt, 8);
            MyPar.Value = UserId;

            return DrRead(SqlHelper.ExecuteReader(ConnStr, CommandType.Text, sql, MyPar));
        }

        /// <summary>
        /// ��ȡ���ŵ�����Ƭ
        /// </summary>
        /// <param name="UserId">�û����</param>
        /// <returns>���ŵ�����Ƭ��Ϣ</returns>
        public PhotoInfo GetByUserId(int UserId)
        {
            string sql = "select top 1 * from [" + Pre + "_Shop_Photo] where [UserId]=@UserId order by [OrderNum]";
            SqlParameter MyPar = new SqlParameter("@UserId", SqlDbType.BigInt, 8);
            MyPar.Value = UserId;

            IList<PhotoInfo> ShopPhotoList = DrRead(SqlHelper.ExecuteReader(ConnStr, CommandType.Text, sql, MyPar));
            if (ShopPhotoList.Count > 0)
                return ShopPhotoList[0];
            else
                return null;
        }
    }
}
