using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.CommonClass;

public partial class MemberMobile_HCRXX : BLL.TranslationBase
{
    public int bzCurrency = 0;
    public double huilv;
    protected void Page_Load(object sender, EventArgs e)
    {
        //获取标准币种
        bzCurrency = CommonDataBLL.GetStandard();
        huilv = (AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString())));
        var hkmoney = (Convert.ToDouble(Request.QueryString["hkmoney"]) * huilv).ToString();
        //Permissions.ComRedirect(Page, Permissions.redirUrl);
        if (!this.IsPostBack)
        {
            //HkTime.Text = (DateTime.Now).ToString();
            Money.Text = Math.Round(double.Parse(hkmoney)).ToString("#0.00");
        }
        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.sub, new string[][] { new string[] { "000321", "提交" } });
    }
    protected void btn_Click(object sender, EventArgs e)
    {
        var id = Request.QueryString["id"];
        var hkTime = HkTime.Text;//汇款时间
        var shje = (Convert.ToDouble(Money.Text) / huilv).ToString();//实汇金额
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
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("007675", "用户开户银行不能为空!请先完善会员信息！") + "');", true);
            return;

        }
        if (zhanghao == "")
        {
            Page.ClientScript.RegisterStartupScript(GetType(), null, "<script language='javascript'>alert('" + GetTran("007677", "用户账户不能为空!请先完善会员信息！") + "')</script>");
            return;

        }
        if (khmame == "")
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("007676", "用户开户名不能为空!请先完善会员信息！") + "');", true);
            return;
        }


        string sql1 = "select * from Withdraw where id='" + id + "'";
        DataTable dt1 = DAL.DBHelper.ExecuteDataTable(sql1);
        string sql = "";
        int m = 0; string url = "";
        int  hkid =Convert.ToInt32( Request.QueryString["hkid"]);
        if (dt1.Rows.Count == 0)
        {
            sql = "update Remittances set RemittancesDate='" + hkTime + "',bankcard='" + zhanghao + "',bankname='" + khh + "',Khname='" + khmame + "',RemitMoney=" + shje + ",Remark='" + hksm + "',WppHkDj=1,shenHestate=11 where ID=" + id;
           m= DAL.DBHelper.ExecuteNonQuery(sql);
              url = "../MemberMobile/DetailDCS.aspx?hkid=" + id;
            Response.Redirect(url);
        }
        else
        {
            var time = DateTime.Now;
           
            sql = "update withdraw set Ppje=" + shje + ",Hkbankcard='" + zhanghao + "',Hkbankname='" + khh + "',Hksm='" + hksm + "',Hktime='" + hkTime + "',HkKhname='" + khmame + "',HkDjdate='" + time + "', HkDj=1,shenHestate=11  where id=" + id;
           m+= DAL.DBHelper.ExecuteNonQuery(sql);
              url = "../MemberMobile/DetailDCS.aspx?hkid=" + hkid;
           
        }

        if (m > 0)
        {

            //发送系统邮件
            string sendnumber = Session["Member"].ToString();

            DataTable dtt = DAL.DBHelper.ExecuteDataTable(" select top 1 number,WithdrawMoney,bankcard,bankname   from  Withdraw where hkid= " + hkid);
            if (dtt != null && dtt.Rows.Count > 0)
            {
                string recivenumber = dtt.Rows[0]["number"].ToString();
                double wm = Convert.ToDouble(dtt.Rows[0]["WithdrawMoney"]);
                string bkcd = dtt.Rows[0]["bankcard"].ToString();
                string bkname = dtt.Rows[0]["bankname"].ToString();
                string content = "<b style='margin:20px; '>汇款查收提醒</b> <p> 会员" + sendnumber + " 已向您的银行账户" + bkname + " " + bkcd + "转账汇款" + wm.ToString("0.00") + " ,请查询您的银行余额以确认。</p> <p>系统邮件</p>";
                SendEmail.SendSystemEmail(sendnumber, "2", content, recivenumber);
            }
 Response.Redirect(url);
        }







    }
}