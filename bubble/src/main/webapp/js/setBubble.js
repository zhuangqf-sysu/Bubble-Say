function setBubble(bubble) {
	bubble.style.listStyle="none";
	margin-left:-30px;
	if(arrow.className=="myself") {
		arrow.style.position:absolute;		
		arrow.style.top:30px;
		arrow.style.right:-38px; /* 圆角的位置需要细心调试哦 */
		arrow.style.width:0;
		arrow.style.height:0;
		arrow.style.font-size:0;
		arrow.style.border:solid 20px;
		arrow.style.z-index:-1;
		if(document.getElementById("opinion").checked==checked) {
			arrow.style.border-color:#373737 #373737 #373737 #F8C301;
		}
		else if(document.getElementById("question").checked==checked) {
			arrow.style.border-color:#373737 #373737 #373737 #EF4340;
		}
		else if(document.getElementById("answer").checked==checked) {
			arrow.style.border-color:#373737 #373737 #373737 #6E3FCF;
		}
		else {
			arrow.style.border-color:#373737 #373737 #373737 #A6D4F2;
		}
	}
	else {
		arrow.style.position:absolute;		
		arrow.style.top:30px;
		arrow.style.left:-38px; /* 圆角的位置需要细心调试哦 */
		arrow.style.width:0;
		arrow.style.height:0;
		arrow.style.font-size:0;
		arrow.style.border:solid 20px;
		arrow.style.z-index:-1;
		if(document.getElementById("opinion").checked==checked) {
			arrow.style.border-color:#373737 #F8C301 #373737 #373737;
		}
		else if(document.getElementById("question").checked==checked) {
			arrow.style.border-color:#373737 #EF4340 #373737 #373737;
		}
		else if(document.getElementById("answer").checked==checked) {
			arrow.style.border-color:#373737 #6E3FCF #373737 #373737;
		}
		else {
			arrow.style.border-color:#373737 #A6D4F2 #373737 #373737;
		}
	}
	
}

