<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SearchPlacement_DoubleLines2.ascx.cs"
    Inherits="UserControl_SearchPlacement_TwoLines2" %>
<!--  引用用户控件页面需引用以下样式
<link rel="Stylesheet" href="CSS/Store.css" type="text/css" id="cssid" />
-->
<style type="text/css">
    body
    {
        margin: 0;
        padding: 0;
        font: lighter 12px/22px Arial, Helvetica, sans-serif;
    }
    .main
    {
        width: 670px;
        margin: 10px auto;
        clear: both;
    }
    .box
    { height:120px;
        border: 1px solid #91b2c6;
        width: 120px;
        margin: 0 auto;
        background: #f2fbff;
        color: #333;
        padding: 3px 0 3px 10px;
    }
    .main2
    {
        width: 463px;
        margin: 0 auto;
        clear: both;
    }
    .main3
    {
        width: 305px;
        margin-left: 10px;
    }
    .main4
    {
        width: 305px;
        margin-left: 40px;
    }
    .lines
    {
        width: 343px;
        clear: both;
        margin: 0 auto;
    }
    .lines2
    {
        width: 185px;
        clear: both;
        margin: 0 auto;
    }
    a.rege
    {
        width: 50px;
        border: 1px solid #484848;
        float: right;
        padding-left: 5px;
    }
    .left
    {
        float: left;
    }
    .right
    {
        float: right;
    }
    .tb
    {
        border: 0px;
        width: 120px;
        cursor: pointer;
        font-size: 12px;
    }
    .aa
    {
        font-size: 12px;
    }
    .aa td a
    {
        text-decoration: none;
        color: #000;
        line-height: 22px;
    }
    .aa td a:hover
    {
        text-decoration: none;
        color: #1d4a68;
    }
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
      msgw=700;//msg width 
      msgh=500;//msg height 
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
      msgObj.style.background="white"; 
      msgObj.style.border="1px solid " + bordercolor; 
      msgObj.style.position = "absolute"; 
      msgObj.style.left = "35%"; 
      msgObj.style.top = "20%"; 
      msgObj.style.font="12px/1.6em Verdana, Geneva, Arial, Helvetica, sans-serif"; 
      msgObj.style.marginLeft = "-225px" ; 
      msgObj.style.marginTop = -75+document.documentElement.scrollTop+"px"; 
      msgObj.style.width = msgw + "px"; 
      msgObj.style.height =msgh + "px"; 
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
          
          //显示页面下拉框
          hidselect(1);
        } 
        document.body.appendChild(msgObj); 
        document.getElementById("msgDiv").appendChild(title); 
        var txt=document.createElement("div"); 
        msgObj.style.background="white"; 
        msgObj.style.width = msgw + "px"; 
        msgObj.style.height =msgh + "px"; 
        txt.setAttribute("id","msgTxt"); 
        txt.innerHTML=str; 
        document.getElementById("msgDiv").appendChild(txt); 
     } 

    function ChangeColor(type,td)
    {
        if(type==0)
        {
             td.style.backgroundColor ="#f2fbff";
             td.style.color="#333";
        }
        else
        {
            td.style.backgroundColor ="#fffef2";
            td.style.color="#0067a9";
        }
    }

    function SearchPlacement(selectValue, floorCount) {
	   var txtSb =  document.getElementById('<%=Txttj.ClientID%>').value;
       document.getElementById("hdTopNumber").value=txtSb ;
       if(txtSb=="")
       {
           $("#info_txtPlacement").html('<%=tran.GetTran("007337","请填写推荐编号") %>'); $("#info_txtPlacement").attr("class", "bgimger"); $("#info_txtPlacement").show();
//            alert('请填写推荐编号！');
            return;
       }
       else
       {
           var b=false;
           if( AjaxClass!=null)
           {
               b =  AjaxClass.CheckNumber(txtSb).value;
           }
           if (!b) { $("#info_txtPlacement").html('<%=tran.GetTran("000717","推荐编号不存在") %>'); $("#info_txtPlacement").attr("class", "bgimger"); $("#info_txtPlacement").show(); return; }
           
           var result=AjaxClass.SearchPlacement_DoubleLines2(txtSb.toString(),floorCount).value;
           
           ShowDialog(result); //弹出框显示网络图
       }
	}
	
	//选择左右区
	function ChangeLeftOrRight(topNum,type,iszc)
	{
	    var num=topNum;
	    if(iszc==1)
	    {
	        num = AjaxClass.GetLeftOrRight(topNum,type).value;
	    }
	    document.getElementById("<%=txtPlaceMent.ClientID%>").value=num;
        document.getElementById("<%=HidDistrict.ClientID %>").value=type.toString();
        document.body.removeChild(document.getElementById("bgDiv")); 
        document.getElementById("msgDiv").removeChild(document.getElementById("msgTitle")); 
        document.body.removeChild(document.getElementById("msgDiv")); 
         $("#SearchPlacement_DoubleLines1_txtPlaceMent").blur();
        //显示页面下拉框
          hidselect(1);
	}
	
	//点击第三层
	function ClickThreeFloor(topNum)
	{
	    document.getElementById("msgTxt").removeChild(document.getElementById("divDrawing")); 
	
	    var result=AjaxClass.SearchPlacement_DoubleLines(topNum.toString(),1).value;
	    document.getElementById("msgTxt").innerHTML+=result;
	    
	    ChangeLine(topNum);
	}
	
	//更新链路图
    function ChangeLine(num)
    {
        var allNums = document.getElementById("hdTopNumber").value;
        var lineHtml = document.getElementById("tdLine").innerHTML;
        var newHtml=AjaxClass.LineDrawingForDouble(num,allNums,lineHtml).value;
        document.getElementById("hdTopNumber").value = newHtml.substring (newHtml.lastIndexOf("|")+1,newHtml.length);
        document.getElementById("tdLine").innerHTML = newHtml.substring (0,newHtml.lastIndexOf("|"));
    }
    
    //点击链路图
    function ClickTable(topNum)
    {
        //更新网络图
        ClickThreeFloor(topNum);
        //更新链路图
        ChangeLine(topNum);
    }
    
    function hidselect(hs)
    {
//     var ss= document.getElementsByTagName("select");
//      for(var i=0; i<ss.length;i++)
//      {
//      if(hs==0)
//         ss[i].style.display="none";
//      else if(hs==1){ss[i].style.display="block";}
//      }
    }
    
</script>

<span class="re_text"><span style="color: Red">* </span><%=tran.GetTran("000027","安置编号")%>：</span>
<span style="float:left;">
 <input type="hidden" value="" id="hdTopNumber" />
            <asp:TextBox ID="Txttj" Width="87px" runat="server" MaxLength="10" Style="display: none;"></asp:TextBox>
            <asp:TextBox ID="txtPlaceMent" CssClass="re_table"  MaxLength="10" runat="server"></asp:TextBox>
            &nbsp;
                
            <input name="btnSearch" type="button"  id="Button1" class="btn2" onclick="SearchPlacement('',0);hidselect(0);"  value='<%=tran.GetTran("005861","寻找安置人") %>' />
            <input type="hidden" runat="server" id="HidAz" />
            <input type="hidden" runat="server" value="1" id="HidDistrict" />
</span>

<%--<table cellspacing="0" cellpadding="0" border="0">
    <tr>
        <td align="right" style="font-family: '宋体';font-size: 14px;line-height: 24px;color: #666;">
            <span style="color: Red">*</span>安置编号：
        </td>
        <td>
           
        </td>
    </tr>
</table>--%>
