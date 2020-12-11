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

using System.IO;
using BLL.CommonClass;
using BLL.other.Company;
using Model.Other;
using Model;
using System.Text.RegularExpressions;

public partial class Company_DataEmpty : BLL.TranslationBase 
{
    protected string strPathFileName1;
    protected string strPathFileName;
    protected string strSerPath;

    protected static bool blDataBackupID = true;
    protected static bool blDataBackupTime = true;
    protected static bool blPathFileName = true;
    protected static bool blOperatorNum = true; 

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        if (!this.IsPostBack)
        {
            this.btnSave.Attributes.Add("realvalue", "0");
            this.btnSave.Attributes.Add("onclick", "this.realvalue   ++;if(this.realvalue==1){if (confirm('" + GetTran("001497", "确认要初始化数据吗") + "?')) { if (confirm('" + GetTran("007095", "请先备份数据库！") + "！')){return true;}else{return false;}} else {this.realvalue=0; return false;}}else{alert('" + GetTran("000792", "不可重复提交") + "!');return false}");

            this.Button1.Attributes.Add("onclick", "return ManageIdValid2()");

            this.div1.Style.Add("display", "");
            this.div2.Style.Add("display", "none");
            GetPermission();
            GetBind();
        }

        Translations();
    }

    private void GetBind()
    {
        this.txtManageId.Text = BLL.CommonClass.CommonDataBLL.getManageID(1);
        this.txtStoreId.Text = BLL.CommonClass.CommonDataBLL.getManageID(2);
        this.txtMemberId.Text = BLL.CommonClass.CommonDataBLL.getManageID(3);
    }

    private void GetPermission()
    {
        string LoginManageId = Session["Company"].ToString();
        string DefaultManageId = BLL.CommonClass.CommonDataBLL.getManageID(1);
        if (LoginManageId.ToLower() != DefaultManageId.ToLower())
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000847", "对不起，您没有此权限！") + "');location.href='SetParameters.aspx';</script>");
        }
    }


    private void Translations()
    {


        this.TranControls(this.btnSave, new string[][] { new string[] { "001495", "数据初始化" } });

        this.TranControls(this.btnReturn, new string[][] { new string[] { "000096", "返 回" } });

    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("SetParameters.aspx");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int nowExpectNum = BLL.CommonClass.CommonDataBLL.getMaxqishu();
        if (nowExpectNum != 1)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("007094", "只有在第一期时才能初始化！") + "');</script>");
            return;
        }
        try
        {
            int num = DataEmptyBLL.getDataEmpty();

            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert("" + GetTran("001509", "数据初始化成功") + "！"));
        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert("" + GetTran("001511", "数据初始化失败") + "！"));
            return;
        }
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        if (this.txtPwd.Text.Trim() == "")
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert("" + GetTran("001626", "密码不能为空！") + ""));
            return;
        }

        if (BLL.other.IndexBLL.CheckLogin("Company", BLL.CommonClass.CommonDataBLL.getManageID(1), Encryption.Encryption.GetEncryptionPwd(txtPwd.Text, BLL.CommonClass.CommonDataBLL.getManageID(1))))
        {
            this.div2.Style.Add("display", "");
            this.div1.Style.Add("display", "none");
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert("" + GetTran("007093", "密码输入错误，请重新输入！！") + ""));
            return;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        txtManageId.Text = DisposeString.DisString(txtManageId.Text.Trim());
        Model.DefaultMessage def = GetModel(1,txtManageId.Text.Trim());

        bool isSure = ManagerBLL.UpdateDefaultManage(def);
        if (!isSure)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert("修改失败！"));
            return;
        }

        Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('修改成功，请重新登录！');top.location.href('../logout.aspx')</script>");
        
    }

    private Model.DefaultMessage GetModel(int type,string number)
    {
        Model.DefaultMessage def = new DefaultMessage();
        def.OldId = BLL.CommonClass.CommonDataBLL.getManageID(type);
        def.NewId = number;
        def.OperateIp = BLL.CommonClass.CommonDataBLL.OperateIP;
        def.OperateNum = Session["Company"].ToString();
        def.UpdateTime = DateTime.Now.ToUniversalTime();
        def.ExpectNum = BLL.CommonClass.CommonDataBLL.getMaxqishu();
        def.Type = type;

        return def;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        int nowExpectNum = BLL.CommonClass.CommonDataBLL.getMaxqishu();
        if (nowExpectNum != 1)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("007094", "只有在第一期时才能初始化！") + "');</script>");
            return;
        }

        txtStoreId.Text = DisposeString.DisString(txtStoreId.Text.Trim());
        if(StoreRegisterConfirmBLL.CheckStoreId(DisposeString.DisString(this.txtStoreId.Text.Trim())))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert("该店铺编号已存在！"));
            return;
        }

        Model.DefaultMessage def = GetModel(2,txtStoreId.Text.Trim());

        bool isSure = ManagerBLL.UpdateDefaultStore(def);
        if (!isSure)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert("修改失败！"));
            return;
        }

        Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('修改成功！');</script>");
        
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        int nowExpectNum = BLL.CommonClass.CommonDataBLL.getMaxqishu();
        if (nowExpectNum != 1)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("007094", "只有在第一期时才能初始化！") + "');</script>");
            return;
        }
        txtMemberId.Text = DisposeString.DisString(txtMemberId.Text.Trim());

        int count = BLL.CommonClass.CommonDataBLL.getCountNumber(txtMemberId.Text.Trim());
        if (count > 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert("该会员编号已存在！"));
            return;
        }

        Model.DefaultMessage def = GetModel(3,txtMemberId.Text.Trim());

        bool isSure = ManagerBLL.UpdateDefaultMember(def);
        if (!isSure)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert("修改失败！"));
            return;
        }

        Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('修改成功！');</script>");
        
    }
}
