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
using BLL.Logistics;
using BLL.Registration_declarations;
using BLL.other.Company;

public partial class Member_Joinreceipt : BLL.TranslationBase
{
    protected string msg;

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        //首次页面加载
        if (!IsPostBack)
        {
            Bind();
            this.DataBind();
        }
        Translations();

        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        AddOrderBLL.BindCurrency_Rate(Dropdownlist1);
        Dropdownlist1.SelectedValue = CountryBLL.GetCurrency();

    }
    private void Translations()
    {

        this.TranControls(this.gvOrder,
               new string[][]{
                    new string []{"000000","选择"},
                    new string []{"000079","订单号"},
                    new string []{"002102","订单消费金额"},
                    new string []{"002103","订单消费PV"},
                    new string []{"001850","收货人"},
                    new string []{"000393","收货人地址"},
                    new string []{"000403","收货人电话"},
                    new string []{"000352","是否付款"},
                    new string []{"000120","运费"},
                    new string []{"000118","重量"},
                    new string []{"007206","快递单号"},
                    new string []{"002109","是否有问题"}
                });
        this.TranControls(this.btnReceived, new string[][] { new string[] { "001832", "货已收到" } });
    }

    /// <summary>
    /// 订货确认
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnReceived_Click(object sender, EventArgs e)
    {
        string orderids = "";
        int count = 0;
        for (int i = 0; i < gvOrder.Rows.Count; i++)
        {
            CheckBox ckIsChoose = gvOrder.Rows[i].Controls[0].FindControl("ckIsChoose") as CheckBox;
            if (ckIsChoose.Checked)
            {
                orderids += gvOrder.Rows[i].Cells[1].Text + ",";
                count++;
            }
        }
        if (count == 0)
        {
            msg = "<script>alert('" + GetTran("001842", "请选择要确认的货单！") + "');</script>";
            return;
        }

        if (new AffirmConsignBLL().NSubmit(orderids, GetNumber()))
        {
            msg = "<script>alert('" + GetTran("000054", "确认成功！") + "');window.location.href=window.location.href</script>";
            Bind();
        }
        else
        {
            msg = "<script>alert('" + GetTran("000993", "确认失败！") + "');</script>";
            Bind();
        }
    }

    /// <summary>
    /// 绑定GridView
    /// </summary>
    private void Bind()
    {
        lblStoreId.Text = GetNumber();

        DataTable ddt = AffirmConsignBLL.GetMemberOrderList(GetNumber(), 2);

        if (ddt.Rows.Count == 0)
            btnReceived.Visible = false;

        gvOrder.DataSource = ddt;
        gvOrder.DataBind();
    }

    /// <summary>
    /// 绑定Y,N换是否
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    protected object CheckYorN(object obj)
    {
        if (obj.ToString() == "Y")
        {
            return GetTran("000233", "是");
        }
        else if (obj.ToString() == "N")
        {
            return GetTran("000235", "否");
        }
        else
        {
            return "";
        }
    }


    /// <summary>
    /// 获取会员编号
    /// </summary>
    /// <returns></returns>
    private string GetNumber()
    {
        if (Session["Member"] == null)
        {
            return "";
        }
        else
        {
            return Session["Member"].ToString();
        }
    }

    protected void lkbtnQuestion_Click(object sender, EventArgs e)
    {
        LinkButton lkbtnQuestion = (LinkButton)sender;
        Response.Redirect("AddQuestion.aspx?orderId=" + lkbtnQuestion.CommandArgument);
    }

    protected void gvOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC';");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg;");
        }
    }
}
