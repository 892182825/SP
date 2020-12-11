<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberStoreReset.aspx.cs"
    Inherits="Company_MemberStoreReset" %>

<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/ExpectNum.ascx" TagName="ExpectNum" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="CSS/Company.css" rel="Stylesheet" type="text/css" />

    <script src="../JS/QCDS2010.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>

    <script language="javascript" src="../js/SqlCheck.js"></script>

    <script src="js/tianfeng.js" type="text/javascript"></script>
<script type="text/javascript" language="javascript">
        function cut1()
        {
             document.getElementById("span2").title='<%=GetTran("000033", "说 明") %>';
        }
    </script>
    <script type="text/javascript">
     function CheckText(aaaaa)
	{
		//这个方法是页面有多个按钮要提交时，多次使用这个方法,传入按钮的ID
		filterSql_II (aaaaa);
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
   
    function Button3_onclick() {

    }
    </script>

    <style type="text/css">
        .style1
        {
            width: 67px;
        }
        .style2
        {
            width: 89px;
        }
        .style3
        {
            width: 75px;
        }
        .style4
        {
            width: 117px;
        }
        .style5
        {
            width: 197px;
        }
    </style>
</head>
<body onload="down2(),secBoardOnly(0)">
    <form id="form1" runat="server">
    <br />
    <table width="100%">
        <tr>
            <td width="100%">
                <div runat="server" id="div2">
                    <table class="biaozzi" width="100%">
                        <tr style="word-wrap: normal">
                            <td nowrap="nowrap" class="style1">
                                <asp:Button ID="btnWL" runat="server" Text="店铺重置" OnClick="btnWL_Click" CssClass="anyes" />
                            </td>
                            <td align="left" class="style2">
                                &nbsp;
                                 <asp:LinkButton ID="LinkButton1" Style="display: none" runat="server" Text="提交" onclick="LinkButton1_Click"></asp:LinkButton>
                                <input class="anyes" id="Button3" onclick="CheckText('LinkButton1')" type="button" value="<%=GetTran("000048", "查 询")%>" />
                                <asp:Button ID="btnSeach" runat="server" Text="查 询" OnClick="btnSeach_Click" CssClass="anyes" Visible="false" />
                            </td>
                            <td class="style3">
                                <%=GetTran("000024", "会员编号")%>：&nbsp;
                            </td>
                            <td class="style4">
                                <asp:TextBox ID="txt_member" runat="server" MaxLength="10"></asp:TextBox>
                            </td>
                            <td class="style5">
                                <%=GetTran("000613", "日期")%>：<asp:TextBox ID="DatePicker2" runat="server" CssClass="Wdate" onfocus="WdatePicker()"></asp:TextBox>
                            </td>
                            <td nowrap="nowrap">
                                <%=GetTran("000045", "期数")%>：
                                <uc2:ExpectNum ID="ExpectNum1" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:GridView ID="givTWmember" Width="100%" runat="server" AutoGenerateColumns="False"
                                    CssClass="tablemb" onrowdatabound="givTWmember_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="Number" HeaderText="会员编号" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="oldstoreid" HeaderText="调整前店铺编号" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="newstoreid" HeaderText="调整后店铺编号" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="expectnum" HeaderText="期数" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="flag" HeaderText="调整类别" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="upddate" HeaderText="日期" ItemStyle-HorizontalAlign="Center" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <table width="100%" class="biaozzi">
                                            <tr>
                                                <th>
                                                    <%#GetTran("000024", "会员编号")%>
                                                </th>
                                                <th>
                                                     <%#GetTran("001012", "调整前店铺编号")%>
                                                </th>
                                                <th>
                                                     <%#GetTran("001013", "调整后店铺编号")%>
                                                </th>
                                                <th>
                                                     <%#GetTran("000045", "期数")%>
                                                </th>
                                                <th>
                                                     <%#GetTran("001014", "调整类别")%>
                                                </th>
                                                <th>
                                                     <%#GetTran("000613", "日期")%>
                                                </th>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="6">
                                <asp:Label ID="lblMassage" runat="server" Text=""></asp:Label>
                            </td>
                            
                        </tr>
                    </table><uc1:Pager ID="Pager1" runat="server" />
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div runat="server" id="div1" style="display: none">
                    <br />
                    <div style="width: 100%; height: 20px; text-align: center; color: #005575; font-weight: bold;"
                        class="biaozzi">
                        <%=GetTran("001058", "会员所属店铺修改")%></div>
                    <br />
                    <table align="center" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
                        <tr>
                            <td>
                                &nbsp;&nbsp; <%=GetTran("000214", "请输入会员编号")%>：<asp:TextBox ID="txtstoreId" runat="server" MaxLength="10"></asp:TextBox>
                                <asp:LinkButton ID="lkSubmit" Style="display: none" runat="server" Text="提交" OnClick="lkSubmit_Click"></asp:LinkButton>
                                <input class="anyes" id="bSubmit" onclick="CheckText('lkSubmit')" type="button" value="<%=GetTran("000048", "查 询")%>"></input>
                                <asp:Button ID="btnSearch" runat="server" Text="查 询" CssClass="anyes" OnClick="btnSearch_Click"
                                    Visible="false" />
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table id="tbShow" runat="server" visible="false" class="biaozzi">
                                    <tr>
                                        <td style="white-space: nowrap" align="right" class="biaozzi">
                                            <%=GetTran("000024", "会员编号")%>：
                                        </td>
                                        <td style="white-space: nowrap">
                                            <asp:Label ID="lblMemberID" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="white-space: nowrap" align="right">
                                            <%=GetTran("000025", "会员姓名")%>：
                                        </td>
                                        <td style="white-space: nowrap">
                                            <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="white-space: nowrap">
                                            <%=GetTran("001088", "当前所属店铺编号")%>：
                                        </td>
                                        <td style="white-space: nowrap">
                                            <asp:Label ID="lblOldStoreID" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="white-space: nowrap" align="right">
                                            <%=GetTran("001086", "新的店铺编号")%>：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNewStoreID" runat="server" MaxLength="10"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="white-space: nowrap" align="right">
                                            <%=GetTran("001084", "更改方式")%>：
                                        </td>
                                        <td style="white-space: nowrap">
                                            <asp:RadioButtonList ID="rdofangshi" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True" Text="个人所属店铺" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="团队所属店铺" Value="2"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td style="white-space: nowrap">
                                            &nbsp;
                                            <asp:LinkButton ID="lkSubmit1" Style="display: none" runat="server" Text="提交" OnClick="lkSubmit1_Click"></asp:LinkButton>
                                            <input class="anyes" id="Button1" onclick="CheckText('lkSubmit1')" type="button"
                                                value="<%=GetTran("000092", "修 改")%>" />
                                            <asp:Button ID="btnEndit" runat="server" Text="修 改" OnClick="btnEndit_Click" CssClass="anyes"
                                                Visible="false" />
                                            &nbsp;<input type="reset" value="<%=GetTran("001081", "清 除")%>" class="anyes" />
                                            <asp:Button ID="Button2" runat="server" Text="返 回" OnClick="Button2_Click" CssClass="anyes" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="cssrain" style="width:100%">
                        <table width="99%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
                            <tr>
                                <td width="80">
                                    <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTableOnly">
                                        <tr>
                                            <td class="secOnly" onclick="secBoardOnly(0)">
                                               <span id="span2" title="" onmouseover="cut1()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <a href="#">
                                        <img src="images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX"
                                            onclick="down2()" /></a>
                                </td>
                            </tr>
                        </table>
                        <div id="divTab2">
                            <table width="99%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
                                <tbody style="display: block" id="tbody0">
                                    <tr>
                                        <td style="padding-left: 20px">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        1、
                                                        <%=GetTran("006842", "更改会员当前所属店铺编号。")%>
                                                        <br />
                                                        2、<%=GetTran("006843", "个人所属店铺是指只更改会员本人的所属店铺。")%><br />
                                                        3、<%=GetTran("006844", "团队所属店铺是指更改这个会员的团队里所有会员的所属店铺编号，如果会员团队里有会员开店的，则不更改他团队所属店铺编号。")%>
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
    </form>
    <%= msg %>
</body>
</html>
