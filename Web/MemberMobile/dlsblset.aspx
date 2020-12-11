<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dlsblset.aspx.cs" Inherits="MemberMobile_dlsblset" %>

<!doctype html>
<html>
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
    <script src="js/jquery-1.7.1.min.js"></script>
    <title><%=GetTran("000000","比例设置")%></title>
    <link rel="stylesheet" href="CSS/style.css">
    <link href="hycss/serviceOrganiz.css" rel="stylesheet" />
    <script type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>

 
    <style>
        body {
            padding: 50px 2% 60px;
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
    
</head>

<body>
    <b id="lang" style="display:none"><%=Session["LanguageCode"] %></b>
 
    <form id="form2" runat="server">
       <div class="t_top">	
            	<a class="backIcon" href="javascript:history.go(-1)"></a>
     <%=GetTran("000000","比例设置")%>
            </div>
        <div class="middle">
            <div class="changeBox zcMsg">
                <ul>

                    
                   
                    <li>
                        <div class="changeLt"><%=GetTran("000024", "会员编号")%>：</div>
                        <div class="changeRt">
                            <asp:TextBox id="txtbh" CssClass="ctConPgTxt" Runat="server" Width="90%" MaxLength="100" TextMode="SingleLine"></asp:textbox>
                        </div>
                    </li>
                    <li>
                        <div class="changeLt"><%=GetTran("000000", "比例")%>：</div>
                        <div class="changeRt">
                            <asp:TextBox id="txtBl" CssClass="ctConPgTxt" Runat="server" Width="90%" MaxLength="100" TextMode="SingleLine"></asp:textbox>
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
                        <%--</div>--%>
                        <asp:Literal ID="liter" runat="server"></asp:Literal>
                       
                        <%-- <div class="editor">
                        <textarea id="content1" class="ctConPgTxt2" rows="8"  runat="server" style="VISIBILITY: hidden; WIDTH: 705px; HEIGHT: 350px" name="content"></textarea>
			            </div>	
                    </li>--%>
                </ul>

                
                <div class="zzBox" id="zzBox" style="margin-top: 20px;">
                    <ul>
                        <li>
                            <asp:Button ID="btnSave" runat="server" Text="提 交" OnClick="sub_Click" Height="35px" Width="93%" Style="margin-top: 1px; border-radius: 5px; margin-left: 12px; padding: -1px 9px; background-color: #96c742; color: #FFF; border: 1px solid #507E0C; background-image: linear-gradient(#addf58,#96c742); text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.4);" CssClass="anyes" />
                            <%--  <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" Height="35px" Width="93%" Style="margin-top: 1px; border-radius: 5px; margin-left: 12px; padding: -1px 9px; background-color: #96c742; color: #FFF; border: 1px solid #507E0C; background-image: linear-gradient(#addf58,#96c742); text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.4);"></asp:LinkButton></li>--%>
                    </ul>

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

            })
        </script>
    </form>
</body>
</html>


