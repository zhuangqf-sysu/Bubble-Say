<%@ page language="java" import="java.util.*" 
         contentType="text/html; charset=utf-8"%>
<% request.setCharacterEncoding("utf-8");%>

<%
    String name = "";
    String channel = "";
    if(request.getParameter("name")!=null) name = request.getParameter("name");
    if(request.getParameter("channel")!=null) channel = request.getParameter("channel");
%>

<!DOCTYPE html>
<html>
<head>
    <link rel="shortcut icon" type="image/x-icon" href="./images/chatroom/bubbles.png" />
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
    <script language="javascript" src="./js/setHeight.js"></script>
	<script language="javascript" src="./js/setPos.js"></script>
	<script language="javascript" src="./js/changeTabs.js"></script>
	<script language="javascript" src="./js/setCheckboxColor.js"></script>
	<script language="javascript" src="../js/isOnly.js"></script>

	<link rel="stylesheet" href="./css/chatroom-all.css">
	<link rel="stylesheet" href="./css/content.css">
	<link rel="stylesheet" href="./css/bubbles.css">
	<link rel="stylesheet" href="./css/checkboxstyle.css">
	<link rel="stylesheet" href="./css/send.css">
   
    <script type="text/javascript" src="${pageContext.request.contextPath}/jquery/jquery-2.1.4.js"></script>
    <script type="text/javascript" src="${pageContext.request.contextPath}/org/cometd.js"></script>
    <script type="text/javascript" src="${pageContext.request.contextPath}/jquery/jquery.cometd.js"></script>
    <script type="text/javascript" src="application.js"></script>

    <script type="text/javascript">
        var config = {
            contextPath: '${pageContext.request.contextPath}'
        };
    </script>
</head>

<input id="name" type="hidden" value=<%=name%> >
<input id="channel" type="hidden" value=<%=channel%> >

<body>
    <body onLoad="setHeight();setPos();setCheckboxColor();"  onResize="setHeight();">
		<div id="screen" style="">
			<div id="chatbox">
				<div class="nav"><img id="all" src="./images/chatroom/all.png" alt="all" title="all" onClick="changeTabs(this);setPos();"></div>
				<div class="nav"><img id="mine" src="./images/chatroom/mine1.png" alt="mine" title="mine" onClick="changeTabs(this);setPos();"></div>
				<div id="display">
					<div id="content">
						
					</div>
				</div>
				<div id="sendbox">
					<form action="register.jsp" method="post" enctype="application/x-www-form-urlencoded">	
						<input id="opinion" name="opinion" type="checkbox" class="css-checkbox" onClick="isOnly();">
						<label for="opinion" id="checkbox1" class="css-label">opinion</label>
						<input id="question" name="question" type="checkbox" class="css-checkbox" onClick="isOnly();">
						<label for="question" id="checkbox2" class="css-label">question</label>
						<input id="answer" name="answer" type="checkbox" class="css-checkbox" onClick="isOnly();">
						<label for="answer" id="checkbox3" class="css-label">answer</label>
						<input id="chatting" name="chatting" type="checkbox" class="css-checkbox" onClick="isOnly();">
						<label for="chatting" id="checkbox4" class="css-label">chatting</label>
						<textarea id="inputbox" name="input" rows="1"></textarea>
						<input id="sendbutton" name="send" type="button" value="send">
					</form>
					<img id="tagbubbles" src="./images/chatroom/bubbles.png" alt="bubbles" title="bubbles">
				</div>
			</div>    <!--end of chatbox-->
		</div>
	</body>	
</body>
</html>
