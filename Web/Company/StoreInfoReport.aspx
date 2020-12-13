<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StoreInfoReport.aspx.cs" Inherits="Company_StoreInfoReport" %>

<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<title>StoreInfoReport</title>
    <script type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
    <script src="../JS/QCDS2010.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function getDate() {
            var d = new Date();
            var day = d.getDate();
            var month = d.getMonth() + 1;
            var year = d.getFullYear();
            var datetimes = year + "-" + month + "-" + day;

            if (document.getElementById("txtEndDate") != null) {
                document.getElementById("txtEndDate").value = datetimes;
            }

            if (month == 0) {
                var start = year - 1 + "-12-" + day;
            }

            else {
                var start = year + "-" + d.getMonth() + "-" + day;
            }

            if (document.getElementById("txtBeginDate") != null) {
                document.getElementById("txtBeginDate").value = start;
            }
        }
        window.onerror = function() {
            return true;
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

    <script language="javascript">
        function cut() {
            document.getElementById("span1").title = '<%=GetTran("000032", "�� ��") %>';
        }
        function cut1() {
            document.getElementById("span2").title = '<%=GetTran("000033", "˵ ��") %>';
        }
    </script>

    <script language="javascript" type="text/javascript">
        function aaa() {
            for (var i = 0; i < form1.elements.length; i++) {
                if (form1.elements[i].type == "text") {
                    if (form1.elements[i].value.indexOf("'") != -1 || form1.elements[i].value.indexOf("=") != -1) {
                        alert('<%=GetTran("000712", "��ѯ�������治�����������ַ���")%>');
                        return false;
                    }
                }
            }
        }
        function CheckText(btname) {
            //���������ҳ���ж����ťҪ�ύʱ�����ʹ���������,���밴ť��ID
            filterSql_II(btname);
        }
    </script>

    <style type="text/css">
        #secTable
        {
            width: 150px;
        }
    </style> 		
</head>
<body  onload="down2()">	
	
	<form id="form1" method="post" runat="server">
	
	<br />
		<table cellSpacing="0" cellPadding="0"  border="0" class="biaozzi" >
		    <tr>
		     <td ><asp:Button id="btnSearch" runat="server"  Text="�� ѯ" CssClass="anyes"  onclick="btnSearch_Click" /> &nbsp;&nbsp;&nbsp;</td>
		        <td ><%=GetTran("001372", "ע�����ڿ�ʼʱ��")%>��</td>
				<td ><asp:TextBox ID="txtBeginDate" runat="server" onfocus="WdatePicker()" CssClass="Wdate" ></asp:TextBox></td>
				<td ><%=GetTran("001373", "��ֹʱ��")%>��</td>
				<td ><asp:TextBox ID="txtEndDate" runat="server" onfocus="WdatePicker()" CssClass="Wdate" ></asp:TextBox></td>
			   
		    </tr>			
		</table>	
		<br />
		 <table align="center" width="100%"   class="biaozzi">
           
            <tr>
                <td align="center" colspan="3" style="word-break:keep-all;word-wrap:normal;">
            <div id="griddiv" >
                   
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" 
                        Width="100%" 
                    
                        OnRowDataBound="GridView1_RowDataBound"  CssClass="tablemb"
                        onpageindexchanging="GridView1_PageIndexChanging" AllowPaging="True">
                                <HeaderStyle HorizontalAlign="Center"  Wrap=false CssClass="tablemb" />
                            <RowStyle Wrap="false" />
                            <SelectedRowStyle HorizontalAlign="Center" Wrap="false"/>
                                                            <AlternatingRowStyle BackColor="#F1F4F8" />
                                                            <RowStyle HorizontalAlign="Center" Wrap="false"/>
                            <FooterStyle HorizontalAlign="Center"/>
                                <Columns>
                                           <asp:BoundField DataField="StoreId" HeaderText="����" />
                                     <asp:TemplateField HeaderText = "������">
                                        
                                        <ItemTemplate>
                                            <%#GetbyName(Eval("Name").ToString())%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:BoundField DataField="Number" HeaderText="�곤���" />
      
                                    <asp:TemplateField HeaderText = "�곤����">
                                        
                                        <ItemTemplate>
                                            <%#GetbyName(Eval("MemberName").ToString())%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="shmoney" HeaderText="�ɶ�����" 
                                        DataFormatString="{0:f}" ItemStyle-CssClass="lab" />
                                    <asp:BoundField DataField="LevelStr" HeaderText="�꼶��" />
                                    <asp:BoundField Visible="False" DataField="PostalCode" HeaderText="���ʱ�" />
                                                                         
                                    <asp:TemplateField HeaderText = "�����˵绰">
                                        
                                        <ItemTemplate>
                                            <%#GetbyTele(Eval("HomeTele").ToString())%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText = "�칫�绰">
                                        
                                        <ItemTemplate>
                                            <%#GetbyTele(Eval("officeTele").ToString())%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText = "�ֻ�">
                                        
                                        <ItemTemplate>
                                            <%#GetbyTele(Eval("MobileTele").ToString())%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText = "����绰">
                                        
                                        <ItemTemplate>
                                            <%#GetbyTele(Eval("FaxTele").ToString())%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField Visible="False" DataField="Email" HeaderText="�����ʼ�" />
                                    
                                 <asp:TemplateField HeaderText = "ע������" ItemStyle-Wrap="false">
                                        
                                        <ItemTemplate>
                                            <%#GetbyRegisterDate(Convert.ToDateTime(Eval("RegisterDate")).ToString("yyyy-MM-dd"))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="adds" HeaderText="����ʡ��" />
                                   
                                    <asp:TemplateField HeaderText = "�������ڵ�">
                                        
                                        <ItemTemplate>
                                            <%#Getbyad(Eval("ad").ToString())%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    
                                 
                                </Columns>
                                 <EmptyDataTemplate>
                                <table >
                                    <tr>
                                    <th>
                                            <%=GetTran("000037", "����")%>
                                        </th>
                                        <th>
                                            <%=GetTran("001423", "������")%>
                                        </th>
                                        <th>
                                            <%=GetTran("001424", "�곤���")%>
                                        </th>
                                        <th>
                                            <%=GetTran("000039", "�곤����")%>
                                        </th>
                                        <th>
                                            <%=GetTran("001425", "�ɶ�����")%>
                                        </th>
                                        <th>
                                            <%=GetTran("001427", "�꼶��")%>
                                        </th>
                                        
                                        <th>
                                            <%=GetTran("000319", "�����˵绰")%>
                                        </th>
                                        <th>
                                            <%=GetTran("000044", "�칫�绰")%>
                                        </th>
                                        <th>
                                            <%=GetTran("000052", "�ֻ�")%>
                                        </th>
                                        <th>
                                            <%=GetTran("000071", "����绰")%>
                                        </th>
                                         
                                          <th>
                                            <%=GetTran("000057", "ע������")%>
                                        </th>
                                          <th>
                                            <%=GetTran("001432", "����ʡ��")%>
                                        </th>
                                          <th>
                                            <%=GetTran("000313", "�������ڵ�")%>
                                        </th>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            </asp:GridView>
                      
                    </div>
                   
                </td>
            </tr>
            
        </table>	
         <table width="100%">
         <div>
            <span style="font-size:12px; margin-left:28px; float:left;"><%=GetTran("000247", "�ܼ�")%> </span> <span style="font-size:12px; float:right;"><%=GetTran("007576", "��ҳ�ɶ�����ϼ�")%> ��<asp:Label ID="lab_bkdhkhj" ForeColor="red" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;<%=GetTran("007577", "��ѯ�ɶ������ܼ�")%>��<asp:Label ID="lab_ckdhkzj" runat="server" ForeColor="Red"></asp:Label></span>
         </div>
    <tr>
        <td align="right">
            <uc1:Pager ID="Pager1" runat="server" />
        </td>
    </tr>
    <tr>
        <td>&nbsp;
        </td>
    </tr>
</table>  
  <div id="cssrain" style="width: 100%">
    <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
        <tr>
            <td width="150">
                <table width="100%" border="0" height="28" cellpadding="0" cellspacing="0" id="secTable">
                    <tr>
                        <td class="sec2" onclick="secBoard(0)" style="white-space: nowrap;">
                            <span id="span1" title="" onmouseover="cut()">
                                <%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000032", "�� ��"))%></span>
                        </td>
                        <td class="sec1" onclick="secBoard(1)" style="white-space: nowrap;">
                            <span id="span2" title="" onmouseover="cut1()">
                                <%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "˵ ��"))%></span>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <a href="#">
                    <img src="images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX"
                        onclick="down2()" style="vertical-align: middle" /></a>
            </td>
        </tr>
    </table>
      
    <div id="divTab2">
        <table width="100%" border="0" height="68" cellspacing="0" class="DMbk" id="mainTable">
            <tbody style="display: block" id="tbody0">
                <tr>
                    <td valign="bottom" style="padding-left: 20px">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:LinkButton ID="btnDownExcel" runat="server" Text="����Excel" OnClick="btnDownExcel_Click"
                                        Style="display: none;"></asp:LinkButton><a href="#"><img src="images/anextable.gif"
                                            width="49" height="47" border="0" onclick="__doPostBack('btnDownExcel','');" /></a>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <!--<a href="#">
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
                                     ����<%=GetTran("006858", "��ѯ���̵ľ���ע����Ϣ��")%>
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
<script type="text/javascript" language="javascript">
    function heji() {
        var lab = 0;
        var lab1 = 0;
        $('.lab').each(
            function() {
                lab = parseFloat($(this).text().replace(',', '')) + lab;
            }
        );
        $('#lab_bkdhkhj').html(lab == 0 ? "0" : lab);
    };

    window.onload = heji();
</script>