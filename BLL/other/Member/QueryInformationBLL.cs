using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

///Add Namespace
using DAL;
using System.Data;

/*
 * 创建者：      汪   华
 * 创建时间：    2009-09-25
 */

namespace BLL.other.Member
{
    public class QueryInformationBLL
    {
        /// <summary>
        /// 查询指定条件的发件箱表中的记录
        /// </summary>
        /// <param name="columnNames">列名</param>
        /// <param name="conditions">查询条件</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetMessageSendInfoByConditions(string columnNames, string conditions)
        {
            return MessageSendDAL.GetMessageSendInfoByConditions(columnNames, conditions); 
        }
    }
}
