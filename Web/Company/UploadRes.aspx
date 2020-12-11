<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadRes.aspx.cs" Inherits="Company_UploadRes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link type="text/css" rel="Stylesheet" href="CSS/Company.css" />

    <script src="js/jquery-1.4.3.min.js" type="text/javascript"></script>
    
    <script language="javascript">
        function chk(source, arguments) {
            var s = document.getElementById("upFile").value;

            if (s.length == 0) {
                arguments.IsValid = false;
            }
            s = s.substr(s.lastIndexOf(".") + 1);
            s = s.toLowerCase();
            //if(s=="exe"||s=="scr"||s=="sys"||s=="inf"||s=="vmx"||s=="Jscript") 
            //if(s!="rar"&&s!="zip"&&s!="doc"&&s!="jpg"&&s!="png"&&s!="gif")
            if (s != "rar" && s != "zip") {
                arguments.IsValid = false;
            }
            else {
                arguments.IsValid = true;
            }
        }
        function Check() {

            var name = document.getElementById("txtResName").value;
            var describe = document.getElementById("txtjianjie").value;
            var file = document.getElementById("upFile").value;

            if (name == "") {
                alert('<%=GetTran("006938","资料名称不能为空") %>');
                return false;
            }
            if (describe.length > 50) {
                alert('<%=GetTran("001392","简介不能超过50个字") %>');
                return false;
            }
            if (file == "") {
                alert('<%=GetTran("001501","请选择要上传的资料") %>');
                return false;
            }
            return true;
        }

        function ChangeValue() {

            var ss = $("#DropDownList2").val();
            if (ss == "2") {
                //alert($("#Lev").attr("id"))
                $("#Lev").show();
            }
            else {
                $("#Lev").hide();
            }
        }
        /*检验文件是否存在*/
        function CheckFileNames() {
            var resName = document.getElementById("txtResName").value;
            if (resName == "") {
                alert("资料名称和资料简介不能为空!");
                return false;
            }
            if (resName.length > 50) {
                alert("输入的简介超过50个字符!");
                return false;
            }
            var msg = AjaxClass.CheckFileNames(resName).val;
            if (msg != "") {
                alert(msg);
            }
            else {
                alert("不存在同名资料，可以使用！");
            }
            return false;
        }
	</script>	
	<script type="text/javascript" src="../js/SqlCheck.js"></script>
</head>
<body onload=""><%--setZF('txtjianjie',50)--%>
    <form id="form1" runat="server" onsubmit="return filterSql_III()" method="post" enctype="multipart/form-data">
    <div>
    <br />
        <table width="100%" class="biaozzi" >
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td align="center" valign="middle" width="100%" height="60px" style="font-size:20px; font-weight:bold;">
                                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table align="center" width="80%" class="biaozzi" >
                    <div runat="server" >
                    <tr id="ShowCountry" runat="server">
                    <td width="40%" align="right"><%=GetTran("000047", "国家")%>：
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList1" runat="server">
                        </asp:DropDownList>
                    </td>
                    </tr>
                    
                    <tr style="display:none;">
                    <td align="right">
                    <%=GetTran("000000", "下载对象")%>：
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList2" onchange="ChangeValue()" runat="server">
                        <asp:ListItem Selected="True" Value="0">全部</asp:ListItem>
                        <asp:ListItem  Value="1" >店铺</asp:ListItem>
                        <asp:ListItem  Value="2" >会员</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    </tr>
                    
                    <tr id ="Lev" runat="server" style="display:none">
                    <td align="right">
                    <%=GetTran("000903", "会员级别")%>：
                    </td>
                    <td>
                    <asp:DropDownList ID="DropDownList3" runat="server">
                        </asp:DropDownList>
                    
                    </td>
                    </tr>
                    
                    </div>
                        <tr>
                            <td width="40%" align="right"><%=GetTran("000204", "资料名称")%>：
                            </td>
                            <td width="60%">
                                <asp:TextBox ID="txtResName" runat="server" MaxLength="20"></asp:TextBox>
                                <asp:Button ID="btncheckName" OnClientClick="return CheckFileNames()" runat="server" Text="文件是否存在" 
                                    OnClick="btncheckName_Click" CssClass="another"/>
                            </td>
                        </tr>
                        <tr>
                            <td align="right"><%=GetTran("000280", "资料简介")%>：
                            </td>
                            <td>
                                <asp:TextBox ID="txtjianjie" runat="server" Height="63px" TextMode="MultiLine" 
                                    Width="303px"></asp:TextBox>
                                <span style="color:Black; font-size:12px;">*<%=GetTran("001392","简介不能超过50个字") %></span></td>
                        </tr>
                        <tr id="showfile" runat="server">
                            <td align="right"><%=GetTran("001394", "上传文件")%>：
                            </td>
                            <td>
                            <br>
                                <asp:FileUpload ID="upFile" runat="server" />
                                <%--<INPUT id="upFile" type="file" name="upFile" runat="server" />--%>
                                <br>
                                <asp:CustomValidator ID="CustomValidator1" runat="server" 
                                    ControlToValidate="upFile" ErrorMessage="上传文件格式不对(只能上传rar zip)，或者文件的名字过长" 
                                    ClientValidationFunction="chk"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="btnUpLoad" OnClientClick="return Check()" runat="server" OnClick="btnUpLoad_Click"  Text="开始上传"  CssClass="anyes"/>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Label ID="labUpInfo" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
