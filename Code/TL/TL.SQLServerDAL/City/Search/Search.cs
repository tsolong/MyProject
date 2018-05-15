using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using TL.Model.City;
using TL.Model.City.Shop;
using TL.Model.City.Search;

namespace TL.SQLServerDAL.City.Search
{
    /// <summary>
    /// ËÑË÷
    /// </summary>
    public class Search : CityDB
    {
        public IList<ShopInfo> GetShopList(SearchInfo SearchInfo)
        {
            IList<ShopInfo> ShopList = new List<ShopInfo>();

            SqlParameter[] MyPar = new SqlParameter[10];

            MyPar[0] = new SqlParameter("@Area", SqlDbType.Int, 4);
            MyPar[0].Value = SearchInfo.Area;

            MyPar[1] = new SqlParameter("@AreaSub", SqlDbType.Int, 4);
            MyPar[1].Value = SearchInfo.AreaSub;

            MyPar[2] = new SqlParameter("@FoodSeries", SqlDbType.NVarChar, 50);
            MyPar[2].Value = SearchInfo.FoodSeries;

            MyPar[3] = new SqlParameter("@FoodSeriesSub", SqlDbType.NVarChar, 50);
            MyPar[3].Value = SearchInfo.FoodSeriesSub;

            MyPar[4] = new SqlParameter("@Consume", SqlDbType.Int, 4);
            MyPar[4].Value = SearchInfo.Consume;

            MyPar[5] = new SqlParameter("@Level", SqlDbType.Int, 4);
            MyPar[5].Value = SearchInfo.Level;

            MyPar[6] = new SqlParameter("@Balcony", SqlDbType.Int, 4);
            MyPar[6].Value = Convert.ToInt32(SearchInfo.Balcony);

            MyPar[7] = new SqlParameter("@Takeaway", SqlDbType.Int, 4);
            MyPar[7].Value = Convert.ToInt32(SearchInfo.Takeaway);

            MyPar[8] = new SqlParameter("@Card", SqlDbType.Int, 4);
            MyPar[8].Value = Convert.ToInt32(SearchInfo.Card);

            MyPar[9] = new SqlParameter("@Park", SqlDbType.Int, 4);
            MyPar[9].Value = SearchInfo.Park;


            SqlDataReader dr = SqlHelper.ExecuteReader(ConnStr, CommandType.StoredProcedure, "SearchShop", MyPar);
            while (dr.Read())
            {
                ShopInfo Shop = new ShopInfo();

                Shop.UserId = Convert.ToInt32(dr["UserId"]);
                Shop.IsOnline = Convert.ToBoolean(dr["IsOnline"]);
                if (dr["Area"] == DBNull.Value) { Shop.Area = null; } else { Shop.Area = Convert.ToInt32(dr["Area"]); };
                if (dr["AreaSub"] == DBNull.Value) { Shop.AreaSub = null; } else { Shop.AreaSub = Convert.ToInt32(dr["AreaSub"]); };
                Shop.FoodSeries = dr["FoodSeries"] == DBNull.Value ? "" : Convert.ToString(dr["FoodSeries"]);
                Shop.FoodSeriesSub = dr["FoodSeriesSub"] == DBNull.Value ? "" : Convert.ToString(dr["FoodSeriesSub"]);
                Shop.ShopName = dr["ShopName"] == DBNull.Value ? "" : Convert.ToString(dr["ShopName"]);
                Shop.Address = dr["Address"] == DBNull.Value ? "" : Convert.ToString(dr["Address"]);
                Shop.MarkAddress = dr["MarkAddress"] == DBNull.Value ? "" : Convert.ToString(dr["MarkAddress"]);
                Shop.Route = dr["Route"] == DBNull.Value ? "" : Convert.ToString(dr["Route"]);
                Shop.Phone = dr["Phone"] == DBNull.Value ? "" : Convert.ToString(dr["Phone"]);
                Shop.MobilePhone = dr["MobilePhone"] == DBNull.Value ? "" : Convert.ToString(dr["MobilePhone"]);
                Shop.Email = Convert.ToString(dr["Email"]);
                if (dr["Consume"] == DBNull.Value) { Shop.Consume = null; } else { Shop.Consume = Convert.ToInt32(dr["Consume"]); };
                if (dr["Level"] == DBNull.Value) { Shop.Level = null; } else { Shop.Level = Convert.ToInt32(dr["Level"]); };
                if (dr["Balcony"] == DBNull.Value) { Shop.Balcony = null; } else { Shop.Balcony = Convert.ToInt32(dr["Balcony"]); };
                if (dr["Takeaway"] == DBNull.Value) { Shop.Takeaway = null; } else { Shop.Takeaway = Convert.ToInt32(dr["Takeaway"]); };
                if (dr["Card"] == DBNull.Value) { Shop.Card = null; } else { Shop.Card = Convert.ToInt32(dr["Card"]); };
                if (dr["Park"] == DBNull.Value) { Shop.Park = null; } else { Shop.Park = Convert.ToInt32(dr["Park"]); };
                Shop.ShopHours = dr["ShopHours"] == DBNull.Value ? "" : Convert.ToString(dr["ShopHours"]);
                Shop.TotalSeat = dr["TotalSeat"] == DBNull.Value ? "" : Convert.ToString(dr["TotalSeat"]);
                Shop.WebSite = dr["WebSite"] == DBNull.Value ? "" : Convert.ToString(dr["WebSite"]);
                Shop.Equipment = dr["Equipment"] == DBNull.Value ? "" : Convert.ToString(dr["Equipment"]);
                Shop.Intro = dr["Intro"] == DBNull.Value ? "" : Convert.ToString(dr["Intro"]);
                Shop.Remark = dr["Remark"] == DBNull.Value ? "" : Convert.ToString(dr["Remark"]);

                ShopList.Add(Shop);
            }
            dr.Close();

            return ShopList;
        }
    }
}
