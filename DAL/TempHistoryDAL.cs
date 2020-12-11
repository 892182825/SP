using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace DAL
{
    /// <summary>
    /// 调网数据访问层
    /// </summary>
    public class TempHistoryDAL
    {

        /// <summary>
        /// 添加数据进入调网记录临时表
        /// </summary>
        /// <param name="col">调整类型</param>
        /// <param name="number">会员编号</param>
        /// <param name="oldPlace">原关系上级</param>
        /// <param name="leader">新关系上级</param>
        /// <param name="qishu">调整期数</param>
        /// <param name="operateBH"></param>
        /// <returns></returns>
        public static int AddTempHistory(SqlTransaction tran, int col, string number, string oldPlace, string leader, int qishu, string operateBH, DateTime adjustdate)
        {
            string sql = "INSERT INTO NetAdjustmentHistory  (Type,Number,Original ,NewLocation,ExpectNum,Error,OperateNum,adjustdate) VALUES(  @twType, @srcBh, @Yweizhi, @dirBh, @twQishu,'',@opreateBH,@adjustdate)";
            SqlParameter[] para ={
									 new SqlParameter ("@twType" , SqlDbType.Int  ),
									 new SqlParameter ("@srcBh" , SqlDbType.VarChar ,20  ),
									 new SqlParameter ("@Yweizhi" , SqlDbType.VarChar ,20  ),
									 new SqlParameter ("@dirBh" , SqlDbType.VarChar ,20  ),
									 new SqlParameter ("@twQishu" , SqlDbType.Int  ), 
									 new SqlParameter ("@opreateBH" , SqlDbType.VarChar,20  ),
                                     new SqlParameter("@adjustdate",SqlDbType.DateTime)
                                 };
            para[0].Value = col;
            para[1].Value = number;
            para[2].Value = oldPlace;
            para[3].Value = leader;
            para[4].Value = qishu;
            para[5].Value = operateBH;
            para[6].Value = adjustdate;
            return DBHelper.ExecuteNonQuery(tran, sql, para, CommandType.Text);
        }

        /// <summary>
        /// 调网业绩-位置处理
        /// </summary>
        /// <param name="number">调网人</param>
        /// <param name="placement">安置编号</param>
        /// <param name="direct">推荐编号</param>
        /// <param name="tran">事务</param>
        public static void ExecuteUpdateNew(string number, string placement, string direct, SqlTransaction tran, int maxExpect, int flag)
        {
            //@xgfenshu money,	--新总积分(修改后)
            //@oldfenshu money,	--老总积分(修改前)
            //@flag bit
            //new MemberInfoDAL().getMemberInfo(number);
            DataTable dt = MemberInfoDAL.GetRecordDataTable(number, maxExpect);
            SqlParameter[] parm ={new SqlParameter("@number",SqlDbType.VarChar,30),
                                                    new SqlParameter("@Placement",SqlDbType.VarChar,30),
                                                    new SqlParameter("@Direct",SqlDbType.VarChar,30),
                                                    new SqlParameter("@CurrentOneMark",SqlDbType.Money),
                                                    new SqlParameter("@oldfenshu",SqlDbType.Money),
                                                    new SqlParameter("@flag",SqlDbType.Int),
                                                };

            parm[0].Value = number;
            parm[1].Value = placement.Trim();
            parm[2].Value = direct.Trim();
            parm[3].Value = dt.Rows[0][0];
            parm[4].Value = dt.Rows[0][1];
            parm[5].Value = flag;

            DBHelper.ExecuteNonQuery(tran, "js_updatenew", parm, CommandType.StoredProcedure);


        }

        /// <summary>
        /// 调网
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="number">编号</param>
        /// <param name="old">原位置</param>
        /// <param name="newbh">新位置</param>
        /// <param name="isAzTj">0：推荐；1：安置</param>
        /// <param name="qishu">期数</param>
        public static int ExecuteUpdateNet(string number, string old, string newbh, int isAzTj, int qishu, int newqushu, string opreateBH, DateTime adjustdate)
        {

            SqlParameter[] parm2 ={new SqlParameter("@Number",SqlDbType.VarChar,30),
													 new SqlParameter("@old",SqlDbType.VarChar,30),
													 new SqlParameter("@new",SqlDbType.VarChar,30),
													 new SqlParameter("@IsAz",SqlDbType.Bit,2),
													 new SqlParameter("@qishu",SqlDbType.Int),
													 new SqlParameter("@newqushu",SqlDbType.Int),
                                                     new SqlParameter("@opreateBH",SqlDbType.VarChar,50),
                                                      new SqlParameter("@adjustdate",SqlDbType.DateTime)
												 };

            parm2[0].Value = number;
            parm2[1].Value = old;
            parm2[2].Value = newbh;
            parm2[3].Value = isAzTj;
            parm2[4].Value = qishu;
            parm2[5].Value = newqushu;
            parm2[6].Value = opreateBH;
            parm2[7].Value = adjustdate;

            return DBHelper.ExecuteDataTable("js_UpdateNet_w", parm2, CommandType.StoredProcedure).Rows.Count;

        }
        /// <summary>
        /// 调网
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="number">编号</param>
        /// <param name="old">原位置</param>
        /// <param name="newbh">新位置</param>
        /// <param name="isAzTj">0：推荐；1：安置</param>
        /// <param name="qishu">期数</param>
        public static int ExecuteUpdateNet(string number, string old, string newbh, int isAzTj, int qishu, string opreateBH, DateTime adjustdate)
        {
            int newqushu = 1;
            if (isAzTj == 1)
                newqushu = AddOrderDataDAL.GetDistrict(newbh, 1);

            SqlParameter[] parm2 ={new SqlParameter("@Number",SqlDbType.VarChar,30),
													 new SqlParameter("@old",SqlDbType.VarChar,30),
													 new SqlParameter("@new",SqlDbType.VarChar,30),
													 new SqlParameter("@IsAz",SqlDbType.Bit,2),
													 new SqlParameter("@qishu",SqlDbType.Int),
													 new SqlParameter("@newqushu",SqlDbType.Int),
                                                     new SqlParameter("@opreateBH",SqlDbType.VarChar,50),
                                                      new SqlParameter("@adjustdate",SqlDbType.DateTime)
												 };

            parm2[0].Value = number;
            parm2[1].Value = old;
            parm2[2].Value = newbh;
            parm2[3].Value = isAzTj;
            parm2[4].Value = qishu;
            parm2[5].Value = newqushu;
            parm2[6].Value = opreateBH;
            parm2[7].Value = adjustdate;

            return DBHelper.ExecuteDataTable("js_UpdateNet_w", parm2, CommandType.StoredProcedure).Rows.Count;

        }


        /// <summary>
        /// 修改推荐安置-位置处理
        /// </summary>
        /// <param name="number">修改人编号</param>
        /// <param name="placement">安置编号</param>
        /// <param name="direct">推荐编号</param>
        /// <param name="tran">事务</param>
        public static void UpdateNet(string number, string placement, string direct, SqlTransaction tran, int maxExpect, int flag)
        {
            //@xgfenshu money,	--新总积分(修改后)
            //@oldfenshu money,	--老总积分(修改前)
            //@flag bit
            //new MemberInfoDAL().getMemberInfo(number);
            DataTable dt = MemberInfoDAL.GetRecordDataTable(number, maxExpect);
            SqlParameter[] parm ={new SqlParameter("@number",SqlDbType.VarChar,30),
                                                    new SqlParameter("@Placement",SqlDbType.VarChar,30),
                                                    new SqlParameter("@Direct",SqlDbType.VarChar,30),
                                                    new SqlParameter("@CurrentOneMark",SqlDbType.Money),
                                                    new SqlParameter("@oldfenshu",SqlDbType.Money),
                                                    new SqlParameter("@flag",SqlDbType.Int),
                                                };

            parm[0].Value = number;
            parm[1].Value = placement.Trim();
            parm[2].Value = direct.Trim();
            parm[3].Value = dt.Rows[0][0];
            parm[4].Value = dt.Rows[0][1];
            parm[5].Value = flag;

            DBHelper.ExecuteNonQuery(tran, "js_updateNet", parm, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 修改推荐安置编号
        /// </summary>
        /// <param name="number">修改人编号</param>
        /// <param name="placement">安置编号</param>
        /// <param name="direct">推荐编号</param>
        /// <param name="tran">事务</param>
        public static int UpdateMemberinfo(string number, string placement, string direct, SqlTransaction tran)
        {
            //@xgfenshu money,	--新总积分(修改后)
            //@oldfenshu money,	--老总积分(修改前)
            //@flag bit
            //new MemberInfoDAL().getMemberInfo(number);
            string strSql = "Update memberinfo set placement=@Placement,direct=@Direct where number=@number";
            SqlParameter[] parm ={
                                     new SqlParameter("@number",SqlDbType.VarChar,30),
                                     new SqlParameter("@Placement",SqlDbType.VarChar,30),
                                     new SqlParameter("@Direct",SqlDbType.VarChar,30)
                                 };

            parm[0].Value = number;
            parm[1].Value = placement.Trim();
            parm[2].Value = direct.Trim();

            int count = (int)DBHelper.ExecuteNonQuery(tran, strSql, parm, CommandType.Text);
            return count;
        }

        /// <summary>
        /// 修改config表重新结算字段
        /// </summary>
        /// <param name="tran">事务</param>
        public static int UpdateConfig(int maxExpectNum, SqlTransaction tran)
        {
            //@xgfenshu money,	--新总积分(修改后)
            //@oldfenshu money,	--老总积分(修改前)
            //@flag bit
            //new MemberInfoDAL().getMemberInfo(number);
            string strSql = "Update config set jsflag=0 where ExpectNum>=@ExpectNum";
            SqlParameter[] parm ={
                                     new SqlParameter("@ExpectNum",SqlDbType.Int)                                 };

            parm[0].Value = maxExpectNum;

            int count = (int)DBHelper.ExecuteNonQuery(tran, strSql, parm, CommandType.Text);
            return count;
        }


        public static int GetFlag(string number)
        {
            string sql = "select flag from memberInfo where number = @number";
            SqlParameter[] par = new SqlParameter[] { new SqlParameter("@Number", SqlDbType.VarChar) };
            par[0].Value = number;
            return int.Parse(DBHelper.ExecuteScalar(sql, par, CommandType.Text).ToString());

        }

        /// <summary>
        /// 调网检错
        /// </summary>
        /// <param name="number">调网人编号</param>
        /// <param name="placement">新安置</param>
        /// <param name="direct">新推荐</param>
        /// <param name="oldplacement">原安置</param>
        /// <param name="olddirect">原推荐</param>
        /// <param name="tran">事务</param>
        /// <param name="xs">线数</param>
        /// <param name="info">失败时返回错误信息，反之返回'OK'</param>
        /// <returns></returns>
        public static string ChangeCheck(string number, string placement, string direct, string oldplacement, string olddirect, int newqushu, int xs, string info)
        {
            SqlParameter[] parm ={new SqlParameter("@bianhao",SqlDbType.VarChar,30),
                                             new SqlParameter("@srcAnZhi",SqlDbType.VarChar,30),
                                             new SqlParameter("@dirAnZhi",SqlDbType.VarChar,30),
                                             new SqlParameter("@srcTuiJian",SqlDbType.VarChar,30),
                                             new SqlParameter("@dirTuiJian",SqlDbType.VarChar,30),
                                             new SqlParameter("@qushu",SqlDbType.Int),
                                             new SqlParameter("@azXianShu",SqlDbType.Int),
                                             new SqlParameter("@info",SqlDbType.VarChar,200)
                                         };

            parm[0].Value = number;
            parm[1].Value = oldplacement;
            parm[2].Value = placement;
            parm[3].Value = olddirect;
            parm[4].Value = direct;
            parm[5].Value = newqushu;
            parm[6].Value = xs;
            parm[7].Value = info;
            parm[7].Direction = System.Data.ParameterDirection.Output;

            DBHelper.ExecuteNonQuery("js_twCheck", parm, CommandType.StoredProcedure);

            info = parm[7].Value.ToString();
            return info;
        }

        /// <summary>
        /// 调网检错
        /// </summary>
        /// <param name="number">调网人编号</param>
        /// <param name="placement">新安置</param>
        /// <param name="direct">新推荐</param>
        /// <param name="oldplacement">原安置</param>
        /// <param name="olddirect">原推荐</param>
        /// <param name="tran">事务</param>
        /// <param name="xs">线数</param>
        /// <param name="info">失败时返回错误信息，反之返回'OK'</param>
        /// <returns></returns>
        public static string ChangeCheck(string number, string placement, string direct, string oldplacement, string olddirect, int qushu, SqlTransaction tran, int xs, string info)
        {
            SqlParameter[] parm ={new SqlParameter("@bianhao",SqlDbType.VarChar,30),
                                             new SqlParameter("@srcAnZhi",SqlDbType.VarChar,30),
                                             new SqlParameter("@dirAnZhi",SqlDbType.VarChar,30),
                                             new SqlParameter("@srcTuiJian",SqlDbType.VarChar,30),
                                             new SqlParameter("@dirTuiJian",SqlDbType.VarChar,30),
                                             new SqlParameter("@qushu",SqlDbType.Int, 4),
                                             new SqlParameter("@azXianShu",SqlDbType.Int),
                                             new SqlParameter("@info",SqlDbType.VarChar,200)
                                         };

            parm[0].Value = number;
            parm[1].Value = oldplacement;
            parm[2].Value = placement;
            parm[3].Value = olddirect;
            parm[4].Value = direct;
            parm[5].Value = qushu;
            parm[6].Value = xs;
            parm[7].Value = info;
            parm[7].Direction = System.Data.ParameterDirection.Output;

            DBHelper.ExecuteNonQuery(tran, "js_twCheck", parm, CommandType.StoredProcedure);

            info = parm[7].Value.ToString();
            return info;
        }

        /// <summary>
        /// 获取网络信息
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static DataTable GetNetMessage(string number)
        {
            string strSql = "select top 1 placement,direct,storeid,district from memberinfo where number=@number";

            SqlParameter[] sps = new SqlParameter[] { 
             new SqlParameter("@number",number)
            };
            DataTable dt = DBHelper.ExecuteDataTable(strSql, sps, CommandType.Text);
            return dt;
        }

    }
}