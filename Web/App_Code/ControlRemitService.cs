using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Data;
using DAL;
using System.Data.SqlClient;
using Encryption;
using BLL.other;

/// <summary>
///ControlRemitService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class ControlRemitService : System.Web.Services.WebService
{

    public ControlRemitService()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public int  getnewremcount()
    {
        return RemittancesDAL. GetnewRemitcount();
    }
    /// <summary>
    /// 关闭声音
    /// </summary>
    /// <param name="rmide"></param>
    /// <returns></returns>
    [WebMethod]
    public int Getupdatesound(string  rmide)
    {
        NumberByBit nbt = new NumberByBit();
        string rmid=   nbt.DecryptDES(rmide,"20120518");
        return RemittancesDAL.Getupdatesound(rmid);
    }

    [WebMethod]
    public bool VilidateLogin(string username, string ukp)
    {
        int res = 1;
           NumberByBit nbt = new NumberByBit();
          string desstnpi= nbt.DecryptDES(ukp,"20120518");
     string[] srmps = desstnpi.Split(',');
        if( srmps.Length==3)
        {
            string uname=srmps[0];
            if (uname == username)
            {
                string pass= Encryption.Encryption.GetEncryptionPwd( srmps[1],username ) ; //加密密码
                
                string ip = srmps[2];

                  res = RemittancesDAL.doOrgman(uname, pass, ip, 2);  //验证登录
            }
        }
        return  res==0;
    }




    /// <summary>
    /// 根据条件获取相应汇款单列表
    /// </summary>
    /// <param name="strtime"></param>
    /// <param name="endtime"></param>
    /// <param name="comisqr"></param>
    /// <param name="mbisqr"></param>
    /// <param name="mbname"></param>
    /// <param name="number"></param>
    /// <returns></returns>
    [WebMethod]
    public DataSet GetRemitlist(string condition, int page, out  int totalpage, out int curpage)
    {

        return RemittancesDAL.GetRemitlist(condition, out totalpage, out curpage, page);
    }
    /// <summary>
    /// 根据类型进行不同操作  类型 4 发展商确认后公司确认操作， 5发展商确认 ，6 发展商确认后 公司没有收到账款点击未到账操作 禁止发展商使用确认功能
    /// </summary>
    /// <param name="ctype"></param>
    /// <returns></returns>
    [WebMethod]
    public int DoControlRemit(int ctype, string rmidstr)
    {
        int errorinfo = 0;
        int ges = -1;
        NumberByBit nbt = new NumberByBit();
        string desstrm = nbt.DecryptDES(rmidstr, "20120518");
        string[] srmps = desstrm.Split(',');
        string rmid = "";
        string oper = "";
        string opip = "";
        if (srmps.Length >= 3)
        {
            rmid = srmps[0].ToString();
            oper = srmps[1].ToString();
            opip = srmps[2].ToString();
        }
        DataTable remittancedt = RemittancesDAL.GetRemittanceinfobyremid(rmid);
        if (remittancedt != null && remittancedt.Rows.Count > 0)
        {
            int roltype = Convert.ToInt32(remittancedt.Rows[0]["RemitStatus"]) == 0 ? 2 : 1;
            string orderid = remittancedt.Rows[0]["RelationOrderID"].ToString();
            double totalrmbmoney = Convert.ToDouble(DBHelper.ExecuteScalar("select isnull(totalrmbmoney,0) from remtemp where  remittancesid='" + rmid + "'"));
           int dotype=0;
           if (orderid == "") dotype = 2; else dotype = 1;

           ges = AddOrderDataDAL.OrderPayment(oper, orderid, opip, roltype, dotype, -1, oper, "", 3, ctype, 1, 1, rmid, totalrmbmoney, ""); ;// AddOrderDataDAL.PaymentChongzhi(desstrm, ctype);
        }
            double tomoney = 0;
        //if (ges == 0 || ges == 7)
        //{

            //// 短信
            //SqlConnection con = null;
            //SqlTransaction tran = null;

            //string info = string.Empty;
            //tomoney = Convert.ToDouble(DBHelper.ExecuteScalar("select isnull(totalrmbmoney,0) from dbo.remtemp where  remittancesid='" + rmid + "'"));
            //string acnumber = DBHelper.ExecuteScalar("select number from dbo.MemberRemittances where  remittancesid='" + rmid + "'").ToString();
            ////手机号码
            //object mt = DAL.DBHelper.ExecuteScalar("select  MobileTele from memberinfo where number ='" + acnumber + "'");
            //string sjhm = mt == null ? "" : mt.ToString();
            ////支付的金额
            //string zfje = tomoney.ToString("f2");

            //if (sjhm != "" && oper != "")
            //{
            //    string sendinfo = "管理员" + oper + "已经成功确认发展商 " + acnumber + " 的支付【" + zfje + "元人民币】，充值成功，请及时登录查看！";
            //    if (ges == 7)
            //    {
            //        sendinfo = "管理员" + oper + "已经成功确认发展商 " + acnumber + " 的支付【" + zfje + "元人民币】，发展商 " + acnumber + " 的账号已成功激活，请及时登录查看！";
            //    }
            //    try
            //    {

            //        con = DAL.DBHelper.SqlCon();
            //        con.Open();
            //        tran = con.BeginTransaction();

            //        bool bo = true;// BLL.MobileSMS.SendMsgMode(tran, "", sendinfo, acnumber, sjhm, "", Model.SMSCategory.sms_Active);

            //        if (bo)
            //            tran.Commit();
            //        else
            //            tran.Rollback();
            //    }
            //    catch (Exception ee)
            //    {
            //        if (tran != null)
            //            tran.Rollback();
            //    }
            //    finally
            //    {
            //        con.Close();
            //    }

            //}
        //    errorinfo = 1;
        //}
        //else
        //{
        //    errorinfo = 0;
        //}
        return      ges;

    }
    /// <summary>
    /// 获取银行帐号集合
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public DataSet GetBankDataSet()
    {
        return RemittancesDAL.GetBankDataSet();
    }
    /// <summary>
    /// 改变发展商是否使用自主确认功能
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public int Doisuseconfirm(string number) 
    {
        return RemittancesDAL.Doisuseconfirm(number);
    }
    /// <summary>
    /// 检查有没有超出错误次数
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    [WebMethod]
    public bool Checklgerror(string number,out int a )
    {
        NumberByBit nbt = new NumberByBit();
          number = nbt.DecryptDES(number, "20120518");
          a = 0;
          return true;// IndexBLL.CheckNotlogin(number, 2, out a);
    }
    /// <summary>
    /// 初始化错误次数
    /// </summary>
    /// <param name="number"></param>
    [WebMethod]
    public void Initlgerrorcs(string number)
    {
      //NumberByBit nbt = new NumberByBit();
      //    number = nbt.DecryptDES(number, "20120518");
      //    IndexBLL.Initlogincs(number,2);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="number"></param>
    [WebMethod]
    public void Updateloerror(string number)
    {
        //NumberByBit nbt = new NumberByBit();
        //number = nbt.DecryptDES(number, "20120518");
        //IndexBLL.Updateloerror(number, 2);
    }

}

