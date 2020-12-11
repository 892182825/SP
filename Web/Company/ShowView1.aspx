<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowView1.aspx.cs" Inherits="Company_ShowView1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>QueryAnZhiNetworkView1</title>
        <script src="../JS/jquery.js" type="text/javascript"></script>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="C#" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		<link rel="Stylesheet" href="CSS/Company.css" type="text/css" />
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
.box{cursor:pointer; font: 8pt "宋体"; position: absolute; background: LightGrey;color:blue;z-index:101 }
</style>
<style type="text/css">

.STYLE1 {	font-size: 12px;	
	color: #333;
}
.STYLE1 a 
{
	
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
	line-height: 1px;
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
</style>
<script type="text/javascript" src="../JS/TW.js"></script>
<script type="text/javascript">
    var istw=false;
    var twbianhao;
    
    function settw(e)
    {
        if(confirm("你确定 "+twbianhao+" 要调网吗？"))
        {
            istw=true;
            document.getElementById("tr21").style.display="none";
            document.getElementById("tr22").style.display="";
            
            document.getElementById("yd_id").innerHTML=twbianhao;
            
            document.getElementById("yd_id").style.display="";
        }
        
        document.getElementById("menus").style.display="none";
    }
    
    function untw()
    {
        istw=false;
        isFirst=true;
        document.getElementById("tr21").style.display="";
        document.getElementById("tr22").style.display="none";
        
        document.getElementById("yd_id").style.display="none";
    }
    
    function down_tw(e,th)
    {
        if(e.button==1 || e.button==0)
        {
            if(istw)
            {
                //alert(th.id.substring(2,th.id.length));
                
                //window.status=th.id.substring(2,th.id.length);
                
                var id=th.id.substring(2,th.id.length);
                var btid=document.getElementById("yd_id").innerHTML;
                    
                if(confirm("你确定要把编号 "+btid+" 及他的团队调到编号 "+id+" 下面吗？"))
                {
                    setAjaxTW("LayerBit1","Ordinal1",btid,id,'<%=ViewState["qs"]%>','<%=ViewState["cengshu"]%>','<%=ViewState["startbh"]%>','<%=ViewState["isanzhi"]%>');
                }
                /*else
                    document.getElementById("zz_id").style.display="none";*/
            }
        }
    }
    
    function aaa(bh,qishu)
    {
        debugger;
        var bianhao = bh.toString().substring(1,bh.toString().length);
        var str=AjaxClass.WangLuoTuNew(bianhao.toString(),qishu.toString()).value;
        var start0 = document.getElementById('statr0');
        start0.innerHTML = str;
        document.getElementById('fuDong').style.display='none';
        document.getElementById('txtbianhao').value = bianhao;
        
        var divstr=AjaxClass.GetDaoHang(bianhao,qishu.toString(),document.getElementById("txtceng").value,'1').value;
        divDH.innerHTML=divstr;
    }
</script>

<!--下面代码采取动态生成，读取表内容(需要建一个表保存管理员设置的菜单项)-->
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
    <td bgcolor="#FFFFFF" class="STYLE1" onclick="fun1(document.getElementById('menus').bh,2,document.getElementById('menus').Qishu)"><%=GetTran("000809", "注册会员")%></td>
  </tr>
  <tr style="display:none"  id="tr12">
    <td bgcolor="#EFEDDE" class="STYLE1"><img src="images/icon_5.gif" width="14" height="14" /></td>
    <td bgcolor="#FFFFFF" class="STYLE1"  onclick="fun1(document.getElementById('menus').bh,3,document.getElementById('menus').Qishu)"><%=GetTran("000812", "审核会员")%></td>
  </tr>
  <tr  id="tr13">
    <td bgcolor="#EFEDDE" class="STYLE1"><img src="images/icon_5.gif" width="14" height="14" /></td>
    <td bgcolor="#FFFFFF" class="STYLE1"  onclick="fun1(document.getElementById('menus').bh,4,document.getElementById('menus').Qishu)"><%=GetTran("000815", "删除会员")%></td>
  </tr>
  <tr  id="tr23">
    <td bgcolor="#EFEDDE" class="STYLE1"><img src="images/icon_8.gif" width="14" height="14" /></td>
    <td bgcolor="#FFFFFF" class="STYLE1"  onclick="fun1(document.getElementById('menus').bh,16,document.getElementById('menus').Qishu)"><%=GetTran("001282", "注销会员")%></td>
  </tr>
  <tr id="tr14">
    <td bgcolor="#EFEDDE" class="STYLE1"><img src="images/icon_4.gif" width="14" height="14" /></td>
    <td bgcolor="#FFFFFF" class="STYLE1"  onclick="fun1(document.getElementById('menus').bh,5,document.getElementById('menus').Qishu)"><%=GetTran("000772", "会员调网")%></td>
  </tr>
  <tr id="tr6">
    <td bgcolor="#EFEDDE" class="STYLE1"><img src="images/icon_4.gif" width="14" height="14" /></td>
    <td bgcolor="#FFFFFF" class="STYLE1"  onclick="fun1(document.getElementById('menus').bh,17,document.getElementById('menus').Qishu)"><%=GetTran("001209", "修改信息")%></td>
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
  
  <tr id="tr21">
    <td bgcolor="#EFEDDE" class="STYLE1"><img src="images/icon_7.gif" width="14" height="14" /></td>
    <td bgcolor="#FFFFFF" class="STYLE1" onmousedown="settw(event)">调网</td>
  </tr>
  <tr id="tr22" style="display:none; ">
    <td bgcolor="#EFEDDE" class="STYLE1"><img src="images/icon_7.gif" width="14" height="14" /></td>
    <td bgcolor="#FFFFFF" class="STYLE1" onclick="untw()">取消调网</td>
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
                fObj.animate({ "left": thisObj.children("td").eq(0).children().children(".img").offset().left + 20 + "px","top": minTr.attr("offsetTop")+79}, 100, function() {
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
                fObj.animate({ "left": thisObj.children("td").eq(0).children().children().children(".img").offset().left + 20 + "px","top": minTr.attr("offsetTop")+79}, 100, function() {
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
    </script>-->
<!-----------------动态生成菜单结束------------------------>
<script type="text/javascript">
function DataBind()
{
    var topBh = document.getElementById('txtbianhao').value;
    var manageId = '<%=GetManageId() %>';
    
    var isTop = AjaxClass.GetIsTop(topBh.toString(),0,manageId.toString()).value;
    
    if(isTop=="1")
    {
         document.getElementById('top'+topBh).style.color='red';
    }

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

	        document.getElementById(bh).style.color="Blue";
	        break;

	    case 2://注册处理代码放在这	
	        window.location.href="../Store/RegisterMember.aspx?type=1&bh="+bianhao;
	        break;

	    case 3://审核处理代码放在这

	        break;

	    case 4://删除处理代码放在这

            var sjBh = AjaxClass.GetSjBh(bianhao,true).value;

            //删除会员
            if(confirm('您确定要删除'+bianhao+'会员吗？'))
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
//            var qs= AjaxClass.GetMaxQishu().value; 

//            var StrDiv=document.getElementById(sjBh);
//            var statr0 = document.getElementById("statr0");

//            var id = "n"+sjBh;
//            tree = document.getElementById(id).tree;
//	        var str=AjaxClass.WangLuoTu2(sjBh,tree,qs).value;
//	        StrDiv.innerHTML=str;

//            if(tree=="")  
//            {
//               statr0.innerHTML="<div id='"+bianhao+"'>"+StrDiv.innerHTML+"</div>"; 				       
//            }
           // alert('2222')
            getDataII('<%=ViewState["startbh"]%>','<%=ViewState["cengshu"]%>','<%=ViewState["qs"]%>','<%=ViewState["isanzhi"]%>','sxaz');
          //  alert('333')

	        break;

	    case 5://调网处理代码放在这

	        window.location.href="tw/ChangeTeam2.aspx?bh="+bianhao;
	        break;

	    case 6://显示内容1 处理代码放在这

	        var labText = document.getElementById('labField1');
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
	        var StrDiv = document.getElementById('n'+bianhao);
	        StrDiv.innerHTML = "<span class=js title ='图标1'><img src='images/icon_6-.gif' width='12' height='12' /></span>" + StrDiv.innerHTML;
	        break;
	    case 12:
	        var StrDiv = document.getElementById('n'+bianhao);
	        StrDiv.innerHTML = "<span class=js title ='图标1'><img src='images/icon_9-.gif' width='12' height='12' /></span>" + StrDiv.innerHTML;
	        break;
	    case 13:
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
	       // alert(nState);
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
       case 16:
            if(confirm('确定注销'+bianhao+'会员吗？'))
            {
                var manager = '<%=Session["Company"].ToString() %>';
                //alert(manager);
                var content = AjaxClass.GetMemberOff(bianhao,manager).value;
	            // window.location.href="MemberOff.aspx?bh="+bianhao;
	            alert(content);
	        }
	        break;
	     case 17:
                window.location.href="MemberInfoModify.aspx?id="+bianhao;
	       
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
        var spanId = AjaxClass.getXHFW(bh,true,qishu).value;

        var sId = spanId.split(",");
        for(var i=0;i<sId.length;i++)
        {
            if(document.getElementById("tr"+sId[i].toString())!=null && sId[i].toString()!=bh)
            {
                document.getElementById("tr"+sId[i].toString()).style.display = "none";                
            }
        }

        document.getElementById("span_"+bh).onclick=function()
        {
             JSNET(document.getElementById("span_"+bh),document.getElementById("span_"+bh).id,document.getElementById("span_"+bh).attributes['tree'].value,1,qishu,cw) ;
        };
    }
    else
    {
        var cengshu = '<%=GetCengshu() %>';
        var cengs = cw ;//+ 1;
        var htm = AjaxClass.GetHtml(bh,tree,qishu,cengshu,true,cengs.toString()).value;

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

//<!-----------------生成快捷菜单脚本------------------------->
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
            menu.style.top = newY - menu.offsetHeight+"px";
        }

        menu.bh=id
        menu.Qishu=Qishu
        
        if(document.getElementById(id).style.color=="blue")
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
             document.getElementById('menus').style.display="none"; 
             document.getElementById('fuDong').style.display='none';
    }

</script>

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
		    function ShowView(abc)
		    {
		        document.getElementById("txtbianhao").value = abc.firstChild.nodeValue;
		        
		        //document.getElementById("lkn_submit1").click();
		        __doPostBack('lkn_submit1','');
		        
		    }
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
	<body>
		<form id="form22" method="post" runat="server">
		<br />
			<table id="table2" cellspacing="1" cellpadding="0" width="100%" border="0">
				<tbody>
					<tr>
						<td>
							<table  cellSpacing="1" cellPadding="0" width="100%" border="0" class="tablemb">
								<tbody>
									<tr >
										<td style="height: 25px" colspan="2">
										    <asp:button  id="button1"  runat="server" CssClass="anyes"  text="显示" 
                                                onclick="button1_Click"></asp:button>&nbsp;&nbsp;&nbsp;
											<asp:label id="lbl_msg" runat="server"><%=GetTran("000045", "期数")%></asp:label><asp:dropdownlist id="dropdownlist_qishu" runat="server"></asp:dropdownlist>
											<%=GetTran("000024", "会员编号")%><asp:textbox id="txtbianhao" Width="85" MaxLength="10" runat="server"></asp:textbox>
											<asp:textbox id="txtceng" runat="server" width="24px" ></asp:textbox><%=GetTran("000845", "层")%>
											
                                            <asp:LinkButton ID="lkn_submit1" runat="server" style="display:none" Text="提交"
                                        onclick="lkn_submit1_Click"></asp:LinkButton>
                                        
											<asp:button id="button2" Visible="false"  runat="server" CssClass="anyes"  text="回到顶部" 
                                                onclick="button2_Click"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
											<a style="display:none" href="javascript:window.history.back()"><u><%=GetTran("000421", "返回")%><%=GetTran("000846", "以下图中深红色代表已审核新增人员，淡红色代表未审核新增人员")%></u></a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                
											    <asp:Button ID="btnCY" runat="server" Text="常用" CssClass="anyes" 
                                                    onclick="btnCY_Click" />
                                
									           <asp:Button ID="Button3" runat="server" Text="表格" CssClass="anyes" 
                                                    onclick="Button3_Click" />
                                                
                                                 <asp:Button ID="Button4" runat="server" Text="展开" CssClass="anyes" 
                                                onclick="Button4_Click"  />
                                                
                                                <asp:Button ID="Button8" runat="server" Text="伸缩" Enabled="false" 
                                                CssClass="anyes" onclick="Button8_Click" />
                                                <br />
										</td>
									</tr>
								</tbody>
							</table>
							<table  cellSpacing="0"  cellPadding="0"  width="100%" >
							    <tr>
							        <td>
							            <table  class="tablema">
							                <tr>
							                     <td>
                                                    <asp:Button ID="Button7" runat="server" Text="转到推荐" CssClass="anyes" 
                                                                    onclick="Button7_Click" /></td>
							                    <td> 可查看网络<asp:Repeater ID="Repeater2" Runat="server">
									                    <ItemTemplate>
										                    <b><%#DataBinder.Eval(Container.DataItem, "wlt")%></b>
									                    </ItemTemplate>
								                    </asp:Repeater></td>
							                </tr>
							            </table>
							       </td>
							    </tr>
							    <tr>
							        <td>
							            <div id="divDH" runat="server" class="STYLE1"></div>
							        </td>
							    </tr>
							    <tr>
									<td  nowrap>
									     <br />
										<div id="statr0" style="overflow: auto; width:100%;  text-indent: 11mm;  letter-spacing: 0em;"
											runat="server"></div>
										<br>
									</td>
								 </tr>
							</table>
						</td>
					</tr>
				</tbody>
			</table>
			
			
			<div id="aabb" align="center">
			</div>
		</form>
	</body>
</html>


