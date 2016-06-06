<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
    <link rel="shortcut icon" type="image/x-icon" href="./images/chatroom/bubbles.png" />
    <script type="text/javascript" src="${pageContext.request.contextPath}/jquery/jquery-2.1.4.js"></script>
    <script type="text/javascript" src="${pageContext.request.contextPath}/org/cometd.js"></script>
    <script type="text/javascript" src="${pageContext.request.contextPath}/jquery/jquery.cometd.js"></script>
    <script type="text/javascript" src="index.js"></script>
    <link rel="stylesheet" href="./css/Bubble-Say.css">
    <%--
    The reason to use a JSP is that it is very easy to obtain server-side configuration
    information (such as the contextPath) and pass it to the JavaScript environment on the client.
    --%>
    <script type="text/javascript">
        var config = {
            contextPath: '${pageContext.request.contextPath}'
        };
    </script>
</head>
<body>
		<div id="screen">
			<img id="appname" src="./images/bubble-say/app-name.png" alt="Bubble Say" title="Bubble Say">
			<div id="bubbles">
				<img id="bubble" src="./images/bubble-say/Bubbles.png" alt="Bubble" title="Bubble">
			</div>
			<div id="loadin">
				<form action="./chatroom.jsp" method="post" enctype="application/x-www-form-urlencoded">	
					Channel: <input id="channel" name="channel" type="text" required/>
					<br>
					name: <input id="name" name="name" type="text" required>
					<br>
					<input id="enter" type=button value="Enter">
					<div id = "noticeMessage"></div>
				</form>
			</div>
		</div>
</body>
</html>
