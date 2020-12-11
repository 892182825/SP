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
using BLL;
using Model.Other;

public partial class Company_configSMS : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(6314);

        if (!IsPostBack)
        {
            DefaultBind();
        }
        Translations();
    }

    private void Translations()
    {
        this.TranControls(this.btn_submit, new string[][] { new string[] { "005793", "设 置" } });
    }

    private void DefaultBind()
    {
        string url = string.Empty;
        string userName = string.Empty;
        string pwd = string.Empty;
        url = BLL.FileOperateBLL.ForXML.ReadSMSConfig("SmsUrl").ToString();
        userName = BLL.FileOperateBLL.ForXML.ReadSMSConfig("UserName").ToString();
        pwd = BLL.FileOperateBLL.ForXML.ReadSMSConfig("Pwd").ToString();
        this.txtPassPort.Text = userName;
        this.txtPwd.Text = pwd;
        this.txtUrl.Text = url;
        string ExSql = "select top 1 SMSUnitPrice from  GlobalConfig ";
        object ObjPrice = DAL.DBHelper.ExecuteScalar(ExSql);
        if (ObjPrice != DBNull.Value && ObjPrice != null)
        {
            this.txtSMSUnitPrice.Text = Convert.ToDouble(ObjPrice).ToString("0.00");
        }
        if (url != "")
        {
            lblSetFlag.Text = GetTran("006943", "己设置");
        }
        else
        {
            lblSetFlag.Text = GetTran("006944", "未设置");
        }
    }
    protected void btn_submit_Click1(object sender, EventArgs e)
    {
        try
        {
            string msg_url = this.txtUrl.Text.Trim();
            string msg_passPort = this.txtPassPort.Text.Trim();
            string msg_passWord = this.txtPwd.Text.Trim();
            string priceStr = BLL.CommonClass.ValidData.InputText(this.txtSMSUnitPrice.Text);
            msg_url = BLL.CommonClass.ValidData.InputText(msg_url);
            msg_passWord = BLL.CommonClass.ValidData.InputText(msg_passWord);
            double dPrice = 0.1;

            if (msg_url == "")
            {
                BLL.CommonClass.Transforms.JSAlert("网关不能为空！");
                return;
            }
            if (msg_passPort == "")
            {
                BLL.CommonClass.Transforms.JSAlert("网关帐号不能为空！");
                return;
            }
            if (msg_passWord == "")
            {
                BLL.CommonClass.Transforms.JSAlert("网关密码不能为空！");
                return;
            }
            try
            {
                dPrice = double.Parse(priceStr);
            }
            catch
            {
                BLL.CommonClass.Transforms.JSAlert("短信单价格式错误！");
                return;
            }

            bool flag = new BLL.FileOperateBLL.ForXML().SaveSMSConfig(msg_url, msg_passPort, msg_passWord, 1);
            bool flag2 = new BLL.MobileSMS().SetSMSUnitPrice(dPrice);
            if (flag && flag2)
            {//设置成功                 
                BLL.CommonClass.Transforms.JSAlert("设置成功！");
            }
            else
            {//设置失败 
                BLL.CommonClass.Transforms.JSAlert("设置失败！");
            }
        }
        catch (Exception ex)
        {
            BLL.CommonClass.Transforms.JSAlert("设置出错：" + ex.Message);
        }
    }
}