<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MessageClassSetting.aspx.cs"
    Inherits="Company_MessageClassSetting" %>

<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
    <link type="text/css" rel="Stylesheet" href="CSS/Company.css" />
      <script src="../JS/QCDS2010.js" type="text/javascript"></script>

    <script>
	 $(document).ready(function(){
			if($.browser.msie && $.browser.version == 6) {
				FollowDiv.follow();
			}
	 });
	 FollowDiv = {
			follow : function(){
				$('#cssrain').css('position','absolute');
				$(window).scroll(function(){
				    var f_top = $(window).scrollTop() + $(window).height() - $("#cssrain").height() - parseFloat($("#cssrain").css("borderTopWidth")) - parseFloat($("#cssrain").css("borderBottomWidth"));
					$('#cssrain').css( 'top' , f_top );
				});
			}
	  }
    </script>

    <script type="text/javascript">
    window.onload=function()
	{
	    down2();
	};
	function down2()
	{
		if(document.getElementById("divTab2").style.display=="none")
		{
			document.getElementById("divTab2").style.display="";
			document.getElementById("imgX").src="images/dis1.GIF";
			
		}
		else
		{
			document.getElementById("divTab2").style.display="none";
			document.getElementById("imgX").src="images/dis.GIF";
		}
	}
    </script>
<SCRIPT language="javascript">
    
      
      function isDelete()
      {
         return window.confirm('<%=GetTran("000248")%>');
      }
</SCRIPT>
     <script language="javascript" type="text/javascript">
    
    function comfir(){
        return confirm('<%=GetTran("000022", "删除")%>');
    }
    
    </script>
     <script type="text/javascript">
        function cut()
        {
             document.getElementById("span1").title='<%=GetTran("000032", "管 理") %>';
        }
        function cut1()
        {
             document.getElementById("span2").title='<%=GetTran("000033", "说 明") %>';
        }
    </script>
</head>
<body >
    <form id="form1" runat="server">
    <div>
    <br />
        <table align="center" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                    <table class="biaozzi">
                        <tr>
                            
                            <td nowrap="nowrap">
                              <%=GetTran("007881", "添加邮件分类")%>：
                            </td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="TxtClassName" runat="server" ></asp:TextBox>
                            </td>
                             <td >
                                &nbsp;<asp:Button ID="BtnAddClass" runat="server" Text="添加" CssClass="anyes" 
                                     onclick="BtnAddClass_Click" />
                            </td>
                           
                           
                        </tr>
                    </table>
                </td>
            </tr>
            <tr><td><asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                        DataKeyNames="ID" ForeColor="#333333" GridLines="None" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                        OnRowEditing="GridView1_RowEditing" Width="40%" 
                    CssClass="tablemb bordercss" onrowdatabound="GridView1_RowDataBound" 
                    onrowdeleting="GridView1_RowDeleting" onrowupdating="GridView1_RowUpdating">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="类别编号" ReadOnly="true" ItemStyle-HorizontalAlign="Center"/>
                            <asp:TemplateField HeaderText="类别名称"  ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <%# Eval("ClassName") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TxtClassName_GV" runat="server" Text='<%# Eval("ClassName") %>' Width="140px">
                                    </asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle Width="150px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="操作"  ItemStyle-HorizontalAlign="Center">
                                <EditItemTemplate>
                                    <asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="true" CommandName="Update"
                                        ><%#GetTran("001823","更新")%></asp:LinkButton>
                                    <asp:LinkButton ID="lbCancel" runat="server" CausesValidation="true" CommandName="Cancel"
                                        ><%#GetTran("001614","取消")%></asp:LinkButton>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbEdit" runat="server" CausesValidation="true" CommandName="Edit"
                                        ><%#GetTran("000259","修改")%></asp:LinkButton>&nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="true" CommandName="Delete"
                                        ><%#GetTran("000022","删除")%></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerSettings FirstPageText="" LastPageText="" NextPageText="" PreviousPageText="" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Height="20px" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#999999" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    </asp:GridView></td></tr>
                    <tr>
                <td>
                    <table class="biaozzi">
                        <tr>
                            
                            <td nowrap="nowrap">
                              <%=GetTran("007884", "绑定类别和管理员")%>：
                            </td>
                            <td nowrap="nowrap">
                                <asp:DropDownList ID="DropClass" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td nowrap="nowrap">
                                <asp:DropDownList ID="DropAdmin" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td nowrap="nowrap">
                           
                                <asp:Button ID="Button2" runat="server" Text="保存" CssClass="anyes" 
                                    onclick="Button2_Click" />
                           
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <br>
                    <table width="100%">
                        <tr>
                            <td style="border:rgb(147,226,244) solid 1px">
                                <asp:GridView ID="givShowMessage" width="60%"  runat="server" 
                                    AutoGenerateColumns="False" OnRowCommand="givShowMessage_RowCommand" 
                                    CssClass="tablemb bordercss" onrowdatabound="givShowMessage_RowDataBound"   AlternatingRowStyle-Wrap="False" 
                        FooterStyle-Wrap="False" 
                        HeaderStyle-Wrap="False" 
                        PagerStyle-Wrap="False" 
                        RowStyle-Wrap="False" 
                        SelectedRowStyle-Wrap="False">
<FooterStyle Wrap="False"></FooterStyle>

<RowStyle Wrap="False"></RowStyle>
                                    <Columns>   
                                    <asp:TemplateField HeaderText="操作" ShowHeader="False" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="Button1" runat="server" CausesValidation="false" CommandName="Del"
                                                    CommandArgument='<%#Eval("ID") %>'  OnClientClick="return comfir()"><%=GetTran("000022", "删除")%></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                 
                                        <asp:BoundField HeaderText="类别编号" DataField="ClassID" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="center"/>
                                        <asp:BoundField HeaderText="类别名称" DataField="ClassName" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="center"/>
                                        <asp:BoundField HeaderText="管理员" DataField="Admin" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="center"/>                                    
                                    </Columns>
<EmptyDataTemplate>
                            <table  cellspacing="0" width="80%">
                                <tr>
                                    <th>
                                       <%=GetTran("000015", "操作")%> 
                                    </th>
                         
                                    <th><%=GetTran("007882", "类别编号")%></th>
										<th><%=GetTran("007883", "类别名称")%></th>
										<th><%=GetTran("000151", "管理员")%></th>
								
                                </tr>                
                            </table>
                        </EmptyDataTemplate>
<PagerStyle Wrap="False"></PagerStyle>

<SelectedRowStyle Wrap="False"></SelectedRowStyle>

<HeaderStyle Wrap="False"></HeaderStyle>

<AlternatingRowStyle BackColor="#F1F4F8" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <uc1:Pager ID="Pager1" runat="server" />
                            </td>
                        </tr>
                        <tr>
			               <td colspan="2" align="center">
                                  &nbsp;<asp:Button 
                                    ID="Button3" runat="server" onclick="Button3_Click" Text="返回" class="anyes"/>
                            </td>
			             </tr>	
                        </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
