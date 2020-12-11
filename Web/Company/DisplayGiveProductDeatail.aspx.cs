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
using DAL;

public partial class Company_DisplayGiveProductDeatail : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        if (!IsPostBack)
        {
            if (null != Request["id"])
            {
                DataTable dt = GiveProductDAL.GetSetGivePVByID(Convert.ToInt32(Request["id"]));
                this.givDoc.DataSource = dt;
                this.givDoc.DataBind();

                this.givDocDitals.DataSource = GiveProductDAL.GetGiveProductBySID(Convert.ToInt32(Request["id"]));
                this.givDocDitals.DataBind();
            }
            else
            {
                ScriptHelper.SetAlert(Page, GetTran("000984", "该编号可能以不存在！！！"), "SetGivePVManager.aspx");
            }
        }
        Translations();
    }

    protected void givDoc_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
        }
        Translations();
    }

    /// <summary>
    /// 获取格林时间
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public string Getdatetime(object date)
    {
        if (string.IsNullOrEmpty(date.ToString()))
        {
            return GetTran("000221", "无");
        }
        DateTime dt = Convert.ToDateTime(date.ToString());
        return dt.AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToUniversalTime().ToString("yyyy-MM-dd");
    }

    private void Translations()
    {
        this.TranControls(this.givDoc,
               new string[][]{
                     new string []{"000022","删除"},
                    new string []{"000035","详细信息"},
                    new string []{"000000","赠送起始PV"},
                    new string []{"000000","赠送结束PV"},
                    new string []{"000000","操作人编号"},
                    new string []{"000000","操作时间"}});

        this.TranControls(this.givDocDitals,
            new string[][]{
                new string[]{"000558","产品编号"},
                new string[]{"000501","产品名称"},
                new string[]{"000505","数量"},
                new string[]{"000041","总金额"},
                new string[]{"000113","总积分"}
            });
    }

    protected void givDocDitals_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "xabc=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=xabc");
        }
    }
}
