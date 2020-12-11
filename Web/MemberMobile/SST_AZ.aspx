<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SST_AZ.aspx.cs" Inherits="_SST_AZ" %>

<%@ Register Src="~/UserControl/MemberTop.ascx" TagName="top" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/MemberBottom.ascx" TagName="bottom" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">


<head id="Head1" runat="server">
  <title>安置网络</title>
  <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no">
<meta name="apple-mobile-web-app-capable" content="yes">
<meta name="apple-mobile-web-app-status-bar-style" content="black">
<meta name="format-detection" content="telephone=no">
<script src="js/jquery-1.7.1.min.js"></script>
<link rel="stylesheet" href="css/style.css">
    <link href="CSS/detail.css" rel="stylesheet" type="text/css" />
 
 
</head>
<body  > 
    <form id="form1" runat="server"  >
        	<div class="navbar navbar-default" role="navigation">
       <div class="navbar-inner">	
            	<a class="btn btn-primary btn-lg" style="float: left;padding:6px;text-shadow: 2px 2px 5px hsl(0, 0%, 61%);" href="first.aspx"><i class="glyphicon glyphicon-chevron-left glyphicon-white"></i></a>
            
                <span style="color:#fff;font-size:18px;margin-left:30%;text-shadow: 2px 2px 5px hsl(0, 0%, 61%);font-weight: 600;">安置网络</span>
            </div>
              </div>
       
        <div  style="height:500px; background-color:#eee;">
            <div class="MemberPage" > 
                      <div class="linktop"><ul>
                    <asp:Literal ID="LitLLT"   runat="server"></asp:Literal> 
                                         </ul></div> 
                <div class="linktinner"><div>编号</div><div>昵称</div><div>人数/业绩</div></div>
                <div class="linkcontent">
                    <ul>
                        <asp:Literal ID="litmemberlist" runat="server"></asp:Literal>

                        
                    </ul>
                </div>

                    <!--树-->
                    <table border="0" class="treeTable" cellpadding="0" cellspacing="0" width="600px" id="treeTab" style="width:100%;margin-left:-8px;color: #1c3a1d;">
                        <tbody id="wlt">
                            <asp:Literal ID="litTitle" runat="server"></asp:Literal>
                        </tbody>
                    </table>

         
                </div>
            </div>
            <input type="hidden" id="hidThNumber" />
        </div>
       <!-- #include file = "comcode.html" -->
    </form>
    <br>
    <br>
    <br>
    
    <script type="text/javascript">
        /*function setBottom()
        {
            document.getElementById("bottomDiv").style.top="0px";
            document.getElementById("bottomDiv").style.position="relative";
        }*/
    </script>

    <input type="hidden" id="hidThNumber">
    <%--<div id="rightMenudiv" style="position:absolute;left:-330px;top:0px;width:150px;height:180px;overflow:hidden;background-color:white;border-bottom:rgb(200,200,200) solid 2px;border-right:rgb(200,200,200) solid 2px;border-left:rgb(220,220,220) solid 1px;border-top:rgb(220,220,220) solid 1px" >
        <table style="width:100%;line-height:20px" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td align="center" style="background-color:rgb(239,237,222);padding:0px 3px 0px 3px">
                    <img src="Images/wuser.ico">
                </td>
                <td id="sColor" style="padding-left:8px;color:blue;cursor:pointer;border:white solid 1px" onmouseover="this.style.border='blue solid 1px'" onmouseout="this.style.border='white solid 1px'" onclick="setNumberColor('s')">
                    编号变色
                </td>
            </tr>
            <tr>
                <td align="center" style="background-color:rgb(239,237,222)">
                    <img src="Images/wusers.ico">
                </td>
                <td id="mColor" style="padding-left:8px;color:blue;cursor:pointer;border:white solid 1px" onmouseover="this.style.border='blue solid 1px'" onmouseout="this.style.border='white solid 1px'"  onclick="setNumberColor('m')">
                    团队变色
                </td>
            </tr>
            <tr>
                <td align="center" style="background-color:rgb(239,237,222);">
                    
                </td>
                <td style="color:rgb(223,223,223)">
                    ---------------------------
                </td>
            </tr>
            <tr>
                <td align="center" style="background-color:rgb(239,237,222)">
                    <img src="Images/w1.ico">
                </td>
                <td id="insertImg1" style="padding-left:8px;color:blue;cursor:pointer;border:white solid 1px" onmouseover="this.style.border='blue solid 1px'" onmouseout="this.style.border='white solid 1px'"  onclick="setNumberImg('Images/w1.ico')">
                    插入标签1
                </td>
            </tr>
            <tr>
                <td align="center" style="background-color:rgb(239,237,222)">
                    <img src="Images/w2.ico">
                </td>
                <td id="insertImg2" style="padding-left:8px;color:blue;cursor:pointer;border:white solid 1px" onmouseover="this.style.border='blue solid 1px'" onmouseout="this.style.border='white solid 1px'"   onclick="setNumberImg('Images/w2.ico')">
                    插入标签2
                </td>
            </tr>
            <tr>
                <td align="center" style="background-color:rgb(239,237,222)">
                    <img src="Images/w3.ico">
                </td>
                <td  id="insertImg3" style="padding-left:8px;color:blue;cursor:pointer;border:white solid 1px" onmouseover="this.style.border='blue solid 1px'" onmouseout="this.style.border='white solid 1px'"   onclick="setNumberImg('Images/w3.ico')">
                    插入标签3
                </td>
            </tr>
            <tr>
                <td align="center" style="background-color:rgb(239,237,222)">
                    <img src="Images/w6.ico">
                </td>
                <td  id="insertImg4" style="padding-left:8px;color:blue;cursor:pointer;border:white solid 1px" onmouseover="this.style.border='blue solid 1px'" onmouseout="this.style.border='white solid 1px'"   onclick="setNumberImg('Images/w6.ico')">
                    插入标签4
                </td>
            </tr>
            <tr>
                <td align="center" style="background-color:rgb(239,237,222)">
                    <img src="Images/w7.ico">
                </td>
                <td  id="insertImg5" style="padding-left:8px;color:blue;cursor:pointer;border:white solid 1px" onmouseover="this.style.border='blue solid 1px'" onmouseout="this.style.border='white solid 1px'"   onclick="setNumberImg('Images/w7.ico')">
                    插入标签5
                </td>
            </tr>
            <tr>
                <td align="center" style="background-color:rgb(239,237,222)">
                    
                </td>
                <td style="color:rgb(223,223,223)">
                    ---------------------------
                </td>
            </tr>
            <tr  style="display:none;">
                <td align="center" style="background-color:rgb(239,237,222)">
                    <img src="Images/wmap.ico">
                </td>
                <td style="padding-left:8px;color:blue;cursor:pointer;border:white solid 1px" onmouseover="this.style.border='blue solid 1px'" onmouseout="this.style.border='white solid 1px'"  onclick="linkXX()">
                    会员详情
                </td>
            </tr>
        </table>
    </div>--%>
</body>
</html>
