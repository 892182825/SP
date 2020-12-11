$(document).ready(function(){
			if($.browser.msie && $.browser.version == 6) {
				FollowDiv.follow();
			}
	 });
	 FollowDiv = {
			follow : function(){
				$('#cssrain').css('position','absolute');
				$(window).scroll(function(){
				    var f_top = $(window).scrollTop() + $(window).height() - $("#cssrain").height() - parseFloat($("#cssrain").css("borderTopWidth")) - parseFloat($("#cssrain").css("borderBottomWidth"));
					$('#cssrain').css( 'top' , f_top );
				});
			}
	  }
	  
	  
	  function down2()
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
	
	function secBoard(n)
  
  {
  //  document.Form1.sa.value=n;
  //  document.getElementById("s").click();
       for(i=0;i<secTable.cells.length;i++)
      secTable.cells[i].className="sec1";
    secTable.cells[n].className="sec2";
    for(i=0;i<mainTable.tBodies.length;i++)
      mainTable.tBodies[i].style.display="none";
    mainTable.tBodies[n].style.display="block";
  }
  
   function cut()
        {
            document.getElementById("span1").title=aa[0];
        }
        
        function bSubmit_onclick() {

}

function GetName1(number)
{
   var name1=AjaxClass.GetMumberName(number).value;
   if(name1!=null)
   {
    document.getElementById("Label1").innerText=name1;
    }
    else
    {
        document.getElementById("Label1").innerText="";
    }
    
}

function GetName2(number)
{
     var name1=AjaxClass.GetMumberName(number).value;
     if(name1!=null)
   {
    document.getElementById("Label3").innerText=name1;
    }
    else
    {
        document.getElementById("Label3").innerText="";
    }
}