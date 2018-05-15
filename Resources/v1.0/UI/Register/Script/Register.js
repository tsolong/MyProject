/****************************************
Name: Register.js
Author: TsoLong
Email: tsolong@126.com
WebSite: http://www.tsolong.com/
Create Date: 2009-02-19
Description:
	用户注册JS
****************************************/
/****************************************
Description:
	注册友好提示效果
	1.不可多个效果一起呈现，后者覆盖前者
Parameter:
	config{
		el: 应用此效果的元素
		width: 宽度（可选参数，默认值200）
		offsetPos: 在原始传置进行偏移[left,right]（可选参数，默认参数[0,0]）
	}
Example:
	tip({
		el: el,
		width: 300,
		offsetPos: [10, -5]
	});
Create Date: 2009-02-22
****************************************/
var tip = function(config){
	tip.show(config);
}
tip.show = function(config){
	config.el = typeof(config.el) == "string" ? $(config.el) : config.el;
	if (!config.el) {
		return
	}
	config.width = config.width || 200;
	config.offsetPos = config.offsetPos || [0, 0];
	if ($("tip")) {
		tip.close();
	}
	var tip = document.createElement("div");
	setAttr(tip, "id", "tip");
	var objPos = getPos(config.el);
	setStyle(tip, {
		width: config.width + "px",
		left: objPos[0] + config.el.offsetWidth + config.offsetPos[0] + "px",
		top: objPos[1] - 1 + config.offsetPos[1] + "px"
	});
	tip.innerHTML = "<div class=\"top\"><div class=\"info\">" + getAttr(config.el, "tip") + "</div></div><div class=\"bottom\"><span></span></div>";
	document.getElementsByTagName("body")[0].appendChild(tip);
}
tip.close = function(){
	var tip = $("tip");
	if (tip) 
		tip.parentNode.removeChild(tip);
}

//获取消息元素
var getMsgElement = function(el){
	var msgElement;
	if (TL.browser.msie || TL.browser.opera) {
		msgElement = el.parentNode.nextSibling;
	}
	else {
		msgElement = el.parentNode.nextSibling.nextSibling;
	}
	return msgElement;
}

//文本框事件
var inputEvent = function(){
	var inputText = $T("register", "input");
	for (var i = 0; i < inputText.length; i++) {
		var el = inputText[i];
		if (el.type == "text" || el.type == "password") {
			changeStyle(el);
		}
	}
	function changeStyle(el){
		addEvent(el, "focus", function(){
			tip({
				el: el,
				width: 290,
				offsetPos: [10, -5]
			});
			setAttr(el, "class", "txt hover");
		})
		addEvent(el, "blur", function(){
			tip.close();
			setAttr(el, "class", "txt");
		})
	}
}

//更换验证码图片事件
var changeValidateImage = function(){
	var ValidateImage = $("ValidateImage");
	addEvent(ValidateImage, "click", function(){
		setAttr(ValidateImage, "src", "ValidateImage.aspx?num=" + new Date().getTime());
		$("ValidateCode").value="";
		$("ValidateCode").focus();
	})
}

//检查用户名
var lastUserName = "";
var flagUserName = false;
var checkUserName = function(){
	var UserName = $("UserName");
	var msgElement = getMsgElement(UserName);
	if (UserName.value.toLowerCase() != "") {
		if (lastUserName == UserName.value.toLowerCase()) {
			return;
		}
		else {
			lastUserName = UserName.value.toLowerCase();
			flagUserName = false;
			
			var sReg = /^[a-zA-Z0-9_]{4,16}$/i;
			if (sReg.test(UserName.value.toLowerCase())) {
				var url = location.href.indexOf("type=shop") != -1 ? "type=shop&" : ""
				new ajax({
					url: "?" + url + "action=checkusernameisexist&username=" + UserName.value.toLowerCase(),
					onLoading: function(){
						setAttr(UserName, "disabled", "disabled");
						msgElement.innerHTML = "<div class=\"loading\"><div>";
					},
					onSuccess: function(o){
						setTimeout(function(){
							removeAttr(UserName, "disabled");
							if (o.responseText == "true") {
								msgElement.innerHTML = "<div class=\"error\">此用户名已被使用，换个其它的吧<div>";
								flagUserName = false;
							}
							else 
								if (o.responseText == "false") {
									msgElement.innerHTML = "<div class=\"right\"><div>";
									flagUserName = true;
								}
						}, 2000)
					}
				})
			}
			else {
				msgElement.innerHTML = "<div class=\"error\">用户名格式错误<div>";
				flagUserName = false;
			}
		}
		/*var len=UserName.value.replace(/[^\x00-\xff]/g,"**").length;//一个汉字算两位
		 var reg = /[^\u4e00-\u9fa5A-Za-z0-9_]/g;//汉字，英文大小写字母，数字，下划线*/
	}
	else {
		msgElement.innerHTML = "<div class=\"error\">此项必填<div>";
		flagUserName = false;
	}
}

//检查密码
var flagPassword = false;
var checkPassword = function(){
	var Password = $("Password");
	var msgElement = getMsgElement(Password);
	if (Password.value.toLowerCase() != "") {
		var sReg = /^[a-zA-Z0-9_]{4,16}$/i;
		if (sReg.test(Password.value.toLowerCase())) {
			msgElement.innerHTML = "<div class=\"right\"><div>";
			flagPassword = true;
		}
		else {
			msgElement.innerHTML = "<div class=\"error\">密码格式错误<div>";
			flagPassword = false;
		}
	}
	else {
		msgElement.innerHTML = "<div class=\"error\">此项必填<div>";
		flagPassword = false;
	}
}

//检查确认密码
var flagConfirmPassword = false;
var checkConfirmPassword = function(){
	var ConfirmPassword = $("ConfirmPassword");
	var msgElement = getMsgElement(ConfirmPassword);
	if (ConfirmPassword.value.toLowerCase() != "") {
		var sReg = /^[a-zA-Z0-9_]{4,16}$/i;
		if (sReg.test(ConfirmPassword.value.toLowerCase())) {
			if (ConfirmPassword.value.toLowerCase() != $("Password").value.toLowerCase()) {
				msgElement.innerHTML = "<div class=\"error\">两次密码填写不一致<div>";
				flagConfirmPassword = false;
			}
			else {
				msgElement.innerHTML = "<div class=\"right\"><div>";
				flagConfirmPassword = true;
			}
		}
		else {
			msgElement.innerHTML = "<div class=\"error\">确认密码格式错误<div>";
			flagConfirmPassword = false;
		}
	}
	else {
		msgElement.innerHTML = "<div class=\"error\">此项必填<div>";
		flagConfirmPassword = false;
	}
}

//检查邮箱
var lastEmail = "";
var flagEmail = false;
var checkEmail = function(){
	var Email = $("Email");
	var msgElement = getMsgElement(Email);
	if (Email.value.toLowerCase() != "") {
		if (lastEmail == Email.value.toLowerCase()) {
			return;
		}
		else {
			lastEmail = Email.value.toLowerCase();
			flagEmail = false;
			
			var sReg = /^[\w\.-]+@[\w\.-]+\.\w+$/i;
			if (sReg.test(Email.value.toLowerCase())) {
				var url = location.href.indexOf("type=shop") != -1 ? "type=shop&" : ""
				new ajax({
					url: "?" + url + "action=checkemailisexist&email=" + Email.value.toLowerCase(),
					onLoading: function(){
						setAttr(Email, "disabled", "disabled");
						msgElement.innerHTML = "<div class=\"loading\"><div>";
					},
					onSuccess: function(o){
						setTimeout(function(){
							removeAttr(Email, "disabled");
							if (o.responseText == "true") {
								msgElement.innerHTML = "<div class=\"error\">此邮箱已被使用，换个其它的吧<div>";
								flagEmail = false;
							}
							else 
								if (o.responseText == "false") {
									msgElement.innerHTML = "<div class=\"right\"><div>";
									flagEmail = true;
								}
						}, 2000)
					}
				})
			}
			else {
				msgElement.innerHTML = "<div class=\"error\">邮箱地址无效<div>";
				flagEmail = false;
			}
		}
	}
	else {
		msgElement.innerHTML = "<div class=\"error\">此项必填<div>";
		flagEmail = false;
	}
}

//检查验证码
var lastValidateCode = "";
var flagValidateCode = false;
var checkValidateCode = function(){
	var ValidateCode = $("ValidateCode");
	var msgElement = getMsgElement(ValidateCode);
	if (ValidateCode.value != "") {
		if (lastValidateCode == ValidateCode.value) {
			return;
		}
		else {
			lastValidateCode = ValidateCode.value;
			flagValidateCode = false;
			
			var sReg = /^[0-9]{4,4}$/i;
			if (sReg.test(ValidateCode.value)) {
				new ajax({
					url: "?action=checkvalidatecode&code=" + ValidateCode.value,
					onLoading: function(){
						setAttr(ValidateCode, "disabled", "disabled");
						msgElement.innerHTML = "<div class=\"loading\"><div>";
					},
					onSuccess: function(o){
						setTimeout(function(){
							removeAttr(ValidateCode, "disabled");
							if (o.responseText == "true") {
								msgElement.innerHTML = "<div class=\"right\"><div>";
								flagValidateCode = true;
							}
							else 
								if (o.responseText == "false") {
									msgElement.innerHTML = "<div class=\"error\">验证码填写错误<div>";
									flagValidateCode = false;
								}
						}, 2000)
					}
				})
			}
			else {
				msgElement.innerHTML = "<div class=\"error\">验证码只能是4位数字<div>";
				flagValidateCode = false;
			}
		}
	}
	else {
		msgElement.innerHTML = "<div class=\"error\">此项必填<div>";
		flagValidateCode = false;
	}
}

//绑定输入检查事件
var bindCheckEvent = function(){
	//检查用户名事件
	addEvent("UserName", "blur", checkUserName);
	//检查密码事件
	addEvent("Password", "blur", checkPassword);
	//检查确认密码事件
	addEvent("ConfirmPassword", "blur", checkConfirmPassword);
	//检查邮箱事件
	addEvent("Email", "blur", checkEmail);
	//检查验证码事件
	addEvent("ValidateCode", "blur", checkValidateCode);
}

//提交前进行检查
var checkRegister = function(){
	//检查用户名
	checkUserName();
	//检查密码
	checkPassword();
	//检查确认密码
	checkConfirmPassword();
	//检查邮箱
	checkEmail();
	//检查验证码
	checkValidateCode();
	
	if (flagUserName && flagPassword && flagConfirmPassword && flagEmail && flagValidateCode) {
		return true;
	}
	else {
		return false;
	}
}