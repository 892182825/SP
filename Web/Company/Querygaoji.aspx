<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Querygaoji.aspx.cs" Inherits="Company_Querygaoji" %>
<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<%@ Register src="../UserControl/ExpectNum.ascx" tagname="ExpectNum" tagprefix="uc1" %>

<%@ Register src="../UserControl/Country.ascx" tagname="Country" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<link href="CSS/Company.css" rel="stylesheet" type="text/css" />
<script src="../JS/QCDS2010.js" type="text/javascript"></script>
 <script language="javascript" type="text/javascript" src="../js/SqlCheck.js"></script>
 <script language="javascript" type="text/javascript">
 function caxuaa()
 {
 var cardType=document.getElementById("DDLtable").options[document.getElementById("DDLtable").selectedIndex].value;
 var tr1 = document.getElementById("tr1");
 var tr2 = document.getElementById("tr2");
 var tr3 = document.getElementById("tr3");
 var tr4 = document.getElementById("tr4");
 var tr4 = document.getElementById("tr5");
 //debugger;
 if (cardType == "Pro_DailyByAreatable" || cardType == "Pro_DailyByStoretable")
      {
        tr2.style.display="";
        tr1.style.display = "none";
        tr3.style.display = "";
        tr4.style.display = "none";
        tr5.style.display = "none";
        tr6.style.display = "none";
       }
       else  if (cardType == "Pro_SummaryOfAllSalesByYear_MonthSalesInfotable")
          {
               tr1.style.display="none";
               tr2.style.display = "";
               tr3.style.display = "none";
               tr4.style.display = "none";
               tr5.style.display = "";
               tr6.style.display = "none";
          }
          else if(cardType =="storeproducttable")
          {
             tr1.style.display = "none";
             tr2.style.display = "none";
             tr3.style.display = "none";
             tr4.style.display = "none";
             tr5.style.display = "none";
             tr6.style.display = "";
          }
          else
          {
             tr1.style.display = "none";
             tr2.style.display = "none";
             tr3.style.display = "none";
             tr4.style.display = "none";
             tr5.style.display = "none";
             tr6.style.display = "none";
          }    
           
 }
   window.onload =function()
        {
	          try
	          {
	         // debugger;
                 caxuaa();
	          }
	         catch(e)
	         {}
	         }
 </script>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查询</title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
        <script src="../JS/QCDS2010.js" type="text/javascript"></script>
    <script language="javascript" src="../javascript/Mymodify.js" type="text/javascript"></script>
     <script type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0"  class="biaozzi">
    <tr id="tr1" style="display:none"><td> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%=GetTran("000201", "请选择查询期数")%>：
                  <uc1:ExpectNum ID="DropDownQiShu" runat="server" IsRun="True" /> 
                  </td></tr>
    <tr id="tr2" style="display:none"><td> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%=GetTran("007599", "统计区域")%>：<uc2:Country ID="area" runat="server" /></td></tr>
           
    <tr id="tr3" style="display:none"><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%=GetTran("007600", "查询日期")%>：<asp:TextBox ID="Date1" runat="server" onfocus="WdatePicker()" 
                     CssClass="Wdate"  ></asp:TextBox>
                   &nbsp;</td></tr> 
     <tr id="tr4" style="display:none"><td> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%=GetTran("000559", "开始时间")%>：<asp:TextBox ID="Date2" 
             runat="server" onfocus="WdatePicker()" 
                     CssClass="Wdate"  ></asp:TextBox>
                   <%=GetTran("005932", "结束时间")%>：
                   &nbsp;<asp:TextBox ID="Date3" runat="server" onfocus="WdatePicker()" 
                     CssClass="Wdate"  ></asp:TextBox>
                   </td></tr>
      <tr id="tr5" style="display:none"><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%=GetTran("001974", "年   度")%>：&nbsp;&nbsp;&nbsp;&nbsp<asp:DropDownList ID="DDLyear" runat="server">
            <asp:ListItem>2005</asp:ListItem>
            <asp:ListItem>2006</asp:ListItem>
            <asp:ListItem>2007</asp:ListItem>
            <asp:ListItem>2008</asp:ListItem>
            <asp:ListItem>2009</asp:ListItem>
            <asp:ListItem>2010</asp:ListItem>
            <asp:ListItem>2011</asp:ListItem>
            <asp:ListItem>2012</asp:ListItem>
            <asp:ListItem>2013</asp:ListItem>
            <asp:ListItem>2014</asp:ListItem>
            <asp:ListItem>2015</asp:ListItem>
            <asp:ListItem>2016</asp:ListItem>
            <asp:ListItem>2017</asp:ListItem>
            <asp:ListItem>2018</asp:ListItem>
            <asp:ListItem>2019</asp:ListItem>
            <asp:ListItem>2020</asp:ListItem>
        </asp:DropDownList>
&nbsp;<%=GetTran("001975", "月份")%>：<asp:DropDownList ID="DDLmonth" runat="server">
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
            <asp:ListItem>3</asp:ListItem>
            <asp:ListItem>4</asp:ListItem>
            <asp:ListItem>5</asp:ListItem>
            <asp:ListItem>6</asp:ListItem>
            <asp:ListItem>7</asp:ListItem>
            <asp:ListItem>8</asp:ListItem>
            <asp:ListItem>9</asp:ListItem>
            <asp:ListItem>10</asp:ListItem>
            <asp:ListItem>11</asp:ListItem>
            <asp:ListItem>12</asp:ListItem>
        </asp:DropDownList></td></tr>
        <tr id="tr6" style="display:none" ><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%=GetTran("000037","服务机构编号")%>：<asp:TextBox 
                ID="Txtstoreid" runat="server" MaxLength="10"></asp:TextBox>
&nbsp;&nbsp;&nbsp;<%=GetTran("000895", " 商品名称")%>：<asp:DropDownList ID="DDLproductname" runat="server">
            </asp:DropDownList>
                </td></tr>
    </table> 
    <asp:Panel ID="Panel1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0"  class="biaozzi">
        <tr><td>
       <table width="100%" border="0" cellspacing="0" cellpadding="0"  class="biaozzi">
            <tr >
              <td> 
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:Button  id="BtnCheckAll" runat="server"  
                     onclick="BtnCheckAll_Click"  CssClass="anyes" 
                      style="cursor:hand; width: 36px;"></asp:Button>&nbsp;
				  <asp:button  id="BtnCancelAll" runat="server"  Text="用保存项查询" 
                     onclick="BtnCancelAll_Click" CssClass="anyes" style="cursor:hand;"></asp:button>&nbsp;
				 
                  <asp:Button ID="Btnview" runat="server" CssClass="anyes" 
                      onclick="Btnview_Click" style="cursor:hand;" Text="查询结果" Visible="False" />
            </td>
            </tr>
       </table>
       </td>
       </tr>
       <tr><td>&nbsp;</td></tr>
       <tr> <td>
       <table width="100%" border="0" cellspacing="0" cellpadding="0"  class="biaozzi">
         <tr>
            <td align="right"><%=GetTran("007603", "选中的表")%> ：</td>
            <td colspan=2>
                <asp:TextBox ID="Txttablecn" runat="server" Height="68px" TextMode="MultiLine" 
                    Width="820px" Enabled="False"></asp:TextBox>
                   </td>
          </tr>
         <tr style="display :none ">
             <td align="right"> <%=GetTran("007603", "选中的表")%> ：</td>
             <td colspan=2>
                 <asp:TextBox ID="Txttableen" runat="server" Height="68px" TextMode="MultiLine" 
                     Width="820px" Enabled="False"></asp:TextBox>
                   </td>
         </tr>
         <tr>
             <td align="right"><%=GetTran("007604", "请选择查询的表")%> ：</td>
             <td>
               <asp:DropDownList ID="DDLtable" runat="server" onChange="caxuaa()">
               </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
               <asp:Button ID="Addtable" runat="server" Text="增加" onclick="Addtable_Click" CssClass="anyes" style="cursor:hand;" />
                <asp:Button ID="Deltable" runat="server" Text="删除" onclick="Deltable_Click" CssClass="anyes" style="cursor:hand;" />
             </td>
             <td>&nbsp;
                
             </td>
         </tr> 
         <tr><td>&nbsp;</td></tr>
          <tr>
            <td align="right"></td>
            <td colspan=2>
                <asp:TextBox ID="Txtglcn" runat="server" Height="68px" TextMode="MultiLine" 
                    Width="820px" Enabled="False"></asp:TextBox>
                   </td>
          </tr>
          <tr style="display :none ">
            <td align="right"> <%=GetTran("007606", "表关联")%> ：</td>
            <td colspan=2>
                <asp:TextBox ID="Txtglen" runat="server" Height="68px" TextMode="MultiLine" 
                    Width="820px" Enabled="False"></asp:TextBox>
                   </td>
          </tr>
          <tr style="display :none ">
            <td align="right"> <%=GetTran("007607", "表关联条件")%>：</td>
            <td colspan=2>
                <asp:TextBox ID="Txtgltj" runat="server" Height="68px" TextMode="MultiLine" 
                    Width="820px" Enabled="False"></asp:TextBox>
                   </td>
          </tr>
          <tr>
            <td align="right"><%=GetTran("007608", "选择表关联")%> ：</td>
            <td >
               <asp:DropDownList ID="DDLuoncol" runat="server">
               </asp:DropDownList> &nbsp;&nbsp;&nbsp;
               <asp:DropDownList ID="DDLgltj" runat="server">
                   <asp:ListItem Value=" INNER JOIN">内连接</asp:ListItem>
                   <asp:ListItem Value=" LEFT JOIN">左连接</asp:ListItem>
                   <asp:ListItem Value="RIGHT JOIN">右连接</asp:ListItem>
               </asp:DropDownList>&nbsp;&nbsp;&nbsp;
               <asp:DropDownList ID="DDLuoncol2" runat="server">
               </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
               <asp:Button ID="Addtable0" runat="server" Text="增加" onclick="Addtable0_Click" CssClass="anyes" style="cursor:hand;" />
                <asp:Button ID="Deltable0" runat="server" Text="删除" onclick="Deltable0_Click" CssClass="anyes" style="cursor:hand;" />
                   </td>
            <td align="left" >&nbsp;
                
                    
                   </td>
          </tr>
       </table>
       </td> 
       </tr>
       <tr><td>&nbsp;</td></tr>
       <tr><td>
       <table width="100%" border="0" cellspacing="0" cellpadding="0"  class="biaozzi">
          <tr>
            <td align="right"  width="90px"><%=GetTran("007612", "选择显示项")%>：</td>
            <td width="310px">
                <asp:ListBox ID="ListBoxcn1" runat="server" Height="160px" Width="310px">
                </asp:ListBox>
                   </td>
            <td  width="70px">
                <asp:Button ID="Button1" runat="server" Text="&gt;&gt;" 
                    onclick="Button1_Click" Width="60px" CssClass="anyes" style="cursor:hand;" />
                <br />
                <asp:Button ID="Button2" runat="server" Text="&gt;" onclick="Button2_Click" 
                    Width="60px" CssClass="anyes" style="cursor:hand;" />
                <br />
                <br />
                <asp:Button ID="Button3" runat="server" Text="&lt;" onclick="Button3_Click" 
                    Width="60px"  CssClass="anyes" style="cursor:hand;"/>
                <br />
                <asp:Button ID="Button4" runat="server" Text="&lt;&lt;" 
                    onclick="Button4_Click" Width="60px" CssClass="anyes" style="cursor:hand;" />
                   </td>
            <td width="440px" align="left">
                <asp:ListBox ID="ListBoxcn2" runat="server" Height="160px" Width="310px">
                </asp:ListBox>
                   </td>
          </tr>
        </table>
       </td> 
       </tr>
       <tr><td>&nbsp;</td></tr>
       <tr>
       <td>
       <table width="100%" border="0" cellspacing="0" cellpadding="0"  class="biaozzi">
          <tr>
             <td align="right"><%=GetTran("000838", "查询条件")%>：</td>
             <td colspan=3>
                 <asp:TextBox ID="Txttjcn" runat="server" Height="68px" TextMode="MultiLine" 
                     Width="820px" Enabled="False"></asp:TextBox>
                   </td>
             
          </tr>
           <tr style="display :none ">
             <td align="right"><%=GetTran("000838", "查询条件")%>：</td>
             <td colspan=3>
                 <asp:TextBox ID="Txttjen" runat="server" Height="68px" TextMode="MultiLine" 
                     Width="820px" Enabled="False"></asp:TextBox>
                   </td>
          </tr>
           <tr>
             <td align="right"><%=GetTran("007613", "选择查询条件")%>：</td>
             <td>
                 <asp:DropDownList ID="DDLtj" runat="server" AutoPostBack="True" 
                     CausesValidation="True" onselectedindexchanged="DDLtj_SelectedIndexChanged">
                 </asp:DropDownList>
                  <asp:DropDownList ID="DDLtjfu2" runat="server">
                     <asp:ListItem Value="like">包含</asp:ListItem>
                     <asp:ListItem Value="not like">不包含</asp:ListItem>
                     <asp:ListItem>=</asp:ListItem>
                 </asp:DropDownList>
                 <asp:DropDownList ID="DDLtjfu1" runat="server" Visible="False">
                     <asp:ListItem>&gt;=</asp:ListItem>
                     <asp:ListItem>&gt;</asp:ListItem>
                     <asp:ListItem>&lt;</asp:ListItem>
                     <asp:ListItem>&lt;=</asp:ListItem>
                     <asp:ListItem>=</asp:ListItem>
                 </asp:DropDownList>
                 <asp:TextBox ID="Txttjvalue" runat="server" MaxLength="200"></asp:TextBox>
                 <asp:TextBox ID="DatePicker1" runat="server" onfocus="WdatePicker()" 
                     CssClass="Wdate" Visible="False"  ></asp:TextBox>
                   </td>
             <td>&nbsp;</td>
             <td>&nbsp;</td>
          </tr>
           <tr>
             <td align="right"><%=GetTran("000015", "操作")%>:</td>
             <td colspan=3>
                 <asp:Button ID="Btntiaojian1" runat="server" Text="并且" 
                     onclick="Btntiaojian1_Click" Enabled="False"  CssClass="anyes" style="cursor:hand;"/>&nbsp;&nbsp;
                 <asp:Button ID="Btntiaojian2" runat="server" Text="或者" 
                     onclick="Btntiaojian2_Click" Enabled="False" CssClass="anyes" style="cursor:hand;" />&nbsp;&nbsp;
                 <asp:Button ID="Btntiaojian3" runat="server" Text="（" 
                     onclick="Btntiaojian3_Click" CssClass="anyes" style="cursor:hand;" />&nbsp;&nbsp;
                 <asp:Button ID="Btntiaojian4" runat="server" Text="）" 
                     onclick="Btntiaojian4_Click" CssClass="anyes" style="cursor:hand;" />&nbsp;&nbsp;
                 <asp:Button ID="Btntiaojian5" runat="server" Text="增加子条件" Width="98px" 
                     onclick="Btntiaojian5_Click" CssClass="anyes" style="cursor:hand;" />&nbsp;&nbsp;
                 <asp:Button ID="Button6" runat="server" Text="删除当前条件" onclick="Button6_Click"  CssClass="anyes" style="cursor:hand;"/>
                   </td>
          </tr>
       </table>
       </td>
       </tr>
       <tr><td>&nbsp;</td></tr>
       <tr>
       <td>
       <table width="100%" border="0" cellspacing="0" cellpadding="0"  class="biaozzi">
         <tr >
           <td align="right"><%=GetTran("007617", "排序条件")%>：</td>
           <td colspan=2>
                 <asp:TextBox ID="Txtpaixucn" runat="server" Height="68px" TextMode="MultiLine" 
                     Width="820px" Enabled="False"></asp:TextBox>
                   </td>
         </tr>
         <tr style="display :none ">
           <td align="right"><%=GetTran("007617", "排序条件")%>：</td>
           <td colspan=2>
                 <asp:TextBox ID="Txtpaixuen" runat="server" Height="68px" TextMode="MultiLine" 
                     Width="820px" Enabled="False"></asp:TextBox>
                   </td>
         </tr>
         <tr>
           <td align="right"><%=GetTran("007619", "增加排序条件")%>：</td>
           <td>
               <asp:DropDownList ID="DDLpaixu" runat="server">
               </asp:DropDownList>
                <asp:Button ID="BtnpaixuAdd" runat="server" Text="增加" 
                   onclick="BtnpaixuAdd_Click"  CssClass="anyes" style="cursor:hand;"/>
               <asp:Button ID="Btnpaixudel" runat="server" Text="删除" 
                   onclick="Btnpaixudel_Click"  CssClass="anyes" style="cursor:hand;"/>
                   </td>
           <td>&nbsp;
              
                   </td>
         </tr>
         <tr>
           <td align="right"><%=GetTran("007380", "排序方式")%>：</td>
           <td colspan=2>
               <asp:RadioButtonList ID="RBLpaixu" runat="server" 
                   RepeatDirection="Horizontal">
                   <asp:ListItem Value="ASC" Selected="True">不排序</asp:ListItem>
                   <asp:ListItem Value="ASC">升序</asp:ListItem>
                   <asp:ListItem Value="DESC">降序</asp:ListItem>
               </asp:RadioButtonList>
                   </td>
         </tr>
       </table> 
       </td>
       </tr>
       
       </table>
    
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" Visible="false" >
         <table width="100%" border="0" cellspacing="0" cellpadding="0"  class="biaozzi">
          <tr><td>&nbsp;&nbsp;
              <asp:Button ID="Btnback" runat="server" onclick="Btnback_Click" 
                  Text="返回查询条件设置页"  CssClass="anyes" style="cursor:hand;"/>&nbsp;&nbsp;&nbsp;&nbsp;
              <asp:Button ID="savecaxun" runat="server" Text="保存查询条件" CssClass="anyes" 
                  style="cursor:hand;" onclick="savecaxun_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <asp:TextBox ID="Txtsavecx" runat="server" Width="168px" MaxLength="100"></asp:TextBox>
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <asp:Label ID="Label1" runat="server"  ><%=GetTran("007634", "请选择保存的查询项：")%></asp:Label>
              <asp:DropDownList ID="DDLbaocunxiang" runat="server">
              </asp:DropDownList>
              <asp:Button ID="Btncaxun" runat="server" Text="查询"  CssClass="anyes" 
                  style="cursor:hand; height: 23px;" onclick="Btncaxun_Click"/>
              </td>
           </tr>
          <tr style="display:none"><td valign="bottom" ><%=GetTran("007635", "您使用的查询语句")%>：<asp:TextBox ID="Txtcaxun" runat="server" Height="68px" 
                  ReadOnly="True" TextMode="MultiLine" Width="80%"></asp:TextBox>
              </td></tr>
              <tr><td>&nbsp;</td></tr>
          <tr><td>
              <asp:GridView ID="GridView1" runat="server" Width="100%" AllowSorting="false" AutoGenerateColumns="true"
               BorderStyle="Solid" AllowPaging="True" 
                  onpageindexchanging="GridView1_PageIndexChanging" 
                  ondatabound="GridView1_DataBound" >
               <AlternatingRowStyle BackColor="#F1F4F8" />
                <HeaderStyle CssClass="tablemb" />
                <RowStyle HorizontalAlign="Center" />
              </asp:GridView>
           </td></tr>
           <tr><td>
              <asp:GridView ID="GridView2" runat="server" Width="100%" AllowSorting="false" AutoGenerateColumns="true"
               BorderStyle="Solid" AllowPaging="True" 
                  onpageindexchanging="GridView2_PageIndexChanging" 
                  ondatabound="GridView2_DataBound" >
               <AlternatingRowStyle BackColor="#F1F4F8" />
                <HeaderStyle CssClass="tablemb" />
                <RowStyle HorizontalAlign="Center" />
              </asp:GridView>
           </td></tr>
         </table> 
        
    </asp:Panel>
    </form>
</body>
</html>
