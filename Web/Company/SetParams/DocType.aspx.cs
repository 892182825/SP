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
 * 创建时间：   2009-09-17
 * 对应菜单:    系统管理->参数设置
 */

public partial class Company_SetParams_DocType : BLL.TranslationBase
{
    /// <summary>
    /// 实例化单据类型模型
    /// </summary>
    DocTypeTableModel docTypeTableModel = new DocTypeTableModel();

    /// <summary>
    /// 为GridView排序事件声明变量和赋值
    /// </summary>
    private static bool blDocTypeID=true; 
    private static bool blDocTypeName=true;
    private static bool blIsRubric=true;
    private static bool blDocTypeDescr = true;

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, "../index.aspx");
        ///设置GridView的样式
        gvDocTypeTable.Attributes.Add("style", "word-break:keep-all;word-wrap:normal");

        if (!Page.IsPostBack)
        {
            divDocTypeTable.Visible = false;            
            DataBindDocTypeTable();
        }
        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.gvDocTypeTable,
                new string[][]{
                    new string []{"000015","操作"},
                    new string []{"000407","单据编号"},
                    new string []{"005220","单据名称"},
                    new string []{"004146","是否红单"},
                    new string []{"005223","单据说明"}});
        this.TranControls(this.btnAdd, new string[][] { new string[] { "002047", "添 加" } });
        this.TranControls(this.btnReturn, new string[][] { new string[] { "000096", "返 回" } });
        this.TranControls(this.btnOK, new string[][] { new string[] { "002190", "确 定" } });
        this.TranControls(this.btnReset, new string[][] { new string[] { "001759", "清 空" } });
        this.TranControls(this.ddlIsRubric,
                new string[][]{
                    new string []{"000235","否"},
                    new string []{"000233","是"}});
        this.TranControls(this.lblDocTypeName, new string[][] { new string[] { "005220", "单据名称：" } });
        this.TranControls(this.lblDocTypeDescr, new string[][] { new string[] { "005384", "单据描述：" } });
        this.TranControls(this.lblIsRubric, new string[][] { new string[] { "004146", "是否红单：" } });

    }

    /// <summary>
    /// 绑定数据
    /// </summary>
    protected void DataBindDocTypeTable()
    {
        ///获取单据类型信息       
        DataTable dtDocTypeTable = SetParametersBLL.GetDocTypeTableInfo();
        ViewState["sortDocTypeTable"] = dtDocTypeTable;
        DataView dv = new DataView((DataTable)ViewState["sortDocTypeTable"]);
        if (ViewState["SortDocTypeTableString"] == null)
            ViewState["SortDocTypeTableString"] = dtDocTypeTable.Columns[0].ColumnName.Trim();
        dv.Sort = ViewState["SortDocTypeTableString"].ToString();
        this.gvDocTypeTable.DataSource = dv;
        this.gvDocTypeTable.DataBind();
        Translations();
    }

    /// <summary>
    /// 给单据模型层赋值
    /// </summary>
    protected bool SetValueDocTypeTable()
    {
        if (txtDocTypeName.Text.Trim() == "")
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("005398", "单据类型名称不能为空!")));
            return false;
        }

        else
        {
            docTypeTableModel.DocTypeID = Convert.ToInt32(ViewState["DocTypeID"]);
            docTypeTableModel.DocTypeName = txtDocTypeName.Text.Trim();
            docTypeTableModel.IsRubric = Convert.ToInt32(ddlIsRubric.SelectedValue);
            docTypeTableModel.DocTypeDescr = txtDocTypeDescr.Text.Trim();
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
        txtDocTypeName.Text = "";
        txtDocTypeDescr.Text = "";
    }

    /// <summary>
    /// 添加单据类型记录
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ResetAll();
        ViewState["ID"] = 1;
        divDocTypeTable.Visible = true;
    }

    protected void gvDocTypeTable_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            int primaryKey = Convert.ToInt32(this.gvDocTypeTable.DataKeys[e.Row.RowIndex].Value.ToString());
            ((LinkButton)e.Row.FindControl("lbtEdit")).CommandArgument = primaryKey.ToString();
            ((LinkButton)e.Row.FindControl("lbtDelete")).CommandArgument = primaryKey.ToString();
        }
       
        ///控制样式
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");

            string hidName = (e.Row.FindControl("hidName") as HiddenField).Value;
            if (hidName == "入库单" || hidName == "出库单" || hidName == "报溢单" || hidName == "报损单" || hidName == "调拨单" || hidName == "退货单" || hidName == "换货退货单")
            {
                ((LinkButton)e.Row.FindControl("lbtEdit")).Enabled = false;
                ((LinkButton)e.Row.FindControl("lbtDelete")).Enabled = false;
            }


            Label lb = e.Row.FindControl("lblIsRubric") as Label;
            if (lb.Text.ToString() == "0")
            {
               lb.Text= GetTran("000235", "否");
            }
            else
            {
                lb.Text = GetTran("000233", "是");
            }
        }
        Translations();
    }

    /// <summary>
    /// 删除指定的单据类型信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtDelete_Command(object sender, CommandEventArgs e)
    {
        //Judge the DocTypeID whether exist by DocTypeID before delete or update
        int docTypeId = Convert.ToInt32(e.CommandArgument);
        int isExistCount = SetParametersBLL.DocTypeIDIsExist(docTypeId);
        if (isExistCount > 0)
        {
            int getCount = SetParametersBLL.DocTypeIDWhetherHasOperation(docTypeId);
            if (getCount > 0)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("005399","对不起，该单据发生了业务，因此不能删除！")));
                divDocTypeTable.Visible = false;
                return;
            }

            else
            {
                BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("DocTypeTable", "DocTypeID");
                cl_h_info.AddRecord(docTypeId);
                //删除指定单据类型信息
                int delCount = SetParametersBLL.DelDocTypeTableByID(docTypeId);
                if (delCount > 0)
                {
                    cl_h_info.AddRecord(docTypeId);
                    cl_h_info.DeletedIntoLogs(ChangeCategory.company28, Session["Company"].ToString(), ENUM_USERTYPE.objecttype9);

                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("005400", "删除单据类型成功!")));
                    divDocTypeTable.Visible = false;
                }

                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("005401", "删除单据类型失败，请联系管理员!")));
                    return;
                }
            }
        }
      
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("005402", "对不起，该单据不存在或该单据已经被删除！")));
            return;                       
        }
        
        DataBindDocTypeTable();
    }

    /// <summary>
    /// 修改指定的单据类型信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtEdit_Command(object sender, CommandEventArgs e)
    {
        divDocTypeTable.Visible = true;
        ViewState["ID"] = 2;
        ViewState["DocTypeID"] = e.CommandArgument;

        //Judge the DocTypeID whether exist by DocTypeID before delete or update
        int docTypeId = Convert.ToInt32(e.CommandArgument);
        int isExistCount = SetParametersBLL.DocTypeIDIsExist(docTypeId);

        if (isExistCount > 0)
        {
            //获取指定的单据类型信息
            DataTable dt = SetParametersBLL.GetDocTypeTableInfoByID(docTypeId);
            if (dt.Rows.Count != 0)
            {
                txtDocTypeName.Text = dt.Rows[0][0].ToString();
                ddlIsRubric.SelectedValue = dt.Rows[0][1].ToString();
                txtDocTypeDescr.Text = dt.Rows[0][2].ToString();
            }
        }

        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("005402", "对不起，该单据不存在或该单据已经被删除！")));
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
        ///给单据模型层赋值
        bool TrueOrFalse=SetValueDocTypeTable();
        if (TrueOrFalse)
        {
            if ((int)ViewState["ID"] == 1)
            {
                ///获取指定单据类型的行数
                int getCount = SetParametersBLL.GetDocTypeTableCountByName(txtDocTypeName.Text.Trim());
                if (getCount > 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("005403","该单据类型已经存在!")));
                }

                else
                {
                    ///添加单据类型信息
                    int addCount = SetParametersBLL.AddDocTypeTable(docTypeTableModel);
                    if (addCount > 0)
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("005404","添加单据类型成功!")));
                        divDocTypeTable.Visible = false;
                    }

                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("005405", "添加单据类型失败,请联系管理员!")));
                    }
                }
            }

            if ((int)ViewState["ID"] == 2)
            {
                ///获取指定单据类型的行数
                int getCount = SetParametersBLL.GetDocTypeTableCountByIDName(Convert.ToInt32(ViewState["DocTypeID"]), txtDocTypeName.Text.Trim());
                if (getCount > 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("005403", "该单据类型已经存在!")));
                }

                else
                {
                    BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("DocTypeTable", "DocTypeID");
                    cl_h_info.AddRecord(docTypeTableModel.DocTypeID);
                    ///修改指定单据类型信息
                    int updCount = SetParametersBLL.UpdDocTypeTableByID(docTypeTableModel);
                    if (updCount > 0)
                    {
                        cl_h_info.AddRecord(docTypeTableModel.DocTypeID);
                        cl_h_info.ModifiedIntoLogs(ChangeCategory.company28, Session["Company"].ToString(), ENUM_USERTYPE.objecttype9);

                        Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("005406", "修改单据类型成功!")));
                        divDocTypeTable.Visible = false;
                    }

                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("005407", "修改单据类型失败,请联系管理员!")));
                        return;
                    }
                }
            }
            DataBindDocTypeTable();
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
        DataTable dt = SetParametersBLL.OutToExcel_DocTypeTable();
        if (dt.Rows.Count < 1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001712", "对不起，没有可以导出的数据!")));
            return;
        }

        else
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["IsRubric"].ToString() == "0")
                {
                    dt.Rows[i]["IsRubric"] = GetTran("000235", "否");
                }

                else
                {
                    dt.Rows[i]["IsRubric"] = GetTran("000233", "是");
                }
            }

            Excel.OutToExcel(dt, GetTran("005408", "单据类型信息"), new string[] { "DocTypeName=" + GetTran("005220", "单据名称"), "IsRubric=" + GetTran("004146", "是否红单"), "DocTypeDescr=" + GetTran("005223", "单据说明") });
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
    /// 排序事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvDocTypeTable_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataView dv = new DataView(((DataTable)ViewState["sortDocTypeTable"]));
        string sortString = e.SortExpression;

        switch (sortString.ToLower().Trim())
        {
            case "doctypeid":
                if(blDocTypeID)
                {
                    dv.Sort="DocTypeID desc";
                    blDocTypeID=false;
                }

                else
                {
                    dv.Sort="DocTypeID asc";
                    blDocTypeID=true;
                }
                break;

            case "doctypename":
                if (blDocTypeName)
                {
                    dv.Sort = "DocTypeName desc";
                    blDocTypeName = false;
                }

                else
                {
                    dv.Sort = "DocTypeName asc";
                    blDocTypeName = true;
                }
                break;

            case "isrubric":
                if (blIsRubric)
                {
                    dv.Sort = "IsRubric desc";
                    blIsRubric = false;
                }

                else
                {
                    dv.Sort = "IsRubric asc";
                    blIsRubric = false;
                }
                break;

            case "doctypedescr":
                if (blDocTypeDescr)
                {
                    dv.Sort = "DocTypeDescr desc";
                    blDocTypeDescr = false;
                }

                else
                {
                    dv.Sort = "DocTypeDescr asc";
                    blDocTypeDescr = true;
                }
                break;
        }
        this.gvDocTypeTable.DataSource = dv;
        this.gvDocTypeTable.DataBind(); 
    }
}
