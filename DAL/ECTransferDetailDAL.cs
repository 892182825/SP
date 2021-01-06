using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    /*
     * 会员电子转账明细
     * **/
    public class ECTransferDetailDAL
    {

        public static bool Withdraw(SqlTransaction tran, WithdrawModel dModel)
        {
            string strSql = @"insert into Withdraw(number,applicationExpectNum,auditExpectNum,isAuditing,WithdrawMoney,WithdrawTime,OperateIP,remark,bankcard,bankname,WithdrawSXF,Khname,IsJL,Wyj,blmoney,Wyjbl ,InvestJB,priceJB,DrawCardtype,AliNo,WeiXNo,InvestJBSXF,InvestJBWYJ ,actype)
                                values(@Number,@ApplicationExpectNum,@AuditingExpectNum,@IsAuditing,@WithdrawMoney,@WithdrawTime,@OperateIP,@Remark,@bankcard,@bankname,@WithdrawSXF,@Khname,@IsJL,@Wyj,@blmoney,@Wyjbl,@InvestJB,@priceJB,@DrawCardtype,@AliNo,@WeiXNo,@InvestJBSXF,@InvestJBWYJ ,@actype)";
            SqlParameter[] para = {
                                      new SqlParameter("@Number",SqlDbType.NVarChar,50),
                                      new SqlParameter("@ApplicationExpectNum",SqlDbType.Int),
                                      new SqlParameter("@AuditingExpectNum",SqlDbType.Int),
                                      new SqlParameter("@IsAuditing",SqlDbType.Int),
                                      new SqlParameter("@WithdrawMoney",SqlDbType.Money),
                                      new SqlParameter("@WithdrawTime",SqlDbType.DateTime,8),
                                      new SqlParameter("@OperateIP",SqlDbType.NVarChar,30),
                                      new SqlParameter("@Remark",SqlDbType.NVarChar,1000),
                                      new SqlParameter("@bankcard",SqlDbType.NVarChar,50),
                                      new SqlParameter("@bankname",SqlDbType.NVarChar,50),
                                      new SqlParameter("@WithdrawSXF",SqlDbType.Money),
                                      new SqlParameter("@Khname",SqlDbType.NVarChar,50),
                                      new SqlParameter("@IsJL",SqlDbType.Int),
                                      new SqlParameter("@Wyj",SqlDbType.Money),
                                      new SqlParameter("@blmoney",SqlDbType.Float),
                                      new SqlParameter("@Wyjbl",SqlDbType.Float),

 new SqlParameter("@InvestJB",dModel.InvestJB),
 new SqlParameter("@priceJB",dModel.PriceJB),
 new SqlParameter("@DrawCardtype",dModel.DrawCardtype),
 new SqlParameter("@AliNo",dModel.AliNo),
 new SqlParameter("@WeiXNo",dModel.WeiXNo),
 new SqlParameter("@InvestJBSXF",dModel.InvestJBSXF),
 new SqlParameter("@InvestJBWYJ",dModel.InvestJBWYJ) ,
  new SqlParameter("@actype",dModel.Actype)
                                  };
            para[0].Value = dModel.Number;
            para[1].Value = dModel.ApplicationExpecdtNum;
            para[2].Value = 0;
            para[3].Value = 0;
            para[4].Value = dModel.WithdrawMoney;
            para[5].Value = dModel.WithdrawTime;
            para[6].Value = dModel.OperateIP;
            para[7].Value = dModel.Remark;
            para[8].Value = dModel.Bankcard;
            para[9].Value = dModel.Bankname;
            para[10].Value = dModel.WithdrawSXF;
            para[11].Value = dModel.Khname;
            para[12].Value = dModel.IsJL;
            para[13].Value = dModel.Wyj;
            para[14].Value = dModel.blmoney;
            para[15].Value = dModel.Wyjbl;
            para[16].Value = dModel.Actype;

            int count = (int)DBHelper.ExecuteNonQuery(tran, strSql, para, CommandType.Text);
            if (count <= 0)
                return false;
            return true;
        }

        public static bool SetMemberShip(SqlTransaction tran, string number, double money, decimal WithdrawSXF)
        {
            string strSql = "Update MemberInfo Set MemberShip=MemberShip+@Money where number=@Number";
            SqlParameter[] para = {
                                      new SqlParameter("@Money",SqlDbType.Money),
                                      new SqlParameter("@Number",SqlDbType.NVarChar,30),
                                       
                                  };
            para[0].Value = money;
            para[1].Value = number;
            

            int count = (int)DBHelper.ExecuteNonQuery(tran, strSql, para, CommandType.Text);
            if (count <= 0)
                return false;
            return true;
        }

        public static bool SetMemberShip1(SqlTransaction tran, string number, double money, decimal WithdrawSXF,decimal Wyj)
        {
            string strSql = "Update MemberInfo Set MemberShip=MemberShip+@Money+@WithdrawSXF+@Wyj where number=@Number";
            SqlParameter[] para = {
                                      new SqlParameter("@Money",SqlDbType.Money),
                                      new SqlParameter("@Number",SqlDbType.NVarChar,30),
                                      new SqlParameter("@WithdrawSXF",SqlDbType.Money),
                                      new SqlParameter("@Wyj",SqlDbType.Money)
                                  };
            para[0].Value = money;
            para[1].Value = number;
            para[2].Value = WithdrawSXF;
            para[3].Value = Wyj;

            int count = (int)DBHelper.ExecuteNonQuery(tran, strSql, para, CommandType.Text);
            if (count <= 0)
                return false;
            return true;
        }
        /// <summary>
        /// 以石斛积分冻结
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="wDraw"></param>
        /// <returns></returns>
        public static bool SetMemberShip1JB(SqlTransaction tran, WithdrawModel wDraw)
        {
            string strSql = "Update MemberInfo Set pointAFz=pointAFz+@Money,pointEFz=pointEFz+@sxf where number=@Number";
            SqlParameter[] para = {
                                      new SqlParameter("@Money",SqlDbType.Money),
                                      new SqlParameter("@sxf",SqlDbType.Money),
                                      new SqlParameter("@Number",SqlDbType.NVarChar,50),
                                      
                                  };
            para[0].Value = wDraw.WithdrawMoney;
            para[1].Value =  wDraw.WithdrawSXF;
            para[2].Value = wDraw.Number;


            int count = (int)DBHelper.ExecuteNonQuery(tran, strSql, para, CommandType.Text);
            if (count <= 0)
                return false;
            return true;
        }

        /// <summary>
        /// 修改审核状态、时间——ds2012——tianfeng
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="dModel"></param>
        /// <returns></returns>
        public static bool SetWithdrawState(SqlTransaction tran, WithdrawModel dModel)
        {
            string strSql = "Update withdraw set isAuditing=2,auditExpectNum=@ExpectNum,AuditingTime=@auditTime,AuditingmanageId=@AuditingManageId,AuditingIp=@AuditingIp where id=@id";
            SqlParameter[] para = {
                                      new SqlParameter("@ExpectNum",SqlDbType.Int),
                                      new SqlParameter("@auditTime",SqlDbType.DateTime),
                                      new SqlParameter("@AuditingManageId",SqlDbType.NVarChar,30),
                                      new SqlParameter("@AuditingIp",SqlDbType.NVarChar,50),
                                      new SqlParameter("@id",SqlDbType.Int)
                                  };
            para[0].Value = dModel.AuditExpectNum;
            para[1].Value = dModel.AuditTime;
            para[2].Value = dModel.AuditingManageId;
            para[3].Value = dModel.AuditingIP;
            para[4].Value = dModel.Id;

            int count = (int)DBHelper.ExecuteNonQuery(tran, strSql, para, CommandType.Text);
            if (count == 0)
                return false;
            return true;

        }

        /// <summary>
        /// 是否已删除提现申请——ds2012——tianfeng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool isDelWithdraw(int id)
        {
            string strSql = "Select Count(1) from withdraw where id=@id";
            SqlParameter[] para = {
                                      new SqlParameter("@id",id)
                                  };
            int count = (int)DBHelper.ExecuteScalar(strSql, para, CommandType.Text);
            if (count == 0)
                return false;
            return true;
        }

        /// <summary>
        /// 是否已审核提现申请——ds2012——tianfeng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int GetAuditState(int id)
        {
            string strSql = "Select isAuditing from withdraw where id=@id";
            SqlParameter[] para = {
                                      new SqlParameter("@id",id)
                                  };
            int count = (int)DBHelper.ExecuteScalar(strSql, para, CommandType.Text);
            return count;
        }
        /// <summary>
        /// 更新提现账户——ds2012——tianfeng
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="number"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        public static bool SetMemberOut(SqlTransaction tran, string number, double money,double wyj)
        {
            string strSql = "";
            if (wyj == 0)
            {
                strSql = "Update MemberInfo Set Out = Out + @Money,MemberShip = MemberShip - @Money where number=@Number";
                

           
            SqlParameter[] para = {
                                      new SqlParameter("@Money",SqlDbType.Money),
                                      new SqlParameter("@Number",SqlDbType.NVarChar,50),
                                     
                                  };
            para[0].Value = money;
            para[1].Value = number;
            int count = (int)DBHelper.ExecuteNonQuery(tran, strSql, para, CommandType.Text);
            if (count <= 0)
                return false;

            }
            else
            {

                return true;
            }
            return true;
        }
        /// <summary>
        /// 删除提现申请——ds2012——tianfeng
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="id"></param>
        /// <param name="money"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool DeleteWithdraw(SqlTransaction tran, int id, double money, string number)
        {
            string strSql = "Delete from Withdraw Where id=@Id";
            SqlParameter[] para = {
                                      new SqlParameter("@Id",id)
                                  };
            int count = 0;
            count = (int)DBHelper.ExecuteNonQuery(tran, strSql, para, CommandType.Text);
            if (count == 0)
                return false;

            strSql = "Update MemberInfo Set MemberShip = MemberShip - @Money Where number=@Number";
            SqlParameter[] para1 = {
                                       new SqlParameter("@Money",SqlDbType.Money),
                                       new SqlParameter("@Number",SqlDbType.NVarChar,30)
                                   };
            para1[0].Value = money;
            para1[1].Value = number;
            count = (int)DBHelper.ExecuteNonQuery(tran, strSql, para1, CommandType.Text);
            if (count == 0)
                return false;
            return true;
        }



        public static bool DeleteWithdraw1(SqlTransaction tran, int id, double money,double sxf, string number)
        {
            string strSql = "Delete from Withdraw Where id=@Id";
            SqlParameter[] para = {
                                      new SqlParameter("@Id",id)
                                  };
            int count = 0;
            count = (int)DBHelper.ExecuteNonQuery(tran, strSql, para, CommandType.Text);
            if (count == 0)
                return false;

            strSql = "Update MemberInfo Set MemberShip = MemberShip - @Money-@Sxf Where number=@Number";
            SqlParameter[] para1 = {
                                       new SqlParameter("@Money",SqlDbType.Money),
                                       new SqlParameter("@Number",SqlDbType.NVarChar,30),
                                         new SqlParameter("@Sxf",SqlDbType.NVarChar,30)

                                   };
            para1[0].Value = money;
            para1[1].Value = number;
            para1[2].Value = sxf;
            count = (int)DBHelper.ExecuteNonQuery(tran, strSql, para1, CommandType.Text);
            if (count == 0)
                return false;
            return true;
        }


        /// <summary>
        /// 提现申请账号错误——ds2012——tianfeng
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="id"></param>
        /// <param name="money"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool updateCardEorror(int id, double money, string number)
        {
            SqlTransaction tran = null;
            SqlConnection con = DBHelper.SqlCon();
            try
            {
                con.Open();
                tran = con.BeginTransaction();
                string strSql = "update Withdraw set isAuditing=3,AuditingTime='" + DateTime.Now.ToUniversalTime() + "'  Where id=@Id";
                SqlParameter[] para = {
                                      new SqlParameter("@Id",id)
                                  };
                int count = 0;
                count = (int)DBHelper.ExecuteNonQuery(tran, strSql, para, CommandType.Text);
                if (count == 0)
                    return false;

                strSql = "Update MemberInfo Set MemberShip = MemberShip - @Money Where number=@Number";
                SqlParameter[] para1 = {
                                       new SqlParameter("@Money",SqlDbType.Money),
                                       new SqlParameter("@Number",SqlDbType.NVarChar,50)
                                   };
                para1[0].Value = money;
                para1[1].Value = number;
                count = (int)DBHelper.ExecuteNonQuery(tran, strSql, para1, CommandType.Text);
                if (count == 0)
                {
                    tran.Rollback();
                    return false;
                }
                tran.Commit();
                return true;
            }
            catch
            {
                tran.Rollback();
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        /// <summary>
        /// 查询提现信息 ds2012——tianfeng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataTable QueryWithdraw(string id)
        {
            string sql = "select * from Withdraw where number=@id and isAuditing<2";
            SqlParameter[] par=new SqlParameter[]{
                new SqlParameter("@id",id)
            };
            return DBHelper.ExecuteDataTable(sql,par,CommandType.Text);

        }
        /// <summary>
        /// 查询提现信息(带事务) ds2012——tianfeng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataTable QueryWithdraw(SqlTransaction tran, string id)
        {
            string sql = "select * from Withdraw where id=@id";
            SqlParameter[] par = new SqlParameter[]{
                new SqlParameter("@id",id)
            };
            return DBHelper.ExecuteDataTable(sql, par, CommandType.Text);

        }

        /// <summary>
        /// 会员修改提现申请
        /// </summary>
        /// <param name="w"></param>
        /// <returns></returns>
        public static bool updateWithdraw(SqlTransaction tran, WithdrawModel w)
        {
            string sql = "update withdraw set AuditExpectNum=0,AuditingTime='1900-1-1 0:00:00',ApplicationExpectNum=@ApplicationExpectNum,WithdrawMoney=@WithdrawMoney,WithdrawTime=@WithdrawTime,OperateIP=@OperateIP,remark=@remark,bankcard=@bankcard,bankname=@bankname where id=@id";
            SqlParameter[] par = new SqlParameter[]{
                new SqlParameter("@ApplicationExpectNum",w.ApplicationExpecdtNum),
                new SqlParameter("@WithdrawMoney",w.WithdrawMoney),
                new SqlParameter("@WithdrawTime",w.WithdrawTime),
                new SqlParameter("@OperateIP",w.OperateIP),
                new SqlParameter("@remark",w.Remark),
                new SqlParameter("@bankcard",w.Bankcard),
                new SqlParameter("@bankname",w.Bankname),
                new SqlParameter("@id",w.Id)
            };
            int num = DBHelper.ExecuteNonQuery(tran,sql, par, CommandType.Text);
            if (num > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool updatemember(SqlTransaction tran, MemberInfoModel m)
        {
            string sql = "update memberInfo set membership=membership+@membership where Number=@Number";
            SqlParameter[] par = new SqlParameter[]{
                  new SqlParameter("@membership",SqlDbType.Money),
                  new SqlParameter("@Number",SqlDbType.NVarChar,30)
            };
            par[0].Value = m.Memberships;
            par[1].Value = m.Number;
            int num = DBHelper.ExecuteNonQuery(tran, sql, par, CommandType.Text);
            if (num > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 开始处理
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool updateKscl(int id)
        {
            string sql = "update withdraw set isAuditing=1 where id=@id";
            SqlParameter[] par = new SqlParameter[]{
                new SqlParameter("@id",id)
            };
            int num = DBHelper.ExecuteNonQuery(sql, par, CommandType.Text);
            if (num > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool XF(SqlTransaction tran, WithdrawModel dModel)
        {
            string strSql = @"insert into MemberCashXF(Number,XFTime,ipon,XFON,XFEN,ps,XFState)
                                values(@Number,@XFTime,@ipon,@XFON,@XFEN,@ps,@XFState)";
            SqlParameter[] para = {

 new SqlParameter("@Number",dModel.Number),
 new SqlParameter("@XFTime",dModel.WithdrawTime),
 new SqlParameter("@ipon",dModel.OperateIP),
 new SqlParameter("@XFON",dModel.WithdrawMoney),
 new SqlParameter("@XFEN",dModel.WithdrawSXF),
 new SqlParameter("@ps",dModel.Remark),
 new SqlParameter("@XFState",dModel.ApplicationExpecdtNum)
                                  };


            int count = (int)DBHelper.ExecuteNonQuery(tran, strSql, para, CommandType.Text);
            if (count <= 0)
                return false;
            return true;
        }

    }
}
