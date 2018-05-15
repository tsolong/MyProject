<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="TL.Web.Register" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Content-Language" content="zh-CN"/>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <meta name="Keywords" content="<%=S.Keywords %>" />
    <meta name="Description" content="<%=S.Description %>" />
    <title><%=RegisterShop ? CurrentCity.Name + "店铺注册" : "会员注册"%> - <%=S.SiteName %> - <%=S.SiteSubName %></title>
    <link type="text/css" rel="stylesheet" href="<%=ResourcesUrl %>Common/TL/TL-Base.css" />
    <link type="text/css" rel="stylesheet" href="<%=ResourcesUrl %>Common/TL/TL-More.css" />
    <link type="text/css" rel="stylesheet" href="<%=ResourcesUrl %>UI/Common/Style/Page.css" />
    <link type="text/css" rel="stylesheet" href="<%=ResourcesUrl %>UI/Register/Style/Register.css" />
    <script type="text/javascript" src="<%=ResourcesUrl %>Common/TL/TL-Core.js"></script>
    <script type="text/javascript" src="<%=ResourcesUrl %>Common/TL/TL-More.js"></script>
    <script type="text/javascript" src="<%=ResourcesUrl %>UI/Common/Script/Page.js"></script>
    <script type="text/javascript" src="<%=ResourcesUrl %>UI/Register/Script/Register.js"></script>
</head>
<body>

<div id="container">
    
    <!--#include file="Include/Header.aspx"-->
    
    <div class="splitLine"></div>

    <!--<div style="margin:50px 0;"><a href="register.aspx">会员用户注册</a>　<a href="register.aspx?type=shop"><%=CurrentCity.Name %>店铺用户注册</a></div>-->
    
    <div id="register">
    
        <div class="topMessage">
            <div class="top"><span></span></div>
            <div class="msg">
			    <h2>欢迎加入<%=S.SiteName %></h2>
        	    <p>填写完下面的信息，你将成为<%=S.SiteName %> <%=RegisterShop ? CurrentCity.Name + "店铺" : "会员"%>用户</p>
            </div>
            <div class="bottom"><span></span></div>
        </div>

        <form action="?<%=RegisterShop?"type=shop&":"" %>action=add" method="post" onsubmit="return checkRegister();">
        
            <table>
                <!--<tr>
                    <td colspan="3" class="splitLine"><div><%=RegisterShop ? "店铺" : "会员"%>帐户信息</div></td>
                </tr>-->
                <tr>
                    <th><label for="UserName">用 户 名：</label></th>
                    <td><input type="text" id="UserName" name="UserName" class="txt" tip="<p>用户名只能由英文字母a～z(不区分大小写)、数字0～9、下划线组成，长度为4-16个字符。</p>" /></td>
                    <td class="registerInfo"></td>
                </tr>
                <tr>
                    <th><label for="Password">密　　码：</label></th>
                    <td><input type="password" id="Password" name="Password" class="txt" tip="<p>密码只能由英文字母a～z(不区分大小写)、数字0～9、下划线组成，长度为4-16个字符。</p>" /></td>
                    <td></td>
                </tr>
                <tr>
                    <th><label for="ConfirmPassword">确认密码：</label></th>
                    <td><input type="password" id="ConfirmPassword" name="ConfirmPassword" class="txt" tip="请再填写一次密码。" /></td>
                    <td></td>
                </tr>
                <tr>
                    <th><label for="Email">邮　　箱：</label></th>
                    <td><input type="text" id="Email" name="Email" class="txt" tip="<p>请填写您常用的邮箱地址，找回密码时会用到。</p>" /></td>
                    <td></td>
                </tr>
                <tr>
                    <th><label for="ValidateCode">验 证 码：</label></th>
                    <td>
                        <img id="ValidateImage" src="ValidateImage.aspx" title="看不清？点击换一张" />
                        <input id="ValidateCode" name="ValidateCode" class="txt" tip="<p>照抄图片里的4位数字。如果看不清？点击图片换一张。</p>" />
                    </td>
                    <td><div id=""></div></td>
                </tr>
                <tr>
                    <td colspan="3">
                        <label title="只有接受用户服务协议才能注册"><input type="checkbox" checked disabled="disabled" /> 我已阅读并接受《<a href="javascript:void(0);">用户服务协议</a>》</label>
                    </td>
                </tr>
            </table>
            
            <div><button type="submit" class="submit" title="点击按钮即可完成注册"></button></div>
            
        </form>
        
        <script type="text/javascript">
        //文本框事件
        inputEvent();
        //更换验证码图片事件
        changeValidateImage();
        //绑定输入检查事件
        bindCheckEvent();
        </script>
        
    </div>
    
    <!--#include file="Include/Footer.aspx"-->
    
</div>

</body>
</html>
