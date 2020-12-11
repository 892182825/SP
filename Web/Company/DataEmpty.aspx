<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DataEmpty.aspx.cs" Inherits="Company_DataEmpty" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    
    <link type="text/css" rel="Stylesheet" href="CSS/Company.css" />
    <script type="text/javascript" language="javascript">
        function ManageIdValid()
        {
            var manageId = document.getElementById("txtManageId").value;
            if(manageId.length==0)
            {
                document.getElementById("spanManage").value="编号不能为空";
                return;
            }
            if(manageId.length<4||manageId.length>10)
            {
                document.getElementById("spanManage").value="编号必须在4位到10位之间";
                return;
            }
            
            var validSTR="abcdefghijklmnopqrstuvwxyz1234567890";

	        for(var i=1;i<manageId.length+1;i++)
	        {
		        if (validSTR.indexOf(manageId.substring(i-1,i))==-1)
		        {
		            document.getElementById("spanManage").value = '<%=this.GetTran("000309","编号请输入字母，数字！") %>';
			        return ;
		        }
	        }
        }
        
        function ManageIdValid2()
        {
            var manageId = document.getElementById("txtManageId").value;
            if(manageId.length==0)
            {
                document.getElementById("spanManage").value="编号不能为空";
                return false;
            }
            if(manageId.length<4||manageId.length>10)
            {
                document.getElementById("spanManage").value="编号必须在4位到10位之间";
                return false;
            }
            
            var validSTR="abcdefghijklmnopqrstuvwxyz1234567890";

	        for(var i=1;i<manageId.length+1;i++)
	        {
		        if (validSTR.indexOf(manageId.substring(i-1,i))==-1)
		        {
		            document.getElementById("spanManage").value = '<%=this.GetTran("000309","编号请输入字母，数字！") %>';
                    return false;			        
		        }
	        }
	        return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <br />
    <br />
    <br />
    <div id="div1" runat="server">
        <table width="600" border="0" align="center" cellpadding="0" cellspacing="0" class="biaozzi">
            <tr>
                <td>
                    请输入密码：<asp:TextBox ID="txtPwd" runat="server" MaxLength="10" TextMode="Password"></asp:TextBox>
                    <asp:Button ID="btnOk" runat="server" Text="确定" CssClass="anyes" 
                        onclick="btnOk_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div id="div2" runat="server">
        <table width="600" border="0" align="center" cellpadding="0" cellspacing="0" class="biaozzi">
            <tr>
                <td align="right">
                    系统默认管理员编号：
                </td>
                <td>
                    <asp:TextBox ID="txtManageId" MaxLength="10" onblur="ManageIdValid()" runat="server"></asp:TextBox>
                    &nbsp;&nbsp;
                    <asp:Button ID="Button1" runat="server" Text="确定" CssClass="anyes" 
                        ontextchanged="TextBox1_TextChanged" onclick="Button1_Click"></asp:Button>&nbsp;&nbsp;
                    <a href="DefaultDetails.aspx?type=1">查看明细</a>
                        <span style="color:Red" id="spanManage"></span>
                </td>
            </tr>
            <tr>
                <td align="right">
                    系统默认店铺编号：
                </td>
                <td>
                    <asp:TextBox ID="txtStoreId" MaxLength="10" runat="server"></asp:TextBox>
                    &nbsp;&nbsp;
                    <asp:Button ID="Button2" runat="server" Text="确定" CssClass="anyes" 
                        onclick="Button2_Click"></asp:Button>&nbsp;&nbsp;
                    <a href="DefaultDetails.aspx?type=2">查看明细</a>
                </td>
            </tr>
            <tr>
                <td align="right">
                    系统默认会员编号：
                </td>
                <td>
                    <asp:TextBox ID="txtMemberId" MaxLength="10" runat="server"></asp:TextBox>
                    &nbsp;&nbsp;
                    <asp:Button ID="Button3" runat="server" Text="确定" CssClass="anyes" 
                        onclick="Button3_Click"></asp:Button>&nbsp;&nbsp;
                    <a href="DefaultDetails.aspx?type=3">查看明细</a>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                        <asp:Button ID="btnSave" runat="server" Text="数据初始化" OnClick="btnSave_Click" 
                            CssClass="another" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnReturn" runat="server" Text="返 回" OnClick="btnReturn_Click" CssClass="anyes" />
                </td>
            </tr>
        </table>
    </div>
<br />
			<br />
			<br/>
			<TABLE id="Table8" cellSpacing="0" cellPadding="0" width="80%" border="0" align=center>
				<!--<TR>
					<TD><IMG height="10" src="../images/common/spacer.gif" width="1"></TD>
				</TR>
				<TR>
					<TD bgColor="#1a71b9"><IMG height="2" src="../images/common/spacer.gif" width="1"></TD>
				</TR>-->
				<tr>
					<td class="zihz12"><%=GetTran("000224", "操作说明")%>：</td>
				</tr>
				<tr>
					<td class="zihz12">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;１、<%=GetTran("001493", "初始化数据会先为其先备份原有的数据")%>。</td>
				</tr>
			</TABLE>
    </div>
    </form>
</body>
</html>
