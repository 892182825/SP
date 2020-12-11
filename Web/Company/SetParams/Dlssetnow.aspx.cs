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

public partial class Company_SetParams_Dlssetnow : BLL.TranslationBase
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
        //this.TranControls(this.gvMemberBank,
        //        new string[][]{
        //            new string []{"000015","操作"},
        //            new string []{"006917","银行编号"},
        //            new string []{"001406","银行名称"},
        //            new string []{"001690","银行所属国家"}
        //        });
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
        DataTable dv = DAL.DBHelper.ExecuteDataTable("select * from dlssettb order by id desc");
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

            DataRowView drv = (DataRowView)e.Row.DataItem;
            Label Label1 = (Label)e.Row.FindControl("Label1");
            Label Label2 = (Label)e.Row.FindControl("Label2");

            int opst =Convert.ToInt32(  drv["opct"]);
            DateTime dtt = Convert.ToDateTime(drv["lastuptime"]).AddHours(8);
            Label2.Text = dtt.ToString();
            Label1.Text = (opst == 0 ? "开启" : "关闭");
            Button btnclose=(Button) e.Row.FindControl("btnclose");
                  Button btnopen=(Button) e.Row.FindControl("btnopen");
                  if (opst == 0) btnclose.Visible = true;
                  else btnopen.Visible = true;

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

        int count = DAL.DBHelper.ExecuteNonQuery("delete from dlssettb where id="+ memberBankId);

        if (count > 0)
        {
            ScriptHelper.SetAlert(Page, "删除成功！");
        }
        else
        {
            ScriptHelper.SetAlert(Page, "删除失败！");
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
        if (txtBankName.Text.Trim()=="")
        {
            ScriptHelper.SetAlert(Page,"会员编号不能为空！");
            return;
        }

        int count = Convert.ToInt32(DAL.DBHelper.ExecuteScalar("select count(0) from memberinfo where number='"+ txtBankName.Text.Trim() + "'"));

        if (count == 0)
        {
            ScriptHelper.SetAlert(Page, "会员编号不存在！");
            return;
        }

        count = Convert.ToInt32(DAL.DBHelper.ExecuteScalar("select count(0) from dlssettb where number='" + txtBankName.Text.Trim() + "'"));

        if (count > 0)
        {
            ScriptHelper.SetAlert(Page, "请不要重复添加！");
            return;
        }

        count = DAL.DBHelper.ExecuteNonQuery("insert into dlssettb (number) values ('"+ txtBankName.Text.Trim() + "')");

        if (count > 0)
        {
            ScriptHelper.SetAlert(Page, "添加成功！");
            txtBankName.Text = "";
        }
        else
        {
            ScriptHelper.SetAlert(Page, "添加失败！");
           
        }

        DataBindMemberBank();
        
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
    protected void gvMemberBank_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id =Convert.ToInt32( e.CommandArgument);
        string cmd = e.CommandName.ToString();
        if(cmd=="Cl"){
            Updatestate(id, 1);
        }
        if (cmd == "OP") {
            Updatestate(id, 0);
        }

        ClientScript.RegisterStartupScript(this.GetType(),"","<script>alert('更新成功');</script>");
        DataBindMemberBank();
    }
    protected void Updatestate(int id ,int tp)
    {
        string sqls = "update dlssettb set opct=" + tp + "  ,lastuptime=getutcdate() where id =" + id;

         DAL.DBHelper.ExecuteNonQuery(sqls);
    }
}
