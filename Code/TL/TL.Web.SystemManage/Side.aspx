<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Side.aspx.cs" Inherits="TL.Web.SystemManage.Side" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
    var subNavItems;
    var SubNavLiItems;
    addEvent(window, "load", function(){
        changeTarget();
        subNavItems = $T("subNav", "ul");
        SubNavLiItems = $T("subNav", "li");
        for (var i = 0; i < SubNavLiItems.length; i++) {
            SubNavLiItems[i].onclick = function(){
                clickSubNavLi(this);
            }
        }
        if (subNavItems.length > 0) 
            subNavItems[subNavItems.length - 1].style.display = "block";
    })
    
    function clickSubNavLi(clickObj){
        for (var i = 0; i < SubNavLiItems.length; i++) {
            removeAttr(SubNavLiItems[i], "class");
            if (SubNavLiItems[i] == clickObj) {
                setAttr(SubNavLiItems[i], "class", "click");
            }
        }
    }
    
    function showSubNav(subNavIndex){
        for (var i = 0; i < subNavItems.length; i++) {
            subNavItems[i].style.display = "none";
            if (i == subNavIndex) 
                subNavItems[i].style.display = "block";
        }
    }
    
    function changeTarget(){
        var a = $T(document, "a");
        for (var i = 0; i < a.length; i++) {
            setAttr(a[i], "target", "mainFrame");
        }
    }
    </script>
</head>
<body>

    <div id="subNav">
        <ul>
            <li><a href="sys/paramedit.aspx">网站参数设置</a></li>
            <li><a href="sys/useradd.aspx">添加管理员</a></li>
            <li><a href="sys/user.aspx">管理员列表</a></li>
            <li><a href="loginout.aspx">退出系统</a></li>
        </ul>
    </div>
    
</body>
</html>
