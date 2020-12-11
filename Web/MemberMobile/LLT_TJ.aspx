<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LLT_TJ.aspx.cs" Inherits="LLT_TJ" %>
<%@ Register Src="~/UserControl/MemberTop.ascx" TagName="top" TagPrefix="uc1"  %>
<%@ Register Src="~/UserControl/MemberBottom.ascx" TagName="bottom" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>推荐链路图</title>
    <link href="CSS/detail.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        
        .treeTable
        {
            border-left:rgb(218,218,218) solid 1px;
            border-top:rgb(218,218,218) solid 1px;
            
            color:rgb(53,53,53);
        }
        
        .treeTable td
        {
            border-right:rgb(218,218,218) solid 1px;
            border-bottom:rgb(218,218,218) solid 1px;
            
            white-space:nowrap;
            text-align:right;
            padding:0px 5px 0px 5px;
            height:20px;
        }
        .treeTable th
        {
            background-image:url(images/detail-table-bg_03.png);height:25px;
            border-right:rgb(218,218,218) solid 1px;
        }
        
        a
        {
        	color:rgb(53,53,53);
        }

    </style>
    
    <script type="text/javascript">
        function setTableRowColor()
        {
             var trarr = document.getElementById("GridView1").getElementsByTagName("tr");
             var _color="";
             
             for(var i=1;i<trarr.length;i++)
             {
                if(i%2==0)
                    trarr[i].style.backgroundColor="rgb(242,242,242)";
                else
                    trarr[i].style.backgroundColor="";
                    
                trarr[i].onmouseover=function()
                {
                    _color=this.style.backgroundColor;
                    this.style.backgroundColor="rgb(255,255,204)";
                };
                trarr[i].onmouseout=function()
                {
                    this.style.backgroundColor=_color;
                };
             }
        }
    </script>
</head>
<body style="font-size:10pt" onload="setTableRowColor()">
    <div class="MemberPage">
    
    <form id="form1" runat="server">
    <uc1:top runat="server" ID="top" />
    <div  style="padding-top:20px;padding-left:20px">
        <asp:Button ID="Button1" runat="server" class="anyes" Text="显示" OnClick="Button1_Click" />
        <span style="color:rgb(0,61,92)">
        <%=GetTran("000045", "期数")%>：
        </span>
        <asp:DropDownList ID="DDLQs" runat="server" style="border:#cccccc solid 1px;">
        </asp:DropDownList>
        <br><br>
        <span style="color:rgb(0,61,92)">
       <%=GetTran("007032","链路图")%>：
       </span>
        <asp:Literal ID="LitLLT" runat="server"></asp:Literal>
        <br><br>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" class="treeTable"  cellpadding="0" cellspacing="0" width="800" border="0">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <a href="LLT_TJ.aspx?endNumber=<%#SfType.getBH() %>&ThNumber=<%#Eval("Number").ToString().Split(' ')[0] %>&qs=<%#DDLQs.SelectedValue %>">
                            <%#Eval("Number").ToString().Split(' ')[0]%>
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField >
                    <ItemTemplate>
                        <%#Eval("Number").ToString().Split(' ')[1]%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField >
                    <ItemTemplate>
                        <asp:Label ID="la" runat="server" Text='<%#GetAzOrTz(Eval("Direct")+"",Eval("Placement")+"","1") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="la" runat="server" Text='<%#Eval("JiBie") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="la" runat="server" Text='<%#Eval("DaiShu") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="区数" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="la" runat="server" Text='<%#Eval("QuShu") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="la" runat="server" Text='<%#Convert.ToDouble(Eval("XinGe")).ToString("0.00") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="la" runat="server" Text='<%#Convert.ToDouble(Eval("XinWang")).ToString("0.00") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="la" runat="server" Text='<%#Eval("XinRen") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="la" runat="server" Text='<%#Eval("ZongRen") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="la" runat="server" Text='<%#Convert.ToDouble(Eval("ZongFen")).ToString("0.00") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
          
            </Columns>
        </asp:GridView>
        <br><br>
    </div>
    </form>
    <uc2:bottom runat="server" ID="bottom" />
    </div>
</body>
</html>
