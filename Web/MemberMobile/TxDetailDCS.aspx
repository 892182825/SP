<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="TxDetailDCS.aspx.cs" Inherits="MemberMobile_TxDetailDCS" %>
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
        function Page_Load() {
            var bankcard = $("#bc").val();
            $("#Label1").text = "....." + bankcard;
        
        }
    </script>

    <script type="text/javascript">
        function abc() {
            if (confirm('<%=GetTran("009042","您确定要点击已收到吗？") %>')) {
                var hid = document.getElementById("hid_fangzhi").value;
                if (hid == "0") {
                    document.getElementById("hid_fangzhi").value = "1";
                } else {
                    alert('<%=GetTran("007387","不可重复提交！") %>');
                return false;
            }
        } else { return false; }
    }
    </script>
 <script type="text/javascript">
        function bcd(v) {
            var id = v.title;
            if (confirm('<%=GetTran("009066","您确定要进行解释吗？") %>')) {
                window.location.href = "TxJS.aspx?id=" + id;
                return false;
                
              } else { return false; }  
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

        .moneyInfo3 a {
            float: left;
            width: 25%;
            text-align: center;
            height: 30px;
            font-size: 16px;
            line-height: 30px;
        }

        .moneyInfoSlt {
            background: forestgreen;
            color: #fff;
        }

        #Button1 {
            /* display: block; */
            height: 25px;
            line-height: 25px;
            text-align: center;
            background: #85ac07;
            width: 64px;
            border-radius: 5px;
            color: #fff;
            font-size: 14px;
            /*margin-left: 34%;*/
            /* margin-top: 11%; */
            position: absolute;
             right: 23%;
             top: 4px;
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
            height: 35px;
            line-height: 35px;
            text-align: center;
            background: #85ac07;
            width: 100%;
            border-radius: 5px;
            color: #fff;
            font-size: 14px;
        }

    </style>


</head>

<body>
    <!--页面内容宽-->
    <form id="form1" runat="server" name="form1" method="post">

        <div class="t_top">
            <a class="backIcon" href="javascript:history.go(-1)"></a>
               <%=GetTran("007290","提现申请")%>
         

        </div>
        <div class="moneyInfo3" style="left: 0px; overflow: hidden; zoom: 1; width: 100%; background-color: #fff">
              <a href="MemberCash.aspx"><%=GetTran("005873","提现") %></a>
            <a href="TxDetailDHK.aspx" ><%=GetTran("8143  ","待汇入") %></a>
            <a href="TxDetailDCS.aspx" class="moneyInfoSlt" ><%=GetTran("8139  ","待查收") %></a>
            <a href="TXDetailYDZ.aspx"><%=GetTran("007371","已到账") %></a>
          
        </div>
         <div class="minMsg minMsg2" style="display: block">
             <div class="minMsgBox">   
                 <div>
                    <ol>
                        <asp:Repeater ID="rep_km" runat="server" OnItemCommand="rep_km_ItemCommand" OnItemDataBound="rep_km_ItemDataBound">
                            <ItemTemplate>
                                <li style="width: 95%; margin-left: 10px; font-size: 15px;line-height: 30px;">
                                    <div>
                                        <div style="color: red; font-size: 19px;position:relative;  ">
                                            <asp:LinkButton ID="Button1" OnClientClick="return abc();" CommandName="yidz" CommandArgument='<%# Eval("id")%>' Style="height: 25px; line-height: 25px; text-align: center; background: #77c225; width: 56px; border-radius: 5px; color: #fff; font-size: 14px; position: absolute;  top: 4px;"
                                              runat="server" Text="已收到" />
                              
                                            <div style="margin-left: 20%;">
                                                <%=huilv==1?"$":"￥"%>
                                                <%#  huilv==1?Math.Round((double.Parse(Eval("Ppje").ToString()))).ToString("#0.00"):Math.Round((double.Parse(Eval("Ppje").ToString())*huilv)).ToString("#0.00")  
                                                %> 
                                            </div>
                                            <asp:LinkButton ID="Button2"   CommandName="Hfxx" CommandArgument='<%# Eval("id")%>' Style="height: 25px; line-height: 25px; text-align: center; background: #00c7db; width: 70px; border-radius: 5px; color: #fff; font-size: 14px; position: absolute; right: 21%; top: 4px;"
                                              runat="server" Text="汇方信息" />

                                            <%--<input type="button" style=" height: 25px; line-height: 27px; text-align: center; background: #ccc; width: 50px;  border-radius: 5px; color: #fff; font-size: 14px;position: absolute; right: 5%;top: 4px;"  id="jieshi" value="解释" />--%>

                                            <asp:Button ID="jieshi" runat="server" Text="解释" style=" height: 25px; line-height: 27px; text-align: center; background: #ccc; width: 50px;  border-radius: 5px; color: #fff; font-size: 14px;position: absolute; right: 5%;top: 4px;"  />
                                            <asp:Button ID="jieshi1" Visible="false" runat="server" Text="解释" ToolTip='<%# Eval("id")%>' OnClientClick="return bcd(this);"  style=" height: 25px; line-height: 27px; text-align: center;  background: red; width: 50px;  border-radius: 5px; color: #fff; font-size: 14px;position: absolute; right: 5%;top: 4px;"  />
                                            <asp:Button ID="jieshi2" runat="server" Visible="false" Text="解释成功" style=" height: 25px; line-height: 27px; text-align: center; background: #ccc; width: 62px;  border-radius: 5px; color: #fff; font-size: 14px;position: absolute; right: 2%;top: 4px;"  />
                                            <asp:HiddenField ID="HiddenField1" Value='<%# Eval("hkid") %>' runat="server" />
                                            <asp:HiddenField ID="HiddenField2" Value='<%# Eval("id") %>' runat="server" />
                                        </div>
                                        <div style="color: red; font-size: 16px;position: absolute; right: 5%;top: 90px; ">
                             
                                        </div>
                                        <div style="font-size: 18px;">
                                          <%#Eval("Khname")%>,<%#Eval("bankname")%>, .....<%#Eval("bankcard").ToString().Length>5 ? Eval("bankcard").ToString().Substring(Eval("bankcard").ToString().ToString().Length - 5) : Eval("bankcard").ToString()%>
                                        </div>
                                        <asp:Panel ID="hkpzid" runat="server" style="font-size: 18px;">

                                            <asp:HiddenField ID="HiddenField3" Value='<%# Eval("hkpzImglj") %>' runat="server" />
                                            <asp:Button ID="Button3" runat="server"  Text="查看汇款凭证" style=" height: 25px; line-height: 27px; text-align: center; background: #85ac07; width: 98%;  border-radius: 5px; color: #fff; font-size: 14px;" CommandName="sc" CommandArgument='<%# Eval("id")%>'  />
                                        </asp:Panel>
                                        
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ol>
                </div>
                <%--<input type="hidden" id="bc" runat="server" value="" />--%>
                <input type="hidden" value="0" id="hid_fangzhi" runat="server" />
            </div>
        </div>
        <input type="hidden"  id="Hkid" runat="server"/>
       <!-- #include file = "comcode.html" -->

    </form>
</body>
</html>
<script type="text/jscript">
    $(function () {
        $('#bottomDiv').css('display', 'none');

        $("#Pager1_div2").css('background-color', '#FFF')
    })

    //function popDiv() {

    //    window.location.href = "/MemberMobile/OnlinePayQD.aspx";
    //}
</script>


