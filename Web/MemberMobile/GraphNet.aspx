<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GraphNet.aspx.cs" Inherits="Company_GraphNet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    
    <style type="text/css">
        .wlttab2
        {
        	width:100%;
        	height:100%;
        	/*border-left:rgb(176,176,176) solid 1px;
        	border-top:rgb(176,176,176) solid 1px;*/
        }
        
        .wlttab2 td
        {
        	border-right:rgb(176,176,176) solid 1px;
        	border-bottom:rgb(176,176,176) solid 1px;
   
        	text-align:center;
        }
        
         .anyes {
            background-image:url("../images/sysButtonCen.png");
            background-repeat:no-repeat;
            color:#FFFFFF;
            font-family:"微软雅黑";
            font-size:14px;
            font-weight:bold;
            height:24px;
            line-height:24px;
            margin-left:10px;
            text-align:center;
            text-decoration:none;
            width:65px;
            border-width:0;
            }
    </style>
    <script type="text/javascript">
        function showcy1()
        {
            window.parent.location.href="MemberNetMapFrame.aspx?isAnzhi=az";
            return false;
        }
        
        function showcy2()
        {
            window.parent.location.href="CommonlyNetwork.aspx";
		    return false;
        }
        
        function showSX()
		{
		     window.parent.location.href="SST_AZ.aspx";
		}

    </script>
</head>
<body  style="font-size:10pt">
    <form id="form1" runat="server">
    <br />
    <div style="padding-left:10px" >
        <asp:Button ID="Button1" runat="server" Text="显示" class="anyes" OnClick="Button1_Click" /> 
        <span style="color:rgb(0,61,92)">
        <%=GetTran("000045", "期数")%>：
        </span>
        <asp:DropDownList ID="DDLQs" runat="server">
        </asp:DropDownList>
        <span style="color:rgb(0,61,92)">
        <%=GetTran("000024","会员编号")%>：
        </span>
        <asp:TextBox ID="txNumber" runat="server"></asp:TextBox>
        
        <asp:Button ID="Button2" runat="server" Text="常用(1)" class="anyes" onclientclick="return showcy1()"  />
		<asp:Button ID="Button3" runat="server" Text="常用(2)" style="display:none;" class="anyes" onclientclick="return showcy2()"  />
		<asp:Button ID="Button4" runat="server" Text="常用(3)" class="anyes" Enabled="false"  />
		        
        <input type="button" ID="Button5" runat="server"   value="伸缩" class="anyes" onclick="showSX()" />
       
        <br><br>
        <span style="color:rgb(0,61,92)">
        <%=GetTran("007032","链路图")%>：
        </span>
        <asp:Literal ID="LitLLT" runat="server"></asp:Literal>
    </div>
    <asp:Literal ID="network" runat="server"></asp:Literal>
    </form>
</body>

</html>
