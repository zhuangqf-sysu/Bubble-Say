function sendMessage() {
	if(document.getElementById("inputbox").value=='') {
		alert("Please input your message!");	
	}
	else  {
		var usernum=document.getElementById("content").childNodes.length+1;
		var elem=document.createElement("li");
		var username=document.createElement("div");
		username.className="username";
		username.innerHTML=document.getElementById("username").value;
		var usermsg=document.createElement("div");
		usermsg.className="usermsg";
		usermsg.innerHTML=document.getElementById("inputbox").value;
		var arrow=document.createElement("div");
		arrow.className="arrow";
		elem.appendChild(username);
		elem.appendChild(usermsg);
		usermsg.appendChild(arrow);
		elem.id="usr"+usernum;
		elem.className="myself";
		elem.style.listStyle="none";
		elem.style.padding="0px";				
		elem.style.maxWidth="80%";
		elem.style.position="relative";
		elem.style.clear="both";
		elem.style.marginBottom="30px";	
		elem.style.textAlign="left";
		var nodes=elem.childNodes;	
		usermsg.style.margin="0px";
		usermsg.style.padding="20px";
		usermsg.style.wordBreak="break-all";
		usermsg.style.marginRight="80px";
		username.style.textAlign="right";
		if(document.getElementById("opinion").checked) { //alert("hello");
			arrow.style.borderTopColor="#373737";
			arrow.style.borderRightColor="#373737";
			arrow.style.borderBottomColor="#373737";
			arrow.style.borderLeftColor="#F8C301";
			usermsg.style.backgroundColor="#F8C301";
		}
		else if(document.getElementById("question").checked) {
			arrow.style.borderTopColor="#373737";
			arrow.style.borderRightColor="#373737";
			arrow.style.borderBottomColor="#373737";
			arrow.style.borderLeftColor="#EF4340";
			usermsg.style.backgroundColor="#EF4340";
		}
		else if(document.getElementById("answer").checked) {
			arrow.style.borderTopColor="#373737";
			arrow.style.borderRightColor="#373737";
			arrow.style.borderBottomColor="#373737";
			arrow.style.borderLeftColor="#6E3FCF";
			usermsg.style.backgroundColor="#6E3FCF";
		}
		else if(document.getElementById("chatting").checked){
			arrow.style.borderTopColor="#373737";
			arrow.style.borderRightColor="#373737";
			arrow.style.borderBottomColor="#373737";
			arrow.style.borderLeftColor="#A6D4F2";
			usermsg.style.backgroundColor="#A6D4F2";
		}
		else {
			arrow.style.borderTopColor="#373737";
			arrow.style.borderRightColor="#373737";
			arrow.style.borderBottomColor="#373737";
			arrow.style.borderLeftColor="#CCCCCC";
			usermsg.style.backgroundColor="#CCCCCC";
		}
		document.getElementById("content").appendChild(elem);
		var box1=document.getElementById("checkbox1");
		box1.style.backgroundPosition="0 0";
		box1=document.getElementById("opinion");
		box1.checked="";
		box1=document.getElementById("checkbox2");
		box1.style.backgroundPosition="0 0";
		box1=document.getElementById("question");
		box1.checked="";
		box1=document.getElementById("checkbox3");
		box1.style.backgroundPosition="0 0";
		box1=document.getElementById("answer");
		box1.checked="";
		box1=document.getElementById("checkbox4");
		box1.style.backgroundPosition="0 0";
		box1=document.getElementById("chatting");
		box1.checked="";
		setPos();
	}
	
}

