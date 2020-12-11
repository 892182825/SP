﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewStoreOrderD.aspx.cs" Inherits="Company_ViewStoreOrderD" EnableEventValidation="false"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
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



<SCRIPT language="javascript">
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
</SCRIPT>
    <style type="text/css">
        .style1
        {
            width: 215px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div><br>
     <table style="width: 100%;" id="talbe1" runat="server" cellpadding="0" cellspacing="1"
        border="0" class="tablemb" align="center">
        <tr>
            <td >
                 <asp:GridView ID="GridView_BillOutOrder" runat="server" AutoGenerateColumns="False" 
                                                width="100%" 
                                                class="tablemb bordercss"  
                                                HeaderStyle-CssClass="tablebt bbb" 
                                                onrowdatabound="GridView_BillOutOrder_RowDataBound">
                                                <EmptyDataTemplate>
                                                <table cellspacing="0" width="100%" >
                                                    <tr>
                                                        
                                                        <th nowrap><%=GetTran("000079","订单号")%></th>
                                                        <th nowrap><%=GetTran("000098","订货店铺")%></th>
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
                                                        
                                                    </tr>                
                                                </table>
                                            </EmptyDataTemplate>
                                                <Columns>
                                                   
                                                    <asp:TemplateField HeaderText="订货店铺" Visible="false">
                                                        <ItemTemplate><!--Label1-->
                                                            <asp:Label ID="Lab_StoreID" runat="server" Text='<%# Bind("StoreID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                        <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="订单号">
                                                        <ItemTemplate><!--Label4-->
                                                            <asp:Label ID="Lab_StoreOrderID" runat="server" Text='<%# Bind("StoreOrderID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                        <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="期数">
                                                        <ItemTemplate><!--Label5-->
                                                            <asp:Label ID="Lab_ExpectNum" runat="server" Text='<%# Bind("ExpectNum") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="订货类型">
                                                        <ItemTemplate><!--Label6-->
                                                            <asp:Label ID="Lab_OrderType" runat="server" Text='<%# GetOrderType(Eval("OrderType").ToString()) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                        <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="总金额">
                                                        <ItemTemplate><!--Label7-->
                                                            <asp:Label ID="Lab_TotalMoney" runat="server" 
                                                                Text='<%# Bind("TotalMoney", "{0:N2}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" Wrap="false" />
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="总积分">
                                                        <ItemTemplate><!--Label8-->
                                                            <asp:Label ID="Lab_TotalPV" runat="server" Text='<%# Bind("TotalPV", "{0:N2}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                        <ItemStyle HorizontalAlign="right"  Wrap="false"/>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="付款否" Visible="false">
                                                        <ItemTemplate><!--Label10-->
                                                            <asp:Label ID="Lab_IsCheckOut" runat="server" Text='<%# StringFormat(Eval("IsCheckOut").ToString()) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                         <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="收货人姓名">
                                                        <ItemTemplate><!--Label11-->
                                                            <asp:Label ID="Lab_InceptPerson" runat="server" Text='<%# Encryption.Encryption.GetDecipherName(Eval("InceptPerson").ToString()) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="收货人国家">
                                                        <ItemTemplate><!--Label10-->
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
                                                    
                                                    <asp:TemplateField HeaderText="收货人地址">
                                                        <ItemTemplate><!--Label12-->
                                                            <asp:Label ID="Lab_InceptAddress" runat="server" Text='<%# SetFormatString(Encryption.Encryption.GetDecipherAddress(Eval("InceptAddress").ToString()),15) %>' title='<%#Encryption.Encryption.GetDecipherAddress(Eval("InceptAddress").ToString()) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                        <ItemStyle HorizontalAlign="left" Wrap="false" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="电话">
                                                        <ItemTemplate><!--Label13-->
                                                            <asp:Label ID="Lab_Telephone" runat="server" Text='<%# Encryption.Encryption.GetDecipherTele(Eval("Telephone").ToString()) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                        <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="订货总重量">
                                                        <ItemTemplate><!--Label14-->
                                                            <asp:Label ID="Lab_Weight" runat="server" Text='<%# Bind("Weight", "{0:N2}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                        <ItemStyle HorizontalAlign="right"  Wrap="false"/>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="运费" Visible="false">
                                                        <ItemTemplate><!--Label15-->
                                                            <asp:Label ID="Lab_Carriage" runat="server" Text='<%# Bind("Carriage", "{0:N2}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                        <ItemStyle HorizontalAlign="Right"  Wrap="false"/>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="订货日期">
                                                        <ItemTemplate><!--Label16-->
                                                            <asp:Label ID="Lab_OrderDateTime" runat="server" Text='<%#GetBiaoZhunTime(Eval("OrderDateTime").ToString()) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                        <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                                                    </asp:TemplateField>
                                                   
                                                </Columns>

                                                <HeaderStyle CssClass="tablebt bbb" Wrap="false"></HeaderStyle>
                                                <AlternatingRowStyle BackColor="#F1F4F8" />
                                            </asp:GridView>
            </td>
        </tr>
       
    </table>
    <br />
        <table style="width:100%;">
        <tr>
        <td>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#F8FBFD" class="tablemb"  
                                     HeaderStyle-CssClass="tablebt bbb" 
                EmptyDataText="无数据" onrowdatabound="GridView1_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="产品编号">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("ProductCode") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="产品名称">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("ProductName") %>'></asp:Label>
                    </ItemTemplate>

                    <ItemStyle HorizontalAlign="Center" />

                </asp:TemplateField>
                <asp:TemplateField HeaderText="数量">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%#Eval("Quantity") %>'></asp:Label>
                    </ItemTemplate>

                    <ItemStyle HorizontalAlign="Center" />

                </asp:TemplateField>
                <asp:TemplateField HeaderText="单位">
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%#Eval("ProductUnitName") %>'></asp:Label>
                    </ItemTemplate>
  
                    <ItemStyle HorizontalAlign="Center" />
  
                </asp:TemplateField>
                <asp:TemplateField HeaderText="期数">
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%#Eval("ExpectNum") %>'></asp:Label>
                    </ItemTemplate>
            
                    <ItemStyle HorizontalAlign="Center" />
            
                </asp:TemplateField>
                <asp:TemplateField HeaderText="单价">
                    <ItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%#Eval("Price") %>'></asp:Label>
                    </ItemTemplate>
      
                    <ItemStyle HorizontalAlign="Right" />
      
                </asp:TemplateField>
                <asp:TemplateField HeaderText="积分">
                    <ItemTemplate>
                        <asp:Label ID="Label7" runat="server" Text='<%#Eval("Pv") %>'></asp:Label>
                    </ItemTemplate>

                    <ItemStyle HorizontalAlign="Right" />

                </asp:TemplateField>
                
             </Columns>

<HeaderStyle CssClass="tablebt bbb"></HeaderStyle>
            <AlternatingRowStyle BackColor="#F1F4F8" />
        </asp:GridView>
        </td>
        </tr>
        <tr>
        <td align="center"><br>
            <input type="button" ID="butt_Query"value='<%=GetTran("000096","返 回") %>'
                                            style="cursor:pointer" Class="anyes" onclick="history.back()"/>
        </td>
        </tr>
        </table>

    </form>
</body>
</html>
