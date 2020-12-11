<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MoneyReport.aspx.cs" Inherits="Company_MoneyReport" %>
<%@ Register assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
	<HEAD>
		<title><%= GetTran("003037", "服务机构汇款汇总")%></title>
				<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../javascript/Mymodify.js"></script>
		<script type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
		
		<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
		<link href="CSS/Company.css" rel="stylesheet" type="text/css" />
		<LINK href="css/tcss.css" type="text/css" rel="stylesheet">

        <script src="js/tianfeng.js" type="text/javascript"></script>
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
	                background-image: url('images/another.gif');
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
		<script type="text/javascript" language="javascript">
            function cut()
            {
                 document.getElementById("span1").title='<%=GetTran("000033", "说 明") %>';
            }
            function cut1()
            {
                 document.getElementById("span2").title='<%=GetTran("000032", "管 理") %>';
            }
        	
	        window.onload=function()
	        {
	            down2();
	            secBoardOnly(0);
	        };
       </script>
	</HEAD>

<body leftmargin="0" topmargin="0">
    <form id="form1" method="post" runat="server">


			<table cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td>
						<table cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td>&nbsp;</td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" border="0">
							<TBODY>
								<tr>
									<td>
										<table cellSpacing="0" cellPadding="0" border="0">
											<tr>
											
												<td><%=GetTran("000522", "起始")%>&nbsp;</td>
												<td><asp:TextBox ID="DatePicker1" CssClass="Wdate" Width="85px" runat="server" onfocus="new WdatePicker()" />
												</td>
												<td>&nbsp;<%=GetTran("000567", "终止")%>&nbsp;</td>
												<td>
												<asp:TextBox ID="DatePicker2" CssClass="Wdate" Width="85px" runat="server" onfocus="new WdatePicker()" /></td>
                                            <td style="display:none;"><FONT face="宋体"><%=GetTran("000444", "分析时段")%>:<%=GetTran("000448", "从")%>
											<asp:label id="lbl_BeginDate" runat="server"></asp:label><%=GetTran("000205", "到")%>
											<asp:label id="lbl_EndDate" runat="server"></asp:label></FONT></td>
									<td align="right"><FONT face="宋体">&nbsp;<%=GetTran("000453", "类型")%>：</FONT></td>
									<td valign="top"><asp:Radiobuttonlist id="rbtn_type" runat="server" Repeatdirection="Horizontal" CssClass="biaozzi">
											<asp:ListItem Value="0" Selected="true">店铺</asp:ListItem>
											<asp:ListItem Value="1">省份</asp:ListItem>
										</asp:Radiobuttonlist></td>
									<td valign="top" align="left">&nbsp;&nbsp;
										<asp:dropdownlist id="ddl_item" runat="server">
											<asp:ListItem Value="-1" Selected="true">汇款</asp:ListItem>
											<asp:ListItem Value="0">周转货</asp:ListItem>
											<asp:ListItem Value="1">报单款</asp:ListItem>
										</asp:dropdownlist>&nbsp;<%--<%=GetTran("000158", "图形")%>：--%>
										<asp:dropdownlist id="ddl_image" runat="server" Visible="false" > 
											<asp:ListItem Value="1">饼图</asp:ListItem>
											<asp:ListItem Value="2">柱形图</asp:ListItem>
										</asp:dropdownlist></td>
                                             <td> &nbsp;&nbsp;<asp:button id="btn_view" OnClientClick="seearch();return false;" runat="server" Text="显 示" onclick="btn_view_Click" CssClass="anyes"></asp:button></td>
												<td>
                                        &nbsp;&nbsp;<asp:button id="Btn_Dsearch" runat="server" Text="按服务机构汇总"
                                             onclick="Btn_Dsearch_Click" CssClass="anyes"></asp:button>
                                        &nbsp;<asp:button id="Btn_Countrysearch" runat="server" Text="按国家汇总" 
                                             CssClass="anyes" onclick="Btn_Countrysearch_Click"></asp:button>
                                        &nbsp;<asp:button id="Btn_Citysearch" runat="server" Text="按省份汇总" 
                                            onclick="Btn_Citysearch_Click" CssClass="anyes"></asp:button>
                                        &nbsp;<asp:button id="Button3" runat="server" Text="图形分析"
                                            Visible="False"></asp:button></td></tr>
										</table>
									</td>
									
											</tr>
								
							</TBODY>
						</table>
						</td>
					</tr>
			</table>
 <table>
               
                <tr>
                    <td>
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                         <%--   <asp:Chart ID="Chart1" runat="server" Height="400px" 
    Width="1000px" BackColor="" 
    onclick="Chart1_Click" >
                                <Legends>
                                    <asp:Legend Name="Legend1" Title="">
                                    </asp:Legend>
                                </Legends>
                                <Series>
                                    <asp:Series Name="Series1" ShadowColor="" 
                    YValuesPerPoint="4" Legend="Legend1" ChartType="Pie" Palette="BrightPastel" CustomProperties="PieLabelStyle=Outside">
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

                               <div id="piechart" style="height:300px;margin-left:50px; width:400px;float:left; border:#ccc 1px solid;"></div>
                              <p id="hover">Mouse position at (<span id="x">0</span>, <span id="y">0</span>). <span
                        id="clickdata"></span></p>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                    </td>
                </tr>
            </table>

    

<div id="cssrain" style="width:100%">
      <table width="99%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
        <tr>
          <td width="80"><table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTableOnly">
              <tr>
                <td class="secOnly" onclick="secBoardOnly(0)">
                    <span id="span1" title="" onmouseover="cut()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
                </td>
                <td class="sec1" onclick="secBoard(1)" style="display:none;">
                    <span id="span2" title="" onmouseover="cut1()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000032", "管 理"))%></span>
                </td>
              </tr>
          </table></td>
          <td><a href="#"><img src="images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX" onclick="down2()"/></a></td>
        </tr>
      </table>
	  <div id="divTab2">
      <table width="99%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
        <tbody style="DISPLAY: block" id="tbody0">
          <tr>
            <td valign="middle" style="padding-left:20px"><table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td>
                   <%=GetTran("006838", "1、用饼状图显示店铺的汇款情况。")%>
                  </td>
                </tr>
            </table></td>
          </tr>
        </tbody>
        <tbody style="DISPLAY: none" id="tbody1">
          <tr>
            <td style="padding-left:20px"><table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td></td>
                </tr>
            </table></td>
          </tr>
        </tbody>
      </table>
	  </div>
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
        var eles= document.getElementsByName("rbtn_type");
        var frp=0;
        for (var i = 0; i < eles.length; i++) {
             if (eles[i].type=="radio"&& eles[i].checked) frp = $(eles[i]).val(); 
        } 
        var act = $("#ddl_item").find("option:selected").val();
      
        var retb = AjaxClass.GetHKTongjitu(frp,act, beg, end).value;

        for (var i = 0; i < retb.length; i++) { 
            piedata.push({ label: retb[i][0], data: retb[i][1] });
            //  cdata.push({ data: retb[i][0], count: retb[i][1] });
        }
 piechart();



    }



</script>