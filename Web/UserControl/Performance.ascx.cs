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
using BLL.other.Member;
using BLL.Registration_declarations;
using BLL.CommonClass;
using System.Data;

public partial class UserControl_Performance : System.Web.UI.UserControl
{
    protected BLL.TranslationBase tran = new BLL.TranslationBase();
    private int expectNum = 0, type = 0;
    private string number = "";
    int bzCurrency = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //获取标准币种
            bzCurrency = CommonDataBLL.GetStandard();
            if (Session["Default_Currency"] == null) Session["Default_Currency"] = bzCurrency;

            readInfo();
        }
    }
    //期数
    public int ExpectNum
    {
        get { return expectNum; }
        set { expectNum = value; }
    }

    //会员编号
    public string Number
    {
        get { return number; }
        set { number = value; }
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
        MemberInfoModel memberInfoModel = MemberInfoModifyBll.getMemberInfo(Number);
        #region 读取个人信息
        if (memberInfoModel != null)
        {
            this.storeid.Text = memberInfoModel.StoreID;
        }

        #endregion

        DataTable dt = DetialQueryBLL.GetMemberInfoBalanceByNumber(ExpectNum, Number);
        if (dt.Rows.Count > 0)
        {
            CurrentTotalNetRecord.Text = (Convert.ToDouble(dt.Rows[0]["CurrentTotalNetRecord"]) / AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()))).ToString("0.00");
            CurrentOneMark.Text = (Convert.ToDouble(dt.Rows[0]["CurrentOneMark"]) / AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()))).ToString("0.00");
            level.Text = dt.Rows[0]["levelstr"].ToString();
            if (dt.Rows[0]["TotalNetNum"] == null || dt.Rows[0]["TotalNetNum"].ToString() == "")
            {
                labTotalNetNum.Text = "0"; ;
            }
            else
            {
                labTotalNetNum.Text = (Convert.ToDouble(dt.Rows[0]["TotalNetNum"]) / AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()))).ToString("0.00");
            }
            labDTotalNetNum.Text = (Convert.ToDouble(dt.Rows[0]["DTotalNetNum"]) / AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()))).ToString("0.00");

            //个人总资格业绩
            if (dt.Rows[0]["TotalOneMark"] == null || dt.Rows[0]["TotalOneMark"].ToString() == "")
            {
                labCurrentOneMark1.Text = "0.00";
            }
            else
            {
                labCurrentOneMark1.Text = (Convert.ToDouble(dt.Rows[0]["TotalOneMark"].ToString()) / AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()))).ToString("0.00");
            }

            labCurrentTotalNetRecord.Text = (Convert.ToDouble(dt.Rows[0]["CurrentTotalNetRecord"].ToString()) / AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()))).ToString("0.00");

            if (dt.Rows[0]["TotalNetRecord"] == null || dt.Rows[0]["TotalNetRecord"].ToString() == "")
            {
                labTotalNetRecord1.Text = "0.00";
            }
            else
            {
                labTotalNetRecord1.Text = (Convert.ToDouble(dt.Rows[0]["TotalNetRecord"].ToString()) / AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()))).ToString("0.00");
            }

            labdCurrentTotalNetRecord2.Text = Convert.ToDouble(dt.Rows[0]["dCurrentTotalNetRecord"].ToString()).ToString("0.00");
            labdTotalNetRecord2.Text = (Convert.ToDouble(dt.Rows[0]["dTotalNetRecord"].ToString()) / AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()))).ToString("0.00");

            CurrentNewNetNum.Text = (Convert.ToDouble(dt.Rows[0]["DCurrentNewNetNum"].ToString()) / AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()))).ToString("0.00");
        }
        else
        {
            CurrentTotalNetRecord.Text = "0.00";
            CurrentOneMark.Text = "0.00";
            level.Text = " ";
            CurrentNewNetNum.Text = "0";

        }
    }

}
