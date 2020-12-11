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
using Model;
using BLL;
using System.Collections.Generic;
using BLL.Registration_declarations;
using System.Data.SqlClient;
using BLL.CommonClass;
using Model.Other;
using BLL.Logistics;
using BLL.other.Company;
using DAL;

public partial class MemberMobile_RegisterMemberTG : System.Web.UI.Page
{
    RegistermemberBLL registermemberBLL = new RegistermemberBLL();
    protected LetUsOrder luo = new LetUsOrder();
    /// <summary>
    /// 页面加载事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetExpires(DateTime.Now);
        //Permissions.ThreeRedirect(Page, "../member/" + Permissions.redirUrl);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        if (!IsPostBack)
        {
            BindDate();
           // Translations();
            //Translate();
            if (Request.QueryString["id"] == null || Request.QueryString["id"]=="")
            {
                Response.Redirect("index.aspx");
            }
        }
    }

    public void BindDate()
    {
        CountryCity2.SelectCountry("中国", "", "", "");

        //会员编号
        string ags = BLL.CommonClass.CommonDataBLL.GetMemberNumber();
        txtNumber.Text ="会员编号："+ ags;
       

    }
  

    /// <summary>
    /// 注册报单流程（包括判断）
    /// 调用逻辑层中的所有方法
    /// </summary>
    public void AddOrderAndInfoProcess()
    {
        string number = CommonDataBLL.quanjiao(txtNumber.Text.Trim());
        number = number.Remove(0, number.Length - 8);
        txtNumber.Text = number.Remove(0, number.Length - 8);
        //会员名是否小于6位
        if (!registermemberBLL.NumberLength(number))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('抱歉！您输入的会员编号小于6位！');</script>", false);
            return;
        }
        if (!registermemberBLL.NumberCheckAgain(number))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('编号请输入字母，数字，横线！');</script>", false);
            return;
        }
        //验证手机号码是否重复
        if (registermemberBLL.CheckTeleTwice(txtTele.Text.Trim()) != null)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('抱歉！该手机号码已被注册！');</script>", false);
            return;
        }
        string name = CommonDataBLL.quanjiao(txtNamee.Text.Trim());
        if (name == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('真实姓名不能为空');</script>", false);
            return;
        }
        //判断用胡地址是否输入
        if (this.CountryCity2.Country == "请选择" || this.CountryCity2.Province == "请选择" || this.CountryCity2.City == "请选择" || this.CountryCity2.Xian == "请选择" || this.CountryCity2.Country == "" || this.CountryCity2.Province == "" || this.CountryCity2.City == "" || this.CountryCity2.Xian == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('对不起，请选择国家省份城市！');</script>", false);
            return;
        }
        if (DAL.CommonDataDAL.GetCPCCode(this.CountryCity2.Country, this.CountryCity2.Province, this.CountryCity2.City, this.CountryCity2.Xian) == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('对不起，请选择国家省份城市！');</script>", false);
            return;
        }
        MemberInfoModel mi = AddUserInfo();

        //if (mi.Placement != "8888888888")
        //{
        //    if (DBHelper.ExecuteScalar("select count(0) from memberinfo where placement='" + mi.Placement + "' and District=" + mi.District + "").ToString() != "0")
        //    {
        //        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('安置人所选区位已有人安置！');</script>", false);
        //        return;
        //    }
        //}

        Session["mbreginfo"] = mi;
        Session["OrderType"] = mi.OrderType;
        Session["UserType"] = 1;

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
            ofm.Type = Convert.ToInt32(rbltotaltype.SelectedValue);


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
           ofm.PhotoPath= "" ;
            Boolean flag = new AddOrderDataDAL().AddFinalOrder(ofm);
            if (flag)
            {

                ClientScript.RegisterStartupScript(GetType(), "msg", "<script>alert('注册成功');location.href='index.aspx';</script>", false);
                //if (Session["UserType"].ToString() == "1")
                //{
                //    int val = AddOrderDataDAL.OrderPayment(ofm.StoreID, ofm.OrderID, ofm.OperateIp, 3, 1, 10, "管理员", "", 1, -1, 1, 1, "", 0, "");
                //    if (val == 0)
                //    {
                //        PublicClass.SendMsg(1, ofm.OrderID, "");
                        
                //    }
                //}
            }
        }
        //string CheckMember = registermemberBLL.CheckMemberInProc1(mi.Number, mi.LoginPass, mi.Direct, mi.MobileTele);
        //CheckMember = new GroupRegisterBLL().GerCheckErrorInfo(CheckMember);
        
            
        //if(CheckMember=="1")
        //{
        // ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('注册成功！');</script>", false);
        //   return;
        //}

           

           
        

       

    }

    public MemberInfoModel AddUserInfo()
    {
        string tuij = Request.QueryString["id"];
        string place =(new AjaxClass()).GetAtuosetPlace(tuij);
        MemberInfoModel memberInfoModel = new MemberInfoModel();
        memberInfoModel.Number = CommonDataBLL.quanjiao(txtNumber.Text.ToUpper());
        memberInfoModel.Placement = place;
        memberInfoModel.Direct = Request.QueryString["id"];
        memberInfoModel.ExpectNum = BLL.CommonClass.CommonDataBLL.getMaxqishu();
        memberInfoModel.OrderID = "";
        memberInfoModel.StoreID = "8888888888";
        memberInfoModel.Name = "";
        memberInfoModel.PetName = Encryption.Encryption.GetEncryptionName(txtNamee.Text.Trim());
        memberInfoModel.OrderType = 21;
        memberInfoModel.LoginPass = Encryption.Encryption.GetEncryptionPwd(txtPassword.Text.Trim(), memberInfoModel.Number);
        memberInfoModel.AdvPass = Encryption.Encryption.GetEncryptionPwd(txtPassword.Text.Trim(), memberInfoModel.Number);
        memberInfoModel.LevelInt = 0;//会员级别
        memberInfoModel.RegisterDate = DateTime.UtcNow;
        memberInfoModel.PaperType.Id = 0;
        memberInfoModel.Sex = 0;
        memberInfoModel.Birthday =Convert.ToDateTime("1990-01-01");

        memberInfoModel.Assister = "";
        memberInfoModel.OfficeTele = "";

        memberInfoModel.HomeTele = "";
        memberInfoModel.MobileTele = txtTele.Text;

        memberInfoModel.FaxTele = "";
        string country = this.CountryCity2.Country;//控件
        string province = this.CountryCity2.Province;//控件
        string city = this.CountryCity2.City; //控件
        string xian = this.CountryCity2.Xian;
        memberInfoModel.City.Country = country;
        memberInfoModel.City.Province = province;
        memberInfoModel.City.City = city;
        memberInfoModel.City.Xian = xian;
        memberInfoModel.CPCCode = DAL.CommonDataDAL.GetCPCCode(country, province, city, xian);
        memberInfoModel.Address = Encryption.Encryption.GetEncryptionAddress(CommonDataBLL.quanjiao(this.txtAddress.Text.Trim()));
        memberInfoModel.PostalCode = "";

        var insert = @"INSERT INTO [ConsigneeInfo]
                               ([Number],[Consignee],[MoblieTele],[CPCCode],[Address]
                               ,IsDefault
                                )
                         VALUES
                               (@Number,@Consignee,@MoblieTele,@CPCCode,@Address,@IsDefault)";

        SqlParameter[] insertpara = {
                                      new SqlParameter("@Number",SqlDbType.VarChar),
                                      new SqlParameter("@Consignee",SqlDbType.VarChar),
                                      new SqlParameter("@MoblieTele",SqlDbType.VarChar),
                                      new SqlParameter("@CPCCode",SqlDbType.VarChar),
                                      new SqlParameter("@Address",SqlDbType.VarChar),
                                      
                                      new SqlParameter("@IsDefault",SqlDbType.Bit),
                                  };
        insertpara[0].Value = CommonDataBLL.quanjiao(txtNumber.Text.ToUpper());
        insertpara[1].Value = Encryption.Encryption.GetEncryptionName(txtNamee.Text.Trim());
        insertpara[2].Value = txtTele.Text;
        insertpara[3].Value = DAL.CommonDataDAL.GetCPCCode(country, province, city, xian);
        insertpara[4].Value = Encryption.Encryption.GetEncryptionAddress(CommonDataBLL.quanjiao(this.txtAddress.Text.Trim()));
        insertpara[5].Value = true;

        var insertvalue = DBHelper.ExecuteNonQuery(insert, insertpara, CommandType.Text);

        memberInfoModel.PaperType.PaperTypeCode = "";
        memberInfoModel.PaperNumber = "";

      
        memberInfoModel.BankCode = "";
        memberInfoModel.BankAddress = "";
        memberInfoModel.BankBook = memberInfoModel.Name;
        memberInfoModel.BankCard = "";
        
        memberInfoModel.BCPCCode = "";
        memberInfoModel.Remark = "";
        memberInfoModel.ChangeInfo = "";
        memberInfoModel.Email = "";
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
    protected void Button1_Click(object sender, EventArgs e)
    {

        AddOrderAndInfoProcess();
    }
   
}