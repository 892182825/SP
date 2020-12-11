﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StoreOffView.aspx.cs" Inherits="Company_StoreOffView" %>
<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/ExpectNum.ascx" TagName="ExpectNum" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link type="text/css" rel="Stylesheet" href="CSS/Company.css" />
    <title>Company_twQuery</title>
    <script language="javascript" type="text/javascript" src="../JS/jquery.js"></script>
     <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
     <script language="javascript" type="text/javascript" src="../js/SqlCheck.js"></script>
     <script language="javascript" type="text/javascript">
    
     var aa=[
        '<%=GetTran("000032", "管 理") %>',
        '<%=GetTran("000033", "说 明") %>',
        '<%=GetTran("000481", "查询条件里面不能输入特殊字符")%>！'
    ];
   
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
 <script language="javascript" type="text/javascript" src="js/memOff.js"></script>
    
</head>
<body  onload="down2()">
    <form id="form1" runat="server">
    <div>
    <br />
        <table width="100%" align="center">
            <tr>
                <td nowrap="nowrap">
                    <table class="biaozzi">
                        <tr>
                            <td nowrap="nowrap">
                               
                                <asp:Button ID="btnWL" runat="server"    Text="服务机构注销"  OnClick="btnWL_Click"  
                                    CssClass="another"/>
                            </td>
                            <td>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                 
                                 <asp:Button ID="btnSeach" runat="server" Text="查 询" OnClick="btnSeach_Click"  CssClass="anyes"  />
                            </td>
                            <td >
                               <%=GetTran("000037", "服务机构编号")%>：
&nbsp;</td>
                            <td >
                                <asp:TextBox ID="txt_member" runat="server" MaxLength="10"></asp:TextBox>
                            </td>
                           
                           
                                <td>
                                <%=GetTran("000613", "日期")%>：<asp:TextBox ID="DatePicker2" runat="server" CssClass="Wdate" 
                                    onfocus="WdatePicker()"></asp:TextBox>
                                
                            </td>
                            <td nowrap="nowrap"><%=GetTran("000045", "期数")%>：
                                <uc2:ExpectNum ID="ExpectNum1" runat="server" />
                            </td>
                             <td nowrap="nowrap"><%=GetTran("001277", "注销状态")%>：<asp:DropDownList ID="DropDownList1" runat="server">
                                 <asp:ListItem Value="0">全部</asp:ListItem>
                                 <asp:ListItem Value="1">已注销</asp:ListItem>
                                 <asp:ListItem Value="2">已恢复</asp:ListItem>
                                 </asp:DropDownList>
&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <!--Gridview显示部分-->
                    <table width="100%">
                        <tr>
                            <td colspan="2" align="center" width="100%">
                                <asp:GridView ID="givTWmember" Width="100%" runat="server" AutoGenerateColumns="False"
                                    CssClass="tablemb" 
                                    onrowdatabound="givTWmember_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="详细信息" HeaderStyle-Wrap="false" Visible="false">
                                            <ItemTemplate>
                                                <img src="images/fdj.gif" /> <asp:LinkButton ID="lbtn_Select" runat="server" CommandName='<%#Eval("Number") %>' OnCommand="lbtn_Select_Click"  Text='<%#GetTran("000035", "详细信息") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       
                                        <asp:BoundField DataField="Storeid" HeaderText="服务机构编号" />
                                       
                                        <asp:TemplateField HeaderText = "店长姓名">
                                            
                                            <ItemTemplate>
                                                <%#GetbyName(Eval("Number").ToString())%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    
                                        <asp:BoundField DataField="zxqishu" HeaderText="注销期数" />
                                       
                                        <asp:TemplateField HeaderText = "注销日期">
                                            
                                            <ItemTemplate>
                                                <%#GetbyDateTime(Eval("zxdate").ToString())%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="操作人编号" DataField="Operator" />
                                        <asp:BoundField HeaderText="操作人姓名" DataField="OperatorName" />
                                        <asp:BoundField DataField="zx" HeaderText="注销状态" />
                                        <asp:TemplateField HeaderText="注销原因" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <label id="lbloffReason1" title='<%=GetTran("007164","注销原因") %>'><%#ShowOffReason(Eval("offReason"))%></label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                    <table width="100%">
                                        <tr>
                                            <th>
                                                <%=GetTran("000037", "服务机构编号")%>
                                            </th>
                                            <th>
                                                <%=GetTran("007571", "店长姓名")%>
                                            </th>
                                            <th>
                                                <%=GetTran("001289", "注销期数")%>
                                            </th>
                                            <th>
                                                <%=GetTran("001292", "注销日期")%>
                                            </th>
                                            <th>
                                                <%=GetTran("004134","操作员编号")%>
                                            </th>
                                            <th>
                                                <%=GetTran("007191", "操作员姓名")%>
                                            </th>
                                            <th>
                                                <%=GetTran("001277", "注销状态")%>
                                            </th>
                                            <th>
                                                <%=GetTran("007164","注销原因")%>
                                            </th>
                                        </tr>
                                        
                                    </table>
                                </EmptyDataTemplate>
                                </asp:GridView>
                            </td>
                        </tr>
                         <tr>
                            <td>
                                <asp:Label ID="lblMassage" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                          
                                <uc1:Pager ID="Pager1" runat="server" />
                                
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <div id="cssrain"  style="width:100%">
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
                                                        <asp:LinkButton ID="btnDownExcel"  runat="server" Text="导出Excel" OnClick="btnDownExcel_Click"
                                                            Style="display: none;"></asp:LinkButton><a href="#"><img src="images/anextable.gif" width="49"
                                                                height="47" border="0" onclick="__doPostBack('btnDownExcel','');" /></a>&nbsp;&nbsp;&nbsp;&nbsp;
                                                       <!-- <a href="#">
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
                                                        １、<%=GetTran("006848", "输入要注册的服务机构编号，使其业绩不向上累计也不产生奖金。")%>
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
        
        <div id="divOffReason" style="position:absolute;left:0px;top:00px;width:300px;height:250px;background-color:white;visibility:hidden;border:#88E0F4 solid 1px; margin-left:-88px; text-align:center;filter:alpha(opacity=90);opacity:0.9;">
            <div style="width:100%;height:20px; background-image:url(../images/tabledp.gif);" align="right">
                <div style="cursor:pointer;color:#FFF;width:30px;padding:3px 3px 0 0; font-size:12px;" onclick="javascript:document.getElementById('divOffReason').style.visibility='hidden';" title='<%=GetTran("000019", "关闭")%>'><%=GetTran("000019", "关闭")%></div>
            </div>
            <div id="divOff" style="width:100%; height:100%; text-align:center;" >
                <textarea id="lblOffReason" readonly="readonly" style="width:98%; height:88%;" rows="4"></textarea>
            </div>
        </div>
          
    </div>
    </form>
   
</body>
</html>
