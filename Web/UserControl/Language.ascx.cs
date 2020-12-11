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
using BLL.CommonClass;
using Model;

public partial class UserControl_Language : System.Web.UI.UserControl
{
    //CommonDataBLL CommonDataBLL = new CommonDataBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlLanaguage.DataSource = CommonDataBLL.GetLanaguage();
            ddlLanaguage.DataValueField = "ID";
            ddlLanaguage.DataTextField = "Name";
            ddlLanaguage.DataBind();
        }
    }
    private int id;

    public int Id
    {
        get 
        {
            return Convert.ToInt32(ddlLanaguage.SelectedItem.Value);
        }
        set 
        {
            ddlLanaguage.SelectedItem.Value = value.ToString() ;
        }
    }


   // private string name;

    public string Name
    {
        get
        {
            return ddlLanaguage.SelectedItem.Text;
        }
       // set { name = value; }
    }
}
