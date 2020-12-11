<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberNetMap.aspx.cs" Inherits="Member_MemberNetMap" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title><%=PTitle%></title>

        <style type="text/css">
        .STYLE2 { COLOR: #00cc00 }
	    .STYLE3 { COLOR: red }
	    .STYLE4 { COLOR: #b3b3b3 }
	    .Tb22
	    {
	    	border-left:rgb(218,218,218) solid 1px;
	    	border-top:rgb(218,218,218) solid 1px;
	    }
	    .Tb22 td
	    {
            font-size:12px;
            border-right:rgb(218,218,218) solid 1px;
	    	border-bottom:rgb(218,218,218) solid 1px;
        }
        
        .anyes {
            /*background-image:url("../images/sysButtonCen.png");*/
            background-color:#96c742;
            background-repeat:no-repeat;
            color:#FFFFFF;
            font-family:"微软雅黑";
            font-size:14px;
            font-weight:bold;
            height:24px;
            line-height:24px;
            margin-left:10px;
            text-align:center;
            text-decoration:none;
            width:65px;
            border-width:0;
            }
            
        #divDH
        {
        	margin-top:10px;
        	font-size:10pt;
        }
        #divDH a
        {
        	color:Gray;
        }
    </style>
        
        <script type="text/javascript">
            function ShowView(isAnzhi, qishu, number, cengshu) {
                var isTrue = false;
                if (isAnzhi == "az") {
                    isTrue = true;
                }
                var loginBh = '<%=GetLoginMember() %>'
                var bhCw = AjaxClass.GetLogoutCw(number.toString(), qishu, isTrue).value;
                var loginCw = AjaxClass.GetLogoutCw(loginBh, qishu, isTrue).value;
                var showCs = AjaxClass.GetShowCengS(0).value;

                if (cengshu == 1) {
                    if (showCs - (bhCw - loginCw + 1) <= cengshu) {
                        return;
                    }
                }
                else {
                    if (showCs - (bhCw - loginCw) <= cengshu) {
                        return;
                    }
                }
                window.location.href = "MemberNetMap.aspx?cengshu=" + cengshu + "&net=" + isAnzhi + "&SelectGrass=" + qishu + "&bianhao=" + number;
            }

            function showSX() {
                var isAnzhi = '<%=Request.QueryString["isAnzhi"] %>';
                if (isAnzhi == "tj")
                    window.parent.location.href = "SST_TJ.aspx";
                else
                    window.parent.location.href = "SST_AZ.aspx";
            }

            function showcy2() {
                window.parent.location.href = "CommonlyNetwork.aspx";
                return false;
            }

            function showcy3() {
                window.parent.location.href = "GraphNetFrame.aspx";
                return false;
            }
        </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table id="tab" cellspacing="0"  cellpadding="0" border="0" runat="server" style="font-size:10pt" width="100%">
            <tr>
                <td>
                    <asp:button  id="Button1" runat="server" CssClass=" anyes" Text="确定" onclick="Button1_Click"></asp:button>
                    &nbsp;
                    <%=GetTran("000045", "期数")%>：
                    <asp:dropdownlist id="DropDownList_QiShu" runat="server" style="border:none;"></asp:dropdownlist>
                    <%=GetTran("000719","并且")%><%=GetTran("000673", "网络起点ID")%>：
                    <asp:textbox id="TextBox1" runat="server" Width="85" style="border:#cccccc solid 1px;"></asp:textbox>
				    <asp:Button ID="Button3" runat="server" Text="常用(11)" CssClass="anyes" Enabled="false"  />
				    <asp:Button ID="Button2" runat="server" Text="常用(222)" CssClass="anyes" style="display:none;" Visible="false" OnClientClick="return showcy2()"/>
				    <asp:Button ID="Button4" runat="server" Text="常用(3333)" CssClass="anyes" Visible="false" OnClientClick="return showcy3()"/>
				    <%--<asp:Button ID="Button4" runat="server" Text="表格" CssClass="anyes" 
                            onclick="Button3_Click" />
				                        <asp:Button ID="btnZk" runat="server" Text="展开" CssClass="anyes" 
                                onclick="btnZk_Click" />
				                        <asp:Button ID="Button5" runat="server" Text="伸缩" CssClass="anyes" 
                            onclick="Button4_Click" />--%>
                    <asp:Button ID="Button5" runat="server" Text="伸缩" CssClass="anyes" OnClientClick="showSX()" /> 
				</td>
			</tr>
		</table>
		<table width="100%"  class="tablema"> 
		    <tr>
		        <td>
                    <div id="divDH" runat="server">
                    </div>
                </td>
            </tr>
			<tr>
				<td align="center">
					<table>
						<tr>
							<td align="center">
								<div id="Divt1" runat="server"></div>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td align="left" style="font-size:10pt;">
				    <br>
					1.<span class="STYLE1"><%=GetTran("000674", "点击节点上的图标，可以查看会员三层以内的下属")%>。</span><br />
					2.<span class="STYLE2"><%=GetTran("000676", "绿色背景为老会员")%>。</span><br />
					3.<span class="STYLE3"><%=GetTran("000677", "红色背景为当期新增会员")%>。</span><br />
				</td>
			</tr>
		</table>
		<asp:textbox id="txt_PressKeyFlag" style="DISPLAY: none" Runat="server"></asp:textbox></form>
	</body>
</html>