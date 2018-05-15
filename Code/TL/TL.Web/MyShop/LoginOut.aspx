<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginOut.aspx.cs" Inherits="TL.Web.MyShop.LoginOut" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Content-Language" content="zh-CN"/>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title><%=S.SiteName %></title>
    <link type="text/css" rel="stylesheet" href="<%=ResourcesUrl %>Common/TL/TL-More.css" />
    <script type="text/javascript" src="<%=ResourcesUrl %>Common/TL/TL-Core.js"></script>
    <script type="text/javascript" src="<%=ResourcesUrl %>Common/TL/TL-More.js"></script>
    <script type="text/javascript">
    if (self != top)
        top.location.href = location.href;
        
    addEvent(window, "load", function(){
        new win({
            type: 3,
            title: "系统提示",
            msg: "您已成功退出系统 <a href=\"login.aspx\">重新登录</a>",
            closeEvent: function(){
                location.href = "login.aspx";
            },
            isOverlay: false,
            dragOpacity: 1
        })
    })
    </script>
</head>
<body>　
</body>
</html>
