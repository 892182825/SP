using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Company_WithdrawShezhi : BLL.TranslationBase
{
    public string msg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        if (!this.IsPostBack)
        {
            string sql = "select WithdrawMinMoney,WithdrawSXF from WithdrawSz ";
            DataTable dt = DAL.DBHelper.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                WithdrawMinMoney.Text = dt.Rows[0]["WithdrawMinMoney"].ToString();
                WithdrawSXF.Text = dt.Rows[0]["WithdrawSXF"].ToString();
            }
        }
    }

    private void Translations()
    {
        //this.TranControls(this.lbtnReturn, new string[][] { new string[] { "000096", "返 回" } });
        this.TranControls(this.btnOK, new string[][] { new string[] { "002190", "确 定" } });
        //TranControls(rbtS, new string[][] { new string[] { "000233", "是" }, new string[] { "000235", "否" } });
    }


    /// <summary>
    /// 确定事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        bool isOk = BLL.CommonClass.CommonDataBLL.SetGrantTx(this.WithdrawMinMoney.Text, this.WithdrawSXF.Text);
        if (isOk)
        {
            //msg = "<script>alert('" + GetTran("005820", "设置成功！") + ";location.href='WithdrawShezhi.aspx');</script>";

            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("005820", "设置成功！") + "');location.href='WithdrawShezhi.aspx'</script>", false);
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("006922", "设置失败！") + "');location.href='WithdrawShezhi.aspx'</script>", false);
            //msg = "<script>alert('" + GetTran("006922", "设置失败！") + "');</script>";
        }
    }


    /// <summary>
    /// 返回上级菜单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("../SetParameters.aspx");
    }
}