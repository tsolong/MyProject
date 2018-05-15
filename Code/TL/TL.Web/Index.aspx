<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="TL.Web.Index" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
    <script type="text/javascript" src="<%=ResourcesUrl %>Common/TL/TL-Core.js"></script>
    <script type="text/javascript" src="<%=ResourcesUrl %>Common/TL/TL-More.js"></script>
    <script type="text/javascript" src="<%=ResourcesUrl %>UI/Common/Script/Page.js"></script>
</head>
<body>

<div id="container">
    
    <!--#include file="Include/Header.aspx"-->
    
    <h1><a href="/search/">开始搜索</a></h1>
    <!--#include file="Include/Footer.aspx"-->
    
</div>

</body>
</html>
