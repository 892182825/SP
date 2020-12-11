<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CountryCityPCode.ascx.cs" Inherits="UserControl_CountryCityPCode" %>
<script type="text/javascript" language="javascript">
    function GetCCode(objid1, objid2, objid3, objid4) {
        var obj1 = document.getElementById(objid1);
        var obj2 = document.getElementById(objid2);
        var obj3 = document.getElementById(objid3);
        var obj4 = document.getElementById(objid4);
        var c = obj1.value;
        var p = obj2.value;
        var city = obj3.value;
        var xian = obj4.value;
        if (c != "请选择" && p != "请选择" && city != "请选择" && xian != "请选择") {
            GetCCode_s2(xian);
        }
    }

    function DropDownGetData(obj, objvalue) {
        var objid = document.getElementById(obj);
        for (var i = objid.options.length - 1; i >= 0; i--) {
            objid.options[i] = null;
        }

        //国家、省、市或县，都是以xml文件的格式进行返回的
        var xmlString = AjaxClass.GetCountry(objid.draw).value;

        if (document.all) {
            xmlDoc = new ActiveXObject("MSXML2.DOMDocument.3.0");
        }
        else {
            xmlDoc = document.implementation.createDocument("", "", null);
        }
        xmlDoc.async = false;
        if (document.all) {

            try {

                xmlDoc.loadXML(xmlString);
                var nodes = xmlDoc.documentElement.childNodes;
                var newOption = document.createElement("OPTION");
                newOption.text = "请选择";
                newOption.value = "请选择";
                objid.options.add(newOption);
                for (i = 0; i < nodes.length; i++) {
                    newOption = document.createElement("OPTION");
                    newOption.text = nodes(i).text;
                    newOption.value = nodes(i).text;
                    objid.options.add(newOption);
                }
                if (objvalue != '') objid.value = objvalue;
            } catch (ex) {
                alert(ex + "ssssss")
            }
        } else if (window.XMLHttpRequest) {
            var oParser = new DOMParser();
            xmlDoc = oParser.parseFromString(xmlString, "text/xml");
            if (xmlDoc.documentElement.tagName != "parsererror") {
                //没有错误发生，进行所需操作
                try {
                    var nodes = xmlDoc.documentElement.childNodes;

                    var newOption = new Option("请选择", "请选择");
                    objid.options[0] = newOption;
                    var oSerializer = new XMLSerializer();

                    for (i = 0; i < nodes.length; i++) {
                        var node = nodes[i].childNodes[0].nodeValue
                        newOption = new Option(node, node);
                        objid.options[i + 1] = newOption;
                    }
                } catch (e) {
                    alert(e)
                }

                if (objvalue != '') objid.value = objvalue;
            } else {
                alert("An Error Occurred");
                return;
            }
        }
    }
    function changguojia(obj1, obj2, obj3, obj4, text1, text2, text3, text4, _default) {
        var objid1 = document.getElementById(obj1);
        var objid2 = document.getElementById(obj2);
        var objid3 = document.getElementById(obj3);
        var objid4 = document.getElementById(obj4);
        objid2.draw = "Province:" + objid1.value;
        DropDownGetData(obj2, '');
        objid3.draw = "City:" + objid2.value + ":" + objid1.value;
        DropDownGetData(obj3, '');
        objid4.draw = "Xian:" + objid3.value + ":" + objid2.value + ":" + objid1.value;
        DropDownGetData(obj4, '');
        SaveChange(objid1.value, objid2.value, objid3.value, objid4.value, text1, text2, text3, text4, _default);
    }
    function changsheng(obj1, obj2, obj3, obj4, text1, text2, text3, text4, _default) {
        var objid1 = document.getElementById(obj1);
        var objid2 = document.getElementById(obj2);
        var objid3 = document.getElementById(obj3);
        var objid4 = document.getElementById(obj4);
        objid3.draw = "City:" + objid2.value + ":" + objid1.value;
        DropDownGetData(obj3, '');
        objid4.draw = "Xian:" + objid3.value + ":" + objid2.value + ":" + objid1.value;
        DropDownGetData(obj4, '');

        SaveChange(objid1.value, objid2.value, objid3.value, objid4.value, text1, text2, text3, text4, _default);
    }
    function changcity(obj1, obj2, obj3, obj4, text1, text2, text3, text4, _default) {
        var objid1 = document.getElementById(obj1);
        var objid2 = document.getElementById(obj2);
        var objid3 = document.getElementById(obj3);
        var objid4 = document.getElementById(obj4);
        objid4.draw = "Xian:" + objid3.value + ":" + objid2.value + ":" + objid1.value;
        DropDownGetData(obj4, '');

        SaveChange(objid1.value, objid2.value, objid3.value, objid4.value, text1, text2, text3, text4, _default);
    }
    function changxian(obj1, obj2, obj3, obj4, text1, text2, text3, text4, _default) {
        var objid1 = document.getElementById(obj1);
        var objid2 = document.getElementById(obj2);
        var objid3 = document.getElementById(obj3);
        var objid4 = document.getElementById(obj4);
        SaveChange(objid1.value, objid2.value, objid3.value, objid4.value, text1, text2, text3, text4, _default);
    }

    function SaveChange(obj1, obj2, obj3, obj4, text1, text2, text3, text4, _default) {
        var Text1 = document.getElementById(text1);
        var Text2 = document.getElementById(text2);
        var Text3 = document.getElementById(text3);
        var Text4 = document.getElementById(text4);
        Text1.value = obj1;
        Text2.value = obj2;
        Text3.value = obj3;
        Text4.value = obj4;
    }
</script>

<div style="height: 25px;width:350px; padding-top:5px" id="dv_cpc" runat="server">
<span style="float:left;margin-right:2px;"><asp:DropDownList ID="ddlCountry" CssClass="ctConPgFor" runat="server" DataTextField="Name" draw="m_country" Width="120px" style="width:75px;height:24px;border:1px solid #ccc;border-radius:3px"
    DataValueField="Name" OnDataBound="ddlCountry_DataBound" >
</asp:DropDownList></span>
<span style="float:left;margin-right:2px;"><asp:DropDownList ID="ddlP" CssClass="ctConPgFor" runat="server" DataTextField="Province" draw="m_province"  Width="120px" style="width:75px;height:24px;border:1px solid #ccc;border-radius:3px"
    DataValueField="Province" OnDataBound="ddlP_DataBound">
</asp:DropDownList></span>
<span style="float:left;margin-right:2px;"><asp:DropDownList ID="ddlCity" CssClass="ctConPgFor" runat="server" DataTextField="City"  draw="m_city"  Width="120px"
  style="width:75px;height:24px;border:1px solid #ccc;border-radius:3px"  DataValueField="City" OnDataBound="ddlCity_DataBound">
</asp:DropDownList></span>
<span style="float:left;margin-right:2px;"><asp:DropDownList ID="ddlX"  style="width:100px;height:24px;border:1px solid #ccc;border-radius:3px"
            runat="server" DataTextField="Xian"  draw="m_xian"  Width="120px"
     DataValueField="Xian" OnDataBound="ddlXian_DataBound">
</asp:DropDownList></span>
<%--<span style="float:left;margin-right:2px;"> <asp:TextBox ID="Address" runat="server" Text=""  MaxLength="150"></asp:TextBox></span>   --%>
</div>
<%--<table style="display: none" border="0" cellspacing="0" cellpadding="0">
	<tr >
		<td><asp:textbox id="TextGuojia" runat="server" width="100%" ></asp:textbox></td>
  
 
		<td><asp:textbox id="TextShenFen" runat="server" ></asp:textbox></td>
 
		<td><asp:textbox id="TextCity" runat="server" ></asp:textbox></td>

		<td><asp:textbox id="TextXian" runat="server" ></asp:textbox></td>
	</tr>
	</table>--%>
<table   style="DISPLAY: none" border="0" cellspacing="0" cellpadding="0">
  
	<tr >
		<td style="width:100%"><asp:textbox id="TextGuojia" runat="server" Width="10"></asp:textbox></td>
        
		<td><asp:textbox id="TextShenFen" runat="server" Width="10"></asp:textbox></td>
		<td><asp:textbox id="TextCity" runat="server" Width="16px"></asp:textbox></td>
		<td><asp:textbox id="TextXian" runat="server" Width="16px"></asp:textbox></td>
	</tr>
	</table>
