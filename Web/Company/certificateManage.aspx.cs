using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Security;

public partial class source_certificateManage:BLL.TranslationBase 
{
    //ddlPwdType btn_reset btn_reset
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Translations();
    }

    private void Translations()
    {
        this.TranControls(this.revPwd, new string[][] { new string[] { "006919", "格式错误" } });
        this.TranControls(this.revPwd0, new string[][] { new string[] { "006919", "格式错误" } });
        this.TranControls(this.btn_submit, new string[][] { new string[] { "000321", "提交" } });
        this.TranControls(this.btn_reset, new string[][] { new string[] { "006812", "重置" } });
        this.TranControls(this.ddlPwdType, new string[][] {
            new string[] { "006836", "合成证书密码" },
            new string[] { "006837", "证书私钥密码" },
        });


    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {

        try
        {
            string cipherPwd = "";
            string ExSql = "";
            string key = new BLL.QuickPay.SecretSecurity().EpayKey;
            string pwdColumn = ddlPwdType.SelectedItem.Value.Trim();

            string CertificatePwd = this.txtCertificatePwd.Text.Trim();
            string PwdConfirm = this.txtPwdConfirm.Text.Trim();
            CertificatePwd = BLL.CommonClass.ValidData.InputText(CertificatePwd);
            PwdConfirm = BLL.CommonClass.ValidData.InputText(PwdConfirm);
            
            if (CertificatePwd == "" || PwdConfirm == "")
            {
                BLL.CommonClass.Transforms.JSAlert(GetTran("006686", "请输入证书密码！"));
                return;
            }
            if (CertificatePwd.Length > 32)
            {
                BLL.CommonClass.Transforms.JSAlert(GetTran("006934", "注：密码为4~32位字符。"));
                return;
            }

            if (CertificatePwd != PwdConfirm)
            {
                BLL.CommonClass.Transforms.JSAlert(GetTran("001365", "两次密码不一样"));
                return;               
            }

            cipherPwd = BLL.QuickPay.SecretSecurity.Encrypt3Des24Bit(CertificatePwd, key);
            
           
           
            string configDesc = this.ddlPwdType.SelectedItem.Text.Trim();
            int var = 0;
            SqlParameter[] spa = null;
            if (cipherPwd != "")
            {
                int exist = int.Parse(DAL.DBHelper.ExecuteScalar("select isnull(count(*),0) as icount from sys_config where configName='" + pwdColumn + "'").ToString());
                if (exist > 0)
                {
                    spa = new SqlParameter[]{
												   new SqlParameter ("@configValue",SqlDbType.VarChar ,100),
						new SqlParameter ("@configName",SqlDbType.VarChar ,50)};
                    spa[0].Value = cipherPwd;
                    spa[1].Value = pwdColumn;
                    ExSql = "update sys_config set configValue=@configValue where configName=@configName";
                }
                else
                {
                    string newCodeStr = DAL.DBHelper.ExecuteScalar("select isnull(max(configCode),0) as configCode  from sys_config").ToString();
                    int inewCode = int.Parse(newCodeStr) + 1;
                    string newCode = inewCode.ToString().PadLeft(5, '0');
                    spa = new SqlParameter[]{
												   new SqlParameter ("@configValue",SqlDbType.VarChar ,100),
												   new SqlParameter ("@configName",SqlDbType.VarChar ,50),
												   new SqlParameter ("@configCode",SqlDbType.VarChar ,5),
												   new SqlParameter ("@configDesc",SqlDbType.VarChar ,200)
						};
                    spa[0].Value = cipherPwd;
                    spa[1].Value = pwdColumn;
                    spa[2].Value = newCode;
                    spa[3].Value = configDesc;

                    ExSql = "insert sys_config(configCode,configName,configValue,configDesc) values(@configCode,@configName,@configValue,@configDesc)";

                }
                var = DAL.DBHelper.ExecuteNonQuery(ExSql, spa, CommandType.Text);
            }
            if (var > 0)
            {
                BLL.CommonClass.Transforms.JSAlert(GetTran("005820", "设置成功！"));
            }
        }
        catch (Exception ex)
        {
            BLL.CommonClass.Transforms.JSAlert(GetTran("005821", "设置失败") + "：" + ex.Message.ToString());
        }
    }

}
