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
using BLL.other;
using System.Data.SqlClient;

using BLL.other.Company;
using BLL.CommonClass;
public partial class Company_Default : BLL.TranslationBase 
{
    protected string msg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["mbreginfo"]!=null)
        {
            Session.Remove("mbreginfo");
        }
        if (Session["fxMemberModel"] != null)
        {
            Session.Remove("fxMemberModel");
        }
        if (!IsPostBack)
        {
            if (Session["Company"] != null)
            {
                Response.Redirect("../Cmain.htm");
            }


            if (Session["languageCode"] == null)
            {
                SqlDataReader sdr =DAL .DBHelper.ExecuteReader("Select Top 1 * From LANGUAGE ORDER BY ID");
                while (sdr.Read())
                {
                    Session["languageCode"] = sdr["languageCode"].ToString().Trim(); 
                    Session["LanguageID"] = sdr["id"].ToString();
                }
                sdr.Close();
                sdr.Dispose();
            }
            //BindLanguage();
        }
        
        Translations();
    }

    private void Translations()
    {

        this.TranControls(this.btnSubmit, new string[][] { new string[] { "005777", "登录" } });
        //this.TranControls(this.btnExit, new string[][] { new string[] { "006812", "重置" } });
        
    }

    //private void BindLanguage()
    //{
    //    SqlDataReader reader =DAL.DBHelper.ExecuteReader("SELECT ID,name,languageCode,languageRemark FROM language ORDER BY NAME DESC");
    //    string languageId = "";
    //    string languageName = "";
    //    this.ddlLanguage.Items.Clear();
    //    while (reader.Read())
    //    {
    //        languageId = reader["id"].ToString();
    //        languageName = reader["NAME"].ToString();
    //        this.ddlLanguage.Items.Add(new ListItem(languageName, languageId));
    //    }
    //    reader.Close();
    //    string LanguageID = Session["LanguageID"].ToString().Trim().ToLower();
    //    foreach (ListItem li in this.ddlLanguage.Items)
    //    {
    //        if (li.Value.ToLower() == LanguageID)
    //        {
    //            li.Selected = true;
    //        }
    //    }
    //    ddlLanguage_SelectedIndexChanged(null, null);

    //}
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //if (Application["jinzhi"] != null && this.txtName.Text.Trim() != DAL.DBHelper.ExecuteScalar("select top 1 number from manage where defaultmanager = 1").ToString() && Application["jinzhi"].ToString().IndexOf("G") != -1)
        //{
        //    //Response.Write("<div align=center style='font-size=12px;' >" + System.Configuration.ConfigurationSettings.AppSettings["jsError"].ToString());
        //    //Response.End();
        //    ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('" + GetTran("003151", "当前系统禁止登陆") + "')</script>");
        //    return;
        //}
        string manageId = BLL.CommonClass.CommonDataBLL.getManageID(1);
        if (!BlackListBLL.GetSystem("G") && this.txtName.Text.Trim() != manageId)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('" + GetTran("003151", "当前系统禁止登陆") + "')</script>");
            return;
        }
        //检验
        if (this.txtName.Text.Trim() == "")
        {
            msg = "<script language='javascript'>alert('" + GetTran("005596", "请输入用户名！") + "');</script>";
            return;
        }
        if (this.txtPwd.Text.Trim() == "")
        {
            msg = "<script language='javascript'>alert('" + GetTran("001854", "请输入密码！") + "');</script>";
            return;
        }
        if (this.txtName.Text.Length < 2 || this.txtName.Text.Length > 10)
        {
            msg = "<script language='javascript'>alert('" + GetTran("005598", "用户名必须在3-10位之间！") + "');</script>";
            return;
        }
        if (this.txtPwd.Text.Length < 4 || this.txtPwd.Text.Length > 10)
        {
            msg = "<script language='javascript'>alert(' " + GetTran("005600", "密码必须在6-10位之间！") + "');</script>";
            return;
        }
        if (this.txtValidate.Text.Trim() == "")
        {
            msg = "<script language='javascript'>alert('" + GetTran("005602", "请输入验证码！") + "');</script>";
            return;
        }

        if (Session["ValidateCode"] != null)
        {
            if (Session["ValidateCode"].ToString() == "")
            {
                msg = "<script language='javascript'>top.location.href='index.aspx';</script>";
                return;
            }
        }
        else
        {
            msg = "<script language='javascript'>top.location.href='index.aspx';</script>";
            return;
        }

        if (this.txtValidate.Text.Trim().ToLower() != Session["ValidateCode"].ToString().Trim().ToLower())
        {
            msg = "<script language='javascript'>alert('" + GetTran("005604", "验证码不正确！") + "');</script>";
            return;
        }
        
        if (BLL.other.Company.BlackListBLL.CheckBlacklistLogin(this.txtName.Text.Trim(), 2, Request.UserHostAddress) && this.txtName.Text.Trim()!=manageId)
        {
            msg = "<script>alert('" + GetTran("005606", "对不起，您的登陆权限失效，请与公司联系！") + "');</script>";
            return;
        }

        //if (BlackListBLL.GetLikeAddress(this.txtName.Text.Trim()) && this.txtName.Text.Trim() != manageId)
        //{
        //    msg = "<script>alert('" + GetTran("005606", "对不起，您的登陆权限失效，请与公司联系！") + "');</script>";
        //    return;
        //}

        string ipAddress = Request.ServerVariables["REMOTE_ADDR"].ToString();//客户IP地址
        try
        {
            if (this.txtName.Text.Trim() != manageId && 0 < BlackListBLL.GetLikeIPCount(ipAddress))
            {
                msg = "<script>alert('" + GetTran("005606", "对不起，您的登陆权限失效，请与公司联系！") + "');</script>";
                return;
            }
        }
        catch
        {
            return;
        }
        if (IndexBLL.CheckLogin("Company", txtName.Text.ToLower(), Encryption.Encryption.GetEncryptionPwd(txtPwd.Text, txtName.Text)))
        {
          
            Session.Remove("Company");
            Session.Remove("Store");
            Session.Remove("Member");
            Session.Remove("Branch");

            if (Session["Default_Currency"] != null)
            {
                Session.Remove("Default_Currency");
            }

            //购物车的session
            Session.Remove("proList");
            Session["page"] = "first.aspx";    
            Session["UserType"] = "1";
            Session["LoginUserType"] = "manage";
            Session["Company"] = this.txtName.Text.Trim();
            Session["TopManageID"] = this.txtName.Text.Trim();
            Session["WTH"] = BLL.other.Company.WordlTimeBLL.ConvertAddHours();
          //  Session["language"] = "chinese";
            HttpContext.Current.Session["LanguegeSelect"] = "Chinese";
   
            Session["permission"] = BLL.other.Company.DeptRoleBLL.GetAllPermission(Session["Company"].ToString());
            HttpContext.Current.Session["OperateBh"] = txtName.Text;

            if (Session["DHNumbers"] != null)
            {
                Session["DHNumbers"] = "";
                Session.Remove("DHNumbers");
            }
            int a = IndexBLL.insertLoginLog(this.txtName.Text, Encryption.Encryption.GetEncryptionPwd(txtPwd.Text, txtName.Text), "Company", DateTime.Now.ToUniversalTime(), CommonDataBLL.OperateIP, 1);

            Session["UserLastLoginDate"] = Convert.ToDateTime(IndexBLL.UplostLogin(this.txtName.Text, "1")).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours());

            Session.Timeout = 30;

            Response.Redirect("../Cmain.htm");
        }
        else
        {
            int a = IndexBLL.insertLoginLog(this.txtName.Text, this.txtPwd.Text, "Company", DateTime.Now.ToUniversalTime(), CommonDataBLL.OperateIP, 2);


            msg = "<script language='javascript'>alert('" + GetTran("005607", "用户名或密码不正确！") + "');</script>";
            return;
        }

    }

    protected void btnExit_Click(object sender, EventArgs e)
    {
    }
    //protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    string languageId = this.ddlLanguage.SelectedValue;
    //    SqlDataReader sdr = DAL.DBHelper.ExecuteReader("SELECT ID,name,languageCode,languageRemark FROM LANGUAGE WHERE id=" + languageId + "");
    //    while (sdr.Read())
    //    {
    //        Session["languageCode"] = sdr["languageCode"].ToString().Trim ();           
    //        Session["LanguageID"] = sdr["ID"].ToString().ToLower().Trim();           
    //    }
    //    sdr.Close();
    //    Translations();
    //}
    
}
