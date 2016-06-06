(function($) {
    $(document).ready(function() {
    	var name;
    	var channel;
    	function dealReply(replyMessage){
    		reply = replyMessage.data.status;
    		if(reply == "OK"){
    			$('form').submit();
    		}
    		else{
    			$('#noticeMessage').html('<p style = "color:red;font-size:60px" id = "msg">'+ replyMessage.data.message + '</p>');
    		}
    	} 
    	function handshake(handshakeReply){
    		if(handshakeReply.successful){
    			$.cometd.batch(function(){
    				$.cometd.subscribe('/service/enterRoom', dealReply);
    				$.cometd.publish('/service/enterRoom', {channel:channel});
    			});
    		}
    	}
    	$('#enter').click(function(){
    		name = $('#name').val();
    		channel = $('#channel').val();
            var cometURL = location.protocol + "//" + location.host + config.contextPath + "/cometd";
            $.cometd.configure({
                url: cometURL,
                logLevel: 'debug'
            });
            
            $.cometd.addListener('/meta/handshake',handshake);
            $.cometd.handshake();
    	});
    })
})(jQuery);