<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageMessage_Drop.aspx.cs"
    Inherits="Company_ManageMessage_Drop" %>

<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

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
    window.onload=function()
	{
	    down2();
	};
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

    <script language="javascript">
     function secBoard(n)
     {
        var tdarr=document.getElementById("secTable").getElementsByTagName("td");
        
        for(var i=0;i<tdarr.length;i++)
        {
            tdarr[i].className="sec1";
        }
        tdarr[n].className="sec2";
        
        var tbody0=document.getElementById("tbody0");
        tbody0.style.display="none";
        var tbody1=document.getElementById("tbody1");
        tbody1.style.display="none";
        
        
        document.getElementById("tbody"+n).style.display="block";
        
      }
      
      function isDelete()
      {
         return window.confirm('<%=GetTran("000248")%>');
      }
    </script>

    <link type="text/css" rel="Stylesheet" href="CSS/Company.css" />

    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>

    <script language="javascript" type="text/javascript">
    function getdate()
    {
        var d=new Date();
        var day=d.getDate();
        var month=d.getMonth() + 1;
        var year=d.getFullYear();
        var datetimes=year+"-"+month+"-"+day;
        document.getElementById("txtendDate").value=datetimes;
        if(month==0)
        {
             var strat=year-1+"-12-"+day;
         }else
         {
            var strat=year+"-"+d.getMonth()+"-"+day;
         }
        document.getElementById("txtdatastrat").value=strat;
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <table width="100%" class="biaozzi" cellpadding="0">
            <tr>
                <td>
                    <table class="biaozzi">
                        <tr>
                             <td style="white-space: nowrap">
                                &nbsp;<asp:Button ID="btnSeach" runat="server" Text="查 询" OnClick="btnSeach_Click"
                                    CssClass="anyes" />
                            </td>
                            <td style="white-space: nowrap">
                                <%=GetTran("000720", "发布日期")%>
                                <%=GetTran("000448", "从")%>：
                            </td>
                            <td style="white-space: nowrap">
                                <asp:TextBox ID="txtdatastrat" CssClass="Wdate" onfocus="WdatePicker()" runat="server"></asp:TextBox>
                            </td>
                            <td style="white-space: nowrap">
                                &nbsp;<%=GetTran("000205", "到")%>：
                            </td>
                            <td style="white-space: nowrap">
                                <asp:TextBox ID="txtendDate" runat="server" CssClass="Wdate" onfocus="WdatePicker()"></asp:TextBox>
                            </td>
                           
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <br>
                    <table width="100%" class="biaozzi">
                        <tr>
                            <td style="border: rgb(147,226,244) solid 1px">
                                <asp:GridView ID="givShowMessage" runat="server" AutoGenerateColumns="False" CssClass="tablemb bordercss"
                                    OnRowCommand="givShowMessage_RowCommand" OnRowDataBound="givShowMessage_RowDataBound"
                                    AlternatingRowStyle-Wrap="False" FooterStyle-Wrap="False" HeaderStyle-Wrap="False"
                                    PagerStyle-Wrap="False" RowStyle-Wrap="False" SelectedRowStyle-Wrap="False" Width="100%">
                                    <FooterStyle Wrap="False"></FooterStyle>
                                    <RowStyle Wrap="False"></RowStyle>
                                    <Columns>
                                        <asp:TemplateField HeaderText="操作" ShowHeader="False" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="Button2" runat="server" OnClientClick="return confirm('确定要删除吗？')"
                                                    CausesValidation="false" CommandArgument='<%#Eval("ID") %>' CommandName="Del"><%#GetTran("000022","删除") %></asp:LinkButton>
                                                <asp:LinkButton ID="lbtnRecover" runat="server" CommandArgument='<%#Eval("ID")%>'
                                                    CommandName="Recover" OnClientClick="return confirm('确实要还原吗？');" 
                                                    oncommand="lbtnRecover_Command" >还原</asp:LinkButton>
                                            </ItemTemplate>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="接收对象" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%#jieshuoduixing(Eval("LoginRole").ToString()) %>'></asp:Label>
                                            </ItemTemplate>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Receive" HeaderText="接收编号" 
                                            ItemStyle-HorizontalAlign="center" >
<ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Sender" HeaderText="发送编号" 
                                            ItemStyle-HorizontalAlign="center" >
<ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:HyperLinkField ItemStyle-HorizontalAlign="center" DataNavigateUrlFields="id"
                                            DataNavigateUrlFormatString="MessageContent.aspx?id={0}&amp;T=V_DroppedMessage&amp;source=ManageMessage_Drop.aspx"
                                            DataTextField="infoTitle" HeaderText="信息标题" ItemStyle-Wrap="false">
<ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                                        </asp:HyperLinkField>
                                        <asp:TemplateField HeaderText="发布日期" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <asp:Label ID="fbsj" runat="server" Text='<%#GetBiaoZhunTime(Eval("Senddate").ToString())%>' />
                                            </ItemTemplate>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <table cellspacing="0" width="100%">
                                            <tr>
                                                <th>
                                                    <%=GetTran("000015", "操作")%>
                                                </th>
                                                <th>
                                                    <%=GetTran("000912", "接收对象")%>
                                                </th>
                                                <th>
                                                    <%=GetTran("000910", "接收编号")%>
                                                </th>
                                                <th>
                                                    <%=GetTran("000908", "发送编号")%>
                                                </th>
                                                <th>
                                                    <%=GetTran("000825", "信息标题")%>
                                                </th>
                                                <th>
                                                    <%=GetTran("000720", "发布日期")%>
                                                </th>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                    <PagerStyle Wrap="False"></PagerStyle>
                                    <SelectedRowStyle Wrap="False"></SelectedRowStyle>
                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                    <AlternatingRowStyle Wrap="False" BackColor="#F1F4F8"></AlternatingRowStyle>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <uc1:Pager ID="Pager1" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="cssrain" style="width: 100%">
                                    <table width="99%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
                                        <tr>
                                            <td width="150">
                                                <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
                                                    <tr>
                                                        <td class="sec2" onclick="secBoard(0)">
                                                            <span id="span1" title="" onmouseover="cut()">
                                                                <%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000032", "管 理"))%></span>
                                                        </td>
                                                        <td class="sec1" onclick="secBoard(1)">
                                                            <span id="span2" title="" onmouseover="cut1()">
                                                                <%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                <a href="#">
                                                    <img src="images/dis.GIF" name="imgX" width="18" height="22" border="0" id="imgX"
                                                        onclick="down2()" style="vertical-align: middle" /></a>
                                            </td>
                                        </tr>
                                    </table>
                                    <div id="divTab2">
                                        <table width="99%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
                                            <tbody style="display: block" id="tbody0">
                                                <tr>
                                                    <td valign="bottom" style="padding-left: 20px">
                                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:LinkButton ID="Button1" Text="导出EXECL" runat="server" OnClick="btndownExcel_Click"
                                                                        Style="display: none;"></asp:LinkButton>
                                                                    <a href="#">
                                                                        <img src="images/anextable.gif" width="49" height="47" border="0" onclick="alert('');__doPostBack('Button1','');" /></a>&nbsp;&nbsp;&nbsp;&nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                            <tbody style="display: none" id="tbody1">
                                                <tr>
                                                    <td style="padding-left: 20px">
                                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td>
                                                                    <%=GetTran("006864", "")%>.
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
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
