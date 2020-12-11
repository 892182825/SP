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
using System.Collections.Generic;
using Model;
using BLL.other.Company;
using BLL.Registration_declarations;
using Model.Other;

public partial class LoginSettingNetWork : BLL.TranslationBase
{
    RegistermemberBLL registermemeberBLL = new RegistermemberBLL();
    protected string msg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.SafeLoginSettingNetWork);
        if (!IsPostBack)
        {
            PageSet();
            this.btnDelBlack.Attributes.Add("onClick", "return confirm('" + GetTran("000914", "是否确定删除该黑名单") + "');");
        }
        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.gvNetWork,
               new string[][]{
                    new string []{"000504","是否删除"},
                    new string []{"001311","领导人编号"},
                    new string []{"001308","网络类型"},
                    new string []{"001307", "领导人姓名"}});
        this.TranControls(this.radlnetWorkType,
                new string[][]{
                    new string []{"000663","安置"},
                    new string []{"000667","推荐"}});
        this.TranControls(this.btnAddBlackGroup, new string[][] { new string[] { "000466", "添加到黑名单" } });
        this.TranControls(this.btnDelBlack, new string[][] { new string[] { "000800", "删除黑名单" } });
    }

    /// <summary>
    /// 解密姓名
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public string getname(object name)
    {
        return Encryption.Encryption.GetDecipherName(name.ToString());
    }
    /// <summary>
    /// 设置获取网络团队黑名单的加载类
    /// </summary>
    protected void PageSet()
    {
        //初始化分页帮助类   

        this.Pager1.PageBind(0, 10, "BlackGroup inner join MemberInfo on Number=GroupValue", "Name,GroupValue,BlackGroup.id,GroupType", " GroupType=6 or GroupType=5", " BlackGroup.id ","gvNetWork");
        if (((DataTable)gvNetWork.DataSource).Rows.Count == 0)
        {
            divChkAll.Visible = false;
        }

    }
    /// <summary>
    /// 删除当前系统网络团队黑名单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btAddToBlackList_Click(object sender, EventArgs e)
    {
        int m = 0;
        int n = 0;

        foreach (GridViewRow row in gvNetWork.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkNet");
                if (chk!=null&&chk.Checked)
                {
                    HiddenField hid = (HiddenField)row.FindControl("hidID");
                    if (BlackGroupBLL.DelBlackGroup(int.Parse(hid.Value)) < 0)
                    {
                        n++;
                    }
                    m++;
                }
            }
        }
        if (m == 0)
        {
            ScriptHelper.SetAlert(out msg, GetTran("001333", "请用户选择要删除的网络团队黑名单."));
        }
        else if (n == 0)
        {
            ScriptHelper.SetAlert(out msg, GetTran("001335", "删除指定网络团队成功."));
            PageSet();
        }
        else
        {
            ScriptHelper.SetAlert(out msg, GetTran("001341", "删除部分网络团队成功."));
            PageSet();
        }
        Translations();
    }

    protected string GetArea(object obj, int i)
    {
        if (obj == null)
        {
            return "";
        }
        else
        {
            string areas = obj.ToString();
            string[] area = areas.Split(',');
            if (area.Length < 3)
            {
                return "";
            }
            return area[i];
        }
    }

    /// <summary>
    /// 加入黑名单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Add_blackList_Click(object sender, EventArgs e)
    {
        //验证用户填写黑名单网络团队
        if (txtNumber.Text == "")
        {
            //提示用户填写
            ScriptHelper.SetAlert(out msg, GetTran("001343", "必须提供网络关系编号!"));
            this.txtNumber.Focus();
            return;
        }
        //验证黑名单网络团队合法性
        if (registermemeberBLL.CheckNumberTwice(txtNumber.Text) == null)
        {
            //提示无效
            ScriptHelper.SetAlert(out msg, GetTran("001347", "必须提供有效的网络关系编号!"));
            this.txtNumber.Text = "";
            this.txtNumber.Focus();
            return;
        }
        //判断是否已经存在该黑名单网络团队 3代表网络团队类型
        if (BlackGroupBLL.HasBlackGroup(txtNumber.Text, (this.radlnetWorkType.SelectedIndex==0?5:6)))
        {
            //提示已经存在,无需填写
            ScriptHelper.SetAlert(out msg, GetTran("001350", "指定的网络关系已经存在，无法增加!"));
            this.txtNumber.Text = "";
            return;
        }
        BlackGroupModel blackGroup = new BlackGroupModel();
        //网络团队类型
        blackGroup.IntGroupType = (this.radlnetWorkType.SelectedIndex == 0 ? 5 : 6);
        blackGroup.IntGroupValue = txtNumber.Text;
        string operateIP = Request.UserHostAddress.ToString();
        string operateNum = Session["Company"].ToString();
        switch (BlackGroupBLL.AddBlackGroup(blackGroup, operateIP, operateNum))
        {
            case 1:
                ScriptHelper.SetAlert(out msg, GetTran("000006", "添加成功!"));
                break;
            default:
                ScriptHelper.SetAlert(out msg, GetTran("001087", "执行发生错误"));
                break;
        }
        this.txtNumber.Text = "";
        PageSet();
        Translations();
    }

    protected string GetNetType(object obj)
    {
        if (obj != null)
        {
            if (obj.ToString() == "5")
                return GetTran("000663", "安置");
            else if (obj.ToString() == "6")
                return GetTran("000667", "推荐");
            else
                return "";
        }
        else
            return "";
    }

    /// <summary>
    /// 调用排序方法
    /// </summary>
    /// <param name="sender">排序方法触发控件</param>
    /// <param name="e">为排序方法提供必要数据</param>
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {

    }


    protected void gvNetWork_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC';");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg;");
        }
    }
}
