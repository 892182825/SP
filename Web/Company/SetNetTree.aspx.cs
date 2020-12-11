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

public partial class Company_SetNetTree : BLL.TranslationBase 
{

    public string msg = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        if (!this.IsPostBack)
        {
            GetBind();
        }
    }

    private void GetBind()
    {
        string strSql = "Select id,menuname,isfold,menutype From Menu Where ID in (105,106,108)";
        DataTable dt = DAL.DBHelper.ExecuteDataTable(strSql);

        this.cbList1.DataSource = dt;
        this.cbList1.DataTextField = "menuname";//绑定的字段名   
        this.cbList1.DataValueField = "id";//绑定的值   
        this.cbList1.DataBind();

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["isFold"].ToString() == "1")
            {
                cbList1.Items[i].Selected = false;
            }
            if (dt.Rows[i]["isFold"].ToString() == "0")
            {
                cbList1.Items[i].Selected = true;
            }
        }

        strSql = "Select id,menuName,isfold,menutype from menu where id in (167,168,169)";
        DataTable dt1 = DAL.DBHelper.ExecuteDataTable(strSql);

        this.CheckBoxList1.DataSource = dt1;
        this.CheckBoxList1.DataTextField = "menuname";//绑定的字段名   
        this.CheckBoxList1.DataValueField = "id";//绑定的值   
        this.CheckBoxList1.DataBind();

        for (int i = 0; i < dt1.Rows.Count; i++)
        {
            if (dt1.Rows[i]["isFold"].ToString() == "1")
            {
                this.CheckBoxList1.Items[i].Selected = false;
            }
            if (dt1.Rows[i]["isFold"].ToString() == "0")
            {
                this.CheckBoxList1.Items[i].Selected = true;
            }
        }

        strSql = "Select * from ViewLayer";
        DataTable dt2 = DAL.DBHelper.ExecuteDataTable(strSql);
        for (int i = 0; i < dt2.Rows.Count; i++)
        {
            if (dt2.Rows[i]["type"].ToString() == "0")
            {
                this.TextBox1.Text = dt2.Rows[i]["cengshu"].ToString();
            }
            else
            {
                this.TxtCengS.Text = dt2.Rows[i]["cengshu"].ToString();
            }
        }        
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        try
        {
            Convert.ToInt32(this.TxtCengS.Text.Trim());
            Convert.ToInt32(this.TextBox1.Text.Trim());
        }
        catch
        {
            msg = "<script>alert('" + GetTran("000897", "显示层数输入格式错误") + "！');</script>";
        }
        using (SqlConnection conn = new SqlConnection(DAL.DBHelper.connString))
        {
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            try
            {
                int isShow = 0,count=0;
                string strSql = "";

                for (int i = 0; i < cbList1.Items.Count; i++)
                {
                    if (cbList1.Items[i].Selected)
                    {
                        isShow = 0;
                    }
                    else
                    {
                        isShow = 1;
                    }
                    strSql = "Update Menu Set isfold=" + isShow + " Where menuType=2 and id=" + cbList1.Items[i].Value;
                    count = (int)DAL.DBHelper.ExecuteNonQuery(tran,strSql);
                    if (count == 0)
                    {
                        msg = "<script>alert('" + GetTran("000002", "修改失败") + "！');</script>";
                        tran.Rollback();
                        return;
                    }
                }

                for (int i = 0; i < this.CheckBoxList1.Items.Count; i++)
                {
                    if (CheckBoxList1.Items[i].Selected)
                    {
                        isShow = 0;
                    }
                    else
                    {
                        isShow = 1;
                    }
                    strSql = "Update Menu Set isfold=" + isShow + " Where menuType=3 and id=" + CheckBoxList1.Items[i].Value;
                    count = (int)DAL.DBHelper.ExecuteNonQuery(tran,strSql);
                    if (count == 0)
                    {
                        msg = "<script>alert('" + GetTran("000002", "修改失败") + "！');</script>";
                        tran.Rollback();
                        return;
                    }
                }

                strSql = "Update ViewLayer Set cengshu=@cengshu Where type=1";
                SqlParameter[] para = {
                                          new SqlParameter("@cengshu",Convert.ToInt32(this.TxtCengS.Text.Trim()))
                                      };
                count = (int)DAL.DBHelper.ExecuteNonQuery(tran, strSql, para, CommandType.Text);
                if (count == 0)
                {
                    msg = "<script>alert('" + GetTran("000002", "修改失败") + "！');</script>";
                    tran.Rollback();
                    return;
                }

                strSql = "Update ViewLayer Set cengshu=@cengshu Where type=0";
                SqlParameter[] para1 = {
                                          new SqlParameter("@cengshu",Convert.ToInt32(this.TextBox1.Text.Trim()))
                                      };
                count = (int)DAL.DBHelper.ExecuteNonQuery(tran, strSql, para1, CommandType.Text);
                if (count == 0)
                {
                    msg = "<script>alert('" + GetTran("000002", "修改失败") + "！');</script>";
                    tran.Rollback();
                    return;
                }
                
                tran.Commit();
                msg = "<script>alert('" + GetTran("000001", "修改成功") + "！');</script>";

            }
            catch
            {
                msg = "<script>alert('" + GetTran("000002", "修改失败") + "！');</script>";
                tran.Rollback();
                return;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
