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
using DAL;
using System.Text;

public partial class Company_QueryOnline : BLL.TranslationBase
{
    static int sphours = 0;  //时区差
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(6308); ///检查相应权限

        if (!IsPostBack)
        {
            Binddatalist();
        }
        loadtimelist();

        //检测当前使用国家 时间与格林威治时间的时区差 并显示准确的注册时间
        sphours = Convert.ToInt32(Session["WTH"]);
        translation();
    }

    private void translation() {
        TranControls(btnsearch, new string[][] { new string[] { "000340", "查询" } });
        TranControls(btnsureget, new string[][] { new string[] { "007808", "确认收款" } });
        TranControls(gvcdRemttlist, new string[][] { 
            new string[] { "007799", "确认收款" },
            new string[]{"001970","收款金额"},
            new string[]{"007805","汇款单编号"},
            new string[]{"000777","汇款人姓名"},
            new string[]{"007806","汇款单时间"},
            new string[]{"001892","汇款人编号"},
            new string[]{"001044","汇款用途"}
        });
    }

    public void loadtimelist()
    {
        ListItem li = null;
        this.ddlhours.Items.Clear();
        this.ddlmins.Items.Clear();
        this.ddlsecs.Items.Clear();
        for (int i = 0; i < 60; i++)
        {
            li = new ListItem(i.ToString(), i.ToString());
            ddlmins.Items.Add(li);
        }
        for (int i = 0; i < 60; i++)
        {
            li = new ListItem(i.ToString(), i.ToString());
            ddlsecs.Items.Add(li);
        }
        for (int i = 0; i < 24; i++)
        {
            li = new ListItem(i.ToString(), i.ToString());
            ddlhours.Items.Add(li);
        }
    }
    public void Binddatalist()
    {
        int mtype = Convert.ToInt32(this.hidtype.Value);

        string table = "  remittances r ";
        string conndition = "   r.payway=1 and isgsqr =0";
        string key = "r.id";
        string column = " r.remitnumber,r.remittancesdate,r.receivablesdate, r.remittancesid,  r.remitnumber as number  ,r.[use], case r.RemitStatus    when  1 then (select name from memberinfo mb  where mb.number=r.remitnumber )  when  0 then (select name from storeinfo st where st.storeid=r.remitnumber ) end as name  ,r.remitmoney ,r.payway  "; 

        if (this.txtnumber.Text.Trim() != "")
        {
            conndition += " and  f.name like '%" + this.txtnumber.Text.Trim() + "%'";
        }
        if (this.txtbstd.Text.Trim() != "")
        {
            conndition += " and  r.remittancesdate > = '" + this.txtbstd.Text.Trim() + "'";
        }
        if (this.txtendd.Text.Trim() != "")
        {
            conndition += " and  r.remittancesdate < = '" + this.txtendd.Text.Trim() + "'";
        }
        if (this.txtRemittancesid.Text.Trim() != "")
        {
            conndition += " and  r.remittancesid > = '" + this.txtRemittancesid.Text.Trim() + "'";
        }

        ViewState["SQLSTR"] = "select " + column + " from " + table + " where" + conndition.ToString()+" order by r.id desc";

        this.Pager1.ControlName = "gvcdRemttlist";
        this.Pager1.key = key;
        this.Pager1.PageColumn = column;
        this.Pager1.Pageindex = 0;
        this.Pager1.PageTable = table;
        this.Pager1.Condition = conndition.ToString();
        this.Pager1.PageSize = 10;
        this.Pager1.PageCount = 0;
        this.Pager1.PageBind();

    }
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        //this.hidtype.Value = this.rdolistrmtype.SelectedValue;
        Binddatalist();
        this.hidremid.Value = "";
        this.lblmoney.Text = GetTran("007809", "未选择");
        this.lblname.Text = GetTran("007809", "未选择");
        this.lblnumber.Text = GetTran("007809", "未选择");
        this.lblremid.Text = GetTran("007809", "未选择");
        this.lblremittancesdate.Text = GetTran("007809", "未选择");
        this.txtgettime.Text = "";
        this.ddlhours.SelectedIndex = 0;
        this.ddlmins.SelectedIndex = 0;
        this.ddlsecs.SelectedIndex = 0;
    }
    protected void btnsureget_Click(object sender, EventArgs e)
    {
        int mtype = Convert.ToInt32(this.hidtype.Value);
        if (this.hidremid.Value == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("007813", "请选择待支付的汇款单") + "！');</script>"); return;
        }
        if (this.txtgettime.Text == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("007814", "请填写收款时间") + "！');</script>"); return;
        }

        DateTime time = Convert.ToDateTime(this.txtgettime.Text + " " + this.ddlhours.SelectedValue + ":" + this.ddlmins.SelectedValue + ":" + this.ddlsecs.SelectedValue);
        int res = -1;
        string opip = Request.UserHostAddress.ToString();
        string opter = Session["company"].ToString();
        string rmid = this.hidremid.Value;

        DataTable remittancedt = RemittancesDAL.GetRemittanceinfobyremid(rmid);
   
        if (remittancedt != null && remittancedt.Rows.Count > 0)
        {
            int roltype  = Convert.ToInt32(remittancedt.Rows[0]["RemitStatus"])==0?2:1;
            string ord = remittancedt.Rows[0]["RelationOrderID"].ToString();
            double totalrmbmoney = Convert.ToDouble(lblmoney.Text.Trim());
            string  remitnumber = remittancedt.Rows[0]["remitnumber"].ToString();
            int isgsqr = Convert.ToInt32(remittancedt.Rows[0]["isgsqr"]);
            int dotype = 0;
            if (ord == "") dotype = 2; else dotype = 1;
            if (isgsqr == 0)
                res = AddOrderDataDAL.OrderPayment(remitnumber, ord, opip, roltype, dotype, 0, opter, "", 4, 1, 1, 1, rmid, totalrmbmoney, "");  //       
            else
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("007815", "该汇款单已确认，不能重复操作") + "！');window.close();</script>");
        }
         
       
        if (res == 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("007816", "确认收款成功") + "！');</script>");
            RemittancesDAL.UpdateRemittancereceivabledate(this.hidremid.Value, time);//更新收款时间
            this.hidremid.Value = "";
            this.lblmoney.Text = GetTran("007809", "未选择");
            this.lblname.Text = GetTran("007809", "未选择");
            this.lblnumber.Text = GetTran("007809", "未选择");
            this.lblremid.Text = GetTran("007809", "未选择");
            this.lblremittancesdate.Text = GetTran("007809", "未选择");
            this.txtgettime.Text = "";
            this.ddlhours.SelectedIndex = 0;
            this.ddlmins.SelectedIndex = 0;
            this.ddlsecs.SelectedIndex = 0;
            Binddatalist();
            return;
        }
        else { ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("007817", "确认收款失败") + "！');</script>"); return; }




    }
    /// <summary>
    /// 行命令事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvcdRemttlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string rmtid = e.CommandArgument.ToString();
        if (e.CommandName == "GT")
        {
            
            DataTable dtrm = DAL.RemittancesDAL.GetRemttancesbyrmidandtp(rmtid);

            if (dtrm != null && dtrm.Rows.Count > 0)
            {
                lblremid.Text = dtrm.Rows[0]["remittancesid"].ToString();
                lblnumber.Text = dtrm.Rows[0]["remitnumber"].ToString();
                lblmoney.Text = Convert.ToDouble(dtrm.Rows[0]["remitmoney"]).ToString("0.00");
                lblname.Text = Encryption.Encryption.GetDecipherName( dtrm.Rows[0]["name"].ToString());
                hidremid.Value = dtrm.Rows[0]["remittancesid"].ToString();
                lblremittancesdate.Text = Convert.ToDateTime(dtrm.Rows[0]["remittancesdate"]).AddHours(Convert.ToInt32(Session["WTH"])).ToString();
                DateTime rcdt = Convert.ToDateTime(dtrm.Rows[0]["receivablesdate"]).AddHours(Convert.ToInt32(Session["WTH"]));
                this.txtgettime.Text = rcdt.ToShortDateString();
                this.ddlhours.SelectedValue = rcdt.Hour.ToString();
                this.ddlmins.SelectedValue = rcdt.Minute.ToString();
                this.ddlsecs.SelectedValue = rcdt.Second.ToString();
            }
        }
    }
    /// <summary>
    /// 行绑定事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvcdRemttlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;
            int useto = Convert.ToInt32(drv["use"]);
            DateTime rmdt = Convert.ToDateTime(drv["remittancesdate"]).AddHours(Convert.ToDouble(Session["WTH"]));
            Label lbluseto = (Label)e.Row.FindControl("lbluseto");
            Label lblrmdate = (Label)e.Row.FindControl("lblrmdate");
            Label lblname = (Label)e.Row.FindControl("lblname");

            lblname.Text = Encryption.Encryption.GetDecipherName(drv["name"].ToString());

            lblrmdate.Text = rmdt.ToString();
            if (useto == 1) lbluseto.Text = GetTran("007810", "支付订单");
            else if (useto == -1) lbluseto.Text = GetTran("007811", "汇款充值");
        }

    }
    /// <summary>
    /// 导出Excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDownExcel_Click(object sender, EventArgs e)
    {
        string cmd = ViewState["SQLSTR"].ToString();
        DataTable dt = DBHelper.ExecuteDataTable(cmd);
        if (dt == null)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("000053", "没有数据，不能导出Excel！") + "')</script>");
            return;
        }
        if (dt.Rows.Count < 1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("000053", "没有数据，不能导出Excel！") + "')</script>");
            return;
        }
        Response.AppendHeader("Content-Disposition", "attachment;filename=FileName.xls");
        Response.ContentEncoding = Encoding.UTF8;

        dt.Columns.Add("uses", typeof(String));
        dt.Columns.Add("remitmoneys", typeof(String));
        foreach (DataRow row in dt.Rows)
        {
            row["name"] = Encryption.Encryption.GetDecipherName(row["name"].ToString());
            try
            {
                row["remittancesdate"] = Convert.ToDateTime(row["remittancesdate"]).AddHours(sphours);
                row["uses"] = row["use"].ToString() == "1" ? GetTran("007810", "支付订单") : GetTran("007811", "汇款充值");
                row["remitmoneys"] = "￥" + double.Parse(row["remitmoney"].ToString()).ToString("f2");
            }
            catch
            {

            }
        }
        StringBuilder sb = Excel.GetExcelTable(dt, GetTran("007812", "在线支付人工确认"), new string[] { "remitmoneys=" + GetTran("000000", "汇款金额"), "remittancesid=" + GetTran("000000", "汇款单编号"), "name=" + GetTran("000000", "汇款人姓名"), "remittancesdate=" + GetTran("000000", "汇款单时间"), "number=" + GetTran("000000", "汇款人编号"), "uses=" + GetTran("000000", "汇款用途"), "mobiletele=" + GetTran("000000", "联系方式") });
        Response.Write(sb.ToString());

        Response.Flush(); //向客户端发送缓冲输出 即下载excel过程
        Response.End();  //发送缓冲输出停止本页执行
    }
}
