function isOnly() {
	var boxes=getClass("input","css-checkbox");
	var backs=getClass("label","css-label");
	var clicktag=event.srcElement;
	var flag=true;
	var k=0;
	for(var i=0;i<boxes.length;i++) {
		if(clicktag.id==boxes[i].id) {//alert("###"+boxes[i].id);
			k=i;
			if(!boxes[i].checked) {	//alert(boxes[i].id+" is checked");
				backs[i].style.backgroundPosition="0 0";
				boxes[i].checked="";
				flag=false;
				break;
			}
		}
		else {
			if(boxes[i].checked) {
				flag=false;
				alert("You can only choose one flag!");
				clicktag.checked="";
				break;
			}
		}
	}
	if(flag) {
		backs[k].style.	backgroundPosition="0 -3rem";
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

