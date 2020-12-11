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
using BLL.CommonClass;
using BLL.other.Company;

/*
 * 创建者：     汪  华
 * 创建时间：   2009-09-23
 * 对应菜单:    系统管理->参数设置
 */

public partial class Company_SetParams_MemberBank : BLL.TranslationBase 
{
    /// <summary>
    /// 实例化会员使用银行模型
    /// </summary>
    MemberBankModel memberBankModel = new MemberBankModel();

    /// <summary>
    /// 为GridView排序事件声明变量和赋值
    /// </summary>
    private static bool blBankID = true;
    private static bool blBankName = true;
    private static bool blCountryName = true;    

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, "../index.aspx");
        ///设置GridView的样式
        gvMemberBank.Attributes.Add("style", "word-break:keep-all;word-wrap:normal");

        if (!Page.IsPostBack)
        {
            divMemberBank.Visible = false;                   
            DataBindMemberBank();
            ViewState["ID"] = 0;
        }
        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.gvMemberBank,
                new string[][]{
                    new string []{"000015","操作"},
                    new string []{"006917","银行编号"},
                    new string []{"001406","银行名称"},
                    new string []{"001690","银行所属国家"}
                });
        this.TranControls(this.btnAdd, new string[][] { new string[] { "002047", "添 加" } });
        this.TranControls(this.btnReturn, new string[][] { new string[] { "000421", "返回" } });
        this.TranControls(this.btnOK, new string[][] { new string[] { "002190", "确 定" } });
        this.TranControls(this.btnReset, new string[][] { new string[] { "001759", "清 空" } });
    }

    /// <summary>
    /// 绑定数据
    /// </summary>
    protected void DataBindMemberBank()
    {
        ///获取会员使用银行信息       
        DataTable dtMemberBank = SetParametersBLL.GetMemberBankInfo();
        ViewState["sortMemberBank"] = dtMemberBank;
        DataView dv = new DataView((DataTable)ViewState["sortMemberBank"]);
        if (ViewState["sortMemberBankstring"] == null)
            ViewState["sortMemberBankstring"] = dtMemberBank.Columns[0].ColumnName.Trim();
        dv.Sort = ViewState["sortMemberBankstring"].ToString();
        this.gvMemberBank.DataSource = dv;
        this.gvMemberBank.DataBind();
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
        txtBankName.Text = "";
    }

    /// <summary>
    /// 添加银行记录
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ResetAll();
        divMemberBank.Visible = true;
        ViewState["ID"] = 1;
    }

    protected void gvMemberBank_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            int primaryKey = Convert.ToInt32(this.gvMemberBank.DataKeys[e.Row.RowIndex].Value.ToString());
            ((LinkButton)e.Row.FindControl("lbtEdit")).CommandArgument = primaryKey.ToString();
            ((LinkButton)e.Row.FindControl("lbtDelete")).CommandArgument = primaryKey.ToString();
        }

        ///控制样式
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
        }
    }

    /// <summary>
    /// 删除指定的会员使用银行信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtDelete_Command(object sender, CommandEventArgs e)
    {
        int memberBankId = Convert.ToInt32(e.CommandArgument);
        //Judge the MemberBankId whether exist by Id before delete or update
        int isExistCount = SetParametersBLL.MemberBankIdIsExist(memberBankId);
        if (isExistCount > 0)
        {
            //Juage the MemberBankId whether has operation before delete by Id
            int getCount = SetParametersBLL.MemberBankIdWhetherHasOperation(memberBankId);
            if (getCount > 0)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("005387", "对不起，该银行发生了业务，因此不能删除!")));
                divMemberBank.Visible = false;
                return;
            }

            else
            {
                BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("MemberBank", "BankID");
                cl_h_info.AddRecord(memberBankId);
                //删除指定会员使用银行信息
                int delCount = SetParametersBLL.DelMemberBankByID(memberBankId);
                if (delCount > 0)
                {
                    cl_h_info.DeletedIntoLogs(ChangeCategory.company28, Session["Company"].ToString(), ENUM_USERTYPE.objecttype9);
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("005388", "删除银行成功!")));
                    divMemberBank.Visible = false;
                }

                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("005389", "删除银行失败，请联系管理员!")));
                    return;
                }
            }
        }

        else
        {
            divMemberBank.Visible = false;
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("005390", "对不起，该银行不存在或者已经被删除!")));
            return;
        }

        DataBindMemberBank();
    }

    /// <summary>
    /// 修改指定的会员使用银行信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtEdit_Command(object sender, CommandEventArgs e)
    {
        ViewState["ID"] = 2;
        ViewState["BankID"] = e.CommandArgument;

        int memberBankId = Convert.ToInt32(e.CommandArgument);
        //Judge the MemberBankId whether exist by Id before delete or update
        int isExistCount = SetParametersBLL.MemberBankIdIsExist(memberBankId);
        if (isExistCount > 0)
        {
            divMemberBank.Visible = true;
            //获取指定的会员使用银行信息
            DataTable dt = SetParametersBLL.GetMemberBankInfoByID(memberBankId);
            if (dt.Rows.Count != 0)
            {
                txtBankName.Text = dt.Rows[0][0].ToString();
                (ddlCountry.Controls[0] as DropDownList).SelectedValue = dt.Rows[0][1].ToString();
            }
        }

        else
        {
            divMemberBank.Visible = false;
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("005390", "对不起，该银行不存在或者已经被删除!")));
            return;
        }
    }

    /// <summary>
    /// 给会员使用银行模型层赋值
    /// </summary>
    protected bool SetValueMemberBankModel()
    {
        if (txtBankName.Text.Trim() == "" || txtBankName.Text.Trim() == null)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("005391", "银行名称不能为空!")));
            return false;
        }

        else
        {
            memberBankModel.BankID = Convert.ToInt32(ViewState["BankID"]);
            memberBankModel.BankName = txtBankName.Text.Trim();
            memberBankModel.CountryCode = Convert.ToInt32((ddlCountry.Controls[0] as DropDownList).SelectedValue);
            return true;
        }        
    }

    /// <summary>
    /// 提交事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        ///给会员使用银行模型层赋值
        bool flag=SetValueMemberBankModel();
        if (flag)
        {
            if ((int)ViewState["ID"] == 1)
            {
                ///获取指定会员使用银行的行数
                int getCount = SetParametersBLL.GetMemberBankCountByNameCountryCode(memberBankModel);
                if (getCount > 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("005392", "该银行已经存在!")));
                }

                else
                {
                    ///添加银行信息                   
                    int addCount = SetParametersBLL.AddMemberBank(memberBankModel);
                    if (addCount > 0)
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("005393", "添加银行成功!")));
                        divMemberBank.Visible = false;
                    }

                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("005394", "添加银行失败,请联系管理员!")));
                        return;
                    }
                }
            }

            if ((int)ViewState["ID"] == 2)
            {
                ///获取指定会员使用银行的行数
                int getCount = SetParametersBLL.GetMemberBankCountByAll(memberBankModel);
                if (getCount > 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("005392", "该银行已经存在!")));
                }

                else
                {
                    BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("MemberBank", "BankID");
                    cl_h_info.AddRecord(memberBankModel.BankID);
                    ///修改指定会员使用银行信息               
                    int updCount = SetParametersBLL.UpdMemberBankByID(memberBankModel);
                    if (updCount > 0)
                    {
                        cl_h_info.AddRecord(memberBankModel.BankID);
                        cl_h_info.ModifiedIntoLogs(ChangeCategory.company28, Session["Company"].ToString(), ENUM_USERTYPE.objecttype9);

                        Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("005395", "修改银行成功!")));
                        this.divMemberBank.Visible = false;
                    }

                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("005396", "修改银行失败,请联系管理员!")));
                    }
                }
            }
            DataBindMemberBank();
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
        DataTable dt = SetParametersBLL.OutToExcel_MemberBank();
        if (dt.Rows.Count < 1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001712", "对不起，没有可以导出的数据!")));
            return;
        }

        else
        {
            Excel.OutToExcel(dt, GetTran("005397", "银行信息"), new string[] { "BankName=" + GetTran("001406", "银行名称"), "CountryName=" + GetTran("001690", "所属国家") });               
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
    protected void gvMemberBank_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataView dv = new DataView(((DataTable)ViewState["sortMemberBank"]));
        string sortString = e.SortExpression;

        switch (sortString.ToLower().Trim())
        {
            case "bankid":
                if (blBankID)
                {
                    dv.Sort = "BankID desc";
                    blBankID = false;
                }

                else
                {
                    dv.Sort = "BankID asc";
                    blBankID = true;
                }
                break;

            case "bankname":
                if (blBankName)
                {
                    dv.Sort = "BankName desc";
                    blBankName = false;
                }

                else
                {
                    dv.Sort = "BankName asc";
                    blBankName = true;
                }
                break;

            case "countryname":
                if (blCountryName)
                {
                    dv.Sort = "CountryName desc";
                    blCountryName = false;
                }

                else
                {
                    dv.Sort = "CountryName asc";
                    blCountryName = true;
                }
                break;          
        }
        this.gvMemberBank.DataSource = dv;
        this.gvMemberBank.DataBind(); 
    }
}
