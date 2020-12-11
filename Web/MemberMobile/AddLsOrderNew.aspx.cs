using System;
using Model;
using System.Collections;
using BLL.Registration_declarations;
using BLL.CommonClass;
using BLL.other.Member;
using DAL;
using System.Web.Services;
using System.Data.SqlClient;
using BLL.other.Company;
using System.Collections.Generic;
using System.Data;

public partial class MemberMobile_AddLsOrderNew : BLL.TranslationBase
{
    protected string type = "", url = "";
    protected LetUsOrder luo = new LetUsOrder();
    protected MemberInfoModel mim = null;
    protected int bzCurrency = 0;
    protected double i;
    RegistermemberBLL registermemberBLL = new RegistermemberBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        type = Request.QueryString["type"] == "" ? "" : Request.QueryString["type"];
        url = Request.QueryString["url"] == "" ? "" : Request.QueryString["url"];
        bzCurrency = CommonDataBLL.GetStandard();
        //i = (AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString())));
        Permissions.ThreeRedirect(Page, "../member/" + Permissions.redirUrl);
        luo.SetVlaue();
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxMemShopCart));
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));

        if (!IsPostBack)
        {
            GetTotalPrice();

            var member = Session["Member"];
            if (member != null)
            {
                var cinfo = MemberInfoModifyBll.getconsigneeInfo(member.ToString(),true); 
                if (cinfo != null)
                {
                    //lbConsignee.Text = cinfo.Consignee;
                    //lbMoblieTele.Text = cinfo.MoblieTele;
                    //labName.Text = "收货人：";
                    var city = CommonDataDAL.GetCPCCode(cinfo.CPCCode);
                    if (city != null)
                    {
                        //lbaddress.Text = city.Province + city.City + city.Xian + cinfo.Address;
                    }
                }
                else
                {
                    //labName.Text = "+新建收货地址";
                }
            }
            labrealparice.Text = (Convert.ToDouble(labCarryMoney.Text) + Convert.ToDouble(lbtotalPrice.Text)).ToString("0.00");
            if (labCarryMoney.Text == "0.00"|| labCarryMoney.Text == "0")
            {
                labCarryMoney.Text = "包邮";
            }
            else
            {
                labCarryMoney.Text = "0.00";
            }
        }
    }

    protected void StartRecord_click(object sender, EventArgs e)
    {
        ConsigneeInfo cinfo = null;
        var member = Session["Member"];
        if (member != null)
        {
            cinfo = MemberInfoModifyBll.getconsigneeInfo(member.ToString(), true);
            if (cinfo == null)
            {
                ScriptHelper.SetAlert(Page, "请先选择收货地址！", "PhoneSettings/SetConAddress.aspx?type="+type+"&&url=AddLsOrder");
                return;
            }
        }
        else
        {
            Response.Redirect("~/MemberMobile/Index.aspx");
            return;
        }
        string count = DBHelper.ExecuteScalar("select count(*) from MemShopCart where memBh='" + luo.MemBh + "' and mType=" + Session["UserType"].ToString() + " and odType=" + luo.OrderType).ToString();
        if (count == "0" && Session["UserType"].ToString() != "1")
        {
            ScriptHelper.SetAlert(Page, GetTran("007430", "您至少要选择一种产品") + "!", "ShopingList.aspx");
            return;
        }
        else
        {

            IList<MemberDetailsModel> choseProList = new List<MemberDetailsModel>();

            OrderDeal od = new OrderDeal();

            OrderFinalModel ofm = new OrderFinalModel();

            ofm.SendWay = 1;//收货途径  会员收货
            if (Session["EditOrderID"] != null)
            {
                OrderFinalModel model = new OrderFinalModel();
                ofm = od.GetDataModelFx(Convert.ToInt32(Session["UserType"]), luo.OrderType, out choseProList, ofm.SendWay);
                ofm.Assister = "";
            }
            else
            {
                if (luo.OrderType == 21 || luo.OrderType == 11 || luo.OrderType == 31)
                {
                    ofm = od.GetDataModel(Convert.ToInt32(Session["UserType"]), luo.OrderType, out choseProList, ofm.SendWay);

                    if (new RegistermemberBLL().CheckNumberTwice(ofm.Number) != null)
                    {
                        ScriptHelper.SetAlert(Page, GetTran("007432", "会员编号已存在") + "！");
                        AgainTime.Value = "0";
                        DAL.DBHelper.ExecuteNonQuery("delete from MemShopCart where memBh='" + luo.MemBh + "' and mType=" + Session["UserType"].ToString());
                        return;
                    }

                    string placement = new RegistermemberBLL().GetHavePlacedOrDriect(ofm.Number, "", ofm.Placement, ofm.Direct);
                    if (placement != null)
                    {
                        ScriptHelper.SetAlert(Page, placement);
                        AgainTime.Value = "0";
                        DAL.DBHelper.ExecuteNonQuery("delete from MemShopCart where memBh='" + luo.MemBh + "' and mType=" + Session["UserType"].ToString());
                        return;
                    }

                    if (ofm.Placement != "8888888888")
                    {
                        if (DBHelper.ExecuteScalar("select count(0) from memberinfo where placement='" + ofm.Placement + "' and District=" + ofm.District + "").ToString() != "0")
                        {
                            ScriptHelper.SetAlert(Page, GetTran("007433", "安置人所选区位已有人安置") + "！");
                            AgainTime.Value = "0";
                            DAL.DBHelper.ExecuteNonQuery("delete from MemShopCart where memBh='" + luo.MemBh + "' and mType=" + Session["UserType"].ToString());
                            return;
                        }
                    }
                }
                else
                {
                   
                    ofm = od.GetDataModelFx(Convert.ToInt32(Session["UserType"]), luo.OrderType, out choseProList, ofm.SendWay);
                    if (!MemberInfoDAL.IsMemberExist(ofm.Number))
                    {
                        ScriptHelper.SetAlert(Page, GetTran("000725", "会员编号不存在") + "！");
                        AgainTime.Value = "0";
                        return;
                    }
                    ofm.Assister = "";
                }
                if (luo.OrderType == 21 || luo.OrderType == 11)
                {
                    if (Convert.ToDouble(ofm.TotalMoney) < SetParametersBLL.GetMemOrderLineOrderBaseLine())
                    {
                        ScriptHelper.SetAlert(Page, GetTran("000000", "会员注册金额不能低于") + SetParametersBLL.GetMemOrderLineOrderBaseLine().ToString("f2") + "！");
                        AgainTime.Value = "0";
                        return;
                    }
                }
            }

            ofm.StoreID = "8888888888";
            ofm.Type = 2;// 运货方式 邮寄 //Convert.ToInt32(this.ddth.SelectedValue);


            //if (lblOdType.Text == GetTran("004008", "注册报单"))
            //{
            //    ofm.IsAgain = 0;
            //}
            //else
            //{
            ofm.IsAgain = 1;
            //}
            ofm.OrderType = 12;

            double yfStr = 0;

            //地址
           
            if (cinfo!=null)
            {
                ofm.CCPCCode = cinfo.CPCCode;
                ofm.ConTelPhone = cinfo.MoblieTele;
                ofm.ConMobilPhone = cinfo.MoblieTele;
                ofm.ConPost = "";
                ofm.Consignee = Encryption.Encryption.GetEncryptionName(cinfo.Consignee);
                ofm.ConZipCode = cinfo.ConZipCode;
                ofm.ConAddress = Encryption.Encryption.GetEncryptionAddress(cinfo.Address);
            }
            //ofm.ConCity.Country = this.CountryCity2.Country;
            //ofm.ConCity.Province = this.CountryCity2.Province;
            //ofm.ConCity.City = this.CountryCity2.City;
            //ofm.ConCity.Xian = this.CountryCity2.Xian;
            //ofm.ConAddress = Encryption.Encryption.GetEncryptionAddress(this.Txtdz.Text);
            //ofm.CCPCCode = DAL.CommonDataDAL.GetCPCCode(CountryCity2.Country, CountryCity2.Province, CountryCity2.City, CountryCity2.Xian);

            yfStr = 0;

            //ofm.ConTelPhone = txtOtherPhone.Text.Trim();
            //ofm.ConMobilPhone = txtOtherPhone.Text.Trim(); //Txtyddh.Text.Trim();
            //ofm.CarryMoney = Convert.ToDecimal(yfStr);
            //ofm.ConPost = "";
            //ofm.Consignee = Encryption.Encryption.GetEncryptionName(txtConName.Text.Trim());
            //ofm.ConZipCode = txtPostCode.Text;

            //ofm.Number = txtMemBh.Text;

            //产品总费用、年费、运费、应付总金额
            double pdtMoney = 0; //double.Parse(this.ltPrice.Text);

            double CarriageMoney = 0;//运费

            ofm.CarryMoney = decimal.Parse("0.00");
            var dayPrice = CommonDataBLL.GetMaxDayPrice();
            decimal yfje = Convert.ToDecimal(ofm.TotalMoney / Convert.ToDecimal(dayPrice));

            ofm.TotalMoney = yfje + Convert.ToDecimal(CarriageMoney);
            //运费类型
            //if (ddth.SelectedValue == "1")//自提
            //{
            //    ofm.CarryMoney = decimal.Parse("0.00");
            //    this.txtYunfei.Text = ofm.CarryMoney.ToString();
            //}
            //else
            //{
            //    this.txtYunfei.Text = ofm.CarryMoney.ToString();
            //    ofm.TotalMoney = Convert.ToDecimal(ofm.TotalMoney) + Convert.ToDecimal(CarriageMoney);//加运费
            //}
            
            ofm.OrderExpect = CommonDataBLL.getMaxqishu();
            ofm.StandardcurrencyMoney = yfje;
            ofm.TotalPv = 0;
            ofm.PaymentMoney = yfje;
            ofm.LevelInt = 1;
            ofm.StoreID = "8888888888";


            ofm.InvestJB = yfje;//投资石斛积分币数量
            ofm.PriceJB = Convert.ToDecimal(dayPrice);//石斛积分当前市价

            ofm.OrderID = registermemberBLL.GetOrderInfo("add", null);

            if (Session["EditOrderID"] != null)
            {
                int zhifuZt = Convert.ToInt32(DBHelper.ExecuteScalar("select defraystate from memberorder where orderid='" + Session["EditOrderID"] + "'"));

                if (zhifuZt == 1)
                {
                    ScriptHelper.SetAlert(Page, "该单已支付！不能修改！");
                    return;
                }

                SqlConnection conn = new SqlConnection(DBHelper.connString);
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                ofm.OrderID = Session["EditOrderID"].ToString();
                new AddOrderDataDAL().Del_Horder(Session["EditOrderID"].ToString(), tran);

                Boolean flag = new DAL.AddOrderDataDAL().AddFinalOrderNoInfo(ofm, tran);

                if (flag)
                {
                    //p_content.Visible = false;
                    Session.Remove("mbreginfo");
                    Session.Remove("fxMemberModel");
                    Session.Remove("LUOrder");
                    Session.Remove("OrderType");
                    Session.Remove("EditOrderID");
                    Session.Remove("MemberUpgradeStore");
                    Session["MemberInfo_NP"] = ofm.Number + "," + ofm.Number; //储存会员的编号

                    tran.Commit();
                    conn.Close();
                    conn.Dispose();

                    DAL.DBHelper.ExecuteNonQuery("delete from MemShopCart where memBh='" + ofm.Number + "' and mType=" + Session["UserType"].ToString());//订单提交成功后，删除购物车

                    if (Session["UserType"].ToString() == "1")
                    {
                        int val = AddOrderDataDAL.OrderPayment(ofm.StoreID, ofm.OrderID, ofm.OperateIp, 1, 1, 1, "管理员", "", 4, -1, 1, 1, "", 0, "");
                        if (val == 0)
                        {
                            ClientScript.RegisterStartupScript(GetType(), "msg", "<script>alert('" + GetTran("000222", "修改成功") + "');location.href='../company/BrowseMemberOrders.aspx';</script>", false);
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(GetType(), "msg", "<script>alert('" + GetTran("007435", "修改成功自动支付失败") + "！');location.href='../company/BrowseMemberOrders.aspx';</script>", false);
                        }

                    }
                    else if (Session["UserType"].ToString() == "2")
                    {
                        if (ofm.IsAgain == 0)
                        {
                            //ClientScript.RegisterStartupScript(GetType(), "msg", "<script>var formobj=document.createElement('form');"
                            //    + "formobj.action='../payserver/chosepay.aspx?blif=" + EncryKey.GetEncryptstr(ofm.OrderID, 1, 1) + "';" +
                            //    "formobj.method='post';formobj.target='_blank';document.body.appendChild(formobj); formobj.submit();location.href='../store/browsememberorders.aspx';</script>", false);
                            //ScriptManager.RegisterStartupScript(this, GetType(), "mag", "var formobj=document.createElement('form');"
                            //    + "formobj.action='../payserver/chosepaysj.aspx?blif=" + EncryKey.GetEncryptstr(ofm.OrderID, 1, 1) + "';" +
                            //    "formobj.method='post';formobj.target='_blank';document.body.appendChild(formobj); formobj.submit();location.href='../store/browsememberorders.aspx';", true);
                        }
                        else
                        {
                            //ClientScript.RegisterStartupScript(GetType(), "msg", "<script>var formobj=document.createElement('form');"
                            //    + "formobj.action='../payserver/chosepay.aspx?blif=" + EncryKey.GetEncryptstr(ofm.OrderID, 1, 1) + "';" +
                            //    "formobj.method='post';formobj.target='_blank';document.body.appendChild(formobj); formobj.submit();location.href='../store/viewfuxiao.aspx';</script>", false);
                            //ScriptManager.RegisterStartupScript(this, GetType(), "mag", "var formobj=document.createElement('form');"
                            //    + "formobj.action='../payserver/chosepaysj.aspx?blif=" + EncryKey.GetEncryptstr(ofm.OrderID, 1, 1) + "';" +
                            //    "formobj.method='post';formobj.target='_blank';document.body.appendChild(formobj); formobj.submit();location.href='../store/browsememberorders.aspx';", true);
                        }
                    }
                    else
                    {
                        if (ofm.IsAgain == 0)
                        {
                            //ClientScript.RegisterStartupScript(GetType(), "msg", "<script>var formobj=document.createElement('form');"
                            //    + "formobj.action='../payserver/chosepay.aspx?blif=" + EncryKey.GetEncryptstr(ofm.OrderID, 1, 1) + "';" +
                            //    "formobj.method='post';formobj.target='_blank';document.body.appendChild(formobj); formobj.submit();location.href='../member/browsememberorders.aspx';</script>", false);
                            //ScriptManager.RegisterStartupScript(this, GetType(), "mag", "var formobj=document.createElement('form');"
                            //    + "formobj.action='../payserver/chosepaysj.aspx?blif=" + EncryKey.GetEncryptstr(ofm.OrderID, 1, 1) + "';" +
                            //    "formobj.method='post';formobj.target='_blank';document.body.appendChild(formobj); formobj.submit();location.href='../member/browsememberorders.aspx';", true);

                        }
                        else
                        {
                            //ClientScript.RegisterStartupScript(GetType(), "msg", "<script>var formobj=document.createElement('form');"
                            //    + "formobj.action='../payserver/chosepay.aspx?blif=" + EncryKey.GetEncryptstr(ofm.OrderID, 1, 1) + "';" +
                            //    "formobj.method='post';formobj.target='_blank';document.body.appendChild(formobj); formobj.submit();location.href='../member/memberorder.aspx';</script>", false);
                            //ScriptManager.RegisterStartupScript(this, GetType(), "mag", "var formobj=document.createElement('form');"
                            //    + "formobj.action='../payserver/chosepaysj.aspx?blif=" + EncryKey.GetEncryptstr(ofm.OrderID, 1, 1) + "';" +
                            //    "formobj.method='post';formobj.target='_blank';document.body.appendChild(formobj); formobj.submit();location.href='../member/browsememberorders.aspx';", true);
                            Response.Redirect("../payserver/chosepaysjpay.aspx?blif=" + EncryKey.GetEncryptstr(ofm.OrderID, 1, 1) + "");
                        }
                    }
                }
                else
                {
                    tran.Rollback();
                    conn.Close();
                    conn.Dispose();
                    ScriptHelper.SetAlert(Page, GetTran("000225", "修改失败"));
                }
            }
            else
            {
                Boolean flag = new DAL.AddOrderDataDAL().AddFinalOrder(ofm);
                
                if (flag)
                {
                    
                    //p_content.Visible = false;
                    Session.Remove("mbreginfo");
                    Session.Remove("fxMemberModel");
                    Session.Remove("LUOrder");
                    Session.Remove("OrderType");
                    Session.Remove("EditOrderID");
                    Session.Remove("MemberUpgradeStore");
                    Session["MemberInfo_NP"] = ofm.Number + "," + ofm.Number; //储存会员的编号

                    DAL.DBHelper.ExecuteNonQuery("delete from MemShopCart where memBh='" + luo.MemBh + "' and mType=" + Session["UserType"].ToString());//订单提交成功后，删除购物车

                    if (Session["UserType"].ToString() == "1")
                    {
                        int val = AddOrderDataDAL.OrderPayment(ofm.StoreID, ofm.OrderID, ofm.OperateIp, 1, 1, 1, "管理员", "", 5, -1, 1, 1, "", 0, "");
                        if (val == 0)
                        {
                            PublicClass.SendMsg(1, ofm.OrderID, "");
                            ClientScript.RegisterStartupScript(GetType(), "msg", "<script>alert('" + GetTran("000000", "购买成功") + "');location.href='../company/BrowseMemberOrders.aspx';</script>", false);
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(GetType(), "msg", "<script>alert('" + GetTran("000000", "报单成功，支付失败，店铺账户余额不足") + "！');location.href='../company/BrowseMemberOrders.aspx';</script>", false);
                        }
                    }
                    else if (Session["UserType"].ToString() == "2")
                    {
                        if (ofm.IsAgain == 0)
                        {
                            //Response.Redirect("../payserver/chosepaysj.aspx?blif=" + EncryKey.GetEncryptstr(ofm.OrderID, 1, 1), true);
                            Response.Redirect("../payserver/chosepaysjpay.aspx?blif=" + EncryKey.GetEncryptstr(ofm.OrderID, 1, 1) + "");
                        }
                        else
                        {
                            Response.Redirect("../payserver/chosepaysjpay.aspx?blif=" + EncryKey.GetEncryptstr(ofm.OrderID, 1, 1) + "");
                            //Response.Redirect("../payserver/chosepaysj.aspx?blif=" + EncryKey.GetEncryptstr(ofm.OrderID, 1, 1), true);
                            //ClientScript.RegisterStartupScript(GetType(), "msg", "<script>var formobj=document.createElement('form');"
                            //    + "formobj.action='../payserver/chosepaysj.aspx?blif=" + EncryKey.GetEncryptstr(ofm.OrderID, 1, 1) + "';</script>" , false);//+
                            // "formobj.method='post';formobj.target='_blank';document.body.appendChild(formobj); formobj.submit();location.href='../store/viewfuxiao.aspx';"
                        }
                    }
                    else
                    {
                        if (ofm.IsAgain == 0)
                        {
                            Response.Redirect("../payserver/chosepaysjpay.aspx?blif=" + EncryKey.GetEncryptstr(ofm.OrderID, 1, 1) + "");
                            //Response.Redirect("../payserver/chosepaysj.aspx?blif=" + EncryKey.GetEncryptstr(ofm.OrderID, 1, 1), true);
                            //ClientScript.RegisterStartupScript(GetType(), "msg", "<script>var formobj=document.createElement('form');"
                            //    + "formobj.action='../payserver/chosepaysj.aspx?blif=" + EncryKey.GetEncryptstr(ofm.OrderID, 1, 1) + "';" +
                            //    "formobj.method='post';formobj.target='_blank';document.body.appendChild(formobj); formobj.submit();location.href='../membermobile/browsememberorders.aspx';</script>", false);
                        }
                        else
                        {
                            Response.Redirect("../payserver/chosepaysjpay.aspx?blif=" + EncryKey.GetEncryptstr(ofm.OrderID, 1, 1) + "");
                            //Response.Redirect("../payserver/chosepaysj.aspx?blif=" + EncryKey.GetEncryptstr(ofm.OrderID, 1, 1), true);
                            //ClientScript.RegisterStartupScript(GetType(), "msg", "<script>var formobj=document.createElement('form');"
                            //    + "formobj.action='../payserver/chosepaysj.aspx?blif=" + EncryKey.GetEncryptstr(ofm.OrderID, 1, 1) + "';" +
                            //    "formobj.method='post';formobj.target='_blank';document.body.appendChild(formobj); formobj.submit();location.href='../membermobile/memberorder.aspx';</script>", false);
                        }
                    }

                }
                else
                {
                    ScriptHelper.SetAlert(Page, GetTran("001557", "报单失败！"));
                }
            }
        }
    }

    private void GetTotalPrice()
    {
        var usertype = 0;
        if (Session["UserType"] != null)
        {
            usertype = Convert.ToInt32(Session["UserType"].ToString());
        }
        else
        {
            usertype = 3;
        }

        string sql = @"select sum(PreferentialPrice*proNum) as TotalPriceAll,
sum(proNum) as totalNum from MemShopCart m  
left join Product p on m.proId=p.productId 
where memBh=@memBh and mType=@mType and odType=@odType";
        var para = new SqlParameter[]
             {
                    new SqlParameter("@memBh",SqlDbType.VarChar),
                    new SqlParameter("@mType",SqlDbType.Int),
                    new SqlParameter("@odType",SqlDbType.Int),
             };
        para[0].Value = luo.MemBh;
        para[1].Value = usertype;
        para[2].Value = luo.OrderType;

        try
        {
            var dt =DBHelper.ExecuteDataTable(sql, para, CommandType.Text);
            foreach (DataRow item in dt.Rows)
            {
                lbtotalPrice.Text = item["TotalPriceAll"].ToString();
            }
        }
        catch (Exception ex)
        {
            
        }
    }

}