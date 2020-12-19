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
using BLL.CommonClass;
using DAL;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Generic;
using BLL.Registration_declarations;
using Model;
using Newtonsoft.Json.Linq;
 
public partial class Member_First : BLL.TranslationBase
{
    public string lang="";
    int bzCurrency = 0;
    public string b5 = "";
    public string b6 = "";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        
         AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));

        if (Request.QueryString["lang"] !=null)
        {
           lang = Request.QueryString["lang"].ToString();
         }
        //获取标准币种
        bzCurrency = CommonDataBLL.GetStandard();
        
        
        Response.Cache.SetExpires(DateTime.Now);
        //ajaxPro注册1111111111

        Session["LUOrder"] = Session["Member"].ToString() + ",12";

        if (!IsPostBack)
        {
            //DataTable dt_one = DAL.DBHelper.ExecuteDataTable("select isfold  from menu where parentid=162 and  sortid=261");
            //string isfold = dt_one.Rows[0]["isfold"].ToString();
            //this.kk.Text = isfold;
            
            if (Session["Default_Currency"] == null) Session["Default_Currency"] = bzCurrency;
            LoadMemberInfo();
            lblPay.Text = Common.GetnowPrice().ToString();
            //int countdls = Convert.ToInt32(DAL.DBHelper.ExecuteScalar("select count(0) from dlssettb where number='" + Session["Member"].ToString() + "'"));

            //if (countdls <= 0)
            //{
            //    //pdhy();
            //}

            //DataTable dts = DAL.DBHelper.ExecuteDataTable("select * from memberinfo where number='" + Session["Member"] + "'");
            //if (dts.Rows != null && dts.Rows.Count > 0)
            //{
            //    string b1 = dts.Rows[0]["MemberState"].ToString();//获取当前石斛积分价格
            //    string b2 = dts.Rows[0]["Direct"].ToString();

            //    if (b1 == "0")
            //    {
            //        DataTable dtss = DAL.DBHelper.ExecuteDataTable("select * from memberinfo where number='" + b2 + "'");
            //        if (dtss.Rows != null && dtss.Rows.Count > 0)
            //        {
            //            string b3 = dtss.Rows[0]["Name"].ToString();
            //            string b4 = dtss.Rows[0]["MobileTele"].ToString();
            //            b5 = "账户未激活，请联系上级激活。上级姓名：" + b3 + "   联系电话：" + b4 + "";
            //            b6 = "1";
            //        }
            //    }
            //    else
            //    {
            //        b5 = "尊敬的会员欢迎你的登录！";
            //        b6 = "0";
            //    }
            //}
        }
       // Permissions.MemRedirect(Page, Permissions.redirUrl);
       
    }

    
  

    public void LoadMemberInfo()
    {
        string number = Session["Member"].ToString();

        DataTable dtmb = DAL.DBHelper.ExecuteDataTable("select ARate,pointAin,pointAout,fuxiaoin,isnull(jackpot,0)-isnull([out],0)-isnull([membership],0) as xjye,isnull( fuxiaoin-fuxiaoout,0) as djye, isnull( pointAin-pointAout,0) as pointA ,isnull(pointBIn-pointBOut,0) as pointB,isnull(pointCIn-pointCOut,0) as pointC,isnull(pointDIn-pointDOut,0) as pointD,isnull(pointEIn-pointEOut,0) as pointE ,Name,levelint,DefaultNumber,MobileTele,isnull(zzye-xuhao,0) as zzye  from MemberInfo where Number='" + number + "'");
        
        double blv = AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()));
        decimal mrsf=0m;
        if (dtmb!=null && dtmb.Rows.Count>0)
        { DataRow dr = dtmb.Rows[0];
            if (blv == 0)
            {
                lblPay.Text = "0.00";
                //lblFx.Text = "0.00";
                //lblFxth.Text = "0.00";
                lblBonse.Text = "0.00";
            }
            else
            {
                decimal djye = Convert.ToDecimal(dr["djye"]); 
                decimal xjye = Convert.ToDecimal(dr["xjye"]);
           decimal  pointA = Convert.ToDecimal(dr["pointA"]);
                decimal pointB = Convert.ToDecimal(dr["pointB"]);
                decimal pointC = Convert.ToDecimal(dr["pointC"]);
                decimal pointD = Convert.ToDecimal(dr["pointD"]);
                decimal pointE = Convert.ToDecimal(dr["pointE"]);

                mobil.Text = Convert.ToString(dr["MobileTele"]);
                Jackpot.Text = xjye.ToString("0.0000");
                
                decimal cudayprice=Common.GetnowPrice();
                lblPay.Text = cudayprice.ToString("0.0000");

                lblPointA.Text = pointA.ToString("0.0000");
                lblPointB.Text = pointB.ToString("0.0000");
                lblPointC.Text = pointC.ToString("0.0000");
                lblPointD.Text = pointD.ToString("0.0000"); 
                lblPointE.Text = pointE.ToString("0.0000");


            }
        }
       

        string sql = "select isnull(fxlj,0) as ffxlj, * from MemberInfoBalance" + CommonDataBLL.getMaxqishu() + " where number='" + Session["Member"].ToString() + "'";
        DataTable dt = DBHelper.ExecuteDataTable(sql);
        if (dt.Rows.Count > 0)
        {
            labCurrentOneMark.Text = (Convert.ToDecimal(dt.Rows[0]["DTotalNetRecord"]) - Convert.ToDecimal(dt.Rows[0]["totaloneMark"])).ToString("f4");
            //Label1.Text = Convert.ToDecimal(dt.Rows[0]["totaloneMark"]).ToString();
            IRate.Text = (Convert.ToDecimal(dt.Rows[0]["ARate"]) * 100).ToString("0.00")+"%";
           // sfje.Text ="每日释放约：" +(mrsf * Convert.ToDecimal(dt.Rows[0]["ARate"])).ToString("0.00");
            int lv=Convert.ToInt16(dt.Rows[0]["Level"].ToString());
             if(lv==1)
             { Label1.Text = "20U"; }
        if(lv==2)
        { Label1.Text = "50U"; }
        if(lv==3)
        { Label1.Text = "100U"; }
        if(lv==4)
        { Label1.Text = "500U"; }
            if (lv == 5)
            { Label1.Text = "1000U"; }
            if (lv == 6)
            { Label1.Text = "1500U"; }
            if (lv == 7)
            { Label1.Text = "3000U"; }
            if (lv==0)
        {

            Label1.Text = "无";
        }
        int lv2 = Convert.ToInt16(dt.Rows[0]["Level2"].ToString());
        if (lv2 == 1)
        { Label2.Text = "初级节点"; }
        if (lv2 == 2)
        { Label2.Text = "中级节点"; }
        if (lv2 == 3)
        { Label2.Text = "高级节点"; }
        if (lv2 == 4)
        { Label2.Text = "顶级节点"; }
        if (lv2 == 0)
        {

            Label2.Text = "";
        }
           
        }
    }

    ////protected void Translations_More()
    ////{
    ////    sp_name.Attributes.Add("title", GetTran("000107", "姓名"));
    ////    sp_number.Attributes.Add("title", GetTran("001195", "编号"));
    ////    sp_petname.Attributes.Add("title", GetTran("001400", "昵称"));
    ////}

    public string GetBiaoZhunTime(string dt)
    {
        return Convert.ToDateTime(dt).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString("yyyy-MM-dd HH:mm");
    }

    protected string getloginRole(string str)    // 前台调用(接受对象的转换)
    {
        switch (str.Trim())
        {
            case "2":
                return GetTran("000599", "会员");
            case "1":
                return GetTran("000388", "店铺");
            default:
                return GetTran("000151", "管理员");
        }
    }

   

    private string GetEmailList() {
        SqlConnection conn = new SqlConnection(DAL.DBHelper.connString);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "MessageCheckCondition";
        conn.Open();

        string rtn = "";
        string idlist = "";
        StringBuilder sb = new StringBuilder();
        sb.Append("DropFlag=0 and LoginRole=2 and ReadFlag=0 and Receive like '%" + Session["Member"] + "%' and ID not in(select MessageID from dbo.F_DroppedReceiveByOperator('" + Session["Member"].ToString() + "',2))");
        DataTable dt = DAL.DBHelper.ExecuteDataTable("select ID,MessageSendID from MessageReceive where DropFlag=0 and loginRole=2 and Receive='*' and ID not in(select MessageID from dbo.F_DroppedReceiveByOperator('" + Session["Member"].ToString() + "',2)) and MessageType='m'");
        foreach (DataRow row in dt.Rows)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@MessageSendID", SqlDbType.Int).Value = Convert.ToInt32(row["MessageSendID"]);
            cmd.Parameters.Add("@number", SqlDbType.VarChar).Value = Session["Member"].ToString();
            cmd.Parameters.Add("@type", SqlDbType.Char).Value = '2';
            cmd.Parameters.Add("@rtn", SqlDbType.Char, 1).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            rtn = cmd.Parameters["@rtn"].Value.ToString();
            if (rtn.Equals("1"))
            {
                idlist += row["ID"].ToString() + ",";
            }
        }
        conn.Close();
        idlist = idlist.TrimEnd(",".ToCharArray());

        //对自己本身的邮件查询
        string sql1 = "SELECT ma.MessageID FROM dbo.MessageReadCondition AS ma,dbo.MessageSend AS me WHERE ma.MessageID=me.ID and DropFlag=0 and me.SenderRole=0 and me.Receive='*' and MessageType='m' and me.LoginRole=2 and ma.ConditionLeader='" + Session["Member"].ToString() + "' order by Senddate desc";
        DataTable dt1 = DAL.DBHelper.ExecuteDataTable(sql1);
        if (dt1.Rows.Count > 0)
        {
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                idlist += "," + dt1.Rows[i]["MessageID"].ToString();
            }
        }
        idlist = idlist.TrimStart(",".ToCharArray());

        if (!idlist.Equals(""))
        {
            sb.Append(" or ID in(" + idlist + ")");
        }
        return "select top 5 * from MessageReceive where " + sb.ToString() + "and ReadFlag=0 and MessageType='m'";
    }

    private string GetShopList()
    {
        string conditions = "DropFlag=0 and SenderRole=0 and Receive='*' and LoginRole=2";

        SqlConnection conn = new SqlConnection(DAL.DBHelper.connString);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "MessageCheckCondition";
        conn.Open();

        string rtn = "";
        string idlist = "";
        DataTable dt = DAL.DBHelper.ExecuteDataTable("select ID from MessageSend where " + conditions);
        foreach (DataRow row in dt.Rows)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@MessageSendID", SqlDbType.Int).Value = Convert.ToInt32(row["ID"]);
            cmd.Parameters.Add("@number", SqlDbType.VarChar).Value = Session["Member"].ToString();
            cmd.Parameters.Add("@type", SqlDbType.Char).Value = '2';
            cmd.Parameters.Add("@rtn", SqlDbType.Char, 1).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            rtn = cmd.Parameters["@rtn"].Value.ToString();
            if (rtn.Equals("1"))
            {
                idlist += row["ID"].ToString() + ",";
            }
        }
        conn.Close();
        idlist = idlist.TrimEnd(",".ToCharArray());
        //对自己本身的公告查询
        string sql1 = "SELECT ma.MessageID FROM dbo.MessageReadCondition AS ma,dbo.MessageSend AS me WHERE ma.MessageID=me.ID and DropFlag=0 and me.SenderRole=0 and me.Receive='*' and me.LoginRole=2 and ma.ConditionLeader='" + Session["Member"].ToString() + "'";
        DataTable dt1 = DAL.DBHelper.ExecuteDataTable(sql1);
        if (dt1.Rows.Count > 0)
        {
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                idlist += "," + dt1.Rows[i]["MessageID"].ToString();
            }
        }
        idlist = idlist.TrimStart(",".ToCharArray());
        string where = "";
        if (!idlist.Equals(""))
        {
            where = "ID in(" + idlist + ")";
        }
        else
        {
            where = "1=2";
        }
        return "select top 5 ID, LoginRole, Receive, InfoTitle, Content, SenderRole, Sender, Senddate, DropFlag, ReadFlag from MessageSend where DropFlag=0 and LoginRole=2 and SenderRole=0 and Receive='*' and " + where + " order by Senddate desc";
    }

    //protected void Timer1_Tick(object sender, EventArgs e)
    //{
    //    UpdatePanel1.Update();

    //    string postdz = "https://openapi.factorde.com/open/api/get_ticker";
    //    Dictionary<String, String> myDi = new Dictionary<String, String>();
    //    myDi.Add("symbol", "ftcusdt");
    //    string rspp = PublicClass.GetFunction(postdz, myDi);
    //    JObject stJson = JObject.Parse(rspp);
    //    //rmoney.Text = rspp;
    //    lblPay.Text = stJson["data"]["last"].ToString();
    //    string sql = "update DayPrice set NowPrice='" + stJson["data"]["last"].ToString() + "'";
    //    DBHelper.ExecuteNonQuery(sql);
    //}
}