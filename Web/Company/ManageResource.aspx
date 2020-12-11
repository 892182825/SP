<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageResource.aspx.cs" Inherits="Company_ManageResource" EnableEventValidation="false" %>

<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>文件上传</title>
    <link type="text/css" rel="Stylesheet" href="CSS/Company.css" />
    <script src="../JS/QCDS2010.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
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

    <script type="text/javascript" language="javascript">
        window.onload = function() {
            //down2();
        }
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
    </script>

    <script language="javascript" type="text/javascript">
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
    <script language="javascript" type="text/javascript">
        function getdate() {
            var d = new Date();
            var day = d.getDate();
            var month = d.getMonth() + 1;
            var year = d.getFullYear();
            var datetimes = year + "-" + month + "-" + day;
            document.getElementById("txtendDate").value = datetimes;
            document.getElementById("txtendDate").value = datetimes;
            if (month == 0) {
                var strat = year - 1 + "-12-" + day;
            } else {
                var strat = year + "-" + d.getMonth() + "-" + day;
            }
            document.getElementById("txtdatastrat").value = strat;
        }
    </script>
    <script type="text/javascript">
        function cut()
        {
             document.getElementById("span1").title='<%=GetTran("000032", "管 理") %>';
        }
        function cut1()
        {
             document.getElementById("span2").title='<%=GetTran("000033", "说 明") %>';
        }
    </script>
    <script language="javascript" type="text/javascript">
        function aaa() {
            for (var i = 0; i < form1.elements.length; i++) {
                if (form1.elements[i].type == "text") {
                    if (form1.elements[i].value.indexOf("'") != -1 || form1.elements[i].value.indexOf("=") != -1) {
                        alert('<%=GetTran("000481","查询条件里面不能输入特殊字符") %>！');
                        return false;
                    }
                }
            }
        }

        function deleteReally() {
            return confirm('<%=GetTran("000248","确定删除吗？")%>');
        }
    </script>
<script type="text/javascript" src="../js/SqlCheck.js"></script>
</head>
<body>
    <form id="form1" runat="server" onsubmit="return filterSql_III()">
    <div>
    <br />
        <table width="100%">
            <tr>
                <td>
                    <table style="word-wrap:normal; white-space:nowrap;" class="biaozzi" >
                        <tr>
                            <td style="white-space:nowrap">
                                <asp:Button ID="Button1" runat="server" Text="搜 索" CssClass="anyes" 
                                    onclick="Button1_Click" />
                            </td>
                            <td style="white-space:nowrap">
                                <%=GetTran("000204")%>：
                            </td>
                            <td style="white-space:nowrap">
                                <asp:TextBox ID="txtName" runat="server" MaxLength="20" ></asp:TextBox>
                            </td>
                            <td style="white-space:nowrap">
                                <%=GetTran("000213")%>：
                            </td>
                            <td>
                                <asp:TextBox ID="txtfilename" runat="server" MaxLength="20"></asp:TextBox>
                            </td>
                            
                            <td style="white-space:nowrap">
                                <input type="button" value=<%=GetTran("001326","上传资料") %> onclick="javascript:window.location.href='UploadRes.aspx';"
                                     class="another"/>
                            </td>
                        </tr>
                    </table>
                    <br>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td colspan="5" style="border:rgb(147,226,244) solid 1px">
                                <asp:GridView ID="givshowFile" runat="server" AutoGenerateColumns="False" Width="100%"
                                    CssClass="tablemb  bordercss" OnRowCommand="givshowFile_RowCommand" 
                                    onrowdatabound="givshowFile_RowDataBound" 
                                    style="word-wrap:normal; white-space:nowrap;"
                                    AlternatingRowStyle-Wrap="false"
                        HeaderStyle-Wrap="false"
                        FooterStyle-Wrap="false" >
                                    <Columns>
                                        <asp:TemplateField HeaderText="下载" ShowHeader="False" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="false" CommandName="downLoad"
                                                    CommandArgument='<%#Eval("ResID") %>'><%=GetTran("000245", "下载")%></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="修改" ShowHeader="False" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hyEdit" runat="server"  style="text-decoration:none;"  Font-Underline="True" NavigateUrl='<%# DataBinder.Eval(Container, "DataItem.ResID", "UploadRes.aspx?action=edit&amp;id={0}") %>'><%=GetTran("000259", "修改")%></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="删除" ShowHeader="False" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="false" CommandName="Del"
                                                    CommandArgument='<%#Eval("ResID") %>'  OnClientClick="return deleteReally()"><%=GetTran("000022", "删除")%></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ResID" Visible="false" HeaderText="资料编号" />
                                        <asp:BoundField DataField="Resname" HeaderText="资料名称" ItemStyle-HorizontalAlign="center" />
                                        <asp:BoundField DataField="FileName" HeaderText="对应文件名"  ItemStyle-HorizontalAlign="center"/>
                                        <asp:BoundField DataField="ResDescription" HeaderText="资料简介" ItemStyle-Wrap="false"  ItemStyle-HorizontalAlign="center"/>
                                        <asp:BoundField DataField="ResSize" HeaderText="文件大小"  ItemStyle-HorizontalAlign="center"/>
                                        <%--DataFormatString="{0:M-dd-yyyy}"--%>
                                        
                                        <asp:TemplateField HeaderText="上次修改日期" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <asp:Label ID="scxgsj" runat="server" Text='<%#GetBiaoZhunTime(Eval("ResDateTime").ToString())%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:BoundField DataField="ResTimes" HeaderText="下载次数" ItemStyle-HorizontalAlign="center"/>
                                    </Columns>
                                    <AlternatingRowStyle BackColor="#F1F4F8" />
                                    <EmptyDataTemplate>
                                        <table width="100%" cellspacing="0">
                                            <tr>
                                                <th><%=GetTran("000245")%></th>
                                                <th><%=GetTran("000272")%></th>
                                                <th><%=GetTran("000204")%></th>
                                                <th><%=GetTran("000278")%></th>
                                                <th><%=GetTran("000280")%></th>
                                                <th><%=GetTran("000282")%></th>
                                                <th><%=GetTran("000283")%></th>
                                                <th><%=GetTran("000287")%></th>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <uc1:Pager ID="Pager1" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
             <tr>
                <td valign="top">
                    
                </td>
            </tr>
        </table>
    </div>
    
    <div id="cssrain" style="width:100%">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
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
                                        <img src="images/dis.GIF" name="imgX" width="18" height="22" border="0" id="imgX"
                                            onclick="down2()" style="vertical-align:middle" /></a>
                                </td>
                            </tr>
                        </table>
                        <div id="divTab2" style="display:none">
                            <table width="100%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
                                <tbody style="display: block" id="tbody0">
                                    <tr>
                                        <td valign="bottom" style="padding-left: 20px">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="btnDownExcel" Visible="false" runat="server" Text="导出Excel" OnClick="btnDownExcel_Click"
                                                            Style="display: none;"></asp:LinkButton><a href="#"><img src="images/anextable.gif" width="49"
                                                                height="47" border="0" onclick="__doPostBack('btnDownExcel','');" /></a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                                <tbody style="display: none"  id="tbody1">
                                    <tr>
                                        <td style="padding-left: 20px">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <%=GetTran("006859")%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    
                    
    </form>
</body>
</html>
