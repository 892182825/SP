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
using BLL.Registration_declarations;
using BLL.other.Company;
using Model;
using Model.Other;
using System.Collections.Generic;

public partial class Company_LoginSettingMember : BLL.TranslationBase
{
    protected string msg="";
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(Model.Other.EnumCompanyPermission.SafeLoginSettingMember);
        if (!IsPostBack)
        {
            PageSet();
            this.btAddToBlackList.Attributes.Add("onClick", "return confirm('" + GetTran("000914", "是否确定删除该黑名单") + "');");
        }
        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.gvBlackIP,
               new string[][]{
                    new string []{"000504","是否删除"},
                    new string []{"000024","会员编号"}});
        this.TranControls(this.Add_blackList, new string[][] { new string[] { "000466", "添加到黑名单" } });
        this.TranControls(this.btAddToBlackList, new string[][] { new string[] { "000800", "删除黑名单" } });
    }
    protected void Add_blackList_Click(object sender, EventArgs e)
    {
            //验证用户填写黑名单IP
        if (txtNumber.Text == "")
        {
            //提示用户填写
            ScriptHelper.SetAlert(out msg, GetTran("001237", "必须提供会员编号!"));
            this.txtNumber.Focus();
            return;
        }
        //判断当前编号是否存在
        if (ChangeTeamBLL.CheckNum(this.txtNumber.Text.Trim()))
        {
            ScriptHelper.SetAlert(out msg, GetTran("001238", "提供会员编号在该系统中不存在!"));
            return;
        }
        //判断是否已经存在该黑名单IP 3代表IP类型
        if (BlackListBLL.HasBlackList(txtNumber.Text, 0,0))
        {
            //提示已经存在,无需填写
            ScriptHelper.SetAlert(out msg, GetTran("001242", "指定的会员编号已经存在，无法增加!"));
            this.txtNumber.Text = "";
            return;
        }
        BlacklistModel blackListModel = new BlacklistModel();
        //ip类型
        blackListModel.UserType = 0;
        blackListModel.UserID = txtNumber.Text;
        blackListModel.OperateBH = Session["Company"].ToString();

        blackListModel.OperateIP = HttpContext.Current.Request.UserHostAddress.ToString();
        blackListModel.GroupID = 0;
        blackListModel.BlackDate = DateTime.Now;
        switch (BlackListBLL.AddBlackList(blackListModel))
        {
            case 1:
                ScriptHelper.SetAlert(out msg, GetTran("000891", "添加成功!"));
                break;
            case -1:
                ScriptHelper.SetAlert(out msg, GetTran("001087", "执行发生错误!"));
                break;
            default:
                ScriptHelper.SetAlert(out msg, GetTran("001089", "数据异常"));
                break;
        }
        this.txtNumber.Text = "";
        PageSet();
        Translations();
    }

    /// <summary>
    /// 设置获取IP黑名单的加载类
    /// </summary>
    protected void PageSet()
    {
        //初始化分页帮助类
        this.Pager1.PageBind(0, 10, "BlackList", " * ", " userType=0 and groupID=0 ", " id ", "gvBlackIP");
        if (((DataTable)gvBlackIP.DataSource).Rows.Count == 0)
        {
            divChkAll.Visible = false;
        }
    }
    
    /// <summary>
    /// 删除当前系统IP黑名单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btAddToBlackList_Click(object sender, EventArgs e)
    {
        int m = 0;
        int n = 0;
        foreach (GridViewRow row in gvBlackIP.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkSelectRow");
                if (chk.Checked)
                {
                    HiddenField hid = (HiddenField)row.FindControl("hidID");
                    if (BlackListBLL.DelBlackList(int.Parse(hid.Value), 0) < 0)
                    {
                        n++;
                    }
                    m++;
                }
            }
        }

        if (m == 0)
        {
            ScriptHelper.SetAlert(out msg, GetTran("001249", "请用户选择要删除的会员黑名单."));
        }
        else if (n == 0)
        {
            ScriptHelper.SetAlert(out msg, GetTran("001251", "删除指定会员黑名单成功."));
            PageSet();  
        }
        else
        {
            ScriptHelper.SetAlert(out msg, GetTran("001253", "删除部分会员黑名单成功."));
            PageSet();  
        }
        Translations();
    }

    /// <summary>
    /// 调用排序方法
    /// </summary>
    /// <param name="sender">排序方法触发控件</param>
    /// <param name="e">为排序方法提供必要数据</param>
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {

    }

    protected void gvBlackIP_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC';");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg;");
        }
    }
}
