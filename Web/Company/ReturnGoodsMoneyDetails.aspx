<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReturnGoodsMoneyDetails.aspx.cs"
    Inherits="Company_ReturnGoodsMoneyDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <%=GetTran("001806", "退货单详细")%></title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />

    <script src="../JS/QCDS2010.js" type="text/javascript"></script>

    <style type="text/css">
        a
        {
            margin-left: 10px;
            font-size: 13px;
            text-decoration: none;
            float: left;
        }
    </style>

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
		    window.onerror=function()
		    {
		        return true;
		    };
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

    <script language="javascript">
     function secBoard(n)
  {
       for(i=0;i<secTable.cells.length;i++)
      secTable.cells[i].className="sec1";
    secTable.cells[n].className="sec2";
    for(i=0;i<mainTable.tBodies.length;i++)
      mainTable.tBodies[i].style.display="none";
    mainTable.tBodies[n].style.display="block";
  }
          window.onload=function()
	    {
	        down2();
	    };
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" align="center" width="100%">
        <tr align="center">
            <td>
                <br />
                <table border="0" cellpadding="0" cellspacing="0" class="biaozzi" align="center"
                    width="100%">
                    <tr>
                        <td style="word-break: keep-all; word-wrap: normal; border: rgb(147,226,244) solid 1px">
                            <asp:GridView ID="gwdetails" runat="server" AutoGenerateColumns="false" AllowSorting="false"
                                OnSorting="gwdetails_Sorting" CssClass="tablemb bordercss" OnRowDataBound="gwdetails_RowDataBound"
                                Width="100%">
                                <AlternatingRowStyle BackColor="#F1F4F8" />
                                <RowStyle HorizontalAlign="Center" />
                                <Columns>
                                    <asp:BoundField HeaderText="退货编号" DataField="DocID" SortExpression="DocID" ItemStyle-Wrap="false"
                                        HeaderStyle-Wrap="false" />
                                    <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                        <HeaderTemplate>
                                            <span>产品名称</span>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <span>
                                                <%#getName(Eval("ProductID")) %></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="退货数量" DataField="ProductQuantity" ItemStyle-Wrap="false"
                                        HeaderStyle-Wrap="false" />
                                    <asp:BoundField HeaderText="期数" DataField="ExpectNum" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                    <asp:BoundField HeaderText="价格" DataField="UnitPrice" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                    <%--                <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                        <HeaderTemplate>
                                            <span>币种</span>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <span>
                                                <%#getMoney(Eval("ProductID"))%></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:BoundField HeaderText="积分" DataField="PV" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                </Columns>
                                <EmptyDataTemplate>
                                    <table class="tablemb" backcolor="#F8FBFD" width="100%">
                                        <tr>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("002088", "退货编号")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("000501", "产品名称")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("001982", "退货数量")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("000045", "期数")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("002084", "价格")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("000414", "积分")%>
                                            </th>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="word-break: keep-all; word-wrap: normal" align="left">
                            <asp:Button ID="btnEHistory" runat="server" Text="返 回" CssClass="anyes" OnClick="btnEHistory_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
