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

///Add Namespace
using BLL.other.Company;
using BLL.CommonClass;

/*
 * 创建者：     汪  华
 * 创建时间：   2009-09-23
 * 对应菜单:    系统管理->参数设置
 */

public partial class Company_SetJjTx : BLL.TranslationBase
{
    public string msg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        if (!IsPostBack)
        {
            ///获取会员订单底线金额
            this.rbtS.SelectedValue = BLL.CommonClass.CommonDataBLL.getGrantState();
        }
        Translations();
    }
    private void Translations()
    {
        //this.TranControls(this.lbtnReturn, new string[][] { new string[] { "000096", "返 回" } });
        this.TranControls(this.btnOK, new string[][] { new string[] { "002190", "确 定" } });
        TranControls(rbtS, new string[][] { new string[] { "000233", "是" }, new string[] { "000235", "否" } });
    }

    /// <summary>
    /// 确定事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        bool isOk = BLL.CommonClass.CommonDataBLL.SetGrantState(this.rbtS.SelectedValue);
        if (isOk)
        {
            msg = "<script>alert('" + GetTran("005820", "设置成功！") + "');</script>";
        }
        else
        {
            msg = "<script>alert('" + GetTran("006922", "设置失败！") + "');</script>";
        }
    }

    /// <summary>
    /// 返回上级菜单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("../SetParameters.aspx");
    }
}
