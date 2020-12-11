<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderReport.aspx.cs" Inherits="Company_OrderReport" %>

<%@ Register assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title><%= GetTran("003039", "服务机构订单汇总")%></title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
		<meta content="C#" name="CODE_LANGUAGE" />
		<meta content="JavaScript" name="vs_defaultClientScript" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
		<script language="javascript" src="../javascript/Mymodify.js"></script>
		<script type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
		
		<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
		<link href="CSS/Company.css" rel="stylesheet" type="text/css" />

        <script src="js/tianfeng.js" type="text/javascript"></script>
		<link href="css/tcss.css" type="text/css" rel="stylesheet" />
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
            function cut()
            {
                 document.getElementById("span1").title='<%=GetTran("000033", "说 明") %>';
            }
	        window.onload=function()
	        {
	            down2();
	            secBoardOnly(0);
	        };
    </script>
	</head>
	<body leftMargin="0" topMargin="0">
		<form id="Form1" method="post" runat="server">
		
			<table cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td>
						<table cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td>&nbsp;</td>
							</tr>
						</table>
						<TABLE cellSpacing="0" cellPadding="0" border="0">
							<TBODY>
								<TR>
									<TD colSpan="2" height="28">
										<TABLE cellSpacing="0" cellPadding="0" border="0">
											<TR class="t_white">
												<TD ><%=GetTran("000522", "起始")%>：</TD>
												<TD >
                                                    <asp:TextBox ID="DatePicker1" runat="server" style="width:80px;" onfocus="new WdatePicker()" CssClass="Wdate"></asp:TextBox></TD>
												<TD >&nbsp;&nbsp;<%=GetTran("000567", "终止")%>：
												
												
												</TD>
												<TD nowrap>
                                                    <asp:TextBox ID="DatePicker2" runat="server" onfocus="new WdatePicker()" style="width:80px;"  CssClass="Wdate"></asp:TextBox></TD>
                                                <td nowrap=nowrap>
                                                    &nbsp;<%=GetTran("000453", "类型")%>：
												<asp:radiobuttonlist id="rbtn_type" runat="server" 
                                                    RepeatDirection="Horizontal" style="margin-left: 0px" RepeatLayout="Flow">
											        <asp:ListItem Value="0" Selected="True">店铺</asp:ListItem>
											        <asp:ListItem Value="1">省份</asp:ListItem>
											        <asp:ListItem Value="2">产品</asp:ListItem>
										        </asp:radiobuttonlist>
										        <asp:dropdownlist id="ddl_item" runat="server">
											        <asp:ListItem Value="-1" Selected="True">订货</asp:ListItem>
											        <asp:ListItem Value="0">已支付</asp:ListItem>
											        <asp:ListItem Value="1">未支付</asp:ListItem>
										        </asp:dropdownlist>&nbsp; <%--<%=GetTran("000158", "图形")%>：--%><asp:dropdownlist id="ddl_image" runat="server" Visible="false" > 
											        <asp:ListItem Value="1">饼图</asp:ListItem>
											        <asp:ListItem Value="2" Selected="True">柱形图</asp:ListItem>
										        </asp:dropdownlist>&nbsp;&nbsp;
										        <asp:button id="btn_view" runat="server" Text="显示图表"   OnClientClick="seearch();return false;" 
                                                    CssClass="anyes" onclick="btn_view_Click"></asp:button>
                                                </td>
											</TR>
										</TABLE>
									</TD>
									<TD colSpan="2" height="28"><FONT face="宋体"></FONT>&nbsp;&nbsp;
                                        <asp:button id="Btn_Dsearch" runat="server" Text="按服务机构汇总" 
                                            CssClass="anyes" onclick="Btn_Dsearch_Click" ></asp:button>
                                        &nbsp;&nbsp;
									    <asp:button id="Btn_Countrysearch" runat="server" Text="按国家汇总" 
                                            CssClass="anyes" onclick="Btn_Countrysearch_Click"></asp:button>
                                        &nbsp;&nbsp;
									    <asp:button id="Btn_Citysearch" runat="server" Text="省市汇总" 
                                            CssClass="anyes" onclick="Btn_Citysearch_Click"></asp:button>
                                        &nbsp;&nbsp;
										<asp:button id="Btn_productsearch" runat="server" Text="产品汇总"  
                                            CssClass="anyes" onclick="Btn_productsearch_Click"></asp:button>
                                        &nbsp;&nbsp;
										<asp:Button id="btn_graph" runat="server" Text="图形分析" Visible="False" CssClass="anyes"></asp:Button></TD>
								</TR>
								
							</TBODY>
						</TABLE>
						</td>
					</tr>
			</table>
			
	        <table>
               <tr>
                    <td align="center">
                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                    <asp:ScriptManager ID="ScriptManager2" runat="server">
                    </asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                   <%--             <asp:Chart ID="Chart1" runat="server" Palette="Fire" Width="1000px" 
                                    onclick="Chart1_Click" Height="400px">
                                    <Legends>
                                        <asp:Legend Name="Legend1">
                                        </asp:Legend>
                                    </Legends>
                                    <series>
                                        <asp:Series Name="Series1" Palette="Fire" ChartType="Pie" 
                                    YValuesPerPoint="4" CustomProperties="PieLabelStyle=Outside">
                                        </asp:Series>
                                    </series>
                                    <chartareas>
                                        <asp:ChartArea Name="ChartArea1">
                                        <AxisX Interval="1"><MajorGrid LineColor="" />
                                </AxisX>
                                <Area3DStyle Enable3D="True" />
                                </asp:ChartArea>
                                    </chartareas>
                                </asp:Chart>--%>
                                  <div id="piechart" style="height:400px;margin-left:50px; width:400px;float:left; border:#ccc 1px solid;"></div>
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
                    <%=GetTran("006840", "1、用柱状图、饼状图显示店铺的订单情况。")%>
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
        var eles = document.getElementsByName("rbtn_type");
        var frp = 0;
        for (var i = 0; i < eles.length; i++) {
            if (eles[i].type == "radio" && eles[i].checked) frp = $(eles[i]).val();
        }
        var act = $("#ddl_item").find("option:selected").val();
       
        var retb = AjaxClass.GetODTongjitu(frp, act, beg, end).value;
      
        for (var i = 0; i < retb.length; i++) {
            piedata.push({ label: retb[i][0], data: retb[i][1] });
            //  cdata.push({ data: retb[i][0], count: retb[i][1] });
        }

        piechart();


    }
    </script>