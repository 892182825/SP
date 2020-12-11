<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LanguageAdd.aspx.cs" Inherits="Company_LanguageAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/Dtd/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=GetTran("006024", "添加新语言")%></title>
     <base target="_self"/>
    <meta http-equiv="Pragma" content="no-cache"/>
	<meta http-equiv="Cache-Control" content="no-cache"/>
	<meta http-equiv="Expires" content="0"/>
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
	  function cut()
        {
             document.getElementById("span1").title='<%=GetTran("000032", "管 理") %>';
        }
        function cut1()
        {
             document.getElementById("span2").title='<%=GetTran("000033", "说 明") %>';
        }
    </script>

    <script language="javascript">
   function secBoard(n)
  
  {
  //  document.Form1.sa.value=n;
  //  document.getElementById("s").click();
       for(i=0;i<secTable.cells.length;i++)
      secTable.cells[i].className="sec2";
    secTable.cells[n].className="sec1";
    for(i=0;i<mainTable.tBodies.length;i++)
      mainTable.tBodies[i].style.display="none";
    mainTable.tBodies[n].style.display="block";
  }
    </script>
  
<style type="text/css">
        table{font-size:9pt;}
        .tdh{text-align:center ;width:100%;font-size:11pt;}
        .tdL{text-align:right ;width:40%;}
        .tdC{text-align:left ;width:60%;}
    .style1
    {
        text-align: center;
        width: 100%;
        font-size: 11pt;
        height: 24px;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server" >
    <div style ="text-align:center ;width:100%" >  
     <table border="1" width="98%" bordercolordark="lightblue" bordercolorlight="#ffffff"  cellpadding="0" cellspacing="0" style ="margin-top:20px;" >
                                <tr>
                                    <td class="style1" colspan="2" style="background-image:url(images/tabledp.gif);color:White;height:25px;">
                                        <asp:Label ID="lblTitle" runat="server" Font-Size="9pt" Text="语言修改"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdL">
                                       <span title ='语言名称' ><%=GetTran("006023", "语言名称")%></span>：</td>
                                    <td class="tdC">
                                        <asp:TextBox ID="txtLanguageName" MaxLength="20" runat="server" Width="124px"></asp:TextBox>
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td class="tdL">
                                       <span title ='语言描述' ><%=GetTran("006637", "语言描述")%></span> ： </td>
                                    <td class="tdC">
                                        <asp:TextBox ID="txtLanguageDesc" runat="server" Height="68px" 
                                            TextMode="MultiLine" Width="194px"></asp:TextBox>
                                    </td>
                                  
                                </tr>
                                <tr>
                                    <td class="tdh" colspan="2">
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="anyes" 
                                            onclick="btnSubmit_Click" style="cursor:pointer" Text="修 改" />
                                            &nbsp;
                                        &nbsp;<input id="Button1" 
                                            onclick="javascript:window.location.href='LanguageManage.aspx';" type="button"  
                                            title="返 回" value ='<%=GetTran("000421", "返回")%>' class="anyes" /></td>
                                </tr>
                            </table>         
    </div>
    <table width="100%">
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        </table>
    <br />
     <div id="cssrain" style="width:100%">
                   <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
                        <tr>
                            <td width="80px">
                                <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
                                    <tr>
                                     <td class="sec2" onclick="secBoard(1)">
                    <span id="span2" title="" onmouseover="cut1()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
                </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <a href="#">
                                    <img src="images/dis.GIF" name="imgX" width="18" height="22" border="0" id="imgX"
                                       align="middle" onclick="down2()" /></a>
                            </td>
                        </tr>
                    </table>
                    <div id="divTab2" style ="display :none ;">
                        <table width="100%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">                    
                            <tbody style="display: block">
                                <tr>
                                    <td style="padding-left: 20px">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <span title ='添加新语言'><%=GetTran("006024", "添加新语言")%></span> <br /> 
                    1.<%=GetTran("006024", "添加新语言")%><br />
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
    
    <script language="javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
</body>
</html>
