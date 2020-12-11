<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowNetworkView.aspx.cs" Inherits="Member_ShowNetworkView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>网络图</title>
   <link href="CSS/Member.css" rel="Stylesheet" type="text/css" />
      <style type="text/css">
    span.ss{cursor:hand;font-size:12px; }

    span.bh{cursor:pointer; color:red;font-size:12px;}
    span.xm{cursor:pointer;color:black;font-size:12px;}
    span.js{cursor:pointer;color:black;font-size:12px;}
    span.bs{cursor:pointer;color:blue;font-size:12px;}
    .box{cursor:pointer; font: 8pt "宋体"; position: absolute; background: LightGrey;color:blue ;z-index:101 }
    </style>
    <style type="text/css">
        A:link
        {
            font-size: 12px;
        }
        A:visited
        {
            font-size: 12px;
        }
        A:active
        {
            font-size: 12px;
        }
        A:hover
        {
            font-size: 12px;
        }
        BODY
        {
            font-size: 12px;
        }
        TD
        {
            font-size: 12px;
        }
        .ls
        {
            font-size: 12px;
        }
        .ls2
        {
            font-size: 16px;
        }
    </style>
    <style type="text/css">
span.ss{cursor:hand;font-size:12px; }

span.bh{cursor:pointer; color:red;font-size:12px;}
span.xm{cursor:pointer;color:black;font-size:12px;}
span.js{cursor:pointer;color:black;font-size:12px;}
span.bs{cursor:pointer;color:blue;font-size:12px;}
.box{cursor:pointer; font: 8pt "宋体"; position: absolute; background: LightGrey;color:blue ;z-index:101 }
</style>
<style type="text/css">
<!--
.STYLE1 {
	font-size: 12px;
	color: #333;
}
.STYLE1 a {
	font-size: 12px;
	color: #333;
	text-decoration: none;
}
.STYLE1 a:hover {
	font-size: 12px;
	color: #FF0000;
	text-decoration: none;
}
.STYLE2 {
	font-size: 12px;
	color: #DFDFDF;
}
.STYLE3 {
	font-size: 7px;
	color: #DFDFDF;
	line-height: 10px;
}
.box{
    filter: progid:DXImageTransform.Microsoft.Shadow(Color=#999999,Direction=120,strength=3);
	border: 1px solid #F3F3F3;
	text-indent: 8px;
	line-height: 22px;
	width: 150px;
} 
 .tr{}
        .td{}
        .img{}
-->
</style>
<script language="JavaScript">
   var message="";
   function clickIE()
   {
    if (document.all)
    {
     (message);
     return false;
    }
   }
   function clickNS(e)
   {
    if (document.layers||(document.getElementById&&!document.all))
    {
     if (e.which==2)
     {
      newX = window.event.x + document.body.scrollLeft
      newY = window.event.y + document.body.scrollTop
      menu = document.all.itemopen
      if ( menu.style.display == "")
      {
       menu.style.display = "none"
      }
      else
      {
       menu.style.display = ""
      }
      menu.style.pixelLeft = newX
      menu.style.pixelTop = newY
     }
     if (e.which==3)
     {
      (message);
      return false;
     }
    }
   }
   if (document.layers)
   {
    document.captureEvents(Event.MOUSEDOWN);
    document.onmousedown=clickNS;
   }
   else
   {
    document.onmouseup=clickNS;document.oncontextmenu=clickIE;
   }
   document.oncontextmenu=new Function("return false")
  </script>
<table ID="menus" class="box" style="display:none" bh="" cellpadding="0" cellspacing="0">
  <tr id="tr10">
    <td width="30" bgcolor="#EFEDDE" class="STYLE1"><img src="images/icon_2.gif" width="14" height="14" /></td>
    <td bgcolor="#FFFFFF" class="STYLE1" onclick="fun1(document.getElementById('menus').bh,1,document.getElementById('menus').Qishu)"><%=GetTran("000806", "编号变色")%></td>
  </tr>
    <tr id="tr19">
    <td bgcolor="#EFEDDE" class="STYLE1"><img src="images/icon_8.gif" width="14" height="14" /></td>
    <td bgcolor="#FFFFFF" class="STYLE1" onclick="fun1(document.getElementById('menus').bh,15,document.getElementById('menus').Qishu)">取消变色</td>
  </tr>
  <tr id="tr11">
    <td bgcolor="#EFEDDE" class="STYLE1"><img src="images/icon_1.gif" width="14" height="14" /></td>
    <td bgcolor="#FFFFFF" class="STYLE1" onclick="fun1(document.getElementById('menus').bh,2,document.getElementById('menus').Qishu)"><%=GetTran("000809", "会员注册")%></td>
  </tr>
  <tr id="tr15">
    <td bgcolor="#EFEDDE" ></td>
    <td bgcolor="#FFFFFF" class="STYLE3">---------------------------</td>
  </tr>
  
  
  <tr id="tr1">
    <td bgcolor="#EFEDDE" class="STYLE1"><img src="images/icon_3.gif" width="14" height="14" /></td>
    <td bgcolor="#FFFFFF" class="STYLE1" onclick="fun1(document.getElementById('menus').bh,6,document.getElementById('menus').Qishu)"><div id="labField1"></div></td>
  </tr>
  <tr id="tr2">
    <td bgcolor="#EFEDDE" class="STYLE1"><img src="images/icon_3.gif" width="14" height="14" /></td>
    <td bgcolor="#FFFFFF" class="STYLE1" onclick="fun1(document.getElementById('menus').bh,7,document.getElementById('menus').Qishu)"><div id="labField2"></div></td>
  </tr>
  <tr id="tr3">
    <td bgcolor="#EFEDDE" class="STYLE1"><img src="images/icon_3.gif" width="14" height="14" /></td>
    <td bgcolor="#FFFFFF" class="STYLE1" onclick="fun1(document.getElementById('menus').bh,8,document.getElementById('menus').Qishu)"><div id="labField3"></div></td>
  </tr>
  <tr id="tr4">
    <td bgcolor="#EFEDDE" class="STYLE1"><img src="images/icon_3.gif" width="14" height="14" /></td>
    <td bgcolor="#FFFFFF" class="STYLE1" onclick="fun1(document.getElementById('menus').bh,9,document.getElementById('menus').Qishu)"><div id="labField4"></div></td>
  </tr>
  <tr id="tr5">
    <td bgcolor="#EFEDDE" class="STYLE1"><img src="images/icon_3.gif" width="14" height="14" /></td>
    <td bgcolor="#FFFFFF" class="STYLE1" onclick="fun1(document.getElementById('menus').bh,10,document.getElementById('menus').Qishu)"><div id="labField5"></div></td>
  </tr>
  <tr id="tr7">
    <td bgcolor="#EFEDDE" class="STYLE1"><img src="images/icon_3.gif" width="14" height="14" /></td>
    <td bgcolor="#FFFFFF" class="STYLE1" onclick="fun1(document.getElementById('menus').bh,18,document.getElementById('menus').Qishu)"><div id="labField7"></div></td>
  </tr>
  
  
   <tr >
    <td bgcolor="#EFEDDE" ></td>
    <td bgcolor="#FFFFFF" class="STYLE3">---------------------------</td>
  </tr>
  <tr id="tr16">
    <td bgcolor="#EFEDDE" class="STYLE1"><img src="images/icon_6-.gif" width="14" height="14" /></td>
    <td bgcolor="#FFFFFF" class="STYLE1" onclick="fun1(document.getElementById('menus').bh,11,document.getElementById('menus').Qishu)"><%=GetTran("000819", "插入标签")%>1</td>
  </tr>
    <tr id="tr17">
    <td bgcolor="#EFEDDE" class="STYLE1"><img src="images/icon_9-.gif" width="14" height="14" /></td>
    <td bgcolor="#FFFFFF" class="STYLE1" onclick="fun1(document.getElementById('menus').bh,12,document.getElementById('menus').Qishu)"><%=GetTran("000819", "插入标签")%>2</td>
  </tr>
    <tr id="tr18">
    <td bgcolor="#EFEDDE" class="STYLE1"><img src="images/icon_11-.gif" width="14" height="14" /></td>
    <td bgcolor="#FFFFFF" class="STYLE1" onclick="fun1(document.getElementById('menus').bh,13,document.getElementById('menus').Qishu)"><%=GetTran("000819", "插入标签")%>3</td>
  </tr>

  <tr id="tr120">
    <td bgcolor="#EFEDDE" class="STYLE1"><img src="images/icon_7.gif" width="14" height="14" /></td>
    <td bgcolor="#FFFFFF" class="STYLE1" onclick="fun1(document.getElementById('menus').bh,14,document.getElementById('menus').Qishu)">取消标签</td>
  </tr>
<!--<tr id="tr21" style="display:none;">
    <td bgcolor="#EFEDDE" class="STYLE1"><img src="images/icon_7.gif" width="14" height="14" /></td>
    <td bgcolor="#FFFFFF" class="STYLE1" onmousedown="settw(event)">调网</td>
  </tr>
  <tr id="tr22" style="display:none;">
    <td bgcolor="#EFEDDE" class="STYLE1"><img src="images/icon_7.gif" width="14" height="14" /></td>
    <td bgcolor="#FFFFFF" class="STYLE1" onclick="untw()">取消调网</td>
  </tr>-->
  <tr style="display:none">
    <td bgcolor="#EFEDDE" class="STYLE1"><img src="images/icon_10.gif" width="14" height="14" /></td>
    <td bgcolor="#FFFFFF" class="STYLE1"><a href="#"><%=GetTran("000819", "插入标签")%>5</a></td>
  </tr>
</table>
    <script type="text/javascript">
    function DataBind()
    {
        debugger;
        var lab1 = document.getElementById('labField1');
        var lab2 = document.getElementById('labField2');
        var lab3 = document.getElementById('labField3');
        var lab4 = document.getElementById('labField4');
        var lab5 = document.getElementById('labField5');
        var lab7 = document.getElementById('labField7');
        
        var t1 = document.getElementById('tr1');
        var t2 = document.getElementById('tr2');
        var t3 = document.getElementById('tr3');
        var t4 = document.getElementById('tr4');
        var t5 = document.getElementById('tr5');
        var t7 = document.getElementById('tr7');
        
        var dispalyText = AjaxClass.GetNetWorkDisplayStatus(2).value;
        var aArray = dispalyText .toString().split(',');
        var count = 0;
        t1.style.display = "none";
        t2.style.display = "none";
        t3.style.display = "none";
        t4.style.display = "none";    
        t5.style.display = "none";
        t7.style.display = "none";
        var i=0;

        for(i=0;i<aArray.length;i++)
        {
            if(aArray[i].toString()!="")
            {
                switch(i)
                {
                    case 0:
                        lab1.innerHTML = aArray[0];
                        t1.style.display = "";
                        break;
                    case 1:
                        lab2.innerHTML = aArray[1];
                        t2.style.display = "";
                        break;
                   case 2:
                        lab3.innerHTML = aArray[2];
                        t3.style.display = "";
                      break;
                    case 3:
                        lab4.innerHTML = aArray[3];
                        t4.style.display = "";
                        break;
                    case 4:
                        lab5.innerHTML = aArray[4];
                        t5.style.display = "";
                        break;
                    case 5:
                        lab7.innerHTML = aArray[5];
                        t7.style.display = "";
                        break;
                    default:
                        break;
                }
            }
        }
    }
    
		    function GetBind(number)
		    {
		        var asg = AjaxClass.GetViewBind(number).values;
		        document.getElementById("divDh").innerHTML = document.getElementById("divDh").innerHTML +" \ "+ asg;
		    }
		    
		    function ShowView(abc)
		    {
		        document.getElementById("txtBh").value = abc.firstChild.nodeValue;
		        document.getElementById("lkn_submit").click();
		    }
		    
		    function popUp(id,Qishu,event) {
                 if (event.button == 2) 
                {
                    twbianhao=id.substring(1,id.length);
                    
                    menu = document.getElementById('menus');//document.all.menus]
                    newX = event.clientX + document.documentElement.scrollLeft
                    newY = event.clientY + document.documentElement.scrollTop
                    menu.style.display = "block"
                    menu.style.left = newX + "px";
                    menu.style.top = newY + "px";
                    if(newY+menu.offsetHeight > ViewportInfo().height+ScrollPosition().top){
                        if(newY - menu.offsetHeight<ScrollPosition().top)
                        {
                            menu.style.top=ScrollPosition().top+"px";
                        }else
                        {
                            menu.style.top = newY - menu.offsetHeight+"px";
                        }
                    }

                    menu.bh=id
                    menu.Qishu=Qishu
                    if(document.getElementById(id).firstChild.style.color=="blue" || document.getElementById(id).firstChild.style.color=="Blue")
                    {
                        document.getElementById("tr10").style.display="none";
                        document.getElementById("tr19").style.display="";
                    }
                    else
                    {
                        document.getElementById("tr10").style.display="";
                        document.getElementById("tr19").style.display="none";
                    }
                    
                }
            }
            function ViewportInfo() {
                var w = (window.innerWidth) ? window.innerWidth : (document.documentElement && document.documentElement.clientWidth) ? document.documentElement.clientWidth : document.body.offsetWidth;
                var h = (window.innerHeight) ? window.innerHeight : (document.documentElement && document.documentElement.clientHeight) ? document.documentElement.clientHeight : document.body.offsetHeight;
                return { width: w, height: h };
            };
            function ScrollPosition() {
                var top = 0, left = 0, width = 0, height = 0;
                if (document.documentElement && document.documentElement.scrollTop) {
                    top = document.documentElement.scrollTop;
                    left = document.documentElement.scrollLeft;
                    width = document.documentElement.scrollWidth;
                    height = document.documentElement.scrollHeight;
                } else if (document.body) {
                    top = document.body.scrollTop;
                    left = document.body.scrollLeft;
                    width = document.body.scrollWidth;
                    height = document.body.scrollHeight;
                };
                return { top: top, left: left, width: width, height: height };
            };
            
            function fun1(bh,flag,qishu)
            {
               debugger;
               var isAnZhi = '<%=isAnZhiNew() %>';
	            document.getElementById('menus').style.display="none"; 
	            var bianhao = bh;//.toString().substring(1,bh.toString().length);
	            switch(flag)
	            {
	                case 1://变色处理代码放在这
	                  //  alert('对编号：'+bianhao+'变色');
                        document.getElementById(bh).firstChild.style.color="Blue";
                        break;

	                case 2://注册处理代码放在这	
	                 //   alert('对编号：'+bianhao+'下级注册');
	                    window.location.href="../Store/RegisterMember.aspx?type=0&bh="+bianhao;
	                    break;
	                case 4://删除处理代码放在这
                   //     alert('删除编号：'+bianhao);
                        //获取上级编号
                        var sjBh = AjaxClass.GetSjBh(bianhao,false).value;

                        //删除会员
	                  if(confirm('<%=GetTran("000832", "您确定要删除该会员吗")%>？'))
                        {
	                        var msg = AjaxClass.DeleteMember(bianhao).value;
	                        alert(msg);
	                        if(msg != '<%=GetTran("000008", "删除成功")%>')
	                        {	            
	                            return;
	                        }
	                    } 
	                    else
	                    {
	                        return;
	                    }

                        //刷新网络图
                         getDataII('<%=ViewState["startbh"]%>','<%=ViewState["cengshu"]%>','<%=ViewState["qs"]%>','<%=ViewState["isanzhi"]%>','sxtj');
	                    break;
	                case 5://调网处理代码放在这
	                //    alert('对编号：'+bianhao+'调网');
	                    window.location.href="tw/ChangeTeam2.aspx?bh="+bianhao;
	                    break;

	                case 6://显示内容1 处理代码放在这
	                    debugger;
	                    var labText = document.getElementById('labField1');
	                //    alert('对编号：'+bianhao+'显示'+labText.innerHTML.toString());

	                    var StrDiv = document.getElementById(bianhao);
//	                    var count = AjaxClass.GetDiv(StrDiv.innerHTML,bianhao).value;
//	                    if(count>0)
//	                    {
//	                        StrDiv = document.getElementById('parent_'+bianhao);
//	                    }

	                    var display = AjaxClass.GetDisplay1(bianhao.toString(),labText.innerHTML.toString(),qishu,2,isAnZhi).value;
	                    StrDiv.innerHTML += "<span class=bs title='"+labText.innerHTML.toString()+"'>["+display+"]</span>";
	                    break;

	                case 7://显示内容2 处理代码放在这

	                    var labText = document.getElementById('labField2');
	                 //   alert('对编号：'+bianhao+'显示'+labText.innerHTML.toString());

	                    var StrDiv = document.getElementById(bianhao);
	                   
	                    var display = AjaxClass.GetDisplay1(bianhao.toString(),labText.innerHTML.toString(),qishu,2,isAnZhi).value;
	                    StrDiv.innerHTML += "<span class=bs title='"+labText.innerHTML.toString()+"'>["+display+"]</span>";
	                    break;

	                case 8://显示内容3 处理代码放在这

	                    var labText = document.getElementById('labField3');
	                //    alert('对编号：'+bianhao+'显示'+labText.innerHTML.toString());

	                    var StrDiv = document.getElementById(bianhao);
	                   
	                    var display = AjaxClass.GetDisplay1(bianhao.toString(),labText.innerHTML.toString(),qishu,2,isAnZhi).value;
	                    StrDiv.innerHTML += "<span class=bs title='"+labText.innerHTML.toString()+"'>["+display+"]</span>";
	                    break;

	                case 9://显示内容4 处理代码放在这

	                    var labText = document.getElementById('labField4');
	                //    alert('对编号：'+bianhao+'显示'+labText.innerHTML.toString());

	                    var StrDiv = document.getElementById(bianhao);
	                   
	                    var display = AjaxClass.GetDisplay1(bianhao.toString(),labText.innerHTML.toString(),qishu,2,isAnZhi).value;
	                    StrDiv.innerHTML += "<span class=bs title='"+labText.innerHTML.toString()+"'>["+display+"]</span>";
	                    break;

	                case 10://显示内容5 处理代码放在这

	                    var labText = document.getElementById('labField5');
	               //     alert('对编号：'+bianhao+'显示'+labText.innerHTML.toString());

	                    var StrDiv = document.getElementById(bianhao);
	                    
	                    var display = AjaxClass.GetDisplay1(bianhao.toString(),labText.innerHTML.toString(),qishu,2,isAnZhi).value;
	                    StrDiv.innerHTML += "<span class=bs title='"+labText.innerHTML.toString()+"'>["+display+"]</span>";
	                    break;

                    case 18://显示内容5 处理代码放在这

	                    var labText = document.getElementById('labField7');
	               //     alert('对编号：'+bianhao+'显示'+labText.innerHTML.toString());

	                    var StrDiv = document.getElementById(bianhao);
	                    var count = AjaxClass.GetDiv(StrDiv.innerHTML,bianhao).value;
	                    
	                    var display = AjaxClass.GetDisplay1(bianhao.toString(),labText.innerHTML.toString(),qishu,2).value;
	                    StrDiv.innerHTML += "<span class=bs title='"+labText.innerHTML.toString()+"'>["+display+"]</span>";
	                    break;
	                    
	                case 11:
	                 //   alert('对编号：'+bianhao+'显示插入图标1');
	                    var StrDiv = document.getElementById(bianhao);
	                    StrDiv.innerHTML = "<span class=js title ='图标1'><img src='images/icon_6-.gif'   align=absmiddle  border=0  width='12' height='12' /></span>" + StrDiv.innerHTML;
	                    break;

	                case 12:
	                 //   alert('对编号：'+bianhao+'显示插入图标2');
	                    var StrDiv = document.getElementById(bianhao);
	                    StrDiv.innerHTML = "<span class=js title ='图标1'><img src='images/icon_9-.gif'  align=absmiddle  border=0  width='12' height='12' /></span>" + StrDiv.innerHTML;
	                    break;

	                case 13:
	                //    alert('对编号：'+bianhao+'显示插入图标3');
	                    var StrDiv = document.getElementById(bianhao);
	                    StrDiv.innerHTML = "<span class=js title ='图标1'><img src='images/icon_11-.gif'  align=absmiddle  border=0  width='12' height='12' /></span>" + StrDiv.innerHTML;
	                    break;
                     case 14:
	                    var StrDiv = document.getElementById(bianhao);
                        var abc = StrDiv.getElementsByTagName("span").length;
			            for(var i=0;i<abc;i++)
			            {
			                if(i==0)
			                {
				                StrDiv.removeChild(StrDiv.firstChild);
				            }
			            }
	                    break;
	                case 15:
	                    debugger;
	                    var nState = AjaxClass.GetNumberState(bianhao.toString(),qishu.toString()).value;
	                    if(nState=="1")
	                    {
	                        document.getElementById(bh).firstChild.style.color="black";
	                    }
	                    else if(nState=="4")
	                    {	                        document.getElementById(bh).firstChild.style.color="Silver";
	                    }
	                    else if(nState=="2")
	                    {
	                        document.getElementById(bh).firstChild.style.color="#FF8686";
	                    } 
	                    else if(nState=="3")
	                    {
	                        document.getElementById(bh).firstChild.style.color="#E30000";
	                    }	        
	                    break;
	                 case 16:
                        if(confirm('确定注销其会员吗？'))
                        {
	                         window.location.href="MemberOff.aspx?bh="+bianhao;
	                    }
	                    break;
	                 case 17:
                        var isdq = AjaxClass.GetRegisUp(bianhao).value;
            
                        if(isdq=='1'||isdq=='2')
                        {
                            alert('对不起，该会员不是当期会员或者非服务机构注册，不能修改');
                            return;
                        }
                        else
                        {
                            window.location.href="../RegisterUpdate1.aspx?OrderID="+isdq+"&Number="+bianhao+"&CssType=2";
                        }
                        
            	        
	                    break;
	                default:
	                    alert('"+<%=GetTran("000843", "选择错误")%>+"！');
	                    break;
	            }
            }
            
             document.documentElement.onclick=function()      
            {  
                    document.getElementById('menus').style.display="none"; 
            }    
    </script>
		<script type="text/javascript">
		    function ShowView(isAnzhi,qishu,number)
		    {
		        var isTrue=false;
		        if(isAnzhi==1)
		        {
		            isTrue = true;
		        }
                var cengshu = '<%=GetCengshu() %>';
                var loginBh = '<%=GetLoginMember() %>'
                var bhCw = AjaxClass.GetLogoutCw(number,qishu,isTrue).value;
                var loginCw = AjaxClass.GetLogoutCw(loginBh,qishu,isTrue).value;
                var showCs = AjaxClass.GetShowCengS(0).value;

                if(showCs- (bhCw -loginCw)<cengshu)
                {
                    cengshu = showCs- (bhCw -loginCw);
                }

                if(cengshu==0)
                {
                    return;
                }
                window.location.href = "ShowNetworkView.aspx?cengshu="+cengshu+"&net="+isAnzhi+"&SelectGrass=" + qishu + "&bh=" + number;
		    }
		</script>
	</HEAD>
	<body onload="javascript:DataBind()">
		<form id="Form2" method="post" runat="server">
		<br />
			<table id="Table2" width="100%" border="0" cellpadding="0" cellspacing="1">
				<tr>
					<td>
						<table width="100%" border="0" cellpadding="0" cellspacing="1" class="tablemb">
							<tr  >
								<TD style="HEIGHT: 25px" colSpan="2">&nbsp;
								    <asp:button id="Button1" runat="server" Text="显示" CssClass=" anyes" 
										  onclick="Button1_Click"></asp:button>&nbsp;&nbsp;
								    <%=GetTran("000045", "期数")%>
							        <asp:dropdownlist id="DropDownList1" runat="server"></asp:dropdownlist>
							        <%=GetTran("000024", "会员编号")%><asp:TextBox ID="txtBh" runat="server" Width="85" MaxLength="10"></asp:TextBox>
									<asp:textbox id="TextBox1" runat="server" Width="24px"></asp:textbox></asp:textbox><%=GetTran("000845", "层")%>
									&nbsp;
									<asp:button id="Button2" runat="server" Text="回到顶部" Visible="false" CssClass=" anyes" 
										  onclick="Button2_Click"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<a href="javascript:window.history.back()" style="display:none"><U><%=GetTran("000421", "返回")%><%=GetTran("000879", "以下图中红色代表新增人员")%></U></A>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:Button ID="btnCY" runat="server" Text="常用" CssClass="anyes" 
                                                onclick="btnCY_Click" />
									<asp:Button ID="Button3" runat="server" Text="表格" CssClass="anyes" 
                                             onclick="Button3_Click" />
                                    <asp:Button ID="Button5" runat="server" Text="展开" Enabled="false" CssClass="anyes" />
                                    <asp:Button ID="Button4" runat="server" Text="伸缩" CssClass="anyes" 
                                               onclick="Button4_Click" />&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="Button6" runat="server" Text="转到安置" CssClass="anyes" 
                                            onclick="Button6_Click" />
                                    <asp:Button ID="Button7" runat="server" Text="转到推荐" CssClass="anyes" 
                                            onclick="Button7_Click" />
								</TD>
							</tr>
						</table>
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="tablema">
						    <tr>
						        <td>
                                    <div id="divDH" runat="server">
                                    </div>
                                </td>
                            </tr>
							<tr>
								<td nowrap style="FONT-SIZE:12pt" class="ls2">
								
									<asp:Repeater ID="Repeater1" Runat="server">
										<ItemTemplate>
											<p style="margin-bottom: -6px"><%#DataBinder.Eval(Container.DataItem, "xinxi")%></p>
										</ItemTemplate>
									</asp:Repeater>
									<p><FONT face="宋体"></FONT>&nbsp;</p>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</TABLE>
			<!--<table width="100%" border="0" cellspacing="0" cellpadding="0">
				<tr>
					<td bgcolor="#1a71b9"><img src="../images/common/spacer.gif" width="1" height="2"></td>
				</tr>
			</table>-->
			<%=msg %>
		</form>
	</body>
</HTML>
