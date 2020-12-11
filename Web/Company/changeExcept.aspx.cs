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
using BLL.other.Company;
using BLL.CommonClass;
using Model.Other;

public partial class Company_changeExcept : BLL.TranslationBase
{
    protected string msg;
    ChangeExceptBLL changeExceptBLL = new ChangeExceptBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        //  权限
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.CustomerChangeExcept);

        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));

        if (!IsPostBack)
        {
            //绑定期数下拉列表
            CommonDataBLL.BindQishuList(this.DpExcept, false);
            GetBindGView("  1=1 and m.number=u.number ");
        }

        Translations();
    }
    /// <summary>
    /// 绑定数据
    /// </summary>
    /// <param name="condation"></param>
    public void GetBindGView(string condation) {
        Pager1.PageBind(0, 10, "updateexpect u,memberinfo m", "*", condation  , "u.ID", "GridView1");
    }

    /// <summary>
    /// 翻译
    /// </summary>
    private void Translations()
    {
        this.TranControls(this.btnOK, new string[][] { new string[] { "000434", "确 定" } });
        this.btnSearch.Text = GetTran("000048", "查 询");
        this.TranControls(this.GridView1, new string[][] { 
                new string[] { "000024", "会员编号" }, 
                new string[] { "001400", "昵称" }, 
                new string[] { "005939", "报单号" }, 
                new string[] { "002055", "报单期数" }, 
                new string[] { "002037", "调整报单期数" }, 
                new string[] { "002058", "支付期数" }, 
                new string[] { "002059", "调整支付期数" }, 
                new string[] { "002061", "调整时间" }, 
                new string[] { "002062", "调整人" },
                new string[] {"007210","期数调整原因"}
        });
    }


    /// <summary>
    /// 确定按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        ChangeExceptProcess(txtOrder.Text, this.txt_number.Text.Trim(), Convert.ToInt32(this.DpExcept.SelectedValue),txtUpdateExpectReason.Text);
    }

    protected object GetUpdateDate(object obj)
    {
        return Convert.ToDateTime(obj).AddHours(Convert.ToDouble(Session["WTH"])).ToString("yyyy-MM-dd HH:mm:ss");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private string GetManage()
    {
        if (Session["Company"] != null)
        {
            return Session["Company"].ToString();
        }
        return "";
    }

    /// <summary>
    /// 更换期数流程
    /// </summary>
    public void ChangeExceptProcess(string orderID, string number, int except, string updateExpectReason)
    {
        //判断登录者是否有更改此会员的权限
        if (this.txt_number.Text.Trim() == "")
        {
            msg = "<script>alert('"+GetTran("000723","会员编号不能为空")+"！');</script>";
            tbChange.Style.Add("display", "");
            tbSearch.Style.Add("display", "none");
            return;
        }
        if (BLL.CommonClass.CommonDataBLL.getCountNumber(txt_number.Text.Trim())<=0) {
            msg = "<script>alert('" + GetTran("000000", "会员编号不存在") + "！');</script>";
            return;
        }
        //bool isTrue = BLL.CommonClass.CommonDataBLL.CheckRole(txt_number.Text.Trim(), BLL.CommonClass.CommonDataBLL.GetNumberXuhao(Session["Company"].ToString()));
        //if (!isTrue)
        //{
        //    msg = "<script>alert('" + GetTran("007081", "对不起，您没有更改此会员的权限") + "！');</script>";
        //    tbChange.Style.Add("display", "");
        //    tbSearch.Style.Add("display", "none");
        //    return;
        //}
        //如果通过期数检查
        string error = changeExceptBLL.Check(orderID, number, except);
        if (error == "")
        {
            string optionuser = GetManage();
            string optionip = Request.UserHostAddress;
            if (changeExceptBLL.UpdateExcept(orderID, except, optionuser, optionip,updateExpectReason))
            {
                msg = "<script>alert('" + GetTran("002066", "期数更新成功！") + "');</script>";
                //绑定期数下拉列表
                txt_number.Text = "";
                ddlOrderId.Items.Clear();
                trOrderId.Style.Add("display", "none");
                GetBindGView("  1=1 and m.number=u.number ");
             
            }
            else
            {
                msg = "<script>alert('" + GetTran("002068", "期数更新失败！") + "');</script>";
                tbChange.Style.Add("display", "");
                tbSearch.Style.Add("display", "none");
            }
        }
        else
        {
            msg="<script>alert('" + error + "');</script>";
            tbChange.Style.Add("display", "");
            tbSearch.Style.Add("display", "none");
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
        }
    }

    private string GetNumber()
    {
        string manageID = Session["Company"].ToString();
        int count = 0;
        string number = BLL.CommonClass.CommonDataBLL.GetWLNumber(manageID, out count);
        if (count == 0)
        {
            return "  1=1  ";
        }
        return  number;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string tj = " 1=1 and m.number=u.number ";
        if (txtBianhao.Text != "")
        {
            tj += " and u.number like '%"+txtBianhao.Text+"%' ";   
        }
        if (txtOrderId.Text != "")
        {
            tj += " and u.orderid like '%" + txtOrderId.Text + "%' ";   
        }
        //tj +=  "  and "+ this.GetNumber();
        GetBindGView(tj);
       
        Pager1.PageBind(0, 10, "updateexpect u,memberinfo m", "*", tj, "u.ID", "GridView1");
        Translations();

    }

    protected string GetReason(string Reason)
    {
        string rtval = "";
        if (Reason.Length <=0)
        {
            rtval = GetTran("000221", "无");
        }
        else
        {
            rtval = "<a href='#' onclick='showControl(event,\"divOffReason\",\"" + Reason + "\")'>" + GetTran("000440", "查看") + "</a>";
        }
        return rtval;
    }
}
