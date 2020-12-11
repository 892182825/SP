<%@ Page Language="C#" AutoEventWireup="true" CodeFile="koukuanyuanyin.aspx.cs" Inherits="Company_koukuanyuanyin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
        <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
<script src="../JS/QCDS2010.js" type="text/javascript"></script>

<script type="text/javascript">
	function down2()
	{
		if(document.getElementById("divTab2").style.display=="none")
		{
			document.getElementById("divTab2").style.display="";
			document.getElementById("imgX").src="images/dis1.GIF";
			
		}
		else
		{
			document.getElementById("divTab2").style.display="none";
			document.getElementById("imgX").src="images/dis.GIF";
		}
	}
</script>
<SCRIPT language="javascript">
     function secBoard(n)
  
  {
  //  document.Form1.sa.value=n;
  //  document.getElementById("s").click();
       for(i=0;i<secTable.cells.length;i++)
      secTable.cells[i].className="sec1";
    secTable.cells[n].className="sec2";
    for(i=0;i<mainTable.tBodies.length;i++)
      mainTable.tBodies[i].style.display="none";
    mainTable.tBodies[n].style.display="block";
  }
</SCRIPT>
</head>
<body>
    <form id="form1" runat="server">
    <br />
			<table width="100%" border="0" cellspacing="0" cellpadding="0" class="biaozzi">
				<tr>
					<td>
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
							<TBODY>
								<tr>
									
									<td vAlign="top" align="center"><span id="DetailSpan" runat="server" style="WIDTH:780px"></span>
									</td>
									</tr>
									</table>
					</td>
				</tr>
				<tr>
					<td align="center">
                        <asp:Button ID="Button1" runat="server" Text="返 回" onclick="Button1_Click"  CssClass="anyes"/>
					</td>
				</tr>
			</table>
    </form>
</body>
</html>
