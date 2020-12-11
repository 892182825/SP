<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OnlinePayment.aspx.cs" Inherits="Member_OnlinePayment" %>

<%@ Register Src="~/UserControl/MemberBottom.ascx" TagName="MemberBottom" TagPrefix="Uc1" %>
<%@ Register Src="~/UserControl/MemberTop.ascx" TagName="MemberTop" TagPrefix="Uc1" %>

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
<script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
<script language="javascript" type="text/javascript" src="../js/SqlCheck.js"></script>
    <script src="js/jquery-1.7.1.min.js"></script>
    <link rel="stylesheet" href="css/style.css">
<script language="javascript" type="text/javascript">
    function CheckText(btname) {
        //这个方法是页面有多个按钮要提交时，多次使用这个方法,传入按钮的ID
        filterSql_II(btname);
    }
</script> 
    <style>
        .changeBox ul li .changeLt {
            width: 30%;
        }

        .changeBox ul li .changeRt {
            width: 70%;
        }

            .changeBox ul li .changeRt .textBox {
                width: 80%;
            }

        .zcMsg ul li .changeRt .zcSltBox {
            width: 80%;
        }

        .zcMsg ul li .changeRt .zcSltBox2 {
            width: 39%;
        }

        #txtadvpass {
            width: 79%;
            border: 1px solid #ccc;
        }

       

        .moneyInfo3 a {
            float: left;
            width: 25%;
            text-align: center;
            height: 30px;
            font-size: 16px;
            line-height: 30px;
        }
        .moneyInfoSlt {
            background:forestgreen;
            color:#fff;
        }
        #Button1 {
            display: block;
            height: 28px;
            line-height: 30px;
            text-align: center;
            background: #85ac07;
            width: 58px;
            border-radius: 5px;
            color: #fff;
            font-size: 14px;
            margin-left: 51%;
            margin-top: 11%;
        }

        #Button2 {
            display: block;
            height: 28px;
            line-height: 30px;
            text-align: center;
            background: #85ac07;
            width: 58px;
            border-radius: 5px;
            color: #fff;
            font-size: 14px;
            margin-left: 238%;
            margin-top: 11%;
        }

        #Button3 {
            display: block;
            height: 28px;
            line-height: 30px;
            text-align: center;
            background: #85ac07;
            width: 58px;
            border-radius: 5px;
            color: #fff;
            font-size: 14px;
            margin-left: 141%;
            margin-top: 11%;
        }

        #sub {
             display: block;
    float: left;
    height: 30px;
    line-height: 35px;
    text-align: center;
    background: #85ac07;
    width: 22%;
    border-radius: 5px;
    color: #fff;
    font-size: 14px;
    margin-left: 10%;
        }
    </style>

    <script type="text/javascript">
        $(function () {
            var lang = $("#lang").text();
            if (lang != "L001") {
                //alert("OnlinePayment");
                $('.changeBox ul li .changeRt').width('60%')
                $('.changeBox ul li .changeLt').width('40%').css('font-size','14px')
            }
        })
       
    </script>
</head>

<body>
<!--页面内容宽-->
<form id="form1" runat="server" name="form1" method="post" >
    <b style="display:none" id="lang"><%=Session["LanguageCode"] %></b>
     	<div class="navbar navbar-default" role="navigation">
       <div class="navbar-inner">	
            	<a class="btn btn-primary btn-lg" style="float: left;padding:6px;text-shadow: 2px 2px 5px hsl(0, 0%, 61%);" href="first.aspx"><i class="glyphicon glyphicon-chevron-left glyphicon-white"></i></a>
            
                <span style="color:#fff;font-size:18px;margin-left:35%;text-shadow: 2px 2px 5px hsl(0, 0%, 61%);font-weight: 600;" > 账户充值</span>
            </div>
              </div>
    <div class="moneyInfo3" style="left:0px;overflow:hidden;zoom:1;width:100%;background-color:#fff">
            <a href="OnlinePayment.aspx" class="moneyInfoSlt">充值</a>
            <a href="DetailDHK.aspx">待汇出</a>
            <a href="DetailDCS.aspx">已汇出</a>
            <a href="DetailYDZ.aspx">已到账</a>
        </div>
    <div class="middle">
        <div class="changeBox zcMsg" style="width: 95%; margin-left: 10px; margin-top: 10px;">
            <ul>
                
               <li style="display:none">
                    <div class="changeLt"><%=GetTran("000024", "会员编号")%>：  </div>
                    <div class="changeRt"><asp:Literal runat="server"  ID="Number"></asp:Literal> </div>

               </li>
                <li>
                    <div style="font-size:18px" class="changeLt">充值金额：</div>
                    <div class="changeRt">
                        <asp:TextBox ID="Money" style="width: 39%;float:left;height: 30px;" placeholder="复投金额" CssClass="textBox" runat="server"
                            MaxLength="50"></asp:TextBox>

                           <asp:Button ID="sub" runat="server" Text="提 交"
                            CssClass="changeBtn" OnClick="sub_Click"  OnClientClick="return abc();"/>
                           <%--<input type="button" id="sub"  name="sub" value="提 交" onclick="popDiv()" />--%>

                        <input type="hidden" value="0" id="hid_fangzhi" runat="server" />
                    </div>
                  
                </li>

            </ul>
        </div>


      
   <!-- #include file = "comcode.html" -->
  
    </div>
</form>
</body>
</html>
<script type="text/javascript">

    //function popDiv() {
    
    //    window.location.href = "/MemberMobile/OnlinePayQD.aspx";
    //}


</script>

<script type="text/jscript">
        $(function () {
            $('#bottomDiv').css('display', 'none');

            $("#Pager1_div2").css('background-color','#FFF')
        })
        
    </script>

<script type="text/javascript">
    function abc() {
        if (window.confirm('<%=GetTran("007587","您确定要充值吗") %>？')) {
            var hid = document.getElementById("hid_fangzhi").value;
            if (hid == "0") {
                document.getElementById("hid_fangzhi").value = "1";
            } else {
                alert('<%=GetTran("007387","不可重复提交!") %>');
                return false;
            }
        } else { return false; }
    }
</script>