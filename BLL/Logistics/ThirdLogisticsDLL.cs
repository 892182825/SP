using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Model.Other;
using DAL;
using System.Data;
using BLL.other.Company;

namespace BLL.Logistics
{
    public class ThirdLogisticsDLL
    {
        /// <summary>
        /// 第三方物流
        /// 
        /// </summary>
        /// <returns></returns>
        ThirdLogisticsDAL thirdLogisticsDAL = new ThirdLogisticsDAL();
        /// <summary>
        /// 添加第三方物流公司
        /// </summary>
        /// <returns></returns>
        public Boolean AddLogistics(LogisticsModel logisticsModel)
        {

            return thirdLogisticsDAL.AddLogistics(logisticsModel) == 0 ? false : true;
        }
         /// <summary>
        /// 根据编号查询物流公司备注
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public string GetRemarkById(string id)
        {
            return thirdLogisticsDAL.GetRemarkById(id);
        }
        /// <summary>
        /// 杳看物流公司
        /// </summary>
        /// <param name="country"></param>
        /// 
        /// <returns></returns>   
        public DataTable GetThirdLogistics(string country)
        {
            return thirdLogisticsDAL.GetThirdLogistics(country);
        }
        // <summary>
        /// 根据ID删除物流公司
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>   
        public Boolean DelThirdLogistics(int id)
        {
            BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("Logistics", "ltrim(rtrim(id))");

            cl_h_info.AddRecord(id);

            cl_h_info.DeletedIntoLogs(BLL.CommonClass.ChangeCategory.company9, id.ToString(), BLL.CommonClass.ENUM_USERTYPE.objecttype8);

            return thirdLogisticsDAL.DelThirdLogistics(id) == 0 ? false : true;
        }
        /// <summary>
        /// 根据ID修改物流公司
        /// </summary>
        /// <param name="item"></param>
        /// 
        /// <returns></returns>   
        public Boolean UpdateThirdLogistics(LogisticsModel logisticsModel, int id)
        {
            BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("Logistics", "ltrim(rtrim(id))");

            cl_h_info.AddRecord(id);

            bool bb= thirdLogisticsDAL.UpdateThirdLogistics(logisticsModel, id) == 0 ? false : true;

            cl_h_info.AddRecord(id);

            cl_h_info.ModifiedIntoLogs(BLL.CommonClass.ChangeCategory.company9, id.ToString(), BLL.CommonClass.ENUM_USERTYPE.objecttype8);

            return bb;
        }
        /// <summary>
        /// 得到物流公司信息用于初始化显示
        /// </summary>
        /// <param name="item"></param>
        /// 
        /// <returns></returns>
        public LogisticsModel GetThirdLogisticsInit(int id)
        {
            return thirdLogisticsDAL.GetThirdLogisticsInit(id);
        }

          //绑定银行
        public IList<MemberBankModel> BindBank_List()
        {
            return thirdLogisticsDAL.BindBank_List();
        }
            //查询是否编号已存在
        public int CheckLogisticsNumIsUse(string number)
        {
            return thirdLogisticsDAL.CheckLogisticsNumIsUse(number);
        }
        /// <summary>
        /// 修改第三方物流信息
        /// </summary>
        /// <param name="logisticsModel">第三方物流基本信息</param>
        /// <param name="id">第三方物流编号</param>
        /// <param name="pass">第三方物流对应管理密码</param>
        /// <param name="warehouse">第三方物流权限仓库编号字符</param>
        /// <returns></returns>
        public int UpdateThirdLogistics(LogisticsModel logisticsModel, int id, string pass, string warehouse)
        {
            LogisticsModel model = thirdLogisticsDAL.GetThirdLogisticsInit(id);
            if (model.Number != logisticsModel.Number)
            {
                if (!ManagerBLL.CheckNumber(logisticsModel.Number))
                {
                    //标识已存在相同编号的管理员不可修改
                    return -2;
                }
            }
            return ThirdLogisticsDAL.UptLogistics(logisticsModel, pass, warehouse, id);
        }

        /// <summary>
        /// 添加第三方物流信息
        /// </summary>
        /// <param name="logisticsModel">第三方物流基本信息</param>
        /// <param name="pass">第三方物流对应管理密码</param>
        /// <param name="warehouse">第三方物流权限仓库编号字符</param>
        /// <returns></returns>
        public int AddLogistics(LogisticsModel logisticsModel, string pass, string warehouse)
        {
            if (!ManagerBLL.CheckNumber(logisticsModel.Number))
            {
                //标识已存在相同编号的管理员不可修改
                return -2;
            }
            return 0;//ThirdLogisticsDAL.AddLogistics(logisticsModel, pass, warehouse);
        }
    }
}
