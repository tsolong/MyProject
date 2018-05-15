/****************************************
Name: Login.js
Author: TsoLong
Email: tsolong@126.com
WebSite: http://www.tsolong.com/
Create Date: 2008-12-10
Description:
	系统管理登录验证JS
****************************************/
if (self != top)
	top.location.href = location.href;

function checkLogin(){
	var UserName= $("UserName");
	var Password= $("Password");
	var loginForm= $("loginForm");
	disabledElement(loginForm,false);
	
	if (UserName.value == "") {
		new win({
			title: "系统提示",
			msg: "请输入您的用户名",
			closeEvent: function(){
				disabledElement(loginForm,true);
				UserName.focus();
			}
		})
	}
	else if (Password.value == "") {
		new win({
			title: "系统提示",
			msg: "请输入您的密码",
			closeEvent: function(){
				disabledElement(loginForm,true);
				Password.focus();
			}
		})
	}
	else {		
		new ajax({
			method: "post",
			url: "?action=checklogin",
			formElement: $("loginForm"),
			onLoading:function(){
				loading({
					content: "正在登录中请稍后..."
				})
			},
			onSuccess: function(o){
				var result = o.responseText;
				if (result != "10")
					loading.close();
				if (result == "1") {
					new win({
						type: 4,
						title: "系统提示 -- 登录失败",
						msg: "您输入的用户名或密码格式不正确，请输入正确的用户名和密码",
						closeEvent: function(){
							disabledElement(loginForm,true);
							UserName.focus();
							UserName.select();
						}
					})
				}
				else if (result == "4") {
					new win({
						type: 4,
						title: "系统提示 -- 登录失败",
						msg: "用户名或密码错误<br>初始用户名和密码都为tsolong",
						closeEvent: function(){
							disabledElement(loginForm,true);
							UserName.focus();
							UserName.select();
						}
					})
				}
				else if (result == "5") {
					new win({
						type: 4,
						title: "系统提示 -- 登录失败",
						msg: "抱歉，你的帐户已被管理员锁定，暂时不能登录",
						closeEvent: function(){
							disabledElement(loginForm,true);
							UserName.focus();
							UserName.select();
						}
					})
				}
				else if (result == "10") {
					setTimeout(function(){
						loading({
							content: "登录成功,页面自动跳转中,请稍等..."
						})
					}, 500)
					setTimeout(function(){
						location.href = "index.aspx";
					}, 1500)
				}
			},
			onError: function(){
				loading.close();
				new win({
					type: 4,
					title: "系统提示",
					msg: "请求服务器出错,请与网站管理员联系",
					closeEvent: function(){
						disabledElement(loginForm, true);
					}
				})
			}
		})
	}
	return false;
}

addEvent(window, "load", function(){
	var loginHtml = '<form id="loginForm" onsubmit="return checkLogin();"><div id="login"><div><label for="UserName">用户名</label><input type="text" id="UserName" name="UserName" class="txt"/></div><div><label for="Password">密　码</label><input type="password" id="Password" name="Password" class="txt"/></div><div class="last"><button type="submit">登　录</button><button type="reset">清　空</button></div></div></form>';
	
	var loginObj = new win({
		type: 6,
		width: 350,
		title: document.title,
		html: loginHtml,
		isOverlay: false,
		isTransition: false,
		isTopControl: false
	}).win
	
	var pos = TL.getWindowCenterPos(loginObj);
	new tween("elastic", "easeOut", pos[1], 0, 80, 10, function(v){
	//new tween("bounce", "easeOut", pos[1], 0, 80, 10, function(v){
		setStyle(loginObj, {
			left: pos[0] + "px",
			top: v + "px"
		})
	}, function(){
		$("UserName").focus();
		$("UserName").value = "tsolong";
		$("Password").value = "tsolong";
	});
})