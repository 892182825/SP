using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DAL.Branch;
using System.Web.UI.WebControls;
using Model.Branch;
using System.Data.SqlClient;
namespace BLL.Branch
{
    /// <summary>
    /// 分公司基本信息
    /// </summary>
    public class BranchModifyBLL
    {

        /// <summary>
        /// 获取国家
        /// </summary>
        /// <param name="cityCode"></param>
        /// <returns></returns>
        public static string GetCity(string cityCode)
        {
           return BranchModifyDAL.GetCity(cityCode);
        }
        /// <summary>
        /// 判断该分公司是不是可以删除
        /// </summary>
        /// <param name="numnber"></param>
        /// <returns> 0 表示没有数据 1 表示有数据（不能删除）</returns>
        public static Boolean GetBranchDel(string numnber)
        {
            return BranchModifyDAL.GetBranchDel(numnber);
        }

        /// <summary>
        /// 绑定银行国家
        /// </summary>
        /// <returns></returns>
        public static List<ListItem> GetBankcity(string number)
        {
            return BranchModifyDAL.GetBankcity(number);
        }

        /// <summary>
        /// 绑定银行国家
        /// </summary>
        /// <returns></returns>
        public static List<ListItem> GetBankcity()
        {
            return BranchModifyDAL.GetBankcity();
        }

         /// <summary>
        /// 获取分公司信息
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static BranchModel GetBreach(string number)
        {
            return BranchModifyDAL.GetBreach(number);
        }

         /// <summary>
        /// 修改分公司休息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static Boolean UpbranchModify(BranchModel model)
        {
            return BranchModifyDAL.UpbranchModify(model);
        }
          /// <summary>
        /// 判断该分公司编号是否存在
        /// </summary>
        /// <param name="numnber"></param>
        /// <returns> 0 表示没有数据 1 表示有数据（不能删除）</returns>
        public static Boolean GetBranchNumber(SqlTransaction tran, string numnber)
        {
            return BranchModifyDAL.GetBranchNumber(tran,numnber);
        }
          /// <summary>
        /// 添加分公司休息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static Boolean AddbranchModify(SqlTransaction tran, BranchModel model)
        {
            return BranchModifyDAL.AddbranchModify(tran, model);
        }

          /// <summary>
        /// 删除分公司
        /// </summary>
        /// <param name="numnber"></param>
        /// <returns> 0 删除失败 1 删除成功</returns>
        public static Boolean DELBranchNumber(string number)
        {
            return BranchModifyDAL.DELBranchNumber(number);
        }
    }
}
