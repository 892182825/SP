<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DefaultDetails.aspx.cs" Inherits="Company_DefaultDetails" %>
<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
    <script src="../JS/QCDS2010.js" type="text/javascript"></script>
    <script src="../JS/SqlCheck.js" type="text/javascript"></script>

    <style type="text/css">
        #secTable
        {
            width: 150px;
        }
    </style>

</head>
<body onload="down2()">
    <form id="form1" runat="server">
    <br />
    <table cellspacing="0" cellpadding="0" border="0" width="100%" class="biaozzi">
        <tr>
            <td>
                <br />
                <table cellspacing="0" cellpadding="0" border="0" width="100%" class="tablemb">
                    <tr>
                        <td valign="top">
                            <asp:GridView ID="GridView1" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False"
                                BorderStyle="Solid">
                                <AlternatingRowStyle BackColor="#F1F4F8" />
                                <HeaderStyle CssClass="tablebt" />
                                <RowStyle HorizontalAlign="Center" />
                                <Columns>
                                    <asp:BoundField DataField="OldId" HeaderText="原编号"></asp:BoundField>
                                    <asp:BoundField DataField="NewId" HeaderText="新编号"></asp:BoundField>
                                    <asp:BoundField DataField="ExpectNum" HeaderText="修改期数"></asp:BoundField>
                                    <asp:TemplateField HeaderText="修改时间">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" 
                                                Text='<%# GetRDate(Eval("UpdateTime")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <table width="100%">
                                        <tr>
                                            <th>
                                            原编号
                                            </th>
                                            <th>新编号</th>
                                            <th>修改期数</th>
                                            <th>修改时间</th>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>                                
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="right">
                <uc1:Pager ID="Pager1" runat="server" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
