<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ModifyOldConsume.aspx.cs" Inherits="Company_ModifyOldConsume" %>

<%@ Register Src="../UserControl/ExpectNum.ascx" TagName="ExpectNum" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>报单浏览</title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
    <script language="javascript" type="text/javascript" src="../JS/jquery-1.2.6.js"></script>
    <script language="javascript" type="text/javascript" src="../JS/QCDS2010.js"></script>
    <script language="javascript" type="text/javascript" src="../javascript/ManagementVsExplanation.js"></script>
    <script src="../JS/SqlCheck.js" type="text/javascript"></script>
  <script type="text/javascript">
	function down2()
	{
		if(document.getElementById("divTab2").style.display=="none")
		{
			document.getElementById("divTab2").style.display="";
			document.getElementById("imgX").src="images/dis1.GIF";
		}
		else
		{
			document.getElementById("divTab2").style.display="none";
			document.getElementById("imgX").src="images/dis.GIF";
		}
	}
    </script>
    <script language="javascript" type="text/javascript">
function secBoard(n)
{
      var tdarr=document.getElementById("secTable").getElementsByTagName("td");

        for(var i=0;i<tdarr.length;i++)
        {
            tdarr[i].className="sec1";
        }
        tdarr[n].className="sec2";

        var tbody0=document.getElementById("tbody0");
        tbody0.style.display="none";
        var tbody1=document.getElementById("tbody1");
        tbody1.style.display="none";

        document.getElementById("tbody"+n).style.display="block";
  }
    </script>
    <script type="text/javascript">
	 $(document).ready(function(){
			if($.browser.msie && $.browser.version == 6) {
				FollowDiv.follow();
			}
	 });
	 FollowDiv = {
			follow : function(){
				$('#cssrain').css('position','absolute');
				$(window).scroll(function(){
				    var f_top = $(window).scrollTop() + $(window).height() - $("#cssrain").height() - parseFloat($("#cssrain").css("borderTopWidth")) - parseFloat($("#cssrain").css("borderBottomWidth"));
					$('#cssrain').css( 'top' , f_top );
				});
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
<body  onload="down2()" >
    <form id="form1" runat="server">
    <br />
    <table width="99%" border="0" cellpadding="0" cellspacing="0" style="word-break: keep-all;word-wrap: normal;" class="biaozzi">
        <tr>
            <td>
                    <asp:LinkButton ID="lkSubmit1" Style="display: none" runat="server" Text="查 询" OnClick="btnSearch_Click"></asp:LinkButton>
                            <input class="anyes" id="Button2" onclick="CheckText('lkSubmit1')" type="button" value='<%=GetTran("000048","查 询")%>'></input>
                <asp:DropDownList ID="ExpectNum1" runat="server">
                </asp:DropDownList>
                <%=this.GetTran("000719", "并且")%>
                <asp:DropDownList ID="ddlContion" runat="server" Style="word-break: keep-all; word-wrap: normal;"
                    AutoPostBack="True" OnSelectedIndexChanged="ddlContion_SelectedIndexChanged">
                    <asp:ListItem Value="B.error">错误信息</asp:ListItem>
                    <asp:ListItem Value="B.StoreId">店铺编号</asp:ListItem>
                    <asp:ListItem Value="A.Number" Selected="True">会员编号</asp:ListItem>
                    <asp:ListItem Value="A.Name">会员姓名</asp:ListItem>
                    <asp:ListItem Value="B.OrderID">订单号</asp:ListItem>
                    <asp:ListItem Value="B.TotalMoney">金额</asp:ListItem>
                    <asp:ListItem Value="B.TotalPv">积分</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="ddlcompare" runat="server">
                </asp:DropDownList>
                <asp:TextBox ID="txtContent" runat="server"></asp:TextBox><%=this.GetTran("000731", "的报单")%>
            </td>
        </tr>
    </table>
    <br />
        <table width="99%" style="word-break: keep-all; word-wrap: normal;">
            <tr>
                <td style="border: rgb(147,226,244) solid 1px">
                    <asp:GridView ID="gv_browOrder" runat="server" AutoGenerateColumns="False" OnRowDataBound="gv_browOrder_RowDataBound"
                        OnRowCommand="gv_browOrder_RowCommand" CssClass="tablemb bordercss" Width="100%">
                        <Columns>
                            <asp:BoundField HeaderText="错误信息" DataField="error" Visible="false">
                                <ItemStyle Wrap="false" />
                            </asp:BoundField>
                            <asp:TemplateField Visible="false">
                                <HeaderTemplate>
                                    确认
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkbtnOk" runat="server" Text="确认" CommandName="OK" Visible='<%# Eval("DefrayState").ToString()=="0"&&Eval("OrderType").ToString().Trim()!="4" %>'
                                        CommandArgument='<%#Eval("OrderID")+":"+Eval("Number")+":"+Eval("defraytype")+":"+Eval("DefrayState")+":"+Eval("orderExpectNum")+":"+Eval("isAgain")+":"+Eval("OStoreID") %>'
                                        OnClientClick="return confirm('您确定要审核吗?')"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    修改
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkbtnModify" runat="server" CommandName="M" Visible='<%#Eval("DefrayType").ToString()=="1"%>'
                                        OnCommand="linkbtnModify_Click" CommandArgument='<%#Eval("OrderID")+":"+Eval("Number")+":"+Eval("defraytype")+":"+Eval("DefrayState")+":"+Eval("orderExpectNum")+":"+Eval("isAgain")+":"+Eval("OStoreID") %>'><%=this.GetTran("000259", "修改")%></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <HeaderTemplate>
                                    删除
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkbtnDelete" runat="server" CommandName="D" Visible='<%#(Eval("OrderExpectNum").ToString()==maxExpect.ToString()&&Eval("defraytype").ToString()=="1")||Eval("defraytype").ToString()=="0" %>'
                                        CommandArgument='<%#Eval("OrderID")+":"+Eval("Number")+":"+Eval("defraytype")+":"+Eval("DefrayState")+":"+Eval("orderExpectNum")+":"+Eval("isAgain")+":"+Eval("OStoreID") %>'><%=this.GetTran("000022", "删除")%></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="会员编号" DataField="number"></asp:BoundField>
                            <asp:TemplateField HeaderText="会员姓名">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# GetName(Eval("name")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="报单店铺编号" DataField="OStoreId"></asp:BoundField>
                            <asp:BoundField HeaderText="报单类型" DataField="orderType"></asp:BoundField>
                            <asp:BoundField HeaderText="支付状态" DataField="PayStatus"></asp:BoundField>
                            <asp:BoundField HeaderText="支付方式" DataField="defrayname"></asp:BoundField>
                            <asp:TemplateField HeaderText="发货方式">
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# GetSendWay(DataBinder.Eval(Container.DataItem,"SendWay").ToString()) %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="期数" DataField="orderExpectNum"></asp:BoundField>
                            <asp:BoundField HeaderText="审核期数" DataField="PayExpectNum"></asp:BoundField>
                            <asp:BoundField HeaderText="订单号" DataField="OrderId"></asp:BoundField>
                            <asp:TemplateField HeaderText="金额">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotalMoney" CssClass="lab" name="lblTotalMoney" runat="server" Text='<%# Eval("totalMoney", "{0:n2}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="积分" DataField="totalPv" ItemStyle-CssClass="lab1" DataFormatString="{0:n2}" HtmlEncode="False">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="注册日期" DataField="RegisterDatec" DataFormatString="{0:yyyy-MM-dd}">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="备注" DataField="remark" Visible="false"></asp:BoundField>
                            <asp:TemplateField HeaderText="备注">
                                <ItemTemplate>
                                    <%# "<span title='"+Eval("remark").ToString()+"'>"+Eval("remark").ToString().Substring(0, (Eval("remark").ToString().Length > 5) ? 5 : Eval("remark").ToString().Length)+"</span>" %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    查看
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <img src="images/fdj.gif" /> <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%#Eval("OrderId") %>'
                                        CommandName='<%#Eval("OStoreId") %>' OnCommand="LinkButton1_Command"> <%=this.GetTran("000440", "查看")%></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <table width="100%">
                            <tr>
                                <th>
                                    <%=this.GetTran("000259", "修改")%>
                                </th>
                                <th>
                                    <%=this.GetTran("000022", "删除")%>
                                </th>
                                <th>
                                    <%=this.GetTran("000024", "会员编号")%>
                                </th>
                                <th>
                                    <%=this.GetTran("000025", "会员姓名")%>
                                </th>
                                <th>
                                    <%=this.GetTran("000030", "店铺编号")%>
                                </th>
                                <th>
                                    <%=this.GetTran("000455", "报单类型")%>
                                </th>
                                <th>
                                    <%=this.GetTran("000775", "支付状态")%>
                                </th>
                                <th>
                                    <%=this.GetTran("000186", "支付方式")%>
                                </th>
                                            <th>
                                                <%=GetTran("001345", "发货方式")%>
                                            </th>
                                <th>
                                    <%=this.GetTran("000045", "期数")%>
                                </th>
                                <th>
                                    <%=this.GetTran("000780", "审核期数")%>
                                </th>
                                <th>
                                    <%=this.GetTran("000079", "订单号")%>
                                </th>
                                <th>
                                    <%=this.GetTran("000322", "金额")%>
                                </th>
                                <th>
                                    <%=this.GetTran("000414", "积分")%>
                                </th>
                                <th>
                                    <%=this.GetTran("000057", "注册日期")%>
                                </th>
                                <th>
                                    <%=this.GetTran("000078", "备注")%>
                                </th>
                                <th>
                                    <%=this.GetTran("000440", "查看")%>
                                </th>
                            </tr>
                         </table>
                        </EmptyDataTemplate>
                        <AlternatingRowStyle BackColor="#F1F4F8" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <div>
        <span style="font-size:12px; margin-left:28px; float:left;">总计 </span> <span style="font-size:12px; float:right;"> 本页金额合计：<asp:Label ID="lab_bjehj" ForeColor="red" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;本页积分合计：<asp:Label ID="lab_bjfhj" runat="server" ForeColor="Red"></asp:Label>&nbsp;&nbsp;&nbsp;查询金额总计：<asp:Label ID="lab_cjezj" runat="server" ForeColor="Red"></asp:Label>&nbsp;&nbsp;&nbsp;查询积分总计：<asp:Label ID="lab_cjfzj" runat="server" ForeColor="Red"></asp:Label></span>
     </div>
        <table width="100%">
            <tr>
                <td align="right">
                    <uc2:Pager ID="Pager1" runat="server" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
            </tr>
        </table> 
        <br />
    
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
                                        <asp:LinkButton ID="btnDownExcel" runat="server" Text="导出Excel" OnClick="btnDownExcel_Click"
                                            Style="display: none;"></asp:LinkButton><a href="#"><img src="images/anextable.gif"
                                                width="49" height="47" border="0" onclick="__doPostBack('btnDownExcel','');" /></a>
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
                                        １、<%=this.GetTran("006931", "查看除当前期外，会员的所有报单；")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        ２、<%=this.GetTran("006932", "修改会员的报单（仅限非当前期的报单）。")%>
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
<script type="text/javascript" language="javascript">
    

    window.onload = function heji() {
        var lab = 0;
        var lab1 = 0;
        $('.lab').each(
        function() {
            lab = parseFloat($(this).text().replace(',', '')) + lab;
        }

    );
        $('#lab_bjehj').html(lab == 0 ? "0" : lab);
        $('.lab1').each(
        function() {
            lab1 = parseFloat($(this).text().replace(',', '')) + lab1;
        }
    );
        $('#lab_bjfhj').html(lab1 == 0 ? "0" : lab1);
    };
</script>