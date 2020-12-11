onit=true;
num=0;
function moveup(iteam,top,txt,rec){
	temp=eval(iteam);
	tempat=eval(top);
	temptxt=eval(txt);
	temprec=eval(rec);
	at=parseInt(temp.style.top);
	temprec.style.display = ""; 

	if (num>27){
		temptxt.style.display = "";
	}

	if(at>(tempat-28)&&onit){
		num++;
		temp.style.top=at-1;
		Stop=setTimeout("moveup(temp,tempat,temptxt,temprec)",10);
	}else{
		return
	} 
}
function movedown(iteam,top,txt,rec){
	temp=eval(iteam);
	temptxt=eval(txt);
	temprec=eval(rec);
	clearTimeout(Stop);
	temp.style.top=top;
	num=0;
	temptxt.style.display = "none";
	temprec.style.display = "none";
}
function ontxt(iteam,top,txt,rec){
	temp = eval(iteam);
	temptxt = eval(txt);
	temprec = eval(rec);
	if (onit){
		temp.style.top = top-28;
		temptxt.style.display = "";
		temprec.style.display = "";
	}
}
function movereset(over){
	if (over==1){
		onit=false;
	}else{
		onit=true;
	}
}

function Init()
{
}

function ShowDirectly()
{
	for (var i=0; i<this_rotate.length; i++)
	{
		var cake = document.getElementById("Cake"+(i+1));
		cake.style.rotation = this_rotate[i];
		cake.adj = this_adj[i] + ",0";
	}
}