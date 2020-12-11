<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompanyDeptManage.aspx.cs" Inherits="Company_CompanyDeptManage" %>

<%@ Register src="../UserControl/Pager.ascx" tagname="Pager" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<HTML>
	<HEAD>
		<title>系统部门管理</title>
		<link href="CSS/Company.css" rel="stylesheet" type="text/css" />
<script src="../JS/QCDS2010.js" type="text/javascript"></script>
<script type="text/javascript" src="js/tianfeng.js"></script>
  
</SCRIPT>
	</HEAD>
	<body onload="down2()">
		<form id="Form1" method="post" runat="server">
		
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td valign="top"><br /><div>
        <asp:GridView ID="gvCompanyDepts" runat="server" AutoGenerateColumns="False" 
             Width="100%" onrowcommand="gvCompanyDepts_RowCommand" onrowdatabound="gvCompanyDepts_RowDataBound"
             BackColor="#F8FBFD" CssClass="tablemb"  >
             <RowStyle HorizontalAlign="Center" />
            <Columns>
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnDel" runat="server" CommandName="D" CommandArgument='<%#Eval("ID") %>' ><%=GetTran("000022", "删除")%></asp:LinkButton>
                        <asp:HyperLink id="Hyperlink1" runat="server" NAME="Hyperlink1" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "id", "CompanyDeptModify.aspx?id={0:d}") %>'><%=GetTran("000036", "编辑")%></asp:HyperLink>
                    </ItemTemplate>
                    <HeaderStyle CssClass="tablebt" Width="20%"  />
                </asp:TemplateField>
                <asp:BoundField DataField="dept" HeaderText="部门名" >
                    <HeaderStyle CssClass="tablebt" Width="30%"  />
                </asp:BoundField>
            </Columns>
            <EmptyDataTemplate>
                <table Class="tablemb" BackColor="#F8FBFD" Width="100%">
                    <tr>
                        <th>
                            部门编号
                        </th>
                        <th>
                            部门名称
                        </th>
                        <th>
                            操作
                        </th>
                    </tr>                
                </table>
            </EmptyDataTemplate>
            <AlternatingRowStyle BackColor="#F1F4F8" />
        </asp:GridView>
        <uc1:Pager ID="PagerCompanyDept" runat="server" />
        </div>
      <br /></td>
  </tr>
  <tr>
  <td><INPUT onclick="javascript:window.location='CompanyDeptAdd.aspx'" type="button" value='<%=GetTran("000949", "添加部门")%>' class="anyes" />
                  <INPUT onclick="location.href='DeptRolesManage.aspx'" type="button" value='<%=GetTran("000421", "返回")%>'class="anyes" > 
  </td>
  </tr>
  <tr>
    <td valign="top">
	
	</td>
  </tr>
</table>

<div id="cssrain" style="width:100%">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
        <tr>
          <td width="80"><table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
              <tr>
                <td class="sec2"><span id="sp" title='<%=GetTran("000628", "说明")%>'><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000628", "说明"))%></span></td>
              </tr>
          </table></td>
          <td><a href="#"><img src="images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX" onclick="down2()"/></a></td>
        </tr>
      </table>
	  <div id="divTab2">
      <table width="100%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
        <tbody style="DISPLAY: block">
          <tr>
            <td valign="bottom" style="padding-left:20px"><table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td>1、<%=GetTran("000988", "设置公司部门")%>。
                  </td>
                </tr>
            </table></td>
          </tr>
        </tbody>
      </table>
	  </div>
    </div>
       
       
</form>
</body>
</HTML>

