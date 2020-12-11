<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WareHouseProductDetails.aspx.cs" Inherits="Company_WareHouseProductDetails" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML xmlns:v="urn:schemas-microsoft-com:vml" xmlns:o="urn:schemas-microsoft-com:office:office">
<head runat="server">
    <title>仓库各产品明细</title>
    <LINK href="css/Company.css" type="text/css" rel="stylesheet">
		<script type="text/javascript" src="../bower_components/jquery/jquery.min.js"></script>
    <link href="../css/bootstrap-united.min.css" rel="stylesheet">

    <link href="../css/charisma-app.css" rel="stylesheet">
    <link href='../bower_components/fullcalendar/dist/fullcalendar.css' rel='stylesheet'>
    <link href='../bower_components/fullcalendar/dist/fullcalendar.print.css' rel='stylesheet' media='print'>
    <link href='../bower_components/chosen/chosen.min.css' rel='stylesheet'>
    <link href='../bower_components/colorbox/example3/colorbox.css' rel='stylesheet'>
    <link href='../bower_components/responsive-tables/responsive-tables.css' rel='stylesheet'>
    <link href='../bower_components/bootstrap-tour/build/css/bootstrap-tour.min.css' rel='stylesheet'>
    <link href='../css/jquery.noty.css' rel='stylesheet'>
    <link href='../css/noty_theme_default.css' rel='stylesheet'>
    <link href='../css/elfinder.min.css' rel='stylesheet'>
    <link href='../css/elfinder.theme.css' rel='stylesheet'>
    <link href='../css/jquery.iphone.toggle.css' rel='stylesheet'>
    <link href='../css/uploadify.css' rel='stylesheet'>
    <link href='../css/animate.min.css' rel='stylesheet'>
	
</head>
<body>
    <form ID="form1" runat="server">    
    <div style="padding-top:18px;" id="visdiv" runat="server">    			
	    <table cellSpacing="0" cellPadding="0" width="800" border="0" class="biaozzi">							
		    <tr>
			    <td align="right"><%=GetTran("000386", "仓库")%>：</td>
			    <td><asp:DropDownList ID="ddlWareHouse" runat="server" AutoPostBack="true" Width="100px" 
                        onselectedindexchanged="ddlWareHouse_SelectedIndexChanged"></asp:DropDownList></td>
			    <td>
				    <asp:Button ID="btnWareHouse" runat="server" Text="仓库汇总"  CssClass="another" onclick="btnWareHouse_Click" />
				    <asp:Button ID="btn_graph" runat="server" Text="图形分析" Visible="False" />
			    </td>
			    <td align="right"><%=GetTran("001930", "仓库图形")%>：</td>
			    <td>
				    <asp:DropDownList ID="ddlWareHouseImage" runat="server" AutoPostBack="true" 
                        onselectedindexchanged="ddlWareHouseImage_SelectedIndexChanged" >
					    <asp:ListItem Value="1" Selected="True">仓库饼图</asp:ListItem>
					    <asp:ListItem Value="2">仓库柱形图</asp:ListItem>
				    </asp:DropDownList>
			    </td>
			    <td><asp:Button ID="btnWareHouseView" runat="server" Text="仓库显示" CssClass="another" 
                        onclick="btnWareHouseView_Click"></asp:Button></td>																	
		    </tr>
		    <tr >
		        <td align="right"><%=GetTran("000390","库位")%>：</td>
			    <td><asp:DropDownList ID="ddlDepotSeat" runat="server" AutoPostBack="true" Width="100px" 
                        onselectedindexchanged="ddlDepotSeat_SelectedIndexChanged"></asp:DropDownList></td>
			    <td><asp:Button ID="btnDepotSeat" runat="server" Text="库位汇总" CssClass="another" 
                        onclick="btnDepotSeat_Click" /></td>	
			    <td align="right"><%=GetTran("001931", "库位图形")%>：</td>
			    <td>
			        <asp:DropDownList ID="ddlDepotSeatImage" runat="server" AutoPostBack="true" 
                        onselectedindexchanged="ddlDepotSeatImage_SelectedIndexChanged">
			            <asp:ListItem Value="1" Selected="True">库位饼图</asp:ListItem>
			            <asp:ListItem Value="2" >库位柱形图</asp:ListItem>
			        </asp:DropDownList>
			    </td>
			    <td><asp:Button ID="btnDepotSeatView" runat="server" Text="库位显示" CssClass="another" 
                        onclick="btnDepotSeatView_Click" /></td>									
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
                <td>
                   <div class="row" id="aa" style="margin-left:0px;display:none;">
             <div class="box col-md-4" style=" padding: 0; background-color: #fff;width:100%">
        <div class="box-inner">
            <div class="box-content">
                <div id="ProductdonutChart" style="height: 400px;width:600px;">
                </div>
            </div>
        </div>
    </div>
        </div><!--/row-->

                     <div class="row" id="bb" style="margin-left:0px;display:none">
        <div class="box col-md-12" style="padding: 0; background-color: #fff;width:100%">
        <div class="box-inner">
            
            <div class="box-content">
                <div id="ProductChart" class="center" style="height:400px;width:600px"></div>
            </div>
        </div>
    </div></div><!--/row-->
                </td>
            </tr>
        </table>
         
         
           <!-- chart libraries start -->
        <script src="../bower_components/flot/excanvas.min.js"></script>
<script src="../bower_components/flot/jquery.flot.js"></script>
<script src="../bower_components/flot/jquery.flot.pie.js"></script>
<script src="../bower_components/flot/jquery.flot.stack.js"></script>
<script src="../bower_components/flot/jquery.flot.resize.js"></script>
        <script src="../bower_components/flot/jquery.flot.time.js"></script>
        <!-- chart libraries end -->
        <script src="../JS/init-chart.js"></script>
  <script>

      //ProductdonutChart
      function yuan() {
          $.plot($("#ProductdonutChart"), data,
              {
                  series: {
                      pie: {
                          innerRadius: 0.5,
                          show: true
                      }
                  },
                  legend: {
                      show: false
                  }
              });
      }
      //ProductChart
      function zhu() {

          var stack = 0, bars = true, lines = false, steps = false;
          //dd1 = dd1.sort(sortNumber);

          function plotWithOptions1() {
              $.plot($("#ProductChart"), [data2], {
                  series: {
                      stack: stack,
                      lines: { show: lines, fill: true, steps: steps },
                      bars: { show: bars, barWidth: 0.6 }
                  }
                  ,
                  xaxis: {
                      ticks: data1
                  }

              });
          }

          plotWithOptions1();

          $(".stackControls input").click(function (e) {
              e.preventDefault();
              stack = $(this).val() == "With stacking" ? true : null;
              plotWithOptions1();
          });
          $(".graphControls input").click(function (e) {
              e.preventDefault();
              bars = $(this).val().indexOf("Bars") != -1;
              lines = $(this).val().indexOf("Lines") != -1;
              steps = $(this).val().indexOf("steps") != -1;
              plotWithOptions1();
          });
      }
        </script>  
        <%=msg %>   
        <%=msg1 %>  
    </div>
          <script src="../bower_components/bootstrap/dist/js/bootstrap.min.js"></script>

<!-- library for cookie management -->
<script src="../js/jquery.cookie.js"></script>
<!-- calender plugin -->
<script src='../bower_components/moment/min/moment.min.js'></script>
<script src='../bower_components/fullcalendar/dist/fullcalendar.min.js'></script>
<!-- data table plugin -->
<script src='../js/jquery.dataTables.min.js'></script>

<!-- select or dropdown enhancer -->
<script src="../bower_components/chosen/chosen.jquery.min.js"></script>
<!-- plugin for gallery image view -->
<script src="../bower_components/colorbox/jquery.colorbox-min.js"></script>
<!-- notification plugin -->
<script src="../js/jquery.noty.js"></script>
<!-- library for making tables responsive -->
<script src="../bower_components/responsive-tables/responsive-tables.js"></script>
<!-- tour plugin -->
<script src="../bower_components/bootstrap-tour/build/js/bootstrap-tour.min.js"></script>
<!-- star rating plugin -->
<script src="../js/jquery.raty.min.js"></script>
<!-- for iOS style toggle switch -->
<script src="../js/jquery.iphone.toggle.js"></script>
<!-- autogrowing textarea plugin -->
<script src="../js/jquery.autogrow-textarea.js"></script>
<!-- multiple file upload plugin -->
<script src="../js/jquery.uploadify-3.1.min.js"></script>
<!-- history.js for cross-browser state change on ajax -->
<script src="../js/jquery.history.js"></script>
<!-- application script for Charisma demo -->
<script src="../js/charisma.js"></script>
    </form>
</body>
</html>
