using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BLL.other.Company;
using BLL.CommonClass;

public partial class UserControl_UCBank : System.Web.UI.UserControl
{
    protected BLL.TranslationBase tran = new BLL.TranslationBase();
    private bool _hasBind = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!_hasBind)
            {
                BindCountryBank();
            }
        }
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        int countryid = -1;
        if (this.DropDownList1.Items.Count > 0)
        {
            countryid = int.Parse(this.DropDownList1.SelectedValue.ToString());
            CommonDataBLL.BindCountryBankByCountryID(countryid, ddlBank);
        }
    }
    private void BindCountryBank()
    {
        DataTable dtct = StoreInfoEditBLL.bindCountry();
        foreach (DataRow item in dtct.Rows)
        {
            this.DropDownList1.Items.Add(new ListItem(item["name"].ToString(), item["id"].ToString()));
        }
        int countryid = -1;
        if (this.DropDownList1.Items.Count > 0)
        {
            countryid = int.Parse(this.DropDownList1.SelectedValue.ToString());
            CommonDataBLL.BindCountryBankByCountryID(countryid, ddlBank);
            _hasBind = true;
        }
    }
    /// <summary>
    /// 国家ID
    /// </summary>
    public int CountryID
    {
        get
        {
            int countryid = -1;
            foreach (ListItem li in this.DropDownList1.Items)
            {
                if (li.Selected)
                {
                    countryid = int.Parse(li.Value);
                }
            }
            return countryid;
        }
        set
        {
            if (!_hasBind)
                BindCountryBank();
            this.DropDownList1.SelectedIndex = -1;
            foreach (ListItem li in this.DropDownList1.Items)
            {
                if (li.Value==value .ToString ())
                {
                    li.Selected = true;
                }
            }
        }
    }
    /// <summary>
    /// 国家名称
    /// </summary>
    public string  CountryName
    {
        get
        {
            string  countryname=string .Empty ;
            foreach (ListItem li in this.DropDownList1.Items)
            {
                if (li.Selected)
                {
                    countryname =li.Text;
                }
            }
            return countryname;
        }
        set
        {
            if (!_hasBind)
                BindCountryBank();
            this.DropDownList1.SelectedIndex = -1;
            foreach (ListItem li in this.DropDownList1.Items)
            {
                if (li.Value == value.ToString())
                {
                    li.Selected = true;
                }
            }
        }
    }

    /// <summary>
    /// 银行代码
    /// </summary>
    public string  BankCode
    {
        get
        {
            string  bankCode = string .Empty ;
            foreach (ListItem li in this.ddlBank.Items)
            {
                if (li.Selected)
                {
                    bankCode = li.Value ;
                }
            }
            return bankCode;
        }
        set
        {
            this.ddlBank.SelectedIndex = -1;
            foreach (ListItem li in this.ddlBank.Items)
            {
                if (li.Value == value.ToString())
                {
                    li.Selected = true;
                }
            }
        }
    }
    /// <summary>
    /// 银行名称
    /// </summary>
    public string BankName
    {
        get
        {
            string countryname = string.Empty;
            foreach (ListItem li in this.ddlBank.Items)
            {
                if (li.Selected)
                {
                    countryname = li.Text;
                }
            }
            return countryname;
        }
        set
        {
            this.ddlBank.SelectedIndex = -1;
            foreach (ListItem li in this.ddlBank.Items)
            {
                if (li.Value == value.ToString())
                {
                    li.Selected = true;
                }
            }
        }
    }
}
