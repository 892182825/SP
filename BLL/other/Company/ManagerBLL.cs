using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DAL;
using System.Data.SqlClient;
using System.Data;
/*
 *  创建人：  孙延昊
 *  创建时间：2009.8.27
 *  文件名：PermissionBLL.cs
 *  功能：管理员添加，修改，删除，查询
 * **/
namespace BLL.other.Company
{
    /// <summary>
    ///  管理员
    /// </summary>
    public class ManagerBLL
    {
        public static DataTable  GetAllManageNumber()
        {
            return ManageDAL.GetAllManageNumber();
        }
        /// <summary>
        /// 获取所有管理员信息
        /// </summary>
        /// <returns></returns>
        public static IList<ManageModel> GetManages()
        {
            return ManageDAL.GetManages();
        }

        /// <summary>
        /// 获取管理员信息
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <returns></returns>
        public static IList<ManageModel> GetManages(int roleID)
        {
            return ManageDAL.GetManages(roleID);
        }

        /// <summary>
        /// ds2012
        /// 获取管理员信息
        /// </summary>
        /// <param name="manageid">管理员ID</param>
        /// <returns></returns>
        public static ManageModel GetManage(int manageid)
        {
            return ManageDAL.GetManage(manageid);
        }

        /// <summary>
        /// 添加管理员
        /// </summary>
        /// <param name="manage">管理员信息</param>
        /// <param name="manageid">管理员权限人编号</param>
        /// <returns></returns>
        public static int AddManage(ManageModel manage,string manageId)
        {
            //if (!CheckManage(manageId, manage.Number))
            //{
            //    return false;
            //}
            return ManageDAL.AddManage(manage,manageId);
        }

        /// <summary>
        /// 更新管理员信息
        /// </summary>
        /// <param name="manage">管理员信息</param>
        /// <returns></returns>
        public static bool UptManage(ManageModel manage)
        {
            return ManageDAL.UptManage(manage);
        }
        ///// <summary>
        ///// 删除管理员信息
        ///// </summary>
        ///// <param name="Dept"></param>
        ///// <param name="manageNum"></param>
        ///// <returns></returns>
        public static bool DelManage(int manageid, string manageNum)
        {
            //if (!CheckManage(manageNum, manage.RoleID))
            //{
            //    return false;
            //}
            //return ManageDAL.DelManage(manageid) ;
            return false;//以上有错误
        }

        /// <summary>
        /// 判断某管理员对某角色管理员的权限
        /// </summary>
        /// <param name="manageNum">权限人编号</param>
        /// <param name="roleNum">管理员编号</param>
        /// <returns></returns>
        public static bool CheckManage(string manageNum, string roleNum)
        {
            return ManageDAL.GetIsPerMan(manageNum, roleNum);
        }

        /// <summary>
        /// 根据管理员编号获得管理员姓名
        /// </summary>
        /// <param name="manageNum">管理员编号</param>
        /// <returns></returns>
        public static string GetNameByAdminID(string manageNum)
        {

            return "";
        }

        /// <summary>
        /// 根据编号获取管理员
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static ManageModel GetManage(string number)
        {
            return ManageDAL.GetManageByNumber(number);
        }

        public static string GetPassword(string p)
        {
            if (p.Length > 6)
            {
                return p;// p.Substring(p.Length - 7, p.Length - 1);
            }
            else
            {
                return p;
            }
        }


        /// <summary>
        /// 验证管理员编号是否存在
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static bool CheckNumber(string p)
        {
            ManageModel manager = ManageDAL.GetManageByNumber(p);
            return manager == null;
        }

        /// <summary>
        /// 删除指定编号的管理员
        /// </summary>
        /// <param name="id">管理员标识</param>
        /// <returns>是否成功</returns>
        public static int DelManage(int id)
        {
            return ManageDAL.DelManage(id);
        }

        /// <summary>
        /// 删除管理员可以查看的团队网络
        /// </summary>
        /// <param name="id">团队标识</param>
        /// <returns>是否成功</returns>
        public static int DelViewManage(int id)
        {
            return ManageDAL.DelViewManage(id);
        }

        
        /// <summary>
        /// 添加物流管理员
        /// </summary>
        /// <param name="manage"></param>
        /// <param name="logist"></param>
        /// <returns></returns>
        public static int AddManageLogistics(System.Data.SqlClient.SqlTransaction tran, string number,string name,int logisterId,string permissionMan)
        {
            ManageModel model = new ManageModel();
            model.Number = number;
            model.LoginPass = Model.Other.MD5Help.MD5Encrypt2(number);
            model.Name = name;
            model.RoleID = 81;//公司部门隐藏角色，在系统创建时创建，id要变化
            model.Status = 1;
            model.PermissionMan = permissionMan;
            model.LastLoginDate = DateTime.Now;
            model.BeginDate = DateTime.Now;
            int n = ManageDAL.AddManageLogistics(tran,model,logisterId);
            return n;
        }

        /// <summary>
        /// 重置管理员的登录密码
        /// </summary>
        /// <param name="manageModel"></param>
        /// <returns></returns>
        public static int ResetLoginpass(ManageModel manageModel)
        {
            return ManageDAL.ResetPass(manageModel);
        }


        public static bool UpdateDefaultManage(Model.DefaultMessage def)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    if (!DAL.ManageDAL.InsertUpaDetails(def, tran))
                    {
                        tran.Rollback();
                        return false;
                    }

                    if (!DAL.ManageDAL.UpdateManageId(def.NewId,def.OldId, tran))
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
                    conn.Close();
                }
            }
        }

        public static bool UpdateDefaultStore(Model.DefaultMessage def)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    if (!DAL.ManageDAL.InsertUpaDetails(def, tran))
                    {
                        tran.Rollback();
                        return false;
                    }

                    if (!DAL.ManageDAL.UpdateStoreId(def.NewId,def.OldId,tran))
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
                    conn.Close();
                }
            }
        }

        public static bool UpdateDefaultMember(Model.DefaultMessage def)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    if (!DAL.ManageDAL.InsertUpaDetails(def, tran))
                    {
                        tran.Rollback();
                        return false;
                    }

                    if (!DAL.ManageDAL.UpdateMemberId(def.NewId, def.OldId, tran))
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
                    conn.Close();
                }
            } 
        }
    }
}
