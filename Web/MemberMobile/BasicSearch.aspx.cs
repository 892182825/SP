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
using System.Data.SqlClient;
using System.Text;
public partial class Member_BasicSearch : BLL.TranslationBase
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
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckMemberPermission();
        //获取标准币种
        bzCurrency = CommonDataBLL.GetStandard();
        if (!IsPostBack)
        {
            //if (Session["Default_Currency"] == null) Session["Default_Currency"] = bzCurrency;


            //int RegQishu = CommonDataBLL.GetRegisterQishu(Session["Member"].ToString());

            //CommonDataBLL.BindQishuListB(RegQishu, DropDownQiShu, true);
            //CommonDataBLL.BindQishuListB(RegQishu, DropDownQiShu1, true);

            //BtnQuery_Click(null, null);

            /*--------------------------------------------------------------------------------------------------------------------------------------*/
            //         int maxExpectNum = DetialQueryBLL.IsSuanceJj(expectnum);
            //         if (Request.QueryString["expectnum"] != null)
            //         {
            //             expectnum = Convert.ToInt32(Request.QueryString["expectnum"]);
            //         }
            //         if (Request.QueryString["isn"] != null) {
            //             int isn =Convert.ToInt32( Request.QueryString["isn"]);

            //             if (isn == 0) { 

            //                 tbsht.Style.Add("display", "none"); 

            //             }
            //             if (isn == 1) {

            //                 tbsht.Style.Add("display", "none");

            //                 expectnum = 0;
            //             }
            //         }
            //         if (Request.QueryString["type"] != null)
            //         {
            //             //历史奖金
            //             if (Request.QueryString["type"] == "0")
            //             {
            //                 if (maxExpectNum == 0)
            //                 {
            //                     Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000344", "当前没有已结算的工资") + "！');</script>");
            //                     //return;
            //                 }
            //                 //drp.DataBound += new EventHandler(DropDownList1_SelectedIndexChanged);
            //                 //this.ExpectNum1.ExpectNum = expectnum;
            //                 //memberperformance.ExpectNum = expectnum;
            //                 //memberperformance.Type = 1;
            //                 //LinkButton1.Visible = true;
            //                 //td1.Visible = true;
            //             }
            //         }
            //         else
            //         {
            //             //this.ExpectNum1.ExpectNum = BLL.CommonClass.CommonDataBLL.GetMaxqishu();
            //             //memberperformance.ExpectNum = BLL.CommonClass.CommonDataBLL.GetMaxqishu();
            //             //memberperformance.Type = 1;
            //             //LinkButton1.Visible = false;
            //             //td1.Visible = false;
            //             //ExpectNum1.Enable = false;
            //         }

            ///*--------------------------------------------------------------------------------------------------------------------------------------*/
            //     }



        }

        Translations();
    }
    /*-------------------------------------------------------------------------------------------------------------------------------------------------------*/
    public void readBonus(int expectnum)
    {
        int maxqs = BLL.CommonClass.CommonDataBLL.GetMaxqishu();
        // int maxExpectNum = DetialQueryBLL.IsSuanceJj(expectnum);  //最大公布期数
        double curreny = AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()));
        if (expectnum == 0) expectnum = 1;
        DataTable dt = MemberInfoModifyBll.getBonusTable(expectnum, Session["Member"].ToString());//第一个参数期数

        DataTable dt_one = DAL.DBHelper.ExecuteDataTable("select IsSuance from config where ExpectNum=" + expectnum);
        string IsSuance = dt_one.Rows[0]["IsSuance"].ToString();//是否显示

        if (dt.Rows.Count > 0)
        {
            if (curreny == 0)
            {

            }
            else
            {
                if (IsSuance == "0")
                {

                    //this.Label1.Text = "0.00";
                    //this.Label2.Text = "0.00";
                    //this.Label3.Text = "0.00";
                    //this.Label4.Text = "0.00";
                    //this.Label5.Text = "0.00";
                    //this.Label6.Text = "0.00";
                    //this.Label7.Text = "0.00";
                    //this.Label8.Text = "0.00";
                    //this.Label9.Text = "0.00";
                    //this.Label10.Text = "0.00";
                    //lbllbonus6.Text = "0.00";

                }
                else
                {

                    //this.Label1.Text = (Convert.ToDouble(dt.Rows[0]["Bonus0"]) / AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()))).ToString("0.00");
                    //this.Label1.Text = (Convert.ToDouble(dt.Rows[0]["Bonus1"]) *
                    //AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()))).ToString("0.00");
                    //this.Label2.Text = (Convert.ToDouble(dt.Rows[0]["Bonus2"]) * curreny).ToString("0.00");
                    //this.Label3.Text = (Convert.ToDouble(dt.Rows[0]["Bonus3"]) * curreny).ToString("0.00");
                    //this.Label4.Text = (Convert.ToDouble(dt.Rows[0]["Bonus4"]) * curreny).ToString("0.00");
                    //this.Label5.Text = (Convert.ToDouble(dt.Rows[0]["Bonus5"]) * curreny).ToString("0.00");
                    //lbllbonus6.Text = (Convert.ToDouble(dt.Rows[0]["Bonus6"]) * curreny).ToString("0.00");
                    //this.Label6.Text = (Convert.ToDouble(dt.Rows[0]["Kougl"]) * curreny).ToString("0.00");
                    //this.Label7.Text = (Convert.ToDouble(dt.Rows[0]["Koufl"]) * curreny).ToString("0.00");
                    //this.Label8.Text = (Convert.ToDouble(dt.Rows[0]["Koufx"]) * curreny).ToString("0.00");
                    //this.Label9.Text = (Convert.ToDouble(dt.Rows[0]["CurrentSolidSend"]) * curreny).ToString("0.00");
                    //this.Label10.Text = (Convert.ToDouble(dt.Rows[0]["CurrentTotalMoney"]) * curreny).ToString("0.00");

                }


            }
        }
        else
        {
            //this.Label1.Text = "0.00";
            //this.Label1.Text = "0.00";
            //this.Label2.Text = "0.00";
            //this.Label3.Text = "0.00";
            //this.Label4.Text = "0.00";
            //this.Label5.Text = "0.00";
            //this.Label6.Text = "0.00";
            //this.Label7.Text = "0.00";
            //this.Label8.Text = "0.00";
            //this.Label9.Text = "0.00";
            //this.Label10.Text = "0.00";
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("BasicSearch.aspx");
    }
    /*---------------------------------------------------------------------------------------------------------------------------------------------------*/
    private void Translations()
    {
        //this.TranControls(this.BtnQuery, new string[][] { new string[] { "000048", "查 询" } });
    }

    protected void BtnQuery_Click(object sender, System.EventArgs e)
    {
        //string bianhao = Session["Member"].ToString();
        //ViewState["bianhao"] = Session["Member"].ToString();
        //DataTable table = new DataTable();
        //int qishu = Convert.ToInt32(DropDownQiShu.SelectedValue);
        //int qishu1 = Convert.ToInt32(DropDownQiShu1.SelectedValue);
        //if (qishu <= qishu1)
        //{
        //    for (int i = qishu1; i >= qishu; i--)
        //    {
        //        DataTable dt = basicSearchBLL.GetMemberBalance(i.ToString(), bianhao);

        //        if (i == qishu1)
        //            table = dt;
        //        else
        //        {
        //            foreach (DataRow row in dt.Rows)
        //            {
        //                table.Rows.Add(row.ItemArray);
        //            }
        //        }
        //    }
        //    ViewState["dt"] = table;
        //    this.rep.DataSource = table.DefaultView;
        //    this.rep.DataBind();

        //    //Translations();
        //}
        //else
        //{
        //    ScriptHelper.SetAlert(this.Page, GetTran("000255", "起始期数不能大于终止期数"));
        //}
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
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string expectnum = e.CommandArgument.ToString();
        Response.Redirect("DetialQuery.aspx?type=0&expectnum=" + expectnum);
    }
    protected void rep_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

    }
}
