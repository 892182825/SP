
    var istw=false;
    var twbianhao;
    
    function settw(e)
    {
        if(confirm("你确定 "+twbianhao+" 要调网吗？"))
        {
            istw=true;
            document.getElementById("tr21").style.display="none";
            document.getElementById("tr22").style.display="";
            
            document.getElementById("yd_id").innerHTML=twbianhao;
            
            document.getElementById("yd_id").style.display="";
        }
        
        document.getElementById("menus").style.display="none";
    }
    
    function untw()
    {
        istw=false;
        isFirst=true;
        document.getElementById("tr21").style.display="";
        document.getElementById("tr22").style.display="none";
        
        document.getElementById("yd_id").style.display="none";
    }
    
    function down_tw(e,th)
    {
        if(e.button==1 || e.button==0)
        {
            if(istw)
            {
                //alert(th.id.substring(2,th.id.length));
                
                //window.status=th.id.substring(2,th.id.length);
                
                var id=th.id.substring(2,th.id.length);
                var btid=document.getElementById("yd_id").innerHTML;
                    
                if(confirm("你确定要把编号 "+btid+" 及他的团队调到编号 "+id+" 下面吗？"))
                {
                    setAjaxTW("LayerBit1","Ordinal1",btid,id,'<%=ViewState["qs"]%>','<%=ViewState["cengshu"]%>','<%=ViewState["startbh"]%>','<%=ViewState["isanzhi"]%>');
                }
                /*else
                    document.getElementById("zz_id").style.display="none";*/
            }
        }
    }
    
    function aaa(bh,qishu)
    {
        debugger;
        var bianhao = bh.toString().substring(1,bh.toString().length);
        var str=AjaxClass.WangLuoTuNew(bianhao.toString(),qishu.toString()).value;
        var start0 = document.getElementById('statr0');
        start0.innerHTML = str;
        document.getElementById('fuDong').style.display='none';
        document.getElementById('txtbianhao').value = bianhao;
        
        var divstr=AjaxClass.GetDaoHang(bianhao,qishu.toString(),document.getElementById("txtceng").value,'1').value;
        divDH.innerHTML=divstr;
    }
    
    

function DataBind()
{
    var topBh = document.getElementById('txtbianhao').value;
    var manageId = '<%=GetManageId() %>';
    
    var isTop = AjaxClass.GetIsTop(topBh.toString(),0,manageId.toString()).value;
    
    if(isTop=="1")
    {
         document.getElementById('top'+topBh).style.color='red';
    }

    //debugger;
    var lab1 = document.getElementById('labField1');
    var lab2 = document.getElementById('labField2');
    var lab3 = document.getElementById('labField3');
    var lab4 = document.getElementById('labField4');
    var lab5 = document.getElementById('labField5');

    var t1 = document.getElementById('tr1');
    var t2 = document.getElementById('tr2');
    var t3 = document.getElementById('tr3');
    var t4 = document.getElementById('tr4');
    var t5 = document.getElementById('tr5');

    var dispalyText = AjaxClass.GetDisplayBind().value;
    var aArray = dispalyText .toString().split(',');
    var count = 0;
    t1.style.display = "none";
    t2.style.display = "none";
    t3.style.display = "none";
    t4.style.display = "none";    
    t5.style.display = "none";
    var i=0;

    for(i=0;i<aArray.length;i++)
    {
        switch(i)
        {
            case 0:
                lab1.innerHTML = aArray[0];
                t1.style.display = "";
                break;
            case 1:
                lab2.innerHTML = aArray[1];
                t2.style.display = "";
                break;
           case 2:
                lab3.innerHTML = aArray[2];
                t3.style.display = "";
              break;
            case 3:
                lab4.innerHTML = aArray[3];
                t4.style.display = "";
                break;
            case 4:
                lab5.innerHTML = aArray[4];
                t5.style.display = "";
                break;
            default:
                break;
        }
    }
}

function fun1(bh,flag,qishu)
{
   // debugger;
	document.getElementById('menus').style.display="none";	
	var bianhao = bh.toString().substring(1,bh.toString().length);
	switch(flag)
	{
	    case 1://变色处理代码放在这

	        document.getElementById(bh).style.color="Blue";
	        break;

	    case 2://注册处理代码放在这	
	        window.location.href="../Store/RegisterMember.aspx?type=1&bh="+bianhao;
	        break;

	    case 3://审核处理代码放在这

	        break;

	    case 4://删除处理代码放在这

            var sjBh = AjaxClass.GetSjBh(bianhao,true).value;

            //删除会员
            if(confirm('您确定要删除'+bianhao+'会员吗？'))
            {
	            var msg = AjaxClass.DeleteMember(bianhao).value;
	            alert(msg);
	            if(msg != '<%=GetTran("000008", "删除成功")%>')
	            {	            
	                return;
	            }
	        }
	        else
	        {
	            return;
	        }
            //刷新网络图
//            var qs= AjaxClass.GetMaxQishu().value; 

//            var StrDiv=document.getElementById(sjBh);
//            var statr0 = document.getElementById("statr0");

//            var id = "n"+sjBh;
//            tree = document.getElementById(id).tree;
//	        var str=AjaxClass.WangLuoTu2(sjBh,tree,qs).value;
//	        StrDiv.innerHTML=str;

//            if(tree=="")  
//            {
//               statr0.innerHTML="<div id='"+bianhao+"'>"+StrDiv.innerHTML+"</div>"; 				       
//            }
           // alert('2222')
            getDataII('<%=ViewState["startbh"]%>','<%=ViewState["cengshu"]%>','<%=ViewState["qs"]%>','<%=ViewState["isanzhi"]%>','sxaz');
          //  alert('333')

	        break;

	    case 5://调网处理代码放在这

	        window.location.href="tw/ChangeTeam2.aspx?bh="+bianhao;
	        break;

	    case 6://显示内容1 处理代码放在这

	        var labText = document.getElementById('labField1');
	        var StrDiv = document.getElementById(bianhao);
	        var count = AjaxClass.GetDiv(StrDiv.innerHTML,bianhao).value;
	        if(count>0)
	        {
	            StrDiv = document.getElementById('parent_'+bianhao);
	        }

	        var display = AjaxClass.GetDisplay(bianhao,labText.innerHTML.toString(),qishu).value;
	        StrDiv.innerHTML += "<span class=bs title='"+labText.innerHTML.toString()+"'>["+display+"]</span>";
	        break;

	    case 7://显示内容2 处理代码放在这

	        var labText = document.getElementById('labField2');
	        var StrDiv = document.getElementById(bianhao);
	        var count = AjaxClass.GetDiv(StrDiv.innerHTML,bianhao).value;
	        if(count>0)
	        {
	            StrDiv = document.getElementById('parent_'+bianhao);
	        }

	        var display = AjaxClass.GetDisplay(bianhao,labText.innerHTML.toString(),qishu).value;
	        StrDiv.innerHTML += "<span class=bs title='"+labText.innerHTML.toString()+"'>["+display+"]</span>";
	        break;

	    case 8://显示内容3 处理代码放在这

	        var labText = document.getElementById('labField3');
	        var StrDiv = document.getElementById(bianhao);
	        var count = AjaxClass.GetDiv(StrDiv.innerHTML,bianhao).value;
	        if(count>0)
	        {
	            StrDiv = document.getElementById('parent_'+bianhao);
	        }

	        var display = AjaxClass.GetDisplay(bianhao,labText.innerHTML.toString(),qishu).value;
	        StrDiv.innerHTML += "<span class=bs title='"+labText.innerHTML.toString()+"'>["+display+"]</span>";
	        break;

	    case 9://显示内容4 处理代码放在这

	        var labText = document.getElementById('labField4');
	        var StrDiv = document.getElementById(bianhao);
	        var count = AjaxClass.GetDiv(StrDiv.innerHTML,bianhao).value;
	        if(count>0)
	        {
	            StrDiv = document.getElementById('parent_'+bianhao);
	        }
	        var display = AjaxClass.GetDisplay(bianhao,labText.innerHTML.toString(),qishu).value;
	        StrDiv.innerHTML += "<span class=bs title='"+labText.innerHTML.toString()+"'>["+display+"]</span>";
	        break;
	    case 10://显示内容5 处理代码放在这
	       var labText = document.getElementById('labField5');
	        var StrDiv = document.getElementById(bianhao);
	        var count = AjaxClass.GetDiv(StrDiv.innerHTML,bianhao).value;
	        if(count>0)
	        {
	            StrDiv = document.getElementById('parent_'+bianhao);
	        }

	        var display = AjaxClass.GetDisplay(bianhao,labText.innerHTML.toString(),qishu).value;
	        StrDiv.innerHTML += "<span class=bs title='"+labText.innerHTML.toString()+"'>["+display+"]</span>";
	        break;
	    case 11:
	        var StrDiv = document.getElementById('n'+bianhao);
	        StrDiv.innerHTML = "<span class=js title ='图标1'><img src='images/icon_6-.gif' width='12' height='12' /></span>" + StrDiv.innerHTML;
	        break;
	    case 12:
	        var StrDiv = document.getElementById('n'+bianhao);
	        StrDiv.innerHTML = "<span class=js title ='图标1'><img src='images/icon_9-.gif' width='12' height='12' /></span>" + StrDiv.innerHTML;
	        break;
	    case 13:
	        var StrDiv = document.getElementById('n'+bianhao);
	        StrDiv.innerHTML = "<span class=js title ='图标1'><img src='images/icon_11-.gif' width='12' height='12' /></span>" + StrDiv.innerHTML;
	        break;
	        
	    case 14:
	        var StrDiv = document.getElementById('n'+bianhao);
            var abc = StrDiv.getElementsByTagName("span").length;
			for(var i=0;i<abc;i++)
			{
			    if(i==0)
			    {
				    StrDiv.removeChild(StrDiv.firstChild);
				}
			}
	        break;
	    case 15:
	        var nState = AjaxClass.GetNumberState(bianhao,qishu.toString()).value;
	       // alert(nState);
	        if(nState=="1")
	        {
	            document.getElementById(bh).style.color="black";
	        }
	        else if(nState=="4")
	        {
	            document.getElementById(bh).style.color="Silver";
	        }
	        else if(nState=="2")
	        {
	            document.getElementById(bh).style.color="#FF8686";
	        } 
	        else if(nState=="3")
	        {
	            document.getElementById(bh).style.color="#E30000";
	        }	        
	        break;
       case 16:
            if(confirm('确定注销'+bianhao+'会员吗？'))
            {
                var manager = '<%=Session["Company"].ToString() %>';
                //alert(manager);
                var content = AjaxClass.GetMemberOff(bianhao,manager).value;
	            // window.location.href="MemberOff.aspx?bh="+bianhao;
	            alert(content);
	        }
	        break;
	     case 17:
                window.location.href="MemberInfoModify.aspx?id="+bianhao;
	       
	        break;
	    default:
	        alert('"+<%=GetTran("000843", "选择错误")%>+"！');
	        break;
	}
}

function JSNET(thisObj,bianhao,tree,flag,qishu,cw)
{
    var StrDiv=document.getElementById(bianhao);
    var statr0 = document.getElementById("statr0");
    var bh = bianhao.substring(5,bianhao.length);
    if(flag==0)
    {
        //下面代码根据实际修改.replace(/ /g,"<img src='../images/03.gif' \>")
        document.getElementById(bianhao).firstChild.src = "../images/014.gif";
        var spanId = AjaxClass.getXHFW(bh,true,qishu).value;
       // alert(spanId);
        var sId = spanId.split(",");
        for(var i=0;i<sId.length;i++)
        {
            if(document.getElementById("tr"+sId[i].toString())!=null && sId[i].toString()!=bh)
            {
                document.getElementById("tr"+sId[i].toString()).style.display = "none";                
            }
        }
        //debugger;
        document.getElementById("span_"+bh).onclick=function()
        {
             JSNET(document.getElementById("span_"+bh),document.getElementById("span_"+bh).id,document.getElementById("span_"+bh).attributes['tree'].value,1,qishu,cw) ;
        };

      //  var str=AjaxClass.WangLuoTu_str2(bianhao,qishu,tree).value;	
        //tree.replace(/ /g,"&nbsp;&nbsp;")+"<span class=ss id='"+bianhao+"' tree='"+tree+"'  ONCLICK=JSNET(this.id,this.tree,1,"+qishu+")>田<span class=bh id="+bianhao+" onMouseUp=popUp(this.id)>"+bianhao+"</span>"+str;	       
       // StrDiv.innerHTML=tree+str;
       // if(tree=="")  
       // {
	   //    statr0.innerHTML="<div id='"+bianhao+"'>"+StrDiv.innerHTML+"</div>";
	   // }
	   
	   
    }
    else
    {
    //debugger;
        var cengshu = '<%=GetCengshu() %>';
        var cengs = cw ;//+ 1;
        var htm = AjaxClass.GetHtml(bh,tree,qishu,cengshu,true,cengs.toString()).value;

        $("#tr"+bh).after(htm);

        document.getElementById(bianhao).firstChild.src = "../images/013.gif";

        document.getElementById("span_"+bh).onclick=function()
        {
             JSNET(document.getElementById("span_"+bh),document.getElementById("span_"+bh).id,document.getElementById("span_"+bh).attributes['tree'].value,0,qishu,cw) ;
        };

	    //利用AJAX调用存储过程得到返回的内容为
	//    alert('展开'+bianhao);
//	    var str=AjaxClass.WangLuoTu2(bianhao,tree,qishu).value;
//	    StrDiv.innerHTML=str;

//        StrDiv.innerHTML=tree.replace(/\ /g,"&nbsp;&nbsp;")+"<span class='ls1' style='CURSOR: hand' id='"+bianhao+"' tree='"+tree+"'  ONCLICK=JSNET(this.id,this.tree,3,"+qishu+")>田</span>"+"&nbsp;<span id='"+bianhao+"' class='ls' style='CURSOR: hand' title='编号和姓名'  ONCLICK=JSNET(this.id,'',3,"+qishu+")>"
//                         +str;                                                   	         			                                                           

//        if(tree=="")  
//        {
//           statr0.innerHTML="<div id='"+bianhao+"'>"+StrDiv.innerHTML+"</div>"; 				       
//        }

          
          //tw();
    }
    

       var jo = "j";
        $("#tab_tr tr").each(function() {
                var thisObj = $(this);
                if (thisObj.css("display") != "none") 
                {
                    if (jo == "j") {
                        thisObj.css("background-color", "#F1F4F8");
                        jo = "0";
                    } else {
                        thisObj.css("background-color", "#FAFAFA");
                        jo = "j";
                    }
                }
            })
            
//          if(flag==0)
//            {
//                 $("#tab_tr tr").each(function() {
//                     var thisObj = $(this);
//                     if (thisObj.css("display") == "none") 
//                     {
//                         document.getElementById("tab_tr").removeChild(thisObj);
//                     }
//                 })
//             }
}

//<!-----------------生成快捷菜单脚本------------------------->
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
            menu.style.top = newY - menu.offsetHeight+"px";
        }
    //    menu.style.pixelLeft = newX + "px"
    //    menu.style.pixelTop = newY + "px"
        menu.bh=id
        menu.Qishu=Qishu
        
        if(document.getElementById(id).style.color=="blue")
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
        function ViewportInfo() {
            var w = (window.innerWidth) ? window.innerWidth : (document.documentElement && document.documentElement.clientWidth) ? document.documentElement.clientWidth : document.body.offsetWidth;
            var h = (window.innerHeight) ? window.innerHeight : (document.documentElement && document.documentElement.clientHeight) ? document.documentElement.clientHeight : document.body.offsetHeight;
            return { width: w, height: h };
        };
        function ScrollPosition() {
            var top = 0, left = 0, width = 0, height = 0;
            if (document.documentElement && document.documentElement.scrollTop) {
                top = document.documentElement.scrollTop;
                left = document.documentElement.scrollLeft;
                width = document.documentElement.scrollWidth;
                height = document.documentElement.scrollHeight;
            } else if (document.body) {
                top = document.body.scrollTop;
                left = document.body.scrollLeft;
                width = document.body.scrollWidth;
                height = document.body.scrollHeight;
            };
            return { top: top, left: left, width: width, height: height };
        };

    document.documentElement.onclick=function()      
    {
      //    debugger;
      //  if(WebCalendar.eventSrc != window.event.srcElement)
             document.getElementById('menus').style.display="none"; 
             document.getElementById('fuDong').style.display='none';
    }
    

   var message="";
   function clickIE()
   {
    if (document.all)
    {
     (message);
     return false;
    }
   }
   function clickNS(e)
   {
    if (document.layers||(document.getElementById&&!document.all))
    {
     if (e.which==2)
     {
      newX = window.event.x + document.body.scrollLeft
      newY = window.event.y + document.body.scrollTop
      menu = document.all.itemopen
      if ( menu.style.display == "")
      {
       menu.style.display = "none"
      }
      else
      {
       menu.style.display = ""
      }
      menu.style.pixelLeft = newX
      menu.style.pixelTop = newY
     }
     if (e.which==3)
     {
      (message);
      return false;
     }
    }
   }
   if (document.layers)
   {
    document.captureEvents(Event.MOUSEDOWN);
    document.onmousedown=clickNS;
   }
   else
   {
    document.onmouseup=clickNS;document.oncontextmenu=clickIE;
   }
   document.oncontextmenu=new Function("return false")



		    function ShowView(abc)
		    {
		        document.getElementById("txtbianhao").value = abc.firstChild.nodeValue;
		        
		        //document.getElementById("lkn_submit1").click();
		        __doPostBack('lkn_submit1','');
		        
		    }


var thColor='';
function overIt(th)
{	
    thColor = th.style.backgroundColor;
    th.style.backgroundColor='#FFFFCC';	
}
function outIt(th)
{	
    th.style.backgroundColor=thColor;	
}



