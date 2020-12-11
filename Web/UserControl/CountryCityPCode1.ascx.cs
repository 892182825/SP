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

public partial class UserControl_CountryCityPCode1 : UserControlBase
{
    bool check = false;
    string MyId = "";
    string GetData = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
            MyId = ((UserControl_CountryCityPCode1)sender).ID.ToString();
            string changeguojia = "changguojia('" + this.MyId + "_ddlCountry','" + this.MyId + "_ddlP','" + this.MyId + "_ddlCity','" + this.MyId + "_ddlX','" + this.MyId + "_TextGuojia','" + this.MyId + "_TextShenFen','" + this.MyId + "_TextCity" + "','" + this.MyId + "_TextXian" + "');";
            string changesheng = "changsheng('" + this.MyId + "_ddlCountry','" + this.MyId + "_ddlP','" + this.MyId + "_ddlCity','" + this.MyId + "_ddlX','" + this.MyId + "_TextGuojia','" + this.MyId + "_TextShenFen','" + this.MyId + "_TextCity" + "','" + this.MyId + "_TextXian" + "');";
            string changecity = "changcity('" + this.MyId + "_ddlCountry','" + this.MyId + "_ddlP','" + this.MyId + "_ddlCity','" + this.MyId + "_ddlX','" + this.MyId + "_TextGuojia','" + this.MyId + "_TextShenFen','" + this.MyId + "_TextCity" + "','" + this.MyId + "_TextXian" + "');";
            string changexian = "changxian('" + this.MyId + "_ddlCountry','" + this.MyId + "_ddlP','" + this.MyId + "_ddlCity','" + this.MyId + "_ddlX','" + this.MyId + "_TextGuojia','" + this.MyId + "_TextShenFen','" + this.MyId + "_TextCity" + "','" + this.MyId + "_TextXian" + "');GetCCode('" + this.MyId + "_ddlCountry','" + this.MyId + "_ddlP','" + this.MyId + "_ddlCity','" + this.MyId + "_ddlX');";

            this.ddlCountry.Attributes.Add("onchange", changeguojia);
            this.ddlP.Attributes.Add("onchange", changesheng);
            this.ddlCity.Attributes.Add("onchange", changecity);
            this.ddlX.Attributes.Add("onchange", changexian);

            if (!check)
            {
                BindGuojia("");
                this.ddlP.Items.Insert(0, new ListItem("请选择", "请选择"));
                this.ddlCity.Items.Insert(0, new ListItem("请选择", "请选择"));
                this.ddlX.Items.Insert(0, new ListItem("请选择", "请选择"));
            }
        }
        else
        {
            string a = this.TextGuojia.Text + this.TextShenFen.Text + this.TextCity.Text;
            this.ddlP.Attributes.Add("draw", "Province:" + this.TextGuojia.Text);
            this.ddlCity.Attributes.Add("draw", "City:" + this.TextShenFen.Text + ":" + this.TextGuojia.Text);
            this.ddlX.Attributes.Add("draw", "Xian:" + this.TextXian.Text + "City:" + this.TextShenFen.Text + ":" + this.TextGuojia.Text);

            this.GetData += "<script>DropDownGetData('" + this.MyId + "_ddlCountry','" + this.TextGuojia.Text + "');</script>";
            this.GetData += "<script>DropDownGetData('" + this.MyId + "_ddlP','" + this.TextShenFen.Text + "');</script>";
            this.GetData += "<script>DropDownGetData('" + this.MyId + "_ddlCity','" + this.TextCity.Text + "');</script>";
            this.GetData += "<script>DropDownGetData('" + this.MyId + "_ddlX','" + this.TextXian.Text + "');</script>";
            this.SelectCountry(this.Country, this.Province, City, this.Xian);
        }

        //获取请求地址url
        string url = Request.Url.ToString();
        //获取页面名称
        string pageName = url.Substring(url.LastIndexOf('/') + 1).ToLower();
        //判断是否带参数了
        int spcialIndex = pageName.IndexOf('?');
        if (spcialIndex != -1)
            pageName = pageName.Substring(0, spcialIndex).ToLower();
        if (pageName == "registermember.aspx" || pageName == "applygoods.aspx" || pageName == "ordergoods.aspx" || pageName == "warehousedepotseat.aspx")
        {
            dv_cpc.Style.Remove("width");
        }
    }
    /// <summary>
    /// 绑定国家
    /// </summary>
    /// <param name="guojia"></param>
    private void BindGuojia(string guojia)
    {
        this.ddlCountry.DataSource = CountryBLL.GetCountryModels();
        this.ddlCountry.DataBind();
        //ddlCountry.SelectedIndex = 0;
        foreach (ListItem item in ddlCountry.Items)
        {
            if (item.Value == guojia)
            {
                item.Selected = true;
                break;
            }
        }

    }
    /// <summary>
    /// 检验是否选择
    /// </summary>
    /// <returns></returns>
    public bool CheckFill()
    {
        if (this.TextGuojia.Text == "" || this.TextGuojia.Text == "请选择")
        {
            return false;
        }
        else if (this.TextShenFen.Text == "" || this.TextShenFen.Text == "请选择")
        {
            return false;
        }
        else if (this.TextCity.Text == "" || this.TextCity.Text == "请选择")
        {
            return false;
        }
        else if (this.TextXian.Text == "" || this.TextXian.Text == "请选择")
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    /// <summary>
    /// 获取国家
    /// </summary>
    public string Country
    {
        get { return GetCountry(); }
        //不要添加 set 属性 请用SelectGuoJia('','','');
    }
    private string GetCountry()
    {
        return this.TextGuojia.Text;
    }
    /// <summary>
    /// 获取省份
    /// </summary>
    public string Province
    {
        get { return GetProvince(); }
        //不要添加 set 属性 请用SelectGuoJia('','','');
    }
    private string GetProvince()
    {
        return this.TextShenFen.Text;
    }
    /// <summary>
    /// 获取城市
    /// </summary>
    public string City
    {
        get { return GetCity(); }
        //不要添加 set 属性 请用SelectGuoJia('','','');
    }
    private string GetCity()
    {
        return this.TextCity.Text;
    }
    /// <summary>
    /// 获取县
    /// </summary>
    public string Xian
    {
        get { return GetXian(); }
        //不要添加 set 属性 请用SelectXian('','','');
    }
    private string GetXian()
    {
        return this.TextXian.Text;
    }

    public bool ReadOnly
    {
        set { setReadOnly(value); }
    }
    bool First = false;
    public bool State
    {
        set { First = value; }
        get { return First; }
        //设置FIRST属性,是否要执行重载
    }

    public void setReadOnly(bool isTrue)
    {
        this.ddlCountry.Enabled = isTrue;
        this.ddlP.Enabled = isTrue;
        this.ddlCity.Enabled = isTrue;
    }

    /// <summary>
    /// 根据国家省份城市的编码绑定控件
    /// </summary>
    /// <param name="code"></param>
    public void SetCityCode(string code)
    {
        DataSet ds = CountryBLL.GetCityCode(code);
        this.SelectCountry(ds.Tables[0].Rows[0]["country"].ToString(), ds.Tables[0].Rows[0]["province"].ToString(), ds.Tables[0].Rows[0]["city"].ToString(), ds.Tables[0].Rows[0]["xian"].ToString());
    }

    public void SelectCountry(string guojia, string shengfen, string city, string xian)
    {
        if (guojia == "")
        {
            guojia = "中国";
        }

        TextCity.Text = city;
        TextGuojia.Text = guojia;
        TextShenFen.Text = shengfen;
        TextXian.Text = xian;
        this.ddlCity.Items.Clear();
        this.ddlP.Items.Clear();
        this.ddlCountry.Items.Clear();
        this.ddlX.Items.Clear();
        BindGuojia(guojia);
        check = true;

        //选中国家
        //选中省份
        this.ddlP.DataSource = CountryBLL.GetProvince(guojia);
        this.ddlP.DataBind();
        ddlP.SelectedIndex = -1;
        ddlP.Items.Insert(0, "请选择");
        foreach (ListItem item1 in ddlP.Items)
        {
            if (item1.Value == shengfen)
            {
                item1.Selected = true;
                break;
            }
        }

        //选中省份
        //选中城市
        this.ddlCity.DataSource = CountryBLL.GetCitys(shengfen, guojia);
        this.ddlCity.DataBind();
        ddlCity.SelectedIndex = -1;
        ddlCity.Items.Insert(0, "请选择");
        foreach (ListItem item2 in ddlCity.Items)
        {
            if (item2.Value == city)
            {
                item2.Selected = true;
                break;
            }
        }

        //选中城市
        //选中县
        this.ddlX.DataSource = CountryBLL.GetXians(city, shengfen, guojia);
        this.ddlX.DataBind();
        ddlX.SelectedIndex = -1;
        ddlX.Items.Insert(0, "请选择");
        foreach (ListItem item3 in ddlX.Items)
        {
            if (item3.Value == xian)
            {
                item3.Selected = true;
                break;
            }
        }

        this.TextShenFen.Text = this.ddlP.SelectedValue;
        this.TextCity.Text = this.ddlCity.SelectedValue;
        this.TextXian.Text = this.ddlX.SelectedValue;
    }

    /// <summary>
    /// 绑定国家完国家信息后绑定省份
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlCountry_DataBound(object sender, EventArgs e)
    {
        this.ddlP.DataSource = CountryBLL.GetProvince(this.ddlCountry.Text);
        this.ddlP.DataBind();
        this.ddlCountry.Items.Insert(0, new ListItem("请选择", "请选择"));
    }

    /// <summary>
    /// 绑定城市信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlP_DataBound(object sender, EventArgs e)
    {
        this.ddlCity.DataSource = CountryBLL.GetCitys(this.ddlP.Text, this.ddlCountry.Text);
        this.ddlCity.DataBind();
    }
    protected void ddlCity_DataBound(object sender, EventArgs e)
    {
        //this.ddlCity.Items.Insert(0, new ListItem("请选择", "请选择"));
        this.ddlX.DataSource = CountryBLL.GetXians(this.ddlCity.Text, this.ddlP.Text, this.ddlCountry.Text);
        this.ddlX.DataBind();
    }
    protected void ddlXian_DataBound(object sender, EventArgs e)
    {
        //this.ddlX.Items.Insert(0, new ListItem("请选择"));
    }
}