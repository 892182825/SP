<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowRegStore.aspx.cs" Inherits="Member_ShowRegStore" %>

<%@ Register Src="../UserControl/Language.ascx" TagName="Language" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/CountryCity.ascx" TagName="CountryCity" TagPrefix="uc2" %>
<%@ Register Src="../UserControl/CountryCityPCode.ascx" TagName="CountryCityPCode" TagPrefix="uc3" %>
<%@ Register Src="~/UserControl/MemberTop.ascx" TagName="top" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/MemberBottom.ascx" TagName="bottom" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="css/detail.css" rel="stylesheet" type="text/css" />
    <link href="css/cash.css" rel="stylesheet" type="text/css" />
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no">
<meta name="apple-mobile-web-app-capable" content="yes">
<meta name="apple-mobile-web-app-status-bar-style" content="black">
<meta name="format-detection" content="telephone=no">
<script src="js/jquery-1.7.1.min.js"></script>

  <title><%=GetTran("006614", "店铺注册")%></title>
<link rel="stylesheet" href="css/style.css">
<style>

.xs_footer li a{display:block;padding-top:40px;background:url(img/shouy1.png) no-repeat center 8px;background-size:32px;}
.xs_footer li .a_cur{color:#77c225}
.xs_footer li:nth-of-type(2) a{background:url(img/jiangj.png) no-repeat center 10px;background-size:32px;}
.xs_footer li:nth-of-type(3) a{background:url(img/xiaoxi1.png) no-repeat center 8px;background-size:32px;}
.xs_footer li:nth-of-type(4) a{background:url(img/anquan1.png) no-repeat center 8px;background-size:27px;}
.minMsgBox ol li span {
    float: none;
    /* margin-right: 5px; */
    background: none; 
     padding: 0px ; 
     border-radius: 0px; 
    color: #333; 
    font-size: 14px; 
    display: block;
    margin: 5px 0 10px;
    border-bottom: 1px solid #ccc;
    padding-bottom: 5px;
    padding-left: 27px;
}
</style>
  

<script type="text/javascript" language="javascript">
        <!--
        function setTab(name, cursel, n, clsname) {
            for (i = 1; i <= n; i++) {
                var menu = document.getElementById(name + i);
                var con = document.getElementById("con_" + name + "_" + i);
                menu.className = i == cursel ? clsname : "";
                con.style.display = i == cursel ? "block" : "none";
            }
        }
        //-->
</script>
    <script type="text/javascript">
        $(function () { 
        var lang = document.getElementById("lang").innerHTML;
        if (lang!="L001") {
            //alert("ShowRegStore");
        }
        })
    </script>
</head>
<body>
    <b style="display:none" id="lang"><%=Session["LanguageCode"] %></b>
    <form id="form1" runat="server">
            	<div class="t_top">	
                    <a class="backIcon" href="javascript:history.go(-1)"></a>
                	<%=GetTran("006614", "店铺注册")%>
            </div>
                 <div class="minMsg minMsg2" style="display: block">
                <div class="minMsgBox">
                    <div>
                        <ol>
                         
                                    <li>
                                        <div>
                                            <%=GetTran("000024", "会员编号")%>：  <asp:Label ID="lblNumber" runat="server"></asp:Label>
                                      
                                           <%=GetTran("000039", "店长姓名")%>： <asp:Label ID="lblName" runat="server"></asp:Label>
                                        
                                                <%=GetTran("000037", "店铺编号")%>： <asp:Label ID="lblStoreID" runat="server"></asp:Label>
                                        
                                       <%=GetTran("000040", "店铺名称")%>：<asp:Label ID="lblStoreName" runat="server"></asp:Label>
                                      
                                               <%=GetTran("000313", "店铺所在地")%>：<asp:Label ID="lblSCPCCODE" runat="server"></asp:Label>
                                          
                                        <%=GetTran("000073", "邮编")%> <asp:Label ID="lblPostalCode" runat="server"></asp:Label>
                                         
                                             <%=GetTran("007541", "推荐机构的会员编号")%>： <asp:Label ID="lblDirect" runat="server"></asp:Label>
                                        
                                       <%=GetTran("006983", "店铺审核状态")%>：  <asp:Label ID="lblShenhe" runat="server"></asp:Label>
                                         
                                        <%=GetTran("000316", "店长联系信息")%>：  <asp:Label ID="lblAddress" runat="server"></asp:Label>
                                           
                                       <%=GetTran("000069", "移动电话")%>  <asp:Label ID="lblMobileTele" runat="server"></asp:Label>
                                         
                                            <%=GetTran("000319", "负责人电话")%> <asp:Label ID="lblHomeTele" runat="server"></asp:Label>
                                        
                                          <%=GetTran("000087", "开户银行")%>： <asp:Label ID="lblBankAddress" runat="server"></asp:Label>
                                        
                                          <%=GetTran("007410", "分/支行名称")%>： <asp:Label ID="labBankBranchname" runat="server"></asp:Label>
                                        
                                         <%=GetTran("000329", "银行账户")%>  <asp:Label ID="lblBankCard" runat="server"></asp:Label>
                                         
                                           <%=GetTran("000330", "电子邮箱")%> <asp:Label ID="lblEmail" runat="server"></asp:Label>
                                            
                                              <%=GetTran("000332", "网址")%> ：  <asp:Label ID="lblNetAddress" runat="server"></asp:Label>
                                             
                                            <%=GetTran("000042", "办店期数")%>： <asp:Label ID="lblExpect" runat="server"></asp:Label>
                                            
                                               <%=GetTran("000078", "备注")%>  <asp:Label ID="lblRemark" runat="server"></asp:Label>
                                          
                                          <%-- <%=GetTran("000046", "级别")%>： <asp:Label ID="lblLevel" runat="server"></asp:Label>--%>
                                        
                                        </div>
                                    </li>
                             
                        </ol>
                
                    </div>
                </div>
            </div>
   <!-- #include file = "comcode.html" -->


     <!--顶部导航  logo 站点地图-->
     <div class="MemberPage" style="display:none">
        <uc1:top runat="server" ID="top" />
        <div class="centerCon" style="margin-left:-180px">
            <!--内容,右下背景-->
            <div class="centConPage">
                <!--表单-->
                <div class="ctConPgCash">
                    <div class="CashH2"><h1><span class="CashTitle" style="width:755px;"><%=GetTran("000460", "服务机构信息")%></span></h1><span class="CashTitleR"></span></div>
        <table  width="761" border="0" cellpadding="1" cellspacing="1">

            <tr>
                <th align="right" style="width:200px;">
                    <%=GetTran("000024", "会员编号")%>：
                </th>
                <td style="white-space: nowrap">
                  
                </td>
            </tr>
            <tr>
                <th align="right">
                    <%=GetTran("000039", "店长姓名")%>：
                </th>
                <td style="white-space: nowrap">
                   
                </td>
            </tr>
            <tr>
                <th align="right" style="white-space: nowrap" bgcolor="#EBF1F1">
                    <%=GetTran("000037", "店铺编号")%>：
                </th>
                <td style="white-space: nowrap">
                   
                </td>
            </tr>
            <tr>
                <th align="right" style="white-space: nowrap" bgcolor="#EBF1F1">
                    <%=GetTran("000040", "店铺名称")%>：
                </th>
                <td style="white-space: nowrap">
                   
                </td>
            </tr>
            <tr>
                <th align="right" style="white-space: nowrap" bgcolor="#EBF1F1">
                    <%=GetTran("000313", "店铺所在地")%>：
                </th>
                <td style="white-space: nowrap">
              
                </td>
            </tr>
            <tr>
                <th align="right" style="white-space: nowrap" bgcolor="#EBF1F1">
                    <%=GetTran("000073", "邮编")%>
                    ：
                </th>
                <td style="white-space: nowrap">
                  
                </td>
            </tr>
            <tr>
                <th align="right" style="white-space: nowrap" bgcolor="#EBF1F1">
                    <%=GetTran("007541", "推荐机构的会员编号")%>：
                </th>
                <td style="white-space: nowrap;">
                
                </td>
            </tr>
            <tr style="display:none;">
                <th align="right" style="white-space: nowrap" bgcolor="#EBF1F1">
                    <%=GetTran("007520", "店铺激活状态")%>：
                </th>
                <td style="white-space: nowrap">
                       <%=GetTran("007520", "店铺激活状态")%>： <asp:Label ID="lblIsJihuo" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th align="right" style="white-space: nowrap" bgcolor="#EBF1F1">
                    <%=GetTran("006983", "店铺审核状态")%>：
                </th>
                <td style="white-space: nowrap">
              
                </td>
            </tr>
            <tr>
                <th align="right" style="white-space: nowrap" bgcolor="#EBF1F1">
                    <%=GetTran("000316", "店长联系信息")%>：
                </th>
                <td style="white-space: nowrap">
                
                </td>
            </tr>
            <tr style="display: none">
                <th align="right" style="white-space: nowrap" bgcolor="#EBF1F1">
                    币种：
                </th>
                <td style="white-space: nowrap" id="td1">
                    币种：  <asp:DropDownList ID="Currency" runat="server">  </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th align="right" style="white-space: nowrap" bgcolor="#EBF1F1">
                    <%=GetTran("000069", "移动电话")%>
                    ：
                </th>
                <td style="white-space: nowrap">
               
                </td>
            </tr>
            <tr>
                <th align="right" style="white-space: nowrap" bgcolor="#EBF1F1">
                    <%=GetTran("000319", "负责人电话")%>
                    ：
                </th>
                <td style="white-space: nowrap">
                
                </td>
            </tr>
            <tr>
                <th align="right" style="white-space: nowrap" bgcolor="#EBF1F1">
                    <%=GetTran("000087", "开户银行")%>：
                </th>
                <td style="white-space: nowrap">
                  
                </td>
            </tr>
            <tr>
                <th align="right" style="white-space: nowrap" bgcolor="#EBF1F1">
                    <%=GetTran("007410", "分/支行名称")%>：
                </th>
                <td style="white-space: nowrap">
                
                </td>
            </tr>
            <tr>
                <th align="right" style="white-space: nowrap" bgcolor="#EBF1F1">
                    <%=GetTran("000329", "银行账户")%>
                    ：
                </th>
                <td style="white-space: nowrap">
                
                </td>
            </tr>
            <tr>
                <th align="right" style="white-space: nowrap" bgcolor="#EBF1F1">
                    <%=GetTran("000330", "电子邮箱")%>
                    ：
                </th>
                <td style="white-space: nowrap">
                
                </td>
            </tr>
            <tr>
                <th align="right" style="white-space: nowrap" bgcolor="#EBF1F1">
                    <%=GetTran("000332", "网址")%> ：
                </th>
                <td style="white-space: nowrap">
              
                </td>
            </tr>
            <tr>
                <th align="right" bgcolor="#EBF1F1">
                    <p align="right">
                        <%=GetTran("000042", "办店期数")%>：</p>
                </th>
                <td>
               
                </td>
            </tr>
            <tr>
                <th align="right" style="white-space: nowrap" bgcolor="#EBF1F1">
                    <%=GetTran("000078", "备注")%>
                    ：
                </th>
                <td style="white-space: nowrap">
             
                </td>
            </tr>
          <%--  <tr>
                <th align="right" style="white-space: nowrap" bgcolor="#EBF1F1">
                    <%=GetTran("000046", "级别")%>：
                </th>
                <td style="white-space: nowrap">
                  
                </td>
            </tr>--%>
            
            <tr style="display:none;">
                <th align="right" style="white-space: nowrap" bgcolor="#EBF1F1">
                    <%=GetTran("000341", "经营面积(平方米)")%>
                    ：
                </th>
                <td style="white-space: nowrap">
                     <%=GetTran("000341", "经营面积(平方米)")%>  <asp:Label ID="lblFareArea" runat="server"></asp:Label>
                </td>
            </tr>
            <tr style="display:none;">
                <th align="right" style="white-space: nowrap" bgcolor="#EBF1F1">
                    <%=GetTran("000343", "投资总额(万元)")%>
                    ：
                </th>
                <td style="white-space: nowrap">
                    <%=GetTran("000343", "投资总额(万元)")%> <asp:Label ID="lblTotalAccountMoney" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table  width="100" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td style="white-space: nowrap" align="center">

                        <input type="button" id="GoStore" value="进入专卖店" onclick="javascript:window.location.href='GoStore.aspx';" style="display:none;" />
                </td>
            </tr>
        </table>
    </div></div>
    <uc2:bottom runat="server" ID="bottom" />
        <!--页面内容结束-->
    </div>
    </div>
    </form>
    <%=msg %>
</body>
</html>