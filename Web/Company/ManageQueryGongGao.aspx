<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageQueryGongGao.aspx.cs"
    Inherits="Company_ManageQueryGongGao" %>

<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
<link type="text/css" rel="Stylesheet" href="CSS/Company.css" />

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
<SCRIPT language="javascript"> 
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
</SCRIPT>
     <script language="javascript" type="text/javascript">
    function getdate()
    {
        var d=new Date();
        var day=d.getDate();
        var month=d.getMonth() + 1;
        var year=d.getFullYear();
        var datetimes=year+"-"+month+"-"+day;
        document.getElementById("txtendDate").value=datetimes;
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
    
    function comfirm()
    {
        return confirm('<%=GetTran("000836","你确定删除")%>'+'？？？');
    }
    
    </script>
     
    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <br />
    <div>
        <table class="biaozzi">
            <tr>
                <td style="white-space:nowrap">
                    &nbsp;<asp:Button ID="btnseach" runat="server" Text="查 询" OnClick="btnseach_Click" CssClass="anyes" />
                </td>
                <td style="white-space:nowrap">
                   <%=GetTran("000720", "发布日期")%>  <%=GetTran("000448", "从")%>：
                </td>
                <td style="white-space:nowrap">
                    <asp:TextBox ID="txtdatastrat" runat="server" CssClass="Wdate" onfocus="WdatePicker()"></asp:TextBox>
                </td>
                <td style="white-space:normal">
                    &nbsp;<%=GetTran("000205", "到")%>：
                </td>
                <td style="white-space:nowrap">
                    <asp:TextBox ID="txtendDate" runat="server" CssClass="Wdate" onfocus="WdatePicker()"></asp:TextBox>
                </td>
                
            </tr>
        </table>
        <br>
        <table width="100%">
            <tr>
                <td colspan="3" width="100%" style="border:rgb(147,226,244) solid 1px">
                    <asp:GridView ID="givMessageSend" runat="server" AutoGenerateColumns="False" CssClass="tablemb  bordercss"
                        OnRowCommand="givMessageSend_RowCommand" Width="100%" OnRowDataBound="givMessageSend_RowDataBound"
                        AlternatingRowStyle-Wrap="False" FooterStyle-Wrap="False" HeaderStyle-Wrap="False"
                        PagerStyle-Wrap="False" RowStyle-Wrap="False" SelectedRowStyle-Wrap="False">
                        <FooterStyle Wrap="False"></FooterStyle>
                        <RowStyle Wrap="False"></RowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="操作" ShowHeader="False" ItemStyle-HorizontalAlign="center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="Endit" CommandArgument='<%#Eval("ID") %>' ><%=GetTran("000259", "修改")%></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="操作" ShowHeader="False" ItemStyle-HorizontalAlign="center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="Button1" runat="server" CausesValidation="false" CommandName="Del"
                                        OnClientClick="return comfirm();" CommandArgument='<%#Eval("ID") %>'
                                        > <%=GetTran("000022", "删除")%> </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="公告标题" ItemStyle-HorizontalAlign=Center>
                            <ItemTemplate >
                                <asp:LinkButton ID="LinkButton1" CommandArgument='<%#Eval("ID") %>' CommandName="GO" ForeColor="Blue"   runat="server"  ToolTip='<%#Eval("Infotitle")%>' ><%#GetStr(Eval("InfoTitle").ToString())%></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="接收者" ItemStyle-HorizontalAlign="center">
                                <ItemTemplate>
                                    <%# jieshuoduixing(DataBinder.Eval(Container.DataItem, "LoginRole").ToString())%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Condition" HeaderText="接收条件"  ItemStyle-HorizontalAlign="center"/>
                            <asp:BoundField DataField="Sender" HeaderText="发送人编号"  ItemStyle-HorizontalAlign="center"/>
                            
                            <asp:TemplateField HeaderText="发布时间"  ItemStyle-HorizontalAlign="center">
                                <ItemTemplate>
                                    <asp:Label ID="fbsj" runat="server" Text='<%#GetBiaoZhunTime(Eval("Senddate").ToString())%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle Wrap="False"></PagerStyle>
                        <SelectedRowStyle Wrap="False"></SelectedRowStyle>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <AlternatingRowStyle Wrap="False" BackColor="#F1F4F8" ></AlternatingRowStyle>
                        <EmptyDataTemplate>
                            <table width="100%" cellspacing="0">
                                <tr>
                                    <th>
                                        <%=GetTran("000015", "操作")%> 
                                    </th>
                                    <th>
                                        <%=GetTran("000724", "公告标题")%>
                                    </th>
                                    <th>
                                       <%=GetTran("000784", "接收者")%> 
                                    </th>
                                    <th>
                                       <%=GetTran("007227", "接收条件")%> 
                                    </th>
                                    <th>
                                        <%=GetTran("000726", "发送人编号")%>
                                    </th>
                                    <th>
                                        <%=GetTran("000727", "发送日期")%>
                                    </th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="margin-left: 40px">
                    <uc1:Pager ID="Pager1" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    
                </td>
            </tr>
        </table>
    </div>
    
    <div id="cssrain" style="width:100%">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
                            <tr>
                                <td width="150">
                                    <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
                                        <tr>
                                            <td class="sec2" onclick="secBoard(0)">
                                                <span id="span1" title="" onmouseover="cut()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000032", "管 理"))%></span>
                                            </td>
                                            <td class="sec1" onclick="secBoard(1)">
                                                <span id="span2" title="" onmouseover="cut1()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <a href="#">
                                        <img src="images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX"
                                            onclick="down2()" style="vertical-align:middle" /></a>
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
                                                        <asp:LinkButton ID="Button1" Text="导出EXECL" runat="server" OnClick="btndownExcel_Click"
                                                            Style="display: none;"></asp:LinkButton>
                                                        <a href="#">
                                                            <img src="images/anextable.gif" width="49" height="47" border="0" onclick="__doPostBack('Button1','');" /></a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
                                                        <%=GetTran("006860", "")%>
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
