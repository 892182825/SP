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
using BLL.other.Company;
using Model;
using System.Collections.Generic;
using Model.Other;

public partial class Company_EncryptionSetting : BLL.TranslationBase 
{
    protected string msg ;
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        //检查相应权限
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.EncryptionSetting);
        if (!IsPostBack)
        {
            Bind();
        }
        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.btnSave, new string[][] { new string[] { "000434", "确 定" } });
    }

    private void Bind()
    {
        gvSetting.DataSource = EncryptionBLL.GetAllSetting();
        gvSetting.DataBind();
    }

    protected object GetKey(object obj)
    {
        if (obj != null)
        { 
            string key=obj.ToString();
            if (key == "--Name--")
            {
                return GetTran("000107", "姓名") + "：";
            }
            else if (key == "--Tele--")
            {
                return GetTran("005819", "电话或手机") +"：";
            }
            else if (key == "--Address--")
            {
                return GetTran("000072", "地址") + "：";
            }
            else if (key == "--Card--")
            {
                return GetTran("001407", "银行卡号") + "：";
            }
            else if (key == "--Number--")
            {
                return GetTran("000083", "证件号码") + "：";
            }
        }
        return "";
    }

    protected object GetValue(object obj)
    {
        if (obj != null)
        {
            if (obj.ToString() == "0")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        return false;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        IList<EncryptionSetting> ess = new List<EncryptionSetting>();
        for(int i=0;i<gvSetting.Rows.Count;i++)
        {
            EncryptionSetting es = new EncryptionSetting();
            es.EncryptionKey = (gvSetting.Rows[i].FindControl("hidKey") as HtmlInputHidden).Value;
            es.EncryptionValue = (gvSetting.Rows[i].FindControl("ckbValue") as CheckBox).Checked ? 1 : 0;
            ess.Add(es);
        }

        Application["jinzhi"] = "HDG";
        if (EncryptionBLL.EditSetting(ess))
        {
            msg = "<script>alert('" + GetTran("005820", "设置成功！") + "');</script>";
        }
        else
        {
            msg = "<script>alert('" + GetTran("005821", "设置失败！！") + "');</script>";
        }
        Application["jinzhi"] = "";

        Bind();
    }
}
