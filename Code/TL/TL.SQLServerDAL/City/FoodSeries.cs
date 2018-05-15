using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using TL.Model.City;

namespace TL.SQLServerDAL.City
{
    /// <summary>
    /// 主菜系
    /// </summary>
    public class FoodSeries: CityDB
    {
        /// <summary>
        /// 获取主菜系对象列表
        /// </summary>
        /// <returns>主菜系对象列表</returns>
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
        /// 获取子菜系列表
        /// </summary>
        /// <param name="FoodSeriesId">地区编号</param>
        /// <returns>地点对象列表</returns>
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
        /// 获取子菜系列表
        /// </summary>
        /// <param name="FoodSeriesId">主菜系编号</param>
        /// <returns>子菜系列表的Json字符串</returns>
        public string GetSubListToJson(int FoodSeriesId)
        {
            string sql = "select [Id], [Name] from [" + Pre + "_FoodSeriesSub] where [FoodSeriesId]=@FoodSeriesId order by [OrderNum]";
            SqlParameter MyPar = new SqlParameter("@FoodSeriesId", SqlDbType.Int, 4);
            MyPar.Value = FoodSeriesId;
            return ToJson(SqlHelper.ExecuteReader(ConnStr, CommandType.Text, sql, MyPar));
        }

        /// <summary>
        /// 获取主菜系名称
        /// </summary>
        /// <param name="Id">主菜系编号</param>
        /// <returns>主菜系名称</returns>
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
        /// 获取子菜系名称
        /// </summary>
        /// <param name="Id">子菜系编号</param>
        /// <returns>子菜系名称</returns>
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
