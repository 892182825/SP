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

public partial class Company_DocPrint : BLL.TranslationBase 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ThreeRedirect(Page, Permissions.redirUrl);
        if (Request["DocID"]!=null)
        {
            string docid = Request["DocID"].ToString();
            string type = Request.Params["type"];
            string typename = "";
            if (type != null)
            {
                if (Request["type"].ToString() == "FH")
                {
                    typename = "发货单";
                }
            }
            UserControl_DocPrintbillOut usercontrol = Page.FindControl("DocPrintBillOut1") as UserControl_DocPrintbillOut;
            usercontrol.DocID = docid;
            usercontrol.TypeName = typename;
        }
    }
}
