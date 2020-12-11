<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CommonlyNetwork.aspx.cs" Inherits="Company_CommonlyNetwork" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        
        
        .wlttab2
        {
        	width:100%;
        	border-left:rgb(136,224,244) solid 1px;
        	border-top:rgb(136,224,244) solid 1px;
        }
        
        .wlttd2
        {
        	border-right:rgb(136,224,244) solid 1px;
        	border-bottom:rgb(136,224,244) solid 1px;
        	height:25px;
        	text-align:center;
        }
        
        .ctd1
        {
        	background-color:rgb(241,244,248);
        	color:rgb(43,128,45);
        }
        .cdiv1
        {
        	padding-top:4px;
        	float:left;
        	width:295px;
        	height:21px;
        	text-align:center;
        }
        .cdiv2
        {
        	padding-top:4px;
        	float:left;
        	width:145px;
        	height:21px;
        	text-align:center;
        }
    </style>
    <script type="text/javascript">
        window.onload=function()
        {
            if(navigator.userAgent.toLowerCase().indexOf("msie 6.0")!=-1)
            {
                //document.getElementById("div1").style.marginLeft="25px";
                //document.getElementById("div2").style.marginLeft="25px";
            }
        }
        
        function showcy1()
        {
            window.location.href="MemberNetMap.aspx?isAnzhi=az";
            return false;
        }
    </script>
</head>
<body style="font-size:10pt">
    <form id="form1" runat="server">
    <div>
        <div style="width:100%;background-color:white; height:600px;" align="center">
            <br>
            <div style="width:98%;padding-left:10px" align="left">
                 <asp:button  id="Button1" runat="server" class="anyes" Text="确定" onclick="Button1_Click"></asp:button>
		        &nbsp;
		        <%=GetTran("000045", "期数")%>：

		        <asp:dropdownlist id="DropDownList_QiShu" runat="server"></asp:dropdownlist>
		        <%=GetTran("000719", "并且")%><%=GetTran("000673", "网络起点ID")%>：

		        <asp:TextBox CssClass="tab_input"  id="TextBox1" runat="server" Width="85"></asp:textbox>
    			
    			<asp:Button ID="Button2" runat="server" Text="常用(1)" class="anyes" onclientclick="return showcy1()"/>
		        <asp:Button ID="Button3" runat="server" Text="常用(2)" class="anyes" Enabled="false"  />

	            <asp:Button ID="Button5" runat="server" Text="伸缩" class="anyes"
                    onclick="Button5_Click" />&nbsp;&nbsp;&nbsp;
            
                <br><br>
                <span style="color:rgb(0,61,92)">
                 <%=GetTran("000369", "可查看网络")%>  
                </span>
                <asp:Literal ID="LitMaxWl" runat="server"></asp:Literal>
                <br><br>
                <span style="color: rgb(0,61,92)"><%=GetTran("007032", "链路图")%> ： </span>
                <asp:Literal ID="LitLLT" runat="server"></asp:Literal>
                <br><br>
            </div>
            <div style="width:900px">
                <div style="width:900px;height:105px;overflow:hidden;">
                    <table border="0" cellpadding="0" cellspacing="0" class="wlttab2">
                        <tr>
                            <td class="wlttd2 ctd1" colspan="6">
                                编号：<asp:Label ID="dyc1" runat="server" Text="--"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="wlttd2" colspan="3" style="width:450px">
                                昵称：<asp:Label ID="dyc2" runat="server" Text="--"></asp:Label>
                            </td>
                            <td class="wlttd2" colspan="3" style="width:450px">
                                级别：<asp:Label ID="dyc3" runat="server" Text="--"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="wlttd2" colspan="6" >
                                <div class="cdiv1">
                                    总：<asp:Label ID="dyc4" runat="server" Text="--"></asp:Label>
                                </div>
                                <div class="cdiv1" style="border-left:rgb(136,224,244) solid 1px;border-right:rgb(136,224,244) solid 1px;">
                                    新：<asp:Label ID="dyc5" runat="server" Text="--"></asp:Label>
                                </div>
                                <div class="cdiv1">
                                    余：<asp:Label ID="dyc6" runat="server" Text="--"></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="wlttd2" colspan="3">
                                <asp:Label ID="dyc7" runat="server" Text="--"></asp:Label>
                            </td>
                            <td class="wlttd2" colspan="3">
                                <asp:Label ID="dyc8" runat="server" Text="--"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="div1" style="float:left;width:450px;height:105px;overflow:hidden;margin:2px 1px 0px 0px;">
                    <table border="0" cellpadding="0" cellspacing="0" class="wlttab2">
                        <tr>
                            <td class="wlttd2 ctd1" colspan="6">
                                编号：<asp:Label ID="dec1" runat="server" Text="--"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="wlttd2" colspan="3" style="width:223px">
                                昵称：<asp:Label ID="dec2" runat="server" Text="--"></asp:Label>
                            </td>
                            <td class="wlttd2" colspan="3" style="width:223px">
                                级别：<asp:Label ID="dec3" runat="server" Text="--"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="wlttd2" colspan="6" >
                                <div class="cdiv2">
                                    总：<asp:Label ID="dec4" runat="server" Text="--"></asp:Label>
                                </div>
                                <div class="cdiv2" style="border-left:rgb(136,224,244) solid 1px;border-right:rgb(136,224,244) solid 1px;">
                                    新：<asp:Label ID="dec5" runat="server" Text="--"></asp:Label>
                                </div>
                                <div class="cdiv2">
                                    余：<asp:Label ID="dec6" runat="server" Text="--"></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="wlttd2" colspan="3">
                                <asp:Label ID="dec7" runat="server" Text="--"></asp:Label>
                            </td>
                            <td class="wlttd2" colspan="3">
                                <asp:Label ID="dec8" runat="server" Text="--"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="float:left;width:449px;height:105px;overflow:hidden;margin:2px 0px 0px 0px">
                    <table border="0" cellpadding="0" cellspacing="0" class="wlttab2">
                        <tr>
                            <td class="wlttd2 ctd1" colspan="6">
                                编号：<asp:Label ID="dec9" runat="server" Text="--"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="wlttd2" colspan="3" style="width:223px">
                                昵称：<asp:Label ID="dec10" runat="server" Text="--"></asp:Label>
                            </td>
                            <td class="wlttd2" colspan="3" style="width:223px">
                                级别：<asp:Label ID="dec11" runat="server" Text="--"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="wlttd2" colspan="6" >
                                <div class="cdiv2">
                                    总：<asp:Label ID="dec12" runat="server" Text="--"></asp:Label>
                                </div>
                                <div class="cdiv2" style="border-left:rgb(136,224,244) solid 1px;border-right:rgb(136,224,244) solid 1px;">
                                    新：<asp:Label ID="dec13" runat="server" Text="--"></asp:Label>
                                </div>
                                <div class="cdiv2">
                                    余：<asp:Label ID="dec14" runat="server" Text="--"></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="wlttd2" colspan="3">
                                <asp:Label ID="dec15" runat="server" Text="--"></asp:Label>
                            </td>
                            <td class="wlttd2" colspan="3">
                                <asp:Label ID="dec16" runat="server" Text="--"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div  id="div2" style="float:left;width:225px;height:135px;overflow:hidden;margin:2px 0px 0px 0px">
                    <table border="0" cellpadding="0" cellspacing="0" class="wlttab2">
                        <tr>
                            <td class="wlttd2 ctd1" colspan="6">
                                编号：<asp:Label ID="dsc1" runat="server" Text="--"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="wlttd2" colspan="3" style="width:50%">
                                昵称：<asp:Label ID="dsc2" runat="server" Text="--"></asp:Label>
                            </td>
                            <td class="wlttd2" colspan="3" style="width:50%">
                                级别：<asp:Label ID="dsc3" runat="server" Text="--"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="wlttd2" colspan="2" style="width:33%" >
                                总：<br><asp:Label ID="dsc4" runat="server" Text="--"></asp:Label>
                            </td>
                            <td class="wlttd2" colspan="2" style="width:33%">
                                新：<br><asp:Label ID="dsc5" runat="server" Text="--"></asp:Label>
                            </td>
                            <td class="wlttd2" colspan="2" style="width:33%">
                                余：<br><asp:Label ID="dsc6" runat="server" Text="--"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="wlttd2" colspan="3">
                                <asp:Label ID="dsc7" runat="server" Text="--"></asp:Label>
                            </td>
                            <td class="wlttd2" colspan="3">
                                <asp:Label ID="dsc8" runat="server" Text="--"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="float:left;width:224px;height:135px;overflow:hidden;margin:2px 1px 0px 1px">
                    <table border="0" cellpadding="0" cellspacing="0" class="wlttab2">
                        <tr>
                            <td class="wlttd2 ctd1" colspan="6">
                                编号：<asp:Label ID="dsc9" runat="server" Text="--"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="wlttd2" colspan="3" style="width:50%">
                                昵称：<asp:Label ID="dsc10" runat="server" Text="--"></asp:Label>
                            </td>
                            <td class="wlttd2" colspan="3" style="width:50%">
                                级别：<asp:Label ID="dsc11" runat="server" Text="--"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="wlttd2" colspan="2" style="width:33%">
                                总：<br><asp:Label ID="dsc12" runat="server" Text="--"></asp:Label>
                            </td>
                            <td class="wlttd2" colspan="2" style="width:33%">
                                新：<br><asp:Label ID="dsc13" runat="server" Text="--"></asp:Label>
                            </td>
                            <td class="wlttd2" colspan="2" style="width:33%">
                                余：<br><asp:Label ID="dsc14" runat="server" Text="--"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="wlttd2" colspan="3">
                                <asp:Label ID="dsc15" runat="server" Text="--"></asp:Label>
                            </td>
                            <td class="wlttd2" colspan="3">
                                <asp:Label ID="dsc16" runat="server" Text="--"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="float:left;width:224px;height:135px;overflow:hidden;margin:2px 1px 0px 0px">
                    <table border="0" cellpadding="0" cellspacing="0" class="wlttab2">
                        <tr>
                            <td class="wlttd2 ctd1" colspan="6">
                                编号：<asp:Label ID="dsc17" runat="server" Text="--"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="wlttd2" colspan="3" style="width:50%">
                                昵称：<asp:Label ID="dsc18" runat="server" Text="--"></asp:Label>
                            </td>
                            <td class="wlttd2" colspan="3" style="width:50%">
                                级别：<asp:Label ID="dsc19" runat="server" Text="--"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="wlttd2" colspan="2" style="width:33%">
                                总：<br><asp:Label ID="dsc20" runat="server" Text="--"></asp:Label>
                            </td>
                            <td class="wlttd2" colspan="2" style="width:33%">
                                新：<br><asp:Label ID="dsc21" runat="server" Text="--"></asp:Label>
                            </td>
                            <td class="wlttd2" colspan="2" style="width:33%">
                                余：<br><asp:Label ID="dsc22" runat="server" Text="--"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="wlttd2" colspan="3">
                                <asp:Label ID="dsc23" runat="server" Text="--"></asp:Label>
                            </td>
                            <td class="wlttd2" colspan="3">
                                <asp:Label ID="dsc24" runat="server" Text="--"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="float:left;width:224px;height:135px;overflow:hidden;margin:2px 0px 0px 0px">
                    <table border="0" cellpadding="0" cellspacing="0" class="wlttab2">
                        <tr>
                            <td class="wlttd2 ctd1" colspan="6">
                                编号：<asp:Label ID="dsc25" runat="server" Text="--"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="wlttd2" colspan="3" style="width:50%">
                                昵称：<asp:Label ID="dsc26" runat="server" Text="--"></asp:Label>
                            </td>
                            <td class="wlttd2" colspan="3" style="width:50%">
                                级别：<asp:Label ID="dsc27" runat="server" Text="--"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="wlttd2" colspan="2" style="width:33%">
                                总：<br><asp:Label ID="dsc28" runat="server" Text="--"></asp:Label>
                            </td>
                            <td class="wlttd2" colspan="2" style="width:33%">
                                新：<br><asp:Label ID="dsc29" runat="server" Text="--"></asp:Label>
                            </td>
                            <td class="wlttd2" colspan="2" style="width:33%">
                                余：<br><asp:Label ID="dsc30" runat="server" Text="--"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="wlttd2" colspan="3">
                                <asp:Label ID="dsc31" runat="server" Text="--"></asp:Label>
                            </td>
                            <td class="wlttd2" colspan="3">
                                <asp:Label ID="dsc32" runat="server" Text="--"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br>
            <br>
        </div>
    </div>
    </div>
    </form>
</body>
</html>

