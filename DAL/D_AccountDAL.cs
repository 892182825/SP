using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class D_AccountDAL
    {
         
        public static void AddAccount(string number, double money, D_AccountSftype sftype, D_AccountKmtype kmtype, DirectionEnum direction,string str,SqlTransaction tran)
        {
            if (number != "" && money != 0)
            {
                string sql = "INSERT INTO [storeaccount]([number],[happentime],[happenmoney],[Balancemoney],[Direction],[sftype],[kmtype],[remark])VALUES(@number,@happentime,@happenmoney,@Balancemoney,@Direction,@sftype,@kmtype,@remark)";
                SqlParameter[] parm = new SqlParameter[] { 
                new SqlParameter("@number",SqlDbType.NVarChar,50),
                new SqlParameter("@happentime",SqlDbType.DateTime),
                new SqlParameter("@happenmoney",SqlDbType.Money),
                new SqlParameter("@Balancemoney",SqlDbType.Money),
                new SqlParameter("@Direction",SqlDbType.TinyInt),
                new SqlParameter("@sftype",SqlDbType.TinyInt),
                new SqlParameter("@kmtype",SqlDbType.TinyInt),
                new SqlParameter("@remark",SqlDbType.NVarChar,500)};
                double Residual = GetBalancemoney(sftype,number,tran);
                if(DirectionEnum.AccountReduced==direction)
                {
                    money=-money;
                }
                parm[0].Value = number;
                parm[1].Value = DateTime.Now.ToUniversalTime();
                parm[2].Value = money;
                parm[3].Value = Residual + money;
                parm[4].Value = (int)direction;
                parm[5].Value = (int)sftype;
                parm[6].Value = (int)kmtype;
                parm[7].Value = str;

                DBHelper.ExecuteNonQuery(tran, sql, parm, CommandType.Text);
            }
        }

        public static void AddAccount(string number, double money, D_AccountSftype sftype, D_AccountKmtype kmtype, DirectionEnum direction, string str, SqlTransaction tran, bool state)
        {
            if (number != "" && money != 0)
            {
                string sql = "INSERT INTO [D_Account]([number],[happentime],[happenmoney],[Balancemoney],[Direction],[sftype],[kmtype],[remark])VALUES(@number,@happentime,@happenmoney,@Balancemoney,@Direction,@sftype,@kmtype,@remark)";
                SqlParameter[] parm = new SqlParameter[] { 
                new SqlParameter("@number",SqlDbType.NVarChar,50),
                new SqlParameter("@happentime",SqlDbType.DateTime),
                new SqlParameter("@happenmoney",SqlDbType.Money),
                new SqlParameter("@Balancemoney",SqlDbType.Money),
                new SqlParameter("@Direction",SqlDbType.TinyInt),
                new SqlParameter("@sftype",SqlDbType.TinyInt),
                new SqlParameter("@kmtype",SqlDbType.TinyInt),
                new SqlParameter("@remark",SqlDbType.NVarChar,500)};
                double Residual = GetBalancemoney(sftype, number, tran);
                if (DirectionEnum.AccountReduced == direction)
                {
                    money = -money;
                }

                parm[0].Value = number;
                parm[1].Value = DateTime.Now.ToUniversalTime();
                parm[2].Value = money;
                if (state)
                {
                    parm[3].Value = Residual + money - money;
                }
                else
                {
                    parm[3].Value = Residual + money;
                }
                parm[4].Value = (int)direction;
                parm[5].Value = (int)sftype;
                parm[6].Value = (int)kmtype;
                parm[7].Value = str;

                DBHelper.ExecuteNonQuery(tran, sql, parm, CommandType.Text);
            }
        }

        public static int  AddAccount(string Coinname, string number, double money, D_AccountSftype sftype, D_AccountKmtype kmtype, DirectionEnum direction, string str, SqlTransaction tran )
        {
            int r = 0;
            if (number != "" && money != 0)
            {
                string sql = "INSERT INTO [Account"+Coinname+"]([number],[happentime],[happenmoney],[Balancemoney],[Direction],[sftype],[kmtype],[remark])VALUES(@number,@happentime,@happenmoney,@Balancemoney,@Direction,@sftype,@kmtype,@remark)";
                SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@number",SqlDbType.NVarChar,50),
                new SqlParameter("@happentime",SqlDbType.DateTime),
                new SqlParameter("@happenmoney",SqlDbType.Money),
                new SqlParameter("@Balancemoney",SqlDbType.Money),
                new SqlParameter("@Direction",SqlDbType.TinyInt),
                new SqlParameter("@sftype",SqlDbType.TinyInt),
                new SqlParameter("@kmtype",SqlDbType.TinyInt),
                new SqlParameter("@remark",SqlDbType.NVarChar,500)};
                double Residual =Convert.ToDouble(
                    DBHelper.ExecuteScalar( tran, "select  point" + Coinname + "in-point" + Coinname+"out  as rr from memberinfo  where   number='"+number+"'",CommandType.Text));
                if (DirectionEnum.AccountReduced == direction)
                {
                    money = -money;
                }

                parm[0].Value = number;
                parm[1].Value = DateTime.Now.ToUniversalTime();
                parm[2].Value = money; 
                  parm[3].Value = Residual + money; 
                parm[4].Value = (int)direction;
                parm[5].Value = (int)sftype;
                parm[6].Value = (int)kmtype;
                parm[7].Value = str;

             r=   DBHelper.ExecuteNonQuery(tran, sql, parm, CommandType.Text);
            }
            return r;
        }



        public static int GetDeliveryflag(string OrderId, SqlTransaction tran)
        {
            string strSql = "Select isnull(Deliveryflag,0) From OrderGoods where OutStorageOrderID=@OrderId";
            SqlParameter[] para = {
                                      new SqlParameter("@OrderId",SqlDbType.NVarChar,20)
                                  };
            para[0].Value = OrderId;

            int Deliveryflag = (int)DBHelper.ExecuteScalar(tran, strSql, para, CommandType.Text);

            return Deliveryflag;
        }

        /// <summary>
        /// 无事务报单款对账单——ds2012——tianfeng
        /// </summary>
        /// <param name="number"></param>
        /// <param name="money"></param>
        /// <param name="sftype"></param>
        /// <param name="kmtype"></param>
        /// <param name="direction"></param>
        /// <param name="str"></param>
        public static void  AddAccount(string number, double money, D_AccountSftype sftype, D_AccountKmtype kmtype, DirectionEnum direction, string str)
        {
            if (number != "" && money != 0)
            {
                SqlParameter[] parm = new SqlParameter[] { 
                new SqlParameter("@number",SqlDbType.NVarChar,50),
                new SqlParameter("@happentime",SqlDbType.DateTime),
                new SqlParameter("@happenmoney",SqlDbType.Money),
                new SqlParameter("@Balancemoney",SqlDbType.Money),
                new SqlParameter("@Direction",SqlDbType.TinyInt),
                new SqlParameter("@sftype",SqlDbType.TinyInt),
                new SqlParameter("@kmtype",SqlDbType.TinyInt),
                new SqlParameter("@remark",SqlDbType.NVarChar,500)};
                double Residual = GetBalancemoney(sftype, number);
                if (DirectionEnum.AccountReduced == direction)
                {
                    money = -money;
                }
                parm[0].Value = number;
                parm[1].Value = DateTime.Now.ToUniversalTime();
                parm[2].Value = money;
                parm[3].Value = Residual;
                parm[4].Value = (int)direction;
                parm[5].Value = (int)sftype;
                parm[6].Value = (int)kmtype;
                parm[7].Value = str;
                DBHelper.ExecuteNonQuery("AddD_Account", parm, CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// 无事务报单款对账单——ds2012——tianfeng
        /// </summary>
        /// <param name="number"></param>
        /// <param name="money"></param>
        /// <param name="sftype"></param>
        /// <param name="kmtype"></param>
        /// <param name="direction"></param>
        /// <param name="str"></param>
        public static void AddAccount1(string number, double money, D_AccountSftype sftype, D_Sftype Dsftype, D_AccountKmtype kmtype, DirectionEnum direction, string str)
        {
            if (number != "" && money != 0)
            {
                SqlParameter[] parm = new SqlParameter[] { 
                new SqlParameter("@number",SqlDbType.NVarChar,50),
                new SqlParameter("@happentime",SqlDbType.DateTime),
                new SqlParameter("@happenmoney",SqlDbType.Money),
                new SqlParameter("@Balancemoney",SqlDbType.Money),
                new SqlParameter("@Direction",SqlDbType.TinyInt),
                new SqlParameter("@sftype",SqlDbType.TinyInt),
                new SqlParameter("@kmtype",SqlDbType.TinyInt),
                new SqlParameter("@remark",SqlDbType.NVarChar,500)};
                double Residual = GetBalancemoney(sftype, number);
                if (DirectionEnum.AccountReduced == direction)
                {
                    money = -money;
                }
                parm[0].Value = number;
                parm[1].Value = DateTime.Now.ToUniversalTime();
                parm[2].Value = money;
                parm[3].Value = Residual;
                parm[4].Value = (int)direction;
                parm[5].Value = (int)Dsftype;
                parm[6].Value = (int)kmtype;
                parm[7].Value = str;
                DBHelper.ExecuteNonQuery("AddD_Account", parm, CommandType.StoredProcedure);
            }
        }


        /// <summary>
        /// 带事务报单款对账单——ds2012——CK
        /// </summary>
        /// <param name="number"></param>
        /// <param name="money"></param>
        /// <param name="sftype"></param>
        /// <param name="kmtype"></param>
        /// <param name="direction"></param>
        /// <param name="str"></param>
        public static int AddAccountTran(string number, double money, D_AccountSftype sftype, D_Sftype Dsftype, D_AccountKmtype kmtype, DirectionEnum direction, string str,SqlTransaction tran)
        {
            int ret = 0;
            if (number != "" && money != 0)
            {
                SqlParameter[] parm = new SqlParameter[] { 
                new SqlParameter("@number",SqlDbType.NVarChar,50),
                new SqlParameter("@happentime",SqlDbType.DateTime),
                new SqlParameter("@happenmoney",SqlDbType.Money),
                new SqlParameter("@Balancemoney",SqlDbType.Money),
                new SqlParameter("@Direction",SqlDbType.TinyInt),
                new SqlParameter("@sftype",SqlDbType.TinyInt),
                new SqlParameter("@kmtype",SqlDbType.TinyInt),
                new SqlParameter("@remark",SqlDbType.NVarChar,500)};
                double Residual = GetBalancemoney(sftype, number, tran);
                if (DirectionEnum.AccountReduced == direction)
                {
                    money = -money;
                }
                parm[0].Value = number;
                parm[1].Value = DateTime.Now.ToUniversalTime();
                parm[2].Value = money;
                parm[3].Value = Residual;
                parm[4].Value = (int)direction;
                parm[5].Value = (int)Dsftype;
                parm[6].Value = (int)kmtype;
                parm[7].Value = str;
                ret = DBHelper.ExecuteNonQuery(tran, "AddD_Account", parm, CommandType.StoredProcedure);
            }
            return ret;
        }


        /// <summary>
        /// 服务机构对账单--ck
        /// </summary>
        /// <param name="number"></param>
        /// <param name="money"></param>
        /// <param name="sftype"></param>
        /// <param name="Dsftype"></param>
        /// <param name="kmtype"></param>
        /// <param name="direction"></param>
        /// <param name="str"></param>
        public static void AddStoreAccount(string number, double money, D_AccountSftype sftype, S_Sftype Ssftype, D_AccountKmtype kmtype, DirectionEnum direction, string str) {
            if (number != "" && money != 0)
            {
                SqlParameter[] parm = new SqlParameter[] { 
                new SqlParameter("@number",SqlDbType.NVarChar,50),
                new SqlParameter("@happentime",SqlDbType.DateTime),
                new SqlParameter("@happenmoney",SqlDbType.Money),
                new SqlParameter("@Balancemoney",SqlDbType.Money),
                new SqlParameter("@Direction",SqlDbType.TinyInt),
                new SqlParameter("@sftype",SqlDbType.TinyInt),
                new SqlParameter("@kmtype",SqlDbType.TinyInt),
                new SqlParameter("@remark",SqlDbType.NVarChar,500)};
                double Residual = GetBalancemoney(sftype, number);
                if (DirectionEnum.AccountReduced == direction)
                {
                    money = -money;
                }
                parm[0].Value = number;
                parm[1].Value = DateTime.Now.ToUniversalTime();
                parm[2].Value = money;
                parm[3].Value = Residual;
                parm[4].Value = (int)direction;
                parm[5].Value = (int)Ssftype;
                parm[6].Value = (int)kmtype;
                parm[7].Value = str;
                DBHelper.ExecuteNonQuery("add_storeAccount", parm, CommandType.StoredProcedure);
            }
        }


        /// <summary>
        /// 服务机构对账单--ck--带事务
        /// </summary>
        /// <param name="number"></param>
        /// <param name="money"></param>
        /// <param name="sftype"></param>
        /// <param name="Dsftype"></param>
        /// <param name="kmtype"></param>
        /// <param name="direction"></param>
        /// <param name="str"></param>
        public static int AddStoreAccount(string number, double money, D_AccountSftype sftype, S_Sftype Ssftype, D_AccountKmtype kmtype, DirectionEnum direction, string str,SqlTransaction tran)
        {
            int ret = 0;
            if (number != "" && money != 0)
            {
                SqlParameter[] parm = new SqlParameter[] { 
                new SqlParameter("@number",SqlDbType.NVarChar,50),
                new SqlParameter("@happentime",SqlDbType.DateTime),
                new SqlParameter("@happenmoney",SqlDbType.Money),
                new SqlParameter("@Balancemoney",SqlDbType.Money),
                new SqlParameter("@Direction",SqlDbType.TinyInt),
                new SqlParameter("@sftype",SqlDbType.TinyInt),
                new SqlParameter("@kmtype",SqlDbType.TinyInt),
                new SqlParameter("@remark",SqlDbType.NVarChar,500)};
                double Residual = GetBalancemoney(sftype, number, tran);
                //if (DirectionEnum.AccountReduced == direction)
                //{
                //    money = -money;
                //}
                parm[0].Value = number;
                parm[1].Value = DateTime.Now.ToUniversalTime();
                parm[2].Value = money;
                parm[3].Value = Residual;
                parm[4].Value = (int)direction;
                parm[5].Value = (int)Ssftype;
                parm[6].Value = (int)kmtype;
                parm[7].Value = str;
                ret = DBHelper.ExecuteNonQuery(tran, "add_storeAccount", parm, CommandType.StoredProcedure);
            }
            return ret;
        }

        public static double GetBalancemoney(D_AccountSftype sftype,string number,SqlTransaction tran)
        {
            string sql="";
            double money = 0;
            switch (sftype)
            {
                case D_AccountSftype.MemberType: sql = "select isnull(Jackpot,0)-isnull(Out,0) as Residual from MemberInfo where Number=@Number"; break;
                case D_AccountSftype.MemberCoshType: sql = "select isnull(TotalRemittances,0)-isnull(TotalDefray,0) as Residual from memberinfo where number=@number"; break;
                case D_AccountSftype.usdtjj: sql = "select isnull(pointAIn,0)-isnull(pointAOut,0) as Residual from memberinfo where number=@number"; break;
                case D_AccountSftype.MemberTypeFx: sql = "select isnull(fuxiaoin,0)-isnull(fuxiaoout,0) as Residual from MemberInfo where Number=@Number"; break;
                case D_AccountSftype.MemberTypeFxth: sql = "select isnull(fuxiaothin,0)-isnull(fuxiaothout,0) as Residual from memberinfo where number=@number"; break;

                case D_AccountSftype.StoreType: sql = "select isnull(TotalAccountMoney,0)-isnull(TotalOrderGoodMoney,0) as Residual from StoreInfo where StoreID=@Number"; break;


                case D_AccountSftype.StoreDingHuokuan: sql = "select isnull(TotalAccountMoney,0)-isnull(TotalOrderGoodMoney,0) as Residual from Storeinfo where storeid=@number"; break;
                case D_AccountSftype.StoreZhouZhuankuan: sql = "select isnull(TurnOverMoney,0)-isnull(TurnOverGoodsMoney,0) as Residual from storeinfo where storeid=@number"; break;
            }
            if (sql != "")
            {
                SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@Number",SqlDbType.NVarChar,50)};
                parm[0].Value = number;
                try
                {
                    object obj = DBHelper.ExecuteScalar(tran,sql, parm, CommandType.Text);

                    if (obj != null)
                    {
                        money = Convert.ToDouble(obj);
                    }
                }
                catch (Exception ex)
                {
                    string ags = ex.Message;
                }
            }
            return money;
        }
        public static double GetBalancemoney(D_AccountSftype sftype, string number)
        {
            string sql = "";
            double money = 0;
            switch (sftype)
            {
                case D_AccountSftype.MemberType: sql = "select isnull(Jackpot,0)-isnull(Out,0) as Residual from MemberInfo where Number=@Number"; break;
                case D_AccountSftype.MemberCoshType: sql = "select isnull(TotalRemittances,0)-isnull(TotalDefray,0) as Residual from memberinfo where number=@number"; break;
                case D_AccountSftype.MemberTypeFx: sql = "select isnull(fuxiaoin,0)-isnull(fuxiaoout,0) as Residual from MemberInfo where Number=@Number"; break;
                case D_AccountSftype.MemberTypeFxth: sql = "select isnull(fuxiaothin,0)-isnull(fuxiaothout,0) as Residual from memberinfo where number=@number"; break;

                case D_AccountSftype.StoreType: sql = "select isnull(TotalAccountMoney,0)-isnull(TotalOrderGoodMoney,0) as Residual from StoreInfo where StoreID=@Number"; break;
                case D_AccountSftype.StoreDingHuokuan: sql = "select isnull(TotalAccountMoney,0)-isnull(TotalOrderGoodMoney,0) as Residual from Storeinfo where storeid=@number"; break;
                case D_AccountSftype.StoreZhouZhuankuan: sql = "select isnull(TurnOverMoney,0)-isnull(TurnOverGoodsMoney,0) as Residual from storeinfo where storeid=@number"; break;

            }
            if (sql != "")
            {
                SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@Number", SqlDbType.NVarChar, 50) };
                parm[0].Value = number;
                try
                {
                    object obj = DBHelper.ExecuteScalar(sql, parm, CommandType.Text);

                    if (obj != null)
                    {
                        money = Convert.ToDouble(obj);
                    }
                }
                catch (Exception ex)
                {
                    string ags = ex.Message;
                }
            }
            return money;
        }
        /// <summary>
        /// 现金对账单--带事务
        /// </summary>
        /// <param name="number">店铺或会员编号</param>
        /// <param name="money">交易金额</param>
        /// <param name="sftype">交易类型</param>
        /// <param name="kmtype">科目</param>
        /// <param name="direction">是进还是出</param>
        public static void AddAccountWithdraw(string number, double money, D_AccountSftype sftype, D_AccountKmtype kmtype, DirectionEnum direction, string str, SqlTransaction tran)
        {
            if (number != "" && money != 0)
            {
                SqlParameter[] parm = new SqlParameter[] { 
                new SqlParameter("@number",SqlDbType.NVarChar,50),
                new SqlParameter("@happentime",SqlDbType.DateTime),
                new SqlParameter("@happenmoney",SqlDbType.Money),
                new SqlParameter("@Balancemoney",SqlDbType.Money),
                new SqlParameter("@Direction",SqlDbType.TinyInt),
                new SqlParameter("@sftype",SqlDbType.TinyInt),
                new SqlParameter("@kmtype",SqlDbType.TinyInt),
                new SqlParameter("@remark",SqlDbType.NVarChar,500)};
                double Residual = GetBalancemoneyWithdraw(sftype, number,tran);
                if (DirectionEnum.AccountReduced == direction)
                {
                    money = -money;
                }
                parm[0].Value = number;
                parm[1].Value = DateTime.Now.ToUniversalTime();
                parm[2].Value = money;
                parm[3].Value = Residual ;
                parm[4].Value = (int)direction;
                parm[5].Value = (int)sftype;
                parm[6].Value = (int)kmtype;
                parm[7].Value = str;
                DBHelper.ExecuteNonQuery(tran, "AddD_AccountWithdraw", parm, CommandType.StoredProcedure);
            }
        }
        /// <summary>
        /// 现金对账单
        /// </summary>
        /// <param name="number">店铺或会员编号</param>
        /// <param name="money">交易金额</param>
        /// <param name="sftype">交易类型</param>
        /// <param name="kmtype">科目</param>
        /// <param name="direction">是进还是出</param>
        public static void AddAccountWithdraw(string number, double money, D_AccountSftype sftype, D_AccountKmtype kmtype, DirectionEnum direction, string str)
        {
            if (number != "" && money != 0)
            {
                SqlParameter[] parm = new SqlParameter[] { 
                new SqlParameter("@number",SqlDbType.NVarChar,50),
                new SqlParameter("@happentime",SqlDbType.DateTime),
                new SqlParameter("@happenmoney",SqlDbType.Money),
                new SqlParameter("@Balancemoney",SqlDbType.Money),
                new SqlParameter("@Direction",SqlDbType.TinyInt),
                new SqlParameter("@sftype",SqlDbType.TinyInt),
                new SqlParameter("@kmtype",SqlDbType.TinyInt),
                new SqlParameter("@remark",SqlDbType.NVarChar,500)};
                double Residual = GetBalancemoneyWithdraw(sftype, number);
                if (DirectionEnum.AccountReduced == direction)
                {
                    money = -money;
                }
                parm[0].Value = number;
                parm[1].Value = DateTime.Now.ToUniversalTime();
                parm[2].Value = money;
                parm[3].Value = Residual;
                parm[4].Value = (int)direction;
                parm[5].Value = (int)sftype;
                parm[6].Value = (int)kmtype;
                parm[7].Value = str;
                DBHelper.ExecuteNonQuery("AddD_AccountWithdraw", parm, CommandType.StoredProcedure);
            }
        }


        /// <summary>
        /// 现金对账单--重载
        /// </summary>
        /// <param name="number">店铺或会员编号</param>
        /// <param name="money">交易金额</param>
        /// <param name="sftype">交易类型</param>
        /// <param name="kmtype">科目</param>
        /// <param name="direction">是进还是出</param>
        public static void AddAccountWithdraw1(string number, double money, D_AccountSftype sftype,D_Sftype Dsftype, D_AccountKmtype kmtype, DirectionEnum direction, string str)
        {
            if (number != "" && money != 0)
            {
                SqlParameter[] parm = new SqlParameter[] { 
                new SqlParameter("@number",SqlDbType.NVarChar,50),
                new SqlParameter("@happentime",SqlDbType.DateTime),
                new SqlParameter("@happenmoney",SqlDbType.Money),
                new SqlParameter("@Balancemoney",SqlDbType.Money),
                new SqlParameter("@Direction",SqlDbType.TinyInt),
                new SqlParameter("@sftype",SqlDbType.TinyInt),
                new SqlParameter("@kmtype",SqlDbType.TinyInt),
                new SqlParameter("@remark",SqlDbType.NVarChar,500)};
                double Residual = GetBalancemoneyWithdraw(sftype, number);
                if (DirectionEnum.AccountReduced == direction)
                {
                    money = -money;
                }
                parm[0].Value = number;
                parm[1].Value = DateTime.Now.ToUniversalTime();
                parm[2].Value = money;
                parm[3].Value = Residual;
                parm[4].Value = (int)direction;
                parm[5].Value = (int)Dsftype;
                parm[6].Value = (int)kmtype;
                parm[7].Value = str;
                DBHelper.ExecuteNonQuery("AddD_AccountWithdraw", parm, CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// 现金对账单--重载--带事务
        /// </summary>
        /// <param name="number">店铺或会员编号</param>
        /// <param name="money">交易金额</param>
        /// <param name="sftype">交易类型</param>
        /// <param name="kmtype">科目</param>
        /// <param name="direction">是进还是出</param>
        public static int AddAccountWithdraw1(string number, double money, D_AccountSftype sftype, D_Sftype Dsftype, D_AccountKmtype kmtype, DirectionEnum direction, string str,SqlTransaction tran)
        {
            int ret = 0;
            if (number != "" && money != 0)
            {
                SqlParameter[] parm = new SqlParameter[] { 
                new SqlParameter("@number",SqlDbType.NVarChar,50),
                new SqlParameter("@happentime",SqlDbType.DateTime),
                new SqlParameter("@happenmoney",SqlDbType.Money),
                new SqlParameter("@Balancemoney",SqlDbType.Money),
                new SqlParameter("@Direction",SqlDbType.TinyInt),
                new SqlParameter("@sftype",SqlDbType.TinyInt),
                new SqlParameter("@kmtype",SqlDbType.TinyInt),
                new SqlParameter("@remark",SqlDbType.NVarChar,500)};
                double Residual = GetBalancemoneyWithdraw(sftype, number, tran);
                //if (DirectionEnum.AccountReduced == direction)
                //{
                //    money = -money;
                //}
                parm[0].Value = number;
                parm[1].Value = DateTime.Now.ToUniversalTime();
                parm[2].Value = money;
                parm[3].Value = Residual;
                parm[4].Value = (int)direction;
                parm[5].Value = (int)Dsftype;
                parm[6].Value = (int)kmtype;
                parm[7].Value = str;
                ret = DBHelper.ExecuteNonQuery(tran, "AddD_AccountWithdraw", parm, CommandType.StoredProcedure);
            }
            return ret;
        }

        public static double GetBalancemoneyWithdraw(D_AccountSftype sftype, string number,SqlTransaction tran)
        {
            string sql = "";
            double money = 0;
            switch (sftype)
            {
                case D_AccountSftype.MemberType: sql = "select isnull(Jackpot,0)-isnull(Out,0) as Residual from MemberInfo where Number=@Number"; break;
                case D_AccountSftype.MemberCoshType: sql = "select isnull(TotalRemittances,0)-isnull(TotalDefray,0) as Residual from memberinfo where number=@number"; break;
                case D_AccountSftype.MemberTypeFx: sql = "select isnull(fuxiaoin,0)-isnull(fuxiaoout,0) as Residual from MemberInfo where Number=@Number"; break;
                case D_AccountSftype.MemberTypeFxth: sql = "select isnull(fuxiaothin,0)-isnull(fuxiaothout,0) as Residual from memberinfo where number=@number"; break;

                case D_AccountSftype.StoreType: sql = "select isnull(TotalAccountMoney,0)-isnull(TotalOrderGoodMoney,0) as Residual from StoreInfo where StoreID=@Number"; break;
                case D_AccountSftype.StoreDingHuokuan: sql = "select isnull(TotalAccountMoney,0)-isnull(TotalOrderGoodMoney,0) as Residual from Storeinfo where storeid=@number"; break;
                case D_AccountSftype.StoreZhouZhuankuan: sql = "select isnull(TurnOverMoney,0)-isnull(TurnOverGoodsMoney,0) as Residual from storeinfo where storeid=@number"; break;
                case D_AccountSftype.zzye: sql = "select isnull(zzye,0)-isnull(xuhao,0) as Residual from memberinfo where Number=@number"; break;
                case D_AccountSftype.MemberTypeBd: sql = "select isnull(pointBIn,0)-isnull(pointBOut,0) as Residual from memberinfo where Number=@number"; break;
                //case D_AccountSftype.MemberTypeBd: sql = "select isnull(pointBIn,0)-isnull(pointBOut,0) as Residual from memberinfo where storeid=@number"; break;
            }
            if (sql != "")
            {
                SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@Number", SqlDbType.NVarChar, 50) };
                parm[0].Value = number;
                object obj = DBHelper.ExecuteScalar(tran,sql, parm, CommandType.Text);
                if (obj != null)
                {
                    money = Convert.ToDouble(obj);
                }
            }
            return money;
        }
        public static double GetBalancemoneyWithdraw(D_AccountSftype sftype, string number)
        {
            string sql = "";
            double money = 0;
            switch (sftype)
            {
                case D_AccountSftype.MemberType: sql = "select isnull(Jackpot,0)-isnull(Out,0) as Residual from MemberInfo where Number=@Number"; break;
                case D_AccountSftype.MemberCoshType: sql = "select isnull(TotalRemittances,0)-isnull(TotalDefray,0) as Residual from memberinfo where number=@number"; break;
                case D_AccountSftype.MemberTypeFx: sql = "select isnull(fuxiaoin,0)-isnull(fuxiaoout,0) as Residual from MemberInfo where Number=@Number"; break;
                case D_AccountSftype.MemberTypeFxth: sql = "select isnull(fuxiaothin,0)-isnull(fuxiaothout,0) as Residual from memberinfo where number=@number"; break;
                case D_AccountSftype.MemberTypeBd: sql = "select isnull(pointBIn,0)-isnull(pointBOut,0) as Residual from memberinfo where number=@number"; break;
                case D_AccountSftype.StoreType: sql = "select isnull(TotalAccountMoney,0)-isnull(TotalOrderGoodMoney,0) as Residual from StoreInfo where StoreID=@Number"; break;
                case D_AccountSftype.StoreDingHuokuan: sql = "select isnull(TotalAccountMoney,0)-isnull(TotalOrderGoodMoney,0) as Residual from Storeinfo where storeid=@number"; break;
                case D_AccountSftype.StoreZhouZhuankuan: sql = "select isnull(TurnOverMoney,0)-isnull(TurnOverGoodsMoney,0) as Residual from storeinfo where storeid=@number"; break;

            }
            if (sql != "")
            {
                SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@Number", SqlDbType.NVarChar, 50) };
                parm[0].Value = number;
                object obj = DBHelper.ExecuteScalar(sql, parm, CommandType.Text);
                if (obj != null)
                {
                    money = Convert.ToDouble(obj);
                }
            }
            return money;
        }
        /// <summary>
        /// 获取期末余额总计
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static double GetTotalBalancemoeny(int type)
        {
            string sql = "";
            double totalBalancemoney = 0;
            if (type == 1)
            {
                sql = "select isnull(sum(totalmoney),0) from (select number,(select top 1 balancemoney from MemberAccount ds where ds.sftype=0 and ds.number=d.number order by happentime desc ) totalmoney from MemberAccount d where sftype=0 group by number) tb";
                object money = DBHelper.ExecuteScalar(sql);
                if (money != null && money != DBNull.Value)
                {
                    totalBalancemoney = Convert.ToDouble(money);
                }
            }
            else if (type == 2)
            {
                sql = "select sum(totalmoney) from (select number,(select top 1 balancemoney from MemberAccount ds where ds.sftype=0 and ds.number=d.number order by happentime desc ) totalmoney from MemberAccount d where sftype=0 group by number) tb";
                object money = DBHelper.ExecuteScalar(sql);
                if (money != null && money != DBNull.Value)
                {
                    totalBalancemoney = Convert.ToDouble(money);
                }
            }
            return totalBalancemoney;
        }
        /// <summary>
        /// 获取所有会员的报单账户余额总计和现金账户余额总计
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllMemberAccountMoney()
        {
            return DBHelper.ExecuteDataTable("select sum(isnull((Jackpot-Out-membership),0)) as TotalMoney,sum(isnull((totalremittances-totaldefray),0)) as TotalOrderMoney from memberinfo");
        }

    }
}
