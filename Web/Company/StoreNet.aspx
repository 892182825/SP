<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StoreNet.aspx.cs" Inherits="Company_StoreNet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link rel=Stylesheet href="CSS/Company.css" type="text/css" />
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
            padding:0px 5px 0px 5px;
            height:18px;
            font-family:Arial;
        }
        .treeTable th
        {
        	border-right:rgb(218,218,218) solid 1px;
            border-bottom:rgb(218,218,218) solid 1px;
            
            background-image:url(images/lmenu02.gif);height:25px;
            color:White;
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
                    trarr[i].style.backgroundColor="rgb(241,244,248)";
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
    <form id="form1" runat="server">
    <div style="padding:10px 0px 0px 20px">
         <asp:Button ID="Button1" runat="server" class="anyes" Text="显示" OnClick="Button1_Click" />
       <%=GetTran("000037", "店铺编号")%> ：
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <span style="color:rgb(0,61,92)">
      <%=GetTran("000045","期数")%>：
        </span>
        <asp:DropDownList ID="DDLQs" runat="server">
        </asp:DropDownList>

        <br>
        <span style="color:rgb(0,61,92)">
       <%--链路图：--%>
       </span>
        <asp:Literal ID="LitLLT" runat="server"></asp:Literal>
        <br><br>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" class="treeTable"  cellpadding="0" cellspacing="0" width="800" border="0">
            <Columns>
                 <asp:TemplateField HeaderText="店铺编号">
                    <ItemTemplate>
                        <asp:Label ID="la" runat="server" Text='<%#Eval("StoreID").ToString().Replace(" ","&nbsp;")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="会员编号">
                    <ItemTemplate>
                        <asp:Label ID="la" runat="server" Text='<%#Eval("Number") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="店铺名字">
                    <ItemTemplate>
                        <asp:Label ID="la" runat="server" Text='<%#Eval("StoreName") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="店铺级别">
                    <ItemTemplate>
                        <asp:Label ID="la" runat="server" Text='<%#GetStoreLevel(Eval("StoreLevelInt")+"") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="center" />
                </asp:TemplateField>
               
          
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
