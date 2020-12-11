<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DisplaceGoodsAdd.aspx.cs"
    Inherits="Company_DisplaceGoodsAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>添加换货</title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />

    <script src="../JS/QCDS2010.js" type="text/javascript"></script>

    <script type="text/javascript">
        window.onerror=function ()
        {
            return true;
        };
    </script>

    <script type="text/javascript">
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
    <form id="form1" runat="server">
    <br />
    <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#F8FBFD"
        class="biaozzi">
        <tr>
            <td>
                <table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#FFFFFF"
                    class="biaozzi">
                    <tr>
                        <td>
                            <table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#FFFFFF"
                                class="biaozzi">
                                <tr>
                                    <td width="180" align="right" bgcolor="#EBF1F1" style="white-space:nowrap">
                                        <%=GetTran("001912", "店号")%>：
                                    </td>
                                    <td bgcolor="#F8FBFD" style="white-space: nowrap">
                                        <asp:TextBox ID="txtStoreId" runat="server" MaxLength="10"></asp:TextBox><asp:Button
                                            ID="Button1" runat="server" Text="绑定店的库存" OnClick="Button1_Click" CssClass="another"/>
                                    </td>
                                    <td width="180" align="right" bgcolor="#EBF1F1" style="white-space:nowrap">
                                       <%=GetTran("001913", "店名")%> ：
                                    </td>
                                    <td width="360" bgcolor="#F8FBFD">
                                        <asp:Label ID="lblShopName" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" bgcolor="#EBF1F1" style="white-space:nowrap">
                                        <%=GetTran("001916", "退货总金额")%>：
                                    </td>
                                    <td bgcolor="#F8FBFD">
                                        <asp:Label ID="lblTotalMoney" runat="server"></asp:Label>&nbsp;&nbsp;
                                        <asp:Label ID="LabCurrency" runat="server"></asp:Label>
                                    </td>
                                    <td align="right" bgcolor="#EBF1F1" style="white-space:nowrap">
                                        <%=GetTran("001813", "退货总积分")%>：
                                    </td>
                                    <td bgcolor="#F8FBFD">
                                        <asp:Label ID="lblTotalIntegral" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" bgcolor="#EBF1F1" style="white-space:nowrap">
                                        <%=GetTran("001919", "换货总金额")%>：
                                    </td>
                                    <td bgcolor="#F8FBFD">
                                        <asp:Label ID="lblHuanMoney" runat="server"></asp:Label>
                                    </td>
                                    <td align="right" bgcolor="#EBF1F1" style="white-space:nowrap">
                                        <%=GetTran("001920", "换货总积分")%>：
                                    </td>
                                    <td bgcolor="#F8FBFD">
                                        <asp:Label ID="lblHuanPv" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" bgcolor="#EBF1F1" style="white-space:nowrap">
                                        <%=GetTran("000112", "收货地址")%>：
                                    </td>
                                    <td bgcolor="#F8FBFD">
                                        <asp:TextBox ID="txtAddress" runat="server" MaxLength="500"></asp:TextBox>
                                    </td>
                                    <td align="right" bgcolor="#EBF1F1" style="white-space:nowrap">
                                        <%=GetTran("000073", "邮编")%>：
                                    </td>
                                    <td bgcolor="#F8FBFD">
                                        <asp:TextBox ID="txtpostalcode" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" bgcolor="#EBF1F1" style="white-space:nowrap">
                                        <%=GetTran("000115", "联系电话")%>：
                                    </td>
                                    <td bgcolor="#F8FBFD">
                                        <asp:TextBox ID="txtTele" runat="server" MaxLength="20"></asp:TextBox>
                                    </td>
                                    <td align="right" bgcolor="#EBF1F1" style="white-space:nowrap">
                                        <%=GetTran("001850", "收货人")%>：
                                    </td>
                                    <td bgcolor="#F8FBFD">
                                        <asp:TextBox ID="txtInceptMan" runat="server" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" bgcolor="#EBF1F1" style="white-space:nowrap">
                                        <%=GetTran("001926", "您如果有特殊说明请填写备注")%>：
                                    </td>
                                    <td colspan="3" bgcolor="#F8FBFD">
                                        <asp:TextBox ID="txtMemo" runat="server" TextMode="MultiLine" Width="100%" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <tr>
                    <td>
                        <asp:Button ID="btnSaveOrder" runat="server" Visible="False" Text="确认换货" OnClick="btnSaveOrder_Click"
                            CssClass="another"></asp:Button>&nbsp;
                        <asp:Button ID="btnViewTotal" runat="server" Visible="False" Text="查看金额积分" OnClick="btnViewTotal_Click"
                            CssClass="another"></asp:Button>&nbsp;&nbsp;
                        <asp:Label ID="Lmessage" runat="server" ForeColor="red"></asp:Label>
                    </td>
                </tr>
                <table border="0" class="biaozzi" align="center" width="100%">
                    <tr>
                        <td style="white-space:nowrap">
                            <%=GetTran("001976", "请填写您要换的产品清单：[注：对于同一种产品不能既退货，又换货。比如：退10个A产品，又同时换5个A产品")%>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="100%" style="border:rgb(147,226,244) solid 1px">
                            <table cellspacing="0" cellpadding="0" rules="all"  style="width: 98%" class="tablemb bordercss">
                                <tr>
                                    <th style="width: 13%; white-space: nowrap;">
                                        <%=GetTran("000263", "产品编码")%>
                                    </th>
                                    <th style="width: 13%; white-space: nowrap;">
                                        <%=GetTran("000501", "产品名称")%>
                                    </th>
                                    <th style="width: 24%; white-space: nowrap;">
                                        <%=GetTran("000560", "产品信息")%>
                                    </th>
                                    <th style="width: 15%; white-space: nowrap;">
                                        <%=GetTran("001980", "该店实际库存")%>
                                    </th>
                                    <th style="width: 18%; white-space: nowrap;">
                                        <%=GetTran("001982", "退货数量")%>
                                    </th>
                                    <th style="width: 17%;white-space: nowrap;">
                                        <%=GetTran("001985", "换货数量")%>
                                    </th>
                                </tr>
                            </table>
                            <div id="Layer1" style=" width: 100%; height: 220px;overflow:auto;" >
                                <asp:GridView ID="gvDisplaceGoodsAdd" runat="server" AutoGenerateColumns="False"
                                    DataKeyNames="ProductID" Width="98%" CssClass="tablemb bordercss">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" Width="13%" />
                                            <HeaderStyle Wrap="false" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblPostalCode" runat="server" Text='<%# Eval("ProductCode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" Width="13%" />
                                            <HeaderStyle Wrap="false" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblname" runat="server" Text='<%# Eval("productName") %>'></asp:Label>
                                                <asp:Label ID="LblHuoid" Visible="False" runat="server" Text='<%#Eval("productID")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" Width="24%" />
                                            <HeaderStyle Wrap="false" />
                                            <ItemTemplate>
                                                <%=GetTran("000503", "单价")%>：
                                                <asp:Label ID="LblPrice" runat="server" Text='<%#Eval("PreferentialPrice") %>' />
                                                <%=GetTran("000414", "积分")%>：
                                                <asp:Label ID="LblPv" runat="server" Text='<%#Eval("PreferentialPV") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" Width="15%" />
                                            <HeaderStyle Wrap="false" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" Width="18%" />
                                            <HeaderStyle Wrap="false" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtTuiQuantity" runat="server" Width="60px" Text='0'></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="Requiredfieldvalidator1" runat="server" Display="Dynamic"
                                                    ControlToValidate="txtTuiQuantity" InitialValue=""><%=GetTran("001990", "*填写")%></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="cv1" runat="server" ErrorMessage="大于等于0" Display="Dynamic"
                                                    ControlToValidate="txtTuiQuantity" Type="Integer" Operator="GreaterThanEqual"
                                                    ValueToCompare="0"></asp:CompareValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TxtPdtNum" runat="server" Width="60px" Text='0'></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="Requiredfieldvalidator2" runat="server" Display="Dynamic"
                                                    ControlToValidate="TxtPdtNum" InitialValue=""><%=GetTran("001990", "*填写")%></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="cv2" runat="server" ErrorMessage="大于等于0" Display="Dynamic"
                                                    ControlToValidate="TxtPdtNum" Type="Integer" Operator="GreaterThanEqual" 
                                                    ValueToCompare="0"></asp:CompareValidator>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="False"></HeaderStyle>
                                            <ItemStyle Width="17%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
