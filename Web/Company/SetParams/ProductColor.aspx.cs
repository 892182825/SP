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
using DAL;
using BLL.other.Company;
using BLL.CommonClass;
using System.Data.SqlClient;
using BLL.Logistics;

/*
 * 创建者：     汪  华
 * 创建时间：   2009-09-15
 * 对应菜单:    系统管理->参数设置
 */

public partial class Company_SetParams_ProductColor : BLL.TranslationBase
{

    /// <summary>
    /// 实例化产品颜色模型
    /// </summary>
    ProductColorModel productColorModel = new ProductColorModel();
    Languages language = new Languages();

    /// <summary>
    /// 为GridView排序事件声明变量和赋值
    /// </summary>
    private static bool blProductColorID = true;
    private static bool blProductColorName = true;
    private static bool blProductColorDescr = true;

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, "../index.aspx");
        ///设置GridView的样式
        gvProductColor.Attributes.Add("style", "word-break:keep-all;word-wrap:normal");

        if (!Page.IsPostBack)
        {
            divProductColor.Visible = false;
            DataBindProductColor();
            ViewState["ID"] = 0;
        }
        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.gvProductColor,
                new string[][]{
                    new string []{"000015","操作"},
                    new string []{"002180","产品颜色编号"},
                    new string []{"002182","产品颜色名称"},
                    new string []{"002183","产品颜色说明"}});
        this.TranControls(this.btnAdd, new string[][] { new string[] { "002047", "添 加" } });
        this.TranControls(this.btnReturn, new string[][] { new string[] { "000096", "返 回" } });
        this.TranControls(this.btnOK, new string[][] { new string[] { "002190", "确 定" } });
        this.TranControls(this.btnReset, new string[][] { new string[] { "001759", "清 空" } });

        this.TranControls(this.lblProductColorName, new string[][] { new string[] { "002194", "颜色名称：" } });
        this.TranControls(this.lblProductColorDescr, new string[][] { new string[] { "002195", "颜色描述：" } });

    }

    /// <summary>
    /// 绑定数据
    /// </summary>
    protected void DataBindProductColor()
    {
        ///获取产品颜色信息       
        DataTable dtproductcolor = SetParametersBLL.GetProductColorInfo();
        ViewState["sortproductcolor"] = dtproductcolor;
        DataView dv = new DataView((DataTable)ViewState["sortproductcolor"]);
        if (ViewState["sortproductcolorstring"] == null)
            ViewState["sortproductcolorstring"] = dtproductcolor.Columns[0].ColumnName.Trim();
        dv.Sort = ViewState["sortproductcolorstring"].ToString();
        this.gvProductColor.DataSource = dv;
        this.gvProductColor.DataBind();
        Translations();
    }

    /// <summary>
    /// 给产品颜色模型层赋值
    /// </summary>
    protected bool SetValueProductColorModel()
    {
        if (txtProductColorName.Text.Trim() == "" || txtProductColorName.Text.Trim() == null)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002196", "颜色名称不能为空!")));
            return false;
        }

        else
        {
            productColorModel.ProductColorID = Convert.ToInt32(ViewState["ProductColorID"]);
            productColorModel.ProductColorName = txtProductColorName.Text.Trim();
            productColorModel.ProductColorDescr = txtProductColorDescr.Text.Trim();
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
        txtProductColorName.Text = "";
        txtProductColorDescr.Text = "";
    }

    /// <summary>
    /// 添加颜色记录
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ResetAll();
        divProductColor.Visible = true;
        ViewState["ID"] = 1;
    }

    protected void gvProductColor_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            int primaryKey = Convert.ToInt32(this.gvProductColor.DataKeys[e.Row.RowIndex].Value.ToString());
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
    /// 删除指定的产品颜色信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtDelete_Command(object sender, CommandEventArgs e)
    {
        int productColorId = Convert.ToInt32(e.CommandArgument);
        //Judge the ProductColorID whether exist by ProductColorID before delete or update
        int isExistCount = SetParametersBLL.ProductColorIDIsExist(productColorId);

        BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("ProductColor", "ProductColorID");

        if (isExistCount > 0)
        {
            //Juage the ProductColorID whether has operation before delete
            int getCount = SetParametersBLL.ProductColorIDWhetherHasOperation(productColorId);
            if (getCount > 0)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002197", "对不起，该颜色发生了业务，因此不能删除!")));
                divProductColor.Visible = false;
                return;
            }

            else
            {
                //Delete the ProductColor information by productColorId
                using (SqlConnection conn = new SqlConnection(DBHelper.connString))
                {
                    conn.Open();
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {

                            cl_h_info.AddRecordtran(tran, productColorId.ToString());

                            SetParametersBLL.DelProductColorByID(tran, productColorId);
                            language.RemoveTranslationRecord(tran, "ProductColor", productColorId);

                            cl_h_info.DeletedIntoLogstran(tran, ChangeCategory.company28, Session["Company"].ToString(), ENUM_USERTYPE.objecttype9);
                            tran.Commit();
                        }

                        catch
                        {
                            tran.Rollback();
                            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002199", "删除颜色失败，请联系管理员!")));
                            return;
                        }
                        Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002198", "删除颜色成功!")));
                    }
                }
            }
        }

        else
        {
            divProductColor.Visible = false;
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002200", "对不起，该颜色不存在或者已经被删除!")));
            return;
        }
        DataBindProductColor();
    }

    /// <summary>
    /// 修改指定的产品颜色信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtEdit_Command(object sender, CommandEventArgs e)
    {
        ViewState["ID"] = 2;
        ViewState["ProductColorID"] = e.CommandArgument;

        int productColorId = Convert.ToInt32(e.CommandArgument);
        //Judge the ProductColorID whether exist by ProductColorID before delete or update
        int isExistCount = SetParametersBLL.ProductColorIDIsExist(productColorId);
        if (isExistCount > 0)
        {
            divProductColor.Visible = true;
            //获取指定的产品颜色信息
            DataTable dt = SetParametersBLL.GetProductColorInfoByID(Convert.ToInt32(ViewState["ProductColorID"]));
            if (dt.Rows.Count != 0)
            {
                txtProductColorName.Text = dt.Rows[0][0].ToString();
                txtProductColorDescr.Text = dt.Rows[0][1].ToString();
            }
        }

        else
        {
            divProductColor.Visible = false;
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002200", "对不起，该颜色不存在或者已经被删除!")));
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
        int value = 0;
        bool flag = SetValueProductColorModel();
        if (flag)
        {
            if ((int)ViewState["ID"] == 1)
            {
                //获取指定产品颜色的行数
                int getCount = SetParametersBLL.GetProductColorCountByName(txtProductColorName.Text.Trim());
                if (getCount > 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002201", "该颜色已经存在!")));
                }

                else
                {
                    //添加颜色信息                
                    using (SqlConnection conn = new SqlConnection(DBHelper.connString))
                    {
                        int id;
                        conn.Open();
                        
                            try
                            {
                                value=SetParametersBLL.AddProductColor(productColorModel, out id);
                                language.AddNewTranslationRecord("ProductColor", "ProductColorName", id, txtProductColorName.Text.Trim(), txtProductColorName.Text.Trim());
                                language.AddNewTranslationRecord("ProductColor", "ProductColorDescr", id, txtProductColorDescr.Text.Trim(), txtProductColorDescr.Text.Trim());
                               
                            }

                            catch (Exception ex)
                            {
                                value = 0;
                               
                                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002203", "添加颜色失败,请联系管理员!")));
                            }
                            if (value > 0)
                            {
                                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002202", "添加颜色成功!")));
                                divProductColor.Visible = false;
                            }
                        
                    }
                }
            }

            if ((int)ViewState["ID"] == 2)
            {
                int updCount = 0;
                ///获取指定产品颜色的行数
                int getCount = SetParametersBLL.GetProductColorCountByIDName(Convert.ToInt32(ViewState["ProductColorID"]), txtProductColorName.Text.Trim());
                if (getCount > 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002201", "该颜色已经存在!")));
                }

                else
                {
                    BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("ProductColor", "ProductColorID");
                    cl_h_info.AddRecord(productColorModel.ProductColorID);


                    using (SqlConnection conn = new SqlConnection(DBHelper.connString))
                    {
                        conn.Open();
                        using (SqlTransaction tran = conn.BeginTransaction())
                        {
                            try
                            {
                                ///修改指定产品颜色信息    
                                value=SetParametersBLL.UpdProductColorByID(tran, productColorModel);
                                language.UpdateNewTranslationRecord(tran, "ProductColor", "ProductColorName", Convert.ToInt32(ViewState["ProductColorID"]), productColorModel.ProductColorName);
                                language.UpdateNewTranslationRecord(tran, "ProductColor", "ProductColorDescr", Convert.ToInt32(ViewState["ProductColorID"]), productColorModel.ProductColorDescr);
                                tran.Commit();
                            }
                            catch (Exception ex)
                            {
                                value = 0;
                                tran.Rollback();
                                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002208", "修改颜色失败,请联系管理员!")));
                            }
                            if (value > 0)
                            {
                                cl_h_info.AddRecord(productColorModel.ProductColorID);
                                cl_h_info.ModifiedIntoLogs(ChangeCategory.company28, Session["Company"].ToString(), ENUM_USERTYPE.objecttype9);
                                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002205", "修改颜色成功!")));
                                divProductColor.Visible = false;

                            }

                        }
                    }

                   
                }
            }
            DataBindProductColor();
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
        DataTable dt = SetParametersBLL.GetProductColorInfo();
        if (dt.Rows.Count < 1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001712", "对不起，没有可以导出的数据!")));
            return;
        }

        else
        {
            Excel.OutToExcel(dt, GetTran("002209", "产品颜色信息"), new string[] { "ProductColorName=" + GetTran("002182", "产品颜色名称"), "ProductColorDescr=" + GetTran("002183", "产品颜色说明") });
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
    protected void gvProductColor_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataView dv = new DataView((DataTable)ViewState["sortproductcolor"]);
        string sortString = e.SortExpression;

        switch (sortString.ToLower().Trim())
        {
            case "productcolorid":
                if (blProductColorID)
                {
                    dv.Sort = "ProductColorID desc";
                    blProductColorID = false;
                }

                else
                {
                    dv.Sort = "ProductColorID asc";
                    blProductColorID = true;
                }
                break;

            case "productcolorname":
                if (blProductColorName)
                {
                    dv.Sort = "ProductColorName desc";
                    blProductColorName = false;
                }

                else
                {
                    dv.Sort = "ProductColorName asc";
                    blProductColorName = true;
                }
                break;

            case "productcolordescr":
                if (blProductColorDescr)
                {
                    dv.Sort = "ProductColorDescr desc";
                    blProductColorDescr = false;
                }

                else
                {
                    dv.Sort = "ProductColorDescr asc";
                    blProductColorDescr = true;
                }
                break;
        }
        this.gvProductColor.DataSource = dv;
        this.gvProductColor.DataBind();
    }
}
