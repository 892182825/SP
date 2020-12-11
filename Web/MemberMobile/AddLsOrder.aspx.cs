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
using System.Web.Services;
using System.Data.SqlClient;
using BLL.other.Company;

public partial class Member_AddLsOrder : BLL.TranslationBase
{
    protected LetUsOrder luo = new LetUsOrder();
    protected MemberInfoModel mim = null;
    protected int bzCurrency = 0;

    protected double i;
    protected void Page_Load(object sender, EventArgs e)
    {
        //获取标准币种
        bzCurrency = CommonDataBLL.GetStandard();
        i = (AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString())));
        Permissions.ThreeRedirect(Page, "../member/" + Permissions.redirUrl);
        luo.SetVlaue();
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxMemShopCart));
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));

        Translations();

        if (!IsPostBack)
        {
            BindRadioType();

            if (Session["mbreginfo"] != null)
            {
                mim = (MemberInfoModel)Session["mbreginfo"];
            }
            if (Session["UserType"] != null && Session["UserType"].ToString() != "3" && Session["EditOrderID"] == null)
            {
                top.Visible = false;
                bottom.Visible = false;
                STop1.Visible = false;
                SLeft1.Visible = false;
                txtMemBh.Visible = true;
                lblMemBh.Style.Value = "display:none;";
            }
            else
            {
                if (Session["UserType"].ToString() == "2")
                {
                    top.Visible = false;
                    bottom.Visible = false;
                    STop1.Visible = true;
                    SLeft1.Visible = true;
                }
                else
                {
                    top.Visible = false;
                    bottom.Visible = false;
                    STop1.Visible = false;
                    SLeft1.Visible = false;
                }

                txtMemBh.Style.Value = "display:none;";
                lblMemBh.Style.Value = "display:block;";

                txtMemBh.Text = luo.MemBh;
                lblMemBh.Text = luo.MemBh;

            }

            BindData();

            //不是注册过来的新会员
            if (Session["mbreginfo"] == null)
            {

                if ((Session["UserType"] != null && Session["UserType"].ToString() == "3") || Session["EditOrderID"] != null)
                {
                    top.Visible = true;
                    bottom.Visible = true;
                    STop1.Visible = false;
                    SLeft1.Visible = false;


                    Txtyddh.Visible = false;
                    txtName.Visible = false;

                    //不是注册过来的，就进行名称和邮编的查询
                    DataTable dt = DAL.DBHelper.ExecuteDataTable("select name,postalcode,mobiletele,cpccode,address,storeid from memberinfo where number='" + luo.MemBh + "'");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        this.txtName.Text = Encryption.Encryption.GetDecipherName(dt.Rows[0]["Name"].ToString());
                        this.labName.Text = Encryption.Encryption.GetDecipherName(dt.Rows[0]["Name"].ToString());
                        if (dt.Rows[0]["postalcode"].ToString() != "")
                        {
                            this.txtPostCode.Text = dt.Rows[0]["postalcode"].ToString();
                            this.labPostCode.Text = dt.Rows[0]["postalcode"].ToString();
                        }
                        else
                        {
                            this.txtPostCode.Text = "111111";
                            this.labPostCode.Text = "111111";
                        }
                        this.Txtyddh.Text = dt.Rows[0]["mobiletele"].ToString();
                        this.labyddh.Text = dt.Rows[0]["mobiletele"].ToString();
                    }
                    GetBindAddress();
                }
                else
                {
                    if (Session["UserType"].ToString() == "2")
                    {
                        top.Visible = false;
                        bottom.Visible = false;
                        STop1.Visible = true;
                        SLeft1.Visible = true;
                    }
                    else
                    {

                        top.Visible = true;
                        bottom.Visible = true;
                        STop1.Visible = false;
                        SLeft1.Visible = false;
                    }


                    this.rbtAddress.Items.Add(GetTran("000200", "新地址"));

                    this.rbtAddress.SelectedIndex = 0;
                    txtName.Enabled = false;
                    Txtyddh.Enabled = false;
                }

            }
            else
            {

                if (Session["UserType"] != null && Session["UserType"].ToString() != "3" && Session["EditOrderID"] == null)
                {
                    top.Visible = false;
                    bottom.Visible = false;
                    STop1.Visible = false;
                    SLeft1.Visible = false;
                    txtMemBh.Visible = true;
                    lblMemBh.Style.Value = "display:none;";
                }
                else
                {
                    if (Session["UserType"].ToString() == "2")
                    {
                        top.Visible = false;
                        bottom.Visible = false;
                        STop1.Visible = true;
                        SLeft1.Visible = true;
                    }
                    else
                    {
                        top.Visible = false;
                        bottom.Visible = false;
                        STop1.Visible = false;
                        SLeft1.Visible = false;
                    }



                }
                //邮编和姓名默认赋值进去
                MemberInfoModel mi = (MemberInfoModel)Session["mbreginfo"];
                mim = mi;

                txtMemBh.Style.Value = "display:none;";
                lblMemBh.Style.Value = "display:block;";
                txtMemBh.Text = mi.Number;
                lblMemBh.Text = mi.Number;

                this.txtName.Text = Encryption.Encryption.GetDecipherName(mi.Name);
                this.labName.Text = Encryption.Encryption.GetDecipherName(mi.Name);
                this.txtPostCode.Text = mi.PostalCode;
                this.labPostCode.Text = mi.PostalCode;
                if (mi.PostalCode.Length == 0)
                {
                    this.labPostCode.Text = "111111";
                    this.txtPostCode.Text = "111111";
                }
                this.Txtyddh.Text = mi.MobileTele;
                this.labyddh.Text = mi.MobileTele;

                txtConName.Text = Encryption.Encryption.GetDecipherName(mi.Name);
                txtOtherPhone.Text = mi.MobileTele;
                this.txtName.Attributes.Add("style", "display:none;");
                this.txtPostCode.Attributes.Add("style", "display:none;");
                this.Txtyddh.Attributes.Add("style", "display:none;");
                if (mi.CPCCode.Trim().Length > 0)
                {
                    CountryCity2.SetCityCode(mi.CPCCode);//将注册的地址设置为默认的
                }
                Txtdz.Text = mi.Address;
                GetBindAddress();
            }
            p_content2.Style.Add("display", "none");
        }

    }

    private void Translations()
    {
        Button1.Text = GetTran("007427", "确认提交");
        this.TranControls(this.ddth, new string[][] { new string[] { "000543", "邮寄" }, new string[] { "000464", "自提" } });
        this.TranControls(this.DDLSendType, new string[][] { new string[] { "007442", "服务机构收货" }, new string[] { "007443", "会员收货" } });
    }

    /// <summary>
    /// 注册过来的新会员
    /// </summary>
    private void GetBindAddress()
    {
        DataTable dt = BLL.CommonClass.CommonDataBLL.GetBindAddress(luo.MemBh);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string code = dt.Rows[i][0].ToString().Substring(0, 8);
                string addres = BLL.CommonClass.CommonDataBLL.GetAddressByCode(code);
                addres += " " + dt.Rows[i][0].ToString().Substring(8, dt.Rows[i][0].ToString().Length - 8);
                addres += " " + dt.Rows[i][2].ToString();
                addres += " " + dt.Rows[i][3].ToString();
                addres += " " + Encryption.Encryption.GetDecipherName(dt.Rows[i][4].ToString());
                addres += " " + dt.Rows[i][5].ToString();
                this.rbtAddress.Items.Add(addres);
            }
            this.panel1.Visible = true;


            if (Session["mbreginfo"] != null) //注册过来的时候，默认的是新地址
            {
                this.rbtAddress.SelectedIndex = dt.Rows.Count;
            }
            else
            {
                this.rbtAddress.SelectedIndex = 0;
            }
            if (rbtAddress.SelectedIndex != dt.Rows.Count)
            {
                string asg = this.rbtAddress.SelectedItem.Text.Trim();
                string[] addr = asg.Split(' ');
                if (addr.Length == 3)
                {
                    this.txtPostCode.Text = DAL.CommonDataDAL.GetZipCode(addr[0], addr[1], addr[2]);
                }
                else
                {
                    this.txtPostCode.Text = "";
                }
                Txtdz.Text = addr[4];
                ddth.SelectedValue = addr[6];
                DDLSendType.SelectedValue = addr[5];
                CountryCity2.SelectCountry(addr[0], addr[1], addr[2], addr[3]);
                txtConName.Text = addr[addr.Length - 2];
                txtOtherPhone.Text = addr[addr.Length - 1];
            }
            else
            {
                panel2.Style.Add("display", "");
            }


        }
        else
        {
            this.rbtAddress.Items.Add(GetTran("000200", "新地址"));

            this.rbtAddress.SelectedIndex = 0;
        }

        if (Session["mbreginfo"] == null)
        {
            ltPayMoney.Text = ((double.Parse(ltYunfei.Text) + double.Parse(ltPrice.Text)) * i).ToString("0.00");
        }
        else
        {

            ltPayMoney.Text = ((double.Parse(ltYunfei.Text) + double.Parse(ltPrice.Text)) * i).ToString("0.00");
        }
    }

    private void BindData()
    {
        string sql = "select productcode,ProductID,ProductName,PreferentialPrice,PreferentialPV,ProductImage,PreferentialPrice*proNum as totalPrice,PreferentialPV*proNum as totalPv,proNum from MemShopCart,Product where MemShopCart.proId=Product.productId and memBh='" + luo.MemBh + "' and mType=" + Session["UserType"].ToString() + " and odType=" + luo.OrderType;
        DataTable dt = DAL.DBHelper.ExecuteDataTable(sql);
        if (dt.Rows.Count == 0)
        {
            if (Session["UserType"].ToString() != "1")
            {
                Response.Redirect("ShopingList.aspx", true);
            }
            else
            {
                return;
            }
        }
        Repeater1.DataSource = dt;
        Repeater1.DataBind();

        sql = "select sum(PreferentialPrice*proNum) as TotalPriceAll,sum(PreferentialPV*proNum) as TotalPvAll,sum(proNum) as totalNum from MemShopCart,Product where MemShopCart.proId=Product.productId and memBh='" + luo.MemBh + "' and mType=" + Session["UserType"].ToString() + " and odType=" + luo.OrderType;

        DataTable dt2 = DAL.DBHelper.ExecuteDataTable(sql);

        if (dt2.Rows.Count > 0)
        {
            ltNum.Text = Convert.ToInt32(dt2.Rows[0]["totalNum"]).ToString();
            labNum.Text = ltNum.Text;
            ltPrice.Text = dt2.Rows[0]["TotalPriceAll"].ToString();
            //ltPv.Text = dt2.Rows[0]["TotalPvAll"].ToString();
            ltPayMoney.Text = (Convert.ToDouble(dt2.Rows[0]["TotalPriceAll"]) * i).ToString();
        }
    }

    private void BindRadioType()
    {
        if (luo.OrderType == 21 || luo.OrderType == 11 || luo.OrderType == 31)
        {
            lblOdType.Text = GetTran("004008", "注册报单");
        }
        else if (luo.OrderType == 12 || luo.OrderType == 22)
        {
            lblOdType.Text = GetTran("001174", "复消报单");
        }
        else if (luo.OrderType == 13 || luo.OrderType == 23)
        {
            lblOdType.Text = GetTran("008111", "升级报单");
        }

        else
        {
            lblOdType.Text = GetTran("001174", "复消报单");
        }
    }

    public double GetWeight()
    {
        object obj = DAL.DBHelper.ExecuteScalar("select isnull(sum(weight*pronum),0) as totalWeight from product p,MemShopCart m where p.productid=m.proid and memBh='" + luo.MemBh + "' and mType=" + Session["UserType"].ToString() + " and odType=" + luo.OrderType);

        return Convert.ToDouble(obj);
    }

    public double returnTotalMoney()
    {
        return double.Parse(ltPrice.Text.Trim());
    }

    /// <summary>
    /// 会员报单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (txtMemBh.Text == "")
        {
            ScriptHelper.SetAlert(Page, GetTran("000129", "会员编号不能为空"));
            AgainTime.Value = "0";
            return;
        }

        if (Txtyddh.Text.Trim() == "")
        {
            GetAlert(GetTran("006889", "移动电话不能为空！"));
            AgainTime.Value = "0";
            return;
        }

        string count = DAL.DBHelper.ExecuteScalar("select count(*) from MemShopCart where memBh='" + luo.MemBh + "' and mType=" + Session["UserType"].ToString() + " and odType=" + luo.OrderType).ToString();
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

            ofm.SendWay = Convert.ToInt32(this.DDLSendType.SelectedValue);
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
                    if (!MemberInfoDAL.IsMemberExist(txtMemBh.Text))
                    {
                        ScriptHelper.SetAlert(Page, GetTran("000725", "会员编号不存在") + "！");
                        AgainTime.Value = "0";
                        return;
                    }
                    ofm = od.GetDataModelFx(Convert.ToInt32(Session["UserType"]), luo.OrderType, out choseProList, ofm.SendWay);
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


            ofm.Type = Convert.ToInt32(this.ddth.SelectedValue);


            if (lblOdType.Text == GetTran("004008", "注册报单"))
            {
                ofm.IsAgain = 0;
            }
            else
            {
                ofm.IsAgain = 1;
            }
            ofm.OrderType = luo.OrderType;

            double yfStr = 0;

            ofm.ConCity.Country = this.CountryCity2.Country;
            ofm.ConCity.Province = this.CountryCity2.Province;
            ofm.ConCity.City = this.CountryCity2.City;
            ofm.ConCity.Xian = this.CountryCity2.Xian;
            ofm.ConAddress = Encryption.Encryption.GetEncryptionAddress(this.Txtdz.Text);
            ofm.CCPCCode = DAL.CommonDataDAL.GetCPCCode(CountryCity2.Country, CountryCity2.Province, CountryCity2.City, CountryCity2.Xian);

            yfStr = 0;

            ofm.ConTelPhone = txtOtherPhone.Text.Trim();
            ofm.ConMobilPhone = txtOtherPhone.Text.Trim(); //Txtyddh.Text.Trim();
            ofm.CarryMoney = Convert.ToDecimal(yfStr);
            ofm.ConPost = "";
            ofm.Consignee = Encryption.Encryption.GetEncryptionName(txtConName.Text.Trim());
            ofm.ConZipCode = txtPostCode.Text;

            ofm.Number = txtMemBh.Text;

            //产品总费用、年费、运费、应付总金额
            double pdtMoney = double.Parse(this.ltPrice.Text);

            double CarriageMoney = 0;//运费

            //运费类型
            if (ddth.SelectedValue == "1")//自提
            {
                ofm.CarryMoney = decimal.Parse("0.00");
                this.txtYunfei.Text = ofm.CarryMoney.ToString();
            }
            else
            {
                this.txtYunfei.Text = ofm.CarryMoney.ToString();
                ofm.TotalMoney = Convert.ToDecimal(ofm.TotalMoney) + Convert.ToDecimal(CarriageMoney);//加运费
            }
            ofm.OrderExpect = CommonDataBLL.getMaxqishu();
            ofm.StandardcurrencyMoney = ofm.TotalMoney;

            ofm.PaymentMoney = ofm.TotalMoney;
            ofm.LevelInt = 1;

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
                    p_content.Visible = false;
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
                        int val = AddOrderDataDAL.OrderPayment(ofm.StoreID, ofm.OrderID, ofm.OperateIp, 2, 3, 10, "管理员", "", 5, -1, 1, 1, "", 0, "");
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
                            ScriptManager.RegisterStartupScript(this, GetType(), "mag", "var formobj=document.createElement('form');"
                                + "formobj.action='../payserver/chosepaysj.aspx?blif=" + EncryKey.GetEncryptstr(ofm.OrderID, 1, 1) + "';" +
                                "formobj.method='post';formobj.target='_blank';document.body.appendChild(formobj); formobj.submit();location.href='../store/browsememberorders.aspx';", true);
                        }
                        else
                        {
                            //ClientScript.RegisterStartupScript(GetType(), "msg", "<script>var formobj=document.createElement('form');"
                            //    + "formobj.action='../payserver/chosepay.aspx?blif=" + EncryKey.GetEncryptstr(ofm.OrderID, 1, 1) + "';" +
                            //    "formobj.method='post';formobj.target='_blank';document.body.appendChild(formobj); formobj.submit();location.href='../store/viewfuxiao.aspx';</script>", false);
                            ScriptManager.RegisterStartupScript(this, GetType(), "mag", "var formobj=document.createElement('form');"
                                + "formobj.action='../payserver/chosepaysj.aspx?blif=" + EncryKey.GetEncryptstr(ofm.OrderID, 1, 1) + "';" +
                                "formobj.method='post';formobj.target='_blank';document.body.appendChild(formobj); formobj.submit();location.href='../store/browsememberorders.aspx';", true);
                        }
                    }
                    else
                    {
                        if (ofm.IsAgain == 0)
                        {
                            //ClientScript.RegisterStartupScript(GetType(), "msg", "<script>var formobj=document.createElement('form');"
                            //    + "formobj.action='../payserver/chosepay.aspx?blif=" + EncryKey.GetEncryptstr(ofm.OrderID, 1, 1) + "';" +
                            //    "formobj.method='post';formobj.target='_blank';document.body.appendChild(formobj); formobj.submit();location.href='../member/browsememberorders.aspx';</script>", false);
                            ScriptManager.RegisterStartupScript(this, GetType(), "mag", "var formobj=document.createElement('form');"
                                + "formobj.action='../payserver/chosepaysj.aspx?blif=" + EncryKey.GetEncryptstr(ofm.OrderID, 1, 1) + "';" +
                                "formobj.method='post';formobj.target='_blank';document.body.appendChild(formobj); formobj.submit();location.href='../member/browsememberorders.aspx';", true);
                        }
                        else
                        {
                            //ClientScript.RegisterStartupScript(GetType(), "msg", "<script>var formobj=document.createElement('form');"
                            //    + "formobj.action='../payserver/chosepay.aspx?blif=" + EncryKey.GetEncryptstr(ofm.OrderID, 1, 1) + "';" +
                            //    "formobj.method='post';formobj.target='_blank';document.body.appendChild(formobj); formobj.submit();location.href='../member/memberorder.aspx';</script>", false);
                            ScriptManager.RegisterStartupScript(this, GetType(), "mag", "var formobj=document.createElement('form');"
                                + "formobj.action='../payserver/chosepaysj.aspx?blif=" + EncryKey.GetEncryptstr(ofm.OrderID, 1, 1) + "';" +
                                "formobj.method='post';formobj.target='_blank';document.body.appendChild(formobj); formobj.submit();location.href='../member/browsememberorders.aspx';", true);
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
                    p_content.Visible = false;
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
                        int val = AddOrderDataDAL.OrderPayment(ofm.StoreID, ofm.OrderID, ofm.OperateIp, 2, 3, 10, "管理员", "", 5, -1, 1, 1, "", 0, "");
                        if (val == 0)
                        {
                            PublicClass.SendMsg(1, ofm.OrderID, "");
                            ClientScript.RegisterStartupScript(GetType(), "msg", "<script>alert('" + GetTran("000436", "注册成功") + "');location.href='../company/BrowseMemberOrders.aspx';</script>", false);
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(GetType(), "msg", "<script>alert('" + GetTran("007438", "注册报单成功，支付失败，店铺账户余额不足") + "！');location.href='../company/BrowseMemberOrders.aspx';</script>", false);
                        }
                    }
                    else if (Session["UserType"].ToString() == "2")
                    {
                        if (ofm.IsAgain == 0)
                        {
                            Response.Redirect("../payserver/chosepaysj.aspx?blif=" + EncryKey.GetEncryptstr(ofm.OrderID, 1, 1), true);
                            //ClientScript.RegisterStartupScript(GetType(), "msg", "<script>var formobj=document.createElement('form');"
                            //    + "formobj.action='../payserver/chosepaysj.aspx?blif=" + EncryKey.GetEncryptstr(ofm.OrderID, 1, 1) + "';</script>", false);// +
                               // "formobj.method='post';formobj.target='_blank';document.body.appendChild(formobj); formobj.submit();location.href='../store/browsememberorders.aspx';"

                            
                        }
                        else
                        {
                            Response.Redirect("../payserver/chosepaysj.aspx?blif=" + EncryKey.GetEncryptstr(ofm.OrderID, 1, 1), true);
                            //ClientScript.RegisterStartupScript(GetType(), "msg", "<script>var formobj=document.createElement('form');"
                            //    + "formobj.action='../payserver/chosepaysj.aspx?blif=" + EncryKey.GetEncryptstr(ofm.OrderID, 1, 1) + "';</script>" , false);//+
                               // "formobj.method='post';formobj.target='_blank';document.body.appendChild(formobj); formobj.submit();location.href='../store/viewfuxiao.aspx';"
                        }
                    }
                    else
                    {
                        if (ofm.IsAgain == 0)
                        {
                            Response.Redirect("../payserver/chosepaysj.aspx?blif=" + EncryKey.GetEncryptstr(ofm.OrderID, 1, 1), true);
                            //ClientScript.RegisterStartupScript(GetType(), "msg", "<script>var formobj=document.createElement('form');"
                            //    + "formobj.action='../payserver/chosepaysj.aspx?blif=" + EncryKey.GetEncryptstr(ofm.OrderID, 1, 1) + "';" +
                            //    "formobj.method='post';formobj.target='_blank';document.body.appendChild(formobj); formobj.submit();location.href='../membermobile/browsememberorders.aspx';</script>", false);
                        }
                        else
                        {
                            Response.Redirect("../payserver/chosepaysj.aspx?blif=" + EncryKey.GetEncryptstr(ofm.OrderID, 1, 1), true);
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

    private void GetAlert(string mess)
    {
        string str444 = @" <script language='javascript' type='text/javascript'>    
                        $(document).ready(function(){
                        $('#DivCarPop').attr('style','display:block;top:' + (document.documentElement.scrollTop + 200) + 'px'); 
                        $('#btnClose').click(function(){$('#DivCarPop').hide()});
                        });</script>";
        ltMess.Text = mess;
        ltError.Text = GetTran("007439", "错误信息提示") + "！";
        Page.RegisterClientScriptBlock("", str444);
    }

    private void GetAlert2(string mess)
    {
        string str444 = @" <script language='javascript' type='text/javascript'>    
                        $(document).ready(function(){
                        $('#DivCarPop').attr('style','display:block;top:' + (document.documentElement.scrollTop + 200) + 'px'); 
                        $('#btnClose').click(function(){window.location.href='ShopingList.aspx'});
                        });</script>";
        ltMess.Text = mess;
        ltError.Text = GetTran("007440", "报单信息提示") + "！";
        Page.RegisterClientScriptBlock("", str444);
    }

    public string GetStoreId()
    {
        return MemberOrderAgainBLL.GetStoreIdByNumber(luo.MemBh);
    }
    /// <summary>
    /// 返回应付金额
    /// </summary>
    /// <returns></returns>
    public string returnPayMoney(string yunfei)
    {
        return (double.Parse(this.ltPrice.Text) + double.Parse(yunfei)).ToString("f2");
    }
    public string returnNF()
    {
        if (Session["mbreginfo"] == null)
        {
            return "0";
        }
        else
        {
            return "30";
        }
    }
    /// <summary>
    /// 发货方式改变，地址也改变
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DDLSendType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lblOdType.Text == GetTran("004008", "注册报单"))
        {
            int selV = int.Parse(this.DDLSendType.SelectedValue);
            if (selV == 0)//公司发货给店铺
            {
                string sql = "select cpccode,storeaddress from storeinfo where storeid='" + mim.StoreID + "'";
                DataTable dt_Store = DAL.DBHelper.ExecuteDataTable(sql);
                if (dt_Store.Rows.Count > 0)
                {
                    CountryCity2.SetCityCode(dt_Store.Rows[0][0].ToString());//将注册的地址设置为默认的
                    Txtdz.Text = dt_Store.Rows[0][1].ToString();
                }
            }
            else //公司发货给会员
            {
                if (mim != null)
                {
                    CountryCity2.SetCityCode(mim.CPCCode);//将注册的地址设置为默认的
                    Txtdz.Text = mim.Address;
                }
            }
        }
        else
        {
            int selV = int.Parse(this.DDLSendType.SelectedValue);
            if (selV == 0)//公司发货给店铺
            {
                if (Session["Member"] != null)
                {
                    string sql = "select scpccode,storeaddress from storeinfo si left join memberinfo mi on si.storeid=mi.storeid where mi.number='" + luo.MemBh + "'";
                    DataTable dt_Store = DAL.DBHelper.ExecuteDataTable(sql);
                    if (dt_Store.Rows.Count > 0)
                    {
                        CountryCity2.SetCityCode(dt_Store.Rows[0][0].ToString());//将注册的地址设置为默认的
                        Txtdz.Text = dt_Store.Rows[0][1].ToString();
                    }
                }
            }
            else //公司发货给会员
            {
                if (Session["Member"] != null)
                {
                    string sql = "select cpccode,address from memberinfo where number='" + luo.MemBh + "'";
                    DataTable dt_Member = DAL.DBHelper.ExecuteDataTable(sql);
                    if (dt_Member.Rows.Count > 0)
                    {
                        CountryCity2.SetCityCode(dt_Member.Rows[0][0].ToString());//将注册的地址设置为默认的
                        Txtdz.Text = dt_Member.Rows[0][1].ToString();
                    }
                }
            }
        }
    }
    public string getShouHuoAddr()
    {
        return CountryCity2.Country + CountryCity2.Province + CountryCity2.City + CountryCity2.Xian + this.Txtdz.Text.Trim();
    }
    /// <summary>
    /// 返回公司的地址
    /// </summary>
    /// <returns></returns>
    public string returnCompanyAddr()
    {
        //返回10个6(顶点编号)的地址作为公司的地址
        DataTable o_addr = DAL.DBHelper.ExecuteDataTable("select c.country+c.province+c.city+c.xian+mi.address,mi.mobiletele,mi.number,mi.name from memberinfo mi join city c on mi.cpccode=c.cpccode where defaultnumber=1");
        if (o_addr.Rows.Count > 0)
        {
            return o_addr.Rows[0][0].ToString() + "," + o_addr.Rows[0][1].ToString() + "," + o_addr.Rows[0][2].ToString() + "," + o_addr.Rows[0][3].ToString();
        }
        return "";
    }
    protected void rbtAddress_SelectedIndexChanged(object sender, EventArgs e)
    {
        string asg = this.rbtAddress.SelectedValue;
        string[] addr = asg.Split(' ');

        //ddth.SelectedValue = addr[6];
        //DDLSendType.SelectedValue = addr[5];
        CountryCity2.SelectCountry(addr[0], addr[1], addr[2], addr[3]);
        Txtdz.Text = addr[4];
        txtConName.Text = addr[addr.Length - 2];
        txtOtherPhone.Text = addr[addr.Length - 1];
    }
    protected void txtMemBh_TextChanged(object sender, EventArgs e)
    {
        if (txtMemBh.Text == "")
        {
            ScriptHelper.SetAlert(Page, GetTran("000723", "会员编号不能为空") + "！");
            return;
        }
        if (!MemberInfoDAL.IsMemberExist(txtMemBh.Text))
        {
            ScriptHelper.SetAlert(Page, GetTran("000725", "会员编号不存在") + "！");
            txtMemBh.Text = "";
            return;
        }
        else
        {
            DataTable dt = DAL.DBHelper.ExecuteDataTable("select name,postalcode,mobiletele,cpccode,address,storeid from memberinfo where number='" + txtMemBh.Text + "'");
            this.txtName.Text = Encryption.Encryption.GetDecipherName(dt.Rows[0]["Name"].ToString());
            this.labName.Text = Encryption.Encryption.GetDecipherName(dt.Rows[0]["Name"].ToString());
            txtName.Visible = false;
            if (dt.Rows[0]["postalcode"].ToString() != "")
            {
                this.txtPostCode.Text = dt.Rows[0]["postalcode"].ToString();
                this.labPostCode.Text = dt.Rows[0]["postalcode"].ToString();
            }
            this.Txtyddh.Text = dt.Rows[0]["mobiletele"].ToString();
            this.labyddh.Text = dt.Rows[0]["mobiletele"].ToString();
            Txtyddh.Visible = false;
            BindMemberAdd(txtMemBh.Text);
        }
    }
    public void BindMemberAdd(string number)
    {
        DataTable dt = BLL.CommonClass.CommonDataBLL.GetBindAddress(number);
        this.rbtAddress.Items.Clear();
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string code = dt.Rows[i][0].ToString().Substring(0, 8);
                string addres = BLL.CommonClass.CommonDataBLL.GetAddressByCode(code);
                addres += " " + dt.Rows[i][0].ToString().Substring(8, dt.Rows[i][0].ToString().Length - 8);
                addres += " " + dt.Rows[i][2].ToString();
                addres += " " + dt.Rows[i][3].ToString();
                addres += " " + Encryption.Encryption.GetDecipherName(dt.Rows[i][4].ToString());
                addres += " " + dt.Rows[i][5].ToString();
                this.rbtAddress.Items.Add(addres);
            }
            this.panel1.Visible = true;

            rbtAddress.SelectedIndex = 0;

            rbtAddress_SelectedIndexChanged(null, null);
        }

        ltPayMoney.Text = ((double.Parse(ltYunfei.Text) + double.Parse(ltPrice.Text))).ToString("0.00");

    }
}