<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="TL.Web.MyShop.Index" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Content-Language" content="zh-CN"/>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>我的店铺(<%=CurrentCity.Name %>) - <%=S.SiteName %></title>
    <script type="text/javascript" src="<%=ResourcesUrl %>Common/TL/TL-Core.js"></script>
    <script type="text/javascript">
    if(self!=top)
        top.location.href=location.href;
    </script>
</head>
<frameset rows="70,*" cols="*" frameborder="no" border="0" framespacing="0">
    <frame src="header.aspx" id="headerFrame" name="headerFrame" scrolling="No" noresize="noresize" />
    <frameset rows="*,30" framespacing="0" frameborder="no" border="0">
        <frameset cols="180,*" frameborder="no" border="0" framespacing="0">
            <frame src="side.aspx" id="sideFrame" name="sideFrame" scrolling="No" noresize="noresize" />
            <frameset cols="6,*" framespacing="0" frameborder="no" border="0">
                <frame src="split.aspx" id="splitFrame" name="splitFrame" scrolling="No" noresize="noresize" />
                <!--[if IE 6]>
                <frame src="shopphoto.aspx" id="mainFrame" name="mainFrame" scrolling="Yes" noresize="noresize" />
                <![endif]-->
                <!--[if gte IE 7]>
                <frame src="shopphoto.aspx" id="mainFrame" name="mainFrame" style="overflow:auto" noresize="noresize" />
                <![endif]-->
                <!--[if !IE]><!-->
                <frame src="shopphoto.aspx" id="mainFrame" name="mainFrame" style="overflow:auto" noresize="noresize" />
                <!--<![endif]-->
            </frameset>
        </frameset>
        <frame src="footer.aspx" id="footerFrame" name="footerFrame" scrolling="No" noresize="noresize" />
    </frameset>
</frameset>
<noframes><body></body></noframes>
</html>
