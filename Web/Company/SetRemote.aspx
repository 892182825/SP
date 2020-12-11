<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetRemote.aspx.cs" Inherits="Company_SetRemote" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>遥控设置</title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />

    <script src="../JS/QCDS2010.js" type="text/javascript"></script>

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
		    window.onerror=function()
		    {
		        return true;
		    };
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

    <script language="javascript">
     function secBoard(n)
  {
       for(i=0;i<secTable.cells.length;i++)
      secTable.cells[i].className="sec1";
    secTable.cells[n].className="sec2";
    for(i=0;i<mainTable.tBodies.length;i++)
      mainTable.tBodies[i].style.display="none";
    mainTable.tBodies[n].style.display="block";
  }	 
         window.onload=function()
	    {
	        down2();
	    };
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <br />
    <table width="660px" border="0" cellpadding="0" cellspacing="0" align="center" class="tablemb">
        <tr>
            <td  style="height: 29px; white-space: nowrap;" align="right" bgcolor="#EBF1F1" width="250px">
                <%=GetTran("","")%>系统开放指令：
            </td>
            <td align="left">
                <asp:TextBox ID="txtopen" runat="server" MaxLength="15" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" style="height: 29px; white-space: nowrap;" align="right" bgcolor="#EBF1F1" >
                <%=GetTran("", "")%>系统关闭指令：
            </td>
            <td align="left">
            <asp:TextBox ID="txtclose" runat="server" MaxLength="15" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" style="height: 29px; white-space: nowrap;" align="right" bgcolor="#EBF1F1" >
                <%=GetTran("", "")%>手机号：
            </td>
            <td align="left">
            <asp:TextBox ID="txtmob" runat="server" MaxLength="15" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                    <asp:Button ID="btnEOk" runat="server" Text="确定" CssClass="anyes" 
                        onclick="btnEOk_Click" />
            </td>
        </tr>
        
    </table>
    <div id="cssrain" style="width: 100%">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
            <tr>
                <td width="80">
                    <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
                        <tr>
                            <td class="sec2" onclick="secBoard(0)">
                                <span id="span1" title="" onmouseover="cut()">
                                    <%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <a href="#">
                        <img src="images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX"
                            onclick="down2()" style="vertical-align: middle" /></a>
                </td>
            </tr>
        </table>
        <div id="divTab2">
            <table width="100%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
                <tbody style="display: block" id="tbody0">
                    <tr>
                        <td valign="bottom" style="padding-left: 20px">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        １、<%=GetTran("", "")%>使用设置的手机号发送指令控制系统。
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
