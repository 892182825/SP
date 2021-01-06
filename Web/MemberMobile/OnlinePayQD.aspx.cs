using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Model;
using BLL.MoneyFlows;
using Model.Other;
using BLL.CommonClass;
using System.Data;
using DAL;
using System.Data.SqlClient;

public partial class MemberMobile_OnlinePayQD : BLL.TranslationBase
{
    public int bzCurrency = 0;
    public double huilv;
    protected void Page_Load(object sender, EventArgs e)
    {
        //获取标准币种
        bzCurrency = CommonDataBLL.GetStandard();
        huilv = (AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString())));
        if (!this.IsPostBack)
        {
            var HkID = Request.QueryString["HkID"];
            var bishu = Request.QueryString["bishu"];
            var RemitMoney = (Convert.ToDouble(Request.QueryString["RemitMoney"]) * huilv).ToString("f2");
            int bishu1 = int.Parse(bishu);
            rmoney.Text = RemitMoney;
            rmoney1.Value = RemitMoney;
            hkid.Value = HkID;
            DataTable dt = null;// RemittancesBLL.jinliucx(HkID);
            bishucount.Text = (dt.Rows.Count).ToString();

            string sm = "select describe from JLparameter where jlcid=8";
            DataTable sm2 = DAL.DBHelper.ExecuteDataTable(sm);
            var shuomin = sm2.Rows[0]["describe"].ToString();
            Label2.Text = shuomin;


            if (bishucount.Text == "0")
            {
                bishucount.Text = "1";

                string dt_one1 = "update Remittances set Ispipei=1 where ID=" + HkID;
                DataTable dt3 = DAL.DBHelper.ExecuteDataTable(dt_one1);

                DataTable dt_one3 = DAL.DBHelper.ExecuteDataTable("select * from remittances where ID=" + HkID);
                string RemittancesID = dt_one3.Rows[0]["RemittancesID"].ToString();//汇款单号
                string RemBankBook = dt_one3.Rows[0]["ImportNumber"].ToString();//汇款单号
                string RemBankname = dt_one3.Rows[0]["name"].ToString();//汇款单号
                string RemBankaddress = dt_one3.Rows[0]["ImportBank"].ToString();//汇款单号
                RemittancesDAL.WithdrawMoney(Session["Member"].ToString(), Convert.ToDouble(RemitMoney) / huilv, RemittancesID, RemBankBook, RemBankname, RemBankaddress);

                string dt_one = "select top(1) * from companybank order by ID desc";
                DataTable dt2 = DAL.DBHelper.ExecuteDataTable(dt_one);
                rep_km.Visible = false;
                rep_km1.Visible = true;
                rep_km1.DataSource = dt2;
                rep_km1.DataBind();
            }
            else
            {
                rep_km1.Visible = false;
                rep_km.Visible = true;
                rep_km.DataSource = dt;
                rep_km.DataBind();
            }
            Translations();
        }
    }

    protected void rep_km1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            var RemitMoney = Convert.ToDouble(Request.QueryString["RemitMoney"]) * huilv;
            Label b = e.Item.FindControl("Label1") as Label;

            b.Text = Math.Round(RemitMoney).ToString("#0.00");

        }
    }

    private void Translations()
    {
        this.TranControls(this.Button2, new string[][]{
            new string []{"001614","取消"},
        });
    }
    /// <summary>
    /// 取消
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Btn_click(object sender, EventArgs e)
    {
        fanhuiz.Value = "1";
        var hkid = Request.QueryString["HkID"];

        int res = 0;
        SqlParameter[] parm = new SqlParameter[] { 
            new SqlParameter("@hkid", hkid),
            new SqlParameter("@err", res)
        };
        parm[1].Direction = ParameterDirection.Output;
        DAL.DBHelper.ExecuteNonQuery("zfpp_cx", parm, CommandType.StoredProcedure);
        if (parm[1].Value.ToString() == "0")
        {
            int fanhui = RemittancesBLL.jiliuZZ(hkid);
            fanhuiz.Value = fanhui.ToString();

            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("009055", "取消成功") + "！');location.href='OnlinePayment.aspx'</script>", false);
        }
        else
        {
            fanhuiz.Value = "0";
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("009056", "取消失败") + "！');</script>", false);
        }
    }
}