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

using BLL.other.Company;
using Model;
using BLL.Registration_declarations;
using System.Collections.Generic;
using BLL.other.Company;
using BLL.CommonClass;
using DAL;
using System.Data.SqlClient;
using System.IO;
using BLL.other.Member;

public partial class UpBasic : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ThreeRedirect(Page, "Member/" + Permissions.redirUrl);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        AjaxPro.Utility.RegisterTypeForAjax(typeof(UpBasic));

        btnSContact.Attributes.Add("onclick", "return Verify()");
        if (!IsPostBack)
        {
            if (Request.QueryString["Number"] == null)
            {
                return;
            }
            ViewState["Eurl"] = Request.UrlReferrer.AbsolutePath;
            ViewState["membernumberE"] = Request.QueryString["Number"].ToString().Trim();
            
            SelLblBasic();

            SellblContact();

            SellblBank();

            this.lblEbankname.Text = this.lblEname.Text;
        }
        else
        {
            if (txtEname.Text.Trim() != lblEname.Text && (!string.IsNullOrEmpty(txtEname.Text.Trim())))
            {
                this.lblEbankname.Text = this.txtEname.Text;
            }
            else
            {
                this.lblEbankname.Text = this.lblEname.Text;
            }
        }
        Translationbtn();
        if (ViewState["history"] != null)
        {
            ViewState["history"] = Convert.ToInt32(ViewState["history"].ToString()) + 1;
        }
        else
        {
            ViewState["history"] = 1;
        }
    }

    protected string GetCssType()
    {
        string cssType = Request.QueryString["CssType"].ToString();
        return cssType;
    }
    /// <summary>
    /// 翻译
    /// </summary>
    private void Translations()
    {
        this.TranControls(this.txtEsex,
                new string[][]{
                   new string []{"000094","男"},
                   new string []{"000095","女"}});


    }
    /// <summary>
    /// 翻译
    /// </summary>
    private void Translationbtn()
    {
        this.TranControls(this.btnEbasic, new string[][] { new string[] { "000259", "修改" } });
        this.TranControls(this.btnSbasic, new string[][] { new string[] { "005901", "保存" } });
        this.TranControls(this.btnCbasic, new string[][] { new string[] { "001614", "取消" } });

        this.TranControls(this.btnEContact, new string[][] { new string[] { "000259", "修改" } });
        this.TranControls(this.btnSContact, new string[][] { new string[] { "005901", "保存" } });
        this.TranControls(this.btnCContact, new string[][] { new string[] { "001614", "取消" } });

        this.TranControls(this.btnEBank, new string[][] { new string[] { "000259", "修改" } });
        this.TranControls(this.btnSBank, new string[][] { new string[] { "005901", "保存" } });
        this.TranControls(this.btnCBank, new string[][] { new string[] { "001614", "取消" } });

        this.TranControls(this.btnEfh, new string[][] { new string[] { "000421", "返回" } });
    }
    /// <summary>
    /// 绑定国家
    /// </summary>
    public void BindCountry()
    {
        string number = ViewState["membernumberE"].ToString();
        string countrycode = MemInfoEditBLL.Getbank(number);
        this.ddlCountry.Items.Clear();

        DataTable dtct = StoreInfoEditBLL.bindCountry();
        foreach (DataRow item in dtct.Rows)
        {
          
            ListItem list1 = new ListItem(item["name"].ToString() , item["countrycode"].ToString());
            if ((!string.IsNullOrEmpty(countrycode)) && item["countrycode"].ToString() == countrycode)
            {
                list1.Selected = true;
                ViewState["childdrop"] = "true";
            }
            this.ddlCountry.Items.Add(list1);
        }

        
       
        ddlCountry_SelectedIndexChanged(null, null);
    }
    /// <summary>
    ///  根据国家选择银行
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        CommonDataBLL.BindCountry_Bank(this.ddlCountry.SelectedItem.Text, this.DdlBank);
        if (ViewState["bankcode"] != null && ViewState["childdrop"] != null)
        {
            this.DdlBank.SelectedValue = ViewState["bankcode"].ToString();
            ViewState["bankcode"] = null;
            ViewState["childdrop"] = null;
        }
    }
    //-----------------------------显示
    /// <summary>
    /// 查看基本信息
    /// </summary>
    public void SelLblBasic()
    {
        lblEnumber.Visible = true;//会员编号
        lblEname.Visible = true;//真实姓名
        lblEnickname.Visible = true;//会员昵称
        lblEstore.Visible = true;//购物店铺
        lblEIdtype.Visible = true;//证件类型
        lblEsex.Visible = true;//会员性别
        lblEbirthday.Visible = true;//出生日期
        lblEidnumber.Visible = true;//证件编号
        lblEaddress.Visible = true;//联系地址
        lblEcode.Visible = true;//邮政编码
        lblEmethod.Visible = true;//运货方式
        lblEcurrency.Visible = true;//支付币种

        labenc.Visible = false;
        txtEnumber.Visible = false;
        txtEname.Visible = false;
        txtEnickname.Visible = false;
        txtEstore.Visible = false;
        txtEIdtype.Visible = false;
        txtEsex.Visible = false;
        txtEbirthday.Visible = false;
        txtEidnumber.Visible = false;
        CountryCityPCode1.Visible = false;
        txtEaddress.Visible = false;
        txtEcode.Visible = false;
        txtEmethod.Visible = false;
        txtEcurrency.Visible = false;

        this.lblEbankname.Text = this.lblEname.Text;

        //tbephoto.Visible = false;
        //默认的会员编号是
        MemberInfoModel model = MemInfoEditBLL.getMemberInfo(ViewState["membernumberE"].ToString());
        MemberOrderModel order = MemberOrderBLL.GetMemberOrder(model.OrderID);
        if (!string.IsNullOrEmpty(model.PhotoPath))
        {
            //string mappath = Server.MapPath(model.PhotoPath);
            this.imgE.ImageUrl = "~/" + model.PhotoPath;
        }
        else
        {
            //string mappath = Server.MapPath("pht.GIF");
            //this.imgE.ImageUrl = mappath;
            this.imgE.ImageUrl = "~/images/pht.GIF";
        }
        lblEnumber.Text = model.Number;
        lblEname.Text = Encryption.Encryption.GetDecipherName(model.Name);
        lblEnickname.Text = model.PetName;
        lblEstore.Text = model.StoreID;
        lblEIdtype.Text = string.IsNullOrEmpty(model.PaperType.PaperType) ? GetTran("000221", "无") : model.PaperType.PaperType;
        lblEsex.Text = model.Sex == 0 ? GetTran("000095", "女") : GetTran("000094", "男");
        lblEbirthday.Text = model.Birthday.ToString("yyyy-MM-dd");
        lblEidnumber.Text = Encryption.Encryption.GetDecipherNumber(model.PaperNumber.ToString().Trim());
        lblEaddress.Text = model.City.Country + " " + model.City.Province + " " + model.City.City + " " +  model.City.Xian + " " + Encryption.Encryption.GetDecipherAddress(model.Address.Trim());
        lblEcode.Text = model.PostalCode.ToString();
        lblEbankname.Text = Encryption.Encryption.GetDecipherName(model.BankBook);
        ViewState["Ephtot"] = model.PhotoPath;
    }

    /// <summary>
    /// 设置图片框大小
    /// </summary>
    public void getimagehw()
    {
        imgE.Attributes.Add("style", " width='100px' Height='130px'");
    }

    /// <summary>
    /// 编辑基本信息
    /// </summary>
    public void editxtBasic()
    {
        txtEnumber.Visible = false;
        txtEname.Visible = true;
        txtEnickname.Visible = true;
        txtEstore.Visible = true;
        txtEIdtype.Visible = true;
        txtEsex.Visible = true;
        txtEbirthday.Visible = true;
        txtEidnumber.Visible = true;
        CountryCityPCode1.Visible = true;
        txtEaddress.Visible = true;
        txtEcode.Visible = true;
        txtEmethod.Visible = true;
        txtEcurrency.Visible = true;
        labenc.Visible = true;

        lblEnumber.Visible = true;
        lblEname.Visible = false;
        lblEnickname.Visible = false;
        lblEstore.Visible = false;
        lblEIdtype.Visible = false;
        lblEsex.Visible = false;
        lblEbirthday.Visible = false;
        lblEidnumber.Visible = false;
        lblEaddress.Visible = false;
        lblEcode.Visible = false;
        lblEmethod.Visible = false;
        lblEcurrency.Visible = false;


        //tbephoto.Visible = true;
        //默认的会员编号是
        MemberInfoModel model = MemInfoEditBLL.getMemberInfo(ViewState["membernumberE"].ToString());
        MemberOrderModel order = MemberOrderBLL.GetMemberOrder(model.OrderID);
        //绑定证件类型
        bindidtype();
        Translations();

        txtEnumber.Text = model.Number;
        txtEname.Text = Encryption.Encryption.GetDecipherName(model.Name);
        txtEnickname.Text = model.PetName;
        txtEstore.Text = model.StoreID;
        try
        {
            txtEIdtype.SelectedValue = model.PaperType.PaperTypeCode.Trim();
        }
        catch (Exception exe1)
        {
            txtEIdtype.SelectedValue = "P000";
        }
        txtEsex.SelectedIndex = model.Sex == 0 ? 1 : 0;
        txtEbirthday.Text = model.Birthday.ToString("yyyy-MM-dd");
        txtEidnumber.Text = Encryption.Encryption.GetDecipherNumber(model.PaperNumber.ToString().Trim());
        CountryCityPCode1.SelectCountry(model.City.Country, model.City.Province, model.City.City,model.City.Xian);
        txtEaddress.Text = Encryption.Encryption.GetDecipherAddress(model.Address.ToString().Trim()); ;
        txtEcode.Text = model.PostalCode.ToString();
        lblEbankname.Text = Encryption.Encryption.GetDecipherName(model.BankBook);
    }
    /// <summary>
    /// 查看联系信息
    /// </summary>
    public void SellblContact()
    {
        lblEmobile.Visible = true;//移动电话
        lblEphone.Visible = true;//家庭电话
        lblEfax.Visible = true;//传真电话
        lblEOffphone.Visible = true;//办公电话
        lblEemail.Visible = true;//Email

        this.divBg.Visible = false;
        this.divCz.Visible = false;
        this.divJt.Visible = false;
        this.txtEmobile.Visible = false;
        this.txtEemail.Visible = false;
        //this.TxtjtdhQh.Visible = false;
        //Txtjtdh.Visible = false;//家庭电话
        //this.TxtczdhQh.Visible = false;
        //Txtczdh.Visible = false;//传真电话
        //this.TxtbgdhFj.Visible = false;
        //this.TxtbgdhQh.Visible = false;
        //Txtbgdh.Visible = false;//办公电话
        //this.TxtbgdhFj.Visible = false;


        //默认的会员编号是8888888888s
        MemberInfoModel model = MemInfoEditBLL.getMemberInfo(ViewState["membernumberE"].ToString());
        //MemberOrderModel order = MemberOrderBLL.GetMemberOrder(model.OrderID);

        lblEmobile.Text = Encryption.Encryption.GetDecipherTele(model.MobileTele);
        lblEphone.Text =Encryption.Encryption.GetDecipherTele(model.HomeTele);
        lblEfax.Text =Encryption.Encryption.GetDecipherTele(model.FaxTele);
        lblEOffphone.Text = Encryption.Encryption.GetDecipherTele(model.OfficeTele);
        lblEemail.Text = model.Email;

    }
    /// <summary>
    /// 编辑联系信息
    /// </summary>
    public void edilblContact()
    {
        lblEmobile.Visible = false;//移动电话
        lblEphone.Visible = false;//家庭电话
        lblEfax.Visible = false;//传真电话
        lblEOffphone.Visible = false;//办公电话
        lblEemail.Visible = false;//Email

        this.divBg.Visible = true;
        this.divCz.Visible = true;
        this.divJt.Visible = true;
        txtEmobile.Visible = true;//移动电话
        //this.TxtjtdhQh.Visible = true;
        //Txtjtdh.Visible = true;//家庭电话
        //this.TxtczdhQh.Visible = true;
        //Txtczdh.Visible = true;//传真电话
        //this.TxtbgdhFj.Visible = true;
        //this.TxtbgdhQh.Visible = true;
        //Txtbgdh.Visible = true;//办公电话
        //this.TxtbgdhFj.Visible = true;
       txtEemail.Visible = true;//Email


        //默认的会员编号是8888888888s
        MemberInfoModel model = MemInfoEditBLL.getMemberInfo(ViewState["membernumberE"].ToString());
        //MemberOrderModel order = MemberOrderBLL.GetMemberOrder(model.OrderID);

        txtEmobile.Text = Encryption.Encryption.GetDecipherTele(model.MobileTele);

        if (this.TxtjtdhQh.Text.Trim() == "")
        {
            this.TxtjtdhQh.Text = "区号";
            this.TxtjtdhQh.Style.Add("color", "gray");
        }
        Txtjtdh.Text = Encryption.Encryption.GetDecipherTele(model.HomeTele);
        if (this.Txtjtdh.Text.Trim() == "")
        {
            this.Txtjtdh.Text = "电话号码";
            this.Txtjtdh.Style.Add("color", "gray");
        }
        if (this.TxtbgdhQh.Text.Trim() == "")
        {
            this.TxtbgdhQh.Text = "区号";
            this.TxtbgdhQh.Style.Add("color", "gray");
        }
        Txtbgdh.Text = Encryption.Encryption.GetDecipherTele(model.OfficeTele);
        if (this.Txtbgdh.Text.Trim() == "")
        {
            this.Txtbgdh.Text = "电话号码";
            this.Txtbgdh.Style.Add("color", "gray");
        }
        if (this.TxtbgdhFj.Text.Trim() == "")
        {
            this.TxtbgdhFj.Text = "分机号";
            this.TxtbgdhFj.Style.Add("color", "gray");
        }

        if (this.TxtczdhQh.Text.Trim() == "")
        {
            this.TxtczdhQh.Text = "区号";
            this.TxtczdhQh.Style.Add("color", "gray");
        }
        Txtczdh.Text = Encryption.Encryption.GetDecipherTele(model.FaxTele);
        if (this.Txtczdh.Text.Trim() == "")
        {
            this.Txtczdh.Text = "电话号码";
            this.Txtczdh.Style.Add("color", "gray");
        }
        if (this.TxtczdhFj.Text.Trim() == "")
        {
            this.TxtczdhFj.Text = "分机号";
            this.TxtczdhFj.Style.Add("color", "gray");
        } 

        txtEemail.Text = model.Email;


    }
    /// <summary>
    /// 查看帐户信息
    /// </summary>
    public void SellblBank()
    {
        lblEbankname.Visible = true;//开户名
        lblEbank.Visible = true;//开户银行
        lblEbanknumber.Visible = true;//银行卡号
        lblEbankaddress.Visible = true;//银行地址

        CcpEbankaddress.Visible = false;
        plEbank.Visible = false;//开户银行
        txtEbanknumber.Visible = false;//银行卡号
        txtEbankaddress.Visible = false;//银行地址
        CountryCityPCode2.Visible = false;

        //默认的会员编号是
        MemberInfoModel model = MemInfoEditBLL.getMemberInfo(ViewState["membernumberE"].ToString());
        lblEbankname.Text = Encryption.Encryption.GetDecipherName(model.BankBook);
        lblEbank.Text = MemInfoEditBLL.GetbankStr(ViewState["membernumberE"].ToString());
        lblEbanknumber.Text = Encryption.Encryption.GetDecipherCard(model.BankCard);
        //lblEbankaddress.Text = Encryption.Encryption.GetDecipherAddress(model.BankAddress);
        CityModel cityM = CommonDataDAL.GetCPCCode(model.BCPCCode);
        lblEbankaddress.Text = cityM.Country + " " + cityM.Province + " " + cityM.City + " " + cityM.Xian + " " + Encryption.Encryption.GetDecipherAddress(model.BankAddress);
    }
    /// <summary>
    /// 编辑帐户信息
    /// </summary>
    public void edilblBank()
    {
        lblEbankname.Visible = true;//开户名
        lblEbank.Visible = false;//开户银行
        lblEbanknumber.Visible = false;//银行卡号
        lblEbankaddress.Visible = false;//银行地址

        CcpEbankaddress.Visible = true;
        plEbank.Visible = true;//开户银行
        txtEbanknumber.Visible = true;//银行卡号
        txtEbankaddress.Visible = true;//银行地址
        CountryCityPCode2.Visible = true;

        //默认的会员编号是8888888888s
        MemberInfoModel model = MemInfoEditBLL.getMemberInfo(ViewState["membernumberE"].ToString());
        txtEbank.Text = model.Bankbranchname;
        ViewState["bankcode"] = model.BankCode;
        txtEbanknumber.Text = Encryption.Encryption.GetDecipherCard(model.BankCard);
        CityModel cityM = CommonDataDAL.GetCPCCode(model.BCPCCode);
        CountryCityPCode2.SelectCountry(cityM.Country, cityM.Province, cityM.City, cityM.Xian);
        txtEbankaddress.Text = Encryption.Encryption.GetDecipherAddress(model.BankAddress);
        BindCountry();
    }
    //-------------------------修改事件
    /// <summary>
    /// 绑定证件类型
    /// </summary>
    public void bindidtype()
    {
        this.txtEIdtype.DataSource = new GroupRegisterBLL().GetCardCode();
        //this.txtEIdtype.DataTextField = "PaperType";
        //this.txtEIdtype.DataValueField = "PaperTypeCode";
        this.txtEIdtype.DataTextField = "PaperType";
        this.txtEIdtype.DataValueField = "papertypecode";
        this.txtEIdtype.DataBind();
    }
    /// <summary>
    /// 基本信息修改
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEbasic_Click(object sender, EventArgs e)
    {
        btnEbasic.Visible = false;
        btnCbasic.Visible = true;
        btnSbasic.Visible = true;
        editxtBasic();
    }
    /// <summary>
    /// 联系信息修改
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEContact_Click(object sender, EventArgs e)
    {
        btnEContact.Visible = false;
        btnSContact.Visible = true;
        btnCContact.Visible = true;
        edilblContact();
    }
    /// <summary>
    /// 账户信息修改
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEBank_Click(object sender, EventArgs e)
    {

        btnEBank.Visible = false;
        btnSBank.Visible = true;
        btnCBank.Visible = true;
        edilblBank();
    }
    //-------------------------取消
    /// <summary>
    /// 基本信息取消
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCbasic_Click(object sender, EventArgs e)
    {
        btnEbasic.Visible = true;
        btnCbasic.Visible = false;
        btnSbasic.Visible = false;
        SelLblBasic();
    }
    /// <summary>
    /// 联系信息取消
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCContact_Click(object sender, EventArgs e)
    {
        btnEContact.Visible = true;
        btnSContact.Visible = false;
        btnCContact.Visible = false;
        SellblContact();
    }
    /// <summary>
    /// 帐户信息取消
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCBank_Click(object sender, EventArgs e)
    {
        btnEBank.Visible = true;
        btnSBank.Visible = false;
        btnCBank.Visible = false;
        SellblBank();
    }

    protected string GetLogoutType()
    {
        string type = "1";
        if (Session["Store"] != null)
        {
            type = "2";
        }
        else if (Session["Member"] != null)
        {
            type = "1";
        }
        else if (Session["Company"] != null)
        {
            type = "3";
        }
        return type;
    }


    //-------------------------保存
    /// <summary>
    /// 保存基本信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSbasic_Click(object sender, EventArgs e)
    {



        if (!Page.IsValid)
        {
            return;
        }
        string dirName = "";
        //string oldFilePath = this.fude.PostedFile.FileName.Trim();
        string oldFileName = "";
        string newFileName = "";
        string filepath = "";
        int photoW = 0, photoH = 0;
        //string newFilePath = string.Empty;
        //try
        //{
        //    if (oldFilePath != string.Empty)
        //    {
        //        if (!Directory.Exists(Server.MapPath("Store\\H_image\\"))) //如果文件夹不存在则创建
        //        {
        //            Directory.CreateDirectory(Server.MapPath("Store\\H_image\\"));
        //        }

        //        //检查目录是否存在
        //        dirName = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString();

        //        oldFileName = System.IO.Path.GetFileName(oldFilePath);
        //        string fileExtName = string.Empty;
        //        try
        //        {
        //            fileExtName = System.IO.Path.GetExtension(oldFilePath);
        //        }
        //        catch
        //        {
        //            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("000823", "上传文件格式不正确！") + "');", true);
        //            return;
        //        }


        //        if (fileExtName.ToLower() != ".icon" && fileExtName.ToLower() != ".jpg" && fileExtName.ToLower() != ".gif" && fileExtName.ToLower() != ".ico")
        //        {
        //            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('上传文件格式不正确，只能上传.icon、.jpg、.gif或者.ico格式的照片！');", true);
        //            return;
        //        }

        //        if (this.fude.PostedFile.ContentLength > 51200)
        //        {
        //            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("000824", "上传文件不能大于50K！") + "');", true);
        //            return;
        //        }

        //        //System.Drawing.Image img = System.Drawing.Image.FromStream(fude.PostedFile.InputStream);
        //        //int width = img.Width;
        //        //int hight = img.Height;
        //        //if (width > 50 || hight > 50)
        //        //{
        //        //    Response.Write("<script>alert('" + GetTran("006034", "图片宽度和高度太大！") + "');</script>");
        //        //    this.Button1.Enabled = true;
        //        //    return "";
        //        //}
        //        System.Random rd = new Random(0);
        //        newFileName = DateTime.Now.Year.ToString() + rd.Next(10).ToString()
        //            + DateTime.Now.Month.ToString() + rd.Next(10).ToString()
        //            + DateTime.Now.Day.ToString() + rd.Next(10).ToString()
        //            + DateTime.Now.Second.ToString()
        //            + fileExtName;
        //        newFilePath = Server.MapPath("Store\\H_image\\") + newFileName;

        //        string LevelIcon = new MemberInfoModifyBll().GetMemberPhoto(this.txtEnumber.Text.ToString()) + "";
        //        if (System.IO.File.Exists(Server.MapPath(LevelIcon)))
        //        {
        //            System.IO.File.Delete(Server.MapPath(LevelIcon));
        //        }

        //        this.fude.PostedFile.SaveAs(newFilePath);
        //        try
        //        {
        //            System.Drawing.Image myIma = System.Drawing.Image.FromFile(newFilePath);
        //            photoH = myIma.Height;
        //            photoW = myIma.Width;

        //        }
        //        catch (Exception ex1)
        //        {
        //            if (System.IO.File.Exists(newFilePath))
        //            {
        //                System.IO.File.Delete(newFilePath);
        //            }
        //            Response.Write("<script>alert('" + GetTran("006895", "图片格式转换错误！") + "');</script>");
        //            return;
        //        }
        //        filepath = @"\Store\H_image\" + newFileName;
        //    }
        //}
        //catch (Exception ex1)
        //{
        //    return;
        //}



        MemberInfoModel model = new MemberInfoModel();

        BLL.Registration_declarations.RegistermemberBLL registermemberBLL = new BLL.Registration_declarations.RegistermemberBLL();
        if (this.txtEname.Text == "")
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("000131", "对不起，会员姓名不能为空！") + "');", true);
            return;
        }
        if (this.txtEstore.Text == "")
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("006706", "对不起，购货店铺不能为空！") + "');", true);
            return;
        }
        if (!MemInfoEditBLL.GetStorenumber(this.txtEstore.Text.Trim()))
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("006707", "对不起，购货店铺编号不存在！") + "');", true);
            return;
        }
        //if (this.txtEcode.Text == "")
        //{
        //    System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("000134", "对不起，邮编不能为空！") + "');", true);
        //    return;
        //}
        if (this.txtEnickname.Text == "")
        {
            this.txtEnickname.Text = this.txtEname.Text;
        }
        
        if (this.txtEidnumber.Text == "" && this.txtEIdtype.SelectedValue.Trim() != "P000")
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("000140", "对不起，证件号码不能为空！") + "');", true);
            return;
        }

        UserControl_CountryCityPCode ucontry = Page.FindControl("CountryCityPCode1") as UserControl_CountryCityPCode;
        DropDownList dllcountry = ucontry.FindControl("ddlCountry") as DropDownList;
        if (dllcountry.SelectedIndex == 0)
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("000143", "请选择国家！") + "');", true);
            return;
        }

        DropDownList dllP = ucontry.FindControl("ddlP") as DropDownList;
        if (dllP.SelectedIndex == 0)
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("000144", "请选择省份！") + "');", true);
            return;
        }
        DropDownList dllcity = ucontry.FindControl("ddlCity") as DropDownList;
        if (dllcity.SelectedIndex == 0)
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("000145", "请选择城市！") + "');", true);
            return;
        }

        if (this.txtEaddress.Text == "")
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("000146", "对不起，地址不能为空！") + "');", true);
            return;
        }
        //检查会员生日
        if (this.txtEIdtype.SelectedValue.Trim() != "P001")
        {

            if (registermemberBLL.CheckBirthDay(this.txtEbirthday.Text) == "error")
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "alert('" + GetTran("000148", "对不起，请选择正确的出生日期！") + "');", true);

                return;
            }
        }
        //验证年龄是否大于18岁
        if (this.txtEIdtype.SelectedValue.Trim() != "P001")
        {
            string alert = registermemberBLL.AgeIs18(this.txtEbirthday.Text.Trim());
            if (alert != null)
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "alert('" + alert + "');", true);
                return;
            }
        }
        //检测身份证需要新方法
        string birthdaysex = "";
        if (this.txtEIdtype.SelectedValue.Trim() == "P001")
        {
            string result = BLL.Registration_declarations.CheckMemberInfo.CHK_IdentityCard(CommonDataBLL.quanjiao(this.txtEidnumber.Text.Trim()));
            if (result.IndexOf(",") <= 0)
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "alert('" + result + "');", true);
                return;
            }
            else
            {
                birthdaysex = result;
            }
        }
        string Number = this.txtEnumber.Text.ToString();
        string Name = Encryption.Encryption.GetEncryptionName(this.txtEname.Text.ToString().Trim());
        string PetName = this.txtEnickname.Text.ToString();
        DateTime Birthday = DateTime.Parse(this.txtEbirthday.Text.ToString());
        string Country = this.CountryCityPCode1.Country;
        string Province = this.CountryCityPCode1.Province;
        string City = this.CountryCityPCode1.City;
        string xian = this.CountryCityPCode1.Xian;
        int Sex = 0;
        if (this.txtEIdtype.SelectedValue.Trim() == "P001")
        {
            Sex = birthdaysex.Substring(birthdaysex.IndexOf(",") + 1).Trim() == GetTran("000094", "男") ? (1) : (0);
            Birthday = Convert.ToDateTime(birthdaysex.Substring(0, birthdaysex.IndexOf(",")));
            //验证年龄是否大于18岁
            string alert = registermemberBLL.AgeIs18(Birthday.ToString());
            if (alert != null)
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "alert('" + alert + "');", true);
                return;
            }
        }
        else
        {
            Birthday = DateTime.Parse(this.txtEbirthday.Text.ToString());

            if (this.txtEsex.SelectedValue.ToString() == "0")
            {
                Sex = 0;
            }
            else
            {
                Sex = 1;
            }
        }
        string PostalCode = this.txtEcode.Text.ToString();
        ChangeLogs cl = new ChangeLogs("MemberInfo", "ltrim(rtrim(Number))");
        cl.AddRecord(Number);
        MemberInfoModel info = new MemberInfoModel();
        info.Number = Number;
        info.Name = Name;
        info.PetName = PetName;
        info.Birthday = Birthday;
        info.Sex = Sex;
        info.PostalCode = PostalCode;
        if (string.IsNullOrEmpty(filepath))
        {
            info.PhotoPath = ViewState["Ephtot"].ToString().Trim();
        }
        else
        {
            info.PhotoPath = filepath;
        }

        info.StoreID = txtEstore.Text.ToString().Trim();
        info.Papertypecode = this.txtEIdtype.SelectedValue.ToString().Trim();
        info.PaperNumber = Encryption.Encryption.GetEncryptionNumber(this.txtEidnumber.Text.ToString().Trim());
        info.CPCCode = CommonDataDAL.GetCPCCode(Country, Province, City,xian);
        info.Address = Encryption.Encryption.GetEncryptionAddress(this.txtEaddress.Text.ToString().Trim());
        info.BankBook = Name;
        //BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("memberinfo", "ltrim(rtrim(number))");//申明日志对象

        if (MemInfoEditBLL.Updmemberbasic(info))
        {
            cl.AddRecord(info.Number);
            cl.ModifiedIntoLogs(ChangeCategory.Order, info.Number, ENUM_USERTYPE.objecttype5);

            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("001401", "操作成功！") + "');", true);
            this.btnSbasic.Visible = false;
            this.btnCbasic.Visible = false;
            this.btnEbasic.Visible = true;
            SelLblBasic();
            return;
        }
        else
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("001541", "操作失败！") + "');", true);
            return;
        }

    }

    private void GetPhone()
    {
        if (this.TxtjtdhQh.Text.Trim() == "区号")
        {
            this.TxtjtdhQh.Text = "";
        }
        if (this.Txtjtdh.Text.Trim() == "电话号码")
        {
            this.Txtjtdh.Text = "";
        }

        if (Txtczdh.Text.Trim() == "电话号码")
        {
            Txtczdh.Text = "";
        }
        if (this.TxtczdhQh.Text.Trim() == "区号")
        {
            TxtczdhQh.Text = "";
        }
        if (TxtczdhFj.Text.Trim() == "分机号")
        {
            TxtczdhFj.Text = "";
        }

        if (TxtbgdhQh.Text.Trim() == "区号")
        {
            TxtbgdhQh.Text = "";
        }
        if (this.Txtbgdh.Text.Trim() == "电话号码")
        {
            Txtbgdh.Text = "";
        }
        if (this.TxtbgdhFj.Text.Trim() == "分机号")
        {
            TxtbgdhFj.Text = "";
        }
    }

    /// <summary>
    /// 保存联系信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSContact_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("001541", "操作失败！") + "');", true);
            return;
        }
        if (string.IsNullOrEmpty(this.txtEmobile.Text.Trim()) && string.IsNullOrEmpty(this.Txtjtdh.Text.Trim()) && string.IsNullOrEmpty(this.Txtczdh.Text.Trim()) && string.IsNullOrEmpty(this.Txtbgdh.Text.Trim()))
        {
            Response.Write("<script>alert('" + GetTran("005992", "请至少填写一种电话联系方式！") + "');</script>");
            return;
        }

        GetPhone();
        ChangeLogs cl = new ChangeLogs("MemberInfo", "ltrim(rtrim(Number))");
        cl.AddRecord(ViewState["membernumberE"].ToString());
        MemberInfoModel info = new MemberInfoModel();
        info.Number = ViewState["membernumberE"].ToString();
        info.MobileTele = Encryption.Encryption.GetEncryptionTele(this.txtEmobile.Text.ToString().Trim());
        info.HomeTele = Encryption.Encryption.GetEncryptionTele(this.Txtjtdh.Text.ToString().Trim());
        info.FaxTele = Encryption.Encryption.GetEncryptionTele(this.Txtczdh.Text.ToString().Trim());
        info.OfficeTele = Encryption.Encryption.GetEncryptionTele(this.Txtbgdh.Text.ToString().Trim());
        info.Email = this.txtEemail.Text.ToString().Trim();

        if (MemInfoEditBLL.UpdmemContact(info))
        {
            cl.AddRecord(info.Number);
            cl.ModifiedIntoLogs(ChangeCategory.Order, info.Number, ENUM_USERTYPE.objecttype5);

            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("001401", "操作成功！") + "');", true);
            this.btnSContact.Visible = false;
            this.btnCContact.Visible = false;
            this.btnEContact.Visible = true;
            SellblContact();
            return;
        }
        else
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("001541", "操作失败！") + "');", true);
            return;
        }
    }
    /// <summary>
    /// 保存银行
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSBank_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }
        ChangeLogs cl = new ChangeLogs("MemberInfo", "ltrim(rtrim(Number))");
        cl.AddRecord(ViewState["membernumberE"].ToString());
        MemberInfoModel info = new MemberInfoModel();
        info.BankBook = Encryption.Encryption.GetEncryptionName(this.lblEname.Text.ToString().Trim());
        info.Number = ViewState["membernumberE"].ToString();
        info.BankCard = Encryption.Encryption.GetEncryptionCard(this.txtEbanknumber.Text.ToString().Trim());
        info.BankAddress = Encryption.Encryption.GetEncryptionAddress(this.txtEbankaddress.Text.ToString().Trim());
        info.Bankbranchname = this.txtEbank.Text.ToString().Trim();
        info.Bank.BankName = this.DdlBank.SelectedValue.ToString().Trim();
        string Country = this.CountryCityPCode2.Country;
        string Province = this.CountryCityPCode2.Province;
        string City = this.CountryCityPCode2.City;
        info.BCPCCode = CommonDataDAL.GetCPCCode(Country, Province, City, CountryCityPCode2.Xian);
        if (MemInfoEditBLL.UpdmemBank(info))
        {
            cl.AddRecord(info.Number);
            cl.ModifiedIntoLogs(ChangeCategory.Order, info.Number, ENUM_USERTYPE.objecttype5);

            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("001401", "操作成功！") + "');", true);
            this.btnSBank.Visible = false;
            this.btnCBank.Visible = false;
            this.btnEBank.Visible = true;
            SellblBank();
            return;
        }
        else
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("001541", "操作失败！") + "');", true);
            return;
        }
    }

    /// <summary>
    /// 返回
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEfh_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript' type='text/javascript'>history.go(-" + ViewState["history"].ToString() + ");</script>");//history.go(-2);
    }

    protected void lkSubmit_Click(object sender, EventArgs e)
    {
        btnSbasic_Click(null, null);
    }
}
