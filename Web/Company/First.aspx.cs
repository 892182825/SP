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
using DAL;
using BLL.other.Company;
using BLL.CommonClass;
using Model.Other;
using Model;

public partial class Company_Index : BLL.TranslationBase
{

    public  int jfdtx = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadff();
            //Session["page"] = "first.aspx";
            if (Session["page"] == null && Session["page"] == "first.aspx")
            {
                Response.Redirect("first.aspx");
            }
            else if (Session["page"] != null && (Session["page"].ToString() != "first.aspx"))
            {
                if (Session["page"].ToString().ToLower() == "registermember.aspx")
                { Response.Redirect("../RegisterMember/" + Session["page"].ToString()); }
                else
                {
                    Response.Redirect(Session["page"].ToString());
                }
            }

            jfdtx = Permissions.GetPermissions(EnumCompanyPermission.AccountFlowManagerWYJF);
            //else
            //{
            //    Response.Redirect(Session["page"].ToString());
            //}
        }
        //查询今天未审核订单
       String data = DateTime.Now.ToString("yyyy-MM-dd");
        String sql = "select COUNT(*) from [MemberInfo]as A,MemberOrder as B where  B.Number=A.Number and B.DefrayState = 0 and B.OrderDate Between '"+data+" 00:00:00' And '"+data+" 23:59:59'" ;
        int count =(int)DAL.DBHelper.ExecuteScalar(sql);
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        //this.b1.Attributes.Remove("onload"); 
        //if (!this.IsPostBack)
        //{
        SqlParameter[] parm0 = { new SqlParameter("@manageid", SqlDbType.VarChar, 50) };
        parm0[0].Value = Session["Company"].ToString();

        var configvalue =GetConfig();
        double mubimax = 0;
        double mubiuse = 0;
        if (configvalue == null)
        {
            mubimax = 0;
           // labmubimax.Text = "石斛积分初始发行量 0";
        }
        else
        {
            mubimax = configvalue.Para15;
            //labmubimax.Text = "石斛积分初始发行量 " + "&nbsp;<font color='red'><strong>" + configvalue.Para15 + "</strong></font>&nbsp;";
        }
        ConfigModel model = ConfigSetBLL.GetConfig(CommonDataBLL.GetMaxqishu());
        string mubisql = @"select isnull(SUM(InvestJB), 0) as InvestJB from MemberOrder where ordertype=" + 31;
       // string jjtj = "select isnull(SUM(AwardIn), 0) as AwardIn from memberinfo";
        var dtvalue = DBHelper.ExecuteDataTable(mubisql);
        //var jjvalue = DBHelper.ExecuteDataTable(jjtj);
        //mbfd.Text = model.Para16.ToString();
        //csmb.Text = model.Para15.ToString();

        if (dtvalue.Rows.Count > 0)
        {
            mubiuse = Convert.ToDouble(dtvalue.Rows[0]["InvestJB"].ToString());
            shyfx.Text = dtvalue.Rows[0]["InvestJB"].ToString();
        }
        else
        {
            mubiuse = 0;
            shyfx.Text = "0";
        }
        
        //if (jjvalue.Rows.Count > 0)
        //{
        //    labyjzj.Text = "业绩奖总计 " + "&nbsp;<font color='red'><strong>" + jjvalue.Rows[0]["AwardIn"].ToString() + "</strong></font>&nbsp;";
        //    myjzjb.Text = jjvalue.Rows[0]["AwardIn"].ToString();
        //}
        //else {
        //    labyjzj.Text = "业绩奖总计 0";
        //}
        string sqls="select SUM(happenmoney) as zsf from MemberAccount where Direction=0 and SfType=1 and KmType=29";

        DataTable dtt = DAL.DBHelper.ExecuteDataTable(sqls);
        foreach (DataRow item in dtt.Rows)
        {
           // zsf.Text = "积分释放总额：" + "&nbsp;<font color='red'><strong>" + item["zsf"].ToString() + "</strong></font>&nbsp;";
        }
       
        DataTable dt = DBHelper.ExecuteDataTable("getcompanydata", parm0, CommandType.StoredProcedure);


        jrrs.Text = dt.Rows[0][2].ToString();
        //zcrs.Text = dt.Rows[0]["mem_count"].ToString();
        //jtsf.Text = dt.Rows[0]["jtyj"].ToString();
        //dtsf.Text = dt.Rows[0]["dtyj"].ToString();
        //zrjt.Text = dt.Rows[0]["ztjtyj"].ToString();
        //zrdt.Text = dt.Rows[0]["ztdtyj"].ToString();
        //jfzsf.Text = (Convert.ToInt32(dt.Rows[0]["jtyj"]) + Convert.ToInt32(dt.Rows[0]["dtyj"])).ToString();
        //zrzsf.Text = (Convert.ToInt32(dt.Rows[0]["ztjtyj"]) + Convert.ToInt32(dt.Rows[0]["ztdtyj"])).ToString();
        //hyjf.Text = dt.Rows[0]["schy"].ToString();
        //double jjfxsr=Convert.ToDouble(dt.Rows[0]["jjfx"]);
        //jjfx.Text = jjfxsr.ToString("0.0000");
        
        //DataTable dtss = DAL.DBHelper.ExecuteDataTable("select top 1 * from DayPrice order by NowPrice desc");
        //if (dtss != null && dtss.Rows.Count > 0)
        //{
        //    shjg.Text = dtss.Rows[0]["NowPrice"].ToString();
        //    zzl.Text = dtss.Rows[0]["Addrate"].ToString();

        //}


        if (dt.Rows[0][0].ToString() == "0")
        {
            this.labgonggao.Text = GetTran("000130", "今天没有发布新公告！");
        }
        else
        {
            this.labgonggao.Text = "<a href='ManageQueryGongGao.aspx?dd=" + dt.Rows[0][10].ToString() + "'>" + GetTran("000141", "今天有") + "&nbsp;<font color='red'><strong>" + dt.Rows[0][0].ToString() + "</strong></font>&nbsp;" + GetTran("000147", "条新公告") + "！</a>";
        }
        if (dt.Rows[0][5].ToString() == "0")
        {
            this.labmembermoney.Text = GetTran("000536", "当前没有要审核的店铺退货！");
        }
        else
        {
            this.labmembermoney.Text = "<a href='RefundmentOrderBrowse.aspx'>" + GetTran("000152", "当前有") + "&nbsp;<font color='red'><strong>" + dt.Rows[0][5].ToString() + "</strong></font>&nbsp;" + GetTran("000540", "笔需要审核的店铺退货") + "！</a>";
        }
        if (dt.Rows[0][1].ToString() == "0")
        {
            this.labmail.Text = GetTran("000159", "当前没有未阅读邮件！");
        }
        else
        {
            this.labmail.Text = "<a href='ManageMessage_Recive.aspx?type=1'>" + GetTran("000152", "当前有") + "&nbsp;<font color='red'><strong>" + dt.Rows[0][1].ToString() + "</strong></font>&nbsp;" + GetTran("000160", "封新邮件") + "！</a>";
        }
        if (dt.Rows[0][6].ToString() == "0")
        {
            this.labstore.Text = GetTran("000544", "当前没有店铺未支付的订单！");
        }
        else
        {
            this.labstore.Text = "<a href='OrderPayment.aspx?type=1'>" + GetTran("000152", "当前有") + "&nbsp;<font color='red'><strong>" + dt.Rows[0][6].ToString() + "</strong></font>&nbsp;" + GetTran("000548", "笔店铺未支付的订单") + "！</a>";
        }
        if (dt.Rows[0][2].ToString() == "0")
        {
            this.labmemberregister.Text = GetTran("000165", "今日没有新注册会员！");
        }
        else
        {
            this.labmemberregister.Text = "<a href='DisplayMemberInfo.aspx?dd=" + dt.Rows[0][10].ToString() + "'>" + GetTran("000141", "今日有") + "&nbsp;<font color='red'><strong>" + dt.Rows[0][2].ToString() + "</strong></font>&nbsp;" + GetTran("000167", "个新注册会员") + "！</a>";
            
        }
        if (dt.Rows[0][7].ToString() == "0")
        {
            this.labordersent.Text = GetTran("000551", "现在没有公司未发货订单！");
        }
        else
        {
            this.labordersent.Text = "<a href='CompanyConsign.aspx'>" + GetTran("000152", "当前有") + "&nbsp;<font color='red'><strong>" + dt.Rows[0][7].ToString() + "</strong></font>&nbsp;" + GetTran("000553", "条公司未发货订单") + "！</a>";
        }
        if (dt.Rows[0][3].ToString() == "0")
        {
            this.labstoreaudting.Text = GetTran("000574", "现在没有要审核的店铺！");
        }
        else
        {
            this.labstoreaudting.Text = "<a href='AuditingStoreRegister.aspx'>" + GetTran("000152", "当前有") + "&nbsp;<font color='red'><strong>" + dt.Rows[0][3].ToString() + "</strong></font>&nbsp;" + GetTran("000575", "家需要审核的店铺") + "！</a>";
        }
        if (dt.Rows[0][8].ToString() == "0")
        {
            this.labmemberaudting.Text = GetTran("000577", "您今日没有要审核的会员报单！");
        }
        else
        {
            //((Int32)dt.Rows[0][8] - 1).ToString()
            this.labmemberaudting.Text = "<a href='BrowseMemberOrders.aspx?dd=1'>" + GetTran("000152", "当前有") + "&nbsp;<font color='red'><strong>" + dt.Rows[0][8].ToString() + "</strong></font>&nbsp;" + GetTran("000582", "笔需要审核的会员报单") + "！</a>";
        }
        if (dt.Rows[0][4].ToString() == "0")
        {
            this.labstoremoney.Text = GetTran("007589", "现在没有要审核的汇款！");
        }
        else
        {
            this.labstoremoney.Text = "<a href='DoSeecan.aspx'>" + GetTran("000152", "当前有") + "&nbsp;<font color='red'><strong>" + dt.Rows[0][4].ToString() + "</strong></font>&nbsp;" + GetTran("007596", "笔需要审核的汇款") + "！</a>";
        }
        if (dt.Rows[0][9].ToString() == "0")
        {
            this.labproduct.Text = GetTran("000590", "现在没有预警的产品！");
        }
        else
        {
            this.labproduct.Text = "<a href='ProductAlert.aspx'>" + GetTran("000152", "当前有") + "&nbsp;<font color='red'><strong>" + dt.Rows[0][9].ToString() + "</strong></font>&nbsp;" + GetTran("000596", "个预警的产品") + "</a>！";
        }

        if (dt.Rows[0]["Auti"].ToString() == "0")
        {
            this.Label1.Text = GetTran("007593", "现在没有未审核提现申请！");
        }
        else 
        {
            this.Label1.Text = "<a href='AuditWithdraw.aspx'>" + GetTran("000152", "当前有") + "&nbsp;<font color='red'><strong>" + dt.Rows[0]["Auti"].ToString() + "</strong></font>&nbsp;" + GetTran("000000", "条未审核提现申请") + "</a>！";
        }
        if (dt.Rows[0]["Singledispute"].ToString() == "0")
        {
            this.labdispute.Text = GetTran("008241", "现在没有未处理的纠纷单！");
        }
        else
        {
            this.labdispute.Text = "<a href='DisputeSheet.aspx'>" + GetTran("000152", "当前有") + "&nbsp;<font color='red'><strong>" + dt.Rows[0]["Singledispute"].ToString() + "</strong></font>&nbsp;" + GetTran("000000", "条未处理的纠纷单") + "</a>！";
        }

        //删除超市报单
        CommonDataBLL.DelOrderByState();
        
        if (getdisCount()==0)
        {
            Litw.Text = "当前没有需要处理的纠纷单";
        }
        else
        {
        //    Litw.Text=" 您当前有"+ getdisCount() +"条纠纷单未处理,请及时处理";
        }


        Label2.Text = "<a href='Billoutorder.aspx'>当前有&nbsp;<font color='red'><strong>" + DBHelper.ExecuteScalar("select COUNT(0) from StoreOrder where SendWay=0 and IsCheckOut='Y' and IsGeneOutBill='N'")+ "</strong></font>&nbsp;条服务机构要货订单</a>";
        //}
    }
    protected int getdisCount()
    {

        //string sql = "  select COUNT(*) from (select w.iscl,w.id as wid,remitnumber as hnumber,w.HkDj as hstate,w.TxDj as tstate, case when w.HkDj=1 then datediff(mi,w.auditingtime,dateadd(hh,-8,w.HkDjdate))  else datediff(mi,w.auditingtime,getutcdate()) end as hchaoshi, r.remitmoney as hmoney,w.HkJs as hshuoming,w.TxJs as hshuoming1, auditingtime as ctime,w.TxDjdate as hctime,r.remark as hremark,w.number as tnumber,case when w.TxDj=1 and w.HkDj=0 then datediff(mi,dateadd(hh,2,w.auditingtime),w.TxDjdate)  when w.TxDj=0 and w.HkDj=0 then datediff(mi,dateadd(hh,2,w.auditingtime),getutcdate())  when w.TxDj=1 and w.HkDj=1 then datediff(mi,w.HkDjdate,w.TxDjdate) else datediff(mi,w.HkDjdate,getdate()) end as tchaoshi,w.withdrawmoney as tmoney,isnull(w.khname,'')+w.bankname+w.bankcard as bankinfo,w.shenhestate from withdraw w,remittances r where w.hkid=r.id and w.shenhestate not in(0,99,-1) and Jfcl=0 and w.isjl=1) as t where 1=1 and (hchaoshi>0 or tchaoshi>0)";

        string sql = "select COUNT(*) from (select w.HkDjdate,w.TxDjdate,w.auditingtime, w.iscl,w.id as wid,remitnumber as hnumber,w.HkDj as hstate,w.TxDj as tstate,case when w.HkDj=1 then datediff(mi,w.auditingtime,dateadd(hh,-10,w.HkDjdate)) else datediff(mi,w.auditingtime,dateadd(hh,-2,getutcdate())) end as hchaoshi,r.remitmoney as hmoney,w.HkJs as hshuoming,w.TxJs as hshuoming1, auditingtime as ctime,w.TxDjdate as hctime,r.remark as hremark,w.number as tnumber,case when w.TxDj=1 and w.HkDj=0 then datediff(mi,dateadd(hh,2,w.auditingtime),w.TxDjdate)when w.TxDj=0 and w.HkDj=0 then datediff(mi,dateadd(hh,2,w.auditingtime),getutcdate()) when w.TxDj=1 and w.HkDj=1 then datediff(mi,w.HkDjdate,dateadd(hh,-24,w.TxDjdate)) else datediff(mi,w.HkDjdate,dateadd(hh,-24,getdate())) end as tchaoshi,w.withdrawmoney as tmoney,isnull(w.khname,'')+w.bankname+w.bankcard as bankinfo,w.shenhestate from withdraw w,remittances r where w.hkid=r.id and w.shenhestate not in(0,99,-1) and Jfcl=0 and w.isjl=1) as t where 1=1 and (hchaoshi>0 or tchaoshi>0) and shenHestate !=20";

        
        return (int)DBHelper.ExecuteScalar(sql);
         
    }



    public void loadff() {
        licpyujing.Visible = (Permissions.GetPermissions(EnumCompanyPermission.StorageAdminCombineProduct) > 0);

        lifwjgyhd.Visible = (Permissions.GetPermissions(EnumCompanyPermission.LogisticsBrowseStoreOrdersDelete) > 0);



        lijfd.Visible = (Permissions.GetPermissions(EnumCompanyPermission.AccountFlowManagerWYJF) > 0);

        linopayorder.Visible = (Permissions.GetPermissions(EnumCompanyPermission.LogisticsBrowseStoreOrders) > 0);

        linosendorder.Visible = (Permissions.GetPermissions(EnumCompanyPermission.LogisticsBillOutOrder) > 0);

        listore.Visible = (Permissions.GetPermissions(EnumCompanyPermission.CustomerStoreManage) > 0);

        listoreback.Visible = (Permissions.GetPermissions(EnumCompanyPermission.LogisticsRefundmentOrderBrowse) > 0);

        litodayreg.Visible = (Permissions.GetPermissions(EnumCompanyPermission.CustomerQureyMember) > 0);

        litxsq.Visible = (Permissions.GetPermissions(EnumCompanyPermission.WithdrawManager) > 0);

        //liwshhk.Visible = (Permissions.GetPermissions(EnumCompanyPermission.RemittanceHandManager) > 0);

        liwshorder.Visible = (Permissions.GetPermissions(EnumCompanyPermission.LogisticsBrowseStoreOrders) > 0);

        liwydyj.Visible = (Permissions.GetPermissions(EnumCompanyPermission.ManageMessageRecive) > 0);
        
       
             
    }


    public static ConfigModel GetConfig()
    {
        string sql = "select para15 from config  where ExpectNum=(select MAX(ExpectNum)from config) ";
        SqlDataReader dr = DBHelper.ExecuteReader(sql,CommandType.Text);
        ConfigModel model = null;
        if (dr.Read())
        {
            model = new ConfigModel();
            model.Para15 = double.Parse(dr["Para15"].ToString());
        }
        dr.Close();
        dr.Dispose();
        return model;
    }
}
