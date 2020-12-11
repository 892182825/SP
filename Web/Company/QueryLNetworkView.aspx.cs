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

public partial class Company_QueryLNetworkView : BLL.TranslationBase
{
    BLL.CommonClass.CommonDataBLL commonDataBLL = new BLL.CommonClass.CommonDataBLL();
    public string msg = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.BalanceQueryTuiJianNetworkView);

        if (!IsPostBack)
        {
            BLL.CommonClass.CommonDataBLL.BindQishuList(DropDownList_QiShu, false);
            if (ddlTeam.Text==null)
            {
                msg = "<script>alert('" + GetTran("000438", "对不起，当前没有可查看的会员网络") + "！');</script>";
                return;
            }
            else
            {
                BLL.CommonClass.CommonDataBLL.GetViewManage(ddlTeam, Session["Company"].ToString(), isAnzhi());
            }

           
        }
        Translations();
    }

    protected bool isAnzhi()
    {
        if (this.RadioButtonList_Type.SelectedValue == "llt_az")
        {
            return true;
        }
        return false;
    }

    private void Translations()
    {
        this.TranControls(this.btn_Submit, new string[][] { new string[] { "000434", "确定" } });
        this.TranControls(this.RadioButtonList_Type,
                            new string[][]
                            {	
                            new string []{"000462","安置链路图"},													
                            new string []{"000463","推荐链路图"}
                            });

    }
    /// <summary>
    /// 选择网络图显示
    /// </summary>
    /// <param name="bianhao">起点编号</param>
    /// <param name="type">选择的网络图类型</param>
    private void getInfo(string bianhao, RadioButtonList type)
    {
        if (bianhao != "")
        {
            if (!BLL.CommonClass.CommonDataBLL.GetRole(Session["Company"].ToString(), bianhao, isAnzhi()))
            {
                msg = "<script>alert('" + GetTran("000438", "对不起，您没有查看该会员网络的权限") + "！');</script>";
                return;
            }
        }
        else if (bianhao=="")
        {
            msg = "<script>alert('" + GetTran("000000", "可查看网络不能为空") + "！');</script>";
            return;
        }else
        {
            msg = "<script>alert('" + GetTran("000438", "对不起，您没有查看该会员网络的权限") + "！');</script>";
            return;
        }

        Session["jgbh"] = bianhao;
        Session["xqbh"] = bianhao;
        Session["jgqs"] = DropDownList_QiShu.SelectedValue;
        switch (type.SelectedValue)
        {
            case "wlt_tj":
                //推荐网络图
                Session["jglx"] = "tj";
                Response.Redirect("ShowNetworkView.aspx");
                break;
            case "wlt_az":
                //安置网络图
                Session["jglx"] = "az";
                Response.Redirect("ShowNetworkView.aspx");
                break;
            case "llt_tj":
                //推荐链路图
                Session["jglx"] = "tj";
                //Response.Redirect("ShowLinkView.aspx?bh=" + bianhao + "&type=tj&qs=" + DropDownList_QiShu.SelectedValue);
                Session["W_DDBH"] = ddlTeam.SelectedItem.Text;
                Session["C_L_TJ"] = bianhao;
                Response.Redirect("LLT_TJ.aspx?ThNumber=" + bianhao + "&qs=" + DropDownList_QiShu.SelectedValue + "&EndNumber=" + ddlTeam.SelectedItem.Text);
                break;
            case "llt_az":
                //安置链路图
                Session["jglx"] = "az";
                //Response.Redirect("ShowLinkView.aspx?bh=" + bianhao + "&type=az&qs=" + DropDownList_QiShu.SelectedValue);
                Session["W_DDBH"] = ddlTeam.SelectedItem.Text;
                Session["C_L_AZ"] = bianhao;
                Response.Redirect("LLT_AZ.aspx?ThNumber=" + bianhao + "&qs=" + DropDownList_QiShu.SelectedValue + "&EndNumber=" + ddlTeam.SelectedItem.Text);
                break;
        }
    }

    /// <summary>
    /// 取得要查询网络的起点编号
    /// </summary>
    /// <param name="isDian">是否是店调用</param>
    /// <returns>起点编号</returns>
    private string getBH()
    {
        string bh = txtBox_GLBH.Text;
        if (bh==null||bh=="")
        {
            
            return "";
        }else
        if (bh.Length == 0)
        {
            bh = this.ddlTeam.SelectedItem.Text;
        }
        else
        {
            bh = txtBox_GLBH.Text;
        }
        return bh;
    }



    protected void btn_Submit_Click(object sender, System.EventArgs e)
    {
        getInfo(getBH(), this.RadioButtonList_Type);
    }
    protected void RadioButtonList_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlTeam.Items.Clear();
        BLL.CommonClass.CommonDataBLL.GetViewManage(ddlTeam, Session["Company"].ToString(), isAnzhi());
    }
}
