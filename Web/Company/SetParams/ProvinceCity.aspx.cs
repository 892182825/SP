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

///Add Namespace
using System.Globalization;
using System.IO;
using System.Text;
using Model;
using BLL.other.Company;
using BLL.CommonClass;
using DAL;
/*
 * 创建者：     汪  华
 * 创建时间：   2009-09-17
 * 对应菜单:    系统管理->参数设置
 */

public partial class Company_SetParams_ProvinceCity : BLL.TranslationBase
{
    /// <summary>
    /// 实例化城市模型
    /// </summary>
    CityModel cityModel = new CityModel();

    /// <summary>
    /// 为GridView排序事件声明变量和赋值
    /// </summary>
    private static bool blID = true;
    private static bool blCountry = true;
    private static bool blProvince = true;
    private static bool blCity = true;
    private static bool blPostCode = true;

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, "../index.aspx");
        ///设置GridView的样式
        gvCity.Attributes.Add("style", "word-break:keep-all;word-wrap:normal");

        if (!Page.IsPostBack)
        {
            divCity.Visible = false;
            DataBindCity();
        }
        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.gvCity,
                new string[][]{
                    new string []{"000015","操作"},
                    new string []{"001195","编号"},
                    new string []{"000047","国家"},
                    new string []{"000109","省份"},
                    new string []{"000110","城市"},
                    new string []{"007526","区县"},
                    new string []{"000073","邮编"},
                    new string []{"003214","地区编码"}});
        this.TranControls(this.btnAdd, new string[][] { new string[] { "002047", "添 加" } });
        this.TranControls(this.btnReturn, new string[][] { new string[] { "000096", "返 回" } });
        this.TranControls(this.btnOK, new string[][] { new string[] { "002190", "确 定" } });
        this.TranControls(this.btnReset, new string[][] { new string[] { "001759", "清 空" } });

        this.TranControls(this.lblCountryName, new string[][] { new string[] { "000047", "国家" } });
        this.TranControls(this.Label1, new string[][] { new string[] { "003214", "地区编码" } });
        this.TranControls(this.RegularExpressionValidator2, new string[][] { new string[] { "003215", "6位数字" } });
        this.TranControls(this.lblProvice, new string[][] { new string[] { "000109", "省份" } });
        this.TranControls(this.lblCity, new string[][] { new string[] { "000110", "城市" } });
        this.TranControls(this.Label2, new string[][] { new string[] { "007526", "区县" } });
        this.TranControls(this.lblPostCode, new string[][] { new string[] { "000073", "邮编" } });
    }

    /// <summary>
    /// 绑定数据
    /// </summary>
    protected void DataBindCity()
    {
        ///获取城市信息       
        DataTable dtCity = SetParametersBLL.GetCityInfo();
        ViewState["sortCity"] = dtCity;
        DataView dv = new DataView((DataTable)ViewState["sortCity"]);
        this.gvCity.DataSource = dv;
        this.gvCity.DataBind();

        string condition = "1<2";
        if (TextBox5.Text.Trim().Length > 0)
        {
            condition += " and Country='" + TextBox5.Text.Trim() + "'";
        }
        if (TextBox2.Text.Trim().Length > 0)
        {
            condition += " and Province='" + TextBox2.Text.Trim() + "'";
        }
        if (TextBox3.Text.Trim().Length > 0)
        {
            condition += " and City='" + TextBox3.Text.Trim() + "'";
        }
        if (TextBox4.Text.Trim().Length > 0)
        {
            condition += " and Xian='" + TextBox4.Text.Trim() + "'";
        }

        string columns = "ID,Country,Province,City,Xian,PostCode,FullName,Abridge,CPCCode";
        string table = "City";
        string key = "ID";

        ViewState["SQLSTR"] = "select " + columns + " from " + table + " where " + condition + " order by " + key + " desc";

        ///分页
        Pager page = Page.FindControl("uclPager") as Pager;
        page.PageBind(0, 10, table, columns, condition, key, "gvCity");
        Translations();
    }

    /// <summary>
    /// 返回添加参数页面
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("../SetParameters.aspx");
    }
    
    /// <summary>
    /// 清空内容
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ResetAll();
    }

    /// <summary>
    /// 清空文本框
    /// </summary>
    public void ResetAll()
    {
        txtProvice.Text = "";
        txtCity.Text = "";
        TextBox1.Text = "";
        txtPostCode.Value = "";
        txt_cpccode.Text = "";
    }

    /// <summary>
    /// 添加城市记录
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        divCity.Visible = true;
        ResetAll();
        ViewState["ID"] = 1;
    }

    protected void gvCity_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            int primaryKey = Convert.ToInt32(this.gvCity.DataKeys[e.Row.RowIndex].Value.ToString());
            ((LinkButton)e.Row.FindControl("lbtEdit")).CommandArgument = primaryKey.ToString();
            ((LinkButton)e.Row.FindControl("lbtDelete")).CommandArgument = primaryKey.ToString();
        }
        ///控制GridView样式
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
        }
        Translations();
    }

    /// <summary>
    /// 删除指定的城市信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtDelete_Command(object sender, CommandEventArgs e)
    {
        int cityID = Convert.ToInt32(e.CommandArgument);

        //Judge the CityId whether has operation by Id before delete
        int getCount = SetParametersBLL.CityIdWhetherHasOperation(cityID);
        if (getCount > 0)
        {
            divCity.Visible = false;
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("003250", "对不起，该城市已经发生了业务，因此不能删除！")));
            return;
        }
        else
        {
            //Judge the CityID whether exist by Id before delete or update
            int isExistCount = SetParametersBLL.CityIdIsExist(cityID);
            if (isExistCount > 0)
            {
                BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("City", "ID");
                cl_h_info.AddRecord(cityID);
                ///删除指定城市信息
                int delCount = SetParametersBLL.DelCityByID(cityID);
                if (delCount > 0)
                {
                    cl_h_info.DeletedIntoLogs(ChangeCategory.company28, Session["Company"].ToString(), ENUM_USERTYPE.objecttype9);
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("003249", "删除城市成功!")));
                }

                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("003248", "删除城市失败，请联系管理员!")));
                    return;
                }
            }
            else
            {
                divCity.Visible = false;
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("003244", "对不起，该城市不存在或者已经被删除!")));
                return;
            }
        }

        DataBindCity();
    }

    /// <summary>
    /// 修改指定的城市信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtEdit_Command(object sender, CommandEventArgs e)
    {
        divCity.Visible = true;
        ViewState["ID"] = 2;
        ViewState["CityID"] = e.CommandArgument;

        int cityID = Convert.ToInt32(e.CommandArgument);

        //Judge the CityID whether exist by Id before delete or update
        int isExistCount = SetParametersBLL.CityIdIsExist(cityID);
        if (isExistCount > 0)
        {
            //获取指定的城市信息
            DataTable dt = SetParametersBLL.GetCityInfoByID(cityID);

            if (dt.Rows.Count == 0)
            {
                divCity.Visible = false;
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("003246", "对不起，该城市所对应的国家不存在！")));
                return;
            }
            else
            {
                (ddlCountry.Controls[0] as DropDownList).SelectedValue = Convert.ToString(dt.Rows[0][0]);
                txtProvice.Text = Convert.ToString(dt.Rows[0][1]);
                txtCity.Text = Convert.ToString(dt.Rows[0][2]);
                TextBox1.Text = Convert.ToString(dt.Rows[0][3]);
                txt_cpccode.Text = dt.Rows[0][4].ToString().Substring(2, dt.Rows[0][4].ToString().Length - 2);
                txtPostCode.Value = dt.Rows[0][5].ToString();
            }
        }
        else
        {
            divCity.Visible = false;
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("003244", "对不起，该城市不存在或者已经被删除!")));
            return;
        }
    }

    /// <summary>
    /// 给模型层赋值
    /// </summary>
    protected bool SetValueCityModel(string type)
    {
        if (txtProvice.Text.Trim() == "" || txtCity.Text.Trim() == "" || txt_cpccode.Text.Trim() == "")
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("003242", "省份名称、城市名和地区编码不能为空!")));
            return false;
        }
        else
        {
            cityModel.Id = Convert.ToInt32(ViewState["CityID"]);
            cityModel.Country = (ddlCountry.Controls[0] as DropDownList).SelectedItem.Text;
            string str = DBHelper.ExecuteScalar("select countrycode from country where name='" + cityModel.Country + "'").ToString();
            cityModel.Province = txtProvice.Text.Trim();
            cityModel.City = txtCity.Text.Trim();
            cityModel.Xian = TextBox1.Text.Trim();
            cityModel.Postcode = txtPostCode.Value;
            cityModel.CPCCode = str + txt_cpccode.Text;
            if (type != "2")
            {
                if ((int)DBHelper.ExecuteScalar("select count(1) from city where CPCCode='" + cityModel.CPCCode + "'") > 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("003241", "该地区编码已存在!")));
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }
    }

    /// <summary>
    /// 添加城市信息
    /// </summary>
    protected void AddCityInfo()
    {
        //添加城市信息
        int addCount = SetParametersBLL.AddCity(cityModel);
        if (addCount > 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("003240", "添加城市成功!")));
            divCity.Visible = false;
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("003235", "添加城市失败,请联系管理员!")));
        }
    }

    /// <summary>
    /// 修改城市信息
    /// </summary>
    protected void UpdCityInfo()
    {
        ///修改指定城市信息
        BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("City", "ID");
        cl_h_info.AddRecord(cityModel.Id);

        int updCount = SetParametersBLL.UpdCityByID(cityModel);
        if (updCount > 0)
        {
            cl_h_info.AddRecord(cityModel.Id);
            cl_h_info.ModifiedIntoLogs(ChangeCategory.company28, Session["Company"].ToString(), ENUM_USERTYPE.objecttype9);

            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("003234", "修改城市成功!")));
            divCity.Visible = false;
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("003233", "修改城市失败,请联系管理员!")));
        }
    }

    /// <summary>
    /// 提交事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        bool flag = SetValueCityModel(ViewState["ID"].ToString());
        if (flag)
        {
            if ((int)ViewState["ID"] == 1)
            {
                ///通过国家省份获取省份行数
                int getCount = SetParametersBLL.GetCityProvinceCountByCountryProvince((ddlCountry.Controls[0] as DropDownList).SelectedItem.Text, txtProvice.Text.Trim());
                if (getCount > 0)
                {
                    ///通过国家省份城市获取城市行数
                    int getCout1 = SetParametersBLL.GetCityCityCountByCountryProvinceCity((ddlCountry.Controls[0] as DropDownList).SelectedItem.Text, txtProvice.Text.Trim(), txtCity.Text.Trim(), TextBox1.Text.Trim());
                    if (getCout1 > 0)
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("003230", "对不起，该城市已经存在!")));
                    }
                    else
                    {
                        ///添加城市信息
                        AddCityInfo();
                    }
                }
                else
                {
                    ///添加城市信息
                    AddCityInfo();
                }
            }

            if ((int)ViewState["ID"] == 2)
            {
                ///通过ID,国家，省份获取行数  
                int getProvinceCount = SetParametersBLL.GetCityProvinceCountByIDCountryProvince(cityModel);
                if (getProvinceCount > 0)
                {
                    ///通过ID,国家，省份,城市获取行数
                    int getCityCount1 = SetParametersBLL.GetCityCityCountByIDCountryProvinceCity(cityModel);
                    if (getCityCount1 > 0)
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("003230", "对不起，该城市已经存在!")));
                    }
                    else
                    {
                        ///修改城市信息
                        UpdCityInfo();
                    }
                }

                else
                {
                    ///修改城市信息
                    UpdCityInfo();
                }
            }
            DataBindCity();
        }

        else
        {
            return;
        }
    }

    /// <summary>
    /// 导出到Excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        //DataTable dt = SetParametersBLL.OutToExcel_City();
        DataTable dt = DAL.DBHelper.ExecuteDataTable(ViewState["SQLSTR"].ToString());
        if (dt == null || dt.Rows.Count < 1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001712", "对不起，没有可以导出的数据!")));
            return;
        }
        else
        {
            Excel.OutToExcel(dt, GetTran("003227", "国家身份城市信息"), new string[] { "Country=" + GetTran("000047", "国家"), "Province=" + GetTran("000109", "省份"), "City=" + GetTran("000110", "城市"), "Xian=" + GetTran("007526", "区县"), "PostCode=" + GetTran("000073", "邮编") });
        }
       
    }

    /// <summary>
    /// 针对导出Excel重载
    /// </summary>
    /// <param name="control"></param>
    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }

    /// <summary>
    /// GridView排序事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvCity_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataView dv = new DataView((DataTable)ViewState["sortCity"]);
        string sortString = e.SortExpression;
        bool flag = true;

        switch (sortString.ToLower().Trim())
        {
            case "id":
                if (blID)
                {
                    dv.Sort = "ID desc";
                    flag = blID = false;
                }
                else
                {
                    dv.Sort = "ID asc";
                    flag = blID = true;
                }
                break;
            case "country":
                if (blCountry)
                {
                    dv.Sort = "Country desc";
                    flag = blCountry = false;
                }
                else
                {
                    dv.Sort = "Country asc";
                    flag = blCountry = true;
                }
                break;
            case "province":
                if (blProvince)
                {
                    dv.Sort = "Province desc";
                    flag = blProvince = false;
                }
                else
                {
                    dv.Sort = "Province asc";
                    flag = blProvince = true;
                }
                break;
            case "city":
                if (blCity)
                {
                    dv.Sort = "City desc";
                    flag = blCity = false;
                }
                else
                {
                    dv.Sort = "City asc";
                    flag = blCity = true;
                }
                break;
            case "xian":
                if (blCity)
                {
                    dv.Sort = "Xian desc";
                    flag = blCity = false;
                }
                else
                {
                    dv.Sort = "Xian asc";
                    flag = blCity = true;
                }
                break;
            case "postcode":
                if (blPostCode)
                {
                    dv.Sort = "PostCode desc";
                    flag = blPostCode = false;
                }
                else
                {
                    dv.Sort = "PostCode asc";
                    flag = blPostCode = true;
                }
                break;
        }

        this.gvCity.DataSource = dv;
        this.gvCity.DataBind();

        string condition = "1<2";
        if (TextBox5.Text.Trim().Length > 0)
        {
            condition += " and Country='" + TextBox5.Text.Trim() + "'";
        }
        if (TextBox2.Text.Trim().Length > 0)
        {
            condition += " and Province='" + TextBox2.Text.Trim() + "'";
        }
        if (TextBox3.Text.Trim().Length > 0)
        {
            condition += " and City='" + TextBox3.Text.Trim() + "'";
        }
        if (TextBox4.Text.Trim().Length > 0)
        {
            condition += " and Xian='" + TextBox4.Text.Trim() + "'";
        }

        ///按照指定的条件进行排序
        Pager page = Page.FindControl("uclPager") as Pager;
        page.PagingSort(0, 10, " City", "ID,Country,Province,City,Xian,PostCode,FullName,Abridge,CPCCode", condition, sortString.ToLower().Trim(), flag, "gvCity");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        DataBindCity();
    }
}