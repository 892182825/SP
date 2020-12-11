<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BrowseBills.aspx.cs" Inherits="Company_BrowseBills" %>

<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/Country.ascx" TagName="Country" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="CSS/Company.css" />

    <script language="javascript" type="text/javascript" src="../javascript/Mymodify.js"></script>

    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>

    <script src="../JS/QCDS2010.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function check(obj) {
            if (obj.value == "不限") {
                obj.value = "";
            }
        }
        function check2(obj) {
            if (obj.value == "") {
                obj.value = "不限";
            }
        }
    </script>

    <style type="text/css">
        td
        {
            word-wrap: normal;
            white-space: nowrap;
        }
    </style>

    <script language="javascript" src="../js/SqlCheck.js"></script>

    <script language="javascript" type="text/javascript">
        function CheckText() {
            filterSql();
        }
    </script>

    <script type="text/javascript">
        function down2() {
            if (document.getElementById("divTab2").style.display == "none") {
                document.getElementById("divTab2").style.display = "";
                document.getElementById("imgX").src = "images/dis1.GIF";

            }
            else {
                document.getElementById("divTab2").style.display = "none";
                document.getElementById("imgX").src = "images/dis.GIF";
            }
        }
    </script>

    <script language="javascript" type="text/javascript">
        function secBoard(n) {
            var tdarr = document.getElementById("secTable").getElementsByTagName("td");

            for (var i = 0; i < tdarr.length; i++) {
                tdarr[i].className = "sec1";
            }
            tdarr[n].className = "sec2";

            var tbody0 = document.getElementById("tbody0");
            tbody0.style.display = "none";
            var tbody1 = document.getElementById("tbody1");
            tbody1.style.display = "none";


            document.getElementById("tbody" + n).style.display = "block";
        }
    </script>

    <script type="text/javascript">
        $(document).ready(function() {
            if ($.browser.msie && $.browser.version == 6) {
                FollowDiv.follow();
            }
        });
        FollowDiv = {
            follow: function() {
                $('#cssrain').css('position', 'absolute');
                $(window).scroll(function() {
                    var f_top = $(window).scrollTop() + $(window).height() - $("#cssrain").height() - parseFloat($("#cssrain").css("borderTopWidth")) - parseFloat($("#cssrain").css("borderBottomWidth"));
                    $('#cssrain').css('top', f_top);
                });
            }
        }
    </script>

    <script language="javascript">
        function cut() {
            document.getElementById("span1").title = '<%=GetTran("000032", "管 理") %>';
        }
        function cut1() {
            document.getElementById("span2").title = '<%=GetTran("000033", "说 明") %>';
        }
                window.onload=function()
	    {
	        down2();
	    };
	    function bSubmit_onclick() {

	    }
    </script>

</head>
<body>
    <form id="form1" runat="server"> 
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <div>
       
        <br>
        <table class="biaozzi" runat="server" id="vistable">
            <tr>
                <td nowrap="nowrap">
                    <asp:LinkButton ID="lkSubmit" Style="display: none" runat="server" Text="提交" OnClick="lkSubmit_Click"></asp:LinkButton>
                    <input class="anyes" id="bSubmit" onclick="CheckText()" type="button" value='<%=GetTran("000440", "查看")%>' onclick="return bSubmit_onclick()"></input>
                    <asp:Button ID="btnSeach" Style="display: none" runat="server" Text="查 询" OnClick="btnSeach_Click"
                        CssClass="anyes" Height="24px" />&nbsp;&nbsp;<%=GetTran("001010", "所用类型")%>：
                </td>
                <td nowrap="nowrap">
                    <asp:DropDownList ID="ddltype" runat="server" Width="75px">
                    </asp:DropDownList>
                </td>
                <td align="right">
                    <%=GetTran("000356", "仓 库")%>：&nbsp;</td>
                <td nowrap="nowrap">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlcangku" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlcangku_SelectedIndexChanged"></asp:DropDownList>
                            <%=GetTran("000358", "库 位")%>：                
                            <asp:DropDownList ID="ddlkuwei" runat="server"></asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td nowrap="nowrap">
                 <%=GetTran("004139", "开出人")%>：
                    <asp:TextBox ID="txtkaichuren" runat="server" onfocus="check(this);" onblur="check2(this);"
                        MaxLength="50" Width="71px">不限</asp:TextBox>
                </td>
                <td align="right">
                    &nbsp;<%=GetTran("001019", "开出日期")%>：<asp:DropDownList ID="ddlkuaichuDatetype" runat="server"
                        Width="89px">
                        <asp:ListItem Selected="True" Value="0">不限</asp:ListItem>
                        <asp:ListItem Value=">">大于</asp:ListItem>
                        <asp:ListItem Value="<">小于</asp:ListItem>
                        <asp:ListItem Value="=">等于</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td nowrap="nowrap">
                    <asp:TextBox ID="txtkaichurenTime" runat="server" class="Wdate" onfocus="new WdatePicker()"
                        Width="81px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <%--<asp:Button ID="Button3" runat="server" onclick="Button3_Click" Text="Button" />--%>
                    <asp:Button ID="Button3" runat="server" Text="全 部" OnClick="Button3_Click" CssClass="anyes" />
                    <%=GetTran("000986", "单据状态")%>：
                </td>
                <td nowrap="nowrap">
                    <asp:DropDownList ID="ddlState" runat="server">
                        <asp:ListItem Selected="True" Value="-1">不限</asp:ListItem>
                        <asp:ListItem Value="0">未审核</asp:ListItem>
                        <asp:ListItem Value="1">已审核</asp:ListItem>
                        <asp:ListItem Value="3">有效</asp:ListItem>
                        <asp:ListItem Value="4">无效</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="right">
                    <%=GetTran("000308", "选择国家")%>：
                </td>
                <td>
                    <uc2:Country ID="Country1" OnCSelectedIndexChanged="Country1_SelectedIndexChanged"
                        runat="server" style="width: 89px" />
                </td>
                
                <td>
                    <%=GetTran("000655", "审核人")%>：
                    <asp:TextBox ID="txtshenheren" runat="server" onfocus="check(this);" onblur="check2(this);"
                        Width="71px" MaxLength="50">不限</asp:TextBox>
                </td>
               
                <td nowrap="nowrap">
                    &nbsp;<%=GetTran("001155", "审核时间")%>：<asp:DropDownList ID="ddlshenheshijitype" runat="server"
                        Width="89px">
                        <asp:ListItem Selected="True" Value="0">不限</asp:ListItem>
                        <asp:ListItem Value=">">大于</asp:ListItem>
                        <asp:ListItem Value="<">小于</asp:ListItem>
                        <asp:ListItem Value="=">等于</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="txtshenherentime" runat="server" CssClass="Wdate" onfocus="new WdatePicker()"
                        Width="81px"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table width="100%" runat="server" id="vistable1">
            <tr>
                <td style="border:rgb(147,226,244) solid 1px">
                    <asp:GridView ID="givDoc" runat="server" AutoGenerateColumns="False" Width="100%"
                        OnRowCommand="givDoc_RowCommand" CssClass="tablemb bordercss" OnRowDataBound="givDoc_RowDataBound"
                        AlternatingRowStyle-Wrap="False" FooterStyle-Wrap="False" HeaderStyle-Wrap="False"
                        PagerStyle-Wrap="False" SelectedRowStyle-Wrap="False">
                        <EmptyDataTemplate>
                            <table class="tablebt" width="100%">
                                <tr>
                                    <th>
                                        <%=GetTran("000399", "查看详细")%>
                                    </th>
                                    <th>
                                        <%=GetTran("001149", "打印")%>
                                    </th>
                                    <th>
                                        <%=GetTran("001151", "单据类型")%>
                                    </th>
                                    <th>
                                        <%=GetTran("000407", "单据编号")%>
                                    </th>
                                    <th>
                                        <%=GetTran("001153", "开出时间")%>
                                    </th>
                                    <th>
                                        <%=GetTran("000519", "开出人")%>
                                    </th>
                                    <th>
                                        <%=GetTran("000041", "总金额")%>
                                    </th>
                                    <%--<th>
                                        <%=GetTran("000562", "币种")%>
                                    </th>--%>
                                    <th>
                                        <%=GetTran("000414", "积分")%>
                                    </th>
                                    <th>
                                        <%=GetTran("000045", "期数")%>
                                    </th>
                                    <th>
                                        <%=GetTran("000655", "审核人")%>
                                    </th>
                                    <th>
                                        <%=GetTran("001155", "审核时间")%>
                                    </th>
                                    <th>
                                        <%=GetTran("000744", "查看备注")%>
                                    </th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <FooterStyle Wrap="False"></FooterStyle>
                        <Columns>
                         <asp:TemplateField>
                                <ItemStyle Wrap="false" HorizontalAlign="center" />
                                <HeaderStyle Wrap="false" />
                                <HeaderTemplate>
                                    <span>查看详细</span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <img src="images/fdj.gif" /><asp:LinkButton ID="Button1" runat="server" CausesValidation="false" CommandName="Details"
                                        CommandArgument='<%#Eval("DocID") %>'><%=GetTran("000399", "查看详细")%></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="打印" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="Button2" runat="server" CausesValidation="false" CommandName="Print"
                                        CommandArgument='<%#Eval("DocID") %>'><%=GetTran("001149", "打印")%></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" HorizontalAlign="center" />
                                <HeaderStyle Wrap="false" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="单据类型">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%#Type(Eval("DocTypeID")) %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" HorizontalAlign="center" />
                                <HeaderStyle Wrap="false" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Wrap="false" HorizontalAlign="center" />
                                <HeaderStyle Wrap="false" />
                                <HeaderTemplate>
                                    <span>单据编号</span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <%# Eval("DocID")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="开出时间" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <span>开出时间</span></HeaderTemplate>
                                <ItemTemplate>
                                    <span>
                                        <%#Getdatetime(Eval("DocMakeTime")) %></span></ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="开出人" DataField="DocMaker" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="center">
                                <HeaderStyle Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="总金额" DataField="TotalMoney" ItemStyle-Wrap="false">
                                <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                <HeaderStyle Wrap="false" />
                            </asp:BoundField>
                            <%--<asp:TemplateField HeaderText="币种" ItemStyle-Wrap="false" >
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%#GetCurrency(Eval("Currency")) %>'></asp:Label>
                                </ItemTemplate>

<ItemStyle Wrap="False"></ItemStyle>
                            </asp:TemplateField>--%>
                            <asp:BoundField HeaderText="积分" DataField="TotalPV">
                                <ItemStyle HorizontalAlign="Right" Wrap="false" />
                                <HeaderStyle Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="期数" DataField="ExpectNum" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="center">
                                <HeaderStyle Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="审核人">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("DocAuditer") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Empty.GetString(Eval("DocAuditer").ToString()) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="审核时间" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                <HeaderTemplate>
                                    <span>审核时间</span></HeaderTemplate>
                                <ItemTemplate>
                                    <span>
                                        <%#Getdatetime(Eval("DocAuditTime"))%></span></ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField HeaderText="备注" DataField="Note" ItemStyle-Wrap="false" >
                     
<ItemStyle Wrap="False"></ItemStyle>
                               </asp:BoundField>--%>
                            <asp:TemplateField HeaderText="查看备注">
                                <ItemTemplate>
                                    <!--Label22-->
                                    <asp:Label ID="Lab_Description" runat="server" Text='<%#GetRemark(Eval("Note").ToString(),Eval("DocID").ToString()) %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" HorizontalAlign="center" />
                                <HeaderStyle Wrap="false" />
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle Wrap="False"></PagerStyle>
                        <SelectedRowStyle Wrap="False"></SelectedRowStyle>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <AlternatingRowStyle BackColor="#F1F4F8" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <uc1:Pager ID="Pager1" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
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
                <td style="white-space: nowrap">
                    <a href="#"><img src="images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX" 
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
                                            <asp:ImageButton ID="Butt_Excel" runat="server" ImageUrl="images/anextable.gif" OnClick="Butt_Excel_Click" />
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
                                        1、<%=GetTran("001213", "根据条件查询入库单、出库单、报损单、报溢单、调拨单等各种单据")%>。
                                        <br />
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
