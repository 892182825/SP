<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WagesDetail.aspx.cs" Inherits="Company_WagesDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../UserControl/ExpectNum.ascx" TagName="ExpectNum" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>工资查询</title><%--奖金查询--%>

    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>

    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />

    <script src="../JS/QCDS2010.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../js/SqlCheck.js"></script>

    <script src="js/tianfeng.js" type="text/javascript" language="javascript"></script>

    <script language="javascript">
        function cut()
        {
             document.getElementById("span1").title='<%=GetTran("000032", "管 理") %>';
        }
        function cut1()
        {
             document.getElementById("span2").title='<%=GetTran("000033", "说 明") %>';
        }
    </script>

    <style type="text/css">
        .style1
        {
            width: 122px;
        }
    </style>

    <script type="text/javascript">
    	function CheckText(btname)
	{
		//这个方法是页面有多个按钮要提交时，多次使用这个方法,传入按钮的ID
		filterSql_II (btname);
	}
    </script>

</head>
<body onload="down2()">
    <form id="form1" runat="server">
    <br />
    <div>
        <table id="Table2" width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <table id="Table1" border="0" cellpadding="0" cellspacing="0" class="biaozzi" style="word-break: keep-all;
                        word-wrap: normal">
                        <tr>
                            <td>
                                <asp:Button CssClass="anyes" ID="BtnQuery" runat="server" Text="查 询" OnClick="BtnQuery_Click" />
                                &nbsp;<%=GetTran("001195", "编号")%>：
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox1" runat="server" Width="74px"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;<%=GetTran("000201", "请选择查询期数")%>：
                            </td>
                            <td>
                                <uc1:ExpectNum ID="DropDownQiShu" runat="server" IsRun="True" />
                                &nbsp;<%=GetTran("000205", "到")%>&nbsp;<uc1:ExpectNum ID="DropDownQiShu1" runat="server"
                                    IsRun="True" />
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table id="Table3" border="0" cellpadding="0" cellspacing="0" class="biaozzi" width="100%">
                        <tr>
                            <td>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound"
                                    Width="100%" CssClass="tablemb" ShowFooter="True">
                                    <AlternatingRowStyle BackColor="#F1F4F8" />
                                    <HeaderStyle CssClass="tablebt" />
                                    <RowStyle HorizontalAlign="Center" />
                                    <Columns>
                                        <asp:BoundField HeaderText="编号" DataField="number" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                            <HeaderStyle Wrap="False"></HeaderStyle>
                                            <ItemStyle Wrap="False"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="期数" DataField="ExpectNum" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                            <HeaderStyle Wrap="False"></HeaderStyle>
                                            <ItemStyle Wrap="False"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="个人本期消费" ItemStyle-HorizontalAlign="Right" DataField="CurrentOneMark"
                                            ItemStyle-Wrap="false"  DataFormatString="{0:f2}" HeaderStyle-Wrap="false">
                                            <HeaderStyle Wrap="False"></HeaderStyle>
                                            <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="网络本期业绩" ItemStyle-HorizontalAlign="Right" DataField="CurrentTotalNetRecord"
                                            ItemStyle-Wrap="false"  DataFormatString="{0:f2}" HeaderStyle-Wrap="false">
                                            <HeaderStyle Wrap="False"></HeaderStyle>
                                            <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                        </asp:BoundField>
                               <%--         <asp:BoundField HeaderText="无" ItemStyle-HorizontalAlign="Right" DataField="Bonus0"
                                            DataFormatString="{0:f2}" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                            <HeaderStyle Wrap="False"></HeaderStyle>
                                            <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                        </asp:BoundField>--%>
                                        
                                        <asp:HyperLinkField HeaderText="业绩奖" ItemStyle-HorizontalAlign="Right" 
                                             DataNavigateUrlFormatString="../Company/Commended.aspx?qishu={0}&&id={1}" 
                                             DataNavigateUrlFields="ExpectNum,number" DataTextField="Bonus0"
                                             DataTextFormatString="{0:f2}" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                            runat="server">
                                        </asp:HyperLinkField>
                                        
                                       <%-- <asp:BoundField HeaderText="推荐奖" ItemStyle-HorizontalAlign="Right" DataField="Bonus1"
                                            DataFormatString="{0:f2}" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                            <HeaderStyle Wrap="False"></HeaderStyle>
                                            <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                        </asp:BoundField>--%>
                                      <%--  <asp:HyperLinkField HeaderText="回本奖" ItemStyle-HorizontalAlign="Right" 
                                             DataNavigateUrlFormatString="../Company/Backaward.aspx?qishu={0}&&id={1}" 
                                             DataNavigateUrlFields="ExpectNum,number" DataTextField="Bonus2"
                                             DataTextFormatString="{0:f2}" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                            
                                            runat="server">
                                        </asp:HyperLinkField>--%>
                                        <%--<asp:BoundField HeaderText="回本奖" ItemStyle-HorizontalAlign="Right" DataField="Bonus2"
                                            DataFormatString="{0:f2}" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                            <HeaderStyle Wrap="False"></HeaderStyle>
                                            <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                        </asp:BoundField>--%>

                                      <%--  <asp:HyperLinkField HeaderText="大区奖" ItemStyle-HorizontalAlign="Right" 
                                             DataNavigateUrlFormatString="../Company/daqujiang.aspx?qishu={0}&&id={1}" 
                                             DataNavigateUrlFields="ExpectNum,number" DataTextField="Bonus3"
                                             DataTextFormatString="{0:f2}" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                            
                                            runat="server">
                                        </asp:HyperLinkField>--%>
                                        <%--<asp:BoundField HeaderText="大区奖"  ItemStyle-HorizontalAlign="Right"
                                            DataField="Bonus3" DataFormatString="{0:f2}" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                            <HeaderStyle Wrap="False"></HeaderStyle>
                                            <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                        </asp:BoundField>--%>

<%--                                         <asp:HyperLinkField HeaderText="小区奖" ItemStyle-HorizontalAlign="Right" 
                                             DataNavigateUrlFormatString="../Company/xiaoqujiang.aspx?qishu={0}&&id={1}" 
                                             DataNavigateUrlFields="ExpectNum,number" DataTextField="Bonus4"
                                             DataTextFormatString="{0:f2}" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                            runat="server">
                                        </asp:HyperLinkField>--%>
                                        <%--<asp:BoundField HeaderText="小区奖"  ItemStyle-HorizontalAlign="Right"
                                            DataField="Bonus4" DataFormatString="{0:f2}" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                            <HeaderStyle Wrap="False"></HeaderStyle>
                                            <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                        </asp:BoundField>--%>
                                      <%--  <asp:HyperLinkField HeaderText="永续奖" ItemStyle-HorizontalAlign="Right" 
                                             DataNavigateUrlFormatString="../Company/yongxujiang.aspx?qishu={0}&&id={1}" 
                                             DataNavigateUrlFields="ExpectNum,number" DataTextField="Bonus5"
                                             DataTextFormatString="{0:f2}" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                            runat="server">
                                        </asp:HyperLinkField>--%>

                                       <%--   <asp:HyperLinkField HeaderText="进步奖" ItemStyle-HorizontalAlign="Right" 
                                             DataNavigateUrlFormatString="../Company/yongxujiang.aspx?qishu={0}&&id={1}" 
                                             DataNavigateUrlFields="ExpectNum,number" DataTextField="Bonus6"
                                             DataTextFormatString="{0:f2}" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                            runat="server">
                                        </asp:HyperLinkField>--%>
                                        <%--<asp:BoundField HeaderText="永续奖" ItemStyle-HorizontalAlign="Right"
                                            DataField="Bonus5" DataFormatString="{0:f2}" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                            <HeaderStyle Wrap="False"></HeaderStyle>
                                            <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                        </asp:BoundField>--%>
                                         <%--<asp:BoundField HeaderText="网平台综合管理费" ItemStyle-HorizontalAlign="Right"
                                            DataField="Kougl" DataFormatString="{0:f2}" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                            <HeaderStyle Wrap="False"></HeaderStyle>
                                            <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                        </asp:BoundField>
                                         <asp:BoundField HeaderText="网扣福利奖金" ItemStyle-HorizontalAlign="Right"
                                            DataField="Koufl" DataFormatString="{0:f2}" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                            <HeaderStyle Wrap="False"></HeaderStyle>
                                            <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                        </asp:BoundField>
                                         <asp:BoundField HeaderText="网扣重复消费" ItemStyle-HorizontalAlign="Right"
                                            DataField="Koufx" DataFormatString="{0:f2}" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                            <HeaderStyle Wrap="False"></HeaderStyle>
                                            <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                        </asp:BoundField>--%>
                                        
                                        <asp:BoundField HeaderText="总计" ItemStyle-HorizontalAlign="Right" DataField="CurrentTotalMoney"
                                            DataFormatString="{0:f2}" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                            <HeaderStyle Wrap="False"></HeaderStyle>
                                            <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                        </asp:BoundField>
                                        <%--<asp:BoundField HeaderText="扣税" ItemStyle-HorizontalAlign="Right" DataField="DeductTax"
                                            DataFormatString="{0:f2}" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                            <HeaderStyle Wrap="False"></HeaderStyle>
                                            <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                        </asp:BoundField>--%>
                                        <asp:BoundField HeaderText="实发" ItemStyle-HorizontalAlign="Right" DataField="CurrentSolidSend"
                                            DataFormatString="{0:f2}" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                            <HeaderStyle Wrap="False"></HeaderStyle>
                                            <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                        </asp:BoundField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <table class="tablemb" width="100%">
                                            <tr>
                                                <th>
                                                    <%=GetTran("001195", "编号")%>
                                                </th>
                                                <th>
                                                    <%=GetTran("000045", "期数")%>
                                                </th>
                                                <th>
                                                    <%=GetTran("000240", "个人本期消费")%>
                                                </th>
                                                <th>
                                                    <%=GetTran("000241", "网络本期业绩")%>
                                                </th>
                                               <%-- <th>
                                                    <%=GetTran("000242", "新增网络人数")%>
                                                </th>--%>
                                                <%--<th>
                                                    <%=GetTran("7577","无")%>
                                                </th>--%>
                                                <th>
                                                    <%=GetTran("010002","业绩奖")%>
                                                </th>
                                              <%--  <th>
                                                    <%=GetTran("007579","回本奖")%>
                                                </th>--%>
                                              <%--  <th>
                                                    <%=GetTran("007580", "大区奖")%>
                                                </th>
                                                <th>
                                                    <%=GetTran("007581", "小区奖")%>
                                                </th>--%>
                                                <%--<th>
                                                    <%=GetTran("007582", "永续奖")%>
                                                </th>
                                                <th>
                                                    <%=GetTran("001352", "网平台综合管理费")%>
                                                </th>
                                                <th>
                                                   <%=GetTran("001353", "网扣福利奖金")%>
                                                </th>
                                                <th>
                                                   <%=GetTran("001355", "网扣重复消费")%>
                                                </th>--%>
                                                <th>
                                                    <%=GetTran("000247", "总计")%>
                                                </th>
                                               <%-- <th>
                                                    <%=GetTran("000249", "扣税")%>
                                                </th>--%>
                                                <%--<th>
                                                    <%=GetTran("000251", "扣款")%>
                                                </th>
                                                <th>
                                                    <%=GetTran("000252", "补款")%>
                                                </th>--%>
                                                <th>
                                                    <%=GetTran("000254", "实发")%>
                                                </th>
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
    </div>
    <br />
    <div id="cssrain" style="width: 100%">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
            <tr>
                <td width="150">
                    <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
                        <tr>
                            <td class="sec2" onclick="secBoard(0)">
                                <span id="span1" title="" onmouseover="cut()">
                                    <%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000032", "管 理"))%></span>
                            </td>
                            <td class="sec1" onclick="secBoard(1)">
                                <span id="span2" title="" onmouseover="cut1()">
                                    <%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
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
                                        <asp:LinkButton ID="Download" runat="server" Text="导出Excel" OnClick="Download_Click"
                                            Style="display: none;"></asp:LinkButton><a href="#"><img src="images/anextable.gif"
                                                width="49" height="47" border="0" onclick="__doPostBack('Download','');" style="cursor: hand;" /></a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
                                        <%=GetTran("001606", "1、按期数范围查询某会员的各项奖金和业绩。")%>
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
