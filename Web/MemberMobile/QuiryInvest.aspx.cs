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
using BLL.Registration_declarations;
using System.Collections.Generic;
using Model;
using DAL;
using Encryption;

public partial class MemberMobile_QuiryInvest : BLL.TranslationBase
{
    BLL.CommonClass.CommonDataBLL commonDataBLL = new BLL.CommonClass.CommonDataBLL();
    BrowseMemberOrdersBLL bll = new BrowseMemberOrdersBLL();
    BLL.CommonClass.CommonDataBLL cbll = new BLL.CommonClass.CommonDataBLL();
    ViewFuXiaoBLL viewFuXiaoBLL = new ViewFuXiaoBLL();
    int type = 0;

    public string msg = "";
    public int bzCurrency = 0;




    protected void Page_Load(object sender, EventArgs e)
    {

        Permissions.MemRedirect(Page, Permissions.redirUrl);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        //获取标准币种
        bzCurrency = CommonDataBLL.GetStandard();

        if (!IsPostBack)
        {
            if (Session["Default_Currency"] == null) Session["Default_Currency"] = bzCurrency;

            CommonDataBLL.DelOrderByState();

            if (Request.QueryString["dd"] != null)
            {
                type = 1;
            }
            Initddl();
            string manageId = BLL.CommonClass.CommonDataBLL.getManageID(3);
            string number = Session["Member"].ToString();
            //  QueryWhere(number, int.Parse(ddlVolume.SelectedValue), "", "", "");//绑定当期的数据

            //this.Pager1.Visible = false;

            Translate();
        }
    }
    private void Translate()
    {
      
    }
    protected void Initddl()
    {
        
    }
    protected void BindVolume()
    {
        CommonDataBLL CommonDataBLL = new CommonDataBLL();
        Hashtable hash = new Hashtable();
        BLL.CommonClass.CommonDataBLL bll = new BLL.CommonClass.CommonDataBLL();
        hash = CommonDataBLL.GetVolumeList();
        if (hash != null)
        {
            foreach (DictionaryEntry objDE in hash)
            {
                ListItem list = new ListItem();
                list.Text = objDE.Key.ToString();
                list.Value = objDE.Value.ToString();
                
            }
        }
        
    }
    protected void BindCompare()
    {
        
    }
    protected void ddlContion_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCompare();
    }

    protected void BindData()
    {
        DataSet ds = new DataSet();
        
    }
    protected void QueryWhere(string number, int volume, string condition, string compare, string content)
    {

        string strwhere = " and assister='" + Session["Member"].ToString() + "' and isagain=0 and  DefrayState>-2 ";

        
        BLL.Registration_declarations.PagerParmsInit model = bll.GetMemberOrderList(condition, compare, content, strwhere);

        if (type == 1)
        {
            if (Request.QueryString["dd"] != null)
            {
                model.SqlWhere = model.SqlWhere + " and Convert(varchar,RegisterDate,23) ='" + Request.QueryString["dd"].ToString() + "'";
            }
        }
        else
        {
            model.SqlWhere = model.SqlWhere + " and B.OrderExpectNum=" + volume + "";
        }

        if (model.ErrInfo != null)
            ScriptHelper.SetAlert(Page, model.ErrInfo);
        else
        {
            
        }

        
    }
    /// <summary>
    /// 查看订单详细信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void linkbtnquery_Click(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "Query")
        {
            if (e.CommandArgument != null)
            {
                string orderId = e.CommandArgument.ToString();
                Response.Redirect("ShowOrderDetailsBD.aspx?byy=88&orderId=" + orderId);
            }
        }
    }

    protected void linkbtnOK_Click(object sender, CommandEventArgs e)
    {

    }

    public string GetMemberOrderType(string type)
    {
        return Common.GetMemberOrderType(type);
    }

    public string GetRegisterDate(string rdate)
    {
        return Convert.ToDateTime(rdate).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString();
    }

    public string GetPayStatus(string paytype)
    {
        if (paytype == "1")
        {
            return GetTran("007524", "已激活");
        }
        else
        {
            return "<font color=#fff>" + new BLL.TranslationBase().GetTran("007525", "未激活") + "</font>";
        }

        
    }

    public string GetDefrayName(string defrayType)
    {
        string defrayName = new GroupRegisterBLL().GetDefryType(defrayType);
        return defrayName;
    }

    public string GetNumberName(string name)
    {
        //解密姓名
        string namestr = Encryption.Encryption.GetDecipherName(name);
        namestr = namestr.Substring(0, 1) + "**";

        return namestr;
    }

    protected string SetVisible(string dd, string orderID)
    {
        if (dd.Length > 0)
        {
            string _openWin = "<a href='ShowRemark.aspx?from=browsememberorders&orderID=" + orderID + "'>" + this.GetTran("000440") + "</a>";

            return _openWin;
        }
        else
        {
            return "无";
        }
    }

    /// <summary>
    /// GridView行处理事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gv_browOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");

            Label lblTotalMoney = e.Row.FindControl("lblTotalMoney") as Label;

            if (AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString())) == 0)
            {
                lblTotalMoney.Text = "0.00";
            }
            else
            {
                lblTotalMoney.Text = (Convert.ToDouble(lblTotalMoney.Text) * AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()))).ToString("f2");
            }
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
           
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        type = 0;
        BindData();
    }

    protected string Getpapernumber(string papernumber)
    {
        if (papernumber.Length > 6)
        {
            return papernumber.Substring(0, 4) + "******" + papernumber.Substring(papernumber.Length - 2, 2);
        }
        else
        {
            return papernumber;
        }
    }

}