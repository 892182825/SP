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
using Model;
using BLL.CommonClass;

public partial class Member_FindRecharge : BLL.TranslationBase
{
    public string msg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //绑定数据
            BindData();
        }
        Translate();
    }

    protected void BindData()
    {
        if (!String.IsNullOrEmpty(txtPhoneNumber.Text) && !PhoneRechargeBLL.CheckPhoneNumber(txtPhoneNumber.Text))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("006545", "手机号码格式错误") + "！')</script>");
            return;
        }

        this.Pager1.Visible = true;

        string number = Session["Member"].ToString();
        string strwhere = " ";

        if (dllState.SelectedValue != "-1")
        {
            strwhere += " and  AddState=" + dllState.SelectedValue + " ";
        }
        if (!String.IsNullOrEmpty(txtPhoneNumber.Text))
        {
            strwhere += " and  PhoneNumber=" + txtPhoneNumber.Text + " ";
        }

        BLL.Registration_declarations.PagerParmsInit model = PhoneRechargeBLL.FindPhoneRecharge(strwhere);
        this.Pager1.ControlName = "gv_browOrder";
        this.Pager1.key = model.Key;
        this.Pager1.PageColumn = model.PageColumn;
        this.Pager1.Pageindex = 0;
        this.Pager1.PageTable = model.PageTable;
        this.Pager1.Condition = model.SqlWhere;
        this.Pager1.PageSize = model.PageSize;
        this.Pager1.PageCount = 0;
        this.Pager1.PageBind();

        Translate();
    }

    /// <summary>
    /// GridView行处理事件
    /// </summary>
    protected void gv_browOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
        }

        if (e.Row.RowType == DataControlRowType.Header)
        {
            this.Translate();
        }
    }

    /// <summary>
    /// 翻译方法
    /// </summary>
    public void Translate()
    {
        this.TranControls(this.dllState, new string[][] { new string[] { "000633", "全部" }, new string[] { "008034", "手机充值失败" }, new string[] { "008036", "手机充值中" }, new string[] { "008037", "手机充值成功" } });
        this.TranControls(this.btnQuery, new string[][] { new string[] { "000011", "搜索" } });
        this.TranControls(this.gv_browOrder, new string[][] 
        { 
             new string[] { "008038", "充值单号" }, 
             new string[] { "000024", "会员编号" },
             new string[] { "005623", "手机号码" },
             new string[] { "008039", "充值金额" },  
             new string[] { "007369", "充值状态" }, 
             new string[] { "008040", "充值时间" }
        });
    }

    public string GetRegisterDate(string rdate)
    {
        return Convert.ToDateTime(rdate).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString();
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindData();
    }
}

