<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GradingLevel.aspx.cs"
    Inherits="Company_GradingLevel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>调整级别</title>
    <link href="CSS/level.css" rel="Stylesheet" type="text/css" />
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../JS/jquery-1.2.6.js"></script>
    <script language="javascript" type="text/javascript" src="../js/SqlCheck.js"></script>

    <script language="javascript" type="text/javascript">
    function CheckText()
	{
		//这个方法是只有1个lkSubmit按钮时候 可直接用
	    filterSql(); 

	   
      }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <br />
    <div align="center">
        <asp:Panel ID="Panel1" runat="server" Width="500px">
            <table class="tablemb" cellspacing="1" cellpadding="1" width="100%" bgcolor="#eeeeee"
                border="0">
                 <tr>
                <th colspan="2">
                    <b>
                    <asp:Literal ID="lit_type" runat="server"></asp:Literal>
                    </b>
                </th>
            </tr>
            <tr>
                <td align="right" width="110px">
                    <asp:Literal ID="lit_number" runat="server"></asp:Literal>：
                </td>
                <td align="left">
                <asp:TextBox ID="txtNumber" runat="server" MaxLength="10"></asp:TextBox>
                &nbsp;&nbsp;<span id="sp_error" style="color:red;font-size:12px"></span>
                </td>
            </tr>
                <tr>
                    <td align="right" width="110px">
                          <%= GetTran("000107", "姓名") %>：
                    </td>
                    <td align="left">
                         <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="110px">
                          <%=GetTran("007149", "原级别")%>：
                    </td>
                    <td align="left">
                        <asp:Label ID="LabelResponse" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="110px">
                            <%=GetTran("007553", "新级别")%>：
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="dplLevel" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr id="tr_expect" runat="server" width="110px">
                    <td align="right">
                            <%=GetTran("007554", "调整期数")%>：
                    </td>
                    <td align="left">
                        <asp:Label ID="Label1" runat="server" Text="第1期"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="110px">
                            <%=GetTran("007213", "定级原因")%>：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Rows="3"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="ButtonSubmit" Enabled="false"  runat="server" Text="调整级别" OnClientClick="return CheckAll()" OnClick="ButtonSubmit_Click"
                            CssClass="another" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="Button1" runat="server" Text="返回" CssClass="anyes" OnClick="Button1_Click">
                    </asp:Button>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <input type="hidden" id="hid_level" />
    </form>
    <%=msg %>
</body>
</html>
<script language="javascript" type="text/javascript">
    $(document).ready(
        function() {
            $('#txtNumber').blur(
        function () {
            $('#sp_error').empty();
            if ($(this).val() != "" && $(this).val() != null) {
                var type1 = '<%= ViewState["Type"].ToString() %>';
                var company1 = '<%=Session["Company"].ToString() %>';
                if (type1 != "") {
                    var res = AjaxClass.GetGradingLevel($(this).val(), type1, company1).value;

                    //$.get("../Ajax/GradingBind.ashx", { type: type1, number: $(this).val(), company: company1, time: new Date().getTime() }, function(res) {
                    if (res != "") {
                        try {
                            var strs = new Array();
                            strs = res.split(",");
                            if (parseInt(strs[0]) >= 0) {
                                var name = strs[2];
                                var howmuch = strs[1];
                                $('#lblName').text(name);
                                $('#LabelResponse').text(howmuch);
                                $('#hid_level').val(strs[0]);
                                $('#ButtonSubmit').removeAttr("disabled");
                            } else if (strs[0] == '-2' || strs[0] == "-3") {
                                $('#sp_error').html('<%=GetTran("007975","编号不存在，请重新输入") %>');
                                $('#ButtonSubmit').attr('disabled', 'disabled');
                                return;
                            } else {
                                $('#sp_error').html('<%=GetTran("007976","编号不正确") %>');
                                $('#ButtonSubmit').attr('disabled', 'disabled');
                                return;
                            }
                        } catch (e) {
                            return false;
                        }
                    }
                    //});
                } else { $('#sp_error').html('<%=GetTran("007977","类型不确定") %>'); return false; }
            } else {
                $('#sp_error').html('<%=GetTran("007978","编号不能为空") %> '); //
                $('#ButtonSubmit').attr('disabled', 'disabled');
                return;
            }
        }
    );

            $('#ButtonSubmit').click(
            function() {
                var i = 0;
                if ($('#txtNumber').val() == "") {
                    alert('<%=GetTran("007979","请先填写编号") %>');
                    i = -1;
                    return false;
                }
               <%-- if ($('#lblName').text() == "" || $('#LabelResponse').text() == "") {
                    alert('<%=GetTran("007980","请检查填写的信息") %>');
                    i = -1;
                    return false;
                }--%>

                if ($('#dplLevel').val() == $('#hid_level').val()) {
                    alert('<%=GetTran("007981","调整后的级别与调整前的一致，不允许调整") %>');
                    i = -1;
                    return false;
                }
                if (i < 0) {
                    return false;
                }
            }
        );
        }
    )

</script>