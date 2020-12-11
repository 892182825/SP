<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DownLoadFiles.aspx.cs" Inherits="Member_DownLoadFiles" EnableEventValidation="false" %>

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
    <script type="text/javascript" src="../js/SqlCheck.js"></script>
</head>

<body>
<form id="form1" method="post" runat="server" onsubmit="return filterSql_III()">
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
        	<li><%--<asp:Button ID="btnSearch" Text="搜 索" CssClass="anyes" runat="server" onclick="btnSearch_Click" />--%>
                   <asp:Button ID="btnSearch" runat="server" Height="27px" Width="47px" Style="margin-left: 17px; padding: 2px 9px; color: #FFF; border: 1px solid #507E0C;background: #507E0C;background-image: linear-gradient(#addf58,#96c742); text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.4);"
                        Text="搜 索" CssClass="anyes" onclick="btnSearch_Click"/>
        	</li>
            <li><%=GetTran("000204","资料名称") %>：<asp:textbox CssClass="ctConPgTxt" id="txt_member" runat="server" ></asp:textbox></li>
            <li><%=GetTran("000213","文件名称") %>：</li>
            <li><asp:textbox id="Txt_Name" runat="server" CssClass="ctConPgTxt" ></asp:textbox></li>
        </ul>
	  </div>

      <!--表单-->
      <div class="ctConPgList-1">
      <asp:GridView id="gvResources" runat="server"  AutoGenerateColumns="False" width="100%" border="0" cellspacing="1" cellpadding="1" onrowcommand="gvResources_RowCommand" onrowdatabound="gvResources_RowDataBound">
        <HeaderStyle CssClass="ctConPgTab" />
        <RowStyle HorizontalAlign="Center"  Wrap="false" />
        <Columns>
        
		    <asp:TemplateField HeaderText="下载">											
				<ItemTemplate>	
				    <asp:ImageButton ID="lbutDownload" ImageUrl="images/download.png" runat="server" CommandName="download" CommandArgument='<%#Eval("ResID") %>' Font-Underline="True" />
				</ItemTemplate>											
			</asp:TemplateField>											
			<asp:BoundField Visible="False" DataField="ResID" HeaderText="资料编号" />
			<asp:BoundField DataField="ResName" HeaderText="资料名称" />
			<asp:BoundField DataField="FileName" HeaderText="对应文件名" />
			<asp:BoundField DataField="ResDescription" HeaderText="资料简介" />
			<asp:BoundField DataField="ResSize" HeaderText="文件大小" />
			<asp:BoundField DataField="ResTimes" HeaderText="下载次数" />
		</Columns>
		<EmptyDataTemplate>
		    <table width="100%" cellspacing="0">
		        <tr class="ctConPgTab">
		            <th><%=GetTran("000245","下载")%></th>
		            <th><%=GetTran("000272", "资料编号")%></th>
		            <th> <%=GetTran("000204","资料名称")%></th>
		            <th><%=GetTran("000278","对应文件名")%> </th>
		            <th><%=GetTran("000280","资料简介")%></th>
		            <th> <%=GetTran("000282","文件大小")%> </th>
		            <th> <%=GetTran("000287", "下载次数")%></th>										     
		        </tr>										    
		    </table>										
		</EmptyDataTemplate>	
      </asp:GridView> 
      </div>
      <div><asp:label id="label1" Runat="server"></asp:label></div>
   <Uc1:MemberPager ID="Pager1" runat="server" />
      
      <div class="ctConPgList-3">
      	<h1><%=GetTran("000649", "")%>：</h1>
        <p>	<%=GetTran("006878", "")%></p>
      </div>
      
	</div>
</div>
<!--页面内容结束-->
<!--版权信息-->
<Uc1:MemberBottom ID="Bottom" runat="server" />
<!--结束-->
</div>
       <script type="text/jscript">
        $(function () {
            $('#bottomDiv').css('display', 'none');

            $("#Pager1_div2").css('background-color','#FFF')
        })
        
    </script>
</form>
</body>
</html>