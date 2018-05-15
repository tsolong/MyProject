using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using TL.Common;
using TL.Model.City;

namespace TL.Web.MyShop
{
    public partial class ShopEditAjax : TL.Web.UI.ShopUserPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = Tools.GetQueryString("action").ToLower();
            switch (action)
            {
                case "getareasub":
                    GetAreaSub();
                    break;
                case "getfoodseriessub":
                    GetFoodSeriesSub();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 获取地点
        /// </summary>
        private void GetAreaSub()
        {
            try
            {
                int AreaId = Convert.ToInt32(Tools.GetQueryString("areaid").ToLower());
                Response.Write(new BLL.City.Area().GetSubListToJson(AreaId));
            }
            catch { }
            Response.End();
        }

        /// <summary>
        /// 获取子菜系
        /// </summary>
        private void GetFoodSeriesSub()
        {
            try
            {
                int FoodSeriesId = Convert.ToInt32(Tools.GetQueryString("foodseriesid").ToLower());
                Response.Write(new BLL.City.FoodSeries().GetSubListToJson(FoodSeriesId));
            }
            catch { }
            Response.End();
        }
    }
}
