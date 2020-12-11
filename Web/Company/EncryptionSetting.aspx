<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EncryptionSetting.aspx.cs" Inherits="Company_EncryptionSetting" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%=GetTran("005187", "数据加密")%></title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <center>
            <table cellspacing="0" cellpadding="0" border="0" width="450px" class="tablemb">
                <tr>
                    <td>
                        <asp:GridView ID="gvSetting" runat="server" ShowHeader="False" Width="100%" AutoGenerateColumns="False">
                            <Columns>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-Width="45%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblKye" runat="server" Text='<%# GetKey(Eval("encryptionkey")) %>'></asp:Label>
                                        <input id="hidKey" type="hidden" runat="server" value='<%# Eval("encryptionkey") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ckbValue" runat="server" 
                                            Checked='<%# GetValue(Eval("encryptionvalue")) %>' Text='<%# GetTran("005822", "是否加密")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td align=center>
                        <asp:Button ID="btnSave" runat="server" Text="确定" CssClass="anyes" 
                            onclick="btnSave_Click" />
                    </td>
                </tr>
            </table>
        </center>
    </div>
    <%=msg %>
    </form>
</body>
</html>
