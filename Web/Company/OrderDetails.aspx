<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderDetails.aspx.cs" Inherits="Company_OrderDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />

    <script src="../JS/QCDS2010.js" type="text/javascript"></script>

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
    <table class="biaozzi" width="100%" >
        <tr>
            <td >
                <asp:GridView ID="gv_orderDetails" 
                    style="word-break: keep-all; word-wrap: normal;" runat="server" 
                    AutoGenerateColumns="false" Width="100%"
                    BackColor="#F8FBFD" CssClass="tablemb" 
                    OnRowDataBound="gv_orderDetails_RowDataBound">
                    <Columns>
                        <asp:BoundField HeaderText="订单号" DataField="OrderId" />
                        <asp:BoundField HeaderText="店铺编号" DataField="StoreId" />
                        <asp:BoundField HeaderText="产品名称" DataField="ProductName" />
                        <asp:BoundField HeaderText="产品型号" DataField="ProductTypeName" />
                        <asp:BoundField HeaderText="数量" DataField="Quantity" />
                        <asp:BoundField HeaderText="单价" DataField="Price" DataFormatString="{0:f2}"/>
                        <asp:BoundField HeaderText="积分" DataField="PV" DataFormatString="{0:f2}"/>
                        <asp:BoundField HeaderText="总金额" DataField="totalmoney" DataFormatString="{0:f2}"/>
                    </Columns>
                    <AlternatingRowStyle BackColor="#F1F4F8" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                <input type="button" onclick="javascript:history.go(-1)" class="anyes" 
                    style="cursor:hand; height: 22px;" value='<%=this.GetTran("000421", "返回")%>'>
                    </input>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
