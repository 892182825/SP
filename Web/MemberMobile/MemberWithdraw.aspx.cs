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
using BLL.MoneyFlows;
using System.Collections.Generic;
using Model.Other;
using BLL.CommonClass;
using BLL.Logistics;
using System.Data.SqlClient;
using BLL.Registration_declarations;


public partial class Member_MemberWithdraw : BLL.TranslationBase
{
   public int bzCurrency = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        //获取标准币种
        bzCurrency = CommonDataBLL.GetStandard();
        if (!IsPostBack)
        {
            if (Session["Default_Currency"] == null) Session["Default_Currency"] = bzCurrency;

            this.beginTime.Text = CommonDataBLL.GetDateBegin().Trim();
            this.endTime.Text = CommonDataBLL.GetDateEnd().Trim();
            bind();
        }
        Translations();
    }


    private void Translations()
    {
        //this.TranControls(this.GridView1,
        //           new string[][]{
        //           new string[]{"000022","删除"},
        //           new string []{"000259","修改"},
        //           new string []{"006983","审核状态"},
        //           new string []{"006984","提现金额"},
        //           new string []{ "008150", "提现手续费 "},
        //           new string []{"007248","申请时间"},
        //           new string []{"000780","审核期数"},
        //           new string []{"006986","提现时间"},
        //           new string []{"001155","审核时间"},
        //           new string []{"000923","卡号"}
        //    });
        this.TranControls(this.Button2, new string[][] { new string[] { "000048", "查 询" } });
    }

    protected string GetAuditState(string auditState)
    {
        string msg = "";
        switch (auditState)
        {
            case "0":
                msg = "<span style='color:red; background: #fff;'>" + GetTran("007254", "处理中") + "</span>";
                break;
            case "1":
                msg = "<font color='red'>" + GetTran("007254", "处理中") + "</font>";
                break;
            case "2":
                msg = "<font color='red'>" + GetTran("007169", "已汇出") + "</font>";
                break;
            case "3":
                msg = "<font color='red'>" + GetTran("000000", "已退回") + "</font>";
                break;
            default:
                msg = "<font color='red'>" + GetTran("007168", "待审核") + "</font>";
                break;
        }
        return msg;

        //string msg = "";
        //switch (auditState)
        //{
        //    case "0":
        //        msg = "<span style='color:red; background: #fff;'>" + GetTran("009097", "可匹配") + "</span>";
        //        break;
        //    case "1":
        //        msg = "<font color='red'>" + GetTran("009098", "匹配成功") + "</font>";
        //        break;
        //    case "2":
        //        msg = "<font color='red'>" + GetTran("009099", "超时待查收") + "</font>";
        //        break;
        //    case "3":
        //        msg = "<font color='red'>" + GetTran("009100", "待汇入") + "</font>";
        //        break;
        //    default:
        //        msg = "<font color='red'>" + GetTran("007168", "待审核") + "</font>";
        //        break;
        //}
        //return msg;

    }


    protected string GetWState(string hkid, string shenhestate, string auditState)
    {
        string msg = "";
        if (hkid == "0")
        {
            switch (auditState)
            {
                case "0":
                    msg = "<span style='color:red; background: #fff;'>" + GetTran("007254", "处理中") + "</span>";
                    break;
                case "1":
                    msg = "<font color='red'>" + GetTran("007254", "处理中") + "</font>";
                    break;
                case "2":
                    msg = "<font color='red'>" + GetTran("007169", "已汇出") + "</font>";
                    break;
                case "3":
                    msg = "<font color='red'>" + GetTran("000000", "已退回") + "</font>";
                    break;
                default:
                    msg = "<font color='red'>" + GetTran("007168", "待审核") + "</font>";
                    break;
            }
        }
        else
        {
            switch (shenhestate)
            {
                /*
                    0 初始状态，可以匹配单
                    1 匹配成功
                    3 待汇出
                    2 超时待查收
                    11 等待确认

                    99 原始单，不能匹配【作废单】
                    98 二次撤销单，不能匹配【作废单】

                    -1 撤销单，不能匹配
                    20 已到账

                    30 提现超时，公司打款
                    31 提现公司打款，待确认
                 */
                case "1":
                    msg = "<span style='color:red; background: #fff;'>" + GetTran("009098", "匹配成功") + "</span>";
                    break;
                case "3":
                    msg = "<font color='red'>" + GetTran("8138  ", "待汇出") + "</font>";
                    break;
                case "2":
                    msg = "<font color='red'>" + GetTran("009099", "超时待查收") + "</font>";
                    break;
                case "11":
                    msg = "<font color='red'>" + GetTran("009087", "等待确认") + "</font>";
                    break;
                case "20":
                    msg = "<font color='red'>" + GetTran("007371", "已到账") + "</font>";
                    break;
                default:
                    msg = "<font color='red'>" + GetTran("007254", "处理中") + "</font>";
                    break;
            }
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
            return DateTime.Parse(AuditTime).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToShortDateString();
        }
    }

    protected string GetWithdrawTime(string WithdrawTime)
    {
        return DateTime.Parse(WithdrawTime).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToShortDateString();
    }

    protected string GetWithdrawMoney(string WithdrawMoney)
    {
        string money = (double.Parse(WithdrawMoney) ).ToString("f2");
       return money;
    }

    public void bind()
    {
        System.Text.StringBuilder condition = new System.Text.StringBuilder();
        condition.Append("  1=1  ");
        string begin = "";
        string end = "";
        if (this.beginTime.Text != "")
        {
            DateTime time = DateTime.Now.ToUniversalTime();
            bool b = DateTime.TryParse(beginTime.Text.Trim(), out time);
            if (!b)
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("000827", "时间格式不正确！") + "');", true);
                return;
            }
            else
            {
                begin = time.ToUniversalTime().ToString();
            }
        }
        if (this.endTime.Text != "")
        {
            DateTime time = DateTime.Now.ToUniversalTime();
            bool b = DateTime.TryParse(endTime.Text.Trim(), out time);
            if (!b)
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("000827", "时间格式不正确！") + "');", true);
                return;
            }
            else
            {
                end = time.AddDays(1).ToUniversalTime().ToString();
            }
        }
        if (begin != "" && end != "") {
            condition.Append(" and WithdrawTime between '" + begin + "' and '" + end + "'");
        }
        condition.Append(" and Number='" + Session["Member"].ToString() + "' and shenHestate not in(-1,99,98) and id>txid_ys");

        string sql = "select * from Withdraw where 1=1 and  " + condition.ToString() + " order by id desc ";
        this.ucPagerMb1.PageSize = 10;
        this.ucPagerMb1.PageInit(sql, "rep_TransferList");
        //this.Pager1.ControlName = "rep_TransferList";
        //this.Pager1.key = "id";
        //this.Pager1.PageColumn = " * ";
        //this.Pager1.Pageindex = 0;
        //this.Pager1.PageTable = " Withdraw ";
        //this.Pager1.Condition = condition.ToString();
        //this.Pager1.PageSize = 10;
        //this.Pager1.PageCount = 0;
        //this.Pager1.PageBind();
        Translations();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
            Label lb = e.Row.FindControl("Label4") as Label;
            if (lb.Text != "<span style='color:red;'>" + GetTran("007254", "处理中") + "</span>")
            {
                ((Image)e.Row.FindControl("imgDete")).ImageUrl = "images/view-button3-.png";
                ((Image)e.Row.FindControl("imgDete")).Enabled = false;
                ((Image)e.Row.FindControl("LinkButton1")).ImageUrl = "images/view-button2-.png";
                ((Image)e.Row.FindControl("LinkButton1")).Enabled = false;
            }
            else
            {
                ((Image)e.Row.FindControl("imgDete")).ImageUrl = "images/view-button3.png";
                ((Image)e.Row.FindControl("imgDete")).Enabled = true;
                ((Image)e.Row.FindControl("LinkButton1")).ImageUrl = "images/view-button2.png";
                ((Image)e.Row.FindControl("LinkButton1")).Enabled = true;
            }
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        bind();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();
        
        if (e.CommandName.ToString() == "Detail")
        {
            Response.Redirect("MemberCash.aspx?id=" + id);
        }
        else if (e.CommandName.ToString() == "Del")
        {
            DataTable dt = RegistermemberBLL.QueryWithdraw(id);
            if (dt.Rows.Count > 0)
            {
                if (RegistermemberBLL.GetAuditState(int.Parse(id)) == 1)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("007174", "该申请单已经审核，不可以删除！") + ")');</script>");
                    bind();
                    return;
                }
                if (RegistermemberBLL.GetAuditState(int.Parse(id)) == 2)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("007986", "该申请单已经开始处理，不可以删除！") + "');</script>");
                    bind();
                    return;
                }
                if (RegistermemberBLL.GetAuditState(int.Parse(id)) == 3)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("007987", "该申请单已经是账号错误，不可以删除！") + "');</script>");
                    bind();
                    return;
                }

                using (SqlConnection conn = new SqlConnection(DAL.DBHelper.connString))
                {
                    conn.Open();
                    SqlTransaction tran = conn.BeginTransaction();
                    try
                    {
                        if (!DAL.ECTransferDetailDAL.DeleteWithdraw(tran, int.Parse(id), Convert.ToDouble(dt.Rows[0]["WithdrawMoney"].ToString()), dt.Rows[0]["number"].ToString()))
                        {
                            tran.Rollback();
                            ScriptHelper.SetAlert(Page, GetTran("000417", "删除失败！"));
                            bind();
                            return;
                        }
                        else
                        {
                            tran.Commit();
                            ScriptHelper.SetAlert(Page, GetTran("000749", "删除成功！"));
                            bind();
                        }
                    }
                    catch {
                        tran.Rollback();
                        ScriptHelper.SetAlert(Page, GetTran("000417", "删除失败！"));
                        bind();
                    }
                }
            }
            else {
                ScriptHelper.SetAlert(Page, GetTran("000861", "不能重复删除！"));
            }
        }
        
    }
}
