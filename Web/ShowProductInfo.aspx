<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowProductInfo.aspx.cs" Inherits="ShowProductInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>商品详细信息</title>
    <link href="CSS/ShowProduct.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div  style="margin-top:50px; margin-left:50px;">
        <div class="prouduct">
    	<div class="prod">
        	<dl>
            	<dd><asp:Image ID="ProductImage" runat="server" Height="250px" Width="250px" /></dd>
                <dt class="title"><asp:Label ID="lblPName" runat="server" Style="margin-left:60px;"></asp:Label></dt>
                <dt><span class="fontl"><%=GetTran("000558","产品编号")%>：</span><span class="fontw"><asp:Label ID="lblPCode" runat="server"></asp:Label></span></dt>
                <dt><span class="fontl"><%=GetTran("007190","产品类别")%>：</span><span class="fontw"><asp:Label ID="lblPType" runat="server"></asp:Label></span></dt>
                <dt><span class="fontl"><%=GetTran("000880","产品规格")%>：</span><span class="fontw"><asp:Label ID="lblPSpec" runat="server"></asp:Label></span></dt>
                <dt><span class="fontl"><%=GetTran("002084","价格")%>：</span><span class="fontw"><asp:Label ID="lblPrice" runat="server"></asp:Label></span></dt>
                <dt><span class="fontl">PV：</span><span class="fontw"><asp:Label ID="lblPPV" runat="server"></asp:Label></span></dt>
            <dt class="an">
                	<ul>
                    	<li class="a1">
                            <asp:LinkButton ID="LinkButton2" runat="server" onclick="LinkButton2_Click"></asp:LinkButton></li>
                    	<li id="a2">
                            <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click"></asp:LinkButton></li>
                    </ul>
              </dt>
              
            </dl>
        </div>
        <div class="prodside">
        	<div class="prodside_tit"><span class="btbg"><%=GetTran("000560","产品信息")%></span></div>
                <div id="div_PDetails" runat="server">
                    
            </div>
        </div>
    </div>
            
    </div>
    </form>
</body>
</html>
