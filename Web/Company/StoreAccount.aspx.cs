using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
public partial class Company_StoreAccount : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        if (!IsPostBack)
        {
            GetBind();
            Translations();
        }
    }

    private void Translations()
    {
        this.TranControls(this.Button1, new string[][] { new string[] { "000340", "查询" } });
        this.TranControls(this.GridView1,
                new string[][]{
                    new string []{"000037","服务机构编号"},
                    new string []{"000040","服务机构名称"},
                    new string []{"007336","订货款余额"},
                    new string []{"008103","周转款余额"}
                });
        TranControls(Button1, new string[][] { new string[] { "000340", "查询" } });
        this.TranControls(this.DropDownList1, new string[][] { new string[] { "007336", "订货款余额" }, new string[] { "008103", "周转款余额" } });
        this.TranControls(this.DropDownList2, new string[][] { new string[] { "000881", "不限" }, new string[] { "000361", "大于" }, new string[] { "000367", "小于" }, new string[] { "000372", "等于" }, new string[] { "000364", "大于等于" }, new string[] { "000368", "小于等于" } });
    }

    public void GetBind()
    {
        string table = "storeinfo";
        string clounms = "storeid,name,TotalAccountMoney,(TotalAccountMoney-TotalOrderGoodMoney) as DHK,(TurnOverMoney-TurnOverGoodsMoney) as ZZK";
        StringBuilder sb = new StringBuilder();
        sb.Append(" 1=1 ");
        if (txtnumber.Text.Trim() != "")
        {
            sb.Append(" and storeid='" + this.txtnumber.Text.Trim() + "'");
        }
        if (txtname.Text.Trim() != "")
        {
            sb.Append(" and name='" + Encryption.Encryption.GetEncryptionName(txtname.Text.Trim()) + "'");
        }
        double money = 0;
        if (txtmoney.Text.Trim() != "")
        {
            bool b = double.TryParse(txtmoney.Text.Trim(), out money);
            if (!b)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "success", "alert('输入的金额有误！');", true);
                return;
            }
        }
        if (DropDownList2.SelectedValue != "-1")
        {
            if (DropDownList1.SelectedValue == "1")
                sb.Append(" and (TotalAccountMoney-TotalOrderGoodMoney)" + DropDownList2.SelectedValue + money);
            else if (DropDownList1.SelectedValue == "2")
                sb.Append(" and (TurnOverMoney-TurnOverGoodsMoney)" + DropDownList2.SelectedValue + money);
        }
        Pager1.PageBind(0, 10, table, clounms, sb.ToString(), "id", "GridView1");
        Translations();

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Text = Encryption.Encryption.GetDecipherName(e.Row.Cells[1].Text);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        GetBind();
    }
}