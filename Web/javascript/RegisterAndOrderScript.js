
function filterSql()
{
	var arrinput=document.getElementsByTagName("input");

	for(var i=0;i<arrinput.length;i++)
	{
		if(arrinput[i].type=="text" || arrinput[i].type=="password")
		{
			arrinput[i].value=getNewStr(arrinput[i].value);
		}
	}

	var arrtextarea=document.getElementsByTagName("textarea");

	for(var i=0;i<arrtextarea.length;i++)
	{
		arrtextarea[i].value=getNewStr(arrtextarea[i].value);
	}

	if (typeof(Page_ClientValidate) != 'function' || Page_ClientValidate())
	{
		__doPostBack('lkSubmit','');
	}

}



	     function tuijianName()
		{
		  
		   var tjid=document.getElementById('Txttj');
		   var tjname=document.getElementById('txtTuiJianName');
		  
		   var result=AjaxClass.gettjazname(tjid.value).value;
		   tjname.innerText=result;
		
		}
		function anzhiName()
		{
		   
		   var anid=document.getElementById('Txtsb');
		   var anname=document.getElementById('txtAnZhiName');
		
		   var result=AjaxClass.gettjazname(anid.value).value;
		   anname.innerText=result;
	
		}
		function GettuijianName()
		{
		   
		   var tjid=document.getElementById('Txttj');
		   if(tjid.value=="")
		   {
		      alert("推荐人为空");
		      return;
		   }
		  
		   var result=AjaxClass.gettuijianvalue(tjid.value).value;
		   alert(result);
		}
		function GetanzhiName()
		{
		    var azid=document.getElementById('Txtsb');
		    var tjid=document.getElementById('Txttj');
		    var aztxt;
		   
		    if(azid.value=="")
		    {
		       if(tjid.value!="")
		       {
		          aztxt=tjid.value;
		       }
		       else
		       {
		          alert("安置人为空");
		          return;
		       }
		    }
		    else
		    {
		   
		       aztxt=azid.value;
		        
		    }
		    
		    var result=AjaxClass.gettjazname(aztxt).value;
		   alert(result);
		}
		function CheckFrom()
		{
		    debugger;
		    var bh=document.getElementById('Txtbh');
		    var tjbh=document.getElementById('Txttj');
		    var azbh=document.getElementById('Txtsb');
		    var xm=document.getElementById('Txtxm');
		    var zj=document.getElementById('Txtzj');
		    var Sex=document.getElementById('RadioBtnSex');
		   // var bithdy=document.getElementById('DatePicker_ww');
		   // alert("1");
		    if(tjbh.value=="")
		    {
		       alert("推荐编号不能为空");
		      // tjbh.Locu
		       return false;
		    }
		    		     					     
		    var validSTR="abcdefghijklmnopqrstuvwxyz-1234567890";
		   
			for(var i=0;i<bh.value.length;i++)
			{
			  
				if (validSTR.indexOf(bh.value.charAt(i).toLowerCase() )==-1)
				{
					alert("编号请输入字母，数字，横线!");
					return false;
				}
			}
		    if(bh.value.length<4||bh.value.length>10)
		    {
		        alert("对不起,编号长度必须在4-10之间");
		        return false;
		    }
		    if(xm.value=="")
		    {
		        alert("姓名不能为空");
		        return false;
		    }
		    var manageId = AjaxClass.GetManageId(3).value+"";
		    if(bh.value== manageId||bh.value=="1111111111")  
			{
				alert("错误！该编号已经被占用！");
				return false;
			}
			if(bh.value == azbh.value)  
			{
				messageLabel.Text="错误！编号和安置编号重！";
				return false;				
			}
			if(bh.value==tjbh.value )  
			{
				messageLabel.Text="错误！编号和推荐编号重！";
				return false;
			}
			// alert(bithdy.value);
			//var ShenFen=AjaxClass.ValidShenFenZheng(zj.value,bithdy.value,Sex.value).value;
			// alert("3");
			//alert(ShenFen);
			// alert("4");
			//return false;
		   // return true;
		}
		
		
		
		function checkData()
        {
             var fileName=document.getElementById("myFile").value;
             if(fileName=="")
             return;
             var exName=fileName.substr(fileName.lastIndexOf(".")+1).toUpperCase()
            //alert(exName)
             if(exName=="JPG"||exName=="BMP"||exName=="GIF")
             {
                document.getElementById("myimg").src=fileName
              }
             else
            {
             alert("请选择正确的图片文件")
             document.getElementById("myFile").value=""
            } 
       }

		function checkObj(obj)
		{
		if(obj.id=='MemberBankCardType')
		{
		   document.getElementById ('MemberBankCardNameParent').style .display =(obj.rows[0].cells[1].childNodes[0].checked==true?'':'none');
		   if(obj.rows[0].cells[1].childNodes[0].checked==false)
		   {
		      document.getElementById ('TxtZheHao').value='详细名称：与存折上红色印章的文字相同';
		   }
		}
		}
		
		function clearText()
		{
		
		 // if(document.getElementById ('MemberBankCardType').rows[0].cells[1].childNodes[0].checked)
		 // {
		 //    document.getElementById ('TxtZheHao').value='';
		 // }
		  return true;
		}
		document.oncontextmenu =function()
		{
		   alert('欢迎您的光临');
		   event.returnValue=false;
		   return;
		}
		
		
		
		function getTuiJianName()
		{
		  var xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
          
          var url="getTuiJianName.aspx?action=reg&number="+document.Form1 .Txttj .value;
		  xmlhttp.open("post", url, false); 
		   xmlhttp.send();      
          if (xmlhttp.readyState == 4) // 调用完毕
                 {
                   if(xmlhttp.status==200)
                     {
                     return (xmlhttp.responseText);  
                    }   
                    else
                    {
                      return "未得到姓名";
                    }
                 }
		}	
	
	    function GetTuiJian()
		{
		  // filterSql();
		   var tjid=document.getElementById('Txttj');
		   var tjname=document.getElementById('txtTuiJianName');
		   if(tjid.value=="")
		   {
		      alert("推荐编号为空");
		      return;
		   }
		   if(tjname.value=="")
		   {
				alert("推荐人姓名为空");
		      return;
		   }
		   //var sql="select count(*) from memberInfo where number='"+tjid.value+"' and name='"+tjname.value+"'";
		   var result=AjaxClass.GetTuiJian(tjid.value,tjname.value).value;
		   alert(result);
		}
		
		function GetAnZhi()
		{
		 // filterSql();
		    var azid=document.getElementById('Txtsb');
		    var tjid=document.getElementById('Txttj');
		    var anname=document.getElementById('txtAnZhiName');
		    var aztxt;
		   
		    if(azid.value=="")
		    {
		       if(tjid.value!="")
		       {
		          aztxt=tjid.value;
		       }
		       else
		       {
		          alert("安置编号为空");
		          return;
		       }
		    }
		    else
		    {
		   
		       aztxt=azid.value;
		        
		    }
		    if(anname.value=="")
		    {
				 alert("安置人姓名为空");
		          return;
		    }
		   anname=anname.value;
		  // var sql="select  count(Name)  from memberInfo where number='"+aztxt+"'"+" and name='"+anname.value+"'";
		   var result=AjaxClass.GetAnZhi(aztxt,anname).value;
		   alert(result);
		}
		
	    function checkNumber()
	    {
	     if(document.getElementById ('ucPaperList_DropDownBank').value=="身份证")
		  {
		     var xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
             var checkUrl="CheckPaperNumber.aspx?cid="+document.getElementById ('Txtzj').value+"&country="+document.getElementById('ucZone_DropDownGuojia').value+'&zone='+document.getElementById('ucZone_DropDownShengfen').value+'&sex='+(document.getElementById ('RadioBtnSex_0').checked==true?"1":"0")+'&birthday='+document.getElementById("ucBirthday_ww").value;
             
             xmlhttp.open("post", checkUrl, false); 
		     xmlhttp.send();  
		     if (xmlhttp.readyState == 4) // 调用完毕
                 {
                   if(xmlhttp.status==200)
                     {
                        if(xmlhttp.responseText=="1")
                          {
                             return true;
                          }
                        else
                          {
                             alert(xmlhttp.responseText);
                             return false;
                          }  
                    }   
                    else
                    {
                      return false;
                    }
                 }    
          }
          else
          {
             return true;
          }
         }

    /*
    获取邮编
    */
    function getAdCode(city)
    {
      var result= AjaxClass.GetAddressCode(city).value;
      alert("该收货地址的邮编是："+result);
    }
    
    
          function card()
		  {
		    var sex=document.getElementById("trSex");
		    var birth=document.getElementById("trBirth");
		    var card=document.getElementById("dplCardType").value;
		    var cardId=document.getElementById("cardID");
		    if(card==1)
		    {
		      sex.style.display="none";
		      birth.style.display="none";
		      cardId.style.display="block";
		    }
		    else if(card!=1&&card!=0)
		    {
		     sex.style.display="block";
		      birth.style.display="block";
		      cardId.style.display="block";
		    }
		    else
		    {
		      sex.style.display="block";
		      birth.style.display="block";
		      cardId.style.display="none";
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
		     else
		     {
		        trNumber.style.display="block";
		        trPass.style.display="block";
		     }
		  }