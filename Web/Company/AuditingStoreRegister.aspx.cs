using System;
using System.Collections;
using System.Collections.Generic;
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
using System.Data.SqlClient;
using System.Text;
using BLL.other.Company;
using Model.Other;
using DAL;
using BLL.CommonClass;
using Standard.Classes;
using System.IO;

public partial class Company_AuditingStoreRegister : BLL.TranslationBase 
{
    string pagecolumn = "ID,StoreInfo.number,StoreID,StoreInfo.name,StoreName,ExpectNum,Direct,TotalInvestMoney,OperateNum,Direct,OfficeTele,RegisterDate,StoreLevelInt";
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.CustomerAuditingStoreRegister);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        if (!IsPostBack)
        {
            btnSeasch_Click(null, null);
        }
        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.givStoreInfo,
                new string[][]{
                    new string []{"000015","操作"},
                    new string []{"000024","会员编号"},
                    new string []{"000037","店编号"},
                    new string []{"000039","店长姓名"},
                    new string []{"000040","店铺名称"},
                    new string []{"000000","总金额(万元)"},
                    new string []{"000042","办店期数"},
                    new string []{"000043","推荐人编号"},
                    new string []{"000044","办公电话"},
                    new string []{"000031","注册时间"},
                    new string []{"000046","级别"}
                });
        this.TranControls(this.btnAddStore, new string[][] { new string[] { "000049", "添加店铺" } });
    }
    protected void givStoreInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string name = e.CommandName;
        string[] aaa = e.CommandArgument.ToString().Split(',');
        string storeid = aaa[0].ToString();
        string number = aaa[1].ToString();
        if (name == "queren")
        {
            if (storeid.Length > 0)
            {
                //BLL.other.Company.StoreRegisterConfirmBLL bll = new BLL.other.Company.StoreRegisterConfirmBLL();
                Response.Cache.SetExpires(DateTime.Now);
                Permissions.CheckManagePermission(EnumCompanyPermission.CustomerAuditingStoreRegisterAuditing);
                using (SqlConnection conn = new SqlConnection(DAL.DBHelper.connString))
                {
                    conn.Open();
                    SqlTransaction tran = conn.BeginTransaction();
                    if (StoreRegisterConfirmBLL.StoreRegisterRest(storeid))
                    {

                        try
                        {
                            //string number = DBHelper.ExecuteScalar("select number from UnauditedStoreInfo where storeid='" + storeid + "'").ToString();

                            string oldStoreid = DBHelper.ExecuteScalar("select storeid from memberinfo where number='" + number + "'").ToString();
                            DateTime time = DateTime.Now;
                            string RiQi = time.ToString();

                            int isMod = Convert.ToInt32(DBHelper.ExecuteScalar("select count(*) from updatestore where expectnum=" + BLL.CommonClass.CommonDataBLL.getMaxqishu() + " and number='" + number + "'"));

                            string upSql = "update memberinfo set storeid='" + storeid + "' where number='" + number + "'";
                            string InUP_Sql = "";
                            if (isMod == 0)
                            {
                                InUP_Sql = @"insert into updatestore(number,OldStoreID,NewStoreID,expectnum,flag,UpdDate)
								values('" + number + "','" + oldStoreid + "','" + storeid + "'," + BLL.CommonClass.CommonDataBLL.getMaxqishu() + ",1,'" + RiQi + "')";
                            }
                            else
                            {
                                InUP_Sql = "update updatestore set OldStoreID='" + oldStoreid + "',NewStoreID='" + storeid + "',flag=1 where Number='" + number + "' and expectnum=" + BLL.CommonClass.CommonDataBLL.getMaxqishu();
                            }
                            // BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("storeinfo", "(ltrim(rtrim(id)))");
                            // cl_h_info.AddRecord(ID);
                            DBHelper.ExecuteNonQuery(tran, upSql, null, CommandType.Text);
                            DBHelper.ExecuteNonQuery(tran, InUP_Sql, null, CommandType.Text);
                            tran.Commit();
                        }
                        catch
                        {
                            tran.Rollback();
                            throw;
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                    else
                    {
                        BLL.CommonClass.Transforms.JSAlert(GetTran("000055", "确认失败,请重新查看未审核信息!"));
                        // ScriptHelper.SetAlert(givStoreInfo, "确认失败，请重新查看未审核信息！！！");
                    }
                }
                BLL.CommonClass.Transforms.JSAlert(GetTran("000054", "确认成功!"));
                btnSeasch_Click(null, null);

            }
        }
        else
        {
            int n = (int)DBHelper.ExecuteNonQuery("delete StoreInfo where storeid='" + storeid + "'");
            if (n > 0)
            {
                BLL.CommonClass.Transforms.JSAlert(GetTran("000749", "删除成功！"));
                btnSeasch_Click(null, null);
            }
            else
            {
                BLL.CommonClass.Transforms.JSAlert(GetTran("000417", "删除失败！"));
                return;
            }
        }
    }


    protected object GetRDate(object obj)
    {
        if (obj != null)
        {
            try { return Convert.ToDateTime(obj).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()); }
            catch { }
        }
        return "";
    }

    protected void btnSeasch_Click(object sender, EventArgs e)
    {
        //string pagecolumn = "ID,number,StoreID,name,StoreName,ExpectNum,Direct,TotalAccountMoney,OperateNum,Direct,OfficeTele,RegisterDate,StoreLevelStr";
        StringBuilder sb = new StringBuilder();
        sb.Append("1=1 and storestate = 0");
        UserControl_ExpectNum expectnum = Page.FindControl("ExpectNum1") as UserControl_ExpectNum;
        if (expectnum.ExpectNum > 0)
        {

            sb.Append(" and ExpectNum=" + expectnum.ExpectNum);
        }
        if (txtStoreId.Text.Trim().Length > 0)
        {
            sb.Append(" and StoreId='"+txtStoreId.Text.Trim()+"'");
        }
        ViewState["sql"] = sb.ToString();
        string codition = sb.ToString();
        Pager pager = Page.FindControl("Pager1") as Pager;
        ViewState["pagewhere"] = sb.ToString();
        pager.PageBind(0, 10, "StoreInfo ", pagecolumn, codition, "RegisterDate", "givStoreInfo");
        Translations();
    }
    protected void btndownExcel_Click(object sender, EventArgs e)
    {
        this.givStoreInfo.Columns[0].Visible = false;
        pagecolumn = "ID,number,StoreID,name,StoreName,ExpectNum,Direct,TotalAccountMoney,OperateNum,Direct,OfficeTele,RegisterDate,StoreLevelInt";
        DataTable dt = DBHelper.ExecuteDataTable("select s.Number,s.StoreID,s.Name,s.StoreName,s.TotalAccountMoney,s.ExpectNum,s.StoreLevelInt,s.Direct,s.OfficeTele,s.RegisterDate,b.levelstr from StoreInfo s,bsco_level b where s.storelevelint=b.levelint and levelflag=1 and " + ViewState["sql"].ToString() + "order by RegisterDate desc");
        if (dt.Rows.Count > 0)
        {
            givStoreInfo.DataSource = dt;
            givStoreInfo.DataBind();
        }
        else {
            ScriptHelper.SetAlert(Page, "没有数据，不能导出");
            return;
        }

        Response.ClearContent();
        Response.Charset = "GB2312";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.AddHeader("content-disposition", "attachment; filename=registerStore.xls");
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        givStoreInfo.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        
    }
    protected void btnAddStore_Click(object sender, EventArgs e)
    {
        Response.Redirect("RegisterStore.aspx");
    }
    protected void givStoreInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int curr = Convert.ToInt32(DBHelper.ExecuteScalar("select rate from Currency where id=standardMoney"));
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
            //Label lab = (Label)e.Row.FindControl("StoreLevelInt");

            //string l = DBHelper.ExecuteScalar("select levelstr from bsco_level where levelflag=1 and levelint = " + Convert.ToInt32(lab.Text.ToString()) + "").ToString();
            //lab.Text = l.ToString();
            e.Row.Cells[3].Text = Encryption.Encryption.GetDecipherName(e.Row.Cells[3].Text.ToString());
            e.Row.Cells[4].Text = Encryption.Encryption.GetDecipherName(e.Row.Cells[4].Text.ToString());
            e.Row.Cells[8].Text = Encryption.Encryption.GetDecipherName(e.Row.Cells[8].Text.ToString());
        }
        Translations();
    }
    protected void lkSubmit_Click(object sender, EventArgs e)
    {
        btnSeasch_Click(null, null);
    }

    protected string getStoLevelStr(string stoid)
    {
        //return CommonDataBLL.GetStoreLevelStr(stoid);
        object o_sl = DAL.DBHelper.ExecuteScalar(@"SELECT b.levelstr FROM storeInfo s inner join bsco_level b on s.StoreLevelInt=b.levelint WHERE StoreID=@sid and b.levelflag=1", new SqlParameter[] {
            new SqlParameter("@sid", stoid)
        }, CommandType.Text);
        if (o_sl != null)
            return o_sl.ToString();
        else
            return "";
    }
}
