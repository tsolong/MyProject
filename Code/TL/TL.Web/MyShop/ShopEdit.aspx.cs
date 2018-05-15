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
                    ShowWindow(4, "ϵͳ��ʾ", "��ѡ��������ڵ���", null, true);
                }
                if (AreaSub == string.Empty)
                {
                    ShowWindow(4, "ϵͳ��ʾ", "��ѡ��������ڵص�", null, true);
                }

                string FoodSeries = Tools.GetForm("FoodSeries");
                string FoodSeriesSub = Tools.GetForm("FoodSeriesSub");
                if (FoodSeries == string.Empty || FoodSeriesSub == string.Empty)
                {
                    ShowWindow(4, "ϵͳ��ʾ", "��ѡ���������Ӫ�Ĳ�ϵ,���ֻ��ѡ�� 4 ����", null, true);
                }
                if (FoodSeries.Split(',').Length > 4 || FoodSeriesSub.Split(',').Length > 4)
                {
                    ShowWindow(4, "ϵͳ��ʾ", "��������Ӫ�Ĳ�ϵ���ֻ��ѡ�� 4 ����", null, true);
                }

                string ShopName = Tools.GetForm("ShopName");
                if (ShopName == string.Empty)
                {
                    ShowWindow(4, "ϵͳ��ʾ", "����д���̵���ʵ����", null, true);
                }

                string Address = Tools.GetForm("Address");
                if (Address == string.Empty)
                {
                    ShowWindow(4, "ϵͳ��ʾ", "����д���̵���ϸ��ַ", null, true);
                }

                string MarkAddress = Tools.GetForm("MarkAddress");
                if (MarkAddress == string.Empty)
                {
                    ShowWindow(4, "ϵͳ��ʾ", "���ڵ�ͼ�ϱ�ע��������λ��", null, true);
                }

                string Route = Tools.GetForm("Route");
                if (Route == string.Empty)
                {
                    ShowWindow(4, "ϵͳ��ʾ", "����д��ͨ·��", null, true);
                }

                string Phone = Tools.GetForm("Phone");
                if (Phone == string.Empty)
                {
                    ShowWindow(4, "ϵͳ��ʾ", "����д��ϵ�绰����", null, true);
                }

                string MobilePhone = Tools.GetForm("MobilePhone");
                if (MobilePhone == string.Empty)
                {
                    ShowWindow(4, "ϵͳ��ʾ", "������ϵ�ֻ�����", null, true);
                }

                string Email = Tools.GetForm("Email").ToLower();
                if (Email == string.Empty)
                {
                    ShowWindow(4, "ϵͳ��ʾ", "����д����", null, true);
                }
                else
                {
                    if (Regex.IsMatch(Email, @"^[\w\.-]+@[\w\.-]+\.\w+$"))
                    {
                        if (Email != Shop_User.Email)
                        {
                            if (new BLL.City.Shop.User().CheckEmailIsExist(Email))
                            {
                                ShowWindow(4, "ϵͳ��ʾ", "�������ѱ�ʹ�ã����������İ�", null, true);
                            }
                        }
                    }
                    else
                    {
                        ShowWindow(4, "ϵͳ��ʾ", "�����ַ��Ч", null, true);
                    }
                }

                string Consume = Tools.GetForm("Consume");
                if (Consume == string.Empty)
                {
                    ShowWindow(4, "ϵͳ��ʾ", "��ѡ���˾�����", null, true);
                }

                string Level = Tools.GetForm("Level");
                if (Level == string.Empty)
                {
                    ShowWindow(4, "ϵͳ��ʾ", "��ѡ������Ǽ�", null, true);
                }

                string Balcony = Tools.GetForm("Balcony");
                if (Balcony == string.Empty)
                {
                    ShowWindow(4, "ϵͳ��ʾ", "��ѡ���Ƿ��а���", null, true);
                }

                string Takeaway = Tools.GetForm("Takeaway");
                if (Takeaway == string.Empty)
                {
                    ShowWindow(4, "ϵͳ��ʾ", "��ѡ���Ƿ�������", null, true);
                }

                string Card = Tools.GetForm("Card");
                if (Card == string.Empty)
                {
                    ShowWindow(4, "ϵͳ��ʾ", "��ѡ���Ƿ����ˢ������", null, true);
                }

                string Park = Tools.GetForm("Park");
                if (Park == string.Empty)
                {
                    ShowWindow(4, "ϵͳ��ʾ", "��ѡ���Ƿ���ͣ����", null, true);
                }

                string Intro = Tools.GetForm("Intro");
                if (Intro == string.Empty)
                {
                    ShowWindow(4, "ϵͳ��ʾ", "������ĵ������¼򵥵Ľ��ܣ�50-500��)", null, true);
                }
                else if (Intro.Length < 50 || Intro.Length > 500)
                {
                    ShowWindow(4, "ϵͳ��ʾ", "���̽�����������ȷ,Ӧ����50-500�֡�", null, true);
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
                    ShowWindow(4, "ϵͳ��ʾ", "��ֵ����", null, true);
                }

                if (new BLL.City.Shop.Shop().Save(MyShopSave) != 0)
                    ShowWindow(3, "ϵͳ��ʾ", "������Ϣ����ɹ�", "shopedit.aspx", false);
                else
                    ShowWindow(4, "ϵͳ��ʾ", "������Ϣ����ʧ��", null, true);
            }

            MyShop = new BLL.City.Shop.Shop().GetById(Shop_User.UserId);
            AreaList = new BLL.City.Area().GetList();
            FoodSeriesList = new BLL.City.FoodSeries().GetList();
        }
    }
}
