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
using BLL.other.Company;
using BLL.CommonClass;
using Model.Other;
using DAL;
using Model;

public partial class Company_MemberOff : BLL.TranslationBase 
{
    protected string msg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.MemberOffView);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        if (!this.IsPostBack)
        {

            if (Session["Company"] != null)
            {
                string ManageID = Session["Company"].ToString();
                txtOperatorNo.Text = ManageID;
                txtOperatorName.Text = DataBackupBLL.GetNameByAdminID(ManageID);
            }
            if (Request.QueryString["Number"] != null)
            {
                this.txtNumber.Text = Request.QueryString["Number"].ToString();
                this.Label4.Text = new AjaxClass().GetName(Request.QueryString["Number"].ToString());
            }
            if (Request.QueryString["type"] != null && Request.QueryString["type"]=="2")
            {
                Label1.Text = GetTran("003024", "恢复注销会员");
                Label2.Text = GetTran("007237", "恢复注销会员编号")+"：";
                Label3.Text = GetTran("007238", "恢复注销原因") + "：";
            }
            else
            {
                Label1.Text = GetTran("001282", "会员注销");
                Label2.Text = GetTran("001295", "恢复注销会员编号") + "：";
                Label3.Text = GetTran("007164", "注销原因") + "：";
            }
        }
        Translations();
    }

    private void Translations()
    {
         
        this.TranControls(btn_Select, new string[][] { new string[] { "000440", "查看" } });
        this.TranControls(this.Button1, new string[][] { new string[] { "000421", "返回" } });


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

    protected void btnquery_Click(object sender, EventArgs e)
    {
        string number = txtNumber.Text.Trim();
        //判断会员是否存在
        int con = MemberOffBLL.getMember(number);
        if (con == 0)
        {
            ScriptHelper.SetAlert(Page, GetTran("000599", "会员") + "" + number + "" + GetTran("000801", "不存在，请重新输入") + "！");
            return;
        }

        if (MemberOffBLL.getMemberZX(number) > 0)
        {
            int con1 = MemberOffBLL.getMemberISzx(number);
            if (Request.QueryString["type"] != null && Request.QueryString["type"]=="1")
            {
                //判断会员是否已注销
                if (con1 == 1)
                {
                    ScriptHelper.SetAlert(Page, GetTran("000599", "会员") + "" + number + "" + GetTran("001310", "已经注销，不需要再次注销了") + "！");
                    return;
                }
            }
            else
            {
                if (con1 == 2)
                {
                    ScriptHelper.SetAlert(Page, GetTran("000599", "会员") + "" + number + "" + GetTran("001310", "已经恢复注销，不需要再次恢复注销了") + "！");
                    return;
                }
            }
        }

        string zxname = GetTran("001286", "已注销");
        zxname = Encryption.Encryption.GetEncryptionName(zxname);
        DateTime nowTime = DateTime.Now.AddHours(Convert.ToDouble(Session["WTH"]));
        string offReason = txtMemberOffreason.Text;

        MemberOffModel mom = new MemberOffModel();
        mom.Number = number;
        mom.Zxqishu = CommonDataBLL.getMaxqishu();
        mom.Zxfate = DateTime.UtcNow;
        mom.OffReason = txtMemberOffreason.Text;
        mom.OperatorNo = txtOperatorNo.Text;
        mom.OperatorName = txtOperatorName.Text;
        if (Request.QueryString["type"] != null && Request.QueryString["type"] == "1")
        {
            int insertCon = MemberOffBLL.getInsertMemberZX(mom, zxname);
            if (insertCon > 0)
            {
                msg = "<script language='javascript'>alert('" + GetTran("001312", "注销会员成功") + "！');window.location.href='MemberOffView.aspx';</script>";
            }
        }
        else
        {
            string id = DAL.DBHelper.ExecuteScalar("select top 1 id from memberOff where number='" + number + "'  order by zxdate desc").ToString();
            int insertCon = MemberOffBLL.getUpdateMemberZX(mom.Number, CommonDataBLL.getMaxqishu(), Convert.ToInt32(id), DateTime.UtcNow, mom.OperatorNo, mom.OperatorName);


            if (insertCon > 0)
            {
                ScriptHelper.SetAlert(Page, GetTran("001338", "确定完成"));
            }
            else
            {
                ScriptHelper.SetAlert(Page, GetTran("007132", "恢复注销失败"));
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("MemberOffList.aspx");
    }
    protected void lkSubmit_Click(object sender, EventArgs e)
    {
        btnquery_Click(null, null);
    }
    protected void btn_Select_Click(object sender, EventArgs e)
    {
        string Number= txtNumber.Text;
        if (Number.Length <= 0)
        {
            ScriptHelper.SetAlert(Page, GetTran("000129", "对不起，会员编号不能为空！"));
            return;
        }
        if (ChangeTeamBLL.CheckNum(Number))
        {
            ScriptHelper.SetAlert(Page, GetTran("000288", "对不起,该会员编号不存在"));
            return;
        }
        Response.Redirect("DisplayMemberDeatail.aspx?ID=" + Number + "&type=MemberOff&lx=" + Request.QueryString["type"]);
        Page.RegisterStartupScript(null, "<script language='javascript'>window.close();</script>");
    }
}
