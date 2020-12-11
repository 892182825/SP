<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorePassReset.aspx.cs" Inherits="Company_StorePassReset" %>

<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/ExpectNum.ascx" TagName="ExpectNum" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>StorePassReset</title>
    <script language="javascript" type="text/javascript" src="../JS/jquery-1.2.6.js"></script>
    <script src="js/companyview.js" type="text/javascript" language="javascript"></script>
    <link type="text/css" rel="Stylesheet" href="CSS/Company.css" />
</head>
<body onload="down2()">
    <form id="form1" runat="server">
    <div>
        <table width="100%" align="center">
            <tr>
                <td nowrap="nowrap">
 <br />
                    <table class="biaozzi">
                        <tr>
                        <td nowrap="nowrap">
                                <asp:Button ID="btnSeach" runat="server" Text="查 询" OnClick="btnSeach_Click"  CssClass="anyes"/>
                            </td>
                            <td nowrap="nowrap">
                                <%=GetTran("000150", "店铺编号")%>：
                            </td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="txtStoreId" runat="server" MaxLength="10" Wrap="False"></asp:TextBox>
                            </td>
                            <td>
                                <%=GetTran("000040", "店铺名称")%>：
                            </td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="txtStoreName" runat="server" MaxLength="25" Wrap="False"></asp:TextBox>
                            </td>
                            <td nowrap="nowrap">
                                <%=GetTran("000045", "期数")%>：
                            </td>
                            <td nowrap="nowrap">
                                <uc2:ExpectNum ID="ExpectNum1" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td colspan="2" align="center" width="100%">
                                <asp:GridView ID="givStoreInfo" Width="100%" runat="server" AutoGenerateColumns="False"
                                    CssClass="tablemb" onrowdatabound="givStoreInfo_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="密码重置" ShowHeader="False" ItemStyle-Wrap="false" ItemStyle-Width="200">
                                            <ItemTemplate>
                                            <a  href="#" onclick="showStoreDetail('<%#DataBinder.Eval(Container.DataItem,"StoreID")%>','2','<%#DataBinder.Eval(Container.DataItem,"Number")%>')"><%# GetTran("000000", "密码重置")%></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Number" HeaderText="会员编号" />
                                        <asp:BoundField DataField="StoreId" HeaderText="店编号" />
                                        <asp:BoundField DataField="Name" HeaderText="店长姓名" />
                                        <asp:BoundField DataField="StoreName" HeaderText="店铺名称" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <table width="100%" class="biaozzi">
                                            <tr>
                                                <th><%# GetTran("000664", "密码重置")%></th>
                                                <th><%# GetTran("000024", "会员编号")%></th>
                                                <th><%# GetTran("000037", "店编号")%></th>
                                                <th><%# GetTran("000039", "店长姓名")%></th>
                                                <th><%# GetTran("000040", "店铺名称")%></th>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblMassage" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <uc1:Pager ID="Pager1" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <div id="cssrain" style="width:100%">
                        <table width="99%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
                            <tr>
                                <td width="150">
                                    <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
                                        <tr>
                                            <td class="sec2" onclick="secBoard(0)">
                                            <span id="span1" title="" onmouseover="cut()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000032", "管 理"))%></span>
                                        </td>
                                        <td class="sec1" onclick="secBoard(1)">
                                            <span id="span2" title="" onmouseover="cut1()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
                                        </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <a href="#">
                                        <img src="images/dis.GIF" name="imgX" width="18" height="22" border="0" id="imgX"
                                            onclick="down2()" style="vertical-align:middle" /></a>
                                </td>
                            </tr>
                        </table>
                        <div id="divTab2" style="display:none;">
                            <table width="99%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
                                <tbody style="display: block" id="tbody0">
                                    <tr>
                                        <td valign="bottom" style="padding-left: 20px">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="btnDownExcel"  runat="server" Text="导出Excel" OnClick="btnDownExcel_Click"
                                                            Style="display: none;"></asp:LinkButton><a href="#"><img src="images/anextable.gif" width="49"
                                                                height="47" border="0" onclick="__doPostBack('btnDownExcel','');" /></a>&nbsp;&nbsp;&nbsp;&nbsp;
                                                       <!-- <a href="#">
                                                            <img src="images/anprtable.gif" width="49" height="47" border="0" /></a>-->
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                                <tbody style="display: none" id="tbody1">
                                    <tr>
                                        <td style="padding-left: 20px">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        1、
                                                        <%= GetTran("000690", "重置店铺的密码，重置后的密码为申请店铺时的会员编号。")%>
                                                    </td>
                                                    
                                                </tr>
                                                <tr style="display:none">
                                                    <td>
                                                        2、
                                                        <%= GetTran("000692", "密码重置：如果该会员有证件号则为证件号码后六位,反之就是该会员的编号.")%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
    <%= msg %>
    
</body>
</html>
