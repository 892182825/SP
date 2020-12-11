<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuerySMS.aspx.cs" Inherits="Company_QuerySMS" %>
<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/Dtd/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>库存查询</title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />

   <script src="../JS/QCDS2010.js" type="text/javascript"></script>

    <script src="../JS/jquery.js" type="text/javascript"></script>
 


    <script src="js/tianfeng.js" type="text/javascript"></script>
   
    <script language="javascript">

  function CheckAll(obj)
  {
  if(obj==1)
  {
      var content = '<%=GetTran("007971", "请选择要删除的记录") %>';
      var flag = '<%=GetTran("005831", "确认要删除吗？")%>';
  }
  else
  {
      var content = '<%=GetTran("007972", "请选择要发送的记录") %>';
      var flag = '<%=GetTran("007973", "确认要发送吗？")%>';
  }
    var count = $("input[type=checkbox]:checked").size();
    if(count<=0)
    {
   
    alert(content);
    return false;
    }
    else if(!window.confirm(flag))
    {
    return false;
    }
    return true;
  }
  
  
  function ChangeAll(obj)
  {
  $("input[type=checkbox]").each(function (index,item){
$(item).attr("checked",$(obj).attr("checked"));
  })
  }
    </script>
  
     <style type ="text/css" >
     table{
         font-size:9pt;
         }
        .style1
        {
            height: 18px;
            width: 136px;
        }
        .style2
        {
            height: 24px;
            width: 136px;
        }
        .style3
        {
            height: 31px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td style="white-space: nowrap">
                <br />
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
                    <tr>
                        <td align="left" style="white-space: nowrap">
                          &nbsp;
                                <asp:linkbutton id="lkSubmit" Runat="server" Text="查 询" style="DISPLAY: none"></asp:linkbutton>
                            </input>
                            &nbsp;&nbsp;<asp:Button ID="btnSearch" runat="server" CssClass="anyes" 
                                onclick="btnSearch_Click" Text="查询" />
&nbsp;<%=GetTran ("005612","发送状态")%>：<asp:DropDownList ID="ddlState" runat="server" 
                                onselectedindexchanged="ddlState_SelectedIndexChanged">
                                <asp:ListItem Value="1">发送成功</asp:ListItem>
                                <asp:ListItem Value="0">发送失败</asp:ListItem>
                                <asp:ListItem Value="-1">--所有--</asp:ListItem>
                            </asp:DropDownList>
                        &nbsp;<%=GetTran ("005623","手机号码")%>：<asp:TextBox ID="txtMobile" runat="server" Width="101px"></asp:TextBox>
                            <%=GetTran ("000752","发送时间")%>：<asp:TextBox ID="txtDateS"  class="Wdate" onfocus="new WdatePicker();" 
                                runat="server" Width="101px"></asp:TextBox>
                            <%=GetTran ("000068","至")%><asp:TextBox ID="txtDateE" runat="server" class="Wdate" 
                                onfocus="new WdatePicker();" Width="111px"></asp:TextBox>  &nbsp;<%=GetTran ("000645","关键字")%>：<asp:TextBox 
                                ID="txtKeyWords" runat="server" Width="218px"></asp:TextBox>
                        &nbsp;</td>                      
                    </tr>
                </table>
                <br />
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="tablemb">
                    <tr>
                        <td>
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                                Width="100%" onrowcommand="GridView1_RowCommand">
                                <AlternatingRowStyle BackColor="#F1F4F8" />
                                <RowStyle HorizontalAlign="Center" />
                                <HeaderStyle CssClass="tablebt" />
                                <Columns>
                                <asp:TemplateField >
                                <HeaderTemplate>
                                    <asp:CheckBox ID="CheckBox2" onclick="ChangeAll(this)" runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                    
                                    <asp:HiddenField ID="HiddenField1"  value='<%#Eval("ID") %>' runat="server" />
                                </ItemTemplate>
                                </asp:TemplateField>
                              
                                
                                <asp:BoundField DataField="senddate" HeaderText="发送时间"></asp:BoundField>
                                
									<asp:BoundField DataField="sucflag" HeaderText="状态"></asp:BoundField> 
                                <asp:BoundField DataField="CustomerID" HeaderText="会员编号"/>
                                
                                     <asp:TemplateField HeaderText="会员昵称">
                                     <ItemTemplate>
                                     <%#GetPetName(Eval("CustomerID").ToString())%>
                                    
                                     </ItemTemplate>
                                     </asp:TemplateField>
                             <asp:BoundField DataField="mobile" HeaderText="手机号码"></asp:BoundField>
                                <asp:TemplateField HeaderText="发送内容" ItemStyle-Wrap="false">
                                        <ItemTemplate>
                                        <div style="width:100%;" title ='<%# DataBinder.Eval(Container, "DataItem.sendMsg")%>'>
                                             <div style='width:400px;white-space:nowrap;text-overflow:ellipsis;overflow:hidden;float:left;'> <asp:Label ID="lblPresetMsg" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.sendMsg")%>'></asp:Label></div>
                                        </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>   
                                  
                                       <asp:TemplateField>
                                <HeaderTemplate>重发</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton2" OnClientClick="return checkC();" CommandArgument='<%#Eval("ID")+","+Eval("mobile")+","+Eval("CustomerID")+","+Eval("sendMsg")+","+Eval("Category") %>' CommandName="AgainSend" Visible='<%#Eval("sucflag").ToString() == GetTran("000000", "成功") ? false : true%>' runat="server"><%=GetTran ("000000","重发")%></asp:LinkButton>
                                </ItemTemplate>
                                </asp:TemplateField>
                                    
									<asp:TemplateField HeaderText="删除">
									<ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" OnClientClick="return checkD();" CommandName="Del" CommandArgument='<%#Eval("ID") %>' runat="server"><%#GetTran("000022","删除")%></asp:LinkButton>
									</ItemTemplate>
									</asp:TemplateField>                                                                 
                                </Columns>
                               
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="white-space: nowrap">
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr><td>
                    <asp:Button ID="Button1" OnClientClick="return CheckAll(1)" runat="server" 
                        CssClass="anyes" Text="多选删除" onclick="Button1_Click" />
                    <asp:Button ID="Button2" runat="server" OnClientClick="return CheckAll(2)" 
                        CssClass="anyes" Text="多选重发" Visible="false" onclick="Button2_Click" />
                    </tr>
                    <tr>
                        <td align="right" style="white-space: nowrap">
                            <uc1:Pager ID="Pager1" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        </table>
    <br />
    <div id="cssrain" style="width:100%">
      <table width="99%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
        <tr>
          <td width="80"><table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTableOnly">
              <tr>
                <td class="secOnly" onclick="secBoardOnly(0)">
                    <span id="span1" title="" onmouseover="cut()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
                </td>
              </tr>
          </table></td>
          <td><a href="#"><img src="images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX" onclick="down2()"/></a></td>          
        </tr>
      </table>
	  <div id="divTab2">
      <table width="99%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
        <tbody style="DISPLAY: block" id="tbody0">
          <tr>
            <td valign="middle" style="padding-left:20px"><table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td>
                                                                        <%=GetTran("005834", "短信查询")%><br /> 
                    1.   1.<%=GetTran("006855", "查看公司所有发出的短信及内容")%><br />   
                  </td>
                </tr>
            </table></td>
          </tr>
        </tbody>
        <tbody style="DISPLAY: none" id="tbody1">
          <tr>
            <td style="padding-left:20px"><table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td></td>
                </tr>
            </table></td>
          </tr>
        </tbody>
      </table>
	  </div>
    </div>
       
             
    </form>
    
    <script language="javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
</body>
</html>
<script language="javascript">
    function checkC() {
        if (confirm('<%=GetTran("007892","确认要重发吗？") %>')) {
            return true;
        } else {
        return false;
        }
    }
    function checkD() {
        if (confirm('<%=GetTran("000248","确定要删除吗？") %>')) {
            return true;
        } else {
        return false;
        }
    }
</script>