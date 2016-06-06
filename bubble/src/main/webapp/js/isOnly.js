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

function getClass(tagName,className) //获得标签名为tagName,类名className的元素
{
    if(document.getElementsByClassName) {//支持这个函数        
		return document.getElementsByClassName(className);
    }
    else{       
		var tags=document.getElementsByTagName(tagName);//获取标签
        var tagArr=[];//用于返回类名为className的元素
        for(var i=0;i < tags.length; i++){
            if(tags[i].class == className){
                tagArr[tagArr.length] = tags[i];//保存满足条件的元素
            }
        }
        return tagArr;
    }
}

