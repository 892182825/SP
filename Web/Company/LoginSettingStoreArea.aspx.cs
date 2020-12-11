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

public partial class LoginSettingStoreArea : BLL.TranslationBase
{
    protected string msg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.SafeLoginSettingStoreArea);
        if (!IsPostBack)
        {
            PageSet();
            this.btAddToBlackList.Attributes.Add("onClick", "return confirm('" + GetTran("000914", "是否确定删除该黑名单") + "');");
        }
        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.gvBlackStoreArea,
               new string[][]{
                    new string []{"000504","是否删除"},
                    new string []{"000150","店铺编号"},
                    new string []{"000039","店长姓名"}});
        this.TranControls(this.Add_blackList, new string[][] { new string[] { "000466", "添加到黑名单" } });
        this.TranControls(this.btAddToBlackList, new string[][] { new string[] { "000800", "删除黑名单" } });
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
    /// 设置获取店铺管辖黑名单的加载类
    /// </summary>
    protected void PageSet()
    {
        //初始化分页帮助类
        this.Pager1.PageBind(0, 10, "BlackGroup inner join StoreInfo on storeid=GroupValue", "Name,GroupValue,BlackGroup.id", " GroupType=4 ", " BlackGroup.id ", "gvBlackStoreArea");
        if (((DataTable)gvBlackStoreArea.DataSource).Rows.Count == 0)
        {
            divChkAll.Visible = false;
        }
    }
    /// <summary>
    /// 删除当前系统店铺管辖黑名单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btAddToBlackList_Click(object sender, EventArgs e)
    {
        int m = 0;
        int n = 0;

        foreach (GridViewRow row in gvBlackStoreArea.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkSelectRow");
                if (chk.Checked)
                {
                    HiddenField hid = (HiddenField)row.FindControl("hidID");
                    if (BlackGroupBLL.DelBlackGroup(int.Parse(hid.Value), 4) < 0)
                    {
                        n++;
                    }
                    m++;
                }
            }
        }
        if (m == 0)
        {
            ScriptHelper.SetAlert(out msg, GetTran("001420", "请用户选择要删除的店铺管辖黑名单."));
        }
        else if (n == 0)
        {
            ScriptHelper.SetAlert(out msg, GetTran("001419", "删除指定店铺管辖成功."));
            PageSet();
        }
        else
        {
            ScriptHelper.SetAlert(out msg, GetTran("001418", "删除部分店铺管辖成功."));
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
        string storeId = txtStoreID.Text.Trim();
        //验证用户填写黑名单店铺管辖
        if (storeId == "")
        {
            //提示用户填写
            ScriptHelper.SetAlert(out msg, GetTran("001417", "必须提供管辖店铺编号!"));
            this.txtStoreID.Focus();
            return;
        }
        //验证黑名单店铺管辖合法性
        if (BlackListBLL.CheckStoreID(storeId))
        {
            //提示无效
            ScriptHelper.SetAlert(out msg, GetTran("001415", "必须提供有效的管辖店铺编号!"));
            this.txtStoreID.Text = "";
            this.txtStoreID.Focus();
            return;
        }
        //判断是否已经存在该黑名单店铺管辖 3代表店铺管辖类型
        if (BlackGroupBLL.HasBlackGroup(storeId, 4))
        {
            //提示已经存在,无需填写
            ScriptHelper.SetAlert(out msg, GetTran("001412", "指定的管辖店铺已经存在，无法增加!"));
            this.txtStoreID.Text = "";
            return;
        }
        BlackGroupModel blackGroup = new BlackGroupModel();
        //店铺管辖类型
        blackGroup.IntGroupType = 4;
        blackGroup.IntGroupValue = storeId;
        string operateIP = Request.UserHostAddress.ToString();
        string operateNum = Session["Company"].ToString();
        switch (BlackGroupBLL.AddBlackGroup(blackGroup, operateIP, operateNum))
        {
            case 1:
                ScriptHelper.SetAlert(out msg, GetTran("000891", "添加成功!"));
                break;
            default:
                ScriptHelper.SetAlert(out msg, GetTran("001087", "执行发生错误"));
                break;
        }
        this.txtStoreID.Text = "";
        PageSet();
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
    protected void gvBlackStoreArea_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC';");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg;");
        }
    }
}
