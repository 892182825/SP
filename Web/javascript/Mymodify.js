
//日历
var date_start,date_end,g_object
var today = new Date();
var separator="-";
var inover=false;
var _Mon = "一";
var _Tue = "二";
var _Wed = "三";
var _Thu = "四";
var _Fri = "五";
var _Sat = "六";
var _Sun = "日";

//获得多语言的星期提示
function GetDatePickerDayString()
{
	if (document.getElementById('datepicker1:txt_DatePickerDayString')!=null)
	{
		var DatePickerDayString  = document.getElementById('datepicker1:txt_DatePickerDayString').value;
		var a = DatePickerDayString.split(',');
		_Sun = a[0];
		_Mon = a[1];
		_Tue = a[2];
		_Wed = a[3];
		_Thu = a[4];
		_Fri = a[5];
		_Sat = a[6];
		var tr = window.cele_date.children[0].children[0].children[1];
		tr.children[0].children[0].innerText = _Sun;
		tr.children[1].innerText = _Mon;
		tr.children[2].innerText = _Tue;
		tr.children[3].innerText = _Wed;
		tr.children[4].innerText = _Thu;
		tr.children[5].innerText = _Fri;
		tr.children[6].innerText = _Sat;
		
	}
}

//mode :时间变换的类型0-年 1-月 2-直接选择月
function change_date(temp,mode)
{
	var t_month,t_year
    if (mode){
        if(mode==1)
        t_month=parseInt(cele_date_month.value,10)+parseInt(temp,10);
        else
        t_month=parseInt(temp)
        if (t_month<cele_date_month.options(0).text) {
            cele_date_month.value=cele_date_month.options(cele_date_month.length-1).text;
            change_date(parseInt(cele_date_year.value,10)-1,0);
            }
        else{
            if (t_month>cele_date_month.options(cele_date_month.length-1).text){
                cele_date_month.value=cele_date_month.options(0).text;
                change_date(parseInt(cele_date_year.value,10)+1,0);
                }            
            else
                {cele_date_month.value=t_month;
                 set_cele_date(cele_date_year.value,cele_date_month.value);                
                }
        }
    }  
    else{
        t_year=parseInt(temp,10);
        
        if (t_year<cele_date_year.options(0).text) {
            cele_date_year.value=cele_date_year.options(0).text;
            set_cele_date(cele_date_year.value,1);                
            }
        else{
            if (parseInt(t_year,10)>parseInt(cele_date_year.options(cele_date_year.length-1).text,10)){
                cele_date_year.value=cele_date_year.options(cele_date_year.length-1).text;
                set_cele_date(cele_date_year.value,12);                
                }            
            else
                {cele_date_year.value=t_year;
                 set_cele_date(cele_date_year.value,cele_date_month.value);                
                }
        }
    }
    /*********2002-02-01 MODIFY BY WING **************/
    window.cele_date.focus();
    /****************MODIFY END***********************/
}

//初始化日历
function init(d_start,d_end)
{
     var temp_str;
     var i=0
     var j=0
     date_start=new Date(1980,7,1)
     date_end=new Date(2004,8,1)
     document.writeln("<div name=\"cele_date\" id=\"cele_date\"  style=\"display:none\"    style=\"LEFT: 69px; POSITION: absolute; TOP: 159px;Z-INDEX:99\" onClick=\"event.cancelBubble=true;\" onBlur=\"hilayer()\" onMouseout=\"lostlayerfocus()\">-</div>");
     window.cele_date.innerHTML="";
     temp_str="<table border=\"0\" bgcolor=\"#DDDDDD\" bordercolor=\"white\"><tr bgcolor=\"#DDDDDD\"><td colspan=7 onmouseover=\"overcolor(this)\">";
     temp_str+="<input type=\"Button\" value=\"<<\" onclick=\"change_date(-1,1)\" onmouseover=\"getlayerfocus()\" style=\"color: #000099; background-color: #BFBFBF; cursor: hand\">-";

     temp_str+=""
     temp_str+="<select name=\"cele_date_year\" id=\"cele_date_year\" language=\"javascript\" onchange=\"change_date(this.value,0)\" onmouseover=\"getlayerfocus()\" onblur=\"getlayerfocus()\" style=\"font-size: 9pt; border: 1px #666666 outset; background-color: #F4F8FB\">"

     for (i=1900;i<=2020;i++)
     {
     	temp_str+="<OPTION value=\""+i.toString()+"\">"+i.toString()+"</OPTION>";
     }
     temp_str+="</select>-";
     temp_str+=""
     temp_str+="<select name=\"cele_date_month\" id=\"cele_date_month\" language=\"javascript\" onchange=\"change_date(this.value,2)\" onmouseover=\"getlayerfocus()\" onblur=\"getlayerfocus()\" style=\"font-size: 9pt; border: 1px #666666 outset; background-color: #F4F8FB\">"

     for (i=1;i<=12;i++)
     {
     	temp_str+="<OPTION value=\""+i.toString()+"\">"+i.toString()+"</OPTION>";
     }
     temp_str+="</select>-";
     temp_str+=""
     temp_str+="<input type=\"Button\" value=\">>\" onclick=\"change_date(1,1)\" onmouseover=\"getlayerfocus()\"  style=\"color: #000099; background-color: #BFBFBF; cursor: hand\">";

     temp_str+="</td></tr><tr bgcolor=\"#DDDDDD\"><td onmouseover=\"overcolor(this)\">"
     temp_str+="<font color=red>"+_Sun+"</font></td><td>";temp_str+=_Mon+"</td><td>"; temp_str+=_Tue+"</td><td>"; temp_str+=_Wed+"</td><td>"
     temp_str+=_Thu+"</td><td>";temp_str+=_Fri+"</td><td>"; temp_str+=_Sat+"</td></tr>";
     for (i=1 ;i<=6 ;i++)
     {
     temp_str+="<tr>";
        for(j=1;j<=7;j++){
            temp_str+="<td name=\"c"+i+"_"+j+"\"id=\"c"+i+"_"+j+"\"  bgcolor=\"#DDDDDD\" style=\"CURSOR: hand\" style=\"COLOR:#000000\" language=\"javascript\" onmouseover=\"overcolor(this)\" onmouseout=\"outcolor(this)\" onclick=\"td_click(this)\">?</td>"
            }
     temp_str+="</tr>"        
     }
     temp_str+="</td></tr></table>";
     window.cele_date.innerHTML=temp_str;
}
function set_cele_date(year,month)
{
   var i,j,p,k
   var nd=new Date(year,month-1,1);
   event.cancelBubble=true;
   cele_date_year.value=year;
   cele_date_month.value=month;   
   k=nd.getDay()-1
   var temp;
   for (i=1;i<=6;i++)
      for(j=1;j<=7;j++)
      {
      eval("c"+i+"_"+j+".innerHTML=\"\"");
      eval("c"+i+"_"+j+".bgColor=\"#DDDDDD\"");
      eval("c"+i+"_"+j+".style.cursor=\"hand\"");
      }
   while(month-1==nd.getMonth())
    { j=(nd.getDay() +1);
      p=parseInt((nd.getDate()+k) / 7)+1;
      eval("c"+p+"_"+j+".innerHTML="+"\""+nd.getDate()+"\"");
      if ((nd.getDate()==today.getDate())&&(cele_date_month.value==today.getMonth()+1)&&(cele_date_year.value==today.getYear())){
      	 eval("c"+p+"_"+j+".bgColor=\"#EFFB64\"");
      }
      if (nd>date_end || nd<date_start)
      {
    //  eval("c"+p+"_"+j+".bgColor=\"#FF9999\"");
     // eval("c"+p+"_"+j+".style.cursor=\"text\"");
      }
      nd=new Date(nd.valueOf() + 86400000)
    }
}

//s_object：点击的对象；d_start-d_end有效的时间区段；需要存放值的控件；
function show_cele_date(eP,d_start,d_end,t_object)
{
GetDatePickerDayString();

window.cele_date.style.display="";
window.cele_date.style.zIndex=99
var s,cur_d
var eT = eP.offsetTop;  
var eH = eP.offsetHeight+eT;  
var dH = window.cele_date.style.pixelHeight;  
var sT = document.body.scrollTop; 
var sL = document.body.scrollLeft; 
event.cancelBubble=true;
window.cele_date.style.posLeft = event.clientX-event.offsetX+sL-5;  
window.cele_date.style.posTop = event.clientY-event.offsetY+eH+sT-5;
if (window.cele_date.style.posLeft+window.cele_date.clientWidth>document.body.clientWidth) window.cele_date.style.posLeft+=eP.offsetWidth-window.cele_date.clientWidth;

if (d_start!=""){
    if (d_start=="today"){
        date_start=new Date(today.getYear(),today.getMonth(),today.getDate());
    }else{
        s=d_start.split(separator);
        date_start=new Date(s[0],s[1]-1,s[2]);
    }
}else{
    date_start=new Date(1900,1,1);
}

if (d_end!=""){
    s=d_end.split(separator);
    date_end=new Date(s[0],s[1]-1,s[2]);
}else{
    date_end=new Date(3000,1,1);
}

g_object=t_object

if(t_object.value!='')
  {
     try
     {
       var cur=t_object.value;
       set_cele_date(cur.substring(0,4),parseInt(cur.substring(5,7)));
       
     }
     catch(e)
     {
       cur_d=new Date()
       set_cele_date(cur_d.getYear(),cur_d.getMonth()+1);
     }
  }
else
{
     cur_d=new Date()
     set_cele_date(cur_d.getYear(),cur_d.getMonth()+1);
}
window.cele_date.style.display="block";

window.cele_date.focus();

}
function td_click(t_object)
{
var t_d
if (parseInt(t_object.innerHTML,10)>=1 && parseInt(t_object.innerHTML,10)<=31 ) 
{ t_d=new Date(cele_date_year.value,cele_date_month.value-1,t_object.innerHTML)
if (t_d<=date_end && t_d>=date_start)
{
var year = cele_date_year.value;
var month = cele_date_month.value;
var day = t_object.innerHTML;
if (parseInt(month)<10) month = "0" + month;
if (parseInt(day)<10) day = "0" + day;

g_object.value=year+separator+month+separator+day;
window.cele_date.style.display="none";};
}

}
function h_cele_date()
{
window.cele_date.style.display="none";
}

function overcolor(obj)
{
  if (obj.style.cursor=="hand") obj.style.color = "#FFFFFF";

  inover=true;
  window.cele_date.focus();

}

function outcolor(obj)
{
	obj.style.color = "#000000";
	inover=false;

}

function getNow(o){
    var Stamp=new Date();
    var year = Stamp.getYear();
    var month = Stamp.getMonth()+1;
    var day = Stamp.getDate();
    if(month<10){
	month="0"+month;
    }
    if(day<10){
	day="0"+day;
    }
    o.value=year+separator+month+separator+day;
}

function hilayer()
{
	if (inover==false)
	{
		var lay=document.all.cele_date;
		lay.style.display="none";
	}
}
function getlayerfocus()
{
	inover=true;
}
function lostlayerfocus()
{
	inover=false;
}
init();
//日历结束

///区域表单
cityareaname=new Array(35);
cityareacode=new Array(35);

function first(preP,preC,formname,selectP,selectC)
   {
     a=0;
if (selectP=='01')
  { a=1;tempoption=new Option('北京','北京',false,true); }
else
  { tempoption=new Option('北京','北京'); }
eval('document.'+formname+'.'+preP+'.options[1]=tempoption;');
cityareacode[0]=new Array('0101','0102','0103','0104','0105','0106','0107','0108');
cityareaname[0]=new Array('东城区','西城区','崇文区','宣武区','朝阳区','海淀区','丰台区','石景山');
if (selectP=='02')
  { a=2;tempoption=new Option('深圳','深圳',false,true); }
else
  { tempoption=new Option('深圳','深圳'); }
eval('document.'+formname+'.'+preP+'.options[2]=tempoption;');
cityareacode[1]=new Array('0201','0202','0203','0204','0205','0206');
cityareaname[1]=new Array('罗湖','福田','南山','盐田','宝安','龙岗');
if (selectP=='03')
  { a=3;tempoption=new Option('上海','上海',false,true); }
else
  { tempoption=new Option('上海','上海'); }
eval('document.'+formname+'.'+preP+'.options[3]=tempoption;');
cityareacode[2]=new Array('0301','0302','0303','0304','0305','0306','0307','0308','0309','0310','0311','0312','0313','0314','0315','0316','0317','0318','0319','0320');
cityareaname[2]=new Array('宝山','金山','南市','长宁','静安','青浦','崇明','卢湾','松江','奉贤','浦东','杨浦','虹口','普陀','闸北','黄浦','闵行','徐汇','嘉定','南汇');
if (selectP=='04')
  { a=4;tempoption=new Option('重庆','重庆',false,true); }
else
  { tempoption=new Option('重庆','重庆'); }
eval('document.'+formname+'.'+preP+'.options[4]=tempoption;');
cityareacode[3]=new Array('0401','0402','0403','0404','0405','0406');
cityareaname[3]=new Array('渝中','江北','沙坪坝','南岸','九龙坡','大渡口');
if (selectP=='05')
  { a=5;tempoption=new Option('天津','天津',false,true); }
else
  { tempoption=new Option('天津','天津'); }
eval('document.'+formname+'.'+preP+'.options[5]=tempoption;');
cityareacode[4]=new Array('0501','0502','0503','0504','0505','0506','0507','0508','0509','0510','0511','0512','0513','0514','0515');
cityareaname[4]=new Array('和平','河北','河西','河东','南开','红桥','塘沽','汉沽','大港','东丽','西青','津南','北辰','武清','滨海');
if (selectP=='06')
  { a=6;tempoption=new Option('广东','广东',false,true); }
else
  { tempoption=new Option('广东','广东'); }
eval('document.'+formname+'.'+preP+'.options[6]=tempoption;');
cityareacode[5]=new Array('0601','0602','0603','0604','0605','0606','0607','0608','0609','0610','0611','0612','0613','0614','0615');
cityareaname[5]=new Array('广州','珠海','中山','佛山','东莞','清远','肇庆','阳江','湛江','韶关','惠州','河源','汕尾','汕头','梅州');
if (selectP=='07')
  { a=7;tempoption=new Option('河北','07',false,true); }
else
  { tempoption=new Option('河北','河北'); }
eval('document.'+formname+'.'+preP+'.options[7]=tempoption;');
cityareacode[6]=new Array('0701','0702','0703','0704','0705','0706','0707','0708','0709','0710','0711');
cityareaname[6]=new Array('石家庄','唐山','秦皇岛','邯郸','邢台','张家口','承德','廊坊','沧州','保定','衡水');
if (selectP=='08')
  { a=8;tempoption=new Option('山西','山西',false,true); }
else
  { tempoption=new Option('山西','山西'); }
eval('document.'+formname+'.'+preP+'.options[8]=tempoption;');
cityareacode[7]=new Array('0801','0802','0803','0804','0805','0806','0807');
cityareaname[7]=new Array('太原','大同','阳泉','朔州','长治','临汾','晋城');
if (selectP=='09')
  { a=9;tempoption=new Option('内蒙古','内蒙古',false,true); }
else
  { tempoption=new Option('内蒙古','09'); }
eval('document.'+formname+'.'+preP+'.options[9]=tempoption;');
cityareacode[8]=new Array('0901','0902','0903','0904','0905','0906','0907','0908','0909','0910','0911');
cityareaname[8]=new Array('呼和浩特','包头','乌海','临河','东胜','集宁','锡林浩特','通辽','赤峰','海拉尔','乌兰浩特');
if (selectP=='10')
  { a=10;tempoption=new Option('辽宁','10',false,true); }
else
  { tempoption=new Option('辽宁','辽宁'); }
eval('document.'+formname+'.'+preP+'.options[10]=tempoption;');
cityareacode[9]=new Array('1001','1002','1003','1004','1005','1006','1007','1008','1009','1010','1011','1012','1013','1014');
cityareaname[9]=new Array('沈阳','大连','鞍山','锦州','丹东','盘锦','铁岭','抚顺','营口','辽阳','阜新','本溪','朝阳','葫芦岛');
if (selectP=='11')
  { a=11;tempoption=new Option('吉林','吉林',false,true); }
else
  { tempoption=new Option('吉林','吉林'); }
eval('document.'+formname+'.'+preP+'.options[11]=tempoption;');
cityareacode[10]=new Array('1101','1102','1103','1104','1105','1106','1107','1108','1109');
cityareaname[10]=new Array('长春','吉林','四平','辽源','通化','白山','松原','白城','延边');
if (selectP=='12')
  { a=12;tempoption=new Option('黑龙江','黑龙江',false,true); }
else
  { tempoption=new Option('黑龙江','黑龙江'); }
eval('document.'+formname+'.'+preP+'.options[12]=tempoption;');
cityareacode[11]=new Array('1201','1202','1203','1204','1205','1206','1207','1208','1209','1210','1211','1212','1213');
cityareaname[11]=new Array('哈尔滨','齐齐哈尔','牡丹江','佳木斯','大庆','伊春','黑河','鸡西','鹤岗','双鸭山','七台河','绥化','大兴安岭');
if (selectP=='13')
  { a=13;tempoption=new Option('江苏','江苏',false,true); }
else
  { tempoption=new Option('江苏','江苏'); }
eval('document.'+formname+'.'+preP+'.options[13]=tempoption;');
cityareacode[12]=new Array('1301','1302','1303','1304','1305','1306','1307','1308','1309','1310','1311','1312','1313');
cityareaname[12]=new Array('南京','苏州','无锡','常州','镇江','连云港 ','扬州','徐州 ','南通','盐城','淮阴','泰州','宿迁');
if (selectP=='14')
  { a=14;tempoption=new Option('浙江','浙江',false,true); }
else
  { tempoption=new Option('浙江','浙江'); }
eval('document.'+formname+'.'+preP+'.options[14]=tempoption;');
cityareacode[13]=new Array('1401','1402','1403','1404','1405','1406','1407','1408','1409','1410','1411');
cityareaname[13]=new Array('杭州','湖州','丽水','温州','绍兴','舟山','嘉兴','金华','台州','衢州','宁波');
if (selectP=='15')
  { a=15;tempoption=new Option('安徽','安徽',false,true); }
else
  { tempoption=new Option('安徽','安徽'); }
eval('document.'+formname+'.'+preP+'.options[15]=tempoption;');
cityareacode[14]=new Array('1501','1502','1503','1504','1505','1506','1507','1508','1509','1510','1511','1512','1513','1514','1515','1516','1517');
cityareaname[14]=new Array('合肥  ','芜湖 ','蚌埠 ','滁州 ','安庆 ','六安 ','黄山 ','宣城 ','淮南 ','宿州 ','马鞍山 ','铜陵','淮北 ','阜阳 ','池州 ','巢湖 ','亳州');
if (selectP=='16')
  { a=16;tempoption=new Option('福建','福建',false,true); }
else
  { tempoption=new Option('福建','福建'); }
eval('document.'+formname+'.'+preP+'.options[16]=tempoption;');
cityareacode[15]=new Array('1601','1602','1603','1604','1605','1606','1607','1608','1609');
cityareaname[15]=new Array('福州 ','厦门 ','泉州 ','漳州 ','龙岩 ','南平 ','宁德 ','莆田 ','三明');
if (selectP=='17')
  { a=17;tempoption=new Option('江西','江西',false,true); }
else
  { tempoption=new Option('江西','江西'); }
eval('document.'+formname+'.'+preP+'.options[17]=tempoption;');
cityareacode[16]=new Array('1701','1702','1703','1704','1705','1706','1707','1708','1709','1710','1711');
cityareaname[16]=new Array('南昌','景德镇','九江','萍乡','新余','鹰潭','赣州','宜春','吉安','上饶','抚州');
if (selectP=='18')
  { a=18;tempoption=new Option('山东','山东',false,true); }
else
  { tempoption=new Option('山东','山东'); }
eval('document.'+formname+'.'+preP+'.options[18]=tempoption;');
cityareacode[17]=new Array('1801','1802','1803','1804','1805','1806','1807','1808','1809','1810','1811','1812','1813','1814','1815','1816','1817');
cityareaname[17]=new Array('济南','青岛','淄博','德州','烟台','潍坊','济宁','泰安','临沂','菏泽','威海','枣庄','日照','莱芜','聊城','滨州','东营');
if (selectP=='19')
  { a=19;tempoption=new Option('河南','河南',false,true); }
else
  { tempoption=new Option('河南','河南'); }
eval('document.'+formname+'.'+preP+'.options[19]=tempoption;');
cityareacode[18]=new Array('1901','1902','1903','1904','1905','1906','1907','1908','1909','1910','1911','1912','1913','1914','1915','1916','1917','1918');
cityareaname[18]=new Array('郑州','开封','洛阳','平顶山','安阳','鹤壁','新乡','焦作','濮阳','许昌','漯河','三门峡','南阳','商丘','周口','驻马店','信阳','济源');
if (selectP=='20')
  { a=20;tempoption=new Option('湖北','湖北',false,true); }
else
  { tempoption=new Option('湖北','湖北'); }
eval('document.'+formname+'.'+preP+'.options[20]=tempoption;');
cityareacode[19]=new Array('2001','2002','2003','2004','2005','2006','2007','2008','2009','2010','2011','2012','2013','2014','2015','2016','2017');
cityareaname[19]=new Array('武汉','黄石','十堰','荆州','宜昌','襄樊','鄂州','荆门','孝感','黄冈','咸宁','恩施','随州','仙桃','天门','潜江','神农架');
if (selectP=='21')
  { a=21;tempoption=new Option('湖南','21',false,true); }
else
  { tempoption=new Option('湖南','湖南'); }
eval('document.'+formname+'.'+preP+'.options[21]=tempoption;');
cityareacode[20]=new Array('2101','2102','2103','2104','2105','2106','2107','2108','2109','2110','2111','2112','2113');
cityareaname[20]=new Array('长沙','株州','湘潭','衡阳','邵阳','岳阳','常德','郴州','益阳','永州','怀化','娄底','湘西 ');
if (selectP=='22')
  { a=22;tempoption=new Option('广西','广西',false,true); }
else
  { tempoption=new Option('广西','广西'); }
eval('document.'+formname+'.'+preP+'.options[22]=tempoption;');
cityareacode[21]=new Array('2201','2202','2203','2204','2205','2206','2207','2208','2209','2210','2211','2212');
cityareaname[21]=new Array('南宁','柳州','桂林','梧州','北海','防城港','钦州','贵港','玉林','贺州','百色','河池');
if (selectP=='23')
  { a=23;tempoption=new Option('海南','海南',false,true); }
else
  { tempoption=new Option('海南','海南'); }
eval('document.'+formname+'.'+preP+'.options[23]=tempoption;');
cityareacode[22]=new Array('2301','2302','2303','2304','2305','2306','2307','2308','2309');
cityareaname[22]=new Array('海口 ','三亚','通什','琼海','琼山','文昌','万宁','东方','儋州');
if (selectP=='24')
  { a=24;tempoption=new Option('四川','四川',false,true); }
else
  { tempoption=new Option('四川','四川'); }
eval('document.'+formname+'.'+preP+'.options[24]=tempoption;');
cityareacode[23]=new Array('2401','2402','2403','2404','2405','2406','2407','2408','2409','2410','2411','2412','2413','2414','2415','2416','2417','2418','2419','2420');
cityareaname[23]=new Array('成都','自贡','攀枝花','泸州','德阳','绵阳','广元','遂宁','内江','乐山','南充  ','宜宾','广安','达川','巴中','雅安','眉山  ','阿坝 ','甘孜 ','凉山 ');
if (selectP=='25')
  { a=25;tempoption=new Option('贵州','贵州',false,true); }
else
  { tempoption=new Option('贵州','贵州'); }
eval('document.'+formname+'.'+preP+'.options[25]=tempoption;');
cityareacode[24]=new Array('2501','2502','2503','2504','2505','2506','2507','2508','2509');
cityareaname[24]=new Array('贵阳 ','六盘水','遵义','铜仁','毕节','安顺','黔西南 ','黔东南','黔南');
if (selectP=='26')
  { a=26;tempoption=new Option('云南','云南',false,true); }
else
  { tempoption=new Option('云南','云南'); }
eval('document.'+formname+'.'+preP+'.options[26]=tempoption;');
cityareacode[25]=new Array('2601','2602','2603','2604','2605','2606','2607','2608','2609','2610','2611','2612','2613','2614','2615','2616','2617');
cityareaname[25]=new Array('昆明','东川','曲靖','玉溪','昭通','思茅','临沧','保山','丽江','文山 ','红河 ','西双版纳 ','楚雄 ','大理 ','德宏 ','怒江','迪庆');
if (selectP=='27')
  { a=27;tempoption=new Option('西藏','27',false,true); }
else
  { tempoption=new Option('西藏','西藏'); }
eval('document.'+formname+'.'+preP+'.options[27]=tempoption;');
cityareacode[26]=new Array('2701','2702','2703','2704','2705','2706','2707');
cityareaname[26]=new Array('拉萨','那曲','昌都','山南','日喀则','阿里','林芝');
if (selectP=='28')
  { a=28;tempoption=new Option('陕西','陕西',false,true); }
else
  { tempoption=new Option('陕西','陕西'); }
eval('document.'+formname+'.'+preP+'.options[28]=tempoption;');
cityareacode[27]=new Array('2801','2802','2803','2804','2805','2806','2807','2808','2809','2810');
cityareaname[27]=new Array('西安','铜川','宝鸡','咸阳','渭南','延安','汉中','榆林','商洛','安康');
if (selectP=='29')
  { a=29;tempoption=new Option('甘肃','甘肃',false,true); }
else
  { tempoption=new Option('甘肃','甘肃'); }
eval('document.'+formname+'.'+preP+'.options[29]=tempoption;');
cityareacode[28]=new Array('2901','2902','2903','2904','2905','2906','2907','2908','2909','2910','2911','2912','2913','2914');
cityareaname[28]=new Array('兰州','金昌','白银','天水','嘉峪关','定西','平凉','庆阳','陇南','武威','张掖','酒泉','甘南 ','临夏');
if (selectP=='30')
  { a=30;tempoption=new Option('青海','青海',false,true); }
else
  { tempoption=new Option('青海','青海'); }
eval('document.'+formname+'.'+preP+'.options[30]=tempoption;');
cityareacode[29]=new Array('3001','3002','3003','3004','3005','3006','3007','3008');
cityareaname[29]=new Array('西宁','海东',' 海北 ','黄南','海南','果洛','玉树','海西');
if (selectP=='31')
  { a=31;tempoption=new Option('宁夏','宁夏',false,true); }
else
  { tempoption=new Option('宁夏','宁夏'); }
eval('document.'+formname+'.'+preP+'.options[31]=tempoption;');
cityareacode[30]=new Array('3101','3102','3103','3104');
cityareaname[30]=new Array('银川','石嘴山','银南','固原');
if (selectP=='32')
  { a=32;tempoption=new Option('新疆','新疆',false,true); }
else
  { tempoption=new Option('新疆','新疆'); }
eval('document.'+formname+'.'+preP+'.options[32]=tempoption;');
cityareacode[31]=new Array('3201','3202','3203','3204','3205','3206','3207','3208','3209','3210','3211','3212','3213');
cityareaname[31]=new Array('乌鲁木齐','克拉玛依','石河子','吐鲁番','哈密','和田','阿克苏','喀什','克孜勒苏','巴音郭楞','昌吉','博尔塔拉','伊犁');
if (selectP=='33')
  { a=33;tempoption=new Option('香港','香港',false,true); }
else
  { tempoption=new Option('香港','香港'); }
eval('document.'+formname+'.'+preP+'.options[33]=tempoption;');
cityareacode[32]=new Array();
cityareaname[32]=new Array();
if (selectP=='34')
  { a=34;tempoption=new Option('澳门','澳门',false,true); }
else
  { tempoption=new Option('澳门','澳门'); }
eval('document.'+formname+'.'+preP+'.options[34]=tempoption;');
cityareacode[33]=new Array();
cityareaname[33]=new Array();
if (selectP=='35')
  { a=35;tempoption=new Option('台湾','台湾',false,true); }
else
  { tempoption=new Option('台湾','台湾'); }
eval('document.'+formname+'.'+preP+'.options[35]=tempoption;');
cityareacode[34]=new Array();
cityareaname[34]=new Array();

eval('document.'+formname+'.'+preP+'.options[a].selected=true;');

cityid=selectP;
    if (cityid!='0')
      {
        b=0;for (i=0;i<cityareaname[cityid-1].length;i++)
           {
             if (selectC==cityareacode[cityid-1][i])
               {b=i+1;tempoption=new Option(cityareaname[cityid-1][i],cityareaname[cityid-1][i],false,true);}
             else
               tempoption=new Option(cityareaname[cityid-1][i],cityareaname[cityid-1][i]);
            eval('document.'+formname+'.'+preC+'.options[i+1]=tempoption;');
           }
        eval('document.'+formname+'.'+preC+'.options[b].selected=true;');
      }
    }
 function selectcityarea(preP,preC,formname)
   {
     cityid=eval('document.'+formname+'.'+preP+'.selectedIndex;');
     j=eval('document.'+formname+'.'+preC+'.length;');
     for (i=1;i<j;i++)
        {eval('document.'+formname+'.'+preC+'.options[j-i]=null;')}
     if (cityid!="0")
       {
         for (i=0;i<cityareaname[cityid-1].length;i++)
            {
             tempoption=new Option(cityareaname[cityid-1][i],cityareaname[cityid-1][i]);
             eval('document.'+formname+'.'+preC+'.options[i+1]=tempoption;');
            }
       }
    }
//区域表单结束


function gopreview()
{
document.preview.username.value=document.theForm.name.value;
document.preview.email.value=document.theForm.e_mail.value;
var popupWin = window.open('', 'preview_page', 'scrollbars=yes,width=500,height=300');
document.preview.submit()
}