<%@ Page Language="C#" AutoEventWireup="true" CodeFile="changeExcept.aspx.cs" Inherits="Company_changeExcept"
    EnableEventValidation="false" %>

<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">


<head id="Head1" runat="server">
<script src="../JS/QCDS2010.js" type="text/javascript"></script>
<script src="../JS/SqlCheck.js" type="text/javascript"></script>
<link href="CSS/Company.css" rel="stylesheet" type="text/css" />

    <script src="js/companyview.js" type="text/javascript"></script>
    <title>
        <%=GetTran("002038", "期数调整")%></title>

  
 
 
    <script type="text/javascript">
     
     function  getwebpro()
     {
        var pm1='<%=GetTran("000129", "对不起，会员编号不能为空！")%>';
        var pm2='<%=GetTran("006084", "对不起，查找不到该会员报单！")%>';
        var pm='';
        
        return {numnul:pm1,noord:pm2};
     }
        
        function CheckText(btname)
	{
		//这个方法是页面有多个按钮要提交时，多次使用这个方法,传入按钮的ID
		filterSql_II (btname);
		
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
<body onload="GetOrder()">
    <div >
        <form id="Form1" method="post" runat="server">
        <br />
        <table id="tbSearch" runat="server" class="biaozzi" style="width: 100%;">
            <tr>
                <td>
                    <input type="button" value='<%=GetTran("002038", "期数调整")%>' class="anyes" onclick="javascript:document.getElementById('tbSearch').style.display='none';javascript:document.getElementById('tbChange').style.display='';" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    

                    <asp:Button ID="btnSearch" CssClass="anyes" runat="server" Text="查 询"  OnClick="btnSearch_Click" />
                    
                    &nbsp;&nbsp;
                    
                    <%=GetTran("000024", "会员编号")%>：<asp:TextBox ID="txtBianhao" runat="server" MaxLength="15" ></asp:TextBox>
                    <%=GetTran("005939", "报单号")%>：<asp:TextBox ID="txtOrderId" runat="server" MaxLength="20"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="biaozzi" width="100%">
                        <tr>
                            <td valign="top" align="left" style="border: rgb(147,226,244) solid 1px">
                                <asp:GridView ID="GridView1" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False"
                                    BorderStyle="Solid" OnRowDataBound="GridView1_RowDataBound" CssClass="tablemb bordercss">
                                    <AlternatingRowStyle BackColor="#F1F4F8" />
                                    <RowStyle HorizontalAlign="Center" />
                                    <Columns>
                                        <asp:BoundField DataField="Number" HeaderText="会员编号"></asp:BoundField>
                                        <asp:BoundField DataField="petname" HeaderText="会员昵称"></asp:BoundField>
                                        <asp:BoundField DataField="OrderId" HeaderText="报单号"></asp:BoundField>
                                        <asp:BoundField DataField="OldOrderExpect" HeaderText="报单期数"></asp:BoundField>
                                        <asp:BoundField DataField="NewOrderExpect" HeaderText="调整报单期数"></asp:BoundField>
                                        <asp:BoundField DataField="OldPayExpect" HeaderText="支付期数"></asp:BoundField>
                                        <asp:BoundField DataField="NewPayExpect" HeaderText="调整支付期数"></asp:BoundField>
                                        <asp:TemplateField HeaderText="调整时间">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# GetUpdateDate(Eval("UpdateDate")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="OperateUser" HeaderText="调整人"></asp:BoundField>
                                        <asp:TemplateField HeaderText="期数调整原因" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <%#GetReason(Eval("UpdateExpectReason").ToString())%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                         </td></tr>
                            <tr>
                                <th>
                                    <%=this.GetTran("000024", "会员编号")%>
                                </th>
                                <th>
                                    <%=this.GetTran("005939", "报单号")%>
                                </th>
                                <th>
                                    <%=this.GetTran("002055", "报单期数")%>
                                </th>
                                <th>
                                   <%=this.GetTran("002037", "调整报单期数")%> 
                                </th>
                                <th>
                                   <%=this.GetTran("002058", "支付期数")%> 
                                </th>
                                <th>
                                   <%=this.GetTran("002059", "调整支付期数")%> 
                                </th>
                                <th>
                                    <%=this.GetTran("002061", "调整时间")%>
                                </th>
                                <th>
                                    <%=this.GetTran("002062", "调整人")%>
                                </th>
                                <th>
                                    <%=GetTran("007210", "期数调整原因")%>
                                </th>
                            </tr>
                                <tr>
                                <td colspan="2" style="text-align:center">  
                                        <%=this.GetTran("001742", "暂无数据")%>!    
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td align="right">
                                <uc1:Pager ID="Pager1" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div id="divOffReason" style="position:absolute;left:0px;top:00px;width:300px;height:250px;background-color:white;visibility:hidden;border:#88E0F4 solid 1px; margin-left:-98px; text-align:center;filter:alpha(opacity=90);opacity:0.9;">
            <div style="width:100%;height:20px; background-image:url(../images/tabledp.gif);" align="right">
                <div style="cursor:pointer;color:#FFF;width:30px;padding:3px 3px 0 0; font-size:12px;" onclick="javascript:document.getElementById('divOffReason').style.visibility='hidden';" title='关闭'>关闭</div>
            </div>
            <div id="divOff" style="width:100%; height:100%; text-align:center;" >
                <textarea id="lblOffReason" readonly="readonly" style="width:98%; height:88%;" rows="4"></textarea>
            </div>
        </div>
        <br>
        <div>
            <table width="600" id="tbChange" border="0" align="center" cellpadding="0" cellspacing="0"   runat="server"  class="tablett" style="display: none;">
                <tr>
                    <td>
                        <table align="center" cellpadding="0" cellspacing="1" border="0" style="width: 100%">
                            <tr>
                                <th align="center" colspan="2" >
                                    <%=GetTran("002038", "期数调整")%>
                                </th>
                            </tr>
                            <tr>
                                <td align="right" style="width:40%;background-color:#EBF1F1;">
                                    <%=GetTran("000024", "会员编号")%>：
                                </td>
                                <td align="left" bgcolor="#F8FBFD">
                                    <asp:TextBox ID="txt_number" runat="server" MaxLength="12" onblur="GetOrders()"></asp:TextBox>
                                     <span id="mbname"  style=" padding-left:20px;"></span></td>
                            </tr>
                            <tr>
                                <td align="right" style="width:40%;background-color:#EBF1F1;">
                                    <%=GetTran("006055", "调整类型")%>：
                                </td>
                                <td align="left" bgcolor="#F8FBFD">
                                    <input id="rdRegister" type="radio" name="rdBtn" checked="checked" onmouseup="CheckType(0)" /><%=GetTran("000029", "注册期数")%>
                                    <input id="rdOrder" type="radio"  name="rdBtn" onmouseup="CheckType(1)" /><%=GetTran("002055", "报单期数")%>
                                </td>
                            </tr>
                            <tr id="trOrderId" runat="server" style="display: none;">
                                <td align="right" style="width:40%;background-color:#EBF1F1;">
                                    <%=GetTran("005939", "报单号")%>：
                                </td>
                                <td align="left" bgcolor="#F8FBFD">
                                    <asp:DropDownList ID="ddlOrderId" runat="server" Width="100px">
                                    
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtOrder" runat="server" Style="display: none;"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width:40%;background-color:#EBF1F1;">
                                    <%=GetTran("000061", "选择期数")%>：
                                </td>
                                <td align="left" bgcolor="#F8FBFD">
                                    <asp:DropDownList ID="DpExcept" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width:40%; background-color:#EBF1F1;">
                                    <%=GetTran("007210","期数调整原因")%>：
                                </td>
                                <td align="left" bgcolor="#F8FBFD">
                                    <asp:TextBox ID="txtUpdateExpectReason" runat="server" TextMode="MultiLine" Height="60px" Width="220px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Button ID="btnOK" runat="server" Text="确 定" OnClick="btnOK_Click" CssClass="anyes" />&nbsp;&nbsp;&nbsp;
                                    <input type="button" value='<%=GetTran("000421", "返回")%>' class="anyes" onclick="javascript:document.getElementById('tbChange').style.display='none';javascript:document.getElementById('tbSearch').style.display='';" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                  <tr>
                <td valign="top">
                    
                </td>
            </tr>
            </table>
        </div>
        <div id="cssrain" style="width:100%">
                        <table width="99%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
                            <tr>
                                <td width="80">
                                    <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
                                        <tr>
                                             <td class="sec2" onclick="secBoard(0)">
                    <span id="span1" title="" ><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
                </td>
                <td class="sec1" onclick="secBoard(1)" style="display:none">
                    <span id="span2" title=""  ><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
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
                            <table width="99%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
                                <tbody style="display: block" id="tbody0">
                                    <tr>
                                        <td valign="bottom" style="padding-left: 20px">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        １、<%=GetTran("006929", "调整会员的报单期数；")%>
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td>
                                                        ２、<%=GetTran("006930", "如果调整后的报单期数小于当前最大期，则结算时必须从修改后的这期开始按顺序重新结算，一直到当前期。")%>
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
                                                        １、<%=GetTran("006839", "输入要调网的会员编号，修改推荐或安置编号就能完成，一个任意大的网络的整体移动及相关处理。")%>。
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                          
                        </div>
                    </div>
        <%=msg %>
        </form>
</body>
</html>
