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

public partial class RegisterMember : BLL.TranslationBase
{
    RegistermemberBLL registermemberBLL = new RegistermemberBLL();
    /// <summary>
    /// 页面加载事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetExpires(DateTime.Now); 
        Permissions.ThreeRedirect(Page, "../member/" + Permissions.redirUrl);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        if (!IsPostBack)
        {
            BindDate();
            //Translations();
            //Translate();
        }
    }

    public void BindDate()
    {
        CountryCity2.SelectCountry("中国", "", "", "");
        if (Session["UserType"] != null && Session["UserType"].ToString() != "3")
        {
            if (Session["UserType"].ToString() == "2")
            {
                //top.Visible = false;
                //bottom.Visible = false;
                //STop1.Visible = true;
                //SLeft1.Visible = true;
            }
            else
            {
            //    top.Visible = false;
            //    bottom.Visible = false;
            //    STop1.Visible = false;
            //    SLeft1.Visible = false;
            }
        }
        else
        {

            //top.Visible = true;
            //bottom.Visible = true;
            //STop1.Visible = false;
            //SLeft1.Visible = false;
        }

       // dplCardType.SelectedIndex = 1;

        //会员编号
        string ags = BLL.CommonClass.CommonDataBLL.GetMemberNumber();
        txtNumber.Text = ags;
        HFNumber.Value = ags;
        this.txtNumber.ReadOnly = true;
        txtDirect.Text = Session["Member"].ToString();
        hiddirect.Value = Session["Member"].ToString();
        DataTable dts = DAL.DBHelper.ExecuteDataTable("select top 1 * from config order by createdate desc");
        if (dts.Rows != null && dts.Rows.Count > 0)
        {
            tz300.Text =Convert.ToInt32(dts.Rows[0]["para1"]).ToString();
            tz3000.Text = Convert.ToInt32(dts.Rows[0]["para2"]).ToString();
            tz21000.Text = Convert.ToInt32(dts.Rows[0]["para3"]).ToString();
            hidtzmoney.Value = Convert.ToInt32(dts.Rows[0]["para1"]).ToString();
        }

    }
    public string GetCSS()
    {
        return Session["UserType"].ToString();
    }
    /// <summary>
    /// 控件翻译方法
    /// </summary>
    //public void Translate()
    //{
    //    this.TranControls(this.dplCardType, new string[][] { 
    //     new string[] { "005898", "无" },
    //     new string[] { "005776", "身份证" },
    //     new string[] { "005775", "护照" },
    //     new string[] { "005774", "港澳台证" },

    //    });
    //}

    /// <summary>
    /// 注册报单流程（包括判断）
    /// 调用逻辑层中的所有方法
    /// </summary>
    public void AddOrderAndInfoProcess()
    {
        string number = CommonDataBLL.quanjiao(HFNumber.Value.Trim());
        string direct = txtDirect.Text.Trim();
        //会员名是否小于6位
        if (!registermemberBLL.NumberLength(number))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + this.GetTran("000306", "抱歉！您输入的会员编号小于6位！") + "');</script>", false);
            return;
        }
        if (!registermemberBLL.NumberCheckAgain(number))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + this.GetTran("000309", "编号请输入字母，数字，横线！") + "');</script>", false);
            return;
        }

        string name = CommonDataBLL.quanjiao(txtName.Text.Trim());
        if (name == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + this.GetTran("008360", "真实姓名不能为空") + "！" + "');</script>", false);
            return;
        }
        string tel = CommonDataBLL.quanjiao(txtTele.Text.Trim());
        if (tel == "" || tel.Length != 11)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('手机号格式不正确！');</script>", false);
            return;

        }

        //string storeid = CommonDataBLL.quanjiao(txtStore.Text.Trim());
        //if (storeid == "")
        //{
        //    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + this.GetTran("008361", "所属店铺不能为空") + "！" + "');</script>", false);
        //    return;
        //}
        //else
        //{
        //    if (!StoreInfoDAL.CheckStoreId(storeid))
        //    {
        //        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + this.GetTran("008362", "所属店铺编号不存在！") + "');</script>", false);
        //        return;
        //    }
        //}

        //判断用胡地址是否输入
        if (this.CountryCity2.Country == "请选择" || this.CountryCity2.Province == "请选择" || this.CountryCity2.City == "请选择" || this.CountryCity2.Xian == "请选择" || this.CountryCity2.Country == "" || this.CountryCity2.Province == "" || this.CountryCity2.City == "" || this.CountryCity2.Xian == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("001548", "对不起，请选择国家省份城市！") + "');</script>", false);
            return;
        }
        if (DAL.CommonDataDAL.GetCPCCode(this.CountryCity2.Country, this.CountryCity2.Province, this.CountryCity2.City, this.CountryCity2.Xian) == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("001548", "对不起，请选择国家省份城市！") + "');</script>", false);
            return;
        }

        //string direct = CommonDataBLL.quanjiao(txtDirect.Text.Trim());

        string placement = hidplacemnet.Value;// CommonDataBLL.quanjiao(SearchPlacement_DoubleLines1.Placement);
        //if (direct == "" || placement == "")
        //{
        //    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + this.GetTran("000716", "推荐编号和安置编号不能为空！") + "');</script>", false);
        //    return;
        //}

        //if (direct == number)
        //{
        //    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + this.GetTran("006700", "推荐编号不能与会员编号相同") + "');</script>", false);
        //    return;
        //}

        if (placement == number)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + this.GetTran("001650", "安置编号不能与会员编号相同") + "');</script>", false);
            return;
        }

        //验证年龄是否大于18岁
        //string birthDate = CommonDataBLL.quanjiao(txtBirthDate.Text.Trim());
        //if (this.dplCardType.SelectedValue != "2")
        //{
        //    string alert = registermemberBLL.AgeIs18(birthDate);
        //    if (alert != null)
        //    {
        //        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + alert + "');</script>", false);
        //        return;
        //    }
        //}

        ////检查会员生日
        //if (this.dplCardType.SelectedValue != "2")
        //{
        //    if (registermemberBLL.CheckBirthDay(birthDate) == "error")
        //    {
        //        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + this.GetTran("000148", "对不起，请选择正确的出生日期！") + "');</script>", false);
        //        return;
        //    }
        //}


        ////检测身份证需要新方法
        //string CardResult = "";
        //if (this.dplCardType.SelectedValue == "2")
        //{
        //    string papernumber = CommonDataBLL.quanjiao(this.txtPapernumber.Text.Trim());
        //    string result = BLL.Registration_declarations.CheckMemberInfo.CHK_IdentityCard(papernumber);
        //    if (result.IndexOf(",") <= 0)
        //    {

        //        return;
        //    }
        //    else
        //    {
        //        CardResult = result;
        //    }
        //    DateTime birthday = Convert.ToDateTime(CardResult.Substring(0, CardResult.IndexOf(",")));
        //    string alerta = registermemberBLL.AgeIs18(birthday.ToString());
        //    if (alerta != null)
        //    {
        //        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + alerta + "');</script>", false);
        //        return;
        //    }
        //}

        //ViewState["CardResult"] = CardResult;
        //验证会员编号是否重复
        if (registermemberBLL.CheckNumberTwice(number) != null)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + this.GetTran("000406", "抱歉！该会员编号重复！") + "');</script>", false);
            if (Request.QueryString["Much"] == null)
            {
                string bb = BLL.CommonClass.CommonDataBLL.GetMemberNumber();
                this.txtNumber.Text = bb;
                this.HFNumber.Value = bb;
                this.txtNumber.ReadOnly = true;
            }
            return;
        }
        //验证手机号码是否重复
        if (registermemberBLL.CheckTeleTwice(txtTele.Text.Trim()) != null)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('抱歉！该手机号码已被注册！');</script>", false);
            return;
        }


        MemberInfoModel mi = AddUserInfo();

        if (mi.Placement != "8888888888")
        {
            if (DBHelper.ExecuteScalar("select count(0) from memberinfo where placement='" + mi.Placement + "' and District=" + mi.District + "  and  memberstate=1 ").ToString() != "0")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + this.GetTran("007433", "安置人所选区位已有人安置！") + "');</script>", false);
                return;
            }
        }



        //注册会员检错1.无上级  2.无此店  3..死循环
        string CheckMember = registermemberBLL.CheckMemberInProc(mi.Number, mi.Placement, mi.Direct, mi.StoreID);
        CheckMember = new GroupRegisterBLL().GerCheckErrorInfo(CheckMember);
        ViewState["Error"] = CheckMember;
        if (Request.QueryString["Much"] == null)
        {

            if (CheckMember != null)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + CheckMember + "');</script>", false);
                return;
            }

            string placement_check = registermemberBLL.GetHavePlacedOrDriect(mi.Number, "", mi.Placement, mi.Direct);
            if (placement_check != null)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + placement_check + "');</script>", false);
                return;
            }



            //判断该编号是否有安置，推荐
            string GetError = registermemberBLL.GetError(mi.Direct, mi.Placement);
            if (GetError != null)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetError + "');</script>", false);

                return;
            }
            string GetError1 = new AjaxClass().CheckNumberNetAn(direct,placement);
            if (GetError1 != null && GetError1 != "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + this.GetTran("005986", "安置编号必须在推荐编号的安置网络下面！") + "');</script>", false);
                return;
            }
            string GetError2 = new AjaxClass().CheckNumberNetAn(Session["Member"].ToString(), direct);
            if (GetError2 != null && GetError2 != "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + this.GetTran("000000", "推荐编号必须在自己的安置网络下面！") + "');</script>", false);
                return;
            }


            #region 安置推荐人必须要激活


            if (Convert.ToInt32(DAL.DBHelper.ExecuteScalar("select COUNT(0) from MemberInfo where   MemberState=0 and Number='" + CommonDataBLL.quanjiao(Session["Member"].ToString()) + "'")) != 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + this.GetTran("000000", "推荐编号未激活！") + "');</script>", false);
                return;
            }

            if (Convert.ToInt32(DAL.DBHelper.ExecuteScalar("select COUNT(0) from MemberInfo where   MemberState=2 and Number='" + CommonDataBLL.quanjiao(Session["Member"].ToString()) + "'")) != 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + this.GetTran("009090", "推荐编号已注销！") + "');</script>", false);
                return;
            }
            if (Convert.ToInt32(DAL.DBHelper.ExecuteScalar("select COUNT(0) from MemberInfo where MemberState=0 and Number='" + CommonDataBLL.quanjiao(hidplacemnet.Value) + "'")) != 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + this.GetTran("000000", "安置编号未激活！") + "');</script>", false);
                return;
            }
            if (Convert.ToInt32(DAL.DBHelper.ExecuteScalar("select COUNT(0) from MemberInfo where MemberState=2 and Number='" + CommonDataBLL.quanjiao(hidplacemnet.Value) + "'")) != 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + this.GetTran("009107", "安置编号已注销！") + "');</script>", false);
                return;
            }

            #endregion

            //int placementXuHao = registermemberBLL.GetXuHao(mi.Direct);
            //if (placementXuHao >= 0)
            //{

            //    string GetError1 = new AjaxClass().CheckNumberNetAn(txtDirect.Text.Trim(), SearchPlacement_DoubleLines1.Placement);
            //    if (GetError1 != null && GetError1 != "")
            //    {
            //        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + this.GetTran("005986", "安置编号必须在推荐编号的安置网络下面！") + "');</script>", false);
            //        return;
            //    }
            //}

            //如果是零购注册，判断推荐人和安置人的注册期数是否合格
            //if (Session["Company"] != null)
            //{
            //    int tjExpectNum = registermemberBLL.GetError2(this.Txttj.Text.Trim());
            //    if (tjExpectNum > Convert.ToInt32(this.ddlQishu.SelectedValue))
            //    {
            //        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + this.GetTran("006013", "推荐人的注册期数必须大于") + this.ddlQishu.SelectedValue + "！');</script>", false);
            //        return;
            //    }

            //    int anExpectNum = registermemberBLL.GetError2(SearchPlacement_DoubleLines1.Placement.Trim());
            //    if (anExpectNum > Convert.ToInt32(this.ddlQishu.SelectedValue))
            //    {
            //        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + this.GetTran("006014", "安置人的注册期数必须大于") + this.ddlQishu.SelectedValue + "！');</script>", false);
            //        return;
            //    }
            //}
        }

        mi.PhotoPath = "";


        if (Session["UserType"] != null && Session["UserType"].ToString() == "1") //公司注册
        {
            mi.OrderType = 31;
            Session["LUOrder"] = mi.Number + ",31,1";
        }
        else if (Session["UserType"] != null && Session["UserType"].ToString() == "2") //店铺注册
        {
            mi.OrderType = 11;
            Session["LUOrder"] = mi.Number + ",11,2";
        }
        else if (Session["UserType"] != null && Session["UserType"].ToString() == "3") //会员注册
        {
            mi.OrderType = 21;
            Session["LUOrder"] = mi.Number + ",21,3";
            mi.Assister = Session["Member"].ToString();
        }
        else //默认店铺注册
        {
            mi.OrderType = 11;
            Session["LUOrder"] = mi.Number + ",11,2";
        }

        Session["mbreginfo"] = mi;

        if (Session["mbreginfo"] != null)
        {
            decimal totalmoeny = 0.00M;
            decimal bili = 0.00M;
            DataTable dts = DAL.DBHelper.ExecuteDataTable("select top 1 * from config order by createdate desc");
            if (dts.Rows != null && dts.Rows.Count > 0)
            {
                decimal htm = Convert.ToDecimal(hidtzmoney.Value);
                if (htm ==Convert.ToDecimal(dts.Rows[0]["para1"]))
                {
                    bili = Convert.ToDecimal(dts.Rows[0]["para4"]);
                }
                if (htm == Convert.ToDecimal(dts.Rows[0]["para2"]))
                {
                    bili = Convert.ToDecimal(dts.Rows[0]["para5"]);
                }
                if (htm == Convert.ToDecimal(dts.Rows[0]["para3"]))
                {
                    bili = Convert.ToDecimal(dts.Rows[0]["para6"]);
                }
            }
            OrderFinalModel ofm = new OrderFinalModel();
            var dayPrice = CommonDataBLL.GetMaxDayPrice();

            totalmoeny = Convert.ToDecimal(hidtzmoney.Value); ;//投资金额
            var value = Convert.ToDecimal(totalmoeny) / Convert.ToDecimal(dayPrice);//投资金额换化石斛积分
            
            var expect = CommonDataBLL.getMaxqishu();



            ofm.InvestJB = Convert.ToDecimal(value * bili);//投资石斛积分数量
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

            ofm.Type = Convert.ToInt32(rbltotaltype.SelectedValue);

            ofm.TotalMoney = Convert.ToDecimal(totalmoeny);
            ofm.TotalPv = Convert.ToDecimal(value);//投资金额兑换成石斛
            ofm.OrderType = mi.OrderType;
            ofm.OrderExpect = expect;
            ofm.StandardcurrencyMoney = ofm.TotalMoney;
            ofm.PaymentMoney = ofm.TotalMoney;
            ofm.OrderDate = DateTime.UtcNow;
            ofm.RemittancesId = "";
            ofm.ElectronicaccountId = "";



            ofm.ConCity.Country = "";
            ofm.ConCity.Province = "";
            ofm.ConCity.City = "";
            ofm.ConCity.Xian = "";
            ofm.ConAddress = mi.Address;
            ofm.CCPCCode = mi.CPCCode;



            ofm.ConTelPhone = mi.MobileTele;
            ofm.ConMobilPhone = mi.MobileTele;
            ofm.CarryMoney = 0;
            ofm.ConPost = mi.Email;
            ofm.Consignee = mi.PetName;
            ofm.ConZipCode = mi.PostalCode;

            ofm.ProductIDList = "";
            ofm.QuantityList = "";
            ofm.NotEnoughProductList = "";
           ofm.PhotoPath= "" ;
            Boolean flag = new AddOrderDataDAL().AddFinalOrder(ofm);
            if (flag)
            {

               // ClientScript.RegisterStartupScript(this.GetType(), "", "<script>$('#tiaoz').show();document.getElementById('tiaoz').href = '../payserver/chosepaysjpay.aspx?blif=" + EncryKey.GetEncryptstr(ofm.OrderID.ToString(), 1, 1) + "'; alertt('注册订单已生成，请及时支付！');</script>", false);
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>location.href = '../payserver/chosepaysjpay.aspx?blif=" + EncryKey.GetEncryptstr(ofm.OrderID.ToString(), 1, 1) + "'; </script>", false);
            }
        }
        else
        {
            Response.Redirect("registermember.aspx");
        }
    }

    public MemberInfoModel AddUserInfo()
    {
        MemberInfoModel memberInfoModel = new MemberInfoModel();
        memberInfoModel.Number = CommonDataBLL.quanjiao(HFNumber.Value.ToUpper());
        memberInfoModel.Placement = CommonDataBLL.quanjiao(hidplacemnet.Value);
        memberInfoModel.Direct = txtDirect.Text.Trim(); //Session["Member"].ToString();
        memberInfoModel.ExpectNum = BLL.CommonClass.CommonDataBLL.getMaxqishu();
        memberInfoModel.OrderID = "";
        memberInfoModel.StoreID = "8888888888";
        memberInfoModel.Name = "";
        memberInfoModel.PetName = Encryption.Encryption.GetEncryptionName(txtName.Text.Trim());
        memberInfoModel.OrderType = 21;
        memberInfoModel.LoginPass = Encryption.Encryption.GetEncryptionPwd(memberInfoModel.Number, memberInfoModel.Number);
        memberInfoModel.AdvPass = Encryption.Encryption.GetEncryptionPwd(memberInfoModel.Number, memberInfoModel.Number);
        memberInfoModel.LevelInt = 0;//会员级别
        memberInfoModel.RegisterDate = DateTime.UtcNow;
        memberInfoModel.PaperType.Id = 0;
        
            memberInfoModel.Birthday = Convert.ToDateTime("1990-01-01");
            memberInfoModel.Sex = 0;
        

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
        memberInfoModel.PostalCode = ""; //CommonDataBLL.quanjiao(this.Txtyb.Text.Trim());


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
        insertpara[0].Value = CommonDataBLL.quanjiao(HFNumber.Value.ToUpper());
        insertpara[1].Value = Encryption.Encryption.GetEncryptionName(txtName.Text.Trim());
        insertpara[2].Value = txtTele.Text;
        insertpara[3].Value = DAL.CommonDataDAL.GetCPCCode(country, province, city, xian);
        insertpara[4].Value = Encryption.Encryption.GetEncryptionAddress(CommonDataBLL.quanjiao(this.txtAddress.Text.Trim()));
        insertpara[5].Value = true;

        var insertvalue = DBHelper.ExecuteNonQuery(insert, insertpara, CommandType.Text);

        string paperCode = null;
        paperCode = "";

        memberInfoModel.PaperType.PaperTypeCode = ""; //证件类型
        memberInfoModel.PaperNumber = "";

        memberInfoModel.BankCode = "";
        memberInfoModel.BankAddress = "";
        memberInfoModel.BankBook = memberInfoModel.Name;
        memberInfoModel.BankCard = "";
        //country = "";
        //province = "";//控件
        //city = "";//控件
        //xian = "";
        memberInfoModel.BCPCCode = DAL.CommonDataDAL.GetCPCCode(country, province, city, xian);
        memberInfoModel.Remark = "";
        memberInfoModel.ChangeInfo = "";
        memberInfoModel.Email = "";
        //memberInfoModel.District = SearchPlacement_DoubleLines1.District;
        memberInfoModel.District = AddOrderDataDAL.GetDistrict(hidplacemnet.Value, Convert.ToInt32(hidDistrict.Value));
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
    //private void Translations()
    //{
    //    this.TranControls(this.RadioBtnSex, new string[][] { new string[] { "000094", "男" }, new string[] { "000095", "女" } });
    //    this.TranControls(this.ImageButton1, new string[][] { new string[] { "007297", "注册" } });
    //}
    protected void txtplacemnet_TextChanged(object sender, EventArgs e)
    {
        
       
            hidplacemnet.Value = txtplacemnet.Text;
        
    }
}