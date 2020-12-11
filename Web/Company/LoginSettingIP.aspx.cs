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
using BLL.other.Company;
using Model;
using DAL;
using System.Collections.Generic;
using Model.Other;

/*
 * 创建人 ：孙延昊
 * IP黑名单设置页面
 * **/
public partial class Company_LoginSettingIP : BLL.TranslationBase
{
    protected string msg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(Model.Other.EnumCompanyPermission.SafeLoginSettingIP);
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
                    new string []{"000961","IP地址"}});
        this.TranControls(this.Add_blackList, new string[][] { new string[] { "000466", "添加到黑名单" } });
        this.TranControls(this.btAddToBlackList, new string[][] { new string[] { "000800", "删除黑名单" } });
    }


    /// <summary>
    /// 设置获取IP黑名单的加载类
    /// </summary>
    protected void PageSet()
    {
        //初始化分页帮助类   
        this.Pager1.PageBind(0, 10, "BlackList", "*", " userType=3 and groupID=0  ", "id ","gvBlackIP");
        if (((DataTable)gvBlackIP.DataSource).Rows.Count == 0)
        {
            divChkAll.Visible = false;
        }
        Translations();
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
                    if (BlackListBLL.DelBlackList(int.Parse(hid.Value), 3) < 0)
                    {
                        n++;
                    }
                    m++;
                }
            }
        }
        if (m == 0)
        {
            ScriptHelper.SetAlert(out msg, GetTran("001065","请用户选择要删除的IP黑名单."));
        }
        else if (n==0)
        {
            ScriptHelper.SetAlert(out msg, GetTran("001068","删除指定IP成功."));
            PageSet();
        }
        else
        {
            ScriptHelper.SetAlert(out msg, GetTran("001070", "删除部分IP成功."));
            PageSet();
        }
    }



    /// <summary>
    /// 验证IP有效性
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    public bool IsValidIP(string ip)
    { 
        string[] ips = ip.Split('.');
        int count = 0;
        if (ips[0].ToString() == "*")
        {
            return false;
        }
        for (int i = 0; i < ips.Length; i++)
        {

            if (ips[i].ToString() == "*")
            {
                count += 1;
            }
        }
        if (ips.Length == 4 && count == 0)
        {
            try
            {
                if (System.Int32.Parse(ips[0]) < 256 && System.Int32.Parse(ips[1]) < 256 & System.Int32.Parse(ips[2]) < 256 & System.Int32.Parse(ips[3]) < 256)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }

        }

        if (ips.Length == 4 && count == 1)
        {
            try
            {
                if (System.Int32.Parse(ips[0]) < 256 && System.Int32.Parse(ips[1]) < 256 & System.Int32.Parse(ips[2]) < 256)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }

        }

        if (ips.Length == 4 && count == 2)
        {
            try
            {
                if (System.Int32.Parse(ips[0]) < 256 && System.Int32.Parse(ips[1]) < 256)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }

        }

        if (ips.Length == 4 && count == 3)
        {
            try
            {
                if (System.Int32.Parse(ips[0]) < 256)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }

        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 加入黑名单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Add_blackList_Click(object sender, EventArgs e)
    {
        //验证用户填写黑名单IP
        if (IPBianhao.Text == "") {
            //提示用户填写
            ScriptHelper.SetAlert(out msg, GetTran("001075","必须提供IP地址!"));
            this.IPBianhao.Focus();
            return;
        }
        //验证黑名单IP合法性
        if (!IsValidIP(IPBianhao.Text))
        {
            //提示无效
            ScriptHelper.SetAlert(out msg,GetTran("001078","必须提供有效的IP地址!"));
            this.IPBianhao.Text = "";
            this.IPBianhao.Focus();
            return;
        }
        //判断是否已经存在该黑名单IP 3代表IP类型
        string ip = IPBianhao.Text.Replace(" ", "");
        string[] ips = ip.Split('.');
        ip="";
        foreach(string _ip in ips)
        {
            if (ip.Trim() != "*")
            {
                try
                {
                    ip += int.Parse(_ip) + ".";
                }
                catch (FormatException)
                {
                    ScriptHelper.SetAlert(out msg,GetTran("001078","必须提供有效的IP地址!"));
                    return;
                }
            }
            else
            {
                ip += _ip + ".";
            }
        }
        ip = ip.Substring(0, ip.Length - 1);
        if (BlackListBLL.HasBlackList(ip, 3, 0))
        { 
            //提示已经存在,无需填写
            ScriptHelper.SetAlert(out msg,GetTran("001083","指定的IP已经存在，无法增加!"));
            this.IPBianhao.Text = "";
            return;
        }
        BlacklistModel blackListModel = new BlacklistModel();
        //ip类型
        blackListModel.UserType = 3;
        blackListModel.UserID = ip;
        blackListModel.OperateBH = Session["Company"].ToString();

        blackListModel.OperateIP = HttpContext.Current.Request.UserHostAddress.ToString();
        blackListModel.GroupID = 0;
        blackListModel.BlackDate = DateTime.Now;
        switch (BlackListBLL.AddBlackList(blackListModel))
        {
            case 1:
                ScriptHelper.SetAlert(out msg, GetTran("000891","添加成功!"));
                break;
            case -1:
                ScriptHelper.SetAlert(out msg, GetTran("001087","执行发生错误!"));
                break;
            default:
                ScriptHelper.SetAlert(out msg, GetTran("001089", "数据异常"));
                break;
        }
        this.IPBianhao.Text = "";
        PageSet();
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
