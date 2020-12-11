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
using Model.Other;

using System.Data.SqlClient;

public partial class Company_QuerySMS : BLL.TranslationBase 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.QuerySMS);
        if (!IsPostBack)
        {
            btnSearch_Click(null, null);
        }
        Translations();
    }
    private void Translations()
    {
        this.TranControls1(this.GridView1,
                new string[][]{
                    new string []{"000000",""},
                    
                    new string []{"000752","发送时间"},
                     
                    new string []{"005612","状态"},
                    new string []{"000024","会员编号"},
                    new string []{"001400","昵称"}, 
                    new string []{"005623","手机号码"},
                    new string []{"005622","短信内容"},
                    
                   
                    new string []{"007891","重发"},
                    new string []{"000022","删除"}});
        this.TranControls(this.ddlState, 
               new string[][]{
                    new string []{"001600","成功"},
                    new string []{"001618","失败"},
                    new string []{"000633","所有"}}
                );
        TranControls(Button1, new string[][] { new string[] { "007893", "多选删除" } });
        TranControls(Button2, new string[][] { new string[] { "007894", "多选重发" } });
        this.TranControls(this.btnSearch, new string[][] { new string[] { "000340", "查询" } });       

    }
    private string getCondition()
    {
        string condition = "1=1";       
        string sendFlag = this.ddlState.SelectedItem.Value.Trim();
        if(sendFlag !="-1")
        {        
            condition += " and hm.sucflag=" + sendFlag;
        }
        try
        {
            string dateS = BLL.CommonClass.ValidData.InputText(this.txtDateS.Text.Trim());
            string dateE = BLL.CommonClass.ValidData.InputText(this.txtDateE.Text.Trim());
            if (dateS != "")
            {
                condition += " and hm.SendDate >='" + DateTime.Parse(dateS) + "'";
            }
            if (dateE != "")
            {
                condition += " and hm.SendDate <='" + DateTime.Parse(dateE).AddDays(1) + "'";
            }
        }
        catch 
        {
            BLL.CommonClass.Transforms.JSAlert(GetTran ("005624","时间格式错误！"));
        }
        string mobile = BLL.CommonClass.ValidData.InputText(this.txtMobile.Text.Trim());
        if (mobile != "")
        {
            condition += " and hm.mobile = '" + mobile + "'";
        }
        string keywords = BLL.CommonClass.ValidData.InputText(this.txtKeyWords.Text.Trim());
        if (keywords != "")
        {
            condition += " and hm.sendMSg like '%" + keywords + "%'";
        }
        return condition;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string table = " h_mobilemsg hm ";

        string columns = " hm.Category,hm.ID,hm.CustomerID,hm.sendMsg,hm.senddate,hm.CustomerID,hm.mobile,hm.BillID,case hm.sucflag when '1' then '" + GetTran("000000", "成功") + "' else '" + GetTran("000000", "失败") + "' end as sucflag,SendNo,SendSmallNo  ";
        string condition = getCondition();
        string key = "hm.ID ";
       
        Pager1.PageBind(0, 10, table, columns, condition, key, "GridView1");
        Translations();
		
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();
        if (e.CommandName == "Del")
        {
            using (SqlConnection con = new SqlConnection(DAL.DBHelper.connString))
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                try
                {
                    BLL.MobileSMS sms = new BLL.MobileSMS();
                    BLL.CommonClass.ChangeLogs cl = new BLL.CommonClass.ChangeLogs("h_mobileMsg", "ltrim(rtrim(str(id)))"); 
                    
                    cl.AddRecordtran(tran, id);
                    sms.DelSMS(tran, id);

                    cl.AddRecordtran(tran, id.ToString());
                    cl.DeletedIntoLogs(BLL.CommonClass.ChangeCategory.company65, Session["Company"].ToString(),BLL.CommonClass. ENUM_USERTYPE.objecttype10); 
 

                    tran.Commit();
                    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('" + BLL.Translation.Translate("000008", "删除成功") + "')", true);
                    btnSearch_Click(null, null);
                }
                catch
                {  
                    tran.Rollback();
                    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('" + BLL.Translation.Translate("000009", "删除失败") + "')", true);
                  
                }
                finally 
                {
                    con.Close();
                }
            }
        }

        if (e.CommandName == "AgainSend")
        {
            string info = "";
            string[] AllValue = e.CommandArgument.ToString().Split(',');
           string CommName = e.CommandArgument.ToString();
           using (SqlConnection con = new SqlConnection(DAL.DBHelper.connString))
           { 
                 con.Open();
                SqlTransaction tran = con.BeginTransaction();
                try
                {
                    BLL.MobileSMS sms = new BLL.MobileSMS();
                    BLL.MobileSMS.SendMsgTo(tran, AllValue[2], AllValue[2], AllValue[1], AllValue[3], out info, (Model.SMSCategory)Convert.ToInt32(AllValue[4]));
                    sms.DelSMS(tran, AllValue[0]);
                    tran.Commit();
                 //BLL.MobileSMS.SendMsgTo(tran, bianhao, bianhao, mobile, msg, out info, Model.SMSCategory.sms_ManualSent);
                    ClientScript.RegisterStartupScript(this.GetType(),"","alert('"+info+"');",true);

                    
                    btnSearch_Click(null, null);
                }
                catch 
                {
                    tran.Rollback();
                    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('" + info + "');", true);
                    
                }
                finally 
                {
                    con.Close();
                    
                }
           }
           
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < this.GridView1.Rows.Count; i++)
        {
            if ((this.GridView1.Rows[i].FindControl("CheckBox1") as CheckBox).Checked)
            {
                string  ss = ((HiddenField)GridView1.Rows[i].FindControl("HiddenField1")).Value;
                using (SqlConnection con = new SqlConnection(DAL.DBHelper.connString))
                {
                    con.Open();
                    SqlTransaction tran = con.BeginTransaction();
                    try
                    {
                        BLL.MobileSMS sms = new BLL.MobileSMS();
                        BLL.CommonClass.ChangeLogs cl = new BLL.CommonClass.ChangeLogs("h_mobileMsg", "ltrim(rtrim(str(id)))");

                        cl.AddRecordtran(tran, ss);
                        sms.DelSMS(tran, ss);

                        cl.AddRecordtran(tran, ss);
                        cl.DeletedIntoLogs(BLL.CommonClass.ChangeCategory.company65, Session["Company"].ToString(), BLL.CommonClass.ENUM_USERTYPE.objecttype10);


                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                    }
                    finally
                    {
                        con.Close();
                    }
                }
               
            }
        }

        btnSearch_Click(null, null);
    }

    public string GetPetName(string number)
    {
        return Encryption.Encryption.GetDecipherName(BLL.other.Company.StoreRegisterBLL.GetMemberPetName(number));
    }


    protected void Button2_Click(object sender, EventArgs e)
    {
        int TrueCount = 0;
        string info = "";
        for (int i = 0; i < this.GridView1.Rows.Count; i++)
        {
            if ((this.GridView1.Rows[i].FindControl("CheckBox1") as CheckBox).Checked)
            {
                string ss = ((HiddenField)GridView1.Rows[i].FindControl("HiddenField1")).Value;
                using (SqlConnection con = new SqlConnection(DAL.DBHelper.connString))
                {
                    con.Open();
                    SqlTransaction tran = con.BeginTransaction();
                    try
                    {
                        BLL.MobileSMS sms = new BLL.MobileSMS();
                        DataTable dt = sms.GetSMS(tran, ss);

                        BLL.MobileSMS.SendMsgTo(tran, dt.Rows[0]["CustomerID"].ToString(), dt.Rows[0]["CustomerID"].ToString(), dt.Rows[0]["mobile"].ToString(), dt.Rows[0]["sendMsg"].ToString(), out info, (Model.SMSCategory)Convert.ToInt32(dt.Rows[0]["Category"].ToString()));
                        sms.DelSMS(tran, ss);
                        if (info.IndexOf("发送成功") != -1)
                        {
                            TrueCount++;
                        }
                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                    }
                    finally
                    {
                        con.Close();
                    }
                }

            }
        }
        ClientScript.RegisterStartupScript(this.GetType(), "", "alert('" + GetTran("007902", "共成功发送") + TrueCount + GetTran("006978", "条") + "！')", true);
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddlState.SelectedValue == "0")
        {
            this.Button2.Visible = true;
        }
    }
}
