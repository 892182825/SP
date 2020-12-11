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
using System.Data.SqlClient;
using BLL.other.Company;
using BLL.CommonClass;
using BLL.Logistics;
using DAL;

/*
 * 创建者：     汪  华
 * 创建时间：   2009-09-16
 * 对应菜单:    系统管理->参数设置
 */

public partial class Company_SetParams_ProductSexType : BLL.TranslationBase
{
    /// <summary>
    /// 实例化产品适用人群模型
    /// </summary>
    ProductSexTypeModel productSexTypeModel = new ProductSexTypeModel();
    Languages language = new Languages();
   

    /// <summary>
    /// 为GridView排序事件声明变量和赋值
    /// </summary>
    private static bool blProductSexTypeID = true;
    private static bool blProductSexTypeName = true;
    private static bool blProductSexTypeDescr = true;

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, "../index.aspx");
        ///设置GridView的样式
        gvProductSexType.Attributes.Add("style", "word-break:keep-all;word-wrap:normal");

        if (!Page.IsPostBack)
        {
            divProductSexType.Visible = false;
            DataBindProductSexType();
            ViewState["ID"] = 0;
        }
        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.gvProductSexType,
                new string[][]{
                    new string []{"000015","操作"},
                    new string []{"002240","适用人群编号"},
                    new string []{"002237","适用人群名称"},
                    new string []{"002236","适用人群说明"}});
        this.TranControls(this.btnAdd, new string[][] { new string[] { "002047", "添 加" } });
        this.TranControls(this.btnReturn, new string[][] { new string[] { "000096", "返 回" } });
        this.TranControls(this.btnOK, new string[][] { new string[] { "002190", "确 定" } });
        this.TranControls(this.btnReset, new string[][] { new string[] { "001759", "清 空" } });

        this.TranControls(this.lblProductSexTypeName, new string[][] { new string[] { "002237", "适用人群名称" } });
        this.TranControls(this.lblProductSexTypeDescr, new string[][] { new string[] { "002236", "适用人群描述" } });

    }

    /// <summary>
    /// 绑定数据
    /// </summary>
    protected void DataBindProductSexType()
    {
        ///获取产品适用人群信息       
        DataTable dtproductSexType = SetParametersBLL.GetProductSexTypeInfo();
        ViewState["sortproductcolor"] = dtproductSexType;
        DataView dv = new DataView((DataTable)ViewState["sortproductcolor"]);
        if (ViewState["sortproductcolorstring"] == null)
            ViewState["sortproductcolorstring"] = dtproductSexType.Columns[0].ColumnName.Trim();
        dv.Sort = ViewState["sortproductcolorstring"].ToString();
        this.gvProductSexType.DataSource = dv;
        this.gvProductSexType.DataBind();
        Translations();
    }

    /// <summary>
    /// 给产品适用人群模型赋值
    /// </summary>
    protected bool SetValueProductSexTypeModel()
    {
        if (txtProductSexTypeName.Text.Trim() == "" || txtProductSexTypeName.Text.Trim() == null)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002217", "适用人群名称不能为空!")));
            return false;
        }

        else
        {
            productSexTypeModel.ProductSexTypeID = Convert.ToInt32(ViewState["ProductSexTypeID"]);
            productSexTypeModel.ProductSexTypeName = txtProductSexTypeName.Text.Trim();
            productSexTypeModel.ProductSexTypeDescr = txtProductSexTypeDescr.Text.Trim();
            return true;
        }
    }   

    /// <summary>
    /// 绑定主键
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvProductSexType_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex >= 0)
        {
            int primaryKey = Convert.ToInt32(this.gvProductSexType.DataKeys[e.Row.RowIndex].Value.ToString());
            ((LinkButton)e.Row.FindControl("lbtEdit")).CommandArgument = primaryKey.ToString();
            ((LinkButton)e.Row.FindControl("lbtDelete")).CommandArgument = primaryKey.ToString();
        }

        ///控制GridView样式
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
        }
    }

    /// <summary>
    /// 添加产品适用人群
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ResetAll();
        divProductSexType.Visible = true;
        ViewState["ID"] = 1;       
    }

    /// <summary>
    /// 清空文本框
    /// </summary>
    public void ResetAll()
    {
        txtProductSexTypeName.Text = "";
        txtProductSexTypeDescr.Text = "";        
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
    /// 修改指定的产品适用人群信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtEdit_Command(object sender, CommandEventArgs e)
    {
        ViewState["ID"] = 2;
        ViewState["ProductSexTypeID"] = e.CommandArgument;       

        int productSexTypeId = Convert.ToInt32(e.CommandArgument);

        //Judge the ProductSexTypeId whether exist by Id before delete or update
        int isExistCount = SetParametersBLL.ProductSexTypeIdIsExist(productSexTypeId);
        if (isExistCount > 0)
        {
            divProductSexType.Visible = true;
            //获取指定的产品适用人群信息
            DataTable dt = SetParametersBLL.GetProductSexTypeInfoByID(productSexTypeId);
            if (dt.Rows.Count != 0)
            {
                txtProductSexTypeName.Text = dt.Rows[0][0].ToString();
                txtProductSexTypeDescr.Text = dt.Rows[0][1].ToString();
            }            
        }

        else
        {
            divProductSexType.Visible = false;
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002219", "对不起，该适用人群不存在或者已经被删除!")));
            return;   
        }
    }

    /// <summary>
    /// 删除指定的适用人群信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>   
    protected void lbtDelete_Command(object sender, CommandEventArgs e)
    {
        int productSexTypeId = Convert.ToInt32(e.CommandArgument);
        BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("ProductSexType", "ProductSexTypeID");

        //Judge the ProductSexTypeId whether exist by Id before delete or update
        int isExistCount = SetParametersBLL.ProductSexTypeIdIsExist(productSexTypeId);
        if (isExistCount > 0)
        {
            //Judge the ProductSexTypeId whether has operation before delete by Id
            int getCount = SetParametersBLL.ProductSexTypeIdWhetherHasOperation(productSexTypeId);
            if (getCount > 0)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002220", "对不起，该适用人群已经发生了业务，因此不能删除!")));
                divProductSexType.Visible = false;
                return;
            }

            else
            {
                //Delete the ProductSexType information by productSexTypeId
                using (SqlConnection conn = new SqlConnection(DBHelper.connString))
                {
                    conn.Open();
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {
                            cl_h_info.AddRecordtran(tran, productSexTypeId.ToString());

                            SetParametersBLL.DelProductSexTypeByID(tran, productSexTypeId);
                            language.RemoveTranslationRecord(tran, "ProductSexType", productSexTypeId);

                            cl_h_info.DeletedIntoLogstran(tran, ChangeCategory.company28, Session["Company"].ToString(), ENUM_USERTYPE.objecttype9);
                            tran.Commit();
                        }

                        catch
                        {
                            tran.Rollback();
                            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002223", "删除适用人群失败，请联系管理员!")));
                            return;
                        }
                        Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002222", "删除适用人群成功!")));
                    }
                }                   
            }
        }

        else
        {
            divProductSexType.Visible = false;
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002219", "对不起，该适用人群不存在或者已经被删除!")));
            return;            
        }

        DataBindProductSexType();
    }
    
    /// <summary>
    /// 提交事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        bool flag = SetValueProductSexTypeModel();
        if (flag)
        {
            if ((int)ViewState["ID"] == 1)
            {
                //获取指定产品适用人群的行数
                int getCount = SetParametersBLL.GetProductSexTypeCountByName(txtProductSexTypeName.Text.Trim());
                if (getCount > 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002226", "该适用人群已经存在!")));
                }

                else
                {
                    //添加适用人群信息             
                    using (SqlConnection conn = new SqlConnection(DBHelper.connString))
                    {
                        int id;
                        conn.Open();
                        using (SqlTransaction tran = conn.BeginTransaction())
                        {
                            try
                            {
                                SetParametersBLL.AddProductSexType(tran, productSexTypeModel,out id);
                                language.AddNewTranslationRecord(tran, "ProductSexType", "ProductSexTypeName", id, txtProductSexTypeName.Text.Trim(),txtProductSexTypeName.Text.Trim());
                                language.AddNewTranslationRecord(tran, "ProductSexType", "ProductSexTypeDescr", id, txtProductSexTypeDescr.Text.Trim(), txtProductSexTypeDescr.Text.Trim());                                
                                tran.Commit();
                            }

                            catch
                            {
                                tran.Rollback();
                                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002229", "添加适用人群失败,请联系管理员!")));
                                return;
                            }

                            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002227", "添加适用人群成功!")));
                            divProductSexType.Visible = false;
                        }
                    }
                }
            }

            if ((int)ViewState["ID"] == 2)
            {
                ///获取指定产品适用人群的行数
                int getCount = SetParametersBLL.GetProductSexTypeCountByIDName(Convert.ToInt32(ViewState["ProductSexTypeID"]), txtProductSexTypeName.Text.Trim());
                if (getCount > 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002226", "该适用人群已经存在!")));
                    return;
                }

                else
                {
                    BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("ProductSexType", "ProductSexTypeID");
                    cl_h_info.AddRecord(productSexTypeModel.ProductSexTypeID);
                    ///修改指定产品适用人群信息                
                    int updCount = SetParametersBLL.UpdProductSexTypeByID(productSexTypeModel);
                    if (updCount > 0)
                    {
                        cl_h_info.AddRecord(productSexTypeModel.ProductSexTypeID);
                        cl_h_info.ModifiedIntoLogs(ChangeCategory.company28, Session["Company"].ToString(), ENUM_USERTYPE.objecttype9);

                        Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002230", "修改适用人群成功!")));
                        divProductSexType.Visible = false;
                    }

                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002231", "修改适用人群失败,请联系管理员!")));
                    }
                }
            }
            DataBindProductSexType();
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
        DataTable dt = SetParametersBLL.OutToExcel_ProductSexType();
        if (dt.Rows.Count < 1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002196", "颜色名称不能为空!")));
            return;
        }

        else
        {
            Excel.OutToExcel(dt, GetTran("002235", "适用人群信息"), new string[] { "ProductSexTypeName=" + GetTran("002237", "适用人群名称"), "ProductSexTypeDescr=" + GetTran("002236", "适用人群说明") });
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
    protected void gvProductSexType_Sorting(object sender, GridViewSortEventArgs e)
    {     
        DataView dv = new DataView((DataTable)ViewState["sortproductcolor"]);
        string sortString = e.SortExpression;

        switch (sortString.ToLower().Trim())
        { 
            case "productsextypeid":
                if (blProductSexTypeID)
                {
                    dv.Sort = "ProductSexTypeID desc";
                    blProductSexTypeID = false;
                }

                else
                {
                    dv.Sort = "ProductSexTypeID asc";
                    blProductSexTypeID = true;
                }
                break;

            case "productsextypename":
                if (blProductSexTypeName)
                {
                    dv.Sort = "ProductSexTypeName desc";
                    blProductSexTypeName = false;
                }

                else
                {
                    dv.Sort = "ProductSexTypeName asc";
                    blProductSexTypeName = true;
                }
                break;

            case "productsextypedescr":
                if (blProductSexTypeDescr)
                {
                    dv.Sort = "ProductSexTypeDescr desc";
                    blProductSexTypeDescr = false;
                }

                else
                {
                    dv.Sort = "ProductSexTypeDescr asc";
                    blProductSexTypeDescr = true;
                }
                break;
        }
        this.gvProductSexType.DataSource = dv;
        this.gvProductSexType.DataBind();
    }
}
