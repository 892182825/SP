using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.CommonClass;
public partial class Member_CurrencyDetail : BLL.TranslationBase
{
    int bzCurrency = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        bzCurrency = CommonDataBLL.GetStandard();
        if (!IsPostBack) {
            if (Session["Default_Currency"] == null) Session["Default_Currency"] = bzCurrency;
            CommonDataBLL.BindCurrency_IDList(this.DropCurrency, Session["Default_Currency"].ToString());
        }
        TranControls(btn_submit, new string[][] { new string[] { "000434", "确定" } });
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        Session["Default_Currency"] = DropCurrency.SelectedValue.ToString();
        Page.ClientScript.RegisterStartupScript(GetType(), null, "<script language='javascript'>alert('" + GetTran("000222", "修改成功！") + "!')</script>");
    }
}
