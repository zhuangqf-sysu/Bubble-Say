function setArrow(arrow) {
	if(document.getElementById("opinion").checked==checked) {
		arrow.style.borderTopColor="#373737";
		arrow.style.borderRightColor="#373737";
		arrow.style.borderBottomColor="#373737";
		arrow.style.borderLeftColor="#F8C301";
	}
	else if(document.getElementById("question").checked==checked) {
		arrow.style.borderTopColor="#373737";
		arrow.style.borderRightColor="#373737";
		arrow.style.borderBottomColor="#373737";
		arrow.style.borderLeftColor="#EF4340";
	}
	else if(document.getElementById("answer").checked==checked) {
		arrow.style.borderTopColor="#373737";
		arrow.style.borderRightColor="#373737";
		arrow.style.borderBottomColor="#373737";
		arrow.style.borderLeftColor="#6E3FCF";
	}
	else {
		arrow.style.borderTopColor="#373737";
		arrow.style.borderRightColor="#373737";
		arrow.style.borderBottomColor="#373737";
		arrow.style.borderLeftColor="#A6D4F2";
	}
	if(arrow.className=="myself") {
		arrow.style.position="absolute";		
		arrow.style.top="30px";
		arrow.style.right="-38px"; /* 圆角的位置需要细心调试哦 */
		arrow.style.width="0";
		arrow.style.height="0";
		arrow.style.fontSize="0";
		arrow.style.borderStyle="solid";
		arrow.style.borderWidth="20px";
		arrow.style.zIndex="-1";
	}
	else {
		arrow.style.position="absolute";		
		arrow.style.top="30px";
		arrow.style.left="-38px"; /* 圆角的位置需要细心调试哦 */
		arrow.style.width="0";
		arrow.style.height="0";
		arrow.style.fontSize="0";
		arrow.style.borderStyle="solid";
		arrow.style.borderWidth="20px";
		arrow.style.zIndex="-1";
	}
	
}

