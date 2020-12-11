using BLL.MoneyFlows;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.CommonClass;

public partial class MemberMobile_TxDetailDHK : BLL.TranslationBase
{
    public int bzCurrency = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        //获取标准币种
        bzCurrency = CommonDataBLL.GetStandard();
        var hybh = Session["Member"].ToString();
        var hkid = Request.QueryString["hkid"];
        string dt_one = "select id,hkid,Khname,bankcard,bankname,shenHestate,WithdrawMoney,WithdrawTime from withdraw where  shenhestate in(0,1, 3) and HkDj=0 and IsJL=1 and Khname is not  null and number='" + hybh + "' order by WithdrawTime desc";
        DataTable dt = DAL.DBHelper.ExecuteDataTable(dt_one);
        Hkid.Value = hkid;
        rep_km.DataSource = dt;
        rep_km.DataBind();
    }


    protected void rep_km_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "txyhc":
                var hybh = Session["Member"].ToString();

                string KeywordCode = e.CommandArgument.ToString();//取得参数      
                string hh = "select * from Withdraw where number='" + hybh + "' and id=" + KeywordCode;
                DataTable dthh = DAL.DBHelper.ExecuteDataTable(hh);
                var txje = dthh.Rows[0]["WithdrawMoney"].ToString();
                var sxf = dthh.Rows[0]["WithdrawSXF"].ToString();
                var wyj = dthh.Rows[0]["Wyj"].ToString();


                string sql = "update Withdraw set shenHestate=-1 where id=" + KeywordCode;
                DataTable dt = DAL.DBHelper.ExecuteDataTable(sql);
                string sql1 = "update memberinfo set MemberShip=MemberShip-(" + txje + "+" + sxf + "+" + wyj + ") where Number='" + hybh + "'";
                DataTable dt1 = DAL.DBHelper.ExecuteDataTable(sql1);

                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("009038", "撤销成功！") + "');location.href='TxDetailDHK.aspx'</script>", false);
                break;
        }
    }

    protected void rep_km_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        LinkButton b = e.Item.FindControl("Button1") as LinkButton;
        HiddenField h = e.Item.FindControl("HiddenField1") as HiddenField;

        if (h.Value == "0")
        {
            b.Visible = true;


        }
        else
        {

            b.Visible = false;
        }
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            LinkButton ib = e.Item.FindControl("Button1") as LinkButton;
            ib.Text = GetTran("002258", "撤销");
        }

    }
}