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
using System.Collections.Generic;
using DAL;
using System.Drawing;
using System.Diagnostics; 
using System.IO;
using System.Data.SqlClient;
public partial class Company_CompanyBalance : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Permissions.CheckManagePermission(EnumCompanyPermission.FinanceJiesuan); //检查相应权限

        this.addNewQishu.Attributes["onclick"] = "return confirm('" + GetTran("001139", "确认要创建新一期吗?") + "')";
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        Translations();
        if (!IsPostBack)
        {
            int MaxQs = ReleaseBLL.GetMaxExpectNum();
            if (MaxQs <= 10)
            {
                this.TextBox1.Text = "1";//结算起始期
                this.TextBox2.Text = Convert.ToString(MaxQs);
            }
            else
            {
                this.TextBox1.Text = Convert.ToString(MaxQs - 10);
                this.TextBox2.Text = Convert.ToString(MaxQs);
            }
            this.Label1.Text = "(" + GetTran("001140", "当前期数为") + "<font color=red>" + MaxQs.ToString() + "</font>，" + GetTran("001141", "请输入") + "1-" + MaxQs.ToString() + GetTran("001142", "范围内的整数") + ")";

            this.showTotalQishuLink1();


            //自动结算是否启用
            if (ReleaseDAL.GetzidongjsQy() == "1")
                readerData();
            else
            {
                TextBox5.Enabled = false; //结算周期文本框
                DropDownList1.Enabled = false; //时 下拉框
                DropDownList2.Enabled = false;
                DropDownList3.Enabled = false;
                Button3.Enabled = false;
                jszq.Enabled = false;
                ClientScript.RegisterStartupScript(GetType(), "", "<script>enabledCK();</script>");
            }
        }
        Translations();

        nowtimeid.Text = DateTime.Now.ToString();
    }

    public void readerData()
    {
        DataTable dt = ReleaseDAL.Getzidongjiesuan();
        if (dt.Rows.Count > 0)
        {
            string[] sj = dt.Rows[0]["jiesuantime"].ToString().Split(' ');

            TextBox5.Text = sj[0] + "-" + sj[1];

            string[] fm = sj[1].Split(':');

            DropDownList1.SelectedValue = fm[0];
            DropDownList2.SelectedValue = fm[1].Replace("AM", "");
            DropDownList3.SelectedValue = fm[2];

            jszq.Text = dt.Rows[0]["jiesuanZQ"].ToString();//结算周期

            if (dt.Rows[0]["isCNewQi"].ToString() == "1")
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>window.onload=function(){setCk()};</script>");
            }

            shijid.Text = dt.Rows[0]["jiesuantime"].ToString();
        }


        CheckBox2.Checked = true;
    }

    private void Translations()
    {
        this.TranControls(this.GridView1,
                new string[][]{
                    new string []{"001179","创建日期"},
                    new string []{"001181","结算期数"},
                    new string []{"001182","是否已结算"},
                    new string []{"001184","结算次数"},
                    new string []{"000015","操作"}

                });
        this.TranControls(this.Button1, new string[][] { new string[] { "000434", "确 定" } });
        this.TranControls(this.btnjslog, new string[][] { new string[] { "007096", "结算日志" } });
        this.TranControls(this.addNewQishu, new string[][] { new string[] { "001173", "创建新一期" } });
        TranControls(Button3, new string[][] { new string[] { "000434", "确 定" } });

    }
    private void showTotalQishuLink1()
    {
        this.GridView1.DataSource = ReleaseBLL.GetConfigInfo();
        this.GridView1.DataBind();
        Translations();
    }
    /// <summary>
    /// 显示所有期数的结算链接。
    /// </summary>
    /// <param name="maxqishu">要显示的期数范围，从1到当前值。</param>
    private void showTotalQishuLink(int startqishu, int maxqishu)
    {
        IList<ConfigModel> list = ReleaseBLL.showTotalQishuLink(startqishu, maxqishu);
        foreach (ConfigModel info in list)
        {
            try
            {

                info.Date = DateTime.Parse(info.Date).AddHours(Convert.ToDouble(Session["WTH"])).ToShortDateString();
            }
            catch
            {

            }

        }
        this.GridView1.DataSource = list;
        this.GridView1.DataBind();
    }


    /// <summary>
    /// 创建新一期
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void addNewQishu_Click(object sender, EventArgs e)
    {
        int AudCount = Permissions.GetPermissions(EnumCompanyPermission.FinanceNewQi);
        if (AudCount != 4202)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000847", "对不起，您没有权限！") + "');</script>");
            return;
        }


        int temp = ReleaseBLL.GetMaxExpectNum();
        try
        {
            Application.UnLock();
            Application.Lock();
            //创建新一期
            ReleaseBLL.addNewQishu(temp + 1);

            RecycleApplication();
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("001143", "新一期创建完成，请重新登录！") + "');top.location.href('index.aspx')</script>");
        }
        catch(Exception eps)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("001144", "创建新一期时发生错误，重新创建！") + "')</script>");
        }
        finally
        {
            Application.UnLock();
            Application["jinzhi"] = "F";
        }
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
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("006915", "对不起，请正确输入！") + "')</script>");
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

        if (StartQishu <= EndQishu)
        {
            showTotalQishuLink(StartQishu, EndQishu);
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("001152", "请正确检查起止顺序！") + "')</script>");
            return;
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
            //string qishu = ((HtmlInputHidden)e.Row.FindControl("HidQishu")).Value;
            //HyperLink link = (HyperLink)e.Row.FindControl("HyperJieSuan");
            //System.Web.UI.HtmlControls.HtmlGenericControl div = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Row.FindControl("jiesuan");
            //if (ReleaseBLL.GetIsExistsConfig(int.Parse(qishu)))
            //{
            //    if (ReleaseBLL.IsNotProvideBonus(int.Parse(qishu)) == false)//当期是否有未发放的奖金
            //    {
            //        //没有
            //        div.InnerHtml = "<a href='javascript:openCountWin(" + qishu + ")'>" + GetTran("001154", "结算第") + qishu + GetTran("000157", "期") + "</a>";
            //    }
            //    else
            //    {
            //        div.InnerHtml = "<a href=\"SalaryGrant.aspx\" onClick=\"return confirm('" + GetTran("000156", "第") + qishu + GetTran("001159", "期有未发放的奖金,请[撤消]后再结算...") + "');\">" + GetTran("001154", "结算第") + qishu + GetTran("000157", "期") + "</a>";
            //        ReleaseBLL.upProvideState(int.Parse(qishu));
            //    }
            //}

            try
            {
                e.Row.Cells[0].Text = DateTime.Parse(e.Row.Cells[0].Text).AddHours(Convert.ToDouble(Session["WTH"])).ToShortDateString();
            }
            catch
            {

            }

            if (e.Row.Cells[2].Text == "0")
                e.Row.Cells[2].Text = GetTran("000235", "否");
            else
                e.Row.Cells[2].Text = GetTran("000233", "是");
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            //绑定表头背景图
            e.Row.Attributes.Add("style", "background-image:url('images/tabledp.gif')");
            Translations();
        }
    }

    /// <summary>
    /// 回收当前应用程序资源
    /// </summary>
    /// <returns></returns>
    public bool RecycleApplication()
    {
        bool success = true;
        try
        {
            HttpRuntime.UnloadAppDomain();
        }
        catch (Exception ex)
        {
            success = false;
        }
        return success;
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string rq = TextBox5.Text.Trim();

        string hour = DropDownList1.SelectedValue;
        string min = DropDownList2.SelectedValue;
        string sec = DropDownList3.SelectedValue;

        int isjs = 0;

        if (Textbox4.Text == "y")
            isjs = 1; //结算时创建新一期选中

        if (DateTime.Compare(Convert.ToDateTime(rq + " " + hour + ":" + min + ":" + sec), DateTime.Now) < 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("007128", "选择日期不能小于当前时间！") + "');</script>");
            return;
        }

        string zqts = jszq.Text.Trim();

        try
        {
            Convert.ToInt32(zqts);
        }
        catch
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('自动结算周期只能为整数！');</script>");
            return;
        }

        if (Convert.ToInt32(zqts) < 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('自动结算周期不能为负数！');</script>");
            return;
        }

        int a = ReleaseDAL.UpdJiesuantime(rq + " " + hour + ":" + min + ":" + sec, isjs, zqts);

        if (a == 1)
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('设置成功！');window.location.href=window.location.href</script>");
        else
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('设置失败！');</script>");


    }
    protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        string isqy = "0";

        if (CheckBox2.Checked)
            isqy = "1";

        //设置自动结算是否启用
        if (ReleaseDAL.UpdJiesuanQy(isqy) == 1)
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('设置成功！');window.location.href=window.location.href;</script>");
        else
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('设置失败！');window.location.href=window.location.href;</script>");
    }
    protected void btnjslog_Click(object sender, EventArgs e)
    {
        Response.Redirect("jslog.aspx");
    }
}