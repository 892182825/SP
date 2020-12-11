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
using Model;
using BLL.MoneyFlows;
using Model.Other;
using BLL.CommonClass;

public partial class Company_ReleaseSet : BLL.TranslationBase 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        //检查相应权限
        Permissions.CheckManagePermission(EnumCompanyPermission.FinanceFafang);
        
        if (!IsPostBack)
        {
            GetShopList();
        }
        
        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.GridView1,
                new string[][]{
                    new string []{"000045","期数"},
                    new string []{"002262","会员是否可以查看奖金"}});
        this.TranControls(this.Button4, new string[][] { new string[] { "000434", "确 定" } });
    }
    private void GetShopList()
    {
        string table = "config";
        string condition = "1<2";
        string key = "config.ExpectNum";
        string cloumns = "config.ExpectNum";
        this.GridView1.DataSourceID = null;
        this.Pager1.ControlName = "GridView1";
        this.Pager1.key = key;
        this.Pager1.PageColumn = cloumns;
        this.Pager1.Pageindex = 0;
        this.Pager1.PageTable = table;
        this.Pager1.Condition = condition;
        this.Pager1.PageSize = 10;
        this.Pager1.PageCount = 0;
        this.Pager1.PageBind();
        Translations();
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in this.GridView1.Rows)
        {
            ChangeLogs cl = new ChangeLogs("config", "ltrim(rtrim(str(ExpectNum)))");
            
            int ExpectNum=int.Parse(row.Cells[0].Text);
            cl.AddRecord(ExpectNum);
            int num = 0;
            bool blean=(row.FindControl("CheckBox1") as CheckBox).Checked;
            if (blean)
            {
                num = ReleaseBLL.Release(ExpectNum, 1);
            }
            else
            {
                num = ReleaseBLL.Release(ExpectNum, 0);
            }

            if (num > 0)
            {
                cl.AddRecord(ExpectNum);
                cl.ModifiedIntoLogs(ChangeCategory.company39, ExpectNum.ToString(), ENUM_USERTYPE.objecttype3);
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("001401", "操作成功！") + "')</script>");
            }
            GetShopList();
            
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
            int num = ReleaseBLL.GetOutBonus(int.Parse(e.Row.Cells[0].Text));
            if (num == 1)
            {
                (e.Row.FindControl("CheckBox1") as CheckBox).Checked = true;
            }
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Attributes.Add("style", "background-image:url('images/tabledp.gif')");
        }
        
        Translations();
    }
}
