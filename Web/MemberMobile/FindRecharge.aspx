<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FindRecharge.aspx.cs" Inherits="Member_FindRecharge" %>

<%@ Register Src="../UserControl/MemberPager.ascx" TagName="Pager" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/MemberTop.ascx" TagName="top" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/MemberBottom.ascx" TagName="bottom" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="CSS/detail.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript" src="../js/SqlCheck.js"></script>


    <link rel="stylesheet" type="text/css" href="hycss/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="hycss/serviceOrganiz.css" />
    <link rel="stylesheet" type="text/css" href="hycss/jquery.mCustomScrollbar.css" />
    <script src="js/jquery-3.1.1.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="js/bootstrap.js" type="text/javascript" charset="utf-8"></script>
    <script src="js/serviceOrganiz.js" type="text/javascript" charset="utf-8"></script>
    <script src="js/jquery-migrate-1.1.0.min.js" type="text/javascript" charset="utf-8"></script>
    <script type="text/javascript" src="js/jquery.mCustomScrollbar.js"></script>
    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>

    <script type="text/javascript" language="javascript">
        function CheckText() {
            //防SQL注入
            filterSql();
        }
    </script>

</head>
<%--<body>
    <form id="form1" runat="server">
    
    <div class="MemberPage">
        <uc1:top runat="server" ID="top" />
        <div class="ctConPgList">
            <ul>
                <li>
                    <%=this.GetTran("006684", "手机号码：")%></li>
                <li>
                    <asp:TextBox ID="txtPhoneNumber" CssClass="ctConPgFor" runat="server"></asp:TextBox>
                </li>
                <li>
                    <%=this.GetTran("007369", "充值状态")%>：</li>
                <li>
                    <li>
                        <asp:DropDownList ID="dllState" CssClass="ctConPgFor" runat="server">
                            <asp:ListItem Value="-1" Selected="True">全部</asp:ListItem>
                            <asp:ListItem Value="0">充值失败</asp:ListItem>
                            <asp:ListItem Value="1">充值中</asp:ListItem>
                            <asp:ListItem Value="2">充值成功</asp:ListItem>
                        </asp:DropDownList>
                    </li>
                    <li>
                        <asp:Button ID="btnQuery" runat="server" Text="搜 索" OnClick="btnQuery_Click" CssClass="anyes"
                            OnClientClick="CheckText()" /></li>
            </ul>
        </div>
        <div class="ctConPgList-1">
            <asp:GridView ID="gv_browOrder" CellPadding="1" border="0" CellSpacing="1" runat="server"
                AutoGenerateColumns="False" Width="100%" OnRowDataBound="gv_browOrder_RowDataBound">
                <HeaderStyle CssClass="ctConPgTab" />
                <AlternatingRowStyle BackColor="#F1F4F8" Wrap="false" />
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:BoundField HeaderText="充值单号" DataField="rechargeid" ItemStyle-Wrap="false">
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="会员编号" DataField="number" ItemStyle-Wrap="false">
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="手机号码" DataField="phonenumber" ItemStyle-Wrap="false">
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="充值金额" DataField="addmoney" ItemStyle-Wrap="false" DataFormatString="{0:F0}">
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="充值状态" DataField="addstate" ItemStyle-Wrap="false">
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="充值日期" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <%# GetRegisterDate(Eval("addtime").ToString())%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <table width="100%" border="0" cellpadding="1" cellspacing="1">
                        <tr class="ctConPgTab">
                            <th>
                                <%=this.GetTran("008038", "充值单号")%>
                            </th>
                            <th>
                                <%=this.GetTran("000024", "会员编号")%>
                            </th>
                            <th>
                                <%=this.GetTran("005623", "手机号码")%>
                            </th>
                            <th>
                                <%=this.GetTran("008039", "充值金额")%>
                            </th>
                            <th>
                                <%=this.GetTran("007369", "充值状态")%>
                            </th>
                            <th>
                                <%=this.GetTran("008040", "充值时间")%>
                            </th>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
        <uc1:Pager ID="Pager1" runat="server" />
        <%=msg %>
        <uc2:bottom runat="server" ID="bottom" />
    </div>
    </form>
</body>--%>
<body>
    <form id="form2" runat="server">
          <uc1:top runat="server" ID="top" />
        <div class="rightArea clearfix">
            <div class="rightAreaIn">
                <div id="qq2" class="fiveSquareBox clearfix searchFactor">
                    <%=this.GetTran("006684", "手机号码：")%>
                    <asp:TextBox ID="txtPhoneNumber" CssClass="ctConPgFor" style="width:auto;height:auto" runat="server"></asp:TextBox>
                    <%=this.GetTran("007369", "充值状态")%>：
                    <asp:DropDownList ID="dllState" style="width:auto;height:auto" CssClass="ctConPgFor" runat="server">
                        <asp:ListItem Value="-1" Selected="True">全部</asp:ListItem>
                        <asp:ListItem Value="0">充值失败</asp:ListItem>
                        <asp:ListItem Value="1">充值中</asp:ListItem>
                        <asp:ListItem Value="2">充值成功</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btnQuery" runat="server" Height="27px" Width="47px" Style="margin-left: 17px; padding: 2px 9px; color: #FFF; border: 1px solid #507E0C; background-image: linear-gradient(#addf58,#96c742); text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.4);"
                        Text="查 询" CssClass="anyes" OnClientClick="CheckText()" />
                </div>
                <div id="qq1" class="noticeEmail width100per mglt0">
                    <div class="pcMobileCut">
                        <div class="noticeHead">
                            <div>
                                <i class="glyphicon glyphicon-file"></i>
                                <span><%=this.GetTran("008142", "手机充值查询")%></span>
                            </div>
                        </div>
                        <div class="noticeBody">
                            <div class="tableWrap clearfix table-responsive">
                                <table class="table-bordered noticeTable">
                                    <tbody>
                                        <tr style="text-align: center">
                                            <td style="padding: 0">
                                                <asp:GridView ID="gv_browOrder" CellPadding="1" border="0" CellSpacing="1" runat="server"
                                                    AutoGenerateColumns="False" Width="100%" OnRowDataBound="gv_browOrder_RowDataBound">
                                                      <HeaderStyle Wrap="false" CssClass="tablemb" />
                                                    <AlternatingRowStyle BackColor="#F1F4F8" Wrap="false" />
                                                    <RowStyle HorizontalAlign="Center" />
                                                    <Columns>
                                                        <asp:BoundField HeaderText="充值单号" DataField="rechargeid" ItemStyle-Wrap="false">
                                                            <ItemStyle Wrap="False"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="会员编号" DataField="number" ItemStyle-Wrap="false">
                                                            <ItemStyle Wrap="False"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="手机号码" DataField="phonenumber" ItemStyle-Wrap="false">
                                                            <ItemStyle Wrap="False"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="充值金额" DataField="addmoney" ItemStyle-Wrap="false" DataFormatString="{0:F0}">
                                                            <ItemStyle Wrap="False"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="充值状态" DataField="addstate" ItemStyle-Wrap="false">
                                                            <ItemStyle Wrap="False"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="充值日期" ItemStyle-Wrap="false">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <%# GetRegisterDate(Eval("addtime").ToString())%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <table width="100%" border="0" cellpadding="1" cellspacing="1">
                                                            <tr class="ctConPgTab">
                                                                <th>
                                                                    <%=this.GetTran("008038", "充值单号")%>
                                                                </th>
                                                                <th>
                                                                    <%=this.GetTran("000024", "会员编号")%>
                                                                </th>
                                                                <th>
                                                                    <%=this.GetTran("005623", "手机号码")%>
                                                                </th>
                                                                <th>
                                                                    <%=this.GetTran("008039", "充值金额")%>
                                                                </th>
                                                                <th>
                                                                    <%=this.GetTran("007369", "充值状态")%>
                                                                </th>
                                                                <th>
                                                                    <%=this.GetTran("008040", "充值时间")%>
                                                                </th>
                                                            </tr>
                                                        </table>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>


                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <uc1:Pager ID="Pager1" runat="server" />

                                <%=msg %>
                                <uc2:bottom runat="server" ID="bottom" />

                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/jscript">
        $(function () {
            $('#rtbiszf label').css('float', 'left');
            $('#rtbiszf').css('width', '16%');
            $('#Pager1_pageTable').css('margin-right', '0px');
            $('#Pager1_btn_submit').css('Height', '22px');
            $('#Pager1_btn_submit').css('width', '70px');
            $('#Pager1_pageTable').css('color', '#333');
            $('#Pager1_ShowTable').css('color', '#333');
            $('input[type="checkbox"]').css({ 'width': '18px', 'margin-right': '10px' })
            $("#qq1").css('width', '101%');
            $("#qq2").css('width', '101%');

        })

    </script>
     <script type="text/jscript">
        $(function () {
            $('#bottomDiv').css('display', 'none');

            $("#Pager1_div2").css('background-color','#FFF')
        })
        
    </script>
    <style>
        .tablemb th {
            padding: 10px 16px;
            border-left: #bebebe !important;
            font-family: Arial;
            font-size: 12px;
            font-weight: bold;
            color: #333;
            text-decoration: none;
            /* background-image: url(../images/tabledp.gif); */
            background-repeat: repeat-x;
            text-align: center;
            text-indent: 10px;
        }

        .tablemb {
            font-family: Arial;
            /* font-size: 12px; */
            /* color: #333; */
            /* margin-top: 90px; */
            text-decoration: none;
            line-height: 31px;
            background-color: #FAFAFA;
            /* border: 1px solid #88F4AE; */
            text-indent: 10px;
            white-space: normal;
            background: url(../../images/img/mws-table-header.png) left bottom repeat-x;
        }

        .rightArea {
            /*margin-left: 0px;*/
            padding-top: 40px;
            min-height: 100%;
        }

        .searchFactor span {
            float: left;
            width: 3%;
            display: block;
        }
    </style>
</body>



</html>
