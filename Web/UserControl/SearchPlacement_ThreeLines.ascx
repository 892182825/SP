<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SearchPlacement_ThreeLines.ascx.cs"
    Inherits="UserControl_SearchPlacement" %>
<!--  引用用户控件页面需引用以下样式
<link rel="Stylesheet" href="CSS/Store.css" type="text/css" id="cssid" />
-->

<!-- 网络图表格样式 -->
<style type="text/css">
.styletable{font-size:12px;border-bottom:solid 1px #626262; border-right:solid 1px #626262; width:600px;}
.styletable td{color:#333; text-align:center; line-height:24px; width:298px;}
.styletable td a{ text-decoration:none;  color:#000;}
.styletable td a:hover{ text-decoration:none;  color:#1d4a68;}
.td1{ background:#dbe7f5;width:199px;}
.td2{ background:#ffefed;width:199px;}
.td3{ background:#fed2c7;width:199px;}
.td1_1{ background:#dbe7f5; border-top:solid 1px #626262;border-left:solid 1px #626262;width:199px;}
.td2_1{ background:#ffefed; border-top:solid 1px #626262;border-left:solid 1px #626262;width:199px;}
.td3_1{ background:#fed2c7; border-top:solid 1px #626262;border-left:solid 1px #626262;width:199px;}

.td1 td,
.td2 td,
.td3 td{border-top:solid 1px #626262; border-left:solid 1px #626262;cursor:pointer;}
.b_r{border-right:solid 1px #626262;}
.aa{ font-size:12px;}
.aa td a{ text-decoration:none;  color:#000; line-height:22px;}
.aa td a:hover{ text-decoration:none;  color:#1d4a68;}
</style>

<script type="text/javascript" language="javascript">

    /********************
    * 取窗口滚动条高度
    ******************/
    function getScrollTop()
    {
        var scrollTop=0;
        if(document.documentElement&&document.documentElement.scrollTop)
        {
        scrollTop=document.documentElement.scrollTop;
        }
        else if(document.body)
        {
        scrollTop=document.body.scrollTop;
        }
        return scrollTop;
    }

    /********************
    * 取窗口可视范围的高度
    *******************/
    function getClientHeight()
    {
        var clientHeight=0;
        if(document.body.clientHeight&&document.documentElement.clientHeight)
        {
            var clientHeight = (document.body.clientHeight<document.documentElement.clientHeight)?document.body.clientHeight:document.documentElement.clientHeight;
        }
        else
        {
            var clientHeight = (document.body.clientHeight>document.documentElement.clientHeight)?document.body.clientHeight:document.documentElement.clientHeight;
        }
        return clientHeight;
    }

    /********************
    * 取文档内容实际高度
    *******************/
    function getScrollHeight()
    {
        return Math.max(document.body.scrollHeight,document.documentElement.scrollHeight);
    }

    /********************
    * 新建DIV
    *******************/
  function ShowDialog(str)
  { 
      var msgw,msgh,bordercolor; 
      msgw=610;//msg width 
      msgh=200;//msg height 
      titleheight=25 //msg title height 
      bordercolor="#498db9";// 
      titlecolor="#498db9";// 
      
      var sWidth,sHeight; 
      sWidth=screen.width; 
      sHeight=getScrollHeight()  ;//screen.height; 

      var bgObj=document.createElement("div"); 
      bgObj.setAttribute('id','bgDiv'); 
      bgObj.style.position="absolute"; 
      bgObj.style.top="0"; 
      bgObj.style.background="#cccccc"; 
      bgObj.style.filter="progid:DXImageTransform.Microsoft.Alpha(style=3,opacity=25,finishOpacity=75"; 
      bgObj.style.opacity="0.6"; 
      bgObj.style.left="0"; 
      bgObj.style.width=sWidth + "px"; 
      bgObj.style.height=sHeight + "px"; 
      bgObj.style.zIndex = "10000"; 
      document.body.appendChild(bgObj); 
      
      var msgObj=document.createElement("div") 
      msgObj.setAttribute("id","msgDiv"); 
      msgObj.setAttribute("align","center"); 
      msgObj.setAttribute("vertical-align","top");
      msgObj.style.background="white"; 
      msgObj.style.border="1px solid " + bordercolor; 
      msgObj.style.position = "absolute"; 
      msgObj.style.left = "50%"; 
      msgObj.style.top = "50%"; 
      msgObj.style.font="12px/1.6em Verdana, Geneva, Arial, Helvetica, sans-serif"; 
      msgObj.style.marginLeft = "-225px" ; 
      msgObj.style.marginTop = -75+document.documentElement.scrollTop+"px"; 
      msgObj.style.width = msgw + "px"; 
      msgObj.style.height =msgh + "px"; 
      msgObj.style.textAlign = "center"; 
      msgObj.style.lineHeight ="25px"; 
      msgObj.style.zIndex = "10001"; 

        var title=document.createElement("h4"); 
        title.setAttribute("id","msgTitle"); 
        title.setAttribute("align","right"); 
        title.style.margin="0"; 
        title.style.padding="3px"; 
        title.style.background=bordercolor; 
        title.style.filter="progid:DXImageTransform.Microsoft.Alpha(startX=20, startY=20, finishX=100, finishY=100,style=1,opacity=75,finishOpacity=100);"; 
        title.style.opacity="0.75"; 
        title.style.border="1px solid " + bordercolor; 
        title.style.height="18px"; 
        title.style.font="12px Verdana, Geneva, Arial, Helvetica, sans-serif"; 
        title.style.color="white"; 
        title.style.cursor="pointer"; 
        title.innerHTML="关闭"; 
        title.onclick=function(){ 
          document.body.removeChild(bgObj); 
          document.getElementById("msgDiv").removeChild(title); 
          document.body.removeChild(msgObj); 
        } 
        document.body.appendChild(msgObj); 
        document.getElementById("msgDiv").appendChild(title); 
        var txt=document.createElement("p"); 
        txt.style.margin="1em 0" 
        txt.setAttribute("id","msgTxt"); 
        txt.innerHTML=str; 
        document.getElementById("msgDiv").appendChild(txt); 
     } 

    function  SearchPlacement(selectValue,floorCount)
	{
	   var txtSb =  document.getElementById('<%=Txttj.ClientID%>').value;
       document.getElementById("hdTopNumber").value=txtSb ;
       
       if(txtSb=="")
       {
            alert('请填写推荐编号！');
            return;
       }
       else
       {
//             var b=  AjaxClass.CheckTuiJian(txtSb.toString()).value;
//     
//           if(!b) { alert('推荐编号不存在！'); return ;}
           var result=AjaxClass.SearchPlacement_ThreeLines(txtSb.toString(),floorCount).value;
           ShowDialog(result); //弹出框显示网络图
       }
	}
	
	//点击表格
	function ClickTable(topNum)
	{
	   //重新添加第一层
	   for(var i=1;i<4;i++)
	   {
	        if(document.getElementById("tbFloor"+i.toString())!=null)
	        {
	            document.getElementById("msgTxt").removeChild(document.getElementById("tbFloor"+i.toString())); 
	        }
	   }
	   
	   document.getElementById("msgTxt").innerHTML += AjaxClass.SearchPlacement_ThreeLines(topNum.toString(),1).value;
	   //更新链路图
	   ChangeLine(topNum);
	}
	
	//鼠标放到表格上
	function MouseOnTable(tbOn,floorCount,topNum)
	{
	   if(floorCount<3)
	   {
	       var floorID="trFloor"+floorCount.toString();
	       var tbByFloor = document.getElementById(floorID).getElementsByTagName("table");
	       for(var i=0;i<tbByFloor.length;i++)
	       {
	            var tdclass = tbByFloor[i].className;
    	        
	            if(tdclass.indexOf("_")>0)
	            {
                    tbByFloor[i].className="td"+floorCount.toString()+"_1";
	            }
	            else
	            {
	                tbByFloor[i].className="td"+floorCount.toString();
	            }
	       }
    	   
	       tbOn.className="td"+(floorCount+1).toString();
    	   
	       //添加下一层
	       for(var i=floorCount;i<3;i++)
	       {
	            if(document.getElementById("tbFloor"+(i+1).toString())!=null)
	            {
	                document.getElementById("msgTxt").removeChild(document.getElementById("tbFloor"+(i+1).toString())); 
	            }
	       }
    	   
	       document.getElementById("msgTxt").innerHTML+=AjaxClass.SearchPlacement_ThreeLines(topNum,floorCount+1).value;
	   }
	   //变换链路图
	   ChangeLine(topNum);
	}
    
    //更新链路图
    function ChangeLine(num)
    {
        var allNums = document.getElementById("hdTopNumber").value;
        var lineHtml = document.getElementById("tdLine").innerHTML;
        var newHtml=AjaxClass.LineDrawing(num,allNums,lineHtml).value;
        document.getElementById("hdTopNumber").value = newHtml.substring (newHtml.lastIndexOf("|")+1,newHtml.length);
        document.getElementById("tdLine").innerHTML = newHtml.substring (0,newHtml.lastIndexOf("|"));
    }
    
    //选择安置人
    function ChoosePlacement(num,qushu)
    {
          document.getElementById("<%=txtPlaceMent.ClientID%>").value=num;
          document.getElementById("<%=txtQuShu.ClientID %>").value=qushu.toString();
    
          document.body.removeChild(document.getElementById("bgDiv")); 
          document.getElementById("msgDiv").removeChild(document.getElementById("msgTitle")); 
          document.body.removeChild(document.getElementById("msgDiv")); 
    }
    
    function DisplayNextTable(tbOn,floorCount)
    {
        if(floorCount>=3) {return;}
        var floorID="trFloor"+floorCount.toString();
	       var tbByFloor = document.getElementById(floorID).getElementsByTagName("table");
	       for(var i=0;i<tbByFloor.length;i++)
	       {
	            var tdclass = tbByFloor[i].className;
    	        
	            if(tdclass.indexOf("_")>0)
	            {
                    tbByFloor[i].className="td"+floorCount.toString()+"_1";
	            }
	            else
	            {
	                tbByFloor[i].className="td"+floorCount.toString();
	            }
	       }
    	   
    	   if(tbOn.className.indexOf("_")>0)
    	   {
	            tbOn.className="td"+(floorCount+1).toString()+"_1";
	       }
	       else
	       {
	            tbOn.className="td"+(floorCount+1).toString();
	       }
    
        //删除下面一层
	   for(var i=1;i<4;i++)
	   {
	        if(document.getElementById("tbFloor"+(floorCount+1).toString())!=null)
	        {
	            document.getElementById("msgTxt").removeChild(document.getElementById("tbFloor"+(floorCount+1).toString())); 
	        }
	   }
    }
    
</script>

<table cellspacing="0" cellpadding="0" border="0" width="50%" class="biaozzi">
    <tr>
        <td align="right">
            <span style="color:Red">*</span>安置编号：
        </td>
        <td>
            <input type="hidden" value="" id="hdTopNumber" />
            <asp:TextBox ID="Txttj" Width="87px" runat="server" MaxLength="10" Style="display: none;"></asp:TextBox>
            <asp:TextBox ID="txtQuShu" Width="87px" MaxLength="10" runat="server" Style="display: none;" ></asp:TextBox>
            <asp:TextBox ID="txtPlaceMent"  MaxLength="10" runat="server"></asp:TextBox>
            &nbsp;<input name="btnSearch" type="button" style="margin-top:8px" class="anyes" id="Button1" onclick="SearchPlacement('',0);" value='寻找安置人' />
            <input type="hidden" runat="server" id="HidAz" />
        </td>
    </tr>
</table>
