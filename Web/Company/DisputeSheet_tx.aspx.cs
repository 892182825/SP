using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Company_DisputeSheet_tx : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Permissions.CheckManagePermission(4503);
        if (!IsPostBack)
        {
            getbind();
        }
    }

    public void getbind()
    {
        
        string table = @" withdraw w 
 left join memberinfo m1 on w.number = m1.Number "; 
        string clounm = @" w.id,w.pricejb, m1.mobiletele,  w.shenhestate  , w.auditingtime,w.actype, w.WithdrawTime,w.withdrawmoney,w.number,m1.Name as tname, w.InvestJB,
w.DrawCardtype,w.AliNo,w.WeiXNo  ";
        StringBuilder sb = new StringBuilder();
        sb.Append(" 1=1  ");
      
        if (txtsnumber.Text.Trim() != "")
        {
            sb.Append(" and m1.mobiletele  = '" + txtsnumber.Text.Trim() + "'");
        }
        if (txtbeigandate.Text.Trim() != "")
        {
            sb.Append(" and w.WithdrawTime >= '" + txtbeigandate.Text.Trim() + " 00:00:00'");
        }
        if (txtenddate.Text.Trim() != "")
        {
            sb.Append(" and w.WithdrawTime <= '" + txtenddate.Text.Trim() + " 23:59:59'");
        }
        if (DropDownList1.SelectedValue != "-2")
        {
            sb.Append(" and w.shenhestate in(" + DropDownList1.SelectedValue + ")");
        }
        else
        {
            sb.Append(" and w.shenhestate  in(-1,0,1)");
        }

        Pager1.PageBind(0, 10, table, clounm, sb.ToString(), "w.id", "Repeater1");
    }
    public string getCoinType(string state)
    {
        string s = "";
        switch (state)
        {
            case "1":
                s = "水星币";
                break;
            case "2":
                s = "金星币";
                break;
            case "3":
                s = "土星币";
                break;
            case "4":
                s = "木星币";
                break;
            case "5":
                s = "火星币";
                break;

        }
        return s;
    }
    public string getstate(string state)
    {
       
        string str = "";
        
          if (state == "0")
        {
            str = "待交易";
        }
        else if (state == "-1")
        {
            str = "已撤销";
        }
        else if (state == "1" )
        {
            str = "交易成功";
        }
        return str;
    }
    public string getstatedate(string state, object auditdate)
    {
        string str = "";
        if (state == "0")
        {
            str = "--";
        }
        if (state == "1")
        {
            str = Convert.ToDateTime(auditdate).ToString("yyyy-MM-dd HH:mm:ss");
        }
        if (state == "-1")
        {
            str = "已撤单";
        }
        return str;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        getbind();
    }

    protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "cs")
        {
            int hkid = Convert.ToInt32(e.CommandArgument);
            int res = Convert.ToInt32( CancelWithdraw(hkid));
            if (res == 1)
            {
                getbind();
                ClientScript.RegisterStartupScript(GetType(), "msg", "alert('撤销成功')", true);
                return;
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "msg", "alert('撤销失败')", true);
                return;
            }
        }
    }
    public string CancelWithdraw(int wdid)
    {
        
        string rec = "0";
        DataTable dt = DBHelper.ExecuteDataTable(" select number,shenHestate ,actype  ,InvestJB,priceJB,InvestJBSXF  from Withdraw  where id =" + wdid);
    

        int st = -1;
        int actype = 1;
        string number = "";
        double sellcount = 0;
        double sellprice = 0;
        double sellsxf = 0;
        if (dt != null && dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            actype = Convert.ToInt32(dr["actype"]);
            number = dr["number"].ToString();
            st = Convert.ToInt32(dr["shenHestate"]);
            sellcount = Convert.ToDouble(dr["InvestJB"]);
            sellprice = Convert.ToDouble(dr["priceJB"]);
            sellsxf = Convert.ToDouble(dr["InvestJBSXF"]);
        }
        
        if (st == -1) return "-1";
        else if (st == 1) return "1";
        else if (st == 0)
        {

            SqlTransaction tran = null;
            SqlConnection conn = null;
            try
            {
                conn = DBHelper.SqlCon();
                conn.Open();
                tran = conn.BeginTransaction();
                string sl = "";
                if (actype == 1) sl = " pointAFz =pointAFz-" + sellcount;
                else if (actype == 2) sl = " pointBFz =pointBFz-" + sellcount;
                else if (actype == 3) sl = " pointCFz =pointCFz-" + sellcount;
                else if (actype == 4) sl = " pointDFz =pointDFz-" + sellcount;
                string sql1 = @" update  memberinfo set " + sl + " ,pointEFz=pointEFz-" + sellsxf + "     where number='" + number + "'  ";
                int c = DBHelper.ExecuteNonQuery(tran, sql1);

                string sql2 = " UPDATE Withdraw	SET shenHestate = -1	WHERE  id =" + wdid;
                c += DBHelper.ExecuteNonQuery(tran, sql2);

                if (c == 2)
                {
                    tran.Commit();
                    rec = "1";
                }
                else tran.Rollback();
            }
            catch (Exception)
            {
                tran.Rollback();

            }
            finally
            {
                tran.Dispose();
                conn.Close();
                conn.Dispose();
            }

        }

        return rec;
    }


}