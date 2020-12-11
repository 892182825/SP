using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.CommonClass;

public partial class MemberMobile_DCSXX : BLL.TranslationBase
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
            var hkmoney =  (Convert.ToDouble(Request.QueryString["hkmoney"]) * huilv).ToString();
            //HkTime.Text = (DateTime.Now).ToString();
            Money.Text = Math.Round(double.Parse(hkmoney)).ToString("#0.00");
        }
    }


    protected void btn_Click(object sender, EventArgs e)
    {

        var id = Request.QueryString["id"];
        var hkTime = HkTime.Text;//汇款时间
        var shje =(Convert.ToDouble(Money.Text)/huilv).ToString();//实汇金额
        var khh = KHH.Text;//开户行
        var zhanghao = ZH.Text;//账号
        var khmame = KHM.Text;//开户名
        var hksm = txtEnote.Text;//汇款说明

        if (Convert.ToDouble(shje) <= 0)
        {

            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("006053", "金额不能小于等于0！") + "');", true);
            return;
        }

        if (khh == "")
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("009068", "开户银行不能为空！") + "');", true);
            return;

        }
        if (zhanghao == "")
        {
            Page.ClientScript.RegisterStartupScript(GetType(), null, "<script language='javascript'>alert('" + GetTran("009069", "汇款账号不能为空！") + "')</script>");
            return;

        }
        if (khmame == "")
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("009070", "开户名不能为空！") + "');", true);
            return;
        }

        string sql1 = "select * from Withdraw where id='" + id + "'";
        DataTable dt1 = DAL.DBHelper.ExecuteDataTable(sql1);
        string sql = "";
        if (dt1.Rows.Count == 0)
        {
            sql = "update Remittances set RemittancesDate='" + hkTime + "',bankcard='" + zhanghao + "',bankname='" + khh + "',Khname='" + khmame + "',RemitMoney=" + shje + ",Remark='" + hksm + "',WppHkDj=1,shenHestate=11 where ID=" + id;
            DataTable dt = DAL.DBHelper.ExecuteDataTable(sql);
            string url = "../MemberMobile/DetailDCS.aspx?hkid=" + id;
            Response.Redirect(url);
        }
        else
        {
            var time = DateTime.Now;
            var hkid = Request.QueryString["hkid"];
            sql = "update withdraw set Ppje=" + shje + ",Hkbankcard='" + zhanghao + "',Hkbankname='" + khh + "',Hksm='" + hksm + "',Hktime='" + hkTime + "',HkKhname='" + khmame + "',HkDjdate='" + time + "', HkDj=1,shenHestate=11  where id=" + id;
            DataTable dt = DAL.DBHelper.ExecuteDataTable(sql);
            string url = "../MemberMobile/DetailDCS.aspx?hkid=" + hkid;
            Response.Redirect(url);
        }

    }
}