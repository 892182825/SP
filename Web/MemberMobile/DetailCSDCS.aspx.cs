using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MemberMobile_DetailCSDCS : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

        var hkid = Request.QueryString["hkid"];
        string dt_one = "select w.hkid, w.bankcard,w.bankname,w.shenHestate,w.WithdrawMoney,m.name,w.auditingtime from memberinfo m, withdraw w,remittances r where m.Number=w.number and w.hkid=r.ID and (w.shenhestate=11 or w.shenhestate=2) order by auditingtime desc";
        DataTable dt = DAL.DBHelper.ExecuteDataTable(dt_one);
        Hkid.Value = hkid;
        rep_km.DataSource = dt;
        rep_km.DataBind();
    }

    protected void rep_km_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Button b = e.Item.FindControl("Button1") as Button;
            HiddenField h = e.Item.FindControl("HiddenField1") as HiddenField;
            HiddenField h2 = e.Item.FindControl("HiddenField2") as HiddenField;
            HyperLink hl = e.Item.FindControl("HyperLink1") as HyperLink;
            HyperLink h3 = e.Item.FindControl("HyperLink2") as HyperLink;
            hl.NavigateUrl = "/MemberMobile/DCSXX.aspx?hkid=" + h.Value + " &hkmoney=" + h2.Value;
            Label l = e.Item.FindControl("Label1") as Label;
            string dt_one = "select w.hkid, w.bankcard,w.bankname,w.shenHestate,w.WithdrawMoney,m.name,w.auditingtime from memberinfo m, withdraw w,remittances r where m.Number=w.number and w.hkid=r.ID and (w.shenhestate=11 or w.shenhestate=2) and hkid='" + h.Value + "' order by auditingtime desc";
            //this.ucPagerMb1.PageInit(dt_one, "rep_km");
            DataTable dt = DAL.DBHelper.ExecuteDataTable(dt_one);
            var shenHestate = dt.Rows[0]["shenHestate"].ToString();
            if (shenHestate == "11")
            {
                hl.Visible = false;
                h3.Visible = true;
            }
            else
            {

                hl.Visible = true;
                h3.Visible = false;

            }

        }
    }
}