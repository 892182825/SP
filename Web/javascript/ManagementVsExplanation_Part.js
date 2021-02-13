  /*
  *创建者：汪华
  *创建时间：2009-09-28  
  *作用：主要用于管理和说明
  */
  
    //图品路径不同，所以有两种方法
    function down3()
	{
		if(document.getElementById("divTab2").style.display=="none")
		{
			document.getElementById("divTab2").style.display="";
			document.getElementById("imgX").src="../images/dis1.GIF";
			
		}
		else
		{
			document.getElementById("divTab2").style.display="none";
			document.getElementById("imgX").src="../images/dis.GIF";
		}		
	}	
	
	function down2()
	{
	    if(document.getElementById("divTab2").style.display=="none")
	    {
		    document.getElementById("divTab2").style.display="";
		    document.getElementById("imgX").src="images/dis1.GIF";
			
	    }
	    else
	    {
		    document.getElementById("divTab2").style.display="none";
		    document.getElementById("imgX").src="images/dis.GIF";
	    }	    
	}
	  
	 function secBoard(n)
     {
        var tdarr=document.getElementById("secTable").getElementsByTagName("td");
        
        for(var i=0;i<tdarr.length;i++)
        {
            tdarr[i].className="sec1";
        }              
   
         tdarr[n].className="sec2";     
        
        var tbody0=document.getElementById("tbody0");
        if(tbody0!=null)
        {
            tbody0.style.display="none";
        }
        
        var tbody1=document.getElementById("tbody1");
        if(tbody1!=null)
        {
            tbody1.style.display="none";        
        }        
        
        document.getElementById("tbody"+n).style.display="block";        
      }     
      

   
