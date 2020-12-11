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
using BLL.other.Company;
using BLL.Logistics;
using BLL.CommonClass;
using DAL;

/*
 * 创建者：     汪  华
 * 创建时间：   2009-09-17
 * 对应菜单:    系统管理->参数设置
 */

public partial class Company_SetParams_ProductStatus : BLL.TranslationBase
{
    /// <summary>
    /// 实例化产品状态模型
    /// </summary>
    ProductStatusModel productStatusModel = new ProductStatusModel();
    Languages language = new Languages();

    /// <summary>
    /// 为GridView排序事件声明变量和赋值
    /// </summary>
    private static bool blProductStatusID = true;
    private static bool blProductStatusName = true;
    private static bool blProductStatusDescr = true;      

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, "../index.aspx");
        ///设置GridView的样式
        gvProductStatus.Attributes.Add("style", "word-break:keep-all;word-wrap:normal");

        if (!Page.IsPostBack)
        {
            divProductStatus.Visible = false;
            DataBindProductStatus();
        }
        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.gvProductStatus,
                new string[][]{
                    new string []{"000015","操作"},
                    new string []{"002293","产品状态编号"},
                    new string []{"002294","产品状态名称"},
                    new string []{"002295","产品状态说明"}});
        this.TranControls(this.btnAdd, new string[][] { new string[] { "002047", "添 加" } });
        this.TranControls(this.btnReturn, new string[][] { new string[] { "000096", "返 回" } });
        this.TranControls(this.btnOK, new string[][] { new string[] { "002190", "确 定" } });
        this.TranControls(this.btnReset, new string[][] { new string[] { "001759", "清 空" } });

        this.TranControls(this.lblProductStatusName, new string[][] { new string[] { "002294", "产品状态名称：" } });
        this.TranControls(this.lblProductStatusDescr, new string[][] { new string[] { "002295", "产品状态说明：" } });

    }

    /// <summary>
    /// 绑定数据
    /// </summary>
    protected void DataBindProductStatus()
    {
        ///获取产品状态信息       
        DataTable dtProductStatus = SetParametersBLL.GetProductStatusInfo();
        ViewState["sortProductStatus"] = dtProductStatus;
        DataView dv = new DataView((DataTable)ViewState["sortProductStatus"]);
        if (ViewState["SortProductStatusString"] == null)
            ViewState["SortProductStatusString"] = dtProductStatus.Columns[0].ColumnName.Trim();
        dv.Sort = ViewState["SortProductStatusString"].ToString();
        this.gvProductStatus.DataSource = dv;
        this.gvProductStatus.DataBind();
        Translations();
    }

    /// <summary>
    /// 给产品状态模型赋值
    /// </summary>
    /// <returns></returns>
    protected bool SetValueProductStatusModel()
    {
        if (txtProductStatusName.Text.Trim() == "" || txtProductStatusName.Text.Trim() == null)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002296","状态名称不能为空!")));
            return false;
        }

        else
        {
            productStatusModel.ProductStatusID = Convert.ToInt32(ViewState["ProductStatusID"]);
            productStatusModel.ProductStatusName = txtProductStatusName.Text.Trim();
            productStatusModel.ProductStatusDescr = txtProductStatusDescr.Text.Trim();
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
        txtProductStatusName.Text = "";
        txtProductStatusDescr.Text = "";
    }

    /// <summary>
    /// 添加状态记录
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        divProductStatus.Visible = true;
        ResetAll();        
        ViewState["ID"] = 1;
    }

    protected void gvProductStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            int primaryKey = Convert.ToInt32(this.gvProductStatus.DataKeys[e.Row.RowIndex].Value.ToString());
            ((LinkButton)e.Row.FindControl("lbtEdit")).CommandArgument = primaryKey.ToString();
            ((LinkButton)e.Row.FindControl("lbtDelete")).CommandArgument = primaryKey.ToString();
        }

        //控制GridView样式
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
        }
        Translations();
    }

    /// <summary>
    /// 删除指定的产品状态信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtDelete_Command(object sender, CommandEventArgs e)
    {
        int productStatusId = Convert.ToInt32(e.CommandArgument);
        //Judge the ProductStatusId whether exist by Id before delete or update

        BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("ProductStatus", "ProductStatusID");

        int isExistCount = SetParametersBLL.ProductStatusIdIsExist(productStatusId);
        if (isExistCount > 0)
        {
            //Judge the ProductStatusId whether has operation before delete by Id
            int getCount = SetParametersBLL.ProductStatusIdWhetherHasOperation(productStatusId);

            if (getCount > 0)
            {
                divProductStatus.Visible = false;
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("003141", "对不起，该状态已经发生了业务，因此不能删除!")));
                return;
            }

            else
            {
                //Delete the ProductStatus information by productStatusId
                using (SqlConnection conn = new SqlConnection(DBHelper.connString))
                {
                    conn.Open();
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {
                            cl_h_info.AddRecordtran(tran,productStatusId.ToString());

                            SetParametersBLL.DelProductStatusByID(tran,productStatusId);
                            language.RemoveTranslationRecord(tran, "ProductStatus", productStatusId);

                            cl_h_info.DeletedIntoLogs(ChangeCategory.company28, Session["Company"].ToString(), ENUM_USERTYPE.objecttype9);
                            tran.Commit();
                        }

                        catch
                        {
                            tran.Rollback();
                            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("003143", "删除状态失败，请联系管理员!")));
                            return;
                        }
                        Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("003142", "删除状态成功!")));
                    }
                }                   
            }
        }

        else
        {
            divProductStatus.Visible = false;
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("003144", "对不起，该状态不存在或者已经被删除!")));
            return;
        }

        DataBindProductStatus();
    }

    /// <summary>
    /// 修改指定的产品状态信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtEdit_Command(object sender, CommandEventArgs e)
    {        
        ViewState["ID"] = 2;
        ViewState["ProductStatusID"] = e.CommandArgument;

        int productStatusId = Convert.ToInt32(e.CommandArgument);
        //Judge the ProductStatusId whether exist by Id before delete or update
        int isExistCount = SetParametersBLL.ProductStatusIdIsExist(productStatusId);
        if (isExistCount > 0)
        {           
            //获取指定的产品状态信息
            DataTable dt = SetParametersBLL.GetProductStatusInfoByID(Convert.ToInt32(ViewState["ProductStatusID"]));
            if (dt.Rows.Count != 0)
            {
                divProductStatus.Visible = true;
                txtProductStatusName.Text = dt.Rows[0][0].ToString();
                txtProductStatusDescr.Text = dt.Rows[0][1].ToString();
            }            
        }

        else
        {
            divProductStatus.Visible = false;
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("003144", "对不起，该状态不存在或者已经被删除!")));
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
        bool flag = SetValueProductStatusModel();
        if (flag)
        {
            if ((int)ViewState["ID"] == 1)
            {
                ///获取指定产品状态的行数
                int getCount = SetParametersBLL.GetProductStatusCountByName(txtProductStatusName.Text.Trim());
                if (getCount > 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("003145", "该状态已经存在!")));
                }

                else
                {
                    //添加状态信息          
                    using (SqlConnection conn = new SqlConnection(DBHelper.connString))
                    {
                        int id;
                        conn.Open();
                        using (SqlTransaction tran = conn.BeginTransaction())
                        {
                            try
                            {
                                SetParametersBLL.AddProductStatus(tran, productStatusModel, out id);
                                language.AddNewTranslationRecord(tran, "ProductStatus", "ProductStatusName", id, txtProductStatusName.Text.Trim(), txtProductStatusName.Text.Trim());
                                language.AddNewTranslationRecord(tran, "ProductStatus", "ProductStatusDescr", id,txtProductStatusDescr.Text.Trim(), txtProductStatusDescr.Text.Trim());
                                tran.Commit();
                            }

                            catch
                            {
                                tran.Rollback();
                                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("003147", "添加状态失败,请联系管理员!")));
                                return;
                            }

                            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("003146", "添加状态成功!")));
                            divProductStatus.Visible = false;
                        }
                    }                   
                }
            }

            if ((int)ViewState["ID"] == 2)
            {
                ///获取指定产品状态的行数
                int getCount = SetParametersBLL.GetProductStatusCountByIDName(Convert.ToInt32(ViewState["ProductStatusID"]), txtProductStatusName.Text.Trim());
                if (getCount > 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("003145", "该状态已经存在!")));
                }

                else
                {
                    BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("ProductStatus", "ProductStatusID");
                    cl_h_info.AddRecord(productStatusModel.ProductStatusID);
                    ///修改指定产品状态信息
                    int updCount = SetParametersBLL.UpdProductStatusByID(productStatusModel);
                    if (updCount > 0)
                    {
                        cl_h_info.AddRecord(productStatusModel.ProductStatusID);
                        cl_h_info.ModifiedIntoLogs(ChangeCategory.company28, Session["Company"].ToString(), ENUM_USERTYPE.objecttype9);


                        Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("003148", "修改状态成功!")));
                        divProductStatus.Visible = false;
                    }

                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("003149", "修改状态失败,请联系管理员!")));
                    }
                }
            }
            DataBindProductStatus();
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
        DataTable dt = SetParametersBLL.OutToExcel_ProductStatus();
        if (dt.Rows.Count < 1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001712", "对不起，没有可以导出的数据!")));
            return;
        }

        else
        {
            Excel.OutToExcel(dt, GetTran("003150", "产品状态信息"), new string[] { "ProductStatusName=" + GetTran("002294", "产品状态名称"), "ProductStatusDescr=" + GetTran("002295", "产品状态说明") });
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
    protected void gvProductStatus_Sorting(object sender, GridViewSortEventArgs e)
    {        
        DataView dv = new DataView((DataTable)ViewState["sortProductStatus"]);
        string sortString = e.SortExpression;

        switch (sortString.ToLower().Trim())
        {
            case "productstatusid":
                if (blProductStatusID)
                {
                    dv.Sort = "ProductStatusID desc";
                    blProductStatusID = false;
                }

                else
                {
                    dv.Sort = "ProductStatusID asc";
                    blProductStatusID = true;
                }
                break;

            case "productstatusname":
                if (blProductStatusName)
                {
                    dv.Sort = "ProductStatusName desc";
                    blProductStatusName = false;
                }

                else
                {
                    dv.Sort = "ProductStatusName asc";
                    blProductStatusName = true;
                }
                break;

            case "productstatusdescr":
                if (blProductStatusDescr)
                {
                    dv.Sort = "ProductStatusDescr desc";
                    blProductStatusDescr = false;
                }
                else
                {
                    dv.Sort = "ProductStatusDescr asc";
                    blProductStatusDescr = true;
                }
                break;
        }
        this.gvProductStatus.DataSource = dv;
        this.gvProductStatus.DataBind();
    }
}
