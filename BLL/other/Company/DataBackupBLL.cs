using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Add Namespace
using System.Data;
using DAL;
using Model;

/*
 * 创建者：     汪  华
 * 创建时间：   2009-09-10
 * 对应菜单：   系统管理->数据备份
 */

namespace BLL.other.Company
{
    public class DataBackupBLL
    {
        /// <summary>
        /// 从计算表中获取期数
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetExpectNumFromConfig()
        {
            return ConfigDAL.GetExpectNumFromConfig();            
        }

        /// <summary>
        /// 把数据从会员信息表中复制到会员备份基本信息表中
        /// </summary>
        /// <returns>返回插入所影响的行数</returns>
        public static int AddAllDataFromMemberInfo()
        {
            return BackupMemberInfoDAL.AddAllDataFromMemberInfo();
        }

        /// <summary>
        /// 把数据从会员信息表中复制到会员备份基本信息表中
        /// </summary>
        /// <returns>返回插入所影响的行数</returns>
        public static int AddAllDataFromMemberInfo(DateTime beginDate, DateTime endDate)
        {
            return BackupMemberInfoDAL.AddAllDataFromMemberInfo(beginDate,endDate);
        }

        /// <summary>
        /// 把数据从会员信息表中复制到会员备份基本信息表中
        /// </summary>
        /// <returns>返回插入所影响的行数</returns>
        public static int AddAllDataFromMemberInfo(int qishu)
        {
            return BackupMemberInfoDAL.AddAllDataFromMemberInfo(qishu);
        }

        /// <summary>
        /// 将指定期数的数据从会员报单产品明细备份到会员备份报单产品明细
        /// </summary>
        /// <param name="expectNum">期数</param>
        /// <returns>返回插入所影响的行数</returns>
        public static int AddBackupMemberDetailsByExpectNum(int expectNum)
        {
            return BackupMemberDetailsDAL.AddBackupMemberDetailsByExpectNum(expectNum);
        }

        /// <summary>
        /// 备份指定时间的会员报单产品明细
        /// </summary>
        /// <param name="beginDate">开始时间</param>
        /// <param name="endDate">截止时间</param>
        /// <returns>返回插入记录所影响的行数</returns>
        public static int AddBackupMemberDetailsByDate(DateTime beginDate, DateTime endDate)
        {
            return BackupMemberDetailsDAL.AddBackupMemberDetailsByDate(beginDate,endDate);
        }

        /// <summary>
        /// 备份指定时间段店铺订货产品明细
        /// </summary>
        /// <returns>返回插入所影响的行数</returns>
        public static int AddBackupOrderDetailByDate(DateTime beginDate, DateTime endDate)
        {
            return BackupOrderDetailDAL.AddBackupOrderDetailByDate(beginDate, endDate);
        }

        /// <summary>
        /// 备份指定期数店铺订货产品明细记录
        /// </summary>
        /// <param name="expectNum">期数</param>
        /// <returns>返回插入记录所影响的行数</returns>
        public static int AddBackupOrderDetailByExpectNum(int expectNum)
        {
            return BackupOrderDetailDAL.AddBackupOrderDetailByExpectNum(expectNum);
        }

        /// <summary>
        /// 向数据库备份路径表中插入记录
        /// </summary>
        /// <param name="backupDatabaseModel">数据库备份路径表模型</param>
        /// <returns>返回向数据库备份路径表中插入记录所影响的行数</returns>
        public static int AddBackupDatabase(BackupDatabaseModel backupDatabaseModel)
        {
            return BackupDatabaseDAL.AddBackupDatabase(backupDatabaseModel);
        }

        /// <summary>
        /// 根据期数删除会员备份报单产品明细数据
        /// </summary>
        /// <param name="expectNum">期数</param>
        /// <returns>返回删除会员备份报单产品明细数据所影响的行数</returns>
        public static int DelBackupMemberDetailsByExpectNum(int expectNum)
        {
            return BackupMemberDetailsDAL.DelBackupMemberDetailsByExpectNum(expectNum);
        }

        /// <summary>
        /// 删除指定日期备份的会员报单产品明细记录
        /// </summary>
        /// <param name="beginDate">开始时间</param>
        /// <param name="endDate">截止时间</param>
        /// <returns>返回删除所影响的行数</returns>
        public static int DelBackupMemberDetailsByDate(DateTime beginDate, DateTime endDate)
        {
            return BackupMemberDetailsDAL.DelBackupMemberDetailsByDate(beginDate,endDate);
        }

        /// <summary>
        /// 删除指定期数店铺订货产品明细记录
        /// </summary>
        /// <param name="expectNum">期数</param>
        /// <returns>返回删除记录所影响的行数</returns>
        public static int DelBackupOrderDetailByExpectNum(int expectNum)
        {
            return BackupOrderDetailDAL.DelBackupOrderDetailByExpectNum(expectNum);
        }

        /// <summary>
        /// 清空所有的店铺订货产品明细备份数据
        /// </summary>
        /// <returns>返回受影响的行数</returns>
        public static int ClearAllBackupOrderDetail()
        {
            return BackupOrderDetailDAL.ClearAllBackupOrderDetail();
        }

        /// <summary>
        /// 删除指定文件路径名数据记录
        /// </summary>
        /// <param name="filePathName">文件路径名</param>
        /// <returns>返回删除指定文件路径名数据记录</returns>
        public static int DelBackupDatabaseByFilePathName(string filePathName)
        {
            return BackupDatabaseDAL.DelBackupDatabaseByFilePathName(filePathName);
        }

        /// <summary>
        /// 从会员备份基本信息表获取所有的信息
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetAllInfoFromBackupMemberInfo()
        {
            return BackupMemberInfoDAL.GetAllInfoFromBackupMemberInfo();
        }

        /// <summary>
        /// 获取所有会员备份报单产品明细数据
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetALLBackupMemberDetails()
        {
            return BackupMemberDetailsDAL.GetALLBackupMemberDetails();
        }

        /// <summary>
        /// 获取所有的店铺订货产品明细备份数据
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetALLBackupOrderDetail()
        {
            return BackupOrderDetailDAL.GetALLBackupOrderDetail();
        }

        /// <summary>
        /// 获取备份数据库系信息
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetBackupDatabaseInfo()
        {
            return BackupDatabaseDAL.GetBackupDatabaseInfo();
        }

        /// <summary>
        /// 根据管理员的编号得到管理员姓名
        /// </summary>
        /// <param name="number">管理员编号</param>
        /// <returns>返回管理员姓名</returns>
        public static string GetNameByAdminID(string number)
        {
            return ManageDAL.GetNameByAdminID(number);
        }

        /// <summary>
        /// 设定要备份的数据库名
        /// </summary>
        /// <returns>返回要备份的数据库名</returns>
        public static string GetDataBaseName()
        {
            return BackupDatabaseDAL.GetDataBaseName();
        }

        /// <summary>
        /// 备份数据库
        /// </summary>
        /// <param name="databaseName">数据库名称</param>
        /// <param name="pathFileName">路径及文件名</param>
        /// <returns>返回</returns>
        public static int BackupDatabaseInfo(string databaseName, string pathFileName)
        {
            return BackupDatabaseDAL.BackupDatabaseInfo(databaseName, pathFileName);
        }
    }
}
