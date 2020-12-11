using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Company_JLCanshu : BLL.TranslationBase
{
    public string msg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        if (!this.IsPostBack)
        {
            string sql = "select * from JLparameter where jlcid=1";
            DataTable dt = DAL.DBHelper.ExecuteDataTable(sql);

            Dhktime.Text = dt.Rows[0]["value"].ToString();

            string sql2 = "select * from JLparameter where jlcid=2 ";
            DataTable dt2 = DAL.DBHelper.ExecuteDataTable(sql2);
            Dcstime.Text = dt2.Rows[0]["value"].ToString();

            string sql3 = "select * from JLparameter where jlcid=3";
            DataTable dt3 = DAL.DBHelper.ExecuteDataTable(sql3);
            sfgc.Text = dt3.Rows[0]["value"].ToString();

            string sql4 = "select * from JLparameter where jlcid=4";
            DataTable dt4 = DAL.DBHelper.ExecuteDataTable(sql4);
            hfgc.Text = dt4.Rows[0]["value"].ToString();

            string sql5 = "select * from JLparameter where jlcid=5";
            DataTable dt5 = DAL.DBHelper.ExecuteDataTable(sql5);
            tkje.Text = dt5.Rows[0]["value"].ToString();

            string sql6 = "select * from JLparameter where jlcid=6";
            DataTable dt6 = DAL.DBHelper.ExecuteDataTable(sql6);
            hkje.Text = dt6.Rows[0]["value"].ToString();

            string sql8 = "select * from JLparameter where jlcid=8";
            DataTable dt8 = DAL.DBHelper.ExecuteDataTable(sql8);
            txtEnote.Text = dt8.Rows[0]["describe"].ToString();

        }

    }

    private void Translations()
    {
        //this.TranControls(this.lbtnReturn, new string[][] { new string[] { "000096", "返 回" } });
        this.TranControls(this.btnOK, new string[][] { new string[] { "002190", "确 定" } });
        //TranControls(rbtS, new string[][] { new string[] { "000233", "是" }, new string[] { "000235", "否" } });
    }


    protected void btnOK_Click(object sender, EventArgs e)
    {
        bool isOk = BLL.CommonClass.CommonDataBLL.Jinliu(this.Dhktime.Text, this.Dcstime.Text, this.sfgc.Text, this.hfgc.Text, this.tkje.Text, this.hkje.Text,this.txtEnote.Text);
        if (isOk)
        {
       
            //msg = "<script>alert('" + GetTran("005820", "设置成功！"');location.href='JLCanshu.aspx');</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("005820", "设置成功！") + "');location.href='JLCanshu.aspx'</script>", false);

        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("006922", "设置失败！") + "');location.href='JLCanshu.aspx'</script>", false);
            //msg = "<script>alert('" + GetTran("006922", "设置失败！") + "');</script>";
        }
    }

}