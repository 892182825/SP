<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Company_Default" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title><%=GetTran("004158", "公司子系统")%></title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="../Company/js/jquery.min.js" type="text/javascript"></script>

    <%--<style type="text/css">
        .STYLE1 {
	        font-size: 12px;
	        color: #FFFFFF;
	        font-family: "Arial";
	        line-height: 22px;
	        text-decoration: none;
        }
        .STYLE2 {
	        font-size: 14px;
	        color: #FFFFFF;
	        line-height: 22px;
	        text-decoration: none;
	        font-family: "Arial";
	        font-weight:bold;
        }
        .STYLE3 {font-size: 12px;
	        color: #333333;
	        font-family: "Arial";
	        line-height: 22px;
	        text-decoration: none;
        }
        .STYLE4 {
	        font-size: 14px;
	        color: #FFFF00;
	        line-height: 22px;
	        text-decoration: none;
	        font-family: "Arial";
	        font-weight:bold;
        }
        .STYLE4 a {
	        font-size: 14px;
	        color: #FFFF00;
	        line-height: 22px;
	        text-decoration: none;
	        font-family: "Arial";
        }
        .STYLE4 a:hover {
	        font-size: 14px;
	        color: #FFFF00;
	        line-height: 22px;
	        text-decoration: none;
	        font-family: "Arial";
        }
         .anliudp {
	        font-size: 12px;
	        color: #FFFFFF;
	        text-decoration: none;
	        background-image: url(images/anliudp.GIF);
	        background-repeat: repeat-x;
	        height: 22px;
	        border-top-width: 0px;
	        border-right-width: 1px;
	        border-bottom-width: 0px;
	        border-left-width: 1px;
	        border-style: solid;
	        border-color: #1D63B8;
        }
        .STYLE5 {
	        font-size: 14px;
	        font-weight: bold;
	        color: #666666;
        }
        
        #_company tr td{ margin:0px; padding:0px;}
    </style>--%>

<style type="text/css">
    *{padding:0;border:0;margin:0;list-style:none;box-sizing:border-box}
    body,html{height:100%;overflow: hidden;font-family:"微软雅黑";}
    .scht_bg{height:50%;}
    .scht_login{   height: 400px;
            overflow: hidden;
            padding-top: 3%;
            width:350px;padding:1%;background:#fff;margin:10% 0 0 65%;border-radius:5px ;border:1px solid #ccc;}
    .scht_l{width:100%;height:20%;text-align:center;}
    .scht_r{width:100%;height:80%;position: relative;}
    .scht_l h5{font-size:30px;font-weight:normal;color:#90bdd0;padding-right:0.8%;}
    .scht_l p{font-size:20px;margin-top:5%;color:#333;}
    .scht_r span{float:left;width:15%;height:100%;background:#e7f0f4;}
    .scht_r input,select{float:left;width:85%;height:100%;background:#f2f8fb;padding-left:5px;}
    .scht_r div{overflow: hidden;margin-top:7%;border-radius:3px;height:35px;border:1px solid #ddd;}
    .scht_user input
    { height:35px; line-height:35px;
    }
    .scht_user span{background:#e7f0f4 url(../img1/scht_user.png) no-repeat center center;background-size:63%;}
    .scht_password span{background:#e7f0f4 url(../img1/scht_password.png) no-repeat center center;background-size:46%;}
    .scht_yzm span{background:#e7f0f4 url(../img1/scht_yzm.png) no-repeat center center;background-size:58%;}
    .scht_login button{background:#24aef1;border-radius:3px;width:100%;margin-top:7%;color:#fff;height:12%}
    .scht_yzm input{width:60%;}
    .scht_r a{text-decoration: none;color:#666;position:absolute;bottom:8%;right:5%;font-size:12px;}
    .scht_yzm img{margin-left:1%;display:inline-block;}
    .scht_r .scht_user1{margin-top:1%;}
    /*body{background:url(../img1/mmmm.png) no-repeat 0 center;background-size:100%;}*/
    .hearer_h5 h5{font-size:40px;height:100px;line-height:100px;width:1220px;margin:0 auto;color:#333;font-weight:normal;}
    .scht_user1 span {
        background: #e7f0f4 url(../img/yyan.png) no-repeat center center;
        background-size: 63%;
    }  
</style>
<script type="text/javascript">
    //$(function () {
    //    var Dheight = $(' .scht_r div').height() + 2;
    //    $('.scht_r input').css({ 'lineHeight': Dheight + 'px', height: Dheight });
    //    $('select').css({ height: Dheight });
    //});
</script>
</head>
 
<%--<body leftmargin="0" topmargin="0">
<table width="600" height="32" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td>&nbsp;</td>
  </tr>
</table>
<table width="600" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td class="style1" style="height: 47px;">&nbsp;</td>
    <td bgcolor="#FF6600" style="width: 430px"><table width="98%" border="0" align="right" cellpadding="0" cellspacing="0">
      <tr>
        <td class="STYLE2"><%=GetTran("004158", "公司子系统")%></td>
      
        <td class="STYLE4"><a href="../Store/index.aspx"><%=GetTran("004160", "店铺子系统")%></a></td>
        <td class="STYLE4"><a href="../Member/index.aspx"><%=GetTran("004168", "会员子系统")%></a></td>
        
      </tr>
    </table></td>
  </tr>
</table>
<table width="600" height="40" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td>&nbsp;</td>
  </tr>
</table>
<table width="600" border="0" align="center" cellpadding="0" cellspacing="0" id="_company">
	<tr>
		<td>
		    <img src="images/DS2010Company_04.jpg" width="111" height="79" alt="" />
		</td>
		<td width="390" height="79" valign="bottom" background="images/DS2010Company_05.jpg"><table width="70%" height="50" border="0" align="center" cellpadding="0" cellspacing="0">
          <tr>
            <td><span class="STYLE5"><%=GetTran("004158", "公司子系统")%></span></td>
          </tr>
        </table></td>
		<td>
			<img src="images/DS2010Company_06.jpg" width="99" height="79" alt=""></td>
	</tr>
	
    <form id="form1" runat="server">
	<tr>
		<td>
			<img src="images/DS2010Company_07.jpg" width="111" height="178" alt=""></td>
		<td background="images/DS2010Company_08.gif"><table width="80%" border="0" align="center" cellpadding="4" cellspacing="0" class="STYLE3">
          <tr>
            <td align="right"><%=GetTran("004169", "语言")%>：</td>
            <td>
                <asp:DropDownList ID="ddlLanguage" runat="server" 
                    onselectedindexchanged="ddlLanguage_SelectedIndexChanged" 
                    AutoPostBack="True">
                </asp:DropDownList>
                        </td>
          </tr>
          <tr>
            <td align="right"><%=GetTran("004171", "用户名")%>：</td>
            <td><asp:TextBox ID="txtName" runat="server" MaxLength="10" Width="120px"></asp:TextBox></td>
          </tr>
          <tr>
            <td align="right"><p><%=GetTran("003231", "密码")%>：</p></td>
            <td><asp:TextBox ID="txtPwd" runat="server" MaxLength="10" TextMode="Password" 
                    Width="120px"></asp:TextBox></td>
          </tr>
          <tr>
            <td align="right"><%=GetTran("004175", "验证码")%>：</td>
            <td >
                <div style="float:left;">
                    <div style="width:75px;float:left;">
                         <asp:TextBox ID="txtValidate" runat="server" MaxLength="5" Width="60px"></asp:TextBox> 
                    </div>
                    
                    <div style="float:left;">
                      <a href="javascript:void(0)"  onclick="UpdateYZ()">  <img style="border:none;" id="img1" src="../image.aspx" /></a>
                   </div> 
                </div> 
            </td>
          </tr>
          <tr>
            <td height="28" colspan="2" align="center">
             <asp:Button ID="btnSubmit" runat="server" Text="登录" onclick="btnSubmit_Click" CssClass="anliudp" />&nbsp;&nbsp;&nbsp;&nbsp;
             <asp:Button ID="btnExit" runat="server" Text="重置" onclientclick="ClearTxt()" CssClass="anliudp"   />
            </td>
          </tr>
        </table></td>
		<td>
			<img src="images/DS2010Company_09.jpg" width="99" height="178" alt=""></td>
	</tr>
	<%=msg %>
    </form>
</table>
<table width="600" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td width="111"><img src="images/DS2010Company_10.jpg" width="111" height="125" alt=""></td>
    <td background="images/DS2010Company_11.gif"><span class="STYLE1"><%=GetTran("000227", "提示")%>：<br>
    &nbsp;&nbsp;&nbsp;&nbsp;* <%=GetTran("004182", "* 为了您系统的安全，建议每周更换一次密码")%>！<br />
    &nbsp;&nbsp;&nbsp;&nbsp;</span>
    </span></td>
    <td width="99"><img src="images/DS2010Company_12.jpg" width="99" height="125" alt=""></td>
  </tr>
</table>
</body>--%>
<body>
        <img src="../img1/index.png" width="100%" height="100%" alt="" style="position:absolute;z-index:-1"/>
    <form id="form1" runat="server" style="height:100%">
   <%-- <td bgcolor="#FF6600" style="width: 430px"><table width="98%" border="0" align="right" cellpadding="0" cellspacing="0">
      <tr>
        <td class="STYLE2"><%=GetTran("004158", "公司子系统")%></td>
      
        <td class="STYLE4"><a href="../Store/index.aspx"><%=GetTran("004160", "店铺子系统")%></a></td>
        <td class="STYLE4"><a href="../Member/index.aspx"><%=GetTran("004168", "会员子系统")%></a></td>
        
      </tr>
    </table></td>--%>
    <div class="scht_login">	
    	<div class="scht_l">	
        	<h5></h5>
            <p>公司后台管理系统</p>
        </div>
    	<div class="scht_r">
            <%--<div class="scht_user1">	
            	<span></span>
                  <asp:DropDownList ID="ddlLanguage" runat="server" 
                    onselectedindexchanged="ddlLanguage_SelectedIndexChanged" 
                    AutoPostBack="True">
                </asp:DropDownList>
            </div>--%>
        	<div class="scht_user">	
            	<span></span>
               <asp:TextBox ID="txtName" runat="server" placeholder="用户名" MaxLength="10"></asp:TextBox>
            </div>
        	<div class="scht_password">	
            	<span></span>
                <asp:TextBox ID="txtPwd" runat="server" placeholder="密码" MaxLength="10" TextMode="Password"></asp:TextBox>
            </div>
        	<div class="scht_yzm">	
            	<span></span>
                <asp:TextBox ID="txtValidate" placeholder="验证码" runat="server" MaxLength="5"></asp:TextBox> 
                <img id="img1" src="../image.aspx" alt="" width="24%" height="100%" />
            </div>
        	   <asp:Button ID="btnSubmit" runat="server" Text="登录" onclick="btnSubmit_Click" CssClass="anliudp" style="background:#24aef1;border-radius:3px;width:100%;margin-top:7%;color:#fff;height:12%" />
            <%-- <asp:Button ID="btnExit" runat="server" Text="重置" onclientclick="ClearTxt()" CssClass="anliudp"  style="color:#666;position:absolute;bottom:8%;left:5%;font-size:12px;height:auto;width:auto;background:none;clear:both;cursor:pointer" />
            --%><a style="display:none" href="../PassWordManage/FindPass.aspx?pass=Two&type=store">忘记密码？</a>
        </div>
    </div>
    <%=msg %>
    </form>
</body>
</html>
<script type="text/javascript" language="javascript">
    function ClearTxt() {
        var txtname = document.getElementById("txtName");
        var txtpwd = document.getElementById("txtPwd");
        var txtyz = document.getElementById("txtValidate");
        if (txtname.value != "" || txtpwd.value != "" || txtyz.value != "") {
            txtname.value = "";
            txtpwd.value = "";
            txtyz.value = "";
        }
    }

    function UpdateYZ() {
        var img1 = document.getElementById("img1");
        img1.src = "../image.aspx?t=" + Math.random();
    }
</script>