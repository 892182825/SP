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
        <tr style="display:none;">
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
              X1矿机价格 &nbsp;<asp:TextBox Id = "txtPara1" runat="server" MaxLength="7"></asp:TextBox>
            </td>
              <td class="style2">
                X1矿机收益率&nbsp;<asp:TextBox Id = "txtPara8" runat="server" MaxLength="7"></asp:TextBox>
            </td>
            
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                X2矿机价格&nbsp;<asp:TextBox Id = "txtPara2" runat="server" MaxLength="7"></asp:TextBox>
            </td>
               <td class="style2">
                X2矿机收益率&nbsp;<asp:TextBox Id = "txtPara9" runat="server" MaxLength="7"></asp:TextBox>
            </td>
             
          
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
               X3矿机价格&nbsp;<asp:TextBox Id = "txtPara3" runat="server" MaxLength="7"></asp:TextBox>
            </td>
            <td class="style2">
              X3矿机收益率&nbsp;<asp:TextBox Id = "txtPara10" runat="server" MaxLength="7"></asp:TextBox>
            </td>
            
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                X4矿机价格&nbsp;<asp:TextBox Id = "txtPara4" runat="server" MaxLength="7"></asp:TextBox>
              
            </td>
            <td class="style2">
                X4矿机收益率&nbsp;<asp:TextBox Id = "txtPara11" runat="server" MaxLength="7"></asp:TextBox>
            </td>
           
          
            <td>&nbsp;</td>
        </tr>
        
        <tr>
              <td class="style1">
                X5矿机价格&nbsp;<asp:TextBox Id = "txtPara5" runat="server" MaxLength="7"></asp:TextBox>
            </td>
             <td class="style2">
              X5矿机收益率&nbsp;<asp:TextBox Id = "txtPara12" runat="server" MaxLength="7"></asp:TextBox>
            </td>
          
         
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                X6矿机价格&nbsp;<asp:TextBox Id = "txtPara6" runat="server" MaxLength="7"></asp:TextBox>
            </td>
             <td class="style2">
                X6矿机收益率&nbsp;<asp:TextBox Id = "txtPara13" runat="server" MaxLength="7"></asp:TextBox>
            </td>
           
            <td>&nbsp;</td>
        </tr>
        <tr>
             <td class="style1">
               X7矿机价格&nbsp;<asp:TextBox Id = "txtPara7" runat="server" MaxLength="7"></asp:TextBox>
            </td>
            <td class="style2">
                X7矿机收益率&nbsp;<asp:TextBox Id = "txtPara14" runat="server" MaxLength="7"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr><td class="style1">
                A币每日涨幅 &nbsp;<asp:TextBox Id = "txtPara15" runat="server" MaxLength="15"></asp:TextBox>
            </td>
             <td class="style2">
                1代代数奖比例&nbsp;<asp:TextBox Id = "txtPara20" runat="server" MaxLength="15"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            
            <td class="style1">
                B币每日涨幅&nbsp;<asp:TextBox Id = "txtPara16" runat="server" MaxLength="15"></asp:TextBox>
            </td>
              <td class="style2">
              2代代数奖比例&nbsp;<asp:TextBox Id = "txtPara21" runat="server" MaxLength="15"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
             <td class="style1">
                 C币每日涨幅&nbsp;<asp:TextBox Id = "txtPara17" runat="server" MaxLength="7"></asp:TextBox>
            </td>
          
             <td class="style1">
                3代代数奖比例&nbsp;<asp:TextBox Id = "txtPara22" runat="server" MaxLength="15"></asp:TextBox>
            </td>
           
          
            <td>&nbsp;</td>
        </tr>
        <tr> 
            <td class="style1">
                D币每日涨幅&nbsp;<asp:TextBox Id = "txtPara18" runat="server" MaxLength="7"></asp:TextBox>
            </td>
          
            <td class="style2">
                4代代数奖比例&nbsp;<asp:TextBox Id = "txtPara23" runat="server" MaxLength="15"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                 E币每日涨幅&nbsp;<asp:TextBox Id = "txtPara19" runat="server" MaxLength="7"></asp:TextBox>
            </td>
            <td class="style2">
                5代代数奖比例&nbsp;<asp:TextBox Id = "txtPara24" runat="server" MaxLength="7"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>

        <tr> <td class="style1">
                小区比例&nbsp;<asp:TextBox Id = "txtPara26" runat="server" MaxLength="7"></asp:TextBox>
            </td>
            <td class="style2">
                 6代代数奖比例&nbsp;<asp:TextBox Id = "txtPara25" runat="server" MaxLength="7"></asp:TextBox>
            </td>
           
            <td>&nbsp;</td>
        </tr>

        <tr>
            <td class="style1">
                代数奖层级&nbsp;<asp:TextBox Id = "txtPara27" runat="server" MaxLength="7"></asp:TextBox>
            </td>
            <td class="style2">
                 
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
    </form>
</body>
</html>