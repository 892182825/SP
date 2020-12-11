<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryLinkNetworkView.aspx.cs" Inherits="Member_QueryLinkNetworkView" %>
<%@ Register Src="~/UserControl/MemberTop.ascx" TagName="top" TagPrefix="uc1"  %>
<%@ Register Src="~/UserControl/MemberBottom.ascx" TagName="bottom" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>链路网络图</title>
    <link href="CSS/detail.css" rel="stylesheet" type="text/css" />
    <style type="text/css"> 
          .treeTable
        {
        	width:50%;
            border-left:rgb(218,218,218) solid 1px;
            border-top:rgb(218,218,218) solid 1px;
            
        }
        
        .treeTable td
        {
            border-right:rgb(218,218,218) solid 1px;
            border-bottom:rgb(218,218,218) solid 1px;
           
        }
    </style>
</head>

	<body bgColor="#ffffff" >
        <form id="Form1" method="post" runat="server">
		<uc1:top runat="server" ID="top" />
              <div class="rightArea clearfix">
	<div class="MemberPage">
    
		
		<br />
		<br />
		<br />
			<TABLE id="Table1" class="treeTable" cellSpacing="0" cellPadding="0" align="center" style="margin:0 auto">
				<tr align="center"  >
					<td colspan="2" style='background-image:url(images/detail-table-bg_03.png);height:30px;' align="center">
						<b><%=GetTran("000459", "链路网络")%></b>
					</td>
				</tr>
				<TR >
					<TD align="right" style="HEIGHT: 40px"><%=GetTran("000373", "网络起点编号")%>：</TD>
					<TD style="HEIGHT: 40px" align="left">
						<asp:TextBox id="txtBox_GLBH" runat="server" CssClass="ctConPgTxt"></asp:TextBox></TD>
				</TR>
				<TR  style="HEIGHT: 30px" class="t_white">
					<TD align="right"><%=GetTran("000375", "要查看的期数")%>：</TD>
					<TD style="HEIGHT: 30px" align="left">
						<asp:DropDownList id="DropDownList_QiShu" runat="server" style="border:#cccccc solid 1px;"></asp:DropDownList></TD>
				</TR>
				<TR  >
					<TD align="right"><%=GetTran("000376", "选择结构类型")%>：</TD>
					<TD style="HEIGHT: 40px" align="left">
						<asp:RadioButtonList id="RadioButtonList_Type" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" class="t_white"
							RepeatColumns="2">
							<asp:ListItem Value="llt_tj" Selected="True">推荐链路图</asp:ListItem>
							<asp:ListItem Value="llt_az" >安置链路图</asp:ListItem>
						</asp:RadioButtonList></TD>
				</TR>
				<TR >
					<TD align="center" colSpan="2" style="HEIGHT: 40px">
					<%--	<asp:Button id="btn_Submit" runat="server" Text="确定" CssClass="anyes" onclick="btn_Submit_Click"></asp:Button>--%>

                         <asp:Button ID="btn_Submit" runat="server" Height="27px" Width="47px" Style=" margin-top: 3px; padding: 2px 9px; color: #FFF; border: 1px solid #507E0C;background:#507E0C; background-image: linear-gradient(#addf58,#96c742); text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.4);"
                                        Text="查 询" CssClass="anyes" onclick="btn_Submit_Click" />

					</TD>
				</TR>
			</TABLE>
		
		
    </div>
                  </div>
            	<uc2:bottom runat="server" ID="bottom" />
            </form>
	
	</body>

</html>
