<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocPrint.aspx.cs" Inherits="Company_DocPrint" %>

<%@ Register src="../UserControl/DocPrintBillOut.ascx" tagname="DocPrintBillOut" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%=GetTran("006898", "单据打印")%></title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        a
        {   
        	text-decoration:none;
        	font-family:Arial;
        	font-size:14px;
        	color:Black;
        }
        a:hover
        {
        	text-decoration:underline;
        	color:Red;
        	font-size:15px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" method="post">
    <br />
        <center>
			<table cellspacing="0" cellpadding="0" border="0" style="WIDTH: 1000px; ">
				<tr style="display:none">
					<td align="center"> <h2><%=GetTran("003059", "单据打印")%></h2></td>
				</tr>
				<tr bgcolor="#ffffff">
					<td align="right"><a href='#' onclick="window.print()" onmouseover="window.status='print this document'"
							onmouseout="window.status=''"><%=GetTran("004125", "打印此文档")%></a></td>
				</tr>
				<tr>
					<td align="center">
						<asp:Panel id="resultPanel" runat="server" Width="100%" HorizontalAlign="Center">
                            <uc1:DocPrintBillOut ID="DocPrintBillOut1" runat="server" />
                        </asp:Panel>
					</td>
				</tr>
			</table>
        </center>
    </form>
</body>
</html>
