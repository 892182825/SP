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
using BLL;
using System.Collections.Generic;
using BLL.Registration_declarations;
using System.Data.SqlClient;
using BLL.CommonClass;
using Model.Other;
using BLL.Logistics;
using BLL.other.Company;
using DAL;

public partial class Company_RegOrder : BLL.TranslationBase
{
    //逻辑层RegistermemberBLL类对象registermemberBLL
    RegistermemberBLL registermemberBLL = new RegistermemberBLL();
    //model层MemberDetailsModel类对象
    MemberDetailsModel memberDetailsModel = new MemberDetailsModel();
    MemberOrderModel memberOrderModel = new MemberOrderModel();
    MemberInfoModel memberInfoModel = new MemberInfoModel();

    /// <summary>
    /// 页面加载事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetExpires(DateTime.Now);
        //ajaxPro注册1111111111
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxMemShopCart));

        DD1.Visible = false;
        DD2.Visible = false;
        if (!IsPostBack)
        {
            sp1.Visible = false;

            CommonDataBLL.BindQishuList(this.ddlQishu, false);

            setProductMenu(GetStoreId());

            AddOrderBLL.BindCurrency_Rate(DropCurrency, GetStoreId());

            new GroupRegisterBLL().GetPaymentType(Ddzf, 1);
        }
        
    }

    /// <summary>
    /// 产生树形菜单
    /// </summary>
    /// <param name="dian"></param>
    public void setProductMenu(string storID)
    {
        ProductTree myTree = new ProductTree();
        this.menuLabel.Text = myTree.getMenuStore(storID,1,0);
    }

    public string GetStoreId()
    {
        return BLL.CommonClass.CommonDataBLL.getManageID(2);
    }

    /// <summary>
    /// 控件翻译
    /// </summary>
    public void ChangLanguageVaile()
    {
        //btnsubmitreg.Text = GetTran("000502", "注册");
       // this.TranControls(this.RegularExpressionValidator1, new string[][] { new string[] { "000467", "抱歉！请输入正确的Email地址" }, });
        //this.TranControls(this.btnsubmitreg, new string[][] { new string[] { "000502", "注 册" }, });
        //this.TranControls(this.RadioBtnSex, new string[][] { new string[] { "000094", "男" }, new string[] { "000095", "女" } });
        this.TranControls(this.ddth, new string[][] { new string[] { "000464", "自提" }, new string[] { "000543", "邮寄" } });

        this.TranControls(this.DDLSendType, new string[][] { new string[] { "007103", "公司发货到店铺" }, new string[] { "007104", "公司直接发给会员" } });
    }








    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("ShoppingCartView.aspx?tt1=tree");
    }
}
