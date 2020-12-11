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
using BLL.other.Company;
using Model.Other;
using System.Data.SqlClient;
using DAL;
using Encryption;
//using BLL.CommonClass;
using System.Text.RegularExpressions;
using BLL.CommonClass;

public partial class Company_RegisterStore : BLL.TranslationBase
{
    protected string msg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.CustomerCreateStore);
        if (!IsPostBack)
        {
            DataTable dtct = StoreInfoEditBLL.bindCountry();
            foreach (DataRow item in dtct.Rows)
            {
                this.DropDownList1.Items.Add(new ListItem(item["name"].ToString(), item["name"].ToString()));
                this.StoreCountry.Items.Add(new ListItem(item["name"].ToString(), item["name"].ToString()));
            }
            SqlDataReader dr1 = StoreInfoEditBLL.bindCity(this.StoreCountry.SelectedValue.ToString());
            while (dr1.Read())
            {
                ListItem list2 = new ListItem(dr1["Province"].ToString(), dr1["Province"].ToString());
                this.StoreCity.Items.Add(list2);
            }
            dr1.Close();

            DataTable dt = DBHelper.ExecuteDataTable("select levelint,levelstr from bsco_level where levelflag=1 order by levelint");
            rdoListLevel.DataSource = dt;
            rdoListLevel.DataTextField = "levelstr";
            rdoListLevel.DataValueField = "levelint";
            rdoListLevel.DataBind();
            rdoListLevel.SelectedValue = DBHelper.ExecuteScalar("select top 1 levelint from bsco_level where levelflag=1 order by levelint").ToString();

            DropDownList1_SelectedIndexChanged(null, null);
            ddlLanaguage.DataSource = CommonDataBLL.GetLanaguage();
            ddlLanaguage.DataTextField = "Name";
            ddlLanaguage.DataValueField = "ID";
            ddlLanaguage.DataBind();
            Currency.DataSource = CommonDataBLL.GetCurrency();
            Currency.DataTextField = "Name";
            Currency.DataValueField = "ID";
            Currency.DataBind();
            if (Session["LoginUserType"].ToString() == "manage")
            {
                lbltitel.Text = GetTran("000554", "添加店信息");
            }
            else
            {
                lbltitel.Text = GetTran("000555", "店铺注册");
            }
            if (this.CountryCity1.City != "")


                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>GetCCode_s2('" + this.CountryCity1.City + "')</script>");
        }
        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.RequiredFieldValidator1, new string[][] { new string[] { "000214", "请输入会员编号!!!" } });
        this.TranControls(this.RegularExpressionValidator4, new string[][] { new string[] { "000220", "会员编号只能为字母数字下划线,且在6~10位之间" } });
        this.TranControls(this.RequiredFieldValidator2, new string[][] { new string[] { "000223", "请输入店编号" } });
        this.TranControls(this.RegularExpressionValidator5, new string[][] { new string[] { "000228", "店编号只能为字母数字下划线,且在4~10位之间" } });
        this.TranControls(this.RequiredFieldValidator5, new string[][] { new string[] { "000230", "请输入推荐您的会员编号" } });
        this.TranControls(this.RegularExpressionValidator6, new string[][] { new string[] { "000232", "会员编号只能是字母数字下划线，且在6~10位之间" } });
        this.TranControls(this.RequiredFieldValidator3, new string[][] { new string[] { "000236", "请输入店长名" } });
        this.TranControls(this.RequiredFieldValidator4, new string[][] { new string[] { "000238", "请输入店铺名称" } });
        this.TranControls(this.RegularExpressionValidator7, new string[][] { new string[] { "000244", "邮编不能有字符" } });
        this.TranControls(this.RegularExpressionValidator8, new string[][] { new string[] { "000250", "不能有字符" } });
        this.TranControls(this.RegularExpressionValidator9, new string[][] { new string[] { "000250", "不能有字符" } });
        this.TranControls(this.RegularExpressionValidator10, new string[][] { new string[] { "000250", "不能有字符" } });
        this.TranControls(this.RegularExpressionValidator11, new string[][] { new string[] { "000250", "不能有字符" } });
        this.TranControls(this.RegularExpressionValidator2, new string[][] { new string[] { "000257", "邮箱格式不正确" } });
        this.TranControls(this.RegularExpressionValidator3, new string[][] { new string[] { "000262", "网址填写不正确" } });
        this.TranControls(this.RegularExpressionValidator12, new string[][] { new string[] { "000268", "输入数据不对" } });
        this.TranControls(this.RegularExpressionValidator13, new string[][] { new string[] { "000268", "输入数据不对" } });

    }
    #region 字段
    UnauditedStoreInfoModel ustore = null;
    StoreInfoModel store = null;
    private string photo = "";
    string sid = "";
    string cp = "";
    string number = "";
    string sname = "";
    string Bank = "";
    string address = "";
    string pcode = "";
    string homeTel = "";
    string officetel = "";
    string mobiletel = "";
    string faxtel = "";
    string b = "";
    string bcard = "";
    string e = "";
    string naddress = "";
    string r = "";
    string sstr = "";
    int sint = -1;
    decimal farea = 0;
    string fbreed = "";
    decimal stouzi = 0;
    string name = "";
    string password = "";
    string scpp = "";
    string CurrencyCode = "";
    #endregion
    private void Setpassword()
    {
        if (mobiletel == "" && e == "")
        {
            BLL.CommonClass.Transforms.JSAlert(GetTran("000276", "对不起，手机或E-Mail必须填一个"));
            return;
        }
    }

    private void SetStoreInfo()
    {
        store = new StoreInfoModel();
        store.PhotoPath = photo;
        store.StoreID = sid;
        store.Number = number;
        store.StoreName = sname;
        store.CPCCode = cp;
        store.StoreAddress = address;
        store.PostalCode = pcode;
        store.HomeTele = homeTel;
        store.OfficeTele = officetel;
        store.MobileTele = mobiletel;
        store.FaxTele = faxtel;
        store.BankCode = Bank;
        store.BankCard = bcard;
        store.Email = e;
        store.NetAddress = naddress;
        store.Remark = r;
        store.StoreLevelInt = sint;
        store.FareArea = Convert.ToDecimal(farea);
        store.TotalInvestMoney = Convert.ToDecimal(stouzi);
        store.Direct = DisposeString.DisString(txtDirect.Text);
        store.Name = name;
        store.ExpectNum = CommonDataBLL.getMaxqishu();
        store.LoginPass = password;
        store.Currency = Convert.ToInt32(CurrencyCode);
        store.OperateIp = HttpContext.Current.Request.UserHostAddress;
        store.OperateNum = Session["Company"].ToString();
        store.SCPPCode = scpp;
    }

    private void SetUStoreInfo()
    {
        ustore = new UnauditedStoreInfoModel();
        ustore.PhotoPath = photo;
        ustore.StoreId = sid;
        ustore.Number = number;
        ustore.StoreName = sname;
        ustore.StoreAddress = address;
        ustore.PostalCode = pcode;
        ustore.HomeTele = homeTel;
        ustore.OfficeTele = officetel;
        ustore.MobileTele = mobiletel;
        ustore.FaxTele = faxtel;
        ustore.Bank = b;
        ustore.BankCard = bcard;
        ustore.Email = e;
        ustore.NetAddress = naddress;
        ustore.Remark = r;
        ustore.Currency = Convert.ToInt32(CurrencyCode);
        ustore.StoreLevelInt = sint;
        ustore.OperateNum = Session["Company"].ToString();
        try
        {
            ustore.FareArea = Convert.ToDecimal(farea);
        }
        catch (Exception)
        {
            BLL.CommonClass.Transforms.JSAlert(GetTran("000279", "输入的格式不对"));
            return;
        }
        ustore.FareBreed = fbreed;
        try
        {
            if (stouzi.ToString().Length > 0)
                ustore.TotalinvestMoney = Convert.ToDecimal(stouzi);
            else
                ustore.TotalaccountMoney = 0;
        }
        catch (Exception)
        {
            BLL.CommonClass.Transforms.JSAlert(GetTran("000279", "输入的格式不对"));
            return;
        }
        ustore.Direct = DisposeString.DisString(txtDirect.Text);
        ustore.Name = name;
        ustore.ExpectNum = CommonDataBLL.getMaxqishu();
        ustore.LoginPass = password;
        ustore.Language = Convert.ToInt32(ddlLanaguage.SelectedItem.Value);
        ustore.OperateIp = HttpContext.Current.Request.UserHostAddress;
        ustore.SCPCCode = scpp;
    }
    private void Gettxt()
    {
        Bank = this.ddlBank.SelectedValue.ToString();
        sid = DisposeString.DisString(txtStoreId.Text.Trim());
        number = DisposeString.DisString(txtNumber.Text.Trim());
        sname = Encryption.Encryption.GetEncryptionName(DisposeString.DisString(txtStoreName.Text.Trim()));
        cp = DBHelper.ExecuteScalar("select cpccode from city where country='" + this.CountryCity1.Country + "' and province='" + this.CountryCity1.Province + "' and city='" + this.CountryCity1.City + "'").ToString();
        address = Encryption.Encryption.GetEncryptionAddress(DisposeString.DisString(txtaddress.Text));
        pcode = DisposeString.DisString(txtPostalCode.Text);
        homeTel = Encryption.Encryption.GetEncryptionTele(DisposeString.DisString(txtHomeTele.Text));
        officetel = Encryption.Encryption.GetEncryptionTele(DisposeString.DisString(txtOfficeTele.Text));
        mobiletel = Encryption.Encryption.GetEncryptionTele(DisposeString.DisString(txtMobileTele.Text));
        faxtel = Encryption.Encryption.GetEncryptionTele(DisposeString.DisString(txtFaxTele.Text));
        bcard = Encryption.Encryption.GetEncryptionCard(DisposeString.DisString(txtBankCard.Text));
        e = DisposeString.DisString(txtEmail.Text);
        naddress = DisposeString.DisString(txtNetAddress.Text);
        r = DisposeString.DisString(txtRemark.Text);
        sint = int.Parse(rdoListLevel.SelectedItem.Value);
        if (DisposeString.DisString(txtFareArea.Text).Length > 0)
            farea = decimal.Parse(DisposeString.DisString(txtFareArea.Text));
        else
        {
            farea = 0;
        }
        fbreed = DisposeString.DisString(txtFareBreed.Text);
        if (DisposeString.DisString(txtTotalAccountMoney.Text).Length > 0)
        {
            stouzi = decimal.Parse(DisposeString.DisString(txtTotalAccountMoney.Text));
        }
        else
        {
            stouzi = 0;
        }

        name = Encryption.Encryption.GetEncryptionName(DisposeString.DisString(txtName.Text));
        string scp = DBHelper.ExecuteScalar("select cpccode from city where country='" + this.StoreCountry.SelectedValue.ToString() + "' and province='" + this.StoreCity.SelectedValue.ToString() + "'").ToString();
        scpp = scp.Substring(0, 4);

        CurrencyCode = DBHelper.ExecuteScalar("select rateid from country where countrycode='" + scpp.Substring(0, 2) + "'").ToString();

    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        StoreRegisterBLL member = new StoreRegisterBLL();

        int exists = 0;
        exists = (int)DBHelper.ExecuteScalar("SELECT COUNT(*) FROM MemberInfo WHERE Number='" + this.txtNumber.Text + "'", CommandType.Text);
        if (exists <= 0)
        {
            BLL.CommonClass.Transforms.JSAlert(GetTran("000288", "对不起,该会员编号不存在"));
            return;
        }
        if (DBHelper.ExecuteScalar("select memberstate from memberinfo where number='" + this.txtNumber.Text + "'").ToString() == "0")
        {
            BLL.CommonClass.Transforms.JSAlert(GetTran("000284", "该会员已注销,请先激活"));
            return;
        }
        if (StoreRegisterConfirmBLL.CheckStoreId(DisposeString.DisString(txtStoreId.Text)))
        {
            BLL.CommonClass.Transforms.JSAlert(GetTran("000291", "对不起,店铺编号已存在"));
            return;
        }

        if (!StoreRegisterBLL.CheckMemberInfoByNumber(DisposeString.DisString(txtDirect.Text)))
        {
            BLL.CommonClass.Transforms.JSAlert(GetTran("000293", "对不起,推荐店铺编号会员不存在"));
            return;
        }
        if (this.CountryCity1.Country == "请选择")
        {
            BLL.CommonClass.Transforms.JSAlert(GetTran("000300", "请选择地址"));
            return;
        }
        if (this.CountryCity1.Province == "请选择")
        {
            BLL.CommonClass.Transforms.JSAlert(GetTran("000300", "请选择地址"));
            return;
        }
        if (this.CountryCity1.City == "请选择")
        {
            BLL.CommonClass.Transforms.JSAlert(GetTran("000300", "请选择地址"));
            return;
        }
        if (this.txtaddress.Text == "")
        {
            BLL.CommonClass.Transforms.JSAlert(GetTran("001280", "地址不能为空！"));
            return;
        }
        if (this.txtRemark.Text.ToString().Length > 150)
        {
            BLL.CommonClass.Transforms.JSAlert(GetTran("006896", "对不起，备注应在150个字以内！"));
            return;
        }
        if (this.txtFareBreed.Text.ToString().Length > 150)
        {
            BLL.CommonClass.Transforms.JSAlert(GetTran("006896", "对不起，经营品种应在150个字以内！"));
            return;
        }
        if (Session["LoginUserType"].ToString().ToLower() == "manage")
        {
            GetPassword();
            Gettxt();
            SetStoreInfo();
            if (StoreRegisterBLL.RegisterStoreInfo(store, "S"))
            {
                msg = "<script language='javascript'>alert('" + GetTran("000023", "注册成功") + "！');location.href('StoreInfoModify.aspx')</script>";
            }

        }
        else
        {

            GetPassword();
            Gettxt();
            SetStoreInfo();
            BLL.other.Company.StoreRegisterBLL ss = new BLL.other.Company.StoreRegisterBLL();
            if (StoreRegisterBLL.RegisterStoreInfo(store, "U"))
            {
                //ScriptHelper.SetAlert(this.btnAdd, "注册成功！！！");
                BLL.CommonClass.Transforms.JSAlert(GetTran("000023", "注册成功"));
            }
        }
    }
    private void GetPassword()
    {
        //密码就是店铺编号
        password = Encryption.Encryption.GetEncryptionPwd(txtStoreId.Text.Trim(), txtStoreId.Text.Trim());
    }
    protected void btnblock_Click(object sender, EventArgs e)
    {
        Response.Redirect("AuditingStoreRegister.aspx");
    }
    protected void lkSubmit_Click(object sender, EventArgs e)
    {
        btnAdd_Click(null, null);
    }
    protected void StoreCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlDataReader dr = StoreInfoEditBLL.bindCity(this.StoreCountry.SelectedValue.ToString());
        this.StoreCity.Items.Clear();
        while (dr.Read())
        {

            ListItem list2 = new ListItem(dr["Province"].ToString(), dr["Province"].ToString());
            this.StoreCity.Items.Add(list2);
        }
        dr.Close();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        CommonDataBLL.BindCountry_Bank(this.DropDownList1.SelectedValue.ToString(), ddlBank);
    }
}
