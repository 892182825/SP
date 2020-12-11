<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberReport2.aspx.cs" Inherits="Company_MemberReport2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%= GetTran("003034", "会员分布情况") %></title>
    <script type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
    <script src="../JS/QCDS2010.js" type="text/javascript"></script>
    <script language="javascript" src="../javascript/Mymodify.js" type="text/javascript"></script>
    <link type="text/css" rel="Stylesheet" href="CSS/Company.css" />
    <link href="CSS/tcss.css" rel="stylesheet" type="text/css" />

    <script src="js/tianfeng.js" type="text/javascript"></script>


    <script type="text/javascript" src="../bower_components/jquery/jquery.min.js"></script>


    <link href="../css/bootstrap-united.min.css" rel="stylesheet" />

    <link href="../css/charisma-app.css" rel="stylesheet" />
    <link href="../bower_components/fullcalendar/dist/fullcalendar.css" rel="stylesheet" />
    <link href="../bower_components/fullcalendar/dist/fullcalendar.print.css" rel="stylesheet" media="print" />
    <link href="../bower_components/chosen/chosen.min.css" rel="stylesheet" />
    <link href="../bower_components/colorbox/example3/colorbox.css" rel="stylesheet" />
    <link href="../bower_components/responsive-tables/responsive-tables.css" rel="stylesheet" />
    <link href="../bower_components/bootstrap-tour/build/css/bootstrap-tour.min.css" rel="stylesheet" />
    <link href="../css/jquery.noty.css" rel="stylesheet" />
    <link href="../css/noty_theme_default.css" rel="stylesheet" />
    <link href="../css/elfinder.min.css" rel="stylesheet" />
    <link href="../css/elfinder.theme.css" rel="stylesheet" />
    <link href="../css/jquery.iphone.toggle.css" rel="stylesheet" />
    <link href="../css/uploadify.css" rel="stylesheet" />
    <link href="../css/animate.min.css" rel="stylesheet" />
    <style type="text/css">
        body {
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

        .but {
            color: #FFFFFF;
            background-image: url('images/anyes.gif');
            background-repeat: no-repeat;
            width: 60px;
            border: 0px solid #FFFFFF;
            height: 22px;
        }
    </style>

    <script type="text/javascript" language="javascript">

        function cut() {
            document.getElementById("span1").title = '<%=GetTran("000033", "说 明") %>';
        }

        window.onload = function () {
            down2();
            secBoardOnly(0);
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>


        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tbody>
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td bgcolor="#ffffff">&nbsp;&nbsp;</td>
                            </tr>
                        </table>
                        <table cellspacing="0" cellpadding="0" border="0">
                            <tr class="t_white">
                                <td>
                                    <%=GetTran("000522", "起始")%>：
                                </td>
                                <td>
                                    <asp:TextBox ID="DatePicker1" runat="server" onfocus="new WdatePicker()" Style="width: 120px; height: 21px;"></asp:TextBox>
                                </td>
                                <td>&nbsp;<%=GetTran("000567", "终止")%>：
                                </td>
                                <td>
                                    <asp:TextBox ID="DatePicker2" runat="server" onfocus="new WdatePicker()" Style="width: 120px; height: 21px;"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <font face="宋体"><%=GetTran("000453", "类型")%>：</font>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rbtn_type" runat="server" RepeatDirection="Horizontal">
                                        <%--<asp:ListItem Value="0" Selected="True">店铺</asp:ListItem>--%>
                                        <asp:ListItem Value="0" Selected="True">省份</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>&nbsp;<%=GetTran("000158", "图形")%>：
                                <asp:DropDownList ID="ddl_image" runat="server">
                                    <asp:ListItem Value="1">饼图</asp:ListItem>
                                    <asp:ListItem Value="2" Selected="True">柱形图</asp:ListItem>
                                </asp:DropDownList>
                                    &nbsp;<asp:Button ID="btn_view" runat="server" Text="显 示" CssClass="another" Style="cursor: hand;" Width="60px" OnClick="btn_view_Click"></asp:Button>
                                </td>
                                <td>
                                    <font face="宋体"></font>
                                    <asp:Button ID="Btn_Countrysearch" runat="server" Text="按按国家汇总" CssClass="another" Style="cursor: hand;" OnClick="Btn_Countrysearch_Click" />&nbsp;
                                <asp:Button ID="Btn_Citysearch" runat="server" Text="按按省份汇总" CssClass="another" OnClick="Btn_Citysearch_Click" Style="cursor: hand;" />&nbsp;
                                <asp:Button ID="btn_graph" runat="server" Text="图形分析" CssClass="but" OnClick="btn_graph_Click" Style="cursor: hand;" Visible="false"></asp:Button>
                                </td>
                            </tr>
                        </table>
                    </td>

                </tr>
                <%--<tr height="30">
                        <td><table><tr>
                       
                            
                        </tr></table><asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:Chart ID="Chart1" runat="server" Height="400px" Width="1000px" BackColor="" onclick="Chart1_Click" >
                                                <Legends>
                                                    <asp:Legend Name="Legend1">
                                                    </asp:Legend>
                                                </Legends>
                                                <Series>
                                                    <asp:Series Name="Series1" ShadowColor="" YValuesPerPoint="4" Legend="Legend1" ChartType="Pie" Palette="BrightPastel">
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
                                            </asp:Chart>
                                        </ContentTemplate>
                        </asp:UpdatePanel></td>
                            
                        </tr>--%>
            </tbody>
        </table>

        <div class="row" id="divstackchart" style="margin-left: 0px; display: none; margin-top: 10px; margin-bottom:20px;">
            <div class="box col-md-4" style="padding: 0; background-color: #fff; width: 100%">
                <div class="box-inner">
                    <div class="box-content">
                        <div id="stackchart" style="height: 400px; width: 550px;">
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row" id="divpiechart" style="margin-left: 0px; display: none; margin-top: 10px; margin-bottom:20px;">
            <div class="box col-md-12" style="padding: 0; background-color: #fff; width: 100%">
                <div class="box-inner">

                    <div class="box-content">
                        <div id="piechart" class="center" style="height: 400px; width: 550px"></div>
                    </div>
                </div>
            </div>
        </div>

        <div id="cssrain" style="width: 100%">
            <table width="99%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
                <tr>
                    <td width="80">
                        <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTableOnly">
                            <tr>
                                <td class="secOnly" onclick="secBoardOnly(0)">
                                    <span id="span1" title="" onmouseover="cut()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td><a href="#">
                        <img src="images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX" onclick="down2()" /></a></td>
                </tr>
            </table>
            <div id="divTab2">
                <table width="99%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
                    <tbody style="display: block" id="tbody0">
                        <tr>
                            <td valign="middle" style="padding-left: 20px">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>１、会员的分布情况，可以按照，省份的分布来查询。
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                    <tbody style="display: none" id="tbody1">
                        <tr>
                            <td style="padding-left: 20px">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>

        <script src="../bower_components/flot/excanvas.min.js"></script>
        <script src="../bower_components/flot/jquery.flot.js"></script>
        <script src="../bower_components/flot/jquery.flot.pie.js"></script>
        <script src="../bower_components/flot/jquery.flot.stack.js"></script>
        <script src="../bower_components/flot/jquery.flot.resize.js"></script>
        <script src="../bower_components/flot/jquery.flot.time.js"></script>

        <script language="javascript">

            function yuan() {
                $.plot($("#piechart"), data,
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

            function zhu() {

                var stack = 0, bars = true, lines = false, steps = false;

                function plotWithOptions1() {
                    $.plot($("#stackchart"), [window.data2], {
                        series: {
                            stack: stack,
                            lines: { show: lines, fill: true, steps: steps },
                            bars: { show: bars, barWidth: 0.6 }
                        },
                        xaxis: {
                            ticks: window.data1
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
