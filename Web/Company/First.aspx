<%@ Page Language="C#" AutoEventWireup="true" CodeFile="First.aspx.cs" Inherits="Company_Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%=GetTran("000509", "公司首页")%></title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery.min.js"></script>
    <style type="text/css">
        a:link
        {
            color: #075C79;
            text-decoration: none;
        }
        a:visited
        {
            text-decoration: none;
            color: #336666;
        }
        a:hover
        {
            text-decoration: none;
            color: #FF3300;
        }
        a:active
        {
            text-decoration: none;
        }


        .tc {
        width:300px;
        height:200px;
        position:absolute;
        right:2px;
        bottom:0;
        border:1px solid #ccc;
       
        display:none;
        background:#f8f8f8
    }
        .tc div {
            height:30px;
            background:#278cdf
        }
        .tc span {
            position:absolute;
            right:5px;
            top:0px;
            font-size:26px;
            color:#fff;
            cursor:pointer
        }
        .tc p {
            padding:0 10px;
            font-size:16px;
        }
    html, body {
        height:100%
    }

        .ulinfo
        { width:800px; margin:auto;  font-size:12px;
        }
            .ulinfo li
            { width:300px;margin-right:80px; text-align:left; padding:10px; float:left; list-style:none;
          height:60px;   }
        .tbss td {
        width:24%;
        text-align:right;
        padding:5px;
        }
    </style>
    <script language=javascript type="text/javascript">
        var i=0;
        
        function getFirst()
        {
            if(i==0)
            {
                i=1;
                window.location.href = window.location.href;
            }
            alert(i);
        }

    </script>
</head>
<body >
    

    <form id="form1" runat="server" style="height:100%">
	
		<br />
		<br />
		<br />
		<%--<a href="ShowView.aspx" visible="false">网络图</a>--%>
        <table class="tbss" width="80%"  align="center" border="1" cellpadding="1" cellspacing="1" style="border-color: #88e0f4;border-collapse: collapse;">
                    <tr>
                       
                    <tr>
                        <td  align="center">
                           积分注册发行量
                        </td>
                        <td style="white-space:nowrap">
                           <asp:Label ID="shyfx" Runat="server" Text="0.00"></asp:Label>
                        </td>
                        <td  align="center">
                           积分奖励发行量
                        </td>
                        <td style="white-space:nowrap">
                           <asp:Label ID="jjfx" Runat="server" Text="0.00"></asp:Label>
                        </td>
                        
                    </tr>
             <tr>
                        
                        <td  align="center">
                         <br />
                        </td>
                        <td style="white-space:nowrap">
                          
                        </td>
                    </tr>
            <tr>
                        <td  align="center">
                           
                        </td>
                        <td style="white-space:nowrap">
                           
                        </td>
                        <td  align="center">
                         <br />
                        </td>
                        <td style="white-space:nowrap">
                          
                        </td>
                    </tr>
                     <tr>
                        <td align="center">
                           注册总人数
                        </td>
                        <td style="white-space:nowrap">
                           <asp:Label ID="zcrs" Runat="server" Text="0.00"></asp:Label>
                        </td>
                         <td  align="center">
                           今日注册总人数
                        </td>
                        <td style="white-space:nowrap">
                           <asp:Label ID="jrrs" Runat="server" Text="0.00"></asp:Label>
                        </td>
                        
                    </tr>
                    <tr>
                       <td align="center">
                           注册总业绩
                        </td>
                        <td style="white-space:nowrap">
                           <asp:Label ID="zcyj" Runat="server" Text="0.00"></asp:Label>
                        </td>
                        <td  align="center">
                           今日注册总业绩
                        </td>
                        <td style="white-space:nowrap">
                           <asp:Label ID="jryj" Runat="server" Text="0.00"></asp:Label>
                        </td>
                    </tr>
            <tr>
                        <td  align="center">
                           业绩奖总计
                        </td>
                        <td style="white-space:nowrap">
                           <asp:Label ID="myjzjb" Runat="server" Text="0.00"></asp:Label>
                        </td>
                        <td  align="center">
                           
                        </td>
                        <td style="white-space:nowrap">
                           
                        </td>
                        
                    </tr>
            <tr>
                        <td  align="center">
                           
                        </td>
                        <td style="white-space:nowrap">
                           
                        </td>
                        <td  align="center">
                         <br />
                        </td>
                        <td style="white-space:nowrap">
                          
                        </td>
                    </tr>
                    <tr>
                        <td  align="center">
                           静态释放总计
                        </td>
                        <td style="white-space:nowrap">
                           <asp:Label ID="jtsf" Runat="server" Text="0.00"></asp:Label>
                        </td>
                        <td  align="center">
                           动态释放总计
                        </td>
                        <td style="white-space:nowrap">
                           <asp:Label ID="dtsf" Runat="server" Text="0.00"></asp:Label>
                        </td>
                    </tr>
            <tr>
                        <td  align="center">
                           昨日静态释放总计
                        </td>
                        <td style="white-space:nowrap">
                           <asp:Label ID="zrjt" Runat="server" Text="0.00"></asp:Label>
                        </td>
                        <td align="center">
                           昨日动态释放总计
                        </td>
                        <td style="white-space:nowrap">
                           <asp:Label ID="zrdt" Runat="server" Text="0.00"></asp:Label>
                        </td>
                        
                    </tr>
            
                  <tr>
                      <td  align="center">
                           积分释放总计
                        </td>
                        <td style="white-space:nowrap">
                           <asp:Label ID="jfzsf" Runat="server" Text="0.00"></asp:Label>
                        </td>
                        <td  align="center">
                           昨日积分释放总计
                        </td>
                        <td style="white-space:nowrap">
                           <asp:Label ID="zrzsf" Runat="server" Text="0.00"></asp:Label>
                        </td>
                    </tr> 
                    
            <tr>
                        <td  align="center">
                           
                        </td>
                        <td style="white-space:nowrap">
                           
                        </td>
                        <td  align="center">
                         <br />
                        </td>
                        <td style="white-space:nowrap">
                          
                        </td>
                    </tr>
            
            
            

                </table>

    <ul width="80%" border="0" align="center" cellpadding="0" cellspacing="16" class="ulinfo">
     
               



            <li  id="ligogo" runat="server" >
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="68" rowspan="2" align="center">
                            <img src="images/midnu01.gif" width="38" height="34" />
                        </td>
                        <td style="white-space:nowrap">
                            <%=GetTran("000101", "当日发布公告")%>
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space:nowrap">
                           <asp:Label ID="labgonggao" Runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </li>
            <li   id="liwydyj" runat="server" visible="false" >
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="68" rowspan="2" align="center">
                            <img src="images/midnu06.gif" width="36" height="32" />
                        </td>
                        <td style="white-space:nowrap">
                            <%=GetTran("000117", "未阅读的邮件")%>
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space:nowrap">
                        <asp:Label ID="labmail" Runat="server"></asp:Label>
                            <!--当前有 <span class="midhong"><a href="#">13</a></span> 封新邮件！-->
                        </td>
                    </tr>
                </table>
            </li>
        
            <li id="litodayreg" runat="server" visible="false" >
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="68" rowspan="2" align="center">
                            <img src="images/midnu02.gif" width="36" height="49" />
                        </td>
                        <td style="white-space:nowrap">
                            <%=GetTran("000122", "今日注册的会员")%>
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space:nowrap">
                        <asp:Label ID="labmemberregister" Runat="server"></asp:Label>
                            <!--今日有 <span class="midhong"><a href="#">1</a></span> 个新注册会员！-->
                        </td>
                    </tr>
                </table>
            </li>
            <li id="listore" runat="server" style="display:none" visible="false">
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="68" rowspan="2" align="center">
                            <img src="images/midnu07.gif" width="43" height="39" />
                        </td>
                        <td style="white-space:nowrap">
                            <%=GetTran("000516", "未审核的店铺")%>
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space:nowrap">
                        <asp:Label ID="labstoreaudting" Runat="server"></asp:Label>
                           <!-- 当前有 <span class="midhong"><a href="#">1</a></span> 家需要审核的店铺！-->
                        </td>
                    </tr>
                </table>
            </li>
       
            <li id="liwshhk" runat="server" visible="false" >
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="68" rowspan="2" align="center">
                            <img src="images/midnu03.gif" width="49" height="33" />
                        </td>
                        <td style="white-space:nowrap">
                            <%=GetTran("007588", "未审核汇款")%>
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space:nowrap">
                        <asp:Label ID="labstoremoney" Runat="server"></asp:Label>
                           <!-- 当前有 <span class="midhong"><a href="#">12</a></span> 笔需要审核的店铺汇款！-->
                        </td>
                    </tr>
                </table>
            </li>
            <li  id="lijfd" runat="server" visible="false" >
                     <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="68" rowspan="2" align="center">
                            <img src="images/midnu03.gif" width="49" height="33" />
                        </td>
                        <td style="white-space:nowrap">
                            <%=GetTran("008240", "未处理的纠纷单")%>
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space:nowrap">
                        <asp:Label ID="labdispute" Runat="server"></asp:Label>
                           <!-- 当前有 <span class="midhong"><a href="#">12</a></span> 笔需要审核的店铺汇款！-->
                        </td>
                    </tr>
                </table>
            </li>
            <li id="listoreback" runat="server" style="display:none" visible="false" >
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="68" rowspan="2" align="center">
                            <img src="images/midnu08.gif" width="54" height="37" />
                        </td>
                        <td style="white-space:nowrap">
                            <%=GetTran("000523", "未审核店铺退货")%>
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space:nowrap">
                            <asp:Label ID="labmembermoney" Runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </li>
       
            <li id="linopayorder" runat="server" style="display:none" visible="false" >
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="68" rowspan="2" align="center">
                            <img src="images/midnu04.gif" width="42" height="25" />
                        </td>
                        <td style="white-space:nowrap">
                            <%=GetTran("000524", "店铺未支付订单")%>
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space:nowrap">
                        <asp:Label ID="labstore" Runat="server"></asp:Label>
                            <!--当前有 <span class="midhong"><a href="#">8</a></span> 笔店铺未支付的订单！-->
                        </td>
                    </tr>
                </table>
            </li>
            <li id="linosendorder" runat="server" visible="false" >
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="68" rowspan="2" align="center">
                            <img src="images/midnu09.gif" width="47" height="34" />
                        </td>
                        <td style="white-space:nowrap">
                             <%=GetTran("000525", "公司未发货订单")%>
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space:nowrap">
                        <asp:Label ID="labordersent" Runat="server"></asp:Label>
                            <!--当前有 <span class="midhong"><a href="#">71</a></span> 条店铺未发货订单！-->
                        </td>
                    </tr>
                </table>
            </li>
        
            <li id="liwshorder" runat="server" style="display:none" visible="false" >
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="68" rowspan="2" align="center">
                            <img src="images/midnu05.gif" width="38" height="31" />
                        </td>
                        <td style="white-space:nowrap">
                             <%=GetTran("000528", "未审核会员报单")%>
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space:nowrap">
                        <asp:Label ID="labmemberaudting" Runat="server"></asp:Label>
                           <!-- 今日有 <span class="midhong"><a href="#">55</a></span> 笔需要审核的会员报单！-->
                        </td>
                    </tr>
                </table>
            </li>
            <li id="licpyujing" runat="server" visible="false" >
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="68" rowspan="2" align="center">
                            <img src="images/midnu10.gif" width="38" height="32" />
                        </td>
                        <td style="white-space:nowrap">
                             <%=GetTran("000530", "产品预警")%>
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space:nowrap">
                        <asp:Label ID="labproduct" Runat="server"></asp:Label>
                            <!--当前有 <span class="midhong"><a href="#">4</a></span> 个预警的产品！-->
                        </td>
                    </tr>
                </table>
            </li>
       
            <li id="litxsq" runat="server"  style="display:none" visible="false">
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="68" rowspan="2" align="center">
                            <img src="images/midnu05.gif" width="38" height="31" />
                        </td>
                        <td style="white-space:nowrap">
                             <%=GetTran("007590", "未审核提现申请")%>
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space:nowrap">
                        <asp:Label ID="Label1" Runat="server"></asp:Label>
                           <!-- 今日有 <span class="midhong"><a href="#">55</a></span> 笔需要审核的会员报单！-->
                        </td>
                    </tr>
                </table>
            </li>
             <li id="lifwjgyhd" runat="server" visible="false" >
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="68" rowspan="2" align="center">
                            <img src="images/midnu05.gif" width="38" height="31" />
                        </td>
                        <td style="white-space:nowrap">
                             <%=GetTran("000000", "服务机构要货订单")%>
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space:nowrap">
                        <asp:Label ID="Label2" Runat="server"></asp:Label>
                           <!-- 今日有 <span class="midhong"><a href="#">55</a></span> 笔需要审核的会员报单！-->
                        </td>
                    </tr>
                </table>
            </li>
       
    </ul>
        <div class="tc"  >
            <div></div>
            
            <asp:Literal ID="Litw" runat="server"></asp:Literal>
                  <p><%--<span></span>您当前有<%# getdisCount() %>条纠纷单未处理--%></p>
            
            <span>×</span>
        </div>
    </form>
</body>
<%--<style>
    

</style>--%>
</html>
<script>
  <%--  $(function () {
        var it='<%=jfdtx%>';
        if(it>0){
            $('.tc').slideDown()
            $('.tc span').click(function () {
                $(this).parent().slideUp()
            });
       
        }

    })--%>

</script>
