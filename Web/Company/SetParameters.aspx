<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetParameters.aspx.cs" Inherits="Company_SetParameters" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SetParameters</title>
    
    <link href="CSS/Company.css" type="text/css" rel="Stylesheet" />
    <script language="javascript" type="text/javascript" src="../javascript/ManagementVsExplanation_Part.js"></script>  
    <script language="javascript" type="text/javascript">
        window.onload=function()
	    {
	        down2();
	    };
    </script>
    <style type="text/css">
        #table1 {border:1px solid #000; border-bottom:none;}
        #table1 th{ border-bottom:1px solid #000;}
        #table1 td{ border-bottom:1px solid #000;}

        #tableP {border:1px solid #000; border-bottom:none;}
        #tableP th{ border-bottom:1px solid #000;}
        #tableP td{ border-bottom:1px solid #000;}

    </style>
    
</head>
<body>
    <form id="form1" runat="server">
    <br />        
        <table border="0" cellpadding="0" cellspacing="0" width="350px" class="tablemb" id="table1" runat="server" style="margin:50px 0px 0px 400px;">
            <tr>
                <th>
                    <%=GetTran("007920", "会员设置")%>
                </th>
            </tr>

            <tr  style="display:none"><td><asp:HyperLink ID="hlMemBaseLine" NavigateUrl="~/Company/SetParams/MemBaseLine.aspx" runat="server" Enabled="false">会员报单底线</asp:HyperLink>
                </td>
            </tr>

            <tr><td><asp:HyperLink ID="hlMemberBank" NavigateUrl="~/Company/SetParams/MemberBank.aspx" runat="server" Enabled="false">会员可用银行</asp:HyperLink>
                </td>
            </tr>

            <tr style="display:none"><td><asp:HyperLink ID="levelIcon" NavigateUrl="LevelIconsetting.aspx" runat="server" Enabled="false">级别图标设置</asp:HyperLink>
                                     </td>
            </tr>
            <tr><td><a href="SetAgreement.aspx"><%=GetTran("006979", "设置会员注册协议内容")%></a></td></tr>
            <tr style="display:none"><td><a href="SetAgreement2.aspx"><%=GetTran("007921","设置退换货协议内容")%></a></td></tr>
            <tr style="display:none"><td><a href="SetParams/Dlssetnow.aspx"><%=GetTran("000000", "代理商设定")%></a></td></tr>
            <tr style="display:none"><td><a href="Dlssetnow.aspx"><%=GetTran("000000", "业绩比例审核")%></a></td></tr>
            
            <tr style="display:none"><th><%=GetTran("007922", "财务设置")%></th></tr>
           <%-- <tr><td><a href="SetParams/ConfigAlipay.aspx"><%=GetTran("007050", "支付宝设置")%></a></td></tr>
            <tr><td><asp:HyperLink ID="hlCertificateManage" NavigateUrl="~/Company/certificateManage.aspx" runat="server" Enabled="false">快钱证书管理</asp:HyperLink></td></tr> 
            <tr><td><a href="SetParams/ConfigsftPay.aspx"><%=GetTran("007923", "盛付通网关商户号设置")%></a></td></tr>
            <tr><td><a href="SetParams/HuanxunConfigPay.aspx"><%=GetTran("007924","环迅网关商户号设置")%></a></td></tr>
            <tr><td><asp:HyperLink ID="hlGetWaySet" NavigateUrl="~/Company/SetParams/ConfigQuickPay.aspx" runat="server" Enabled="false">快钱网关设置</asp:HyperLink></td></tr>--%>
            <tr style="display:none"><td><a href="SetJjTx.aspx"><%=GetTran("007087","奖金提现设置") %></a></td></tr>
            <tr style="display:none"><td><a href="WithdrawShezhi.aspx"><%=GetTran("008145","会员提现设置") %></a></td></tr>
             <tr style="display:none"><td><a href="JLCanshu.aspx"><%=GetTran("009065","金流参数设置") %></a></td></tr>
            
            <tr><th><%=GetTran("007925", "高级设置")%></th></tr>
            <tr><td><asp:HyperLink ID="hlProvinceCity" NavigateUrl="~/Company/SetParams/ProvinceCity.aspx" runat="server" Enabled="false">省份城市</asp:HyperLink></td></tr>
            <tr><td><asp:HyperLink ID="hlDocType" NavigateUrl="~/Company/SetParams/DocType.aspx" runat="server" Enabled="false">单据类型</asp:HyperLink></td></tr>
            <tr><td><asp:HyperLink ID="hlWareHouseDepotSeat" NavigateUrl="~/Company/SetParams/WareHouseDepotSeat.aspx" runat="server" Enabled="false">仓库库位设置</asp:HyperLink></td></tr>
            <tr style="display:none"><td><a href="SetParams/SetPassResetMail.aspx"><%=GetTran("007088", "密码重置邮箱设置")%></a></td></tr>
            <tr style="display:none"><td><a href="SetNetTree.aspx"><%=GetTran("006999", "店铺和会员网络图查看设置")%></a></td></tr>

            <tr><th>交易设置</th></tr>
            <tr><td>
                <asp:HyperLink ID="hlExchangeTime" NavigateUrl="~/Company/SetParams/ExchangeTime.aspx" runat="server">交易时间</asp:HyperLink>
             </td></tr>
            <tr>
                <td>
                     <asp:HyperLink ID="hlHolidays" NavigateUrl="~/Company/SetParams/futoushezhi.aspx" runat="server">复投设置</asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td>
                     <asp:HyperLink ID="HyperLink1" NavigateUrl="~/Company/SetParams/shangjigai.aspx" runat="server">会员上级修改</asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td>
                     <asp:HyperLink ID="HyperLink2" NavigateUrl="~/Company/SetParams/jiesuo.aspx" runat="server">解锁</asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td>
                     <asp:HyperLink ID="HyperLink3" NavigateUrl="~/Company/SetParams/xiajimingdanxx.aspx" runat="server">下级业绩名单</asp:HyperLink>
                </td>
            </tr>
        </table>
        <table  border="0" cellpadding="0" cellspacing="0" width="350px" class="tablemb" id="tableP" runat="server" style="margin:50px 0px 0px 400px;">
             <tr><th><%=GetTran("007926","产品设置")%></th></tr>
            <tr><td><asp:HyperLink ID="hlProductColor" NavigateUrl="~/Company/SetParams/ProductColor.aspx" runat="server" Enabled="false">产品颜色</asp:HyperLink></td></tr>
            <tr style="display:none;"><td><asp:HyperLink ID="hlProductType"  NavigateUrl="~/Company/SetParams/ProductType.aspx" runat="server" Enabled="false">产品型号</asp:HyperLink></td></tr>
            <tr><td><asp:HyperLink ID="hlProductSize" NavigateUrl="~/Company/SetParams/ProductSize.aspx" runat="server" Enabled="false">产品尺寸</asp:HyperLink></td></tr>
            <tr><td><asp:HyperLink ID="hlProductSpec" NavigateUrl="~/Company/SetParams/ProductSpec.aspx" runat="server" Enabled="false">产品规格</asp:HyperLink></td></tr>
            <tr><td><asp:HyperLink ID="hlProductStatus" NavigateUrl="~/Company/SetParams/ProductStatus.aspx" runat="server" Enabled="false">产品状态</asp:HyperLink></td></tr>
            <tr><td><asp:HyperLink ID="hlProductUnit" NavigateUrl="~/Company/SetParams/ProductUnit.aspx" runat="server" Enabled="false">产品单位</asp:HyperLink></td></tr>
             <tr><td style="white-space:nowrap"><asp:HyperLink ID="hlProductSexType" NavigateUrl="~/Company/SetParams/ProductSexType.aspx" runat="server" Enabled="false">产品适用人群</asp:HyperLink></td></tr>
        </table>
        <div id="cssrain">
            <table width="100%" border="0" cellpadding="0" cellspacing="0" background="../images/DMdp.gif">
                <tr>
                    <td width="80">
                        <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="Table1">
                            <tr>
                                <td class="sec2">
                                    <span id="sp" title='<%=GetTran("000033")%>'><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033"))%></span>
                                    
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td><img src="../images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX" onclick="down2()"/></td>
                </tr>
            </table>
            <div id="divTab2">
                <table width="100%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
                    <tbody style="DISPLAY: block">
                        <tr>
                            <td valign="bottom" style="padding-left:20px">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                      <td>
                                      </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                    <tbody >
                        <tr>
                            <td style="padding-left:20px">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td><%=GetTran("006871", "设置系统中使用的各种基础数据。")%> </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    <br />
    <br />
    <br />
    </form>
</body>
</html>
