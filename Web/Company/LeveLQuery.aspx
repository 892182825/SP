<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LeveLQuery.aspx.cs" Inherits="Company_LeveLQuery" %>

<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/ExpectNum.ascx" TagName="ExpectNum" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Company_twQuery</title>
    <link type="text/css" rel="Stylesheet" href="CSS/Company.css" />
    
    <script src="../JS/QCDS2010.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../js/SqlCheck.js"></script>
    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
    <script language="javascript" type="text/javascript">
        function CheckText() {
            //这个方法是只有1个lkSubmit按钮时候 可直接用
            filterSql();
        }
    </script>

    <script type="text/javascript">
        function down2() {
            if (document.getElementById("divTab2").style.display == "none") {
                document.getElementById("divTab2").style.display = "";
                document.getElementById("imgX").src = "images/dis1.GIF";

            }
            else {
                document.getElementById("divTab2").style.display = "none";
                document.getElementById("imgX").src = "images/dis.GIF";
            }
        }

        function secBoard(n) {
            var tdarr = document.getElementById("secTable").getElementsByTagName("td");

            for (var i = 0; i < tdarr.length; i++) {
                tdarr[i].className = "sec1";
            }
            tdarr[n].className = "sec2";

            var tbody0 = document.getElementById("tbody0");
            tbody0.style.display = "none";
            var tbody1 = document.getElementById("tbody1");
            tbody1.style.display = "none";


            document.getElementById("tbody" + n).style.display = "block";
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function() {
            if ($.browser.msie && $.browser.version == 6) {
                FollowDiv.follow();
            }
        });
        FollowDiv = {
            follow: function() {
                $('#cssrain').css('position', 'absolute');
                $(window).scroll(function() {
                    var f_top = $(window).scrollTop() + $(window).height() - $("#cssrain").height() - parseFloat($("#cssrain").css("borderTopWidth")) - parseFloat($("#cssrain").css("borderBottomWidth"));
                    $('#cssrain').css('top', f_top);
                });
            }
        }
    </script>
    <script language="javascript" type="text/javascript">
        function aaa() {
            for (var i = 0; i < form1.elements.length; i++) {
                if (form1.elements[i].type == "text") {
                    if (form1.elements[i].value.indexOf("'") != -1 || form1.elements[i].value.indexOf("=") != -1) {
                        alert('<%=GetTran("000481", "查询条件里面不能输入特殊字符")%>！');
                        return false;
                    }
                }
            }
        }
    </script>
    <script type="text/javascript">
        function cut() {
            document.getElementById("span1").title = '<%=GetTran("000032", "管 理") %>';
        }
        function cut1() {
            document.getElementById("span2").title = '<%=GetTran("000033", "说 明") %>';
        }
    </script>
</head>
<body  onload="down2()">
    <form id="form1" runat="server">
    <div>
    <br />
        <table width="100%" align="center">
            <tr>
                <td nowrap="nowrap">
                    <table class="biaozzi">
                        <tr>
                            <td>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:linkbutton id="lkSubmit"  Runat="server" Text="提交" style="DISPLAY: none"
                                    onclick="lkSubmit_Click" ></asp:linkbutton>
                                <input class="anyes" id="bSubmit" onclick="CheckText()" type="button" value="<%=GetTran("000048", "查 询")%>" />

                                <asp:Button ID="btnSeach" runat="server" Text="查 询" OnClick="btnSeach_Click" CssClass="anyes" Visible="false" />
                            </td>
                            <td >
                            <asp:Literal ID="lit_number" runat="server"></asp:Literal>
                            &nbsp;</td>
                            <td >
                                <asp:TextBox ID="txt_member" runat="server" MaxLength="10"></asp:TextBox>
                            </td>
                            <td>
                                <%=GetTran("000613", "日期")%>：<asp:TextBox ID="DatePicker2" runat="server" CssClass="Wdate" 
                                    onfocus="WdatePicker()"></asp:TextBox>
                            </td>
                            <td nowrap="nowrap"><%=GetTran("000045", "期数")%>：
                                <uc2:ExpectNum ID="ExpectNum1" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <!--Gridview显示部分-->
                    <table width="100%">
                        <tr>
                            <td colspan="2" align="center" width="100%">
                                <asp:GridView ID="givTWmember" Width="100%" runat="server" AutoGenerateColumns="False"
                                    CssClass="tablemb" 
                                    onrowdatabound="givTWmember_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="Number" HeaderText="会员编号" />
                                        <asp:BoundField DataField="name1" HeaderText="会员姓名" />
                                        <asp:BoundField DataField="oldlv" HeaderText="原级别" />
                                        <asp:BoundField DataField="lv" HeaderText="调整级别" />
                                    
                                        <asp:BoundField DataField="ExpectNum" HeaderText="期数" />

                                        <asp:TemplateField HeaderText="日期">
                                            <ItemTemplate>
                                                <%#PageBase.GetbyDT(Eval("inputdate").ToString())%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="operaternum" HeaderText="期数" />
                                        <asp:BoundField DataField="remark" HeaderText="备注" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                    <table width="100%">
                                        <tr>
                                            <th>
                                                <%=GetTran("000024", "会员编号")%>
                                            </th>
                                            <th>
                                                <%=GetTran("007149", "原级别")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000771", "调整级别")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000045", "期数")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000613", "日期")%>
                                            </th>
                                           <th>
                                                <%=GetTran("004134", "操作人编号")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000078", "备注")%>
                                            </th>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                                </asp:GridView>
                                
                                <asp:GridView ID="gv1" runat="server" Width="100%" AutoGenerateColumns="False" CssClass="tablemb"
                                        OnRowDataBound="gv1_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="Number" HeaderText="店铺编号" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="oldlv" HeaderText="调整前级别" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="lv" HeaderText="调整级别" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="ExpectNum" HeaderText="期数" ItemStyle-HorizontalAlign="Center" />
                                            <asp:TemplateField HeaderText="日期">
                                                <ItemTemplate>
                                                    <%#PageBase.GetbyDT(Eval("InputDate").ToString())%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Remark" HeaderText="调整原因" ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <table width="100%" class="biaozzi">
                                                <th><%# GetTran("000150", "店铺编号")%></th>
                                                <th><%#GetTran("007215", "调整前级别")%></th>
                                                <th><%# GetTran("000771", "调整级别")%></th>
                                                <th><%# GetTran("000045", "期数")%></th>
                                                <th><%# GetTran("000613", "日期")%></th>
                                                <th><%#GetTran("007213","定级原因") %></th>
                                            </table>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                            </td>
                        </tr>
                         <tr>
                            <td>
                                <asp:Label ID="lblMassage" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <uc1:Pager ID="Pager1" runat="server" />
                            </td>
                        </tr>
                    </table>
                    
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <div id="cssrain" style="width:100%">
                        <table width="99%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
                            <tr>
                                <td width="150">
                                    <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
                                        <tr>
                                            <td class="sec2" onclick="secBoard(0)">
                    <span id="span1" title="" onmouseover="cut()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000032", "管 理"))%></span>
                </td>
                <td class="sec1" onclick="secBoard(1)">
                    <span id="span2" title="" onmouseover="cut1()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
                </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <a href="#">
                                        <img src="images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX"
                                            onclick="down2()" style="vertical-align:middle" /></a>
                                </td>
                            </tr>
                        </table>
                        <div id="divTab2">
                            <table width="99%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
                                <tbody style="display: block" id="tbody0">
                                    <tr>
                                        <td valign="bottom" style="padding-left: 20px">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="btnDownExcel"  runat="server" Text="导出Excel" OnClick="btnDownExcel_Click"
                                                            Style="display: none;"></asp:LinkButton><a href="#"><img src="images/anextable.gif" width="49"
                                                                height="47" border="0" onclick="__doPostBack('btnDownExcel','');" /></a>&nbsp;&nbsp;&nbsp;&nbsp;
                                                       <!-- <a href="#">
                                                            <img src="images/anprtable.gif" width="49" height="47" border="0" /></a>-->
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                                <tbody style="display: none" id="tbody1">
                                    <tr>
                                        <td style="padding-left: 20px">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                    １、 <asp:Literal ID="lit_beizhu" runat="server"></asp:Literal>
                                                       
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                          
                        </div>
                    </div>
                </td>
            </tr>
        </table>
          
    </div>
    </form>
   
</body>
</html>
