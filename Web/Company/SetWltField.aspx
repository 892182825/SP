<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetWltField.aspx.cs" Inherits="SetWltField" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <style type="text/css">
        .tab
        {
        	border-left:rgb(218,218,218) solid 1px;
            border-top:rgb(218,218,218) solid 1px;
            
            color:rgb(53,53,53);
        }
        .tab th
        {
            text-align: center;
            border-right:rgb(218,218,218) solid 1px;
            border-bottom:rgb(218,218,218) solid 1px;
            background-image:url(images/lmenu02.gif);height:25px;
            color:White;
        }
        .tab td
        {
            text-align: center;
            border-right:rgb(218,218,218) solid 1px;
            border-bottom:rgb(218,218,218) solid 1px;
        }
        a
        {
        	color:rgb(53,53,53);
        }
    </style>
    
    <script  type="text/javascript">
        function save(id, th) {
            var f;
            var v;

            var inputarr = th.parentNode.parentNode.getElementsByTagName("input");

            f = inputarr[0].value;
            v = inputarr[1].checked ? "1" : "0";

            getData(f, v, id);
        }
        
        var xmlHttp;

        function createXMLHttpRequest() {
            if (window.ActiveXObject) {
                xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
            }
            else if (window.XMLHttpRequest) {
                xmlHttp = new XMLHttpRequest();
            }
        }

        function getData(f, v, id) {
            if (xmlHttp == null)
                createXMLHttpRequest();

            xmlHttp.open("get", encodeURI("SetWltField.aspx?action=save&field=" + f + "&visible=" + v + "&id=" + id + "&date=" + new Date().getTime()), true);
            xmlHttp.onreadystatechange = function() {
                if (xmlHttp.readyState == 4) {
                    if (xmlHttp.status == 200) {
                        alert(xmlHttp.responseText);
                    }
                }
            };
            xmlHttp.send(null);
        }

        function show(id, th) {
            document.getElementById("stab1").style.display = "none";
            document.getElementById("stab2").style.display = "none";
            document.getElementById("stab3").style.display = "none";
            document.getElementById("stab4").style.display = "none";
            document.getElementById("stab5").style.display = "none";
            document.getElementById("stab6").style.display = "none";

            document.getElementById("tdc1").style.backgroundColor = "white";
            document.getElementById("tdc2").style.backgroundColor = "white";
            document.getElementById("tdc3").style.backgroundColor = "white";

            document.getElementById("stab" + id).style.display = "";
            document.getElementById("stab" + (id + 3)).style.display = "";

            th.style.backgroundColor = "rgb(138,171,215)";
        }
    </script>
</head>
<body style="font-size:10pt">
    <form id="form1" runat="server">
    
    <div style="padding-top:20px;padding-left:20px">
    
        <table style="width:300px;height:30px" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td id="tdc1" align="center" style="cursor:pointer;background-color:rgb(138,171,215)" onclick="show(1,this)">
                    <%=GetTran("007944", "公司设置")%>
                </td>
                <td id="tdc2" align="center" style="cursor:pointer;display:none" onclick="show(2,this)">
                    <%=GetTran("007945", "服务机构设置")%>
                </td>
                <td id="tdc3" align="center" style="cursor:pointer" onclick="show(3,this)">
                    <%=GetTran("007920", "会员设置")%>
                </td>
            </tr>
        </table>
        <div style="width:100%;height:2px;background-color:rgb(138,171,215);overflow:hidden;"></div>
        <br />

        <asp:Repeater ID="Repeater1" runat="server">
            <HeaderTemplate>
                <table width='400px' border='0' cellspacing='0' cellpadding='0' class="tab" id="stab1">
                    <tr>
                        <th width='140'>
                            <%=GetTran("007946", "公司推荐网路图列名")%>
                        </th>
                        <th>
                            <%=GetTran("007947", "是否显示")%>
                        </th>
                        <th>
                            <%=GetTran("000015","操作")%>
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                    <tr>
                        <td>
                            <input type="text" id='txC<%#Eval("ID")%>' value='<%# Eval("FieldName").ToString()%>' style="width:120px;border:gray solid 1px" />
                        </td>
                        <td>
                            <input type="checkbox" id='ckC<%#Eval("ID")%>' <%#Eval("IsVisible").ToString()=="1"?"checked":""%> />
                        </td>
                        <td align='right'>
                            <a href="#" onclick="save('<%#Eval("ID")%>',this)"><%=GetTran("005901","保存")%></a>
                        </td>
                    </tr>
            </ItemTemplate>
            <FooterTemplate>
                    
                </table>
            </FooterTemplate>

        </asp:Repeater>
       
        <asp:Repeater ID="Repeater4" runat="server">
            <HeaderTemplate>
                <table width='400px' border='0' cellspacing='0' cellpadding='0' class="tab" id="stab4" style="display:">
                    <tr>
                        <th width='140'>
                            <%=GetTran("007948", "公司安置网路图列名")%>
                        </th>
                        <th>
                            <%=GetTran("007947", "是否显示")%>
                        </th>
                        <th>
                            <%=GetTran("000015","操作")%>
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                    <tr>
                        <td>
                            <input type="text" id='txC<%#Eval("ID")%>' value='<%#Eval("FieldName").ToString()%>' style="width:120px;border:gray solid 1px" />
                        </td>
                        <td>
                            <input type="checkbox" id='ckC<%#Eval("ID")%>' <%#Eval("IsVisible").ToString()=="1"?"checked":""%> />
                        </td>
                        <td align='right'>
                            <a href="#" onclick="save('<%#Eval("ID")%>',this)"><%=GetTran("005901","保存")%></a>
                        </td>
                    </tr>
            </ItemTemplate>
            <FooterTemplate>
                   
                </table>
            </FooterTemplate>

        </asp:Repeater>
        
        <asp:Repeater ID="Repeater2" runat="server">
            <HeaderTemplate>
                <table width='400px' border='0' cellspacing='0' cellpadding='0' class="tab"  id="stab2" style="display:none">
                    <tr>
                        <th width='180'>
                            <%=GetTran("007949", "服务机构推荐网路图列名")%>
                        </th>
                        <th>
                            <%=GetTran("007947", "是否显示")%>
                        </th>
                        <th>
                            <%=GetTran("000015","操作")%>
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                    <tr>
                        <td>
                            <input type="text" id='txS<%#Eval("ID")%>' value='<%#Eval("FieldName").ToString()%>' style="width:120px;border:gray solid 1px" />
                        </td>
                        <td>
                            <input type="checkbox" id='ckS<%#Eval("ID")%>' <%#Eval("IsVisible").ToString()=="1"?"checked":""%> />
                        </td>
                        <td align='right'>
                            <a href="#" onclick="save('<%#Eval("ID")%>',this)"><%=GetTran("005901","保存")%></a>
                        </td>
                    </tr>
            </ItemTemplate>
            <FooterTemplate>
                   
                </table>
            </FooterTemplate>

        </asp:Repeater>
       
        <asp:Repeater ID="Repeater5" runat="server">
            <HeaderTemplate>
                <table width='400px' border='0' cellspacing='0' cellpadding='0' class="tab"  id="stab5" style="display:none">
                    <tr>
                        <th width='180'>
                            <%=GetTran("007950", "服务机构安置网路图列名")%>
                        </th>
                        <th>
                            <%=GetTran("007947", "是否显示")%>
                        </th>
                        <th>
                            <%=GetTran("000015","操作")%>
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                    <tr>
                        <td>
                            <input type="text" id='txS<%#Eval("ID")%>' value='<%#Eval("FieldName").ToString()%>' style="width:120px;border:gray solid 1px" />
                        </td>
                        <td>
                            <input type="checkbox" id='ckS<%#Eval("ID")%>' <%#Eval("IsVisible").ToString()=="1"?"checked":""%> />
                        </td>
                        <td align='right'>
                            <a href="#" onclick="save('<%#Eval("ID")%>',this)"><%=GetTran("005901","保存")%></a>
                        </td>
                    </tr>
            </ItemTemplate>
            <FooterTemplate>
                   
                </table>
            </FooterTemplate>

        </asp:Repeater>
        
        <asp:Repeater ID="Repeater3" runat="server">
            <HeaderTemplate>
                <table width='400px' border='0' cellspacing='0' cellpadding='0' class="tab"  id="stab3" style="display:none">
                    <tr>
                        <th width='140'>
                            <%=GetTran("007951", "会员推荐网路图列名")%>
                        </th>
                        <th>
                            <%=GetTran("007947", "是否显示")%>
                        </th>
                        <th>
                            <%=GetTran("000015","操作")%>
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                    <tr>
                        <td>
                            <input type="text" id='txM<%#Eval("ID")%>' value='<%#Eval("FieldName").ToString()%>' style="width:120px;border:gray solid 1px" />
                        </td>
                        <td>
                            <input type="checkbox" id='ckM<%#Eval("ID")%>' <%#Eval("IsVisible").ToString()=="1"?"checked":""%> />
                        </td>
                        <td align='right'>
                            <a href="#" onclick="save('<%#Eval("ID")%>',this)"><%=GetTran("005901","保存")%></a>
                        </td>
                    </tr>
            </ItemTemplate>
            <FooterTemplate>
       
                </table>
                
            </FooterTemplate>

        </asp:Repeater>
       
        <asp:Repeater ID="Repeater6" runat="server">
            <HeaderTemplate>
                <table width='400px' border='0' cellspacing='0' cellpadding='0' class="tab"  id="stab6" style="display:none">
                    <tr>
                        <th width='140'>
                            <%=GetTran("007952", "会员安置网路图列名")%>
                        </th>
                        <th>
                            <%=GetTran("007947", "是否显示")%>
                        </th>
                        <th>
                            <%=GetTran("000015","操作")%>
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                    <tr>
                        <td>
                            <input type="text" id='txM<%#Eval("ID")%>' value='<%#Eval("FieldName").ToString()%>' style="width:120px;border:gray solid 1px" />
                        </td>
                        <td>
                            <input type="checkbox" id='ckM<%#Eval("ID")%>' <%#Eval("IsVisible").ToString()=="1"?"checked":""%> />
                        </td>
                        <td align='right'>
                            <a href="#" onclick="save('<%#Eval("ID")%>',this)"><%=GetTran("005901","保存")%></a>
                        </td>
                    </tr>
            </ItemTemplate>
            <FooterTemplate>
    
                </table>
            </FooterTemplate>

        </asp:Repeater>
    </div>
    </form>
</body>
</html>