<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RefundmentOrderBrowse.aspx.cs"
    Inherits="Company_RefundmentOrderBrowse" %>

<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <%=GetTran("001785", "退货浏览")%></title>

    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>

    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />

    <script src="../JS/QCDS2010.js" type="text/javascript"></script>
<script language="javascript" src="../js/SqlCheck.js" type="text/javascript"></script>
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
<!--
//分别是奇数行默认颜色,偶数行颜色,鼠标放上时奇偶行颜色
    var aBgColor = ["#F1F4F8","#FFFFFF","#FFFFCC","#FFFFCC"];
    //从前面iHead行开始变色，直到倒数iEnd行结束
    function addTableListener(o,iHead,iEnd)
    {
        o.style.cursor = "normal";
        iHead = iHead > o.rows.length?0:iHead;
        iEnd = iEnd > o.rows.length?0:iEnd;
        for (var i=iHead;i<o.rows.length-iEnd ;i++ )
        {
            o.rows[i].onmouseover = function(){setTrBgColor(this,true)}
            o.rows[i].onmouseout = function(){setTrBgColor(this,false)}
        }
    }
    function setTrBgColor(oTr,b)
    {
        oTr.rowIndex % 2 != 0 ? oTr.style.backgroundColor = b ? aBgColor[3] : aBgColor[1] : oTr.style.backgroundColor = b ? aBgColor[2] : aBgColor[0];
    }
    window.onload = function(){addTableListener(document.getElementById('<%=Session["GridViewID"] %>'),0,0);}

function MM_jumpMenu(targ,selObj,restore){ //v3.0
  eval(targ+".location='"+selObj.options[selObj.selectedIndex].value+"'");
  if (restore) selObj.selectedIndex=0;
}
//-->
    </script>

    <script language="javascript">
     function secBoard(n)
  
  {
  //  document.Form1.sa.value=n;
  //  document.getElementById("s").click();
       for(i=0;i<secTable.cells.length;i++)
      secTable.cells[i].className="sec1";
    secTable.cells[n].className="sec2";
    for(i=0;i<mainTable.tBodies.length;i++)
      mainTable.tBodies[i].style.display="none";
    mainTable.tBodies[n].style.display="block";
  }
    </script>

    <style type="text/css">
        .style1
        {
            width: 40%;
            text-align: right;
        }
    </style>

    <script language="javascript" type="text/javascript">
        function ShowTbInfo(DocId,StoreId,TotalMoney,TotalPv,Date)
        {
            document.getElementById("lblDocId").innerHTML=DocId;
            document.getElementById("lblStoreId").innerHTML=StoreId;
            document.getElementById("lblTotalMoney").innerHTML=TotalMoney;
            document.getElementById("lblTotalPv").innerHTML=TotalPv;
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
            var str = AjaxClass.ShenHe(document.getElementById("lblDocId").innerHTML,document.getElementById("lblStoreId").innerHTML,document.getElementById("ddlWareHouse").value,document.getElementById("ddlDepotSeat").value).value;
            if(str=="")
            {
                alert('<%=GetTran("000858", "审核成功！")%>');
                window.location.href="RefundmentOrderBrowse.aspx";
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
<body >
    <form id="Form1" method="post" runat="server" onsubmit="CheckSql()">
    <br />
    <table id="tbSearch" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <table class="biaozzi" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width:5%;">
                            <asp:Button ID="btnAdd" runat="server" Text="添加退货单" OnClick="btnAdd_Click" CssClass="anyes" />
                               &nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td style="width:5%;">
                            <asp:Button  ID="btn_Submit" runat="server" Text="查 询" OnClick="btn_Submit_Click"
                                CssClass="anyes" ></asp:Button>&nbsp;
                        </td>
                        <td style="width:5%;">
                            &nbsp;<%=GetTran("000058", "请选择国家")%>：<asp:DropDownList ID="DropCurrency" runat="server">
                            </asp:DropDownList>
                            &nbsp;
                            <%=GetTran("001819", "条件")%>：
                            <asp:DropDownList ID="DropDownList_Items" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_Items_SelectedIndexChanged">
                                <asp:ListItem Value="client">退货店铺</asp:ListItem>
                                <asp:ListItem Value="ExpectNum">期数</asp:ListItem>
                                <asp:ListItem Value="totalMoney">总价格</asp:ListItem>
                                <asp:ListItem Value="docMakeTime">退货日期</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;
                            <asp:DropDownList ID="DropDownList_condition" runat="server">
                            </asp:DropDownList>
                            &nbsp;
                        </td>
                        <td style="width:5%;">
                            <asp:TextBox ID="DatePicker1" Style="width: 120px" onfocus="new WdatePicker()" runat="server" />&nbsp;
                            <asp:TextBox ID="txtCondition" runat="server" MaxLength="100" />&nbsp;
                        </td>
                        <td style="width:5%;">
                            <asp:DropDownList ID="DDPStatus" runat="server">
                                <asp:ListItem Value="StateFlag=0 And CloseFlag=0">未审核</asp:ListItem>
                                <asp:ListItem Value="StateFlag=1">已审核</asp:ListItem>
                                <asp:ListItem Value="CloseFlag=1">无效</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="width:5%;">
                            &nbsp;
                            <%=GetTran("001800", "的退单记录")%>&nbsp;
                        </td>
                        <td></td>
                    </tr>
                </table>
                <br />
                <table  width="100%">
                    <tr>
                        <td style="border:rgb(147,226,244) solid 1px">
                            <asp:GridView ID="gvRefundmentBrowse" runat="server" AutoGenerateColumns="False"
                                OnRowCommand="gvRefundmentBrowse_RowCommand" Width="100%" CssClass="tablemb bordercss"
                                 OnRowDataBound="gvRefundmentBrowse_RowDataBound1">
                                 <AlternatingRowStyle BackColor="#F1F4F8" />
                                <RowStyle HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField HeaderText="审核退货单" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                        <ItemTemplate>
                                            <input id="hidStoreId" type="hidden" value='<%# DataBinder.Eval(Container.DataItem,"client") %>'
                                                runat="server" name="hidStoreId" />
                                            <asp:HiddenField ID="hidTotalMoney" runat="server" Value='<%# Eval("totalMoney") %>' />
                                            <asp:HiddenField ID="hidTotalPv" runat="server" Value='<%# Eval("totalPV") %>' />
                                            <asp:HiddenField ID="hidDocId" runat="server" Value='<%# Eval("docID") %>' />
                                            <asp:HiddenField ID="hidDate" runat="server" Value='<%# Eval("docMakeTime") %>' />
                                            <asp:Literal ID="ltAuditing" runat="server"></asp:Literal>
                                            <asp:LinkButton ID="btnnouse" Text='<%#GetTran("001069", "无效")%>' CommandArgument='<%# DataBinder.Eval(Container.DataItem,"docID") %>'
                                                CommandName="NoEffect" runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                        <ItemStyle Wrap="False"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="退入库" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                        <ItemTemplate>
                                            <%# GetTran("001009", GetWareHouseNameByID (DataBinder.Eval(Container.DataItem , "warehouseID").ToString()) )%>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                        <ItemStyle Wrap="False"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="退货单详细" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                        <ItemTemplate>
                                           <img src="images/fdj.gif" /> <%#GetDetailURL(DataBinder.Eval(Container.DataItem,"docID").ToString() ,"RefundmentOrderDetails.aspx")%>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                        <ItemStyle Wrap="False"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="client" SortExpression="client" HeaderText="退货店铺" HeaderStyle-Wrap="false"
                                        ItemStyle-Wrap="false">
                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                        <ItemStyle Wrap="False"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="docID" SortExpression="docID" HeaderText="退货单号" HeaderStyle-Wrap="false"
                                        ItemStyle-Wrap="false">
                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                        <ItemStyle Wrap="False"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ExpectNum" SortExpression="ExpectNum" HeaderText="期数"
                                        HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                        <ItemStyle Wrap="False"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="是否审核" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "StateFlag").ToString().Trim() == "1" ? GetTran("000233", "是") : GetTran("000235", "否")%>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                        <ItemStyle Wrap="False"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="是否失效" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "CloseFlag").ToString().Trim() == "1" ? GetTran("000233", "是") : GetTran("000235", "否")%>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                        <ItemStyle Wrap="False"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="退货总价">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" name="lblTotalMoney" Text='<%# Eval("totalMoney") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle HorizontalAlign="Right" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="totalPV" HeaderText="退货总积分" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="退货日期" SortExpression="dat_docMakeTime">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# GetOrderDate(Eval("docMakeTime")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="查看备注" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                        <ItemTemplate>
                                            <%#SetVisible(Eval("note").ToString())%>
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
                                                <%=GetTran("001802", "审核退货单")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("001805", "退入库")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("001806", "退货单详细")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("001808", "退货店铺")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("001809", "退货单号")%>
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
                                                <%=GetTran("001812", "退货总价")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("001813", "退货总积分")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("001814", "退货日期")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("000744", "查看备注")%>
                                            </th>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0">
                    <tr>
                        <td>
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            <uc1:Pager ID="Pager1" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <table id="tbInfo" class="tablemb" style="width: 50%; display: none;">
        <tr>
            <th colspan="2">
                <%=GetTran("006030", "退货审核")%>
            </th>
        </tr>
        <tr>
            <td class="style1">
                <%=GetTran("001809", "退货单号")%>：
            </td>
            <td>
                <asp:Label ID="lblDocId" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <%=GetTran("001808", "退货店铺")%>：
            </td>
            <td>
                <asp:Label ID="lblStoreId" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <%=GetTran("001812", "退货总价")%>：
            </td>
            <td>
                <asp:Label ID="lblTotalMoney" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <%=GetTran("001813", "退货总积分")%>：
            </td>
            <td>
                <asp:Label ID="lblTotalPv" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <%=GetTran("001814", "退货日期")%>：
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
    <div id="divOffReason" style="position:absolute;left:0px;top:0px;width:300px;height:250px;background-color:white;visibility:hidden;border:#88E0F4 solid 1px; margin-left:-108px;text-align:center;filter:alpha(opacity=90);opacity:0.9;">
            <div style="width:100%;height:20px; background-image:url(../images/tabledp.gif);" align="right">
                <div style="cursor:pointer;color:#FFF;width:30px;padding:3px 3px 0 0; font-size:12px;" onclick="javascript:document.getElementById('divOffReason').style.visibility='hidden';" title='<%=GetTran("000019", "关闭")%>'><%=GetTran("000019", "关闭")%></div>
            </div>
            <div id="divOff" style="width:100%; height:100%; text-align:center;" >
                <textarea id="lblOffReason" readonly="readonly" style="width:98%; height:88%;" rows="4"></textarea>
            </div>
        </div>
    <div id="cssrain" style="width:100%;">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
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
                            onclick="down2()" align="middle" /></a>
                </td>
            </tr>
        </table>
        <div id="divTab2" style="display: none;">
            <table width="100%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
                <tbody style="display: block">
                    <tr>
                        <td valign="bottom" style="padding-left: 20px">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <a href="#">
                                            <asp:Button ID="bunExportExcel" runat="server" Text="Button" OnClick="bunExportExcel_Click"
                                                Style="display: none;" /><img onclick="__doPostBack('bunExportExcel','');" src="images/anextable.gif"
                                                    width="49" height="47" border="0" /></a>&nbsp;&nbsp;&nbsp;&nbsp; <a href="#" style="display: none;">
                                                        <img src="images/anprtable.gif" width="49" height="47" border="0" /></a>
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
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <%=GetTran("001815", "1、查询店铺的退货申请。")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <%=GetTran("001816", "2、审核店铺的退货申请。")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <%=GetTran("001817", "3、添加店铺的退货申请。")%>
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
