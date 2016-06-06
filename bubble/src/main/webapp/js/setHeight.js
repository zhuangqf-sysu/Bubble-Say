function setHeight() {
	var v=document.body.offsetHeight*0.8;
	//var v=document.body.clientHeight;
	//var v=windows.innerHeight;
	//var v = document.documentElement.clientHeight;
    var screens = document.getElementById('screen');
    screens.style.minHeight = v+"px";
	//alert(v);
}