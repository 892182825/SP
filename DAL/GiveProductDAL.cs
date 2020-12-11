using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class GiveProductDAL
    {
        /// <summary>
        /// 添加赠送产品信息
        /// </summary>
        /// <param name="tran">处理事务</param>
        /// <param name="tobjopda_depotManageDoc">某种单据明细类对象数组</param>
        /// <returns>返回插入所影响的行数</returns>
        public static int CreateGiveProducts(SqlTransaction tran, ArrayList giveProductList,int setGivePVID)
        {
            string sql = @"INSERT INTO GiveProduct
										(ProductID,Price,PV, 
										ProductQuantity,TotalPrice,TotalPV,SetGivePVID) 
										 VALUES  (@ProductID,@Price,@PV, 
										@ProductQuantity, @TotalPrice,@TotalPV,@SetGivePVID)";
            SqlParameter[] objPara ={
									   new SqlParameter("@ProductID",SqlDbType.Int  ),
                                       new SqlParameter("@Price", SqlDbType.Money ),
									   new SqlParameter("@PV" , SqlDbType.Money),
									   new SqlParameter("@ProductQuantity",SqlDbType.Int),
									   new SqlParameter("@TotalPrice", SqlDbType.Money ),
									   new SqlParameter("@TotalPV", SqlDbType.Money),
                                       new SqlParameter("@SetGivePVID",SqlDbType.Int)
								   };

            int count = 0;

            foreach (GiveProductModel giveProduct in giveProductList)
            {
                objPara[0].Value = giveProduct.productId;
                objPara[1].Value = giveProduct.Price;
                objPara[2].Value = giveProduct.PV;
                objPara[3].Value = giveProduct.ProductQuantity;
                objPara[4].Value = giveProduct.TotalPrice;
                objPara[5].Value = giveProduct.TotalPV;
                objPara[6].Value = setGivePVID;
                DBHelper.ExecuteNonQuery(tran, sql, objPara, CommandType.Text);
                count++;
            }
            return count;
        }
        /// <summary>
        /// 添加赠送pv范围记录
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="setGiveModel"></param>
        /// <returns></returns>
        public static int AddSetGivePV(SqlTransaction tran,SetGivePVModel setGiveModel)
        {
            string sql = @"INSERT INTO SetGivePV
										(totalpvStart,totalpvEnd,operatenum,operateip) 
										 VALUES  (@totalpvStart,@totalpvEnd,@operatenum,@operateip);set @addID=@@identity";
            SqlParameter[] objPara ={
									   new SqlParameter("@totalpvStart",SqlDbType.Float  ),
                                       new SqlParameter("@totalpvEnd",SqlDbType.Float  ),
                                       new SqlParameter("@operatenum", SqlDbType.NVarChar ),
									   new SqlParameter("@operateip" , SqlDbType.NVarChar),
                                       new SqlParameter("@addID",SqlDbType.Int)
								   };

            objPara[0].Value = setGiveModel.TotalPVStart;
            objPara[1].Value = setGiveModel.TotalPVEnd;
            objPara[2].Value = setGiveModel.OperateNum;
            objPara[3].Value = setGiveModel.OperateIP;
            objPara[4].Value = 0;
            objPara[4].Direction = ParameterDirection.Output;
            DBHelper.ExecuteNonQuery(tran, sql, objPara, CommandType.Text);
             
            return Convert.ToInt32(objPara[4].Value);
        }
        /// <summary>
        /// 获取最后一条结束PV值
        /// </summary>
        /// <returns></returns>
        public static bool GetLastEndPV(double totalpvStart)
        {
            SqlParameter[] objPara ={
									   new SqlParameter("@totalpvStart",SqlDbType.Int  )
								   };
            objPara[0].Value = totalpvStart;
            object lstEndPV = DBHelper.ExecuteScalar(" select id from setGivePV where totalpvStart>@totalpvStart and totalpvEnd<@totalpvStart union select id from setGivePV where (totalpvStart=@totalpvStart or totalpvEnd=@totalpvStart)",objPara,CommandType.Text);
            if (lstEndPV != null & lstEndPV != DBNull.Value)
            {
                return true;
            }
            return false;
        }

        public static DataTable GetDtBYPv(double pv)
        {
            DataTable dt = null;
            object pvId = DBHelper.ExecuteScalar("select isnull(Id,-1) as id from SetGivePV where " + pv + " -totalpvStart>=0 and totalpvEnd-" + pv + ">=0");
            if (pvId!=null)
            {
                if (pvId.ToString()!="-1")
                {
                    dt = DBHelper.ExecuteDataTable("select g.productId,productCode,productName,productImage,g.price,g.pv,g.productQuantity,totalPrice,totalPv from product p,GiveProduct g where g.productId=p.productId and setgivepvid=" + pvId.ToString());
                }
            }

            return dt;
        }
        /// <summary>
        /// 获取赠送PV设置记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataTable GetSetGivePVByID(int id)
        { 
            SqlParameter[] objPara ={
									   new SqlParameter("@id",SqlDbType.Int  )
								   };
            objPara[0].Value = id;
            return DBHelper.ExecuteDataTable("select totalpvStart,totalpvEnd,operatenum,operatetime from SetGivePV where id=@id",objPara,CommandType.Text);
        }
        /// <summary>
        /// 获取对应赠送pv范围之内的产品
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static DataTable GetGiveProductBySID(int sid)
        {
            SqlParameter[] objPara ={
									   new SqlParameter("@sid",SqlDbType.Int  )
								   };
            objPara[0].Value = sid;
            return DBHelper.ExecuteDataTable("select g.productId,productCode,productName,productImage,g.price,g.pv,g.productQuantity,totalPrice,totalPv from product p,GiveProduct g where g.productId=p.productId and setgivepvid=@sid", objPara, CommandType.Text);
        }
        /// <summary>
        /// 删除赠送PV设置
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DelSetSetGivePVByID(SqlTransaction tran,int id)
        {
            SqlParameter[] objPara ={
									   new SqlParameter("@id",SqlDbType.Int  )
								   };
            objPara[0].Value = id;
            return DBHelper.ExecuteNonQuery(tran, "delete from SetGivePV where id=@id",objPara, CommandType.Text);
        }
        /// <summary>
        /// 删除相应的赠送产品记录
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static int DelGiveProductBySID(SqlTransaction tran, int sid)
        {
            SqlParameter[] objPara ={
									   new SqlParameter("@sid",SqlDbType.Int  )
								   };
            objPara[0].Value = sid;
            return DBHelper.ExecuteNonQuery(tran, "delete from GiveProduct where SetGivePVID=@sid",objPara,CommandType.Text);
        }
    }
}
