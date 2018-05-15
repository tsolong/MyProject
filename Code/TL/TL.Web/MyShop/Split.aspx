<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Split.aspx.cs" Inherits="TL.Web.MyShop.Split" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Content-Language" content="zh-CN"/>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title></title>
    <link type="text/css" rel="stylesheet" href="<%=ResourcesUrl %>Common/TL/TL-Base.css" />
    <link type="text/css" rel="stylesheet" href="<%=ResourcesUrl %>MyShop/Style/Page.css" />
    <script type="text/javascript" src="<%=ResourcesUrl %>Common/TL/TL-Core.js"></script>
    <script type="text/javascript" src="<%=ResourcesUrl %>MyShop/Script/Common.js"></script>
    <script type="text/javascript">
    addEvent(window, "load", function(){
        var flag = 0;
        var temp = null;
        var split = $("split");
        
        addEvent(split, "click", function(){
            var parObj = parent.document.getElementsByTagName("frameset")[2];
            if (parObj) {
                if (flag == 0) {
                    flag = 1;
                    temp = getAttr(parObj, "cols");
                    setAttr(parObj, "cols", "0,*")
                    split.className = "show";
                }
                else {
                    flag = 0;
                    setAttr(parObj, "cols", temp)
                    removeAttr(split, "class")
                }
            }
        })
    })
    </script>
</head>
<body id="splitBody">

    <div id="split" title="隐藏/展开 左侧菜单项"></div>
    
</body>
</html>
