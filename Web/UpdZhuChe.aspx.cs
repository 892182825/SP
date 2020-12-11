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
using DAL.Other;


public partial class Store_UpdZhuChe : BLL.TranslationBase
{
    RegistermemberBLL registermemberBLL = new RegistermemberBLL();

    public IList<MemberDetailsModel> AddMemberDetails()
    {
        double totalMoney = 0.00;
        double totalPv = 0.00;

        ViewState["StateCount"] = false;//声明一个状态，判断选择的产品数量是否超出库存
        //获取用户选择商品id集合
        List<MemberDetailsModel> details = new List<MemberDetailsModel>();
        //获取用户提交的选中商品id串
        string pids = hidpids.Value;

        string[] orpids = pids.Split(',');
        if (orpids.Length > 1)
        {
            //获取商品集合
            List<ProductModel> productModelList = registermemberBLL.GetProductModelList();

            //用户选择商品到总商品集合里去匹配
            for (int i = 1; i < orpids.Length; i++)
            {
                int productid = Convert.ToInt32(orpids[i].Substring(1));
                ProductModel productuse = ProductDAL.GetProductById(productid); //获取对象

                string productName = productuse.ProductName.ToString();
                string numStr = Request[orpids[i]];

                if (numStr == null)
                {
                    numStr = "0";
                }
                //读取用户输入的数量
                double pdtNum;
                if (numStr != "")
                {
                    try
                    {
                        pdtNum = Convert.ToInt32(numStr);
                    }
                    catch
                    {
                        return null;
                    }
                }
                else
                {
                    pdtNum = 0;
                }

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
                    md.ProductId = (int)productuse.ProductID;
                    md.ProductName = productName;

                    details.Add(md);

                    //判断不可销售的产品是否超出库存
                    if (MemberOrderAgainBLL.GetIsSellByProId(Convert.ToInt32(productuse.ProductID)) == "1")
                    {
                        int shuliang = md.Quantity;
                        DataTable dtdetail = MemberOrderAgainBLL.GetMemberDetailsByOrderID(Request.QueryString["OrderID"].ToString());
                        for (int j = 0; j < dtdetail.Rows.Count; j++)
                        {
                            if (dtdetail.Rows[j]["productid"].ToString() == productuse.ProductID.ToString())
                            {
                                shuliang = shuliang - Convert.ToInt32(dtdetail.Rows[j]["Quantity"]);
                            }
                        }

                        if (MemberOrderAgainBLL.GetCountByProIdAndStoreId(Convert.ToInt32(productuse.ProductID), Request.QueryString["OrderID"].ToString()) + shuliang < 0)
                        {
                            ViewState["StateCount"] = true;
                        }
                    }

                }
            }
        }
        ViewState["TotalMoney"] = totalMoney;
        ViewState["TotalPv"] = totalPv;
        return details;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ThreeRedirect(Page, "Member/" + Permissions.redirUrl);
        string OrderId = Request.QueryString["OrderID"].ToString();
        string bianhao = Request.QueryString["Number"].ToString();

        double dpm = Convert.ToDouble(DAL.DBHelper.ExecuteScalar("select totalaccountmoney-totalordergoodmoney from storeinfo where storeid='" + GetStoreID() + "'"));
        lblMoney.Text = dpm + "";
        ViewState["dpm"] = dpm;

        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));

        if (!IsPostBack)
        {
            //绑定菜单树
            setProductMenu(GetStoreID(), OrderId);
            //绑定值
            UptTree(OrderId);

            Label1.Text = bianhao;

            AddOrderBLL.BindCurrency_Rate(DropCurrency, GetStoreID());

            new GroupRegisterBLL().GetPaymentType(Ddzf, 1);

            this.Ddzf.Enabled = false;

            ReaderMemberInfo(bianhao, OrderId);
        }
        else
        {
            //保存更新后的值
            savePageDisp();
        }
        Translations();
    }

    private void Translations()
    {
        this.TranControls(this.btnEditProduct, new string[][] { new string[] { "000259", "确 定" } });
        this.TranControls(this.DDLSendType, new string[][] { new string[] { "007103", "公司发货到店铺" }, new string[] { "007104", "公司直接发给会员" } });
    }

    public void ReaderMemberInfo(string bianhao, string OrderId)
    {
        MemberInfoModel mim = new MemberInfoModel();

        DataTable dtmb = RegistermemberBLL.Getcontryofmember(bianhao);

        if (dtmb != null && dtmb.Rows.Count > 0)
        {
            Txtyb.Text = dtmb.Rows[0]["postcode"].ToString();


            mim.PostalCode = dtmb.Rows[0]["postalCode"].ToString();
            mim.HomeTele = dtmb.Rows[0]["homeTele"].ToString();
            mim.MobileTele = dtmb.Rows[0]["MobileTele"].ToString();
            mim.Email = dtmb.Rows[0]["Email"].ToString();
        }

        DataTable dtorder = RegistermemberBLL.Getorderinfoofmember(OrderId);

        if (dtorder != null && dtorder.Rows.Count > 0)
        {
            CountryCity2.SelectCountry(dtorder.Rows[0]["Country"].ToString(), dtorder.Rows[0]["Province"].ToString(), dtorder.Rows[0]["City"].ToString(), dtorder.Rows[0]["Xian"].ToString());
            Txtdz.Text = Encryption.Encryption.GetDecipherAddress(dtorder.Rows[0]["ConAddress"].ToString());
            mim.Address = Encryption.Encryption.GetDecipherAddress(dtorder.Rows[0]["ConAddress"].ToString());
            mim.CPCCode = dtorder.Rows[0]["ccpccode"].ToString();
            ViewState["cpccode"] = dtorder.Rows[0]["ccpccode"].ToString();
            ddth.SelectedValue = dtorder.Rows[0]["sendType"].ToString();

            Ddzf.SelectedValue = dtorder.Rows[0]["DefrayType"].ToString();
            ViewState["DefrayType"] = dtorder.Rows[0]["DefrayType"].ToString();
            if (dtorder.Rows[0]["defrayType"].ToString() == "2")
            {
                this.txtdzbh.Text = dtorder.Rows[0]["electronicAccountId"].ToString();
            }
            DDLSendType.SelectedValue = dtorder.Rows[0]["SendWay"].ToString();
        }

        Session["mim"] = mim;
    }

    public string GetStoreID()
    {
        string storeID = "";
        if (Session["storeId"] == null)
        {
            if (Session["Store"] == null)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('登录超时，请重新登录！');window.top.location.href='index.aspx';<script>");
            }
            else
            {
                storeID = Session["Store"].ToString();
            }
        }
        else
        {
            storeID = Session["storeId"].ToString();
        }

        return storeID;
    }

    /// <summary>
    /// 产生树形菜单
    /// </summary>
    /// <param name="dian"></param>
    public void setProductMenu(string storID, string OrderId)
    {
        ProductTree myTree = new ProductTree();
        this.menuLabel.Text = myTree.getEditMenu10(storID, OrderId).Replace("../", "");
    }

    /// <summary>
    /// 更新树
    /// </summary>
    /// <param name="orderId">订单id</param>
    public void UptTree(string orderId)
    {
        this.hidpids.Value = "";
        AddOrderBLL addOrderBLL = new AddOrderBLL();
        string result = "<script language='javascript'>";
        if (orderId.Length > 12)
        {
            orderId = orderId.Substring(0, 12);
        }
        List<MemberDetailsModel> list = addOrderBLL.GetDetails(orderId);
        foreach (MemberDetailsModel memberDetailsModel in list)
        {
            this.hidpids.Value += ",N" + memberDetailsModel.ProductId;
            result = result + "Form1.N" + memberDetailsModel.ProductId + ".value=" + memberDetailsModel.Quantity;
            result += ";";
        }
        result += "</script>";
        this.txt_jsLable.Text = result;
    }

    /// <summary>
    ///  刷新后保存用户输入的数量
    /// </summary>
    public void savePageDisp()
    {
        string status = null;
        //获取用户提交的选中商品id串
        string pids = hidpids.Value;

        string[] orpids = pids.Split(',');
        if (orpids.Length > 1)
        {
            status = "<script language='javascript'>";
            for (int i = 1; i < orpids.Length; i++)
            {
                //读取用户输入的数量
                int pdtNum = 0;
                string numStr = Request[orpids[i]];
                if (numStr == null)
                {
                    numStr = "0";
                }
                if (numStr != "")
                {
                    try
                    {
                        pdtNum = Convert.ToInt32(numStr);
                    }
                    catch (Exception)
                    {

                    }
                }
                else
                    pdtNum = 0;
                if (pdtNum > 0)
                    status = status + "Form1." + orpids[i] + ".value=" + pdtNum.ToString() + ";";
            }
            status = status + "</script>";
            this.txt_jsLable.Text = status;
        }
    }

    protected void btnEditProduct_Click(object sender, EventArgs e)
    {
        if (CountryCity2.CheckFill() == false)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请选择地址！')</script>");
            return;
        }
        if (Txtdz.Text.Trim() == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请填写地址！')</script>");
            return;
        }

        string OrderId = Request.QueryString["OrderID"].ToString();
        string bianhao = Request.QueryString["Number"].ToString();

        SqlDataReader hydr = DAL.DBHelper.ExecuteReader("select PayCurrency,DefrayType from MemberOrder where orderid='" + OrderId + "'");
        string fukuanMode = "";
        int bizhong = 0;

        if (hydr.Read())
        {
            fukuanMode = hydr["DefrayType"].ToString();
            bizhong = Convert.ToInt32(hydr["PayCurrency"]);

            hydr.Close();
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('订单不存在')</script>");
            return;
        }

        string error = "";
        //获取用户选择商品的集合

        IList<MemberDetailsModel> choseProList = AddMemberDetails();
        if (choseProList == null)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + this.GetTran("000294", "抱歉！购货数量必须输入数字！") + "');</script>", false);
            return;
        }
        if (choseProList.Count == 0 && Request.QueryString["tp"] == "-1")
        {

            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + this.GetTran("000296", "抱歉！您还没有选择商品！") + "');</script>", false);
            return;
        }
        if (Convert.ToBoolean(ViewState["StateCount"]))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("007039", "对不起，您选择了停售产品，并且超出了店铺库存数量！") + "');</script>", false);
            return;
        }


        //获得总的购货金额和pv
        double TotalMoney = registermemberBLL.getZongJing(choseProList);
        double SumPv = registermemberBLL.getZongPv(choseProList);

        double bottonLine = new BLL.Registration_declarations.AddOrderBLL().GetBottomLine();
        double ce = TotalMoney - bottonLine;
        if (ce < 0)
        {
            ScriptHelper.SetAlert(Page, "报单金额必须大于" + bottonLine.ToString());
            return;
        }

        //无货总金额
        double notEnoughmoney = registermemberBLL.CheckMoneyIsEnough(choseProList, GetStoreID(), OrderId);
        //if (Ddzf.SelectedValue != "2")
        //{
        //    if (notEnoughmoney > Convert.ToDouble(ViewState["dpm"]))
        //    {
        //        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('店铺报单额不够！')</script>");
        //        return;
        //    }
        //}
        if (this.Ddzf.SelectedValue.ToString() == "2")
        {
            string elcNumber = this.txtdzbh.Text.Trim().ToLower();
            string elcPassword = Encryption.Encryption.GetEncryptionPwd(this.txtdzmima.Text, elcNumber);
            string CheckEctResult = registermemberBLL.CheckEctPassWord(this.Ddzf.SelectedValue, elcNumber, elcPassword);
            if (CheckEctResult != null)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + CheckEctResult + "');</script>", false);
                return;
            }

            //需要修改
            double elcMoney = Convert.ToDouble(CommonDataBLL.EctBalance(txtdzbh.Text.Trim()));
            //如果电子金额少于用户订单总额
            if (elcMoney < Convert.ToDouble(TotalMoney))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("001528", "用户") + this.txtdzbh.Text.Trim() + GetTran("001529", "的电子钱包不够本次报单！") + "');</script>", false);
                return;
            }
        }
        //有货总金额
        double EnoughProductMoney = 0.00;//Convert.ToDouble(registermemberBLL.getEnoughProductMoney(choseProList, GetStoreID()));

        int qs = BLL.CommonClass.CommonDataBLL.getMaxqishu();

        List<MemberDetailsModel> lmdm = new List<MemberDetailsModel>();

        lmdm = (List<MemberDetailsModel>)choseProList;

        if (Session["mim"] != null)
        {
            MemberInfoModel mim = (MemberInfoModel)Session["mim"];

            MemberOrderModel mom = new MemberOrderModel();
            mom.Number = bianhao;
            mom.OrderId = OrderId;
            mom.StoreId = GetStoreID();
            mom.TotalMoney = Convert.ToDecimal(MemberOrderAgainBLL.GetBzMoney(GetStoreID(), Convert.ToDouble(TotalMoney)));// Convert.ToDecimal(TotalMoney);
            mom.TotalPv = Convert.ToDecimal(SumPv);

            DataTable dtoinfo = RegistermemberBLL.Getmominfoofmember(OrderId);

            if (dtoinfo != null && dtoinfo.Rows.Count > 0)
            {
                mom.CarryMoney = Convert.ToDecimal(dtoinfo.Rows[0]["CarryMoney"]);
                mom.OrderExpect = Convert.ToInt32(dtoinfo.Rows[0]["OrderExpectNum"]);
                mom.PayExpect = Convert.ToInt32(dtoinfo.Rows[0]["PayExpectNum"]);
                mom.Err = "";
                mom.IsAgain = Convert.ToInt32(dtoinfo.Rows[0]["IsAgain"]);
                mom.OrderDate = Convert.ToDateTime(dtoinfo.Rows[0]["OrderDate"]); //DateTime.Now;
                mom.Remark = "";
                mom.DefrayState = Convert.ToInt32(dtoinfo.Rows[0]["DefrayState"]);
                mom.Consignee = dtoinfo.Rows[0]["Consignee"].ToString();
                mom.RemittancesId = dtoinfo.Rows[0]["RemittancesId"].ToString();
                mom.ElectronicaccountId = dtoinfo.Rows[0]["ElectronicaccountId"].ToString();
                mom.OrderType = Convert.ToInt32(dtoinfo.Rows[0]["OrderType"]);
                mom.IsreceiVables = Convert.ToInt32(dtoinfo.Rows[0]["DefrayState"]);
            }

            CityModel cm = new CityModel();
            cm.Country = CountryCity2.Country;
            cm.Province = CountryCity2.Province;
            cm.City = CountryCity2.City;
            cm.Xian = CountryCity2.Xian;

            mom.ConCity = cm;
            mom.CCPCCode = mim.CPCCode;
            mom.ConAddress = Encryption.Encryption.GetEncryptionAddress(Txtdz.Text.Trim());
            mom.ConZipCode = mim.PostalCode;
            mom.ConTelPhone = mim.HomeTele;
            mom.ConMobilPhone = mim.MobileTele;
            mom.ConPost = mim.Email;
            mom.DefrayType = Convert.ToInt32(Ddzf.SelectedValue);
            mom.DefrayType = Convert.ToInt32(ViewState["DefrayType"]);
            mom.PayMoney = Convert.ToDecimal(MemberOrderAgainBLL.GetBzMoney(GetStoreID(), Convert.ToDouble(TotalMoney)) / MemberOrderAgainBLL.GetBzHl(Convert.ToInt32(DropCurrency.SelectedValue)));//Convert.ToDecimal(TotalMoney);
            mom.PayCurrency = Convert.ToInt32(DropCurrency.SelectedValue);
            mom.StandardCurrency = MemberOrderAgainBLL.GetBzTypeId(GetStoreID());
            mom.StandardcurrencyMoney = Convert.ToDecimal(TotalMoney);
            mom.OperateIp = Request.UserHostAddress;
            mom.OperateNumber = GetStoreID();
            mom.SendType = Convert.ToInt32(ddth.SelectedValue);

            if (mom.DefrayState == 0)
            {
                mom.PaymentMoney = 0;
            }
            else
            {
                mom.PaymentMoney = Convert.ToDecimal(MemberOrderAgainBLL.GetBzMoney(GetStoreID(), Convert.ToDouble(TotalMoney)) / MemberOrderAgainBLL.GetBzHl(Convert.ToInt32(DropCurrency.SelectedValue)));
            }
            mom.ReceivablesDate = Convert.ToDateTime(DBHelper.ExecuteScalar("select ReceivablesDate from MemberOrder where OrderID='" + OrderId + "'")); //DateTime.Now;
            mom.EnoughProductMoney = Convert.ToDecimal(EnoughProductMoney);
            mom.LackProductMoney = Convert.ToDecimal(MemberOrderAgainBLL.GetBzMoney(GetStoreID(), Convert.ToDouble(notEnoughmoney)));//获得标准币种Convert.ToDecimal(notEnoughmoney);
            mom.SendWay = Convert.ToInt32(DDLSendType.SelectedValue);
            if (Ddzf.SelectedValue == "2")
            {
                mom.ElectronicaccountId = this.txtdzbh.Text.Trim();
            }
            if (mom.OperateNumber.Trim().Length == 0)
            {
                if (Session["Company"] != null)
                    mom.OperateNumber = "管理员：" + Session["Company"].ToString();
            }

            string aa = MemberOrderBLL.UpdateMemberOrder(OrderId, lmdm, mom, GetStoreID());

            if (aa == "1")
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('修改成功');window.location.href=window.location.href+'&date='+new Date().getTime()</script>");
            else
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('修改失败')</script>");
        }
        else
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('登录超时，请重新登录');window.location.href='index.aspx'</script>");
    }
}