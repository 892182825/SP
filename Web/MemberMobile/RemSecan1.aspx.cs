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
using Model;
using BLL;
using DAL;
using BLL.CommonClass;
using System.Collections.Generic;
using BLL.MoneyFlows;
using System.Text.RegularExpressions;
using System.Text;
using System.IO;

public partial class RemSecan : BLL.TranslationBase
{
    bool isact = false;
    public int bzCurrency = 0;
    DataTable tab = new DataTable();
    public int id;
    int i = 0;
     public string  Number;
     public string Name;
     public string totalrmbmoney;
     public string remittancesdate;

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        //获取标准币种
        bzCurrency = CommonDataBLL.GetStandard();
        if (!IsPostBack)
        {
            id =Convert.ToInt32( Request["id"].ToString());
            if (Session["Default_Currency"] == null) Session["Default_Currency"] = bzCurrency;
            LoadDatabyorderid();
                ucPager1.Visible = false;
            translation();
        }

    }

    private void translation()
    {
        this.TranControls(this.ddlpaystate, new string[][] { new string[] { "000633", "全部" }, new string[] { "007370", "未到账" }, new string[] { "007371", "已到账" } });
        this.TranControls(this.btnnopay, new string[][] { new string[] { "000340", "查询" } });
    //    this.TranControls(this.gvSecanRemits, new string[][]{
    //        new string[]{"001892","汇款人编号"},
    //        new string[]{"000777","汇款人姓名"},
    //        new string[]{"001970","汇款金额"},
    //        new string[]{"007372","汇款申请时间"},
    //        new string[]{"007374","到账情况"},
    //        new string[]{"007375","到账时间"},
    //        new string[]{"007376","剩余有效期 "},
    //        new string[]{"000015","操作"}

    //   });
    //    this.TranControls(this.btnupload, new string[][] { new string[] { "001396", "开始上传" } });

    }

    /// <summary>
    /// 加载数据
    /// </summary>
    public void LoadDatabyorderid()
    {
        if (Session["Member"] != null)
        {

            DataTable dt_one = DAL.DBHelper.ExecuteDataTable("select * from remtemp where ID=" + id);
            string flag = Convert.ToDouble(dt_one.Rows[0]["flag"]).ToString();

            string cond = "rp.ID="+id+" and mp.Remittancesid=rp.Remittancesid  and rp.number=mi.number  and mp.remitstatus=1 and rp.isusepay=1 and  (rp.orderid<>'' or rp.number='" + Session["Member"].ToString() + "')   ";



          
                cond = cond + " and   rp.flag =   " + flag;

                string clonum = "   mi.[name],  rp.number ,rp.totalrmbmoney,rp.totalmoney,rp.flag,mp.remittancesdate,mp.ReceivablesDate,mp.Remittancesid ,rp.memberflag,rp.suremoney ,rp.filepicname,rp.CompanySureTime ";
            string table = "  remtemp  rp,Remittances mp ,(select number,name  from memberinfo  where number='" + Session["Member"].ToString() + "' or direct ='" + Session["Member"].ToString() + "') mi ";
            this.ucPager1.PageBind(0, 10, table, clonum, cond, " remittancesdate ", "rep1");
        }
    } 

    protected string GetWithdrawMoney(string WithdrawMoney)
    {
        string money = "";
        if (AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString())) == 0)
        {
            money = "0.00";
        }
        else
        {
            money = (double.Parse(WithdrawMoney) * AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()))).ToString("f2");
        }
        return money;
    }


    protected void gvSecanRemits_RowDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HiddenField lblmoney = e.Item.FindControl("HiddenField1") as HiddenField;

            HiddenField lblleavetime = e.Item.FindControl("HiddenField2") as HiddenField;

            HiddenField lblrecstatue = e.Item.FindControl("HiddenField3") as HiddenField;

            Label lblconinfo = e.Item.FindControl("lblconinfo") as Label;
            Button btnconfirmrmd = e.Item.FindControl("btnconfirmrmd") as Button;
            this.TranControls(btnconfirmrmd, new string[][] { new string[] { "007618", "款已汇出" } });
            lblconinfo.Text = GetTran("007169", "已汇出");

            int timesd = Convert.ToInt32(DateTime.Now.Subtract(Convert.ToDateTime(lblleavetime.Value).AddHours(Convert.ToDouble(Session["WTH"]))).TotalHours);
            TimeSpan ts = ((Convert.ToDateTime(lblleavetime.Value).AddHours(Convert.ToDouble(Session["WTH"]))).AddHours(48).Subtract(DateTime.Now));

            if (Convert.ToInt32(lblrecstatue.Value) != 1)
            {
                lblconinfo.Visible = false;
                btnconfirmrmd.Visible = true;
            }
        }
    }
    public string getst(DateTime dtime)
    {
        int timesd = Convert.ToInt32(DateTime.Now.Subtract(dtime).TotalHours);
        if (timesd < 144)
            return GetTran("007370", "未到账");
        else
            return GetTran("007370", "未到账");

    }
    /// <summary>
    /// 行命令事件
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvSecanRemits_RowCommand(object sender, RepeaterCommandEventArgs e)
    {
        string rimid = e.CommandArgument.ToString();
        if (e.CommandName == "cof")
        {
            string opnumber = "";
            string opip = Request.UserHostAddress.ToString();

            int ges = AddOrderDataDAL.PaymentRemitmoney(rimid, opnumber, opip, 1);
            if (ges == 0 || ges == 7)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script> alert('" + GetTran("007630", "汇款充值确认操作成功") + "！');location.href='Remsecan.aspx';</script>");
                //LoadDatabyorderid();alert('" + GetTran("009050", "到账成功") + "！');location.href='TxDetailYDZ.aspx'
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script> alert('" + GetTran("007631", "汇款充值确认失败，再次进行确认") + "！');</script>");
            }

        

        }
        if (e.CommandName == "upd")
        {
        }
    }
    protected void btnupload_Click(object sender, EventArgs e)
    {
       
    }
    /// <summary>
    /// 待充值
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnsearch_Click(object sender, EventArgs e)
    {

        LoadDatabyorderid();

    }

    public string getzt(int time, TimeSpan ts)
    {

        //int timesd = Convert.ToInt32(DateTime.Now.Subtract(Convert.ToDateTime(drv["remittancesdate"]).AddHours(Convert.ToDouble(Session["WTH"]))).TotalHours);

        if (time > 48)
        {

            return GetTran("007625", "已过期");

        }
        else
        {
           
            return (ts.Days * 24 + ts.Hours).ToString() + GetTran("007628", "小时") + ts.Minutes.ToString() + GetTran("007003", "分") + ts.Seconds.ToString() + GetTran("007629", "秒");
        }

    }



    public string GetSendType1(string sendType)
    {
        if (sendType == "1")
        {
            
            return GetTran("007341", "完成");
        }
        if (sendType == "4")
        {

            return GetTran("007627", "迟到账");
        }
        if (sendType == "0")
        {

            return GetTran("007370", "未到账");
        }
        else {

            return GetTran("007254", "处理中");
        }

    }

}