using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Model;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections;

/*
 * 修改者：     汪  华
 * 修改时间：   2009-09-04
 */



namespace DAL
{
    /// <summary>
    /// 类名：CommonDataDAL
    /// 用途：DAL公共类
    /// 创建者：
    /// 功能：
    /// 
    /// </summary>
    public class CommonDataDAL
    {
        /// <summary>
        /// 查询Lanaguage表，查询语言
        /// 宋俊
        /// </summary>
        /// <returns></returns>
        public static DataTable GetLanaguage()
        {
            return DBHelper.ExecuteDataTable("getLanaguage", CommandType.StoredProcedure);
        }
        public static DataTable getCurrency()
        {
            return DBHelper.ExecuteDataTable("getCurrency", CommandType.StoredProcedure);
        }

        /// <summary>
        /// 根据银行代码，获取银行信息bankname,BankID,BankCode,CountryID,CountryCode,countryCode,CountryForShort,CountryName
        /// </summary>
        /// <param name="BankCode"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable GetCountryBankByBankCode(SqlTransaction tran, string BankCode)
        {
            DataTable dt = new DataTable();
            string sql = @"select 
                        bankname,BankID,BankCode,CountryID,CountryCode,countryCode,CountryForShort,CountryName
                            from V_CountryBank a where a.BankCode='" + BankCode + "'";
            dt = DBHelper.ExecuteDataTable(tran, sql, null, CommandType.Text);
            return dt;
        }


        /// <summary>
        /// 根据银行代码，获取银行信息bankname,BankID,BankCode,CountryID,CountryCode,countryCode,CountryForShort,CountryName
        /// </summary>
        /// <param name="BankCode"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable GetCountryBankByBankCode(string BankCode)
        {
            DataTable dt = new DataTable();
            string sql = @"select 
                        bankname,BankID,BankCode,CountryID,CountryCode,countryCode,CountryForShort,CountryName
                            from V_CountryBank a where a.BankCode='" + BankCode + "'";
            dt = DBHelper.ExecuteDataTable(sql);
            return dt;
        }

        /// <summary>
        /// 进入店铺级别表
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="StoreId">店编号</param>
        /// <param name="level">级别</param>
        /// <param name="levelstr">级别名称</param>
        /// <returns></returns>
        public static bool UPIntoStoreLevel(SqlTransaction tran, string StoreId, int level, string levelstr, int volume)
        {
            int reslut = 0;
            SqlParameter[] param = {
                            new SqlParameter("@StoreID",SqlDbType.VarChar,20),
                            new SqlParameter("@levelstr",SqlDbType.VarChar),
                            new SqlParameter("@level",SqlDbType.Int),
                            new SqlParameter("@volume",SqlDbType.Int),
                                   };
            param[0].Value = StoreId;
            param[1].Value = levelstr;
            param[2].Value = level;
            param[3].Value = volume;
            reslut = DBHelper.ExecuteNonQuery(tran, "P_UpStoreLevel", param, CommandType.StoredProcedure);
            if (reslut < 1)
            {
                return false;
            }
            return true;


        }

        /// <summary>
        /// 获取协议
        /// </summary>
        /// <returns></returns>
        public static string GetAgreement()
        {
            return DBHelper.ExecuteScalar("select top 1 isnull(agreement,'') from agreement").ToString();
        }
        /// <summary>
        /// 获取多语言版本注册协议
        /// </summary>
        /// <param name="CountryCode">国家</param>
        /// <param name="LanguageCode">语言</param>
        /// <returns></returns>
        public static string GetAgreement(int CountryCode, string LanguageCode)
        {
            string str = "select top 1 isNull(Agreement,'') from Agreement where CountryCode=@CountryCode and LanguageCode=@LanguageCode";
            SqlParameter[] parm = new SqlParameter[]
            {
                new SqlParameter("@CountryCode",CountryCode),
                new SqlParameter("@LanguageCode",LanguageCode)
            };
            object obj = DBHelper.ExecuteScalar(str, parm, CommandType.Text);

            return obj == null ? "" : obj.ToString();
        }

        public static string GetAgreement(int CountryCode, string LanguageCode, int aType)
        {
            string str = "select top 1 isNull(Agreement,'') from Agreement where CountryCode=@CountryCode and LanguageCode=@LanguageCode and aType=@aType";
            SqlParameter[] parm = new SqlParameter[]
            {
                new SqlParameter("@CountryCode",CountryCode),
                new SqlParameter("@LanguageCode",LanguageCode),
                new SqlParameter("@aType",SqlDbType.Int)
            };
            parm[2].Value = aType;
            object obj = DBHelper.ExecuteScalar(str, parm, CommandType.Text);

            return obj == null ? "" : obj.ToString();
        }


        /// <summary>
        /// 更新协议内容
        /// </summary>
        /// <param name="agreement">协议内容</param>
        /// <returns></returns>
        public static int UpdateAgreement(string agreement)
        {
            return DBHelper.ExecuteNonQuery("update agreement set agreement=@am", new SqlParameter[1] { new SqlParameter("@am", agreement) }, CommandType.Text);
        }

        /// <summary>
        /// 更新协议内容
        /// </summary>
        /// <param name="Agreement">内容</param>
        /// <param name="CountryCode">国家</param>
        /// <param name="LanguageCode">语言</param>
        /// <returns></returns>
        public static int UpdateAgreement(string Agreement, int CountryCode, string LanguageCode)
        {
            string strSql = "update Agreement set Agreement=@Agreement where CountryCode=@CountryCode and LanguageCode=@LanguageCode";
            SqlParameter[] parms = new SqlParameter[]
            {
                new SqlParameter("@Agreement",Agreement),
                new SqlParameter("@CountryCode",CountryCode),
                new SqlParameter("@LanguageCode",LanguageCode)
            };
            return DBHelper.ExecuteNonQuery(strSql, parms, CommandType.Text);
        }

        public static int UpdateAgreement(string Agreement, int CountryCode, string LanguageCode, int aType)
        {
            string strSql = "update Agreement set Agreement=@Agreement where CountryCode=@CountryCode and LanguageCode=@LanguageCode and aType=@aType";
            SqlParameter[] parms = new SqlParameter[]
            {
                new SqlParameter("@Agreement",Agreement),
                new SqlParameter("@CountryCode",CountryCode),
                new SqlParameter("@LanguageCode",LanguageCode),
                new SqlParameter("@aType",SqlDbType.Int)
            };
            parms[3].Value = aType;
            return DBHelper.ExecuteNonQuery(strSql, parms, CommandType.Text);
        }

        /// <summary>
        /// 插入不同国家和语言的注册协议内容
        /// </summary>
        /// <param name="Agreement">内容</param>
        /// <param name="CountryCode">国家</param>
        /// <param name="LanguageCode">语言</param>
        /// <returns></returns>
        public static int InsertAgreement(string Agreement, int CountryCode, string LanguageCode)
        {
            string strSql = "Insert into Agreement values(@Agreement,@CountryCode,@LanguageCode)";
            SqlParameter[] parms = new SqlParameter[]
            {
                new SqlParameter("@Agreement",Agreement),
                new SqlParameter("@CountryCode",CountryCode),
                new SqlParameter("@LanguageCode",LanguageCode)
            };
            return DBHelper.ExecuteNonQuery(strSql, parms, CommandType.Text);
        }

        public static int InsertAgreement(string Agreement, int CountryCode, string LanguageCode, int aType)
        {
            string strSql = "Insert into Agreement values(@Agreement,@CountryCode,@LanguageCode,@aType)";
            SqlParameter[] parms = new SqlParameter[]
            {
                new SqlParameter("@Agreement",Agreement),
                new SqlParameter("@CountryCode",CountryCode),
                new SqlParameter("@LanguageCode",LanguageCode),
                new SqlParameter("@aType",SqlDbType.Int)
            };
            parms[3].Value = aType;
            return DBHelper.ExecuteNonQuery(strSql, parms, CommandType.Text);
        }

        /// <summary>
        /// 根据国家语言判断注册协议是否已添加
        /// </summary>
        /// <param name="CountryCode">国家</param>
        /// <param name="LanguageCode">语言</param>
        /// <returns></returns>
        public static int GetAgreementByCode(int CountryCode, string LanguageCode)
        {
            string strSql = "select count(1) from Agreement where CountryCode=@CountryCode and LanguageCode=@LanguageCode";
            SqlParameter[] parms = new SqlParameter[]
            {
                new SqlParameter("@CountryCode",CountryCode),
                new SqlParameter("@LanguageCode",LanguageCode)
            };

            return Convert.ToInt32(DBHelper.ExecuteScalar(strSql, parms, CommandType.Text));
        }

        public static int GetAgreementByCode(int CountryCode, string LanguageCode, int aType)
        {
            string strSql = "select count(1) from Agreement where CountryCode=@CountryCode and LanguageCode=@LanguageCode and aType=@aType";
            SqlParameter[] parms = new SqlParameter[]
            {
                new SqlParameter("@CountryCode",CountryCode),
                new SqlParameter("@LanguageCode",LanguageCode),
                new SqlParameter("@aType",SqlDbType.Int)
            };
            parms[2].Value = aType;

            return Convert.ToInt32(DBHelper.ExecuteScalar(strSql, parms, CommandType.Text));
        }

        public static DataTable GetGongGao(string isNum)
        {
            if (isNum == "1")
            {
                return DBHelper.ExecuteDataTable("select top 1  InfoTitle, Content from MessageSend where DropFlag=0 and SenderRole=0 and Receive='*' and LoginRole=2  order by Senddate desc");
            }
            else
            {
                return DBHelper.ExecuteDataTable("select top 1 InfoTitle, Content from MessageSend where DropFlag=0 and senderrole=0 and receive='*' and LoginRole=1  order by Senddate desc");
            }


        }
        public static int isYueDu(string bh, string isNum)
        {
            String sqlStr = "select gonggaotype from memberinfo where number=@num";
            SqlParameter sPara = new SqlParameter("@num", SqlDbType.NVarChar, 50);
            sPara.Value = bh;

            if (isNum == "1")
            {
                return Convert.ToInt32(DBHelper.ExecuteScalar(sqlStr, sPara, CommandType.Text));
            }
            else
            {
                sqlStr = "select gonggaotype from storeinfo where storeid=@num";
                return Convert.ToInt32(DBHelper.ExecuteScalar(sqlStr, sPara, CommandType.Text));
            }

        }

        public static DataTable GetLeverList()
        {
            DataTable dt = DAL.DBHelper.ExecuteDataTable("select bsco_level.levelint,T_translation." + System.Web.HttpContext.Current.Session["languageCode"].ToString() + " from bsco_level,T_translation where bsco_level.id=T_translation.primarykey and T_translation.tableName='bsco_level' and levelflag=0  and bsco_level.levelint!=0  order by levelint");
            return dt;
        }

        public static int isYD(string bh, string isNum)
        {
            String sqlStr = "update memberinfo set gonggaotype=1  where number=@num";
            SqlParameter sPara = new SqlParameter("@num", SqlDbType.NVarChar, 50);
            sPara.Value = bh;
            if (isNum == "1")
            {
                return Convert.ToInt32(DBHelper.ExecuteScalar(sqlStr, sPara, CommandType.Text));
            }
            else
            {
                sqlStr = "update storeinfo set gonggaotype=1 where storeid=@num";
                return Convert.ToInt32(DBHelper.ExecuteScalar(sqlStr, sPara, CommandType.Text));
            }

        }


        /// <summary>
        /// 读取系统的最大期数。
        /// 
        /// </summary>
        /// <returns></returns>
        public static int getMaxqishu()
        {
            string sql = "select max(ExpectNum) from config";
            object obj = null;
            obj = DBHelper.ExecuteScalar(sql);
            if (obj != null)
                return Convert.ToInt32(obj.ToString());
            else
                throw new Exception("缺少期数设置值");


        }

        public static bool GetRole(string tj, string number)
        {
            string strSql = "Select Count(1) from memberinfo where number=@number and " + tj;
            SqlParameter[] para = {
                                      new SqlParameter("@number",SqlDbType.NVarChar,30)
                                  };
            para[0].Value = number;
            int count = (int)DBHelper.ExecuteScalar(strSql, para, CommandType.Text);
            if (count == 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 检查对应会员的权限
        /// </summary>
        /// <param name="number">会员编号</param>
        /// <param name="xuhao">管理者序号</param>
        /// <returns></returns>
        public static bool CheckRole(string number, string xuhao)
        {
            string sql = "select count(1) from memberinfo as m ,memberinfobalance" + CommonDataDAL.getMaxqishu() + " as b where m.number=b.number and m.number=@number and b.PlacementList like '%," + xuhao + ",%'";
            SqlParameter[] para = { new SqlParameter("@number", number) };
            int count = (int)DBHelper.ExecuteScalar(sql, para, CommandType.Text);
            if (count == 0)
            {
                return false;
            }
            return true;
        }

        public static string GetWLNumber1(string manageID, out int count)
        {
            string strSql = "select isnull(number,'') as number,type from viewManage where manageid=@manageID";
            SqlParameter[] para = {
                                      new SqlParameter("@manageID",SqlDbType.NVarChar,20)
                                  };
            para[0].Value = manageID;
            DataTable dt = DBHelper.ExecuteDataTable(strSql, para, CommandType.Text);
            string topNumber = "";
            string number = "(";

            count = dt.Rows.Count;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                topNumber = dt.Rows[i]["number"].ToString();
                if (i != 0)
                {
                    number += " or ";
                }
                number += "A.number in (" + GetNumberTeam(topNumber, Convert.ToInt32(dt.Rows[i]["type"].ToString())) + ")";
            }
            number += ")";
            return number;
        }

        public static DataTable GetBindAddress(string number)
        {
            string sqlStr = @"select top 5 * from 
                            (select (ccpccode+conaddress) as test,max(orderdate) as orderdate,'' as sendway,'' as sendtype,Consignee,ConMobilPhone from memberorder 
                            where number=@number and len(ccpccode+conaddress)>8 group by ccpccode+conaddress,sendway,sendtype,Consignee,ConMobilPhone) t
                            order by orderdate desc";

            SqlParameter[] para = {
                                      new SqlParameter("@number",SqlDbType.NVarChar,30)
                                  };
            para[0].Value = number;

            DataTable dt = DBHelper.ExecuteDataTable(sqlStr, para, CommandType.Text);
            return dt;
        }

        public static DataTable GetBindStoreAddress(string storeId)
        {
            string sqlStr = @"select top 5 * from 
                              (select (cpccode+inceptaddress) as test,max(orderdatetime) as orderdatetime from storeorder 
                              where storeid=@storeId and ordertype!=2 and len(cpccode+inceptaddress)>8 group by cpccode+inceptaddress) t
                              order by orderdatetime desc";

            SqlParameter[] para = {
                                      new SqlParameter("@storeId",SqlDbType.NVarChar,30)
                                  };
            para[0].Value = storeId;

            DataTable dt = DBHelper.ExecuteDataTable(sqlStr, para, CommandType.Text);
            return dt;
        }

        public static DataTable GetBindOrderAddress(string StoreId)
        {
            string sqlStr = @"select top 5 * from 
                              (select (cpccode+inceptaddress) as test,max(orderdatetime) as orderdatetime from ordergoods 
                              where storeid=@StoreId and ordertype!=2 and len(cpccode+inceptaddress)>8 group by cpccode+inceptaddress) t
                              order by orderdatetime desc";

            SqlParameter[] para = {
                                      new SqlParameter("@StoreId",SqlDbType.NVarChar,30)
                                  };
            para[0].Value = StoreId;

            DataTable dt = DBHelper.ExecuteDataTable(sqlStr, para, CommandType.Text);
            return dt;
        }

        public static string GetAddressByCode(string code)
        {
            string strSql = @"Select top 1 isnull( Country+' '+Province+' '+City+' '+xian ,'') From City Where cpccode = @code";
            SqlParameter[] para = {
                                      new SqlParameter("@code",SqlDbType.NVarChar,20)
                                  };
            para[0].Value = code;
            string address = DBHelper.ExecuteScalar(strSql, para, CommandType.Text).ToString(); ;
            return address;
        }

        public static string GetWLNumber(string manageID, out int count)
        {
            string strSql = "select isnull(number,'') number,type from viewManage where manageid=@manageID";
            SqlParameter[] para = {
                                      new SqlParameter("@manageID",SqlDbType.NVarChar,20)
                                  };
            para[0].Value = manageID;
            DataTable dt = DBHelper.ExecuteDataTable(strSql, para, CommandType.Text);
            string topNumber = "";
            string number = "(";

            count = dt.Rows.Count;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                topNumber = dt.Rows[i]["number"].ToString();
                if (i != 0)
                {
                    number += " or ";
                }
                number += "number in (" + GetNumberTeam(topNumber, Convert.ToInt32(dt.Rows[i]["type"].ToString())) + ")";
            }
            number += ")";

            if (number == "()")
            {
                count = 1;
                number = " 1=2 ";
            }
            return number;
        }

        public static string GetWLNumber(string manageID, out int count, string tName)
        {
            string strSql = "select isnull(number,'') number,type from viewManage where manageid=@manageID";
            SqlParameter[] para = {
                                      new SqlParameter("@manageID",SqlDbType.NVarChar,20)
                                  };
            para[0].Value = manageID;
            DataTable dt = DBHelper.ExecuteDataTable(strSql, para, CommandType.Text);
            string topNumber = "";
            string number = "(";

            count = dt.Rows.Count;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                topNumber = dt.Rows[i]["number"].ToString();
                if (i != 0)
                {
                    number += " or ";
                }
                number += tName + ".number in (" + GetNumberTeam(topNumber, Convert.ToInt32(dt.Rows[i]["type"].ToString())) + ")";
            }
            number += ")";
            return number;
        }
        /// <summary>
        /// 获取对应编号的序号
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string GetNumberXuhao(string number)
        {
            string sql = "select isnull(xuhao,-1) as xuhao from memberinfo where number=@number";
            SqlParameter[] para = { new SqlParameter("@number", number) };
            DataTable dt = DBHelper.ExecuteDataTable(sql, para, CommandType.Text);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["xuhao"].ToString();
            } return "";
        }

        public static string GetNumberTeam(string topNumber, int type)
        {
            string field = "";
            if (type == 1)
            {
                field = "PlacementList";
            }
            else
            {
                field = "DirectList";
            }
            //int startXH = 0, endXH = 0, baseCengwei = 0;

            //getXHFW(topNumber, true, CommonDataDAL.getMaxqishu(), out startXH, out endXH, out baseCengwei);
            //string strSql = "select J.number from MemberInfoBalance" + CommonDataDAL.getMaxqishu() + " as J where J." + field + ">=" + startXH.ToString() + " and J." + field + "<=" + endXH.ToString();
            string strSql = "select J." + field + " from MemberInfoBalance" + CommonDataDAL.getMaxqishu() + " as J where j.number='" + topNumber + "'";
            string numberstr = DBHelper.ExecuteScalar(strSql).ToString();
            string str = "'" + topNumber + "'";
            if (numberstr != "")
            {
                string number = "'" + numberstr.Substring(0, numberstr.Length - 1).Replace(",", "','") + "'";
                string selectnumber = "select number from memberinfo where xuhao in(" + number + ")";
                DataTable dt = DBHelper.ExecuteDataTable(selectnumber);

                foreach (DataRow dr in dt.Rows)
                {
                    str += ",'" + dr["number"].ToString() + "'";
                }
            }
            return str;
        }


        /// <summary>
        /// 求某编号的序号范围 ： 
        /// </summary>
        /// <param name="number">编号</param>
        /// <param name="anzhi">null为推荐，否则为安置</param>
        /// <param name="qishu">期数</param>
        /// <param name="sXuhao">起始序号</param>
        /// <param name="eXuhao">终止序号</param>
        public static void getXHFW(string bianhao, bool isAnZhi, int qishu, out int sXuhao, out int eXuhao, out int Cengwei)
        {
            string myXuhao, myCengwei;
            Cengwei = 9999;
            if (isAnZhi)
            {
                myXuhao = "Ordinal1";
                myCengwei = "LayerBit1";
            }
            else
            {
                myXuhao = "Ordinal2";
                myCengwei = "LayerBit2";
            }
            //获取最大序号
            object maxNum = DAL.DBHelper.ExecuteScalar("SELECT MAX(" + myXuhao + ") as mShu FROM MemberInfoBalance" + qishu.ToString(), CommandType.Text);
            if (maxNum == System.DBNull.Value)
            {
                eXuhao = 0;
            }
            else
            {
                eXuhao = Convert.ToInt32(maxNum);
            }

            sXuhao = eXuhao + 1;
            //获取输入会员的层位和序号
            SqlDataReader dr;

            string sql = "SELECT   isnull(" + myCengwei + ",0)  as  " + myCengwei + " , isnull(" + myXuhao + ",0)  as  " + myXuhao + " FROM MemberInfoBalance" + qishu.ToString() + " WHERE number = @num";
            SqlParameter spa = new SqlParameter("@num", SqlDbType.NVarChar, 50);
            spa.Value = bianhao;
            dr = DAL.DBHelper.ExecuteReader(sql, spa, CommandType.Text);
            if (dr.Read())
            {
                Cengwei = Convert.ToInt32(dr[myCengwei]);
                sXuhao = Convert.ToInt32(dr[myXuhao]);
            }
            dr.Close();

            //确定终止序号
            int lsXuhao = Convert.ToInt32(DAL.DBHelper.ExecuteScalar("SELECT " + myXuhao + " FROM MemberInfoBalance" + qishu.ToString() + " WHERE " + myCengwei + "<=" + Cengwei.ToString() + " AND " + myXuhao + ">" + sXuhao.ToString() + " ORDER BY " + myXuhao + " ASC", CommandType.Text));
            if (lsXuhao > 0)
            {
                eXuhao = lsXuhao - 1;
            }
        }

        public static int GetRegisterQishu(string number)
        {
            string sql = "Select ExpectNum From MemberInfo Where Number=@Number";
            SqlParameter[] para = {
                                      new SqlParameter("@Number",SqlDbType.NVarChar,30)
                                  };
            para[0].Value = number;
            return (int)DBHelper.ExecuteScalar(sql, para, CommandType.Text);
        }

        public static string GetManageID(int type)
        {
            string sql = "";
            string ManageId = "";
            switch (type)
            {
                case 1:
                    if (System.Web.HttpContext.Current.Application["TopManageID"] == null)
                    {
                        sql = "Select top 1 Number From Manage Where DefaultManager=1";
                        ManageId = DBHelper.ExecuteScalar(sql).ToString();

                        System.Web.HttpContext.Current.Application["TopManageID"] = ManageId;
                    }
                    else
                    {
                        ManageId = System.Web.HttpContext.Current.Application["TopManageID"].ToString();
                    }
                    break;
                case 2:
                    if (System.Web.HttpContext.Current.Application["TopStoreID"] == null)
                    {
                        sql = "Select top 1 StoreID From StoreInfo Where DefaultStore=1";
                        ManageId = DBHelper.ExecuteScalar(sql).ToString();

                        System.Web.HttpContext.Current.Application["TopStoreID"] = ManageId;
                    }
                    else
                    {
                        ManageId = System.Web.HttpContext.Current.Application["TopStoreID"].ToString();
                    }
                    break;
                case 3:
                    if (System.Web.HttpContext.Current.Application["TopMemberID"] == null)
                    {
                        sql = "Select top 1 Number From MemberInfo Where DefaultNumber=1";
                        ManageId = DBHelper.ExecuteScalar(sql).ToString();

                        System.Web.HttpContext.Current.Application["TopMemberID"] = ManageId;
                    }
                    else
                    {
                        ManageId = System.Web.HttpContext.Current.Application["TopMemberID"].ToString();
                    }
                    break;
                default:
                    break;
            }

            return ManageId;
        }

        /// <summary>
        /// 判断会员编号是否存在
        /// </summary>
        /// <returns></returns>
        public static int getCountNumber(string number)
        {
            string sql = "select count(id) from memberinfo where number=@number";
            SqlParameter[] para = {
                                      new SqlParameter("@number",SqlDbType.NVarChar,50)
                                  };
            para[0].Value = number;
            int count = (int)DBHelper.ExecuteScalar(sql, para, CommandType.Text);
            return count;
        }

        public static bool SetResetEmail(string email, string username, string pwd, string emailAddress)
        {
            string sql = "select count(*) from email";
            int count = (int)DBHelper.ExecuteScalar(sql);
            if (count == 0)
            {
                sql = @"insert into email values(@Email,@UserName,@Password,@EmailAddress)";
            }
            else
            {
                sql = @"Update Email Set email=@Email,username=@UserName,password=@Password,emailaddress=@EmailAddress";
            }
            SqlParameter[] para = {
                                      new SqlParameter("@Email",SqlDbType.NVarChar,50),
                                      new SqlParameter("@UserName",SqlDbType.NVarChar,50),
                                      new SqlParameter("@Password",SqlDbType.NVarChar,50),
                                      new SqlParameter("@EmailAddress",SqlDbType.NVarChar,50)
                                  };
            para[0].Value = email;
            para[1].Value = username;
            para[2].Value = pwd;
            para[3].Value = emailAddress;

            count = (int)DBHelper.ExecuteNonQuery(sql, para, CommandType.Text);
            if (count == 0)
                return false;
            return true;
        }

        public static DataTable GetEmail()
        {
            string sql = "select top 1 * from email";
            DataTable dt = DBHelper.ExecuteDataTable(sql);
            return dt;
        }

        public static bool SetMemberTotalDefray(string number, double totalMoney)
        {
            string sql = "Update MemberInfo Set TotalDefray=TotalDefray+@TotalDefray Where Number=@Number";
            SqlParameter[] para = {
                                      new SqlParameter("@TotalDefray",SqlDbType.Money),
                                      new SqlParameter("@Number",SqlDbType.NVarChar,30)
                                  };
            para[0].Value = totalMoney;
            para[1].Value = number;

            int count = (int)DBHelper.ExecuteNonQuery(sql, para, CommandType.Text);
            if (count == 0)
                return false;
            return true;
        }

        public static bool SetMemberTotalDefray(SqlTransaction tran, string number, double totalMoney)
        {
            string sql = "Update MemberInfo Set TotalDefray=TotalDefray+@TotalDefray Where Number=@Number";
            SqlParameter[] para = {
                                      new SqlParameter("@TotalDefray",SqlDbType.Money),
                                      new SqlParameter("@Number",SqlDbType.NVarChar,30)
                                  };
            para[0].Value = totalMoney;
            para[1].Value = number;

            int count = (int)DBHelper.ExecuteNonQuery(tran, sql, para, CommandType.Text);
            if (count == 0)
                return false;
            return true;
        }

        public static bool SetMemberTotalRemittances(SqlTransaction tran, string number, double totalMoney)
        {

            string sql = "Update MemberInfo Set TotalRemittances=TotalRemittances+@TotalRemittances Where Number=@Number";
            SqlParameter[] para = {
                                      new SqlParameter("@TotalRemittances",SqlDbType.Money),
                                      new SqlParameter("@Number",SqlDbType.NVarChar,30)
                                  };
            para[0].Value = totalMoney;
            para[1].Value = number;

            int count = (int)DBHelper.ExecuteNonQuery(tran, sql, para, CommandType.Text);
            if (count == 0)
                return false;
            return true;
        }

        public static string getManageName(string manageNumber)
        {
            string sql = "Select Name From manage where number=@number";
            SqlParameter[] para = {
                                      new SqlParameter("@number",SqlDbType.NVarChar,30)
                                  };
            para[0].Value = manageNumber;

            return DBHelper.ExecuteScalar(sql, para, CommandType.Text).ToString();
        }

        public static string getOutStorageOrderId(string storeOrderId)
        {
            string sql = "select isnull(outStorageOrderId,'') From StoreOrder Where StoreOrderId=@StoreOrderID";
            SqlParameter[] para = {
                                      new SqlParameter("@StoreOrderID",SqlDbType.NVarChar,30)
                                  };
            para[0].Value = storeOrderId;

            return DBHelper.ExecuteScalar(sql, para, CommandType.Text).ToString();
        }

        public static int GetMemberOrder(string date, string storeId)
        {
            string sql = "select count(0) from memberorder where orderdate>=@OrderDate and defraystate=0 and StoreId=@StoreId";
            SqlParameter[] para = {
                                      new SqlParameter("@OrderDate",SqlDbType.DateTime),
                                      new SqlParameter("@StoreId",SqlDbType.NVarChar,30)
                                  };
            para[0].Value = Convert.ToDateTime(date);
            para[1].Value = storeId;
            return (int)DBHelper.ExecuteScalar(sql, para, CommandType.Text);
        }

        public static int GetDefrayState(string OrderId)
        {
            string sql = "Select DefrayState From MemberOrder Where OrderId=@OrderId";
            SqlParameter[] para = {
                                      new SqlParameter("@OrderId",SqlDbType.NVarChar,30)
                                  };
            para[0].Value = OrderId;

            return (int)DBHelper.ExecuteScalar(sql, para, CommandType.Text);
        }

        public static int GetStoreOrder(string storeId)
        {
            string sql = "select count(0) from StoreOrder where isSent='Y' and sendway=0 and isReceived='N'and StoreId=@StoreId"; // and sendtype=1 
            SqlParameter[] para = {
                                      new SqlParameter("@StoreId",SqlDbType.NVarChar,30)
                                  };
            para[0].Value = storeId;
            return (int)DBHelper.ExecuteScalar(sql, para, CommandType.Text);
        }

        public static DataTable GetMessageSend()
        {
            string sql = @"select top 5 ID, LoginRole, Receive, InfoTitle, Content, SenderRole, Sender, Senddate, DropFlag, ReadFlag from MessageSend 
                        where DropFlag=0 and senderrole=0 and receive='*' and LoginRole=1  order by Senddate desc";

            DataTable dt = DBHelper.ExecuteDataTable(sql);
            return dt;
        }

        public static DataTable GetMessageReceive(string storeId)
        {
            string sql = "select top 5 * from MessageReceive where DropFlag=0 and LoginRole=1 and Receive=@Receive order by Senddate desc";
            SqlParameter[] para = {
                                      new SqlParameter("@Receive",SqlDbType.NVarChar,30)
                                  };
            para[0].Value = storeId;
            DataTable dt = DBHelper.ExecuteDataTable(sql, para, CommandType.Text);
            return dt;
        }

        public static bool SetSendType(string OrderID)
        {
            string sql = "Update MemberOrder Set isreceivables=1 Where OrderId=@OrderID";
            SqlParameter[] para = {
                                      new SqlParameter("@OrderID",SqlDbType.NVarChar,30)
                                  };
            para[0].Value = OrderID;
            int count = (int)DBHelper.ExecuteNonQuery(sql, para, CommandType.Text);
            if (count == 0)
                return false;
            return true;
        }

        public static int getDeliveryflag(string OrderID)
        {
            string sql = "select Deliveryflag from Ordergoods where memberorderid=@OrderID";
            SqlParameter[] para = {
                                      new SqlParameter("@OrderID",SqlDbType.NVarChar,30)
                                  };
            para[0].Value = OrderID;
            DataTable dt = DBHelper.ExecuteDataTable(sql, para, CommandType.Text);
            if (dt.Rows.Count == 0)
            {
                return -1;
            }
            return Convert.ToInt32(dt.Rows[0]["Deliveryflag"]);
        }

        public static string getGrantState()
        {
            string sql = "select top 1 isnull(grantState,0) from GrantSet";
            DataTable dt = DBHelper.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            return "0";
        }

        public static bool SetGrantState(SqlTransaction tran, string grantState)
        {
            int count = 0;
            string sql = "select count(0) from GrantSet";
            count = (int)DBHelper.ExecuteScalar(tran, sql, CommandType.Text);

            if (count > 0)
            {
                sql = "Update GrantSet Set GrantState=@GrantState";
                SqlParameter[] para = {
                                          new SqlParameter("@GrantState",SqlDbType.Int)
                                      };
                para[0].Value = grantState;
                count = (int)DBHelper.ExecuteNonQuery(tran, sql, para, CommandType.Text);
            }
            else
            {
                sql = "Insert Into GrantSet (GrantState,SetExpectNum) values(@GrantState,@SetExpectNum)";
                SqlParameter[] para = {
                                          new SqlParameter("@GrantState",SqlDbType.Int),
                                          new SqlParameter("@SetExpectNum",SqlDbType.Int)
                                      };
                para[0].Value = grantState;
                para[1].Value = CommonDataDAL.getMaxqishu();

                count = (int)DBHelper.ExecuteNonQuery(tran, sql, para, CommandType.Text);
            }
            if (count == 0)
                return false;
            return true;
        }

        /// <summary>
        ///会员提现设置 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="WithdrawMinMoney">提现最低金额</param>
        /// <param name="WithdrawSXF">提现手续费比例</param>
        /// <returns></returns>
        public static bool SetGrantTx(SqlTransaction tran, string WithdrawMinMoney,string WithdrawSXF)
        {
            int count = 0;
            string sql = "select count(0) from WithdrawSz";
            count = (int)DBHelper.ExecuteScalar(tran, sql, CommandType.Text);

            if (count > 0)
            {
                sql = "Update WithdrawSz Set WithdrawMinMoney=@WithdrawMinMoney,WithdrawSXF=@WithdrawSXF";
                SqlParameter[] para = {
                                          new SqlParameter("@WithdrawMinMoney",SqlDbType.Int),
                                           new SqlParameter("@WithdrawSXF",SqlDbType.Money)
                                      };
                para[0].Value = Convert.ToInt32(WithdrawMinMoney);
                para[1].Value = Convert.ToDouble(WithdrawSXF);
                count = (int)DBHelper.ExecuteNonQuery(tran, sql, para, CommandType.Text);
            }
            else
            {
                sql = "Insert Into WithdrawSz (WithdrawMinMoney,WithdrawSXF) values(@WithdrawMinMoney,@WithdrawSXF)";
                SqlParameter[] para = {
                                          new SqlParameter("@WithdrawMinMoney",SqlDbType.Int),
                                           new SqlParameter("@WithdrawSXF",SqlDbType.Money)
                                      };
                para[0].Value = Convert.ToInt32(WithdrawMinMoney);
                para[1].Value = Convert.ToDouble(WithdrawSXF);

                count = (int)DBHelper.ExecuteNonQuery(tran, sql, para, CommandType.Text);
            }
            if (count == 0)
                return false;
            return true;
        }

        public static bool Jinliu(SqlTransaction tran, string Dhktime, string Dcstime, string sfgc, string hfgc, string tkje, string hkje,string txtEnote)
        {
            int count = 0; int count2 = 0; int count3 = 0; int count4 = 0; int count5 = 0; int count6 = 0; int count7 = 0;
            string sql = "Update JLparameter Set value=@value where jlcid=1";
            SqlParameter[] para = {
                                          new SqlParameter("@value",SqlDbType.Int),
                                           
                                      };
            para[0].Value = Convert.ToInt32(Dhktime);
            count = (int)DBHelper.ExecuteNonQuery(tran, sql, para, CommandType.Text);

            string sql2 = "Update JLparameter Set value=@value where jlcid=2";
            SqlParameter[] para2 = {
                                          new SqlParameter("@value",SqlDbType.Int),

                                      };
            para2[0].Value = Convert.ToInt32(Dcstime);
            count2 = (int)DBHelper.ExecuteNonQuery(tran, sql2, para2, CommandType.Text);



            string sql3 = "Update JLparameter Set value=@value where jlcid=3";
            SqlParameter[] para3 = {
                                          new SqlParameter("@value",SqlDbType.Int),

                                      };
            para3[0].Value = Convert.ToInt32(sfgc);
            count3 = (int)DBHelper.ExecuteNonQuery(tran, sql3, para3, CommandType.Text);

            string sql4 = "Update JLparameter Set value=@value where jlcid=4";
            SqlParameter[] para4 = {
                                          new SqlParameter("@value",SqlDbType.Int),

                                      };
            para4[0].Value = Convert.ToInt32(hfgc);
            count4 = (int)DBHelper.ExecuteNonQuery(tran, sql4, para4, CommandType.Text);


            string sql5= "Update JLparameter Set value=@value where jlcid=5";
            SqlParameter[] para5 = {
                                          new SqlParameter("@value",SqlDbType.Int),

                                      };
            para5[0].Value = Convert.ToInt32(tkje);
            count5 = (int)DBHelper.ExecuteNonQuery(tran, sql5, para5, CommandType.Text);


            string sql6 = "Update JLparameter Set value=@value where jlcid=6";
            SqlParameter[] para6 = {
                                          new SqlParameter("@value",SqlDbType.Int),

                                      };
            para6[0].Value = Convert.ToInt32(hkje);
            count6 = (int)DBHelper.ExecuteNonQuery(tran, sql6, para6, CommandType.Text);





            string sql7 = "Update JLparameter Set describe=@describe where jlcid=8";
            SqlParameter[] para7= {
                                          new SqlParameter("@describe",SqlDbType.NVarChar,4000),

                                      };
            para7[0].Value = txtEnote;
            count7 = (int)DBHelper.ExecuteNonQuery(tran, sql7, para7, CommandType.Text);


            if (count == 0 || count2 == 0 || count3 == 0 || count4 == 0 || count5 == 0 || count6 == 0 || count7 == 0)
                return false;
            return true;
        }




        public static bool SetGrantMenu(SqlTransaction tran, string grantState)
        {
            if (grantState == "1")
            {
                string sql = "Update menuleft Set isfold=1 Where id=153";
                int count = 0;
                count = (int)DBHelper.ExecuteNonQuery(tran, sql, null, CommandType.Text);
                if (count == 0)
                    return false;

                //sql = "Update menuleft Set isfold=1 where id=93";
                //count = (int)DBHelper.ExecuteNonQuery(tran, sql, null, CommandType.Text);
                //if (count == 0)
                //    return false;

                sql = "Update menu Set isfold=1 Where sortid=261";
                count = (int)DBHelper.ExecuteNonQuery(tran, sql, null, CommandType.Text);
                if (count == 0)
                    return false;

            }
            else
            {
                string sql = "Update menuleft Set isfold=0 Where id=153";
                int count = 0;
                count = (int)DBHelper.ExecuteNonQuery(tran, sql, null, CommandType.Text);
                if (count == 0)
                    return false;

                //sql = "Update menuleft Set isfold=0 where id=93";
                //count = (int)DBHelper.ExecuteNonQuery(tran, sql, null, CommandType.Text);
                //if (count == 0)
                //    return false;

                sql = "Update menu Set isfold=0 Where sortid=261";
                count = (int)DBHelper.ExecuteNonQuery(tran, sql, null, CommandType.Text);
                if (count == 0)
                    return false;
            }
            return true;
        }

        public static string GetElectronicaccountid(string number)
        {
            string sql = "select electronicaccountid from memberorder where number=@number and isagain=0";
            SqlParameter[] para = {
                                    new SqlParameter("@number",SqlDbType.NVarChar,20)
                                  };
            para[0].Value = number;
            string electronicaccountid = DBHelper.ExecuteScalar(sql, para, CommandType.Text).ToString();
            return electronicaccountid;
        }

        public static bool CheckStoreId(string storeId)
        {
            string strSql = "select count(storeid) from storeinfo where storeid=@storeId";
            SqlParameter[] para = {
                                      new SqlParameter("@storeId",SqlDbType.NVarChar,20)
                                  };
            para[0].Value = storeId;
            int count = (int)DBHelper.ExecuteScalar(strSql, para, CommandType.Text);
            if (count == 0)
            {
                return false;
            }
            return true;
        }

        public static bool CheckStoreId1(string storeId)
        {
            string strSql = "select StoreState from storeinfo where storeid=@storeId";
            SqlParameter[] para = {
                                      new SqlParameter("@storeId",SqlDbType.NVarChar,20)
                                  };
            para[0].Value = storeId;
            int count = (int)DBHelper.ExecuteScalar(strSql, para, CommandType.Text);
            if (count == 0)
            {
                return false;
            }
            return true;
        }

        public static int TXset()
        {
            string strSql = "select isfold   from menu where storeid=@storeId";
            SqlParameter[] para = {
                                      new SqlParameter("@storeId",SqlDbType.NVarChar,20)
                                  };
            para[0].Value = "261";
            int count = (int)DBHelper.ExecuteScalar(strSql, para, CommandType.Text);
            if (count == 0)
            {
                return 0;
            }
            return 1;
        }


        public static string GetDirect(string number)
        {
            string strSql = "select direct from MemberInfo where number=@num";
            SqlParameter spa = new SqlParameter("@num", SqlDbType.NVarChar, 50);
            spa.Value = number;
            string upBianhao = DBHelper.ExecuteScalar(strSql, spa, CommandType.Text).ToString();
            return upBianhao;
        }

        public static string GetPname(string number)
        {
            string strSql = "select petname from MemberInfo where number=@num";
            SqlParameter spa = new SqlParameter("@num", SqlDbType.NVarChar, 50);
            spa.Value = number;
            string petname = DBHelper.ExecuteScalar(strSql, spa, CommandType.Text).ToString();
            return petname;
        }

        /// <summary>
        /// 获取会员的注册期数
        /// </summary>
        /// <returns></returns>
        public static int getNumberRegExpect(string number)
        {
            string sql = "select expectNum from memberinfo where number=@number";
            SqlParameter[] para = {
                                      new SqlParameter("@number",SqlDbType.NVarChar,20)
                                  };
            para[0].Value = number;
            int Expect = (int)DBHelper.ExecuteScalar(sql, para, CommandType.Text);
            return Expect;
        }

        /// <summary>
        /// 获取店长编号
        /// </summary>
        /// <returns></returns>
        public static string getStoreNumber(string storeId)
        {
            string sql = "select number from storeInfo where storeId=@storeId";
            SqlParameter[] para = {
                                      new SqlParameter("@storeId",SqlDbType.NVarChar,20)
                                  };
            para[0].Value = storeId;
            string tBh = DBHelper.ExecuteScalar(sql, para, CommandType.Text).ToString();
            return tBh;
        }

        /// <summary>
        /// 获取手续费
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string GetLeftsxfMoney(string money)
        {
            DataTable strSql = DAL.DBHelper.ExecuteDataTable("select WithdrawSXF from WithdrawSz");
            string sxf = strSql.Rows[0]["WithdrawSXF"].ToString();
            string leftSXF = (Convert.ToDouble(money) * Convert.ToDouble(sxf)).ToString("#0.00");
            return leftSXF;
        }
        public static string GetLefwyjMoney(string money)
        {
            //DataTable strSql = DAL.DBHelper.ExecuteDataTable("select value from JLparameter where parameter='sfgc'");
            //string wyj = strSql.Rows[0]["value"].ToString();
            double bl = Convert.ToDouble(DAL.MemberInfoDAL.JFparameter("sfgc")) / 100;
            string leftWYJ = (Convert.ToDouble(money) * bl).ToString("#0.00");
            return leftWYJ;
        }


        /// <summary>
        /// 获取最低提现金额
        /// </summary>
        /// <returns></returns>
        public static string GetMinTxMoney()
        {
            object o_mintxMoney = DAL.DBHelper.ExecuteScalar("select WithdrawMinMoney from WithdrawSz");
            if (o_mintxMoney != null)
            {
                return Convert.ToDouble(o_mintxMoney).ToString("0.00");
            }
            else
                return "0.00";
        }

        public static string GetLeftMoney(string number)
        {
            string strSql = "select top 1 isnull(jackpot,0)-isnull(out,0)-isnull(membership,0) from memberInfo where number=@number";
            SqlParameter[] para = {
                                      new SqlParameter("@number",SqlDbType.NVarChar,50)
                                  };
            para[0].Value = number;
            string leftMoney = Convert.ToDouble(DBHelper.ExecuteScalar(strSql, para, CommandType.Text)).ToString("0.00");
            return leftMoney;
        }



        /// <summary>
        /// 奖金账户实际余额
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string GetLeftMoney1(string number)
        {
            string strSql = "select top 1 isnull(jackpot,0)-isnull(out,0) from memberInfo where number=@number";
            SqlParameter[] para = {
                                      new SqlParameter("@number",SqlDbType.NVarChar,50)
                                  };
            para[0].Value = number;
            string leftMoney = Convert.ToDouble(DBHelper.ExecuteScalar(strSql, para, CommandType.Text)).ToString("0.00");
            return leftMoney;
        }

        /// <summary>
        /// 判断会员编号是否存在
        /// </summary>
        /// <returns></returns>
        public static string getNumberStoreID(string number)
        {
            string sql = "select top 1 storeid from memberinfo where number=@number";
            SqlParameter[] para = {
                                      new SqlParameter("@number",SqlDbType.NVarChar,50)
                                  };
            para[0].Value = number;
            string storeId = DBHelper.ExecuteScalar(sql, para, CommandType.Text).ToString();
            return storeId;
        }

        /// <summary>
        /// 获取剩余逻辑库存
        /// </summary>
        /// <returns></returns>
        public static int GetLeftLogicProductInventory(int productID)
        {
            string strSql = "select count(productid) from LogicProductInventory where productid=@productID";

            SqlParameter[] para = {
                                      new SqlParameter("@productID",productID)
                                  };
            int count = (int)DBHelper.ExecuteScalar(strSql, para, CommandType.Text);
            if (count == 0)
            {
                return count;
            }

            string sql = "select top 1 isnull((totalin-totalout),0) as leftQuantity from LogicProductInventory where productid=@productID";
            SqlParameter[] para1 = {
                                      new SqlParameter("@productID",productID)
                                  };
            int leftQuantity = Convert.ToInt32(DBHelper.ExecuteScalar(sql, para1, CommandType.Text));
            return leftQuantity;
        }


        /// <summary>
        /// 读取证件编码
        /// </summary>
        /// <returns></returns>
        public static string getPaperType(int id)
        {
            string sql = "select PaperTypeCode from bsco_PaperType where id=@id";
            SqlParameter[] para = {
                                      new SqlParameter("@id",SqlDbType.Int)
                                  };
            para[0].Value = id;
            string paperCode = DBHelper.ExecuteScalar(sql, para, CommandType.Text).ToString();
            return paperCode;
        }

        /// <summary>
        /// 判断管理员是否已有此网络的查看权限
        /// </summary>
        /// <returns></returns>
        public static int getNumberRole(string number, string manageID, int type)
        {
            string sql = "select count(id) from viewmanage where number=@number and manageid=@manageID and type=@type";
            SqlParameter[] para = {
                                      new SqlParameter("@number",SqlDbType.NVarChar,20),
                                      new SqlParameter("@manageID",SqlDbType.NVarChar,20),
                                      new SqlParameter("@type",SqlDbType.Int)
                                  };
            para[0].Value = number;
            para[1].Value = manageID;
            para[2].Value = type;
            int count = (int)DBHelper.ExecuteScalar(sql, para, CommandType.Text);
            return count;
        }

        /// <summary>
        /// 判断随即获取的编号是否已存在
        /// </summary>
        /// <returns></returns>
        public static int getNumber(string number)
        {
            string strSql = "select count(Number) from MemberInfo where Number=@number";
            SqlParameter[] para = {
                                      new SqlParameter("@number",number)
                                  };

            int count = (int)DBHelper.ExecuteScalar(strSql, para, CommandType.Text);
            return count;
        }

        /// <summary>
        /// 添加管理员可以查看团队的网络图
        /// 
        /// </summary>
        /// <returns></returns>
        public static int AddViewNumber(string number, string manageID, int type)
        {
            string sql = "insert into viewmanage(manageID,number,type) values(@manageID,@number,@type)";
            SqlParameter[] para = {
                                      new SqlParameter("@number",SqlDbType.NVarChar,20),
                                      new SqlParameter("manageID",SqlDbType.NVarChar,20),
                                      new SqlParameter("@type",SqlDbType.Int)
                                  };
            para[0].Value = number;
            para[1].Value = manageID;
            para[2].Value = type;
            int count = (int)DBHelper.ExecuteNonQuery(sql, para, CommandType.Text);
            return count;
        }

        /// <summary>
        /// 读取级别。
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetLevel(string level, int levelType)
        {
            string levelStr = "";
            string sql = "select top 1 isnull(icopath,'') from BSCO_level where levelflag=@levelType and levelint = @level";
            SqlParameter[] para = {
                                      new SqlParameter("@levelType",SqlDbType.Int),
                                      new SqlParameter("@level",SqlDbType.NVarChar,10)
                                  };
            para[0].Value = levelType;
            para[1].Value = level;

            DataTable dt = DBHelper.ExecuteDataTable(sql, para, CommandType.Text);
            if (dt.Rows.Count > 0)
            {
                levelStr = dt.Rows[0][0].ToString();
            }
            else
            {
                levelStr = DBHelper.ExecuteScalar("select top 1 isnull(icopath,'')  from BSCO_level where levelflag=0 and levelint = 0").ToString();
            }
            return levelStr;
        }

        /// <summary>
        /// 读取级别。
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetLevel1(string level, int levelType)
        {
            string levelStr = "";
            string sql = "select top 1 isnull(icopath,'') from BSCO_level where levelflag=@levelType and levelint = @level";
            SqlParameter[] para = {
                                      new SqlParameter("@levelType",SqlDbType.Int),
                                      new SqlParameter("@level",SqlDbType.NVarChar,10)
                                  };
            para[0].Value = levelType;
            para[1].Value = level;

            DataTable dt = DBHelper.ExecuteDataTable(sql, para, CommandType.Text);
            if (dt.Rows.Count > 0)
            {
                levelStr = dt.Rows[0][0].ToString();
            }
            else
            {
                levelStr = DBHelper.ExecuteScalar("select top 1 isnull(icopath,'')  from BSCO_level where levelflag=0 and levelint = 0").ToString();
            }
            levelStr = levelStr.Substring(3, levelStr.Length - 3);
            return levelStr;
        }

        public static string GetStrLevel(int level)
        {
            string sql = "Select top 1 isnull(levelstr,0) from BSCO_level where levelflag=0 and levelint=@num";
            SqlParameter spa = new SqlParameter("@num", SqlDbType.Int);
            spa.Value = level;
            string strLevel = DBHelper.ExecuteScalar(sql, spa, CommandType.Text).ToString();
            return strLevel;
        }



        /// <summary>
        /// 读取系统的工资未发布的期数。
        /// 
        /// </summary>
        /// <returns></returns>
        public static ArrayList getNotFabuqishu()
        {
            ArrayList list = new ArrayList();
            string sql = "select ExpectNum from config where IsSuance=0";
            SqlDataReader reader = DBHelper.ExecuteReader(sql);
            while (reader.Read())
            {
                list.Add(reader[0].ToString().Trim());
            }
            reader.Close();
            return list;
        }
        /// <summary>
        /// 获取城市编号--用于四级联动
        /// </summary>
        /// <param name="country">国家</param>
        /// <param name="province">省份</param>
        /// <param name="city">城市</param>
        /// <returns>城市编号 </returns>
        public static string GetCPCCode(string country, string province, string city, string xian)
        {
            country = country == "" ? ("请选择") : (country);
            province = province == "" ? ("请选择") : (province);
            city = city == "" ? ("请选择") : (city);
            xian = xian == "" ? ("请选择") : (xian);
            SqlParameter[] paras = new SqlParameter[4];
            paras[0] = new SqlParameter("@country", country);
            paras[1] = new SqlParameter("@province", province);
            paras[2] = new SqlParameter("@city", city);
            paras[3] = new SqlParameter("@xian", xian);
            object obj = DBHelper.ExecuteScalar("select cpccode from city where country=@country and province=@province and City=@city and Xian=@xian ", paras, CommandType.Text);
            if (obj == null)
                return "";
            else
                return obj.ToString();
        }
        /// <summary>
        /// 获取城市编号
        /// </summary>
        /// <param name="country">国家</param>
        /// <param name="province">省份</param>
        /// <param name="city">城市</param>
        /// <returns>城市编号 </returns>
        public static string GetCPCCode(string country, string province, string city)
        {
            country = country == "" ? ("请选择") : (country);
            province = province == "" ? ("请选择") : (province);
            city = city == "" ? ("请选择") : (city);
            SqlParameter[] paras = new SqlParameter[3];
            paras[0] = new SqlParameter("@country", country);
            paras[1] = new SqlParameter("@province", province);
            paras[2] = new SqlParameter("@city", city);
            object obj = DBHelper.ExecuteScalar("select cpccode from city where country=@country and province=@province and City=@city ", paras, CommandType.Text);
            if (obj == null)
                return "";
            else
                return obj.ToString();
        }


        /// <summary>
        /// 读取店铺国家代码。
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetStoreScpcCode(string storeid)
        {
            string strSql = "select top 1 scpccode from storeinfo where storeid=@StoreID";
            SqlParameter[] para = {
                                      new SqlParameter("@StoreID",SqlDbType.NVarChar,50)
                                  };
            para[0].Value = storeid;
            string scpcCode = DBHelper.ExecuteScalar(strSql, para, CommandType.Text).ToString();
            return scpcCode;
        }

        /// <summary>
        /// 获取城市编号
        /// </summary>
        /// <param name="city">城市实体</param>
        /// <returns>城市编号</returns>
        public static string GetCPCCode(CityModel city)
        {
            SqlParameter[] paras = new SqlParameter[4];
            paras[0] = new SqlParameter("@country", city.Country);
            paras[1] = new SqlParameter("@province", city.Province);
            paras[2] = new SqlParameter("@city", city.City);
            paras[3] = new SqlParameter("@xian", city.Xian);
            object obj = DBHelper.ExecuteScalar("select cpccode from city where country=@country and province=@province and City=@city and xian=@xian", paras, CommandType.Text);
            if (obj == null)
                return "";
            else
                return obj.ToString();
        }

        public static string GetZipCode(string country, string privance, string city)
        {
            SqlParameter[] paras = new SqlParameter[3];
            paras[0] = new SqlParameter("@country", country);
            paras[1] = new SqlParameter("@province", privance);
            paras[2] = new SqlParameter("@city", city);
            object obj = DBHelper.ExecuteScalar("select top 1 isnull(PostCode,'') from city where country=@country and province=@province and City=@city ", paras, CommandType.Text);
            if (obj == null)
                return "";
            else return obj.ToString();
        }

        /// <summary>
        /// 获取城市信息
        /// </summary>
        /// <param name="cpcode">编码</param>
        /// <returns>城市信息 </returns>
        public static CityModel GetCPCCode(string cpcode)
        {
            CityModel city = null;
            SqlParameter[] paras = new SqlParameter[1];
            paras[0] = new SqlParameter("@cpcode", cpcode);
            SqlDataReader reader = DBHelper.ExecuteReader("select country,province,City,Xian from city where cpccode=@cpcode ", paras, CommandType.Text);
            if (reader.Read())
            {
                city = new CityModel();
                city.Country = reader["country"].ToString();
                city.Province = reader["province"].ToString();
                city.City = reader["City"].ToString();
                city.Xian = reader["Xian"].ToString();
            }
            else
            {
                city = new CityModel();
                city.Country = "";
                city.Province = "";
                city.City = "";
                city.Xian = "";
            }
            reader.Close();
            return city;
        }
        /// <summary>
        /// 获取开户行
        /// </summary>
        /// <param name="code">开户行编号</param>
        /// <returns>开户行名称</returns>
        public static string GetBankName(string bankCode)
        {
            SqlParameter[] paras = new SqlParameter[1];
            paras[0] = new SqlParameter("@BankCode", bankCode);
            object obj = DBHelper.ExecuteScalar("select BankName from MemberBank where BankCode=@BankCode ", paras, CommandType.Text);
            if (obj == null)
                return "";
            else
                return obj.ToString();
        }

        /// <summary>
        /// 获取证件类型
        /// </summary>
        /// <param name="city">证件编号</param>
        /// <returns>证件编号</returns>
        public static Bsco_PaperType GetPaperType(string PapertypeCode)
        {
            Bsco_PaperType paper = null;
            SqlParameter[] paras = new SqlParameter[1];
            paras[0] = new SqlParameter("@PapertypeCode", PapertypeCode);
            SqlDataReader reader = DBHelper.ExecuteReader("select * from bsco_PaperType where PaperTypeCode=@PapertypeCode ", paras, CommandType.Text);
            if (reader.Read())
            {
                paper = new Bsco_PaperType(Convert.ToInt32(reader["id"]));
                paper.PaperType = reader["PaperType"].ToString();
                paper.PaperTypeCode = reader["PaperTypeCode"].ToString();
            }
            else
            {
                paper = new Bsco_PaperType();
                paper.PaperType = "";
                paper.PaperTypeCode = "";
            }
            reader.Close();
            return paper;
        }
        /// <summary>
        /// 获得会员或店铺的级别
        /// </summary>
        /// <param name="number">级别名称</param>
        /// <returns></returns>
        public static int GetLevel(string levelstr)
        {
            string sql = "select levelint from bsco_level where levelstr=@num and levelflag=1";
            SqlParameter spa = new SqlParameter("@num", SqlDbType.NVarChar, 50);
            spa.Value = levelstr;
            object obj = DBHelper.ExecuteScalar(sql, spa, CommandType.Text);
            if (obj == null)
                return 0;
            else
                return int.Parse(obj.ToString());
        }
        #region 分页
        /// <summary>
        /// 得到指定页码数据
        /// </summary>
        /// <param name="PageIndex">页码</param>
        /// <param name="PageSize">页记录数</param>
        /// <param name="table">表名</param>
        ///<param name="columns">列</param>
        /// <param name="condition">条件</param>
        /// <param name="key">关键字</param>
        /// <param name="RecordCount">总记录数</param>
        ///<param name="PageCount">页数</param>
        /// <returns></returns>
        public static DataTable GetDataTablePage_Sms(int PageIndex, int PageSize, string table, string columns, string condition, string key, out int RecordCount, out int PageCount)
        {
            RecordCount = 0;
            PageCount = 0;
            SqlParameter[] parm0 = {new SqlParameter("@PageIndex",SqlDbType.Int),
									   new SqlParameter("@PageSize",SqlDbType.Int),
									   new SqlParameter("@table",SqlDbType.VarChar,100),
									   new SqlParameter("@columns",SqlDbType.NVarChar,2000),
									   new SqlParameter("@condition",SqlDbType.NVarChar,2000),
									   new SqlParameter("@key",SqlDbType.VarChar,50),
									   new SqlParameter("@RecordCount",SqlDbType.Int),
									   new SqlParameter("@PageCount",SqlDbType.Int)
								   };
            parm0[0].Value = PageIndex;
            parm0[1].Value = PageSize;
            parm0[2].Value = table;
            parm0[3].Value = columns;
            parm0[4].Value = condition;
            parm0[5].Value = key;
            parm0[6].Value = RecordCount;
            parm0[7].Value = PageCount;
            parm0[6].Direction = System.Data.ParameterDirection.Output;
            parm0[7].Direction = System.Data.ParameterDirection.Output;
            DataTable dt = DBHelper.ExecuteDataTable("GetCustomersDataPage_Sms", parm0, CommandType.StoredProcedure);
            RecordCount = Convert.ToInt32(parm0[6].Value);
            PageCount = Convert.ToInt32(parm0[7].Value);
            return dt;
        }
        #endregion
        #region  分页
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="table"></param>
        /// <param name="columns"></param>
        /// <param name="condition"></param>
        /// <param name="key"></param>
        /// <param name="RecordCount"></param>
        /// <param name="PageCount"></param>
        /// <returns></returns>
        public static DataTable GetDataTablePage_gr(int PageIndex, int PageSize, string table, string columns, string condition, string key, out int RecordCount, out int PageCount)
        {
            RecordCount = 0;
            PageCount = 0;
            SqlParameter[] parm0 = {new SqlParameter("@PageIndex",SqlDbType.Int),
									   new SqlParameter("@PageSize",SqlDbType.Int),
									   new SqlParameter("@table",SqlDbType.VarChar,100),
									   new SqlParameter("@columns",SqlDbType.NVarChar,2000),
									   new SqlParameter("@condition",SqlDbType.NVarChar,2000),
									   new SqlParameter("@key",SqlDbType.VarChar,50),
									   new SqlParameter("@RecordCount",SqlDbType.Int),
									   new SqlParameter("@PageCount",SqlDbType.Int)
								   };
            parm0[0].Value = PageIndex;
            parm0[1].Value = PageSize;
            parm0[2].Value = table;
            parm0[3].Value = columns;
            parm0[4].Value = condition;
            parm0[5].Value = key;
            parm0[6].Value = RecordCount;
            parm0[7].Value = PageCount;
            parm0[6].Direction = System.Data.ParameterDirection.Output;
            parm0[7].Direction = System.Data.ParameterDirection.Output;
            DataTable dt = DBHelper.ExecuteDataTable("GetCustomersDataPage_gr", parm0, CommandType.StoredProcedure);
            RecordCount = Convert.ToInt32(parm0[6].Value);
            PageCount = Convert.ToInt32(parm0[7].Value);
            return dt;
        }
        #endregion
        #region 分页
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="table"></param>
        /// <param name="columns"></param>
        /// <param name="condition"></param>
        /// <param name="key"></param>
        /// <param name="RecordCount"></param>
        /// <param name="PageCount"></param>
        /// <returns></returns>
        public DataTable GetDataTablePage(int PageIndex, int PageSize, string table, string columns, string condition, string key, out int RecordCount, out int PageCount)
        {

            RecordCount = 0;
            PageCount = 0;
            SqlParameter[] parm0 = {new SqlParameter("@PageIndex",SqlDbType.Int),
									   new SqlParameter("@PageSize",SqlDbType.Int),
									   new SqlParameter("@table",SqlDbType.VarChar,1000),
									   new SqlParameter("@columns",SqlDbType.NVarChar,2000),
									   new SqlParameter("@condition",SqlDbType.NVarChar,2000),
									   new SqlParameter("@key",SqlDbType.VarChar,50),
				                       new SqlParameter("@RecordCount",SqlDbType.Int),
				                       new SqlParameter("@PageCount",SqlDbType.Int)
								   };

            parm0[0].Value = PageIndex;
            parm0[1].Value = PageSize;
            parm0[2].Value = table;
            parm0[3].Value = columns;
            parm0[4].Value = condition;
            parm0[5].Value = key;
            parm0[6].Value = RecordCount;
            parm0[7].Value = PageCount;

            parm0[6].Direction = System.Data.ParameterDirection.Output;
            parm0[7].Direction = System.Data.ParameterDirection.Output;


            DataTable dt = DBHelper.ExecuteDataTable("GetCustomersDataPage", parm0, CommandType.StoredProcedure);
            RecordCount = Convert.ToInt32(parm0[6].Value);
            PageCount = Convert.ToInt32(parm0[7].Value);

            return dt;
        }
        #endregion
        /// <summary>
        /// 会员电子账号余额
        /// </summary>
        /// <param name="EctNumber">电子账户编号</param>
        /// <returns></returns>
        public static object EctBalance(string EctNumber)
        {
            object obj = new object();
            SqlParameter[] param = {new SqlParameter("@EctNumber",SqlDbType.VarChar,50),
								   };
            param[0].Value = EctNumber;
            obj = DBHelper.ExecuteScalar("select isnull((TotalRemittances-TotalDefray),0) as Balance  from MemberInfo where number=@ectnumber", param, CommandType.Text);
            return obj;

        }
        /// <summary>
        /// 店铺产品库存量
        /// </summary>
        /// <param name="storeId">店铺编号</param>
        /// <param name="productId">产品编号</param>
        /// <returns></returns>
        public static object StoreStock(string storeId, int productId)
        {
            object obj = new object();
            SqlParameter[] param ={
                                 new  SqlParameter("@storeId",SqlDbType.VarChar,50),
                                 new  SqlParameter("productId",SqlDbType.Int,10),
                                 };
            param[0].Value = storeId;
            param[1].Value = productId;
            obj = DBHelper.ExecuteScalar("P_StoreStock", param, CommandType.StoredProcedure);
            return obj;
        }
        /// <summary>
        /// 店铺剩余金额
        /// </summary>
        /// <param name="storeId">店铺编号</param>
        /// <returns></returns>
        public static object StoreLaveAmount(string storeId)
        {
            object obj = new object();
            SqlParameter[] param = {new SqlParameter("@storeId",SqlDbType.VarChar,50),
								   };
            param[0].Value = storeId;
            obj = DBHelper.ExecuteScalar("P_StoreLaveAmount", param, CommandType.StoredProcedure);
            return obj;
        }

        public static string GetLeftMenu(int type)
        {
            string strSql = "select * from menuLeft where parentid=@type";
            SqlParameter[] para = {
                                      new SqlParameter("@type",type)
                                  };
            string strMenu = "";
            DataTable dt = DBHelper.ExecuteDataTable(strSql, para, CommandType.Text);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i != 0)
                {
                    strMenu += ",";
                }
                strMenu += dt.Rows[i]["id"].ToString();
            }
            return strMenu;
        }

        public static int GetLeftMenuCount(int type)
        {
            string strSql = "select count(id) from menuLeft where parentID=@type";
            SqlParameter[] para = {
                                      new SqlParameter("@type",SqlDbType.Int)
                                  };
            para[0].Value = type;
            int count = (int)DBHelper.ExecuteScalar(strSql, para, CommandType.Text);
            return count;
        }

        /// <summary>
        /// 获取查询控制显示内容
        /// </summary>
        /// <returns>返回当前状态</returns>
        public static string GetDisplayField(string Column, CheckBoxList cbl, int type)
        {
            string strSql = "select n.field,t." + Column + " as namee,n.flag from NetWorkDisplayStatus as n left join T_translation as t on t.primarykey=n.id where t.tablename='NetWorkDisplayStatus' and n.type=@type";
            SqlParameter[] para = {
                                      new SqlParameter("@type",SqlDbType.Int)
                                  };
            para[0].Value = type;
            DataTable dt = DBHelper.ExecuteDataTable(strSql, para, CommandType.Text);
            string NetWorkDisplayStatus = "";
            cbl.DataSource = dt;

            cbl.DataTextField = "namee";
            cbl.DataValueField = "field";

            cbl.DataBind();
            for (int i = 0; i < cbl.Items.Count; i++)
            {
                if (dt.Rows[i]["flag"].ToString() == "1")
                {
                    cbl.Items[i].Selected = true;
                    NetWorkDisplayStatus += cbl.Items[i].Text.Trim() + ",";
                }
            }
            return NetWorkDisplayStatus;
        }

        public static DataTable GetNetWorkDisplayStatus(int type)
        {
            string strSql = "select  flag  from  NetWorkDisplayStatus where type=@type order by id";
            SqlParameter[] para = {
                                      new SqlParameter("@type",SqlDbType.Int)
                                  };
            para[0].Value = type;
            DataTable dt = DBHelper.ExecuteDataTable(strSql, para, CommandType.Text);
            return dt;
        }

        public static DataTable GetNetWorkDisplayStatus1(int type)
        {
            string strSql = "select  name  from  NetWorkDisplayStatus where type=@type and flag=1 order by id";
            SqlParameter[] para = {
                                      new SqlParameter("@type",SqlDbType.Int)
                                  };
            para[0].Value = type;
            DataTable dt = DBHelper.ExecuteDataTable(strSql, para, CommandType.Text);
            return dt;
        }

        /// <summary>
        /// 更新查询控制显示内容
        /// </summary>
        /// <returns>返回影响的行数</returns>
        public static int UpdateDisplayField(string field, int flag, int type, SqlTransaction tran)
        {
            string strSql = "Update NetWorkDisplayStatus set flag=@flag where field=@field and type=@type";
            SqlParameter[] para = {
                                      new SqlParameter ("@flag",SqlDbType.Int),
                                      new SqlParameter ("@field",SqlDbType.NVarChar,50),
                                      new SqlParameter("@type",SqlDbType.Int)
                                  };
            para[0].Value = flag;
            para[1].Value = field;
            para[2].Value = type;
            int count = (int)DBHelper.ExecuteNonQuery(tran, strSql, para, CommandType.Text);
            return count;
        }


        /// <summary>
        /// 更新会员报单表
        /// </summary>
        /// <param name="orderId">报单编号</param>
        /// <param name="volume">期数</param>
        /// <returns></returns>
        public static bool ConfirmMembersOrder(SqlTransaction tran, string orderId, int volume, decimal enoughproductMoney, decimal lackproductmoney)
        {
            int result = 0;
            SqlParameter[] param = { 
                                  new  SqlParameter("@orderId",SqlDbType.VarChar,50),
                                 new  SqlParameter("@volume",SqlDbType.Int,10),
                                 new SqlParameter("@enoughproductMoney",SqlDbType.Decimal),
                                 new SqlParameter("@lackproductmoney",SqlDbType.Decimal)
                                  };
            param[0].Value = orderId;
            param[1].Value = volume;
            param[2].Value = enoughproductMoney;
            param[3].Value = lackproductmoney;
            result = DBHelper.ExecuteNonQuery(tran, "Update MemberOrder Set IsReceivables=1,PayMentMoney=@enoughproductMoney+@lackproductmoney, ReceivablesDate=getdate(),DefrayState=1,Remark='通过审核',PayExpectNum=@volume,lackproductmoney=@lackproductmoney,enoughproductMoney=@enoughproductMoney Where OrderID=@orderId", param, CommandType.Text);
            if (result < 1)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 更新会员报单表
        /// </summary>
        /// <param name="orderId">报单编号</param>
        /// <param name="volume">期数</param>
        /// <returns></returns>
        public static bool ConfirmMembersOrder(SqlTransaction tran, string orderId, int volume)
        {
            int result = 0;
            SqlParameter[] param = { 
                                  new  SqlParameter("@orderId",SqlDbType.VarChar,50),
                                 new  SqlParameter("@volume",SqlDbType.Int,10)
                                  };
            param[0].Value = orderId;
            param[1].Value = volume;
            result = DBHelper.ExecuteNonQuery(tran, "Update MemberOrder Set DefrayState=1,Remark='通过审核',PayExpectNum=@volume Where OrderID=@orderId", param, CommandType.Text);
            if (result < 1)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 更新会员报单表
        /// </summary>
        /// <param name="orderId">报单编号</param>
        /// <param name="volume">期数</param>
        /// <returns></returns>
        public static bool ConfirmMembersDetails(SqlTransaction tran, int productId, string orderId, decimal NotEnoughProduct)
        {
            int result = 0;
            string strSql = "Update MemberDetails Set NotEnoughProduct=@NotEnoughProduct Where orderId=@orderId And productId=@productId";
            SqlParameter[] param = { 
                                        new SqlParameter("@NotEnoughProduct",SqlDbType.Decimal),
                                        new  SqlParameter("@orderId",SqlDbType.NVarChar,50),
                                        new  SqlParameter("@productId",SqlDbType.Int,10)
                                   };
            param[0].Value = NotEnoughProduct;
            param[1].Value = orderId;
            param[2].Value = productId;
            result = DBHelper.ExecuteNonQuery(tran, strSql, param, CommandType.Text);
            if (result < 1)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 更新结算表
        /// </summary>
        /// <param name="number">会员编号</param>
        /// <param name="xgfenshu">新个分数</param>
        /// <param name="volume">期数</param>
        /// <returns></returns>
        public static bool UPMemberInfoBalance(SqlTransaction tran, string number, decimal xgfenshu, int volume)
        {
            //try {                
            SqlParameter[] param = { 
                                  new  SqlParameter("@bianhao",SqlDbType.VarChar,50),
                                  new  SqlParameter("@xgfenshu",SqlDbType.Decimal),
                                  new  SqlParameter("@qishu",SqlDbType.Int,10),
                                  };
            param[0].Value = number;
            param[1].Value = xgfenshu;
            param[2].Value = volume;
            DBHelper.ExecuteNonQuery(tran, "P_sure", param, CommandType.StoredProcedure);
            return true;

            //}
            //catch { return false; }
        }
        /// <summary>
        /// 更新店铺库存
        /// </summary>
        /// <param name="storeId">店铺编号</param>
        /// <param name="productId">产品编号</param>
        /// <param name="productcount">产品数量</param>
        /// <returns></returns>
        public static bool UPStoreStock(SqlTransaction tran, string storeId, int productId, int productcount)
        {
            int result = 0;
            SqlParameter[] param = { 
                                  new SqlParameter("@StoreID",SqlDbType.VarChar,20),
                                  new SqlParameter("@ProductID",SqlDbType.Int),
                                  new SqlParameter("@ProductCount",SqlDbType.Int)
                                  };
            param[0].Value = storeId;
            param[1].Value = productId;
            param[2].Value = productcount;
            result = DBHelper.ExecuteNonQuery(tran, "P_UPSotreStock", param, CommandType.StoredProcedure);
            if (result < 1)
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// 修改会员电子账户余额
        /// </summary>
        /// <param name="number">电子账户编号</param>
        /// <param name="amount">修改金额</param>
        /// <returns></returns>
        public static bool UPMemberEct(string number, decimal amount)
        {
            int reslut = 0;
            SqlParameter[] param = {
                                       new SqlParameter("@number",SqlDbType.VarChar,20),
            new SqlParameter("@money",SqlDbType.Decimal),
                                   };
            param[0].Value = number;
            param[1].Value = amount;
            reslut = DBHelper.ExecuteNonQuery("P_UP_EctBalance", param, CommandType.StoredProcedure);
            if (reslut < 1)
            {
                return false;
            }
            return true;
        }
        #region 根据管理员的编号得到管理员姓名
        /// <summary>
        /// 根据管理员的编号得到管理员姓名
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        public string GetNameByAdminID(string Number)
        {
            string name = string.Empty;
            string sSQL = "Select Name From Manage Where Number = '" + Number + "'";
            SqlDataReader reader = DBHelper.ExecuteReader(sSQL);
            while (reader.Read())
            {
                name = reader[0].ToString();
            }
            reader.Close();

            return (name.Length == 0 ? "管理员" : name);
        }
        #endregion

        /// <summary>
        /// 得到期数的显示状态( 0:表示按期数显示，1：表示按日期显示)
        /// </summary>
        public static int GetShowQishuDate()
        {
            int Value = 0;
            try
            {
                Value = Convert.ToInt32(DBHelper.ExecuteScalar("Select [FileValue] From DataDictionary where [code]='0001'"));
            }
            catch
            {
                return 0;
            }
            return Value;
        }


        public static IList<ConfigModel> GetExpectNum()
        {
            string sql = "select ExpectNum,Date,stardate,enddate from config order by ExpectNum desc";
            IList<ConfigModel> configs = null;
            SqlDataReader dr = DBHelper.ExecuteReader(sql);
            if (dr.HasRows)
            {
                configs = new List<ConfigModel>();
                while (dr.Read())
                {
                    ConfigModel config = new ConfigModel();
                    config.ExpectNum = dr.GetInt32(0);
                    config.Date = dr.GetString(1);
                    config.Stardate = dr.GetDateTime(2).ToString();
                    config.Enddate = dr.GetDateTime(3).ToString();
                    configs.Add(config);
                }
            }
            dr.Close();
            dr.Dispose();

            return configs;
        }

        public static object GetMaxExpect()
        {
            string sql = "select max(ExpectNum) from config";
            object obj = DBHelper.ExecuteScalar(sql);
            return obj == null ? 1 : int.Parse(obj.ToString());
        }
        /// <summary>
        /// 获取石斛积分价格
        /// </summary>
        /// <returns></returns>
        public static object GetMaxDayPrice()
        {
            string sql = "select MAX(NowPrice) from DayPrice";
            object obj = DBHelper.ExecuteScalar(sql);
            return obj;
        }
        public static object GetMaxExpect(SqlTransaction tran)
        {
            string sql = "select max(ExpectNum) from config";
            object obj = DBHelper.ExecuteScalar(tran, sql, CommandType.Text);
            return obj == null ? 1 : int.Parse(obj.ToString());
        }

        public static string GetSjBh(string bianhao, bool isAz)
        {
            string sjType = "Direct";
            if (isAz)
            {
                sjType = "Placement";
            }

            string sql = "select " + sjType + " from memberInfo where number=@num";
            SqlParameter spa = new SqlParameter("@num", SqlDbType.NVarChar, 50);
            spa.Value = bianhao;
            string sjBh = DBHelper.ExecuteScalar(sql, spa, CommandType.Text).ToString();
            //if (sjBh != "")
            //{
            //    sjBh = DBHelper.ExecuteScalar("select " + sjType + " from memberinfo where number='"+sjBh+"'").ToString();
            //}
            return sjBh;
        }

        public static string GetRegisUp(string bianhao)
        {
            string regUp = "";
            string sql = "select expectNum from memberInfo where number=@num";
            SqlParameter spa = new SqlParameter("@num", SqlDbType.NVarChar, 50);
            spa.Value = bianhao;
            int sjBh = Convert.ToInt32(DBHelper.ExecuteScalar(sql, spa, CommandType.Text));
            if (sjBh != getMaxqishu())
            {
                //不是当前期会员，不能修改报单！
                regUp = "1";
            }
            else
            {
                String sqlStr = "select count(0) from memberorder where number=@num and ordertype=0";
                int ordertype = Convert.ToInt32(DBHelper.ExecuteScalar(sqlStr, spa, CommandType.Text));
                if (ordertype > 0)
                {
                    sqlStr = "select orderid from memberorder where number=@num and ordertype=0";
                    string orderid = DBHelper.ExecuteScalar(sqlStr, spa, CommandType.Text).ToString();
                    regUp = orderid;
                }
                else
                {
                    //对不起，注册单非服务机构注册，不能修改！
                    regUp = "2";
                }
            }


            return regUp;
        }

        /// <summary>
        /// 获得网络图显示内容
        /// </summary>
        /// <param name="number">会员编号</param>
        /// <param name="type">显示内容</param>
        /// <returns></returns>
        public static string GetDisplay(string number, string labField, int ExpectNum)
        {
            string display = "";
            string strSql = "select top 1 displayField from display where FieldName=@labField";
            SqlParameter[] para1 = {
                                       new SqlParameter("@labField",SqlDbType.NVarChar,50)
                                   };
            para1[0].Value = labField;
            display = DBHelper.ExecuteScalar(strSql, para1, CommandType.Text).ToString();

            strSql = "select " + display + "  from MemberInfoBalance" + ExpectNum + " where number=@number";
            SqlParameter[] para = {
                                      new SqlParameter("@number",SqlDbType.NVarChar,20)
                                  };
            para[0].Value = number;
            string displayValue = (Convert.ToDouble(DBHelper.ExecuteScalar(strSql, para, CommandType.Text))).ToString();
            return displayValue;

        }

        /// <summary>
        /// 获得网络图显示内容
        /// </summary>
        /// <param name="number">会员编号</param>
        /// <param name="type">显示内容</param>
        /// <returns></returns>
        public static string GetDisplay1(string number, string labField, int ExpectNum, int type, int isAnzhi)
        {
            string display = "";
            string strSql = "select top 1 Field from NetWorkDisplayStatus where name=@labField and type=@type";
            SqlParameter[] para1 = {
                                       new SqlParameter("@labField",SqlDbType.NVarChar,50),
                                       new SqlParameter("@type",SqlDbType.Int)
                                   };
            para1[0].Value = labField;
            para1[1].Value = type;
            display = DBHelper.ExecuteScalar(strSql, para1, CommandType.Text).ToString();
            if (isAnzhi == 0)
            {
                if (display.ToLower() == "currenttotalnetrecord" || display.ToLower() == "currentnewnetnum" || display.ToLower() == "totalnetnum" || display.ToLower() == "totalnetrecord")
                {
                    display = "D" + display;
                }
            }

            strSql = "select " + display + "  from MemberInfoBalance" + ExpectNum + " where number=@number";
            SqlParameter[] para = {
                                      new SqlParameter("@number",SqlDbType.NVarChar,20)
                                  };
            para[0].Value = number;
            if (display.ToLower() == "level")
            {
                int level = (int)DBHelper.ExecuteScalar(strSql, para, CommandType.Text);
                string levelStr = DAL.CommonDataDAL.GetStrLevel(level);
                return levelStr;
            }
            else
            {
                string displayValue = (Convert.ToDouble(DBHelper.ExecuteScalar(strSql, para, CommandType.Text))).ToString();
                return displayValue;
            }
        }


        /// <summary>
        /// 获得网络图显示内容表数据
        /// </summary>
        /// <returns>返回</returns>
        public static DataTable GetDisplayBind()
        {
            string strSql = "select top 5 * from display where indicate = 1";
            DataTable dt = DBHelper.ExecuteDataTable(strSql, null, CommandType.Text);
            return dt;
        }


        /// <summary>
        /// 获得会员或店铺的级别
        /// </summary>
        /// <param name="table">表明</param>
        /// <param name="number">编号</param>
        /// <returns></returns>
        public static DataTable GetLevel(string table, string storeId)
        {

            SqlParameter[] param = {
                           // new SqlParameter("@table",SqlDbType.VarChar),
                            new SqlParameter("@StoreID",SqlDbType.VarChar)
                         
                                   };
            //param[0].Value = table;
            param[0].Value = storeId;
            return DBHelper.ExecuteDataTable("P_StoreLevel", param, CommandType.StoredProcedure);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public static DataTable GetLevel2(string table, string number)
        {
            SqlParameter[] param = {
                           // new SqlParameter("@table",SqlDbType.VarChar),
                            new SqlParameter("@number",SqlDbType.VarChar)
                         
                                   };
            //param[0].Value = table;
            param[0].Value = number;
            return DBHelper.ExecuteDataTable("P_GetLevel", param, CommandType.StoredProcedure);

        }

        public static DataTable GetBalanceLevel(string number)
        {
            string sql = "Select isnull(level,0) level From MemberInfoBalance" + DAL.CommonDataDAL.getMaxqishu() + " where number=@number";
            SqlParameter[] para = {
                                      new SqlParameter("@number",SqlDbType.NVarChar,30)
                                  };
            para[0].Value = number;
            DataTable dt = DBHelper.ExecuteDataTable(sql, para, CommandType.Text);
            return dt;
        }

        /// <summary>
        /// 修改会员级别
        /// </summary>
        /// <param name="number">会员编号</param>
        /// <param name="level">级别</param>
        /// <param name="levelstr">级别名称</param>
        /// <returns></returns>
        public static bool UPdateMemberLevel(SqlTransaction tran, string number, int level, string levelstr)
        {
            string sql = " UPDATE memberInfo SET LevelInt=" + level + " WHERE number=@num";
            SqlParameter[] spa = new SqlParameter[] { new SqlParameter("@num", SqlDbType.NVarChar, 50) };
            spa[0].Value = number;
            int result = DBHelper.ExecuteNonQuery(tran, sql, spa, CommandType.Text);
            if (result < 1)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 更新结算表中的级别
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="number">编号</param>
        /// <param name="NowLevel">级别</param>
        /// <returns></returns>
        public static bool UPJiesuanLevel(SqlTransaction tran, string number, int NowLevel)
        {
            string sql = "UPDATE MemberInfoBalance" + getMaxqishu() + " SET Level=@num WHERE number=@num1";
            SqlParameter[] spas = new SqlParameter[] { 
                new SqlParameter("@num",SqlDbType.Int),
                new SqlParameter("@num1",SqlDbType.NVarChar,50)
            };
            spas[0].Value = NowLevel;
            spas[1].Value = number;
            int result = DBHelper.ExecuteNonQuery(tran, sql, spas, CommandType.Text);
            if (result < 1)
            {
                return false;
            }
            return true;

        }
        /// <summary>
        /// 进入级别表
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="number">编号</param>
        /// <param name="level">级别</param>
        /// <param name="volume">期数</param>
        /// <returns></returns>
        public static bool UPdateIntoLevel(SqlTransaction tran, string number, int level, int volume, DateTime inputdate)
        {
            int reslut = 0;
            SqlParameter[] param = {
                            new SqlParameter("@number",SqlDbType.VarChar,20),
                            new SqlParameter("@level",SqlDbType.Int),
                            new SqlParameter("@volume",SqlDbType.Int),
                            new SqlParameter("@inputdate",SqlDbType.DateTime)
                                   };
            param[0].Value = number;
            param[1].Value = level;
            param[2].Value = volume;
            param[3].Value = inputdate;
            reslut = DBHelper.ExecuteNonQuery(tran, "P_Level", param, CommandType.StoredProcedure);
            if (reslut < 1)
            {
                return false;
            }
            return true;
        }

        public static bool UPdateIntoLevel(GradingModel model)
        {
            int reslut = 0;
            SqlParameter[] param = {
                            new SqlParameter("@number",SqlDbType.VarChar,20),
                            new SqlParameter("@level",SqlDbType.Int),
                            new SqlParameter("@volume",SqlDbType.Int),
                            new SqlParameter("@inputdate",SqlDbType.DateTime),
                            new SqlParameter("@operaternum",SqlDbType.NVarChar,50),
                            new SqlParameter("@operaterip",SqlDbType.NVarChar,50),
                            new SqlParameter("@GradingStatus",SqlDbType.Int),
                            new SqlParameter("@remark",SqlDbType.NVarChar)
                                   };
            param[0].Value = model.Number;
            param[1].Value = model.LevelNum;
            param[2].Value = model.ExpectNum1;
            param[3].Value = model.InputDate1;
            param[4].Value = model.OperaterNum1;
            param[5].Value = model.OperateIP1;
            param[6].Value = (int)model.GradingStatus;
            param[7].Value = model.Mark1;
            reslut = DBHelper.ExecuteNonQuery("P_Level", param, CommandType.StoredProcedure);
            if (reslut < 1)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取上级编号
        /// </summary>
        /// <param name="number">编号</param>
        /// <returns></returns>
        public static string UpBianhao(string number)
        {
            string bh = "";
            string strSql = "Select Placement From MemberInfo Where Number=@number";
            SqlParameter[] param = {
                                        new SqlParameter("@number",SqlDbType.VarChar,20)
                                   };
            param[0].Value = number;
            bh = DBHelper.ExecuteScalar(strSql, param, CommandType.Text).ToString();
            return bh;
        }

        /// <summary>
        /// 更新店铺级别
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="StoreId">店编号</param>
        /// <param name="level">级别</param>
        /// <param name="levelstr">级别名称</param>
        /// <returns></returns>
        public static bool UPStoreLevel(SqlTransaction tran, string StoreId, int level)
        {
            string sql = "UPDATE StoreInfo SET StoreLevelInt=@num WHERE storeid=@num1";
            SqlParameter[] spas = new SqlParameter[] { 
                new SqlParameter("@num",SqlDbType.Int),
                new SqlParameter("@num1",SqlDbType.NVarChar,50)
            };
            spas[0].Value = level;
            spas[1].Value = StoreId;
            int result = DBHelper.ExecuteNonQuery(tran, sql, spas, CommandType.Text);
            if (result < 1)
            {
                return false;
            }
            return true;

        }

        /// <summary>
        /// 会员是否注销
        /// </summary>
        /// <param name="number">会员编号</param>
        /// <returns></returns>
        public static bool GetIsActive(string number)
        {
            string strSql = "select top 1 isnull(MemberState,0) from memberinfo where number=@number";
            SqlParameter[] para = {
                                      new SqlParameter("@number",SqlDbType.NVarChar,30)
                                  };
            para[0].Value = number;

            int isActive = (int)DBHelper.ExecuteScalar(strSql, para, CommandType.Text);
            if (isActive == 0)
            {
                return true;
            }
            return false;
        }

        public static string GetBiZhong(int id)
        {
            return DBHelper.ExecuteScalar("select top 1 [name] from Currency where id=@id", new SqlParameter[1] { new SqlParameter("@id", id) }, CommandType.Text).ToString();
        }
        /// <summary>
        /// 获取店铺级别名称
        /// </summary>
        /// <param name="storeid">店铺编号</param>
        /// <returns></returns>
        public static string GetStoreLevelStr(string storeid)
        {
            SqlParameter[] param = { new SqlParameter("@storeid", SqlDbType.NVarChar) };
            param[0].Value = storeid;
            return DBHelper.ExecuteScalar("getStoreLevelStr", param, CommandType.StoredProcedure).ToString();
        }
    }
}