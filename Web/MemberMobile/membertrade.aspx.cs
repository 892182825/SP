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
using BLL.other.Member;
using DAL;

public partial class Member_membertrade : BLL.TranslationBase
{
    /// <summary>
    /// 页面加载事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        if (!IsPostBack)
        {
            //绑定货币下拉框
            AddOrderBLL.BindCurrency_Rate(this.DropCurrency);
           // CommonDataBLL.GetPaymentType(rdPayType, 6);

            //防止重复提交
            go.Attributes["realvalue"] = "0";
            go.Attributes["onclick"] = "if(navigator.userAgent.toLowerCase().indexOf('ie')!=-1){this.realvalue   ++;if(this.realvalue==1){if (confirm('" + GetTran("007151", "确定提交吗？") + "?')) {return true;} else {this.realvalue=0; return false;}}else{alert('" + GetTran("000792", "不可重复提交") + "!');return false}}else{int(this.btnReg.getAttribute['realvalue'])   ++;if(int(this.btnReg.getAttribute['realvalue'])==1){if (confirm('" + GetTran("007151", "确定提交吗？") + "?')) {return true;} else {int(this.btnReg.getAttribute['realvalue'])=0; return false;}}else{alert('" + GetTran("000792", "不可重复提交") + "!');return false}}";

            if (IsEdit())
            {
                EditBind();
            }
            else
            {
                if (Session["proList"] != null && Session["proList"] != null)
                {
                    ArrayList list = (ArrayList)Session["proList"];

                    ShoppingBind(list);

                    GetBindAddress();

                }
                else
                {
                    AddBind();
                    GetBindAddress();
                }
            }
        }
        else
        {
            savePageDisp();
        }

        Translations();
    }

    private void ShoppingBind(ArrayList list)
    {
        ProductTree myTree = new ProductTree();
        string manageId = BLL.CommonClass.CommonDataBLL.getManageID(2);
        this.menuLabel.Text = myTree.getEditMenuShopp(manageId, list);

        //绑定产品信息

        string result = "<script language='javascript'>";

        foreach (MemberDetailsModel memberDetailsModel in list)
        {
            this.hidpids.Value += ",N" + memberDetailsModel.ProductId;
            result = result + "Form1.N" + memberDetailsModel.ProductId + ".value=" + memberDetailsModel.Quantity;
            result += ";";
        }
        result += "</script>";
        this.Label1.Text = result;
    }

    /// <summary>
    /// 翻译
    /// </summary>
    private void Translations()
    {
        this.TranControls(this.ddth, new string[][] { 
             new string[]{ "000543", "邮寄" } ,
            new string[] { "000464", "自提" } ,
                                                       
        });
        this.TranControls(this.go, new string[][] { new string[] { "000434", "确 定" } });
        this.TranControls(this.DDLSendType, new string[][] { new string[] { "007103", "公司发货到店铺" }, new string [] { "007104", "公司直接发给会员" } });
    }


    /// <summary>
    /// 添加的时，绑定信息
    /// </summary>
    private void AddBind()
    {
        //购货店铺，默认为注册店铺
        TxtStore.Text = GetStoreId();
        //绑定产品树
        ProductTree myTree = new ProductTree();
        this.menuLabel.Text = myTree.getMemberProductList(GetStoreId());

        //绑定会员信息
        MemberInfoModel mi = MemberInfoModifyBll.getMemberInfo(GetMember());
        lblNumber.Text = mi.Number;
        lblName.Text = Encryption.Encryption.GetDecipherName(mi.Name);
        this.Txtxm.Text = Encryption.Encryption.GetDecipherName(mi.Name);
        this.Txtyb.Text = mi.PostalCode;
        this.Email.Text = mi.Email;
        this.Txtjtdh.Text = Encryption.Encryption.GetDecipherTele(mi.HomeTele);
        this.Txtyddh.Text = Encryption.Encryption.GetDecipherTele(mi.MobileTele);
        //this.CountryCity1.SelectCountry(mi.City.Country, mi.City.Province, mi.City.City,mi.City.Xian);
        //this.Txtdz.Text = Encryption.Encryption.GetDecipherAddress(mi.Address);

        //this.rdPayType.Enabled = true;
        //this.rdPayType.AutoPostBack = true;
        //this.dzpanel.Visible = false;
    }

    /// <summary>
    ///  修改时绑定信息
    /// </summary>
    private void EditBind()
    {
        //this.rdPayType.Enabled = false;
        //this.dzpanel.Visible = false;

        string OrderId = GetOrderId();

        //绑定产品树
        ProductTree myTree = new ProductTree();
        myTree.Orderid = OrderId;
        this.menuLabel.Text = myTree.getEditMenu10(GetStoreId(),GetOrderId());

        //绑定产品信息
        UptTree(OrderId);

        //绑定会员信息
        MemberInfoModel mi = MemberInfoModifyBll.getMemberInfo(GetMember());
        lblNumber.Text = mi.Number;
        lblName.Text = Encryption.Encryption.GetDecipherName(mi.Name);
        //绑定订单信息
        MemberOrderModel mo = MemberOrderAgainBLL.GetMemberOrderByOrderId(OrderId);
        this.Txtxm.Text = Encryption.Encryption.GetDecipherName(mo.Consignee);
        this.Txtyb.Text = mo.ConZipCode;
        this.Email.Text = mo.ConPost;
        this.Txtjtdh.Text = Encryption.Encryption.GetDecipherTele(mo.ConTelPhone);
        this.Txtyddh.Text = Encryption.Encryption.GetDecipherTele(mo.ConMobilPhone);
        this.CountryCity1.SelectCountry(mo.ConCity.Country, mo.ConCity.Province, mo.ConCity.City,mo.ConCity.Xian);
        this.Txtdz.Text = Encryption.Encryption.GetDecipherAddress(mo.ConAddress);
        this.ddth.SelectedValue = mo.SendType.ToString();
        //this.rdPayType.SelectedValue = mo.DefrayType.ToString();
        this.Txtbz.Text = mo.Remark;
        //if (mo.DefrayState.ToString() == "1")
        //{
        //    this.rdPayType.Enabled = false;
        //}
        //if (this.rdPayType.SelectedValue == "2")
        //{
        //    this.dzpanel.Visible = true;
        //    this.lblNumber.Text = mo.ElectronicaccountId;
        //}
        this.DropCurrency.SelectedValue = mo.PayCurrency.ToString();
        DDLSendType.SelectedValue = mo.SendWay.ToString();

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
        //验证店铺编号
        if (TxtStore.Text == "" || TxtStore.Text==null )
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "click", "alert('" + GetTran("006026", "店铺编号不能为空！") + "');", true);
            return;
        }
        //获取用户选择商品的总钱和总积分
        IList<MemberDetailsModel> choseProList = AddMemberDetails();
        decimal SumMoney = Convert.ToDecimal(ViewState["TotalMoney"]);//Convert.ToDecimal(new RegistermemberBLL().getZongJing(choseProList));
        decimal SumPv = Convert.ToDecimal(ViewState["TotalPv"]);//Convert.ToDecimal(new RegistermemberBLL().getZongPv(choseProList));
        string orderID = MemberOrderAgainBLL.GetOrderInfo(IsEdit(), GetOrderId());//获取报单号
        MemberOrderModel momberorder = AddOrdrer(orderID, SumMoney, SumPv);

       

        //验证店铺是否存在
        if (!MemberOrderAgainBLL.CheckStore(TxtStore.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "click", "alert('" + GetTran("006027", "店铺编号不存在！") + "');", true);
            return;
        }

        //需要修改session取得期数，需要修改
        if (!new RegistermemberBLL().IsMaxQiShu(CommonDataBLL.getMaxqishu()))
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "click", "alert('" + GetTran("001543", "只可对最大期数据进行操作") + "');", true);
            return;
        }
        //得到用户选择商品总金额和总积分
        if (choseProList.Count == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "click", "alert('" + GetTran("001550", "对不起，您还没有输入订货数量信息！") + "');", true);
            return;
        }
        if (Convert.ToBoolean(ViewState["StateCount"]))
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "click", "alert('" + GetTran("007039", "对不起，您选择了停售产品，并且超出了店铺库存数量！") + "');", true);
            return;
        }

        if (panel2.Visible)
        {
            //验证国家省份城市是否选择
            if (CountryCity1.Country == "" || CountryCity1.Province == "" || CountryCity1.City == "")//|| CountryCity1.Country == "请选择" || CountryCity1.Province == "请选择" || CountryCity1.City == "请选择")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "click", "alert('" + GetTran("001548", "对不起，请选择国家省份城市！") + "');", true);
                return;
            }
        }

        if (panel2.Visible)
        {
            //详细地址不能为空
            if (Txtdz.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "click", "alert('" + GetTran("006933", "对不起，请填写详细地址！") + "');", true);
                return;
            }
        }
        double notEnoughmoney = new RegistermemberBLL().CheckMoneyIsEnough(choseProList, GetStoreId(), orderID);
        //不足货物的钱
        momberorder.LackProductMoney = Convert.ToDecimal(MemberOrderAgainBLL.GetBzMoney(GetStoreId(), notEnoughmoney));//获得标准币种

        //添加订单，跟新会员业绩，和该店库存报单的费用
        if (MemberOrderAgainBLL.AddOrderData(IsEdit(), momberorder, choseProList))
        {
            if (!IsEdit())
            {
                double totalmoney = Convert.ToDouble(SumMoney);
                double totalcomm = 0;
                double zongMoney = totalmoney + totalcomm;

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "click", "var formobj=document.createElement('form');"
                                + "formobj.action='../payserver/chosepay.aspx?blif=" + EncryKey.GetEncryptstr(momberorder.OrderId, 1, 1) + "';" +
                                "formobj.method='post';formobj.target='_blank';document.body.appendChild(formobj); formobj.submit();location.href='MemberOrder.aspx';", true);

                //购物车的session
                if(Session["proList"]!=null)
                    Session.Remove("proList");
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "click", "alert('" + GetTran("000222", "修改成功！") + "');location.href='membertrade.aspx';", true);
            }
        }
        else
        {
            if (!IsEdit())
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "click", "alert('" + GetTran("001557", "报单失败！") + "');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "click", "alert('" + GetTran("000225", "修改失败！") + "');", true);
            }
        }
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
    /// 获取店铺编号
    /// </summary>
    /// <returns></returns>
    public string GetStoreId()
    {
        if (TxtStore.Text != "")
        {
            return TxtStore.Text;
        }
        else
        {
            return MemberOrderAgainBLL.GetStoreIdByNumber(GetMember());
        }
    }

    /// <summary>
    /// 获取会员编号
    /// </summary>
    /// <returns></returns>
    private string GetMember()
    {
        if (Session["Member"] != null)
        {
            return Session["Member"].ToString();
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
        mo.Number = lblNumber.Text;
        mo.OrderId = orderId;
        mo.StoreId = GetStoreId();
        mo.TotalMoney = Convert.ToDecimal(MemberOrderAgainBLL.GetBzMoney(GetStoreId(), Convert.ToDouble(totalMoney)));//获得标准币种
        mo.TotalPv = totalPv;
        mo.OrderExpect = CommonDataBLL.getMaxqishu();
        mo.PayExpect = -1;
        mo.IsAgain = 1;
        mo.OrderDate = DateTime.UtcNow;
        mo.Err = "";
        mo.Remark = this.Txtbz.Text;
        mo.DefrayState = 0;//会员网上购物默认为未支付

        mo.PayCurrency = Convert.ToInt32(DropCurrency.SelectedValue);
        mo.PayMoney = Convert.ToDecimal(MemberOrderAgainBLL.GetBzMoney(GetStoreId(), Convert.ToDouble(totalMoney)) / MemberOrderAgainBLL.GetBzHl(mo.PayCurrency));

        mo.StandardCurrency = MemberOrderAgainBLL.GetBzTypeId(GetStoreId());
        mo.StandardcurrencyMoney = totalMoney;

        mo.OperateIp = CommonDataBLL.OperateIP;
        mo.OperateNumber = Session["Member"].ToString();
        mo.DefrayType = 0; //Convert.ToInt32(this.rdPayType.SelectedValue);
        mo.SendType = Convert.ToInt32(this.ddth.SelectedValue);
        mo.CarryMoney = 0;
        mo.RemittancesId = "";
        mo.ElectronicaccountId = lblNumber.Text.Trim();
        mo.OrderType = 22;//会员网上购物
        //if (panel2.Visible)
        //{
            mo.ConCity.Country = this.CountryCity1.Country;
            mo.ConCity.Province = this.CountryCity1.Province;
            mo.ConCity.City = this.CountryCity1.City;
            mo.ConCity.Xian = this.CountryCity1.Xian;
            mo.ConAddress = Encryption.Encryption.GetEncryptionAddress(this.Txtdz.Text);
        //}
        //else
        //{
        //    string addressRbt = this.rbtAddress.SelectedItem.Text.Trim();
        //    string gas523 = this.rbtAddress.SelectedValue;
        //    string[] addr = addressRbt.Split(' ');
        //    mo.ConCity.Country = addr[0].ToString();
        //    mo.ConCity.Province = addr[1].ToString();
        //    mo.ConCity.City = addr[2].ToString();
        //    mo.ConCity.Xian = addr[3];
        //    mo.ConAddress = Encryption.Encryption.GetEncryptionAddress(addr[4].ToString());
        //}
      
        mo.ConTelPhone = Encryption.Encryption.GetEncryptionTele(this.Txtjtdh.Text.Trim());
        mo.ConMobilPhone = Encryption.Encryption.GetEncryptionTele(this.Txtjtdh.Text.Trim());
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
                        if (IsEdit())
                        {
                            DataTable dtdetail = MemberOrderAgainBLL.GetMemberDetailsByOrderID(GetOrderId());
                            for (int j = 0; j < dtdetail.Rows.Count; j++)
                            {
                                if (dtdetail.Rows[j]["productid"].ToString() == productuse.ProductID.ToString())
                                {
                                    shuliang = shuliang - Convert.ToInt32(dtdetail.Rows[j]["Quantity"]);
                                }
                            }
                        }

                        if (MemberOrderAgainBLL.GetCountByProIdAndStoreId(Convert.ToInt32(productuse.ProductID), GetStoreId()) + shuliang < 0)
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

    /// <summary>
    /// 更新树
    /// </summary>
    /// <param name="orderId">订单id</param>
    public void UptTree(string orderId)
    {
        AddOrderBLL addOrderBLL = new AddOrderBLL();
        string result = "<script language='javascript'>";
        List<MemberDetailsModel> list = addOrderBLL.GetDetails(orderId);

        foreach (MemberDetailsModel memberDetailsModel in list)
        {
            this.hidpids.Value += ",N" + memberDetailsModel.ProductId;
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

        string[] pids = this.hidpids.Value.Split(',');

        status = "<script language='javascript'>";
        if (pids.Length > 1)
        {
            for (int i = 1; i < pids.Length; i++)
            {
                //读取用户输入的数量
                int pdtNum;
                string numStr = Request[pids[i]];
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
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "click", "alert('" + GetTran("001559", "对不起，订单数量只能输入数字！") + "');", true);
                        return;
                    }
                }
                else
                    pdtNum = 0;
                if (pdtNum > 0)
                    status = status + "Form1." + pids[i] + ".value=" + pdtNum.ToString() + ";";
            }
        }
        status = status + "</script>";
        this.Label1.Text = status;
    }

    protected void TxtStore_TextChanged(object sender, EventArgs e)
    {
        //验证店铺编号是否存在
        if(!MemberOrderAgainBLL.CheckStore(GetStoreId()))
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "click", "alert('" + GetTran("001779", "对不起，您输入的店铺不存在！") + "');", true);
            return;
        }

        //绑定产品树
        ProductTree myTree = new ProductTree();
        this.menuLabel.Text = myTree.getMenu10(GetStoreId());
        AddOrderBLL.BindCurrency_Rate(this.DropCurrency, GetStoreId());
    }

    private void GetBindAddress()
    {
        DataTable dt = BLL.CommonClass.CommonDataBLL.GetBindAddress(lblNumber.Text);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string code = dt.Rows[i][0].ToString().Substring(0, 8);
                string addres = BLL.CommonClass.CommonDataBLL.GetAddressByCode(code);
                addres += " " + Encryption.Encryption.GetDecipherAddress(dt.Rows[i][0].ToString().Substring(8, dt.Rows[i][0].ToString().Length - 8));
                addres += " " + dt.Rows[i][2].ToString();
                addres += " " + dt.Rows[i][3].ToString();
                addres += " " + Encryption.Encryption.GetDecipherName(dt.Rows[i][4].ToString());
                addres += " " + dt.Rows[i][5].ToString();
                this.rbtAddress.Items.Add(addres);
            }

            //this.panel2.Visible = false;
            //this.panel1.Visible = true;
            //this.rbtAddress.Items.Add("新地址");

            this.rbtAddress.SelectedIndex = 0;

            rbtAddress_SelectedIndexChanged(null, null);
        }
        else
        {
            //this.panel2.Visible = true;
            //this.panel1.Visible = false;
        }

    }


    protected void rbtAddress_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (this.rbtAddress.SelectedItem.Text == "新地址")
        {
            //this.panel2.Visible = true;
            //this.Txtyb.Text = "";
        }
        else
        {
            string addres = this.rbtAddress.SelectedItem.Text.Trim();
            string[] addr = addres.Split(' ');
            CountryCity1.SelectCountry(addr[0], addr[1], addr[2], addr[3]);
            Txtdz.Text = addr[4];
            //DDLSendType.SelectedValue = addr[5];
            //ddth.SelectedValue = addr[6];
            Txtxm.Text = addr[addr.Length - 2];
            Txtjtdh.Text = addr[addr.Length - 1];
            //this.Txtyb.Text = DAL.CommonDataDAL.GetZipCode(addr[0], addr[1], addr[2]);
            //this.panel2.Visible = false;
        }
    }
    protected void goReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("ShoppingCart.aspx");
    }
}
