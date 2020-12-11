<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BillOutOrder.aspx.cs" Inherits="Company_BillOutOrder" EnableEventValidation="false"%>

<%@ Register src="../UserControl/Pager.ascx" tagname="Pager" tagprefix="uc1" %>
<%@ Register src="../UserControl/CountryCityPCode.ascx" tagname="CountryCityPCode" tagprefix="uc2" %>
<%@ Register src="../UserControl/Country.ascx" tagname="Country" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
    <script src="../JS/QCDS2010.js" type="text/javascript"></script>
    <script type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        window.onerror=function ()
        {
            //return true;
        };
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
    	
	    //全选
        function allCk() {
            var trarr = document.getElementById("GridView_BillOutOrder").getElementsByTagName("tr");

            var isCk = false;
            if (document.getElementById("ckQck").checked)
                isCk = true;

            for (var i = 1; i < trarr.length; i++) {
                trarr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked = isCk;
            }
        }
    </script>

    <SCRIPT language="javascript" type="text/javascript">
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

        function isChuKu() {
            return confirm('<%=GetTran("001115","确定出库？")%>');
        }

        function isCheDan() {
            return confirm('<%=GetTran("001178","确定要撤单吗？")%>');
        }
      
        //币种变时，钱也要变。（表格列数变化时一定要更改此方法）
        var first=true; 
        var from=0;
        function setHuiLv_II() {
            var th = document.getElementById("Dropdownlist2");
            if (first) {
                from = AjaxClass.GetCurrency().value - 0;
                first = false;
            }

            var to = th.options[th.selectedIndex].value - 0;


            var hl = AjaxClass.GetCurrency_Ajax(from, to).value;

            var trarr = document.getElementById("GridView2_BilloutOrder").getElementsByTagName("tr");
            for (var i = 1; i < trarr.length; i++) {
                trarr[i].getElementsByTagName("td")[7].getElementsByTagName("span")[0].innerHTML =
                    (parseFloat(trarr[i].getElementsByTagName("td")[7].getElementsByTagName("span")[0].firstChild.nodeValue.replace(/,/g, "")) / hl).toFixed(2);
            }

        }
    </SCRIPT>
    <script type="text/javascript">
        function cut() {
            document.getElementById("span1").title = '<%=GetTran("000032", "管 理") %>';
        }
        function cut1() {
            document.getElementById("span2").title = '<%=GetTran("000033", "说 明") %>';
        }
            
        //币种变时，钱也要变。（表格列数变化时一定要更改此方法）
        var first=true; 
        var from=0;
        function setHuiLv() {
            var th = document.getElementById("Dropdownlist2");

            if (first) {
                from = AjaxClass.GetCurrency().value - 0;
                first = false;
            }

            var to = th.options[th.selectedIndex].value - 0;


            var hl = AjaxClass.GetCurrency_Ajax(from, to).value;

            var trarr = document.getElementById("GridView_BillOutOrder").getElementsByTagName("tr");
            for (var i = 1; i < trarr.length; i++) {
                trarr[i].getElementsByTagName("td")[6].getElementsByTagName("span")[0].innerHTML =
                    (parseFloat(trarr[i].getElementsByTagName("td")[6].getElementsByTagName("span")[0].firstChild.nodeValue.replace(/,/g, "")) / hl).toFixed(2);
            }

            from = to;
        }

        function selectMode() {
            //            var th=document.getElementById("DropDownList1");
            //            if(th.options[th.selectedIndex].value=="isGeneOutBill='N' ")
            //            {
            //                document.getElementById("Dropdownlist2").onchange=setHuiLv;
            //            }
            //            else
            //            {
            //                document.getElementById("Dropdownlist2").onchange=setHuiLv_II;
            //            }
        }
    </script>
    <script type="text/javascript" src="../js/SqlCheck.js"></script>
</head>
<body>
<form id="form1" runat="server" onsubmit="return filterSql_III()">
    <div style="width:100%"><br>
        <table style="width:100%">
            <tr>
                <td>
				   <table style="width:100%">
				        <tr>
				            <td>
				                <table border="0" cellpadding="0" cellspacing="0" class="biaozzi">
				                    <tr>
				                        <td>
				                            <asp:Button ID="btn_Submit" runat="server" Text="查 询" 
                                            style="cursor:pointer;"  CssClass="anyes"
                                                onclick="btn_Submit_Click" align="absmiddle" Height="22px" />&nbsp;&nbsp;
                                                
				                            <%=GetTran("000047","国家")%>：<asp:DropDownList ID="DropCurrery" runat="server">
                                            </asp:DropDownList>
                            
                                            <asp:DropDownList ID="DropDownList_Items" runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="DropDownList_Items_SelectedIndexChanged">
                                                <asp:ListItem Value="so.StoreOrderID">订单号</asp:ListItem>
                                                <asp:ListItem Value="so.InceptPerson">收货人姓名</asp:ListItem>
                                                <asp:ListItem Value="〖Country〗">收货人国家</asp:ListItem>
                                                <asp:ListItem Value="〖Province〗">收货人省份</asp:ListItem>
                                                <asp:ListItem Value="〖City〗">收货人城市</asp:ListItem>
                                                <asp:ListItem Value="so.InceptAddress">收货人地址</asp:ListItem>
                                                <asp:ListItem Value="so.Telephone">收货人电话</asp:ListItem>                                                
                                                <asp:ListItem Value="so.OrderDateTime">订货日期</asp:ListItem>                                                
                                                <asp:ListItem Value="so.TotalMoney">订货总金额</asp:ListItem>                                                
                                                <asp:ListItem Value="m.Number">会员编号</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:dropdownlist id="DropDownList_condition" runat="server">                                               
                                            </asp:dropdownlist>
                                            <asp:TextBox ID="TextBox4" runat="server" Width="80px" MaxLength="100"></asp:TextBox>
                                            <asp:TextBox ID="txtBox_rq" Visible="false" runat="server" Width="80px" onfocus="new WdatePicker()" CssClass="Wdate"></asp:TextBox>
                                            <%=GetTran("000060")%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td><%=GetTran("000640", "订单状态")%>：</td>
				                        <td> 
                                            <asp:RadioButtonList ID="DropDownList1"  RepeatDirection="Horizontal" runat="server" style=" margin-left:0px;">
                                                <asp:ListItem Value="so.isGeneOutBill='N'" Selected="True">未出库</asp:ListItem>
												<asp:ListItem Value="so.isGeneOutBill='Y'">部分出库</asp:ListItem>
												<asp:ListItem Value="so.isGeneOutBill='A'">全部出库</asp:ListItem>
                                            </asp:RadioButtonList>
				                        </td>
				                        <td style="display:none;">
				                             &nbsp;&nbsp;&nbsp;&nbsp;<%=GetTran("000562")%>：
                                            <asp:dropdownlist id="Dropdownlist2" runat="server" Width="100px" 
                                    EnableViewState="False"></asp:dropdownlist>    
				                        </td>
				                    </tr>
				                    </table>
				                <table style="width:100%">
				                    <tr>
				                        <td style="border:rgb(147,226,244) solid 1px"> 
                                            <asp:GridView ID="GridView_BillOutOrder" runat="server" AutoGenerateColumns="False" 
                                                onselectedindexchanged="GridView1_SelectedIndexChanged"  width="100%" 
                                                class="tablemb bordercss"  
                                                HeaderStyle-CssClass="tablebt bbb" 
                                                onrowdatabound="GridView_BillOutOrder_RowDataBound">
                                                <EmptyDataTemplate>
                                                <table cellspacing="0" width="100%">
                                                    <tr>
                                                        <th nowrap><%=GetTran("001017","出库")%></th>
                                                        <%--<th nowrap><%=GetTran("000098","订货店铺")%></th>--%>
                                                        <th nowrap><%=GetTran("000079","订单号")%></th>
                                                        <%--<th nowrap><%=GetTran("000328", "是否发货")%></th>--%>
                                                        <th>会员编号</th>
                                                        <th>会员昵称</th>
                                                        <th>会员姓名</th>
                                                        <th nowrap><%=GetTran("000045","期数")%></th>
                                                        <th nowrap><%=GetOrderType( GetTran("000106","订单类型"))%></th>
                                                        <th nowrap><%=GetTran("000041","总金额")%></th>
                                                        <th nowrap><%=GetTran("000113","总积分")%></th>
                                                        <th nowrap><%=GetTran("000383", "收货人姓名")%></th>
                                                        <th nowrap>
                                                            <%=GetTran("000108", "收货人国家")%>
                                                        </th>
                                                        <th nowrap>
                                                            <%=GetTran("000109","省份")%>
                                                        </th>
                                                        <th nowrap>
                                                            <%=GetTran("000110","城市")%>
                                                        </th>
                                                        <th nowrap><%=GetTran("000112","收货地址")%></th>
                                                        <th nowrap><%=GetTran("000646","电话")%></th>
                                                        <th nowrap><%=GetTran("000118","重量")%></th>
                                                        <th nowrap><%=GetTran("000067","订货日期")%></th>
                                                        <th nowrap>出库日期</th>
                                                        <th nowrap><%=GetTran("000744","查看备注")%></th>
                                                    </tr>                
                                                </table>
                                            </EmptyDataTemplate>
                                                <Columns>
                                                      <asp:TemplateField HeaderText="全选" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CheckBox1" runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                        <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="出库">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btn_BillOutOrder" runat="server"  onclick="Button1_Click" CommandName="select"><%=GetTran("001017","出库")%></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                        <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="详细">
                                                        <ItemTemplate>
                                                            <img src="images/fdj.gif" /><asp:LinkButton ID="lbtn_Details" runat="server" CommandName="select" 
                                                                onclick="LinkButton1_Click"><%=GetTran("000339", "详细")%></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                        <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="订货店铺" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lab_StoreID" runat="server" Text='<%# Bind("StoreID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                        <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="订单号">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lab_StoreOrderID" runat="server" Text='<%# Bind("StoreOrderID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                        <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                                                    </asp:TemplateField>
   
                                                    <asp:TemplateField HeaderText="会员编号">
                                                        <ItemTemplate>
                                                            <asp:Label ID="a1" runat="server" Text='<%#Eval("Number") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                        <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                                                    </asp:TemplateField>
<%--                                                    <asp:TemplateField HeaderText="会员昵称">
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
                                                    <asp:TemplateField HeaderText="是否发货" Visible="false">
                                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                                        <ItemTemplate>
                                                            <%#GetSentType(DataBinder.Eval(Container,"DataItem.isSent").ToString()) %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="期数">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lab_ExpectNum" runat="server" Text='<%# Bind("ExpectNum") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="订货类型">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lab_OrderType" runat="server" Text='<%# GetOrderType(Eval("OrderType").ToString()) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                        <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="总金额">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lab_TotalMoney" runat="server" 
                                                                Text='<%# Bind("TotalMoney", "{0:N2}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" Wrap="false" />
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="总积分" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lab_TotalPV" runat="server" Text='<%# Bind("TotalPV", "{0:N2}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                        <ItemStyle HorizontalAlign="right"  Wrap="false"/>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="付款否" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lab_IsCheckOut" runat="server" Text='<%# StringFormat(Eval("IsCheckOut").ToString()) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                         <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="付款日期" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lab_PayMentDateTime" runat="server" Text='<%# GetBiaoZhunTime(Eval("PayMentDateTime").ToString()) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                        <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="收货人姓名">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lab_InceptPerson" runat="server" Text='<%# Encryption.Encryption.GetDecipherName(Eval("InceptPerson").ToString()) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="收货人国家">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lab_Province" runat="server" Text='<%#Eval("country") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap=false />
                                                        <HeaderStyle Wrap=false />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="省份">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lab_a" runat="server" Text='<%#Eval("province") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap=false />
                                                        <HeaderStyle Wrap=false />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="城市">
                                                        <ItemTemplate>
                                                            <asp:Label ID="b" runat="server" Text='<%#Eval("city") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap=false />
                                                        <HeaderStyle Wrap=false />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="县">
                                                        <ItemTemplate>
                                                            <asp:Label ID="bx" runat="server" Text='<%#Eval("Xian") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap=false />
                                                        <HeaderStyle Wrap=false />
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="收货人地址">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lab_InceptAddress" runat="server" Text='<%# SetFormatString(Encryption.Encryption.GetDecipherAddress(Eval("InceptAddress").ToString()),15) %>' title='<%#Encryption.Encryption.GetDecipherAddress(Eval("InceptAddress").ToString()) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                        <ItemStyle HorizontalAlign="left" Wrap="false" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="电话">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lab_Telephone" runat="server" Text='<%# Encryption.Encryption.GetDecipherTele(Eval("Telephone").ToString()) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                        <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="订货总重量">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lab_Weight" runat="server" Text='<%# Bind("Weight", "{0:N2}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                        <ItemStyle HorizontalAlign="right"  Wrap="false"/>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="运费" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lab_Carriage" runat="server" Text='<%# Bind("Carriage", "{0:N2}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                        <ItemStyle HorizontalAlign="Right"  Wrap="false"/>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="订货日期">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lab_OrderDateTime" runat="server" Text='<%#GetBiaoZhunTime(Eval("OrderDateTime").ToString()) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                        <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="出库日期">
                                                        <ItemTemplate>
                                                            <asp:Label ID="sLab_OrderDateTime2" runat="server" Text='<%#GetBiaoZhunTime(Eval("AuditingDate").ToString()) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                        <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="查看备注">
                                                        <ItemTemplate>
                                                           <img src="images/fdj.gif" /> <asp:Label ID="Lab_Description" runat="server" Text='<%#GetRemark(Eval("Description").ToString(),Eval("StoreOrderID").ToString()) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                                    </asp:TemplateField>
                                                </Columns>

                                                <HeaderStyle CssClass="tablebt bbb" Wrap="false"></HeaderStyle>
                                                <AlternatingRowStyle BackColor="#F1F4F8" />
                                            </asp:GridView>
                                            <asp:GridView ID="GridView2_BilloutOrder" runat="server" AutoGenerateColumns="False" 
                                                onselectedindexchanged="GridView1_SelectedIndexChanged"  width="100%" 
                                                 class="tablemb bordercss"  
                                     HeaderStyle-CssClass="tablebt bbb" 
                                                onrowdatabound="GridView2_BilloutOrder_RowDataBound">
                                                <EmptyDataTemplate>
                                                    <table cellspacing="0"   width="100%" >
                                                        <tr>
                                                        <%--<th nowrap><%=GetTran("001060", "撤单")%></th>--%>
                                                        <th nowrap><%=GetTran("000079","订单号")%></th>
                                                        <th>会员编号</th>
                                                        <th>会员昵称</th>
                                                        <th>会员姓名</th>
                                                        <th nowrap><%=GetTran("000098","订货店铺")%></th>
                                                        <th nowrap><%=GetTran("000099", "对应出库单号")%></th>
                                                        <th nowrap><%=GetTran("000045","期数")%></th>
                                                        <th nowrap><%=GetTran("000106","订单类型")%></th>
                                                        <th nowrap><%=GetTran("000041","总金额")%></th>
                                                        <th nowrap><%=GetTran("000113","总积分")%></th>
                                                        <th nowrap><%=GetTran("000383", "收货人姓名")%></th>
                                                        <th nowrap>
                                                            <%=GetTran("000108", "收货人国家")%>
                                                        </th>
                                                        <th nowrap>
                                                            <%=GetTran("000109","省份")%>
                                                        </th>
                                                        <th nowrap>
                                                            <%=GetTran("000110","城市")%>
                                                        </th>
                                                        <th nowrap><%=GetTran("000112","收货地址")%></th>
                                                        <th nowrap><%=GetTran("000646","电话")%></th>
                                                        <th nowrap><%=GetTran("000118","重量")%></th>
                                                        <th nowrap><%=GetTran("000067","订货日期")%></th>
                                                        <th nowrap>出库日期</th>
                                                        <th nowrap><%=GetTran("000744","查看备注")%></th>
                                                    </tr>                        
                                                    </table>
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:TemplateField Visible="false" HeaderText="撤单">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="slbtn_Disfrock" runat="server" CommandName="select" visible='<%#GetFF(Eval("isSent").ToString())%>'
                                                                OnClientClick="return isCheDan()" onclick="sLinkButton2_Click"><%=GetTran("001060", "撤单")%></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                        <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="详细">
                                                        <ItemTemplate>
                                                            <img src="images/fdj.gif" /><asp:LinkButton ID="slbtn_Details" runat="server" CommandName="select" onclick="sLinkButton1_Click" 
                                                                ><%=GetTran("000339", "详细")%></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                        <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="订货店铺" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="sLab_StoreID" runat="server" Text='<%# Bind("StoreID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                        <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="订单号">
                                                        <ItemTemplate>
                                                            <asp:Label ID="sLab_StoreOrderID" runat="server" Text='<%# Bind("StoreOrderID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                        <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="会员编号">
                                                        <ItemTemplate>
                                                            <asp:Label ID="a1" runat="server" Text='<%#Eval("Number") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                        <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
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
                                           <%--         <asp:TemplateField HeaderText="出库单号">
                                                        <ItemTemplate><!--sLab4-->
                                                            <asp:Label ID="sLab_OutStorageOrderID" runat="server" Text='<%#Bind ("OutStorageOrderID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                        <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="期数">
                                                        <ItemTemplate>
                                                            <asp:Label ID="sLab_ExpectNum" runat="server" Text='<%# Bind("ExpectNum") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="订货类型">
                                                        <ItemTemplate>
                                                            <asp:Label ID="sLab_OrderType" runat="server" Text='<%# GetOrderType(Eval("OrderType").ToString()) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                        <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="总金额">
                                                        <ItemTemplate>
                                                            <asp:Label ID="sLab_TotalMoney" runat="server" 
                                                                Text='<%# Bind("TotalMoney", "{0:N2}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                        <ItemStyle HorizontalAlign="Right"  Wrap="false"/>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="总积分">
                                                        <ItemTemplate>
                                                            <asp:Label ID="sLab_TotalPV" runat="server" Text='<%# Bind("TotalPV", "{0:N2}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                        <ItemStyle HorizontalAlign="right"  Wrap="false"/>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="付款否" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="sLab_IsCheckOut" runat="server" Text='<%# StringFormat(Eval("IsCheckOut").ToString()) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="付款日期" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="sLab_PayMentDateTime" runat="server" Text='<%# GetBiaoZhunTime(Eval("PayMentDateTime").ToString()) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="收货人姓名">
                                                        <ItemTemplate>
                                                            <asp:Label ID="sLab_InceptPerson" runat="server" Text='<%# Encryption.Encryption.GetDecipherName(Eval("InceptPerson").ToString()) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                        <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                                                    </asp:TemplateField>                                                    
                                                    <asp:TemplateField HeaderText="收货人国家">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lab_Province" runat="server" Text='<%#Eval("country") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap=false />
                                                        <HeaderStyle Wrap=false />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="省份">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lab_a" runat="server" Text='<%#Eval("province") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap=false />
                                                        <HeaderStyle Wrap=false />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="城市">
                                                        <ItemTemplate>
                                                            <asp:Label ID="b" runat="server" Text='<%#Eval("city") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap=false />
                                                        <HeaderStyle Wrap=false />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="县">
                                                        <ItemTemplate>
                                                            <asp:Label ID="bx" runat="server" Text='<%#Eval("Xian") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap=false />
                                                        <HeaderStyle Wrap=false />
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="收货人地址">
                                                        <ItemTemplate>
                                                            <asp:Label ID="sLab_InceptAddress" runat="server" Text='<%# SetFormatString(Encryption.Encryption.GetDecipherAddress(Eval("InceptAddress").ToString()),15) %>' title='<%#Encryption.Encryption.GetDecipherAddress(Eval("InceptAddress").ToString()) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                        <ItemStyle HorizontalAlign="left"  Wrap="false"/>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="电话">
                                                        <ItemTemplate>
                                                            <asp:Label ID="sLab_Telephone" runat="server" Text='<%# Encryption.Encryption.GetDecipherTele(Eval("Telephone").ToString()) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="订货总重量">
                                                        <ItemTemplate>
                                                            <asp:Label ID="sLab_Weight" runat="server" Text='<%# Bind("Weight", "{0:N2}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                        <ItemStyle HorizontalAlign="right" Wrap="false" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="运费" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="sLab_Carriage" runat="server" Text='<%# Bind("Carriage", "{0:N2}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                        <ItemStyle HorizontalAlign="Right" Wrap="false" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="订货日期">
                                                        <ItemTemplate>
                                                            <asp:Label ID="sLab_OrderDateTime" runat="server" Text='<%#GetBiaoZhunTime(Eval("OrderDateTime").ToString()) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                        <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="出库日期">
                                                        <ItemTemplate>
                                                            <asp:Label ID="sLab_OrderDateTime2" runat="server" Text='<%#GetBiaoZhunTime(Eval("AuditingDate").ToString()) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                        <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="查看备注">
                                                        <ItemTemplate>
                                                           <img src="images/fdj.gif" /> <asp:Label ID="sLab_Description" runat="server" Text='<%#GetRemark(Eval("Description").ToString(),Eval("StoreOrderID").ToString()) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                        <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                                                    </asp:TemplateField>
                                                </Columns>

                                                <HeaderStyle CssClass="tablebt bbb" Wrap="false"></HeaderStyle>
                                                <AlternatingRowStyle BackColor="#F1F4F8" />
                                            </asp:GridView>
				                        </td>
				                        <td>
				                            &nbsp;
				                        </td>
				                    </tr>
				                    <tr style="display:none;">
				                        <td style="font-size:10pt">
                                            <asp:Panel ID="Panel1" runat="server">
                                                <input type='checkbox' id="ckQck" onclick="allCk()"> <label id="a" for="ckQck">全选</label>
				                                <asp:Button ID="Button1" runat="server" Text="出 库" 
                                                style="cursor:pointer;"  CssClass="anyes"
                                                     align="absmiddle" Height="22px" onclick="Button1_Click1" />
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
				                </table>
				            </td>
				        </tr>
				   </table>
                </td>
            </tr>
        </table>
    </div>
    <div id="cssrain" style="width:100%">
        <table width="99%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
        <tr>
          <td width="150"><table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
              <tr>
                <td class="sec2" onclick="secBoard(0)">
                    <span id="span1" title="" onmouseover="cut()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000032", "管 理"))%></span>
                </td>
                <td class="sec1" onclick="secBoard(1)">
                    <span id="span2" title="" onmouseover="cut1()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
                </td>
              </tr>
          </table></td>
          <td><a href="#"><img src="images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX" onclick="down2()"/></a></td>
        </tr>
      </table>
      
        <div id="divTab2">
      <table width="99%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
        <tbody style="DISPLAY: block" id="tbody0">
          <tr>
            <td valign="bottom" style="padding-left:20px"><table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td><a href="#">
                  <asp:ImageButton ID="btn_Excel" runat="server"  
                        onclick="Button2_Click" ImageUrl="images/anextable.gif"/>
                  <%-- <img src="images/anextable.gif" width="49" height="47" border="0" /></a>
                  &nbsp;&nbsp;&nbsp;&nbsp;<a href="#"><img src="images/anprtable.gif" width="49" height="47" border="0" /></a> --%></td>
                </tr>
            </table></td>
          </tr>
        </tbody>
        <tbody style="DISPLAY: none" id="tbody1">
          <tr>
            <td style="padding-left:20px"><table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td> <%--<%=GetTran("006850", "1、订单出库。")%>--%>1、点击出库，对已经支付的订单进行出库操作。</td>
                  <br />
                   
                </tr>
            </table></td>
          </tr>
        </tbody>
      </table>
	  </div>
    </div>
</form>
</body>
</html>