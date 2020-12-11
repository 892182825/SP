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

///Add Namespace
using System.Globalization;
using System.IO;
using System.Text;
using Model;
using BLL.other.Company;
using BLL.CommonClass;

/*
 * 创建者：     汪  华
 * 创建时间：   2009-09-16
 * 对应菜单:    系统管理->参数设置
 */

public partial class Company_SetParams_ProductSize : BLL.TranslationBase
{
    /// <summary>
    /// 实例化产品尺寸模型
    /// </summary>
    ProductSizeModel productSizeModel = new ProductSizeModel();

    /// <summary>
    /// 为GridView排序事件声明变量和赋值
    /// </summary>
    private static bool blProductSizeID = true;
    private static bool blProductSizeName = true;
    private static bool blProductSizeDescr = true;

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, "../index.aspx");
        ///设置GridView的样式
        gvProductSize.Attributes.Add("style", "word-break:keep-all;word-wrap:normal");

        if (!Page.IsPostBack)
        {
            divProductSize.Visible = false;
            DataBindProductSize();
            ViewState["ID"] = 0;
        }
        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.gvProductSize,
                new string[][]{
                    new string []{"000015","操作"},
                    new string []{"002270","产品尺寸编号"},
                    new string []{"002268","产品尺寸名称"},
                    new string []{"002269","产品尺寸说明"}});
        this.TranControls(this.btnAdd, new string[][] { new string[] { "002047", "添 加" } });
        this.TranControls(this.btnReturn, new string[][] { new string[] { "000096", "返 回" } });
        this.TranControls(this.btnOK, new string[][] { new string[] { "002190", "确 定" } });
        this.TranControls(this.btnReset, new string[][] { new string[] { "001759", "清 空" } });

        this.TranControls(this.lblProductSizeName, new string[][] { new string[] { "002268", "产品尺寸名称：" } });
        this.TranControls(this.lblProductSizeDescr, new string[][] { new string[] { "002269", "产品尺寸说明：" } });

    }
    /// <summary>
    /// 绑定数据
    /// </summary>
    protected void DataBindProductSize()
    {
        ///获取产品尺寸信息       
        DataTable dtProductSize = SetParametersBLL.GetProductSizeInfo();
        ViewState["sortproductcolor"] = dtProductSize;
        DataView dv = new DataView((DataTable)ViewState["sortproductcolor"]);
        if (ViewState["sortproductcolorstring"] == null)
            ViewState["sortproductcolorstring"] = dtProductSize.Columns[0].ColumnName.Trim();
        dv.Sort = ViewState["sortproductcolorstring"].ToString();
        this.gvProductSize.DataSource = dv;
        this.gvProductSize.DataBind();
        Translations();
    }

    /// <summary>
    /// 给产品尺寸模型赋值
    /// </summary>
    /// <returns></returns>
    protected bool SetValueProductSizeModel()
    {
        if (txtLength.Text.Trim()== "" || txtWidth.Text=="" || txtHigh.Text=="")
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002254", "尺寸名称不能为空!")));
            return false;
        }

        else
        {
            productSizeModel.ProductSizeID = Convert.ToInt32(ViewState["ProductSizeID"]);
            productSizeModel.ProductSizeName = txtLength.Text.Trim() + "*" + txtWidth.Text.Trim() + "*" + txtHigh.Text.Trim();
            productSizeModel.ProductSizeDescr = txtProductSizeDescr.Text.Trim();
            return true;
        }
    }

    /// <summary>
    /// 返回添加参数页面
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("../SetParameters.aspx");
    }

    /// <summary>
    /// 清空内容
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ResetAll();
    }

    /// <summary>
    /// 清空文本框
    /// </summary>
    public void ResetAll()
    {
        txtLength.Text = "";
        txtWidth.Text = "";
        txtHigh.Text = "";
        txtProductSizeDescr.Text = "";
    }

    /// <summary>
    /// 添加尺寸记录
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ResetAll();
        divProductSize.Visible = true;
        ViewState["ID"] = 1;
    }

    protected void gvProductSize_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            int primaryKey = Convert.ToInt32(this.gvProductSize.DataKeys[e.Row.RowIndex].Value.ToString());
            ((LinkButton)e.Row.FindControl("lbtEdit")).CommandArgument = primaryKey.ToString();
            ((LinkButton)e.Row.FindControl("lbtDelete")).CommandArgument = primaryKey.ToString();
        }

        ///控制GridView样式
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
        }
        Translations();
    }

    /// <summary>
    /// 删除指定的产品尺寸信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtDelete_Command(object sender, CommandEventArgs e)
    {
        int productSizeId = Convert.ToInt32(e.CommandArgument);
        //Judge the ProductSizeId whether exist by Id before delete or update
        int isExistCount = SetParametersBLL.ProductSizeIdIsExist(productSizeId);
        if (isExistCount > 0)
        {
            //Juage the ProductSizeId whether has operation before delete by Id
            int getCount = SetParametersBLL.ProductSizeIdWhetherHasOperation(productSizeId);
            if (getCount > 0)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002255", "对不起，该尺寸已经发生了业务，因此不能删除!")));
                divProductSize.Visible = false;
                return;
            }

            else
            {
                BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("ProductSize", "ProductSizeID");
                cl_h_info.AddRecord(productSizeId);
                //删除指定产品尺寸信息
                int delCount = SetParametersBLL.DelProductSizeByID(productSizeId);
                if (delCount > 0)
                {
                    cl_h_info.DeletedIntoLogs(ChangeCategory.company28, Session["Company"].ToString(), ENUM_USERTYPE.objecttype9);
                    divProductSize.Visible = false;
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002256","删除尺寸成功!")));
                }

                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002257", "删除尺寸失败，请联系管理员!")));
                    return;
                }
            }
        }

        else
        {
            divProductSize.Visible = false;
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002260", "对不起，该尺寸不存在或者已经被删除!")));
            return;
        }

        DataBindProductSize();
    }

    /// <summary>
    /// 修改指定的产品尺寸信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtEdit_Command(object sender, CommandEventArgs e)
    {
        ViewState["ID"] = 2;
        ViewState["ProductSizeID"] = e.CommandArgument;

        int productSizeId = Convert.ToInt32(e.CommandArgument);
        //Judge the ProductSizeId whether exist by Id before delete or update
        int isExistCount = SetParametersBLL.ProductSizeIdIsExist(productSizeId);
        if (isExistCount > 0)
        {
            divProductSize.Visible = true;
            //获取指定的产品尺寸信息
            DataTable dt = SetParametersBLL.GetProductSizeInfoByID(productSizeId);
            if (dt.Rows.Count != 0)
            {
                string[] strProductSize = Convert.ToString(dt.Rows[0][0]).Split('*');
                txtLength.Text = strProductSize[0];
                txtWidth.Text = strProductSize[1];
                txtHigh.Text = strProductSize[2];
                txtProductSizeDescr.Text = dt.Rows[0][1].ToString();
            }
        }

        else
        {
            divProductSize.Visible = false;
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002260", "对不起，该尺寸不存在或者已经被删除!")));
            return; 
        }        
    }

    /// <summary>
    /// 提交事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        bool flag = SetValueProductSizeModel();
        if (flag)
        {
            if ((int)ViewState["ID"] == 1)
            {
                ///获取指定产品尺寸的行数
                int getCount = SetParametersBLL.GetProductSizeCountByName(txtProductSizeName.Text.Trim());
                if (getCount > 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002261", "该尺寸已经存在!")));
                }

                else
                {
                    //添加尺寸信息               
                    int addCount = SetParametersBLL.AddProductSize(productSizeModel);
                    if (addCount > 0)
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002263", "添加尺寸成功!")));
                        divProductSize.Visible = false;
                    }

                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002264", "添加尺寸失败,请联系管理员!")));
                    }
                }
            }

            if ((int)ViewState["ID"] == 2)
            {
                ///获取指定产品尺寸的行数
                int getCount = SetParametersBLL.GetProductSizeCountByIDName(Convert.ToInt32(ViewState["ProductSizeID"]), txtProductSizeName.Text.Trim());
                if (getCount > 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002261", "该尺寸已经存在!")));
                }

                else
                {
                    BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("ProductSize", "ProductSizeID");
                    cl_h_info.AddRecord(productSizeModel.ProductSizeID);
                    ///修改指定产品尺寸信息                
                    int updCount = SetParametersBLL.UpdProductSizeByID(productSizeModel);
                    if (updCount > 0)
                    {
                        cl_h_info.AddRecord(productSizeModel.ProductSizeID);
                        cl_h_info.ModifiedIntoLogs(ChangeCategory.company28, Session["Company"].ToString(), ENUM_USERTYPE.objecttype9);

                        Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002265", "修改尺寸成功!")));
                        divProductSize.Visible = false;
                    }

                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002266", "修改尺寸失败,请联系管理员!")));
                    }
                }
            }
            DataBindProductSize();
        }

        else
        {
            return;
        }        
    }

    /// <summary>
    /// 导出到Excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        DataTable dt = SetParametersBLL.OutToExcel_ProductSize();
        if (dt.Rows.Count < 1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001712", "对不起，没有可以导出的数据!")));
            return;
        }

        else
        {
            Excel.OutToExcel(dt, GetTran("002267", "产品尺寸信息"), new string[] { "ProductSizeName=" + GetTran("002268", "产品尺寸名称"), "ProductSizeDescr=" + GetTran("002269", "产品尺寸说明") });
        }
    }

    /// <summary>
    /// 针对导出Excel重载
    /// </summary>
    /// <param name="control"></param>
    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }
    
    /// <summary>
    /// GridView排序事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvProductSize_Sorting(object sender, GridViewSortEventArgs e)
    {       
        DataView dv = new DataView((DataTable)ViewState["sortproductcolor"]);
        string sortString = e.SortExpression;

        switch (sortString.ToLower().Trim())
        { 
            case "productsizeid":
                if (blProductSizeID)
                {
                    dv.Sort = "ProductSizeID desc";
                    blProductSizeID = false;
                }

                else
                {
                    dv.Sort = "ProductSizeID asc";
                    blProductSizeID = true;
                }
                break;

            case "productsizename":
                if (blProductSizeName)
                {
                    dv.Sort = "ProductSizeName desc";
                    blProductSizeName = false;
                }

                else
                {
                    dv.Sort = "ProductSizeName asc";
                    blProductSizeName = true;
                }
                break;

            case "productsizedescr":
                if (blProductSizeDescr)
                {
                    dv.Sort = "ProductSizeDescr desc";
                    blProductSizeDescr = false;
                }

                else
                {
                    dv.Sort = "ProductSizeDescr asc";
                    blProductSizeDescr = true;
                }
                break;
        }
        this.gvProductSize.DataSource = dv;
        this.gvProductSize.DataBind();
    }
}
