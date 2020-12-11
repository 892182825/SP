<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductTreeList.aspx.cs" Inherits="Company_ProductTreeList" %>

<%@ Register TagPrefix="ucl" TagName="uclCountry" Src="~/UserControl/Country.ascx" %>

<html>
<head>
    <title><%=GetTran("005052", "添加新品")%></title>
    <link href="CSS/Company.css" type="text/css" rel="Stylesheet" />

    <script type="text/javascript">
        function menuTree(menu, img, plus) {
            if (menu.style.display == "none") {
                menu.style.display = "";
                img.src = "images/foldopen.gif";
                plus.src = "images/MINUS2.GIF";
            }
            else {
                menu.style.display = "none";
                img.src = "images/foldclose.gif";
                plus.src = "images/PLUS2.GIF";
            }
        }
        var newWin = 0;
        var widthProduct = 850;
        var heightProduct = 750;
        var widthFold = 420;
        var heightFold = 220;

        var topProduct = 100;
        var leftProduct = 100;
        var topFold = 100;
        var leftFold = 100;

        var widthDel = 400;
        var heightDel = 100;
        var topDel = 100;
        var leftDel = 100;

        function openAddWin(action, id) {
            var sure = true;
            if (action == "add") {
                param = 'status:no;resizable:no;scroll:yes;help:no;dialogWidth:' + widthProduct + 'px;dialogHeight:' + heightProduct + 'px;dialogLeft:' + leftProduct + 'px;dialogTop:' + topProduct + 'px;';
            }

            else if (action == "addFold") {
                param = 'status:no;resizable:no;scroll:no;help:no;dialogWidth:' + widthFold + 'px;dialogHeight:' + heightFold + 'px;dialogLeft:' + leftFold + 'px; dialogTop:' + topFold + 'px;';
            }

            else if (action == "deleteItem") {
                if (window.confirm('<%=GetTran("005609","您确定要删除此产品？")%>')) {
				        sure = true;
				        param = 'status:no;resizable:no;copyhistory:no;scroll:no;help:no;dialogWidth:' + widthDel + 'px;dialogHeight:' + heightDel + 'px;dialogLeft:' + leftDel + 'px;dialogTop:' + topDel + 'px;';
				    }

				    else {
				        sure = false;
				    }

				}

				else if (action == "deleteFold") {
				    if (window.confirm('<%=GetTran("005617","您确定要删除此产品类？")%>\n<%=GetTran("005613","删除此类将删除其下属的所有产品类及产品！")%>')) {
				        sure = true;
				        param = 'status:no;resizable:no;copyhistory:no;scroll:no;help:no;dialogWidth:' + widthDel + 'px;dialogHeight:' + heightDel + 'px;dialogLeft:' + leftDel + 'px;dialogTop:' + topDel + 'px;';
				    }

				    else {
				        sure = false;
				    }
				}

				else if (action == "editFold") {
				    param = 'status:no;resizable:no;copyhistory:no;scroll:no;help:no;dialogWidth:' + widthFold + 'px;dialogHeight:' + heightFold + 'px;dialogLeft:' + leftFold + 'px;dialogTop:' + topFold + 'px;';
				}

				else if (action == "editItem") {
				    param = 'status:no;resizable:no;copyhistory:no;scroll:yes;help:no;dialogWidth:' + widthProduct + 'px;dialogHeight:' + heightProduct + 'px;dialogLeft:' + leftProduct + 'px;dialogTop=' + topProduct + 'px;';
				}

    if (sure) {
        newWin = window.open("AddProduct.aspx?action=" + action + "&id=" + id + "&date=" + new Date().getTime(), 'nW', param);
        if (newWin == 1) {
            window.location.reload();
        }
    }
}
function openAddWin2(action, id, countryCode) {
    if (action == "addFold") {
        param = 'status:no;resizable:no;scroll:no;help:no;dialogWidth:' + widthFold + 'px;dialogHeight:' + heightFold + 'px;dialogLeft:' + leftFold + 'px; dialogTop:' + topFold + 'px;';
    }

    if (action == "add") {
        param = 'status:no;resizable:no;scroll:yes;help:no;dialogWidth:' + widthProduct + 'px;dialogHeight:' + heightProduct + 'px;dialogLeft:' + leftProduct + 'px;dialogTop:' + topProduct + 'px;';
    }

    newWin = window.open("AddProduct.aspx?action=" + action + "&id=" + id + "&countryCode=" + countryCode, 'nW', param);
    if (newWin == 1) {
        window.location.reload();
    }
}

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <br />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="upProductTreeList" runat="server">
            <ContentTemplate>
                <div>
                    <table border="0" cellpadding="0" cellspacing="0" class="biaozzi">
                        <tr>
                            <td><%=GetTran("000058","请选择国家")%>：</td>
                            <td>
                                <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table border="0" cellpadding="0" cellspacing="0" class="biaozzi">
                        <tr>
                            <td align="left">
                                <div style="float: left;"><a style='color: #075C79;' href="javascript:window.location.reload();"><%=GetTran("003190","刷新产品列表")%></a></div>
                               
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <div onclick="javascript:window.location.href='SetParameters.aspx?type=p';" style="cursor: pointer; "><%=GetTran("007636", "转至产品属性设置页")%></div>
                            </td>
                        </tr>
                        <tr align="left">
                            <td>
                                <asp:Label ID="lblmenu" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
