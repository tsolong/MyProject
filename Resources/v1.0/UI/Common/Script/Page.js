/****************************************
Name: Page.js
Author: TsoLong
Email: tsolong@126.com
WebSite: http://www.tsolong.com/
Create Date: 2009-03-17
Description:
	页面脚本
****************************************/
addEvent(window,"load",function(){
	addEvent($("otherCity"),"mouseover",function(){
		setAttr($("otherCity"), "class", "show");
	})
	addEvent($("otherCity"),"mouseout",function(){
		removeAttr($("otherCity"), "class");
	})
})