<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TranslationsAdd.aspx.cs" Inherits="languageAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title><%=GetTran("006038", "添加翻译记录")%></title>
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
      secTable.cells[i].className="sec2";
    secTable.cells[n].className="sec1";
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
      <table style="width: 100%; "  border="1" bordercolordark="lightblue" bordercolorlight="#ffffff"  class="tablemb"
                                cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="3">
                    <%=GetTran("006038", "添加翻译记录")%> </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 119px;">
                     <%=GetTran("006665", "默认语言")%>：</td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlLanguageAdd" runat="server">
                    </asp:DropDownList></td>
                <td style="width: 100px">
                    &nbsp;</td>
            </tr>
            <tr>
               <td style="text-align: right; width: 119px;">
                     <%=GetTran("006666", "默认值")%>：</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtDefaultLanguage" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvCN" runat="server" ControlToValidate="txtDefaultLanguage"
                        ErrorMessage="*"></asp:RequiredFieldValidator></td>
                <td style="width: 100px; color: #000000;">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right; width: 119px;">
                     <%=GetTran("001680", "描述")%>：</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtDesc" runat="server"  TextMode="MultiLine" 
                       style="width:486px;height:147px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvDesc" runat="server" ControlToValidate="txtDesc"
                        ErrorMessage="*"></asp:RequiredFieldValidator>&nbsp;</td>
                <td style="width: 100px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="3" style="text-align: center">
                    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click"   CssClass ="another"  Text="添加" />
                    <input id="Reset1"   class ="another"  type="reset"  title ="重置" value ='<%=GetTran("006812", "重置")%>' />
                    <input id="btnClose"   class ="another"  type="button" title="关闭" value ='<%=GetTran("000019", "关闭")%>' onclick ="javascript:window.returnValue=1;window.close();" /></td>
            </tr>
        </table>
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
                    <div id="divTab2" style ="display:none;">
                        <table width="100%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">                    
                            <tbody style="display: block">
                                <tr>
                                    <td style="padding-left: 20px">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <%=GetTran("006038", "添加翻译记录")%><br /> 
                    1.<%=GetTran("006038", "添加翻译记录")%><br />
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
