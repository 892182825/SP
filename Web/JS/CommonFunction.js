  
  //去空格
  function trim(s)
  { 
      return  s.replace(/(^\s*)|(\s*$)/g,"");     
  }
  
  
    
	    /*
	        Registermember.aspx begin 
	    */
	      function change()
        {
            var a = '<%=GetCssType() %>';
            var css=document.getElementById("cssid");
            if (a==1)
                css.setAttribute("href","../Company/CSS/Company.css");
            if (a==2)
                css.setAttribute("href","CSS/Store.css");
            if (a==3)
                css.setAttribute("href","../Member/CSS/Member.css");
        }
        
        function ChangeNumber()
        {
        debugger
            var number = document.getElementById("Txtbh");
            var bh = AjaxClass.GetMemberNumber().value;     
            number.value = bh;
            document.getElementById("HidBh").value = bh;
        }
        
        function GetAzNumber()
        {
            var number = document.getElementById('Txtsb').value;
            if(number != "")
            {
                document.getElementById('HidAz').value = number;
            }
        }
        
        function GetTxtNumber()
        {
            var number = document.getElementById("Txtbh").value;
            if(number!="")
            {
                if(number.length<6)
                {
                   // alert('<%=this.GetTran("000306","抱歉！您输入的会员编号小于6位！") %>');
                    document.getElementById("labNumber").value = getWebPro().nblen;                    
                    return;                
                }
                var validSTR="abcdefghijklmnopqrstuvwxyz-1234567890";
			
			    for(var i=1;i<number.length+1;i++)
			    {
				    if (validSTR.indexOf(number.substring(i-1,i))==-1)
				    {
				        document.getElementById("labNumber").value = getWebPro().nbuse;
					    //alert('<%=this.GetTran("000309","编号请输入字母，数字，横线！") %>');
					    return ;
				    }
			    }
			    
			    document.getElementById("HidBh").value = number;
            }
            else 
            {
                document.getElementById("labNumber").value =getWebPro().nbnull;
                //alert('<%=GetTran("000129","会员编号不能为空！") %>');
                return;
            }
            document.getElementById("labNumber").value = "";
        }
        
        function GetTxtNumber2()
        {
            var number = document.getElementById("Txtbh").value;
            if(number!="")
            {
                if(number.length<6)
                {
                    document.getElementById("labNumber").value = getWebPro().nblen;       
                    //alert('<%=this.GetTran("000306","抱歉！您输入的会员编号小于6位！") %>');
                    return false;                
                }
                var validSTR="abcdefghijklmnopqrstuvwxyz-1234567890";
			
			    for(var i=1;i<number.length+1;i++)
			    {
				    if (validSTR.indexOf(number.substring(i-1,i))==-1)
				    {
				        document.getElementById("labNumber").value =getWebPro().nbuse;
					    //alert('<%=this.GetTran("000309","编号请输入字母，数字，横线！") %>');
					    return false;
				    }
			    }
            }
            else 
            {
                document.getElementById("labNumber").value =getWebPro().nbnull;
                //alert('<%=GetTran("000129","会员编号不能为空！") %>');
                return false;
            }
            document.getElementById("labNumber").value = "";
            return true;
        }
        
        function ShowTjName()
        {
            document.getElementById("aTj").style.display = "none";
            document.getElementById("Span1").style.display = "";
            document.getElementById("spanTj").style.display = "";
        }
        
        function NoneTjName()
        {
            document.getElementById("aTj").style.display = "";
            document.getElementById("Span1").style.display = "none";
            document.getElementById("spanTj").style.display = "none";
        }
        
        function ShowAzName()
        {
            document.getElementById("aAz").style.display = "none";
            document.getElementById("Span2").style.display = "";
            document.getElementById("spanAz").style.display = "";
        }
        
        function NoneAzName()
        {
            document.getElementById("aAz").style.display = "";
            document.getElementById("Span2").style.display = "none";
            document.getElementById("spanAz").style.display = "none";
        }
        
    
	    function ShowStore()
	    {
	        var isMuch = '<%=GetRegType() %>';
	        if(isMuch=="1")
	        {
	            document.getElementById('Txtbh').style.borderStyle = "";
	        }
	        
	        var type = '<%=GetLogoutType() %>';

	        if(type=="2")
	        {
	            document.getElementById('trStore').style.display = "none";
	        }
	        else
	        {
	            document.getElementById('trStore').style.display = "";
	        }

	        var cardType=document.getElementById("dplCardType").options[document.getElementById("dplCardType").selectedIndex].value;

	        //var cardId = document.getElementById("cardID");
	        var tr3 = document.getElementById("tr3");
	        //var divSex = document.getElementById("divSex");
	        var tr1 = document.getElementById("tr1");
	        var tr2 = document.getElementById("tr2");
	        
	        if (cardType == "2")
            {
                tr3.style.display="";
                tr1.style.display = "none";
                tr2.style.display = "none";

            }
            else if (cardType != "1" && cardType != "2")
            {
                tr3.style.display="";
                tr1.style.display = "";
                tr2.style.display = "";
            }
            else
            {
                tr3.style.display="none";
                tr1.style.display = "";
                tr2.style.display = "";
            }
	     }
	     
	    function UpNet(bianhao)
	    {
		    var tBh =  getWebPro().GetTopBh2;
		    var sjBh = "";
		    var topMemberId = getWebPro().GetTopBh() ;
		    if(bianhao==tBh || bianhao==topMemberId)
		    {
			    sjBh = bianhao;
		    }
		    else
		    {
			    sjBh = AjaxClass.UpBianhao(bianhao.toString()).value;
		    }
		    var div = document.getElementById('Divt1');
		    
		    var strhtml1=AjaxClass.WangLuoTu_FindAnZhi(sjBh.toString()).value;
            div.innerHTML=strhtml1;
		    
		    document.getElementById("divShowView").style.display="none";
	    }
    	
	    function DownNet(bianhao)
	    {
		    var topBianhao = bianhao.toString();
		    var div = document.getElementById('Divt1');
		    var strhtml1=AjaxClass.WangLuoTu_FindAnZhi(topBianhao).value;

		    div.innerHTML = strhtml1;
		    
		    document.getElementById("divShowView").style.display="none";
	    }
	    
	  
	    function GetAnZhiNumber(bianhao)
	    {
	        
		    var anzhibh = bianhao;
		    var sjBh = bianhao;
		    document.getElementById('Txtsb').value = sjBh;
		    document.getElementById('HidAz').value = sjBh;
		   // document.getElementById('petname3').value = AjaxClass.GetPname(sjBh).value;
		    document.getElementById('Divt1').style.display = "none";
		    
		    document.getElementById("divShowView").style.display="none";
	    }
	    
	    function VerifyPaperNumber()
	    {
	        var paperNumber = document.getElementById("Txtzj").value;
	       // var cardType = document.getElementById("dplCardType").value; 
	        var cardType=document.getElementById("dplCardType").options[document.getElementById("dplCardType").selectedIndex].value;
	        
            if(cardType == "2")
            {
                var result = AjaxClass.VerifyPaperNumber(paperNumber).value;
                if(result.indexOf(",") <= 0)
                {
                    //alert(result);
                    document.getElementById('spanZj').innerHTML = result;
                    return;
                }
            }
            else
            {
            
                if(cardType != "1")
                {
                 debugger;
                    if (paperNumber.length < 6)
                    {
                       // alert('<%= this.GetTran("000326", "证件号不能小于6位")%>');
                        document.getElementById("spanZj").innerHTML = " "+getWebPro().cardlen;
                        return;
                    }
                    
                    var validSTR="abcdefghijklmnopqrstuvwxyz-_:()1234567890";
			
			        for(var i=1;i<paperNumber.length+1;i++)
			        {
				        if (validSTR.indexOf(paperNumber.substring(i-1,i))==-1)
				        {
				            document.getElementById("spanZj").innerHTML = " "+getWebPro().carduse;
					        //alert('<%=this.GetTran("006818","证件号码只能只能输入字母，数字，横线，下划线，冒号和括号！") %>');
					        return ;
				        }
			        }
                 }
            }
            document.getElementById("spanZj").innerHTML = "";
	    }

	    //详细地址
	    function addOnfocus()
	    {
		    var address = document.getElementById('txtdz');
		    if(address.value=='详细地址')
		    {
		        address.style.color = "";
			    address.value='';
		    }
	    }
	    
	    function addOnfocus2()
	    {
	        debugger;
		    var address = document.getElementById('txtdz');
		    if(address.value=='详细地址')
		    {
		    alert(getWebPro().adrnull+"!");
		        //document.getElementById('spanAddress').innerHTML =getWebPro().adrnull ;
		        //alert('<%=GetTran("006816","详细地址不能为空！") %>');
			    return false;
		    }
		    document.getElementById('spanAddress').innerHTML = "";
		    return true;
	    }
    	
	    function addressOnblur()
	    {
		    var address = document.getElementById('txtdz');
		    if(address.value=='')
		    {
		        address.style.color = "gray";
			    address.value='详细地址';
			    alert(getWebPro().adrnull);
			    return;
		    }
	    }    
	    
	    function jtdhQhOnfocus()
	    {
	        var txtvalue = document.getElementById('TxtjtdhQh');
		    if(txtvalue.value=='区号')
		    {
		        txtvalue.style.color = "";
			    txtvalue.value='';
		    }
	    }
	    
	    function BankAddressOnfocus()
	    {
	        var txtvalue = document.getElementById('txtBranchName');
	        if(txtvalue.value=='支行名称')
	        {
	            txtvalue.style.color = "";
			    txtvalue.value='';
	        }
	    }
	    
	    function BankAddressOnblur()
	    {
	        var txtvalue = document.getElementById('txtBranchName');
	        if(txtvalue.value=='')
	        {
	            txtvalue.style.color = "gray";
			    txtvalue.value='支行名称';
	        }
	    }
	    
	    function czdhQhOnfocus()
	    {
	        var txtvalue = document.getElementById('TxtczdhQh');
		    if(txtvalue.value=='区号')
		    {
		        txtvalue.style.color = "";
			    txtvalue.value='';
		    }
	    }
	    
	    function czdhQhOnblur()
	    {
	        var txtvalue = document.getElementById('TxtczdhQh');
	        if(txtvalue.value=='')
	        {
	            txtvalue.style.color = "gray";
			    txtvalue.value='区号';
	        }
	        else
	        {
	            if(txtvalue.value!='区号')
		        {
			        var isInt = isIntTel(txtvalue.value);
			        if(!isInt)
			        {
				        document.getElementById('spanFaxTel').innerHTML =  '传真电话区号只能输入数字！';
				        return;
			        }
			    }
	        }
	        document.getElementById('spanFaxTel').innerHTML =  '';
	    }
	    
	    function czdhQhOnblur2()
	    {
	        var txtvalue = document.getElementById('TxtczdhQh');
	        if(txtvalue.value=='')
	        {
	            txtvalue.style.color = "gray";
			    txtvalue.value='区号';
	        }
	        else
	        {
	            if(txtvalue.value!='区号')
		        {
			        var isInt = isIntTel(txtvalue.value);
			        if(!isInt)
			        {
				        document.getElementById('spanFaxTel').innerHTML =  '传真电话区号只能输入数字！';
				        return false;
			        }
			    }
	        }
	        document.getElementById('spanFaxTel').innerHTML =  '';
	        return true;
	    }
	    
	    function bgdhQhOnfocus()
	    {
	        var txtvalue = document.getElementById('TxtbgdhQh');
		    if(txtvalue.value=='区号')
		    {
		        txtvalue.style.color = "";
			    txtvalue.value='';
		    }
	    }
	    
	    function bgdhQhOnblur()
	    {
	        var txtvalue = document.getElementById('TxtbgdhQh');
	        if(txtvalue.value=='')
	        {
	            txtvalue.style.color = "gray";
			    txtvalue.value='区号';
	        }
	        else
	        {
	            if(txtvalue.value!='区号')
		        {
			        var isInt = isIntTel(txtvalue.value);
			        if(!isInt)
			        {
				        document.getElementById('spanOfficeTel').innerHTML =  '传真电话区号只能输入数字！';
				        return;
			        }
			    }
	        }
	        document.getElementById('spanOfficeTel').innerHTML =  '';
	    }
	    
	    function bgdhQhOnblur2()
	    {
	        var txtvalue = document.getElementById('TxtbgdhQh');
	        if(txtvalue.value=='')
	        {
	            txtvalue.style.color = "gray";
			    txtvalue.value='区号';
	        }
	        else
	        {
	            if(txtvalue.value!='区号')
		        {
			        var isInt = isIntTel(txtvalue.value);
			        if(!isInt)
			        {
				        document.getElementById('spanOfficeTel').innerHTML =  '传真电话区号只能输入数字！';
				        return false;
			        }
			    }
	        }
	        document.getElementById('spanOfficeTel').innerHTML =  '';
	        return true;
	    }
	    
	    function jtdhQhOnblur()
	    {
	        var txtvalue = document.getElementById('TxtjtdhQh');
	        if(txtvalue.value=='')
	        {
	            txtvalue.style.color = "gray";
			    txtvalue.value='区号';
	        }
	        else
	        {
	            if(txtvalue.value!='区号')
		        {
			        var isInt = isIntTel(txtvalue.value);
			        if(!isInt)
			        {
				        document.getElementById('spanFamilyTel').innerHTML =  '家庭电话区号只能输入数字！';
				        return;
			        }
			    }
	        }
	        document.getElementById('spanFamilyTel').innerHTML =  '';
	    }
	    
	    function jtdhQhOnblur2()
	    {
	        var txtvalue = document.getElementById('TxtjtdhQh');
	        if(txtvalue.value=='')
	        {
	            txtvalue.style.color = "gray";
			    txtvalue.value='区号';
	        }
	        else
	        {
	            if(txtvalue.value!='区号')
		        {
			        var isInt = isIntTel(txtvalue.value);
			        if(!isInt)
			        {
				        document.getElementById('spanFamilyTel').innerHTML =  '家庭电话区号只能输入数字！';
				        return false;
			        }
			    }
	        }
	        document.getElementById('spanFamilyTel').innerHTML =  '';
	        return true;
	    }
	    
	    function VerifyPaperNumber2()
	    {
	        var paperNumber = document.getElementById("Txtzj").value;
	       // var cardType = document.getElementById("dplCardType").value; 
	        var cardType=document.getElementById("dplCardType").options[document.getElementById("dplCardType").selectedIndex].value;

            if(cardType == "2")
            {
                var result = AjaxClass.VerifyPaperNumber(paperNumber).value;
                if(result.indexOf(",") <= 0)
                {
                    alert(result);
                    return false;
                }
            }
            else
            {
                if(cardType != "1")
                {
                    if (paperNumber.length < 6)
                    {
                       document.getElementById("spanZj").innerHTML = " "+getWebPro().cardlen;
                       // alert('<%=GetTran("000326","证件号不能小于6位!") %>');
                        return false;
                    }
                                        
                    var validSTR="abcdefghijklmnopqrstuvwxyz-_:()1234567890";
			
			        for(var i=1;i<paperNumber.length+1;i++)
			        {
				        if (validSTR.indexOf(paperNumber.substring(i-1,i))==-1)
				        {
				            document.getElementById("spanZj").innerHTML = " "+getWebPro().carduse;
					        //alert('<%=this.GetTran("006818","证件号码只能只能输入字母，数字，横线，下划线，冒号和括号！") %>');
					        return false;
				        }
			        }
                 }
            }
            return true;
	    }

	     //判断是否是半角数字
	    function isShuZi(txtStr)
	    {
		    var	oneNum="";
		    for(var i=0;i<txtStr.length;i++)
		    {
			    oneNum=txtStr.substring(i,i+1);
			    if (oneNum<"0" || oneNum>"9")
				    return true;
		    }
		    return false;
	    }
	    
	    function isTel(txtStr)
	    {
	        debugger;
	        var validSTR="1234567890-#*";

		    for(var i=1;i<txtStr.length+1;i++)
		    {
		      //  alert(validSTR.indexOf(txtStr.substring(i-1,i)));
			    if (validSTR.indexOf(txtStr.substring(i-1,i))==-1)
			    {				    
				    return false;
			    }
		    }
		    return true;
	    }
	    
	    function isIntTel(txtStr)
	    {
	        var validSTR="1234567890";

		    for(var i=1;i<txtStr.length+1;i++)
		    {
		      //  alert(validSTR.indexOf(txtStr.substring(i-1,i)));
			    if (validSTR.indexOf(txtStr.substring(i-1,i))==-1)
			    {				    
				    return false;
			    }
		    }
		    return true;
	    }

	    //判断输入的手机号格式是否正确
	    function MobTelQuOnblur()
	    {
		    var mobTel = document.getElementById('Txtyddh').value;
		    if(mobTel!='')
		    {
			    var isInt = isShuZi(mobTel);
			    if(isInt)
			    {
			        document.getElementById('spanMobile').innerHTML =getWebPro().mpbj;
				    //alert('<%=GetTran("006559","移动电话必须是半角数字组成的!") %>');
				    return;
			    }
			    if(mobTel.length!=11)
			    {
			        document.getElementById('spanMobile').innerHTML =getWebPro().mplen;
				    return;
			    }
		    }
		    else
		    {
		        document.getElementById('spanMobile').innerHTML =getWebPro().mpnull ;
		        return;
		    }
		    document.getElementById('spanMobile').innerHTML = "";
	    }
    	
	    function MobTelQuOnblur2()
	    {
		    var mobTel = document.getElementById('Txtyddh').value;
		    if(mobTel!='')
		    {
			    var isInt = isShuZi(mobTel);
			    if(isInt)
			    {
				    document.getElementById('spanMobile').innerHTML =getWebPro().mpbj ;
				    return false;
			    }
			    if(mobTel.length!=11)
			    {
				    document.getElementById('spanMobile').innerHTML = getWebPro().mplen;
				    return false;
			    }
		    }
		    else
		    {
		        document.getElementById('spanMobile').innerHTML =getWebPro().mpnull;
		        return false;
		    }
		    document.getElementById('spanMobile').innerHTML = "";
		    return true;
	    }

	    function CheckBankCard()
	    {
	        var bankCard = document.getElementById("TxtBankCard").value;
	        if(bankCard!="")
	        {
	            var isInt = isShuZi(bankCard);
	            if(isInt)
			    {
				    document.getElementById('spanCard').innerHTML =getWebPro().bknum;
				    return ;
			    }
			    if(bankCard.length>25 || bankCard.length<10)
			    {
			        document.getElementById('spanCard').innerHTML = getWebPro().bklen;
				    return ;
			    }
	        }
	        document.getElementById('spanCard').innerHTML = "";
	    }
	    
	    function CheckBankCard2()
	    {
	        var bankCard = document.getElementById("TxtBankCard").value;
	        if(bankCard!="")
	        {
	            var isInt = isShuZi(bankCard);
	            if(isInt)
			    {
				    document.getElementById('spanCard').innerHTML = getWebPro().bknum;
				    return false;
			    }
			    if(bankCard.length>25 || bankCard.length<10)
			    {
			        document.getElementById('spanCard').innerHTML =getWebPro().bklen;
				    return false;
			    }
	        }
	        document.getElementById('spanCard').innerHTML = "";
	        return true;
	    }
	    
	    function VerifyPostCard()
	    {
	        var postCard = document.getElementById('Txtyb').value;
//	        if(postCard=='')
//	        {
//	            document.getElementById('spanTb').innerHTML = '<%=GetTran("000134","对不起，邮编不能为空！") %>';
//	            //alert('<%=GetTran("000134","对不起，邮编不能为空！") %>');
//	            return ;
//	        }
            if(postCard!='')
            {
	            var isInt = isShuZi(postCard);
		        if(isInt)
		        {
		            document.getElementById('spanTb').innerHTML =getWebPro().posuse;
			        //alert('<%=GetTran("006819","邮编必须是半角数字组成的！") %>');
			        return ;
		        }
		        if(postCard.length!=6)
		        {
		            document.getElementById('spanTb').innerHTML =getWebPro().poslen;
		            //alert('<%=GetTran("006820","邮编必须是6位！") %>');
		            return ;
		        }
		        document.getElementById('spanTb').innerHTML = "";
		    }
	    }
	    
	    function VerifyPostCard2()
	    {
	        var postCard = document.getElementById('Txtyb').value;
//	        if(postCard=='')
//	        {
//	            document.getElementById('spanTb').innerHTML = '<%=GetTran("000134","对不起，邮编不能为空！") %>';
//	            //alert('<%=GetTran("000134","对不起，邮编不能为空！") %>');
//	            return false;
//	        }
            if(postCard!='')
            {
	            var isInt = isShuZi(postCard);
		        if(isInt)
		        {
		            document.getElementById('spanTb').innerHTML =getWebPro().posuse;
			        //alert('<%=GetTran("006819","邮编必须是半角数字组成的！") %>');
			        return false;
		        }
		        if(postCard.length!=6)
		        {
		            document.getElementById('spanTb').innerHTML =getWebPro().poslen;
		            //alert('<%=GetTran("006820","邮编必须是6位！") %>');
		            return false;
		        }
		        document.getElementById('spanTb').innerHTML = "";
		    }
		    return true;
	    }
	    
	    function famTelOnfocus()
	    {
	        var famTel = document.getElementById('Txtjtdh');
	        if(famTel.value=='电话号码')
	        {
	            famTel.style.color="";
	            famTel.value="";
	        }
	    }

	    function faxTelFjOnfocus()
	    {
	        var faxTel = document.getElementById('TxtczdhFj');
	        if(faxTel.value=='分机号')
	        {
	            faxTel.style.color="";
	            faxTel.value="";
	        }
	    }
	    
	    function faxTelFjOnblur()
	    {
		    var faxTel = document.getElementById('TxtczdhFj');
		    if(faxTel.value!='')
		    {
		        if(faxTel.value!='分机号')
		        {
		            var isInt = isIntTel(faxTel.value);
		            if(!isInt)
		            {
			            document.getElementById('spanFaxTel').innerHTML =  '传真电话分机号只能输入数字！';
			            return;
		            }
		        }
			}
		    else
		    {
		        faxTel.style.color = "gray";
			    faxTel.value='分机号';
		    }
		    document.getElementById('spanFaxTel').innerHTML = "";
	    }

	    function faxTelFjOnblur2()
	    {
		    var famTel = document.getElementById('TxtczdhFj');
		    if(famTel.value!='' && famTel.value!="分机号")
		    {
			    var isInt = isTel(famTel.value);
			    if(!isInt)
			    {
				    document.getElementById('spanFaxTel').innerHTML = '传真电话分机号只能输入数字！';
				    return false;
			    }
			}
			document.getElementById('spanFaxTel').innerHTML = "";
		    return true;
	    }

	    function officeTelFjOnfocus()
	    {
	        var faxTel = document.getElementById('TxtbgdhFj');
	        if(faxTel.value=='分机号')
	        {
	            faxTel.style.color="";
	            faxTel.value="";
	        }
	    }

	    function officeTelFjOnblur()
	    {
		    var faxTel = document.getElementById('TxtbgdhFj');
		    if(faxTel.value!='')
		    {
		        if(faxTel.value!='分机号')
		        {
		            var isInt = isIntTel(faxTel.value);
		            if(!isInt)
		            {
			            document.getElementById('spanOfficeTel').innerHTML =  '办公电话分机号只能输入数字！';
			            return;
		            }
		        }
			}
		    else
		    {
		        faxTel.style.color = "gray";
			    faxTel.value='分机号';
		    }
		    document.getElementById('spanOfficeTel').innerHTML = "";
	    }

	    function officeTelFjOnblur2()
	    {
		    var famTel = document.getElementById('TxtbgdhFj');
		    if(famTel.value!='' && famTel.value!="分机号")
		    {
			    var isInt = isTel(famTel.value);
			    if(!isInt)
			    {
				    document.getElementById('spanOfficeTel').innerHTML = '办公电话分机号只能输入数字！';
				    return false;
			    }
			}
			document.getElementById('spanOfficeTel').innerHTML = "";
		    return true;
	    }

	    function famTelOnblur()
	    {
		    var famTel = document.getElementById('Txtjtdh');
		    if(famTel.value!='')
		    {
		        if(famTel.value!='电话号码')
		        {
			        var isInt = isIntTel(famTel.value);
			        if(!isInt)
			        {
				        document.getElementById('spanFamilyTel').innerHTML =  '家庭电话只能输入数字！';
				        return;
			        }
			    }
		    }
		    else
		    {
		        famTel.style.color = "gray";
			    famTel.value='电话号码';
		    }
		    document.getElementById('spanFamilyTel').innerHTML = "";
	    }

	    function famTelOnblur2()
	    {
		    var famTel = document.getElementById('Txtjtdh');
		    if(famTel.value!='' && famTel.value!="电话号码")
		    {
			    var isInt = isTel(famTel.value);
			    if(!isInt)
			    {
				    document.getElementById('spanFamilyTel').innerHTML = '家庭电话只能输入数字！';
				    return false;
			    }
			}
			document.getElementById('spanFamilyTel').innerHTML = "";
		    return true;
	    }

	    function faxTelOnfocus()
	    {
	        var faxTel = document.getElementById('Txtczdh');
	        if(faxTel.value=='电话号码')
	        {
	            faxTel.style.color="";
	            faxTel.value="";
	        }
	    }

	    function faxTelOnblur()
	    {
		    var faxTel = document.getElementById('Txtczdh');
		    if(faxTel.value!='')
		    {
		        if(faxTel.value!='电话号码')
		        {
			        var isInt = isIntTel(faxTel.value);
			        if(!isInt)
			        {
				        document.getElementById('spanFaxTel').innerHTML =  '传真电话只能输入数字！';
				        return;
			        }
			    }
		    }
		    else
		    {
		        faxTel.style.color = "gray";
			    faxTel.value='电话号码';
		    }
		    document.getElementById('spanFaxTel').innerHTML = "";
	    }

	    function faxTelOnblur2()
	    {
		    var faxTel = document.getElementById('Txtczdh');
		    if(faxTel.value!='' && famTel.value!="电话号码")
		    {
			    var isInt = isTel(faxTel.value);
			    if(!isInt)
			    {
				    document.getElementById('spanFaxTel').innerHTML = '家庭电话只能输入数字！';
				    return false;
			    }
			}
			document.getElementById('spanFaxTel').innerHTML = "";
		    return true;
	    }

	    function offmTelOnfocus()
	    {
	        var faxTel = document.getElementById('Txtbgdh');
	        if(faxTel.value=='电话号码')
	        {
	            faxTel.style.color="";
	            faxTel.value="";
	        }
	    }

	    function offmTelOnblur()
	    {
		    var faxTel = document.getElementById('Txtbgdh');
		    if(faxTel.value!='')
		    {
		        if(faxTel.value!='电话号码')
		        {
			        var isInt = isIntTel(famTel.value);
			        if(!isInt)
			        {
				        document.getElementById('spanOfficeTel').innerHTML =  '办公电话只能输入数字！';
				        return;
			        }
			    }
		    }
		    else
		    {
		        faxTel.style.color = "gray";
			    faxTel.value='电话号码';
		    }
		    document.getElementById('spanOfficeTel').innerHTML = "";
	    }

	    function offmTelOnblur2()
	    {
		    var faxTel = document.getElementById('Txtbgdh');
		    if(faxTel.value!='' && famTel.value!="电话号码")
		    {
			    var isInt = isTel(faxTel.value);
			    if(!isInt)
			    {
				    document.getElementById('spanOfficeTel').innerHTML = '办公电话只能输入数字！';
				    return false;
			    }
			}
			document.getElementById('spanOfficeTel').innerHTML = "";
		    return true;
	    }
	    
	    function CheckRemark()
	    {
	        var remark = document.getElementById("Txtbz").value;
	        if(remark.length>500)
	        {
	            document.getElementById('spanRemark').innerHTML =getWebPro().rklen;
	            //alert('<%=GetTran("006824","备注内容过长！") %>');
	            return;
	        }
	        document.getElementById('spanRemark').innerHTML = "";
	    }
	    
	    function CheckRemark2()
	    {
	        var remark = document.getElementById("Txtbz").value;
	        if(remark.length>500)
	        {
	            document.getElementById('spanRemark').innerHTML =getWebPro().rklen;
	            //alert('<%=GetTran("006824","备注内容过长！") %>');
	            return false;
	        }
	        document.getElementById('spanRemark').innerHTML = "";
	        return true;
	    }
	    
	    function GetBankUser()
		{
//		debugger;
//		    var name = document.getElementById("Txtxm").value;
//		    if(name!="")
//		    {
//		        if(!CheckName(name))
//		        {
//		            alert('<%=GetTran("006952","姓名不可以输入“_”！") %>');
//		            return;
//		        }
//		    }
		    document.getElementById("MemberBankCardName").innerHTML = document.getElementById("Txtxm").value;
		}
		
//		function GetBankUser2()
//		{
//		    var name = document.getElementById("Txtxm").value;
//		    if(name!="")
//		    {
//		        if(!CheckName(name))
//		        {
//		            alert('<%=GetTran("006952","姓名不可以输入“_”！") %>');
//		            return false;
//		        }
//		    }
//		    return true;
//		}
//		
		function CheckPetName()
		{
		    var name = document.getElementById("Txtxm").value;
		    if(name=="")
		    {
		     alert(getWebPro().tname);
		            return false; 
		    }
		    return true;
		}
//		
//		function CheckPetName2()
//		{
//		    var name = document.getElementById("Txtxm").value;
//		    if(name!="")
//		    {
//		        if(!CheckName(name))
//		        {
//		            alert('<%=GetTran("006953","昵称不可以输入“_”！") %>');
//		            return false;
//		        }
//		    }
//		    return true;
//		}
		
		
		function CheckName(txtStr)
	    {
	        debugger;
	        var validSTR="_";

		    for(var i=1;i<txtStr.length+1;i++)
		    {
			    if (validSTR.indexOf(txtStr.substring(i-1,i))>-1)
			    {				    
				    return false;
			    }
		    }
		    return true;
	    }

	    function Verify()
	    {
	    
	    
	    
	         var a = GetTxtNumber2()
	         if(a==false)
	            return false;
           a=CheckPetName();
           if(!a)return false;
 

	         a = VerifyPaperNumber2();
		     if(a==false)
		        return false;

		     a = addOnfocus2();
		     if(a==false)
		        return false;

		     a = MobTelQuOnblur2();
		     if(a==false)
		        return false;

		     a = famTelOnblur2();
		     if(a==false)
		        return false;
		        
		     a = officeTelFjOnblur2();
		     if(a==false)
		        return false;
		        
		     a = jtdhQhOnblur2();
		     if(a==false)
		        return false;
		        
		     a = faxTelFjOnblur2();
		     if(a==false)
		        return false;
		        
		     a = bgdhQhOnblur2();
		     if(a==false)
		        return false;
		        
		     a = czdhQhOnblur2();
		     if(a==false)
		        return false;

		     a = faxTelOnblur2();
		     if(a==false)
		        return false;

		     a = offmTelOnblur2();
		     if(a==false)
		        return false;

		     a = CheckStoreId2();
		     if(a==false)
		        return false;
		        
		     a = SetAZText2();
		     if(a==false)
		        return false;

		     a = VerifyPostCard2();
		     if(a==false)
		        return false;

		     a = CheckRemark2();
		     if(a==false)
		        return false;

		     a = CheckBankCard2();
		     if(a==false)
		        return false;
		        
		        a= isagree();
		        if(!a)return false;
		        
		        return true;
	    }

	    function GetTxtColor()
	    {
	        var txtAddress = document.getElementById("txtdz");
	        if(txtAddress.value=="详细地址")
	        {  
	            txtAddress.style.color = "gray";
	        }
	        else
	        {
	            txtAddress.style.color = "";
	        }

	        var txtFamQh = document.getElementById('TxtjtdhQh');
	        if(txtFamQh.value=="区号")
	        {    
	            txtFamQh.style.color = "gray";
	        }
	        else
	        {
	            txtFamQh.style.color = "";
	        }
	        
	        var txtFamTel = document.getElementById('Txtjtdh');
	        if(txtFamTel.value=='电话号码')
	        {
	            txtFamTel.style.color = "gray";
	        }
	        else
	        {
	            txtFamTel.style.color = "";
	        }

	        var faxValue = document.getElementById('TxtczdhQh');
	        if(faxValue.value=="区号")
	        {    
	            faxValue.style.color = "gray";
	        }
	        else
	        {
	            faxValue.style.color = "";
	        }
	        
	        faxValue = document.getElementById('Txtczdh');
	        if(faxValue.value=="电话号码")
	        {    
	            faxValue.style.color = "gray";
	        }
	        else
	        {
	            faxValue.style.color = "";
	        }
	        
	        faxValue = document.getElementById('TxtczdhFj');
	        if(faxValue.value=="分机号")
	        {    
	            faxValue.style.color = "gray";
	        }
	        else
	        {
	            faxValue.style.color = "";
	        }
	        
	        var officValue = document.getElementById('TxtbgdhQh');
	        if(officValue.value=="区号")
	        {    
	            officValue.style.color = "gray";
	        }
	        else
	        {
	            officValue.style.color = "";
	        }
	        
	        officValue = document.getElementById('Txtbgdh');
	        if(officValue.value=="电话号码")
	        {    
	            officValue.style.color = "gray";
	        }
	        else
	        {
	            officValue.style.color = "";
	        }
	        
	        officValue = document.getElementById('TxtbgdhFj');
	        if(officValue.value=="分机号")
	        {    
	            officValue.style.color = "gray";
	        }
	        else
	        {
	            officValue.style.color = "";
	        }
	        
	        var BankAddress = document.getElementById('txtBranchName');
	        if(BankAddress.value == "支行名称")
	        {
	            BankAddress.style.color = "gray";
	        }
	        else
	        {
	            BankAddress.style.color = "";
	        }
	    }

     
	     
	     
	     
	     
	     
	       function CheckAgree(ckAgree)
	    {
	        if(ckAgree.checked)
	        {
	            document.getElementById("Button1").style.display="";
	            document.getElementById("btnAgree").style.display="none";
	        }
	        else
	        {
	            document.getElementById("Button1").style.display="none";
	            document.getElementById("btnAgree").style.display="";
	        }
	    }
	    
		function GetTuiJian()
		{
		   var tjid=document.getElementById('Txttj');
		   var tjname=document.getElementById('txtTuiJianName');
		   if(tjid.value=="")
		   {
		      alert(getWebPro().tra79);
		      return;
		   }
		   if(tjname.value=="")
		   {
				alert(getWebPro().tra84);
		      return;
		   }
		   var result=AjaxClass.GetTuiJian(tjid.value,tjname.value).value;
		   alert(result);
		}
		
		function GetAnZhi()
		{
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
		          alert(getWebPro().tra86);
		          return;
		       }
		    }
		    else
		    {
		       aztxt=azid.value;
		    }
		    if(anname.value=="")
		    {
				 alert(getWebPro().tra88);
		          return;
		    }
		   anname=anname.value;
		   var result=AjaxClass.GetAnZhi(aztxt,anname).value;
		   alert(result);
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
		function UpdateTree()
		{
		    CheckStoreId();
		   var storeId=document.getElementById("TxtStore").value;
		   var saveStore=document.getElementById("saveStore").value;
		   document.getElementById("saveStore").value=storeId;
		    if(storeId!="")
		    {
	            if(storeId!=saveStore)
		        {
		           var returnValue=AjaxClass.setProductMenu(storeId).value;
		           document.getElementById("menuLabel").innerHTML=returnValue;
		        }	                
		    }
		}
		
		function CheckStoreId()
		{
		    var storeId = document.getElementById("TxtStore").value;
		    if(storeId=="")
		    {
		        document.getElementById("labStoreId").innerHTML =getWebPro().stnull;
		        //alert('<%=GetTran("006026","店铺编号不能为空！") %>');
		        return;
		    }
		    var returnValue=AjaxClass.CheckStoreID(storeId).value;
		    if(returnValue==false)
		    {
		        document.getElementById("labStoreId").innerHTML =getWebPro().stisno;
		        //alert('<%=GetTran("006817","店铺编号不存在，请重新输入！") %>');
		        return;
		    }
		}
		
		function CheckStoreId2()
		{
		    var storeId = document.getElementById("TxtStore").value;
		    if(storeId=="")
		    {
		        document.getElementById("labStoreId").innerHTML =getWebPro().stnull;
		        //alert('<%=GetTran("006026","店铺编号不能为空！") %>');
		        return false;
		    }
		    var returnValue=AjaxClass.CheckStoreID(storeId).value;
		    if(returnValue==false)
		    {
		        document.getElementById("labStoreId").innerHTML =getWebPro().stisno;
		        //alert('<%=GetTran("006817","店铺编号不存在，请重新输入！") %>');
		        return false;
		    }
		}
		
		

      function Check()
      {
//        //判断火狐浏览器
//        if(!window.ActiveXObject)
//        { 
//          document.getElementById("moveDiv").style.marginLeft="0px";
//         }
	  }	 

 
		function menu( menu,img,plus )
		{			
			if( menu.style.display == "none") 
			{
				menu.style.display="";
				img.src="images/foldopen.gif";
				plus.src="images/MINUS2.GIF";		
			}
			else
			{
				menu.style.display = "none";
				img.src="images/foldclose.gif";
				plus.src="images/PLUS2.GIF";				
			}
			
		}
		function GetCCode_s2(city)
		{
		    var sobj = document.getElementById("Txtyb");
		    sobj.value=AjaxClass.GetAddressCode(city).value
		}
		
		
		function  OnChose(selectedValue)
		{
		   // alert(selectedValue);
		   document.getElementById("Txtsb").value=selectedValue;
		   document.getElementById("Divt1").style.display="none";
		}	
		
		function  ShowDiv(selectValue)
		{
		 debugger;   
		  if(selectValue==null)
		  {
		       if(document.getElementById("Divt1").style.display=="none")
		       {
		           document.getElementById("Divt1").innerHTML="";
		           
		           var txtSb=  document.getElementById('<%=Txttj.ClientID%>').value;
		           var topBh = getWebPro().GetTopBh ;
        		   
		           if(txtSb=="")
		           {
//		              document.getElementById("Divt1").innerHTML="<font color=\"red\">请先填写推荐编号!</font>";
                        alert(getWebPro().numtj );
                        return;                        
		           }
		           else
		           {
		               var LoginType = getWebPro().LoginType ;
		               if(LoginType=="Store")
		               {
		                   var isSure = AjaxClass.CheckTuiJian(topBh,txtSb).value;
		                   if(!isSure)
		                   {
		                        alert(getWebPro().numtjs);
                                return;  
		                   }
		               }
	                   var result=AjaxClass.WangLuoTu_FindAnZhi(txtSb.toString()).value;
	                   document.getElementById("Divt1").innerHTML=result;
	                   document.getElementById("Divt1").style.display="block";
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
	 
            var OverTable;
            function ShowViewDiv(sender,number)
            {
                //弹出层
                OverTable=sender;
                OverTable.style.backgroundImage = "url(images/dp20100.gif)";
                
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

                document.getElementById("divShowView").style.left = (sender.offsetLeft + leftpos - document.getElementById("Divt1").scrollLeft - 250 + 50) + "px";
                document.getElementById("divShowView").style.top = (sender.offsetTop + toppos + sender.offsetHeight - 2) + "px";
              //显示网络图
               document.getElementById("divShowView").innerHTML="";
    		   
               if(number=="")
               {
                    document.getElementById("divShowView").style.display="none";
                    return;                        
               }
               else
               {
                   var result=AjaxClass.WangLuoTu_AnZhiShowDiv(number.toString()).value;
                   document.getElementById("divShowView").innerHTML=result;
                   
                   if(result!='<table  border="0" cellspacing="0" cellpadding="0"> <tr valign="top"></td></tr></table></td></tr></table>')
                     document.getElementById("divShowView").style.display = "block";
                   
               }

            }
            
            function HideViewDiv(sender)
            {
                OverTable.style.backgroundImage = "url(images/dp2010.gif)";
                document.getElementById("divShowView").style.display = "none";
            }
      
            function SetAZText()
            {
                if(document.getElementById('Txttj').value=="")
                {                    
//                    document.getElementById('Txtsb').readOnly=true;
//                    document.getElementById('Txtsb').value="";
                    document.getElementById('spanTjtxt').innerHTML = getWebPro().numtj;
                    return;
                }
                else
                    document.getElementById('Txtsb').readOnly=false;                        
                
                document.getElementById('spanTjtxt').innerHTML = "";
            }
            
            function SetAZText2()
            {
                if(document.getElementById('Txttj').value=="")
                {                    
//                    document.getElementById('Txtsb').readOnly=true;
//                    document.getElementById('Txtsb').value="";
                    document.getElementById('spanTjtxt').innerHTML =getWebPro().numtjs;
                    return false;
                }
                else
                    document.getElementById('Txtsb').readOnly=false;                        
                
                document.getElementById('spanTjtxt').innerHTML = "";
                return true;
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
                   var result=AjaxClass.GetProductDetail(pid).value;
                   document.getElementById("divShowProduct").innerHTML=result;
                   
                   if(navigator.appName=="Microsoft Internet Explorer" && navigator.appVersion.split(";")[1].replace(/[ ]/g,"")=="MSIE6.0")
                   { 
                      for(var i=0; i<document.getElementsByTagName("SELECT").length;i++)
                        document.getElementsByTagName("SELECT")[i].style.visibility="hidden";
                   }
               }

            }
            function HideProductDiv(sender)
            {
                document.getElementById("divShowProduct").style.display = "none";
                if(navigator.appName=="Microsoft Internet Explorer" && navigator.appVersion.split(";")[1].replace(/[ ]/g,"")=="MSIE6.0")
                { 
                    for(var i=0; i<document.getElementsByTagName("SELECT").length;i++)
                        document.getElementsByTagName("SELECT")[i].style.visibility="visible";
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
				        alert(getWebPro().pdnum );
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

			var storeid= getWebPro().gsti;

            var curr=document.getElementById("DropCurrency").value;
 
			divPro.innerHTML=AjaxClass.DataBindTxt(pId,storeid,productid,"tablemb","",curr).value;//更新产品表格记录;tabletext;

//		    if(hasChose>0)
//		    {
//		      document.getElementById("lblErr").style.display="none";
//		    }
	    }
	    
	    function  isagree(){
	       var chk= document.getElementById("ckAgree");
	      
	       if(!chk.checked ){
	       alert('如果您不接受此协议，公司将拒绝您注册会员。');
	       return false;
	       }
	       return true;
	        }
            
	    /*
	        Registermember.aspx  end 
	    */