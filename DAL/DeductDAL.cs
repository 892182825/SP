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
    public class DeductDAL
    {
        /// <summary>
        /// 扣款原因
        /// </summary>
        /// <param name="expectnum">期数</param>
        /// <param name="number">编号</param>
        /// <returns></returns>
        public static object Reason(int expectnum, string number,int id)
        {
            string sql = "SELECT DeductReason FROM Deduct WHERE ExpectNum=@expectnum and Number=@Number and id=@id";
            SqlParameter[] paras = new SqlParameter[3];
            paras[0] = new SqlParameter("@expectnum", SqlDbType.Int);
            paras[1] = new SqlParameter("@Number", SqlDbType.VarChar);
            paras[2] = new SqlParameter("@id", SqlDbType.Int);
            paras[0].Value = expectnum;
            paras[1].Value = number;
            paras[2].Value = id;
            object obj = DBHelper.ExecuteScalar(sql,paras,CommandType.Text);
            return obj;
        }
        /// <summary>
        /// 编号是否存在
        /// </summary>
        /// <param name="number">编号</param>
        /// <returns></returns>
        public static bool IsExist(string number)
        {
            string sql = "select count(ID) from MemberInfo where Number=@Number";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@Number",SqlDbType.VarChar,50)};
            parm[0].Value = number;
            object obj = DBHelper.ExecuteScalar(sql,parm,CommandType.Text);
            if (Convert.ToInt32(obj) <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 添加扣款或补款信息
        /// </summary>
        /// <param name="info">扣补款对象</param>
        public static bool AddInfo(DeductModel info)
        {
            SqlParameter[] parm = new SqlParameter[]{
            new SqlParameter("@Number",SqlDbType.NVarChar,50),
            new SqlParameter("@DeductMoney",SqlDbType.Money),
            new SqlParameter("@DeductReason",SqlDbType.Text),
            new SqlParameter("@ExpectNum",SqlDbType.Int),
            new SqlParameter("@IsDeduct",SqlDbType.Int),
            new SqlParameter("@OperateIP",SqlDbType.VarChar,50),
            new SqlParameter("@OperateNum",SqlDbType.VarChar,50),
            new SqlParameter("@KeyinDate",SqlDbType.DateTime)
            };
            parm[0].Value = info.Number;
            parm[1].Value = info.DeductMoney;
            parm[2].Value = info.DeductReason;
            parm[3].Value = info.ExpectNum;
            parm[4].Value = info.IsDeduct;
            parm[5].Value = info.OperateIP;
            parm[6].Value = info.OperateNum;
            parm[7].Value = DateTime.Now.ToUniversalTime();
            return DBHelper.ExecuteNonQuery("Deduct_Withhold", parm, CommandType.StoredProcedure) > 0 ? true : false;
        }
        /// <summary>
        /// 添加扣款或补款信息
        /// </summary>
        /// <param name="info">扣补款对象</param>
        public static bool AddInfo(SqlTransaction tran,DeductModel info)
        {
            SqlParameter[] parm = new SqlParameter[]{
            new SqlParameter("@Number",SqlDbType.NVarChar,50),
            new SqlParameter("@DeductMoney",SqlDbType.Money),
            new SqlParameter("@DeductReason",SqlDbType.Text),
            new SqlParameter("@ExpectNum",SqlDbType.Int),
            new SqlParameter("@IsDeduct",SqlDbType.Bit),
            new SqlParameter("@OperateIP",SqlDbType.VarChar,50),
            new SqlParameter("@OperateNum",SqlDbType.VarChar,50),
            new SqlParameter("@KeyinDate",SqlDbType.DateTime)
            };
            parm[0].Value = info.Number;
            parm[1].Value = info.DeductMoney;
            parm[2].Value = info.DeductReason;
            parm[3].Value = info.ExpectNum;
            parm[4].Value = info.IsDeduct;
            parm[5].Value = info.OperateIP;
            parm[6].Value = info.OperateNum;
            parm[7].Value = DateTime.Now.ToUniversalTime();
            return DBHelper.ExecuteNonQuery(tran,"Deduct_Withhold", parm, CommandType.StoredProcedure)>0?true:false;
        }
        /// <summary>
        /// 审核补扣款
        /// </summary>
        /// <param name="d"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static int UpdateDeduct(DeductModel d,SqlTransaction tran)
        {
            string sql = "update deduct set isAudit=1,AuditTime=@auditingtime,OperateIP=@OperateIP,OperateNum=@OperateNum,AuditExpectNum=@AuditExpectNum where id=@id";
            SqlParameter[] par = new SqlParameter[]{
                new SqlParameter("@auditingtime",d.AuditingTime),
                new SqlParameter("@OperateIP",d.OperateIP),
                new SqlParameter("@OperateNum",d.OperateNum),
                new SqlParameter("@AuditExpectNum",d.Auditingexctnum),
                new SqlParameter("@id",d.ID)
            };
            return DBHelper.ExecuteNonQuery(tran, sql, par, CommandType.Text);
        }

        /// <summary>
        /// 根据编号获取会员信息
        /// </summary>
        /// <param name="number">编号</param>
        /// <returns></returns>
        public static MemberInfoModel GetMemberInfo(string number)
        {
            MemberInfoModel info = new MemberInfoModel();
            string sql = "SELECT Number,Name,case Sex when 0 then '女' when 1 then '男' end Sex,Address FROM MemberInfo  WHERE Number=@Number";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@Number", SqlDbType.VarChar, 50) };
            parm[0].Value = number;
            SqlDataReader reader = DBHelper.ExecuteReader(sql,parm,CommandType.Text);
            if (reader.Read())
            {
                info.Number = reader["Number"].ToString();
                info.Name = reader["Name"].ToString();
                //info.SexStr = reader["Sex"].ToString();
                info.Address = reader["Address"].ToString();
            }
            reader.Close();
            return info;
        }
        /// <summary>
        /// 根据期数和编号查询扣补款信息
        /// </summary>
        /// <param name="expectnum">期数</param>
        /// <param name="number">编号</param>
        /// <returns></returns>
        public static DeductModel GetDeductInfo(int expectnum, string number)
        {
            DeductModel info = null ;
            string sql = "select ID,DeductMoney,DeductReason,ExpectNum,Number from Deduct where Deduct.ExpectNum=@ExpectNum and Deduct.Number=@Number";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@ExpectNum", SqlDbType.Int), new SqlParameter("@Number", SqlDbType.VarChar, 50) };
            parm[0].Value = expectnum;
            parm[1].Value = number;
            SqlDataReader reader = DBHelper.ExecuteReader(sql,parm,CommandType.Text);
            if (reader.Read())
            {
                info = new DeductModel(reader.GetInt32(0));
                info.DeductMoney = 0;// double.Parse(reader["DeductMoney"].ToString());
                info.DeductReason = reader["DeductReason"].ToString();
                info.ExpectNum = Convert.ToInt32(reader["ExpectNum"].ToString());
                info.Number = reader["Number"].ToString();
            }
            reader.Close();
            return info;
        }

        /// <summary>
        /// 获取扣补款信息
        /// </summary>
        /// <param name="expct"></param>
        /// <param name="deduct"></param>
        /// <param name="mark"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public DataTable GetMember(int expct,int deduct,string mark,string search)
        {
            string sql = "select MemberInfo.Number,MemberInfo.Name,Deduct.ID,Deduct.DeductMoney,Deduct.DeductReason,Deduct.ExpectNum,case when Deduct.IsDeduct = 0 then '扣款' else '补款' end as IsDeduct,KeyinDate,Deduct.OperateNum from MemberInfo,Deduct where  Deduct.Number=MemberInfo.Number";
            if (expct != -1)
            {
                sql = sql + " and Deduct.ExpectNum="+expct;
            }
            if (deduct != -1)
            {
                sql = sql + " and IsDeduct ="+deduct;
            }
            if (mark!=string.Empty && search != string.Empty)
            {
                sql = sql + " and " + mark + " like '%" + search + "%' ";
            }
            return DBHelper.ExecuteDataTable(sql, CommandType.Text);
        }
        /// <summary>
        /// 判断是否已经添加计算差异
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool isExistsBonusDifference(int id)
        {
            string sql = "select count(*) from BonusDifference where id=@id and cyflag=1";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@id",SqlDbType.Int)};
            parm[0].Value = id;
            if (Convert.ToInt32(DBHelper.ExecuteScalar(sql, parm, CommandType.Text)) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更新记录差异状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int upBonusDifference(int id)
        {
            string sql = "update BonusDifference set cyflag=1 where id=@id";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@id",SqlDbType.Int)};
            parm[0].Value = id;
            return DBHelper.ExecuteNonQuery(sql, parm, CommandType.Text);
        }
        /// <summary>
        /// 获得记录差异信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static BonusDifferenceModel GetBonusDifference(int id)
        {
            BonusDifferenceModel info = null;
            string sql = "select * from BonusDifference where id=@id";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int) };
            parm[0].Value = id;
            SqlDataReader reader = DBHelper.ExecuteReader(sql,parm,CommandType.Text);
            if (reader.Read())
            {
                info = new BonusDifferenceModel();
                info.Chayi = Convert.ToDouble(reader["Chayi"]);
                info.Cyflag = Convert.ToInt32(reader["Cyflag"]);
                info.Id = Convert.ToInt32(reader["Id"]);
                info.Jsdate = Convert.ToDateTime(reader["Jsdate"]);
                info.Number = reader["Number"].ToString();
                info.Qishu = Convert.ToInt32(reader["Qishu"]);
                info.Xbukuan = Convert.ToDouble(reader["Xbukuan"]);
                info.Xkoukuan = Convert.ToDouble(reader["Xkoukuan"]);
                info.Xkoushui = Convert.ToDouble(reader["Xkoushui"]);
                info.Xshifa = Convert.ToDouble(reader["Xshifa"]);
                info.Xzongji = Convert.ToDouble(reader["Xzongji"]);
                info.Ybukuan = Convert.ToDouble(reader["Ybukuan"]);
                info.Ykoukuan = Convert.ToDouble(reader["Ykoukuan"]);
                info.Ykoushui = Convert.ToDouble(reader["Ykoushui"]);
                info.Yshifa = Convert.ToDouble(reader["Yshifa"]);
                info.Yzongji = Convert.ToDouble(reader["Yzongji"]);
            }
            reader.Close();
            return info;
        }
        /// <summary>
        /// 删除记录差异
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DelBonusDifference(int id)
        {
            string sql = "delete BonusDifference where id=@id";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int) };
            parm[0].Value = id;
            return DBHelper.ExecuteNonQuery(sql, parm, CommandType.Text);
        }
        /// <summary>
        /// 判断是否已经删除计算差异
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool isDelBonusDifference(int id)
        {
            string sql = "select count(*) from BonusDifference where id=@id";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int) };
            parm[0].Value = id;
            if (Convert.ToInt32(DBHelper.ExecuteScalar(sql, parm, CommandType.Text)) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
