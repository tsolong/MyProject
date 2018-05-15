<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="TL.Web.Search.Index" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Content-Language" content="zh-CN"/>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <meta name="Keywords" content="<%=S.Keywords %>" />
    <meta name="Description" content="<%=S.Description %>" />
    <title><%=S.SiteName %> - <%=S.SiteSubName %></title>
    <link type="text/css" rel="stylesheet" href="<%=ResourcesUrl %>Common/TL/TL-Base.css" />
    <link type="text/css" rel="stylesheet" href="<%=ResourcesUrl %>Common/TL/TL-More.css" />
    <link type="text/css" rel="stylesheet" href="<%=ResourcesUrl %>UI/Common/Style/Page.css" />
    <link type="text/css" rel="stylesheet" href="<%=ResourcesUrl %>UI/Search/Style/Search.css" />
    <script type="text/javascript" src="<%=ResourcesUrl %>Common/TL/TL-Core.js"></script>
    <script type="text/javascript" src="<%=ResourcesUrl %>Common/TL/TL-More.js"></script>
    <script type="text/javascript" src="<%=ResourcesUrl %>UI/Common/Script/Page.js"></script>
    <script type="text/javascript" src="<%=ResourcesUrl %>UI/Search/Script/Search.js"></script>
</head>
<body>

<div id="container">
    
    
    <!--#include file="../Include/Header.aspx"-->
    

    
    <ul class="category">
    
    <!--地区-->
    <%
        if (AreaList.Count > 0)
        {
            Response.Write("<li>");
            Response.Write("<div>");

            if (Area != 0)
                Response.Write("<a href=\"" + GetSearchUrl("AreaAll", "") + "\">全部地区</a>：");
            else
                Response.Write("全部地区：");

            Response.Write("</div><ul>");

            for (int i = 0; i < AreaList.Count; i++)
            {
                Response.Write("<li>");

                if (Area == AreaList[i].Id)
                {
                    AreaName = AreaList[i].Name;
                    Response.Write("<span class=\"sel\">" + AreaList[i].Name + "</span>");
                }
                else
                {
                    Response.Write("<a href=\"" + GetSearchUrl("Area", AreaList[i].Id.ToString()) + "\">" + AreaList[i].Name + "</a>");
                }

                Response.Write("</li>");
            }

            Response.Write("</ul>");

            if (Area != 0 && AreaSubList.Count > 0)
            {
                Response.Write("<ul class=\"sub\">");

                for (int i = 0; i < AreaSubList.Count; i++)
                {
                    Response.Write("<li>");

                    if (AreaSub == AreaSubList[i].Id)
                    {
                        AreaSubName = AreaSubList[i].Name;
                        Response.Write("<span class=\"sel\">" + AreaSubList[i].Name + "</span>");
                    }
                    else
                    {
                        Response.Write("<a href=\"" + GetSearchUrl("AreaSub", AreaSubList[i].Id.ToString()) + "\">" + AreaSubList[i].Name + "</a>");
                    }

                    Response.Write("</li>");
                }

                Response.Write("</ul>");
            }

            Response.Write("</li>");
        }
    %>
    
    <!--菜系-->
    <%
        if (FoodSeriesList.Count > 0)
        {
            Response.Write("<li>");
            Response.Write("<div>");

            if (FoodSeries != 0)
                Response.Write("<a href=\"" + GetSearchUrl("FoodSeriesAll", "") + "\">全部菜系</a>：");
            else
                Response.Write("全部菜系：");

            Response.Write("</div><ul>");

            for (int i = 0; i < FoodSeriesList.Count; i++)
            {
                Response.Write("<li>");

                if (FoodSeries == FoodSeriesList[i].Id)
                {
                    FoodSeriesName = FoodSeriesList[i].Name;
                    Response.Write("<span class=\"sel\">" + FoodSeriesList[i].Name + "</span>");
                }
                else
                {
                    Response.Write("<a href=\"" + GetSearchUrl("FoodSeries", FoodSeriesList[i].Id.ToString()) + "\">" + FoodSeriesList[i].Name + "</a>");
                }

                Response.Write("</li>");
            }

            Response.Write("</ul>");

            if (FoodSeries != 0 && FoodSeriesSubList.Count > 0)
            {
                Response.Write("<ul class=\"sub\">");

                for (int i = 0; i < FoodSeriesSubList.Count; i++)
                {
                    Response.Write("<li>");

                    if (FoodSeriesSub == FoodSeriesSubList[i].Id)
                    {
                        FoodSeriesSubName = FoodSeriesSubList[i].Name;
                        Response.Write("<span class=\"sel\">" + FoodSeriesSubList[i].Name + "</span>");
                    }
                    else
                    {
                        Response.Write("<a href=\"" + GetSearchUrl("FoodSeriesSub", FoodSeriesSubList[i].Id.ToString()) + "\">" + FoodSeriesSubList[i].Name + "</a>");
                    }

                    Response.Write("</li>");
                }

                Response.Write("</ul>");
            }

            Response.Write("</li>");
        }
    %>
    
        <!--人均消费-->
        <li>
            <div class="t">
            <%
                if (Consume != 0)
                    Response.Write("<a href=\"" + GetSearchUrl("ConsumeAll", "") + "\">全部人均</a>：");
                else
                    Response.Write("全部人均：");
            %>
            </div>
            <ul>
            <%
                string[,] ConsumeString = new string[,]{ 
                    { "1", "20元以下" },
                    { "2", "20-49" },
                    { "3", "50-99" },
                    { "4", "100-149" },
                    { "5", "150-199" },
                    { "6", "200-249" },
                    { "7", "250-299" },
                    { "8", "300-349" },
                    { "9", "350-399" },
                    { "10", "400-449" },
                    { "11", "450-499" },
                    { "12", "500及以上" }
                };
                for (int i = 0; i < ConsumeString.Length / 2; i++)
                {
                    if (Consume.ToString() == ConsumeString[i, 0])
                    {
                        Response.Write("<li><span class=\"sel\">" + ConsumeString[i, 1] + "</span></li>");
                        ConsumeStr = ConsumeString[i, 1];
                    }
                    else
                        Response.Write("<li><a href=\"" + GetSearchUrl("Consume", ConsumeString[i, 0]) + "\">" + ConsumeString[i, 1] + "</a></li>");
                }
            %>
            </ul>
        </li>
        
        <!--星级-->
        <li>
            <div class="t">
            <%
                if (Level != 0)
                    Response.Write("<a href=\"" + GetSearchUrl("LevelAll", "") + "\">全部星级</a>：");
                else
                    Response.Write("全部星级：");
            %>
            </div>
            <ul>
            <%
                string[,] LevelString = new string[,]{ 
                    { "6", "无" },
                    { "1", "一星级" },
                    { "2", "二星级" },
                    { "3", "三星级" },
                    { "4", "四星级" },
                    { "5", "五星级" }
                };
                for (int i = 0; i < LevelString.Length / 2; i++)
                {
                    if (Level.ToString() == LevelString[i, 0])
                        Response.Write("<li><span class=\"sel\">" + LevelString[i, 1] + "</span></li>");
                    else
                        Response.Write("<li><a href=\"" + GetSearchUrl("Level", LevelString[i, 0]) + "\">" + LevelString[i, 1] + "</a></li>");
                }
            %>
            </ul>
        </li>
        
        <!--包厢-->
        <li>
            <div class="t">包　　厢：</div>
            <ul>
            <%
                string[,] BalconyString = new string[,]{ 
                    { "0", "不限" },
                    { "1", "有" },
                    { "2", "无" }
                };
                for (int i = 0; i < BalconyString.Length / 2; i++)
                {
                    if (i == 0)
                    {
                        if (Balcony.ToString() == BalconyString[i, 0])
                            Response.Write("<li><span class=\"sel\">" + BalconyString[i, 1] + "</span></li>");
                        else
                            Response.Write("<li><a href=\"" + GetSearchUrl("BalconyAll", BalconyString[i, 0]) + "\">" + BalconyString[i, 1] + "</a></li>");
                    }
                    else
                    {
                        if (Balcony.ToString() == BalconyString[i, 0])
                            Response.Write("<li><span class=\"sel\">" + BalconyString[i, 1] + "</span></li>");
                        else
                            Response.Write("<li><a href=\"" + GetSearchUrl("Balcony", BalconyString[i, 0]) + "\">" + BalconyString[i, 1] + "</a></li>");
                    }
                }
            %>
            </ul>
        </li>
        
        <!--外卖-->
        <li>
            <div class="t">外　　卖：</div>
            <ul>
            <%
                string[,] TakeawayString = new string[,]{ 
                    { "0", "不限" },
                    { "1", "有" },
                    { "2", "无" }
                };
                for (int i = 0; i < TakeawayString.Length / 2; i++)
                {
                    if (i == 0)
                    {
                        if (Takeaway.ToString() == TakeawayString[i, 0])
                            Response.Write("<li><span class=\"sel\">" + TakeawayString[i, 1] + "</span></li>");
                        else
                            Response.Write("<li><a href=\"" + GetSearchUrl("TakeawayAll", TakeawayString[i, 0]) + "\">" + TakeawayString[i, 1] + "</a></li>");
                    }
                    else
                    {
                        if (Takeaway.ToString() == TakeawayString[i, 0])
                            Response.Write("<li><span class=\"sel\">" + TakeawayString[i, 1] + "</span></li>");
                        else
                            Response.Write("<li><a href=\"" + GetSearchUrl("Takeaway", TakeawayString[i, 0]) + "\">" + TakeawayString[i, 1] + "</a></li>");
                    }
                }
            %>
            </ul>
        </li>
        
        <!--刷卡-->
        <li>
            <div class="t">刷　　卡：</div>
            <ul>
            <%
                string[,] CardString = new string[,]{ 
                    { "0", "不限" },
                    { "1", "有" },
                    { "2", "无" }
                };
                for (int i = 0; i < CardString.Length / 2; i++)
                {
                    if (i == 0)
                    {
                        if (Card.ToString() == CardString[i, 0])
                            Response.Write("<li><span class=\"sel\">" + CardString[i, 1] + "</span></li>");
                        else
                            Response.Write("<li><a href=\"" + GetSearchUrl("CardAll", CardString[i, 0]) + "\">" + CardString[i, 1] + "</a></li>");
                    }
                    else
                    {
                        if (Card.ToString() == CardString[i, 0])
                            Response.Write("<li><span class=\"sel\">" + CardString[i, 1] + "</span></li>");
                        else
                            Response.Write("<li><a href=\"" + GetSearchUrl("Card", CardString[i, 0]) + "\">" + CardString[i, 1] + "</a></li>");
                    }
                }
            %>
            </ul>
        </li>
        
        <!--停车场-->
        <li>
            <div class="t">停 车 场：</div>
            <ul>
            <%
                string[,] ParkString = new string[,]{ 
                    { "0", "不限" },
                    { "1", "有（收费）" },
                    { "2", "有（免费）" },
                    { "3", "无" }
                };
                for (int i = 0; i < ParkString.Length / 2; i++)
                {
                    if (i == 0)
                    {
                        if (Park.ToString() == ParkString[i, 0])
                            Response.Write("<li><span class=\"sel\">" + ParkString[i, 1] + "</span></li>");
                        else
                            Response.Write("<li><a href=\"" + GetSearchUrl("ParkAll", ParkString[i, 0]) + "\">" + ParkString[i, 1] + "</a></li>");
                    }
                    else
                    {
                        if (Park.ToString() == ParkString[i, 0])
                            Response.Write("<li><span class=\"sel\">" + ParkString[i, 1] + "</span></li>");
                        else
                            Response.Write("<li><a href=\"" + GetSearchUrl("Park", ParkString[i, 0]) + "\">" + ParkString[i, 1] + "</a></li>");
                    }
                }
            %>
            </ul>
        </li>
        
    </ul>
    
    
    <div></div>
    
    <div class="splitLine"></div>
    
    <p>
    您本次的搜索条件是:
    <%if (Area != 0)
      { %>
        <a href="/search/area_<%=Area %>/"><%=AreaName%></a> 
    <%} %>
    <%if (AreaSub != 0)
      { %>
        > <a href="/search/area_<%=Area %>_<%=AreaSub %>/"><%=AreaSubName%></a> 
    <%} %>
    
    <%if (FoodSeries != 0)
      { %>
        <a href="/search/foodseries_<%=FoodSeries %>/"><%=FoodSeriesName%></a> 
    <%} %>
    <%if (FoodSeriesSub != 0)
      { %>
        > <a href="/search/foodseries_<%=FoodSeries %>_<%=FoodSeriesSub %>/"><%=FoodSeriesSubName%></a> 
    <%} %>
    
    <%if (Consume != 0)
      {%>
       <a href="/search/consume_<%=Consume %>/"><%=ConsumeStr %></a> 
    <%} %>
    </p>
    
    <ul id="shopList">
    <%
        TL.BLL.City.Shop.Photo BLLPhoto= new TL.BLL.City.Shop.Photo();
        TL.BLL.City.Area BLLArea = new TL.BLL.City.Area();
        TL.BLL.City.FoodSeries BLLFoodSeries = new TL.BLL.City.FoodSeries();
        for (int i = 0; i < ShopList.Count; i++)
        {
        %>
            <li>
                <div class="photo">
                <%
                    TL.Model.City.Shop.PhotoInfo ShopPhoto = BLLPhoto.GetByUserId(ShopList[i].UserId);
                    if (ShopPhoto != null)
                    {
                        string PhotoUrl = CurrentCity.PicturesUrl + ShopPhoto.Url + "100x100" + ShopPhoto.Ext;
                    
                %>
                    <img src="<%=PhotoUrl%>" />
                    <%} %>
                </div>
                <div class="info">
                    <h2><%=ShopList[i].ShopName %></h2>
                    <dl>
                        <dt>地区：</dt>
                        <dd><%=BLLArea.GetName(Convert.ToInt32(ShopList[i].Area))%> <%=BLLArea.GetSubName(Convert.ToInt32(ShopList[i].AreaSub))%></dd>
                        <dt>菜系：</dt>
                        <dd>
                        <%
                            string[] tempStr1 = ShopList[i].FoodSeries.Split(',');
                            string[] tempStr2 = ShopList[i].FoodSeriesSub.Split(',');
                            for (int j = 0; j < tempStr1.Length; j++)
                            {
                                //Response.Write(BLLFoodSeries.GetName(Convert.ToInt32(tempStr1[j].Substring(1, tempStr1[j].Length - 2))) + "->" + BLLFoodSeries.GetSubName(Convert.ToInt32(tempStr2[j].Substring(1, tempStr2[j].Length - 2))));
                                Response.Write(BLLFoodSeries.GetSubName(Convert.ToInt32(tempStr2[j].Substring(1, tempStr2[j].Length - 2))) + " ");
                            }
                        %>
                        </dd>
                        <dt>地址：</dt>
                        <dd><%=ShopList[i].Address %></dd>
                        <dt>人均消费：</dt>
                        <dd>
                        <%
                            for (int j = 0; j < ConsumeString.Length / 2; j++)
                            {
                                if (ShopList[i].Consume.ToString() == ConsumeString[j, 0])
                                    Response.Write("<b style=\"color:red;\">" + ConsumeString[j, 1] + "</b>");
                            }
                        %>
                        </dd>
                        <dt></dt>
                        <dd></dd>
                    </dl>
                </div>
                <div class="clear"></div>
            </li>
        <%
        }
    %>
    </ul>
    
    <!--#include file="../Include/Footer.aspx"-->

    <%="真实地址:"+Request.Url%>
    
</div>

</body>
</html>
