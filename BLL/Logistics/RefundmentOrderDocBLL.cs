using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Logistics;
using DAL.Logistics;
using System.Data;
using System.Data.SqlClient;
using Model;
using BLL.CommonClass;

namespace BLL.Logistics
{
    public class RefundmentOrderDocBLL
    {
        public RefundmentOrderDocBLL()
        {
        }

        /// <summary>
        /// 添加会员退货单记录
        /// </summary>
        /// <param name="refundmentOrder"></param>
        /// <returns></returns>
        public bool AddRefundmentOrderDoc(RefundmentOrderDocModel refundmentOrder, ref string msg)
        {
            try
            {
                RefundmentOrderDocDAL rdo = new RefundmentOrderDocDAL();
                return rdo.AddRefundmentOrderDoc(refundmentOrder, ref msg);

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 审核会员退货审核单
        /// </summary>
        /// <param name="refundmentOrder"></param>
        /// <returns></returns>
        public bool AuditRefundmentOrderDoc(RefundmentOrderDocModel refundmentOrder, ref string msg)
        {
            try
            {
                RefundmentOrderDocDAL rdo = new RefundmentOrderDocDAL();
                return rdo.AuditRefundmentOrderDoc(refundmentOrder, ref msg);

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 获取会员退货单
        /// </summary>
        /// <param name="DocID"></param>
        /// <returns></returns>
        public RefundmentOrderDocModel GetRefundmentOrderDocByDocID(string DocID, ref string msg)
        {

            try
            {
                RefundmentOrderDocDAL rdo = new RefundmentOrderDocDAL();
                return rdo.GetRefundmentOrderDocByDocID(DocID, ref msg);

            }
            catch (Exception ex)
            {
                return null;
            }

        }
        /// <summary>
        /// 根据地址信息，获取详细的国家，省份，城市，邮编，国家代码，国家简称等信息
        /// </summary>
        /// <param name="cpccode"></param>
        /// <returns></returns>
        public DataTable GetCountryCityByCPCCode(string cpccode)
        {
            try
            {
                RefundmentOrderDocDAL rdo = new RefundmentOrderDocDAL();
                return rdo.GetCountryCityByCPCCode(cpccode);

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取退货单的退单详细 DocID,OriginalDocID,ProductID ,ProductCode ,ProductName ,UnitPrice ,UnitPV,QuantityReturning,LeftQuantity,OrderQuantity
        /// </summary>
        /// <returns></returns>
        public DataTable GetRefundmentOrderDetailsByDocID(string docid)
        {
            try
            {
                RefundmentOrderDocDAL rdo = new RefundmentOrderDocDAL();
                return rdo.GetRefundmentOrderDetailsByDocID(docid);

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 锁定退货单
        /// </summary>
        /// <param name="docid"></param>
        /// <returns></returns>
        public bool LuckRefundmentOrderDoc(string docid)
        {
            try
            {
                RefundmentOrderDocDAL rdo = new RefundmentOrderDocDAL();
                return rdo.LuckRefundmentOrderDoc(docid);

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 解锁退货单
        /// </summary>
        /// <param name="docid"></param>
        /// <returns></returns>
        public bool UnluckRefundmentOrderDoc(string docid)
        {
            try
            {
                RefundmentOrderDocDAL rdo = new RefundmentOrderDocDAL();
                return rdo.UnluckRefundmentOrderDoc(docid);

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 删除未审核的退货单
        /// </summary>
        /// <param name="docid"></param>
        /// <returns></returns>
        public bool DelRefundmentOrderDoc(string docid)
        {
            try
            {
                RefundmentOrderDocDAL rdo = new RefundmentOrderDocDAL();
                return rdo.DelRefundmentOrderDoc(docid);

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 会员退货审核时，添加一条汇款记录
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="info"></param>
        /// <returns></returns>

        public int AddDataTOMemberRemittancesFromRefundOrder(SqlTransaction tran, RemittancesModel info)
        {
            try
            {
                RefundmentOrderDocDAL rdo = new RefundmentOrderDocDAL();
                return rdo.AddDataTOMemberRemittancesFromRefundOrder(tran, info);

            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        /// <summary>
        /// 获得会员退货单单据编号
        /// </summary>
        /// <returns></returns>
        public string CreateRefundmentOrderDocIdByTypeCode()
        {
            try
            {
                RefundmentOrderDocDAL rdo = new RefundmentOrderDocDAL();
                return rdo.CreateRefundmentOrderDocIdByTypeCode();

            }
            catch (Exception ex)
            {
                return string.Empty;
            }

        }

        /// <summary>
        /// 创建会员汇款单号M+YYMMDD{ID}
        /// </summary>
        /// <returns></returns>
        public string CreateMemberRemittancesID()
        {
            try
            {
                RefundmentOrderDocDAL rdo = new RefundmentOrderDocDAL();
                return rdo.CreateMemberRemittancesID();

            }
            catch (Exception ex)
            {
                return string.Empty;
            }

        }


        /// <summary>
        /// 填写会员退货退款单
        /// </summary>
        /// <param name="refundmentOrder"></param>
        /// <param name="isEdit"></param>
        /// <param name="OperateBh"></param>
        /// <param name="OperateIP"></param>
        /// <param name="qishu"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool UpdateRefundmentOrderDocForBill(RefundmentOrderDocModel refundmentOrder, bool isEdit, ref string msg)
        {
            try
            {
                RefundmentOrderDocDAL rdo = new RefundmentOrderDocDAL();
                return rdo.UpdateRefundmentOrderDocForBill(refundmentOrder, isEdit, CommonDataBLL.OperateBh, CommonDataBLL.OperateIP, CommonDataBLL.getMaxqishu(), ref msg);

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}