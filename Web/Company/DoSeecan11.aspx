<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DoSeecan11.aspx.cs" Inherits="DoSeecan11" %>

 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

 
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>汇款浏览操作</title>
   
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
<style type="text/css">
body{ margin:0px; padding:0px; list-style:none; color:#fff; font-family:"宋体"; font-size:12px;}

.gvtable{ border-left:none; border-top:none; border-right:#ccc solid 1px; border-bottom:#ccc solid 1px;    margin-top:20px; }
.gvtable th{ border-right:none; border-left:#ccc solid 1px;border-bottom:none; border-top:#ccc solid 1px; line-height:20px; font-size:14px ; font-weight:bold; color:#222; }

.gvtable td{ border-right:none; border-left:#ccc solid 1px;border-bottom:none; border-top:#ccc solid 1px; line-height:20px; font-size:12px ;   color:#444; }

.btnt{ width:20px; height:20px;  margin:10px; border:#ccc 1px solid;   cursor:pointer;}
.btno{ width:20px; height:20px; margin-top:10px; margin-bottom:10px; margin-left:5px;   cursor:pointer;  border:#ccc 1px solid; }
.btnc{  width:20px; height:20px; margin-top:10px; margin-bottom:10px; margin-left:5px;   border:#ccc 1px solid;  background:#eeaa99; } 

 
.buts{ width:58px; height:24px;  color:#000; background:url(../images/but_1.jpg) top left no-repeat; border:none; cursor:pointer; margin:5px;}
.butt{ width:58px; height:24px;  color:#000; background:url(../images/but_2.jpg) top left no-repeat; border:none; cursor:pointer; margin:5px;}


</style>
</head>
<body>
    <form id="form1" runat="server">
    <center>
       
        <div style="width: 700px; margin-top: 0px; padding: 10px; height: 100%;">
            <div style="color: #000; text-align: left;">
                汇入卡号：<asp:DropDownList ID="ddllistcard" runat="server" >
                </asp:DropDownList>
                &nbsp;&nbsp;
                <asp:Button ID="btnsrearch" CssClass="butt" runat="server" Text="查询" OnClick="btnsrearch_Click" /> 
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="lkbtnmore" Text="浏览更多" runat="server" OnClick="lkbtnmore_Click"></asp:LinkButton>
                <br /><div style="display:none;">
                <asp:LinkButton ID="LinkButton1" runat="server"  Text=""         onclick="LinkButton1_Click"></asp:LinkButton></div>
                <br/> 汇单时间：<asp:Button ID="btnyytoday" runat="server" Text="前天" CssClass="butt" OnClick="btnyytoday_Click" />&nbsp;&nbsp;<asp:Button
                    ID="btnyestoday" CssClass="butt" runat="server" Text="昨天" OnClick="btnyestoday_Click" />&nbsp;&nbsp;<asp:Button
                        CssClass="butt" ID="btntoday" runat="server" Text="今天" OnClick="btntoday_Click" /> <br/>其他日期：<asp:TextBox
                            ID="txtbegintime" CssClass="inptext" runat="server" onfocus="WdatePicker()"></asp:TextBox>&nbsp;
                &nbsp; &nbsp; 至 &nbsp;&nbsp;
                <asp:TextBox CssClass="inptext" ID="txtendtime" onfocus="WdatePicker()" runat="server"></asp:TextBox>
                <br />
                会员编号：<asp:TextBox ID="txtnumber" CssClass="inptext " runat="server"></asp:TextBox>
               <br/> 会员姓名：<asp:TextBox ID="txtname" CssClass="inptext "
                    runat="server"></asp:TextBox>
              <br/> 汇款金额：<asp:TextBox ID="txtrmmoney" onblur="vlidatenaa()" MaxLength="15" CssClass="inptext"
                    runat="server"></asp:TextBox>
            </div>
            <asp:GridView ID="gvdormitlist" Width="100%" runat="server" AutoGenerateColumns="False" CssClass="tablemb"
                CellPadding="0" BackColor="White" BorderWidth="1px" ForeColor="Black"
                GridLines="Vertical" OnRowDataBound="gvdormitlist_RowDataBound" OnRowCommand="gvdormitlist_RowCommand">
             
                <Columns>
                    <asp:TemplateField HeaderText="公司审核">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lkbtnmncomsur" Visible="false" CommandArgument='<%#Eval( "Remittancesid") %>' OnClientClick="return confirm('确定收到该笔汇款了吗？');"
                                CommandName="spa" runat="server" Text="到帐"> </asp:LinkButton>
                            <asp:LinkButton ID="lkbtncomsure" Visible="false" CommandArgument='<%#Eval( "Remittancesid") %>'  OnClientClick="return confirm('确定收到该笔汇款了吗？');"
                                CommandName="sup" runat="server" Text="到帐"> </asp:LinkButton>
                            <asp:Label ID="lblsplit" Visible="false" runat="server" Text="/"></asp:Label>
                            <asp:LinkButton ID="lkbtnnoremit" Visible="false" CommandArgument='<%#Eval( "Remittancesid") %>'  OnClientClick="return confirm('确定收到该笔汇款了吗？');"
                                CommandName="nop" runat="server" Text="迟到账"> </asp:LinkButton> 
                            <asp:Label ID="lblsplit1" Visible=false runat="server" Text="/"></asp:Label><asp:LinkButton ID="lkbtnnopay" Visible=false
                                    CommandName="nou" CommandArgument='<%# Eval("number") %>' runat="server" Text="未到账"> </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="汇款金额" DataField="totalrmbmoney" DataFormatString="{0:c}" />
                    <asp:TemplateField HeaderText="自助充值时间">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbldatemb" runat="server" Text="未充值"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="汇入账户">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("RemBankBook") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAccount" runat="server" Text=""></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="150px" />
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="姓名" DataField="name" />
                    <asp:BoundField HeaderText="编号" DataField="number" />
                    <asp:TemplateField HeaderText="汇款凭证">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lkbtnshowphoto" runat="server" Text="查看"> </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                        <asp:TemplateField HeaderText="提示音">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lkbtnclosesound"   CommandArgument='<%#Eval( "Remittancesid") %>'
                                CommandName="cls" runat="server" Text="关闭"> </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                </Columns>
                <EmptyDataTemplate>
                    <table cellpadding="0" cellspacing="0" class="tablema" width="100%">
                        <tr>
                            <th>
                                公司审核
                            </th>
                            <th>
                                汇款金额
                            </th>
                            <th>
                                自助充值时间
                            </th>
                            <th>
                                汇款账户
                            </th>
                            <th>
                                姓名
                            </th>
                            <th>
                                编号
                            </th>
                             
                           
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <FooterStyle BackColor="#CCCC99" />
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            <div id="spage" runat="server">
                <asp:HiddenField ID="hidcurpge" Value="1" runat="server" />
            </div>
        </div>
    </center>
    <div id="divpic" style="position: absolute; background-color: #ffeecc; width: 500px;
        border: #996633 solid 2px; display: none; padding: 0px; color: #111;">
        <div style="float: right; cursor: pointer; background: #eee; position: absolute;
            border: 1px solid #ccc; color: #111; width: 20px; height: 10px; padding: 0px;"
            onclick="closediv()">
            关闭
        </div>
        <center>
            <img alt="汇款凭证" id="imgshow" src="" width="500px" />
        </center>
    </div>
    <div id="bkdiv" style="position: absolute; display: none; width: 100%; top: 0px;
        left: 0px; filter: alpha(opacity=40); background-color: #ccc;">
        &nbsp;
    </div>
    <div id="soundplay" style="display: none;">
    </div>
    </form>
</body>
</html>

<script type="text/javascript" language="javascript">
var divpic;
var bkdiv;
function showphoto(picurl){
  var imgs=document.getElementById("imgshow");
  imgs.src=picurl; 
   divpic= document.getElementById("divpic");
   bkdiv=document.getElementById("bkdiv");
     var sl= document.body.clientWidth
; //document.body.clientWidth; 
   var st= window.screen.availHeight  ;//  document.body.clientHeight;
  
  bkdiv.style.width=sl;
  bkdiv.style.height=st;
  
    divpic.style.left=(sl-500)/2;
    divpic.style.top=(st-600)/2;;
       divpic.style.zIndex=101;
   bkdiv.style.zIndex=100;
   divpic.style.display="block";
   bkdiv.style.display="block";

  return false;
}
function closediv(){
   divpic.style.display="none";
   bkdiv.style.display="none";
 }
 
function getstyle(){

   var id=<%=oc %>;

  var ele= document.getElementById("btn"+id)
 if(ele!=null)
  ele.className="btnc";
  
 
 
 }
// function  settimesong()
// {
//  
//      var v=AjaxClass.Getnewrmcount().value;  
// 
//    if(v>0)
//    {    
//  document.getElementById('player').Play(); 
//    }
// 
// }
 
 function submitfrom(){
 
  __doPostBack('LinkButton1','');
  // document.forms['form1'].submit();
  }
 
       var control;
function showPlayer(url){//播放代码

 var fileType=new Array();
 fileType[0] =new Array(".mpg", ".mpeg",".asf",".wmv",".mp4");//MP 视频
 fileType[1] =new Array(".mid",".mmf",".mp3",".wav",".midi",".wma");//MP 音频
 fileType[2] =new Array(".rm",".rmvb");//RP 视频
 fileType[3] =new Array(".amr");//RP 音频
 fileType[4] =new Array(".3gp",".3gpp");//3GP 视频 

  var playerType = -1;
 lastname = url.substring(url.lastIndexOf("."),url.length);
 lastname = lastname.toLowerCase();  for(j=0;j<=fileType.length;j++){
  for (i = 0; i < fileType[j].length; i++) {
   if (fileType[j][i] == lastname) {playerType=j;break;}
   }
  if(playerType > -1)break;
  } 

  var divsound=document.getElementById("soundplay");
    
   if(playerType==0)
  divsound.innerHTML='<embed style="display:none;" type="application/x-mplayer2" pluginspage="http://www.microsoft.com/Windows/Downloads/Contents/Products/MediaPlayer/" volume=100 src="'+url+'" width=280 height=255 showstatusbar=false showcontrols="true" controls="ControlPanel" autostart="true"></embed>';
 else if(playerType==1)     
   divsound.innerHTML='<embed id="player" style="display:none;" type="application/x-mplayer2" pluginspage="http://www.microsoft.com/Windows/Downloads/Contents/Products/MediaPlayer/" volume=100 src="'+url+'" width=280 height=69 displaysize=0 showdisplay=false showstatusbar=true showcontrols="true" autostart="false"></embed>';
   else if(playerType==2)
    divsound.innerHTML='<embed style="display:none;" type="audio/x-pn-realaudio-plugin" src="'+url+'" width=280 autostart="true" loop="true" repeat="true" controls="ImageWindow,ControlPanel" height=251></embed>';
 else if(playerType==3)
    divsound.innerHTML='<embed style="display:none;" type="audio/x-pn-realaudio-plugin" src="'+url+'" width=280 autostart="true" loop="true" repeat="true" controls="ControlPanel,StatusBar" height=60></embed>';
 else if(playerType==4)
    divsound.innerHTML='<embed style="display:none;" type="video/quicktime" pluginspage="http://www.apple.com/quicktime/download/index.html" volume=100 src="'+url+'" width=180 height=155 showstatusbar=false showcontrols="true" autostart="true" palette="transparent"></embed>';
 else
    divsound.innerHTML='文件扩展名不支持!';

      
      }
      function vlidatenaa(){
         var money=document.getElementById("txtrmmoney");
         
         var mon=money.value;
         
         if(isNaN(mon))
         {
          alert("查询金额请输入数字！");
          money.value="";
         }
        
      }
      
 //window.onload=function(){  getstyle() ;   showPlayer('../sound/mail.wav');   setTimeout("settimesong()","1000") ;     }
 
 
 
</script>

