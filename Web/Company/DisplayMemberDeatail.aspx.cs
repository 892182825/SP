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
using BLL;

public partial class Company_DisplayMemberDeatail : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now); 
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                ViewState["Number"] = Request.QueryString["id"];
                GetDetial(Request.QueryString["id"]);
            }
            else
            {
                Response.Write("<script>alert('" + GetTran("000128", "参数错误!!") + "');window.location.href='QureyMember.aspx';</script>");
            }
        }
    }
    public void GetDetial(string Number)
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
        if (!BLL.CommonClass.CommonDataBLL.GetRole2(Session["Company"].ToString(),false))
        {
            this.Recommended.Text = "";
        }
        else
        {
            this.Recommended.Text = member.Direct.ToString();
        }
        

        //this.Placement.Text = member.Placement.ToString();
        //this.Recommended.Text = member.Direct.ToString();
        this.Name.Text =Encryption.Encryption.GetDecipherName(member.Name.ToString());
        this.PetName.Text = member.PetName.ToString();
        this.Birthday.Text = member.Birthday.ToString("yyyy-MM-dd");
        if (Convert.ToInt32(member.Sex) == 0)
        {
            this.Sex.Text = GetTran("000095", "女");
        }
        else
        {
            this.Sex.Text = GetTran("000094", "男");
        }
        this.PostolCode.Text = member.PostalCode.ToString();
        this.HomeTele.Text =Encryption.Encryption.GetDecipherTele(member.HomeTele.ToString());
        this.OfficeTele.Text = Encryption.Encryption.GetDecipherTele(member.OfficeTele.ToString());
        this.FaxTele.Text =Encryption.Encryption.GetDecipherTele(member.FaxTele.ToString());
        labEmail.Text = member.Email.ToString();
        this.MoblieTele.Text =member.MobileTele.ToString();
        //this.Country.Text = member.Country.ToString() + member.Province.ToString() + member.City.ToString() + member.Address.ToString();
        this.Country.Text = member.City.Country + member.City.Province + member.City.City +member.City.Xian+Encryption.Encryption.GetDecipherAddress(member.Address);
        this.PaperNumber.Text =Encryption.Encryption.GetDecipherNumber(member.PaperNumber.ToString());
        this.PaperType.Text = member.PaperType.PaperType.ToString();
        this.Bank.Text =member.Bank.BankName.ToString()+member.Bankbranchname.ToString();
//        this.BankAddress.Text = member.BankCountry.ToString() + member.BankProvince.ToString()+member.BankCity.ToString()+member.BankAddress.ToString();
        this.BankAddress.Text = member.BankCity.Country + member.BankCity.Province + member.BankCity.City + Encryption.Encryption.GetDecipherAddress(member.BankAddress.ToString());
        this.BankNum.Text =Encryption.Encryption.GetDecipherCard(member.BankCard.ToString());
        this.BankBook.Text =Encryption.Encryption.GetDecipherName(member.BankBook.ToString());
        this.ExpectNum.Text = member.ExpectNum.ToString();
        this.Remark.Text = member.Remark.ToString();
    }
}
