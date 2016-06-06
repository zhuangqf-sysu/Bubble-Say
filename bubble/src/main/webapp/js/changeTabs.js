function changeTabs() {
	var tab1 = document.getElementById('all');
	var tab2 = document.getElementById('mine');
	var clicktag=event.srcElement;
	if(clicktag.id == "all" && tab1.src != "../images/chatroom/all.png") {
		tab1.src="../images/chatroom/all.png";
		tab2.src="../images/chatroom/mine1.png";
		var users=getClass("li","others");
		for(var i=0;i<users.length;i++) {
			users[i].style.display="block";	
		}
	}
	else if(clicktag.id == "mine" && tab2.src != "../images/chatroom/mine.png") {
		tab2.src="../images/chatroom/mine.png";	
		tab1.src="../images/chatroom/all1.png";
		var users=getClass("li","others");
		for(var i=0;i<users.length;i++) {
			users[i].style.display="none";	
		}
	}
}

function getClass(tagName,className) //��ñ�ǩ��ΪtagName,����className��Ԫ��
{
    if(document.getElementsByClassName) {//֧���������        
		return document.getElementsByClassName(className);
    }
    else{       
		var tags=document.getElementsByTagName(tagName);//��ȡ��ǩ
        var tagArr=[];//���ڷ�������ΪclassName��Ԫ��
        for(var i=0;i < tags.length; i++){
            if(tags[i].class == className){
                tagArr[tagArr.length] = tags[i];//��������������Ԫ��
            }
        }
        return tagArr;
    }
}

