
   function secBoard(n)  
  {
       for(i=0;i<secTable.cells.length;i++)
      secTable.cells[i].className="sec2";
    secTable.cells[n].className="sec1";
    for(i=0;i<mainTable.tBodies.length;i++)
      mainTable.tBodies[i].style.display="none";
    mainTable.tBodies[n].style.display="block";
  }
  
  
  
  
      function aaa()
    {
        for(var i=0;i<form1.elements.length;i++)
        {
            if(form1.elements[i].type=="text")
            {
                if(form1.elements[i].value.indexOf("'")!=-1||form1.elements[i].value.indexOf("=")!=-1)
                {
                    alert('"+<%=GetTran("000481", "查询条件里面不能输入特殊字符")%>+"！');
                    return false;
                }
            }
        }
    }
    
    
    	function FunGetPreSMS()
		{
            var url="SMSselect.aspx";	
		
			var returnValue=window.showModalDialog(url,"","dialogwidth=900px;dialogheight=600px;status=no;center:yes;scroll:no;" );		
			returnValue=decodeURI(returnValue);			
			if(returnValue!=""&&returnValue!=undefined&&returnValue!="undefined")
			{	  
			    if(returnValue.length <=256)
			    {
				  document .getElementById ("txtMsg").value=returnValue;
				}
				else 
				{
				   alert ('<%=BLL.Translation.Translate("006807", "短信内容不能超过256个字符！") %>');
				}
			}
		}
		
		
		
		
			modalWin = ""; 		
		function xShowModalDialog( sURL, vArguments, sFeatures ) 
		{
		  if (sURL==null||sURL=='') 
		  { 
		          alert ("Invalid URL input."); 
		          return false; 
		  } 
		  if (vArguments==null||vArguments=='') 
		  { 
		          vArguments=''; 
		  } 
		  if (sFeatures==null||sFeatures=='') 
		  { 
		          sFeatures=dFeatures; 
		   } 
		  if (window.navigator.appVersion.indexOf("MSIE")!=-1) 
		  { 
		          window.showModalDialog ( sURL, vArguments, sFeatures ); 
		          return false; 
		  } 
		   sFeatures = sFeatures.replace(/ /gi,''); 
		   aFeatures = sFeatures.split(";"); 
		   sWinFeat = "directories=0,menubar=0,titlebar=0,toolbar=0,"; 
    for ( x in aFeatures ) 
    { 
        aTmp = aFeatures[x].split(":"); 
        sKey = aTmp[0].toLowerCase(); 
        sVal = aTmp[1]; 
        switch (sKey) 
        { 
            case "dialogheight": 
                sWinFeat += "height="+sVal+","; 
                pHeight = sVal; 
                break; 
            case "dialogwidth": 
                sWinFeat += "width="+sVal+","; 
                pWidth = sVal; 
                break; 
            case "dialogtop": 
                sWinFeat += "screenY="+sVal+","; 
                break; 
            case "dialogleft": 
                sWinFeat += "screenX="+sVal+","; 
                break; 
            case "resizable": 
                sWinFeat += "resizable="+sVal+","; 
                break; 
            case "status": 
                sWinFeat += "status="+sVal+","; 
                break; 
            case "center": 
                if ( sVal.toLowerCase() == "yes" ) 
                { 
                    sWinFeat += "screenY="+((screen.availHeight-pHeight)/2)+","; 
                    sWinFeat += "screenX="+((screen.availWidth-pWidth)/2)+","; 
                } 
                break; 
        } 
    } 
    modalWin=window.open(String(sURL),"",sWinFeat); 
    if (vArguments!=null&&vArguments!='') 
    { 
        modalWin.dialogArguments=vArguments; 
    } 
} 





    function getPosition(obj)
    {
        var result = 0;
        if(obj.selectionStart)
        { //非IE浏览器
          result = obj.selectionStart
        }
        else
        { //IE
            var rng;
            if(obj.tagName == "TEXTAREA")
            { //如果是文本域
              rng = event.srcElement.createTextRange();
               rng.moveToPoint(event.x,event.y);
            }
           else
           { //输入框
               rng = document.selection.createRange();
           }
           rng.moveStart("character",-event.srcElement.value.length);
           result = rng.text.length;
        }
        return result;
     }

function getValue(obj){
     var objTxt=document .getElementById ("txtMsg");
      var defaultText=objTxt .value;
      if(defaultText!="")
      {
        var pos=0;
        try
        {
         pos  = getPosition(obj);
        }
        catch (e)
       {
         pos=defaultText.length;
       }
     document .getElementById ("txtIndex").value =pos;   }
}

    function FunOperate(opType)
    {
      var objTxt=document .getElementById ("txtMsg");
      var defaultText=objTxt .value;
      var index=Number(document .getElementById ("txtIndex").value );
      var Flag;
      var saveText="";
      if(opType =="0")
      {//保存为新项
         if(defaultText=="")
         {
           alert ("请填写要存储的短语内容！");
           return ;
         }
         else 
         {
           saveText=defaultText;
           __doPostBack("lbtnSaveNew","");
         }
      }
      else  if(opType =="1")
      {//插入编号
        Flag="[NO]";
      }
      else  if(opType =="2")
      {//插入姓名
        Flag="[Name]";
      }
      else  if(opType =="3")
      {//插入称呼
        Flag="[PetName]";
      }
      if(Flag !=null&&Flag !="")
      {
         saveText=defaultText.substr(0,index)+Flag +defaultText.substr(index,defaultText.length);        
      }
      document .getElementById ("txtMsg").value=saveText;
      document .getElementById ("divControl").display="none;";	
    }
    function mOver(obj)
    { 
      for(var i=1;i<=4;i++)
      {
        if(obj.id=="divRow"+i.toString ())
        {
             document .getElementById ("divRow"+i.toString ()).className="divOver";  
        }
        else 
        {
             document .getElementById ("divRow"+i.toString ()).className="divOut";  
        }     
      }
    }
    function mOut(obj)
    {
     obj.className="divOut";  
    }
    function divhidden()
{
   document.getElementById ("divControl").style.display ="none"; 
}
    function showDiv(ObjID)
    {
       var objDiv=document.getElementById ("divControl");
       var objPos=document.getElementById (ObjID);  
       setPos(ObjID);
    }
   
   

function mouseDown(e)
			{
			  var objID;		
			  var objDiv;	
			  var objType;
			  var objElc;
				if(navigator.userAgent.toLowerCase().indexOf('ie')!=-1)
				{
				   objID=e.srcElement.id					   
				}
				else 
				{
				    objID=e.target.id;				  
				}
				
				if(objID!=null&&objID!=undefined&&objID!="")
				{				 
				   objElc=document .getElementById (objID);
				
				}	 
				if(objID!="divControl"&&objID !="btnIndert"&&objID .indexOf('divRow')==-1)
				{
				  objDiv=document .getElementById ("divControl");
				  if(objDiv!=null&&objDiv!=undefined)
				  {

				  divhidden();
				  }				  		 
				}
			}
			
			
			
			function setPos(ObjID)
{
   var e=document.getElementById ("divControl"); 
  e.style.display ="block";
  var arr=getoffset(ObjID); 
  var l=arr[0];
  var t=arr[1];
  var objWidth=document .getElementById (ObjID ).offsetWidth;

  e.style.position='absolute';
  e.style.top=t+'px';                        //外层前景的位置
  e.style.left=objWidth+l+'px';  
}
function getoffset(id) 
{ 
   var e=document.getElementById (id);
   var t=e.offsetTop; 
   var l=e.offsetLeft;   
   while(e=e.offsetParent) 
   { 
      t+=e.offsetTop; 
      l+=e.offsetLeft; 
   } 
    var rec = new Array(1); 
    rec[0] = l; 
    rec[1] = t; 
    return rec 
}

function showDetails(pos,sendNo,smallNo)
{

   var str=AjaxClass.ShowDetailsMsg(sendNo,smallNo).value;	
   var objDiv=document.getElementById ("divControl");
   objDiv.innerHTML=str;
   var objPos=document.getElementById (pos);

   setPos(pos);   

   
}

function divhidden()
{
   document.getElementById ("divControl").style.display ="none"; 
}