using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
   public  class AuditingMemberagainDAL
    {
       public AuditingMemberagainDAL() { }

       /// <summary>
       /// 删除 当期,未支付的自由注册 信息
       /// </summary>
       /// <returns>返回 0 失败</returns>
       //public string DeleteCurretMemberinfo(string Number,int ExceptNum,string OrderID,string StoreID)
       //{
       //    string info = "";
       //    try { 
           
       //    using (SqlConnection con = new SqlConnection(DBHelper.connString))
       //    {
       //        con.Open();
       //        SqlTransaction tran = con.BeginTransaction();

       //        //判断该会员是否有复效单
       //        int result = new BrowsememberordersDAL().GetHasOrderAgain(Number);
       //        if (result > 0)
       //        {
       //            info = "抱歉！该会员有重复消费请先删除重复消费!";
       //            tran.Rollback();
       //            return info;

       //        }

       //        SqlParameter[] parms =
       //                         {
       //                             new SqlParameter ("@Number" , SqlDbType .VarChar ,20) ,
       //                             new SqlParameter ("@ExpectNum"   , SqlDbType .Int ) 
									
       //                         };

       //        parms[0].Value = Number;
       //        parms[1].Value = ExceptNum;
       //        //业绩和网络处理 返回-1 ：事务回滚  0：未调整网络图和业绩
       //         result = DBHelper.ExecuteNonQuery(tran, "js_delnew", parms, CommandType.StoredProcedure);

       //         if (result > 0)
       //         {
       //             SqlParameter[] parm = {new SqlParameter("@OrderID", SqlDbType.VarChar,15),
       //                               new SqlParameter("@StoreID", SqlDbType.VarChar,10)};
       //             parm[0].Value = OrderID;
       //             parm[1].Value = StoreID;
       //             //删除memberorder信息
       //             result = DBHelper.ExecuteNonQuery(tran, "delete_h_order", parm, CommandType.StoredProcedure);

       //             if (result > 0)
       //                 tran.Commit();
       //             else
       //             {
       //                 info = " 删除【memberorder】信息失败！  ";
       //                 tran.Rollback();
       //             } 
       //         }
       //         else
       //         {
       //             info = " 不允许删除！  ";
       //             tran.Rollback();
       //         } 

               

       //    }
       //    }
       //    catch { }
       //    return info;

       //}


       public string DeleteCurretMemberinfo(string Number, int ExceptNum, string OrderID, string StoreID)
       {
                string info = "";          

               using (SqlConnection con = new SqlConnection(DBHelper.connString))
               {
                   con.Open();
                   SqlTransaction tran = con.BeginTransaction();

                   try
                    {

                       //						/* 删除自由注册会员时的操作---删除会员及所有些会员的订单(由于是注册时购买的)
                       //						 * delete the memberinfo table
                       //						 * delete the memberorder table that the member order in the talbe
                       //						 * delete the memberdetails table about with the memberorder.
                       //						 * 调用js_delnew
                       
                      
                       SqlParameter[] parms =
								    {
									    new SqlParameter ("@Number" , SqlDbType .VarChar ,20) ,
									    new SqlParameter ("@ExpectNum"   , SqlDbType .Int ) 
    									
								    };

                       parms[0].Value = Number;
                       parms[1].Value = ExceptNum;
                       //业绩和网络处理 返回-1 ：事务回滚  0：未调整网络图和业绩
                       DBHelper.ExecuteNonQuery(tran, "js_delnew", parms, CommandType.StoredProcedure);

                           SqlParameter[] parm = {new SqlParameter("@OrderID", SqlDbType.VarChar,15),
									      new SqlParameter("@StoreID", SqlDbType.VarChar,10)};
                           parm[0].Value = OrderID;
                           parm[1].Value = StoreID;
                           //删除memberorder信息
                           DBHelper.ExecuteNonQuery(tran, "delete_h_order", parm, CommandType.StoredProcedure);

                      tran.Commit();
                    }
                   catch { tran.Rollback(); info = "删除失败！！"; }


               }
          
           return info;

       }
    }
}
