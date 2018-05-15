<div id="header">
        <div class="logo">Logo</div>
        <div class="city">
            <span class="current"><%=CurrentCity.Name %></span>
            <span id="otherCity">[其它城市]
                <ul><%
                    System.Collections.Generic.IList<TL.Model.City.CityInfo> CityList = TL.Config.SysConfig.GetCityList();
                    for (int i = 0; i < CityList.Count; i++)
                    {
                %>
                    <li><a href="http://<%=CityList[i].Domain %>/"><%=CityList[i].Name %></a></li><%
                    }
                %>
                </ul>
            </span>
        </div>
        <div class="headerNav">
            <a href="/">首页</a> | 
            <a href="/login.aspx">会员登录</a> | 
            <a href="/register.aspx">会员注册</a> | 
            <a href="/myshop/login.aspx"><%=CurrentCity.Name %>店铺登录</a> | 
            <a href="/register.aspx?type=shop"><%=CurrentCity.Name %>店铺注册</a> | 
            <a href="javascript:void(0);">帮助</a> | 
            <a href="javascript:void(0);" onclick="setHome(this,'<%=S.SiteDomain %>')">设为首页</a> | 
            <a href="javascript:void(0);" onclick="addFavorite('<%=S.SiteDomain %>','<%=S.SiteName %> - <%=S.SiteSubName %>');">加入收藏</a>
        </div>
    </div>