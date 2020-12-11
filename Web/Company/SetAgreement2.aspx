<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetAgreement2.aspx.cs" Inherits="Company_SetAgreement2" ValidateRequest="false"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
     <link type="text/css" rel="Stylesheet" href="CSS/Company.css" />
      <script type="text/javascript" src="../EDIT/kindeditor.aspx" ></script>

    <script type="text/javascript">
				KE.show
				({
					id : 'content1',
					cssPath : 'EDIT/index.css',

					width : '700px',
					height:'300px',
					resizeMode : 2 //0:不能拖动，1:只能往下拖，2:可以拖动
				});
		</script>
		
		<script type="text/javascript">

	function abc()
	{
		var str=document.getElementById("TextBox1").value;

		document.getElementById("content1").value=str;
	}
	
	function ddd()
	{
		var str=KE.util.getData('content1');
		document.getElementById("TextBox1").value=str;

		__doPostBack("LinkButton1",'');
	}
function Button1_onclick() {

}

		</script>
</head>
<body onload="abc()">
    <form id="form1" runat="server">
        <div>
         <br />
         <table cellspacing="0" cellpadding="0" class="biaozzi">
            <tr>
                <td align="center" style="font-size:20px;"> <%=GetTran("007927", "协议内容")%> </td>
            </tr>
            <tr>
                <td>
                    <%=GetTran("000047","国家") %>： <asp:DropDownList ID="ddlCountry" runat="server" 
                        onselectedindexchanged="ddlCountry_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <%=GetTran("004169","语言") %>：<asp:DropDownList ID="ddlLanguage" runat="server" 
                        onselectedindexchanged="ddlLanguage_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                </td>
            </tr> 
            <tr>
                <td>
                
                </td>
            </tr>
            <tr>
                <td valign="top" align="center" style="word-spacing:normal">
                    <div id="div2" style="DISPLAY: none"><asp:TextBox id="TextBox1" runat="server"></asp:TextBox></div>
                    <div class="editor"><textarea id="content1" style="VISIBILITY: hidden; WIDTH: 700px; HEIGHT: 200px" name="content"></textarea>
				    </div>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <input id="Button1" type="button" value='<%=GetTran("000434","确定") %>' class="anyes" onclick="ddd()" onclick="return Button1_onclick()" />&nbsp;<asp:LinkButton ID="LinkButton1" runat="server" 
                        onclick="btn_Save_Click"></asp:LinkButton>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_Cancle" runat="server" Text="取 消" CssClass="anyes" 
                        onclick="btn_Cancle_Click"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <input type="button" id="butt_Query"value='<%=GetTran("000421","返回") %>' style="cursor:pointer" Class="anyes" onclick="history.back()"/>
                </td>
            </tr>
        </table>
       </div>
    </form>
</body>
</html>

