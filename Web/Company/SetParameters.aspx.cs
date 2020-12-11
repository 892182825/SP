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

using Model.Other;

public partial class Company_SetParameters : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        //检查相应权限
        Response.Cache.SetExpires(DateTime.Now);        
        Permissions.CheckManagePermission(EnumCompanyPermission.SystemBscoManage);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));

        if (!Page.IsPostBack)
        {
            table1.Visible = false;

            if (Request.QueryString["type"] != null)
            {
                table1.Visible = false;
                tableP.Visible = true;
            }
            else
            {
                table1.Visible = true;
                tableP.Visible = false;
            }
            if (Permissions.GetPermissions(EnumCompanyPermission.SystemBscoManage) != 6201)           
            {
                hlDocType.Enabled = false;
                hlMemBaseLine.Enabled = false;
                hlMemberBank.Enabled = false;
                hlProductColor.Enabled = false;
                hlProductSexType.Enabled = false;
                hlProductSize.Enabled = false;
                hlProductSpec.Enabled = false;
                hlProductStatus.Enabled = false;
                hlProductUnit.Enabled = false;
                hlProvinceCity.Enabled = false;
                hlWareHouseDepotSeat.Enabled = false;
                levelIcon.Enabled = false;
                //hlCertificateManage.Enabled = false;
                //hlGetWaySet.Enabled = false;
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('对不起，你没有权限访问');</script>");
                return;
            }

            else
            {
                hlDocType.Enabled = true;
                hlMemBaseLine.Enabled = true;
                hlMemberBank.Enabled = true;
                hlProductColor.Enabled = true;
                hlProductSexType.Enabled = true;
                hlProductSize.Enabled = true;
                hlProductSpec.Enabled = true;
                hlProductStatus.Enabled = true;
                hlProductUnit.Enabled = true;
                hlProvinceCity.Enabled = true;
                hlWareHouseDepotSeat.Enabled = true;
                levelIcon.Enabled = true;
                //hlCertificateManage.Enabled = true;
                //hlGetWaySet.Enabled = true;
            }  
        }
        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.hlDocType, new string[][] { new string[] { "001151", "单据类型" } });
        this.TranControls(this.hlProductColor, new string[][] { new string[] { "001950", "产品颜色" } });
        this.TranControls(this.hlProductType, new string[][] { new string[] { "000882", "产品型号" } });
        this.TranControls(this.hlProductSexType, new string[][] { new string[] { "001951", "产品适用人群" } });
        this.TranControls(this.hlProductSize, new string[][] { new string[] { "002174", "产品尺寸" } });
        this.TranControls(this.hlProductSpec, new string[][] { new string[] { "000880", "产品规格" } });
        this.TranControls(this.hlProductStatus, new string[][] { new string[] { "001952", "产品状态" } });
        this.TranControls(this.hlProductUnit, new string[][] { new string[] { "001868", "产品单位" } });
        this.TranControls(this.hlMemBaseLine, new string[][] { new string[] { "002173", "会员报单底线" } });
        this.TranControls(this.hlProvinceCity, new string[][] { new string[] { "002172", "省份城市" } });
        this.TranControls(this.hlMemberBank, new string[][] { new string[] { "002170", "会员可用银行" } });
        this.TranControls(this.hlWareHouseDepotSeat, new string[][] { new string[] { "002169", "仓库库位设置" } });
        this.TranControls(this.levelIcon, new string[][] { new string[] { "002168", "级别图标设置" } });
        //this.TranControls(this.hlCertificateManage, new string[][] { new string[] { "006699", "快钱证书管理" } });
        //this.TranControls(this.hlGetWaySet, new string[][] { new string[] { "006679", "快钱网关设置" } });
    }
}
