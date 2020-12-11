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

public partial class UserControl_MemberTop : System.Web.UI.UserControl
{
    protected BLL.TranslationBase tran = new BLL.TranslationBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ThreeRedirect(Page, Permissions.redirUrl);
        DataTable dt_one = DAL.DBHelper.ExecuteDataTable("select GrantState from GrantSet");
        string GrantState = dt_one.Rows[0]["GrantState"].ToString();//是否显示提现申请

        int countdls = Convert.ToInt32(DAL.DBHelper.ExecuteScalar("select count(0) from dlssettb where number='"+Session["Member"]+"'"));

        if (countdls<=0)
        {
            dlsblli.Visible = false;
        }

        DataTable dt_two = DAL.DBHelper.ExecuteDataTable("select * from Menu  Where menuType=3 and sortid='167'");
        DataTable dt_two1 = DAL.DBHelper.ExecuteDataTable("select * from Menu  Where menuType=3 and sortid='168'");
        DataTable dt_two2 = DAL.DBHelper.ExecuteDataTable("select * from Menu  Where menuType=3 and sortid='169'");

        string isfold = dt_two.Rows[0]["isfold"].ToString();//是否显示推荐网络图
        string isfold1 = dt_two1.Rows[0]["isfold"].ToString();//是否显示安置网络图
        string isfold2 = dt_two2.Rows[0]["isfold"].ToString();//是否显示会员链路图

        if (isfold == "1")
        {
            tj.Visible = false;
           
        }
        if (isfold1 == "1")
        {
            az.Visible = false;
          
        }
        if (isfold2 == "1")
        {
            lianlu.Visible = false;
        }


        if (GrantState == "0")
        {
            txsq.Visible = false;
            txsqll.Visible = false;
        }
        if (Session["Member"] != null)
        {
            if (dingbuuse.Value == "")
            {
                dingbuuse.Value = Encryption.Encryption.GetEncryptionPwd(Session["Member"].ToString(), Session["Member"].ToString());
            }
            else
            {
                if (dingbuuse.Value != Encryption.Encryption.GetEncryptionPwd(Session["Member"].ToString(), Session["Member"].ToString()))
                {
                    Session["Member"] = null;
                    Response.Redirect("../Logout.aspx",true);
                }
            }
        }
    }
}
