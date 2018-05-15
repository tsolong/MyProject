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
using System.Text.RegularExpressions;

using TL.Common;
using TL.Model.City;
using TL.Model.City.Shop;

namespace TL.Web.MyShop
{
    public partial class ShopEdit : TL.Web.UI.ShopUserPage
    {
        public ShopInfo MyShop;
        public IList<AreaInfo> AreaList;
        public IList<FoodSeriesInfo> FoodSeriesList;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Tools.GetQueryString("action").ToLower() == "save")
            {
                string Area = Tools.GetForm("Area");
                string AreaSub = Tools.GetForm("AreaSub");
                if (Area == string.Empty)
                {
                    ShowWindow(4, "系统提示", "请选择店铺所在地区", null, true);
                }
                if (AreaSub == string.Empty)
                {
                    ShowWindow(4, "系统提示", "请选择店铺所在地点", null, true);
                }

                string FoodSeries = Tools.GetForm("FoodSeries");
                string FoodSeriesSub = Tools.GetForm("FoodSeriesSub");
                if (FoodSeries == string.Empty || FoodSeriesSub == string.Empty)
                {
                    ShowWindow(4, "系统提示", "请选择店铺所经营的菜系,最多只能选择 4 个。", null, true);
                }
                if (FoodSeries.Split(',').Length > 4 || FoodSeriesSub.Split(',').Length > 4)
                {
                    ShowWindow(4, "系统提示", "店铺所经营的菜系最多只能选择 4 个。", null, true);
                }

                string ShopName = Tools.GetForm("ShopName");
                if (ShopName == string.Empty)
                {
                    ShowWindow(4, "系统提示", "请填写店铺的真实名称", null, true);
                }

                string Address = Tools.GetForm("Address");
                if (Address == string.Empty)
                {
                    ShowWindow(4, "系统提示", "请填写店铺的详细地址", null, true);
                }

                string MarkAddress = Tools.GetForm("MarkAddress");
                if (MarkAddress == string.Empty)
                {
                    ShowWindow(4, "系统提示", "请在地图上标注店铺所在位置", null, true);
                }

                string Route = Tools.GetForm("Route");
                if (Route == string.Empty)
                {
                    ShowWindow(4, "系统提示", "请填写交通路线", null, true);
                }

                string Phone = Tools.GetForm("Phone");
                if (Phone == string.Empty)
                {
                    ShowWindow(4, "系统提示", "请填写联系电话号码", null, true);
                }

                string MobilePhone = Tools.GetForm("MobilePhone");
                if (MobilePhone == string.Empty)
                {
                    ShowWindow(4, "系统提示", "请填联系手机号码", null, true);
                }

                string Email = Tools.GetForm("Email").ToLower();
                if (Email == string.Empty)
                {
                    ShowWindow(4, "系统提示", "请填写邮箱", null, true);
                }
                else
                {
                    if (Regex.IsMatch(Email, @"^[\w\.-]+@[\w\.-]+\.\w+$"))
                    {
                        if (Email != Shop_User.Email)
                        {
                            if (new BLL.City.Shop.User().CheckEmailIsExist(Email))
                            {
                                ShowWindow(4, "系统提示", "此邮箱已被使用，换个其它的吧", null, true);
                            }
                        }
                    }
                    else
                    {
                        ShowWindow(4, "系统提示", "邮箱地址无效", null, true);
                    }
                }

                string Consume = Tools.GetForm("Consume");
                if (Consume == string.Empty)
                {
                    ShowWindow(4, "系统提示", "请选择人均消费", null, true);
                }

                string Level = Tools.GetForm("Level");
                if (Level == string.Empty)
                {
                    ShowWindow(4, "系统提示", "请选择店铺星级", null, true);
                }

                string Balcony = Tools.GetForm("Balcony");
                if (Balcony == string.Empty)
                {
                    ShowWindow(4, "系统提示", "请选择是否有包厢", null, true);
                }

                string Takeaway = Tools.GetForm("Takeaway");
                if (Takeaway == string.Empty)
                {
                    ShowWindow(4, "系统提示", "请选择是否有外卖", null, true);
                }

                string Card = Tools.GetForm("Card");
                if (Card == string.Empty)
                {
                    ShowWindow(4, "系统提示", "请选择是否可以刷卡消费", null, true);
                }

                string Park = Tools.GetForm("Park");
                if (Park == string.Empty)
                {
                    ShowWindow(4, "系统提示", "请选择是否有停车场", null, true);
                }

                string Intro = Tools.GetForm("Intro");
                if (Intro == string.Empty)
                {
                    ShowWindow(4, "系统提示", "请对您的店铺做下简单的介绍（50-500字)", null, true);
                }
                else if (Intro.Length < 50 || Intro.Length > 500)
                {
                    ShowWindow(4, "系统提示", "店铺介绍字数不正确,应该是50-500字。", null, true);
                }


                ShopInfo MyShopSave = new ShopInfo();
                try
                {
                    MyShopSave.UserId = Shop_User.UserId;
                    MyShopSave.Area = Convert.ToInt32(Area);
                    MyShopSave.AreaSub = Convert.ToInt32(AreaSub);
                    MyShopSave.FoodSeries = FoodSeries;
                    MyShopSave.FoodSeriesSub = FoodSeriesSub;
                    MyShopSave.ShopName = ShopName;
                    MyShopSave.Address = Address;
                    MyShopSave.MarkAddress = MarkAddress;
                    MyShopSave.Route = Route;
                    MyShopSave.Phone = Phone;
                    MyShopSave.MobilePhone = MobilePhone;
                    MyShopSave.Email = Email;
                    MyShopSave.Consume = Convert.ToInt32(Consume);
                    MyShopSave.Level = Convert.ToInt32(Level);
                    MyShopSave.Balcony = Convert.ToInt32(Balcony);
                    MyShopSave.Takeaway = Convert.ToInt32(Takeaway);
                    MyShopSave.Card = Convert.ToInt32(Card);
                    MyShopSave.Park = Convert.ToInt32(Park);
                    MyShopSave.ShopHours = Tools.GetForm("ShopHours");
                    MyShopSave.TotalSeat = Tools.GetForm("TotalSeat");
                    MyShopSave.WebSite = Tools.GetForm("WebSite") == "http://" ? "" : Tools.GetForm("WebSite");
                    MyShopSave.Equipment = Tools.GetForm("Equipment");
                    MyShopSave.Intro = Intro;
                    MyShopSave.Remark = Tools.GetForm("Remark");
                }
                catch
                {
                    ShowWindow(4, "系统提示", "赋值出错", null, true);
                }

                if (new BLL.City.Shop.Shop().Save(MyShopSave) != 0)
                    ShowWindow(3, "系统提示", "店铺信息保存成功", "shopedit.aspx", false);
                else
                    ShowWindow(4, "系统提示", "店铺信息保存失败", null, true);
            }

            MyShop = new BLL.City.Shop.Shop().GetById(Shop_User.UserId);
            AreaList = new BLL.City.Area().GetList();
            FoodSeriesList = new BLL.City.FoodSeries().GetList();
        }
    }
}
