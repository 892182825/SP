<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DataModel.aspx.cs" Inherits="Company_SetParams_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
	<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=yes" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />
    <title>数据模型</title>
    <link href="../../CSS/bootstrap-cerulean.min.css" rel="stylesheet" />
    <link href="../../CSS/bootstrap.css" rel="stylesheet" />
    <script src="../bower_components/jquery/jquery.min.js"></script>
    <script src="../../JS/bootstrap.js"></script>
    <script>
        $(function () {
          
            $("#sctl").click(function () {
                var ck ="sctl";
                $("#HiddenField1").val(ck);

            });
            $("#rstl").click(function () {
                var ck = "rstl";
                $("#HiddenField1").val(ck);

            });
            $("#cjjs").click(function () {
                var ck = "cjjs";
                $("#HiddenField1").val(ck);

            });
            $("#mnyx").click(function () {
                var ck = "mnyx";
                $("#HiddenField1").val(ck);

            });

        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="view">
							<div class="tabbable" id="tabs-45854">
								<!-- Only required for left/right tabs -->
								<ul class="nav nav-tabs">
									<li class="active" id="sctl" runat="server">
										<a href="#panel-625832" data-toggle="tab" >市场体量</a>
									</li>
									<li id="rstl">
										<a href="#panel-677389" data-toggle="tab" >人数体量</a>
									</li>
                                    <li id="cjjs">
										<a href="#panel-665412" data-toggle="tab" >未定</a>
									</li>
                                    <li id="mnyx">
										<a href="#panel-665444" data-toggle="tab" >未定</a>
									</li>
								</ul>
								<div class="tab-content">
                                    <div class="tab-pane active" id="panel-625832">
                                        <div class="form-group">
                                            <label for="exampleInputEmail1">入金(U)</label>
                                            <asp:TextBox runat="server" ID="rj" class="form-control" placeholder="入金"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label for="exampleInputEmail1">每日递增(%)</label>
                                            <asp:TextBox runat="server" ID="dz" class="form-control" placeholder="每日递增"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label for="exampleInputEmail1">时间（天）</label>
                                            <asp:TextBox runat="server" ID="shijian" class="form-control" placeholder="时间"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label for="exampleInputEmail1">每日卖出（每天入金的%）</label>
                                            <asp:TextBox runat="server" ID="maichu" class="form-control" placeholder="每日卖出"></asp:TextBox>
                                        </div>
                                    </div>
									<div class="tab-pane" id="panel-677389">
										<div class="form-group">
                                            <label for="exampleInputEmail1">衡定矿机等级(U)</label>
                                            <asp:TextBox runat="server" ID="TextBox1" class="form-control" placeholder="衡定矿机等级"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label for="exampleInputEmail1">初始人数</label>
                                            <asp:TextBox runat="server" ID="TextBox2" class="form-control" placeholder="初始人数"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label for="exampleInputEmail1">时间（天）</label>
                                            <asp:TextBox runat="server" ID="TextBox3" class="form-control" placeholder="时间"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label for="exampleInputEmail1">人数递增（%）</label>
                                            <asp:TextBox runat="server" ID="TextBox4" class="form-control" placeholder="人数递增"></asp:TextBox>
                                        </div>
									</div>
                                    <div class="tab-pane" id="panel-665412">
										其他数据模型看需要哪个再商量添加
									</div>
								</div>
							</div>
						</div>
            <asp:Button runat="server" ID="btn" CssClass="btn btn-default" OnClick="btn_Click" Text="提交" />
            <asp:HiddenField ID="HiddenField1" runat="server" Value="sctl" />
            <div id="modelshow" runat="server" class="view">
             
        </div>  
        </div>
    </form>
</body>
</html>
