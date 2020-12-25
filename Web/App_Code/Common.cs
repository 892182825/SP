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
using DAL;
using System.Data.SqlClient;
using BLL.other.Company;

/// <summary>
///Common 的摘要说明
/// </summary>
public class Common
{
    public static string GetTran(string keyCode,string defaultText)
    {
        return new BLL.TranslationBase().GetTran(keyCode, defaultText);
    }
    public Common()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    public static string GetOrderType(int type)
    {
        string orderType = "";
        switch (type)
        {
            case 31:
                orderType = new BLL.TranslationBase().GetTran("10011", "公司注册"); 
                break;
            case 21:
                orderType =   "会员注册" ;
                break;
            case 22:
                orderType =  "抢购体验矿机" ;
                break;
            case 23:
                orderType =  "购买矿机" ;
                break;
            case 24:
                orderType = "矿机升级";
                break;

            case 25:
                orderType = "会员复投";
                break;

        }
        return orderType;
    }
    /// <summary>
    /// 获取会员报单类型
    /// </summary>
    /// <param name="type">类型ID string</param>
    /// <returns>类型名称</returns>
    public static string GetMemberOrderType(string type)
    {
        string orderType = "";
        switch (type)
        {
            case "31":
                orderType = new BLL.TranslationBase().GetTran("001458", "零购注册");
                break;
            case "11":
                orderType = new BLL.TranslationBase().GetTran("000555", "服务机构注册");
                break;
            case "21":
                orderType = new BLL.TranslationBase().GetTran("007530", "会员注册");
                break;

            case "12":
                orderType = new BLL.TranslationBase().GetTran("001445", "服务机构复消");
                break;
            case "22":
                orderType = new BLL.TranslationBase().GetTran("001448", "会员复消");
                break;
            case "24":
                orderType = new BLL.TranslationBase().GetTran("009123", "自动复消报单");
                break;
            case "25":
                orderType = new BLL.TranslationBase().GetTran("008122", "会员复消提货");
                break;

            case "13":
                orderType = new BLL.TranslationBase().GetTran("008153", "服务机构升级单");
                break;

            case "23":
                orderType = new BLL.TranslationBase().GetTran("008154", "会员升级单");
                break;

            case "33":
                orderType = new BLL.TranslationBase().GetTran("008155", "公司升级单");
                break;

        }
        return orderType;
    }
    /// <summary>
    /// 获取会员报单类型
    /// </summary>
    /// <param name="type">类型ID INT</param>
    /// <returns>类型名称</returns>
    public static string GetMemberOrderType(int type)
    {
        string orderType = "";
        switch (type)
        {
            case 11:
                orderType = new BLL.TranslationBase().GetTran("000555", "服务机构注册");
                break;
            case 12:
                orderType = new BLL.TranslationBase().GetTran("001445", "服务机构复消");
                break;
            case 22:
                orderType = new BLL.TranslationBase().GetTran("001448", "会员复消");
                break;
            case 21:
                orderType = new BLL.TranslationBase().GetTran("007530", "会员注册");
                break;
            case 31:
                orderType = new BLL.TranslationBase().GetTran("001458", "零购注册");
                break;
            case 25:
                orderType = new BLL.TranslationBase().GetTran("008122", "会员复消提货");
                break;
        }
        return orderType;
    }
    /// <summary>
    /// 翻译grid字段中的数字
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static string GetDeftrayState(string id)
    {
        string deftrayState = null;
        switch (id)
        {
            case "0":
                deftrayState = "<font color=red>" + new BLL.TranslationBase().GetTran("000521", "未支付") + "</font>";
                break;
            case "1":
                deftrayState = new BLL.TranslationBase().GetTran("000517", "已支付");
                break;
            case "2":
                deftrayState = "<font color=red>" + new BLL.TranslationBase().GetTran("000521", "未支付") + "</font>";
                break;
        }
        return deftrayState;

    }
    /// <summary>
    /// 证件类型
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static string GetPapertype(string code,string languagecode)
    {
        string sql = "select " + languagecode + " from t_translation where tablename ='bsco_PaperType' and primarykey= (select id from bsco_PaperType where papertypecode='" + code + "')";
        object obj = DBHelper.ExecuteScalar(sql);
        if (obj == null || obj == DBNull.Value)
        {
            return new BLL.TranslationBase().GetTran("000221", "无");
        }
        else
        {
            return obj.ToString();
        }
    }
    /// <summary>
    /// 获取运货方式
    /// </summary>
    /// <param name="sendType"></param>
    /// <returns></returns>
    public static string GetSendType(string sendType)
    {
        if (sendType == "1")
        {
            return new BLL.TranslationBase().GetTran("000464", "自提");
        }
        else
        {
            return new BLL.TranslationBase().GetTran("000543", "邮寄");
        }

    }

    /// <summary>
    /// 获取发货方式
    /// </summary>
    /// <param name="sendWay"></param>
    /// <returns></returns>
    public static string GetSendWay(string sendWay)
    {
        if (sendWay == "0")
        {
            return new BLL.TranslationBase().GetTran("007442", "服务机构收货");
        }
        else
        {
            return new BLL.TranslationBase().GetTran("007443", "会员收货");
        }

    }

    /// <summary>
    /// 支付类型
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string GetOrderPayType(object obj)
    {
        if (obj.ToString() == "0")
        {
            return new BLL.TranslationBase().GetTran("000221", "无");
        }
        else if (obj.ToString() == "1")
        {
            return new BLL.TranslationBase().GetTran("007442", "服务机构收货");
        }
        else if (obj.ToString() == "2")
        {
            return new BLL.TranslationBase().GetTran("007444", "电子货币支付");
        }
        else if (obj.ToString() == "3")
        {
            return new BLL.TranslationBase().GetTran("008123", "转账汇款");
        }
        else if (obj.ToString() == "4")
        {
            return new BLL.TranslationBase().GetTran("005963", "在线支付");
        }
        else if (obj.ToString() == "5")
        {
            return new BLL.TranslationBase().GetTran("000277", "周转款订货");
        }
        else if (obj.ToString() == "6")
        {
            return new BLL.TranslationBase().GetTran("007529", "订货款订货");
        }
        else if (obj.ToString() == "7")
        {
            return new BLL.TranslationBase().GetTran("008123", "转账汇款");
        }
        else if (obj.ToString() == "8")
        {
            return new BLL.TranslationBase().GetTran("005963", "在线支付");
        }
        else
        {
            return GetTran("000521", "未支付");
        }
    }

    /// <summary>
    /// 获取最新价格
    /// </summary>
    /// <returns></returns>
    public static decimal GetnowPrice() {
        decimal de = Convert.ToDecimal(DBHelper.ExecuteScalar("select CoinPrice  from CoinPriceList where id=(select max(id)  from CoinPriceList)  "));
        return de;
     }
    /// <summary>
    /// 判断是否可以开始交易
    /// </summary>
    /// <returns></returns>
    public static int GetISCanCharge()
    {
        int rec = 0;
        DateTime dn = DateTime.UtcNow;

        string sql = "select top 1  Opentime,Closetime,Opentime1,Closetime1 ,Opentime2,Closetime2 ,Opentime3,Closetime3  from  ExchangeTime order by id desc ";
        DataTable dt = DBHelper.ExecuteDataTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            int open = Convert.ToInt32(dt.Rows[0]["Opentime"]);
            int close = Convert.ToInt32(dt.Rows[0]["Closetime"]);
            int open1 = 0;
            int close1 = 0;
            int open2 = 0;
            int close2 = 0;
            int open3 = 0;
            int close3 = 0;
            if (dt.Rows[0]["Opentime1"].ToString() != "" && dt.Rows[0]["Opentime1"].ToString() != null)
            {
                 open1 = Convert.ToInt32(dt.Rows[0]["Opentime1"]);
            }
            if (dt.Rows[0]["Closetime1"].ToString() != "" && dt.Rows[0]["Closetime1"].ToString() != null)
            {
                 close1 = Convert.ToInt32(dt.Rows[0]["Closetime1"]);
            }
            if (dt.Rows[0]["Opentime2"].ToString() != "" && dt.Rows[0]["Opentime2"].ToString() != null)
            {
                 open2 = Convert.ToInt32(dt.Rows[0]["Opentime2"]);
            }
            if (dt.Rows[0]["Closetime2"].ToString() != "" && dt.Rows[0]["Closetime2"].ToString() != null)
            {
                 close2 = Convert.ToInt32(dt.Rows[0]["Closetime2"]);
            }
            if (dt.Rows[0]["Opentime3"].ToString() != "" && dt.Rows[0]["Opentime3"].ToString() != null)
            {
                 open3 = Convert.ToInt32(dt.Rows[0]["Opentime3"]);
            }
            if (dt.Rows[0]["Closetime3"].ToString() != "" && dt.Rows[0]["Closetime3"].ToString() != null)
            {
                 close3 = Convert.ToInt32(dt.Rows[0]["Closetime3"]);
            }
            if ((dn.Hour >= open && dn.Hour <= close) || (dn.Hour >= open1 && dn.Hour <= close1) || (dn.Hour >= open2 && dn.Hour <= close2) || (dn.Hour >= open3 && dn.Hour <= close3)) rec = 1;
            //if (dn.Hour >= open1.Hour && dn.Hour <= close1.Hour) rec = 1;
            //if (dn.Hour >= open2.Hour && dn.Hour <= close2.Hour) rec = 1;
            //if (dn.Hour >= open3.Hour && dn.Hour <= close3.Hour) rec = 1;
        }
        else
        {
            if (dn.Hour >= 9 && dn.Hour < 21) rec = 1;
        }
        return rec;
    }

    /// <summary>
    /// 获取手续费 违约金比例 ,石斛积分增长率
    /// </summary>
    /// <returns></returns>
    public static decimal GetSxfWyjblv(int type) {
       
        decimal reb=0;
        DataTable de = DBHelper.ExecuteDataTable("select top 1 para12,para13,para14   from config order by ExpectNum desc");
        if(de!=null && de.Rows.Count>0){
            if (type == 0) reb = Convert.ToDecimal(de.Rows[0]["para13"]);
            if (type == 1) reb = Convert.ToDecimal(de.Rows[0]["para12"]);
            if (type == 2) reb = Convert.ToDecimal(de.Rows[0]["para14"]);

        }
        return reb;
     }


    /// <summary>
    /// 根据复投金额计算出来得到的石斛积分
    /// </summary>
    /// <param name="number"></param>
    /// <param name="totalmoney"></param>
    /// <returns></returns>
    public static decimal GetFTJBbyMoney(string number, decimal totalmoney)
    {
        decimal dm = 0;
        SqlParameter[] sps = new SqlParameter[] { 
         new SqlParameter("@number",number),
         new SqlParameter("@ftmoney",totalmoney), 
         new SqlParameter("@ftjb",DbType.Decimal)
        };
         sps[2].Direction = ParameterDirection.Output;
         dm = Convert.ToDecimal( DBHelper.ExecuteScalar("GetFTJBbyMoney" ,sps,CommandType.StoredProcedure ));
        
        return  dm;
    }
    /// <summary>
    /// 将传入的字符串中间部分字符替换成特殊字符
    /// </summary>
    /// <param name="value">需要替换的字符串 </param>
    /// <param name="startLen">前保留长度 4</param>
    /// <param name="endLen">尾保留长度 4</param>
    /// <param name="replaceChar">特殊字符 *</param>
    /// <returns>被特殊字符替换的字符串</returns>
    public static string ReplaceWithSpecialChar(string value, int startLen, int endLen, char specialChar)
    {
        try
        {
            int lenth = value.Length - startLen - endLen;

            string replaceStr = value.Substring(startLen, lenth);

            string specialStr = string.Empty;

            for (int i = 0; i < replaceStr.Length; i++)
            {
                specialStr += specialChar;
            }

            value = value.Replace(replaceStr, specialStr);
        }
        catch (Exception)
        {
            throw;
        }

        return value;
    }

    public static bool WPermission(string company)
    {
        DataTable dt = StorageInBLL.GetMoreManagerPermissionByNumber(company);
        if (dt == null || dt.Rows.Count <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    /// <summary>
    /// 查询库存
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    public static DataTable GetSeacrhinventory(string product)
    {
        SqlParameter[] sparams = new SqlParameter[]
        {
                new SqlParameter("@Productid",SqlDbType.VarChar)
        };
        sparams[0].Value = product;

        string sql = "select TotalIn-TotalOut as pcount from ProductQuantity where Productid=@Productid";
        return DBHelper.ExecuteDataTable(sql, sparams, CommandType.Text);
    }
    public static string ReturnAlert(string content)
    {
        string retVal;
        retVal = "<script language='javascript'>alert('" + content.Replace("'", " ").Replace("\r", " ").Replace("\n", "").Replace("\t", " ") + "');</script>";
        return retVal;
    }
    /// <summary>
    /// 查看会员注册期数
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public static int GetMemberExpectNum(string number)
    {
        int value = 0;
        string sql = @"select  ExpectNum from memberinfo where Number=@Number";

       SqlParameter[] sparams = new SqlParameter[]
       {
                new SqlParameter("@Number",SqlDbType.NVarChar,50)
       };
       sparams[0].Value = number;
       DataTable dt= DBHelper.ExecuteDataTable(sql, sparams, CommandType.Text);
        if (dt != null && dt.Rows.Count > 0)
        {
            value = Convert.ToInt32(dt.Rows[0]["ExpectNum"]);
        }
       return value;
    }

    /// <summary>
    /// 获取当前阶段 每个币种占两个阶段
    /// </summary>
    /// <returns></returns>
    public static int GetcurJieDuan() {
        int jd =0;
        DataTable dtc = DBHelper.ExecuteDataTable("select CoinIndex ,Coinstate  from  CoinPlant where  Coinstate>0  and Coinstate<3 order by id ");
        string conindex = "";
        int cst = 0;
        if (dtc != null && dtc.Rows.Count > 0)
        {
            DataRow dr = dtc.Rows[0];
            conindex = dr["CoinIndex"].ToString() ;
            cst = Convert.ToInt32(dr["Coinstate"]);
        }
        switch (conindex)
        {
            case "CoinA":
                jd = 0; jd += cst;
                break;
            case "CoinB":
                jd = 2; jd += cst;
                break;
            case "CoinC":
                jd = 4; jd += cst;
                break;
            case "CoinD":
                jd = 6; jd += cst;
                break;
            case "CoinE":
                jd = 8; jd += cst;
                break; 
        }

        return jd;
    }
}
