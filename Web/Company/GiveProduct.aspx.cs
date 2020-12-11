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
//Add Namespace
using System.Data.SqlClient;
using BLL.other.Company;
using BLL.CommonClass;
using Model.Other;
using Model;

public partial class Company_SetParams_GiveProduct : BLL.TranslationBase
{
    ArrayList list = new ArrayList();
    /// <summary>
    /// 页面加载事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        //ajaxPro注册1111111111
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));

        //检查店铺
        if (!IsPostBack)
        {
            Session["giveProductList"] = null;
            BindCountry();
            ddlCountry_SelectedIndexChanged(null, null);
            Translaton();
        }
    }
    public void Translaton()
    {
        this.TranControls(btnSaveOrder, new string[][] { new string[] { "000064", "确认" } });
    }
    /// <summary>
    /// 绑定国家
    /// </summary>
    public void BindCountry()
    {

        this.ddlCountry.DataSource = StoreInfoEditBLL.bindCountry();
        this.ddlCountry.DataValueField = "countrycode";
        this.ddlCountry.DataTextField = "name";
        this.ddlCountry.DataBind();

    }
    /// <summary>
    /// 国家改变事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCountry.SelectedValue != "" || ddlCountry.SelectedValue != null)
        {
            setProductMenu(ddlCountry.SelectedValue);
        }
        else
        {
            string alertMessage = GetTran("001959", "对不起，没有可选国家!");
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + alertMessage + "');</script>");
            return;
        }
    }

    /// <summary>
    /// 产生树形菜单
    /// </summary>
    /// <param name="dian"></param>
    public void setProductMenu(string countryCode)
    {
        ProductTree myTree = new ProductTree();
        this.menuLabel.Text = myTree.GetCountryProduct(countryCode);
    }
    /// <summary>
    /// 确认
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSaveOrder_Click(object sender, EventArgs e)
    {
        if (Session["giveProductList"] == null || Session["giveProductList"].ToString() == "")
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001992", "对不起，您所填写的入库产品的数量不能全部是0!")));
            return;
        }
        else
        {
            list = (ArrayList)Session["giveProductList"];

            if (txtPvStart.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("000000", "请填写赠送起始PV！")));
                return;
            }
            double pvStart,pvEnd;
            if (!double.TryParse(txtPvStart.Text.Trim(),out pvStart))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("000000", "请填写正确的赠送起始PV！")));
                return;
            }
            if (pvStart < 0)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("000000", "赠送起始PV必须不小于0！")));
                return;
            }
            if (GiveProductBLL.GetLastEndPV(pvStart))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("000000", "赠送起始PV已存在！")));
                return;
            }
            if (!double.TryParse(txtPVEnd.Text.Trim(), out pvEnd))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("000000", "请填写正确的赠送结束PV！")));
                return;
            }
            if (pvEnd < 0)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("000000", "赠送结束PV必须不小于0！")));
                return;
            }
            if (GiveProductBLL.GetLastEndPV(pvEnd))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("000000", "赠送结束PV已存在！")));
                return;
            }
            if (pvEnd <= pvStart)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("000000", "赠送结束PV必须大于起始PV！")));
                return;
            }


            using (SqlConnection conn = new SqlConnection(DAL.DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                   SetGivePVModel sm=new SetGivePVModel();
                    sm.TotalPVStart=pvStart;
                    sm.TotalPVEnd=pvEnd;
                    sm.OperateIP=Request.UserHostAddress;
                    sm.OperateNum=Session["Company"].ToString();
                    int setGivePVID = GiveProductBLL.AddSetGivePV(tran, sm);
                    GiveProductBLL.CreateGiveProducts(tran, list, setGivePVID);
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("000000", "参数设置失败，请联系管理员!")));
                    return;
                }
                finally
                {
                    conn.Close();
                }
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "abs", "alert('" + GetTran("000000", "赠送产品设置成功!") + "');location.href='GiveProduct.aspx'", true);
        }
    }
}
