<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
    <link rel="shortcut icon" type="image/x-icon" href="./images/chatroom/bubbles.png" />
    <script type="text/javascript" src="${pageContext.request.contextPath}/jquery/jquery-2.1.4.js"></script>
    <script type="text/javascript" src="${pageContext.request.contextPath}/org/cometd.js"></script>
    <script type="text/javascript" src="${pageContext.request.contextPath}/jquery/jquery.cometd.js"></script>
    <script type="text/javascript" src="createRoom.js"></script>
    <script type="text/javascript">
        var config = {
            contextPath: '${pageContext.request.contextPath}'
        };
    </script>
</head>
<body>
	<form>	
		roomId: <input id="roomId" name="roomId" type="text">
		<br>
		<input id="create" name="create" type="button" value="create">
		<div id = "noticeMessage"></div>
	</form>
	<br/>
	<div id = "body"></div>
</body>
