using System;
using System.Collections.Generic;
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
using BLL;
using Model;
using System.Collections;
using BLL.Registration_declarations;
using BLL.CommonClass;

public partial class Store_MemberOrderAgain : BLL.TranslationBase
{

    
    /// <summary>
    /// 页面加载事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        this.lblMoney.Text = new RegistermemberBLL().GetLeftRegisterMemberMoney(GetStoreId());
        //获得语言

        if (!IsPostBack)
        {
            //绑定货币下拉框
            AddOrderBLL.BindCurrency_Rate(this.DropCurrency,GetStoreId());
            CommonDataBLL.GetPaymentType(rdPayType, 2);
            if (IsEdit())
            {
                EditBind();
            }
            else
            {
                AddBind();
                GetBindAddress();
            }
        }
        else
        {
            savePageDisp();
        }

        Translations();
    }

    /// <summary>
    /// 翻译
    /// </summary>
    private void Translations()
    {
        this.TranControls(this.ddth, new string[][] { new string[] { "000464", "自提" } ,
                                                        new string[]{ "000543", "邮寄" } ,
                                                        new string[]{ "000470", "其它" }
        });
        this.TranControls(this.go, new string[][] { new string[] { "000434", "确 定" } });
        this.TranControls(this.DDLSendType, new string[][] { new string[] { "007103", "公司发货到店铺" }, new string[] { "007104", "公司直接发给会员" } });
    }


    /// <summary>
    /// 添加的时，绑定信息
    /// </summary>
    private void AddBind()
    {
        //绑定产品树
        ProductTree myTree = new ProductTree();
        this.menuLabel.Text = myTree.getMenu10Again(GetStoreId());

        this.rdPayType.Enabled = true;
        this.rdPayType.AutoPostBack = true;
        this.dzpanel.Visible = false;
    }

    /// <summary>
    ///  修改时绑定信息
    /// </summary>
    private void EditBind()
    {
        this.Txtbh.Enabled = false;
        this.rdPayType.Enabled = false;
        this.dzpanel.Visible = false;

        string OrderId = GetOrderId();

        //绑定产品树
        ProductTree myTree = new ProductTree();
        myTree.Orderid = OrderId;
        this.menuLabel.Text = myTree.getEditMenu10Again(GetStoreId(), GetOrderId());

        //绑定产品信息
        UptTree(OrderId);

        //绑定订单信息
        this.Txtbh.Enabled = false;//修改时会员编号不能修改
        MemberOrderModel mo = MemberOrderAgainBLL.GetMemberOrderByOrderId(OrderId);
        this.Txtbh.Text = mo.Number;
        this.Txtxm.Text = Encryption.Encryption.GetDecipherName(mo.Consignee);
        this.Txtyb.Text = mo.ConZipCode;
        this.Email.Text = mo.ConPost;
        this.Txtjtdh.Text = Encryption.Encryption.GetDecipherTele(mo.ConTelPhone);
        this.Txtyddh.Text = Encryption.Encryption.GetDecipherTele(mo.ConMobilPhone);
        this.CountryCity1.SelectCountry(mo.ConCity.Country, mo.ConCity.Province, mo.ConCity.City,mo.ConCity.Xian);
        this.Txtdz.Text = Encryption.Encryption.GetDecipherAddress(mo.ConAddress);
        this.rdPayType.SelectedValue = mo.DefrayType.ToString();
        this.Txtbz.Text = mo.Remark;
        if (mo.DefrayState.ToString() == "1")
        {
            this.rdPayType.Enabled = false;
        }
        if (this.rdPayType.SelectedValue == "2")
        {
            this.dzpanel.Visible = true;
            this.txtdzbh.Text = mo.ElectronicaccountId;
        }
        this.DropCurrency.SelectedValue = mo.PayCurrency.ToString();//支付币种

        DDLSendType.SelectedValue = mo.SendWay.ToString();

        this.TxtStore.Text = GetStoreId();

        GetBindAddress();
        string addess = mo.ConCity.Country + " " + mo.ConCity.Province + " " + mo.ConCity.City + " " + mo.ConCity.Xian + " " + mo.ConAddress;
        this.rbtAddress.SelectedValue = addess;

    }

    /// <summary>
    /// 确定按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void go_Click(object sender, EventArgs e)
    {
        //获取用户选择商品的总钱和总积分
        IList<MemberDetailsModel> choseProList = AddMemberDetails();
        decimal SumMoney = Convert.ToDecimal(ViewState["TotalMoney"]);// Convert.ToDecimal(new RegistermemberBLL().getZongJing(choseProList));
        decimal SumPv = Convert.ToDecimal(ViewState["TotalPv"]);// Convert.ToDecimal(new RegistermemberBLL().getZongPv(choseProList));
        string orderID = MemberOrderAgainBLL.GetOrderInfo(IsEdit(), GetOrderId());//获取报单号
        MemberOrderModel momberorder = AddOrdrer(orderID, SumMoney, SumPv);

        //需要修改session取得期数，需要修改
        if (!new RegistermemberBLL().IsMaxQiShu(CommonDataBLL.getMaxqishu()))
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert('" + GetTran("001543", "只可对最大期数据进行操作") + "');", true);
            return;
        }
        //编号不能为空
        if (Txtbh.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert('" + GetTran("001547", "编号不能为空！") + "');", true); 
            return;
        }

        if (panel2.Visible)
        {
            //验证国家省份城市是否选择
            if (CountryCity1.Country == "" || CountryCity1.Province == "" || CountryCity1.City == "")//|| CountryCity1.Country == "请选择" || CountryCity1.Province == "请选择" || CountryCity1.City == "请选择")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert('" + GetTran("001548", "对不起，请选择国家省份城市！") + "');", true);
                return;
            }
        }

        if (panel2.Visible)
        {
            //详细地址不能为空
            if (Txtdz.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert('" + GetTran("006933", "对不起，请填写详细地址！") + "');", true);
                return;
            }
        }

        //编号是否存在
        if (new MemberOrderAgainBLL().CheckNuberIsExist(this.Txtbh.Text.Trim()) <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert('" + GetTran("001549", "对不起,该编号不存在！") + "');", true); 
            return;
        }
        //得到用户选择商品总金额和总积分
        if (choseProList.Count == 0)
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert('" + GetTran("001550", "对不起，您还没有输入订货数量信息！") + "');", true);
            return;
        }
        if (Convert.ToBoolean(ViewState["StateCount"]))
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert('" + GetTran("007039", "对不起，您选择了停售产品，并且超出了店铺库存数量！") + "');", true);
            return;
        }

        //电子钱包密码是否正确
        if (this.rdPayType.SelectedValue.ToString() == "2")
        {
            //用户编号不能为空
            if (txtdzbh.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert('" + GetTran("001551", "对不起，您输入的用户编号！") + "');", true);
                return;
            }
            //电子账户密码不能为空
            if (txtdzbh.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert('" + GetTran("001552", "对不起，您输入的电子账户密码！") + "');", true);
                return;
            }
            //验证电子账户密码
            if (!MemberOrderAgainBLL.CheckEctPassWord(txtdzbh.Text, Encryption.Encryption.GetEncryptionPwd(txtdzbh.Text, txtdzmima.Text)))
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert('" + GetTran("001554", "对不起，您输入的电子账户密码不正确！") + "');", true);
                return;
            }
        }

        //分析购物条件
        double PrevMoney = 0;
        double StoreLeftMoney = 0;
        bool judge = true;
        double notEnoughmoney = new RegistermemberBLL().CheckMoneyIsEnough(choseProList, GetStoreId(),GetOrderId());
        string checkResult = MemberOrderAgainBLL.CheckOption(IsEdit(), notEnoughmoney, orderID, GetStoreId(), choseProList, this.rdPayType.SelectedValue.ToString(), out StoreLeftMoney, out PrevMoney, out judge);

        if (checkResult != null)
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", checkResult, true);
            return;
        }

        //检查公司逻辑库存
        IList<MemberDetailsModel> porList = CommonDataBLL.GetNewOrderDetail1(choseProList);
        for (int i = 0; i < porList.Count; i++)
        {
            int left = BLL.CommonClass.CommonDataBLL.GetLeftLogicProductInventory(Convert.ToInt32(porList[i].ProductId));
            if (left < choseProList[i].Quantity)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert('" + GetTran("005967", "对不起，公司库存不够") + "！" + porList[i].ProductName + GetTran("005970", "库存数只有") + "：" + left + "');", true);
                return;
            }
        }

        //电子钱包是否足够
        if (this.rdPayType.SelectedValue == "2")
        {
            //需要修改
            double elcMoney = Convert.ToDouble(CommonDataBLL.EctBalance(this.txtdzbh.Text.Trim()));
            //如果电子金额少于用户订单总额
            if (elcMoney < Convert.ToDouble(momberorder.TotalMoney))
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert('" + GetTran("001528", "用户") + this.txtdzbh.Text + GetTran("001529", "的电子钱包不够本次报单！") + "');", true); 
                return;
            }
        }

        //不足货物的钱
        momberorder.LackProductMoney = Convert.ToDecimal(MemberOrderAgainBLL.GetBzMoney(GetStoreId(), notEnoughmoney));//获得标准币种


        //添加订单，跟新会员业绩，和该店库存报单的费用
        if (MemberOrderAgainBLL.AddOrderData(IsEdit(), momberorder, choseProList))
        {
            if (!IsEdit())
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert('" + GetTran("001556", "报单成功！") + "');location.href='MemberOrderAgain.aspx';", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert('" + GetTran("000222", "修改成功！") + "');location.href='BrowseMemberOrders.aspx';", true);
            }
        }
        else
        {
            if (!IsEdit())
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert('" + GetTran("001557", "报单失败！") + "');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert('" + GetTran("000225", "修改失败！") + "');", true);
            }
        }
    }

    /// <summary>
    /// 获取店铺编号
    /// </summary>
    /// <returns>返回店铺编号</returns>
    private string GetStoreId()
    {
        return MemberOrderAgainBLL.GetStoreIdByOrderId(GetOrderId());
    }

    /// <summary>
    /// 修改时：获取订单号
    /// </summary>
    /// <returns></returns>
    private string GetOrderId()
    {
        if (Request.QueryString["orderId"] != null)
        {
            return Request.QueryString["orderId"].ToString();
        }
        return "";
    }

    /// <summary>
    /// 判断是添加还是修改
    /// </summary>
    /// <returns>True为修改，False为添加</returns>
    private bool IsEdit()
    {
        if (Request.QueryString.Count == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }


    /// <summary>
    /// MemberOrderModel类对象赋值
    /// </summary>
    /// <returns></returns>
    public MemberOrderModel AddOrdrer(string orderId, decimal totalMoney, decimal totalPv)
    {
        MemberOrderModel mo = new MemberOrderModel();
        mo.Number = this.Txtbh.Text;
        mo.OrderId = orderId;
        mo.StoreId = GetStoreId();
        mo.TotalMoney = Convert.ToDecimal(MemberOrderAgainBLL.GetBzMoney(GetStoreId(), Convert.ToDouble(totalMoney)));//获得标准币种
        mo.TotalPv = totalPv;
        mo.OrderExpect = CommonDataBLL.getMaxqishu();
        mo.PayExpect = CommonDataBLL.getMaxqishu();
        mo.IsAgain = 1;
        mo.OrderDate = DateTime.Now;
        mo.Err = "";
        mo.Remark = this.Txtbz.Text;
        mo.DefrayState = 1;

        mo.PayCurrency = Convert.ToInt32(DropCurrency.SelectedValue);
        mo.PayMoney = Convert.ToDecimal(MemberOrderAgainBLL.GetBzMoney(GetStoreId(), Convert.ToDouble(totalMoney)) / MemberOrderAgainBLL.GetBzHl(mo.PayCurrency));

        mo.StandardCurrency = MemberOrderAgainBLL.GetBzTypeId(GetStoreId());
        mo.StandardcurrencyMoney = totalMoney;

        mo.OperateIp = CommonDataBLL.OperateIP;
        mo.OperateNumber = GetStoreId();
        mo.DefrayType = Convert.ToInt32(this.rdPayType.SelectedValue);
        mo.CarryMoney = 0;
        mo.RemittancesId = "";
        mo.ElectronicaccountId = this.txtdzbh.Text.Trim();
        mo.OrderType = 1;
        if (panel2.Visible)
        {
            mo.ConCity.Country = this.CountryCity1.Country;
            mo.ConCity.Province = this.CountryCity1.Province;
            mo.ConCity.City = this.CountryCity1.City;
            mo.ConAddress = Encryption.Encryption.GetEncryptionAddress(this.Txtdz.Text);
        }
        else
        {
            string addressRbt = this.rbtAddress.SelectedItem.Text.Trim();
            string gas523 = this.rbtAddress.SelectedValue;
            string[] addr = addressRbt.Split(' ');
            mo.ConCity.Country = addr[0].ToString();
            mo.ConCity.Province = addr[1].ToString();
            mo.ConCity.City = addr[2].ToString();
            mo.ConAddress = Encryption.Encryption.GetEncryptionAddress(addr[3].ToString());
        }
        mo.ConTelPhone = Encryption.Encryption.GetEncryptionTele(this.Txtjtdh.Text.Trim());
        mo.ConMobilPhone = Encryption.Encryption.GetEncryptionTele(this.Txtyddh.Text.Trim());
        mo.ConPost = this.Email.Text.Trim();
        mo.Consignee = Encryption.Encryption.GetEncryptionName(Txtxm.Text.Trim());
        mo.ConZipCode = this.Txtyb.Text.Trim();
        mo.SendWay = Convert.ToInt32(DDLSendType.SelectedValue);
        //如果已经支付的报单
        if (IsEdit())
        {
            MemberOrderModel moOld = MemberOrderAgainBLL.GetMemberOrderByOrderId(orderId);
            mo.OrderDate = moOld.OrderDate;
            mo.OrderExpect = moOld.OrderExpect;
            mo.PayExpect = moOld.PayExpect;
            mo.DefrayState = moOld.DefrayState;
            mo.IsAgain = moOld.IsAgain;
            mo.OrderType = moOld.OrderType;
        }

        return mo;
    }

    /// <summary>
    /// 取得用户所选的商品集合
    /// </summary>
    /// <returns></returns>
    public IList<MemberDetailsModel> AddMemberDetails()
    {
        /*************************************************************************************/
        //for (int i = 0; i < productModelList.Count; i++)
        //{
            #region
            //object pNumber = Request["N" + productModelList[i].ProductID];
            //int ptNum = 0;
            //if (pNumber != null)
            //{
            //    try
            //    {
            //        ptNum = Convert.ToInt32(pNumber);
            //    }
            //    catch
            //    {
            //       // return null;
            //    }
            //    if (ptNum > 0)
            //    {
            //        if (productModelList[i].IsCombineProduct == 1) { hasGroupItem = "true"; } else { }
            //        arrayList.Add(new RegistermemberBLL().GetUserChooseProduct(ptNum, productModelList[i]));
            //    }
            //}
            //else
            //{
            //    ptNum = 0;
            //}
            #endregion
        //}
        double totalMoney = 0.00;
        double totalPv = 0.00;

        ViewState["StateCount"] = false;//声明一个状态，判断选择的产品数量是否超出库存

        //获取用户选择商品id集合
        List<MemberDetailsModel> details = new List<MemberDetailsModel>();
        //获取商品集合
        List<ProductModel> productModelList = new RegistermemberBLL() .GetProductModelList();
        //用户选择商品到总商品集合里去匹配
        for (int i = 0; i < productModelList.Count; i++)
        {

            string productid = productModelList[i].ProductID.ToString();
            string productName = productModelList[i].ProductName.ToString();
            string numStr = Request["N" + productid];

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
                double price = Convert.ToDouble(productModelList[i].PreferentialPrice);
                double pv = Convert.ToDouble(productModelList[i].PreferentialPV);
                totalMoney += price * pdtNum;
                totalPv += pv * pdtNum;

                MemberDetailsModel md = new MemberDetailsModel();
                md.Price = (decimal)price;
                md.Pv = (decimal)pv;
                md.Quantity = (int)pdtNum;
                md.ProductId = (int)productModelList[i].ProductID;
                md.ProductName = productName;

                details.Add(md);

                
                //判断不可销售的产品是否超出库存
                if (MemberOrderAgainBLL.GetIsSellByProId(Convert.ToInt32(productModelList[i].ProductID)) == "1")
                {
                    
                    int shuliang = md.Quantity;
                    if (IsEdit())
                    {
                        
                        DataTable dtdetail = MemberOrderAgainBLL.GetMemberDetailsByOrderID(GetOrderId());
                        for (int j = 0; j < dtdetail.Rows.Count; j++)
                        {
                            if (dtdetail.Rows[j]["productid"].ToString() == productModelList[i].ProductID.ToString())
                            {
                                shuliang = shuliang - Convert.ToInt32(dtdetail.Rows[j]["Quantity"]);
                            }
                        }
                    }

                    if (MemberOrderAgainBLL.GetCountByProIdAndStoreId(Convert.ToInt32(productModelList[i].ProductID), GetStoreId()) + shuliang < 0)
                    {
                        ViewState["StateCount"] = true;
                    }
                }


            }
        }
        ViewState["TotalMoney"] = totalMoney;
        ViewState["TotalPv"] = totalPv;

        return details;

    }


    /// <summary>
    /// 更新树
    /// </summary>
    /// <param name="orderId">订单id</param>
    public void UptTree(string orderId)
    {
        AddOrderBLL addOrderBLL = new AddOrderBLL();
        string result = "<script language='javascript'>";
        List<MemberDetailsModel> list = addOrderBLL.GetDetails(orderId);
        List<MemberDetailsModel> OrignalChoose2 = new List<MemberDetailsModel>();
        List<MemberDetailsModel> OrignalChoose3 = new List<MemberDetailsModel>();
        foreach (MemberDetailsModel memberDetailsModel in list)
        {
            result = result + "Form1.N" + memberDetailsModel.ProductId + ".value=" + memberDetailsModel.Quantity;
            result += ";";
        }
        result += "</script>";
        this.Label1.Text = result;
    }



    /// <summary>
    ///  刷新后保存用户输入的数量
    /// </summary>
    public void savePageDisp()
    {
        string status = null;
        //获取用户选择的商品
        List<int> list = new RegistermemberBLL().GetProductID();
        status = "<script language='javascript'>";
        for (int i = 0; i < list.Count; i++)
        {
            //读取用户输入的数量
            int pdtNum;
            string numStr = Request["N" + list[i].ToString()];
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
                catch
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert('" + GetTran("001559", "对不起，订单数量只能输入数字！") + "');", true);
                    return;
                }
            }
            else
                pdtNum = 0;
            if (pdtNum > 0)
                status = status + "Form1.N" + list[i].ToString() + ".value=" + pdtNum.ToString() + ";";
        }
        status = status + "</script>";
        this.Label1.Text = status;
    }


    protected void Ddzf_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.rdPayType.SelectedValue == "2")
        {
            this.DropCurrency.SelectedIndex = 0;
            this.DropCurrency.Enabled = false;
            this.dzpanel.Visible = true;
        }
        else
        {
            this.DropCurrency.Enabled = true;
            this.dzpanel.Visible = false;
            AddOrderBLL.BindCurrency_Rate(this.DropCurrency, GetStoreId());
        }
    }
    /// <summary>
    /// 获取该用户编号的信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Txtbh_TextChanged(object sender, EventArgs e)
    {
        if (MemberOrderAgainBLL.CheckMemberExist(Txtbh.Text.Trim()))
        {
            MemberInfoModel mi = MemberOrderAgainBLL.GetMemberInfoByNumber(Txtbh.Text.Trim());
            this.Txtxm.Text = Encryption.Encryption.GetDecipherName(mi.Name);
            this.Txtyb.Text = mi.PostalCode;
            this.Email.Text = mi.Email;
            this.Txtjtdh.Text = Encryption.Encryption.GetDecipherTele(mi.HomeTele);
            this.Txtyddh.Text = Encryption.Encryption.GetDecipherTele(mi.MobileTele);
            this.Txtdz.Text = Encryption.Encryption.GetDecipherAddress(mi.Address);
            CountryCity1.SelectCountry(mi.City.Country, mi.City.Province, mi.City.City,mi.City.Xian);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert('" + GetTran("001560", "对不起，您输入的会员编号不存在，请重新输入！") + "')", true); 
            Txtbh.Text = "";
        }
    }

    private void GetBindAddress()
    {
        DataTable dt = BLL.CommonClass.CommonDataBLL.GetBindAddress(this.Txtbh.Text.Trim());
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string code = dt.Rows[i][0].ToString().Substring(0, 8);
                string addres = BLL.CommonClass.CommonDataBLL.GetAddressByCode(code);
                addres += " " +Encryption.Encryption.GetDecipherAddress(dt.Rows[i][0].ToString().Substring(8, dt.Rows[i][0].ToString().Length - 8));
                this.rbtAddress.Items.Add(addres);
            }
            this.panel2.Visible = false;
            this.panel1.Visible = true;
            this.rbtAddress.Items.Add("新地址");

            this.rbtAddress.SelectedIndex = 0;
        }
        else
        {
            this.panel2.Visible = true;
            this.panel1.Visible = false;
        }

    }


    protected void rbtAddress_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.rbtAddress.SelectedItem.Text == "新地址")
        {
            this.panel2.Visible = true;
            this.Txtyb.Text = "";
        }
        else
        {
            string addres = this.rbtAddress.SelectedItem.Text.Trim();
            string[] addr = addres.Split(' ');
            this.Txtyb.Text = DAL.CommonDataDAL.GetZipCode(addr[0], addr[1], addr[2]);
            this.panel2.Visible = false;
        }
    }
}
