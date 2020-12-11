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


public partial class MemberMobile_BrowseMemberOrders : BLL.TranslationBase
{
    BLL.CommonClass.CommonDataBLL commonDataBLL = new BLL.CommonClass.CommonDataBLL();
    BrowseMemberOrdersBLL bll = new BrowseMemberOrdersBLL();
    BLL.CommonClass.CommonDataBLL cbll = new BLL.CommonClass.CommonDataBLL();
    ViewFuXiaoBLL viewFuXiaoBLL = new ViewFuXiaoBLL();
    int type = 0;

    public string msg = "";
  public  int bzCurrency = 0;


    

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
        //this.TranControls(this.rdbtnType, new string[][] { new string[] { "000633", "全部" }, new string[] { "007524", "已激活" }, new string[] { "007525", "未激活" } });
        //this.TranControls(this.ddlContion, new string[][] { new string[] { "000024", "会员编号" }, 
        //    new string[] { "000063", "会员昵称" }, new string[] { "000026", "推荐编号" },
        //    new string[] { "000027", "安置编号" }, new string[] { "000079", "订单号" }});
        //this.TranControls(this.ddlType, new string[][] { new string[] { "002055", "报单期数" }, new string[] { "002058", "支付期数" } });
        //this.TranControls(this.btnSearch, new string[][] { new string[] { "000011", "搜索" } });
    }
    protected void Initddl()
    {
        //BindVolume();
        //BLL.CommonClass.CommonDataBLL.BindQishuList(ddlVolume, false);
        //BindCompare();
        //ViewState["MaxExceptNum"] = ddlVolume.SelectedValue;
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
               // ddlVolume.Items.Add(list);
            }
        }
        //ddlVolume.Items.Add(new ListItem("全部", "-1"));
    }
    protected void BindCompare()
    {
        //ddlcompare.Items.Clear();
        //this.txtContent.Text = "";

        //ddlcompare.Items.Add(new ListItem(this.GetTran("000881", "不限"), "all"));

        //if (ddlContion.SelectedValue != "B.TotalMoney" && ddlContion.SelectedValue != "B.TotalPv")
        //{
        //    ddlcompare.Items.Add(new ListItem(this.GetTran("000821", "包含字符"), " like "));
        //    ddlcompare.Items.Add(new ListItem(this.GetTran("000822", "不包含字符"), " not like "));
        //}
        //else
        //{
        //    ddlcompare.Items.Add(new ListItem(this.GetTran("000802", "数值等于"), " = "));
        //    ddlcompare.Items.Add(new ListItem(this.GetTran("000804", "数值大于"), " > "));
        //    ddlcompare.Items.Add(new ListItem(this.GetTran("000808", "数值小于"), " < "));
        //    ddlcompare.Items.Add(new ListItem(this.GetTran("000810", "数值大于等于"), " >= "));
        //    ddlcompare.Items.Add(new ListItem(this.GetTran("000813", "数值小于等于"), " <= "));
        //    ddlcompare.Items.Add(new ListItem(this.GetTran("000816", "数值不等于"), " <> "));
        //}
    }
    protected void ddlContion_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCompare();
    }

    protected void BindData()
    {
        DataSet ds = new DataSet();
        //string number = Session["Member"].ToString();
        //int volume = Convert.ToInt32(ddlVolume.SelectedValue);
        //string condition = ddlContion.SelectedItem.Value;
        //string compare = ddlcompare.SelectedValue;
        //string content = txtContent.Text.Trim();
        //if (ddlContion.SelectedItem.Value == "B.TotalMoney" || ddlContion.SelectedItem.Value == "B.TotalPv")
        //{
        //    try
        //    {
        //        double m = Convert.ToDouble(txtContent.Text.Trim());
        //        content = m.ToString();
        //    }
        //    catch
        //    {
        //        msg = "<script>alert('" + GetTran("007528", "查询条件格式输入错误，请重新输入") + "！');</script>";
        //        return;
        //    }
        //}

       // QueryWhere(number, volume, condition, compare, content);
    }
    protected void QueryWhere(string number, int volume, string condition, string compare, string content)
    {

        string strwhere = " and assister='" + Session["Member"].ToString() + "' and isagain=0 and  DefrayState>-2 ";

        //if (rdbtnType.SelectedValue != "-1")
        //{
        //    strwhere += " and  DefrayState=" + rdbtnType.SelectedValue + " ";
        //}

        //this.Pager1.Visible = true;
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
            //this.Pager1.ControlName = "rep";
            //this.Pager1.key = model.Key;
            //this.Pager1.PageColumn = model.PageColumn;
               
            //this.Pager1.Pageindex = 0;
            //this.Pager1.PageTable = model.PageTable;
            //this.Pager1.Condition = model.SqlWhere;
            //this.Pager1.PageSize = model.PageSize;
            //this.Pager1.PageCount = 0;
            //this.Pager1.PageBind();
        }
      
        //Translate();
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

        //return Common.GetDeftrayState(paytype);
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
            //this.Translate();
        }
    }

    /// <summary>
    /// 翻译方法
    /// </summary>
    //public void Translate()
    //{
    //    this.TranControls(this.ddlContion, new string[][] { new string[] { "000024", "会员编号" }, new string[] { "000063", "会员昵称" }, new string[] { "000026", "推荐编号" }, new string[] { "000027", "安置编号" }, new string[] { "000079", "订单号" } });
    //    this.TranControls(this.rdbtnType, new string[][] { new string[] { "000633", "全部" }, new string[] { "007524", "已激活" }, new string[] { "007525", "未激活" } });
    //    this.TranControls(this.btnQuery, new string[][] { new string[] { "000011", "搜索" } });
    //    this.TranControls(this.ddlType, new string[][]{
    //       new string[] { "002055", "报单期数" }, 
    //            new string[] { "002058", "支付期数" }
    //    });
    //    this.TranControls(this.gv_browOrder, new string[][] 
    //    { 
    //        new string[] { "000811", "查看明细" }, 
    //        new string[] { "000024", "会员编号" },
    //        new string[] { "000025", "会员姓名" },
    //        new string[] { "000063", "会员昵称" },  
    //        new string[] { "000026", "推荐编号" }, 
    //        new string[] { "000027", "安置编号" },
    //        new string[] { "000047", "国家" },
    //        new string[] { "000109", "省份" },
    //        new string[] { "000110", "城市 " },
    //        new string[] { "007526", "区县" },
    //        new string[] { "000081", "证件类型" },
    //        new string[] { "000083", "证件号码" },
    //        new string[] { "005623", "手机号码" },
    //        new string[] { "007527", "激活状态" },
    //        new string[] { "000456", "支付类型" } ,
    //        new string[] { "001345", "发货方式" } ,
    //        new string[] { "007416", "收货途径" },
    //        new string[] { "000045", "期数" },
    //        new string[] { "000780", "审核期数" },
    //        new string[] { "000322", "金额" },
    //        new string[] { "000414", "积分" },
    //        new string[] { "000079", "订单号" },
    //        new string[] { "000057", "注册日期" },
    //    });
    //    //this.TranControls(this.btnQuery, new string[][] { new string[] { "000695", "搜索" } });
    //}
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