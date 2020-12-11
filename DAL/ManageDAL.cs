using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

//Add Namespace
using Model;
using System.Data;
using Model.Other;

/*
 * 创建者：     汪  华
 * 创建时间：   2009-09-08
 * 文件名：     ManageDAL
 */

namespace DAL
{
    public class ManageDAL
    {
        /// <summary>
        /// 获取管理员信息
        /// </summary>
        /// <returns></returns>
        public static IList<Model.ManageModel> GetManages()
        {
            IList<ManageModel> manages = null;
            SqlDataReader reader = DBHelper.ExecuteReader("", CommandType.StoredProcedure);
            if (reader.HasRows)
            {
                manages = new List<ManageModel>(0);
                while (reader.Read())
                {
                    ManageModel manage = new ManageModel(reader.GetInt32(0));
                    manage.Number = reader.GetString(1);
                    manage.Name = reader.GetString(2);
                    manage.Post = reader.GetString(3);
                    manage.Branch = reader.GetString(4);
                    manage.BeginDate = reader.GetDateTime(5);
                    manage.LastLoginDate = reader.GetDateTime(6);
                    manage.Status = reader.GetInt32(7);
                    manage.PermissionMan = reader.GetString(8);
                    manage.RoleID = reader.GetInt32(9);
                    manages.Add(manage);
                }
            }
            reader.Close();
            return manages;
        }
        /// <summary>
        /// 根据角色获取管理员信息
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <returns></returns>
        public static IList<Model.ManageModel> GetManages(int roleID)
        {
            IList<ManageModel> manages = null ;
            SqlParameter[] paras = new SqlParameter[1];
            paras[0] = DBHelper.GetSqlParameter("@DeptRole",SqlDbType.Int,paras[0],roleID);
            SqlDataReader reader = DBHelper.ExecuteReader("GetManagesByComDeptId", paras, CommandType.StoredProcedure);
            if (reader.HasRows)
            {
                manages = new List<ManageModel>(0);
                while (reader.Read())
                {
                    ManageModel manage = new ManageModel(reader.GetInt32(0));
                    manage.Number = reader.GetString(1);
                    manage.Name = reader.GetString(2);
                    manages.Add(manage);
                }
            }
            reader.Close();
            return manages;
        }


        public static SqlParameter GetSqlParameter(string paraName, SqlDbType sqlDbType, out SqlParameter para, object obj)
        {
            para = new SqlParameter(paraName, sqlDbType);
            para.Value = obj;
            return para;
        }

        public static SqlParameter GetSqlParameter(string paraName, SqlDbType sqlDbType, object obj)
        {
            SqlParameter para = new SqlParameter(paraName, sqlDbType);
            para.Value = obj;
            return para;
        }

        /// <summary>
        /// 添加新管理员
        /// </summary>
        /// <param name="manage">管理员对象</param>
        /// <param name="manageNum">管理员编号</param>
        /// <returns></returns>
        public static int AddManage(Model.ManageModel manage, string manageNum)
        {
            try
            {
                SqlParameter[] paras = new SqlParameter[]{
                    GetSqlParameter("@Number",SqlDbType.VarChar,manage.Number),
                    GetSqlParameter("@Name",SqlDbType.VarChar,manage.Name),
                    GetSqlParameter("@Post",SqlDbType.VarChar,manage.Post),
                    GetSqlParameter("@Branch",SqlDbType.VarChar,manage.Branch),
                    GetSqlParameter("@LoginPass",SqlDbType.VarChar,manage.LoginPass),
                    GetSqlParameter("@BeginDate",SqlDbType.DateTime,manage.BeginDate),
                    GetSqlParameter("@LastLoginDate",SqlDbType.SmallDateTime,manage.LastLoginDate),
                    GetSqlParameter("@State",SqlDbType.Int,manage.Status),
                    GetSqlParameter("@PermissionMan",SqlDbType.NVarChar,manage.PermissionMan),
                    GetSqlParameter("@RoleID",SqlDbType.Int,manage.RoleID),
                    GetSqlParameter("@IsViewPermissions",SqlDbType.Int,manage.IsViewPermissions),
                    GetSqlParameter("@IsRecommended",SqlDbType.Int,manage.IsRecommended)
            };
                return DBHelper.ExecuteNonQuery("AddManage", paras, CommandType.StoredProcedure);
            }
            catch (SqlException)
            {
                return -1;
            }
        }
        /// <summary>
        /// 更新管理员信息
        /// </summary>
        /// <param name="manage">管理员对象</param>
        /// <param name="manageNum">管理员编号</param>
        /// <returns></returns>
        public static bool UptManage(Model.ManageModel manage)
        {
            bool n = false;
            try
            {
                SqlParameter[] paras = new SqlParameter[]{
                    GetSqlParameter("@id",SqlDbType.Int,manage.ID),
                    GetSqlParameter("@Number",SqlDbType.VarChar,manage.Number),
                    GetSqlParameter("@Name",SqlDbType.VarChar,manage.Name),
                    GetSqlParameter("@Post",SqlDbType.VarChar,manage.Post),
                    GetSqlParameter("@Branch",SqlDbType.VarChar,manage.Branch),
                    GetSqlParameter("@Status",SqlDbType.Int,manage.Status),
                    GetSqlParameter("@PermissionMan",SqlDbType.VarChar,manage.PermissionMan),
                    GetSqlParameter("@RoleID",SqlDbType.Int,manage.RoleID),
                    GetSqlParameter("@IsViewPermissions",SqlDbType.Int,manage.IsViewPermissions),
                    GetSqlParameter("@IsRecommended",SqlDbType.Int,manage.IsRecommended)
            };
                n=DBHelper.ExecuteNonQuery("UptManage", paras, CommandType.StoredProcedure)==1;
            }
            catch (SqlException)
            {
                //throw new Exception("修改管理员信息产生异常!");
                return false;
            }
            return n;
        }

        /// <summary>
        /// 更改管理员登录密码
        /// </summary>
        /// <param name="number">管理员编号</param>
        /// <param name="newloginPass">新密码</param>
        /// <returns>返回更改管理员密码锁影响的行数</returns>
        public static int UpdManageLoginPass(string number, string newloginPass)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@number",SqlDbType.NVarChar,40),
                new SqlParameter("@newloginPass",SqlDbType.VarChar,100)
            };
            sparams[0].Value = number;
            sparams[1].Value = newloginPass;
            
            return DBHelper.ExecuteNonQuery("UpdManageLoginPass", sparams, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 通过服务机构编号获取服务机构姓名
        /// </summary>
        /// <param name="number">服务机构编号</param>
        /// <returns>服务机构姓名</returns>
        public static string GetStoreNameByNumber(string number)
        {
            String sql = "select StoreName from StoreInfo where StoreID='"+ number+"'";
            String storeName = (String)DBHelper.ExecuteScalar(sql);

            return storeName;
        }


        /// <summary>
        /// 通过管理员编号获取管理员姓名
        /// </summary>
        /// <param name="number">管理员编号</param>
        /// <returns>返回管理员姓名</returns>
        public static string GetManageNameByNumber(string number)
        {
            string manageName = string.Empty;
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@number",SqlDbType.NVarChar,40)
            };
            sparams[0].Value = number;
            SqlDataReader dr = DBHelper.ExecuteReader("GetManageNameByNumber", sparams, CommandType.StoredProcedure);
            while (dr.Read())
            {
                manageName = dr[0].ToString();
            }
            dr.Close();
            return (manageName.Length == 0 ? "管理员" : manageName);
        }

        /// <summary>
        /// 验证当前登录管理员是否可以对指定管理员进行修改，删除操作
        /// </summary>
        /// <param name="manageNum">权限人编号</param>
        /// <param name="roleNum">管理员编号</param>
        /// <returns></returns>
        public static bool GetIsPerMan(string manageNum, string roleNum)
        {
            SqlParameter[] paras = new SqlParameter[] { 
                GetSqlParameter("@manageNum",SqlDbType.VarChar,roleNum)
            };
            object obj = DBHelper.ExecuteScalar("GetPerMan",paras, CommandType.StoredProcedure);
            return obj == null ? false : obj.ToString().Equals(manageNum);
        }
        
        /// <summary>
        /// 添加物流公司管理员
        /// </summary>
        /// <param name="manage">管理员信息</param>
        /// <param name="eCompanyPer">物流公司仓库权限</param>
        /// <param name="roleId">角色编号</param>
        /// <returns></returns>
        public static bool AddLogisticsManage(ManageModel manage,int[] eCompanyPer,int roleId)
        {
            DeptRoleDAL deptRoleServer = new DeptRoleDAL();
            
            return false;
        }
        /// <summary>
        /// 根据管理员编号获取管理员信息
        /// </summary>
        /// <param name="number"></param>
        /// <returns>ManageModel 管理员信息</returns>
        public static ManageModel GetManageByNumber(string number)
        {
            ManageModel manage = null;
            //调用存储过程根据编号查询管理员
            SqlDataReader reader = DBHelper.ExecuteReader("GetManageByNumber",new SqlParameter("@number",number) ,CommandType.StoredProcedure);
            if (reader.HasRows)
            {
                reader.Read();
                //给管理员对象赋值
                manage = new ManageModel(reader.GetInt32(0));
                manage.Number = reader.GetString(1);
                manage.Name = reader.GetString(2);
                manage.Post = reader.GetString(3);
                manage.Branch = reader.GetString(4);
                manage.BeginDate = reader.GetDateTime(6);
                manage.LastLoginDate = reader.GetDateTime(7);
                manage.Status = reader.GetInt32(8);
                manage.PermissionMan = reader.GetString(9);
                manage.RoleID = reader.GetInt32(10);
            }
            reader.Close();
            return manage;
        }

        /// <summary>
        /// 通过管理员编号获取角色ID
        /// </summary>
        /// <param name="number">管理员编号</param>
        /// <returns>返回角色ID</returns>
        public static int GetRoleIDByNumber(string number)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            { 
                new SqlParameter("@number",SqlDbType.VarChar,100)
            };
            sparams[0].Value = number;
            
            return Convert.ToInt32(DBHelper.ExecuteScalar("GetRoleIDByNumber", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// 通过管理员编号和登录密码获取行数
        /// </summary>
        /// <param name="number">管理员编号</param>
        /// <param name="loginPass">登录密码</param>
        /// <returns>返回行数</returns>
        public static int GetCountByNumAndLoginPass(string number, string loginPass)
        {
            ///注意加密后的密码变长了，所以loginPass的长度加长了
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@number",SqlDbType.NVarChar,40),
                new SqlParameter("@loginPass",SqlDbType.VarChar,100)
            };

            sparams[0].Value = number;
            sparams[1].Value = loginPass;            

            return Convert.ToInt32(DBHelper.ExecuteScalar("GetCountByNumAndLoginPass", sparams, CommandType.StoredProcedure));
        }
        
        public static int DelManage(int id)
        {
            int i = 0;
            try
            {
                string sql = "delete from manage where id = @id";
                i=DBHelper.ExecuteNonQuery(sql, GetSqlParameter("@id", SqlDbType.Int, id), CommandType.Text);
            }
            catch (SqlException)
            {
                return -1;
            }
            return i;
        }

        public static int DelViewManage(int id)
        {
            int i = 0;
            try
            {
                string sql = "delete from viewmanage where id = @id";
                i=DBHelper.ExecuteNonQuery(sql, GetSqlParameter("@id", SqlDbType.Int, id), CommandType.Text);
            }
            catch (SqlException)
            {
                return -1;
            }
            return i;
        }

        /// <summary>
        /// ds2012
        /// 根据编号获取管理员信息
        /// </summary>
        /// <param name="manageid"></param>
        /// <returns></returns>
        public static ManageModel GetManage(int manageid)
        {
            ManageModel manage = null;
            string sql = "select top 1 * from manage where id = @id";
            SqlDataReader dr = null;
            try
            {
                dr = DBHelper.ExecuteReader(sql, new SqlParameter("@id", manageid), CommandType.Text);
            }
            catch (SqlException)
            {
                if(dr!=null)
                dr.Close();
                return null;
            }
            if (dr.Read())
            {
                manage = new ManageModel(dr.GetInt32(0));
                manage.Number = dr.GetString(1);
                manage.Name = dr.GetString(2);
                manage.Post = dr.GetString(3);
                manage.Branch = dr.GetString(4);
                manage.LoginPass = dr.GetString(5);
                manage.BeginDate = dr.GetDateTime(6);
                manage.LastLoginDate = dr.GetDateTime(7);
                manage.Status = dr.GetInt32(8);
                manage.PermissionMan = dr.GetString(9);
                manage.RoleID = dr.GetInt32(10);
                manage.IsViewPermissions = dr.GetInt32(13);
                manage.IsRecommended = dr.GetInt32(14);
            }
            dr.Close();
            return manage;
        }
        /// <summary>
        /// 得到所有管理员的编号
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllManageNumber()
        {
            return DBHelper.ExecuteDataTable("GetManageNumber",CommandType.StoredProcedure);
        }
//        ID
//Number
//Name
//Post
//Branch
//LoginPass
//BeginDate
//LastLoginDate
//Status
//PermissionMan
//RoleID
        #region 根据管理员的编号得到管理员姓名
        /// <summary>
        /// 根据管理员的编号得到管理员姓名
        /// </summary>
        /// <param name="number">管理员编号</param>
        /// <returns>返回管理员姓名</returns>
        public static string GetNameByAdminID(string number)
        {
            string name = string.Empty;
            string sSQL = "Select Name From Manage Where Number = '" + number + "'";
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
        /// 添加物流公司管理员
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="model">实体类</param>
        /// <param name="logisterId">物流公司编号</param>
        /// <returns></returns>
        public static int AddManageLogistics(SqlTransaction tran, ManageModel manage, int logisterId)
        {
            SqlParameter[] paras = new SqlParameter[]{
                    new SqlParameter("@Number",manage.Number),
                    new SqlParameter("@Name",manage.Name),
                    new SqlParameter("@Post",""),
                    new SqlParameter("@Branch",""),
                    new SqlParameter("@LoginPass",manage.LoginPass),
                    new SqlParameter("@BeginDate",manage.BeginDate),
                    new SqlParameter("@LastLoginDate",manage.LastLoginDate),
                    new SqlParameter("@State",manage.Status),
                    new SqlParameter("@PermissionMan",manage.PermissionMan),
                    new SqlParameter("@RoleID",manage.RoleID),
                    new SqlParameter("@logister",logisterId)
            };
            return DBHelper.ExecuteNonQuery(tran,"AddManage", paras, CommandType.StoredProcedure);
        }

        public static int ResetPass(ManageModel manageModel)
        {
            string sql = "update manage set loginpass=@loginpass where number=@number";
            SqlParameter[] paras = new SqlParameter[]{
                new SqlParameter("@loginpass",manageModel.LoginPass),
                new SqlParameter("@number",manageModel.Number)
            };
            return DBHelper.ExecuteNonQuery(sql, paras, CommandType.Text);
        }

        /// <summary>
        /// 获取最后登录时间
        /// </summary>
        /// <param name="mid">管理员编号</param>
        /// <returns>返回时间</returns>
        public static DateTime GetLoginTime(string mid)
        {
            return DateTime.Now;
            //SqlParameter para = new SqlParameter("@mid", mid);
            //return DateTime.Parse(DBHelper.ExecuteScalar("select LastLoginDate from manage where Number=@mid", para, CommandType.Text).ToString());
        }

        public static bool InsertUpaDetails(Model.DefaultMessage def,SqlTransaction tran)
        {
            string sql = @"Insert Into UpdDefaultDedails(ExpectNum,OldId,NewId,OperateIp,OperateNum,UpdateTime,Type) values(@ExpectNum,@OldId,@NewId,@OperateIp,@OperateNum,@UpdateTime,@Type)";
            SqlParameter[] para = {
                                      new SqlParameter("@ExpectNum",SqlDbType.Int),
                                      new SqlParameter("@OldId",SqlDbType.NVarChar,30),
                                      new SqlParameter("@NewId",SqlDbType.NVarChar,30),
                                      new SqlParameter("@OperateIp",SqlDbType.NVarChar,30),
                                      new SqlParameter("@OperateNum",SqlDbType.NVarChar,30),
                                      new SqlParameter("@UpdateTime",SqlDbType.DateTime),
                                      new SqlParameter("@Type",SqlDbType.Int)
                                  };
            para[0].Value = def.ExpectNum;
            para[1].Value = def.OldId;
            para[2].Value = def.NewId;
            para[3].Value = def.OperateIp;
            para[4].Value = def.OperateNum;
            para[5].Value = def.UpdateTime;
            para[6].Value = def.Type;

            int count = (int)DBHelper.ExecuteNonQuery(tran, sql, para, CommandType.Text);
            if (count == 0)
                return false;
            return true;
        }

        public static bool UpdateManageId(string NewManageId,string OldManageId ,SqlTransaction tran)
        {
            string sql = @"Update Manage Set Number=@Number,LoginPass=@LoginPass Where DefaultManager=1";
            SqlParameter[] para = {
                                      new SqlParameter("@Number",SqlDbType.NVarChar,30),
                                      new SqlParameter("@LoginPass",SqlDbType.NVarChar,50)
                                  };
            para[0].Value = NewManageId;
            para[1].Value = Encryption.Encryption.GetEncryptionPwd(NewManageId, NewManageId);

            int count = (int)DBHelper.ExecuteNonQuery(tran, sql, para, CommandType.Text);
            if (count == 0)
                return false;

            sql = "Update viewmanage Set manageId=@ManageId Where manageId=@OldId";
            SqlParameter[] para1 = {
                                       new SqlParameter("@ManageId",SqlDbType.NVarChar,30),
                                       new SqlParameter("@OldId",SqlDbType.NVarChar,30)
                                   };
            para1[0].Value = NewManageId;
            para1[1].Value = OldManageId;
            count = (int)DBHelper.ExecuteNonQuery(tran, sql, para1, CommandType.Text);
            if (count == 0)
                return false;

            return true;
        }

        public static bool UpdateStoreId(string NewStoreId, string OldStoreId, SqlTransaction tran)
        {
            string sql = @"Update StoreInfo Set StoreId=@StoreId,Loginpass=@Loginpass Where Defaultstore=1";
            SqlParameter[] para = {
                                      new SqlParameter("@StoreId",SqlDbType.NVarChar,30),
                                      new SqlParameter("@Loginpass",SqlDbType.NVarChar,50)
                                  };
            para[0].Value = NewStoreId;
            para[1].Value = Encryption.Encryption.GetEncryptionPwd(NewStoreId, NewStoreId);

            int count = (int)DBHelper.ExecuteNonQuery(tran, sql, para, CommandType.Text);
            if (count == 0)
                return false;

            SqlParameter[] para1 = {
                                       new SqlParameter("@StoreId",SqlDbType.NVarChar,30),
                                       new SqlParameter("@OldStoreId",SqlDbType.NVarChar,30)
                                   };
            para1[0].Value = NewStoreId;
            para1[1].Value = OldStoreId;

            count = (int)DBHelper.ExecuteNonQuery(tran, "UpdateDefaultStore", para1, CommandType.Text);
            if (count == 0)
                return false;

            return true;
        }

        public static bool UpdateMemberId(string NewMemberId, string OldMemberId, SqlTransaction tran)
        {
            string sql = @"Update MemberInfo Set Number=@NewMemberId,Loginpass=@Loginpass Where DefaultNumber=1";
            SqlParameter[] para = {
                                      new SqlParameter("@NewMemberId",SqlDbType.NVarChar,30),
                                      new SqlParameter("@Loginpass",SqlDbType.NVarChar,50)
                                  };
            para[0].Value = NewMemberId;
            para[1].Value = Encryption.Encryption.GetEncryptionPwd(NewMemberId, NewMemberId);

            int count = (int)DBHelper.ExecuteNonQuery(tran, sql, para, CommandType.Text);
            if (count == 0)
                return false;

            SqlParameter[] para1 = {
                                       new SqlParameter("@OldNumber",SqlDbType.NVarChar,30),
                                       new SqlParameter("@NewNumber",SqlDbType.NVarChar,30)
                                   };
            para1[0].Value = OldMemberId;
            para1[1].Value = NewMemberId;

            count = (int)DBHelper.ExecuteNonQuery(tran, "UpdateDefaultMember", para1, CommandType.Text);
            if (count == 0)
                return false;

            return true;
        }

        /// <summary>
        /// 修改管理员密码
        /// </summary>
        /// <param name="Number">编号</param>
        /// <param name="PWD">密码</param>
        /// <returns></returns>
        public static int UpdateManagePass(string Number, string PWD)
        {
            string passWord = Encryption.Encryption.GetEncryptionPwd(PWD, Number);
            string strSql = "Update Manage set LoginPass=@LoginPass where Number=@Number";
            SqlParameter[] parms = new SqlParameter[]
            {
                new SqlParameter("@LoginPass",passWord),
                new SqlParameter("@Number",Number)
            };

            int count = DBHelper.ExecuteNonQuery(strSql,parms,CommandType.Text);

            return count;
        }

    }
}
