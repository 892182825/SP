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
using System.Collections.Generic;
using BLL;


public partial class UserControl_ExpectNum : System.Web.UI.UserControl
{
    protected bool isRun = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            IList<ConfigModel> configs = CommonDataBLL.GetVolumeLists();
            ddlExpectNum.DataSource = configs;
            ddlExpectNum.DataTextField = "date";
            ddlExpectNum.DataValueField = "ExpectNum";
            ddlExpectNum.DataBind();
            if (!isRun)
            {
                ddlExpectNum.Items.Add(new ListItem(new TranslationBase().GetTran("000633", "全部"), "-1"));
            }
        }
    }

    public void SetAutoPost(bool post)
    {
        this.ddlExpectNum.AutoPostBack = post;
    }

    public void Remove(ListItem item)
    {
        ddlExpectNum.Items.Remove(item);
    }

    public void Remove(int index)
    {
        ddlExpectNum.Items.RemoveAt(index);
    }

    public bool IsRun
    {
        get { return isRun; }
        set { isRun = value; }
    }

    public int ExpectNum
    {
        get { return ddlExpectNum.Items.Count == 0 ? CommonDataBLL.GetMaxqishu() : Convert.ToInt32(this.ddlExpectNum.SelectedValue.ToString()); }
        set { this.ddlExpectNum.SelectedValue = value.ToString(); }
    }

    public bool Enable
    {
        set { this.ddlExpectNum.Enabled = value; }
    }
}