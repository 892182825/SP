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

using Model.Other;
using BLL.other.Company;
using System.Collections.Generic;
using BLL.CommonClass;
public partial class Company_LoginSetting : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        string manageId = BLL.CommonClass.CommonDataBLL.getManageID(1);
        if (Session["Company"] == null || Session["Company"].ToString().Trim() != manageId)
        Permissions.CheckManagePermission(EnumCompanyPermission.SafeLoginSetting);
        if(!IsPostBack)
        getStatus();
        Translations();
    }

    private void Translations()
    {
        this.TranControls(this.cb_list,
                new string[][]{
                    new string []{"000599","会员"},
                    new string []{"000388","店铺"},
                    new string []{"000151","管理员"},
                    new string []{"006546","分公司"}});
        this.TranControls(this.BtnPermissionNormal, new string[][] { new string[] { "000434", "确定" } });

    }

    /// <summary>
    /// 确定按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnPermissionNormal_Click(object sender, EventArgs e)
    {
        string Operatenum = CommonDataBLL.OperateBh;
        string Operateip = CommonDataBLL.OperateIP;
        List<ListItem> list = new List<ListItem>();
        for (int i = 0; i < cb_list.Items.Count; i++)
        {
            ListItem item = new ListItem();
            item.Value=cb_list.Items[i].Value;
            item.Selected = cb_list.Items[i].Selected;
            list.Add(item);
        }

        if (BlackListBLL.SetLoginSystem(list,Operatenum,Operateip))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('" + GetTran("001401", "操作成功！") + "')</script>");
            return;
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('" + GetTran("001541", "操作失败！") + "')</script>");
            return;
        }
        getStatus();
    }
    //读取当前系统状态
    private void getStatus()
    {
        Translations();
        List<ListItem> list=BlackListBLL.GetLoginSystem();
        foreach (ListItem item in list)
        {
            switch (item.Text)
            {
                case "H": cb_list.Items[0].Selected = item.Selected; break;
                case "D": cb_list.Items[1].Selected = item.Selected; break;
                case "G": cb_list.Items[2].Selected = item.Selected; break;
                case "B": cb_list.Items[3].Selected = item.Selected; break;
            }
        }
    }
}
