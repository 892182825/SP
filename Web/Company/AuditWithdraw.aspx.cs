using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Model;
using BLL;
using BLL.MoneyFlows;
using BLL.CommonClass;
using Model.Other;
using System.Collections.Generic;
using System.Text;
using DAL;
using Standard.Classes;
using System.Data.SqlClient;
using BLL.Logistics;

public partial class Company_AuditWithdraw : BLL.TranslationBase
{
    ProcessRequest process = new ProcessRequest();
    protected int chb = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Expires = 0;

        //检查相应权限
        Permissions.CheckManagePermission(EnumCompanyPermission.FinanceAuditingStoreAccount);
        Response.Cache.SetExpires(DateTime.Now.ToUniversalTime());
        ViewState["byzj"] = 0;
        ViewState["zj"] = 0;
        ViewState["yzj"] = 0;
        ViewState["wzj"] = 0;
        if (!IsPostBack)
        {
            if (Session["Company"] == null)
            {
                Response.Write("Index.aspx");
            }
            this.DropDownList1.Items.Add(new ListItem(GetTran("000633", "全部"), "-1"));
            this.DropDownList1.Items.Add(new ListItem(GetTran("007168", "待审核"), "0"));
            this.DropDownList1.Items.Add(new ListItem(GetTran("007254", "处理中"), "1"));
            this.DropDownList1.Items.Add(new ListItem(GetTran("007169", "已汇出"), "2"));
            this.DropDownList1.Items.Add(new ListItem(GetTran("007171", "账号错误"), "3"));

            //绑定国家
            BindCountry_List();
            this.Datepicker1.Text = CommonDataBLL.GetDateBegin().ToString();
            this.Datepicker2.Text = CommonDataBLL.GetDateEnd().ToString();
            GetShopList2();
            getTotal();
            translation();
        }
        chb = Pager1.PageSize;
    }

    private void translation()
    {
        TranControls(BtnConfirm, new string[][]{
            new string[]{"000340","查询"}
        });
        TranControls(GridView1, new string[][]{
            new string[]{"000000","<input type=\"checkbox\" id=\"checkAll\" />"},
            new string[]{"000761","审核"},
            new string[]{"000024","会员编号"},
            new string[]{"000025","会员姓名"},
            new string[]{"000000","总金额"},
            new string[]{"006984","提现金额"},
            new string[]{"006983","审核状态"},
            new string[]{"000000","提现币种"},
            new string[]{"000000","手机号"},
            new string[]{"006986","提现时间"},
            new string[]{"001155","审核时间"},
            new string[]{"000000","提现位置"},
            
            new string[]{"000744","查看备注"},
            new string[]{"000022","删除"}
        });
        TranControls(rad_list, new string[][] { new string[] { "007169", "已汇出" }, new string[] { "007822", "账号出错" }, new string[] { "007170", "开始处理" } });
        TranControls(btn_listsubmit, new string[][] { new string[] { "007823", "批量处理" } });
        TranControls(ddlType, new string[][]{
            new string[]{"000361","大于"},
            new string[]{"000364","大于等于"},
            new string[]{"000372","等于"},
            new string[]{"000367","小于"},
            new string[]{"000368","小于等于"}
        });
    }

    /// <summary>
    /// 绑定国家
    /// </summary>
    private void BindCountry_List()
    {
        IList<CountryModel> list = RemittancesBLL.BindCountry_List();
        this.DropCurrency.DataSource = list;
        this.DropCurrency.DataTextField = "Name";
        this.DropCurrency.DataValueField = "CountryCode";
        this.DropCurrency.DataBind();
        this.DropCurrency.Items.Add(new ListItem(GetTran("000633", "全部"), "-1"));
        this.DropCurrency.SelectedValue = "-1";
    }

    private void GetShopList2()
    {
        StringBuilder condition = new StringBuilder();
        string table = " MemberCash W ,MemberInfo M ";
        condition.Append(" M.Number=w.Number  ");

        string BeginRiQi = "";
        string EndRiQi = "";
        if (this.Datepicker1.Text != "")
        {
            try
            {
                DateTime time = DateTime.Parse(this.Datepicker1.Text);
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000827", "时间格式不正确！") + "')</script>");
                return;
            }
            BeginRiQi = this.Datepicker1.Text.Trim().ToString();
            DisposeString.DisString(BeginRiQi, "'", "");
            if (this.Datepicker2.Text != "")
            {
                try
                {
                    DateTime time = DateTime.Parse(this.Datepicker2.Text);
                }

                catch
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000827", "时间格式不正确！") + "')</script>");
                    return;
                }
                EndRiQi = DateTime.Parse(this.Datepicker2.Text.Trim()).AddHours(23).AddMinutes(59).AddSeconds(59).ToString();
                DisposeString.DisString(EndRiQi, "'", "");

                condition.Append(" and  WithdrawTime>= '" + BeginRiQi + "' and WithdrawTime<='" + EndRiQi + "'");
            }
            else
            {
                condition.Append(" and WithdrawTime>= '" + BeginRiQi + "'");
            }
        }
        else
        {
            if (this.Datepicker2.Text != "")
            {
                try
                {
                    DateTime time = DateTime.Parse(this.Datepicker2.Text);
                }

                catch
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000827", "时间格式不正确！") + "')</script>");
                    return;
                }
                EndRiQi = DateTime.Parse(this.Datepicker2.Text.Trim()).AddHours(23).AddMinutes(59).AddSeconds(59).ToString();
                condition.Append(" and WithdrawTime<='" + EndRiQi + "'");
            }
        }
        if (this.DropDownList1.SelectedValue != "-1")
        {
            condition.Append(" and isAuditing='" + this.DropDownList1.SelectedValue.Trim() + "' ");
        }

        if (this.txtNumber.Text.Trim() != "")
        {
            condition.Append(" and w.Number like '%" + this.txtNumber.Text.Trim() + "%'");
        }
        if (this.txtMoney.Text.Trim() != "")
        {
            try
            {
                Convert.ToDouble(this.txtMoney.Text.Trim());
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("001094", "金额输入不正确！") + "')</script>");
                return;
            }
            string fuhao = "";
            switch (this.ddlType.SelectedValue)
            {
                case "0":
                    fuhao = ">";
                    break;
                case "1":
                    fuhao = ">=";
                    break;
                case "2":
                    fuhao = "=";
                    break;
                case "3":
                    fuhao = "<=";
                    break;
                case "4":
                    fuhao = "<";
                    break;
                default:
                    fuhao = "=";
                    break;
            }
            condition.Append(" and w.withdrawMoney " + fuhao + Convert.ToDouble(this.txtMoney.Text.Trim()));
        }

        string cloumns = " w.withdrawMoney-w.WithdrawSXF as withdrawMoneys,w.Number,w.isauditing,w.Id,w.withdrawsxf,w.withdrawMoney,w.WithdrawSXF,w.wyj,w.WithdrawTime,w.bankcard,w.AuditingTime,w.Remark,w.IsJL,m.name as name";
        string key = " w.id ";
        ViewState["key"] = key;
        ViewState["PageColumn"] = cloumns;
        ViewState["table"] = table;
        ViewState["condition"] = condition.ToString();
        this.GridView1.DataSourceID = null;
        this.Pager1.ControlName = "GridView1";
        this.Pager1.key = key;
        this.Pager1.PageColumn = cloumns;
        this.Pager1.Pageindex = 0;
        this.Pager1.PageTable = table;
        this.Pager1.Condition = condition.ToString();
        this.Pager1.PageSize = 10;
        this.Pager1.PageCount = 0;
        this.Pager1.PageBind();
        translation();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");

            string isAuditing = ((HtmlInputHidden)e.Row.FindControl("HidisAuditingr")).Value;
            if (isAuditing == "1")//开始处理
            {
                ((LinkButton)e.Row.FindControl("LinkButton1")).Visible = true;
                ((LinkButton)e.Row.FindControl("LinkButton2")).Visible = true;
                ((LinkButton)e.Row.FindControl("LinkButton3")).Visible = false;
                ((LinkButton)e.Row.FindControl("LinkBtnDelete")).Visible = true;
                ((LinkButton)e.Row.FindControl("LinkButton1")).Attributes["onclick"] = "return confirm('" + GetTran("000834", "确定审核？") + "')";
                ((LinkButton)e.Row.FindControl("LinkButton2")).Attributes["onclick"] = "return confirm('" + GetTran("007172", "是否确定账号错误？") + "')";
            }
            else if (isAuditing == "2")//已汇出
            {
                ((CheckBox)e.Row.FindControl("chb")).Visible = false;
                ((LinkButton)e.Row.FindControl("LinkButton1")).Visible = false;
                ((LinkButton)e.Row.FindControl("LinkButton2")).Visible = false;
                ((LinkButton)e.Row.FindControl("LinkButton3")).Visible = false;
                ((LinkButton)e.Row.FindControl("LinkBtnDelete")).Visible = false;

            }
            else if (isAuditing == "0")//待审核
            {
                ((LinkButton)e.Row.FindControl("LinkButton1")).Visible = false;
                ((LinkButton)e.Row.FindControl("LinkButton2")).Visible = true;
                ((LinkButton)e.Row.FindControl("LinkButton3")).Visible = true;
                ((LinkButton)e.Row.FindControl("LinkBtnDelete")).Visible = true;
                ((LinkButton)e.Row.FindControl("LinkBtnDelete")).Attributes["onclick"] = "return confirm('" + GetTran("000836", "确定删除？") + "')";
                ((LinkButton)e.Row.FindControl("LinkButton3")).Attributes["onclick"] = "return confirm('" + GetTran("007198", "确定开始处理") + "？')";
                ((LinkButton)e.Row.FindControl("LinkButton2")).Attributes["onclick"] = "return confirm('" + GetTran("007172", "是否确定账号错误？") + "')";
            }
            else//账号错误
            {
                ((CheckBox)e.Row.FindControl("chb")).Visible = false;
                ((LinkButton)e.Row.FindControl("LinkButton1")).Visible = false;
                ((LinkButton)e.Row.FindControl("LinkButton2")).Visible = false;
                ((LinkButton)e.Row.FindControl("LinkButton3")).Visible = false;
                ((LinkButton)e.Row.FindControl("LinkBtnDelete")).Visible = false;
            }
            ViewState["byzj"] = Convert.ToDouble(ViewState["byzj"]) + Convert.ToDouble(e.Row.Cells[4].Text);
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Attributes.Add("style", "background-image:url('images/tabledp.gif')");
            e.Row.Cells[1].Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + e.Row.Cells[1].Text + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
            e.Row.Cells[1].Text = GetTran("007160", "本页合计");
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[4].Text = Convert.ToDouble(ViewState["byzj"]).ToString("f2");
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string[] args = e.CommandArgument.ToString().Split(',');

        string numebr = args[0].ToString();
        string isAuditing = args[1].ToString();
        int id = Convert.ToInt32(args[2].ToString());
        double money = Convert.ToDouble(args[3].ToString());
        double withdrawSXF = Convert.ToDouble(args[4].ToString());
        double wyj = Convert.ToDouble(args[5].ToString());
        int IsJL=Convert.ToInt16(args[6].ToString());
        WithdrawModel wDraw = new WithdrawModel();
        wDraw.Id = id;
        wDraw.Number = numebr;
        wDraw.ApplicationExpecdtNum = BLL.CommonClass.CommonDataBLL.getMaxqishu();
        wDraw.WithdrawMoney = money;
        wDraw.WithdrawSXF = withdrawSXF;
        wDraw.Wyj = wyj;
        wDraw.AuditExpectNum = BLL.CommonClass.CommonDataBLL.getMaxqishu();
        wDraw.AuditingIP = BLL.CommonClass.CommonDataBLL.OperateIP;
        wDraw.AuditingManageId = BLL.CommonClass.CommonDataBLL.OperateBh;
        wDraw.AuditTime = DateTime.Now.ToUniversalTime();
        wDraw.IsJL = IsJL;

        if(e.CommandName.ToString() == "Lbtn")
        {
            if (BLL.Registration_declarations.RegistermemberBLL.GetMemberCashAuditState(id) == 2)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("007176", "该申请单已经审核，不可以重复审核！") + "')</script>");
                this.BtnConfirm_Click(null, null);
                return;
            }

            if (!BLL.Registration_declarations.RegistermemberBLL.isDelMemberCash(id))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("007177", "该申请单已经被删除！") + "')</script>");
                this.BtnConfirm_Click(null, null);
                return;
            }
            if (wDraw.IsJL != 1)
            {
                double leftMoney = Convert.ToDouble(BLL.CommonClass.CommonDataBLL.GetLeftMoney1(numebr));
                if (money > leftMoney)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("007175", "可提现余额不足，不能审核！") + "')</script>");
                    return;
                }
            }
            Application.Lock();
            bool isSure = false;
            
                isSure = BLL.Registration_declarations.RegistermemberBLL.AuditWithdraw(wDraw);
                
            Application.UnLock();
            if (isSure)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000858", "审核成功！") + "')</script>");
                this.BtnConfirm_Click(null, null);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("006041", "审核失败！") + "')</script>");
                return;
            }
        }
        if (e.CommandName == "Del")
        {
            if (BLL.Registration_declarations.RegistermemberBLL.GetMemberCashAuditState(id) == 1)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("007174", "该申请单已经审核，不可以删除！") + "');</script>");
                return;
            }
            if (BLL.Registration_declarations.RegistermemberBLL.GetMemberCashAuditState(id) == 3)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("007173", "该申请单账号错误，不可以删除！") + "');</script>");
                return;
            }

            if (!BLL.Registration_declarations.RegistermemberBLL.isDelMemberCash(id))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("007178", "该申请单已经删除，不可以删除！") + "')</script>");
                this.BtnConfirm_Click(null, null);
                return;
            }

            ChangeLogs cl = new ChangeLogs("MemberCash", "ltrim(rtrim(str(id)))");
            cl.AddRecord(wDraw.Id);
            Application.Lock();
            bool isSure = BLL.Registration_declarations.RegistermemberBLL.DeleteWithdraw(wDraw);
            Application.UnLock();
            if (isSure)
            {
                cl.AddRecord(wDraw.Id);
                cl.DeletedIntoLogs(ChangeCategory.company14, Session["Company"].ToString(), ENUM_USERTYPE.objecttype5);

                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000008", "删除成功！)") + "')</script>");
                this.BtnConfirm_Click(null, null);
                return;
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000009", "删除失败！)") + "')</script>");
                return;
            }
        }
        //账号错误
        if (e.CommandName == "carderror")
        {
            if (BLL.Registration_declarations.RegistermemberBLL.GetMemberCashAuditState(id) == 2)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("007181", "该申请单已经审核，不可以转成账号错误！") + "');</script>");
                return;
            }
            if (BLL.Registration_declarations.RegistermemberBLL.GetMemberCashAuditState(id) == 3)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("007180", "该申请单已经是账号错误，不可以重复账号错误！") + "');</script>");
                return;
            }

            if (!BLL.Registration_declarations.RegistermemberBLL.isDelMemberCash(id))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("007179", "该申请单已经删除，不可以转成账号错误！") + "')</script>");
                this.BtnConfirm_Click(null, null);
                return;
            }

            ChangeLogs cl = new ChangeLogs("MemberCash", "ltrim(rtrim(str(id)))");
            cl.AddRecord(wDraw.Id);
            Application.Lock();
            bool isSure = false;
           
                isSure = BLL.Registration_declarations.RegistermemberBLL.updateCardEorror(wDraw.Id, wDraw.WithdrawMoney , wDraw.Number);
            
            Application.UnLock();
            if (isSure)
            {
                cl.AddRecord(wDraw.Id);
                cl.DeletedIntoLogs(ChangeCategory.company14, Session["Company"].ToString(), ENUM_USERTYPE.objecttype5);

                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("001401", "操作成功！)") + "')</script>");
                this.BtnConfirm_Click(null, null);
                return;
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("001541", "操作失败！)") + "')</script>");
                return;
            }
        }
        //开始处理
        if (e.CommandName == "kscl")
        {
            if (BLL.Registration_declarations.RegistermemberBLL.GetMemberCashAuditState(id) == 1)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("007182", "该申请单已经审核，不可以在开始处理！") + "');</script>");
                return;
            }
            if (BLL.Registration_declarations.RegistermemberBLL.GetMemberCashAuditState(id) == 2)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("007183", "该申请单已经开始处理，不可以在开始处理！") + "');</script>");
                return;
            }
            if (BLL.Registration_declarations.RegistermemberBLL.GetMemberCashAuditState(id) == 3)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("007184", "该申请单已经是账号错误，不可以在开始处理！") + "');</script>");
                return;
            }

            if (!BLL.Registration_declarations.RegistermemberBLL.isDelMemberCash(id))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("007185", "该申请单已经删除，不可以在开始处理！") + "')</script>");
                this.BtnConfirm_Click(null, null);
                return;
            }

            ChangeLogs cl = new ChangeLogs("Withdraw", "ltrim(rtrim(str(id)))");
            cl.AddRecord(wDraw.Id);
            Application.Lock();
            bool isSure = BLL.Registration_declarations.RegistermemberBLL.updateKscl(wDraw.Id);
            Application.UnLock();
            if (isSure)
            {
                cl.AddRecord(wDraw.Id);
                cl.DeletedIntoLogs(ChangeCategory.company14, Session["Company"].ToString(), ENUM_USERTYPE.objecttype5);

                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("001401", "操作成功！)") + "')</script>");
                this.BtnConfirm_Click(null, null);
                return;
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("001541", "操作失败！)") + "')</script>");
                return;
            }
        }
    }

    protected string GetNumberName(string name)
    {
        //解密姓名
        string namestr = Encryption.Encryption.GetDecipherName(name);
        return namestr;
    }

    protected string GetAuditState(string auditState)
    {
        string msg = "";
        switch (auditState)
        {
            case "0":
                msg = "<font color='red'>" + GetTran("007168", "待审核") + "</font>";
                break;
            case "1":
                msg = "<font color='red'>" + GetTran("007254", "处理中") + "</font>";
                break;
            case "2":
                msg = "<font color='red'>" + GetTran("007169", "已汇出") + "</font>";
                break;
            case "3":
                msg = "<font color='red'>" + GetTran("007824", "账号有误") + "</font>";
                break;
            default:
                msg = "<font color='red'>" + GetTran("007168", "待审核") + "</font>";
                break;
        }
        return msg;
    }

    protected string GetAuditExpectNum(string AuditExpect)
    {
        if (AuditExpect == "0")
        {
            return "";
        }
        else
        {
            return AuditExpect.ToString();
        }
    }

    protected string GetAuditTime(string AuditTime)
    {
        if (AuditTime == "1900-1-1 0:00:00")
        {
            return "";
        }
        else
        {
            DateTime Dtime = Convert.ToDateTime(AuditTime);
            return Dtime.AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString("yyyy-MM-dd HH:mm:ss");
        }
    }

    protected string GetWithdrawTime(string WithdrawTime)
    {
        DateTime Dtime = Convert.ToDateTime(WithdrawTime);
        return Dtime.AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString("yyyy-MM-dd HH:mm:ss");
    }

    protected string GetName(string name)
    {
        return Encryption.Encryption.GetDecipherName(name);
    }
    protected string GetBankCard(string name)
    {
        return Encryption.Encryption.GetDecipherName(name);
    }
    protected void BtnConfirm_Click(object sender, EventArgs e)
    {
        this.GetShopList2();
        getTotal();
    }
    //查看备注
    protected string SetVisible(string dd, string id)
    {
        if (dd.Length > 0)
        {
            string _openWin = "";

            _openWin = "<a href =\"javascript:void(window.open('ShowHuiKuanRemark.aspx?id=" + id + "&strtype=1','','width=500,height=130'))\">" + GetTran("000440", "查看") + "</a>";

            return _openWin;
        }
        else
        {
            return GetTran("000221", "无");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string cmd = "select w.Remark,w.AuditingTime,w.WithdrawTime,w.AuditExpectNum,w.ApplicationExpectNum,w.isauditing,w.withdrawMoney,w.Number,m.name as name,m.bankcode,m.bankbranchname,m.bankbook,m.bankcard from " + ViewState["table"].ToString() + " where " + ViewState["condition"].ToString() + "";
        DataTable dt1 = DBHelper.ExecuteDataTable(cmd);
        if (dt1 == null || dt1.Rows.Count < 1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("000053", "没有数据，不能导出Excel！") + "')</script>");
            return;
        }


        DataTable dt = new DataTable();
        dt = dt1.Clone();
        dt.Columns[0].DataType = typeof(String);
        dt.Columns[1].DataType = typeof(String);
        dt.Columns[2].DataType = typeof(String);
        dt.Columns[3].DataType = typeof(String);
        dt.Columns[4].DataType = typeof(String);
        dt.Columns[5].DataType = typeof(String);
        dt.Columns[6].DataType = typeof(String);
        dt.Columns[7].DataType = typeof(String);
        dt.Columns[8].DataType = typeof(String);
        dt.Columns[10].DataType = typeof(String);
        dt.Columns[11].DataType = typeof(String);
        dt.Columns[9].DataType = typeof(String);
        foreach (DataRow r in dt1.Rows)
        {
            DataRow newrow = dt.NewRow();
            newrow["Number"] = r["Number"];
            newrow["name"] = GetNumberName(r["name"].ToString());
            newrow["withdrawMoney"] = r["withdrawMoney"];
            newrow["isauditing"] = GetAuditState(r["isauditing"].ToString());
            newrow["ApplicationExpectNum"] = r["ApplicationExpectNum"].ToString();
            newrow["AuditExpectNum"] = GetAuditExpectNum(r["AuditExpectNum"].ToString());
            newrow["WithdrawTime"] = GetWithdrawTime(r["WithdrawTime"].ToString());
            newrow["AuditingTime"] = GetAuditTime(r["AuditingTime"].ToString());
            newrow["Remark"] = r["Remark"];
            newrow["bankcard"] = GetBankCard(r["bankcard"].ToString());
            newrow["bankbook"] = GetName(r["bankbook"].ToString());
            newrow["bankcode"] = r["bankcode"].ToString();
            dt.Rows.Add(newrow);
        }

        Excel.OutToExcel(dt, GetTran("000599", "会员"),
            new string[] { "Number=" + GetTran("000024", "会员编号"),"name="+GetTran("000025", "会员姓名"),
                "withdrawMoney=" + GetTran("006984", "提现金额"), "isauditing="+GetTran("006983", "审核状态"),
                "IsJL=" + GetTran("000000", "提现币种"),"AuditExpectNum=" + GetTran("000780", "审核期数"),
                "WithdrawTime=" + GetTran("006986", "提现时间"), "AuditingTime=" + GetTran("001155", "审核时间"), 
                "bankcode=" + GetTran("001406", "银行名称"), "bankcard=" + GetTran("000923", "卡号"),
                "bankbook=" + GetTran("000086", "开户名"), "bankcode=" + GetTran("001406", "开户行"),
                "remark="+GetTran("000078", "备注")});

    }

    public void getTotal()
    {
        Label1.Text = GetTran("007825", "提现金额总计") + "：<font color='red'>" + RemittancesBLL.GetTotalMoney("withdrawMoney", "MemberCash", "") + "</font>";
        Label3.Text = GetTran("007826", "已汇出提现金额总计") + "：<font color='red'>" + RemittancesBLL.GetTotalMoney("withdrawMoney", "MemberCash", " where isauditing=2") + "</font>";
        Label4.Text = GetTran("007827", "待审核提现金额总计") + "：<font color='red'>" + RemittancesBLL.GetTotalMoney("withdrawMoney", "MemberCash", " where isauditing=0") + "</font>";
        Label5.Text = GetTran("007828", "开始处理提现金额总计") + "：<font color='red'>" + RemittancesBLL.GetTotalMoney("withdrawMoney", "MemberCash", " where isauditing=1") + "</font>";
    }

    /// <summary>
    /// 批量处理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_listsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int ret = 0;

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                CheckBox chb = (CheckBox)GridView1.Rows[i].FindControl("chb") as CheckBox;

                LinkButton LinkButton1 = (LinkButton)GridView1.Rows[i].FindControl("LinkButton1") as LinkButton;
                LinkButton LinkButton2 = (LinkButton)GridView1.Rows[i].FindControl("LinkButton2") as LinkButton;
                LinkButton LinkButton3 = (LinkButton)GridView1.Rows[i].FindControl("LinkButton3") as LinkButton;
                string[] args = null;
                args = LinkButton1.ToolTip.ToString().Split(',');
                string numebr = args[0].ToString();
                string isAuditing = args[1].ToString();
                int id = Convert.ToInt32(args[2].ToString());
                double money = Convert.ToDouble(args[3].ToString());
                int bz= Convert.ToInt32(args[2].ToString());
                WithdrawModel wDraw = new WithdrawModel();
                wDraw.Id = id;
                wDraw.Number = numebr;
                wDraw.ApplicationExpecdtNum = BLL.CommonClass.CommonDataBLL.getMaxqishu();
                wDraw.WithdrawMoney = money;
                wDraw.AuditExpectNum = BLL.CommonClass.CommonDataBLL.getMaxqishu();
                wDraw.AuditingIP = BLL.CommonClass.CommonDataBLL.OperateIP;
                wDraw.AuditingManageId = BLL.CommonClass.CommonDataBLL.OperateBh;
                wDraw.AuditTime = DateTime.Now.ToUniversalTime();
                wDraw.Wyj = bz;
                //已汇出
                if (rad_list.SelectedValue == "1" && chb.Checked == true && LinkButton1.Visible == true)
                {
                    if (BLL.Registration_declarations.RegistermemberBLL.GetMemberCashAuditState(id) == 2)
                    {
                        ret = -1;
                        Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("007829", "编号为") + id + GetTran("007176", "的该申请单已经审核，不可以重复审核！") + "')</script>");
                        this.BtnConfirm_Click(null, null);
                        break;
                    }

                    if (!BLL.Registration_declarations.RegistermemberBLL.isDelMemberCash(id))
                    {
                        ret = -1;
                        Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("007829", "编号为") + id + GetTran("007177", "该申请单已经被删除！") + "')</script>");
                        this.BtnConfirm_Click(null, null);
                        break;
                    }

                    double leftMoney = Convert.ToDouble(BLL.CommonClass.CommonDataBLL.GetLeftMoney1(numebr));
                    if (money > leftMoney)
                    {
                        ret = -1;
                        Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("007829", "编号为") + id + GetTran("007175", "可提现余额不足，不能审核！") + "')</script>");
                        break;
                    }
                    Application.Lock();
                    bool isSure = false;

                        isSure = BLL.Registration_declarations.RegistermemberBLL.AuditWithdraw(wDraw);

                    Application.UnLock();
                    if (isSure)
                    {
                        
                        ret = 1;
                    }
                    else
                    {
                        ret = -1;
                        Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("007829", "编号为") + id + GetTran("006041", "审核失败！") + "')</script>");
                        break;
                    }

                }//账户出错
                else if (rad_list.SelectedValue == "2" && chb.Checked == true && LinkButton2.Visible == true)
                {

                    if (BLL.Registration_declarations.RegistermemberBLL.GetMemberCashAuditState(id) == 2)
                    {
                        ret = -1;
                        Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("007829", "编号为") + id + GetTran("007181", "该申请单已经审核，不可以转成账号错误！") + "');</script>");
                        break;
                    }
                    if (BLL.Registration_declarations.RegistermemberBLL.GetMemberCashAuditState(id) == 3)
                    {
                        ret = -1;
                        Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("007829", "编号为") + id + GetTran("007180", "该申请单已经是账号错误，不可以重复账号错误！") + "');</script>");
                        break;
                    }

                    if (!BLL.Registration_declarations.RegistermemberBLL.isDelMemberCash(id))
                    {
                        ret = -1;
                        Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("007829", "编号为") + id + GetTran("007179", "该申请单已经删除，不可以转成账号错误！") + "')</script>");
                        this.BtnConfirm_Click(null, null);
                        break;
                    }

                    ChangeLogs cl = new ChangeLogs("MemberCash", "ltrim(rtrim(str(id)))");
                    cl.AddRecord(wDraw.Id);
                    Application.Lock();
                    bool isSure = BLL.Registration_declarations.RegistermemberBLL.updateCardEorror(wDraw.Id, wDraw.WithdrawMoney, wDraw.Number);
                    Application.UnLock();
                    if (isSure)
                    {
                        cl.AddRecord(wDraw.Id);
                        cl.DeletedIntoLogs(ChangeCategory.company14, Session["Company"].ToString(), ENUM_USERTYPE.objecttype5);
                        ret = 1;

                    }
                    else
                    {
                        ret = -1;
                        Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("007829", "编号为") + id + GetTran("001541", "操作失败！)") + "')</script>");
                        break;
                    }

                }//开始处理
                else if (rad_list.SelectedValue == "3" && chb.Checked == true && LinkButton3.Visible == true)
                {
                    if (BLL.Registration_declarations.RegistermemberBLL.GetMemberCashAuditState(id) == 1)
                    {
                        ret = -1;
                        Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("007829", "编号为") + id + GetTran("007182", "该申请单已经审核，不可以在开始处理！") + "');</script>");
                        break;
                    }
                    if (BLL.Registration_declarations.RegistermemberBLL.GetMemberCashAuditState(id) == 2)
                    {
                        ret = -1;
                        Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("007829", "编号为") + id + GetTran("007183", "该申请单已经开始处理，不可以在开始处理！") + "');</script>");
                        break;
                    }
                    if (BLL.Registration_declarations.RegistermemberBLL.GetMemberCashAuditState(id) == 3)
                    {
                        ret = -1;
                        Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("007829", "编号为") + id + GetTran("007184", "该申请单已经是账号错误，不可以在开始处理！") + "');</script>");
                        break;
                    }

                    if (!BLL.Registration_declarations.RegistermemberBLL.isDelMemberCash(id))
                    {
                        ret = -1;
                        Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("007829", "编号为") + id + GetTran("007185", "该申请单已经删除，不可以在开始处理！") + "')</script>");
                        this.BtnConfirm_Click(null, null);
                        break;
                    }

                    ChangeLogs cl = new ChangeLogs("MemberCash", "ltrim(rtrim(str(id)))");
                    cl.AddRecord(wDraw.Id);
                    Application.Lock();
                    bool isSure = BLL.Registration_declarations.RegistermemberBLL.updateKscl(wDraw.Id);
                    Application.UnLock();
                    if (isSure)
                    {
                        cl.AddRecord(wDraw.Id);
                        cl.DeletedIntoLogs(ChangeCategory.company14, Session["Company"].ToString(), ENUM_USERTYPE.objecttype5);
                        ret = 1;
                    }
                    else
                    {
                        ret = -1;
                        Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("007829", "编号为") + id + GetTran("001541", "操作失败！") + "')</script>");
                        break;
                    }
                }
            }

            if (rad_list.SelectedValue == "1" && ret == 0)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("007834", "选项中没有需要已汇出的选项") + "')</script>");
                this.BtnConfirm_Click(null, null);
                return;
            }
            else if (rad_list.SelectedValue == "2" && ret == 0)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("007835", "选项中没有账户错误的选项") + "')</script>");
                this.BtnConfirm_Click(null, null);
                return;
            }
            else if (rad_list.SelectedValue == "3" && ret == 0)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("007836", "选项中没有开始处理的选项") + "')</script>");
                this.BtnConfirm_Click(null, null);
                return;
            }

            if (ret > 0)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("001401", "操作成功！") + "')</script>");
                this.BtnConfirm_Click(null, null);
                return;
            }

        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("001541", "操作失败！") + "')</script>");
            return;
        }
    }
}