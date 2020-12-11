var isTW=false;
var xmlHttp;
var isDown=false;
var isFirst=true;
var divx=0;
var divy=0;


if(navigator.userAgent.toLowerCase().indexOf("ie")!=-1)	
    window.attachEvent("onload",myLoad);
else
    window.addEventListener("load",myLoad,false);

function myLoad()
{
    /*document.body.innerHTML=document.body.innerHTML+"<div id=\"yd_id\" style=\"position:absolute;left:0px;top:0px;width:70px;height:15px;display:none;color:red;background-color:cyan;z-index:80;filter:alpha(opacity=80);opacity:0.8\"></div>"
													+"<div id=\"zz_id\" style=\"position:absolute;left:0px;top:0px;width:100%;height:100%;display:none;background-color:gray;z-index:70;filter:alpha(opacity=50);opacity:0.5\"></div>"
													+"<div align=\"center\" id=\"load_div\" style=\"position:absolute;left:0px;top:0px;width:100%;z-index:71;display:none;font-size:10pt\"><br><br><br><br><br><br><br><br><br><br><br><br><br><img src=\"../Images/ajax-loader.gif\"><br>调网中...</div>";*/
    
    document.getElementById("aabb").innerHTML="<div id=\"yd_id\" style=\"position:absolute;left:0px;top:0px;width:70px;height:15px;display:none;color:red;background-color:cyan;z-index:180;filter:alpha(opacity=80);opacity:0.8\"></div>"
													+"<div id=\"zz_id\" style=\"position:absolute;left:0px;top:0px;width:100%;height:100%;display:none;background-color:gray;z-index:70;filter:alpha(opacity=50);opacity:0.5\"></div>"
													+"<div align=\"center\" id=\"load_div\" style=\"position:absolute;left:0px;top:0px;width:100%;z-index:71;display:none;font-size:10pt\"><br><br><br><br><br><br><br><br><br><br><br><br><br><img src=\"../Images/ajax-loader.gif\"><br>调网中...</div>";

    
    document.onmousemove=mouseMove;
};

function mouseMove(e)
{
	if(istw)
	{
	    if(e==null)
			e=window.event;
		
		var x=e.clientX;
		var y=e.clientY;
		
		document.getElementById("yd_id").style.left=x+document.documentElement.scrollLeft+20+"px";
		document.getElementById("yd_id").style.top=y+document.documentElement.scrollTop+"px";
	}
}

var xmlHttp;
var _qs;
var _isanzhi;
var _cs;
var _jgbh;

function createXMLHttpRequest()
{
    if(window.ActiveXObject)
        xmlHttp=new ActiveXObject("Microsoft.XMLHTTP");
    else if(window.XMLHttpRequest)
        xmlHttp=new XMLHttpRequest();
}

function setAjaxTW(cw,xh,btwbianhao,tdbianhao,qs,cs,jgbh,isanzhi)
{
    document.getElementById("load_div").style.display="";
    
    _qs=qs;
    _isanzhi=isanzhi;
    _cs=cs;
    _jgbh=jgbh;
    
    if(xmlHttp==null)
        createXMLHttpRequest();
    
    xmlHttp.open("get","../TWAjax.aspx?cw="+cw+"&xh="+xh+"&btbianhao="+btwbianhao+"&tdbianhao="+tdbianhao+"&qs="+qs+"&mode=tw&date="+new Date().getTime(),true);
    xmlHttp.onreadystatechange=stateChange;
    xmlHttp.send(null);
    
}

function stateChange()
{
    if(xmlHttp.readyState==4)
    {
        if(xmlHttp.status==200)
        {
            var rt=xmlHttp.responseText;
            
            if(rt=="1")
                alert("不能调在自己的团队下");
            else if(rt=="2")
                alert("当期会员不需调网，请到报单浏览处修改");
            else if(rt=="3")
                alert("此编号下已经有三个人");
            else
            {   
                //调网成功，重新加载数据
                if(_isanzhi=="1")//安置
                    getData("sxaz");
                else if(_isanzhi=="0")
                    getData("sxtj");

                alert(rt);
                
                untw();
            } 
            document.getElementById("zz_id").style.display="none";
            document.getElementById("load_div").style.display="none";
            
        }
    }
}

//刷新数据
function getData(mode)
{
    if(xmlHttp==null)
        createXMLHttpRequest();
    
    xmlHttp.open("get","../TWAjax.aspx?startbh="+_jgbh+"&cs="+_cs+"&qs="+_qs+"&isanzhi="+_isanzhi+"&mode="+mode+"&date="+new Date().getTime(),true);
    xmlHttp.onreadystatechange=function()
    {
        if(xmlHttp.readyState==4)
        {
            if(xmlHttp.status==200)
            {
                document.getElementById("statr0").innerHTML=xmlHttp.responseText;
            }
        }
    };
    xmlHttp.send(null);
   
}


//用于删除刷新
function getDataII(sxjgbh,sxcs,sxqs,sxisanzhi,mode)
{
    if(xmlHttp==null)
        createXMLHttpRequest();

    xmlHttp.open("get","../TWAjax.aspx?startbh="+sxjgbh+"&cs="+sxcs+"&qs="+sxqs+"&isanzhi="+sxisanzhi+"&mode="+mode+"&date="+new Date().getTime(),true);
    xmlHttp.onreadystatechange=function()
    {
        if(xmlHttp.readyState==4)
        {
            if(xmlHttp.status==200)
            {
                document.getElementById("statr0").innerHTML=xmlHttp.responseText;
            }
        }
    };
    xmlHttp.send(null);
   
}
