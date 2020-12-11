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
using BLL.other;
using Model;
using BLL.other.Company;

public partial class UCCoutry : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //绑定国家信息列表框
        if (!IsPostBack)
        {
            Initddl();
        }
    }
    protected void Initddl()
    {
        //绑定国家信息到下拉列表框
        this.ddlCountry.DataSource = CountryBLL.GetCountryModels();
        //设定绑定显示为CountryModel.Name
        this.ddlCountry.DataTextField = "Name";
        //设定绑定值为CountryModel.ID
        this.ddlCountry.DataValueField = "ID";
        this.ddlCountry.DataBind();
    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
