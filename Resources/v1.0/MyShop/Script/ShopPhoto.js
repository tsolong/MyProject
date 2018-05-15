/****************************************
Name: ShopPhoto.js
Author: TsoLong
Email: tsolong@126.com
WebSite: http://www.tsolong.com/
Create Date: 2009-04-13
Description:
	店铺照片上传JS
****************************************/
function UploadFile(config){
	this.container = null;
	this.files = [];
	this.fileCount = 5;
	this.onFileChange = null;
	this.onFileCount = null;
	this.onSubmit = null;
	
	for (var par in config) {
		this[par] = config[par];
	}
	
	this.init();
}
UploadFile.prototype = {
	init: function(){
		var oThis = this;
		
		//iframe
		var uploadFileFrame;
		var frameName = "uploadFrame_" + Math.floor(Math.random() * 1000);
		try {//ie不能修改iframe的name
			uploadFileFrame = document.createElement("<iframe name=\"" + frameName + "\">");
		} 
		catch (e) {//ff
			uploadFileFrame = document.createElement("iframe");
			setAttr(uploadFileFrame, "name", frameName);
		}
		setAttr(uploadFileFrame, "id", frameName);
		setStyle(uploadFileFrame, {
			display: "none"
		});

		//上传表单
		var uploadFileForm
		try {//ie不能设置form的enctype
			uploadFileForm = document.createElement("<form enctype=\"multipart/form-data\">");
		} 
		catch (e) {//ff
			uploadFileForm = document.createElement("form");
			setAttr(uploadFileForm, "enctype", "multipart/form-data");
		}
		setAttr(uploadFileForm, "action", "?action=upload");
		setAttr(uploadFileForm, "target", frameName);
		setAttr(uploadFileForm, "method", "post");
		uploadFileForm.onsubmit = function(){
			return oThis.onSubmit(oThis.files);
		}
		
		//添加按钮
		var addFileBtn = document.createElement("input")
		setAttr(addFileBtn, "type", "button");
		setAttr(addFileBtn, "class", "btn");
		setAttr(addFileBtn, "value", "增 加");
		setStyle(addFileBtn, {
			margin: "0 20px 0 0"
		});
		addEvent(addFileBtn, "click", function(){
			oThis.addFile();
		})
		
		//上传按钮
		var uploadFileBtn = document.createElement("input")
		setAttr(uploadFileBtn, "type", "submit");
		setAttr(uploadFileBtn, "class", "btn");
		setAttr(uploadFileBtn, "value", "上 传");
		
		//文件列表区域
		this.fileList = document.createElement("div");
		
		this.container.appendChild(uploadFileFrame);
		this.container.appendChild(uploadFileForm);
		uploadFileForm.appendChild(addFileBtn);
		uploadFileForm.appendChild(uploadFileBtn);
		uploadFileForm.appendChild(this.fileList);

		//初始化时默认添加一个上传控件
		this.addFile();
	},
	addFile: function(){
		var oThis = this;
		if (this.files.length < this.fileCount) {
			var div = document.createElement("div");
			setStyle(div, {
				margin: "10px 20px 0 0"
			});
			
			var file = document.createElement("input");
			setAttr(file, "name", "FileName");
			setAttr(file, "type", "file");
			setAttr(file, "size", "35");
			setStyle(file, {
				margin: "0 15px 0 0"
			});
			if (this.onFileChange) {
				addEvent(file, "change", function(){
					oThis.onFileChange(file);
				})
			}
			
			var cancelFileBtn = document.createElement("a");
			setAttr(cancelFileBtn, "href", "javascript:void(0);");
			cancelFileBtn.innerHTML = "取消";
			addEvent(cancelFileBtn, "click", function(){
				oThis.cancelFile(cancelFileBtn);
			})
			
			div.appendChild(file);
			div.appendChild(cancelFileBtn);
			this.fileList.appendChild(div);
			this.files.push(file);
		}
		else {
			if (this.onFileCount) 
				this.onFileCount(this.fileCount);
		}
	},
	cancelFile: function(cancelFileBtn){
		var file = cancelFileBtn.previousSibling;
		for (var i = 0, n = 0; i < this.files.length; i++) {
			if (this.files[i] != file) 
				this.files[n++] = this.files[i];
		}
		if (this.files.length > 0) 
			this.files.length -= 1;
		file.parentNode.removeChild(file);
		cancelFileBtn.parentNode.removeChild(cancelFileBtn);
	}
}

function checkFileExt(extName){
	var extNames = [".gif", ".jpeg", ".jpg", ".png"];
	for (var i = 0; i < extNames.length; i++) {
		if (extName.toLowerCase() == extNames[i].toLowerCase()) {
			return true;
		}
	}
	return false;
}

var winUploadShopPhoto;
function showUploadShopPhoto(){
	if (parseInt($("_CurrentTotalPhoto").value) >= parseInt($("_TotalPhoto").value)) {
		new win({
			type: 4,
			title: "系统提示",
			msg: "当前您的店铺照片总数为 " + $("_CurrentTotalPhoto").value + " 张，总共只能上传 " + $("_TotalPhoto").value + " 张，因此不能再上传了。"
		})
		return;
	}
	
	winUploadShopPhoto = new win({
		type: 6,
		height: 200,
		title: "上传店铺照片",
		html: ""
	})
	setStyle(winUploadShopPhoto.win.middle, {
		padding: "10px"
	});
	
	new UploadFile({
		container: winUploadShopPhoto.win.middle,
		fileCount: $("_UploadShopPhotoBatchSize").value,
		onFileChange: function(file){
		},
		onFileCount: function(fileCount){
			new win({
				title: "系统提示",
				msg: "最多可以同时上传 " + fileCount + " 张店铺照片"
			});
		},
		onSubmit: function(files){
			if (files.length == 0) {
				new win({
					title: "系统提示",
					msg: "没有可上传的店铺照片"
				});
				return false;
			}
			for (var i = 0; i < files.length; i++) {
				if (files[i].value == "") {
					new win({
						title: "系统提示",
						msg: "您还未选择第 " + (i + 1) + " 张店铺照片"
					});
					return false;
				}
				if (!checkFileExt(files[i].value.substr(files[i].value.lastIndexOf("."), files[i].value.length - 1).toLowerCase())) {
					new win({
						type: 4,
						title: "系统提示",
						msg: "第 " + (i + 1) + " 张店铺照片文件格式不正确"
					});
					return false;
				}
			}
			loading({
				content: "店铺照片正在上传中，请不要关闭浏览器 ..."
			})
			return true;
		}
	})
}

function uploadEnd(result){
	loading.close();
	var result = JSON.parse(result);
	new win({
		type: result.type ? 3 : 4,
		title: "系统提示",
		msg: result.msg,
		closeEvent: function(){
			if (result.type) 
				location.reload();
		}
	})
}

function del(id){
	if (!id || id == "") {
		new win({
			type: 1,
			title: "系统提示",
			msg: "请选择要删除的照片"
		})
	}
	else {
		new win({
			type: 5,
			title: "系统提示",
			msg: "删除店铺照片将无法恢复,你确定要删除吗?",
			confirmEvent: function(){
				location.href = "?action=del&id=" + id;
			}
		})
	}
}

function delAll(){
	if ($("_CurrentTotalPhoto").value == "0") {
		new win({
			type: 1,
			title: "系统提示",
			msg: "您还没有上传店铺照片"
		})
	}
	else {
		new win({
			type: 5,
			title: "系统提示",
			msg: "删除全部店铺照片将无法恢复,你确定要删除全部吗?",
			confirmEvent: function(){
				location.href = "?action=delall";
			}
		})
	}
}