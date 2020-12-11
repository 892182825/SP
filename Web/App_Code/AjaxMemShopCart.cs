using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Collections;
using BLL.CommonClass;
using System.Text;
using BLL.other.Member;
using DAL;

/// <summary>
///AjaxMemShopCart 的摘要说明
/// </summary>
public class AjaxMemShopCart : BLL.TranslationBase
{
    protected LetUsOrder luo = new LetUsOrder();
    public AjaxMemShopCart()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    [AjaxPro.AjaxMethod]
    public string GetShopCartStr(string proid, string proNum, string oType)
    {
        //if (Session["OrderType"] != null)
        //{
        //    oType = Session["OrderType"].ToString();
        //}

        oType = Session["LUOrder"].ToString().Split(',')[1];

        string number = "";
        //if (Session["mbreginfo"] == null)
        //{
        //    number = Session["LUOrder"].ToString().Split(',')[0];//((Model.MemberInfoModel)Session["fxMemberModel"]).Number;
        //}
        //else
        //{
        //    number = ((Model.MemberInfoModel)Session["mbreginfo"]).Number;
        //}

        number = Session["LUOrder"].ToString().Split(',')[0];

        string str = "";
        string sql = "UpdMemShopCart";
        SqlParameter[] sp = new SqlParameter[] {
            new SqlParameter("@proid",SqlDbType.Int),
            new SqlParameter("@proNum",SqlDbType.Int),
            new SqlParameter("@memBh",number),
            new SqlParameter("@mType",SqlDbType.Int),
            new SqlParameter("@odType",SqlDbType.Int)
        };

        sp[0].Value = Convert.ToInt32(proid);
        sp[1].Value = Convert.ToInt32(proNum);
        int mType1 = 0;
        if (HttpContext.Current.Session["Company"] != null)
        {
            mType1 = 1;
        }
        else if (HttpContext.Current.Session["Store"] != null)
        {
            mType1 = 2;
        }
        else if (HttpContext.Current.Session["Member"] != null)
        {
            mType1 = 3;
        }
        else
        {
            return "添加失败！";
        }
        sp[3].Value = mType1;

        sp[4].Value = Convert.ToInt32(oType);

        DataTable dt = DAL.DBHelper.ExecuteDataTable(sql, sp, CommandType.StoredProcedure);

        if (dt.Rows[0]["TotalPrice"].ToString() != "0")
        {
            str = " <li class='plicImg-1'>购物车中共" + dt.Rows[0]["tNum"].ToString() + "种产品</li>";
            str += "<li>总金额：" + Convert.ToDouble(dt.Rows[0]["TotalPrice"].ToString()).ToString("0.00") + " 总PV:" + Convert.ToDouble(dt.Rows[0]["TotalPv"].ToString()).ToString("0.00") + "</li>";
            //str = "购物车共 <span>" + dt.Rows[0]["tNum"].ToString() + "</span> 种产品<br/>";
            //str += "总金额：<span>" + Convert.ToDouble(dt.Rows[0]["TotalPrice"].ToString()).ToString("0.00") + "</span>";
            //str += "总PV：<span>" + Convert.ToDouble(dt.Rows[0]["TotalPv"].ToString()).ToString("0.00") + "</span>";
        }
        else
        {
            return "添加失败！";
        }

        return str;
    }

    [AjaxPro.AjaxMethod]
    public string GetShopCartStrFx(string proid, string proNum, string oType)
    {
        //if (Session["OrderType"] != null)
        //{
        //    oType = Session["OrderType"].ToString();
        //}
        oType = Session["LUOrder"].ToString().Split(',')[1];

        string str = "";
        string sql = "UpdMemShopCart";
        SqlParameter[] sp = new SqlParameter[] {
            new SqlParameter("@proid",SqlDbType.Int),
            new SqlParameter("@proNum",SqlDbType.Int),
            new SqlParameter("@memBh",((Model.OrderFinalModel)Session["fxMemberModel"]).Number),
            new SqlParameter("@mType",SqlDbType.Int),
            new SqlParameter("@odType",SqlDbType.Int)
        };

        sp[0].Value = Convert.ToInt32(proid);
        sp[1].Value = Convert.ToInt32(proNum);
        int mType1 = 0;
        if (HttpContext.Current.Session["Company"] != null)
        {
            mType1 = 1;
        }
        else if (HttpContext.Current.Session["Store"] != null)
        {
            mType1 = 2;
        }
        else if (HttpContext.Current.Session["Member"] != null)
        {
            mType1 = 3;
        }
        else
        {
            return "添加失败！";
        }
        sp[3].Value = mType1;

        sp[4].Value = Convert.ToInt32(oType);

        DataTable dt = DAL.DBHelper.ExecuteDataTable(sql, sp, CommandType.StoredProcedure);

        if (dt.Rows[0]["TotalPrice"].ToString() != "0")
        {
            str = "购物车共 <span>" + dt.Rows[0]["tNum"].ToString() + "</span> 种产品<br/>";
            str += "总金额：<span>" + Convert.ToDouble(dt.Rows[0]["TotalPrice"].ToString()).ToString("0.00") + "</span>";
            str += "总PV：<span>" + Convert.ToDouble(dt.Rows[0]["TotalPv"].ToString()).ToString("0.00") + "</span>";
        }
        else
        {
            return "添加失败！";
        }

        return str;
    }

    [AjaxPro.AjaxMethod]
    public string DelOne(int proid, string oType)
    {
        oType = Session["LUOrder"].ToString().Split(',')[1];

        int mType1 = 0;
        if (HttpContext.Current.Session["Company"] != null)
        {
            mType1 = 1;
        }
        else if (HttpContext.Current.Session["Store"] != null)
        {
            mType1 = 2;
        }
        else if (HttpContext.Current.Session["Member"] != null)
        {
            mType1 = 3;
        }
        string number = "";
        if (Session["mbreginfo"] == null)
        {
            //number = ((Model.MemberInfoModel)Session["fxMemberModel"]).Number;
            number = Session["LUOrder"].ToString().Split(',')[0];
        }
        else
        {
            number = ((Model.MemberInfoModel)Session["mbreginfo"]).Number;
        }
        string sql = "delete from MemShopCart where memBh=@memBh and proId=@proid and mType=@mType and odType=@odType";
        SqlParameter[] sp = new SqlParameter[] {
            new SqlParameter("@memBh",number),
            new SqlParameter("@proid",SqlDbType.Int),
            new SqlParameter("@mType",SqlDbType.Int),
            new SqlParameter("@odType",SqlDbType.Int)
        };
        sp[1].Value = proid;
        sp[2].Value = mType1;
        sp[3].Value = Convert.ToInt32(oType); ;
        int k = DAL.DBHelper.ExecuteNonQuery(sql, sp, CommandType.Text);
        if (k > 0)
        {
            sql = "select isnull(sum(PreferentialPrice*proNum),0.00) as TotalPriceAll,isnull(sum(PreferentialPV*proNum),0.00) as TotalPvAll,isnull(sum(proNum),0.00) as totalNum from MemShopCart,Product where MemShopCart.proId=Product.productId and memBh='" + number + "' and mType=" + mType1 + " and odType=" + oType;

            DataTable dt2 = DAL.DBHelper.ExecuteDataTable(sql);

            return dt2.Rows[0][0].ToString() + "|" + dt2.Rows[0][1].ToString() + "|" + dt2.Rows[0][2].ToString();
        }
        else
        {
            return "-1";
        }
    }

    [AjaxPro.AjaxMethod]
    public string DelOneFx(int proid, string oType)
    {
        oType = Session["LUOrder"].ToString().Split(',')[1];

        int mType1 = 0;
        if (HttpContext.Current.Session["Company"] != null)
        {
            mType1 = 1;
        }
        else if (HttpContext.Current.Session["Store"] != null)
        {
            mType1 = 2;
        }
        else if (HttpContext.Current.Session["Member"] != null)
        {
            mType1 = 3;
        }
        string sql = "delete from MemShopCart where memBh=@memBh and proId=@proid and mType=@mType and odType=@odType";
        SqlParameter[] sp = new SqlParameter[] {
            new SqlParameter("@memBh",((Model.OrderFinalModel)Session["fxMemberModel"]).Number),
            new SqlParameter("@proid",SqlDbType.Int),
            new SqlParameter("@mType",SqlDbType.Int),
            new SqlParameter("@odType",SqlDbType.Int)
        };
        sp[1].Value = proid;
        sp[2].Value = mType1;
        sp[3].Value = Convert.ToInt32(oType); ;
        int k = DAL.DBHelper.ExecuteNonQuery(sql, sp, CommandType.Text);
        if (k > 0)
        {
            sql = "select isnull(sum(PreferentialPrice*proNum),0.00) as TotalPriceAll,isnull(sum(PreferentialPV*proNum),0.00) as TotalPvAll,isnull(sum(proNum),0.00) as totalNum from MemShopCart,Product where MemShopCart.proId=Product.productId and memBh='" + ((Model.OrderFinalModel)Session["fxMemberModel"]).Number + "' and mType=" + mType1 + " and odType=" + oType;

            DataTable dt2 = DAL.DBHelper.ExecuteDataTable(sql);

            return dt2.Rows[0][0].ToString() + "|" + dt2.Rows[0][1].ToString() + "|" + dt2.Rows[0][2].ToString();
        }
        else
        {
            return "-1";
        }
    }

    [AjaxPro.AjaxMethod]
    public string UpdShopCart(int proid, int proNum, string oType)
    {
        oType = Session["LUOrder"].ToString().Split(',')[1];

        if (proNum <= 0)
        {
            string str1 = DelOne(proid, oType);
            if (str1 == "-1")
            {
                return "0";
            }
            else
            {
                return "1";
            }
        }
        string number = "";
        if (Session["mbreginfo"] == null)
        {
            number = ((Model.MemberInfoModel)Session["fxMemberModel"]).Number;
        }
        else
        {
            number = ((Model.MemberInfoModel)Session["mbreginfo"]).Number;
        }
        string sql = "UpdMemShopCart2";
        SqlParameter[] sp = new SqlParameter[] {
            new SqlParameter("@proid",SqlDbType.Int),
            new SqlParameter("@proNum",SqlDbType.Int),
            new SqlParameter("@memBh",number),
            new SqlParameter("@mType",SqlDbType.Int),
            new SqlParameter("@odType",SqlDbType.Int)
        };

        sp[0].Value = proid;
        sp[1].Value = proNum;
        int mType1 = 0;
        if (HttpContext.Current.Session["Company"] != null)
        {
            mType1 = 1;
        }
        else if (HttpContext.Current.Session["Store"] != null)
        {
            mType1 = 2;
        }
        else if (HttpContext.Current.Session["Member"] != null)
        {
            mType1 = 3;
        }
        else
        {
            return "添加失败！";
        }
        sp[3].Value = mType1;

        sp[4].Value = Convert.ToInt32(oType);

        DataTable dt = DAL.DBHelper.ExecuteDataTable(sql, sp, CommandType.StoredProcedure);

        if (dt.Rows[0]["TotalPrice"].ToString() == "0")
        {
            return "0";
        }
        CartCount(number, number, oType);
        return "1";
    }

    [AjaxPro.AjaxMethod]
    public string UpdShopCartFx(int proid, int proNum, string oType)
    {
        oType = Session["LUOrder"].ToString().Split(',')[1];

        if (proNum <= 0)
        {
            string str1 = DelOneFx(proid, oType);
            if (str1 == "-1")
            {
                return "0";
            }
            else
            {
                return "1";
            }
        }
        string sql = "UpdMemShopCart2";
        SqlParameter[] sp = new SqlParameter[] {
            new SqlParameter("@proid",SqlDbType.Int),
            new SqlParameter("@proNum",SqlDbType.Int),
            new SqlParameter("@memBh",((Model.OrderFinalModel)Session["fxMemberModel"]).Number),
            new SqlParameter("@mType",SqlDbType.Int),
            new SqlParameter("@odType",SqlDbType.Int)
        };

        sp[0].Value = proid;
        sp[1].Value = proNum;
        int mType1 = 0;
        if (HttpContext.Current.Session["Company"] != null)
        {
            mType1 = 1;
        }
        else if (HttpContext.Current.Session["Store"] != null)
        {
            mType1 = 2;
        }
        else if (HttpContext.Current.Session["Member"] != null)
        {
            mType1 = 3;
        }
        else
        {
            return "添加失败！";
        }
        sp[3].Value = mType1;

        sp[4].Value = Convert.ToInt32(oType);

        DataTable dt = DAL.DBHelper.ExecuteDataTable(sql, sp, CommandType.StoredProcedure);

        if (dt.Rows[0]["TotalPrice"].ToString() == "0")
        {
            return "0";
        }

        return "1";
    }

    [AjaxPro.AjaxMethod]
    public string IsStoreExist(string storeBh)
    {
        string str = "";
        string sql = "select count(1) from storeinfo where storeid=@memBh";
        SqlParameter[] sp = new SqlParameter[] {
            new SqlParameter("@memBh",storeBh)
        };

        str = DAL.DBHelper.ExecuteScalar(sql, sp, CommandType.Text).ToString();

        return str;
    }

    [AjaxPro.AjaxMethod]
    public string GetStoreMess(string storeBh)
    {
        string str = "";
        string sql = "select isnull(storename,''),isnull(storeLevelInt,0) from storeinfo where storeid=@memBh";
        SqlParameter[] sp = new SqlParameter[] {
            new SqlParameter("@memBh",storeBh)
        };

        DataTable dt = DAL.DBHelper.ExecuteDataTable(sql, sp, CommandType.Text);

        if (dt.Rows.Count > 0)
        {
            str = "店主昵称是：" + dt.Rows[0][0].ToString() + ";专卖店类型是：无级别";
        }
        else
        {
            str = "店主昵称是：;专卖店类型是：无级别";
        }

        return str;
    }

    [AjaxPro.AjaxMethod]
    public string GetLiuLanPro(string oType)
    {
        oType = Session["LUOrder"].ToString().Split(',')[1];

        int mType1 = 0;
        if (HttpContext.Current.Session["Company"] != null)
        {
            mType1 = 1;
        }
        else if (HttpContext.Current.Session["Store"] != null)
        {
            mType1 = 2;
        }
        else if (HttpContext.Current.Session["Member"] != null)
        {
            mType1 = 3;
        }
        string number = "";
        if (Session["mbreginfo"] == null)
        {
            number = ((Model.MemberInfoModel)Session["fxMemberModel"]).Number;
        }
        else
        {
            number = ((Model.MemberInfoModel)Session["mbreginfo"]).Number;
        }
        string sql = "select top 7 * from MemShopCart,product where MemShopCart.proid=product.productid and memBh=@memBh and mType=@mType and odType=@odType  order by id desc";
        SqlParameter[] sp = new SqlParameter[] {
            new SqlParameter("@memBh",number),
            new SqlParameter("@mType",SqlDbType.Int),
            new SqlParameter("@odType",SqlDbType.Int)
        };
        sp[1].Value = mType1;
        sp[2].Value = Convert.ToInt32(oType);
        DataTable dt = DAL.DBHelper.ExecuteDataTable(sql, sp, CommandType.Text);
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        if (dt.Rows.Count == 0)
        {
            sb.Append("");
        }
        int bzCurrency = CommonDataBLL.GetStandard();
        double currency = AjaxClass.GetCurrency(int.Parse(Session["Default_Currency"] == null ? bzCurrency.ToString() : Session["Default_Currency"].ToString()));
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            sb.Append(@" <div class='prcClassImg'><h6 class='pImages' style='margin-right: 30px;'><img src='" + FormatURL(dt.Rows[i]["ProductID"]) + "' width='64' height='54' /></h6> <ul>	<li><a href='#'>" + dt.Rows[i]["productname"].ToString() + "</a></li>"
                  + "<li><a href='#'>价格：" + (Convert.ToDouble(dt.Rows[i]["PreferentialPrice"]) * currency).ToString() + "</a></li> <li><a href='#'>PV：" + Convert.ToDouble(dt.Rows[i]["PreferentialPV"]).ToString() + "</a></li> </ul> <div style='clear:both'></div>  </div>");
            //            sb.Append(@"<dl>
            //            	<dd><img src='" + FormatURL(dt.Rows[i]["ProductID"]) + "' width='55' height='55' /></dd><dt>" + dt.Rows[i]["productname"].ToString() + "<br />金额：￥" + Convert.ToDouble(dt.Rows[i]["PreferentialPrice"]).ToString("0.00") + "<br />PV:" + Convert.ToDouble(dt.Rows[i]["PreferentialPV"]).ToString("0.00") + "</dt></dl>");
        }

        return sb.ToString();
    }

    [AjaxPro.AjaxMethod]
    public string GetLiuLanProFx(string oType)
    {
        oType = Session["LUOrder"].ToString().Split(',')[1];

        int mType1 = 0;
        if (HttpContext.Current.Session["Company"] != null)
        {
            mType1 = 1;
        }
        else if (HttpContext.Current.Session["Store"] != null)
        {
            mType1 = 2;
        }
        else if (HttpContext.Current.Session["Member"] != null)
        {
            mType1 = 3;
        }
        string sql = "select top 7 * from MemShopCart,product where MemShopCart.proid=product.productid and memBh=@memBh and mType=@mType and odType=@odType  order by id desc";
        SqlParameter[] sp = new SqlParameter[] {
            new SqlParameter("@memBh",((Model.OrderFinalModel)Session["fxMemberModel"]).Number),

             new SqlParameter("@mType",SqlDbType.Int),

             new SqlParameter("@odType",SqlDbType.Int)
        };
        sp[1].Value = mType1;
        sp[2].Value = Convert.ToInt32(oType);
        DataTable dt = DAL.DBHelper.ExecuteDataTable(sql, sp, CommandType.Text);
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        if (dt.Rows.Count == 0)
        {
            sb.Append("");
        }

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            sb.Append(@"<dl>
            	<dd><img src='" + FormatURL(dt.Rows[i]["ProductID"]) + "' width='55' height='55' /></dd><dt>" + dt.Rows[i]["productname"].ToString() + "<br />金额：￥" + Convert.ToDouble(dt.Rows[i]["PreferentialPrice"]).ToString("0.00") + "<br />PV:" + Convert.ToDouble(dt.Rows[i]["PreferentialPV"]).ToString("0.00") + "</dt></dl>");
        }

        return sb.ToString();
    }

    protected string FormatURL(object strArgument)
    {
        string result = "../ReadImage.aspx?ProductID=" + strArgument.ToString();
        if (result == "" || result == null)
        {
            result = "";
        }
        return result;
    }

    [AjaxPro.AjaxMethod]
    public string GetStLeft()
    {
        string storeid = DAL.DBHelper.ExecuteScalar("select storeid from memberinfo where number='" + HttpContext.Current.Session["Member"].ToString() + "'").ToString();
        return new BLL.Registration_declarations.RegistermemberBLL().GetLeftRegisterMemberMoney(storeid);
    }

    [AjaxPro.AjaxMethod]
    public string GetMemberDeclarations()
    {
        return BLL.MoneyFlows.ReleaseBLL.GetMemberDeclarations(Session["Member"].ToString()).ToString("F2");
    }

    [AjaxPro.AjaxMethod]
    public string DataBindTxt(string strPid, string storeID, string productids, string cla, string clr, int curr)
    {

        double currency = 1;
        string selectcu = DAL.CurrencyDAL.GetJieCheng(DAL.StoreInfoDAL.GetStoreCurrency(storeID));
        if (curr > 0)
        {
            selectcu = DAL.CurrencyDAL.GetJieCheng(curr);
            currency = GetCurrency(DAL.StoreInfoDAL.GetStoreCurrency(storeID), curr);
        }

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        try
        {
            double price = 0.00;
            double pv = 0.00;
            double zongJine = 0.00;
            double zongPv = 0.00;
            string pName = "";

            string[] strId = Regex.Split(strPid, ",", RegexOptions.IgnoreCase);
            string[] strProductIDs = Regex.Split(productids, ",", RegexOptions.IgnoreCase);

            SqlDataReader dr = DAL.ProductDAL.GetProductInfo();

            System.Collections.Hashtable ht = new Hashtable();
            ListItem li = new ListItem();
            for (int i = 0; i < strId.Length - 1; i++)
            {
                string num = strId[i].ToString().Trim();
                string id = strProductIDs[i].ToString().ToLower();
                if (id.Length > 1)
                {//正常
                    id = id.Substring(1);
                    ht.Add(id, num);
                }
            }


            sb.Append("<table  border=0 align=center style='width:100%' cellpadding=0 cellspacing=1 class=" + cla + " style='width:100%;'>");
            sb.Append("<tr>");
            sb.Append("<th align=center  bgcolor=" + clr + " ><b>&nbsp;&nbsp;" + BLL.Translation.Translate("000501", "产品名称") + "&nbsp;&nbsp;</b></th>");
            sb.Append("<th align=center bgcolor=" + clr + " ><b>&nbsp;&nbsp;" + BLL.Translation.Translate("000503", "单价") + "&nbsp;&nbsp;</b></th>");
            sb.Append("<th align=center bgcolor=" + clr + " ><b>&nbsp;&nbsp;" + BLL.Translation.Translate("000505", "数量") + "&nbsp;&nbsp;</b></th>");
            sb.Append("<th align=center bgcolor=" + clr + " ><b>&nbsp;&nbsp;" + BLL.Translation.Translate("000322", "金额") + "&nbsp;&nbsp;</b></th>");
            sb.Append("<th align=center bgcolor=" + clr + " ><b>&nbsp;&nbsp;" + BLL.Translation.Translate("000414", "积分") + "&nbsp;&nbsp;</b></th>");
            sb.Append("<th align=center bgcolor=" + clr + " ><b>&nbsp;&nbsp;" + BLL.Translation.Translate("000045", "期数") + "&nbsp;&nbsp;</b></th>");
            sb.Append("<th align=center bgcolor=" + clr + " ><b>&nbsp;&nbsp;&nbsp;" + BLL.Translation.Translate("000510", "订购日期") + "&nbsp;&nbsp;&nbsp;</b></th>");
            sb.Append("</tr>");
            int iCurQishu = 1;// new CommonDataBLL().getMaxqishu();
            string dateNow = DateTime.Now.ToString("yyyy-MM-dd");

            int n = 0;

            while (dr.Read())
            {
                string productid = dr["productID"].ToString().Trim();
                string productId_ = "N" + productid;
                if (ht.Contains(productid))
                {
                    int iCount = int.Parse(ht[productid].ToString());  //产品数量
                    pName = dr["productName"].ToString();                //产品名称
                    price = Convert.ToDouble(dr["PreferentialPrice"]) * iCount;
                    pv = Convert.ToDouble(dr["PreferentialPV"]) * iCount;
                    zongJine += price;
                    zongPv += pv;

                    if (n % 2 == 0)
                    {
                        sb.Append("<tr bgColor=\"#F1F4F8\" onmouseover=\"bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC';\" onmouseout=\"this.style.backgroundColor=bg;\" >");
                    }
                    else
                    {
                        sb.Append("<tr onmouseover=\"bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC';\" onmouseout=\"this.style.backgroundColor=bg;\" >");
                    }
                    sb.Append("<td align=\"center\">" + pName.ToString() + "</td>");
                    sb.Append("<td align=\"center\">" + (Convert.ToDouble(dr["PreferentialPrice"]) / currency).ToString("0.00") + "</td>");
                    sb.Append("<td align=\"center\">" + iCount + "</td>");
                    sb.Append("<td align=\"center\">" + (price / currency).ToString("0.00") + "</td>");
                    sb.Append("<td align=\"center\">" + pv.ToString() + "</td>");
                    sb.Append("<td align=\"center\">" + iCurQishu.ToString() + "</td>");
                    sb.Append("<td align=\"center\">" + dateNow + "</td>");
                    sb.Append("</tr>");
                    n++;
                }
            }
            dr.Close();
            sb.Append("<tr bgColor=\"#F1F4F8\">");
            sb.Append("<td colspan=\"3\" align=\"center\" class=\"biaozzi\" style=\"color:red;\" ><b>" + BLL.Translation.Translate("000247", "总计") + "(" + selectcu + ")：</b></td>");
            sb.Append("<td align=\"center\" class=\"xhzi\" style=\"color:red;\">" + (zongJine / currency).ToString("0.00") + "</td>");
            sb.Append("<td align=\"center\" class=\"xhzi\" style=\"color:red;\">" + zongPv.ToString("0.00") + "</td>");
            sb.Append("<td colspan=\"2\" align=\"center\">&nbsp;</td>");
            sb.Append("</tr>");
            sb.Append("</table>");
        }
        catch (Exception ex)
        {
            sb.Append(ex.Message.ToString());
        }
        return sb.ToString();
    }

    /// <summary>
    /// 获取及时汇率
    /// </summary>
    /// <param name="from"> 要转换的币种 </param>
    /// <param name="to"> 转换成的币种 </param>
    /// <returns> 汇率 </returns>
    public static double GetCurrency(int from, int to)
    {
        if (System.Web.HttpContext.Current.Application["CurrencySetting"] == null)
        {
            return BLL.other.Company.SetRateBLL.GetCurrencyBySql(from, to);
        }
        else if (System.Web.HttpContext.Current.Application["CurrencySetting"].ToString() == "Currency")
        {
            string fromcu = DAL.CurrencyDAL.GetJieCheng(from);
            string tocu = DAL.CurrencyDAL.GetJieCheng(to);
            net.webservicex.www.CurrencyConvertor CC = new net.webservicex.www.CurrencyConvertor();
            return CC.ConversionRate((net.webservicex.www.Currency)Enum.Parse(typeof(net.webservicex.www.Currency), fromcu), (net.webservicex.www.Currency)Enum.Parse(typeof(net.webservicex.www.Currency), tocu));
        }
        else
        {
            return 1;
        }
    }

    /// <summary>
    /// 获取产品信息
    /// </summary>
    /// <param name="productid">产品ID</param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string GetProductDetail(string productid)
    {
        DataTable dt = BLL.CommonClass.CommonDataBLL.GetProductById(productid);

        string html = " <table width=\"300\" border=\"0\" cellpadding=\"0\" cellspacing=\"4\" class=\"bjkk\">";

        html += "<tr><td width=\"105\"><img src=\"../ReadImage.aspx?ProductID=" + dt.Rows[0]["ProductID"].ToString() + "\" style=\"width:105px;height:145px;\" /> </td>";
        html += "<td valign=\"top\"><table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" bgcolor=\"#FFFFFF\" class=\"biaozzi\">";
        html += "<tr><td width=\"40\" align=\"right\" valign=\"top\" nowrap>" + BLL.Translation.TranslateFromDB("004193") + "：</td><td><span class=\"bzbt\"> " + dt.Rows[0]["ProductName"].ToString() + " </span></td></tr>";//产品名称
        html += "<tr><td width=\"40\" align=\"right\" valign=\"top\" nowrap>" + BLL.Translation.TranslateFromDB("002084") + "：</td> <td>" + dt.Rows[0]["PreferentialPrice"].ToString() + " </td> </tr>";//优惠价格
        html += "<tr><td width=\"40\" align=\"right\" valign=\"top\" nowrap>" + BLL.Translation.TranslateFromDB("000414") + "：</td> <td>" + dt.Rows[0]["PreferentialPV"].ToString() + " </td> </tr>";//优惠价格
        if (dt.Rows[0]["Description"].ToString().Length > 30)
            html += "<tr><td width=\"40\" align=\"right\" valign=\"top\">" + BLL.Translation.TranslateFromDB("000628") + "：</td> <td class=\"smbiaozzi\" title='" + dt.Rows[0]["Description"].ToString() + "'> <textarea class=\"biaozzi\" style=\"width:100%;height:100%;border:0;overflow: hidden;\">" + dt.Rows[0]["Description"].ToString().Substring(0, 30) + "... </textarea></td></tr>";
        else
            html += "<tr><td width=\"40\" align=\"right\" valign=\"top\">" + BLL.Translation.TranslateFromDB("000628") + "：</td> <td class=\"smbiaozzi\">" + dt.Rows[0]["Description"].ToString() + " </td></tr>";
        html += "</table></td></tr></table>";
        return html;
    }

    [AjaxPro.AjaxMethod]
    public string GetZpStr(double pv)
    {
        string str = @"<tr>
            <th> 产品图片</th>
        <th> 产品编号</th>
        <th> 产品名称</th>
        <th> 单价</th>
        <th> PV</th>
        <th> 赠送数量</th>
        <th> 总金额</th>
        <th> 总PV</th>
        </tr>
        ";
        return str + BLL.other.Company.GiveProductBLL.GetTableZp(pv);
    }

    [AjaxPro.AjaxMethod]
    public double GetCarryMoney(double totalMoney)
    {
        return 0;
    }

    [AjaxPro.AjaxMethod]
    public string ShoppingListXF(string productid,int pgidx, int pagesize,string txtProName,int OrderType,string ddlSort,string type)
    {
        string curstr = "";
        string txt1 = "";
        string txt2 = "";
        string conditons = "";
        var count = 0;
        int pageindex = 0;
        if (!int.TryParse(pgidx.ToString(), out pageindex))
        {
            return curstr;
        }

        string sqls = @"WITH  pc as(
    select productcode, ProductID, ProductName, CommonPrice, CommonPV, PreferentialPrice, PreferentialPV,[Description], ProductImage,
    currency.name as currency,ROW_NUMBER() OVER(ORDER BY ";

       

        if (ddlSort != "-1" && ddlSort != "-2")
        {
            sqls += ""+ ddlSort + "";
        }
        else
        {
            sqls+= " productid asc ";
        }
        sqls += @") as rowNum FROM
    Product, currency where Product.CountryCode = 86 and Product.currency = currency.id and Product.isfold = 0  ";

        if (productid != "")
        {
            string pid = productid + ",";
            string pidstr = productid + ",";
            while (1 == 1)
            {
                DataTable dt = DAL.DBHelper.ExecuteDataTable("select productid from product where pid in (" + pidstr.Substring(0, pidstr.Length - 1).ToString() + ") and issell=0");
                pidstr = "";
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        pidstr = pidstr + dt.Rows[i][0].ToString() + ",";
                        pid = pid + dt.Rows[i][0].ToString() + ",";
                    }
                }
                else
                {
                    break;
                }
            }

            sqls += " and Product.pid in (" + pid.Substring(0, pid.Length - 1).ToString() + ")";
        }

        if (OrderType == 31 || OrderType == 11 || OrderType == 21)
        {
            sqls += " and (Product.Yongtu=0 or Product.Yongtu=1)";
        }
        else
        {
            sqls += " and (Product.Yongtu=0 or Product.Yongtu=2)";
        }


        if (!string.IsNullOrEmpty(txtProName))
        {
            if(txtProName!= "请输入产品关键词")
            {
                sqls += " and ProductName like'%" + txtProName + "%'";
            }
        }

        if (txt1 != "" && txt2!= "")
        {
            int k1 = 0;
            int.TryParse(txt1, out k1);
            int k2 = 0;
            int.TryParse(txt2, out k2);

            sqls+= " and " + conditons + ">=" + k1 + " and " + conditons + "<=" + k2;
        }

        sqls += @" and Product.issell = 0) SELECT * from pc WHERE rowNum BETWEEN " +(pagesize * (pageindex - 1) + 1)+ " AND " + pageindex * pagesize + "";

        try
        {
            DataTable dtt = DAL.DBHelper.ExecuteDataTable(sqls, CommandType.Text);
            foreach (DataRow item in dtt.Rows)
            {
                curstr += @"<li><a href='ShowProductInfo.aspx?oT=3&rt=3&ty=1&ID=" + item["ProductID"] + "&type=" + type + "'>";
                curstr += "<img  with='200px' height='200px' src='" + FormatURL(item["ProductID"]) + "'> </a>";
                curstr += "<p>"+ item["ProductName"] + "</p>";
                curstr += "<p class='jiag'>$ " + item["PreferentialPrice"] + "</p>";
                /*curstr += "<p class='jiag'>PV " + item["PreferentialPV"] + "</p>"*/;
                curstr += "</li>";
            }
        }
        catch (Exception ex)
        {
            return curstr;
        }
        if (!string.IsNullOrEmpty(curstr))
        {
        }
        return curstr;
    }

    [AjaxPro.AjaxMethod]
    public int CartCount(string member,string mtype,string odtype)
    {
        string sql = @"select isnull(sum(PreferentialPrice*proNum),0) as TotalPriceAll,isnull(sum(PreferentialPV*proNum),0) 
as TotalPvAll,isnull(sum(proNum),0) as totalNum from MemShopCart,Product where MemShopCart.proId=Product.productId  
and mType=" + mtype + " and odType=" + odtype + " and memBh='" + member + "'";
        DataTable dt2 = DAL.DBHelper.ExecuteDataTable(sql);

        if (dt2.Rows.Count > 0)
        {
            var num = Convert.ToInt32(dt2.Rows[0]["totalNum"]);
            Session["CartCount"] = num;
            return num;
        }
        return 0;

    }
    /// <summary>
    /// 确认订单
    /// </summary>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string AddLsOrderDetail()
    {
        luo.SetVlaue();
        var curstr =new StringBuilder("");
        var usertype=0;
        if (Session["UserType"] != null)
        {
            usertype = Convert.ToInt32(Session["UserType"].ToString()); 
        }
        else
        {
            usertype = 3;
        }
       
        string sql = @"select productcode,ProductID,ProductName,PreferentialPrice,PreferentialPV,ProductImage,
PreferentialPrice*proNum as totalPrice,PreferentialPV*proNum as totalPv,proNum,pc.ProductColorName
from MemShopCart m  
left join Product p on m.proId=p.productId 
left join ProductColor pc on p.ProductColorID=pc.ProductColorID where memBh=@memBh
 and mType=@mType and odType=@odType";
        SqlParameter[] para =
                {
            new SqlParameter("@memBh",SqlDbType.VarChar),
            new SqlParameter("@mType",SqlDbType.Int),
            new SqlParameter("@odType",SqlDbType.Int),
        };
        para[0].Value = luo.MemBh;
        para[1].Value = usertype;
        para[2].Value = luo.OrderType;
        try
        {
            var dt = DAL.DBHelper.ExecuteDataTable(sql, para, CommandType.Text);
            foreach (DataRow item in dt.Rows)
            {
                //curstr.Append(@"<article class='confirmOrder'>");
                //curstr.Append(@"<img src='" + FormatURL(item["ProductID"]) + "' class='confirmOrderimg'/>");
                //curstr.Append(@"<div class='producttext'>");
                //curstr.Append(@"<span class='producttextspan'>" + item["ProductName"] + "</span>");
                //curstr.Append(@"<span class='producthue'>颜色：" + item["ProductColorName"] + "</span>");
                //curstr.Append(@"<span class='productprice'>&yen;" + item["PreferentialPrice"] + " <em class='productem'>X" + item["proNum"] + "</em></span>");
                //curstr.Append(@"</div></article>");

                curstr.Append(@"<li>");
                curstr.Append(@"<img src='" + FormatURL(item["ProductID"]) + "' class='shop-pic'/>");
                curstr.Append(@"<div class='order-mid'>");
                curstr.Append(@"<div class='tit'>"+ item["ProductName"] + "</div>");
                curstr.Append(@"<div class='order-price'>$"+ item["PreferentialPrice"] + " <i>X"+ item["proNum"]+"</i></div>");
                curstr.Append(@"</div></li>");
            }
        }
        catch (Exception ex)
        {
            return curstr.ToString();
        }
        return curstr.ToString();
    }
    /// <summary>
    /// 会员收货地址
    /// </summary>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string AddLsOrderAddress()
    {
        luo.SetVlaue();
        var curstr = new StringBuilder("");
        try
        {
            var cinfo = MemberInfoModifyBll.getconsigneeInfo(luo.MemBh, true);
            if (cinfo == null)
            {
                curstr.Append(@"<a href='PhoneSettings/SetConAddress.aspx?type=AddLsOrder' style='display: block;text-decoration: none;'>+新建收货地址");
                curstr.Append(@"<i class='glyphicon glyphicon-chevron-right' style='padding-right: 1rem'></i>");
                curstr.Append(@"</a>");
            }
            else
            {
                curstr.Append(@"<a href='PhoneSettings/SetConAddress.aspx?type=AddLsOrder&&url=AddLsOrder'>");
                curstr.Append(@"<p>收货人：");
                curstr.Append(@""+ cinfo.Consignee + " &nbsp; &nbsp;");
                curstr.Append(@"" + cinfo.MoblieTele + " </p>");
                var city = CommonDataDAL.GetCPCCode(cinfo.CPCCode);
                if (city != null)
                {
                    var strvalue= city.Province + city.City + city.Xian + cinfo.Address;
                    curstr.Append(@"<span>"+ strvalue + "</span>");
                }
                
                curstr.Append(@"</a>");
            }
        }
        catch (Exception)
        {
            return curstr.ToString();
        }
        return curstr.ToString();
    }
}