<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateNet.aspx.cs" Inherits="UpdateNet" %>

<%@ Register Src="UserControl/SearchPlacement_DoubleLines2.ascx" TagName="SearchPlacement_DoubleLines"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="Company/CSS/Company.css" rel="stylesheet" id="cssid" type="text/css" />

    <script src="JS/jquery-1.4.1-vsdoc.js" type="text/javascript"></script>

    <script type="text/javascript" src="../member/js/RegisterXY.js"></script>

    <script src="JS/Editreginfo.js" type="text/javascript"></script>

    <script type="text/javascript">
        function getpro() {
            var pm1 = '<%=GetTopBh()%>';
            var pm2 = '<%=GetTran("005961","请先填写推荐编号!") %>';
            var pm3 = '<%=GetTran("006015","推荐编号必须在店长的推荐网络下面！") %>';


            return {
                gtb: pm1,
                tjbh: pm2,
                tjwl: pm3
            };
        }
    </script>

    <style type="text/css">
        .re_text
        {
            float: left;
        }
        .btn2
        {
            background-image: url(Member/images/regButRegs_03.png);
            background-repeat: repeat-x;
            height: 30px;
            width: 150px;
            border: 1px solid #075c75;
            font-family: "微软雅黑";
            font-size: 14px;
            line-height: 30px;
            font-weight: bold;
            color: #FFF;
            margin-right: 20px;
            margin-left: 20px;
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
            <div style="width: 50%; font-size: 12px; text-align: left;">
                *<%=GetTran("000024", "会员编号")%>：<asp:Label ID="labBh" runat="server"></asp:Label><br />
                *<%=GetTran("000026", "推荐编号")%>：<asp:TextBox ID="txtDirect" runat="server" onblur="dddddd()"></asp:TextBox><br />
                <uc1:SearchPlacement_DoubleLines ID="SearchPlacement_DoubleLines1" runat="server" />
                <asp:HiddenField ID="HFTopNumber" runat="server" Value="" />
                <span id="info_txtPlacement" style="display: none;" class="re_pro"></span>
            </div>
            <table width="80%" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr align="center">
                    <td colspan="2">
                        <asp:Label ID="lblmessage" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 35%">
                    </td>
                    <td align="left">
                    </td>
                </tr>
                <tr>
                    <td align="right">
                    </td>
                    <td align="left">
                    </td>
                </tr>
                <tr>
                    <%--<td align="right"><%=GetTran("000027", "安置编号")%>：</td>
                <td align="left">
                    <asp:TextBox ID="txtAz" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                    <input name="button2" type="button" class="anyes" id="Button2"  onclick=" ShowDiv(null)" value='<%=GetTran("005861", "寻找安置人")%>' />
                </td>--%>
                    <td align="center" colspan="2">
                    </td>
                </tr>
                <tr>
                    <td id="plc" align="left" valign="middle" colspan="2">
                        <table width="100%">
                            <tr>
                                <td>
                                    <div id="Divt1" style="margin-left: 50px; display: none; overflow-y: hidden; overflow: auto;
                                        width: 700px; height: 200px;" align="center">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnOk" runat="server" CssClass="anyes" Text="确定" OnClick="btnOk_Click" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnBack" runat="server" CssClass="anyes" Text="返回" OnClick="btnBack_Click"
                            Visible="false" />
                        <input type="button" value="返回" onclick="history.go(-1);" class="anyes" />
                    </td>
                </tr>
            </table>
        </center>
    </div>
    </form>
    <%=msg %>
</body>
</html>

<script type="text/javascript">
    function dddddd() {
        document.getElementById("HFTopNumber").value = document.getElementById("txtDirect").value
        $("#SearchPlacement_DoubleLines1_Txttj").val($("#txtDirect").attr("value"));
        //$("#HFTopNumber").val($("#txtDirect").text());
    }
    
    
    window.onload = function() {
        $("#SearchPlacement_DoubleLines1_Txttj").val($("#txtDirect").attr("value"));
        $("#HFTopNumber").val($("#txtDirect").text());
//        alert($("#HFTopNumber").val();
    }
</script>

