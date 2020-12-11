using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Company_DisputeSheet_cl : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Permissions.CheckManagePermission(4502);
        if (!IsPostBack)
        {
            getbind();
        }
    }

    public void getbind()
    {
        string id = Request.QueryString["id"];
        string sql = @"select * from (select w.iscl,w.id as wid,remitnumber as hnumber,w.HkDj as hstate,w.TxDj as tstate,case when w.HkDj=1 then datediff(mi,w.auditingtime,w.HkDjdate) else datediff(mi,w.auditingtime,getdate()) end as hchaoshi,
r.remitmoney as hmoney,w.HkJs as hshuoming,auditingtime as ctime,w.TxDjdate as hctime,r.remark as hremark,w.number as tnumber,
case when w.TxDj=1 and w.HkDj=0 then datediff(mi,dateadd(hh,2,w.auditingtime),w.TxDjdate) when w.TxDj=0 and w.HkDj=0 then datediff(mi,dateadd(hh,2,w.auditingtime),getdate()) 
when w.TxDj=1 and w.HkDj=1 then datediff(mi,w.HkDjdate,w.TxDjdate) else datediff(mi,w.HkDjdate,getdate()) end as tchaoshi,w.InvestJB+w.InvestJBSXF as tmoney,isnull(w.khname,'')+w.bankname+w.bankcard as bankinfo,w.shenhestate
 from withdraw w,remittances r where w.hkid=r.id and ((w.shenhestate not in(0,99,-1) and Jfcl=0) or (Jfcl=1 and w.shenhestate=-1)) and w.isjl=1) as t where wid=" + id;
        DataTable dt = DAL.DBHelper.ExecuteDataTable(sql);
        if (dt.Rows.Count > 0)
        {
            lbldh.Text = id;
            lblhfname.Text = GetName(dt.Rows[0]["hnumber"].ToString());
            lblsfname.Text = GetName(dt.Rows[0]["tnumber"].ToString());
            HiddenField1.Value = GetPhone(dt.Rows[0]["hnumber"].ToString()) + " / ";
            HiddenField1.Value += GetPhone(dt.Rows[0]["tnumber"].ToString());
            txtmoney.Text = Convert.ToDouble(dt.Rows[0]["tmoney"]).ToString("f2");
            if (dt.Rows[0]["tstate"].ToString() == "1")
            {
                dz.Visible = false;
            }
        }

    }

    public string GetName(string number)
    {
        string sql = "select name from memberinfo where number='" + number + "'";
        object obj = DAL.DBHelper.ExecuteScalar(sql);
        if (obj != null)
            return obj.ToString();

        return "";
    }
    public string GetPhone(string number)
    {
        string sql = "select mobiletele from memberinfo where number='" + number + "'";
        object obj = DAL.DBHelper.ExecuteScalar(sql);
        if (obj != null)
            return obj.ToString();

        return "";
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("DisputeSheet.aspx");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string WithdrawID = Request.QueryString["id"];
        int hkflag = 0;
        if (CheckBox1.Checked)
            hkflag = 1;
        int txflag = 0;
        if (CheckBox2.Checked)
            txflag = 1;
        int IsReceive = Convert.ToInt32(Request.QueryString["status"]);
        int Revokflag = 0;
        int Getflag = 0;
        if (dz.Visible == true)
        {
            if (rbldz.SelectedValue == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "msg", "alert('请选择撤销或者到账')", true);
                return;
            }
            if (rbldz.SelectedValue == "1")
            {
                Revokflag = 1;
            }
            if (rbldz.SelectedValue == "2")
            {
                Getflag = 1;
            }
        }
        double GetMoney = 0;
        if (Getflag == 1)
        {
            bool bl = double.TryParse(txtmoney.Text.Trim(), out GetMoney);
            if (!bl)
            {
                ClientScript.RegisterStartupScript(GetType(), "msg", "alert('到账金额只能输入数字')", true);
                return;
            }
        }

        SqlParameter[] par = new SqlParameter[]{
            new SqlParameter("@WithdrawID",WithdrawID),
            new SqlParameter("@hkflag",hkflag ),
            new SqlParameter("@txflag",txflag ),
            new SqlParameter("@IsReceive",IsReceive ),
            new SqlParameter("@Revokflag",Revokflag ),
            new SqlParameter("@Getflag",Getflag  ),
            new SqlParameter("@GetMoney",GetMoney ),
            new SqlParameter("@error",SqlDbType.Int)
        };
        par[7].Direction = ParameterDirection.Output;
        DAL.DBHelper.ExecuteNonQuery("DisOrders", par, CommandType.StoredProcedure);
        int res = Convert.ToInt32(par[7].Value);
        if (res == 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "msg", "alert('执行成功');window.location='DisputeSheet.aspx'", true);
            return;
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "msg", "alert('执行失败')", true);
            return;
        }
    }
}