using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using BLL.other.Member;
using BLL.Registration_declarations;
using BLL.CommonClass;
using System.Data;

public partial class UserControl_MemberPerformance :System.Web.UI.UserControl 
{
    protected BLL.TranslationBase tran = new BLL.TranslationBase();
    private int expectNum = 0, type = 0;
    int bzCurrency = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        //获取标准币种
        bzCurrency = CommonDataBLL.GetStandard();
        if (Session["Default_Currency"] == null) Session["Default_Currency"] = bzCurrency;
        if (Type == 1)
        {
            tr1.Style.Clear();
            tr1.Style.Add("display", "");
        }
        else
        {
            tr1.Style.Clear();
            tr1.Style.Add("display", "none");
        }
        readInfo();
    }
    //期数
    public int ExpectNum {
        get { return expectNum; }
        set { expectNum = value; }
    }
    //类型
    public int Type
    {
        get { return type; }
        set { type = value; }
    }
    //绑定信息
    public void readInfo()
    {
        MemberInfoModel memberInfoModel = MemberInfoModifyBll.getMemberInfo(Session["Member"].ToString());
      

        DataTable dt = DetialQueryBLL.GetMemberInfoBalanceByNumber(ExpectNum, Session["Member"].ToString());
        if (dt.Rows.Count > 0)
        {
            lblNewPeople.Text = dt.Rows[0]["CurrentNewNetNum"].ToString();
            if (AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString())) == 0)
            {
                lblNewYeji.Text = "0.00";
                lblTotalYeji.Text = "0.00";
                lblCurrentOneMark2.Text = "0.00";
                lblTotalOneMark2.Text = "0.00";
            }
            else
            {
                lblNewYeji.Text = (double.Parse(dt.Rows[0]["CurrentTotalNetRecord"].ToString())).ToString("0.00");
                lblTotalYeji.Text = (double.Parse(dt.Rows[0]["TotalNetRecord"].ToString()) ).ToString("0.00");
                lblCurrentOneMark2.Text = (double.Parse(dt.Rows[0]["CurrentOneMark2"].ToString())).ToString("0.00");
                lblTotalOneMark2.Text = (double.Parse(dt.Rows[0]["TotalOneMark2"].ToString())).ToString("0.00");
            }
            lblTotalPeople.Text = dt.Rows[0]["TotalNetNum"].ToString();
        }
    }

}
