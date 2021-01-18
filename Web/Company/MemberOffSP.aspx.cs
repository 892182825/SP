using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using BLL.other.Company;
using BLL.CommonClass;
using Model.Other;
using Model;
using DAL;

public partial class Company_MemberOffSP : BLL.TranslationBase 
{
    protected string msg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.MemberSPOff);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));

        Translations();
        if (!this.IsPostBack)
        {

            if (Session["Company"] != null)
            {
                string ManageID = Session["Company"].ToString();
                txtOperatorNo.Text = ManageID;
                txtOperatorName.Text = DataBackupBLL.GetNameByAdminID(ManageID);
            }
            if (Request.QueryString["Number"] != null)
            {
                this.txtNumber.Text = Request.QueryString["Number"].ToString();
            }
            this.Label1.Text = GetTran("007567", "冻结会员");
            if (Request.QueryString["type"] != null)
            {
                if (Request.QueryString["Number"] != null)
                {
                    this.txtNumber.Text = Request.QueryString["Number"].ToString();
                }
                Label1.Text = GetTran("007224", "解冻会员");
            }
        }
       
    }

    private void Translations()
    {
        this.TranControls(btn_Select, new string[][] { new string[] { "000440", "查看" } });
        this.TranControls(this.Button1, new string[][] { new string[] { "000421", "返回" } });


    }

    private string GetNumber()
    {
        string manageID = Session["Company"].ToString();
        int count = 0;
        string number = BLL.CommonClass.CommonDataBLL.GetWLNumber(manageID, out count);
        if (count == 0)
        {
            return "('')";
        }
        return number;
    }

    protected void btnquery_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["type"] == null)
        {
            string number = txtNumber.Text.Trim();
            //判断会员是否存在
            if (txtNumber.Text.Trim() != "")
            {
                string sql = "select number from MemberInfo where MobileTele='" + txtNumber.Text + "'";
                DataTable shj = DBHelper.ExecuteDataTable(sql);
                if (shj.Rows.Count > 0)
                {
                    number = shj.Rows[0][0].ToString();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert(' 无此手机号，请检查后再重新输入 ！')</script>");
                    return;
                }
               
            }
            int con = MemberOffBLL.getMember(number);
            if (con == 0)
            {
                LabelResponse.Text = "<font color='red'>" + GetTran("000599", "会员") + "" + number + "" + GetTran("000801", "不存在，请重新输入") + "！</font>";

                return;
            }
            if (MemberOffBLL.getMemberZX(number) > 0)
            {
                int con1 = MemberOffBLL.getMemberSPISzx(number);
                //判断会员是否已冻结
                if (con1 == 1)
                {
                    LabelResponse.Text = "<font color='red'>" + GetTran("000599", "会员") + "" + number + "" + GetTran("007563", "已经冻结，不需要再次冻结了") + "！</font>";

                    return;
                }
            }

            string zxname = GetTran("001286", "已冻结");
            zxname = Encryption.Encryption.GetEncryptionName(zxname);
            DateTime nowTime = DateTime.Now.AddHours(Convert.ToDouble(Session["WTH"]));
            string offReason = txtMemberOffreason.Text;

            MemberOffModel mom = new MemberOffModel();
            mom.Number = number;
            mom.Zxqishu = CommonDataBLL.getMaxqishu();
            mom.Zxfate = DateTime.Now.ToUniversalTime();
            mom.OffReason = txtMemberOffreason.Text;
            mom.OperatorNo = txtOperatorNo.Text;
            mom.OperatorName = txtOperatorName.Text;

            int insertCon = MemberOffBLL.getInsertMemberSPZX(mom, zxname);
            if (insertCon > 0)
            {
                msg = "<script language='javascript'>alert('" + GetTran("007564", "冻结会员成功") + "！');window.location.href='MemberSPOffView.aspx';</script>";
            }
        }
        else
        {
            //int id = Convert.ToInt32(DAL.DBHelper.ExecuteScalar("select top 1 id from memberOffSP where number='" + txtNumber.Text.Trim() + "'  order by zxdate desc"));
            DataTable dt = DAL.DBHelper.ExecuteDataTable("select top 1 id,isnull(name,'') as name,isnull(storeid,'') as storeid,isnull(papernumber,'')as papernumber,isnull(mobiletele,'') as mobiletele from memberoffsp where number='" + txtNumber.Text.Trim() + "' order by zxdate desc");
            if (dt.Rows.Count > 0) {
                string number = txtNumber.Text;
                string reason = txtMemberOffreason.Text;
                string opert = Session["company"].ToString();
                string opname =GetTran("000151", "管理员");
                string storeid = dt.Rows[0]["storeid"].ToString();
                string name = dt.Rows[0]["name"].ToString();
                string papernumber = dt.Rows[0]["papernumber"].ToString();
                string mobiletele = dt.Rows[0]["mobiletele"].ToString();
                int id = int.Parse(dt.Rows[0]["id"].ToString());
                SqlTransaction tran = null;
                SqlConnection con = DAL.DBHelper.SqlCon();
                con.Open();
                tran = con.BeginTransaction();
                try
                {
                    string sql_insert = "insert into memberoffsp(number,[name],zxqishu,storeid,papernumber,mobiletele,iszx,offreason,operator,operatorname,zxdate) values(@number,@name,@zxqishu,@storeid,@papernumber,@mobiletele,2,@offreason,@operator,@operatorname,@zxdate)";
                    SqlParameter[] sps = { new SqlParameter("@number", number), new SqlParameter("@name", name), new SqlParameter("@zxqishu", CommonDataBLL.getMaxqishu()), new SqlParameter("@storeid", storeid), new SqlParameter("@papernumber", papernumber), new SqlParameter("@mobiletele", mobiletele), new SqlParameter("@operator", opert), new SqlParameter("@operatorname", opname), new SqlParameter("@zxdate", DateTime.Now.ToUniversalTime()), new SqlParameter("@offreason", reason) };
                    int ret = DAL.DBHelper.ExecuteNonQuery(tran, sql_insert, sps, CommandType.Text);
                    if (ret > 0)
                    {
                        string sql_updateOff = "update memberoffsp set [type]=1,operator=@Operator,operatorname=@OperatorName where id=@id and iszx=1";
                        SqlParameter[] sp_updateOff = { new SqlParameter("@Operator", opert), new SqlParameter("@operatorname", opname), new SqlParameter("@id", id) };
                        int ret1 = DAL.DBHelper.ExecuteNonQuery(tran, sql_updateOff, sp_updateOff, CommandType.Text);
                        if (ret1 > 0)
                        {
                            string sql_updateMember = "update memberinfo set memberstate=1 where number=@number";
                            SqlParameter[] sp_updateMember = { new SqlParameter("@number", number) };
                            int ret2 = DAL.DBHelper.ExecuteNonQuery(tran, sql_updateMember, sp_updateMember, CommandType.Text);
                            if (ret2 > 0)
                            {
                                tran.Commit();
                                con.Close();
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "", "alert('" + GetTran("007565", "恢复解冻完成!") + "');location.href='MemberRestoreSPOff.aspx';", true);
                            }
                            else { tran.Rollback(); con.Close(); ScriptHelper.SetAlert(Page, GetTran("007566", "恢复解冻失败")); }
                        }
                        else { tran.Rollback(); con.Close(); ScriptHelper.SetAlert(Page, GetTran("007566", "恢复解冻失败")); }
                    }
                    else
                    {
                        tran.Rollback();
                        con.Close();
                        ScriptHelper.SetAlert(Page, GetTran("007566", "恢复解冻失败"));
                    }
                }
                catch {
                    tran.Rollback();
                    con.Close();

                    ScriptHelper.SetAlert(Page, GetTran("007566", "恢复解冻失败"));
                }
            }
            
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["type"] == null)
        {
            Response.Redirect("MemberSPOffView.aspx");
        }
        else {
            Response.Redirect("MemberRestoreSPOff.aspx");
        }
    }
    protected void lkSubmit_Click(object sender, EventArgs e)
    {
        btnquery_Click(null, null);
    }
    protected void btn_Select_Click(object sender, EventArgs e)
    {
        string Number = txtNumber.Text;
        if (Number.Length <= 0)
        {
            LabelResponse.Text = "<font color='red'>" + GetTran("000129", "对不起，会员编号不能为空！") + "</font>";
            return;
        }
        if (ChangeTeamBLL.CheckNum(Number))
        {
            LabelResponse.Text = "<font color='red'>" + GetTran("000288", "对不起,该会员编号不存在") + "</font>";
            return;
        }

        Response.Redirect("DisplayMemberDeatail.aspx?ID=" + Number + "&type=MemberoffSP");
    }
}
