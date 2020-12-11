using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Model;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
namespace DAL
{
    /*
     *  转账管理
     * **/
    public class ECRemitDetailDAL
    {
        /// <summary>
        /// 转账记录查询
        /// </summary>
        /// <returns></returns>
        public List<ECTransferDetailModel> GetECRemitDetail(int IsRemittances)
        {
            List<ECTransferDetailModel> list = new List<ECTransferDetailModel>();
            string sql = "select * from ECTransferDetail";
            if (IsRemittances >= 0)
            {
                sql = sql + " where IsRemittances =" + IsRemittances;
            }
            SqlDataReader read = DBHelper.ExecuteReader(sql, CommandType.Text);

            while (read.Read())
            {
                ECTransferDetailModel model = new ECTransferDetailModel();
                model.Id = int.Parse(read["ID"].ToString());
                model.IsRemittances = int.Parse(read["IsRemittances"].ToString());
                model.InNumber = read["InNumber"].ToString();
                model.OutNumber = read["OutNumber"].ToString();
                model.OutMoney = double.Parse(read["OutMoney"].ToString());
                model.Date = DateTime.Parse(read["Date"].ToString());
                if (read["RemittancesDate"].ToString() != string.Empty)
                    model.RemittancesDate = DateTime.Parse(read["RemittancesDate"].ToString());
                if (read["ReceivablesDate"].ToString() != string.Empty)
                    model.ReceivablesDate = DateTime.Parse(read["ReceivablesDate"].ToString());
                model.Remark = read["Remark"].ToString();
                list.Add(model);
            }
            read.Close();
            return list;
        }
        /// <summary>
        /// 电子转账 查询
        /// </summary>
        /// <param name="number"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static DataTable GetMember(string number, string name)
        {
            string sql = "select Number,Name,Bank,BankCard,Jackpot-ECTPay-membership-Out as TransferMoney,ReleaseMoney from MemberInfo where Jackpot-ECTPay-membership-Out>0 ";
            if (number != string.Empty)
            {
                sql = sql + " and Number='" + number + "'";
            }
            if (name != string.Empty)
            {
                sql = sql + " and Name='" + name + "'";
            }
            return DBHelper.ExecuteDataTable(sql, CommandType.Text);
        }

        /// <summary> 
        /// 账户密码验证——ds2012——tianfeng
        /// </summary>
        /// <param name="number"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static int ValidatePwd(string number, string pwd)
        {
            int logincount = 0;
            SqlParameter[] par = new SqlParameter[]{
                new SqlParameter("@number",number)
            };
            string logincount_sql = "select isnull(advcount,0),isnull(advtime,getutcdate()) from memberinfo where number=@number";
            DataTable dt = DAL.DBHelper.ExecuteDataTable(logincount_sql,par,CommandType.Text);
            logincount = Convert.ToInt32(dt.Rows[0][0]);
            DateTime nowtime = Convert.ToDateTime(DAL.DBHelper.ExecuteScalar("select getutcdate()"));
            DateTime dtime = Convert.ToDateTime(dt.Rows[0][1]);
            TimeSpan ts = dtime.AddHours(2) - nowtime;
            if (ts.Seconds <= 0)
            {
                string update_member = "update memberinfo set advcount=0,advtime=getutcdate() where number='" + number + "'";
                DAL.DBHelper.ExecuteNonQuery(update_member);
            }
            if (logincount >= 5 && ts.Seconds > 0)
            {
                //msg = "<script language='javascript'>alert('" + GetTran("000000", "对不起，您连续5次输入密码错，请2小时候在登录！") + "');</script>";
                return 2;
            }
            string sql = "select count(*) from MemberInfo where Number =@number and AdvPass =@pwd";
            SqlParameter[] parameter = new SqlParameter[]{
            new SqlParameter("@number",SqlDbType.NVarChar,50),
            new SqlParameter("@pwd",SqlDbType.NVarChar,50)
            };
            parameter[0].Value = number;
            parameter[1].Value = pwd;
            //return Convert.ToInt32(DBHelper.ExecuteScalar(sql, parameter, CommandType.Text).ToString()) > 0 ? true : false;
            int num = Convert.ToInt32(DBHelper.ExecuteScalar(sql, parameter, CommandType.Text).ToString());
            if (num > 0)
            {
                SqlParameter[] pars = new SqlParameter[]{
                    new SqlParameter("@number",number)
                };
                string update = "update memberinfo set advcount=0,advtime=getutcdate() where number=@number";
                DBHelper.ExecuteNonQuery(update,pars,CommandType.Text);
                return 0;
            }
            else
            {
                string up_lost = "update memberinfo set advcount=advcount+1,advtime=getutcdate() where number='" + number + "'";
                DBHelper.ExecuteNonQuery(up_lost);
                return 1;
            }
        }
        /// <summary>
        /// 验证会员是否存在
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static Boolean validateNumber(string number)
        {
            string sql = "select count(Number) from MemberInfo where Number =@number";
            SqlParameter[] parameter = new SqlParameter[]{
            new SqlParameter("@number",SqlDbType.VarChar,40),
            };
            parameter[0].Value = number;
            return Convert.ToInt32(DBHelper.ExecuteScalar(sql, parameter, CommandType.Text).ToString()) > 0 ? true : false;
        }
        /// <summary>
        /// 汇款申报
        /// </summary>
        /// <returns></returns>
        public static Boolean AddECRemitDetail(ECRemitDetailModel remitemodel)
        {
            string str = string.Empty;
            if (remitemodel.PayMentMethod == 0)
            {
                SqlParameter[] parameter = new SqlParameter[]{
                new SqlParameter("@ID",SqlDbType.Int),
                new SqlParameter("@RemitID",SqlDbType.VarChar,40),
                new SqlParameter("@GatheringID",SqlDbType.VarChar,40),
                new SqlParameter("@RemitMoney",SqlDbType.Money),
                new SqlParameter("@Currency",SqlDbType.VarChar,40),
                new SqlParameter("@PaymentExpectNum",SqlDbType.Int),
                new SqlParameter("@PaymentMethod",SqlDbType.Int),
                new SqlParameter("@Confirmed",SqlDbType.Int),
                new SqlParameter("@RemitDate",SqlDbType.DateTime),
                new SqlParameter("@RemitterRemark",SqlDbType.VarChar,200),
                new SqlParameter("@AbouchementRemark",SqlDbType.VarChar,200),
                new SqlParameter("@error",SqlDbType.Int)
            };
                parameter[0].Value = remitemodel.ID;
                parameter[1].Value = remitemodel.RemitID;
                parameter[2].Value = remitemodel.GatheringID;
                parameter[3].Value = remitemodel.RemitMoney;
                parameter[4].Value = remitemodel.Currency;
                parameter[5].Value = remitemodel.PayMentExpectNum;
                parameter[6].Value = remitemodel.PayMentMethod;
                parameter[7].Value = remitemodel.Confirmed;
                parameter[8].Value = remitemodel.RemitDate;
                parameter[9].Value = remitemodel.RemitterRemark;
                parameter[10].Value = remitemodel.AbouchementRemark;
                parameter[11].Direction =ParameterDirection.Output;
                DBHelper.ExecuteNonQuery("AddECRemitDetail", parameter, CommandType.StoredProcedure);
                str= parameter[11].Value.ToString();
            }
            if (remitemodel.PayMentMethod == 1)
            {
                SqlParameter[] parameter = new SqlParameter[]{
                new SqlParameter("@ID",SqlDbType.Int),
                new SqlParameter("@RemitID",SqlDbType.VarChar,40),
                new SqlParameter("@GatheringID",SqlDbType.VarChar,40),
                new SqlParameter("@RemitMoney",SqlDbType.Money),
                new SqlParameter("@Currency",SqlDbType.VarChar,40),
                new SqlParameter("@PaymentExpectNum",SqlDbType.Int),
                new SqlParameter("@PaymentMethod",SqlDbType.Int),
                new SqlParameter("@Confirmed",SqlDbType.Int),
                new SqlParameter("@RemitDate",SqlDbType.DateTime),
                new SqlParameter("@RemitterRemark",SqlDbType.VarChar,200),
                new SqlParameter("@AbouchementRemark",SqlDbType.VarChar,200),

                new SqlParameter("@RemitNumber",SqlDbType.VarChar,50),
                new SqlParameter("@RemitBank",SqlDbType.VarChar,50),
                new SqlParameter("@RemitCard",SqlDbType.VarChar,50),
                new SqlParameter("@AbouchementCard",SqlDbType.VarChar,50),
                new SqlParameter("@Remitter",SqlDbType.VarChar,50),
                new SqlParameter("@IdentityCard",SqlDbType.VarChar,20),
                new SqlParameter("@error",SqlDbType.Int)
            };
                parameter[0].Value = remitemodel.ID;
                parameter[1].Value = remitemodel.RemitID;
                parameter[2].Value = remitemodel.GatheringID;
                parameter[3].Value = remitemodel.RemitMoney;
                parameter[4].Value = remitemodel.Currency;
                parameter[5].Value = remitemodel.PayMentExpectNum;
                parameter[6].Value = remitemodel.PayMentMethod;
                parameter[7].Value = remitemodel.Confirmed;
                parameter[8].Value = remitemodel.RemitDate;
                parameter[9].Value = remitemodel.RemitterRemark;
                parameter[10].Value = remitemodel.AbouchementRemark;
                
                parameter[11].Value = remitemodel.RemitNumber;
                parameter[12].Value = remitemodel.RemitBank;
                parameter[13].Value = remitemodel.RemitCard;
                parameter[14].Value = remitemodel.AbouchementCard;
                parameter[15].Value = remitemodel.Remitter;
                parameter[16].Value = remitemodel.IdentityCard;
                parameter[17].Direction=ParameterDirection.Output;
                DBHelper.ExecuteNonQuery("AddECRemitDetail", parameter, CommandType.StoredProcedure);
                str = parameter[17].Value.ToString();
            }
            return int.Parse(str)>0?false:true;
        }
        /// <summary>
        /// 查看会员电子转账(汇款)明细
        /// </summary>
        /// <returns></returns>
        public static List<ECRemitDetailModel> GetECRemitDetail(ECRemitDetailModel detailmodel, DateTime EndTime)
        {
            //0001-1-1
            string sql = "select * from ECRemitDetail where 1=1  ";
            List<ECRemitDetailModel> list = new List<ECRemitDetailModel>();
            if (detailmodel.RemitID != null)
            {
                sql = sql + " and RemitID ='" + detailmodel.RemitID + "'";
            }
            if (detailmodel.RemitDate != DateTime.Parse("0001-1-1"))
            {
                sql = sql + " and RemitDate between '" + detailmodel.RemitDate + "' and '" + EndTime + "'";
            }
            if (detailmodel.PayMentExpectNum != 0)
            {
                sql = sql + " and PaymentExpectNum =" + detailmodel.PayMentExpectNum;
            }
            SqlDataReader read = DBHelper.ExecuteReader(sql, CommandType.Text);
            while (read.Read())
            {
                ECRemitDetailModel model = new ECRemitDetailModel();
                model.ID = int.Parse(read["ID"].ToString());
                model.IsAffirm = int.Parse(read["IsAffirm"].ToString());
                model.GatheringID = read["GatheringID"].ToString();
                model.RemitID = read["RemitID"].ToString();
                model.PayMentExpectNum = int.Parse(read["PaymentExpectNum"].ToString());
                model.PayMentDate = DateTime.Parse(read["PaymentDate"].ToString());
                model.RemitMoney = double.Parse(read["RemitMoney"].ToString());
                model.Currency = read["Currency"].ToString();
                model.RemitterRemark = read["RemitterRemark"].ToString();
                list.Add(model);
            }
            read.Close();
            return list;
        }
        /// <summary>
        /// 收款确认
        /// </summary>
        /// <returns></returns>
        public static Boolean UptECTransferDetail(string RemitID, int CollectionExpectNum, string GatheringID, int ID, double RemitMoney)
        {
            SqlParameter[] parameter = new SqlParameter[]{
                new SqlParameter("@CollectionExpectNum",SqlDbType.Int),
                new SqlParameter("@RemitID",SqlDbType.VarChar,40),
                new SqlParameter("@GatheringID",SqlDbType.VarChar,50),
                new SqlParameter("@ID",SqlDbType.Int),
                new SqlParameter("@RemitMoney",SqlDbType.Money),
                 new SqlParameter("@date",SqlDbType.DateTime),
                new SqlParameter("@returnstr",SqlDbType.Int)
            };
            parameter[0].Value = CollectionExpectNum;
            parameter[1].Value = RemitID;
            parameter[2].Value = GatheringID;
            parameter[3].Value = ID;
            parameter[4].Value = RemitMoney;
            parameter[5].Value = DateTime.Now.ToUniversalTime();
            parameter[6].Direction = ParameterDirection.Output;
            DBHelper.ExecuteNonQuery("UptECTransferDetail", parameter, CommandType.StoredProcedure);
            string str = parameter[6].Value.ToString();
            return int.Parse(str) > 0 ? false : true;
        }

        /// <summary>
        /// 电子转账——ds2012——tianfeng
        /// </summary>
        /// <param name="detailmodel"></param>
        /// <returns></returns>
        public static int AddMoneyManage(ECTransferDetailModel detailmodel,int type)
        {
            SqlParameter[] parameter = new SqlParameter[]{
            new SqlParameter("@OutNumber",SqlDbType.VarChar,40),
            new SqlParameter("@OutAccountType",SqlDbType.Int),
            new SqlParameter("@OutMoney",SqlDbType.Money,40),
            new SqlParameter("@InNumber",SqlDbType.VarChar,40),
            new SqlParameter("@InAccountType",SqlDbType.Int),
            new SqlParameter("@ExpectNum",SqlDbType.Int),
            new SqlParameter("@OperateIP",SqlDbType.VarChar,40),
            new SqlParameter("@OperateNum",SqlDbType.VarChar,40),
            new SqlParameter("@Remark",SqlDbType.VarChar,40),
            new SqlParameter("@Date",SqlDbType.DateTime),
            new SqlParameter("@intype",SqlDbType.Int),
            new SqlParameter("@returnstr",SqlDbType.Int)
            };
            parameter[0].Value = detailmodel.OutNumber;
            parameter[1].Value = detailmodel.OutMoney;
            parameter[2].Value = detailmodel.outAccountType;
            parameter[3].Value = detailmodel.InNumber;
            parameter[4].Value = detailmodel.inAccountType;
            parameter[5].Value = detailmodel.ExpectNum;
            parameter[6].Value = detailmodel.OperateIP;
            parameter[7].Value = detailmodel.OperateNumber;
            parameter[8].Value = detailmodel.Remark;
            parameter[9].Value = DateTime.Now.ToUniversalTime();
            parameter[10].Value = type;
            parameter[11].Direction = ParameterDirection.Output;
            DBHelper.ExecuteNonQuery("AddECTransferDetail", parameter, CommandType.StoredProcedure);
            string rest = parameter[11].Value.ToString();
            return int.Parse(rest);
        }


        /// <summary>
        /// 电子转账(含多种类型转入)Chengkai(12-05-31)--带事务处理
        /// </summary>
        /// <param name="detailmodel"></param>
        /// <param name="Intype">转入类型--2.为会员现金账户转入 1.为会员消费账户转入 0.为店铺订货款转入</param>
        /// <param name="Outtype">转出类型 -- 1.为会员现金转出 0.为会员消费账户转出</param>
        /// <returns></returns>
        public static int AddMoneyManageTran(ECTransferDetailModel detailmodel, int Outtype, int Intype,SqlTransaction tran)
        {
            SqlParameter[] parameter = new SqlParameter[]{
            new SqlParameter("@OutNumber",SqlDbType.VarChar,50),
            new SqlParameter("@OutAccountType",SqlDbType.Int),
            new SqlParameter("@OutMoney",SqlDbType.Money,40),
            new SqlParameter("@InNumber",SqlDbType.VarChar,50),
            new SqlParameter("@InAccountType",SqlDbType.Int),
            new SqlParameter("@ExpectNum",SqlDbType.Int),
            new SqlParameter("@OperateIP",SqlDbType.VarChar,40),
            new SqlParameter("@OperateNum",SqlDbType.VarChar,50),
            new SqlParameter("@Remark",SqlDbType.VarChar,100),
            new SqlParameter("@Date",SqlDbType.DateTime),
            new SqlParameter("@outtype",SqlDbType.Int),
            new SqlParameter("@intype",SqlDbType.Int),
            new SqlParameter("@returnstr",SqlDbType.Int)
            };
            parameter[0].Value = detailmodel.OutNumber;
            parameter[1].Value = (int)detailmodel.outAccountType;
            parameter[2].Value = detailmodel.OutMoney;
            parameter[3].Value = detailmodel.InNumber;
            parameter[4].Value = (int)detailmodel.inAccountType;
            parameter[5].Value = detailmodel.ExpectNum;
            parameter[6].Value = detailmodel.OperateIP;
            parameter[7].Value = detailmodel.OperateNumber;
            parameter[8].Value = detailmodel.Remark;
            parameter[9].Value = DateTime.Now;
            parameter[10].Value = Outtype;
            parameter[11].Value = Intype;
            parameter[12].Direction = ParameterDirection.Output;
            DBHelper.ExecuteNonQuery(tran, "AddECTransferDetailS", parameter, CommandType.StoredProcedure);
            string rest = parameter[12].Value.ToString();
            return int.Parse(rest);
        }


        /// <summary>
        /// 电子转账(含多种类型转入)Chengkai(12-05-31)--不带事务处理
        /// </summary>
        /// <param name="detailmodel"></param>
        /// <param name="Intype">转入类型--2.为会员现金账户转入 1.为会员消费账户转入 0.为店铺订货款转入</param>
        /// <param name="Outtype">转出类型 -- 1.为会员现金转出 0.为会员消费账户转出</param>
        /// <returns></returns>
        public static int AddMoneyManage(ECTransferDetailModel detailmodel, int Outtype, int Intype)
        {
            SqlParameter[] parameter = new SqlParameter[]{
            new SqlParameter("@OutNumber",SqlDbType.VarChar,40),
            new SqlParameter("@OutAccountType",SqlDbType.Int),
            new SqlParameter("@OutMoney",SqlDbType.Money,40),
            new SqlParameter("@InNumber",SqlDbType.VarChar,40),
            new SqlParameter("@InAccountType",SqlDbType.Int),
            new SqlParameter("@ExpectNum",SqlDbType.Int),
            new SqlParameter("@OperateIP",SqlDbType.VarChar,40),
            new SqlParameter("@OperateNum",SqlDbType.VarChar,40),
            new SqlParameter("@Remark",SqlDbType.VarChar,40),
            new SqlParameter("@Date",SqlDbType.DateTime),
            new SqlParameter("@outtype",SqlDbType.Int),
            new SqlParameter("@intype",SqlDbType.Int),
            new SqlParameter("@returnstr",SqlDbType.Int)
            };
            parameter[0].Value = detailmodel.OutNumber;
            parameter[1].Value = (int)detailmodel.outAccountType;
            parameter[2].Value = detailmodel.OutMoney;
            parameter[3].Value = detailmodel.InNumber;
            parameter[4].Value = (int)detailmodel.inAccountType;
            parameter[5].Value = detailmodel.ExpectNum;
            parameter[6].Value = detailmodel.OperateIP;
            parameter[7].Value = detailmodel.OperateNumber;
            parameter[8].Value = detailmodel.Remark;
            parameter[9].Value = DateTime.Now.ToUniversalTime();
            parameter[10].Value = Outtype;
            parameter[11].Value = Intype;
            parameter[12].Direction = ParameterDirection.Output;
            DBHelper.ExecuteNonQuery("AddECTransferDetailS", parameter, CommandType.StoredProcedure);
            string rest = parameter[12].Value.ToString();
            return int.Parse(rest);
        }


        /// <summary>
        /// 电子转账
        /// </summary>
        /// <param name="detailmodel"></param>
        /// <returns></returns>
        public static int AddMoneyManage(ECTransferDetailModel detailmodel,SqlTransaction tran)
        {
            SqlParameter[] parameter = new SqlParameter[]{
            new SqlParameter("@OutNumber",SqlDbType.VarChar,40),
            new SqlParameter("@OutAccountType",SqlDbType.Int),
            new SqlParameter("@OutMoney",SqlDbType.Money,40),
            new SqlParameter("@InNumber",SqlDbType.VarChar,40),
            new SqlParameter("@InAccountType",SqlDbType.Int),
            new SqlParameter("@ExpectNum",SqlDbType.Int),
            new SqlParameter("@OperateIP",SqlDbType.VarChar,40),
            new SqlParameter("@OperateNum",SqlDbType.VarChar,40),
            new SqlParameter("@Remark",SqlDbType.VarChar,40),
            new SqlParameter("@Date",SqlDbType.DateTime),
            new SqlParameter("@returnstr",SqlDbType.Int)
            };
            parameter[0].Value = detailmodel.OutNumber;
            parameter[1].Value = detailmodel.OutMoney;
            parameter[2].Value = detailmodel.OutMoney;
            parameter[3].Value = detailmodel.InNumber;
            parameter[4].Value = detailmodel.inAccountType;
            parameter[5].Value = detailmodel.ExpectNum;
            parameter[6].Value = detailmodel.OperateIP;
            parameter[7].Value = detailmodel.OperateNumber;
            parameter[8].Value = detailmodel.Remark;
            parameter[9].Value = DateTime.Now.ToUniversalTime();
            parameter[10].Direction = ParameterDirection.Output;
            DBHelper.ExecuteNonQuery(tran,"AddECTransferDetail", parameter, CommandType.StoredProcedure);
            string rest = parameter[10].Value.ToString();
            return int.Parse(rest);
        }

        /// <summary>
        /// 绑定币种
        /// </summary>
        /// <returns></returns>
        public static List<CurrencyModel> GetCurrency()
        {
            List<CurrencyModel> list = new List<CurrencyModel>();
            SqlDataReader read = DBHelper.ExecuteReader("GetAllCurrencyIDName", CommandType.StoredProcedure);
            while (read.Read())
            {
                CurrencyModel model = new CurrencyModel(int.Parse(read["ID"].ToString()));
                model.Name = read["Name"].ToString();
                list.Add(model);
            }
            read.Close();
            return list;
        }

        /// <summary>
        /// 查看备注
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetNote(int id)
        { 
            string sql="select remitterremark from ECRemitDetail where id=@num";
            SqlParameter spa = new SqlParameter("@num",SqlDbType.Int);
            spa.Value = id;
            return DBHelper.ExecuteScalar(sql,spa, CommandType.Text).ToString();
        }

        /// <summary>
        /// 查看备注
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetEFTNote(int id)
        {
            string sql = "select remark from MemberRemittances where id=@num";
            SqlParameter spa = new SqlParameter("@num", SqlDbType.Int);
            spa.Value = id;
            return DBHelper.ExecuteScalar(sql, spa, CommandType.Text).ToString();
        }
        /// <summary>
        /// 转账确认
        /// </summary>
        /// <param name="id">转账ID</param>
        /// <param name="iszhuanchu">是否是转出</param>
        public static void TransferConfirm(int id, int iszhuanchu)
        {
            string sql = "";
            if (iszhuanchu == 0)
            {
                sql = "update MoneyTransfer set OutIsConfirm=1,OutConfirmationTime=@Data where id=@id";
                SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@Data", SqlDbType.DateTime), new SqlParameter("@id", SqlDbType.Int) };
                parm[0].Value = DateTime.Now.ToUniversalTime();
                parm[1].Value = id;
                DBHelper.ExecuteNonQuery(sql, parm, CommandType.Text);
            }
            else
            {
                sql = "update MoneyTransfer set TrunIsConfirm=1,TrunConfirmationTime=@Data where id=@id";
                SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@Data", SqlDbType.DateTime), new SqlParameter("@id", SqlDbType.Int) };
                parm[0].Value = DateTime.Now.ToUniversalTime();
                parm[1].Value = id;
                DBHelper.ExecuteNonQuery(sql, parm, CommandType.Text);
            }
        }

        public static bool TransferConfirm(int id, int iszhuanchu,SqlTransaction tran)
        {
            string sql = "";
            int count = 0;
            if (iszhuanchu == 0)
            {
                sql = "update MoneyTransfer set OutIsConfirm=1,OutConfirmationTime=@Data where id=@id";
                SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@Data", SqlDbType.DateTime), new SqlParameter("@id", SqlDbType.Int) };
                parm[0].Value = DateTime.Now.ToUniversalTime();
                parm[1].Value = id;
                count = (int)DBHelper.ExecuteNonQuery(tran,sql, parm, CommandType.Text);
            }
            else
            {
                sql = "update MoneyTransfer set TrunIsConfirm=1,TrunConfirmationTime=@Data where id=@id";
                SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@Data", SqlDbType.DateTime), new SqlParameter("@id", SqlDbType.Int) };
                parm[0].Value = DateTime.Now.ToUniversalTime();
                parm[1].Value = id;
                count = (int)DBHelper.ExecuteNonQuery(tran,sql, parm, CommandType.Text);
            }
            if (count == 0)
                return false;
            return true;
        }

        /// <summary>
        /// 判断转账是否已经确认
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool isQuren(int id, int iszhuanchu)
        {
            bool blean=false;
            string sql = "";
            if (iszhuanchu == 0)
            {
                sql = "select OutIsConfirm from MoneyTransfer where ID=@ID";
            }
            else
            {
                sql = "select TrunIsConfirm from MoneyTransfer where ID=@ID";
            }
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@ID",SqlDbType.Int)};
            parm[0].Value = id;
            Object obj = DBHelper.ExecuteScalar(sql, parm, CommandType.Text);
            if (obj != DBNull.Value)
            {
                if (obj.ToString() == "0")
                {
                    blean = false;
                }
                else
                    blean = true;
            }
            return blean;
        }
        /// <summary>
        /// 根据编号获得转账信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static MoneyTransferModel GetMoneyTransfer(int id)
        {
            MoneyTransferModel info = new MoneyTransferModel();
            string sql = "select * from MoneyTransfer where ID=@id";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@id",SqlDbType.Int)};
            parm[0].Value = id;
            //SqlDataReader reader = DBHelper.ExecuteReader(sql,parm,CommandType.Text);
            DataTable dt = DBHelper.ExecuteDataTable(sql, parm, CommandType.Text);
            if (dt.Rows.Count>0)
            {
                info.Id = Convert.ToInt32(dt.Rows[0]["ID"]);
                info.Money = Convert.ToDouble(dt.Rows[0]["Money"]);
                info.OutConfirmationTime = Convert.ToDateTime(dt.Rows[0]["OutConfirmationTime"]);
                info.OutIsConfirm = Convert.ToInt32(dt.Rows[0]["OutIsConfirm"]);
                info.OutNumber = dt.Rows[0]["OutNumber"].ToString();
                info.Remark = dt.Rows[0]["Remark"].ToString();
                info.TransferTime = Convert.ToDateTime(dt.Rows[0]["TransferTime"]);
                info.TransferType = Convert.ToInt32(dt.Rows[0]["TransferType"]);
                info.TrunConfirmationTime = Convert.ToDateTime(dt.Rows[0]["TrunConfirmationTime"]);
                info.TrunIsConfirm = Convert.ToInt32(dt.Rows[0]["TrunIsConfirm"]);
                info.TrunNumber = dt.Rows[0]["TrunNumber"].ToString();
            }
            return info;
        }
        public static MoneyTransferModel GetMoneyTransfer(int id,SqlTransaction tran)
        {
            MoneyTransferModel info = new MoneyTransferModel();
            string sql = "select * from MoneyTransfer where ID=@id";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int) };
            parm[0].Value = id;
            //SqlDataReader reader = DBHelper.ExecuteReader(tran, sql, parm, CommandType.Text);
            DataTable dt = DBHelper.ExecuteDataTable(tran,sql, parm, CommandType.Text);
            if (dt.Rows.Count > 0)
            {
                info.Id = Convert.ToInt32(dt.Rows[0]["ID"]);
                info.Money = Convert.ToDouble(dt.Rows[0]["Money"]);
                info.OutConfirmationTime = Convert.ToDateTime(dt.Rows[0]["OutConfirmationTime"]);
                info.OutIsConfirm = Convert.ToInt32(dt.Rows[0]["OutIsConfirm"]);
                info.OutNumber = dt.Rows[0]["OutNumber"].ToString();
                info.Remark = dt.Rows[0]["Remark"].ToString();
                info.TransferTime = Convert.ToDateTime(dt.Rows[0]["TransferTime"]);
                info.TransferType = Convert.ToInt32(dt.Rows[0]["TransferType"]);
                info.TrunConfirmationTime = Convert.ToDateTime(dt.Rows[0]["TrunConfirmationTime"]);
                info.TrunIsConfirm = Convert.ToInt32(dt.Rows[0]["TrunIsConfirm"]);
                info.TrunNumber = dt.Rows[0]["TrunNumber"].ToString();
            }
            return info;
        }
        /// <summary>
        /// 判断转账记录是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool isExistsTransfer(int id)
        {
            string sql = "select count(*) from MoneyTransfer where ID=@id";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@id",SqlDbType.Int)};
            parm[0].Value = id;
            if (Convert.ToInt32(DBHelper.ExecuteScalar(sql, parm, CommandType.Text)) > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 删除转账信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DelTransfer(int id,MoneyTransferModel info)
        {
            if (info.TransferType == 1 && info.TrunIsConfirm == 0)
            {

                string sql = "update MemberInfo set TotalDefray=TotalDefray-@TotalDefray where Number=@Number";
                SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@TotalDefray", SqlDbType.Money), new SqlParameter("@Number", SqlDbType.NVarChar, 50) };
                parm[0].Value = info.Money;
                parm[1].Value = info.OutNumber;
                int num = DBHelper.ExecuteNonQuery(sql, parm, CommandType.Text);
            }
            if(info.TransferType==2&&info.TrunIsConfirm==0)
            {
                string sql = "update MemberInfo set Out=Out-@Out where Number=@Number";
                SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@Out", SqlDbType.Money), new SqlParameter("@Number", SqlDbType.NVarChar, 50) };
                parm[0].Value = info.Money;
                parm[1].Value = info.OutNumber;
                int num = DBHelper.ExecuteNonQuery(sql, parm, CommandType.Text);
            }
            string sql1 = "delete MoneyTransfer where id=@id";
            SqlParameter[] parm1 = new SqlParameter[] { new SqlParameter("@id",SqlDbType.Int)};
            parm1[0].Value = id;
            return DBHelper.ExecuteNonQuery(sql1,parm1,CommandType.Text);
        }
        /// <summary>
        /// 查看会员的现金账户和报单账户
        /// </summary>
        /// <param name="number">会员编号</param>
        /// <param name="Cash">现金账户</param>
        /// <param name="Declarations">报单账户</param>
        public static void DelTransfer(string number, out double Cash, out double Declarations)
        {
            Cash = 0;
            Declarations = 0;
            string sql = "select (Jackpot-membership-Out) as Cash,(TotalRemittances-TotalDefray) as Declarations from memberinfo where number=@number";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@number",SqlDbType.NVarChar,50)};
            parm[0].Value = number;
            SqlDataReader reader = DBHelper.ExecuteReader(sql,parm,CommandType.Text);
            if (reader.Read())
            {
                Cash = Convert.ToDouble(reader["Cash"]);
                Declarations = Convert.ToDouble(reader["Declarations"]);
            }
            reader.Close();
        }
    }
}
