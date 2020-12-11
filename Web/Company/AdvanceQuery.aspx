<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdvanceQuery.aspx.cs" Inherits="Company_AdvanceQuery" %>

<%@ Register Src="../UserControl/ExpectNum.ascx" TagName="ExpectNum" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>高级查询</title>
    <script src="../JS/jquery-1.2.6.js" type="text/javascript" language="javascript"></script>
        <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(
        function() {

            $('#TextContent').click(
                    function() {
                        var ddl = $('#DropDownCondition').val();
                        if (ddl == "MemberInfo.RegisterDate" || ddl == "dateadd(hh,8,MemberInfo.advtime)" || ddl == "MemberInfo.Birthday") {
                            WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' });
                        } else { $(this).val(""); }
                    }
                );
            $('#TextContent2').click(
                    function() {
                        var ddl = $('#DropDownCondition2').val();
                        if (ddl == "MemberInfo.RegisterDate" || ddl == "dateadd(hh,8,MemberInfo.advtime)" || ddl == "MemberInfo.Birthday") {
                            WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' });
                        } else { $(this).val(""); }
                    }
                );
        }
    )
</script>
    <script>
		   function ChangeSearch()
		   {
		     if(document.all.Panel1.style.display =="")
		     {
		      document.all.Panel1.style.display ="none";
		      document.all.Panel2.style.display =""; 
		      document.all.TextBox1.value="";
		    
		      }  
		     else
		      {
		    
		        document.all.Panel1.style.display ="";
		        document.all.Panel2.style.display ="none";
		        document.all.TextContent.value="";
		        document.all.TextXiaoqu.value="";
		       
		      }
		   }
		   function Display()
		   {
		     document.all.Panel1.style.display ="none";
		     document.all.TextContent.value="";
		     document.all.TextXiaoqu.value="";
		     document.all.TextBox1.value="";
		     document.all.check.checked=false;
		     
		   }
    </script>

    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../js/SqlCheck.js"></script>
    <script language="javascript" type="text/javascript">
    function CheckText()
	{
		//这个方法是只有1个lkSubmit按钮时候 可直接用
		filterSql();
	}

	

    </script>

    <script type="text/javascript">
	function down2()
	{
		if(document.getElementById("divTab2").style.display=="none")
		{
			document.getElementById("divTab2").style.display="";
			document.getElementById("imgX").src="images/dis1.GIF";
			
		}
		else
		{
			document.getElementById("divTab2").style.display="none";
			document.getElementById("imgX").src="images/dis.GIF";
		}
	}
    </script>

    <script language="javascript">
     function secBoard(n)
  
  {
  //  document.Form1.sa.value=n;
  //  document.getElementById("s").click();
       for(i=0;i<secTable.cells.length;i++)
      secTable.cells[i].className="sec1";
    secTable.cells[n].className="sec2";
    for(i=0;i<mainTable.tBodies.length;i++)
      mainTable.tBodies[i].style.display="none";
    mainTable.tBodies[n].style.display="block";
  }
    </script>

    <style type="text/css">
        .style3
        {
            width: 53px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td style="height: 225px">
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
                    <tr>
                        <td>
                            <%=GetTran("000201", "请选择查询期数")%>：
                            <uc1:ExpectNum ID="DropDownQiShu" runat="server" IsRun="True" />
                            <asp:DropDownList ID="DropCurrency" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropCurrency_SelectedIndexChanged"
                                Visible="false">
                            </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="lkSubmit" runat="server" Text="提交" Style="display: none" OnClick="lkSubmit_Click"></asp:LinkButton>
                            <input class="anyes" id="bSubmit" onclick="CheckText()" type="button" value="<%=GetTran("000048", "查 询")%>" />
                            <asp:Button ID="BtnQuery" Visible="false" runat="server" Text="查 询" OnClick="BtnQuery_Click"
                                CssClass="anyes" Style="cursor: hand;"></asp:Button>&nbsp;
                            <asp:Button ID="BtnCancelAll" runat="server" Text="重 选" OnClick="BtnCancelAll_Click"
                                CssClass="anyes" Style="cursor: hand;"></asp:Button>&nbsp;
                            <asp:Button ID="BtnCheckAll" runat="server" Text="全 选" OnClick="BtnCheckAll_Click"
                                CssClass="anyes" Style="cursor: hand;"></asp:Button>&nbsp;
                            <%=GetTran("007192", "查询记录")%>：
                            <asp:DropDownList ID="DropDownList2" runat="server" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:Button ID="Button2" runat="server" Text="删除查询记录" onclick="Button2_Click" CssClass="anyes" />
                        </td>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="添加查询记录" CssClass="anyes" OnClick="Button1_Click" />
                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table width="100%" border="1" cellpadding="0" cellspacing="0" class="biaozzi">
                    <tr>
                        <td>
                            <asp:DataList ID="DataConditions" runat="server" RepeatColumns="5" RepeatDirection="Horizontal"
                                Width="100%">
                                <ItemStyle Width="20%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:CheckBox ID="Check" Text='<%# DataBinder.Eval( Container.DataItem, "Name")%>'
                                        Checked='<%# DataBinder.Eval( Container.DataItem, "Check")%>' runat="server">
                                    </asp:CheckBox>
                                   
                                    <input id="Hidname" type="hidden" value='<%# DataBinder.Eval( Container.DataItem, "name")%>'
                                        name="Hidname" runat="server">
                                    <input id="HidValue" type="hidden" value='<%# DataBinder.Eval( Container.DataItem, "Key")%>'
                                            name="HidValue" runat="server">
                                    <!--<INPUT id="HidDispType" type="hidden" name="HidDispType" value='<%# DataBinder.Eval( Container.DataItem, "Key")%>' runat="server">-->
                                </ItemTemplate>
                            </asp:DataList>
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
                    <tr>
                        <td style="width: 100%">
                            <input id="check" onclick="ChangeSearch()" type="checkbox"><%=GetTran("000830", "使用关键词搜索")%>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%" colspan="3">
                            <asp:Panel ID="Panel1" runat="server" Width="100%">
                                <%=GetTran("000831", "关键词搜索")%>
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></asp:Panel>
                            <asp:Panel ID="Panel2" runat="server" Width="100%">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
                                    <tr>
                                        <td colspan="3">
                                            <span>
                                                <%=GetTran("000833", "只查询以")%>
                                                <asp:TextBox ID="TextXiaoqu" runat="server" MaxLength="10" Width="72px"></asp:TextBox>
                                                <span>
                                                    <%=GetTran("000835", "编号为起点的网络范围")%></span></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style3" colspan="3">
                                           <%=GetTran("007930", "子条件")%> 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="DropDownCondition" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="DropDownRelation" runat="server">
                                                            <asp:ListItem Value="like">包含</asp:ListItem>
                                                            <asp:ListItem Value="not like">不包含</asp:ListItem>
                                                            <asp:ListItem Value="&gt;">大于</asp:ListItem>
                                                            <asp:ListItem Value="&lt;">小于</asp:ListItem>
                                                            <asp:ListItem Value="&gt;=">大于等于</asp:ListItem>
                                                            <asp:ListItem Value="&lt;=">小于等于</asp:ListItem>
                                                            <asp:ListItem Value="=">等于</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextContent" runat="server" Width="167px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="DropDownList1" runat="server">
                                                <asp:ListItem Value=" and ">并且</asp:ListItem>
                                                <asp:ListItem Value=" or ">或者</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="DropDownCondition2" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="DropDownRelation2" runat="server">
                                                            <asp:ListItem Value="like">包含</asp:ListItem>
                                                            <asp:ListItem Value="not like">不包含</asp:ListItem>
                                                            <asp:ListItem Value="&gt;">大于</asp:ListItem>
                                                            <asp:ListItem Value="&lt;">小于</asp:ListItem>
                                                            <asp:ListItem Value="&gt;=">大于等于</asp:ListItem>
                                                            <asp:ListItem Value="&lt;=">小于等于</asp:ListItem>
                                                            <asp:ListItem Value="=">等于</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextContent2" runat="server" Width="167px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table class="biaozzi">
                                <tr>
                                    <td>
                                        <%=GetTran("000840", "排序方式")%>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownOrder" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownOrderKind" runat="server">
                                            <asp:ListItem Value="asc">正序</asp:ListItem>
                                            <asp:ListItem Value="desc">返序</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <%=GetTran("000864", "排序")%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
