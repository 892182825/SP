<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowOrderGoods.aspx.cs" Inherits="Company_ShowOrderGoods" %>

<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>

    <script type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>

    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />

    <script src="../JS/QCDS2010.js" type="text/javascript"></script>

    <script type="text/javascript">
        window.onerror=function ()
        {
            return true;
        };
    </script>

    <script>
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
	
	window.onload=function()
	{
	    down2();
	};
    </script>

    <script language="javascript">
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
      
      function isDelete()
      {
         return window.confirm('<%=GetTran("000248")%>');
      }
    </script>

    <script type="text/javascript">
        function cut()
        {
             document.getElementById("span1").title='<%=GetTran("000032", "管 理") %>';
        }
        function cut1()
        {
             document.getElementById("span2").title='<%=GetTran("000033", "说 明") %>';
        }
        
    </script>

    <script type="text/javascript" src="../js/SqlCheck.js"></script>

</head>
<body>
    <form id="form1" runat="server" onsubmit="return filterSql_III()">
    <div>
        <br>
        <table>
            <tr>
                <td>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
                        <tr>
                            <td>
                                <asp:Button ID="butt_Query" runat="server" Text="查 询" Style="cursor: pointer" CssClass="anyes"
                                    OnClick="Button1_Click" align="absmiddle" Height="22px" />&nbsp;&nbsp;
                                <%=GetTran("000047")%>：<asp:DropDownList ID="DropCurrency" runat="server">
                                </asp:DropDownList>
                                <asp:DropDownList ID="DropDownList_Items" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_Items_SelectedIndexChanged">
                                    <asp:ListItem Value="StoreID">订货店铺</asp:ListItem>
                                    <asp:ListItem Value="StoreOrderID">订单号</asp:ListItem>
                                    <asp:ListItem Value="InceptPerson">收货人姓名</asp:ListItem>
                                    <asp:ListItem Value="〖Country〗">收货人国家</asp:ListItem>
                                    <asp:ListItem Value="〖Province〗">收货人省份</asp:ListItem>
                                    <asp:ListItem Value="〖City〗">收货人城市</asp:ListItem>
                                    <asp:ListItem Value="InceptAddress">收货人地址</asp:ListItem>
                                    <asp:ListItem Value="Telephone">收货人电话</asp:ListItem>
                                    <asp:ListItem Value="TotalMoney">总价格</asp:ListItem>
                                    <asp:ListItem Value="ExpectNum">期数</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="DropDownList_condition" runat="server">
                                </asp:DropDownList>
                                <asp:TextBox ID="txtBox_keyWords" runat="server" Width="80px" MaxLength="100"></asp:TextBox>
                                <asp:TextBox ID="txtBox_rq" Visible="false" runat="server" Width="80px" onfocus="new WdatePicker()"
                                    CssClass="Wdate"></asp:TextBox>
                                &nbsp;<asp:DropDownList ID="DDLSendWay" Visible="false" runat="server">
                                    <asp:ListItem Selected="True" Value="-1">全部</asp:ListItem>
                                    <asp:ListItem Value="0">公司发货到店铺</asp:ListItem>
                                    <asp:ListItem Value="1">公司直接发货给会员</asp:ListItem>
                                </asp:DropDownList>
                                <%=GetTran("000060")%>&nbsp;&nbsp;
                                <%=GetTran("000067")%>：<!--TextBox1--><asp:TextBox ID="txtBox_OrderDateTimeStart"
                                    runat="server" Width="80px" CssClass="Wdate" onfocus="new WdatePicker()"></asp:TextBox>
                                <%=GetTran("000068")%>：<!--TextBox2--><asp:TextBox ID="txtBox_OrderDateTimeEnd" runat="server"
                                    Width="80px" onfocus="new WdatePicker()" CssClass="Wdate"></asp:TextBox>
                                &nbsp;
                                <%=GetTran("000070")%>：<asp:TextBox ID="txtBox_ConsignmentDateTimeStart" Width="80px"
                                    runat="server" onfocus="new WdatePicker()" CssClass="Wdate"></asp:TextBox>
                                <%=GetTran("000068")%>：
                                <!--TextBox4-->
                                <asp:TextBox ID="txtBox_ConsignmentDateTimeEnd" Width="80px" runat="server" onfocus="new WdatePicker()"
                                    CssClass="Wdate"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <br>
                    <table style="width: 100%;">
                        <tr>
                            <td style="border: rgb(147,226,244) solid 1px">
                                <!--GridView1-->
                                <asp:GridView ID="GridView_Order" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                    Width="100%" CssClass="tablemb bordercss" OnRowDataBound="GridView_Order_RowDataBound">
                                    <EmptyDataTemplate>
                                        <table cellspacing="0" style="width: 100%;">
                                            <tr>
                                                <%--<th nowrap>
                                                    <%=GetTran("000022")%>
                                                </th>--%>
                                                <th nowrap>
                                                    <%=GetTran("000079")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000098")%>
                                                </th>
                                                <!-- 
                                                <th nowrap>
                                                    <%=GetTran("000099")%>
                                                </th>
                                                -->
                                                <th nowrap>
                                                    <%=GetTran("000045")%>
                                                </th>
                                                <!-- 
                                                <th nowrap>
                                                    <%=GetTran("000102")%>
                                                </th>
                                                                                               
                                                <th nowrap>
                                                    <%=GetTran("000104")%>
                                                </th>-->
                                                <th nowrap>
                                                    <%=GetTran("000106")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000107")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000108")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000109")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000110")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000112")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000073")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000041")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000113")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000115")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000118")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000067")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000078")%>
                                                </th>
                                                <!--
                                                <th nowrap>
                                                    <%=GetTran("000127")%>
                                                </th>
                                                -->
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="删除" Visible="false">
                                            <ItemTemplate>
                                                <!--LinkButton1-->
                                                <asp:LinkButton ID="lButt_Delete" runat="server" Visible='<%#IsDelete(Eval("IsCheckOut").ToString()) %>'
                                                    OnClick="LinkButton1_Click" CommandName="select" OnClientClick="return isDelete()"><%=GetTran("000022")%></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="详细">
                                            <ItemTemplate>
                                                <!--lkb-->
                                                <img src="images/fdj.gif" />
                                                <asp:LinkButton ID="lButt_Details" runat="server" CommandName="select" OnClick="lkb_Click"><%=GetTran("000339", "详细")%></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="订货店铺">
                                            <ItemTemplate>
                                                <!--Label3-->
                                                <asp:Label ID="Lab_StoreID" runat="server" Text='<%#Empty.GetString(Eval("StoreID").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="订单号">
                                            <ItemTemplate>
                                                <!--Label4-->
                                                <asp:Label ID="Lab_StoreOrderID" runat="server" Text='<%#Empty.GetString(Eval("OrderGoodsID").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="期数">
                                            <ItemTemplate>
                                                <!--Label6-->
                                                <asp:Label ID="Lab_ExpectNum" runat="server" Text='<%#Empty.GetString(Eval("ExpectNum").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="发货方式">
                                            <ItemTemplate>
                                                <%# GetSendWay(DataBinder.Eval(Container.DataItem,"SendWay").ToString())%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="付款否" Visible="false">
                                            <ItemTemplate>
                                                <!--Label7-->
                                                <asp:Label ID="Lab_IsCheckOut" runat="server" Text='<%#StringFormat(Eval("IsCheckOut").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="订单类型">
                                            <ItemTemplate>
                                                <!--Label10-->
                                                <asp:Label ID="Lab_OrderType" runat="server" Text='<%#GetOrderType(Eval("OrderType").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="姓名">
                                            <ItemTemplate>
                                                <!--Label11-->
                                                <asp:Label ID="Lab_InceptPerson" runat="server" Text='<%# Empty.GetString(Encryption.Encryption.GetDecipherName(Eval("InceptPerson").ToString())) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="收货人国家">
                                            <ItemTemplate>
                                                <!--Label10-->
                                                <asp:Label ID="lab_Province" runat="server" Text='<%#Eval("country") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="收货地址">
                                            <ItemTemplate>
                                                <!--Label12-->
                                                <asp:Label ID="Lab_InceptAddress" runat="server" Text='<%#SetFormatString(Encryption.Encryption.GetDecipherAddress(Eval("InceptAddress").ToString()),15) %>'
                                                    title='<%#Encryption.Encryption.GetDecipherAddress(Eval("InceptAddress").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="left" Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="邮编">
                                            <ItemTemplate>
                                                <!--Label13-->
                                                <asp:Label ID="Lab_PostalCode" runat="server" Text='<%#Empty.GetString(Eval("PostalCode").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="总金额">
                                            <ItemTemplate>
                                                <!--Label14-->
                                                <asp:Label ID="Lab_TotalMoney" runat="server" Text='<%#Empty.GetString(Eval("TotalMoney", "{0:N2}").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="总积分">
                                            <ItemTemplate>
                                                <!--Label15-->
                                                <asp:Label ID="Lab_TotalPV" runat="server" Text='<%#Empty.GetString(Eval("TotalPV", "{0:N2}").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="right" Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="联系电话">
                                            <ItemTemplate>
                                                <!--Label16-->
                                                <asp:Label ID="Lab_Telephone" runat="server" Text='<%#Empty.GetString(Encryption.Encryption.GetDecipherTele(Eval("Telephone").ToString())) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="重量" Visible="false">
                                            <ItemTemplate>
                                                <!--Label17-->
                                                <asp:Label ID="Lab_Weight" runat="server" Text='<%#Empty.GetString(Eval("Weight", "{0:N2}").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="right" Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="运费" Visible="false">
                                            <ItemTemplate>
                                                <!--Label18-->
                                                <asp:Label ID="Lab_Carriage" runat="server" Text='<%#Empty.GetString(Eval("Carriage", "{0:N2}").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="订货日期">
                                            <ItemTemplate>
                                                <!--Label20-->
                                                <asp:Label ID="Lab_OrderDateTime" runat="server" Text='<%#GetBiaoZhunTime(Eval("OrderDateTime").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="备注">
                                            <ItemTemplate>
                                                <!--Label22-->
                                                <asp:Label ID="Lab_Remark" runat="server" Text='<%# Eval("Description")  %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="tablebt bbb"></HeaderStyle>
                                    <AlternatingRowStyle BackColor="#F1F4F8" />
                                </asp:GridView>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <uc1:Pager ID="Pager1" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                    </table>
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
                                        <a href="#">
                                            <asp:ImageButton ID="Butt_Excel" runat="server" OnClick="Button2_Click" ImageUrl="images/anextable.gif" />
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
                                        <%=GetTran("006849")%><br />
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
