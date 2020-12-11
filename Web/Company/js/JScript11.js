 if(navigator.userAgent.toLowerCase().indexOf("ie")!=-1)
    {
        window.attachEvent("onload",page_load);
    }
    else
        window.addEventListener("load",page_load,false);
    
    function page_load()
	{
	    page_load();
	    down2();
	}
			
			var newEditWin=0;
			function openCountWin(qs) 
			{
			
				var param='toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbar=no,resizable=no,copyhistory=no,width=500,height=400,left=300, top=200,screenX=100,screenY=0';
			
				if( newEditWin ){
						if( !newEditWin.closed ){
							window.alert(aa[3]);
							newEditWin.focus();
		        			return;
		        			}
		 		}
				var sure = window.confirm(aa[0]+aa[1]+qs+aa[2]);
				if(sure){
				if(AjaxClass.GetIsExistsConfig(qs).value==false)
				{
				    alert(aa[4]);
					return;
				}
				//window.open("BalanceBegin.aspx?action=begin&qs=" + qs,'nW', param);
				    newEditWin = window.open("BalanceRunning_Proc.aspx?action=begin&qs=" + qs, 'nW', param);
					if(newEditWin==null)
					{
					alert(aa[5]);
					return;
					}
					newEditWin.focus();
					window.location .href =window.location .href ;
				}
												
			}
			function confimAddNew()
			{
				if(window.confirm(aa[6])) return true;
				else return false;
			
			
			}	

function ck(th)
			{
				if(th.checked)
				{
					//document.getElementById("zdjsid").style.display="";
					document.getElementById("Textbox4").value="y";
				}
				else
				{
					//document.getElementById("zdjsid").style.display="none";
					document.getElementById("Textbox4").value="n";
				}
			}
			
			function setCk()
			{
				document.getElementById("CheckBox3").checked=true;
				ck(document.getElementById("CheckBox3"));
			}
			
			function enabledCK()
			{
				document.getElementById("CheckBox3").disabled=true;
			}
			
			
			var t2hm;
			var sshm;

			function page_load()
			{
				var t2=document.getElementById("shijid").value.replace(/-/g,"/")//"2010/06/15 10:20:08"; //下一个结算时间
				t2hm=new Date(t2).getTime();

				var nowtime=new Date(document.getElementById("nowtimeid").value.replace(/-/g,"/"));  //现在时间"2010/06/12 9:10:00"
				sshm=nowtime.getTime();

				setDJS();
			}
			
			function setDJS()
			{
				var nowhm=sshm;

				var ts=parseInt((t2hm-nowhm)/(1000*60*60*24));

				//alert(ts)

				nowhm=nowhm+ts*24*60*60*1000;
				var xs=parseInt((t2hm-nowhm)/(1000*60*60));

				//alert(xs)

				nowhm=nowhm+xs*60*60*1000;
				var fs=parseInt((t2hm-nowhm)/(1000*60));

				//alert(fs)

				nowhm=nowhm+fs*60*1000;
				var ms=parseInt((t2hm-nowhm)/(1000));

				//alert(ms)
				
				if(ts+""=="NaN")
                    ts="0";
                if(xs+""=="NaN")
                    xs="0";
                if(fs+""=="NaN")
                    fs="0";
                if(ms+""=="NaN")
                    ms="0";
                    
				document.getElementById("lzdjs").value=ts+" 天 "+xs+" 时 "+fs+" 分 "+ms+" 秒";

				sshm=sshm+1000;

				setTimeout("setDJS()",1000);
			}