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

public partial class Company_DisputeSheet_hk : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Permissions.CheckManagePermission(4501);
        if (!IsPostBack)
        {
            getbind();
        }   
    }
    public void getbind()
    {
        //string table = "remittances r,withdraw w";
        string table = @"remittances r 
 left join memberinfo m1 on r.remitnumber = m1.Number ";
        //string clounm = "r.remittancesid,r.remitnumber as number,w.HkKhname as hkhname,w.Hkbankcard as hbankcard,w.khname as tkhname,w.bankcard as tbankcard,w.withdrawmoney,r.remittancesdate,w.shenhestate,r.id";
        string clounm = @"r.shenhestate,r.actype,r.InvestJB,r.pricejb, m1. mobiletele,r.ReceivablesDate, r.remittancesid,r.remitnumber as rnumber,r.remittancesdate,r.RemitMoney,r.RemitCardtype,r.AliNo,r.WeiXNo,r.ID,
r.bankcard,r.bankname,r.Khname,m1.Name as rname ";
        StringBuilder sb = new StringBuilder();
        sb.Append(" 1=1   and  shenhestate in(-1,0,1)   ");
      
        if (txtnumber.Text.Trim() != "")
        {
            sb.Append(" and  m1.mobiletele = '" + txtnumber.Text.Trim() + "'");
        }
       
        if (txtbeigandate.Text.Trim() != "")
        {
            sb.Append(" and r.remittancesdate >= '" + txtbeigandate.Text.Trim() + " 00:00:00'");
        }
        if (txtenddate.Text.Trim() != "")
        {
            sb.Append(" and r.remittancesdate <= '" + txtenddate.Text.Trim() + " 23:59:59'");
        }
        if (DropDownList1.SelectedValue != "-2")
        {
            sb.Append(" and r.shenhestate=" + DropDownList1.SelectedValue);
        }

        Pager1.PageBind(0, 10, table, clounm, sb.ToString(), "r.id", "Repeater1");
    }

    protected string Remittancesinfo(int remitCardtype)
    {
        string str = "";
        if (remitCardtype == 0)
        {
            str = "";
        }
        else if (remitCardtype == 1)
        {
            
        }else if (remitCardtype == 2)
        {
            
        }
        return str;
    }

    protected string Withdrawinfo(int drawCardtype)
    {
        string str = "";
        if (drawCardtype == 0)
        {
            str = "";
        }
        else if (drawCardtype == 1)
        {

        }
        else if (drawCardtype == 2)
        {

        }
        return str;
    }
    public string getCoinType(string state)
    { string s = "";
        switch (state)
        {
            case "1":s = "水星币";
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
            str = "待交易 ";
        }
        if (state == "1")
        {
            str = "交易成功";
        } 
        if (state == "-1")
        {
            str = "已撤单";
        }
        return str;
    }
    public string getstatedate(string state,object auditdate)
    {
        string str = "";
        if (state == "0")
        {
            str = "--";
        }
        if (state == "1")
        {
            str = Convert.ToDateTime( auditdate).ToString("yyyy-MM-dd HH:mm:ss");
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
            int hkid =Convert.ToInt32( e.CommandArgument ); 
            int res =Convert.ToInt32(CancelRemittance(hkid));
            if (res ==1)
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

    public string CancelRemittance(int rmid)
    {
        
        DataTable dtt = DBHelper.ExecuteDataTable("select  shenhestate ,remitnumber,RemitMoney,InvestJB,priceJB   from remittances  where id =" + rmid);
        int st = -1;
        string number = "";
        double remoney = 0;

        if (dtt != null && dtt.Rows.Count > 0)
        {
            DataRow dr = dtt.Rows[0];
            st = Convert.ToInt32(dr["shenhestate"]);
            number = dr["remitnumber"].ToString();
            remoney = Convert.ToDouble(dr["RemitMoney"]);
        }
        int ccc = 0; 
        if (st != 0) return "-1";
        else
        {
            string sql7s = "update   memberinfo   set membership =membership-" + remoney + "    where  number='" + number + "' ";

            DAL.DBHelper.ExecuteNonQuery(sql7s);
            string sqls = "update   remittances   set  shenhestate=-1  where  id= " + rmid;
            ccc = DAL.DBHelper.ExecuteNonQuery(sqls);

        }




        return ccc.ToString();
    }

    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item)
        {
           
        }
    }
}