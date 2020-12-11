using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DAL;
using System.Data;
using System.Data.SqlClient;
public partial class Company_SetParams_QueryHolidays : BLL.TranslationBase
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack){
            DataBindMemberBank();
        }
    }
    /// <summary>
    /// 绑定数据
    /// </summary>
    protected void DataBindMemberBank()
    {
        string sql = @"select ID,StartTime,EndTime,Content from Holidays";
        ///获取会员使用银行信息       
        DataTable dtMemberBank = DBHelper.ExecuteDataTable(sql);
        ViewState["sortHolidays"] = dtMemberBank;
        DataView dv = new DataView((DataTable)ViewState["sortHolidays"]);
        if (ViewState["sortHolidaystring"] == null)
            ViewState["sortHolidaystring"] = dtMemberBank.Columns[0].ColumnName.Trim();
        dv.Sort = ViewState["sortHolidaystring"].ToString();
        this.gvMemberBank.DataSource = dv;
        this.gvMemberBank.DataBind();
    }

    protected void lbtDelete_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandArgument == null) {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('删除失败');</script>", false);
        }
        int id = Convert.ToInt32(e.CommandArgument);

        
        var sql = @"delete Holidays where ID=@ID";
        SqlParameter[] para = {
                                      new SqlParameter("@ID",SqlDbType.Int),
                                  };
        para[0].Value = id;
        var  returnvalue = DBHelper.ExecuteNonQuery(sql, para, CommandType.Text);
        if (returnvalue > 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('删除成功');</script>", false);
            DataBindMemberBank();
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('删除失败');</script>", false);
        }

    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("../SetParams/SetHolidays.aspx?type=1");
    }

    /// <summary>
    /// 返回添加参数页面
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("../SetParameters.aspx");
    }

    protected void lbtEdit_Command(object sender, CommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);
        Response.Redirect("../SetParams/SetHolidays.aspx?type=2&&id=" + id);
    }

    protected void gvMemberBank_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            int primaryKey = Convert.ToInt32(this.gvMemberBank.DataKeys[e.Row.RowIndex].Value.ToString());
            ((LinkButton)e.Row.FindControl("lbtEdit")).CommandArgument = primaryKey.ToString();
            ((LinkButton)e.Row.FindControl("lbtDelete")).CommandArgument = primaryKey.ToString();
        }

        ///控制样式
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
        }
    }
    /// <summary>
    /// GridView排序事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvMemberBank_Sorting(object sender, GridViewSortEventArgs e)
    {
        
    }
}