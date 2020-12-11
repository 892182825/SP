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
using DAL;
using BLL.other.Company;
using Model.Other;
using System.Collections.Generic;
using BLL.CommonClass;
using System.Data.SqlClient;

public partial class Company_AddProvider : BLL.TranslationBase
{
    protected string msg = "";
    protected string num = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        if (!IsPostBack)
        { 
            Permissions.CheckManagePermission(EnumCompanyPermission.CustomerProviderViewEditEdit);
            AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));

            BindDate();
            if (Request.QueryString["id"] != null&&Request.QueryString["id"] != "" )
            {
                int providerId = Convert.ToInt32(Request.QueryString["id"]);
                num = providerId.ToString();
                int isExistCount = ProviderManageBLL.ProviderIdIsExist(providerId);
                if (isExistCount > 0)
                {
                    BindInfo(providerId);
                }

                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("001146", "对不起，该供应商不存在或者已经被删除！") + "');location.href('Provider_ViewEdit.aspx')</script>");
                    return;
                }
                TranControls(lab, new string[][] { new string[] { "001025", "供应商修改" } });
            }

            else
            {
                lab.Text = GetTran("000973", "添加供应商");
            }
            Translations_More(); 
        }        
    }
    /// <summary>
    /// 修改时绑定信息
    /// </summary>
    /// <param name="id"></param>
    private void BindInfo(int id)
    {
        RequiredFieldValidator1.Visible = false;
        txtnumber.Visible = false;
        lblNumber.Visible = true;
        lblInfo.Visible = false;

        ViewState["id"] = id;
        ProviderInfoModel provider = new ProviderInfoModel();
        provider = ProviderManageBLL.GetProviderInfoById(id);

        txtAddress.Text = provider.Address;
        txtBankAddress.Text = provider.BankAddress;

        if (provider.BankName.ToString() != "")
        {
            this.ddlCountry.SelectedValue = ProviderManageBLL.GetCountryByBank(provider.BankName);
        }

        ddlCountry_SelectedIndexChanged(null, null);
        ddlBankName.SelectedValue = provider.BankName;
        txtBankNumber.Text = provider.BankNumber;
        txtDutyNumber.Text = provider.DutyNumber;
        txtEmail.Text = provider.Email;
        txtFax.Text = provider.Fax;
        txtForShort.Text = provider.ForShort;
        txtLinkMan.Text = provider.LinkMan;
        txtMobile.Text = provider.Mobile;
        txtName.Text = provider.Name;
        lblNumber.Text = provider.Number;
        txtRemark.Text = provider.Remark;
        txtTelephone.Text = provider.Telephone;
        txtUrl.Text = provider.Url;                 
    }
    /// <summary>
    /// 绑定国家和银行
    /// </summary>
    private void BindDate()
    {
        DataTable items = StoreInfoEditBLL.bindCountry();
        foreach (DataRow item in items.Rows)
        {
            this.ddlCountry.Items.Add(new ListItem(item["name"].ToString(), item["name"].ToString()));
        }
        ddlCountry_SelectedIndexChanged(null, null);
    }
    /// <summary>
    /// Translation
    /// </summary>
    protected void Translations_More()
    {
     
        TranControls(btnSave, new string[][] { new string[] { "001124", "保 存" } });
        TranControls(btnReset, new string[][] { new string[] { "006812", "重 置" } });
        TranControls(RequiredFieldValidator1, new string[][] { new string[] { "002125", "必填" } });
        TranControls(RegularExpressionValidator1, new string[][] { new string[] { "000854", "编号格式错误" } });
        TranControls(RequiredFieldValidator2, new string[][] { new string[] { "002125", "必填" } });
        TranControls(RegularExpressionValidator6, new string[][] { new string[] { "000076", "不能输入字母,请重输" } });
        TranControls(RegularExpressionValidator3, new string[][] { new string[] { "001267", "电子邮箱格式不对" } });
        TranControls(RegularExpressionValidator2, new string[][] { new string[] { "001256", "你输入的网址格式不对，请输入格式为http://1.com" } });
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if ( Request.QueryString["id"] != null&&Request.QueryString["id"] != "")
        {
            ProviderInfoModel provider = new ProviderInfoModel();
            provider.Address = txtAddress.Text.Trim();
            provider.BankAddress = txtBankAddress.Text.Trim();
            provider.BankName = ddlBankName.SelectedValue;
            provider.BankNumber = txtBankNumber.Text.Trim();
            provider.ID = Convert.ToInt32(ViewState["id"]);
            provider.DutyNumber = txtDutyNumber.Text.Trim();
            provider.Email = txtEmail.Text.Trim();
            provider.Fax = txtFax.Text.Trim();
            provider.ForShort = txtForShort.Text.Trim();
            provider.LinkMan = txtLinkMan.Text.Trim();

            provider.Mobile = txtMobile.Text.Trim();
            provider.Name = txtName.Text.Trim();
            provider.Number = lblNumber.Text.Trim();
            provider.Remark = txtRemark.Text.Trim();
            provider.Telephone = txtTelephone.Text.Trim();

            provider.Url = txtUrl.Text.Trim();

            provider.OperateIP = Request.UserHostAddress;
            provider.OperateNum = CommonDataBLL.OperateBh;
            provider.Status = 1;
            provider.PermissionMan = "";

            int providerNameIsExist = ProviderManageBLL.ProviderNameIsExist(provider.ID, provider.Name);
            if (providerNameIsExist > 0)
            {
                this.msg = "<script language='javascript'>alert('" + GetTran("001170", "该供应商名称已被注册，请换一个供应商名称！") + "');</script>";
                return;
            }
            BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("ProviderInfo", "(ltrim(rtrim(id)))");
            cl_h_info.AddRecord(Convert.ToInt32(provider.ID));
            int updCount = ProviderManageBLL.UpdatePrivider(provider);
            if (updCount > 0)
            {
                cl_h_info.AddRecord(Convert.ToInt32(provider.ID));//不能放到事务中  修改数据后
                cl_h_info.ModifiedIntoLogs(BLL.CommonClass.ChangeCategory.company6, Session["Company"].ToString(), BLL.CommonClass.ENUM_USERTYPE.objecttype4);//不能放到事务中

                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("000222", "修改成功！") + "');location.href='Provider_ViewEdit.aspx'</script>");
            }
            else
            {
                ScriptHelper.SetAlert(this.Page, GetTran("001190", "修改失败，请检查你输入的信息！"));
            }            
        }

        //Add Provider
        else
        {
            string number = DisposeString.DisString(txtnumber.Text.Trim());
            if (ProviderManageBLL.GetProviderinfoNumber(number))
            {
                this.msg = "<script language='javascript'>alert('" + GetTran("001162", "该供应商编号已被注册，请换一个供应商编号！") + "');</script>";
                return;
            }
            if (ProviderManageBLL.ProviderNameIsExist(txtName.Text.Trim()) > 0)
            {
                this.msg = "<script language='javascript'>alert('" + GetTran("001170", "该供应商名称已被注册，请换一个供应商名称！") + "');</script>";
                return;
            }
            ProviderInfoModel provider = new ProviderInfoModel();
            provider.Address = DisposeString.DisString(txtAddress.Text.Trim());
            provider.BankAddress = DisposeString.DisString(txtBankAddress.Text.Trim());
            provider.BankName = DisposeString.DisString(ddlBankName.SelectedValue);
            provider.BankNumber = DisposeString.DisString(txtBankNumber.Text.Trim());
            provider.DutyNumber = DisposeString.DisString(txtDutyNumber.Text.Trim());
            provider.Email = DisposeString.DisString(txtEmail.Text.Trim());
            provider.Fax = DisposeString.DisString(txtFax.Text.Trim());
            provider.ForShort = DisposeString.DisString(txtForShort.Text.Trim());
            provider.LinkMan = DisposeString.DisString(txtLinkMan.Text.Trim());
            provider.Mobile = DisposeString.DisString(txtMobile.Text.Trim());
            provider.Name = DisposeString.DisString(txtName.Text.Trim());

            provider.Remark= DisposeString.DisString(txtRemark.Text.Trim());
            provider.Telephone = DisposeString.DisString(txtTelephone.Text.Trim());
            provider.Url = DisposeString.DisString(txtUrl.Text.Trim());
            provider.OperateIP = Request.UserHostAddress;
            provider.OperateNum = CommonDataBLL.OperateBh;
            provider.Status = 1;
            provider.Number = number;
            provider.PermissionMan = "";

            int addCount = ProviderManageBLL.AddPrivider(provider);
            if (addCount > 0)
            {
                this.msg = "<script language='javascript'>alert('" + GetTran("001194", "保存成功！") + "');location.href='Provider_ViewEdit.aspx'</script>";
            }
            else
            {
                ScriptHelper.SetAlert(this.btnSave, GetTran("001177", "添加失败，请检查你输入的信息！"));
            }
        }
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBankName.Items.Clear();
        DataTable dt = CommonDataBLL.GetCountry_Bank(ddlCountry.SelectedValue);
        foreach (DataRow dr in dt.Rows)
        {
            ddlBankName.Items.Add(new ListItem(dr["bankname"].ToString(), dr["bankcode"].ToString()));
        }
    }

    /// <summary>
    /// Reset the value of the text
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtnumber.Text = "";
        txtBankAddress.Text = "";
        txtBankNumber.Text = "";
        txtDutyNumber.Text = "";
        txtEmail.Text = "";
        txtFax.Text = "";
        txtForShort.Text = "";
        txtLinkMan.Text = "";
        txtMobile.Text = "";
        txtName.Text = "";
        txtRemark.Text = "";
        txtTelephone.Text = "";
        txtUrl.Text = "";
        ddlCountry.SelectedIndex = 0;
        ddlBankName.SelectedIndex = 0;
    }
}
