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
using BLL.Registration_declarations;
using Model.Other;
using BLL.other.Company;
using BLL.CommonClass;
using DAL;

public partial class Company_tw_ChangeTeam2 : BLL.TranslationBase
{
    RegistermemberBLL registermemberBLL = new RegistermemberBLL();
    private bool flag = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, "../index.aspx");
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.BalanceDefault);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));

        if (!this.IsPostBack)
        {
            if (Request.QueryString["bh"] != null)
            {
                string bh = Request.QueryString["bh"].ToString();
                this.txtbh.Text = bh;
                Button1_Click(null, null);
            }
            Translations();
        }
    }

    private void Translations()
    {
        this.TranControls(this.btn_fh, new string[][] { new string[] { "000096", "返 回" } });

        this.TranControls(this.Button1, new string[][] { new string[] { "000351", "提 交" } });

        this.TranControls(this.btn_re, new string[][] { new string[] { "000713", "开始调整" } });
    }

    private string GetNumber()
    {
        string manageID = Session["Company"].ToString();
        int count = 0;
        string number = BLL.CommonClass.CommonDataBLL.GetWLNumber(manageID, out count);
        if (count == 0)
        {
            return "('')";
        }
        return number;
    }

    protected void btn_re_Click(object sender, EventArgs e)
    {
        string number = txtbh.Text.Trim();
        string direct = DisposeString.DisString(this.txttuijian.Text, "'", "").Trim();
        string sqltele = "select number from MemberInfo where MobileTele='" + direct + "'";
        DataTable shjjjj = DBHelper.ExecuteDataTable(sqltele);

        string sql = "select number from MemberInfo where MobileTele='" + number + "'";
        DataTable shj = DBHelper.ExecuteDataTable(sql);

        number = shj.Rows[0][0].ToString();

        if (shjjjj.Rows.Count > 0)
        {
            direct = shjjjj.Rows[0][0].ToString();
            

            

            string olddirect = "";
            if (ViewState["oldTj"] != null)
            {
                olddirect = ViewState["oldTj"].ToString();
            }


            if (this.lblbh.Text.Trim() == "")
            {
                lblmessage.Text = GetTran("000723", "会员编号不能为空!");
                return;
            }
            if (direct == "")
            {
                lblmessage.Text = GetTran("000716", "推荐编号不能为空!");
                return;
            }
            if (ChangeTeamBLL.CheckNum(direct))
            {
                lblmessage.Text = GetTran("000717", "推荐编号不存在!");
                return;
            }

            string manageId = BLL.CommonClass.CommonDataBLL.getManageID(3);
            if (this.txtbh.Text.Trim() == manageId)
            {
                ScriptHelper.SetAlert(Page, manageId + GetTran("000714", "不可以调网") + "！");
                return;
            }

            lblmessage.Text = "";

            Application.Lock();



            int newqushu = 1; //AddOrderDataDAL.GetDistrict(placement, 1);
            DateTime nowTime = DateTime.UtcNow;
            int maxExpectNum = CommonDataBLL.GetMaxqishu();
            int j = 0;
            j = TempHistoryDAL.ExecuteUpdateNet(number, olddirect, direct, 0, maxExpectNum, newqushu, CommonDataBLL.OperateBh, nowTime);
            

            if (j >= 0)
            {
                
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('调网成功');window.location.href='../twQuery.aspx';</script>");

            }
            else
            {
                
                this.lblwz.Text = "";
                this.lblbh.Text = "";
                this.lbltuijian.Text = "";

                this.txttuijian.Text = "";

                this.txtbh.Text = "";
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('调网失败');</script>");

            }
            Application.UnLock();
           

            
            
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        lblmessage.Text = "";
        //string manageId = BLL.CommonClass.CommonDataBLL.getManageID(3);
        //if (this.txtbh.Text.Trim() == manageId)
        //{
        //    ScriptHelper.SetAlert(Page, manageId + GetTran("000714", "不可以调网") + "！");
        //    return;
        //}
        string number = DisposeString.DisString(this.txtbh.Text, "'", "").Trim();
        string sqltele = "select number from MemberInfo where MobileTele='" + number + "'";
        DataTable shjjjj = DBHelper.ExecuteDataTable(sqltele);
        if (shjjjj.Rows.Count > 0)
        {
            number = shjjjj.Rows[0][0].ToString();
        if (number == "")
        {
            lblmessage.Text = GetTran("000000", "会员编号不能为空!");
            this.lblwz.Text = "";
            this.lblbh.Text = "";
            this.lbltuijian.Text = "";
           // this.lblanzhi.Text = "";
            this.txttuijian.Text = "";
            //this.txtanzhi.Text = "";
            return;
        }
        if (ChangeTeamBLL.CheckNum(number))
        {
            lblmessage.Text = GetTran("000000", "会员编号不存在!");
            this.lblwz.Text = "";
            this.lblbh.Text = "";
            this.lbltuijian.Text = "";
            //this.lblanzhi.Text = "";
            this.txttuijian.Text = "";
            //this.txtanzhi.Text = "";
            return;
        }
        
        
        DataTable dt = ChangeTeamBLL.GetMemberInfoDataTable(number);
        
        this.lblwz.Text = GetTran("006767", "网络调整编号") + "&nbsp;";
        this.lblbh.Text = this.txtbh.Text + "&nbsp;&nbsp;&nbsp;&nbsp;" + GetTran("000107", "姓名") + "：" + StoreRegisterBLL.GetMemberName(number.ToString());
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0][0].ToString() != "" && dt.Rows[0][0].ToString() != null)
            {
                string tName = StoreRegisterBLL.GetMemberName(dt.Rows[0][0].ToString());
                string sql = "select MobileTele from MemberInfo where number='" + dt.Rows[0][0].ToString() + "'";
                DataTable shj = DBHelper.ExecuteDataTable(sqltele);
                this.lbltuijian.Text = shj.Rows[0][0].ToString() + "<br/>" + tName;

                this.txttuijian.Text = shj.Rows[0][0].ToString();

                this.Label1.Text = tName;
                ViewState["oldTj"] = shj.Rows[0][0].ToString();
            }
            else
            {

                string tName = "";

                this.lbltuijian.Text = "";

                this.txttuijian.Text = "";

                this.Label1.Text = tName;

                ViewState["oldTj"] = "";
            }

            
        }
        
        }
        
        flag = true;
    }
    protected void btn_fh_Click(object sender, EventArgs e)
    {
        Response.Redirect("../twQuery.aspx");
    }

}