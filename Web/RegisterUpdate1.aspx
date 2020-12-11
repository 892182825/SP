<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegisterUpdate1.aspx.cs" Inherits="RegisterUpdate1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <script type="text/javascript">
        function change(th) {
            var arrtd = document.getElementById("trid").getElementsByTagName("td");

            for (var i = 0; i < arrtd.length; i++) {
                arrtd[i].getElementsByTagName("a")[0].style.color = "gray";
                arrtd[i].style.backgroundColor = "";

            }
            th.parentNode.style.backgroundColor = "rgb(240,240,240)";
            th.style.color = "rgb(0,106,60)";
        }
    </script>
</head>
<body onload="change(document.getElementById('lbtBasic'))">
    <form id="form1" runat="server">
    <div>
        <div style="width:100%" align="center">
            <br>
            <br>
            <table style="width:100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td align="left" >
                        <table style="height: 30px; width: 100%" border="0" cellspacing="0">
                            <tr id="trid">
                                <td align="center" title='<%=GetTran("005905", "基本信息修改") %>' id="td1" runat="server">
                                    <img src="images/a.gif" width="20" height="20" align="absmiddle">&nbsp;<a style="text-decoration:none;font-size:10pt;color:rgb(240,240,240);font-weight:bold" ID="lbtBasic"  onclick="change(this)"
                                    href='UpBasic.aspx?Number=<%=ViewState["Number"]%>&CssType=<%=ViewState["CssType"]%>' target="aa"><%=GetFormatString(GetTran("005905", "基本信息修改"))%></a>
                                </td>
                                <td align="center" title='<%=GetTran("005906", "购物信息修改") %>' id="td2" runat="server">
                                    <img src="images/b.gif" width="20" height="20" align="absmiddle">&nbsp;<a style="text-decoration:none;font-size:10pt;color:rgb(240,240,240);font-weight:bold" ID="lbtProduct" onclick="change(this)"
                                    href='UpdZhuChe.aspx?OrderID=<%=ViewState["OrderID"]%>&Number=<%=ViewState["Number"] %>&CssType=<%= ViewState["CssType"]%>&tp=<%= ViewState["tp"]%>' target="aa"><%=GetFormatString(GetTran("005906", "购物信息修改"))%></a>
                                </td>
                                <td align="center" title='<%=GetTran("005907", "位置信息修改") %>' id="td3" runat="server">
                                    <img src="images/c.gif" width="20" height="20" align="absmiddle">&nbsp;<a style="text-decoration:none;font-size:10pt;color:rgb(240,240,240);font-weight:bold" ID="lbtNet" onclick="change(this)"
                                    href='UpdateNet.aspx?Number=<%=ViewState["Number"]  %>&CssType=<%=ViewState["CssType"] %>' target="aa"><%=GetFormatString(GetTran("005907", "位置信息修改"))%></a>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="border:rgb(240,240,240) solid 1px;" >
                        <iframe src='<%=mainurlstr%>' name="aa" style="height: 600px; width: 100%" frameborder="0">
                        </iframe>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
