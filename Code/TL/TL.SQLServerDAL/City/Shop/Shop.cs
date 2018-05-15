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
    /// 店铺
    /// </summary>
    public class Shop : CityDB
    {
        /// <summary>
        /// 获取单个店铺信息
        /// </summary>
        /// <param name="UserId">店铺编号</param>
        /// <returns>店铺信息对象</returns>
        public ShopInfo GetById(int UserId)
        {
            string sql = "select top 1 * from [" + Pre + "_Shop_User] where [UserId]=@UserId";
            SqlParameter MyPar = new SqlParameter("@UserId", SqlDbType.BigInt, 8);
            MyPar.Value = UserId;

            ShopInfo MyShop = null;

            SqlDataReader dr = SqlHelper.ExecuteReader(ConnStr, CommandType.Text, sql, MyPar);
            while (dr.Read())
            {
                MyShop = new  ShopInfo();

                MyShop.UserId = Convert.ToInt32(dr["UserId"]);
                MyShop.IsOnline = Convert.ToBoolean(dr["IsOnline"]);
                if (dr["Area"] == DBNull.Value) { MyShop.Area = null; } else { MyShop.Area = Convert.ToInt32(dr["Area"]); };
                if (dr["AreaSub"] == DBNull.Value) { MyShop.AreaSub = null; } else { MyShop.AreaSub = Convert.ToInt32(dr["AreaSub"]); };
                MyShop.FoodSeries = dr["FoodSeries"] == DBNull.Value ? "" : Convert.ToString(dr["FoodSeries"]);
                MyShop.FoodSeriesSub = dr["FoodSeriesSub"] == DBNull.Value ? "" : Convert.ToString(dr["FoodSeriesSub"]);
                MyShop.ShopName = dr["ShopName"] == DBNull.Value ? "" : Convert.ToString(dr["ShopName"]);
                MyShop.Address = dr["Address"] == DBNull.Value ? "" : Convert.ToString(dr["Address"]);
                MyShop.MarkAddress = dr["MarkAddress"] == DBNull.Value ? "" : Convert.ToString(dr["MarkAddress"]);
                MyShop.Route = dr["Route"] == DBNull.Value ? "" : Convert.ToString(dr["Route"]);
                MyShop.Phone = dr["Phone"] == DBNull.Value ? "" : Convert.ToString(dr["Phone"]);
                MyShop.MobilePhone = dr["MobilePhone"] == DBNull.Value ? "" : Convert.ToString(dr["MobilePhone"]);
                MyShop.Email = Convert.ToString(dr["Email"]);
                if (dr["Consume"] == DBNull.Value) { MyShop.Consume = null; } else { MyShop.Consume = Convert.ToInt32(dr["Consume"]); };
                if (dr["Level"] == DBNull.Value) { MyShop.Level = null; } else { MyShop.Level = Convert.ToInt32(dr["Level"]); };
                if (dr["Balcony"] == DBNull.Value) { MyShop.Balcony = null; } else { MyShop.Balcony = Convert.ToInt32(dr["Balcony"]); };
                if (dr["Takeaway"] == DBNull.Value) { MyShop.Takeaway = null; } else { MyShop.Takeaway = Convert.ToInt32(dr["Takeaway"]); };
                if (dr["Card"] == DBNull.Value) { MyShop.Card = null; } else { MyShop.Card = Convert.ToInt32(dr["Card"]); };
                if (dr["Park"] == DBNull.Value) { MyShop.Park = null; } else { MyShop.Park = Convert.ToInt32(dr["Park"]); };
                MyShop.ShopHours = dr["ShopHours"] == DBNull.Value ? "" : Convert.ToString(dr["ShopHours"]);
                MyShop.TotalSeat = dr["TotalSeat"] == DBNull.Value ? "" : Convert.ToString(dr["TotalSeat"]);
                MyShop.WebSite = dr["WebSite"] == DBNull.Value ? "" : Convert.ToString(dr["WebSite"]);
                MyShop.Equipment = dr["Equipment"] == DBNull.Value ? "" : Convert.ToString(dr["Equipment"]);
                MyShop.Intro = dr["Intro"] == DBNull.Value ? "" : Convert.ToString(dr["Intro"]);
                MyShop.Remark = dr["Remark"] == DBNull.Value ? "" : Convert.ToString(dr["Remark"]);
            }
            dr.Close();
            return MyShop;
        }


        /// <summary>
        /// 保存店铺信息
        /// </summary>
        /// <param name="MyShop">店铺对象</param>
        /// <returns>操作所影响的行数</returns>
        public int Save(ShopInfo MyShop)
        {
            SqlParameter[] MyPar = new SqlParameter[25];

            MyPar[0] = new SqlParameter("@UserId", SqlDbType.Int, 8); 
            MyPar[0].Value = MyShop.UserId;

            MyPar[1] = new SqlParameter("@IsOnline", SqlDbType.Bit, 1);
            MyPar[1].Value = 1;

            MyPar[2] = new SqlParameter("@Area", SqlDbType.Int, 4);
            MyPar[2].Value = MyShop.Area;

            MyPar[3] = new SqlParameter("@AreaSub", SqlDbType.Int, 4);
            MyPar[3].Value = MyShop.AreaSub;

            MyPar[4] = new SqlParameter("@FoodSeries", SqlDbType.NVarChar, 50);
            MyPar[4].Value = MyShop.FoodSeries;

            MyPar[5] = new SqlParameter("@FoodSeriesSub", SqlDbType.NVarChar, 50);
            MyPar[5].Value = MyShop.FoodSeriesSub;

            MyPar[6] = new SqlParameter("@ShopName", SqlDbType.NVarChar, 20);
            MyPar[6].Value = MyShop.ShopName;

            MyPar[7] = new SqlParameter("@Address", SqlDbType.NVarChar, 50);
            MyPar[7].Value = MyShop.Address;

            MyPar[8] = new SqlParameter("@MarkAddress", SqlDbType.NVarChar, 30);
            MyPar[8].Value = MyShop.MarkAddress;

            MyPar[9] = new SqlParameter("@Route", SqlDbType.NVarChar, 50);
            MyPar[9].Value = MyShop.Route;

            MyPar[10] = new SqlParameter("@Phone", SqlDbType.NVarChar, 20);
            MyPar[10].Value = MyShop.Phone;

            MyPar[11] = new SqlParameter("@MobilePhone", SqlDbType.NVarChar, 11);
            MyPar[11].Value = MyShop.MobilePhone;

            MyPar[12] = new SqlParameter("@Email", SqlDbType.NVarChar, 30);
            MyPar[12].Value = MyShop.Email;

            MyPar[13] = new SqlParameter("@Consume", SqlDbType.Int, 4);
            MyPar[13].Value = MyShop.Consume;

            MyPar[14] = new SqlParameter("@Level", SqlDbType.Int, 4);
            MyPar[14].Value = MyShop.Level;

            MyPar[15] = new SqlParameter("@Balcony", SqlDbType.Int, 4);
            MyPar[15].Value = Convert.ToInt32(MyShop.Balcony);

            MyPar[16] = new SqlParameter("@Takeaway", SqlDbType.Int, 4);
            MyPar[16].Value = Convert.ToInt32(MyShop.Takeaway);

            MyPar[17] = new SqlParameter("@Card", SqlDbType.Int, 4);
            MyPar[17].Value = Convert.ToInt32(MyShop.Card);

            MyPar[18] = new SqlParameter("@Park", SqlDbType.Int, 4);
            MyPar[18].Value = MyShop.Park;

            MyPar[19] = new SqlParameter("@ShopHours", SqlDbType.NVarChar, 30);
            MyPar[19].Value = MyShop.ShopHours;

            MyPar[20] = new SqlParameter("@TotalSeat", SqlDbType.NVarChar, 5);
            MyPar[20].Value = MyShop.TotalSeat;

            MyPar[21] = new SqlParameter("@WebSite", SqlDbType.NVarChar, 50);
            MyPar[21].Value = MyShop.WebSite;

            MyPar[22] = new SqlParameter("@Equipment", SqlDbType.NText);
            MyPar[22].Value = MyShop.Equipment;

            MyPar[23] = new SqlParameter("@Intro", SqlDbType.NText);
            MyPar[23].Value = MyShop.Intro;

            MyPar[24] = new SqlParameter("@Remark", SqlDbType.NText);
            MyPar[24].Value = MyShop.Remark;

            string sql = "update [" + Pre + "_Shop_User] set"+
                "[IsOnline]=@IsOnline," +
                "[Area]=@Area," +
                "[AreaSub]=@AreaSub," +
                "[FoodSeries]=@FoodSeries," +
                "[FoodSeriesSub]=@FoodSeriesSub," +
                "[ShopName]=@ShopName," +
                "[Address]=@Address," +
                "[MarkAddress]=@MarkAddress," +
                "[Route]=@Route," +
                "[Phone]=@Phone," +
                "[MobilePhone]=@MobilePhone," +
                "[Email]=@Email," +
                "[Consume]=@Consume," +
                "[Level]=@Level," +
                "[Balcony]=@Balcony," +
                "[Takeaway]=@Takeaway," +
                "[Card]=@Card," +
                "[Park]=@Park," +
                "[ShopHours]=@ShopHours," +
                "[TotalSeat]=@TotalSeat," +
                "[WebSite]=@WebSite," +
                "[Equipment]=@Equipment," +
                "[Intro]=@Intro," +
                "[Remark]=@Remark " +
                "where [UserId]=@UserId";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, MyPar);
        }
    }
}
