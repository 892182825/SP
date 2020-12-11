<%@ Page Language="C#" AutoEventWireup="true" CodeFile="detest.aspx.cs" Inherits="detest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    
    <script  language=javascript >
         
	  
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="ec" onclick="Button1_Click" />
        
    </div>
    <input id="div1" name="djs" value="2017-10-18 23:59:59"></input> 
    <input id="div2" name="djs" value="2017-10-21 22:59:59"></input> 
    <input id="div3" name="djs" value="2017-10-22 15:59:59"></input> 
    <div id="Text1" name="ddjs"></div> 
    <div id="Div4" name="ddjs"></div>
    <div id="Div5" name="ddjs"></div>
    </form>
</body>
</html>
<script language="javascript" type="text/javascript">
    var interval = 1000;
    function ShowCountDown(dates, divname) {
        var now = new Date();
        var endDate = new Date(dates);
        var leftTime = endDate.getTime() - now.getTime();
        var leftsecond = parseInt(leftTime / 1000);
        //var day1=parseInt(leftsecond/(24*60*60*6)); 
        var day1 = Math.floor(leftsecond / (60 * 60 * 24));
        var hour = Math.floor((leftsecond - day1 * 24 * 60 * 60) / 3600);
        var minute = Math.floor((leftsecond - day1 * 24 * 60 * 60 - hour * 3600) / 60);
        var second = Math.floor(leftsecond - day1 * 24 * 60 * 60 - hour * 3600 - minute * 60);
        //var cc = document.getElementById(divname);
        var str = day1 + "天" + hour + "小时" + minute + "分" + second + "秒";
        if (day1 < 0 || hour < 0 || minute < 0) {
            str = "0天0小时0分0秒";
        }

        divname.innerHTML = str;
    }
    //window.setInterval(function () { ShowCountDown('2017-10-20 23:59:59', 'divdown1'); }, interval);
    

    window.setInterval(function () {
        var dates = document.getElementsByName("djs");
        var divname = document.getElementsByName("ddjs");
        for (var i = 0; i < dates.length; i++) {
            //alert(dates[i].value);
            ShowCountDown(dates[i].value, divname[i]);
        }
    }, interval);
</script> 