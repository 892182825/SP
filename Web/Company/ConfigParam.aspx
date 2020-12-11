<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfigParam.aspx.cs" Inherits="Company_BauseParam" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>结算参数设置</title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../JS/QCDS2010.js"></script>
    <script language="javascript" type="text/javascript" src="../javascript/ManagementVsExplanation.js"></script>
    <script language="javascript" type="text/javascript">
        window.onerror = function() {
            return true;
        }
        window.onload = function() {
            down2();
        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 50%;
        }
        .style2
        {
            /*width: 278px;*/
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" >
    <div align="center" class="biaozzi">
    <br />
    <h4><%=GetTran("001240", "参数设置")%></h4>
    <br />
    <table width="80%" border="0" cellpadding="0" cellspacing="0" class="tablemb" style="text-align:right;">
        <tr>
            <td colspan="3" align="center">
               <asp:DropDownList ID="ddlExpectNum" runat="server" AutoPostBack="True" onselectedindexchanged="ddlExpectNum_SelectedIndexChanged"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2" height="20px">
            </td>
        </tr>
        <tr>
            <td class="style1">
                投资金额1&nbsp;<asp:TextBox Id = "txtPara1" runat="server" MaxLength="7"></asp:TextBox>
            </td>
            <td class="style2">
                分配石斛积分比例1&nbsp;<asp:TextBox Id = "txtPara4" runat="server" MaxLength="7"></asp:TextBox>
              
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                投资金额2&nbsp;<asp:TextBox Id = "txtPara2" runat="server" MaxLength="7"></asp:TextBox>
            </td>
            <td class="style2">
                分配石斛积分比例2&nbsp;<asp:TextBox Id = "txtPara5" runat="server" MaxLength="7"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                投资金额3&nbsp;<asp:TextBox Id = "txtPara3" runat="server" MaxLength="7"></asp:TextBox>
            </td>
            <td class="style2">
                分配石斛积分比例3&nbsp;<asp:TextBox Id = "txtPara6" runat="server" MaxLength="7"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                释放石斛积分初始比例1&nbsp;<asp:TextBox Id = "txtPara7" runat="server" MaxLength="7"></asp:TextBox>
            </td>
            <td class="style2">
                释放石斛积分扣除消费比例&nbsp;<asp:TextBox Id = "txtPara8" runat="server" MaxLength="7"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        
        <tr>
            <td class="style1">
                释放石斛积分初始比例2&nbsp;<asp:TextBox Id = "txtPara17" runat="server" MaxLength="7"></asp:TextBox>
            </td>
            <td class="style2">
                业绩奖释放比例&nbsp;<asp:TextBox Id = "txtPara9" runat="server" MaxLength="7"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                释放石斛积分初始比例3&nbsp;<asp:TextBox Id = "txtPara18" runat="server" MaxLength="7"></asp:TextBox>
            </td>
            <td class="style2">
              业绩奖释放速度比例&nbsp;<asp:TextBox Id = "txtPara10" runat="server" MaxLength="7"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                业绩奖释放封顶比例&nbsp;<asp:TextBox Id = "txtPara11" runat="server" MaxLength="7"></asp:TextBox>
            </td>
            <td class="style2">
               违约金比例&nbsp;<asp:TextBox Id = "txtPara12" runat="server" MaxLength="7"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                手续费比例&nbsp;<asp:TextBox Id = "txtPara13" runat="server" MaxLength="7"></asp:TextBox>
            </td>
            <td class="style2">
                石斛积分增长速度比例&nbsp;<asp:TextBox Id = "txtPara14" runat="server" MaxLength="7"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                石斛积分发行量&nbsp;<asp:TextBox Id = "txtPara15" runat="server" MaxLength="15"></asp:TextBox>
            </td>
            <td class="style2">
                封顶石斛积分量&nbsp;<asp:TextBox Id = "txtPara16" runat="server" MaxLength="15"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                积分最低买入金额&nbsp;<asp:TextBox Id = "txtPara20" runat="server" MaxLength="15"></asp:TextBox>
            </td>
            <td class="style2">
                积分最高买入金额&nbsp;<asp:TextBox Id = "txtPara21" runat="server" MaxLength="15"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                积分最低卖出金额&nbsp;<asp:TextBox Id = "txtPara22" runat="server" MaxLength="15"></asp:TextBox>
            </td>
            <td class="style2">
                积分最高卖出金额&nbsp;<asp:TextBox Id = "txtPara23" runat="server" MaxLength="15"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                奖金日封顶倍数&nbsp;<asp:TextBox Id = "txtPara19" runat="server" MaxLength="7"></asp:TextBox>
            </td>
            <td class="style2">
                买入赠送比例&nbsp;<asp:TextBox Id = "txtPara24" runat="server" MaxLength="7"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>

        <tr>
            <td style="text-align:center" colspan="3">
                <asp:Button ID="btnEdit" runat="server" onclick="btnEdit_Click" Text="修改" CssClass="anyes" />
            </td>
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
                                    <td><%=GetTran("006869", "1、设置某一期的结算参数的数值，当结算该期时，系统自动按照设置的数值进行结算。")%></td>
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