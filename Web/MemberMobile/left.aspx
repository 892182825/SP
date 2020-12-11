<%@ Page Language="C#" AutoEventWireup="true" CodeFile="left.aspx.cs" Inherits="Member_left" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="CSS/Member.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .white
        {
            font-family: "Arial";
            font-size: 12px;
            color: #005575;
            text-decoration: none;
            text-indent: 20px;
            display: block;
            height: 32px;
        }
        .yellow
        {
            font-family: "Arial";
            font-size: 12px;
            color: #00B9FF;
            text-indent: 20px;
            display: block;
            height: 32px;
            text-decoration: none;
        }
    </style>

    <script type="text/javascript">
			var divobj; //要移动的div对象
			var w_divobj; //外围div的对象
			//var size=33; // 移动的步数
			var movePx=1; //每一步的像素
			var moveSD=5; //移动的速度
			//var i=0; //计数器，计数是否移动完毕
			var time1;
			var time2;
			var availMoveH;  //可移动高度
			var isFirst=true; //用于首次加载

			function abc()
			{
				clearTimeout(time2);
				clearTimeout(time1);
				time1=setTimeout("abc()",moveSD);

				if(divobj==null)
					divobj=document.getElementById("div_id");

				if(w_divobj==null)
					w_divobj=document.getElementById("w_div_id");

				//获取可移动的高度
				if(isFirst)
				{
					isFirst=false;

					var w_h=w_divobj.style.height.replace("px","")-0;
					var h=divobj.offsetHeight;
						
					if(h>w_h)
					{
						availMoveH=h-w_h;
					}
					else
					{
						clearTimeout(time1);
						return;
					}
				}

				//if(i<size)
				{
					if(Math.abs(divobj.style.top.replace("px","")-0)<availMoveH)
						divobj.style.top=divobj.style.top.replace("px","")-0-movePx+"px";
					else
					{
						i=0;
						clearTimeout(time1);
					}
				}
				//else
				//{
				//	i=0;
				//	clearTimeout(time1);
				//}
				//i++;
			}

			function ddd()
			{
				clearTimeout(time1);
				clearTimeout(time2);	
				time2=setTimeout("ddd()",moveSD);

				if(divobj==null)
					divobj=document.getElementById("div_id");
				//if(i<size)
				{
					if(divobj.style.top.replace("px","")-0<0)
						divobj.style.top=divobj.style.top.replace("px","")-0+movePx+"px";
					else
					{
						i=0;
						clearTimeout(time2);		
					}
				}
				//else
				//{
				//	i=0;
				//	clearTimeout(time2);
				//}

				//i++;
			}

			function w_exit()
			{
			    clearTimeout(time1);
				clearTimeout(time2);	
			}

			function loadM()
			{
				var h=document.documentElement.clientHeight;

				//alert(h);

				document.getElementById("maindiv_id").style.top=h-(document.getElementById("maindiv_id").style.height.replace("px","")-0)+"px";

				setZXKF();

				if(navigator.userAgent.toLowerCase().indexOf("ie")!=-1)
			        window.frameElement.onresize=gb;
			    else
			        window.onresize=gb;
			}

			function gb()
		    {
		       var h=document.documentElement.clientHeight;

				//alert(h);

				document.getElementById("maindiv_id").style.top=h-(document.getElementById("maindiv_id").style.height.replace("px","")-0)+"px";

				setZXKF();
		    }

			function aacc()
		    {
		        window.parent.document.getElementById("pp").cols="0px,25px,*";
		    }

			function setZXKF()
			{
				setTimeout("setZXKF()",200);

				if(window.parent.frames["topFrame"].isVisible=="hidden")
				{
					document.getElementById("zxkfdiv").style.visibility="visible";
				}
				else if(window.parent.frames["topFrame"].isVisible=="visible")
				{
					document.getElementById("zxkfdiv").style.visibility="hidden";
				}
			}

			function showXZKF()
			{
				window.parent.frames["topFrame"].isVisible="visible";
			}
    </script>
</head>
<body class="lmenudp" onload="loadM()" onresize="loadM()">
    <form id="form1" runat="server">
    <div>
        <table width="195" height="460" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td valign="top">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <img src="images/logo.gif" width="170" height="80" />
                            </td>
							<td>
								<div id="zxkfdiv" style="width:20px;height:80px;border:red solid 1px;visibility:hidden;cursor:pointer" onclick="showXZKF()">
									
								</div>
							</td>
                        </tr>
                    </table>
                    <table width="95%" height="138" border="0" align="right" cellpadding="0" cellspacing="0"
                        class="tongzi">
                        <tr>
                            <td>
                                <marquee onmouseover="this.stop()" onmouseout="this.start()" direction="up" behavior="scroll"
                                    scrollamount="2" height="138">
                                    <%=GetTran("000024", "会员编号")%>：<asp:Literal ID="ltlNumber" runat="server"></asp:Literal><br />
                                    <%=GetTran("000025", "会员姓名")%>：<asp:Literal ID="ltlNme" runat="server"></asp:Literal><br />
                                    <%=GetTran("002051", "上次登录")%>：<asp:Literal ID="ltlTime" runat="server"></asp:Literal><br />
                                    <asp:Literal ID="ltlMessage" runat="server" Visible="false"></asp:Literal>
                                </marquee>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id="divMore" runat="server">
        <div id="maindiv_id" style="position: absolute; left: 10px; top: 0px; width: 176px;
            height: 354px; overflow: hidden;">
            <div class="le-title" id="titleName" runat="server">
                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
            </div>
            <div style="position: absolute; left: 140px; top: 0px; width: 20px; height: 20px;
                overflow: hidden; cursor: pointer" onclick="aacc()">
                aaa
            </div>
            <div id="w_div_id" style="position: absolute; left: 0px; top: 24px; width: 174px;
                height: 330px; overflow: hidden; border: #88E0F4 solid 1px; filter: alpha(opacity=90);
                background-color: #FFFFFF;">
                <div id="div_id" style="position: absolute; left: 0px; top: 0px; width: 140px;" runat="server">
                </div>
                <div id="div1" style="position: absolute; left: 150px; top: 10px; width: 26px;">
                    <div onmouseover="ddd()" onmouseout="w_exit()">
                        <a href="#">
                            <img src="images/top.gif" alt="点击向上滚动" width="20" height="20" border="0" /></a></div>
                    <div onmouseover="abc()" onmouseout="w_exit()" style="margin-top: 270px;">
                        <a href="#">
                            <img src="images/bottom.gif" alt="点击向下滚动" width="20" height="20" border="0" /></a></div>
                </div>
            </div>
        </div>
    </div>
    <div style="position: absolute; left: 10px; bottom: 0px; width: 176px; overflow: hidden;
        display: none; height: 354px;" id="divSome" runat="server">
        <asp:Literal ID="Literal2" runat="server"></asp:Literal>
        <div style="position: absolute; left: 147px; top: 2px; width: 20px; height: 20px;
            overflow: hidden; cursor: pointer" onclick="aacc()">
            <img src="../Company/images/menunu15.gif" align="absmiddle" />
        </div>
    </div>
    </form>
</body>
</html>
