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
using System.Data.SqlClient;
using DAL;


public partial class Company_ProductStock : BLL.TranslationBase
{
    protected string Flag;
    protected string msg;

    protected void Page_Load(object sender, System.EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        gvProduct.Attributes.Add("style", "word-break:keep-all;word-wrap:normal");
        Flag = Request.Params["Flag"].ToString();
        if (Flag == "WareHouse")
        {
            msg = GetTran("001943","仓库产品明细");
            Bind();
        }
        else
        {
            msg = GetTran("001945","店铺产品明细");
            BindStore_ProductDetail();
        }
        Translations_More();
    }

    /// <summary>
    /// Translation
    /// </summary>
    protected void Translations_More()
    {
        TranControls(gvProduct, new string[][]
                        {
                            new string[]{"000012","序号"},
                            new string[]{"000263","产品编码"},
                            new string[]{"000501","产品名称"},
                            new string[]{"000518","单位"},
                            new string[]{"000503","单价"},
                            new string[]{"000359","入库数量"},
                            new string[]{"000362","出库数量"},
                            new string[]{"000363","实际数量"},
                            new string[]{"001685","在途数量"}
                        }
                    );
    }

    protected void Bind()
    {
        SqlParameter[] param ={
									 new SqlParameter("@WareHouseID",SqlDbType.Int)
								 };
        param[0].Value = Convert.ToInt32(Session["Condition"].ToString());
        DataTable dt = new DataTable();
        dt = DBHelper.ExecuteDataTable("ProductQuantity_get", param, CommandType.StoredProcedure);
        this.lbl_storename.Visible = false;
        if (dt.Rows.Count < 1)
        {
            this.lbl_flag.Text = GetTran("001946","没有相关数据");
        }
        else
        {
            this.lbl_flag.Text = GetTran("000355","仓库名称") + ":" + dt.Rows[0][0].ToString();
        }
        this.lbl_title.Text = GetTran("000386","仓库");

        this.gvProduct.DataSource = dt;
        this.gvProduct.DataBind();
    }

    protected void BindStore_ProductDetail()
    {
        SqlParameter[] param ={
									 new SqlParameter("@Storeid",SqlDbType.VarChar)
								 };

        param[0].Value = Session["Condition"].ToString();

        DataTable dt2 = new DataTable();
        dt2 = DBHelper.ExecuteDataTable("D_Kucun_getData", param, CommandType.StoredProcedure);
        if (dt2.Rows.Count < 1)
        {
            this.lbl_flag.Text = GetTran("001946", "没有相关数据");

            return;
        }
        else
        {
            this.lbl_flag.Text = GetTran("000040","店铺名称") + ":" + Encryption.Encryption.GetDecipherName(Convert.ToString(dt2.Rows[0][0]));
        }
        this.lbl_title.Text = GetTran("000388","店铺");
        this.lbl_storename.Text = GetTran("000039","店长姓名") + "：" +Encryption.Encryption.GetDecipherName(Convert.ToString(dt2.Rows[0][1]));
        this.lbl_storename.Visible = true;
        this.gvProduct.DataSource = dt2;
        this.gvProduct.DataBind();
    }

    protected string getstr(string str)
    {
        if (str == "0")
        {
            return str;
        }
        else
        {
            int index = str.IndexOf(".");
            if (index == -1)
            {
                return str + ".00";
            }
            else
            {
                return str.Substring(0, index + 3);
            }
        }
    }

    protected void gvProduct_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex >= 0)
        {
            ((Label)e.Row.FindControl("lbl_code")).Text = Convert.ToString(e.Row.RowIndex+1);
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
        }
    }
}