using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using DAL;
public partial class Company_SetParams_SetHolidays : BLL.TranslationBase
{
    protected string type = "", id = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, "../index.aspx");

        type = Request.QueryString["type"] == "" ? "" : Request.QueryString["type"];
        id = Request.QueryString["id"] == "" ? "" : Request.QueryString["id"];
        if (!IsPostBack)
        {
            if (type == "2") { 
                DefaultBind(id);
            }
        }
        Translations();
    }
    private void DefaultBind(string id)
    {
        string sql = @"select ID,StartTime,EndTime,Content from Holidays where id=" + id;
        DataTable exchangeTime = DBHelper.ExecuteDataTable(sql);

        if (exchangeTime.Rows.Count == 0)
        {
            return;
        }
        else
        {
            txtBox_OrderDateTimeStart.Text = exchangeTime.Rows[0]["StartTime"].ToString();
            txtBox_OrderDateTimeEnd.Text = exchangeTime.Rows[0]["EndTime"].ToString();
            txtContent.Text = exchangeTime.Rows[0]["Content"].ToString();
        }
    }
    private void Translations()
    {
        this.TranControls(this.lbtnReturn, new string[][] { new string[] { "000096", "返 回" } });
        this.TranControls(this.btnOK, new string[][] { new string[] { "002190", "确 定" } });
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        string totalDataStart = txtBox_OrderDateTimeStart.Text.Trim();
        string totalDataEnd = txtBox_OrderDateTimeEnd.Text.Trim();
        string content = txtContent.Text;
        if (string.IsNullOrEmpty(content)) {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('节假日名字不能为空')</script>");
            return;
        }
        if (totalDataStart == "") {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('开始时间不能为空')</script>");
            return;
        }
        if(totalDataEnd==""){
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('结束时间不能为空')</script>");
            return;
        }

        try
        {
           
            var startTime = Convert.ToDateTime(totalDataStart);
            var endTime = Convert.ToDateTime(totalDataEnd).AddHours(23).AddMinutes(59).AddSeconds(59);
           
            if (type != null && type == "1")
            {
                SqlParameter[] paras = new SqlParameter[3];
                paras[0] = new SqlParameter("@StartTime", startTime);
                paras[1] = new SqlParameter("@EndTime", endTime);
                paras[2] = new SqlParameter("@Content", content);
                DBHelper.ExecuteNonQuery("insert into Holidays values(@StartTime,@EndTime,@Content) ", paras, CommandType.Text);
            }
            else if (type != null && type == "2")
            {
                SqlParameter[] paras = new SqlParameter[4];
                paras[0] = new SqlParameter("@StartTime", startTime);
                paras[1] = new SqlParameter("@EndTime", endTime);
                paras[2] = new SqlParameter("@Content", content);
                paras[3] = new SqlParameter("@ID", id);
                DBHelper.ExecuteNonQuery("update Holidays set StartTime=@StartTime,EndTime=@EndTime,Content=@Content where ID=@ID", paras, CommandType.Text);
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