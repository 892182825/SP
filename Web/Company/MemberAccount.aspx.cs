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
using System.Text;
using DAL;

public partial class Company_MemberAccount : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page,Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(6310); ///检查相应权限

        if (!IsPostBack)
        {
            GetBind();
            //translation();
        }
    }

    private void translation() {
        TranControls(DropDownList1, new string[][] { new string[] { "000000", "可用FTC账户" }, new string[] { "000000", "冻结FTC" }, new string[] { "000000", "保险账户)" }, new string[] { "000000", "奖金账户（USDT）" } });
        TranControls(Button1, new string[][] { new string[] { "000340", "查询" } });
        TranControls(DropDownList2, new string[][]{
            new string[]{"000881","不限"},
            new string[]{"000361","大于"},
            new string[]{"000367","小于"},
            new string[]{"000372","等于"},
            new string[]{"000364","大于等于"},
            new string[]{"000368","小于等于"}
        });
        TranControls(GridView1, new string[][]{
            new string[]{"000000","会员编号"},
            new string[]{"000000","会员姓名"},
            new string[]{"000000","可用FTC账户"},
            new string[]{"000000","冻结FTC "},
            new string[]{"000000","保险账户 "},
            new string[]{ "000000", "奖金账户（USDT）"},
            new string[]{ "000000", "保险锁仓"}
        });
    }

    public void GetBind()
    {
        string table = "memberinfo a,memberinfobalance1 b";
        string clounms = "a.number,name,Jackpot-Out as kyjb,fuxiaoin-fuxiaoout as fx,pointBIn-pointBOut as xf,pointAIn-pointAOut as tzjb,TotalRemittances-TotalDefray as xfjf,b.ARate as sfsd,zzye-xuhao as sczh";
        StringBuilder sb = new StringBuilder();
        sb.Append(" 1=1 and a.number=b.number ");
        
        string number = "";
        if (txtnumber.Text.Trim() != "")
        {
            string sql = "select number from MemberInfo where MobileTele='" + txtnumber.Text + "'";
            DataTable shj = DBHelper.ExecuteDataTable(sql);
            if (shj.Rows.Count > 0)
            {
                number = shj.Rows[0][0].ToString();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("002096", "无此编号，请检查后再重新输入") + "！')</script>");
                return;
            }
            sb.Append("  and a.Number='" + number + "'");
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
            sb.Append(" and " + DropDownList1.SelectedValue + DropDownList2.SelectedValue + money);
        }
        Pager1.PageBind(0, 10, table, clounms, sb.ToString(), "id", "GridView1");
        //translation();

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
