using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Model;

namespace DAL
{
   public class AddBMOrderDAL
    {
        /// <summary>
        ///  充值信息记录到数据库
        /// </summary>
        /// <param name="model"></param>
        /// <returns>返回成功与否</returns> 
        public int AddBMOrder(BMOrder model)
        {
            SqlParameter[] para = 
							{	
							    new SqlParameter("@ourterTid", model.OurterTid),
								new SqlParameter("@orderTime", model.OrderTime),
								new SqlParameter("@operateTime", model.OperateTime),
								new SqlParameter("@Number",model.Number),
								new SqlParameter("@rechargeAccount", model.RechargeAccount),
								new SqlParameter("@itemName", model.ItemName),
								new SqlParameter("@itemNum", model.ItemNum),
								new SqlParameter("@saleAmount", model.SaleAmount),
								new SqlParameter("@EPmny",  model.EPmny ),
								new SqlParameter("@hl", model.Hl),
								new SqlParameter("@billId" , model.BillId),						
								new SqlParameter("@revokeMessage" ,model.RevokeMessage),
								new SqlParameter("@rechargeState", model.RechargeState),
								new SqlParameter("@OuterType", model.OuterType)
								
						    
							};
            return DBHelper.ExecuteNonQuery("AddBMOrder", para, CommandType.StoredProcedure);

        }


        /// <summary>
        ///  买票信息记录到数据库
        /// </summary>
        /// <param name="model"></param>
        /// <returns>返回成功与否</returns> 
        public int AddBMOrderPW(BMOrderPW model)
        {
            SqlParameter[] para = 
							{	
							    new SqlParameter("@ourterTid", model.OurterTid),
								new SqlParameter("@ctime", model.Ctime),
								new SqlParameter("@etime", model.Etime),
								new SqlParameter("@Number",model.Number),
								new SqlParameter("@itemId", model.ItemId),
								new SqlParameter("@passengerName", model.PassengerName),
								new SqlParameter("@passengerTel", model.PassengerTel),
								new SqlParameter("@startTime", model.StartTime),
								new SqlParameter("@startStation" , model.StartStation),						
								new SqlParameter("@recevieStation" ,model.RecevieStation),
								new SqlParameter("@flightCompanyName", model.FlightCompanyName),
                                new SqlParameter("@depTime" , model.DepTime),						
								new SqlParameter("@arriTime" ,model.ArriTime),
								new SqlParameter("@flightCompanyCode", model.FlightCompanyCode),
                                new SqlParameter("@flightNo" , model.FlightNo),						
								new SqlParameter("@seatMsg" ,model.SeatMsg),
								new SqlParameter("@seatStatus", model.SeatStatus),
								new SqlParameter("@parPrice", model.ParPrice),
                                new SqlParameter("@EPmny",  model.EPmny ),
								new SqlParameter("@hl", model.Hl),
                                new SqlParameter("@totalPayCash",  model.TotalPayCash ),
								new SqlParameter("@orderType", model.OrderType),
                                new SqlParameter("@title", model.Title),
								
						    
							};
            return DBHelper.ExecuteNonQuery("AddBMOrderPW", para, CommandType.StoredProcedure);

        }
    }
}
