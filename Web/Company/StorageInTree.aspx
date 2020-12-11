<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageInTree.aspx.cs" Inherits="Company_StorageInTree" %>

<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/Country.ascx" TagName="Country" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="CSS/Company.css" />

    <script language="javascript" type="text/javascript" src="../javascript/Mymodify.js"></script>

    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>

    <script src="../JS/QCDS2010.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
    function check(obj)
    {
        if (obj.value=="不限")
        {
        obj.value="";   
        }
    }
    function check2(obj)
    {
        if(obj.value=="")
         {
         obj.value="不限";
         }
    } 
    </script>

    <style type="text/css">
        td
        {
            word-wrap: normal;
            white-space: nowrap;
        }
    </style>

    <script language="javascript" src="../js/SqlCheck.js"></script>

    <script language="javascript" type="text/javascript">
    function CheckText()
	{
		filterSql();
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

    <script type="text/javascript">
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
                window.onload=function()
	    {
	        down2();
	    };
function bSubmit_onclick() {

}

    </script>

</head>
<body>
    <form id="form1" runat="server"> 
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <div>
       
        <br>
 <table width="100%" border="0" cellpadding="0"  cellspacing="0" class="biaozzi">
		            <tr>
						<td align="center" ><asp:label ID="lbl_title" runat="server"></asp:label><%=GetTran("001947", "各产品库存明细表")%></td>
					</tr>
					<tr>
						<td>&nbsp;&nbsp;<asp:label ID="lbl_flag" runat="server" ></asp:label>&nbsp;&nbsp;
							<asp:label ID="lbl_storename" runat="server" ></asp:label></td>
					</tr>		            
		        </table>		
        <table width="100%">
            <tr>
                <td style="border:rgb(147,226,244) solid 1px">
                    <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="False" Width="100%"
                        CssClass="tablemb bordercss" OnRowDataBound="gvProduct_RowDataBound"
                        AlternatingRowStyle-Wrap="False" FooterStyle-Wrap="False" HeaderStyle-Wrap="False"
                        PagerStyle-Wrap="False" SelectedRowStyle-Wrap="False">
            
                        <FooterStyle Wrap="False"></FooterStyle>
                        <Columns>

                            <asp:TemplateField HeaderText="仓库名称" ShowHeader="False">
                                <ItemTemplate>
                                  <%#Eval("WareHouseName")%>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" HorizontalAlign="center" />
                                <HeaderStyle Wrap="false" />
                            </asp:TemplateField>
                           <asp:BoundField DataField="SeatName" HeaderText="库位名称" ItemStyle-Wrap="false" />
                            <asp:TemplateField HeaderText="仓库名称" ShowHeader="False">
                                <ItemTemplate>
                                  <%#Eval("ProductCode")%>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" HorizontalAlign="center" />
                                <HeaderStyle Wrap="false" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="仓库名称" ShowHeader="False">
                                <ItemTemplate>
                                  <%#Eval("ProductName")%>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" HorizontalAlign="center" />
                                <HeaderStyle Wrap="false" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="仓库名称" ShowHeader="False">
                                <ItemTemplate>
                                  <%#Eval("ProductBigUnitName")%>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" HorizontalAlign="center" />
                                <HeaderStyle Wrap="false" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="仓库名称" ShowHeader="False">
                                <ItemTemplate>
                                  <%#Eval("ProductSmallUnitName")%>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" HorizontalAlign="center" />
                                <HeaderStyle Wrap="false" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="仓库名称" ShowHeader="False">
                                <ItemTemplate>
                                  <%#Eval("TotalIn")%>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" HorizontalAlign="center" />
                                <HeaderStyle Wrap="false" />
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="仓库名称" ShowHeader="False">
                                <ItemTemplate>
                                  <%#Eval("TotalOut")%>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" HorizontalAlign="center" />
                                <HeaderStyle Wrap="false" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="仓库名称" ShowHeader="False">
                                <ItemTemplate>
                                  <%#Eval("TotalEnd")%>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" HorizontalAlign="center" />
                                <HeaderStyle Wrap="false" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="仓库名称" ShowHeader="False">
                                <ItemTemplate>
                                  <%#Eval("AlertnessCount")%>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" HorizontalAlign="center" />
                                <HeaderStyle Wrap="false" />
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle Wrap="False"></PagerStyle>
                        <SelectedRowStyle Wrap="False"></SelectedRowStyle>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <AlternatingRowStyle BackColor="#F1F4F8" />
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
                 <p>
                    <asp:Button ID="Button3" runat="server" Text="返 回"  CssClass="anyes" 
                        onclick="Button3_Click" />
                    </p>
                </td>
            </tr>
        </table>
    </div>
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
                <td style="white-space: nowrap">
                    <a href="#"><img src="images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX" 
                            onclick="down2()" /></a>
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
                                        <a href="#">
                                            <asp:ImageButton ID="Butt_Excel" runat="server" ImageUrl="images/anextable.gif" OnClick="Butt_Excel_Click" />
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
                                        1、<%=GetTran("001213", "根据条件查询入库单、出库单、报损单、报溢单、调拨单等各种单据")%>。
                                        <br />
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

