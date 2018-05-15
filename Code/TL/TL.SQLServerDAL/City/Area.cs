using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using TL.Model.City;

namespace TL.SQLServerDAL.City
{
    /// <summary>
    /// ����->����
    /// </summary>
    public class Area : CityDB
    {
        /// <summary>
        /// ��ȡ���������б�
        /// </summary>
        /// <returns>���������б�</returns>
        public IList<AreaInfo> GetList()
        {
            string sql = "select * from [" + Pre + "_Area] order by [OrderNum]";
            SqlDataReader dr = SqlHelper.ExecuteReader(ConnStr, CommandType.Text, sql, null);

            IList<AreaInfo> AreaList = new List<AreaInfo>();
            while (dr.Read())
            {
                AreaInfo Area = new AreaInfo();
                Area.Id = Convert.ToInt32(dr["Id"]);
                Area.Name = Convert.ToString(dr["Name"]);
                Area.OrderNum = Convert.ToInt32(dr["OrderNum"]);
                AreaList.Add(Area);
            }
            dr.Close();
            return AreaList;
        }

        /// <summary>
        /// ��ȡ�ص��б�
        /// </summary>
        /// <param name="AreaId">�������</param>
        /// <returns>�ص�����б�</returns>
        public IList<AreaSubInfo> GetSubList(int AreaId)
        {
            string sql = "select * from [" + Pre + "_AreaSub] where [AreaId]=@AreaId order by [OrderNum]";
            SqlParameter MyPar = new SqlParameter("@AreaId", SqlDbType.Int, 4);
            MyPar.Value = AreaId;
            SqlDataReader dr = SqlHelper.ExecuteReader(ConnStr, CommandType.Text, sql, MyPar);

            IList<AreaSubInfo> AreaSubList =new List<AreaSubInfo>();
            while(dr.Read())
            {
                AreaSubInfo AreaSub =new AreaSubInfo();
                AreaSub.Id=Convert.ToInt32(dr["Id"]);
                AreaSub.Name=Convert.ToString(dr["Name"]);
                AreaSub.OrderNum= Convert.ToInt32(dr["OrderNum"]);
                AreaSub.AreaId = Convert.ToInt32(dr["AreaId"]);
                AreaSubList.Add(AreaSub);
            }
            dr.Close();
            return AreaSubList;
        }

        /// <summary>
        /// ��ȡ�ص��б�
        /// </summary>
        /// <param name="AreaId">�������</param>
        /// <returns>�ص��б��Json�ַ���</returns>
        public string GetSubListToJson(int AreaId)
        {
            string sql = "select [Id], [Name] from [" + Pre + "_AreaSub] where [AreaId]=@AreaId order by [OrderNum]";
            SqlParameter MyPar = new SqlParameter("@AreaId", SqlDbType.Int, 4);
            MyPar.Value = AreaId;
            return ToJson(SqlHelper.ExecuteReader(ConnStr, CommandType.Text, sql, MyPar));
        }

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <param name="Id">�������</param>
        /// <returns>��������</returns>
        public string GetName(int Id)
        {
            string sql = "select top 1 [Name] from [" + Pre + "_Area] where [Id]=@Id";
            SqlParameter MyPar = new SqlParameter("@Id", SqlDbType.Int, 4);
            MyPar.Value = Id;
            object Result = SqlHelper.ExecuteScalar(ConnStr, CommandType.Text, sql, MyPar);
            if (Result != null)
                return Result.ToString();
            else
                return null;
        }

        /// <summary>
        /// ��ȡ�ص�����
        /// </summary>
        /// <param name="Id">�ص���</param>
        /// <returns>�ص�����</returns>
        public string GetSubName(int Id)
        {
            string sql = "select top 1 [Name] from [" + Pre + "_AreaSub] where [Id]=@Id";
            SqlParameter MyPar = new SqlParameter("@Id", SqlDbType.Int, 4);
            MyPar.Value = Id;
            object Result = SqlHelper.ExecuteScalar(ConnStr, CommandType.Text, sql, MyPar);
            if (Result != null)
                return Result.ToString();
            else
                return null;
        }
    }
}
