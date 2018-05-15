<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Header.aspx.cs" Inherits="TL.Web.SystemManage.Header" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Content-Language" content="zh-CN"/>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title></title>
    <link type="text/css" rel="stylesheet" href="<%=ResourcesUrl %>Common/TL/TL-Base.css" />
    <link type="text/css" rel="stylesheet" href="<%=ResourcesUrl %>SystemManage/Style/Page.css" />
    <script type="text/javascript" src="<%=ResourcesUrl %>Common/TL/TL-Core.js"></script>
    <script type="text/javascript" src="<%=ResourcesUrl %>SystemManage/Script/Common.js"></script>
    <script type="text/javascript">
    addEvent(window, "load", function(){
        var navItems = $T("nav", "li");
        if (navItems.length > 0) 
            setAttr(navItems[navItems.length - 1], "class", "click");
        for (var i = 0; i < navItems.length; i++) {
            navItems[i].onclick = function(){
                showSubNav(this);
            }
        }
        
        function showSubNav(clickObj){
            for (var i = 0; i < navItems.length; i++) {
                removeAttr(navItems[i], "class");
                if (navItems[i] == clickObj) {
                    setAttr(navItems[i], "class", "click");
                    parent.frames["sideFrame"].showSubNav(i);
                }
            }
        }
    })
    </script>
</head>
<body>

    <div id="header">
    
        <a class="logo" href="<%=S.SiteDomain %>" title="<%=S.SiteName %>" target="_blank"></a>
        
        <div class="loginUserInfo">
            您好管理员，<span><%=Sys_User.UserName %></span> 
            <%if (Sys_User.LastLoginTime != null)
              { %>
            最后登录时间[ <%=Sys_User.LastLoginTime%> ]
            <%} %>
            <%if (Sys_User.LastLoginIP != "")
              { %>
            最后登录IP[ <%=Sys_User.LastLoginIP %> ]
            <%} %>
            [ <a href="loginout.aspx" target="_top">退出</a> ]
        </div>
        
        <div id="nav">
            <ul>
                <li><b>系统管理</b></li>
            </ul>
        </div>
        
    </div>
    
</body>
</html>
