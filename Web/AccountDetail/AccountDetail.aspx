<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccountDetail.aspx.cs" EnableEventValidation="false" Inherits="AccountDetail_AccountDetail" %>

<%@ Register Src="~/UserControl/MemberBottom.ascx" TagName="MemberBottom" TagPrefix="Uc1" %>
<%@ Register Src="~/UserControl/MemberTop.ascx" TagName="MemberTop" TagPrefix="Uc1" %>
<%@ Register Src="../UserControl/ucPagerMb.ascx" TagName="ucPagerMb" TagPrefix="uc1" %>

<%@ Register Src="~/UserControl/STop.ascx" TagPrefix="uc1" TagName="STop" %>
<%@ Register Src="~/UserControl/SLeft.ascx" TagPrefix="uc1" TagName="SLeft" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=8" />
    <link href="../member/css/style.css" rel="stylesheet" type="text/css" />
    <link href="../member/css/detail.css" rel="stylesheet" type="text/css" />
    <link href="../member/css/products.css" id="cssid" rel="stylesheet" type="text/css" />
    <link href="../css/table.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="../JS/jquery-1.2.6.js"></script>
    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
    <link href="../Member/hycss/serviceOrganiz.css" rel="stylesheet" />
    <style type="text/css">
        input[type="submit"] {
            height: 27px;
            width: 47px;
            float: left;
            /*margin-left: -42px;*/
            padding: 2px 9px;
            color: #FFF;
            border: 1px solid #507E0C;
            background: #507E0C;
            background-image: linear-gradient(#addf58,#96c742);
            text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.4);
        }

        body, html {
                word-break: inherit;
        }

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

        .proLayerLine li input{
        float:left;
        }
        .proLayerLine li {
            overflow: hidden;
            line-height: 38px;
        }
    </style>
    <script type="text/javascript" >

         $(function () {
             //选择不同语言是将要改的样式放到此处
             var lang = $("#lang").text();
             // alert(1);
             if (lang != "L001") {

                 $('.zhuan').width('89%')
                 $('.zhuan1').width('25%')
                 $('.zhuan2').width('25%')
                 $('.searchFactor span input').width('auto')


             }

             $('.zhuan1 #btn_kmtype').width('185px')
         });
    </script>  
</head>
<%--<body>
<form id="form1" name="form1" runat="server" method="post" action="">
<div class="MemberPage">
<Uc1:MemberTop ID="Top" runat="server" />

<!--内容部分,左下背景-->
<div class="centerCon-1">
	<!--内容,右下背景-->
	<div class="centConPage">
	  <div class="ctConPgList">
      	<ul>
      	<li>
                <asp:Button ID="btn_serach" runat="server" Width="52" Height="20" 
                    style="background-image:url(../member/images/loginButton1-fb.png)" Text="搜索" 
                    onclick="btn_serach_Click" />
                </li>
      	<li>
      	    <%=GetTran("000303","请选择")%><%=GetTran("006615","科目")%>：<input type="button" id="btn_kmtype" title="" style="background-image:url(../images/selem2.gif);width:187px;height:25px;font-size:12px;border-width:0px;" runat="server" />
      	</li>
      	<li><%=GetTran("000719","并且")%>：
      	    <asp:DropDownList ID="ddl_OutIn" style="border-width:1px;" runat="server">
      	        <asp:ListItem Value="-1" Text="全部"></asp:ListItem>
      	        <asp:ListItem Value="0" Text="转入"></asp:ListItem>
      	        <asp:ListItem Value="1" Text="转出"></asp:ListItem>
      	    </asp:DropDownList>
      	</li>
      	<li>
            <%=GetTran("006581", "发生时间")%>：
            <asp:Button ID="btn_zuotian" CssClass="anyes1" runat="server" Text="前天" 
                onclick="btn_zuotian_Click" />
            <asp:Button ID="btn_jintian" CssClass="anyes1" runat="server" Text="昨天" 
                onclick="btn_jintian_Click" />
            <asp:Button ID="btn_mingtian" CssClass="anyes1" runat="server" Text="今天" 
                onclick="btn_mingtian_Click" />
            <asp:TextBox ID="Datepicker1" CssClass="Wdate" runat="server" onfocus="WdatePicker()"
                Width="85px"></asp:TextBox>
            <%=GetTran("000068", "至")%>
            <asp:TextBox ID="Datepicker2" CssClass="Wdate" runat="server" onfocus="WdatePicker()"
                Width="85px"></asp:TextBox>
      	</li>
      	</ul>
      	</div>
      	<div class="proLayer" id="div_type" style="display:none;">
      	<div class="proLayerTitle">
    	<ul>
            <li class="titleLeft"><%=GetTran("007645", "请选择科目类别")%></li>
            <li class="titleRight"><a href="javascript:void(0)" id="a_type">[<%=GetTran("000434","确定")%>]</a></li>
        </ul>
        </div>
        <div class="proLayerLine">
    	   <ul>
	        <asp:Repeater ID="rep_kmtype" runat="server">
                <ItemTemplate>
    	                <li><input name="chb_name" type="checkbox" value='<%#Eval("SubjectID") %>' alt='<%#GetTran(Eval("SubjectName").ToString(),"") %>' /><%#GetTran(Eval("SubjectName").ToString(),"") %></li>
    	         </ItemTemplate>
            </asp:Repeater>
    	   </ul>
        </div>
      	</div>
      	      <div class="ctConPgList-1">
      	        <table width="100%" border="0" cellspacing="1" cellpadding="1">
      	            <tr class="ctConPgTab">
      	                <td><%=GetTran("000719","并且")%><%=GetTran("006615", "科目")%></td>
      	                <td><%=GetTran("006581","发生时间")%></td>
      	                <td><%=GetTran("007275","转入金额")%></td>
      	                <td><%=GetTran("001630","转出金额")%></td>
      	                <td><%=GetTran("007276","结余金额")%></td>
      	                <td><%=GetTran("006616","摘要")%></td>
      	            </tr>
      	            <asp:Repeater ID="rep_km" runat="server">
      	                <ItemTemplate>
      	                    <tr>
      	                        <td>
      	                        <%#BLL.Logistics.D_AccountBLL.GetKmtype(Eval("kmtype").ToString()) %>
      	                        </td>
      	                        <td>
      	                            <%# DateTime.Parse(Eval("happentime").ToString()).AddHours(8)%>
      	                        </td>
      	                        <td>
      	                            <%#Eval("Direction").ToString()=="0"? double.Parse(Eval("happenmoney").ToString()).ToString("0.00"):"" %>
      	                        </td>
      	                        <td>
      	                            <%#Eval("Direction").ToString()=="1"? Math.Abs(double.Parse(Eval("happenmoney").ToString())).ToString("0.00"):"" %>
      	                        </td>
      	                        <td>
      	                            <%#double.Parse(Eval("Balancemoney").ToString()).ToString("0.00")%>
      	                        </td>
      	                        <td>
      	                            <%#getMark(Eval("remark").ToString())%>
      	                        </td>
      	                    </tr> 
      	                </ItemTemplate>
      	            </asp:Repeater>
      	        </table>
      	        <table width="100%" border="0" cellspacing="0" cellpadding="0">
      	            <tr>
                        <td colspan="6" align="right"><asp:Literal ID="lit_heji" runat="server"></asp:Literal></td>
                    </tr>
      	        </table>
      	      </div>
      	      <div  class="prcSchPage">
       		<table cellspacing="1">
        	<tr>
        	    <td>  <%=GetTran("007648", "共找到")%><strong class="required"><%= ucPagerMb1.RecordCount %></strong><%=GetTran("006978","条")%></td>
            	<td> <uc1:ucPagerMb ID="ucPagerMb1" runat="server" /></td>
            	<td>
            	 <div class="btn">
                        	<div class="btnLeft"></div>
                        	<input name="" type="submit" class="btnC" value='<%=GetTran("000434","确定") %>' onclick="javascript:document.getElementById('ucPagerMb1_GoTo').click()" />

                            <div class="btnRight"></div>
                        </div></td>
            </tr>
           </table>
    </div></div></div>
<Uc1:MemberBottom ID="bottom" runat="server" />

</div>
</form>
</body>--%>


<body>
     <b id="lang" style="display:none"><%=Session["LanguageCode"] %></b>
    <form id="form2" runat="server">
        <Uc1:STop runat="server" ID="STop1" />


        <Uc1:SLeft runat="server" ID="SLeft1" />
        <Uc1:MemberTop ID="Top" runat="server" />
        <%--<Uc1:MemberTop ID="Top" runat="server" />--%>

        <div class="rightArea clearfix">
            <div class="rightAreaIn" >
                <div id="qq2" class="fiveSquareBox clearfix searchFactor">
                    <span style="width: 22%" class="zhuan1">
                       
                        <span><%=GetTran("006615", "科目")%>：</span>
                        <input type="button" id="btn_kmtype" title="" style="background-image: url(../images/selem2.gif); width: 85px; height: 25px;margin-top: 3px; font-size: 12px; border-width: 0px;" runat="server" />
                    </span>
                    <span style="width: 11%" class="zhuan2">
                        <span><%=GetTran("000719","并且")%>：</span>
                        <asp:DropDownList ID="ddl_OutIn" Style="border-width: 1px; width: 60px" runat="server">
                            <asp:ListItem Value="-1" Text="全部"></asp:ListItem>
                            <asp:ListItem Value="0" Text="转入"></asp:ListItem>
                            <asp:ListItem Value="1" Text="转出"></asp:ListItem>
                        </asp:DropDownList>

                    </span>
                    <span style="width: 60%" class="zhuan">
                        <span><%=GetTran("006581", "发生时间")%>：</span>
                        <span style="width: 75%; margin-top: 3px;">

                            <asp:Button ID="btn_zuotian" CssClass="anyes1" runat="server" Text="前天"
                                OnClick="btn_zuotian_Click" />
                            <asp:Button ID="btn_jintian" CssClass="anyes1" runat="server" Text="昨天"
                                OnClick="btn_jintian_Click" />
                            <asp:Button ID="btn_mingtian" CssClass="anyes1" runat="server" Text="今天"
                                OnClick="btn_mingtian_Click" />
                            <asp:TextBox ID="Datepicker1" Style="margin-top:5px" CssClass="Wdate" runat="server" onfocus="WdatePicker()"
                                Width="20.5%"></asp:TextBox>
                            <span><%=GetTran("000068", "至")%></span>
                            <asp:TextBox ID="Datepicker2" Style="margin-top:5px"  CssClass="Wdate" runat="server" onfocus="WdatePicker()"
                                Width="20.5%"></asp:TextBox>
                        </span>



                    </span>
                    <asp:Button ID="btn_serach" runat="server" Height="27px" Width="47px" Style="width:auto;float: left; margin-left: -42px; padding: 2px 9px; color: #FFF; border: 1px solid #507E0C; background-image: linear-gradient(#addf58,#96c742); text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.4);"
                        Text="搜索" CssClass="anyes" OnClick="btn_serach_Click" />

                </div>
                <div class="proLayer" id="div_type" style="display:none;">
                <div class="proLayerTitle">
                    <ul>
                        <li class="titleLeft"><%=GetTran("007645", "请选择科目类别")%></li>
                        <li class="titleRight"><a href="javascript:void(0)" id="a_type">[<%=GetTran("000434","确定")%>]</a></li>
                    </ul>
                </div>
                <div class="proLayerLine">
                    <ul>
                        <asp:Repeater ID="rep_kmtype" runat="server">
                            <ItemTemplate>
                                <li>
                                    <input name="chb_name" type="checkbox" value='<%#Eval("SubjectID") %>' alt='<%#GetTran(Eval("SubjectName").ToString(),"") %>' /><%#GetTran(Eval("SubjectName").ToString(),"") %></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                    </div>

                <div id="qq1" class="noticeEmail width100per mglt0">
                    <div class="pcMobileCut">
                        <div class="noticeHead">
                            <div>
                                <i class="glyphicon glyphicon-file"></i>
                                <span><%=GetTran("008140","账户明细") %></span>
                            </div>
                        </div>
                        <div class="noticeBody">
                            <div class="tableWrap clearfix table-responsive" >
                                <table class="table-bordered noticeTable">
                                    <tr class="tablemb">
                                        <td style="white-space:nowrap;"><%=GetTran("006615", "科目")%></td>
                                        <td style="white-space:nowrap;"><%=GetTran("006581","发生时间")%></td>
                                        <td style="white-space:nowrap;"><%=GetTran("007275","转入金额")%></td>
                                        <td style="white-space:nowrap;"><%=GetTran("001630","转出金额")%></td>
                                        <td style="white-space:nowrap;"><%=GetTran("007276","结余金额")%></td>
                                        <td style="white-space:nowrap;"><%=GetTran("006616","摘要")%></td>
                                    </tr>
                                    <asp:Repeater ID="rep_km" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td style="white-space:nowrap;">
                                                    <%#BLL.Logistics.D_AccountBLL.GetKmtype(Eval("kmtype").ToString()) %>
                                                </td>
                                                <td style="white-space:nowrap;"> 
                                                     <%#Eval("kmtype").ToString()=="0"?DateTime.Parse(Eval("happentime").ToString()):Eval("kmtype").ToString()=="8"? DateTime.Parse(Eval("happentime").ToString()):DateTime.Parse(Eval("happentime").ToString()).AddHours(8)%>
                                            <%--  <%# DateTime.Parse(Eval("happentime").ToString()).AddHours(8)%>--%>
                                                </td>
                                                <td style="white-space:nowrap;">
                                                    <%#Eval("Direction").ToString()=="0"? double.Parse(Eval("happenmoney").ToString()).ToString("0.00"):"" %>
                                                </td>
                                                <td style="white-space:nowrap;">
                                                    <%#Eval("Direction").ToString()=="1"? Math.Abs(double.Parse(Eval("happenmoney").ToString())).ToString("0.00"):"" %>
                                                </td>
                                                <td style="white-space:nowrap;">
                                                    <%#double.Parse(Eval("Balancemoney").ToString()).ToString("0.00")%>
                                                </td>
                                                <td >
                                                    <%#getMark(Eval("remark").ToString())%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td colspan="6" align="right">
                                            <asp:Literal ID="lit_heji" runat="server"></asp:Literal></td>
                                    </tr>
                                </table>
                            </div>
                            <div class="prcSchPage">
                                <table cellspacing="1">
                                    <tr>
                                        <td><%=GetTran("007648", "共找到")%><strong class="required">
                                            <%= ucPagerMb1.RecordCount %></strong><%=GetTran("006978","条")%></td>
                                        <td>
                                            <Uc1:ucPagerMb ID="ucPagerMb1" runat="server" />
                                        </td>
                                        <td>
                                            <div class="btn">
                                              <%--  <div class="btnLeft"></div>--%>
                                                <input name="" type="submit" style=" height:27px; padding-right: 19px; padding-left: 11px;
                                                    background-image: linear-gradient(#addf58,#96c742);
                                                    border: 1px solid #507E0C;" id="jj" class="btnC"  value='<%=GetTran("000434","确定") %>' 
                                                    onclick="javascript: document.getElementById('ucPagerMb1_GoTo').click()" />

                                              <%--  <div class="btnRight"></div>--%>
                                            </div>
                                        </td>
                                    </tr>
                                </table>

                            </div>
                        </div>
                    </div>                 
                </div>
            </div>
            <Uc1:MemberBottom ID="bottom" runat="server" />
        </div>
    </form>
    <script type="text/jscript">
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

</body>
 
</html>
<script type="text/javascript" language="javascript">
    $(document).ready(function () {
        var logintype = '<%=Session["UserType"].ToString() %>';
        if (logintype == "1") {
            $("#cssid").attr("href", "../member/css/products-co.css")
        } else if (logintype == "2") {
            $("#cssid").attr("href", "../member/css/products-use.css")
        } else {
            $("#cssid").attr("href", "../member/css/products.css")
        }
    });
    //自定义rTrim()方法去除字串右侧杂质  
    String.prototype.rTrim = function (Useless) {
        var regex = eval("/" + Useless + "*$/g");
        return this.replace(regex, "");
    }
    $(document).ready(
        function () {
            $('#a_type').click(
            function () {
                var type = document.getElementsByName("chb_name");
                var btnname = "";
                var hidID = "";
                for (var i = 0; i < type.length; i++) {
                    if (type[i].checked) {
                        btnname = btnname + type[i].alt + ",";
                    }
                }
                var btnname1 = btnname.rTrim(',');
                if (btnname1 != "") {
                    $('#btn_kmtype').val(btnname1);
                    $('#btn_kmtype').attr("title", btnname1);
                } else { $('#btn_kmtype').val('<%=GetTran("007650","选择/修改") %>'); }
                $('#div_type').toggle("fast");

            }
        );

            $('#btn_kmtype').click(
                function () {
                    $('#div_type').toggle("fast");
                }
            );
        })


</script>
