<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeductSalaryView.aspx.cs"
    Inherits="Company_DeductSalaryView" EnableEventValidation="false" %>

<%@ Register Src="../UserControl/ExpectNum.ascx" TagName="ExpectNum" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>扣款</title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function getname() {
            var number = document.getElementById("txtbh").value;
            var name = AjaxClass.GetName(number).value;
            document.getElementById("lit_name").innerHTML = name;
        }
    </script>

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
        {
            secTable.cells[i].className="sec1";
        }
        secTable.cells[n].className="sec2";
        for(i=0;i<mainTable.tBodies.length;i++)
        {
            mainTable.tBodies[i].style.display="none";
        }
        mainTable.tBodies[n].style.display="block";
      }
    </script>

    <script language="javascript" type="text/javascript">
        function vali() {
            if (confirm('<%=GetTran("007427","确定提交") %>！')) { 
                var tbh = document.getElementById("txtbh").value;
                var tmoney = document.getElementById("money").value;
                var tquestion = document.getElementById("question").value;
                if (tbh.length <= 0 || tmoney.length <= 0 || tquestion.length <= 0) {
                    alert('<%=GetTran("002043","每一项都必须填写") %>');
                    return false;
                }
            } else { return false; }
        }
         function valimember()
        {
            var tbh=document.getElementById("txtbh").value;
              if(tbh.length<=0)
              {
                     alert('<%=GetTran("002042","请填写会员编号") %>');
                     return false;
              }
        }
    </script>

    <script language="javascript" src="../js/SqlCheck.js"></script>

    <script language="javascript" type="text/javascript">
    function CheckText()
	{
		filterSql();
	}
    </script>

    <script type="text/javascript">
        function cut()
        {
             document.getElementById("span1").title='<%=GetTran("000032", "管 理") %>';
        }
        function cut1()
        {
             document.getElementById("span2").title='<%=GetTran("000033", "说 明") %>';
        }
        
        window.onload=function()
	    {
	        down2();
	    };
    </script>

    <script type="text/javascript" language="javascript">
        function Fmoney(money)
        {
            if(isNaN(money.value))
            {
                alert('<%=GetTran("001145", "请输入数字！") %>');
                money.value=0;
                money.focus();
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <br />
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
        <tr>
            <td>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
                    <tr>
                        <td>
                        </td>
                        <td valign="top">
                            <table cellspacing="1" cellpadding="0" width="600px" border="0" align="center" class="tablemb">
                                <tr>
                                    <th colspan="2">
                                        FTC调整
                                    </th>
                                </tr>
                                <tr>
                                    <td align="right" style="white-space: nowrap;" bgcolor="#EBF1F1">
                                        <%=GetTran("000024", "会员编号")%>：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtbh" runat="server" onblur="getname()" MaxLength="100" Width="88px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="white-space: nowrap;" bgcolor="#EBF1F1"><%=GetTran("000025","会员姓名")%>：</td>
                                    <td><asp:Label ID="lit_name" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td align="right" style="white-space: nowrap;" bgcolor="#EBF1F1">
                                        <%= GetTran("003163","操作类型")%>：
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="rad_Deduct" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Value="0" Text="扣款"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="补款"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 29px; white-space: nowrap;" align="right" bgcolor="#EBF1F1">
                                        <%= GetTran("000000","FTC")%>：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="money" runat="server" Width="88px" MaxLength="9" onblur="Fmoney(this)"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 29px; white-space: nowrap;" align="right" bgcolor="#EBF1F1">
                                        <%=GetTran("000078", "备注")%>：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="question" MaxLength="500" runat="server" Width="285px" TextMode="MultiLine"
                                            Height="110px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 7px">
                        </td>
                    </tr>
                </table>
                <table cellspacing="0" cellpadding="0" width="600px" border="0" align="center">
                    <tr>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 26%">
                                    </td>
                                    <td style="width: 80px">
                                        <asp:Button ID="Button1" runat="server" Text="提 交" OnClick="Button1_Click" CssClass="anyes"
                                            OnClientClick="return abc();"></asp:Button>
                                             <input type="hidden" value="0" id="hid_fangzhi" runat="server" />
                                    </td>
                                    <td style="width: 100px">
                                        <asp:Button ID="Button3" runat="server" Text="返 回" CssClass="anyes" OnClick="Button3_Click">
                                        </asp:Button>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 7px">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="cssrain" style="width: 100%">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" background="../images/DMdp.gif">
            <tr>
                <td width="80">
                    <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="Table1">
                        <tr>
                            <td class="sec2">
                                <span id="sp" title='<%=GetTran("000033")%>'>
                                    <%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033"))%></span>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <img src="../images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX"
                        onclick="down2()" />
                </td>
            </tr>
        </table>
        <div id="divTab2">
            <table width="100%" height="68" border="0" cellspacing="0" class="DMbk" id="Table2">
                <tbody style="display: block">
                    <tr>
                        <td valign="bottom" style="padding-left: 20px">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        1、<%=GetTran("002028", "此功能是添加扣补款功能")%>。<br />
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
    <script type="text/javascript">
        function abc() {
            if (confirm('确定转入！')) {
                var hid = document.getElementById("hid_fangzhi").value;
                if (hid == "0") {
                    document.getElementById("hid_fangzhi").value = "1";
                } else {
                    alert("不可重复提交！");
                    return false;
                }
            } else { return false; }
        }
   </script>