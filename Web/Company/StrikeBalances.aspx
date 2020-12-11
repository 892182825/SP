<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StrikeBalances.aspx.cs" Inherits="Company_StrikeBalances"
    EnableEventValidation="false" %>

<%@ Register Src="../UserControl/ExpectNum.ascx" TagName="ExpectNum" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>工资退回</title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />

    <script src="../JS/QCDS2010.js" type="text/javascript"></script>

    <script src="js/tianfeng.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../js/SqlCheck.js"></script>

    <script language="javascript">
		
		function CheckFrom()
		{
		    var bh=document.getElementById('text_recebh');
		     
		    var validSTR="1234567890";
		   
			for(var i=0;i<bh.value.length;i++)
			{
			  
				if (validSTR.indexOf(bh.value.charAt(i).toLowerCase() )==-1)
				{
					alert('<%=GetTran("001145", "输入数字")%>');
					return false;
				}
			}
          }
		
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

    <style type="text/css">
        .style1
        {
            width: 122px;
        }
    </style>
    </SCRIPT>

    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>

    <script language="javascript" type="text/javascript">

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
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
                        <tr>
                            <td>
                                <asp:Button ID="BtnConfirm" runat="server" Text="查 询" OnClick="BtnConfirm_Click"
                                    CssClass="anyes" Style="cursor: hand;"></asp:Button>
                                &nbsp;<%=GetTran("000024", "会员编号")%>：&nbsp;<asp:TextBox
                                    ID="txt_member" runat="server" Width="80"></asp:TextBox>&nbsp;
                                <%=GetTran("000045", "期数")%>：
                                <uc1:ExpectNum ID="DropDownQiShu" runat="server" />
                                <%=GetTran("000322", "金额")%>：
                                <asp:TextBox ID="text_recebh" runat="server" Width="80"></asp:TextBox>&nbsp;&nbsp;
                                &nbsp;<asp:Button ID="Button2" Text="会员工资退回" runat="server" OnClick="Button2_Click"
                                    CssClass="another" Style="cursor: hand;"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td valign="top" style="word-break: keep-all; word-wrap: normal">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                                    OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" 
                                    CssClass="tablemb" ShowFooter="true">
                                    <RowStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" CssClass="tablebt" />
                                    <AlternatingRowStyle BackColor="#F1F4F8" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="编号" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="删除">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" CommandName="del" CausesValidation="false" ID="Linkbutton2"
                                                    CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID") %>'><%#GetTran("000022", "删除")%></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="会员编号" DataField="Number" />
                                        <asp:BoundField HeaderText="会员姓名" DataField="name" />
                                        <asp:BoundField HeaderText="期数" DataField="ExpectNum" />
                                        <asp:BoundField HeaderText="退回金额" DataField="MoneyNum" DataFormatString="{0:f2}" />
                                        <asp:BoundField HeaderText="退回日期" DataField="StartDate" DataFormatString="{0:d}" />
                                        <asp:TemplateField HeaderText="原因">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <font face="宋体">
                                                    <asp:HyperLink ID="HyperLink1" NAME="HyperLink1" runat="server"><%#GetTran("000440", "查看")%></asp:HyperLink></font>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <table class="tablemb" width="100%">
                                            <tr>
                                                <th style="display: none;">
                                                    <%#GetTran("001195", "编号")%>
                                                </th>
                                                <th>
                                                    <%#GetTran("000022", "删除")%>
                                                </th>
                                                <th>
                                                    <%#GetTran("000024", "会员编号")%>
                                                </th>
                                                <th>
                                                    <%#GetTran("000025", "会员姓名")%>
                                                </th>
                                                <th>
                                                    <%#GetTran("000045", "期数")%>
                                                </th>
                                                <th>
                                                    <%#GetTran("001488", "退回金额")%>
                                                </th>
                                                <th>
                                                    <%#GetTran("001489", "退回日期")%>
                                                </th>
                                                <th>
                                                    <%#GetTran("001490", "原因")%>
                                                </th>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td height="35">
                                <uc2:Pager ID="Pager1" runat="server" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
                        <tr>
                            <td height="36">
                                <asp:LinkButton ID="Button1" Text="导出EXECL" runat="server" OnClick="Button1_Click"
                                    Style="display: none;"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <br />
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
                            onclick="down2()" /></a>
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
                                        <a href="#">
                                            <img src="images/anextable.gif" width="49" height="47" border="0" onclick="__doPostBack('Button1','');"
                                                style="cursor: hand;" /></a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
                                        <%=GetTran("001492", "１ 、从工资汇兑中把会员的奖金从会员电子账记户中发放会员的银行中或现金给会员，因某种原因会员并没收到，公司实际也没有发出的，将这金额仍然放回到会员的电子账户中去。")%>
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
