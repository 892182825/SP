﻿function down2()
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
  //  document.Form1.sa.value=n;
  //  document.getElementById("s").click();
//       for(i=0;i<secTable.cells.length;i++)
//      secTable.cells[i].className="sec1";
//    secTable.cells[n].className="sec2";
//    for(i=0;i<mainTable.tBodies.length;i++)
//      mainTable.tBodies[i].style.display="none";
//    mainTable.tBodies[n].style.display="block";
      var tdarr=document.getElementById("secTable").getElementsByTagName("td");
        
        for(var i=0;i<tdarr.length;i++)
        {
            tdarr[i].className="sec1";
        }
        tdarr[n].className="sec2";
        
        var tbody0=document.getElementById("tbody0");
        tbody0.style.display="none";
        var tbody1=document.getElementById("tbody1");
        tbody1.style.display="none";
        
        
        document.getElementById("tbody"+n).style.display="block";
  }
  
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
	  
	  function cut()
        {
             document.getElementById("span1").title=aa[0];
        }
        function cut1()
        {
             document.getElementById("span2").title=aa[1];
        }
        
        function aaa()
    {
   // var str="'";
   // var str1="=";
        for(var i=0;i<form1.elements.length;i++)
        {
            if(form1.elements[i].type=="text")
            {
                if(form1.elements[i].value.indexOf("'")!=-1||form1.elements[i].value.indexOf("=")!=-1)
                {
                    alert(aa[2]);
                    return false;
                }
            }
        }
    }
    	function CheckText(btname)
	{
		//这个方法是页面有多个按钮要提交时，多次使用这个方法,传入按钮的ID
		filterSql_II (btname);
		
	}
	
	