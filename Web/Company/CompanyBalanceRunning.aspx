<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompanyBalanceRunning.aspx.cs" Inherits="Company_CompanyBalanceRunning" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    		<script>
			HideWait();
		</script>
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
    <center>
        <table width="50%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
                    <tr>
                    <td align="center"><%=GetTran("001193", "特别提醒")%>：</td>
                    </tr>
                    <tr align="center">
                    <td><%=GetTran("001196", "在弹出写有“程序运行完毕......”字样的新窗口")%></td>
                    </tr>
                    <tr align="center">
                    <td><%=GetTran("001197", "之前，请不要关闭该系统中的任何窗口。")%></td>
                    </tr>
                    <tr align="center">
                    <td><%=GetTran("001199", "如果结算时间明显超时，说明系统出了问题，")%></td>
                    </tr>
                    <tr align="center">
                    <td><%=GetTran("001201", "这时，可以关闭任何窗口，并联系系统维护人员。")%></td>
                    </tr>
                    <tr>
                    <td align="center"><%=GetTran("001202", "明显超时：远远超过上一次的结算时间。")%></td>
                    </tr>
                    </table>
                    </center>
    </form>
</body>
</html>
