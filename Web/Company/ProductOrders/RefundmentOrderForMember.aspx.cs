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
using BLL.other.Member;

public partial class Company_ProductOrders_RefundmentOrderForMember : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string number = string.Empty;
        Translations();
        if (Session["Member"] != null)
        {
            number = Session["Member"].ToString();
            Response.Redirect("RefundmentOrderForMemberAdd.aspx?number=" + number);
        }
    }

    private void Translations()
    {
        this.TranControls(this.btn_Confirm, new string[][] { new string[] { "000064", "确认" } });
        this.TranControls(this.btn_Cancel, new string[][] { new string[] { "007737", "不同意" } });
        this.TranControls(this.btn_ArgeeConfirm, new string[][] { new string[] { "007738", "同意并确认" } });
 
    }
    protected void btn_Confirm_Click(object sender, EventArgs e)
    {
        string number = this.txt_MemberID.Text.Trim();
        var member = MemberInfoModifyBll.getMemberInfo(number);
        if (member != null)
        {
            this.pnl_Argeement.Visible = true;
            this.pnl_NumberConfirm.Visible = false;
            //跳过协议直接跳转
            btn_ArgeeConfirm_Click(null, null);
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("001021", "你输入的会员不存在！") + "');</script>", false);
        }
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        this.pnl_Argeement.Visible = false;
        this.pnl_NumberConfirm.Visible = true;
    }
    protected void btn_ArgeeConfirm_Click(object sender, EventArgs e)
    {
        Response.Redirect("RefundmentOrderForMemberAdd.aspx?number=" + this.txt_MemberID.Text);

        #region 
        /*
        string url = Request.Url.ToString ();
        string targetUrl = string.Empty;
        string prevUrl = "http://";
        string port = Request.Url.Port.ToString ();
        string doman = Request.Url.Host;
        prevUrl += doman;
        if (port != "80" && port != string.Empty)
        {
            prevUrl += ":" + port;
        }
        //站点名
        string siteName = Request.ApplicationPath.TrimStart('/');
        if (siteName != string.Empty)
        {
            prevUrl += "/" + siteName;
        }
        targetUrl = prevUrl + "/Company/ProductOrders/RefundmentOrderBrowseForMember.aspx?number=" + this.txt_MemberID.Text;
        Response .Redirect (url);
         */ 
        #endregion
    }
    protected void txt_MemberID_TextChanged(object sender, EventArgs e)
    {
        if(!string .IsNullOrEmpty (this.txt_MemberID .Text))
        {
            var member = MemberInfoModifyBll.getMemberInfo(this.txt_MemberID.Text);
            if (member != null)
            {
                lbl_MemberName.Text =GetTran("000107", "姓名：") + member.Name;
            }
        }
    }
}
