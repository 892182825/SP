<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BaodanDetail.aspx.cs" Inherits="Company_BaodanDetail" %>
<%@ Register src="../UserControl/Pager.ascx" tagname="Pager" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
    <script src="../JS/QCDS2010.js" type="text/javascript"></script>
    <script src="js/companyview.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <div>
        <table id="table1" cellspacing="0" cellpadding="0" width="100%" border="0" class="biaozzi">
            <tr >
                <td align="center" colspan="3" >
                    <font style="font-weight: bold; font-size: 20px; color: black; font-style: normal"><%=GetTran("000770", "会员销售细表")%></font>
                </td>
            </tr>
            <tr align="right">
                <td width="10%" style="white-space:nowrap" >
                  &nbsp;
                </td>
                <td align="left" width="40px" style=" white-space:nowrap">
                    <asp:Label ID="lbl_number" runat="server">Label</asp:Label>
                </td>                
                <td align="right">
                    &nbsp;
                </td>
            </tr>
            <tr >
                <td align="center" colspan="3" style="white-space:nowrap">
                    <asp:GridView ID="gvInfo" runat="server" Width="100%" AutoGenerateColumns="False"
                        BorderColor="Black" AlternatingRowStyle-Wrap="false" onrowdatabound="gvInfo_RowDataBound" CssClass="tablemb">
                        <HeaderStyle HorizontalAlign="Center" Wrap="false"></HeaderStyle>
                        <Columns>
                            <asp:BoundField DataField="Number" HeaderText="会员编号" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="Name" HeaderText="会员姓名" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="ExpectNum" HeaderText="销售期数" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="PayExpectNum" HeaderText="付款期数" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="OrderId" HeaderText="定单号" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="TotalMoney" HeaderText="金额" ItemStyle-Wrap="false" DataFormatString="{0:f2}"/>
                            <asp:BoundField DataField="TotalPv" HeaderText="积分" ItemStyle-Wrap="false" DataFormatString="{0:f2}" />
                            <asp:BoundField DataField="orderDate" HeaderText="销售日期" DataFormatString="{0:d}" ItemStyle-Wrap="false" />
                        </Columns>
                    </asp:GridView>
                    <asp:Label ID="lbl_message" runat="server"></asp:Label>
                </td>
            </tr>           
        </table>
    </div>
    <uc2:Pager ID="Pager1" runat="server" />
    <br />
    <div style="width:100%">
    <table>
        <tr>
            <td valign="top">
                <div id="cssrain" style="width:100%">
                    <table width="99%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
                        <tr>
                            <td width="150">
                                <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
                                    <tr>
                                       <td class="sec2" onclick="secBoard(0)">
                                            <span id="span1" title="" ><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000032", "管 理"))%></span>
                                        </td>
                                        <td class="sec1" onclick="secBoard(1)">
                                            <span id="span2" title="" ><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <a href="#">
                                    <img src="images/dis.GIF" name="imgX" width="18" height="22" border="0" id="imgX"
                                        onclick="down2()" /></a>
                            </td>
                        </tr>
                    </table>
                     <div id="divTab2" style="display:none;">
            <table width="100%" border="0" height="68" cellspacing="0" class="DMbk" id="Table2">
                <tbody style="display: block" id="tbody0">
                    <tr>
                        <td valign="bottom" style="padding-left: 20px">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                       <asp:ImageButton ID="btnExcel" runat="server" ImageUrl="images/anextable.gif" OnClick="btnExcel_Click" />
                                                                    
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
                                    <tr>
                                                <td>
                                                    １、<%=this.GetTran("000000", "查看会员销售明细")%>
                                                </td>
                                            </tr>
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
</body>
</html>
