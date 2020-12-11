using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Web;


//Add Namespace
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using Model;

namespace BLL.other.Company
{
    public class MemberOffBLL
    {
        public static string getMemberName(string number)
        {
            return MemberOffDAL.getMemberName(number);
        }
        public static int getMemberSPISzx(string number)
        {
            return MemberOffDAL.getMemberSPISzx(number);
        }
        /// <summary>
        /// --ds2012--www-b874dce8700
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="number"></param>
        /// <param name="qishu"></param>
        /// <param name="id"></param>
        /// <param name="zxdate"></param>
        /// <returns></returns>
        public static int getUpdateMemberSPZX(string number, int qishu, int id, DateTime zxdate, string reason, string opert, string opname)
        {
            return MemberOffDAL.getUpdateMemberSPZX(number, qishu, id, zxdate, reason, opert, opname);
        }
        /// <summary>
        /// --ds2012--www-b874dce8700
        /// </summary>
        /// <param name="number"></param>
        /// <param name="qishu"></param>
        /// <param name="zxname"></param>
        /// <param name="zxdate"></param>
        /// <returns></returns>
        public static int getInsertMemberSPZX(MemberOffModel mom, string zxname)
        {
            return MemberOffDAL.getInsertMemberSPZX(mom, zxname);
        }

        /// <summary>
        /// --ds2012--www-b874dce8700
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int getMember(string number)
        {
            return MemberOffDAL.getMember(number);
        }

        /// <summary>
        /// --ds2012--www-b874dce8700
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int getMemberZX(string number)
        {
            return MemberOffDAL.getMemberZX(number);
        }

        public static int getMemberISzx(string number)
        {
            return MemberOffDAL.getMemberISzx(number);
        }

        /// <summary>
        /// --ds2012--www-b874dce8700
        /// </summary>
        /// <param name="number"></param>
        /// <param name="qishu"></param>
        /// <param name="zxname"></param>
        /// <param name="zxdate"></param>
        /// <returns></returns>
        public static int getInsertMemberZX(MemberOffModel mom,string zxname)
        {
            return MemberOffDAL.getInsertMemberZX(mom, zxname);
        }

        /// <summary>
        /// --ds2012--www-b874dce8700
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="number"></param>
        /// <param name="qishu"></param>
        /// <param name="id"></param>
        /// <param name="zxdate"></param>
        /// <returns></returns>
        public static int getUpdateMemberZX(string number, int qishu, int id, DateTime zxdate, string Operator, string OperatorName)
        {
            return MemberOffDAL.getUpdateMemberZX(number, qishu, id, zxdate, Operator, OperatorName);
        }
    }
}
