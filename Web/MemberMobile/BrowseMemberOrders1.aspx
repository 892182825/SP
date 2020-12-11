<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BrowseMemberOrders1.aspx.cs" Inherits="Member_BrowseMemberOrders" %>

<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager1" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/MemberTop.ascx" TagName="top" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/MemberBottom.ascx" TagName="bottom" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<meta http-equiv="x-ua-compatible" content="ie=11" />
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no">
<meta name="apple-mobile-web-app-capable" content="yes">
<meta name="apple-mobile-web-app-status-bar-style" content="black">
<meta name="format-detection" content="telephone=no">

<head id="Head1" runat="server">
    <title></title>
    <%-- 新模板引用--%>   
    <script src="js/jquery-1.4.3.min.js" type="text/javascript"></script>
    <script src="js/bootstrap.js" type="text/javascript" charset="utf-8"></script>
    <script src="js/serviceOrganiz.js" type="text/javascript" charset="utf-8"></script>
    <script src="js/jquery-migrate-1.1.0.min.js" type="text/javascript" charset="utf-8"></script>
    <script type="text/javascript" src="js/jquery.mCustomScrollbar.js"></script>
    <%-- 新模板引用--%>

  
    <script type="text/javascript" language="javascript" src="../js/SqlCheck.js"></script>

    <script src="js/jquery-1.7.1.min.js"></script>



  <link rel="stylesheet" href="css/style.css">
<style>

 

</style>

    <script type="text/javascript" language="javascript">
        function CheckText() {
            //防SQL注入
            filterSql();
        }

        var defaultcur;

        //    window.onload=function (){
        //       defaultcur=document.getElementById("ddlCurrency").value;
        //    }
        function Language() {
            var chaxun='<%=GetTran("002055", "查询条件")%>';

            }
      
    </script>
    <script type="text/javascript">
        var lang = $("#lang").text();
        if (lang!="L001") {

        }
    </script>
</head>



<body>
    <b id="lang" style="display:none"><%=Session["LanguageCode"] %></b>
    <form id="form1" runat="server">
       <div class="t_top">	
            	<a class="backIcon" href="javascript:history.go(-1)"></a>

                	注册浏览
            	
                
            </div>


            <div class="middle">

            <div class="minMsg minMsg2" style="display: block">
                <div class="minMsgBox">
                    <div>
                        <ol>
                            <asp:Repeater ID="rep" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <div>
                                            <%=GetTran("000024", "会员编号")%>：<%#Eval("Number") %>
                                            <br />
                                            <br />
                                            <%=GetTran("000025", "会员姓名")%>：<%#Eval("Name") %>  
                                            <br />
                                            <br />
                                            <%=GetTran("000030", "所属服务机构")%>：<%#Eval("StoreID") %>
                                            <br />
                                            <br />
                                            <%=GetTran("000774", "报单途径")%>：<%# Common.GetMemberOrderType(Eval("ordertype").ToString())%>
                                            <br />
                                            <br />
                                            <%=GetTran("000775", "支付状态")%>： <%# GetPayStatus(DataBinder.Eval(Container.DataItem, "defraystate").ToString())%>
                                            <br />
                                            <br />
                                            <%=GetTran("000186", "支付方式")%>： <%# GetOrderType(DataBinder.Eval(Container.DataItem, "defrayType"))%>
                                            <br />
                                            <br />
                                            <%=GetTran("000045", "期数")%>：<%#Eval("OrderExpectNum") %>
                                            <br />
                                            <br />
                                            <%=GetTran("000780", "审核期数")%>：<%#Eval("PayExpectNum") %>
                                            <br />
                                            <br />
                                            <%=GetTran("000079", "订单号")%>：<%#Eval("OrderID") %>
                                            <br />
                                            <br />
                                            <%=GetTran("000322", "金额")%>：
                                            <%# ( Convert.ToDouble(DataBinder.Eval(Container.DataItem, "TotalMoney"))*
                        ( AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()) ))
                        ).ToString("f2") 
   %>
                    <%--                        <%#Eval("TotalMoney").ToString("f2") %>--%>
                                            <br />
                                            <br />
                                            <%=GetTran("000057", "注册日期")%>：<%#GetRegisterDate( Eval("RegisterDate").ToString()) %>
                                        </div>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ol>
                        <div style="display:none">
                        <ol>
                             <asp:Repeater ID="rep_km1" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <div>
                                            <%=GetTran("000501", "产品名称")%>：
                                            <br />
                                            <br />
                                            <div style="float: left">
                                                <%=GetTran("000882", "产品型号")%>：
                                            </div>
                                            <div style="float: right">
                                                <%=GetTran("000505", "数量")%>：
                                            </div>
                                            <br />
                                                 <br />
                                            <div style="float: left">
                                                <%=GetTran("000503", "单价")%>：<lable style="color: #e06f00">￥<%# Eval("Price", "{0:n2}") %></lable>
                                            </div>
                                            <div style="float: right">
                                                <%=GetTran("000414", "积分")%>：<lable style="color: #e06f00"><%# Eval("Price", "{0:n2}") %></lable>
                                            </div>
                                             <br />
                                        </div>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ol>
                 </div>
                    </div>
                </div>
            </div>
        </div>

     
      <!-- #include file = "comcode.html" -->

    </form>
  
</body>
</html>
