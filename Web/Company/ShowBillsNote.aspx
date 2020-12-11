<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowBillsNote.aspx.cs" Inherits="Company_ShowBillsNote" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link rel="Stylesheet" type="text/css" href="CSS/Company.css" />
    <script src="../JS/QCDS2010.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../javascript/Mymodify.js"></script>
    
    <script>
	 $(document).ready(function(){
			if($.browser.msie && $.browser.version == 6) {
				FollowDiv.follow();
			}
	 });
	 FollowDiv = {
			follow : function(){
				$('#cssrain').css('position','absolute');
				$(window).scroll(function(){
				    var f_top = $(window).scrollTop() + $(window).height() - $("#cssrain").height() - parseFloat($("#cssrain").css("borderTopWidth")) - parseFloat($("#cssrain").css("borderBottomWidth"));
					$('#cssrain').css( 'top' , f_top );
				});
			}
	  }
    </script>

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
       for(i=0;i<secTable.cells.length;i++)
      secTable.cells[i].className="sec1";
    secTable.cells[n].className="sec2";
    for(i=0;i<mainTable.tBodies.length;i++)
      mainTable.tBodies[i].style.display="none";
    mainTable.tBodies[n].style.display="block";
  }
</SCRIPT>
    <script language="javascript" type="text/javascript">
    function check(obj)
    {
        if (obj.value=="不限")
        {
        obj.value="";   
        }
    }
    function check2(obj)
    {
        if(obj.value=="")
         {
         obj.value="不限";
         }
    } 
    	window.onload=function()
	{
	    down2();
	};
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center"><br />
        <table  width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#F8FBFD" class="tablemb"  >
				<tr >
					<th background="images/tabledp.gif" class="tablebt" align="center" style="HEIGHT: 24px">
						<%=GetTran("000744", "查看备注")%>
					</th>
				</tr>
				<tr>
					<td><br>
						<br>
						<asp:Literal ID="Literal1" runat="server"></asp:Literal>
					</td>
				</tr>
				<tr>
					<td align="center"><br>
						<br>
						<br>
						</td>
				</tr>
			</table>
			<br />
            <input type="button" ID="butt_Query"value='<%=GetTran("000421","返回") %>' 
                                            style="cursor:pointer" Class="anyes" onclick="history.back()"/>
    </div>
    <div id="cssrain" style="width:100%">
        <table width="99%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
            <tr>
                <td width="80">
                    <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
                        <tr>
                            <td class="sec2" onclick="secBoard(0)">
                                <%=GetTran("000033", "说 明")%>
                            </td>
                            <%--<td class="sec1" onclick="secBoard(1)">
                                
                            </td>--%>
                        </tr>
                    </table>
                </td>
                <td>
                    <a href="#">
                        <img src="images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX"
                            onclick="down2()" /></a>
                </td>
            </tr>
        </table>
        <div id="divTab2" width="100%">
            <table width="100%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
                <tbody style="display: block">
                    <tr>
                        <td valign="bottom" style="padding-left: 20px">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        1、<%=GetTran("000078", "备注")%>。
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </tbody>
                <tbody style="display: none">
                    <tr>
                        <td style="padding-left: 20px">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
