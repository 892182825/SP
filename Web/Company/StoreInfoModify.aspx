<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StoreInfoModify.aspx.cs"
    Inherits="Company_StoreInfoModify" %>

<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>StoreInfoModify</title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../JS/jquery-1.2.6.js"></script>

    <script  type="text/javascript" language="javascript" src="../js/SqlCheck.js"></script>

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
 <script language="javascript" type="text/javascript">
    function aaa()
    {
   // var str="'";
   // var str1="=";
        for(var i=0;i<form1.elements.length;i++)
        {
            if(form1.elements[i].type=="text")
            {
                if(form1.elements[i].value.indexOf("'")!=-1||form1.elements[i].value.indexOf("=")!=-1)
                {
                    alert('查询条件里面不能输入特殊字符！');
                    return false;
                }
            }
        }
    }
</script>
    <script src="../JS/QCDS2010.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table cellspacing="0" cellpadding="0" border="0" width="100%">
        <tr>
            <td>
            <br />
                <table cellspacing="0" cellpadding="0" border="0" class="biaozzi" width="100%">
                    <tr>
                        <td  style="white-space:nowrap;">
                        <asp:linkbutton id="lkSubmit" style="DISPLAY: none"  Runat="server" Text="提交" onclick="lkSubmit_Click"></asp:linkbutton>
                            <input class="anyes" id="bSubmit" onclick="CheckText()" type="button" value="<%=GetTran("000048", "查 询")%>" />
                            <asp:Button ID="BtnSeach" runat="server" Text="查 询" OnClick="BtnSeach_Click" CssClass="anyes" Visible="false" />&nbsp;
                            <%=GetTran("000150", "店铺编号")%>：<asp:TextBox ID="txtstoreid" runat="server" Width="80px" MaxLength="20"></asp:TextBox>&nbsp;
                             <%=GetTran("000039", "店长姓名")%>：<asp:TextBox ID="txtname" runat="server" Width="80px" MaxLength="100"></asp:TextBox>&nbsp;
                              <%=GetTran("000040", "店铺名称")%>：<asp:TextBox ID="txtstorename" runat="server" Width="80px" MaxLength="100"></asp:TextBox>&nbsp;
                              <%=GetTran("000046", "级别")%>： <asp:DropDownList runat="server" ID="dplLevel">
                            </asp:DropDownList>&nbsp;
                               <%=GetTran("000045", "期数")%>： <asp:DropDownList runat="server" ID="drpExpectNum">
                            </asp:DropDownList>&nbsp;
                            &nbsp;<%=GetTran("000031", "注册时间")%>：<asp:TextBox ID="txtBox_OrderDateTimeStart"
                                runat="server" Width="80px" CssClass="Wdate" onfocus="new WdatePicker()"></asp:TextBox>
                            <%=GetTran("000068")%>：<asp:TextBox ID="txtBox_OrderDateTimeEnd" runat="server" Width="80px"
                                onfocus="new WdatePicker()" CssClass="Wdate"></asp:TextBox>
                            </td>
                    </tr>
                </table>
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <table class="tablemb" cellspacing="0" cellpadding="0" border="0" width="100%">
                    <tr>
                        <td>
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="givSearchStoreInfo_RowCommand"
                                OnRowDataBound="GridView1_RowDataBound" Width="100%">
                                <AlternatingRowStyle BackColor="#F1F4F8" />
                                <RowStyle HorizontalAlign="Center" />
                                <HeaderStyle CssClass="tablebt" />
                                <HeaderStyle Wrap="false" />
                                <Columns>
                                    <asp:TemplateField HeaderText="操作" ShowHeader="False" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="DelLink" runat="server" CommandName="del" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id")%>'><%#GetTran("000022", "删除")%></asp:LinkButton>
                                            <asp:HyperLink ID="Hyperlink1" runat="server" Text="" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "id", "UpdStore.aspx?id={0:d}") %>'
                                                NAME="Hyperlink1"><%#GetTran("000036", "编辑")%></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="店编号" DataField="StoreID" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                    <asp:BoundField DataField="Number" HeaderText="会员编号" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                     <asp:BoundField HeaderText="店长姓名" DataField="Name" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                    <asp:BoundField HeaderText="店铺名称" DataField="StoreName" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                    <asp:BoundField HeaderText="办店期数" DataField="ExpectNum" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                    <asp:BoundField HeaderText="推荐人编号" DataField="Direct" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                  <%--  <asp:BoundField HeaderText="省份" DataField="Province" ItemStyle-Wrap="false" />--%>
                                    <asp:TemplateField HeaderText="注册时间">
                                        <ItemTemplate>
                                                <%# GetRDate(Eval("RegisterDate")) %>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("RegisterDate") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="店铺所属国家" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="country" Text='<%# DataBinder.Eval(Container.DataItem, "SCPCCode")%>'> </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                                    <asp:TemplateField HeaderText="级别" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                        <ItemTemplate>
              <%--                              <asp:Label runat="server" ID="StoreLevelInt" Text='<%# DataBinder.Eval(Container.DataItem, "StoreLevelInt")%>'> </asp:Label>--%>
                                          <asp:Label runat="server" ID="StoreLevelInt" Text='<%# getStoLevelStr(DataBinder.Eval(Container.DataItem, "storeid").ToString()) %>'> </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <table width="100%" style="white-space:nowrap">
                                        <tr style="white-space:nowrap">
                                            <th style="white-space:nowrap">
                                                <%#GetTran("000015", "操作")%>
                                            </th>

                                     <th style="white-space:nowrap">  
                                        <%#GetTran("000024", "会员编号")%>
                                    </th>
                                     <th style="white-space:nowrap">
                                        
                                        <%#GetTran("000037", "店编号")%>
                                    </th>
                                    <th style="white-space:nowrap">
                                        
                                        <%#GetTran("000039", "店长姓名")%>
                                    </th>
                                    <th style="white-space:nowrap">
                                        
                                        <%#GetTran("000040", "店铺名称")%>
                                    </th>
                                    <th style="white-space:nowrap">
                                        
                                        <%#GetTran("000042", "办店期数")%>
                                    </th>
                                    <th style="white-space:nowrap">
                                        
                                        <%#GetTran("000454", "店铺所属国家")%>
                                    </th>
                                    <th style="white-space:nowrap">
                                    <%#GetTran("000043", "推荐人编号")%>
                                        
                                    </th>
                                    <th style="white-space:nowrap">
                                        
                                        <%#GetTran("000057", "注册日期")%>
                                    </th>
                                    <th style="white-space:nowrap">
                                        
                                        <%#GetTran("000046", "级别")%>
                                    </th>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" border="0" width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <uc1:Pager ID="Pager1" runat="server" />
                        </td>
                    </tr>
                </table>
                <br />
          <br />
             <br />
            </td>
        </tr>
        <tr>
            <td>
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
                                                    <asp:LinkButton ID="download" Text="导出EXECL" runat="server" OnClick="download_Click"
                                                        Style="display: none;"></asp:LinkButton><a href="#"><img src="images/anextable.gif" width="49"
                                                            height="47" border="0" onclick="__doPostBack('download','');" /></a>&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <!--<a href="#">
                                                        <img src="images/anprtable.gif" width="49" height="47" border="0" /></a>-->
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
                                                    1、
                                                    <%=GetTran("000746", "根据条件查询到店铺，点击“编辑“修改店铺的基本信息进行修改，并可删除不存在业务关系的店铺。")%>
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
    <%= msg%>
    </form>
</body>
</html>
