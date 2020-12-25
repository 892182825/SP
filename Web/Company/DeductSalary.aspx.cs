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

using Model.Other;
using BLL.MoneyFlows;
using System.Text;
using Encryption;
using Model;
using BLL.CommonClass;
using System.Data.SqlClient;
using DAL;

public partial class Company_DeductSalary : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.FinanceKoukuanMingxi);

        Response.Cache.SetExpires(DateTime.Now);
        if (!IsPostBack)
        {
            lkSubmit_Click(null, null);
        }
        Translations_More();
    }

    protected void Translations_More()
    {


        TranControls(gvdeduct, new string[][]
                        {
                            new string[] { "000015","操作"},
                            new string[] { "001195","编号"},
                            new string[] { "000107","姓名"},
                            new string[] { "000","扣补款火星币"},
                            new string[] { "000045","期数"},
                            new string[] { "001838","录入日期"},
                            new string[] { "001835","操作编号"},
                            new string[] { "006983","审核状态"},
                            new string[]{"000078","备注"},

                        }
            );
        TranControls(DropDownList1, new string[][]
                        {
                            new string[] { "000633","全部"},
                            new string[] { "251","扣款"},
                            new string[] { "000252","补款"}
                        }
          );
        TranControls(DropDownList2, new string[][]
                        {
                            new string[] { "001871","条件不限"},
                            new string[] { "001195","编号"},
                            new string[] { "000107","姓名"},
                        }
         );
        TranControls(ddl_audit, new string[][] { new string[] { "000633", "全部" }, new string[] { "001009", "未审核" }, new string[] { "000761", "审核" } });
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnConfirm_Click(object sender, EventArgs e)
    {
        int expct = this.DropDownQiShu.ExpectNum;
        int deduct = Convert.ToInt32(this.DropDownList1.SelectedValue);
        string search = string.Empty;
        string mark = string.Empty;
        if (TxtSearch.Text != "" && DropDownList2.SelectedIndex != 0)
        {
            mark = DropDownList2.SelectedValue;
            search = TxtSearch.Text;
        }

        string sql = "  Deduct.Number=MemberInfo.Number and IsDeduct in(0,1) ";
        if (expct != -1)
        {
            sql = sql + " and Deduct.ExpectNum=" + expct;
        }
        if (deduct != -1)
        {
            sql = sql + " and IsDeduct =" + deduct;
        }
        if (ddl_audit.SelectedValue != "-1")
        {
            sql = sql + " and isaudit=" + ddl_audit.SelectedValue;
        }
        if (mark != string.Empty && search != string.Empty && mark == "MemberInfo.Name")
        {
            sql = sql + " and " + mark + " like '%" + Encryption.Encryption.GetEncryptionName(search.Trim()) + "%' ";
        }
        else if (mark != string.Empty && search != string.Empty && mark == "MemberInfo.Number")
        {
            sql = sql + " and " + mark + " like '%" + search + "%' ";
        }
        ViewState["sql"] = "select isAudit,MemberInfo.Number,MemberInfo.[Name],case when Deduct.IsDeduct = 0 then '" + GetTran("000251", "扣款") + "' else '" + GetTran("000252", "补款") + "' end as typeE,Deduct.DeductMoney, Deduct.ExpectNum,KeyinDate,Deduct.OperateNum,Deduct.DeductReason from MemberInfo,Deduct where" + sql + " order by Deduct.id desc";
        Pager pager = Page.FindControl("Pager1") as Pager;
        pager.PageBind(0, 10, "MemberInfo,Deduct", "isAudit,MemberInfo.Number,MemberInfo.Name,Deduct.ID,Deduct.DeductMoney,Deduct.DeductReason,Deduct.ExpectNum,case when Deduct.IsDeduct = 0 then '" + GetTran("000251", "扣款") + "' else '" + GetTran("000252", "补款") + "' end as IsDeduct,KeyinDate,Deduct.OperateNum",
            sql, "Deduct.id", "gvdeduct");
        Translations_More();
    }

    //获取格林时间
    public string Getdate(object Vdate)
    {
        DateTime Dtime = Convert.ToDateTime(Vdate);
        return Dtime.AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToShortDateString();
    }

    /// <summary>
    /// 解密姓名
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public string getname(object name)
    {
        return Encryption.Encryption.GetDecipherName(name.ToString());
    }

    protected string GetModify(string dd, string id)//, string _pageUrl
    {

        if (dd.Length > 0)
        {
            string sql = dd.ToString() + "&qishu=" + id.ToString();
            return sql;

        }
        else
        {
            return GetTran("000221", "无");
        }
    }

    /// <summary>
    /// 绑定备注
    /// </summary>
    /// <param name="node">备注</param>
    /// <param name="id"></param>
    /// <returns></returns>
    public string GetNote(object expectnum, object number, object node, object id)
    {
        string str = "";
        if (node.ToString().Trim().Length > 0)
        {
            str = "<a href =\"javascript:void(window.open('ECNote.aspx?expectnum=" + expectnum.ToString() + "&number=" + number.ToString() + "&id=" + id.ToString() + "','','width=350,height=160'))\">" + GetTran("000440", "查看") + "</a>";
        }
        else
        {
            str = GetTran("000221", "无");
        }
        return str;
    }
    /// <summary>
    /// 导出EXCEL
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.AppendHeader("Content-Disposition", "attachment;filename=FileName.xls");
        Response.ContentEncoding = Encoding.UTF8;

        string cmd = ViewState["sql"].ToString();
        DataTable dt = DAL.DBHelper.ExecuteDataTable(cmd);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dt.Rows[i]["Name"] = Encryption.Encryption.GetDecipherName(dt.Rows[i]["Name"].ToString());
            dt.Rows[i]["KeyinDate"] = string.IsNullOrEmpty(dt.Rows[i]["KeyinDate"].ToString()) ? dt.Rows[i]["KeyinDate"] : Convert.ToDateTime(dt.Rows[i]["KeyinDate"].ToString()).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString();
        }
        Excel.OutToExcel(dt, GetTran("002000", "扣补款"), new string[] { "Number=" + GetTran("001195", "编号"), "Name=" + GetTran("000107", "姓名"), "typeE=" + GetTran("002004", "扣补款类型"), "DeductMoney=" + GetTran("002005", "扣补金额"), "ExpectNum=" + GetTran("000045", "期数"), "KeyinDate=" + GetTran("002009", "录入时间"), "OperateNum=" + GetTran("001835", "操作编号"), "DeductReason=" + GetTran("000078", "备注") });
    }
    protected void gvdeduct_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
            HiddenField hf = e.Row.FindControl("HiddenField1") as HiddenField;
            if (hf.Value == "1")
            {
                e.Row.Cells[0].Text = "&nbsp;";
            }
        }
        Translations_More();
    }
    protected void lkSubmit_Click(object sender, EventArgs e)
    {
        BtnConfirm_Click(null, null);
        Translations_More();
    }
    protected void gvdeduct_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();
        string sql = "select * from deduct where id=" + id;
        DataTable dt = DAL.DBHelper.ExecuteDataTable(sql);
        DeductModel model = new DeductModel();
        if (dt.Rows.Count > 0)
        {
            model.Auditing = 1;
            model.DeductMoney = Convert.ToDouble(dt.Rows[0]["DeductMoney"]);
            model.ExpectNum = CommonDataBLL.getMaxqishu();
            model.ID = Convert.ToInt32(id);
            model.IsDeduct = Convert.ToInt32(dt.Rows[0]["IsDeduct"]);
            model.Number = dt.Rows[0]["Number"].ToString();
            model.OperateIP = Request.UserHostAddress;
            model.OperateNum = Session["Company"].ToString();
            model.AuditingTime = DateTime.Now.ToUniversalTime();
            model.Auditingexctnum = CommonDataBLL.getMaxqishu();
        }
        if (e.CommandName == "ok")
        {
            if (dt.Rows[0]["isaudit"].ToString() == "1")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script >alert('" + GetTran("000849", "不能重复审核") + "')</script>");
                return;
            }
            if ( GetAudit(model))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script >alert('" + GetTran("000858", "审核成功") + "')</script>");
                BtnConfirm_Click(null, null);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script >alert('" + GetTran("006041", "审核失败") + "！')</script>");
            }
        }
        if (e.CommandName == "del")
        {
            if (DAL.DBHelper.ExecuteNonQuery("delete from deduct where id=" + id, CommandType.Text) > 0)
            {
                ScriptHelper.SetAlert(Page, GetTran("000008", "删除成功"));
                BtnConfirm_Click(null, null);
            }
            else {
                ScriptHelper.SetAlert(Page, GetTran("000009", "删除失败"));
            }
        }
    }
    private bool GetAudit(DeductModel info) {
        bool bl = false;
        using (SqlConnection conn = new SqlConnection(DBHelper.connString))
        {
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            try
            {
                if (DeductDAL.UpdateDeduct(info, tran) <= 0)
                {
                    tran.Rollback();
                    return false;
                }
                string p = "pointEin =pointEin +" + info.DeductMoney;
                if(info.IsDeduct==0) p = "pointEout =pointEout +" + info.DeductMoney;

             int r=    DBHelper.ExecuteNonQuery(tran,"update  memberinfo  set  "+p+"  where  number='"+info.Number+"'");
                if (r == 0) { tran.Rollback(); return false; }
                //对账单
                string rem = "扣款";
                D_AccountKmtype dakm = D_AccountKmtype.AddMoneycut;
                DirectionEnum dem = DirectionEnum.AccountReduced;
                if (info.IsDeduct == 0) { dakm = D_AccountKmtype.AddMoneycut; }
                else if (info.IsDeduct == 1) {dakm = D_AccountKmtype.AddMoneyget; dem= DirectionEnum.AccountsIncreased; rem = "补款"; }
                 
                int c = D_AccountDAL.AddAccount("E", info.Number, info.DeductMoney, D_AccountSftype.MemberType, dakm, dem, "管理员添加"+ rem+" "+info.DeductMoney, tran);
                if (c == 0) {
                    tran.Rollback();
                    return  false;
                } 
                tran.Commit();
                return true;
            }
            catch
            {
                tran.Rollback();
                return false;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }


        return bl;
    }
}