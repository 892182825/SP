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
//using BLL.CommonClass;
using System.Text.RegularExpressions;
using BLL.CommonClass;
using BLL.other.Company;
using Model;
using Model.Other;
using System.Data.SqlClient;
using Encryption;

public partial class Member_RegisterStore : BLL.TranslationBase
{
    protected string msg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ThreeRedirect(Page, Permissions.redirUrl);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        Response.Cache.SetExpires(DateTime.Now);
        //Permissions.CheckManagePermission(EnumCompanyPermission.CustomerCreateStore);
        if (!IsPostBack)
        {
            if (Session["Member"] != null)
            {
                txtNumber.Text = Session["Member"].ToString();
                txtNumber.ReadOnly = true;
            }

            //如果该会员有自己的服务机构，那么就直接显示服务机构的记录，
            //如果没有，就直接显示申请服务机构的页面
            //先判断中间表UnauditedStoreInfo里是否有数据，如果有数据，是未审核；如果没有记录，就是查询storeinfo表里是否有记录，
            if (int.Parse(DAL.DBHelper.ExecuteScalar("select count(0) from storeinfo where number='" + Session["Member"].ToString() + "'").ToString()) > 0)
            {
                Response.Redirect("ShowRegStore.aspx");
                return;
            }

            DataTable dtct = StoreInfoEditBLL.bindCountry();
            foreach (DataRow item in dtct.Rows)
            {
                this.DropDownList2.Items.Add(new ListItem(item["name"].ToString(), item["name"].ToString()));
                this.StoreCountry.Items.Add(new ListItem(item["name"].ToString(), item["name"].ToString()));
            }
        
            SqlDataReader dr1 = StoreInfoEditBLL.bindCity(this.StoreCountry.SelectedValue.ToString());
            while (dr1.Read())
            {
                ListItem list2 = new ListItem(dr1["Province"].ToString(), dr1["Province"].ToString());
                this.StoreCity.Items.Add(list2);
            }
            dr1.Close();
            //DataTable dt = DBHelper.ExecuteDataTable("select levelint,levelstr from bsco_level where levelflag=1 order by levelint");
            //rdoListLevel.DataSource = dt;
            //rdoListLevel.DataTextField = "levelstr";
            //rdoListLevel.DataValueField = "levelint";
            //rdoListLevel.DataBind();
            //rdoListLevel.SelectedValue = DBHelper.ExecuteScalar("select top 1 levelint from bsco_level where levelflag=1 order by levelint").ToString();
            // BLL.CommonClass.CommonDataBLL common = new CommonDataBLL();
            DropDownList2_SelectedIndexChanged(null, null);
            ddlLanaguage.DataSource = CommonDataBLL.GetLanaguage();
            ddlLanaguage.DataTextField = "Name";
            ddlLanaguage.DataValueField = "ID";
            ddlLanaguage.DataBind();
            Currency.DataSource = CommonDataBLL.GetCurrency();
            Currency.DataTextField = "Name";
            Currency.DataValueField = "ID";
            Currency.DataBind();
            CommonDataBLL.BindQishuList(this.DropDownList1, false);
            //if (Session["LoginUserType"].ToString() == "manage")
            //{
            //    btnGetmember.Visible = true;
            //    lbltitel.Text = "添加店信息";
            //}
            //else
            //{
            //lbltitel.Text = "店铺注册";
            //    btnGetmember.Visible = false;
            //}
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
        //this.TranControls(this.RegularExpressionValidator1, new string[][] { new string[] { "000266", "你上传的文件格式不对" } });
        this.TranControls(this.RegularExpressionValidator12, new string[][] { new string[] { "000268", "输入数据不对" } });
        this.TranControls(this.RegularExpressionValidator13, new string[][] { new string[] { "000268", "输入数据不对" } });
        // this.TranControls(this.btnGetmember, new string[][] { new string[] { "000533", "点击获取信息" } });
        

    }
    #region 字段
    UnauditedStoreInfoModel ustore = null;

    private string photo = "";
    string storeid = "";
    string number = "";
    string storename = "";
    string scountry = "";
    string storepriovince = "";
    string country = "";
    string province = "";
    string city = "";
    string address = "";
    string postalcode = "";
    string homeTel = "";
    string officetel = "";
    string mobiletel = "";
    string faxtel = "";
    string bankCode = "";
    string bankCard = "";
    string email = "";
    string netAddress = "";
    string remark = "";
    string storelevelstr = "";
    int storelevelint = -1;
    string fareArea = "";
    string fareBreed = "";
    string stouzi = "";
    string name = "";
    string password = "";
    string CurrencyCode = "";
    #endregion
    private void Setpassword()
    {
        if (mobiletel == "" && email == "")
        {
            msg = "<script language='javascript'>alert('" + GetTran("000276", "对不起，手机或E-Mail必须填一个") + "')</script>";
            return;
        }
    }

    private void SetUStoreInfo()
    {
        ustore = new UnauditedStoreInfoModel();
        try
        {
            ustore.FareArea = Convert.ToDecimal(fareArea);
        }
        catch (Exception)
        {
            msg = "<script language='javascript'>alert('" + GetTran("000279", "输入的格式不对") + "')</script>";
        }
        ustore.FareBreed = fareBreed;
        try
        {
            if (stouzi.Length > 0)
                ustore.TotalaccountMoney = Convert.ToDecimal(stouzi);
            else
                ustore.TotalaccountMoney = 0;
        }
        catch (Exception)
        {
            msg = "<script language='javascript'>alert('" + GetTran("000279", "输入的格式不对") + "')</script>";
        }
        ustore.Number = number;
        ustore.StoreId = storeid;
        ustore.Name = Encryption.Encryption.GetEncryptionName(new AjaxClass().GetMumberName(number));//加密店长姓名
        ustore.StoreName = Encryption.Encryption.GetEncryptionName(storename);//加密店铺名称
        ustore.Country = country;
        ustore.Province = province;
        ustore.City = city;
        ustore.CPCCode = DBHelper.ExecuteScalar("select cpccode from city where country='" + this.StoreCountry.SelectedValue.ToString() + "' and province='" + this.StoreCity.SelectedValue.ToString() + "'").ToString().Substring(0, 4);
        ustore.SCPCCode = CommonDataBLL.GetCityCode(country, province, city);
        ustore.StoreAddress = Encryption.Encryption.GetEncryptionAddress(address);//加密地址
        ustore.HomeTele = Encryption.Encryption.GetEncryptionTele(homeTel);//加密家庭电话
        ustore.OfficeTele = Encryption.Encryption.GetEncryptionTele(officetel);//加密办公电话
        ustore.MobileTele = Encryption.Encryption.GetEncryptionTele(mobiletel);//加密电话
        ustore.FaxTele = Encryption.Encryption.GetEncryptionTele(faxtel);//加密传真
        ustore.BankCode = bankCode;
        ustore.BankCard = Encryption.Encryption.GetEncryptionCard(bankCard);//加密卡号
        ustore.Email = email;
        ustore.NetAddress = netAddress;
        ustore.Remark = remark;
        ustore.Direct = DisposeString.DisString(txtDirect.Text);
        ustore.ExpectNum = CommonDataBLL.getMaxqishu();
        ustore.RegisterDate = DateTime.Now.ToUniversalTime();
        ustore.LoginPass = password;
        ustore.AdvPass = password;
        ustore.StoreLevelStr = storelevelstr;
        //ustore.FareArea = Convert.ToDecimal(fareArea);
        ustore.FareBreed = fareBreed;
        //ustore.TotalaccountMoney = 0;
        ustore.TotalinvestMoney = decimal.Parse(stouzi);
        ustore.PostalCode = postalcode;
        ustore.AccreditExpectNum = int.Parse(this.DropDownList1.SelectedValue);
        ustore.PermissionMan = "";
        //ustore.Language = Convert.ToInt32(ddlLanaguage.SelectedItem.Value);
        //ustore.Currency = Convert.ToInt32(Currency.SelectedValue);
        CurrencyCode = DBHelper.ExecuteScalar("select rateid from country where countrycode='" + ustore.CPCCode.Substring(0, 2) + "'").ToString();
        ustore.Currency = Convert.ToInt32(CurrencyCode);
        ustore.StoreLevelInt = storelevelint;
        ustore.StoreCity = storepriovince;
        ustore.StoreCountry = scountry;
        ustore.OperateIp = CommonDataBLL.OperateIP;
        ustore.OperateNum = "";
        ustore.PhotoPath = photo;
    }
    private string Gettxt()
    {
        storeid = DisposeString.DisString(txtStoreId.Text.Trim());
        number = DisposeString.DisString(txtNumber.Text.Trim());
        storename = DisposeString.DisString(txtStoreName.Text.Trim());
        scountry = StoreCountry.SelectedItem.Value;
        storepriovince = StoreCity.SelectedItem.Value;
        UserControl_CountryCityPCode1 ucountry = Page.FindControl("CountryCity1") as UserControl_CountryCityPCode1;
        if (ucountry.CheckFill() == false)
        {
            return GetTran("001278", "请选择国家地址");
        }
        if (this.txtaddress.Text == "")
        {
            return GetTran("001280", "地址不能为空！");
        }
        country = ucountry.Country;
        //country = StoreCountryd.SelectedItem.Value;
        province = ucountry.Province;
        //province = StoreCountryp.SelectedItem.Value;
        // city = StoreCountryc.SelectedItem.Value;
        city = ucountry.City;
        address = DisposeString.DisString(txtaddress.Text);
        postalcode = DisposeString.DisString(txtPostalCode.Text);
        homeTel = DisposeString.DisString(txtHomeTele.Text);
        officetel = DisposeString.DisString(txtOfficeTele.Text);
        mobiletel = DisposeString.DisString(txtMobileTele.Text);
        faxtel = DisposeString.DisString(txtFaxTele.Text);
        bankCode = ddlBank.SelectedItem.Value;
        bankCard = DisposeString.DisString(txtBankCard.Text);
        email = DisposeString.DisString(txtEmail.Text);
        netAddress = DisposeString.DisString(txtNetAddress.Text);
        remark = DisposeString.DisString(txtRemark.Text);
        //storelevelstr = rdoListLevel.SelectedItem.Text;
        //storelevelint = int.Parse(rdoListLevel.SelectedItem.Value);
        if (DisposeString.DisString(txtFareArea.Text).Length > 0)
            fareArea = DisposeString.DisString(txtFareArea.Text);
        else
        {
            fareArea = "0";
        }
        fareBreed = DisposeString.DisString(txtFareBreed.Text);
        if (DisposeString.DisString(txtTotalAccountMoney.Text).Length > 0)
        {
            stouzi = DisposeString.DisString(txtTotalAccountMoney.Text);
        }
        else
        {
            stouzi = "0";
        }

        name = DisposeString.DisString(txtName.Text);

        return "";


    }
    private void FileLoad(out int photoW, out int photoH)
    {
        #region 上传图片
        string dirName = "";

        //string oldFilePath = this.filePhotoPath.PostedFile.FileName.Trim();

        string oldFileName = "";
        string newFileName = "";

        photoW = 0;
        photoH = 0;
        //try
        //{
        //    if (oldFilePath != string.Empty)
        //    {
        //        //检查目录是否存在
        //        dirName = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString();
        //        if (!System.IO.Directory.Exists(Server.MapPath("../UpLoad/" + dirName)))
        //        {
        //            System.IO.Directory.CreateDirectory(Server.MapPath("../UpLoad/" + dirName));
        //        }

        //        oldFileName = System.IO.Path.GetFileName(oldFilePath);

        //        string fileExtName = System.IO.Path.GetExtension(oldFilePath);
        //        Regex reg = new Regex(@"^.+\.(jpg)|(bmp)|(gif)|(swf)|(jpeg)$");
        //        if (!reg.Match(oldFileName.ToLower()).Success)
        //        {
        //            Response.Write("<script>alert('" + GetTran("000281", "您上传文件的格式不符合要求") + "');</script>");
        //            return;
        //        }
        //        if (filePhotoPath.PostedFile.ContentLength > 51200)
        //        {
        //            Response.Write("<script>alert('" + GetTran("000824", "上传文件不能大于50K！") + "');</script>");
        //            return;
        //        }

        //        System.Random rd = new Random(0);
        //        newFileName = DateTime.Now.Year.ToString() + rd.Next(10).ToString()
        //            + DateTime.Now.Month.ToString() + rd.Next(10).ToString()
        //            + DateTime.Now.Day.ToString() + rd.Next(10).ToString()
        //            + DateTime.Now.Second.ToString()
        //            + fileExtName;

        //        string newFilePath = Server.MapPath("..\\UpLoad\\" + dirName) + "\\" + newFileName;
        //        this.filePhotoPath.PostedFile.SaveAs(newFilePath);

        //        try
        //        {
        //            System.Drawing.Image myIma = System.Drawing.Image.FromFile(newFilePath);
        //            photoH = myIma.Height;
        //            photoW = myIma.Width;

        //        }
        //        catch
        //        { }

        //    }
        //}
        //catch (Exception ext)
        //{
        //    msg = "<script>alert('" + ext.Message + "')</script>";
        //    return;
        //}

        #endregion
    }
    string str = "";
    protected void btnAdd_Click(object sender, EventArgs e)
    {

        int exists = 0;
        exists = (int)StoreRegisterBLL.IsMemberNum(this.txtNumber.Text);
        if (exists <= 0)
        {
            msg = "<script language='javascript'>alert('" + GetTran("000288", "对不起,该会员编号不存在") + "')</script>";
            return;
        }
        if (DBHelper.ExecuteScalar("select MemberState from memberinfo where number='" + this.txtNumber.Text + "'").ToString() == "2")
        {
            msg = "<script language='javascript'>alert('" + GetTran("000284", "该会员已注销,请先激活") + "')</script>";
            return;
        }

        if (StoreRegisterBLL.CheckStoreNumber(this.txtNumber.Text.Trim())==-1) {
            ScriptHelper.SetAlert(Page, "该会员已经申请服务机构,不可重复申请");
            return;
        }

        if (StoreRegisterBLL.CheckStoreNumber(this.txtNumber.Text.Trim()) == -2)
        {
            ScriptHelper.SetAlert(Page, "该会员已经是服务机构，不可重复申请");
            return;
        }

        if (StoreRegisterConfirmBLL.CheckStoreId(DisposeString.DisString(txtStoreId.Text)))
        {
            msg = "<script language='javascript'>alert('" + GetTran("000291", "对不起,店铺编号已存在") + "')</script>";
            return;
        }

        if (!StoreRegisterBLL.CheckMemberInfoByNumber(DisposeString.DisString(txtDirect.Text)))
        {
            msg = "<script language='javascript'>alert('" + GetTran("000293", "对不起,推荐店铺编号会员不存在") + "')</script>";
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
        GetPassword();
        string str = Gettxt();
        if (str != "")
        {
            msg = "<script language='javascript'>alert('" + str + "')</script>";
            return;
        }
        SetUStoreInfo();
        if (StoreRegisterBLL.AllerRegisterStoreInfo(ustore))
        {
            msg = "<script language='javascript'>alert('申请成功,请等待公司审核');location.href='ShowRegStore.aspx'</script>";
            
        }

    }
    private void GetPassword()
    {
        //str = Common.GetReadomStr(6);
        //password = MD5Help.MD5Decrypt(str);
        //原来
        //password = StoreRegisterBLL.GetPassword(this.txtNumber.Text.ToString(), this.txtStoreId.Text.ToString());

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
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        CommonDataBLL.BindCountry_Bank(this.DropDownList2.SelectedValue.ToString(), ddlBank);
    }
}
