using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Model;
using BLL.MoneyFlows;
using Model.Other;
using BLL.CommonClass;
using System.Data;
using System.Data.SqlClient;
public partial class MemberMobile_OnlineYZSM : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            string sm = "select describe from JLparameter where jlcid=8";
            DataTable sm2 = DAL.DBHelper.ExecuteDataTable(sm);
            var shuomin = sm2.Rows[0]["describe"].ToString();
            Label2.Text = shuomin;
            trans();
        }
    }

    protected void trans()
    {
        this.TranControls(this.Button1, new string[][] { new string[] { "000434", "确定" } });
        this.TranControls(this.Button2, new string[][] { new string[] { "000567", "终止" } });
        //this.TranControls(this.Button3, new string[][] { new string[] { "000011", "确定" } });
    }
    protected void Btn_Click(object sender, EventArgs e)
    {
        fanhuiz.Value = "1";
        var hkid = Request.QueryString["hkid"];
        int fanhui = RemittancesBLL.jiliuZZ(hkid);
        fanhuiz.Value = fanhui.ToString();
        if (fanhui == 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("009013", "终止成功") + "！');location.href='OnlinePayment.aspx'</script>", false);
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("009012", "终止失败!") + "');</script>", false);
        }
    }

    protected void sub_Click(object sender, EventArgs e)
    {
        var hkid = Request.QueryString["hkid"];
        var RemitMoney = Request.QueryString["money"];
        object o_shenhestate = DAL.DBHelper.ExecuteScalar("select shenHestate from Withdraw where hkid=@hkid", new SqlParameter[]{
        new SqlParameter("@hkid", hkid)
        }, CommandType.Text);
        if (o_shenhestate == null)
        {
            o_shenhestate = DAL.DBHelper.ExecuteScalar("select shenHestate from Withdraw where ID=@hkid", new SqlParameter[]{
            new SqlParameter("@hkid", hkid)
            }, CommandType.Text);
            if (o_shenhestate == null)
            {
                if (o_shenhestate != null && o_shenhestate.ToString() == "-1")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("005864", "账户充值") + GetTran("001069", "无效") + "');</script>", false);
                    return;
                }

                DAL.DBHelper.ExecuteNonQuery("update Remittances set shenHestate=3 where ID=@id", new SqlParameter[]{
                new SqlParameter("@id", hkid)
                }, CommandType.Text);
            }
        }
        else
        {
            if (o_shenhestate != null && o_shenhestate.ToString() == "-1")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("005864", "账户充值") + GetTran("001069", "无效") + "');</script>", false);
                return;
            }

            DAL.DBHelper.ExecuteNonQuery("update Withdraw set shenHestate=3 where hkid=@hkid", new SqlParameter[]{
            new SqlParameter("@hkid", hkid)
            }, CommandType.Text);
        }

       
      

        huikId.Value = hkid;
        string url = "../MemberMobile/DetailDHK.aspx?hkid=" + hkid + " &RemitMoney=" + RemitMoney;
        Response.Redirect(url);
        //Page.ClientScript.RegisterStartupScript(GetType(), null, @"<script type='text/javascript'>var formobj=document.createElement('form');formobj.action='" + url + "';formobj.method='post';formobj.target='_blank';document.body.appendChild(formobj);formobj.submit(); </script>");
    }
}