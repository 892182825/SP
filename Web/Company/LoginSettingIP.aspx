﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginSettingIP.aspx.cs" Inherits="Company_LoginSettingIP" %>

<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title><%=GetTran("000948", "DS2014信息管理系统")%></title>
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
        function cut()
        {
             document.getElementById("span1").title='<%=GetTran("000032", "管 理") %>';
        }
        function cut1()
        {
             document.getElementById("span2").title='<%=GetTran("000033", "说 明") %>';
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

    <script language="javascript">
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
          window.onload=function()
	    {
	        down2();
	    };
    </script>

</head>
<body>
    <form id="Form1" method="post" runat="server">
    <table width="99%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td valign="top">
                <br />
                <div>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
                        <tr>
                            <td>
                                <%=GetTran("000952","IP编号") %>：
                                &nbsp;<asp:TextBox ID="IPBianhao" runat="server" MaxLength="16"></asp:TextBox>&nbsp;&nbsp;
                                <asp:Button ID="Add_blackList" runat="server" Text="添加到黑名单" OnClick="Add_blackList_Click" CssClass="another">
                                </asp:Button>&nbsp;
                                <asp:Button ID="btAddToBlackList" runat="server" Text="删除黑名单" OnClick="btAddToBlackList_Click" CssClass="another">
                                </asp:Button>
                                </td></tr>
                                <tr><td>
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                        <td style="border:rgb(147,226,244) solid 1px">
                           <asp:GridView ID="gvBlackIP" runat="server" Width="100%" AutoGenerateColumns="False"
                        AllowSorting="True" OnSorting="GridView1_Sorting" BackColor="#F8FBFD"  CssClass="tablemb bordercss"
                        OnRowDataBound="gvBlackIP_RowDataBound">
                        <RowStyle HorizontalAlign="Center" />
                        <Columns>
                            <asp:TemplateField HeaderText="是否删除">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelectRow" runat="server" />
                                    <asp:HiddenField ID="hidID" runat="server" Value='<%# Bind("id") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="userid" HeaderText="IP地址"></asp:BoundField>
                        </Columns>
                        <EmptyDataTemplate>
                        </td></tr>
                                <tr style="border:solid 0px white;">
                                    <th>
                                        <%=GetTran("000504", "是否删除")%>
                                    </th>
                                    <th>
                                        <%=GetTran("000961", "IP地址")%>
                                    </th>
                                </tr>   
                        </EmptyDataTemplate>
                        <AlternatingRowStyle BackColor="#F1F4F8" />
                    </asp:GridView>
                    
                        </td><uc2:Pager ID="Pager1" runat="server" />
                        </tr>
                    </table>
                 
                </div>
                <div class="biaozzi" runat="server" id="divChkAll">
                   <input type="checkbox" name="chkAll_" onclick="javascript:var chks = document.getElementsByTagName('input');for(var i=0;i<chks.length;i++){if(chks[i].type=='checkbox'){chks[i].checked=this.checked;}}"
                                    id="chkAll" /><%=GetTran("000776", "选择所有")%>
                </div>
                <br />
            </td>
        </tr>
        <tr>
            <td valign="top">
                <div id="cssrain" style="width:100%">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" background="../images/DMdp.gif">
        <tr>
          <td width="80">
          <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="Table1">
                                    <tr>
                                        <td class="sec2">
                                            <span id="sp" title='<%=GetTran("000033")%>'><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033"))%></span>
                                            
                                        </td>
                                    </tr>
                                </table></td>
          <td><img src="../images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX" onclick="down2()"/></td>
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
                                                    1、<%=GetTran("001023", "设置某些IP不能登录系统")%>。<br />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <%= msg %>
    <asp:Literal ID="alertMsg" runat="server"></asp:Literal>
    </form>
</body>
</html>
