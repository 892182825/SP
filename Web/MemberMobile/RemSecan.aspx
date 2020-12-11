<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RemSecan.aspx.cs" Inherits="RemSecan" %>

<%@ Register Src="../MemberMobile/PageSj.ascx" TagName="PageSj" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/MemberTop.ascx" TagName="MemberTop" TagPrefix="uc2" %>
<%--<%@ Register Src="../UserControl/MemberPager1.ascx" TagName="MemberPager1" TagPrefix="uc3" %>--%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=11" />
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />

    <title></title>

    <link rel="stylesheet" href="CSS/style.css" />
    <link href="hycss/serviceOrganiz.css" rel="stylesheet" />
    <script src="js/jquery-1.7.1.min.js"></script>

    <script src="js/jquery-3.1.1.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="js/bootstrap.js" type="text/javascript" charset="utf-8"></script>
    <script src="js/serviceOrganiz.js" type="text/javascript" charset="utf-8"></script>
    <script src="js/jquery-migrate-1.1.0.min.js" type="text/javascript" charset="utf-8"></script>
    <script type="text/javascript" src="js/jquery.mCustomScrollbar.js"></script>
    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
</head>
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
    .fiveSquareBox {
        margin-bottom:4px;
    }
    input[type=checkbox] {
        float: right;
        width: auto;
        margin-right: 45px;
    }
    .proLayerLine ul li {
        overflow:hidden;
        width:50%;
        float:left;
        line-height:28px;
    }
    .proLayerLine ul {
        overflow:hidden
    }
</style>

<body>
    <%
        if (Session["LanguageCode"] == null)
        {
            Session["LanguageCode"] = "L001";
        }
    %>
    <b id="lang" style="display:none"><%=Session["LanguageCode"] %></b>
    <form id="form2" runat="server">
        <div class="t_top">	
            <a class="backIcon" href="javascript:history.go(-1)"></a>
                <%=GetTran("9112", "转账汇款管理")%> 
        </div>

        <div class="middle">
            <div  class="minMsg minMsg2" style="display:block">
                <div class="minMsgBox">
                      <div id="qq2" class=" clearfix searchFactor"  >
                 
                <span style="width:50%;overflow:hidden;line-height:30px;float:left">
                 <span style="float:left"><%=GetTran("007369","充值状态") %>：</span>
                    <asp:DropDownList ID="ddlpaystate" runat="server" style="width: 50%;float:left;margin-top: 3%;">
                        <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="未到账" Selected="True" Value="0"></asp:ListItem>
                        <asp:ListItem Text="已到账" Value="1"></asp:ListItem>
                    </asp:DropDownList>
                </span>
                <span style=" overflow: hidden" >
                    <span  id="rq" style="float: left"><%=GetTran("007248","申请时间") %>：</span>

                    <asp:TextBox ID="txtstdate" CssClass="Wdate" runat="server" onfocus="WdatePicker()" style="margin-top: 5px; width: 32%;"></asp:TextBox>
                
                      <span style="float: left; width: 9%; text-align: center"> <%=GetTran("000068", "至")%></span>
                    <asp:TextBox ID="txtenddate" CssClass="Wdate" runat="server" onfocus="WdatePicker()" style="margin-top: 5px;width: 32%;"></asp:TextBox>
                    </span>
                          <asp:Button ID="btnnopay" runat="server" Height="27px" Width="100%" Style="margin-top: 1px; margin-left: -1px; padding: 4px 9px; background-color: #96c742; color: #FFF; border: 1px solid #507E0C; background-image: linear-gradient(#addf58,#96c742); text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.4);"
                        Text="查 询" CssClass="anyes"  OnClick="btnsearch_Click" />
                </div>
				     <ol >
                          <asp:Repeater ID="rep" runat="server" >
                            <ItemTemplate>
                             <li>
                        	            <a  href="RemSecan1.aspx?id=<%# Eval("ID")%>" >
                                            <div>  <%=GetTran("000777", "汇款人姓名")%>：<%# Eval("name") %> <label>
                                                <%-- <%# Eval("totalrmbmoney") %> --%>
                                                <%=( AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()) ))==1?"$":"￥"%>

                                                <%# Math.Abs( Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalmoney"))*
                        ( AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()) ))
                        ).ToString("#0.00") %>                   </label></div>
                                            <p><%=GetTran("007372", "汇款申请时间")%>：<%# DateTime.Parse(Eval("remittancesdate").ToString()).AddHours(8) %>
                                                <span>
                                                    <asp:Label ID="lblleavetime" runat="server"></asp:Label>
                                                     <%# (Convert.ToInt32(Eval("flag")) == 1)?GetTran("007371","已到账") : getzt( Convert.ToInt32(DateTime.Now.Subtract(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "remittancesdate")).AddHours(Convert.ToDouble(Session["WTH"]))).TotalHours),
                                             ((Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "remittancesdate"))).AddHours(48).Subtract(DateTime.Now))
                                                ) %><%--.AddHours(Convert.ToDouble(Session["WTH"]))--%>
                                           
                                                
                                                                                                              </span></p>   
                                        </a>
                           </li>
                           </ItemTemplate>
                      </asp:Repeater>
                      </ol>
                    
                      
                </div>
            
             </div>
        </div>
        <uc1:PageSj ID="ucPager1" runat="server" />
      
       <!-- #include file = "comcode.html" -->
    </form>
    <%--<script type="text/jscript">
        $(function () {
            $('#rtbiszf label').css('float', 'left');
            $('#rtbiszf').css('width', '16%');
            $('#Pager1_pageTable').css('margin-right', '0px');
            $('#Pager1_btn_submit').css('Height', '22px');
            $('#Pager1_btn_submit').css('width', '70px');
            $('#Pager1_pageTable').css('color', '#333');
            $('#Pager1_ShowTable').css('color', '#333');
            $('input[type="checkbox"]').css({ 'width': '18px', 'margin-right': '10px' })
            $("#qq1").css('width', '101%');
            $("#qq2").css('width', '101%');
        })
    </script>
    <style>
        .tablemb th {
            padding: 10px 16px;
            border-left: #bebebe !important;
            font-family: Arial;
            font-size: 12px;
            font-weight: bold;
            color: #333;
            text-decoration: none;
            /* background-image: url(../images/tabledp.gif); */
            background-repeat: repeat-x;
            text-align: center;
            text-indent: 10px;
        }

        .tablemb {
            font-family: Arial;
            /* font-size: 12px; */
            /* color: #333; */
            /* margin-top: 90px; */
            text-decoration: none;
            line-height: 31px;
            background-color: #FAFAFA;
            /* border: 1px solid #88F4AE; */
            text-indent: 10px;
            white-space: normal;
            background: url(../../images/img/mws-table-header.png) left bottom repeat-x;
        }
        .rightArea {
            padding-top: 40px;
            min-height: 100%;
        }
        .searchFactor span {
            float: left;
            width: auto;
            display: block;
        }
    </style>--%>
</body>

</html>
<script type="text/javascript" >
    $(function () {
        var lang = $("#lang").text();
        if (lang != "L001") {
            $('#rq').width('100%')
        }
    });
    //选择不同语言是将要改的样式放到此处
</script>  
<script language="javascript" type="text/javascript">
    function check() {
        if (confirm('<%=GetTran("007620","请慎重操作！如果您还没有汇出该款，或者正准备汇款，请不要点击此“款已汇出”按钮！否则您将受到损失！  如果您确认该笔款已经汇出，请点确定按钮即可。") %>')) {
            return true;
        } else {
            return false;
        }
    }
</script>
<script>
    function js_method() {
        document.getElementById(div1).style.display == "none";
        document.getElementById(div2).style.display == "black";
    }
    var divup;
    var bkdiv;
    function showdiv(rid, pic) {

        var hidremid = document.getElementById("hidremid");
        hidremid.value = rid;
        var imgshow = document.getElementById("imgshow");

        if (pic != "")
            imgshow.src = "upload/" + pic;
        else imgshow.src = "upload/df.gif";

        divup = document.getElementById("useUpload");
        bkdiv = document.getElementById("bkdiv");
        var sl = document.body.offsetWidth;
        var st = document.body.offsetHeight;

        bkdiv.style.width = sl;
        bkdiv.style.height = st;

        divup.style.left = (sl - 300) / 2;
        divup.style.top = (st - 200) / 2;
        divup.style.display = "block";

        bkdiv.style.display = "";
        divup.style.zIndex = "101";
        bkdiv.style.zIndex = "100";
        return false;
    }
    function closediv() {
        divup.style.display = "none";

        bkdiv.style.display = "none";
    }
</script>