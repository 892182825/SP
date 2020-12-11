<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddRefundmentOrder.aspx.cs"    Inherits="Company_AddRefundmentOrder" %> 

<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%=GetTran("001894", "添加退货单")%></title>
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
    </script>

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
    function setTrBgColor(oTr,b)
    {
        oTr.rowIndex % 2 != 0 ? oTr.style.backgroundColor = b ? aBgColor[3] : aBgColor[1] : oTr.style.backgroundColor = b ? aBgColor[2] : aBgColor[0];
    }
    window.onload = function(){addTableListener(document.getElementById('<%=Session["GridViewID"] %>'),0,0);}

function MM_jumpMenu(targ,selObj,restore){ //v3.0
  eval(targ+".location='"+selObj.options[selObj.selectedIndex].value+"'");
  if (restore) selObj.selectedIndex=0;
}
//-->
    </script>

    <script language="javascript">
     function secBoard(n)
  
  {
  //  document.Form1.sa.value=n;
  //  document.getElementById("s").click();
       for(i=0;i<secTable.cells.length;i++)
      secTable.cells[i].className="sec1";
    secTable.cells[n].className="sec2";
    for(i=0;i<mainTable.tBodies.length;i++)
      mainTable.tBodies[i].style.display="none";
    mainTable.tBodies[n].style.display="block";
  }
    </script>

</head>
<body>
    <form id="Form1" method="post" runat="server">
    <div style="height: auto; width: 100%; overflow: hidden;">
        <br />
        <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#F8FBFD"
            class="biaozzi">
            <tr>
                <td>
                    <table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#FFFFFF"
                        class="biaozzi">
                        <tr>
                            <td width="180" align="right" bgcolor="#EBF1F1">
                                <%=GetTran("000150", "店号")%>：
                            </td>
                            <td bgcolor="#F8FBFD" style="white-space: nowrap">
                                <asp:TextBox ID="txtStoreId" runat="server" MaxLength="10"></asp:TextBox>
                                <asp:Button ID="btnBindStore" runat="server" Text="绑定店的库存" OnClick="btnBindStore_Click"
                                    CssClass="another" />
                                &nbsp;&nbsp;
                                <asp:Label ID="Lmessage" runat="server" ForeColor="red"></asp:Label>
                            </td>
                            <td width="180" align="right" bgcolor="#EBF1F1">
                               <%=GetTran("000040", "店名")%> ：
                            </td>
                            <td width="360" bgcolor="#F8FBFD">
                                <asp:Label ID="lblShopName" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" bgcolor="#EBF1F1">
                               <%=GetTran("000041", "总共金额")%> ：
                            </td>
                            <td bgcolor="#F8FBFD">
                                <asp:Label ID="lblTotalMoney" runat="server" />
                            </td>
                            <td align="right" bgcolor="#EBF1F1">
                                <%=GetTran("000113", "总积分")%>：
                            </td>
                            <td bgcolor="#F8FBFD">
                                <asp:Label ID="lblTotalIntegral" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" bgcolor="#EBF1F1">
                                <%=GetTran("000072", "地址")%>：
                            </td>
                            <td bgcolor="#F8FBFD">
                                <asp:Label ID="lbladdress" runat="server" />
                            </td>
                            <td align="right" bgcolor="#EBF1F1">
                               <%=GetTran("000073", "邮编")%> ：
                            </td>
                            <td bgcolor="#F8FBFD">
                                <asp:Label ID="lblpostalcode" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" bgcolor="#EBF1F1">
                               <%=GetTran("000115", "联系电话")%> ：
                            </td>
                            <td bgcolor="#F8FBFD">
                                <asp:Label ID="lbltele" runat="server" />
                            </td>
                            <td align="right" bgcolor="#EBF1F1">
                                <%=GetTran("000107", "姓名")%>：
                            </td>
                            <td bgcolor="#F8FBFD">
                                <asp:Label ID="lblinceptman" runat="server" />
                                <asp:TextBox ID="TextBox1" runat="server" Visible="false" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap="nowrap" bgcolor="#EBF1F1">
                                <%=GetTran("000926", "备注信息")%>：
                            </td>
                            <td colspan="3" bgcolor="#F8FBFD">
                                <asp:TextBox ID="txtMemo" runat="server" TextMode="MultiLine" Width="100%" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnSaveOrder" runat="server" Text="确认退单" OnClick="btnSaveOrder_Click"
                                                Visible="False" CssClass="another" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSearch" runat="server" Text="查看金额积分" OnClick="btnSearch_Click"
                                                Visible="False" CssClass="another" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <table border="0" class="biaozzi" align="center" width="100%">
            <tr>
               <td align="left" width="100%" style="border:rgb(147,226,244) solid 1px">
                   <table  rules="all" cellpadding="0" cellspacing="0"  style="width: 100%" class="tablemb bordercss">
                        <tr>
                            <th style="width: 7%; white-space: nowrap;">
                                <%=GetTran("000263", "产品编码")%>
                            </th>
                            <th style="width: 7%; white-space: nowrap;">
                               <%=GetTran("000501", "产品名称")%> 
                            </th>
                            <th style="width: 23%; white-space: nowrap;">
                               <%=GetTran("000560", "产品信息")%> 
                            </th>
                            <th style="width: 28%; white-space: nowrap;">
                                <%=GetTran("000518", "单位")%>
                            </th>
                            <th style="white-space: nowrap;">
                                <%=GetTran("000505", "数量")%>
                            </th>
                        </tr>
                    </table>
                    <div id="Layer1" style="overflow-y: auto; overflow-x: hidden; width: 100%; height: 220px;">
                        <asp:GridView ID="gdvRefundment" runat="server" AutoGenerateColumns="false" Width="100%"
                             bgcolor="#F8FBFD" CssClass="tablemb bordercss"  OnRowDataBound="gdvRefundment_RowDataBound"
                            ShowHeader="false">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemStyle Wrap="false" HorizontalAlign="center" Width="7%" />
                                    <HeaderStyle Wrap="false" />
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Productcode")%>' />
                                        <asp:Label ID="Label2" Visible="False" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ProductID")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemStyle Wrap="false" HorizontalAlign="center" Width="7%" />
                                    <HeaderStyle Wrap="false" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblname" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ProductName")%>' />
                                        <asp:Label ID="LblHuoid" Visible="False" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ProductID")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemStyle Wrap="false" HorizontalAlign="center" Width="23%" />
                                    <HeaderStyle Wrap="false" />
                                    <ItemTemplate>
                                        单价：
                                        <asp:Label ID="LblPrice" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PreferentialPrice") %>' />
                                        积分：
                                        <asp:Label ID="LblPv" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.PreferentialPV")%>' />
                                        <input id="HidbigProductUnitID" type="hidden" value='<%# DataBinder.Eval( Container.DataItem, "bigProductUnitID" )%>'
                                            runat="server" name="HidbigProductUnitID" />
                                        <input id="HidBigUnitName" type="hidden" value='<%# Eval("BigUnitName" )%>' runat="server"
                                            name="HidBigUnitName" />
                                        <input id="HidsmallProductUnitID" type="hidden" value='<%# Eval("smallProductUnitID" )%>'
                                            runat="server" name="HidsmallProductUnitID" />
                                        <input id="HidSmallUnitName" type="hidden" value='<%# Eval("SmallUnitName" )%>' runat="server"
                                            name="HidSmallUnitName" />
                                        <input id="HidBigSmallMultiPle" type="hidden" value='<%# Eval("BigSmallMultiPle" )%>'
                                            runat="server" name="HidBigSmallMultiPle" />
                                        <input id="HidNeedNumber" type="hidden" value='<%# Eval("MaxCount" )%>' runat="server"
                                            name="HidNeedNumber" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemStyle Wrap="false" HorizontalAlign="center" Width="28%" />
                                    <HeaderStyle Wrap="false" />
                                    <ItemTemplate>
                                        <asp:RadioButtonList ID="RadioBtnCate" runat="server" RepeatDirection="Horizontal"
                                            RepeatLayout="Flow">
                                            <asp:ListItem Value="0" Selected="True">大箱</asp:ListItem>
                                            <asp:ListItem Value="1">小箱</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemStyle Wrap="false" HorizontalAlign="center"/>
                                    <HeaderStyle Wrap="false" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="TxtPdtNum" runat="server" Width="80px" Text='<%# DataBinder.Eval(Container,"DataItem.MaxCount") %>'>
                                        </asp:TextBox>
                                        <asp:Label ID="lblmessage" runat="server" Visible="False" Text="必选数量" />
                                        <asp:RequiredFieldValidator ID="rf" runat="server" Display="Static" ControlToValidate="TxtPdtNum"
                                            InitialValue="">*填写</asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="rv" runat="server" ControlToValidate="TxtPdtNum" Display="Static"></asp:RangeValidator>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <AlternatingRowStyle BackColor="#F1F4F8" />
                        </asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
