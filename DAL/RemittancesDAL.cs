using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Model;
using Model.Other;
using DAL.Other;

/*
 *创建者：  张振
 *创建时间：2009-08-28
 *修改者：  汪华
 *修改时间：2009-08-31
 *文件名：  RemittancesDAL
 *
 */

namespace DAL
{
    public class RemittancesDAL
    {


        /// <summary>
        /// 汇款合计总额——ds2012——tianfeng
        /// </summary>
        /// <param name="summoney">总金额</param>
        /// <param name="currencyname">货币类型名称</param>
        public static void TotalMoney(out double summoney, out string currencyname)
        {
            summoney = 0;
            currencyname = "";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@RemitMoney", SqlDbType.Money), new SqlParameter("@currency", SqlDbType.VarChar, 50) };
            parm[0].Value = summoney;
            parm[1].Value = currencyname;

            parm[0].Direction = ParameterDirection.Output;
            parm[1].Direction = ParameterDirection.Output;
            DBHelper.ExecuteReader("Remittances_Amount", parm, CommandType.StoredProcedure);
            summoney = double.Parse(parm[0].Value.ToString());
            currencyname = parm[1].ToString();
        }
        /// <summary>
        /// 更新店铺汇款额,预收账款——ds2012——tianfeng
        /// </summary>
        ///<param name="type">汇款ID</param>
        /// <param name="money">汇款金额</param>
        /// <param name="storeID">汇款店铺</param>
        /// <param name="storeID">操作者IP</param>
        /// <param name="storeID">操作者编号</param>
        public static void Auditing(int id, double money, string storeID, string OperateIP, string OperateNum)
        {
            SqlParameter[] parm = new SqlParameter[] {
            new SqlParameter("@ID",SqlDbType.Int),
            new SqlParameter("@Money",SqlDbType.Money),
            new SqlParameter("@StoreID",SqlDbType.VarChar,50),
            new SqlParameter("@OperateIP",SqlDbType.VarChar,50),
            new SqlParameter("@OperateNum",SqlDbType.VarChar,50),
            new SqlParameter("@ReceivablesDate",SqlDbType.DateTime)
            };
            parm[0].Value = id;
            parm[1].Value = money;
            parm[2].Value = storeID;
            parm[3].Value = OperateIP;
            parm[4].Value = OperateNum;
            parm[5].Value = DateTime.Now.ToUniversalTime();
            DBHelper.ExecuteNonQuery("Remittances_remitMoney", parm, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 更新会员汇款额,预收账款——ds2012——tianfeng
        /// </summary>
        ///<param name="type">汇款ID</param>
        /// <param name="money">汇款金额</param>
        /// <param name="storeID">汇款会员</param>
        /// <param name="storeID">操作者IP</param>
        /// <param name="storeID">操作者编号</param>
        public static void MemberAuditing(int id, double money, string number, string OperateIP, string OperateNum)
        {
            SqlParameter[] parm = new SqlParameter[] {
            new SqlParameter("@ID",SqlDbType.Int),
            new SqlParameter("@Money",SqlDbType.Money),
            new SqlParameter("@Number",SqlDbType.VarChar,50),
            new SqlParameter("@OperateIP",SqlDbType.VarChar,50),
            new SqlParameter("@OperateNum",SqlDbType.VarChar,50),
            new SqlParameter("@ReceivablesDate",SqlDbType.DateTime)
            };
            parm[0].Value = id;
            parm[1].Value = money;
            parm[2].Value = number;
            parm[3].Value = OperateIP;
            parm[4].Value = OperateNum;
            parm[5].Value = DateTime.Now.ToUniversalTime();
            DBHelper.ExecuteNonQuery("MemberRemittances_remitMoney", parm, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 删除店铺未审核汇款——ds2012——tianfeng
        /// </summary>
        /// <param name="id">汇款编号</param>
        public static void DeleteMoney(int id)
        {
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int) };
            parm[0].Value = id;
            DBHelper.ExecuteNonQuery("Remittances_Delete_Unapprove", parm, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 删除会员未审核汇款——ds2012——tianfeng
        /// </summary>
        /// <param name="id">汇款编号</param>
        public static void DeleteMemberMoney(int id)
        {
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int) };
            parm[0].Value = id;
            DBHelper.ExecuteNonQuery("Delete  Remittances where id=@ID", parm, CommandType.Text);
        }
        /// <summary>
        /// 添加汇款，更新店铺金额——ds2012——tianfeng
        /// </summary>
        /// <param name="info">汇款信息对象</param>
        /// <param name="RateName1">店铺货币汇率名称</param>
        /// <param name="RateName2">实际付款汇率名称</param>
        public static void AddRemittances(RemittancesModel info, string RateName1, string RateName2, out int id)
        {
            id = 0;
            SqlParameter[] parm = new SqlParameter[] {
            new SqlParameter("@RateName1",SqlDbType.VarChar),
            new SqlParameter("@RateName2",SqlDbType.VarChar),
            new SqlParameter("@ReceivablesDate",SqlDbType.DateTime),
            new SqlParameter("@RemittancesDate",SqlDbType.DateTime),
            new SqlParameter("@ImportBank",SqlDbType.VarChar,50),
            new SqlParameter("@ImportNumber",SqlDbType.NVarChar,500),
            new SqlParameter("@RemittancesAccount",SqlDbType.NVarChar,500),
            new SqlParameter("@RemittancesBank",SqlDbType.VarChar,50),
            new SqlParameter("@SenderID",SqlDbType.NVarChar,500),
            new SqlParameter("@Sender",SqlDbType.NVarChar,500),
            new SqlParameter("@StoreID",SqlDbType.VarChar),
            new SqlParameter("@RemitMoney",SqlDbType.Money),
            new SqlParameter("@StandardCurrency",SqlDbType.Int),
            new SqlParameter("@Use",SqlDbType.Int),
            new SqlParameter("@PayExpect",SqlDbType.Int),
            new SqlParameter("@PayWay",SqlDbType.Int),
            new SqlParameter("@Managers",SqlDbType.VarChar,50),
            new SqlParameter("@ConfirmType",SqlDbType.Int),
            new SqlParameter("@Remark",SqlDbType.VarChar,50),
            new SqlParameter("@RemittancesCurrency",SqlDbType.Int),
            new SqlParameter("@RemittancesMoney",SqlDbType.Money),
            new SqlParameter("@Photopath",SqlDbType.VarChar,50),
            new SqlParameter("@OperateIP",SqlDbType.VarChar,50),
            new SqlParameter("@OperateNum",SqlDbType.VarChar,50),
            new SqlParameter("@Remittancesid",SqlDbType.NVarChar,50),
            new SqlParameter("@RemitStatus",SqlDbType.Int),
            new SqlParameter("@id",SqlDbType.Int),
            new SqlParameter("@isgsqr",SqlDbType.Int)
            };
            parm[0].Value = RateName1;
            parm[1].Value = RateName2;
            parm[2].Value = info.ReceivablesDate;
            if (info.RemittancesDate != null)
            {
                parm[3].Value = info.RemittancesDate;
            }
            else
            {
                parm[3].Value = null;
            }
            parm[4].Value = info.ImportBank;
            parm[5].Value = info.ImportNumber;
            parm[6].Value = info.RemittancesAccount;
            parm[7].Value = info.RemittancesBank;
            parm[8].Value = info.SenderID;
            parm[9].Value = info.Sender;
            parm[10].Value = info.RemitNumber;
            parm[11].Value = info.RemitMoney;
            parm[12].Value = info.StandardCurrency;
            parm[13].Value = info.Use;
            parm[14].Value = info.PayexpectNum;
            parm[15].Value = info.PayWay;
            parm[16].Value = info.Managers;
            parm[17].Value = info.ConfirmType;
            parm[18].Value = info.Remark;
            parm[19].Value = info.RemittancesCurrency;
            parm[20].Value = info.RemittancesMoney;
            parm[21].Value = info.PhotoPath;
            parm[22].Value = info.OperateIp;
            parm[23].Value = info.OperateNum;
            parm[24].Value = info.Remittancesid;
            parm[25].Value = info.RemitStatus;
            parm[26].Value = 0;
            parm[26].Direction = ParameterDirection.Output;
            parm[27].Value = info.IsGSQR;

            DBHelper.ExecuteNonQuery("Remittances_AddremitMoney", parm, CommandType.StoredProcedure);
            id = int.Parse(parm[26].Value.ToString());
        }
        /// <summary>
        /// 添加会员汇款，更新会员金额——ds2012——tianfeng
        /// </summary>
        /// <param name="info">汇款信息对象</param>
        /// <param name="RateName1">店铺货币汇率名称</param>
        /// <param name="RateName2">实际付款汇率名称</param>
        public static void AddMemberRemittances(RemittancesModel info, string RateName1, string RateName2, out int id)
        {
            id = 0;
            SqlParameter[] parm = new SqlParameter[] {
            new SqlParameter("@RateName1",SqlDbType.VarChar),
            new SqlParameter("@RateName2",SqlDbType.VarChar),
            new SqlParameter("@ReceivablesDate",SqlDbType.DateTime),
            new SqlParameter("@RemittancesDate",SqlDbType.DateTime),
            new SqlParameter("@ImportBank",SqlDbType.VarChar,50),
            new SqlParameter("@ImportNumber",SqlDbType.NVarChar,500),
            new SqlParameter("@RemittancesAccount",SqlDbType.NVarChar,500),
            new SqlParameter("@RemittancesBank",SqlDbType.VarChar,50),
            new SqlParameter("@SenderID",SqlDbType.NVarChar,500),
            new SqlParameter("@Sender",SqlDbType.NVarChar,500),
            new SqlParameter("@RemitNumber",SqlDbType.VarChar),
            new SqlParameter("@RemitMoney",SqlDbType.Money),
            new SqlParameter("@StandardCurrency",SqlDbType.Int),
            new SqlParameter("@Use",SqlDbType.Int),
            new SqlParameter("@PayExpect",SqlDbType.Int),
            new SqlParameter("@PayWay",SqlDbType.Int),
            new SqlParameter("@Managers",SqlDbType.VarChar,50),
            new SqlParameter("@ConfirmType",SqlDbType.Int),
            new SqlParameter("@Remark",SqlDbType.VarChar,50),
            new SqlParameter("@RemittancesCurrency",SqlDbType.Int),
            new SqlParameter("@RemittancesMoney",SqlDbType.Money),
            new SqlParameter("@Photopath",SqlDbType.VarChar,50),
            new SqlParameter("@OperateIP",SqlDbType.VarChar,50),
            new SqlParameter("@OperateNum",SqlDbType.VarChar,50),
            new SqlParameter("@Remittancesid",SqlDbType.NVarChar,50),
            new SqlParameter("@RemitStatus",SqlDbType.Int),
            new SqlParameter("@id",SqlDbType.Int),
            new SqlParameter("@isgsqr",SqlDbType.Int)
            };
            parm[0].Value = RateName1;
            parm[1].Value = RateName2;
            parm[2].Value = info.ReceivablesDate;
            if (info.RemittancesDate != null)
            {
                parm[3].Value = info.RemittancesDate;
            }
            else
            {
                parm[3].Value = null;
            }
            parm[4].Value = info.ImportBank;
            parm[5].Value = info.ImportNumber;
            parm[6].Value = info.RemittancesAccount;
            parm[7].Value = info.RemittancesBank;
            parm[8].Value = info.SenderID;
            parm[9].Value = info.Sender;
            parm[10].Value = info.RemitNumber;
            parm[11].Value = info.RemitMoney;
            parm[12].Value = info.StandardCurrency;
            parm[13].Value = info.Use;
            parm[14].Value = info.PayexpectNum;
            parm[15].Value = info.PayWay;
            parm[16].Value = info.Managers;
            parm[17].Value = info.ConfirmType;
            parm[18].Value = info.Remark;
            parm[19].Value = info.RemittancesCurrency;
            parm[20].Value = info.RemittancesMoney;
            parm[21].Value = info.PhotoPath;
            parm[22].Value = info.OperateIp;
            parm[23].Value = info.OperateNum;
            parm[24].Value = info.Remittancesid;
            parm[25].Value = info.RemitStatus;
            parm[26].Value = 0;
            parm[26].Direction = ParameterDirection.Output;
            parm[27].Value = info.IsGSQR;
            DBHelper.ExecuteNonQuery("MemberRemittances_AddremitMoney", parm, CommandType.StoredProcedure);
            id = int.Parse(parm[26].Value.ToString());
        }


        /// <summary>
        /// 添加会员汇款，更新会员金额——ds2012——CK-带事务
        /// </summary>
        /// <param name="info">汇款信息对象</param>
        /// <param name="RateName1">店铺货币汇率名称</param>
        /// <param name="RateName2">实际付款汇率名称</param>
        public static void AddMemberRemittancesTran(RemittancesModel info, string RateName1, string RateName2, out int id, SqlTransaction tran)
        {
            id = 0;
            SqlParameter[] parm = new SqlParameter[] {
            new SqlParameter("@RateName1",SqlDbType.VarChar),
            new SqlParameter("@RateName2",SqlDbType.VarChar),
            new SqlParameter("@ReceivablesDate",SqlDbType.DateTime),
            new SqlParameter("@RemittancesDate",SqlDbType.DateTime),
            new SqlParameter("@ImportBank",SqlDbType.VarChar,50),
            new SqlParameter("@ImportNumber",SqlDbType.NVarChar,500),
            new SqlParameter("@RemittancesAccount",SqlDbType.NVarChar,500),
            new SqlParameter("@RemittancesBank",SqlDbType.VarChar,50),
            new SqlParameter("@SenderID",SqlDbType.NVarChar,500),
            new SqlParameter("@Sender",SqlDbType.NVarChar,500),
            new SqlParameter("@RemitNumber",SqlDbType.VarChar),
            new SqlParameter("@RemitMoney",SqlDbType.Money),
            new SqlParameter("@StandardCurrency",SqlDbType.Int),
            new SqlParameter("@Use",SqlDbType.Int),
            new SqlParameter("@PayExpect",SqlDbType.Int),
            new SqlParameter("@PayWay",SqlDbType.Int),
            new SqlParameter("@Managers",SqlDbType.VarChar,50),
            new SqlParameter("@ConfirmType",SqlDbType.Int),
            new SqlParameter("@Remark",SqlDbType.VarChar,50),
            new SqlParameter("@RemittancesCurrency",SqlDbType.Int),
            new SqlParameter("@RemittancesMoney",SqlDbType.Money),
            new SqlParameter("@Photopath",SqlDbType.VarChar,50),
            new SqlParameter("@OperateIP",SqlDbType.VarChar,50),
            new SqlParameter("@OperateNum",SqlDbType.VarChar,50),
            new SqlParameter("@Remittancesid",SqlDbType.NVarChar,50),
            new SqlParameter("@RemitStatus",SqlDbType.Int),
            new SqlParameter("@id",SqlDbType.Int),
            new SqlParameter("@isgsqr",SqlDbType.Int)
            };
            parm[0].Value = RateName1;
            parm[1].Value = RateName2;
            parm[2].Value = info.ReceivablesDate;
            if (info.RemittancesDate != null)
            {
                parm[3].Value = info.RemittancesDate;
            }
            else
            {
                parm[3].Value = null;
            }
            parm[4].Value = info.ImportBank;
            parm[5].Value = info.ImportNumber;
            parm[6].Value = info.RemittancesAccount;
            parm[7].Value = info.RemittancesBank;
            parm[8].Value = info.SenderID;
            parm[9].Value = info.Sender;
            parm[10].Value = info.RemitNumber;
            parm[11].Value = info.RemitMoney;
            parm[12].Value = info.StandardCurrency;
            parm[13].Value = info.Use;
            parm[14].Value = info.PayexpectNum;
            parm[15].Value = info.PayWay;
            parm[16].Value = info.Managers;
            parm[17].Value = info.ConfirmType;
            parm[18].Value = info.Remark;
            parm[19].Value = info.RemittancesCurrency;
            parm[20].Value = info.RemittancesMoney;
            parm[21].Value = info.PhotoPath;
            parm[22].Value = info.OperateIp;
            parm[23].Value = info.OperateNum;
            parm[24].Value = info.Remittancesid;
            parm[25].Value = info.RemitStatus;
            parm[26].Value = 0;
            parm[26].Direction = ParameterDirection.Output;
            parm[27].Value = info.IsGSQR;
            DBHelper.ExecuteNonQuery(tran, "MemberRemittances_AddremitMoney", parm, CommandType.StoredProcedure);
            id = int.Parse(parm[26].Value.ToString());
        }

        /// <summary>
        /// 返回汇款单信息用户店铺账户查询
        /// </summary>
        /// <returns></returns>
        public static IList<RemittancesModel> GetRemittanceList(int storeId, PaginationModel pagin)
        {
            return null;
        }

        /// <summary>
        /// 根据店铺编号获取店铺信息——ds2012——tianfeng
        /// </summary>
        /// <param name="storeID">店铺编号</param>
        /// <returns></returns>
        public static RemittancesModel GetRemitByStoreID(string storeID)
        {
            RemittancesModel info = null;
            string sql = "select top 1 StandardCurrency,[Use],PayWay,ConfirmType,RemittancesBank,RemittancesAccount,ImportBank,ImportNumber,Sender,SenderID from Remittances where StoreID=@StoreID order by id desc";
            SqlParameter[] parm = new SqlParameter[] {
            new SqlParameter("@StoreID",SqlDbType.VarChar)
            };
            parm[0].Value = storeID;
            SqlDataReader reader = DBHelper.ExecuteReader(sql, parm, CommandType.Text);
            if (reader.Read())
            {
                info = new RemittancesModel();
                info.StandardCurrency = Convert.ToInt32(reader["StandardCurrency"].ToString());
                info.Use = Convert.ToInt32(reader["Use"].ToString());
                info.PayWay = Convert.ToInt32(reader["PayWay"].ToString());
                info.ConfirmType = Convert.ToInt32(reader["ConfirmType"].ToString());
                info.RemittancesBank = reader["RemittancesBank"].ToString();
                info.RemittancesAccount = reader["RemittancesAccount"].ToString();
                info.ImportBank = reader["ImportBank"].ToString();
                info.ImportNumber = reader["ImportNumber"].ToString();
                info.Sender = reader["Sender"].ToString();
                info.SenderID = reader["SenderID"].ToString();
            }
            reader.Close();
            return info;
        }
        /// <summary>
        /// 根据汇单号获取该汇款单信息
        /// </summary>
        /// <param name="storeID">汇单号</param>
        /// <returns></returns>
        public static RemittancesModel GetRemitByHuidan(string huidan)
        {
            RemittancesModel info = null;
            string sql = "select top 1 * from Remittances where Remittancesid=@Remittancesid order by id desc";
            SqlParameter[] parm = new SqlParameter[] {
            new SqlParameter("@Remittancesid",SqlDbType.VarChar)
            };
            parm[0].Value = huidan;
            SqlDataReader reader = DBHelper.ExecuteReader(sql, parm, CommandType.Text);
            if (reader.Read())
            {
                info = new RemittancesModel();
                info.ID = int.Parse(reader["id"].ToString());
                info.StandardCurrency = Convert.ToInt32(reader["StandardCurrency"].ToString());
                info.Use = Convert.ToInt32(reader["Use"].ToString());
                info.PayWay = Convert.ToInt32(reader["PayWay"].ToString());
                info.ConfirmType = Convert.ToInt32(reader["ConfirmType"].ToString());
                info.RemittancesBank = reader["RemittancesBank"].ToString();
                info.RemittancesAccount = reader["RemittancesAccount"].ToString();
                info.ImportBank = reader["ImportBank"].ToString();
                info.ImportNumber = reader["ImportNumber"].ToString();
                info.Sender = reader["Sender"].ToString();
                info.SenderID = reader["SenderID"].ToString();
                info.Remittancesid = reader["Remittancesid"].ToString();
                info.RemitMoney = decimal.Parse(reader["RemitMoney"].ToString());
                info.RemitNumber = reader["RemitNumber"].ToString();
            }
            reader.Close();
            return info;
        }
        /// <summary>
        /// 根据id获取会员信息——ds2012——tianfeng
        /// </summary>
        /// <param name="storeID">汇单号</param>
        /// <returns></returns>
        public static RemittancesModel GetMemberRemitByID(int id)
        {
            RemittancesModel info = null;
            string sql = "select top 1 * from Remittances where id=@Remittancesid order by id desc";
            SqlParameter[] parm = new SqlParameter[] {
            new SqlParameter("@Remittancesid",SqlDbType.VarChar)
            };
            parm[0].Value = id;
            SqlDataReader reader = DBHelper.ExecuteReader(sql, parm, CommandType.Text);
            if (reader.Read())
            {
                info = new RemittancesModel();
                info.ID = int.Parse(reader["id"].ToString());
                info.StandardCurrency = Convert.ToInt32(reader["StandardCurrency"].ToString());
                info.Use = Convert.ToInt32(reader["Use"].ToString());
                info.PayWay = Convert.ToInt32(reader["PayWay"].ToString());
                info.ConfirmType = Convert.ToInt32(reader["ConfirmType"].ToString());
                info.RemittancesBank = reader["RemittancesBank"].ToString();
                info.RemittancesAccount = reader["RemittancesAccount"].ToString();
                info.ImportBank = reader["ImportBank"].ToString();
                info.ImportNumber = reader["ImportNumber"].ToString();
                info.Sender = reader["Sender"].ToString();
                info.SenderID = reader["SenderID"].ToString();
                info.Remittancesid = reader["Remittancesid"].ToString();
                info.RemitMoney = decimal.Parse(reader["RemitMoney"].ToString());
                info.RemitNumber = reader["RemitNumber"].ToString();
                info.RemittancesMoney = decimal.Parse(reader["RemittancesMoney"].ToString());
                info.RemittancesCurrency = int.Parse(reader["RemittancesCurrency"].ToString());
            }
            reader.Close();
            return info;
        }
        /// <summary>
        /// 根据汇单号获取店铺信息——ds2012——tianfeng
        /// </summary>
        /// <param name="storeID">汇单号</param>
        /// <returns></returns>
        public static RemittancesModel GetRemitByID(int ID)
        {
            RemittancesModel info = null;
            string sql = "select top 1 * from Remittances where ID=@ID order by id desc";
            SqlParameter[] parm = new SqlParameter[] {
            new SqlParameter("@ID",SqlDbType.Int)
            };
            parm[0].Value = ID;
            SqlDataReader reader = DBHelper.ExecuteReader(sql, parm, CommandType.Text);
            if (reader.Read())
            {
                info = new RemittancesModel();
                info.ID = int.Parse(reader["id"].ToString());
                info.StandardCurrency = Convert.ToInt32(reader["StandardCurrency"].ToString());
                info.Use = Convert.ToInt32(reader["Use"].ToString());
                info.PayWay = Convert.ToInt32(reader["PayWay"].ToString());
                info.ConfirmType = Convert.ToInt32(reader["ConfirmType"].ToString());
                info.RemittancesBank = reader["RemittancesBank"].ToString();
                info.RemittancesAccount = reader["RemittancesAccount"].ToString();
                info.ImportBank = reader["ImportBank"].ToString();
                info.ImportNumber = reader["ImportNumber"].ToString();
                info.Sender = reader["Sender"].ToString();
                info.SenderID = reader["SenderID"].ToString();
                info.Remittancesid = reader["Remittancesid"].ToString();
                info.RemitMoney = decimal.Parse(reader["RemitMoney"].ToString());
                info.RemitNumber = reader["RemitNumber"].ToString();
                info.RemittancesCurrency = int.Parse(reader["RemittancesCurrency"].ToString());
                info.RemittancesMoney = decimal.Parse(reader["RemittancesMoney"].ToString());
            }
            reader.Close();
            return info;
        }

        /// <summary>
        /// Get statistics money information by storeID——ds2012——tianfeng
        /// </summary>
        /// <param name="storeID">storeID</param>
        /// <returns>return Datatable object</returns>
        public static DataTable GetStatisticsMoneyInfoByStoreID(string storeID, int payExpectNum)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@storeID",SqlDbType.NVarChar,50),
                new SqlParameter("@payExpectNum",SqlDbType.Int)
            };
            sparams[0].Value = storeID;
            sparams[1].Value = payExpectNum;
            return DBHelper.ExecuteDataTable("GetStatisticsMoneyInfoByStoreID", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 获得银行——ds2012——tianfeng
        /// </summary>
        /// <returns></returns>
        public static DataTable GetBank()
        {
            string sql = "Select ID,Bank+ '—'+BankName+'——'+BankBook as BankName From CompanyBank Order By ID Desc";
            return DBHelper.ExecuteDataTable(sql);
        }
        /// <summary>
        /// 获取币种——ds2012——tianfeng
        /// </summary>
        /// <returns></returns>
        public static IList<CurrencyModel> GetCurrency()
        {
            IList<CurrencyModel> list = new List<CurrencyModel>();
            string sql = "select ID,Name,Rate from Currency where bzflag=1";
            SqlDataReader reader = DBHelper.ExecuteReader(sql);
            while (reader.Read())
            {
                CurrencyModel info = new CurrencyModel(Convert.ToInt32(reader["ID"].ToString()));
                info.Name = reader["Name"].ToString();
                info.Rate = double.Parse(reader["Rate"].ToString());
                list.Add(info);
            }
            reader.Close();
            return list;
        }
        /// <summary>
        /// 获取币种——ds2012——tianfeng
        /// </summary>
        /// <returns></returns>
        public static double GetCurrency(int id)
        {
            string sql = "select Rate from Currency where id=" + id;
            Object obj = DBHelper.ExecuteScalar(sql);
            if (obj != null)
            {
                return Convert.ToDouble(obj);
            }
            else
            {
                return 1;
            }
        }
        /// <summary>
        /// 获取系统的标准币种——ds2012——tianfeng
        /// </summary>
        /// <returns></returns>
        public static string GetCurrencyNameByStoreID()
        {
            string currencyName = "";
            string sql = "Select ID from Currency where StandardMoney=ID";
            SqlDataReader reader = DBHelper.ExecuteReader(sql);
            if (reader.Read())
            {
                currencyName = reader["ID"].ToString();
            }
            reader.Close();
            return currencyName;
        }
        /// <summary>
        /// 汇款申报，更新店铺金额——ds2012——tianfeng
        /// </summary>
        /// <param name="info">汇款信息对象</param>
        /// <param name="RateName1">店铺货币汇率名称</param>
        /// <param name="RateName2">实际付款汇率名称</param>
        public static int RemitDeclare(RemittancesModel info, string RateName1, string RateName2)
        {
            int mm = 0;
            try
            {

          
            SqlParameter[] parm = new SqlParameter[] {
            new SqlParameter("@RateName1",SqlDbType.Int),
            new SqlParameter("@RateName2",SqlDbType.Int),
            new SqlParameter("@ReceivablesDate",SqlDbType.DateTime),
            new SqlParameter("@RemittancesDate",SqlDbType.DateTime),
            new SqlParameter("@ImportBank",SqlDbType.VarChar,50),
            new SqlParameter("@ImportNumber",SqlDbType.NVarChar,500),
            new SqlParameter("@RemittancesAccount",SqlDbType.NVarChar,500),
            new SqlParameter("@RemittancesBank",SqlDbType.VarChar,50),
            new SqlParameter("@SenderID",SqlDbType.NVarChar,500),
            new SqlParameter("@Sender",SqlDbType.NVarChar,500),
            new SqlParameter("@RemitNumber",SqlDbType.VarChar),
            new SqlParameter("@RemitMoney",SqlDbType.Money),
            new SqlParameter("@StandardCurrency",SqlDbType.Int),
            new SqlParameter("@Use",SqlDbType.Int),
            new SqlParameter("@PayExpect",SqlDbType.Int),
            new SqlParameter("@PayWay",SqlDbType.Int),
            new SqlParameter("@Managers",SqlDbType.VarChar,50),
            new SqlParameter("@ConfirmType",SqlDbType.Int),
            new SqlParameter("@Remark",SqlDbType.VarChar,50),
            new SqlParameter("@isGSQR",SqlDbType.Bit),
            new SqlParameter("@RemittancesCurrency",SqlDbType.Int),
            new SqlParameter("@RemittancesMoney",SqlDbType.Money),
            new SqlParameter("@OperateIP",SqlDbType.VarChar,50),
            new SqlParameter("@OperateNum",SqlDbType.VarChar,50),
            new SqlParameter("@Remittancesid",SqlDbType.NVarChar,50),
            new SqlParameter("@RemitStatus",SqlDbType.Int),
            new SqlParameter("@IsJL",SqlDbType.Int),
            new SqlParameter("@name",SqlDbType.NVarChar,50),
              new SqlParameter("@PriceJB",info.PriceJB),
                new SqlParameter("@InvestJB",info.InvestJB)
            };

            parm[0].Value = 1;
            parm[1].Value = 1;
            parm[2].Value = info.ReceivablesDate;
            parm[3].Value = info.RemittancesDate;
            parm[4].Value = info.ImportBank;
            parm[5].Value = info.ImportNumber;
            parm[6].Value = info.RemittancesAccount;
            parm[7].Value = info.RemittancesBank;
            parm[8].Value = info.SenderID;
            parm[9].Value = info.Sender;
            parm[10].Value = info.RemitNumber;
            parm[11].Value = info.RemitMoney;
            parm[12].Value = info.StandardCurrency;
            parm[13].Value = info.Use;
            parm[14].Value = info.PayexpectNum;
            parm[15].Value = info.PayWay;
            parm[16].Value = info.Managers;
            parm[17].Value = info.ConfirmType;
            parm[18].Value = info.Remark;
            parm[19].Value = info.IsGSQR;
            parm[20].Value = info.RemittancesCurrency;
            parm[21].Value = info.RemittancesMoney;
            parm[22].Value = info.OperateIp;
            parm[23].Value = info.OperateNum;
            parm[24].Value = info.Remittancesid;
            parm[25].Value = info.RemitStatus;
            parm[26].Value = info.IsJL;
            parm[27].Value = info.name;
            mm=  DBHelper.ExecuteNonQuery("Remittances_MakeRemitMoney", parm, CommandType.StoredProcedure);

            }
            catch (Exception ee)
            { 
                mm=0;
            }

            return mm;
        }



        //public static DataTable jinliucx(string HkID,int bishu)
        //{
        //    SqlParameter[] parm = new SqlParameter[] {
        //    new SqlParameter("@hkid",SqlDbType.NVarChar,50),
        //    new SqlParameter("@ppTimes",SqlDbType.Int),
        //    };
        //    parm[0].Value = HkID;
        //    parm[1].Value = bishu;
        //    //DBHelper.ExecuteNonQuery("zfpp", parm, CommandType.StoredProcedure);
        //    DataTable dt = DAL.DBHelper.ExecuteDataTable("zfpp", parm, CommandType.StoredProcedure);
        //    return dt;
        //}


        ///// <summary>
        ///// 汇款申报，更新会员金额——ds2012——tianfeng
        ///// </summary>
        ///// <param name="info">汇款信息对象</param>
        ///// <param name="RateName1">货币汇率名称</param>
        ///// <param name="RateName2">实际付款汇率名称</param>
        //public static void RemitDeclare(RemittancesModel info, string RateName1, string RateName2)
        //{
        //    SqlParameter[] parm = new SqlParameter[] { 
        //    new SqlParameter("@RateName1",SqlDbType.Int),
        //    new SqlParameter("@RateName2",SqlDbType.Int),
        //    new SqlParameter("@ReceivablesDate",SqlDbType.DateTime),
        //    new SqlParameter("@RemittancesDate",SqlDbType.DateTime),
        //    new SqlParameter("@ImportBank",SqlDbType.VarChar,50),
        //    new SqlParameter("@ImportNumber",SqlDbType.NVarChar,500),
        //    new SqlParameter("@RemittancesAccount",SqlDbType.NVarChar,500),
        //    new SqlParameter("@RemittancesBank",SqlDbType.VarChar,50),
        //    new SqlParameter("@SenderID",SqlDbType.NVarChar,500),
        //    new SqlParameter("@Sender",SqlDbType.NVarChar,500),
        //    new SqlParameter("@Number",SqlDbType.VarChar),
        //    new SqlParameter("@RemitMoney",SqlDbType.Money),
        //    new SqlParameter("@StandardCurrency",SqlDbType.Int),
        //    new SqlParameter("@Use",SqlDbType.Int),
        //    new SqlParameter("@PayExpect",SqlDbType.Int),
        //    new SqlParameter("@PayWay",SqlDbType.Int),
        //    new SqlParameter("@Managers",SqlDbType.VarChar,50),
        //    new SqlParameter("@ConfirmType",SqlDbType.Int),
        //    new SqlParameter("@Remark",SqlDbType.VarChar,50),
        //    new SqlParameter("@isGSQR",SqlDbType.Bit),
        //    new SqlParameter("@RemittancesCurrency",SqlDbType.Int),
        //    new SqlParameter("@RemittancesMoney",SqlDbType.Money),
        //    new SqlParameter("@OperateIP",SqlDbType.VarChar,50),
        //    new SqlParameter("@OperateNum",SqlDbType.VarChar,50),
        //    new SqlParameter("@Remittancesid",SqlDbType.NVarChar,50),
        //    new SqlParameter("@RemitStatus",SqlDbType.Int)
        //    };
        //    parm[0].Value = int.Parse(RateName1);
        //    parm[1].Value = int.Parse(RateName2);
        //    parm[2].Value = info.ReceivablesDate;
        //    parm[3].Value = info.RemittancesDate;
        //    parm[4].Value = info.ImportBank;
        //    parm[5].Value = info.ImportNumber;
        //    parm[6].Value = info.RemittancesAccount;
        //    parm[7].Value = info.RemittancesBank;
        //    parm[8].Value = info.SenderID;
        //    parm[9].Value = info.Sender;
        //    parm[10].Value = info.RemitNumber;
        //    parm[11].Value = info.RemitMoney;
        //    parm[12].Value = info.StandardCurrency;
        //    parm[13].Value = info.Use;
        //    parm[14].Value = info.PayexpectNum;
        //    parm[15].Value = info.PayWay;
        //    parm[16].Value = info.Managers;
        //    parm[17].Value = info.ConfirmType;
        //    parm[18].Value = info.Remark;
        //    parm[19].Value = info.IsGSQR;
        //    parm[20].Value = info.RemittancesCurrency;
        //    parm[21].Value = info.RemittancesMoney;
        //    parm[22].Value = info.OperateIp;
        //    parm[23].Value = info.OperateNum;
        //    parm[24].Value = info.Remittancesid;
        //    parm[25].Value = info.RemitStatus;
        //    DBHelper.ExecuteNonQuery("MemberRemittances_MakeRemitMoney", parm, CommandType.StoredProcedure);
        //}



        /// <summary>
        /// 汇款申报，更新会员金额
        /// </summary>
        /// <param name="info">汇款信息对象</param>
        /// <param name="RateName1">货币汇率名称</param>
        /// <param name="RateName2">实际付款汇率名称</param>
        public static bool RemitDeclare(SqlTransaction tran, RemittancesModel info, string RateName1, string RateName2)
        {
            SqlParameter[] parm = new SqlParameter[] {
            new SqlParameter("@RateName1",SqlDbType.Int),
            new SqlParameter("@RateName2",SqlDbType.Int),
            new SqlParameter("@ReceivablesDate",SqlDbType.DateTime),
            new SqlParameter("@RemittancesDate",SqlDbType.DateTime),
            new SqlParameter("@ImportBank",SqlDbType.VarChar,50),
            new SqlParameter("@ImportNumber",SqlDbType.NVarChar,500),
            new SqlParameter("@RemittancesAccount",SqlDbType.NVarChar,500),
            new SqlParameter("@RemittancesBank",SqlDbType.VarChar,50),
            new SqlParameter("@SenderID",SqlDbType.NVarChar,500),
            new SqlParameter("@Sender",SqlDbType.NVarChar,500),
            new SqlParameter("@Number",SqlDbType.VarChar),
            new SqlParameter("@RemitMoney",SqlDbType.Money),
            new SqlParameter("@StandardCurrency",SqlDbType.Int),
            new SqlParameter("@Use",SqlDbType.Int),
            new SqlParameter("@PayExpect",SqlDbType.Int),
            new SqlParameter("@PayWay",SqlDbType.Int),
            new SqlParameter("@Managers",SqlDbType.VarChar,50),
            new SqlParameter("@ConfirmType",SqlDbType.Int),
            new SqlParameter("@Remark",SqlDbType.VarChar,50),
            new SqlParameter("@isGSQR",SqlDbType.Bit),
            new SqlParameter("@RemittancesCurrency",SqlDbType.Int),
            new SqlParameter("@RemittancesMoney",SqlDbType.Money),
            new SqlParameter("@OperateIP",SqlDbType.VarChar,50),
            new SqlParameter("@OperateNum",SqlDbType.VarChar,50),
            new SqlParameter("@Remittancesid",SqlDbType.NVarChar,50),
            new SqlParameter("@RemitStatus",SqlDbType.Int)
            };
            parm[0].Value = int.Parse(RateName1);
            parm[1].Value = int.Parse(RateName2);
            parm[2].Value = info.ReceivablesDate;
            parm[3].Value = info.RemittancesDate;
            parm[4].Value = info.ImportBank;
            parm[5].Value = info.ImportNumber;
            parm[6].Value = info.RemittancesAccount;
            parm[7].Value = info.RemittancesBank;
            parm[8].Value = info.SenderID;
            parm[9].Value = info.Sender;
            parm[10].Value = info.RemitNumber;
            parm[11].Value = info.RemitMoney;
            parm[12].Value = info.StandardCurrency;
            parm[13].Value = info.Use;
            parm[14].Value = info.PayexpectNum;
            parm[15].Value = info.PayWay;
            parm[16].Value = info.Managers;
            parm[17].Value = info.ConfirmType;
            parm[18].Value = info.Remark;
            parm[19].Value = info.IsGSQR;
            parm[20].Value = info.RemittancesCurrency;
            parm[21].Value = info.RemittancesMoney;
            parm[22].Value = info.OperateIp;
            parm[23].Value = info.OperateNum;
            parm[24].Value = info.Remittancesid;
            parm[25].Value = info.RemitStatus;
            int count = (int)DBHelper.ExecuteNonQuery(tran, "MemberRemittances_MakeRemitMoney", parm, CommandType.StoredProcedure);
            if (count == 0)
                return false;
            return true;
        }

        /// <summary>
        /// 绑定国家——ds2012——tianfeng
        /// </summary>
        /// <returns></returns>
        public static IList<CountryModel> BindCountry_List()
        {
            IList<CountryModel> list = new List<CountryModel>();
            string sql = "Select ID,Name,CountryCode From Country order by id  ";
            SqlDataReader reader = DBHelper.ExecuteReader(sql);
            while (reader.Read())
            {
                CountryModel info = new CountryModel(int.Parse(reader["ID"].ToString()));
                info.Name = reader["Name"].ToString();
                info.CountryCode = reader["CountryCode"].ToString();
                list.Add(info);
            }
            reader.Close();
            return list;
        }
        /// <summary>
        /// 应收账款分页
        /// </summary>
        /// <param name="pagin">分页帮助类</param>
        /// <param name="key">键</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static IList<StoreInfoModel> GetStoreOrderList(PaginationModel pagin, string tableName, string key, string cloumns, string condition)
        {
            IList<StoreInfoModel> list = new List<StoreInfoModel>();
            SqlDataReader reader = SqlDataReaderHelp.DisSqlReader(pagin, tableName, key, cloumns, condition);
            while (reader.Read())
            {
                StoreInfoModel info = new StoreInfoModel();
                info.ID = Convert.ToInt32(reader["ID"].ToString());
                info.StoreID = reader["StoreID"].ToString();
                info.Name = reader["Name"].ToString();
                info.StoreName = reader["StoreName"].ToString();
                info.StoreAddress = reader["StoreAddress"].ToString();
                list.Add(info);
            }
            reader.Close();
            return list;
        }
        /// <summary>
        /// 预收账款分页
        /// </summary>
        /// <param name="pagin">分页帮助类</param>
        /// <param name="key">键</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static IList<RemittancesModel> ReceiveOrderList(PaginationModel pagin, string tableName, string key, string cloumns, string condition)
        {
            IList<RemittancesModel> list = new List<RemittancesModel>();
            SqlDataReader reader = SqlDataReaderHelp.DisSqlReader(pagin, tableName, key, cloumns, condition);
            while (reader.Read())
            {
                RemittancesModel info = new RemittancesModel();
                info.RemitNumber = reader["RemitNumber"].ToString();
                info.Sender = reader["Sender"].ToString();
                info.Managers = reader["Managers"].ToString();
                info.ImportBank = reader["ImportBank"].ToString();
                info.PayWay = Convert.ToInt32(reader["PayWay"].ToString());
                info.Use = Convert.ToInt32(reader["Use"].ToString());
                info.StandardCurrency = Convert.ToInt32(reader["StandardCurrency"].ToString());
                info.ConfirmType = Convert.ToInt32(reader["ConfirmType"].ToString());
                info.SenderID = reader["SenderID"].ToString();
                info.RemitMoney = decimal.Parse(reader["RemitMoney"].ToString());
                info.PayexpectNum = Convert.ToInt32(reader["PayExpectNum"].ToString());
                info.ID = Convert.ToInt32(reader["ID"].ToString());
                info.ReceivablesDate = DateTime.Parse(reader["ReceivablesDate"].ToString());
                info.IsGSQR = bool.Parse(reader["isgsqr"].ToString());
                info.Remark = reader["Remark"].ToString();
                list.Add(info);
            }
            reader.Close();
            return list;
        }
        /// <summary>
        /// 判断店铺汇款是否以审核——ds2012——tianfeng
        /// </summary>
        /// <param name="id">汇款id</param>
        /// <returns></returns>
        public static object IsGSQR(int id)
        {
            string sql = "select IsGSQR from Remittances where id=@ID";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int) };
            parm[0].Value = id;
            Object obj = DBHelper.ExecuteScalar(sql, parm, CommandType.Text);
            return obj;
        }
        /// <summary>
        /// 判断会员汇款是否以审核——ds2012——tianfeng
        /// </summary>
        /// <param name="id">汇款id</param>
        /// <returns></returns>
        public static object IsMemberGSQR(int id)
        {
            string sql = "select IsGSQR from Remittances where RemitStatus=1 and id=@ID";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int) };
            parm[0].Value = id;
            Object obj = DBHelper.ExecuteScalar(sql, parm, CommandType.Text);
            return obj;
        }
        /// <summary>
        /// 根据汇单号判断是否以审核——ds2012——tianfeng
        /// </summary>
        /// <param name="huidan">汇单号</param>
        /// <returns></returns>
        public static object IsGSQRByHuidan(string huidan)
        {
            string sql = "select IsGSQR from Remittances where Remittancesid=@Remittancesid";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@Remittancesid", SqlDbType.NVarChar, 50) };
            parm[0].Value = huidan;
            Object obj = DBHelper.ExecuteScalar(sql, parm, CommandType.Text);
            return obj;
        }
        /// <summary>
        /// 店铺汇款单是否存在——ds2012——tianfeng
        /// </summary>
        /// <param name="id">汇款单编号</param>
        /// <returns></returns>
        public static bool IsExist(int id)
        {
            bool blean = false;
            string sql = "select count(*) from Remittances where id=@ID";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int) };
            parm[0].Value = id;
            Object obj = DBHelper.ExecuteScalar(sql, parm, CommandType.Text);
            if (int.Parse(obj.ToString()) > 0)
            {
                blean = true;
            }
            return blean;
        }

        /// <summary>
        /// 会员汇款单是否存在——ds2012——tianfeng
        /// </summary>
        /// <param name="id">汇款单编号</param>
        /// <returns></returns>
        public static bool MemberIsExist(int id)
        {
            bool blean = false;
            string sql = "select count(*) from Remittances where RemitStatus=1 and  id=@ID";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int) };
            parm[0].Value = id;
            Object obj = DBHelper.ExecuteScalar(sql, parm, CommandType.Text);
            if (int.Parse(obj.ToString()) > 0)
            {
                blean = true;
            }
            return blean;
        }
        /// <summary>
        /// 根据店铺编号统计各期汇款总额
        /// CYB
        /// </summary>
        /// <param name="storeid">店铺编号</param>
        /// <returns></returns>
        public static Object SelRemitMoneyByStoreID(string storeid)
        {
            string sql = "select sum(RemitMoney) from Remittances where StoreID=@num";
            SqlParameter[] sparams = new SqlParameter[]
            {
              new SqlParameter("@num",SqlDbType.NVarChar,50)

            };
            sparams[0].Value = storeid;
            return DBHelper.ExecuteScalar(sql, sparams, CommandType.Text);
        }
        /// <summary>
        /// 根据店铺编号查询付款折合金额和标示币种
        /// CYB
        /// </summary>
        /// <param name="storeid"></param>
        /// <returns></returns>
        public static DataTable GetRemittancesByStoreID(string storeid)
        {
            RemittancesModel remittances = new RemittancesModel();
            string sql = "select RemitMoney,StandardCurrency from Remittances where StoreID=@num";
            SqlParameter[] sparams = new SqlParameter[]
            {
              new SqlParameter("@num",SqlDbType.NVarChar,50)

            };
            sparams[0].Value = storeid;
            return DBHelper.ExecuteDataTable(sql, sparams, CommandType.Text);
        }
        /// <summary>
        /// 根据店铺编号和期数统计对应的已审核的汇款总额
        /// </summary>
        /// <param name="storeid"></param>
        /// <param name="expectNum"></param>
        /// <returns></returns>
        public static Object SelRemitMoneyByStoreIDAnExpectNum_Y(string storeid, int expectNum)
        {
            string sql = "SELECT isnull(sum(RemitMoney),0) FROM Remittances WHERE StoreID=@num and isgsqr=1 and PayExpectNum=@num1";
            SqlParameter[] sparams = new SqlParameter[]
            {
              new SqlParameter("@num",SqlDbType.NVarChar,50),
              new SqlParameter("@num1",SqlDbType.Int),
            };
            sparams[0].Value = storeid;
            sparams[1].Value = expectNum;
            return DBHelper.ExecuteScalar(sql, sparams, CommandType.Text);
        }
        /// <summary>
        /// 根据店铺编号和期数统计对应的未审核的汇款总额
        /// </summary>
        /// <param name="storeid"></param>
        /// <param name="expectNum"></param>
        /// <returns></returns>
        public static Object SelRemitMoneyByStoreIDAnExpectNum_N(string storeid, int expectNum)
        {
            string sql = "SELECT isnull(sum(RemitMoney),0) FROM Remittances WHERE StoreID=@num and isgsqr=0 and PayExpectNum=@num1";
            SqlParameter[] sparams = new SqlParameter[]
            {
              new SqlParameter("@num",SqlDbType.NVarChar,50),
              new SqlParameter("@num1",SqlDbType.Int),
            };
            sparams[0].Value = storeid;
            sparams[1].Value = expectNum;
            return DBHelper.ExecuteScalar(sql, sparams, CommandType.Text);
        }
        /// <summary>
        /// 根据店铺编号统计对应的已审核的汇款总额
        /// </summary>
        /// <param name="storeid"></param>
        /// <returns></returns>
        public static Object SelRemitMoneyByStoreID_Y(string storeid)
        {
            string sql = "SELECT isnull(sum(RemitMoney),0) FROM Remittances WHERE StoreID=@num and isgsqr=1";
            SqlParameter[] sparams = new SqlParameter[]
            {
              new SqlParameter("@num",SqlDbType.NVarChar,50)

            };
            sparams[0].Value = storeid;
            return DBHelper.ExecuteScalar(sql, sparams, CommandType.Text);
        }
        /// <summary>
        /// 根据店铺编号统计对应的未审核的汇款总额
        /// </summary>
        /// <param name="storeid"></param>
        /// <returns></returns>
        public static Object SelRemitMoneyByStoreID_N(string storeid)
        {
            string sql = "SELECT isnull(sum(RemitMoney),0) FROM Remittances WHERE StoreID=@num and isgsqr=0";
            SqlParameter[] sparams = new SqlParameter[]
            {
              new SqlParameter("@num",SqlDbType.NVarChar,50)

            };
            sparams[0].Value = storeid;
            return DBHelper.ExecuteScalar(sql, sparams, CommandType.Text);
        }

        public static string GetCurrencyByID(int type)
        {
            string sql = "select Name from Currency where ID=@num";
            SqlParameter[] sparams = new SqlParameter[]
            {
              new SqlParameter("@num",SqlDbType.Int)

            };
            sparams[0].Value = type;
            object obj = DBHelper.ExecuteScalar(sql, sparams, CommandType.Text);
            if (obj != null)
            {
                return obj.ToString();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 判断汇单号是否存在——ds2012——tianfeng
        /// </summary>
        /// <param name="huidan">汇单号</param>
        /// <returns></returns>
        public static bool isExistsHuiDan(string huidan)
        {
            string sql = "select count(1) from Remittances WHERE Remittancesid=@num";
            SqlParameter[] sparams = new SqlParameter[]
            {
              new SqlParameter("@num",SqlDbType.NVarChar,30)

            };
            sparams[0].Value = huidan;
            int num = int.Parse(DBHelper.ExecuteScalar(sql, sparams, CommandType.Text).ToString());
            if (num > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 判断会员汇单号是否存在
        /// </summary>
        /// <param name="huidan">汇单号</param>
        /// <returns></returns>
        public static bool MemberisExistsHuiDan(string huidan)
        {
            string sql = "select count(1) from MemberRemittances WHERE Remittancesid=@num";
            SqlParameter[] sparams = new SqlParameter[]
            {
              new SqlParameter("@num",SqlDbType.NVarChar,30)

            };
            sparams[0].Value = huidan;
            int num = int.Parse(DBHelper.ExecuteScalar(sql, sparams, CommandType.Text).ToString());
            if (num > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 获得会员汇款相应币种的总金额
        /// </summary>
        /// <param name="isqueren"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static DataTable GetTotalMoneyorCurrency(string isqueren, DateTime begin, DateTime end, string number)
        {
            string quren = "";
            if (isqueren != "-1")
                quren = " and IsGSQR='" + isqueren + "'";
            string sql = "select sum(RemittancesMoney) as totalmoney,currency.Name,currency.id from currency,MemberRemittances where MemberRemittances.RemittancesCurrency=currency.id and Number='" + number + "' and ReceivablesDate>= '" + begin + "' and ReceivablesDate<='" + end + "' " + quren + " group by currency.name,currency.id ";
            return DBHelper.ExecuteDataTable(sql);
        }
        /// <summary>
        /// 电子转账
        /// </summary>
        /// <param name="info">汇款信息对象</param>
        /// <param name="RateName1">货币汇率名称</param>
        /// <param name="RateName2">实际付款汇率名称</param>
        public static void RemitEFT(RemittancesModel info, string RateName1, string RateName2)
        {
            SqlParameter[] parm = new SqlParameter[] {
            new SqlParameter("@RateName1",SqlDbType.Int),
            new SqlParameter("@RateName2",SqlDbType.Int),
            new SqlParameter("@ReceivablesDate",SqlDbType.DateTime),
            new SqlParameter("@RemittancesDate",SqlDbType.DateTime),
            new SqlParameter("@ImportBank",SqlDbType.VarChar,50),
            new SqlParameter("@ImportNumber",SqlDbType.NVarChar,500),
            new SqlParameter("@RemittancesAccount",SqlDbType.NVarChar,500),
            new SqlParameter("@RemittancesBank",SqlDbType.VarChar,50),
            new SqlParameter("@SenderID",SqlDbType.NVarChar,500),
            new SqlParameter("@Sender",SqlDbType.NVarChar,500),
            new SqlParameter("@Number",SqlDbType.VarChar),
            new SqlParameter("@RemitMoney",SqlDbType.Money),
            new SqlParameter("@StandardCurrency",SqlDbType.Int),
            new SqlParameter("@Use",SqlDbType.Int),
            new SqlParameter("@PayExpect",SqlDbType.Int),
            new SqlParameter("@PayWay",SqlDbType.Int),
            new SqlParameter("@Managers",SqlDbType.VarChar,50),
            new SqlParameter("@ConfirmType",SqlDbType.Int),
            new SqlParameter("@Remark",SqlDbType.VarChar,50),
            new SqlParameter("@isGSQR",SqlDbType.Bit),
            new SqlParameter("@RemittancesCurrency",SqlDbType.Int),
            new SqlParameter("@RemittancesMoney",SqlDbType.Money),
            new SqlParameter("@OperateIP",SqlDbType.VarChar,50),
            new SqlParameter("@OperateNum",SqlDbType.VarChar,50),
            new SqlParameter("@Remittancesid",SqlDbType.NVarChar,50),
            new SqlParameter("@Iszhifu",SqlDbType.Int)
            };
            parm[0].Value = int.Parse(RateName1);
            parm[1].Value = int.Parse(RateName2);
            parm[2].Value = info.ReceivablesDate;
            parm[3].Value = info.RemittancesDate;
            parm[4].Value = info.ImportBank;
            parm[5].Value = info.ImportNumber;
            parm[6].Value = info.RemittancesAccount;
            parm[7].Value = info.RemittancesBank;
            parm[8].Value = info.SenderID;
            parm[9].Value = info.Sender;
            parm[10].Value = info.RemitNumber;
            parm[11].Value = info.RemitMoney;
            parm[12].Value = info.StandardCurrency;
            parm[13].Value = info.Use;
            parm[14].Value = info.PayexpectNum;
            parm[15].Value = info.PayWay;
            parm[16].Value = info.Managers;
            parm[17].Value = info.ConfirmType;
            parm[18].Value = info.Remark;
            parm[19].Value = info.IsGSQR;
            parm[20].Value = info.RemittancesCurrency;
            parm[21].Value = info.RemittancesMoney;
            parm[22].Value = info.OperateIp;
            parm[23].Value = info.OperateNum;
            parm[24].Value = info.Remittancesid;
            parm[25].Value = info.RemitStatus;
            DBHelper.ExecuteNonQuery("MemberEFT_MakeRemitMoney", parm, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 奖金转店铺
        /// </summary>
        /// <param name="info"></param>
        /// <param name="RateName1"></param>
        /// <param name="RateName2"></param>
        public static void EFT(RemittancesModel info, string RateName1, string RateName2)
        {
            SqlParameter[] parm = new SqlParameter[] {
            new SqlParameter("@RateName1",SqlDbType.Int),
            new SqlParameter("@RateName2",SqlDbType.Int),
            new SqlParameter("@ReceivablesDate",SqlDbType.DateTime),
            new SqlParameter("@RemittancesDate",SqlDbType.DateTime),
            new SqlParameter("@ImportBank",SqlDbType.VarChar,50),
            new SqlParameter("@ImportNumber",SqlDbType.NVarChar,500),
            new SqlParameter("@RemittancesAccount",SqlDbType.NVarChar,500),
            new SqlParameter("@RemittancesBank",SqlDbType.VarChar,50),
            new SqlParameter("@SenderID",SqlDbType.NVarChar,500),
            new SqlParameter("@Sender",SqlDbType.NVarChar,500),
            new SqlParameter("@StoreID",SqlDbType.VarChar),
            new SqlParameter("@RemitMoney",SqlDbType.Money),
            new SqlParameter("@StandardCurrency",SqlDbType.Int),
            new SqlParameter("@Use",SqlDbType.Int),
            new SqlParameter("@PayExpect",SqlDbType.Int),
            new SqlParameter("@PayWay",SqlDbType.Int),
            new SqlParameter("@Managers",SqlDbType.VarChar,50),
            new SqlParameter("@ConfirmType",SqlDbType.Int),
            new SqlParameter("@Remark",SqlDbType.VarChar,50),
            new SqlParameter("@isGSQR",SqlDbType.Bit),
            new SqlParameter("@RemittancesCurrency",SqlDbType.Int),
            new SqlParameter("@RemittancesMoney",SqlDbType.Money),
            new SqlParameter("@OperateIP",SqlDbType.VarChar,50),
            new SqlParameter("@OperateNum",SqlDbType.VarChar,50),
            new SqlParameter("@Remittancesid",SqlDbType.NVarChar,50),
            new SqlParameter("@RemitStatus",SqlDbType.Int)
            };
            parm[0].Value = int.Parse(RateName1);
            parm[1].Value = int.Parse(RateName2);
            parm[2].Value = info.ReceivablesDate;
            parm[3].Value = info.RemittancesDate;
            parm[4].Value = info.ImportBank;
            parm[5].Value = info.ImportNumber;
            parm[6].Value = info.RemittancesAccount;
            parm[7].Value = info.RemittancesBank;
            parm[8].Value = info.SenderID;
            parm[9].Value = info.Sender;
            parm[10].Value = info.RemitNumber;
            parm[11].Value = info.RemitMoney;
            parm[12].Value = info.StandardCurrency;
            parm[13].Value = info.Use;
            parm[14].Value = info.PayexpectNum;
            parm[15].Value = info.PayWay;
            parm[16].Value = info.Managers;
            parm[17].Value = info.ConfirmType;
            parm[18].Value = info.Remark;
            parm[19].Value = info.IsGSQR;
            parm[20].Value = info.RemittancesCurrency;
            parm[21].Value = info.RemittancesMoney;
            parm[22].Value = info.OperateIp;
            parm[23].Value = info.OperateNum;
            parm[24].Value = info.Remittancesid;
            parm[25].Value = info.RemitStatus;
            DBHelper.ExecuteNonQuery("MoneyTransferShops", parm, CommandType.StoredProcedure);
        }
        ///// <summary>
        ///// 会员奖金转店铺
        ///// </summary>
        ///// <param name="info"></param>
        ///// <param name="RateName1"></param>
        ///// <param name="RateName2"></param>
        //public static void EFT(RemittancesModel info, string RateName1, string RateName2)
        //{
        //    SqlParameter[] parm = new SqlParameter[] { 
        //    new SqlParameter("@RateName1",SqlDbType.Int),
        //    new SqlParameter("@RateName2",SqlDbType.Int),
        //    new SqlParameter("@ReceivablesDate",SqlDbType.DateTime),
        //    new SqlParameter("@RemittancesDate",SqlDbType.DateTime),
        //    new SqlParameter("@ImportBank",SqlDbType.VarChar,50),
        //    new SqlParameter("@ImportNumber",SqlDbType.NVarChar,500),
        //    new SqlParameter("@RemittancesAccount",SqlDbType.NVarChar,500),
        //    new SqlParameter("@RemittancesBank",SqlDbType.VarChar,50),
        //    new SqlParameter("@SenderID",SqlDbType.NVarChar,500),
        //    new SqlParameter("@Sender",SqlDbType.NVarChar,500),
        //    new SqlParameter("@RemittancesNumber",SqlDbType.VarChar,50),
        //    new SqlParameter("@Number",SqlDbType.NVarChar,50),
        //    new SqlParameter("@RemitMoney",SqlDbType.Money),
        //    new SqlParameter("@StandardCurrency",SqlDbType.Int),
        //    new SqlParameter("@Use",SqlDbType.Int),
        //    new SqlParameter("@PayExpect",SqlDbType.Int),
        //    new SqlParameter("@PayWay",SqlDbType.Int),
        //    new SqlParameter("@Managers",SqlDbType.VarChar,50),
        //    new SqlParameter("@ConfirmType",SqlDbType.Int),
        //    new SqlParameter("@Remark",SqlDbType.VarChar,50),
        //    new SqlParameter("@isGSQR",SqlDbType.Bit),
        //    new SqlParameter("@RemittancesCurrency",SqlDbType.Int),
        //    new SqlParameter("@RemittancesMoney",SqlDbType.Money),
        //    new SqlParameter("@OperateIP",SqlDbType.VarChar,50),
        //    new SqlParameter("@OperateNum",SqlDbType.VarChar,50),
        //    new SqlParameter("@Remittancesid",SqlDbType.NVarChar,50),
        //    new SqlParameter("@Iszhifu",SqlDbType.Int),
        //    new SqlParameter("@StoreID",SqlDbType.NVarChar,50)
        //    };
        //    parm[0].Value = int.Parse(RateName1);
        //    parm[1].Value = int.Parse(RateName2);
        //    parm[2].Value = info.ReceivablesDate;
        //    parm[3].Value = info.RemittancesDate;
        //    parm[4].Value = "";
        //    parm[5].Value = "";
        //    parm[6].Value = "";
        //    parm[7].Value = "";
        //    parm[8].Value = "";
        //    parm[9].Value = "";
        //    parm[10].Value = "";
        //    parm[11].Value = info.Number;
        //    parm[12].Value = info.RemitMoney;
        //    parm[13].Value = info.StandardCurrency;
        //    parm[14].Value = info.Use;
        //    parm[15].Value = info.PayexpectNum;
        //    parm[16].Value = info.PayWay;
        //    parm[17].Value = info.Managers;
        //    parm[18].Value = info.ConfirmType;
        //    parm[19].Value = info.Remark;
        //    parm[20].Value = info.IsGSQR;
        //    parm[21].Value = info.RemittancesCurrency;
        //    parm[22].Value = info.RemittancesMoney;
        //    parm[23].Value = info.OperateIp;
        //    parm[24].Value = info.OperateNum;
        //    parm[25].Value = info.Remittancesid;
        //    parm[26].Value = info.Iszhufu;
        //    parm[27].Value = info.StoreID;
        //    DBHelper.ExecuteNonQuery("MemberMoneyTransferShops", parm, CommandType.StoredProcedure);
        //}
        /// <summary>
        /// 会员奖金转店铺
        /// </summary>
        /// <param name="info"></param>
        /// <param name="RateName1">店铺货币汇率名称</param>
        /// <param name="RateName2">实际付款汇率名称</param>
        /// <param name="storeId">店铺ID</param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static bool EFT(RemittancesModel info, string RateName1, string RateName2, string storeId, SqlTransaction tran)
        {
            SqlParameter[] parm = new SqlParameter[] {
            new SqlParameter("@RateName1",SqlDbType.Int),
            new SqlParameter("@RateName2",SqlDbType.Int),
            new SqlParameter("@ReceivablesDate",SqlDbType.DateTime),
            new SqlParameter("@RemittancesDate",SqlDbType.DateTime),
            new SqlParameter("@ImportBank",SqlDbType.VarChar,50),
            new SqlParameter("@ImportNumber",SqlDbType.NVarChar,500),
            new SqlParameter("@RemittancesAccount",SqlDbType.NVarChar,500),
            new SqlParameter("@RemittancesBank",SqlDbType.VarChar,50),
            new SqlParameter("@SenderID",SqlDbType.NVarChar,500),
            new SqlParameter("@Sender",SqlDbType.NVarChar,500),
            new SqlParameter("@Number",SqlDbType.NVarChar,50),
            new SqlParameter("@RemitMoney",SqlDbType.Money),
            new SqlParameter("@StandardCurrency",SqlDbType.Int),
            new SqlParameter("@Use",SqlDbType.Int),
            new SqlParameter("@PayExpect",SqlDbType.Int),
            new SqlParameter("@PayWay",SqlDbType.Int),
            new SqlParameter("@Managers",SqlDbType.VarChar,50),
            new SqlParameter("@ConfirmType",SqlDbType.Int),
            new SqlParameter("@Remark",SqlDbType.VarChar,50),
            new SqlParameter("@isGSQR",SqlDbType.Bit),
            new SqlParameter("@RemittancesCurrency",SqlDbType.Int),
            new SqlParameter("@RemittancesMoney",SqlDbType.Money),
            new SqlParameter("@OperateIP",SqlDbType.VarChar,50),
            new SqlParameter("@OperateNum",SqlDbType.VarChar,50),
            new SqlParameter("@Remittancesid",SqlDbType.NVarChar,50),
            new SqlParameter("@RemitStatus",SqlDbType.Int),
            new SqlParameter("@StoreID",SqlDbType.NVarChar,50)
            };
            parm[0].Value = int.Parse(RateName1);
            parm[1].Value = int.Parse(RateName2);
            parm[2].Value = info.ReceivablesDate;
            parm[3].Value = info.RemittancesDate;
            parm[4].Value = "";
            parm[5].Value = "";
            parm[6].Value = "";
            parm[7].Value = "";
            parm[8].Value = "";
            parm[9].Value = "";
            parm[10].Value = info.RemitNumber;
            parm[11].Value = info.RemitMoney;
            parm[12].Value = info.StandardCurrency;
            parm[13].Value = info.Use;
            parm[14].Value = info.PayexpectNum;
            parm[15].Value = info.PayWay;
            parm[16].Value = info.Managers;
            parm[17].Value = info.ConfirmType;
            parm[18].Value = info.Remark;
            parm[19].Value = info.IsGSQR;
            parm[20].Value = info.RemittancesCurrency;
            parm[21].Value = info.RemittancesMoney;
            parm[22].Value = info.OperateIp;
            parm[23].Value = info.OperateNum;
            parm[24].Value = info.Remittancesid;
            parm[25].Value = info.RemitStatus;
            parm[26].Value = storeId;
            int coun = (int)DBHelper.ExecuteNonQuery(tran, "MemberMoneyTransferShops", parm, CommandType.StoredProcedure);
            if (coun == 0)
                return false;
            return true;
        }

        /// <summary>
        /// 店铺是否存在
        /// </summary>
        /// <param name="id">店铺编号</param>
        /// <returns></returns>
        public static bool IsStoreExist(string number)
        {
            bool blean = false;
            string sql = "select count(*) from StoreInfo where StoreID=@Number";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@Number", SqlDbType.NVarChar, 50) };
            parm[0].Value = number;
            Object obj = DBHelper.ExecuteScalar(sql, parm, CommandType.Text);
            if (int.Parse(obj.ToString()) > 0)
            {
                blean = true;
            }
            return blean;
        }

        public static bool IsMemberExist(string number)
        {
            bool blean = false;
            string sql = "select count(*) from MemberInfo where Number=@Number";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@Number", SqlDbType.NVarChar, 50) };
            parm[0].Value = number;
            Object obj = DBHelper.ExecuteScalar(sql, parm, CommandType.Text);
            if (int.Parse(obj.ToString()) > 0)
            {
                blean = true;
            }
            return blean;
        }

        /// <summary>
        /// 店铺是否存在
        /// </summary>
        /// <param name="id">店铺编号</param>
        /// <returns></returns>
        public static bool IsMemberExist(string number, string Name)
        {
            bool blean = false;
            string sql = "select count(*) from MemberInfo where Number=@Number and Name=@Name";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@Number", SqlDbType.NVarChar, 50), new SqlParameter("@Name", SqlDbType.NVarChar, 500) };
            parm[0].Value = number;
            parm[1].Value = Name;
            Object obj = DBHelper.ExecuteScalar(sql, parm, CommandType.Text);
            if (int.Parse(obj.ToString()) > 0)
            {
                blean = true;
            }
            return blean;
        }

        /// <summary>
        /// 店铺是否存在
        /// </summary>
        /// <param name="id">店铺编号</param>
        /// <returns></returns>
        public static bool IsStoreExist(string number, string Name)
        {
            bool blean = false;
            string sql = "select count(*) from StoreInfo where StoreID=@Number and Name=@Name";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@Number", SqlDbType.NVarChar, 50), new SqlParameter("@Name", SqlDbType.NVarChar, 500) };
            parm[0].Value = number;
            parm[1].Value = Name;
            Object obj = DBHelper.ExecuteScalar(sql, parm, CommandType.Text);
            if (int.Parse(obj.ToString()) > 0)
            {
                blean = true;
            }
            return blean;
        }
        /// <summary>
        /// 根据会员编号获取店铺编号
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string GetStoreIDByNumber(string number)
        {
            string sql = "select StoreID from MemberInfo where Number=@number";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@number", SqlDbType.NVarChar, 50) };
            parm[0].Value = number;
            object obj = DBHelper.ExecuteScalar(sql, parm, CommandType.Text);
            if (obj != null)
                return obj.ToString();
            else
                return "";
        }
        /// <summary>
        /// 根据汇单ID获取信息
        /// </summary>
        /// <param name="huidan"></param>
        /// <returns></returns>
        public static RemittancesModel GetMemberRemittances(string huidan)
        {
            RemittancesModel info = null;
            string sql = "select top 1 * from Remittances where Remittancesid=@Remittancesid order by id desc";
            SqlParameter[] parm = new SqlParameter[] {
            new SqlParameter("@Remittancesid",SqlDbType.VarChar)
            };
            parm[0].Value = huidan;
            SqlDataReader reader = DBHelper.ExecuteReader(sql, parm, CommandType.Text);
            if (reader.Read())
            {
                info = new RemittancesModel();
                info.ID = int.Parse(reader["id"].ToString());
                info.StandardCurrency = Convert.ToInt32(reader["StandardCurrency"].ToString());
                info.Use = Convert.ToInt32(reader["Use"].ToString());
                info.PayWay = Convert.ToInt32(reader["PayWay"].ToString());
                info.ConfirmType = Convert.ToInt32(reader["ConfirmType"].ToString());
                info.RemittancesBank = reader["RemittancesBank"].ToString();
                info.RemittancesAccount = reader["RemittancesAccount"].ToString();
                info.ImportBank = reader["ImportBank"].ToString();
                info.ImportNumber = reader["ImportNumber"].ToString();
                info.Sender = reader["Sender"].ToString();
                info.SenderID = reader["SenderID"].ToString();
                info.Remittancesid = reader["Remittancesid"].ToString();
                info.RemitMoney = decimal.Parse(reader["RemitMoney"].ToString());
                info.RemitNumber = reader["RemitNumber"].ToString();
                info.RemittancesMoney = decimal.Parse(reader["RemittancesMoney"].ToString());
            }
            reader.Close();
            return info;
        }

        public static object IsGSQRMemberByHuidan(string huidan)
        {
            string sql = "select IsGSQR from Remittances where Remittancesid=@Remittancesid";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@Remittancesid", SqlDbType.NVarChar, 50) };
            parm[0].Value = huidan;
            Object obj = DBHelper.ExecuteScalar(sql, parm, CommandType.Text);
            return obj;
        }
        /// <summary>
        /// 判断汇单号是否存在——ds2012——tianfeng
        /// </summary>
        /// <param name="huidan">汇单号</param>
        /// <returns></returns>
        public static bool isMemberExistsHuiDan(string huidan)
        {
            string sql = "select count(1) from Remittances WHERE Remittancesid=@num";
            SqlParameter[] sparams = new SqlParameter[]
            {
              new SqlParameter("@num",SqlDbType.NVarChar,30)

            };
            sparams[0].Value = huidan;

            int num = int.Parse(DBHelper.ExecuteScalar(sql, sparams, CommandType.Text).ToString());
            if (num > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 查询店铺信息——ds2012——tianfeng
        /// </summary>
        /// <param name="table"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static DataTable QueryStore(string table, string condition)
        {
            string cmd = "select id,StoreID ,Name ,StoreName ,StoreAddress,Cpccode from " + table + " where " + condition + "";
            DataTable dt = DBHelper.ExecuteDataTable(cmd);
            return dt;
        }

        /// <summary>
        /// 根据银行编码查询银行名称——ds2012——tianfeng
        /// </summary>
        /// <param name="bankcode"></param>
        /// <returns></returns>
        public static string QueryBankName(string bankcode)
        {
            string sql = "select isnull(bankname,'') from memberbank where bankcode=@bankcode";
            SqlParameter[] par = new SqlParameter[]{
                new SqlParameter("@bankcode",bankcode)
            };
            return DBHelper.ExecuteScalar(sql, par, CommandType.Text).ToString();
        }

        /// <summary>
        /// 根据付款用途统计金额（店铺汇款）——ds2012——tianfeng
        /// </summary>
        /// <param name="use"></param>
        /// <returns></returns>
        public static double GetUseTotal(int use)
        {
            string sql = "select isnull(sum(remitmoney),0) from Remittances where [use]=@use";
            SqlParameter[] par = new SqlParameter[]{
                new SqlParameter("@use",use)
            };
            double d = Convert.ToDouble(DBHelper.ExecuteScalar(sql, par, CommandType.Text));
            return d;
        }

        /// <summary>
        /// 查询会员汇款总计——ds2012——tianfeng
        /// </summary>
        /// <returns></returns>
        public static double GetTotalMemberRemittances()
        {
            string sql = "select isnull(sum(remitmoney),0) from Remittances";
            double d = Convert.ToDouble(DBHelper.ExecuteScalar(sql));
            return d;
        }

        /// <summary>
        /// 通用的统计金额——ds2012——tianfeng
        /// </summary>
        /// <param name="clounms"></param>
        /// <param name="table"></param>
        /// <param name="sqlwhere"></param>
        /// <returns></returns>
        public static double GetTotalMoney(string clounms, string table, string sqlwhere)
        {
            string sql = "select isnull(sum(" + clounms + "),0) from " + table + sqlwhere;
            double d = Convert.ToDouble(DBHelper.ExecuteScalar(sql));
            return d;
        }

        /// <summary>
        /// 执行添加汇款单数据
        /// </summary>
        /// <param name="number"></param>
        /// <param name="ttprice"></param>
        /// <param name="ip"></param>
        /// <param name="remark"></param>
        public static string GetAddnewRemattice(string number, double ttprice, string ip, string orderid, string remark, int rotype)
        {
            string sqlpro = "GetAddnewRemittances";
            SqlParameter[] sps = new SqlParameter[] {
              new SqlParameter("@Opnumber",number),
              new SqlParameter("@rtotalmoney",ttprice),
              new SqlParameter("@opip",ip),
              new SqlParameter("@orderid",orderid),
              new SqlParameter("@remark",remark) ,
              new SqlParameter("@rotype",rotype)

            };

            string id = DBHelper.ExecuteScalar(sqlpro, sps, CommandType.StoredProcedure).ToString();
            return id;
        }


        public static bool WithdrawMoney(string number, double ttprice, string HKID, string RemBankBook, string RemBankname, string RemBankaddress)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    if (!GetAddnewRemattice1(tran, number, ttprice, HKID, RemBankBook, RemBankname, RemBankaddress))
                    {
                        tran.Rollback();
                        return false;
                    }


                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return false;
                }
                finally
                {
                    conn.Close();
                }

            }

        }

        public static bool GetAddnewRemattice1(SqlTransaction tran,string number, double ttprice,string HKID,string RemBankBook, string RemBankname,string RemBankaddress)
        {
            string sqlpro = "insert into remtemp(number,RemittancesId,totalmoney,totalRmbmoney,fixid,MemberFlag,RemBankBook,RemBankname,RemBankaddress,SureMoney,isusepay) "+
                "values(@Opnumber,@HKID,@rtotalmoney,@rtotalmoney,1,1,@RemBankBook,@RemBankname,@RemBankaddress,@rtotalmoney,1)";
            SqlParameter[] sps = new SqlParameter[] {
              new SqlParameter("@Opnumber",SqlDbType.NVarChar,2000),
              new SqlParameter("@rtotalmoney",SqlDbType.Decimal),
              new SqlParameter("@HKID",SqlDbType.NVarChar,2000),
              new SqlParameter("@RemBankBook",SqlDbType.NVarChar,2000),
              new SqlParameter("@RemBankname",SqlDbType.NVarChar,2000),
              new SqlParameter("@RemBankaddress",SqlDbType.NVarChar,2000)

            };

            sps[0].Value = number;
            sps[1].Value = ttprice;
            sps[2].Value = HKID;
            sps[3].Value = RemBankBook;
            sps[4].Value = RemBankname;
            sps[5].Value = RemBankaddress;
            //int count = (int)DBHelper.ExecuteNonQuery(tran, strSql, para, CommandType.Text);
            int count = (int)DBHelper.ExecuteNonQuery(tran, sqlpro, sps, CommandType.Text);
            if (count <= 0)
                return false;
            return true;
        }

        /// <summary>
        /// 判断汇款的标示是否生成
        /// </summary>
        /// <param name="rmid"></param>
        /// <returns></returns>
        public static bool getishasfix(string rmid) 
        {
            string sqlstr = "select count(0) from  retemp where remittancesid=@rid";
            SqlParameter[] sps = new SqlParameter[] {new SqlParameter("@rid",rmid) };
            int count =Convert.ToInt32( DBHelper.ExecuteScalar(sqlstr,sps,CommandType.Text));
            return count > 0;
        }



        /// <summary>
        /// 根据条件获取汇款单列表
        /// </summary>
        /// <param name="strtime"></param>
        /// <param name="endtime"></param>
        /// <param name="comisqr"></param>
        /// <param name="mbisqr"></param>
        /// <param name="mbname"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public static DataSet GetRemitlist(string condition, out  int totalpage, out int curpage, int page)
        {
            int t = 0;
            int c = 0;
            string sqlstr = "GetRemitlist";
            SqlParameter[] sps = new SqlParameter[] {
              new SqlParameter("@condition", condition), 
              new SqlParameter("@page", page),   
              new SqlParameter("@totalpage", t),
                 new SqlParameter("@curpge", c)
             };
            sps[2].Direction = ParameterDirection.Output;
            sps[3].Direction = ParameterDirection.Output;

            DataSet dt = DBHelper.ExecuteDataSet(sqlstr, sps, CommandType.StoredProcedure);
            totalpage = Convert.ToInt32(sps[2].Value);
            curpage = Convert.ToInt32(sps[3].Value);
            return dt;

        }


        /// <summary>
        ///  修改网商的确认汇款功能
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int Doisuseconfirm(string number)
        {
            string sqlpro = "Updateconfirmisuse";
            SqlParameter[] sps = new SqlParameter[] {
             new SqlParameter("@number",number)
            };
            int count = Convert.ToInt32(DBHelper.ExecuteNonQuery(sqlpro, sps, CommandType.StoredProcedure));
            return count;
        }

        /// <summary>
        /// 完成充值过程
        /// </summary>
        /// <param name="rimetid"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int PaymentChongzhi(string rimetid, int type)
        {
            string sqlproc = "PaymentMoney";
            int er = 0;
            SqlParameter[] sps = new SqlParameter[] {
              new SqlParameter("@Remittancesid",rimetid),
              new SqlParameter("@type",type),
             new SqlParameter("@err",er)
            };
            sps[2].Direction = ParameterDirection.Output;
            DBHelper.ExecuteNonQuery(sqlproc, sps, CommandType.StoredProcedure);
            er = Convert.ToInt32(sps[2].Value);

            return er;
        }

        public static void GetAddnewRetmp(string opnumber, string rmid, string ip, string remark, int rotype)
        {
            string sqlpro = "GetAddnewRetmp";
            SqlParameter[] sps = new SqlParameter[] {
             new SqlParameter("@Opnumber",opnumber),
             new SqlParameter("@RemittancesId",rmid),
             new SqlParameter("@opip",ip),
             new SqlParameter("@remark",remark) ,
             new SqlParameter("@rotype",@rotype)
         
            };

            DBHelper.ExecuteNonQuery(sqlpro, sps, CommandType.StoredProcedure);
        }



        public static int GetnewRemitcount()
        {
            string sqlstr = "select count(0) from remtemp where  flag=0 and memberflag=1  and   sound=0  and   membersuretime >dateadd(dd,-3,getdate()) ";

            int count = Convert.ToInt32(DBHelper.ExecuteScalar(sqlstr));
            return count;
        }

        /// <summary>
        /// 关闭声音
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int Getupdatesound(int rpid, int p)
        {


            string sqlstr = "update remtemp set  sound=1 where   id= " + rpid;
            if (p == 1)
                sqlstr = "update remtempstore set  sound=1 where  id= " + rpid;
            int count = Convert.ToInt32(DBHelper.ExecuteNonQuery(sqlstr));
            return count;

        }

        /// <summary>
        /// 操作员管理 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="pass"></param>
        /// <param name="ip"></param>
        /// <param name="dotype"></param>
        /// <returns></returns>
        public static int doOrgman(string username, string pass, string ip, int dotype)
        {
            string sqlpro = "AddvalidateOrgman";
            int op = 0;
            SqlParameter[] sps = new SqlParameter[] { 
              new SqlParameter("@username",username),
              new SqlParameter("@userpass",pass),
              new SqlParameter("@dousetype",dotype),
              new SqlParameter("@doip",ip),
              new SqlParameter("@op",op)
            };
            sps[4].Direction = ParameterDirection.Output;
            DBHelper.ExecuteNonQuery(sqlpro, sps, CommandType.StoredProcedure);
            op = Convert.ToInt32(sps[4].Value);

            return op;


        }

        /// <summary>
        /// 关闭声音
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int Getupdatesound(string id)
        {
            string sqlstr = "update remtemp set  sound=1 where   remittancesid= '" + id + "'  ";
            int count = Convert.ToInt32(DBHelper.ExecuteNonQuery(sqlstr));
            return count;
        }


        /// <summary>
        /// 获取银行卡号列表
        /// </summary>
        /// <returns></returns>
        public static DataSet GetBankDataSet()
        {
            string sqlstr = " select *  from companybank ";

            DataSet ds = DBHelper.ExecuteDataSet(sqlstr,new SqlParameter[]{}, CommandType.Text);
            return ds;
        }

        /// <summary>
        /// 获取汇款表数据汇款id
        /// </summary>
        /// <param name="remid"></param>
        /// <returns></returns>
        public static DataTable GetRemittanceinfobyremid(string  remid)
        {
            DataTable dt = null;
            string sqlstr = "select  * from remittances  where remittancesid = @remid";
            SqlParameter[] sps = new SqlParameter[] { 
            new SqlParameter("@remid",remid)};
            dt = DBHelper.ExecuteDataTable(sqlstr,sps,CommandType.Text);
            return dt;
        }

        /// <summary>
        /// 在线支付时添加对应汇款单
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="p"></param>
        /// <param name="ip"></param>
        /// <param name="opert"></param>
        /// <param name="p_5"></param>
        /// <returns></returns>
        public static string AddRemittancebytypeOnline(string orderid, int p, string ip, string opert, int p_5 )
        {
            string sqlpro = "AddRemittancebytypeOnline";

            SqlParameter[] sps = new SqlParameter[] { 
             new SqlParameter("@orderid",orderid),
             new SqlParameter("@tp",p),
             new SqlParameter("@ip",ip),
             new SqlParameter("@oper",opert),
             new SqlParameter("@paycurry",p_5) 
            };
            object ovj = DBHelper.ExecuteScalar(sqlpro, sps, CommandType.StoredProcedure);
            return ovj == null ? "" : ovj.ToString();
        }
        /// <summary>
        /// 修改汇款单状态
        /// </summary>
        /// <param name="hkid"></param>
        /// <param name="payway"></param>
        public static void     UpdateOnlinepayway(string  hkid ,int payway)
        {
            string sqlstr = " update  remittances set payway=@payway  where remittancesid =@hkid";
            SqlParameter[] spps = new SqlParameter[] { new SqlParameter("@payway",payway),
            new SqlParameter("@hkid",hkid)};
            DBHelper.ExecuteNonQuery(sqlstr,spps,CommandType.Text);
        }
        /// <summary>
        /// 公司确认 在线 充值 支付 收款 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="time"></param>
        /// <param name="mtype"></param>
        public static void UpdateRemittancereceivabledate(string rmid, DateTime time )
        {
            string sql = "update   remittances set  receivablesdate =@dtime where remittancesid=@rmid ";
           
            SqlParameter[] sps = new SqlParameter[] { 
             new SqlParameter("@dtime",time),
             new SqlParameter("@rmid",rmid)
            };
            DBHelper.ExecuteNonQuery(sql, sps, CommandType.Text);
        }

        /// <summary>
        /// 根据类型获取不同经销商 专卖店 汇款
        /// </summary>
        /// <param name="remid"></param>
        /// <param name="mtp"></param>
        /// <returns></returns>
        public static DataTable GetRemttancesbyrmidandtp(string remid )
        {
            string sqlstr = "select remitnumber,  r.remittancesid,r.receivablesdate,r.remittancesdate,r.remitnumber as number ,case r.RemitStatus    when  1 then (select name from memberinfo mb  where mb.number=r.remitnumber )  when  0 then (select name from storeinfo st where st.storeid=r.remitnumber ) end as name ,r.remitmoney ,r.payway   from remittances r  where  r.isgsqr=0 and   remittancesid=@rmid";
             
            SqlParameter[] sps = new SqlParameter[] { new SqlParameter("@rmid", remid) };
            DataTable dt = DBHelper.ExecuteDataTable(sqlstr, sps, CommandType.Text);
            return dt;
        }

        /// <summary>
        /// 如果不适用普通汇款功能，清除生成的remtemp表数据
        /// </summary>
        /// <param name="hkid"></param>
        /// <param name="p"></param>
        public static void DelRemittancesrelationremtemp(string hkid )
        {
            string sqlstr = " delete  remtemp   where remittancesid=@hkid  and flag=0 ; delete remittances where relationorderid<>'' and  remittancesid=@hkid "; 
            SqlParameter[] sps = new SqlParameter[] {
              new SqlParameter("@hkid",hkid)
            };
            DBHelper.ExecuteNonQuery(sqlstr, sps, CommandType.Text);

        }
        /// <summary>
        /// 如果是用普通汇款功能，更新字段isusepay=1 表数据
        /// </summary>
        /// <param name="hkid"></param>
        /// <param name="p"></param>
        public static void UPRemittancesre(string hkid)
        {
            string sqlstr = "  update remtemp set  isusepay=1 where  remittancesid=@hkid ";
            SqlParameter[] sps = new SqlParameter[] {
              new SqlParameter("@hkid",hkid)
            };
            DBHelper.ExecuteNonQuery(sqlstr, sps, CommandType.Text);

        }



    }
}
