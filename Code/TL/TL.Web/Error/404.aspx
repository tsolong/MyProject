<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="404.aspx.cs" Inherits="TL.Web.Error._04" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>系统提示： 对不起，你访问的页面不存在 !</title>
    <style type="text/css">
    body{ margin:0;	font-size:12px; font-family:"宋体";}
    #error{ margin:150px auto 0 auto; width:600px; position:relative;}
    #error .title{ height:26px; line-height:26px; text-indent:10px; color:#fff; font-weight:bold; background:url(/error/images/title.gif) no-repeat;}
    #error .content{ height:170px; text-indent:165px; border:2px solid #c2d0d9; border-top:none; background:url(/error/images/info.gif) 20px center no-repeat;}
    #error .content img{ margin:60px 0 0 0;}
    #error .btn{ position:absolute;	bottom:20px; right:0px;}
    #error .btn img{ margin:0 30px 0 0; cursor:pointer;}
    </style>
    <script type="text/javascript">
    window.onload = function(){
	    document.images[1].onclick = function(){
		    location.href = "/";
	    };
	    document.images[2].onclick = function(){
		    history.go(-1);
	    }
    }
    </script>
</head>
<body>
    <div id="error">
	    <div class="title">系统提示</div>
	    <div class="content"><img src="/error/images/msg.gif" /></div>
	    <div class="btn"><img src="/error/images/home.gif" title="首页" /><img src="/error/images/back.gif" title="返回前一页" /></div>
    </div>
</body>
</html>
