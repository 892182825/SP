<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LevelIconsetting.aspx.cs" Inherits="Company_LevelIconsetting" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        function onloadIcon(levelid)
        {
            if(window.showModalDialog('DownFileLoad.aspx?levelid='+levelid,parent,'dialogWidth:400px;dialogHeight:100px;center:yes;status:no;scroll:yes;help:no;'))
            {
                window.location.reload(); 

            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="biaozzi"><tr><td>        
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  align="center"
            width="80%" CssClass="tablemb" onrowdatabound="GridView1_RowDataBound">
                                            <AlternatingRowStyle BackColor="#F1F4F8" />
                                <HeaderStyle CssClass="tablebt"/>
                                <RowStyle HorizontalAlign="Center" />
            <Columns>
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick='<%#Eval("id","onloadIcon({0})") %>'> <%=GetTran("001646", "上传图片")%></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="级别名称" DataField="levelstr"/>
                <asp:BoundField HeaderText="级别类型" DataField="levelflag"/>
                <asp:TemplateField HeaderText="级别图标">
                    <ItemTemplate>
                        <asp:Image ID="Image1" runat="server" ImageUrl='<%#Eval("ICOPath") %>' Width="20px" Height="20px"/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>            <table class="tablemb" Width="100%" >
                                <tr>
                                    <th>
                                        <%=GetTran("000015", "操作")%>
                                    </th>
                                    <th>
                                         <%=GetTran("001637", "级别名称")%>
                                    </th>
                                    <th>
                                        <%=GetTran("001639", "级别类型")%>
                                    </th>
                                    <th>
                                        <%=GetTran("001641", "级别图标")%>
                                        </th>
                                    </table>
                                    </EmptyDataTemplate>


        </asp:GridView>
        
        </td ></tr >
        <tr><td align ="center" ><input type="button" id="butt_Query"value='<%=GetTran("000421","返回") %>' style="cursor:pointer" Class="anyes" onclick="history.back()"/>
        </td></tr>
        </table>

    </div>
    </form>
</body>
</html>
