<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Lsjjxxaspx.aspx.cs" Inherits="MemberMobile_Lsjjxxaspx" %>


<%@ Register Src="../UserControl/ExpectNum.ascx" TagName="ExpectNum1" TagPrefix="Uc1" %>

<%@ Register Src="~/UserControl/MemberBottom.ascx" TagName="MemberBottom" TagPrefix="Uc1" %>
<%@ Register Src="~/UserControl/MemberTop.ascx" TagName="MemberTop" TagPrefix="Uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no">
<meta name="apple-mobile-web-app-capable" content="yes">
<meta name="apple-mobile-web-app-status-bar-style" content="black">
<meta name="format-detection" content="telephone=no">
    <link rel="stylesheet" href="css/style.css">
    <script src="js/jquery-1.7.1.min.js"></script>

    <title><%=GetTran("004052","历史奖金") %> </title>
        <link href="hycss/serviceOrganiz.css" rel="stylesheet" />
<style>

     .searchFactor {
        margin-bottom:5px
    }
</style>

    <script type="text/javascript" >
        //选择不同语言是将要改的样式放到此处
        var lang = $("#lang").text();
        if (lang != "L001") {
            //alert("1111");

            //alert("BasicSearch");

        }

 </script> 
</head>

<body>
    <b id="lang" style="display:none"><%=Session["LanguageCode"] %></b>
 
     <form id="form2" runat="server">
              
 	<div class="t_top">	
                	<%=GetTran("003097","奖金查询") %>
            	
                
            </div>
            <div class="middle">
            <div class="mailTile">

                <div class="mailLT mailSlt mailbtn">
                   <%=GetTran("004052","历史奖金") %> 
                </div>
                <div class="mailRt mailbtn">
                    <a href="#"><%=GetTran("006064","最新奖金") %></a>
                </div>
            </div>

    
                 <div class="minMsg minMsg2" style="display:block">
               <div class="minMsgBox">
					<h3><%=GetTran("007572","本期收入")%></h3>

                    <ul>
                        <li>
                            <div class="zxjj_1"><span>  <%=GetTran("007578","推荐奖")%>：</span>
                                <asp:Label ID="Label1" runat="server" Text="" Width="80px"></asp:Label></div>

                            <div class="zxjj_1"><span>  <%=GetTran("007579","回本奖")%>：</span> 
                                 <asp:Label ID="Label2" runat="server" Text="" Width="80px"></asp:Label></div>
                            <div class="zxjj_1"><span> <%=GetTran("007580", "大区奖")%>：</span>
                                  <asp:Label ID="Label3" runat="server" Text="" Width="80px"></asp:Label>
                            </div>
                            <div class="zxjj_1"><span>  <%=GetTran("007581", "小区奖")%>：</span>
                                  <asp:Label ID="Label4" runat="server" Text="" Width="80px"></asp:Label>
                            </div>
                            <div class="zxjj_1"><span>  <%=GetTran("007582", "永续奖")%>：</span>
                                   <asp:Label ID="Label5" runat="server" Text="" Width="80px"></asp:Label>
                            </div>
                             <div class="zxjj_1"><span>  <%=GetTran("001352", "网平台综合管理费")%>：</span>
                                   <asp:Label ID="Label6" runat="server" Text="" Width="80px"></asp:Label>
                            </div>
                             <div class="zxjj_1"><span>    <%=GetTran("001353", "网扣福利奖金")%>：</span>
                                   <asp:Label ID="Label7" runat="server" Text="" Width="80px"></asp:Label>
                            </div>
                             <div class="zxjj_1"><span>  <%=GetTran("001355", "网扣重复消费")%>：</span>
                                   <asp:Label ID="Label8" runat="server" Text="" Width="80px"></asp:Label>
                            </div>
                             
                            
                            
                        </li>
                        <li>
                            <div class="zxjj_1"><span>   <%=GetTran("000254", "实发")%>：</span>
                                   <asp:Label ID="Label9" runat="server" Text="" Width="80px"></asp:Label>
                            </div>
                        </li>
                        <li class="last">
                             <div class="zxjj_1"><span>   <%=GetTran("000247", "总计")%>：</span>
                                   <asp:Label ID="Label10" runat="server" Text="" Width="80px"></asp:Label>
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
       <!-- #include file = "comcode.html" -->
         </div>
         </form>
<script type="text/javascript">
    $(function () {
        $('.mailbtn').on('click', function () {
            $(this).addClass('mailSlt').siblings('.mailbtn').removeClass('mailSlt');
            var Mindex = $(this).index();
            $('.minMsg').eq(Mindex).show().siblings('.minMsg').hide();

        })

    })
</script>
</body>


</html>

