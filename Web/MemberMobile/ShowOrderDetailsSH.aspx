<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowOrderDetailsSH.aspx.cs"
    Inherits="Member_ShowOrderDetails" %>
<%@ Register Src="../UserControl/MemberPager.ascx" TagName="Pager" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/MemberTop.ascx" TagName="top" TagPrefix="uc1"  %>
<%@ Register Src="~/UserControl/MemberBottom.ascx" TagName="bottom" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<meta http-equiv="x-ua-compatible" content="ie=11" />
<head id="Head1" runat="server">
      <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
    <script src="js/jquery-1.7.1.min.js"></script>
 
    <title>开户浏览</title>
    <link href="CSS/detail.css" rel="stylesheet" type="text/css" />
       <link rel="stylesheet" href="CSS/style.css">
    <script src="../JS/QCDS2010.js" type="text/javascript"></script>
       <script src="../javascript/My97DatePicker/WdatePicker.js"></script>

    <script type="text/javascript">
                           
    function getArgs(parm) //parm: 地址栏上参数名称
    {
      var pars=location.search;//获取当前url
      var pos=pars.indexOf('?');//查找第一个?
      pars=pars.substring(pos+1);//获取参数部分
      var ps=pars.split("&");
      var temp;
      var name,value,index;
      for(var i=0;i<ps.length;i++)
      {
        temp=ps[i]; 
        index=temp.indexOf("=");
        if(index==-1) continue;//如果参数中未包含=则继续
        name=temp.substring(0,index);//参数名称
        if(name==parm)
        {
            value=temp.substring(index+1);//参数的值
            document.location.href=value+".aspx";
        }     
      }
    }
    </script>
    
    <script type="text/javascript">
        $(function () {
            a.dianji();
        })
        var a = {
            dianji: function () {
                $("#ucPagerMb1").css('display', 'none');
            },
        }

    </script>
    <style>
        body {
            padding: 50px 2% 60px;
        }

        .xs_footer li a {
            display: block;
            padding-top: 40px;
            background: url(images/img/shouy1.png) no-repeat center 8px;
            background-size: 32px;
        }

        .xs_footer li .a_cur {
            color: #77c225;
        }

        .xs_footer li:nth-of-type(2) a {
            background: url(images/img/jiangj1.png) no-repeat center 10px;
            background-size: 32px;
        }

        .xs_footer li:nth-of-type(3) a {
            background: url(images/img/xiaoxi1.png) no-repeat center 8px;
            background-size: 32px;
        }

        .xs_footer li:nth-of-type(4) a {
            background: url(images/img/anquan1.png) no-repeat center 8px;
            background-size: 27px;
        }
    </style>
</head>
<body>
    <form id="Form1" runat="server">
         <div class="t_top">
            <a class="backIcon" href="Receiving.aspx"></a>
          <%=GetTran("004025","收货确认") %>
           
        </div>
        <div class="middle">

            <div class="minMsg minMsg2" style="display: block">
                <div class="minMsgBox">
                    <div>
                        <ol>
                            <asp:Repeater ID="rep" runat="server">
                                <ItemTemplate>
                                    <li>
                                            <%=GetTran("000045", "期数")%>：<%#Eval("ExpectNum") %>
                                           <br />
                                            <br />
                                            <%=GetTran("000079", "订单号")%>：<%#Eval("storeorderid") %>
                                            <br />
                                            <br />
                                            <%=GetTran("000322", "金额")%>：<%# (Convert.ToDouble(Eval("TotalMoney"))*
                        ( AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()) ))
                        ).ToString("f2")  %> 
                                             <br />
                                            <br />
                                            <%=GetTran("000414", "积分")%>：<%#Eval("Totalpv") %>
                                              <br />
                                            <br />
                                            <%=GetTran("001429", "报单日期")%>：<%# DateTime.Parse(Eval("orderdatetime").ToString()).AddHours(8)  %>

                                            </li>
                                             </ItemTemplate>
                                </asp:Repeater>
                            </ol> 
                          <ol>
                            <asp:Repeater ID="rep2" runat="server">
                                <ItemTemplate>
                                    <li>
                       
                                        <div>
                                            <%=GetTran("000501", "产品名称")%>：<%#Eval("productname") %>
                                            <br />
                                            <br />
                                            <div style="float: left">
                                             <%=GetTran("000558", "产品编号")%>：<%#Eval("productcode") %>
                                            </div>
                                            <div style="float: right">
                                                <%=GetTran("000505", "数量")%>：<%#Eval("ProductQuantity") %>
                                            </div>
                                            <br />
                                                 <br />
                                            <div style="float: left">
                                                <%=GetTran("000503", "单价")%>：<lable style="color: #e06f00"><%=( AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()) ))==1?"$":"￥"%><%#  (Convert.ToDouble( Eval("UnitPrice"))*
                        ( AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()) ))
                        ).ToString("f2")  %></lable>
                                            </div>
                                            <div style="float: right">
                                                <%=GetTran("000414", "积分")%>：<lable style="color: #e06f00"><%# Eval("PV") %></lable>
                                            </div>
                                             <br />
                                        </div>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ol>
                    </div> 
                    </div> </div> 

        </div>
       <!-- #include file = "comcode.html" -->

        <!-- html内容-->
        <!--顶部导航  logo 站点地图-->
        <div class="MemberPage" style="display:none">
          <uc1:top runat="server" ID="top" />
            <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0" class="biaozzi">
                <tr>
                    <td height="48" align="center">
                        <font size="3"><b>
                            <%=GetTran("000884", "订单明细")%></b></font>
                    </td>
                </tr>
            </table>

                        <asp:GridView ID="gvorder" runat="server" Width="100%" CellPadding="1" CellSpacing="1"
                            AutoGenerateColumns="False" OnRowDataBound="gvorder_RowDataBound">
                            <HeaderStyle HorizontalAlign="Center" CssClass="ctConPgTab"></HeaderStyle>
                            <AlternatingRowStyle BackColor="#F1F4F8" />
                            <Columns>
                                <asp:BoundField DataField="ExpectNum" HeaderText="期数"></asp:BoundField>
                                <asp:BoundField DataField="docid" HeaderText="发货单号"></asp:BoundField>
                                <asp:BoundField Visible="False" DataField="client" HeaderText="会员编号"></asp:BoundField>
                                <asp:BoundField DataField="TotalMoney" ItemStyle-HorizontalAlign="Right" HeaderText="金额"
                                    DataFormatString="{0:n2}">
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Totalpv" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"
                                    HeaderText="积分">
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="报单日期">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# GetOrderDate(Eval("orderdatetime")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
              <%--                  <asp:TemplateField HeaderText="发货日期">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# GetOrderDate(Eval("ConsignmentDateTime")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
  
                            </Columns>
                            <EmptyDataTemplate>
                                <table cellspacing="0" cellpadding="0" width="100%" border="1">
                                    <tr class="ctConPgTab">
                                        <th>
                                            <%=GetTran("000045", "期数")%>
                                        </th>
                                        <th>
                                            <%=GetTran("000079", "订单号")%>
                                        </th>
                                        <th>
                                            <%=GetTran("000322", "金额")%>
                                        </th>
                                        <th>
                                            <%=GetTran("000414", "积分")%>
                                        </th>
                                        <th>
                                            <%=GetTran("001429", "报单日期")%>
                                        </th>
                              <%--          <th>
                                            <%=GetTran("000070", "确认店铺")%>
                                        </th>--%>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:GridView>
                            <br />
                             <br />
                              <br />
                        <asp:GridView ID="myDatGrid" runat="server" AutoGenerateColumns="False" AllowSorting="false"
                            Width="100%" CellPadding="1" CellSpacing="1"  OnRowDataBound="myDatGrid_RowDataBound">
                            <HeaderStyle HorizontalAlign="Center" CssClass="ctConPgTab"></HeaderStyle>
                            <Columns>
                                <asp:BoundField DataField="docid" ItemStyle-HorizontalAlign="Center" HeaderText="订单号">
                                </asp:BoundField>
                                 <asp:BoundField DataField="Productcode" HeaderText="产品编号" ItemStyle-HorizontalAlign="Center">
                                </asp:BoundField>
                                <asp:BoundField DataField="ProductName" HeaderText="产品名称" ItemStyle-HorizontalAlign="Center">
                                </asp:BoundField>

                                <asp:BoundField DataField="productQuantity" HeaderText="数量" ItemStyle-HorizontalAlign="Center">
                                </asp:BoundField>
                                <asp:BoundField DataField="unitPrice" HeaderText="单价" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right">
                                </asp:BoundField>
                                <asp:BoundField DataField="pv" HeaderText="积分" ItemStyle-HorizontalAlign="Right"
                                    DataFormatString="{0:f2}"></asp:BoundField>
                                <asp:BoundField DataField="producttotal" HeaderText="总金额" ItemStyle-HorizontalAlign="Right"
                                    DataFormatString="{0:f2}"></asp:BoundField>
                            </Columns>
                            <EmptyDataTemplate>
                                <table cellspacing="0" cellpadding="1" border="1" width="100%">
                                    <tr class="ctConPgTab">
                                        <th>
                                            <%=GetTran("000079", "订单号")%>
                                        </th>
                                         <th>
                                            <%=GetTran("000558", "产品编号")%>
                                        </th>
                                        <th>
                                            <%=GetTran("000501", "产品名称")%>
                                        </th>
       
                                        <th>
                                            <%=GetTran("000505", "数量")%>
                                        </th>
                                        <th>
                                            <%=GetTran("000503", "单价")%>
                                        </th>
                                        <th>
                                            <%=GetTran("000414", "积分")%>
                                        </th>
                                        <th>
                                            <%=GetTran("000041", "总金额")%>
                                        </th>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:GridView>


                        <!--  <a visible="false" onclick="getArgs('form')" style="cursor: hand">返回</a>>
                <a onclick="history.go(-1)" style="cursor: hand">返回</a -->
                <div>
                        <asp:Button ID="btnE" runat="server" Text="返 回" CssClass="anyes" OnClick="btnE_Click" /></div>
 <uc2:bottom runat="server" ID="bottom" />

        </div>

    </form>
</body>
</html>
