using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Company_SetParams_futoushezhi : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
    }
    protected void lbtnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("../SetParameters.aspx");
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (txtOpen.Text == "" || txtMoney.Text == "")
        {
            BLL.CommonClass.Transforms.JSAlert(GetTran("007055", "各项内容均不可为空！"));
        }
        try
        {
            string number = "";
            decimal TotalOneMark = 0m;
            decimal ARate = 0m;
            decimal yyj = 0m;
            string Direct = "";
            string sql = "select a.number,TotalOneMark,b.ARate,a.Direct from MemberInfo a ,memberinfobalance1 b where a.number=b.number and MobileTele='" + txtOpen.Text + "'";
            DataTable shj = DBHelper.ExecuteDataTable(sql);
            if (shj.Rows.Count > 0)
            {
                number = shj.Rows[0][0].ToString();
                TotalOneMark =Convert.ToDecimal(shj.Rows[0][1].ToString());
                ARate = Convert.ToDecimal(shj.Rows[0][2].ToString());
                Direct = shj.Rows[0][3].ToString();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('无此编号，请检查后再重新输入111！')</script>");
                return;
            }
            string xsql = "select ISNULL(sum(TotalOneMark),0) as yyj from memberinfobalance1  where Direct='" + number + "'";
            DataTable dt = DBHelper.ExecuteDataTable(xsql);
            if (dt.Rows.Count > 0)
            {
                yyj = Convert.ToDecimal(dt.Rows[0][0].ToString());
                
            }
            double bs = 0;
            if (txtMoney.Text == "5000" || txtMoney.Text == "3000")
            {
                bs = 2;
            }
            else
            {
                bs = 1.5;
            }


            DBHelper.ExecuteNonQuery("insert into memberFT values('" + number + "','" + yyj + "'," + TotalOneMark + "," + txtMoney.Text + ",'" + ARate + "') ");

            DBHelper.ExecuteNonQuery("update memberinfobalance1 set TotalOneMark+=" + TotalOneMark + ",fxlj=0,level=case " + txtMoney.Text + " when 500 then 1 when 1000 then 2 when 3000 then 3 when 5000 then 4 end ,ARate=0.002  where number='" + number + "'");
            DBHelper.ExecuteNonQuery("update memberinfo set fuxiaoin=" + Convert.ToDecimal(txtMoney.Text) + "/" + Common.GetnowPrice() + "*" + bs + ",fuxiaoout=0  where number='" + number + "'");
            DBHelper.ExecuteNonQuery("WITH tb AS (select a.number,a.direct,0 AS cw from memberinfo a  where a.number='" + number + "'	union all select m.number ,m.direct,tb.cw+1 from memberinfo m ,tb where tb.direct = m.Number)update memberinfobalance1 set DCurrentTotalNetRecord+=5000,DTotalNetRecord+=5000  FROM tb a,memberinfobalance1 b where a.Number=b.number ");
            //string ftcx = "select yyj from memberFT a,memberinfo b  where a.number=b.number and a.Direct='" + Direct + "'";
            //DataTable ft = DBHelper.ExecuteDataTable(ftcx);
            //if (ft.Rows.Count > 0)
            //{
            //    DBHelper.ExecuteNonQuery("update memberFT set yyj=yyj-" + TotalOneMark + " where number='" + Direct + "'");

            //}

            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('设置成功！');", true);
            return;
           
        }
        catch (Exception ex)
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('错误: \\n" + ex.Message.Replace("'", "\\'").Replace("\r\n", "\\r\\n") + ex.StackTrace.Replace("'", "\\'").Replace("\r\n", "\\r\\n") + "');", true);
            return;
           
        }
    }
    protected void jsjj_Click(object sender, EventArgs e)
    {
        if (txtOpen.Text == "" || txtMoney.Text == "")
        {
            BLL.CommonClass.Transforms.JSAlert(GetTran("007055", "各项内容均不可为空！"));
        }
        string sql = @"  WITH tb AS (select a.number,a.direct,0 AS cw from memberinfo a  where a.MobileTele='"+txtOpen.Text+"'";
	    sql += @"
    union all
	select m.number ,m.direct,tb.cw+1 from memberinfo m ,tb where tb.direct = m.Number)select b.MobileTele,b.Name,a.level,a.level2,b.Sex,c.cw,(case when (select sum(withdrawMoney)+SUM(HappenMoney) from Withdraw w,MemberAccount d where w.number=a.Number and d.number=a.number and isAuditing=2 and Direction=1 and SfType=1 and KmType=5 )>(select sum(TotalPv/PriceJB) from memberorder where number=a.Number and ordertype in(22,23) and PayExpectNum=0 ) then 0 else 1 end) as sfhb from memberinfobalance1 a,memberinfo b,tb c where a.number=b.Number and a.number=c.Number ";
        DataTable myda = DBHelper.ExecuteDataTable(sql);
        if (myda.Rows.Count > 0)
        {
            this.GridView1.DataSource = myda;
            this.GridView1.DataBind();
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('无此编号，请检查后再重新输入111！')</script>");
            return;
        }

    }
}