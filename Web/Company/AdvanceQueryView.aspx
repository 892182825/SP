<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdvanceQueryView.aspx.cs" Inherits="Company_AdvanceQueryView"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <meta http-equiv="Content-Type" content="text/html; charset=GB2312">
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
</script>
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
<body  onload="down2()">
    <form id="form1" runat="server">
    <br />
    <div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td valign="top"><br /><div>
				<table width="100%" cellpadding="0" cellspacing="0"
					border="0" align="center" class="biaozzi">
					<tr>
						<TD colspan="4"><span style="display:none"> <%=GetTran("000909", "当前货币")%>：<asp:Label id="LabelCurrency" runat="server" Visible=false></asp:Label>
						
                            <asp:dropdownlist id="DropCurrency" runat="server" AutoPostBack="True" onselectedindexchanged="DropCurrency_SelectedIndexChanged" 
                                        ></asp:dropdownlist></span>  
                            &nbsp;&nbsp;&nbsp;&nbsp;<%=GetTran("001015", "当前期数")%>：<asp:label id="CurrentGrass" runat="server" ForeColor="#400040" Font-Names="Arial Narrow" Font-Size="Larger"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;
										<INPUT onclick="javascript:window.location='AdvanceQuery.aspx'" type="button" value="<%=GetTran("000096", "返 回")%>" 
											 Class="anyes" style="cursor:hand;"></TD>
					</tr>
				</table>
				<table border="0" cellpadding="0" cellspacing="1"   width="100%" class="biaozzi">
					<tr>
						<TD colspan="4" style="word-break:keep-all;word-wrap:normal;">
							<div id="griddiv" style="overflow:auto">
                                <asp:GridView ID="GridView1" runat="server" ondatabound="GridView1_DataBound" 
                                    onrowcreated="GridView1_RowCreated" onrowdatabound="GridView1_RowDataBound"
                                    onpageindexchanging="GridView1_PageIndexChanging" 
                                    onsorting="GridView1_Sorting" width="100%" >
                                <HeaderStyle HorizontalAlign="Center"  Wrap=false CssClass="tablemb" />
                                <RowStyle Wrap="false" />
                                <SelectedRowStyle HorizontalAlign="Center" Wrap="false"/>
                                                                <AlternatingRowStyle BackColor="#F1F4F8" />
                                                                <RowStyle HorizontalAlign="Center" Wrap="false"/>
                                <FooterStyle HorizontalAlign="Center"/>
                                <PagerSettings Mode="Numeric" NextPageText="下一页" PreviousPageText="上一页" />
                                <PagerStyle HorizontalAlign="Center" />
                                </asp:GridView>
							</div>
						</TD>
					</tr>
					<tr style=" display:none;">
						<td>
							<table >
                                
								<tr>
									<td style="WIDTH: 40px">
										<asp:button id="BtnFirstPage" runat="server" Text="第一页" 
											
                                            onclick="BtnFirstPage_Click" Visible="False"></asp:button></td>
									<td style="WIDTH: 5px">
										<asp:button id="BtnLastPage" runat="server" Text="最后一页"
											
                                            onclick="BtnLastPage_Click" Visible="False"></asp:button></td>
									<td style="WIDTH: 68px">
										&nbsp;</td>
									<td align="left">
										<FONT face="宋体">&nbsp;</FONT></td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
				</div>
				</td>
    </tr>
    <tr>
    <td valign="top">
    <table id="ShowTable" runat="server" height="30px;" width="100%" style="font-family: 宋体;	font-size: 12px;	line-height: 24px;	color: #005575;	text-decoration: none;">
    <tr>
        <td align="left" width="220px" nowrap="nowrap" >
           <asp:Label ID="Label1" runat="server"  class="biaozzi"></asp:Label>
        </td>
        <td nowrap="nowrap" align="right" >
            <table id="pageS" runat="server"  style="font-family: 宋体;	font-size: 12px;	line-height: 24px;	color: #005575;	text-decoration: none;">
                <tr>
                    <td nowrap="nowrap">
                        <asp:Button ID="btnFirst" runat="server" onclick="btnFirst_Click" Text=""  width="49" height="17" border="0" align="absmiddle" style="cursor:hand;background-image:url(images/one.gif)" BorderWidth="0"/>
                    </td>
                    <td nowrap="nowrap">
                       <asp:Button ID="btnPre" runat="server" onclick="btnPre_Click" Text=""  width="49" height="17" border="0" align="absmiddle" style="cursor:hand; background-image:url(images/up.gif)" BorderWidth="0"/>
                    </td>
                    <td nowrap="nowrap">
                        <asp:Button ID="btnNext" runat="server" onclick="btnNext_Click" Text=""  width="49" height="17" border="0" align="absmiddle" style="cursor:hand;background-image:url(images/down.gif)" BorderWidth="0"/>
                    </td>
                    <td nowrap="nowrap">
                        <asp:Button ID="btnLast" runat="server" onclick="btnLast_Click" Text=""  width="49" height="17" border="0" align="absmiddle" style="cursor:hand;background-image:url(images/last.gif)" BorderWidth="0"/>
                    </td>
                    <td nowrap="nowrap" valign="middle"><label><%=GetTran("001022", "跳转到")%></label>
                        <asp:DropDownList ID="dropPageList" runat="server" AutoPostBack="True" onselectedindexchanged="dropPageList_SelectedIndexChanged" 
                            >
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
    <input type="hidden" id="pageIndex" runat="server" value="1" />
    <input type="hidden" id="rCount" runat="server" value="0" />
    <input type="hidden" id="pageCount" runat="server" value="0"/>
										&nbsp;</td>
    </tr>
</table>
</div>	
<br/>
<div id="cssrain" style="width:100%">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
        <tr>
          <td width="150"><table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
               <td class="sec2" onclick="secBoard(0)">
                    <span id="span1" title="" onmouseover="cut()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000032", "管 理"))%></span>
                </td>
                <td class="sec1" onclick="secBoard(1)">
                    <span id="span2" title="" onmouseover="cut1()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
                </td>
                                        </tr>
          </table></td>
          <td><a href="#"><img src="images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX" onclick="down2()"/></a></td>
        </tr>
      </table>
	  <div id="divTab2">
      <table width="100%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
        <tbody style="DISPLAY: block"  id="tbody0">
          <tr>
            <td valign="bottom" style="padding-left:20px"><table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td>
                      <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click" style="display:none;"></asp:LinkButton><a href="#"><img src="images/anextable.gif" width="49" height="47" border="0" onclick="__doPostBack('LinkButton1','');"/></a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                </tr>
            </table></td>
          </tr>
        </tbody>
        <tbody style="DISPLAY: none"  id="tbody1">
          <tr>
            <td style="padding-left:20px"><table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td> １、<%=GetTran("006846", "根据自己的需要，组合出不同内容和形式的奖金、业绩、基本资料等的查询报表。")%>。</td>
                </tr>
            </table></td>
          </tr>
        </tbody>
      </table>
	  </div>
    </div>
    </form>
</body>
</html>
