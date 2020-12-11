<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DoSeecan.aspx.cs" Inherits="DoSeecan" %>

<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/PagerTwo.ascx" TagName="Pager1" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>

    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />

    <script src="../JS/QCDS2010.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../js/SqlCheck.js"></script>

    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-1.4.3.min.js"></script>
    <script>
        function showbigimg(ele) {
         
            var left = (screen.width - 700) / 2;
            var top = (screen.height - 150) / 2;
            $("#imgdiv").css("top", 150);
            $("#imgdiv").css("left", left);
            $("#imgdiv").show();
            $("#imgshow").attr("src", $(ele).attr("src"));
           

        }

        function hidimgdiv() {
            $("#imgdiv").hide();
        }

</script>
    <script type="text/javascript">
        window.onerror = function () {
            return true;
        };
    </script>

    <script src="js/tianfeng.js" type="text/javascript"></script>

    <script language="javascript">
        function cut() {
            document.getElementById("span1").title = '<%=GetTran("000032", "管 理") %>';
        }
        function cut1() {
            document.getElementById("span2").title = '<%=GetTran("000033", "说 明") %>';
        }

        function myrefresh() {
            window.location.reload();
        }
    </script>

    <script>
        function CheckText(btname) {
            //这个方法是页面有多个按钮要提交时，多次使用这个方法,传入按钮的ID
            filterSql_II(btname);
        }
        window.onload = function () {
            down2();
        }
        function opshow(id) {

            var left = (screen.width - 200) / 2;
            var top = (screen.height - 150) / 2;

            window.open("ok.aspx?repid=" + id, "newwindow", "height=150, width=200, top=" + top + ", left=" + left + ", toolbar=no, menubar=no, scrollbars=no, resizable=no,location=n o, status=no");
            return false;
        }
        function confirm() {

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
                                                
                                                &nbsp;
                                            </td>
                                            <td>
                                                <%=GetTran("007798", "汇款时间")%>
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
                                           
                                            <td align="right">
                                                <%=GetTran("000024","会员编号")%>：
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtNumber" runat="server" MaxLength="10"></asp:TextBox>
                                            </td>
                                        
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <div runat="server" id="div1">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" id="tbColor">
                                <tr>
                                    <td style="word-break: keep-all; word-wrap: normal">
                                        <asp:GridView ID="GridView1" runat="server" ShowFooter="True" 
                                            OnRowDataBound="GridView1_RowDataBound" Width="100%" CssClass="tablemb" 
                                            AutoGenerateColumns="False" EnableModelValidation="True">
                                            <AlternatingRowStyle BackColor="#F1F4F8" />
                                            <HeaderStyle CssClass="tablebt" Wrap="false" />
                                            <RowStyle HorizontalAlign="Center" />
                                            <Columns>
                                            <asp:TemplateField HeaderText="公司审核">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lkbtsuregetmoney"  CommandArgument='<%#Eval("id") %>'  Visible="false"  CommandName="OM" runat="server" Text="确认收款"></asp:LinkButton> 
                                                        <asp:Label ID="lblsure" runat="server" ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="汇款金额"  DataField="totalmoney" DataFormatString="{0:0.00}" />
                                                <asp:TemplateField HeaderText="汇款时间">
                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblremdate" runat="server" Text='<%# Bind("remittancesdate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="汇入账户">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblbankinfo" runat="server" Text='<%# Bind("RemBankBook") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("RemBankBook") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="姓名" DataField="name" />
                                                <asp:BoundField HeaderText="编号" DataField="number" />
                                                <asp:BoundField HeaderText="金流匹配" DataField="isjlstr" />
                                                <asp:TemplateField HeaderText="汇款凭证"> 
                                                     <ItemTemplate>
                                                         <asp:Image ID="Image1" style="cursor:pointer;" Visible="false" Width="100px"  height="50px" onclick="showbigimg(this);" AlternateText="点击查看" ImageUrl='<%# "~/hkpzimg/"+ Eval("hkpzImglj").ToString() %>' runat="server" />
                                                         </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <table width="100%" class="tablemb">
                                                    <tr>
                                                        <th> <%=GetTran("007818", "公司审核")%></th>
                                                        <th> <%=GetTran("001970", "汇款金额")%></th>
                                                        <th> <%=GetTran("007819", "汇入账户")%></th>
                                                        <th> <%=GetTran("000107", "姓名")%></th>
                                                        <th> <%=GetTran("001195", "编号")%></th>
                                                        <th> <%=GetTran("000000", "金流匹配")%></th>
                                                           <th> <%=GetTran("000000", "汇款凭证")%></th>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                            <table width="99%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
                                <tr>
                                    <td colspan="3">
                                        <uc1:Pager ID="Pager1" runat="server" />
                                    </td>
                                </tr>
                                 
                            </table>
                        </div>
                    </div>
                    <br />
                </td>
            </tr>
        </table>
        <%--<div id="bgdiv" style="position:absolute;"></div>
        <div id="confirminfo"  style=" width:200px; height:250px; border:2px #ccc solid; padding:10px; position:absolute;">
          <table class="tablemb">
            <tr>
                <td>
                    汇款金额：<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    匹配金额：<asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    实汇金额：￥<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="Button2" runat="server" Text="确认收款" CssClass="anyes" 
                        onclick="Button1_Click" OnClientClick="return checkedcf('确定收到该笔汇款了吗?')" />
                </td>
            </tr>
        </table>
        </div>--%>
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
                                        <asp:Button ID="Button1" Text="导出EXECL" runat="server"  Style="display: none;" OnClick="Button1_DaoChu">
                                        </asp:Button><a href="#"><img src="images/anextable.gif" width="49" height="47" border="0"
                                            onclick="__doPostBack('Button1','');" style="cursor: hand;" /></a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
                                        <%=GetTran("007821", "审核会员提现申请")%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
        <div id="imgdiv" style=" border-radius:20px; display:none; position:absolute;   width:600px; padding:20px; background-color:#808080; height:auto;">
            <div style="height:40px;width:40px;background-color:#fff; border-radius:20px;position:absolute;right:0px; top:0px;font-size:25px;text-align :center; vertical-align:middle;cursor:pointer;  " onclick="hidimgdiv()">×</div>
            <img id="imgshow"  style="max-width:600px; "    />
        </div>

    </form>
</body>
</html>








