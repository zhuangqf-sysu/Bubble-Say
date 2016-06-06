(function($) {


    $(document).ready(function() {
    
    /*ready*/
        var myName = $('#name').val();
        var channel = $('#channel').val();
        if(myName=="") myName = "anonymous";
        if(channel=="") channel = "chat";
        
   /*function*/
       var recive=function(event){
            
            var msgbox = "<div class=";
            
            if(event.data.sender==myName) msgbox+="\"myself\">";
            else msgbox+="\"others\">";
            
            msgbox+="<div class=\"username\">"+event.data.sender+"</div>";
            msgbox+="<div class=\"usermsg ";
            
            if(event.data.style=="opinion") msgbox+="usermsg_opinion \"";
            else if(event.data.style=="question") msgbox+="usermsg_question \"";
            else if(event.data.style=="answer") msgbox+="usermsg_answer \"";
            else if(event.data.style=="chatting") msgbox+="usermsg_chat\"";
            else msgbox+="usermsg0\"";
            msgbox+=">"+event.data.msg+"<div class=\"arrow";
            if(event.data.style=="opinion") msgbox+="_opinion \"";
            else if(event.data.style=="question") msgbox+="_question \"";
            else if(event.data.style=="answer") msgbox+="_answer \"";
            else if(event.data.style=="chatting") msgbox+="_chat\"";
            else msgbox+="0\"";
            msgbox+="\"></div></div></div>"
            
            $('#content').append(msgbox);
            
            setPos();
        }
        
        function subscribe(){
            $.cometd.subscribe('/'+channel,recive);
        }
        
        function myHandshake(event){
            if (event.successful) {
                $.cometd.batch(function() {
                    subscribe();
                    recive({data:{
                        sender:"system",
                        msg:"Hello,"+myName+"! Welcome to "+channel+"!"
                    }});
                });
            }else{
                recive({data:{
                    sender:"system",
                    msg:"Connect Error!"
               }});
            }
        }
        
        function connect(){
            var cometURL = location.protocol + "//" + location.host + config.contextPath + "/cometd";
            $.cometd.configure({
                url: cometURL,
                logLevel: 'debug'
            });
            
            $.cometd.addListener('/meta/handshake',myHandshake);
            $.cometd.handshake();
        }
        
    /*main*/
        connect();
        $('#sendbutton').click(function(){
            var check = "default";
            if($("#opinion")[0].checked) check = "opinion";
            else if($("#question")[0].checked) check = "question";
            else if($("#answer")[0].checked) check = "answer";
            else if($("#chatting")[0].checked) check = "chatting";
			$.cometd.publish('/'+channel, {
			    sender: myName,
				msg : $('#inputbox').val(),
				style: check
			});
			$('#inputbox').val("");
		});
        $(window).unload(function() {
            $.cometd.unsubscribe('/'+channel);
            $.cometd.disconnect();
        });
    });
})(jQuery);
