<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LanguageManage.aspx.cs" Inherits="Company_LanguageManage" %>
<%@ Register Src="../UserControl/PagerSorting.ascx" TagName="Pager" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/Dtd/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=GetTran("006021", "语言管理")%></title>
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
  
     <style type ="text/css" >
     table{
         font-size:9pt;
         }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td style="white-space: nowrap">
                <br />
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
                    <tr>
                        <td align="left" style="white-space: nowrap">
                          &nbsp;
                                <asp:linkbutton id="lkSubmit" Runat="server" Text="查 询" style="DISPLAY: none"></asp:linkbutton>
                           
                            &nbsp;&nbsp;<asp:Button ID="btnSearch" runat="server" CssClass="anyes" 
                                onclick="btnSearch_Click" Text="查询" />

                                    
&nbsp;<%=GetTran("006022", "语言代码")%>：<asp:TextBox ID="txtLanguageCode" MaxLength="8" runat="server" Width="65px"></asp:TextBox>
&nbsp;<%=GetTran("006023", "语言名称")%>：<asp:TextBox ID="txtLanguageName" MaxLength="20" runat="server" Width="165px"></asp:TextBox>
                       &nbsp;
                           
                                <input id="addNew"  type ="button" value='<%=GetTran("006024", "添加新语言")%>' class ="another"  title ='添加新语言' onclick ="javascript:window.location.href='LanguageAdd.aspx?mode=0';" />
                                                  
                                <input id="addNew0" type ="button" value='<%=GetTran("006643", "返回翻译管理")%>' 
                                class ="another"  title ='返回翻译管理' 
                                onclick ="javascript:window.location.href='Translations.aspx?mode=0';" /></td>                      
                    </tr>
                </table>
                <br />
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="tablemb">
                    <tr>
                        <td>
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound"
                                Width="100%" onrowcommand="GridView1_RowCommand">
                                <AlternatingRowStyle BackColor="#F1F4F8" />
                                <RowStyle HorizontalAlign="Center" />
                                <HeaderStyle CssClass="tablebt" />
                                 <Columns>
                                        <asp:TemplateField HeaderText="操作">
                                        <ItemStyle HorizontalAlign ="Center" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnDelLanguage" runat="server" 
                                                    CommandArgument='<%# Eval("ID")%>' 
                                                    CommandName="del" ><%#GetTran("000022", "删除")%></asp:LinkButton>
                                                  <a href='LanguageAdd.aspx?mode=1&id=<%# Eval("ID")%>'><%#GetTran("000036", "编辑")%></a>
                                                 <asp:LinkButton ID="lbtnEdit" runat="server" 
                                                    CommandArgument='<%# Eval("ID")%>' 
                                                    CommandName="edit"  Text="修改" Visible ="false" ></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle Width="80px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="语言代码">                                      
                                            <ItemTemplate>
                                                <asp:Label ID="lblLanguageCode" runat="server" Text='<%# Bind("languageCode") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("languageCode") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="name" HeaderText="语言名称" >                                       
                                            <HeaderStyle HorizontalAlign="Center" Width="160px" />
                                            <ItemStyle HorizontalAlign="Left" Width="160px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LanguageRemark" HeaderText="语言简介" ReadOnly="true">
                                      
                                            <HeaderStyle HorizontalAlign="Center" Width="160px" />
                                            <ItemStyle HorizontalAlign="Left" Width="160px" />
                                        </asp:BoundField>
                                    </Columns>
                               
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="white-space: nowrap">
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="right" style="white-space: nowrap">
                            <uc1:Pager ID="Pager1" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
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
                                                    <%=GetTran("006021", "语言管理")%><br /> 
                    1.<%=GetTran("006021", "语言管理")%><br />
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
