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

//Add Namespace
using System.Data.SqlClient;
using BLL.other.Company;
using Model;

/*
 * 完成时间：2009-11-18
 * 完成者：  汪  华
 */

public partial class Company_ProductStockDetail : BLL.TranslationBase
{
    protected static DataTable dt;
    protected static string Flag;
    protected static string msg;
	
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        ///设置GridView的样式
        gvWareHouse.Attributes.Add("style", "word-break:keep-all;word-wrap:normal");
        gvStore.Attributes.Add("style", "word-break:keep-all;word-wrap:normal");

        this.lbl_message.Visible = false;
        if (!this.IsPostBack)
        {
            Bind();
        }
        Translations_More();
    }

    /// <summary>
    /// Translation
    /// </summary>
    protected void Translations_More()
    {
        TranControls(gvWareHouse,new string[][]
                        {
                             new string[]{"000263","产品编码"},
                              new string[]{"000501","产品名称"},
                                new string[]{"000355","仓库名称"},
                                new string[]{"000357","库位名称"},
                                new string[]{"000359","入库数量"},
                                new string[]{"000362","出库数量"},
                                new string[]{"000363","实际数量"},
                                new string[]{"000365","预警数量"}
                        }
                    );

        TranControls(gvStore, new string[][]
                        {
                                new string[]{"000040","店铺名称"},
                                new string[]{"000039","店长姓名"},
                                 new string[]{"000263","产品编码"},
                                new string[]{"000359","入库数量"},
                                new string[]{"000362","出库数量"},
                                new string[]{"000363","实际数量"},
                                new string[]{"001685","在途数量"}
                        }
            );
    }

    private void Bind()
    {
        Flag = Request.Params["Flag"];
        if (Flag == "Company")
        {
            msg = GetTran("000290", "产品仓库明细");
            dt = ProductWareHouseDetailsBLL.GetMoreWareHouseProductInfoByProductCode(Session["ProductCode"].ToString());
            this.lbl_title.Text = GetTran("001824", "公司");
            if (dt.Rows.Count < 1)
            {
                this.lbl_message.Text = GetTran("000380", "没有此产品的库存明细信息"); 
                this.lbl_message.Visible = true;
            }

            else
            {
                this.lbl_message.Visible = false;
            }

            this.gvWareHouse.DataSource = dt;
            this.gvWareHouse.DataBind();
        }

        if (Flag == "Store")
        {
            msg = GetTran("000295", "产品店铺明细"); 
            dt = ProductWareHouseDetailsBLL.GetMoreStoreProductInfoByProductCode(Session["ProductCode"].ToString());
            this.lbl_title.Text = GetTran("000388", "店铺");
            if (dt.Rows.Count < 1)
            {
                this.lbl_message.Text = GetTran("000380", "没有此产品的库存明细信息");
                this.lbl_message.Visible = true;
            }

            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["StoreName"] = Encryption.Encryption.GetDecipherName(Convert.ToString(dt.Rows[i]["StoreName"]));
                    dt.Rows[i]["Name"] = Encryption.Encryption.GetDecipherName(Convert.ToString(dt.Rows[i]["Name"]));
                }
                this.lbl_message.Visible = false;
            }

            this.gvStore.DataSource = dt;
            this.gvStore.DataBind();
        }
    }

    /// <summary>
    /// 行绑定事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvWareHouse_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ///控制GridView样式
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
        }
    }

    /// <summary>
    /// 行绑定事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvStore_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ///控制GridView样式
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Flag = Request.Params["Flag"];
        if (Flag == "Company")
        {
            Response.Redirect("ProductWareHouseDetails.aspx?Flag=3");
        }
        if (Flag == "Store")
        {
            Response.Redirect("ProductWareHouseDetails.aspx?Flag=4");
        }
    }
}
