<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeptRolesManage.aspx.cs" Inherits="Company_DeptRolesManage" %>

<%@ Register src="../UserControl/Pager.ascx" tagname="Pager" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
	<title>角色管理页</title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../JS/jquery-1.2.6.js"></script>
    <script language="javascript" type="text/javascript" src="../javascript/ManagementVsExplanation.js"></script>
    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
    <script language="javascript" type="text/javascript" src="../JS/QCDS2010.js"></script>
    <script src="js/tianfeng.js" type="text/javascript"></script>

    <script type="text/javascript">
        function cutDescription() {
            document.getElementById("span2").title = '<%=GetTran("000033", "说 明") %>';
        }

        window.onload = function() {
            down2();
        } 
    </script>
</head>

<body>
<form id="Form1" method="post" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td valign="top"><br /><div>
        <asp:GridView ID="gvDeptRoless" runat="server" AutoGenerateColumns="False" Width="100%"
            onrowcommand="gvDeptRoless_RowCommand" 
            onrowdatabound="gvDeptRoless_RowDataBound" BackColor="#F8FBFD" CssClass="tablemb">
                                        <RowStyle BorderWidth="0px" HorizontalAlign="Center"/>
            <Columns>
                <asp:TemplateField HeaderText="操作" >
                    <ItemTemplate>
                         <asp:LinkButton ID="lbtnDel" runat="server" CommandName="D" CommandArgument='<%#Eval("id") %>'><%=GetTran("000022", "删除")%></asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;
						<asp:HyperLink id="Hyperlink1" runat="server" NAME="Hyperlink1" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "id", "DeptRoleModify.aspx?id={0:d}") %>'><%=GetTran("000036", "编辑")%></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="name" HeaderText="角色名" >
                </asp:BoundField>
                <asp:BoundField DataField="dept" HeaderText="部门名" >
                </asp:BoundField>
                <asp:BoundField DataField="roleName" HeaderText="直属角色" >
                </asp:BoundField>
                <asp:BoundField DataField="addDate" HeaderText="创建时间" >
                </asp:BoundField>
            </Columns> 
            <EmptyDataTemplate >
                        </td></tr>
                        <tr>
                            <th>
                                角色名
                            </th>
                            <th>
                                部门名
                            </th>
                            <th>
                                直属角色
                            </th>
                            <th>
                                创建时间
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
  
                  <INPUT onclick="javascript:window.location='CompanyDeptManage.aspx'" type="button" value='<%=GetTran("000949", "添加部门")%>' class="another">
                  <INPUT onclick="javascript:window.location='DeptRoleAdd.aspx'" type="button" value='<%=GetTran("000950", "添加角色")%>' class="another">
                  <INPUT onclick="javascript:window.location='ManagerManage.aspx'" type="button" value='<%=GetTran("000325", "添加管理员")%>' class="another">
  </td>
  </tr>
 
</table>

    <div id="cssrain" style="width:100%">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
        <tr>
          <td width="80px"><table width="100%" style="height:28px;" border="0" cellpadding="0" cellspacing="0" id="Table1">
               <tr>
                    <td class="sec2">
                       <td class="sec2"><span id="span1" title='<%=GetTran("000628", "说明")%>' onmouseover="cut1()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span></td>   
                    </td>
                </tr>
          </table></td>
          <td><a href="#"><img src="images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="img1" alt="" onclick="down3()"/></a></td>
        </tr>
      </table>
	  <div id="divTab2">
      <table width="100%" style="height:68px;" border="0" cellspacing="0" class="DMbk" id="Table2">       
        <tbody style="DISPLAY:block" id="tbody1">
          <tr>
            <td style="padding-left:20px"><table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td>
                   1、<%=GetTran("000955", "设置角色使用系统的权限")%>。<br />
                    2、<%=GetTran("000956", "添加新的角色，删除角色")%>。   
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
</html>
