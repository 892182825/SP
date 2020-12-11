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
using DAL;
using Model;
using BLL.CommonClass;
using BLL.Registration_declarations;
using BLL.other.Member;

public partial class MemberMobile_Lsjjxxaspx : BLL.TranslationBase
{

    int expectnum = 0;
    public int bzCurrency = 0;
    public string jiesuan;
    public bool etat;
    int isjiesuan;
    BasicSearchBLL basicSearchBLL = new BasicSearchBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckMemberPermission();
        //获取标准币种
        bzCurrency = CommonDataBLL.GetStandard();
        if (!IsPostBack)
        {
            if (Session["Default_Currency"] == null) Session["Default_Currency"] = bzCurrency;


            int RegQishu = CommonDataBLL.GetRegisterQishu(Session["Member"].ToString());


            //BtnQuery_Click(null, null);

            /*--------------------------------------------------------------------------------------------------------------------------------------*/

            if (Request.QueryString["expectnum"] != null)
            {
                expectnum = Convert.ToInt32(Request.QueryString["expectnum"]);
            }
            int maxExpectNum = DetialQueryBLL.IsSuanceJj(expectnum);
            if (Request.QueryString["type"] != null)
            {
                //历史奖金
                if (Request.QueryString["type"] == "0")
                {
                    if (maxExpectNum == 0)
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000344", "当前没有已结算的工资") + "！');</script>");
                        //return;
                    }

                }
            }
            else
            {

                readBonus(maxExpectNum);
     
            }

            /*--------------------------------------------------------------------------------------------------------------------------------------*/
        }





        Translations();
    }
    /*-------------------------------------------------------------------------------------------------------------------------------------------------------*/
    public void readBonus(int expectnum)
    {
        int maxqs = DetialQueryBLL.IsSuanceJj(expectnum);// BLL.CommonClass.CommonDataBLL.GetMaxqishu();
        double curreny = AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()));
        DataTable dt = MemberInfoModifyBll.getBonusTable(maxqs, Session["Member"].ToString());//第一个参数期数

        DataTable dt_one = DAL.DBHelper.ExecuteDataTable("select IsSuance from config where ExpectNum=" + maxqs);
        string IsSuance = dt_one.Rows[0]["IsSuance"].ToString();//是否显示

        if (dt.Rows.Count > 0)
        {
            if (curreny == 0)
            {
                //this.Label1.Text = "0";
                this.Label1.Text = "0.00";
                this.Label2.Text = "0.00";
                this.Label3.Text = "0.00";
                this.Label4.Text = "0.00";
                this.Label5.Text = "0.00";
                this.Label6.Text = "0.00";
                this.Label7.Text = "0.00";
                this.Label8.Text = "0.00";
                this.Label9.Text = "0.00";
                this.Label10.Text = "0.00";
            }
            else
            {
                if (IsSuance == "0")
                {

                    this.Label1.Text = "0.00";
                    this.Label2.Text = "0.00";
                    this.Label3.Text = "0.00";
                    this.Label4.Text = "0.00";
                    this.Label5.Text = "0.00";
                    this.Label6.Text = "0.00";
                    this.Label7.Text = "0.00";
                    this.Label8.Text = "0.00";
                    this.Label9.Text = "0.00";
                    this.Label10.Text = "0.00";

                }
                else
                {

                    //this.Label1.Text = (Convert.ToDouble(dt.Rows[0]["Bonus0"]) / AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()))).ToString("0.00");
                    this.Label1.Text = (Convert.ToDouble(dt.Rows[0]["Bonus1"]) *
                    AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()))).ToString("0.00");
                    this.Label2.Text = (Convert.ToDouble(dt.Rows[0]["Bonus2"]) * curreny).ToString("0.00");
                    this.Label3.Text = (Convert.ToDouble(dt.Rows[0]["Bonus3"]) * curreny).ToString("0.00");
                    this.Label4.Text = (Convert.ToDouble(dt.Rows[0]["Bonus4"]) * curreny).ToString("0.00");
                    this.Label5.Text = (Convert.ToDouble(dt.Rows[0]["Bonus5"]) * curreny).ToString("0.00");
                    this.Label6.Text = (Convert.ToDouble(dt.Rows[0]["Kougl"]) * curreny).ToString("0.00");
                    this.Label7.Text = (Convert.ToDouble(dt.Rows[0]["Koufl"]) * curreny).ToString("0.00");
                    this.Label8.Text = (Convert.ToDouble(dt.Rows[0]["Koufx"]) * curreny).ToString("0.00");
                    this.Label9.Text = (Convert.ToDouble(dt.Rows[0]["CurrentSolidSend"]) * curreny).ToString("0.00");
                    this.Label10.Text = (Convert.ToDouble(dt.Rows[0]["CurrentTotalMoney"]) * curreny).ToString("0.00");

                }


            }
        }
        else
        {
            //this.Label1.Text = "0.00";
            this.Label1.Text = "0.00";
            this.Label2.Text = "0.00";
            this.Label3.Text = "0.00";
            this.Label4.Text = "0.00";
            this.Label5.Text = "0.00";
            this.Label6.Text = "0.00";
            this.Label7.Text = "0.00";
            this.Label8.Text = "0.00";
            this.Label9.Text = "0.00";
            this.Label10.Text = "0.00";
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("BasicSearch.aspx");
    }
    /*---------------------------------------------------------------------------------------------------------------------------------------------------*/
    private void Translations()
    {



    }



    public String regler(int qishu)
    {
        isjiesuan = (int)DBHelper.ExecuteScalar("Select isSuance from config where ExpectNum=" + qishu);
        if (isjiesuan == 1)
        {
            return GetTran("000260", "已结算");
        }
        else
        {

            return GetTran("000215", "未结算");
        }
    }
    #region 注释代码
    //protected void Download_Click(object sender, System.EventArgs e)
    //{
    //    GridView1.AllowPaging = false;
    //    this.GridView1.AllowSorting = false;
    //    this.GridView1.DataSource = (DataTable)ViewState["dt"];
    //    this.GridView1.DataBind();

    //    Response.Clear();
    //    Response.Buffer = true;
    //    Response.Charset = "GB2312";
    //    Response.AppendHeader("Content-Disposition", "attachment;filename=HelloAdmin.xls");
    //    Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");//设置输出流为简体中文
    //    Response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。 
    //    this.EnableViewState = false;
    //    System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("ZH-CN", true);
    //    System.IO.StringWriter oStringWriter = new System.IO.StringWriter(myCItrad);
    //    System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
    //    this.GridView1.RenderControl(oHtmlTextWriter);
    //    Response.Write(oStringWriter.ToString());
    //    Response.End();
    //}


    //protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
    //        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");

    //        Label labjiesuan = (Label)e.Row.FindControl("labjiesuan");
    //        HtmlInputHidden Hidqishu = (HtmlInputHidden)e.Row.FindControl("Hidqishu");
    //        Image linkbtn = (Image)e.Row.FindControl("LinkButton1");
    //        //System.Data.DataTable dt = (System.Data.DataTable)ViewState["dt"];
    //        for (int i = 0; i < this.GridView1.Columns.Count; i++)
    //         {
    //             string name = GridView1.Columns[i].ToString();

    //             //if (name == GetTran("7577", "无"))
    //             //{
    //             //    if (AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString())) == 0)
    //             //    {
    //             //        e.Row.Cells[i].Text = "0";
    //             //    }
    //             //    else
    //             //    {
    //             //        e.Row.Cells[i].Text = (Convert.ToDouble(e.Row.Cells[i].Text) / AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()))).ToString("f2");
    //             //    }
    //             //}

    //              if (name == GetTran("007578", "推荐奖"))
    //             {
    //                 if (AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString())) == 0)
    //                 {
    //                     e.Row.Cells[i].Text = "0";
    //                 }
    //                 else
    //                 {
    //                     e.Row.Cells[i].Text = (Convert.ToDouble(e.Row.Cells[i].Text) / AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()))).ToString("f2");
    //                 }
    //             }
    //             else if (name == GetTran("007579", "回本奖"))
    //             {
    //                 if (AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString())) == 0)
    //                 {
    //                     e.Row.Cells[i].Text = "0";
    //                 }
    //                 else
    //                 {
    //                     e.Row.Cells[i].Text = (Convert.ToDouble(e.Row.Cells[i].Text) / AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()))).ToString("f2");
    //                 }
    //             }
    //              else if (name == GetTran("007580", "大区奖"))
    //              {
    //                  if (AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString())) == 0)
    //                  {
    //                      e.Row.Cells[i].Text = "0";
    //                  }
    //                  else
    //                  {
    //                      e.Row.Cells[i].Text = (Convert.ToDouble(e.Row.Cells[i].Text) / AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()))).ToString("f2");
    //                  }
    //              }
    //             else if (name == GetTran("007581", "小区奖"))
    //             {
    //                 if (AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString())) == 0)
    //                 {
    //                     e.Row.Cells[i].Text = "0";
    //                 }
    //                 else
    //                 {
    //                     e.Row.Cells[i].Text = (Convert.ToDouble(e.Row.Cells[i].Text) / AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()))).ToString("f2");
    //                 }
    //             }
    //             else if (name == GetTran("007582", "永续奖"))
    //             {
    //                 if (AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString())) == 0)
    //                 {
    //                     e.Row.Cells[i].Text = "0";
    //                 }
    //                 else
    //                 {
    //                     e.Row.Cells[i].Text = (Convert.ToDouble(e.Row.Cells[i].Text) / AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()))).ToString("f2");
    //                 }
    //             }

    //             else if (name == GetTran("001352", "网平台综合管理费"))
    //             {
    //                 if (AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString())) == 0)
    //                 {
    //                     e.Row.Cells[i].Text = "0";
    //                 }
    //                 else
    //                 {
    //                     e.Row.Cells[i].Text = (Convert.ToDouble(e.Row.Cells[i].Text) / AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()))).ToString("f2");
    //                 }
    //             }
    //             else if (name == GetTran("001353", "网扣福利奖金"))
    //             {
    //                 if (AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString())) == 0)
    //                 {
    //                     e.Row.Cells[i].Text = "0";
    //                 }
    //                 else
    //                 {
    //                     e.Row.Cells[i].Text = (Convert.ToDouble(e.Row.Cells[i].Text) / AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()))).ToString("f2");
    //                 }
    //             }
    //             else if (name == GetTran("001355", "网扣重复消费"))
    //             {
    //                 if (AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString())) == 0)
    //                 {
    //                     e.Row.Cells[i].Text = "0";
    //                 }
    //                 else
    //                 {
    //                     e.Row.Cells[i].Text = (Convert.ToDouble(e.Row.Cells[i].Text) / AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()))).ToString("f2");
    //                 }
    //             }

    //             else if (name == GetTran("000247", "总计"))
    //             {
    //                 if (AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString())) == 0)
    //                 {
    //                     e.Row.Cells[i].Text = "0";
    //                 }
    //                 else
    //                 {
    //                     e.Row.Cells[i].Text = (Convert.ToDouble(e.Row.Cells[i].Text) / AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()))).ToString("f2");
    //                 }
    //             }
    //             else if (name == GetTran("000249", "扣税"))
    //             {
    //                 if (AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString())) == 0)
    //                 {
    //                     e.Row.Cells[i].Text = "0";
    //                 }
    //                 else
    //                 {
    //                     e.Row.Cells[i].Text = (Convert.ToDouble(e.Row.Cells[i].Text) / AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()))).ToString("f2");
    //                 }
    //             }
    //             else if (name == GetTran("000251", "扣款"))
    //             {
    //                 if (AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString())) == 0)
    //                 {
    //                     e.Row.Cells[i].Text = "0";
    //                 }
    //                 else
    //                 {
    //                     e.Row.Cells[i].Text = (Convert.ToDouble(e.Row.Cells[i].Text) / AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()))).ToString("f2");
    //                 }
    //             }
    //             else if (name == GetTran("000254", "实发"))
    //             {
    //                 if (AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString())) == 0)
    //                 {
    //                     e.Row.Cells[i].Text = "0";
    //                 }
    //                 else
    //                 {
    //                     e.Row.Cells[i].Text = (Convert.ToDouble(e.Row.Cells[i].Text) / AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()))).ToString("f2");
    //                 }
    //             }
    //             else if (name == GetTran("000252", "补款"))
    //             {
    //                 if (AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString())) == 0)
    //                 {
    //                     e.Row.Cells[i].Text = "0";
    //                 }
    //                 else
    //                 {
    //                     e.Row.Cells[i].Text = (Convert.ToDouble(e.Row.Cells[i].Text) / AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()))).ToString("f2");
    //                 }
    //             }

    //         }

    //HtmlInputHidden Hidqishu = (HtmlInputHidden)e.Row.FindControl("Hidqishu");
    // int isjiesuan = (int)DBHelper.ExecuteScalar("Select isSuance from config where ExpectNum=" + Hidqishu.Value);
    // if (isjiesuan == 1)
    // {
    //     labjiesuan.Text = GetTran("000260", "已结算");
    //     labjiesuan.Enabled = false;
    // }
    // else
    // {
    //     for (int i = 2; i < this.GridView1.Columns.Count; i++)
    //     {
    //         if (i == 24)
    //             continue;
    //         else
    //         {
    //             e.Row.Cells[i].Text = "0";
    //         }
    //     }
    //     e.Row.Cells[2].Text = GetTran("000215", "未结算");
    //     e.Row.Cells[2].ForeColor = System.Drawing.Color.Red;
    //     linkbtn.ImageUrl = "images/view-button-.png";
    //     linkbtn.Enabled = false;
    // }
    //    }
    //}
    //public override void VerifyRenderingInServerForm(Control control)
    //{

    //}
    #endregion
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string expectnum = e.CommandArgument.ToString();
        Response.Redirect("DetialQuery.aspx?type=0&expectnum=" + expectnum);
    }
    protected void rep_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        

    }
}
