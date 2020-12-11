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
using Model.Other;

public partial class Company_SetParams_ConfigAlipay : BLL.TranslationBase
{
    protected string msg;

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, "../index.aspx");
        Permissions.CheckManagePermission(EnumCompanyPermission.SystemBscoManage);
        if (!IsPostBack)
        {
            DefaultBind();
        }
        Translations();
    }

    private void Translations()
    {
        this.TranControls(this.btnSubmit, new string[][] { new string[] { "000321", "提交" } });
    }

    private void DefaultBind()
    {
        DataTable dtAlipay = DBHelper.ExecuteDataTable("select top 1 AlipayEmail,AlipayID,AlipayKey,ReturnUrl,NotifyUrl from AlipaySet");

        if (dtAlipay.Rows.Count == 0)
        {
            return;
        }
        else
        {
            txtCard.Text = BLL.QuickPay.SecretSecurity.Decrypt3Des24Bit(dtAlipay.Rows[0]["AlipayEmail"].ToString(), new BLL.QuickPay.SecretSecurity().AlipayKey);
            txtId.Text = BLL.QuickPay.SecretSecurity.Decrypt3Des24Bit(dtAlipay.Rows[0]["AlipayID"].ToString(), new BLL.QuickPay.SecretSecurity().AlipayKey);
            txtKey.Text = BLL.QuickPay.SecretSecurity.Decrypt3Des24Bit(dtAlipay.Rows[0]["AlipayKey"].ToString(), new BLL.QuickPay.SecretSecurity().AlipayKey);
            txtReturnUrl.Text =  BLL.QuickPay.SecretSecurity.Decrypt3Des24Bit(dtAlipay.Rows[0]["ReturnUrl"].ToString(), new BLL.QuickPay.SecretSecurity().AlipayKey) ;
            txtPostUrl.Text = BLL.QuickPay.SecretSecurity.Decrypt3Des24Bit(dtAlipay.Rows[0]["NotifyUrl"].ToString(), new BLL.QuickPay.SecretSecurity().AlipayKey);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtCard.Text == "" || txtId.Text == "" || txtKey.Text == "" || txtReturnUrl.Text == "" || txtPostUrl.Text == "")
        {
            BLL.CommonClass.Transforms.JSAlert(GetTran("007055", "各项内容均不可为空！"));
        }

        try
        {
            SqlParameter[] paras = new SqlParameter[5];
            paras[0] = new SqlParameter("@AlipayEmail", BLL.QuickPay.SecretSecurity.Encrypt3Des24Bit(txtCard.Text, new BLL.QuickPay.SecretSecurity().AlipayKey));
            paras[1] = new SqlParameter("@AlipayID", BLL.QuickPay.SecretSecurity.Encrypt3Des24Bit(txtId.Text, new BLL.QuickPay.SecretSecurity().AlipayKey));
            paras[2] = new SqlParameter("@AlipayKey", BLL.QuickPay.SecretSecurity.Encrypt3Des24Bit(txtKey.Text, new BLL.QuickPay.SecretSecurity().AlipayKey));
            paras[3] = new SqlParameter("@ReturnUrl", BLL.QuickPay.SecretSecurity.Encrypt3Des24Bit(txtReturnUrl.Text, new BLL.QuickPay.SecretSecurity().AlipayKey));
            paras[4] = new SqlParameter("@NotifyUrl", BLL.QuickPay.SecretSecurity.Encrypt3Des24Bit(txtPostUrl.Text, new BLL.QuickPay.SecretSecurity().AlipayKey));

            if (DBHelper.ExecuteScalar("select count(0) from AlipaySet").ToString() == "0")
            {
                DBHelper.ExecuteNonQuery("insert into AlipaySet values(@AlipayEmail,@AlipayID,@AlipayKey,@ReturnUrl,@NotifyUrl) ", paras, CommandType.Text);
            }
            else
            {
                DBHelper.ExecuteNonQuery("update AlipaySet set AlipayEmail=@AlipayEmail,AlipayID=@AlipayID,AlipayKey=@AlipayKey,ReturnUrl=@ReturnUrl,NotifyUrl=@NotifyUrl ", paras, CommandType.Text);
            }

            msg = "<script>alert('" + GetTran("005820", "设置成功！") + "');</script>";
        }
        catch (Exception ex)
        {
            msg = "<script>alert('" + GetTran("005821", "设置失败") + "：" + ex.Message.ToString() + "');</script>";
        }
    }
}
