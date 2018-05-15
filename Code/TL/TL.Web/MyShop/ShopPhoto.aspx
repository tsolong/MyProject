<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShopPhoto.aspx.cs" Inherits="TL.Web.MyShop.ShopPhoto" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Content-Language" content="zh-CN"/>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title></title>
    <link type="text/css" rel="stylesheet" href="<%=ResourcesUrl %>Common/TL/TL-Base.css" />
    <link type="text/css" rel="stylesheet" href="<%=ResourcesUrl %>Common/TL/TL-More.css" />
    <link type="text/css" rel="stylesheet" href="<%=ResourcesUrl %>MyShop/Style/Page.css" />
    <script type="text/javascript" src="<%=ResourcesUrl %>Common/TL/TL-Core.js"></script>
    <script type="text/javascript" src="<%=ResourcesUrl %>Common/TL/TL-More.js"></script>
    <script type="text/javascript" src="<%=ResourcesUrl %>MyShop/Script/Common.js"></script>
    <script type="text/javascript" src="<%=ResourcesUrl %>MyShop/Script/ShopPhoto.js"></script>
    <style  type="text/css">
    #shopPhotoList{
        width:100%;
    }
    #shopPhotoList li{
        margin:0 20px 20px 0;
        float:left;
        _width:110px;
        _display:inline;
        padding:10px;
        border:1px solid #ccc;
    }
    #shopPhotoList li .photo{
        overflow:hidden;
        width:110px;
        height:110px;
        text-align:center;
    }
    #shopPhotoList li .photo a img{
        padding:4px;
        border:1px solid #888;
        filter:alpha(opacity=50);
        opacity:0.5;
    }
    #shopPhotoList li .photo a:hover img{
        background:#f1f3ac;
        filter:alpha(opacity=100);
        opacity:1;
    }
    #shopPhotoList li ul li{
        _display:block;
        margin:5px 0 0 0;
        float:none;
        padding:0;
        border:none;
    }
    .txtarea{
        width:100px;
        height:50px;
    }
    .txt{
        width:100px;
    }
    </style>
</head>
<body>

<div class="panel">

    <div class="toolBar">
        <div class="currentMenu">店铺照片</div>
        <a href="javascript:location.reload();"><b><span class="refresh">刷新</span></b></a>
        <a href="javascript:history.go(-1);"><b><span class="back">返回</span></b></a>
        <a href="javascript:showUploadShopPhoto();"><b><span class="pic">上传照片</span></b></a>
        <a href="javascript:delAll();"><b><span class="del">删除全部</span></b></a>
    </div>

    <input type="hidden" id="_CurrentTotalPhoto" value="<%=CurrentTotalPhoto %>" />
    <input type="hidden" id="_TotalPhoto" value="<%=TotalPhoto %>" />
    <input type="hidden" id="_UploadShopPhotoBatchSize" value="<%=TL.Config.SysConfig.GetConfigValue("UploadShopPhotoBatchSize") %>" />

    <div class="content">
        
        <div class="pageMsg msgHint">
            <ul>
                <div>提示：</div>
                <li>当前您的店铺照片为 <%=CurrentTotalPhoto %> 张</li>
                <li>总共只能上传 <%=TotalPhoto %> 张</li>
                <li>最多可以同时上传 <%=TL.Config.SysConfig.GetConfigValue("UploadShopPhotoBatchSize")%> 张店铺照片</li>
                <li>单张店铺照片容量只能在 <%=UploadShopPhotoMinSize / 1024 %>kb - <%=UploadShopPhotoMaxSize / 1024 %>kb 之间</li>
                <li>只能上传 <%=TL.Config.SysConfig.GetConfigValue("UploadShopPhotoExt") %> 格式的文件</li>
                <li>描述内容最长50字，排序只允许数字</li>
            </ul>
        </div>
        
        <%if (ShopPhotoList.Count > 0)
          {%>
        <form action="?action=save" method="post">
        
            <ul id="shopPhotoList">
                <%for(int i=0;i<ShopPhotoList.Count;i++){ %>
                <li>
                    <div class="photo">
                        <a href="<%=CurrentCity.PicturesUrl+ShopPhotoList[i].Url+"600x600"+ShopPhotoList[i].Ext %>" target="_blank">
                        <img src="<%=CurrentCity.PicturesUrl+ShopPhotoList[i].Url+"100x100"+ShopPhotoList[i].Ext %>" title="<%=ShopPhotoList[i].Description %>" /></a>
                    </div>
                    <ul>
                        <li>描述：</li>
                        <li><textarea name="Description<%=ShopPhotoList[i].Id %>" class="txtarea"><%=ShopPhotoList[i].Description%></textarea></li>
                        <li>排序：</li>
                        <li><input name="OrderNum<%=ShopPhotoList[i].Id %>" type="text" class="txt" value="<%=ShopPhotoList[i].OrderNum %>" /></li>
                        <li><button type="button" onclick="del(<%=ShopPhotoList[i].Id %>);">删除</button></li>
                    </ul>
                </li>
                <%} %>
                <div class="clear"></div>
            </ul>
            
            <div class="formBtn"><button type="submit">保 存</button></div>
            
        </form>
        <%} %>
    </div>
    
</div>

</body>
</html>
