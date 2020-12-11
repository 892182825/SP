<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ThirdLogistics.aspx.cs" Inherits="Company_ThirdLogistics" %>

<%@ Register src="../UserControl/Country.ascx" tagname="Country" tagprefix="uc1" %>

<%@ Register src="../UserControl/Pager.ascx" tagname="Pager" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>杳看物流公司信息</title>
<link href="CSS/Company.css" rel="stylesheet" type="text/css" />
    <script src="../JS/QCDS2010.js" type="text/javascript"></script>
    <script type="text/javascript">
        window.onerror=function ()
        {
            return true;
        };
    </script>
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
      
      function isDelete()
      {
         return window.confirm('<%=GetTran("000248")%>');
      }
</SCRIPT>

</head>
<body>
    <form id="form1" runat="server">
    <br />
    <div>
        <table width="98%" class="biaozzi">
            <tr>
                <td>
                    <asp:Button ID="Button2" runat="server" Text="查 询" CssClass="anyes" 
                        onclick="Button2_Click"/>
                        
                    <%=GetTran("000047", "国家")%>：<%--<uc1:Country ID="Country1" runat="server"  />--%>
                    <asp:DropDownList ID="DropCurrency" runat="server">
                    </asp:DropDownList>
                    <br>
                     <br>
        
                </td>
            </tr>
            <tr>
                <td style="border:rgb(147,226,244) solid 1px">
                    <asp:GridView ID="gvThirdLogistics" runat="server" AutoGenerateColumns="false" 
                        OnRowCommand="gvThirdLogistics_RowCommand" DataKeyNames="ID" width="100%"  
                        cellpadding="0"  CssClass="tablemb bordercss" 
                        onrowdatabound="gvThirdLogistics_RowDataBound">
                        <EmptyDataTemplate>
                            <table border="0" cellspacing="0" width="100%">
                                <tr>                
                                    <th nowrap>
                                        <%=GetTran("000015","操作") %>
                                    </th>
                                    <th nowrap>
                                        <%=GetTran("000015","操作") %>
                                    </th>
                                    
                                    <th nowrap>
                                        <%=GetTran("001897","公司编号") %>
                                    </th>
                                    <th nowrap>
                                        <%=GetTran("001900","公司名称") %>
                                    </th>
                                    <th nowrap>
                                        <%=GetTran("001910","负责人姓名") %>
                                    </th>
                                    <th nowrap>
                                        <%=GetTran("000044","办公电话") %>
                                    </th>
                                    <th nowrap>
                                        <%=GetTran("000071","传真电话") %>
                                    </th>
                                    
                                    
                                    <th nowrap>
                                        <%=GetTran("001918","所在国家") %>
                                    </th>
                  
                                    <th nowrap>
                                        <%=GetTran("001921","所在省份") %>
                                    </th>
                                    <th nowrap>
                                            <%=GetTran("001923","所在城市") %>
                                        </th>
                                        <th nowrap>
                                           <%=GetTran("000087", "开户银行")%> 
                                        </th>
                                        <th nowrap>
                                            <%=GetTran("000088","银行帐号") %>
                                        </th>
                                    <th nowrap>
                                        <%=GetTran("000962", "税号")%>
                                    </th>
                                    <th nowrap>
                                        <%=GetTran("001981", "批准日期")%>
                                    </th>
                                    <th nowrap>
                                        <%=GetTran("001983", "营业执照号码")%>
                                    </th>
                                    <th nowrap>
                                        <%=GetTran("000078", "备注")%>
                                    </th>
                                </tr>                
                            </table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="操作">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkModify" runat="server" Text='<%#GetTran("000259","修改") %>' NavigateUrl='<%# DataBinder.Eval(Container, "DataItem.id", "AddThirdLogistics.aspx?action=edit&amp;id={0}") %>'/>
                                     <input id="hiddenID" type="hidden" value='<%Eval("id") %>' name="hiddenID" runat="server" />
                                </ItemTemplate>
                                <ItemStyle Wrap=false />
                                <HeaderStyle Wrap=false />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="操作">
                                <ItemTemplate>
                                    <asp:LinkButton ID="delLink" runat="server" Text='<%#GetTran("000022","删除") %>'  CommandName="del"  CommandArgument='<%#Eval("id") %>' OnClientClick="return isDelete()"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Wrap=false />
                                <HeaderStyle Wrap=false />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Number" SortExpression="Number" HeaderText="公司编号" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"/>
                            <asp:BoundField DataField="LogisticsCompany" SortExpression="LogisticsCompany" HeaderText="公司名称"  ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"/>
                            <asp:BoundField DataField="Principal" SortExpression="Principal" HeaderText="负责人姓名"  ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                            <asp:BoundField Visible="False" DataField="StoreAddress" SortExpression="StoreAddress"
                                HeaderText="办公地址"  ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"/>
                            <asp:BoundField DataField="Telephone1" HeaderText="办公电话"  ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                            <asp:BoundField DataField="Telephone4" HeaderText="传真电话"  ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                            <asp:BoundField Visible="False" DataField="Telephone1" HeaderText="负责人电话"  ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"/>
                            <asp:BoundField Visible="False" DataField="Telephone3" HeaderText="负责人手机"  ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"/>
                            <asp:BoundField DataField="Country" SortExpression="Country" HeaderText="所在国家"  ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"/>
                            <asp:BoundField DataField="Province" SortExpression="Province" HeaderText="所在省份"  ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"/>
                            <asp:BoundField DataField="City" SortExpression="City" HeaderText="所在城市"  ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                            <asp:BoundField DataField="Bank" SortExpression="Bank" HeaderText="开户银行"  ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"/>
                            <asp:BoundField DataField="BankCard" SortExpression="BankCard" HeaderText="银行帐号"  ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                            <asp:BoundField DataField="Tax" HeaderText="税号"  ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                            <asp:BoundField Visible="False" DataField="Administer" SortExpression="Administer"
                                HeaderText="操作管理员"  ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"/>
                                
                            <asp:TemplateField HeaderText="批准日期">
                                <ItemTemplate>
                                    <asp:Label ID="rqlab" runat="server" Text='<%#GetBiaoZhunTime(Eval("RigisterDate").ToString())%>' ></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap=false />
                                <HeaderStyle Wrap=false />
                            </asp:TemplateField>
                            <asp:BoundField DataField="LicenceCode" SortExpression="LicenceCode" HeaderText="营业执照号码"  ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"/>
                            <asp:TemplateField  HeaderText="备注">
                                <ItemTemplate>
									 <%#SetVisible(Eval("remark").ToString(), Eval("id").ToString())%>					
                                </ItemTemplate>
                                <ItemStyle Wrap=false />
                                <HeaderStyle Wrap=false />
                            </asp:TemplateField>
                        </Columns>
                         <AlternatingRowStyle BackColor="#F1F4F8" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td><uc2:Pager ID="Pager1" runat="server" />            
			<input type="button" id="btnAddLOgistics" value='<%=GetTran("001825", "添加物流公司") %>' onclick="javascript:window.location.href='AddThirdLogistics.aspx';" class="anyes"  />
                    
                </td>
            </tr>
        </table>
    </div>
    
    <br/>
	<div id="cssrain" style="width:100%">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
        <tr>
          <td width="150"><table width="100%" height="28" border="0" cellpadding="0" cellspacing=
          "0" id="secTable">
              <tr>
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
        <tbody style="DISPLAY: block" id="tbody0">
          <tr>
            <td valign="bottom" style="padding-left:20px"><table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td><a href="#">
                                                        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="btnDownLoad_Click" style="display:none;"  /><img
                                                            onclick="__doPostBack('LinkButton1','');" src="images/anextable.gif" width="49" height="47"
                                                            border="0" /></a><asp:LinkButton 
                          ID="LinkButton1" runat="server" onclick="LinkButton1_Click"></asp:LinkButton>
                      &nbsp;&nbsp;&nbsp;&nbsp; <%--<a href="#">
                                                                <img src="images/anprtable.gif" width="49" height="47" border="0" /></a>--%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                </tr>
            </table></td>
          </tr>
        </tbody>
        <tbody style="DISPLAY: none" id="tbody1">
          <tr>
            <td style="padding-left:20px"><table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td> <%=GetTran("001778","1、对第三方物流公司进行添加、修改、删除的操作。")%></td>
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
