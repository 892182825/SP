using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using Model;
using BLL.CommonClass;

public partial class MemberMobile_MemberCashList : BLL.TranslationBase
{
    public int bzCurrency = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        //获取标准币种
        bzCurrency = CommonDataBLL.GetStandard();
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        if (!IsPostBack)
        {
            
        
        }
    }

    protected void btn_SeachList_Click(object sender, EventArgs e)
    {


    }
}