using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using Model;
using BLL.other.Company;
using BLL.CommonClass;
using DAL;

public partial class Company_StoreInfoDetail : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        if (!IsPostBack)
        {
            StoreInfoModel store = null;
            if (Request["id"]!=null&&Request["id"]!="")
            {
                 store=StoreInfoSearchBLL.GetStoreInfoById(Convert.ToInt32(Request["id"]));
            }
            else if (Request["storeid"] != null && Request["storeid"]!="")
            {
                store = StoreInfoSearchBLL.GetStoreInfoByStoreid(Request["storeid"]);
            }
            else
            {
                ScriptHelper.SetScript(Page, "<script>window.history.go(-1)<script>", false);
            }
            if (store != null)
            {
                lblStoreId.Text = store.StoreID;
                lblNumber.Text = store.Number;
                lblName.Text = Encryption.Encryption.GetDecipherName(store.Name);
                if (store.CPCCode.ToString() == "00000")
                {
                    lblLianxifangshi.Text = "";
                }
                else
                {
                    CityModel city = CommonDataBLL.GetCPCCode(store.CPCCode);
                    if (city != null)
                    {
                        lblLianxifangshi.Text = city.Country + city.Province + city.City + store.StoreAddress;
                        lbladdress.Text = city.Country + city.Province + city.City + Encryption.Encryption.GetDecipherAddress(store.StoreAddress);
                    }
                }

                lblPostalCode.Text = store.PostalCode;
                lblfuzherentel.Text = Encryption.Encryption.GetDecipherTele(store.HomeTele);
                lblofficess.Text = Encryption.Encryption.GetDecipherTele(store.OfficeTele);
                if (store.Language < 0)
                {
                    lblLanager.Text = "";
                }
                else
                {
                    lblLanager.Text = LanguageBLL.GetLanguageNameById(store.Language);
                }
                lblMobiletel.Text = Encryption.Encryption.GetDecipherTele(store.MobileTele);
                lblfaxtel.Text = Encryption.Encryption.GetDecipherTele(store.FaxTele);
                lblback.Text = store.Bank.BankName.ToString();
                lblbackcard.Text = Encryption.Encryption.GetDecipherCard(store.BankCard);
                lblEmail.Text = store.Email;
                lblNetAddress.Text = store.NetAddress;
                lblRemark.Text = store.Remark;
                lblDirect.Text = store.Direct;
                if (store.ExpectNum == 0)
                {
                    lblExpectNum.Text = "";
                }
                else
                {
                    lblExpectNum.Text = store.ExpectNum.ToString();
                }
                lblRegisterDate.Text = store.RegisterDate.ToLongDateString();
                int jibie = Convert.ToInt32(store.StoreLevelInt);
                string l = getStoLevelStr(store.StoreID);
                this.lblStoreLevelStr.Text = l.ToString();
                lblTotalMaxMoney.Text = store.TotalMaxMoney.ToString();
                lblFareArea.Text = store.FareArea.ToString();
                lblTotalInvestMoney.Text = store.TotalInvestMoney.ToString("0.00");
                DataTable dt = CommonDataBLL.getStoreCountry(store.CPCCode);
                if (dt != null && dt.Rows.Count > 0)
                {
                    lblCountry.Text = dt.Rows[0]["Country"].ToString() + dt.Rows[0]["Province"].ToString();
                }
            }
            
        }
    }

    protected string getStoLevelStr(string stoid)
    {
        //return CommonDataBLL.GetStoreLevelStr(stoid);
        object o_sl = DAL.DBHelper.ExecuteScalar(@"SELECT b.levelstr FROM storeInfo s inner join bsco_level b on s.StoreLevelInt=b.levelint WHERE StoreID=@sid and b.levelflag=1", new SqlParameter[] {
            new SqlParameter("@sid", stoid)
        }, CommandType.Text);
        if (o_sl != null)
            return o_sl.ToString();
        else
            return "";
    }
}
