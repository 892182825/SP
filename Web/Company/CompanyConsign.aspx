<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompanyConsign.aspx.cs" Inherits="Company_CompanyConsign" EnableEventValidation="false" %>

<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>

    <script type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>

    <script type="text/javascript">
        window.onload = function() {
            if (document.getElementById("DropDownList3").options[document.getElementById("DropDownList3").selectedIndex].text == '<%=GetTran("001371","已发货") %>') {
                document.getElementById("tr_id").removeChild(document.getElementById("tr_id").firstChild);
                document.getElementById("tr_id").removeChild(document.getElementById("tr_id").firstChild);
            }
        }
    </script>

    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />

    <script src="../JS/QCDS2010.js" type="text/javascript"></script>

    <script type="text/javascript">
        window.onerror = function() {
            return true;
        }
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

    <script type="text/javascript">
        window.onload = function() {
            down2();

            selectMode();
        }
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

    <script type="text/javascript">
        function cut() {
            document.getElementById("span1").title = '<%=GetTran("000032", "管 理") %>';
        }
        function cut1() {
            document.getElementById("span2").title = '<%=GetTran("000033", "说 明") %>';
        }
    </script>

    <script language="javascript">
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
  
        //币种变时，钱也要变。（表格列数变化时一定要更改此方法）
        var first=true; 
        var from=0;
        function setHuiLv_II() {
            var th = document.getElementById("Dropdownlist4");
            if (first) {
                from = AjaxClass.GetCurrency().value - 0;
                first = false;
            }

            var to = th.options[th.selectedIndex].value - 0;


            var hl = AjaxClass.GetCurrency_Ajax(from, to).value;

            var trarr = document.getElementById("GridView2_CompanyConsign").getElementsByTagName("tr");
            for (var i = 1; i < trarr.length; i++) {
                trarr[i].getElementsByTagName("td")[7].getElementsByTagName("span")[0].innerHTML =
                (parseFloat(trarr[i].getElementsByTagName("td")[7].getElementsByTagName("span")[0].firstChild.nodeValue.replace(/,/g, "")) / hl).toFixed(2);
            }

            from = to;
        }
    </script>

    <script type="text/javascript">
        function isCheck(mode) {
            var isCk = false;

            var ckID = "GridView1_CompanyConsign_ctl02_CkBox_More";

            for (var i = 2; i <= 11; i++) {
                if (i < 10)
                    ckID = "GridView1_CompanyConsign_ctl0" + i + "_CkBox_More";
                else
                    ckID = "GridView1_CompanyConsign_ctl" + i + "_CkBox_More";

                var ckobj = document.getElementById(ckID);

                if (ckobj != null) {
                    if (ckobj.checked) {
                        isCk = true;
                        break;
                    }
                }
                else {
                    if (i == 2)
                        break;
                }
            }

            if (isCk)
            //return confirm("确定"+mode+"？");
                return confirm('<%=GetTran("001491","确定发货吗？") %>');
            else {
                alert('<%=GetTran("001494","请选中复选框") %>');
                return false;
            }
        }

        function isCheck2(mode) {
            var isCk = false;

            var ckID = "GridView3_ctl02_hCkBox_More";

            for (var i = 2; i <= 11; i++) {
                if (i < 10)
                    ckID = "GridView3_ctl0" + i + "_hCkBox_More";
                else
                    ckID = "GridView3_ctl" + i + "_hCkBox_More";

                var ckobj = document.getElementById(ckID);

                if (ckobj != null) {
                    if (ckobj.checked) {
                        isCk = true;
                        break;
                    }
                }
                else {
                    if (i == 2)
                        break;
                }
            }

            if (isCk)
                return confirm('<%=GetTran("000434","确定") %> ' + mode + "？");
            else {
                alert('<%=GetTran("001494","请选中复选框") %>');
                return false;
            }
        }

        function isFahuo() {
            return confirm('<%=GetTran("001398","确定发货？") %>');
        }
    
        //币种变时，钱也要变。（表格列数变化时一定要更改此方法）
        var first=true; 
        var from=0;
        function setHuiLv() {
            var th = document.getElementById("Dropdownlist4");

            if (first) {
                from = AjaxClass.GetCurrency().value - 0;
                first = false;
            }

            var to = th.options[th.selectedIndex].value - 0;

            var hl = AjaxClass.GetCurrency_Ajax(from, to).value;

            var trarr = document.getElementById("GridView1_CompanyConsign").getElementsByTagName("tr");
            for (var i = 1; i < trarr.length; i++) {
                trarr[i].getElementsByTagName("td")[9].getElementsByTagName("span")[0].innerHTML =
                (parseFloat(trarr[i].getElementsByTagName("td")[9].getElementsByTagName("span")[0].firstChild.nodeValue.replace(/,/g, "")) / hl).toFixed(2);
            }

            from = to;
        }

        function selectMode() {
            var th = document.getElementById("DropDownList_Items");
            if (th.options[th.selectedIndex].value == "so.IsSent='N' ") {
                document.getElementById("Dropdownlist4").onchange = setHuiLv;
            }
            else {
                document.getElementById("Dropdownlist4").onchange = setHuiLv_II;
            }
        }
        
        //全选
        function allCk() {
            var trarr = document.getElementById("GridView1_CompanyConsign").getElementsByTagName("tr");

            var isCk = false;
            if (document.getElementById("ckQck").checked)
                isCk = true;

            for (var i = 1; i < trarr.length; i++) {
                trarr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked = isCk;
            }
        }
    </script>

    <script type="text/javascript" src="../js/SqlCheck.js"></script>

</head>
<body>
    <form id="form1" runat="server" onsubmit="return filterSql_III()">
    <div style="width: 100%">
        <br />
        <table style="width: 100%">
            <tr>
                <td>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
                        <tr>
                            <td>
                                <asp:Button ID="btn_Submit" runat="server" Text="查 询" Style="cursor: pointer;" CssClass="anyes"
                                    OnClick="btn_Submit_Click" align="absmiddle" Height="22px" />&nbsp;&nbsp;
                                <%=GetTran("000047")%>：<asp:DropDownList ID="DropCurrency" runat="server">
                                </asp:DropDownList>
                                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_Items_SelectedIndexChanged">
                                    <asp:ListItem Value="so.StoreOrderID">订单号</asp:ListItem>
                                    <asp:ListItem Value="ity.DocID">出库单号</asp:ListItem>
                                    <asp:ListItem Value="so.InceptPerson">收货人姓名</asp:ListItem>
                                    <asp:ListItem Value="〖Country〗">收货人国家</asp:ListItem>
                                    <asp:ListItem Value="〖Province〗">收货人省份</asp:ListItem>
                                    <asp:ListItem Value="〖City〗">收货人城市</asp:ListItem>
                                    <asp:ListItem Value="ity.Address">收货人地址</asp:ListItem>
                                    <asp:ListItem Value="so.PostalCode">收货人邮编</asp:ListItem>
                                    <asp:ListItem Value="so.Telephone">收货人电话</asp:ListItem>
                                    <asp:ListItem Value="ity.TotalMoney">订货总金额</asp:ListItem>
                                    <asp:ListItem Value="so.PayMentDateTime">付款日期</asp:ListItem>
                                    <asp:ListItem Value="mi.Number">会员编号</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="DropDownList_condition" runat="server">
                                </asp:DropDownList>
                                <asp:TextBox ID="TextBox4" runat="server" Width="120" MaxLength="100"></asp:TextBox>
                                <asp:TextBox ID="txtBox_rq" Visible="false" runat="server" Width="80px" onfocus="new WdatePicker()"
                                    CssClass="Wdate"></asp:TextBox>
                                <%=GetTran("000060")%>&nbsp;&nbsp;
                                <%=GetTran("000640", "订单状态")%>
                                <asp:DropDownList ID="DropDownList_Items" runat="server">
                                    <asp:ListItem Value="ity.IsSend=0">未发货</asp:ListItem>
                                    <asp:ListItem Value="ity.IsSend=1">已发货</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="Dropdownlist4" Visible="false" runat="server" Width="100px"
                                    EnableViewState="False">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table style="width: 100%">
                        <tr>
                            <td colspan="2" style="border: rgb(147,226,244) solid 1px">
                                <asp:GridView ID="GridView1_CompanyConsign" runat="server" AutoGenerateColumns="False"
                                    OnSelectedIndexChanged="GridView1_SelectedIndexChanged" Width="100%" class="tablemb bordercss"
                                    HeaderStyle-CssClass="tablebt bbb" OnRowDataBound="GridView1_CompanyConsign_RowDataBound">
                                    <EmptyDataTemplate>
                                        <table cellspacing="0" width="100%">
                                            <tr>
                                                <th nowrap>
                                                    <%=GetTran("001336","多选") %>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("001340","发货") %>
                                                </th>
                                                <th nowrap style='display: none;'>
                                                    <%= GetTran("000024", "会员编号")%>
                                                </th>
                                                <th nowrap>
                                                    <%= GetTran("000099","对应出库单号")%>
                                                </th>
                                                <th nowrap>
                                                    <%= GetTran("000079","订单号")%>
                                                </th>
                                                <th>
                                                    <%=GetTran("000024","会员编号") %>
                                                </th>
                                            <%--<th>
                                                    会员昵称
                                                </th>
                                                <th>
                                                    会员姓名
                                                </th>--%>
                                                <th nowrap>
                                                    <%= GetTran("000067","订货日期")%>
                                                </th>
                                                <th nowrap>
                                                    <%= GetTran("000045","期数")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetOrderType( GetTran("000106","订单类型"))%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000041","总金额") %>
                                                </th>
                                                <th nowrap>
                                                    <%= GetTran("000113","总积分")%>
                                                </th>
                                                <th nowrap>
                                                    <%= GetTran("000118","重量")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000383", "收货人姓名")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000108", "收货人国家")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000109","省份") %>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000110","城市") %>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("007711", "县")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000112","收货地址") %>
                                                </th>
                                                <th nowrap>
                                                    <%= GetTran("000073","邮编")%>
                                                </th>
                                                <th nowrap>
                                                    <%= GetTran("000115","联系电话")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("001345", "发货方式")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000386", "仓库")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000390", "库位")%>
                                                </th>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="多选">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CkBox_More" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="发货">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtn_Sent" runat="server" CommandName="select" OnClick="LinkButton1_Click"><%=GetTran("001340","发货") %></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="详细">
                                            <ItemTemplate>
                                                <img src="images/fdj.gif" /><asp:LinkButton ID="lbtn_StoreOrderID" runat="server"
                                                    OnClick="Label2_Click" CommandName="select"><%=GetTran("000339", "详细")%></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="店铺编号" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="Lab_StoreID" runat="server" Text='<%# Bind("client") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="出库单号">
                                            <ItemTemplate>
                                                <asp:Label ID="Lab_OutStorageOrderID" runat="server" Text='<%# Empty.GetString(Eval("docid").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="订单号">
                                            <ItemTemplate>
                                                <asp:Label ID="ddh" runat="server" Text='<%# Bind("StoreOrderID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="会员编号">
                                            <ItemTemplate>
                                                <asp:Label ID="a1" runat="server" Text='<%#Eval("Number") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                        </asp:TemplateField>
                                        <%--  <asp:TemplateField HeaderText="会员昵称">
                                                        <ItemTemplate><!--Label4-->
                                                            <asp:Label ID="a2" runat="server" Text='<%#Eval("PetName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                        <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="会员姓名">
                                                        <ItemTemplate><!--Label4-->
                                                            <asp:Label ID="a3" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                        <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                                                    </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="订货日期">
                                            <ItemTemplate>
                                                <asp:Label ID="Lab_OrderDateTime" runat="server" Text='<%# GetBiaoZhunTime(Eval("OrderDateTime").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="期数">
                                            <ItemTemplate>
                                                <asp:Label ID="Lab_ExpectNum" runat="server" Text='<%# Bind("ExpectNum") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="付款日期" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="Lab_PayMentDateTime" runat="server" Text='<%# GetBiaoZhunTime(Eval("PayMentDateTime").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="订货类型">
                                            <ItemTemplate>
                                                <asp:Label ID="Lab_OrderType" runat="server" Text='<%# GetOrderType(Eval("OrderType").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="总金额">
                                            <ItemTemplate>
                                                <asp:Label ID="Lab_TotalMoney" runat="server" Text='<%# Bind("TotalMoney", "{0:N2}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="总积分">
                                            <ItemTemplate>
                                                <asp:Label ID="Lab_TotalPV" runat="server" Text='<%# Bind("TotalPV", "{0:N2}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="right" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="订货总重量(kg)">
                                            <ItemTemplate>
                                                <asp:Label ID="Lab_Weight" runat="server" Text='<%#Eval("Weight", "{0:N2}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="right" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="运费" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lab_Carriage" runat="server" Text='<%# Eval("Carriage", "{0:N2}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="姓名">
                                            <ItemTemplate>
                                                <asp:Label ID="Lab_InceptPerson" runat="server" Text='<%#  Empty.GetString(Encryption.Encryption.GetDecipherName(Eval("InceptPerson").ToString())) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="收货人国家">
                                            <ItemTemplate>
                                                <asp:Label ID="lab_Province" runat="server" Text='<%#Eval("country") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="省份">
                                            <ItemTemplate>
                                                <asp:Label ID="lab_a" runat="server" Text='<%#Eval("province") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="城市">
                                            <ItemTemplate>
                                                <asp:Label ID="b" runat="server" Text='<%#Eval("city") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="县">
                                            <ItemTemplate>
                                                <asp:Label ID="_bx" runat="server" Text='<%#Eval("Xian") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="收货人地址">
                                            <ItemTemplate>
                                                <asp:Label ID="Lab_InceptAddress" runat="server" Text='<%# SetFormatString(Encryption.Encryption.GetDecipherAddress(Eval("Address").ToString()),15) %>'
                                                    title='<%#Encryption.Encryption.GetDecipherAddress(Eval("Address").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" Wrap="false" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="邮编">
                                            <ItemTemplate>
                                                <asp:Label ID="Lab_PostalCode" runat="server" Text='<%# Bind("PostalCode") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="电话">
                                            <ItemTemplate>
                                                <asp:Label ID="Lab_Telephone" runat="server" Text='<%# Encryption.Encryption.GetDecipherTele(Eval("Telephone").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="发货方式">
                                            <ItemTemplate>
                                                <asp:Label ID="Lab_ConveyanceMode" runat="server" Text='<%#  getFh(Eval("ConveyanceMode").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="物流公司" Visible="false">
                                            <ItemTemplate><!--Label18-->
                                                <asp:Label ID="Lab_ConveyanceCompany" runat="server" Text='<%#  Empty.GetString(Eval("ConveyanceCompany").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap=false HorizontalAlign="center" />
                                            <HeaderStyle Wrap=false />
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="仓库">
                                            <ItemTemplate>
                                                <asp:Label ID="Lab_WareHouseName" runat="server" Text='<%#  Empty.GetString(Eval("WareHouseName").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="库位">
                                            <ItemTemplate>
                                                <asp:Label ID="txtBox_SeatName" runat="server" Text='<%# Empty.GetString(Eval("SeatName").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <AlternatingRowStyle BackColor="#F1F4F8" />
                                </asp:GridView>
                                <asp:GridView ID="GridView2_CompanyConsign" runat="server" AutoGenerateColumns="False"
                                    Width="100%" class="tablemb bordercss" HeaderStyle-CssClass="tablebt bbb" OnRowDataBound="GridView2_CompanyConsign_RowDataBound"
                                    OnSelectedIndexChanged="GridView2_CompanyConsign_SelectedIndexChanged">
                                    <EmptyDataTemplate>
                                        <table cellspacing="0" width="100%">
                                            <tr>
                                                <th nowrap style="display: none">
                                                    <%= GetTran("000024", "订货店铺")%>
                                                </th>
                                                <th nowrap>
                                                    <%= GetTran("000099","对应出库单号")%>
                                                </th>
                                                <th nowrap>
                                                    <%= GetTran("000079","订单号")%>
                                                </th>
                                                <th>
                                                    会员编号
                                                </th>
                                                <th>
                                                    会员昵称
                                                </th>
                                                <th>
                                                    会员姓名
                                                </th>
                                                <th nowrap>
                                                    <%= GetTran("000067","订货日期")%>
                                                </th>
                                                <th nowrap>
                                                    <%= GetTran("000045","期数")%>
                                                </th>
                                                <th nowrap>
                                                    <%= GetTran("000106","订单类型")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000041","总金额") %>
                                                </th>
                                                <th nowrap>
                                                    <%= GetTran("000113","总积分")%>
                                                </th>
                                                <th nowrap>
                                                    <%= GetTran("000118","重量")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000383", "收货人姓名")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000108", "收货人国家")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000109","省份") %>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000110","城市") %>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000112","收货地址") %>
                                                </th>
                                                <th nowrap>
                                                    <%= GetTran("000073","邮编")%>
                                                </th>
                                                <th nowrap>
                                                    <%= GetTran("000115","联系电话")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("001345", "发货方式")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000121", "物流公司")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000386", "仓库")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000390", "库位")%>
                                                </th>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="订单号">
                                            <ItemTemplate>
                                                <img src="images/fdj.gif" />
                                                <asp:LinkButton ID="aacc" runat="server" OnClick="Label2_Click" CommandName="select"><%=GetTran("000339", "详细")%></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="店铺编号" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="yLab_StoreID" runat="server" Text='<%# Bind("client") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="要货单号">
                                            <ItemTemplate>
                                                <asp:Label ID="order" runat="server" Text='<%# Bind("StoreOrderID") %>' Style="text-decoration: none;
                                                    cursor: text;"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="出库单号">
                                            <ItemTemplate>
                                                <asp:Label ID="aa" runat="server" Text='<%# Bind("docid") %>' Style="text-decoration: none;
                                                    cursor: text;"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="会员编号">
                                            <ItemTemplate>
                                                <asp:Label ID="a1" runat="server" Text='<%#Eval("Number") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                        </asp:TemplateField>
                                        <%-- <asp:TemplateField HeaderText="会员昵称">
                                                        <ItemTemplate><!--Label4-->
                                                            <asp:Label ID="a2" runat="server" Text='<%#Eval("PetName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                        <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="会员姓名">
                                                        <ItemTemplate><!--Label4-->
                                                            <asp:Label ID="a3" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                        <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                                                    </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="订货日期">
                                            <ItemTemplate>
                                                <asp:Label ID="yLab_OrderDateTime" runat="server" Text='<%# GetBiaoZhunTime(Eval("OrderDateTime").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="期数">
                                            <ItemTemplate>
                                                <asp:Label ID="yLab_ExpectNum" runat="server" Text='<%# Bind("ExpectNum") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="付款时间" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="yLab_PayMentDateTime" runat="server" Text='<%# GetBiaoZhunTime(Eval("PayMentDateTime").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="订货类型">
                                            <ItemTemplate>
                                                <asp:Label ID="yLab_OrderType" runat="server" Text='<%# GetOrderType(Eval("OrderType").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="总金额">
                                            <ItemTemplate>
                                                <asp:Label ID="yLab_TotalMoney" runat="server" Text='<%# Bind("TotalMoney", "{0:N2}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="总积分">
                                            <ItemTemplate>
                                                <asp:Label ID="yLab_TotalPV" runat="server" Text='<%# Bind("TotalPV", "{0:N2}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="right" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="订货总重量(kg)">
                                            <ItemTemplate>
                                                <asp:Label ID="yLab_Weight" runat="server" Text='<%#Eval("Weight", "{0:N2}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="right" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="运费" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="ylab_Carriage" runat="server" Text='<%# Eval("Carriage", "{0:N2}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="预计到达时间" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="yLab_ForeCastArriveDateTime" runat="server" Text='<%# SetTimeFormat(Eval("ForeCastArriveDateTime").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="姓名">
                                            <ItemTemplate>
                                                <asp:Label ID="yLab_InceptPerson" runat="server" Text='<%#  Empty.GetString(Encryption.Encryption.GetDecipherName(Eval("InceptPerson").ToString())) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="收货人国家">
                                            <ItemTemplate>
                                                <asp:Label ID="ylab_Province" runat="server" Text='<%#Eval("country") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="省份">
                                            <ItemTemplate>
                                                <asp:Label ID="ylab_a" runat="server" Text='<%#Eval("province") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="城市">
                                            <ItemTemplate>
                                                <asp:Label ID="yb" runat="server" Text='<%#Eval("city") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="县">
                                            <ItemTemplate>
                                                <asp:Label ID="bxx" runat="server" Text='<%#Eval("Xian") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="收货人地址">
                                            <ItemTemplate>
                                                <asp:Label ID="yLab_InceptAddress" runat="server" Text='<%# SetFormatString(Encryption.Encryption.GetDecipherAddress(Eval("Address").ToString()),15) %>'
                                                    title='<%#Eval("Address") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" Wrap="false" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="邮编">
                                            <ItemTemplate>
                                                <asp:Label ID="yLab_PostalCode" runat="server" Text='<%# Bind("PostalCode") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="电话">
                                            <ItemTemplate>
                                                <asp:Label ID="yLabe_Telephone" runat="server" Text='<%# Encryption.Encryption.GetDecipherTele(Eval("Telephone").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="发货方式">
                                            <ItemTemplate>
                                                <asp:Label ID="yLab_ConveyanceMode" runat="server" Text='<%#  getFh(Eval("ConveyanceMode").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" />
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="物流公司">
                                            <ItemTemplate>
                                                <asp:Label ID="yLab_ConveyanceCompany" runat="server" Text='<%#  Empty.GetString(Eval("ConveyanceCompany").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="仓库">
                                            <ItemTemplate>
                                                <asp:Label ID="yLab_WareHouseName" runat="server" Text='<%# Bind("WareHouseName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="库位">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server" Width="50px"></asp:TextBox>
                                                <a href="#" onmouseup=selectKW('<%#Eval("WareHouseID") %>',event,this)>选择</a>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="库位">
                                            <ItemTemplate>
                                                <asp:Label ID="txtBox_SeatName" runat="server" Text='<%#Eval("SeatName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <AlternatingRowStyle BackColor="#F1F4F8" />
                                </asp:GridView>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="Panel1" runat="server">
                                    <input type='checkbox' id="ckQck" onclick="allCk()">
                                    <label id="a" for="ckQck" style='font-size: 10pt'>
                                      <%=GetTran("004198", " 全选")%> </label>
                                    <asp:Button ID="btn_MoreSent" runat="server" Text="多选发货" Style="cursor: pointer;"
                                        CssClass="another" OnClick="Button5_Click" align="absmiddle" OnClientClick="return isCheck('发货')"
                                        Height="22px" />
                                </asp:Panel>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <uc1:Pager ID="Pager1" runat="server" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btn_Select" runat="server" Text="选中合单" Style="cursor: pointer; display: none"
                                    CssClass="another" OnClick="Button4_Click" align="absmiddle" Height="22px" OnClientClick="return isCheck('合单')" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="display: none;">
                <!--合单不要-->
                <td>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
                        <tr>
                            <td>
                                <hr>
                                ◆ 合单显示：<br>
                                <asp:Button ID="btn_UniteQuery" runat="server" Text="查 询" Style="cursor: pointer;"
                                    CssClass="anyes" OnClick="Button1_Click1" align="absmiddle" Height="22px" />
                                国家<asp:DropDownList ID="DropDownList2" runat="server">
                                </asp:DropDownList>
                                订单状态
                                <asp:DropDownList ID="DropDownList3" runat="server">
                                    <asp:ListItem Value="so.IsSent='N' ">未发货</asp:ListItem>
                                    <asp:ListItem Value="so.IsSent='Y' ">已发货</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%">
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                    Width="100%" bgcolor="#F8FBFD" class="tablemb" HeaderStyle-CssClass="tablebt bbb">
                                    <EmptyDataTemplate>
                                        <table class="tablebt" width="1300px">
                                            <tbody>
                                                <tr id="tr_id">
                                                    <th>
                                                        多选
                                                    </th>
                                                    <th>
                                                        发货
                                                    </th>
                                                    <th>
                                                        删除
                                                    </th>
                                                    <th>
                                                        店铺编号
                                                    </th>
                                                    <th>
                                                        订单号
                                                    </th>
                                                    <th>
                                                        出库单号
                                                    </th>
                                                    <th>
                                                        单据时间
                                                    </th>
                                                    <th>
                                                        订货类型
                                                    </th>
                                                    <th>
                                                        <th>
                                                            订货总重量(kg)
                                                        </th>
                                                        <th>
                                                            运费
                                                        </th>
                                                        <th>
                                                            预计到达时间
                                                        </th>
                                                        <th>
                                                            姓名
                                                        </th>
                                                        <th>
                                                            收货人地址
                                                        </th>
                                                        <th>
                                                            邮编
                                                        </th>
                                                        <th>
                                                            电话
                                                        </th>
                                                        <th>
                                                            发货方式
                                                        </th>
                                                        <th>
                                                            物流公司
                                                        </th>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="多选">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="hCkBox_More" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="发货">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="hLbtn_Sent" runat="server" CommandName="select" OnClick="hLinkButton1_Click"
                                                    OnClientClick="return confirm('确定发货？');">发货</asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="删除">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="hLbtn_Delete" runat="server" CommandName="select" OnClick="hLbtn_Delete_Click"
                                                    OnClientClick="return confirm('确定删除？')">删除</asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="店铺编号">
                                            <ItemTemplate>
                                                <asp:Label ID="hLab_StoreID" runat="server" Text='<%# Bind("StoreID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="订单号">
                                            <ItemTemplate>
                                                <asp:Label ID="hLab_UniteDocID" runat="server" Text='<%# Bind("UniteDocID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="出库单号">
                                            <ItemTemplate>
                                                <asp:Literal ID="hLab_DocIDs" runat="server" Text='<%# SetDocIDsFormat(Eval("DocIDs").ToString()) %>'></asp:Literal>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="单据时间">
                                            <ItemTemplate>
                                                <asp:Literal ID="s" runat="server" Text='<%# SetTimeFormat(Eval("UniteDocTime").ToString()) %>'></asp:Literal>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="购货时间">
                                            <ItemTemplate>
                                                <asp:Label ID="hLabel5" runat="server" Text='<%# Bind("OrderDateTime") %>'></asp:Label>
                                            </ItemTemplate>
                                           
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="期数">
                                            <ItemTemplate>
                                                <asp:Label ID="hLabel6" runat="server" Text='<%# Bind("ExpectNum") %>'></asp:Label>
                                            </ItemTemplate>
                                          
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="到款时间">
                                            <ItemTemplate>
                                                <asp:Label ID="hLabel7" runat="server" Text='<%# Bind("PayMentDateTime") %>'></asp:Label>
                                            </ItemTemplate>
                                           
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="订货类型">
                                            <ItemTemplate>
                                                <asp:Label ID="hLab_OrderType" runat="server" Text='<%# Bind("OrderType") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="总金额">
                                            <ItemTemplate>
                                                <asp:Label ID="hLabel9" runat="server" Text='<%# Bind("TotalMoney") %>'></asp:Label>
                                            </ItemTemplate>                                         
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="总积分">
                                            <ItemTemplate>
                                                <asp:Label ID="hLabel10" runat="server" Text='<%# Bind("TotalPV") %>'></asp:Label>
                                            </ItemTemplate>                                         
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="订货总重量(kg)">
                                            <ItemTemplate>
                                                <asp:Label ID="htxtBox_Weight" runat="server" Text='<%#Eval("Weight", "{0:N2}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="right" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="运费">
                                            <ItemTemplate>
                                                <asp:Label ID="hlab_Carriage" runat="server" Text='<%# Eval("Carriage", "{0:N2}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="预计到达时间" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="hlab_ForeCastArriveDateTime" runat="server" Text='<%# SetTimeFormat(Eval("ForeCastArriveDateTime").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="姓名">
                                            <ItemTemplate>
                                                <asp:Label ID="hLab_InceptPerson" runat="server" Text='<%#  Empty.GetString(Encryption.Encryption.GetDecipherName(Eval("InceptPerson").ToString())) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="收货人国家">
                                            <ItemTemplate>
                                                <asp:Label ID="hlab_Province" runat="server" Text='<%#Eval("country") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="省份">
                                            <ItemTemplate>
                                                <asp:Label ID="hlab_a" runat="server" Text='<%#Eval("province") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="城市">
                                            <ItemTemplate>
                                                <asp:Label ID="hb" runat="server" Text='<%#Eval("city") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="县">
                                            <ItemTemplate>
                                                <asp:Label ID="bx" runat="server" Text='<%#Eval("Xian") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="收货人地址">
                                            <ItemTemplate>
                                                <asp:Label ID="hLab_InceptAddress" runat="server" Text='<%# SetFormatString(Encryption.Encryption.GetDecipherAddress(Eval("InceptAddress").ToString()),15) %>'
                                                    title='<%#Eval("InceptAddress") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="left" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="邮编">
                                            <ItemTemplate>
                                                <asp:Label ID="hLab_PostalCode" runat="server" Text='<%# Bind("PostalCode") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="电话">
                                            <ItemTemplate>
                                                <asp:Label ID="hLab_Telephone" runat="server" Text='<%# Bind("Telephone") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="发货方式">
                                            <ItemTemplate>
                                                <asp:Label ID="hLab_ConveyanceMode" runat="server" Text='<%# Empty.GetString(Eval("ConveyanceMode").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="物流公司">
                                            <ItemTemplate>
                                                <asp:Label ID="hLab_ConveyanceCompany" runat="server" Text='<%# Empty.GetString(Eval("ConveyanceCompany").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="仓库">
                                            <ItemTemplate><!--hLabel19-->
                                                <asp:Label ID="hLab_WareHouseName" runat="server"  Text='<%# Bind("WareHouseName") %>'></asp:Label>
                                            </ItemTemplate>
                                            
                                        </asp:TemplateField>--%>
                                        <%--<asp:TemplateField HeaderText="库位">
                                            <ItemTemplate>
                                                <asp:TextBox ID="hTextBox1" runat="server" Width="50px"></asp:TextBox>
                                                <a href="#" onmouseup=selectKW('<%#Eval("WareHouseID") %>',event,this)>选择</a>
                                            </ItemTemplate>
                                            
                                        </asp:TemplateField>--%>
                                    </Columns>
                                    <HeaderStyle CssClass="tablebt bbb"></HeaderStyle>
                                </asp:GridView>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <uc1:Pager ID="Pager3" runat="server" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="hButton6" runat="server" Text="多选发货" Style="cursor: pointer;" CssClass="another"
                                    OnClick="Button6_Click" align="absmiddle" Height="22px" OnClientClick="return isCheck2('发货')" />
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
    </div>
    <br>
    <br>
    <br>
    <br>
    <br>
    <div id="cssrain" style="width: 100%">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
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
            <table width="100%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
                <tbody style="display: block" id="tbody0">
                    <tr>
                        <td valign="bottom" style="padding-left: 20px">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td align="center">
                                                    <asp:ImageButton ID="btn_Excel" runat="server" OnClick="Button2_Click" CausesValidation="false"
                                                        ImageUrl="images/anextable.gif" />
                                                </td>
                                            </tr>
                                        </table>
                                        <%-- <img src="images/anextable.gif" width="49" height="47" border="0" /></a>
                  &nbsp;&nbsp;&nbsp;&nbsp;<a href="#"><img src="images/anprtable.gif" width="49" height="47" border="0" /></a> --%>
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
                                        <%--<%=GetTran("006852", "1、订单发货。")%>--%>                                       
1、点击发货，对已经出库的订单进行发货操作。
                                        </td><br />
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