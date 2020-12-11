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
using System.Text;
using Model.Other;
using DAL;

public partial class Company_LeveLQuery : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        ViewState["LevelType"] = Request.QueryString["type"] == "" ? "" : Request.QueryString["type"];
        if (!this.IsPostBack)
        {
            if (ViewState["LevelType"].ToString() != "" && ViewState["LevelType"].ToString() == "Member")
            {
                Permissions.CheckManagePermission(EnumCompanyPermission.BalanceMemberLevelModify);
                givTWmember.Visible = true;
                gv1.Visible = false;
                lit_beizhu.Text = GetTran("006841", "人工修改会员的级别。");
                lit_number.Text = GetTran("000024", "会员编号") + "：";
            }
            else if (ViewState["LevelType"].ToString() != "" && ViewState["LevelType"].ToString() == "Store")
            {
                Permissions.CheckManagePermission(EnumCompanyPermission.StoreLevelQuery);
                givTWmember.Visible = false;
                gv1.Visible = true;
                lit_beizhu.Text = GetTran("006847", "人工修改店铺的级别。");
                lit_number.Text = GetTran("000150", "店铺编号") + "：";
            }
            btnSeach_Click(null, null);

            Translations();
        }
    }

    private void Translations()
    {
        this.TranControls(this.givTWmember,
                new string[][]{
                    new string []{"000024","会员编号"},
                    new string []{"000025","会员姓名"},
                    new string []{"007149","原级别"},
                    new string []{"000771","调整级别"},
                    new string []{"000045","期数"},
                    new string []{"000613","日期"},
                    new string []{"004134","操作人编号"},
                    new string []{"000078","备注"}
                });
        this.TranControls(this.gv1,
               new string[][]{
                    new string []{"000150","店铺编号"},
                    new string []{"007215","调整前级别"},
                    new string []{"000771","调整级别"},
                    new string []{"000045","期数"},
                    new string []{"000613","日期"},
                    new string []{"007213","定级原因"}
                });

    }

    protected void btnSeach_Click(object sender, EventArgs e)
    {
        if (ViewState["LevelType"].ToString() != "" && ViewState["LevelType"].ToString() == "Member")
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("1=1  and Grading.levelnum=bsco_level.levelint and bsco_level.levelflag=0 and Grading.GradingStatus=1");

            string mb = DisposeString.DisString(txt_member.Text);
            if (mb.Length > 0)
            {
                sb.Append(" and Number like '%" + this.txt_member.Text.Trim() + "%'");

            }
            if (this.DatePicker2.Text != "")
            {
                sb.Append(" and year(inputdate)=year('" + Convert.ToDateTime(this.DatePicker2.Text).ToUniversalTime() + "') and month(inputdate)=month('" + Convert.ToDateTime(this.DatePicker2.Text).ToUniversalTime() + "') and day(inputdate)=day('" + Convert.ToDateTime(this.DatePicker2.Text).AddDays(1).ToUniversalTime() + "') ");
            }
            UserControl_ExpectNum exp = Page.FindControl("ExpectNum1") as UserControl_ExpectNum;
            int ExpectNum = exp.ExpectNum;
            if (ExpectNum > 0)
            {
                sb.Append(" and ExpectNum=" + ExpectNum);
            }

            ViewState["SQLSTR"] = "select remark,operaternum,Number,Grading.ExpectNum,inputdate,bsco_level.levelstr as lv,isnull((select top 1 isnull(levelstr,'') from bsco_level where levelint=Grading.oldLN),'无') as oldlv,(select top 1 [name] from memberinfo where number=Grading.number) as name1 from Grading,bsco_level where " + sb.ToString();

            Pager pager = Page.FindControl("Pager1") as Pager;
            pager.PageBind(0, 10, "Grading,bsco_level", "remark,operaternum,Grading.Number,Grading.ExpectNum,inputdate,bsco_level.levelstr as lv,isnull((select top 1 isnull(levelstr,'') from bsco_level where levelflag=0 and levelint=Grading.oldLN),'无') as oldlv,(select top 1 [name] from memberinfo where number=Grading.number) as name1", sb.ToString(), "Grading.inputdate", "givTWmember");
            givTWmember.Visible = true;
            gv1.Visible = false;

        }
        else if (ViewState["LevelType"].ToString() != "" && ViewState["LevelType"].ToString() == "Store")
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("1=1 and Grading.levelnum=bsco_level.levelint and bsco_level.levelflag=1 and Grading.GradingStatus=0");

            string mb = txt_member.Text.ToString();
            if (mb.Length > 0)
            {
                sb.Append(" and Number like '%" + this.txt_member.Text.Trim() + "%'");

            }
            if (this.DatePicker2.Text != "")
            {
                sb.Append(" and year(inputdate)=year('" + this.DatePicker2.Text + "') and month(inputdate)=month('" + this.DatePicker2.Text + "') and day(inputdate)=day('" + this.DatePicker2.Text + "') ");
            }
            UserControl_ExpectNum exp = Page.FindControl("ExpectNum1") as UserControl_ExpectNum;
            int ExpectNum = exp.ExpectNum;
            if (ExpectNum > 0)
            {
                sb.Append(" and ExpectNum=" + ExpectNum);
            }

            ViewState["SQLSTR"] = "select remark,operaternum,Number,Grading.ExpectNum,inputdate,bsco_level.levelstr as lv,isnull((select top 1 isnull(levelstr,'') from bsco_level where levelflag=1 and levelint=Grading.oldLN),'无') as oldlv,(select top 1 [name] from storeinfo where storeid=Grading.number) as name1 from Grading,bsco_level where " + sb.ToString();

            Pager pager = Page.FindControl("Pager1") as Pager;
            pager.PageBind(0, 10, "Grading,bsco_level", "remark,operaternum,Number,Grading.ExpectNum,inputdate,bsco_level.levelstr as lv,isnull((select top 1 isnull(levelstr,'') from bsco_level where levelflag=1 and levelint=Grading.oldLN),'无') as oldlv,(select top 1 [name] from storeinfo where storeid=Grading.number) as name1", sb.ToString(), "Grading.inputdate", "gv1");
            givTWmember.Visible = false;
            gv1.Visible = true;
        }

        Translations();
    }


    protected void givTWmember_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //解密姓名  
            e.Row.Cells[1].Text = Encryption.Encryption.GetDecipherName(e.Row.Cells[1].Text);
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
        }
    }
    protected void btnDownExcel_Click(object sender, EventArgs e)
    {
        string cmd = ViewState["SQLSTR"].ToString();
        DataTable dt = DBHelper.ExecuteDataTable(cmd);
        if (dt == null || dt.Rows.Count < 1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("000672", "没有数据，不能导出Excel") + "！')</script>");
            return;
        }

        DateTime dtime1 = PageBase.GetUtcNowTime();
        foreach (DataRow row in dt.Rows)
        {
            DateTime.TryParse(PageBase.GetDTbyStr(row["inputdate"].ToString()).ToString(), out dtime1);
            row["inputdate"] = dtime1;
            row["name1"] = Encryption.Encryption.GetDecipherName(row["name1"].ToString());
        }
        if (ViewState["LevelType"].ToString() != "" && ViewState["LevelType"].ToString() == "Member")
        {
            Excel.OutToExcel(dt, GetTran("000764", "会员定级"), new string[] { "Number=" + GetTran("000024", "会员编号") + "", "name1=" + GetTran("000025", "会员姓名") + "", "oldlv=" + GetTran("007149", "原级别") + "", "lv=" + GetTran("000771", "调整级别") + "", "ExpectNum=" + GetTran("000045", "期数") + "", "inputdate=" + GetTran("000613", "日期") + "" });
        }
        else if (ViewState["LevelType"].ToString() != "" && ViewState["LevelType"].ToString() == "Store")
        {
            Excel.OutToExcel(dt, GetTran("001157", "服务机构定级"), new string[] { "Number=" + GetTran("000150", "店铺编号") + "", "oldlv=" + GetTran("007215", "调整前级别") + "", "lv=" + GetTran("000771", "调整级别") + "", "ExpectNum=" + GetTran("000045", "期数") + "", "inputdate=" + GetTran("000613", "日期") + "" });
        }
    }
    protected void lkSubmit_Click(object sender, EventArgs e)
    {
        btnSeach_Click(null, null);
    }

    protected void gv1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Translations();
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
        }
    }
}