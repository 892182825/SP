<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StoreOff.aspx.cs" Inherits="Company_StoreOff" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <link href="CSS/level.css" rel="Stylesheet" type="text/css" />
    <link href="CSS/Company.css"rel="stylesheet" type="text/css" />
      <script language="javascript" type="text/javascript" src="../js/SqlCheck.js"></script>
 <script language="javascript" type="text/javascript">
    function CheckText()
	{
		//这个方法是只有1个lkSubmit按钮时候 可直接用
		filterSql();
	}

	

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <div align="center">
    <table class="tablemb"  width="500px">
            <tr>
               <th colspan="2">
                    <b><%=GetTran("007195", "注销服务机构")%></b>
                </th>
            </tr>
            <tr>
                <td align="right">
                    <%=GetTran("007574", "注销服务机构编号")%>：
                    
                    
                </td>
                <td align="left">
                    <asp:TextBox ID="txtStoreid" runat="server" MaxLength="10"></asp:TextBox>
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_Select" runat="server" Text="查看" CssClass="another" 
                        onclick="btn_Select_Click" />
                </td>
            </tr>
            <tr>
                <td align="right"><%=GetTran("004134","操作人编号")%>：</td>
                <td align="left">
                    <asp:TextBox ID="txtOperatorNo" runat="server" MaxLength="10" ReadOnly="true"></asp:TextBox>
                </td>
                
            </tr>
            <tr style="display:none;">
                <td align="right"><%=GetTran("007191", "操作人姓名")%>：</td>
                <td align="left">
                    <asp:TextBox ID="txtOperatorName" runat="server" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <%=GetTran("007164","注销原因")%>：
                </td>
                <td align="left">
                    <asp:TextBox ID="txtMemberOffreason" runat="server" TextMode="MultiLine" Height="60px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                 <asp:linkbutton id="lkSubmit"  Runat="server" Text="提交" onclick="lkSubmit_Click" style="display:none"></asp:linkbutton>
                                <input class="another" id="bSubmit" onclick="CheckText()" type="button" value="<%=GetTran("001296", "确认注销")%>" />
                                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnquery" runat="server" Text="确认注销" OnClick="btnquery_Click"  visible="false" CssClass="anyes"></asp:Button>
                    <asp:Button ID="Button1" runat="server" Text="返回" 
                         CssClass="another" onclick="Button1_Click"></asp:Button>
                </td>    
            </tr>
           <tr>
                <td colspan="2">
                    <asp:Label ID="LabelResponse" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        <%=msg %>
        </div>
    </form>
</body>
</html>
