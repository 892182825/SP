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
using System.Text;
using Model;
using BLL.Registration_declarations;

public partial class MemberMobile_ReCast : BLL.TranslationBase
{
    RegistermemberBLL registermemberBLL = new RegistermemberBLL();
    decimal num = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
       // AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        if (!IsPostBack)
        {
            Bind();
        }
    }

    public void Bind()
    {
       
        decimal sum = 0;
        int lv = 0;

        DataTable dt_one = DAL.DBHelper.ExecuteDataTable("select isnull(jackpot,0)-isnull([out],0)-membership as xjye,* from memberinfo where Number='" + Session["Member"].ToString() + "'");
        if (dt_one.Rows != null && dt_one.Rows.Count > 0)
        {
            sum = Convert.ToDecimal(dt_one.Rows[0]["xjye"]);//获取账户金额
            //lv = Convert.ToInt32(dt_one.Rows[0]["LevelInt"]);//获取账户等级
        }
        string sqllll = "select Level from MemberInfoBalance" + CommonDataBLL.getMaxqishu() + " where number='" + Session["Member"].ToString() + "'";
        DataTable dat = DBHelper.ExecuteDataTable(sqllll);
        if (dat.Rows.Count > 0)
        {

            lv = Convert.ToInt32(dat.Rows[0]["Level"]);
        }

        DataTable dt = DAL.DBHelper.ExecuteDataTable("select isnull(sum(TotalPv),0) as TotalPv from memberorder where Number='" + Session["Member"].ToString() + "' and ordertype=22 and PayExpectNum=1");
        if (dt.Rows != null && dt.Rows.Count > 0)
        {
            sum = Convert.ToDecimal(dt.Rows[0]["TotalPv"]);//获取账户金额
            
        }

        num = Common.GetnowPrice();//获取当前石斛积分价格
        hidnew.Value = num.ToString();
        if(lv==1)
        { Jackpot.Text="500"; }
        if(lv==2)
        { Jackpot.Text="1000"; }
        if(lv==3)
        { Jackpot.Text="3000"; }
        if(lv==4)
        { Jackpot.Text="5000"; }
        if(lv==0)
        {
            if (sum != null && sum != 0)
            {
                Jackpot.Text = Convert.ToInt32(sum).ToString();
            }
            else
            {
                Jackpot.Text = "无";

            }
            }
        
        
         
    }


    
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        lind();
    }
    public void lind()
    {
        if (ft.Text == "" && newprice.Value=="")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('抱歉！锁仓金额不能为空！');</script>", false);
            return;
        }
        try
        {
            decimal a1 = Convert.ToDecimal(newprice.Value);//复投的金额

            if (Jackpot.Text != "无")
            {
                int ptdj = Convert.ToInt32(Jackpot.Text);
                if (a1 <= ptdj)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('抱歉！锁仓金额不能小于当前等级！');</script>", false);
                    return;
                }
                else
                {
                    a1 = a1 - ptdj;
                }
            }
           
        
        
        if (a1 <= 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('锁仓金额不能小于等于0！');</script>", false);
            ft.Text = Math.Abs(a1).ToString();
            return;
        }
        if (MemberInfoDAL.CheckState(Session["Member"].ToString()))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script> alert('" + GetTran("000000", "会员账户已冻结，不能复投支付!") + "'); </script>");

            return;
        }
        

        num = Common.GetnowPrice();//获取当前石斛积分价格


        int a3 =Convert.ToInt32(a1 / num);//复投的金额

       decimal num1 = Common.GetFTJBbyMoney(Session["Member"].ToString(),a3);
       OrderFinalModel ofm = new OrderFinalModel();
       MemberInfoModel mi = AddUserInfo();

       
       ofm.SendWay = 1;
       ofm.Number = mi.Number;
       ofm.Placement = mi.Placement;
       ofm.Direct = mi.Direct;
       ofm.ExpectNum = mi.ExpectNum;
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
       //ofm.IsAgain = 1;

       ofm.TotalMoney = a3;
       ofm.TotalPv = a1;
       ofm.OrderType = mi.OrderType;
       ofm.OrderExpect = mi.ExpectNum;
       ofm.StandardcurrencyMoney = ofm.TotalMoney;
       ofm.PaymentMoney = ofm.TotalMoney;
       ofm.OrderDate = DateTime.UtcNow;
       ofm.RemittancesId = "";
       ofm.ElectronicaccountId = "";

       ofm.InvestJB = a3;//投资石斛积分数量
       ofm.PriceJB = num;//当前石斛积分市价

       
       ofm.ConCity.Country = "";
       ofm.ConCity.Province = "";
       ofm.ConCity.City = "";
       ofm.ConCity.Xian = "";
       ofm.ConAddress = "";

       ofm.CCPCCode = "" ;



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
           ClientScript.RegisterStartupScript(this.GetType(), "", "<script>$('#tiaoz').show();document.getElementById('tiaoz').href = '../payserver/chosepaysjpay.aspx?blif=" + EncryKey.GetEncryptstr(ofm.OrderID.ToString(), 1, 1) + "'; ;alertt('订单已生成，请及时支付！');</script>", false);
           //return;
       }
        }
        catch (OverflowException)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('err:转化的不是一个Decimal型数据');</script>", false);
        }
        catch (FormatException)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('err:格式错误');</script>", false);
        }
        catch (ArgumentNullException)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('err:不能为空');</script>", false);

        }
    }


    public MemberInfoModel AddUserInfo()
    {
        MemberInfoModel memberInfoModel = new MemberInfoModel();
        memberInfoModel.Number = Session["Member"].ToString();
        memberInfoModel.Placement = "";
        memberInfoModel.Direct ="";
        memberInfoModel.ExpectNum = BLL.CommonClass.CommonDataBLL.getMaxqishu();
        memberInfoModel.OrderID = "";
        memberInfoModel.StoreID = "8888888888";
        memberInfoModel.Name = "";
        memberInfoModel.PetName = "";
        memberInfoModel.OrderType = 22;
        if (Jackpot.Text == "无" || Jackpot.Text == "0")
        {
            memberInfoModel.OrderType = 22;
        }
        else
        {
            memberInfoModel.OrderType = 23;
        }
        
        memberInfoModel.LoginPass ="";
        memberInfoModel.AdvPass = "";
        memberInfoModel.LevelInt = 0;

        if (Jackpot.Text == "500")
        { memberInfoModel.LevelInt = 1;//会员级别 
        }
        if (Jackpot.Text == "1000")
        { memberInfoModel.LevelInt = 2;//会员级别 
        }
        if (Jackpot.Text == "3000")
        { memberInfoModel.LevelInt = 3;//会员级别 
        }
        if (Jackpot.Text == "5000")
        { memberInfoModel.LevelInt = 4;//会员级别 
        }
        if (Jackpot.Text== "无")
        { memberInfoModel.LevelInt = 0;//会员级别 
        }
        
        memberInfoModel.RegisterDate = DateTime.UtcNow;
        memberInfoModel.PaperType.Id = 0;
        memberInfoModel.Sex = 0;
        memberInfoModel.Birthday = Convert.ToDateTime("1990-01-01");
        
        memberInfoModel.Assister = "";
        memberInfoModel.OfficeTele = "";

        memberInfoModel.HomeTele = "";
        memberInfoModel.MobileTele = "";

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
}