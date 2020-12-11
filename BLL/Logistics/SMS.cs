using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace BLL
{
    public class MobileSMS
    {
        public MobileSMS()
        { 
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="Recipient">接收短信前缀语，用于拼接短信公共部分</param>
        /// <param name="recipientNo">接收人在系统中的编号[可为空]</param>
        /// <param name="mobile">接受人手机号码</param>
        /// <param name="primaryKey">短信关联外键[可为空]</param>
        /// <param name="smsCategory">发送类型</param>
        /// <returns></returns>
        public static bool SendMsgTo(SqlTransaction tran,string PreMsg, string recipientNo, string mobile,string primaryKey, Model.SMSCategory smsCategory)
        {
            string ExSql = "select DefaultContent from configSMS where isOpen=1 and configCode=" + (int)smsCategory;
            string info = string.Empty;
            string msg = string.Empty;           
            bool flag = false;
            DataTable  dt = DAL.DBHelper.ExecuteDataTable (tran,ExSql);
            if(dt!=null&&dt.Rows.Count >0)
            {
                msg = dt.Rows[0]["DefaultContent"].ToString();              
            }            
            if (msg != "")
            {
                msg = PreMsg + msg;
                flag = SendMsgTo(tran, recipientNo, primaryKey, mobile, msg, out info, smsCategory);
            }
            return flag;
            
        }

       
        /// <summary>
        /// 匹配模式发送短信内容，用于收款，发货，注册的短信通知——ds2012——tianfeng
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="PreName">接收短信称呼</param>
        /// <param name="SuffixMsg">个人信息</param>
        /// <param name="recipientNo">接收人在系统中的编号[可为空]</param>
        /// <param name="mobile">接受人手机号码</param>
        /// <param name="primaryKey">>短信关联外键[可为空]</param>
        /// <param name="smsCategory">发送类型</param>
        /// <returns></returns>
        public static bool SendMsgMode(SqlTransaction tran, string PreName, string Profile, string recipientNo, string mobile, string primaryKey, Model.SMSCategory smsCategory)
        {
            string ExSql = "select DefaultContent from configSMS where isOpen=1 and configCode=" + (int)smsCategory;
            string info = string.Empty;
            string msg = string.Empty;
            bool flag = false;
            DataTable dt = DAL.DBHelper.ExecuteDataTable(tran, ExSql);
            if (dt != null && dt.Rows.Count > 0)
            {
                msg = dt.Rows[0]["DefaultContent"].ToString();
            }
            if (msg != "")
            {
                Regex rxName = new Regex(@"(\[Name\])", RegexOptions.IgnoreCase);
                Regex rxProfile = new Regex(@"(\[Profile\])", RegexOptions.IgnoreCase);

                msg = rxName.Replace(msg, PreName);       //替换称呼部分rxProfile
                msg = rxProfile.Replace(msg, Profile);       //替换个人信息
             
                flag = SendMsgTo(tran, recipientNo, primaryKey, mobile, msg, out info, smsCategory);
            }
            return flag;

        }

       
        /// <summary>
        /// [带事务,三参数]发送短信并记录历史记录
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="mobile">接收人手机号码</param>
        /// <param name="sendMsg">发送内容</param>
        /// <returns></returns>
        public static bool SendMsgTo(SqlTransaction tran, string mobile,string sendMsg)
        {            
            string msg = string.Empty;
            bool flag = false;
            if (mobile == "" || sendMsg == "")
                return flag;
            string info = string.Empty;
            flag = SendMsgTo(tran, "", "", mobile, sendMsg, out info, Model .SMSCategory.sms_GroupSend);           
            return flag;
        }


       

       
        /// <summary>
        /// [带事务,7参数]发送短信并记录历史记录
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="recipientNo">接收人在系统中的编号[可为空]</param>
        /// <param name="primaryKey">关联主键[可为空]</param>
        /// <param name="mobile">接收人手机号码</param>
        /// <param name="msg">发送内容</param>
        /// <param name="info">返回信息[out参数]</param>
        /// <param name="category">短信类型</param>
        /// <returns></returns>
        public static bool SendMsgTo(SqlTransaction tran, string recipientNo, string primaryKey, string mobile, string msg, out string info,Model.SMSCategory category)
        {
            info = "";
            bool flag = false;
            bool flag2 = false;
            string mRtv = "";

            if (mobile != "")
            {
                if (msg.Length <= 256)
                {
                    MobileSMS sm = new MobileSMS();

                    string tempMsg = msg;
                    string temp = "";
                    ArrayList msgAr = new ArrayList();
                    while (tempMsg.Length > 65)
                    {
                        temp = tempMsg.Substring(0, 65).Trim();//前65个字符
                        tempMsg = tempMsg.Substring(65);       //65以后的字符					
                        msgAr.Add(temp);
                    }
                    if (tempMsg.Length > 0)
                    {
                        msgAr.Add(tempMsg);
                    }

                    int maxID = int.Parse(DAL.DBHelper.ExecuteScalar(tran, "select isnull(max(id),0)  from h_mobilemsg", new SqlParameter[0], CommandType.Text).ToString());
                    string SendNo = maxID.ToString().PadLeft(8, '0');
                    int SendSmallNo = 1;                               //分条序号
                    foreach (string str in msgAr)
                    {//短信分条发送							
                        flag = sm.SendMobile(tran, new string[] { mobile }, str, out mRtv);
                        string smallNo = SendSmallNo.ToString() + "/" + msgAr.Count.ToString();
                        flag2 = sm.SaveSendMsgRecord(tran, recipientNo, primaryKey, mobile, flag, mRtv, str, SendNo, smallNo, (int)category);
                        if (flag && flag2)
                        {
                            SendSmallNo++;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    info = BLL.Translation.Translate("007140", "短信内容过长");
                    return false;
                }
            }
            if (flag && flag2)
            {
                info = BLL.Translation.Translate("005615", "发送成功！") ;
                return true;
            }
            else
            {
                if (!flag)
                {
                    info = BLL.Translation.Translate("007141", "短信发送失败，返回信息：") +mRtv.Replace("'", "");
                }
                if (!flag2)
                {
                    info = BLL.Translation.Translate("007142", "短信发送记录失败，请与管理员联系！");
                }
                return false;
            }
        }
        /*======================================
        说明:封装单发接口
        参数:
            dst:目标手机号码
            msg:发送短信内容
        返回值:
            true:发送成功; The operation has timed-out.
            false:发送失败
        ======================================*/
        /// <summary>
        ///  [带事务]
        /// </summary>
        /// <param name="mobiles">目标手机号码数组类型</param>
        /// <param name="SmsMsg">发送短信内容</param>
        /// <param name="mRtv">返回值内容</param>
        /// <returns></returns>
        public bool SendMobile(SqlTransaction tran, string[] mobiles, string SmsMsg, out string mRtv)
        {
            string mToUrl = "";	//即将引用的url   			
            mRtv = "";		//引用的返回字符串
            SmsMsg = SmsMsg.Trim();
            if (SmsMsg == "")
            {
                return false;
            }
            string DstMobile = "";
            foreach (string mobile in mobiles)
            {
                if (DstMobile != "")
                {
                    DstMobile = DstMobile + "," + mobile;
                }
                else
                {
                    DstMobile = mobile;
                }
            }
            if (DstMobile == "")
            {
                return false;
            }
            //去掉没有去掉的Mark标签
            Regex rxNo = new Regex(@"(\[No\])", RegexOptions.IgnoreCase);
            Regex rxName = new Regex(@"(\[Name\])", RegexOptions.IgnoreCase);
            Regex rxPetName = new Regex(@"(\[PetName\])", RegexOptions.IgnoreCase);
           
            SmsMsg = rxNo.Replace(SmsMsg, "");
            SmsMsg = rxName.Replace(SmsMsg, "");
            SmsMsg = rxPetName.Replace(SmsMsg, "");

            Regex rxResetPwd = new Regex(@"(\[pwd:.+\])", RegexOptions.IgnoreCase);
            MatchCollection matchParse = rxResetPwd.Matches(SmsMsg);
            if (matchParse.Count > 0)
            {
                string resetPwd = matchParse[0].ToString();
                resetPwd = resetPwd.Substring(5, resetPwd.Length - 6);
                SmsMsg = rxResetPwd.Replace(SmsMsg, resetPwd);
            }

            SmsMsg = System.Web.HttpUtility.UrlEncode(SmsMsg, System.Text.Encoding.GetEncoding("gb2312"));
            string url = string.Empty;
            string name = string.Empty;
            string pwd = string.Empty;
            string isOpen = string.Empty;
            string ExSql = string.Empty;
            url = BLL.FileOperateBLL.ForXML.ReadSMSConfig("SmsUrl").ToString().Trim();
            name = BLL.FileOperateBLL.ForXML.ReadSMSConfig("UserName").ToString().Trim();
            pwd = BLL.FileOperateBLL.ForXML.ReadSMSConfig("Pwd").ToString().Trim();
            isOpen = BLL.FileOperateBLL.ForXML.ReadSMSConfig("IsOpen").ToString().Trim();

            if (url != "" && name != "" && pwd != "" && isOpen=="1")
            {
                name = System.Web.HttpUtility.UrlEncode(name, System.Text.Encoding.GetEncoding("gb2312"));
                pwd = System.Web.HttpUtility.UrlEncode(pwd, System.Text.Encoding.GetEncoding("gb2312"));
                mToUrl = url + "name=" + name + "&pwd=" + pwd + "&dst=" + DstMobile + "&msg=" + SmsMsg + "";
                try
                {
                    System.Net.HttpWebResponse rs = (System.Net.HttpWebResponse)System.Net.HttpWebRequest.Create(mToUrl).GetResponse();
                    System.IO.StreamReader sr = new System.IO.StreamReader(rs.GetResponseStream(), System.Text.Encoding.Default);
                    mRtv = sr.ReadToEnd();
                }
                catch (Exception ex)
                {
                    mRtv = ex.Message.ToString().Trim();
                    return false;	//对 url http 请求的时候发生的错误  比如页面不存在 或者页面本身执行发生错误
                }
            }
            else
            {
                return false;
            }

            if (mRtv.Substring(0, 4) == "num=")
            {//有返回发送成功失败数量
                if (mRtv.Substring(0, 5) != "num=0")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        
        /// <summary>
        /// [带事务]用于记录发送记录
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="bianhao">接收人编号</param>
        /// <param name="primaryKey">关联主键,可以为空</param>
        /// <param name="mobiles">手机号码</param>
        /// <param name="flag">发送状态</param>
        /// <param name="receiveMsg">返回信息</param>
        /// <param name="sendMsg">发送内容</param>
        /// <param name="SendNo">发送编号[8位]</param>
        /// <param name="SendSmallNo">小序号</param>
        /// <param name="mode">短信类型：0-人工指定号码发送；1-会员注册通知；2-发货通知；3-汇款审核通知；4-应收账款通知</param>
        /// <returns></returns>
        private bool SaveSendMsgRecord(SqlTransaction tran, string bianhao, string primaryKey, string mobiles, bool flag, string receiveMsg, string sendMsg, string SendNo, string SendSmallNo,int mode)
        {
            bool flag2 = false;
            int var = 0;
            int newid=0;
            try
            {
                string ExSql = string.Empty;
                string sFlag = "0";
                SqlParameter[] paras = null;  
                if (flag)
                {
                    sFlag = "1";
                }
                else
                {
                    sFlag = "0";
                }

                Regex rxResetPwd = new Regex(@"(\[pwd:.+\])", RegexOptions.IgnoreCase);
                MatchCollection matchParse = rxResetPwd.Matches(sendMsg);
                if (matchParse.Count > 0)
                {
                    string resetPwd = matchParse[0].ToString();
                    resetPwd = resetPwd.Substring(5, resetPwd.Length - 6);
                    sendMsg = rxResetPwd.Replace(sendMsg, "******");
                }

                #region 记录正式记录表h_mobileMsg
                ExSql = "select count(1) from h_mobileMsg where sendMsg=@sendMsg and mobile=@mobile";
                paras = new SqlParameter[]{
												 new SqlParameter ("@mobile",SqlDbType.NVarChar,20),
												 new SqlParameter ("@sendMsg",SqlDbType.NVarChar,11)												
											 };
                paras[0].Value = mobiles;
                paras[1].Value = sendMsg;
                var = Convert .ToInt32(DAL.DBHelper.ExecuteScalar(tran, ExSql, paras, CommandType.Text));

                if (var == 0)
                {
                    ExSql = "insert into h_mobileMsg(CustomerID,mobile,receiveMsg,sucflag,sendMsg,SendNo,SendSmallNo,BillID,Category) "
                        + "values(@CustomerID,@mobile,@receiveMsg,@sucflag,@sendMsg,@SendNo,@SendSmallNo,@BillID,@Category);select @@identity";
                    if (mobiles != "")
                    {
                        paras = new SqlParameter[]{
												 new SqlParameter ("@CustomerID",SqlDbType.NVarChar,20),
												 new SqlParameter ("@mobile",SqlDbType.NVarChar,11),
												 new SqlParameter ("@receiveMsg",SqlDbType.NVarChar,500),
												 new SqlParameter ("@sucflag",SqlDbType.Char ,1),
												 new SqlParameter ("@sendMsg",SqlDbType.NVarChar ,120),  //SendNo,SendSmallNo
												 new SqlParameter ("@SendNo",SqlDbType.Char ,8),
												 new SqlParameter ("@SendSmallNo",SqlDbType.Char ,5),
												 new SqlParameter ("@BillID",SqlDbType.NVarChar ,200),      //单据编号
												 new SqlParameter ("@Category",SqlDbType.Int)
												
											 };
                        paras[0].Value = bianhao;
                        paras[1].Value = mobiles;
                        paras[2].Value = receiveMsg;
                        paras[3].Value = sFlag;
                        paras[4].Value = sendMsg;
                        paras[5].Value = SendNo;
                        paras[6].Value = SendSmallNo;
                        paras[7].Value = primaryKey;
                        paras[8].Value = mode;

                        newid = Convert.ToInt32 (DAL.DBHelper.ExecuteScalar(tran, ExSql, paras, CommandType.Text));
                        //var = DAL.DBHelper.ExecuteNonQuery(tran, ExSql, paras, CommandType.Text);
                        if (newid > 0)
                        {
                            flag2 = true;
                        }
                        else
                        {
                            flag2 = false;
                        }
                    }
                    else
                    {//己存在同手机号码同短信内容的记录，直接返回
                        flag2 = true;
                    }
                }
                #endregion

                if (!flag && flag2==true)
                {//短信发送失败,将记录一张临时表

                    #region 
                    ExSql = "select count(1) from h_TempMsg where sendMsg=@sendMsg and mobile=@mobile";
                    paras = new SqlParameter[]{
												 new SqlParameter ("@mobile",SqlDbType.NVarChar,20),
												 new SqlParameter ("@sendMsg",SqlDbType.NVarChar,11)												
											 };
                    paras[0].Value = mobiles;
                    paras[1].Value = sendMsg;
                    var = Convert .ToInt32(DAL.DBHelper.ExecuteScalar(tran, ExSql, paras, CommandType.Text));

                    if (var == 0)
                    {
                        ExSql = "insert into h_TempMsg(bianhao,mobile,receiveMsg,sucflag,sendMsg,SendNo,SendSmallNo,OrderID,mode,smsID) values(@bianhao,@mobile,@receiveMsg,@sucflag,@sendMsg,@SendNo,@SendSmallNo,@OrderID,@mode,@smsID)";
                        if (mobiles != "")
                        {
                            paras = new SqlParameter[]{
												 new SqlParameter ("@bianhao",SqlDbType.NVarChar,20),
												 new SqlParameter ("@mobile",SqlDbType.NVarChar,11),
												 new SqlParameter ("@receiveMsg",SqlDbType.NVarChar,500),
												 new SqlParameter ("@sucflag",SqlDbType.Char ,1),
												 new SqlParameter ("@sendMsg",SqlDbType.NVarChar ,120),  //SendNo,SendSmallNo
												 new SqlParameter ("@SendNo",SqlDbType.Char ,8),
												 new SqlParameter ("@SendSmallNo",SqlDbType.Char ,5),
												 new SqlParameter ("@OrderID",SqlDbType.NVarChar ,200),
												 new SqlParameter ("@mode",SqlDbType.Int),
												 new SqlParameter ("@smsID",SqlDbType.Int)
											 };
                            paras[0].Value = bianhao;
                            paras[1].Value = mobiles;
                            paras[2].Value = receiveMsg;
                            paras[3].Value = sFlag;
                            paras[4].Value = sendMsg;
                            paras[5].Value = SendNo;
                            paras[6].Value = SendSmallNo;
                            paras[7].Value = primaryKey;
                            paras[8].Value = mode;
                            paras[9].Value = newid;


                            var = DAL.DBHelper.ExecuteNonQuery(tran, ExSql, paras, CommandType.Text);
                            if (var > 0)
                            {
                                flag2 = true;
                            }
                            else
                            {
                                flag2 = false;
                            }
                        }
                        else
                        {//己存在同手机号码同短信内容的记录，直接返回
                            flag2 = true;
                        }
                    }
                    #endregion
                }
               
            }
            catch (Exception ex)
            {
                string msg = "";
                if (flag)
                {
                    msg = "短信己发送成功，但短信记录失败，请与管理员联系：" + ex.Message;
                }
                else
                {
                    msg = "短信发送失败，短信记录出错，请与管理员联系：" + ex.Message;
                }
                throw new Exception(msg);
            }
            return flag2;
        }


        /// <summary>
        /// 短信预设内容及开关设置
        /// </summary>
        /// <param name="mode">预设类型：1-会员注册通知；2-发货通知；3-汇款审核通知；4-应收账款通知；5-生日祝福通知；</param>
        /// <param name="defaultContent"></param>
        /// <param name="isOpen"></param>
        /// <returns></returns>
        public static  bool ConfigSMS(int mode, string defaultContent, int isOpen)
        {
            string ExSql = string .Empty ;
            string remark = string.Empty;
            switch (mode)
            {
                case 1:
                    remark = "会员注册通知";
                    break;
                case 2:
                    remark = "发货通知";
                    break;
                case 3:
                    remark = "汇款审核通知";
                    break;
                case 4:
                    remark = "应收账款通知";
                    break;
                case 5:
                    remark = "生日祝福通知";
                    break;
            }
            ExSql = "update configSms set DefaultContent=@DefaultContent,isOpen=@isOpen,remark=@remark where configCode=@configCode";
            SqlParameter[] spas = new SqlParameter[] {
            new SqlParameter ("@DefaultContent",SqlDbType.NVarChar ,1000),
            new SqlParameter ("@isOpen",SqlDbType.Int),
            new SqlParameter ("@remark",SqlDbType.NVarChar ,500),
            new SqlParameter ("@configCode",SqlDbType.Int)
            };
            spas[0].Value = defaultContent;
            spas[1].Value = isOpen;
            spas[2].Value = remark;
            spas[3].Value = mode;

            int var = DAL.DBHelper.ExecuteNonQuery(ExSql, spas, CommandType.Text);
            if (var == 0)
            {
                ExSql = "insert into configSms(DefaultContent,isOpen,remark,configCode) values(@DefaultContent,@isOpen,@remark,@configCode)";
                var = DAL.DBHelper.ExecuteNonQuery(ExSql, spas, CommandType.Text);
            }
            if (var > 0)
                return true;
            else
                return false;

        }

        /// <summary>
        /// 获取默认的短信内容，如果己设置
        /// </summary>
        /// <returns></returns>
        public DataTable GetDefaultConfig()
        {
            string ExSql = "select * from configSMS";
            DataTable dt = DAL.DBHelper.ExecuteDataTable(ExSql);
            return dt;
        }

        /// <summary>
        /// 短信单价设置
        /// </summary>
        /// <param name="UnitPrice"></param>
        /// <returns></returns>
        public bool SetSMSUnitPrice(double UnitPrice)
        {
            string ExSql = "update GlobalConfig set SMSUnitPrice=" + UnitPrice + "";
            int var = DAL.DBHelper.ExecuteNonQuery(ExSql);
            if (var == 0)
            {
                ExSql = "insert into GlobalConfig(CurrentRegisterMode,CurrentStoreMode,CurrentChangeMode,SMSUnitPrice) values(1,5,0,0.1)";
                var = DAL.DBHelper.ExecuteNonQuery(ExSql);
            }
            if (var > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 带事物的删除短信
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DelSMS(SqlTransaction tran,string id)
        {
            string sql = "delete from h_mobileMsg where id='"+id+"'";
            SqlParameter[] par = new SqlParameter[] { 
            new SqlParameter("@ID",id)
            };
            if (DAL.DBHelper.ExecuteNonQuery(tran, sql, par, CommandType.Text) > 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 根据编号获取短信信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetSMS(SqlTransaction tran,string id)
        {
            string sql = "select * from h_mobileMsg where id = (@ID)";
            SqlParameter[] par = new SqlParameter[] { 
            new SqlParameter("@ID",id)
            };

            return DAL.DBHelper.ExecuteDataTable(tran,sql,par,CommandType.Text);
        }
    }
}
