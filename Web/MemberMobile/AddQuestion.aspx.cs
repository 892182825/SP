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
using BLL.Logistics;
using DAL;

public partial class Store_AddQuestion : BLL.TranslationBase
{
    protected string msg;

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        if (!IsPostBack)
        {
            string orderid=GetOrderId();
            txtQuestion.Text=DBHelper.ExecuteScalar("select feedback from storeorder where storeorderid='" + orderid + "'") + "";
        }
        Translations();
    }

    private void Translations()
    {
        this.TranControls(this.btnAdd, new string[][] { new string[] { "000321", "提交" } });
        this.TranControls(this.btnAdd, new string[][] { new string[] { "000321", "提交" } });
    } 
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (AffirmConsignBLL.AddQuestion(GetOrderId(), txtQuestion.Text))
        {
            msg = "<script>alert('" + GetTran("001881", "提交成功！") + "');window.navigate('Joinreceipt.aspx');</script>";
        }
        else
        {
            msg = "<script>alert('" + GetTran("000302", "提交失败！") + "');</script>";
        }
    }

    private string GetOrderId()
    {
        if (Request.QueryString["orderId"] != null)
        {
            return Request.QueryString["orderid"].ToString();
        }
        else
        {
            return "";
        }
    }
}
