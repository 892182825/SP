<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MemberBottom.ascx.cs" Inherits="UserControl_MemberBottom" %>
<!--版权信息-->
<div id="bottomDiv" class="copyrignt" style="display:none">
	<ul>
    	<li><a href="#"><%=tran.GetTran("000421", "返回")%></a><span>|</span></li>
        <li><a href="../member/first.aspx"><%=tran.GetTran("001478", "首页")%></a><span>|</span></li>
        <li><a href="#"><%=tran.GetTran("001651", "帮助")%></a><span>|</span></li>
        <li><a href="#"><%=tran.GetTran("007282", "联系我们")%></a></li>
        <li class="allright">Copyright © 2012 All Rights Reserved</li>
  </ul>
</div>
<script type="text/javascript">
    
//    function setBottom()
//    {//alert(document.getElementById("bottomDiv").offsetTop+"  "+document.getElementById("bottomDiv").offsetHeight+"  "+document.documentElement.scrollTop+"  "+document.documentElement.clientHeight);
//        if(document.getElementById("bottomDiv").offsetTop+document.getElementById("bottomDiv").offsetHeight<document.documentElement.scrollTop+document.documentElement.clientHeight)
//        {
//            document.getElementById("bottomDiv").style.position="absolute";
//            //alert(document.documentElement.scrollTop+document.documentElement.clientHeight -document.getElementById("bottomDiv").offsetHeight+"px")
//            document.getElementById("bottomDiv").style.top=document.documentElement.scrollTop+document.documentElement.clientHeight -document.getElementById("bottomDiv").offsetHeight+"px";
//        }
//        
//        setTimeout("setBottom()",5);
//    }
//    
//    setBottom();
</script>