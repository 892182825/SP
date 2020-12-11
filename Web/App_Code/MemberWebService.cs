using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;
using BLL.CommonClass;
using System.Text;
using Model;
using BLL.other.Member;
using BLL.Registration_declarations;
using DAL;
using Model.Other;
using BLL.other.Company;
using BLL.Logistics;
using BLL.other.Store;
using BLL.other;
using BLL.MoneyFlows;
using System.IO;
using System.Net;

/// <summary>
///MemberWebService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class MemberWebService : System.Web.Services.WebService
{
    public MemberWebService()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 

    }

    /// <summary>
    /// 语言选择
    /// </summary>
    [WebMethod(EnableSession = true)]
    public static void LoadLanguage()
    {
        SqlDataReader sdr = DAL.DBHelper.ExecuteReader("Select Top 1 ID,languageCode From Language where languageCode='L001'");
        while (sdr.Read())
        {
            HttpContext.Current.Session["languageCode"] = sdr["languageCode"].ToString().Trim();
            HttpContext.Current.Session["LanguageID"] = sdr["ID"].ToString();
        }
        sdr.Close();
    }

#region 会员系统

    #region 相关判断检测及数据检索
    /// <summary>
    /// 相关参数解密测试
    /// </summary>
    /// <param name="canshus"></param>
    /// <returns></returns>
    [WebMethod]
    public string Demo(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }

            return canshus;
        }
        else
            return "请传入相关的参数";
    }
    /// <summary>
    /// 检测登陆的会员是否存在
    /// </summary>
    /// <param name="canshus"></param>
    /// <returns></returns>
    [WebMethod]
    public string IsExistsLoginMember(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }


            string[] str_parms = canshus.Split(','); //参数分解

            string bianhao = str_parms[0]; //会员编号
            string pwd = str_parms[1];     //会员密码

            object o_login = DAL.DBHelper.ExecuteScalar("select Number  from MemberInfo  where LoginPass='" + pwd + "'");
            if (o_login != null)
            {
                if (Encryption.Encryption.GetEncryptionByMD5(o_login.ToString()) == bianhao)
                {
                    return "true";
                }
                else
                    return "false";
            }
            else
                return "false";
        }
        else
            return "请传入相关的参数";
    }

    /// <summary>
    /// 检测会员是否存在
    /// </summary>
    /// <param name="canshus"></param>
    /// <returns></returns>
    [WebMethod]
    public bool IsExistsMember(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                return false;
            }

            string bianhao = canshus; //会员编号

            int count = Convert.ToInt32(DAL.DBHelper.ExecuteScalar("select COUNT(0)  from MemberInfo  where Number='" + bianhao + "'"));
            if (count > 0)
            {
                return true;
            }
            else
                return false;
        }
        else
            return false;
    }
    /// <summary>
    /// 检测会员是否存在
    /// </summary>
    /// <param name="bianhao"></param>
    /// <returns></returns>
    public bool MemberIsExists(string bianhao)
    {

        int count = Convert.ToInt32(DAL.DBHelper.ExecuteScalar("select COUNT(0)  from MemberInfo  where Number='" + bianhao + "'"));
        if (count > 0)
        {
            return true;
        }
        else
            return false;
    }

    /// <summary>
    /// 检测服务机构是否存在
    /// </summary>
    /// <param name="canshus"></param>
    /// <returns></returns>
    [WebMethod]
    public string IsExistsStore(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }

            string storeid = canshus;  //服务机构编号

            int count = Convert.ToInt32(DAL.DBHelper.ExecuteScalar("select COUNT(0)  from StoreInfo  where StoreID='" + storeid + "'"));
            if (count > 0)
                return "true";
            else
                return "false";
        }
        else
            return "请传入相关的参数";
    }

    /// <summary>
    /// 检测会员是否有自己的服务机构
    /// </summary>
    /// <param name="canshus"></param>
    /// <returns></returns>
    [WebMethod]
    public string MemberHasStore(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }

            string bianhao = canshus;

            if (Convert.ToInt32(DAL.DBHelper.ExecuteScalar("select count(0) from StoreInfo where Number='" + bianhao + "'")) > 0)
                return "true";
            else
                return "false";
        }
        else
            return "请传入相关的参数";
    }
    /// <summary>
    /// 获取会员的所属店铺编号
    /// </summary>
    /// <param name="bianhao"></param>
    /// <returns></returns>
    public string GetMemberOfStoreID(string bianhao)
    {
        object o_storeid = DAL.DBHelper.ExecuteScalar("select StoreID from MemberInfo where Number='" + bianhao + "'");
        if (o_storeid != null)
            return o_storeid.ToString();
        else
            return "";
    }
    /// <summary>
    /// 验证密码是否正确
    /// </summary>
    /// <param name="parmeters">加密的参数</param>
    /// <returns></returns>
    [WebMethod]
    public string CheckPass(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            canshus = PublicClass.DESDeCode(canshus);

            string[] str_checkpass = canshus.Split(',');

            if (str_checkpass.Length != 4)
            {
                int sysType = Convert.ToInt32(str_checkpass[0]);    //0 公司  1 服务机构  2 会员
                int passType = Convert.ToInt32(str_checkpass[1]);   //0 登陆密码  1 二级密码
                string bianhao = str_checkpass[2];      //管理员编号/服务机构编号/会员编号
                string pass = str_checkpass[3]; //登陆密码/二级密码【MD5加密过的】

                if (sysType == 0)//公司
                {
                    if (passType == 0)//登陆密码
                    {
                        if (IndexBLL.CheckLogin("Company", bianhao.Trim().ToLower(), pass))
                            return "true";
                        else
                            return "false";
                    }
                    else if (passType == 1)//二级密码
                    {

                    }
                }
                else if (sysType == 1)//服务机构
                {
                    if (passType == 0)//登陆密码
                    {
                        if (IndexBLL.CheckLogin("Store", bianhao.Trim().ToLower(), pass))
                            return "true";
                        else
                            return "false";
                    }
                    else if (passType == 1)//二级密码
                    {
                        //PwdModifyBLL.checkstoreadvpass(bianhao, pass);
                        if (PwdModifyBLL.checkstore(bianhao, pass, 0) > 0)
                            return "true";
                        else
                            return "false";
                    }
                }
                else if (sysType == 2)//会员
                {
                    if (passType == 0)//登陆密码
                    {
                        if (IndexBLL.CheckLogin("Member", bianhao.Trim().ToLower(), pass))
                            return "true";
                        else
                            return "false";
                    }
                    else if (passType == 1)//二级密码
                    {
                        if (PwdModifyBLL.check(bianhao, pass, 0) > 0)
                            return "true";
                        else
                            return "false";
                    }
                }
            }
        }

        return "false";
    }
    /// <summary>
    /// 加载期数
    /// </summary>
    /// <param name="canshus"></param>
    /// <returns></returns>
    [WebMethod]
    public string LoadExpectNum(string canshus)
    {

        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }
            

            string[] str_parmeters = canshus.Split(','); //分解参数

            //参数
            string needQuanBu = str_parmeters[0]; //是否需要全部选项    0:不要全部;    1:要全部
            int showQISHUorRIQI = Convert.ToInt32(str_parmeters[1]); // 0:表示按期数显示;     1：表示按日期显示
            string selectQishu = str_parmeters[2]; //默认选中的期数   -1,没有默认选项



            StringBuilder XmlStr = new StringBuilder();
            XmlStr.Append("<LoadExpectNum>");


            int maxqishu = CommonDataDAL.getMaxqishu();
            int i = 1;

            if (needQuanBu == "1")
            {
                XmlStr.Append("<ExpectNum enId='en_" + i + "'>");

                XmlStr.AppendFormat("<qishu>{0}</qishu>", "-1");
                XmlStr.AppendFormat("<qishuStr>{0}</qishuStr>", "全部");


                XmlStr.Append("</ExpectNum>");

                i += 1;
            }

            //int showQISHUorRIQI = CommonDataDAL.GetShowQishuDate();   // 0:表示按期数显示，1：表示按日期显示

            SqlDataReader dr = DBHelper.ExecuteReader("SELECT ExpectNum,Convert(char(10),[Date],120) as [Date],stardate,enddate FROM CONFIG ORDER BY ExpectNum");
            //循环遍历，添加期数选项
            while (dr.Read())
            {

                XmlStr.Append("<ExpectNum enId='en_" + i + "'>");


                if (showQISHUorRIQI == 0)
                {
                    XmlStr.AppendFormat("<qishu>{0}</qishu>", dr["ExpectNum"].ToString());
                    XmlStr.AppendFormat("<qishuStr>{0}</qishuStr>", "第 " + dr["ExpectNum"].ToString() + " 期");
                }
                else if (showQISHUorRIQI == 1)
                {
                    XmlStr.AppendFormat("<qishu>{0}</qishu>", dr["ExpectNum"].ToString());
                    XmlStr.AppendFormat("<qishuStr>{0}</qishuStr>", dr["Date"].ToString());
                }
                else
                {
                    XmlStr.AppendFormat("<qishu>{0}</qishu>", dr["ExpectNum"].ToString());
                    XmlStr.AppendFormat("<qishuStr>{0}</qishuStr>", "第 " + dr["ExpectNum"].ToString() + " 期 (" + dr["stardate"].ToString() + "至" + dr["enddate"].ToString() + ")");
                }

                XmlStr.Append("</ExpectNum>");

                i += 1;
            }
            dr.Close();
            dr.Dispose();


            XmlStr.Append("</LoadExpectNum>");

            return XmlStr.ToString();
        }
        else
            return "请传入相关的参数";
    }
    #endregion

    #region 支付模块
    /// <summary>
    /// 生成尾数
    /// </summary>
    /// <param name="billid">订单编号/汇款单号</param>
    /// <param name="roletype">1 会员  2 服务机构</param>
    /// <param name="dotype">1 订单支付生成尾数  2 汇款充值生成尾数</param>
    /// <param name="biaohao">会员编号/服务机构编号</param>
    /// <param name="ip"></param>
    /// <param name="remark"></param>
    public void Loadmk(string billid, int roletype, int dotype, string biaohao, string ip, string remark)
    {

        double ordertmoney = 0;

        MemberOrderModel memberorder = null;
        DataTable ordergoodstable = null;

        if (roletype == 1)  //会员
        {
            if (dotype == 1)
            {
                memberorder = MemberOrderDAL.GetMemberOrder(billid);
                ordertmoney = Convert.ToDouble(memberorder.TotalMoney);
            }
        }
        else if (roletype == 2)  //服务机构
        {
            if (dotype == 1)
            {
                ordergoodstable = OrderDetailDAL.Getordergoodstablebyorderid(billid);
                if (ordergoodstable != null && ordergoodstable.Rows.Count > 0)
                    ordertmoney = Convert.ToDouble(ordergoodstable.Rows[0]["totalmoney"]);
            }
        }

        if (dotype == 1)  //订单支付生成尾数
        {

            remark = biaohao + "汇款支付订单" + billid;

            int c = 1; //0 服务机构   1 会员
            if (roletype == 2)
                c = 0;

            RemittancesDAL.GetAddnewRemattice(biaohao, ordertmoney, ip, billid, remark, c);

        }
        else if (dotype == 2) //汇款充值生成尾数
        {
            RemittancesDAL.GetAddnewRetmp(biaohao, billid, ip, remark, roletype);
        }

    }
    /// <summary>
    /// 获取使用的银行代码
    /// </summary>
    /// <param name="usetype"></param>
    /// <param name="hdcode"></param>
    /// <returns></returns>
    public string Getbankcode(int usetype, string hdcode)
    {
        string bankcode = "";

        switch (hdcode)
        {
            case "ICBCB2C":
                if (usetype == 1)
                    bankcode = hdcode;
                else if (usetype == 2)
                    bankcode = "ICBC";
                else if (usetype == 3)
                    bankcode = "ICBC";
                break;
            case "BOCB2C":
                if (usetype == 1)
                    bankcode = hdcode;
                else if (usetype == 2)
                    bankcode = "BOC";
                else if (usetype == 3)
                    bankcode = "";
                break;
            case "COMM":
                if (usetype == 1)
                    bankcode = hdcode;
                else if (usetype == 2)
                    bankcode = "BCOM";
                else if (usetype == 3)
                    bankcode = hdcode;
                break;
            case "SPABANK":
                if (usetype == 1)
                    bankcode = hdcode;
                else if (usetype == 2)
                    bankcode = "PAB";
                else if (usetype == 3)
                    bankcode = "SZPAB";
                break;
            case "NBBANK":
                if (usetype == 1)
                    bankcode = hdcode;
                else if (usetype == 2)
                    bankcode = "NBCB";
                else if (usetype == 3)
                    bankcode = "NBCB";
                break;
            case "HZCBB2C":
                if (usetype == 1)
                    bankcode = hdcode;
                else if (usetype == 2)
                    bankcode = "HZB";
                else if (usetype == 3)
                    bankcode = "HCCB";
                break;
            case "BJBANK":
                if (usetype == 1)
                    bankcode = hdcode;
                else if (usetype == 2)
                    bankcode = "BOB";
                else if (usetype == 3)
                    bankcode = "";
                break;
            case "SHRCB":
                if (usetype == 1)
                    bankcode = hdcode;
                else if (usetype == 2)
                    bankcode = "SHRCC";
                else if (usetype == 3)
                    bankcode = "";
                break;
            case "PSBC-DEBIT":
                if (usetype == 1)
                    bankcode = hdcode;
                else if (usetype == 2)
                    bankcode = "";
                else if (usetype == 3)
                    bankcode = "PSBC";
                break;

            default:
                bankcode = hdcode;
                break;
        }

        return bankcode;
    }
    /// <summary>
    /// 在线支付转向路径
    /// </summary>
    /// <param name="money"></param>
    /// <returns></returns>
    public string Getposturl(int roletype, int dotype, int paytype, string hkid, double totalmoney)
    {

        string posturl = "";
        string banklcode = "";

        if (paytype == 3)
        {
            posturl = "payment/Default.aspx?total_fee=" + totalmoney + "&out_trade_no=" + hkid;
        }
        else if (paytype == 4) //环迅支付
        {
            posturl = "huanxun/redirect.aspx?totalmoney=" + totalmoney + "&hkid=" + hkid;
        }
        else if (paytype == 5)  //快钱支付
        {
            posturl = "quickPay/sendpki.aspx?totalmoney=" + totalmoney + "&hkid=" + hkid;
        }
        else if (paytype == 6) //盛付通支付
        {
            posturl = "shengpay/SendOrderSFT.aspx?totalmoney=" + totalmoney + "&hkid=" + hkid;
        }
        else
        {

            //网银充入接口 支付宝
            if (DocTypeTableDAL.Getpaytypeisusebyid(1, 8))
            {
                banklcode = Getbankcode(1, "");
                posturl = "payment/Default.aspx?total_fee=" + totalmoney + "&out_trade_no=" + hkid + "&defaultbank=" + banklcode;
            }
            else if (DocTypeTableDAL.Getpaytypeisusebyid(2, 8)) //快钱
            {
                banklcode = Getbankcode(2, "");
                posturl = "quickPay/sendpki.aspx?totalmoney=" + totalmoney + "&hkid=" + hkid + "&defaultbank=" + banklcode;
            }
            else if (DocTypeTableDAL.Getpaytypeisusebyid(3, 8)) //盛付通
            {
                banklcode = Getbankcode(3, "");
                posturl = "shengpay/SendOrderSFT.aspx?totalmoney=" + totalmoney + "&hkid=" + hkid + "&defaultbank=" + banklcode;
            }

        }
        return posturl;
    }
    /// <summary>
    /// 支付模块加载
    /// </summary>
    /// <param name="canshus">加密参数</param>
    /// <returns></returns>
    [WebMethod]
    public string Pay_Load(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {

            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }


            string[] str_parms = canshus.Split(','); //参数解密

            if (str_parms.Length == 5)
            {
                ///参数
                string billid = str_parms[0];   //支付id：订单号/汇款单号
                string dotype = str_parms[1];   //支付类型：1 订单支付  2 汇款充值
                string roletype = str_parms[2]; //支付角色：1 会员 2 店铺
                string loginnumber = str_parms[3];//支付编号
                string ip = str_parms[4];         //IP


                string biaohao = "";
                string remark = "";

                int i_roletype = Convert.ToInt32(roletype);
                int i_dotype = Convert.ToInt32(dotype);

                object o_bianhao = null;

                if (i_roletype == 1) //会员
                {
                    if (i_dotype == 1)//1 订单支付生成尾数
                    {
                        o_bianhao = DAL.DBHelper.ExecuteScalar("select Number from MemberOrder where OrderID='" + billid + "'");
                        if (o_bianhao != null)
                            biaohao = o_bianhao.ToString();

                        remark = biaohao + "手机支付订单" + billid;
                    }
                    else if (i_dotype == 2)//2 汇款充值生成尾数
                    {
                        o_bianhao = DAL.DBHelper.ExecuteScalar("select RemitNumber from Remittances where RemitStatus=1 and RemittancesID='" + billid + "'");
                        if (o_bianhao != null)
                            biaohao = o_bianhao.ToString();

                        remark = biaohao + "手机支付汇款单" + billid;
                    }
                }
                else if (i_roletype == 2) //服务机构
                {
                    if (i_dotype == 1)//1 订单支付生成尾数
                    {
                        o_bianhao = DAL.DBHelper.ExecuteScalar("select StoreID from OrderGoods where OrderGoodsID='" + billid + "'");
                        if (o_bianhao != null)
                            biaohao = o_bianhao.ToString();

                        remark = biaohao + "手机支付订单" + billid;
                    }
                    else if (i_dotype == 2)//2 汇款充值生成尾数
                    {
                        o_bianhao = DAL.DBHelper.ExecuteScalar("select RemitNumber from Remittances where RemitStatus=0 and RemittancesID='" + billid + "'");
                        if (o_bianhao != null)
                            biaohao = o_bianhao.ToString();

                        remark = biaohao + "手机支付汇款单" + billid;
                    }
                }

                //离线支付
                if (DocTypeTableDAL.Getpaytypeisusebyid(1, 1) || DocTypeTableDAL.Getpaytypeisusebyid(6, 5))
                {
                    //生成尾数
                    Loadmk(billid, Convert.ToInt32(roletype), Convert.ToInt32(dotype), biaohao, ip, remark);
                }
                else
                    return "支付错误";
            }
            else
                return "参数不正确";

            return "加载成功";
        }
        else
            return "请传入相关参数";
    }
    /// <summary>
    /// 支付 订单/汇款单
    /// </summary>
    /// <param name="canshus">加密的参数</param>
    /// <returns></returns>
    [WebMethod]
    public string Pay(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {

            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }


            //参数定义
            int roletype = 0;    //1 会员  2 服务机构
            int dotype = 0;       //1 订单支付生成尾数  2 汇款充值生成尾数
            string billid = ""; //订单编号/汇款单号
            int paytype1 = 0;    //支付方式【1 电子货币支付  2 去服务机构支付  3 在线支付  4 离线支付  5 服务机构支付 会员订单  6 服务机构电子账户支付 订货单】
            int paytype2 = 0;    //具体支付账户：0 消费账户  1 现金账户  3 支付宝  4 环迅  5 快钱  10 订货款  11 周转款
            int operateType = 0;   //0 公司操作  1 服务机构操作  2 会员操作
            string loginnumber = ""; //服务机构编号/会员编号
            string curip = "";    //IP
            string suregetmoney = ""; //0 我暂未收到该会员支付的上数金额   1 我确认已收到该会员支付的上数金额
            string advpass = "";   //二级密码

            string[] str_payInfo = canshus.Split(','); //分解参数
            if (str_payInfo.Length == 10)
            {
                roletype = Convert.ToInt32(str_payInfo[0]);    //1 会员  2 服务机构
                dotype = Convert.ToInt32(str_payInfo[1]);    //1 订单支付生成尾数  2 汇款充值生成尾数
                billid = str_payInfo[2];                     //订单编号/汇款单号
                paytype1 = Convert.ToInt32(str_payInfo[3]);    //支付方式
                paytype2 = Convert.ToInt32(str_payInfo[4]);    //支付方式
                operateType = Convert.ToInt32(str_payInfo[5]); //0 公司操作  1 服务机构操作  2 会员操作
                loginnumber = str_payInfo[6];             //服务机构编号/会员编号
                curip = str_payInfo[7];          //IP
                suregetmoney = str_payInfo[8];
                advpass = str_payInfo[9];

                DataTable dt = null;

                if (dotype == 1) //1 订单支付生成尾数
                {
                    if (roletype == 1)//1 会员
                        dt = DAL.DBHelper.ExecuteDataTable("select top 1 Number,TotalMoney from MemberOrder where OrderID='" + billid + "'");
                    else if (roletype == 2)//2 服务机构
                        dt = DAL.DBHelper.ExecuteDataTable("select top 1 StoreID as Number,TotalMoney from OrderGoods where OrderGoodsID='" + billid + "'");
                }
                else if (dotype == 2) //2 汇款充值生成尾数
                {
                    if (roletype == 1)//1 会员
                        dt = DAL.DBHelper.ExecuteDataTable("select top 1 RemitNumber as Number,RemitMoney as TotalMoney from Remittances where RemitStatus=1 and RemittancesID='" + billid + "'");
                    else if (roletype == 2)//2 服务机构
                        dt = DAL.DBHelper.ExecuteDataTable("select top 1 RemitNumber as Number,RemitMoney as TotalMoney from Remittances where RemitStatus=0 and RemittancesID='" + billid + "'");
                }

                object o_remittancesid = DAL.DBHelper.ExecuteScalar("select RemittancesID  from Remittances  where Relationorderid='" + billid + "'");

                if (paytype1 == 4) //离线支付
                {
                    if (o_remittancesid != null && dotype == 1)
                        RemittancesDAL.UPRemittancesre(o_remittancesid.ToString());
                    else if (dotype == 2)
                        RemittancesDAL.UPRemittancesre(billid);
                }
                else
                {
                    if (o_remittancesid != null && dotype == 1)
                        RemittancesDAL.DelRemittancesrelationremtemp(o_remittancesid.ToString());
                    else if (dotype == 2)
                        RemittancesDAL.DelRemittancesrelationremtemp(billid);

                    if (paytype1 == 1)//电子货币支付
                    {

                        if (MemberOrderDAL.Getvalidteiscanpay(billid, loginnumber))//限制订单必须有订货所属店铺推荐人协助人支付)
                        {
                            return "该订单不属于您的协助或推荐报单，不能完成支付！";
                        }
                        if (dt.Rows[0]["Number"].ToString() != loginnumber)//如果不是自己给自己支付
                        {
                            if (suregetmoney == "0")
                            {
                                return "请确认已收到该会员支付的报单金额";
                            }
                        }
                        if (advpass.Trim() == "")
                        {
                            return "二级密码不能为空！";
                        }
                        string oldPass = Encryption.Encryption.GetEncryptionPwd(advpass.Trim(), loginnumber);
                        int n = PwdModifyBLL.check(loginnumber, oldPass, 1);
                        if (n <= 0)
                        {
                            return "二级密码不正确！";
                        }

                        if (MemberInfoDAL.CheckState(loginnumber))
                        {
                            return "会员账户已冻结，不能完成支付!";
                        }

                        int res = AddOrderDataDAL.OrderPayment(loginnumber, billid, curip, roletype, dotype, paytype2, loginnumber, "", 2, -1, 1, 1, "", 0, "");

                        if (res == 0)
                            return "支付成功";
                        else
                            return "支付失败";

                    }
                    else if (paytype1 == 2)//去服务机构支付
                    {
                        return "请将" + billid + "订单款额 " + dt.Rows[0]["TotalMoney"].ToString() + "元 交给您的购货店铺，并要求其为您支付订单";
                    }
                    else if (paytype1 == 3)//在线支付
                    {
                        string hkid = billid;

                        if (dotype == 1)
                            hkid = RemittancesDAL.AddRemittancebytypeOnline(billid, roletype, curip, loginnumber, 1);
                        else if (dotype == 2)
                            RemittancesDAL.UpdateOnlinepayway(billid, 4);

                        string posturl = Getposturl(roletype, dotype, paytype2, hkid, 0);

                        return posturl;
                    }
                    else if (paytype1 == 5)//服务机构支付 会员订单
                    {

                        if (MemberOrderDAL.Getvalidteiscanpay(billid, loginnumber))//限制订单必须有订货所属店铺推荐人协助人支付)
                        {
                            return "该订单不属于您的协助或推荐报单，不能完成支付！";
                        }
                        if (suregetmoney == "0") //验证是否确认收到款
                        {
                            return "请确认已收到该会员支付的报单金额";
                        }
                        if (advpass == "")
                        {
                            return "二级密码不能为空！";
                        }
                        string oldPass = Encryption.Encryption.GetEncryptionPwd(advpass, loginnumber);
                        int n = PwdModifyBLL.checkstore(loginnumber, oldPass, 1);
                        if (n <= 0)
                        {
                            return "二级密码不正确！";
                        }

                        int res = AddOrderDataDAL.OrderPayment(loginnumber, billid, curip, 2, 3, paytype2, loginnumber, "", 5, -1, 1, 1, "", 0, "");

                        if (res == 0)
                            return "支付成功";
                        else
                            return "支付失败";

                    }
                    else if (paytype1 == 6)//服务机构电子账户支付 订货单
                    {
                        if (advpass == "")
                        {
                            return "二级密码不能为空！";
                        }
                        string oldPass = Encryption.Encryption.GetEncryptionPwd(advpass, loginnumber);
                        int n = PwdModifyBLL.checkstore(loginnumber, oldPass, 1);
                        if (n <= 0)
                        {
                            return "二级密码不正确！";
                        }

                        int res = AddOrderDataDAL.OrderPayment(loginnumber, billid, curip, roletype, dotype, paytype2, loginnumber, "", 2, -1, 1, 1, "", 0, "");

                        if (res == 0)
                            return "支付成功";
                        else
                            return "支付失败";

                    }
                }


                return "支付成功";

            }
            else
                return "传入的参数不正确";
        }
        else
            return "请传入相关参数";
    }
    #endregion

    #region  首页
    /// <summary>
    /// 会员系统-首页-查询 邮件 的SQL语句
    /// </summary>
    /// <param name="bianhao"></param>
    /// <returns></returns>
    private string GetEmailList(string bianhao)
    {
        SqlConnection conn = new SqlConnection(DAL.DBHelper.connString);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "MessageCheckCondition";
        conn.Open();

        string rtn = "";
        string idlist = "";
        StringBuilder sb = new StringBuilder();
        sb.Append("DropFlag=0 and LoginRole=2 and ReadFlag=0 and Receive like '%" + bianhao + "%' and ID not in(select MessageID from dbo.F_DroppedReceiveByOperator('" + bianhao + "',2))");
        DataTable dt = DAL.DBHelper.ExecuteDataTable("select ID,MessageSendID from MessageReceive where DropFlag=0 and loginRole=2 and Receive='*' and ID not in(select MessageID from dbo.F_DroppedReceiveByOperator('" + bianhao + "',2)) and MessageType='m'");
        foreach (DataRow row in dt.Rows)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@MessageSendID", SqlDbType.Int).Value = Convert.ToInt32(row["MessageSendID"]);
            cmd.Parameters.Add("@number", SqlDbType.VarChar).Value = bianhao;
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
        string sql1 = "SELECT ma.MessageID FROM dbo.MessageReadCondition AS ma,dbo.MessageSend AS me WHERE ma.MessageID=me.ID and DropFlag=0 and me.SenderRole=0 and me.Receive='*' and me.LoginRole=2 and ma.ConditionLeader='" + bianhao + "'";
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
        return "select top 5 * from MessageReceive where " + sb.ToString() + "and ReadFlag=0";
    }
    /// <summary>
    /// 标准时间格式
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public string GetBiaoZhunTime(string dt)
    {
        return Convert.ToDateTime(dt).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString("yyyy-MM-dd HH:mm");
    }
    /// <summary>
    /// 会员系统-首页-查询 公告 的SQL语句
    /// </summary>
    /// <returns></returns>
    private string GetAnnouncement(string bianhao)
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
            cmd.Parameters.Add("@number", SqlDbType.VarChar).Value = bianhao;
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
        string sql1 = "SELECT ma.MessageID FROM dbo.MessageReadCondition AS ma,dbo.MessageSend AS me WHERE ma.MessageID=me.ID and DropFlag=0 and me.SenderRole=0 and me.Receive='*' and me.LoginRole=2 and ma.ConditionLeader='" + bianhao + "'";
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
        return "select top 5 ID, LoginRole, Receive, InfoTitle, Content, SenderRole, Sender, Senddate, DropFlag, ReadFlag from MessageSend where DropFlag=0 and LoginRole=2 and SenderRole=0 and Receive='*' and " + where + " order by Senddate asc";
    }
    /// <summary>
    /// 会员系统-首页【用户信息/账户信息/业务信息/业绩信息】
    /// </summary>
    /// <param name="canshus"></param>
    /// <returns></returns>
    [WebMethod]
    public string Member_FirstPage(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }

            string bianhao = canshus; //会员编号

            //用户信息
            string number = "";
            string name = "";
            string petname = "";
            string registerDate = "";
            string activeDate = "";
            string lastLoginDate = "";
            string level = "";

            //账户信息
            string consumerAccount = "";
            string cashAccount = "";

            //业务信息
            string currentNewNetNum = "";
            string newYeji = "";
            string totalYeji = "";
            string totalPeople = "";

            //业绩信息
            string currentTotalNetRecord = "";
            string currentOneMark = "";
            string totalNetNum = "";
            string dTotalNetNum = "";
            string currentOneMark1 = "";
            string totalNetRecord = "";
            string dCurrentTotalNetRecord = "";
            string dTotalNetRecord = "";
            string dCurrentNewNetNum = "";




            ///用户信息

            // 未读邮件 最新公告
            int emailCount = Convert.ToInt32(DAL.DBHelper.ExecuteScalar("select count(*) from MessageReceive where DropFlag=0 and  readflag=0 and LoginRole=2 and  Receive ='" + bianhao + "'").ToString());
            int announcementCount = Convert.ToInt32(DAL.DBHelper.ExecuteScalar("select count(*) from MessageReceive where DropFlag=0 and LoginRole=2 and SenderRole=0 and Receive='*'").ToString());


            SqlDataReader sdr = DAL.DBHelper.ExecuteReader("select Number,Name,registerdate,ActiveDate,LastLoginDate,petname,levelint,(TotalRemittances-TotalDefray) as CashAccount from MemberInfo  where Number='" + bianhao + "'");
            if (sdr.Read())
            {
                number = sdr["Number"].ToString();//会员编号
                name = Encryption.Encryption.GetDecipherName(sdr["Name"].ToString());//会员姓名
                petname = sdr["petName"].ToString();//会员昵称
                registerDate = GetBiaoZhunTime(sdr["registerdate"].ToString());//注册日期
                activeDate = GetBiaoZhunTime(sdr["ActiveDate"].ToString());//激活日期
                lastLoginDate = GetBiaoZhunTime(sdr["LastLoginDate"].ToString());//上次登录时间
                level = CommonDataBLL.GetLevelStr(sdr["levelint"].ToString());//会员级别

                cashAccount = Convert.ToDouble(sdr["CashAccount"].ToString()).ToString("0.00");//现金账户
            }
            sdr.Close();
            sdr.Dispose();




            ///账户信息

            //消费账户
            consumerAccount = double.Parse(CommonDataBLL.GetLeftMoney(bianhao)).ToString("0.00");


            ///业务信息
            ///业绩信息

            int maxQishu = CommonDataBLL.getMaxqishu();

            DataTable dt = DetialQueryBLL.GetMemberInfoBalanceByNumber(maxQishu, bianhao);
            if (dt.Rows.Count > 0)
            {

                //业务信息
                currentNewNetNum = dt.Rows[0]["CurrentNewNetNum"].ToString();//新增人数
                newYeji = double.Parse(dt.Rows[0]["CurrentTotalNetRecord"].ToString()).ToString("0.00");//新增业绩
                totalYeji = double.Parse(dt.Rows[0]["TotalNetRecord"].ToString()).ToString("0.00");//总网人数
                totalPeople = dt.Rows[0]["TotalNetNum"].ToString();//总网业绩


                //业绩信息
                currentTotalNetRecord = (Convert.ToDouble(dt.Rows[0]["CurrentTotalNetRecord"])).ToString("0.00");

                currentOneMark = (Convert.ToDouble(dt.Rows[0]["CurrentOneMark"])).ToString("0.00");
                level = dt.Rows[0]["levelstr"].ToString();

                if (dt.Rows[0]["TotalNetNum"] == null || dt.Rows[0]["TotalNetNum"].ToString() == "")
                {
                    totalNetNum = "0"; ;
                }
                else
                {
                    totalNetNum = (Convert.ToDouble(dt.Rows[0]["TotalNetNum"])).ToString("0.00");
                }
                dTotalNetNum = (Convert.ToDouble(dt.Rows[0]["DTotalNetNum"])).ToString("0.00");

                //个人总资格业绩
                if (dt.Rows[0]["TotalOneMark"] == null || dt.Rows[0]["TotalOneMark"].ToString() == "")
                {
                    currentOneMark1 = "0.00";
                }
                else
                {
                    currentOneMark1 = (Convert.ToDouble(dt.Rows[0]["TotalOneMark"].ToString())).ToString("0.00");
                }

                if (dt.Rows[0]["TotalNetRecord"] == null || dt.Rows[0]["TotalNetRecord"].ToString() == "")
                {
                    totalNetRecord = "0.00";
                }
                else
                {
                    totalNetRecord = (Convert.ToDouble(dt.Rows[0]["TotalNetRecord"].ToString())).ToString("0.00");
                }

                dCurrentTotalNetRecord = Convert.ToDouble(dt.Rows[0]["dCurrentTotalNetRecord"].ToString()).ToString("0.00");
                dTotalNetRecord = (Convert.ToDouble(dt.Rows[0]["dTotalNetRecord"].ToString())).ToString("0.00");

                dCurrentNewNetNum = (Convert.ToDouble(dt.Rows[0]["DCurrentNewNetNum"].ToString())).ToString("0.00");
            }
            else
            {
                currentTotalNetRecord = "0.00";
                currentOneMark = "0.00";
                level = " ";
                currentNewNetNum = "0";

            }



            StringBuilder XmlStr = new StringBuilder();
            XmlStr.Append("<FirstPage>");
            XmlStr.Append("<UserInfo>");

            //用户信息
            XmlStr.AppendFormat("<emailCount>{0}</emailCount>", emailCount);
            XmlStr.AppendFormat("<announcementCount>{0}</announcementCount>", announcementCount);
            //XmlStr.AppendFormat("<title>{0}</title>", name + " (" + petname + ") (" + number + ")");
            XmlStr.AppendFormat("<title>{0}</title>", name + " (" + number + ")");
            XmlStr.AppendFormat("<registerDate>{0}</registerDate>", registerDate);
            XmlStr.AppendFormat("<activeDate>{0}</activeDate>", activeDate);
            XmlStr.AppendFormat("<lastLoginDate>{0}</lastLoginDate>", lastLoginDate);
            XmlStr.AppendFormat("<level>{0}</level>", level);

            //业务信息
            XmlStr.AppendFormat("<currentNewNetNum>{0}</currentNewNetNum>", currentNewNetNum);
            XmlStr.AppendFormat("<newYeji>{0}</newYeji>", newYeji);
            XmlStr.AppendFormat("<totalYeji>{0}</totalYeji>", totalYeji);
            XmlStr.AppendFormat("<totalPeople>{0}</totalPeople>", totalPeople);

            //账户信息
            XmlStr.AppendFormat("<cashAccount>{0}</cashAccount>", cashAccount);
            XmlStr.AppendFormat("<consumerAccount>{0}</consumerAccount>", consumerAccount);

            //业绩信息
            XmlStr.AppendFormat("<currentTotalNetRecord>{0}</currentTotalNetRecord>", currentTotalNetRecord);
            XmlStr.AppendFormat("<currentOneMark>{0}</currentOneMark>", currentOneMark);
            XmlStr.AppendFormat("<totalNetNum>{0}</totalNetNum>", totalNetNum);
            XmlStr.AppendFormat("<dTotalNetNum>{0}</dTotalNetNum>", dTotalNetNum);
            XmlStr.AppendFormat("<currentOneMark1>{0}</currentOneMark1>", currentOneMark1);
            XmlStr.AppendFormat("<totalNetRecord>{0}</totalNetRecord>", totalNetRecord);
            XmlStr.AppendFormat("<dCurrentTotalNetRecord>{0}</dCurrentTotalNetRecord>", dCurrentTotalNetRecord);
            XmlStr.AppendFormat("<dTotalNetRecord>{0}</dTotalNetRecord>", dTotalNetRecord);
            XmlStr.AppendFormat("<dCurrentNewNetNum>{0}</dCurrentNewNetNum>", dCurrentNewNetNum);

            XmlStr.AppendFormat("<maxQishu>{0}</maxQishu>", CommonDataDAL.getMaxqishu());

            XmlStr.Append("</UserInfo>");
            XmlStr.Append("</FirstPage>");

            return XmlStr.ToString();
        }
        else
            return "请传入相关的参数";
    }

    /// <summary>
    /// 会员系统-首页-未读邮件
    /// </summary>
    /// <param name="bianhao"></param>
    /// <returns></returns>
    [WebMethod]
    public string Member_FirstPage_UnreadMsg(string bianhao)
    {
        if (IsExistsMember(bianhao))
        {
            string sql = GetEmailList(bianhao);
            DataTable dtEmail = DAL.DBHelper.ExecuteDataTable(sql);//DAL.DBHelper.ExecuteDataTable("select Number  from MemberInfo  where LoginPass='" + pwd + "'")

            /*
            //将数据集返回出去
            DataSet ds = new DataSet();
            ds.Tables.Add(dtEmail);
            return ds;
             * */

            StringBuilder XmlStr = new StringBuilder();
            XmlStr.Append("<FirstPage>");

            for (int i = 0; i < dtEmail.Rows.Count; i++)
            {
                XmlStr.Append("<PerformanceInfo>");
                XmlStr.AppendFormat("<ID>{0}</ID>", dtEmail.Rows[i]["ID"].ToString());
                XmlStr.AppendFormat("<InfoTitle>{0}</InfoTitle>", dtEmail.Rows[i]["InfoTitle"].ToString());
                XmlStr.AppendFormat("<Senddate>{0}</Senddate>", dtEmail.Rows[i]["Senddate"].ToString());
                XmlStr.Append("</PerformanceInfo>");
            }

            if (dtEmail.Rows.Count == 0)
            {
                XmlStr.Append("<PerformanceInfo>");
                XmlStr.Append("暂无未读邮件");
                XmlStr.Append("</PerformanceInfo>");
            }


            XmlStr.Append("</FirstPage>");

            return XmlStr.ToString();
        }
        else
            return "无此会员相关信息";
    }
    /// <summary>
    /// 会员系统-首页-最新公告
    /// </summary>
    /// <param name="bianhao"></param>
    /// <returns></returns>
    [WebMethod]
    public string Member_FirstPage_LatestNews(string bianhao)
    {
        if (IsExistsMember(bianhao))
        {
            string sql = GetAnnouncement(bianhao);
            DataTable dtGG = DAL.DBHelper.ExecuteDataTable(sql);

            /*
            //将数据集返回出去
            DataSet ds = new DataSet();
            ds.Tables.Add(dtGG);
            return ds;
             * */

            StringBuilder XmlStr = new StringBuilder();
            XmlStr.Append("<FirstPage>");

            for (int i = 0; i < dtGG.Rows.Count; i++)
            {
                XmlStr.Append("<PerformanceInfo>");
                XmlStr.AppendFormat("<ID>{0}</ID>", dtGG.Rows[i]["ID"].ToString());
                XmlStr.AppendFormat("<InfoTitle>{0}</InfoTitle>", dtGG.Rows[i]["InfoTitle"].ToString());
                XmlStr.AppendFormat("<Senddate>{0}</Senddate>", dtGG.Rows[i]["Senddate"].ToString());
                XmlStr.Append("</PerformanceInfo>");
            }

            if (dtGG.Rows.Count == 0)
            {
                XmlStr.Append("<PerformanceInfo>");
                XmlStr.Append("暂无最新公告");
                XmlStr.Append("</PerformanceInfo>");
            }

            XmlStr.Append("</FirstPage>");

            return XmlStr.ToString();
        }
        else
            return "无此会员相关信息";
    }
    #endregion

    #region 团队结构
    /// <summary>
    /// 会员系统-团队结构-常用网络图
    /// </summary>
    /// <param name="bianhao"></param>
    /// <param name="type"></param>
    /// <param name="qishu"></param>
    /// <param name="topbianhao"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string Member_TeamStructure_NetWork(string bianhao, string type, int qishu, string topbianhao)
    {
        if (IsExistsMember(bianhao))
        {

            if (DAL.MemberInfoDAL.SelectMemberExist(bianhao) == false)
            {
                return "您不能查看该网络";
            }

            if (topbianhao != bianhao)
            {
                string fatherBh = bianhao;
                string sonBh = topbianhao;
                bool flag = new BLL.Registration_declarations.RegistermemberBLL().isNet(type, fatherBh, sonBh);
                if (!flag)
                {
                    return "您不能查看该网络";
                }
            }

            //SqlDataReader sdr = DAL.DBHelper.ExecuteReader("Select Top 1 ID,languageCode From Language where languageCode='L001'");
            //while (sdr.Read())
            //{
            //    Session["languageCode"] = sdr["languageCode"].ToString().Trim();
            //    Session["LanguageID"] = sdr["ID"].ToString();
            //}
            //sdr.Close();
            LoadLanguage();//默认语言指定

            string networkstring = "";
            if (type == "az")
            {
                networkstring = JieGouNew2.Direct_Table_New(bianhao, qishu, 1);
            }
            else
            {
                networkstring = JieGouNew2.Direct_Table_New(bianhao, qishu, 2);
            }
            return networkstring;
        }
        else
            return "无此会员相关信息";

    }
    #endregion

    #region 报单管理

    #region 注册新人
    /// <summary>
    /// 生成会员编号
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public string GetNewNumber()
    {
        //新会员编号
        string newNumber = AjaxClass.GetMemberNumber();

        return newNumber;
    }
    /// <summary>
    /// 证件类型
    /// </summary>
    /// <returns></returns>
    [WebMethod(EnableSession=true)]
    public string LoadCardType()
    {
        StringBuilder XmlStr = new StringBuilder();

        int id = 1;

        LoadLanguage();

        List<Bsco_PaperType> list = new AddMemberInfomDAL().GetCard();
        foreach (Bsco_PaperType model in list)
        {
            XmlStr.Append("<PTInfo id='" + id + "'>");
            XmlStr.AppendFormat("<PaperTypeCode>{0}</PaperTypeCode>", model.PaperTypeCode);
            XmlStr.AppendFormat("<PaperType>{0}</PaperType>", CommonDataBLL.GetLanguageStr(model.Id, "bsco_PaperType", "papertype"));
            XmlStr.Append("</PTInfo>");

            id++;
        }

        return XmlStr.ToString();
    }
    /// <summary>
    /// 身份证验证
    /// </summary>
    /// <param name="zjNumber"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string VerifyPaperNumber(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }


            string[] str_parmeters = canshus.Split(','); //分解参数

            string zjNumber = str_parmeters[0]; //证件号码

            LoadLanguage();//默认语言指定

            return new AjaxClass().VerifyPaperNumber(zjNumber);

        }
        else
            return "请传入相关的参数";
    }
    /// <summary>
    /// 推荐编号验证
    /// </summary>
    /// <param name="canshus">加密的参数</param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string CheckDirect(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }

            string[] str = canshus.Split(',');
            if (str.Length == 2)
            {

                string bianhao = str[0]; //登陆的会员编号
                string direct = str[1];  //注册会员时填写的推荐人编号

                LoadLanguage();//默认语言指定


                if (direct.Trim().Length == 0)
                    return "推荐编号不能为空";

                if (!MemberIsExists(direct))
                    return "推荐编号不存在";

                string error = new AjaxClass().CheckNumberNetAn(bianhao, direct);
                if (error.Trim().Length > 0)
                    return "推荐编号不在登陆会员的安置团队内！";
                else
                    return "OK-" + new AjaxClass().getpetnamelevelstr(direct);

            }
            else
                return "传入的参数有误";
        }
        else
            return "请传入相关参数";
    }
    /// <summary>
    /// 验证安置编号
    /// </summary>
    /// <param name="canshus">加密的参数</param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string CheckPlacement(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus); //参数解密
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }

            string bianhao = ""; //登陆的会员编号
            string number = "";    //注册时，生成的会员编号
            string direct = "";    //注册会员时填写的推荐人编号
            string placement = "";   //注册会员时填写的安置人编号

            LoadLanguage();//默认语言指定

            string[] str = canshus.Split(',');
            if (str.Length == 4)
            {
                LoadLanguage();//默认语言指定

                bianhao = str[0];
                number = str[1];
                direct = str[2];
                placement = str[3];

                if (direct.Trim().Length == 0)
                    return "推荐编号不能为空";

                if (placement.Trim().Length == 0)
                    return "安置编号不能为空";

                if (!MemberIsExists(direct))
                    return "推荐编号不存在";

                if (!MemberIsExists(placement))
                    return "安置编号不存在";

                string GetError1 = new AjaxClass().CheckNumberNetAn(direct, placement);
                if (GetError1 != null && GetError1 != "")
                    return "安置编号必须在推荐编号的安置网络下面！";

                string pla = new RegistermemberBLL().GetHavePlacedOrDriect(number, "", placement, direct);
                if (pla != null)
                    return pla;

                return "OK-" + new AjaxClass().getpetnamelevelstr(placement);
            }
            else
                return "传入的参数有误";
        }
        else
            return "请传入相关参数";
    }
    /// <summary>
    /// 注册下一步验证
    /// </summary>
    /// <param name="canshus">加密的参数</param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string regMemberNext(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {

            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }

            string bianhao = ""; //登陆的会员编号 即 协助注册人
            string number = ""; //会员编号,生成的新会员编号
            string name = "";    //真实姓名
            string petname = "";  //昵称
            string storeid = "";   //所属服务机构
            string zjtype = "";   //证件类型
            string zjnumber = "";   //证件号码
            string birthdate = "";   //出生日期
            string sex = "";     //性别
            string mobiletele = "";   //移动电话
            string emial = "";  //电子邮件
            string ctry = "";    //联系地址-国家
            string prov = "";    //联系地址-省份
            string city = "";    //联系地址-城市
            string xian = "";    //联系地址-区县
            string addr = ""; //详细地址
            string direct = ""; //推荐人编号
            string placement = ""; //安置人编号

            if (canshus.Trim().Length > 0)
            {
                string[] str_parm = canshus.Split(',');
                if (str_parm.Length == 18)
                {
                    //参数解析
                    bianhao = CommonDataBLL.quanjiao(str_parm[0]);
                    number = CommonDataBLL.quanjiao(str_parm[1]);
                    name = str_parm[2];
                    petname = str_parm[3];
                    storeid = CommonDataBLL.quanjiao(str_parm[4]);
                    zjtype = str_parm[5];
                    zjnumber = CommonDataBLL.quanjiao(str_parm[6]);
                    birthdate = str_parm[7];
                    sex = str_parm[8];
                    mobiletele = str_parm[9];
                    emial = str_parm[10];
                    ctry = str_parm[11];
                    prov = str_parm[12];
                    city = str_parm[13];
                    xian = str_parm[14];
                    addr = str_parm[15];
                    direct = CommonDataBLL.quanjiao(str_parm[16]);
                    placement = CommonDataBLL.quanjiao(str_parm[17]);


                    LoadLanguage();//默认语言指定


                    RegistermemberBLL registermemberBLL = new RegistermemberBLL();

                    //会员名是否小于6位
                    if (!registermemberBLL.NumberLength(CommonDataBLL.quanjiao(number)))
                    {
                        return "抱歉！您输入的会员编号小于6位！";
                    }
                    if (!registermemberBLL.NumberCheckAgain(CommonDataBLL.quanjiao(number)))
                    {
                        return "编号请输入字母，数字，横线！";
                    }
                    if (name == "")
                    {
                        return "真实姓名不能为空！";
                    }
                    if (storeid == "")
                    {
                        return "所属店铺不能为空！";
                    }
                    else
                    {
                        if (!StoreInfoDAL.CheckStoreId(storeid))
                        {
                            return "所属店铺编号不存在！";
                        }
                    }
                    //判断用胡地址是否输入
                    if (DAL.CommonDataDAL.GetCPCCode(ctry, prov, city, xian) == "")
                    {
                        return "对不起，请选择国家省份城市！";
                    }
                    if (direct == "" || placement == "")
                    {
                        return "推荐编号和安置编号不能为空！";
                    }

                    if (direct == number)
                    {
                        return "推荐编号不能与会员编号相同";
                    }

                    if (placement == number)
                    {
                        return "安置编号不能与会员编号相同";
                    }

                    if (zjtype != "2")
                    {
                        //验证年龄是否大于18岁
                        string alert = registermemberBLL.AgeIs18(birthdate);
                        if (alert != null)
                        {
                            return alert;
                        }

                        //检查会员生日
                        if (registermemberBLL.CheckBirthDay(birthdate) == "error")
                        {
                            return "对不起，请选择正确的出生日期！";
                        }
                    }


                    //检测身份证需要新方法
                    string CardResult = "";
                    if (zjtype == "2")
                    {
                        string result = BLL.Registration_declarations.CheckMemberInfo.CHK_IdentityCard(CommonDataBLL.quanjiao(zjnumber));
                        if (result.IndexOf(",") <= 0)
                        {
                            return result;
                        }
                        else
                        {
                            CardResult = result;
                        }
                        DateTime birthday = Convert.ToDateTime(CardResult.Substring(0, CardResult.IndexOf(",")));
                        string alerta = registermemberBLL.AgeIs18(birthday.ToString());
                        if (alerta != null)
                        {
                            return alerta;
                        }
                    }

                    //验证会员编号是否重复
                    if (registermemberBLL.CheckNumberTwice(CommonDataBLL.quanjiao(number)) != null)
                    {
                        return "抱歉！该会员编号重复！";
                    }


                    if (DBHelper.ExecuteScalar("select count(0) from memberinfo where placement='" + placement + "' and District=" + direct + "").ToString() != "0")
                    {
                        return "安置人所选区位已有人安置！";
                    }

                    //注册会员检错1.无上级  2.无此店  3..死循环
                    string CheckMember = registermemberBLL.CheckMemberInProc(number, placement, direct, storeid);
                    CheckMember = new GroupRegisterBLL().GerCheckErrorInfo(CheckMember);


                    if (CheckMember != null)
                    {
                        return CheckMember;
                    }

                    string placement_err = registermemberBLL.GetHavePlacedOrDriect(number, "", placement, direct);
                    if (placement_err != null)
                    {
                        return placement_err;
                    }



                    //判断该编号是否有安置，推荐
                    string GetError = registermemberBLL.GetError(direct, placement);
                    if (GetError != null)
                    {
                        return GetError;
                    }
                    string GetError1 = new AjaxClass().CheckNumberNetAn(direct, placement);
                    if (GetError1 != null && GetError1 != "")
                    {
                        return "安置编号必须在推荐编号的安置网络下面！";
                    }

                    return "Next";

                }
                else
                    return "传入的参数有误";
            }
            else
                return "请传入相关参数";
        }
        else
            return "请传入相关参数";
    }
    #endregion

    #region 注册浏览
    /// <summary>
    /// 半隐身份证号
    /// </summary>
    /// <param name="papernumber"></param>
    /// <returns></returns>
    protected string GetPapernumber(string papernumber)
    {
        if (papernumber.Length > 6)
        {
            return papernumber.Substring(0, 4) + "******" + papernumber.Substring(papernumber.Length - 2, 2);
        }
        else
        {
            return papernumber;
        }
    }
    /// <summary>
    /// 注册浏览
    /// </summary>
    /// <param name="canshus"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string ShowRegInfo(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }

            string[] str_parm = canshus.Split(',');

            string bianhao = str_parm[0];    //会员编号
            string qishu = str_parm[1]; //期数：全部 -1
            string cond_columns = str_parm[2]; //其他列
            string opersymbol = str_parm[3]; //运算符号
            string value = str_parm[4]; //查询内容
            string defraystate = str_parm[5]; //支付状态：全部 -1   未支付 0   已支付 1


            string conditions = ""; //查询条件

            if (qishu != "-1")
            {
                conditions += " and B.OrderExpectNum=" + qishu;
            }
            if (value.Trim().Length > 0)
            {
                conditions += " and " + cond_columns + " " + opersymbol + " '%" + value + "%'";
            }
            if (defraystate != "-1")
            {
                conditions += " and  DefrayState=" + defraystate;
            }


            string key = "A.registerDate";
            string table = "MemberInfo as A,MemberOrder as B,city c";
            string column = "B.SendWay,A.ID,A.Number,b.OrderID,A.StoreID,A.Name,A.PetName,case when B.Error='' then '0' end as Error,B.totalMoney,B.totalPv,B.OrderExpectNum,B.PayExpectNum,B.defraytype"
                                + " ,A.RegisterDate,case B.DefrayState when 1 then B.ReceivablesDate else '' end ReceivablesDate,A.Remark,B.ordertype ,B.ordertype ,c.country,c.province,city,xian,  B.defraystate,a.mobiletele, a.papertypecode,papernumber,sendway,sendtype,direct,placement,"
                                + " B.DefrayState,B.lackproductmoney";
            string condition = " B.Number=A.Number and c.cpccode=a.cpccode and assister='" + bianhao + "' and isagain=0 ";

            if (conditions.Trim().Length > 0)
                condition += conditions;

            string sql = "select " + column + "  from " + table + "  where " + condition + "  order by " + key + " desc";
            DataTable dt = DAL.DBHelper.ExecuteDataTable(sql);

            if (dt == null || dt.Rows.Count <= 0)
            {
                return "暂无数据";
            }

            int i_id = 1;

            StringBuilder XmlStr = new StringBuilder();
            XmlStr.Append("<RegisteredBrowse>");

            LoadLanguage();//默认语言指定

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                XmlStr.Append("<RBInfo id=\"" + i_id + "\">");

                XmlStr.AppendFormat("<number>{0}</number>", dt.Rows[i]["Number"].ToString());
                XmlStr.AppendFormat("<name>{0}</name>", dt.Rows[i]["Name"].ToString());
                XmlStr.AppendFormat("<petname>{0}</petname>", dt.Rows[i]["PetName"].ToString());
                XmlStr.AppendFormat("<direct>{0}</direct>", dt.Rows[i]["Direct"].ToString());
                XmlStr.AppendFormat("<placement>{0}</placement>", dt.Rows[i]["Placement"].ToString());

                XmlStr.AppendFormat("<zjtype>{0}</zjtype>", Common.GetPapertype(dt.Rows[i]["papertypecode"].ToString(), "L001"));
                XmlStr.AppendFormat("<papernumber>{0}</papernumber>", GetPapernumber(dt.Rows[i]["papernumber"].ToString()));
                XmlStr.AppendFormat("<state>{0}</state>", dt.Rows[i]["defraystate"].ToString() == "1" ? "已激活" : "未激活");
                XmlStr.AppendFormat("<sendWay>{0}</sendWay>", Common.GetSendWay(dt.Rows[i]["SendWay"].ToString()));
                XmlStr.AppendFormat("<orderexpectnum>{0}</orderexpectnum>", dt.Rows[i]["orderexpectnum"].ToString());
                XmlStr.AppendFormat("<orderId>{0}</orderId>", dt.Rows[i]["OrderID"].ToString());

                XmlStr.AppendFormat("<registerDate>{0}</registerDate>", dt.Rows[i]["RegisterDate"].ToString());
                XmlStr.AppendFormat("<receivablesDate>{0}</receivablesDate>", GetBiaoZhunTime(dt.Rows[i]["ReceivablesDate"].ToString()));

                XmlStr.Append("</RBInfo>");

                i_id++;
            }

            XmlStr.Append("</RegisteredBrowse>");

            return XmlStr.ToString();
        }
        else
            return "请传入相关参数";
    }
    #endregion

    #region 复消报单
    /// <summary>
    /// 加载产品【注意有图片的路径】
    /// </summary>
    /// <param name="canshus"></param>
    /// <returns></returns>
    [WebMethod]
    public string LoadProducts(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }

            //参数分割
            string[] str_parm = canshus.Split(',');

            string type = str_parm[0];  //1 注册报单  2 复消报单
            string pid = str_parm[1];  //产品类别ProductID

            //产品
            string sql = "select productid,productname,PreferentialPrice,PreferentialPV,isfold,pid  from product  where pid=" + pid;

            if (type == "1")
                sql += " and (yongtu=0 or yongtu=1)";
            else if (type == "2")
                sql += " and (yongtu=0 or yongtu=2)";

            DataTable dt = DAL.DBHelper.ExecuteDataTable(sql);

            int prdClass_Id = 1;
            int prd_Id = 1;

            StringBuilder XmlStr = new StringBuilder();
            XmlStr.Append("<ProductsInfo>");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["isfold"].ToString() == "1")
                {
                    XmlStr.Append("<prdClass id=\"" + prdClass_Id + "\">");
                    XmlStr.AppendFormat("<prductid>{0}</prductid>", dt.Rows[i]["productid"].ToString());
                    XmlStr.AppendFormat("<productname>{0}</productname>", dt.Rows[i]["productname"].ToString());
                    XmlStr.AppendFormat("<pid>{0}</pid>", dt.Rows[i]["pid"].ToString());
                    XmlStr.Append("</prdClass>");

                    prdClass_Id++;
                }
                else
                {
                    XmlStr.Append("<prd id=\"" + prd_Id + "\">");
                    XmlStr.AppendFormat("<prductid>{0}</prductid>", dt.Rows[i]["productid"].ToString());
                    XmlStr.AppendFormat("<productname>{0}</productname>", dt.Rows[i]["productname"].ToString());
                    XmlStr.AppendFormat("<preferentialPrice>{0}</preferentialPrice>", dt.Rows[i]["PreferentialPrice"].ToString());
                    XmlStr.AppendFormat("<preferentialPV>{0}</preferentialPV>", dt.Rows[i]["PreferentialPV"].ToString());
                    XmlStr.AppendFormat("<pid>{0}</pid>", dt.Rows[i]["pid"].ToString());
                    XmlStr.AppendFormat("<imgUrl>{0}</imgUrl>", "http://192.168.1.254/DS2014_MVS/ReadImage.aspx?ProductID=" + dt.Rows[i]["productid"]);
                    XmlStr.Append("</prd>");

                    prd_Id++;
                }
            }

            XmlStr.Append("</ProductsInfo>");

            return XmlStr.ToString();

        }
        else
            return "请传入相关参数";

    }
    /// <summary>
    /// 提交订单
    /// </summary>
    /// <param name="canshus">加密的参数</param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string SubmitOrders(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }


            if (canshus.Length > 0)
            {
                string[] str_parm = canshus.Split(',');
                if (str_parm.Length == 29)
                {
                    LoadLanguage();//默认语言指定

                    //新会员相关信息
                    string bianhao = "";  //登陆的会员编号 即 协助注册人
                    string number = "";  //会员编号,生成的新会员编号
                    string name = "";   // 真实姓名
                    string petname = "";  //昵称
                    string storeid = "";  //所属服务机构
                    string zjtype = "";   //证件类型
                    string zjnumber = "";  //证件号码
                    string birthdate = "";  //出生日期
                    string sex = "";  //性别
                    string mobiletele = "";  // 移动电话
                    string emial = "";  //电子邮件
                    string ctry = "";     //联系地址-国家
                    string prov = "";     //联系地址-省份
                    string city = "";     //联系地址-城市
                    string xian = "";     //联系地址-区县
                    string addr = "";  // 详细地址
                    string direct = "";  // 推荐人编号
                    string placement = "";  // 安置人编号

                    //注册单相关信息
                    string consigneeName = "";  //收货人姓名
                    string consigneeMobile = "";//收货人电话
                    string consigneeCPCCode = "";  //收货地址，格式：国家-省份-城市-区县
                    string consigneeAddr = "";    //详细地址
                    string sendtype = "";   //运货方式
                    string sendway = "";        //收货途径
                    string fee = "";         //快递费用
                    string ord_detail = "";  //订购的详细产品及数量，格式：ProductID1-ProductID2-ProductID3/Quantity1-Quantity2-Quantity3

                    //操作类型
                    string operateType = ""; //0 报单   1 修改订单
                    string orderid = "";
                    string ordType = ""; //0 公司注册单  1 店铺注册单  2 会员注册单  
                                         //3 公司复消单  4 店铺复消单  5 会员复消单
                                         //6 公司升级单  7 店铺升级单  8 会员升级单

                    //新会员相关信息
                    bianhao = CommonDataBLL.quanjiao(str_parm[0]);
                    number = CommonDataBLL.quanjiao(str_parm[1]);
                    name = str_parm[2];
                    petname = str_parm[3];
                    storeid = CommonDataBLL.quanjiao(str_parm[4]);
                    zjtype = str_parm[5];
                    zjnumber = CommonDataBLL.quanjiao(str_parm[6]);
                    birthdate = str_parm[7];
                    sex = str_parm[8];
                    mobiletele = str_parm[9];
                    emial = str_parm[10];
                    ctry = str_parm[11];
                    prov = str_parm[12];
                    city = str_parm[13];
                    xian = str_parm[14];
                    addr = str_parm[15];
                    direct = CommonDataBLL.quanjiao(str_parm[16]);
                    placement = CommonDataBLL.quanjiao(str_parm[17]);

                    //注册单相关信息
                    consigneeName = str_parm[18];
                    consigneeMobile = str_parm[19];
                    consigneeCPCCode = str_parm[20];
                    consigneeAddr = str_parm[21];
                    sendtype = str_parm[22];
                    sendway = str_parm[23];
                    fee = str_parm[24];
                    ord_detail = str_parm[25];

                    operateType = str_parm[26]; //0 报单   1 修改订单
                    orderid = str_parm[27];
                    ordType = str_parm[28];


                    if (ord_detail == "")
                    {
                        return "请至少选择一种产品";
                    }


                    string[] str_consigneeCPCCode = consigneeCPCCode.Split('-');

                    RegistermemberBLL registermemberBLL = new RegistermemberBLL();
                    string CardResult = "";
                    if (ordType == "0" || ordType == "1" || ordType == "2") //注册
                    {

                        //会员名是否小于6位
                        if (!registermemberBLL.NumberLength(CommonDataBLL.quanjiao(number)))
                        {
                            return "抱歉！您输入的会员编号小于6位！";
                        }
                        if (!registermemberBLL.NumberCheckAgain(CommonDataBLL.quanjiao(number)))
                        {
                            return "编号请输入字母，数字，横线！";
                        }
                        if (name == "")
                        {
                            return "真实姓名不能为空！";
                        }

                        if (storeid == "")
                        {
                            return "所属店铺不能为空！";
                        }
                        else
                        {
                            if (!StoreInfoDAL.CheckStoreId(storeid))
                            {
                                return "所属店铺编号不存在！";
                            }
                        }
                        //判断用户地址是否输入
                        if (ctry == "" || ctry == "请选择" || prov == "" || prov == "请选择" || city == "" || city == "请选择" || xian == "" || xian == "请选择")
                        {
                            return "对不起，请选择国家省份城市！";
                        }
                        if (DAL.CommonDataDAL.GetCPCCode(ctry, prov, city, xian) == "")
                        {
                            return "对不起，请选择国家省份城市！";
                        }
                        if (addr == "")
                        {
                            return "对不起，请填写详细地址！";
                        }
                        if (direct == "" || placement == "")
                        {
                            return "推荐编号或安置编号都不能为空";
                        }

                        if (direct == number)
                        {
                            return "推荐编号不能与会员编号相同";
                        }

                        if (placement == number)
                        {
                            return "安置编号不能与会员编号相同";
                        }


                        if (consigneeName == "")
                        {
                            return "请填写收货人姓名";
                        }
                        if (consigneeMobile == "")
                        {
                            return "收货人电话";
                        }
                        if (str_consigneeCPCCode[0] == "" || str_consigneeCPCCode[0] == "请选择" || str_consigneeCPCCode[1] == "" || str_consigneeCPCCode[1] == "请选择" || str_consigneeCPCCode[2] == "" || str_consigneeCPCCode[2] == "请选择" || str_consigneeCPCCode[3] == "" || str_consigneeCPCCode[3] == "请选择")
                        {
                            return "请选择收货地址";
                        }
                        if (consigneeAddr == "")
                        {
                            return "详细地址";
                        }


                        //验证会员编号是否重复
                        if (registermemberBLL.CheckNumberTwice(CommonDataBLL.quanjiao(number)) != null)
                        {
                            return "抱歉！该会员编号重复！";
                        }

                        //验证年龄是否大于18岁
                        if (zjtype != "2")
                        {
                            string alert = registermemberBLL.AgeIs18(birthdate);
                            if (alert != null)
                            {
                                return alert;
                            }
                        }

                        //检查会员生日
                        if (zjtype != "2")
                        {
                            if (registermemberBLL.CheckBirthDay(birthdate) == "error")
                            {
                                return "对不起，请选择正确的出生日期！";
                            }
                        }


                        //检测身份证需要新方法
                        if (zjtype == "2")
                        {
                            string result = BLL.Registration_declarations.CheckMemberInfo.CHK_IdentityCard(CommonDataBLL.quanjiao(zjnumber));
                            if (result.IndexOf(",") <= 0)
                            {
                                return result;
                            }
                            else
                            {
                                CardResult = result;
                            }
                            DateTime birthday = Convert.ToDateTime(CardResult.Substring(0, CardResult.IndexOf(",")));
                            string alerta = registermemberBLL.AgeIs18(birthday.ToString());
                            if (alerta != null)
                            {
                                return alerta;
                            }
                        }

                        //是否存在 推荐/安置 编号
                        if (!MemberInfoDAL.IsMemberExist(direct))
                        {
                            return "该推荐编号不存在！";
                        }
                        if (!MemberInfoDAL.IsMemberExist(placement))
                        {
                            return "该安置编号不存在！";
                        }

                        string error = new AjaxClass().CheckNumberNetAn(bianhao, direct);
                        if (error.Trim().Length > 0)
                        {
                            return "推荐编号不在登陆会员的安置团队内！";
                        }

                        //指定安置人下的位置
                        if (DBHelper.ExecuteScalar("select count(0) from memberinfo where placement='" + placement + "' and District=" + direct + "").ToString() != "0")
                        {
                            return "安置人所选区位已有人安置！";
                        }

                        //注册会员检错1.无上级  2.无此店  3..死循环
                        string CheckMember = registermemberBLL.CheckMemberInProc(number, placement, direct, storeid);
                        CheckMember = new GroupRegisterBLL().GerCheckErrorInfo(CheckMember);
                        if (CheckMember != null)
                        {
                            return CheckMember;
                        }

                        string pla = registermemberBLL.GetHavePlacedOrDriect(number, "", placement, direct);
                        if (pla != null)
                        {
                            return pla;
                        }


                        //判断该编号是否有安置，推荐
                        string GetError = registermemberBLL.GetError(direct, placement);
                        if (GetError != null)
                        {
                            return GetError;
                        }
                        string GetError1 = new AjaxClass().CheckNumberNetAn(direct, placement);
                        if (GetError1 != null && GetError1 != "")
                        {
                            return "安置编号必须在推荐编号的安置网络下面！";
                        }
                    }


                    ///对象赋值

                    OrderFinalModel ofm = new OrderFinalModel();

                    if (ordType == "0" || ordType == "1" || ordType == "2") //注册
                    {
                        ofm.Number = number;
                        ofm.StoreID = storeid;
                    }
                    else //非注册
                    {
                        ofm.Number = bianhao;
                        ofm.StoreID = GetMemberOfStoreID(ofm.Number);
                    }

                    //会员注册信息
                    ofm.Name = Encryption.Encryption.GetEncryptionName(name);
                    ofm.Placement = placement;
                    ofm.Direct = direct;
                    ofm.ExpectNum = BLL.CommonClass.CommonDataBLL.getMaxqishu();
                    ofm.PetName = petname == "" ? "昵称" : (CommonDataBLL.quanjiao(petname));
                    ofm.LoginPass = Encryption.Encryption.GetEncryptionPwd(ofm.Number, ofm.Number);
                    ofm.AdvPass = Encryption.Encryption.GetEncryptionPwd(ofm.Number, ofm.Number);
                    ofm.LevelInt = 1;//会员级别
                    ofm.RegisterDate = DateTime.UtcNow;
                    ofm.Assister = bianhao;
                    ofm.OfficeTele = "";
                    ofm.HomeTele = "";
                    ofm.MobileTele = mobiletele;
                    ofm.FaxTele = "";
                    ofm.City.Country = ctry;
                    ofm.City.Province = prov;
                    ofm.City.City = city;
                    ofm.City.Xian = xian;
                    ofm.CPCCode = DAL.CommonDataDAL.GetCPCCode(ctry, prov, city, xian);
                    ofm.Address = Encryption.Encryption.GetEncryptionAddress(CommonDataBLL.quanjiao(addr));
                    ofm.PostalCode = "";
                    if (ordType == "0" || ordType == "1" || ordType == "2") //注册
                    {
                        ofm.PaperType.Id = Convert.ToInt32(zjtype);
                        ofm.PaperType = new Bsco_PaperType(Convert.ToInt32(zjtype));
                        string paperCode = BLL.CommonClass.CommonDataBLL.getPaperType(Convert.ToInt32(zjtype));
                        ofm.PaperType.PaperTypeCode = paperCode.Trim(); //证件类型
                        ofm.PaperNumber = Encryption.Encryption.GetEncryptionNumber(CommonDataBLL.quanjiao(zjnumber));
                    }
                    else
                    {
                        ofm.PaperType.Id = 0;
                        ofm.PaperType.PaperTypeCode = ""; //证件类型
                        ofm.PaperNumber = "";
                    }
                    if (ordType == "0" || ordType == "1" || ordType == "2") //注册
                    {
                        if (zjtype == "2")
                        {
                            ofm.Sex = CardResult.Substring(CardResult.IndexOf(",") + 1).Trim() == "男" ? (1) : (0);
                            ofm.Birthday = Convert.ToDateTime(CardResult.Substring(0, CardResult.IndexOf(",")));
                        }
                        else
                        {
                            ofm.Sex = Convert.ToInt32(sex);
                            ofm.Birthday = Convert.ToDateTime(CommonDataBLL.quanjiao(birthdate));
                        }

                        if (Convert.ToInt32(DAL.DBHelper.ExecuteScalar("select COUNT(0)  from MemberInfo where Placement='" + placement + "' and District=1")) == 0)
                            ofm.District = 1;
                        else
                            ofm.District = 2;
                    }
                    else
                    {
                        ofm.Sex = 0;
                        ofm.Birthday = DateTime.Now;
                    }
                    ofm.BankCode = "";
                    ofm.BankAddress = "";
                    ofm.BankBook = "";
                    ofm.BankCard = "";
                    ofm.BCPCCode = DAL.CommonDataDAL.GetCPCCode("", "", "", "");
                    ofm.Remark = "";
                    ofm.ChangeInfo = "";
                    ofm.Email = emial;
                    ofm.Answer = "";
                    ofm.Question = "";
                    ofm.IsBatch = 0;//不是批量注册  modify
                    ofm.Language = 1;
                    ofm.OperateIp = CommonDataBLL.OperateIP;//调用方法
                    ofm.OperaterNum = CommonDataBLL.OperateBh;//调用方法
                    ofm.Error = "";
                    ofm.Bankbranchname = "";
                    ofm.CCPCCode = DAL.CommonDataDAL.GetCPCCode(ctry, prov, city, xian);
                    ofm.Type = Convert.ToInt32(sendtype);
                    ofm.IsAgain = 0;
                    ofm.OrderType = 0;
                    ofm.PhotoPath = "";
                    ofm.RegisterDate = DateTime.UtcNow;
                    ofm.OrderDate = DateTime.UtcNow;


                    //订单相关信息
                    if (orderid.Trim().Length > 0)
                        ofm.OrderID = orderid;
                    else
                        ofm.OrderID = registermemberBLL.GetOrderInfo("add", null);

                    ofm.CCPCCode = DAL.CommonDataDAL.GetCPCCode(str_consigneeCPCCode[0], str_consigneeCPCCode[1], str_consigneeCPCCode[2], str_consigneeCPCCode[3]);
                    ofm.ConAddress = Encryption.Encryption.GetEncryptionAddress(consigneeAddr);

                    ofm.ConTelPhone = "";
                    ofm.ConMobilPhone = consigneeMobile;
                    ofm.ConPost = "";
                    ofm.Consignee = Encryption.Encryption.GetEncryptionName(consigneeName);
                    ofm.ConZipCode = "";

                    ofm.CarryMoney = 0;

                    ofm.OrderExpect = CommonDataBLL.getMaxqishu();
                    ofm.StandardcurrencyMoney = ofm.TotalMoney;
                    ofm.PaymentMoney = ofm.TotalMoney;

                    ofm.RemittancesId = "";
                    ofm.ElectronicaccountId = "";

                    //订购产品信息
                    string[] str_details = ord_detail.Split('/');
                    if (str_details[0].Split('-').Length != str_details[1].Split('-').Length)
                        return "产品信息不正确";
                    else
                    {
                        ofm.ProductIDList = str_details[0];
                        ofm.QuantityList = str_details[1];
                        ofm.NotEnoughProductList = ofm.ProductIDList;
                    }

                    //0 公司注册单  1 店铺注册单  2 会员注册单  
                    //3 公司复消单  4 店铺复消单  5 会员复消单
                    //6 公司升级单  7 店铺升级单  8 会员升级单
                    if (ordType == "0")
                    {
                        ofm.OrderType = 31;
                    }
                    else if (ordType == "1")
                    {
                        ofm.OrderType = 11;
                    }
                    else if (ordType == "2")
                    {
                        ofm.OrderType = 21;
                    }
                    else if (ordType == "4")
                    {
                        ofm.OrderType = 12;
                    }
                    else if (ordType == "5")
                    {
                        ofm.OrderType = 22;
                    }

                    ofm.PayExpect = -1;
                    if (ordType == "0" || ordType == "1" || ordType == "2") //注册
                        ofm.IsAgain = 0;
                    else
                        ofm.IsAgain = 1;
                    ofm.OrderDate = DateTime.UtcNow;
                    ofm.DefrayState = 0; //未支付

                    ofm.RemittancesId = "";
                    ofm.ElectronicaccountId = "";
                    ofm.DefrayType = -1;
                    ofm.PayCurrency = 1;
                    ofm.PayMoney = 0;
                    ofm.CarryMoney = 0;
                    ofm.IsreceiVables = 0;
                    ofm.IsRetail = 0;
                    ofm.DeclareMoney = 0;
                    ofm.PaymentMoney = 0;


                    //总金额 总PV
                    DataTable dt_buy = new DataTable(); //产品明细
                    dt_buy.Columns.Add("proId", typeof(System.Int32)); //购买产品
                    dt_buy.Columns.Add("proNum", typeof(System.Int32));//购买数量
                    DataRow dr;
                    DataTable dt_price_pv;
                    string[] prds = ofm.ProductIDList.Split('-');
                    string[] quat = ofm.QuantityList.Split('-');
                    for (int i = 0; i < prds.Length; i++)
                    {
                        if (quat[i].Trim().Length > 0)
                        {
                            dt_price_pv = DAL.DBHelper.ExecuteDataTable("SELECT PreferentialPrice,PreferentialPV  FROM Product  where ProductID=" + prds[i]);
                            if (dt_price_pv != null && dt_price_pv.Rows.Count > 0)
                            {
                                ofm.TotalMoney += Convert.ToDecimal(MemberOrderAgainBLL.GetBzMoney(ofm.StoreID, Convert.ToDouble(dt_price_pv.Rows[0]["PreferentialPrice"]) * Convert.ToInt32(quat[i])));
                                ofm.TotalPv = Convert.ToDecimal(Convert.ToDouble(dt_price_pv.Rows[0]["PreferentialPV"]) * Convert.ToInt32(quat[i]));
                            }

                            dr = dt_buy.NewRow();
                            dr["proId"] = Convert.ToInt32(prds[i]);
                            dr["proNum"] = Convert.ToInt32(quat[i]);
                            dt_buy.Rows.Add(dr);
                        }
                    }

                    if (ofm.SendWay == 0)
                    {
                        IList<MemberDetailsModel> choseProList = new OrderDeal().AddMemberDetails(dt_buy);
                        ofm.EnoughProductMoney = Convert.ToDecimal(registermemberBLL.getEnoughProductMoney(choseProList, ofm.StoreID));
                        double notEnoughmoney = registermemberBLL.CheckMoneyIsEnough(choseProList, ofm.StoreID);
                        ofm.LackProductMoney = Convert.ToDecimal(registermemberBLL.ChangeNotEnoughMoney(ofm.StoreID, notEnoughmoney));
                    }
                    else
                    {
                        ofm.EnoughProductMoney = 0;
                        ofm.LackProductMoney = ofm.TotalMoney;
                    }

                    if (prds.Length == 0)
                    {
                        ofm.TotalMoney = 0;
                        ofm.TotalPv = 0;

                        ofm.ProductIDList = ",";
                        ofm.QuantityList = ",";
                        ofm.NotEnoughProductList = ",";
                    }

                    ofm.StandardCurrency = MemberOrderAgainBLL.GetBzTypeId(ofm.StoreID);
                    ofm.StandardcurrencyMoney = ofm.TotalPv;

                    ofm.ProductIDList = ofm.ProductIDList.Replace("-", ",") + ",";
                    ofm.QuantityList = ofm.QuantityList.Replace("-", ",") + ",";
                    ofm.NotEnoughProductList = ofm.ProductIDList;



                    //处理逻辑
                    Boolean flag = false;
                    if (operateType == "0")
                    {
                        Application.Lock();
                        flag = new DAL.AddOrderDataDAL().AddFinalOrder(ofm);
                        Application.UnLock();
                    }
                    else if (operateType == "1")
                    {
                        SqlConnection conn = new SqlConnection(DBHelper.connString);
                        conn.Open();

                        SqlTransaction tran = conn.BeginTransaction();

                        new AddOrderDataDAL().Del_Horder(orderid, tran);

                        Application.Lock();
                        flag = new DAL.AddOrderDataDAL().AddFinalOrderNoInfo(ofm, tran);
                        Application.UnLock();

                        if (flag)
                            tran.Commit();
                        else
                            tran.Rollback();

                        conn.Close();
                        conn.Dispose();
                    }

                    if (flag)
                    {
                        if (ordType == "0" || ordType == "1" || ordType == "2")
                            return "注册完成！";
                        else
                            return "报单成功！";
                    }
                    else
                    {
                        if (ordType == "0" || ordType == "1" || ordType == "2")
                            return "注册失败！";
                        else
                            return "报单失败！";
                    }

                }
                else
                    return "传入的相关参数不正确";
            }
            else
                return "请传入相关参数";

        }
        else
            return "请传入相关参数";

    }
    #endregion

    #region 报单浏览
    /// <summary>
    /// 报单浏览
    /// </summary>
    /// <param name="canshus"></param>
    /// <returns></returns>
    [WebMethod]
    public string ShowMemberOrders(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }


            string[] str_parms = canshus.Split(','); //分解参数
            string bianhao = str_parms[0];    //会员编号
            string qishu = str_parms[1]; //期数：全部 -1
            string sdate = str_parms[2]; //开始日期
            string edate = str_parms[3]; //结束日期
            string defraystate = str_parms[4]; //支付状态：全部 -1   未支付 0   已支付 1

            string conditions = ""; //查询条件

            if (qishu != "-1")
            {
                conditions += " and o.OrderExpectNum=" + qishu;
            }
            if (sdate != "" && edate != "")
            {
                conditions += " and  o.orderdate between '" + sdate + "' and '" + edate + "' ";
            }
            if (defraystate != "-1")
            {
                conditions += " and o.DefrayState=" + defraystate;
            }
            

            string key = "o.id";
            string table = "memberorder o,memberinfo h";
            string column = "o.SendWay,o.OrderExpectNum,o.payExpectNum,o.OrderID,o.Number,sendtype,o.TotalMoney,o.StoreID,o.Totalpv,o.OrderDate,h.Name,o.defraystate as zhifu,o.ordertype as fuxiaoname,o.defraytype,o.IsReceivables, case o.defraystate when 1 then 1 else case o.paymentmoney when 0 then 0 else 1 end end as dpqueren,o.defraystate as gsqueren";
            string condition = "o.number=h.number and ((o.number='" + bianhao + "') or (o.isagain =0 and h.assister='" + bianhao + "'))";

            if (conditions.Trim().Length > 0)
                condition += conditions;

            string sql = "select " + column + "  from " + table + "  where " + condition + "  order by " + key + " desc";
            DataTable dt = DAL.DBHelper.ExecuteDataTable(sql);

            if (dt == null || dt.Rows.Count == 0)
            {
                return "暂无数据";
            }

            int i_id = 1;

            StringBuilder XmlStr = new StringBuilder();
            XmlStr.Append("<MemberOrders>");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                XmlStr.Append("<MOInfo id=\"" + i_id + "\">");

                XmlStr.AppendFormat("<number>{0}</number>", dt.Rows[i]["Number"].ToString());
                XmlStr.AppendFormat("<defraystate>{0}</defraystate>", dt.Rows[i]["zhifu"].ToString());
                XmlStr.AppendFormat("<defrayType>{0}</defrayType>", dt.Rows[i]["defrayType"].ToString());
                XmlStr.AppendFormat("<sendWay>{0}</sendWay>", dt.Rows[i]["SendWay"].ToString());
                XmlStr.AppendFormat("<sendtype>{0}</sendtype>", dt.Rows[i]["Sendtype"].ToString());

                XmlStr.AppendFormat("<orderExpectNum>{0}</orderExpectNum>", dt.Rows[i]["OrderExpectNum"].ToString());
                XmlStr.AppendFormat("<payExpectNum>{0}</payExpectNum>", dt.Rows[i]["PayExpectNum"].ToString());
                XmlStr.AppendFormat("<orderID>{0}</orderID>", dt.Rows[i]["OrderID"].ToString());
                XmlStr.AppendFormat("<totalMoney>{0}</totalMoney>", dt.Rows[i]["totalMoney"].ToString());
                XmlStr.AppendFormat("<totalpv>{0}</totalpv>", dt.Rows[i]["Totalpv"].ToString());
                XmlStr.AppendFormat("<fuxiaoname>{0}</fuxiaoname>", dt.Rows[i]["fuxiaoname"].ToString());

                XmlStr.AppendFormat("<orderdate>{0}</orderdate>", GetBiaoZhunTime(dt.Rows[i]["orderdate"].ToString()));

                XmlStr.Append("</MOInfo>");

                i_id++;
            }

            XmlStr.Append("</MemberOrders>");

            return XmlStr.ToString();
        }
        else
            return "请传入相关参数";
    }
    /// <summary>
    /// 订单明细
    /// </summary>
    /// <param name="canshus"></param>
    /// <returns></returns>
    [WebMethod]
    public string Show_OrderDetails(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }

            string orderId = canshus;  //订单编号

            string sql = "select m.productid,m.orderid,m.storeid,m.Quantity,m.Price,m.Pv,m.Quantity*m.Price as totalmoney,p.ProductName,ptype.ProductTypeName" +
                         " from memberdetails m,product  p,producttype ptype " +
                         " where m.ProductID=p.ProductID and p.producttypeid=ptype.producttypeid and orderid=@OrderID";
            SqlParameter[] parm2 = { new SqlParameter("@OrderID", SqlDbType.VarChar, 40) };
            parm2[0].Value = orderId;
            DataTable dt_orderDetails = DAL.DBHelper.ExecuteDataTable(sql, parm2, CommandType.Text);


            if (dt_orderDetails == null || dt_orderDetails.Rows.Count <= 0)
                return "暂无该数据！";


            StringBuilder XmlStr = new StringBuilder();
            XmlStr.Append("<OrderDetailsInfo>");


            for (int i = 0; i < dt_orderDetails.Rows.Count; i++)
            {
                XmlStr.Append("<ODInfo id='" + (i + 1) + "'>");

                XmlStr.AppendFormat("<orderId>{0}</orderId>", dt_orderDetails.Rows[i]["OrderID"].ToString());
                XmlStr.AppendFormat("<storeid>{0}</storeid>", dt_orderDetails.Rows[i]["storeid"].ToString());
                XmlStr.AppendFormat("<productName>{0}</productName>", dt_orderDetails.Rows[i]["ProductName"].ToString());
                XmlStr.AppendFormat("<productTypeName>{0}</productTypeName>", dt_orderDetails.Rows[i]["ProductTypeName"].ToString());
                XmlStr.AppendFormat("<quantity>{0}</quantity>", dt_orderDetails.Rows[i]["Quantity"].ToString());
                XmlStr.AppendFormat("<price>{0}</price>", dt_orderDetails.Rows[i]["Price"].ToString());
                XmlStr.AppendFormat("<pv>{0}</pv>", dt_orderDetails.Rows[i]["pv"].ToString());
                XmlStr.AppendFormat("<totalmoney>{0}</totalmoney>", dt_orderDetails.Rows[i]["totalmoney"].ToString());

                XmlStr.Append("</ODInfo>");
            }

            XmlStr.Append("</OrderDetailsInfo>");

            return XmlStr.ToString();
        }
        else
            return "请传入相关参数";
    }
    #endregion

    #region 报单支付
    /// <summary>
    /// 报单支付查询
    /// </summary>
    /// <param name="canshus"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string ShowPaymentOrders(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }

            string[] str_parms = canshus.Split(',');
            string bianhao = str_parms[0];   //会员编号
            string qishu = str_parms[1];     //期数
            string startdate = str_parms[2];  //开始时间
            string enddate = str_parms[3];    //结束时间

            string tiaojian = ""; //查询条件

            if (qishu != "-1")
            {
                tiaojian += " and o.OrderExpectNum=" + qishu;
            }
            if (startdate != "" && enddate != "")
            {
                tiaojian += " and  o.orderdate between '" + startdate + "' and '" + enddate + "' ";
            }


            string number = bianhao;

            string condition = " o.number=h.number and ((o.number='" + number + "') or (o.isagain =0 and h.assister='" + number + "')) and  o.DefrayState=0 " + tiaojian;
            string key = "o.id";
            string column = " o.SendWay,o.OrderExpectNum,o.payExpectNum,o.OrderID,sendtype,o.Number,o.TotalMoney,o.StoreID,o.Totalpv,o.OrderDate,h.Name,o.defraystate as zhifu,o.ordertype as fuxiaoname,o.defraytype,o.IsReceivables, case o.defraystate when 1 then 1 else case o.paymentmoney when 0 then 0 else 1 end end as dpqueren,o.defraystate as gsqueren   ";
            string table = " memberorder o,memberinfo h ";


            DataTable dt = DAL.DBHelper.ExecuteDataTable("select " + column + " from " + table + " where " + condition + " order by " + key + " desc");

            if (dt == null || dt.Rows.Count <= 0)
                return "暂无数据";

            LoadLanguage();

            int i_id = 1;

            StringBuilder XmlStr = new StringBuilder();
            XmlStr.Append("<PaymentOrders>");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                XmlStr.Append("<POInfo id='" + (i + 1) + "'>");

                XmlStr.AppendFormat("<zhifu>{0}</zhifu>", GetDeftrayState(dt.Rows[i]["zhifu"].ToString()));

                XmlStr.AppendFormat("<sendWay>{0}</sendWay>", Common.GetSendWay(dt.Rows[i]["SendWay"].ToString()));

                XmlStr.AppendFormat("<sendtype>{0}</sendtype>", Common.GetSendType(dt.Rows[i]["Sendtype"].ToString()));
                XmlStr.AppendFormat("<orderExpectNum>{0}</orderExpectNum>", dt.Rows[i]["OrderExpectNum"].ToString());

                XmlStr.AppendFormat("<orderID>{0}</orderID>", dt.Rows[i]["OrderID"].ToString());
                XmlStr.AppendFormat("<totalMoney>{0}</totalMoney>", dt.Rows[i]["totalMoney"].ToString());
                XmlStr.AppendFormat("<totalpv>{0}</totalpv>", dt.Rows[i]["Totalpv"].ToString());
                XmlStr.AppendFormat("<fuxiaoname>{0}</fuxiaoname>", Common.GetMemberOrderType(dt.Rows[i]["fuxiaoname"].ToString()));
                XmlStr.AppendFormat("<orderdate>{0}</orderdate>", GetBiaoZhunTime(dt.Rows[i]["orderdate"].ToString()));

                XmlStr.Append("</POInfo>");

                i_id++;
            }

            XmlStr.Append("</PaymentOrders>");

            return XmlStr.ToString();


        }
        else
            return "请传入相关参数";
    }

    /// <summary>
    /// 订单删除
    /// </summary>
    /// <param name="canshus"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string DelOrder(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }

            string[] str_parms = canshus.Split(',');

            string orderid = str_parms[0]; //订单编号
            int operateType = Convert.ToInt32(str_parms[1]);  //0 公司操作  1 服务机构操作  2 会员操作


            int maxEcept = CommonDataBLL.GetMaxqishu();

            Model.MemberOrderModel model = MemberOrderBLL.GetMemberOrder(orderid);

            if (model == null)
            {
                return "订单已不存在";
            }
            if (model.DefrayState == 1)
            {
                return "订单已支付，不能删除";
            }


            string result = "";

            LoadLanguage();

            if (operateType == 2)
            {
                if (model.IsAgain == 1)
                {
                    result = new AuditingMemberagainBLL().DelMembersDeclaration(orderid, Convert.ToDouble(model.TotalPv), model.Number, model.OrderExpect, model.StoreId, Convert.ToDouble(model.TotalMoney));
                }
                else
                {
                    result = new BrowseMemberOrdersBLL().DelMembersDeclaration(model.Number, model.OrderExpect, model.OrderId, model.StoreId, Convert.ToDouble(model.LackProductMoney));
                }
                result = result == null ? ("删除成功") : (result);
            }

            return result;

        }
        else
            return "请传入相关参数";
    }
    #endregion

    #region 收货确认
    /// <summary>
    /// 收货确认数据
    /// </summary>
    /// <param name="canshus"></param>
    /// <returns></returns>
    [WebMethod]
    public string ShowReceivingData(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }


            string[] str_parm = canshus.Split(',');
            if (str_parm.Length == 5)
            {
                string bianhao = str_parm[0];     //会员编号
                string expectnum = str_parm[1];     //期数
                string sdate = str_parm[2];     //报单开始日期
                string edate = str_parm[3];     //报单结束日期
                string receivedstate = str_parm[4];     //收货状态

                string conditions = "";  //查询条件

                if (expectnum != "-1")
                {
                    conditions += "and o.ExpectNum=" + expectnum;
                }
                if (sdate != "" || edate != "")
                {
                    conditions += " and  o.docmaketime between '" + sdate + "' and '" + edate + "' ";
                }
                if (receivedstate != "-1")
                {
                    conditions += " and o.isreceived=" + receivedstate;
                }


                string number = bianhao;
                string key = "o.docid";
                string condition = " o.storeorderid=s.storeorderid and o.issend=1 and o.doctypeid=2 and s.sendway=1 and  o.client='" + number + "' " + conditions;
                string column = " o.isreceived,o.ExpectNum,o.docid,s.storeorderid,s.orderdatetime,o.client,o.TotalMoney,o.Totalpv,s.kuaididh,s.ConveyanceCompany ";
                string table = " InventoryDoc o,storeorder s ";

                DataTable dt = DAL.DBHelper.ExecuteDataTable("select " + column + " from " + table + " where " + condition + " order by " + key + " desc");

                if (dt != null && dt.Rows.Count > 0)
                {
                    int i_id = 1;

                    StringBuilder XmlStr = new StringBuilder();
                    XmlStr.Append("<Receiving>");

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        XmlStr.Append("<RInfo id=\"" + i_id + "\">");

                        XmlStr.AppendFormat("<docid>{0}</docid>", dt.Rows[i]["docid"].ToString());
                        XmlStr.AppendFormat("<storeorderid>{0}</storeorderid>", dt.Rows[i]["storeorderid"].ToString());
                        XmlStr.AppendFormat("<kuaididh>{0}</kuaididh>", dt.Rows[i]["kuaididh"].ToString());
                        XmlStr.AppendFormat("<expectNum>{0}</expectNum>", dt.Rows[i]["ExpectNum"].ToString());
                        XmlStr.AppendFormat("<totalMoney>{0}</totalMoney>", dt.Rows[i]["TotalMoney"].ToString());

                        XmlStr.AppendFormat("<totalpv>{0}</totalpv>", dt.Rows[i]["Totalpv"].ToString());
                        XmlStr.AppendFormat("<orderdatetime>{0}</orderdatetime>", GetBiaoZhunTime(dt.Rows[i]["orderdatetime"].ToString()));

                        XmlStr.AppendFormat("<isreceived>{0}</isreceived>", dt.Rows[i]["isreceived"].ToString());

                        XmlStr.Append("</RInfo>");

                        i_id++;
                    }

                    XmlStr.Append("</Receiving>");

                    return XmlStr.ToString();
                }
                else
                    return "暂无数据";

            }
            else
                return "传入的相关参数不正确";

        }
        else
            return "请传入相关参数";
    }
    /// <summary>
    /// 收货确认明细
    /// </summary>
    /// <param name="canshus"></param>
    /// <returns></returns>
    [WebMethod]
    public string ShowReceivingDetails(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }


            string docid = canshus; //出库单号


            string sql = "select o.ExpectNum,o.docid,oo.storeorderid,oo.orderdatetime,o.client,o.TotalMoney,o.Totalpv,oo.ConsignmentDateTime,oo.kuaididh from" +
                "  InventoryDoc o,storeorder oo where o.storeorderid=oo.storeorderid  and o.docid='" + docid + "'";
            DataTable dt_order = DBHelper.ExecuteDataTable(sql);

            sql = "select i.*,p.productname,p.productcode from InventoryDocDetails i join product p on i.productid=p.productid where i.docid='" + docid + "'";
            DataTable dt_detail = DAL.DBHelper.ExecuteDataTable(sql);

            if (dt_order != null && dt_order.Rows.Count > 0 && dt_detail != null && dt_detail.Rows.Count > 0)
            {
                int i_id = 1;

                StringBuilder XmlStr = new StringBuilder();
                XmlStr.Append("<ReceivingDetail>");

                for (int i = 0; i < dt_order.Rows.Count; i++)
                {
                    XmlStr.Append("<R_OInfo>");

                    XmlStr.AppendFormat("<expectNum>{0}</expectNum>", dt_order.Rows[i]["ExpectNum"].ToString());
                    XmlStr.AppendFormat("<docid>{0}</docid>", dt_order.Rows[i]["docid"].ToString());

                    XmlStr.AppendFormat("<totalMoney>{0}</totalMoney>", dt_order.Rows[i]["TotalMoney"].ToString());

                    XmlStr.AppendFormat("<totalpv>{0}</totalpv>", dt_order.Rows[i]["Totalpv"].ToString());
                    XmlStr.AppendFormat("<orderdatetime>{0}</orderdatetime>", GetBiaoZhunTime(dt_order.Rows[i]["orderdatetime"].ToString()));

                    XmlStr.Append("</R_OInfo>");
                }
                for (int i = 0; i < dt_detail.Rows.Count; i++)
                {
                    XmlStr.Append("<R_DInfo id=\"" + i_id + "\">");

                    XmlStr.AppendFormat("<docid>{0}</docid>", dt_detail.Rows[i]["docid"].ToString());
                    XmlStr.AppendFormat("<productcode>{0}</productcode>", dt_detail.Rows[i]["Productcode"].ToString());
                    XmlStr.AppendFormat("<productName>{0}</productName>", dt_detail.Rows[i]["ProductName"].ToString());
                    XmlStr.AppendFormat("<productQuantity>{0}</productQuantity>", dt_detail.Rows[i]["productQuantity"].ToString());

                    XmlStr.AppendFormat("<unitPrice>{0}</unitPrice>", dt_detail.Rows[i]["unitPrice"].ToString());
                    XmlStr.AppendFormat("<pv>{0}</pv>", dt_detail.Rows[i]["pv"].ToString());
                    XmlStr.AppendFormat("<producttotal>{0}</producttotal>", dt_detail.Rows[i]["producttotal"].ToString());

                    XmlStr.Append("</R_DInfo>");

                    i_id++;
                }

                XmlStr.Append("</ReceivingDetail>");

                return XmlStr.ToString();
            }
            else
                return "暂无数据";
        }
        else
            return "请传入相关参数";

    }
    /// <summary>
    /// 收货确认-确认收货功能
    /// </summary>
    /// <param name="canshus"></param>
    /// <returns></returns>
    [WebMethod]
    public string ConfirmReceiving(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }


            string docids = canshus; //出库单号

            if (docids.Length == 0 || docids.IndexOf(",") == -1)
            {
                return "请选择要确认的货单";
            }

            if (new AffirmConsignBLL().Submit(docids))
                return "确认成功";
            else
                return "确认失败";

        }
        else
            return "请传入相关参数";
    }
    #endregion

    #region 申请服务机构
    /// <summary>
    /// 服务机构信息
    /// </summary>
    /// <param name="canshus"></param>
    /// <returns></returns>
    [WebMethod]
    public string ShowStoreInfo(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }


            string bianhao = canshus; //会员编号


            //显示的服务机构相关信息
            string number = "";       //会员编号
            string name = "";          //机构负责人姓名
            string storeID = "";     //服务机构编号
            string storename = "";   //服务机构名称
            string scpccode = "";       //服务机构所在地
            string postalcode = "";     //邮编
            string direct = "";     //推荐机构的会员编号
            string shenhe = "";     //审核状态
            string dzaddress = "";   //机构服务人联系信息
            string mobiletele = "";    //移动电话
            string hometele = "";   //负责人电话
            string bank = "";     //开户银行
            string bankBranchname = "";   //分/支行名称
            string bankcard = "";    //银行账户
            string email = "";     //电子邮箱
            string netaddress = "";     //网址
            string expect = "";     //申请服务机构期数
            string remark = "";   //备注 
            string level = "";      //级别
            //string fareArea = "";       //经营面积(平方米)
            //string totalAccountMoney = "";    //投资总额(万元)

            string sql = "select top 1 Number,Direct,Name,StoreName,SCPCCode,postalcode,CPCCode,MobileTele,HomeTele,BankCode,BankCard,Email,NetAddress,ExpectNum,Remark,StoreLevelInt,FareArea,FareArea as frea,TotalAccountMoney,StoreID,BranchNumber,StoreAddress,isnull(storestate,0) as storestate  from StoreInfo  where Number='" + bianhao + "'";
            SqlDataReader sdr = DAL.DBHelper.ExecuteReader(sql);
            if (sdr.Read())
            {
                number = sdr.GetString(0).ToString();
                direct = sdr.GetString(1);
                name = Encryption.Encryption.GetDecipherName(sdr.GetString(2));
                storename = Encryption.Encryption.GetDecipherName(sdr.GetString(3));
                DataTable dt = DBHelper.ExecuteDataTable("select top 1 country,province from city where cpccode like'" + sdr.GetString(4) + "%'");
                if (dt.Rows.Count > 0)
                {
                    scpccode = dt.Rows[0]["country"].ToString() + dt.Rows[0]["province"].ToString();
                }
                postalcode = sdr.GetString(5).ToString();
                dzaddress = BLL.CommonClass.CommonDataBLL.GetAddressByCode(sdr.GetString(6).ToString()) + sdr.GetString(21); ;
                mobiletele = sdr.GetString(7).ToString();
                hometele = sdr.GetString(8).ToString();
                bank = DAL.DBHelper.ExecuteScalar("select BankName from MemberBank where BankCode='" + sdr.GetString(9).ToString() + "'").ToString();
                bankcard = sdr.GetString(10).ToString();
                email = sdr.GetString(11).ToString();
                netaddress = sdr.GetString(12).ToString();
                expect = "第" + sdr.GetInt32(13).ToString() + "期";
                remark = sdr.GetString(14).ToString();
                level = DBHelper.ExecuteDataTable("select isnull(levelstr,'') as levelstr from bsco_level where levelflag=1 and levelint=" + sdr.GetInt32(15)).Rows[0][0].ToString();

                //fareArea = sdr.GetDecimal(16).ToString();
                //totalAccountMoney = sdr.GetDecimal(18).ToString();

                storeID = sdr.GetString(19).ToString();
                bankBranchname = sdr.GetString(20);

                if (int.Parse(sdr["storestate"].ToString()) == 0)
                {
                    shenhe = "未激活";
                }
                else if (int.Parse(sdr["storestate"].ToString()) == 1)
                {
                    shenhe = "已激活";
                }
                else if (int.Parse(sdr["storestate"].ToString()) == 2)
                {
                    shenhe = "已注销";
                }
                else if (int.Parse(sdr["storestate"].ToString()) == 3)
                {
                    shenhe = "已冻结";
                }
            }
            sdr.Close();
            sdr.Dispose();



            StringBuilder XmlStr = new StringBuilder();
            XmlStr.Append("<StoreInfo>");

            XmlStr.AppendFormat("<number>{0}</number>", number);
            XmlStr.AppendFormat("<name>{0}</name>", name);
            XmlStr.AppendFormat("<storeID>{0}</storeID>", storeID);
            XmlStr.AppendFormat("<storename>{0}</storename>", storename);
            XmlStr.AppendFormat("<scpccode>{0}</scpccode>", scpccode);
            XmlStr.AppendFormat("<postalcode>{0}</postalcode>", postalcode);
            XmlStr.AppendFormat("<direct>{0}</direct>", direct);
            XmlStr.AppendFormat("<shenhe>{0}</shenhe>", shenhe);
            XmlStr.AppendFormat("<dzaddress>{0}</dzaddress>", dzaddress);
            XmlStr.AppendFormat("<mobiletele>{0}</mobiletele>", mobiletele);
            XmlStr.AppendFormat("<hometele>{0}</hometele>", hometele);
            XmlStr.AppendFormat("<bank>{0}</bank>", bank);
            XmlStr.AppendFormat("<bankBranchname>{0}</bankBranchname>", bankBranchname);
            XmlStr.AppendFormat("<bankcard>{0}</bankcard>", bankcard);
            XmlStr.AppendFormat("<email>{0}</email>", email);
            XmlStr.AppendFormat("<netaddress>{0}</netaddress>", netaddress);
            XmlStr.AppendFormat("<expect>{0}</expect>", expect);
            XmlStr.AppendFormat("<remark>{0}</remark>", remark);
            XmlStr.AppendFormat("<level>{0}</level>", level);

            XmlStr.Append("</StoreInfo>");

            return XmlStr.ToString();

        }
        else
            return "请传入相关参数";
    }
    /// <summary>
    /// 会员申请服务机构
    /// </summary>
    /// <param name="canshus">加密的参数</param>
    /// <returns></returns>
    [WebMethod(EnableSession=true)]
    public string RegisterStore(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }


            string bianhao = "";   //申请人的会员编号
            string storeid = "";   //服务机构编号
            string direct = "";    //推荐服务机构的会员编号
            string name = "";      //机构负责人姓名
            string storename = "";  //服务机构名称
            string storeaddr = "";  //服务机构所在地
            string contactaddr = "";  //联系人地址
            string postcode = "";     //邮编 
            string hometele = "";   //负责人电话
            string officetele = ""; //办公电话
            string mobile = "";   //移动电话
            string faxtele = "";  //传真电话
            string openingbank = "";  //开户银行
            string bankcard = "";  //银行账户
            string email = "";   //电子邮箱 
            string storeurl = "";   //网址
            string remark = "";   //备注
            string storelevel = "";   //服务机构级别
            string businessarea = "";  //经营面积(平方米) 
            string operatingvarieties = "";  //经营品种
            string investmenttotal = "";  //投资总额


            string[] str_parm = canshus.Split(',');
            if (str_parm.Length == 21)
            {
                bianhao = CommonDataBLL.quanjiao(str_parm[0]);  //申请人的会员编号
                storeid = CommonDataBLL.quanjiao(str_parm[1]);  //服务机构编号
                direct = CommonDataBLL.quanjiao(str_parm[2]);   //推荐服务机构的会员编号
                name = str_parm[3];     //机构负责人姓名
                storename = str_parm[4];//服务机构名称
                storeaddr = str_parm[5];//服务机构所在地，格式：中国-北京
                contactaddr = str_parm[6];//联系人地址，格式：中国-安徽省-六安市-寿县-正阳镇王祠村尚圩队
                postcode = str_parm[7];//邮编 
                hometele = str_parm[8];//负责人电话
                officetele = str_parm[9];//办公电话
                mobile = str_parm[10]; //移动电话
                faxtele = str_parm[11];//传真电话
                openingbank = str_parm[12];//开户银行
                bankcard = str_parm[13];//银行账户
                email = str_parm[14];//电子邮箱 
                storeurl = str_parm[15];//网址，格式：http://www.baidu.com
                remark = str_parm[16];//备注
                storelevel = str_parm[17];//服务机构级别，格式：0-一级店
                businessarea = str_parm[18];//经营面积(平方米)
                operatingvarieties = str_parm[19];//经营品种
                investmenttotal = str_parm[20];//投资总额


                ///相关验证

                string[] str_storeaddr = storeaddr.Split('-');
                string[] str_contactaddr = contactaddr.Split('-');

                int exists = (int)StoreRegisterBLL.IsMemberNum(bianhao);
                if (exists <= 0)
                {
                    return "对不起,该会员编号不存在";
                }
                if (DBHelper.ExecuteScalar("select MemberState from memberinfo where number='" + bianhao + "'").ToString() == "2")
                {
                    return "该会员已注销,请先激活";
                }
                if (StoreRegisterBLL.CheckStoreNumber(bianhao) == -1)
                {
                    return "该会员已经申请服务机构,不可重复申请";
                }
                if (StoreRegisterBLL.CheckStoreNumber(bianhao) == -2)
                {
                    return "该会员已经是服务机构，不可重复申请";
                }
                if (StoreRegisterConfirmBLL.CheckStoreId(DisposeString.DisString(storeid)))
                {
                    return "对不起,店铺编号已存在";
                }
                if (!StoreRegisterBLL.CheckMemberInfoByNumber(DisposeString.DisString(direct)))
                {
                    return "对不起,推荐店铺编号会员不存在";
                }
                if (str_storeaddr[0] == "" || str_storeaddr[0] == "请选择" || str_storeaddr[1] == "" || str_storeaddr[1] == "请选择")
                {
                    return "请选择服务机构所在地";
                }
                if (str_contactaddr[0] == "" || str_contactaddr[0] == "请选择" || str_contactaddr[1] == "" || str_contactaddr[1] == "请选择" || str_contactaddr[2] == "" || str_contactaddr[2] == "请选择" || str_contactaddr[3] == "" || str_contactaddr[3] == "请选择")
                {
                    return "请选择联系人地址";
                }
                if (str_contactaddr[4] == "")
                {
                    return "请选择联系人的详细地址";
                }
                if (remark.Length > 150)
                {
                    return "对不起，备注应在150个字以内！";
                }
                if (operatingvarieties.Length > 150)
                {
                    return "对不起，经营品种应在150个字以内！";
                }



                ///实体类相关赋值

                UnauditedStoreInfoModel ustore = new UnauditedStoreInfoModel();

                ustore.Number = bianhao;
                ustore.StoreId = storeid;
                ustore.Name = Encryption.Encryption.GetEncryptionName(new AjaxClass().GetMumberName(bianhao));//加密店长姓名
                ustore.StoreName = Encryption.Encryption.GetEncryptionName(storename);//加密店铺名称

                ustore.StoreCity = str_storeaddr[0];
                ustore.StoreCountry = str_storeaddr[1];
                object o_cpccode = DBHelper.ExecuteScalar("select cpccode from city where country='" + str_storeaddr[0] + "' and province='" + str_storeaddr[1] + "'").ToString();
                if (o_cpccode == null)
                    return "服务机构所在地不正确！";
                else
                    ustore.CPCCode = o_cpccode.ToString().Substring(0, 4);

                ustore.Country = str_contactaddr[0];
                ustore.Province = str_contactaddr[1];
                ustore.City = str_contactaddr[2];
                ustore.Xian = str_contactaddr[3];

                ustore.SCPCCode = CommonDataBLL.GetCityCode(str_contactaddr[0], str_contactaddr[1], str_contactaddr[2], str_contactaddr[3]);

                ustore.StoreAddress = Encryption.Encryption.GetEncryptionAddress(str_contactaddr[4]);//加密地址
                ustore.HomeTele = Encryption.Encryption.GetEncryptionTele(hometele);//加密家庭电话
                ustore.OfficeTele = Encryption.Encryption.GetEncryptionTele(officetele);//加密办公电话
                ustore.MobileTele = Encryption.Encryption.GetEncryptionTele(mobile);//加密电话
                ustore.FaxTele = Encryption.Encryption.GetEncryptionTele(faxtele);//加密传真
                ustore.BankCode = openingbank;
                ustore.BankCard = Encryption.Encryption.GetEncryptionCard(bankcard);//加密卡号
                ustore.Email = email;
                ustore.NetAddress = storeurl;
                ustore.Remark = remark;
                ustore.Direct = DisposeString.DisString(direct);
                ustore.ExpectNum = CommonDataBLL.getMaxqishu();
                ustore.RegisterDate = DateTime.Now.ToUniversalTime();

                string password = Encryption.Encryption.GetEncryptionPwd(storeid, storeid);
                ustore.LoginPass = password;
                ustore.AdvPass = password;

                string[] str_storelevel = storelevel.Split('-');
                ustore.StoreLevelInt = Convert.ToInt32(str_storelevel[0]);
                ustore.StoreLevelStr = str_storelevel[1];

                ustore.FareBreed = operatingvarieties;
                ustore.TotalinvestMoney = decimal.Parse(investmenttotal);

                ustore.PostalCode = postcode;

                ustore.AccreditExpectNum = CommonDataBLL.getMaxqishu();
                ustore.PermissionMan = "";
                ustore.Currency = Convert.ToInt32(DBHelper.ExecuteScalar("select rateid from country where countrycode='" + ustore.CPCCode.Substring(0, 2) + "'").ToString());

                ustore.OperateIp = CommonDataBLL.OperateIP;
                ustore.OperateNum = "";
                ustore.PhotoPath = "";

                if (BLL.other.Company.StoreRegisterBLL.AllerRegisterStoreInfo(ustore))
                    return "注册成功,请等待公司审核";
                else
                    return "注册失败";

            }
            else
                return "传入的相关参数不正确";

        }
        else
            return "请传入相关参数";
    }

    /// <summary>
    /// 获取相关级别
    /// </summary>
    /// <param name="canshus"></param>
    /// <returns></returns>
    [WebMethod]
    public string GetLevelByLevelFlag(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }

            //0 会员级别  1 服务机构级别
            string levelflag = canshus;

            DataTable dt = DAL.DBHelper.ExecuteDataTable("select id,levelint,levelstr,ICOPath from BSCO_Level where levelflag=" + levelflag + " order by levelint");

            StringBuilder XmlStr = new StringBuilder();
            XmlStr.Append("<BSCO_Level>");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                XmlStr.Append("<LevelInfo id='" + (i + 1) + "'>");
                XmlStr.AppendFormat("<id>{0}</id>", dt.Rows[i]["id"].ToString());
                XmlStr.AppendFormat("<levelint>{0}</levelint>", dt.Rows[i]["levelint"].ToString());
                XmlStr.AppendFormat("<levelstr>{0}</levelstr>", dt.Rows[i]["levelstr"].ToString());
                XmlStr.AppendFormat("<ICOPath>{0}</ICOPath>", dt.Rows[i]["ICOPath"].ToString());
                XmlStr.Append("</LevelInfo>");
            }

            XmlStr.Append("</BSCO_Level>");

            return XmlStr.ToString();

        }
        else
            return "请传入相关参数";
    }
    /// <summary>
    /// 银行信息
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public string GetMemberBank()
    {

        string sql = "Select a.BankName,a.BankID,a.BankCode  From MemberBank a,Country b  where a.countrycode=b.id and b.Name='中国' and bankcode<>'000000'";
        DataTable dt = DAL.DBHelper.ExecuteDataTable(sql);

        StringBuilder XmlStr = new StringBuilder();
        XmlStr.Append("<MBankInfo>");

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            XmlStr.Append("<MBInfo id='" + (i + 1) + "'>");

            XmlStr.AppendFormat("<BankCode>{0}</BankCode>", dt.Rows[i]["BankCode"].ToString());
            XmlStr.AppendFormat("<BankName>{0}</BankName>", dt.Rows[i]["BankName"].ToString());

            XmlStr.Append("</MBInfo>");
        }

        XmlStr.Append("</MBankInfo>");

        return XmlStr.ToString();

    }
    #endregion

    #endregion

    #region 财务管理

    #region 最新奖金
    /// <summary>
    /// 最新奖金
    /// </summary>
    /// <param name="parmeters">加密参数</param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string newestBonus(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {

            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }


            //分解参数
            string[] str_parmeters = canshus.Split(',');

            //子参数
            string number = str_parmeters[0]; //会员编号
            int expectnum = Convert.ToInt32(str_parmeters[1]); //奖金查询期数


            LoadLanguage();//默认语言指定


            //收入
            double bonus0 = 0;
            double bonus1 = 0;
            double bonus2 = 0;
            double bonus3 = 0;
            double bonus4 = 0;
            double bonus5 = 0;

            double shifa = 0;
            double zongji = 0;

            //支出
            double koushui = 0;
            double heji = 0;

            //获取标准币种
            int bzCurrency = CommonDataBLL.GetStandard();
            if (Session["Default_Currency"] == null)
                Session["Default_Currency"] = bzCurrency;

            DataTable dt = MemberInfoModifyBll.getBonusTable(expectnum, number);
            if (dt.Rows.Count > 0)
            {
                if (AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString())) != 0)
                {
                    bonus0 = Convert.ToDouble(dt.Rows[0]["Bonus0"]) / AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()));
                    bonus1 = Convert.ToDouble(dt.Rows[0]["Bonus1"]) / AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()));
                    bonus2 = Convert.ToDouble(dt.Rows[0]["Bonus2"]) / AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()));
                    bonus3 = Convert.ToDouble(dt.Rows[0]["Bonus3"]) / AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()));
                    bonus4 = Convert.ToDouble(dt.Rows[0]["Bonus4"]) / AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()));
                    bonus5 = Convert.ToDouble(dt.Rows[0]["Bonus5"]) / AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()));
                    shifa = Convert.ToDouble(dt.Rows[0]["CurrentSolidSend"]) / AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()));
                    zongji = shifa;

                    koushui = Convert.ToDouble(dt.Rows[0]["DeductTax"]) / AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()));
                    heji = koushui;
                }


                StringBuilder XmlStr = new StringBuilder();
                XmlStr.Append("<BonusDetails>");
                XmlStr.Append("<newestBonus>");

                XmlStr.AppendFormat("<bonus0>{0}</bonus0>", bonus0);
                XmlStr.AppendFormat("<bonus1>{0}</bonus1>", bonus1);
                XmlStr.AppendFormat("<bonus2>{0}</bonus2>", bonus2);
                XmlStr.AppendFormat("<bonus3>{0}</bonus3>", bonus3);
                XmlStr.AppendFormat("<bonus4>{0}</bonus4>", bonus4);
                XmlStr.AppendFormat("<bonus5>{0}</bonus5>", bonus5);
                XmlStr.AppendFormat("<shifa>{0}</shifa>", shifa);
                XmlStr.AppendFormat("<zongji>{0}</zongji>", zongji);

                XmlStr.AppendFormat("<koushui>{0}</koushui>", koushui);
                XmlStr.AppendFormat("<heji>{0}</heji>", heji);

                XmlStr.Append("</newestBonus>");
                XmlStr.Append("</BonusDetails>");

                return XmlStr.ToString();

            }
            else
                return "没有该会员的相关信息";

        }
        else
            return "请传入相关的参数";

    }
    #endregion

    #region 历史奖金
    /// <summary>
    /// 历史奖金
    /// </summary>
    /// <param name="parmeters">加密参数</param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string historyBonus(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);//会员编号
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }



            string[] str_parmeters = canshus.Split(',');
            string number = str_parmeters[0];//会员编号
            int expectnum_s = Convert.ToInt32(str_parmeters[1]);//开始期数
            int expectnum_e = Convert.ToInt32(str_parmeters[2]);//结束期数


            LoadLanguage();//默认语言指定


            StringBuilder ReturnXml = new StringBuilder();
            StringBuilder XmlStr = new StringBuilder();
            BasicSearchBLL basicSearchBLL = new BasicSearchBLL();

            ReturnXml.Append("<FinancialManagement>");
            for (int i = expectnum_s; i <= expectnum_e; i++)
            {

                DataTable dt = basicSearchBLL.GetMemberBalance(i.ToString(), number);
                if (dt.Rows.Count > 0)
                {

                    XmlStr.Append("<historyBonus id='hbon" + i + "'>");

                    XmlStr.AppendFormat("<expectnum>{0}</expectnum>", i);

                    int isjiesuan = (int)DBHelper.ExecuteScalar("Select isSuance from config where ExpectNum=" + i);
                    if (isjiesuan == 1)
                        XmlStr.AppendFormat("<jiesuanstate>{0}</jiesuanstate>", "已结算");
                    else
                        XmlStr.AppendFormat("<jiesuanstate>{0}</jiesuanstate>", "未结算");

                    XmlStr.AppendFormat("<bonus0>{0}</bonus0>", Convert.ToDouble(dt.Rows[0]["Bonus0"]).ToString("f2"));
                    XmlStr.AppendFormat("<bonus1>{0}</bonus1>", Convert.ToDouble(dt.Rows[0]["Bonus1"]).ToString("f2"));
                    XmlStr.AppendFormat("<bonus2>{0}</bonus2>", Convert.ToDouble(dt.Rows[0]["Bonus2"]).ToString("f2"));
                    XmlStr.AppendFormat("<bonus3>{0}</bonus3>", Convert.ToDouble(dt.Rows[0]["Bonus3"]).ToString("f2"));
                    XmlStr.AppendFormat("<bonus4>{0}</bonus4>", Convert.ToDouble(dt.Rows[0]["Bonus4"]).ToString("f2"));
                    XmlStr.AppendFormat("<bonus5>{0}</bonus5>", Convert.ToDouble(dt.Rows[0]["Bonus5"]).ToString("f2"));

                    XmlStr.AppendFormat("<CurrentTotalMoney>{0}</CurrentTotalMoney>", Convert.ToDouble(dt.Rows[0]["CurrentTotalMoney"]).ToString("f2"));
                    XmlStr.AppendFormat("<DeductTax>{0}</DeductTax>", Convert.ToDouble(dt.Rows[0]["DeductTax"]).ToString("f2"));
                    XmlStr.AppendFormat("<CurrentSolidSend>{0}</CurrentSolidSend>", Convert.ToDouble(dt.Rows[0]["CurrentSolidSend"]).ToString("f2"));

                    XmlStr.Append("</historyBonus>");

                }
            }

            if (XmlStr.ToString().Trim().Length == 0)
                return "没有该会员的相关信息";
            else
                ReturnXml.Append(XmlStr.ToString());

            ReturnXml.Append("</FinancialManagement>");


            return XmlStr.ToString();
        }
        else
            return "请传入相关的参数";
    }
    #endregion

    #region 账户充值
    [WebMethod(EnableSession = true)]
    public string OnlinePayment(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }


            string[] str_parmeters = canshus.Split(','); //参数分解

            string number = str_parmeters[0]; //会员编号
            string money = str_parmeters[1];  //充值金额
            string dtype = str_parmeters[2];//汇款用途  0 现金账户  1 消费账户


            //验证店铺是否选择
            if (number.Trim().Length == 0)
            {
                return "请输入会员编号！";
            }
            //验证金额是否输入正确
            double d = 0;
            bool b = double.TryParse(money.Trim(), out d);
            if (!b)
            {
                return "金额输入不正确！";
            }
            if (d <= 0)
            {
                return "申报的金额必须大于0！";
            }
            if (d > 9999999)
            {
                return "输入金额太大！";
            }
            if (dtype != "0" && dtype != "1")
            {
                return "汇款用途错误！";
            }


            LoadLanguage();


            RemittancesModel info = new RemittancesModel();
            info.ReceivablesDate = DateTime.UtcNow;
            info.RemittancesDate = DateTime.UtcNow;
            info.ImportBank = "";
            info.ImportNumber = "";
            info.RemittancesAccount = "";
            info.RemittancesBank = "";
            info.SenderID = "";
            info.Sender = "";
            info.RemitNumber = number;
            info.RemitMoney = decimal.Parse(money);
            info.StandardCurrency = 1;
            info.Use = int.Parse(dtype);
            info.PayexpectNum = BLL.CommonClass.CommonDataBLL.getMaxqishu();
            info.Managers = number;
            info.ConfirmType = 0;
            info.Remark = "";
            info.RemittancesCurrency = 1;
            info.RemittancesMoney = decimal.Parse(money);
            info.OperateIp = CommonDataBLL.OperateIP;
            info.OperateNum = number;

            //获取汇单号
            string huidan = "HK" + Model.Other.MYDateTime.ToYYMMDDHHmmssString();
            //判断汇单号是否存在:true存在,false不存在
            bool isExist = RemittancesBLL.isMemberExistsHuiDan(huidan);
            while (isExist)
            {
                huidan = "HK" + Model.Other.MYDateTime.ToYYMMDDHHmmssString();
                isExist = RemittancesBLL.isMemberExistsHuiDan(huidan);
            }
            info.RemitStatus = 1;
            info.IsGSQR = false;
            info.Remittancesid = huidan;

            RemittancesBLL.RemitDeclare(info, "1", "1");

            string billid = EncryKey.GetEncryptstr(huidan, 2, 1);

            return "blif=" + billid;

        }
        else
            return "请传入相关的参数";
    }
    #endregion

    #region 充值浏览
    /// <summary>
    /// 充值浏览
    /// </summary>
    /// <param name="parmeters">加密参数</param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string resultBrowse(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);//会员编号
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }



            string[] str_parmeters = canshus.Split(',');
            //会员编号
            string number = str_parmeters[0]; //会员编号
            //查询条件
            string states = str_parmeters[1]; //是否审核
            string paySTime = str_parmeters[2]; //付款起始日期
            string payETime = str_parmeters[3]; //付款结束日期


            LoadLanguage();//默认语言指定



            StringBuilder condition = new StringBuilder();
            string cloumns = " Remittances.isgsqr,Remittances.remittancesid,Remittances.RemittancesMoney,Remittances.RemittancesCurrency,Remittances.RemitNumber, Remittances.Sender,Remittances.Managers,Remittances.ImportBank, Remittances.PayWay,Remittances.Remittancesid,Remittances.[Use],Remittances.StandardCurrency,Remittances.ConfirmType, Remittances.SenderID, Remittances.RemitMoney,Remittances.PayExpectNum,Remittances.Id,Remittances.ReceivablesDate,Remittances.PayExpectNum,Remittances.isgsqr,Remittances.Remark ";
            string table = " Remittances ,MemberInfo ";
            string key = "Remittances.id";
            condition.Append("MemberInfo.Number=Remittances.RemitNumber and Remittances.relationorderid='' and Remittances.RemitStatus=1 and MemberInfo.Number='" + number + "' ");

            if (paySTime != "")
            {
                DateTime time = DateTime.Now.ToUniversalTime();
                bool b = DateTime.TryParse(paySTime.Trim(), out time);
                if (!b)
                    return "时间格式不正确！";

                DisposeString.DisString(paySTime, "'", "");
                if (payETime != "")
                {
                    b = DateTime.TryParse(payETime.Trim(), out time);
                    if (!b)
                        return "时间格式不正确！";

                    payETime = (DateTime.Parse(payETime).AddHours(23).AddMinutes(59).AddSeconds(59)).ToString();
                    DisposeString.DisString(payETime, "'", "");

                    condition.Append(" and ReceivablesDate>= '" + paySTime + "' and ReceivablesDate<='" + payETime + "'");
                }
                else
                {
                    condition.Append(" and ReceivablesDate>= '" + paySTime + "'");
                }
            }

            else
            {
                if (payETime != "")
                {
                    DateTime time = DateTime.Now.ToUniversalTime();
                    bool b = DateTime.TryParse(payETime.Trim(), out time);
                    if (!b)
                        return "时间格式不正确！";

                    payETime = (DateTime.Parse(payETime.Trim()).AddHours(23).AddMinutes(59).AddSeconds(59)).ToString();
                    condition.Append(" and ReceivablesDate<='" + payETime + "'");
                }
            }

            if (states != "-1")
            {
                condition.Append(" and IsGSQR='" + states.Trim() + "' ");
            }

            StringBuilder XmlStr = new StringBuilder();

            DataTable dt = DAL.DBHelper.ExecuteDataTable("select " + cloumns + "  from " + table + "  where " + condition + "  order by " + key + " desc");

            if (dt == null && dt.Rows.Count == 0)
            {
                return "暂无该会员相关信息";
            }

            XmlStr.Append("<RechargeBrowse>");

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                XmlStr.Append("<resultBrowse id='" + (i + 1) + "'>");

                XmlStr.AppendFormat("<id>{0}</id>", dt.Rows[i]["id"].ToString());
                XmlStr.AppendFormat("<RemittancesID>{0}</RemittancesID>", dt.Rows[i]["RemittancesID"].ToString());

                XmlStr.AppendFormat("<RemitNumber>{0}</RemitNumber>", dt.Rows[i]["RemitNumber"].ToString());
                XmlStr.AppendFormat("<RemittancesMoney>{0}</RemittancesMoney>", dt.Rows[i]["RemittancesMoney"].ToString());
                XmlStr.AppendFormat("<isgsqr>{0}</isgsqr>", GetPayStatus(dt.Rows[i]["isgsqr"].ToString()));
                XmlStr.AppendFormat("<ReceivablesDate>{0}</ReceivablesDate>", bool.Parse(dt.Rows[i]["isgsqr"].ToString()) == false ? "" : DateTime.Parse(dt.Rows[i]["ReceivablesDate"].ToString()).ToString("yyyy-MM-dd"));
                XmlStr.AppendFormat("<PayexpectNum>{0}</PayexpectNum>", bool.Parse(dt.Rows[i]["isgsqr"].ToString()) == false ? "" : dt.Rows[i]["PayexpectNum"].ToString());
                XmlStr.AppendFormat("<PayWay>{0}</PayWay>", bool.Parse(dt.Rows[i]["isgsqr"].ToString()) == false ? "无" : GetPayWay(dt.Rows[i]["PayWay"].ToString()));
                XmlStr.AppendFormat("<ImportBank>{0}</ImportBank>", dt.Rows[i]["ImportBank"].ToString());
                XmlStr.AppendFormat("<Managers>{0}</Managers>", dt.Rows[i]["Managers"].ToString());
                XmlStr.AppendFormat("<Sender>{0}</Sender>", dt.Rows[i]["Sender"].ToString());
                XmlStr.AppendFormat("<SenderID>{0}</SenderID>", dt.Rows[i]["SenderID"].ToString());
                XmlStr.AppendFormat("<Remark>{0}</Remark>", dt.Rows[i]["Remark"].ToString());

                XmlStr.Append("</resultBrowse>");
            }

            XmlStr.Append("</RechargeBrowse>");


            return XmlStr.ToString();
        }
        else
            return "请传入相关的参数";
    }
    /// <summary>
    /// 支付状态
    /// </summary>
    /// <param name="paytype"></param>
    /// <returns></returns>
    public string GetPayStatus(string paytype)
    {
        int type = 0;
        if (paytype.ToLower() == "true")
        {
            type = 1;
        }
        string payStatus = GetDeftrayState(type.ToString());
        return payStatus;
    }
    /// <summary>
    /// 支付状态
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public string GetDeftrayState(string id)
    {
        string deftrayState = null;
        switch (id)
        {
            case "0":
                deftrayState = "未支付";
                break;
            case "1":
                deftrayState = "已支付";
                break;
            case "2":
                deftrayState = "未支付";
                break;
        }

        return deftrayState;
    }
    /// <summary>
    /// 获取付款方式
    /// </summary>
    /// <param name="PayWay"></param>
    /// <returns></returns>
    protected string GetPayWay(string PayWay)
    {
        switch (PayWay)
        {
            case "0":
                return "在线支付";
            case "1":
                return "普通汇款";
            case "2":
                return "汇款人工确认";
        }
        return "";
    }
    /// <summary>
    /// 充值浏览-删除
    /// </summary>
    /// <param name="canshus"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string DelResultBrowse(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);//会员编号
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }


            string[] str_parmeters = canshus.Split(','); //参数分解

            string delId = str_parmeters[0]; //id编号

            if (delId == string.Empty)
            {
                return "请传入参数";
            }


            ChangeLogs cl = new ChangeLogs("Remittances", "ltrim(rtrim(str(id)))");


            //判断汇款是否被删除
            bool blean = RemittancesBLL.MemberIsExist(int.Parse(delId));
            if (blean == false)
            {
                return "不能重复删除！";
            }
            cl.AddRecord(delId);

            if (delId == "" || delId == null)
            {
                return "参数出错！";
            }
            //判断是否审核，不能删除已审核的单子
            Object obj = RemittancesBLL.IsMemberGSQR(int.Parse(delId));
            try
            {
                bool b = bool.Parse(obj.ToString());
                if (b == true)
                {
                    return "不能删除已审核的单子！";
                }
            }
            catch
            {
                return "类型转换错误！";
            }

            //删除未审核的单子
            RemittancesBLL.DeleteMemberMoney(Convert.ToInt32(delId));

            cl.AddRecord(delId);
            cl.ModifiedIntoLogs(ChangeCategory.member1, delId, ENUM_USERTYPE.objecttype5);

            return "成功删除！";
        }
        else
            return "请传入相关的参数";
    }
    /// <summary>
    /// 充值浏览-支付
    /// </summary>
    /// <param name="canshus"></param>
    /// <returns></returns>
    [WebMethod]
    public string PayResultBrowse(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }


            string[] str_parmeters = canshus.Split(','); //参数分解

            string remittancesid = str_parmeters[0]; //汇款单号
            if (remittancesid.Trim().Length == 0)
            {
                return "请输入参数！";
            }

            //判断是否审核，不能删除已审核的单子
            Object obj = DAL.DBHelper.ExecuteScalar("select IsGSQR from Remittances where RemitStatus=1 and RemittancesID='" + remittancesid + "'");

            if (obj != null)
            {
                bool b = bool.Parse(obj.ToString());
                if (b == true)
                {
                    return "不能支付已审核的单子！";
                }
            }
            else
            {
                return "记录不存在！";
            }

            string billid = EncryKey.GetEncryptstr(remittancesid, 2, 1);

            return "blif=" + billid;
        }
        else
            return "请传入相关的参数";
    }
    #endregion

    #region 手机充值
    [WebMethod(EnableSession = true)]
    public string PhoneRecharge(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }


            string[] str_parmeters = canshus.Split(','); //参数分解

            string number = str_parmeters[0]; //会员编号
            string phonenum = str_parmeters[1];//手机号码
            string money = str_parmeters[2];  //充值金额



            if (String.IsNullOrEmpty(phonenum))
            {
                return "请输入手机号码！";
            }
            else if (!PhoneRechargeBLL.CheckPhoneNumber(phonenum))
            {
                return "手机号码格式错误！";
            }

            if (MemberInfoDAL.CheckState(Session["Member"].ToString()))
            {
                return "会员账户已冻结，不能完成支付!";
            }

            PhoneRecharge pr = new PhoneRecharge();
            pr.RechargeID = new PhoneRechargeBLL().GetRechargeID();
            pr.Number = number;
            pr.AddMoney = Convert.ToDecimal(money);
            pr.AddState = 1;
            pr.PhoneNumber = phonenum;
            pr.AddTime = DateTime.Now.ToUniversalTime();
            pr.OperateIP = CommonDataBLL.OperateIP;
            pr.OperaterNum = number;
            string result = PhoneRechargeBLL.AddRecharge(pr);

            if (String.Equals(result, "1"))
            {
                return "支付失败,账户可用余额不足！";
            }
            else if (String.Equals(result, "fail"))
            {
                return "手机充值失败，请重新操作！";
            }
            else if (String.Equals(result, "ok"))
            {
                string url = "http://222.66.13.70/DS2012_MVS/phonerecharge/chongzhi.aspx";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                request.Accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/ag-plugin, application/xaml+xml, application/vnd.ms-xpsdocument, application/x-ms-xbap, application/x-ms-application, */*";
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 3.0.04506.30)";

                request.Method = "post";
                request.KeepAlive = true;
                request.Headers.Add("Accept-Language", "zh-cn,zh;q=0.5");
                request.Headers.Add("Accept-Charset", "GB2312,utf-8;q=0.7,*;q=0.7");
                request.ContentType = "application/x-www-form-urlencoded";

                Encoding ec = Encoding.GetEncoding("gb2312");
                Byte[] bt = ec.GetBytes("txtPhoneNumber=" + pr.PhoneNumber + "&ddlMoney=" + pr.AddMoney.ToString() + "&RechargeID=" + pr.RechargeID);
                request.ContentLength = bt.Length;

                Stream streamrequest = request.GetRequestStream();
                streamrequest.Write(bt, 0, bt.Length);

                return "手机充值操作完成，请等候5~10分钟话费将会充值到您的手机！";
            }

            return "充值失败！";
        }
        else
            return "请传入相关的参数";
    }
    #endregion

    #region 手机充值查询
    /// <summary>
    /// 手机充值查询
    /// </summary>
    /// <param name="parmeters">加密参数</param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string FindRecharge(string canshus)
    {

        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);//会员编号
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }



            string[] str_parmeters = canshus.Split(',');

            //会员编号
            string number = str_parmeters[0];

            //查询条件
            string mobile = CommonDataBLL.quanjiao(str_parmeters[1]); //手机号码
            string states = str_parmeters[2]; //充值状态


            LoadLanguage();//默认语言指定


            if (!String.IsNullOrEmpty(mobile) && !PhoneRechargeBLL.CheckPhoneNumber(mobile))
            {
                return "手机号码格式错误";
            }

            string strwhere = " ";

            if (states != "-1")
            {
                strwhere += " and  AddState=" + states + " ";
            }
            if (!String.IsNullOrEmpty(mobile))
            {
                strwhere += " and  PhoneNumber=" + mobile + " ";
            }

            BLL.Registration_declarations.PagerParmsInit model = PhoneRechargeBLL.FindPhoneRecharge(strwhere);

            StringBuilder XmlStr = new StringBuilder();

            DataTable dt = DAL.DBHelper.ExecuteDataTable("select " + model.PageColumn + "  from " + model.PageTable + "  where " + model.SqlWhere + "  order by " + model.Key + " desc");

            if (dt == null && dt.Rows.Count == 0)
            {
                return "暂无该会员相关信息";
            }

            XmlStr.Append("<Mobilephonerechargequery>");

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                XmlStr.Append("<FindRecharge id='" + (i + 1) + "'>");

                XmlStr.AppendFormat("<id>{0}</id>", dt.Rows[0]["id"].ToString());

                XmlStr.AppendFormat("<rechargeid>{0}</rechargeid>", dt.Rows[0]["rechargeid"].ToString());
                XmlStr.AppendFormat("<number>{0}</number>", dt.Rows[0]["number"].ToString());
                XmlStr.AppendFormat("<phonenumber>{0}</phonenumber>", dt.Rows[0]["phonenumber"].ToString());
                XmlStr.AppendFormat("<addmoney>{0}</addmoney>", dt.Rows[0]["addmoney"].ToString());
                XmlStr.AppendFormat("<addstate>{0}</addstate>", dt.Rows[0]["addstate"].ToString());
                XmlStr.AppendFormat("<addtime>{0}</addtime>", GetBiaoZhunTime(dt.Rows[0]["addtime"].ToString()));

                XmlStr.Append("</FindRecharge>");
            }

            XmlStr.Append("</Mobilephonerechargequery>");


            return XmlStr.ToString();
        }
        else
            return "请传入相关的参数";

    }

    public string getst(DateTime dtime)
    {
        int timesd = Convert.ToInt32(DateTime.Now.Subtract(dtime).TotalHours);
        if (timesd < 144)
            return "未到账";
        else
            return "未到账";
    }
    #endregion

    #region 离线汇款管理
    /// <summary>
    /// 离线汇款管理----操作、到账情况、剩余有效期
    /// </summary>
    /// <param name="flag">到账情况</param>
    /// <param name="remittancesdate">离线汇款申请时间</param>
    /// <param name="memberflag">是否是离线汇款  0 非离线汇款   1 离线汇款</param>
    /// <param name="suremoney">确定汇款金额</param>
    /// <param name="totalmoney">应汇金额</param>
    /// <returns></returns>
    public string recstatue(string flag, string remittancesdate, string memberflag, string suremoney, string totalmoney)
    {
        string recstatue = ""; //到账情况
        string leavetime = ""; //剩余有效期
        string coninfo = "已充值"; //操作状态显示    如果为空，则隐藏
        bool confirmrmd = true; //操作按钮是否显示


        int timesd = Convert.ToInt32(DateTime.Now.Subtract(Convert.ToDateTime(remittancesdate).AddHours(8)).TotalHours);
        TimeSpan ts = ((Convert.ToDateTime(remittancesdate).AddHours(8)).AddHours(48).Subtract(DateTime.Now));
        if (timesd > 48)
        {
            leavetime = "已过期"; //此汇款单已超时，不能进行操作！
            confirmrmd = false;
        }
        else
            leavetime = (ts.Days * 24 + ts.Hours).ToString() + "小时" + ts.Minutes.ToString() + "分" + ts.Seconds.ToString() + "秒";


        if (Convert.ToInt32(flag) == 1)
        {
            recstatue = "完成";
            leavetime = "";
        }
        else if (Convert.ToInt32(flag) == 4)
        {
            recstatue = "迟到账";
            leavetime = "";
        }
        else
        {
            recstatue = getst(Convert.ToDateTime(remittancesdate));
            if (Convert.ToInt32(memberflag) != 1)//非离线汇款
            {
                coninfo = "";
            }
            else
            {
                double smoney = Math.Floor(Convert.ToDouble(suremoney));
                double allmoney = Math.Floor(Convert.ToDouble(suremoney));

                if (smoney < allmoney)
                {
                    coninfo = "处理中";
                }
            }
        }



        StringBuilder XmlStr = new StringBuilder();


        XmlStr.AppendFormat("<recstatue>{0}</recstatue>", recstatue); //到账情况

        XmlStr.AppendFormat("<leavetime>{0}</leavetime>", leavetime); //剩余有效期

        XmlStr.AppendFormat("<confirmrmd>{0}</confirmrmd>", confirmrmd);//true 款已汇出按钮显示   false 款已汇出按钮隐藏
        XmlStr.AppendFormat("<coninfo>{0}</coninfo>", coninfo);//信息提示  若confirmrmd为false，则显示该信息


        return XmlStr.ToString();

    }

    /// <summary>
    /// 离线汇款管理
    /// </summary>
    /// <param name="parmeters">加密参数</param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string RemSecan(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);//会员编号
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }



            string[] str_parmeters = canshus.Split(',');

            //会员编号
            string number = str_parmeters[0];

            //查询条件
            string states = str_parmeters[1]; //充值状态
            string sTime = str_parmeters[2];  //申请开始时间
            string eTime = str_parmeters[3];  //申请结束时间


            LoadLanguage();//默认语言指定



            string cond = "mp.Remittancesid=rp.Remittancesid  and rp.number=mi.number  and mp.remitstatus=1 and rp.isusepay=1 and  (rp.orderid<>'' or rp.number='" + number + "')   ";
            string clonum = " mi.[name],  rp.number ,rp.totalrmbmoney,rp.totalmoney,rp.flag,mp.remittancesdate,mp.ReceivablesDate,mp.Remittancesid ,rp.memberflag,rp.suremoney ,rp.filepicname ";
            string table = " remtemp  rp,Remittances mp ,(select number,name  from memberinfo  where number='" + number + "' or direct ='" + number + "') mi ";

            if (states != "-1")
            {
                cond = cond + " and rp.flag=" + states;
            }

            if (sTime != "")
            {
                DateTime time = DateTime.Now.ToUniversalTime();
                bool b = DateTime.TryParse(sTime.Trim(), out time);
                if (!b)
                    return "时间格式不正确！";

                DisposeString.DisString(sTime, "'", "");
                if (sTime != "")
                {
                    sTime = (DateTime.Parse(sTime.Trim()).AddHours(-8).AddMinutes(0).AddSeconds(0)).ToString();
                    cond += " and mp.remittancesdate>= '" + sTime + "'";
                }
            }

            if (eTime != "")
            {
                DateTime time = DateTime.Now.ToUniversalTime();
                bool b = DateTime.TryParse(eTime.Trim(), out time);
                if (!b)
                    return "时间格式不正确！";

                eTime = (DateTime.Parse(eTime.Trim()).AddHours(15).AddMinutes(59).AddSeconds(59)).ToString();
                cond += " and mp.remittancesdate<='" + eTime + "'";
            }


            StringBuilder XmlStr = new StringBuilder();

            DataTable dt = DAL.DBHelper.ExecuteDataTable("select " + clonum + "  from " + table + "  where " + cond + "  order by mp.remittancesdate desc");

            if (dt == null && dt.Rows.Count == 0)
            {
                return "暂无该会员相关信息";
            }

            XmlStr.Append("<Offlinepaymentmanagement>");

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                XmlStr.Append("<FindRecharge id='" + (i + 1) + "'>");

                XmlStr.AppendFormat("<remittancesid>{0}</remittancesid>", dt.Rows[i]["RemittancesID"].ToString());
                XmlStr.AppendFormat("<number>{0}</number>", dt.Rows[i]["number"].ToString());
                XmlStr.AppendFormat("<name>{0}</name>", dt.Rows[i]["name"].ToString());

                XmlStr.AppendFormat("<totalrmbmoney>{0}</totalrmbmoney>", Convert.ToDouble(dt.Rows[i]["totalrmbmoney"].ToString()).ToString("F2"));
                XmlStr.AppendFormat("<remittancesdate>{0}</remittancesdate>", GetBiaoZhunTime(dt.Rows[i]["remittancesdate"].ToString()));
                XmlStr.AppendFormat("<receivablesDate>{0}</receivablesDate>", GetBiaoZhunTime(dt.Rows[i]["ReceivablesDate"].ToString()));

                XmlStr.Append(recstatue(dt.Rows[i]["flag"].ToString(), dt.Rows[i]["remittancesdate"].ToString(), dt.Rows[i]["memberflag"].ToString(), dt.Rows[i]["suremoney"].ToString(), dt.Rows[i]["totalmoney"].ToString()));

                XmlStr.Append("</FindRecharge>");
            }

            XmlStr.Append("</Offlinepaymentmanagement>");


            return XmlStr.ToString();
        }
        else
            return "请传入相关的参数";
    }
    /// <summary>
    /// 离线汇款管理----款已汇出 操作
    /// </summary>
    /// <param name="parmeters"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string Remitted(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }


            //参数分解
            string[] str_parmeters = canshus.Split(',');

            //参数
            string opnumber = str_parmeters[0]; //操作者编号
            string opip = str_parmeters[1]; //操作者IP
            string rimid = str_parmeters[2]; //汇款单号

            int ges = AddOrderDataDAL.PaymentRemitmoney(rimid, opnumber, opip, 1);
            if (ges == 0 || ges == 7)
                return "汇款充值确认操作成功！";
            else
                return "汇款充值确认失败，再次进行确认！";

        }
        else
            return "请传入相关的参数";
    }
    #endregion

    #region 账户明细
    /// <summary>
    /// 科目列表
    /// </summary>
    /// <param name="canshus"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string GetKmtype(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }


            //分解参数
            string[] str_canshus = canshus.Split(',');

            //参数
            string accountType = str_canshus[0]; //账户类型


            StringBuilder XmlStr = new StringBuilder();

            DataTable dt = CommonDataBLL.GetKmtype(accountType);

            if (dt == null && dt.Rows.Count == 0)
            {
                return "暂无相关信息";
            }

            LoadLanguage();

            XmlStr.Append("<KmType>");

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                XmlStr.Append("<ktype id='" + (i + 1) + "'>");

                XmlStr.AppendFormat("<SubjectID>{0}</SubjectID>", dt.Rows[i]["SubjectID"].ToString());
                XmlStr.AppendFormat("<SubjectName>{0}</SubjectName>", BLL.Translation.Translate(dt.Rows[i]["SubjectName"].ToString()));

                XmlStr.Append("</ktype>");
            }


            return XmlStr.ToString();
        }
        else
            return "请传入相关的参数";
    }
    /// <summary>
    /// 账户明细
    /// </summary>
    /// <param name="parmeters"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string AccountDetails(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }


            //分解参数
            string[] str_parmeters = canshus.Split(',');

            //参数
            string number = str_parmeters[0]; //会员编号
            string type = str_parmeters[1];   //账户类型
            string kmtype = str_parmeters[2]; //科目
            string outin = str_parmeters[3];  //-1 全部  0 转入  1 转出
            string sTime = str_parmeters[4];  //起始时间
            string eTime = str_parmeters[5];  //结束时间



            StringBuilder sb = new StringBuilder();
            string table = "";
            if (type == "AccountXJ")
            {
                table = "memberaccount";
                sb.Append("and number = '" + number + "' and SfType=1 ");
            }
            else if (type == "AccountXF")
            {
                table = "memberaccount";
                sb.Append("and number = '" + number + "' and SfType=0 ");
            }
            else if (type == "AccountDH")
            {
                table = "storeaccount";
                sb.Append("and number = '" + number + "' and SfType=0 ");
            }
            else if (type == "AccountZZ")
            {
                table = "storeaccount";
                sb.Append("and number = '" + number + "' and SfType=1 ");
            }
            string BeginRiQi = "";
            string EndRiQi = "";

            if (kmtype != "-1")
            {
                sb.Append(" and kmtype in(" + kmtype + ")");
            }
            if (outin != "-1")
            {
                sb.Append(" and Direction=" + outin);
            }

            if (sTime != "")
            {
                DateTime time = DateTime.Now.ToUniversalTime();
                bool b = DateTime.TryParse(sTime, out time);
                if (!b)
                {
                    return "时间格式不正确！";
                }
                BeginRiQi = sTime.Trim().ToString();
                DisposeString.DisString(BeginRiQi, "'", "");
                if (eTime != "")
                {
                    b = DateTime.TryParse(eTime, out time);
                    if (!b)
                    {
                        return "时间格式不正确！";
                    }
                    EndRiQi = (DateTime.Parse(eTime).AddHours(23).AddMinutes(59).AddSeconds(59)).ToString();
                    DisposeString.DisString(EndRiQi, "'", "");

                    sb.Append(" and happentime>= '" + BeginRiQi + "' and happentime<='" + EndRiQi + "'");
                }
                else
                {
                    sb.Append(" and happentime>= '" + BeginRiQi + "'");
                }
            }

            else
            {
                if (eTime != "")
                {
                    DateTime time = DateTime.Now.ToUniversalTime();
                    bool b = DateTime.TryParse(eTime, out time);
                    if (!b)
                    {
                        return "时间格式不正确！";
                    }
                    EndRiQi = (DateTime.Parse(eTime).AddHours(23).AddMinutes(59).AddSeconds(59)).ToString();
                    sb.Append(" and happentime<='" + EndRiQi + "'");
                }
            }
            DataTable dt_details = null;
            string heji = "";
            double currency = 1;
            string happenmoney = "happenmoney/" + currency + " as happenmoney";
            string Balancemoney = "Balancemoney/" + currency + " as Balancemoney";
            string cloumns = "id,number,happentime,Direction,sftype,kmtype,remark" + "," + happenmoney + "," + Balancemoney;
            string key = "id";
            if (table != "")
            {
                heji = GetHappenMoney("select isnull(sum(ABS(isnull(happenmoney,0))),0) as happenmoney from " + table + " where 1=1  " + sb.ToString(), outin);
                dt_details = DAL.DBHelper.ExecuteDataTable("select " + cloumns + " from " + table + " where 1=1  " + sb.ToString() + " order by " + key + " desc ");

            }
            else
            {
                return "账户类型错误";
            }

            LoadLanguage();

            StringBuilder XmlStr = new StringBuilder();

            XmlStr.Append("<AccountDetails>");

            for (int i = 0; i < dt_details.Rows.Count; i++)
            {
                XmlStr.Append("<ADetails id='ad_" + (i + 1) + "'>");

                XmlStr.AppendFormat("<kmType>{0}</kmType>", BLL.Logistics.D_AccountBLL.GetKmtype(dt_details.Rows[i]["kmtype"].ToString()));
                XmlStr.AppendFormat("<happentime>{0}</happentime>", DateTime.Parse(dt_details.Rows[i]["happentime"].ToString()).AddHours(Convert.ToInt32(Session["WTH"])));
                XmlStr.AppendFormat("<happenmoneyIn>{0}</happenmoneyIn>", dt_details.Rows[i]["Direction"].ToString() == "0" ? double.Parse(dt_details.Rows[i]["happenmoney"].ToString()).ToString("0.00") : "");
                XmlStr.AppendFormat("<happenmoneyOut>{0}</happenmoneyOut>", dt_details.Rows[i]["Direction"].ToString() == "1" ? Math.Abs(double.Parse(dt_details.Rows[i]["happenmoney"].ToString())).ToString("0.00") : "");
                XmlStr.AppendFormat("<Balancemoney>{0}</Balancemoney>", double.Parse(dt_details.Rows[i]["Balancemoney"].ToString()).ToString("0.00"));
                XmlStr.AppendFormat("<remark>{0}</remark>", PublicClass.getMark(dt_details.Rows[i]["remark"].ToString()));

                XmlStr.Append("</ADetails>");
            }

            XmlStr.Append("<AccountS>");
            //XmlStr.AppendFormat("<btitle>{0}</btitle>", GetHappenMoney("select isnull(sum(ABS(isnull(happenmoney,0))),0) as happenmoney  from " + table + "  where 1=1 " + sb.ToString(), "-1"));
            XmlStr.Append(GetHappenMoney("select isnull(sum(ABS(isnull(happenmoney,0))),0) as happenmoney  from " + table + "  where 1=1 " + sb.ToString(), "-1"));
            XmlStr.Append("</AccountS>");

            XmlStr.Append("</AccountDetails>");


            return XmlStr.ToString();

        }
        else
            return "请传入相关的参数";
    }
    /// <summary>
    /// 获取总入，总出
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="outin">类型：-1 所有  0 入  1 出</param>
    /// <returns></returns>
    private string GetHappenMoney(string sql, string outin)
    {
        string money = "0.00";

        if (outin != "-1")
        {
            money = DAL.DBHelper.ExecuteScalar(sql, CommandType.Text).ToString();
            if (outin == "0")
            {
                //money = "<inStr>转入</inStr>" + "<inMoney>" + double.Parse(money).ToString("0.00") + "</inMoney>";
                money = "<inMoney>" + double.Parse(money).ToString("0.00") + "</inMoney>";
            }
            else if (outin == "1")
            {
                //money = "<outStr>转出</outStr>" + "<outMoney>" + Math.Abs(double.Parse(money)).ToString("0.00") + "</outMoney>";
                money = "<outMoney>" + Math.Abs(double.Parse(money)).ToString("0.00") + "</outMoney>";
            }
        }
        else //全部
        {
            double money1 = double.Parse(DAL.DBHelper.ExecuteScalar(sql + " and Direction=0", CommandType.Text).ToString());
            double money2 = Math.Abs(double.Parse(DAL.DBHelper.ExecuteScalar(sql + " and direction=1", CommandType.Text).ToString()));

            //money = "<inStr>转入</inStr>" + "<inMoney>" + money1.ToString("0.00") + "</inMoney>" + "<outStr>转出</outStr>" + "<outMoney>" + money2.ToString("0.00") + "</outMoney>" + "<balanceStr>账户余额</balanceStr>" + "<balanceMoney>" + (money1 - money2).ToString("0.00") + "</balanceMoney>";
            money = "<inMoney>" + money1.ToString("0.00") + "</inMoney>" + "<outMoney>" + money2.ToString("0.00") + "</outMoney>" + "<balanceMoney>" + (money1 - money2).ToString("0.00") + "</balanceMoney>";
        }
        return money;
    }
    #endregion

    #region 提现申请
    /// <summary>
    /// 加载 提现申请信息/修改提现申请信息
    /// </summary>
    /// <param name="canshus"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string LoadMemberCashInfo(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }



            string[] str_parmeters = canshus.Split(',');

            //参数
            string number = str_parmeters[0]; //会员编号
            string id = str_parmeters[1];  //id编号  提现申请时为-1，修改提现申请时为大于0的id编号

            //变量定义
            string wMoney = "0.00"; //提现金额
            string leftMoney = "0.00"; //账户余额
            string bankbranchname = ""; //开户行
            string accountName = ""; //开户名
            string bankcard = ""; //银行卡号
            string remark = ""; //备注

            //银行信息
            string sql = "select name,bankname,bankaddress,bankbranchname,bankcard,bankbook from memberinfo m,memberbank mb where m.bankcode=mb.bankcode and number='" + number + "'";
            DataTable dt = DAL.DBHelper.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                bankbranchname = dt.Rows[0]["bankname"].ToString() + dt.Rows[0]["bankbranchname"].ToString();
                accountName = Encryption.Encryption.GetDecipherName(dt.Rows[0]["name"].ToString());
                bankcard = Encryption.Encryption.GetEncryptionCard(dt.Rows[0]["bankcard"].ToString());
            }

            if (id != "-1") //修改提现申请
            {
                DataTable dt_updInfo = BLL.Registration_declarations.RegistermemberBLL.QueryWithdraw(id);
                if (dt.Rows.Count > 0)
                {
                    number = dt.Rows[0]["number"].ToString();
                    wMoney = Convert.ToDouble(dt.Rows[0]["WithdrawMoney"].ToString()).ToString("0.00");
                    leftMoney = (Convert.ToDouble(dt.Rows[0]["WithdrawMoney"]) + Convert.ToDouble(BLL.CommonClass.CommonDataBLL.GetLeftMoney(number))).ToString("f2");
                    accountName = dt.Rows[0]["bankname"].ToString();
                    bankcard = dt.Rows[0]["bankcard"].ToString();
                    remark = dt.Rows[0]["remark"].ToString();
                }
            }
            else //提现申请
            {
                leftMoney = BLL.CommonClass.CommonDataBLL.GetLeftMoney(number);
            }

            //LoadLanguage();

            StringBuilder XmlStr = new StringBuilder();

            XmlStr.Append("<LoadMemberCashInfo>");

            XmlStr.Append("<mcInfo>");

            XmlStr.AppendFormat("<number>{0}</number>", number);
            XmlStr.AppendFormat("<wMoney>{0}</wMoney>", wMoney);
            XmlStr.AppendFormat("<leftMoney>{0}</leftMoney>", leftMoney);
            XmlStr.AppendFormat("<bankbranchname>{0}</bankbranchname>", bankbranchname);
            XmlStr.AppendFormat("<accountName>{0}</accountName>", accountName);
            XmlStr.AppendFormat("<bankcard>{0}</bankcard>", bankcard);
            XmlStr.AppendFormat("<remark>{0}</remark>", remark);

            XmlStr.Append("</mcInfo>");

            XmlStr.Append("</LoadMemberCashInfo>");

            return XmlStr.ToString();
        }
        else
            return "请传入相关的参数";
    }

    /// <summary>
    /// 提现申请/修改提现申请
    /// </summary>
    /// <param name="canshus">加密参数</param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string MemberCash(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }



            string[] str_parmeters = canshus.Split(',');

            //参数
            string editType = str_parmeters[0]; //类型   0 提现  1 修改提现
            string number = str_parmeters[1]; //会员编号
            string advpass = str_parmeters[2];//二级密码
            string money = str_parmeters[3];  //提现金额
            string bank = str_parmeters[4];  //开户银行
            string bankname = str_parmeters[5];  //开户名
            string bankcard = str_parmeters[6];  //银行账户
            string remark = str_parmeters[7];  //备注
            string id = str_parmeters[8];  //id   editTyp=1时，id不为空



            //判断会员账户是否被冻结
            if (DAL.MemberInfoDAL.CheckState(number))
            {
                return "您的账户被冻结，不能使用电子转账";
            }

            if (bank.Trim() == "" || bank.Trim() == "未填写")
            {
                return "用户开户银行不能为空!请先完善会员信息！";
            }
            if (bankname.Trim() == "" || bankname.Trim() == "未填写")
            {
                return "用户开户名不能为空!请先完善会员信息！";
            }
            if (bankcard.Trim() == "" || bankcard.Trim() == "未填写")
            {
                return "用户账户不能为空!请先完善会员信息！";
            }

            if (advpass.Trim().Length == 0)
            {
                return "请输入二级密码！";
            }
            if (money.Trim().Length == 0)
            {
                return "请输入提现金额！";
            }
            if (remark.Trim().Length > 500)
            {
                return "对不起，备注输入的字符太多,最多500个字符！";
            }

            bool f_m = false;
            double d_m = 0;
            f_m = double.TryParse(money, out d_m);
            if (!f_m)
            {
                return "提现金额必须为数值类型！";
            }

            string word = Encryption.Encryption.GetEncryptionPwd(advpass, number);
            int blean = ECRemitDetailBLL.ValidatePwd(number, word);
            if (blean == 1)
            {
                return "电子账户密码不正确！";
            }
            else if (blean == 2)
            {
                return "对不起，您连续5次输入密码错，请2小时候在登录！";
            }

            try
            {
                if (Convert.ToDouble(money) <= 0)
                {
                    return "金额不能小于等于0！";
                }

                if (editType == "0") //提现
                {
                    if (Convert.ToDouble(money) > Convert.ToDouble(BLL.CommonClass.CommonDataBLL.GetLeftMoney(number)))
                    {
                        return "金额超出最大可转金额！";
                    }
                }
                else //修改提现
                {

                    DataTable dt = BLL.Registration_declarations.RegistermemberBLL.QueryWithdraw(id);
                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToDouble(money) > (Convert.ToDouble(dt.Rows[0]["WithdrawMoney"]) + Convert.ToDouble(BLL.CommonClass.CommonDataBLL.GetLeftMoney(number))))
                        {
                            return "金额超出最大可转金额！";
                        }
                    }
                    else
                    {
                        return "金额超出最大可转金额！";
                    }

                }
            }
            catch
            {
                return "金额只能是数字！";
            }

            LoadLanguage();

            WithdrawModel wDraw = new WithdrawModel();
            wDraw.Number = number;
            wDraw.ApplicationExpecdtNum = BLL.CommonClass.CommonDataBLL.getMaxqishu();
            wDraw.WithdrawMoney = d_m;
            wDraw.WithdrawTime = DateTime.Now.ToUniversalTime();
            wDraw.OperateIP = BLL.CommonClass.CommonDataBLL.OperateIP;
            wDraw.Remark = remark;
            wDraw.Bankcard = bankcard;
            wDraw.Bankname = bankname;

            bool isSure = false;

            if (editType == "0")
            {
                isSure = BLL.Registration_declarations.RegistermemberBLL.WithdrawMoney(wDraw);
            }
            else
            {
                wDraw.Id = Convert.ToInt32(id);
                if (BLL.Registration_declarations.RegistermemberBLL.GetAuditState(wDraw.Id) == 3)
                {
                    return "该申请单账号错误，不可以修改！";
                }
                if (BLL.Registration_declarations.RegistermemberBLL.GetAuditState(wDraw.Id) == 2)
                {
                    return "该申请单已经开始处理，不可以修改！";
                }

                if (!BLL.Registration_declarations.RegistermemberBLL.isDelWithdraw(wDraw.Id))
                {
                    return "该申请单已经删除，不可以修改！";
                }
                isSure = BLL.Registration_declarations.RegistermemberBLL.updateWithdraw(wDraw);
            }

            if (isSure)
            {
                if (editType == "0")
                {
                    return "提现申请完成！";
                }
                else
                {
                    return "修改成功！";
                }

            }
            else
            {
                if (editType == "0")
                {
                    return "提现申请失败！";
                }
                else
                {
                    return "修改失败！";
                }
            }

        }
        else
            return "请传入相关的参数";
    }
    #endregion

    #region 提现申请浏览
    /// <summary>
    /// 提现申请浏览
    /// </summary>
    /// <param name="canshus"></param>
    /// <returns></returns>
    [WebMethod]
    public string MemberWithdraw(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }



            string[] str_parmeters = canshus.Split(',');

            //参数
            string number = str_parmeters[0]; //会员编号
            string startT = str_parmeters[1]; //开始时间
            string endT = str_parmeters[2]; //结束时间

            string conditions = " number='" + number + "' ";
            if (startT != "")
            {
                DateTime time = DateTime.Now.ToUniversalTime();
                bool b = DateTime.TryParse(startT.Trim(), out time);
                if (!b)
                {
                    return "时间格式不正确！";
                }
                else
                {
                    startT = time.ToUniversalTime().ToString();
                }
            }
            if (endT != "")
            {
                DateTime time = DateTime.Now.ToUniversalTime();
                bool b = DateTime.TryParse(endT.Trim(), out time);
                if (!b)
                {
                    return "时间格式不正确！";
                }
                else
                {
                    endT = time.AddDays(1).ToUniversalTime().ToString();
                }
            }
            if (startT != "" && endT != "")
            {
                conditions += " and WithdrawTime between '" + startT + "' and '" + endT + "'";
            }


            DataTable dt = DAL.DBHelper.ExecuteDataTable("select * from Withdraw where " + conditions + " order by WithdrawTime desc");


            StringBuilder XmlStr = new StringBuilder();

            XmlStr.Append("<Withdraw>");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                XmlStr.Append("<WithdrawDetails wdId='wd_" + i + "'>");

                XmlStr.AppendFormat("<id>{0}</id>", dt.Rows[i]["id"].ToString());
                XmlStr.AppendFormat("<isAuditing>{0}</isAuditing>", GetAuditState(dt.Rows[i]["isAuditing"].ToString())); //审核状态
                XmlStr.AppendFormat("<WithdrawMoney>{0}</WithdrawMoney>", Convert.ToDouble(dt.Rows[i]["WithdrawMoney"]).ToString("f2"));//提现金额
                XmlStr.AppendFormat("<WithdrawTime>{0}</WithdrawTime>", PublicClass.GetBiaoZhunTime(dt.Rows[i]["WithdrawTime"].ToString()));//申请时间
                XmlStr.AppendFormat("<AuditExpectNum>{0}</AuditExpectNum>", GetAuditExpectNum(dt.Rows[i]["AuditExpectNum"].ToString()));//审核期数
                XmlStr.AppendFormat("<AuditingTime>{0}</AuditingTime>", GetAuditTime(dt.Rows[i]["AuditingTime"].ToString()));//审核时间
                XmlStr.AppendFormat("<bankcard>{0}</bankcard>", dt.Rows[i]["bankcard"].ToString());

                XmlStr.Append("</WithdrawDetails>");
            }

            XmlStr.Append("</Withdraw>");


            return XmlStr.ToString();
        }
        else
            return "请传入相关的参数";
    }
    protected string GetAuditState(string auditState)
    {
        string msg = "";
        switch (auditState)
        {
            case "0":
                msg = "处理中";
                break;
            case "1":
                msg = "处理中";
                break;
            case "2":
                msg = "已汇出";
                break;
            case "3":
                msg = "账号有误";
                break;
            default:
                msg = "待审核";
                break;
        }
        return msg;
    }
    protected string GetAuditExpectNum(string AuditExpect)
    {
        if (AuditExpect == "0")
        {
            return "";
        }
        else
        {
            return AuditExpect.ToString();
        }
    }
    protected string GetAuditTime(string AuditTime)
    {
        if (AuditTime == "1900-1-1 0:00:00")
        {
            return "";
        }
        else
        {
            return DateTime.Parse(AuditTime).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToShortDateString();
        }
    }
    /// <summary>
    /// 提现申请删除
    /// </summary>
    /// <param name="canshus"></param>
    /// <returns></returns>
    [WebMethod]
    public string DelWithdraw(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }



            string[] str_parmeters = canshus.Split(',');

            //参数
            string id = str_parmeters[0]; //提现申请的id号

            DataTable dt = RegistermemberBLL.QueryWithdraw(id);
            if (dt.Rows.Count > 0)
            {
                if (RegistermemberBLL.GetAuditState(int.Parse(id)) == 1)
                {
                    return "该申请单已经审核，不可以删除！";
                }
                if (RegistermemberBLL.GetAuditState(int.Parse(id)) == 2)
                {
                    return "该申请单已经开始处理，不可以删除！";
                }
                if (RegistermemberBLL.GetAuditState(int.Parse(id)) == 3)
                {
                    return "该申请单已经是账号错误，不可以删除！";
                }

                using (SqlConnection conn = new SqlConnection(DAL.DBHelper.connString))
                {
                    conn.Open();
                    SqlTransaction tran = conn.BeginTransaction();
                    try
                    {
                        if (!DAL.ECTransferDetailDAL.DeleteWithdraw(tran, int.Parse(id), Convert.ToDouble(dt.Rows[0]["WithdrawMoney"].ToString()), dt.Rows[0]["number"].ToString()))
                        {
                            tran.Rollback();
                            return "删除失败！";
                        }
                        else
                        {
                            tran.Commit();
                            return "删除成功！";
                        }
                    }
                    catch
                    {
                        tran.Rollback();
                        return "删除失败！";
                    }
                    finally
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }
            else
            {
                return "不能重复删除！";
            }

        }
        else
            return "请传入相关的参数";
    }
    #endregion

    #region 电子转账
    /// <summary>
    /// 电子转账
    /// </summary>
    /// <param name="canshus"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string ElectronicFundsTransfer(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }



            string[] str_parmeters = canshus.Split(',');

            //参数
            string outnumber = str_parmeters[0]; //转出人编号
            string outaccout = str_parmeters[1]; //转出账户类型
            string outmoney = str_parmeters[2]; //转出金额
            string outadvpass = str_parmeters[3]; //转出人二级密码

            string innumber = str_parmeters[4]; //转入人编号
            string inaccout = str_parmeters[5]; //转入账户类型
            string check = str_parmeters[6]; //是否收到该会员支付的上数金额
            string note = str_parmeters[7]; //备注


            //判断会员账户是否被冻结
            if (MemberInfoDAL.CheckState(outnumber))
            {
                return "您的账户被冻结，不能使用电子转账";
            }

            ECTransferDetailModel detailmodel = new ECTransferDetailModel();

            //验证金额是否合法
            double money = 0.0;
            if (!double.TryParse(outmoney, out money))
            {
                return "金额必须是数字，请重新输入！";
            }
            //验证是否输入金额
            if (outmoney.Length <= 0 || money <= 0)
            {
                return "转出金额必须大于0！";
            }
            double Cash = 0;//现金账户
            double Declarations = 0;//报单账户
            ECRemitDetailBLL.GetCashDeclarations(outnumber, out Cash, out Declarations);

            //验证转账金额最大值
            if (outaccout == ((int)OutAccountType.MemberCash).ToString() && money > Cash)
            {
                return "转出金额必须小于当前现金账户最大可转金额！";
            }
            else if (outaccout == ((int)OutAccountType.MemberCons).ToString() && money > Declarations)
            {
                return "转出金额必须小于当前消费账户最大可转金额！";
            }


            //验证转入编号
            if (innumber.Trim() == "")
            {
                return "编号不能为空！";
            }

            //验证会员是否自己给自己转入现金账户
            if (innumber.Trim() == outnumber.Trim() && outaccout == "0" && inaccout == "0")
            {
                return "自己不能转入自己的现金账户";
            }
            //验证会员是否自己给自己转入消费账户
            if (innumber.Trim() == outnumber.Trim() && outaccout == "1" && inaccout == "1")
            {
                return "自己不能转入自己的消费账户";
            }

            //验证会员转入账户是否服务机构订货款和转入编号是否服务机构编号
            if (inaccout == "2" && (int)DAL.DBHelper.ExecuteScalar("select count(0) as count from storeinfo where storeid= '" + innumber.Trim() + "'") <= 0)
            {
                return "转入服务机构订货款，转入编号必须是服务机构编号";
            }
            else if (inaccout != "2" && (int)DAL.DBHelper.ExecuteScalar("select count(0) as count from memberinfo where number= '" + innumber.Trim() + "'") <= 0)
            {
                return "转入编号不正确，请重新填写!";
            }

            //验证备注do
            if (note.Trim().Length > 500)
            {
                return "您输入的字符超出最大范围！";
            }
            //验证密码 非空
            if (outadvpass == "")
            {
                return "请输入二级密码！";
            }

            //验证电子账户密码是否正确
            string word = Encryption.Encryption.GetEncryptionPwd(outadvpass, outnumber);
            int blean = ECRemitDetailBLL.ValidatePwd(outnumber, word);
            if (blean == 1)
            {
                return "电子账户密码不正确！";
            }
            else if (blean == 2)
            {
                return "对不起，您连续5次输入密码错，请2小时候在登录！";
            }
            //是否收到款项
            if (check == "2")
            {
                return "请先确认是否收到该会员支付的上数金额";
            }


            LoadLanguage();

            detailmodel.OutNumber = outnumber;
            detailmodel.OutMoney = double.Parse(outmoney);
            detailmodel.ExpectNum = CommonDataBLL.getMaxqishu();
            detailmodel.InNumber = innumber;
            detailmodel.OperateIP = CommonDataBLL.OperateIP;// 获取ip
            detailmodel.OperateNumber = outnumber;// 获取当前会员编号
            detailmodel.Remark = note;
            detailmodel.outAccountType = (OutAccountType)int.Parse(outaccout);
            detailmodel.inAccountType = (InAccountType)int.Parse(inaccout);

            string msgInfo = "操作失败！";

            SqlTransaction tran = null;
            SqlConnection conn = DAL.DBHelper.SqlCon();
            conn.Open();
            tran = conn.BeginTransaction();
            try
            {
                //对服务机构,消费账户转服务机构订货款
                if (detailmodel.outAccountType == OutAccountType.MemberCons && detailmodel.inAccountType == InAccountType.StoreOrder)
                {
                    if (ECRemitDetailBLL.AddMoneyManageTran(detailmodel, 0, 0, tran) == 0)
                    {
                        int ret = D_AccountBLL.AddAccountWithdrawTran(outnumber, detailmodel.OutMoney, D_AccountSftype.MemberCoshType, D_Sftype.EleAccount, D_AccountKmtype.AccountTransfer, DirectionEnum.AccountReduced, "007988~" + innumber + "~007992", tran);
                        int ret1 = D_AccountBLL.AddStoreAccount(innumber, detailmodel.OutMoney, D_AccountSftype.StoreType, S_Sftype.dianhuo, D_AccountKmtype.RechargeByTransfer, DirectionEnum.AccountsIncreased, outnumber + "~007989~" + innumber + "~007993~" + detailmodel.OutMoney + "~000564", tran);
                        if (ret > 0 && ret1 > 0)
                        {
                            tran.Commit();
                            msgInfo = "转帐成功！";
                        }
                        else
                        {
                            tran.Rollback();
                            msgInfo = "转帐失败！";
                        }
                    }
                    else
                    {
                        tran.Rollback();
                        msgInfo = "转帐失败！";
                    }
                }//对会员
                else
                {
                    if (detailmodel.outAccountType == OutAccountType.MemberCash && detailmodel.inAccountType == InAccountType.MemberCons)
                    {
                        //对会员现金账户转入会员消费账户
                        if (ECRemitDetailBLL.AddMoneyManageTran(detailmodel, 1, 1, tran) == 0)
                        {
                            int ret = D_AccountBLL.AddAccountWithdrawTran(outnumber, detailmodel.OutMoney, D_AccountSftype.MemberType, D_Sftype.BounsAccount, D_AccountKmtype.AccountTransfer, DirectionEnum.AccountReduced, "007990~" + innumber + "~007260", tran);
                            int ret1 = D_AccountBLL.AddAccountTran(innumber, detailmodel.OutMoney, D_AccountSftype.MemberCoshType, D_Sftype.EleAccount, D_AccountKmtype.RechargeByTransfer, DirectionEnum.AccountsIncreased, outnumber + "~007991~" + innumber + "~007988~" + detailmodel.OutMoney + "~000564", tran);
                            if (ret > 0 && ret1 > 0)
                            {
                                tran.Commit();
                                msgInfo = "转帐成功！";
                            }
                            else
                            {
                                tran.Rollback();
                                msgInfo = "转帐失败！";
                            }

                        }
                        else
                        {
                            tran.Rollback();
                            msgInfo = "转帐失败！";
                        }
                    }
                    else if (detailmodel.outAccountType == OutAccountType.MemberCash && detailmodel.inAccountType == InAccountType.MemberCash)
                    {
                        //对会员现金账户转入会员现金账户
                        if (ECRemitDetailBLL.AddMoneyManageTran(detailmodel, 1, 2, tran) == 0)
                        {
                            int ret = D_AccountBLL.AddAccountWithdrawTran(outnumber, detailmodel.OutMoney, D_AccountSftype.MemberType, D_Sftype.BounsAccount, D_AccountKmtype.AccountTransfer, DirectionEnum.AccountReduced, "007990~" + innumber + "~007259", tran);
                            int ret1 = D_AccountBLL.AddAccountTran(innumber, detailmodel.OutMoney, D_AccountSftype.MemberType, D_Sftype.BounsAccount, D_AccountKmtype.RechargeByTransfer, DirectionEnum.AccountsIncreased, outnumber + "~007991~" + innumber + "~007990~" + detailmodel.OutMoney + "~000564", tran);
                            if (ret > 0 && ret1 > 0)
                            {
                                tran.Commit();
                                msgInfo = "转帐成功！";
                            }
                            else
                            {
                                tran.Rollback();
                                msgInfo = "转帐失败！";
                            }

                        }
                        else
                        {
                            tran.Rollback();
                            msgInfo = "转帐失败！";
                        }
                    }
                    else if (detailmodel.outAccountType == OutAccountType.MemberCons && detailmodel.inAccountType == InAccountType.MemberCons)
                    {
                        //对会员消费账户转入会员消费账户
                        if (ECRemitDetailBLL.AddMoneyManageTran(detailmodel, 0, 1, tran) == 0)
                        {
                            int ret = D_AccountBLL.AddAccountWithdrawTran(outnumber, detailmodel.OutMoney, D_AccountSftype.MemberCoshType, D_Sftype.EleAccount, D_AccountKmtype.AccountTransfer, DirectionEnum.AccountReduced, "007988~" + innumber.Trim() + "~007260", tran);
                            int ret1 = D_AccountBLL.AddAccountTran(innumber, detailmodel.OutMoney, D_AccountSftype.MemberCoshType, D_Sftype.EleAccount, D_AccountKmtype.RechargeByTransfer, DirectionEnum.AccountsIncreased, outnumber.Trim() + "~007989~" + innumber.Trim() + "~007988~" + detailmodel.OutMoney + "~000564", tran);
                            if (ret > 0 && ret1 > 0)
                            {
                                tran.Commit();
                                msgInfo = "转帐成功！";
                            }
                            else
                            {
                                tran.Rollback();
                                msgInfo = "转帐失败！";
                            }

                        }
                        else
                        {
                            tran.Rollback();
                            msgInfo = "转帐失败！";
                        }
                    }
                }
            }
            catch
            {
                tran.Rollback();
                msgInfo = "转帐失败！";
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }

            return msgInfo;
        }
        else
            return "请传入相关的参数";
    }
    #endregion

    #region 转账浏览
    /// <summary>
    /// 转账浏览
    /// </summary>
    /// <param name="canshus"></param>
    /// <returns></returns>
    [WebMethod(EnableSession=true)]
    public string TransferList(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }



            string[] str_parmeters = canshus.Split(',');

            //参数
            string outnumber = str_parmeters[0]; //转出会员编号
            string inaccount = str_parmeters[1]; //转入账户
            string outaccout = str_parmeters[2]; //转出账户
            string outmoney = str_parmeters[3]; //转账金额
            string operationsymbol = str_parmeters[4]; //运算符号

            string conditions = " outNumber='" + outnumber + "' ";
            if (inaccount != "-1")
            {
                conditions += " and InAccountType=" + inaccount;
            }
            if (outaccout != "-1")
            {
                conditions += " and OutAccountType=" + outaccout;
            }
            double OutMoney = 0;
            if (outmoney.Trim() != "")
            {
                if (double.TryParse(outmoney.Trim(), out OutMoney))
                {
                    conditions += " and OutMoney" + operationsymbol + OutMoney;
                }
                else
                {
                    return "金额输入不正确！";
                }
            }



            DataTable dt = DAL.DBHelper.ExecuteDataTable("select * from ECTransferDetail where " + conditions + " order by RemittancesDate desc");

            if (dt == null || dt.Rows.Count == 0)
            {
                return "暂无相关数据";
            }

            LoadLanguage();

            StringBuilder XmlStr = new StringBuilder();

            XmlStr.Append("<TransferList>");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                XmlStr.Append("<TList tId='t_" + (i + 1) + "'>");

                XmlStr.AppendFormat("<id>{0}</id>", dt.Rows[i]["ID"].ToString());
                XmlStr.AppendFormat("<innumber>{0}</innumber>", dt.Rows[i]["InNumber"].ToString()); //收款人编号
                XmlStr.AppendFormat("<inAccountType>{0}</inAccountType>", GetInAccountType(Convert.ToInt32(dt.Rows[i]["InAccountType"])));//转入账户类型
                XmlStr.AppendFormat("<outAccountType>{0}</outAccountType>", GetOutAccountType(Convert.ToInt32(dt.Rows[i]["OutAccountType"])));//转出账户类型
                XmlStr.AppendFormat("<expectNum>{0}</expectNum>", GetAuditExpectNum(dt.Rows[i]["ExpectNum"].ToString()));//转账期数
                XmlStr.AppendFormat("<outMoney>{0}</outMoney>", Convert.ToDouble(dt.Rows[i]["OutMoney"]).ToString("f2"));//转账金额
                XmlStr.AppendFormat("<date>{0}</date>", PublicClass.GetBiaoZhunTime(dt.Rows[i]["Date"].ToString()));//转账时间
                XmlStr.AppendFormat("<tf_remark>{0}</tf_remark>", dt.Rows[i]["remark"].ToString());//备注

                XmlStr.Append("</TList>");
            }

            XmlStr.Append("</TransferList>");


            return XmlStr.ToString();
        }
        else
            return "请传入相关的参数";
    }
    /// <summary>
    /// 获取转入账户类型
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    protected string GetInAccountType(int type)
    {
        switch (type)
        {
            case (int)InAccountType.MemberCash:
                return "会员现金账户";
            case (int)InAccountType.MemberCons:
                return "会员消费账户";
            case (int)InAccountType.StoreOrder:
                return "服务机构订货款";
        }
        return "";
    }
    /// <summary>
    /// 获取转出账户类型
    /// </summary>
    /// <param name="Type"></param>
    /// <returns></returns>
    protected string GetOutAccountType(int Type)
    {
        switch (Type)
        {
            case (int)OutAccountType.MemberCash:
                return "会员现金账户";
            case (int)OutAccountType.MemberCons:
                return "会员消费账户";
        }
        return "";
    }
    #endregion

    #endregion

    #region 个性修改

    #region 二级密码验证
    [WebMethod]
    public string CheckAdv(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }

            string[] str_info = canshus.Split(',');

            if (str_info.Length == 3)
            {
                string bianhao = str_info[0].Trim(); //服务机构编号/会员编号
                string type = str_info[1].Trim(); //类型 member store
                string inputPass = str_info[2].Trim(); //输入的密码

                if (bianhao == "" || type == "" || inputPass == "")
                {
                    return "传入的相关参数不正确";
                }

                if (type == "member")
                {
                    string oldPass = Encryption.Encryption.GetEncryptionPwd(inputPass, bianhao);
                    int n = PwdModifyBLL.check(bianhao, oldPass, 1);
                    if (n > 0)
                    {
                        return "success";
                    }
                    else
                    {
                        return "二级密码不正确！";
                    }
                }
                else if (type == "store")
                {
                    string oldPass = Encryption.Encryption.GetEncryptionPwd(inputPass, bianhao);
                    int n = PwdModifyBLL.checkstoreadvpass(bianhao, oldPass);
                    if (n == 0)
                    {
                        return "success";
                    }
                    else if (n == 1)
                    {
                        return "二级密码不正确！";
                    }
                    else if (n == 2)
                    {
                        return "二级密码输入次数过多";
                    }
                }

                return "类型不正确！";
            }
            else
                return "传入的相关参数不正确";
        }
        else
            return "请传入相关参数";
    }
    #endregion

    #region 密码修改
    /// <summary>
    /// 修改密码
    /// </summary>
    /// <param name="parmeters">加密参数</param>
    /// <returns></returns>
    [WebMethod(EnableSession=true)]
    public string updatePass(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }

            string[] str_updPass = canshus.Split(',');

            if (str_updPass.Length == 6)
            {
                string sysType = str_updPass[0];//0 公司  1 服务机构  2 会员
                int passType = Convert.ToInt32(str_updPass[1]);//0 登陆密码  1 二级密码
                string bianhao = str_updPass[2].Trim();//管理员编号/服务机构编号/会员编号
                string oldpass = str_updPass[3];//原 登陆密码/二级密码
                string newpass1 = str_updPass[4].Trim();//新 登陆密码/二级密码 输入密码
                string newpass2 = str_updPass[5].Trim();//新 登陆密码/二级密码 确认密码

                int n = 0;
                string str = "";

                if (passType == 0)
                    str = "一级密码";
                else
                    str = "二级密码";

                LoadLanguage();//默认语言指定

                if (sysType == "1")//服务机构
                {

                    if (newpass1.Trim().Length < 4 || newpass1.Trim().Length > 10)
                        return "密码长度必须在4到10之间！";

                    string StoreID = Session["Store"].ToString();
                    string NewPass = Encryption.Encryption.GetEncryptionPwd(newpass1, bianhao);
                    string oldPass = Encryption.Encryption.GetEncryptionPwd(oldpass, bianhao);

                    if (passType == 0)
                    {
                        n = PwdModifyBLL.checkstore(StoreID, oldPass, 0);
                        if (n > 0)
                            n = 0;
                        else
                            n = 1;
                    }
                    else
                    {
                        n = PwdModifyBLL.checkstoreadvpass(StoreID, oldPass);
                    }
                    if (n == 0)
                    {
                        if (newpass1 == newpass2)
                        {
                            BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("StoreInfo", "ltrim(rtrim(StoreID))");
                            cl_h_info.AddRecord(StoreID);

                            int i = 0;
                            if (passType == 0)
                                i = PwdModifyBLL.updStorePass(StoreID, NewPass);
                            else
                                i = PwdModifyBLL.updStoreadvPass(StoreID, NewPass);

                            if (i > 0)
                            {
                                cl_h_info.AddRecord(StoreID);
                                cl_h_info.ModifiedIntoLogs(BLL.CommonClass.ChangeCategory.store8, bianhao, BLL.CommonClass.ENUM_USERTYPE.objecttype6);

                                return "密码修改成功！";
                            }
                            else
                                return "密码修改失败！";
                        }
                        else
                            return "两次密码不一样";
                    }
                    else
                        return "原始密码不正确！";

                }
                else if (sysType == "2")//会员
                {
                    if (newpass1 != "" && newpass2 != "" && oldpass != "")
                    {
                        if (newpass1.Trim().Length < 4 || newpass1.Trim().Length > 10)
                            return "密码长度必须在4到10之间！";

                        if (newpass1 != newpass2)
                            return "两次密码不一样！";

                        string NewPass = Encryption.Encryption.GetEncryptionPwd(newpass1, bianhao);
                        string oldPass = Encryption.Encryption.GetEncryptionPwd(oldpass, bianhao);

                        n = PwdModifyBLL.check(bianhao, oldPass, Convert.ToInt32(passType));

                        if (n > 0)
                        {
                            BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("Memberinfo", "ltrim(rtrim(number))");
                            cl_h_info.AddRecord(bianhao);

                            int i = 0;
                            i = PwdModifyBLL.updateMemberPass(bianhao, NewPass, passType);
                            if (i > 0)
                            {
                                cl_h_info.AddRecord(bianhao);
                                cl_h_info.ModifiedIntoLogs(BLL.CommonClass.ChangeCategory.member3, bianhao, BLL.CommonClass.ENUM_USERTYPE.objecttype6);

                                return str + "修改成功";
                            }
                            else
                                return str + "修改失败！";
                        }
                        else
                            return "原始密码不正确，请准确填写！";
                    }
                    else
                        return "密码不能为空！";
                }

                return "";

            }
            else
                return "传入的相关参数不正确";
        }
        else
            return "请传入相关参数";
    }
    #endregion

    #region 资料修改
    /// <summary>
    /// 资料修改
    /// </summary>
    /// <param name="parmeters">加密参数</param>
    /// <returns></returns>
    [WebMethod(EnableSession=true)]
    public string updMemInfo(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }

            string[] str = canshus.Split(',');

            if (str.Length == 14)
            {

                string number = str[0]; //会员编号
                string petname = str[1];//会员昵称
                string hometel = Encryption.Encryption.GetEncryptionTele(str[2]);//家庭电话
                string faxtel = Encryption.Encryption.GetEncryptionTele(str[3]);//传真电话
                string officetel = Encryption.Encryption.GetEncryptionTele(str[4]);//办公电话
                string mobile = Encryption.Encryption.GetEncryptionTele(str[5]);//移动电话
                string cpccode = str[6];//地址，格式：中国-安徽省-合肥市-包河区
                string addr = str[7];//详细地址
                string postcode = str[8];//邮编
                string bankcode = str[9];//开户银行
                string bankcpccode = str[10];//开户行地址，格式：中国-安徽省-合肥市-包河区
                string bankbrachname = str[11];//分/支行名称
                string bankcard = Encryption.Encryption.GetEncryptionCard(str[12]);//银行账号
                string remark = str[13];//备注


                if (remark.Length > 500)
                {
                    return "备注内容不能超过500字";
                }

                //地址CPCCode
                string[] str1 = cpccode.Split('-');
                cpccode = DAL.CommonDataDAL.GetCPCCode(str1[0], str1[1], str1[2], str1[3]);

                //开户行地址
                string[] str2 = bankcpccode.Split('-');
                if (str2[0].Trim().Length > 0 && str2[1].Trim().Length > 0 && str2[2].Trim().Length > 0 && str2[3].Trim().Length > 0)
                    bankcpccode = DAL.CommonDataDAL.GetCPCCode(str2[0], str2[1], str2[2], str2[3]);
                else
                    bankcpccode = "";

                BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("Memberinfo", "ltrim(rtrim(number))");
                cl_h_info.AddRecord(number);

                MemberInfoModifyBll mf = new MemberInfoModifyBll();
                if (mf.updMemberInfo(number, petname, hometel, faxtel, officetel, mobile, cpccode, addr, postcode, bankcode, bankcpccode, bankbrachname, bankcard, remark))
                {
                    cl_h_info.AddRecord(number);
                    cl_h_info.ModifiedIntoLogs(BLL.CommonClass.ChangeCategory.member3, number, BLL.CommonClass.ENUM_USERTYPE.objecttype5);

                    return "修改成功！";
                }
                else
                    return "修改失败！";

            }
            else
                return "传入的相关参数不正确";
        }
        else
            return "请传入相关参数";
    }
    /// <summary>
    /// 资料修改-显示会员信息
    /// </summary>
    /// <param name="parmeters">加密参数</param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string showMemInfo(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            string number = "";
            try
            {
                number = PublicClass.DESDeCode(canshus);//会员编号
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }

            MemberInfoModel member = MemberInfoModifyBll.getMemberInfo(number);
            if (member != null)
            {

                StringBuilder XmlStr = new StringBuilder();
                XmlStr.Append("<PersonalityChange>");
                XmlStr.Append("<ShowMemInfo>");

                XmlStr.AppendFormat("<number>{0}</number>", member.Number);
                XmlStr.AppendFormat("<name>{0}</name>", Encryption.Encryption.GetDecipherName(member.Name));
                XmlStr.AppendFormat("<petname>{0}</petname>", member.PetName);
                XmlStr.AppendFormat("<sex>{0}</sex>", member.Sex == 1 ? "男" : "女");
                XmlStr.AppendFormat("<birthdate>{0}</birthdate>", member.Birthday);
                XmlStr.AppendFormat("<zjType>{0}</zjType>", member.PaperType.PaperType);
                XmlStr.AppendFormat("<zjNumber>{0}</zjNumber>", Encryption.Encryption.GetDecipherNumber(member.PaperNumber));

                XmlStr.AppendFormat("<hometel>{0}</hometel>", member.HomeTele);
                XmlStr.AppendFormat("<faxtel>{0}</faxtel>", member.FaxTele);
                XmlStr.AppendFormat("<officetel>{0}</officetel>", member.OfficeTele);
                XmlStr.AppendFormat("<mobiletel>{0}</mobiletel>", member.MobileTele);

                CityModel cm1 = DAL.CommonDataDAL.GetCPCCode(member.CPCCode);
                //XmlStr.AppendFormat("<cpccode>{0}</cpccode>", member.CPCCode);
                XmlStr.AppendFormat("<cpccode>{0}</cpccode>", cm1.Country + "-" + cm1.Province + "-" + cm1.City + "-" + cm1.Xian);

                XmlStr.AppendFormat("<addr>{0}</addr>", member.Address);
                XmlStr.AppendFormat("<postcode>{0}</postcode>", member.PostalCode);
                XmlStr.AppendFormat("<bankcode>{0}</bankcode>", member.BankCode);

                CityModel cm2 = DAL.CommonDataDAL.GetCPCCode(member.BCPCCode);
                //XmlStr.AppendFormat("<bankcpccode>{0}</bankcpccode>", member.BCPCCode);
                XmlStr.AppendFormat("<bankcpccode>{0}</bankcpccode>", cm2.Country + "-" + cm2.Province + "-" + cm2.City + "-" + cm2.Xian);

                XmlStr.AppendFormat("<branchname>{0}</branchname>", member.Bankbranchname);
                XmlStr.AppendFormat("<bankbook>{0}</bankbook>", member.BankBook);
                XmlStr.AppendFormat("<bankcard>{0}</bankcard>", Encryption.Encryption.GetDecipherNumber(member.BankCard));
                XmlStr.AppendFormat("<remark>{0}</remark>", member.Remark);

                XmlStr.Append("</ShowMemInfo>");
                XmlStr.Append("</PersonalityChange>");

                return XmlStr.ToString();
            }
            else
                return "没有该会员的相关信息";
        }
        else
            return "请传入相关的参数";
    }
    #endregion

    #endregion

    #region 信息中心

    #region 资料下载
    /// <summary>
    /// 资料下载
    /// </summary>
    /// <param name="canshus"></param>
    /// <returns></returns>
    [WebMethod]
    public string DownLoadFiles(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }



            string[] str_parmeters = canshus.Split(',');

            //参数
            string number = str_parmeters[0]; //会员编号
            string dataName = str_parmeters[1]; //资料名称
            string fileName = str_parmeters[2]; //文件名称

            string conditions = " 1=1 ";

            int Lev = Convert.ToInt32(BLL.CommonClass.CommonDataBLL.GetBalanceLevel(number).Rows[0][0]);

            string condition = "1<2 and DownTarget <> 1 and (DownMenberLev=0 or DownMenberLev=" + Lev + ")";

            if (dataName.Trim() != "")
            {
                condition += " and ResName like '%" + dataName.Trim().Replace("'", "") + "%'";
            }

            if (fileName.Trim() != "")
            {
                condition += " and FileName like '%" + fileName.Trim().Replace("'", "") + "%'";
            }



            DataTable dt = DAL.DBHelper.ExecuteDataTable("select ResID,ResName,FileName,ResDescription,ResSize,ResDateTime,ResTimes  from Resources  where " + conditions + "  order by ResID desc");


            StringBuilder XmlStr = new StringBuilder();

            XmlStr.Append("<DownLoadFile>");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                XmlStr.Append("<dlFiles fId='f_" + i + "'>");

                string path = Server.MapPath("../Company/upLoadRes/" + dt.Rows[i]["FileName"].ToString());
                FileInfo file = new FileInfo(path);
                if (file.Exists)
                {
                    XmlStr.AppendFormat("<downUrl>{0}</downUrl>", "222.66.13.70/DS2014_MVS/Company/upLoadRes/" + dt.Rows[i]["FileName"].ToString());

                    //DownLoadFilesBLL.UpdResourcesResTimesByResID(Convert.ToInt32(dt.Rows[i]["ResID"]));

                }
                else
                {
                    XmlStr.AppendFormat("<downUrl>{0}</downUrl>", "您下载的文件不在文件夹中");
                }

                XmlStr.AppendFormat("<resID>{0}</resID>", dt.Rows[i]["ResID"].ToString());
                XmlStr.AppendFormat("<resName>{0}</resName>", dt.Rows[i]["ResName"].ToString()); //资料名称
                XmlStr.AppendFormat("<fileName>{0}</fileName>", dt.Rows[i]["FileName"].ToString());//对应文件名
                XmlStr.AppendFormat("<resDescription>{0}</resDescription>", dt.Rows[i]["ResDescription"].ToString());//资料简介
                XmlStr.AppendFormat("<resSize>{0}</resSize>", GetAuditExpectNum(dt.Rows[i]["ResSize"].ToString()));//文件大小
                XmlStr.AppendFormat("<resTimes>{0}</resTimes>", Convert.ToInt32(dt.Rows[i]["ResTimes"]));//下载次数

                XmlStr.Append("</dlFiles>");
            }

            XmlStr.Append("</DownLoadFile>");


            return XmlStr.ToString();
        }
        else
            return "请传入相关的参数";
    }
    /// <summary>
    /// 资料下载--增加下载次数
    /// </summary>
    /// <param name="canshus"></param>
    /// <returns></returns>
    [WebMethod]
    public string AddLoadFileTimes(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }



            string[] str_parmeters = canshus.Split(',');

            //参数
            string resID = str_parmeters[0]; //资料ID号


            int loadfile = Convert.ToInt32(DAL.DBHelper.ExecuteScalar("select COUNT(0)  from Resources  where  ResID=" + resID));
            if (loadfile > 0)
            {
                int flag = DownLoadFilesBLL.UpdResourcesResTimesByResID(Convert.ToInt32(resID));
                if (flag > 0)
                    return "success";
                else
                    return "fail";
            }
            else
                return "下载的资料不存在";

        }
        else
            return "请传入相关的参数";
    }

    #endregion

    #region 公告查阅
    /// <summary>
    /// 公告查询
    /// </summary>
    /// <param name="canshus">加密参数</param>
    /// <returns></returns>
    [WebMethod]
    public string QueryInfomation(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }



            string[] str_parmeters = canshus.Replace("，", ",").Split(',');

            //参数
            string number = str_parmeters[0]; //会员编号
            string startD = str_parmeters[1]; //发布开始日期
            string endD = str_parmeters[2]; //发布结束日期


            string conditions = " 1=1 ";


            if (startD.Trim() == "")
            {
                return "起始时间不能为空！";
            }
            if (endD.Trim() == "")
            {
                return "终止时间不能为空！";
            }

            bool flag = false;

            DateTime b_startD;
            flag = DateTime.TryParse(startD, out b_startD);
            if (!flag)
            {
                return "起始时间格式不正确！";
            }

            DateTime b_endD;
            flag = DateTime.TryParse(endD, out b_endD);
            if (!flag)
            {
                return "终止时间格式不正确！";
            }



            b_startD = b_startD.AddHours(-8).AddMinutes(0).AddSeconds(0);
            b_endD = b_endD.AddHours(15).AddMinutes(59).AddSeconds(59);
            if (b_startD > b_endD)
            {
                return "起始时间不能大于终止之间！";
            }

            conditions += "and Senddate between '" + b_startD + "' and '" + b_endD + "'";



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
                cmd.Parameters.Add("@number", SqlDbType.VarChar).Value = number;
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
            string sql1 = "SELECT ma.MessageID FROM dbo.MessageReadCondition AS ma,dbo.MessageSend AS me WHERE ma.MessageID=me.ID and DropFlag=0 and me.SenderRole=0 and me.Receive='*' and me.LoginRole=2 and ma.ConditionLeader='" + number + "'";
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


            DataTable dt_xml = DAL.DBHelper.ExecuteDataTable("select ID, LoginRole, Receive, InfoTitle, Content, SenderRole, Sender, Senddate, DropFlag, ReadFlag  from MessageSend  where " + conditions + " and " + where + "  order by id desc");


            StringBuilder XmlStr = new StringBuilder();

            XmlStr.Append("<QueryInfomation>");

            for (int i = 0; i < dt_xml.Rows.Count; i++)
            {
                XmlStr.Append("<qInfo qId='q_" + i + "'>");

                XmlStr.AppendFormat("<qId>{0}</qId>", dt_xml.Rows[i]["id"].ToString());
                XmlStr.AppendFormat("<InfoTitle>{0}</InfoTitle>", dt_xml.Rows[i]["InfoTitle"].ToString()); //公告标题
                XmlStr.AppendFormat("<Sender>{0}</Sender>", dt_xml.Rows[i]["Sender"].ToString());//发送人编号
                XmlStr.AppendFormat("<Senddate>{0}</Senddate>", GetBiaoZhunTime(dt_xml.Rows[i]["Senddate"].ToString()));//发布日期

                XmlStr.Append("</qInfo>");
            }

            XmlStr.Append("</QueryInfomation>");


            return XmlStr.ToString();
        }
        else
            return "请传入相关的参数";
    }
    /// <summary>
    /// 公告/邮件 详细
    /// </summary>
    /// <param name="canshus"></param>
    /// <returns></returns>
    [WebMethod]
    public string MessageContent(string canshus)
    {
        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }



            string[] str_parmeters = canshus.Replace("，", ",").Split(',');

            //参数
            string number = str_parmeters[0];
            string id = str_parmeters[1];
            string tableName = str_parmeters[2];



            string sqlid = "";
            int i = 1;

            StringBuilder XmlStr = new StringBuilder();
            XmlStr.Append("<MessageContent>");

            BLL.other.Company.MessageReceiveBLL bll = new MessageReceiveBLL();
            SqlDataReader dr = bll.GetGongGao(tableName, Convert.ToInt32(id));
            if (dr.Read())
            {
                XmlStr.Append("<mCnt mcId='mc_" + i + "'>");

                sqlid = dr["id"].ToString();


                XmlStr.AppendFormat("<Senddate>{0}</Senddate>", DateTime.Parse(dr["Senddate"].ToString()).AddHours(Convert.ToDouble(8)).ToString());

                if (dr["Receive"].ToString().Trim() == "*")
                    XmlStr.AppendFormat("<receive>{0}</receive>", number);
                else
                    XmlStr.AppendFormat("<receive>{0}</receive>", dr["Receive"].ToString());

                XmlStr.AppendFormat("<infotitle>{0}</infotitle>", dr["Infotitle"].ToString());
                XmlStr.AppendFormat("<sender>{0}</sender>", dr["sender"].ToString());
                XmlStr.AppendFormat("<content>{0}</content>", dr["Content"].ToString());

                XmlStr.Append("</mCnt>");

                i = i++;
            }
            dr.Close();

            bll.UpdateIsRead(tableName, sqlid);

            XmlStr.Append("</MessageContent>");


            return XmlStr.ToString();
        }
        else
            return "请传入相关的参数";
    }
    #endregion

    #region 邮件管理
    /// <summary>
    /// 邮件分类 和 收件人员
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public string GetEmailInfo()
    {
        int i = 0;

        StringBuilder XmlStr = new StringBuilder();
        XmlStr.Append("<EmailInfo>");

        //邮件分类
        System.Data.SqlClient.SqlDataReader dr = DAL.DBHelper.ExecuteReader("Select ID,ClassName From MsgClass");
        while (dr.Read())
        {
            XmlStr.Append("<emailclass ecId='ec_" + i + "'>");

            XmlStr.AppendFormat("<id>{0}</id>", dr["id"].ToString()); //value
            XmlStr.AppendFormat("<loginRole>{0}</loginRole>", dr["ClassName"].ToString()); //text

            XmlStr.Append("</emailclass>");

            i = i + 1;
        }
        dr.Close();

        //收件人员
        XmlStr.Append("<inite_bianhao>");
        XmlStr.AppendFormat("<value>{0}</value>", BLL.CommonClass.CommonDataBLL.getManageID(1));
        XmlStr.AppendFormat("<text>{0}</text>", "信息管理员");
        XmlStr.Append("</inite_bianhao>");

        XmlStr.Append("</EmailInfo>");

        if (i > 0)
            return XmlStr.ToString();
        else
            return "无";
    }
    /// <summary>
    /// 写邮件
    /// </summary>
    /// <param name="canshus">邮件发送相关参数</param>
    /// <returns></returns>
    [WebMethod]
    public string WriteEmail(string canshus)
    {

        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }



            string[] str_parmeters = canshus.Replace("，", ",").Split(',');

            //参数
            string senderNum = str_parmeters[0]; //发送者
            string receiveNum = str_parmeters[1]; //收件人员
            string loginRoleV = str_parmeters[2]; //收件人员类型Value【固定值：0】
            string loginRoleT = str_parmeters[3]; //收件人员类型Text【固定值：管理员】
            string emailClass = str_parmeters[4]; //邮件分类
            string title = str_parmeters[5]; //信息标题
            string content = str_parmeters[6]; //邮件内容



            if (receiveNum.Trim() == "")
            {
                return "对不起，编号不能为空！";
            }
            string sqlStr = "";
            switch (loginRoleV)
            {
                case "0": sqlStr = "Select count(0) From Manage where Number='" + receiveNum + "'"; break;
                case "1": sqlStr = "Select count(0) From MemberInfo where Number='" + receiveNum + "'"; break;
                case "2": sqlStr = "Select count(0) From StoreInfo where storeid='" + receiveNum + "'"; break;
            }
            MessageSendBLL msb = new MessageSendBLL();
            if (msb.check(sqlStr) != 1)
            {
                return "对不起，" + loginRoleT + "编号格式错误！";
            }
            if (emailClass.Equals(""))
            {
                return "请选择邮件分类！";
            }
            if (title.Trim() == "")
            {
                return "对不起，标题信息不能为空！";
            }
            if (content.Trim().Length.Equals(0))
            {
                return "请输入邮件内容！";
            }
            if (content.Trim().Length > 4000)
            {
                return "您输入的信息过长！";
            }
            DateTime date = DateTime.Now;
            MessageSendModel msm = new MessageSendModel();
            ///表示会员
            msm.LoginRole = "0";
            msm.Receive = BLL.CommonClass.CommonDataBLL.getManageID(1);
            msm.InfoTitle = title.ToString().Replace("<", "&lt;").Replace(">", "&gt;");
            msm.Content = content.Trim();
            msm.Sender = senderNum;
            msm.Senddate = date;
            msm.SenderRole = "2";
            msm.MessageClassID = Convert.ToInt32(emailClass);
            msm.MessageType = 'm';
            int i = 0;
            i = msb.MemberSendToManage(msm);
            if (i > 0)
            {
                return "邮件发送成功！";
            }
            else
            {
                return "邮件发送失败！";
            }
        }
        else
            return "请传入相关的参数";
    }
    /// <summary>
    /// 收件箱
    /// </summary>
    /// <param name="canshus"></param>
    /// <returns></returns>
    [WebMethod]
    public string ReceiveEmail(string canshus)
    {

        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }



            string[] str_parmeters = canshus.Replace("，", ",").Split(',');

            //参数
            string number = str_parmeters[0]; //收件会员编号
            string startD = str_parmeters[1]; //起始时间
            string endD = str_parmeters[2]; //结束时间


            if (startD.Trim() == "")
            {
                return "起始时间不能为空！";
            }
            if (endD.Trim() == "")
            {
                return "终止时间不能为空！";
            }


            ///注意会员的登录角色是2
            StringBuilder sb = new StringBuilder();
            string wheretime = "";
            string classid = "";

            sb.Append("DropFlag=0 and LoginRole=2 and Receive like '%" + number + "%' and ID not in(select MessageID from dbo.F_DroppedReceiveByOperator('" + number + "',2))");
            if (classid.Trim() != "")
            {
                sb.Append(" and classid=" + classid);
            }
            if (startD != "" && endD != "")
            {
                bool flag = false;

                DateTime b_startD;
                flag = DateTime.TryParse(startD, out b_startD);
                if (!flag)
                {
                    return "起始时间格式不正确！";
                }

                DateTime b_endD;
                flag = DateTime.TryParse(endD, out b_endD);
                if (!flag)
                {
                    return "终止时间格式不正确！";
                }



                b_startD = b_startD.AddHours(0 - BLL.other.Company.WordlTimeBLL.ConvertAddHours()).AddMinutes(0).AddSeconds(0);
                b_endD = b_endD.AddHours(23 - BLL.other.Company.WordlTimeBLL.ConvertAddHours()).AddMinutes(59).AddSeconds(59);
                if (b_startD > b_endD)
                {
                    return "起始时间不能大于终止之间！";
                }

                wheretime = "Senddate between '" + b_startD + "'" + " and '" + b_endD + "'";
                sb.Append("and " + wheretime);
            }



            SqlConnection conn = new SqlConnection(DAL.DBHelper.connString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "MessageCheckCondition";
            conn.Open();

            string rtn = "";
            string idlist = "";
            DataTable dt = DAL.DBHelper.ExecuteDataTable("select ID,MessageSendID from MessageReceive where DropFlag=0 and loginRole=2 and Receive='*' and ID not in(select MessageID from dbo.F_DroppedReceiveByOperator('" + number + "',2)) and MessageType='m' and " + wheretime);
            foreach (DataRow row in dt.Rows)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@MessageSendID", SqlDbType.Int).Value = Convert.ToInt32(row["MessageSendID"]);
                cmd.Parameters.Add("@number", SqlDbType.VarChar).Value = number;
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
            string sql1 = "SELECT ma.MessageID FROM dbo.MessageReadCondition AS ma,dbo.MessageSend AS me WHERE ma.MessageID=me.ID and DropFlag=0 and me.SenderRole=0 and me.Receive='*' and me.LoginRole=2 and ma.ConditionLeader='" + number + "'";
            DataTable dt1 = DAL.DBHelper.ExecuteDataTable(sql1);
            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    idlist += "," + dt1.Rows[i]["MessageID"].ToString();
                }
            }
            idlist = idlist.TrimStart(",".ToCharArray());

            if (!idlist.Equals("") && classid == "")
            {
                sb.Append(" or ID in(" + idlist + ")");
            }

            DataTable dt_xml = DAL.DBHelper.ExecuteDataTable("select * from MessageReceive where " + sb.ToString());


            StringBuilder XmlStr = new StringBuilder();

            XmlStr.Append("<ReceiveEmail>");

            for (int i = 0; i < dt_xml.Rows.Count; i++)
            {
                XmlStr.Append("<reInfo reId='re_" + i + "'>");

                XmlStr.AppendFormat("<id>{0}</id>", dt_xml.Rows[i]["id"].ToString()); //id
                XmlStr.AppendFormat("<loginRole>{0}</loginRole>", getloginRole(dt_xml.Rows[i]["loginRole"].ToString())); //接收对象
                XmlStr.AppendFormat("<receiver>{0}</receiver>", GetService(dt_xml.Rows[i]["id"].ToString())); //接收编号
                XmlStr.AppendFormat("<sender>{0}</sender>", dt_xml.Rows[i]["sender"].ToString());//发送编号
                XmlStr.AppendFormat("<infoTitle>{0}</infoTitle>", dt_xml.Rows[i]["infoTitle"].ToString());//信息标题
                XmlStr.AppendFormat("<senddate>{0}</senddate>", GetBiaoZhunTime(dt_xml.Rows[i]["Senddate"].ToString()));//发布日期
                XmlStr.AppendFormat("<isRead>{0}</isRead>", new MessageSendBLL().getReadType(dt_xml.Rows[i]["id"].ToString()) == 1 ? "已阅读" : "未阅读");//是否阅读

                XmlStr.Append("</reInfo>");
            }

            XmlStr.Append("</ReceiveEmail>");


            return XmlStr.ToString();
        }
        else
            return "请传入相关的参数";
    }
    protected string getloginRole(string str)    // 前台调用(接受对象的转换)
    {
        switch (str.Trim())
        {
            case "2":
                return "会员";
            case "1":
                return "服务机构";
            default:
                return "管理员";
        }
    }
    /// <summary>
    /// 获取发送邮件的对应级别
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    protected string GetService(string id)
    {
        string res = "全体成员";
        try
        {
            string sql = "select isnull(levelstr,'') as levelstr FROM BSCO_Level as b, dbo.MessageReadCondition as m  where b.levelint=m.ConditionLevel and b.levelflag=0 and MessageID=" + id;
            res = DAL.DBHelper.ExecuteScalar(sql).ToString();
        }
        catch
        {
            res = "全体成员";
        }
        return res;
    }
    /// <summary>
    /// 邮件展开
    /// </summary>
    /// <param name="canshus"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string MessageCascade(string canshus)
    {

        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }



            string[] str_parmeters = canshus.Replace("，", ",").Split(',');

            //参数
            string id = str_parmeters[0]; //邮件id号


            int i = 1;

            StringBuilder XmlStr = new StringBuilder();
            XmlStr.Append("<MessageCascade>");

            SqlDataReader dr = DAL.DBHelper.ExecuteReader("GetMessageCascade", new SqlParameter("@id", Convert.ToInt32(id)), CommandType.StoredProcedure);

            while (dr.Read())
            {
                string SenderRole = "";
                string LoginRole = "";
                switch (dr["SenderRole"].ToString().Trim())
                {
                    case "0": SenderRole = "管理员"; break;
                    case "1": SenderRole = "服务机构"; break;
                    case "2": SenderRole = "会员"; break;
                }
                switch (dr["LoginRole"].ToString().Trim())
                {
                    case "0": LoginRole = "管理员"; break;
                    case "1": LoginRole = "服务机构"; break;
                    case "2": LoginRole = "会员"; break;
                }

                XmlStr.Append("<mcInfo mcId='mc_" + i + "'>");

                XmlStr.AppendFormat("<sender>{0}</sender>", dr["Sender"].ToString() + "(" + SenderRole + ")"); //发件人
                XmlStr.AppendFormat("<receiver>{0}</receiver>", dr["Receive"].ToString() + "(" + LoginRole + ")"); //收件人
                XmlStr.AppendFormat("<sendDate>{0}</sendDate>", DateTime.Parse(dr["SendDate"].ToString()).AddHours(Convert.ToDouble(Session["WTH"])).ToString()); //收件时间
                XmlStr.AppendFormat("<infoTitle>{0}</infoTitle>", dr["InfoTitle"].ToString()); //标题
                XmlStr.AppendFormat("<content>{0}</content>", dr["Content"].ToString()); //内容

                XmlStr.Append("</mcInfo>");

                i = i++;
            }
            dr.Close();
            dr.Dispose();

            XmlStr.Append("</MessageCascade>");


            return XmlStr.ToString();
        }
        else
            return "请传入相关的参数";
    }
    /// <summary>
    /// 发件箱
    /// </summary>
    /// <param name="canshus"></param>
    /// <returns></returns>
    [WebMethod]
    public string SendEmail(string canshus)
    {

        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }



            string[] str_parmeters = canshus.Replace("，", ",").Split(',');

            //参数
            string number = str_parmeters[0]; //发件会员编号
            string startD = str_parmeters[1]; //起始时间
            string endD = str_parmeters[2]; //结束时间


            if (startD.Trim() == "")
            {
                return "起始时间不能为空！";
            }
            if (endD.Trim() == "")
            {
                return "终止时间不能为空！";
            }


            StringBuilder sb = new StringBuilder();
            sb.Append("DropFlag=0 and SenderRole=2 and Sender='" + number + "' and ID not in(select MessageID from dbo.F_DroppedSendByOperator('" + number + "',2))");

            if (startD != "" && endD != "")
            {
                bool flag = false;

                DateTime b_startD;
                flag = DateTime.TryParse(startD, out b_startD);
                if (!flag)
                {
                    return "起始时间格式不正确！";
                }

                DateTime b_endD;
                flag = DateTime.TryParse(endD, out b_endD);
                if (!flag)
                {
                    return "终止时间格式不正确！";
                }



                b_startD = b_startD.AddHours(0 - BLL.other.Company.WordlTimeBLL.ConvertAddHours()).AddMinutes(0).AddSeconds(0);
                b_endD = b_endD.AddHours(23 - BLL.other.Company.WordlTimeBLL.ConvertAddHours()).AddMinutes(59).AddSeconds(59);
                if (b_startD > b_endD)
                {
                    return "起始时间不能大于终止之间！";
                }

                sb.Append("and Senddate between '" + b_startD + "' and '" + b_endD + "'");
            }

            DataTable dt_xml = DAL.DBHelper.ExecuteDataTable("select *  from MessageSend  where " + sb.ToString() + "  order by Senddate desc");


            StringBuilder XmlStr = new StringBuilder();

            XmlStr.Append("<SendEmail>");

            for (int i = 0; i < dt_xml.Rows.Count; i++)
            {
                XmlStr.Append("<seInfo seId='se_" + (i + 1) + "'>");

                XmlStr.AppendFormat("<id>{0}</id>", dt_xml.Rows[i]["id"].ToString()); //id
                XmlStr.AppendFormat("<loginRole>{0}</loginRole>", getloginRole(dt_xml.Rows[i]["loginRole"].ToString())); //接收对象
                XmlStr.AppendFormat("<receiver>{0}</receiver>", GetService(dt_xml.Rows[i]["id"].ToString())); //接收编号
                XmlStr.AppendFormat("<sender>{0}</sender>", dt_xml.Rows[i]["sender"].ToString());//发送编号
                XmlStr.AppendFormat("<infoTitle>{0}</infoTitle>", dt_xml.Rows[i]["infoTitle"].ToString());//信息标题
                XmlStr.AppendFormat("<readFlag>{0}</readFlag>", gethandlestatus(dt_xml.Rows[i]["ReadFlag"].ToString()));//处理状态
                XmlStr.AppendFormat("<senddate>{0}</senddate>", GetBiaoZhunTime(dt_xml.Rows[i]["Senddate"].ToString()));//发布日期

                XmlStr.Append("</seInfo>");
            }

            XmlStr.Append("</SendEmail>");


            return XmlStr.ToString();
        }
        else
            return "请传入相关的参数";
    }
    protected string gethandlestatus(string str)    // 前台调用(接受对象的转换)
    {
        switch (str.Trim())
        {
            case "0":
                return "未读邮件";
            case "1":
                return "已阅读";
            case "2":
                return "处理中";
            case "3":
                return "已处理";
            default:
                return "未知";
        }
    }
    /// <summary>
    /// 发件箱邮件删除
    /// </summary>
    /// <param name="canshus"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string DelSendEmail(string canshus)
    {

        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }



            string[] str_parmeters = canshus.Replace("，", ",").Split(',');

            //参数
            string number = str_parmeters[0]; //会员编号
            string id = str_parmeters[1]; //发件箱邮件id


            if (MessageSendBLL.DelMessageSendById(Convert.ToInt32(id), number, 2))
            {
                return "删除成功！";
            }
            else
                return "删除失败！";

        }
        else
            return "请传入相关的参数";
    }
    /// <summary>
    /// 废件箱
    /// </summary>
    /// <param name="canshus"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string UnusedEmail(string canshus)
    {

        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }



            string[] str_parmeters = canshus.Replace("，", ",").Split(',');

            //参数
            string number = str_parmeters[0]; //发件会员编号
            string startD = str_parmeters[1]; //起始时间
            string endD = str_parmeters[2]; //结束时间


            if (startD.Trim() == "")
            {
                return "起始时间不能为空！";
            }
            if (endD.Trim() == "")
            {
                return "终止时间不能为空！";
            }


            StringBuilder sb = new StringBuilder();
            sb.Append(" Operator='" + number + "' and OperatorType=2 ");

            if (startD != "" && endD != "")
            {
                bool flag = false;

                DateTime b_startD;
                flag = DateTime.TryParse(startD, out b_startD);
                if (!flag)
                {
                    return "起始时间格式不正确！";
                }

                DateTime b_endD;
                flag = DateTime.TryParse(endD, out b_endD);
                if (!flag)
                {
                    return "终止时间格式不正确！";
                }



                b_startD = b_startD.AddHours(0 - BLL.other.Company.WordlTimeBLL.ConvertAddHours()).AddMinutes(0).AddSeconds(0);
                b_endD = b_endD.AddHours(23 - BLL.other.Company.WordlTimeBLL.ConvertAddHours()).AddMinutes(59).AddSeconds(59);
                if (b_startD > b_endD)
                {
                    return "起始时间不能大于终止之间！";
                }

                sb.Append("and Senddate between '" + b_startD + "' and '" + b_endD + "'");
            }

            DataTable dt_xml = DAL.DBHelper.ExecuteDataTable("select * from V_DroppedMessage where " + sb.ToString());


            StringBuilder XmlStr = new StringBuilder();

            XmlStr.Append("<UnusedEmail>");

            for (int i = 0; i < dt_xml.Rows.Count; i++)
            {
                XmlStr.Append("<ueInfo ueId='ue_" + (i + 1) + "'>");

                XmlStr.AppendFormat("<id>{0}</id>", dt_xml.Rows[i]["id"].ToString()); //id
                XmlStr.AppendFormat("<loginRole>{0}</loginRole>", getloginRole(dt_xml.Rows[i]["loginRole"].ToString())); //接收对象
                //XmlStr.AppendFormat("<receiver>{0}</receiver>", GetService(dt_xml.Rows[i]["Receive"].ToString())); //接收编号
                XmlStr.AppendFormat("<receiver>{0}</receiver>", dt_xml.Rows[i]["Receive"].ToString()); //接收编号
                XmlStr.AppendFormat("<sender>{0}</sender>", dt_xml.Rows[i]["sender"].ToString());//发送编号
                XmlStr.AppendFormat("<infoTitle>{0}</infoTitle>", dt_xml.Rows[i]["infoTitle"].ToString());//信息标题
                XmlStr.AppendFormat("<senddate>{0}</senddate>", GetBiaoZhunTime(dt_xml.Rows[i]["Senddate"].ToString()));//发布日期

                XmlStr.Append("</ueInfo>");
            }

            XmlStr.Append("</UnusedEmail>");


            return XmlStr.ToString();
        }
        else
            return "请传入相关的参数";
    }
    /// <summary>
    /// 删除废件箱
    /// </summary>
    /// <param name="canshus"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string DelUnusedEmail(string canshus)
    {

        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }



            string[] str_parmeters = canshus.Replace("，", ",").Split(',');

            //参数
            string number = str_parmeters[0]; //会员编号
            string id = str_parmeters[1]; //废件箱邮件id


            int i = new MessageSendBLL().delMessageDrop(Convert.ToInt32(id));
            if (i > 0)
                return "删除成功！";
            else
                return "删除失败！";

        }
        else
            return "请传入相关的参数";
    }
    /// <summary>
    /// 还原废件箱
    /// </summary>
    /// <param name="canshus"></param>
    /// <returns></returns>
    [WebMethod]
    public string RecoverUnusedEmail(string canshus)
    {

        if (canshus.Trim().Length > 0)
        {
            try
            {
                canshus = PublicClass.DESDeCode(canshus);
            }
            catch (Exception es)
            {
                if (es.ToString().IndexOf("要解密的数据的长度无效。") != -1)
                    return "要解密的数据的长度无效。";
                else if (es.ToString().IndexOf("未找到任何可识别的数字。") != -1)
                    return "未找到任何可识别的数字。";
                else
                    return es.ToString();
            }



            string[] str_parmeters = canshus.Replace("，", ",").Split(',');

            //参数
            string number = str_parmeters[0]; //会员编号
            string id = str_parmeters[1]; //废件箱邮件id


            int recoverCount = MessageDropBLL.RecoverMessage(Convert.ToInt32(id));
            if (recoverCount > 0)
                return "邮件还原成功！";

            else
                return "还原失败，请联系管理员！";
        }
        else
            return "请传入相关的参数";
    }
    #endregion

    #endregion

#endregion

}