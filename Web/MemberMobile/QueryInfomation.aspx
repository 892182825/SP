<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryInfomation.aspx.cs" Inherits="Member_QueryInfomation" EnableEventValidation="false" %>

<%@ Register Src="~/UserControl/MemberBottom.ascx" TagName="MemberBottom" TagPrefix="Uc1" %>
<%@ Register Src="~/UserControl/MemberTop.ascx" TagName="MemberTop" TagPrefix="Uc1" %>
<%@ Register Src="~/UserControl/MemberPager.ascx" TagName="MemberPager" TagPrefix="Uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=11" />

    <title>会员管理系统</title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="css/detail.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>

    <link rel="stylesheet" type="text/css" href="hycss/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="hycss/serviceOrganiz.css" />
    <link rel="stylesheet" type="text/css" href="hycss/jquery.mCustomScrollbar.css" />
    <script src="js/jquery-3.1.1.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="js/bootstrap.js" type="text/javascript" charset="utf-8"></script>
    <script src="js/serviceOrganiz.js" type="text/javascript" charset="utf-8"></script>
    <script src="js/jquery-migrate-1.1.0.min.js" type="text/javascript" charset="utf-8"></script>
    <script type="text/javascript" src="js/jquery.mCustomScrollbar.js"></script>


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
</head>

<%--<body><form id="Form1" method="post" runat="server">
<!--页面内容宽-->
<div class="MemberPage">
<!--顶部信息,logo,help-->
<Uc1:MemberTop ID="Top" runat="server" />


<!--内容部分,左下背景-->
<div class="centerCon-1">
	<!--内容,右下背景-->
	<div class="centConPage">
	  <div class="ctConPgList">
      	<ul>
        	<li><asp:button  id="btnConfirm" runat="server"  Text="搜 索" onclick="btnConfirm_Click" CssClass="anyes" /></li>
            <li><asp:Label runat="server" ID="ldlBeginTime" ><%=GetTran("000720", "发布日期")%>  <%=GetTran("000448", "从")%>：</asp:Label>	</li>
            <li><asp:TextBox ID="txtBeginTime" CssClass="Wdate" runat="server" onfocus="WdatePicker()" ></asp:TextBox></li>
            <li><asp:Label runat="server" ID="lblEndTime" ><%=GetTran("000205", "到")%>：</asp:Label></li>
            <li><asp:TextBox runat="server" ID="txtEndTime" onfocus="WdatePicker()" CssClass="Wdate" ></asp:TextBox></li>
        </ul>
	    
	  </div>

      
      <!--表单-->
      <div class="ctConPgList-1">
      <asp:GridView id="gvMessageSend" runat="server"  AllowSorting="True" width="100%" border="0" cellspacing="1" cellpadding="0" AutoGenerateColumns="False" onpageindexchanged="gvMessageSend_PageIndexChanged" onrowdatabound="gvMessageSend_RowDataBound" onrowcommand="gvMessageSend_RowCommand" >
        <HeaderStyle CssClass="ctConPgTab" />
        <RowStyle HorizontalAlign="Center"  Wrap="false" />	
        <Columns>		     
		    <asp:TemplateField HeaderText="公告标题" ItemStyle-HorizontalAlign=Center>
            <ItemTemplate >
                <%#GetStr(Eval("InfoTitle").ToString())%>
                </ItemTemplate>
            </asp:TemplateField>
    	     							
		    <asp:BoundField DataField="Sender" HeaderText="发送人编号" />
		    <asp:TemplateField HeaderText="发布日期" >
                <ItemTemplate>
                    <asp:Label ID="fbsj" runat="server" Text='<%#GetBiaoZhunTime(Eval("Senddate").ToString())%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="查看详细">
                <ItemTemplate>
                    <asp:ImageButton CommandArgument='<%#Eval("ID") %>' ID="LinkButton1" CommandName="GO" runat="server" ImageUrl="images/view-button.png" />
                </ItemTemplate>
            </asp:TemplateField>
	    </Columns>
	    <EmptyDataTemplate>
	        <table width="100%" cellspacing="0">
	            <tr class="ctConPgTab">
	              <th><%=GetTran("000724", "公告标题")%></th>
			      <th><%=GetTran("000726", "发送人编号")%></th>
			      <th><%=GetTran("000727", "发送日期")%></th>										            
	            </tr>
	        </table>
	    </EmptyDataTemplate>	
      </asp:GridView>
      </div>  
     <Uc1:MemberPager ID="Pager1" runat="server" />
      

      
	</div>
</div>



<!--版权信息-->

<Uc1:MemberBottom ID="Bottom" runat="server" />
<!--结束-->
</div>
 <script type="text/jscript">
      $(function () {
          $('#bottomDiv').css('display', 'none');
      

      })

  </script>
</form>
</body>--%>
<body>
    <form id="Form1" method="post" runat="server">
        <Uc1:MemberTop ID="Top" runat="server" />
         <div class="rightArea clearfix">
        <div class="rightAreaIn">
            <div class="fiveSquareBox clearfix searchFactor">
                <span><%=GetTran("000838", "查询条件")%>：</span>
                <ul>
                    <li style="float: left; margin-right: 10px; margin-top: -3px;">
                        <asp:Button ID="btnConfirm" runat="server" Height="27px" Width="47px" Style="margin-top: 1px; margin-left: 5px; padding: -1px 9px; background-color: #96c742; color: #FFF; border: 1px solid #507E0C; background-image: linear-gradient(#addf58,#96c742); text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.4);"
                            Text="搜 索" CssClass="anyes" OnClick="btnConfirm_Click" />

                    </li>
                    <li style="float: left; margin-right: 10px;">
                        <asp:Label runat="server" ID="ldlBeginTime"><%=GetTran("000720", "发布日期")%>  <%=GetTran("000448", "从")%>：</asp:Label>
                    </li>
                    <li style="float: left">
                        <asp:TextBox Style="width: auto; height: auto" ID="txtBeginTime" CssClass="Wdate" runat="server" onfocus="WdatePicker()"></asp:TextBox></li>
                    <li style="float: left">
                        <asp:Label runat="server" ID="lblEndTime"><%=GetTran("000205", "到")%>：</asp:Label></li>
                    <li style="float: left">
                        <asp:TextBox Style="width: auto; height: auto" runat="server" ID="txtEndTime" onfocus="WdatePicker()" CssClass="Wdate"></asp:TextBox></li>
                </ul>
            </div>
           
            <div class="noticeEmail width100per mglt0">
                <div class="pcMobileCut">
                    <div class="noticeHead" style="background: #019c87; margin: 0 2px; border-radius: 4px 4px 0 0;">
                        <div>
                            <i class="glyphicon glyphicon-file"></i>
                            <span><%=GetTran("004028", "公告查阅")%></span>
                        </div>
                    </div>
                    <div class="noticeBody">
                        <div class="tableWrap clearfix table-responsive">
                            <table class="table-bordered noticeTable">
                                <tbody>
                                    <tr>
                                        <td style="padding: 0">
                                            <asp:GridView CssClass="tablemb bordercss"
                                                ID="gvMessageSend" runat="server" AllowSorting="True" Width="100%"
                                                AutoGenerateColumns="False" OnPageIndexChanged="gvMessageSend_PageIndexChanged"
                                                OnRowDataBound="gvMessageSend_RowDataBound" OnRowCommand="gvMessageSend_RowCommand">
                                                <HeaderStyle Wrap="false" CssClass="tablemb" />
                                                <AlternatingRowStyle BackColor="#F1F4F8" />
                                                <RowStyle HorizontalAlign="Center" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="公告标题" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <%#GetStr(Eval("InfoTitle").ToString())%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="Sender" HeaderText="发送人编号" />
                                                    <asp:TemplateField HeaderText="发布日期">
                                                        <ItemTemplate>
                                                            <asp:Label ID="fbsj" runat="server" Text='<%#GetBiaoZhunTime(Eval("Senddate").ToString())%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="查看详细">
                                                        <ItemTemplate>
                                                            <asp:ImageButton CommandArgument='<%#Eval("ID") %>' ID="LinkButton1" CommandName="GO" runat="server" Style="width: auto; height: auto" ImageUrl="images/view-button.png" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <table width="100%" cellspacing="0">
                                                        <tr class="ctConPgTab">
                                                            <th><%=GetTran("000724", "公告标题")%></th>
                                                            <th><%=GetTran("000726", "发送人编号")%></th>
                                                            <th><%=GetTran("000727", "发送日期")%></th>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                            <Uc1:MemberPager ID="Pager1" runat="server" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>

                            <Uc1:MemberBottom ID="Bottom" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
             </div>
    </form>
    <script type="text/jscript">
        $(function () {
            $('#bottomDiv').css('display', 'none');


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
    </style>

</body>

</html>
