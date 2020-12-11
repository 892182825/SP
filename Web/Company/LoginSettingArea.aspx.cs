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

public partial class AreaBlackGroup : BLL.TranslationBase
{
    protected string msg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(Model.Other.EnumCompanyPermission.SafeLoginSettingArea);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        if (!IsPostBack)
        {
            PageSet();
            this.btAddToBlackList.Attributes.Add("onClick", "return confirm('" + GetTran("000914", "是否确定删除该黑名单") + "');");
        }
        Translations();
    }

    private void Translations()
    {
        this.TranControls(this.gvBlackArea,
                new string[][]{
                    new string []{"000504","是否删除"},
                    new string []{"000047","国家"},
                    new string []{"000109","省份"},
                    new string []{"000110","城市"}});
        this.TranControls(this.Add_blackList, new string[][] { new string[] { "000466", "添加到黑名单" } });
        this.TranControls(this.btAddToBlackList, new string[][] { new string[] { "000800", "删除黑名单" } });
    }


    /// <summary>
    /// 设置获取区域管辖黑名单的加载类
    /// </summary>
    protected void PageSet()
    {
        //初始化分页帮助类
        this.Pager1.PageBind(0, 10, "BlackGroup", "GroupValue,BlackGroup.id", " GroupType=3 ", " BlackGroup.id ", "gvBlackArea");
        Translations();
        if (((DataTable)gvBlackArea.DataSource).Rows.Count == 0)
        {
            divChkAll.Visible = false;
        }
    }
    /// <summary>
    /// 删除当前系统区域管辖黑名单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btAddToBlackList_Click(object sender, EventArgs e)
    {
        int m = 0;
        int n = 0;
        foreach (GridViewRow row in gvBlackArea.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkSelectRow");
                if (chk.Checked)
                {
                    HiddenField hid = (HiddenField)row.FindControl("hidID");
                    if (BlackGroupBLL.DelBlackGroup(int.Parse(hid.Value), 3) < 0)
                    {
                        n++;
                    }
                    m++;
                }
            }
        }
        if (m == 0)
        {
            ScriptHelper.SetAlert(out msg, GetTran("000865", "请用户选择要删除的区域管辖黑名单."));
        }
        else if (n == 0)
        {
            ScriptHelper.SetAlert(out msg, GetTran("000872", "删除指定区域管辖成功."));
            PageSet();
        }
        else
        {
            ScriptHelper.SetAlert(out msg, GetTran("000875", "删除部分区域管辖成功."));
            PageSet();
        }
    }

    protected string GetArea(object obj, int i)
    {
        if (obj == null)
        {
            return "";
        }
        else
        {
            CityModel model = CommonDataDAL.GetCPCCode(obj.ToString());
            string areas = model.Country + "," + model.Province + "," + model.City + "," + model.Xian;
            string[] area = areas.Split(',');
            if (area.Length != 4)
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
        //验证用户填写黑名单区域管辖
        string country = CountryCity1.Country;
        string p = CountryCity1.Province;
        string city = CountryCity1.City;
        string xian = CountryCity1.Xian;
        if (!this.CountryCity1.CheckFill())
        {
            ScriptHelper.SetAlert(out msg, GetTran("000886", "必须提供有效的区域"));
            CountryCity1.SelectCountry(country, p, city, xian);
            return;
        }
        string area = country + "," + p + "," + city + "," + xian;
        area = area.Replace(" ", "");      
        string[] areas = area.Split(',');
        if(areas.Length!=4)
        {
            ScriptHelper.SetAlert(out msg, GetTran("000887", "必须提供有效的区域!"));
            CountryCity1.SelectCountry(country, p, city, xian);
            return;
        }
        //判断是否已经存在该黑名单区域管辖 3代表区域管辖类型
        string cpcode = CommonDataDAL.GetCPCCode(country,p,city,xian);
        if (BlackGroupBLL.HasBlackGroup(cpcode, 3))
        {
            //提示已经存在,无需填写
            ScriptHelper.SetAlert(out msg, GetTran("000890", "指定的黑名单区域已经存在，无法增加!"));
            CountryCity1.SelectCountry(country, p, city, xian);
            return;
        }
        BlackGroupModel blackGroup = new BlackGroupModel();
        //区域管辖类型
        blackGroup.IntGroupType = 3;
        blackGroup.IntGroupValue = cpcode;
        string operateIP = Request.UserHostAddress.ToString();
        string operateNum = Session["Company"].ToString();
        switch (BlackGroupBLL.AddBlackGroup(blackGroup, operateIP, operateNum))
        {
            case 1:
                ScriptHelper.SetAlert(out msg, GetTran("000891", "添加成功!"));
                PageSet();
                break;
            default:
                ScriptHelper.SetAlert(out msg, GetTran("000893", "执行发生错误"));
                break;
        }
    }
    /// <summary>
    /// 调用排序方法
    /// </summary>
    /// <param name="sender">排序方法触发控件</param>
    /// <param name="e">为排序方法提供必要数据</param>
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {

    }

    protected void gvBlackArea_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC';");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg;");
        }
    }
}
