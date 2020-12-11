<%@ Page Language="C#" AutoEventWireup="true" CodeFile="chanPinDetail.aspx.cs" Inherits="Company_chanPinDetail" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML xmlns:v="urn:schemas-microsoft-com:vml" xmlns:o="urn:schemas-microsoft-com:office:office">
	<head>
		<title>OrderReport</title>
		<script language="javascript" src="../javascript/Mymodify.js"></script>
		<LINK href="css/tcss.css" type="text/css" rel="stylesheet">		
		<LINK href="css/Company.css" type="text/css" rel="stylesheet">
		<style type="text/css">
		    body {
	                margin-left: 0px;
	                margin-top: 0px;
	                margin-right: 0px;
	                margin-bottom: 0px;
	                background-color:White;
	                
	                font-family: "宋体";
	                font-size: 12px;
	                line-height: 24px;
	                color: #005575;
	                text-decoration: none;
                }
                
                .but
                {
                	color: #FFFFFF;
	                background-image: url('images/anyes.gif');
	                background-repeat: no-repeat;
	                width: 60px;
	                border: 0px solid #FFFFFF;
	                height: 22px;
                }
		</style>
		<script type="text/javascript">
		    window.onerror=function()
		    {
		        return true;
		    };
		</script>
		<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
	</head>
	<body leftMargin="0" topMargin="0">
		<form id="Form1" method="post" runat="server">
		<asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
		<br />
		<div>
			<table cellSpacing="0" cellPadding="0" width="400px" border="0">
			    <tr>
				    <td style="WIDTH: 280px"><%=GetTran("000150")%>：
					    <asp:TextBox id="txtStoreId" runat="server" MaxLength="10"></asp:TextBox></td>
				    <td>
				        <asp:Button id="btnStoreInfo" runat="server" CssClass="anyes"  Text="按服务机构汇总"  OnClientClick="seearch();return false;"    onclick="btnStoreInfo_Click" />
				        &nbsp; <asp:Button id="btn_graph" runat="server" class="anyes" Text="图形分析" Visible="False" />
				    </td>
			    </tr>
			    <tr >
				    <td >
						    <asp:label id="lblStoreId" runat="server"></asp:label><%--&nbsp;<%=GetTran("000158")%>：--%>
						<asp:DropDownList id="ddl_image" runat="server" AutoPostBack="true"  Visible="false"
                            onselectedindexchanged="ddl_image_SelectedIndexChanged">
						    <asp:ListItem Value="1"  Selected="True">店铺库存饼图</asp:ListItem>
						    <asp:ListItem Value="2">店铺库存柱形图</asp:ListItem>
					    </asp:DropDownList></td>
				    <td><asp:Button id="btn_view" runat="server"  OnClientClick="seearch();return false;" class="but" Text="显 示" 
                            onclick="btn_view_Click"  /></td>
			    </tr>		
			</table>
			
			<br />    	    	
            <table runat="server" id="tb_Chart">
                <tr>
                    <td align="center">
                        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                     <%--   <asp:Chart ID="displayChart" runat="server" Height="400px" 
    Width="1000px" onclick="displayChart_Click">
                           <Legends>
                                                    <asp:Legend Name="Legend1">
                                                    </asp:Legend>
                                                </Legends>
                                                <Series>
                                                    <asp:Series Name="Series1" ShadowColor="" 
                    YValuesPerPoint="4" Legend="Legend1" ChartType="Pie" Palette="BrightPastel">
                                                    </asp:Series>
                                                </Series>
                                                <ChartAreas>
                                                    <asp:ChartArea Name="ChartArea1" 
                    BackGradientStyle="DiagonalLeft" ShadowColor="" 
                    BackImageTransparentColor="64, 64, 64" BorderColor="">
                                                        <AxisX interval="1">
                                                            <MajorGrid LineColor="" />
                                                        </AxisX>
                                                        <Area3DStyle Enable3D="True" PointDepth="70" WallWidth="1" />
                                                    </asp:ChartArea>
                                                </ChartAreas>
                        </asp:Chart>--%>
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
</HTML>
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
        var txtStoreId = $("#txtStoreId").val();
       

        var retb = AjaxClass.GetCPTongjitu(txtStoreId).value;

        for (var i = 0; i < retb.length; i++) {
            
            piedata.push({ label: retb[i][0], data: retb[i][1] });
            //  cdata.push({ data: retb[i][0], count: retb[i][1] });
        }
    piechart();



    }



</script>