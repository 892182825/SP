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

public partial class Company_ControlBalance : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        //检查相应权限
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.FinanceTiaokong);
        if (!IsPostBack)
        {
            int MaxQs = ReleaseBLL.GetMaxExpectNum();
            if (MaxQs == 1)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("001291", "当前最大期是第一期不能进入调控结算") + "')</script>");
                return;
            }

            if (MaxQs <= 10)
            {
                this.TextBox1.Text = "1";
                this.TextBox2.Text = Convert.ToString(MaxQs-1);
            }
            else
            {
                this.TextBox1.Text = Convert.ToString(MaxQs - 10);
                this.TextBox2.Text = Convert.ToString(MaxQs-1);
            }
            this.Label1.Text = "(" + GetTran("001140", "当前期数为") + "<font color=red>" + MaxQs.ToString() + "</font>，" + GetTran("001141", "请输入") + "1-" + (MaxQs - 1).ToString() + GetTran("001142", "范围内的整数") + ")";
            //this.showTotalQishuLink(Convert.ToInt32(this.TextBox1.Text.Trim()),Convert.ToInt32(this.TextBox2.Text.Trim()));
            this.showTotalQishuLink1(MaxQs);
        }
        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.GridView1,
                new string[][]{
                    new string []{"001179","创建日期"},
                    new string []{"001181","结算期数"},
                    new string []{"000015","操作"}

                });
        this.TranControls(this.Button1, new string[][] { new string[] { "000434", "确 定" } });
    }
    private void showTotalQishuLink1(int MaxQs)
    {
        this.GridView1.DataSource = ReleaseBLL.GetConfigInfo1(MaxQs);
        this.GridView1.DataBind();
    }
    /// <summary>
    /// 显示所有期数的结算链接。
    /// </summary>
    /// <param name="maxqishu">要显示的期数范围，从1到当前值。</param>
    private void showTotalQishuLink(int startqishu, int maxqishu)
    {
        this.GridView1.DataSource = ReleaseBLL.showTotalQishuLink(startqishu, maxqishu);
        this.GridView1.DataBind();
    }
    /// <summary>
    /// 插入配置表
    /// </summary>
    /// <param name="qishu"></param>
    private void insertConfig(int qishu)
    {
        ReleaseBLL.insertConfig(qishu);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        int StartQishu = 0, EndQishu = 0;
        try
        {
            StartQishu = Convert.ToInt32(this.TextBox1.Text.Trim());
            EndQishu = Convert.ToInt32(this.TextBox2.Text.Trim());

        }

        catch
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("001145", "请输入数字！") + "')</script>");
            return;
        }
        
        if (StartQishu <= 0 || EndQishu <= 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("001147", "请输入大于零的整数！") + "')</script>");
            return;
        }

        if (EndQishu > ReleaseBLL.GetMaxExpectNum())
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("001150", "不能大于最大期数！") + "')</script>");
            return;
        }

        if(EndQishu==ReleaseBLL.GetMaxExpectNum())
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("001150", "不能大于最大期数！") + "')</script>");
            return;
        }

        if (StartQishu <= EndQishu)
        {
            showTotalQishuLink(StartQishu, EndQishu);
        }

        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("001152", "请正确检查起止顺序！") + "')</script>");
            return;
        }
        Translations();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
            string qishu = ((HtmlInputHidden)e.Row.FindControl("HidQishu")).Value;
            HyperLink link = (HyperLink)e.Row.FindControl("HyperJieSuan");
            System.Web.UI.HtmlControls.HtmlGenericControl div = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Row.FindControl("jiesuan");
            if (ReleaseBLL.IsNotProvideBonus(int.Parse(qishu)) == false)
            {
                div.InnerHtml = "<a href='javascript:openCountWin(" + qishu + ")'>" + GetTran("001154", "结算第") +qishu + GetTran("000157", "期")+ "</a>";
            }
            else
            {
                div.InnerHtml = "<a href=\"SalaryGrant.aspx\" onClick=\"return confirm('" + GetTran("000156", "第") + qishu + GetTran("001159", "期有未发放的奖金,请[撤消]后再结算...") + "');\">" + GetTran("001154", "结算第") + qishu + GetTran("000157", "期") + "</a>";
                ReleaseBLL.upProvideState(int.Parse(qishu));
            }
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Attributes.Add("style", "background-image:url('images/tabledp.gif')");
        }
    }
}
