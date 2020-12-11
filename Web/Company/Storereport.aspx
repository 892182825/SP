<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Storereport.aspx.cs" Inherits="Company_Storereport" %>

<%@ Register assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%= GetTran("003036", "服务机构分布情况") %></title>

    <script type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
   <script src="../JS/QCDS2010.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../javascript/Mymodify.js"></script>
 
    <link type="text/css" rel="Stylesheet" href="CSS/Company.css" />
 
    <script src="js/tianfeng.js" type="text/javascript"></script>
   
    <style type="text/css">
        body
        {
            margin-left: 0px;
            margin-top: 0px;
            margin-right: 0px;
            margin-bottom: 0px;
            background-color: White;
            font-family: "宋体";
            font-size: 12px;
            line-height: 24px;
            color: #005575;
            text-decoration: none;
        }
        .but
        {
            color: #FFFFFF;
            background-image: url( 'images/anyes.gif' );
            background-repeat: no-repeat;
            width: 60px;
            border: 0px solid #FFFFFF;
            height: 22px;
        }
    </style>
    <script type="text/javascript" language="javascript">            
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
<body leftmargin="0" topmargin="0">
    <form id="Form1" method="post" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" width="100%" border="0">
                    <tr>
                        <td bgcolor="#ffffff">
                            &nbsp;
                            
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td height="28">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr class="t_white">
                                        <td>
                                            <%=GetTran("000522", "起始")%>：</td>
                                        <td>
                                            <asp:TextBox ID="DatePicker1" runat="server" onfocus="new WdatePicker()" Style="width: 80px;
                                                height: 21px;"></asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp;<%=GetTran("000567", "终止")%>：</td>
                                        <td>
                                            <asp:TextBox ID="DatePicker2" runat="server" onfocus="new WdatePicker()" Style="width: 80px;
                                                height: 21px;" />
                                        </td>
                                          <td>
                              <%--  &nbsp;<%=GetTran("000158", "图形")%>：
                                <asp:DropDownList ID="ddl_image" runat="server">
                                    <asp:ListItem Value="1">饼图</asp:ListItem>
                                    <asp:ListItem Value="2" Selected="True">柱形图</asp:ListItem>
                                </asp:DropDownList>--%>
                                <asp:Button ID="btn_view" OnClientClick="seearch();return false;" runat="server" Text="显示" CssClass="anyes" OnClick="btn_view_Click">
                                </asp:Button>
                            </td>
                                        <td>
                                <font face="宋体"></font>&nbsp;&nbsp;
                                <asp:Button ID="Btn_Dsearch" runat="server" Text="查 询" CssClass="anyes" OnClick="Btn_Dsearch_Click"  >
                                </asp:Button>&nbsp;&nbsp;
                                <asp:Button ID="Btn_Countrysearch" runat="server" Text="按按国家汇总" CssClass="anyes" 
                                                onclick="Btn_Countrysearch_Click"  >
                                </asp:Button>
                            </td>
                                    </tr>
                                </table>
                            </td>
                            
                        </tr>
                        
                    </tbody>
                </table>
                <asp:Label ID="Label1" runat="server" Visible=false></asp:Label>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server"> 
                        <ContentTemplate>

                           
                           <%--<asp:Chart ID="Chart1" runat="server" Height="400px" Width="1000px" BackColor="" onclick="Chart1_Click" >
                                <Legends>
                                    <asp:Legend Name="Legend1" Title="">
                                    </asp:Legend>
                                </Legends>
                                <Series>
                                    <asp:Series Name="Series1" ShadowColor="" YValuesPerPoint="4" Legend="Legend1" ChartType="Pie" Palette="BrightPastel" CustomProperties="PieLabelStyle=Outside">
                                    </asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1" BackGradientStyle="DiagonalLeft" ShadowColor="" BackImageTransparentColor="64, 64, 64" BorderColor="">
                                        <AxisX interval="1">
                                            <MajorGrid LineColor="" />
                                        </AxisX>
                                        <Area3DStyle Enable3D="True" PointDepth="70" WallWidth="1" />
                                    </asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart> --%>

                             <%--<div id="stackchart" style="height:300px; width:500px; float:left;"></div>--%>
                                    <div id="piechart" style="height:300px; width:400px;float:left;"></div>
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
                    １、<%=GetTran("006946", "用柱状图、饼状图显示的店铺分布情情况。")%>
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
                $("#hover").html('<span style="font-weight: bold; color: ' + obj.series.color + '">' + obj.series.label + ' (' + obj.series.data[0][1] + '人)</span>');
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
   
        var retb = AjaxClass.GetTongjitu(1, beg, end).value;
        
        for (var i = 0; i < retb.length; i++) {
            
            piedata.push({ label: retb[i][0], data: retb[i][1] });
            cdata.push({ data: retb[i][0], count: retb[i][1] });
        } 
        for (var i = 0; i < cdata.length; i++) { 
            stkY.push([i, cdata[i].count]); 
            labelx.push([i, cdata[i].date]); 
        }
stskdata = [{ label: "区域店铺统计", data: stkY }];
  piechart();


    }



</script>