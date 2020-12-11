using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;
using BLL.Registration_declarations;
using BLL.CommonClass;
using DAL;

/// <summary>
///OrderDeal 的摘要说明
/// </summary>
public class OrderDeal
{
    public OrderDeal()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    public OrderFinalModel GetDataModel(int mType, int OType, out IList<MemberDetailsModel> choseProList, int sendway)
    {
        OrderFinalModel ofm = new OrderFinalModel();

        RegistermemberBLL registermemberBLL = new RegistermemberBLL();

        MemberInfoModel mim = ((MemberInfoModel)HttpContext.Current.Session["mbreginfo"]);
        ofm.SendWay = sendway;
        ofm.Number = mim.Number;
        ofm.Placement = mim.Placement;
        ofm.Direct = mim.Direct;
        ofm.ExpectNum = CommonDataBLL.getMaxqishu();
        ofm.OrderID = registermemberBLL.GetOrderInfo("add", null);
        ofm.StoreID = mim.StoreID;
        ofm.Name = mim.Name;
        ofm.PetName = mim.PetName;
        ofm.LoginPass = mim.LoginPass;
        ofm.AdvPass = mim.AdvPass;
        ofm.LevelInt = mim.LevelInt;

        ofm.RegisterDate = mim.RegisterDate;
        ofm.Birthday = mim.Birthday;
        ofm.Sex = mim.Sex;
        ofm.HomeTele = mim.HomeTele;
        ofm.OfficeTele = mim.OfficeTele;
        ofm.MobileTele = mim.MobileTele;
        ofm.FaxTele = mim.FaxTele;
        ofm.CPCCode = mim.CPCCode;
        ofm.Address = mim.Address;
        ofm.PostalCode = mim.PostalCode;
        ofm.PaperType.PaperTypeCode = mim.PaperType.PaperTypeCode;
        ofm.PaperNumber = mim.PaperNumber;
        ofm.BankCode = mim.BankCode;
        ofm.BankAddress = mim.BankAddress;
        ofm.BankCard = mim.BankCard;
        ofm.BCPCCode = mim.BCPCCode;
        ofm.BankBook = mim.BankBook;
        ofm.Remark = mim.Remark;
        ofm.ChangeInfo = mim.ChangeInfo;
        ofm.PhotoPath = mim.PhotoPath;
        ofm.Email = mim.Email;
        ofm.IsBatch = mim.IsBatch;
        ofm.Language = mim.Language;
        ofm.OperateIp = mim.OperateIp;
        ofm.OperaterNum = mim.OperaterNum;
        ofm.Answer = mim.Answer;
        ofm.Question = mim.Question;
        ofm.Error = mim.Error;
        ofm.Bankbranchname = mim.Bankbranchname;
        ofm.Flag = mim.Flag;
        ofm.Assister = mim.Assister;
        ofm.District = mim.District;

        //memberorder开始
        DataTable dt11 = GetMoneyAndProMess(mim.Number, mType, OType);
        if (dt11.Rows.Count > 0)
        {
            ofm.TotalMoney = Convert.ToDecimal(MemberOrderAgainBLL.GetBzMoney(mim.StoreID, Convert.ToDouble(dt11.Rows[0]["TotalPriceAll"])));
            ofm.TotalPv = Convert.ToDecimal(dt11.Rows[0]["TotalPvAll"]);
        }
        else
        {
            ofm.TotalMoney = 0;
            ofm.TotalPv = 0;
        }

        ofm.PayExpect = -1;
        ofm.OrderExpect = CommonDataBLL.getMaxqishu();
        ofm.IsAgain = OType;
        ofm.OrderDate = DateTime.Now.ToUniversalTime();
        ofm.DefrayState = 0; //未支付
        ofm.OrderType = OType;
        //   ofm.Type  
        //ofm.OrderType  ofm.CCPCCode  ofm.ConAddress ofm.ConTelPhone ofm.ConMobilPhone  ofm.ConPost
        //ofm.Consignee ofm.ConZipCode ofm.SendWay 不在此处理
        ofm.RemittancesId = "";
        ofm.ElectronicaccountId = "";
        ofm.DefrayType = -1;
        ofm.PayCurrency = 1;
        ofm.PayMoney = 0;
        ofm.StandardCurrency = MemberOrderAgainBLL.GetBzTypeId(mim.StoreID);
        ofm.StandardcurrencyMoney = Convert.ToDecimal(dt11.Rows[0]["TotalPriceAll"]);
        ofm.CarryMoney = 0;
        ofm.IsreceiVables = 0;
        ofm.IsRetail = 0;
        ofm.DeclareMoney = 0;
        ofm.PaymentMoney = 0;

        DataTable dt22 = GetProMess(mim.Number, mType, OType);
        choseProList = AddMemberDetails(dt22);

        if (ofm.SendWay == 0)
        {
            ofm.EnoughProductMoney = Convert.ToDecimal(registermemberBLL.getEnoughProductMoney(choseProList, mim.StoreID));
            double notEnoughmoney = registermemberBLL.CheckMoneyIsEnough(choseProList, mim.StoreID);
            ofm.LackProductMoney = Convert.ToDecimal(registermemberBLL.ChangeNotEnoughMoney(mim.StoreID, notEnoughmoney));
        }
        else
        {
            ofm.EnoughProductMoney = 0;
            ofm.LackProductMoney = ofm.TotalMoney;

        }

        if (dt22.Rows.Count > 0)
        {
            string proNum = "";
            string proId = "";
            string notProList = "";
            for (int i = 0; i < dt22.Rows.Count; i++)
            {
                proNum += dt22.Rows[i]["proNum"].ToString() + ",";
                proId += dt22.Rows[i]["proId"].ToString() + ",";
                notProList += choseProList[i].NotEnoughProduct.ToString() + ",";
            }

            ofm.ProductIDList = proId;
            ofm.QuantityList = proNum;
            ofm.NotEnoughProductList = notProList;
        }
        else
        {
            ofm.ProductIDList = ",";
            ofm.QuantityList = ",";
            ofm.NotEnoughProductList = ",";
        }

        return ofm;

    }

    public OrderFinalModel GetDataModelFx(int mType, int OType, out IList<MemberDetailsModel> choseProList, int sendway)
    {
        OrderFinalModel ofm = new OrderFinalModel();

        MemberInfoModel ofm2 = ((MemberInfoModel)HttpContext.Current.Session["fxMemberModel"]);

        RegistermemberBLL registermemberBLL = new RegistermemberBLL();

        MemberInfoModel mim = new MemberInfoModel();
        ofm.SendWay = sendway;
        mim.Number = ofm2.Number;
        mim.StoreID = ofm2.StoreID;

        ofm.Number = mim.Number;
        ofm.Placement = "";
        ofm.Direct = "";
        ofm.ExpectNum = CommonDataBLL.getMaxqishu();
        ofm.OrderID = registermemberBLL.GetOrderInfo("add", null);
        ofm.StoreID = ofm2.StoreID;
        ofm.Name = "";
        ofm.PetName = "";
        ofm.LoginPass = "";
        ofm.AdvPass = "";
        ofm.LevelInt = 1;

        ofm.RegisterDate = DateTime.UtcNow;
        ofm.Birthday = DateTime.UtcNow;
        ofm.Sex = 0;
        ofm.HomeQuhao = "";
        ofm.HomeTele = "";
        ofm.OfficeQuhao = "";
        ofm.OfficeTele = "";
        ofm.OfficeFjh = "";
        ofm.MobileTele = "";
        ofm.FaxQuhao = "";
        ofm.FaxTele = "";
        ofm.FaxFjh = "";
        ofm.CPCCode = "";
        ofm.Address = "";
        ofm.PostalCode = "";
        ofm.PaperType.PaperTypeCode = "";
        ofm.PaperNumber = "";
        ofm.BankCode = "";
        ofm.BankAddress = "";
        ofm.BankCard = "";
        ofm.BCPCCode = "";
        ofm.BankBook = "";
        ofm.Remark = "";
        ofm.ChangeInfo = "";
        ofm.Healthy = 1;
        ofm.PhotoPath = "";
        ofm.PhotoW = 0;
        ofm.PhotoH = 0;
        ofm.Email = "";
        ofm.IsBatch = 0;
        ofm.Language = 1;
        ofm.OperateIp = "";
        ofm.OperaterNum = "";
        ofm.District = 1;
        ofm.Answer = "";
        ofm.Question = "";
        ofm.Error = "";
        ofm.Bankbranchname = "";

        //memberorder开始
        DataTable dt11 = GetMoneyAndProMess(mim.Number, mType, OType);
        if (dt11.Rows.Count > 0)
        {
            ofm.TotalMoney = Convert.ToDecimal(dt11.Rows[0]["TotalPvAll"]);
            ofm.TotalPv = Convert.ToDecimal(dt11.Rows[0]["TotalPvAll"]);
        }
        else
        {
            ofm.TotalMoney = 0;
            ofm.TotalPv = 0;
        }

        ofm.PayExpect = -1;
        ofm.OrderExpect = CommonDataBLL.getMaxqishu();
        ofm.IsAgain = OType;
        ofm.OrderDate = DateTime.UtcNow;
        ofm.DefrayState = 0; //未支付

        ofm.RemittancesId = "";
        ofm.ElectronicaccountId = "";
        ofm.DefrayType = -1;
        ofm.PayCurrency = 1;
        ofm.PayMoney = 0;
        ofm.StandardCurrency = 1;
        ofm.StandardcurrencyMoney = Convert.ToDecimal(dt11.Rows[0]["TotalPriceAll"]);
        ofm.CarryMoney = 0;
        ofm.IsreceiVables = 0;
        ofm.IsRetail = 0;
        ofm.DeclareMoney = 0;
        ofm.PaymentMoney = 0;

        DataTable dt22 = GetProMess(mim.Number, mType, OType);
        choseProList = AddMemberDetails(dt22);
        if (ofm.SendWay == 0)
        {
            ofm.EnoughProductMoney = Convert.ToDecimal(registermemberBLL.getEnoughProductMoney(choseProList, mim.StoreID));
            double notEnoughmoney = registermemberBLL.CheckMoneyIsEnough(choseProList, mim.StoreID);
            ofm.LackProductMoney = Convert.ToDecimal(registermemberBLL.ChangeNotEnoughMoney(mim.StoreID, notEnoughmoney));
        }
        else
        {
            ofm.EnoughProductMoney = 0;
            ofm.LackProductMoney = ofm.TotalMoney;
        }

        if (dt22.Rows.Count > 0)
        {
            string proNum = "";
            string proId = "";
            string notProList = "";
            for (int i = 0; i < dt22.Rows.Count; i++)
            {
                proNum += dt22.Rows[i]["proNum"].ToString() + ",";
                proId += dt22.Rows[i]["proId"].ToString() + ",";
                notProList += choseProList[i].NotEnoughProduct.ToString() + ",";
            }

            ofm.ProductIDList = proId;
            ofm.QuantityList = proNum;
            ofm.NotEnoughProductList = notProList;
        }
        else
        {
            ofm.ProductIDList = ",";
            ofm.QuantityList = ",";
            ofm.NotEnoughProductList = ",";
        }

        return ofm;

    }

    public OrderFinalModel GetDataModelFx1(int mType, string storeid, out IList<MemberDetailsModel> choseProList, int flag)
    {
        OrderFinalModel ofm = new OrderFinalModel();

        LetUsOrder luo = new LetUsOrder();
        luo.SetVlaue();
        //OrderFinalModel ofm2 = ((OrderFinalModel)HttpContext.Current.Session["fxMemberModel"]);

        RegistermemberBLL registermemberBLL = new RegistermemberBLL();

        MemberInfoModel mim = new MemberInfoModel();

        mim.Number = luo.MemBh;
        mim.StoreID = storeid;
        int OType = luo.OrderType;
        ofm.Number = luo.MemBh;
        ofm.Placement = "";
        ofm.Direct = "";
        ofm.ExpectNum = CommonDataBLL.getMaxqishu();
        ofm.OrderID = ofm.OrderID = new RegistermemberBLL().GetOrderInfo("add", null);
        ofm.StoreID = storeid;
        ofm.Name = "";
        ofm.PetName = "";
        ofm.LoginPass = "";
        ofm.AdvPass = "";
        ofm.LevelInt = 1;

        ofm.RegisterDate = DateTime.Now;
        ofm.Birthday = DateTime.Now;
        ofm.Sex = 0;
        ofm.HomeQuhao = "";
        ofm.HomeTele = "";
        ofm.OfficeQuhao = "";
        ofm.OfficeTele = "";
        ofm.OfficeFjh = "";
        ofm.MobileTele = "";
        ofm.FaxQuhao = "";
        ofm.FaxTele = "";
        ofm.FaxFjh = "";
        ofm.CPCCode = "";
        ofm.Address = "";
        ofm.PostalCode = "";
        ofm.PaperType.PaperTypeCode = "";
        ofm.PaperNumber = "";
        ofm.BankCode = "";
        ofm.BankAddress = "";
        ofm.BankCard = "";
        ofm.BCPCCode = "";
        ofm.BankBook = "";
        ofm.Remark = "";
        ofm.ChangeInfo = "";
        ofm.Healthy = 1;
        ofm.PhotoPath = "";
        ofm.PhotoW = 0;
        ofm.PhotoH = 0;
        ofm.Email = "";
        ofm.IsBatch = 0;
        ofm.Language = 1;
        ofm.OperateIp = "";
        ofm.OperaterNum = "";
        ofm.District = 1;
        ofm.Answer = "";
        ofm.Question = "";
        ofm.Error = "";
        ofm.Bankbranchname = "";
        ofm.Flag = flag;

        //memberorder开始
        DataTable dt11 = GetMoneyAndProMess(mim.Number, mType, OType);
        if (dt11.Rows.Count > 0)
        {
            ofm.TotalMoney = Convert.ToDecimal(MemberOrderAgainBLL.GetBzMoney(mim.StoreID, Convert.ToDouble(dt11.Rows[0]["TotalPriceAll"])));
            ofm.TotalPv = Convert.ToDecimal(dt11.Rows[0]["TotalPvAll"]);
        }
        else
        {
            ofm.TotalMoney = 0;
            ofm.TotalPv = 0;
        }

        ofm.PayExpect = ofm.ExpectNum;
        ofm.OrderExpect = ofm.ExpectNum;
        //ofm.IsAgain = ofm2.IsAgain;
        ofm.OrderDate = DateTime.Now.ToUniversalTime();
        ofm.DefrayState = 0; //ofm2.DefrayState;

        ofm.OrderType = OType;

        //   ofm.Type  
        //ofm.OrderType  ofm.CCPCCode  ofm.ConAddress ofm.ConTelPhone ofm.ConMobilPhone  ofm.ConPost
        //ofm.Consignee ofm.ConZipCode ofm.SendWay 不在此处理
        ofm.RemittancesId = "";
        ofm.ElectronicaccountId = "";
        ofm.DefrayType = -1;//ofm2.DefrayType;
        ofm.PayCurrency = -1;
        ofm.PayMoney = 0;
        ofm.StandardCurrency = MemberOrderAgainBLL.GetBzTypeId(mim.StoreID);
        ofm.StandardcurrencyMoney = Convert.ToDecimal(dt11.Rows[0]["TotalPriceAll"]);
        ofm.CarryMoney = 0;
        ofm.IsreceiVables = 0;
        ofm.IsRetail = 0;
        ofm.DeclareMoney = 0;
        ofm.PaymentMoney = 0;

        DataTable dt22 = GetProMess(mim.Number, mType, OType);
        choseProList = AddMemberDetails(dt22);
        if (ofm.SendWay == 0)
        {
            ofm.EnoughProductMoney = Convert.ToDecimal(registermemberBLL.getEnoughProductMoney(choseProList, mim.StoreID));
            double notEnoughmoney = registermemberBLL.CheckMoneyIsEnough(choseProList, mim.StoreID);
            ofm.LackProductMoney = Convert.ToDecimal(registermemberBLL.ChangeNotEnoughMoney(mim.StoreID, notEnoughmoney));
        }
        else
        {
            ofm.EnoughProductMoney = 0;
            ofm.LackProductMoney = ofm.TotalMoney;
        }
        if (dt22.Rows.Count > 0)
        {
            string proNum = "";
            string proId = "";
            string notProList = "";
            for (int i = 0; i < dt22.Rows.Count; i++)
            {
                proNum += dt22.Rows[i]["proNum"].ToString() + ",";
                proId += dt22.Rows[i]["proId"].ToString() + ",";
                notProList += choseProList[i].NotEnoughProduct.ToString() + ",";
            }

            ofm.ProductIDList = proId;
            ofm.QuantityList = proNum;
            ofm.NotEnoughProductList = notProList;
        }
        else
        {
            ofm.ProductIDList = ",";
            ofm.QuantityList = ",";
            ofm.NotEnoughProductList = ",";
        }

        return ofm;

    }

    public static DataTable GetMoneyAndProMess(string bh, int mType, int OType)
    {
        string sql = "select isnull(sum(PreferentialPrice*proNum),0.00) as TotalPriceAll,isnull(sum(PreferentialPV*proNum),0.00) as TotalPvAll from MemShopCart,Product where MemShopCart.proId=Product.productId and memBh=@number and mType=@mType1 and odType=@oType";
        SqlParameter[] sp = new SqlParameter[] {       
            new SqlParameter("@number",bh),
             
             new SqlParameter("@mType1",SqlDbType.Int),

             new SqlParameter("@oType",SqlDbType.Int)

        };

        sp[1].Value = mType;
        sp[2].Value = OType;

        return DBHelper.ExecuteDataTable(sql, sp, CommandType.Text);
    }

    public DataTable GetProMess(string bh, int mType, int OType)
    {
        string sql = "select proNum,proId from MemShopCart where  memBh=@number and mType=@mType1 and odType=@oType";
        SqlParameter[] sp = new SqlParameter[] {       
            new SqlParameter("@number",bh),
             
             new SqlParameter("@mType1",SqlDbType.Int),

             new SqlParameter("@oType",SqlDbType.Int)

        };

        sp[1].Value = mType;
        sp[2].Value = OType;

        return DBHelper.ExecuteDataTable(sql, sp, CommandType.Text);
    }

    public IList<MemberDetailsModel> AddMemberDetails(DataTable dt5)
    {
        double totalMoney = 0.00;
        double totalPv = 0.00;


        //获取用户选择商品id集合
        List<MemberDetailsModel> details = new List<MemberDetailsModel>();


        //用户选择商品到总商品集合里去匹配
        for (int i = 0; i < dt5.Rows.Count; i++)
        {


            int productid = Convert.ToInt32(dt5.Rows[i]["proid"]);
            ProductModel productuse = ProductDAL.GetProductById(productid); //获取对象


            string productName = productuse.ProductName.ToString();



            //读取用户输入的数量
            double pdtNum = Convert.ToDouble(dt5.Rows[i]["proNum"]);


            // 读取用户输入的订货信息
            if (pdtNum > 0)
            {
                //保存订单信息和汇总信息
                double price = Convert.ToDouble(productuse.PreferentialPrice);
                double pv = Convert.ToDouble(productuse.PreferentialPV);
                totalMoney += price * pdtNum;
                totalPv += pv * pdtNum;

                MemberDetailsModel md = new MemberDetailsModel();
                md.Price = (decimal)price;
                md.Pv = (decimal)pv;
                md.Quantity = (int)pdtNum;
                md.NotEnoughProduct = (int)pdtNum;
                md.ProductId = (int)productuse.ProductID;
                md.ProductName = productName;

                details.Add(md);




            }
        }

        return details;
    }
}