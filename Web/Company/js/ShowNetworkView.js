
    function DataBind()
    {
        var topBh = document.getElementById('txtBh').value;
        var manageId = '<%=GetManageId() %>';
        var isAnzhi = '<%=isAnzhi2() %>';
        
        var isTop = AjaxClass.GetIsTop(topBh.toString(),isAnzhi,manageId.toString()).value;
        
        if(isTop=="1")
        {
             document.getElementById('top'+topBh).style.color='red';
        }

        
        
    }
    
		    function GetBind(number)
		    {
		        var asg = AjaxClass.GetViewBind(number).values;
		        document.getElementById("divDh").innerHTML = document.getElementById("divDh").innerHTML +" \ "+ asg;
		    }
		    
		    function ShowView(abc)
		    {
		        document.getElementById("txtBh").value = abc.firstChild.nodeValue;
		        document.getElementById("lkn_submit").click();
		    }
		    
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
                        if(newY - menu.offsetHeight<ScrollPosition().top)
                        {
                            menu.style.top=ScrollPosition().top+"px";
                        }else
                        {
                            menu.style.top = newY - menu.offsetHeight+"px";
                        }
                    }

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
            
             
