window.onload=function(){
	document.getElementById("print").onclick=function(){
		window.print();
	}
	
	var dtList=document.body.getElementsByTagName("dt");
	bindEvent(dtList,"DD");
	
	function bindEvent(eList,subTagName){
		for(var i=0;i<eList.length;i++){
			eList[i].onclick=function(){
				if(this.nextSibling){
					var next=this.nextSibling;
					while(next.nodeType==3){
						next=next.nextSibling;
					}
					if(next.nodeName==subTagName){
						if(next.style.display==""||next.style.display=="block")
							next.style.display="none";
						else
							next.style.display="block";
					}
				}
			}
		}
	}
}