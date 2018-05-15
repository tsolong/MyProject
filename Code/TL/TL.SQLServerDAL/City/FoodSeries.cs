using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using TL.Model.City;

namespace TL.SQLServerDAL.City
{
    /// <summary>
    /// ����ϵ
    /// </summary>
    public class FoodSeries: CityDB
    {
        /// <summary>
        /// ��ȡ����ϵ�����б�
        /// </summary>
        /// <returns>����ϵ�����б�</returns>
        public IList<FoodSeriesInfo> GetList() 
        {
            string sql = "select * from [" + Pre + "_FoodSeries] order by [OrderNum]";
            SqlDataReader dr = SqlHelper.ExecuteReader(ConnStr, CommandType.Text, sql, null);

            IList<FoodSeriesInfo> FoodSeriesList = new List<FoodSeriesInfo>();
            while (dr.Read())
            {
                FoodSeriesInfo FoodSeries = new FoodSeriesInfo();
                FoodSeries.Id = Convert.ToInt32(dr["Id"]);
                FoodSeries.Name = Convert.ToString(dr["Name"]);
                FoodSeries.OrderNum = Convert.ToInt32(dr["OrderNum"]);
                FoodSeriesList.Add(FoodSeries);
            }
            dr.Close();
            return FoodSeriesList;
        }

        /// <summary>
        /// ��ȡ�Ӳ�ϵ�б�
        /// </summary>
        /// <param name="FoodSeriesId">�������</param>
        /// <returns>�ص�����б�</returns>
        public IList<FoodSeriesSubInfo> GetSubList(int FoodSeriesId)
        {
            string sql = "select * from [" + Pre + "_FoodSeriesSub] where [FoodSeriesId]=@FoodSeriesId order by [OrderNum]";
            SqlParameter MyPar = new SqlParameter("@FoodSeriesId", SqlDbType.Int, 4);
            MyPar.Value = FoodSeriesId;
            SqlDataReader dr = SqlHelper.ExecuteReader(ConnStr, CommandType.Text, sql, MyPar);

            IList<FoodSeriesSubInfo> FoodSeriesSubList = new List<FoodSeriesSubInfo>();
            while (dr.Read())
            {
                FoodSeriesSubInfo FoodSeriesSub = new FoodSeriesSubInfo();
                FoodSeriesSub.Id = Convert.ToInt32(dr["Id"]);
                FoodSeriesSub.Name = Convert.ToString(dr["Name"]);
                FoodSeriesSub.OrderNum = Convert.ToInt32(dr["OrderNum"]);
                FoodSeriesSub.FoodSeriesId = Convert.ToInt32(dr["FoodSeriesId"]);
                FoodSeriesSubList.Add(FoodSeriesSub);
            }
            dr.Close();
            return FoodSeriesSubList;
        }

        /// <summary>
        /// ��ȡ�Ӳ�ϵ�б�
        /// </summary>
        /// <param name="FoodSeriesId">����ϵ���</param>
        /// <returns>�Ӳ�ϵ�б��Json�ַ���</returns>
        public string GetSubListToJson(int FoodSeriesId)
        {
            string sql = "select [Id], [Name] from [" + Pre + "_FoodSeriesSub] where [FoodSeriesId]=@FoodSeriesId order by [OrderNum]";
            SqlParameter MyPar = new SqlParameter("@FoodSeriesId", SqlDbType.Int, 4);
            MyPar.Value = FoodSeriesId;
            return ToJson(SqlHelper.ExecuteReader(ConnStr, CommandType.Text, sql, MyPar));
        }

        /// <summary>
        /// ��ȡ����ϵ����
        /// </summary>
        /// <param name="Id">����ϵ���</param>
        /// <returns>����ϵ����</returns>
        public string GetName(int Id)
        {
            string sql = "select top 1 [Name] from [" + Pre + "_FoodSeries] where [Id]=@Id";
            SqlParameter MyPar = new SqlParameter("@Id", SqlDbType.Int, 4);
            MyPar.Value = Id;
            object Result = SqlHelper.ExecuteScalar(ConnStr, CommandType.Text, sql, MyPar);
            if (Result != null)
                return Result.ToString();
            else
                return null;
        }

        /// <summary>
        /// ��ȡ�Ӳ�ϵ����
        /// </summary>
        /// <param name="Id">�Ӳ�ϵ���</param>
        /// <returns>�Ӳ�ϵ����</returns>
        public string GetSubName(int Id)
        {
            string sql = "select top 1 [Name] from [" + Pre + "_FoodSeriesSub] where [Id]=@Id";
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
