using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Data.SqlClient;
using DAL;
using System.Data;
using Model.Other;
public partial class Company_SetParams_ExchangeTime : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, "../index.aspx");
        if (!IsPostBack)
        {
            DefaultBind();
        }
        Translations();
    }

    private void DefaultBind()
    {
        DataTable exchangeTime = DBHelper.ExecuteDataTable("select top 1 * from ExchangeTime");

        if (exchangeTime.Rows.Count == 0)
        {
            return;
        }
        else
        {
            txtOpen.Text = exchangeTime.Rows[0]["OpenTime"].ToString();
            txtClose.Text = exchangeTime.Rows[0]["CloseTime"].ToString();
            txtOpen1.Text = exchangeTime.Rows[0]["OpenTime1"].ToString();
            txtClose1.Text = exchangeTime.Rows[0]["CloseTime1"].ToString();
            if (txtClose1.Text != "" && txtOpen1.Text != "")
            {
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>$('#kq1').show(); $('#gb1').show();i++;</script>");
            }
            txtOpen2.Text = exchangeTime.Rows[0]["OpenTime2"].ToString();
            txtClose2.Text = exchangeTime.Rows[0]["CloseTime2"].ToString();
            if (txtClose2.Text != "" && txtOpen2.Text != "")
            {
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript1", "<script>$('#kq1').show(); $('#gb1').show();$('#kq2').show(); $('#gb2').show();i=i+2;</script>");
            }
            txtOpen3.Text = exchangeTime.Rows[0]["OpenTime3"].ToString();
            txtClose3.Text = exchangeTime.Rows[0]["CloseTime3"].ToString();
            if (txtClose3.Text != "" && txtOpen3.Text != "")
            {
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript2", "<script>$('#kq1').show(); $('#gb1').show();$('#kq2').show(); $('#gb2').show();$('#kq3').show(); $('#gb3').show();i=i+3;</script>");
            }
        }
    }
    private void Translations()
    {
        this.TranControls(this.lbtnReturn, new string[][] { new string[] { "000096", "返 回" } });
        this.TranControls(this.btnOK, new string[][] { new string[] { "002190", "确 定" } });
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (txtOpen.Text == "" || txtClose.Text == "")
        {
            BLL.CommonClass.Transforms.JSAlert(GetTran("007055", "各项内容均不可为空！"));
        }
        try
        {
            SqlParameter[] paras = new SqlParameter[8];
            paras[0] = new SqlParameter("@OpenTime", txtOpen.Text.Trim());
            paras[1] = new SqlParameter("@CloseTime", txtClose.Text.Trim());
            paras[2] = new SqlParameter("@OpenTime1", txtOpen1.Text.Trim());
            paras[3] = new SqlParameter("@CloseTime1", txtClose1.Text.Trim());
            paras[4] = new SqlParameter("@OpenTime2", txtOpen2.Text.Trim());
            paras[5] = new SqlParameter("@CloseTime2", txtClose2.Text.Trim());
            paras[6] = new SqlParameter("@OpenTime3", txtOpen3.Text.Trim());
            paras[7] = new SqlParameter("@CloseTime3", txtClose3.Text.Trim());


            if (DBHelper.ExecuteScalar("select count(0) from ExchangeTime").ToString() == "0")
            {
                DBHelper.ExecuteNonQuery("insert into ExchangeTime values(@OpenTime,@CloseTime,@OpenTime1,@CloseTime1,@OpenTime2,@CloseTime2,@OpenTime3,@CloseTime3) ", paras, CommandType.Text);
            }
            else
            {
                DBHelper.ExecuteNonQuery("update ExchangeTime set OpenTime=@OpenTime,CloseTime=@CloseTime,OpenTime1=@OpenTime1,CloseTime1=@CloseTime1,OpenTime2=@OpenTime2,CloseTime2=@CloseTime2,OpenTime3=@OpenTime3,CloseTime3=@CloseTime3", paras, CommandType.Text);
            }

            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("005820", "设置成功！") + "')</script>");
        }
        catch (Exception ex)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("005821", "设置失败") + "：" + ex.Message.ToString() + "');</script>");
        }
    }

    protected void lbtnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("../SetParameters.aspx");
    }
}