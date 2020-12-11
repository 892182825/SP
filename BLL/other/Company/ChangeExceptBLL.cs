using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace BLL.other.Company
{
    public class ChangeExceptBLL
    {
        ChangeExceptDAL changeExceptDAL = new ChangeExceptDAL();

        /// <summary>
        /// 获得所有期数
        /// </summary>
        public List<int> GetExcept()
        {
            return changeExceptDAL.GetExceptList();
        }

        /// <summary>
        /// 对期数的各种判断
        /// </summary>
        /// <returns></returns>
        public string Check(string orderId, string number, int exceptNum)
        {
            if (ChangeExceptDAL.SelectMemberAndOrder(orderId, number) == 0)
            {
                return BLL.Translation.Translate("006028", "对不起，您输入的订单号或会员编号有误！"); 
            }
            //该会员注册期数
            int memberRegisExce = changeExceptDAL.GetRegisExce(number);

            //获取报单期数
            int orderExcept = changeExceptDAL.GetOrderExcept(orderId);

            //获取报单是否是首次报单
            int isfirst = ChangeExceptDAL.SelectIsAgain(orderId);

            //获取当前期
            int exceptNow = CommonDataDAL.getMaxqishu();

            //判断是否又修改到了当前期
            if (exceptNum == exceptNow)
            {
                return BLL.Translation.Translate("005784", "当前期报单不能调整到当前期！"); 
            }

            //只能修改当前期
            if (orderExcept != exceptNow)
            {
                return BLL.Translation.Translate("002069", "只能调整当前期报单期数！"); 
            }

            if (isfirst == 0)
            {
                //首次报单 不能改到推荐
                if (exceptNum < ChangeExceptDAL.SelectTJQiShu(number) || exceptNum<ChangeExceptDAL.SelectAZQiShu(number))
                {
                    return BLL.Translation.Translate("005791", "首次报单不可以调整到推荐或安置人注册期数之前！");
                }

            }
            else
            {
                //如果该报单期数小于该会员注册期数
                if (memberRegisExce > exceptNum)
                {
                    return BLL.Translation.Translate("002070", "报单期数不能小于该会员注册期数！");
                }
            }

            return "";
        }

        /// <summary>
        /// 更改该报单的期数
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="except"></param>
        /// <returns></returns>
        public bool UpdateExcept(string orderId, int except, string user, string ip, string updateExpectReason)
        {
            try
            {
                changeExceptDAL.UptOrderExcept(except, orderId, user, ip,updateExpectReason);
            }
            catch 
            {
                return false; 
            }
            return true;
        }
    }
}
