<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RefundmentOrderForMemberList.aspx.cs" Inherits="Member_RefundmentOrderListForMember" %>

<%@ Register Src="~/UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/MemberPager.ascx" TagName="Pager" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/MemberTop.ascx" TagName="top" TagPrefix="uc1"  %>
<%@ Register Src="~/UserControl/MemberBottom.ascx" TagName="bottom" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        会员退货浏览</title>
    <link href="CSS/detail.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>


    <script src="../../JS/QCDS2010.js" type="text/javascript"></script>
<script type="text/javascript" language="javascript" src="../../js/SqlCheck.js"></script>
    <script type="text/javascript">
        window.onerror=function ()
        {
            return true;    
        };
    </script>

    <script type="text/javascript">
<!--
//分别是奇数行默认颜色,偶数行颜色,鼠标放上时奇偶行颜色
    var aBgColor = ["#F1F4F8","#FFFFFF","#FFFFCC","#FFFFCC"];
    //从前面iHead行开始变色，直到倒数iEnd行结束
    function addTableListener(o,iHead,iEnd)
    {
        o.style.cursor = "normal";
        iHead = iHead > o.rows.length?0:iHead;
        iEnd = iEnd > o.rows.length?0:iEnd;
        for (var i=iHead;i<o.rows.length-iEnd ;i++ )
        {
            o.rows[i].onmouseover = function(){setTrBgColor(this,true)}
            o.rows[i].onmouseout = function(){setTrBgColor(this,false)}
        }
    }
    function setTrBgColor(oTr,b)
    {
        oTr.rowIndex % 2 != 0 ? oTr.style.backgroundColor = b ? aBgColor[3] : aBgColor[1] : oTr.style.backgroundColor = b ? aBgColor[2] : aBgColor[0];
    }
    window.onload = function(){addTableListener(document.getElementById('<%=Session["GridViewID"] %>'),0,0);}

function MM_jumpMenu(targ,selObj,restore){ //v3.0
  eval(targ+".location='"+selObj.options[selObj.selectedIndex].value+"'");
  if (restore) selObj.selectedIndex=0;
}
//-->
    </script>

    <script type="text/javascript" language="javascript">
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
        .style1
        {
            width: 40%;
            text-align: right;
        }
    </style>

    <script language="javascript" type="text/javascript">
        function ShowTbInfo(DocId,StoreId,TotalMoney,TotalPv,Date)
        {
            document.getElementById("lblDocId").innerHTML=DocId;
            document.getElementById("lblStoreId").innerHTML=StoreId;
            document.getElementById("lblTotalMoney").innerHTML=TotalMoney;
            document.getElementById("lblTotalPv").innerHTML=TotalPv;
            document.getElementById("lblDate").innerHTML=Date;
            document.getElementById("tbInfo").style.display='';
            document.getElementById("tbSearch").style.display='none';
        }
        function WareHouseChange(wh)
        {
            var list = AjaxClass.BindDepotSeat(wh.value).value;
            
            while(document.getElementById("ddlDepotSeat").childNodes.length>0)
            {
                document.getElementById("ddlDepotSeat").removeChild(document.getElementById("ddlDepotSeat").childNodes[0]);
            }
            
            for(var i=0 ; i<list.length;i++)
            {
                document.getElementById("ddlDepotSeat").options[i]=new Option(list[i][0],list[i][1]);
            }
            
        }
        function ShenHe()
        {
            var str = AjaxClass.ShenHe(document.getElementById("lblDocId").innerHTML,document.getElementById("lblStoreId").innerHTML,document.getElementById("ddlWareHouse").value,document.getElementById("ddlDepotSeat").value).value;
            if(str=="")
            {
                alert('<%=GetTran("000858", "审核成功！")%>');
                window.location.href="RefundmentOrderBrowse.aspx";
            }
            else
            {
                alert(str);
            }
        }
        
        //----------------转换汇率----------------------
        var defaultcur;
        
        window.onload=function (){
            defaultcur=document.getElementById("ddlCurrency").value;
        }
        
        //-----------------------------------
        function CheckSql()
        {
            filterSql_III();
        }
    </script>

</head>
<body >
    <form id="Form1" method="post" runat="server" onsubmit="CheckSql()">
    <div class="MemberPage">
          <uc1:top runat="server" ID="top" />

                <table class="biaozzi" cellpadding="0" cellspacing="0">
     
                    <tr>
                        <td >
                            <asp:Button ID="btnAdd" runat="server" Visible="false" Text="添加退货单" OnClick="btnAdd_Click" CssClass="anyes" />
                               &nbsp;&nbsp;&nbsp;&nbsp;

                            <asp:Button  ID="btn_Submit" runat="server" Text="查 询" OnClick="btn_Submit_Click"
                                CssClass="anyes" ></asp:Button>&nbsp;

                           <%-- &nbsp;<%=GetTran("000058", "请选择国家")%>：--%><asp:DropDownList Visible="false" ID="DropCurrency" runat="server">
                            </asp:DropDownList>
                            &nbsp;
                            <%=GetTran("001819", "条件")%>：
                            <asp:DropDownList ID="DropDownList_Items" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_Items_SelectedIndexChanged">
                               
                                <asp:ListItem Value="ExpectNum">期数</asp:ListItem>
                                <asp:ListItem Value="TotalMoney">总价格</asp:ListItem>
                                <asp:ListItem Value="RefundmentDate_DT">退货日期</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;
                            <asp:DropDownList ID="DropDownList_condition" runat="server">
                            </asp:DropDownList>
                            &nbsp;

                            <asp:TextBox ID="DatePicker1" CssClass="ctConPgTxt" Style="width: 120px" onfocus="new WdatePicker()" runat="server" />&nbsp;
                            <asp:TextBox ID="txtCondition" CssClass="ctConPgTxt" runat="server" MaxLength="100" />&nbsp;
                             <asp:DropDownList ID="DDPStatus" runat="server">
                                <asp:ListItem Value="">所有</asp:ListItem>
                                <asp:ListItem Value="StatusFlag_NR=0 ">未审核</asp:ListItem>
                                <asp:ListItem Value="StatusFlag_NR=2">已审核</asp:ListItem>
                                <asp:ListItem Value="StatusFlag_NR=-1">无效</asp:ListItem>
                            </asp:DropDownList>
                               &nbsp;
                            <%=GetTran("001800", "的退单记录")%>
                        </td>
                      
                    </tr>
                </table>
            
                            <asp:GridView ID="gvRefundmentBrowse" CellSpacing="1" CellPadding="1" runat="server" AutoGenerateColumns="False"
                                OnRowCommand="gvRefundmentBrowse_RowCommand" Width="100%" 
                                 OnRowDataBound="gvRefundmentBrowse_RowDataBound1">
                                 <HeaderStyle CssClass="ctConPgTab" />
                                 <AlternatingRowStyle BackColor="#F1F4F8" />
                                <RowStyle HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField HeaderText="操作" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                        <ItemTemplate>                                          
                                            
                                            <asp:LinkButton ID="lbtn_Audit" Text='审核' OnClientClick="javascript:return confirm('确定要审核吗？');"  CommandArgument='<%# DataBinder.Eval(Container.DataItem,"docID") %>'
                                                CommandName="audit" runat="server" />
                                            <asp:LinkButton ID="lbtn_lock" Text='锁定' OnClientClick="javascript:return confirm('确定要锁定吗？');"  CommandArgument='<%# DataBinder.Eval(Container.DataItem,"docID") %>'
                                                CommandName="lock" runat="server" />
                                            <asp:LinkButton ID="lbtn_unlock" Text='解锁' OnClientClick="javascript:return confirm('确定要解锁吗？');"  CommandArgument='<%# DataBinder.Eval(Container.DataItem,"docID") %>'
                                                CommandName="unlock" runat="server" Visible="false" />
                                                
                                            <asp:LinkButton ID="lbl_Del" Text='删除'  OnClientClick="javascript:return confirm('确定要删除吗？');"  CommandArgument='<%# DataBinder.Eval(Container.DataItem,"docID") %>'
                                                CommandName="del" runat="server" />
                                                  <asp:LinkButton ID="lbtn_details" Text='详细'  CommandArgument='<%# DataBinder.Eval(Container.DataItem,"docID") %>'
                                                CommandName="details" runat="server" />
                                                  <asp:LinkButton ID="lbtn_Print" Text='打印'  CommandArgument='<%# DataBinder.Eval(Container.DataItem,"docID") %>'
                                                CommandName="print" runat="server" />
                                                <asp:Label ID="lbl_StatusFlag_NR" runat="server" Visible="false"  Text='<%# Eval("StatusFlag_NR") %>'></asp:Label>
                                                <asp:Label ID="lbl_IsLock" runat="server" Visible="false"  Text='<%# Eval("IsLock") %>'></asp:Label>
                                                <asp:Label ID="lbl_docID" Visible="false"  runat="server" Text='<%# Eval("docID") %>'></asp:Label>
                                                
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                        <ItemStyle Wrap="False"></ItemStyle>
                                    </asp:TemplateField>                                   
                                    <asp:BoundField DataField="docID" SortExpression="docID" HeaderText="退货单号" HeaderStyle-Wrap="false"
                                        ItemStyle-Wrap="false">
                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                        <ItemStyle Wrap="False"></ItemStyle>
                                    </asp:BoundField>                        
                                    <asp:BoundField DataField="OriginalDocIDS" SortExpression="OriginalDocIDS" HeaderText="订单号" HeaderStyle-Wrap="false"
                                        ItemStyle-Wrap="false">
                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                        <ItemStyle Wrap="False"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ExpectNum" SortExpression="ExpectNum" HeaderText="期数"
                                        HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                        <ItemStyle Wrap="False"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="是否审核" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "StatusFlag_NR").ToString().Trim() == "2" ? "是" : "否"%>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                        <ItemStyle Wrap="False"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="是否锁定" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "IsLock").ToString().Trim() == "1" ? "是" : "否"%>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                        <ItemStyle Wrap="False"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="退货总价">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" name="lblTotalMoney" Text='<%# Eval("totalMoney") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle HorizontalAlign="Right" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="totalPV" HeaderText="退货总积分" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="退货日期" SortExpression="dat_docMakeTime">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# GetOrderDate(Eval("RefundmentDate_DT")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <table cellspacing="1" cellpadding="1"  border="1" style="width: 100%;
                                        border-collapse: collapse;">
                                        <tr class="ctConPgTab">
                                            <th style="white-space: nowrap">
                                                <%=GetTran("001802", "操作")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("001809", "退货单号")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("000045", "期数")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("000605", "是否审核")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("001811", "是否失效")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("001812", "退货总价")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("001813", "退货总积分")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("001814", "退货日期")%>
                                            </th>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:GridView>
        <uc2:Pager ID="Pager1" runat="server" />
               
       <asp:Label ID="lblMessage" runat="server"></asp:Label>
                       
     <uc2:bottom runat="server" ID="bottom" />

        </div>
    </form>
</body>
</html>
