﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class H5saoma_zhifu : System.Web.UI.Page
{
    public string erma = "";
    public string je = "";
    public string wz = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
    }
}