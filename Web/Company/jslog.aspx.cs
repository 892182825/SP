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
using System.Text;
using Model;
using Model.Other;
using BLL.other.Company;
using BLL.CommonClass;
using BLL;
using DAL;
using Standard.Classes;

public partial class Company_jslog : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);


        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        if (!IsPostBack)
        {
            CommonDataBLL.BindQishuList(this.DropDownExpectNum, true);
            BtnConfirm_Click(null, null);
        }
        Translations();

    }

    

    protected string SetVisible(string dd, string id)
    {
        if (dd.Length > 0)
        {
            string _openWin = "<a href =\"javascript:void(window.open('jslogremark.aspx?id=" + id + "','','width=1000,height=800'))\">" + GetTran("000440", "查看") + "</a>";
            return _openWin;
        }
        else
        {
            return GetTran("000221", "无");
        }
    }

    private void Translations()
    {
        this.TranControls(this.GridView1,
                new string[][]{
                    new string []{"000045", "期数"},
                    new string []{"001593", "状态"},
                    new string []{"003189", "操作者IP"},
                    new string []{"005858", "操作者编号"},
                    new string []{"000559", "开始时间"},
                    new string []{"005932", "结束时间"},

                    new string []{"000744", "查看备注"}
                   });

        this.TranControls(this.btnhf, new string[][] { new string[] { "000421", "返回" } });
    }
    //查询按钮
    protected void BtnConfirm_Click(object sender, EventArgs e)
    {
        getMemberInfo();
    }

  

    #region 获得会员
    public void getMemberInfo()
    {

        int ExpectNum = 0;
        if (this.DropDownExpectNum.SelectedValue.ToString() == "")
        {
            ExpectNum = 0;
        }
        else
        {
            ExpectNum = Convert.ToInt32(this.DropDownExpectNum.SelectedItem.Value);
        }
        StringBuilder sb = new StringBuilder();
        sb.Append(" 1=1 ");
          
        if (ExpectNum > 0)
        {
            sb.Append(" and qishu=" + PageBase.InputText(ExpectNum.ToString()));
        }

        ViewState["SQLSTR"] = "SELECT *,case jstype when 0 then '正在结算' when 1 then '正常结束' when 2 then '错误报单结束' when 3 then '异常结束' else '未启动' end as type FROM jiesuaninfo WHERE " + sb.ToString();
        string asg = ViewState["SQLSTR"].ToString();
        Pager pager = Page.FindControl("Pager1") as Pager;
        pager.Pageindex = 0;
        pager.PageSize = 10;
        pager.PageTable = "jiesuaninfo";
        pager.Condition = sb.ToString();
        pager.PageColumn = " *,case jstype when 0 then '正在结算' when 1 then '正常结束' when 2 then '错误报单结束' when 3 then '异常结束' else '未启动' end as type";
        pager.ControlName = "GridView1";
        pager.key = " ID ";
        pager.InitBindData = true;
        pager.PageBind();
        Translations();
    }
    #endregion

    
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
           

        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Attributes.Add("style", "background-image:url('images/tabledp.gif')");
        }
        Translations();
    }
    protected void lkSubmit1_Click(object sender, EventArgs e)
    {
        BtnConfirm_Click(null, null);
    }
    protected void btnDownExcel_Click(object sender, EventArgs e)
    {
        

    }
    protected void btnhf_Click(object sender, EventArgs e)
    {
        Response.Redirect("CompanyBalance.aspx");
    }
}
