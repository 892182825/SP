function moveup(item, init){
	//window.status = "move " + item;
	box=document.getElementById("Box"+item);
	boxHeight=this_hight[item-1];
	txt=document.getElementById("Txt"+item);	
	tempinit = init
	height=parseInt(box.style.height);
	if (tempinit)
	{
		height = 0;
		box.style.height = height;
		box.style.top = bottomLinePosition;
		txt.style.top = bottomLinePosition + 50;		
		tempinit = false;
	}

	if(!tempinit && height < boxHeight){
		box.style.height = height + 100;
		box.style.top = bottomLinePosition - height - 100;
		txt.style.top = bottomLinePosition - height - 100 - 520;
		Stop=setTimeout("moveup(" + item + ",tempinit)",10);
	}
	
	else{
		if (this_hight.length > item)
			moveup(item+1, true);
	} 
}

function Init()
{
	try
	{
		if (this_hight.length>10)
			ShowDirectly();
		else
		{
		
		 // for(var i=0;i<this_hight.length;i++)
		  // {
		//	   moveup(i+1, true);
		 //  }
		   moveup(1,true);
		}
	}
	catch(e)
	{
		ShowDirectly();
	}
}

function ShowDirectly()
{
	for (var i=0; i<this_hight.length; i++)
	{
		var box = document.getElementById("Box"+(i+1));
		var txt=document.getElementById("Txt"+(i+1));
		box.style.height = this_hight[i];
		box.style.top = bottomLinePosition - this_hight[i];		
		txt.style.top = bottomLinePosition - this_hight[i] - 520;
	}
}