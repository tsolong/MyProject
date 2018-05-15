<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ParamEdit.aspx.cs" Inherits="TL.Web.SystemManage.Sys.ParamEdit" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Content-Language" content="zh-CN"/>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title></title>
    <link type="text/css" rel="stylesheet" href="<%=ResourcesUrl %>Common/TL/TL-Base.css" />
    <link type="text/css" rel="stylesheet" href="<%=ResourcesUrl %>Common/TL/TL-More.css" />
    <link type="text/css" rel="stylesheet" href="<%=ResourcesUrl %>SystemManage/Style/Page.css" />
    <script type="text/javascript" src="<%=ResourcesUrl %>Common/TL/TL-Core.js"></script>
    <script type="text/javascript" src="<%=ResourcesUrl %>Common/TL/TL-More.js"></script>
    <script type="text/javascript" src="<%=ResourcesUrl %>SystemManage/Script/Common.js"></script>
    <script type="text/javascript">
    function checkEdit(){
        var SiteName = $("SiteName");
        var SiteSubName = $("SiteSubName");
	    var SiteDomain = $("SiteDomain");
	    var SiteEmail = $("SiteEmail");
	    var Keywords = $("Keywords");
	    var Description = $("Description");
	    var Copyright = $("Copyright");
	    
	    if (SiteName.value == "") {
		    new win({
			    title: "系统提示",
			    msg: "请填写网站名称",
			    closeEvent : function(){
			        SiteName.focus();
			    }
		    })
		    return false;
	    }
	    if (SiteSubName.value == "") {
		    new win({
			    title: "系统提示",
			    msg: "请填写网站子标题",
			    closeEvent : function(){
			        SiteSubName.focus();
			    }
		    })
		    return false;
	    }
	    else if (SiteDomain.value == "") {
		    new win({
			    title: "系统提示",
			    msg: "请填写网站域名",
			    closeEvent : function(){
			        SiteDomain.focus();
			    }
		    })
		    return false;
	    }
	    else if (SiteEmail.value == "") {
		    new win({
			    title: "系统提示",
			    msg: "请填写网站邮箱",
			    closeEvent : function(){
			        SiteEmail.focus();
			    }
		    })
		    return false;
	    }
		else if (!/^[\w\.-]+@[\w\.-]+\.\w+$/i.test(SiteEmail.value.toLowerCase())) {
			new win({
			    type : 4,
			    title: "系统提示",
			    msg: "邮箱地址无效",
			    closeEvent : function(){
			        SiteEmail.focus();
			    }
		    })
		    return false;
		}
	    else if (Keywords.value == "") {
		    new win({
			    title: "系统提示",
			    msg: "请填写关键词",
			    closeEvent : function(){
			        Keywords.focus();
			    }
		    })
		    return false;
	    }
	    else if (Description.value == "") {
		    new win({
			    title: "系统提示",
			    msg: "请填写网页描述",
			    closeEvent : function(){
			        Description.focus();
			    }
		    })
		    return false;
	    }
	    else if (Copyright.value == "") {
		    new win({
			    title: "系统提示",
			    msg: "请填写网站版权信息",
			    closeEvent : function(){
			        Copyright.focus();
			    }
		    })
		    return false;
	    }
    }
    </script>
</head>
<body>

<div class="panel">

    <div class="toolBar">
        <div class="currentMenu">网站参数设置</div>
        <a href="javascript:location.reload();"><b><span class="refresh">刷新</span></b></a>
        <a href="javascript:history.go(-1);"><b><span class="back">返回</span></b></a>
    </div>

    <div class="content">

        <form action="?action=save" method="post" onsubmit="return checkEdit();">
        
            <table class="editTab">
                <tr>
                    <th><label for="SiteName">网站名称：</label></th>
                    <td><input type="text" id="SiteName" name="SiteName" class="txt" value="<%=S.SiteName %>" /></td>
                    <td class="rowMsg"><p><span class="required">*</span></p></td>
                </tr>
                <tr>
                    <th><label for="SiteSubName">网站子标题：</label></th>
                    <td><input type="text" id="SiteSubName" name="SiteSubName" class="txt" value="<%=S.SiteSubName %>" /></td>
                    <td class="rowMsg"><p><span class="required">*</span></p></td>
                </tr>
                <tr>
                    <th><label for="SiteDomain">网站域名：</label></th>
                    <td><input type="text" id="SiteDomain" name="SiteDomain" class="txt" value="<%=S.SiteDomain %>" /></td>
                    <td class="rowMsg"><p><span class="required">*</span></p></td>
                </tr>
                <tr>
                    <th><label for="SiteEmail">网站邮箱：</label></th>
                    <td><input type="text" id="SiteEmail" name="SiteEmail" class="txt" value="<%=S.SiteEmail %>" /></td>
                    <td class="rowMsg"><p><span class="required">*</span></p></td>
                </tr>
                <tr>
                    <th><label for="Keywords">关键词：</label></th>
                    <td><textarea id="Keywords" name="Keywords" class="txtarea"><%=S.Keywords %></textarea></td>
                    <td class="rowMsg"><p><span class="required">*</span></p></td>
                </tr>
                <tr>
                    <th><label for="Description">网页描述：</label></th>
                    <td><textarea id="Description" name="Description" class="txtarea"><%=S.Description%></textarea></td>
                    <td class="rowMsg"><p><span class="required">*</span></p></td>
                </tr>
                <tr>
                    <th><label for="Copyright">版权信息：</label></th>
                    <td><textarea id="Copyright" name="Copyright" class="txtarea"><%=S.Copyright %></textarea></td>
                    <td class="rowMsg"><p><span class="required">*</span></p></td>
                </tr>
            </table>
            
            <div class="formBtn"><button type="submit">保 存</button><button type="reset">清 空</button></div>
            
        </form>
        
    </div>
    
</div>

</body>
</html>
