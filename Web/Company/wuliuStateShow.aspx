<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wuliuStateShow.aspx.cs" Inherits="Company_wuliuStateShow" EnableEventValidation="false"%>

<%@ Register src="../UserControl/Pager.ascx" tagname="Pager" tagprefix="uc1" %>

<%@ Register src="../UserControl/Country.ascx" tagname="Country" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <script type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
    <script src="../JS/QCDS2010.js" type="text/javascript"></script>
    <script type="text/javascript">
        window.onerror=function ()
        {
            return true;
            }
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
    window.onload=function()
	{
	    down2();
	};
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
        function cut()
        {
             document.getElementById("span1").title='<%=GetTran("000032", "管 理") %>';
        }
        function cut1()
        {
             document.getElementById("span2").title='<%=GetTran("000033", "说 明") %>';
        }
    </script>



<SCRIPT language="javascript">
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
      
</SCRIPT>
<script type="text/javascript" src="../js/SqlCheck.js"></script>
    </head>
<body>
    <form id="form1" runat="server" onsubmit="return filterSql_III()">
        <div >
        <br>
        <table>
            <tr>
                <td>
                    
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
                        
                        <tr>
                            <td>
                                <asp:Button ID="btn_Submit" runat="server" Text="查 询" 
                                            style="cursor:pointer"  CssClass="anyes"
                                                onclick="Button1_Click" align="absmiddle" Height="22px" />&nbsp;&nbsp;
                                                
                               <%=GetTran("000047")%>：<asp:DropDownList ID="DropCurrency" runat="server">
                                </asp:DropDownList>
                                <asp:DropDownList ID="DropDownList_Items" runat="server" AutoPostBack="true" 
                                    onselectedindexchanged="DropDownList_Items_SelectedIndexChanged">
                                    <asp:ListItem Value="StoreID" Enabled="false">订货店铺</asp:ListItem>
									<asp:ListItem Value="StoreOrderID">订单号</asp:ListItem>
									<asp:ListItem Value="PostalCode">邮政编码</asp:ListItem>
									<asp:ListItem Value="IsSent">是否发货</asp:ListItem>
									<asp:ListItem Value="ConveyanceCompany">物流公司名称</asp:ListItem>
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
                                <asp:TextBox ID="txtBox_keyWords" runat="server"  Width="80px" MaxLength="100"></asp:TextBox>
                                <asp:TextBox ID="txtBox_rq" Visible="false" runat="server" Width="80px" onfocus="new WdatePicker()" CssClass="Wdate"></asp:TextBox>
                                
                                <%=GetTran("000060")%>&nbsp;&nbsp; <%=GetTran("000067")%>：<!--TextBox1--><asp:TextBox ID="txtBox_OrderDateTimeStart" runat="server" 
                                                onfocus="new WdatePicker()" CssClass="Wdate" Width="80px"></asp:TextBox>
                                                <%=GetTran("000068")%>：<!--TextBox2--><asp:TextBox ID="txtBox_OrderDateTimeEnd" runat="server"  Width="80px" onfocus="new WdatePicker()" CssClass="Wdate"></asp:TextBox>
                                            &nbsp;
                                            <%=GetTran("000070")%>：<asp:TextBox ID="txtBox_ConsignmentDateTimeStart"  Width="80px" runat="server" onfocus="new WdatePicker()" CssClass="Wdate"></asp:TextBox>
                                            <%=GetTran("000068")%>：
                                            <!--TextBox4-->
                                            <asp:TextBox ID="txtBox_ConsignmentDateTimeEnd" Width="80px" runat="server" onfocus="new WdatePicker()" CssClass="Wdate"></asp:TextBox>   
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <%=GetTran("000074")%>
                            </td>
                        </tr>
                    </table><br>
                    <table>
                       
                        <tr>
                            <td></td>
                            <td style="border:rgb(147,226,244) solid 1px"><!--GridView1-->
                                <asp:GridView ID="GridView_WuliuStateShow" runat="server" AutoGenerateColumns="False" 
                                    onselectedindexchanged="GridView1_SelectedIndexChanged" width="100%" 
                                    class="tablemb bordercss"  
                                     HeaderStyle-CssClass="tablebt bbb" 
                                    onrowdatabound="GridView_WuliuStateShow_RowDataBound" 
                                    onrowcommand="GridView_WuliuStateShow_RowCommand">
                                    <EmptyDataTemplate>
                                        <table cellspacing="0" width="100%">
                                            <tr>
                                                <th nowrap>
                                                    <%=GetTran("000079", "订单号")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000098", "订货店铺")%>
                                                </th>
                                                 <th nowrap>
                                                    <%=GetTran("007206", "快递单号")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000045", "期数")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000102", "发货否")%>
                                                </th>
                                                
                                                <th nowrap>
                                                    <%=GetTran("000383", "收货人姓名")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000108", "收货人国家")%>
                                                </th>
                                                <th nowrap>
                                                   <%=GetTran("000109", "省份")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000110", "城市")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000112", "收货地址")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000073", "邮编")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000115", "联系电话")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000041", "总金额")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000113", "总积分")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000118", "重量")%>
                                                </th>
                                                <th nowrap>
                                                     <%=GetTran("000121", "物流公司")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000067", "订货日期")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000070", "发货日期")%>
                                                </th>
                                            </tr>               
                                        </table>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="详细">
                                            <ItemTemplate><!--lkb-->
                                               <img src="images/fdj.gif" /> <asp:LinkButton ID="lbtn_See" runat="server" CommandArgument='<%# Eval("StoreOrderID") %>' CommandName="select" ><%=GetTran("000339", "详细")%></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap=false />
                                            <HeaderStyle Wrap=false />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="订货店铺" Visible="false">
                                            <ItemTemplate><!--Label2-->
                                                <asp:Label ID="lab_StoreID" runat="server" Text='<%#Eval("StoreID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap=false />
                                            <HeaderStyle Wrap=false />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="订单号">
                                            <ItemTemplate><!--Label3-->
                                                <asp:Label ID="lab_StoreOrderID" runat="server" Text='<%#Eval("StoreOrderID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap=false />
                                            <HeaderStyle Wrap=false />
                                        </asp:TemplateField>
                         <%--               <asp:TemplateField HeaderText="对应出库号">
                                            <ItemTemplate><!--Label4-->
                                                <asp:Label ID="lab_OutStorageOrderID" runat="server" Text='<%#Empty.GetString(Eval("OutStorageOrderID").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap=false />
                                            <HeaderStyle Wrap=false />
                                        </asp:TemplateField>--%>
                                         <asp:TemplateField HeaderText="快递单号">
                                            <ItemTemplate><!--Label4-->
                                                <asp:Label ID="lab_OutStorageOrderIDA" runat="server" Text='<%#Eval("kuaididh") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap=false />
                                            <HeaderStyle Wrap=false />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="期数">
                                            <ItemTemplate><!--Label5-->
                                                <asp:Label ID="lab_ExpectNum" runat="server" Text='<%#Eval("ExpectNum") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap=false />
                                            <HeaderStyle Wrap=false />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="是否付款" Visible="false">
                                            <ItemTemplate><!--Label6-->
                                                <asp:Label ID="lab_IsCheckOut" runat="server" Text='<%#StringFormat(Eval("IsCheckOut").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap=false />
                                            <HeaderStyle Wrap=false />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="是否发货">
                                            <ItemTemplate><!--Label7-->
                                                <asp:Label ID="lab_IsSent" runat="server" Text='<%#StringFormat(Eval("IsSent").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap=false />
                                            <HeaderStyle Wrap=false />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="是否到达" Visible="false">
                                            <ItemTemplate><!--Label8-->
                                                <asp:Label ID="lab_IsReceived" runat="server" Text='<%#StringFormat(Eval("IsReceived").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap=false />
                                            <HeaderStyle Wrap=false />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="收货人姓名">
                                            <ItemTemplate><!--Label9-->
                                                <asp:Label ID="lab_InceptPerson" runat="server" Text='<%#Empty.GetString(Encryption.Encryption.GetDecipherName(Eval("InceptPerson").ToString())) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap=false />
                                            <HeaderStyle Wrap=false />
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
                                                <asp:Label ID="lab_InceptAddress" runat="server" Text='<%#SetFormatString(Encryption.Encryption.GetDecipherAddress(Eval("InceptAddress").ToString()),15) %>' title='<%#Eval("InceptAddress") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" Wrap=false  />
                                            <HeaderStyle Wrap=false />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="收货人邮编">
                                            <ItemTemplate><!--Label13-->
                                                <asp:Label ID="lab_PostalCode" runat="server" Text='<%#Eval("PostalCode") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap=false />
                                            <HeaderStyle Wrap=false />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="收货人电话">
                                            <ItemTemplate><!--Label14-->
                                                <asp:Label ID="lab_Telephone" runat="server" Text='<%#Encryption.Encryption.GetDecipherTele(Eval("Telephone").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap=false />
                                            <HeaderStyle Wrap=false />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="订货总金额">
                                            <ItemTemplate><!--Label15-->
                                                <asp:Label ID="lab_TotalMoney" runat="server" Text='<%#Eval("TotalMoney","{0:N2}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right"  Wrap=false />
                                            <HeaderStyle Wrap=false />
                                        </asp:TemplateField>
                                       
                                        <asp:TemplateField HeaderText="订货总积分">
                                            <ItemTemplate><!--Label17-->
                                                <asp:Label ID="lab_TotalPV" runat="server" Text='<%#Eval("TotalPV","{0:N2}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap=false HorizontalAlign="right"/>
                                            <HeaderStyle Wrap=false  />
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="联系电话">
                                            <ItemTemplate><!--Label18-->
                                                <asp:Label ID="lab_Telephone_2" runat="server" Text='<%#Eval("Telephone") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap=false />
                                            <HeaderStyle Wrap=false />
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="重量">
                                            <ItemTemplate><!--Label19-->
                                                <asp:Label ID="lab_Weight" runat="server" Text='<%#Eval("Weight","{0:N2}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap=false HorizontalAlign=Right/>
                                            <HeaderStyle Wrap=false  />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="物流公司">
                                            <ItemTemplate><!--Label19-->
                                                <asp:Label ID="Lab_ConveyanceCompany" runat="server" Text='<%#Empty.GetString(Eval("ConveyanceCompany").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                     
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                                     
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="订货日期">
                                            <ItemTemplate><!--Label20-->
                                                <asp:Label ID="lab_OrderDateTime" runat="server" Text='<%#Empty.GetString(GetBiaoZhunTime(Eval("OrderDateTime").ToString())) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap=false />
                                            <HeaderStyle Wrap=false />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="发货时间">
                                            <ItemTemplate><!--Label21-->
                                                <asp:Label ID="lab_ConsignmentDateTime" runat="server" Text='<%#Empty.GetString(GetBiaoZhunTime(Eval("ConsignmentDateTime").ToString()) )%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap=false />
                                            <HeaderStyle Wrap=false />
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle BackColor="White" />

<HeaderStyle CssClass="tablebt bbb"></HeaderStyle>
                                    <AlternatingRowStyle BackColor="#F1F4F8" />
                                </asp:GridView>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                  
                                <uc1:Pager ID="Pager1" runat="server" />
                                  
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td><!--Button2-->
                    <%--<asp:Button ID="btn_Excel" runat="server" Text="导出Excel" 
                        onclick="Button2_Click" />--%>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td></td>
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
                  <td> <%--<%=GetTran("001625")%>--%>1、对订货单处理状态的跟踪，查看订单处理到哪一步了。
</td><br />
                   
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
