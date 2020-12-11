<%@ Control Language="C#" AutoEventWireup="true" CodeFile="STop.ascx.cs" Inherits="UserControl_STop" %>
<link rel="stylesheet" type="text/css" href="../CSS/bootstrap.css" />
<link href="../Store/CSS/serviceOrganiz.css" rel="stylesheet" />
<%--<script src="../JS/jquery-3.1.1.min.js" type="text/javascript" charset="utf-8"></script>--%>
<%--<script src="../Company/js/jquery-1.4.3.min.js"></script>--%>
<%--<script src="../JS/bootstrap.js" type="text/javascript" charset="utf-8"></script>--%>
<script src="../JS/serviceOrganiz.js" type="text/javascript" charset="utf-8"></script>
<div class="topHead clearfix">
    <div class="pull-left logoBox">
        <a href="javascript:;"><img src="../images/img/logo.png" style="height:60px;" /></a>
    </div>
    <div class="pull-right">
        <a href="javascript:;" class="glyphicon glyphicon-align-justify selectBtn"></a>
        <a href="../Logout.aspx"><%=GetTran("001652","退出")%></a>
        <a href="../store/First.aspx"><%=GetTran("001478","首页") %></a>
    </div>
</div>