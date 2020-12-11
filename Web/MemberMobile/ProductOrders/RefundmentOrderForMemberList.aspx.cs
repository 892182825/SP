using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_ProductOrders_RefundmentOrderForMemberList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string number = string.Empty;
        if (Session["Member"] == null)
            Response.End();
        else
            number = Session["Member"].ToString();
        Response.Redirect("~/Company/ProductOrders/RefundmentOrderForMemberList.aspx?number=" + number);
    }
}
