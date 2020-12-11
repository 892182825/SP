<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowNetworkViewNewTJ.aspx.cs" Inherits="Member_ShowNetworkViewNewTJ" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>QueryAnZhiNetworkView1</title>
		<script src="../JS/jquery.js" type="text/javascript"></script>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="C#" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		<link rel="Stylesheet" href="CSS/Member.css" type="text/css" />
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
		<!--------------设置网络图的样式(根据需要修改)------------------->
<style type="text/css">
span.ss{cursor:pointer;font-size:12px; } 
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
<body>
<!--下面代码采取动态生成，box(需要建一个表保存管理员设置的菜单项)-->
<table ID="menus" class="box" style="display:none" bh=""  cellpadding="0" cellspacing="0">
  <tr>
    <td width="30" bgcolor="#EFEDDE" class="STYLE1"><img src="images/icon_2.gif" width="14" height="14" /></td>
    <td bgcolor="#FFFFFF" class="STYLE1"><a href="#" onclick="fun1(document.getElementById('menus').bh,1,document.getElementById('menus').Qishu)"><%=GetTran("000806", "编号变色")%></a></td>
  </tr>
    <tr id="tr19">
    <td bgcolor="#EFEDDE" class="STYLE1"><img src="images/icon_8.gif" width="14" height="14" /></td>
    <td bgcolor="#FFFFFF" class="STYLE1"><a href="#" onclick="fun1(document.getElementById('menus').bh,15,document.getElementById('menus').Qishu)">取消变色</a></td>
  </tr>
  <tr>
    <td bgcolor="#EFEDDE" class="STYLE1"><img src="images/icon_1.gif" width="14" height="14" /></td>
    <td bgcolor="#FFFFFF" class="STYLE1"><a href="#" onclick="fun1(document.getElementById('menus').bh,2,document.getElementById('menus').Qishu)"><%=GetTran("000809", "会员注册")%></a></td>
  </tr>
  <tr>
    <td bgcolor="#EFEDDE" class="STYLE1"></td>
    <td bgcolor="#FFFFFF" class="STYLE3">---------------------------</td>
  </tr>
 <tr>
    <td bgcolor="#EFEDDE" class="STYLE1"><img src="images/icon_6-.gif" width="14" height="14" /></td>
    <td bgcolor="#FFFFFF" class="STYLE1" onclick="fun1(document.getElementById('menus').bh,11,document.getElementById('menus').Qishu)"><%=GetTran("000819", "插入标签")%>1</td>
  </tr>
    <tr>
    <td bgcolor="#EFEDDE" class="STYLE1"><img src="images/icon_9-.gif" width="14" height="14" /></td>
    <td bgcolor="#FFFFFF" class="STYLE1" onclick="fun1(document.getElementById('menus').bh,12,document.getElementById('menus').Qishu)"><%=GetTran("000819", "插入标签")%>2</td>
  </tr>
    <tr>
    <td bgcolor="#EFEDDE" class="STYLE1"><img src="images/icon_11-.gif" width="14" height="14" /></td>
    <td bgcolor="#FFFFFF" class="STYLE1" onclick="fun1(document.getElementById('menus').bh,13,document.getElementById('menus').Qishu)"><%=GetTran("000819", "插入标签")%>3</td>
  </tr>
  <tr style="display:none">
    <td bgcolor="#EFEDDE" class="STYLE1"><img src="images/icon_7.gif" width="14" height="14" /></td>
    <td bgcolor="#FFFFFF" class="STYLE1"><a href="#"><%=GetTran("000819", "插入标签")%>2</a></td>
  </tr>
  <tr style="display:none">
    <td bgcolor="#EFEDDE" class="STYLE1"><img src="images/icon_8.gif" width="14" height="14" /></td>
    <td bgcolor="#FFFFFF" class="STYLE1"><a href="#"><%=GetTran("000819", "插入标签")%>3</a></td>
  </tr>

  <tr style="display:none">
    <td bgcolor="#EFEDDE" class="STYLE1"><img src="images/icon_10.gif" width="14" height="14" /></td>
    <td bgcolor="#FFFFFF" class="STYLE1"><a href="#"><%=GetTran("000819", "插入标签")%>5</a></td>
  </tr>
  
  <tr id="tr6"20>
    <td bgcolor="#EFEDDE" class="STYLE1"><img src="images/icon_7.gif" width="14" height="14" /></td>
    <td bgcolor="#FFFFFF" class="STYLE1"><a href="#" onclick="fun1(document.getElementById('menus').bh,14,document.getElementById('menus').Qishu)">取消标签</a></td>
  </tr>

  <tr style="display:none">
    <td bgcolor="#EFEDDE" class="STYLE1"><img src="images/icon_10.gif" width="14" height="14" /></td>
    <td bgcolor="#FFFFFF" class="STYLE1"><a href="#"><%=GetTran("000819", "插入标签")%>5</a></td>
  </tr>
</table>
<table id="fuDong"  style="border:solid 1px; border-color:#003D5C" border="0" cellspacing="0" cellpadding="0"  class='tree_grid' style="position: absolute; width: 200px; left: 50px;top: 50px; background: #fff;">

    </table>
   <!-- 
<script type="text/javascript">
        //最小索引

        var minIndex = 0;
        //最大索引

        var maxIndex = 0;
        //当前行索引

        var thisIndex = 0;
        //前(后)推算的行数

        var showCount = 5;
        //行对象集合

        var obj = $(".tr");
        //行的td对象集合
        var objTd = {};
        //拼接浮动区域HTML的字符串
        var htmlStr = "";
        //浮动层对象

        var fObj = $("#fuDong");
        //当前行对象

        var mindex=0;
        var thisObj = {};
        $(".tr").live("mouseover", function() {
        obj = $(".tr");
            htmlStr = "";
            thisObj = $(this);
            thisIndex = thisObj.index()
            //给最小索引赋值

            if (thisIndex < showCount) {
                minIndex = 0;
            } else {
                minIndex = thisIndex - showCount;
            }
            //给最大索引赋值

            if (thisIndex + showCount >= obj.length) {
                maxIndex = obj.length - 1;
            } else {
                maxIndex = thisIndex + showCount;
            }
            //最小索引的行

            var minTr = obj.eq(minIndex);
            mindex = minIndex;
            fObj.stop();
            //fObj.css("top", minTr.attr("offsetTop"));
            try
            {
                fObj.animate({ "left": thisObj.children("td").eq(0).children().children(".img").offset().left + 15 + "px","top": minTr.attr("offsetTop")+32}, 100, function() {
                htmlStr += "<tr style='height:18px'><th>代数</th><th>级别</th><th>新个分数</th><th>新网分数</th><th>新网人数</th><th>总网人数</th><th>总网分数</th></tr>";
                    while (minIndex <= maxIndex) {
                        if (thisObj.css("display") != "none") {
                            objTd = obj.eq(minIndex).children("td");
                            htmlStr += "<tr style='height:18px'><td>" + objTd.eq(1).text().replace(/(^\s*)|(\s*$)/g, "") + "</td><td>" + objTd.eq(2).text().replace(/(^\s*)|(\s*$)/g, "") + "</td><td>" + objTd.eq(3).text().replace(/(^\s*)|(\s*$)/g, "") + "</td><td>" + objTd.eq(4).text().replace(/(^\s*)|(\s*$)/g, "") + "</td><td>" + objTd.eq(5).text().replace(/(^\s*)|(\s*$)/g, "") + "</td><td>" + objTd.eq(6).text().replace(/(^\s*)|(\s*$)/g, "") + "</td><td>" + objTd.eq(7).text().replace(/(^\s*)|(\s*$)/g, "") + "</td></tr>";
                        }
                        minIndex++;
                    }
                    fObj.html(htmlStr);
                    addbackgroundcolor(mindex);
                });
            }
            catch(e)
            {
                fObj.animate({ "left": thisObj.children("td").eq(0).children().children().children(".img").offset().left + 15 + "px","top": minTr.attr("offsetTop")+32}, 100, function() {
                htmlStr += "<tr style='height:18px'><th>代数</th><th>级别</th><th>新个分数</th><th>新网分数</th><th>新网人数</th><th>总网人数</th><th>总网分数</th></tr>";
                    while (minIndex <= maxIndex) {
                        if (thisObj.css("display") != "none") {
                            objTd = obj.eq(minIndex).children("td");
                            htmlStr += "<tr style='height:18px'><td>" + objTd.eq(1).text().replace(/(^\s*)|(\s*$)/g, "") + "</td><td>" + objTd.eq(2).text().replace(/(^\s*)|(\s*$)/g, "") + "</td><td>" + objTd.eq(3).text().replace(/(^\s*)|(\s*$)/g, "") + "</td><td>" + objTd.eq(4).text().replace(/(^\s*)|(\s*$)/g, "") + "</td><td>" + objTd.eq(5).text().replace(/(^\s*)|(\s*$)/g, "") + "</td><td>" + objTd.eq(6).text().replace(/(^\s*)|(\s*$)/g, "") + "</td><td>" + objTd.eq(7).text().replace(/(^\s*)|(\s*$)/g, "") + "</td></tr>";
                        }
                        minIndex++;
                    }
                    fObj.html(htmlStr);
                    addbackgroundcolor(mindex);
                });
            }
            document.getElementById('fuDong').style.display='';
        });
        function addbackgroundcolor(mindex){
            if(mindex==0||(mindex%2==0)){
                $("#fuDong tr:nth-child(2n+3)").css("background-color","#F1F4F8");
                $("#fuDong tr:nth-child(2n+2)").css("background-color","#FAFAFA");
            }else{
                $("#fuDong tr:nth-child(2n+3)").css("background-color","#FAFAFA");
                $("#fuDong tr:nth-child(2n+2)").css("background-color","#F1F4F8");
            }
            $("#fuDong tr").eq(thisIndex-mindex).css("background-color","#FFFFCC");
        }
</script>
-->
<!-----------------动态生成菜单结束------------------------>
<script type="text/javascript">
function aaa(bh,qishu)
{
    var bianhao = bh.toString().substring(1,bh.toString().length);
    var str=AjaxClass.WangLuoTuTj(bianhao.toString(),qishu.toString()).value;
    var start0 = document.getElementById('statr0');
    start0.innerHTML = str;
    document.getElementById('fuDong').style.display='none';
    document.getElementById('txtbianhao').value = bianhao;
    
    var divstr=AjaxClass.GetDaoHang(bianhao,qishu.toString(),document.getElementById("txtceng").value,'0').value;
        divDH.innerHTML=divstr;
}
function DataBind()
{
    //debugger;
    var lab1 = document.getElementById('labField1');
    var lab2 = document.getElementById('labField2');
    var lab3 = document.getElementById('labField3');
    var lab4 = document.getElementById('labField4');
    var lab5 = document.getElementById('labField5');
    
    var t1 = document.getElementById('tr1');
    var t2 = document.getElementById('tr2');
    var t3 = document.getElementById('tr3');
    var t4 = document.getElementById('tr4');
    var t5 = document.getElementById('tr5');
    
    var dispalyText = AjaxClass.GetDisplayBind().value;
    var aArray = dispalyText .toString().split(',');
    var count = 0;
    t1.style.display = "none";
    t2.style.display = "none";
    t3.style.display = "none";
    t4.style.display = "none";    
    t5.style.display = "none";
    var i=0;

    for(i=0;i<aArray.length;i++)
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
            default:
                break;
        }
    }
}

function fun1(bh,flag,qishu)
{
   // debugger;
	document.getElementById('menus').style.display="none";
	var bianhao = bh.toString().substring(1,bh.toString().length);
	switch(flag)
	{
	    case 1://变色处理代码放在这

	   //     alert('对编号：'+bianhao+'变色');
	        document.getElementById(bh).style.color="Blue";
	        break;

	    case 2://注册处理代码放在这	
	  //      alert('对编号：'+bianhao+'下级注册');
	        window.location.href="registerMember.aspx?type=0&bh="+bianhao;
	        break;

	    case 3://审核处理代码放在这

	   //     alert('对编号：'+bianhao+'审核');
	        break;

	    case 4://删除处理代码放在这

        //    alert('删除编号：'+bianhao);
            //获取上级编号
            var sjBh = AjaxClass.GetSjBh(bianhao,false).value;

            //删除会员
	        var msg = AjaxClass.DeleteMember(bianhao).value;
	        alert(msg);
	       if(confirm('您确定要删除'+bianhao+'会员吗？'))
            {
	            var msg = AjaxClass.DeleteMember(bianhao).value;
	            alert(msg);
	            if(msg != '<%=GetTran("000008", "删除成功")%>')
	            {	            
	                return;
	            }
	        }

            //刷新网络图

            var qs= AjaxClass.GetMaxQishu().value; 

            var StrDiv=document.getElementById(sjBh);
            var statr0 = document.getElementById("statr0");

            var id = "n"+sjBh;
            tree = document.getElementById(id).tree;
	        var str=AjaxClass.WangLuoTu2(sjBh,tree,qs).value;
	        StrDiv.innerHTML=str;

            if(tree=="")  
            {
               statr0.innerHTML="<div id='"+bianhao+"'>"+StrDiv.innerHTML+"</div>"; 				       
            }
	        break;

	    case 5://调网处理代码放在这

	   //     alert('对编号：'+bianhao+'调网');
	        window.location.href="tw/ChangeTeam2.aspx?bh="+bianhao;
	        break;

	    case 6://显示内容1 处理代码放在这

	        //debugger;
	        var labText = document.getElementById('labField1');
	 //       alert('对编号：'+bianhao+'显示'+labText.innerHTML.toString());

	        var StrDiv = document.getElementById(bianhao);
	        var count = AjaxClass.GetDiv(StrDiv.innerHTML,bianhao).value;
	        if(count>0)
	        {
	            StrDiv = document.getElementById('parent_'+bianhao);
	        }

	        var display = AjaxClass.GetDisplay(bianhao,labText.innerHTML.toString(),qishu).value;
	        StrDiv.innerHTML += "<span class=bs title='"+labText.innerHTML.toString()+"'>["+display+"]</span>";
	        break;

	    case 7://显示内容2 处理代码放在这

	        var labText = document.getElementById('labField2');
	   //     alert('对编号：'+bianhao+'显示'+labText.innerHTML.toString());

	        var StrDiv = document.getElementById(bianhao);
	        var count = AjaxClass.GetDiv(StrDiv.innerHTML,bianhao).value;
	        if(count>0)
	        {
	            StrDiv = document.getElementById('parent_'+bianhao);
	        }

	        var display = AjaxClass.GetDisplay(bianhao,labText.innerHTML.toString(),qishu).value;
	        StrDiv.innerHTML += "<span class=bs title='"+labText.innerHTML.toString()+"'>["+display+"]</span>";
	        break;

	    case 8://显示内容3 处理代码放在这

	        var labText = document.getElementById('labField3');
	    //    alert('对编号：'+bianhao+'显示'+labText.innerHTML.toString());

	        var StrDiv = document.getElementById(bianhao);
	        var count = AjaxClass.GetDiv(StrDiv.innerHTML,bianhao).value;
	        if(count>0)
	        {
	            StrDiv = document.getElementById('parent_'+bianhao);
	        }

	        var display = AjaxClass.GetDisplay(bianhao,labText.innerHTML.toString(),qishu).value;
	        StrDiv.innerHTML += "<span class=bs title='"+labText.innerHTML.toString()+"'>["+display+"]</span>";
	        break;

	    case 9://显示内容4 处理代码放在这

	        var labText = document.getElementById('labField4');
	    //    alert('对编号：'+bianhao+'显示'+labText.innerHTML.toString());

	        var StrDiv = document.getElementById(bianhao);
	        var count = AjaxClass.GetDiv(StrDiv.innerHTML,bianhao).value;
	        if(count>0)
	        {
	            StrDiv = document.getElementById('parent_'+bianhao);
	        }

	        var display = AjaxClass.GetDisplay(bianhao,labText.innerHTML.toString(),qishu).value;
	        StrDiv.innerHTML += "<span class=bs title='"+labText.innerHTML.toString()+"'>["+display+"]</span>";
	        break;

	    case 10://显示内容5 处理代码放在这

	       var labText = document.getElementById('labField5');
	   //     alert('对编号：'+bianhao+'显示'+labText.innerHTML.toString());

	        var StrDiv = document.getElementById(bianhao);
	        var count = AjaxClass.GetDiv(StrDiv.innerHTML,bianhao).value;
	        if(count>0)
	        {
	            StrDiv = document.getElementById('parent_'+bianhao);
	        }

	        var display = AjaxClass.GetDisplay(bianhao,labText.innerHTML.toString(),qishu).value;
	        StrDiv.innerHTML += "<span class=bs title='"+labText.innerHTML.toString()+"'>["+display+"]</span>";
	        break;

	    case 11:
	  //      alert('对编号：'+bianhao+'显示插入图标1');
	        var StrDiv = document.getElementById('n'+bianhao);
	        StrDiv.innerHTML = "<span class=js title ='图标1'><img src='images/icon_6-.gif' width='12' height='12' /></span>" + StrDiv.innerHTML;
	        break;

	    case 12:
	   //     alert('对编号：'+bianhao+'显示插入图标2');
	        var StrDiv = document.getElementById('n'+bianhao);
	        StrDiv.innerHTML = "<span class=js title ='图标1'><img src='images/icon_9-.gif' width='12' height='12' /></span>" + StrDiv.innerHTML;
	        break;

	    case 13:
	   //     alert('对编号：'+bianhao+'显示插入图标3');
	        var StrDiv = document.getElementById('n'+bianhao);
	        StrDiv.innerHTML = "<span class=js title ='图标1'><img src='images/icon_11-.gif' width='12' height='12' /></span>" + StrDiv.innerHTML;
	        break;
	    case 14:
	        var StrDiv = document.getElementById('n'+bianhao);
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
	        var nState = AjaxClass.GetNumberState(bianhao,qishu.toString()).value;
	        alert(nState);
	        if(nState=="1")
	        {
	            document.getElementById(bh).style.color="black";
	        }
	        else if(nState=="4")
	        {
	            document.getElementById(bh).style.color="Silver";
	        }
	        else if(nState=="2")
	        {
	            document.getElementById(bh).style.color="#FF8686";
	        } 
	        else if(nState=="3")
	        {
	            document.getElementById(bh).style.color="#E30000";
	        }	        
	        break;
	    default:
	        alert('"+<%=GetTran("000843", "选择错误")%>+"！');
	        break;
	}
}

function JSNET(thisObj,bianhao,tree,flag,qishu,cw)
{
    var StrDiv=document.getElementById(bianhao);
    var statr0 = document.getElementById("statr0");
    var bh = bianhao.substring(5,bianhao.length);
    if(flag==0)
    {
        //下面代码根据实际修改.replace(/ /g,"<img src='../images/03.gif' \>")
        document.getElementById(bianhao).firstChild.src = "../images/014.gif";
        var spanId = AjaxClass.getXHFW(bh,false,qishu).value;
        var sId = spanId.split(",");
        for(var i=0;i<sId.length;i++)
        {
            if(document.getElementById("tr"+sId[i].toString())!=null && sId[i].toString()!=bh)
            {
                document.getElementById("tr"+sId[i].toString()).style.display = "none";                
            }
        }
        debugger;
        document.getElementById("span_"+bh).onclick=function()
        {
             JSNET(document.getElementById("span_"+bh),document.getElementById("span_"+bh).id,document.getElementById("span_"+bh).attributes['tree'].value,1,qishu,cw) ;
        };
    }
    else
    {
        var cengshu = '<%=GetCengshu() %>';
        var cengs = cw ;//+ 1;
        var loginBh = '<%=GetLoginMember() %>'
        var bhCw = AjaxClass.GetLogoutCw(bh,qishu,false).value;
        var loginCw = AjaxClass.GetLogoutCw(loginBh,qishu,false).value;
        var showCs = AjaxClass.GetShowCengS(0).value;
        
        if(showCs- (bhCw -loginCw)<cengshu)
        {
            cengshu = showCs- (bhCw -loginCw);
        }
        
        if(cengshu==0)
        {
            return;
        }
        var htm = AjaxClass.GetHtml(bh,tree,qishu,cengshu,false,cengs.toString()).value;

        $("#tr"+bh).after(htm);

        document.getElementById(bianhao).firstChild.src = "../images/013.gif";

        document.getElementById("span_"+bh).onclick=function()
        {
             JSNET(document.getElementById("span_"+bh),document.getElementById("span_"+bh).id,document.getElementById("span_"+bh).attributes['tree'].value,0,qishu,cw) ;
        };
    }

       var jo = "j";
        $("#tab_tr tr").each(function() {
                var thisObj = $(this);
                if (thisObj.css("display") != "none") 
                {
                    if (jo == "j") {
                        thisObj.css("background-color", "#F1F4F8");
                        jo = "0";
                    } else {
                        thisObj.css("background-color", "#FAFAFA");
                        jo = "j";
                    }
                }
            })
}

<!-----------------生成快捷菜单脚本------------------------->
function popUp(id,Qishu,event) {
     if (event.button == 2) 
    {
        menu = document.getElementById('menus');//document.all.menus]
        newX = event.clientX + document.documentElement.scrollLeft
        newY = event.clientY + document.documentElement.scrollTop
        menu.style.display = "block"
        menu.style.left = newX + "px";
        menu.style.top = newY + "px";
        if(newY+menu.offsetHeight > ViewportInfo().height+ScrollPosition().top){
                        if(newY - menu.offsetHeight<ScrollPosition().top){
                            menu.style.top=ScrollPosition().top;
                        }else{
                            menu.style.top = newY - menu.offsetHeight+"px";
                        }
                    }
    //    menu.style.pixelLeft = newX + "px"
    //    menu.style.pixelTop = newY + "px"
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

    document.documentElement.onclick=function()      
    {      
      //    debugger;
      //  if(WebCalendar.eventSrc != window.event.srcElement)
             document.getElementById('menus').style.display="none"; 
             document.getElementById('fuDong').style.display='none';
    }    

</script>

<!------------------下面代码防止右键调出IE快捷菜单---------------------------->
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
<script type="text/javascript">
var thColor='';
function overIt(th)
{	
    thColor = th.style.backgroundColor;
    th.style.backgroundColor='#FFFFCC';	
}
function outIt(th)
{	
    th.style.backgroundColor=thColor;	
}
</script>
	</head>
	<body >
	<br />
		<form id="form2" method="post" runat="server">
			<table id="table2" cellspacing="1" cellpadding="0" width="100%" border="0">
				<tbody>
					<tr>
						<td>
							<table  cellSpacing="1" cellPadding="0" width="100%" border="0" class="tablemb">
								<tbody>
									<tr class="t_head">
										<td style="height: 25px" colspan="2">&nbsp;
										    <asp:button  id="button1"  runat="server" CssClass="anyes"  text="显示" 
                                                onclick="button1_Click"></asp:button>&nbsp;&nbsp;
											<asp:dropdownlist id="dropdownlist_qishu" runat="server"></asp:dropdownlist>
											<%=GetTran("000024", "会员编号")%><asp:textbox id="txtbianhao" runat="server" Width="85px" MaxLength="10"></asp:textbox>
											<asp:textbox id="txtceng" runat="server" width="24px"></asp:textbox><%=GetTran("000845", "层")%>
                                            &nbsp;
											<asp:button id="button2"  runat="server" CssClass="anyes" Visible="false" text="回到顶部" 
                                                onclick="button2_Click"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<a href="javascript:window.history.back()" style="display:none"><u><%=GetTran("000421", "返回")%><%=GetTran("000846", "以下图中深红色代表已审核新增人员，淡红色代表未审核新增人员")%></u></a>

											<asp:Button ID="btnCY" runat="server" Text="常用" CssClass="anyes" 
                                                onclick="btnCY_Click" />
									        <asp:Button ID="Button3" runat="server" Text="表格" CssClass="anyes" 
                                                onclick="Button3_Click" />
									        <asp:Button ID="Button4" runat="server" Text="展开" CssClass="anyes" 
                                                onclick="Button4_Click"  />
                                            <asp:Button ID="Button5" runat="server" Text="伸缩" Enabled="false" CssClass="anyes" />			&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="Button6" runat="server" Text="转到安置" CssClass="anyes" 
                                                          onclick="Button6_Click" />
                                        </td>
									</tr>
								</tbody>
							</table>
							<table  cellSpacing="0"  cellPadding="0" width="100%" >
							<tr>
							        <td>
							            <div id="divDH" runat="server" class="STYLE1"></div>
							        </td>
							    </tr>
								<tr>
									<td class="ls2" style="font-size: 12pt" nowrap>
										<div id="statr0" style="overflow: auto; width:100%;  text-indent: 11mm; line-height: 12pt; letter-spacing: 0em;"
											runat="server"></div>
										
										<br />
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</tbody>
			</table>
		</form>
	</body>
</html>
