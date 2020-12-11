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
using System.Text;
using Model;
using BLL.other.Company;
using BLL.CommonClass;

/*
 * 创建者：     汪  华
 * 创建时间：   2009-09-17
 * 对应菜单:    系统管理->参数设置
 */

public partial class Company_SetParams_ProductType : System.Web.UI.Page
{
    /// <summary>
    /// 实例化产品型号模型
    /// </summary>
    ProductTypeModel productTypeModel = new ProductTypeModel();

    /// <summary>
    /// 为GridView排序事件声明变量和赋值
    /// </summary>
    private static bool blProductTypeID = true;
    private static bool blProductTypeName = true;
    private static bool blProductTypeDescr = true;    

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, "../index.aspx");
        ///设置GridView的样式
        gvProductType.Attributes.Add("style", "word-break:keep-all;word-wrap:normal");

        if (!Page.IsPostBack)
        {
            divProductType.Visible = false;
            DataBindProductType();
        }
    }

    /// <summary>
    /// 绑定数据
    /// </summary>
    protected void DataBindProductType()
    {
        ///获取产品型号信息       
        DataTable dtProductType = SetParametersBLL.GetProductTypeInfo();
        ViewState["sortProductType"] = dtProductType;
        DataView dv = new DataView((DataTable)ViewState["sortProductType"]);
        if (ViewState["SortProductTypeString"] == null)
            ViewState["SortProductTypeString"] = dtProductType.Columns[0].ColumnName.Trim();
        dv.Sort = ViewState["SortProductTypeString"].ToString();
        this.gvProductType.DataSource = dv;
        this.gvProductType.DataBind();
    }

    /// <summary>
    /// 给产品型号模型赋值
    /// </summary>
    /// <returns></returns>
    protected bool SetValueProductTypeModel()
    {
        if (txtProductTypeName.Text.Trim() == "" || txtProductTypeName.Text.Trim() == null)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert("型号名称不能为空!"));
            return false;
        }

        else
        {
            productTypeModel.ProductTypeID = Convert.ToInt32(ViewState["ProductTypeID"]);
            productTypeModel.ProductTypeName = txtProductTypeName.Text.Trim();
            productTypeModel.ProductTypeDescr = txtProductTypeDescr.Text.Trim();
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
        txtProductTypeName.Text = "";
        txtProductTypeDescr.Text = "";
    }

    /// <summary>
    /// 添加型号记录
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        divProductType.Visible = true;
        ResetAll();
        ViewState["ID"] = 1;
    }

    protected void gvProductType_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            int primaryKey = Convert.ToInt32(this.gvProductType.DataKeys[e.Row.RowIndex].Value.ToString());
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
    /// 删除指定的产品型号信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtDelete_Command(object sender, CommandEventArgs e)
    {
        int productTypeId = Convert.ToInt32(e.CommandArgument);
        //Judge the ProductTypeId whether exist by Id before delete or update
        int isExistCount = SetParametersBLL.ProductTypeIdIsExist(productTypeId);
        if (isExistCount > 0)
        {
            //Judge the ProductTypeId whether has operation before delete by Id
            int getCount = SetParametersBLL.ProductTypeIdWhetherHasOperation(productTypeId);
            if (getCount > 0)
            {
                divProductType.Visible = false;
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert("对不起，该型号已经发生了业务，因此不能删除!"));
                return;
            }

            else
            {
                //删除指定产品型号信息
                int delCount = SetParametersBLL.DelProductTypeByID(Convert.ToInt32(e.CommandArgument));
                if (delCount > 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert("删除型号成功!"));
                }

                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert("删除型号失败，请联系管理员!"));
                    return;
                }
            }
        }

        else
        {
            divProductType.Visible = false;
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert("对不起，该型号不存在或者已经被删除!"));
            return;
        }

        DataBindProductType();
    }

    /// <summary>
    /// 修改指定的产品型号信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtEdit_Command(object sender, CommandEventArgs e)
    {
        ViewState["ID"] = 2;
        ViewState["ProductTypeID"] = e.CommandArgument;

        int productTypeId = Convert.ToInt32(e.CommandArgument);
        //Judge the ProductTypeId whether exist by Id before delete or update
        int isExistCount = SetParametersBLL.ProductTypeIdIsExist(productTypeId);
        if (isExistCount > 0)
        {
            //获取指定的产品型号信息            
            DataTable dt = SetParametersBLL.GetProductTypeInfoByID(productTypeId);
            if (dt.Rows.Count != 0)
            {
                divProductType.Visible = true;
                txtProductTypeName.Text = dt.Rows[0][0].ToString();
                txtProductTypeDescr.Text = dt.Rows[0][1].ToString();
            }
        }

        else
        {
            divProductType.Visible = false;
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert("对不起，该型号不存在或者已经被删除!"));
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
        bool flag = SetValueProductTypeModel();
        if (flag)
        {
            if ((int)ViewState["ID"] == 1)
            {
                ///获取指定产品型号的行数
                int getCount = SetParametersBLL.GetProductTypeCountByName(txtProductTypeName.Text.Trim());
                if (getCount > 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert("该型号已经存在!"));
                }

                else
                {
                    //添加型号信息               
                    int addCount = SetParametersBLL.AddProductType(productTypeModel);
                    if (addCount > 0)
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert("添加型号成功!"));
                        divProductType.Visible = false;
                    }

                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert("添加型号失败,请联系管理员!"));
                        return;
                    }
                }
            }

            if ((int)ViewState["ID"] == 2)
            {
                ///获取指定产品型号的行数
                int getCount = SetParametersBLL.GetProductTypeCountByIDName(Convert.ToInt32(ViewState["ProductTypeID"]), txtProductTypeName.Text.Trim());
                if (getCount > 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert("该型号已经存在!"));
                }

                else
                {
                    ///修改指定产品型号信息
                    int updCount = SetParametersBLL.UpdProductTypeByID(productTypeModel);
                    if (updCount > 0)
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert("修改型号成功!"));
                        divProductType.Visible = false;
                    }

                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert("修改型号失败,请联系管理员!"));
                    }
                }
            }
            DataBindProductType();
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
        DataTable dt = SetParametersBLL.OutToExcel_ProductType();
        if (dt.Rows.Count < 1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert("对不起，没有可以导出的数据!"));
            return;
        }

        else
        {
            Excel.OutToExcel(dt, "产品型号信息", new string[] { "ProductTypeName=产品型号名称", "ProductTypeDescr=产品型号说明" });
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
    protected void gvProductType_Sorting(object sender, GridViewSortEventArgs e)
    {        
        DataView dv = new DataView((DataTable)ViewState["sortProductType"]);
        string sortString = e.SortExpression;

        switch(sortString.ToLower().Trim())
        {
            case "producttypeid":
                if (blProductTypeID)
                {
                    dv.Sort = "ProductTypeID desc";
                    blProductTypeID = false;
                }

                else
                {
                    dv.Sort = "ProductTypeID asc";
                    blProductTypeID = true;
                }
                break;

            case "producttypename":
                if (blProductTypeName)
                {
                    dv.Sort = "ProductTypeName desc";
                    blProductTypeName = false;
                }

                else
                {
                    dv.Sort = "ProductTypeName asc";
                    blProductTypeName = true;
                }
                break;

            case "producttypedescr":
                if (blProductTypeDescr)
                {
                    dv.Sort = "ProductTypeDescr desc";
                    blProductTypeDescr = false;
                }

                else
                {
                    dv.Sort = "ProductTypeDescr asc";
                    blProductTypeDescr = true;
                }
                break;
        }
        this.gvProductType.DataSource = dv;
        this.gvProductType.DataBind();
    }
}
