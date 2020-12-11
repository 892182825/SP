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

public partial class Company_SetParams_ConfigQuickPay : BLL.TranslationBase 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, "../index.aspx");
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

        string key = new BLL.QuickPay.SecretSecurity().EpayKey;
        string ExSql = "select configValue  from sys_config where configName='quickPay_AccountID'";
        string AccountID = string.Empty;
        object objAccid = DAL.DBHelper.ExecuteScalar(ExSql);
        if (objAccid != DBNull.Value && objAccid != null)
        {
            AccountID = objAccid.ToString();
            //if (AccountID != "")
            //AccountID = BLL.QuickPay.SecretSecurity.Decrypt3Des24Bit(AccountID, key);
        }


        ExSql = "select configValue  from sys_config where configName='quickPay_Key'";
        string AccountKey = string.Empty;
        object objAccKey = DAL.DBHelper.ExecuteScalar(ExSql);
        if (objAccKey != DBNull.Value && objAccKey != null)
        {
            AccountKey = objAccKey.ToString();
            //if (AccountKey != "")
            //AccountKey = BLL.QuickPay.SecretSecurity.Decrypt3Des24Bit(AccountKey, key);       
        }

        ExSql = "select configValue  from sys_config where configName='quickPay_PageUrl'";
        string PageUrl = string.Empty;
        object objPageUrl = DAL.DBHelper.ExecuteScalar(ExSql);
        if (objPageUrl != DBNull.Value && objPageUrl != null)
        {
            PageUrl = objPageUrl.ToString();
            //if (PageUrl!="")
            // PageUrl = BLL.QuickPay.SecretSecurity.Decrypt3Des24Bit(PageUrl, key);
        }

        ExSql = "select configValue  from sys_config where configName='quickPay_BgUrl'";
        string BgUrl = string.Empty;
        object objBgUrl = DAL.DBHelper.ExecuteScalar(ExSql);
        if (objBgUrl != DBNull.Value && objBgUrl != null)
        {
            BgUrl = objBgUrl.ToString();
            //if(BgUrl!="")
            // BgUrl = BLL.QuickPay.SecretSecurity.Decrypt3Des24Bit(BgUrl, key);
        }

        if (AccountID != "")
        {
            lblIDFlag.Text = GetTran("006943", "己设置");
       
            txtAcctId.Text = BLL.QuickPay.SecretSecurity.Decrypt3Des24Bit(AccountID, key);
        }
        else
        {
            lblIDFlag.Text = GetTran("006944", "未设置");
        }
        if (AccountKey != "")
        {
            this.lblKeyFlag.Text = GetTran("006943", "己设置");
            txtKey.Text = BLL.QuickPay.SecretSecurity.Decrypt3Des24Bit(AccountKey, key);
        }
        else
        {
            this.lblKeyFlag.Text = GetTran("006944", "未设置");
        }
        if (PageUrl != "")
        {
            lblPageUrl.Text = GetTran("006943", "己设置");
            txtPageUrl.Text = BLL.QuickPay.SecretSecurity.Decrypt3Des24Bit(PageUrl, key).ToLower();
        }
        else
        {
            lblPageUrl.Text = GetTran("006944", "未设置");
        }
        if (BgUrl != "")
        {
            this.lblBgUrl.Text = GetTran("006943", "己设置");
            txtBgUrl.Text =BLL.QuickPay.SecretSecurity.Decrypt3Des24Bit(BgUrl, key).ToLower();
        }
        else
        {
            this.lblBgUrl.Text = GetTran("006944", "未设置");
        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int var = 0;
        SqlTransaction tran = null;
        SqlConnection con = DAL.DBHelper.SqlCon();
        try
        {
            string AccID = this.txtAcctId.Text.Trim();
            string accKey = this.txtKey.Text.Trim();
            string pageUrl = this.txtPageUrl.Text.Trim();
            string bgUrl = this.txtBgUrl.Text.Trim();
            AccID = BLL.CommonClass.ValidData.InputText(AccID);
            accKey = BLL.CommonClass.ValidData.InputText(accKey);
            pageUrl = BLL.CommonClass.ValidData.InputText(pageUrl);
            bgUrl = BLL.CommonClass.ValidData.InputText(bgUrl);

            string key = new BLL.QuickPay.SecretSecurity().EpayKey;

            if (AccID == "" || accKey == "" || pageUrl == "" || bgUrl == "")
            {
                BLL.CommonClass.Transforms.JSAlert(GetTran("007055", "各项内容均不可为空！"));
            }

            if (AccID == "")
            {
                BLL.CommonClass.Transforms.JSAlert(GetTran("006682", "请输入人民币网关账户！"));
                return;
            }
            else
            {
                AccID = BLL.QuickPay.SecretSecurity.Encrypt3Des24Bit(AccID, key);
            }

            if (accKey == "")
            {
                BLL.CommonClass.Transforms.JSAlert(GetTran("006683", "请输入人民币网关密钥！"));
                return;
            }
            else
            {
                accKey = BLL.QuickPay.SecretSecurity.Encrypt3Des24Bit(accKey, key);
            }
            if (pageUrl == "")
            {
                BLL.CommonClass.Transforms.JSAlert(GetTran("006682", "请输入重定下地址！"));
                return;
            }
            else
            {
                pageUrl = BLL.QuickPay.SecretSecurity.Encrypt3Des24Bit( pageUrl , key);
            }
            if (bgUrl == "")
            {
                BLL.CommonClass.Transforms.JSAlert(GetTran("006682", "请输入服务器通知地址！"));
                return;
            }
            else
            {
                bgUrl = BLL.QuickPay.SecretSecurity.Encrypt3Des24Bit( bgUrl , key);
            }

            string ExSql = "";

            string accountColumn = "quickPay_AccountID";
            string accountKey = "quickPay_Key";
            string accountPageUrl = "quickPay_PageUrl";
            string accountBgUrl = "quickPay_BgUrl";
            string AccIDDesc = "人民币网关账户";
            string accKeyDesc = "人民币网关密钥";
            string pageUrlDesc = "重定向地址";
            string bgUrlDesc = "服务器通知地址";

            string newCodeStr = string.Empty;
            string newCode = string.Empty;
            int inewCode = 0;

            con.Open();
            tran = con.BeginTransaction();
            SqlParameter[] spa = null;

            if (pageUrl != "")
            {
                ExSql = "select isnull(count(*),0) as icount from sys_config where configName='" + accountPageUrl + "'";
                int exist = int.Parse(DAL.DBHelper.ExecuteScalar(tran, ExSql, CommandType.Text).ToString());
                if (exist > 0)
                {
                    spa = new SqlParameter[]{
												   new SqlParameter ("@configValue",SqlDbType.VarChar ,100),
						new SqlParameter ("@configName",SqlDbType.VarChar ,50)};
                    spa[0].Value = pageUrl;
                    spa[1].Value = accountPageUrl;
                    ExSql = "update sys_config set configValue=@configValue where configName=@configName";
                }
                else
                {
                    ExSql = "select isnull(max(configCode),0) as configCode  from sys_config";
                    newCodeStr = DAL.DBHelper.ExecuteScalar(tran, ExSql, CommandType.Text).ToString();
                    inewCode = int.Parse(newCodeStr) + 1;
                    newCode = inewCode.ToString().PadLeft(5, '0');
                    spa = new SqlParameter[]{
												   new SqlParameter ("@configValue",SqlDbType.NVarChar ,100),
												   new SqlParameter ("@configName",SqlDbType.NVarChar ,50),
												   new SqlParameter ("@configCode",SqlDbType.NVarChar ,5),
												   new SqlParameter ("@configDesc",SqlDbType.NVarChar ,200)
						};
                    spa[0].Value = pageUrl;
                    spa[1].Value = accountPageUrl;
                    spa[2].Value = newCode;
                    spa[3].Value = pageUrlDesc;
                    ExSql = "insert sys_config(configCode,configName,configValue,configDesc) values(@configCode,@configName,@configValue,@configDesc)";

                }
                var = DAL.DBHelper.ExecuteNonQuery(tran, ExSql, spa, CommandType.Text);
            }
            if (bgUrl != "")
            {
                ExSql = "select isnull(count(*),0) as icount from sys_config where configName='" + accountBgUrl + "'";
                int exist = int.Parse(DAL.DBHelper.ExecuteScalar(tran, ExSql, CommandType.Text).ToString());
                if (exist > 0)
                {
                    spa = new SqlParameter[]{
												   new SqlParameter ("@configValue",SqlDbType.VarChar ,100),
						new SqlParameter ("@configName",SqlDbType.VarChar ,50)};
                    spa[0].Value = bgUrl;
                    spa[1].Value = accountBgUrl;
                    ExSql = "update sys_config set configValue=@configValue where configName=@configName";
                }
                else
                {
                    ExSql = "select isnull(max(configCode),0) as configCode  from sys_config";
                    newCodeStr = DAL.DBHelper.ExecuteScalar(tran, ExSql, CommandType.Text).ToString();
                    inewCode = int.Parse(newCodeStr) + 1;
                    newCode = inewCode.ToString().PadLeft(5, '0');
                    spa = new SqlParameter[]{
												   new SqlParameter ("@configValue",SqlDbType.NVarChar ,100),
												   new SqlParameter ("@configName",SqlDbType.NVarChar ,50),
												   new SqlParameter ("@configCode",SqlDbType.NVarChar ,5),
												   new SqlParameter ("@configDesc",SqlDbType.NVarChar ,200)
						};
                    spa[0].Value = bgUrl;
                    spa[1].Value = accountBgUrl;
                    spa[2].Value = newCode;
                    spa[3].Value = bgUrlDesc;
                    ExSql = "insert sys_config(configCode,configName,configValue,configDesc) values(@configCode,@configName,@configValue,@configDesc)";

                }
                var = DAL.DBHelper.ExecuteNonQuery(tran, ExSql, spa, CommandType.Text);
            }
            if (AccID != "")
            {
                ExSql = "select isnull(count(*),0) as icount from sys_config where configName='" + accountColumn + "'";
                int exist = int.Parse(DAL.DBHelper.ExecuteScalar(tran, ExSql, CommandType.Text).ToString());
                if (exist > 0)
                {
                    spa = new SqlParameter[]{
												   new SqlParameter ("@configValue",SqlDbType.VarChar ,100),
						new SqlParameter ("@configName",SqlDbType.VarChar ,50)};
                    spa[0].Value = AccID;
                    spa[1].Value = accountColumn;
                    ExSql = "update sys_config set configValue=@configValue where configName=@configName";
                }
                else
                {
                    ExSql = "select isnull(max(configCode),0) as configCode  from sys_config";
                    newCodeStr = DAL.DBHelper.ExecuteScalar(tran, ExSql, CommandType.Text).ToString();
                    inewCode = int.Parse(newCodeStr) + 1;
                    newCode = inewCode.ToString().PadLeft(5, '0');
                    spa = new SqlParameter[]{
												   new SqlParameter ("@configValue",SqlDbType.NVarChar ,100),
												   new SqlParameter ("@configName",SqlDbType.NVarChar ,50),
												   new SqlParameter ("@configCode",SqlDbType.NVarChar ,5),
												   new SqlParameter ("@configDesc",SqlDbType.NVarChar ,200)
						};
                    spa[0].Value = AccID;
                    spa[1].Value = accountColumn;
                    spa[2].Value = newCode;
                    spa[3].Value = AccIDDesc;
                    ExSql = "insert sys_config(configCode,configName,configValue,configDesc) values(@configCode,@configName,@configValue,@configDesc)";

                }
                var = DAL.DBHelper.ExecuteNonQuery(tran, ExSql, spa, CommandType.Text);
            }
            if (accKey != "" && var > 0)
            {
                ExSql = "select isnull(count(*),0) as icount from sys_config where configName='" + accountKey + "'";
                int exist = int.Parse(DAL.DBHelper.ExecuteScalar(tran, ExSql, CommandType.Text).ToString());
                if (exist > 0)
                {
                    spa = new SqlParameter[]{
												   new SqlParameter ("@configValue",SqlDbType.VarChar ,100),
						new SqlParameter ("@configName",SqlDbType.VarChar ,50)};
                    spa[0].Value = accKey;
                    spa[1].Value = accountKey;
                    ExSql = "update sys_config set configValue=@configValue where configName=@configName";
                }
                else
                {
                    ExSql = "select isnull(max(configCode),0) as configCode  from sys_config";
                    newCodeStr = DAL.DBHelper.ExecuteScalar(tran, ExSql, CommandType.Text).ToString();
                    inewCode = int.Parse(newCodeStr) + 1;
                    newCode = inewCode.ToString().PadLeft(5, '0');
                    spa = new SqlParameter[]{
												   new SqlParameter ("@configValue",SqlDbType.NVarChar ,100),
												   new SqlParameter ("@configName",SqlDbType.NVarChar ,50),
												   new SqlParameter ("@configCode",SqlDbType.NVarChar ,5),
												   new SqlParameter ("@configDesc",SqlDbType.NVarChar ,200)
						};
                    spa[0].Value = accKey;
                    spa[1].Value = accountKey;
                    spa[2].Value = newCode;
                    spa[3].Value = accKeyDesc;
                    ExSql = "insert sys_config(configCode,configName,configValue,configDesc) values(@configCode,@configName,@configValue,@configDesc)";

                }
                var = DAL.DBHelper.ExecuteNonQuery(tran, ExSql, spa, CommandType.Text);
            }
            if (var > 0)
            {
                BLL.CommonClass.Transforms.JSAlert(GetTran("005820", "设置成功！"));
                tran.Commit();
            }
            else
            {
                tran.Rollback();
            }
        }
        catch (Exception ex)
        {
            BLL.CommonClass.Transforms.JSAlert(GetTran("005821", "设置失败") + "：" + ex.Message.ToString());
            tran.Rollback();
        }
        if (var > 0)
        {
            DefaultBind();
        }
    }
}
