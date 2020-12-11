<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation = "false" CodeFile="MemberInfoReport.aspx.cs" Inherits="Company_H_infoReport" %>

<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>��Ա�ֲ����</title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />

    <script src="../JS/jquery-1.4.1-vsdoc.js" type="text/javascript"></script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <br />
    <table  cellspacing="0" cellpadding="0"  border="0"  class="biaozzi">
        <tr>        
        <td ><asp:Button ID="Btn_Dsearch" runat="server" Text="�� ѯ" CssClass="anyes" OnClick="Btn_Dsearch_Click" />  &nbsp;&nbsp;&nbsp;</td>                    
            <td><%=GetTran("001372", "ע�����ڿ�ʼʱ��")%>��</td>
            <td><asp:TextBox ID="DatePicker1" runat="server" onfocus="WdatePicker()" CssClass="Wdate"></asp:TextBox></td>
            <td><%=GetTran("001373", "��ֹʱ��")%>��</td>
            <td><asp:TextBox ID="DatePicker2" runat="server" onfocus="WdatePicker()" CssClass="Wdate"></asp:TextBox></td>           
        </tr>
    </table>
    <br />
    <table align="center"  width="100%" class="biaozzi">
        <tr>
            <td align="center" style="word-break:keep-all;word-wrap:normal;">
                <asp:GridView ID="gvMemberDetail" runat="server"  CssClass="tablemb"
AutoGenerateColumns="False" Width="100%" onrowdatabound="gvMemberDetail_RowDataBound"
                    AlternatingRowStyle-Wrap="False" FooterStyle-Wrap="False" HeaderStyle-Wrap="False"
                PagerStyle-Wrap="False" RowStyle-Wrap="False" SelectedRowStyle-Wrap="False" >
                            <AlternatingRowStyle  Wrap="false" />
<SelectedRowStyle Wrap="False"></SelectedRowStyle>
                            <HeaderStyle CssClass="tablemb" Wrap="false" />
                            <RowStyle HorizontalAlign="Center"  Wrap="false" />
                            <Columns>
                                  <asp:BoundField DataField="Number" HeaderText="��Ա���" />
                                     <asp:TemplateField HeaderText = "����">
                                        
                                        <ItemTemplate>
                                            <%#GetbyName(Eval("Name").ToString())%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:BoundField DataField="Placement" HeaderText="���ñ��" />
                                    <asp:TemplateField HeaderText = "��������">
                                        
                                        <ItemTemplate>
                                            <%#GetbyName(Eval("AnzhiName").ToString())%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Direct" HeaderText="�Ƽ����" />
                                    <asp:TemplateField HeaderText = "�Ƽ�����">
                                        
                                        <ItemTemplate>
                                            <%#GetbyName(Eval("tuijianname").ToString())%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:BoundField DataField="ExpectNum" HeaderText="ע������"  />
                                    <asp:TemplateField HeaderText = "ע������" ItemStyle-Wrap="false">
                                        
                                        <ItemTemplate>
                                            <%#GetbyRegisterDate(Eval("RegisterDate").ToString())%>
                                        </ItemTemplate>
<ItemStyle Wrap="False"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText = "�ƶ��绰"> 
                                        <ItemTemplate>
                                            <%#GetbyTele(Eval("Tele").ToString())%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:BoundField DataField="Address" HeaderText="����ʡ�ݳ���" />
                                    <asp:TemplateField HeaderText = "��ַ"> 
                                        <ItemTemplate>
                                            <%#Getbyad(Eval("ad").ToString())%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText = "֤������"> 
                                        <ItemTemplate>
                                            <%#GetbyPaperNumber(Eval("PaperNumber").ToString())%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                             <asp:BoundField Visible="False" DataField="papertype" HeaderText="֤������" />
                                <asp:BoundField Visible="False" DataField="Bank" HeaderText="��������" />
                                <asp:BoundField Visible="False" DataField="BankCard" HeaderText="���п���" />
                            </Columns>
<FooterStyle Wrap="False"></FooterStyle>
<PagerStyle Wrap="False"></PagerStyle>
                             <EmptyDataTemplate>
                                <table>
                                    <tr>
                                    <th>
                                            <%=GetTran("000024", "��Ա���")%>
                                        </th>
                                        <th>
                                            <%=GetTran("000107", "����")%>
                                        </th>
                                        <th>
                                            <%=GetTran("000027", "���ñ��")%>
                                        </th>
                                        <th>
                                            <%=GetTran("000097", "��������")%>
                                        </th>
                                        <th>
                                            <%=GetTran("000026", "�Ƽ����")%>
                                        </th>
                                        <th>
                                            <%=GetTran("000192", "�Ƽ�����")%>
                                        </th>
                                        <th>
                                            <%=GetTran("000029", "ע������")%>
                                        </th>
                                        <th>
                                            <%=GetTran("000057", "ע������")%>
                                        </th>
                                        <th>
                                            <%=GetTran("000069", "�ƶ��绰")%>
                                        </th>
                                        <th>
                                            <%=GetTran("000916", "����ʡ�ݳ���")%>
                                        </th>
                                        <th>
                                            <%=GetTran("000072", "��ַ")%>
                                        </th>
                                        <th>
                                            <%=GetTran("000083", "֤������")%>
                                        </th>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>  
    <table width="100%">
    <tr>
        <td align="right">
            <uc1:Pager ID="Pager1" runat="server" />
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
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
                        <img src="images/dis.GIF" name="imgX" width="18" height="22" border="0" id="imgX"
                            onclick="down2()" style="vertical-align: middle" /></a>
                </td>
            </tr>
        </table>
        <div id="divTab2" style="display:none;">
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
                                         ����<%=GetTran("006857", "��ѯĳ��ʱ��λ�Ա����ϸ��Ϣ��")%>
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
    <script type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
    <script src="js/companyview.js" type="text/javascript"></script>
</body>
</html>


