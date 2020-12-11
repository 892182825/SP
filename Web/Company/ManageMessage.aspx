<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageMessage.aspx.cs" ValidateRequest="false" Inherits="Company_ManageMessage" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link type="text/css" rel="Stylesheet" href="CSS/Company.css" />

   <link rel="stylesheet" href="../javascript/kindeditor/themes/default/default.css" />
	<link rel="stylesheet" href="../javascript/kindeditor/plugins/code/prettify.css" />
	<script charset="utf-8" src="../javascript/kindeditor/kindeditor.js"></script>
	<script charset="utf-8" src="../javascript/kindeditor/lang/zh_CN.js"></script>
	<script charset="utf-8" src="../javascript/kindeditor/plugins/code/prettify.js"></script>
	<script>
                          
		KindEditor.ready(function(K) {
			var editor1 = K.create('#content1', {
			    cssPath: '../javascript/kindeditor/plugins/code/prettify.css',
			    uploadJson: '../javascript/kindeditor/asp.net/upload_json.ashx',
			    fileManagerJson: '../javascript/kindeditor/asp.net/file_manager_json.ashx',
				allowFileManager : true,
				afterCreate : function() {
					var self = this;
					K.ctrl(document, 13, function() {
						self.sync();
						K('form[name=form1]')[0].submit();
					});
					K.ctrl(self.edit.doc, 13, function() {
						self.sync();
						K('form[name=form1]')[0].submit();
					});
				}
			});
			prettyPrint();
		});

	</script>

</head>
<body>
    <form id="form1" runat="server">
    <br />
        <table class="biaozzi">
            <tr>
                <td>
                    <table class="biaozzi" >
                        <tr>
                            <td>
                                <table cellspacing="0" cellpadding="2" class="biaozzi">
                                <tr style="display:none;"><td><%=GetTran("007867", "发送国家")%>：
                                    <asp:DropDownList ID="DropDownList1" runat="server">
                                    </asp:DropDownList></td>
                                </tr>
                                <tr style="display:none;"><td>
                                    <%=GetTran("007868", "发送语言")%>：
                                    <asp:DropDownList ID="DropDownList2" runat="server">
                                    </asp:DropDownList>
                                </td></tr>
                                    <tr>
                                        <td style="white-space:nowrap">
                                           <%=GetTran("001519", "发布对象")%>：&nbsp;&nbsp;<asp:DropDownList ID="drop_LoginRole" 
                                                runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="drop_LoginRole_SelectedIndexChanged">
                                                <%--和数据库保持一致（2009-10-18所该）--%>
                                               <%-- <asp:ListItem Value="0"> 管理员</asp:ListItem>--%>
                                              <%--  <asp:ListItem Value="1">店铺</asp:ListItem>--%>
                                                <asp:ListItem Value="2" Selected="True">会员</asp:ListItem>
                                              
                                            </asp:DropDownList> 
                                        </td>
                                        
                                                </tr>
                                                <tr>
                                               <td> <asp:RadioButton ID="RadioBianhao" runat="server" AutoPostBack="True" 
                                                oncheckedchanged="RadioBianhao_CheckedChanged" Checked="True" 
                                                GroupName="RadioGroup" Text="指定编号" /> 
                                                   <asp:RadioButton ID="RadioRange" runat="server" AutoPostBack="True" 
                                                 oncheckedchanged="RadioRange_CheckedChanged" GroupName="RadioGroup" 
                                                Text="指定范围" /></td> 
                                        <td id="td_GongGao" runat="server" style="white-space:nowrap">
                              
                                            <asp:Label ID="Label2" runat="server"><%=GetTran("001684", "收件人编号")%>：</asp:Label><asp:TextBox ID="txtBianhao"
                                                runat="server" style="width:300px;" MaxLength="100"></asp:TextBox>
                                                <asp:Label ID="Label1" runat="server">(<%=GetTran("007873", "信件群发时 , 编号用 ' , ' 分隔(如:001,002),星号(*)表示发给所有成员")%>)</asp:Label>
                                        </td>
                                      
                                         <td id="td_ReceiveRange" runat="server" style="white-space:nowrap">
                                            &nbsp;
                                            <%--<asp:CheckBox ID="ChkLevel" runat="server" 
                                                oncheckedchanged="ChkLevel_CheckedChanged" AutoPostBack="True" Text="级别:"></asp:CheckBox>
                                            <asp:DropDownList ID="DropLevel" runat="server">
                                            </asp:DropDownList> --%>
                                             &nbsp;
                                            <asp:CheckBox ID="ChkBonus" runat="server" 
                                                oncheckedchanged="ChkBonus_CheckedChanged" AutoPostBack="True" Text="奖金区间:"></asp:CheckBox>
                                            <asp:TextBox ID="TxtBonusFrom" runat="server" MaxLength="10" Width="80px"></asp:TextBox>
                                            <asp:Label ID="LblTo" runat="server" Text="~"></asp:Label><asp:TextBox ID="TxtBonusTo" runat="server" MaxLength="10" Width="80px"></asp:TextBox>
                                            <asp:CheckBox ID="ChkNet" runat="server" 
                                                 AutoPostBack="True" oncheckedchanged="ChkNet_CheckedChanged" Text="团队:"></asp:CheckBox>
                                            <asp:TextBox ID="TxtLeader" runat="server" MaxLength="10" Width="92px"></asp:TextBox>
                                            <asp:DropDownList ID="DropRelation" runat="server">
                                            </asp:DropDownList> 
                                             &nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="white-space:nowrap">
                                <%=GetTran("000825", "信息标题")%>：&nbsp;<asp:TextBox ID="txtTitle" runat="server" Width="443px"  TextMode="SingleLine" MaxLength="100" ></asp:TextBox>
                                <asp:Label
                                    runat="server" ID="Label3">(<%=GetTran("001572", "标题限制在100个汉字以内")%>)</asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="word-spacing:normal">
                    <table cellspacing="0" cellpadding="0" border="0">
                        <tr>
                            <td valign="top" align="center" style="word-spacing:normal">
                                <div id="div2" style="DISPLAY: none"><asp:TextBox id="TextBox1" runat="server"></asp:TextBox></div>
                                <div class="editor">
                                <textarea id="content1" rows="8"  runat="server" wrap="physical" style=" VISIBILITY: hidden; WIDTH: 700px; HEIGHT: 350px" name="content"></textarea>
								
								</div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                              <asp:Button ID="btn_Save" 
                                    runat="server" Text="提 交" OnClick="btn_Save_Click" CssClass="anyes">
                                </asp:Button>
                                <asp:Button ID="btn_Cancle" runat="server" Text="取 消" CssClass="anyes" 
                                    onclick="btn_Cancle_Click"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    </form>
</body>
</html>
