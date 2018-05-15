<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShopEdit.aspx.cs" Inherits="TL.Web.MyShop.ShopEdit" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Content-Language" content="zh-CN"/>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title></title>
    <link type="text/css" rel="stylesheet" href="<%=ResourcesUrl %>Common/TL/TL-Base.css" />
    <link type="text/css" rel="stylesheet" href="<%=ResourcesUrl %>Common/TL/TL-More.css" />
    <link type="text/css" rel="stylesheet" href="<%=ResourcesUrl %>MyShop/Style/Page.css" />
    <script type="text/javascript" src="<%=ResourcesUrl %>Common/TL/TL-Core.js"></script>
    <script type="text/javascript" src="<%=ResourcesUrl %>Common/TL/TL-More.js"></script>
    <script type="text/javascript" src="<%=ResourcesUrl %>MyShop/Script/Common.js"></script>
    <script type="text/javascript" src="<%=ResourcesUrl %>MyShop/Script/ShopEdit.js"></script>
    <script type="text/javascript" src=" http://api.51ditu.com/js/maps.js "></script>
    <style  type="text/css">
    .currentFoodSeries{
        border:1px dashed #000;
        margin:0 0 10px 0;
        padding:5px 10px;
        line-height:18px;
    }
    .currentFoodSeries b{
    }
    .currentFoodSeries li{
        list-style-type:decimal;
        list-style-position:inside;
    }
    #AreaSub,#FoodSeriesSubPanel{display:none;}
    #addFoodSeriesBtn{margin:0 89px 0 24px;}
    </style>
</head>
<body>

<div class="panel">

    <div class="toolBar">
        <div class="currentMenu">店铺信息设置</div>
        <a href="javascript:location.reload();"><b><span class="refresh">刷新</span></b></a>
        <a href="javascript:history.go(-1);"><b><span class="back">返回</span></b></a>
    </div>
    
    <input type="hidden" id="_CityName" value="<%=CurrentCity.Name %>" />
    <input type="hidden" id="_CityEName" value="<%=CurrentCity.EName %>" />

    <div class="content">

        <form id="shopEditForm" action="?action=save" method="post" onsubmit="return checkEdit();">
        
            <table class="editTab">
                <tr>
                    <th><label>所在城市：</label></th>
                    <td><%=CurrentCity.Name %></td>
                    <td class="rowMsg"><p><span class="required">*</span> 此项不可设置，您注册的是<%=CurrentCity.Name %>店铺。</p></td>
                </tr>
                <tr>
                    <th><label for="Area">所在地区：</label></th>
                    <td>
                        <select id="Area" name="Area">
                            <option value="">请选择</option>
                            <%
                                for (int i = 0; i < AreaList.Count; i++)
                                {
                                    if (MyShop.Area == AreaList[i].Id)
                                        Response.Write("<option value=\"" + AreaList[i].Id + "\" selected>" + AreaList[i].Name + "</option>");
                                    else
                                        Response.Write("<option value=\"" + AreaList[i].Id + "\">" + AreaList[i].Name + "</option>");
                                }
                            %>
                        </select>
                        <select id="AreaSub" name="AreaSub"></select>
                        <%if (MyShop.AreaSub != null) {%>
                        <script type="text/javascript">
                        getAreaSub.call($("Area"), function(){
		                    for (var i = 0; i < $("AreaSub").options.length; i++) {
			                    var op = $("AreaSub").options[i];
			                    if (op.value == "<%=MyShop.AreaSub %>") 
				                    op.selected = true;
		                    }
	                    })
                        </script>
                        <%} %>
                    </td>
                    <td class="rowMsg"><p><span class="required">*</span> 请选择店铺在<%=CurrentCity.Name %>的地区以及地点。</p></td>
                </tr>
                <tr>
                    <th><label>经营菜系：</label></th>
                    <td>
                        <%if (MyShop.FoodSeries != "" && MyShop.FoodSeriesSub != "")
                          {
                              Response.Write("<ol class=\"currentFoodSeries\"><b>经营菜系：</b>");
                              string[] tempStr1 = MyShop.FoodSeries.Split(',');
                              string[] tempStr2 = MyShop.FoodSeriesSub.Split(',');

                              TL.BLL.City.FoodSeries BllFoodSeries = new TL.BLL.City.FoodSeries();
                              for (int i = 0; i < tempStr1.Length; i++)
                              {
                                  Response.Write("<li>" + BllFoodSeries.GetName(Convert.ToInt32(tempStr1[i].Substring(1, tempStr1[i].Length - 2))) + "->" + BllFoodSeries.GetSubName(Convert.ToInt32(tempStr2[i].Substring(1, tempStr2[i].Length - 2))) + "</li>");
                              }
                              Response.Write("</ol>");
                          } %>
                        <select id="selFoodSeries">
                            <option value=""><%=MyShop.FoodSeries != "" ? "重新选择" : "请选择"%></option>
                            <%for (int i = 0; i < FoodSeriesList.Count; i++) {%>
                            <option value="<%= FoodSeriesList[i].Id %>"><%=FoodSeriesList[i].Name %></option>
                            <%} %>
                        </select>
                        <div id="FoodSeriesSubPanel">
                            <select id="selFoodSeriesSub" multiple="multiple" style="width:130px; height:200px;"></select>
                            <select id="selMyFoodSeriesSub" multiple="multiple" style="width:200px; height:200px;"></select>
                            <div>
                                <button type="button" id="addFoodSeriesBtn">添加</button>
                                <button type="button" id="delFoodSeriesBtn">删除</button>
                            </div>
                        </div>
                        <input type="hidden" id="FoodSeries" name="FoodSeries" class="txt" value="<%=MyShop.FoodSeries %>" />
                        <input type="hidden" id="FoodSeriesSub" name="FoodSeriesSub" class="txt" value="<%=MyShop.FoodSeriesSub %>" />
                    </td>
                    <td class="rowMsg"><p><span class="required">*</span> 请选择店铺所经营的菜系,最多只能选择4个。</p></td>
                </tr>
                <tr>
                    <th><label for="ShopName">店铺名称：</label></th>
                    <td><input type="text" id="ShopName" name="ShopName" class="txt" value="<%=MyShop.ShopName %>" /></td>
                    <td class="rowMsg"><p><span class="required">*</span> 请填写店铺的真实名称。</p></td>
                </tr>
                <tr>
                    <th><label for="Address">详细地址：</label></th>
                    <td><input type="text" id="Address" name="Address" class="txt" value="<%=MyShop.Address %>" /></td>
                    <td class="rowMsg"><p><span class="required">*</span> 请填写店铺的详细地址。</p></td>
                </tr>
                <tr>
                    <th><label onclick="showMarkMap();">标注地址：</label></th>
                    <td>
                        <input type="hidden" id="MarkAddress" name="MarkAddress" class="txt" value="<%=MyShop.MarkAddress %>" />
                        <button type="button" id="markBtn" onclick="showMarkMap();"><%=MyShop.MarkAddress != String.Empty ? "查看 / 重标" : "点击标注"%></button>
                    </td>
                    <td class="rowMsg"><p><span class="required">*</span> 请在地图上标注店铺所在位置。</p></td>
                </tr>
                <tr>
                    <th><label for="Route">交通路线：</label></th>
                    <td><input type="text" id="Route" name="Route" class="txt" value="<%=MyShop.Route %>" /></td>
                    <td class="rowMsg"><p><span class="required">*</span> 路线之间请用","号隔开，如：（989路,581路,地铁1号线）</p></td>
                </tr>
                <tr>
                    <th><label for="Phone">电　　话：</label></th>
                    <td><input type="text" id="Phone" name="Phone" class="txt" value="<%=MyShop.Phone %>" /></td>
                    <td class="rowMsg"><p><span class="required">*</span> 请填写联系电话号码</p></td>
                </tr>
                <tr>
                    <th><label for="MobilePhone">手　　机：</label></th>
                    <td><input type="text" id="MobilePhone" name="MobilePhone" class="txt" value="<%=MyShop.MobilePhone %>" /></td>
                    <td class="rowMsg"><p><span class="required">*</span> 请填联系手机号码</p></td>
                </tr>
                <tr>
                    <th><label for="Email">邮　　箱：</label></th>
                    <td><input type="text" id="Email" name="Email" class="txt" value="<%=MyShop.Email %>" /></td>
                    <td class="rowMsg"><p><span class="required">*</span> 请填写邮箱地址</p></td>
                </tr>
                <tr>
                    <th><label for="Consume">人均消费：</label></th>
                    <td>
                        <select id="Consume" name="Consume">
                            <option value="">请选择</option>
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
                                    if (MyShop.Consume.ToString() == ConsumeString[i, 0])
                                        Response.Write("<option value=\"" + ConsumeString[i, 0] + "\" selected>" + ConsumeString[i, 1] + "</option>");
                                    else
                                        Response.Write("<option value=\"" + ConsumeString[i, 0] + "\">" + ConsumeString[i, 1] + "</option>");
                                }
                            %>
                        </select>
                    </td>
                    <td class="rowMsg"><p><span class="required">*</span> 请选择人均消费</p></td>
                </tr>
                <tr>
                    <th><label for="Level">店铺星级：</label></th>
                    <td>
                        <select id="Level" name="Level">
                            <option value="">请选择</option>
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
                                    if (MyShop.Level.ToString() == LevelString[i, 0])
                                        Response.Write("<option value=\"" + LevelString[i, 0] + "\" selected>" + LevelString[i, 1] + "</option>");
                                    else
                                        Response.Write("<option value=\"" + LevelString[i, 0] + "\">" + LevelString[i, 1] + "</option>");
                                }
                            %>
                        </select>
                    </td>
                    <td class="rowMsg"><p><span class="required">*</span> 请选择店铺星级</p></td>
                </tr>
                <tr>
                    <th><label>包　　厢：</label></th>
                    <td>
                    <%
                        string[,] BalconyString = new string[,]{ 
                            { "1", "有" },
                            { "2", "无" }
                        };
                        for (int i = 0; i < BalconyString.Length / 2; i++)
                        {
                            if (MyShop.Balcony.ToString() == BalconyString[i, 0])
                                Response.Write("<input type=\"radio\" name=\"Balcony\" value=\"" + BalconyString[i, 0] + "\" checked />" + BalconyString[i, 1]);
                            else
                                Response.Write("<input type=\"radio\" name=\"Balcony\" value=\"" + BalconyString[i, 0] + "\" />" + BalconyString[i, 1]);
                        }
                    %>
                    </td>
                    <td class="rowMsg"><p><span class="required">*</span> 有无包厢</p></td>
                </tr>
                <tr>
                    <th><label>外　　卖：</label></th>
                    <td>
                    <%
                        string[,] TakeawayString = new string[,]{ 
                            { "1", "有" },
                            { "2", "无" }
                        };
                        for (int i = 0; i < TakeawayString.Length / 2; i++)
                        {
                            if (MyShop.Takeaway.ToString() == TakeawayString[i, 0])
                                Response.Write("<input type=\"radio\" name=\"Takeaway\" value=\"" + TakeawayString[i, 0] + "\" checked />" + TakeawayString[i, 1]);
                            else
                                Response.Write("<input type=\"radio\" name=\"Takeaway\" value=\"" + TakeawayString[i, 0] + "\" />" + TakeawayString[i, 1]);
                        }
                    %>
                    </td>
                    <td class="rowMsg"><p><span class="required">*</span> 有无外卖</p></td>
                </tr>
                <tr>
                    <th><label>刷　　卡：</label></th>
                    <td>
                    <%
                        string[,] CardString = new string[,]{ 
                            { "1", "有" },
                            { "2", "无" }
                        };
                        for (int i = 0; i < CardString.Length / 2; i++)
                        {
                            if (MyShop.Card.ToString() == CardString[i, 0])
                                Response.Write("<input type=\"radio\" name=\"Card\" value=\"" + CardString[i, 0] + "\" checked />" + CardString[i, 1]);
                            else
                                Response.Write("<input type=\"radio\" name=\"Card\" value=\"" + CardString[i, 0] + "\" />" + CardString[i, 1]);
                        }
                    %>
                    </td>
                    <td class="rowMsg"><p><span class="required">*</span> 可不可以刷卡</p></td>
                </tr>
                <tr>
                    <th><label>停 车 场：</label></th>
                    <td>
                    <%
                        string[,] ParkString = new string[,]{ 
                            { "1", "有（收费）" },
                            { "2", "有（免费）" },
                            { "3", "无" }
                        };
                        for (int i = 0; i < ParkString.Length / 2; i++)
                        {
                            if (MyShop.Park.ToString() == ParkString[i, 0])
                                Response.Write("<input type=\"radio\" name=\"Park\" value=\"" + ParkString[i, 0] + "\" checked />" + ParkString[i, 1]);
                            else
                                Response.Write("<input type=\"radio\" name=\"Park\" value=\"" + ParkString[i, 0] + "\" />" + ParkString[i, 1]);
                        }
                    %>
                    </td>
                    <td class="rowMsg"><p><span class="required">*</span> 有无停车场</p></td>
                </tr>
                <tr>
                    <th><label for="ShopHours">营业时间：</label></th>
                    <td><input type="text" id="ShopHours" name="ShopHours" class="txt" value="<%=MyShop.ShopHours %>" /></td>
                    <td class="rowMsg"><p>如：（每天8：00-23：00）</p></td>
                </tr>
                <tr>
                    <th><label for="TotalSeat">总座位数：</label></th>
                    <td><input type="text" id="TotalSeat" name="TotalSeat" class="txt" value="<%=MyShop.TotalSeat %>" /></td>
                    <td class="rowMsg"><p></p></td>
                </tr>
                <tr>
                    <th><label for="WebSite">店铺网址：</label></th>
                    <td><input type="text" id="WebSite" name="WebSite" class="txt" value="<%=MyShop.WebSite == "" ? "http://" : MyShop.WebSite %>" /></td>
                    <td class="rowMsg"><p></p></td>
                </tr>
                <tr>
                    <th><label for="Equipment">设备服务：</label></th>
                    <td>
                        <textarea id="Equipment" name="Equipment" class="txtarea"><%=MyShop.Equipment %></textarea>
                    </td>
                    <td class="rowMsg"><p>如：（上网,KTV,英语服务）</p></td>
                </tr>
                <tr>
                    <th><label for="Intro">店铺介绍：</label></th>
                    <td>
                        <textarea id="Intro" name="Intro" class="txtarea"><%=MyShop.Intro %></textarea>
                    </td>
                    <td class="rowMsg"><p><span class="required">*</span> 请对您的店铺做下简单的介绍（50-500字)</p></td>
                </tr>
                <tr>
                    <th><label for="Remark">备　　注：</label></th>
                    <td>
                        <textarea id="Remark" name="Remark" class="txtarea"><%=MyShop.Remark %></textarea>
                    </td>
                    <td class="rowMsg"><p>如果您还有什么想说的，就在这里说吧。</p></td>
                </tr>
            </table>
            
            <div class="formBtn"><button type="submit">保 存</button><button type="reset">清 空</button></div>
            
        </form>
        
    </div>
    
</div>

</body>
</html>
