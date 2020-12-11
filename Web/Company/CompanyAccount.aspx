<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompanyAccount.aspx.cs" Inherits="Company_CompanyAccount"
    EnableEventValidation="false" %>

<%@ Register Src="../UserControl/Country.ascx" TagName="Country" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>账户管理</title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />

    <script src="../JS/QCDS2010.js" type="text/javascript"></script>
    <script src="../JS/sryz.js" type="text/javascript"></script>
    <script type="text/javascript">
		    window.onerror=function()
		    {
		        return true;
		    };
    </script>


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

    <script language="javascript" src="../js/SqlCheck.js"></script>

    <script language="javascript" type="text/javascript">
        function vali()
        {
            var tbank=document.getElementById("txtbank").value;
            var tcard=document.getElementById("txtcard").value;
            if(tbank.length<=0)
            {
                alert('<%=GetTran("002060", "请填写开户行")%>');
                return false;
            }
            if(tcard.length<=0)
            {
                alert('<%=GetTran("002081", "请填写账号")%>');
                 return false;
            }
            var bu = document.getElementById("but1").value;
            
            if(bu=="添 加"){
                return checkedcf("是否添加账号？");
            }else if(bu=="保 存"){
                return checkedcf("是否修改账号？");
            }
        }
        
        function conf()
        {
            return confirm('<%=GetTran("002101", "您是否要删除！！！")%>');
        }
        window.onload=function()
	    {
	        down2();
	    };
    </script>

</head>
<body>
    <form id="form1" runat="server" onsubmit="return filterSql_III()">
    <asp:ScriptManager ID="scirpt1" runat="server" />
    <br />
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" class="biaozzi">
                    <tr>
                        <td style="white-space: nowrap;">
                            <span>&nbsp;&nbsp;<%=GetTran("002078", "选择银行国家")%>：&nbsp;&nbsp;</span>
                        </td>
                        <td>
                            <uc1:Country ID="CountryUC" runat="server" />
                        </td>
                        <td style="white-space: nowrap;">
                            <span>&nbsp;&nbsp;<%=GetTran("001243", "开户行")%>：&nbsp;&nbsp;</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtbank" runat="server" Width="160px" MaxLength="50" onkeyup = "ValidateValue(this)"/>
                        </td>
                        <td style="white-space: nowrap;">
                            <span>&nbsp;&nbsp;<%=GetTran("000086", "开户名")%>：&nbsp;&nbsp;</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtname" runat="server" Width="160px" MaxLength="50"  onkeyup = "ValidateValue(this)"/>
                        </td>
                        <td style="white-space: nowrap;">
                            <span>&nbsp;&nbsp;<%=GetTran("002073", "账号")%>：&nbsp;&nbsp;</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtcard" runat="server" Width="160px" MaxLength="25" />
                            <asp:RegularExpressionValidator ID="REVtxtcard" runat="server" ValidationGroup="B"
                                ControlToValidate="txtcard" ErrorMessage="只能输入25位数字、-" SetFocusOnError="True"
                                ValidationExpression="^[0-9 -]*"></asp:RegularExpressionValidator>
                        </td>
                        <td>
                            &nbsp;
                            <asp:Button ID="but1" runat="server" Text="添 加" OnClientClick="return  vali()" OnClick="but1_Click"
                                CssClass="anyes" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <br />
            </td>
        </tr>
        <tr>
            <td width="100%" style="border: rgb(147,226,244) solid 1px">
                <asp:GridView ID="gwbankCard" runat="server" AutoGenerateColumns="false" Width="100%"
                    OnRowCommand="gwbankCard_RowCommand" CssClass="tablemb bordercss" OnRowDataBound="gwbankCard_RowDataBound">
                    <AlternatingRowStyle BackColor="#F1F4F8" />
                    <RowStyle HorizontalAlign="Center" />
                    <HeaderStyle CssClass="tablebt" />
                    <Columns>
                        <asp:TemplateField HeaderText="操作" ItemStyle-Width="100px">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnUpt" runat="server" CommandName="updCard" CommandArgument='<%#Eval("ID") %>'><%=GetTran("000259", "修改")%></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:LinkButton ID="btnDel" runat="server" CommandName="delCard" OnClientClick="return conf()"
                                    CommandArgument='<%#Eval("ID") %>'><%=GetTran("000022", "删除")%></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <span>国家</span></HeaderTemplate>
                            <ItemTemplate>
                                <span>
                                    <%#bindcontry(Eval("countryID")) %></span>
                                <asp:HiddenField ID="hdfID" runat="server" Value='<%#Eval("ID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="开户行" DataField="Bank" />
                        <asp:BoundField HeaderText="开户名" DataField="bankname" />
                        <asp:BoundField HeaderText="账号" DataField="BankBook" />
                    </Columns>
                    <EmptyDataTemplate>
                        <table bgcolor="#F8FBFD" width="100%">
                            <tr>
                                <th>
                                    <%=GetTran("000015", "操作")%>
                                </th>
                                <th>
                                    <%=GetTran("000047", "国家")%>
                                </th>
                                <th>
                                    <%=GetTran("001243", "开户行")%>
                                </th>
                                <th>
                                    <%=GetTran("000086", "开户名")%>
                                </th>
                                <th>
                                    <%=GetTran("002073", "账号")%>
                                </th>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <uc2:Pager ID="Pager1" runat="server" />
                        </td>
                    </tr>
                </table>
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
                                        <asp:ImageButton ID="ImageButton1" runat="server" OnClick="btnEXCL_Click" ImageUrl="images/anextable.gif"
                                            Style="display: none" />
                                        <a href="#">
                                            <img src="images/anextable.gif" width="49" height="47" border="0" onclick="__doPostBack('ImageButton1','');" /></a>
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
                                        1、<%=GetTran("002063", "添加、修改、查询银行帐号")%>．
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
