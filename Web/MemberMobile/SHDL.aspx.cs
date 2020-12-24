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
using BLL.CommonClass;
using DAL;
using System.Data.SqlClient;
using BLL.other;
using Newtonsoft.Json.Linq;
using BLL.other.Company;
using System.Net;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Model;
using BLL.Registration_declarations;

public partial class Member_SHDL : BLL.TranslationBase
{
    protected string msg = "";
    protected string msgg = "";
    RegistermemberBLL registermemberBLL = new RegistermemberBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region PC浏览进行转向

            /*System.Web.HttpBrowserCapabilities myBrowserCaps = Request.Browser;
            int isMobile = ((System.Web.Configuration.HttpCapabilitiesBase)myBrowserCaps).IsMobileDevice ? 1 : 0;

            if (isMobile == 0)
            {
                Response.Redirect("../Member/Index.aspx");
                return;
            }*/
            //改用脚本判断
            //if (IsPC())
            //{
            //    Response.Redirect("www.sanble.net");
            //    Response.Redirect("../Member/Index.aspx");
            //    return;
            //}

            #endregion

            GetFunction();
            if (Session["Member"] != null)
            {
                Session["UserType"] = 3;
                Session["LUOrder"] = Session["Member"].ToString() + ",12";
                Session["languageCode"] = "L001";
                
              // 
            }

            if (Session["languageCode"] == null)
            {
                SqlDataReader sdr = DAL.DBHelper.ExecuteReader("Select Top 1 * From LANGUAGE ORDER BY ID");
                while (sdr.Read())
                {
                    Session["languageCode"] = sdr["languageCode"].ToString().Trim();
                    Session["LanguageID"] = sdr["id"].ToString();
                }
                sdr.Close();
                sdr.Dispose();
            }
            // BindLanguage();
        }
        Translations();
    }
    /// <summary>
    /// 翻译
    /// </summary>
    private void Translations()
    {
        // this.TranControls(this.btnSubmit, new string[][] { new string[] { "005777", "登录" } });
        //this.TranControls(this.btnReset, new string[][] { new string[] { "001614", "取消" } });
    }


    public void GetFunction()
    {
        try
        {

      
        if (Request.QueryString.Count > 0)
        {
            string code = Request.QueryString["code"].ToString();
            string grant_type = "authorization_code";

            string yum = "https://oauth.factorde.com/api/sns/oauth/access_token";
            Dictionary<String, String> myDictionary = new Dictionary<String, String>();
            myDictionary.Add("app_id", PublicClass.app_id);
            myDictionary.Add("secret", PublicClass.app_secret);
            myDictionary.Add("code", code);
            myDictionary.Add("grant_type", grant_type);

            //string jsonStr = PublicClass.GetSignContent(myDictionary);
            //jsonStr = HttpUtility.UrlEncode(jsonStr);//字符串进行编码，参数中有中文时一定需要这一步转换，否则接口接收的到参数会乱码
            string rsp = PublicClass.doHttpPost(yum, myDictionary);
            JObject studentsJson = JObject.Parse(rsp);
            Session["Member"] = studentsJson["data"]["openid"].ToString();
            string access_token = studentsJson["data"]["access_token"].ToString();
            Session["access_token"] = access_token;
            if (Session["Member"].ToString() != "")
            {
                DataTable dt = ChangeTeamBLL.GetMemberInfoDataTable(Session["Member"].ToString());
                if (dt.Rows.Count > 0)
                {
                    Session["UserType"] = 3;
                    Session["LUOrder"] = Session["Member"].ToString() + ",12";
                    Session["languageCode"] = "L001";
                    Response.Redirect("SHJF.aspx");
                }
                else
                {

                    ClientScript.RegisterStartupScript(GetType(), "msg", "<script>alert('" + GetTran("000000", "登陆失败，请确认制度是否注册！") + "');</script>", false); return;
                }
                
            }

            return;
        }
        else {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>window.location.href = 'https://oauth.factorde.com/api/connect/oauth/authorize?app_id=4f95ab748e204c65d0bdaa61b4e3f1d7&redirect_uri=http%3a%2f%2fsp.factorde.com%2fMemberMobile%2fSHDL.aspx&response_type=code&scope=snsapi_base&wallet_redirect=http%3a%2f%2fzd.factorde.com%2fMemberMobile%2fSHDL.aspx';</script>");
            return;
        }
        }
        catch (Exception)
        {

            ClientScript.RegisterStartupScript(GetType(), "msg", "<script>alert('" + GetTran("000000", "登陆失败，请确认云端钱包是否登录") + "');</script>", false); return;
        }

    }
    

   

  
   
    /// <summary>
    /// 登陆事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //protected void btnSubmit_Click(object sender, EventArgs e)
    //{
        
    //    if (!BlackListBLL.GetSystem("H"))
    //    {
    //        ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('" + GetTran("000000", "当前系统禁止登陆") + "')</script>");
    //        return;
    //    }
    //    //检验
    //    if (this.txtName.Text.Trim() == "")
    //    {
    //        msg = "<script language='javascript'>alert('" + GetTran("000000", "请输入用户名！") + "');</script>";
    //        return;
    //    }
    //    if (this.txtPwd.Text.Trim() == "")
    //    {
    //        msg = "<script language='javascript'>alert('" + GetTran("000000", "请输入密码！") + "');</script>";
    //        return;
    //    }
    //    if (this.txtName.Text.Length < 2 || this.txtName.Text.Length > 12)
    //    {
    //        msg = "<script language='javascript'>alert('" + GetTran("000000", "用户名必须在3-12位之间！") + "');</script>";
    //        return;
    //    }
    //    if (this.txtPwd.Text.Length < 4 || this.txtPwd.Text.Length > 16)
    //    {
    //        msg = "<script language='javascript'>alert(' " + GetTran("000000", "密码必须在6-16位之间！") + "');</script>";
    //        return;
    //    }
    //    if (this.txtValidate.Text.Trim() == "")
    //    {
    //        msg = "<script language='javascript'>alert('" + GetTran("000000", "请输入验证码！") + "');</script>";
    //        return;
    //    }

    //    if (Session["ValidateCode"] != null)
    //    {
    //        if (Session["ValidateCode"].ToString() == "")
    //        {
    //            msg = "<script language='javascript'>top.location.href='index.aspx';</script>";
    //            return;
    //        }
    //    }
    //    else
    //    {
    //        msg = "<script language='javascript'>top.location.href='index.aspx';</script>";
    //        return;
    //    }

    //    if (this.txtValidate.Text.Trim().ToLower() != Session["ValidateCode"].ToString().Trim().ToLower())
    //    {
    //        msg = "<script language='javascript'>alert('" + GetTran("000000", "验证码不正确！") + "');</script>";
    //        return;
    //    }

    //    if (BLL.other.Company.BlackListBLL.CheckBlacklistLogin(this.txtName.Text.Trim(), 0, Request.UserHostAddress))
    //    {
    //        msg = "<script>alert('" + GetTran("000000", "对不起，您的登陆权限失效，请与公司联系！") + "');</script>";
    //        return;
    //    }

    //    string manageId = BLL.CommonClass.CommonDataBLL.getManageID(3);
    //    if (BlackListBLL.GetLikeAddress(this.txtName.Text.Trim()) && this.txtName.Text.Trim() != manageId)
    //    {
    //        msg = "<script>alert('" + GetTran("000000", "对不起，您的登陆权限失效，请与公司联系！") + "');</script>";
    //        return;
    //    }

    //    string ipAddress = Request.ServerVariables["REMOTE_ADDR"].ToString();//客户IP地址
    //    try
    //    {
    //        if (this.txtName.Text.Trim() != manageId && 0 < BlackListBLL.GetLikeIPCount(ipAddress))
    //        {
    //            msg = "<script>alert('" + GetTran("000000", "对不起，您的登陆权限失效，请与公司联系！") + "');</script>";
    //            return;
    //        }
    //    }
    //    catch
    //    {
    //        return;
    //    }
    //    DataTable dtss = DAL.DBHelper.ExecuteDataTable("select top 1 * from memberinfo where Number='" + txtName.Text.Trim() + "' order by ID");
    //    if(dtss.Rows.Count==0)
    //    {
        
    //        DataTable dts = DAL.DBHelper.ExecuteDataTable("select top 1 * from memberinfo where MobileTele='" + txtName.Text.Trim() + "' order by ID");
    //        if (dts.Rows.Count != 0)
    //        {
    //            string num = dts.Rows[0]["Number"].ToString();//获取会员编号
    //            txtName.Text = num;
    //        }
    //        else
    //        {
    //            msg = "<script>alert('" + GetTran("000000", "会员编号或手机号输入有误！") + "');</script>";
    //            return;
    //        }
            
    //    }
    //    if (IndexBLL.CheckLogin("Member", txtName.Text.Trim().ToLower(), Encryption.Encryption.GetEncryptionPwd(txtPwd.Text.Trim(), txtName.Text.Trim())))
    //    {
    //        if (Session["mbreginfo"] != null)
    //        {
    //            Session.Remove("mbreginfo");
    //        }
    //        if (Session["fxMemberModel"] != null)
    //        {
    //            Session.Remove("fxMemberModel");
    //        }
    //        if (Session["Default_Currency"] != null)
    //        {
    //            Session.Remove("Default_Currency");
    //        }
    //        Session.Remove("Company");
    //        Session.Remove("Store");
    //        Session.Remove("Member");
    //        Session.Remove("Branch");

    //        //购物车的session
    //        Session.Remove("proList");
    //        Session["Default_Currency"] = CommonDataBLL.GetMember(this.txtName.Text);
    //        Session["UserType"] = "3";
    //        Session["LUOrder"] = this.txtName.Text + ",22";
           
    //        string cartCountsql = "select isnull(sum(PreferentialPrice*proNum),0) as TotalPriceAll,isnull(sum(PreferentialPV*proNum),0) as TotalPvAll,isnull(sum(proNum),0) as totalNum from MemShopCart,Product where MemShopCart.proId=Product.productId  and mType=" + Session["UserType"].ToString() + " and odType=" + Session["LUOrder"].ToString().Split(',')[1] + " and memBh='" + this.txtName.Text + "'";
    //        DataTable dt2 = DAL.DBHelper.ExecuteDataTable(cartCountsql);

    //        if (dt2.Rows.Count > 0)
    //        {
    //            Session["CartCount"] = Convert.ToInt32(dt2.Rows[0]["totalNum"]);
    //        }else
    //        {
    //            Session["CartCount"] = 0;
    //        }
          

    //        //Session["LanguageCode"] = ddlLanguage.SelectedValue;

    //        string strSql = "select top 1 isnull(MemberState ,0) from memberInfo where number=@userName";
    //        SqlParameter[] para = {
    //                                      new SqlParameter("@userName",SqlDbType.NVarChar,20)
    //                                  };
    //        para[0].Value = this.txtName.Text;
    //        int isActive = (int)DBHelper.ExecuteScalar(strSql, para, CommandType.Text);
    //        //if (isActive == 0)
    //        //{
    //        //    msg = "<script>alert('" + GetTran("000000", "会员未激活，不能登录！") + "');</script>";
    //        //    return;
    //            /*
    //            object o_ordid = DBHelper.ExecuteScalar("select OrderID  from MemberOrder  where Number='" + this.txtName.Text.Trim() + "' and IsAgain=0");
    //            if (o_ordid != null)
    //            {
    //                string orderid = o_ordid.ToString();
    //                if (MemberOrderDAL.Getvalidteiscanpay(orderid, this.txtName.Text.Trim()))//限制订单必须有订货所属店铺推荐人协助人支付)
    //                {
    //                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script> alert('该订单不属于您的协助或推荐报单，不能完成支付！'); window.location.href='../Logout.aspx'; </script>");

    //                    return;
    //                }
    //                ScriptManager.RegisterStartupScript(this, GetType(), "mag", "var formobj=document.createElement('form');"
    //                                        + "formobj.action='../payserver/chosepay.aspx?blif=" + EncryKey.GetEncryptstr(orderid, 1, 1) + "';" +
    //                                        "formobj.method='post';formobj.target='_blank';document.body.appendChild(formobj); formobj.submit();", true);
    //            }
    //             * */
    //        //}
    //        //else
    //        if (isActive == 2)
    //        {
    //            msg = "<script>alert('" + GetTran("000000", "会员已注销，不能登录！") + "');</script>";
    //            return;
    //        }

    //        Session["Member"] = this.txtName.Text;
    //        Session["EmailCount"] = AjaxClass.GetNewEmailCount();
    //        // Session["language"] = "chinese";
    //        // MemberOrderDAL.clearouttimenopay();//执行规定时间未确认汇款 释放尾数
    //        if (Session["DHNumbers"] != null)
    //        {
    //            Session["DHNumbers"] = "";
    //            Session.Remove("DHNumbers");
    //        }
    //        int a = IndexBLL.insertLoginLog(this.txtName.Text, Encryption.Encryption.GetEncryptionPwd(txtPwd.Text, txtName.Text), "Member", DateTime.Now.ToUniversalTime(), CommonDataBLL.OperateIP, 1);
    //        //string sql = "update memberinfo set language=" + Session["LanguageID"].ToString() + " where number='" + txtName.Text.Trim() + "'";
    //        //DBHelper.ExecuteNonQuery(sql);

    //        Session["UserLastLoginDate"] = Convert.ToDateTime(IndexBLL.UplostLogin(this.txtName.Text, "3"));
    //        Session["WTH"] = BLL.other.Company.WordlTimeBLL.ConvertAddHours();
    //        Session.Timeout = 30;

    //        //Response.Redirect("../Hmain.htm");
    //        Response.Redirect("First.aspx");
    //    }
    //    else
    //    {
    //        int a = IndexBLL.insertLoginLog(this.txtName.Text, this.txtPwd.Text, "Member", DateTime.Now.ToUniversalTime(), CommonDataBLL.OperateIP, 2);
    //        msg = "<script>alert('" + GetTran("000000", "用户名或密码不正确！") + "');</script>";
    //        return;
    //    }
    //}
    
    public bool IsPC()
    {
        string userAgentInfo = Request.UserAgent;
        string[] Agents = new string[]{"Android", "iPhone",
                    "SymbianOS", "Windows Phone",
                    "iPad", "iPod"};
        var flag = true;
        for (var v = 0; v < Agents.Length; v++)
        {
            if (userAgentInfo.IndexOf(Agents[v]) > 0)
            {
                flag = false;
                break;
            }
        }
        return flag;
    }
}