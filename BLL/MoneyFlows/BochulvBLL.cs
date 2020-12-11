using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Model.Other;
using DAL;

/*
 * 创建人：孙延昊
 * 文件名：BochulvBLL.cs
 * 创建时间：2009-9-2
 * 功能描述：系统拨出率
 * **/
namespace BLL
{
    public class BochulvBLL
    {
        BochulvDAL bochulvDAL = new BochulvDAL();
        /// <summary>
        /// 获取播出率所有信息
        /// </summary>
        /// <returns></returns>
        public IList<BochulvModel> GetBochulv()
        {
            return bochulvDAL.GetBochulv();
        }
    }
}

