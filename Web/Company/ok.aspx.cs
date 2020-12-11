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
using BLL.CommonClass;
using DAL;

public partial class Company_ok : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now.ToUniversalTime());

        if (!IsPostBack)
        {
            if (Request.QueryString["repid"] != null)
            {
                getbind(Request.QueryString["repid"]);
            }

            //getbind2(Request.QueryString["repid"]);
        }
    }

    public void getbind(string id)
    {
        string sql = "select rp.id,rp.totalrmbmoney,rp.totalmoney,hkpzImglj from remtemp  rp,Remittances mp where mp.Remittancesid=rp.Remittancesid and rp.Remittancesid='" + id + "'";
        DataTable dt = DAL.DBHelper.ExecuteDataTable(sql);
        if (dt.Rows.Count > 0)
        {
            Label1.Text = "$" + Convert.ToDouble(dt.Rows[0]["totalmoney"].ToString()).ToString("f2");
            Label2.Text = "$" + Convert.ToDouble(dt.Rows[0]["totalrmbmoney"].ToString()).ToString("f2");
            TextBox1.Text= Convert.ToDouble(dt.Rows[0]["totalmoney"].ToString()).ToString("f2");
            imgpingz.ImageUrl = "~/hkpzimg/" + dt.Rows[0]["hkpzImglj"].ToString();
          
        }
    }
    public void getbind2(string id)
    {
        string sql = "select rp.id,rp.totalrmbmoney,rp.totalmoney from RemtempStore  rp,Remittances mp where mp.Remittancesid=rp.Remittancesid and rp.Remittancesid='" + id + "'";
        DataTable dt = DAL.DBHelper.ExecuteDataTable(sql);
        if (dt.Rows.Count > 0)
        {
            Label1.Text = "$" + Convert.ToDouble(dt.Rows[0]["totalmoney"].ToString()).ToString("f2");
            Label2.Text = "$" + Convert.ToDouble(dt.Rows[0]["totalrmbmoney"].ToString()).ToString("f2");
            TextBox1.Text = Convert.ToDouble(dt.Rows[0]["totalmoney"].ToString()).ToString("f2");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string number = Request.QueryString["number"];
        string rmid = Request.QueryString["repid"];
        string ip = Request.UserHostAddress.ToString();
        string orderid = Request.QueryString["orderid"];
        double remark = 0;
        if (TextBox1.Text.Trim() != "")
        {
            bool b = double.TryParse(TextBox1.Text.Trim(), out remark);
            if (!b)
            {
                ClientScript.RegisterStartupScript(GetType(), "aaa", "alert('实汇金额只能输入数字！');", true);
                return;
            }
            //if (remark > Convert.ToDouble(Label2.Text.Trim().Substring(1)))
            //{
            //    ClientScript.RegisterStartupScript(GetType(), "aaa", "alert('实汇金额不能大于汇款金额！');", true);
            //    return;
            //}
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "aaa", "alert('实汇金额不能为空！');", true);
            return;
        }
        int res = -1;
        int res1 = -1;
        DataTable remittancedt = RemittancesDAL.GetRemittanceinfobyremid(rmid);
        string oper = Session["company"].ToString();
        string opip = Request.UserHostAddress.ToString();
        if (remittancedt != null && remittancedt.Rows.Count > 0)
        {
            int roltype = Convert.ToInt32(remittancedt.Rows[0]["RemitStatus"]) == 0 ? 2 : 1;
            string ord = remittancedt.Rows[0]["RelationOrderID"].ToString();
            double totalrmbmoney = Convert.ToDouble(TextBox1.Text.Trim());
            int isgsqr = Convert.ToInt32(remittancedt.Rows[0]["isgsqr"]);
            int dotype = 0;
            if (ord == "") dotype = 2; else dotype = 1;
            if (isgsqr == 0)
            {

                res = AddOrderDataDAL.OrderPayment(oper, ord, opip, roltype, dotype, 0, oper, "", 3, 1, 1, 1, rmid, totalrmbmoney, "");
                DataTable dt_one = DAL.DBHelper.ExecuteDataTable("select Ispipei,IsJL from remittances where RemittancesID='" + rmid + "'");
                string Ispipie = dt_one.Rows[0]["Ispipei"].ToString();//汇款id
                string IsJL = dt_one.Rows[0]["IsJL"].ToString();//汇款id
                if (Ispipie == "1" && IsJL == "1")
                {
                    res1 = AddOrderDataDAL.OrderPayment1(rmid);

                }

            }
    
            else
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('该汇款单已确认，不能重复操作！');window.close();</script>");
        }

        if (res == 0)
        {
            PublicClass.SendMsg(1, remittancedt.Rows[0]["RelationOrderID"].ToString(), "");
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('确认收款成功！');window.opener.location.href='DoSeecan.aspx';window.close();;</script>");
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('确认收款失败！');</script>");
        }
    }
}