<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RecordsShall.aspx.cs" Inherits="Company_RecordsShall" %>
<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>结算记录扣补款</title>
        <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
<script src="../JS/QCDS2010.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript" src="../js/SqlCheck.js"></script>
<script language="javascript" type="text/javascript">
	function confirmvalue()
	{
	    return confirm('<%=GetTran("000248", "确定要删除吗？")%>');
	}
	function confirmvalue1()
	{
	    return confirm('<%=GetTran("001386", "确定要添加吗？")%>');
	}
		function CheckText(btname)
	{
		//这个方法是页面有多个按钮要提交时，多次使用这个方法,传入按钮的ID
		filterSql_II (btname);
		
	}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
          <tr>
          <td><asp:linkbutton id="lkSubmit" style="DISPLAY: none" Runat="server" Text="查 询" onclick="lkSubmit_Click"></asp:linkbutton>
                                <input class="anyes" id="bSubmit" onclick="CheckText('lkSubmit')" type="button" value='<%=GetTran("000048", "查 询")%>'></input>
                                
              <asp:Button ID="Button1" runat="server" Text="查 询" CssClass="anyes" 
                  onclick="Button1_Click" style="DISPLAY: none"/>&nbsp;<asp:DropDownList
                  ID="DropDownList1" runat="server">
                  <asp:ListItem Value="0">未添加</asp:ListItem>
                  <asp:ListItem Value="1">已添加</asp:ListItem>
              </asp:DropDownList>
              &nbsp;<%=GetTran("000024", "会员编号")%>：<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>&nbsp;
          </td>
          </tr>
          </table>
          <br />
    <table width="100%" border="0" cellpadding="0" cellspacing="0"   id="tbColor">
          <tr>
            <td style="word-break:keep-all;word-wrap:normal"><asp:GridView ID="GridView1" 
                    runat="server" AutoGenerateColumns="False"
                                width="100%" CssClass="tablemb" 
                    onrowcommand="GridView1_RowCommand" onrowdatabound="GridView1_RowDataBound">
                               <AlternatingRowStyle BackColor="#F1F4F8" />
                                <HeaderStyle CssClass="tablebt" Wrap="false"/>
                                <RowStyle HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField HeaderText="操作" ItemStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Lbtn" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID")%>' OnClientClick="return confirmvalue1()"><%#GetTran("006639", "添加")%></asp:LinkButton>&nbsp;
                                            <asp:LinkButton ID="LinkBtnDelete" runat="server" CommandName="Del" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID")%>' OnClientClick="return confirmvalue()"><%#GetTran("000022", "删除")%></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="number" HeaderText="会员编号"/>
                                    <asp:BoundField DataField="Name" HeaderText="会员姓名"/>
                                    <asp:BoundField DataField="yzongji" HeaderText="原总计" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right"/>
                                    <asp:BoundField DataField="ykoushui" HeaderText="原扣税"  DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right"/>
                                    <asp:BoundField DataField="ykoukuan" HeaderText="原扣款" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right"/>
                                    <asp:BoundField DataField="ybukuan" HeaderText="原补款"  DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right"/>
                                    <asp:BoundField DataField="yshifa" HeaderText="原实发" DataFormatString="{0:f2}"  ItemStyle-HorizontalAlign="Right"/>
                                    <asp:BoundField DataField="xzongji" HeaderText="本次结算总计"  DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right"/>
                                    <asp:BoundField DataField="xkoushui" HeaderText="本次结算扣税" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right"/>
                                    <asp:BoundField DataField="xkoukuan" HeaderText="本次结算扣款" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right"/>
                                    <asp:BoundField DataField="xbukuan" HeaderText="本次结算补款" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right"/>
                                    <asp:BoundField DataField="xshifa" HeaderText="本次结算的实发" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right"/>
                                    <asp:BoundField DataField="chayi" HeaderText="两次结算的差异额" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right"/>
                                    <asp:BoundField DataField="qishu" HeaderText="期数"/>
                                    <asp:BoundField DataField="cyflag" HeaderText="差异额是否添加"/>
                                    <asp:BoundField DataField="jsdate" HeaderText="本次结算时间" ItemStyle-Wrap="false"/>
                                </Columns>
                                <EmptyDataTemplate>
                                    <table width="100%" CssClass="tablemb" >
                                        <tr>
                                            <th>会员编号</th>
                                            <th>会员姓名</th>
                                            <th>原总计</th>
                                            <th>原扣款</th>
                                            <th>原补款</th>
                                            <th>原实发</th>
                                            <th>本次结算总计</th>
                                            <th>本次结算扣税</th>
                                            <th>本次结算扣款</th>
                                            <th>本次结算补款</th>
                                            <th>本次结算的实发</th>
                                            <th>两次结算的差异额</th>
                                            <th>期数</th>
                                            <th>差异额是否添加</th>
                                            <th>本次结算时间</th>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:GridView></td>
          </tr>
          </table>
          <table width="99%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
          <tr>
            <td><uc1:Pager ID="Pager1" runat="server" /></td>
          </tr>
</table>
    </form>
</body>
</html>
