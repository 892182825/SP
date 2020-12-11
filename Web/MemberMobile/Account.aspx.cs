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
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;
using System.Collections.Generic;
using Newtonsoft.Json;

public partial class MemberMobile_Account : BLL.TranslationBase
{
    public string nic = "";
    protected string msg = "";
    protected string msg1 = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        if (!IsPostBack)
        {
            Bind();
        }
    }
    public void Bind()
    {
        decimal iRate = 0;
        decimal aRate = 0;
        DataTable dt_one = DAL.DBHelper.ExecuteDataTable("select isnull(jackpot-out,0) as xjye, isnull(TotalRemittances-TotalDefray,0)as xfye  , isnull(InvestIn-Investout,0)as tzdj,isnull(AwardIn-AwardOut,0)as jldj  ,isnull(fuxiaoin-fuxiaoout,0)as fxje,   * from memberinfo where Number='" + Session["Member"].ToString() + "'");
     if(dt_one!=null&&dt_one.Rows.Count>0){
        decimal xjye=Convert.ToDecimal(dt_one.Rows[0]["xjye"]);
           decimal xfye=Convert.ToDecimal(dt_one.Rows[0]["xfye"]);
           decimal tzdj=Convert.ToDecimal(dt_one.Rows[0]["tzdj"]);
           decimal jldj=Convert.ToDecimal(dt_one.Rows[0]["jldj"]);
         decimal fxje=Convert.ToDecimal(dt_one.Rows[0]["fxje"]);
            iRate = (Convert.ToDecimal(dt_one.Rows[0]["IRate"])*1000);
            aRate= Convert.ToDecimal(dt_one.Rows[0]["ARate"]) * 1000;
           nic = dt_one.Rows[0]["PetName"].ToString();
        this.Jackpot.Text =xjye.ToString();
        this.TotalRemittances.Text = xfye.ToString("f2");
        this.InvestIn.Text =tzdj.ToString();
        this.fuxiaoin.Text = fxje.ToString();
        int QiShu = (int)DAL.CommonDataDAL.GetMaxExpect();
        DataTable dttt = DAL.DBHelper.ExecuteDataTable("select  * from MemberInfoBalance" + QiShu.ToString() + " where Number='" + Session["Member"].ToString() + "'");
        if (dttt != null && dttt.Rows.Count > 0)
        {
            //if(Convert.ToDecimal(dttt.Rows[0]["IRate"]) * 1000!=0)
            //iRate = Convert.ToDecimal(dttt.Rows[0]["IRate"])*1000;
            //this.IRate.Text = (Convert.ToDecimal(dttt.Rows[0]["IRate"]) * 1000).ToString("f2") + "‰";
            this.AwardIn.Text = jldj.ToString();
                //this.ARate.Text = (Convert.ToDecimal(dttt.Rows[0]["ARate"]) * 1000).ToString("f2") + "‰";
            //if (Convert.ToDecimal(dttt.Rows[0]["ARate"])*1000 != 0)
            //{
            //   aRate = Convert.ToDecimal(dttt.Rows[0]["ARate"])*1000;
            //}
                labCurrentOneMark.Text= Convert.ToDecimal(dttt.Rows[0]["CurrentOneMark"]).ToString("f4");
                labTotalNetRecord.Text = Convert.ToDecimal(dttt.Rows[0]["TotalNetRecord"]).ToString("f4");
            labCurrentTotalNetRecord.Text= Convert.ToDecimal(dttt.Rows[0]["CurrentTotalNetRecord"]).ToString("f4");
            }
        DataTable dts = DAL.DBHelper.ExecuteDataTable("select top 1 * from config order by createdate desc");
        if (dts != null && dts.Rows.Count > 0)
        {
            this.Addrate.Text = (Convert.ToDecimal(dts.Rows[0]["para14"]) * 100).ToString("f2") + "%";
        }
        DataTable dtss = DAL.DBHelper.ExecuteDataTable("select top 1 * from DayPrice order by NowPrice desc");
        decimal price = 0;
        if (dtss != null && dtss.Rows.Count > 0)
        {
             price = Convert.ToDecimal(dtss.Rows[0]["NowPrice"]);
            this.NowPrice.Text = price.ToString("0.0000");

            lblzzc.Text = (((xjye + tzdj + jldj) * price) + xfye).ToString("0.00");
        }
            this.IRate.Text = iRate.ToString("f2") + "‰";
            this.ARate.Text = aRate.ToString("f2") + "‰";

            DataTable ddss = DAL.DBHelper.ExecuteDataTable("select top 7 * from DayPrice order by NowDate desc");
        if (ddss != null && ddss.Rows.Count > 0)
        {
            if (ddss.Rows.Count < 7)
            {
                string[] strArray = new string[] { };
                //List<string> dd1 = new List<string>();
                string ddt = "[";
                
                for (int i = 0; i < ddss.Rows.Count; i++)
                {
                    string ssd = ddss.Rows[i]["NowPrice"].ToString();
                    int j = 0;
                    j = 6 - i;
                    if (ssd == "")
                    {

                        //dd1.Add(price.ToString());
                       
                        ddt +="["+ j +","+ price.ToString() + "],";
                    }
                    else
                    {
                        //dd1.Add(ssd);
                        ddt += "[" + j + "," + ssd + "],";  
                    }

                }
                ddt = ddt.Substring(0, ddt.Length - 1);
                ddt += "];";
                msg1 = "<script language='javascript'>var dd1=" + ddt + " var dd2=" + (Convert.ToDouble(price)-0.5) + ";var dd3=" + (Convert.ToDouble(price) + 0.5) + ";</script>";
            }
            else
            {
                msg1 = "<script language='javascript'>var dd1 = [[0," + ddss.Rows[0]["NowPrice"].ToString() + "],  [1," + ddss.Rows[1]["NowPrice"].ToString() + "],  [2," + ddss.Rows[2]["NowPrice"].ToString() + "],  [3," + ddss.Rows[3]["NowPrice"].ToString() + "],  [4," + ddss.Rows[4]["NowPrice"].ToString() + "],  [5," + ddss.Rows[5]["NowPrice"].ToString() + "],  [6," + ddss.Rows[6]["NowPrice"].ToString() + "]]; var dd2=" + (Convert.ToDouble(price) - 0.5) + ";var dd3=" + (Convert.ToDouble(price) + 0.5) + ";</script>";
           
            }


        }


        msg = "<script language='javascript'>var data = [{ label: '可用积分账户', data: '" + xjye + "'}, { label: '消费账户', data: '" + xfye + "'},{ label: '注册积分账户', data: '" + fxje + "'}, { label: '投资积分账户(冻结)', data: '" + tzdj + "'},{ label: '奖励积分账户(冻结)', data: '" + jldj + "'}];</script>";

     }
    }
}