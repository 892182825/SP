<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberOff.aspx.cs" Inherits="Company_MemberOff" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <base target="_self" />
    <link href="CSS/level.css" rel="Stylesheet" type="text/css" />
    <link href="CSS/Company.css"rel="stylesheet" type="text/css" />
      <script language="javascript" type="text/javascript" src="../js/SqlCheck.js"></script>
 <script language="javascript" type="text/javascript">
    function CheckText()
	{
		//这个方法是只有1个lkSubmit按钮时候 可直接用
		filterSql();
	}
        function getname(){
            var number = document.getElementById("txtNumber").value;
            var name = AjaxClass.GetName(number).value;
            document.getElementById("Label4").innerHTML=name;
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
                    <b>
                        <asp:Label ID="Label1" runat="server" Text='<%=GetTran("001282", "会员注销")%>'></asp:Label> 
                    </b>
                </th>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label2" runat="server" Text=' <%=GetTran("001295", "注销会员编号")%>：'></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtNumber" runat="server" MaxLength="10" onblur="getname()"></asp:TextBox>
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_Select" runat="server" Text="查看" CssClass="another" 
                        onclick="btn_Select_Click" />
                </td>
            </tr>
            <tr style="display:none;">
                <td align="right"><%=GetTran("004134","操作人编号")%>：</td>
                <td align="left">
                    <asp:Label ID="txtOperatorNo" runat="server" Text="Label"></asp:Label>
                </td>
                
            </tr>
            <tr>
                <td align="right">
                    <%=GetTran("000025", "会员姓名")%>：
                </td>
                <td align="left">
                    <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
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
                    
                    <asp:Label ID="Label3" runat="server" Text='<%=GetTran("007164","注销原因")%>：'></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtMemberOffreason" runat="server" TextMode="MultiLine" Height="60px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                
                </td>
                <td align="left">
                 <asp:linkbutton id="lkSubmit"  Runat="server" Text="提交" onclick="lkSubmit_Click" style="display:none"></asp:linkbutton>
                                <input class="another" id="bSubmit" onclick="CheckText()" type="button" value="<%=GetTran("000064", "确认")%>" />
                <asp:Button ID="btnquery" runat="server" Text="确认" OnClick="btnquery_Click"  visible="false" CssClass="anyes"></asp:Button>
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
