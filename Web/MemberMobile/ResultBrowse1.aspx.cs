﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BLL.CommonClass;
using BLL.MoneyFlows;
using Model.Other;
using System.Text;
using BLL.Logistics;

public partial class Member_ResultBrowse : BLL.TranslationBase
{
    public int id;
      public int bzCurrency = 0;
       
    protected void Page_Load(object sender, EventArgs e)
    {
        //获取标准币种
        bzCurrency = CommonDataBLL.GetStandard();
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        ViewState["fk"] = 0;
        ViewState["zf"] = 0;
        if (!IsPostBack)
        {
            id = Convert.ToInt32(Request["id"].ToString());
            this.DropDownList1.Items.Add(new ListItem(GetTran("000633", "全部"), "-1"));
            this.DropDownList1.Items.Add(new ListItem(GetTran("001009", "未审核"), "false"));
            this.DropDownList1.Items.Add(new ListItem(GetTran("001011", "已审核"), "true"));

            this.Datepicker1.Text = CommonDataBLL.GetDateBegin().ToString();
            //this.Datepicker2.Text = CommonDataBLL.GetDateEnd().ToString();
            GetShopList2();
            getTotalMoney(Session["Member"].ToString());
            Pager1.Visible = false;
        }

        //Translations();
    }
    //private void Translations()
    //{
    //    this.TranControls(this.BtnConfirm, new string[][] { new string[] { "000340", "查询" } });
    //    this.TranControls(this.GridView2,
    //            new string[][]{
    //                new string []{"000938","支付"},
    //                new string []{"000744","查看备注"},
    //                new string []{"000022","删除"},
    //                new string []{"000024","会员编号"},
    //                new string []{"000603","汇出金额"},
    //                new string []{"000775", "支付状态" },
    //                new string []{"000786","付款日期"},
    //                new string []{"000739","付款期数"},
    //                new string []{"005881","付款类型"},
    //                new string []{"000601","汇入银行"},
    //                new string []{"000519","经办人"},
    //                new string []{"000602","汇款人"},
    //                new string []{"000743","汇款人身份证"}
    //            });
    //}
    //绑定会员汇款
    private void GetShopList2()
    {
        StringBuilder condition = new StringBuilder();
        string table = " Remittances ,MemberInfo ";
        condition.Append("Remittances.id="+id+" and MemberInfo.Number=Remittances.RemitNumber and Remittances.relationorderid='' and Remittances.RemitStatus=1 and MemberInfo.Number='" + Session["Member"].ToString() + "' ");
        string BeginRiQi = "";
        string EndRiQi = "";
        if (this.Datepicker1.Text != "")
        {
            DateTime time = DateTime.Now.ToUniversalTime();
            bool b = DateTime.TryParse(Datepicker1.Text.Trim(), out time);
            if (!b)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000827", "时间格式不正确！") + "')</script>");
                return;
            }
            BeginRiQi = this.Datepicker1.Text.Trim().ToString();
            DisposeString.DisString(BeginRiQi, "'", "");
            if (this.Datepicker2.Text != "")
            {
                b = DateTime.TryParse(Datepicker2.Text.Trim(), out time);
                if (!b)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000827", "时间格式不正确！") + "')</script>");
                    return;
                }
                EndRiQi = (DateTime.Parse(this.Datepicker2.Text).AddHours(23).AddMinutes(59).AddSeconds(59)).ToString();
                DisposeString.DisString(EndRiQi, "'", "");

                condition.Append(" and ReceivablesDate>= '" + BeginRiQi + "' and ReceivablesDate<='" + EndRiQi + "'");
            }
            else
            {
                condition.Append(" and ReceivablesDate>= '" + BeginRiQi + "'");
            }
        }

        else
        {
            if (this.Datepicker2.Text != "")
            {
                DateTime time = DateTime.Now.ToUniversalTime();
                bool b = DateTime.TryParse(Datepicker2.Text.Trim(), out time);
                if (!b)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000827", "时间格式不正确！") + "')</script>");
                    return;
                }
                EndRiQi = (DateTime.Parse(this.Datepicker2.Text.Trim()).AddHours(23).AddMinutes(59).AddSeconds(59)).ToString();
                condition.Append(" and ReceivablesDate<='" + EndRiQi + "'");
            }
        }

        if (this.DropDownList1.SelectedValue != "-1")
        {
            condition.Append(" and IsGSQR='" + this.DropDownList1.SelectedValue.Trim() + "' ");
        }
        string cloumns = " Remittances.isgsqr,Remittances.remittancesid,Remittances.RemittancesMoney,Remittances.RemittancesCurrency,Remittances.RemitNumber, Remittances.Sender,Remittances.Managers,Remittances.ImportBank, Remittances.PayWay,Remittances.Remittancesid,Remittances.[Use],Remittances.StandardCurrency,Remittances.ConfirmType, Remittances.SenderID, Remittances.RemitMoney,Remittances.PayExpectNum,Remittances.Id,Remittances.ReceivablesDate,Remittances.PayExpectNum,Remittances.isgsqr,Remittances.Remark ";
        string key = "Remittances.id";
        ViewState["key"] = key;
        ViewState["PageColumn"] = cloumns;
        ViewState["table"] = table;
        ViewState["condition"] = condition.ToString();
        this.GridView2.DataSourceID = null;
        this.Pager1.ControlName = "rep1";
        this.Pager1.key = key;
        this.Pager1.PageColumn = cloumns;
        this.Pager1.Pageindex = 0;
        this.Pager1.PageTable = table;
        this.Pager1.Condition = condition.ToString();
        this.Pager1.PageSize = 10;
        this.Pager1.PageCount = 0;
        this.Pager1.PageBind();
        //Translations();
    }
   
     
   

    public string GetPayStatus(string paytype)
    {
        int type = 0;
        if (paytype.ToLower() == "true")
        {
            type = 1;
        }
        string payStatus = new BLL.Registration_declarations.GroupRegisterBLL().GetDeftrayState(type.ToString());
        return payStatus;
    }
    protected void linkbtnOK_Click(object sender, CommandEventArgs e)
    {
         string billid = EncryKey.GetEncryptstr(e.CommandArgument.ToString(), 2, 1);
        ScriptManager.RegisterStartupScript(this, GetType(), "mag", "var formobj=document.createElement('form');"
                         + "formobj.action='../payserver/chosepaysj.aspx?blif=" + billid + "';" +
                         "formobj.method='post';formobj.target='_blank';document.body.appendChild(formobj); formobj.submit();", true);
        //  Page.ClientScript.RegisterStartupScript(GetType(), null, "<script language='javascript'>window.open('../payserver/chosepay.aspx?blif=" + billid + "');</script>");
        return;
    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            ChangeLogs cl = new ChangeLogs("Remittances", "ltrim(rtrim(str(id)))");

            if (e.CommandArgument.ToString() == string.Empty)
            {
                return;
            }
            //得到更新的id
            //string updId = ((HtmlInputHidden)this.GridView1.SelectedRow.FindControl("HidId")).Value;
            GridViewRow gvrow = (GridViewRow)(((Image)e.CommandSource).NamingContainer);
            string updId = (this.GridView2.Rows[gvrow.RowIndex].FindControl("HidId") as HtmlInputHidden).Value;
            //判断汇款是否被删除
            bool blean = RemittancesBLL.MemberIsExist(int.Parse(updId));
            if (blean == false)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("000861", "不能重复删除！") + "')</script>");
                return;
            }
            cl.AddRecord(updId);

            if (updId == "" || updId == null)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000855", "参数出错！") + "')</script>");
                return;
            }
            //判断是否审核，不能删除已审核的单子
            Object obj = RemittancesBLL.IsMemberGSQR(int.Parse(updId));
            try
            {
                bool b = bool.Parse(obj.ToString());
                if (b == true)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("000867", "不能删除已审核的单子！") + "')</script>");
                    return;
                }
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("000852", "类型转换错误！") + "')</script>");
                return;
            }
            //删除未审核的单子
            RemittancesBLL.DeleteMemberMoney(Convert.ToInt32(updId));
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000874", "成功删除！") + "')</script>");
            BtnConfirm_Click(null, null);
            cl.AddRecord(updId);
            cl.ModifiedIntoLogs(ChangeCategory.member1, updId, ENUM_USERTYPE.objecttype5);

        }
        else if (e.CommandName == "Pay") { 
            string billid = EncryKey.GetEncryptstr(e.CommandArgument.ToString(), 2, 1);
            ScriptManager.RegisterStartupScript(this, GetType(), "mag", "var formobj=document.createElement('form');"
                             + "formobj.action='../payserver/chosepay.aspx?blif=" + billid + "';" +
                             "formobj.method='post';formobj.target='_blank';document.body.appendChild(formobj); formobj.submit();", true);
          //  Page.ClientScript.RegisterStartupScript(GetType(), null, "<script language='javascript'>window.open('../payserver/chosepay.aspx?blif=" + billid + "');</script>");
            return;
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");

            if (((HtmlInputHidden)e.Row.FindControl("HidGsqr")).Value == "True")
            {
                ((Image)e.Row.FindControl("LinkBtnDelete")).ImageUrl = "images/view-button3-.png";
                ((Image)e.Row.FindControl("LinkBtnDelete")).Enabled = false;
                ((Image)e.Row.FindControl("LinkBtnPay")).ImageUrl = "images/view-button1-.png";
                ((Image)e.Row.FindControl("LinkBtnPay")).Enabled = false;
            }
            else
            {
                //如果是在线支付未充值成功的没有付款日期
                if (e.Row.Cells[9].Text == "2")
                {
                    e.Row.Cells[7].Text = "";
                }
                ((Image)e.Row.FindControl("LinkBtnDelete")).ImageUrl = "images/view-button3.png";
                ((Image)e.Row.FindControl("LinkBtnDelete")).Attributes["onclick"] = "return confirm('" + GetTran("000836", "确定删除？") + "')";
                ((Image)e.Row.FindControl("LinkBtnPay")).ImageUrl = "images/view-button1.png";
            }

            if (((HtmlInputHidden)e.Row.FindControl("HidGsqr")).Value == "False")
            {
                ((Image)e.Row.FindControl("LinkBtnDelete")).ImageUrl = "images/view-button3.png";
                ((Image)e.Row.FindControl("LinkBtnDelete")).Attributes["onclick"] = "return confirm('" + GetTran("000836", "确定删除？") + "')";
            }
            //解密
            e.Row.Cells[12].Text = Encryption.Encryption.GetDecipherNumber(e.Row.Cells[12].Text.Trim().Replace("&nbsp;", ""));//解密汇款人身份证
            e.Row.Cells[11].Text = Encryption.Encryption.GetDecipherName(e.Row.Cells[11].Text.Trim());//解密汇款人


            string isGsqr = ((HtmlInputHidden)e.Row.FindControl("HidGsqr")).Value;
            string payway = ((HtmlInputHidden)e.Row.FindControl("HidPayway")).Value;
            Image lbtn = (Image)e.Row.FindControl("LinkBtnDelete");
            Image LinkPM = (Image)e.Row.FindControl("LinkBtnPay");

            double totalmoney = Convert.ToDouble(((HtmlInputHidden)e.Row.FindControl("HidTotalMoney")).Value);
            double totalcomm = 0;
            double zongMoney = totalcomm + totalmoney;
            string OrderID = ((HtmlInputHidden)e.Row.FindControl("Hidremittancesid")).Value;

            if (isGsqr.ToLower() == "true")
            {
                lbtn.ImageUrl = "images/view-button3-.png";
                lbtn.Enabled = false;
                LinkPM.Enabled = false;
                LinkPM.ImageUrl = "images/view-button1.png";
            }
            else
            {
                lbtn.ImageUrl = "images/view-button3.png";
                LinkPM.ImageUrl = "images/view-button1-.png";
            }
            ViewState["fk"] = Convert.ToDouble(ViewState["fk"]) + Convert.ToDouble(e.Row.Cells[4].Text);
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Attributes.Add("style", "background-image:url('images/detail-table-bg_03.png')");
            e.Row.Cells[3].Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + e.Row.Cells[3].Text + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = GetTran("007160", "本页总计");

            e.Row.Cells[4].Text = Convert.ToDouble(ViewState["fk"]).ToString("f2");
            e.Row.HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
        }

        //Translations();
    }

    protected void BtnConfirm_Click(object sender, EventArgs e)
    {
        GetShopList2();
    }
    //查看备注
    protected string SetVisible(string dd, string id)
    {
        if (dd.Length > 0)
        {
            string _openWin = "";
            _openWin = "<a href =\"javascript:void(window.open('ShowHuiKuanRemark.aspx?id=" + id + "&type=1','','width=800,height=130'))\"><img src=\"images/view-button.png\" width=\"20\" height=\"20\"> </a>";
            return _openWin;
        }
        else
        {
            return "<img src=\"images/view-button-.png\" width=\"20\" height=\"20\">";
        }
    }
    public void getTotalMoney(string number)
    {
        Label1.Text = "<b>" + GetTran("007591", "已支付付款金额") + "</b>" + "：<span style=\"color:red\">" + RemittancesBLL.GetTotalMoney("RemitMoney", "Remittances", " where isgsqr=1 and RemitStatus=1 and relationorderid='' and RemitNumber='" + number + "'") + "</span>";
        Label2.Text = "<b>" + GetTran("007592", "未支付付款金额") + "</b>" + "：<span style=\"color:red\">" + RemittancesBLL.GetTotalMoney("RemitMoney", "Remittances", " where isgsqr=0 and RemitStatus=1 and relationorderid='' and RemitNumber='" + number + "'") + "</span>";
    }
    /// <summary>
    /// 获取付款方式
    /// </summary>
    /// <param name="PayWay"></param>
    /// <returns></returns>
    protected string GetPayWay(string PayWay) {
            switch (PayWay)
            {
                case "0":
                    return GetTran("005963", "在线支付");
                case "1":
                    return GetTran("007594", "普通汇款");
                case "2":
                    return GetTran("007595", "汇款人工确认");
            }
        return "";
    }
protected void rep1_ItemCommand(object source, RepeaterCommandEventArgs e)
{
    string billid = EncryKey.GetEncryptstr(e.CommandArgument.ToString(), 2, 1);
    ScriptManager.RegisterStartupScript(this, GetType(), "mag", "var formobj=document.createElement('form');"
                     + "formobj.action='../payserver/chosepaysj.aspx?blif=" + billid + "';" +
                     "formobj.method='post';formobj.target='_blank';document.body.appendChild(formobj); formobj.submit();", true);
    //  Page.ClientScript.RegisterStartupScript(GetType(), null, "<script language='javascript'>window.open('../payserver/chosepay.aspx?blif=" + billid + "');</script>");
    return;
}
}

