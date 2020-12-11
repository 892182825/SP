using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

///Add Namespace
using Model;

/*
 *
 * 创建时间：09/09/12
 * 修改者：  汪  华
 * 修改时间：2009-10-08
 * 说明：    库位表
 */
namespace DAL
{
    public class DepotSeatDAL
    {
        /// <summary>
        /// 根据仓库id返会库位名称，返回的是一个xml格式的字符串
        /// </summary>
        /// <param name="WareHouseID"></param>
        /// <returns></returns>
        //public string GetDepotSeat(string WareHouseID)
        //{
        //    SqlDataReader dr = DBHelper.ExecuteReader("select SeatName from dbo.DepotSeat where WareHouseID='"+WareHouseID+"' for xml raw('Row'),elements,root('Root')");

        //    dr.Read();

        //    string xmlstr=dr[0].ToString();

        //    dr.Close();

        //    return xmlstr;
        //}

        /// <summary>
        /// 向库位表中插入记录
        /// </summary>
        /// <param name="depotSeatModel">库位模型</param>
        /// <returns>返回向库位表中插入记录所影响的行数</returns>
        public static int AddDepotSeat(DepotSeatModel depotSeatModel)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@wareHouseID",SqlDbType.Int),
                new SqlParameter("@depotSeatID",SqlDbType.Int),
                new SqlParameter("@seatName",SqlDbType.VarChar,50),
                new SqlParameter("@remark",SqlDbType.VarChar,200)
            };
            sparams[0].Value = depotSeatModel.WareHouseID;
            sparams[1].Value = depotSeatModel.DepotSeatID;
            sparams[2].Value = depotSeatModel.SeatName;
            sparams[3].Value = depotSeatModel.Remark;

            return DBHelper.ExecuteNonQuery("AddDepotSeat", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 删除指定的库位信息
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns>返回删除指定的库位信息所影响的行数</returns>
        public static int DelDepotSeatByID(int id)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@id",SqlDbType.Int)
            };
            sparams[0].Value = id;

            return DBHelper.ExecuteNonQuery("DelDepotSeatByID", sparams, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 获得当前仓库的库位数量
        /// </summary>
        /// <param name="WareHoseID">仓库ID
        /// <returns></returns>
        public static int getDepotSeatCountByWareHoseID(int WareHoseID)
        {
            string sql = "select count(*) from DepotSeat where WareHouseID=@WareHouseID";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@WareHouseID",WareHoseID)};
            object obj = DBHelper.ExecuteScalar(sql,parm,CommandType.Text);
            if (obj == null)
                return 0;
            else
                return int.Parse(obj.ToString());
        }
        /// <summary>
        /// 删除指定的库位信息根据仓库编号
        /// </summary>
        /// <param name="id">主键ID</param>
        public static int DelDepotSeatByWareHouse(int WareHouseID)
        {
            string sql = @"delete DepotSeat where WareHouseID=@WareHouseID";
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@WareHouseID",SqlDbType.Int)
            };
            sparams[0].Value = WareHouseID;

            return DBHelper.ExecuteNonQuery(sql, sparams, CommandType.Text);
        }


        /// <summary>
        /// 更新指定的库位信息
        /// </summary>
        /// <param name="depotSeatModel">库位模型</param>
        /// <returns>返回更新指定的库位信息所影响的行数</returns>
        public static int UpdDepotSeatByID(DepotSeatModel depotSeatModel)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@id",SqlDbType.Int),
                new SqlParameter("@wareHouseID",SqlDbType.Int),
                new SqlParameter("@seatName",SqlDbType.VarChar,50),
                new SqlParameter("@remark",SqlDbType.VarChar,200)
            };

            sparams[0].Value = depotSeatModel.ID;
            sparams[1].Value = depotSeatModel.WareHouseID;
            sparams[2].Value = depotSeatModel.SeatName;
            sparams[3].Value = depotSeatModel.Remark;

            return DBHelper.ExecuteNonQuery("UpdDepotSeatByID", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 获取最大的库位ID
        /// </summary>
        /// <returns>返回最大的库位ID</returns>
        public static int GetMaxDepotSeatIDFromDepotSeat()
        {
            return Convert.ToInt32(DBHelper.ExecuteScalar("GetMaxDepotSeatIDFromDepotSeat", CommandType.StoredProcedure));
        }

        public static DataTable GetDepotSeat(string WareHouseID)
        {
            SqlParameter[] sPara = new SqlParameter[] { new SqlParameter("@num", SqlDbType.Int) };
            sPara[0].Value = Convert.ToInt32(WareHouseID);

            return DBHelper.ExecuteDataTable("select ID,DepotSeatID,SeatName from dbo.DepotSeat where WareHouseID=@num",sPara,CommandType.Text);
        }

        public static DataTable GetDepotSeats(string WareHouseID)
        {
            SqlParameter[] sPara = new SqlParameter[] { new SqlParameter("@num", SqlDbType.Int) };
            sPara[0].Value = Convert.ToInt32(WareHouseID);
            return DBHelper.ExecuteDataTable("select DepotSeatID ID,SeatName Name from dbo.DepotSeat where WareHouseID=@num", sPara, CommandType.Text);
        }

        /// <summary>
        /// 获取库位信息
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetDepotSeatInfo()
        {            
            return DBHelper.ExecuteDataTable("GetDepotSeatInfo", CommandType.StoredProcedure);
        }

        /// <summary>
        /// 获取指定的库位信息
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetDepotSeatInfoByID(int id)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@id",SqlDbType.Int)
            };
            sparams[0].Value = id;
            
            return DBHelper.ExecuteDataTable("GetDepotSeatInfoByID", sparams, CommandType.StoredProcedure);
        }
        
        /// <summary>
        /// 通过仓库ID获取库位信息
        /// </summary>
        /// <param name="wareHouseID">仓库ID</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetDepotSeatInfoByWareHouaseID(int wareHouseID)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@wareHouseID",SqlDbType.Int)
            };
            sparams[0].Value = wareHouseID;
            
            return DBHelper.ExecuteDataTable("GetDepotSeatInfoByWareHouaseID",sparams,CommandType.StoredProcedure);
        }

        /// <summary>
        /// 获取指定的库位名称的行数
        /// </summary>
        /// <param name="depotSeatModel">库位模型</param>
        /// <returns>返回指定的库位名称的行数</returns>
        public static int GetSeatNameCountByWareHouseIDSeatName(DepotSeatModel depotSeatModel)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@wareHouseID",SqlDbType.Int),
                new SqlParameter("@seatName",SqlDbType.VarChar,50)
            };
            sparams[0].Value = depotSeatModel.WareHouseID;
            sparams[1].Value = depotSeatModel.SeatName;
                        
            return Convert.ToInt32(DBHelper.ExecuteScalar("GetSeatNameCountByWareHouseIDSeatName", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// 获取指定的库位名称的行数
        /// </summary>
        /// <param name="depotSeatModel">库位模型</param>
        /// <returns>返回指定的库位名称的行数</returns>
        public static int GetSeatNameCountByIDWareHouseIDSeatName(DepotSeatModel depotSeatModel)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@id",SqlDbType.Int),
                new SqlParameter("@wareHouseID",SqlDbType.Int),
                new SqlParameter("@seatName",SqlDbType.VarChar,50)
            };
            sparams[0].Value = depotSeatModel.ID;
            sparams[1].Value = depotSeatModel.WareHouseID;
            sparams[2].Value = depotSeatModel.SeatName;

            return (int)DBHelper.ExecuteScalar("GetSeatNameCountByIDWareHouseIDSeatName", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 获取对应库位xml字符串
        /// </summary>
        /// <returns></returns>
        public static string GetDepotSeatXML(string idvalue)
        {
            string cmd = "select DepotSeatID,SeatName from dbo.DepotSeat where WareHouseID=@num";
            SqlParameter[] sPara = new SqlParameter[] { new SqlParameter("@num", SqlDbType.Int) };
            sPara[0].Value = Convert.ToInt32(idvalue);

            SqlDataReader dr = DBHelper.ExecuteReader(cmd,sPara,CommandType.Text);

            string xmlstr = "<?xml version='1.0' encoding='utf-8' ?><Root>";

            while (dr.Read())
            {
                xmlstr = xmlstr + "<Row><DepotSeatID>" + dr["DepotSeatID"] + "</DepotSeatID><SeatName>" + dr["SeatName"] + "</SeatName></Row>";
            }
            xmlstr = xmlstr + "</Root>";

            dr.Close();

            return xmlstr;
        }

        /// <summary>
        /// Judge the DepotSeatId whether has operation by Id before delete
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Return the counts of the DepotSeat by Id</returns>
        public static int DepotSeatIdWhetherHasOperation(int Id)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@Id",SqlDbType.Int)
            };
            sparams[0].Value = Id;

            return Convert.ToInt32(DBHelper.ExecuteScalar("DepotSeatIdWhetherHasOperation", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// Judge the DepotSeatId whether exist by Id before delete or update
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Return the counts of the DepotSeat by Id</returns>
        public static int DepotSeatIdIsExist(int Id)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@Id",SqlDbType.Int)
            };
            sparams[0].Value = Id;

            return Convert.ToInt32(DBHelper.ExecuteScalar("DepotSeatIdIsExist", sparams, CommandType.StoredProcedure));
        }
        /// <summary>
        /// 获取库位名
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Return the counts of the WareHouse by Id</returns>
        public static string DepotSeatEName(int Id)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@DepotSeat",SqlDbType.Int)
            };
            sparams[0].Value = Id;
            return (DBHelper.ExecuteScalar("select seatname from DepotSeat where depotseatid=@DepotSeat", sparams, CommandType.Text).ToString());
        } 
    }
}
