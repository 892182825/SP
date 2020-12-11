<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WriteEmail.aspx.cs" Inherits="Member_WriteEmail" ValidateRequest="false"%>
<%@ Register Src="~/UserControl/MemberBottom.ascx" TagName="MemberBottom" TagPrefix="Uc1" %>
<%@ Register Src="~/UserControl/MemberTop.ascx" TagName="MemberTop" TagPrefix="Uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<%--<meta http-equiv="x-ua-compatible" content="ie=7" />--%>
    
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no">

<title></title>
<link href="css/style.css" rel="stylesheet" type="text/css" />
<link href="css/detail.css" rel="stylesheet" type="text/css" />
<link href="css/cash.css" rel="stylesheet" type="text/css" />
<script charset="utf-8" src="../javascript/kindeditor/kindeditor.js"></script>
<script charset="utf-8" src="../javascript/kindeditor/lang/zh_CN.js"></script>
<script charset="utf-8" src="../javascript/kindeditor/plugins/code/prettify.js"></script>
<script type="text/javascript">
    KindEditor.ready(function(K) {
        var editor1 = K.create('#content1', {
            cssPath: '../javascript/kindeditor/plugins/code/prettify.css',
            uploadJson: '../javascript/kindeditor/asp.net/upload_json.ashx',
            fileManagerJson: '../javascript/kindeditor/asp.net/file_manager_json.ashx',
            allowFileManager: true,
            filterMode: true,
            afterCreate: function() {
                var self = this;
                K.ctrl(document, 13, function() {
                    self.sync();
                    K('form[name=example]')[0].submit();
                });
                K.ctrl(self.edit.doc, 13, function() {
                    self.sync();
                    K('form[name=example]')[0].submit();
                });
            }
        });
        prettyPrint();
    });
</script>

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
        .ctConPgCash {
            margin:0;
        }
        #btnSave,#btnCancle {
            padding:5px 20px;
            background:#77c225;
            border-radius:3px;
            color:#fff;
        }
        #btnCancle {
            background:#999;
            color:#fff
        }
    </style>
</head>

<body>

<form id="form2" runat="server">
    <div class="t_top">	
            	<a class="backIcon" href="javascript:history.go(-1)"></a>
     <%=GetTran("003104","写邮件")%>
            </div>
	  <!--表单-->
      <div class="ctConPgCash" style="width:auto;height:auto">
        <table  style="" width="100%" border="0" cellspacing="1" cellpadding="0">
          <tr>
            <th width=""><%=GetTran("000826", "收件人员")%>：</th>
            <td width="">
            <asp:dropdownlist CssClass="ctConPgFor" id="drop_LoginRole" AutoPostBack="True" Runat="server" Enabled="False" Visible="False">
					<asp:ListItem Value="0" Selected="True">管理员</asp:ListItem>
				</asp:dropdownlist>
				<asp:DropDownList id="txtNumber"  CssClass="ctConPgFor" runat="server" Width="" Enabled="False"></asp:DropDownList>
            </td>
          </tr>
          <tr>
            <th><%=GetTran("007398", "邮件分类")%>：</th>
            <td>
                <asp:RadioButtonList ID="RadioListClass" runat="server"  
                    RepeatDirection="Horizontal" RepeatLayout="Flow">
                </asp:RadioButtonList>
            </td>
          </tr>
          <tr>
            <th><%=GetTran("000825", "信息标题")%>：</th>
            <td><asp:TextBox id="txtTitle" CssClass="ctConPgTxt" Runat="server" Width="" MaxLength="100" TextMode="SingleLine" style="margin-top:5px;"></asp:textbox><FONT color="#ff0000" style="display:block">
                *<asp:Label Runat=server ForeColor=red id=Label1><%=GetTran("001245", "标题限制在100字以内")%></asp:Label></FONT></td>
          </tr>
	    <tr>
			<td colspan="2" style="padding-left:0px;">
               <div class="editor">
                   <textarea id="content1" class="ctConPgTxt2" rows="8"  runat="server" style="VISIBILITY: hidden; WIDTH:100%; HEIGHT: 350px" name="content"></textarea>
			   </div>					
			</td>						
		</tr>
		</table>
        
        <ul>
        	<li><asp:button id="btnSave" Runat="server" Text="发 布" onclick="btnSave_Click" CssClass="anyes"  /><asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click"></asp:LinkButton></li>
            <li><asp:Button id="btnCancle" Runat="server" Text="取 消" onclick="btnCancle_Click" CssClass="anyes" /></li>
        </ul>
     <!-- #include file = "comcode.html" -->
</div>
    

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


