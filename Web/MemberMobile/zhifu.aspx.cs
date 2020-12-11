using BLL.CommonClass;
using DAL;
using Model;
using Model.Other;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MemberMobile_zhifu : BLL.TranslationBase
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["Member"] = "c616d3f3a2e532355b049a2230f20b83";
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));


        





        if (!IsPostBack)
        {


            if (Session["languageCode"] == null)
            {
                SqlDataReader sdr = DAL.DBHelper.ExecuteReader("Select Top 1 * From LANGUAGE ORDER BY ID");
                while (sdr.Read())
                {
                    Session["languageCode"] = sdr["languageCode"].ToString().Trim();
                    Session["LanguageID"] = sdr["id"].ToString();
                }
                sdr.Close();
                sdr.Dispose();
            }
            LoadMemberInfo();
            Session["UserType"] = 3;
            Session["LUOrder"] = Session["Member"].ToString() + ",12";
            Session["languageCode"] = "L001";
        }
           

    }

    public void LoadMemberInfo()
    {
        SqlDataReader sdr = DAL.DBHelper.ExecuteReader("select ARate,pointAin,pointAout,isnull(jackpot,0)-isnull([out],0) as xjye,isnull( fuxiaoin-fuxiaoout,0) as djye, isnull( pointAin-pointAout,0) as jldj  ,Name,levelint,DefaultNumber,MobileTele from MemberInfo where Number='" + Session["Member"] + "'");
        String str = (String)Session["Member"];
        //double blv = AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()));

        if (sdr.Read())
        {
            //lblNumber.Text = Session["Member"].ToString();

            //根据用户选择语言来翻译变量，L001中文，L002英文
            //if ((String)Session["languageCode"] == "L001")
            //{
            ////lblName.Text = Encryption.Encryption.GetDecipherName(sdr["Name"].ToString());
            //lblPetName.Text = sdr["petName"].ToString();
            //第二个参数表示翻译类别
            //lblLevel.Text = CommonDataBLL.GetLevelStr(sdr["levelint"].ToString());

            //  lblLevel.Text = sdr["levelint"].ToString() == "0" ? GetTran("006899", "世联会员") : sdr["levelint"].ToString() == "1" ? GetTran("004255", "世联普商") : sdr["levelint"].ToString() == "2" ? GetTran("004258", "世联咨商") : sdr["levelint"].ToString() == "3" ? GetTran("005219", "世联特别咨商") : sdr["levelint"].ToString() == "4" ? GetTran("005222", "世联高级咨商") : GetTran("005224", "世联全面咨商");
            //}
            //else if ((String)Session["languageCode"] == "L002")
            //{
            //    lblName.Text = Encryption.Encryption.GetDecipherName(sdr["L002"].ToString());
            //    lblPetName.Text = sdr["L002"].ToString();
            //    lblLevel.Text = CommonDataBLL.GetLevelStr(sdr["levelint"].ToString(),"L002");
            //}
            ////lblRegisterDate.Text = GetBiaoZhunTime(sdr["registerdate"].ToString());
            ////lblActiveDate.Text = GetBiaoZhunTime(sdr["ActiveDate"].ToString());
            // lblLastLoginDate.Text = GetBiaoZhunTime(Session["UserLastLoginDate"].ToString()).Split(' ')[0];

            //this.kk.Text = Convert.ToString(sdr["DefaultNumber"]);

           
                decimal djye = Convert.ToDecimal(sdr["djye"]);
                decimal jldj = Convert.ToDecimal(sdr["jldj"]);
                decimal xjye = Convert.ToDecimal(sdr["xjye"]);
                mobil.Text = Convert.ToString(sdr["MobileTele"]);
                Jackpot.Text = xjye.ToString("0.0000");
                fuxiaoin.Text = djye.ToString("0.0000");
                decimal cudayprice = Common.GetnowPrice();
                lblPay.Text = cudayprice.ToString("0.0000");
                //  lblPay.Text =  Math.Round((Convert.ToDouble(sdr["leftMoney"]) * blv)).ToString("#0.00");
                decimal ze = jldj / cudayprice;
                pointAIn.Text = jldj.ToString() + "(" + ze.ToString("0.0000") + "FTC)";

                lblBonse.Text = (ze + xjye + djye).ToString("0.0000");

                //lblBonse.Text = Math.Round((double.Parse(CommonDataBLL.GetLeftMoney(Session["Member"].ToString())) * AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString())))).ToString("#0.00");
                //lblBonse.Text = Math.Round((double.Parse(CommonDataBLL.GetLeftMoney1(Session["Member"].ToString())) * AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString())))).ToString("#0.00");
                //  lblBonse.Text = Math.Round(Convert.ToDouble(sdr["xjye"]) * blv).ToString("#0.00");

                //lblFx.Text =   Math.Round(((Convert.ToDouble(sdr["fuxiaoin"]) - Convert.ToDouble(sdr["fuxiaoout"])) * blv)).ToString("#0.00");

                //lblFxth.Text =  Math.Round(((Convert.ToDouble(sdr["fuxiaothin"]) - Convert.ToDouble(sdr["fuxiaothout"])) * blv)).ToString("#0.00");

                //lblzsjf.Text = Math.Round(((Convert.ToDouble(sdr["pointAin"]) - Convert.ToDouble(sdr["pointAout"])) * blv)).ToString("#0.00");

                //lblsfjf.Text = Math.Round(((Convert.ToDouble(sdr["pointBin"]) - Convert.ToDouble(sdr["pointBout"])) * blv)).ToString("#0.00");

            
        }
        sdr.Close();
        sdr.Dispose();

        string sql = "select isnull(fxlj,0) as ffxlj, * from MemberInfoBalance" + CommonDataBLL.getMaxqishu() + " where number='" + Session["Member"].ToString() + "'";
        DataTable dt = DBHelper.ExecuteDataTable(sql);
        if (dt.Rows.Count > 0)
        {
            labCurrentOneMark.Text = (Convert.ToDecimal(dt.Rows[0]["DTotalNetRecord"]) - Convert.ToDecimal(dt.Rows[0]["totaloneMark"])).ToString("f4");
            //Label1.Text = Convert.ToDecimal(dt.Rows[0]["totaloneMark"]).ToString();
            IRate.Text = (Convert.ToDecimal(dt.Rows[0]["ARate"]) * 100).ToString("f4") + "%";
            int lv = Convert.ToInt16(dt.Rows[0]["Level"].ToString());
            if (lv == 1)
            { Label1.Text = "500"; }
            if (lv == 2)
            { Label1.Text = "1000"; }
            if (lv == 3)
            { Label1.Text = "3000"; }
            if (lv == 4)
            { Label1.Text = "5000"; }
            if (lv == 0)
            {

                Label1.Text = "无";
            }
        }
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
    protected void jrsc_Click(object sender, EventArgs e)
    {
        string postdz = "xxx.com/AppShop/AppShopHandler.ashx?action=login";
        Dictionary<String, String> myDi = new Dictionary<String, String>();
        myDi.Add("userName", "15250944562");
        myDi.Add("Password", "aichun.123");
        string rspp = PublicClass.GetFunction(postdz, myDi);
        JObject stJson = JObject.Parse(rspp);

    }
}
