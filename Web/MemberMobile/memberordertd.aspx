<%@ Page Language="C#" AutoEventWireup="true" CodeFile="memberordertd.aspx.cs" Inherits="Member_memberordertd" %>

<%@ Register src="../UserControl/Pager.ascx" tagname="Pager" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>团队业绩浏览</title>
        <link href="CSS/Member.css" rel="stylesheet" type="text/css" />
    <script src="../JS/QCDS2010.js" type="text/javascript"></script>
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
    <script type="text/javascript">
        var lang = $("#lang").text();
        if (lang!="L001") {
            //alert("memberordertd");
        }
    </script>
</head>
    
	<body leftMargin="0" topMargin="0" marginheight="0" marginwidth="0">
        <b id="lang" style="display:none"><%=Session["LanguageCode"] %></b>
		<form id="Form1" method="post" runat="server">
		<br />
			<table cellSpacing="0" cellPadding="0" border="0" width="100%" class="biaozzi">
				<tr>
					<td>
						<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
							<TR>
								<td  align="left" class="biaozzi"><%=GetTran("000201", "请选择查询期数")%>：<asp:dropdownlist id="DropDownQiShu" runat="server" AutoPostBack="True" onselectedindexchanged="DropDownQiShu_SelectedIndexChanged"></asp:dropdownlist>&nbsp;
								</td>
							</TR>
							
						</table>
						<br />
						<table cellSpacing="0" cellPadding="0" border="0" width="100%" align="center">
							<tr>
								<td vAlign="top" align="center">
								<asp:GridView id="gvorder" runat="server" Width="100%" AutoGenerateColumns="False" 
                                        CssClass="tablemb" onrowdatabound="gvorder_RowDataBound">
										<Columns>
											<asp:TemplateField HeaderText="明细">
											    <HeaderTemplate>
											        <span>
											           明细
											        </span>
											    </HeaderTemplate>
											    <ItemTemplate>
											       <img src="images/fdj.gif" />   <asp:LinkButton ID="lbdetail" runat="server" PostBackUrl='<%# Eval("OrderID","ShowOrderDetailsTD.aspx?byy={0}") %>'><%=GetTran("000440", "查看")%></asp:LinkButton>
											    </ItemTemplate>
											</asp:TemplateField>
											<asp:BoundField DataField="OrderExpectNum" HeaderText="期数"></asp:BoundField>
											<asp:BoundField DataField="zhifu" HeaderText="支付状态"></asp:BoundField>
											<asp:BoundField DataField="OrderID" HeaderText="订单号"></asp:BoundField>
											<asp:BoundField DataField="Number" HeaderText="会员编号"></asp:BoundField>
											<asp:BoundField DataField="Name" HeaderText="会员姓名"></asp:BoundField>
											<asp:BoundField DataField="TotalMoney" DataFormatString="{0:n2}" ItemStyle-HorizontalAlign="Right" HeaderText="金额"></asp:BoundField>
											<asp:BoundField DataField="Totalpv" HeaderText="积分"  ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" ></asp:BoundField>
											<asp:BoundField DataField="orderdate" HeaderText="报单日期" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundField>
											<asp:BoundField DataField="StoreID" HeaderText="确认店铺"></asp:BoundField>
											<asp:BoundField DataField="fuxiaoname" HeaderText="报单途径"></asp:BoundField>
											<asp:BoundField DataField="dpqueren" HeaderText="店铺确认"></asp:BoundField>
                                            <asp:BoundField DataField="gsqueren" HeaderText="公司确认"></asp:BoundField>
										</Columns>
										<EmptyDataTemplate>
										<table cellSpacing="0" cellPadding="0" border="1" width="100%" align="center">
										    <tr>
										        <th>
										        <span><%=GetTran("000811", "明细")%></span>
										        </th>
										         <th>
										        <span><%=GetTran("000045", "期数")%></span>
										        </th>
										        <th>
										        <span><%=GetTran("000775", "支付状态")%></span>
										        </th>
										        <th>
										        <span><%=GetTran("000079", "订单号")%></span>
										        </th>
										        <th>
										        <span><%=GetTran("000024", "会员编号")%></span>
										        </th>
										        <th>
										        <span><%=GetTran("000025", "会员姓名")%></span>
										        </th>
										        <th>
										        <span><%=GetTran("000322", "金额")%></span>
										        </th>
										        <th>
										        <span><%=GetTran("000414", "积分")%></span>
										        </th>
										        <th>
										        <span><%=GetTran("001429", "报单日期")%></span>
										        </th>
										        <th>
										        <span><%=GetTran("000793", "确认店铺")%></span>
										        </th>
										        <th>
										        <span><%=GetTran("000774", "报单途径")%></span>
										        </th>
										        <th>
                                                <span><%=GetTran("006049", "店铺确认")%></span>
                                                </th>         
                                            <th>
                                                <span><%=GetTran("006048", "公司确认")%></span>
                                            </th>                                 
										    </tr>
										</table>
										</EmptyDataTemplate>
									</asp:GridView>
									</td>
							</tr>
							<tr>
								<td align="center" >
								    <uc1:Pager ID="Pager1" runat="server" />
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<br /><br />
			<font class="biaozzi" style="color:Red;"><%=GetTran("000224", "操作说明")%>：
			<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;１、<%=GetTran("001597", "查看本人团队各期的消费信息，点击明细栏中的“查看”可以看到每张订单的产品明细。")%></font>
			<div id="cssrain" style="display:none;">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
                        <tr>
                            <td width="150">
                                <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
                                    <tr>
                                        <td class="sec2" onclick="secBoard(0)">
                                            <%=GetTran("000032", "管 理")%>
                                        </td>
                                        <td class="sec1" onclick="secBoard(1)">
                                            <%=GetTran("000033", "说 明")%>
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
                            <tbody style="display: block">
                                <tr>
                                    <td valign="bottom" style="padding-left: 20px">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <a href="#">
                                                    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" Style="display: none;" /><img
                                                            onclick="__doPostBack('Button1','');" src="images/anextable.gif" width="49" height="47"
                                                            border="0"  style="cursor:hand;"/>
                            
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                            <tbody style="display: none">
                                <tr>
                                    <td style="padding-left: 20px">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; １、<%=GetTran("001597", "查看本人团队各期的消费信息，点击明细栏中的“查看”可以看到每张订单的产品明细。")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
			</form>
		<script src="../javascript/fontbold.js"></script>
	</body></html>
