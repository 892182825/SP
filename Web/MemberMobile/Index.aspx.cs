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

public partial class Member_Index : BLL.TranslationBase
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
               
                        string sql = "select  Number,Direct,name from memberinfo  where number='" + Session["Member"].ToString() + "'";
                        DataTable dt = DBHelper.ExecuteDataTable(sql);
                        if (dt.Rows.Count > 0)
                        {
                        if (dt.Rows[0]["Direct"].ToString() != null || dt.Rows[0]["Direct"].ToString() != "" )
                        {
                            string sqlqq = "select  Number from memberinfo  where MobileTele='" + dt.Rows[0]["Direct"].ToString() + "'";
                            DataTable dtt = DBHelper.ExecuteDataTable(sqlqq);
                            if (dtt.Rows.Count > 0)
                            {
                                string sqlz = "update memberinfo set Direct='" + dtt.Rows[0][0].ToString() + "' where number='" + Session["Member"].ToString() + "'";
                                DBHelper.ExecuteNonQuery(sqlz);
                                string sqll = "update MemberInfoBalance" + CommonDataBLL.getMaxqishu() + " set Direct='" + dtt.Rows[0][0].ToString() + "' where number='" + Session["Member"].ToString() + "'";
                                DBHelper.ExecuteNonQuery(sqll);
                            }
                        }
                        //if (dtt.Rows[0]["name"].ToString() == null || dtt.Rows[0]["name"].ToString() == "")
                        //{
                        //    string sql = "update memberinfo set name='" + dtt.Rows[0][0].ToString() + "' where number='" + Session["Member"].ToString() + "'";
                        //    DBHelper.ExecuteNonQuery(sql);
                        //    string sqll = "update MemberInfoBalance" + CommonDataBLL.getMaxqishu() + " set Direct='" + dtt.Rows[0][0].ToString() + "' where number='" + Session["Member"].ToString() + "'";
                        //    DBHelper.ExecuteNonQuery(sqll);
                        //}
                    }

                 
                else
                {


                    

                    AddOrderAndInfoProcess(access_token);
                }
                Session["UserType"] = 3;
                Session["LUOrder"] = Session["Member"].ToString() + ",12";
                Session["languageCode"] = "L001";
                Response.Redirect("First.aspx");
            }

            return;
        }
        else {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>window.location.href = 'https://oauth.factorde.com/api/connect/oauth/authorize?app_id=4f95ab748e204c65d0bdaa61b4e3f1d7&redirect_uri=http%3a%2f%2fzd.factorde.com%2fMemberMobile%2fIndex.aspx&response_type=code&scope=snsapi_base&wallet_redirect=http%3a%2f%2fzd.factorde.com%2fMemberMobile%2fIndex.aspx';</script>");
            return;
        }
        }
        catch (Exception)
        {

            ClientScript.RegisterStartupScript(GetType(), "msg", "<script>alert('" + GetTran("000000", "登陆失败，请确认云端钱包是否登录") + "');</script>", false); return;
        }

    }
    

    /// <summary>
    /// 注册报单流程（包括判断）
    /// 调用逻辑层中的所有方法
    /// </summary>
    public void AddOrderAndInfoProcess(string access_token)
    {

        try
        {

        
        string xx = "https://oauth.factorde.com/api/sns/user/info";
        Dictionary<String, String> myD = new Dictionary<String, String>();
        myD.Add("app_id", PublicClass.app_id);
        myD.Add("access_token", access_token);
        myD.Add("lang", "zh_CN");
        myD.Add("version", "1.0");
        myD.Add("charset", "utf8");
        myD.Add("openid", Session["Member"].ToString());
        //string jsonStr = PublicClass.GetSignContent(myD);
        //jsonStr = HttpUtility.UrlEncode(jsonStr);//字符串进行编码，参数中有中文时一定需要这一步转换，否则接口接收的到参数会乱码
        string hz = PublicClass.GetFunction(xx, myD);
        //ck.Text = hz;
        JObject studentsJson = JObject.Parse(hz);
        Session["Member"] = studentsJson["data"]["openid"].ToString();
        if (studentsJson["data"]["nickname"].ToString() == null || studentsJson["data"]["nickname"].ToString() == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "msg", "<script>alert('" + GetTran("000000", "请先实名认证") + "');</script>", false);
            return;
        
        }
        int countdls = Convert.ToInt32(DAL.DBHelper.ExecuteScalar("select count(0) from memberinfo where number='" + Session["Member"].ToString() + "'"));

        if (countdls <= 0)
        {


            string number = Session["Member"].ToString();

            string name = studentsJson["data"]["nickname"].ToString();
            string mobile_number = studentsJson["data"]["mobile_number"].ToString();
            string email = studentsJson["data"]["email"].ToString();
            string parent_mobile_number = studentsJson["data"]["parent_mobile_number"].ToString();
            string parent_email = studentsJson["data"]["parent_email"].ToString();

            MemberInfoModel mi = AddUserInfo(number, name, mobile_number, email, parent_mobile_number);

            Session["mbreginfo"] = mi;
            //Session["OrderType"] = 22;
            Session["UserType"] = 3;
            Session["LUOrder"] = Session["Member"].ToString() + ",12";
            Session["languageCode"] = "L001";

            if (Session["mbreginfo"] != null)
            {
                decimal totalmoeny = 0.00M;
                decimal bili = 0.00M;
                DataTable dts = DAL.DBHelper.ExecuteDataTable("select top 1 * from config order by createdate desc");
                if (dts.Rows != null && dts.Rows.Count > 0)
                {
                    totalmoeny = Convert.ToDecimal(dts.Rows[0]["para1"]);//投资金额
                    bili = Convert.ToDecimal(dts.Rows[0]["para4"]);
                }
                OrderFinalModel ofm = new OrderFinalModel();
                var dayPrice = CommonDataBLL.GetMaxDayPrice();

                var value = Convert.ToDecimal(totalmoeny) / Convert.ToDecimal(dayPrice);//投资金额换化石斛积分
                decimal totalpv = 0.0M;
                var expect = CommonDataBLL.getMaxqishu();


                totalpv = Convert.ToDecimal(value);
                ofm.InvestJB = Convert.ToDecimal(value * bili);//投资石斛积分币数量
                ofm.PriceJB = Convert.ToDecimal(dayPrice);//石斛积分当前市价
                ofm.SendWay = 1;
                ofm.Number = mi.Number;
                ofm.Placement = mi.Placement;
                ofm.Direct = mi.Direct;
                ofm.ExpectNum = expect;
                ofm.OrderID = registermemberBLL.GetOrderInfo("add", null);
                ofm.StoreID = mi.StoreID;
                ofm.Name = mi.Name;
                ofm.PetName = mi.PetName;
                ofm.LoginPass = mi.LoginPass;
                ofm.AdvPass = mi.AdvPass;
                ofm.LevelInt = mi.LevelInt;

                ofm.RegisterDate = mi.RegisterDate;
                ofm.Birthday = mi.Birthday;
                ofm.Sex = mi.Sex;
                ofm.HomeTele = mi.HomeTele;
                ofm.OfficeTele = mi.OfficeTele;
                ofm.MobileTele = mi.MobileTele;
                ofm.FaxTele = mi.FaxTele;
                ofm.CPCCode = mi.CPCCode;
                ofm.Address = mi.Address;
                ofm.PostalCode = mi.PostalCode;
                ofm.PaperType.PaperTypeCode = mi.PaperType.PaperTypeCode;
                ofm.PaperNumber = mi.PaperNumber;
                ofm.BankCode = mi.BankCode;
                ofm.BankAddress = mi.BankAddress;
                ofm.BankCard = mi.BankCard;
                ofm.BCPCCode = mi.BCPCCode;
                ofm.BankBook = mi.BankBook;
                ofm.Remark = mi.Remark;
                ofm.ChangeInfo = mi.ChangeInfo;
                ofm.PhotoPath = mi.PhotoPath;
                ofm.Email = mi.Email;
                ofm.IsBatch = mi.IsBatch;
                ofm.Language = mi.Language;
                ofm.OperateIp = mi.OperateIp;
                ofm.OperaterNum = mi.OperaterNum;
                ofm.Answer = mi.Answer;
                ofm.Question = mi.Question;
                ofm.Error = mi.Error;
                ofm.Bankbranchname = mi.Bankbranchname;
                ofm.Flag = mi.Flag;
                ofm.Assister = mi.Assister;
                ofm.District = mi.District;

                ofm.TotalMoney = Convert.ToDecimal(totalmoeny);
                ofm.TotalPv = Convert.ToDecimal(totalpv);//投资金额兑换成石斛积分
                ofm.OrderType = mi.OrderType;
                ofm.OrderExpect = expect;
                ofm.StandardcurrencyMoney = ofm.TotalMoney;
                ofm.PaymentMoney = ofm.TotalMoney;
                ofm.OrderDate = DateTime.UtcNow;
                ofm.RemittancesId = "";
                ofm.ElectronicaccountId = "";
                ofm.Type = 0;


                ofm.ConCity.Country = "";
                ofm.ConCity.Province = "";
                ofm.ConCity.City = "";
                ofm.ConCity.Xian = "";
                ofm.ConAddress = "";
                ofm.CCPCCode = "";



                ofm.ConTelPhone = "";
                ofm.ConMobilPhone = "";
                ofm.CarryMoney = 0;
                ofm.ConPost = "";
                ofm.Consignee = "";
                ofm.ConZipCode = "";

                ofm.ProductIDList = "";
                ofm.QuantityList = "";
                ofm.NotEnoughProductList = "";
                ofm.PhotoPath = "";
                Boolean flag = new AddOrderDataDAL().AddFinalOrder(ofm);
                if (flag)
                {
                   
                    int val = AddOrderDataDAL.OrderPayment(ofm.StoreID, ofm.OrderID, ofm.OperateIp, 3, 1, 10, "管理员", "", 1, -1, 1, 1, "", 0, "");
                    if (val == 0)
                    {
                        //PublicClass.SendMsg(1, ofm.OrderID, "");
                        DataTable dt = ChangeTeamBLL.GetMemberInfoDataTable(Session["Member"].ToString());
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0][0].ToString() != "" && dt.Rows[0][0].ToString() != null)
                            {
                                string sqlqq = "select  Number from memberinfo  where MobileTele='" + dt.Rows[0][0].ToString() + "'";
                                DataTable dtt = DBHelper.ExecuteDataTable(sqlqq);
                                if (dtt.Rows.Count > 0)
                                {
                                    string sql = "update memberinfo set Direct='" + dt.Rows[0][0].ToString() + "' where number='" + Session["Member"].ToString() + "'";
                                    DBHelper.ExecuteNonQuery(sql);
                                    string sqll = "update MemberInfoBalance" + CommonDataBLL.getMaxqishu() + " set Direct='" + dt.Rows[0][0].ToString() + "' where number='" + Session["Member"].ToString() + "'";
                                    DBHelper.ExecuteNonQuery(sqll);

                                }

                            }
                        }
                        Response.Redirect("First.aspx");
                        ClientScript.RegisterStartupScript(GetType(), "msg", "<script>alert('" + GetTran("000436", "注册成功") + "');location.href='index.aspx';</script>", false);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "msg", "<script>alert('" + GetTran("000000", "登陆失败，请联系管理员") + "');</script>", false);
                    }
                }
            }
        }
        else
        {
            DataTable dt = ChangeTeamBLL.GetMemberInfoDataTable(Session["Member"].ToString());
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString() != "" && dt.Rows[0][0].ToString() != null)
                {
                    string sqlqq = "select  Number,Direct,name from memberinfo  where MobileTele='" + dt.Rows[0][0].ToString() + "'";
                    DataTable dtt = DBHelper.ExecuteDataTable(sqlqq);
                    if (dtt.Rows.Count > 0)
                    {
                        if (dtt.Rows[0]["Direct"].ToString() != null && dtt.Rows[0]["Direct"].ToString() !="")
                        {
                        string sql = "update memberinfo set Direct='" + dtt.Rows[0][0].ToString() + "' where number='" + Session["Member"].ToString() + "'";
                        DBHelper.ExecuteNonQuery(sql);
                        string sqll = "update MemberInfoBalance" + CommonDataBLL.getMaxqishu() + " set Direct='" + dtt.Rows[0][0].ToString() + "' where number='" + Session["Member"].ToString() + "'";
                        DBHelper.ExecuteNonQuery(sqll);
                        }
                        if (dtt.Rows[0]["name"].ToString() == "" || dtt.Rows[0]["name"].ToString() == null)
                        {
                            string name = studentsJson["data"]["nickname"].ToString();
                            string sql = "update memberinfo set name='" + name + "' where number='" + Session["Member"].ToString() + "'";
                            DBHelper.ExecuteNonQuery(sql);

                        }

                    }

                }
            }
            

            Response.Redirect("First.aspx");
        }
        }
        catch (Exception)
        {

            ClientScript.RegisterStartupScript(GetType(), "msg", "<script>alert('" + GetTran("000000", "登陆失败，请确认云端钱包是否登录") + "');</script>", false);
        }


    }

    public MemberInfoModel AddUserInfo(string number, string name, string mobile_number, string email, string parent_mobile_number)
    {
        string tuij = parent_mobile_number;
        if (tuij == "")
        {
            tuij = "d2918447acbc262fbcb01efce558752c";
        }
        //string place = (new AjaxClass()).GetAtuosetPlace(tuij);
        MemberInfoModel memberInfoModel = new MemberInfoModel();
        memberInfoModel.Number = number;
        memberInfoModel.Placement = "";
        memberInfoModel.Direct = tuij;
        memberInfoModel.ExpectNum = BLL.CommonClass.CommonDataBLL.getMaxqishu();
        memberInfoModel.OrderID = "";
        memberInfoModel.StoreID = "8888888888";
        memberInfoModel.Name = name;
        memberInfoModel.PetName = "";
        memberInfoModel.OrderType = 21;
        memberInfoModel.LoginPass = "";
        memberInfoModel.AdvPass = "";
        memberInfoModel.LevelInt = 0;//会员级别
        memberInfoModel.RegisterDate = DateTime.UtcNow;
        memberInfoModel.PaperType.Id = 0;
        memberInfoModel.Sex = 0;
        memberInfoModel.Birthday = Convert.ToDateTime("1990-01-01");

        memberInfoModel.Assister = "";
        memberInfoModel.OfficeTele = "";

        memberInfoModel.HomeTele = "";
        memberInfoModel.MobileTele = mobile_number;

        memberInfoModel.FaxTele = "";

        memberInfoModel.City.Country = "";
        memberInfoModel.City.Province = "";
        memberInfoModel.City.City = "";
        memberInfoModel.City.Xian = "";
        memberInfoModel.CPCCode = "";
        memberInfoModel.Address = "";
        memberInfoModel.PostalCode = "";

        memberInfoModel.PaperType.PaperTypeCode = "";
        memberInfoModel.PaperNumber = "";


        memberInfoModel.BankCode = "";
        memberInfoModel.BankAddress = "";
        memberInfoModel.BankBook = memberInfoModel.Name;
        memberInfoModel.BankCard = "";

        memberInfoModel.BCPCCode = "";
        memberInfoModel.Remark = "";
        memberInfoModel.ChangeInfo = "";
        memberInfoModel.Email = email;
        //memberInfoModel.District = SearchPlacement_DoubleLines1.District;
        memberInfoModel.District = 0;
        memberInfoModel.Answer = "";
        memberInfoModel.Question = "";
        memberInfoModel.IsBatch = Convert.ToInt32(ViewState["isBatch"]);//不是批量注册  modify
        memberInfoModel.Language = 1;
        memberInfoModel.OperateIp = CommonDataBLL.OperateIP;//调用方法

        memberInfoModel.OperaterNum = CommonDataBLL.OperateBh;//调用方法

        memberInfoModel.Error = ViewState["Error"] == null ? "" : ViewState["Error"].ToString();

        memberInfoModel.Bankbranchname = "";
        return memberInfoModel;
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