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
using System.Data.SqlClient;
using Model;
using DAL;
using BLL.other.Company;
using BLL.CommonClass;
using BLL.Logistics;

/*
 * 创建者：     汪  华
 * 创建时间：   2009-09-17
 * 对应菜单:    系统管理->参数设置
 */

public partial class Company_SetParams_ProductUnit : BLL.TranslationBase
{
    /// <summary>
    /// 实例化产品单位模型
    /// </summary>
    ProductUnitModel productUnitModel = new ProductUnitModel();
    Languages language = new Languages();

    /// <summary>
    /// 为GridView排序事件声明变量和赋值
    /// </summary>
    private static bool blProductUnitID = true;
    private static bool blProductUnitName = true;
    private static bool blProductUnitDescr = true;

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, "../index.aspx");
        ///设置GridView的样式
        gvProductUnit.Attributes.Add("style", "word-break:keep-all;word-wrap:normal");

        if (!Page.IsPostBack)
        {
            divProductUnit.Visible = false;
            DataBindProductUnit();
        }
        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.gvProductUnit,
                new string[][]{
                    new string []{"000015","操作"},
                    new string []{"003154","产品单位编号"},
                    new string []{"003157","产品单位名称"},
                    new string []{"003158","产品单位说明"}});
        this.TranControls(this.btnAdd, new string[][] { new string[] { "002047", "添 加" } });
        this.TranControls(this.btnReturn, new string[][] { new string[] { "000096", "返 回" } });
        this.TranControls(this.btnOK, new string[][] { new string[] { "002190", "确 定" } });
        this.TranControls(this.btnReset, new string[][] { new string[] { "001759", "清 空" } });

        this.TranControls(this.lblProductUnitName, new string[][] { new string[] { "003157", "产品单位名称：" } });
        this.TranControls(this.lblProductUnitDescr, new string[][] { new string[] { "003158", "产品单位说明：" } });

    }

    /// <summary>
    /// 绑定数据
    /// </summary>
    protected void DataBindProductUnit()
    {
        ///获取产品单位信息       
        DataTable dtProductUnit = SetParametersBLL.GetProductUnitInfo();
        ViewState["sortProductUnit"] = dtProductUnit;
        DataView dv = new DataView((DataTable)ViewState["sortProductUnit"]);
        if (ViewState["SortProductUnitString"] == null)
            ViewState["SortProductUnitString"] = dtProductUnit.Columns[0].ColumnName.Trim();
        dv.Sort = ViewState["SortProductUnitString"].ToString();
        this.gvProductUnit.DataSource = dv;
        this.gvProductUnit.DataBind();
        Translations();
    }

    /// <summary>
    /// 产品单位模型赋值
    /// </summary>
    /// <returns></returns>
    protected bool SetValueProductUnitModel()
    {
        if (txtProductUnitName.Text.Trim() == "" || txtProductUnitName.Text.Trim() == null)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("003175", "单位名称不能为空!")));
            return false;
        }

        else
        {
            productUnitModel.ProductUnitID = Convert.ToInt32(ViewState["ProductUnitID"]);
            productUnitModel.ProductUnitName = txtProductUnitName.Text.Trim();
            productUnitModel.ProductUnitDescr = txtProductUnitDescr.Text.Trim();
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
        txtProductUnitName.Text = "";
        txtProductUnitDescr.Text = "";
    }

    /// <summary>
    /// 添加单位记录
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        divProductUnit.Visible = true;
        ResetAll();
        ViewState["ID"] = 1;
    }

    protected void gvProductUnit_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            int primaryKey = Convert.ToInt32(this.gvProductUnit.DataKeys[e.Row.RowIndex].Value.ToString());
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
    /// 删除指定的产品单位信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtDelete_Command(object sender, CommandEventArgs e)
    {
        int productUnitId = Convert.ToInt32(e.CommandArgument);

        BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("ProductUnit", "ProductUnitID");
        //Judge the ProductUnitId whether exist by Id before delete or update
        int isExistCount = SetParametersBLL.ProductUnitIdIsExist(productUnitId);
        if (isExistCount > 0)
        {
            //Judge the ProductUnitId whether has operation by Id before delete
            int getCount = SetParametersBLL.ProductUnitIdWhetherHasOperation(productUnitId);
            if (getCount > 0)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("003174", "对不起，该产品单位发生了业务，因此不能删除!")));
                divProductUnit.Visible = false;
                return;
            }

            else
            {
                //Delete the productUint information by productUnitId
                using (SqlConnection conn = new SqlConnection(DBHelper.connString))
                {                    
                    conn.Open();
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {
                            cl_h_info.AddRecordtran(tran, productUnitId.ToString());

                            SetParametersBLL.DelProductUnitByID(tran,productUnitId);
                            language.RemoveTranslationRecord(tran, "ProductUnit", productUnitId);
                            cl_h_info.DeletedIntoLogs(ChangeCategory.company28, Session["Company"].ToString(), ENUM_USERTYPE.objecttype9);
                            tran.Commit();
                        }

                        catch
                        {
                            tran.Rollback();
                            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("003172", "删除单位失败，请联系管理员!")));
                            return;
                        }
                        Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("003173", "删除单位成功!")));                       
                    }
                }                   
            }
        }

        else
        {
            divProductUnit.Visible = false;
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("003171", "对不起，该产品单位不存在或者已经被删除!")));
            return;
        }      
        
        DataBindProductUnit();
    }

    /// <summary>
    /// 修改指定的产品单位信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtEdit_Command(object sender, CommandEventArgs e)
    {
        divProductUnit.Visible = true;
        ViewState["ID"] = 2;
        ViewState["ProductUnitID"] = e.CommandArgument;
        int productUnitId = Convert.ToInt32(e.CommandArgument);

        //Judge the ProductUnitId whether exist by Id before delete or update
        int isExistCount = SetParametersBLL.ProductUnitIdIsExist(productUnitId);
        if (isExistCount > 0)
        {
            //获取指定的产品单位信息
            DataTable dt = SetParametersBLL.GetProductUnitInfoByID(productUnitId);
            if (dt.Rows.Count != 0)
            {
                txtProductUnitName.Text = dt.Rows[0][0].ToString();
                txtProductUnitDescr.Text = dt.Rows[0][1].ToString();
            }            
        }

        else
        {
            divProductUnit.Visible = false;
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("003171", "对不起，该产品单位不存在或者已经被删除!")));           
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
        bool flag = SetValueProductUnitModel();
        if (flag)
        {
            if ((int)ViewState["ID"] == 1)
            {
                ///获取指定产品单位的行数
                int getCount = SetParametersBLL.GetProductUnitCountByName(txtProductUnitName.Text.Trim());
                if (getCount > 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("003167", "该单位已经存在!")));
                    return;
                }

                else
                {
                    //Add ProductUnit Information
                    using (SqlConnection conn = new SqlConnection(DBHelper.connString))
                    {
                        int id;
                        conn.Open();
                        using (SqlTransaction tran = conn.BeginTransaction())
                        {
                            try
                            {
                                SetParametersBLL.AddProductUnit(tran,productUnitModel,out id);
                                language.AddNewTranslationRecord(tran, "ProductUnit", "ProductUnitName", id, txtProductUnitName.Text.Trim(),txtProductUnitName.Text.Trim());
                                language.AddNewTranslationRecord(tran, "ProductUnit", "ProductUnitDescr", id, txtProductUnitDescr.Text.Trim(), txtProductUnitDescr.Text.Trim());
                                tran.Commit();
                            }

                            catch
                            {
                                tran.Rollback();
                                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("003168", "添加单位失败,请联系管理员!")));
                                return;
                            }

                            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("003169", "添加单位成功!")));
                            divProductUnit.Visible = false;
                        }
                    }                   
                }
            }

            if ((int)ViewState["ID"] == 2)
            {
                ///获取指定产品单位的行数
                int getCount = SetParametersBLL.GetProductUnitCountByIDName(Convert.ToInt32(ViewState["ProductUnitID"]), txtProductUnitName.Text.Trim());
                if (getCount > 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("003167", "该单位已经存在!")));
                }

                else
                {
                    ///修改指定产品单位信息
                    BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("ProductUnit", "ProductUnitID");
                    cl_h_info.AddRecord(productUnitModel.ProductUnitID);

                    int updCount = SetParametersBLL.UpdProductUnitByID(productUnitModel);
                    if (updCount > 0)
                    {
                        cl_h_info.AddRecord(productUnitModel.ProductUnitID);
                        cl_h_info.ModifiedIntoLogs(ChangeCategory.company28, Session["Company"].ToString(), ENUM_USERTYPE.objecttype9);

                        Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("003165", "修改单位成功!")));
                        divProductUnit.Visible = false;
                    }

                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("003164", "修改单位失败,请联系管理员!")));
                    }
                }
            }
            DataBindProductUnit();
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
        DataTable dt = SetParametersBLL.OutToExcel_ProductUnit();
        if (dt.Rows.Count < 1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "001712", Transforms.ReturnAlert(GetTran("", "对不起，没有可以导出的数据!")));
            return;
        }

        else
        {
            Excel.OutToExcel(dt, GetTran("003161", "产品单位信息"), new string[] { "ProductUnitName=" + GetTran("003157", "产品单位名称"), "ProductUnitDescr=" + GetTran("003158", "产品单位说明") });
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
    protected void gvProductUnit_Sorting(object sender, GridViewSortEventArgs e)
    {        
        DataView dv = new DataView((DataTable)ViewState["sortProductUnit"]);
        string sortString = e.SortExpression;

        switch (sortString.ToLower().Trim())
        { 
            case "productunitid":
                if (blProductUnitID)
                {
                    dv.Sort = "ProductUnitID desc";
                    blProductUnitID = false;
                }

                else
                {
                    dv.Sort = "ProductUnitID asc";
                    blProductUnitID = true;
                }
                break;

            case "productunitname":
                if (blProductUnitName)
                {
                    dv.Sort = "ProductUnitName desc";
                    blProductUnitName = false;
                }

                else
                {
                    dv.Sort = "ProductUnitName asc";
                    blProductUnitName = true;
                }
                break;

            case "productunitdescr":
                if (blProductUnitDescr)
                {
                    dv.Sort = "ProductUnitName desc";
                    blProductUnitDescr = false;
                }

                else
                {
                    dv.Sort = "ProductUnitName asc";
                    blProductUnitDescr = true;
                }
                break;
        }
        this.gvProductUnit.DataSource = dv;
        this.gvProductUnit.DataBind();
    }    
}
