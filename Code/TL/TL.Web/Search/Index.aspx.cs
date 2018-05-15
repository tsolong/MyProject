using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using TL.Common;
using TL.Model.City;
using TL.Model.City.Shop;
using TL.Model.City.Search;

namespace TL.Web.Search
{
    public partial class Index : TL.Web.UI.BasePage
    {
        //地区对象列表
        public IList<AreaInfo> AreaList;
        //地区地点对象列表
        public IList<AreaSubInfo> AreaSubList;
        //主菜系对象列表
        public IList<FoodSeriesInfo> FoodSeriesList;
        //子菜系对象列表
        public IList<FoodSeriesSubInfo> FoodSeriesSubList;

        //店铺列表
        public IList<ShopInfo> ShopList;


        #region 声明参数

        public int Area;
        public int AreaSub;
        public int FoodSeries;
        public int FoodSeriesSub;
        public int Consume;
        public int Level;
        public int Balcony;
        public int Takeaway;
        public int Card;
        public int Park;

        public string AreaName;
        public string AreaSubName;
        public string FoodSeriesName;
        public string FoodSeriesSubName;
        public string ConsumeStr;

        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            #region 获取参数

            //地区
            try { Area = Convert.ToInt32(Tools.GetQueryString("area")); }
            catch { Area = 0; }

            //地区地点
            try { AreaSub = Convert.ToInt32(Tools.GetQueryString("areasub")); }
            catch { AreaSub = 0; }

            //主菜系
            try { FoodSeries = Convert.ToInt32(Tools.GetQueryString("foodseries")); }
            catch { FoodSeries = 0; }

            //子菜系
            try { FoodSeriesSub = Convert.ToInt32(Tools.GetQueryString("foodseriessub")); }
            catch { FoodSeriesSub = 0; }

            //人均消费
            try { Consume = Convert.ToInt32(Tools.GetQueryString("consume")); }
            catch { Consume = 0; }

            //星级
            try { Level = Convert.ToInt32(Tools.GetQueryString("level")); }
            catch { Level = 0; }

            //包厢
            try { Balcony = Convert.ToInt32(Tools.GetQueryString("balcony")); }
            catch { Balcony = 0; }

            //外卖
            try { Takeaway = Convert.ToInt32(Tools.GetQueryString("takeaway")); }
            catch { Takeaway = 0; }

            //刷卡
            try { Card = Convert.ToInt32(Tools.GetQueryString("card")); }
            catch { Card = 0; }

            //停车场
            try { Park = Convert.ToInt32(Tools.GetQueryString("park")); }
            catch { Park = 0; }

            #endregion


            //获取地区对象列表
            AreaList = new BLL.City.Area().GetList();
            if (Area != 0)
                AreaSubList = new BLL.City.Area().GetSubList(Area);

            //获取菜系对象列表
            FoodSeriesList = new BLL.City.FoodSeries().GetList();
            if (FoodSeries != 0)
                FoodSeriesSubList = new BLL.City.FoodSeries().GetSubList(FoodSeries);

            //获取搜索店铺对象列表
            GetSearchShopList();
        }

        public void GetSearchShopList()
        {
            SearchInfo SearchInfo = new SearchInfo();
            SearchInfo.Area = Area;
            SearchInfo.AreaSub = AreaSub;
            SearchInfo.FoodSeries = FoodSeries != 0 ? "{" + FoodSeries.ToString() + "}" : "";
            SearchInfo.FoodSeriesSub = FoodSeriesSub != 0 ? "{" + FoodSeriesSub.ToString() + "}" : "";
            SearchInfo.Consume = Consume;
            SearchInfo.Level = Level;
            SearchInfo.Balcony = Balcony;
            SearchInfo.Takeaway = Takeaway;
            SearchInfo.Card = Card;
            SearchInfo.Park = Park;

            /*Response.Write(SearchInfo.Area + "<br>");
            Response.Write(SearchInfo.AreaSub + "<br>");
            Response.Write(SearchInfo.FoodSeries + "<br>");
            Response.Write(SearchInfo.FoodSeriesSub + "<br>");
            Response.Write(SearchInfo.Consume + "<br>");
            Response.Write(SearchInfo.Level + "<br>");
            Response.Write(SearchInfo.Balcony + "<br>");
            Response.Write(SearchInfo.Takeaway + "<br>");
            Response.Write(SearchInfo.Card + "<br>");
            Response.Write(SearchInfo.Park + "<br>");*/

            ShopList = new BLL.City.Search.Search().GetShopList(SearchInfo);
        }

        /// <summary>
        /// 获取搜索导航Url
        /// </summary>
        /// <param name="Str"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public string GetSearchUrl(string Str, string Value)
        {
            string SearchUrl = "/search/";

            if (Str == "Area")
                SearchUrl += "area_" + Value;
            else if (Str == "AreaAll") { }
            else if (Area != 0)
                SearchUrl += "area_" + Area;
            if (Area != 0)
            {
                if (Str == "Area" || Str == "AreaAll") { }
                else
                {
                    if (Str == "AreaSub")
                        SearchUrl += "_" + Value;
                    else if (Str == "AreaSubAll") { }
                    else if (AreaSub != 0)
                        SearchUrl += "_" + AreaSub;

                }
            }
            if (Str == "Area" || (Str != "AreaAll" && Area != 0))
                SearchUrl += "/";

            if (Str == "FoodSeries")
                SearchUrl += "foodseries_" + Value;
            else if (Str == "FoodSeriesAll") { }
            else if (FoodSeries != 0)
                SearchUrl += "foodseries_" + FoodSeries;
            if (FoodSeries != 0)
            {
                if (Str == "FoodSeries" || Str == "FoodSeriesAll") { }
                else
                {
                    if (Str == "FoodSeriesSub")
                        SearchUrl += "_" + Value;
                    else if (Str == "FoodSeriesSubAll") { }
                    else if (FoodSeriesSub != 0)
                        SearchUrl += "_" + FoodSeriesSub;
                }
            }
            if (Str == "FoodSeries" || (Str != "FoodSeriesAll" && FoodSeries != 0))
                SearchUrl += "/";

            if (Str == "Consume")
                SearchUrl += "consume_" + Value + "/";
            else
                if (Str != "ConsumeAll" && Consume != 0)
                    SearchUrl += "consume_" + Consume + "/";

            if (Str == "Level")
                SearchUrl += "level_" + Value + "/";
            else
                if (Str != "LevelAll" && Level != 0)
                    SearchUrl += "level_" + Level + "/";

            if (Str == "Balcony")
                SearchUrl += "balcony_" + Value + "/";
            else
                if (Str != "BalconyAll" && Balcony != 0)
                    SearchUrl += "balcony_" + Balcony + "/";

            if (Str == "Takeaway")
                SearchUrl += "takeaway_" + Value + "/";
            else
                if (Str != "TakeawayAll" && Takeaway != 0)
                    SearchUrl += "takeaway_" + Takeaway + "/";

            if (Str == "Card")
                SearchUrl += "card_" + Value + "/";
            else
                if (Str != "CardAll" && Card != 0)
                    SearchUrl += "card_" + Card + "/";

            if (Str == "Park")
                SearchUrl += "park_" + Value + "/";
            else
                if (Str != "ParkAll" && Park != 0)
                    SearchUrl += "park_" + Park + "/";

            return SearchUrl;
        }
    }
}
