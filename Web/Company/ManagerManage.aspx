<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManagerManage.aspx.cs" Inherits="Company_ManagerManage" %>

<%@ Register src="../UserControl/Pager.ascx" tagname="Pager" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<HTML xmlns="http://www.w3.org/1999/xhtml">
	<HEAD>
		<title>GlManage</title>
		<link href="CSS/Company.css" rel="stylesheet" type="text/css" />
<script language="javascript" type="text/javascript" src="../JS/jquery-1.2.6.js"></script>
        <script src="js/tianfeng.js" type="text/javascript"></script>
<script src="../JS/QCDS2010.js" type="text/javascript"></script>
</HEAD>
	<body onload="down2()">
		<form id="Form1" method="post" runat="server">
			<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td valign="top"><br /><div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                        Width="100%" onrowcommand="GridView1_RowCommand" 
            BackColor="#F8FBFD" CssClass="tablemb" onrowdatabound="GridView1_RowDataBound">
            <RowStyle HorizontalAlign="Center" />
                        <Columns>
                            <asp:TemplateField HeaderText="操作">
                            <ItemTemplate>
                                <asp:LinkButton ID="lkbDel" runat="server" CommandName="D" CommandArgument='<%#Eval("ID") %>' ><%=GetTran("000022", "删除")%></asp:LinkButton>
                                <asp:HyperLink id="Hyperlink1" runat="server" NAME="Hyperlink1" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "id", "ManagerModify.aspx?id={0:d}") %>'><%=GetTran("000036", "编辑")%></asp:HyperLink>
                            </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Number" HeaderText="管理员编号" >
                            </asp:BoundField>
                            <asp:BoundField DataField="mName" HeaderText="管理员名称" >
                            </asp:BoundField>
                            <asp:BoundField DataField="Dept" HeaderText="部门" >
                            </asp:BoundField>
                            <asp:BoundField DataField="dName" HeaderText="角色" >
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="操作">
                            <ItemTemplate>
                                <asp:HyperLink id="Hyperlink2" runat="server" NAME="Hyperlink1" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "Number", "ViewManage.aspx?manageID={0:d}") %>'><%=GetTran("001057", "网络图管理")%></asp:HyperLink>
                            </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="加入时间">
                                <ItemTemplate>
                                    <%#DateTime.Parse(Eval("begindate").ToString()).AddHours(double.Parse(Session["WTH"].ToString()))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                        </td></tr>
                                <tr>
                                    <th>
                                        管理员编号
                                    </th>
                                    <th>
                                        管理员名称
                                    </th>
                                    <th>
                                        角色
                                    </th>
                                    <th>
                                        网络图管理
                                    </th>
                                    <th>
                                        加入时间
                                    </th>
                                    <th>
                                        操作
                                    </th>
                                </tr>   
                        </EmptyDataTemplate>
            <AlternatingRowStyle BackColor="#F1F4F8" />
                    </asp:GridView>
                    <uc1:Pager ID="Pager1" runat="server" />
                    </div>
      <br /></td>
  </tr>
                <tr>
                  <td>
                        <INPUT onclick="javascript:window.location='ManagerAdd.aspx'" type="button" value='<%=GetTran("000325", "添加管理员")%>' class="another" />
                        <INPUT onclick="javascript: window.location = 'DeptRolesManage.aspx'" type="button" value='<%=GetTran("000421", "返回")%>'  class="anyes"/></td>
                </tr>
  
</table>
<div id="cssrain" style="width:100%">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
                        <tr>
                            <td width="80">
                                <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="Table1">
                                    <tr>
                                        <td class="sec2">
                                            <span id="sp" title='<%=GetTran("000628", "说明")%>'><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000032", "管 理"))%></span>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <a href="#">
                                    <img src="images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="img1"
                                        onclick="down2()" /></a>
                            </td>
                        </tr>
                    </table>
                    <div id="divTab2">
                        <table width="100%" height="68" border="0" cellspacing="0" class="DMbk" id="Table2">
                            <tbody style="display: block">
                                <tr>
                                    <td valign="bottom" style="padding-left: 20px">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
	1、<%=GetTran("001059", "设置管理员使用系统的权限")%>。<br />
                      2、<%=GetTran("001061", "添加新的管理员，删除管理员")%>。
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
</form>
					</body>
</HTML>
