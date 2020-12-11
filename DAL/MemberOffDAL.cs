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

namespace DAL
{
    public class MemberOffDAL
    {
        public static string getMemberName(string number)
        {
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@number", number);

            string num = DBHelper.ExecuteScalar("select top 1 name from memberOff where number=@number order by zxdate desc", para, CommandType.Text).ToString();

            return num;
        }
        public static string getMemberNameSP(string number)
        {
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@number", number);

            string num = DBHelper.ExecuteScalar("select top 1 name from memberOffSP where number=@number order by id desc", para, CommandType.Text).ToString();

            return num;
        }

        public static int getMemberSPISzx(string number)
        {
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@number", number);

            object objNum = DBHelper.ExecuteScalar("select top 1 iszx from memberOffSP where number=@number  order by zxdate desc", para, CommandType.Text);
            int num = 0;
            if (objNum != null && objNum != DBNull.Value)
            {
                num = (int)objNum;
            }
            return num;
        }
        public static int getInsertMemberSPZX(MemberOffModel mom, string zxname)
        {
            SqlParameter[] para = new SqlParameter[7];
            para[0] = new SqlParameter("@number", mom.Number);
            para[1] = new SqlParameter("@zxqishu", mom.Zxqishu);
            para[2] = new SqlParameter("@zxname", zxname);
            para[3] = new SqlParameter("@zxdate", mom.Zxfate);
            para[4] = new SqlParameter("@offReason", mom.OffReason);
            para[5] = new SqlParameter("@Operator", mom.OperatorNo);
            para[6] = new SqlParameter("@OperatorName", mom.OperatorName);

            int num = DBHelper.ExecuteNonQuery("MemberoffSP_zx", para, CommandType.StoredProcedure);


            return num;
        }
        public static int getUpdateMemberSPZX(string number, int qishu, int id, DateTime zxdate, string reason, string opert, string opname)
        {
            SqlParameter[] para = new SqlParameter[7];
            para[0] = new SqlParameter("@number", number);
            para[1] = new SqlParameter("@zxqishu", qishu);
            para[2] = new SqlParameter("@id", id);
            para[3] = new SqlParameter("@zxdate", zxdate);

            para[4] = new SqlParameter("@offReason", reason);
            para[5] = new SqlParameter("@Operator", opert);
            para[6] = new SqlParameter("@OperatorName", opname);


            int num = DBHelper.ExecuteNonQuery("Memberoffsp_fhzx", para, CommandType.StoredProcedure);

            return num;
        }
        public static int getMember(string number)
        {
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@number", number);

            int num = (int)DBHelper.ExecuteScalar("select count(0) from memberinfo where number=@number",para,CommandType.Text);

            return num;
        }

        public static int getMemberZX(string number)
        {
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@number", number);

            int num = (int)DBHelper.ExecuteScalar("select count(0) from memberOff where number=@number", para, CommandType.Text);

            return num;
        }

        public static int getMemberISzx(string number)
        {
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@number", number);

            int num = (int)DBHelper.ExecuteScalar("select top 1 iszx from memberOff where number=@number  order by zxdate desc", para, CommandType.Text);

            return num;
        }

        
        public static int getInsertMemberZX(MemberOffModel mom, string zxname)
        {
            SqlParameter[] para = new SqlParameter[7];
            para[0] = new SqlParameter("@number", mom.Number);
            para[1] = new SqlParameter("@zxqishu",mom.Zxqishu);
            para[2] = new SqlParameter("@zxname",zxname);
            para[3] = new SqlParameter("@zxdate",mom.Zxfate);
            para[4] = new SqlParameter("@offReason",mom.OffReason);
            para[5] = new SqlParameter("@Operator", mom.OperatorNo);
            para[6] = new SqlParameter("@OperatorName", mom.OperatorName);

            int num = DBHelper.ExecuteNonQuery("Memberoff_zx", para, CommandType.StoredProcedure);


            return num;
        }

        public static int getUpdateMemberZX(string number, int qishu, int id, DateTime zxdate, string Operator, string OperatorName)
        {
            SqlParameter[] para = new SqlParameter[6];
            para[0] = new SqlParameter("@number", number);
            para[1] = new SqlParameter("@zxqishu", qishu);
            para[2] = new SqlParameter("@id",id);
            para[3] = new SqlParameter("@zxdate", zxdate);
            para[4] = new SqlParameter("@Operator", Operator);
            para[5] = new SqlParameter("@OperatorName", OperatorName);

            int num = DBHelper.ExecuteNonQuery("Memberoff_fhzx", para, CommandType.StoredProcedure);
    
            return num;
        }


    }
}
