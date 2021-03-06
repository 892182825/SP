﻿using System;
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

public partial class UserControl_SearchPlacement : System.Web.UI.UserControl
{
    #region 属性

    /// <summary>
    /// 推荐
    /// </summary>
    public string Direct
    {
        get { return Txttj.Text; }
        set { Txttj.Text = value; }
    }

    /// <summary>
    /// 安置
    /// </summary>
    public string Placement
    {
        get { return txtPlaceMent.Text; }
        set { txtPlaceMent.Text = value; }
    }

    public int QuShu
    {
        get { return Convert.ToInt32(txtQuShu.Text); }
        set { txtQuShu.Text = value.ToString(); }
    }

    #endregion

    #region Page_Load

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #endregion

    #region 方法

    protected string GetStoreID()
    {
        return "";
    }

    protected string GetTopBh()
    {
        string tBh = "";
        if (Session["Store"] != null)
        {
            tBh = BLL.CommonClass.CommonDataBLL.getStoreNumber(Session["Store"].ToString());
        }
        else if (Session["Member"] != null)
        {
            tBh = Session["Member"].ToString();
        }
        else
        {
            tBh = BLL.CommonClass.CommonDataBLL.getManageID(3);
        }
        return tBh;
    }

    protected string LoginType()
    {
        if (Session["Company"] != null)
        {
            return "Company";
        }
        else if (Session["Store"] != null)
        {
            return "Store";
        }
        else
        {
            return "Member";
        }
    }

    protected string GetTopBh2()
    {
        string tBh = "";
        tBh = this.Txttj.Text.Trim();
        return tBh;
    }

    #endregion 
}
