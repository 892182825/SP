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
 * 创建时间：   2009-09-15
 * 对应菜单:    系统管理->参数设置
 */

public partial class Company_SetParams_ProductSpec : BLL.TranslationBase
{
    /// <summary>
    /// 实例化产品规格模型
    /// </summary>
    ProductSpecModel productSpecModel = new ProductSpecModel();

    /// <summary>
    /// 为GridView排序事件声明变量和赋值
    /// </summary>
    private static bool blProductSpecID = true;
    private static bool blProductSpecName = true;
    private static bool blProductSpecDescr = true;    

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, "../index.aspx");
        ///设置GridView的样式
        gvProductSpec.Attributes.Add("style", "word-break:keep-all;word-wrap:normal");

        if (!Page.IsPostBack)
        {
            divProductSpec.Visible = false;
            DataBindProductSpec();
            ViewState["ID"] = 0;
        }
        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.gvProductSpec,
                new string[][]{
                    new string []{"000015","操作"},
                    new string []{"002273","产品规格编号"},
                    new string []{"002274","产品规格名称"},
                    new string []{"002275","产品规格说明"}});
        this.TranControls(this.btnAdd, new string[][] { new string[] { "002047", "添 加" } });
        this.TranControls(this.btnReturn, new string[][] { new string[] { "000096", "返 回" } });
        this.TranControls(this.btnOK, new string[][] { new string[] { "002190", "确 定" } });
        this.TranControls(this.btnReset, new string[][] { new string[] { "001759", "清 空" } });

        this.TranControls(this.lblProductSpecName, new string[][] { new string[] { "002274", "产品规格名称：" } });
        this.TranControls(this.lblProductSpecDescr, new string[][] { new string[] { "002275", "产品规格说明：" } });

    }

    /// <summary>
    /// 绑定数据
    /// </summary>
    protected void DataBindProductSpec()
    {
        ///获取产品规格信息       
        DataTable dtProductSpec = SetParametersBLL.GetProductSpecInfo();
        ViewState["sortProductSpec"] = dtProductSpec;
        DataView dv = new DataView((DataTable)ViewState["sortProductSpec"]);
        if (ViewState["sortProductSpecstring"] == null)
            ViewState["sortProductSpecstring"] = dtProductSpec.Columns[0].ColumnName.Trim();
        dv.Sort = ViewState["sortProductSpecstring"].ToString();
        this.gvProductSpec.DataSource = dv;
        this.gvProductSpec.DataBind();
        Translations();
    }

    /// <summary>
    /// 给产品规格模型赋值
    /// </summary>
    /// <returns></returns>
    protected bool SetValueProductSpecModel()
    {
        if (txtProductSpecName.Text.Trim() == "" || txtProductSpecName.Text.Trim() == null)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002276", "规格名称不能为空!")));
            return false;
        }

        else
        {
            productSpecModel.ProductSpecID = Convert.ToInt32(ViewState["ProductSpecID"]);
            productSpecModel.ProductSpecName = txtProductSpecName.Text.Trim();
            productSpecModel.ProductSpecDescr = txtProductSpecDescr.Text.Trim();
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
        txtProductSpecName.Text = "";
        txtProductSpecDescr.Text = "";
    }

    /// <summary>
    /// 添加规格记录
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ResetAll();
        divProductSpec.Visible = true;
        ViewState["ID"] = 1;
    }

    protected void gvProductSpec_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            int primaryKey = Convert.ToInt32(this.gvProductSpec.DataKeys[e.Row.RowIndex].Value.ToString());
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
    /// 删除指定的产品规格信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtDelete_Command(object sender, CommandEventArgs e)
    {
        int productSpecId = Convert.ToInt32(e.CommandArgument);

        //Judge the ProductSpecId whether exist by Id before delete or update
        int isExistCount = SetParametersBLL.ProductSpecIdIsExist(productSpecId);
        if (isExistCount > 0)
        {
            //Judge the ProductSpecId whether has operation before delete by Id
            int getCount = SetParametersBLL.ProductSpecIdWhetherHasOperation(productSpecId);
            if (getCount > 0)
            {
                divProductSpec.Visible = false;
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002277","对不起，该规格已经发生了业务，因此不能删除!")));
                return;
            }

            else
            {
                BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("ProductSpec", "ProductSpecID");
                cl_h_info.AddRecord(productSpecId);
                //删除指定产品规格信息
                int delCount = SetParametersBLL.DelProductSpecByID(productSpecId);
                if (delCount > 0)
                {
                    cl_h_info.DeletedIntoLogs(ChangeCategory.company28, Session["Company"].ToString(), ENUM_USERTYPE.objecttype9);

                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002278","删除规格成功!")));
                }

                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002279","删除规格失败，请联系管理员!")));
                    return;
                }
            }
        }

        else
        {
            divProductSpec.Visible = false;
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002280","对不起，该规格不存在或者已经被删除!")));
            return;
        }

        DataBindProductSpec();
    }

    /// <summary>
    /// 修改指定的产品规格信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtEdit_Command(object sender, CommandEventArgs e)
    {        
        ViewState["ID"] = 2;
        ViewState["ProductSpecID"] = e.CommandArgument;

        int productSpecId = Convert.ToInt32(e.CommandArgument);

        //Judge the ProductSpecId whether exist by Id before delete or update
        int isExistCount = SetParametersBLL.ProductSpecIdIsExist(productSpecId);
        if (isExistCount > 0)
        {
            //获取指定的产品规格信息
            DataTable dt = SetParametersBLL.GetProductSpecInfoByID(productSpecId);
            if (dt.Rows.Count > 0)
            {
                divProductSpec.Visible = true;
                txtProductSpecName.Text = dt.Rows[0][0].ToString();
                txtProductSpecDescr.Text = dt.Rows[0][1].ToString();
            }           
        }

        else
        {
            divProductSpec.Visible = false;
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002280", "对不起，该规格不存在或者已经被删除!")));
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
        bool flag = SetValueProductSpecModel();
        if (flag)
        {
            if ((int)ViewState["ID"] == 1)
            {
                ///获取指定产品规格的行数
                int getCount = SetParametersBLL.GetProductSpecCountByName(txtProductSpecName.Text.Trim());
                if (getCount > 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002281","该规格已经存在!")));
                }

                else
                {
                    ///添加规格信息               
                    int addCount = SetParametersBLL.AddProductSpec(productSpecModel);
                    if (addCount > 0)
                    {
                        divProductSpec.Visible = false;
                        Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002282","添加规格成功!")));
                    }

                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002283","添加规格失败,请联系管理员!")));
                        return;
                    }
                }
            }

            if ((int)ViewState["ID"] == 2)
            {
                ///获取指定产品规格的行数
                int getCount = SetParametersBLL.GetProductSpecCountByIDName(Convert.ToInt32(ViewState["ProductSpecID"]), txtProductSpecName.Text.Trim());
                if (getCount > 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002281", "该规格已经存在!")));
                }

                else
                {
                    BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("ProductSpec", "ProductSpecID");
                    cl_h_info.AddRecord(productSpecModel.ProductSpecID);
                    ///修改指定产品规格信息                
                    int updCount = SetParametersBLL.UpdProductSpecByID(productSpecModel);
                    if (updCount > 0)
                    {
                        cl_h_info.AddRecord(productSpecModel.ProductSpecID);
                        cl_h_info.ModifiedIntoLogs(ChangeCategory.company28, Session["Company"].ToString(), ENUM_USERTYPE.objecttype9);

                        Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002284","修改规格成功!")));
                    }

                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002285", "修改规格失败,请联系管理员!")));
                    }
                }
            }
            DataBindProductSpec();
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
        DataTable dt = SetParametersBLL.OutToExcel_ProductSpec();
        if (dt.Rows.Count < 1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001712", "对不起，没有可以导出的数据!")));
            return;
        }

        else
        {
            Excel.OutToExcel(dt, GetTran("002286", "产品规格信息"), new string[] { "ProductSpecName=" + GetTran("002274", "产品规格名称"), "ProductSpecDescr=" + GetTran("002275", "产品规格说明") });
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
    protected void gvProductSpec_Sorting(object sender, GridViewSortEventArgs e)
    {       
        DataView dv = new DataView((DataTable)ViewState["sortProductSpec"]);
        string sortString = e.SortExpression;

        switch (sortString.ToLower().Trim())
        { 
            case "productspecid":
                if (blProductSpecID)
                {
                    dv.Sort = "ProductSpecID desc";
                    blProductSpecID = false;
                }

                else
                {
                    dv.Sort = "ProductSpecID asc";
                    blProductSpecID = true;
                }
                break;

            case "productspecname":
                if (blProductSpecName)
                {
                    dv.Sort = "ProductSpecName desc";
                    blProductSpecName = false;
                }

                else
                {
                    dv.Sort = "ProductSpecName asc";
                    blProductSpecName = true;
                }
                break;

            case "productspecdescr":
                if (blProductSpecDescr)
                {
                    dv.Sort = "ProductSpecDescr desc";
                    blProductSpecDescr = false;
                }

                else
                {
                    dv.Sort = "ProductSpecDescr asc";
                    blProductSpecDescr = true;
                }
                break;
        }
        this.gvProductSpec.DataSource = dv;
        this.gvProductSpec.DataBind();
    }
}
