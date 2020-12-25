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
using System.Text;
using Model.Other;

public partial class AccountDetail_AccountDetail : BLL.TranslationBase
{
    protected int bzCurrency = 0;
    public double i;
    public string ttt = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //获取标准币种
        bzCurrency = CommonDataBLL.GetStandard();
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        ttt = Request.QueryString["type"];
       //// Session["member"]= "b16b6adb609a399d644fef7123bf35fe";
        //i = AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()));
        ViewState["Kmtype"] =( Request.QueryString["type"] == "" ? "" : Request.QueryString["type"]);
        String str = ViewState["Kmtype"].ToString();
    //    if (ViewState["Kmtype"].ToString() == "AccountXJ" || ViewState["Kmtype"].ToString() == "AccountXF" || ViewState["Kmtype"].ToString() == "AccountFX" || ViewState["Kmtype"].ToString() == "AccountFXth")
    //    {
    //        //    Top.Visible = true;
    //        //    bottom.Visible = true;
    //        //    SLeft1.Visible = false;
    //        //    STop1.Visible = false;
    //        Permissions.ThreeRedirect(Page, "../member/" + Permissions.redirUrl);
    //}
        //else { Top.Visible = false; bottom.Visible = false; Permissions.ThreeRedirect(Page, "../store/" + Permissions.redirUrl); }
        if (!IsPostBack)
        {
          
            //GetKmtype();
            //DataBind();
        }
        translation();
    }

    private void translation() {
       
    }

    /// <summary>
    /// 绑定数据
    /// </summary>
   
    /// <summary>
    /// 获取总入，总出
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    private string GetHappenMoney(string sql) {
        string money = "0.00";

      
        return money;
    }

    /// <summary>
    /// 绑定科目列表
    /// </summary>
    private void GetKmtype() {
       
    }
    //前天
    protected void btn_zuotian_Click(object sender, EventArgs e)
    {
        
    }
    //昨天
    protected void btn_jintian_Click(object sender, EventArgs e)
    {
       
    }
    //今天
    protected void btn_mingtian_Click(object sender, EventArgs e)
    {
        
    }
    protected void btn_serach_Click(object sender, EventArgs e)
    {
        DataBind();
    }

    protected string getMark(string remark)
    {
        string res = "";
        if (remark.IndexOf('~') > 0)
        {
            for (int i = 0; i < remark.Split('~').Length; i++) {
                res +=  GetTran(remark.Split('~')[i], "") == "" ? remark.Split('~')[i] : GetTran(remark.Split('~')[i], "");
            }
        }
        else
        {
            res = remark;
        }
        return res;
    }
}
