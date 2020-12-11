<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BandanReport.aspx.cs" Inherits="Company_BandanReport" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns:v="urn:schemas-microsoft-com:vml" xmlns:o="urn:schemas-microsoft-com:office:office">
<head id="Head1" runat="server">
    <title><%= GetTran("003043", "会员销售汇总") %></title>
    <link href="css/Company.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
	    window.onerror=function()
	    {
	        return true;
	    };
	</script>

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />			

    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <br />
    <div>
        <table cellspacing="0" cellpadding="0" border="0" class="biaozzi">
            <tr>
                <td><%=GetTran("000398", "起始时间")%>：</td>
                <td><asp:TextBox ID="txtBeginTime" runat="server" CssClass="Wdate" onfocus="new WdatePicker()" /></td>
                <td><%=GetTran("000405", "终止时间")%>：</td>
                <td><asp:TextBox ID="txtEndTime" runat="server" CssClass="Wdate" onfocus="new WdatePicker()" /></td>
                <td colspan="4">                   
                    <asp:Button ID="btnCitySearch" runat="server" Text="按省市汇总" CssClass="another" OnClick="btnCitySearch_Click" />
                    &nbsp;
                    <asp:Button ID="btnProductSearch" runat="server" CssClass="another" Text="按产品汇总" OnClick="btnProductSearch_Click" />
                    &nbsp;
                    <asp:Button ID="btnCountrySearch" runat="server" CssClass="another" 
                        Text="按按国家汇总" onclick="btnCountrySearch_Click" />
                    &nbsp;
                    <asp:Button ID="btn_image" runat="server" Text="图形分析" CssClass="another" Visible="False"
                        OnClick="btn_image_Click" />
                </td>
            </tr>
            <tr>
                
                <td align="right"><%=GetTran("000453", "类型")%>：</td>
                <td >
                    <asp:RadioButtonList ID="rbtn_type" runat="server" RepeatDirection="Horizontal" CssClass="biaozzi">
                        <asp:ListItem Value="0" Selected="True">省份</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td align="right"><%=GetTran("000455", "报单类型")%>：</td>
                <td>
                    <asp:DropDownList ID="ddl_type" runat="server">
                        <asp:ListItem Value="0">首次报单</asp:ListItem>
                        <asp:ListItem Value="1">重复消费</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="2"><%=GetTran("000456", "支付类型")%>：
                    <asp:DropDownList ID="ddl_item" runat="server">
                        <asp:ListItem Value="-1" Selected="True">小计</asp:ListItem>
                        <asp:ListItem Value="0">已支付</asp:ListItem>
                        <asp:ListItem Value="1">未支付</asp:ListItem>
                    </asp:DropDownList>
                 <%--   &nbsp;<%=GetTran("000158", "图形")%>：--%>
                    <asp:DropDownList ID="ddl_image" runat="server" Visible="false">
                        <asp:ListItem Value="1" Selected="True">饼图</asp:ListItem>
                        <asp:ListItem Value="2">柱形图</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btn_view" runat="server"  OnClientClick="seearch();return false;"  Text="显 示" CssClass="anyes" OnClick="btn_view_Click" />                                                    
                </td>
            </tr>           
        </table>
               
        <br />    	    	
        <table >
            <tr>
                <td align="center">
                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>       
                 <div id="piechart" style="height:400px;margin-left:50px; width:400px;float:left; border:#ccc 1px solid;"></div>
                              <p id="hover">Mouse position at (<span id="x">0</span>, <span id="y">0</span>). <span
                        id="clickdata"></span></p>
                     </ContentTemplate>
                        </asp:UpdatePanel>
                </td>
            </tr>
        </table>               
    </div>
    
    </form>
</body>
</html>
 <script src="js/jquery-1.4.3.min.js"></script>
<script src="bower_components/flot/excanvas.min.js"></script>
<script src="bower_components/flot/jquery.flot.js"></script>
<script src="bower_components/flot/jquery.flot.pie.js"></script>
<script src="bower_components/flot/jquery.flot.stack.js"></script>
<script src="bower_components/flot/jquery.flot.resize.js"></script>
<script language="javascript">

    function piechart() {
        if ($("#piechart").length) {
            $.plot($("#piechart"), piedata,
                {
                    series: {
                        pie: {
                            show: true
                        }
                    },
                    grid: {
                        hoverable: true,
                        clickable: true
                    },
                    legend: {
                        show: false
                    }
                });

            function pieHover(event, pos, obj) {
                if (!obj)
                    return;
                // percent = parseFloat(obj.series.percent).toFixed(2);
                $("#hover").html('<span style="font-weight: bold; color: ' + obj.series.color + '">' + obj.series.label + ' (' + obj.series.data[0][1] + '元)</span>');
            }

            $("#piechart").bind("plothover", pieHover);
        }
    }


    var piedata = [];
    var stskdata = [];

    var cdata = [];
    var stkY = [];
    var labelx = [];
    $(function () {




        //pie chart  
        //piedata = [
        //  { label: "I中国", data: 12 },
        //  { label: "外国", data: 27 },
        //  { label: "Safari", data: 85 },
        //  { label: "Opera", data: 64 },
        //  { label: "Firefox", data: 90 },
        //  { label: "Chrome", data: 112 }
        //];
        //  stkY = [[0, 10], [1, 8], [2, 15]  ];
        //  labelx = [["北京", 0], ["上海", 1], [ "深圳",2 ]];

        seearch();
        //   stskdata = [{ label: "区域店铺统计", data: stkY }];

        // loadstak();



    });

    function seearch() {
        piedata = [];
        var beg = $("#DatePicker1").val();
        var end = $("#DatePicker2").val();
        var eles = document.getElementsByName("rbtn_type");
        var frp = 0;
        for (var i = 0; i < eles.length; i++) {
            if (eles[i].type == "radio" && eles[i].checked) frp = $(eles[i]).val();
        }
        var dtp = $("#ddl_type").find("option:selected").val();
        var act = $("#ddl_item").find("option:selected").val();

        var retb = AjaxClass.GetMBTongjitu(frp, dtp, act, beg, end).value;

        for (var i = 0; i < retb.length; i++) {
            piedata.push({ label: retb[i][0], data: retb[i][1] });
            //  cdata.push({ data: retb[i][0], count: retb[i][1] });
        }

        piechart();


    }
    </script>