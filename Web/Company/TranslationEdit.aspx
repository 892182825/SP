<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TranslationEdit.aspx.cs" Inherits="TranslationEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title><%=GetTran("006037", "翻译编辑")%></title>
    <base target="_self"/>
    <meta http-equiv="Pragma" content="no-cache"/>
	<meta http-equiv="Cache-Control" content="no-cache"/>
	<meta http-equiv="Expires" content="0"/>
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
	  function cut()
        {
             document.getElementById("span1").title='<%=GetTran("000032", "管 理") %>';
        }
        function cut1()
        {
             document.getElementById("span2").title='<%=GetTran("000033", "说 明") %>';
        }
         window.onerror=function()
		    {
		        return true;
		    };
		    
  function secBoard(n)  
  {
       for(i=0;i<secTable.cells.length;i++)
      secTable.cells[i].className="sec1";
    secTable.cells[n].className="sec2";
    for(i=0;i<mainTable.tBodies.length;i++)
      mainTable.tBodies[i].style.display="none";
    mainTable.tBodies[n].style.display="block";
  }
   

   
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
  
<style type="text/css">
        table{font-size:9pt;}
        .tdh{text-align:center ;width:100%;font-size:11pt;}
        .tdL{text-align:right ;width:120px;}
        .tdC{text-align:left ;width:160px;}
        .tdR{text-align:left ;width:120px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
     <div style="text-align: center">
        <table style="width: 100%;"  border="1" bordercolordark="lightblue" bordercolorlight="#ffffff"  class="tablemb"
                                cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="3" style ="font-size:11pt;font-weight:bold ;background-image:url(images/tabledp.gif);color:White;" >
                  <%=GetTran("006645", "翻译内容修改")%></td>
            </tr>
            <tr>
                <td style="text-align: right; width: 119px;">
                  <%=GetTran("006646", "要修改的语言")%>：</td>
                <td style="width: 316px; text-align: left">
                    <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged">
                    </asp:DropDownList></td>
                <td >
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right; width: 119px;">
                    <%=GetTran("006647", "词典编码")%> ：</td>
                <td style="width: 316px; text-align: left">
                    <asp:TextBox ID="txtCode" runat="server" ReadOnly="True"></asp:TextBox><asp:RequiredFieldValidator ID="rfvCN" runat="server" ControlToValidate="txtCode"
                        ErrorMessage="*"></asp:RequiredFieldValidator></td>
               <td >
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 119px; height: 86px; text-align: right">
                     <%=GetTran("006648", "翻译内容")%>：</td>
                <td colspan="2" style="vertical-align: top; height: 86px; text-align: left">
                    <asp:TextBox ID="txtLanguage" runat="server" Height="67px"
                        Width="495px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLanguage"
                        ErrorMessage="*"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td style=" text-align: right; width: 119px;">
                     <%=GetTran("001680", "描述")%>：</td>
                <td style="width: 316px; text-align: left">
                    <asp:TextBox ID="txtDesc" runat="server" Height="63px" TextMode="MultiLine" Width="259px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDesc"
                        ErrorMessage="*"></asp:RequiredFieldValidator></td>
                <td >
                    &nbsp;</td>
            </tr>
            <tr>
                <td style=" text-align: right; width: 119px;">
                    &nbsp;
                </td>
                <td style="width: 316px; text-align: left">
                    <asp:Button ID="btnEdit" runat="server"  CssClass ="another"  OnClick="btnEdit_Click" Text="修改" />&nbsp; <asp:Button
                        ID="btmBack" runat="server" CssClass ="another" Text="返回" OnClick="btmBack_Click" CausesValidation="False" /></td>
               <td >
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="3" style="text-align: center">
                </td>
            </tr>
        </table>
        <br />
    
    </div>
    </div>
    
    <div style ="display:none ;" id="cssrain">
                   <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
                        <tr>
                            <td width="80px">
                                <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
                                    <tr>
                                  <td class="sec2" onclick="secBoard(0)">
                                            <%=GetTran("000033","说 明") %>
                                        </td>
                                       <!--  <td class="sec1" onclick="secBoard(1)">
                                            管 理
                                        </td>  -->
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
                                                    <%=GetTran("006037", "翻译编辑")%><br /> 
                    1.<%=GetTran("006037", "翻译编辑")%><br />
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

