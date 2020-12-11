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
using System.Data.SqlClient;
using BLL.CommonClass;
using Model.Other;
using DAL;
using Model;


public partial class Company_GradingLevel : BLL.TranslationBase
{
    CommonDataBLL bll = new CommonDataBLL();
    public string msg = "", type = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        Response.Cache.SetExpires(DateTime.Now);
        type = Request.QueryString["type"] == "" ? "" : Request.QueryString["type"];
        ViewState["Type"] = Request.QueryString["type"] == "" ? "" : Request.QueryString["type"];
        if (ViewState["Type"].ToString() != "Member" && ViewState["Type"].ToString() != "Store")
        {
            Response.Redirect("index.aspx");
            return;
        }
        if (!this.IsPostBack)
        {
            if (ViewState["Type"].ToString() != "" && ViewState["Type"].ToString() == "Member")
            {
                Permissions.CheckManagePermission(EnumCompanyPermission.MemberGrading);
                DataTable dt = DAL.CommonDataDAL.GetLeverList();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dplLevel.Items.Add(new ListItem(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));

                }
                lit_type.Text = GetTran("000764", "会员定级");
                lit_number.Text = GetTran("000024", "会员编号");
                Label1.Text = "第" + CommonDataBLL.getMaxqishu() + "期";
            }
            else if (ViewState["Type"].ToString() != "" && ViewState["Type"].ToString() == "Store")
            {
                Permissions.CheckManagePermission(EnumCompanyPermission.CustomerStoreLevel);
                DataTable dt = DBHelper.ExecuteDataTable("select levelint,levelstr from bsco_level where levelflag=1 order by levelint");
                this.dplLevel.DataTextField = "levelstr";
                this.dplLevel.DataValueField = "levelint";
                this.dplLevel.DataSource = dt;
                this.dplLevel.DataBind();
                lit_type.Text = GetTran("001157", "服务机构定级");
                lit_number.Text = GetTran("000150", "店铺编号");
                tr_expect.Visible = false;
            }
            Translations();
        }
    }

    private void Translations()
    {
        this.TranControls(this.Button1, new string[][] { new string[] { "000421", "返回" } });
        this.TranControls(this.ButtonSubmit, new string[][] { new string[] { "000771", "调整级别" } });
    }

    /// <summary>
    /// 提交数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonSubmit_Click(object sender, EventArgs e)
    {
        if (txtNumber.Text.Trim() == "")
        {
            Page.ClientScript.RegisterStartupScript(GetType(), null, "<script language='javascript'>alert('编号不能为空!')</script>", false);
            return;
        }
        DataTable dt = CommonDataBLL.GetBalanceLevel(txtNumber.Text.Trim());
        if (dt.Rows.Count == 0 && ViewState["Type"].ToString() == "Member")
        {
            Page.ClientScript.RegisterStartupScript(GetType(), null, "<script language='javascript'>alert('会员编号不能存在!')</script>", false);
            return;
        }

        DataTable dt1 = CommonDataBLL.GetLevel("StoreInfo", txtNumber.Text.Trim());
        if (dt1.Rows.Count == 0 && ViewState["Type"].ToString() == "Store")
        {
            Page.ClientScript.RegisterStartupScript(GetType(), null, "<script language='javascript'>alert('服务机构编号不能存在!')</script>", false);
            return;
        }
        string howMuch = dplLevel.SelectedItem.Text;
        int NowLevel = Convert.ToInt32(this.dplLevel.SelectedValue);
        DateTime nowTime = DateTime.Now.ToUniversalTime();
        GradingModel model = new GradingModel();
        model.Number = txtNumber.Text.Trim();
        model.LevelNum = NowLevel;
        model.ExpectNum1 = CommonDataBLL.getMaxqishu();
        model.InputDate1 = nowTime;
        model.OperaterNum1 = Session["Company"].ToString();
        model.OperateIP1 = Request.UserHostAddress;
        model.Mark1 = TextBox1.Text.Trim();
        if (ViewState["Type"].ToString() == "Member")
        {
            model.GradingStatus = Grading.MemberLevel;
            DataTable dt_one = DAL.DBHelper.ExecuteDataTable("select MemberState from memberinfo where  number='" + txtNumber.Text + "'");
            string MemberState = dt_one.Rows[0]["MemberState"].ToString();//汇款id
            if (MemberState == "0")
            {

                msg = "<script>alert('该会员未激活，无法定级！');</script>";

                return;
            }
        }
        else if (ViewState["Type"].ToString() == "Store")
        {
            model.GradingStatus = Grading.StoreLevel;
            DataTable dt_one = DAL.DBHelper.ExecuteDataTable("select StoreState from StoreInfo where  StoreID='" + txtNumber.Text + "'");
            string StoreState = dt_one.Rows[0]["StoreState"].ToString();//汇款id
            if (StoreState == "0")
            {

                msg = "<script>alert('该服务机构未激活，无法定级！');</script>";

                return;
            }

        }

        //int isMod = Convert.ToInt32(DBHelper.ExecuteScalar("select StoreState from StoreInfo where  and  StoreID='" + txtNumber.Text + "'"));



        //进入级别表
        if (!CommonDataBLL.UPdateIntoLevel(model))
        {
            msg = "<script>alert('" + GetTran("007148", "调级别失败") + "！');</script>";

            return;
        }


        else
        {
            if (ViewState["Type"].ToString() == "Member")
            {
                msg = "<script>alert('" + GetTran("000797", "该会员调整后的级别为") + "：" + howMuch + "');window.location='LeveLQuery.aspx?type=Member';</script>";

                LabelResponse.Text = "" + GetTran("000797", "该会员调整后的级别为") + " <b>" + howMuch + "</b>.";
            }
            else if (ViewState["Type"].ToString() == "Store")
            {
                msg = "<script>alert('" + GetTran("001165", "该店铺调整后的级别为") + "：" + howMuch + "');window.location='LeveLQuery.aspx?type=Store';</script>";

                LabelResponse.Text = "" + GetTran("001175", "该店铺的级别为") + " <b>" + howMuch + "</b>.";
            }
            ClearTxt();
        }



    }
    /// <summary>
    /// 返回按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (ViewState["Type"].ToString() == "Member")
        {
            Response.Redirect("LevelQuery.aspx?type=Member");
        }
        else if (ViewState["Type"].ToString() == "Store")
        {
            Response.Redirect("LevelQuery.aspx?type=Store");
        }
    }
    /// <summary>
    /// 清楚控件的值
    /// </summary>
    private void ClearTxt()
    {
        txtNumber.Text = "";
        lblName.Text = "";
        TextBox1.Text = "";
        LabelResponse.Text = "";
    }
}