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

public partial class Company_ReleaseGrant : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        //检查相应权限
        Permissions.CheckManagePermission(EnumCompanyPermission.FinanceJiangjinshezhi);

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
                    new string []{"006639","添加"},
                    new string []{"002258","撤销"},
                    new string []{"000440","查看"}
                });
    }
    private void GetShopList()
    {
        string table = "config";
        string condition = "1<2";
        string key = "expectnum";
        string cloumns = "*";
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
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (IsProcessExist("ds2010.exe"))
        //{
        //    ClientScript.RegisterStartupScript(this.GetType(), "success", "alert('" + GetTran("007165", "奖金正在结算……") + "');", true);
        //    return;
        //}

        //获取最后一次结算状态
        DataTable dt = CommonDataBLL.GetJstype();

        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["jstype"].ToString() == "0")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "success", "alert('" + GetTran("007165", "奖金正在结算……") + "');", true);
                GetShopList();
                return;
            }
        }

        if (e.CommandName == "add_jj")
        {
            string qishu = e.CommandArgument.ToString();
            bool blean = ReleaseBLL.IsProvide(int.Parse(qishu));
            if (blean)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("001374", "对不起，该期奖金已经写入用户电子帐户！") + "');</script>");
                GetShopList();
                return;
            }
            else
            {
                bool b = ReleaseBLL.Provide(int.Parse(qishu));
                if (b)
                {

                    Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("001375", "写入成功！") + "');</script>");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("001376", "对不起，写入电子帐户失败，请联系维护人员！") + "');</script>");
                }
            }
            GetShopList();
        }
        if (e.CommandName == "cancle_jj")
        {
            string ExpectNum = e.CommandArgument.ToString();
            bool blean = ReleaseBLL.IsProvide(int.Parse(ExpectNum));
            if (!blean)
            {
                ScriptHelper.SetAlert(Page, "奖金已撤销！");
                GetShopList();
                return;
            }
            else
            {
                int num = ReleaseBLL.Cancel(int.Parse(ExpectNum));
                if (num == 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("001380", "撤销第") + ExpectNum + GetTran("001381", "期奖金成功！") + "');</script>");
                }

                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("001215", "对不起，撤消电子帐户失败，请联系维护人员！") + "');</script>");
                }
            }
            GetShopList();
        }

        if (e.CommandName == "add_View")
        {
            string qishu = e.CommandArgument.ToString();
            Response.Redirect("SalaryGrantView.aspx?qs=" + qishu);
        }
    }
    protected void lkbtn_CancleTrue_Click(object sender, EventArgs e)
    {
        string ExpectNum = this.txt_qishu.Value;
        int num = ReleaseBLL.Cancel(int.Parse(ExpectNum));
        if (num == 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("001380", "撤销第") + ExpectNum + GetTran("001381", "期奖金成功！") + "');</script>");
        }

        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("001215", "对不起，撤消电子帐户失败，请联系维护人员！") + "');</script>");            
        }
        GetShopList();
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Response.Redirect("SalarySet.aspx");
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
            LinkButton button1 = (LinkButton)e.Row.FindControl("add_jj");
            LinkButton button2 = (LinkButton)e.Row.FindControl("cancle_jj");

            int ExpectNum = Convert.ToInt32(e.Row.Cells[0].Text.ToString().Trim());
            if (ReleaseBLL.IsProvide(ExpectNum))
            {
                button1.Visible = false;
            }
            else
            {
                button2.Visible = false;
            }

            button1.Attributes.Add("onClick", "return confirm('" + GetTran("001386", "你确定要添加吗?") + "');");
            button2.Attributes.Add("onClick", "return confirm('" + GetTran("001388", "你确定要撤消吗?") + "');");	
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Attributes.Add("style", "background-image:url('images/tabledp.gif')");
        }
        Translations();
    }
    /// <summary> 
    /// 判断进程是否存在 
    /// </summary>
    /// <param name="proName"></param>
    /// <returns></returns>
    public static bool IsProcessExist(String proName)
    {

        foreach (System.Diagnostics.Process thisproc in System.Diagnostics.Process.GetProcessesByName(proName))
        {
            if (thisproc.ProcessName.ToLower().Trim() == proName.ToLower().Trim())
            {
                return true;
            }

        }
        return false;
    } 

}
