<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResultBrowse1.aspx.cs" Inherits="Member_ResultBrowse" %>

<%@ Register Src="../MemberMobile/PageSj.ascx" TagName="PageSj" TagPrefix="uc1" %>

<%@ Register Src="~/UserControl/MemberBottom.ascx" TagName="MemberBottom" TagPrefix="Uc1" %>
<%@ Register Src="~/UserControl/MemberTop.ascx" TagName="MemberTop" TagPrefix="Uc1" %>
<%--<%@ Register Src="~/UserControl/MemberPager.ascx" TagName="MemberPager" TagPrefix="Uc1" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=11" />
      <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no">
<meta name="apple-mobile-web-app-capable" content="yes">
<meta name="apple-mobile-web-app-status-bar-style" content="black">
<meta name="format-detection" content="telephone=no">
    <title></title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="css/detail.css" rel="stylesheet" type="text/css" />

    <link href="../Member/hycss/serviceOrganiz.css" rel="stylesheet" />

    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>

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
    <script src="js/jquery-1.7.1.min.js"></script>
    <script type="text/javascript">
        function CheckText(btname) {
            //这个方法是页面有多个按钮要提交时，多次使用这个方法,传入按钮的ID
            filterSql_II(btname);

        }
        $(function () {
            $("#p1").each(function (index, ele) {
                if ($ele.text() == "未支付") {
                    $(this).style("color", "red");
                    $("#div").style("display", "block")
                } else {
                    $(this).style("color", "green");
                    $("#div").style("display", "none")
                }
            });
            });
    </script>
    <style>
        .btn
        {
                height: 21px;
    line-height: 21px;
    width: 14%;
    background: #85ac07;
    text-align: center;
    color: #fff;
    border-radius: 4px;
    margin-bottom: 5px;
        }
    </style>
</head>


<script type="text/javascript" >
    //选择不同语言是将要改的样式放到此处
    var lang = $("#lang").text();
    // alert(1);
    if (lang != "L001") {
        //alert("1111");

       // alert("ResultBrowse1");

    }

 </script>  

<body>
    <b id="lang" style="display:none"><%=Session["LanguageCode"] %></b>
    <form id="form2" runat="server">
        	<div class="t_top">	
            	<a class="backIcon" href="javascript:history.go(-1)"></a>

                	<%=GetTran("8140","充值浏览详细") %>
            	
                
            </div>

         <div class="middle">
            <div class="minMsg minMsg2" style="display:block">

                <div class="minMsgBox">
                 <ol>
                           <asp:Repeater ID="rep1" runat="server"   OnItemCommand="rep1_ItemCommand">
                            <ItemTemplate>
                                    <li>
                                        <div> 
                                            <%=GetTran("000024", "会员编号")%>：<%# Eval("RemitNumber  ") %>
                                            <br />
                                            <br />
                                            <%=GetTran("000519", "经办人")%>： <%# Eval("managers ") %>
                                            <br />

                                            <br />
                                            <%=GetTran("000603", "汇出金额")%>：<%#  (Convert.ToDouble( Eval("RemitMoney"))*
                        ( AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()) ))
                        ).ToString("f2")  %>
                                            <br />
                                            <br />
                                            <%=GetTran("000786", "付款日期")%>：<%# DateTime.Parse(Eval("ReceivablesDate").ToString()).AddHours(8)%>
                                            <br />
                                            <br />
                                            <%=GetTran("005881", "付款类型")%>：<%# GetPayWay(DataBinder.Eval(Container.DataItem,"PayWay").ToString()) %>
                                            <br />
                                            <br />
                                            <%=GetTran("000739", "付款期数")%>： <%# Eval("PayExpectNum ") %>
                                              <br />
                                            <br />
                                         
                                            <%=GetTran("000744", "查看备注")%>： <%# Eval("Remark ") %>
                                                 <br />
                                            <br />
                                            
                                            <%=GetTran("000775", "支付状态")%>： <%# Convert.ToBoolean( Eval("IsGSQR"))? GetTran("000517","已支付"):GetTran("000521","未支付") %>
                                            <%--OnCommand="linkbtnOK_Click"--%>
                                           <%-- <asp:ImageButton style="margin-left: 36%;" class="btn" ID="LinkBtnPay"   CommandName="Pay"  CommandArgument='<%#Eval("RemittancesID") %>' value="去支付" runat="server"></asp:ImageButton>--%>
                                            
                              <%--             <asp:ImageButton style="margin-left: 36%;" class="search3" ID="HyperLinkPayMent" OnCommand="linkbtnOK_Click" CommandArgument='<%# Eval("OrderID") %>' value="去支付" runat="server"></asp:ImageButton>    --%>
                                      
                                        </div>
                                    </li>
                               </ItemTemplate>
                      </asp:Repeater>
                        </ol>
                    </div>


               <%-- 查询--%>
                <div class="fiveSquareBox clearfix searchFactor" style="display:none"

                    <span class="onePart">
                        <span><%=GetTran("000605","是否审核") %>：</span>
                         <%=GetTran("000605","是否审核")%>： &nbsp;&nbsp;--
                        <asp:DropDownList ID="DropDownList1" CssClass="ctConPgFor" runat="server">
                        </asp:DropDownList>

                        <span>&nbsp <%=GetTran("000786", "付款日期")%>：</span>
                           <%=GetTran("000786", "付款日期")%>：

                        <asp:TextBox style="width:100px;margin-top:6px" ID="Datepicker1" CssClass="Wdate" runat="server" onfocus="WdatePicker()"></asp:TextBox>&nbsp;
                         <%--  <span>&nbsp <%=GetTran("000068", "至")%>：</span>--%>
                          <%=GetTran("000068", "至")%>&nbsp; &nbsp;
                        <asp:TextBox style="width:100px;margin-top:6px" ID="Datepicker2" runat="server" CssClass="Wdate" onfocus="WdatePicker()"></asp:TextBox>

<%--                        <asp:Button ID="BtnConfirm" runat="server" Height="27px" Width="47px" Style="margin-left: 17px; padding: 2px 9px; color: #FFF; border: 1px solid #507E0C; background-image: linear-gradient(#addf58,#96c742); text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.4);"
                        Text="查 询" CssClass="anyes" OnClick="BtnConfirm_Click"/>
                        ----%>
                        <asp:Button ID="BtnConfirm" runat="server" Height="27px" Width="47px" Style="margin-top: 1px; margin-left: 8px; padding: 4px 9px; background-color: #96c742; color: #FFF; border: 1px solid #507E0C; background-image: linear-gradient(#addf58,#96c742); text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.4);"
                            Text="查 询" OnClick="BtnConfirm_Click" CssClass="anyes" />
                    </span>


                </div>

                <div class="minMsgBox" style="display:none">
				<h3>离线汇款管理</h3>

                    <ol>
                          <asp:Repeater ID="rep" runat="server" >
                            <ItemTemplate>
                        <li>
                        	<a href="ResultBrowse.aspx?id=<%# Eval("id") %>">
                                <div><%# Eval("RemitNumber  ")  %><label><%# Eval("RemitMoney") %></label></div>
                                <p><%# Eval("ReceivablesDate ") %><span>payWay</span></p>
                            </a>
                        </li>
                                </ItemTemplate>
                         </asp:Repeater >
                    </ol>
                </div>


            <div style="display:none; margin-left: 10px">
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                    &nbsp;&nbsp;
                    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                </div>
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
          <uc1:PageSj ID="Pager1" runat="server" />
       <%-- <Uc1:MemberBottom ID="bottom" runat="server" />--%>
        <!-- #include file = "comcode.html" -->

    </form>
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
        })
    </script>
</body>
</html>
