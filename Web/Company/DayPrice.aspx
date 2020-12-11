<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DayPrice.aspx.cs" Inherits="Company_DayPrice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>

    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />

    <script src="../JS/QCDS2010.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../js/SqlCheck.js"></script>
    <script>
    function cut() {
            document.getElementById("span1").title = '管 理';
        }
        function cut1() {
            document.getElementById("span2").title = '说 明';
        }
        </script>
</head>
<body>
    <form id="form1" runat="server">
        <br />
     <div>
       
        <table border="0" cellpadding="0" cellspacing="0" class="biaozzi" style="word-break: keep-all;
            word-wrap: normal">
            <tr>
                <td>
                    <asp:Button ID="BtnConfirm" runat="server" Text="查 询" OnClick="BtnConfirm_Click"
                        CssClass="anyes" Style="cursor: hand;"></asp:Button>
                </td>
                <td>
                    &nbsp;发生时间：
                    <asp:TextBox ID="Datepicker1" runat="server" CssClass="Wdate" onfocus="WdatePicker()"
                        Width="85px"></asp:TextBox>
                </td>
                <td align="left">
                    &nbsp;
                    至&nbsp;
                </td>
                <td>
                    &nbsp;
                    <asp:TextBox ID="Datepicker2" runat="server" CssClass="Wdate" onfocus="WdatePicker()"
                        Width="85px"></asp:TextBox>
                </td>
                
            </tr>
        </table>
        <br />
        <table width="100%" border="0" cellpadding="0" cellspacing="0" id="Table1">
            <tr>
                <td style="word-break: keep-all; word-wrap: normal">
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView2_RowDataBound"
                        Width="100%" CssClass="tablemb">
                        <AlternatingRowStyle BackColor="#F1F4F8" />
                        <HeaderStyle CssClass="tablebt" />
                        <RowStyle HorizontalAlign="Center" />
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="编号" />
                            
                            <asp:TemplateField HeaderText="日期">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# GetHDate(Eval("NowDate")) %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("NowDate") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle Wrap="False" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="价格">
                                <ItemTemplate>
                                    <%#Eval("NowPrice")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="增长率">
                                <ItemTemplate>
                                    <%#Eval(" Addrate")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            
                        </Columns>
                        <EmptyDataTemplate>
                            <table class="tablemb" width="100%">
                                <tr>
                                    <th>
                                        编号
                                    </th>
                                    <th>
                                        日期
                                    </th>
                                    <th>
                                        价格
                                    </th>
                                    <th>
                                        增长率
                                    </th>
                                    
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <table width="99%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
            <tr>
                <td>
                    <uc1:Pager ID="Pager1" runat="server" />
                </td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
            <tr>
                <td class="zihz12">
                </td>
            </tr>
            <tr>
                <td style="height: 22px" class="zihz12">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <div id="cssrain" style="width: 100%">
            <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
                <tr>
                    <td width="150">
                        <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
                            <tr>
                                <td class="sec2" onclick="secBoard(0)">
                                    <span id="span1" title="" onmouseover="cut()">
                                        管 理</span>
                                </td>
                                <td class="sec1" onclick="secBoard(1)">
                                    <span id="span2" title="" onmouseover="cut1()">
                                         说 明</span>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <a href="#">
                            <img src="images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX"
                                onclick="down2()" /></a>
                    </td>
                </tr>
            </table>
            <div id="divTab2">
                <table width="100%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
                    <tbody style="display: block" id="tbody0">
                        <tr>
                            <td valign="bottom" style="padding-left: 20px">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="images/anextable.gif" OnClick="Download_Click" />
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
                                            1、查询石斛积分每天价格历史记录。
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
</html>
