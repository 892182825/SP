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
using DAL;
using Model;
using BLL.other.Store;
using BLL.CommonClass;

public partial class chosepay : BLL.TranslationBase
{
    protected string loginnumber = "";
      public int bzCurrency = 0;
      public double huilv;
    protected void Page_Load(object sender, EventArgs e)
    {
        //获取标准币种
        bzCurrency = CommonDataBLL.GetStandard();
        huilv = (AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString())));
        Permissions.ThreeRedirect(Page, "../Member/" + Permissions.redirUrl);

        //AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxMemShopCart));

        if (!IsPostBack)
        {
            Translations();
            lbltype.Text = GetTran("007286", "报单支付");
            lblot.Text = GetTran("000079", "订单号");
            if (Request.QueryString["blif"] != null && Request.QueryString["blif"] != "")
            {
                string blif = Request.QueryString["blif"].ToString();
                string[] strs = EncryKey.GetDecrypt(blif);
                if (strs.Length >= 3)
                {
                    ViewState["billid"] = strs[0];


                    ViewState["dotype"] = strs[1];
                    ViewState["roletype"] = strs[2];
                    if (ViewState["billid"] != null && ViewState["dotype"] != null && ViewState["roletype"] != null)
                    {
                        if (ViewState["dotype"].ToString() == "2")
                        {
                            loadpaytype();
                            LoadData1(); //数据加载
                        }
                       
                        else
                        {
                            if (ViewState["roletype"].ToString() == "2")
                            {
                                loadpaytype();
                                LoadData1(); //数据加载
                            }
                            else
                            {
                                DataTable dt_one = DAL.DBHelper.ExecuteDataTable("select ordertype from memberorder where OrderID=" + strs[0]);
                                string ordertype = dt_one.Rows[0]["ordertype"].ToString();//订单类型
                                loadpaytype();
                                LoadData(ordertype); //数据加载
                            }

                        }
                        ViewState["loginnumber"] = loginnumber;

                        //店铺离线支付
                        if (DocTypeTableDAL.Getpaytypeisusebyid(1, 1) || DocTypeTableDAL.Getpaytypeisusebyid(6, 5))
                        {
                            Loadmk(); //生成标识 
                        }
                    }
                    else
                        Response.Redirect("payerror1.aspx");
                }
                else
                {
                    Response.Redirect("payerror1.aspx");
                }
            }
            else
            {
                Response.Redirect("../default.aspx");
            }
            
        }
    }

    private void Translations()
    {
        this.TranControls(this.btnsure, new string[][] { new string[] { "000938", "支付" } });
        this.TranControls(this.rdostactypepaymb, new string[][] { new string[] { "007383", "服务机构周转款" }, new string[] { "007253", "服务机构订货款" } });
        this.TranControls(this.rdoisagree, new string[][] { new string[] { "007467", "我确认已收到该会员支付的上数金额" }, new string[] { "007468", "我暂未收到该会员支付的上数金额" } });
        this.TranControls(this.rdostaccount, new string[][] { new string[] { "007383", "服务机构周转款" }, new string[] { "007253", "服务机构订货款" } });
        this.TranControls(this.rdoaccounttype, new string[][] { new string[] { "006068", "现金账户" }, new string[] { "007252", "消费账户" } });
        this.TranControls(this.rdoaccounttype2, new string[][] { new string[] { "008113", "复消账户" }});
        this.TranControls(this.rdombsuregetmoney, new string[][] { new string[] { "007467", "我确认已收到该会员支付的上数金额" }, new string[] { "007468", "我暂未收到该会员支付的上数金额" } });
        this.TranControls(this.rdoaccounttype3, new string[][] { new string[] { "008114", "复消提货账户" } });

    }

    /// <summary>
    /// 根据公司设置使用的支付方式显示
    /// </summary>
    public void loadpaytype()
    {
        if (ViewState["roletype"] != null && ViewState["roletype"].ToString() == "1")
        {
            //账户支付
            if (DocTypeTableDAL.Getpaytypeisusebyid(2, 5)) this.div_1.Style.Add("display", "");
            //店铺支付
            if (DocTypeTableDAL.Getpaytypeisusebyid(1, 5)) this.div_2.Style.Add("display", "");
            //环迅支付
            if (DocTypeTableDAL.Getpaytypeisusebyid(5, 5))
            {
                this.div_3.Style.Add("display", ""); hxpayspan.Style.Add("display", ""); hxpic.Style.Add("display", ""); rdohxpay.Checked = true;
            }
            else { rdohxpay.Visible = false; rdohxpay.Checked = false; }      //快钱支付

            if (DocTypeTableDAL.Getpaytypeisusebyid(4, 5))
            {
                this.div_3.Style.Add("display", "");
                qkpayspan.Style.Add("display", "");
                qkpic.Style.Add("display", "");
                rdoquickpay.Checked = true;
            }
            else { rdoquickpay.Visible = false; rdoquickpay.Checked = false; }
            //支付宝支付
            if (DocTypeTableDAL.Getpaytypeisusebyid(3, 5))
            {
                this.div_3.Style.Add("display", "");
                rdoaliypay.Checked = true;
                alipayspan.Style.Add("display", ""); appic.Style.Add("display", "");
            }
            else
            {
                rdoaliypay.Visible = false;
                rdoaliypay.Checked = false;
            }
            if (DocTypeTableDAL.Getpaytypeisusebyid(6, 5))
            {
                this.div_4.Style.Add("display", "");

            }
            if (DocTypeTableDAL.Getpaytypeisusebyid(7, 5))
            {
                this.div_3.Style.Add("display", "");
                rdosft.Checked = true;
                sftspan.Style.Add("display", ""); sftpic.Style.Add("display", "");

            }
            //是否启用网银支付
            if (DocTypeTableDAL.Getpaytypeisusebyid(1, 9))
            {
                this.div_3.Style.Add("display", "");
                banklist.Style.Add("display", "");
                if (!(DocTypeTableDAL.Getpaytypeisusebyid(3, 5) || DocTypeTableDAL.Getpaytypeisusebyid(4, 5) || DocTypeTableDAL.Getpaytypeisusebyid(5, 5)))
                {
                    rdoapbankicbc.Checked = true;
                    if (DocTypeTableDAL.Getpaytypeisusebyid(3, 8))
                    {
                        //盛付通特有银行
                        RadioHXB.Visible = true;
                        RadioBOS.Visible = true;
                        RadioCBHB.Visible = true;
                        RadioHKBCHINA.Visible = true;
                        RadioGZCB.Visible = true;
                        RadioHKBEA.Visible = true;
                        RadioNJCB.Visible = true;
                        RadioWZCB.Visible = true;
                        //RadioSDE.Visible = true;
                        //RadioZHNX.Visible = true;
                        //RadioYDXH.Visible = true;
                        RadioCZCB.Visible = true;
                        RadioGNXS.Visible = true;
                        RadioPSBC.Visible = true;
                        RadioSDB.Visible = true;

                        RadioHXB1.Visible = true;
                        RadioBOS1.Visible = true;
                        RadioCBHB1.Visible = true;
                        RadioHKBCHINA1.Visible = true;
                        RadioGZCB1.Visible = true;
                        RadioHKBEA1.Visible = true;
                        RadioNJCB1.Visible = true;
                        RadioWZCB1.Visible = true;
                        //RadioSDE1.Visible = true;
                        //RadioZHNX1.Visible = true;
                        //RadioYDXH1.Visible = true;
                        RadioCZCB1.Visible = true;
                        RadioGNXS1.Visible = true;
                        RadioPSBC1.Visible = true;
                        RadioSDB1.Visible = true;
                    }

                    else if (DocTypeTableDAL.Getpaytypeisusebyid(1, 8))
                    {
                        RadioBJBANK.Visible = true;
                        RadioPSBC.Visible = true;
                        RadioFDB.Visible = true;
                        RadioSDB.Visible = true;

                        RadioSDB1.Visible = true;
                        RadioFDB1.Visible = true;
                        RadioPSBC1.Visible = true; RadioBJBANK1.Visible = true;

                    }
                }
            }
        }
        else if (ViewState["roletype"] != null && ViewState["roletype"].ToString() == "2")
        {
            //账户支付
            if (DocTypeTableDAL.Getpaytypeisusebyid(2, 1)) this.div_6.Style.Add("display", "");

            //环迅支付
            if (DocTypeTableDAL.Getpaytypeisusebyid(5, 1))
            {
                this.div_3.Style.Add("display", ""); hxpayspan.Style.Add("display", ""); hxpic.Style.Add("display", ""); rdohxpay.Checked = true;
            }
            else { rdohxpay.Visible = false; rdohxpay.Checked = false; }      //快钱支付

            if (DocTypeTableDAL.Getpaytypeisusebyid(4, 1))
            {
                this.div_3.Style.Add("display", "");
                qkpayspan.Style.Add("display", "");
                qkpic.Style.Add("display", "");
                rdoquickpay.Checked = true;
            }
            else { rdoquickpay.Visible = false; rdoquickpay.Checked = false; }
            //支付宝支付
            if (DocTypeTableDAL.Getpaytypeisusebyid(3, 1))
            {
                this.div_3.Style.Add("display", "");
                rdoaliypay.Checked = true;
                alipayspan.Style.Add("display", ""); appic.Style.Add("display", "");
            }
            else
            {
                rdoaliypay.Visible = false;
                rdoaliypay.Checked = false;
            }
            if (DocTypeTableDAL.Getpaytypeisusebyid(3, 1)) //盛付通
            {
                this.div_3.Style.Add("display", "");
                rdosft.Checked = true;
                sftspan.Style.Add("display", ""); sftpic.Style.Add("display", "");
            }
            else
            {
                rdosft.Visible = false;
                rdosft.Checked = false;
            }

            //店铺离线支付
            if (DocTypeTableDAL.Getpaytypeisusebyid(1, 1))
            {
                this.div_4.Style.Add("display", "");

            }
            //是否启用网银支付
            if (DocTypeTableDAL.Getpaytypeisusebyid(1, 9))
            {
                this.div_3.Style.Add("display", "");
                banklist.Style.Add("display", "");
                if (!(DocTypeTableDAL.Getpaytypeisusebyid(3, 1) || DocTypeTableDAL.Getpaytypeisusebyid(4, 1) || DocTypeTableDAL.Getpaytypeisusebyid(5, 1)))
                {
                    rdoapbankicbc.Checked = true;
                    if (DocTypeTableDAL.Getpaytypeisusebyid(3, 8))
                    {
                        //盛付通特有银行
                        RadioHXB.Visible = true;
                        RadioBOS.Visible = true;
                        RadioCBHB.Visible = true;
                        RadioHKBCHINA.Visible = true;
                        RadioGZCB.Visible = true;
                        RadioHKBEA.Visible = true;
                        RadioNJCB.Visible = true;
                        RadioWZCB.Visible = true;
                        //RadioSDE.Visible = true;
                        //RadioZHNX.Visible = true;
                        //RadioYDXH.Visible = true;
                        RadioCZCB.Visible = true;
                        RadioGNXS.Visible = true;
                        RadioPSBC.Visible = true;
                        RadioSDB.Visible = true;
                        RadioHXB1.Visible = true;
                        RadioBOS1.Visible = true;
                        RadioCBHB1.Visible = true;
                        RadioHKBCHINA1.Visible = true;
                        RadioGZCB1.Visible = true;
                        RadioHKBEA1.Visible = true;
                        RadioNJCB1.Visible = true;
                        RadioWZCB1.Visible = true;
                        //RadioSDE1.Visible = true;
                        //RadioZHNX1.Visible = true;
                        //RadioYDXH1.Visible = true;
                        RadioCZCB1.Visible = true;
                        RadioGNXS1.Visible = true;
                        RadioPSBC1.Visible = true;
                        RadioSDB1.Visible = true;
                    }
                    else if (DocTypeTableDAL.Getpaytypeisusebyid(1, 8))
                    {
                        RadioBJBANK.Visible = true;
                        RadioPSBC.Visible = true;
                        RadioFDB.Visible = true;
                        RadioSDB.Visible = true;

                        RadioSDB1.Visible = true;
                        RadioFDB1.Visible = true;
                        RadioPSBC1.Visible = true; RadioBJBANK1.Visible = true;

                    }
                }
            }
        }
    }
    public string getsplit(string bk)
    {
        int c = 4;
        for (int i = 0; i < bk.Length; i++)
        {
            if (i % c == 0)
            {
                bk = bk.Insert(i, "  ");
                c = 6;
            }
        }
        return bk;
    }
    /// <summary>
    /// 加载数据
    /// </summary>
    public void LoadData(string ordertype)
    {
        string billid = ViewState["billid"].ToString();
        int dotype = Convert.ToInt32(ViewState["dotype"]);
        int roletype = Convert.ToInt32(ViewState["roletype"]);
        this.lblorderid.Text = billid;


        MemberOrderModel memberorder = null;
        DataTable ordergoodstable = null;
        DataTable dtcb = null;  //查询会员汇入银行 
        string paymentnumber = "";//被支付订单（汇款单）所属会员编号
        double totalmoney = 0;// 被支付订单（汇款单）总金额

        if (dotype == 1)  //订单支付 
        {
            lbltype.Text = GetTran("000907", "订单支付");
            lblot.Text = GetTran("000079", "订单号");

            if (ordertype == "22"||ordertype =="12")
            {

                div_2.Style.Add("display", "none");
                div_3.Style.Add("display", "none");
                div_4.Style.Add("display", "none");
                div_5.Style.Add("display", "none");
                div_6.Style.Add("display", "none");
                rdoaccounttype.Style.Add("display", "block");
                rdoaccounttype3.Style.Add("display", "none");
                rdoaccounttype2.Style.Add("display", "block");
                p1.Style.Add("display", "block");
                p2.Style.Add("display", "block");
                p3.Style.Add("display", "none");
            }
            else if (ordertype == "25")
            {
                div_2.Style.Add("display", "none");
                div_3.Style.Add("display", "none");
                div_4.Style.Add("display", "none");
                div_5.Style.Add("display", "none");
                div_6.Style.Add("display", "none");
                rdoaccounttype.Style.Add("display", "none");
                rdoaccounttype3.Style.Add("display", "block");
                rdoaccounttype2.Style.Add("display", "none");
                p1.Style.Add("display", "none");
                p2.Style.Add("display", "none");
                p3.Style.Add("display", "block");
            }
            else if (ordertype == "11" || ordertype == "21" || ordertype == "31" || ordertype == "13" || ordertype == "23" || ordertype == "33")
            {
                div_3.Style.Add("display", "none");
            }

            // if (ordertype == "11" || ordertype == "21" || ordertype == "31")
            //{
            //    rdoaccounttype.Items.RemoveAt(1);
            //}

        }
        else if (dotype == 2)//充值
        {
            div_1.Style.Add("display", "none");
            div_2.Style.Add("display", "none");

            div_5.Style.Add("display", "none");
            div_6.Style.Add("display", "none");
            lbltype.Text = GetTran("007451", "订单支付");
            lblot.Text = GetTran("005854", "汇款单号");
            ViewState["remid"] = billid;

            RemittancesModel remittance = RemittancesDAL.GetRemitByHuidan(billid);
            paymentnumber = remittance.RemitNumber.ToString();//被支付订单（汇款单）所属会员编号
            totalmoney = Convert.ToDouble(remittance.RemitMoney)*huilv;// 被支付订单（汇款单）总金额
        }


        if (roletype == 1)  //会员
        {
            //隐藏店铺操作
            div_6.Style.Add("display", "none");
            div_5.Style.Add("display", "none");

            if (Session["Member"] != null)
                loginnumber = Session["Member"].ToString();
            else if (Session["Store"] != null)
            {
                loginnumber = Session["Store"].ToString();
                //店铺支付会员订单
                div_1.Style.Add("display", "none");
                div_2.Style.Add("display", "none");
                div_3.Style.Add("display", "none");
                div_4.Style.Add("display", "none");
                div_6.Style.Add("display", "none");
                div_5.Style.Add("display", "");
            }


            if (dotype == 1)  //订单支付 
            {
                if (MemberOrderDAL.Getvalidteiscanpay(billid, loginnumber))//限制订单必须有订货所属店铺中心支付)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script> alert('" + GetTran("007452", "该订单不属于您的协助或推荐报单，不能完成支付") + "'); window.location.href='../Logout.aspx'; </script>");

                    return;
                }
                memberorder = MemberOrderDAL.GetMemberOrder(billid);
                if (loginnumber == "") loginnumber = memberorder.Number;
                else ViewState["odnumber"] = memberorder.Number;
                if (memberorder.Number == loginnumber)//如果是自己支付自己的订单则不需要确认收到款
                { div_sure.Visible = false; rdombsuregetmoney.Visible = false; }
                if (memberorder == null)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script> window.location.href='payerror1.aspx?ef=" + EncryKey.Encrypt("1").ToLower() + "';</script>");
                    return;
                }
                if (memberorder.DefrayState == 1)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>window.location.href='payerror1.aspx?ef=" + EncryKey.Encrypt("2").ToLower() + "';</script>");
                    return;
                }
                paymentnumber = memberorder.Number.ToString();//被支付订单（汇款单）所属会员编号
                totalmoney = Convert.ToDouble(memberorder.TotalMoney)*huilv;// 被支付订单（汇款单）总金额

            }
            dtcb = CompanyBankDAL.getdtcompanybankbynumber(loginnumber, 1);

        }
        else
            if (roletype == 2)  //店铺
        {
            //隐藏店铺操作
            div_2.Style.Add("display", "none");
            div_1.Style.Add("display", "none");
            div_5.Style.Add("display", "none");
            loginnumber = Session["Store"].ToString();
            dtcb = CompanyBankDAL.getdtcompanybankbynumber(loginnumber, 2);
            if (dotype == 1)  //订单操作
            {
                ordergoodstable = OrderDetailDAL.Getordergoodstablebyorderid(billid);
                if (ordergoodstable != null && ordergoodstable.Rows.Count > 0)
                {
                    if (ordergoodstable.Rows[0]["IsCheckOut"].ToString() == "Y")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>window.location.href='payerror1.aspx?ef=" + EncryKey.Encrypt("2").ToLower() + "';</script>");
                        return;
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script> window.location.href='payerror1.aspx?ef=" + EncryKey.Encrypt("1").ToLower() + "';</script>");
                    return;
                }
                paymentnumber = ordergoodstable.Rows[0]["storeid"].ToString();//被支付订单（汇款单）所属会员编号
                totalmoney = Convert.ToDouble(ordergoodstable.Rows[0]["totalmoney"])*huilv;// 被支付订单（汇款单）总金额

            }
        }
        lblstoreid1.Text = loginnumber;
        lblstoreID2.Text = loginnumber;
        lblordernumber.Text = loginnumber;
        lbltotalmoney.Text = totalmoney.ToString("0.00");
        lblordertmoney.Text = totalmoney.ToString("0.00");
        ViewState["tm"] = totalmoney;
        string cardstr = "";
        int i = 1;
        if (dtcb != null && dtcb.Rows.Count > 0)
        {
            foreach (DataRow item in dtcb.Rows)
            {
                cardstr += " <div   id='bank" + i.ToString() + "'><div><span style='margin-left:16.5px'>" + GetTran("007506", "账") + "" + GetTran("007453", "号") + "：<span><span>" + getsplit(item["BankBook"].ToString()) + "</span></div><div>" + GetTran("001243", "开户行") + "：<span>" + item["Bank"].ToString() + "</span></div><div>" + GetTran("000086", "开户名") + "：<span>" + item["Bankname"].ToString() + "</span></div></div>"; i++;
               // cardstr += " <div   id='bank" + i.ToString() + "'>" + GetTran("001243", "开户行") + "：&nbsp;&nbsp;&nbsp;&nbsp;" + item["Bank"].ToString() + "<br/>&nbsp;&nbsp;&nbsp;&nbsp;" + GetTran("007506", "账") + "&nbsp;&nbsp;&nbsp;" + GetTran("007453", "号") + "：&nbsp;&nbsp;&nbsp;&nbsp;" + getsplit(item["BankBook"].ToString()) + "<br/>&nbsp;&nbsp;&nbsp;&nbsp;" + GetTran("000086", "开户名") + "：&nbsp;&nbsp;&nbsp;&nbsp;" + item["Bankname"].ToString() + "</div>"; i++;
            }
            this.cardlist.InnerHtml = cardstr;//绑定银行信息
        }

    }

    public void LoadData1()
    {
        string billid = ViewState["billid"].ToString();
        int dotype = Convert.ToInt32(ViewState["dotype"]);
        int roletype = Convert.ToInt32(ViewState["roletype"]);
        this.lblorderid.Text = billid;


        MemberOrderModel memberorder = null;
        DataTable ordergoodstable = null;
        DataTable dtcb = null;  //查询会员汇入银行 
        string paymentnumber = "";//被支付订单（汇款单）所属会员编号
        double totalmoney = 0;// 被支付订单（汇款单）总金额

        if (dotype == 1)  //订单支付 
        {
            lbltype.Text = GetTran("000907", "订单支付");
            lblot.Text = GetTran("000079", "订单号");
        }
        else if (dotype == 2)//充值
        {
            div_1.Style.Add("display", "none");
            div_2.Style.Add("display", "none");

            div_5.Style.Add("display", "none");
            div_6.Style.Add("display", "none");
            lbltype.Text = GetTran("007451", "订单支付");
            lblot.Text = GetTran("005854", "汇款单号");
            ViewState["remid"] = billid;

            RemittancesModel remittance = RemittancesDAL.GetRemitByHuidan(billid);
            paymentnumber = remittance.RemitNumber.ToString();//被支付订单（汇款单）所属会员编号
            totalmoney = Convert.ToDouble(remittance.RemitMoney)*huilv;// 被支付订单（汇款单）总金额
        }


        if (roletype == 1)  //会员
        {
            //隐藏店铺操作
            div_6.Style.Add("display", "none");
            div_5.Style.Add("display", "none");

            if (Session["Member"] != null)
                loginnumber = Session["Member"].ToString();
            else if (Session["Store"] != null)
            {
                loginnumber = Session["Store"].ToString();
                //店铺支付会员订单
                div_1.Style.Add("display", "none");
                div_2.Style.Add("display", "none");
                div_3.Style.Add("display", "none");
                div_4.Style.Add("display", "none");
                div_6.Style.Add("display", "none");
                div_5.Style.Add("display", "");
            }


            if (dotype == 1)  //订单支付 
            {
                if (MemberOrderDAL.Getvalidteiscanpay(billid, loginnumber))//限制订单必须有订货所属店铺中心支付)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script> alert('" + GetTran("007452", "该订单不属于您的协助或推荐报单，不能完成支付") + "'); window.location.href='../Logout.aspx'; </script>");

                    return;
                }
                memberorder = MemberOrderDAL.GetMemberOrder(billid);
                if (loginnumber == "") loginnumber = memberorder.Number;
                else ViewState["odnumber"] = memberorder.Number;
                if (memberorder.Number == loginnumber)//如果是自己支付自己的订单则不需要确认收到款
                { div_sure.Visible = false; rdombsuregetmoney.Visible = false; }
                if (memberorder == null)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script> window.location.href='payerror1.aspx?ef=" + EncryKey.Encrypt("1").ToLower() + "';</script>");
                    return;
                }
                if (memberorder.DefrayState == 1)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>window.location.href='payerror1.aspx?ef=" + EncryKey.Encrypt("2").ToLower() + "';</script>");
                    return;
                }
                paymentnumber = memberorder.Number.ToString();//被支付订单（汇款单）所属会员编号
                totalmoney = Convert.ToDouble(memberorder.TotalMoney)*huilv;// 被支付订单（汇款单）总金额

            }
            dtcb = CompanyBankDAL.getdtcompanybankbynumber(loginnumber, 1);

        }
        else
            if (roletype == 2)  //店铺
        {
            //隐藏店铺操作
            div_2.Style.Add("display", "none");
            div_1.Style.Add("display", "none");
            div_5.Style.Add("display", "none");
            loginnumber = Session["Store"].ToString();
            dtcb = CompanyBankDAL.getdtcompanybankbynumber(loginnumber, 2);
            if (dotype == 1)  //订单操作
            {
                ordergoodstable = OrderDetailDAL.Getordergoodstablebyorderid(billid);
                if (ordergoodstable != null && ordergoodstable.Rows.Count > 0)
                {
                    if (ordergoodstable.Rows[0]["IsCheckOut"].ToString() == "Y")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>window.location.href='payerror1.aspx?ef=" + EncryKey.Encrypt("2").ToLower() + "';</script>");
                        return;
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script> window.location.href='payerror1.aspx?ef=" + EncryKey.Encrypt("1").ToLower() + "';</script>");
                    return;
                }
                paymentnumber = ordergoodstable.Rows[0]["storeid"].ToString();//被支付订单（汇款单）所属会员编号
                totalmoney = Convert.ToDouble(ordergoodstable.Rows[0]["totalmoney"])*huilv;// 被支付订单（汇款单）总金额

            }
        }
        lblstoreid1.Text = loginnumber;
        lblstoreID2.Text = loginnumber;
        lblordernumber.Text = loginnumber;
        lbltotalmoney.Text = totalmoney.ToString("0.00");
        lblordertmoney.Text = totalmoney.ToString("0.00");
        ViewState["tm"] = totalmoney;
        string cardstr = "";
        int i = 1;
        if (dtcb != null && dtcb.Rows.Count > 0)
        {
            foreach (DataRow item in dtcb.Rows)
            {
                cardstr += " <div   id='bank" + i.ToString() + "'><div>" + GetTran("001243", "开户行") + "：" + item["Bank"].ToString() + "</div><div>" + GetTran("007506", "账") + "&nbsp;&nbsp;&nbsp;&nbsp;" + GetTran("007453", "号") + "：" + getsplit(item["BankBook"].ToString()) + "</div><div>" + GetTran("000086", "开户名") + "：" + item["Bankname"].ToString() + "</div></div>"; i++;
            }
            this.cardlist.InnerHtml = cardstr;//绑定银行信息
        }

    }

    /// <summary>
    /// 获取标示码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Loadmk()
    {
        string billid = ViewState["billid"].ToString();
        int dotype = Convert.ToInt32(ViewState["dotype"]);
        int roletype = Convert.ToInt32(ViewState["roletype"]);
          double currency = AjaxClass.GetCurrency(int.Parse(Session["Default_Currency"] == null ? bzCurrency.ToString() : Session["Default_Currency"].ToString())); 
        string ip = Request.UserHostAddress.ToString();
        string remark = "";
        MemberOrderModel memberorder = null;
        DataTable ordergoodstable = null;
        double ordertmoney = 0;
        if (roletype == 1)  //会员订单
        {
            if (dotype == 1)
            {
                memberorder = MemberOrderDAL.GetMemberOrder(billid);
                ordertmoney = Convert.ToDouble(memberorder.TotalMoney);
            }
        }
        else
            if (roletype == 2)  //店铺
        {
            if (dotype == 1)
            {
                ordergoodstable = OrderDetailDAL.Getordergoodstablebyorderid(billid);
                if (ordergoodstable != null && ordergoodstable.Rows.Count > 0)
                    ordertmoney = Convert.ToDouble(ordergoodstable.Rows[0]["totalmoney"]);
            }
        }
        string biaoshi = "";
        if (dotype == 1)  //订单支付生成尾数
        {

            remark = loginnumber + "汇款支付订单" + billid;
            int c = 1; //数据库中1代表会员 0代表店铺
            if (roletype == 2) c = 0;
            string rmid = RemittancesDAL.GetAddnewRemattice(loginnumber, ordertmoney, ip, billid, remark, c);
            ViewState["remid"] = rmid;
            DataTable dt = DBHelper.ExecuteDataTable("select totalrmbmoney ,totalmoney from  remtemp where remittancesid='" + rmid + "'");

            if (dt != null && dt.Rows.Count > 0)
            {
                biaoshi =(Convert.ToDouble(dt.Rows[0]["totalmoney"])*huilv).ToString("f2");
            }
        }
        else
            if (dotype == 2) //汇款充值生成尾数
        {

            RemittancesDAL.GetAddnewRetmp(loginnumber, billid, ip, remark, roletype);
            DataTable dt = DBHelper.ExecuteDataTable("select totalrmbmoney ,totalmoney from  remtemp where remittancesid='" + billid + "'");  //查找出标识

            if (dt != null && dt.Rows.Count > 0)
            {
                biaoshi = (Convert.ToDouble(dt.Rows[0]["totalmoney"])*huilv).ToString("f2");

            }

        }
        if (biaoshi != "" && biaoshi.ToString().IndexOf('.') > 0)
        {
            //lblpartmoney.Text = biaoshi.Substring(0, biaoshi.IndexOf('.'));
            //string chart = biaoshi.Substring(biaoshi.IndexOf('.') + 1);
            //this.lblmoneyre.Text = biaoshi;
            //this.lblrmb.Text = biaoshi;
            //lblchat.Text = chart;
            //lbljiao.Text = chart.Substring(0, 1);
            //lblfen.Text = chart.Substring(1);
            DataTable dt = DBHelper.ExecuteDataTable("select totalrmbmoney ,totalmoney from  remtemp where remittancesid='" + billid + "'");  //查找出标识
            string bb = (double.Parse(biaoshi)).ToString();
            //lblpartmoney.Text = (bb).Substring(0, (bb).IndexOf('.'));
            //string chart = bb.Substring(bb.IndexOf('.') + 1);
            this.lblmoneyre.Text = (double.Parse(bb)).ToString();
            this.lblrmb.Text = ((double.Parse(biaoshi))).ToString("#0.00");
            //lblchat.Text = (double.Parse(chart) ).ToString(); ;
            //lbljiao.Text = chart.Substring(0, 1);
            //lblfen.Text = chart.Substring(1);

        }

    }
    /// <summary>
    /// 在线支付转向路径
    /// </summary>
    /// <param name="money"></param>
    /// <returns></returns>
    public string Getposturl(string hkid)
    {
        int dotype = Convert.ToInt32(ViewState["dotype"]);
        int roletype = Convert.ToInt32(ViewState["roletype"]);
        double totalmoney = Convert.ToDouble(ViewState["tm"])*huilv;
        string posturl = "";
        string banklcode = "";

        if (rdoaliypay.Checked)
        {
            posturl = "payment/Default.aspx?total_fee=" + totalmoney + "&out_trade_no=" + hkid;

        }
        else
            if (rdoquickpay.Checked)  //快钱支付
        {
            posturl = "quickPay/sendpki.aspx?totalmoney=" + totalmoney + "&hkid=" + hkid;
        }
        else
                if (rdohxpay.Checked) //环迅支付
        {
            posturl = "huanxun/redirect.aspx?totalmoney=" + totalmoney + "&hkid=" + hkid;
        }
        else
                    if (rdosft.Checked) //盛付通支付
        {
            posturl = "shengpay/SendOrderSFT.aspx?totalmoney=" + totalmoney + "&hkid=" + hkid;
        }
        else
        {

            //网银充入接口 支付宝
            if (DocTypeTableDAL.Getpaytypeisusebyid(1, 8))
            {
                banklcode = Getbankcode(1);
                posturl = "payment/Default.aspx?total_fee=" + totalmoney + "&out_trade_no=" + hkid + "&defaultbank=" + banklcode;
            }
            else if (DocTypeTableDAL.Getpaytypeisusebyid(2, 8)) //快钱
            {
                banklcode = Getbankcode(2);
                posturl = "quickPay/sendpki.aspx?totalmoney=" + totalmoney + "&hkid=" + hkid + "&defaultbank=" + banklcode;
            }
            else if (DocTypeTableDAL.Getpaytypeisusebyid(3, 8)) //盛付通
            {
                banklcode = Getbankcode(3);
                posturl = "shengpay/SendOrderSFT.aspx?totalmoney=" + totalmoney + "&hkid=" + hkid + "&defaultbank=" + banklcode;
            }
        }
        return posturl;
    }
    /// <summary>
    /// 支付按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnsure_Click(object sender, EventArgs e)
    {
        int res = -1;
        string usemoney = lbltotalmoney.Text.Trim();
        string billid = ViewState["billid"].ToString();
        int roletype = Convert.ToInt32(ViewState["roletype"]);
        int dotype = Convert.ToInt32(ViewState["dotype"]);

        loginnumber = ViewState["loginnumber"].ToString();

        string curip = Request.UserHostAddress.ToString();
        if (!rdorempay.Checked)  //非离线支付 
        {
            if (ViewState["remid"] != null)
            {
                RemittancesDAL.DelRemittancesrelationremtemp(ViewState["remid"].ToString());
            }
        }
        else //使用离线支付方式
        {
            if (ViewState["remid"] != null)
            {
                RemittancesDAL.UPRemittancesre(ViewState["remid"].ToString());
            }
        }

        if (rdoonlinepay.Checked)  //在线支付 
        {
            string hkid = billid;
            ClientScript.RegisterStartupScript(GetType(), "msg", "alert('该功能正在开发中，请耐心等候！！！');", true);
            return;
            if (dotype == 1)
                hkid = RemittancesDAL.AddRemittancebytypeOnline(billid, roletype, curip, loginnumber, 1);
            else if (dotype == 2)
                RemittancesDAL.UpdateOnlinepayway(billid, 4);

            string posturl = Getposturl(hkid);

            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>window.open ('" + posturl + "');</script>");
            this.btnsure.Enabled = false;
            return;
        }
        else if (rdorempay.Checked)  //离线支付 
        {
            usemoney = lblrmb.Text.Trim();
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script> window.location.href='payerror1.aspx?ef=" + EncryKey.Encrypt("100" + "," + billid + "," + usemoney) + "';</script>");
            return;
        }
        else if (rdostorepay.Checked)  //去店铺支付 
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script> window.location.href='payerror1.aspx?ef=" + EncryKey.Encrypt("101" + "," + billid + "," + usemoney) + "';</script>");
            return;
        }
        else if (rdoectpay.Checked)  //会员电子货币支付 
        {
            if (MemberOrderDAL.Getvalidteiscanpay(billid, loginnumber))//限制订单必须有订货所属店铺推荐人协助人支付)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script> alert('" + GetTran("007452", "该订单不属于您的协助或推荐报单，不能完成支付！") + "'); window.location.href='../Logout.aspx'; </script>");

                return;
            }
            if (ViewState["odnumber"].ToString() != loginnumber)//如果不是自己给自己支付
            {
                if (this.rdombsuregetmoney.SelectedValue == "0")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script> alert('" + GetTran("007455", "请确认已收到该会员支付的报单金额") + "');   </script>");

                    return;
                }
            }
            if (this.txtadvpass.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("006656", "二级密码不能为空！") + "');</script>");

                return;
            }
            string oldPass = Encryption.Encryption.GetEncryptionPwd(this.txtadvpass.Text.ToString(), loginnumber);
            int n = PwdModifyBLL.check(loginnumber, oldPass, 1);
            if (n <= 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script> alert('" + GetTran("006058", "二级密码不正确！") + "'); </script>");

                return;
            }

            if (MemberInfoDAL.CheckState(Session["Member"].ToString()))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script> alert('" + GetTran("007456", "会员账户已冻结，不能完成支付!") + "'); </script>");

                return;
            }


            DataTable dt_one = DAL.DBHelper.ExecuteDataTable("select ordertype from MemberOrder where OrderID=" + billid);
            string ordertype = dt_one.Rows[0]["ordertype"].ToString();//订单类型
            int act;
            if (ordertype == "22"||ordertype=="12")
            {
                act = Convert.ToInt32(rdoaccounttype2.SelectedValue);

            }
            else if (ordertype == "25")
            {
                act = Convert.ToInt32(rdoaccounttype3.SelectedValue);

            }
            else
            {

                act = Convert.ToInt32(rdoaccounttype.SelectedValue);
            }

            res = AddOrderDataDAL.OrderPayment(loginnumber, billid, curip, roletype, dotype, act, loginnumber, "", 2, -1, 1, 1, "", 0, "");
            this.btnsure.Enabled = false;
        }
        else if (rdostpaymb.Checked)  //店铺支付 会员订单
        {
            if (MemberOrderDAL.Getvalidteiscanpay(billid, loginnumber))//限制订单必须有订货所属店铺推荐人协助人支付)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script> alert('" + GetTran("007452", "该订单不属于您的协助或推荐报单，不能完成支付！") + "'); window.location.href='../Logout.aspx'; </script>");
                return;
            }
            if (this.rdoisagree.SelectedValue == "0") //验证是否确认收到款
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script> alert('" + GetTran("007455", "请确认已收到该会员支付的报单金额") + "！');   </script>");
                return;
            }
            if (this.txtpayadbpass.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("006656", "二级密码不能为空！") + "');</script>");
                return;
            }
            string oldPass = Encryption.Encryption.GetEncryptionPwd(this.txtpayadbpass.Text.ToString(), Session["Store"].ToString());
            int n = PwdModifyBLL.checkstore(loginnumber, oldPass, 1);
            if (n <= 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script> alert('" + GetTran("006058", "二级密码不正确！") + "'); </script>");
                return;
            }

            int act = Convert.ToInt32(rdostactypepaymb.SelectedValue);
            res = AddOrderDataDAL.OrderPayment(loginnumber, billid, curip, 2, 3, act, loginnumber, "", 5, -1, 1, 1, "", 0, "");
            this.btnsure.Enabled = false;
        }
        else if (rdostopayorder.Checked)  //店铺电子账户支付 订货单
        {
            if (this.txtstadvpass.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("006656", "二级密码不能为空！") + "');</script>");

                return;
            }
            string oldPass = Encryption.Encryption.GetEncryptionPwd(this.txtstadvpass.Text.ToString(), Session["Store"].ToString());
            int n = PwdModifyBLL.checkstore(loginnumber, oldPass, 1);
            if (n <= 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script> alert('" + GetTran("006058", "二级密码不正确！") + "'); </script>");

                return;
            }
            int act = Convert.ToInt32(rdostaccount.SelectedValue);

            res = AddOrderDataDAL.OrderPayment(loginnumber, billid, curip, roletype, dotype, act, loginnumber, "", 2, -1, 1, 1, "", 0, ""); this.btnsure.Enabled = false;
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script> alert('" + GetTran("000000", "请至少选择一种支付方式！") + "'); </script>");
            return;
        }

        PublicClass.SendMsg(1, billid, "");

        ClientScript.RegisterStartupScript(this.GetType(), "", "<script> window.location.href='payerror1.aspx?ef=" + EncryKey.Encrypt(res.ToString() + "," + billid + "," + usemoney) + "';</script>");
        return;
    }
    /// <summary>
    /// 获取使用的银行代码
    /// </summary>
    /// <param name="usetype">1 支付宝   2 快钱 3盛付通</param>
    /// <returns></returns>
    public string Getbankcode(int usetype)
    {
        string bankcode = "";
        string hdcode = this.hidbankName.Value;

        switch (hdcode)
        {
            case "ICBCB2C":
                if (usetype == 1)
                    bankcode = hdcode;
                else if (usetype == 2)
                    bankcode = "ICBC";
                else if (usetype == 3)
                    bankcode = "ICBC";
                break;
            case "BOCB2C":
                if (usetype == 1)
                    bankcode = hdcode;
                else if (usetype == 2)
                    bankcode = "BOC";
                else if (usetype == 3)
                    bankcode = "";
                break;
            case "COMM":
                if (usetype == 1)
                    bankcode = hdcode;
                else if (usetype == 2)
                    bankcode = "BCOM";
                else if (usetype == 3)
                    bankcode = hdcode;
                break;
            case "SPABANK":
                if (usetype == 1)
                    bankcode = hdcode;
                else if (usetype == 2)
                    bankcode = "PAB";
                else if (usetype == 3)
                    bankcode = "SZPAB";
                break;
            case "NBBANK":
                if (usetype == 1)
                    bankcode = hdcode;
                else if (usetype == 2)
                    bankcode = "NBCB";
                else if (usetype == 3)
                    bankcode = "NBCB";
                break;
            case "HZCBB2C":
                if (usetype == 1)
                    bankcode = hdcode;
                else if (usetype == 2)
                    bankcode = "HZB";
                else if (usetype == 3)
                    bankcode = "HCCB";
                break;
            case "BJBANK":
                if (usetype == 1)
                    bankcode = hdcode;
                else if (usetype == 2)
                    bankcode = "BOB";
                else if (usetype == 3)
                    bankcode = "";
                break;
            case "SHRCB":
                if (usetype == 1)
                    bankcode = hdcode;
                else if (usetype == 2)
                    bankcode = "SHRCC";
                else if (usetype == 3)
                    bankcode = "";
                break;
            case "PSBC-DEBIT":
                if (usetype == 1)
                    bankcode = hdcode;
                else if (usetype == 2)
                    bankcode = "";
                else if (usetype == 3)
                    bankcode = "PSBC";
                break;

            default:
                bankcode = hdcode;
                break;
        }

        return bankcode;
    }
}