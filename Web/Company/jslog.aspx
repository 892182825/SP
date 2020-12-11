<%@ Page Language="C#" AutoEventWireup="true" CodeFile="jslog.aspx.cs" Inherits="Company_jslog" %>


<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>会员信息编辑</title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />

    <script src="../JS/QCDS2010.js" type="text/javascript"></script>

    <script src="../JS/SqlCheck.js" type="text/javascript"></script>
  
    <script language="javascript" type="text/javascript">
        var aa=[
        '<%=GetTran("000032", "管 理") %>',
        '<%=GetTran("000033", "说 明") %>',
        '<%=GetTran("000481", "查询条件里面不能输入特殊字符")%>！'
    ];
    </script>

    <script src="js/jslog.js" type="text/javascript"></script>

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
                        <td align="left" style="white-space: nowrap">
                            <asp:Button ID="BtnConfirm" Style="display: none" runat="server" Text="查 询" OnClick="BtnConfirm_Click"
                                CssClass="anyes"></asp:Button>
                            <asp:LinkButton ID="lkSubmit1" Style="display: none" runat="server" Text="查 询" OnClick="lkSubmit1_Click"></asp:LinkButton>
                            <input class="anyes" id="Button2" onclick="CheckText('lkSubmit1')" type="button" value='<%=GetTran("000048","查 询")%>'/>
                                
                              <%=GetTran("000029", "注册期数")%>：<asp:DropDownList ID="DropDownExpectNum" runat="server" >
                            </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp; <asp:button id="btnhf" Runat="server" Text="返回" 
                                onclick="btnhf_Click"  CssClass="another"></asp:button>
                        </td>
                    </tr>
                </table>
                <br />
                <table cellspacing="0" cellpadding="0" border="0" width="100%" class="tablemb">
                    <tr>
                        <td valign="top">
                            <asp:GridView ID="GridView1" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False"
                                BorderStyle="Solid" OnRowDataBound="GridView1_RowDataBound">
                                <AlternatingRowStyle BackColor="#F1F4F8" />
                                <HeaderStyle  />
                                <RowStyle HorizontalAlign="Center" />
                                <Columns>
                                   
                                    <asp:BoundField DataField="qishu" HeaderText="期数"></asp:BoundField>
                                    <asp:BoundField DataField="type" HeaderText="状态"></asp:BoundField>
                                    <asp:BoundField DataField="OperateIP" HeaderText="操作者IP"></asp:BoundField>
                                    <asp:BoundField DataField="OperateBh" HeaderText="操作者编号"></asp:BoundField>
                                    <asp:BoundField DataField="begindate" HeaderText="开始时间"></asp:BoundField>
                                     <asp:BoundField DataField="enddate" HeaderText="结束时间" ></asp:BoundField>
                                    <asp:TemplateField HeaderText="查看备注">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                        <ItemTemplate>
                                            <%#SetVisible(Eval("remark").ToString() , Eval("id").ToString() )%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                </Columns>
                                <EmptyDataTemplate>
                                    <table width="100%">
                                        <tr>
                                            <th>
                                                <%=GetTran("000045", "期数")%>
                                            </th>
                                            <th>
                                                <%=GetTran("001593", "状态")%>
                                            </th>
                                            <th>
                                                <%=GetTran("003189", "操作者IP")%>
                                            </th>
                                            <th>
                                                <%=GetTran("005858", "操作者编号")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000559", "开始时间")%>
                                            </th>
                                            <th>
                                                <%=GetTran("005932", "结束时间")%>
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
                </table>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="right">
                <uc1:Pager ID="Pager1" runat="server" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
        </tr>
    </table>
    <div id="cssrain" style="width: 100%">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
            <tr>
                <td width="150">
                    <table width="100%" border="0" height="28" cellpadding="0" cellspacing="0" id="secTable">
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
