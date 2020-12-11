using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DAL;
using BLL.Registration_declarations;
using Model.Other;
using BLL.other.Company;
using BLL.CommonClass;

public partial class UpdateNet : BLL.TranslationBase
{
    public string msg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ThreeRedirect(Page, "Member/" + Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        //ajaxPro注册
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));

        if (!this.IsPostBack)
        {
            string number = Request.QueryString["Number"].ToString();
            this.labBh.Text = number;
            getNetMessage(number);
        }
        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.btnOk, new string[][] { new string[] { "000434", "确定" } });
        this.TranControls(this.btnBack, new string[][] { new string[] { "000421", "返回" } });
    }

    protected string GetCssType()
    {
        string cssType = Request.QueryString["CssType"].ToString();
        return cssType;
    }

    private void getNetMessage(string number)
    {

        DataTable dtms = ChangeTeamBLL.GetNetMessage(number);
        if (dtms != null && dtms.Rows.Count > 0)
        {
            SearchPlacement_DoubleLines1.Placement = dtms.Rows[0]["placement"].ToString();
            this.txtDirect.Text = dtms.Rows[0]["direct"].ToString();
            ViewState["placement"] = dtms.Rows[0]["placement"].ToString();
            ViewState["direct"] = dtms.Rows[0]["direct"].ToString();
            ViewState["storeid"] = dtms.Rows[0]["storeid"].ToString();
            ViewState["district"] = dtms.Rows[0]["district"].ToString();
            HFTopNumber.Value = dtms.Rows[0]["direct"].ToString();
            
        }
        else
        {
            msg = "<script>alert('" + GetTran("000537", "对不起,该会员不存在") + "');</script>";
        }

    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        string number = this.labBh.Text;
        string placement = SearchPlacement_DoubleLines1.Placement;
        string direct = DisposeString.DisString(this.txtDirect.Text, "'", "").Trim();
        string oldplacement = ViewState["placement"].ToString();
        string olddirect = ViewState["direct"].ToString();
        string storeid = ViewState["storeid"].ToString();

        if (placement == "" || direct == "")
        {
            lblmessage.Text = GetTran("000716", "推荐编号或安置编号都不能为空!");
            return;
        }
        if (ChangeTeamBLL.CheckNum(direct))
        {
            lblmessage.Text = GetTran("000717", "推荐编号不存在!");
            return;
        }
        if (ChangeTeamBLL.CheckNum(placement))
        {
            lblmessage.Text = GetTran("000718", "安置编号不存在!");
            return;
        }
        string topMemberId = BLL.CommonClass.CommonDataBLL.getManageID(3);
        int district = Convert.ToInt32(ViewState["district"]); ;
        if (placement != topMemberId)
        {
            int flag_xiou = ChangeTeamBLL.GetPlacementCount(placement, number);
            if (flag_xiou >= 2)
            {
                ScriptHelper.SetAlert(Page, GetTran("000000", "此安置编号下已经安置了两个人！"));
                return;
            }
            if (DBHelper.ExecuteScalar("select count(0) from memberinfo where placement='" + placement + "' and District=" + direct + "").ToString() != "0")
            {
                district = AddOrderDataDAL.GetDistrict(placement, 1);
                if (district == 1)
                {
                    if (DBHelper.ExecuteScalar("select count(0) from memberinfo where placement='" + placement + "' and District=2").ToString() != "0")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + this.GetTran("000000", "安置人所选区位已有人安置！") + "');</script>", false);
                        return;
                    }
                }
                else if (district == 2)
                {
                    if (DBHelper.ExecuteScalar("select count(0) from memberinfo where placement='" + placement + "' and District=1").ToString() != "0")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + this.GetTran("000000", "安置人所选区位已有人安置！") + "');</script>", false);
                        return;
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + this.GetTran("000000", "安置人所选区位已有人安置！") + "');</script>", false);
                    return;
                }
            }


            RegistermemberBLL registermemberBLL = new RegistermemberBLL();
            string CheckMember = registermemberBLL.CheckMemberInProc(number, placement, direct, storeid);
            CheckMember = new GroupRegisterBLL().GerCheckErrorInfo(CheckMember);

            if (CheckMember != null)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + CheckMember + "');</script>", false);
                return;
            }

            string p_info = registermemberBLL.GetHavePlacedOrDriect(number, "", placement, direct);
            if (p_info != null)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + p_info + "');</script>", false);
                return;
            }

            //判断该编号是否有安置，推荐
            string GetError = registermemberBLL.GetError(direct, placement);
            if (GetError != null)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetError + "');</script>", false);

                return;
            }
            string GetError1 = new AjaxClass().CheckNumberNetAn(direct, placement);
            if (GetError1 != null && GetError1 != "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + this.GetTran("005986", "安置编号必须在推荐编号的安置网络下面！") + "');</script>", false);
                return;
            }

            #region 安置推荐人必须要激活

            if (Convert.ToInt32(DAL.DBHelper.ExecuteScalar("select COUNT(0) from MemberInfo where MemberState=1 and Number='" + direct + "'")) == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + this.GetTran("000000", "招商编号未激活！") + "');</script>", false);
                return;
            }
            if (Convert.ToInt32(DAL.DBHelper.ExecuteScalar("select COUNT(0) from MemberInfo where MemberState=1 and Number='" + placement + "'")) == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + this.GetTran("000000", "互助编号未激活！") + "');</script>", false);
                return;
            }

            #endregion
        }

        bool ispass = false;
        if (ViewState["placement"].ToString() != placement || ViewState["direct"].ToString() != direct)
        {
            Application.Lock();
            string msg = ChangeTeamBLL.UpdateNet(number, placement, direct, oldplacement, olddirect, district, ChangeTeamBLL.GetFlag(number), out ispass);
            Application.UnLock();

            ScriptHelper.SetAlert(Page, msg);
        }
        else
        {
            ScriptHelper.SetAlert(Page, this.GetTran("000000", "推荐、安置人编号未变化！"));
        }
    }

    protected string GetTopBh()
    {
        string tBh = "";
        if (Session["Store"] != null)
        {
            tBh = BLL.CommonClass.CommonDataBLL.getStoreNumber(Session["Store"].ToString());
        }
        else if (Session["Member"] != null)
        {
            tBh = Session["Member"].ToString();
        }
        else
        {
            tBh = BLL.CommonClass.CommonDataBLL.getManageID(3); ;
        }
        return tBh;
    }

    protected string GetTopBh2()
    {
        string tBh = "";
        tBh = this.txtDirect.Text.Trim();
        return tBh;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript' type='text/javascript'>history.go(-1);</script>");//history.go(-2);
    }
}