

/**
UpdZhuChe.aspx  begin
*/


function menu( menu,img,plus )
		{			
			if( menu.style.display == "none") 
			{
				menu.style.display="";
				img.src="store/images/foldopen.gif";
				plus.src="store/images/MINUS2.GIF";		
			}
			else
			{
				menu.style.display = "none";
				img.src="store/images/foldclose.gif";
				plus.src="store/images/PLUS2.GIF";				
			}			
		}		
		          
		function GetCCode_s2(city)
		{
		    var sobj = document.getElementById("<%=Txtyb.ClientID %>");
		    sobj.value=AjaxClass.GetAddressCode(city).value
		}			
		
		function abc()
		{
		    if(document.getElementById("Ddzf").options[document.getElementById("Ddzf").selectedIndex].value=="2")
		    {
		        document.getElementById("DD1").style.display="";
		        document.getElementById("DD2").style.display="";
		    }
		    else
		    {
		        document.getElementById("DD1").style.display="none";
		        document.getElementById("DD2").style.display="none";
		    }
		}	
		
		function  Bind()
		{
	 
		   var divPro = document.getElementById('product');
			var pId="";
			var productid="";
			var hasChose=0;
			for(var i=0;i<document.getElementById('menuLabel').getElementsByTagName('input').length;i++)
			{
				if(document.getElementById('menuLabel').getElementsByTagName('input').item(i).getAttribute("type")=="text")
				{
				    var numx=document.getElementById('menuLabel').getElementsByTagName('input').item(i).value;
				    var num=Number(document.getElementById('menuLabel').getElementsByTagName('input').item(i).value);
				    if(numx!=num)//验证输入产品数量是否是数字

				    {
				        alert(getpro().pdnum);
				        document.getElementById('menuLabel').getElementsByTagName('input').item(i).value=0;
				    }
				    if(num>0)//数量大于0记录产品
				    {
				      
				    	pId+=document.getElementById('menuLabel').getElementsByTagName('input').item(i).value+",";
					    productid+=document.getElementById('menuLabel').getElementsByTagName('input').item(i).name+",";
					    ++hasChose; 
					}
				}
			}

			
			var storeid= getpro().gstid;

			divPro.innerHTML=AjaxClass.DataBindTxt(pId,storeid,productid,"tablemb","",document.getElementById("DropCurrency").options[document.getElementById("DropCurrency").selectedIndex].value).value;//更新产品表格记录	
	    
	      
	    
	    }
	    
	    
	     function ShowProductDiv(sender,pid)
            {
                //弹出层
                document.getElementById("divShowProduct").style.display = "block";
                var leftpos = 0,toppos = 0;
                var pObject = sender.offsetParent;
                if (pObject)
                {
                    leftpos += pObject.offsetLeft;
                    toppos += pObject.offsetTop;
                }
                while(pObject=pObject.offsetParent )
                {
                    leftpos += pObject.offsetLeft;
                    toppos += pObject.offsetTop;
                };

                document.getElementById("divShowProduct").style.left = (sender.offsetLeft + leftpos) + "px";
                document.getElementById("divShowProduct").style.top = (sender.offsetTop + toppos + sender.offsetHeight - 2) + "px";
                
               //显示树信息
               document.getElementById("divShowProduct").innerHTML="";
    		   
               if(pid=="")
               {
                    document.getElementById("divShowProduct").style.display="none";
                    return;                        
               }
               else
               {
                   var result=AjaxClass.GetProductDetail1(pid).value;
                   document.getElementById("divShowProduct").innerHTML=result;
               }

            }
            function HideProductDiv(sender)
            {
                document.getElementById("divShowProduct").style.display = "none";
            }
            
            /**
             UpdZhuChe.aspx  end 
            **/
            
            
            
            
            /**
             UpdateNet begin 
            **/
             function UpNet(bianhao)
	    {
		    var tBh = getpro().gtb;
		    var sjBh = "";
		    debugger;
		    var topMemberId = AjaxClass.GetManageId(3).value+"";
		    if(bianhao==tBh || bianhao==topMemberId)
		    {
			    sjBh = bianhao;
		    }
		    else
		    {
			    sjBh = AjaxClass.UpBianhao(bianhao.toString()).value;
		    }
		    var div = document.getElementById('Divt1');
		    var strhtml1=AjaxClass.WangLuoTu_AnZhi(sjBh).value;

		    div.innerHTML = strhtml1;
	    }
    	
	    function DownNet(bianhao)
	    {
    	    debugger;
		    var topBianhao = bianhao.toString();
		    var div = document.getElementById('Divt1');
		    var strhtml1=AjaxClass.WangLuoTu_AnZhi(topBianhao).value;

		    div.innerHTML = strhtml1;
	    }
	    function GetAnZhiNumber(bianhao)
	    {
	        
		    var anzhibh = bianhao;
		    var sjBh = bianhao;
		    document.getElementById('txtAz').value = sjBh;
		   // document.getElementById('petname3').value = AjaxClass.GetPname(sjBh).value;
		    document.getElementById('Divt1').style.display = "none";
	    }
        
        function  ShowDiv(selectValue)
		{
		  if(selectValue==null)
		  {
		       if(document.getElementById("Divt1").style.display=="none")
		       {
		           document.getElementById("Divt1").innerHTML="";
		           var txtSb=  document.getElementById("txtTj").value;
		           var topBh = '<%=GetTopBh() %>';
        		   
		           if(txtSb=="")
		           {
                        alert(getpro().tjbh);
                        return;                        
		           }
		           else
		           {
//		               var isSure = AjaxClass.CheckTuiJian(topBh,txtSb).value;
//		               if(!isSure)
//		               {
//		                    alert(getpro().tjwl);
//                            return;  
//		               }
//		               else
//		               {
		                   var result=AjaxClass.WangLuoTu_AnZhi(txtSb).value;
		                   document.getElementById("Divt1").innerHTML=result;
		                   document.getElementById("Divt1").style.display="block";
		               //}
		           }
		        }
		        else
		        {
		            document.getElementById("Divt1").style.display = "none";
		        }
		  }
		  else
		  {
		     var result=AjaxClass.Direct_Table2(selectValue).value
		     document.getElementById("Divt1").innerHTML=result;
		  }
		   
		}
            
            /**
             UpdateNet end  
            **/