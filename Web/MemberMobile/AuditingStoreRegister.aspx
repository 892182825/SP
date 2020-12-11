<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AuditingStoreRegister.aspx.cs"
    EnableEventValidation="false" Inherits="Company_AuditingStoreRegister" %>

<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/CountryCity.ascx" TagName="CountryCity" TagPrefix="uc2" %>
<%@ Register Src="../UserControl/ExpectNum.ascx" TagName="ExpectNum" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="Stylesheet" href="CSS/member.css" />

    <script src="../JS/QCDS2010.js" type="text/javascript"></script>

    <script language="javascript" src="../js/SqlCheck.js"></script>

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

    <script language="javascript">
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
    function CheckText()
	{
		//这个方法是只有1个lkSubmit按钮时候 可直接用
		filterSql();
	}

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

    <script language="javascript" type="text/javascript">
function secBoard(n)
{
  //  document.Form1.sa.value=n;
  //  document.getElementById("s").click();
//       for(i=0;i<secTable.cells.length;i++)
//      secTable.cells[i].className="sec1";
//    secTable.cells[n].className="sec2";
//    for(i=0;i<mainTable.tBodies.length;i++)
//      mainTable.tBodies[i].style.display="none";
//    mainTable.tBodies[n].style.display="block";
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
    </script>

</head>
<body onload="down2()">
    <form id="form1" runat="server">
    <table width="100%">
        <tr>
            <td>
                <div>
                    <br />
                    <table class="biaozzi">
                        <tr> 
                            <td width="60px" align="right">
                                <asp:Button ID="btnAddStore" runat="server" OnClick="btnAddStore_Click" Text="添加店铺"
                                    CssClass="another" />
                            </td>
                            <td>
                                <asp:LinkButton ID="lkSubmit" runat="server" Text="提交" Style="display: none" OnClick="lkSubmit_Click"></asp:LinkButton>
                                <input class="anyes" id="bSubmit" onclick="CheckText()" type="button" value='<%=GetTran("000048", "查 询")%>' />
                                <asp:Button ID="btnSeasch" runat="server" Text="查 询" OnClick="btnSeasch_Click" CssClass="anyes"
                                    Visible="false" />
                            </td>
                            <td>
                               <asp:DropDownList ID="ddl_shenhe" runat="server">
                                <asp:ListItem Value="0" Text="审核"></asp:ListItem>
                                <asp:ListItem Value="1" Text="未审核"></asp:ListItem>
                               </asp:DropDownList> 
                            </td>
                            <td style="display:none">
                                <%=GetTran("000058", "请选择国家")%>：
                            </td>
                            <td style="display:none">
                                <uc2:CountryCity ID="CountryCity1" runat="server" />
                            </td>
                            <td>
                                <asp:CheckBox ID="chShi" runat="server" Visible="false" Text="按照省市查询" />
                            </td>
                            <td>
                                <%=GetTran("000061", "请选择期数")%>：<uc3:ExpectNum ID="ExpectNum1" runat="server" />
                            </td>
                            <td>
                                <%=GetTran("000037", "店编号")%>：<asp:TextBox ID="txtStoreId" runat="server" MaxLength="10"></asp:TextBox>
                            </td>
                            <td nowrap="nowrap">
                                <%=GetTran("000909", "当前货币")%>：
                                <asp:DropDownList ID="DropCurrency" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropCurrency_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <br />
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="givStoreInfo" runat="server" AutoGenerateColumns="False" CssClass="tablemb"
                    OnRowCommand="givStoreInfo_RowCommand" OnRowDataBound="givStoreInfo_RowDataBound"
                    AlternatingRowStyle-Wrap="False" FooterStyle-Wrap="False" HeaderStyle-Wrap="False"
                    PagerStyle-Wrap="False" RowStyle-Wrap="False" SelectedRowStyle-Wrap="False" Width="100%">
                    <RowStyle Wrap="False"></RowStyle>
                    <Columns>
                        <asp:TemplateField HeaderText="会员编号" ShowHeader="False" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="btnbianhao" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "number")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="StoreId" HeaderText="店编号" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="name" HeaderText="店长姓名" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="StoreName" HeaderText="店铺名称" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="TotalAccountMoney" HeaderText="总金额" ItemStyle-Wrap="false"
                            NullDisplayText="0.00" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N}" />
                        <asp:BoundField DataField="ExpectNum" HeaderText="办店期数" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Direct" HeaderText="推荐人编号" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="OfficeTele" HeaderText="办公电话" ItemStyle-Wrap="false" NullDisplayText="无"
                            ItemStyle-HorizontalAlign="Center" Visible="false" />
                       <asp:TemplateField HeaderText="注册时间">
                                        <ItemTemplate>
                                                <%# GetRDate(Eval("RegisterDate")) %>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("RegisterDate") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                        <asp:TemplateField HeaderText="级别" ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="StoreLevelInt" Text='<%# DataBinder.Eval(Container.DataItem, "StoreLevelInt")%>'> </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle Wrap="False"></FooterStyle>
                    <PagerStyle Wrap="False"></PagerStyle>
                    <EmptyDataTemplate>
                        <table width="100%" class="biaozzi">
                            <tr>
                                <th>
                                    <%#GetTran("000015", "操作")%>
                                </th>
                                <th>
                                    <%#GetTran("000024", "会员编号")%>
                                </th>
                                <th>
                                    <%#GetTran("000037", "店编号")%>
                                </th>
                                <th>
                                    <%#GetTran("000039", "店长姓名")%>
                                </th>
                                <th>
                                    <%#GetTran("000040", "店铺名称")%>
                                </th>
                                <th>
                                    <%#GetTran("000041", "总金额")%>
                                </th>
                                <th>
                                    <%#GetTran("000042", "办店期数")%>
                                </th>
                                <th>
                                    <%#GetTran("000043", "推荐人编号")%>
                                </th>
                                <th>
                                    <%#GetTran("000031", "注册时间")%>
                                </th>
                                <th>
                                    <%#GetTran("000046", "级别")%>
                                </th>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <SelectedRowStyle Wrap="False"></SelectedRowStyle>
                    <HeaderStyle Wrap="False"></HeaderStyle>
                    <AlternatingRowStyle Wrap="False"></AlternatingRowStyle>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                <div style="margin-left: 40px">
                    <uc1:Pager ID="Pager1" runat="server" />
                </div>
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
                                    <img src="images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX"
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
                                                    <asp:LinkButton ID="btndownExcel" Text="导出EXECL" runat="server" OnClick="btndownExcel_Click"
                                                        Style="display: none;"></asp:LinkButton>
                                                    <a href="#">
                                                        <img src="images/anextable.gif" width="49" height="47" border="0" onclick="__doPostBack('btndownExcel','');" /></a>&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <!--<a href="#"><img src="images/anprtable.gif" width="49" height="47" border="0" /></a> -->
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
                                                 1、<%=GetTran("000077", "添加新的店铺.")%><br />
                                                    2、<%=GetTran("000075", "审核自由注册的店铺.")%>
                                                   
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
    </form>
</body>
</html>
