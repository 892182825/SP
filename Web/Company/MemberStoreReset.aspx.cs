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
using Model;
using DAL;
using System.Text;
using Model.Other;
using BLL.CommonClass;

public partial class Company_MemberStoreReset : BLL.TranslationBase
{
    protected string msg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.CustomerQueryMemberStoreReset);
        btnSeach_Click(null, null);
    }
    private void Translations()
    {
        this.TranControls(this.givTWmember,
                new string[][]{
                    new string []{"000024","会员编号"},
                    new string []{"001012","调整前店铺编号"},
                    new string []{"001013","调整后店铺编号"},
                    new string []{"000045","期数"},
                    new string []{"001014","调整类别"},
                    new string []{"000613","日期"}});
        this.TranControls(this.Button2, new string[][] { new string[] { "000096", "返 回" } });
        this.TranControls(this.btnWL, new string[][] { new string[] { "001016", "店铺重置" } });
        this.TranControls(this.btnSeach, new string[][] { new string[] { "000048", "查 询" } });


        this.TranControls(this.rdofangshi,
                new string[][]{
                    new string []{"001032","个人所属店铺"},
                    new string []{"001035","团队所属店铺"}});


    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

        if (this.txtstoreId.Text.ToString().Length < 1)
        {
            this.msg = "<script language='javascript'>alert('" + GetTran("001018", "请输入会员编号！") + "')</script>";
            return;
        }

        //判断登录者是否有更改此会员的权限
        string manageId = BLL.CommonClass.CommonDataBLL.getManageID(1);
        if (txtstoreId.Text.Trim() == manageId)
        {
            msg = "<script>alert('" + GetTran("007131", "对不起，您没有更改此会员的权限！") + "');</script>";
            return;
        }

        if (DisposeString.DisString(txtstoreId.Text.Trim()) == "")
            Response.End();
        //OwnerStoreResetBLL owner = new OwnerStoreResetBLL();
        MemberInfoModel member = OwnerStoreResetBLL.GetMemberInfoById(DisposeString.DisString(txtstoreId.Text.Trim()));
        if (member != null)
        {
            tbShow.Visible = true;
            lblMemberID.Text = member.Number.ToString();
            lblName.Text =Encryption.Encryption.GetDecipherName(member.Name);
            lblOldStoreID.Text = member.StoreID;
        }
        else
        {
            this.tbShow.Visible = false;
            this.msg = "<script language='javascript'>alert('" + GetTran("001021", "你输入的会员不存在！") + "')</script>";
            // ScriptHelper.SetAlert(btnSearch,"你输入的会员不存在！！！");
            return;
        }
        Translations();
    }
    protected void btnEndit_Click(object sender, EventArgs e)
    {
       #region 
        /* **********************
        if (this.txtNewStoreID.Text.ToString().Length > 0)
        {
            if (StoreRegisterConfirmBLL.CheckStoreId(DisposeString.DisString(txtNewStoreID.Text.Trim())))
            {
                if (Convert.ToInt32(rdofangshi.SelectedValue) == 1)
                {
                    string memberid = DisposeString.DisString(txtstoreId.Text.Trim());
                    string newStoreid = DisposeString.DisString(txtNewStoreID.Text.Trim());
                    string oldStoreid = DisposeString.DisString(lblOldStoreID.Text.Trim());
                    int flag = Convert.ToInt32(this.rdofangshi.SelectedValue);
                    int ex = CommonDataBLL.getMaxqishu();
                    if (OwnerStoreResetBLL.MemberStoreReset(memberid, newStoreid, oldStoreid, flag, ex))
                    {
                        this.msg = "<script language='javascript'>alert('修改成功！！！')</script>";
                        btnSearch_Click(null, null);
                        this.txtNewStoreID.Text = "";
                    }
                    else
                    {
                        this.msg = "<script language='javascript'>alert('修改失败！！！')</script>";
                    }
                }
                else
                {
                    string memberid = DisposeString.DisString(txtstoreId.Text.Trim());
                    string newStoreid = DisposeString.DisString(txtNewStoreID.Text.Trim());
                    string oldStoreid = DisposeString.DisString(lblOldStoreID.Text.Trim());
                    int flag = Convert.ToInt32(this.rdofangshi.SelectedValue);
                    int ex = CommonDataBLL.getMaxqishu();
                    if (OwnerStoreResetBLL.MemberStoreReset2(memberid, newStoreid, oldStoreid, flag, ex))
                    {
                        this.msg = "<script language='javascript'>alert('修改成功！！！')</script>";
                        btnSearch_Click(null, null);
                        this.txtNewStoreID.Text = "";
                    }
                    else
                    {
                        this.msg = "<script language='javascript'>alert('修改失败！！！')</script>";
                    }
                }
            }
            else
            {
                this.msg = "<script language='javascript'>alert('对不起，新店铺不存在！！！')</script>";
            }
        }
        else
        {
            this.msg = "<script language='javascript'>alert('请输入新店铺编号！！！')</script>";
        }
        */
#endregion
        if (this.txtNewStoreID.Text.ToString().Length > 0)
        {
            if (StoreRegisterConfirmBLL.CheckStoreId(DisposeString.DisString(txtNewStoreID.Text.Trim())))
            {
                string InUP_Sql = "";


                string memberid = DisposeString.DisString(txtstoreId.Text.Trim());
                string newStoreid = DisposeString.DisString(txtNewStoreID.Text.Trim());
                string oldStoreid = DisposeString.DisString(lblOldStoreID.Text.Trim());
                int flag = Convert.ToInt32(this.rdofangshi.SelectedValue);
                int expectnum = CommonDataBLL.getMaxqishu();
                DateTime time = DateTime.Now;
                string RiQi = time.ToString();

                int isMod = Convert.ToInt32(DBHelper.ExecuteScalar("select count(*) from updatestore where flag=1 and expectnum=" + expectnum + " and number='" + memberid + "'"));
                //int isMod1 = Convert.ToInt32(DBHelper.ExecuteScalar("select count(*) from updatestore where flag=2 and expectnum=" + expectnum + " and number='" + memberid + "'"));

                DataTable dt_one = DAL.DBHelper.ExecuteDataTable("select StoreState from StoreInfo where  StoreID='" + txtNewStoreID.Text + "'");
                string StoreState = dt_one.Rows[0]["StoreState"].ToString();//汇款id
                if (StoreState == "0")
                {
                    msg = "<script>alert('该服务机构未激活，无法重置！');</script>";
                    return;
                }


                using (SqlConnection conn = new SqlConnection(DAL.DBHelper.connString))
                {
                    conn.Open();
                    SqlTransaction tran = conn.BeginTransaction();
                    try
                    {
                        if (flag == 1)
                        {
                            string upSql = "update memberinfo set storeid='" + this.txtNewStoreID.Text.Trim() + "' where number='" + memberid + "'";
                            if (isMod == 0)
                            {
                                InUP_Sql = @"insert into updatestore(number,OldStoreID,NewStoreID,expectnum,flag,UpdDate)
								values('" + memberid + "','" + oldStoreid + "','" + newStoreid + "'," + expectnum + ",1,'" + RiQi + "')";
                            }
                            else
                            {
                                InUP_Sql = "update updatestore set OldStoreID='" + oldStoreid + "',NewStoreID='" + newStoreid + "',flag=1 where Number='" + memberid + "' and expectnum=" + expectnum;
                            }
                           // BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("storeinfo", "(ltrim(rtrim(id)))");
                           // cl_h_info.AddRecord(ID);
                            DBHelper.ExecuteNonQuery(tran, upSql, null, CommandType.Text);
                            DBHelper.ExecuteNonQuery(tran, InUP_Sql, null, CommandType.Text);
                           // cl_h_info.ModifiedIntoLogs(BLL.CommonClass.ChangeCategory.Store, Session["Company"].ToString(), BLL.CommonClass.ENUM_USERTYPE.Company);

                        }
                        else
                        {
                            if (isMod == 0)
                            {
                                InUP_Sql = @"insert into updatestore(number,OldStoreID,NewStoreID,expectnum,flag,UpdDate)
								values('" + memberid + "','" + oldStoreid + "','" + newStoreid + "'," + expectnum + ",2,'" + RiQi + "')";
                            }
                            else
                            {
                                InUP_Sql = "update updatestore set OldStoreID='" + oldStoreid + "',NewStoreID='" + newStoreid + "',flag=2 where Number='" + memberid + "' and expectnum=" + expectnum;
                            }

                            SqlParameter[] parm ={new SqlParameter("@bianhao",SqlDbType.VarChar,20),
													 new SqlParameter("@storeid",SqlDbType.VarChar,20)};
                            parm[0].Value = memberid;
                            parm[1].Value = newStoreid;

                            DBHelper.ExecuteNonQuery(tran, InUP_Sql, null, CommandType.Text);
                            DBHelper.ExecuteNonQuery(tran, "UpdateMemberStoreid", parm, CommandType.StoredProcedure);
                        }
                        tran.Commit();
                        msg = "<script>alert('" + GetTran("001024", "更改成功！") + "')</script>";
                        this.lblOldStoreID.Text = this.txtNewStoreID.Text.ToString();
                        this.txtNewStoreID.Text = "";

                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            else
            {

                msg = "<script>alert('" + GetTran("001027", "新的所属店铺编号不存在！") + "')</script>";
            }
        }
        else
        {
            this.msg = "<script language='javascript'>alert('" + GetTran("001030", "请输入新店铺编号！") + "')</script>";
        }
    }
    protected void lkSubmit_Click(object sender, EventArgs e)
    {
        btnSearch_Click(null, null);
    }
    protected void lkSubmit1_Click(object sender, EventArgs e)
    {
        btnEndit_Click(null, null);
    }
    protected void btnWL_Click(object sender, EventArgs e)
    {
        this.div2.Style.Add("display","none");
        this.div1.Style.Add("display", "block");
    }

    protected void btnSeach_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("1=1");

        string mb = DisposeString.DisString(txt_member.Text);
        if (mb.Length > 0)
        {
            sb.Append(" and Number like '%" + this.txt_member.Text.Trim() + "%'");

        }
        if (this.DatePicker2.Text != "")
        {
            sb.Append(" and year(upddate)=year('" + this.DatePicker2.Text + "') and month(upddate)=month('" + this.DatePicker2.Text + "') and day(upddate)=day('" + this.DatePicker2.Text + "') ");
        }
        UserControl_ExpectNum exp = Page.FindControl("ExpectNum1") as UserControl_ExpectNum;
        int ExpectNum = exp.ExpectNum;
        if (ExpectNum > 0)
        {
            sb.Append(" and ExpectNum=" + ExpectNum);
        }
        Pager pager = Page.FindControl("Pager1") as Pager;
        pager.PageBind(0, 10, "updatestore", "number ,oldstoreid,newstoreid,expectnum,case  flag when 1 then '"+ GetTran("001032", "个人所属店铺重置") +"' when 2 then '"+ GetTran("001035", "团队所属店铺重置") +"' end as flag, upddate", sb.ToString(), "upddate", "givTWmember");
        Translations();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        this.div2.Style.Add("display", "block");
        this.div1.Style.Add("display", "none");
        btnSeach_Click(null,null);
    }
    protected void givTWmember_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Translations();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        btnSeach_Click(null, null);
    }
}
