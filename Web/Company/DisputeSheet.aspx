<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DisputeSheet.aspx.cs" Inherits="Company_DisputeSheet" %>

<%@ Register src="../UserControl/Pager.ascx" tagname="Pager" tagprefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
    <style>
        .biaozzi {
            border-top:solid 1px #005575;
            border-left:solid 1px #005575;     
        }
        .biaozzi th {
            border-right:solid 1px #005575;
            border-bottom:solid 1px #005575;
        }
        .biaozzi td {
            border-right:solid 1px #005575;
            border-bottom:solid 1px #005575;
            color:#333;
        }
    </style>   <script src="js/jquery-1.4.3.min.js"></script>
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
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:98%; margin-left:10px; margin-top:15px;">
        <table width="100%" class="biaozzi" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="查询" CssClass="anyes" OnClick="Button1_Click" />
                    <asp:DropDownList ID="DropDownList1" runat="server">
                        <asp:ListItem Value="-1">不限</asp:ListItem>
                        <asp:ListItem Value="0">汇款</asp:ListItem>
                        <asp:ListItem Value="1">提现</asp:ListItem>
                    </asp:DropDownList>
                    编号：<asp:TextBox ID="txtnumber" runat="server"></asp:TextBox>
                    点击：
                    <asp:DropDownList ID="DropDownList2" runat="server">
                        <asp:ListItem Value="-1">不限</asp:ListItem>
                        <asp:ListItem Value=" hstate = 1">汇方已点击</asp:ListItem>
                        <asp:ListItem Value=" tstate = 1">收方已点击</asp:ListItem>
                    </asp:DropDownList>

                    <asp:DropDownList ID="ddlhistory" runat="server">
                        <asp:ListItem Selected="True" Text="未处理" Value="0"></asp:ListItem>
                        <asp:ListItem   Text="已处理" Value="1"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <br />
        <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
            <ItemTemplate>
                <div>
                    单号：<%# Eval("wid") %>
                    <table width="100%" class="biaozzi" cellspacing="0" cellpadding="0">
                        <tr style="background-color:#fff;">
                            <th width="6%">编号</th>
                            <th width="6%">姓名</th>
                            <th width="6%">电话</th>
                            <th width="3%">点击</th>
                            <th width="4%">超时</th>
                            <th width="6%">金额</th>
                            <th width="30%">解释原因</th>
                            <th width="16%">银行信息</th>
                            <th width="10%">撮合时间/汇出时间</th>
                            <th width="3%">责</th>
                            <th width="10%">汇款说明</th>
                        </tr>
                        <tr align="center">
                            <td><%# Eval("hnumber") %></td>
                            <td><%# GetName(Eval("hnumber").ToString()) %></td>
                            <td><%# GetPhone(Eval("hnumber").ToString()) %></td>
                            <td><%# Eval("shenst").ToString()=="1"?"√":"" %></td>
                            <td><%# getchaoshi(Eval("hchaoshi").ToString()) %></td>
                            <td><%# Convert.ToDouble(Eval("hmoney")).ToString("f2") %></td>
                            <td>
                                 <%#    Eval("hkpzImglj").ToString()==""?"":"<img src='../hkpzimg/"+ Eval("hkpzImglj").ToString()+"' alt='点击查看' style='width:100px;height:50px; cursor:pointer; ' onclick='showbigimg(this);'   />" %>
                                <div id="JS" style="width:100%;word-break:break-all;overflow:hidden;" title='<%# Eval("hshuoming") %>'><%# Eval("hshuoming") %></div></td>
                            <td>&nbsp;</td>
                            <td rowspan="2">
                                <%# Convert.ToDateTime(Eval("ctime")).AddHours(8).ToString() %>
                                <br />&nbsp;
                                <%# Eval("hstate").ToString()=="1"?Eval("hctime"):"" %>
                            </td>
                            <td><%# Eval("hzr").ToString()=="1"?"√":"" %></td>
                            <td><%# Eval("hremark") %></td>
                        </tr>
                        <tr align="center">
                            <td><%# Eval("tnumber") %></td>
                            <td><%# GetName(Eval("tnumber").ToString()) %></td>
                            <td><%# GetPhone(Eval("tnumber").ToString()) %></td>
                            <td><%# Eval("tstate").ToString()=="1"?"√":"" %></td>
                            <td><%# getchaoshi(Eval("tchaoshi").ToString()) %></td>
                            <td><%# Convert.ToDouble(Eval("tmoney")).ToString("f2") %></td>
                            <td>
                             



                                <div style="width:100%;word-break:break-all;">
                                <%# Eval("hshuoming1") %>
                                </div>
                            </td>
                            <td><%# Eval("bankinfo") %></td>
                            <td><%# Eval("tzr").ToString()=="1"?"√":"" %></td>
                            <td>
                                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="cl" Visible='<%# Eval("iscl").ToString()=="1"?false:true %>' CommandArgument='<%# Eval("wid")+","+Eval("tmoney")+","+Eval("tstate") %>'>处理</asp:LinkButton>

                            </td>
                        </tr>
                    </table>
                    <br />
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <uc1:Pager ID="Pager1" runat="server" />
    </div>
         <div id="imgdiv" style=" border-radius:20px; display:none; position:absolute;   width:600px; padding:20px; background-color:#808080; height:auto;">
            <div style="height:40px;width:40px;background-color:#fff; border-radius:20px;position:absolute;right:0px; top:0px;font-size:25px;text-align :center; vertical-align:middle;cursor:pointer;  " onclick="hidimgdiv()">×</div>
            <img id="imgshow"  style="max-width:600px; "    />
        </div>
    </form>
</body>
</html>
