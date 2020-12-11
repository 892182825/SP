<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SMSSend.aspx.cs" Inherits="Company_SMSSend" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
     <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../../SqlCheck.js"></script>
      <script src="../JS/QCDS2010.js" type="text/javascript"></script>

    <script src="js/SMSS.js" type="text/javascript"></script>

    <script src="js/tianfeng.js" type="text/javascript"></script>





    
    
    <script language="javascript" type="text/javascript">

    function CheckText(btname)
	{
		//这个方法是页面有多个按钮要提交时，多次使用这个方法,传入按钮的ID
		filterSql_II (btname);
		
	}
</script>
   <script type="text/javascript">
       function down2() {
           if (document.getElementById("divTab2").style.display == "none") {
               document.getElementById("divTab2").style.display = "";
               document.getElementById("imgX").src = "images/dis1.GIF";

           }
           else {
               document.getElementById("divTab2").style.display = "none";
               document.getElementById("imgX").src = "images/dis.GIF";
           }
       }
    </script>
   <style type="text/css" >      
        .MobileList{width:300px;}
        .divR0{text-align:left ;float:left;font-size:9pt;height:20px;margin-left:3px;}
        .divR1{text-align:left ;float:left;font-size:9pt;padding-top:5px;height:20px;margin-left:3px;}
             .divt
        {
            text-align:left ;float:left;width:100%;height:20px;font-size:9pt;padding-top:5px;
            }
             .divOut
        {
            text-align:left ;float:left;width:100%;height:20px;font-size:9pt;padding-top:5px;background-color:White;color:#000000;
            }
             .divOver
        {
            text-align:left ;float:left;width:100%;height:20px;font-size:9pt;padding-top:5px;background-color:Blue;color:White ;
            }
        .divControls
        {
            display :none ;
            BORDER-RIGHT:#1285b4 1px solid; 
            BORDER-TOP:#1285b4 1px solid; Z-INDEX:101; LEFT:4px; 
            BORDER-LEFT:#1285b4 1px solid; WIDTH:auto;
            WORD-BREAK:break-all;
            BORDER-BOTTOM:#1285b4 1px solid; 
            POSITION:absolute; TOP:316px; BACKGROUND-COLOR:#ffffff; 
            TEXT-ALIGN:left;
            width:200px;
            cursor :pointer ;
            }
         
    </style>
</head>

<body onload="down2()">
    <form id="form1" runat="server" onmousedown="mouseDown(event)">
    	
    <div>
				<table  cellspacing="0" cellpadding="0" border="0" width="100%" class="biaozzi">
					<tr>
						<td bgColor="#ffffff">&nbsp;</td>
					</tr>
				</table>
				<table  cellspacing="0" cellpadding="0" border="0" width="100%" class="biaozzi">
					<tr>
						<td colspan ="4">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%=GetTran("000482", "当前操作")%>:<font color="#ff0033"><%=GetTran("000484", "短信群发")%></font>
						</td>
					</tr>					
					<tr >
						<td colspan ="4">
						<div class ="divR1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%=GetTran("000912", "接收对象")%>：</div>
						<div class ="divR0">
						<asp:DropDownList ID="ddlRecever" runat ="server" 
                                onselectedindexchanged="ddlRecever_SelectedIndexChanged" 
                                AutoPostBack="True" >
                         <asp:ListItem Value="-1" Selected="True">指定手机</asp:ListItem>
                        <asp:ListItem Value="3">会员</asp:ListItem>
                           <%-- <asp:ListItem Value="2">店长</asp:ListItem>--%>
                            
                        </asp:DropDownList>
						</div> 
                        <div class ="divR1" id="divM0" runat ="server" ><%=GetTran("006631", "会员类型")%>：</div> 
                        <div class ="divR0" id="divM1" runat ="server" >
                        <asp:DropDownList ID="ddlMemberType" runat ="server" AutoPostBack="True" 
                                onselectedindexchanged="ddlMemberType_SelectedIndexChanged" >
                         <asp:ListItem Value="-1" Selected="True" >所有</asp:ListItem>
                         <asp:ListItem Value="0">今天生日的会员</asp:ListItem>
                            <asp:ListItem Value="1">奖金己发放</asp:ListItem>
                            <asp:ListItem Value="2">推荐网络团队为</asp:ListItem>
                             <asp:ListItem Value="3">安置网络团队为</asp:ListItem>
                        </asp:DropDownList></div>
                            <asp:TextBox ID="MangeTerm" runat="server" Visible=false></asp:TextBox> 
                         <div class ="divR1" id="divM2"   runat ="server" ><%=GetTran("000045", "期数")%>：</div> 
                        <div class ="divR0" id="divM3"  runat ="server" ><asp:DropDownList ID="ddlQishu" runat ="server" ></asp:DropDownList></div> 
                       
                        <div class ="divR1"  id="divM4" runat ="server" ><%=GetTran("000243", "奖金")%>：</div> 
                        <div class ="divR0"  id="divM5" runat ="server" >
                        <asp:DropDownList ID="ddlMatch" runat ="server"  >
                            <asp:ListItem>&gt;</asp:ListItem>
                            <asp:ListItem>=</asp:ListItem>
                            <asp:ListItem>&lt;</asp:ListItem>
                            <asp:ListItem>&gt;=</asp:ListItem>
                            <asp:ListItem>&lt;=</asp:ListItem>
                            </asp:DropDownList></div> 
                        <div class ="divR0" id="divM6"  runat ="server" ><asp:TextBox ID="txtkeyWords" runat="server" style="width:60px;" 
                                    MaxLength="12"></asp:TextBox></div> 
                         <div class ="divR1" id="divMS0"  runat ="server" ><%=GetTran("000046", "级别")%>：</div> 
                        <div class ="divR0"  id="divMS1"  runat ="server" ><asp:DropDownList ID="ddlJibie" runat ="server" ></asp:DropDownList></div>                       
                                <asp:LinkButton ID="lbtnSaveNew" runat="server" style="display:none ;" onclick="lbtnSaveNew_Click">保存内容</asp:LinkButton>
                        </td>
                       
                     
					</tr>
					</table>
				<table  cellspacing="0" cellpadding="0" border="0" width="100%" class="biaozzi">
						<tr>
							<td vAlign="top">
								<TABLE cellspacing="0" cellpadding="0" border="0" width="100%" class="tablemb">
									
									<tr id="trAdd"  runat="server">
										<td width="17">&nbsp;&nbsp;
										</td>
										<td  align="center" style="text-align: left;" colspan="4">
                                            <div style="height:30px;margin-top:2px;float:left;text-align:right;"><%=GetTran("000489", "接收号码")%>：</div> 
                                           <div style="height:30px;float:left; width: 240px; text-align:left;">
                                               <asp:TextBox ID="txtMobile" runat="server" style="width:100px" MaxLength="15"></asp:TextBox>
                                            &nbsp;<asp:button id="btnAddMobile" 
                                                Height="20px"  CssClass="another" runat ="server" Text="添加号码" 
                                                onclick="btnAddMobile_Click"></asp:button></div>                                          
										</td>
									</tr>									
									<tr>
										<td width="17">&nbsp;</td>
										<td  align="center" style="text-align: left;" colspan="4" >
                                           <div style ="float:left;width:300px;text-align:center ;">
                                           <%=GetTran("005714", "接收列表")%>
                                           </div>
                                           <div style ="float:left;width:100px;"></div>
                                           <div style ="float:left;width:350px;text-align:center ;">
                                           <%=GetTran("000490", "发送内容")%>
                                           </div>
                                           </td>
									</tr>
									<tr>
										<td width="17">&nbsp;</td>
										<td  align="center" style="text-align: left;" colspan="4" >
                                           <div style ="float:left;width:300px;">
                                            <asp:ListBox ID="ListBoxMobiles" runat="server"  style="width:290px;height:350px;" >
                                            </asp:ListBox>
                                            </div>
                                           <div style ="float:left;width:100px;text-align:center ;margin-top:150px;">
                                            <asp:button id="btnRemoveMobile" Width="60px" 
                                                Height="20px"  CssClass="another" runat ="server" Text="移除号码" 
                                                onclick="btnRemoveMobile_Click"></asp:button>
										    </div>
                                           <div style ="float:left;width:350px;">
                                           <asp:textbox id="txtMsg" MaxLength ="1000" runat="server"  style="width:330px;height:340px;" BorderStyle="Groove" TextMode="MultiLine" onmouseup="getValue(this)"  ></asp:textbox>
                                           </div>
                                           </td>
									</tr>
									<tr>
										<td colspan ="5" style="width:100%;text-align:center ;padding-left:280px;padding-top:10px;margin-bottom:10px;" >
										<div class ="divR0" >
										<asp:button id="btnSend" Width="60px" 
                                                Height="20px"  CssClass="anyes" runat ="server" Text="发 送" 
                                                onclick="btnSend_Click"></asp:button></div>
										<div class ="divR0" >
										<asp:button id="btnClear"  Width="60px" Height="20px" 
                                                runat ="server" CssClass="anyes" Text="重 填" onclick="btnClear_Click"></asp:button></div>									
										<div class ="divR0" >
										<input id="Button2" class="anyes" type="button" onclick ="FunGetPreSMS()" value='<%=GetTran("000487","预设短语")%>' /></div> 
										<div class ="divR0"  id="divSmsSet" runat ="server" >
										<input id="btnIndert" class="anyes" type="button"  onclick ="javascript:showDiv(this.id);" value='<%=GetTran("006547","内容设置")%>' /></div> 	<br />&nbsp;	
										</td> 
									</tr>
								</TABLE>
							</td>
							
						</tr>
						<tr>
							<td>
							</td>
						</tr>
				 </table>		
				
    </div>
    
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
                    
                                                     <%=GetTran("000484", "短信群发")%><br /> 
                    1.<%=GetTran("006807", "短信内容不能超过256个字符。")%><br />
                    2.<%=GetTran("006670", "根据不同的过滤条件，针对不同范围和数量的手机号码群进行信息群发。")%><br />           
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
                
          <!--菜单DIV-->      
                 <input id="txtIndex" runat ="server" value ="0"   type ="hidden"  />
    <div id="divControl"  class="divControls" >
    <div id="divRow1" title ="存储为短语"  class ="divt" onmouseout ="mOut(this)" onmouseover ="mOver(this)"  onclick ="javascript:FunOperate('0');" >&nbsp;&nbsp;存储为短语</div>
    <div id="divRow2" title ="光标处插入客户[编号]"   class ="divt" onmouseout ="mOut(this)" onmouseover ="mOver(this)"  onclick ="javascript:FunOperate('1');" >&nbsp;&nbsp;插入客户[编号]</div>
    <div id="divRow3" title ="光标处插入客户[姓名]"   class ="divt" onmouseout ="mOut(this)" onmouseover ="mOver(this)"  onclick ="javascript:FunOperate('2');" >&nbsp;&nbsp;插入客户[姓名]</div>
    <div id="divRow4" title ="光标处插入客户[称呼]"   class ="divt" onmouseout ="mOut(this)" onmouseover ="mOver(this)" onclick ="javascript:FunOperate('3');" >&nbsp;&nbsp;插入客户[称呼]</div>
     </div>
    </form> 
</body>
</html>
