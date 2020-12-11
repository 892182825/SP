<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StrikeBalancesView.aspx.cs" Inherits="Company_StrikeBalancesView" %>


<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>会员信息编辑</title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />

    <script src="../JS/QCDS2010.js" type="text/javascript"></script>

    <script src="../JS/SqlCheck.js" type="text/javascript"></script>
    <script src="js/tianfeng.js" type="text/javascript"></script>
    
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
    function aaa()
    {
        for(var i=0;i<form1.elements.length;i++)
        {
            if(form1.elements[i].type=="text")
            {
                if(form1.elements[i].value.indexOf("'")!=-1||form1.elements[i].value.indexOf("=")!=-1)
                {
                    alert('<%=GetTran("000712", "查询条件里面不能输入特殊字符！")%>');
                    return false;
                }
            }
        }
    }
    	function CheckText(btname)
	{
		//这个方法是页面有多个按钮要提交时，多次使用这个方法,传入按钮的ID
		filterSql_II (btname);
		
	}
    </script>

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
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td align="center">
                            <asp:Label ID="labmx" runat="server" Font-Size="Large"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="white-space: nowrap">
                            <asp:Button ID="BtnConfirm" Style="display: none" runat="server" Text="查 询" OnClick="BtnConfirm_Click"
                                CssClass="anyes"></asp:Button>
                            <asp:LinkButton ID="lkSubmit1" Style="display: none" runat="server" Text="查 询" OnClick="lkSubmit1_Click"></asp:LinkButton>
                            <input class="anyes" id="Button2" onclick="CheckText('lkSubmit1')" type="button" value='<%=GetTran("000048","查 询")%>'/>
                                
                                &nbsp;<%=GetTran("000024", "会员编号")%>：<asp:TextBox
                                    ID="Number" runat="server" Width="80px" MaxLength="10"></asp:TextBox>
                            &nbsp;<%=GetTran("000025", "会员姓名")%>：<asp:TextBox ID="Name" runat="server" Width="80px" MaxLength="50"></asp:TextBox>
                          
                            &nbsp;<%=GetTran("000029", "注册期数")%>：<asp:DropDownList ID="DropDownExpectNum" runat="server" >
                            </asp:DropDownList>&nbsp;&nbsp;
                           <asp:button  id="Button3" 
											 runat="server"  Text="返 回" 
                                        onclick="Button3_Click" CssClass="anyes" style="cursor:hand;"></asp:button>
                        </td>
                    </tr>
                </table>
                <br />
                <table cellspacing="0" cellpadding="0" border="0" width="100%" class="tablemb">
                    <tr>
                        <td valign="top">
                            <asp:GridView ID="GridView1" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False"
                                BorderStyle="Solid" OnRowDataBound="GridView1_RowDataBound"  ShowFooter="true"
                                onrowcommand="GridView1_RowCommand">
                                <AlternatingRowStyle BackColor="#F1F4F8" />
                                <HeaderStyle CssClass="tablebt" />
                                <RowStyle HorizontalAlign="Center" />
                                <FooterStyle HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField HeaderText="会员工资退回">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                        <ItemTemplate>
                                            
                                            <asp:LinkButton ID="linkbtnth" runat="server" CommandName="linkbtnth" CommandArgument='<%#Eval("ExpectNum")+":"+Eval("Number") %>'>会员工资退回</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Number" HeaderText="会员编号"></asp:BoundField>
                                     <asp:BoundField DataField="Name" HeaderText="会员姓名"></asp:BoundField>
                                    <asp:BoundField DataField="ExpectNum" HeaderText="期数"></asp:BoundField>
                                    
                                    <asp:BoundField DataField="CurrentOneMark" HeaderText="新个分数" DataFormatString="{0:f2}"></asp:BoundField>
                                   <%-- <asp:BoundField DataField="Bonus0" HeaderText="无" DataFormatString="{0:f2}"></asp:BoundField>--%>
                                    <asp:BoundField DataField="Bonus1" HeaderText="推荐奖" DataFormatString="{0:f2}"></asp:BoundField>
                                    <asp:BoundField DataField="Bonus2" HeaderText="回本奖" DataFormatString="{0:f2}"></asp:BoundField>
                                     <asp:BoundField DataField="Bonus3" HeaderText="大区奖" DataFormatString="{0:f2}"></asp:BoundField>
                                     <asp:BoundField DataField="Bonus4" HeaderText="小区奖" DataFormatString="{0:f2}"></asp:BoundField>
                                     <asp:BoundField DataField="Bonus5" HeaderText="永续奖" DataFormatString="{0:f2}"></asp:BoundField>
                                     <asp:BoundField DataField="Bonus6" HeaderText="网平台综合管理费" DataFormatString="{0:f2}"></asp:BoundField>
                                     <asp:BoundField DataField="Bonus7" HeaderText="网扣福利奖金" DataFormatString="{0:f2}"></asp:BoundField>
                                     <asp:BoundField DataField="Bonus8" HeaderText="网扣重复消费" DataFormatString="{0:f2}"></asp:BoundField>

                                     <asp:BoundField DataField="DeductMoney" HeaderText="扣款" DataFormatString="{0:f2}"></asp:BoundField>
                                    <asp:BoundField DataField="DeductTax" HeaderText="扣税" DataFormatString="{0:f2}"></asp:BoundField>
                                    <asp:BoundField DataField="Total" HeaderText="总计" DataFormatString="{0:f2}"></asp:BoundField>
                                    <asp:BoundField DataField="bqbukuan" HeaderText="补款" DataFormatString="{0:f2}"></asp:BoundField>
                                    <asp:BoundField DataField="CurrentSolidSend" HeaderText="实发" DataFormatString="{0:f2}"></asp:BoundField>
                                    <asp:TemplateField HeaderText="时间">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" 
                                                Text='<%# GetRDate(Eval("TransferToPurseDate")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("TransferToPurseDate") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <table width="100%">
                                        <tr>
                                            
                                            <th>
                                                <%=GetTran("000024", "会员编号")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000025", "会员姓名")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000045", "期数")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000939", "新个分数")%>
                                            </th>
                                           <%-- <th>
                                                    <%=GetTran("7577","无")%>
                                                </th>--%>
                                                <th>
                                                    <%=GetTran("007578","推荐奖")%>
                                                </th>
                                                <th>
                                                    <%=GetTran("007579","回本奖")%>
                                                </th>
                                                <th>
                                                    <%=GetTran("007580", "大区奖")%>
                                                </th>
                                                <th>
                                                    <%=GetTran("007581", "小区奖")%>
                                                </th>
                                                <th>
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
                                            </th>
                                              <th>
                                                <%=GetTran("000251", "扣款")%>
                                            </th>
                                             <th>
                                                <%=GetTran("000249", "扣税")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000247", "总计")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000252", "补款")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000254", "实发")%>
                                            </th>
                                            <th>
                                                <%=GetTran("001546", "时间")%>
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
    <table width="100%" class="biaozzi">
        <tr>
            <td align="right" colspan="7">
                <uc1:Pager ID="Pager1" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
    </table>
    <div id="cssrain" style="width: 100%">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
            <tr>
                <td width="150">
                    <table width="100%" border="0" height="28" cellpadding="0" cellspacing="0" id="secTable">
                        <tr>
                            <td class="sec2" onclick="secBoard(0)" style="white-space: nowrap;">
                                <span id="span1" title="" onmouseover="cut()">
                                <%=GetTran("000032", "管 理")%></span>
                            </td>
                            <td class="sec1" onclick="secBoard(1)" style="white-space: nowrap;">
                                <span id="span2" title="" onmouseover="cut1()">
                                <%=GetTran("000033", "说 明")%></span>
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
                                        <asp:LinkButton ID="btnDownExcel" runat="server" Text="导出Excel" OnClick="btnDownExcel_Click"
                                            Style="display: none;"></asp:LinkButton><a href="#"><img src="images/anextable.gif"
                                                width="49" height="47" border="0" onclick="__doPostBack('btnDownExcel','');" /></a>
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <!--<a href="#">
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
