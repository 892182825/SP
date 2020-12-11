<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowView.aspx.cs" Inherits="Company_ShowView" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>QueryAnZhiNetworkView1</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
		<meta content="C#" name="CODE_LANGUAGE" />
		<meta content="JavaScript" name="vs_defaultClientScript" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
		<style type="text/css">
		    A:link { FONT-SIZE: 12px }
			A:visited { FONT-SIZE: 12px }
			A:active { FONT-SIZE: 12px }
			A:hover { FONT-SIZE: 12px }
			BODY { FONT-SIZE: 12px }
			TD { FONT-SIZE: 12px }
			.ls { FONT-SIZE: 11px }
			.ls1 { FONT-SIZE: 11px;color:Purple; }
			.ls2 { FONT-SIZE: 20px }
		</style>
        <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
		<script type="text/javascript" language="javascript">
			function JSNET(bianhao,tree,flag,qishu,ISAZ,storeid)
			{
			    debugger;
				var StrDiv=document.getElementById(bianhao);
				var statr0=document.getElementById("statr0");

				if(flag==3)
				{
				    qishu=qishu-0;
				    ISAZ=ISAZ-0;
					var str=AjaxClass.WangLuoTu12(bianhao.toString(),tree.toString(),qishu,ISAZ,storeid).value;

					if(tree=="")
                    {
				       statr0.innerHTML="<div id='"+bianhao+"'>"+str+"</div>";
				    }
				    else
					   StrDiv.innerHTML=str;
				}
				else  if(flag==0 )
				{
				    var str=AjaxClass.WangLuoTu_str12(bianhao,qishu).value;
			        StrDiv.innerHTML=tree.replace(/\ /g,"&nbsp;&nbsp;")+"<span class='ls1' style='CURSOR: hand' id='"+bianhao+"' tree='"+tree+"'  ONCLICK=JSNET(this.id,this.tree,3,"+qishu+","+ISAZ+",'"+storeid+"')>田</span>"+"&nbsp;<span id='"+bianhao+"' class='ls' style='CURSOR: hand' title='编号和姓名'  ONCLICK=JSNET(this.id,'',3,"+qishu+","+ISAZ+",'"+storeid+"')>"
                                     +str;
                    if(tree=="")
                    {
				       statr0.innerHTML="<div id='"+bianhao+"'>"+StrDiv.innerHTML+"</div>";
				    }
				}
			}
		</script>
	</head>
    <body MS_POSITIONING="GridLayout">
        <form id="Form2" method="post" runat="server">
            <table id="Table2" cellSpacing="1" cellPadding="0" width="100%" border="0">
                <tr>
                    <td>
                        <table class="t_bg" cellSpacing="1" cellPadding="0" width="100%" border="0">
                            <tr class="t_head">
                                <td style="HEIGHT: 25px" colSpan="2">&nbsp;
                                    <asp:label id="lbl_msg" runat="server">显示</asp:label><asp:dropdownlist id="DropDownList_QiShu" runat="server"></asp:dropdownlist>
                                    <asp:Label id="Lab_Bianhao" runat="server" Visible="False" Height="6px">会员编号</asp:Label><asp:textbox id="txtbianhao" runat="server" Visible="False"></asp:textbox><asp:textbox id="txtceng" runat="server" Width="24px" Height="22px" Visible="False"></asp:textbox>
                                    <asp:button onmousedown="this.className='button_out'" id="Button1" onmouseover="this.className='button_green'"
                                    onmouseout="this.className='button_blue'" runat="server" 
                                        CssClass="button_blue" Text="显示" onclick="Button1_Click"></asp:button>&nbsp;&nbsp;&nbsp;
                                    <asp:button onmousedown="this.className='button_out'" id="Button2" onmouseover="this.className='button_green'"
                                    onmouseout="this.className='button_blue'" runat="server" CssClass="button_blue" Text="回到顶部"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <a href="javascript:window.history.back()"><u>返回</u></A>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<font color="red">以下图中红色代表新增人员</font>
                                    <br />
                                </td>
                            </tr>
                        </table>
                        <table borderColor="#99caf2" cellSpacing="0" borderColorDark="#ffffff" cellPadding="0"
                                border="1"  width=100%>
                            <tr>
                                <td class="ls2" style="FONT-SIZE: 12pt" noWrap>
                                    <div id="statr0" style="OVERFLOW: auto; WIDTH:100%;  TEXT-INDENT: 11mm; LINE-HEIGHT: 12pt; LETTER-SPACING: 0em;"  runat="server"></div>
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </form>
    </body>
</html>
