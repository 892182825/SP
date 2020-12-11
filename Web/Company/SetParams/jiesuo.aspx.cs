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
            AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        }
        Translations();
    }

    private void DefaultBind()
    {
        
    }
    private void Translations()
    {
        this.TranControls(this.lbtnReturn, new string[][] { new string[] { "000096", "返 回" } });
        this.TranControls(this.btnOK, new string[][] { new string[] { "002190", "确 定" } });
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (txtOpen.Text == "" )
        {
            BLL.CommonClass.Transforms.JSAlert(GetTran("007055", "各项内容均不可为空！"));
        }
        try
        {
            string number = "";
            int TotalOneMark = 0;
            decimal ARate = 0m;
            decimal yyj = 0m;
            string Direct = "";
            string sql = "select number,zzye-xuhao as yyj from MemberInfo where MobileTele='" + txtOpen.Text + "'";
            DataTable shj = DBHelper.ExecuteDataTable(sql);
            if (shj.Rows.Count > 0)
            {
                number = shj.Rows[0][0].ToString();
                yyj = Convert.ToDecimal(shj.Rows[0][1].ToString());
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('无此编号，请检查后再重新输入111！')</script>");
                return;
            }



            if (yyj <= 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('此人没有锁仓或已释放完毕请核对后重新输入！')</script>");
                return;
            }
            else
            {
                DBHelper.ExecuteNonQuery("update memberinfo set Out=Out-zzye+xuhao,xuhao=zzye where Number='" + number + "'");
                DBHelper.ExecuteNonQuery("INSERT INTO MemberAccount(Number,HappenTime,HappenMoney,BalanceMoney,Direction,SfType,KmType,Remark)SELECT a.number,GETutcDATE(),zzye-xuhao,a.Jackpot-a.[out],0,1,29,'可用锁仓解锁'+CAST(zzye-xuhao as nvarchar(50)) from memberinfo a where zzye>0 and Number='" + number + "'");

            }
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('设置成功！');", true);
            return;
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