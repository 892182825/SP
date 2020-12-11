using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Add Namespace
using DAL;


/*
 *  创建者：    汪  华
 *  创建时间：  2009-09-10
 *  对应菜单：  系统管理->密码修改
 */

namespace BLL.other.Company
{
    public class PassModifyBLL
    {
        /// <summary>
        /// 更改管理员登录密码
        /// </summary>
        /// <param name="number">管理员编号</param>
        /// <param name="newloginPass">新密码</param>
        /// <returns>返回更改管理员密码锁影响的行数</returns>
        public static int UpdManageLoginPass(string number, string newloginPass)
        {
            return ManageDAL.UpdManageLoginPass(number, newloginPass);
        }

        /// <summary>
        /// 通过管理员编号和登录密码获取行数
        /// </summary>
        /// <param name="number">管理员编号</param>
        /// <param name="loginPass">登录密码</param>
        /// <returns>返回行数</returns>
        public static int GetCountByNumAndLoginPass(string number, string loginPass)
        {
            return ManageDAL.GetCountByNumAndLoginPass(number, loginPass);
        }

    }
}
