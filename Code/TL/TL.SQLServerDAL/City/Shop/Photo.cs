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
    /// 店铺照片
    /// </summary>
    public class Photo : CityDB
    {
        /// <summary>
        /// 添加店铺照片
        /// </summary>
        /// <param name="ShopUser">用户对象</param>
        /// <returns>操作所影响的行数</returns>
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
        /// 删除店铺照片 可删除单个或多个
        /// </summary>
        /// <param name="Id">照片编号</param>
        /// <param name="UserId">用户编号</param>
        /// <param name="PhotoUrls">输出参数，返回删除记录中的店铺照片的Url</param>
        /// <returns>操作所影响行数</returns>
        public int Del(string Id, int UserId, out string PhotoUrls)
        {
            PhotoUrls = GetPhotoUrl(Id, UserId);

            SqlParameter MyPar = new SqlParameter("@UserId", SqlDbType.BigInt, 8);
            MyPar.Value = UserId;

            string sql = "delete from [" + Pre + "_Shop_Photo] where Id in(" + Id + ") and UserId=@UserId";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, MyPar);
        }

        /// <summary>
        /// 删除全部照片
        /// </summary>
        /// <param name="UserId">用户编号</param>
        /// <param name="PhotoUrls">输出参数，返回删除记录中的店铺照片的Url</param>
        /// <returns>操作所影响的行数</returns>
        public int DelAll(int UserId, out string PhotoUrls)
        {
            PhotoUrls = GetPhotoUrl(null, UserId);

            SqlParameter MyPar = new SqlParameter("@UserId", SqlDbType.BigInt, 8);
            MyPar.Value = UserId;

            string sql = "delete from [" + Pre + "_Shop_Photo] where UserId=@UserId";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, MyPar);
        }

        /// <summary>
        /// 获取指定店铺照片编号的Url
        /// </summary>
        /// <param name="Id">店铺照片编号，可以是单个或多个</param>
        /// <param name="UserId">用户编号</param>
        /// <returns>店铺照片Url</returns>
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
        /// 保存单张照片信息
        /// </summary>
        /// <param name="ShopPhoto">店铺照片对象</param>
        /// <returns>操作所影响行数</returns>
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
        /// 获取指定用户店铺照片的张数
        /// </summary>
        /// <param name="UserId">用户编号</param>
        /// <returns>店铺照片张数</returns>
        public int GetTotalPhoto(int UserId) 
        {
            SqlParameter MyPar = new SqlParameter("@UserId", SqlDbType.BigInt, 8);
            MyPar.Value = UserId;

            string sql = "select count(*) from [" + Pre + "_Shop_Photo] where UserId=@UserId";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnStr, CommandType.Text, sql, MyPar));
        }

        /// <summary>
        /// 将DataReader中的数据转换为对象集合
        /// </summary>
        /// <param name="dr">DataReader对象</param>
        /// <returns>对象集合</returns>
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
        /// 获取店铺照片列表
        /// </summary>
        /// <param name="UserId">用户编号</param>
        /// <returns>店铺照片对象列表</returns>
        public IList<PhotoInfo> GetList(int UserId)
        {
            string sql = "select * from [" + Pre + "_Shop_Photo] where [UserId]=@UserId order by [OrderNum] desc";
            SqlParameter MyPar = new SqlParameter("@UserId", SqlDbType.BigInt, 8);
            MyPar.Value = UserId;

            return DrRead(SqlHelper.ExecuteReader(ConnStr, CommandType.Text, sql, MyPar));
        }

        /// <summary>
        /// 获取单张店铺照片
        /// </summary>
        /// <param name="UserId">用户编号</param>
        /// <returns>单张店铺照片信息</returns>
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
