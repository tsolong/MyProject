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
        //���������б�
        public IList<AreaInfo> AreaList;
        //�����ص�����б�
        public IList<AreaSubInfo> AreaSubList;
        //����ϵ�����б�
        public IList<FoodSeriesInfo> FoodSeriesList;
        //�Ӳ�ϵ�����б�
        public IList<FoodSeriesSubInfo> FoodSeriesSubList;

        //�����б�
        public IList<ShopInfo> ShopList;


        #region ��������

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
            #region ��ȡ����

            //����
            try { Area = Convert.ToInt32(Tools.GetQueryString("area")); }
            catch { Area = 0; }

            //�����ص�
            try { AreaSub = Convert.ToInt32(Tools.GetQueryString("areasub")); }
            catch { AreaSub = 0; }

            //����ϵ
            try { FoodSeries = Convert.ToInt32(Tools.GetQueryString("foodseries")); }
            catch { FoodSeries = 0; }

            //�Ӳ�ϵ
            try { FoodSeriesSub = Convert.ToInt32(Tools.GetQueryString("foodseriessub")); }
            catch { FoodSeriesSub = 0; }

            //�˾�����
            try { Consume = Convert.ToInt32(Tools.GetQueryString("consume")); }
            catch { Consume = 0; }

            //�Ǽ�
            try { Level = Convert.ToInt32(Tools.GetQueryString("level")); }
            catch { Level = 0; }

            //����
            try { Balcony = Convert.ToInt32(Tools.GetQueryString("balcony")); }
            catch { Balcony = 0; }

            //����
            try { Takeaway = Convert.ToInt32(Tools.GetQueryString("takeaway")); }
            catch { Takeaway = 0; }

            //ˢ��
            try { Card = Convert.ToInt32(Tools.GetQueryString("card")); }
            catch { Card = 0; }

            //ͣ����
            try { Park = Convert.ToInt32(Tools.GetQueryString("park")); }
            catch { Park = 0; }

            #endregion


            //��ȡ���������б�
            AreaList = new BLL.City.Area().GetList();
            if (Area != 0)
                AreaSubList = new BLL.City.Area().GetSubList(Area);

            //��ȡ��ϵ�����б�
            FoodSeriesList = new BLL.City.FoodSeries().GetList();
            if (FoodSeries != 0)
                FoodSeriesSubList = new BLL.City.FoodSeries().GetSubList(FoodSeries);

            //��ȡ�������̶����б�
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
        /// ��ȡ��������Url
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
