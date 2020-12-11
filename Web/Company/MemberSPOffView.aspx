<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberSPOffView.aspx.cs"
    Inherits="Company_MemberSPOffView" %>

<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/ExpectNum.ascx" TagName="ExpectNum" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link type="text/css" rel="Stylesheet" href="CSS/Company.css" />
    <title>Company_twQuery</title>

    <script language="javascript" type="text/javascript" src="../JS/jquery.js"></script>

    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>

    <script language="javascript" type="text/javascript" src="../js/SqlCheck.js"></script>

    <script language="javascript" type="text/javascript">
    
     var aa=[
        '<%=GetTran("000032", "管 理") %>',
        '<%=GetTran("000033", "说 明") %>',
        '<%=GetTran("000481", "查询条件里面不能输入特殊字符")%>！'
    ];
     function guanbi() {
         document.getElementById("divOffReason").style.display = "none";
     }
   function showControl(e,id,offReason) {

            document.getElementById("divOffReason").style.display = "";
            var x=e.clientX-(document.getElementById(id).style.width.replace("px","")-0)/2;
            var y=e.clientY;
            
            document.getElementById(id).style.left=x+"px";
            document.getElementById(id).style.top=y+"px";
            document.getElementById(id).style.visibility="visible";
            document.getElementById("lblOffReason").innerText = offReason;
            document.getElementById("divClose").innerHTML = "<br/><a href='javascript:void(0)' onclick='guanbi()' style='color:red;font-size:12px;'>关闭</a>";
        }
    </script>

    <script language="javascript" type="text/javascript" src="js/memOff.js"></script>

</head>
<body onload="down2()">
    <form id="form1" runat="server">
    <div>
        <br />
        <table width="100%" align="center">
            <tr>
                <td nowrap="nowrap">
                    <table class="biaozzi">
                        <tr>
                            <td nowrap="nowrap" style="display: none;">
                                <asp:Button ID="btnWL" runat="server" Text="会员冻结" OnClick="btnWL_Click" CssClass="another" />
                            </td>
                            <td>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnSeach" runat="server" Text="查 询" OnClick="btnSeach_Click" CssClass="anyes" />
                            </td>
                            <td>
                                <%=GetTran("000024", "会员编号")%>： &nbsp;
                            </td>
                            <td>
                                <asp:TextBox ID="txt_member" runat="server" MaxLength="15"></asp:TextBox>
                            </td>
                            <td>
                                <%=GetTran("000025", "会员姓名")%>： &nbsp;
                            </td>
                            <td>
                                <asp:TextBox ID="txtName" runat="server" MaxLength="15"></asp:TextBox>
                            </td>
                            <td>
                                <%=GetTran("000613", "日期")%>：<asp:TextBox ID="DatePicker2" runat="server" CssClass="Wdate"
                                    onfocus="WdatePicker()"></asp:TextBox>
                            </td>
                            <td nowrap="nowrap">
                                <%=GetTran("000045", "期数")%>：
                                <uc2:ExpectNum ID="ExpectNum1" runat="server" />
                            </td>
                            <td nowrap="nowrap">
                                <%=GetTran("007560", "冻结状态")%>：<asp:DropDownList ID="DropDownList1" runat="server">
                                    <asp:ListItem Value="0">全部</asp:ListItem>
                                    <asp:ListItem Value="1">已冻结</asp:ListItem>
                                    <asp:ListItem Value="2">已恢复</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <!--Gridview显示部分-->
                    <table width="100%">
                        <tr>
                            <td colspan="2" align="center" width="100%">
                                <asp:GridView ID="givTWmember" Width="100%" runat="server" AutoGenerateColumns="False"
                                    CssClass="tablemb" OnRowDataBound="givTWmember_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="Number" HeaderText="会员编号" />
                                        <asp:TemplateField HeaderText="会员姓名">
                                            <ItemTemplate>
                                                <%#GetbyName(Eval("Number").ToString())%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="zxqishu" HeaderText="冻结期数" />
                                        <asp:TemplateField HeaderText="冻结日期">
                                            <ItemTemplate>
                                                <%#GetbyDateTime(Eval("zxdate").ToString())%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="操作人编号" DataField="Operator" />
                                        <asp:BoundField HeaderText="操作人姓名" DataField="OperatorName" />
                                        <asp:BoundField DataField="zx" HeaderText="冻结状态" />
                                        <asp:TemplateField HeaderText="冻结原因" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <label id="lbloffReason1" title='<%=GetTran("000000","冻结原因") %>'>
                                                    <%#ShowOffReason(Eval("offReason"))%></label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <table width="100%">
                                            <tr>
                                                <th>
                                                    <%=GetTran("000024", "会员编号")%>
                                                </th>
                                                <th>
                                                    <%=GetTran("001400", "昵称")%>
                                                </th>
                                                <th>
                                                    <%=GetTran("000000", "冻结期数")%>
                                                </th>
                                                <th>
                                                    <%=GetTran("000000", "冻结日期")%>
                                                </th>
                                                <th>
                                                    <%=GetTran("004134","操作人编号")%>
                                                </th>
                                                <th>
                                                    <%=GetTran("007272", "操作人姓名")%>
                                                </th>
                                                <th>
                                                    <%=GetTran("007560", "冻结状态")%>
                                                </th>
                                                <th>
                                                    <%=GetTran("007561", " 冻结原因")%>
                                                </th>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblMassage" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <uc1:Pager ID="Pager1" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
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
                                                        <asp:LinkButton ID="btnDownExcel" runat="server" Text="导出Excel" OnClick="btnDownExcel_Click"
                                                            Style="display: none;"></asp:LinkButton><a href="#"><img src="images/anextable.gif"
                                                                width="49" height="47" border="0" onclick="__doPostBack('btnDownExcel','');" /></a>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;<%=GetTran("007429","公司可以下载和保存此页面的文档") %>
                                                        <!-- <a href="#">
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
                                                        １、<%=GetTran("006848", "输入要注册的会员编号，使其业绩不向上累计也不产生奖金。")%>
                                                    </td>
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
        <div id="divOffReason" style="position: absolute; left: 0px; top: 00px; width: 300px;
            height: 250px; background-color: white; visibility: hidden; border: #88E0F4 solid 1px;
            margin-left: -98px; text-align: center; filter: alpha(opacity=90); opacity: 0.9;">
            <div id="divOff" style="width: 100%; height: 80%; text-align: center;">
                <textarea id="lblOffReason" readonly="readonly" style="width: 98%; height: 80%;"
                    rows="4"></textarea>
                    <div id="divClose" style="width:auto; height:auto; padding-bottom:10px; text-align:center;">
                    
                    </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
