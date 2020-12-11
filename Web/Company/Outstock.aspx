<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Outstock.aspx.cs" Inherits="Company_Outstock" %>

<%@ Register src="../UserControl/WareHouseDepotSeat.ascx" tagname="WareHouseDepotSeat" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
    <script src="../JS/QCDS2010.js" type="text/javascript"></script>
    <script type="text/javascript">
        window.onerror = function() {
            return true;
        };
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
        function checkCount(obj) {
            if (obj.value == "") {
                obj.value = "0";
                return;
            }
            if (isNaN(obj.value)) {
                alert('<%=GetTran("007713","产品出库数量只能是数字!") %>');
                obj.value = "0";
                return;
            }
            if (obj.value < 0) {
                alert('<%=GetTran("007714","产品出库数量不能小于0!" )%>');
                obj.value = "0";
                return;
            }
            var txt = document.getElementById(obj.id + "1");
            if (obj.value * 1 > txt.innerHTML * 1) {
                alert('<%=GetTran("007716","产品出库数量不能大于剩余数量!") %>');
                obj.value = "0";
                return;
            }
        }
        function checkPro() {
            var bl = false;
            var inputList = document.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                if (inputList[i].type == "text") {
                    var id = inputList[i].id;
                    if (id.substring(0, 8) == "Repeater") {
                        if (inputList[i].value > 0)
                            bl = true;
                    }
                }
            }
            if (!bl)
                alert('<%=GetTran("007715","产品出库数量不能全部为0!") %>');
            return bl;
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
    <style type="text/css">
        .ddtable td
        {
            white-space:nowrap;
        }
    </style>

    <script type="text/javascript">
    <!--
    //分别是奇数行默认颜色,偶数行颜色,鼠标放上时奇偶行颜色
        var aBgColor = ["#F1F4F8","#FFFFFF","#FFFFCC","#FFFFCC"];
        //从前面iHead行开始变色，直到倒数iEnd行结束
        function addTableListener(o,iHead,iEnd)
        {
            o.style.cursor = "normal";
            iHead = iHead > o.rows.length?0:iHead;
            iEnd = iEnd > o.rows.length?0:iEnd;
            for (var i=iHead;i<o.rows.length-iEnd ;i++ )
            {
                o.rows[i].onmouseover = function(){setTrBgColor(this,true)}
                o.rows[i].onmouseout = function(){setTrBgColor(this,false)}
            }
        }
        function setTrBgColor(oTr, b) {
            oTr.rowIndex % 2 != 0 ? oTr.style.backgroundColor = b ? aBgColor[3] : aBgColor[1] : oTr.style.backgroundColor = b ? aBgColor[2] : aBgColor[0];
        }
        window.onload = function() { addTableListener(document.getElementById("tbColor"), 0, 0); }

        function MM_jumpMenu(targ, selObj, restore) { //v3.0
            eval(targ + ".location='" + selObj.options[selObj.selectedIndex].value + "'");
            if (restore) selObj.selectedIndex = 0;
        }
    //-->
    </script>

    <script type="text/javascript" language="javascript">
        function secBoard(n) {
            for (i = 0; i < secTable.cells.length; i++)
                secTable.cells[i].className = "sec1";
            secTable.cells[n].className = "sec2";
            for (i = 0; i < mainTable.tBodies.length; i++)
                mainTable.tBodies[i].style.display = "none";
            mainTable.tBodies[n].style.display = "block";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server"><br>
        <table width="80%" border="0" cellpadding="0" cellspacing="1" bgcolor="#F8FBFD" class="tablemb ddtable"  id="tbColor">
            <tr>
                <th colspan="4" align="center" background="images/tabledp.gif" class="tablebt">
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                </th>
            </tr>
            <tr style="background-color:white">
                <td align="right">
                    <%=GetTran("001216","单据日期")%>：</td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server" ReadOnly="true" style="color:gray"></asp:TextBox>
                </td>
                <td align="right">
                    <%=GetTran("000079","订单号")%>：</td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server" ReadOnly="true" style="color:gray"></asp:TextBox>
                </td>
            </tr>
            <tr style="background-color:#F1F4F8">
                <td align="right">
                    <%=GetTran("001271","出库类别")%>：
                </td>
                <td>
                    <asp:TextBox ID="TextBox3" runat="server" ReadOnly="true" style="color:gray"></asp:TextBox>
                </td>
                <td align="right">
                    <%=GetTran("000519","经办人")%>：
                </td>
                <td>
                    <asp:TextBox ID="TextBox4" runat="server" ReadOnly="true" style="color:gray"></asp:TextBox>
                </td>
            </tr>
            <tr style="background-color:white">
                <td align="right">
                    <%=GetTran("001273","原始单据号")%>：
                </td>
                <td>
                     <asp:TextBox ID="TextBox5" runat="server" ReadOnly="true" style="color:gray"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Label ID="fhck" runat="server" ><%= GetTran("001279","发货仓库")%>：</asp:Label>
                </td>
                <td>
                     <asp:DropDownList ID="DropDownList1" runat="server" 
                         DataTextField="WareHouseName" DataValueField="WareHouseID" AutoPostBack="True" 
                         onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                     </asp:DropDownList>
                    
                     <%=GetTran("000390","库位")%>：<asp:DropDownList ID="DropDownList2" runat="server" DataTextField="SeatName" DataValueField='DepotSeatID'>    
                     </asp:DropDownList>
                </td>
            </tr>
            <tr  style="background-color:#F1F4F8">
                <td align="right">
                    <%=GetTran("001276","附加信息")%>：</td>
                <td colspan="2">
                    <asp:TextBox ID="TextBox6" runat="server" TextMode="MultiLine"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text='确定出库' 
                                             CssClass="another" onclick="Button1_Click" align="absmiddle" Height="22px" OnClientClick="return checkRep(this);" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button3" runat="server" Text='返 回'
                         CssClass="anyes" align="absmiddle" Height="22px" onclick="Button3_Click" />                    
                </td>
            </tr>
        </table>
        <table width="80%" border="0" cellpadding="0" cellspacing="1" bgcolor="#F8FBFD" class="tablemb ddtable">
            <asp:Repeater ID="Repeater1" runat="server">
                <HeaderTemplate>
                    <tr class="tablebt" align="center">
                        <th><%=GetTran("000558", "产品编号")%></th>
                        <th><%=GetTran("000501", "产品名称")%></th>
                        <th><%=GetTran("000503", "单价")%></th>
                        <th><%=GetTran("000414", "积分（PV）")%></th>
                        <th><%=GetTran("007718", "订购数量")%></th>
                        <th><%=GetTran("007719", "已出库数量")%></th>
                        <th><%=GetTran("007720", "剩余数量")%></th>
                        <th><%=GetTran("000362", "出库数量")%></th>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                     <tr>
                        <td><asp:HiddenField ID="HF" runat="server" Value='<%# Eval("productid") %>' />
                            <asp:Label ID="lblCode" runat="server" Text='<%# Eval("productcode") %>'>
                        </asp:Label></td>
                        <td><%# Eval("productname") %></td>
                        <td align="right">
                            <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("price") %>'></asp:Label></td>
                        <td align="right"><asp:Label ID="lblPV" runat="server" Text='<%# Eval("pv") %>'></asp:Label></td>
                        <td align="right"><%# Eval("quantity") %></td>
                        <td align="right"><%# Eval("outbillquantity")%></td>
                        <td align="right"> 
                            <asp:Label ID="txtCount1" runat="server" Text='<%# Eval("count") %>'></asp:Label></td>
                        <td align="center">
                            <asp:TextBox ID="txtCount" runat="server" Width="40" MaxLength="9" Text='<%# Eval("count") %>' onblur="checkCount(this);"></asp:TextBox>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </form>
</body>
</html>
<script type="text/javascript">
    function checkRep(obj) {
        if (obj.value != '<%= GetTran("008047","正在出库...") %>') {
            obj.value = '<%= GetTran("008047","正在出库...") %>';
            return true;
        } else
            return false;
    }
</script>