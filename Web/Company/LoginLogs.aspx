<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginLogs.aspx.cs" Inherits="Company_LoginLogs" %>


<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
    <script src="../JS/QCDS2010.js" type="text/javascript"></script>

    <script src="../JS/SqlCheck.js" type="text/javascript"></script>

    <script src="js/tianfeng.js" type="text/javascript"></script>
    
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

    <script language="javascript" type="text/javascript">
    function aaa()
    {
        for(var i=0;i<form1.elements.length;i++)
        {
            if(form1.elements[i].type=="text")
            {
                if(form1.elements[i].value.indexOf("'")!=-1||form1.elements[i].value.indexOf("=")!=-1)
                {
                    alert('<%=GetTran("000712", "查询条件里面不能输入特殊字符！")%>');
                    return false;
                }
            }
        }
    }
    	function CheckText(btname)
	{
		//这个方法是页面有多个按钮要提交时，多次使用这个方法,传入按钮的ID
		filterSql_II (btname);
		
	}
    </script>

    <style type="text/css">
        #secTable
        {
            width: 150px;
        }
    </style>

</head>
<body onload="down2()">
    <form id="form1" runat="server">
    <br />
    <table cellspacing="0" cellpadding="0" border="0" width="100%" class="biaozzi">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td align="left" style="white-space: nowrap">
                            <asp:Button ID="BtnConfirm" runat="server" Text="查 询" OnClick="BtnConfirm_Click" CssClass="anyes"></asp:Button>
                                
                                &nbsp;编号：<asp:TextBox
                                    ID="Number" runat="server" Width="80px" MaxLength="15"></asp:TextBox>
                            &nbsp;输入信息：<asp:TextBox ID="pass" runat="server" Width="80px" MaxLength="50"></asp:TextBox>
                            &nbsp;类型：<asp:DropDownList ID="ddlChangeType" runat="server">
                                        <asp:ListItem Value="-1" Selected="True">全部</asp:ListItem>
                                        <asp:ListItem Value="1">公司</asp:ListItem>
                                        <asp:ListItem Value="2">服务中心</asp:ListItem>
                                        <asp:ListItem Value="3">会员</asp:ListItem>
                                    </asp:DropDownList>
                            
                            &nbsp;IP：<asp:TextBox ID="txtip" runat="server" Width="80px" MaxLength="50"></asp:TextBox>
                            &nbsp;状态：<asp:DropDownList ID="DropDownList1" runat="server">
                                        <asp:ListItem Value="-1" Selected="True">全部</asp:ListItem>
                                        <asp:ListItem Value="1">正确</asp:ListItem>
                                        <asp:ListItem Value="2">错误</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;开始时间：<asp:TextBox ID="txtDate" runat="server" onfocus="WdatePicker()" CssClass="Wdate"></asp:TextBox>
                                    &nbsp;结束时间：<asp:TextBox ID="txtDate2" runat="server" onfocus="WdatePicker()" CssClass="Wdate"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />
                <table cellspacing="0" cellpadding="0" border="0" width="100%" class="tablemb">
                    <tr>
                        <td valign="top">
                            <asp:GridView ID="GridView1" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False"
                                BorderStyle="Solid" OnRowDataBound="GridView1_RowDataBound">
                                <AlternatingRowStyle BackColor="#F1F4F8" />
                                <HeaderStyle CssClass="tablebt" />
                                <RowStyle HorizontalAlign="Center" />
                                <Columns>
                                    
                                    <asp:BoundField DataField="Number" HeaderText="编号"></asp:BoundField>
                                    <asp:BoundField DataField="pass" HeaderText="输入信息"></asp:BoundField>
                                    <asp:BoundField DataField="lx" HeaderText="类型"></asp:BoundField>
                                    <asp:TemplateField HeaderText="时间">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" 
                                                Text='<%# GetRDate(Eval("logindate")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("logindate") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="loginIP" HeaderText="IP地址"></asp:BoundField>
                                     <asp:BoundField DataField="zt" HeaderText="状态" ></asp:BoundField>
                                    
                                    
                                </Columns>
                                <EmptyDataTemplate>
                                    <table width="100%">
                                        <tr>
                                            <th>
                                                编号
                                            </th>
                                            <th>
                                                输入信息
                                            </th>
                                            <th>
                                                类型
                                            </th>
                                            <th>
                                                时间
                                            </th>
                                            <th>
                                                IP地址
                                            </th>
                                            <th>
                                                状态
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
    </table>
    <table width="100%">
        <tr>
            <td align="right">
                <uc1:Pager ID="Pager1" runat="server" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
        </tr>
    </table>
    <div id="cssrain" style="width: 100%">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
            <tr>
                <td width="150">
                    <table width="100%" border="0" height="28" cellpadding="0" cellspacing="0" id="secTable">
                        <tr>
                            <td class="sec2" onclick="secBoard(0)" style="white-space: nowrap;">
                                <span id="span1" title="" onmouseover="cut()">
                                    <%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000032", "管 理"))%></span>
                            </td>
                            <td class="sec1" onclick="secBoard(1)" style="white-space: nowrap;">
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
            <table width="100%" border="0" height="68" cellspacing="0" class="DMbk" id="mainTable">
                <tbody style="display: block" id="tbody0">
                    <tr>
                        <td valign="bottom" style="padding-left: 20px">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="btnDownExcel" runat="server" Text="导出Excel" OnClick="btnDownExcel_Click"
                                            Style="display: none;"></asp:LinkButton><a href="#"><img src="images/anextable.gif"
                                                width="49" height="47" border="0" onclick="__doPostBack('btnDownExcel','');" /></a>
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                     
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
