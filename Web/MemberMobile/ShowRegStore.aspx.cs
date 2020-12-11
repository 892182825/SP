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
using System.Data.SqlClient;
using DAL;

public partial class Member_ShowRegStore : BLL.TranslationBase
{
    public string msg;
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        if (!Page.IsPostBack)
        {
            bindData();
        }
    }
    /// <summary>
    /// 绑定专卖店数据
    /// </summary>
    public void bindData()
    {
        string tableName = "StoreInfo";
        //先判断中间表UnauditedStoreInfo里是否有数据，如果有数据，是未审核；如果没有记录，就是查询storeinfo表里是否有记录，
        DataTable dt1 = DAL.DBHelper.ExecuteDataTable("select isnull(storestate,0) as storestate from StoreInfo where number='" + Session["Member"].ToString() + "'");
        if (dt1.Rows.Count > 0 && int.Parse(dt1.Rows[0]["storestate"].ToString()) == 0)
        {
            this.lblShenhe.Text = GetTran("007525", "未激活");
        }
        else if (dt1.Rows.Count > 0 && int.Parse(dt1.Rows[0]["storestate"].ToString()) == 1)
        {
            this.lblShenhe.Text = GetTran("007524", "已激活");
        }
        else if ((dt1.Rows.Count > 0 && int.Parse(dt1.Rows[0]["storestate"].ToString()) == 2))
        {
            this.lblShenhe.Text = GetTran("001286", "已注销");
        }
        else if ((dt1.Rows.Count > 0 && int.Parse(dt1.Rows[0]["storestate"].ToString()) == 3))
        {
            this.lblShenhe.Text = GetTran("007542", "已冻结");
        }
        string sql = "select top 1 Number,Direct,Name,StoreName,SCPCCode,postalcode,CPCCode,MobileTele,HomeTele,BankCode,BankCard,Email,NetAddress,ExpectNum,Remark,StoreLevelInt,FareArea,FareArea as frea,TotalAccountMoney,StoreID,BranchNumber,StoreAddress  from " + tableName + " where Number='" + Session["Member"].ToString() + "'";
        SqlDataReader sdr = DAL.DBHelper.ExecuteReader(sql);
        if (sdr.Read())
        {
            this.lblNumber.Text = sdr.GetString(0).ToString();
            this.lblDirect.Text = sdr.GetString(1);
            this.lblName.Text = Encryption.Encryption.GetDecipherName(sdr.GetString(2));
            this.lblStoreName.Text = Encryption.Encryption.GetDecipherName(sdr.GetString(3));
            DataTable dt = DBHelper.ExecuteDataTable("select top 1 country,province from city where cpccode like'" + sdr.GetString(4) + "%'");
            if (dt.Rows.Count > 0)
            {
                this.lblSCPCCODE.Text = dt.Rows[0]["country"].ToString() + dt.Rows[0]["province"].ToString();
            }
            //BLL.CommonClass.CommonDataBLL.GetAddressByCode(sdr.GetString(4).ToString());
            this.lblPostalCode.Text = sdr.GetString(5).ToString();
            this.lblAddress.Text = BLL.CommonClass.CommonDataBLL.GetAddressByCode(sdr.GetString(6).ToString());
            this.lblMobileTele.Text = sdr.GetString(7).ToString();
            this.lblHomeTele.Text = sdr.GetString(8).ToString();
            this.lblBankAddress.Text = DAL.DBHelper.ExecuteScalar("select BankName from MemberBank where BankCode='" + sdr.GetString(9).ToString() + "'").ToString();
            this.lblBankCard.Text = sdr.GetString(10).ToString();
            this.lblEmail.Text = sdr.GetString(11).ToString();
            this.lblNetAddress.Text = sdr.GetString(12).ToString();
            this.lblExpect.Text = GetTran("000156", "第") + sdr.GetInt32(13).ToString() + GetTran("000157", "期");
            this.lblRemark.Text = sdr.GetString(14).ToString();
            //this.lblLevel.Text = DBHelper.ExecuteDataTable("select isnull(levelstr,'') as levelstr from bsco_level where levelflag=1 and levelint=" + sdr.GetInt32(15)).Rows[0][0].ToString();
            this.lblFareArea.Text = sdr.GetDecimal(16).ToString();
            this.lblTotalAccountMoney.Text = sdr.GetDecimal(18).ToString();
            this.lblStoreID.Text = sdr.GetString(19).ToString();
            this.labBankBranchname.Text = sdr.GetString(20);
            this.lblAddress.Text = this.lblAddress.Text + sdr.GetString(21);//加上StoreAddress
        }
        sdr.Close();
        sdr.Dispose();
    }
}
