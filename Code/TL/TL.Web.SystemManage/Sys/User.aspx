<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="TL.Web.SystemManage.Sys.User" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
    addEvent(window, "load", function(){
	    bindRowsEvent("sysUserList");
    })
    
    function del(userId){
	    if (!userId || userId == "") {
		    new win({
			    type: 1,
			    title: "系统提示",
			    msg: "请选择要删除的管理员"
		    })
	    }
	    else {
		    new win({
			    type: 5,
			    title: "系统提示",
			    msg: "删除管理员将无法恢复,你确定要删除吗?",
			    confirmEvent: function(){
				    location.href = "?action=del&userid=" + userId + "&p=<%=PageIndex %>"
			    }
		    })
	    }
    }
    
    function locked(userId){
	    if (!userId || userId == "") {
		    new win({
			    type: 1,
			    title: "系统提示",
			    msg: "请选择要锁定的管理员"
		    })
	    }
	    else {
			location.href = "?action=locked&userid=" + userId + "&p=<%=PageIndex %>"
	    }
    }
    
    function unLocked(userId){
	    if (!userId || userId == "") {
		    new win({
			    type: 1,
			    title: "系统提示",
			    msg: "请选择要解锁的管理员"
		    })
	    }
	    else {
			location.href = "?action=unlocked&userid=" + userId + "&p=<%=PageIndex %>"
	    }
    }
    </script>
</head>
<body>

<div class="panel">
    
    <div class="toolBar">
        <div class="currentMenu">管理员列表</div>
        <a href="useradd.aspx"><b><span class="add">添加</span></b></a>
        <a href="javascript:del(getSelectedValue('sysUserList'));"><b><span class="del">删除</span></b></a>
        <a href="javascript:locked(getSelectedValue('sysUserList'));"><b><span class="locked">锁定</span></b></a>
        <a href="javascript:unLocked(getSelectedValue('sysUserList'));"><b><span class="unLocked">解锁</span></b></a>
        <a href="javascript:changeSelect('sysUserList',1);"><b><span class="changeSelect1">全选</span></b></a>
        <a href="javascript:changeSelect('sysUserList',2);"><b><span class="changeSelect2">反选</span></b></a>
        <a href="javascript:changeSelect('sysUserList',3);"><b><span class="changeSelect3">不选</span></b></a>
        <a href="javascript:location.reload();"><b><span class="refresh">刷新</span></b></a>
        <a href="javascript:history.go(-1);"><b><span class="back">返回</span></b></a>
    </div>
    
    <div class="content">
    
        <%if (RecordTotal > 0){ %>
        <table id="sysUserList" class="listTab">
            <thead>
                <tr>
	                <th>选择</th>
	                <th>编号</th>
	                <th>用户名</th>
	                <th>最后一次登录时间</th>
	                <th>最后一次登录IP</th>
	                <th>锁定</th>
	                <th>创建时间</th>
	                <th>操作</th>
                </tr>
            </thead>
            <tbody>
            <%for (int i = 0; i < SysUserList.Count; i++){ %>
	            <tr>
	                <td><input type="checkbox" value="<%=SysUserList[i].UserId %>"></td>
	                <td><%=SysUserList[i].UserId%></td>
	                <td><%=SysUserList[i].UserName%></td>
	                <td><%=SysUserList[i].LastLoginTime%></td>
	                <td><%=SysUserList[i].LastLoginIP%></td>
	                <td><%=SysUserList[i].Locded ? "是" : "否"%></td>
	                <td><%=SysUserList[i].CreateDate%></td>
	                <td onmouseout='this.className="oper";' onmouseover='this.className="operHover";' class="oper">
		                <div>操作
			                <ul>
			                    <li><a href="javascript:del(<%=SysUserList[i].UserId%>);"><div>删除</div></a></li>
				                <li><a href="userpassword.aspx?userid=<%=SysUserList[i].UserId%>&p=<%=PageIndex %>"><div>修改密码</div></a></li>
				                <%if (SysUserList[i].Locded){%>
				                <li><a href="javascript:unLocked(<%=SysUserList[i].UserId%>);"><div>解锁</div></a></li>
				                <%}else{ %>
				                <li><a href="javascript:locked(<%=SysUserList[i].UserId%>);"><div>锁定</div></a></li>
				                <%} %>
			                </ul>
		                </div>
	                </td>
	            </tr>
            <%} %>
            </tbody>
        </table>
        <%
            Response.Write(PageBarHtml);
        }
        else
        { %>
        <div class="noData"></div>
        <%} %>
        
    </div>
    
</div>

</body>
</html>
