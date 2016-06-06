(function($) {
    $(document).ready(function() {
    	var roomId;
    	function dealReply(replyMessage){
    		reply = replyMessage.data.status;
    		$('#noticeMessage').html('<p style = "color:red;font-size:20px" id = "msg">'+ replyMessage.data.message + '</p>');
    	}
    	function handshake(handshakeReply){
    		if(handshakeReply.successful){
    			$.cometd.batch(function(){
    				$.cometd.subscribe('/service/createRoom', dealReply);
    				$.cometd.publish('/service/createRoom', {roomId:roomId});
    			});
    		}
    	}
    	$('#create').click(function(){
    		roomId = $('#roomId').val();
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