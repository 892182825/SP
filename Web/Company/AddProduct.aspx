<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddProduct.aspx.cs" Inherits="Company_AddProduct" ValidateRequest="false" %>

<%@ Register TagPrefix="ucl" TagName="ProductColor" Src="~/UserControl/ProductColor.ascx" %>
<%@ Register TagPrefix="ucl" TagName="ProductSexType" Src="~/UserControl/ProductSexType.ascx" %>
<%@ Register TagPrefix="ucl" TagName="ProductSize" Src="~/UserControl/ProductSize.ascx" %>
<%@ Register TagPrefix="ucl" TagName="ProductSpec" Src="~/UserControl/ProductSpec.ascx" %>
<%@ Register TagPrefix="ucl" TagName="ProductStatus" Src="~/UserControl/ProductStatus.ascx" %>
<%@ Register TagPrefix="ucl" TagName="ProductType" Src="~/UserControl/ProductType.ascx" %>
<%@ Register TagPrefix="ucl" TagName="ProductUnit" Src="~/UserControl/ProductUnit.ascx" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
    <title>产品管理</title>
    <link href="CSS/Company.css" type="text/css" rel="Stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/js.js"></script>
    <script language="javascript" type="text/javascript" src="../JS/SqlCheck.js"></script>
    <!--  在线编辑器  -->
    <link rel="stylesheet" href="../javascript/kindeditor/themes/default/default.css" />
	<link rel="stylesheet" href="../javascript/kindeditor/plugins/code/prettify.css" />
	<script type="text/javascript" language="javascript" charset="utf-8" src="../javascript/kindeditor/kindeditor.js"></script>
	<script type="text/javascript" language="javascript" charset="utf-8" src="../javascript/kindeditor/lang/zh_CN.js"></script>
	<script type="text/javascript" language="javascript" charset="utf-8" src="../javascript/kindeditor/plugins/code/prettify.js"></script>

    <script language="javascript" type="text/javascript">
        function reloadopener() {
            window.returnValue = 1;
            top.window.opener = null;
            top.window.close();
        }

        function SelectIsApply() {
            if (!document.getElementById('chbApply').checked) {
                document.getElementById('price0Box').value = '0';
                document.getElementById('price0Box').unselectable = 'On';
                document.getElementById('price1Box').value = '0';
                document.getElementById('price1Box').unselectable = 'On';
                document.getElementById('price2Box').value = '0';
                document.getElementById('price2Box').unselectable = 'On';
                document.getElementById('pv1Box').value = '0';
                document.getElementById('pv1Box').unselectable = 'On';
                document.getElementById('pv2Box').value = '0';
                document.getElementById('pv2Box').unselectable = 'On';
            }
            else {
                document.getElementById('price0Box').unselectable = 'Off';
                document.getElementById('price1Box').unselectable = 'Off';
                document.getElementById('price2Box').unselectable = 'Off';
                document.getElementById('pv1Box').unselectable = 'Off';
                document.getElementById('pv2Box').unselectable = 'Off';
            }
        }

        function yz(th, str) {
            if (th.value.length > 0) {
                if (isNaN(th.value)) {
                    alert(str);
                    th.focus();
                }
            }
        }
		
//		function InitGroupSell() {		   
//		    var ogjGroupSellN = document.getElementById("rbtnN");
//		    var ogjGroupSellY = document.getElementById("rbtnY");
//		    var bForGroup = 0;
//		    if (ogjGroupSellN.checked) {
//		        bForGroup = 0;
//		    }
//		    else {
//		        bForGroup = 1;
//		    }
//		    FunForGroupSells(bForGroup);
//		}
        function InitGroupSell() { 
            var ogjGroupSellN = document.getElementById("rbtnN");
            
            var ogjGroupSellY = document.getElementById("rbtnY");
            if (ogjGroupSellN != null && ogjGroupSellN != undefined && ogjGroupSellY != null && ogjGroupSellY != undefined) {
                var bForGroup = 0;
                if (ogjGroupSellN.checked) {
                    bForGroup = 0;
                }
                else {
                    bForGroup = 1;
                }
                FunForGroupSells(bForGroup);
            }
        }

		function FunForGroupSells(selectValue) {		   
		   // var defaultValue = 0;
		    if (selectValue == 0) {
		        for (var i = 1; i <= 3; i++) {
		            document.getElementById("groupsell" + i.toString()).className = "hidObj";
		            // document .getElementById ("groupsell1").className
		        }
		    }
		    else {
		        for (var i = 1; i <= 3; i++) {
		            document.getElementById("groupsell" + i.toString()).className = "expObj";
		        }
		    }
		}
    </script>

    <style type="text/css">
        .tdl20
        {
            text-align: right;
            width: 18%;
        }
        .tdl30
        {
            text-align: left;
            width: 30%;
        }
        .tdr20
        {
            text-align: right;
            width: 18%;
        }
        .tdr30
        {
            text-align: left;
            width: 30%;
        }
        .hidObj
        {
            display: none;
        }
     
    </style>
</head>
<body onload="RTC();InitGroupSell();">
    <form id="Form1" method="post" runat="server" onsubmit="filterSql_III()">
    <table width="100%" runat="server" id="tbAll">
        <tr>
            <td>
                <table id="Table1" width="100%" class="biaozzi">
                    <tr>
                        <td style="white-space: nowrap">
                            <asp:Label ID="lblMessage" EnableViewState="False" ForeColor="Red" runat="server"></asp:Label>&nbsp;&nbsp;
                            <asp:Panel ID="Panel4" runat="server">
                                <%=GetTran("004152","当前国家")%>：<asp:Label ID="lblCountry" runat="server"></asp:Label>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="editPanel" runat="server">
                    <table id="Table3" width="100%" border="0px" cellpadding="0px" cellspacing="1px"
                        class="tablemb">
                        <tr runat="server" id="tr_State">
                            <td align="right" bgcolor="#EBF1F1" style="white-space: nowrap">
                                <%=GetTran("001952","产品状态")%>：
                            </td>
                            <td>
                                <asp:CheckBox ID="ChboxHide" runat="server" Checked="true" Style="color: Red" Text="是否销售" />
                                <asp:Panel ID="panel1" runat="server" Visible="false">
                                    <asp:RadioButton ID="addP" runat="server" AutoPostBack="True" Text="产 品" GroupName="pp"
                                        Checked="True"></asp:RadioButton>
                                    <asp:RadioButton ID="addClass" runat="server" AutoPostBack="True" Text="类 别" GroupName="pp">
                                    </asp:RadioButton>
                                </asp:Panel>
                                <asp:Label ID="lblclassName" runat="server" Visible="False"><%=GetTran("004217","例如")%>：<%=GetTran("004218","保健品类")%></asp:Label>
                            </td>
                        </tr>
                        <tr runat="server" id="tr_Combine">
                            <td align="right" bgcolor="#EBF1F1" style="white-space: nowrap">
                                &nbsp;&nbsp;
                                <asp:Label ID="lblname" runat="server"></asp:Label><%=GetTran("004193","产品名称")%>：
                            </td>
                            <td style="white-space: nowrap">
                                <asp:TextBox ID="txtProductName" runat="server" MaxLength="20" Enabled="false"></asp:TextBox><font
                                    color="red">*</font>
                                <asp:CheckBox ID="chbcombine" runat="server" Style="color: Red" Text="是组合产品" Enabled="false">
                                </asp:CheckBox>&nbsp;&nbsp;
                                <asp:CheckBox ID="chbApply" runat="server" Text="是赠品" Visible="false" Enabled="False">
                                </asp:CheckBox>
                            </td>
                        </tr>
                        <asp:Panel ID="panel2" runat="server" Visible="true">
                            <tr>
                                <td align="right" bgcolor="#EBF1F1">
                                    <%=GetTran("000263","产品编码")%>：
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtProductCode" runat="server" MaxLength="20"></asp:TextBox><font
                                        color="red">*</font>
                                </td>
                            </tr>
                            <tr style="display: none;">
                                <td align="right" bgcolor="#EBF1F1">
                                    <%=GetTran("007100", "产品用途")%>：
                                </td>
                                <td colspan="2">
                                    <asp:CheckBox ID="ChboxFirst" runat="server" Checked="true" Text="首次报单产品" />
                                    <asp:CheckBox ID="ChboxAgain" runat="server" Checked="true" Text="复消报单产品" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" bgcolor="#EBF1F1">
                                    <%=GetTran("000882","产品型号")%>：
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtProductType" runat="server" MaxLength="10"></asp:TextBox><font
                                        color="red">*</font>
                                    <ucl:ProductType ID="uclProductType" runat="server" Visible="false" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" bgcolor="#EBF1F1">
                                    <%=GetTran("004191","成本价格")%>：
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtCostPrice" runat="server" MaxLength="7" onblur="yz(this,'成本价格只能输入数字！')"
                                        Text="0"></asp:TextBox>
                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" bgcolor="#EBF1F1" style="white-space: nowrap">
                                    <%=GetTran("000620","普通价格")%>：
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtCommonPrice" runat="server" MaxLength="7" Text="0" onblur="yz(this,'普通价格只能输入数字！')"></asp:TextBox>
                                    <asp:Label ID="Label3" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" bgcolor="#EBF1F1" style="white-space: nowrap">
                                    <%=GetTran("000623","普通价积分")%>：
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCommonPV" runat="server" MaxLength="7" Text="0" onblur="yz(this,'普通价积分只能输入数字！')"></asp:TextBox>&nbsp;<%=GetTran("004208","如果您的计划中没有提到积分问题,请填写成与金额相同即可。")%>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" bgcolor="#EBF1F1" style="white-space: nowrap">
                                    <%=GetTran("000627","优惠价格")%>：
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtPreferentialPrice" runat="server" Text="0" MaxLength="7" onblur="yz(this,'优惠价格只能输入数字！')"></asp:TextBox><font
                                        color="red">*</font>
                                    <asp:Label ID="Label2" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" bgcolor="#EBF1F1" style="white-space: nowrap">
                                    <%=GetTran("000631","优惠价积分")%>：
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtPreferentialPV" Text="0" runat="server" MaxLength="7" onblur="yz(this,'优惠价积分只能输入数字！')"></asp:TextBox><font
                                        color="red">*</font> &nbsp;<%=GetTran("004208","如果您的计划中没有提到积分问题,请填写成与金额相同即可。")%>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" bgcolor="#EBF1F1" style="white-space: nowrap">
                                    <%=GetTran("004187","大单位名称")%>：
                                </td>
                                <td colspan="2">
                                    <ucl:ProductUnit ID="uclProductBigUnit" runat="server" />
                                    <font color="red">*</font>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" bgcolor="#EBF1F1" style="white-space: nowrap">
                                    <%=GetTran("004186","小单位名称")%>：
                                </td>
                                <td colspan="2">
                                    <ucl:ProductUnit ID="uclProductSmallUnit" runat="server" />
                                    <font color="red">*</font><%=GetTran("004204","如果没有大单位，请选择与小单位相同的名称")%>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" bgcolor="#EBF1F1" style="white-space: nowrap">
                                    <%=GetTran("004184","一个大单位等于")%>：
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtBigSmallMultiple" runat="server" Width="60px" MaxLength="5"></asp:TextBox><%=GetTran("004202","个小单位")%><font
                                        color="red">*</font><%=GetTran("004203","如1箱等于20瓶，如果没有大单位，请输入1")%>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" bgcolor="#EBF1F1">
                                    <%=GetTran("004183","产品图片")%>：
                                </td>
                                <td>
                                    <asp:FileUpload ID="UpPhotos" runat="server" /><%=GetTran("004073", "产品图片的大小请限制在300K之内") + "..."%>
                                </td>
                                <td rowspan="8">
                                    <asp:Image runat="server" ID="imgProduct" Width="130px" Height="100px" Visible="false"
                                        ImageAlign="Left" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" bgcolor="#EBF1F1">
                                    <%=GetTran("001877","产品产地")%>：
                                </td>
                                <td colspan="1">
                                    <asp:TextBox ID="txtProductArea" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="white-space: nowrap">
                                <td align="right" bgcolor="#EBF1F1">
                                    <%=GetTran("000880","产品规格")%>：
                                </td>
                                <td colspan="1">
                                    <ucl:ProductSpec ID="uclProductSpec" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" bgcolor="#EBF1F1">
                                    <%=GetTran("002174","产品尺寸")%>：
                                </td>
                                <td colspan="1">
                                    <ucl:ProductSize ID="uclProductSize" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" bgcolor="#EBF1F1">
                                    <asp:Label ID="lblProductColor" runat="server"><%=GetTran("001950","产品颜色")%>：</asp:Label>
                                </td>
                                <td colspan="1">
                                    <ucl:ProductColor ID="uclProductColor" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" bgcolor="#EBF1F1">
                                    <%=GetTran("000118","重量")%>：
                                </td>
                                <td colspan="1">
                                    <asp:TextBox ID="txtWeight" runat="server" MaxLength="10" Text="0"></asp:TextBox>g<font
                                        color="red">*</font>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" bgcolor="#EBF1F1">
                                    <%=GetTran("004163","产品形态")%>：
                                </td>
                                <td colspan="1">
                                    <ucl:ProductStatus ID="uclProductStatus" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" bgcolor="#EBF1F1">
                                    <%=GetTran("004162","适用人群")%>：
                                </td>
                                <td colspan="1">
                                    <ucl:ProductSexType ID="uclProductSexType" runat="server" />
                                </td>
                            </tr>
                            <tr style="display:none;">
                                <td align="right" bgcolor="#EBF1F1">
                                   <%=GetTran("007985", "是否只对团队销售")%> ：
                                </td>
                                <td colspan="2">
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <asp:RadioButton ID="rbtnN" onclick="FunForGroupSells(0)" runat="server" Text="否"
                                                    Checked="true" GroupName="GroupSell" />
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="rbtnY" onclick="FunForGroupSells(1)" runat="server" Text="是"
                                                    GroupName="GroupSell" />
                                            </td>
                                            <td id="groupsell3" class="hidObj">
                                                &nbsp;（如果产品只针对团队销售，安置团队与推荐团队中至少填写一项。）
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="groupsell1" class="hidObj">
                                <td align="right" bgcolor="#EBF1F1">
                                    允许购买的安置团队：
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txt_GroupIDS_AZ_TX" runat="server" TextMode="MultiLine"></asp:TextBox>&nbsp;（多团队编号之间用分号隔开）
                                </td>
                            </tr>
                            <tr id="groupsell2" class="hidObj">
                                <td align="right" bgcolor="#EBF1F1">
                                    允许购买的推荐团队：
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txt_GroupIDS_TJ_TX" runat="server" TextMode="MultiLine"></asp:TextBox>&nbsp;（多团队编号之间用分号隔开）
                                </td>
                            </tr>
                            <tr>
                                <td align="right" bgcolor="#EBF1F1" style="white-space: nowrap">
                                    <%=GetTran("000365","预警数量")%>：
                                </td>
                                <td style="white-space: nowrap" colspan="2">
                                    <asp:TextBox ID="txtAlertnessCount" runat="server" MaxLength="6"></asp:TextBox><%=GetTran("004201","当现有库存量低于此数量时,系统将给予提示--其意义类似于涨潮时,要涨到多高就报警!这个高度就是预警")%>
                                </td>
                            </tr>
                        </asp:Panel>
                        <tr class="biaozzi" runat="server" id="tr_Description">
                            <td align="right" bgcolor="#EBF1F1">
                                <%=GetTran("000628","说明")%>：
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Height="40px"
                                    Width="300px" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <asp:Panel ID="Panel3" runat="server">
                            <tr class="biaozzi" runat="server" id="tr1">
                                <td align="right" bgcolor="#EBF1F1">
                                    详细说明：
                                </td>
                                <td colspan="2">
                                    <textarea id="txtDetails"  style="width:95%;height:300px;visibility:hidden;" runat="server"></textarea>
                                    <script type="text/javascript" language="javascript">
		                                KindEditor.ready(function(K) {
			                                var editor1 = K.create('#txtDetails', {
				                                cssPath : '../javascript/kindeditor/plugins/code/prettify.css',
				                                uploadJson : '../javascript/kindeditor/asp.net/upload_json.ashx',
				                                fileManagerJson : '../javascript/kindeditor/asp.net/file_manager_json.ashx',
				                                allowFileManager : true,
				                                afterCreate : function() {
					                                var self = this;
					                                K.ctrl(document, 13, function() {
						                                self.sync();
						                                K('form[name=example]')[0].submit();
					                                });
					                                K.ctrl(self.edit.doc, 13, function() {
						                                self.sync();
						                                K('form[name=example]')[0].submit();
					                                });
				                                }
			                                });
			                                prettyPrint();
		                                });
	                                </script>
                                </td>
                            </tr>
                        </asp:Panel>
                    </table>
                    <br />
                    <table border="0px" cellpadding="0px" cellspacing="0px" width="100%">
                        <tr>
                            <td align="center">
                                <asp:Button ID="doAddButtton" Text="保 存" runat="server" OnClick="doAddButtton_Click"  CssClass="anyes"></asp:Button>&nbsp;&nbsp;
                                <input id="Button1" onclick="reloadopener()" type="button" value='<%=GetTran("004156","关 闭")%>'
                                    class="anyes" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
