<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompanyBalance.aspx.cs" Inherits="Company_CompanyBalance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>工资结算</title>
    <script language="javascript" type="text/javascript" src="../JS/QCDS2010.js"></script>
    <script language="javascript" type="text/javascript" src="../javascript/ManagementVsExplanation.js"></script>
    <script type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
    <script language="javascript" type="text/javascript">
        var aa = [
            '<%=GetTran("001130", "您确定要结算")%>',
            '<%=GetTran("000156", "第")%>',
            '<%=GetTran("001131", "期奖金？？？")%>',
            '<%=GetTran("001129", "您已经打开了一个计算窗口！\n请关闭窗口在再选择结算其他期！")%>',
            '<%=GetTran("001132", "前几期有没有结算的期数，请先结算！")%>',
            '<%=GetTran("001133", "请打开允许弹出窗口！")%>',
            '<%=GetTran("001134", "您确定要创建新一期？？？")%>'
        ];
    </script>

    <script src="js/JScript11.js" type="text/jscript"></script>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <br />
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
			<tr>
				<td>
					<table width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
						<tr>
							<td height="48">
								<%=GetTran("001135", "显示从")%>&nbsp;
								<asp:TextBox id="TextBox1" runat="server" Width="64px" MaxLength="10"></asp:TextBox>&nbsp;<%=GetTran("001136", "期至")%>&nbsp;
								<asp:TextBox id="TextBox2" runat="server" Width="64px" MaxLength="10"></asp:TextBox>&nbsp;<%=GetTran("001137", "期的结算列表")%>
								<asp:Label id="Label1" runat="server"></asp:Label>
								&nbsp;<asp:Button id="Button1" runat="server" Text="确 定" onclick="Button1_Click"  CssClass="anyes"></asp:Button>
								&nbsp;<asp:button id="addNewQishu" Runat="server" Text="创建新一期" onclick="addNewQishu_Click"  CssClass="another"></asp:button>&nbsp;&nbsp;
                                <asp:button id="btnjslog" Runat="server" Text="结算日志" onclick="btnjslog_Click"  
                                    CssClass="another"></asp:button></td>
						</tr>
					</table>
					<table id="__01"  width="100%" border="0" cellpadding="0" cellspacing="0" class="tablemb">
						<tr>
							<td valign="top">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                    Width="100%" onrowdatabound="GridView1_RowDataBound" >
                                    <RowStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" CssClass="tablebt"/>
                                        <AlternatingRowStyle BackColor="#F1F4F8" />
                                    <Columns>
                                        <asp:BoundField HeaderText="创建日期" DataField="Date" DataFormatString="{0:d}"/>
                                        <asp:BoundField HeaderText="结算期数" DataField="ExpectNum"/>
                                        <asp:BoundField HeaderText="是否已结算" DataField="jsflag"/>
                                        <asp:BoundField HeaderText="结算次数" DataField="jsnum"/>
                                        <%--<asp:TemplateField HeaderText="操作">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink id="HyperJieSuan" runat="server" Visible="False"><%#GetTran("001138", "结算")%></asp:HyperLink>
												<DIV id="jiesuan" runat="server"></DIV>
												<INPUT id=HidQishu type=hidden value='<%# DataBinder.Eval(Container, "DataItem.ExpectNum")%>' name=HidQishu runat="server">
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>
                                       
                                </asp:GridView>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
		<br />
		<table class="tablemb" style="border:none;display:none;">
			<tr>
			    <td>
                    <%#GetTran("001138", "结算")%>
                    <table cellspacing="0" cellpadding="0" >
						<tr>
							<td><%=GetTran("007848", "自动结算时间")%>：&nbsp;
							</td>
							<td align="center">
                                <asp:TextBox ID="TextBox5" runat="server" CssClass="Wdate"
                                onfocus="new WdatePicker()"></asp:TextBox></td>
							<td>&nbsp;&nbsp;
								<asp:dropdownlist id="DropDownList1" runat="server">
									<asp:ListItem Value="00">00</asp:ListItem>
									<asp:ListItem Value="01">01</asp:ListItem>
									<asp:ListItem Value="02">02</asp:ListItem>
									<asp:ListItem Value="03">03</asp:ListItem>
									<asp:ListItem Value="04">04</asp:ListItem>
									<asp:ListItem Value="05">05</asp:ListItem>
									<asp:ListItem Value="06">06</asp:ListItem>
									<asp:ListItem Value="07">07</asp:ListItem>
									<asp:ListItem Value="08">08</asp:ListItem>
									<asp:ListItem Value="09">09</asp:ListItem>
									<asp:ListItem Value="10">10</asp:ListItem>
									<asp:ListItem Value="11">11</asp:ListItem>
									<asp:ListItem Value="12">12</asp:ListItem>
									<asp:ListItem Value="13">13</asp:ListItem>
									<asp:ListItem Value="14">14</asp:ListItem>
									<asp:ListItem Value="15">15</asp:ListItem>
									<asp:ListItem Value="16">16</asp:ListItem>
									<asp:ListItem Value="17">17</asp:ListItem>
									<asp:ListItem Value="18">18</asp:ListItem>
									<asp:ListItem Value="19">19</asp:ListItem>
									<asp:ListItem Value="20">20</asp:ListItem>
									<asp:ListItem Value="21">21</asp:ListItem>
									<asp:ListItem Value="22">22</asp:ListItem>
									<asp:ListItem Value="23">23</asp:ListItem>
								</asp:dropdownlist><%=GetTran("007002","时") %>&nbsp;&nbsp;
								<asp:dropdownlist id="DropDownList2" runat="server">
									<asp:ListItem Value="00">00</asp:ListItem>
									<asp:ListItem Value="01">01</asp:ListItem>
									<asp:ListItem Value="02">02</asp:ListItem>
									<asp:ListItem Value="03">03</asp:ListItem>
									<asp:ListItem Value="04">04</asp:ListItem>
									<asp:ListItem Value="05">05</asp:ListItem>
									<asp:ListItem Value="06">06</asp:ListItem>
									<asp:ListItem Value="07">07</asp:ListItem>
									<asp:ListItem Value="08">08</asp:ListItem>
									<asp:ListItem Value="09">09</asp:ListItem>
									<asp:ListItem Value="10">10</asp:ListItem>
									<asp:ListItem Value="11">11</asp:ListItem>
									<asp:ListItem Value="12">12</asp:ListItem>
									<asp:ListItem Value="13">13</asp:ListItem>
									<asp:ListItem Value="14">14</asp:ListItem>
									<asp:ListItem Value="15">15</asp:ListItem>
									<asp:ListItem Value="16">16</asp:ListItem>
									<asp:ListItem Value="17">17</asp:ListItem>
									<asp:ListItem Value="18">18</asp:ListItem>
									<asp:ListItem Value="19">19</asp:ListItem>
									<asp:ListItem Value="20">20</asp:ListItem>
									<asp:ListItem Value="21">21</asp:ListItem>
									<asp:ListItem Value="22">22</asp:ListItem>
									<asp:ListItem Value="23">23</asp:ListItem>
									<asp:ListItem Value="24">24</asp:ListItem>
									<asp:ListItem Value="25">25</asp:ListItem>
									<asp:ListItem Value="26">26</asp:ListItem>
									<asp:ListItem Value="27">27</asp:ListItem>
									<asp:ListItem Value="28">28</asp:ListItem>
									<asp:ListItem Value="29">29</asp:ListItem>
									<asp:ListItem Value="30">30</asp:ListItem>
									<asp:ListItem Value="31">31</asp:ListItem>
									<asp:ListItem Value="32">32</asp:ListItem>
									<asp:ListItem Value="33">33</asp:ListItem>
									<asp:ListItem Value="34">34</asp:ListItem>
									<asp:ListItem Value="35">35</asp:ListItem>
									<asp:ListItem Value="36">36</asp:ListItem>
									<asp:ListItem Value="37">37</asp:ListItem>
									<asp:ListItem Value="38">38</asp:ListItem>
									<asp:ListItem Value="39">39</asp:ListItem>
									<asp:ListItem Value="40">40</asp:ListItem>
									<asp:ListItem Value="41">41</asp:ListItem>
									<asp:ListItem Value="42">42</asp:ListItem>
									<asp:ListItem Value="43">43</asp:ListItem>
									<asp:ListItem Value="44">44</asp:ListItem>
									<asp:ListItem Value="45">45</asp:ListItem>
									<asp:ListItem Value="46">46</asp:ListItem>
									<asp:ListItem Value="47">47</asp:ListItem>
									<asp:ListItem Value="48">48</asp:ListItem>
									<asp:ListItem Value="49">49</asp:ListItem>
									<asp:ListItem Value="50">50</asp:ListItem>
									<asp:ListItem Value="51">51</asp:ListItem>
									<asp:ListItem Value="52">52</asp:ListItem>
									<asp:ListItem Value="53">53</asp:ListItem>
									<asp:ListItem Value="54">54</asp:ListItem>
									<asp:ListItem Value="55">55</asp:ListItem>
									<asp:ListItem Value="56">56</asp:ListItem>
									<asp:ListItem Value="57">57</asp:ListItem>
									<asp:ListItem Value="58">58</asp:ListItem>
									<asp:ListItem Value="59">59</asp:ListItem>
								</asp:dropdownlist><%=GetTran("007003","分")%>&nbsp;&nbsp;
								<asp:dropdownlist id="DropDownList3" runat="server">
									<asp:ListItem Value="00">00</asp:ListItem>
									<asp:ListItem Value="01">01</asp:ListItem>
									<asp:ListItem Value="02">02</asp:ListItem>
									<asp:ListItem Value="03">03</asp:ListItem>
									<asp:ListItem Value="04">04</asp:ListItem>
									<asp:ListItem Value="05">05</asp:ListItem>
									<asp:ListItem Value="06">06</asp:ListItem>
									<asp:ListItem Value="07">07</asp:ListItem>
									<asp:ListItem Value="08">08</asp:ListItem>
									<asp:ListItem Value="09">09</asp:ListItem>
									<asp:ListItem Value="10">10</asp:ListItem>
									<asp:ListItem Value="11">11</asp:ListItem>
									<asp:ListItem Value="12">12</asp:ListItem>
									<asp:ListItem Value="13">13</asp:ListItem>
									<asp:ListItem Value="14">14</asp:ListItem>
									<asp:ListItem Value="15">15</asp:ListItem>
									<asp:ListItem Value="16">16</asp:ListItem>
									<asp:ListItem Value="17">17</asp:ListItem>
									<asp:ListItem Value="18">18</asp:ListItem>
									<asp:ListItem Value="19">19</asp:ListItem>
									<asp:ListItem Value="20">20</asp:ListItem>
									<asp:ListItem Value="21">21</asp:ListItem>
									<asp:ListItem Value="22">22</asp:ListItem>
									<asp:ListItem Value="23">23</asp:ListItem>
									<asp:ListItem Value="24">24</asp:ListItem>
									<asp:ListItem Value="25">25</asp:ListItem>
									<asp:ListItem Value="26">26</asp:ListItem>
									<asp:ListItem Value="27">27</asp:ListItem>
									<asp:ListItem Value="28">28</asp:ListItem>
									<asp:ListItem Value="29">29</asp:ListItem>
									<asp:ListItem Value="30">30</asp:ListItem>
									<asp:ListItem Value="31">31</asp:ListItem>
									<asp:ListItem Value="32">32</asp:ListItem>
									<asp:ListItem Value="33">33</asp:ListItem>
									<asp:ListItem Value="34">34</asp:ListItem>
									<asp:ListItem Value="35">35</asp:ListItem>
									<asp:ListItem Value="36">36</asp:ListItem>
									<asp:ListItem Value="37">37</asp:ListItem>
									<asp:ListItem Value="38">38</asp:ListItem>
									<asp:ListItem Value="39">39</asp:ListItem>
									<asp:ListItem Value="40">40</asp:ListItem>
									<asp:ListItem Value="41">41</asp:ListItem>
									<asp:ListItem Value="42">42</asp:ListItem>
									<asp:ListItem Value="43">43</asp:ListItem>
									<asp:ListItem Value="44">44</asp:ListItem>
									<asp:ListItem Value="45">45</asp:ListItem>
									<asp:ListItem Value="46">46</asp:ListItem>
									<asp:ListItem Value="47">47</asp:ListItem>
									<asp:ListItem Value="48">48</asp:ListItem>
									<asp:ListItem Value="49">49</asp:ListItem>
									<asp:ListItem Value="50">50</asp:ListItem>
									<asp:ListItem Value="51">51</asp:ListItem>
									<asp:ListItem Value="52">52</asp:ListItem>
									<asp:ListItem Value="53">53</asp:ListItem>
									<asp:ListItem Value="54">54</asp:ListItem>
									<asp:ListItem Value="55">55</asp:ListItem>
									<asp:ListItem Value="56">56</asp:ListItem>
									<asp:ListItem Value="57">57</asp:ListItem>
									<asp:ListItem Value="58">58</asp:ListItem>
									<asp:ListItem Value="59">59</asp:ListItem>
								</asp:dropdownlist><%=GetTran("007629", "秒")%>&nbsp;&nbsp;&nbsp;
							</td>
						</tr>
						<tr>
							<td colspan="3">
								&nbsp;
							</td>
						</tr>
						<tr>
							<td colspan="3">
								<%=GetTran("007849", "结算周期")%>：<asp:TextBox ID="jszq" runat="server" Width="20px"></asp:TextBox><%=GetTran("007853","天")%>&nbsp;&nbsp;&nbsp;
								<%=GetTran("007850", "离结算还有")%>：<input type="text" id="lzdjs" style="width:160px;border:none;color:Red;font-weight:bold;" readonly>&nbsp;&nbsp;&nbsp;<input id="CheckBox3" onclick="ck(this)" type="checkbox"><%=GetTran("007852", "结算时创建新一期")%>
								<asp:button id="Button3" runat="server" Text="确定" 
                                    onclick="Button3_Click"  CssClass="anyes"></asp:button>&nbsp;&nbsp;&nbsp;
								<asp:CheckBox id="CheckBox2" runat="server" AutoPostBack="True" 
                                    oncheckedchanged="CheckBox2_CheckedChanged"></asp:CheckBox><%=GetTran("007851","启用")%>&nbsp;
							</td>
						</tr>
						<tr style="DISPLAY:none ">
							<td align="center" colSpan="3"><asp:textbox id="Textbox4" runat="server"></asp:textbox>
							<asp:TextBox id="shijid" runat="server"></asp:TextBox><asp:TextBox id="nowtimeid" runat="server"></asp:TextBox>
							</td>
						</tr>
					</table>
			    </td>
			</tr>
		</table>
			
		<table  width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi" align="center" style="DISPLAY: none">
			<tr>
				<td align="center" style="HEIGHT: 55px">
                    <%--<%=GetTran("006925", "设置自动结算周期(天数)：")%><asp:TextBox id="TextBox3" runat="server" MaxLength="3"></asp:TextBox>
			        <asp:Button id="Button2" runat="server" Text="设置" CssClass="anyes" 
                        onclick="Button2_Click"></asp:Button>
			        <asp:CheckBox id="CheckBox1" runat="server" AutoPostBack="True" 
                        oncheckedchanged="CheckBox1_CheckedChanged"></asp:CheckBox><%=GetTran("006926", "启用自动结算")%>--%>
					<br>
				</td>
			</tr>
			<tr align="center">
				<td>
					<div style="DISPLAY: inline; WIDTH: 235px; HEIGHT: 15px" ms_positioning="FlowLayout"></div>
					<table>
						<tr>
							<td>
								<asp:table id="qishuTable" ForeColor="red" Runat="server" BorderWidth="0px" GridLines="Both"
									BorderStyle="Solid" HorizontalAlign="Center" Width="200px"></asp:table>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
		<div id="cssrain" style="width:100%">
            <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
                <tr>
                    <td width="80px">
                        <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
                            <tr>
                                <td class="sec2">
                                    <span id="span1" title="" onmouseover="cutDescription()"><%=GetTran("000033", "说 明")%></span>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td><a href="#"><img src="images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX" onclick="down2()" /></a></td>
                </tr>
            </table>
            <div id="divTab2">
                <table width="100%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
                    <tbody style="display: block" id="tbody1">
                        <tr>
                            <td style="padding-left: 20px">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></td>
                                        <td></td>
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
