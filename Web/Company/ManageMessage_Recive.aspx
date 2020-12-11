<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageMessage_Recive.aspx.cs"
    Inherits="Company_ManageMessage_Recive" %>

<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>收件箱</title>
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
      
      function isDelete()
      {
         return window.confirm('<%=GetTran("000248")%>');
      }
</SCRIPT>
    <script language="javascript" type="text/javascript">
        function SelectAll(obj)
        {
            var allInput=document.getElementsByTagName('input');
            for(var i=0;i<allInput.length;i++)
           {
                if(allInput[i].type=="checkbox")
                {
                    allInput[i].checked=obj.checked;
                }
            }
        }
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
    
    function comfir(){
        return confirm('<%=GetTran("000836","确定删除吗") %>？')
    }
    </script>

    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
    
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


</head>
<body >
    <form id="form1" runat="server">
    <div>
    <br />
        <table width="100%">
            <tr>
                <td>
                    <table class="biaozzi">
                        <tr>
                          
                            <td nowrap="nowrap">
                                <%=GetTran("001259", "收件日期")%>  <%=GetTran("000448", "从")%>：
                            </td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="txtdatastrat" runat="server" CssClass="Wdate" onfocus="WdatePicker()"></asp:TextBox>
                             </td>
                            <td nowrap="nowrap" align="right">
                                &nbsp;<%=GetTran("000205", "到")%>：
                            </td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="txtendDate" runat="server" CssClass="Wdate" onfocus="WdatePicker()"></asp:TextBox>
                            </td>
                            
                            <td nowrap="nowrap" align="right"><%=GetTran("007398", "邮件分类")%>：</td>
		                     <td nowrap="nowrap">
				                <asp:dropdownlist id="DropMsgClass" Runat="server">
                					<asp:ListItem Text="不限" Value="-1" Selected="True"></asp:ListItem>
				                </asp:dropdownlist>
			                </td>	

                        </tr>
                        <tr>
                      
                        <td nowrap="nowrap" align="right"><%=GetTran("007405", "处理状态")%>：</td>
                         <td nowrap="nowrap">
				            <asp:dropdownlist id="DropHandleStatus" Runat="server">
				            <asp:ListItem Text="不限" value="-1" Selected="True">
            					</asp:ListItem>
            					<asp:ListItem Text="未读" value="0"></asp:ListItem>
            					<asp:ListItem Text="已阅读" value="1"></asp:ListItem>
            					<asp:ListItem Text="处理中" Value="2"></asp:ListItem>
            					<asp:ListItem Text="已处理" Value="3"></asp:ListItem>
				            </asp:dropdownlist>
			            </td>	
			             <td nowrap="nowrap" align="right"><%=GetTran("000024", "会员编号")%>：</td>
                             <td nowrap="nowrap">
                                <asp:TextBox ID="TxtBianhao" runat="server" MaxLength="10"></asp:TextBox>
                            </td>
                              <td nowrap="nowrap">
                                &nbsp;<asp:Button ID="btnSeach" runat="Server" Text="查 询" OnClick="btnSeach_Click" CssClass="anyes" />
                                 
                            </td>

                        </tr>
                        
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <br>
                    <table width="100%">
                        <tr>
                            <td width="100%"  style="border:rgb(147,226,244) solid 1px">
                                <asp:GridView ID="givShowMessage" runat="server" AutoGenerateColumns="False" CssClass="tablemb bordercss"
                                    DataKeyNames="ID" OnRowCommand="givShowMessage_RowCommand" Width="100%" 
                                    onrowdatabound="givShowMessage_RowDataBound"   AlternatingRowStyle-Wrap="False" 
                        FooterStyle-Wrap="False" 
                        HeaderStyle-Wrap="False" 
                        PagerStyle-Wrap="False" 
                        RowStyle-Wrap="False" 
                        SelectedRowStyle-Wrap="False">
<FooterStyle Wrap="False"></FooterStyle>

<RowStyle Wrap="False"></RowStyle>
                                    <Columns>
                                        <asp:TemplateField HeaderText="转 发" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk" runat="server" />
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <input id="Checkbox1" onclick="SelectAll(this)" type="checkbox" /><%=GetTran("004198", "全选")%>
                                            </HeaderTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                             <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="Button1" runat="server"  OnClientClick="return comfir()"  CommandName="Del" CommandArgument='<%#Eval("ID") %>' ><%=GetTran("000022", "删除")%></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="Button2" runat="server"  CommandName="huifu" CommandArgument='<%#Eval("ID") %>' ><%=GetTran("001696", "回复")%></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:HyperLinkField ItemStyle-HorizontalAlign="center" DataNavigateUrlFields="id" DataNavigateUrlFormatString="MessageContent.aspx?id={0}&amp;T=MessageReceive&amp;source=ManageMessage_Recive.aspx"
                                               Text="展开" HeaderText="展开相关邮件" ItemStyle-Wrap="false"></asp:HyperLinkField>
                                        
                                        <asp:TemplateField HeaderText="接收对象"  ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <%# jieshuoduixing(DataBinder.Eval(Container, "DataItem.loginRole").ToString()) %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="发件人"  ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <%# jieshuoduixing(DataBinder.Eval(Container, "DataItem.SenderRole").ToString()) %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="发送编号" DataField="Sender"  ItemStyle-HorizontalAlign="center"/>
                                        <asp:HyperLinkField ItemStyle-HorizontalAlign="center" DataNavigateUrlFields="id" DataNavigateUrlFormatString="MessageContent.aspx?id={0}&amp;T=MessageReceive&amp;source=ManageMessage_Recive.aspx"
                                            DataTextField="infoTitle" HeaderText="信息标题" ItemStyle-Wrap="false"></asp:HyperLinkField>
                                        
                                        <asp:TemplateField HeaderText="发布日期"  ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <asp:Label ID="fbsj" runat="server" Text='<%#GetBiaoZhunTime(Eval("Senddate").ToString())%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
  
                                        <asp:BoundField HeaderText="处理状态" DataField="HandleStatus" ItemStyle-Wrap="false"  ItemStyle-HorizontalAlign="center"/>

                                        <asp:BoundField HeaderText="邮件分类" DataField="ClassName" ItemStyle-Wrap="false"  ItemStyle-HorizontalAlign="center"/>

                                   
                                    </Columns>
                                    <AlternatingRowStyle BackColor="#F1F4F8" />
                                    <EmptyDataTemplate>
                            <table cellspacing="0"  Width="100%">
                                <tr>
                                    <th>
                                        <%=GetTran("001689", "转发")%> 
                                    </th>
                                    <th>
                                        <%=GetTran("000015", "操作")%> 
                                    </th>
                                    <th>
                                        <%=GetTran("007403", "展开相关邮件")%> 
                                    </th>
                                    <th>
                                       <%=GetTran("000912", "接收对象")%>
                                    </th>
                                    <th>
                                        <%=GetTran("001721", "发件人")%>
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
									<th>
										<%=GetTran("000906", "是否阅读")%>
									</th>
                                    
                                </tr>                
                            </table>
                        </EmptyDataTemplate>

<PagerStyle Wrap="False"></PagerStyle>

<SelectedRowStyle Wrap="False"></SelectedRowStyle>

<HeaderStyle Wrap="False"></HeaderStyle>

                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <uc1:Pager ID="Pager1" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="biaozzi">
                        <tr id="tr_Manager" runat="server">
                            <td>
                                 <%=GetTran("007875", "转发至")%>：
                            </td>
                            <td>
                                <asp:DropDownList ID="DropMsgClass_2" runat="server">
                                    <asp:ListItem Text="不转发" Value="-1" Selected="True"></asp:ListItem>

                                </asp:DropDownList>
                            </td>
                             <td>
                                 <%=GetTran("007876", "修改状态")%>：
                            </td>
                            <td>
                                <asp:DropDownList ID="DropHandleStatus_2" runat="server">
                                    <asp:ListItem Text="不修改" Value="-1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="已处理" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="处理中" Value="2"></asp:ListItem>

                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="Button_Submit" runat="server" Text="提交" 
                                    CssClass="anyes" onclick="Button_Submit_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="cssrain" style="width:100%">
                        <table width="99%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
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
                                            onclick="down2()" style="vertical-align:middle"/></a>
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
                                                        <asp:LinkButton ID="Button1" Text="导出EXECL" runat="server" OnClick="btnExecl_Click"
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
                                                        <%=GetTran("006861", "")%><br>
                                                        <%=GetTran("006862", "")%>
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
            <tr><td style="width:100%" align="right">
                    <asp:HyperLink ID="HyperClassSetting" runat="server" Font-Names="Arial" 
                        Font-Size="Small" NavigateUrl="~/Company/MessageClassSetting.aspx" 
                        Visible="False"><%=GetTran("007879", "设置邮件分类")%></asp:HyperLink>
                    </td></tr>
        </table>
    </div>
    </form>
</body>
</html>
