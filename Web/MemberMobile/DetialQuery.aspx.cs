using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Model;
using BLL.other.Member;
using BLL.Registration_declarations;
using BLL.CommonClass;
using System.Data;

public partial class Member_DetialQuery : BLL.TranslationBase 
{
    int expectnum = 0;
    int qishu;
    int bzCurrency = 0;
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

            //CommonDataBLL.BindQishuListB(RegQishu,DropDownQiShu, false);
            //CommonDataBLL.BindQishuListB(RegQishu,DropDownQiShu1, false);

            //BtnQuery_Click(null,null);

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
                    //drp.DataBound += new EventHandler(DropDownList1_SelectedIndexChanged);
                    //this.ExpectNum1.ExpectNum = expectnum;
                    //memberperformance.ExpectNum = expectnum;
                    //memberperformance.Type = 1;
                    //LinkButton1.Visible = true;
                    //td1.Visible = true;
                }
            }
            else
            {
                //this.ExpectNum1.ExpectNum = BLL.CommonClass.CommonDataBLL.GetMaxqishu();
                //memberperformance.ExpectNum = BLL.CommonClass.CommonDataBLL.GetMaxqishu();
                //memberperformance.Type = 1;
                readBonus();
                //LinkButton1.Visible = false;
                //td1.Visible = false;
                //ExpectNum1.Enable = false;
            }

            /*--------------------------------------------------------------------------------------------------------------------------------------*/
        }





        //Translations();
    }
    /*-------------------------------------------------------------------------------------------------------------------------------------------------------*/
    public void readBonus()
    {
        int maxqs = BLL.CommonClass.CommonDataBLL.GetMaxqishu();
        double curreny = AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()));
        DataTable dt = MemberInfoModifyBll.getBonusTable(maxqs, Session["Member"].ToString());//第一个参数期数
        if (dt.Rows.Count > 0)
        {
            if (curreny == 0)
            {
                //this.Label1.Text = "0";
                this.Label1.Text = "0";
                this.Label2.Text = "0";
                this.Label3.Text = "0";
                this.Label4.Text = "0";
                this.Label5.Text = "0";
                this.Label6.Text = "0";
                this.Label7.Text = "0";
                this.Label8.Text = "0";
                this.Label9.Text = "0";
                this.Label10.Text = "0";
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
                this.Label10.Text = (Convert.ToDouble(dt.Rows[0]["CurrentSolidSend"]) * curreny).ToString("0.00");
            
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
    //protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    int maxExpectNum = DetialQueryBLL.IsSuanceJj(2);

    //    if (maxExpectNum == 0)
    //    {
    //        Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000156", "第") + " " + this.ExpectNum1.ExpectNum + " " + GetTran("000349", "奖金未结算") + "！');</script>");
    //        return;
    //    }
    //    readBonus();
    //}
    //protected void LinkButton1_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("BasicSearch.aspx");
    //}
}
