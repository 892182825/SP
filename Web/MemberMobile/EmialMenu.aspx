<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmialMenu.aspx.cs" Inherits="Member_EmialMenu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body{ margin:0px; padding:0px; list-style:none;}
        .email{ width:140px; height:500px; float:left; padding:5px 10px; background:url(images/bg.jpg); font-family:"微软雅黑"; font-weight:bold; font-size:12px;}
        .email ul{ margin:0px; padding:0px; list-style:none;}
        .email ul li{ width:140px;line-height:30px; float:left; display:block; border-bottom:dashed 1px #CCC;}
        .email ul li a{ text-decoration:none; color:#000;}
        .email ul li a:hover{ color:#207100;}
        .email ul li img{ float:left; margin:5px 10px 0 0;}
    </style>
    <script type="text/javascript" language="javascript">
    
        window.onload=function()
        {
//            parent.document.all("Email_Left").style.height=document.body.scrollHeight; 
//            parent.document.all("Email_Right").style.height=document.body.scrollHeight;
        }
        function changeMenu(num)
        {
            var Email_Right = parent.document.all("Email_Right");
            
            switch(num)
            {
                case 1:
                    Email_Right.src="WriteEmail.aspx";
                    break;
                case 2:
                    Email_Right.src="ReceiveEmail.aspx";
                    break;
                case 3:
                    Email_Right.src="SendEmail.aspx";
                    break;
                case 4:
                    Email_Right.src="UnusedEmail.aspx";
                    break;
            }
            if(num.toString().length>=2){
                var rnum = num.toString().substring(1,2);
                Email_Right.src="ReceiveEmail.aspx?type="+rnum;
            }
        }
     </script>
</head>
<body >
    <form id="form1" runat="server">
    <div class="email">
        <ul>
            <li id="liWriteEmail" onclick="changeMenu(1)">
                <img src="images/1.png" width="16" height="16" alt='<%=GetTran("003104", "写邮件")%>' /><a href="#"><%=GetTran("003104", "写邮件")%></a>
            </li>
            <li id="liReceiveEmail" onclick="changeMenu(2)">
                <img src="images/2.png" width="16" height="16" alt='<%=GetTran("005151", "收件箱")%>' /><a href="#"><%=GetTran("005151", "收件箱")%></a>
            </li>
            <li id="liSendEmail" onclick="changeMenu(3)">
                <img src="images/3.png" width="16" height="16" alt='<%=GetTran("005154", "发件箱")%>' /><a href="#"><%=GetTran("005154", "发件箱")%></a>
            </li>
            <li id="liUnusedEmail" onclick="changeMenu(4)">
                <img src="images/4.png" width="16" height="16" alt='<%=GetTran("003107","废件箱") %>' /><a href="#"><%=GetTran("003107", "废件箱")%></a>
            </li>
        </ul>
    </div>
    </form>
</body>
</html>