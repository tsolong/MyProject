/****************************************
Name: ShopEdit.js
Author: TsoLong
Email: tsolong@126.com
WebSite: http://www.tsolong.com/
Create Date: 2009-03-30
Description:
	店铺信息设置JS
****************************************/
function showMarkMap(){
	var LTMapWindow = new win({
		type: 6,
		width: 600,
		height: 400,
		title: $("ShopName").value != "" ? "您可以（查看/重新标注）您店铺的地址 - " + $("_CityName").value + "地图" : "请点击地图上的标注按钮标注您店铺的地址 - " + $("_CityName").value + "地图",
		//html: ""
		html: "<div id=\"mapDiv\" style=\"width:100%; height:100%;\"></div>"
	})
	
	//这种方法在关闭对话框时会报脚本错误并且会超出对话框的宽度
	//LTMapWindow.win.middle.style.width = "100%";//修复IE关闭win对话框报js错误
	//var map51 = new LTMaps(LTMapWindow.win.middle);
	
	//创建地图
	var map51 = new LTMaps($("mapDiv"));
	
	//已标注过则加载标注的点
	if ($("MarkAddress").value != "") {
		var pointStr = $("MarkAddress").value.split(",");
		var point = new LTPoint(pointStr[0], pointStr[1])
		
		map51.centerAndZoom(point, 0);
		map51.addOverLay(new LTMarker(point));
		
		var infoWin = new LTInfoWindow(point, [5, -20]);
		infoWin.setLabel("这里就是您店铺的位置");
		if($("ShopName").value != ""){
			infoWin.setTitle($("ShopName").value);
		}
		else{
			infoWin.setTitle("您还未填写店铺的名称，填写后将在这里显示");
			infoWin.setWidth(300);
		}
		map51.addOverLay(infoWin);
	}
	//未标注过则加载对应城市的地图
	else {
		map51.cityNameAndZoom($("_CityEName").value, 5);//根据城市名称和缩放级别来初始化
	}
	
	map51.addControl(new LTStandMapControl());//添加标准缩放控件
	LTEvent.bind(map51, "dblclick", map51, function(){this.zoomIn()})//绑定双击鼠标放大
	map51.handleMouseScroll();//启用鼠标滚轮缩放
	
	//标注控件
	var icon = new LTIcon("http://api.51ditu.com/img/ezmarker/tack.gif", [78, 39], [39, 39]);
	var markControl = new LTMarkControl(icon);
	map51.addControl(markControl);
	LTEvent.addListener(markControl, "mouseup", function(point){
		new win({
			type: 5,
			title: "系统提示",
			msg: "标注成功，是否保存",
			confirmEvent: function(){
				//getLongitude经度
				//getLatitude纬度
				$("MarkAddress").value = point.getLongitude() + "," + point.getLatitude();
				$("markBtn").innerHTML = "查看 / 重标"
				LTMapWindow.close();
			},
			closeEvent: function(){
			}
		})
	})
}

function getFoodSeriesValue(){
	var FoodSeries = $("FoodSeries");
	var FoodSeriesSub = $("FoodSeriesSub");
	var selMyFoodSeriesSub = $("selMyFoodSeriesSub");
	//FoodSeries.value = "";
	//FoodSeriesSub.value = "";
	
	var str1 = str2 = "";
	for (var i = 0; i < selMyFoodSeriesSub.options.length; i++) {
		var currentOption = selMyFoodSeriesSub.options[i];
		var optionValue = currentOption.value.split(",");
		//if (str1.indexOf("{" + optionValue[0] + "}") == -1) {
			str1 += str1 == "" ? "{" + optionValue[0] + "}" : ",{" + optionValue[0] + "}";
		//}
		str2 += str2 == "" ? "{" + optionValue[1] + "}" : ",{" + optionValue[1] + "}";
	}
	
	if (str1 != "" && str1 != FoodSeries.value) 
		FoodSeries.value = str1
	if (str2 != "" && str2 != FoodSeriesSub.value) 
		FoodSeriesSub.value = str2;
}

function checkRadio(formId, elName){
	var form = $(formId);
	for (var i = 0; i < form.length; i++) {
		var el = form[i];
		if (el.type == "radio" && el.name==elName && el.checked) {
			return true;
		}
	}
	return false;
}

function checkEdit(){
	getFoodSeriesValue();
	
	var Area = $("Area");
	var AreaSub = $("AreaSub");
	var FoodSeries = $("FoodSeries");
	var FoodSeriesSub = $("FoodSeriesSub");
	var ShopName = $("ShopName");
	var Address = $("Address");
	var MarkAddress = $("MarkAddress");
	var Route = $("Route");
	var Phone = $("Phone");
	var MobilePhone = $("MobilePhone");
	var Email = $("Email");
	var Consume = $("Consume");
	var Level = $("Level");
	var Intro = $("Intro");
	
	if (Area.value == "") {
		new win({
			title: "系统提示",
			msg: "请选择店铺所在地区",
			closeEvent: function(){
				Area.focus();
			}
		})
		return false;
	}
	else if (AreaSub.value == "") {
		new win({
			title: "系统提示",
			msg: "请选择店铺所在地点",
			closeEvent: function(){
				AreaSub.focus();
			}
		})
		return false;
	}
	else if (FoodSeries.value == "" || FoodSeriesSub.value == "") {
		new win({
			title: "系统提示",
			msg: "请选择店铺所经营的菜系,最多只能选择4个。",
			closeEvent: function(){
				$("selFoodSeries").focus();
			}
		})
		return false;
	}
	else if (FoodSeries.value.split(",").length > 4 || FoodSeriesSub.value.split(",").length > 4) {
		new win({
			title: "系统提示",
			msg: "店铺所经营的菜系最多只能选择4个。",
			closeEvent: function(){
				$("selFoodSeries").focus();
			}
		})
		return false;
	}
	else if (ShopName.value == "") {
		new win({
			title: "系统提示",
			msg: "请填写店铺的真实名称",
			closeEvent: function(){
				ShopName.focus();
			}
		})
		return false;
	}
	else if (Address.value == "") {
		new win({
			title: "系统提示",
			msg: "请填写店铺的详细地址",
			closeEvent: function(){
				Address.focus();
			}
		})
		return false;
	}
	else if (MarkAddress.value == "") {
		new win({
			title: "系统提示",
			msg: "请在地图上标注店铺所在位置",
			closeEvent: function(){
				showMarkMap();
			}
		})
		return false;
	}
	else if (Route.value == "") {
		new win({
			title: "系统提示",
			msg: "请填写交通路线",
			closeEvent: function(){
				Route.focus();
			}
		})
		return false;
	}
	else if (Phone.value == "") {
		new win({
			title: "系统提示",
			msg: "请填写联系电话号码",
			closeEvent: function(){
				Phone.focus();
			}
		})
		return false;
	}
	else if (MobilePhone.value == "") {
		new win({
			title: "系统提示",
			msg: "请填联系手机号码",
			closeEvent: function(){
				MobilePhone.focus();
			}
		})
		return false;
	}
	else if (Email.value == "") {
		new win({
			title: "系统提示",
			msg: "请填写邮箱",
			closeEvent: function(){
				Email.focus();
			}
		})
		return false;
	}
	else if (!/^[\w\.-]+@[\w\.-]+\.\w+$/i.test(Email.value.toLowerCase())) {
		new win({
			type: 4,
			title: "系统提示",
			msg: "邮箱地址无效",
			closeEvent: function(){
				Email.focus();
			}
		})
		return false;
	}
	else if (Consume.value == "") {
		new win({
			title: "系统提示",
			msg: "请选择人均消费",
			closeEvent: function(){
				Consume.focus();
			}
		})
		return false;
	}
	else if (Level.value == "") {
		new win({
			title: "系统提示",
			msg: "请选择店铺星级",
			closeEvent: function(){
				Level.focus();
			}
		})
		return false;
	}
	else if(!checkRadio("shopEditForm","Balcony")){
		new win({
			title: "系统提示",
			msg: "请选择是否有包厢"
		})
		return false;
	}
	else if(!checkRadio("shopEditForm","Takeaway")){
		new win({
			title: "系统提示",
			msg: "请选择是否有外卖"
		})
		return false;
	}
	else if(!checkRadio("shopEditForm","Card")){
		new win({
			title: "系统提示",
			msg: "请选择是否可以刷卡消费"
		})
		return false;
	}
	else if(!checkRadio("shopEditForm","Park")){
		new win({
			title: "系统提示",
			msg: "请选择是否有停车场"
		})
		return false;
	}
	else if (Intro.value == "") {
		new win({
			title: "系统提示",
			msg: "请对您的店铺做下简单的介绍（50-500字)",
			closeEvent: function(){
				Intro.focus();
			}
		})
		return false;
	}
	else if (Intro.value.length < 50 || Intro.value.length > 500) {
		new win({
			title: "系统提示",
			msg: "店铺介绍字数不正确,应该是50-500字。",
			closeEvent: function(){
				Intro.focus();
			}
		})
		return false;
	}
}

function getAreaSub(callBack){
	areaId = this.value;
	var AreaSub = $("AreaSub");
	AreaSub.options.length = 0;
	if (!areaId || areaId == "") {
		AreaSub.style.display = "none";
	}
	else {
		new ajax({
			url: "shopeditajax.aspx?action=getareasub&areaid=" + areaId,
			onSuccess: function(o){
				var result = o.responseText;
				if (result != "") {
					result = JSON.parse(o.responseText);
					AreaSub.options[AreaSub.options.length] = new Option("请选择", "");
					for (var i = 0; i < result.length; i++) {
						AreaSub.options[AreaSub.options.length] = new Option(result[i].Name, result[i].Id);
					}
					AreaSub.style.display = "inline";
				}
				if (callBack) 
					callBack();
			}
		})
	}
}

function getFoodSeriesSub(){
	var selFoodSeriesId = this.value;
	var selFoodSeriesSub = $("selFoodSeriesSub");
	selFoodSeriesSub.options.length = 0;
	if (!selFoodSeriesId || selFoodSeriesId == "") {
		$("FoodSeriesSubPanel").style.display = "none";
		$("selMyFoodSeriesSub").options.length = 0;
	}
	else {
		new ajax({
			url: "shopeditajax.aspx?action=getfoodseriessub&foodseriesid=" + selFoodSeriesId,
			onSuccess: function(o){
				var result = o.responseText;
				if (result != "") {
					result = JSON.parse(o.responseText);
					for (var i = 0; i < result.length; i++) {
						selFoodSeriesSub.options[selFoodSeriesSub.options.length] = new Option(result[i].Name, result[i].Id);
					}
					$("FoodSeriesSubPanel").style.display = "block";
				}
			}
		})
	}
}

function addFoodSeries(){
	var selFoodSeries = $("selFoodSeries");
	var selFoodSeriesSub = $("selFoodSeriesSub");
	var selMyFoodSeriesSub = $("selMyFoodSeriesSub");
	if (selFoodSeriesSub.value != "") {
		if (selMyFoodSeriesSub.options.length < 4) {
			var selOption = selFoodSeries.options[selFoodSeries.selectedIndex];
			var selOptionSub = selFoodSeriesSub.options[selFoodSeriesSub.selectedIndex];
			var flag = false;
			for (var i = 0; i < selMyFoodSeriesSub.options.length; i++) {
				if (selMyFoodSeriesSub.options[i].value == selOption.value + "," + selOptionSub.value) {
					flag = true;
					selMyFoodSeriesSub.options[i].selected = true;
				}
				else {
					selMyFoodSeriesSub.options[i].selected = false;
				}
			}
			if (!flag) {
				selMyFoodSeriesSub.options[selMyFoodSeriesSub.options.length] = new Option(selOption.innerHTML + "->" + selOptionSub.innerHTML, selOption.value + "," + selOptionSub.value);
				selMyFoodSeriesSub.options[selMyFoodSeriesSub.options.length - 1].selected = true;
			}
			else {
				new win({
					title: "系统提示",
					msg: "您已经选过 “" + selOption.innerHTML + "->" + selOptionSub.innerHTML + "” 菜系，不能重复选择"
				})
			}
		}
		else {
			new win({
				title: "系统提示",
				msg: "最多只能选择4个菜系"
			})
		}
	}
	else {
		new win({
			title: "系统提示",
			msg: "请先选择您要添加的菜系"
		})
	}
}

function delFoodSeries(){
	var selMyFoodSeriesSub = $("selMyFoodSeriesSub");
	if (selMyFoodSeriesSub.options.length > 0) {
		if (selMyFoodSeriesSub.value != "") {
			var opNextIndex = selMyFoodSeriesSub.selectedIndex;
			var selOption = selMyFoodSeriesSub.options[selMyFoodSeriesSub.selectedIndex];
			selOption.parentNode.removeChild(selOption);
			if (selMyFoodSeriesSub.options.length > 0) {
				if (opNextIndex < selMyFoodSeriesSub.options.length) 
					selMyFoodSeriesSub.options[opNextIndex].selected = true
				else 
					selMyFoodSeriesSub.options[selMyFoodSeriesSub.options.length - 1].selected = true
			}
		}
		else {
			new win({
				title: "系统提示",
				msg: "请先选择要删除的菜系"
			})
		}
		
	}
	else {
		new win({
			title: "系统提示",
			msg: "您还未选择菜系"
		})
	}
}

addEvent(window,"load",function(){
	addEvent("Area", "change", function(){
		getAreaSub.call($("Area"));
	})
	addEvent("selFoodSeries", "change", function(){
		getFoodSeriesSub.call($("selFoodSeries"));
	})
	addEvent("addFoodSeriesBtn", "click", function(){
		addFoodSeries();
	})
	addEvent("delFoodSeriesBtn", "click", function(){
		delFoodSeries();
	})
})