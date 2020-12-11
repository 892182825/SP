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
using BLL.Logistics;
using Model;
using BLL.CommonClass;
using Standard;
using Model.Other;
using System.Collections.Generic;
using DAL;

public partial class Company_AddThirdLogistics : BLL.TranslationBase
{
    ThirdLogisticsDLL thirdLogisticsDLL = new ThirdLogisticsDLL();
    //用于添加
    private bool AddThirdLogistics()
    {
        LogisticsModel logisticsModel = Set();
                  
        if (logisticsModel == null)
        {
            ScriptHelper.SetAlert(Page, "请输入正确的批准日期");
            //this.txtDate.Focus();
            return false;
        }
        else
        {
            return thirdLogisticsDLL.AddLogistics(Set());
        }

    }

    private LogisticsModel Set()
    { 
        LogisticsModel logisticsModel = new LogisticsModel();
        logisticsModel.Number = this.txtnumber.Text.Trim();
        logisticsModel.LogisticsCompany = txtName.Text.Trim();
        logisticsModel.Telephone1 = txtTelephone1.Text.Trim();
        logisticsModel.Telephone2 = txtTelephone2.Text.Trim();
        logisticsModel.Telephone3 = txtTelephone3.Text.Trim();
        logisticsModel.Telephone4 = txtTelephone4.Text.Trim();
        logisticsModel.StoreAddress = txtStoreAddress.Text.Trim();
        logisticsModel.PostalCode = txtPostalCode.Text.Trim();
        logisticsModel.Principal = txtPrincipal.Text.Trim();
        //logisticsModel.Bank = txtBank.Text.Trim();
        logisticsModel.Bank = ddlBank.SelectedItem.Text;
        logisticsModel.BankCard = txtBankCard.Text.Trim();
        logisticsModel.Tax = txtTax.Text.Trim();
        logisticsModel.LicenceCode = txtLicenceCode.Text.Trim();
        logisticsModel.Remark = txtRemark.Text.Trim();
        logisticsModel.Country = CountryCity1.Country;
        logisticsModel.Province = CountryCity1.Province;
        logisticsModel.City = CountryCity1.City;
        logisticsModel.Xian = CountryCity1.Xian;
        string sgas = "select Cpccode from city where Country='" + CountryCity1.Country + "' and Province='" + CountryCity1.Province + "' and City='" + CountryCity1.City + "' and xian='" + CountryCity1.Xian + "'";
        string Cpccode = DAL.DBHelper.ExecuteScalar(sgas) + "";
        logisticsModel.Cpccode = Cpccode;
        logisticsModel.BankCode = DAL.DBHelper.ExecuteScalar("select BankCode from MemberBank where BankId='" + ddlBank.SelectedValue + "'") + "";

        try
        {
            logisticsModel.RigisterDate = Convert.ToDateTime(txtDate.Text.ToString());
        }
        catch (FormatException )
        {
            return null;
        }
       logisticsModel.City = CountryCity1.City;
       logisticsModel.Country = CountryCity1.Country;
       logisticsModel.Province = CountryCity1.Province;
       logisticsModel.Xian = CountryCity1.Xian;
        logisticsModel.OperateIP = Request.UserHostAddress;
        return logisticsModel;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));

        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.CustomerThirdLogisticsAdd);
        this.DataBind();
        if (!IsPostBack)
        {
            
            BindBank_List();
            //进入修改记录
            if (Request.QueryString["id"] != null)
            {
                //权限
                
                ViewState["mode"] = "update";
                int vid = Convert.ToInt32(Request.QueryString["id"]);
                //保存id编号
                ViewState["id"] = vid;
                this.messagebox.Text = GetTran("002122", "修改物流公司基本信息");
                //得到所有信息用于初始化，填冲到各个文本框中
                txtnumber.ReadOnly = true;
                lblNumber.Visible = true;
                lblNumber.Text = GetTran("002124", "编号不可修改");
                showDetail(vid);

                txtDate.Enabled = false;
            }
            else
            {
                ViewState["mode"] = "add";
                this.messagebox.Text = GetTran("002119", "添加物流公司基本信息");
                lblNumber.Visible = true;
                lblNumber.Text = GetTran("002125", "必填");
                txtDate.Text = DateTime.Now.ToShortDateString();
            }
            if (this.CountryCity1.City != "")
            
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>GetCCode_s2('" + this.CountryCity1.Xian + "')</script>");
        }

        SetFanY();
    
    }

    public void SetFanY()
    {
        this.TranControls(this.btnSave, new string[][] { new string[] { "001124", "保 存" } });
        this.TranControls(this.btnClear, new string[][] { new string[] { "000096", "返 回" } });
    }

    //得到所有信息用于初始化，填冲到各个文本框中
    private void showDetail(int id)
    {
        LogisticsModel logistics = thirdLogisticsDLL.GetThirdLogisticsInit(id);

        txtnumber.Text = logistics.Number.ToString();//绑定  物流公司编号
        txtName.Text = logistics.LogisticsCompany.ToString();//绑定  物流公司名称
        txtTelephone2.Text = logistics.Telephone2.ToString();//绑定  办公电话
        txtStoreAddress.Text = logistics.StoreAddress.ToString();//绑定  办公地址
        txtPostalCode.Text = logistics.PostalCode;//绑定  邮编
        txtTelephone3.Text = logistics.Telephone3.ToString();//绑定  负责人手机
        txtTelephone1.Text = logistics.Telephone1.ToString();//绑定  负责人电话
        txtTelephone4.Text = logistics.Telephone4.ToString();//绑定  传真电话
        txtPrincipal.Text = logistics.Principal.ToString();//绑定  负责人姓名
        //txtBank.Text = logistics.Bank.ToString();//绑定  银行帐号
        ddlBank.SelectedItem.Text = logistics.Bank.ToString();
        txtBankCard.Text = logistics.BankCard.ToString();//绑定  营业执
        txtTax.Text = logistics.Tax.ToString();//绑定  税号
        txtLicenceCode.Text = logistics.LicenceCode.ToString();//绑定  营业执
        txtRemark.Text = logistics.Remark.ToString();//绑定  备注
        txtDate.Text = logistics.RigisterDate.ToShortDateString();
        this.CountryCity1.SelectCountry(logistics.Country, logistics.Province, logistics.City,logistics.Xian);
   
    }

    //绑定银行
    private void BindBank_List()
    {
        IList<MemberBankModel> list = thirdLogisticsDLL.BindBank_List();
        this.ddlBank.DataSource = list;
        this.ddlBank.DataTextField = "BankName";
        this.ddlBank.DataValueField = "BankID";
        this.ddlBank.DataBind();
    }
    //返回浏览
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ThirdLogistics.aspx");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ViewState["mode"].ToString() == "update")
        {
            //根据传的参来决定调用update方法
            int id = Convert.ToInt32(Request.QueryString["id"]);
   
            if (!this.CountryCity1.CheckFill())
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("002126", "请选择正确国家省份城市信息") + "')</script>");

                return;
            }
            if (txtName.Text.Trim() == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("002127", "公司名称不能为空!") + "');</script>");
                return;
            }
            if (thirdLogisticsDLL.UpdateThirdLogistics(Set(), id))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000222", "修改成功!") + "');location.href='ThirdLogistics.aspx';</script>");
                this.truncate();
            }
        }
        else
        {
            if (!this.CountryCity1.CheckFill())
            {
                ScriptHelper.SetAlert(Page, GetTran("002126", "请选择正确国家省份城市信息"));
                return;
            }
            if (txtnumber.Text.Trim() == "")
            {
                //lblMsg.Visible = true;
                //lblMsg.Text = "  编号不能为空!";
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("001547", "编号不能为空!") + "');</script>");
                return;
            }
            if (txtName.Text.Trim() == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("002127", "公司名称不能为空!") + "');</script>");
                return;
            }
            if (thirdLogisticsDLL.CheckLogisticsNumIsUse(txtnumber.Text) > 0)
            {
                //lblMsg.Visible = true;
                //lblMsg.Text = " 编号已存在!";
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("002130", "编号已存在!") + "');</script>");
                return;
            }
            //根据传的参来决定调用Add方法
            if (AddThirdLogistics())
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000891", "添加成功!") + "');location.href='ThirdLogistics.aspx';</script>");
                this.truncate();
            }
        }

    }
    //用于清空文本框的内容
    private void truncate()
    {
        txtRemark.Text = string.Empty;
        txtTax.Text = string.Empty;
        txtnumber.Text = string.Empty;
        txtTelephone1.Text = string.Empty;
        txtTelephone2.Text = string.Empty;
        txtTelephone3.Text = string.Empty;
        txtTelephone4.Text = string.Empty;
        txtStoreAddress.Text = string.Empty;
        txtPrincipal.Text = string.Empty;
        txtName.Text = string.Empty;
        txtPostalCode.Text = string.Empty;
        txtBankCard.Text = string.Empty;
        txtLicenceCode.Text = string.Empty;
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        //truncate();

        Response.Redirect("ThirdLogistics.aspx");
    }
}
