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
using Model.Other;
public partial class Company_NetWorkDisplaySetting : BLL.TranslationBase
{
    public string msg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.SafeDetailQuerySetting);

        Response.Cache.SetExpires(DateTime.Now);
        if (!IsPostBack)
        {
            string NetWorkDisplayStatus = Application["NetWorkDisplayStatus"].ToString().Trim();
            //string NetWorkDisplayStatus="11111111111";
            string NetWorkDisplayTextStatus = "";

            NetWorkDisplayTextStatus = DAL.CommonDataDAL.GetDisplayField(Session["LanguageCode"].ToString(), CheckBoxList1,0);

            NetWorkDisplayTextStatus = NetWorkDisplayTextStatus.Trim(",".ToCharArray());
            LblStatus.Text = NetWorkDisplayTextStatus;

            NetWorkDisplayTextStatus = DAL.CommonDataDAL.GetDisplayField(Session["LanguageCode"].ToString(), CheckBoxList2,1);

            NetWorkDisplayTextStatus = NetWorkDisplayTextStatus.Trim(",".ToCharArray());
            Label1.Text = NetWorkDisplayTextStatus;

            NetWorkDisplayTextStatus = DAL.CommonDataDAL.GetDisplayField(Session["LanguageCode"].ToString(), CheckBoxList3,2);

            NetWorkDisplayTextStatus = NetWorkDisplayTextStatus.Trim(",".ToCharArray());
            Label2.Text = NetWorkDisplayTextStatus;
        }
        Translations();
    }

    private void Translations()
    {
        this.TranControls(this.CheckBoxList1,
                new string[][]{
                    new string []{"000046","级别"},
                    new string []{"000939","新个分数"},
                    new string []{"000936","新网分数"},
                    new string []{"000934","新网人数"},
                    new string []{"000933","总网人数"},
                    new string []{"000937","总网分数"}});
        this.TranControls(this.btnsetup, new string[][] { new string[] { "000434", "确定" } });

        this.TranControls(this.CheckBoxList2,
               new string[][]{
                    new string []{"000046","级别"},
                    new string []{"000939","新个分数"},
                    new string []{"000936","新网分数"},
                    new string []{"000934","新网人数"},
                    new string []{"000933","总网人数"},
                    new string []{"000937","总网分数"}});
        this.TranControls(this.Button1, new string[][] { new string[] { "000434", "确定" } });

        this.TranControls(this.CheckBoxList2,
               new string[][]{
                    new string []{"000046","级别"},
                    new string []{"000939","新个分数"},
                    new string []{"000936","新网分数"},
                    new string []{"000934","新网人数"},
                    new string []{"000933","总网人数"},
                    new string []{"000937","总网分数"}});
        this.TranControls(this.Button2, new string[][] { new string[] { "000434", "确定" } });
    }

    protected void btnsetup_Click(object sender, EventArgs e)
    {
        string NetWorkDisplayTextStatus = "";
        int flag = 0;
        int count = 0;
        using (SqlConnection conn = new SqlConnection(DBHelper.connString))
        {
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            try
            {
                for (int i = 0; i < CheckBoxList1.Items.Count; i++)
                {
                    if (CheckBoxList1.Items[i].Selected)
                    {
                        flag = 1;
                        NetWorkDisplayTextStatus += CheckBoxList1.Items[i].Text.Trim() + ",";
                    }
                    else
                    {
                        flag = 0;
                    }

                    count = (int)DAL.CommonDataDAL.UpdateDisplayField(CheckBoxList1.Items[i].Value, flag,0, tran);

                    if (count < 1)
                    {
                        msg = "<script language='javascript'>alert('" + GetTran("001616", "更新失败") + "！');</script>";
                        tran.Rollback();
                        return;
                    }

                }
                tran.Commit();
            }
            catch
            {
                tran.Rollback();
                msg = "<script language='javascript'>alert('" + GetTran("001616", "更新失败") + "！');</script>";
                return;
            }
            finally
            {
                conn.Close();
            }
        }
        NetWorkDisplayTextStatus = NetWorkDisplayTextStatus.Trim(",".ToCharArray());
        LblStatus.Text = NetWorkDisplayTextStatus;
        msg = "<script language='javascript'>alert('" + GetTran("001615", "更新成功") + "！');</script>";
        Translations();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string NetWorkDisplayTextStatus = "";
        int flag = 0;
        int count = 0;
        using (SqlConnection conn = new SqlConnection(DBHelper.connString))
        {
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            try
            {
                for (int i = 0; i < CheckBoxList2.Items.Count; i++)
                {
                    if (CheckBoxList2.Items[i].Selected)
                    {
                        flag = 1;
                        NetWorkDisplayTextStatus += CheckBoxList2.Items[i].Text.Trim() + ",";
                    }
                    else
                    {
                        flag = 0;
                    }

                    count = (int)DAL.CommonDataDAL.UpdateDisplayField(CheckBoxList2.Items[i].Value, flag, 1, tran);

                    if (count < 1)
                    {
                        msg = "<script language='javascript'>alert('" + GetTran("001616", "更新失败") + "！');</script>";
                        tran.Rollback();
                        return;
                    }

                }
                tran.Commit();
            }
            catch
            {
                tran.Rollback();
                msg = "<script language='javascript'>alert('" + GetTran("001616", "更新失败") + "！');</script>";
                return;
            }
            finally
            {
                conn.Close();
            }
        }
        NetWorkDisplayTextStatus = NetWorkDisplayTextStatus.Trim(",".ToCharArray());
        Label1.Text = NetWorkDisplayTextStatus;
        msg = "<script language='javascript'>alert('" + GetTran("001615", "更新成功") + "！');</script>";
        Translations();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        string NetWorkDisplayTextStatus = "";
        int flag = 0;
        int count = 0;
        using (SqlConnection conn = new SqlConnection(DBHelper.connString))
        {
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            try
            {
                for (int i = 0; i < CheckBoxList3.Items.Count; i++)
                {
                    if (CheckBoxList3.Items[i].Selected)
                    {
                        flag = 1;
                        NetWorkDisplayTextStatus += CheckBoxList3.Items[i].Text.Trim() + ",";
                    }
                    else
                    {
                        flag = 0;
                    }

                    count = (int)DAL.CommonDataDAL.UpdateDisplayField(CheckBoxList3.Items[i].Value, flag, 2, tran);

                    if (count < 1)
                    {
                        msg = "<script language='javascript'>alert('" + GetTran("001616", "更新失败") + "！');</script>";
                        tran.Rollback();
                        return;
                    }

                }
                tran.Commit();
            }
            catch
            {
                tran.Rollback();
                msg = "<script language='javascript'>alert('" + GetTran("001616", "更新失败") + "！');</script>";
                return;
            }
            finally
            {
                conn.Close();
            }
        }
        NetWorkDisplayTextStatus = NetWorkDisplayTextStatus.Trim(",".ToCharArray());
        Label2.Text = NetWorkDisplayTextStatus;
        msg = "<script language='javascript'>alert('" + GetTran("001615", "更新成功") + "！');</script>";
        Translations();
    }
}
