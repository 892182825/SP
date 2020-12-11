using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BLL.other.Company;
using BLL.CommonClass;
using DAL;
using Model.Other;
using Model;
using System.Text;
using System.IO;
using System.Drawing;
using BLL.other.Member;
using BLL.Registration_declarations;


public partial class Company_MemberInfoModify : BLL.TranslationBase
{
    protected string msg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Permissions.CheckManagePermission(EnumCompanyPermission.CustomerModifyMemberInfoKaHao);
        Response.Cache.SetExpires(DateTime.Now);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));

        if (!IsPostBack)
        {
            BtnUpdate.Attributes.Add("onclick", "return Verify()");
            CommonDataBLL.BindCountry_Bank("中国", MemberBank);
            CommonDataBLL.BindPaperType(PaperType);
            if (Request.QueryString["id"] != null)
            {
                // CommonDataBLL.bing
                //保存编号
                DataTable dt = StoreInfoEditBLL.bindCountry();
                foreach (DataRow item in dt.Rows)
                {
                    this.DropDownList1.Items.Add(new ListItem(item["name"].ToString(), item["name"].ToString()));
                }
                string number = Request.QueryString["id"].ToString();
                ViewState["Number"] = number; //存储到试图中

                getMemberInfo(number); //绑定信息

                System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "paperchange();", true);
            }
            else
            {
                Response.Redirect("QueryMemberInfo.aspx");
            }

            Translations();
        }
    }
    private void Translations()
    {
        this.BtnUpdate.Text = GetTran("000092", "修 改");
        this.TranControls(this.Sex,
                new string[][]{
                    new string []{"000094","男"},
                    new string []{"000095","女"}});
        this.TranControls(this.Button1, new string[][] { new string[] { "000096", "返 回" } });
        //this.TranControls(this.RegularExpressionValidator6, new string[][] { new string[] { "000076", "不能输入字母,请重输！" } });
        this.TranControls(this.RegularExpressionValidator5, new string[][] { new string[] { "006827", "只能输入数字！" } });
        this.TranControls(this.RegularExpressionValidator3, new string[][] { new string[] { "006828", "格式不正确！" } });

    }
    protected void BtnUpdate_Click1(object sender, EventArgs e)
    {
        BLL.Registration_declarations.RegistermemberBLL registermemberBLL = new BLL.Registration_declarations.RegistermemberBLL();
        if (this.Number.Text == "")
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("000129", "对不起，会员编号不能为空！") + "');", true);
            return;
        }
        if (this.Name.Text == "")
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("000131", "对不起，会员姓名不能为空！") + "');", true);
            return;
        }
        if (MoblieTele.Text == "")
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("000000", "移动电话不能为空！") + "');", true);
            return;
        }
        else
        {
            if (MoblieTele.Text.Length != 11)
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("000000", "移动电话格式不正确！") + "');", true);
                return;
            }
        }
        if (this.PostolCode.Text == "")
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("000134", "对不起，邮编不能为空！") + "');", true);
            return;
        }
        if (this.PetName.Text == "")
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("000136", "对不起，会员昵称不能为空！") + "');", true);
            return;
        }

        if (this.PaperNumber.Text == "" && this.PaperType.SelectedValue.Trim() != "P000")
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("000140", "对不起，证件号码不能为空！") + "');", true);
            return;
        }


        if (this.Address.Text == "")
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("000146", "对不起，地址不能为空！") + "');", true);
            return;
        }
        //检查会员生日
        if (this.PaperType.SelectedValue.Trim() != "P001")    //如果证件类型不是身份证 则判断会员生日是否输入
        {

            if (registermemberBLL.CheckBirthDay(this.Birthday.Text) == "error")
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "alert('" + GetTran("000148", "对不起，请选择正确的出生日期！") + "');", true);

                return;
            }

            //验证年龄是否大于18岁
            string alert = registermemberBLL.AgeIs18(this.Birthday.Text.Trim());
            if (alert != null)
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "alert('" + alert + "');", true);

                return;

            }
        }


        //检测身份证需要新方法
        string birthdaysex = "";
        if (this.PaperType.SelectedValue.Trim() == "P001")
        {
            string result = BLL.Registration_declarations.CheckMemberInfo.CHK_IdentityCard(CommonDataBLL.quanjiao(this.PaperNumber.Text.Trim()));
            if (result.IndexOf(",") <= 0)
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "alert('" + result + "');", true);
                return;
            }
            else
            {
                birthdaysex = result; // 从身份证号中取到生日和性别组成的字符串用逗号分隔
            }
        }

        string oldChangeInfo = "";
        bool flag = false;
        StringBuilder changeInfo = new StringBuilder();
        ChangeLogs cl = new ChangeLogs("MemberInfo", "ltrim(rtrim(Number))");
        cl.AddRecord(ViewState["Number"].ToString());
        MemberInfoModel mem = MemInfoEditBLL.getMemberInfo(ViewState["Number"].ToString());

        oldChangeInfo = mem.ChangeInfo.ToString();
        changeInfo.Append(mem.ChangeInfo.ToString());

        changeInfo.Append(GetTran("000151", "管理员 "));
        changeInfo.Append(Session["Company"]);
        changeInfo.Append(GetTran("000153", " 在 "));
        changeInfo.Append(DateTime.Now.ToString());
        changeInfo.Append(GetTran("000156", " 第 "));
        changeInfo.Append(Session["ExpectNum"].ToString());
        changeInfo.Append(GetTran("000157", " 期 "));
        changeInfo.Append(GetTran("000161", " 修改了如下内容") + "：");

        //判断用户是否修改了姓名
        if (this.Name.Text != Encryption.Encryption.GetDecipherName(mem.Name.ToString().Trim()))
        {
            flag = true;
            changeInfo.Append("\n" + GetTran("000164", " 修改了姓名，原姓名") + "：");
            changeInfo.Append(mem.Name.ToString());
            changeInfo.Append("；" + GetTran("000166", " 新姓名") + "：");
            changeInfo.Append(this.Name.Text.ToString());
        }
        //判断用户是否修改了昵称
        if (this.PetName.Text != mem.PetName.ToString())
        {
            flag = true;
            changeInfo.Append("\n" + GetTran("000168", " 修改了昵称，原昵称") + "：");
            changeInfo.Append(mem.PetName.ToString());
            changeInfo.Append("；" + GetTran("000170", " 新昵称") + "：");
            changeInfo.Append(this.PetName.Text.ToString());
        }



        //判断是否更改了证件号码和证件类型
        if ((this.PaperType.SelectedValue.ToString() != mem.Papertypecode) || (this.PaperNumber.Text.Trim() != Encryption.Encryption.GetDecipherNumber(mem.PaperNumber.Trim())))
        {
            if (this.PaperType.ToString().Trim() != "")
            {
                flag = true;
                changeInfo.Append("\n" + GetTran("000202", " 修改了证件类型或证件号码，原证件类型") + "：");
                changeInfo.Append(mem.PaperType.ToString());
                changeInfo.Append("，" + GetTran("000203", " 原证件号码") + "：");
                changeInfo.Append(mem.PaperNumber.ToString());
                changeInfo.Append("；" + GetTran("000206", " 新证件类型") + "：");
                changeInfo.Append(this.PaperType.ToString());
                changeInfo.Append("，" + GetTran("000207", " 新证件号码") + "：");
                changeInfo.Append(this.PaperNumber.ToString());
            }
        }

        //判断用户是否修改了开户行
        if (this.MemberBank.SelectedValue.ToString() != mem.Bank.ToString())
        {
            flag = true;
            changeInfo.Append("\n" + GetTran("000210", " 修改了开户行，原开户行") + "：");
            changeInfo.Append(mem.Bank.ToString());
            changeInfo.Append("；" + GetTran("000211", " 新开户行") + "：");
            changeInfo.Append(this.MemberBank.SelectedValue.ToString());
        }


        //判断用户是否修改了银行帐号
        if (this.BankNum.Text.ToString() != mem.BankCard.ToString())
        {
            flag = true;
            changeInfo.Append("\n" + GetTran("000212", " 修改了银行帐号，原银行帐号") + "：");
            changeInfo.Append(mem.BankCard.ToString());
            changeInfo.Append("；" + GetTran("000216", " 新银行帐号") + "：");
            changeInfo.Append(this.BankNum.Text.ToString());
            changeInfo.Append("\n");
        }



        string Number = this.Number.Text.ToString();
        string Placement = this.Placement.Text.ToString();
        string Direct = this.Recommended.Text.ToString();
        string Name = Encryption.Encryption.GetEncryptionName(this.Name.Text.ToString().Trim());
        string PetName = this.PetName.Text.ToString();
        DateTime Birthday = DateTime.Parse(this.Birthday.Text.ToString());
        int Sex = 0;
        if (this.PaperType.SelectedValue.Trim() == "P001")
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
            Birthday = DateTime.Parse(this.Birthday.Text.ToString());

            if (this.Sex.SelectedValue.ToString() == "0")
            {
                Sex = 0;
            }
            else
            {
                Sex = 1;
            }
        }
        //邮编号
        string PostalCode = this.PostolCode.Text.ToString();
        //家庭电话号码      
        string HomeTele = this.Txtjtdh.Text.Trim() == "电话号码" ? "" : Encryption.Encryption.GetEncryptionTele(this.Txtjtdh.Text.ToString().Trim());
        //办公电话号码
        string OfficeTele = this.Txtbgdh.Text.Trim() == "电话号码" ? "" : Encryption.Encryption.GetEncryptionTele(this.Txtbgdh.Text.ToString().Trim());
        //传真电话号
        string FaxTele = this.Txtczdh.Text.Trim() == "电话号码" ? "" : Encryption.Encryption.GetEncryptionTele(this.Txtczdh.Text.ToString().Trim());
        //手机号
        string MobileTele = Encryption.Encryption.GetEncryptionTele(this.MoblieTele.Text.ToString().Trim());
        string Country = this.CountryCity1.Country; //国家
        string Province = this.CountryCity1.Province;  //省份
        string City = this.CountryCity1.City;  //城市
        string Xian = this.CountryCity1.Xian;
        //详细地址
        string Address = Encryption.Encryption.GetEncryptionAddress(this.Address.Text.ToString().Trim());
        string PaperNumber = Encryption.Encryption.GetEncryptionNumber(this.PaperNumber.Text.ToString().Trim());//证件号
        string PaperType = this.PaperType.SelectedValue.ToString().Trim(); //证件类型
        string Bank = this.MemberBank.SelectedValue.ToString(); //银行名称
        string BankAddress = Encryption.Encryption.GetEncryptionAddress(this.BankAdderss.Text.ToString().Trim());  //银行地址
        //银行所属国家
        string BankCountry = this.CountryCity2.Country;
        string BankProvince = this.CountryCity2.Province;//银行所属省份
        string BankCity = this.CountryCity2.City;  //银行所属城市
        string BankCard = Encryption.Encryption.GetEncryptionCard(this.BankNum.Text.ToString().Trim());//银行卡号
        string BankBook = Encryption.Encryption.GetEncryptionName(this.Name.Text.ToString().Trim()); //开户名
        //当前期数
        int ExpectNum = Convert.ToInt32(this.ExpectNum.Text.ToString());
        if (this.Remark.Text.Length > 500)  //备注在500字以内
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("006708", "对不起，备注输入的字符太多,最多500个字符！") + "');", true);
            return;
        };
        string Remark = this.Remark.Text.ToString();
        string OrderId = this.OrderID.Text.ToString();
        string ChangeInfo = "";
        if (flag == true)
        {
            ChangeInfo = changeInfo.ToString(); //修改信息提示
        }
        else
        {
            ChangeInfo = oldChangeInfo;
        }

        //
        string OperateIp = HttpContext.Current.Request.UserHostAddress.ToString();
        string OperaterNum = CommonDataBLL.OperateBh;
        MemberInfoModel info = new MemberInfoModel();
        info.Number = Number;
        info.Placement = ViewState["Placement"].ToString();
        info.Direct = ViewState["Direct"].ToString();
        info.Name = Name;
        info.PetName = PetName;
        info.Birthday = Birthday;
        info.Sex = Sex;
        info.PostalCode = PostalCode;
        info.StoreID = "8888888888";
        info.HomeTele = HomeTele;
        info.OfficeTele = OfficeTele;
        info.MobileTele = MobileTele;
        info.FaxTele = FaxTele;
        info.CPCCode = CommonDataDAL.GetCPCCode(Country, Province, City, Xian);
        info.Address = Address;
        info.Papertypecode = PaperType;
        info.PaperNumber = PaperNumber;
        info.BankCode = Bank;
        info.BankAddress = BankAddress;
        info.BankCard = BankCard;
        info.BankBook = BankBook;
        info.ExpectNum = ExpectNum;
        info.Remark = Remark;
        info.OrderID = OrderId;
        info.ChangeInfo = changeInfo.ToString();
        info.OperateIp = OperateIp;
        info.OperaterNum = OperaterNum;
        info.BCPCCode = CommonDataDAL.GetCPCCode(BankCountry, BankProvince, BankCity);
        info.Bankbranchname = this.txtEbank.Text;
        info.PhotoPath = "";
        int jjtx = Convert.ToInt32(this.rbtJj.SelectedValue);

        updMemberInfo(info);  // 修改信息 
        cl.AddRecord(info.Number);   //记录操作
        //记录日志
        cl.ModifiedIntoLogs(ChangeCategory.company0, info.Number, ENUM_USERTYPE.objecttype5);
    }

    public void getMemberInfo(string Number)
    {
        MemberInfoModel member = MemInfoEditBLL.getMemberInfo(Number);

        this.Number.Text = member.Number.ToString();
        if (!BLL.CommonClass.CommonDataBLL.GetRole2(Session["Company"].ToString(), true))
        {
            this.Placement.Text = "";
        }
        else
        {
            this.Placement.Text = member.Placement.ToString();
        }
        if (!BLL.CommonClass.CommonDataBLL.GetRole2(Session["Company"].ToString(), false))
        {
            this.Recommended.Text = "";
        }
        else
        {
            this.Recommended.Text = member.Direct.ToString();
        }

        ViewState["Placement"] = member.Placement.ToString();
        ViewState["Direct"] = member.Direct.ToString();
        this.Name.Text = Encryption.Encryption.GetDecipherName(member.Name.ToString().Trim());
        this.PetName.Text = member.PetName.ToString();
        this.Birthday.Text = member.Birthday.ToShortDateString().ToString();
        if (Convert.ToInt32(member.Sex) == 0)
        {
            this.Sex.SelectedValue = "0";
        }
        else
        {
            this.Sex.SelectedValue = "1";
        }
        this.PostolCode.Text = member.PostalCode.ToString();

        this.Txtjtdh.Text = Encryption.Encryption.GetDecipherTele(member.HomeTele.ToString().Trim());
        if (this.Txtjtdh.Text.Trim() == "")
        {
            this.Txtjtdh.Text = "电话号码";
            this.Txtjtdh.Style.Add("color", "gray");
        }
        Txtbgdh.Text = Encryption.Encryption.GetDecipherTele(member.OfficeTele);
        if (this.Txtbgdh.Text.Trim() == "")
        {
            this.Txtbgdh.Text = "电话号码";
            this.Txtbgdh.Style.Add("color", "gray");
        }

        Txtczdh.Text = Encryption.Encryption.GetDecipherTele(member.FaxTele);
        if (this.Txtczdh.Text.Trim() == "")
        {
            this.Txtczdh.Text = "电话号码";
            this.Txtczdh.Style.Add("color", "gray");
        }

        this.MoblieTele.Text = Encryption.Encryption.GetDecipherTele(member.MobileTele.ToString().Trim());

        this.CountryCity1.SelectCountry(member.City.Country, member.City.Province, member.City.City, member.City.Xian);
        this.Address.Text = Encryption.Encryption.GetDecipherAddress(member.Address.ToString().Trim());

        this.PaperType.SelectedValue = member.PaperType.PaperTypeCode.ToString().Trim();

        this.PaperNumber.Text = Encryption.Encryption.GetDecipherNumber(member.PaperNumber.ToString().Trim());

        member.Bank = new MemberBankModel();
        if (member.BankCode.ToString() == "000000")
        {
            this.DropDownList1.SelectedValue = DBHelper.ExecuteScalar("select top 1 name from country").ToString();
        }
        else
        {
            object obj = DBHelper.ExecuteScalar("select b.name from memberbank a,country b where a.countrycode=b.id and bankcode='" + member.BankCode.ToString() + "'");
            if (obj != null)
            {
                this.DropDownList1.SelectedValue = obj.ToString();
            }
            else
            {
                this.DropDownList1.SelectedValue = "中国";
                //DropDownList1.SelectedValue = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByValue("")).ToString();
            }

        }

        CommonDataBLL.BindCountry_Bank(this.DropDownList1.SelectedValue.ToString(), MemberBank);

        this.MemberBank.SelectedValue = member.BankCode;
        this.BankAdderss.Text = Encryption.Encryption.GetDecipherAddress(member.BankAddress.ToString().Trim());

        this.CountryCity2.SelectCountry(member.BankCity.Country, member.BankCity.Province, member.BankCity.City, member.BankCity.Xian);

        this.BankNum.Text = Encryption.Encryption.GetDecipherCard(member.BankCard.ToString().Trim());
        this.BankBook.Text = Encryption.Encryption.GetDecipherName(member.BankBook.ToString().Trim());

        this.ExpectNum.Text = member.ExpectNum.ToString();
        this.Remark.Text = member.Remark.ToString();
        this.OrderID.Text = member.OrderID.ToString();
        this.txtEbank.Text = member.Bankbranchname;
        if (member.PhotoPath != "")
        {
            this.img1.ImageUrl = ".." + member.PhotoPath;
        }
        else
        {
            this.img1.ImageUrl = "../images/pht.GIF";
        }

        ViewState["PhotoPath"] = member.PhotoPath;
        Session["ExpectNum"] = member.ExpectNum.ToString();
    }

    public void updMemberInfo(MemberInfoModel info)
    {
        MemInfoEditBLL meb = new MemInfoEditBLL();
        int i = 0;
        i = meb.updateMember(info);
        if (i > 0)
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("000222", "修改成功！") + "');", true);
            getMemberInfo(ViewState["Number"].ToString());
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("000225", "修改失败！") + "')</script>");
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("QueryMemberInfo.aspx");
        //Response.Write("<script>window.history.go(-1);</script>");
    }

    /// <summary>
    /// 查找邮编
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdCode_Click(object sender, EventArgs e)
    {
        string result = new RegistermemberBLL().GetAddressCode(this.CountryCity1.Xian.ToString());
        this.PostolCode.Text = result;
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        CommonDataBLL.BindCountry_Bank(this.DropDownList1.SelectedValue.ToString(), MemberBank);
    }

}