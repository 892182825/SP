//------------------------------------------------------------------------功能JS
var Last_className="";
var Last_Row=null;
function SelectRow(Row){
	//如果上次有选中并且不是自己那一条则恢复
	if(Row!=Last_Row){
		//如果为空则设置上次选中的Class
		if(Last_Row==null){
			Last_className="";//Row.cells[0].className;
			Last_Row=Row;
		}else{
			for(i=0;i<Last_Row.cells.length;i++){		
				Last_Row.cells[i].className=Last_className;	
			}
			Last_className=Row.cells[0].className;
			Last_Row=Row;
		}		
		
			
		//设置新的行的CSS为选中
		for(i=0;i<Row.cells.length;i++){		
			Row.cells[i].className="SelectRow";
		}	
	} 
	
}
function FucCheckSingnNum(NUM)
{
    var i,j,strTemp;
    strTemp="0123456789";
    strTemp2="-0123456789";
    if ( NUM.length== 0)
        return 0
    for (i=0;i<NUM.length;i++)
    {
		if(i==0)
			j=strTemp2.indexOf(NUM.charAt(i));
		else
			j=strTemp.indexOf(NUM.charAt(i));
        if (j==-1)
        {
        //说明有字符不是数字
            return 0;
        }
    }
    //说明是数字
    return 1;
}
function RTC(){
 
	var dh = parent.window.dialogHeight;
		while (isNaN(dh))
		{
			dh = dh.substr(0,dh.length-1);
		}
		var dw = parent.window.dialogWidth;
		while (isNaN(dw))
		{
			dw = dw.substr(0,dw.length-1);
		}

		difh = dh - parent.document.body.clientHeight;
		difw = dw - parent.document.body.clientWidth;

	 
		parent.window.dialogHeight = (document.body.scrollHeight + 0 + parseInt(difh , 10)) + 'px';
		parent.window.dialogWidth = (parent.document.body.scrollWidth + parseInt(difw , 10)) + 'px';
		 // parent.window.dialogHeight =document.body.scrollHeight +15+ 'px';
	 	 // parent.window.dialogWidth = document.body.scrollWidth  +15+ 'px';
}
function fucCheckLength(strTemp)
{
    var i,sum;
    sum=0;
    for(i=0;i<strTemp.length;i++)
    {
        if ((strTemp.charCodeAt(i)>=0) && (strTemp.charCodeAt(i)<=255))
            sum=sum+1;
        else
            sum=sum+2;
    }
    return sum;
}
function fucCheckFilename(Filename)
{
	
	var i,j,strTemp;
	j=-1;
	strTemp="/\\:*?\"<>|";
	for (i=0;i<Filename.length;i++){
		j=strTemp.indexOf(Filename.charAt(i));
		 
		if (j==-1)
			continue;
		else
			return 1;//说明不合法
			
	}
    //说明合法
 
		return 0;
}
function fucCheckTEL(TEL)
{
	var i,j,strTemp;
	strTemp="0123456789-()#呼转 ";
	for (i=0;i<TEL.length;i++)
	{
		j=strTemp.indexOf(TEL.charAt(i));
		if (j==-1)
		{
		//说明有字符不合法
			return 0;
		}
		}
    //说明合法
    return 1;
}
function fucCheckNUM(NUM)
{
    var i,j,strTemp;
    strTemp="012345678.9";
    if ( NUM.length== 0)
        return 0
    for (i=0;i<NUM.length;i++)
    {
        j=strTemp.indexOf(NUM.charAt(i));
        if (j==-1)
        {
        //说明有字符不是数字
            return 0;
        }
    }
    //说明是数字
    return 1;
}
function fucCheck_singn_NUM(NUM)
{
    var i,j,strTemp;
    strTemp="0123456789-";
    if ( NUM.length== 0)
        return 0
    for (i=0;i<NUM.length;i++)
    {
        j=strTemp.indexOf(NUM.charAt(i));
        if (j==-1)
        {
        //说明有字符不是数字
            return 0;
        }
    }
    //说明是数字
    return 1;
}
function fucCheckNO(NO)
{
    var i,j,strTemp;
    strTemp="ABCDEFGHabcdefgh0123456789";
    if ( NO.length== 0)
        return 0
    for (i=0;i<NO.length;i++)
    {
        j=strTemp.indexOf(NO.charAt(i));
        if (j==-1)
        {
        //说明有字符不是数字
            return 0;
        }
    }
    //说明是数字
    return 1;
}
function fucCheckPartNO(NO)
{
    var i,j,strTemp;
    strTemp="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-.";
    if ( NO.length== 0)
        return 0
    for (i=0;i<NO.length;i++)
    {
        j=strTemp.indexOf(NO.charAt(i));
        if (j==-1)
        {
        //说明有字符不是数字
            return 0;
        }
    }
    //说明是数字s
    return 1;
}
function fucCheckMemberNO(NO)
{
    var i,j,strTemp;
    strTemp="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    if ( NO.length== 0)
        return 0
    for (i=0;i<NO.length;i++)
    {
        j=strTemp.indexOf(NO.charAt(i));
        if (j==-1)
        {
        //说明有字符不是数字
            return 0;
        }
    }
    //说明是数字
    return 1;
}
function fucCheckEmailNO(NO)
{
    var i,j,strTemp;
    strTemp="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_.";
    if ( NO.length== 0)
        return 0
    for (i=0;i<NO.length;i++)
    {
        j=strTemp.indexOf(NO.charAt(i));
        if (j==-1)
        {
        //说明有字符不是数字
            return 0;
        }
    }
    //说明是数字
    return 1;
}
//+++++++++++++++检查e-mail的合法性
function chkemail(a)
{
	var i=a.length;
	var temp = a.indexOf('@');
	var tempd = a.indexOf('.');
	if (temp > 1) {
		if ((i-temp) > 3){
			if ((i-tempd)>0){
				return 1;
			}
		}
	}
	return 0;
}
//-----------------------------------------------------------------------
var marked_row = new Array;
function setPointer(theRow, theRowNum, theAction, theDefaultColor, thePointerColor, theMarkColor)
{
    var theCells = null;

    // 1. Pointer and mark feature are disabled or the browser can't get the
    //    row -> exits
    if ((thePointerColor == '' && theMarkColor == '')
        || typeof(theRow.style) == 'undefined') {
        return false;
    }

    // 2. Gets the current row and exits if the browser can't get it
    if (typeof(document.getElementsByTagName) != 'undefined') {
        theCells = theRow.getElementsByTagName('td');
    }
    else if (typeof(theRow.cells) != 'undefined') {
        theCells = theRow.cells;
    }
    else {
        return false;
    }

    // 3. Gets the current color...
    var rowCellsCnt  = theCells.length;
    var domDetect    = null;
    var currentColor = null;
    var newColor     = null;
    // 3.1 ... with DOM compatible browsers except Opera that does not return
    //         valid values with "getAttribute"
    if (typeof(window.opera) == 'undefined'
        && typeof(theCells[0].getAttribute) != 'undefined') {
        currentColor = theCells[0].getAttribute('bgcolor');
        domDetect    = true;
    }
    // 3.2 ... with other browsers
    else {
        currentColor = theCells[0].style.backgroundColor;
        domDetect    = false;
    } // end 3

    // 4. Defines the new color
    // 4.1 Current color is the default one
    if (currentColor == ''
        || currentColor.toLowerCase() == theDefaultColor.toLowerCase()) {
        if (theAction == 'over' && thePointerColor != '') {
            newColor              = thePointerColor;
        }
        else if (theAction == 'click' && theMarkColor != '') {
            newColor              = theMarkColor;
        }
    }
    // 4.1.2 Current color is the pointer one
    else if (currentColor.toLowerCase() == thePointerColor.toLowerCase()
             && (typeof(marked_row[theRowNum]) == 'undefined' || !marked_row[theRowNum])) {
        if (theAction == 'out') {
            newColor              = theDefaultColor;
        }
        else if (theAction == 'click' && theMarkColor != '') {
            newColor              = theMarkColor;
            marked_row[theRowNum] = true;
        }
    }
    // 4.1.3 Current color is the marker one
    else if (currentColor.toLowerCase() == theMarkColor.toLowerCase()) {
        if (theAction == 'click') {
            newColor              = (thePointerColor != '')
                                  ? thePointerColor
                                  : theDefaultColor;
            marked_row[theRowNum] = (typeof(marked_row[theRowNum]) == 'undefined' || !marked_row[theRowNum])
                                  ? true
                                  : null;
        }
    } // end 4

    // 5. Sets the new color...
    if (newColor) {
        var c = null;
        // 5.1 ... with DOM compatible browsers except Opera
        if (domDetect) {
            for (c = 0; c < rowCellsCnt; c++) {
                theCells[c].setAttribute('bgcolor', newColor, 0);
            } // end for
        }
        // 5.2 ... with other browsers
        else {
            for (c = 0; c < rowCellsCnt; c++) {
                theCells[c].style.backgroundColor = newColor;
            }
        }
    } // end 5

    return true;
} // end of the 'setPointer()' function
function Dialogreload(url,width,height){
 try{
var a=showDialogary(url,width,height);
 
if(a==1){
 
	location.href=location.href;
 
}
}catch(e){}
}
function Dialogsearch(url,width,height){
 try{
var a=showDialogary(url,width,height);
 
if(a!=undefined){

	if(a.substr(0,1)=="?")
		location.href=a; 
	else
	//	return false;
	location.href=a; 
 
}
}catch(e){}
}
 function SearchsendTo(query){
  window.returnValue =query+"&search=1"; 
  window.close()
 }
 function uploadhit(){
	HiwavoDiv.style.display='';
 
 }
 

function GetDialog(url,arry,width,height){
		var arry=window.showModalDialog(url,arry,"scroll:no;status:0;help:0;resizable:1;dialogWidth:"+width+"px;dialogHeight:"+height+"px");
	return arry;
} 
 
function showDialog(url,width,height){
	window.showModalDialog(url,"","scroll:0;status:no;help:0;resizable:1;dialogWidth:"+width+"px;dialogHeight:"+height+"px");
}
function openUrl(Url,top,left,width,height){
window.open(Url,'','scrollbars=yes,width='+width+',height='+height+',top='+top+',left='+left)
}
function openDialog(url,width,height){ 
		window.open(url ,window," status:no;help:no;resizable:no;scroll:no;dialogHeight:"+height+"px;dialogWidth:"+width+"px");
 }
function showDialogary(url,width,height){
	var arry=window.showModalDialog(url,"","scroll:0;status:0;help:0;resizable:1;dialogWidth:"+width+"px;dialogHeight:"+height+"px");

	return arry;
}
function MM_openBrWindow(theURL,winName,features) { //v2.0
  window.open(theURL,winName,features);
}
function MM_callJS(jsStr) { //v2.0
  return eval(jsStr)
}
  function del(url,text){
    if(confirm(text)){
	   location.replace(url);
	}else 
    return false;	
  }
  function agree()
{
  if(confirm('确定删除该所选记录？'))
    return true;
  else 
    return false;	
}
 
function checkall(form)
 {
  for (var i=0;i<form.elements.length;i++)
    {
    var e = form.elements[i];
    if (e.name != 'chkall')
       e.checked = form.chkall.checked;
    }
  }
 function MU(e)
{
if (!e)
var e=window.event;
var S=e.srcElement;
while (S.tagName!="TD")
{S=S.parentElement;}
S.className="P";
}
function MO(e)
{
if (!e)
var e=window.event;
var S=e.srcElement;
while (S.tagName!="TD")
{S=S.parentElement;}
S.className="T";
}
function Subm(act,first,dosub,e)
{
if (act=='delete')
{
if (!e)	var e=window.event;
e.cancelBubble=true;
}
if (act=='notbulkmail')
frm.action="/cgi-bin/notbulk";
else if (act=='blocksender')
{
frm.action="/cgi-bin/kill";
frm.ReportLevel.value="0";
}
else if (act=='report')
{
frm.action="/cgi-bin/kill";
frm.ReportLevel.value="1";
}
else if (act=='report_n_block')
{
frm.action="/cgi-bin/kill";
frm.ReportLevel.value="2";
}
num=((first) ? slct1st(frm) : numChecked(frm));
if (num>0)
{
frm._HMaction.value=act;
if (dosub)
frm.submit();
}
else
Err("150995653");
}
 
function administrator_check(){
	if(form1.bas_name.value==""){
	    alert('用户名不能为空！');
		return false;
	}
		if(form1.bas_password.value==""){
	    alert('密码不能为空！');
		return false;
	}else{
	    if(form1.bas_password.value!=form1.re_password.value){
	     alert('两次密码不一致，请重新输入！');
		return false;
		}
	
	
	}


}

//----------------------------------表情
function appEmotionStr(num) {
	document.all.bas_info.value += e_arr[num];
}
function showEmotionImg() {
	document.write("<table width=100% border=0 cellspacing=0 cellpadding=0 class=font>");
	var cols = 16;
	var rows = 4;
	for (i=0; i<rows; i++) {
		document.write("<tr align=center>");
		j = i*cols;
		for (; j<i*cols + cols; j++) {
			var tips = e_arr[j];
			document.write("<td height=25><img src=/Hiwavo/Images/Face/" + j + ".gif   style='cursor:hand' title='" + tips + "' onclick=appEmotionStr(" + j + ")></td>");
		}
		document.write("</tr>");
	}
	document.write("</table>");
	
}
//////////////////////// emotion func below ////////////////////////

var emotion_shortcut = ":) #_# 8*) :D :\S> :P B_) B_I ^_* :$ :| :( :.( :_( >:( :V *_* :^ :? :! =:| :% :O :X |-) :Z :9 :T :-* *_/ :#| :69 //shuang //qiang //ku //zan //heart //break //F //W //mail //strong //weak //share //phone //mobile //kiss //V //sun //moon //star (!) //TV //clock //gift //cash //coffee //rice //watermelon //tomato //pill //pig //football //shit";
var e_arr = emotion_shortcut.split(" ");

var Emotion_Num = e_arr.length;

var EmotionArray = new Array(Emotion_Num);

for (i=0; i<Emotion_Num; i++) {
	var idx = e_arr[i];
	EmotionArray[idx] = i;
}

var abs_path    =       "<img align='absmiddle' src=/Hiwavo/Images/Face/";
var suffix      =       ".gif border=0>";

function getEmotion(idx) {
        document.write(abs_path + EmotionArray[idx] + suffix);
}

function doStr(src) {
	var quote = /(:\)|\#_\#|8\*\)|:\S>|:D|:P|B_\)|B_I|\^_\*|:\$|:\||:\(|:\.\(|:_\(|>:\(|:V|\*_\*|:\^|:\?|:\!|\=:\||:%|:O|:X|\|-\)|:Z|:9|:T|:-\*|\*_\/|:#\||:69|\/\/shuang|\/\/qiang|\/\/ku|\/\/zan|\/\/heart|\/\/break|\/\/F|\/\/W|\/\/mail|\/\/strong|\/\/weak|\/\/share|\/\/phone|\/\/mobile|\/\/kiss|\/\/V|\/\/sun|\/\/moon|\/\/star|\(\!\)|\/\/TV|\/\/clock|\/\/gift|\/\/cash|\/\/coffee|\/\/rice|\/\/watermelon|\/\/tomato|\/\/pill|\/\/pig|\/\/football|\/\/shit)/g;
	var src = src.replace(quote, "<script" + ">getEmotion('$1')</" + "script>");
	document.write(src);
}

function doFlatTxt(src) {       
 
		var quote = /(:\)|\#_\#|8\*\)|:\S>|:D|:P|B_\)|B_I|\^_\*|:\$|:\||:\(|:\.\(|:_\(|>:\(|:V|\*_\*|:\^|:\?|:\!|\=:\||:%|:O|:X|\|-\)|:Z|:9|:T|:-\*|\*_\/|:#\||:69|\/\/shuang|\/\/qiang|\/\/ku|\/\/zan|\/\/heart|\/\/break|\/\/F|\/\/W|\/\/mail|\/\/strong|\/\/weak|\/\/share|\/\/phone|\/\/mobile|\/\/kiss|\/\/V|\/\/sun|\/\/moon|\/\/star|\(\!\)|\/\/TV|\/\/clock|\/\/gift|\/\/cash|\/\/coffee|\/\/rice|\/\/watermelon|\/\/tomato|\/\/pill|\/\/pig|\/\/football|\/\/shit)/g;
		var src = src.replace(quote, "<script" + ">getEmotion('$1')</" + "script>");
		document.write(src);
	 
}
//////////////////////// Image to Symbol ////////////////////////

var imgToSymbol = new Array(Emotion_Num);

for (var i in EmotionArray) {
	var idx = "<IMG src=\"../Images/Face/" + EmotionArray[i] + ".gif\" border=0>";
	imgToSymbol[idx] = i;
}

function imgToSym(src) {
        var quote = /<IMG src=\"images\/([0-9][0-9]?)\.gif\" border=0>/g;
        var src = src.replace(quote, function ($1) {return imgToSymbol[$1];});
        return src;
}

//////////////////////// show func below ////////////////////////

function check_keyword(){
	if(form1.keyword.value==""){
		alert("请输入搜索关键字！");
		form1.keyword.focus();
		return false;	
	}

}

function checkvip(Open,Replace){
	if(Open==0){
		alert("您暂时还不能使用该功能。因为您的VIP服务已到期，请您先申请激活！");
		if(Replace==0)
			return false;
		else
			history.back();
	}

}
var timer;
function goBack(){ 
	clearInterval(timer); 
    location.replace("../Play/");
}
function vipback(Open){
	if(Open==0){	
	  timer=setInterval("goBack()",100000); 
	}
}

//
function setZF(id,len)
        {
            var th=document.getElementById(id);
            
            if(th.value.length>=len)
            {
                th.value=th.value.substring(0,len-1);
                
                th=null;
            }
            
            setTimeout("setZF('"+id+"','"+len+"')",50);
        }
