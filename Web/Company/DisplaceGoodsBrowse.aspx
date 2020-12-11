<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DisplaceGoodsBrowse.aspx.cs"
    EnableEventValidation="false" Inherits="Company_DisplaceGoodsBrowse" %>

<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/Country.ascx" TagName="Country" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>换货单审核管理</title>

    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>

    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
<script language="javascript" src="../js/SqlCheck.js"></script>

    <script src="../JS/QCDS2010.js" type="text/javascript"></script>

    <style type="text/css">
        .style1
        {
            width: 40%;
            text-align: right;
        }
    </style>

    <script type="text/javascript">
        window.onerror=function ()
        {
            return true;
        };
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
			document.getElementById("imgX").src="../Company/images/dis1.GIF";
			
		}
		else
		{
			document.getElementById("divTab2").style.display="none";
			document.getElementById("imgX").src="../Company/images/dis.GIF";
		}
	}
    </script>

    <script language="javascript" type="text/javascript">
     function secBoard(n)
  
  {
       for(i=0;i<secTable.cells.length;i++)
      secTable.cells[i].className="sec1";
    secTable.cells[n].className="sec2";
    for(i=0;i<mainTable.tBodies.length;i++)
      mainTable.tBodies[i].style.display="none";
    mainTable.tBodies[n].style.display="block";
  }
    </script>

    <script language="javascript" type="text/javascript">
        function ShowTbInfo(DocId,StoreId,OutTotalMoney,InTotalMoney,Date)
        {
            document.getElementById("lblDocId").innerHTML=DocId;
            document.getElementById("lblStoreId").innerHTML=StoreId;
            document.getElementById("lblOutTotalMoney").innerHTML=OutTotalMoney;
            document.getElementById("lblInTotalMoney").innerHTML=InTotalMoney;
            document.getElementById("lblDate").innerHTML=Date;
            document.getElementById("tbInfo").style.display='';
            document.getElementById("tbSearch").style.display='none';
        }
        function WareHouseChange(wh)
        {
            var list = AjaxClass.BindDepotSeat(wh.value).value;
            
            while(document.getElementById("ddlDepotSeat").childNodes.length>0)
            {
                document.getElementById("ddlDepotSeat").removeChild(document.getElementById("ddlDepotSeat").childNodes[0]);
            }
            
            for(var i=0 ; i<list.length;i++)
            {
                document.getElementById("ddlDepotSeat").options[i]=new Option(list[i][0],list[i][1]);
            }
            
        }
        function ShenHe()
        {
            var str = AjaxClass.HuanHuoShenHe(document.getElementById("lblDocId").innerHTML,document.getElementById("lblStoreId").innerHTML,document.getElementById("ddlWareHouse").value,document.getElementById("ddlDepotSeat").value).value;
            if(str=="")
            {
                alert('<%=GetTran("000858", "审核成功！")%>');
                window.location.href="DisplaceGoodsBrowse.aspx";
            }
            else
            {
                alert(str);
            }
        }
        //----------------转换汇率----------------------
        var defaultcur;
        
        window.onload=function (){
            defaultcur=document.getElementById("ddlCurrency").value;
        }
        
        //-----------------------------------
        function CheckSql()
        {
            filterSql_III();
        }
        function showControl(e,id,offReason)
        {
            var x=e.clientX-(document.getElementById(id).style.width.replace("px","")-0)/2;
            var y=e.clientY;
            
            document.getElementById(id).style.left=x+"px";
            document.getElementById(id).style.top=y+"px";
            document.getElementById(id).style.visibility="visible";
            document.getElementById("lblOffReason").innerText = offReason;   
        }
    </script>

</head>
<body>
    <form id="Form1" method="post" runat="server" onsubmit="CheckSql()">
    <br />
    <table width="100%" cellpadding="0" cellspacing="0" id="tbSearch">
        <tr>
            <td>
                <table width="100%" class="biaozzi" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 5%;">
                            <asp:Button ID="btnAdd" runat="server" Text="添加换货单" OnClick="btnAdd_Click" CssClass="anyes"/>
                        </td>
                        <td style="width: 50%;">
                            &nbsp;
                            <asp:Button ID="btn_Submit" runat="server" OnClick="btn_Submit_Click1" Text="查 询" CssClass="anyes" />&nbsp;<%=GetTran("000143", "请选择国家")%>
                            &nbsp;<uc2:Country ID="Country1" runat="server" />
                            &nbsp;&nbsp;<%=GetTran("001819", "条件")%>&nbsp;
                            <asp:DropDownList ID="DropDownList_Items" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_Items_SelectedIndexChanged">
                                <asp:ListItem Value="StoreID">换货店铺</asp:ListItem>
                                <asp:ListItem Value="expectnum">期数</asp:ListItem>
                                <asp:ListItem Value="MakeDocDate">换货单日期</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;
                            <asp:DropDownList ID="DropDownList_condition" runat="server">
                                <asp:ListItem>包括</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;
                            <asp:TextBox ID="DatePicker1" runat="server" onfocus="WdatePicker()" Width="80px"></asp:TextBox>
                            <asp:TextBox ID="txtCondition" runat="server" Width="80px" MaxLength="50" />
                            &nbsp;
                            <asp:DropDownList ID="DDPStatus" runat="server">
                                <asp:ListItem Value="StateFlag='N' And CloseFlag='N'">未审核</asp:ListItem>
                                <asp:ListItem Value="StateFlag='Y'">已审核</asp:ListItem>
                                <asp:ListItem Value="CloseFlag='Y'">无效</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="width: 5%;" align="left">
                          <%=GetTran("001821", "的换货单记录")%>
                            &nbsp;
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%">
                    <tr>
                        <td style="border: rgb(147,226,244) solid 1px">
                            <asp:GridView ID="gvDisplaceGoods" runat="server" AutoGenerateColumns="False" DataKeyNames="DisplaceOrderID"
                                OnRowCommand="gvDisplaceGoods_RowCommand" OnRowDataBound="gvDisplaceGoods_RowDataBound"
                                CssClass="tablemb bordercss" Width="100%" EmptyDataText="暂无数据!">
                                <AlternatingRowStyle BackColor="#F1F4F8" />
                                <RowStyle HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField HeaderText="审核换货单" ShowHeader="False" HeaderStyle-Wrap="false"
                                        ItemStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hidOutTotalMoney" runat="server" Value='<%# Eval("OutTotalMoney" , "{0:N2}") %>' />
                                            <asp:HiddenField ID="hidInTotalMoney" runat="server" Value='<%# Eval("InTotalMoney" , "{0:N2}") %>' />
                                            <asp:HiddenField ID="hidDocId" runat="server" Value='<%# Eval("DisplaceOrderID") %>' />
                                            <asp:HiddenField ID="hidDate" runat="server" Value='<%# Eval("MakeDocDate") %>' />
                                            <input id="hidStoreId" runat="server" name="hidStoreId" type="hidden" value='<%# DataBinder.Eval(Container.DataItem,"StoreID") %>' />
                                            <asp:Literal ID="ltAuditing" runat="server"></asp:Literal>
                                            <asp:LinkButton ID="btnnouse" runat="server" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"DisplaceOrderID") %>'
                                                CommandName="NoEffect"><%#GetTran("001069", "无效")%></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                        <ItemStyle Wrap="False"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="编辑换货单" ShowHeader="False" HeaderStyle-Wrap="false"
                                        ItemStyle-Wrap="false" Visible="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"DisplaceOrderID") %>'
                                                CausesValidation="False" CommandName="Edit"><%#GetTran("000036", "编辑")%></asp:LinkButton>
                                            <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"DisplaceOrderID") %>'
                                                CausesValidation="False" CommandName="del"><%#GetTran("000022", "删除")%></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                        <ItemStyle Wrap="False"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="换货单详细" ShowHeader="False" HeaderStyle-Wrap="false"
                                        ItemStyle-Wrap="false">
                                        <ItemTemplate>
                                           <img src="images/fdj.gif" /> <%#GetDetailURL(DataBinder.Eval(Container.DataItem,"DisplaceOrderID").ToString() ,"DisplaceGoodsDetails.aspx")%>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                        <ItemStyle Wrap="False"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderStyle-Wrap="false" ItemStyle-Wrap="false" DataField="StoreID"
                                        HeaderText="换货店铺" SortExpression="StoreID">
                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                        <ItemStyle Wrap="False"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderStyle-Wrap="false" ItemStyle-Wrap="false" DataField="DisplaceOrderID"
                                        HeaderText="换货单号" SortExpression="DisplaceOrderID">
                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                        <ItemStyle Wrap="False"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderStyle-Wrap="false" ItemStyle-Wrap="false" DataField="RefundmentOrderID"
                                        HeaderText="对应退单号" SortExpression="RefundmentOrderID">
                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                        <ItemStyle Wrap="False"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderStyle-Wrap="false" ItemStyle-Wrap="false" DataField="StoreOrderID"
                                        HeaderText="对应订单号" SortExpression="StoreOrderID">
                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                        <ItemStyle Wrap="False"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderStyle-Wrap="false" Visible="false" ItemStyle-Wrap="false" DataField="StoreOrderID"
                                        HeaderText="对应出库单号" SortExpression="OutStrageOrderID">
                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                        <ItemStyle Wrap="False"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderStyle-Wrap="false" ItemStyle-Wrap="false" DataField="ExpectNum"
                                        HeaderText="期数" SortExpression="ExpectNum">
                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                        <ItemStyle Wrap="False"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="是否审核" SortExpression="StateFlag">
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" 
                                                Text='<%# GetYesOrNo(Eval("StateFlag")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("StateFlag") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="是否失效" SortExpression="CloseFlag">
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" 
                                                Text='<%# GetYesOrNo(Eval("CloseFlag")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("CloseFlag") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="退货额">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" name="lblTotalMoney" Text=' <%# Eval("OutTotalMoney", "{0:N2}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle HorizontalAlign="Right" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="进货额">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" name="lblTotalMoney" Text='<%# Eval("InTotalMoney", "{0:N2}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle HorizontalAlign="Right" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderStyle-Wrap="false" ItemStyle-Wrap="false" DataField="MakeDocDate"
                                        HeaderText="换货单日期" SortExpression="MakeDocDate" DataFormatString="{0:d}">
                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                        <ItemStyle Wrap="False"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="查看备注" SortExpression="Remark" HeaderStyle-Wrap="false"
                                        ItemStyle-Wrap="false">
                                        <ItemTemplate>
                                            <%#SetVisible(Eval("Remark").ToString())%>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                        <ItemStyle Wrap="False"></ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <table cellspacing="0" cellpadding="0" rules="all" border="1" style="width: 100%;
                                        border-collapse: collapse;">
                                        <tr>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("001856", "审核换货单")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("001860", "编辑换货单")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("001862", "换货单详细")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("001863", "换货店铺")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("001864", "换货单号")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("001866", "对应退单号")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("001867", "对应订单号")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("000099", "对应出库单号")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("000045", "期数")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("000605", "是否审核")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("001811", "是否失效")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("001875", "退货额")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("001876", "进货额")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("001878", "换货单日期")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("000744", "查看备注")%>
                                            </th>
                                        </tr>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                
            </td>
        </tr>
    </table>
    <div id="divOffReason" style="position:absolute;left:0px;top:0px;width:300px;height:250px;background-color:white;visibility:hidden;border:#88E0F4 solid 1px; margin-left:-108px;text-align:center;filter:alpha(opacity=90);opacity:0.9;">
            <div style="width:100%;height:20px; background-image:url(../images/tabledp.gif);" align="right">
                <div style="cursor:pointer;color:#FFF;width:30px;padding:3px 3px 0 0; font-size:12px;" onclick="javascript:document.getElementById('divOffReason').style.visibility='hidden';" title='<%=GetTran("000019", "关闭")%>'><%=GetTran("000019", "关闭")%></div>
            </div>
            <div id="divOff" style="width:100%; height:100%; text-align:center;" >
                <textarea id="lblOffReason" readonly="readonly" style="width:98%; height:88%;" rows="4"></textarea>
            </div>
        </div>
    <table width="100%" border="0">
                    <tr>
                        <td>
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            <uc1:Pager ID="Pager1" runat="server" />
                        </td>
                    </tr>
                </table>
    <br />
    <table id="tbInfo" class="tablemb" style="width: 50%; display: none;">
        <tr>
            <th colspan="2">
                <%=GetTran("001856", "审核换货单")%>
            </th>
        </tr>
        <tr>
            <td class="style1">
                <%=GetTran("001864", "换货单号")%>：
            </td>
            <td>
                <asp:Label ID="lblDocId" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <%=GetTran("001863", "换货店铺")%>：
            </td>
            <td>
                <asp:Label ID="lblStoreId" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <%=GetTran("001875", "退货总额")%>：
            </td>
            <td>
                <asp:Label ID="lblOutTotalMoney" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <%=GetTran("001876", "进货总额")%>：
            </td>
            <td>
                <asp:Label ID="lblInTotalMoney" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <%=GetTran("001878", "换货日期")%>：
            </td>
            <td>
                <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <%=GetTran("006031", "退入仓库")%>：
            </td>
            <td>
                <asp:DropDownList ID="ddlWareHouse" runat="server" onChange="WareHouseChange(this)">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <%=GetTran("006032", "退入库位")%>：
            </td>
            <td>
                <asp:DropDownList ID="ddlDepotSeat" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <input type="button" onclick="ShenHe()" value='<%=GetTran("000761", "审核")%>' class="anyes" />&nbsp;
                &nbsp;
            </td>
            <td>
                &nbsp; &nbsp;
                <input type="button" onclick="javascript:document.getElementById('tbInfo').style.display='none';document.getElementById('tbSearch').style.display='';"
                    value='<%=GetTran("000421", "返回")%>' class="anyes" />
            </td>
        </tr>
    </table>
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
                            onclick="down2()" /></a>
                </td>
            </tr>
        </table>
        <div id="divTab2" style="display: none;">
            <table width="99%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
                <tbody style="display: block">
                    <tr>
                        <td valign="bottom" style="padding-left: 20px">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <a href="#">
                                            <asp:ImageButton ID="btnExecel" runat="server" OnClick="btnExecel_Click" ImageUrl="images/anextable.gif" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </tbody>
                <tbody style="display: none">
                    <tr>
                        <td style="padding-left: 20px">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        1、<%=GetTran("001828", "查询店铺的换货申请")%>。
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        2、<%=GetTran("001829", "审核店铺的换货申请")%>。
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        3、<%=GetTran("001830", "添加店铺的换货申请")%>。
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
