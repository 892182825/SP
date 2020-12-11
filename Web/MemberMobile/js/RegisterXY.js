function addZZ()
{
    var pdiv=document.createElement("div");
	document.body.appendChild(pdiv);
	
	pdiv.innerHTML="<div id='zzdiv3' style='position:fixed;left:0px;top:0px;width:100%;height:800px;background-color:black;filter:alpha(opacity=60);opacity:0.6;display:none;'></div>"
												   + "<div id='xydiv3' style='z-index: 1000;position:fixed;left:0px;top:0px;width:100%;height:90%;border:gray solid 1px;background-color:white;display:none;' align='center'>"
														
														+"<iframe width='100%' height='92%' src='../Agreement.aspx'></iframe>"
														+"<br><br><div onclick='hidenXY()' style='margin-bottom:10px;cursor:pointer;'>关闭</div>"
												   +"</div>";

}
function showXY()
{
	document.getElementById("zzdiv3").style.display="";
	document.getElementById("xydiv3").style.display="";
}

function hidenXY()
{
	document.getElementById("zzdiv3").style.display="none";
	document.getElementById("xydiv3").style.display="none";

	
}

addZZ();