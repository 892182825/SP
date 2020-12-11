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
    /// �������ݷ��ʲ�
    /// </summary>
    public class TempHistoryDAL
    {

        /// <summary>
        /// ������ݽ��������¼��ʱ��
        /// </summary>
        /// <param name="col">��������</param>
        /// <param name="number">��Ա���</param>
        /// <param name="oldPlace">ԭ��ϵ�ϼ�</param>
        /// <param name="leader">�¹�ϵ�ϼ�</param>
        /// <param name="qishu">��������</param>
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
        /// ����ҵ��-λ�ô���
        /// </summary>
        /// <param name="number">������</param>
        /// <param name="placement">���ñ��</param>
        /// <param name="direct">�Ƽ����</param>
        /// <param name="tran">����</param>
        public static void ExecuteUpdateNew(string number, string placement, string direct, SqlTransaction tran, int maxExpect, int flag)
        {
            //@xgfenshu money,	--���ܻ���(�޸ĺ�)
            //@oldfenshu money,	--���ܻ���(�޸�ǰ)
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
        /// ����
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="number">���</param>
        /// <param name="old">ԭλ��</param>
        /// <param name="newbh">��λ��</param>
        /// <param name="isAzTj">0���Ƽ���1������</param>
        /// <param name="qishu">����</param>
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
        /// ����
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="number">���</param>
        /// <param name="old">ԭλ��</param>
        /// <param name="newbh">��λ��</param>
        /// <param name="isAzTj">0���Ƽ���1������</param>
        /// <param name="qishu">����</param>
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
        /// �޸��Ƽ�����-λ�ô���
        /// </summary>
        /// <param name="number">�޸��˱��</param>
        /// <param name="placement">���ñ��</param>
        /// <param name="direct">�Ƽ����</param>
        /// <param name="tran">����</param>
        public static void UpdateNet(string number, string placement, string direct, SqlTransaction tran, int maxExpect, int flag)
        {
            //@xgfenshu money,	--���ܻ���(�޸ĺ�)
            //@oldfenshu money,	--���ܻ���(�޸�ǰ)
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
        /// �޸��Ƽ����ñ��
        /// </summary>
        /// <param name="number">�޸��˱��</param>
        /// <param name="placement">���ñ��</param>
        /// <param name="direct">�Ƽ����</param>
        /// <param name="tran">����</param>
        public static int UpdateMemberinfo(string number, string placement, string direct, SqlTransaction tran)
        {
            //@xgfenshu money,	--���ܻ���(�޸ĺ�)
            //@oldfenshu money,	--���ܻ���(�޸�ǰ)
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
        /// �޸�config�����½����ֶ�
        /// </summary>
        /// <param name="tran">����</param>
        public static int UpdateConfig(int maxExpectNum, SqlTransaction tran)
        {
            //@xgfenshu money,	--���ܻ���(�޸ĺ�)
            //@oldfenshu money,	--���ܻ���(�޸�ǰ)
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
        /// �������
        /// </summary>
        /// <param name="number">�����˱��</param>
        /// <param name="placement">�°���</param>
        /// <param name="direct">���Ƽ�</param>
        /// <param name="oldplacement">ԭ����</param>
        /// <param name="olddirect">ԭ�Ƽ�</param>
        /// <param name="tran">����</param>
        /// <param name="xs">����</param>
        /// <param name="info">ʧ��ʱ���ش�����Ϣ����֮����'OK'</param>
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
        /// �������
        /// </summary>
        /// <param name="number">�����˱��</param>
        /// <param name="placement">�°���</param>
        /// <param name="direct">���Ƽ�</param>
        /// <param name="oldplacement">ԭ����</param>
        /// <param name="olddirect">ԭ�Ƽ�</param>
        /// <param name="tran">����</param>
        /// <param name="xs">����</param>
        /// <param name="info">ʧ��ʱ���ش�����Ϣ����֮����'OK'</param>
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
        /// ��ȡ������Ϣ
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