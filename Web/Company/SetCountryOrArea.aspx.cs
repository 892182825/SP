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

//Add Namespace
using System.Text;
using Model;
using Model.Other;
using BLL.other.Company;
using BLL.CommonClass;

/*
 * Author:      WangHua
 * FinishDate:  2010-02-22
 */

public partial class Company_SetCountryOrArea : BLL.TranslationBase 
{
    /// <summary>
    /// 为GridView排序事件声明变量和赋值
    /// </summary>
    private static bool blID = true;
    private static bool blName = true;
    private static bool blRatename = true;

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        ///检查相应权限
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(6315);

        ///设置GridView的样式
        gvCountry.Attributes.Add("style", "word-break:keep-all;word-wrap:normal");

        if (!IsPostBack)
        {
            GetAllCurrencyIDName();
            GetCountryInfo();

            DropDownList1.DataTextField = "country";
            DropDownList1.DataValueField = "id";
            DropDownList1.DataSource = CountryBLL.GetContry();
            DropDownList1.DataBind();
            
        }
        Translations();
    }

    private void Translations()
    {
        this.TranControls(this.btnAddNewCountry, new string[][] { new string[] { "002047", "添 加" } });
        this.TranControls(this.gvCountry,
                new string[][]{
                    new string []{"000022","删除"},
                    new string []{"002112","国家编号"},
                    new string []{"002112","国家编码"},
                    new string []{"002107","国家简称"},
                    new string []{"001026","国家"},
                    new string []{"002113","使用币种"}                   
                });
    }

    protected void GetAllCurrencyIDName()
    {
        ///获取汇率表中所有的ID和币种名称
        DropNewCurrency.DataSource = SetRateBLL.GetAllCurrencyIDName();
        DropNewCurrency.DataTextField = "Name";
        DropNewCurrency.DataValueField = "ID";
        DropNewCurrency.DataBind();
    }

    /// <summary>
    /// 获取国家信息
    /// </summary>
    private void GetCountryInfo()
    {
        ///通过联合查询获取更多国家信息
        DataTable dt = SetRateBLL.GetMoreCountryInfo();
        ViewState["sortlanguage"] = dt;
        if (ViewState["sortlanguagestring"] == null)
            ViewState["sortlanguagestring"] = "name asc";
        DataView dv = new DataView((DataTable)ViewState["sortlanguage"]);
        dv.Sort = ViewState["sortlanguagestring"].ToString();
        gvCountry.DataSource = dv;
        gvCountry.DataBind();
        Translations();
    }

    private void ResetValue()
    {
        txtCountryCode.Value = "";
    }

    /// <summary>
    /// 添加国家
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddNewCountry_Click(object sender, EventArgs e)
    {
        ///通过国家名称获取行数
        int getCount = SetRateBLL.GetCountryCountByCountryName(CountryBLL.GetCountryName(DropDownList1.SelectedValue));
        if (getCount > 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002142", "国家名称已经存在!")));
            return;
        }

        getCount = SetRateBLL.GetCountryCountByRateID(Convert.ToInt32(this.DropNewCurrency.SelectedValue));

        if (txtCountryCode.Value.Trim()== "")
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002144", "对不起，国家编码不能为空!")));
            return;
        }

        else
        {
            int ccCount = SetRateBLL.CountryCodeIsExist(txtCountryCode.Value.Trim());
            if (ccCount > 0)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002145", "对不起，该国家编码已经存在!")));
                return;
            }
        }

        ///实例化国家模型
        CountryModel countryModel = new CountryModel();
        countryModel.CountryCode = txtCountryCode.Value.Trim();
        countryModel.CountryForShort = CountryBLL.GetCountryShortName(DropDownList1.SelectedValue);
        countryModel.Name = CountryBLL.GetCountryName(DropDownList1.SelectedValue);
        countryModel.RateID = Convert.ToInt32(DropNewCurrency.SelectedItem.Value);
        ///向国家表中插入相关记录
        int addCount = SetRateBLL.AddCountry(countryModel);
        if (addCount > 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002146", "添加国家成功!")));
            ResetValue();
        }

        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002149", "添加国家失败，请联系管理员!")));
            return;
        }     

        GetAllCurrencyIDName();
        GetCountryInfo();
    }

    /// <summary>
    /// 国家行绑定(获取国家ID)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvCountry_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            int countryID = Convert.ToInt32(this.gvCountry.DataKeys[e.Row.RowIndex].Value.ToString());
            ((LinkButton)e.Row.FindControl("lbtnDelCountry")).CommandArgument = countryID.ToString();
        }

        ///控制GridView样式
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
        }
    }

    /// <summary>
    /// 删除国家
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvCountry_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DelCountry")
        {
            int Id = Convert.ToInt32(e.CommandArgument);
            //Judge the CountryCode whether has operation before delete by Id
            int getCount = SetRateBLL.CountryCodeWhetherHasOperation(Id);
            if (getCount > 0)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002151","该国家已经发生了业务，因此不能删除!")));
                return;
            }

            else
            {
                BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("Country", "Id");
                cl_h_info.AddRecord(Id);
                ///删除指定的国家记录                     
                int delCount = SetRateBLL.DelCountryByID(Id);
                if (delCount > 0)
                {
                    cl_h_info.DeletedIntoLogs(BLL.CommonClass.ChangeCategory.company38, Session["Company"].ToString(), BLL.CommonClass.ENUM_USERTYPE.objecttype9);
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002154","删除国家成功!")));
                }

                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002156", "删除国家失败,请联系管理员！")));
                }
            }
            GetCountryInfo();
        }
    }

    /// <summary>
    /// 针对导出Excel重载
    /// </summary>
    /// <param name="control"></param>
    public override void VerifyRenderingInServerForm(Control control)
    {
        
    } 

    /// <summary>
    /// 导出国家到Excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExcelCountry_Click(object sender, EventArgs e)
    {
        DataTable dt = SetRateBLL.OutToExcel_Country();
        if (dt.Rows.Count < 1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001712", "没有可以导出的数据!")));
            return;
        }

        else
        {
            Excel.OutToExcel1(dt, GetTran("002157", "国家信息"), new string[] { "CountryCode="+GetTran("002106","国家编码"), "CountryForShort="+GetTran("002107","国家简称"), "Name="+GetTran("001026","国家名称"), "CurrencyName="+GetTran("002113","使用币种") });
        }  

    }   

    /// <summary>
    /// GridView排序事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvCountry_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataView dv = new DataView((DataTable)ViewState["sortlanguage"]);
        string sortString = e.SortExpression;

        switch (sortString.ToLower().Trim())
        {
            case "id":
                if (blID)
                {
                    dv.Sort = "ID desc";
                    blID = false;
                }

                else
                {
                    dv.Sort = "ID asc";
                    blID = true;
                }
                break;

            case "[name]":
                if (blName)
                {
                    dv.Sort = "[Name] desc";
                    blName = false;
                }

                else
                {
                    dv.Sort = "[Name] asc";
                    blName = true;
                }
                break;

            case "ratename":
                if (blRatename)
                {
                    dv.Sort = "Ratename desc";
                    blRatename = false;
                }

                else
                {
                    dv.Sort = "Ratename";
                    blRatename = true;
                }
                break;
        }
        this.gvCountry.DataSource = dv;
        this.gvCountry.DataBind();
    }
}
