<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfigParam2.aspx.cs" Inherits="Company_BauseParam" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>结算参数设置</title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
<script src="../JS/QCDS2010.js" type="text/javascript"></script>
        <script language="javascript" type="text/javascript" src="../javascript/ManagementVsExplanation.js"></script>
    <script language="javascript" type="text/javascript">
	window.onerror=function()
    {
        return true;
    };
    window.onload=function()
	{
	    down2();
	};
    </script>
    <style type="text/css">
        .style1
        {
            width: 350px;
        }
        .style2
        {
            width: 46%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" >
    <div align="center" class="biaozzi">
    <br />
    <h4><%=GetTran("001306", "调控参数设置")%></h4>
    <br />
        <table width="80%" border="0" cellpadding="0" cellspacing="0" class="tablemb" style="text-align:right;">
    <tr>
    <td colspan="3" align="center">
       <asp:DropDownList ID="ddlExpectNum" runat="server" AutoPostBack="True" 
            onselectedindexchanged="ddlExpectNum_SelectedIndexChanged">
</asp:DropDownList>
</td>
    </tr>
        <tr>
        <td colspan="2" height="20px">
        </td>
        </tr>
    <tr>
    <td class="style2">
        <%=GetTran("001241", "世联普商")%> <asp:TextBox Id = "txtPara1" runat="server" MaxLength="7"></asp:TextBox>
        </td>
        <td class="style1">
        <%=GetTran("001244", "世联咨商")%>&nbsp;<asp:TextBox Id = "txtPara2" runat="server" MaxLength="7"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
        </tr>
    <tr>
    <td class="style2">
        <%=GetTran("001246", "特别咨商")%>&nbsp;<asp:TextBox Id = "txtPara3" runat="server" MaxLength="7"></asp:TextBox>
        </td>
        <td class="style1">
        <%=GetTran("001247", "高级咨商")%>&nbsp;<asp:TextBox Id = "txtPara4" runat="server" MaxLength="7"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
        </tr>
    <tr>
    <td class="style2">
        <%=GetTran("001248", "全面咨商")%>&nbsp;<asp:TextBox Id = "txtPara5" runat="server" MaxLength="7"></asp:TextBox>
        </td>
        <td class="style1">
        <%=GetTran("001250", "回本奖第一层比例")%>&nbsp;<asp:TextBox Id = "txtPara7" runat="server" MaxLength="7"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
        </tr>
    <tr>
    <td class="style2">
        <%=GetTran("001252", "推荐奖比例")%>&nbsp;<asp:TextBox Id = "txtPara6" runat="server" MaxLength="7"></asp:TextBox>
        </td>
        <td class="style1">
        <%=GetTran("001254", "回本奖第二层起比例")%>&nbsp;<asp:TextBox Id = "txtPara8" runat="server" MaxLength="7"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
        </tr>
    <tr>
    <td class="style2">
        <%=GetTran("001255", "大区奖层数")%>&nbsp;<asp:TextBox Id = "txtPara9" runat="server" MaxLength="7"></asp:TextBox>
        </td>
        <td class="style1">
        <%=GetTran("001257", "大区奖比例")%><asp:TextBox Id = "txtPara10" runat="server" MaxLength="7"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
        </tr>
    <tr>
    <td class="style2">
        <%=GetTran("001260", "大区奖封顶")%><asp:TextBox Id = "txtPara11" runat="server" MaxLength="7"></asp:TextBox>
        </td>
        <td class="style1">
        <%=GetTran("001261", "小区奖推荐人数")%><asp:TextBox Id = "txtPara12" runat="server" MaxLength="7"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
        </tr>
    <tr>
    <td class="style2">
        <%=GetTran("001262", "世联普商小区奖金比例")%><asp:TextBox Id = "txtPara13" runat="server" MaxLength="7"></asp:TextBox>
        </td>
        <td class="style1">
         <%=GetTran("001263", "世联资商小区奖金比例")%><asp:TextBox Id = "txtPara14" runat="server" MaxLength="7"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
        </tr>
    <tr>
    <td class="style2">
        <%=GetTran("001264", "世联特别资商小区奖金比例")%><asp:TextBox Id = "txtPara15" runat="server" MaxLength="7"></asp:TextBox>
        </td>
        <td class="style1">
        <%=GetTran("001265", "世联高级咨商小区奖金比例")%><asp:TextBox Id = "txtPara16" runat="server" MaxLength="7"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
        </tr>
    <tr>
    <td class="style2">
        <%=GetTran("001266", "世联全面资商小区奖金比例")%><asp:TextBox Id = "txtPara17" runat="server" MaxLength="7"></asp:TextBox>
        </td>
        <td class="style1">
        <%=GetTran("001268", "世联普商小区奖封顶")%><asp:TextBox Id = "txtPara18" runat="server" MaxLength="7"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
        </tr>
    <tr>
    <td class="style2">
        <%=GetTran("001269", "世联资商小区奖封顶")%><asp:TextBox Id = "txtPara19" runat="server" MaxLength="7"></asp:TextBox>
        </td>
        <td class="style1">
        <%=GetTran("001270", "世联特别资商小区奖封顶")%><asp:TextBox Id = "txtPara20" runat="server" MaxLength="7"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>


        </tr>
            <tr>
    <td class="style2">
        <%=GetTran("008122", "世联高级咨商小区奖封顶")%> <asp:TextBox Id = "txtPara21" runat="server" MaxLength="7"></asp:TextBox>
        </td>
        <td class="style1">
        <%=GetTran("008123", "世联全面资商小区奖封顶")%>&nbsp;<asp:TextBox Id = "txtPara22" runat="server" MaxLength="7"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
        </tr>

            <tr>
    <td class="style2">
        <%=GetTran("008124", "永续奖会员开始月数")%> <asp:TextBox Id = "txtPara23" runat="server" MaxLength="7"></asp:TextBox>
        </td>
        <td class="style1">
        <%=GetTran("008125", "永续奖重消额")%>&nbsp;<asp:TextBox Id = "txtPara24" runat="server" MaxLength="7"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
        </tr>
             <tr>
    <td class="style2">
        <%=GetTran("008126", "永续奖层数")%> <asp:TextBox Id = "txtPara25" runat="server" MaxLength="7"></asp:TextBox>
        </td>
        <td class="style1">
        <%=GetTran("008127", "永续奖比例")%>&nbsp;<asp:TextBox Id = "txtPara26" runat="server" MaxLength="7"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
        </tr>
             <tr>
    <td class="style2">
        <%=GetTran("008128", "网平台综合管理费")%> <asp:TextBox Id = "txtPara27" runat="server" MaxLength="7"></asp:TextBox>
        </td>
        <td class="style1">
        <%=GetTran("008129", "网扣福利奖金")%>&nbsp;<asp:TextBox Id = "txtPara28" runat="server" MaxLength="7"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
        </tr>
             <tr>
    <td class="style2">
        <%=GetTran("008130", "网扣重复消费")%> <asp:TextBox Id = "txtPara29" runat="server" MaxLength="7"></asp:TextBox>
        </td>
    
        <td>
            &nbsp;</td>
        </tr>


        <tr>
        <td colspan="2" height="20px">
        </td>
        </tr>
    <tr>
    <td style="text-align:center" colspan="3">
        <asp:Button ID="btnEdit" runat="server" onclick="btnEdit_Click" Text="修 改" CssClass="anyes" /></td>

        </tr>
    </table>
    </div>
    <div id="cssrain" style="width:100%">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
            <tr>
                <td width="80px">
                    <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
                        <tr>
                            <td class="sec2">
                                <span id="span1" title="" onmouseover="cutDescription()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
                            </td>
                        </tr>
                    </table>
                </td>
                <td><a href="#"><img src="images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX" onclick="down2()" /></a></td>
            </tr>
        </table>
        <div id="divTab2">
            <table width="100%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
                <tbody style="display: block" id="tbody1">
                    <tr>
                        <td style="padding-left: 20px">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td><%=GetTran("006873", "1、根据拨出率的调控措施，设置不同的调控参数。")%></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
