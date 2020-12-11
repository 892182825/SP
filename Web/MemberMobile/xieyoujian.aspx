<%@ Page Language="C#" AutoEventWireup="true" CodeFile="xieyoujian.aspx.cs" Inherits="MemberMobile_xieyoujian" %>

<!doctype html>
<html>
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
    <script src="../bower_components/jquery/jquery.min.js"></script>
    <title><%=GetTran("003104","写邮件")%></title>
    <link rel="stylesheet" href="CSS/style.css">
   
    <%--<script type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>--%>

       <script type="text/javascript">
        function abc() {
            if (confirm('<%=GetTran("007386","确定转入！") %>')) {
                var hid = document.getElementById("hid_fangzhi").value;
                if (hid == "0") {
                    document.getElementById("hid_fangzhi").value = "1";
                } else {
                alert('<%=GetTran("007387","不可重复提交！") %>');
                    return false;
                }
            } else { return false; }
        }
        function getname() {
            var number = document.getElementById("txt_InNumber").value;
            var rad_Inzh = document.getElementsByName("rad_Inzh");
            var rad = 0;
            for (i = 0; i < rad_Inzh.length; i++) {
                if (rad_Inzh[i].checked) {
                    if (rad_Inzh[i].value == "2") { var name = AjaxClass.GetPetName(number, "Store").value; } else {
                        var name = AjaxClass.GetPetName(number, "Member").value;
                    }
                    document.getElementById("lab_nicheng").innerHTML = name;
                }
            }
        }
    </script>
  <script type="text/jscript">
        $(function () {
            $('#bottomDiv').css('display', 'none');
        })

    </script>

    <script type="text/jscript">
        $(function () {
            $('#rad_Outzh label').css({ 'float': 'left', 'margin-top': '1px'});
            $('#rad_Outzh').css({ 'width': '19%', 'margin-left': '5px'});
            $('#rad_Outzh input').css({ border: 0, 'margin': '2px', 'float': 'left' });

            $('#RadioListClass label').css({ 'float': 'left' });
            $('#RadioListClass input').css({ 'float': 'left' });


            $('#rad_xianjin').css({ 'float': 'left' });
            $('#rad_xiaofei').css({ 'float': 'left' });
            $('#rad_fuwujigou').css({ 'float': 'left' });



            $('#aa').css({ 'float': 'left' });
            $('#bb').css({ 'float': 'left' });
            $('#cc').css({ 'float': 'left' });

        })
    </script>
    <script type="text/javascript">
        $(function () {
            a.dianji();
        })
        var a = {
            dianji: function () {
                $(".DD").on("click", function () {
                    location.href = "/MemberDateUpdatePhone/Index?";
                })
            },
        }

    </script>
    <style>
        
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
    <script type="text/javascript" >
        $(function () {
            var lang = $("#lang").text();
            if (lang != "L001") {
                //alert("OnlinePayment");
                $('.changeBox ul li .changeRt').width('55%')
                $('.changeBox ul li .changeLt').width('45%').css('font-size', '12px')
            }
        })

 </script> 
</head>

<body>
    <b id="lang" style="display:none"><%=Session["LanguageCode"] %></b>
 
    <form id="form2" runat="server">
       <div class="navbar navbar-default" role="navigation">
            <div class="navbar-inner">
                <a class="btn btn-primary btn-lg" style="float: left; padding: 6px; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);" href="javascript:history.go(-1)"><i class="glyphicon glyphicon-chevron-left glyphicon-white"></i></a>

                <span style="color: #fff; font-size: 18px; margin-left: 30%; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);">发送邮件</span>
            </div>
        </div>
        <div class="middle">
            <div class="changeBox zcMsg">
                <ul>

                    <li>
                        <div class="control-group">
                        <div class="changeLt"><%=GetTran("000826", "收件人员")%>：</div>
                        <div class="controls">
                            <asp:DropDownList CssClass="selectError" ID="drop_LoginRole" AutoPostBack="True" runat="server" Enabled="false" Visible="false">
                                <asp:ListItem Value="0" Selected="True">管理员</asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="txtNumber" CssClass="selectError" data-rel="chosen" runat="server" Width="152px" Height="40px" style=" border: 1px solid #ccc; border-radius: 5px;" Enabled="true"></asp:DropDownList>
                        </div>
                            </div>
                    </li>
                    <li style="display:none;">
                        <div class="changeLt"><%=GetTran("007398", "邮件分类")%>：</div>
                        <div class="changeRt">
                            <asp:RadioButtonList ID="RadioListClass" runat="server"
                                RepeatDirection="Horizontal" RepeatLayout="Flow">
                            </asp:RadioButtonList>
                        </div>
                        
                    </li>
                    <li>
                        <div class="changeLt"><%=GetTran("000825", "信息标题")%>：</div>
                        <div class="changeRt">
                            <asp:TextBox id="txtTitle" CssClass="form-control" onkeydown="ValidateValue(this)" onblur="ValidateValue(this)" onkeyup="ValidateValue(this)"  Runat="server" Width="90%" MaxLength="30" TextMode="SingleLine"></asp:textbox>
                        </div>
                    </li>
                    <li >
                        <div class="changeLt"><%=GetTran("9110", "邮件内容")%>：</div>
                        <div class="changeRt">
                            <asp:TextBox ID="content1"  TextMode="MultiLine" onkeydown="ValidateValue(this)" onblur="ValidateValue(this)" onkeyup="ValidateValue(this)"     runat="server"  Style="border: 1px solid #ccc;" Width="90%" Height="150px" MaxLength="100" />
                        </div>
                        <%-- <div class="editor">
                        <textarea id="content1" class="ctConPgTxt2" rows="8"  runat="server" style="VISIBILITY: hidden; WIDTH: 705px; HEIGHT: 350px" name="content"></textarea>
			            </div>--%>	
                    </li>

                    <%--<li>
                        <div class="changeLt"><%=GetTran("0000", "上传图片")%>：</div>
                        <div class="changeRt" >
                            <span > <asp:FileUpload runat="server" ID="uppic" Width="70%" /> </span>
                           <%--<asp:Button  ID="btnUp"  runat="server" Text="上传" OnClick="btnUp_Click"  Height="35px" Width="15%" Style="margin-top: 1px; border-radius: 5px;  padding: -1px 9px; background-color: #96c742; color: #FFF; border: 1px solid #507E0C; background-image: linear-gradient(#addf58,#96c742); text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.4);"  />--%>
                        </div>
                        <asp:Literal ID="liter" runat="server"></asp:Literal>
                       
                        <%-- <div class="editor">
                        <textarea id="content1" class="ctConPgTxt2" rows="8"  runat="server" style="VISIBILITY: hidden; WIDTH: 705px; HEIGHT: 350px" name="content"></textarea>
			            </div>	
                    </li>--%>
                </ul>

                
                
                            <asp:Button ID="btnSave" runat="server" Text="发 布" OnClick="btnSave_Click" Height="40px" Width="100%"  CssClass="btn btn-primary btn-lg" />
                            <%--  <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" Height="35px" Width="93%" Style="margin-top: 1px; border-radius: 5px; margin-left: 12px; padding: -1px 9px; background-color: #96c742; color: #FFF; border: 1px solid #507E0C; background-image: linear-gradient(#addf58,#96c742); text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.4);"></asp:LinkButton></li>--%>
                    

                </div>
            </div>
            <input type="hidden" value="0" id="hid_fangzhi" runat="server" />
            <asp:HiddenField ID="HiddenField1" runat="server" />
        </div>
    <!-- #include file = "comcode.html" -->

        <script type="text/javascript">
            $(function () {
                $('.mailbtn').on('click', function () {
                    $(this).addClass('mailSlt').siblings('.mailbtn').removeClass('mailSlt');
                    var Mindex = $(this).index();
                    $('.minMsg').eq(Mindex).show().siblings('.minMsg').hide();

                })

            });
            $(function () {
                $(".glyphicon").removeClass("a_cur");
                $("#c5").addClass("a_cur");
            });
        </script>

         
    </form>
</body>
</html>

