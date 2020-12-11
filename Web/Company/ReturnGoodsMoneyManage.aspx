<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReturnGoodsMoneyManage.aspx.cs"
    Inherits="Company_ReturnGoodsMoneyManage" EnableEventValidation="false" %>

<%@ Register Src="../UserControl/Country.ascx" TagName="Country" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <%=GetTran("001964","退货款管理") %></title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>

    <script src="../JS/QCDS2010.js" type="text/javascript"></script>

    <script src="js/tianfeng.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function vali() 
        {
             var ddtime=document.getElementById("ddlItem").value;
              var txvalue=document.getElementById("txtvalue").value;
              
              if(this.ddlItem.SelectedItem.Value == "Client")
              {
                return true;
              }
             if(ddtime=="DocMakeTime" )
             {
                var txtime=document.getElementById("txttime").value;
                if(txtime.length<=0)
                {
                    alert('<%=GetTran("001965","请填写退货时间") %>！');
                    return false;
                 }
                 return true;
             }
             if(txvalue.length<=0)
             {
             alert('<%=GetTran("001968","请填写查询条件") %>');
                    return false;
             }
             return true;

        }
        
        function valireturn()
        {
              var tvali=document.getElementById("txtvali").value;
               var tmoney=document.getElementById("txtmoney").value;
               var lmoney=document.getElementById("labmoney").innetText;
             if(tvali.length<=0)
             {
                 alert('<%=GetTran("001969","请填写验货人") %>！');
                    return false;
             }
              if(tmoney.length<=0)
             {
                 alert('<%=GetTran("001971","请填写退货扣款额") %>！');
                    return false;
             }
              if(tmoney>lmoney)
             {
                 alert('<%=GetTran("001971","请填写退货扣款额") %>！');
                    return false;
             }
             return checkedcf('<%=GetTran("007754","是否确认添加退货扣款单") %>?');
        }
    </script>

    <script type="text/javascript">
		    window.onerror=function()
		    {
		        return true;
		    };
    </script>

    <script language="javascript">
        function cut()
        {
             document.getElementById("span1").title='<%=GetTran("000032", "管 理") %>';
        }
        function cut1()
        {
             document.getElementById("span2").title='<%=GetTran("000033", "说 明") %>';
        }
        window.onload=function()
	    {
	        down2();
	    };
    </script>

    <style type="text/css">
        .style1
        {
            height: 21px;
        }
    </style>

    <script language="javascript" src="../js/SqlCheck.js"></script>

</head>
<body>
    <form id="form1" runat="server" onsubmit="return filterSql_III()">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <br />
    <asp:Panel ID="panelselT" runat="server" Visible="true" Style="white-space: nowrap">
        <table border="0" cellpadding="0" cellspacing="0" class="biaozzi">
            <tr>
                <td align="center">
                    <asp:Button ID="butsele" runat="server" Text="查找" OnClientClick="return vali()" OnClick="butsele_Click"
                        CssClass="anyes" />
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlItem" runat="server" Width="80px" AutoPostBack="true" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged">
                                            <asp:ListItem Value="Client">退货店铺</asp:ListItem>
                                            <asp:ListItem Value="ExpectNum">期数</asp:ListItem>
                                            <asp:ListItem Value="TotalMoney">总价格</asp:ListItem>
                                            <asp:ListItem Value="DocMakeTime">退货日期</asp:ListItem>
                                        </asp:DropDownList>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                        <asp:DropDownList ID="ddllist" Width="80px" runat="server" AutoPostBack="false">
                                        </asp:DropDownList>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                        <asp:TextBox ID="txtvalue" runat="server" Width="120px" Visible="true" MaxLength="50" />
                                        <asp:TextBox ID="txttime" runat="server" Width="120px" CssClass="Wdate" onfocus="WdatePicker()"></asp:TextBox>&nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlstatu" Width="80px" runat="server">
                                            <asp:ListItem Value="0">未退款</asp:ListItem>
                                            <asp:ListItem Value="1">已退款</asp:ListItem>
                                        </asp:DropDownList>
                                        &nbsp;
                                    </td>
                                    <td style="white-space: nowrap">
                                        <span>&nbsp;&nbsp;<%=GetTran("001800", "的退单记录")%>&nbsp;&nbsp;</span>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <br />
    <asp:Panel ID="panelsel" runat="server" Visible="true">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="word-break: keep-all; word-wrap: normal; border: rgb(147,226,244) solid 1px">
                    <asp:GridView ID="gwmoney" runat="server" AutoGenerateColumns="False" OnRowCommand="gwmoney_RowCommand"
                        CssClass="tablemb bordercss" OnRowDataBound="gwmoney_RowDataBound" Width="100%" ShowFooter="true">
                        <AlternatingRowStyle BackColor="#F1F4F8" />
                        <HeaderStyle CssClass="tablebt" />
                        <RowStyle HorizontalAlign="Center" />
                        <Columns>
                            <asp:TemplateField ItemStyle-Wrap="false">
                                <HeaderTemplate>
                                    <span style="white-space: nowrap">审核退货单</span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtflag" CommandName="select" runat="server" EnableViewState="false"
                                        Visible='<%# getValidate(Eval("StateFlag"),Eval("CloseFlag")) %>'><%=GetTran("001924", "填写退货退款单")%></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <span>操作</span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbup" runat="server" Text="修改" Visible="true" CommandName="upmoney"
                                        EnableViewState="false"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <span style="white-space: nowrap">退货单详细</span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <img src="images/fdj.gif" /><asp:LinkButton ID="lbtsele" runat="server" PostBackUrl='<%#Eval("DocID","ReturnGoodsMoneyDetails.aspx?ID={0}") %>'> <%=GetTran("000440", "查看")%></asp:LinkButton>
                                    <asp:HiddenField ID="hdfddocid" runat="server" Value='<%#Eval("ID")%>' />
                                    <asp:HiddenField ID="hdfEddoc" runat="server" Value='<%#Eval("DocID")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="退货店铺" DataField="Client" />
                            <asp:BoundField HeaderText="退货单号" DataField="DocID" />
                            <asp:BoundField HeaderText="期数" DataField="ExpectNum" />
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <span style="white-space: nowrap">是否审核</span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="labstate" runat="server" Text='<%#getstr(Eval("StateFlag")) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <span style="white-space: nowrap">是否失效</span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="labclose" runat="server" Text='<%#getstr(Eval("CloseFlag")) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <span style="white-space: nowrap">是否退款</span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="labFlag" runat="server" Text='<%#getstr(Eval("Flag")) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Right">
                                <HeaderTemplate>
                                    <span style="white-space: nowrap">退货总价</span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <span>
                                        <asp:Label ID="labmon" runat="server" Text='<%#Eval("TotalMoney")%>' /></span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="扣款" DataField="Charged" DataFormatString="{0:n2}" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField HeaderText="退货总积分" DataField="TotalPV" />
                            <asp:TemplateField ItemStyle-Wrap="false">
                                <HeaderTemplate>
                                    <span style="white-space: nowrap">退货日期</span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <span>
                                        <%#Getdate(Eval("DocMakeTime", "{0:d}"))%></span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <span style="white-space: nowrap">查看备注</span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# setstr(Eval("Note"),Eval("DocID")) %>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <table border="1" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <th>
                                        <%=GetTran("001802", "审核退货单")%>
                                    </th>
                                    <th>
                                        <%=GetTran("001806", "退货单详细")%>
                                    </th>
                                    <th>
                                        <%=GetTran("001808", "退货店铺")%>
                                    </th>
                                    <th>
                                        <%=GetTran("001809", "退货单号")%>
                                    </th>
                                    <th>
                                        <%=GetTran("000045", "期数")%>
                                    </th>
                                    <th>
                                        <%=GetTran("000605", "是否审核")%>
                                    </th>
                                    <th>
                                        <%=GetTran("001811", "是否失效")%>
                                    </th>
                                    <th>
                                        <%=GetTran("001988", "是否退款")%>
                                    </th>
                                    <th>
                                        <%=GetTran("001812", "退货总价")%>
                                    </th>
                                    <th>
                                        <%=GetTran("000251", "扣款")%>
                                    </th>
                                    <th>
                                        <%=GetTran("001813", "退货总积分")%>
                                    </th>
                                    <th>
                                        <%=GetTran("001814", "退货日期")%>
                                    </th>
                                    <th>
                                        <%=GetTran("000744", "查看备注")%>
                                    </th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" class="biaozzi">
                        <tr>
                            <td colspan="3">
                                <uc2:Pager ID="Pager1" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="plflag" runat="server" Visible="false" Width="100%">
        <table border="0" cellpadding="0" cellspacing="1" class="tablemb" align="center"
            width="450px">
            <tr>
                <th colspan="2" style="white-space: nowrap">
                    <%=GetTran("001924", "填写退货退款单")%>
                </th>
            </tr>
            <tr>
                <td style="width: 180px" align="right" bgcolor="#EBF1F1">
                    <span>
                        <%=GetTran("001808", "退货店铺")%>：</span>
                </td>
                <td>
                    <asp:Label ID="labstore" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 180px" align="right" bgcolor="#EBF1F1">
                    <span>
                        <%=GetTran("001809","退货单号") %>：</span>
                </td>
                <td>
                    <asp:Label ID="labcard" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 180px" align="right" bgcolor="#EBF1F1">
                    <span>
                        <%=GetTran("001917", "退货款额")%>：</span>
                </td>
                <td>
                    <asp:Label ID="labmoney" runat="server" />
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 180px" style="white-space: nowrap;" align="right" bgcolor="#EBF1F1">
                    <span>
                        <%=GetTran("001915", "验货人")%>：</span>
                </td>
                <td>
                    <asp:TextBox ID="txtvali" runat="server" MaxLength="20" />
                </td>
            </tr>
            <tr>
                <td style="width: 180px" style="white-space: nowrap;" align="right" bgcolor="#EBF1F1">
                    <span>
                        <%=GetTran("001908","退货扣款") %>：</span>
                </td>
                <td>
                    <asp:TextBox ID="txtmoney" Text="0.00" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 180px; word-break: keep-all; word-wrap: normal" align="right" valign="middle"
                    bgcolor="#EBF1F1">
                    <span>
                        <%=GetTran("001903", "验货和扣款原因")%>：</span>
                </td>
                <td>
                    <asp:TextBox ID="txtnote" runat="server" TextMode="MultiLine" Height="80px" MaxLength="250"
                        Width="220px" />
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="2" align="center" width="450px">
            <tr>
                <td align="right">
                    <asp:Button ID="butInventoryDoc" runat="server" Text="确定" OnClientClick="return valireturn()"
                        OnClick="butInventoryDoc_Click" CssClass="anyes" />
                </td>
                <td>
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnret" runat="server" Text="返回" CssClass="anyes" OnClick="btnret_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <div id="cssrain" style="width: 100%">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
            <tr>
                <td width="150">
                    <table width="100%" border="0" height="28" cellpadding="0" cellspacing="0" id="secTable">
                        <tr>
                            <td class="sec2" onclick="secBoard(0)" style="white-space: nowrap;">
                                <span id="span1" title="" onmouseover="cut()">
                                    <%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000032", "管 理"))%></span>
                            </td>
                            <td class="sec1" onclick="secBoard(1)" style="white-space: nowrap;">
                                <span id="span2" title="" onmouseover="cut1()">
                                    <%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <a href="#">
                        <img src="images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX"
                            onclick="down2()" style="vertical-align: middle" /></a>
                </td>
            </tr>
        </table>
        <div id="divTab2">
            <table width="100%" border="0" height="68" cellspacing="0" class="DMbk" id="mainTable">
                <tbody style="display: block" id="tbody0">
                    <tr>
                        <td valign="bottom" style="padding-left: 20px">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="btnEXCEL" runat="server" ImageUrl="images/anextable.gif" OnClick="btnEXCEL_Click"
                                            Style="display: none" />
                                        <a href="#">
                                            <img src="images/anextable.gif" width="49" height="47" border="0" onclick="__doPostBack('btnEXCEL','');" /></a>
                                        &nbsp;&nbsp;&nbsp;&nbsp;
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
                                        <%=GetTran("001899", "1、查看所有的店铺向公司退货的退货单。")%><br />
                                        <%=GetTran("001896", "2、对已经审核的退货单（也就是所退的产品已入库）,进行退款操作，可以全额退款，也可差额退款。")%>
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
