          function card()
		  {
		    debugger;
		   // var divSex=document.getElementById("divSex");
		   var tr1 = document.getElementById("tr1");
		   var tr2 = document.getElementById("tr2");
//		    var birth=document.getElementById("trBirth");
		    var card=document.getElementById("dplCardType").options[document.getElementById("dplCardType").selectedIndex].value;
		    var tr3=document.getElementById("tr3");
		    if(card=='2')
		    {
		   
		      //divSex.style.display="none";
		      tr1.style.display="none";
		      tr2.style.display="none";
//		      birth.style.display="none";
		      tr3.style.display="";
		    }
		    else if(card!='1'&&card!='1')
		    {
		      tr1.style.display="";
		      tr2.style.display="";
//		      birth.style.display="block";
		      tr3.style.display="";
		    }
		    else
		    {
		      tr1.style.display="";
		      tr2.style.display="";
//		      birth.style.display="block";
		      tr3.style.display="none";
		    }
		  }
		  
		  
		  function elcCardConsume()
		  {
		     var trNumber=document.getElementById("DD1");
		     var trPass=document.getElementById("DD2");
		     var elcId=document.getElementById("Ddzf").value;
		     if(elcId==1)
		     {
		        trNumber.style.display="none";
		        trPass.style.display="none";
		     }
		     else if(elcId==2)
		     {
		        trNumber.style.display="";
		        trPass.style.display="";
		     }
		     else
		     {
		        trNumber.style.display="none";
		        trPass.style.display="none";
		     }
		  }