<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResultBrowse.aspx.cs" Inherits="Member_ResultBrowse" %>

<%--<%@ Register Src="../MemberMobile/PageSj.ascx" TagName="PageSj" TagPrefix="uc1" %>--%>
<%@ Register Src="../UserControl/ucPagerMb.ascx" TagName="ucPagerMb" TagPrefix="uc1" %>
<%--<%@ Register Src="~/UserControl/MemberPager.ascx" TagName="MemberPager" TagPrefix="Uc1" %>--%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=11" />
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />
    <title></title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="css/detail.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-1.4.3.min.js"></script>
    <%--<link href="../Member/hycss/serviceOrganiz.css" rel="stylesheet" />--%>

    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
    <style>
        .searchFactor{padding:25px 15px 15px;border: 1px solid #999;background: #f8f8f8;border-radius: 6px;box-shadow: 0px 0px 6px #c1c1c1;}
        .searchFactor span{float: left;width:auto;display: block;}
        .searchFactor span select,.searchFactor span input{float: left;width: 12%;/*margin: 0 1%*/ margin-left:10px;position: relative;top: -4px;}
        .searchFactor span:nth-child(1){width: auto;}
        .searchFactor span:nth-child(5){width:auto;}
        .searchFactor .widthAuto{width: auto;}
    </style> 
    <script type="text/javascript">
<!--
         
    function MM_preloadImages() { //v3.0
        var d = document; if (d.images) {
            if (!d.MM_p) d.MM_p = new Array();
            var i, j = d.MM_p.length, a = MM_preloadImages.arguments; for (i = 0; i < a.length; i++)
                if (a[i].indexOf("#") != 0) { d.MM_p[j] = new Image; d.MM_p[j++].src = a[i]; }
        }
    }
    //-->
    </script>

    <script language="javascript" type="text/javascript" src="../js/SqlCheck.js"></script>

    <script type="text/javascript">
        function CheckText(btname) {
            //这个方法是页面有多个按钮要提交时，多次使用这个方法,传入按钮的ID
            filterSql_II(btname);

        }
    </script>
    <style type="text/css">
        .DropDownList1 {
         margin-top:7%;
        }
        #ucPagerMb1_txtPn {
            width: 30px;
            margin: 0 5px;
            height: 23px;
        }
    </style>
</head>
<body>
    <b id="lang" style="display:none"><%=Session["LanguageCode"] %></b>
    <form id="form2" runat="server">
        <div class="t_top">	
            <a class="backIcon" href="javascript:history.go(-1)"></a>
            <%=GetTran("005843","充值浏览") %>
        </div>

        <div class="middle">
            <div class="minMsg minMsg2" style="display:block">
               <%-- 查询 --%>
               <div class="fiveSquareBox clearfix searchFactor">
                    <span style="overflow:hidden;line-height:30px;float:left;">
                        <span style="float:left"><%=GetTran("000605","是否审核") %>：</span>
                        <span style="float:left">
                        <asp:DropDownList ID="DropDownList1" runat="server" style="width:80px;margin-top:10px;">
                        </asp:DropDownList></span>
                    </span>
                    <span style=" overflow: hidden;"  >
                        <span class="rq" style="float: left"> <%=GetTran("000786", "付款日期")%>：</span>
                        <span class="srq">
                            <asp:TextBox style="margin-top: 5px;width:80px;" ID="Datepicker1" CssClass="Wdate" runat="server" onfocus="WdatePicker()"></asp:TextBox>
                            <span>&nbsp;<%=GetTran("000068", "至")%></span>
                            <asp:TextBox  style="margin-top: 5px;width:80px;" ID="Datepicker2" runat="server" CssClass="Wdate" onfocus="WdatePicker()"></asp:TextBox>
                       </span>    
                    </span>
                       
                        <asp:Button ID="BtnConfirm" runat="server" Height="27px" Width="100%"
                             Style="margin-top: 1px; margin-left: -1px; padding: 4px 9px; background-color: #96c742; color: #FFF; 
                                border: 1px solid #507E0C; background-image: linear-gradient(#addf58,#96c742); 
                                text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.4);"
                            Text="查 询" OnClick="BtnConfirm_Click" CssClass="anyes" />
                </div>
                <div class="minMsgBox">
				<h3></h3>
                       <div style="display:block; margin-left: 10px;display:none;">
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                    &nbsp;&nbsp;
                    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                </div>
                    <ol>
                        <asp:Repeater ID="rep" runat="server" >
                        <ItemTemplate>
                        <li>
                        	<a href="ResultBrowse1.aspx?id=<%# Eval("id") %>">
                                <div><%# Eval("RemitNumber") %><label>
                                <%--<%# Eval("RemitMoney") %>--%>
                                <%=( AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()) ))==1?"$":"￥"%>
                                <%# (Convert.ToDouble(DataBinder.Eval(Container.DataItem, "RemitMoney"))*
                                    (AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString())))).ToString("f2") %>
                                                                  </label></div>
                                <p>
                                    <%# DateTime.Parse(Eval("ReceivablesDate").ToString()).AddHours(8)%>
                                    <%--<span><%# Convert.ToBoolean( Eval("IsGSQR"))? GetTran("000517","已支付"):GetTran("000521","未支付")%> </span>--%>
                                    <span><%# GetWState(Convert.ToBoolean(Eval("IsGSQR")), Eval("hkid").ToString(), Eval("shenhestate").ToString()) %></span>
                                </p>
                            </a>
                        </li>
                        </ItemTemplate>
                        </asp:Repeater>
                    </ol>
                </div>
                 <!-- #include file = "comcode.html" -->
                <div class="noticeEmail width100per mglt0" style="display:none; margin-top:20px">
                    <div class="pcMobileCut">
                        <div class="noticeHead">
                            <div>
                                <i class="glyphicon glyphicon-file"></i>
                                <span><%=GetTran("005843","充值浏览") %></span>
                            </div>
                        </div>
                        <div class="noticeBody">
                            <div class="tableWrap clearfix table-responsive">
                                <table class="table-bordered noticeTable">
                                    <tbody>
                                        <tr>
                                            <td style="padding: 0">
                                                <asp:GridView ID="GridView2" runat="server" OnRowCommand="GridView2_RowCommand" OnRowDataBound="GridView2_RowDataBound"
                                                    AutoGenerateColumns="False" Width="100%" border="0" CellSpacing="1" CellPadding="0">
                                                    <HeaderStyle CssClass="tablemb" />
                                                    <RowStyle HorizontalAlign="Center" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Wrap="false">
                                                            <HeaderTemplate>
                                                                支付
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="LinkBtnPay" runat="server" CommandName="Pay" CommandArgument='<%#Eval("RemittancesID") %>'
                                                                    Width="20" Height="20" />
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="False"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="查看备注">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <%#SetVisible(DataBinder.Eval(Container.DataItem, "Remark").ToString(), DataBinder.Eval(Container.DataItem, "id").ToString())%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="删除">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="LinkBtnDelete" runat="server" CommandName="Del" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.RemitNumber")%>'
                                                                    Width="20" Height="20" />

                                                                <input id="HidGsqr" type="hidden" value='<%# DataBinder.Eval(Container, "DataItem.IsGSQR")%>'
                                                                    name="Hidden3" runat="server" />
                                                                <input id="HidId" type="hidden" value='<%# DataBinder.Eval(Container,"DataItem.Id")%>'
                                                                    name="Hidden4" runat="server" />
                                                                <input id="HidPayway" type="hidden" value='<%#DataBinder.Eval(Container,"DataItem.payway") %>'
                                                                    name="hidsg" runat="server" />
                                                                <input id="HidTotalMoney" type="hidden" value='<%#DataBinder.Eval(Container,"DataItem.remitMoney") %>'
                                                                    name="hidsg" runat="server" />
                                                                <input id="Hidremittancesid" type="hidden" value='<%#DataBinder.Eval(Container,"DataItem.remittancesid") %>'
                                                                    name="hidR" runat="server" />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="RemitNumber" HeaderText="会员编号" />
                                                        <asp:BoundField DataField="RemittancesMoney" HeaderText="汇出金额" DataFormatString="{0:f2}" />
                                                        <asp:TemplateField HeaderText="支付状态">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <%# GetPayStatus(DataBinder.Eval(Container.DataItem, "isgsqr").ToString())%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="付款日期">
                                                            <ItemTemplate>
                                                                <%# bool.Parse(Eval("isgsqr").ToString()) == false ? "" : DateTime.Parse(Eval("ReceivablesDate").ToString()).ToString("yyyy-MM-dd")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="付款期数">
                                                            <ItemTemplate>
                                                                <%# bool.Parse(Eval("isgsqr").ToString()) == false?"":Eval("PayexpectNum").ToString()%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="付款类型">
                                                            <ItemTemplate>
                                                                <%# bool.Parse(Eval("isgsqr").ToString()) == false ? GetTran("000221","无") : GetPayWay(Eval("PayWay").ToString())%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="ImportBank" HeaderText="汇入银行" />
                                                        <asp:BoundField DataField="Managers" HeaderText="经办人" />
                                                        <asp:BoundField DataField="Sender" HeaderText="汇款人" />
                                                        <asp:BoundField DataField="SenderID" HeaderText="汇款人身份证" />
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <table width="100%" border="0" cellspacing="1" cellpadding="0">
                                                            <tr class="ctConPgTab">
                                                                <th>
                                                                    <%=GetTran("000015", "操作")%>
                                                                </th>
                                                                <th>
                                                                    <%=GetTran("000024", "会员编号")%>
                                                                </th>
                                                                <th>
                                                                    <%=GetTran("000737", "付款数额")%>
                                                                </th>
                                                                <th style="display: none;">
                                                                    <%=GetTran("000738", "付款用途")%>
                                                                </th>
                                                                <th>
                                                                    <%=GetTran("000739", "付款期数")%>
                                                                </th>
                                                                <th>
                                                                    <%=GetTran("000698", "付款方式")%>
                                                                </th>
                                                                <th>
                                                                    <%=GetTran("000595", "确认方式")%>
                                                                </th>
                                                                <th>
                                                                    <%=GetTran("000601", "汇入银行")%>
                                                                </th>
                                                                <th>
                                                                    <%=GetTran("000519", "经办人")%>
                                                                </th>
                                                                <th>
                                                                    <%=GetTran("000602", "汇款人")%>
                                                                </th>
                                                                <th>
                                                                    <%=GetTran("000743", "汇款人身份证")%>
                                                                </th>
                                                                <th>
                                                                    <%=GetTran("000744", "查看备注")%>
                                                                </th>
                                                            </tr>
                                                        </table>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            <%--    <Uc1:MemberPager ID="Pager2" runat="server" />--%>

                            </div>
                           
                        </div>
                    </div>

                </div>

            </div>
        </div>
            <script type="text/jscript">
               
        $(function () {
            $('#rdbtnType label').css('float', 'left');
            $('#rdbtnType').css({ 'width': '22%', 'margin-left': '5px' });
            $('#rdbtnType input').css({ 'width': '10%', 'margin-left': '5px' });
            $('#Pager1_pageTable').css('margin-right', '0px');
            $('#Pager1_btn_submit').css('Height', '22px');
            $('#Pager1_btn_submit').css('width', '70px');
            $('#Pager1_pageTable').css('color', '#333');
            $('#Pager1_ShowTable').css('color', '#333');
            //$('input[type="CheckBox"]')({ 'float:': 'left', 'width': 'auto' })
            $(function () {
               // alert(1);
                //选择不同语言是将要改的样式放到此处
                var lang = $('#lang').text();
                //alert(1);
                if (lang!= "L001") {
                    //alert("1111");
                    $('.rq').width('100%');
                    $('#Datepicker2').css({ 'width': '90%', 'margin-left': '-63%' })
                    $('.drq').css({ 'margin-top': '0', 'margin-left': '70%' });
                    $('.to').css({ 'margin-left': '-15%' })
                }
            })
        })
    </script>
        <script type="text/javascript" >
           

 </script>  
          <uc1:ucPagerMb ID="ucPagerMb1" runat="server" />
       <%-- <Uc1:MemberBottom ID="bottom" runat="server" />--%>
    </form>

</body>
</html>
