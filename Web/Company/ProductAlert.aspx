<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductAlert.aspx.cs" Inherits="Company_DisplayMemberInfo" %>

<%@ Register src="../UserControl/Pager.ascx" tagname="Pager" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%=GetTran("000530", "产品预警")%></title>
    <script type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
    <script src="../JS/QCDS2010.js" type="text/javascript"></script>
    <script src="../JS/SqlCheck.js" type="text/javascript"></script>
    <script src="js/tianfeng.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
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
    </script>
   <script language="javascript" type="text/javascript">

	function CheckText(btname)
	{
		//这个方法是页面有多个按钮要提交时，多次使用这个方法,传入按钮的ID
		filterSql_II (btname);
	}
	</script>
	<script type="text/javascript" language="javascript">
        function document.onkeydown()
        {
            var e=event.srcElement;
            if(event.keyCode==13)
            {
                document.getElementById("BtnConfirm").click();
                return false;
            }
        }
    </script> 
    <script type="text/javascript">
        function bohuiShenqing()
        {
            if(window.confirm("确认要驳回申请么？"))
            {
                 return true;
            }
            else
            {
                 return false;
            }
        }
        function confirmShenqing()
        {
            if(window.confirm("确定要同意申请么？"))
            {
                 return true;
            }
            else
            {
                 return false;
            }
        }
    </script>
</head>
<body onload="down2()">
    <form id="form1" method="post" runat="server">
    <br />
    <table cellspacing="0" cellpadding="0" border="0" width="100%" class="biaozzi">
        <tr>
            <td align="left">
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td align="left"  style="white-space:nowrap;">
                            <asp:Button ID="BtnConfirm" runat="server" Text="查 询" OnClick="BtnConfirm_Click" CssClass="anyes"></asp:Button>
                            &nbsp;<%=GetTran("000000", "产品编号")%>：<asp:TextBox ID="Number" runat="server" Width="80px"  MaxLength="10"></asp:TextBox>
                            &nbsp;<%=GetTran("000000", "产品名称")%>：<asp:TextBox ID="Name" runat="server" Width="80px" MaxLength="50"></asp:TextBox>
                            &nbsp;<%=GetTran("000000", "产品型号")%>：<asp:DropDownList ID="ddlProductType" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <br />
                <table cellspacing="0" cellpadding="0" border="0" width="100%" class="tablemb">
                    <tr>
                        <td valign="top" align="left">
                            <asp:GridView ID="GridView1" runat="server" Width="100%" AllowSorting="true" AutoGenerateColumns="False"
                                BorderStyle="Solid" OnRowDataBound="GridView1_RowDataBound">
                                <AlternatingRowStyle BackColor="#F1F4F8" />
                                <HeaderStyle CssClass="tablebt" />
                                <RowStyle HorizontalAlign="Center" />
                                <Columns>
                                    <asp:BoundField DataField="ProductCode" HeaderText="产品编号"></asp:BoundField>
                                    <asp:BoundField DataField="ProductName" HeaderText="产品名称"></asp:BoundField>
                                    <asp:BoundField DataField="producttypename" HeaderText="产品型号"></asp:BoundField>
                                    <asp:BoundField DataField="ProductArea" HeaderText="产品产地"></asp:BoundField>
                                    
                                    <asp:BoundField DataField="CostPrice" HeaderText="成本价"></asp:BoundField>
                                    <asp:BoundField DataField="CommonPrice" HeaderText="普通价"></asp:BoundField>
                                    <asp:BoundField DataField="PreferentialPrice" HeaderText="优惠价"></asp:BoundField>
                                    <asp:BoundField DataField="Weight" HeaderText="产品重量"></asp:BoundField>
                                    <asp:TemplateField HeaderText="是否组合产品">
                                        <ItemTemplate>
                                            <%#returnCombination(Eval("IsCombineProduct").ToString())%>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                
                                    <asp:BoundField DataField="TotalIn" HeaderText="小单位总入"></asp:BoundField>
                                    <asp:BoundField DataField="TotalOut" HeaderText="小单位总出"></asp:BoundField>
                                    <asp:BoundField DataField="leftKucun" HeaderText="剩余数量"></asp:BoundField>
                                    <asp:BoundField DataField="AlertnessCount" HeaderText="小单位数量预警"></asp:BoundField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <table class="biaozzi" width="100%">
                                        <tr>
                                            <th><%=GetTran("000558", "产品编号")%></th><th>
                                            <th><%=GetTran("000501", "产品名称")%></th><th>
                                            <th><%=GetTran("000882", "产品型号")%></th><th>
                                            <th><%=GetTran("001877", "产品产地")%></th><th>
                                            
                                            <th><%=GetTran("001955", "成本价")%></th><th>
                                            <th><%=GetTran("001956", "普通价")%></th><th>
                                            <th><%=GetTran("001957", "优惠价")%></th><th>
                                            <th><%=GetTran("001953", "产品重量")%></th><th>
                                            
                                            <th><%=GetTran("001890", "是否组合产品")%></th><th>
                                            <th><%=GetTran("000359", "小单位总入")%></th><th>
                                            <th><%=GetTran("000362", "小单位总出")%></th><th>
                                            <th><%=GetTran("000363", "剩余数量")%></th><th>
                                            <th><%=GetTran("000365", "小单位数量预警")%></th><th>
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
                </table>
                
                <br /><br /><br />
    
                            <div id="cssrain" style="width:100%">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
                                    <tr>
                                        <td width="150">
                                            <table width="100%" border="0" height="28" cellpadding="0" cellspacing="0" id="secTable">
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
                                                <img src="images/dis1.GIF" name="imgX" width="18" height="22" border="0" 
                                                id="imgX" onclick="down2()" style="vertical-align:middle"/></a></td>
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
                                                                <asp:LinkButton ID="Button1" runat="server" Text="导出EXECL" OnClick="Button1_Click"
                                                                    Style="display: none"></asp:LinkButton>
                                                                <a href="#">
                                                                    <img src="images/anextable.gif" width="49" alt="" height="47" border="0" onclick="__doPostBack('Button1','');" /></a>&nbsp;&nbsp;&nbsp;&nbsp;
                                                                
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
                                                                <%=GetTran("000286", "1、根据条件查询会员的基本信息。")%>
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
