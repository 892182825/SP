using System;
using System.Data.SqlClient;
using System.Data;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using Microsoft.Office.Interop.Owc11;
using System.IO;
using Microsoft.Office.Interop;
using OWC11 = Microsoft.Office.Interop.Owc11;
using System.Text;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Collections;
using System.Web.UI.WebControls;
using BLL.CommonClass;
using DAL;
using BLL.other.Company;
using BLL.other.Member;
using BLL.Registration_declarations;
using BLL.other;
using BLL.CommonClass;
using System.Xml;
using BLL.MoneyFlows;
using BLL.Logistics;
using Model.Other;
using System.Web;
using Model;
using System.Collections.Generic;

/// <summary>
/// AjaxClass 的摘要说明。
/// </summary>
public class AjaxClass : BLL.TranslationBase
{

    CountryBLL countryBLL = new CountryBLL();
    BLL.Registration_declarations.BrowseMemberOrdersBLL bll = new BLL.Registration_declarations.BrowseMemberOrdersBLL();
    public AjaxClass()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    /// <summary>
    /// 根据会员编号获取会员姓名
    /// 宋俊：2009-9-13
    /// </summary>
    /// <param name="number">会员编号</param>
    /// <returns>会员姓名</returns>
    [AjaxPro.AjaxMethod]
    public string GetMumberName(string number)
    {
        return StoreRegisterBLL.GetMemberName(number);
    }
    [AjaxPro.AjaxMethod]
    public string getMemberMTele(string member)
    {
        return StoreRegisterBLL.getMemberMTele(member);
    }
    [AjaxPro.AjaxMethod]
    public string getMemberOTele(string member)
    {
        return StoreRegisterBLL.getMemberOTele(member);
    }
    [AjaxPro.AjaxMethod]
    public string getMemberFTele(string member)
    {
        return StoreRegisterBLL.getMemberFTele(member);
    }
    [AjaxPro.AjaxMethod]
    public string getMemberHTele(string member)
    {
        return StoreRegisterBLL.getMemberHTele(member);
    }

    [AjaxPro.AjaxMethod]
    public string GetMenuLeft(int pid, int type)
    {
        return MenuBLL.GetMenuLeft(pid, type);
    }

    [AjaxPro.AjaxMethod]
    public bool CheckStoreID(string storeId)
    {
        return new BLL.Registration_declarations.RegistermemberBLL().CheckStoreId(storeId);
    }

    [AjaxPro.AjaxMethod]
    public bool CheckStoreID1(string storeId)
    {
        return new BLL.Registration_declarations.RegistermemberBLL().CheckStoreId1(storeId);
    }
    [AjaxPro.AjaxMethod]
    public int TXset()
    {

        return new BLL.Registration_declarations.RegistermemberBLL().TXset();
    }
    [AjaxPro.AjaxMethod]
    public List<string[]> GetTongjitu(int type, string begin, string end)
    {
        List<string[]> rez = new List<string[]>();

        switch (type)
        {

            case 1:
                rez = ConstructData(begin, end);//
                break;
            default:
                break;
        }

        return rez;
    }
    [AjaxPro.AjaxMethod]
    public List<string[]> GetHKTongjitu(int forp, string act, string begin, string end)
    {
        List<string[]> rez = new List<string[]>();

        rez = ConstructData(forp, act, begin, end);


        return rez;
    }
    [AjaxPro.AjaxMethod]
    public List<string[]> GetODTongjitu(int forp, string paystate, string begin, string end)
    {
        ArrayList al = ReportFormsBLL.ConstructData(forp, begin, end, paystate);
        List<string[]> rez = new List<string[]>();
        for (int i = 0; i < al.Count; i++)
        {
            DAL.ReportFormsDAL.Item im = (DAL.ReportFormsDAL.Item)al[i];
            string[] s = new string[]{
         im.Text ,im.Value.ToString()
         };
            rez.Add(s);
        }

        return rez;
    }
    [AjaxPro.AjaxMethod]
    public List<string[]> GetCPTongjitu(string storeid)
    {
        ArrayList al = ReportFormsBLL.ConstructData_II(storeid);
        List<string[]> rez = new List<string[]>();
        for (int i = 0; i < al.Count; i++)
        {
            DAL.ReportFormsDAL.Item im = (DAL.ReportFormsDAL.Item)al[i];
            string[] s = new string[]{
         im.Text ,im.Value.ToString()
         };
            rez.Add(s);
        }

        return rez;
    }
    [AjaxPro.AjaxMethod]
    public List<string[]> GetMBTongjitu(int forp, string bdtype, string act, string begin, string end)
    {
        List<string[]> rez = MmemberConstructData(forp, bdtype, act, begin, end);


        return rez;
    }
    protected List<string[]> MmemberConstructData(int forp, string bdtype, string paytp, string begin, string end)
    {
        int top = 9;
        bool need = true;
        decimal other = 0;
        string column = "";
        string result = "";
        if (begin == "" || begin == null) begin = DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd HH:mm:ss");
        if (end == "" || end == null) end = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        DataTable table = new DataTable();
        ArrayList coll = new ArrayList();
        SqlParameter[] parm = new SqlParameter[]
        {
		    new SqlParameter("@BeginDate",SqlDbType.DateTime),
		    new SqlParameter("@EndDate",SqlDbType.DateTime)							
		};
        parm[0].Value = begin;
        parm[1].Value = end;

        if (forp == 0)
        {
            column = "storeid";

            table = DBHelper.ExecuteDataTable("H_order_getDataBystore", parm, CommandType.StoredProcedure);
            if (bdtype == "0")
            {
                switch (paytp)
                {
                    case "-1":
                        result = "BFbaodan";
                        break;
                    case "0":
                        result = "BFCheckOut";
                        break;
                    case "1":
                        result = "BFNCheckOut";
                        break;
                }
            }
            if (bdtype == "1")
            {
                switch (paytp)
                {
                    case "-1":
                        result = "BAbaodan";
                        break;
                    case "0":
                        result = "BACheckOut";
                        break;
                    case "1":
                        result = "BANCheckOut";
                        break;
                }
            }
        }
        if (forp == 1)
        {
            column = "city";
            table = DBHelper.ExecuteDataTable("MemberOrder_GetDataByArea", parm, CommandType.StoredProcedure);
            if (bdtype == "0")
            {
                switch (paytp)
                {
                    case "-1":
                        result = "BFbaodan";
                        break;
                    case "0":
                        result = "BFCheckOut";
                        break;
                    case "1":
                        result = "BFNCheckOut";
                        break;
                }
            }
            if (bdtype == "1")
            {
                switch (paytp)
                {
                    case "-1":
                        result = "BAbaodan";
                        break;
                    case "0":
                        result = "BACheckOut";
                        break;
                    case "1":
                        result = "BANCheckOut";
                        break;
                }
            }
        }

        int rows = table.Rows.Count;
        DataView dv = table.DefaultView;
        dv.Sort = result + "" + " Desc";
        if (top >= dv.Count)
        {
            need = false;
            top = dv.Count;
        }

        List<string[]> rez = new List<string[]>();

        for (int i = 0; i < top; i++)
        {
            string[] s = new string[]{
         dv[i][column].ToString() ,dv[i][result].ToString()
         };
            rez.Add(s);
        }

        return rez;
    }


    /// <summary>
    /// 获取注册店铺统计图
    /// </summary>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    /// <returns></returns>

    private List<string[]> ConstructData(string begin, string end)
    {
        int top = 9;
        bool need = true;
        decimal other = 0;
        string column = "";
        string result = "";
        if (begin == "" || begin == null) begin = DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd HH:mm:ss");
        if (end == "" || end == null) end = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        DataTable table = new DataTable();
        ArrayList coll = new ArrayList();

        SqlParameter[] param ={
									 new SqlParameter("@BeginDate",SqlDbType.DateTime),
									 new SqlParameter("@EndDate",SqlDbType.DateTime)
								 };
        param[0].Value = Convert.ToDateTime(begin).ToUniversalTime();
        param[1].Value = Convert.ToDateTime(end).AddDays(1).ToUniversalTime();



        table = DAL.DBHelper.ExecuteDataTable("Store_info_getDatabyArea", param, CommandType.StoredProcedure);
        List<string[]> rez = new List<string[]>();
        foreach (DataRow item in table.Rows)
        {
            string[] s = new string[]{
          item["city"].ToString(),item["BNum"].ToString()
         };
            rez.Add(s);
        }
        return rez;

    }

    /// <summary>
    /// 获取店铺汇款统计图
    /// </summary>
    /// <param name="forp"></param>
    /// <param name="act"></param>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    protected List<string[]> ConstructData(int forp, string act, string begin, string end)
    {
        int top = 9;
        bool need = true;
        decimal other = 0;
        string column = "";
        string result = "";
        DataTable table;
        ArrayList coll = new ArrayList();
        if (begin == "" || begin == null) begin = DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd HH:mm:ss");
        if (end == "" || end == null) end = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        if (forp == 0)
        {
            column = "remitnumber";
            if (act == "-1")
            {
                table = CommonDataBLL.GetStoreTotal(DateTime.Parse(begin).ToUniversalTime().ToString(), (DateTime.Parse(end).AddHours(23).AddMinutes(59).AddSeconds(59)).ToUniversalTime().ToString());

                result = "amt";
            }
            else
            {
                table = CommonDataBLL.getDataByStoreid(DateTime.Parse(begin).ToUniversalTime().ToString(), Convert.ToDateTime(end).AddDays(1).ToUniversalTime().ToString());
                if (act == "0")
                {
                    result = "Eyajin";
                }
                if (act == "1")
                {
                    result = "Ejiameng";
                }
            }
        }
        else
        {
            column = "province";
            if (act == "-1")
            {

                table = CommonDataBLL.GetStoreProvince(DateTime.Parse(begin).ToUniversalTime().ToString(), DateTime.Parse(end).ToUniversalTime().ToString());
                result = "amt";
            }
            else
            {
                table = CommonDataBLL.getDataByCity(DateTime.Parse(begin).ToUniversalTime().ToString(), (DateTime.Parse(end).AddHours(23).AddMinutes(59).AddSeconds(59)).ToUniversalTime().ToString());
                if (act == "0")
                {
                    result = "Eyajin";
                }
                if (act == "1")
                {
                    result = "Ejiameng";
                }
                column = "province";
            }
        }


        List<string[]> rez = new List<string[]>();
        foreach (DataRow item in table.Rows)
        {
            string[] s = new string[]{
          item[column].ToString(),item[result].ToString()
         };
            rez.Add(s);
        }
        return rez;


    }




    [AjaxPro.AjaxMethod]
    public int sendvcode(string number)
    {
        Random rdo = new Random();
        string vcode = rdo.Next(1000, 9999).ToString();
        HttpContext.Current.Session["vcode"] = vcode;
        string msg = "亲爱的用户您好，您的验证码为" + vcode + " ，验证码10分钟内有效。";
        if (SendEmail.SendSMS(number, msg)) return 1;
        else return 0;
    }
    [AjaxPro.AjaxMethod]
    public int Checknumberphone(string number, string phone, string name)
    {
        if (DAL.MemberInfoDAL.checkmbphone(number, phone, name) > 0)
            return 1;
        else return 0;
    }

    [AjaxPro.AjaxMethod]
    public bool GetShoukuan(string txtGather, string orderId)
    {
        bool blean = MemberOrderDAL.Batch(double.Parse(txtGather), DateTime.Now.ToUniversalTime(), orderId);

        return blean;
    }

    [AjaxPro.AjaxMethod]
    public string GetZipCode(string address)
    {
        string[] addr = address.Split(' ');
        return DAL.CommonDataDAL.GetZipCode(addr[0], addr[1], addr[2]);
    }
    [AjaxPro.AjaxMethod]
    public static int GetNewEmailCount()
    {
        if (HttpContext.Current.Session["Member"] != null)
        {
            string number = HttpContext.Current.Session["Member"].ToString();
            string sqlst = " select COUNT(0) from  MessageReceive where Receive='" + number + "' and ReadFlag=0  ";
            int nmc = Convert.ToInt32(DBHelper.ExecuteScalar(sqlst)); return nmc;
        }
        else
        {
            return 0;
        }

    }
    [AjaxPro.AjaxMethod]
    public int GetShopCartCount()
    {
        int nmc = 0;
        if (HttpContext.Current.Session["Member"] != null)
        {
            string number = HttpContext.Current.Session["Member"].ToString();
            string sqlst = " select COUNT(0) from  ShopCart where number='" + number + "'   ";
              nmc = Convert.ToInt32(DBHelper.ExecuteScalar(sqlst));
        }
        return nmc;
    }
    [AjaxPro.AjaxMethod]
    public int Getscp()
    {
        return 2;
    }
    [AjaxPro.AjaxMethod]
    public string GetDaoHang(string bianhao, string qishu, string cengshu, string isanzhi)
    {
        string page = "ShowNetworkViewNew";
        if (isanzhi == "0")
            page = "ShowNetworkViewNewTj";
        string divstr = "";
        if (bianhao != null && bianhao != "")
        {
            divstr = BLL.Translation.Translate("007032", "链路图") + "：";

            if (Session["DHNumbers"] == null)
            {
                Session["DHNumbers"] = new string[2] { bianhao, "" };
                divstr += "<a href='" + page + ".aspx?cengshu=" + cengshu + "&SelectGrass=" + qishu + "&bh=" + bianhao + "&isanzhi=" + isanzhi + "'>" + CommonDataBLL.GetPetNameByNumber(bianhao) + "</a> →";
            }
            else
            {
                string[] nums = Session["DHNumbers"] as string[];

                if (nums[0] != bianhao)
                {
                    if (nums[1] != bianhao)
                    {
                        nums[1] = bianhao;
                    }

                    IList<string> lists = Jiegou.GetNumberForTop(nums[0], Convert.ToInt32(qishu), Session["jglx"].ToString() == "az");
                    int count = 0;
                    foreach (string str in lists)
                    {
                        if (nums[1] == str)
                            count++;
                    }

                    if (count == 0)
                        divstr += "<a href='" + page + ".aspx?cengshu=" + cengshu + "&SelectGrass=" + qishu + "&bh=" + nums[1] + "&isanzhi=" + isanzhi + "'>" + CommonDataBLL.GetPetNameByNumber(nums[1]) + "</a> →";
                    else
                    {
                        string highNum = nums[1];
                        string numbers = "";
                        do
                        {
                            numbers += highNum + ",";
                            highNum = Jiegou.GetHighNumber(highNum, Session["jglx"].ToString() == "az");
                        } while (highNum != nums[0]);
                        numbers += nums[0] + ",";

                        for (int i = numbers.Split(new char[] { ',' }).Length - 1; i >= 0; i--)
                        {
                            if (numbers.Split(new char[] { ',' })[i] != "")
                                divstr += "<a href='" + page + ".aspx?cengshu=" + cengshu + "&SelectGrass=" + qishu + "&bh=" + numbers.Split(new char[] { ',' })[i] + "&isanzhi=" + isanzhi + "'>" + CommonDataBLL.GetPetNameByNumber(numbers.Split(new char[] { ',' })[i]) + "</a> →";
                        }

                    }
                }
                else
                    divstr += "<a href='" + page + ".aspx?cengshu=" + cengshu + "&SelectGrass=" + qishu + "&bh=" + nums[0] + "&isanzhi=" + isanzhi + "'>" + CommonDataBLL.GetPetNameByNumber(nums[0]) + "</a> →";

                Session["DHNumbers"] = nums;
            }

        }
        else
        {
            Session["DHNumbers"] = "";
            Session["DHNumbers"] = null;
        }

        return divstr;
    }

    [AjaxPro.AjaxMethod]
    public int GetLogoutCw(string number, int qishu, bool isAnzhi)
    {
        string field = "LayerBit2";
        if (isAnzhi)
        {
            field = "LayerBit1";
        }
        string strSql = "Select top 1 " + field + " From MemberInfoBalance" + qishu.ToString() + " Where number=@number";
        SqlParameter[] para = {
                                  new SqlParameter("@number",number)
                              };
        int cw = (int)DBHelper.ExecuteScalar(strSql, para, CommandType.Text);
        return cw;
    }
    [AjaxPro.AjaxMethod]
    public int GetShowCengS(int type)
    {
        string strSql = "Select isnull(cengshu,0) from ViewLayer Where type=@type";
        SqlParameter[] para = {
                                  new SqlParameter("@type",type)
                              };
        int cengs = (int)DBHelper.ExecuteScalar(strSql, para, CommandType.Text);
        return cengs;
    }

    [AjaxPro.AjaxMethod]
    public static int GetShowCengS1(int type)
    {
        string strSql = "Select isnull(cengshu,0) from ViewLayer Where type=@type";
        SqlParameter[] para = {
                                  new SqlParameter("@type",type)
                              };
        int cengs = (int)DBHelper.ExecuteScalar(strSql, para, CommandType.Text);
        return cengs;
    }


    [AjaxPro.AjaxMethod]
    public static int GetLogoutCw1(string number, int qishu, bool isAnzhi)
    {
        string field = "LayerBit2";
        if (isAnzhi)
        {
            field = "LayerBit1";
        }
        string strSql = "Select top 1 " + field + " From MemberInfoBalance" + qishu.ToString() + " Where number=@number";
        SqlParameter[] para = {
                                  new SqlParameter("@number",number)
                              };
        int cw = (int)DBHelper.ExecuteScalar(strSql, para, CommandType.Text);
        return cw;
    }


    [AjaxPro.AjaxMethod]
    public string GetManageId(int type)
    {
        return BLL.CommonClass.CommonDataBLL.getManageID(type);
    }

    [AjaxPro.AjaxMethod]
    public string IsChaoShi(string str)
    {
        string type = "0";
        if (Convert.ToDateTime(Session["ReFurbish_Timeout"]) < DateTime.Now)
        {
            type = "1";
            return type;
        }

        if (Session["Company"] == null && Session["Store"] == null && Session["Member"] == null && Session["Branch"] == null)
        {
            type = "1";
            return type;
        }
        string manageID = "";
        if (Application["jinzhi"] != null)
        {
            if (Session["Branch"] != null && (!BlackListBLL.GetSystem("B")))
            {
                type = "2";
                return type; //登陆设置 分公司登陆 退出
            }

            if (Session["Member"] != null && (!BlackListBLL.GetSystem("H")))
            {
                type = "2";
                return type;  //登陆设置 会员登陆 退出
            }

            if (Session["Store"] != null && (!BlackListBLL.GetSystem("D")))
            {
                type = "2";
                return type;  //登陆设置 店铺登陆 退出
            }

            manageID = BLL.CommonClass.CommonDataBLL.getManageID(1);

            if (Session["Company"] != null && Session["Company"].ToString() != manageID && (!BlackListBLL.GetSystem("G")))
            {
                type = "2";
                return type;  //登陆设置 管理员退出  除了''
            }

            if (Session["Company"] != null && Session["permission"] != null && Application["jinzhi"].ToString().IndexOf("J") >= 0) // 'J'是结算时的状态
            {
                Hashtable table = (Hashtable)HttpContext.Current.Session["permission"];
                if (!table.Contains(EnumCompanyPermission.FinanceJiesuan))
                {
                    type = "1";
                    return type;  //结算时 没有结算权限的管理员退出					
                }
            }
        }

        //会员被注销时，自动退出系统 
        if (Session["Member"] != null)
        {
            if (DAL.CommonDataDAL.GetIsActive(Session["Member"].ToString()))
            {
                type = "1";
                return type;
            }
        }

        string bianhao = "";
        int UserType = -1;

        int loginType = 0;

        if (Session["Member"] != null)
        {
            bianhao = Session["Member"].ToString();
            loginType = 3;
            UserType = 0;
        }
        else if (Session["Store"] != null)
        {
            bianhao = Session["Store"].ToString();
            loginType = 2;
            UserType = 4;
        }
        else if (Session["Company"] != null)
        {
            bianhao = Session["Company"].ToString();
            loginType = 1;
            UserType = 2;
        }
        else if (Session["Branch"] != null)
        {
            bianhao = Session["Branch"].ToString();
            UserType = 3;
        }


        manageID = BLL.CommonClass.CommonDataBLL.getManageID(loginType);

        // 黑名单处理 开始

        string[] SecPostion = Request.ServerVariables["REMOTE_ADDR"].ToString().Split('.');//客户IP地址

        //string ipAddress = SecPostion[0] + "." + SecPostion[1];
        string ipAddress = Request.ServerVariables["REMOTE_ADDR"].ToString();//客户IP地址
        try
        {
            if (bianhao != manageID && 0 < BlackListBLL.GetLikeIPCount(ipAddress))
            {
                type = "2";
                return type;
            }
        }
        catch
        {
            return "";
        }
        //限制区域登陆
        try
        {
            if (bianhao != manageID && BlackListBLL.GetLikeAddress(bianhao))
            {
                type = "2";
                return type;
            }
        }
        catch
        {
            return "";
        }

        if (bianhao == "" || UserType == -1) return "";
        try
        {
            if (0 < BlackListBLL.GetLikeIPCount(UserType, bianhao))
            {
                type = "2";
                return type;
            }
        }
        catch
        {
            return "";
        }
        // 黑名单处理 结束 	
        if (BLL.CommonClass.Login.isDenyLogin())
        //限时登陆
        {
            type = "2";
            return type;
        }

        return "";
    }






    /// <summary>
    /// 期数连动lab的起止时间
    /// </summary>
    /// <param name="txtGather"></param>
    /// <param name="orderId"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string GetStarEndDate(string value)
    {
        DataTable date = DBHelper.ExecuteDataTable("select stardate,enddate from config where ExpectNum=" + value);

        return Convert.ToDateTime(date.Rows[0][0]).ToString("yyyy-MM-dd") + "--" + Convert.ToDateTime(date.Rows[0][1]).ToString("yyyy-MM-dd");
    }

    [AjaxPro.AjaxMethod]
    public string GetViewBind(string number)
    {
        string str = "<a href='ShowNetworkView.aspx?bh=" + number + "'>" + number + "</a>";
        return str;
    }

    /// <summary>
    /// 根据产品ID，取得产品信息
    /// </summary>
    /// <param name="pid">产品ID</param>
    /// <returns></returns>
    public string GetProductInfo(string pid)
    {
        return "";
    }

    /// <summary>
    /// 获取会员报单
    /// </summary>
    /// <param name="member">会员编号</param>
    /// <param name="flag"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public ArrayList GetMemberOrder(string bh, string flag)
    {
        ArrayList list = new ArrayList();
        string name = MemberInfoDAL.GetMemberName(bh);
        foreach (DataRow dr in MemberOrderDAL.GetMemberOrder(bh, flag).Rows)
        {
            list.Add(dr["orderid"].ToString());
        }
        list.Add(name);
        return list;
    }

    /// <summary>
    /// 获取产品信息
    /// </summary>
    /// <param name="productid">产品ID</param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string GetProductDetail(string productid)
    {
        DataTable dt = CommonDataBLL.GetProductById(productid);

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

    /// <summary>
    /// 获取产品信息
    /// </summary>
    /// <param name="productid">产品ID</param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string GetProductDetail1(string productid)
    {
        DataTable dt = CommonDataBLL.GetProductById(productid);

        string html = " <table width=\"300\" border=\"0\" cellpadding=\"0\" cellspacing=\"4\" class=\"bjkk\">";

        html += "<tr><td width=\"105\"><img src=\"ReadImage.aspx?ProductID=" + dt.Rows[0]["ProductID"].ToString() + "\" style=\"width:105px;height:145px;\" /> </td>";
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
    public string isYueDu(string bh, string isNum)
    {
        int isyd = CommonDataBLL.isYueDu(bh, isNum);
        return isyd.ToString();
    }

    [AjaxPro.AjaxMethod]
    public string isYD(string bh, string isNum)
    {
        int isyd = CommonDataBLL.isYD(bh, isNum);
        return isyd.ToString();
    }

    [AjaxPro.AjaxMethod]
    public string GetGongGao(string isNum)
    {
        DataTable dt = CommonDataBLL.GetGongGao(isNum);
        string html = "";
        if (dt.Rows.Count > 0)
        {


            html = "<table  cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" border=\"0\" class=\"biaozzi\" >";

            html += "<tr> <td align='center' style=\"width:70%;height:50px;\">" + dt.Rows[0][0].ToString() + "</td> <td align='center' style=\"width:30%;height:50px;\"><input class='anyes' id='Button2E' onclick=\"isYD()\" type='button' value='已  阅'/></td></tr>";//产品名称

            html += "<tr> <td align='center' colspan=2 style=\"width:100%;\"> <div style='WIDTH:650px;height:350px;border:gray solid 1px;overflow:auto;padding-left:5px;padding-top:5px;'>";
            html += "<span id='DetailSpan' runat='server' >" + dt.Rows[0][1].ToString() + "</span>";
            html += "</div></td> </tr>";
            // html += "<tr> <td align='center' style=\"width:90%;\"></td> </tr>";//普通价积分



            html += "</table>";
        }
        return html;
    }


    [AjaxPro.AjaxMethod]
    public string GetStorePhones(string id, string type)
    {
        DataTable dt = new DataTable();
        if (type == "Store")
            dt = CommonDataBLL.GetStorePhone(id);
        else if (type == "Member")
            dt = CommonDataBLL.GetMemberPhone(id);

        string html = "<table  cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" border=\"0\" class=\"biaozzi\" >";

        html += "<tr> <td align='right' style='width:100px;'>" + BLL.Translation.TranslateFromDB("000065") + "：</td> <td> " + dt.Rows[0]["HomeTele"].ToString() + " </td>";//家庭电话
        html += "<tr> <td align='right'  style='width:100px;'>" + BLL.Translation.TranslateFromDB("000044") + "：</td> <td>" + dt.Rows[0]["OfficeTele"].ToString() + " </td> </tr>";//办公电话
        html += "<tr> <td align='right' style='width:100px;'>" + BLL.Translation.TranslateFromDB("005623") + "：</td> <td>" + dt.Rows[0]["MobileTele"].ToString() + " </td> </tr>";//手机号码
        html += "<tr> <td align='right' style='width:100px;'>" + BLL.Translation.TranslateFromDB("000643") + "：</td> <td>" + dt.Rows[0]["FaxTele"].ToString() + " </td> </tr>";//传真号码
        html += "<tr> <td align='right' style='width:100px;'>" + BLL.Translation.TranslateFromDB("000073") + "：</td> <td>" + dt.Rows[0]["PostalCode"].ToString() + " </td> </tr>";//邮编
        if (type == "Member")
        {
            html += "<tr> <td align='right' style='width:100px;'>会员地址：</td> <td>" + Encryption.Encryption.GetDecipherAddress(dt.Rows[0]["Address"].ToString()) + " </td> </tr>";//店铺地址
        }
        else
        {
            html += "<tr> <td align='right' style='width:100px;'>" + BLL.Translation.TranslateFromDB("001038") + "：</td> <td>" + Encryption.Encryption.GetDecipherAddress(dt.Rows[0]["Address"].ToString()) + " </td> </tr>";//店铺地址
        }


        html += "</table>";
        return html;
    }

    /// <summary>
    /// 绑定库位
    /// </summary>
    /// <param name="ddlDepotSeat">库位列表</param>
    /// <param name="wid"></param>
    [AjaxPro.AjaxMethod]
    public ArrayList BindDepotSeat(string wid)
    {
        ArrayList list = new ArrayList();
        foreach (DataRow dr in ReturnedGoodsBLL.GetDepotSeat(wid).Rows)
        {
            list.Add(new string[] { dr["SeatName"].ToString(), dr["DepotSeatID"].ToString() });
        }
        return list;
    }

    /// <summary>
    /// 绑定库位
    /// </summary>
    /// <param name="ddlDepotSeat">库位列表</param>
    /// <param name="wid"></param>
    [AjaxPro.AjaxMethod]
    public string ShenHe(string docid, string storeid, string WareHouseId, string DepotSeatid)
    {
        int i = new ReturnedGoodsBLL().GetStaInventoryDocByDocId(docid);
        if (i > 0)
        {
            return BLL.Translation.Translate("000583", "退货单") + docid + GetTran("001845", "已被审核过了,不需要再审核了!");
        }

        //查出此退货单里的产品ID和数量
        DataTable dt = new ReturnedGoodsBLL().GetProductIdAndQuantityByDocId(docid);
        foreach (DataRow dr in dt.Rows)
        {
            decimal tuihuo = Convert.ToDecimal(dr["productQuantity"].ToString());
            int kucun = new ReturnedGoodsBLL().GetCertainProductLeftStoreCount(dr["productid"].ToString(), storeid);
            if (kucun < Convert.ToInt32(tuihuo))
            {
                return BLL.Translation.Translate("001847", "对不起该店库存小于退货数量,请核对!");
            }
        }
        //审核退货单
        new ReturnedGoodsBLL().UpdateStaInventoryDocOfStateFlag(DepotSeatid, WareHouseId, Session["Company"].ToString(), docid, storeid);

        return "";
    }

    /// <summary>
    /// 绑定库位
    /// </summary>
    /// <param name="ddlDepotSeat">库位列表</param>
    /// <param name="wid"></param>
    [AjaxPro.AjaxMethod]
    public string HuanHuoShenHe(string docid, string storeid, string WareHouseId, string DepotSeatid)
    {
        DisplaceGoodBrowseBLL bll = new DisplaceGoodBrowseBLL();
        int i = bll.GetStateDisplaceDocByDocId(docid);
        if (i > 0)
        {
            return GetTran("000408", "换货单") + docid + "" + GetTran("001845", "已被审核过了,不需要再审核了!");
        }

        //判断换货店的库存是否大于等于退货的数量
        if (bll.CheckStoreGreaterThanDisplaceQuantity(storeid, docid) > 0)
        {
            return GetTran("005824", "店现有库存小于退货数量!");
        }
        //判断进货数量是否小于等于公司库存数量
        if (bll.CheckCompanyGreaterThanOderQuantity(docid) > 0)
        {
            return GetTran("005825", "公司现有库存小于店进货数量!");
        }
        //判断退货额加剩余订货额是否小于等于预进货额
        if (bll.CheckMoneyWheatherEnough(storeid, docid) < 0)
        {
            return GetTran("005826", "店退货额加剩余订货额小于进货额!");
        }
        //得到可用的换货单编号，返回换货单编号
        string DisplaceOrderId = docid;
        //根据单据类型的不同来获取不同的单据ID
        BLL.Logistics.ReturnedGoodsBLL returnedGoodsBLL = new ReturnedGoodsBLL();
        BLL.other.Company.InventoryDocBLL inventoryDocBLL = new InventoryDocBLL();
        //得到可用的订单编号         
        string storeOrderId = BLL.other.Company.InventoryDocBLL.GetNewOrderID();

        string refundmentOrderID = inventoryDocBLL.GetNewOrderID(Model.Other.EnumOrderFormType.ReturnOutStorage);
        //读取系统的最大期数。
        int expectNum = BLL.CommonClass.CommonDataBLL.getMaxqishu();

        //审核换货单
        if (bll.UpdateReplacement(DisplaceOrderId, storeOrderId, refundmentOrderID, storeid, expectNum, WareHouseId, DepotSeatid))
        {
            return "";
        }
        else
        {
            return GetTran("006041", "审核失败!");
        }

    }

    /// <summary>
    /// 获取安置人姓名
    /// </summary>
    /// <param name="comtext"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string gettjazname(string tjid)
    {

        string info = "";
        try
        {
            string sql = "select Name from memberInfo where number=@number";

            SqlParameter[] para = {
                                        new SqlParameter("@number",SqlDbType.NVarChar,30)
                                   };
            para[0].Value = tjid;

            SqlDataReader dr = DBHelper.ExecuteReader(sql, para, CommandType.Text);
            if (dr.Read())
            {
                info = Encryption.Encryption.GetDecipherName(dr[0].ToString().Trim());
                if (info == "")
                {
                    info = this.GetTran("000707");// "无姓名";
                }
            }
            else
            {
                info = this.GetTran("000710");// "编号不存在";
            }
            dr.Close();
        }
        catch
        {
            info = this.GetTran("000710");// "编号不存在";
        }
        return info;
    }

    /// <summary>
    /// 获取推荐人姓名(需要翻译)
    /// </summary>
    /// <param name="comtext"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string gettuijianvalue(string comtext)
    {
        string info = "";
        string sql = "select Name from memberInfo where number=@number";

        SqlParameter[] para = {
                                        new SqlParameter("@number",SqlDbType.NVarChar,30)
                                   };
        para[0].Value = comtext;


        System.Data.SqlClient.SqlDataReader dr = DBHelper.ExecuteReader(sql, para, CommandType.Text);
        if (dr.Read())
        {
            if (dr[0].ToString().Trim() == "")
            {
                //info = CommonData.GetClassTran("classes/AjaxClass.cs_87", "推荐人没有姓名");
                info = "推荐人没有姓名";
            }
            else
            {
                //info = CommonData.GetClassTran("classes/AjaxClass.cs_90", "推荐人的姓名是") + dr[0].ToString().Trim();
                info = "推荐人的姓名是" + dr[0].ToString().Trim();
            }
        }
        else
        {
            //info = CommonData.GetClassTran("classes/AjaxClass.cs_96", "您输入的推荐编号不存在");
            info = "您输入的推荐编号不存在";
        }
        dr.Close();
        return info;
    }
    //[AjaxPro.AjaxMethod]
    //public string GetHoues(string draw)
    //{
    //    string sxml=String.Empty;
    //    DataTable tb = null;
    //    string[] draws = draw.Split(':');
    //    switch (draws[0].ToLower())
    //    { 
    //        case "warehouse":
    //            tb = WareHouseDAL.GetProductWareHouseInfo();
    //            break ;
    //        case "depotseat":
    //            tb=DepotSeatDAL.GetDepotSeats("asdf");
    //            break;
    //    }
    //    if (tb == null)
    //        return "";
    //    if (tb.Rows.Count>0)
    //    {
    //        XmlDocument doc = new XmlDocument();
    //        doc.LoadXml("<Data></Data>");
    //        XmlElement root = doc.DocumentElement;
    //        for (int i = 0; i < tb.Rows.Count; i++)
    //        {
    //            XmlElement item = doc.CreateElement("Item");
    //            item.SetAttribute("key",tb.Rows[i]["ID"].ToString());
    //            item.SetAttribute("value", tb.Rows[i]["Name"].ToString());
    //            root.AppendChild(item);
    //        }
    //        sxml = doc.OuterXml;
    //    }
    //    return sxml;
    //}
    [AjaxPro.AjaxMethod]
    public string GetCountry(string draw)
    {
        DataTable dt = null;
        string[] draws = draw.Split(':');
        switch (draws[0])
        {
            case "m_country":
                dt = CountryBLL.GetCountrys();
                break;
            case "Province":
                dt = CountryBLL.GetProvince(draws[1]);
                break;
            case "City":
                dt = CountryBLL.GetCitys(draws[1], draws[2]);
                break;
            case "Xian":
                dt = CountryBLL.GetXians(draws[1], draws[2], draws[3]);
                break;
        }
        if (dt == null)
            return "";
        System.Text.StringBuilder strBld = new System.Text.StringBuilder();
        strBld.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
        strBld.Append("<Data>");
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strBld.Append("<Item>");
                strBld.Append(dt.Rows[i][0].ToString());
                strBld.Append("</Item>");

            }
        }
        strBld.Append("</Data>");

        return strBld.ToString();

    }

    /// <summary>
    /// 在线订货--判断选择付款类型
    /// </summary>
    /// <param name="state">选择类型</param>
    /// <returns>店铺余额</returns>
    [AjaxPro.AjaxMethod]
    public string StateMoney(string state, string storeid)
    {
        return BLL.other.Store.StoreInfoModifyBLL.GetStoreMoney(state, storeid);
    }

    //显示购物信息
    /// <summary>
    /// 
    /// </summary>
    /// <param name="strPid">产品ID字符串</param>
    /// <param name="storeID">店铺ID</param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string DataBindTxt(string strPid, string storeID, string productids, string cla, string clr, int curr)
    {
        int bzCurrency = CommonDataBLL.GetStandard();
        double huilv = (AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString())));
        double currency = 1;
        string selectcu = CurrencyDAL.GetJieCheng(StoreInfoDAL.GetStoreCurrency(storeID));
        if (curr > 0)
        {
            selectcu = CurrencyDAL.GetJieCheng(curr);
            currency = GetCurrency(StoreInfoDAL.GetStoreCurrency(storeID), curr);
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

            SqlDataReader dr = ProductDAL.GetProductInfo();

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
            //sb.Append("<th align=center bgcolor=" + clr + " ><b>&nbsp;&nbsp;" + BLL.Translation.Translate("000045", "期数") + "&nbsp;&nbsp;</b></th>");
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
                    sb.Append("<td align=\"center\">" + (Convert.ToDouble(dr["PreferentialPrice"]) / currency * huilv).ToString("0.00") + "</td>");
                    sb.Append("<td align=\"center\">" + iCount + "</td>");
                    sb.Append("<td align=\"center\">" + (price / currency * huilv).ToString("0.00") + "</td>");
                    sb.Append("<td align=\"center\">" + pv.ToString() + "</td>");
                    //sb.Append("<td align=\"center\">" + iCurQishu.ToString() + "</td>");
                    sb.Append("<td align=\"center\">" + dateNow + "</td>");
                    sb.Append("</tr>");
                    n++;
                }// "(" + selectcu + ")
            }
            dr.Close();
            sb.Append("<tr bgColor=\"#F1F4F8\">");
            sb.Append("<td colspan=\"3\" align=\"center\" class=\"biaozzi\" style=\"color:red;\" ><b>" + BLL.Translation.Translate("000247", "总计") + "：</b></td>");
            sb.Append("<td align=\"center\" class=\"xhzi\" style=\"color:red;\">" + (zongJine / currency * huilv).ToString("0.00") + "</td>");
            sb.Append("<td align=\"center\" class=\"xhzi\" style=\"color:red;\">" + zongPv.ToString() + "</td>");
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



    [AjaxPro.AjaxMethod]
    public void Shopping(string proid, string proName)
    {
        ArrayList list;



        string ProName = proName;
        string ProId = proid;

        //  string ProId = (sender as LinkButton).CommandArgument.ToString();



        //  Response.Write("<script>alert('" + ProId + "');</script>");

        if (Session["proList"] != null && Session["proList"].ToString() != "")
        {
            list = (ArrayList)Session["proList"];


            int s = 0;
            MemberDetailsModel md = new MemberDetailsModel();
            md.Quantity = 1;
            foreach (MemberDetailsModel memberDetailsModel in list)
            {

                if (memberDetailsModel.ProductId == Convert.ToInt32(ProId))
                {
                    memberDetailsModel.Quantity = memberDetailsModel.Quantity + 1;

                    //list.Remove(memberDetailsModel);
                    s = 1;
                }
                //else
                //{
                //    md.Quantity = 1;
                //}

            }
            md.ProductId = Convert.ToInt32(ProId);

            if (s != 1)
            {
                list.Add(md);
            }
        }
        else
        {
            list = new ArrayList();

            MemberDetailsModel md = new MemberDetailsModel();

            md.ProductId = Convert.ToInt32(ProId);
            md.Quantity = 1;

            list.Add(md);
        }
        Session["proList"] = list;



    }

    [AjaxPro.AjaxMethod]
    public void EnShopping(int num, string proid)
    {
        ArrayList list;



        //string ProName = proName;
        string ProId = proid;

        //  string ProId = (sender as LinkButton).CommandArgument.ToString();



        //  Response.Write("<script>alert('" + ProId + "');</script>");

        if (Session["proList"] != "" && Session["proList"] != null)
        {
            list = (ArrayList)Session["proList"];


            int s = 0;
            MemberDetailsModel md = new MemberDetailsModel();
            md.Quantity = 1;
            foreach (MemberDetailsModel memberDetailsModel in list)
            {

                if (memberDetailsModel.ProductId == Convert.ToInt32(ProId))
                {
                    memberDetailsModel.Quantity = num;

                    //list.Remove(memberDetailsModel);
                    s = 1;
                }
                //else
                //{
                //    md.Quantity = 1;
                //}

            }
            md.ProductId = Convert.ToInt32(ProId);

            if (s != 1)
            {
                list.Add(md);
            }
        }
        else
        {
            list = new ArrayList();

            MemberDetailsModel md = new MemberDetailsModel();

            md.ProductId = Convert.ToInt32(ProId);
            md.Quantity = 1;

            list.Add(md);
        }
        Session["proList"] = list;



    }

    [AjaxPro.AjaxMethod]
    public string divhtml()
    {
        ArrayList list = (ArrayList)Session["proList"];

        string pid = "";
        string productid = "";

        if (Session["proList"] != "" && Session["proList"] != null)
        {
            foreach (MemberDetailsModel memberDetailsModel in list)
            {
                pid += memberDetailsModel.Quantity + ",";

                productid += "n" + memberDetailsModel.ProductId + ",";


            }
        }
        string storeid = BLL.CommonClass.CommonDataBLL.getManageID(2);
        int curr = 1;

        //AjaxClass aj = new AjaxClass();

        string a = DataBindTxtShopp(pid, storeid, productid, "tablemb", "", curr);

        return a;
    }


    [AjaxPro.AjaxMethod]
    public string DataBindTxtShopp(string strPid, string storeID, string productids, string cla, string clr, int curr)
    {

        double currency = 1;
        string selectcu = CurrencyDAL.GetJieCheng(StoreInfoDAL.GetStoreCurrency(storeID));
        if (curr > 0)
        {
            selectcu = CurrencyDAL.GetJieCheng(curr);
            currency = GetCurrency(StoreInfoDAL.GetStoreCurrency(storeID), curr);
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
            string strSql = "Select * From Product Where IsSell=0";
            SqlDataReader dr = DBHelper.ExecuteReader(strSql);

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


            sb.Append("<table  border=0 align=center style=\"width:100%\" cellpadding=0 cellspacing=1 class=" + cla + " style=\"width:100%;\">");
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
                //					if(ar.Contains (productId_))
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
                        sb.Append("<tr bgColor=\"#F1F4F8\" >");
                    }
                    else
                    {
                        sb.Append("<tr >");
                    }
                    sb.Append("<td align=\"center\">" + pName.ToString() + "</td>");
                    sb.Append("<td align=\"center\">" + (Convert.ToDouble(dr["PreferentialPrice"]) / currency).ToString("0.00") + "</td>");
                    sb.Append("<td align=\"center\"><input maxLength=6 value=" + iCount + " type=text class=priceBox  size=3 name=N" + productid + " onblur='EnShopp(this," + productid + ")'></input></td>");
                    sb.Append("<td align=\"center\">" + (price / currency).ToString("0.00") + "</td>");
                    sb.Append("<td align=\"center\">" + pv.ToString() + "</td>");
                    sb.Append("<td align=\"center\">" + iCurQishu.ToString() + "</td>");
                    sb.Append("<td align=\"center\">" + dateNow + "</td>");
                    sb.Append("</tr>");
                    n++;
                }//"(" + selectcu + ")
            }
            dr.Close();
            sb.Append("<tr bgColor=\"#F1F4F8\">");
            sb.Append("<td colspan=\"3\" align=\"center\" class=\"biaozzi\" style=\"color:red;\" ><b>" + BLL.Translation.Translate("000247", "总计") + "：</b></td>");
            sb.Append("<td align=\"center\" class=\"xhzi\" style=\"color:red;\">" + (zongJine / currency).ToString("0.00") + "</td>");
            sb.Append("<td align=\"center\" class=\"xhzi\" style=\"color:red;\">" + zongPv.ToString() + "</td>");
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
            return SetRateBLL.GetCurrencyBySql(from, to);
        }
        else if (System.Web.HttpContext.Current.Application["CurrencySetting"].ToString() == "Currency")
        {
            string fromcu = CurrencyDAL.GetJieCheng(from);
            string tocu = CurrencyDAL.GetJieCheng(to);
            net.webservicex.www.CurrencyConvertor CC = new net.webservicex.www.CurrencyConvertor();
            return CC.ConversionRate((net.webservicex.www.Currency)Enum.Parse(typeof(net.webservicex.www.Currency), fromcu), (net.webservicex.www.Currency)Enum.Parse(typeof(net.webservicex.www.Currency), tocu));
        }
        else
        {
            return 1;
        }
    }

    /// <summary>
    /// 获取及时汇率--哟标准币种转换成1——ds2012——tianfeng
    /// </summary>
    /// <param name="to"> 转换成的币种 </param>
    /// <returns> 汇率 </returns>
    public static double GetCurrency(int to)
    {
        int from = SetRateBLL.GetDefaultCurrencyId();
        if (System.Web.HttpContext.Current.Application["CurrencySetting"] == null)
        {
            //获取汇率
            return SetRateBLL.GetCurrencyBySql(from, to);
        }
        else if (System.Web.HttpContext.Current.Application["CurrencySetting"].ToString() == "Currency")
        {
            //标准币种简称
            string fromcu = CurrencyDAL.GetJieCheng(from);
            //会员币种简称
            string tocu = CurrencyDAL.GetJieCheng(to);
            net.webservicex.www.CurrencyConvertor CC = new net.webservicex.www.CurrencyConvertor();
            return CC.ConversionRate((net.webservicex.www.Currency)Enum.Parse(typeof(net.webservicex.www.Currency), fromcu), (net.webservicex.www.Currency)Enum.Parse(typeof(net.webservicex.www.Currency), tocu));
        }
        else
        {
            return 1;
        }
    }

    /// <summary>
    /// 获取及时汇率--Ajax
    /// </summary>
    /// <param name="to"> 转换成的币种 </param>
    /// <returns> 汇率 </returns>
    [AjaxPro.AjaxMethod]
    public string GetCurrency_Ajax(int from, int to)
    {
        if (System.Web.HttpContext.Current.Application["CurrencySetting"] == null)
        {
            return SetRateBLL.GetCurrencyBySql(from, to).ToString();
        }
        else
        {
            return "1";
        }
    }

    [AjaxPro.AjaxMethod]
    public string CheckProduct(string num, string storeID, string productids)
    {
        SqlDataReader dr = null;
        string str = "";
        try
        {
            string[] nums = Regex.Split(num, ",", RegexOptions.IgnoreCase);
            string[] strProductIDs = Regex.Split(productids, ",", RegexOptions.IgnoreCase);
            string strSql = "select isNull((-(ActualStorage+HasOrderCount)),0),ProductID from Stock where (ActualStorage+HasOrderCount)<0 and storeid='" + storeID + "'";
            dr = DBHelper.ExecuteReader(strSql);

            while (dr.Read())
            {
                for (int i = 0; i < nums.Length; i++)
                {
                    if (strProductIDs[i] == ("N" + dr.GetInt32(1)))
                    {
                        if ((dr.GetDecimal(0)) > Decimal.Parse(nums[i]))
                        {
                            string name = DBHelper.ExecuteScalar("select ProductName from Product where ProductID='" + dr.GetInt32(1) + "'").ToString();
                            str += name + "数量不能少于" + (dr.GetDecimal(0)) + "；";
                        }
                    }
                }
                if (productids.IndexOf("N" + dr.GetInt32(1)) < 0)
                {
                    string name = DBHelper.ExecuteScalar("select ProductName from Product where ProductID='" + dr.GetInt32(1) + "'").ToString();
                    str += name + "数量不能少于" + (dr.GetDecimal(0)) + "；";
                }
            }
        }
        catch { }
        finally
        {
            dr.Close();
        }

        return str;
    }

    [AjaxPro.AjaxMethod]
    public string WangLuoTu12(string number, string tree, int ExpectNum, int ISAZ, string storeid)
    {
        SqlParameter[] paraJB ={
									  new SqlParameter("@ID",     SqlDbType.VarChar,20),
									  new SqlParameter("@TREE",   SqlDbType.VarChar,400),
									  new SqlParameter("@ISAZ",   SqlDbType.Int),
									  new SqlParameter("@CS ",    SqlDbType.Int),
									  new SqlParameter("@ExpectNum ", SqlDbType.Int),
									  new SqlParameter("@storeID",SqlDbType.VarChar,20)
								  };
        paraJB[0].Value = number;
        paraJB[1].Value = tree;
        paraJB[2].Value = ISAZ;
        paraJB[3].Value = 3;
        paraJB[4].Value = ExpectNum;
        paraJB[5].Value = storeid;

        string str = "";
        DataTable dt = DBHelper.ExecuteDataTable("JS_TreeNet", paraJB, CommandType.StoredProcedure);

        foreach (DataRow row in dt.Rows)
        {
            str += row[0].ToString();
        }
        return str;
    }

    [AjaxPro.AjaxMethod]
    public string WangLuoTu_str12(string bianhao, int qishu)
    {
        string[] state = new string[] { "level", "CurrentOneMark", "CurrentTotalNetRecord", "NotTotalMark", "TotalNotNetRecord", "CurrentNewNetNum", "TotalNetNum" };
        string[] data = new string[8];
        string register_qishu = "";
        string sql = "";
        string str = "";
        string shuju = "";

        DataTable dt = DBHelper.ExecuteDataTable("select  field,name  from  NetWorkDisplayStatus    where   flag=1 and type=2");
        DataRow[] row;
        data[0] = DBHelper.ExecuteScalar("select  name  from  MemberInfo  where  Number='" + bianhao + "'").ToString();
        register_qishu = DBHelper.ExecuteScalar("select  expectNum  from  MemberInfo  where  Number='" + bianhao + "'").ToString();

        for (int i = 0; i < state.Length; i++)
        {
            row = dt.Select("field='" + state[i].ToString().Trim() + "'");
            if (row.Length > 0)
            {
                sql = "select " + state[i].ToString() + " from  MemberInfoBalance" + qishu + "  where  Number='" + bianhao + "'";
                shuju = DBHelper.ExecuteScalar(sql).ToString();
                if (state[i].ToString().Trim().ToLower() == "level")
                    data[i + 1] = shuju;//AjaxClass.GetLevel1(shuju);
                else
                    data[i + 1] = Convert.ToDecimal(shuju).ToString("f0");

                foreach (DataRow pp in row)
                {
                    str += "&nbsp;<span  class=ls title=" + pp["name"].ToString() + ">[" + data[i + 1] + "]</span>";
                }
            }
        }

        if (qishu == Convert.ToInt32(register_qishu))
            str = "</span><span class=ls><font  color=red>" + bianhao + "</font>&nbsp;" + data[0] + "</span>" + str;
        else
            str = "</span><span class=ls><font  class=ls >" + bianhao + "</font>&nbsp;" + data[0] + "</span>" + str;
        return str;
    }

    [AjaxPro.AjaxMethod]
    public string WangLuoTu(string number, string tree, int ExpectNum, int ISAZ, string storeid)
    {
        TreeViewBLL treeViewBLL = new TreeViewBLL();
        //number = "";
        //int layerNum = 1;
        //获得存储过程产生的树
        DataTable table = treeViewBLL.GetExtendTreeView(number, tree, ISAZ, 3, ExpectNum, storeid);
        string tree2 = "";
        //循环拼出树
        foreach (DataRow row in table.Rows)
        {
            tree2 += row[0].ToString();
        }
        return tree2;
    }

    [AjaxPro.AjaxMethod]
    public string WangLuoTu_str(string number, int ExpectNum)
    {
        string[] state = new string[] { "level", "CurrentOneMark", "CurrentTotalNetRecord", "NotTotalMark", "TotalNotNetRecord", "CurrentNewNetNum", "TotalNetNum" };
        string[] data = new string[8];
        string register_ExpectNum = "";
        string sql = "";
        string str = "";
        string shuju = "";

        DataTable dt = DAL.DBHelper.ExecuteDataTable("select  field,name  from  NetWorkDisplayStatus    where   flag=1");
        DataRow[] row;
        data[0] = DAL.DBHelper.ExecuteScalar("select  petname  from  memberInfo  where  number='" + number + "'").ToString();
        register_ExpectNum = DAL.DBHelper.ExecuteScalar("select  ExpectNum  from  memberInfo  where  number='" + number + "'").ToString();

        for (int i = 0; i < state.Length; i++)
        {
            row = dt.Select("field='" + state[i].ToString().Trim() + "'");
            if (row.Length > 0)
            {
                sql = "select " + state[i].ToString() + " from  MemberInfoBalance" + ExpectNum + "  where  number='" + number + "'";
                shuju = DAL.DBHelper.ExecuteScalar(sql).ToString();
                if (state[i].ToString().Trim() == "level")
                    data[i + 1] = AjaxClass.GetLevel1(shuju);
                else
                    data[i + 1] = Convert.ToDecimal(shuju).ToString("f0");

                foreach (DataRow pp in row)
                {
                    str += "&nbsp;<span  class=ls title=" + pp["name"].ToString() + ">[" + data[i + 1] + "]</span>";
                }

            }
        }
        if (ExpectNum == Convert.ToInt32(register_ExpectNum))
            str = "</span><span class=ls><font  color=red>" + number + "</font>&nbsp;" + data[0] + "</span>" + str;
        else
            str = "</span><span class=ls><font  class=ls >" + number + "</font>&nbsp;" + data[0] + "</span>" + str;
        return str;
    }

    [AjaxPro.AjaxMethod]
    public string GetNetWorkDisplayStatus(int type)
    {
        DataTable dt = BLL.CommonClass.CommonDataBLL.GetNetWorkDisplayStatus1(type);
        string typeList = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            typeList += dt.Rows[i][0].ToString() + ",";
        }
        return typeList;
    }

    [AjaxPro.AjaxMethod]
    public string WangLuoTu2(string number, string tree, int ExpectNum)
    {
        tree = tree.Replace("<img src='../images/02.gif' />", "＿").Replace("<img src='../images/03.gif' />", "~").Replace("<img src='../images/04.gif' />", "|");
        TreeViewBLL treeViewBLL = new TreeViewBLL();
        //number = "";
        //int layerNum = 1;
        //获得存储过程产生的树
        int type = 0;
        if (System.Web.HttpContext.Current.Session["Company"] != null)
        {
            type = 0;
        }
        else if (System.Web.HttpContext.Current.Session["Store"] != null)
        {
            type = 1;
        }
        else if (System.Web.HttpContext.Current.Session["Member"] != null)
        {
            type = 2;
        }
        DataTable table = treeViewBLL.GetExtendTreeView_NewAz(number, tree, 3, ExpectNum, type, 0);
        string tree2 = "";
        //循环拼出树
        foreach (DataRow row in table.Rows)
        {
            tree2 += row[0].ToString();
        }
        return tree2.Replace("＿", "<img src='../images/02.gif' />").Replace("~", "<img src='../images/03.gif' />").Replace("|", "<img src='../images/04.gif' />");
    }

    [AjaxPro.AjaxMethod]
    public string GetHtml(string bh, string tree, int qishu, int cengshu, bool isAnZhi, string cw)
    {
        tree = tree.Replace("<img src='../images/011.gif'  align=absmiddle  border=0 />", "─");
        tree = tree.Replace("<img src='../images/013.gif'  align=absmiddle  border=0 />", "☆");
        tree = tree.Replace("<img src='../images/014.gif'  align=absmiddle  border=0 />", "★").Replace("<img src='../images/006.gif'  align=absmiddle  border=0 />", "├").Replace("<img src='../images/003.gif'  align=absmiddle  border=0 />", "└").Replace("<img src='../images/015.gif'  align=absmiddle  border=0 />", "~").Replace("<img src='../images/004.gif'  align=absmiddle  border=0 />", "|");

        TreeViewBLL treeViewBLL = new TreeViewBLL();
        //获得存储过程产生的树
        int type = 0;
        if (System.Web.HttpContext.Current.Session["Company"] != null)
        {
            type = 0;
        }
        else if (System.Web.HttpContext.Current.Session["Store"] != null)
        {
            type = 1;
        }
        else if (System.Web.HttpContext.Current.Session["Member"] != null)
        {
            type = 2;
        }
        DataTable table;
        if (isAnZhi)
        {
            table = treeViewBLL.GetExtendTreeView_NewAz(bh, tree, cengshu, qishu, type, Convert.ToInt32(cw));
        }
        else
        {
            table = treeViewBLL.GetExtendTreeView_NewTj(bh, tree, cengshu, qishu, type, Convert.ToInt32(cw));
        }
        string str = "";
        //循环拼出树
        str = "";

        int count = 0;
        foreach (DataRow row in table.Rows)
        {
            count++;
            if (row["bh"].ToString() != bh)
            {
                string strStyle = "";
                if (count % 2 == 0)
                {
                    strStyle = "background-color:#F1F4F8";
                }
                else
                {
                    strStyle = " background-color:#FAFAFA";
                }
                str += "<tr style='" + strStyle + "' class='tr' id='tr" + row["bh"].ToString() + "' onmousedown=\"down_tw(event,this)\">";
                str += "<td valing='middle'>" + row["htmltree"].ToString().Replace("─", "<img src='../images/011.gif'  align=absmiddle  border=0 />").Replace("☆", "<img src='../images/013.gif'  align=absmiddle  border=0 />").Replace("★", "<img src='../images/014.gif'  align=absmiddle  border=0 />").Replace("├", "<img src='../images/006.gif'  align=absmiddle  border=0 />").Replace("└", "<img src='../images/003.gif'  align=absmiddle  border=0 />").Replace("~", "<img src='../images/015.gif'  align=absmiddle  border=0 />").Replace("|", "<img src='../images/004.gif'  align=absmiddle  border=0 />") + "<img src='../images/1.png' class='img' align=absmiddle border=0 /></td>";
                str += "<td align='center' valing='middle' title='层数'>" + row["cw"].ToString() + "</td>";
                str += "<td align='left' valing='middle' title='级别'>" + row["level"].ToString() + "</td>";
                str += "<td align='right' valing='middle' title='新个分数'>" + row["CurrentOneMark"].ToString() + "</td>";
                if (isAnZhi)
                {
                    str += "<td align='right' valing='middle' title='新网分数'>" + row["CurrentTotalNetRecord"].ToString() + "</td>";
                    str += "<td align='right' valing='middle' title='新网人数'>" + row["CurrentNewNetNum"].ToString() + "</td>";
                    str += "<td align='right' valing='middle' title='总网人数'>" + row["TotalNetNum"].ToString() + "</td>";
                    str += "<td align='right' valing='middle' title='总网分数'>" + row["TotalNetRecord"].ToString() + "</td>";
                }
                else
                {
                    str += "<td align='right' valing='middle' title='新网分数'>" + row["DCurrentTotalNetRecord"].ToString() + "</td>";
                    str += "<td align='right' valing='middle' title='新网人数'>" + row["DCurrentNewNetNum"].ToString() + "</td>";
                    str += "<td align='right' valing='middle' title='总网人数'>" + row["DTotalNetNum"].ToString() + "</td>";
                    str += "<td align='right' valing='middle' title='总网分数'>" + row["DTotalNetRecord"].ToString() + "</td>";
                }

                str += "</tr>";
            }

        }
        return str;
    }


    [AjaxPro.AjaxMethod]
    public string GetHtml1(string bh, string tree, int qishu, int cengshu, bool isAnZhi, string cw)
    {
        tree = tree.Replace("<img src='../images/011.gif'  align=absmiddle  border=0 />", "─");
        tree = tree.Replace("<img src='../images/013.gif'  align=absmiddle  border=0 />", "☆");
        tree = tree.Replace("<img src='../images/014.gif'  align=absmiddle  border=0 />", "★").Replace("<img src='../images/006.gif'  align=absmiddle  border=0 />", "├").Replace("<img src='../images/003.gif'  align=absmiddle  border=0 />", "└").Replace("<img src='../images/015.gif'  align=absmiddle  border=0 />", "~").Replace("<img src='../images/004.gif'  align=absmiddle  border=0 />", "|");

        TreeViewBLL treeViewBLL = new TreeViewBLL();
        //获得存储过程产生的树
        int type = 0;
        if (System.Web.HttpContext.Current.Session["Company"] != null)
        {
            type = 0;
        }
        else if (System.Web.HttpContext.Current.Session["Store"] != null)
        {
            type = 1;
        }
        else if (System.Web.HttpContext.Current.Session["Member"] != null)
        {
            type = 2;
        }
        DataTable table;

        table = treeViewBLL.GetExtendTreeView_NewAz1(bh, tree, cengshu, qishu, type, Convert.ToInt32(cw));

        string str = "";
        //循环拼出树
        str = "";

        int count = 0;
        foreach (DataRow row in table.Rows)
        {
            count++;
            if (row["bh"].ToString() != bh)
            {
                string strStyle = "";
                if (count % 2 == 0)
                {
                    strStyle = "background-color:#F1F4F8";
                }
                else
                {
                    strStyle = " background-color:#FAFAFA";
                }
                str += "<tr style='" + strStyle + "' class='tr' id='tr" + row["bh"].ToString() + "' onmousedown=\"down_tw(event,this)\">";
                str += "<td valing='middle'>" + row["htmltree"].ToString().Replace("─", "<img src='../images/011.gif'  align=absmiddle  border=0 />").Replace("☆", "<img src='../images/013.gif'  align=absmiddle  border=0 />").Replace("★", "<img src='../images/014.gif'  align=absmiddle  border=0 />").Replace("├", "<img src='../images/006.gif'  align=absmiddle  border=0 />").Replace("└", "<img src='../images/003.gif'  align=absmiddle  border=0 />").Replace("~", "<img src='../images/015.gif'  align=absmiddle  border=0 />").Replace("|", "<img src='../images/004.gif'  align=absmiddle  border=0 />") + "<img src='../images/1.png' class='img' align=absmiddle border=0 /></td>";
                str += "<td align='center' valing='middle' title='层数'>" + row["cw"].ToString() + "</td>";
                str += "<td align='left' valing='middle' title='级别'>" + row["level"].ToString() + "</td>";
                str += "<td align='right' valing='middle' title='新个分数'>" + row["CurrentOneMark"].ToString() + "</td>";
                if (isAnZhi)
                {
                    str += "<td align='right' valing='middle' title='新网分数'>" + row["CurrentTotalNetRecord"].ToString() + "</td>";
                    str += "<td align='right' valing='middle' title='新网人数'>" + row["CurrentNewNetNum"].ToString() + "</td>";
                    str += "<td align='right' valing='middle' title='总网人数'>" + row["TotalNetNum"].ToString() + "</td>";
                    str += "<td align='right' valing='middle' title='总网分数'>" + row["TotalNetRecord"].ToString() + "</td>";
                }
                else
                {
                    str += "<td align='right' valing='middle' title='新网分数'>" + row["DCurrentTotalNetRecord"].ToString() + "</td>";
                    str += "<td align='right' valing='middle' title='新网人数'>" + row["DCurrentNewNetNum"].ToString() + "</td>";
                    str += "<td align='right' valing='middle' title='总网人数'>" + row["DTotalNetNum"].ToString() + "</td>";
                    str += "<td align='right' valing='middle' title='总网分数'>" + row["DTotalNetRecord"].ToString() + "</td>";
                }

                str += "</tr>";
            }

        }
        return str;
    }

    [AjaxPro.AjaxMethod]
    public string GetIsTop(string bianhao, int isAnzhi, string manageId)
    {
        string sql = "select count(0) from viewmanage where type=" + isAnzhi + " and number='" + bianhao + "' and manageid='" + manageId + "'";
        int count = Convert.ToInt32(DBHelper.ExecuteScalar(sql));
        return count.ToString();
    }

    [AjaxPro.AjaxMethod]
    public string GetNumberState(string number, string qishu)
    {
        int regQishu = (int)DBHelper.ExecuteScalar("select isnull(expectnum,0) from memberinfo where number='" + number + "'");
        if (regQishu < CommonDataBLL.getMaxqishu())
        {
            string strSql = "select isnull(isactive,0) from MemberInfo where number=@number";
            SqlParameter[] para = {
                                      new SqlParameter("@number",SqlDbType.NVarChar,30)
                                  };
            para[0].Value = number;
            int isSh = (int)DBHelper.ExecuteScalar(strSql, para, CommandType.Text);
            if (isSh == 0)
            {
                return "4";//以前期未审核
            }
            else
            {
                return "1";//以前期已审核
            }
        }
        else
        {
            string strSql = "select isnull(isactive,0) from MemberInfo where number=@number";
            SqlParameter[] para = {
                                      new SqlParameter("@number",SqlDbType.NVarChar,30)
                                  };
            para[0].Value = number;
            int isSh = (int)DBHelper.ExecuteScalar(strSql, para, CommandType.Text);
            if (isSh == 0)
            {
                return "2";//当期未审核
            }
            else
            {
                return "3";//当期已审核
            }
        }
    }

    [AjaxPro.AjaxMethod]
    public string WangLuoTu_str2(string number, int ExpectNum, string tree)
    {
        string[] state = new string[] { "level", "CurrentOneMark", "CurrentTotalNetRecord", "CurrentNewNetNum", "TotalNetNum", "TotalNetRecord" };
        string[] data = new string[8];
        string register_ExpectNum = "";
        string sql = "";
        string str = "";
        string shuju = "";

        DataTable dt = DAL.DBHelper.ExecuteDataTable("select  field,name  from  NetWorkDisplayStatus    where   flag=1");
        DataRow[] row;
        data[0] = DAL.DBHelper.ExecuteScalar("select  petname  from  memberInfo  where  number='" + number + "'").ToString();
        register_ExpectNum = DAL.DBHelper.ExecuteScalar("select  ExpectNum  from  memberInfo  where  number='" + number + "'").ToString();

        for (int i = 0; i < state.Length; i++)
        {
            row = dt.Select("field='" + state[i].ToString().Trim() + "'");
            if (row.Length > 0)
            {
                sql = "select " + state[i].ToString() + " from  MemberInfoBalance" + ExpectNum + "  where  number='" + number + "'";
                shuju = Convert.ToDecimal(DAL.DBHelper.ExecuteScalar(sql)).ToString();
                if (state[i].ToString().Trim() == "level")
                    data[i + 1] = AjaxClass.GetLevel(shuju);
                else
                {
                    if (shuju != "0")
                    {
                        data[i + 1] = (Convert.ToDouble(shuju)).ToString();
                    }
                    else
                    {
                        data[i + 1] = "0";
                    }
                }

                foreach (DataRow pp in row)
                {
                    if (state[i].ToString().Trim() == "level")
                    {
                        str += "<span  class=js title=" + pp["name"].ToString() + "><img height='18' src='" + data[i + 1] + "' /></span>";
                    }
                    else
                    {
                        str += "<span  class=js title=" + pp["name"].ToString() + ">[" + data[i + 1] + "]</span>";
                    }
                }
            }
        }

        int isActive = Convert.ToInt32(DAL.DBHelper.ExecuteScalar("select top 1 isnull(isactive,0) from memberinfo where number='" + number + "'"));
        string color = "";
        if (ExpectNum == Convert.ToInt32(register_ExpectNum))
        {
            if (isActive == 1)
            {
                color = "color:#E30000";
            }
            else
            {
                color = "color:##FF8686";
            }
            str = "<span class=ss id='" + number + "' tree=\"" + tree + "\"  ONCLICK=JSNET(this.id,this.attributes[\"tree\"].value,1," + ExpectNum + ")>田<span class=bh id=n" + number + " onMouseUp=popUp(this.id," + ExpectNum + ",event) title='编号' style='" + color + "'>" + number + "</span>  <span class=xm title='姓名'>" + data[0] + "</span>" + str + "</br>";
            //str = "</span><span class=ls title='编号和姓名' style='" + color + "'>" + number + "</span>  <span class=xm>" + data[0] + "</span>" + str;
        }
        else
        {
            if (isActive == 1)
            {
                color = "color:black";
            }
            else
            {
                color = "color:Silver";
            }
            // str = "</span><span class=ls title='编号和姓名' style='" + color + "'>" + number + "</span>  <span class=xm>" + data[0] + "</span>" + str;
            str = "<span class=ss id='" + number + "' tree=\"" + tree + "\"  ONCLICK=JSNET(this.id,this.attributes[\"tree\"].value,1," + ExpectNum + ")>田<span class=bh id=n" + number + " onMouseUp=popUp(this.id," + ExpectNum + ",event) title='编号' style='" + color + "'>" + number + "</span>  <span class=xm title='姓名'>" + data[0] + "</span>" + str + "</br>";
        }
        return str;
    }

    [AjaxPro.AjaxMethod]
    public string getXHFW(string number, bool isAnzhi, int QiShu)
    {
        int startXH = 0, endXH = 0, baseCengwei = 0;
        string myXuhao, myCengwei;
        if (isAnzhi)
        {
            myXuhao = "Ordinal1";
            myCengwei = "LayerBit1";
        }
        else
        {
            myXuhao = "Ordinal2";
            myCengwei = "LayerBit2";
        }
        Jiegou.getXHFW(number, isAnzhi, QiShu, out startXH, out endXH, out baseCengwei);

        string sql = "select j.number from MemberInfoBalance" + QiShu.ToString() + " as J where  J." + myXuhao + ">=" + startXH.ToString() + " and J." + myXuhao + "<=" + endXH.ToString() + " order by J." + myXuhao + " DESC";

        SqlDataReader dr = DBHelper.ExecuteReader(sql);

        string spanId = "";
        int count = 0;
        while (dr.Read())
        {
            if (count != 0)
            {
                spanId += ",";
            }
            spanId += dr["number"].ToString();
            count++;
        }
        dr.Close();

        return spanId;
    }

    [AjaxPro.AjaxMethod]
    public string WangLuoTu1(string number, string tree, int ExpectNum)
    {
        tree = tree.Replace("<img src='../images/02.gif' />", "＿").Replace("<img src='../images/03.gif' />", "~").Replace("<img src='../images/04.gif' />", "|");
        TreeViewBLL treeViewBLL = new TreeViewBLL();
        //number = "";
        //int layerNum = 1;
        //获得存储过程产生的树
        int type = 0;
        if (System.Web.HttpContext.Current.Session["Company"] != null)
        {
            type = 0;
        }
        else if (System.Web.HttpContext.Current.Session["Store"] != null)
        {
            type = 1;
        }
        else if (System.Web.HttpContext.Current.Session["Member"] != null)
        {
            type = 2;
        }
        DataTable table = treeViewBLL.GetExtendTreeView_NewTj(number, tree, 3, ExpectNum, type);
        string tree2 = "";
        //循环拼出树
        int asg = table.Rows.Count;
        foreach (DataRow row in table.Rows)
        {
            tree2 += row[0].ToString();
        }
        return tree2.Replace("＿", "<img src='../images/02.gif' />").Replace("~", "<img src='../images/03.gif' />").Replace("|", "<img src='../images/04.gif' />");
    }

    //安置
    [AjaxPro.AjaxMethod]
    public string WangLuoTuNew(string bianhao, string qs)
    {
        //获得存储过程产生的树
        int type = 0;//公司查看网络图
        int qishu = Convert.ToInt32(qs);
        string tree = "";
        int cengshu = 5;
        DataTable table = new TreeViewBLL().GetExtendTreeView_NewAz(bianhao, tree, cengshu, qishu, type, 0);
        string str = "";
        //循环拼出树
        str = "<table id='tab_tr' border=\"0\" cellspacing=\"0\" cellpadding=\"0\"  class='tree_grid'>";
        str += "<tr>"
               + "<th align='center'>会员编号</th>"
               + "<th align='center' >层数</th>"
               + "<th align='center'>级别</th>"
               + "<th align='right'>新个分数</th>"
               + "<th align='right'>新网分数</th>"
               + "<th align='right'>新网人数</th>"
               + "<th align='right' >总网人数</th>"
               + "<th align='right' >总网分数</th>"

               + "</tr>";
        int count = 0;
        foreach (DataRow row in table.Rows)
        {
            count++;

            string strStyle = "";
            if (count % 2 == 0)
            {
                strStyle = "background-color:#F1F4F8"; ;
            }
            else
            {
                strStyle = " background-color:#FAFAFA";
            }
            str += "<tr style='" + strStyle + "' class=\"tr\"  id='tr" + row["bh"].ToString() + "'  onmousedown=\"down_tw(event,this)\">";
            str += "<td valing='middle'>" + row["htmltree"].ToString().Replace("─", "<img src='../images/011.gif'  align=absmiddle  border=0 />").Replace("☆", "<img src='../images/013.gif'  align=absmiddle  border=0 />").Replace("★", "<img src='../images/014.gif'  align=absmiddle  border=0 />").Replace("├", "<img src='../images/006.gif'  align=absmiddle  border=0 />").Replace("└", "<img src='../images/003.gif'  align=absmiddle  border=0 />").Replace("~", "<img src='../images/015.gif'  align=absmiddle  border=0 />").Replace("|", "<img src='../images/004.gif'  align=absmiddle  border=0 />") + "<img src='../images/1.png' class='img' align=absmiddle border=0 /></td>";
            str += "<td align='center' valing='middle' title='层数'>" + row["cw"].ToString() + "</td>";
            str += "<td align='left' valing='middle' title='级别'>" + row["level"].ToString() + "</td>";
            str += "<td align='right' valing='middle' title='新个分数'>" + row["CurrentOneMark"].ToString() + "</td>";
            str += "<td align='right' valing='middle' title='新网分数'>" + row["CurrentTotalNetRecord"].ToString() + "</td>";
            str += "<td align='right' valing='middle' title='新网人数'>" + row["CurrentNewNetNum"].ToString() + "</td>";
            str += "<td align='right' valing='middle' title='总网人数'>" + row["TotalNetNum"].ToString() + "</td>";
            str += "<td align='right' valing='middle' title='总网分数'>" + row["TotalNetRecord"].ToString() + "</td>";

            str += "</tr>";
        }
        str += "</table>";

        return str;
    }

    [AjaxPro.AjaxMethod]
    public string WangLuoTuTj(string bianhao, string qs)
    {

        //获得存储过程产生的树
        int type = 0;//公司查看网络图
        int qishu = Convert.ToInt32(qs);
        string tree = "";
        int cengshu = 5;
        DataTable table = new TreeViewBLL().GetExtendTreeView_NewTj(bianhao, tree, cengshu, qishu, type, 0);
        string str = "";
        //循环拼出树
        str = "<table id='tab_tr' border=\"0\" cellspacing=\"0\" cellpadding=\"0\"  class='tree_grid'>";
        str += "<tr>"
               + "<th align='center'>会员编号</th>"
               + "<th align='center' >层数</th>"
               + "<th align='center'>级别</th>"
               + "<th align='right'>新个分数</th>"
               + "<th align='right'>新网分数</th>"
               + "<th align='right'>新网人数</th>"
               + "<th align='right' >总网人数</th>"
               + "<th align='right' >总网分数</th>"

               + "</tr>";
        int count = 0;
        foreach (DataRow row in table.Rows)
        {
            count++;

            string strStyle = "";
            if (count % 2 == 0)
            {
                strStyle = "background-color:#F1F4F8";
            }
            else
            {
                strStyle = " background-color:#FAFAFA";
            }
            str += "<tr style='" + strStyle + "' class=\"tr\" id='tr" + row["bh"].ToString() + "'  onmousedown=\"down_tw(event,this)\">";
            str += "<td valing='middle'>" + row["htmltree"].ToString().Replace("─", "<img src='../images/011.gif'  align=absmiddle  border=0 />").Replace("☆", "<img src='../images/013.gif'  align=absmiddle  border=0 />").Replace("★", "<img src='../images/014.gif'  align=absmiddle  border=0 />").Replace("├", "<img src='../images/006.gif'  align=absmiddle  border=0 />").Replace("└", "<img src='../images/003.gif'  align=absmiddle  border=0 />").Replace("~", "<img src='../images/015.gif'  align=absmiddle  border=0 />").Replace("|", "<img src='../images/004.gif'  align=absmiddle  border=0 />") + "<img src='../images/1.png' class='img' align=absmiddle border=0 /></td>";
            str += "<td align='center' valing='middle' title='层数'>" + row["cw"].ToString() + "</td>";
            str += "<td align='left' valing='middle' title='级别'>" + row["level"].ToString() + "</td>";
            str += "<td align='right' valing='middle' title='新个分数'>" + row["CurrentOneMark"].ToString() + "</td>";
            str += "<td align='right' valing='middle' title='新网分数'>" + row["DCurrentTotalNetRecord"].ToString() + "</td>";
            str += "<td align='right' valing='middle' title='新网人数'>" + row["DCurrentNewNetNum"].ToString() + "</td>";
            str += "<td align='right' valing='middle' title='总网人数'>" + row["DTotalNetNum"].ToString() + "</td>";
            str += "<td align='right' valing='middle' title='总网分数'>" + row["DTotalNetRecord"].ToString() + "</td>";

            str += "</tr>";
        }
        str += "</table>";
        return str;

    }

    [AjaxPro.AjaxMethod]
    public string WangLuoTu_str1(string number, int ExpectNum, string tree)
    {
        string[] state = new string[] { "level", "CurrentOneMark", "CurrentTotalNetRecord", "CurrentNewNetNum", "TotalNetNum", "TotalNetRecord" };
        string[] data = new string[8];
        string register_ExpectNum = "";
        string sql = "";
        string str = "";
        string shuju = "";

        DataTable dt = DAL.DBHelper.ExecuteDataTable("select  field,name  from  NetWorkDisplayStatus    where   flag=1");
        DataRow[] row;
        data[0] = DAL.DBHelper.ExecuteScalar("select  petname  from  memberInfo  where  number='" + number + "'").ToString();
        register_ExpectNum = DAL.DBHelper.ExecuteScalar("select  ExpectNum  from  memberInfo  where  number='" + number + "'").ToString();

        for (int i = 0; i < state.Length; i++)
        {
            row = dt.Select("field='" + state[i].ToString().Trim() + "'");
            if (row.Length > 0)
            {
                sql = "select " + state[i].ToString() + " from  MemberInfoBalance" + ExpectNum + "  where  number='" + number + "'";
                shuju = DAL.DBHelper.ExecuteScalar(sql).ToString();
                if (state[i].ToString().Trim() == "level")
                {
                    data[i + 1] = AjaxClass.GetLevel(shuju);
                }
                else
                {
                    //if (state[i].ToString().Trim() == "CurrentNewNetNum" || state[i].ToString().Trim() == "TotalNetNum")
                    //{
                    //    data[i + 1] = shuju;
                    //}
                    //else
                    //{
                    if (shuju != "0" && shuju != "0.00")
                    {
                        data[i + 1] = (Convert.ToDouble(shuju)).ToString();
                    }
                    else
                    {
                        data[i + 1] = "0";
                    }
                    //}
                }

                foreach (DataRow pp in row)
                {
                    if (state[i].ToString().Trim() == "level")
                    {
                        str += "<span  class=js title=" + pp["name"].ToString() + "><img height='18' src='" + data[i + 1] + "' /></span>";
                    }
                    else
                    {
                        str += "<span  class=js title=" + pp["name"].ToString() + ">[" + data[i + 1] + "]</span>";
                    }
                }
            }
        }

        int isActive = Convert.ToInt32(DAL.DBHelper.ExecuteScalar("select top 1 isnull(isactive,0) from memberinfo where number='" + number + "'"));
        string color = "";
        if (ExpectNum == Convert.ToInt32(register_ExpectNum))
        {
            if (isActive == 1)
            {
                color = "color:#E30000";
            }
            else
            {
                color = "color:##FF8686";
            }
            str = "<span class=ss id='" + number + "' tree=\"" + tree + "\"  ONCLICK=JSNET(this.id,this.attributes['tree'].value,1," + ExpectNum + ")>田<span class=bh id=n" + number + " onMouseUp=popUp(this.id," + ExpectNum + ",event) title='编号' style='" + color + "'>" + number + "</span>  <span class=xm title='姓名'>" + data[0] + "</span>" + str + "</br>";
            //str = "</span><span class=ls title='编号和姓名' style='" + color + "'>" + number + "</span>  <span class=xm>" + data[0] + "</span>" + str;
        }
        else
        {
            if (isActive == 1)
            {
                color = "color:black";
            }
            else
            {
                color = "color:Silver";
            }
            // str = "</span><span class=ls title='编号和姓名' style='" + color + "'>" + number + "</span>  <span class=xm>" + data[0] + "</span>" + str;
            str = "<span class=ss id='" + number + "' tree=\"" + tree + "\"  ONCLICK=JSNET(this.id,this.attributes['tree'].value,1," + ExpectNum + ")>田<span class=bh id=n" + number + " onMouseUp=popUp(this.id," + ExpectNum + ",event) title='编号' style='" + color + "'>" + number + "</span>  <span class=xm title='姓名'>" + data[0] + "</span>" + str + "</br>";
        }

        return str;
    }

    [AjaxPro.AjaxMethod]
    public string DeleteMember(string member)
    {
        return bll.DelMembersDeclarationTreeNet(member);
    }

    [AjaxPro.AjaxMethod]
    public string VerifyPaperNumber(string paperNumber)
    {
        return BLL.Registration_declarations.CheckMemberInfo.CHK_IdentityCard(paperNumber);
    }

    /// <summary>
    /// 获得该会员的奖金提现账户
    /// </summary>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string GetMemberBonus()
    {
        return ReleaseBLL.GetMemberBonus(Session["Member"].ToString()).ToString("F2");
    }
    /// <summary>
    /// 获得该会员的奖金报单账户
    /// </summary>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string GetMemberDeclarations()
    {
        return ReleaseBLL.GetMemberDeclarations(Session["Member"].ToString()).ToString("F2");
    }
    [AjaxPro.AjaxMethod]
    public int GetMaxQishu()
    {
        return BLL.CommonClass.CommonDataBLL.GetMaxqishu();
    }

    [AjaxPro.AjaxMethod]
    public string GetSjBh(string bianhao, bool isAz)
    {
        return DAL.CommonDataDAL.GetSjBh(bianhao, isAz);
    }

    [AjaxPro.AjaxMethod]
    public string GetRegisUp(string bianhao)
    {
        return DAL.CommonDataDAL.GetRegisUp(bianhao);
    }

    /// <summary>
    /// 获取菜单个数
    /// </summary>
    /// <param name="type">菜单类型</param>
    /// <param name="pid">菜单父菜单ID</param>
    /// <returns>返回菜单个数</returns>
    [AjaxPro.AjaxMethod]
    public string GetLeftMenuCount(int type)
    {
        return DAL.CommonDataDAL.GetLeftMenuCount(type).ToString();
    }

    /// <summary> 
    /// 获取菜单个数
    /// </summary>
    /// <param name="type">菜单类型</param>
    /// <param name="pid">菜单父菜单ID</param>
    /// <returns>返回菜单个数</returns>
    [AjaxPro.AjaxMethod]
    public string GetLeftMenu(int type)
    {
        return DAL.CommonDataDAL.GetLeftMenu(type).ToString();
    }

    [AjaxPro.AjaxMethod]
    public string GetTitleNameLeft(int pid)
    {
        return MenuDAL.GetTitleNameLeft(pid);
    }

    [AjaxPro.AjaxMethod]
    public string GetDisplay(string bianhao, string labField, int ExpectNum)
    {
        return DAL.CommonDataDAL.GetDisplay(bianhao, labField, ExpectNum);
    }

    [AjaxPro.AjaxMethod]
    public string GetDisplay1(string bianhao, string labField, int ExpectNum, int type, int isAnzhi)
    {
        return DAL.CommonDataDAL.GetDisplay1(bianhao, labField, ExpectNum, type, isAnzhi);
    }

    [AjaxPro.AjaxMethod]
    public int GetDiv(string divStr, string bianhao)
    {
        string newBh = bianhao;
        int count = divStr.IndexOf(newBh);
        return count;
    }

    /// <summary>
    /// 获取网络图快捷菜单显示内容
    /// </summary>
    [AjaxPro.AjaxMethod]
    public string GetDisplayBind()
    {
        DataTable dt = DAL.CommonDataDAL.GetDisplayBind();

        string displayText = "";
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i != 0)
                {
                    displayText += ",";
                }
                displayText += dt.Rows[i]["FieldName"].ToString();
            }
        }
        return displayText;
    }

    /// <summary>
    /// 获得级别
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public static string GetLevel(string level)
    {
        string chaBuMoney = "";
        //switch (level)
        //{
        //    case "0":
        //        chaBuMoney = "普通会员";
        //        break;
        //    case "1":
        //        chaBuMoney = "一星会员";
        //        break;
        //    case "2":
        //        chaBuMoney = "二星会员";
        //        break;
        //    case "3":
        //        chaBuMoney = "三星会员";
        //        break;
        //    case "4":
        //        chaBuMoney = "四星会员";
        //        break;
        //    default:
        //        chaBuMoney = "普通会员";
        //        break;

        //}

        chaBuMoney = DAL.CommonDataDAL.GetLevel(level, 0);
        return chaBuMoney;
    }
    public static string GetLevel1(string level)
    {
        string chaBuMoney = "";
        //switch (level)
        //{
        //    case "0":
        //        chaBuMoney = "普通会员";
        //        break;
        //    case "1":
        //        chaBuMoney = "一星会员";
        //        break;
        //    case "2":
        //        chaBuMoney = "二星会员";
        //        break;
        //    case "3":
        //        chaBuMoney = "三星会员";
        //        break;
        //    case "4":
        //        chaBuMoney = "四星会员";
        //        break;
        //    default:
        //        chaBuMoney = "普通会员";
        //        break;

        //}

        chaBuMoney = DAL.CommonDataDAL.GetStrLevel(Convert.ToInt32(level));
        return chaBuMoney;
    }


    ///<summary>
    ///随即生成编号
    ///</summary>
    ///<return>编号</return>
    [AjaxPro.AjaxMethod]
    public static string GetMemberNumber()
    {
        return BLL.CommonClass.CommonDataBLL.GetMemberNumber();
    }

    [AjaxPro.AjaxMethod]
    public string GetTuiJian(string number, string names)
    {

        string sql = "select count(*) from memberInfo where number=@number and name=@names";
        string info = "";
        try
        {
            SqlParameter[] para = {
                                    new SqlParameter("@number",SqlDbType.NVarChar,20),
                                    new SqlParameter("@names",SqlDbType.NVarChar,50)
                                   };
            para[0].Value = number;
            para[1].Value = Encryption.Encryption.GetEncryptionName(names);

            int count = Convert.ToInt32(DBHelper.ExecuteScalar(sql, para, CommandType.Text));
            if (count != 1)
            {
                info = this.GetTran("000701"); // "输入的推荐人编号或推荐人姓名不正确!";
            }
            else
            {
                info = this.GetTran("000700");//"验证通过";
            }
        }
        catch
        {
            info = this.GetTran("000701"); //"输入的推荐人编号或推荐人姓名不正确";
        }
        return info;
    }


    [AjaxPro.AjaxMethod]
    public string GetAnZhi(string number, string name)
    {
        string sql = "select  count(Name)  from memberInfo where number=@number and name=@names";
        string info = "";
        try
        {
            SqlParameter[] para = {
                                    new SqlParameter("@number",SqlDbType.NVarChar,20),
                                    new SqlParameter("@names",SqlDbType.NVarChar,50)
                                   };
            para[0].Value = number;
            para[1].Value = Encryption.Encryption.GetEncryptionName(name);

            int count = Convert.ToInt32(DBHelper.ExecuteScalar(sql, para, CommandType.Text));
            //int count = Convert.ToInt32(DBHelper.ExecuteScalar(sql));
            if (count != 1)
            {
                info = this.GetTran("000694");// "输入的安置编号或安置人姓名不正确!";
            }
            else
            {
                info = this.GetTran("000700");//"验证通过";
            }
        }
        catch
        {
            info = this.GetTran("000694");// "输入的安置编号或安置人姓名不正确";
        }
        return info;
    }

    [AjaxPro.AjaxMethod]
    public string GetAddressCode(string city)
    {
        return new RegistermemberBLL().GetAddressCode(city);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="pName"></param>
    /// <param name="pNumber"></param>
    /// <param name="storeId"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string GetProdcutTable(string pid, string pNumber, string storeId)
    {
        string[] Pids = pid.Split(',');
        string[] PNumbers = pNumber.Split(',');
        string table = "<table width=520 border=0 align=center cellpadding=0 cellspacing=0 ><tr><td>商品名</td><td>商品单价</td><td>商品积分</td><td>选择数量</td></tr>";
        //string Sql = "";
        for (int i = 0; i < Pids.Length; i++)
        {
            if (Pids[i].IndexOf("N") >= 0)
                if (Convert.ToInt32(PNumbers[i]) > 0)
                    //SqlDataReader reader=DBHelper.ExecuteReader(Sql,CommandType.Text);
                    //if (reader.Read) 
                    //{
                    table += "<tr><td>" + Pids[i] + "</td><td>" + PNumbers[i] + "</td><td></td><td>" + PNumbers[i] + "</td></tr>";
            //}
        }

        table += "</table>";

        return table;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="storID"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string setProductMenu(string storID)
    {
        ProductTree myTree = new ProductTree();
        string va = myTree.getEditMenu11(storID);


        return va;
    }

    [AjaxPro.AjaxMethod]
    public string GetStoreLeft(string storeId)
    {
        return this.GetTran("001544", "该店铺的可报单额为") + "：<font color=red>" + new RegistermemberBLL().GetLeftRegisterMemberMoney(storeId) + "</Font>";
    }

    /// <summary>
    /// 自由注册ajax树
    /// </summary>
    /// <param name="storID"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string setProductMenuOfFreeOrder(string storID)
    {
        MemberTree2 myTree = new MemberTree2();
        string va = myTree.getEditMenu11(storID);
        return va;

    }
    [AjaxPro.AjaxMethod]
    public bool GetIsExistsConfig(int ExpectNum)
    {
        string sql = "select count(*) from config where jsflag=0 and ExpectNum<@ExpectNum";
        SqlParameter[] parm = new SqlParameter[] { 
            new SqlParameter("@ExpectNum",SqlDbType.Int)
        };
        parm[0].Value = ExpectNum;
        object obj = DBHelper.ExecuteScalar(sql, parm, CommandType.Text);
        if (Convert.ToInt32(obj) > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    [AjaxPro.AjaxMethod]
    public bool CheckTuiJian(string tbh, string tuijian)
    {
        return new RegistermemberBLL().GetError3(tbh, tuijian);
    }
    [AjaxPro.AjaxMethod]
    public bool CheckTuiJian(string tbh)
    {
        bool bo = false;
        object count = DBHelper.ExecuteScalar("select count(0) from memberinfo where number='" + tbh + "' ");
        if (count != null)
        {
            if (Convert.ToInt32(count) > 0)
            {
                bo = true;
            }

        }
        return bo;

    }
    /// <summary>
    /// 检查会员编号是否存在
    /// </summary>
    /// <param name="tbh"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public bool CheckNumber(string number)
    {
        bool bo = false;
        object count = DBHelper.ExecuteScalar("select count(0) from memberinfo where number='" + number + "' ");
        if (count != null)
        {
            if (Convert.ToInt32(count) > 0)
            {
                bo = true;
            }

        }
        return bo;

    }

    [AjaxPro.AjaxMethod]
    public string OpenSmsContent(int id)
    {
        StringBuilder sb = new StringBuilder();

        string sql = "select * from smscontent where isfold=2 and pid=" + id;

        DataTable dt = DBHelper.ExecuteDataTable(sql);

        sb.Append("<table  border=0 cellpadding=0 cellspacing=1 style='width:800px' class=tablemb>");

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            int xh = i + 1;
            sb.Append("<tr>");
            sb.Append("<td style='width:700px'>" + xh + "、" + dt.Rows[i]["productName"].ToString() + "</td><td><a style=\"color:#075C79\" href=\"javascript:openAddWin('editItemSMS'," + dt.Rows[i]["ProductID"].ToString() + ")\">" + BLL.Translation.Translate("000259", "修改") + "</a>&nbsp;&nbsp;<a style=\"color:#075C79\" href=\"javascript:openAddWin('deleteItemSMS'," + dt.Rows[i]["ProductID"].ToString() + ")\">" + BLL.Translation.Translate("000022", "删除") + "</a></td>");
            sb.Append("</tr>");
        }

        sb.Append("</table>");

        return sb.ToString();
    }

    [AjaxPro.AjaxMethod]
    public string OpenSmsContentXZ(int id)
    {
        StringBuilder sb = new StringBuilder();

        string sql = "select * from smscontent where isfold=2 and pid=" + id;

        DataTable dt = DBHelper.ExecuteDataTable(sql);

        sb.Append("<table  border=0 cellpadding=0 cellspacing=1 style='width:800px' class=tablemb>");

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            int xh = i + 1;
            sb.Append("<tr>");
            sb.Append("<td style='width:700px'>" + xh + "、" + dt.Rows[i]["productName"].ToString() + "</td><td><a style=\"color:#075C79\" href=\"javascript:openXZ('SMS'," + dt.Rows[i]["ProductID"].ToString() + ")\">" + BLL.Translation.Translate("000018", "选择") + "</a></td>");
            sb.Append("</tr>");
        }

        sb.Append("</table>");

        return sb.ToString();
    }

    [AjaxPro.AjaxMethod]
    public string ReadSmsProductName(int id)
    {
        string productName = "";
        string sql = "select productName from smscontent where productid=" + id;

        productName = DBHelper.ExecuteScalar(sql).ToString();

        return productName;
    }

    [AjaxPro.AjaxMethod]
    public static string GetMemberOff(string bianhao, string manager, string offReason)
    {
        string content = "";
        string number = bianhao;
        int con = MemberOffBLL.getMember(number);
        if (con == 0)
        {
            return content = BLL.Translation.Translate("000599", "会员") + "" + number + "" + BLL.Translation.Translate("000801", "不存在，请重新输入") + "！";
        }         //----
        //string manageID = Session["Company"].ToString();
        int count = 0;
        string glnumber = BLL.CommonClass.CommonDataBLL.GetWLNumber(manager, out count);
        if (count == 0)
        {
            glnumber = "('')";
        }

        //-----         //判断登录者是否有更改此会员的权限
        bool isTrue = BLL.CommonClass.CommonDataBLL.GetRole(glnumber, number);
        if (!isTrue)
        {
            return content = "对不起，您没有更改此会员的权限！";

        } if (MemberOffBLL.getMemberZX(number) > 0)
        {
            int con1 = MemberOffBLL.getMemberISzx(number);
            if (con1 == 1)
            {
                return content = BLL.Translation.Translate("000599", "会员") + "" + number + "" + BLL.Translation.Translate("001310", "已经注销，不需要再次注销了") + "！";
            }
        } string zxname = BLL.Translation.Translate("001286", "已注销");
        zxname = Encryption.Encryption.GetEncryptionName(zxname);
        MemberOffModel mom = new MemberOffModel();
        mom.Number = number;
        mom.Zxqishu = CommonDataBLL.getMaxqishu();
        mom.Zxfate = DateTime.Now.ToUniversalTime();
        mom.OffReason = offReason;
        mom.OperatorNo = manager;
        mom.OperatorName = DataBackupBLL.GetNameByAdminID(manager); int insertCon = MemberOffBLL.getInsertMemberZX(mom, zxname);
        if (insertCon > 0)
        {
            return content = BLL.Translation.Translate("001312", "注销会员成功") + "！";
        }
        else
        {
            return content;
        }
    }

    /// <summary>
    /// 自由注册专用
    /// </summary>
    /// <param name="strPid"></param>
    /// <param name="storeID"></param>
    /// <param name="productids"></param>
    /// <param name="cla"></param>
    /// <param name="clr"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string DataBindTxt2(string strPid, string storeID, string productids, string cla, string clr)
    {


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
            string strSql = "Select * From Product Where IsSell=0";
            SqlDataReader dr = DBHelper.ExecuteReader(strSql);

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
            sb.Append("<table  border=0 cellpadding=0 cellspacing=1 style='width:426px' class=" + cla + ">");
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
                //					if(ar.Contains (productId_))
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
                    sb.Append("<td align=\"center\">" + Convert.ToDouble(dr["PreferentialPrice"]).ToString("0.00") + "</td>");
                    sb.Append("<td align=\"center\">" + iCount + "</td>");
                    sb.Append("<td align=\"center\">" + price.ToString("0.00") + "</td>");
                    sb.Append("<td align=\"center\">" + pv.ToString() + "</td>");
                    sb.Append("<td align=\"center\">" + iCurQishu.ToString() + "</td>");
                    sb.Append("<td align=\"center\">" + dateNow + "</td>");
                    sb.Append("</tr>");
                    n++;
                }
            }
            dr.Close();
            sb.Append("<tr bgColor=\"#F1F4F8\">");
            sb.Append("<td colspan=\"3\" align=\"center\" class=\"biaozzi\">总计</td>");
            sb.Append("<td align=\"center\" class=\"xhzi\">" + zongJine.ToString("0.00") + "</td>");
            sb.Append("<td align=\"center\" class=\"xhzi\">" + zongPv.ToString() + "</td>");
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


    [AjaxPro.AjaxMethod]
    public string UpBianhao(string bianhao)
    {
        return BLL.CommonClass.CommonDataBLL.UpBianhao(bianhao);
    }
    [AjaxPro.AjaxMethod]
    public string GetAtuosetPlace(string direct)
    {
        string res = "-1";
        SqlParameter[] paras = new SqlParameter[1];
        paras[0] = new SqlParameter("@direct", direct);

        DataTable dtt = DBHelper.ExecuteDataTable("seekEndAnzhi", paras, CommandType.StoredProcedure);

        if (dtt != null && dtt.Rows.Count > 0)
        {
            string number = dtt.Rows[0]["number"].ToString();
            int MemberState = Convert.ToInt32(dtt.Rows[0]["MemberState"]);
            string name = dtt.Rows[0]["name"].ToString();
            string levelstr = dtt.Rows[0]["levelstr"].ToString();
            if (MemberState == 1)
            {
                res = number; //+ "," + name + " " + levelstr;
            }
            else res = "0";
        }
        else
        {
            res = "-1";
        }

        return res;
    }
    [AjaxPro.AjaxMethod]
    public string GetLeftOrRight(string topNum, int type)
    {
        int qs = CommonDataBLL.GetMaxqishu();
        string num = "";
        SqlParameter[] paras = new SqlParameter[4];
        paras[0] = new SqlParameter("@Number", topNum);
        paras[1] = new SqlParameter("@Type", type);
        paras[2] = new SqlParameter("@qishu", qs);
        paras[3] = new SqlParameter("@AnZhiBySeek", SqlDbType.NVarChar, 50);
        paras[3].Direction = ParameterDirection.Output;

        DBHelper.ExecuteReader("SeekAnZhi", paras, CommandType.StoredProcedure);

        num = paras[3].Value.ToString();

        return num;
    }

    [AjaxPro.AjaxMethod]
    public string LineDrawing(string num, string allNums, string lineHtml)
    {
        DataTable dtNumber = DBHelper.ExecuteDataTable("select petName,placement from memberinfo where number=@num", new SqlParameter[1] { new SqlParameter("@num", num) }, CommandType.Text);
        int numIndex = allNums.IndexOf(dtNumber.Rows[0]["placement"].ToString());
        string newNums = "";
        if (numIndex >= 0)
        {
            newNums = allNums.Substring(0, numIndex + dtNumber.Rows[0]["placement"].ToString().Length) + "," + num;
        }
        else
        {
            newNums = num;
        }
        string[] nums = newNums.Split(new char[1] { ',' });

        lineHtml = "链路图";
        for (int i = 0; i < nums.Length; i++)
        {
            string petName = DBHelper.ExecuteScalar("select petName from memberinfo where number=@num", new SqlParameter[1] { new SqlParameter("@num", nums[i]) }, CommandType.Text).ToString();
            if (i % 3 == 0 && i / 3 > 0)
            {
                lineHtml += "<br>";
            }
            lineHtml += "→<a href='javascript:void(0);' onclick=\"ClickTable('" + nums[i] + "');\">" + nums[i] + " &nbsp; " + petName + "</a>";
        }

        lineHtml += "|" + newNums;

        return lineHtml;
    }

    [AjaxPro.AjaxMethod]
    public string LineDrawingForDouble(string num, string allNums, string lineHtml)
    {
        DataTable dtNumber = DBHelper.ExecuteDataTable("select petName,placement from memberinfo where number=@num", new SqlParameter[1] { new SqlParameter("@num", num) }, CommandType.Text);
        int numIndex = allNums.IndexOf(dtNumber.Rows[0]["placement"].ToString());
        string newNums = "";
        if (numIndex >= 0)
        {
            newNums = allNums.Substring(0, numIndex + dtNumber.Rows[0]["placement"].ToString().Length) + "," + num;
        }
        else
        {
            DataTable dtNumberUp = DBHelper.ExecuteDataTable("select petName,placement from memberinfo where number=@num", new SqlParameter[1] { new SqlParameter("@num", dtNumber.Rows[0]["placement"].ToString()) }, CommandType.Text);
            if (dtNumberUp.Rows.Count == 0)
            {
                newNums = num;
            }
            else
            {
                numIndex = allNums.IndexOf(dtNumberUp.Rows[0]["placement"].ToString());
                if (numIndex >= 0)
                {
                    newNums = allNums.Substring(0, numIndex + dtNumberUp.Rows[0]["placement"].ToString().Length) + "," + dtNumber.Rows[0]["placement"].ToString() + "," + num;
                }
                else
                {
                    newNums = num;
                }
            }
        }
        string[] nums = newNums.Split(new char[1] { ',' });

        lineHtml = (GetTran("007032", "链路图"));
        for (int i = 0; i < nums.Length; i++)
        {
            string petName = DBHelper.ExecuteScalar("select petName from memberinfo where number=@num", new SqlParameter[1] { new SqlParameter("@num", nums[i]) }, CommandType.Text).ToString();
            if (i % 5 == 0 && i / 5 > 0)
            {
                lineHtml += "<br>";
            }
            lineHtml += "→<a href='javascript:void(0);' onclick=\"ClickTable('" + nums[i] + "');\">" + nums[i] + " &nbsp; " + petName + "</a>";
        }

        lineHtml += "|" + newNums;

        return lineHtml;
    }

    /// <summary>
    /// 寻找安置人 -- 双线
    /// </summary>
    /// <param name="topBianhao">顶点编号</param>
    /// <param name="floorCount">层数</param> 
    /// <returns>网络图HTML</returns>
    [AjaxPro.AjaxMethod]
    public string SearchPlacement_DoubleLines(string topBianhao, int floorCount)
    {
        string resultHtml = "";
        string chageLine = " onmouseover='ChangeLine(\"" + topBianhao + "\");' ";

        DataTable topInfo = DBHelper.ExecuteDataTable("select BSCO_level.levelint,BSCO_level.levelstr,petName from memberinfo , BSCO_level where memberinfo.levelInt=BSCO_level.levelInt  and number=@num and memberstate=1 ", new SqlParameter[1] { new SqlParameter("@num", topBianhao) }, CommandType.Text);
        //链路图 --- 添加第一层的时候添加链路图
        if (floorCount == 0)
        {
            resultHtml += "<table id='tbLine' width='600' border='0' class='aa'>";
            resultHtml += "<tr>";
            resultHtml += "<td id='tdLine'>" + (GetTran("007032", "链路图")) + "→<a href='javascript:void(0);' onclick=\"ClickTable('" + topBianhao + "');\">" + topBianhao + " &nbsp; " + topInfo.Rows[0]["petName"].ToString() + "</a></td>";
            resultHtml += "</tr>";
            resultHtml += "</table>";

            floorCount++;
        }
        string jb = topInfo.Rows[0]["levelstr"].ToString();
        //一级
        resultHtml += "<div id='divDrawing' class='main'>";//总DIV
        resultHtml += "<div class='box'>";
        resultHtml += "<table class='tb' " + chageLine + " >";
        resultHtml += "<tr>";
        resultHtml += "<td colspan='2' onmousemove='ChangeColor(1,this);' onmouseout='ChangeColor(0,this);'>" + (GetTran("001195", "编号")) + "：" + topBianhao + "</td>";
        resultHtml += "</tr>";
        resultHtml += "<tr>";
        resultHtml += "<td colspan='2' onmousemove='ChangeColor(1,this);' onmouseout='ChangeColor(0,this);'>" + (GetTran("001400", "昵称")) + "：" + topInfo.Rows[0]["petName"].ToString() + "</td>";
        resultHtml += "</tr>";
        resultHtml += "<tr>";
        resultHtml += "<td colspan='2' onmousemove='ChangeColor(1,this);' onmouseout='ChangeColor(0,this);'> " + jb + "</td>";
        resultHtml += "</tr>";
        resultHtml += "<tr>";
        resultHtml += "<td style='width:50%' onmousemove='ChangeColor(1,this);' onmouseout='ChangeColor(0,this);'  onclick='ChangeLeftOrRight(\"" + topBianhao + "\",1,1);' >" + (GetTran("8114", "极左")) + "</td><td onmousemove='ChangeColor(1,this);' onmouseout='ChangeColor(0,this);' onclick='ChangeLeftOrRight(\"" + topBianhao + "\",2,1);' >" + (GetTran("8115", "极右")) + "</td>";
        resultHtml += "</tr>";
        resultHtml += "</table>";
        resultHtml += "</div>";
        //一级链接二级图片
        resultHtml += "<div class='lines'><img src='../images/line2.gif' /></div>";

        //二级    
        DataTable dtFloorTwo = DAL.DBHelper.ExecuteDataTable("select top 2 District,memberinfo.Number,BSCO_level.levelint,BSCO_level.levelstr,petName from memberinfo , BSCO_level where memberinfo.levelInt=BSCO_level.levelInt and levelflag=0   and memberstate=1  and placement=@num order by memberinfo.District asc ", new SqlParameter[1] { new SqlParameter("@num", topBianhao) }, CommandType.Text);
        resultHtml += "<div class='main2'>";
        resultHtml += Jiegou.CreateTableInDiv(dtFloorTwo, 2, topBianhao);
        resultHtml += "</div>";

        //三级
        if (dtFloorTwo.Rows.Count > 0)
        {
            DataTable dtFloorThreeLeft = DAL.DBHelper.ExecuteDataTable("select top 2 District,memberinfo.Number,BSCO_level.levelint,BSCO_level.levelstr,petName from memberinfo , BSCO_level where memberinfo.levelInt=BSCO_level.levelInt and levelflag=0   and memberstate=1  and placement=@num order by memberinfo.District asc ", new SqlParameter[1] { new SqlParameter("@num", dtFloorTwo.Rows[0]["Number"].ToString()) }, CommandType.Text);

            if (dtFloorTwo.Rows[0]["District"].ToString() == "2")
            {
                resultHtml += "<div class='main3 left'>";
                resultHtml += "<div class='lines2'><img src='../images/line1.gif' /></div>";
                resultHtml += "&nbsp;";
                resultHtml += "</div>";

                resultHtml += "<div class='main4 left'>";
                resultHtml += "<div class='lines2'><img src='../images/line1.gif' /></div>";

                resultHtml += Jiegou.CreateTableInDiv(dtFloorThreeLeft, 3, dtFloorTwo.Rows[0]["Number"].ToString());

                resultHtml += "</div>";
            }
            else
            {
                resultHtml += "<div class='main3 left'>";
                resultHtml += "<div class='lines2'><img src='../images/line1.gif' /></div>";

                resultHtml += Jiegou.CreateTableInDiv(dtFloorThreeLeft, 3, dtFloorTwo.Rows[0]["Number"].ToString());

                resultHtml += "</div>";
            }
        }
        if (dtFloorTwo.Rows.Count > 1)
        {
            DataTable dtFloorThreeRight = DAL.DBHelper.ExecuteDataTable("select top 2 District,memberinfo.Number,BSCO_level.levelint,BSCO_level.levelstr,petName from memberinfo , BSCO_level where memberinfo.levelInt=BSCO_level.levelInt and levelflag=0  and memberstate=1   and placement=@num order by memberinfo.District asc ", new SqlParameter[1] { new SqlParameter("@num", dtFloorTwo.Rows[1]["Number"].ToString()) }, CommandType.Text);
            resultHtml += "<div class='main4 left'>";
            resultHtml += "<div class='lines2'><img src='../images/line1.gif' /></div>";

            resultHtml += Jiegou.CreateTableInDiv(dtFloorThreeRight, 3, dtFloorTwo.Rows[1]["Number"].ToString());

            resultHtml += "</div>";
        }
        resultHtml += "</div>";

        return resultHtml;
    }

    /// <summary>
    /// 寻找安置人 -- 双线
    /// </summary>
    /// <param name="topBianhao">顶点编号</param>
    /// <param name="floorCount">层数</param> 
    /// <returns>网络图HTML</returns>
    [AjaxPro.AjaxMethod]
    public string SearchPlacement_DoubleLines2(string topBianhao, int floorCount)
    {
        string resultHtml = "";
        string chageLine = " onmouseover='ChangeLine(\"" + topBianhao + "\");' ";

        DataTable topInfo = DBHelper.ExecuteDataTable("select BSCO_level.levelint,BSCO_level.levelstr,petName from memberinfo , BSCO_level where memberinfo.levelInt=BSCO_level.levelInt  and number=@num", new SqlParameter[1] { new SqlParameter("@num", topBianhao) }, CommandType.Text);
        //链路图 --- 添加第一层的时候添加链路图
        if (floorCount == 0)
        {
            resultHtml += "<table id='tbLine' width='600' border='0' class='aa'>";
            resultHtml += "<tr>";
            resultHtml += "<td id='tdLine'>" + (GetTran("007032", "链路图")) + "→<a href='javascript:void(0);' onclick=\"ClickTable('" + topBianhao + "');\">" + topBianhao + " &nbsp; " + topInfo.Rows[0]["petName"].ToString() + "</a></td>";
            resultHtml += "</tr>";
            resultHtml += "</table>";

            floorCount++;
        }
        string jb = topInfo.Rows[0]["levelint"].ToString();
        //一级
        resultHtml += "<div id='divDrawing' class='main'>";//总DIV
        resultHtml += "<div class='box'>";
        resultHtml += "<table class='tb' " + chageLine + " >";
        resultHtml += "<tr>";
        resultHtml += "<td colspan='2' onmousemove='ChangeColor(1,this);' onmouseout='ChangeColor(0,this);'>" + (GetTran("001195", "编号")) + "：" + topBianhao + "</td>";
        resultHtml += "</tr>";
        resultHtml += "<tr>";
        resultHtml += "<td colspan='2' onmousemove='ChangeColor(1,this);' onmouseout='ChangeColor(0,this);'>" + (GetTran("001400", "昵称")) + "：" + topInfo.Rows[0]["petName"].ToString() + "</td>";
        resultHtml += "</tr>";
        resultHtml += "<tr>";
        resultHtml += "<td colspan='2' onmousemove='ChangeColor(1,this);' onmouseout='ChangeColor(0,this);'>" + (GetTran("006906", "级别")) + "：" + (jb == "0" ? GetTran("006899", "世联会员") : jb == "1" ? GetTran("004255", "世联普商") : jb == "2" ? GetTran("004258", "世联咨商") : jb == "3" ? GetTran("005219", "世联特别咨商") : jb == "4" ? GetTran("005222", "世联高级咨商") : GetTran("005224", "世联全面咨商")) + "</td>";
        resultHtml += "</tr>";
        resultHtml += "<tr>";
        resultHtml += "<td style='width:50%' onmousemove='ChangeColor(1,this);' onmouseout='ChangeColor(0,this);'  onclick='ChangeLeftOrRight(\"" + topBianhao + "\",1,1);' >" + (GetTran("008114", "极左")) + "</td><td onmousemove='ChangeColor(1,this);' onmouseout='ChangeColor(0,this);' onclick='ChangeLeftOrRight(\"" + topBianhao + "\",2,1);' >" + (GetTran("008115", "极右")) + "</td>";
        resultHtml += "</tr>";
        resultHtml += "</table>";
        resultHtml += "</div>";
        //一级链接二级图片
        resultHtml += "<div class='lines'><img src='images/line2.gif' /></div>";

        //二级    
        DataTable dtFloorTwo = DAL.DBHelper.ExecuteDataTable("select top 2 District,memberinfo.Number,BSCO_level.levelint,BSCO_level.levelstr,petName from memberinfo , BSCO_level where memberinfo.levelInt=BSCO_level.levelInt and levelflag=0  and placement=@num order by memberinfo.District asc ", new SqlParameter[1] { new SqlParameter("@num", topBianhao) }, CommandType.Text);
        resultHtml += "<div class='main2'>";
        resultHtml += Jiegou.CreateTableInDiv(dtFloorTwo, 2, topBianhao);
        resultHtml += "</div>";

        //三级
        if (dtFloorTwo.Rows.Count > 0)
        {
            DataTable dtFloorThreeLeft = DAL.DBHelper.ExecuteDataTable("select top 2 District,memberinfo.Number,BSCO_level.levelint,BSCO_level.levelstr,petName from memberinfo , BSCO_level where memberinfo.levelInt=BSCO_level.levelInt and levelflag=0  and placement=@num order by memberinfo.District asc ", new SqlParameter[1] { new SqlParameter("@num", dtFloorTwo.Rows[0]["Number"].ToString()) }, CommandType.Text);

            if (dtFloorTwo.Rows[0]["District"].ToString() == "2")
            {
                resultHtml += "<div class='main3 left'>";
                resultHtml += "<div class='lines2'><img src='images/line1.gif' /></div>";
                resultHtml += "&nbsp;";
                resultHtml += "</div>";

                resultHtml += "<div class='main4 left'>";
                resultHtml += "<div class='lines2'><img src='images/line1.gif' /></div>";

                resultHtml += Jiegou.CreateTableInDiv(dtFloorThreeLeft, 3, dtFloorTwo.Rows[0]["Number"].ToString());

                resultHtml += "</div>";
            }
            else
            {
                resultHtml += "<div class='main3 left'>";
                resultHtml += "<div class='lines2'><img src='images/line1.gif' /></div>";

                resultHtml += Jiegou.CreateTableInDiv(dtFloorThreeLeft, 3, dtFloorTwo.Rows[0]["Number"].ToString());

                resultHtml += "</div>";
            }
        }
        if (dtFloorTwo.Rows.Count > 1)
        {
            DataTable dtFloorThreeRight = DAL.DBHelper.ExecuteDataTable("select top 2 District,memberinfo.Number,BSCO_level.levelint,BSCO_level.levelstr,petName from memberinfo , BSCO_level where memberinfo.levelInt=BSCO_level.levelInt and levelflag=0  and placement=@num order by memberinfo.District asc ", new SqlParameter[1] { new SqlParameter("@num", dtFloorTwo.Rows[1]["Number"].ToString()) }, CommandType.Text);
            resultHtml += "<div class='main4 left'>";
            resultHtml += "<div class='lines2'><img src='images/line1.gif' /></div>";

            resultHtml += Jiegou.CreateTableInDiv(dtFloorThreeRight, 3, dtFloorTwo.Rows[1]["Number"].ToString());

            resultHtml += "</div>";
        }
        resultHtml += "</div>";

        return resultHtml;
    }

    /// <summary>
    /// 寻找安置人 -- 三线
    /// </summary>
    /// <param name="topBianhao">顶点编号</param>
    /// <param name="floorCount">层数</param>
    /// <returns>网络图HTML</returns>
    [AjaxPro.AjaxMethod]
    public string SearchPlacement_ThreeLines(string topBianhao, int floorCount)
    {
        DataTable dtFloorOne = MemberInfoDAL.GetMemberInfoByPlacement(topBianhao);
        string topPetName = DBHelper.ExecuteScalar("select petName from memberinfo where number=@num", new SqlParameter[1] { new SqlParameter("@num", topBianhao) }, CommandType.Text).ToString();
        string resultHtml = "";
        //链路图 --- 添加第一层的时候添加链路图
        if (floorCount == 0)
        {
            resultHtml += "<table id='tbLine' width='600' border='0' class='aa'>";
            resultHtml += "<tr>";
            resultHtml += "<td id='tdLine'>" + (GetTran("007032", "链路图")) + "→<a href='javascript:void(0);' onclick=\"ClickTable('" + topBianhao + "');\">" + topBianhao + " &nbsp; " + topPetName + "</a></td>";
            resultHtml += "</tr>";
            resultHtml += "</table>";

            floorCount++;
        }

        //网络图
        resultHtml += "<table id='tbFloor" + floorCount + "' width='100%' border='0' cellpadding='0' cellspacing='0' class='styletable' style='margin-top:2px;'>";
        resultHtml += "<tr id='trFloor" + floorCount + "'>";

        int count = 1, count2 = 1;
        foreach (DataRow row in dtFloorOne.Rows)
        {
            if (count < 4)
            {
                if (count == Convert.ToInt32(row["District"]))
                {
                    string onMouseOver = " onmouseover=\"MouseOnTable(this," + floorCount + ",'" + row["Number"].ToString() + "');\" ";
                    string onClick = " onclick=\" ClickTable(\'" + row["Number"].ToString() + "\',1);\" ";

                    resultHtml += "<td ><table width='100%' border='0' cellpadding='0' cellspacing='0' class='td" + floorCount + "'  " + onMouseOver + onClick + "  >";
                    resultHtml += "<tr>";
                    resultHtml += "<td>" + row["Number"].ToString() + "</td>";
                    resultHtml += "</tr>";
                    resultHtml += "<tr>";
                    resultHtml += "<td>" + row["PetName"].ToString() + "</td>";
                    resultHtml += "</tr>";
                    resultHtml += "</table></td>";

                    count2++;
                }
                else
                {
                    resultHtml += "<td><table width='100%' border='0' cellpadding='0' cellspacing='0' class='td" + floorCount + "_1' onmouseover='DisplayNextTable(this," + floorCount + ");' >";
                    resultHtml += "<tr>";
                    resultHtml += "<td><a href='javascript:void(0);' onclick=\"ChoosePlacement('" + topBianhao + "'," + count + ");\" style='font-size:14px; font-weight:bold;'>选择位置</a></td>";
                    resultHtml += "</tr>";
                    resultHtml += "<tr>";
                    resultHtml += "<td style='border-bottom:solid 1px #fed2c7;'>&nbsp;</td>";
                    resultHtml += "</tr>";
                    resultHtml += "</table></td>";
                }
            }
            count++;
        }
        for (int i = 3 - dtFloorOne.Rows.Count; i > 0; i--)
        {
            resultHtml += "<td><table width='100%' border='0' cellpadding='0' cellspacing='0' class='td" + floorCount + "_1' onmouseover='DisplayNextTable(this," + floorCount + ");' >";
            resultHtml += "<tr>";
            resultHtml += "<td><a href='javascript:void(0);' onclick=\"ChoosePlacement('" + topBianhao + "'," + count2 + ");\" style='font-size:14px; font-weight:bold;'>选择位置</a></td>";
            resultHtml += "</tr>";
            resultHtml += "<tr>";
            resultHtml += "<td style='border-bottom:solid 1px #fed2c7;'>&nbsp;</td>";
            resultHtml += "</tr>";
            resultHtml += "</table></td>";
        }

        resultHtml += "</tr>";
        resultHtml += "</table>";

        return resultHtml;
    }

    /// <summary>
    /// 常用安置网络图
    /// </summary>
    /// <param name="Div_AnZhi">ＤＩＶ</param>
    /// <param name="TopBianhao">首点编号</param>
    /// <param name="qishu">期数</param>
    /// <returns>返回Table形式的字符串</returns>
    [AjaxPro.AjaxMethod]
    public string WangLuoTu_AnZhi(string TopBianhao)
    {
        string strhtml1 = "";
        try
        {
            string qishu = BLL.CommonClass.CommonDataBLL.GetMaxqishu().ToString();

            DataTable dt = BLL.other.Store.StoreInfoModifyBLL.GetMemberInfo(qishu);

            DataTable dt_anzhi;
            DataTable dt_anzhi1;
            DataRow[] row;
            int div_count = 0;
            int count = 0;
            int i = 0;
            int p = 0;
            int k = 0;
            int jb = 0;

            string jibie = "";
            string colors = "";
            string sql = "";
            string anzhi = "";

            if (TopBianhao == "")
            {
                anzhi = BLL.CommonClass.CommonDataBLL.getManageID(3);
            }
            else
                anzhi = TopBianhao;


            dt_anzhi = BLL.other.Store.StoreInfoModifyBLL.GetMemberPlacement(anzhi, qishu);// DBHelper.ExecuteDataTable(sql);
            count = dt_anzhi.Rows.Count;

            div_count += count;
            for (i = 0; i < dt_anzhi.Rows.Count; i++)
            {
                dt_anzhi1 = BLL.other.Store.StoreInfoModifyBLL.GetMemberPlacement2(dt_anzhi.Rows[i]["number"].ToString(), qishu); //DBHelper.ExecuteDataTable("select *  from  memberInfo  where placeMent='" + dt_anzhi.Rows[i]["number"] + "' and  expectnum<='" + qishu + "' and  placement<>''");
                div_count += dt_anzhi1.Rows.Count;
            }

            string[] bianhaohtml = new string[div_count + 2]; ;
            string[] jibiehtml = new string[div_count + 2];
            string[] color = new string[div_count + 2];
            string[] img = new string[div_count + 2];
            for (p = 0; p < div_count + 1; p++)
            {
                bianhaohtml[p] = "&nbsp;";
                jibiehtml[p] = "&nbsp";
                color[p] = "#b3b3b3";
                img[p] = "images/mept.gif";
            }
            #region 一层
            row = dt.Select("number='" + TopBianhao + "'", "number");
            if (row.Length > 0)
            {
                if (row[0]["number"].ToString().Trim() == "")
                {
                    jibie = "";
                }
                else
                {
                    jb = int.Parse(row[0]["level"].ToString());
                    //  jibie = Business.GetLevelForEng(jb.ToString());
                }

                colors = "#00cc00";
                if (row[0]["expectnum"].ToString() == BLL.CommonClass.CommonDataBLL.GetMaxqishu().ToString())
                {
                    colors = "red";
                }

                bianhaohtml[0] = "<a href=\"#\" onclick=\"UpNet(" + row[0]["number"].ToString() + ")\"><font color=#333333>" + row[0]["number"].ToString() + "</font></a>";
                jibiehtml[0] = jibie;
                color[0] = colors;
                img[0] = Jiegou.GetLevel1(jb.ToString(), 0);
            }
            #endregion
            #region 二层
            for (i = 0; i < dt_anzhi.Rows.Count + 1; i++)
            {
                if (i <= (dt_anzhi.Rows.Count - 1))
                {
                    row = dt.Select("number='" + dt_anzhi.Rows[i]["number"] + "'", "number");
                    string asgsa = dt_anzhi.Rows[i]["number"].ToString();
                    if (row.Length > 0)
                    {
                        if (row[0]["number"].ToString().Trim() == "")
                        {
                            jibie = "";
                        }
                        else
                        {
                            jb = int.Parse(row[0]["level"].ToString());
                            // jibie = Business.GetLevelForEng(jb.ToString());
                        }

                        colors = "#00cc00";
                        if (row[0]["expectnum"].ToString() == BLL.CommonClass.CommonDataBLL.GetMaxqishu().ToString())
                        {
                            colors = "red";
                        }

                        bianhaohtml[i + 1] = "<a  href=\"#\" onclick=\"DownNet(" + row[0]["number"].ToString() + ")\"  ><font color=#333333>" + row[0]["number"].ToString() + "</font></a>";
                        string asgd = bianhaohtml[i + 1].ToString();
                        jibiehtml[i + 1] = jibie;
                        color[i + 1] = colors;
                        img[i + 1] = Jiegou.GetLevel1(jb.ToString(), 0);
                    }
                }
                else
                {

                    colors = "#00cc00";

                    bianhaohtml[i + 1] = "<a href=\"#\" onclick=\"GetAnZhiNumber(" + anzhi.ToString() + ")\"><img src=\"images/xzuo.gif\" border=\"0\"></img></a>";
                    color[i + 1] = colors;
                    img[i + 1] = "images/ren04.gif";
                }
            }
            #endregion


            int _count1 = dt_anzhi.Rows.Count + 1;//第二层线的个数(上线推荐的个数)
            int _count2 = 0;
            int width1 = 0;//第二层线的宽度
            int _count3 = 0;
            int width3 = 0;
            int width4 = 0;
            int _count4 = 0;
            for (i = 0; i < _count1; i++)
            {
                if (i <= (dt_anzhi.Rows.Count - 1))
                {
                    if (_count1 > 1)
                    {
                        dt_anzhi1 = BLL.other.Store.StoreInfoModifyBLL.GetMemberPlacement(dt_anzhi.Rows[i]["number"].ToString(), qishu);// DBHelper.ExecuteDataTable("select *  from  memberinfo  where  placement='" + dt_anzhi.Rows[i]["number"] + "' and  expectnum<=" + qishu);
                        _count4 = dt_anzhi1.Rows.Count;

                        _count2++;
                        _count3++;
                    }
                    else
                        _count2 = _count1;
                }
                else
                {
                    _count2++;
                    _count3++;
                }
                if (i == 0)
                {
                    //width3第二层线头部的宽度
                    width3 = 50;
                }
                if (i == (_count1 - 1))
                {
                    width4 = 50;
                }
            }
            width1 = _count2 * 100 - width3 - width4;//第二层线中间部分的宽度

            strhtml1 = "<table  border=\"0\" cellspacing=\"0\" cellpadding=\"0\"> " +
                "<tr align=\"center\" valign=\"top\">" +
                "<td height=\"63\" colspan=\"" + _count1 + "\" align=\"center\">" +
                "<table width=\"100\" height=\"63\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">" +
                "<tr>" +
                "<td height=\"47\" colspan=\"3\" valign=\"top\">" +
                "<TABLE  borderColor=#CCCCCC cellSpacing=0  borderColorDark=#ffffff cellPadding=0 height=100%  width=100>" +

                "<TR>" +
                "<TD align=\"center\" colSpan=\"2\" height=\"20\"><img src=" + img[0] + "></img></TD>" +
                "</TR>" +

                "<TR>" +
                "<TD align=\"center\" colSpan=\"2\" height=\"20\">" + bianhaohtml[0] + "</TD>" +
                "</TR>" +
                "</TABLE>" +
                "</td>" +
                "</tr>";
            if (_count1 > 0)
            {
                strhtml1 += "<tr>" +
                    "<td width=\"49\" height=\"16\"></td>" +
                    "<td width=\"1\" background=\"images/images_02.gif\"  > </td>" +
                    "<td width=\"49\"></td>" +
                    "</tr>";

            }
            strhtml1 += "</table>" +
                "</td>" +
                "</tr>";
            if (_count1 > 0)
            {
                if (_count2 > 1)
                {
                    strhtml1 += "<tr align=\"center\" valign=\"top\">" +
                        "<td colspan=\"" + _count1 + "\" height=\"2\" >" +
                        "<table  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" +
                        "<tr>" +
                        "<td height=\"1\" width=\"" + width3 + "\" ></td>" +
                        "<td height=\"1\" width=\"" + width1 + "\"  background=\"images/images03_05.gif\"></td>" +
                        "<td height=\"1\" width=\"" + width4 + "\"  ></td>" +
                        "</tr>" +
                        "</table>" +
                        "</td>" +
                        "</tr>";
                }
                else
                {
                    strhtml1 += "<tr align=\"center\" valign=\"top\">" +
                        "<td colspan=\"" + _count1 + "\" height=\"2\" align=\"center\">" +
                        "<table  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" +
                        "<tr>" +
                        "<td height=\"1\" width=\"2\"  background=\"images/images03_05.gif\"></td>" +
                        "</tr>" +
                        "</table>" +
                        "</td>" +
                        "</tr>";
                }
            }

            strhtml1 += "<tr valign=\"top\">";

            int t = 0;
            int _count = 0;
            for (i = 0; i < dt_anzhi.Rows.Count + 1; i++)
            {
                if (i <= (dt_anzhi.Rows.Count - 1))
                {
                    strhtml1 += "<td  align=\"center\">" +
                        "<table  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" +
                        "<tr align=\"center\" valign=\"top\">" +
                        "<td height=\"79\" colspan=\"3\">" +
                        "<table  height=\"79\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">" +
                        "<tr>" +
                        "<td width=\"49\" height=\"16\"></td>" +
                        "<td width=\"1\"  background=\"images/images_02.gif\"></td>" +
                        "<td width=\"49\"></td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td height=\"47\" colspan=\"3\" valign=\"top\">" +
                        "<TABLE  borderColor=#CCCCCC cellSpacing=0  borderColorDark=#ffffff cellPadding=0 height=100%  width=100>" +

                        "<TR>" +
                        "<TD align=\"center\"  colSpan=\"2\" height=\"20\"><img src=" + img[i + 1] + "></img></TD>" +
                        "</TR>" +

                        "<TR>" +
                        "<TD align=\"center\" colSpan=\"2\" height=\"20\">" + bianhaohtml[i + 1] + "</TD>" +
                        "</TR>" +
                        "</TABLE>" +
                        "</td>" +
                        "</tr>";

                    strhtml1 += "</table>" +
                        "</td>" +
                        "</tr>";
                }
                else
                {
                    if (i == dt_anzhi.Rows.Count)
                    {
                        strhtml1 += "<td  align=\"center\">" +
                            "<table  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" +
                            "<tr align=\"center\" valign=\"top\">" +
                            "<td height=\"79\" colspan=\"3\">" +
                            "<table  height=\"79\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">" +
                            "<tr>" +
                            "<td width=\"49\" height=\"16\"></td>" +
                            "<td width=\"1\"  background=\"images/images_02.gif\"></td>" +
                            "<td width=\"49\"></td>" +
                            "</tr>" +
                            "<tr>" +
                            "<td height=\"47\" colspan=\"3\" valign=\"top\">" +
                            "<TABLE  borderColor=#CCCCCC cellSpacing=0  borderColorDark=#ffffff cellPadding=0 height=100%  width=100>" +


                            "<TR>" +
                            "<TD   rowspan=\"2\"  valign=\"middle\"  align=\"center\" colSpan=\"2\" height=\"20\">" + bianhaohtml[i + 1] + "</TD>" +
                            "</TR>" +
                            "</TABLE>" +
                            "</td>" +
                            "</tr>";

                        strhtml1 += "</table>" +
                            "</td>" +
                            "</tr>";
                    }
                    else
                    {
                        strhtml1 += "<td  align=\"center\">" +
                            "<table  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" +
                            "<tr align=\"center\" valign=\"top\">" +
                            "<td height=\"79\" colspan=\"3\">" +
                            "<table  height=\"79\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">" +
                            "<tr>" +
                            "<td width=\"49\" height=\"16\"></td>" +
                            "<td width=\"1\"  background=\"images/images_02.gif\"></td>" +
                            "<td width=\"49\"></td>" +
                            "</tr>" +
                            "<tr>" +
                            "<td height=\"47\" colspan=\"3\" valign=\"top\">" +
                            "<TABLE  borderColor=#CCCCCC cellSpacing=0  borderColorDark=#ffffff cellPadding=0 height=100%  width=100>" +

                            "<TR >" +
                            "<TD  rowspan=\"2\"  valign=\"middle\" align=\"center\" colSpan=\"2\" height=\"20\"><img src=" + img[i + 1] + "></img></TD>" +
                            "</TR>" +


                            "</TABLE>" +
                            "</td>" +
                            "</tr>";

                        strhtml1 += "</table>" +
                            "</td>" +
                            "</tr>";
                    }
                }

                if (_count > 0)
                {
                    strhtml1 += "<tr>" +
                        "<td width=\"49\" height=\"2\"></td>" +
                        "<td background=\"images/images03_05.gif\"></td>" +
                        "<td width=\"49\"></td>" +
                        "</tr>";
                }

                strhtml1 += "</table>" +
                    "</td>";

            }

            strhtml1 += "</td>" +
                "</tr>" +
                "</table>" +
                "</td>";
            strhtml1 += "</tr></table>";

        }
        catch (Exception ex)
        {
            strhtml1 = ex.Message.ToString();
        }
        return strhtml1;
    }

    /// <summary>
    /// 常用安置网络图
    /// </summary>
    /// <param name="Div_AnZhi">ＤＩＶ</param>
    /// <param name="TopBianhao">首点编号</param>
    /// <param name="qishu">期数</param>
    /// <returns>返回Table形式的字符串</returns>
    [AjaxPro.AjaxMethod]
    public string WangLuoTu_FindAnZhi(string TopBianhao)
    {
        string strhtml1 = "";
        try
        {
            string qishu = BLL.CommonClass.CommonDataBLL.GetMaxqishu().ToString();

            DataTable dt = BLL.other.Store.StoreInfoModifyBLL.GetMemberInfo(qishu);

            DataTable dt_anzhi;
            DataTable dt_anzhi1;
            DataRow[] row;
            int div_count = 0;
            int count = 0;
            int i = 0;
            int p = 0;
            int k = 0;
            int jb = 0;

            string jibie = "";
            string colors = "";
            //string sql = "";
            string anzhi = "";

            if (TopBianhao == "")
            {
                return "";
            }
            else
                anzhi = TopBianhao;


            dt_anzhi = BLL.other.Store.StoreInfoModifyBLL.GetMemberPlacement(anzhi, qishu);// DBHelper.ExecuteDataTable(sql);
            count = dt_anzhi.Rows.Count;

            div_count += count;
            for (i = 0; i < dt_anzhi.Rows.Count; i++)
            {
                dt_anzhi1 = BLL.other.Store.StoreInfoModifyBLL.GetMemberPlacement2(dt_anzhi.Rows[i]["number"].ToString(), qishu); //DBHelper.ExecuteDataTable("select *  from  memberInfo  where placeMent='" + dt_anzhi.Rows[i]["number"] + "' and  expectnum<='" + qishu + "' and  placement<>''");
                div_count += dt_anzhi1.Rows.Count;
            }

            string[] bianhaohtml = new string[div_count + 2]; ;
            string[] jibiehtml = new string[div_count + 2];
            string[] color = new string[div_count + 2];
            //string[] img = new string[div_count + 2];
            double[] zwyj = new double[div_count + 2];
            string[] petname = new string[div_count + 2];

            for (p = 0; p < div_count + 1; p++)
            {
                bianhaohtml[p] = "&nbsp;";
                jibiehtml[p] = "&nbsp";
                color[p] = "#b3b3b3";
                //img[p] = "images/mept.gif";
                zwyj[p] = 0;
            }
            #region 一层
            row = dt.Select("number='" + TopBianhao + "'", "number");
            if (row.Length > 0)
            {
                if (row[0]["number"].ToString().Trim() == "")
                {
                    jibie = "";
                }
                else
                {
                    jb = int.Parse(row[0]["level"].ToString());
                    //  jibie = Business.GetLevelForEng(jb.ToString());
                }

                colors = "#00cc00";
                if (row[0]["expectnum"].ToString() == BLL.CommonClass.CommonDataBLL.GetMaxqishu().ToString())
                {
                    colors = "red";
                }

                bianhaohtml[0] = "<a href=\"#\" onclick=\"UpNet('" + row[0]["number"].ToString() + "')\"><font color=#333333>" + row[0]["number"].ToString() + "</font></a>";
                jibiehtml[0] = jibie;
                color[0] = colors;
                //img[0] = Jiegou.GetLevel(jb.ToString(), 0);
                zwyj[0] = MemberInfoModifyBll.GetMemberNetYeJi(row[0]["number"].ToString(), Convert.ToInt32(qishu));
                petname[0] = MemberInfoModifyBll.GetPetNameByNumber(row[0]["number"].ToString());
            }
            #endregion
            #region 二层
            for (i = 0; i < dt_anzhi.Rows.Count + 1; i++)
            {
                if (i <= (dt_anzhi.Rows.Count - 1))
                {
                    row = dt.Select("number='" + dt_anzhi.Rows[i]["number"] + "'", "number");
                    string asgsa = dt_anzhi.Rows[i]["number"].ToString();
                    if (row.Length > 0)
                    {
                        //if (row[0]["number"].ToString().Trim() == "")
                        //{
                        //    jibie = "";
                        //}
                        //else
                        //{
                        //    jb = int.Parse(row[0]["level"].ToString());
                        //    // jibie = Business.GetLevelForEng(jb.ToString());
                        //}

                        colors = "#00cc00";
                        if (row[0]["expectnum"].ToString() == qishu)
                        {
                            colors = "red";
                        }

                        bianhaohtml[i + 1] = "<a  href=\"#\" onclick=\"DownNet('" + row[0]["number"].ToString() + "')\"   ><font color=#333333>" + row[0]["number"].ToString() + "</font></a>";
                        string asgd = bianhaohtml[i + 1].ToString();
                        jibiehtml[i + 1] = jibie;
                        color[i + 1] = colors;
                        //img[i + 1] = Jiegou.GetLevel(jb.ToString(), 0);
                        zwyj[i + 1] = MemberInfoModifyBll.GetMemberNetYeJi(row[0]["number"].ToString(), Convert.ToInt32(qishu));
                        petname[i + 1] = MemberInfoModifyBll.GetPetNameByNumber(row[0]["number"].ToString());
                    }
                }
                else
                {

                    colors = "#00cc00";

                    bianhaohtml[i + 1] = "<img style=\"cursor: pointer; \" src=\"images/xzuo.gif\" border=\"0\" onclick=\"GetAnZhiNumber('" + anzhi.ToString() + "')\"></img>";
                    color[i + 1] = colors;
                    //img[i + 1] = "images/ren04.gif";
                }
            }
            #endregion


            int _count1 = dt_anzhi.Rows.Count + 1;//第二层线的个数(上线推荐的个数)
            int _count2 = 0;
            int width1 = 0;//第二层线的宽度
            int _count3 = 0;
            int width3 = 0;
            int width4 = 0;
            int _count4 = 0;
            for (i = 0; i < _count1; i++)
            {
                if (i <= (dt_anzhi.Rows.Count - 1))
                {
                    if (_count1 > 1)
                    {
                        dt_anzhi1 = BLL.other.Store.StoreInfoModifyBLL.GetMemberPlacement(dt_anzhi.Rows[i]["number"].ToString(), qishu);// DBHelper.ExecuteDataTable("select *  from  memberinfo  where  placement='" + dt_anzhi.Rows[i]["number"] + "' and  expectnum<=" + qishu);
                        _count4 = dt_anzhi1.Rows.Count;

                        _count2++;
                        _count3++;
                    }
                    else
                        _count2 = _count1;
                }
                else
                {
                    _count2++;
                    _count3++;
                }
                if (i == 0)
                {
                    //width3第二层线头部的宽度
                    width3 = 50;
                }
                if (i == (_count1 - 1))
                {
                    width4 = 50;
                }
            }
            width1 = _count2 * 100 - width3 - width4;//第二层线中间部分的宽度

            strhtml1 = "<table  border=\"0\" cellspacing=\"0\" cellpadding=\"0\"> " +
                "<tr align=\"center\" valign=\"top\">" +
                "<td height=\"63\" colspan=\"" + _count1 + "\" align=\"center\">" +
                "<table width=\"100\" height=\"63\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">" +
                "<tr>" +
                "<td height=\"47\" colspan=\"3\" valign=\"top\">" +
                "<TABLE  borderColor=#CCCCCC cellSpacing=0  borderColorDark=#ffffff cellPadding=0 height=100%  width=100 class='bk2010'>" +

                "<TR>" +
                "<TD align=\"center\" colSpan=\"2\" height=\"20\" title=\"编号\">" + bianhaohtml[0] + "</TD>" +
                "</TR>" +

                "<TR>" +
                "<TD align=\"center\" colSpan=\"2\" height=\"20\" title=\"昵称\" style=\"font-size:10px;\">" + petname[0] + "</TD>" +
                "</TR>" +

                "<TR>" +
                "<TD align=\"center\" colSpan=\"2\" height=\"20\" title=\"总网业绩\" style=\"font-size:10px;\">" + zwyj[0].ToString("0.00") + "</TD>" +
                "</TR>" +

                "<TR>" +
                "<TD align=\"center\" colSpan=\"2\" height=\"20\" >" +
                "<table cellSpacing=0 cellPadding=0 style=\"height:18px;width:60px;\" >" +
                "<tr>" +
                "<td style=\"width:20px;\" align=\"center\"><img src='images/num01.gif' /></td>" +
                "<td style=\"width:20px;\" align=\"center\"><img src='images/num03.gif' /></td>" +
                "<td style=\"width:20px;\" align=\"center\"><img src='images/num02.gif' /></td>" +
                "</tr>" +
                "</table>" +
                "</TD>" +
                "</TR>" +

                "</TABLE>" +
                "</td>" +
                "</tr>";
            if (_count1 > 0)
            {
                strhtml1 += "<tr>" +
                    "<td width=\"49\" height=\"16\"></td>" +
                    "<td width=\"1\" background=\"images/images_02.gif\"  > </td>" +
                    "<td width=\"49\"></td>" +
                    "</tr>";

            }
            strhtml1 += "</table>" +
                "</td>" +
                "</tr>";
            if (_count1 > 0)
            {
                if (_count2 > 1)
                {
                    strhtml1 += "<tr align=\"center\" valign=\"top\">" +
                        "<td colspan=\"" + _count1 + "\" height=\"2\" >" +
                        "<table  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" +
                        "<tr>" +
                        "<td height=\"1\" width=\"" + width3 + "\" ></td>" +
                        "<td height=\"1\" width=\"" + width1 + "\"  background=\"images/images03_05.gif\"></td>" +
                        "<td height=\"1\" width=\"" + width4 + "\"  ></td>" +
                        "</tr>" +
                        "</table>" +
                        "</td>" +
                        "</tr>";
                }
                else
                {
                    strhtml1 += "<tr align=\"center\" valign=\"top\">" +
                        "<td colspan=\"" + _count1 + "\" height=\"2\" align=\"center\">" +
                        "<table  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" +
                        "<tr>" +
                        "<td height=\"1\" width=\"2\"  background=\"images/images03_05.gif\"></td>" +
                        "</tr>" +
                        "</table>" +
                        "</td>" +
                        "</tr>";
                }
            }

            strhtml1 += "<tr valign=\"top\">";

            int t = 0;
            int _count = 0;
            for (i = 0; i < dt_anzhi.Rows.Count + 1; i++)
            {
                if (i <= (dt_anzhi.Rows.Count - 1))
                {
                    strhtml1 += "<td  align=\"center\">" +
                        "<table  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" +
                        "<tr align=\"center\" valign=\"top\">" +
                        "<td height=\"79\" colspan=\"3\">" +
                        "<table  height=\"79\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">" +
                        "<tr>" +
                        "<td width=\"49\" height=\"16\"></td>" +
                        "<td width=\"1\"  background=\"images/images_02.gif\"></td>" +
                        "<td width=\"49\"></td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td height=\"47\" colspan=\"3\" valign=\"top\">" +
                        "<TABLE  borderColor=#CCCCCC cellSpacing=0  borderColorDark=#ffffff cellPadding=0 height=100%  width=100 class='bk2010' onmouseover=\"ShowViewDiv(this,'" + dt_anzhi.Rows[i]["number"].ToString() + "')\" onmouseout=\"HideViewDiv(this)\">" +

                        "<TR>" +
                        "<TD align=\"center\" colSpan=\"2\" height=\"20\" title=\"编号\">" + bianhaohtml[i + 1] + "</TD>" +
                        "</TR>" +

                        "<TR>" +
                        "<TD align=\"center\" colSpan=\"2\" height=\"20\" title=\"昵称\" style=\"font-size:10px;\">" + petname[i + 1] + "</TD>" +
                        "</TR>" +

                         "<TR>" +
                        "<TD align=\"center\"  colSpan=\"2\" height=\"20\" title=\"总网业绩\" style=\"font-size:10px;\">" + zwyj[i + 1].ToString("0.00") + "</TD>" +
                        "</TR>" +

                        "<TR>" +
                        "<TD align=\"center\" colSpan=\"2\" height=\"20\" >" +
                        "<table cellSpacing=0 cellPadding=0 style=\"height:18px;width:60px;\" >" +
                        "<tr>" +
                        "<td style=\"width:20px;\" align=\"center\"><img src='images/num01.gif' /></td>" +
                        "<td style=\"width:20px;\" align=\"center\"><img src='images/num03.gif' /></td>" +
                        "<td style=\"width:20px;\" align=\"center\"><img src='images/num02.gif' /></td>" +
                        "</tr>" +
                        "</table>" +
                        "</TD>" +
                        "</TR>" +

                        "</TABLE>" +
                        "</td>" +
                        "</tr>";

                    strhtml1 += "</table>" +
                        "</td>" +
                        "</tr>";
                }
                else
                {
                    if (i == dt_anzhi.Rows.Count)
                    {
                        strhtml1 += "<td  align=\"center\">" +
                            "<table  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" +
                            "<tr align=\"center\" valign=\"top\">" +
                            "<td height=\"79\" colspan=\"3\">" +
                            "<table  height=\"79\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">" +
                            "<tr>" +
                            "<td width=\"49\" height=\"16\"></td>" +
                            "<td width=\"1\"  background=\"images/images_02.gif\"></td>" +
                            "<td width=\"49\"></td>" +
                            "</tr>" +
                            "<tr>" +
                            "<td height=\"47\" colspan=\"3\" valign=\"top\">" +
                            "<TABLE  borderColor=#CCCCCC cellSpacing=0  borderColorDark=#ffffff cellPadding=0 height=100%  width=100>" +


                            "<TR>" +
                            "<TD   rowspan=\"2\"  valign=\"middle\"  align=\"center\" colSpan=\"2\" height=\"20\" title=\"编号\">" + bianhaohtml[i + 1] + "</TD>" +
                            "</TR>" +
                            "</TABLE>" +
                            "</td>" +
                            "</tr>";

                        strhtml1 += "</table>" +
                            "</td>" +
                            "</tr>";
                    }
                    else
                    {
                        strhtml1 += "<td  align=\"center\">" +
                            "<table  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" +
                            "<tr align=\"center\" valign=\"top\">" +
                            "<td height=\"79\" colspan=\"3\">" +
                            "<table  height=\"79\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">" +
                            "<tr>" +
                            "<td width=\"49\" height=\"16\"></td>" +
                            "<td width=\"1\"  background=\"images/images_02.gif\"></td>" +
                            "<td width=\"49\"></td>" +
                            "</tr>" +
                            "<tr>" +
                            "<td height=\"47\" colspan=\"3\" valign=\"top\">" +
                            "<TABLE  borderColor=#CCCCCC cellSpacing=0  borderColorDark=#ffffff cellPadding=0 height=100%  width=100>" +

                            "<TR >" +
                            "<TD  rowspan=\"2\"  valign=\"middle\" align=\"center\" colSpan=\"2\" height=\"20\"><img src='images/ren04.gif'></img></TD>" +
                            "</TR>" +


                            "</TABLE>" +
                            "</td>" +
                            "</tr>";

                        strhtml1 += "</table>" +
                            "</td>" +
                            "</tr>";
                    }
                }

                if (_count > 0)
                {
                    strhtml1 += "<tr>" +
                        "<td width=\"49\" height=\"2\"></td>" +
                        "<td background=\"images/images03_05.gif\"></td>" +
                        "<td width=\"49\"></td>" +
                        "</tr>";
                }

                strhtml1 += "</table>" +
                    "</td>";

            }

            strhtml1 += "</td>" +
                "</tr>" +
                "</table>" +
                "</td>";
            strhtml1 += "</tr></table>";

        }
        catch (Exception ex)
        {
            strhtml1 = ex.Message.ToString();
        }
        return strhtml1;
    }

    /// <summary>
    /// 常用安置网络图
    /// </summary>
    /// <param name="Div_AnZhi">ＤＩＶ</param>
    /// <param name="TopBianhao">首点编号</param>
    /// <param name="qishu">期数</param>
    /// <returns>返回Table形式的字符串</returns>
    [AjaxPro.AjaxMethod]
    public string WangLuoTu_AnZhiShowDiv(string TopBianhao)
    {
        string strhtml1 = "";
        try
        {
            string qishu = BLL.CommonClass.CommonDataBLL.GetMaxqishu().ToString();
            DataTable dt = DAL.DBHelper.ExecuteDataTable("select b.number,a.level,b.ExpectNum,b.Direct  from MemberInfoBalance" + qishu + " a,memberInfo b where a.number=b.number");

            DataTable dt_anzhi;
            DataTable dt_anzhi1;
            DataRow[] row;
            int div_count = 0;
            int count = 0;
            int i = 0;
            int p = 0;
            int k = 0;
            int jb = 0;

            string jibie = "";
            string colors = "";
            string sql = "";
            string anzhi = "";

            if (TopBianhao == "")
            {
                return "";
            }
            else
                anzhi = TopBianhao;

            sql = "select  *  from   memberInfo   where  Placement='" + anzhi + "' and  ExpectNum<='" + qishu + "' ";
            dt_anzhi = DAL.DBHelper.ExecuteDataTable(sql);
            count = dt_anzhi.Rows.Count;

            div_count += count;
            for (i = 0; i < dt_anzhi.Rows.Count; i++)
            {
                dt_anzhi1 = DAL.DBHelper.ExecuteDataTable("select *  from  memberInfo  where  Placement='" + dt_anzhi.Rows[i]["number"] + "' and  ExpectNum<='" + qishu + "' and  Direct<> (select top 1 number from manage where defaultmanager = 1)");
                div_count += dt_anzhi1.Rows.Count;
            }

            string[] bianhaohtml = new string[div_count + 10];
            string[] jibiehtml = new string[div_count + 10];
            string[] color = new string[div_count + 10];
            double[] zwyj = new double[div_count + 10];
            string[] petname = new string[div_count + 10];

            for (p = 0; p < div_count; p++)
            {
                bianhaohtml[p] = "&nbsp;";
                jibiehtml[p] = "&nbsp";
                color[p] = "#b3b3b3";
                zwyj[p] = 0;
            }
            BLL.CommonClass.CommonDataBLL commonDataBLL = new BLL.CommonClass.CommonDataBLL();

            #region 一层
            row = dt.Select("number='" + TopBianhao + "'", "number");
            if (row.Length > 0)
            {
                colors = "#00cc00";
                if (row[0]["ExpectNum"].ToString() == CommonDataBLL.getMaxqishu().ToString())
                {
                    colors = "red";
                }

                bianhaohtml[0] = "<a href=\"#\" onclick=\"UpNet('" + row[0]["number"].ToString() + "')\"><font color=#333333>" + row[0]["number"].ToString() + "</font></a>";
                color[0] = colors;
                zwyj[0] = MemberInfoModifyBll.GetMemberNetYeJi(row[0]["number"].ToString(), Convert.ToInt32(qishu));
                petname[0] = MemberInfoModifyBll.GetPetNameByNumber(row[0]["number"].ToString());
            }
            #endregion

            #region 二层
            for (i = 0; i < dt_anzhi.Rows.Count; i++)
            {
                string helper = "number='" + dt_anzhi.Rows[i]["number"] + "'";
                row = dt.Select(helper, "number");
                if (row.Length > 0)
                {
                    colors = "#00cc00";
                    if (row[0]["ExpectNum"].ToString() == CommonDataBLL.getMaxqishu().ToString())
                    {
                        colors = "red";
                    }
                    string helper33 = row[0]["number"] + "";
                    bianhaohtml[i + 1] = "<table style=\"cursor:hand;font-size:12px;\"><tr><td onclick=\"DownNet('" + row[0]["number"].ToString() + "')\"><font color=#333333 >" + row[0]["number"].ToString() + "</font></td></tr></table>";
                    color[i + 1] = colors;
                    zwyj[i + 1] = MemberInfoModifyBll.GetMemberNetYeJi(row[0]["number"].ToString(), Convert.ToInt32(qishu));
                    petname[i + 1] = MemberInfoModifyBll.GetPetNameByNumber(row[0]["number"].ToString());
                }
            }
            #endregion

            int _count1 = dt_anzhi.Rows.Count;//第二层线的个数(上线推荐的个数)
            int _count2 = 0;
            int width1 = 0;                     //第二层线的宽度
            int _count3 = 0;
            int width3 = 0;
            int width4 = 0;
            int _count4 = 0;
            for (i = 0; i < _count1; i++)
            {
                if (_count1 > 1)
                {
                    dt_anzhi1 = DAL.DBHelper.ExecuteDataTable("select *  from  memberInfo  where  Placement='" + dt_anzhi.Rows[i]["number"] + "' and  ExpectNum<='" + qishu + "'");
                    _count4 = dt_anzhi1.Rows.Count;
                    if (_count4 > 1)
                    {
                        _count2 += _count4;
                        _count3 += _count4;
                    }
                    else
                    {
                        _count2++;
                        _count3++;
                    }
                }
                else
                    _count2 = _count1;

                if (i == 0)
                {
                    //width3第二层线头部的宽度
                    if (_count4 <= 1)
                        width3 = 50;
                    else
                        width3 = _count4 * 50;
                }
                if (i == (_count1 - 1))
                {
                    //width4第二层线头部的宽度
                    if (_count4 <= 1)
                        width4 = 50;
                    else
                        width4 = _count4 * 50;
                }
            }
            width1 = _count2 * 100 - width3 - width4;//第二层线中间部分的宽度

            strhtml1 = "<table  border=\"0\" cellspacing=\"0\" cellpadding=\"0\"> ";

            if (_count1 > 0)
            {
                if (_count2 > 1)
                {
                    strhtml1 += "<tr align=\"center\" valign=\"top\">" +
                                "<td colspan=\"" + _count1 + "\" height=\"1\" >" +
                                    "<table  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" +
                                        "<tr>" +
                                            "<td height=\"1\"  width=\"" + width3 + "\" ></td>" +
                                            "<td height=\"1\"  width=\"" + width1 + "\"  background=\"images/images03_05.gif\"></td>" +
                                            "<td height=\"1\"  width=\"" + width4 + "\"  ></td>" +
                                        "</tr>" +
                                    "</table>" +
                                "</td>" +
                            "</tr>";
                }
                else
                {

                    strhtml1 += "<tr align=\"center\" valign=\"top\">" +
                                "<td colspan=\"" + _count1 + "\" height=\"1\" align=\"center\">" +
                                    "<table  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" +
                                        "<tr>" +
                                            "<td height=\"2\" width=\"2\"  background=\"images/images03_05.gif\"></td>" +
                                        "</tr>" +
                                    "</table>" +
                                "</td>" +
                            "</tr>";
                }
            }

            strhtml1 += "<tr valign=\"top\">";

            int t = 0;
            int _count = 0;
            int width2 = 0;
            for (i = 0; i < dt_anzhi.Rows.Count; i++)
            {
                dt_anzhi1 = DAL.DBHelper.ExecuteDataTable("select *  from  memberInfo  where  Placement='" + dt_anzhi.Rows[i]["number"] + "'  and  ExpectNum<='" + qishu + "'");
                _count = dt_anzhi1.Rows.Count;
                width2 = (_count - 1) * 100;//第三层线的宽度
                strhtml1 += "<td  align=\"center\">" +
                            "<table  border=\"0\" cellspacing=\"0\" cellpadding=\"0\" >" +
                                "<tr align=\"center\" valign=\"top\">" +
                                    "<td height=\"79\" colspan=\"3\">" +
                                        "<table  height=\"79\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">" +
                                            "<tr>" +
                                                "<td width=\"49\" height=\"16\"></td>" +
                                                "<td width=\"1\"  background=\"images/images02_02.gif\"></td>" +
                                                "<td width=\"49\"></td>" +
                                            "</tr>" +
                                            "<tr>" +
                                                "<td height=\"100\" colspan=\"3\" valign=\"top\">" +
                                                    "<TABLE  borderColor=#CCCCCC cellSpacing=0  borderColorDark=#ffffff cellPadding=0 height=100%  width=100 class='bk2010' >" +
                                                        "<TR>" +
                                                           "<TD align=\"center\" colSpan=\"2\" height=\"20\" title=\"编号\">" + bianhaohtml[i + 1] + "</TD>" +
                                                        "</TR>" +
                                                        "<TR>" +
                                                             "<TD align=\"center\" colSpan=\"2\" height=\"20\" title=\"昵称\" style=\"font-size:10px;\">" + petname[i + 1] + "</TD>" +
                                                        "</TR>" +
                                                        "<TR>" +
                                                            "<TD align=\"center\"  colSpan=\"2\" height=\"20\" title=\"总网业绩\" style=\"font-size:10px;\">" + zwyj[i + 1].ToString("0.00") + "</TD>" +
                                                        "</TR>" +
                                                        "<TR>" +
                                                             "<TD align=\"center\" colSpan=\"2\" height=\"20\" >" +
                                                                "<table cellSpacing=0 cellPadding=0 style=\"height:18px;width:60px;\" >" +
                                                                     "<tr>" +
                                                                        "<td style=\"width:20px;\" align=\"center\"><img src='images/num01.gif' /></td>" +
                                                                        "<td style=\"width:20px;\" align=\"center\"><img src='images/num03.gif' /></td>" +
                                                                        "<td style=\"width:20px;\" align=\"center\"><img src='images/num02.gif' /></td>" +
                                                                   "</tr>" +
                                                                 "</TABLE>" +
                                                             "</td>" +
                                                         "</tr>" +
                                                    "</table>" +
                                                "</td>" +
                                           "</tr>";
                if (_count > 0)
                {
                    strhtml1 += "<tr>" +
                                    "<td width=\"49\" height=\"16\"></td>" +
                                    "<td width=\"1\"  background=\"images/images02_02.gif\"></td>" +
                                    "<td width=\"49\"></td>" +
                                "</tr>";
                }

                strhtml1 += "</table>" +
                        "</td>" +
                     "</tr>";

                if (_count > 0)
                {
                    if (_count == 1)
                    {
                        strhtml1 += "<tr>" +
                                    "<td width=\"49\" height=\"1\"></td>" +
                                    "<td background=\"images/images03_05.gif\"></td>" +
                                    "<td width=\"49\"></td>" +
                                    "</tr>";
                    }
                    else
                    {
                        strhtml1 += "<tr>" +
                                    "<td width=\"49\" height=\"1\"></td>" +
                                    "<td width=\"" + width2 + "\" background=\"images/images03_05.gif\"></td>" +
                                    "<td width=\"49\"></td>" +
                                    "</tr>";
                    }
                    strhtml1 += "<tr vAlign=\"top\">" +
                                 "<td colSpan=\"3\" align=\"center\">" +
                                    "<table cellSpacing=\"0\" cellPadding=\"0\"  border=\"0\">" +
                                        "<tr vAlign=\"top\">";
                }
                //if(t==1)
                //count+=dt_tuijian1.Rows.Count;
                for (int j = 0; j < dt_anzhi1.Rows.Count; j++)
                {
                    row = dt.Select("number='" + dt_anzhi1.Rows[j]["number"] + "'", "number");
                    if (row.Length > 0)
                    {
                        colors = "#00cc00";
                        if (row[0]["ExpectNum"].ToString() == CommonDataBLL.getMaxqishu().ToString())
                        {
                            colors = "red";
                        }
                        bianhaohtml[count + 1 + j] = "<table style=\"cursor:hand;font-size:12px;\"><tr><td onclick=\"DownNet('" + row[0]["number"].ToString() + "')\"><font color=\"#333333\" >" + row[0]["number"].ToString() + "</font></td></tr></table>";
                        jibiehtml[count + 1 + j] = jibie;
                        color[count + 1 + j] = colors;
                        zwyj[count + 1 + j] = MemberInfoModifyBll.GetMemberNetYeJi(row[0]["number"].ToString(), Convert.ToInt32(qishu));
                        petname[count + 1 + j] = MemberInfoModifyBll.GetPetNameByNumber(row[0]["number"].ToString());

                        strhtml1 += "<td  height=\"63\" align=\"center\">" +
                                        "<table cellSpacing=\"0\" cellPadding=\"0\"  border=\"0\">" +
                                            "<tr>" +
                                                "<td width=\"49\" height=\"16\"></td>" +
                                                "<td width=\"1\" background=\"images/images02_02.gif\"></td>" +
                                                "<td width=\"49\" ></td>" +
                                            "</tr>" +
                                            "<tr>" +
                                                "<td vAlign=\"top\" colSpan=\"3\" height=\"100\">" +
                                                    "<TABLE  borderColor=#CCCCCC cellSpacing=0  borderColorDark=#ffffff cellPadding=0 height=100%  width=100 class='bk2010' >" +
                                                        "<TR>" +
                                                            "<TD align=\"center\" colSpan=\"2\" height=\"20\" title=\"编号\">" + bianhaohtml[count + 1 + j] + "</TD>" +
                                                        "</TR>" +
                                                        "<TR>" +
                                                              "<TD align=\"center\" colSpan=\"2\" height=\"20\" title=\"昵称\" style=\"font-size:10px;\">" + petname[count + 1 + j] + "</TD>" +
                                                        "</TR>" +
                                                               "<TD align=\"center\"  colSpan=\"2\" height=\"20\" title=\"总网业绩\" style=\"font-size:10px;\">" + zwyj[count + 1 + j].ToString("0.00") + "</TD>" +
                                                        "</TR>" +
                                                        "<TR>" +
                                                            "<TD align=\"center\" colSpan=\"2\" height=\"20\" >" +
                                                               "<table cellSpacing=0 cellPadding=0 style=\"height:18px;width:60px;\" >" +
                                                                  "<tr>" +
                                                                    "<td style=\"width:20px;\" align=\"center\"><img src='images/num01.gif' /></td>" +
                                                                    "<td style=\"width:20px;\" align=\"center\"><img src='images/num03.gif' /></td>" +
                                                                    "<td style=\"width:20px;\" align=\"center\"><img src='images/num02.gif' /></td>" +
                                                                  "</tr>" +
                                                              "</table>" +
                                                            "</td>" +
                                                         "<TR>" +
                                                     "</TABLE>" +
                                                "</td>" +
                                            "</tr>" +
                                        "</table>" +
                                    "</td>";
                    }

                }
                t = 1;
                if (_count > 0)
                {
                    strhtml1 += "</tr>" +
                        "</table>" +
                        "</td>" +
                        "</tr>";
                }

                strhtml1 += "</table>" +
                           "</td>";


            }
            strhtml1 += "</td>" +
                      "</tr>" +
                      "</table>" +
                      "</td>";
            strhtml1 += "</tr></table>";

        }
        catch (Exception ex)
        {
            strhtml1 = ex.Message.ToString();
        }
        return strhtml1;
    }

    /// <summary>
    /// 常用推荐网络图
    /// </summary>
    /// <param name="Div_TuiJian">ＤＩＶ</param>
    /// <param name="TopBianhao">首点编号</param>
    /// <param name="ExpectNum">期数</param>
    /// <returns>返回Table形式的字符串</returns>
    [AjaxPro.AjaxMethod]
    public string Direct_Table2(string TopBianhao)
    {
        string qishu = BLL.CommonClass.CommonDataBLL.getMaxqishu().ToString();
        string temp = string.Empty;//字段（推荐编号或安置编号）
        temp = "Placement";

        string str = "select b.number,a.level,b.ExpectNum,b.Direct  from MemberInfoBalance" + qishu + " a,memberInfo b where a.number=b.number";
        DataTable dt = DAL.DBHelper.ExecuteDataTable(str);

        DataTable dt_tuijian;
        DataTable dt_tuijian1;
        DataRow[] row;
        int div_count = 0;
        int count = 0;
        int i = 0;
        int p = 0;
        int k = 0;
        int jb = 0;

        string jibie = "";
        string colors = "";
        string sql = "";
        string tuijian = "";

        //if (TopBianhao == "")
        //{
        //    tuijian = DAL.DBHelper.ExecuteScalar("select number from manage where defaultmanager = 1").ToString();
        //}
        //else
        tuijian = TopBianhao;
        sql = "select  *  from   memberInfo   where  " + temp + "='" + tuijian + "' and  ExpectNum<='" + qishu + "' ";
        dt_tuijian = DAL.DBHelper.ExecuteDataTable(sql);
        count = dt_tuijian.Rows.Count;

        div_count += count;
        for (i = 0; i < dt_tuijian.Rows.Count; i++)
        {
            dt_tuijian1 = DAL.DBHelper.ExecuteDataTable("select *  from  memberInfo  where  " + temp + "='" + dt_tuijian.Rows[i]["number"] + "' and  ExpectNum<='" + qishu + "' and  Direct<> (select top 1 number from manage where defaultmanager = 1)");
            div_count += dt_tuijian1.Rows.Count;
        }

        string[] bianhaohtml = new string[div_count + 10];
        string[] jibiehtml = new string[div_count + 10];
        string[] color = new string[div_count + 10];
        for (p = 0; p < div_count; p++)
        {
            bianhaohtml[p] = "&nbsp;";
            jibiehtml[p] = "&nbsp";
            color[p] = "#b3b3b3";
        }
        BLL.CommonClass.CommonDataBLL commonDataBLL = new BLL.CommonClass.CommonDataBLL();
        #region 一层
        row = dt.Select("number='" + TopBianhao + "'", "number");
        if (row.Length > 0)
        {
            if (row[0]["number"].ToString().Trim() == "") { jibie = ""; }
            else
            {
                jb = row[0]["level"] == System.DBNull.Value ? 0 : int.Parse(row[0]["level"].ToString());
                //jb = row[0]["level"] == System.DBNull.Value ? 0 : int.Parse(row[0]["level"].ToString());
                jibie = GetLevel(jb.ToString(), 0);
            }

            colors = "#00cc00";
            if (row[0]["ExpectNum"].ToString() == CommonDataBLL.getMaxqishu().ToString())
            {
                colors = "red";
            }
            bianhaohtml[0] = "<a href=javascript:ShowDiv('" + row[0]["number"] + "')>" + row[0]["number"].ToString() + "</a>";
            jibiehtml[0] = jibie;
            color[0] = colors;
        }
        #endregion
        #region 二层
        for (i = 0; i < dt_tuijian.Rows.Count; i++)
        {
            string helper = "number='" + dt_tuijian.Rows[i]["number"] + "'";
            row = dt.Select(helper, "number");
            if (row.Length > 0)
            {

                if (row[0]["number"].ToString().Trim() == "") { jibie = ""; }
                else
                {
                    string helper2 = "" + row[0]["level"];
                    jb = row[0]["level"] == System.DBNull.Value ? 0 : int.Parse(row[0]["level"].ToString());
                    //jb = row[0]["level"] == System.DBNull.Value ? 0 : int.Parse(row[0]["level"].ToString());
                    jibie = GetLevel(jb.ToString(), 0);
                }
                colors = "#00cc00";
                if (row[0]["ExpectNum"].ToString() == CommonDataBLL.getMaxqishu().ToString())
                {
                    colors = "red";
                }
                string helper33 = row[0]["number"] + "";
                bianhaohtml[i + 1] = "<a href=javascript:ShowDiv('" + row[0]["number"] + "')>" + row[0]["number"].ToString() + "</a>";
                jibiehtml[i + 1] = jibie;
                color[i + 1] = colors;
            }
        }
        #endregion

        string strhtml1 = "";
        int _count1 = dt_tuijian.Rows.Count;//第二层线的个数(上线推荐的个数)
        int _count2 = 0;
        int width1 = 0;                     //第二层线的宽度
        int _count3 = 0;
        int width3 = 0;
        int width4 = 0;
        int _count4 = 0;
        for (i = 0; i < _count1; i++)
        {
            if (_count1 > 1)
            {
                dt_tuijian1 = DAL.DBHelper.ExecuteDataTable("select *  from  memberInfo  where  " + temp + "='" + dt_tuijian.Rows[i]["number"] + "' and  ExpectNum<='" + qishu + "'");
                _count4 = dt_tuijian1.Rows.Count;
                if (_count4 > 1)
                {
                    _count2 += _count4;
                    _count3 += _count4;
                }
                else
                {
                    _count2++;
                    _count3++;
                }
            }
            else
                _count2 = _count1;

            if (i == 0)
            {
                //width3第二层线头部的宽度
                if (_count4 <= 1)
                    width3 = 50;
                else
                    width3 = _count4 * 50;
            }
            if (i == (_count1 - 1))
            {
                //width4第二层线头部的宽度
                if (_count4 <= 1)
                    width4 = 50;
                else
                    width4 = _count4 * 50;
            }
        }
        width1 = _count2 * 100 - width3 - width4;//第二层线中间部分的宽度


        strhtml1 = "<table  border=\"0\" cellspacing=\"0\" cellpadding=\"0\"> " +
                    "<tr align=\"center\" valign=\"top\">" +
                        "<td height=\"63\" colspan=\"" + _count1 + "\" align=\"center\">" +
                            "<table width=\"100\" height=\"63\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">" +
                                "<tr>" +
                                    "<td height=\"47\" colspan=\"3\" valign=\"top\">" +
                                        "<TABLE  borderColor=#CCCCCC cellSpacing=0  borderColorDark=#ffffff cellPadding=0 height=100%  width=100 border=1>" +
                                            "<TR>" +
                                                "<TD align=\"center\" colSpan=\"2\" height=\"20\">" + bianhaohtml[0] + "</TD>" +
                                            "</TR>" +
            //"<TR>" +
            //    "<TD align=\"center\" bgColor=\"#ffffcc\" colSpan=\"2\" height=\"20\"><font size=2px><img height='18' src=''></font></TD>" +//" + jibiehtml[0] + "
            //"</TR>" +
                                            "<TR>" +
                                                "<TD align=\"center\" colSpan=\"2\" height=\"17\"><input type=\"button\" id=\"selected\" value=\"请选择\" class=\"anyes\" onclick=\"OnChose('" + TopBianhao + "')\"/></TD>" +
                                            "</TR>" +
                                        "</TABLE>" +
                                    "</td>" +
                                "</tr>";
        if (_count1 > 0)
        {
            strhtml1 += "<tr>" +
                           "<td width=\"49\" height=\"16\"></td>" +
                           "<td width=\"2\" background=\"images/images02_02.gif\"  > </td>" +
                           "<td width=\"49\"></td>" +
                       "</tr>";

        }
        strhtml1 += "</table>" +
                "</td>" +
            "</tr>";
        if (_count1 > 0)
        {
            if (_count2 > 1)
            {
                strhtml1 += "<tr align=\"center\" valign=\"top\">" +
                            "<td colspan=\"" + _count1 + "\" height=\"2\" >" +
                                "<table  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" +
                                    "<tr>" +
                                        "<td height=\"2\"  width=\"" + width3 + "\" ></td>" +
                                        "<td height=\"2\"  width=\"" + width1 + "\"  background=\"images/images03_05.gif\"></td>" +
                                        "<td height=\"2\"  width=\"" + width4 + "\"  ></td>" +
                                    "</tr>" +
                                "</table>" +
                            "</td>" +
                        "</tr>";
            }
            else
            {

                strhtml1 += "<tr align=\"center\" valign=\"top\">" +
                            "<td colspan=\"" + _count1 + "\" height=\"2\" align=\"center\">" +
                                "<table  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" +
                                    "<tr>" +
                                        "<td height=\"2\" width=\"2\"  background=\"images/images03_05.gif\"></td>" +
                                    "</tr>" +
                                "</table>" +
                            "</td>" +
                        "</tr>";
            }
        }

        strhtml1 += "<tr valign=\"top\">";

        int t = 0;
        int _count = 0;
        int width2 = 0;
        for (i = 0; i < dt_tuijian.Rows.Count; i++)
        {
            dt_tuijian1 = DAL.DBHelper.ExecuteDataTable("select *  from  memberInfo  where  " + temp + "='" + dt_tuijian.Rows[i]["number"] + "'  and  ExpectNum<='" + qishu + "'");
            _count = dt_tuijian1.Rows.Count;
            width2 = (_count - 1) * 100;//第三层线的宽度
            strhtml1 += "<td  align=\"center\">" +
                        "<table  border=\"0\" cellspacing=\"0\" cellpadding=\"0\" >" +
                            "<tr align=\"center\" valign=\"top\">" +
                                "<td height=\"79\" colspan=\"3\">" +
                                    "<table  height=\"79\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">" +
                                        "<tr>" +
                                            "<td width=\"49\" height=\"16\"></td>" +
                                            "<td width=\"2\"  background=\"images/images02_02.gif\"></td>" +
                                            "<td width=\"49\"></td>" +
                                        "</tr>" +
                                        "<tr>" +
                                            "<td height=\"47\" colspan=\"3\" valign=\"top\">" +
                                                 "<TABLE  borderColor=#CCCCCC cellSpacing=0  borderColorDark=#ffffff cellPadding=0 height=100%  width=100 border=1  >" +
                                                    "<TR>" +
                                                        "<TD align=\"center\" colSpan=\"2\" height=\"20\">" + bianhaohtml[i + 1] + "</TD>" +
                                                    "</TR>" +
                //"<TR>" +
                //    "<TD align=\"center\" bgColor=\"#ffffcc\" colSpan=\"2\" height=\"20\"><font size=2px><img height='18' src=''></font></TD>" +
                //"</TR>" +
                                                    "<TR>" +
                                                        "<TD align=\"center\" colSpan=\"2\" height=\"17\"><input type=\"button\" id=\"selected\" value=\"请选择\" class=\"anyes\"  onclick=\"OnChose('" + dt_tuijian.Rows[i]["number"] + "')\"/></TD>" +
                                                    "</TR>" +
                                                "</TABLE>" +
                                            "</td>" +
                                        "</tr>";
            if (_count > 0)
            {
                //strhtml1 += "<tr>" +
                //                "<td width=\"49\" height=\"16\"></td>" +
                //                "<td width=\"2\"  background=\"images/images02_02.gif\"></td>" +
                //                "<td width=\"49\"></td>" +
                //            "</tr>";
            }

            strhtml1 += "</table>" +
                    "</td>" +
                 "</tr>";

            if (_count > 0)
            {
                if (_count == 1)
                {
                    strhtml1 += "<tr>" +
                                "<td width=\"49\" height=\"2\"></td>" +
                                "<td background=\"images/images03_05.gif\"></td>" +
                                "<td width=\"49\"></td>" +
                                "</tr>";
                }
                else
                {
                    strhtml1 += "<tr>" +
                                "<td width=\"49\" height=\"2\"></td>" +
                                "<td width=\"" + width2 + "\" background=\"images/images03_05.gif\"></td>" +
                                "<td width=\"49\"></td>" +
                                "</tr>";
                }
                strhtml1 += "<tr vAlign=\"top\">" +
                             "<td colSpan=\"3\" align=\"center\">" +
                                "<table cellSpacing=\"0\" cellPadding=\"0\"  border=\"0\">" +
                                    "<tr vAlign=\"top\">";
            }
            if (t == 1)
                count += dt_tuijian1.Rows.Count;
            t = 1;
            if (_count > 0)
            {
                strhtml1 += "</tr>" +
                    "</table>" +
                    "</td>" +
                    "</tr>";
            }

            strhtml1 += "</table>" +
                       "</td>";


        }
        strhtml1 += "</td>" +
                  "</tr>" +
                  "</table>" +
                  "</td>";
        strhtml1 += "</tr></table>";

        return strhtml1;
    }

    /// <summary>
    /// 对应级别。
    /// </summary>
    /// <param name="level">根据级别对应资格</param>
    public static string GetLevel(string level, int levelType)
    {
        string chaBuMoney = "";
        chaBuMoney = DAL.CommonDataDAL.GetLevel(level, levelType);
        return chaBuMoney;
    }

    //获取币种
    [AjaxPro.AjaxMethod]
    public static string GetCurrency()
    {
        return DBHelper.ExecuteScalar("select top 1 standardMoney from dbo.Currency").ToString();
    }
    [AjaxPro.AjaxMethod]
    public static DataTable BindCountry_Bank(string CountryCode)
    {
        return CommonDataBLL.BindCountry_Bank(CountryCode);
    }
    [AjaxPro.AjaxMethod]
    public static int getPermissions()
    {
        return Permissions.GetPermissions(EnumCompanyPermission.CustomerModifyMemberInfoZheHao);
    }

    [AjaxPro.AjaxMethod]
    public DataSet GetDepartmentList(string CorpCd)
    {
        DataTable dt = StorageInBLL.GetDepotSeatInfoByWareHouaseID(Convert.ToInt32(CorpCd));
        DataSet ds = new DataSet();
        ds.Tables.Add(dt);

        return ds;
    }

    [AjaxPro.AjaxMethod]
    public string GetAlbumsIDByID(string Id)
    {
        string albumsId = DAL.DBHelper.ExecuteScalar("Select Top 1 AlbumsID From AlbumsPhotos Where ID=" + Id).ToString();
        return albumsId;
    }

    [AjaxPro.AjaxMethod]
    public bool SetImagePhoto(string number, string photoPath)
    {
        string sql = "Update MemberInfo Set PhotoPath = @PhotoPath Where number=@number";
        SqlParameter[] para = {
                                  new SqlParameter("@PhotoPath",photoPath.Substring(2,photoPath.Length-2)),
                                  new SqlParameter("@number",number)
                              };
        int count = (int)DBHelper.ExecuteNonQuery(sql, para, CommandType.Text);
        if (count == 0)
            return false;
        return true;
    }


    [AjaxPro.AjaxMethod]
    public int Addgz(string myid, string number)
    {
        string sql = "insert into Friendmy(number,Friendid,Groupid,Microblogflag ,Createdate,CreateIP) values(@number,@Friendid,@Groupid,@Microblogflag ,@Createdate,@CreateIP)";
        SqlParameter[] para = {
                                  new SqlParameter("@number",SqlDbType.NVarChar,20),
                                  new SqlParameter("@Friendid",SqlDbType.NVarChar,20),
                                  new SqlParameter("@Groupid",SqlDbType.Int),
                                  new SqlParameter("@Microblogflag",SqlDbType.Int),
                                  new SqlParameter("@Createdate",SqlDbType.DateTime),
                                  new SqlParameter("@CreateIP",SqlDbType.NVarChar,50)
                              };
        para[0].Value = myid;
        para[1].Value = number;
        para[2].Value = 0;
        para[3].Value = 1;
        para[4].Value = DateTime.Now;
        para[5].Value = HttpContext.Current.Request.UserHostAddress;
        int count = (int)DBHelper.ExecuteNonQuery(sql, para, CommandType.Text);

        return count;

    }


    [AjaxPro.AjaxMethod]
    public int getDelMicroblog(object id)
    {

        int delmic = DBHelper.ExecuteNonQuery("delete from Microblog where id=" + id.ToString());

        return delmic;
    }


    [AjaxPro.AjaxMethod]
    public int getDelFriendmy(object id)
    {

        int delmic = DBHelper.ExecuteNonQuery("delete from Friendmy where id=" + id.ToString());

        return delmic;
    }

    [AjaxPro.AjaxMethod]
    public string getUpGroup(object value, object Friendmyid, object FriendGroupid)
    {

        string UpGroupName = "";
        if (value.ToString() == "0" && FriendGroupid.ToString() == "0")
        {
            int UpGroupid = DBHelper.ExecuteNonQuery("update Friendmy set Groupid=0 where id=" + Friendmyid.ToString());
            if (UpGroupid > 0)
            {
                UpGroupName = "未分组";
            }
        }
        else
        {
            int UpGroupid = DBHelper.ExecuteNonQuery("update Friendmy set Groupid=" + FriendGroupid.ToString() + " where id=" + Friendmyid.ToString());
            if (UpGroupid > 0)
            {
                UpGroupName = DBHelper.ExecuteScalar("select groupname from FriendGroup where id=" + FriendGroupid.ToString()).ToString();
            }
        }

        return UpGroupName;
    }

    [AjaxPro.AjaxMethod]
    public string getUpFriendGroup(object id, string number)
    {
        StringBuilder divBuilder = new StringBuilder();
        DataTable dt = new DataTable();
        string newdiv = "";
        dt = DBHelper.ExecuteDataTable("select * from FriendGroup where number='" + number + "'");
        divBuilder.Append("<div id='divPop" + id + "' style='background-color: #f0f0f0; border: solid 1px #000000; position: absolute;display:none; width: 200px;'>");
        divBuilder.Append("<ul>");

        if (dt.Rows.Count > 0)
        {
            divBuilder.Append("<li><input id='rbt" + id + "_0' type='radio' name='" + id + "' value='0' onclick='UpFriendGroup(this.value," + id.ToString() + ",0)' /><label for='rbt" + id + "_0'>未分组</label></li>");
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                divBuilder.Append("<li><input id='rbt" + id + "_" + (i + 1) + "' type='radio' name='" + id.ToString() + "' value='" + dt.Rows[i]["id"].ToString() + "' onclick='UpFriendGroup(this.value," + id.ToString() + "," + dt.Rows[i]["id"].ToString() + ")' /><label for='rbt" + id + "_" + (i + 1) + "'>" + dt.Rows[i]["groupname"].ToString() + "</label></li>");
            }
        }
        else
        {
            divBuilder.Append("<li>没有创建分组！</li>");
        }
        divBuilder.Append("<li><a href='javascript:viod(0)' id='close" + id + "' onclick='closeUpdiv(" + id + ")'>关闭</a>！</li>");
        divBuilder.Append("</ul>");
        divBuilder.Append("</div>");

        newdiv = divBuilder.ToString();

        return newdiv;
    }


    [AjaxPro.AjaxMethod]
    public string getNewDivId(string number)
    {
        string newdivId = "";

        DataTable dt = DBHelper.ExecuteDataTable("select top 1 m.*,s.petname,f.Friendid from microblog m left join memberinfo s on  m.number=s.number left join Friendmy f on m.number=f.friendid where m.number='" + number + "' order by createdate desc");

        if (dt.Rows.Count > 0)
        {
            newdivId = "jqdiv" + dt.Rows[0]["id"].ToString();
        }

        return newdivId;
    }

    [AjaxPro.AjaxMethod]
    public string getNewDiv(string content, string number)
    {
        StringBuilder divBuilder = new StringBuilder();
        DataTable dt = new DataTable();
        string newdiv = "";
        string img = "";
        string img1 = "";

        int num = BLL.MicroblogManager.inserMicroblog(content, number, HttpContext.Current.Request.UserHostAddress);

        if (num > 0)
        {
            dt = DBHelper.ExecuteDataTable("select top 1 m.*,s.petname,f.Friendid,s.photopath from microblog m left join memberinfo s on  m.number=s.number left join Friendmy f on m.number=f.friendid where m.number='" + number + "' order by createdate desc");
            if (dt.Rows.Count > 0)
            {
                img = dt.Rows[0]["photopath"].ToString();
                if (img != "")
                {
                    img1 = img.Replace("MemberSpace/", "");
                    img1 = img.Replace("MemberSpace/", "");
                }
                else
                {
                    img1 = "../../photo/default.gif";
                }
                divBuilder.Append("<div class='meager_cont' style='display:none' id='jqdiv" + dt.Rows[0]["id"].ToString() + "'>");
                divBuilder.Append("<dl>");
                divBuilder.Append("<dd> <img  ID=Image2 src='../" + img1.ToString() + "' width='45' height='45'   /></dd>");
                divBuilder.Append("<dt><span class='meagerfont'>" + dt.Rows[0]["petname"].ToString() + "：</span>" + dt.Rows[0]["Microblog"].ToString() + "");
                divBuilder.Append("<br />");
                divBuilder.Append("<span class='m_dt'>时间：" + dt.Rows[0]["Createdate"].ToString() + "</span>&nbsp;&nbsp;&nbsp;&nbsp;");

                divBuilder.Append("<input type='button' onclick='javascript:Submit_send(" + dt.Rows[0]["id"].ToString() + ")'  id='" + dt.Rows[0]["id"].ToString() + "' value='删除' class='button01' />");
                divBuilder.Append("</dt>");
                divBuilder.Append("</dl>");
                divBuilder.Append("</div>");

                newdiv = divBuilder.ToString();
            }

        }
        else
        {
            newdiv = "";
        }
        return newdiv;


    }
    /// <summary>
    /// 供应商编号是否存在  ---DS2012
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string checkProviderID(string id)
    {
        if (ProviderManageBLL.GetProviderinfoNumber(id))
        {
            return GetTran("000000", "供应商编号已存在！");
        }
        else
        {
            return "";
        }
    }
    /// <summary>
    /// 供应商名称是否存在  ---DS2012
    /// </summary>
    /// <param name="name"></param>
    /// <param name="num"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string checkProviderName(string name, string num)
    {
        if (num == "")
        {
            if (ProviderManageBLL.ProviderNameIsExist(name) > 0)
            {
                return GetTran("000000", "供应商名称已存在！");
            }
            else
            {
                return "";
            }
        }
        else
        {
            if (ProviderManageBLL.ProviderNameIsExist(Convert.ToInt32(num), name) > 0)
            {
                return GetTran("000000", "供应商名称已存在！");
            }
            else
            {
                return "";
            }
        }
    }

    /// <summary>
    /// 获取国家产品列表  ---DS2012
    /// </summary>
    /// <param name="countryCode"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string GetProductMenu(string countryCode)
    {
        ProductTree myTree = new ProductTree();
        string va = myTree.GetCountryProduct(countryCode);
        return va;
    }
    /// <summary>
    /// 存储入库产品列表  ---DS2012
    /// </summary>
    /// <param name="countryCode"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public void GetstorageList(string ProId, string proName)
    {
        ArrayList list;

        string ProName = proName;

        if (Session["storageList"] != null && Session["storageList"].ToString() != "")
        {
            list = (ArrayList)Session["storageList"];
            int s = 0;
            InventoryDocDetailsModel md = new InventoryDocDetailsModel();
            md.ProductQuantity = 1;
            //md.Pici = "0";
            md.MeasureUnit = "0";
            md.UnitPrice = ProductModeBLL.GetProductPriceByID(Convert.ToInt32(ProId));
            foreach (InventoryDocDetailsModel memberDetailsModel in list)
            {

                if (memberDetailsModel.ProductID == Convert.ToInt32(ProId))
                {
                    memberDetailsModel.ProductQuantity = memberDetailsModel.ProductQuantity + 1;

                    s = 1;
                }

            }
            md.ProductID = Convert.ToInt32(ProId);

            if (s != 1)
            {
                list.Add(md);
            }
        }
        else
        {
            list = new ArrayList();

            InventoryDocDetailsModel md = new InventoryDocDetailsModel();

            md.ProductID = Convert.ToInt32(ProId);
            md.ProductQuantity = 1;
            //md.Pici = "0";
            md.MeasureUnit = "0";
            md.UnitPrice = ProductModeBLL.GetProductPriceByID(Convert.ToInt32(ProId));

            list.Add(md);
        }
        Session["storageList"] = list;

    }
    /// <summary>
    /// 存储赠送产品列表  ---DS2012
    /// </summary>
    /// <param name="countryCode"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public void GetGiveProductList(string ProId, string proName)
    {
        ArrayList list;

        string ProName = proName;

        if (Session["giveProductList"] != null && Session["giveProductList"].ToString() != "")
        {
            list = (ArrayList)Session["giveProductList"];
            int s = 0;
            GiveProductModel md = new GiveProductModel();
            md.ProductQuantity = 1;

            foreach (GiveProductModel giveProductModel in list)
            {

                if (giveProductModel.productId == Convert.ToInt32(ProId))
                {
                    giveProductModel.ProductQuantity = giveProductModel.ProductQuantity + 1;

                    s = 1;
                }

            }
            md.productId = Convert.ToInt32(ProId);

            if (s != 1)
            {
                list.Add(md);
            }
        }
        else
        {
            list = new ArrayList();

            GiveProductModel md = new GiveProductModel();

            md.productId = Convert.ToInt32(ProId);
            md.ProductQuantity = 1;
            list.Add(md);
        }
        Session["giveProductList"] = list;

    }
    /// <summary>
    /// 更新入库单产品数量  ---DS2012
    /// </summary>
    /// <param name="num"></param>
    /// <param name="proid"></param>
    [AjaxPro.AjaxMethod]
    public void EnstorageList(int num, string proid)
    {
        ArrayList list;
        string ProId = proid;

        if (Session["storageList"] != null && Session["storageList"].ToString() != "")
        {
            list = (ArrayList)Session["storageList"];


            int s = 0;
            InventoryDocDetailsModel md = new InventoryDocDetailsModel();
            md.ProductQuantity = 1;
            //md.Pici = "0";
            md.MeasureUnit = "0";
            md.UnitPrice = ProductModeBLL.GetProductPriceByID(Convert.ToInt32(ProId));
            foreach (InventoryDocDetailsModel memberDetailsModel in list)
            {
                if (memberDetailsModel.ProductID == Convert.ToInt32(ProId))
                {
                    if (num == 0)
                    {
                        list.Remove(memberDetailsModel);
                    }
                    else
                    {
                        memberDetailsModel.ProductQuantity = num;
                        s = 1;
                    }

                }
            }
            md.ProductID = Convert.ToInt32(ProId);

            if (s == 0)
            {
                list.Add(md);
            }
        }
        else
        {
            list = new ArrayList();

            InventoryDocDetailsModel md = new InventoryDocDetailsModel();

            md.ProductID = Convert.ToInt32(ProId);
            md.ProductQuantity = 1;
            //md.Pici = "0";
            md.MeasureUnit = "0";
            md.UnitPrice = ProductModeBLL.GetProductPriceByID(Convert.ToInt32(ProId));

            list.Add(md);
        }
        Session["storageList"] = list;
    }
    /// <summary>
    /// 更新赠送产品数量  ---DS2012
    /// </summary>
    /// <param name="num"></param>
    /// <param name="proid"></param>
    [AjaxPro.AjaxMethod]
    public void EnGiveProductList(int num, string proid)
    {
        ArrayList list;
        string ProId = proid;
        if (Session["giveProductList"] != null && Session["giveProductList"].ToString() != "")
        {
            list = (ArrayList)Session["giveProductList"];
            int s = 0;
            GiveProductModel md = new GiveProductModel();
            md.ProductQuantity = 1;

            foreach (GiveProductModel giveProductModel in list)
            {

                if (giveProductModel.productId == Convert.ToInt32(ProId))
                {
                    giveProductModel.ProductQuantity = num;

                    s = 1;
                }

            }
            md.productId = Convert.ToInt32(ProId);

            if (s != 1)
            {
                list.Add(md);
            }
        }
        else
        {
            list = new ArrayList();

            GiveProductModel md = new GiveProductModel();

            md.productId = Convert.ToInt32(ProId);
            md.ProductQuantity = 1;
            list.Add(md);
        }
        Session["giveProductList"] = list;
    }
    /// <summary>
    /// 更新入库单产品批次
    /// </summary>
    /// <param name="num"></param>
    /// <param name="proid"></param>
    [AjaxPro.AjaxMethod]
    public void EnstorageListPi(string num, string proid)
    {
        ArrayList list;
        string ProId = proid;

        if (Session["storageList"] != null && Session["storageList"].ToString() != "")
        {
            list = (ArrayList)Session["storageList"];


            int s = 0;
            InventoryDocDetailsModel md = new InventoryDocDetailsModel();
            md.ProductQuantity = 1;
            //md.Pici = "0";
            md.MeasureUnit = "0";
            md.UnitPrice = ProductModeBLL.GetProductPriceByID(Convert.ToInt32(ProId));
            //foreach (InventoryDocDetailsModel memberDetailsModel in list)
            //{
            //    if (memberDetailsModel.ProductID == Convert.ToInt32(ProId))
            //    {
            //        memberDetailsModel.Pici = num;
            //        s = 1;
            //    }
            //}
            md.ProductID = Convert.ToInt32(ProId);

            if (s == 0)
            {
                list.Add(md);
            }
        }
        else
        {
            list = new ArrayList();

            InventoryDocDetailsModel md = new InventoryDocDetailsModel();

            md.ProductID = Convert.ToInt32(ProId);
            md.ProductQuantity = 1;
            //md.Pici = "0";
            md.MeasureUnit = "0";
            md.UnitPrice = ProductModeBLL.GetProductPriceByID(Convert.ToInt32(ProId));

            list.Add(md);
        }
        Session["storageList"] = list;
    }
    /// <summary>
    /// 更新入库单产品批次
    /// </summary>
    /// <param name="num"></param>
    /// <param name="proid"></param>
    [AjaxPro.AjaxMethod]
    public void EnstorageListBig(string num, string proid)
    {
        ArrayList list;
        string ProId = proid;

        if (Session["storageList"] != null && Session["storageList"].ToString() != "")
        {
            list = (ArrayList)Session["storageList"];


            int s = 0;
            InventoryDocDetailsModel md = new InventoryDocDetailsModel();
            md.ProductQuantity = 1;
            //md.Pici = "0";
            md.MeasureUnit = "0";
            md.UnitPrice = ProductModeBLL.GetProductPriceByID(Convert.ToInt32(ProId));
            foreach (InventoryDocDetailsModel memberDetailsModel in list)
            {
                if (memberDetailsModel.ProductID == Convert.ToInt32(ProId))
                {
                    memberDetailsModel.MeasureUnit = num;
                    s = 1;
                }
            }
            md.ProductID = Convert.ToInt32(ProId);

            if (s == 0)
            {
                list.Add(md);
            }
        }
        else
        {
            list = new ArrayList();

            InventoryDocDetailsModel md = new InventoryDocDetailsModel();

            md.ProductID = Convert.ToInt32(ProId);
            md.ProductQuantity = 1;
            //md.Pici = "0";
            md.MeasureUnit = "0";
            md.UnitPrice = ProductModeBLL.GetProductPriceByID(Convert.ToInt32(ProId));

            list.Add(md);
        }
        Session["storageList"] = list;
    }
    /// <summary>
    /// 更新入库单产品批次
    /// </summary>
    /// <param name="num"></param>
    /// <param name="proid"></param>
    [AjaxPro.AjaxMethod]
    public void EnstorageListPrice(string num, string proid)
    {
        ArrayList list;
        string ProId = proid;

        if (Session["storageList"] != null && Session["storageList"].ToString() != "")
        {
            list = (ArrayList)Session["storageList"];


            int s = 0;
            InventoryDocDetailsModel md = new InventoryDocDetailsModel();
            md.ProductQuantity = 1;
            //md.Pici = "0";
            md.MeasureUnit = "0";
            md.UnitPrice = ProductModeBLL.GetProductPriceByID(Convert.ToInt32(ProId));
            foreach (InventoryDocDetailsModel memberDetailsModel in list)
            {
                if (memberDetailsModel.ProductID == Convert.ToInt32(ProId))
                {
                    memberDetailsModel.UnitPrice = Convert.ToDouble(num);
                    s = 1;
                }
            }
            md.ProductID = Convert.ToInt32(ProId);

            if (s == 0)
            {
                list.Add(md);
            }
        }
        else
        {
            list = new ArrayList();

            InventoryDocDetailsModel md = new InventoryDocDetailsModel();

            md.ProductID = Convert.ToInt32(ProId);
            md.ProductQuantity = 1;
            //md.Pici = "0";
            md.MeasureUnit = "0";
            md.UnitPrice = ProductModeBLL.GetProductPriceByID(Convert.ToInt32(ProId));

            list.Add(md);
        }
        Session["storageList"] = list;
    }
    /// <summary>
    /// 绑定产品列表 ---DS2012
    /// </summary>
    /// <param name="curr"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string BindstorageList(string curr)
    {
        return DataBindCar("tablemb", "", Convert.ToInt32(curr));
    }

    /// <summary>
    /// 绑定增品列表 ---DS2012
    /// </summary>
    /// <param name="curr"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string BindsGiveProductList(string curr)
    {
        return DataBindGiveCar("tablemb", "", Convert.ToInt32(curr));
    }

    /// <summary>
    /// 获取产品信息带库存
    /// </summary>
    /// <param name="productid">产品ID</param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string GetProductDetailIsCount(string productid)
    {
        DataTable dt = CommonDataBLL.GetProductById(productid);

        string count = DBHelper.ExecuteScalar("select sum(totalin-totalout) as pcount from ProductQuantity where productid=" + productid).ToString(); ;

        string html = " <table width=\"300\" border=\"0\" cellpadding=\"0\" cellspacing=\"4\" class=\"bjkk\">";

        html += "<tr><td width=\"105\"><img src=\"../ReadImage.aspx?ProductID=" + dt.Rows[0]["ProductID"].ToString() + "\" style=\"width:105px;height:145px;\" /> </td>";
        html += "<td valign=\"top\"><table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" bgcolor=\"#FFFFFF\" class=\"biaozzi\">";
        html += "<tr><td width=\"40\" align=\"right\" valign=\"top\" nowrap>" + BLL.Translation.TranslateFromDB("004193") + "：</td><td><span class=\"bzbt\"> " + dt.Rows[0]["ProductName"].ToString() + " </span></td></tr>";//产品名称
        html += "<tr><td width=\"40\" align=\"right\" valign=\"top\" nowrap>" + BLL.Translation.TranslateFromDB("002084") + "：</td> <td  valign=\"top\">" + dt.Rows[0]["PreferentialPrice"].ToString() + " </td> </tr>";//优惠价格
        html += "<tr><td width=\"40\" align=\"right\" valign=\"top\" nowrap>" + BLL.Translation.TranslateFromDB("000414") + "：</td> <td  valign=\"top\">" + dt.Rows[0]["PreferentialPV"].ToString() + " </td> </tr>";//优惠价格
        html += "<tr><td width=\"40\" align=\"right\" valign=\"top\" nowrap>" + BLL.Translation.TranslateFromDB("007144") + "：</td> <td  valign=\"top\">" + count + " </td> </tr>";//优惠价格
        if (dt.Rows[0]["Description"].ToString().Length > 30)
            html += "<tr><td width=\"40\" align=\"right\" valign=\"top\">" + BLL.Translation.TranslateFromDB("000628") + "：</td> <td class=\"smbiaozzi\" title='" + dt.Rows[0]["Description"].ToString() + "'> <textarea class=\"biaozzi\" style=\"width:100%;height:100%;border:0;overflow: hidden;\">" + dt.Rows[0]["Description"].ToString().Substring(0, 30) + "... </textarea></td></tr>";
        else
            html += "<tr><td width=\"40\" align=\"right\" valign=\"top\">" + BLL.Translation.TranslateFromDB("000628") + "：</td> <td class=\"smbiaozzi\">" + dt.Rows[0]["Description"].ToString() + " </td></tr>";
        html += "</table></td></tr></table>";
        return html;
    }

    /// <summary>
    /// 获取选择产品列表 入库  ---DS2012
    /// </summary>
    /// <param name="strPid"></param>
    /// <param name="storeID"></param>
    /// <param name="productids"></param>
    /// <param name="cla"></param>
    /// <param name="clr"></param>
    /// <param name="curr"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string DataBindCar(string cla, string clr, int countryCode)
    {

        double currency = CurrencyDAL.GetRate(countryCode);
        string selectcu = CurrencyDAL.GetJCByCountry(countryCode);
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        try
        {
            double price = 0.00;
            double pv = 0.00;
            double zongJine = 0.00;
            double zongPv = 0.00;

            sb.Append("<table  border=0 align=center style='width:100%' cellpadding=0 cellspacing=1 class=" + cla + " style='width:100%;'>");
            sb.Append("<tr>");
            sb.Append("<th align=center  bgcolor=" + clr + " ><b>&nbsp;&nbsp;" + BLL.Translation.Translate("000558", "产品编号") + "&nbsp;&nbsp;</b></th>");
            sb.Append("<th align=center  bgcolor=" + clr + " ><b>&nbsp;&nbsp;" + BLL.Translation.Translate("000501", "产品名称") + "&nbsp;&nbsp;</b></th>");
            sb.Append("<th align=center bgcolor=" + clr + " ><b>&nbsp;&nbsp;" + BLL.Translation.Translate("000503", "单价") + "&nbsp;&nbsp;</b></th>");
            sb.Append("<th align=center bgcolor=" + clr + " ><b>&nbsp;&nbsp;" + BLL.Translation.Translate("007144", "库存") + "&nbsp;&nbsp;</b></th>");
            sb.Append("<th align=center bgcolor=" + clr + " ><b>&nbsp;&nbsp;" + BLL.Translation.Translate("007689", "单位") + "&nbsp;&nbsp;</b></th>");
            sb.Append("<th align=center bgcolor=" + clr + " ><b>&nbsp;&nbsp;" + BLL.Translation.Translate("000505", "数量") + "&nbsp;&nbsp;</b></th>");
            sb.Append("<th align=center bgcolor=" + clr + " ><b>&nbsp;&nbsp;" + BLL.Translation.Translate("002065", "进货单价") + "&nbsp;&nbsp;</b></th>");
            sb.Append("<th align=center bgcolor=" + clr + " ><b>&nbsp;&nbsp;" + BLL.Translation.Translate("000322", "金额") + "&nbsp;&nbsp;</b></th>");
            sb.Append("<th align=center bgcolor=" + clr + " ><b>&nbsp;&nbsp;" + BLL.Translation.Translate("000414", "积分") + "&nbsp;&nbsp;</b></th>");
            //sb.Append("<th align=center bgcolor=" + clr + " ><b>&nbsp;&nbsp;" + BLL.Translation.Translate("000658", "批次") + "&nbsp;&nbsp;</b></th>");
            // sb.Append("<th align=center bgcolor=" + clr + " ><b>&nbsp;&nbsp;生产日期&nbsp;&nbsp;</b></th>");
            sb.Append("<th align=center bgcolor=" + clr + " ><b>&nbsp;&nbsp;" + BLL.Translation.Translate("007688", "有效期") + "&nbsp;&nbsp;</b></th>");
            sb.Append("<th align=center bgcolor=" + clr + " ><b>&nbsp;&nbsp;&nbsp;" + BLL.Translation.Translate("002167", "入库日期") + "&nbsp;&nbsp;&nbsp;</b></th>");
            sb.Append("</tr>");
            string dateNow = DateTime.Now.ToString("yyyy-MM-dd");

            int n = 0;
            if (Session["storageList"] != null && Session["storageList"].ToString() != "")
            {
                ArrayList list = (ArrayList)Session["storageList"];
                foreach (InventoryDocDetailsModel md in list)
                {
                    int productid = md.ProductID;
                    SqlDataReader dr = DBHelper.ExecuteReader("GetMoreProductInfoByProductid", new SqlParameter("@productid", productid), CommandType.StoredProcedure);
                    //InventoryDocDetailsModel md = (InventoryDocDetailsModel)ht[productid];
                    int iCount = int.Parse(md.ProductQuantity.ToString());  //产品数量
                    if (dr.Read())
                    {
                        string pName = dr["productName"].ToString();                //产品名称

                        if (md.MeasureUnit == "0")
                        {
                            price = md.UnitPrice * iCount;
                            pv = Convert.ToDouble(dr["PreferentialPV"]) * iCount;
                        }
                        else
                        {
                            price = md.UnitPrice * iCount * Convert.ToInt32(dr["BigSmallMultiPle"]);
                            pv = Convert.ToDouble(dr["PreferentialPV"]) * iCount * Convert.ToInt32(dr["BigSmallMultiPle"]);
                            md.MeasureUnit = dr["BigSmallMultiPle"].ToString();
                        }

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
                        sb.Append("<td align=\"center\">" + dr["productcode"].ToString() + "</td>");
                        sb.Append("<td align=\"center\">" + pName.ToString() + "</td>");
                        sb.Append("<td align=\"right\">" + (Convert.ToDouble(dr["PreferentialPrice"]) / currency).ToString("0.00") + "</td>");
                        sb.Append("<td align=\"right\">" + dr["pcount"].ToString() + "</td>");

                        if (md.MeasureUnit == "0")
                        {
                            sb.Append("<td align=\"left\"><input type=\"checkbox\" onclick='checkbig(this," + productid + ")' />" + dr["BigUnitName"].ToString() + "(×" + dr["BigSmallMultiPle"].ToString() + ")</td>");
                        }
                        else
                        {
                            sb.Append("<td align=\"left\"><input type=\"checkbox\" checked=\"checked\" onclick='checkbig(this," + productid + ")' />" + dr["BigUnitName"].ToString() + "(×" + dr["BigSmallMultiPle"].ToString() + ")</td>");
                        }
                        sb.Append("<td align=\"center\"><input style='width:50px;' onblur='EnShopp(this," + productid + ");' value='" + iCount + "' type='text'></input></td>");
                        sb.Append("<td align=\"center\"><input style='width:50px;' onblur='EnShoppPrice(this," + productid + ");' value='" + md.UnitPrice + "' type='text'></input></td>");
                        sb.Append("<td align=\"right\">" + (price / currency).ToString("0.00") + "</td>");
                        sb.Append("<td align=\"right\">" + pv.ToString() + "</td>");
                        //sb.Append("<td align=\"center\"><input style='width:50px;' type='text' value='" + md.Pici + "' onblur='EnShoppPi(this," + productid + ");' ></input></td>");
                        // sb.Append("<td align=\"center\"><input style='width:80px;' type='text' id='txt_MFD_DT_" + productid + "' value='' onchange=\"EnMFD_DT(this," + productid + ")\"  CssClass='Wdate' onfocus='WdatePicker()'/></td>");
                        string exp_dt = md.EffectiveDate == null ? "" : Convert.ToDateTime(md.EffectiveDate).ToString("yyyy-MM-dd");
                        string PickerString = "<input style='width:80px;' type='text' id='txt_Exp_DT_" + productid + "'  value='" + exp_dt + "' onchange=\"EnExp_DT(this," + productid + ")\" CssClass='Wdate' onclick='WdatePicker()'/>";
                        sb.Append("<td align=\"center\">" + PickerString + "</td>");
                        sb.Append("<td align=\"center\">" + dateNow + "</td>");
                        sb.Append("</tr>");
                        n++;
                    }
                    md.PV = Convert.ToDouble(dr["PreferentialPV"]);
                    dr.Close();
                }
                Session["storageList"] = list;
            }//+ "(" + selectcu + ")
            sb.Append("<tr bgColor=\"#F1F4F8\">");
            sb.Append("<td colspan=\"6\" align=\"center\" class=\"biaozzi\" style=\"color:red;\" ><b>" + BLL.Translation.Translate("000247", "总计") + "：</b></td>");
            sb.Append("<td align=\"right\" class=\"xhzi\" style=\"color:red;\">" + (zongJine / currency).ToString("0.00") + "</td>");
            sb.Append("<td align=\"right\" class=\"xhzi\" style=\"color:red;\">" + zongPv.ToString() + "</td>");
            sb.Append("<td colspan=\"1\" align=\"center\">&nbsp;</td>");
            sb.Append("<td colspan=\"1\" align=\"center\">&nbsp;</td>");
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
    /// 获取选择产品列表 赠送产品  ---DS2012
    /// </summary>
    /// <param name="strPid"></param>
    /// <param name="storeID"></param>
    /// <param name="productids"></param>
    /// <param name="cla"></param>
    /// <param name="clr"></param>
    /// <param name="curr"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string DataBindGiveCar(string cla, string clr, int countryCode)
    {

        double currency = CurrencyDAL.GetRate(countryCode);
        string selectcu = CurrencyDAL.GetJCByCountry(countryCode);
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        try
        {
            double price = 0.00;
            double pv = 0.00;
            double zongJine = 0.00;
            double zongPv = 0.00;

            sb.Append("<table  border=0 align=center style='width:100%' cellpadding=0 cellspacing=1 class=" + cla + " style='width:100%;'>");
            sb.Append("<tr>");
            sb.Append("<th align=center  bgcolor=" + clr + " ><b>&nbsp;&nbsp;" + BLL.Translation.Translate("000558", "产品编号") + "&nbsp;&nbsp;</b></th>");
            sb.Append("<th align=center  bgcolor=" + clr + " ><b>&nbsp;&nbsp;" + BLL.Translation.Translate("000501", "产品名称") + "&nbsp;&nbsp;</b></th>");
            sb.Append("<th align=center bgcolor=" + clr + " ><b>&nbsp;&nbsp;" + BLL.Translation.Translate("000322", "金额") + "&nbsp;&nbsp;</b></th>");
            sb.Append("<th align=center bgcolor=" + clr + " ><b>&nbsp;&nbsp;" + BLL.Translation.Translate("000414", "积分") + "&nbsp;&nbsp;</b></th>");
            sb.Append("<th align=center bgcolor=" + clr + " ><b>&nbsp;&nbsp;" + BLL.Translation.Translate("000505", "数量") + "&nbsp;&nbsp;</b></th>");
            sb.Append("<th align=center bgcolor=" + clr + " ><b>&nbsp;&nbsp;" + BLL.Translation.Translate("000000", "总金额") + "&nbsp;&nbsp;</b></th>");
            sb.Append("<th align=center bgcolor=" + clr + " ><b>&nbsp;&nbsp;" + BLL.Translation.Translate("000000", "总积分") + "&nbsp;&nbsp;</b></th>");
            sb.Append("</tr>");
            string dateNow = DateTime.Now.ToString("yyyy-MM-dd");

            int n = 0;
            if (Session["giveProductList"] != null && Session["giveProductList"].ToString() != "")
            {
                ArrayList list = (ArrayList)Session["giveProductList"];
                foreach (GiveProductModel md in list)
                {
                    int productid = md.productId;
                    SqlDataReader dr = DBHelper.ExecuteReader("GetMoreProductInfoByProductid", new SqlParameter("@productid", productid), CommandType.StoredProcedure);
                    //InventoryDocDetailsModel md = (InventoryDocDetailsModel)ht[productid];
                    int iCount = int.Parse(md.ProductQuantity.ToString());  //产品数量
                    if (dr.Read())
                    {
                        string pName = dr["productName"].ToString();                //产品名称
                        md.Price = Convert.ToDouble(dr["PreferentialPrice"]);
                        md.PV = Convert.ToDouble(dr["PreferentialPV"]);
                        price = Convert.ToDouble(dr["PreferentialPrice"]) * iCount;
                        pv = Convert.ToDouble(dr["PreferentialPV"]) * iCount;
                        md.TotalPrice = price;
                        md.TotalPV = pv;
                        //zongJine += price;
                        //zongPv += pv;

                        if (n % 2 == 0)
                        {
                            sb.Append("<tr bgColor=\"#F1F4F8\" onmouseover=\"bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC';\" onmouseout=\"this.style.backgroundColor=bg;\" >");
                        }
                        else
                        {
                            sb.Append("<tr onmouseover=\"bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC';\" onmouseout=\"this.style.backgroundColor=bg;\" >");
                        }
                        sb.Append("<td align=\"center\">" + dr["productcode"].ToString() + "</td>");
                        sb.Append("<td align=\"center\">" + pName.ToString() + "</td>");
                        sb.Append("<td align=\"right\">" + (Convert.ToDouble(dr["PreferentialPrice"]) / currency).ToString("0.00") + "</td>");
                        sb.Append("<td align=\"right\">" + (Convert.ToDouble(dr["PreferentialPv"]) / currency).ToString("0.00") + "</td>");
                        sb.Append("<td align=\"center\"><input style='width:50px;' onblur='EnShopp(this," + productid + ");' value='" + iCount + "' type='text'></input></td>");
                        sb.Append("<td align=\"right\">" + price.ToString() + "</td>");
                        sb.Append("<td align=\"right\">" + pv.ToString() + "</td>");
                        sb.Append("</tr>");
                        n++;
                    }
                    md.PV = Convert.ToDouble(dr["PreferentialPV"]);
                    dr.Close();
                }
                Session["giveProductList"] = list;
            }
            sb.Append("</table>");
        }
        catch (Exception ex)
        {
            sb.Append(ex.Message.ToString());
        }
        return sb.ToString();
    }

    [AjaxPro.AjaxMethod]
    public string GetDetailsTable(string orderid, string storeid)
    {
        IList<OrderDetailModel> list = OrdersBrowseBLL.GetDetails(orderid, storeid);
        StringBuilder builder = new StringBuilder();
        builder.Append("<table border=0 align=center style='width:100%' cellpadding=0 cellspacing=0 class='tablemb bordercss'><thead><tr>");
        builder.Append("<th>" + GetTran("000079", "订单号") + "</th>");
        builder.Append("<th>" + GetTran("000895", "商品名称") + "</th>");
        builder.Append("<th>" + GetTran("000898", "商品数量") + "</th>");
        builder.Append("<th>" + GetTran("000900", "商品单价") + "</th>");
        builder.Append("<th>" + GetTran("000902", "商品积分") + "</th></tr></thead> <tbody>");
        foreach (OrderDetailModel orderDetail in list)
        {

            builder.Append("<tr onmouseover=\" bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC';\" onmouseout=\"this.style.backgroundColor=bg;\" ><td>" + orderid + "</td>");
            builder.Append("<td>" + orderDetail.ProductName + "</td>");
            builder.Append("<td>" + orderDetail.Quantity + "</td>");
            builder.Append("<td>" + orderDetail.Price + "</td>");
            builder.Append("<td>" + orderDetail.Pv + "</td> </tr>");
        }
        builder.Append("</tbody></table>");

        return builder.ToString();

    }

    /// <summary>
    /// 绑定产品列表 ---DS2012
    /// </summary>
    /// <param name="curr"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string BindReporeDamage(string curr, string WareHouseID, string DepotSeat)
    {
        return DataBindReporeDamage("tablemb", "", Convert.ToInt32(curr), WareHouseID, DepotSeat);
    }
    /// <summary>
    /// 获取选择产品列表 入库  ---DS2012
    /// </summary>
    /// <param name="strPid"></param>
    /// <param name="storeID"></param>
    /// <param name="productids"></param>
    /// <param name="cla"></param>
    /// <param name="clr"></param>
    /// <param name="curr"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string DataBindReporeDamage(string cla, string clr, int countryCode, string WareHouseID, string DepotSeat)
    {

        double currency = CurrencyDAL.GetRate(countryCode);
        string selectcu = CurrencyDAL.GetJCByCountry(countryCode);
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        try
        {
            double price = 0.00;
            double pv = 0.00;
            double zongJine = 0.00;
            double zongPv = 0.00;

            sb.Append("<table  border=0 align=center style='width:100%' cellpadding=0 cellspacing=1 class=" + cla + " style='width:100%;'>");
            sb.Append("<tr>");
            sb.Append("<th align=center  bgcolor=" + clr + " ><b>&nbsp;&nbsp;" + BLL.Translation.Translate("000558", "产品编号") + "&nbsp;&nbsp;</b></th>");
            sb.Append("<th align=center  bgcolor=" + clr + " ><b>&nbsp;&nbsp;" + BLL.Translation.Translate("000501", "产品名称") + "&nbsp;&nbsp;</b></th>");
            sb.Append("<th align=center bgcolor=" + clr + " ><b>&nbsp;&nbsp;" + BLL.Translation.Translate("000503", "单价") + "&nbsp;&nbsp;</b></th>");
            sb.Append("<th align=center bgcolor=" + clr + " ><b>&nbsp;&nbsp;" + BLL.Translation.Translate("000414", "积分") + "&nbsp;&nbsp;</b></th>");
            sb.Append("<th align=center bgcolor=" + clr + " ><b>&nbsp;&nbsp;" + BLL.Translation.Translate("007144", "库存") + "&nbsp;&nbsp;</b></th>");
            sb.Append("<th align=center bgcolor=" + clr + " ><b>&nbsp;&nbsp;" + BLL.Translation.Translate("000518", "单位") + "&nbsp;&nbsp;</b></th>");
            sb.Append("<th align=center bgcolor=" + clr + " ><b>&nbsp;&nbsp;" + BLL.Translation.Translate("000505", "数量") + "&nbsp;&nbsp;</b></th>");
            sb.Append("</tr>");

            int n = 0;
            if (Session["storageList"] != null && Session["storageList"].ToString() != "")
            {
                ArrayList list = (ArrayList)Session["storageList"];
                foreach (InventoryDocDetailsModel md in list)
                {
                    int productid = md.ProductID;
                    SqlParameter[] paras = new SqlParameter[]{
                        new SqlParameter("@Country",countryCode),
                        new SqlParameter("@DepotSeatID",DepotSeat),
                        new SqlParameter("@WareHouseID",WareHouseID),
                        new SqlParameter("@productid",productid),
                    };
                    SqlDataReader dr = DBHelper.ExecuteReader("GetProductInfoQueantity", paras, CommandType.StoredProcedure);
                    int iCount = int.Parse(md.ProductQuantity.ToString());  //产品数量
                    if (dr.Read())
                    {
                        string pName = dr["productName"].ToString();                //产品名称

                        if (md.MeasureUnit == "0")
                        {
                            price = md.UnitPrice * iCount;
                            pv = Convert.ToDouble(dr["PreferentialPV"]) * iCount;
                        }
                        else
                        {
                            price = md.UnitPrice * iCount * Convert.ToInt32(dr["BigSmallMultiPle"]);
                            pv = Convert.ToDouble(dr["PreferentialPV"]) * iCount * Convert.ToInt32(dr["BigSmallMultiPle"]);
                            md.MeasureUnit = dr["BigSmallMultiPle"].ToString();
                        }

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
                        sb.Append("<td align=\"center\">" + dr["productcode"].ToString() + "</td>");
                        sb.Append("<td align=\"center\">" + pName.ToString() + "</td>");
                        sb.Append("<td align=\"right\">" + (Convert.ToDouble(dr["PreferentialPrice"]) / currency).ToString("0.00") + "</td>");
                        sb.Append("<td align=\"right\">" + (Convert.ToDouble(dr["PreferentialPv"]) / currency).ToString("0.00") + "</td>");
                        sb.Append("<td align=\"right\">" + dr["pcount"].ToString() + "</td>");
                        sb.Append("<td align=\"left\"><input type=\"checkbox\" disabled=\"disabled\" onclick='checkbig(this," + productid + ")' />" + dr["BigProductUnitName"].ToString() + "(×" + dr["BigSmallMultiple"].ToString() + ")</td>");
                        sb.Append("<td align=\"center\"><input style='width:50px;' onblur='EnShopp(this," + productid + ");' value='" + iCount + "' type='text'></input></td>");
                        sb.Append("</tr>");
                        n++;
                    }
                    md.PV = Convert.ToDouble(dr["PreferentialPV"]);
                    dr.Close();
                }
                Session["storageList"] = list;
            }
            sb.Append("</table>");
        }
        catch (Exception ex)
        {
            sb.Append(ex.Message.ToString());
        }
        return sb.ToString();
    }


    /// <summary>
    /// 生产日期 
    /// </summary>
    /// <param name="num"></param>
    /// <param name="proid"></param>
    [AjaxPro.AjaxMethod]
    public void EnstorageListMFD_DT(string value, string proid)
    {//xyc
        ArrayList list;
        string ProId = proid;
        if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(ProId))
            return;
        if (Session["storageList"] != null && Session["storageList"].ToString() != "")
        {
            list = (ArrayList)Session["storageList"];


            int s = 0;
            InventoryDocDetailsModel md = new InventoryDocDetailsModel();
            md.ProductQuantity = 1;
            //md.Pici = "0";
            md.MeasureUnit = "0";
            md.UnitPrice = ProductModeBLL.GetProductPriceByID(Convert.ToInt32(ProId));
            //foreach (InventoryDocDetailsModel memberDetailsModel in list)
            //{
            //    if (memberDetailsModel.ProductID == Convert.ToInt32(ProId))
            //    {
            //        memberDetailsModel.MFD_DT = DateTime.Parse(value);
            //        s = 1;
            //    }
            //}
            md.ProductID = Convert.ToInt32(ProId);

            if (s == 0)
            {
                list.Add(md);
            }
        }
        else
        {
            list = new ArrayList();

            InventoryDocDetailsModel md = new InventoryDocDetailsModel();

            md.ProductID = Convert.ToInt32(ProId);
            md.ProductQuantity = 1;
            //md.Pici = "0";
            md.MeasureUnit = "0";
            md.UnitPrice = ProductModeBLL.GetProductPriceByID(Convert.ToInt32(ProId));

            list.Add(md);
        }
        Session["storageList"] = list;
    }

    /// <summary>
    /// 有效期
    /// </summary>
    /// <param name="num"></param>
    /// <param name="proid"></param>
    [AjaxPro.AjaxMethod]
    public void EnstorageListExp_DT(string value, string proid)
    {//xyc
        ArrayList list;
        string ProId = proid;
        if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(ProId))
            return;
        if (Session["storageList"] != null && Session["storageList"].ToString() != "")
        {
            list = (ArrayList)Session["storageList"];


            int s = 0;
            InventoryDocDetailsModel md = new InventoryDocDetailsModel();
            md.ProductQuantity = 1;
            //md.Pici = "0";
            md.MeasureUnit = "0";
            md.UnitPrice = ProductModeBLL.GetProductPriceByID(Convert.ToInt32(ProId));
            foreach (InventoryDocDetailsModel memberDetailsModel in list)
            {
                if (memberDetailsModel.ProductID == Convert.ToInt32(ProId))
                {
                    memberDetailsModel.EffectiveDate = DateTime.Parse(value);
                    s = 1;
                }
            }
            md.ProductID = Convert.ToInt32(ProId);

            if (s == 0)
            {
                list.Add(md);
            }
        }
        else
        {
            list = new ArrayList();

            InventoryDocDetailsModel md = new InventoryDocDetailsModel();

            md.ProductID = Convert.ToInt32(ProId);
            md.ProductQuantity = 1;
            //md.Pici = "0";
            md.MeasureUnit = "0";
            md.UnitPrice = ProductModeBLL.GetProductPriceByID(Convert.ToInt32(ProId));

            list.Add(md);
        }
        Session["storageList"] = list;
    }

    [AjaxPro.AjaxMethod]
    public string getjinbaolevelstr(string number)
    {
        string res = "";
        string sqsltr = "select  petname ,b.levelstr,b.levelint from memberinfo m  ,BSCO_level b  where m.levelint = b.levelint and b.levelflag=0 and m.number=@number  ";
        SqlParameter[] sps = new SqlParameter[] { new SqlParameter("@number", number) };
        DataTable dt = DBHelper.ExecuteDataTable(sqsltr, sps, CommandType.Text);
        string jb = dt.Rows[0]["levelint"].ToString();
        if (dt != null && dt.Rows.Count > 0)
        {
            res = dt.Rows[0]["petname"].ToString() + "," + dt.Rows[0]["levelstr"];
        }
        //return res;

        //string s = MemberInfoDAL.Getmembernameandlevestr(number);
        string s = res;

        if (s != "")
        {
            string[] sss = s.Split(',');
            if (sss.Length > 1)
                res = sss[0] + "  " + sss[1];
        }
        return res;
    }


    /// <summary>
    /// 获取昵称等级
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string getpetnamelevelstr(string number)
    {
        string res = "";
        string sqsltr = "select  petname ,b.levelstr,b.levelint from memberinfo m  ,BSCO_level b  where m.levelint = b.levelint and b.levelflag=0 and m.number=@number  ";
        SqlParameter[] sps = new SqlParameter[] { new SqlParameter("@number", number) };
        DataTable dt = DBHelper.ExecuteDataTable(sqsltr, sps, CommandType.Text);
        string jb = dt.Rows[0]["levelint"].ToString();
        if (dt != null && dt.Rows.Count > 0)
        {
            res = dt.Rows[0]["petname"].ToString() + "," + (jb == "0" ? GetTran("006899", "世联会员") : jb == "1" ? GetTran("004255", "世联普商") : jb == "2" ? GetTran("004258", "世联咨商") : jb == "3" ? GetTran("005219", "世联特别咨商") : jb == "4" ? GetTran("005222", "世联高级咨商") : GetTran("005224", "世联全面咨商"));
        }
        //return res;

        //string s = MemberInfoDAL.Getmembernameandlevestr(number);
        string s = res;

        if (s != "")
        {
            string[] sss = s.Split(',');
            if (sss.Length > 1)
                res = sss[0] + "  " + sss[1];
        }
        return res;
    }
    /// <summary>
    /// 获取产品的销售价和积分
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string getProductMoneyPv(string productid, int id)
    {
        string res = "0,0";
        DataTable dt = DBHelper.ExecuteDataTable("select preferentialprice* (select rate from Currency where ID=" + id + ") as preferentialprice ,preferentialpv from product where productid=" + productid);
        if (dt.Rows.Count > 0)
        {
            res = dt.Rows[0]["preferentialprice"].ToString().ToString() + "," + dt.Rows[0]["preferentialpv"].ToString();
        }
        return res;
    }

    /// <summary>
    /// 获取会员姓名
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string GetName(string number)
    {
        string str = "";
        string sql = "select name from memberinfo where number=@number";
        SqlParameter par = new SqlParameter("@number", number);
        object name = DAL.DBHelper.ExecuteScalar(sql, par, CommandType.Text);
        if (name != null)
        {
            str = Encryption.Encryption.GetDecipherName(name.ToString());
        }
        return str;
    }

    /// <summary>
    /// 获取昵称
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string GetPetName(string number, string type)
    {
        string str = "";
        if (type == "Member")
        {
            string sql = "select Name from memberinfo where MobileTele=@number";
            SqlParameter par = new SqlParameter("@number", number);
            object name = DAL.DBHelper.ExecuteScalar(sql, par, CommandType.Text);
            if (name != null)
            {
                //str = Encryption.Encryption.GetDecipherName(name.ToString());
                str = name.ToString();
            }

        }
        else if (type == "Store")
        {
            string sql = "select storename from storeinfo where storeid=@number";
            SqlParameter par = new SqlParameter("@number", number);
            object name = DAL.DBHelper.ExecuteScalar(sql, par, CommandType.Text);
            if (name != null)
            {
                //str = Encryption.Encryption.GetDecipherName(name.ToString());
                str = name.ToString();
            }
        }
        return str;
    }

    /// <summary>
    /// 判断一个编号是否在另一个编号安置网络下面
    /// </summary>
    /// <param name="topNumber"></param>
    /// <param name="number"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string CheckNumberNet(string topNumber, string number)
    {
        int xuhao = new RegistermemberBLL().GetXuHao(topNumber);
        return new RegistermemberBLL().GetError1(number, xuhao.ToString());
    }


    /// <summary>
    /// 判断一个编号是否在另一个编号安置网络下面-安置
    /// </summary>
    /// <param name="topNumber"></param>
    /// <param name="number"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string CheckNumberNetAn(string topNumber, string number)
    {
        int ret = 0;
        int QiShu = (int)DAL.CommonDataDAL.GetMaxExpect();
        SqlParameter[] para ={new SqlParameter("@number",number),
                             new SqlParameter("@numberTop",topNumber),
                             new SqlParameter("@qishu",QiShu),
                             new SqlParameter("@ret",ret)
                            };
        para[3].Direction = ParameterDirection.Output;
        DBHelper.ExecuteNonQuery("p_CheckAn", para, CommandType.StoredProcedure);
        ret = Convert.ToInt32(para[3].Value);
        if (ret >= 0)
        {
            return "";
        }
        else
        {
            return "-1";
        }
    }

    /// <summary>
    /// 判断一个编号是否在另一个编号安置网络下面-推荐
    /// </summary>
    /// <param name="topNumber"></param>
    /// <param name="number"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string CheckNumberNetTui(string topNumber, string number)
    {
        int ret = 0;
        int QiShu = (int)DAL.CommonDataDAL.GetMaxExpect();
        SqlParameter[] para ={new SqlParameter("@number",number),
                             new SqlParameter("@numberTop",topNumber),
                             new SqlParameter("@qishu",QiShu),
                             new SqlParameter("@ret",ret)
                            };
        para[3].Direction = ParameterDirection.Output;
        DBHelper.ExecuteNonQuery("p_CheckTui", para, CommandType.StoredProcedure);
        ret = Convert.ToInt32(para[3].Value);
        if (ret >= 0)
        {
            return "";
        }
        else
        {
            return "-1";
        }
    }

    [AjaxPro.AjaxMethod]
    public string GetGradingLevel(string number, string type, string operaterNum)
    {
        try
        {
            if (number != "" && type != "")
            {
                return DataBind(number, type, operaterNum);
            }
            else
            {
                return ("0,无,无");
            }
        }
        catch { return ("0,无,无"); }
    }


    [AjaxPro.AjaxMethod]
    public string Getsxf(string money)
    {
        try
        {
            var sxf = Convert.ToDouble(BLL.CommonClass.CommonDataBLL.GetLeftsxfMoney(money)).ToString("f2");
            return sxf;
        }
        catch { return ("0"); }
    }


    [AjaxPro.AjaxMethod]
    public string Getsxf1(string money)
    {
        try
        {
            var sxf = Convert.ToDouble(BLL.CommonClass.CommonDataBLL.GetLefwyjMoney(money)).ToString("f2");
            return sxf;
        }
        catch { return ("0"); }
    }

    private string DataBind(string number, string type, string company)
    {
        string ret = "-3,无,无";
        if (number == "")
        {
            ret = "-1,无,无";//编号不能为空
        }
        else
        {
            if (type == "Member")
            {
                DataTable dt = CommonDataBLL.GetBalanceLevel(number);
                if (dt.Rows.Count == 0)
                {
                    ret = "-2,无,无";//编号不存在
                }
                else
                {
                    int i = Convert.ToInt32(dt.Rows[0]["level"].ToString());
                    string HowMuch = DAL.DBHelper.ExecuteScalar("select isnull(levelstr,'无') as levelstr from bsco_level where levelint='" + i.ToString() + "' and levelflag=0").ToString();
                    string name = BLL.other.Company.StoreRegisterBLL.GetMemberName(number);
                    ret = i.ToString() + "," + HowMuch + "," + name;
                }
            }
            else if (type == "Store")
            {
                /*DataTable dt = CommonDataBLL.GetLevel("StoreInfo", number);
                if (dt.Rows.Count == 0)
                {
                    ret = "-2" + ",无";
                }
                else
                {
                    string howMuch = null;
                    int i = Convert.ToInt32(dt.Rows[0]["StoreLevelInt"].ToString());
                    howMuch = DAL.DBHelper.ExecuteScalar("select isnull(levelstr,'无') as levelstr from bsco_level where levelint='" + i.ToString() + "' and levelflag=1").ToString();
                    string name = DAL.DBHelper.ExecuteScalar("select isnull(name,'无') as name from storeinfo where storeid='" + number + "'").ToString();
                    ret = i.ToString() + "," + howMuch + "," + name;
                }*/
                DataTable dt = DAL.DBHelper.ExecuteDataTable("select isnull(name,'无') as name,StoreLevelInt,case StoreLevelInt when -1 then '无' else isnull(levelstr,'无') end as levelstr from storeinfo a left join bsco_level b on a.StoreLevelInt=b.levelint where a.storeid=@storeid", new SqlParameter[]{
                new SqlParameter("@storeid", number)
                }, CommandType.Text);
                if (dt != null && dt.Rows.Count == 0)
                {
                    ret = "-2" + ",无";
                }
                else
                {
                    if (dt.Rows[0]["StoreLevelInt"].ToString() == "-1")
                        ret = "0," + dt.Rows[0]["levelstr"].ToString() + "," + dt.Rows[0]["name"].ToString();
                    else
                        ret = dt.Rows[0]["StoreLevelInt"].ToString() + "," + dt.Rows[0]["levelstr"].ToString() + "," + dt.Rows[0]["name"].ToString();
                }
            }
            else
            {
                ret = "-3,无,无";
            }
        }
        return ret;
    }


    [AjaxPro.AjaxMethod]
    public void getsession(string index)
    {
        HttpContext.Current.Session["page"] = index;
    }
    [AjaxPro.AjaxMethod]
    public void gettopsession(string index)
    {
        HttpContext.Current.Session["pagetop"] = index;
    }
    [AjaxPro.AjaxMethod]
    public string gettopsession2()
    {
        return HttpContext.Current.Session["pagetop"].ToString();
    }

    /// <summary>
    /// 奖金查询
    /// </summary>
    /// <param name="isact">显示期数（默认为20）</param>
    /// <param name="pageindex"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string BasicSearch(int isact, int pageindex)
    {
        if (pageindex == 2)
            return "";
        string number = "";
        string curstr = "";
        string jiesuan = "";
        int pgsize = 20;
        int isjiesuan = 0;
        int length = 0;
        if (HttpContext.Current.Session["Member"] != null)
        {
            number = HttpContext.Current.Session["Member"].ToString();
        }
        string sql = @"select max(expectNum) from config";
        var maxqs = Convert.ToInt32(DBHelper.ExecuteScalar(sql));

        if (isact == -1)
        {
            length = maxqs;
        }
        else
        {
            if (isact > maxqs)
            {
                length = maxqs;
            }
            else if (isact < maxqs)
            {
                length = isact;
            }
            else
            {
                length = isact;
            }
        }
        var memberExpectNum = Common.GetMemberExpectNum(number);//会员注册期数
        if (memberExpectNum != 0)
        {
            for (int i = memberExpectNum; i <= length; i++)
            {
                string sqls = @"SELECT m.bonus0,m.TotalNetRecord,m.CurrentTotalNetRecord,CurrentSolidSend FROM memberinfobalance" + i + " m WHERE Number=@Number";
                SqlParameter[] para = {
                                            new SqlParameter("@Number",SqlDbType.VarChar),

                                        };
                para[0].Value = number;

                DataTable dtt = DAL.DBHelper.ExecuteDataTable(sqls, para, CommandType.Text);

                isjiesuan = (int)DBHelper.ExecuteScalar("Select jsflag from config where ExpectNum=" + i);
                if (isjiesuan == 1)
                {
                    jiesuan = "已结算";
                }
                else
                {
                    jiesuan = "未结算";
                }
                foreach (DataRow item in dtt.Rows)
                {
                    curstr += @"<li><a href='yejiDetail.aspx?qs=" + i + "'>";
                    curstr += @"<span class='glyphicon glyphicon-signal blue'></span><div class='ctinfo1'> 第" + i + "期</div><div class='ctinfo2'>" + Convert.ToDouble(item["CurrentSolidSend"]).ToString("0.00") + "</div><div class='ctinfo3'><p class='p1'>" + jiesuan + "</p></div>";
                    curstr += @"<div class='ctinfo4'><a class='glyphicon glyphicon-ok-sign blue' href='yejiDetail.aspx?qs=" + i + "' >详细</a></div></a></li>";
                }
            }
        }

        //for (int i = 1; i <= length; i++)
        //{
        //    string sqls = @"SELECT m.bonus0,m.TotalNetRecord,m.CurrentTotalNetRecord,CurrentSolidSend FROM memberinfobalance" + i + " m WHERE Number=@Number";
        //    SqlParameter[] para = {
        //                                    new SqlParameter("@Number",SqlDbType.VarChar),

        //                                };
        //    para[0].Value = number;

        //    DataTable dtt = DAL.DBHelper.ExecuteDataTable(sqls, para, CommandType.Text);

        //    isjiesuan = (int)DBHelper.ExecuteScalar("Select jsflag from config where ExpectNum=" + i);
        //    if (isjiesuan == 1)
        //    {
        //        jiesuan = "已结算";
        //    }
        //    else
        //    {
        //        jiesuan = "未结算";
        //    }
        //    foreach (DataRow item in dtt.Rows)
        //    {
        //        curstr += @"<li><a href='yejiDetail.aspx?qs=" + i + "'>";
        //        curstr += @"<span class='glyphicon glyphicon-signal blue'></span><div class='ctinfo1'> 第" + i + "期</div><div class='ctinfo2'>" + Convert.ToDouble(item["CurrentSolidSend"]).ToString("0.00") + "</div><div class='ctinfo3'><p class='p1'>" + jiesuan + "</p></div>";
        //        curstr += @"<div class='ctinfo4'><a class='glyphicon glyphicon-ok-sign blue' href='yejiDetail.aspx?qs=" + i + "' >详细</a></div></a></li>";
        //    }
        //}
        return curstr;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="isact">(近一个月30，近三个月90，全部)</param>
    /// <param name="pageindex"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string BasicSearchs(int isact, int pageindex)
    {
        var jiesuan = "";
        int maxqs = 0;//总共几期
        var number = "";//会员编号
        var curstr = new StringBuilder("");//HTML
        int pgsize = 20;//每页显示20
        if (HttpContext.Current.Session["Member"] != null)
        {
            number = HttpContext.Current.Session["Member"].ToString();
        }
        else
        {
            return curstr.ToString();
        }
        var maxvalue = DBHelper.ExecuteScalar("select max(expectNum) from config");
        if (maxvalue != null)
        {
            maxqs = Convert.ToInt32(maxvalue);
        }
        else
        {
            return curstr.ToString();
        }
        for (int i = 1; i <= maxqs; i++)
        {
            SqlParameter[] para = {
                                            new SqlParameter("@Number",SqlDbType.VarChar),

                                        };

            SqlParameter[] insertpara = {
                                          new SqlParameter("@bonus0",SqlDbType.Decimal),
                                          new SqlParameter("@TotalNetRecord",SqlDbType.Decimal),
                                          new SqlParameter("@CurrentTotalNetRecord",SqlDbType.Decimal),
                                          new SqlParameter("@CurrentSolidSend",SqlDbType.Decimal),
                                          new SqlParameter("@qs",SqlDbType.Int),
                                          new SqlParameter("@number",SqlDbType.VarChar),
                                          new SqlParameter("@isjiesuan",SqlDbType.Int),


                                      };

            var isjiesuan = (int)DBHelper.ExecuteScalar("Select isSuance from config where ExpectNum=" + i);

            string mmsql = @"SELECT m.bonus0,m.TotalNetRecord,m.CurrentTotalNetRecord,CurrentSolidSend FROM memberinfobalance" + i + " m WHERE Number=@Number";

            para[0].Value = number;

            DataTable mmdtt = DAL.DBHelper.ExecuteDataTable(mmsql, para, CommandType.Text);

            foreach (DataRow item in mmdtt.Rows)
            {

                insertpara[0].Value = item["bonus0"].ToString();
                insertpara[1].Value = Convert.ToDecimal(item["TotalNetRecord"]);
                insertpara[2].Value = Convert.ToDecimal(item["CurrentTotalNetRecord"]);
                insertpara[3].Value = Convert.ToDecimal(item["CurrentSolidSend"]);
                insertpara[4].Value = Convert.ToDecimal(item["qs"]);
                insertpara[5].Value = number;
                insertpara[6].Value = isjiesuan;
            }

            var insert = @"INSERT INTO [FA2018].[dbo].[jjchaxun]
           ([bonus0]
           ,[TotalNetRecord]
           ,[CurrentTotalNetRecord]
           ,[CurrentSolidSend]
           ,[qs]
           ,[number]
           ,[isjiesuan])
     VALUES
           (@bonus0
           ,@TotalNetRecord
           ,@CurrentTotalNetRecord
           ,@CurrentSolidSend
           ,@qs
           ,@number
           ,@isjiesuan)";

            try
            {
                var insertvalue = DBHelper.ExecuteNonQuery(insert, insertpara, CommandType.Text);
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        string sqls = @"WITH  jjcx as(
	select m.bonus0,m.TotalNetRecord,m.CurrentTotalNetRecord,m.CurrentSolidSend,m.qs,m.isjiesuan,m.number
	ROW_NUMBER() OVER(ORDER BY m.CurrentSolidSend asc ) as rowNum FROM
	jjchaxun m )
  SELECT * from jjcx WHERE rowNum BETWEEN " + ((pageindex - 1) * pgsize + 1) + " AND " + pageindex * pgsize + "";

        DataTable dtt = DAL.DBHelper.ExecuteDataTable(sqls, CommandType.Text);

        foreach (DataRow item in dtt.Rows)
        {
            jiesuan = Convert.ToInt32(item["qs"]) == 1 ? "已结算" : "未结算";
            curstr.Append(@"<li><span class='glyphicon glyphicon-signal blue'></span>
<div class='ctinfo1'> 第" + Convert.ToInt32(item["qs"]) + "期</div><div class='ctinfo2'>" + Convert.ToDouble(item["CurrentSolidSend"]).ToString("0.00") + "</div><div class='ctinfo3'><p class='p1'>" + jiesuan + "</p></div><div class='ctinfo4'><a class='glyphicon glyphicon-ok-sign blue' href='yejiDetail.aspx?qs=" + Convert.ToInt32(item["qs"]) + "'>详细</a></div></li>");
        }
        return curstr.ToString();
    }

    [AjaxPro.AjaxMethod]
    public string yejiDetail(int pageindex, int qs)
    {
        string number = "";
        string curstr = "";
        int pgsize = 20;
        if (HttpContext.Current.Session["Member"] != null)
        {
            number = HttpContext.Current.Session["Member"].ToString();
        }
        string sqls = @"WITH  mx as(
	select m.hybh,m.xjbh,bdbh,bonus,qs,orderid,
	ROW_NUMBER() OVER(ORDER BY m.bonus asc ) as rowNum FROM
	mx0 m where hybh=@Number and qs=@qs)
  SELECT * from mx WHERE rowNum BETWEEN " + ((pageindex - 1) * pgsize + 1) + " AND " + pageindex * pgsize + "";

        SqlParameter[] para = {
                                      new SqlParameter("@Number",SqlDbType.VarChar),
                                      new SqlParameter("@qs",SqlDbType.Int),
                                  };
        para[0].Value = number;
        para[1].Value = qs;
        DataTable dtt = DAL.DBHelper.ExecuteDataTable(sqls, para, CommandType.Text);

        if (pageindex == 1 && dtt.Rows.Count > 0)
        {
            //            curstr += @"<li><span class='glyphicon glyphicon-th-list blue'></span>
            //<div class='ctinfo1'>报单编号</div><div class='ctinfo2'>奖励</div></li>";
        }
        foreach (DataRow item in dtt.Rows)
        {
            //< span class='glyphicon glyphicon-signal blue'></span>
            curstr += @"<li>
<div class='ctinfo1'>" + item["xjbh"] + "</div><div class='ctinfo4'>" + item["bdbh"] + "</div><div class='ctinfo3'>" + Convert.ToDouble(item["bonus"]).ToString("0.00") + "</div></li>";
        }
        return curstr;
    }
    /// <summary>
    /// 订单浏览页面数据加载
    /// </summary>
    /// <param name="isact"></param>
    /// <param name="pageindex"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string BrowseMemberOrders(int isact, int pageindex)
    {
        string curstr = "";
        string cdit = "";
        int pgsize = 20;
        string direct = HttpContext.Current.Session["Member"].ToString();

        if (isact != -1) cdit += " and  mo.DefrayState=" + isact;
        string sqls = @"WITH sss AS(  
   SELECT  m.Petname,m.number,m.Direct ,m.MobileTele,RegisterDate,m.Name,mo.TotalMoney,m.MemberState,mo.DefrayState,mo.OrderID  ,ROW_NUMBER() OVER(ORDER BY  m.id desc ) AS rowNum FROM memberinfo m left join memberorder mo on m.number=mo.number where (m.Direct='" + direct + "' or m.Assister='"+direct+"')  and mo.IsAgain=0  AND ordertype in(11,21,31)  " + cdit + "   ) SELECT * FROM sss WHERE rowNum BETWEEN " + ((pageindex - 1) * pgsize + 1) + " AND " + pageindex * pgsize + "";


        DataTable dtt = DAL.DBHelper.ExecuteDataTable(sqls);
        foreach (DataRow item in dtt.Rows)
        {
            int ispay = Convert.ToInt32(item["DefrayState"]);
            if (ispay == 0)
            {
                curstr += " <li><span class='glyphicon glyphicon-user greay'></span><div class='ctinfo'> " + GetsubString(item["Petname"].ToString(), 4) + "</div><div class='ctinfo2'> <p class='p1'>" + item["number"].ToString() + "</p><p>" + item["MobileTele"].ToString() + "</p></div><div class='ctinfo3'>&yen;" + Convert.ToDouble(item["TotalMoney"]).ToString("0.00") + "</div> <a   href='../payserver/chosepaysjpay.aspx?blif=" + EncryKey.GetEncryptstr(item["orderid"].ToString(), 1, 1) + "' class='btn  btn-danger '> 激活</a></li>";
            }
            else if (ispay == 1)
            {
                curstr += " <li><span class='glyphicon glyphicon-user blue'></span><div class='ctinfo'> " + GetsubString(item["Petname"].ToString(), 4) + "</div><div class='ctinfo2'> <p  class='p1'>" + item["number"].ToString() + "</p><p>" + item["MobileTele"].ToString() + "</p></div><div class='ctinfo3'>&yen;" + Convert.ToDouble(item["TotalMoney"]).ToString("0.00") + "</div> <a class='glyphicon glyphicon-ok-sign blue'   >正常</a></li>";
            }
        }
        return curstr;

    }


    /// <summary>
    /// 订单浏览页面数据加载
    /// </summary>
    /// <param name="isact"></param>
    /// <param name="pageindex"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string MemberOrdersTZ()
    {

        string curstr = "";
        int pgsize = 20;
        int pageindex = 1;
        string direct = HttpContext.Current.Session["Member"].ToString();


        string sqls = @"WITH sss AS(  
   SELECT    Number,OrderID,TotalMoney,DefrayState,OrderDate,InvestJB,PriceJB,Totalpv,ROW_NUMBER() OVER(ORDER BY OrderDate desc, id desc ) AS rowNum FROM  memberorder where Number='" + direct + "'  and ordertype in(13,23,33,11,21,31)  ) SELECT * FROM sss WHERE rowNum BETWEEN " + ((pageindex - 1) * pgsize + 1) + " AND " + pageindex * pgsize + "";


        DataTable dtt = DAL.DBHelper.ExecuteDataTable(sqls);
        foreach (DataRow item in dtt.Rows)
        {
            int ispay = Convert.ToInt32(item["DefrayState"]);
            if (ispay == 0)
            {
                curstr += "<tr  onclick=\"location.href='../payserver/chosepaysjpay.aspx?blif=" + EncryKey.GetEncryptstr(item["OrderID"].ToString(), 1, 1) + "'\"><td>" + Convert.ToDateTime(item["OrderDate"]).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString("yy-MM-dd HH:mm:ss") + "</td><td><span style=''>&yen;" + Convert.ToDouble(item["TotalMoney"]).ToString("0.00") + "</span></br><span style=''>" + Convert.ToDouble(item["Totalpv"]).ToString("0.00") + "</span></td><td><a class='glyphicon glyphicon-exclamation-sign' >未支付</a></td>";
            }
            else if (ispay == 1)
            {
                curstr += "<tr><td>" + Convert.ToDateTime(item["OrderDate"]).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString("yy-MM-dd HH:mm:ss") + "</td><td><span style=''>&yen;" + Convert.ToDouble(item["TotalMoney"]).ToString("0.00") + "</span></br><span style=''>" + Convert.ToDouble(item["Totalpv"]).ToString("0.00") + "</span></td><td><a class='glyphicon glyphicon-ok' style='color: gray;'  >已支付</a></td>";
            }
        }
        return curstr;

    }
    public string GetsubString(string str, int len)
    {
        if (str.Length > len) return str.Substring(0, len) + "..";
        else return str;
    }

    /// <summary>
    /// 账户明细页面数据加载
    /// </summary>
    /// <param name="isact"></param>
    /// <param name="pageindex"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string AccountDetailOrders(int isact, int pageindex, string q1)
    {
       
        string curstr = "";
        string cdit = "";
        int pgsize = 10;
        if (HttpContext.Current.Session["Member"] != null)
        {
            string direct = HttpContext.Current.Session["Member"].ToString();
            if (q1 == "AccountFZ")
            {
                cdit += " and  SfType=4";
            }
            if (q1 == "AccountFX")
            {
                cdit += " and  SfType=2";
            }
            if (q1 == "AccountXJ")
            {
                cdit += " and  SfType=1";
            }
            if (q1 == "AccountXF")
            {
                cdit += " and  SfType=6";
            }
            if (q1 == "AccountFXth")
            {
                cdit += " and  SfType=5";
            }
            if (isact != -1) cdit += " and  Direction=" + isact;
            string sqls = @"WITH sss AS(SELECT id,number,HappenTime,KmType,HappenMoney,BalanceMoney,Direction,ROW_NUMBER() OVER(ORDER BY HappenTime desc, id desc ) as rowNum FROM memberaccount where number='" + direct + "'  " + cdit + "   ) SELECT * FROM sss WHERE rowNum BETWEEN " + ((pageindex - 1) * pgsize + 1) + " AND " + pageindex * pgsize + "";


            DataTable dtt = DAL.DBHelper.ExecuteDataTable(sqls);
            foreach (DataRow item in dtt.Rows)
            {
                int ispay = Convert.ToInt32(item["Direction"]);
                if (ispay == 0)
                {
                    curstr += "<tr onclick=\"location.href='Zhxiangxi.aspx?id=" + Convert.ToString(item["id"]).ToString() + "'\"><td>" + Convert.ToDateTime(item["HappenTime"]).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString("yy-MM-dd") + "</br>" + Convert.ToDateTime(item["HappenTime"]).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString("HH:mm:ss") + "</td><td> " + BLL.Logistics.D_AccountBLL.GetKmtype(Convert.ToString(item["KmType"]).ToString()) + "</td><td><span style='color: #dd4814;float:right;'>+" + Convert.ToDouble(item["HappenMoney"]).ToString("0.0000") + "</span></br><span style='float:right;'>" + Convert.ToDouble(item["BalanceMoney"]).ToString("0.0000") + "</span></td></tr>";
                }
                else if (ispay == 1)
                {
                    curstr += "<tr onclick=\"location.href='Zhxiangxi.aspx?id=" + Convert.ToString(item["id"]).ToString() + "'\"><td>" + Convert.ToDateTime(item["HappenTime"]).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString("yy-MM-dd") + "</br>" + Convert.ToDateTime(item["HappenTime"]).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString("HH:mm:ss") + "</td><td> " + BLL.Logistics.D_AccountBLL.GetKmtype(Convert.ToString(item["KmType"]).ToString()) + "</td><td><span  style='color: #4caf50;float:right;'>-" + Convert.ToDouble(item["HappenMoney"]).ToString("0.0000") + "</span></br><span style='float:right;'>" + Convert.ToDouble(item["BalanceMoney"]).ToString("0.0000") + "</span></td></tr>";
                }
            }
        }
        return curstr;

    }


    /// <summary>
    /// 扫码支付页面数据加载
    /// </summary>
    /// <param name="isact"></param>
    /// <param name="pageindex"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string H5Orders(int pageindex)
    {

        string curstr = "";
        
        int pgsize = 10;
        
           
             

                string sqls = @"WITH sss AS(SELECT id,mobile,je,ewm,zhifu,isno,ROW_NUMBER() OVER(ORDER BY id desc ) as rowNum FROM H5saoma where isno=0   ) SELECT * FROM sss WHERE rowNum BETWEEN " + ((pageindex - 1) * pgsize + 1) + " AND " + pageindex * pgsize + "";


            DataTable dtt = DAL.DBHelper.ExecuteDataTable(sqls);
            foreach (DataRow item in dtt.Rows)
            {

                curstr += "<div id=\"qud" + Convert.ToString(item["id"]).ToString() + "\" ></div><div class=\"caption\" contenteditable=\"true\"><h3>支付金额：" + Convert.ToString(item["je"]).ToString() + "</h3><p><h3>手机号：" + Convert.ToString(item["mobile"]).ToString() + "</h3><p><h3>支付方式：" + Convert.ToString(item["zhifu"]).ToString() + "</h3><p><a class=\"btt\" onclick=\"shengc('qud" + Convert.ToString(item["id"]).ToString() + "','" + Convert.ToString(item["ewm"]).ToString() + "');\">查看</a><a class=\"btt\" onclick=\"zhifuwancheng(" + Convert.ToString(item["id"]).ToString() + ");\" >完成</a> </p></div>";
               
            }
        
        return curstr;

    }

    /// <summary>
    /// 扫码支付页面数据加载
    /// </summary>
    /// <param name="isact"></param>
    /// <param name="pageindex"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string H5Ordout(int pageindex)
    {

        string curstr = "";

        SqlTransaction tran = null;
        SqlConnection conn = DAL.DBHelper.SqlCon();
        conn.Open();
        tran = conn.BeginTransaction();
        try
        {
            string sql = "update H5saoma set isno=1 where ID='" + pageindex + "'";
            int cg = DBHelper.ExecuteNonQuery(tran, sql);

           if (cg > 0)
            {
                tran.Commit();
                conn.Close();
                curstr = "1";
            }
            else
            {
                tran.Rollback();
                conn.Close();
                curstr = "0";
            }
        }
        catch (Exception e)
        {

            conn.Close();
            curstr = e.Message;
        }

        return curstr;

    }



    /// <summary>
    /// 转账页面数据加载
    /// </summary>
    /// <param name="isact"></param>
    /// <param name="pageindex"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string TransferListOrders(int isact, int pageindex)
    {
        string curstr = "";
        string cdit = "";
        int pgsize = 10;
        if (HttpContext.Current.Session["Member"] != null)
        {
            string direct = HttpContext.Current.Session["Member"].ToString();

            if (isact != -1)
            {
                if (isact == 0)
                {
                    cdit += " and  OutNumber!='" + direct+"'";
                }
                else {
                    cdit += " and  OutNumber='" + direct+"'";
                }
            } 
            string sqls = @"WITH sss AS(SELECT *,ROW_NUMBER() OVER(ORDER BY id desc ) as rowNum FROM ECTransferDetail where (outNumber='" + direct + "' or InNumber='" + direct + "') " + cdit + ") SELECT * FROM sss WHERE rowNum BETWEEN " + ((pageindex - 1) * pgsize + 1) + " AND " + pageindex * pgsize + "";


            DataTable dtt = DAL.DBHelper.ExecuteDataTable(sqls);
            foreach (DataRow item in dtt.Rows)
            {
                string ispay = Convert.ToString(item["OutNumber"]);
                if (ispay != direct)
                {
                    curstr += "<tr onclick=\"location.href='ZhuanZXX.aspx?id=" + Convert.ToString(item["id"]).ToString() + "'\"><td>" + Convert.ToDateTime(item

    ["Date"]).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString("yy-MM-dd HH:mm:ss") + "</td><td> " + BLL.CommonClass.CommonDataBLL.GetPetNameByNumber(Convert.ToString(item["InNumber"]).ToString()) + "</td><td style='color: #dd4814'>+"

    + Convert.ToDouble(item["OutMoney"]).ToString("0.00") + "</td></tr>";
                }
                else if (ispay == direct)
                {
                    curstr += "<tr onclick=\"location.href='ZhuanZXX.aspx?id=" + Convert.ToString(item["id"]).ToString() + "'\"><td>" + Convert.ToDateTime(item

    ["Date"]).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString("yy-MM-dd HH:mm:ss") + "</td><td> " + BLL.CommonClass.CommonDataBLL.GetPetNameByNumber(Convert.ToString(item["InNumber"]).ToString()) + "</td><td style='color: #4caf50'>-"

    + Convert.ToDouble(item["OutMoney"]).ToString("0.00") + "</td></tr>";
                }
            }
        }
        return curstr;

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

    /// <summary>
    /// 收货确认
    /// </summary>
    /// <param name="isact"></param>
    /// <param name="pageindex"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string shuohsuo(string orderid)
    {
        bool jg = false;
        if (orderid != "")
        {
            jg = new AffirmConsignBLL().Submit1(orderid);
        }
        return jg.ToString();
    }

    /// <summary>
    /// 订单浏览页面数据加载
    /// </summary>
    /// <param name="isact"></param>
    /// <param name="pageindex"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string MemberDetailOrders(string isact, int pageindex)
    {
        string curstr = "";
        string cdit = "";
        int pgsize = 10;
        if (HttpContext.Current.Session["Member"] != null)
        {
            string direct = HttpContext.Current.Session["Member"].ToString();

            if (isact == "0") { cdit += " and  a.DefrayState=" + isact; }
            if (isact != "0" && isact != "-1") { cdit += " and d.IsSent='" + isact+"'"; }
            string sqls = @"WITH sss AS(select a.Number,a.OrderID,b.Price,b.Pv,a.OrderDate,b.Quantity,b.ProductID,c.ProductImage,c.ProductName,a.DefrayState,d.IsSent,d.IsReceived,ROW_NUMBER() OVER(ORDER BY a.OrderDate desc,a.id desc  ) as rowNum from MemberOrder a left join MemberDetails b on a.OrderID=b.OrderID left join Product c on c.ProductID=b.ProductID left join StoreOrder d on a.OrderID=d.StoreOrderID where  a.Number='" + direct + "' and a.ordertype=22 and a.DefrayState>-2  " + cdit + "   ) SELECT * FROM sss WHERE rowNum BETWEEN " + ((pageindex - 1) * pgsize + 1) + " AND " + pageindex * pgsize + "";

            DataTable dtt = DAL.DBHelper.ExecuteDataTable(sqls);
            foreach (DataRow item in dtt.Rows)
            {
                string ispay = item["DefrayState"].ToString();
                if (ispay == "0")
                {
                    curstr += "<li><div class='aui-list-title-info'><a href='#' class='aui-well'><div class='aui-well-bd'>订单号：" + item["OrderID"].ToString() + "<br/>时  间：" + Convert.ToDateTime(item["OrderDate"]).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString() +
                        "</div><div class='aui-well-ft'>待付款</div></a><a href='ShowProductInfo.aspx?oT=3&rt=3&ty=1&ID=" + item["ProductID"] + "&type=ShopingXF' class='aui-list-product-fl-item'><div class='aui-list-product-fl-img'><img src='" + FormatURL(item["ProductID"]) +
                        "' alt=''></div><div class='aui-list-product-fl-text'><h3 class='aui-list-product-fl-title'>" + item["ProductName"].ToString() + "</h3><div class='aui-list-product-fl-mes'><div><span class='aui-list-product-item-price'>¥" + Convert.ToDecimal(item["Price"]).ToString("0.00") +
                        "</span><span class='aui-list-product-item-del-price' style='float: right;margin-top: 3px;'>" + item["Pv"].ToString() + "</span></div><div class='aui-btn-purchase'>x" + item["Quantity"].ToString() + "</div></div></div></a></div><div class='aui-list-title-btn'><a href='../payserver/chosepaysjpay.aspx?blif=" + EncryKey.GetEncryptstr(item["OrderID"].ToString(), 1, 1) +
                        "'class='red-color'>立即付款</a></div><div class='aui-dri'></div></li>";
                }
                else
                {
                    ispay = item["IsSent"].ToString();
                    if (ispay == "N")
                    {
                        curstr += "<li><div class='aui-list-title-info'><a href='#' class='aui-well'><div class='aui-well-bd'>订单号：" + item["OrderID"].ToString() + "<br/>时  间：" + Convert.ToDateTime(item["OrderDate"]).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString() +
                            "</div><div class='aui-well-ft'>待发货</div></a><a href='ShowProductInfo.aspx?oT=3&rt=3&ty=1&ID=" + item["ProductID"] + "&type=ShopingXF' class='aui-list-product-fl-item'><div class='aui-list-product-fl-img'><img src='" + FormatURL(item["ProductID"]) +
                            "' alt=''></div><div class='aui-list-product-fl-text'><h3 class='aui-list-product-fl-title'>" + item["ProductName"].ToString() + "</h3><div class='aui-list-product-fl-mes'><div><span class='aui-list-product-item-price'>¥" + Convert.ToDecimal(item["Price"]).ToString("0.00") +
                            "</span><span class='aui-list-product-item-del-price' style='float: right;margin-top: 3px;'>" + item["Pv"].ToString() + "</span></div><div class='aui-btn-purchase'>x" + item["Quantity"].ToString() + "</div></div></div></a></div><div class='aui-list-title-btn'><a href='#'class='hui-color'>已付款</a></div><div class='aui-dri'></div></li>";

                    }
                    if (ispay == "Y")
                    {
                        string IsReceived = item["IsReceived"].ToString();
                        if (IsReceived == "N")
                        {
                            curstr += "<li><div class='aui-list-title-info'><a href='#' class='aui-well'><div class='aui-well-bd'>订单号：" + item["OrderID"].ToString() + "<br/>时  间：" + Convert.ToDateTime(item["OrderDate"]).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString() +
                                 "</div><div class='aui-well-ft'>已发货</div></a><a href='ShowProductInfo.aspx?oT=3&rt=3&ty=1&ID=" + item["ProductID"] + "&type=ShopingXF' class='aui-list-product-fl-item'><div class='aui-list-product-fl-img'><img src='" + FormatURL(item["ProductID"]) +
                                 "' alt=''></div><div class='aui-list-product-fl-text'><h3 class='aui-list-product-fl-title'>" + item["ProductName"].ToString() + "</h3><div class='aui-list-product-fl-mes'><div><span class='aui-list-product-item-price'>¥" + Convert.ToDecimal(item["Price"]).ToString("0.00") +
                                 "</span><span class='aui-list-product-item-del-price' style='float: right;margin-top: 3px;'>" + item["Pv"].ToString() + "</span></div><div class='aui-btn-purchase'>x" + item["Quantity"].ToString() + "</div></div></div></a></div><div class='aui-list-title-btn'><a onclick=\"shouhuo('" + item["OrderID"].ToString() + "');\" class='red-color'>确认收货</a></div><div class='aui-dri'></div></li>";
                        }
                        else
                        {
                            curstr += "<li><div class='aui-list-title-info'><a href='#' class='aui-well'><div class='aui-well-bd'>订单号：" + item["OrderID"].ToString() + "<br/>时  间：" + Convert.ToDateTime(item["OrderDate"]).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString() +
                                    "</div><div class='aui-well-ft'>已收货</div></a><a href='ShowProductInfo.aspx?oT=3&rt=3&ty=1&ID=" + item["ProductID"] + "&type=ShopingXF' class='aui-list-product-fl-item'><div class='aui-list-product-fl-img'><img src='" + FormatURL(item["ProductID"]) +
                                    "' alt=''></div><div class='aui-list-product-fl-text'><h3 class='aui-list-product-fl-title'>" + item["ProductName"].ToString() + "</h3><div class='aui-list-product-fl-mes'><div><span class='aui-list-product-item-price'>¥" + Convert.ToDecimal(item["Price"]).ToString("0.00") +
                                    "</span><span class='aui-list-product-item-del-price' style='float: right;margin-top: 3px;'>" + item["Pv"].ToString() + "</span></div><div class='aui-btn-purchase'>x" + item["Quantity"].ToString() + "</div></div></div></a></div><div class='aui-list-title-btn'><a class='hui-color'>已收货</a></div><div class='aui-dri'></div></li>";

                        }
                    }
                }
            }
        }
        return curstr;

    }


    /// <summary>
    /// 发件箱页面数据加载
    /// </summary>
    /// <param name="isact"></param>
    /// <param name="pageindex"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string fajianxiangDetail(int pageindex)
    {
        string curstr = "";
        string cdit = "";
        if (HttpContext.Current.Session["Member"] != null)
        {
            int pgsize = 10;
            string direct = HttpContext.Current.Session["Member"].ToString();


            string sqls = @"WITH sss AS(select *,ROW_NUMBER() OVER(ORDER BY Senddate desc ) as rowNum from MessageSend where DropFlag=0 and SenderRole=2 and Sender='" + direct + "' and ID not in(select MessageID from dbo.F_DroppedSendByOperator('" + direct + "',2))) SELECT * FROM sss WHERE rowNum BETWEEN " + ((pageindex - 1) * pgsize + 1) + " AND " + pageindex * pgsize + "";


            DataTable dtt = DAL.DBHelper.ExecuteDataTable(sqls);
            foreach (DataRow item in dtt.Rows)
            {
                int ispay = Convert.ToInt32(item["ReadFlag"]);
                if (ispay == 0)
                {
                    curstr += "<li  onclick=\"location.href='fajianxx.aspx?id=" + item["ID"].ToString() + "'\"><div style='font-size: 12px;'><span style='float:left;'>接收对象：管理员</span><span style='float:right;'>" + Convert.ToDateTime(item["Senddate"]).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString() + "</span></div><div><span style='float:left;'>标题：" + Convert.ToString(item["InfoTitle"]).ToString() + "</span><Label style='color:blue'>未阅读</Label></div></li>";
                }
                else if (ispay == 1)
                {
                    curstr += "<li  onclick=\"location.href='fajianxx.aspx?id=" + item["ID"].ToString() + "'\"><div style='font-size: 12px;'><span style='float:left;'>接收对象：管理员</span><span style='float:right;'>" + Convert.ToDateTime(item["Senddate"]).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString() + "</span></div><div><span style='float:left;'>标题：" + Convert.ToString(item["InfoTitle"]).ToString() + "</span><Label>已阅读</Label></div></li>";

                }
            }
        }
        return curstr;

    }


    /// <summary>
    /// 废件箱页面数据加载
    /// </summary>
    /// <param name="isact"></param>
    /// <param name="pageindex"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string feijianxiangDetail(int pageindex)
    {
        string curstr = "";
        string cdit = "";
        int pgsize = 10;
        if (HttpContext.Current.Session["Member"] != null)
        {
            string direct = HttpContext.Current.Session["Member"].ToString();


            string sqls = @"WITH sss AS(select *,ROW_NUMBER() OVER(ORDER BY Senddate desc ) as rowNum from V_DroppedMessage where OperatorType=2 and Operator='" + direct + "') SELECT * FROM sss WHERE rowNum BETWEEN " + ((pageindex - 1) * pgsize + 1) + " AND " + pageindex * pgsize + "";


            DataTable dtt = DAL.DBHelper.ExecuteDataTable(sqls);
            foreach (DataRow item in dtt.Rows)
            {
                int ispay = Convert.ToInt32(item["ReadFlag"]);
                if (ispay == 0)
                {
                    curstr += "<li  onclick=\"location.href='feijianxx.aspx?id=" + item["ID"].ToString() + "'\"><div style='font-size: 12px;'><span style='float:left;'>接收对象：管理员</span><span style='float:right;'>" + Convert.ToDateTime(item["Senddate"]).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString() +

    "</span></div><div><span style='float:left;'>标题：" + Convert.ToString(item["InfoTitle"]).ToString() + "</span><Label style='color:blue'>未阅读</Label></div></li>";
                }
                else if (ispay == 1)
                {
                    curstr += "<li  onclick=\"location.href='feijianxx.aspx?id=" + item["ID"].ToString() + "'\"><div style='font-size: 12px;'><span style='float:left;'>接收对象：管理员</span><span style='float:right;'>" + Convert.ToDateTime(item["Senddate"]).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString() +

    "</span></div><div><span style='float:left;'>标题：" + Convert.ToString(item["InfoTitle"]).ToString() + "</span><Label>已阅读</Label></div></li>";

                }
            }
        }
        return curstr;

    }

    /// <summary>
    /// 收件箱页面数据加载
    /// </summary>
    /// <param name="isact"></param>
    /// <param name="pageindex"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string shoujianxiangDetail(int pageindex)
    {
        string curstr = "";
        string cdit = "";
        int pgsize = 10; if (HttpContext.Current.Session["Member"] != null)
        {
            string direct = HttpContext.Current.Session["Member"].ToString();


            string sqls = @"WITH sss AS(select *,ROW_NUMBER() OVER(ORDER BY Senddate desc ) as rowNum from MessageReceive where DropFlag=0   and (Receive like 

'%" + direct + "%' or Receive like '%*%') and MessageType='m' and ID not in(select MessageID from dbo.F_DroppedReceiveByOperator('" + direct + "',2))) SELECT * FROM sss WHERE rowNum BETWEEN " + ((pageindex - 1) * pgsize + 1) + " AND " + pageindex * pgsize + "";


            DataTable dtt = DAL.DBHelper.ExecuteDataTable(sqls);
            foreach (DataRow item in dtt.Rows)
            {
                int ispay = Convert.ToInt32(item["ReadFlag"]);
                if (ispay == 0)
                {
                    curstr += "<li  onclick=\"location.href='shoujianxx.aspx?id=" + item["ID"].ToString() + "'\"><div style='font-size: 12px;'><span style='float:left;'>发送人：管理员</span><span style='float:right;'>" + Convert.ToDateTime(item["Senddate"]).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString() +

    "</span></div><div><span style='float:left;'>标题：" + Convert.ToString(item["InfoTitle"]).ToString() + "</span><Label style='color:blue'>未阅读</Label></div></li>";
                }
                else if (ispay == 1)
                {
                    curstr += "<li  onclick=\"location.href='shoujianxx.aspx?id=" + item["ID"].ToString() + "'\"><div style='font-size: 12px;'><span style='float:left;'>发送人：管理员</span><span style='float:right;'>" + Convert.ToDateTime(item["Senddate"]).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString() +

    "</span></div><div><span style='float:left;'>标题：" + Convert.ToString(item["InfoTitle"]).ToString() + "</span><Label>已阅读</Label></div></li>";

                }
            }
        }
        return curstr;

    }


    /// <summary>
    /// 公告查阅页面数据加载
    /// </summary>
    /// <param name="isact"></param>
    /// <param name="pageindex"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string ddcyDetail(int pageindex)
    {
        string curstr = "";
        string cdit = "";
        if (HttpContext.Current.Session["Member"] != null)
        {
            int pgsize = 10;
            string direct = HttpContext.Current.Session["Member"].ToString();
            SqlConnection conn = new SqlConnection(DAL.DBHelper.connString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "MessageCheckCondition";
            conn.Open();
            string conditions = "DropFlag=0 and SenderRole=0 and Receive='*' and LoginRole=2 and MessageType='a'";

            string rtn = "";
            string idlist = "";
            //DataTable dt = DAL.DBHelper.ExecuteDataTable("select ID from MessageSend  join MessageReadCondition on MessageSend.ID=MessageReadCondition.MessageID   where " + conditions + "and  MessageReadCondition.ConditionLevel=(select levelint from memberinfo where number='" + Session["Member"].ToString() + "')");
            DataTable dt = DAL.DBHelper.ExecuteDataTable("select ID from MessageSend where " + conditions);
            foreach (DataRow row in dt.Rows)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@MessageSendID", SqlDbType.Int).Value = Convert.ToInt32(row["ID"]);
                cmd.Parameters.Add("@number", SqlDbType.VarChar).Value = direct;
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
            string sql1 = "SELECT ma.MessageID FROM dbo.MessageReadCondition AS ma,dbo.MessageSend AS me WHERE ma.MessageID=me.ID and DropFlag=0 and me.SenderRole=0 and me.Receive='*' and me.LoginRole=2 and (ma.ConditionLeader='" + direct + "' or ma.ConditionLeader='')";
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
            where = "ID in(" + idlist + ") and MessageType='a'";
        }
        else
        {
            where = "1=2";
        }
        string sqls = @"WITH sss AS(select *,ROW_NUMBER() OVER(ORDER BY Senddate desc ) as rowNum from MessageSend where " + conditions + " and " + where + ") SELECT * FROM sss WHERE rowNum BETWEEN " + ((pageindex - 1) * pgsize + 1) + " AND " + pageindex * pgsize + "";


        DataTable dtt = DAL.DBHelper.ExecuteDataTable(sqls);
        foreach (DataRow item in dtt.Rows)
        {
            int ispay = Convert.ToInt32(item["ReadFlag"]);
            if (ispay == 0)
            {
                curstr += "<li  onclick=\"location.href='ddcyxx.aspx?id=" + item["ID"].ToString() + "'\"><div style='font-size: 12px;'><span style='float:left;'>发送人：管理员</span><span style='float:right;'>" + Convert.ToDateTime(item["Senddate"]).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString() +
"</span></div><div><span style='float:left;'>标题：" + Convert.ToString(item["InfoTitle"]).ToString() + "</span><Label style='color:blue'>未阅读</Label></div></li>";
            }
            else if (ispay == 1)
            {
                curstr += "<li  onclick=\"location.href='ddcyxx.aspx?id=" + item["ID"].ToString() + "'\"><div style='font-size: 12px;'><span style='float:left;'>发送人：管理员</span><span style='float:right;'>" + Convert.ToDateTime(item["Senddate"]).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString() +
"</span></div><div><span style='float:left;'>标题：" + Convert.ToString(item["InfoTitle"]).ToString() + "</span><Label>已阅读</Label></div></li>";

            }
        }
        }
        return curstr;

    }




    /// <summary>
    /// 添加买入数据单
    /// </summary>
    /// <param name="Mcount"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public int AddRemittance(decimal Mcount)
    {
        if (Session["Member"] == null) return -1;

        string number = Session["Member"].ToString();

        string sqls = "select COUNT(0)  from Remittances  where  RemitNumber='" + number + "' and shenhestate in (0,1,11,20)  and DateDiff(dd,RemittancesDate,getutcdate())=0";
        int bc = Convert.ToInt32(DAL.DBHelper.ExecuteScalar(sqls));
        if (Mcount >5000)
        {
            return -3;  //单笔买入不能超过21000
        }
        if (bc >= 100)
        {
            return -2;  //当前还有未完成交易的买入单，不能继续交易
        }
        else
        {

            decimal sgprice = Common.GetnowPrice();

            RemittancesModel info = new RemittancesModel();
            info.InvestJB = Mcount;
            info.PriceJB = sgprice;
            info.ReceivablesDate = DateTime.UtcNow;
            info.RemittancesDate = DateTime.UtcNow;
            info.IsJL = 1;
            info.ImportBank = "";
            info.ImportNumber = "";
            info.name = "";
            info.RemittancesAccount = "";
            info.RemittancesBank = "";
            info.SenderID = "";
            info.Sender = "";
            info.RemitNumber = number;

            info.RemitMoney = Mcount * sgprice;//价格是根据购买石斛积分数量计算出来的
            info.StandardCurrency = 0;
            info.Use = 0; /*int.Parse(this.DeclarationType.SelectedValue)*/
            info.PayexpectNum = BLL.CommonClass.CommonDataBLL.getMaxqishu();
            info.Managers = number;
            info.ConfirmType = 0;
            info.Remark = "";
            info.RemittancesCurrency = int.Parse(Session["Default_Currency"].ToString());
            info.RemittancesMoney = Mcount * sgprice;//价格是根据购买石斛积分数量计算出来的
            info.OperateIp = CommonDataBLL.OperateIP;
            info.OperateNum = Session["Member"].ToString();

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

            int rc = RemittancesBLL.RemitDeclare(info, "1", "1");

            DataTable dt_one = DAL.DBHelper.ExecuteDataTable("select ID from remittances where RemittancesID='" + huidan + "'");
            int HkID = 0;
            if (dt_one != null && dt_one.Rows.Count > 0) HkID = Convert.ToInt32(dt_one.Rows[0]["ID"]);//汇款ID
            int bishu = 4;
      return HkID;
        }
  

    }

    /// <summary>
    /// 买入匹配大于0 说明匹配成功了
    /// </summary>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public int PiepeiRemittance(int hkid)
    {
        int num = 0;
        if (Session["Member"] == null) num = -1;
        string number = Session["Member"].ToString();
        int bishu = 4;
        DataTable dt = RemittancesBLL.jinliucx(hkid.ToString(), bishu);
        if (dt != null) num = dt.Rows.Count;



        return num;
    }
    /// <summary>
    /// 匹配
    /// </summary>
    /// <param name="hkid"></param>
    /// <returns></returns>
  [AjaxPro.AjaxMethod]
    public int PiepeiRemittanceGO(int hkid)
    {
        
     int c= PiepeiRemittance(  hkid);
        if (c > 0)
        {
            DAL.DBHelper.ExecuteNonQuery("update  remittances set shenhestate =1  where id=" + hkid);
            DataTable dtt = DAL.DBHelper.ExecuteDataTable(" select top 1   r.RemitNumber,r.RemitMoney  from Remittances r  where  r.id= " + hkid);
            if (dtt != null && dtt.Rows.Count > 0)
            {
                double wm = Convert.ToDouble(dtt.Rows[0]["RemitMoney"]);
                string rnumber = dtt.Rows[0]["RemitNumber"].ToString();

                string content = "<b style='margin:20px; '>系统匹配邮件</b> <p> 系统已为您的买入石斛积分完成匹配 ，请立即到交易中心查看买单并在两小时内完成汇款。<a href='Sellbuydetails.aspx?rmid=" + hkid + "'  >点击进入>></a></p> <p>系统邮件</p>"; 
                SendEmail.SendSystemEmail("System", "1", content, rnumber);
            }
        }

        return c;
    }
    /// <summary>
    /// 获取银行卡信息
    /// </summary>
    /// <param name="ctype"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string GetChoseCardInfo(int ctype)
    {
        if (Session["Member"] == null) return "-1";
        string cinfo = "";
        string number = Session["Member"].ToString();
        string sqls = "select isnull(name,'') Name ,isnull(BankBook,'') BankBook,isnull(BankCard,'') BankCard,isnull(mb.BankName,'') BankName,isnull(m.zhifubao ,'') zhifubao,isnull(m.weixin,'') weixin  from  memberinfo m left join MemberBank mb on m.BankCode=mb.BankCode   where number='" + number + "'";
        DataTable dtt = DBHelper.ExecuteDataTable(sqls);

        if (dtt != null && dtt.Rows.Count > 0)
        {
            string Name = dtt.Rows[0]["Name"] .ToString();
            string bankbook = dtt.Rows[0]["BankBook"].ToString();
            string bankname = dtt.Rows[0]["BankName"].ToString();
            string BankCard = dtt.Rows[0]["BankCard"].ToString();
            string zhifubao = dtt.Rows[0]["zhifubao"].ToString();
            string weixin = dtt.Rows[0]["weixin"].ToString();
            if (ctype == 0)
            {
                if (BankCard != "")
                    cinfo = GetCutJiami(bankbook) + " " + bankname + "  " + GetCutJiami(BankCard);
            }
            if (ctype == 1)
            {
                if (zhifubao != "")
                    cinfo = GetCutJiami(Name) + " " + GetCutJiami(zhifubao);
            }

            if (ctype == 2)
            {
                if (weixin != "")
                    cinfo = GetCutJiami(Name) + " " + GetCutJiami(weixin);
            }

        }
        return cinfo;
    }


    public string GetCutJiami(string ca)
    {
        string rec = "";
        if (ca == "")
        {
            rec = ca;
        }
        else if (ca.Length <= 3)
        {
            rec = ca.Substring(0, 1) + "*" + ca.Substring(ca.Length - 1, 1);
        }
        else
        {
            rec = ca.Substring(0, 3) + "****" + ca.Substring(ca.Length - 3, 3);
        }
        return rec;
    }

    [AjaxPro.AjaxMethod]
    public string AddWithdawNew(decimal sellcount, string password, int ctype,string yzm)
    {
        if (Session["Member"] == null) return "-1";
        string number = Session["Member"].ToString();
        decimal sgprice = Common.GetnowPrice();
        string smscode = "";
        if (DAL.MemberInfoDAL.CheckState(number)) { return "-2"; }

        if (Session["smscode"] == null)
        {
            return "验证码不正确！";
        }
        else
        {
             smscode = Session["smscode"].ToString();
        }
        if (yzm == "" || yzm == null)
        {
            return "请输入验证码！";
        }
        if (smscode != yzm)
        {
            return "验证码不正确！";
        }

        string sqlss = "select COUNT(0)  from withdraw  where  number='" + number + "' and shenhestate in (0,1,11,20) and DateDiff(dd,WithdrawTime,getutcdate())=0";
        int bc = Convert.ToInt32(DAL.DBHelper.ExecuteScalar(sqlss));


        if (bc >= 1)
        {
            return "每天最多只能卖出一单";
        }

        #region 为空验证

        if (ctype == -1)
        {
            return "请选择收款方式";
        }


        decimal txMoney = sellcount * sgprice; //提现金额
        if (sellcount <= 0)
        {
            return "请输入卖出数量";
        }

        string word = Encryption.Encryption.GetEncryptionPwd(password, number);
        int blean = ECRemitDetailBLL.ValidatePwd(number, word);
        if (blean == 1)
        {
            return "二级密码不正确";
        }
        else if (blean == 2)
        {
            return "对不起，您连续5次输入密码错，请2小时候在进行此操作";
        }

        #endregion

        decimal sxfbl = Common.GetSxfWyjblv(0); //手续费比例
        decimal wyjbl = Common.GetSxfWyjblv(1); //违约金比例

        decimal wyj = Convert.ToDecimal(txMoney.ToString()) * wyjbl; //违约金
        decimal sxf = Convert.ToDecimal(txMoney.ToString()) * sxfbl;  //手续费

        decimal wyjjb = wyjbl * sellcount; //违约金石斛积分数量
        decimal sxfjb = sxfbl * sellcount;  //手续费石斛积分数量

        decimal xjye = Convert.ToDecimal(BLL.CommonClass.CommonDataBLL.GetLeftMoney(number)); //现金账户余额【单位：美元】

        try
        {
            //string hkxz = " select value from JLparameter where jlcid=5";
            //DataTable dthkxz = DAL.DBHelper.ExecuteDataTable(hkxz);
            //string value = dthkxz.Rows[0]["value"].ToString();

            //if (txMoney % Convert.ToDouble(value) != 0)
            //{
            //    System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("009057", "提现金额只能为") + value + GetTran("009058", "的倍数") + "！');", true);
            //    this.money.Text = "";
            //    return;
            //}
            string strSql = "select top 1 zzye from memberInfo where number=@number";
            SqlParameter[] para = {
                                      new SqlParameter("@number",SqlDbType.NVarChar,30)
                                  };
            para[0].Value = number;
            decimal leftMoney = Convert.ToDecimal(DBHelper.ExecuteScalar(strSql, para, CommandType.Text));
            xjye = xjye - leftMoney;

            if (((sellcount + wyjjb + sxfjb)) > xjye)
            {

                return "超出最大可卖数量";
            }


            //    if ((txMoney + sxf) < Convert.ToDouble(BLL.CommonClass.CommonDataBLL.GetMinTxMoney()) / huilv)
            //    {
            //        System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("008149", "金额不得低于最低提现金额！") + "');", true);
            //        return "" ;
            //    }
            //}
            //else
            //{
            //    if (txMoney > Convert.ToDouble(rmoney.Text.Trim()) / huilv)
            //    {
            //        System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("005868", "金额超出最大可转金额！") + "');", true);
            //        return;
            //    }
            //}
        }
        catch (Exception eps)
        {

            return "系统繁忙稍后再试";
        }

        object o_one1 = DAL.DBHelper.ExecuteScalar("select value from JLparameter where jlcid=3");
        string value1 = "0";//提现违约金比例
        if (o_one1 != null)
            value1 = o_one1.ToString();



        WithdrawModel wDraw = new WithdrawModel();
        wDraw.DrawCardtype = ctype;
        wDraw.InvestJB = sellcount;
        wDraw.PriceJB = sgprice;

        wDraw.Number = Session["Member"].ToString();
        wDraw.ApplicationExpecdtNum = BLL.CommonClass.CommonDataBLL.getMaxqishu();

        wDraw.WithdrawMoney = Convert.ToDouble(txMoney);
        wDraw.WithdrawTime = DateTime.UtcNow;
        wDraw.OperateIP = BLL.CommonClass.CommonDataBLL.OperateIP;
        wDraw.Remark = "会员卖出石斛积分";

        wDraw.Wyj = Convert.ToDouble(wyj);  //违约金
        wDraw.WithdrawSXF = Convert.ToDouble(sxf);//手续费

        wDraw.InvestJBWYJ = wyjjb;  //违约金石斛积分数量
        wDraw.InvestJBSXF = sxfjb;//手续费石斛积分数量

        wDraw.IsJL = 1;
        wDraw.blmoney = Convert.ToDouble(sxfbl); //提现手续费比例
        wDraw.Wyjbl = Convert.ToDouble(wyjbl);//违约金比例 

        string sqls = "select  Name ,isnull(BankBook,'') BankBook,isnull(BankCard,'') BankCard,isnull(mb.BankName,'') BankName,isnull(m.zhifubao ,'') zhifubao,isnull(m.weixin,'') weixin  from  memberinfo m left join MemberBank mb on m.BankCode=mb.BankCode   where number='" + number + "'";
        DataTable dtt = DBHelper.ExecuteDataTable(sqls);
        if (dtt != null && dtt.Rows.Count > 0)
        {
            string Name = dtt.Rows[0]["Name"].ToString();
            string bankbook = dtt.Rows[0]["BankBook"].ToString();
            string bankname = dtt.Rows[0]["BankName"].ToString();
            string BankCard = dtt.Rows[0]["BankCard"].ToString();
            string zhifubao = dtt.Rows[0]["zhifubao"].ToString();
            string weixin = dtt.Rows[0]["weixin"].ToString();

            if (ctype == 0)  //选择银行卡
            {
                wDraw.Bankcard = BankCard;
                wDraw.Bankname = bankname;
                wDraw.Khname = bankbook;
            }
            if (ctype == 1)  //选择支付宝
            {
                wDraw.Khname = Name;
                wDraw.AliNo = zhifubao;
            }
            if (ctype == 2)  //选择微信号
            {
                wDraw.Khname = Name;
                wDraw.WeiXNo = weixin;
            }


        }


        bool isSure = false;

        isSure = BLL.Registration_declarations.RegistermemberBLL.WithdrawMoney1(wDraw);


        if (isSure)
        {
            AutoPipeiWithdraw(); //自动匹配
            return "0";
        }
        else return "-3";


    }


    public void AutoPipeiWithdraw()
    {
        string sqlstr = "select ID from remittances where  shenhestate =0  and isjl=1 order by remittancesDate  ";
        DataTable dtlist = DBHelper.ExecuteDataTable(sqlstr);
        foreach (DataRow item in dtlist.Rows)
        {
            int rmid = Convert.ToInt32(item["id"]);
            int c = PiepeiRemittance(rmid);
            if (c > 0)
            {
                DAL.DBHelper.ExecuteNonQuery("update  remittances set shenhestate =1  where id=" + rmid);
                DataTable dtt = DAL.DBHelper.ExecuteDataTable(" select top 1  r.RemitNumber,r.RemitMoney  from Remittances r  where  r.id= " + rmid);
                if (dtt != null && dtt.Rows.Count > 0)
                {
                    double wm = Convert.ToDouble(dtt.Rows[0]["RemitMoney"]);
                    string rnumber = dtt.Rows[0]["RemitNumber"].ToString();

                    string content = "<b style='margin:20px; '>系统匹配邮件</b> <p> 系统已为您的买入石斛积分完成匹配 ，请立即到交易中心查看买单并在两小时内完成汇款。<a href='Sellbuydetails.aspx?rmid=" + rmid + "'  >点击进入>></a></p> <p>系统邮件</p>";
                    SendEmail.SendSystemEmail("System", "1", content, rnumber);
                }
            }
        }



    }



    /// <summary>
    /// 加载交易中的数据状态
    /// </summary>
    /// <param name="pgidex"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string LoadSellerList(int pgidex)
    {
        if (Session["Member"] == null) return "-1";//and ReceivablesDate>  DATEADD(hour,-2,GETutcDATE()))
        string number = Session["Member"].ToString();
        string sqls = @"  select  * from (
  (select 0 as ratid , id  , 0 as bs,RemittancesDate as  trantime, investJB ,remitmoney as ttpriec,Ispipei,shenhestate,ReceivablesDate  as strartime   from remittances   where remitnumber=@number   and IsJL=1  and   (    shenhestate in(11, 3,1,0)    ) and id  not in (select  hkid from withdraw where iscl=1 ) )
  union(select hkid as ratid , id, 1 as bs, WithdrawTime as  trantime, investJB ,WithdrawMoney as ttpriec, 0 as Ispipei,shenhestate, hktime as strartime from Withdraw where number=@number and IsJL=1 and  iscl=0 and  shenhestate in(0,1, 3,11) and HkDj=0 )) as td order by td.trantime desc";

        SqlParameter[] sps = new SqlParameter[]{
           new SqlParameter("@number",number)
          };
        DataTable dtt = DBHelper.ExecuteDataTable(sqls, sps, CommandType.Text);
        string rechtml = " <li class='title' ><div class='firstdiv' >委托时间</div><div>数量/市值</div><div class='secdiv'>状态</div><div class='fourdiv'>操作</div></li> ";
        foreach (DataRow item in dtt.Rows)
        {
            int bs = Convert.ToInt32(item["bs"]);
            if (bs == 0)
            {
                rechtml += @" <li class='buyli'  > 
                      <a href='Sellbuydetails.aspx?rmid=" + item["id"] + @"' style='color: #dd4814;' > 
                          <div class='firstdiv'><p>买入</p>
                        <p>" + Convert.ToDateTime(item["trantime"]).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString("HH:mm:ss") + @"</p>   </div>
                        <div><p>" + Convert.ToInt32(item["investJB"]).ToString() + "</p><p>&yen;" + Convert.ToDouble(item["ttpriec"]).ToString("0.00") + @"</p></div>
                        <div class='secdiv'>" + GetRemitStateStr(Convert.ToInt32(item["Ispipei"]), Convert.ToInt32(item["shenhestate"]), Convert.ToDateTime(item["strartime"]).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours())) + @"</div>
                          </a>
                        <div  class='fourdiv'>" + GetRemitStateButton(Convert.ToInt32(item["id"]), Convert.ToInt32(item["shenhestate"]), Convert.ToDateTime(item["strartime"]).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()) ) + @"    </div>         </li>";

            }
            if (bs == 1)
            {
                rechtml += @" <li class='sellli'  > 
                        <a href='Selldetails.aspx?wdid=" + item["id"] + @"' > 
                             <div  class='firstdiv' ><p>卖出</p>
                        <p>" + Convert.ToDateTime(item["trantime"]).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString("HH:mm:ss") + @"</p>
                                      </div>
                        <div><p>" + Convert.ToInt32(item["investJB"]).ToString() + "</p><p>&yen;" + Convert.ToDouble(item["ttpriec"]).ToString("0.00") + @"</p></div>
                        <div class='secdiv'>" + GetDrawStateStr(Convert.ToInt32(item["id"]), Convert.ToInt32(item["shenhestate"])) + @"</div>
                            </a>
                        <div  class='fourdiv'>" + GetDrawStateButton(Convert.ToInt32(item["id"]), Convert.ToInt32(item["shenhestate"])) + "  </div>    </li>";

            }
        }




        return rechtml;

    }



    [AjaxPro.AjaxMethod]
    public string LoadSellerSuccessList(int pgidex)
    {
        if (Session["Member"] == null) return "-1";
        string number = Session["Member"].ToString();
        string sqls = @"    select  *from (
  (select w.number as Trnumber, w.DrawCardtype AS sktyep, 0 as ratid , r.id  , 0 as bs,RemittancesDate as  trantime,r.investJB ,r.RemitMoney as ttpriec, Ispipei,r.shenhestate,ReceivablesDate  as strartime   from remittances  r   join Withdraw w on r.ID=w.hkid   where remitnumber=@number   and r.IsJL=1  and   r. shenhestate=20 and  DateDiff(dd,RemittancesDate,getutcdate())<2  ) 
  union(select r.remitnumber as Trnumber, r.RemitCardtype AS sktyep,    hkid as ratid , w.id, 1 as bs, WithdrawTime as  trantime, w.investJB ,WithdrawMoney as ttpriec, 0 as Ispipei,w.shenhestate, hktime as strartime from Withdraw w   join  Remittances r  on w.hkid=r.id where number=@number  and w.IsJL=1 and  w.shenhestate =20   and  DateDiff(dd,WithdrawTime,getutcdate())<2   )) as td order by td.trantime desc
  
  ";

        SqlParameter[] sps = new SqlParameter[]{
           new SqlParameter("@number",number)
          };
        DataTable dtt = DBHelper.ExecuteDataTable(sqls, sps, CommandType.Text);
        string rechtml = @" <ul>  <li class='title'>
                        <div class='firstdiv'>委托时间</div>
                        <div>数量/市值</div>
                        <div class='fourdiv'>交易方</div>
                        <div class='secdiv' style='float: right;'>状态</div>
                    </li> ";
        foreach (DataRow item in dtt.Rows)
        {
            int bs = Convert.ToInt32(item["bs"]);
            if (bs == 0)
            {

                rechtml += @"<li class='buyli'>
                        <a href='Sellbuydetails.aspx?rmid=" + item["id"] + @"'>
                            <div class='firstdiv'>
                                <p>买入</p>
                                <p>" + Convert.ToDateTime(item["trantime"]).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString("HH:mm:ss") + @"</p>
                            </div>
                            <div>
                                <p>" + Convert.ToInt32(item["investJB"]).ToString() + @"</p>
                                <p>&yen;" + Convert.ToDouble(item["ttpriec"]).ToString("0.00") + @"</p>
                            </div>

                            <div class='fourdiv'>
                                <p>" + item["Trnumber"].ToString() + @"</p>
                                <p>" + Gettrantype(Convert.ToInt32(item["sktyep"])) + @"</p>
                            </div>

                            <div class='secdiv' style='color: forestgreen; float: right;'>买入已成</div>
                        </a>
                    </li>";



            }
            if (bs == 1)
            {

                rechtml += @"<li class='sellli'>
                        <a href='Selldetails.aspx?wdid=" + item["id"] + @"'>
                            <div class='firstdiv'>
                                <p>卖出</p>
                                <p>" + Convert.ToDateTime(item["trantime"]).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString("HH:mm:ss") + @"</p>
                            </div>
                            <div>
                                <p>" + Convert.ToInt32(item["investJB"]).ToString() + @"</p>
                                <p>&yen;" + Convert.ToDouble(item["ttpriec"]).ToString("0.00") + @"</p>
                            </div>

                            <div class='fourdiv'>
                                <p>" + item["Trnumber"].ToString() + @"</p>
                                <p>" + Gettrantype(Convert.ToInt32(item["sktyep"])) + @"</p>
                            </div>

                            <div class='secdiv' style='color: forestgreen; float: right;'>卖出已成</div>
                        </a>
                    </li>";

            }
        }
        rechtml += "</ul>";




        return rechtml;

    }

    public string Gettrantype(int type)
    {
        string resss = "";
        if (type == 0) resss = "银行卡";
        if (type == 1) resss = "支付宝";
        if (type == 2) resss = "微信";
        return resss;
    }

    private string GetRemitStateStr(int ispp, int shenhe,DateTime dtrev)
    {
        string mstr = "";
        if (dtrev > DateTime.Now.AddHours(-2))
        {

            if (shenhe == 0) mstr = "买入已报";
            if (shenhe == 1) mstr = "买入待汇";
            if (shenhe == 11) mstr = "买入已汇";
        }else 
        mstr = "<span style='color:#999;'>买入超时</span>";
        return mstr;
    }

    private string GetRemitStateButton(int hkid, int shenhe, DateTime dtst)
    {
        string mstr = "";
        if(shenhe!=20){
            if (dtst > DateTime.Now.AddHours(-2))
            {
                if (shenhe == 0) mstr += "<a  class='btn btn-danger'  onclick='cancelbuy(this," + hkid + @")'  >买入<br/>撤销</a>  ";// <a   class='btn btn-success '  onclick='confirmRmit(" + hkid + @")' >确认<br/>汇款 </a> 
                if (shenhe == 1)
                {
                    mstr += "<p name='ddjs' retime='" + dtst.AddHours(2).ToString() + "' >&nbsp;&nbsp; </p>";
                    mstr += "  <p >  <a   onclick='confirmRmit( " + hkid + ");' class='btn btn-success ' style='width:60%;'  >通知查收</a></p>";//sendRemitmesg(this," + hkid + ");
                }
                if (shenhe == 11)
                {
                    //mstr += "<p name='ddjs'  retime='" + dtst.ToString() + "' >&nbsp;&nbsp; </p>";
                    mstr += "  <p>  <a class='btn btn-default ' style='width:60%;'  >已通知</a></p>";
                }
            }
            else {
                mstr += "  <a  class='btn btn-default'  href='Sellbuydetails.aspx?rmid=" + hkid + @"'  >超时<br/>说明</a> ";
                if (shenhe==-1)
                {
                mstr=" <a  class='btn btn-danger'  onclick='delcsdj(" + hkid + @");'  >超时<br/>删除</a> ";
                }
            }
             }
        return mstr;
    }

    private string GetDrawStateStr(int wdid, int shenhe)
    {
        string mstr = "";
        if (shenhe == 0)
        {
            mstr += "卖出已报";

        } if (shenhe == 1)
        {
            mstr += "确认收款";
        }

        return mstr;
    }

    private string GetDrawStateButton(int wdid, int shenhe)
    {

        string mstr = "";
        if (shenhe == 0)
        {
            mstr += "<a  class='btn btn-danger ' onclick='cancelsell(this," + wdid + ")' style='color:#fff;'  >卖出<br/>撤销</a>";

        } if (shenhe == 1 || shenhe == 3 || shenhe == 11)
        {
            mstr += "<a   class='btn btn-success '  onclick='shoukuan(this," + wdid + ");' style='color:#fff;' >确认<br/>收款 </a>";
        }
        return mstr;
    }


    /// <summary>
    /// 确认汇款
    /// </summary>
    /// <param name="rmid"></param>
    /// <param name="rtype"></param>
    /// <param name="kahao"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string ConfirmRemittance(string rmid, string rtype, string name, string bankname, string kahao)
    {
        if (Session["Member"] == null) return "-1";
        int rc = 0;
        int c = Convert.ToInt32(DBHelper.ExecuteScalar("select count(0) from withdraw where  hkid=@rmid", new SqlParameter[] { new SqlParameter("@rmid", rmid) }, CommandType.Text));
        if (c == 0)
        { c=PiepeiRemittance(Convert.ToInt32(rmid));
        }
            if (c > 0)
            {
                string dn = DateTime.UtcNow.ToString();
                string sql = " update  Remittances   set  WppHkDj=1,shenHestate=1,ReceivablesDate=@time , RemitCardtype =@rtype  ,khname=@name  ";

                SqlParameter[] sps = null;
                if (rtype == "0")
                {
                    sps = new SqlParameter[] { 
          new SqlParameter("@time",dn),
           new SqlParameter("@rmid",rmid),
             new SqlParameter("@rtype",rtype), 
             new SqlParameter("@name",name),
            new SqlParameter("@bankcard",kahao) ,
            new SqlParameter("@bankname",bankname)
              };
                    sql += " ,bankcard=@bankcard  , bankname=@bankname ";

                }
                if (rtype == "1")
                {
                    sps = new SqlParameter[] { 
          new SqlParameter("@time",dn),
           new SqlParameter("@rmid",rmid),
             new SqlParameter("@rtype",rtype), 
             new SqlParameter("@name",name),
            new SqlParameter("@AliNo",kahao)  
              };
                    sql += " ,AliNo=@AliNo  ";
                }
                if (rtype == "2")
                {
                    sps = new SqlParameter[] { 
          new SqlParameter("@time",dn),
          new SqlParameter("@rmid",rmid),
             new SqlParameter("@rtype",rtype), 
             new SqlParameter("@name",name),
            new SqlParameter("@WeiXNo",kahao)  
              }; sql += " ,WeiXNo=@WeiXNo  ";
                }
                sql += "   where id= @rmid ";

                rc = DBHelper.ExecuteNonQuery(sql, sps, CommandType.Text);


           
            if (rc > 0)
                ConfirmRemittanceSendMsg(Convert.ToInt32(rmid));//发邮件 
        } 

        return rc.ToString();

    }




    [AjaxPro.AjaxMethod]
    public string ConfirmRemittanceSendMsg(int rmid)
    {
        if (Session["Member"] == null) return "-1";
        string recc = "";
        string sql = " update  Remittances   set   shenHestate=11  where   id=" + rmid;
        recc = DBHelper.ExecuteNonQuery(sql).ToString();


        //发送系统邮件
        string sendnumber = Session["Member"].ToString();

        DataTable dtt = DAL.DBHelper.ExecuteDataTable(" select top 1 id,number,WithdrawMoney,bankcard,bankname ,drawcardtype,alino,weixno  from  Withdraw where hkid= " + rmid);
        if (dtt != null && dtt.Rows.Count > 0)
        {
            string id = dtt.Rows[0]["id"].ToString();
            string recivenumber = dtt.Rows[0]["number"].ToString();
            double wm = Convert.ToDouble(dtt.Rows[0]["WithdrawMoney"]);
            string bkcd = dtt.Rows[0]["bankcard"].ToString();
            string bkname = dtt.Rows[0]["bankname"].ToString();
            string alino = dtt.Rows[0]["alino"].ToString();
            string weixno = dtt.Rows[0]["weixno"].ToString();
            int dtype = Convert.ToInt32(dtt.Rows[0]["drawcardtype"]);
            string content = "";
            if (dtype == 0)
                content = "<b style='margin:20px; '>卖出查收提醒</b> <p> 会员" + sendnumber + " 已向您的银行账户" + bkname + " " + bkcd + "转账汇款" + wm.ToString("0.00") + " ,请查询您的银行余额确认收款，并在交易中心确认收款。<a href='Selldetails.aspx?wdid="+id+"'  >点击进入>></a></p> <p>系统邮件</p>";
            if (dtype == 1)
                content = "<b style='margin:20px; '>卖出查收提醒</b> <p> 会员" + sendnumber + " 已向您的支付宝账户" + alino + "转账汇款" + wm.ToString("0.00") + " ,请查询您的支付宝余额确认收款，并在交易中心确认收款。<a href='Selldetails.aspx?wdid=" + id + "'  >点击进入>></a></p> <p>系统邮件</p>";
            if (dtype == 2)
                content = "<b style='margin:20px; '>卖出查收提醒</b> <p> 会员" + sendnumber + " 已向您的微信钱包" + weixno + "转账汇款" + wm.ToString("0.00") + " ,请查询您的微信钱包余额确认收款，并在交易中心确认收款。<a href='Selldetails.aspx?wdid=" + id + "'  >点击进入>></a></p> <p>系统邮件</p>";


            SendEmail.SendSystemEmail(sendnumber, "1", content, recivenumber);
        }


        return recc;

    }


    /// <summary>
    /// 撤销买入
    /// </summary>
    /// <param name="rmid"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string CancelRemittance(int rmid)
    {
        string sql7s = "update   withdraw   set  hkid=0  where  hkid= " + rmid;

        DAL.DBHelper.ExecuteNonQuery(sql7s);
        string sqls = "update   remittances   set  shenhestate=-1  where  id= " + rmid;

        int ccc = DAL.DBHelper.ExecuteNonQuery(sqls);

        return ccc.ToString();
    }
  /// <summary>
    /// 删除买入
    /// </summary>
    /// <param name="rmid"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string DelRemittance(int rmid)
    {
        string sql7s = "update   withdraw   set  hkid=0  where  hkid= " + rmid;

        DAL.DBHelper.ExecuteNonQuery(sql7s);
        string sqls = "delete  from    remittances    where  id= " + rmid;

        int ccc = DAL.DBHelper.ExecuteNonQuery(sqls);

        return ccc.ToString();
    }
    /// <summary>
    /// 撤销卖出
    /// </summary>
    /// <param name="wdid"></param>
    /// <returns></returns>
    [AjaxPro.AjaxMethod]
    public string CancelWithdraw(int wdid)
    {
        if (Session["Member"] == null) { return "-1"; }
        string rec = "0";
        int rc = Convert.ToInt32(DBHelper.ExecuteScalar("select hkid from Withdraw where id =" + wdid));
        if (rc > 0)
        {
            rec = "-2";

        }
        else
        {

            SqlTransaction tran = null;
            SqlConnection conn = null;


            try
            {
                conn = DBHelper.SqlCon();
                conn.Open();
                tran = conn.BeginTransaction();
                string sql1 = @" update  memberinfo set MemberShip= MemberShip- w.dj  from 
  (    select wd.number, SUM( InvestJB+InvestJBSXF+InvestJBWYJ) as dj  from Withdraw  wd  where  wd.id=" + wdid + @"  group by wd.number)  w where memberinfo .Number=w.number";

                int c = DBHelper.ExecuteNonQuery(tran, sql1);

                string sql2 = " UPDATE Withdraw	SET shenHestate = -1	WHERE  id =" + wdid;
                c += DBHelper.ExecuteNonQuery(tran, sql2);

                if (c == 2)
                {
                    tran.Commit();
                    rec = "1";
                }
                else tran.Rollback();
            }
            catch (Exception)
            {
                tran.Rollback();

            }
            finally
            {
                tran.Dispose();
                conn.Close();
                conn.Dispose();
            }

        }

        return rec;
    }


   [AjaxPro.AjaxMethod]
    public string ConfirmWithdrawSK(int wdid) 
    {
        if (Session["Member"] == null) return "-1";
           
       string restr="0";
       int  issh=Convert.ToInt32(DAL.DBHelper.ExecuteScalar("select  shenhestate from  Withdraw where id="+wdid));
       if (issh != 20)
       {
           string sqls = "ConfirmWithDrawSk";
           int outp = 0;
           try
           {


               SqlParameter[] sps = new SqlParameter[]{
        new SqlParameter("@wdid",wdid),
         new SqlParameter("@rec",outp)
       };
               sps[1].Direction = ParameterDirection.Output;
               DBHelper.ExecuteNonQuery(sqls, sps, CommandType.StoredProcedure);
               int c = Convert.ToInt32(sps[1].Value);
               if (c == 1)
               {
                   restr = "1";
                   string sendnumber = Session["Member"].ToString();
                   DataTable dtt = DAL.DBHelper.ExecuteDataTable(" select top 1  r.RemitNumber,w.WithdrawMoney  from Remittances r left join   Withdraw   w on r.ID=w.hkid where w.id= " + wdid);
                   if (dtt != null && dtt.Rows.Count > 0)
                   {
                       double wm = Convert.ToDouble(dtt.Rows[0]["WithdrawMoney"]);
                       string rnumber = dtt.Rows[0]["RemitNumber"].ToString();

                       string content = "<b style='margin:20px; '>买入查收提醒</b> <p> 会员" + sendnumber + " 确认收到您的转账汇款" + wm.ToString("0.00") + " ,请到您石斛积分账户查收石斛积分。</p> <p>系统邮件</p>";
                       SendEmail.SendSystemEmail(sendnumber, "1", content, rnumber);
                   }


               }
           }
           catch (Exception eee)
           {

           }



           //{
           //发送系统邮件通知


           //}
           //else
           //{
           //    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("009051", "到账失败") + "！');location.href='TxDetailDCS.aspx'</script>", false);
           //}


       }
       else {
           restr = "2";
       }
       return restr;

    }

   [AjaxPro.AjaxMethod]
   public string XFOrders(int isact, int pageindex)
   {
       string curstr = "";
       string cdit = "";
       int pgsize = 10;
       if (Session["Member"] != null)
       {
           string direct = Session["Member"].ToString();


           string sqls = @"WITH sss AS(SELECT case when XFState = '0' then '已提交' when XFState = '1' then '处理中' when XFState ='2' then '完成'  when XFState ='3' then '拒绝'  end  as zt,*,ROW_NUMBER() OVER(ORDER BY id desc ) as rowNum FROM MemberCashXF where Number='" + direct + "') SELECT * FROM sss WHERE rowNum BETWEEN " + ((pageindex - 1) * pgsize + 1) + " AND " + pageindex * pgsize + "";


           DataTable dtt = DAL.DBHelper.ExecuteDataTable(sqls);
           foreach (DataRow item in dtt.Rows)
           {
               string ispay = Convert.ToString(item["Number"]);

               curstr += "<tr><td>" + Convert.ToDateTime(item["XFTime"]).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString("yy-MM-dd HH:mm:ss") + "</td><td style='color: #dd4814'>"+ Convert.ToDouble(item["XFON"]).ToString("0.00") + "</td> <td style='color: #dd4814'>"+ item["zt"].ToString() + "</td></tr>";

           }
       }
       return curstr;

   }
   public string Efayitype(int type)
   {
       string resss = "";
       if (type == 0) resss = GetTran("010063", "充值中");
       if (type == 1) resss = GetTran("000968", "成功");
       if (type == 9) resss = GetTran("010064", "撤销");
       return resss;
   }

   [AjaxPro.AjaxMethod]
   public string EshenghuoCZ(int pgidex, int type)
   {
       if (Session["Member"] == null) return "-1";
       string curstr = "";
       int pgsize = 10;
       string number = Session["Member"].ToString();
       //string sqls = @"    select  * from (select * from BMOuter where Number='" + number + "' and OuterType=" + type + ") as td order by td.trantime desc";
       string sqls = @"WITH sss AS(SELECT id,rechargeAccount,saleAmount,EPmny,ROW_NUMBER() OVER(ORDER BY  id desc ) as rowNum FROM BMOuter where Number='" + number + "' and OuterType=" + type + "   ) SELECT * FROM sss WHERE rowNum BETWEEN " + ((pgidex - 1) * pgsize + 1) + " AND " + pgidex * pgsize + "";

       DataTable dtt = DBHelper.ExecuteDataTable(sqls);
       foreach (DataRow item in dtt.Rows)
       {

           curstr += "<tr ><td>" + item["rechargeAccount"].ToString() + "</td><td>" + item["saleAmount"].ToString() + "</td><td><span >" + Convert.ToDouble(item["EPmny"]).ToString("0.00") + "</span></td></tr>";
          
       }




       return curstr;


   }


   [AjaxPro.AjaxMethod]
   public string EshenghuoCP(int pgidex, int type)
   {
       if (Session["Member"] == null) return "-1";
       string curstr = "";
       int pgsize = 10;
       string number = Session["Member"].ToString();
       //string sqls = @"    select  * from (select * from BMPiaowu where Number='" + number + "' and orderType=" + type + ") as td order by td.trantime desc";
       string sqls = @"WITH sss AS(SELECT startStation,depTime,EPmny,ROW_NUMBER() OVER(ORDER BY  id desc ) as rowNum FROM BMPiaowu where Number='" + number + "' and OuterType=" + type + "   ) SELECT * FROM sss WHERE rowNum BETWEEN " + ((pgidex - 1) * pgsize + 1) + " AND " + pgidex * pgsize + "";

       DataTable dtt = DBHelper.ExecuteDataTable(sqls);
       foreach (DataRow item in dtt.Rows)
       {

           curstr += "<tr ><td>" + item["startStation"].ToString() + "</td><td>" + item["depTime"].ToString() + "</td><td><span >" + Convert.ToDouble(item["EPmny"]).ToString("0.00") + "</span></td></tr>";
          
       }




       return curstr;

   }

     
  

}