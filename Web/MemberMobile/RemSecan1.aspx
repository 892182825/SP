<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RemSecan1.aspx.cs" Inherits="RemSecan" %>

<%@ Register Src="../MemberMobile/PageSj.ascx" TagName="PageSj" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/MemberTop.ascx" TagName="MemberTop" TagPrefix="uc2" %>
<%--<%@ Register Src="../UserControl/MemberPager1.ascx" TagName="MemberPager1" TagPrefix="uc3" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=11" />
      <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no">
<meta name="apple-mobile-web-app-capable" content="yes">
<meta name="apple-mobile-web-app-status-bar-style" content="black">
<meta name="format-detection" content="telephone=no">
    <title></title>
   <script src="js/jquery-1.7.1.min.js"></script>
<link rel="stylesheet" href="css/style.css">
    <script src="js/jquery-3.1.1.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="js/bootstrap.js" type="text/javascript" charset="utf-8"></script>
    <script src="js/serviceOrganiz.js" type="text/javascript" charset="utf-8"></script>
    <script src="js/jquery-migrate-1.1.0.min.js" type="text/javascript" charset="utf-8"></script>
    <script type="text/javascript" src="js/jquery.mCustomScrollbar.js"></script>
    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
</head>
    <script type="text/javascript" >
        $(function () {
            //选择不同语言是将要改的样式放到此处
            var lang = $("#lang").text();;
            //alert(1);
            if (lang != "L001") {
                //alert("1111");

                //alert("RemSecan");

            }
        })
        

 </script>  

    <body>
        <b id="lang" style="display:none"><%=Session["LanguageCode"] %></b>
    <form id="form2" runat="server">
          	<div class="t_top">	
            	<a class="backIcon" href="RemSecan.aspx"></a>

                	      	<%=GetTran("9112", "转账汇款管理")%>
            
                
            </div>

            <div id="qq2" class="fiveSquareBox clearfix searchFactor" style="display:none">
                  <%--  <asp:Button ID="btnnopay" CssClass="anyes" runat="server" Text="查询" OnClick="btnsearch_Click" />--%>
                    <span><%=GetTran("007248","申请时间") %>：</span>
                    <asp:TextBox ID="txtstdate" CssClass="Wdate" runat="server" onfocus="WdatePicker()" style="float:left" Width="85px"></asp:TextBox>
                    <span>&nbsp&nbsp  <%=GetTran("000068", "至")%>&nbsp&nbsp</span>
                    <asp:TextBox ID="txtenddate" CssClass="Wdate" runat="server" onfocus="WdatePicker()" style="float:left" Width="85px"></asp:TextBox>
                    <span>&nbsp&nbsp <%=GetTran("007369","充值状态") %>：</span>
                    <asp:DropDownList ID="ddlpaystate" runat="server" style="width:auto;margin-top:-3px">
                        <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="未到账" Selected="True" Value="0"></asp:ListItem>
                        <asp:ListItem Text="已到账" Value="1"></asp:ListItem>
                    </asp:DropDownList>
                          <asp:Button ID="btnnopay" runat="server" Height="27px" Width="47px" Style=" margin-left: 17px; padding: 2px 9px; color: #FFF; border: 1px solid #507E0C;background:#507E0C; background-image: linear-gradient(#addf58,#96c742); text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.4);"
                        Text="查 询" CssClass="anyes"  OnClick="btnsearch_Click" />
                </div>
          
           <div class="middle" style="display:none">
            <div id="div1" class="minMsg minMsg2" >
                <div class="minMsgBox">
				<h3><%=GetTran("8141  ","离线汇款详情") %></h3>
                   
                </div>
            </div>
        </div>
         <div id="div2" class="minMsg" style="display:block">
                <div class="minMsgBox">
					 <div id="div3" style="display:none">
				     <ol>
                          <asp:Repeater ID="rep" runat="server" >
                            <ItemTemplate>
                        	 <li>
                                        <li>
                        	<a   id="a" runat="server" href="#" >
                                <div><%=GetTran("000063","会员昵称") %>：<%# Eval("name") %> <label>
                                     <%#  (Convert.ToDouble( Eval("totalrmbmoney"))*
                        ( AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()) ))
                        ).ToString("f2") %> </label></div>
                                <p><%=GetTran("000057","注册日期") %>：<%# Eval("remittancesdate") %><span><%=GetTran("007524","已激活") %></span></p>
                                    
                            </a>
                           </li>
                           </ItemTemplate>
                      </asp:Repeater>
                      </ol>
                     <div>
                      
                </div>
            
             </div>
                     <div>
                        <ol>
                           <asp:Repeater ID="rep1" runat="server" OnItemCommand="gvSecanRemits_RowCommand"  OnItemDataBound="gvSecanRemits_RowDataBound">
                            <ItemTemplate>
                                    <li>
                                        <div> 
                                            <%=GetTran("001892", "汇款人编号")%>：<%# Eval("number") %>
                                            <br />
                                            <br />
                                            <%=GetTran("000777", "汇款人姓名")%>： <%# Eval("Name") %>
                                            <br />
                                            <br />
                                            <%=GetTran("001970", "汇款金额")%>：
                                            <%#Math.Abs((Convert.ToDouble( Eval("totalmoney"))*
                        ( AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()) ))
                        )).ToString("#0.00")  %>
                                           
                                            <br />
                                            <br />
                                            <%=GetTran("007372", "汇款申请时间")%>：<%# DateTime.Parse(Eval("remittancesdate").ToString()).AddHours(8)  %>
                                            <br />
                                            <br />
                                            <%=GetTran("007374", "到账情况")%>：<%#GetSendType1(Eval("flag").ToString()) %>
                                            <br />
                                            <br />
                                            <%=GetTran("007375", "到账时间")%>： <%# GetSendType1(Eval("flag").ToString())=="完成"? DateTime.Parse(Eval("CompanySureTime").ToString()).ToString(): "00:00:00"%>
                                            <br />
                                            <br />
                                            <%=GetTran("007376", "剩余有效期")%>：<%# GetSendType1(Eval("flag").ToString())=="完成"? GetTran("007625","已过期") :  getzt( Convert.ToInt32(DateTime.Now.Subtract(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "remittancesdate")).AddHours(Convert.ToDouble(Session["WTH"]))).TotalHours),
                                             ((Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "remittancesdate"))).AddHours(48).Subtract(DateTime.Now))) %>
                                            <br /><%--.AddHours(Convert.ToDouble(Session["WTH"]))--%>
                                            <br />
                                              <%=GetTran("000015", "操作 ")%>：
                                            <asp:Button ID="btnconfirmrmd" CssClass="anyes" Height="27px" Width="76px" Style="width:auto;margin-left: 17px; padding: 0px 9px; color: #FFF; border: 1px solid #507E0C; background-image: linear-gradient(#addf58,#96c742); text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.4);" CommandName="cof" Visible="false" CommandArgument='<%#Eval("Remittancesid") %>' OnClientClick="return huikuan();" runat="server" Text="款已汇出" />
                                            <asp:Label ID="lblconinfo" style=" margin-right:270px" runat="server"><%=GetTran("007169", "已汇出")%></asp:Label>
                                            <asp:HiddenField ID="HiddenField1" Value='<%# Eval("totalrmbmoney") %>' runat="server" />
                                            <asp:HiddenField ID="HiddenField2" Value='<%# Eval("remittancesdate") %>' runat="server" />
                                             <asp:HiddenField ID="HiddenField3" Value='<%# Eval("memberflag") %>' runat="server" />
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

     <uc1:PageSj ID="ucPager1" runat="server" />
      <!-- #include file = "comcode.html" -->

   
    </form>
<%--    <script type="text/jscript">
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
 function showdiv(rid,pic) {
 
   var hidremid=document.getElementById("hidremid");
   hidremid.value=rid;
   var imgshow=document.getElementById("imgshow");
   
   if(pic!="")
   imgshow.src="upload/"+pic;
   else  imgshow.src="upload/df.gif";
   
   divup= document.getElementById("useUpload");
   bkdiv=document.getElementById("bkdiv");
  var sl=document.body.offsetWidth; 
   var st=document.body.offsetHeight;
  
  bkdiv.style.width=sl;
  bkdiv.style.height=st;
  
   divup.style.left=(sl-300)/2;
    divup.style.top=(st-200)/2;
    divup.style.display="block";
 
    bkdiv.style.display="";
    divup.style.zIndex="101";
    bkdiv.style.zIndex="100";
   return false;
 }
 function closediv(){ 
   divup.style.display="none";
 
    bkdiv.style.display="none";
 }
</script>

