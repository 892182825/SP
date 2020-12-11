using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Model;

namespace BLL.MoneyFlows
{
   public class BMOrderBLL
    {

       AddBMOrderDAL aod = new AddBMOrderDAL();
        /// <summary>
        /// 充值信息记录到数据库
        /// </summary>
        /// <returns></returns>
       public int AddBMOrder(BMOrder model)
        {
            return aod.AddBMOrder(model);
        }

       /// <summary>
       /// 充值信息记录到数据库
       /// </summary>
       /// <returns></returns>
       public int AddBMOrderPW(BMOrderPW model)
       {
           return aod.AddBMOrderPW(model);
       }
    }
}
