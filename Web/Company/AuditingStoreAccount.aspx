<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AuditingStoreAccount.aspx.cs"
    Inherits="Company_AuditingStoreAccount" EnableEventValidation="false" %>

<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/PagerTwo.ascx" TagName="Pager1" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />

    <script src="../JS/QCDS2010.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../js/SqlCheck.js"></script>

    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />

    <script src="js/tianfeng.js" type="text/javascript"></script>
    <script type="text/javascript">
        window.onerror=function ()
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
    </script>

    
    <script language="javascript" type="text/javascript">
    	function CheckText(btname)
	{
		//这个方法是页面有多个按钮要提交时，多次使用这个方法,传入按钮的ID
		filterSql_II (btname);
		
	}
	
	//有疑问  没有地方调用
	function ShowPhone(sender,id,type)
	{
	    //弹出层
        document.getElementById("divShowPhone").style.display = "block";
        var leftpos = 0,toppos = 0;
        var pObject = sender.offsetParent;
        if (pObject)
        {
            leftpos += pObject.offsetLeft;
            toppos += pObject.offsetTop;
        }
        while(pObject=pObject.offsetParent )
        {
            leftpos += pObject.offsetLeft;
            toppos += pObject.offsetTop;
        };
        
        

        document.getElementById("divShowPhone").style.left = (sender.offsetLeft + leftpos - parseInt(document.getElementById("divShowPhone").style.width.replace("px"))) + "px";
        document.getElementById("divShowPhone").style.top = (sender.offsetTop + toppos + sender.offsetHeight - 2) + "px";
        
       //显示树信息
       document.getElementById("divShowPhone").innerHTML="";
       
       if(id=="")
       {    
            document.getElementById("divShowPhone").style.display="none";
            return;                        
       }
       else
       {
           var result=AjaxClass.GetStorePhones(id,type).value;
           document.getElementById("divShowPhone").innerHTML=result;
       }
	}
	
    function HidePhoneDiv(sender)
    {
        document.getElementById("divShowPhone").style.display = "none";
    }
    </script>

</head>
<body onload="down2()">
    <form id="form1" runat="server">
    <br />
    <div width="100%">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td valign="top">
                    <div>
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi" style="word-break: keep-all;
                            word-wrap: normal">
                            <tr>
                                <td align="left" style="white-space: nowrap">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Button ID="BtnConfirm" runat="server" Text="查 询" OnClick="BtnConfirm_Click"
                                                    CssClass="anyes" Style="cursor: hand;"></asp:Button>
                                                
                                                <%=GetTran("000303","请选择")%>：
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropDownList1" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <%=GetTran("000719", "并且")%>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddl_type" runat="server" AutoPostBack="true" 
                                                    onselectedindexchanged="ddl_type_SelectedIndexChanged">
                                                    <asp:ListItem Value="-1" Selected="True">全部</asp:ListItem>
                                                    <asp:ListItem Value="remittances.RemitStatus=0">服务机构</asp:ListItem>
                                                    <asp:ListItem Value="remittances.RemitStatus=1">会员</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropDownCate" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownCate_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropDownListCondition" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox1" runat="server" MaxLength="50"></asp:TextBox>
                                            </td>
                                            <td>
                                                （<%=GetTran("000733", "不填为任意")%>）
                                            </td>
                                            <td>
                                                <span>
                                                    <%=GetTran("000719", "并且")%></span><%=GetTran("000732", "汇款日期在")%>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Datepicker1" runat="server" CssClass="Wdate" onfocus="WdatePicker()"
                                                    Width="85px"></asp:TextBox>
                                            </td>
                                            <td align="left">
                                                &nbsp;
                                                <%=GetTran("000068", "至")%>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Datepicker2" runat="server" CssClass="Wdate" onfocus="WdatePicker()"
                                                    Width="85px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <%=GetTran("000734", "的记录")%>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label2" runat="server"></asp:Label><span> </span>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <div runat="server" id="div2">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" id="Table1">
                                <tr>
                                    <td style="word-break: keep-all; word-wrap: normal">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand"
                                            OnRowDataBound="GridView1_RowDataBound" Width="100%" CssClass="tablemb" ShowFooter="true">
                                            <AlternatingRowStyle BackColor="#F1F4F8" />
                                            <HeaderStyle CssClass="tablebt" Wrap="false" />
                                            <RowStyle HorizontalAlign="Center" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="核实" ItemStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton1" Visible="false" runat="server" CommandName="Lbtn" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.Id")+","+DataBinder.Eval(Container, "DataItem.ConfirmType")%>'><%#GetTran("000644", "核实")%></asp:LinkButton>&nbsp;
                                                        <input id="ConfirmType" type="hidden" value='<%# DataBinder.Eval(Container, "DataItem.ConfirmType")%>'
                                                            name="Hidden3" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="False"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="RemitNumber" HeaderText="编号" />
                                                <asp:TemplateField HeaderText="姓名">
                                                    <ItemTemplate>
                                                        <%# GetName(Eval("RemitStatus").ToString(), Eval("RemitNumber").ToString())%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="汇款类型">
                                                    <ItemTemplate>
                                                        <%# Eval("RemitStatus").ToString() == "1" ? GetTran("000599", "会员") :Eval("RemitStatus").ToString() == "2"?GetTran("000599", "会员"): GetTran("000388","服务机构")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="状态">
                                                    <ItemTemplate>
                                                        <%# bool.Parse(Eval("IsGSQR").ToString()) == false ? GetTran("001009", "未审核") : GetTran("001011", "已审核")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="联系电话">
                                                    <ItemTemplate>
                                                        <%# GetMobile(Eval("RemitStatus").ToString(), Eval("RemitNumber").ToString())%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="RemitMoney" HeaderText="汇款金额" DataFormatString="{0:f2}"
                                                    ItemStyle-HorizontalAlign="Right">
                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                </asp:BoundField>
                                                 <asp:TemplateField HeaderText="付款方式">
                                                    <ItemTemplate>
                                                        <%# GetPayWay(Eval("PayWay").ToString()) %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="确认方式">
                                                    <ItemTemplate>
                                                        <%# GetConfirmType(Eval("ConfirmType").ToString())%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="付款用途">
                                                    <ItemTemplate>
                                                        <%# GetUse(Eval("Use").ToString()) %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="登记时间">
                                                    <ItemTemplate>
                                                        <%#DateTime.Parse(Eval("RemittancesDate").ToString()).AddHours(Convert.ToDouble(Session["WTH"])).ToString("yyyy-MM-dd")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="审核时间">
                                                    <ItemTemplate>
                                                        <%#DateTime.Parse(Eval("ReceivablesDate").ToString()).AddHours(Convert.ToDouble(Session["WTH"]))%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="PayexpectNum" HeaderText="审核期数" />
                                                <asp:TemplateField HeaderText="备注">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <img src="images/fdj.gif" /><%#SetVisible(DataBinder.Eval(Container.DataItem, "Remark").ToString(), DataBinder.Eval(Container.DataItem, "id").ToString())%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="财务备注">
                                                    <ItemTemplate>
                                                    
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="删除" Visible="false" ItemStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkBtnDelete" runat="server" CommandName="Del" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.Id")%>'><%#GetTran("000022", "删除")%></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="False"></ItemStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <table class="tablemb" width="100%">
                                                    <tr>
                                                        <th style="display:none">
                                                            <%=GetTran("000644", "核实")%>
                                                        </th>
                                                        <th>
                                                            <%=GetTran("001195", "编号")%>
                                                        </th>
                                                        <th>
                                                            <%=GetTran("000107", "姓名")%>
                                                        </th>
                                                        <th>
                                                            <%=GetTran("001593", "状态")%>
                                                        </th>
                                                        <th>
                                                            <%=GetTran("000115", "联系电话")%>
                                                        </th>
                                                        <th>
                                                            <%=GetTran("001970", "汇款金额")%>
                                                        </th>
                                                         <th>
                                                            <%=GetTran("000698", "付款方式")%>
                                                        </th>
                                                         <th>
                                                            <%=GetTran("000595", "确认方式")%>
                                                        </th>
                                                        <th>
                                                            <%=GetTran("000738", "付款用途")%>
                                                        </th> 
                                                        <th>
                                                            <%=GetTran("007257", "登记时间")%>
                                                        </th>
                                                        <th>
                                                            <%=GetTran("001155", "审核时间")%>
                                                        </th>
                                                        <th>
                                                            <%=GetTran("000780", "审核期数")%>
                                                        </th>
                                                        <th>
                                                            <%=GetTran("000078", "备注")%>
                                                        </th>
                                                        <th>
                                                            <%=GetTran("007258", "财务备注")%>
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
                                    <td colspan="5">
                                        <uc2:Pager1 ID="Pager11" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <br />
                </td>
            </tr>
        </table>
    </div>
    <div id="cssrain" style="width: 100%">
        <table width="99%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
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
                            onclick="down2()" style="vertical-align: middle" /></a>
                </td>
            </tr>
        </table>
        <div id="divTab2">
            <table width="99%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
                <tbody style="display: block" id="tbody0">
                    <tr>
                        <td valign="bottom" style="padding-left: 20px">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="ImageButton1" runat="server" OnClick="Button1_Click" ImageUrl="images/anextable.gif" />
                                        
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
                                        1、<%=GetTran("007742", "对服务机构和会员汇款申报的删除、修改和查看。")%>
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