<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Receiving.aspx.cs" Inherits="Member_Receiving" %>

<%@ Register Src="../MemberMobile/PageSj.ascx" TagName="PageSj" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/MemberPager.ascx" TagName="Pager" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/MemberTop.ascx" TagName="top" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/MemberBottom.ascx" TagName="bottom" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no">
<meta name="apple-mobile-web-app-capable" content="yes">
<meta name="apple-mobile-web-app-status-bar-style" content="black">
<meta name="format-detection" content="telephone=no">
<script src="js/jquery-1.7.1.min.js"></script>
    <title>收货确认</title>
    <link rel="stylesheet" href="css/style.css">
<style>

   .middle .searchFactor span select,.middle .searchFactor span input {
    margin-left:0px;
    }

</style>
    <link href="CSS/detail.css" rel="stylesheet" type="text/css" />

    <script src="../JS/QCDS2010.js" type="text/javascript"></script>

    <script src="../javascript/My97DatePicker/WdatePicker.js"></script>
    <link href="../Member/hycss/serviceOrganiz.css" rel="stylesheet" />
    <script>
        $(document).ready(function () {
            if ($.browser.msie && $.browser.version == 6) {
                FollowDiv.follow();
            }
        });
        FollowDiv = {
            follow: function () {
                $('#cssrain').css('position', 'absolute');
                $(window).scroll(function () {
                    var f_top = $(window).scrollTop() + $(window).height() - $("#cssrain").height() - parseFloat($("#cssrain").css("borderTopWidth")) - parseFloat($("#cssrain").css("borderBottomWidth"));
                    $('#cssrain').css('top', f_top);
                });
            }
        }
        function checkall(obj) {
            var list = document.getElementsByClassName("aa");
            for (var i = 0; i < list.length; i++) {
                if (list[i].type == "checkbox") {
                    list[i].checked = obj.checked;
                }
            }
        }

        function checkDate() {
            var bl = false;
            var list = document.getElementsByTagName("input");
            for (var i = 0; i < list.length; i++) {
                if (list[i].type == "checkbox" && list[i].id != "CheckBox1") {
                    bl = true;
                }
            }
            return bl;
        }
    </script>
    <script type="text/javascript">
        $(function () {
            var lang = $("#lang").text();
            if (lang != "L001") {
                $('.rq').width('100%')
                $('#rep_ctl00_Button1').css({ "width": "auto", "margin-left":"55%" })
            }
        })
        
    </script>
    <style>
    .minMsgBox ol li a {
        width:100%
    }
        .ctConPgList-2 ul li {
            width: 100%;
        }

</style>
</head>



<body>
    <b style="display:none" id="lang"><%=Session["LanguageCode"] %></b>
    <form id="form2" runat="server">
        <%--    <uc1:STop runat="server" ID="STop1" />


        <uc1:SLeft runat="server" ID="SLeft1" />--%>
  <%--      <uc1:top runat="server" ID="top" />--%>
        	<div class="t_top">	
            	<a class="backIcon" href="javascript:history.go(-1)"></a>

                	<%=GetTran("004025","收货确认") %>
            
                
            </div>
         <div class="middle">
                <div id="qq2" class="fiveSquareBox clearfix searchFactor">
                <span style="width: 50%;overflow:hidden;line-height:30px;float:left">
                    <span style="float:left"><%=GetTran("000838","查询条件")%>：</span>
                      <asp:DropDownList ID="ddlQiShu" CssClass="ctConPgFor" runat="server" Style="width: 50%;float:left;margin-top: 3%;">
                        </asp:DropDownList>
                </span>
                <span style="overflow: hidden">
                    <span style="float: left" class="rq"><%=GetTran("001429","报单日期") %>：</span>

                    <asp:TextBox ID="txtDate" Style="margin-top: 5px; width: 32%" runat="server" CssClass="Wdate" onfocus="WdatePicker()"></asp:TextBox>
                    <span style="float: left; width: 9%; text-align: center"><%=GetTran("000205","到") %></span>
                    <asp:TextBox ID="txtDateEnd" Style="margin-top: 5px; width: 32%" runat="server" CssClass="Wdate" onfocus="WdatePicker()"></asp:TextBox>
                </span>
                <span style="width: 100%;height:38px;line-height:30px;overflow:hidden">
                    <span style="float:left;width:100%;">
                      <asp:RadioButtonList ID="rdbtnType" Style="width: 100%;float:left;" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="-1" Selected="True">全部</asp:ListItem>
                            <asp:ListItem Value="1"> 已收货 </asp:ListItem>
                            <asp:ListItem Value="0"> 未收货</asp:ListItem>
                        </asp:RadioButtonList>
                     
                    </span>
                </span>
               <asp:Button ID="btnSearch" runat="server" Height="27px" Width="100%" Style="margin-top: 1px; margin-left: -1px; padding: 4px 9px; background-color: #96c742; color: #FFF; border: 1px solid #507E0C; background-image: linear-gradient(#addf58,#96c742); text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.4);"
                            Text="搜 索" OnClick="btnSearch_Click" CssClass="anyes" />
            </div>


            <div class="minMsg minMsg2" style="display:block">
                 
               <div class="minMsgBox">
                   <h3></h3>
                   <div style="margin-top: 160px;padding-bottom: 70px;">
                       <ol>
                           <asp:Repeater ID="rep" runat="server" OnItemCommand="rep_km_ItemCommand" OnItemDataBound="rep_km_ItemDataBound" >
                               <ItemTemplate>
                                   <li>
                                       <a href="ShowOrderDetailsSH.aspx?byy=<%# Eval("docid") %>">
                                           <div>
                                               <%# Eval("StoreOrderID")  %><label>
                                                   <%=( AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()) ))==1?"$":"￥"%><%# (Convert.ToDouble(  Eval("TotalMoney"))*
                        ( AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()) ))
                        ).ToString("f2")  %></label>
                                           </div>
                                           <p>
                                               <%# DateTime.Parse(Eval("orderdatetime").ToString()).AddHours(8)  %>
                                               <label style="color: #666">
                                                     <asp:CheckBox style="width: 20px;float:left;display:none" ID="CheckBox1"  class="aa" runat="server" Checked />
                                                   <asp:HiddenField ID="HF" Value='<%# Eval("DocID") %>' runat="server" />
                                                  <%-- <asp:Button ID="Button1" runat="server" Height="15px" Width="47px" Style="float: left; margin-top: 1px; margin-left: 8px; padding: 4px 9px; background-color: #96c742; color: #FFF; border: 1px solid #507E0C; background-image: linear-gradient(#addf58,#96c742); text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.4); box-sizing: content-box"
                                                       Text="确认收货" OnClick="Button1_Click" OnClientClick="return checkDate();" CssClass="anyes" />--%>
                                                      <asp:LinkButton ID="Button1" CommandName="qrsh" CommandArgument='<%# Eval("docid")%>'  Height="15px" Width="48px" Style="float: left; margin-top: -19px; margin-left: 80%; padding: 4px 9px; background-color: #96c742; color: #FFF; border: 1px solid #507E0C; background-image: linear-gradient(#addf58,#96c742); text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.4); box-sizing: content-box"
                                              runat="server" Text="确认收货" />
                                                   <asp:Label ID="ysh" runat="server" Visible="false" Text="已收货"></asp:Label>
                                               </label>
                                           </p>
                                       </a>
                                   </li>
                               </ItemTemplate>
                           </asp:Repeater>
                       </ol>
                       <uc2:bottom runat="server" ID="bottom" />
                       <uc1:PageSj ID="Pager1" runat="server" />
                   </div>
                </div>
    <!-- #include file = "comcode.html" -->

               
                <%--<div class="fiveSquareBox clearfix searchFactor">
                    <span><%=GetTran("000838","查询条件")%>：</span>
                    <span class="onePart">
                        <asp:DropDownList ID="ddlQiShu" CssClass="ctConPgFor" runat="server">
                        </asp:DropDownList>
                        <span><%=GetTran("001429","报单日期") %>：</span>
                        <asp:TextBox ID="txtDate" Style="margin-top: 5px;" runat="server" CssClass="Wdate" onfocus="WdatePicker()"></asp:TextBox>
                        <span> <%=GetTran("000205","到") %></span>

                        <asp:TextBox ID="txtDateEnd" Style="margin-top: 5px;" runat="server" CssClass="Wdate" onfocus="WdatePicker()"></asp:TextBox>

                        <asp:RadioButtonList ID="rdbtnType" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="-1">全部</asp:ListItem>
                            <asp:ListItem Value="1">已收货</asp:ListItem>
                            <asp:ListItem Value="0" Selected="True">未收货</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:Button ID="btnSearch" runat="server" Height="27px" Width="47px" Style="margin-top: 1px; margin-left: 8px; padding: 4px 9px; background-color: #96c742; color: #FFF; border: 1px solid #507E0C; background-image: linear-gradient(#addf58,#96c742); text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.4);"
                            Text="搜 索" OnClick="btnSearch_Click" CssClass="anyes" />
                    </span>


                </div>--%>

                <div class="noticeEmail width100per mglt0" style="display:none">
                    <div class="pcMobileCut">
                        <div class="noticeHead">
                            <div>
                                <i class="glyphicon glyphicon-file"></i>
                                <span><%=GetTran("005011", "报单浏览")%></span>
                            </div>
                        </div>
                        <div class="noticeBody">
                            <div class="tableWrap clearfix table-responsive">
                                <table class="table-bordered noticeTable">
                                    <tbody>
                                        <tr>
                                            <td style="padding: 0">
                                                <asp:GridView ID="gvorder" runat="server" Width="100%" CellPadding="1" CellSpacing="1"
                                                    AutoGenerateColumns="False" OnRowDataBound="gvorder_RowDataBound">
                                                    <HeaderStyle CssClass="tablemb" />
                                                    <AlternatingRowStyle BackColor="#F1F4F8" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Wrap="false">
                                                            <HeaderTemplate>
                                                                操作
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="CB" Checked="true" runat="server" />
                                                                <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("isreceived") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="False"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="明细">
                                                            <HeaderTemplate>
                                                                <span>明细 </span>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="images/view-button.png"
                                                                    PostBackUrl='<%# Eval("docid","ShowOrderDetailsSH.aspx?byy={0}") %>'></asp:ImageButton>
                                                                <asp:HiddenField ID="HF" Value='<%# Eval("docid") %>' runat="server" />
                                                                <asp:LinkButton ID="L1" PostBackUrl='ShowOrderDetailsSH.aspx?byy=<%# Eval("docid") %>)'
                                                                    Visible="false" runat="server" Text="查看"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="docid" HeaderText="出库单号"></asp:BoundField>
                                                        <asp:BoundField DataField="storeorderid" HeaderText="订单号"></asp:BoundField>
                                                        <asp:TemplateField HeaderText="快递单号">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <%# Eval("kuaididh")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="ExpectNum" HeaderText="期数"></asp:BoundField>
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
                                                                <asp:Label ID="Label1" runat="server" Text='<%# GetOrderDate(Eval("orderdatetime"))%>'>
                                                                   
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <table cellspacing="1" cellpadding="1" width="100%">
                                                            <tr class="ctConPgTab">
                                                                <th>
                                                                    <%=GetTran("000015", "操作")%>
                                                                </th>
                                                                <th>
                                                                    <%=GetTran("000811", "明细")%>
                                                                </th>
                                                                <th>
                                                                    <%=GetTran("002158", "出库单号")%>
                                                                </th>
                                                                <th>
                                                                    <%=GetTran("000079", "订单号")%>
                                                                </th>
                                                                <th>
                                                                    <%=GetTran("007206", "快递单号")%>
                                                                </th>
                                                                <th>
                                                                    <%=GetTran("000045", "期数")%>
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
                                                            </tr>
                                                        </table>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <div style="float:left;margin-top: 35px;line-height: 30px;">
                                    <span style="float:left"><%=GetTran("004198","全选")%>：</span>
                                <asp:CheckBox style="width: 20px;float:left" ID="CheckBox1"  runat="server"  onclick="checkall(this);" />
                                    <asp:Button ID="Button1"  runat="server" Height="27px" Width="47px" style="float:left; margin-top: 1px; margin-left: 8px; padding: 4px 9px; background-color: #96c742; color: #FFF; border: 1px solid #507E0C; background-image: linear-gradient(#addf58,#96c742); text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.4);"
                                        Text="确认收货" OnClick="Button1_Click" OnClientClick="return checkDate();" CssClass="anyes" />
                                </div>
                        </div>
                    </div>

                </div>

            </div>
        </div>
     <!-- #include file = "comcode.html" -->

    </form>
    <script type="text/jscript">
        $(function () {
            $('#rdbtnType label').css('float', 'left');
        
            $('#rdbtnType input').css({ 'width': '5%', border: 0, 'margin': '5px' });

            //$('#rdbtnType label').css('float', 'left');
            //$('#rdbtnType').css({ 'width': '30%', 'margin-left': '5px' });
            //$('#rdbtnType input').css({ 'width': '5%', 'margin': '5px', border: 0,'border':'0px'})
            $('#Pager1_pageTable').css('margin-right', '0px');
            $('#Pager1_btn_submit').css('Height', '22px');
            $('#Pager1_btn_submit').css('width', '70px');
            $('#Pager1_pageTable').css('color', '#333');
            $('#Pager1_ShowTable').css('color', '#333');
            //$('input[type="CheckBox"]')({ 'float:': 'left', 'width': 'auto' })
        })
    </script>
</body>

</html>
