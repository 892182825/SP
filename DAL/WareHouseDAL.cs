using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web;
/*
 * 
 * 创建时间：   09/08/31
 * 修改者：     汪  华
 * 修改时间：   2009-09-08
 * 文件名：     WareHouseDAL
 * 功能：       对仓库信息表 增，删，改，查
 * 
 */
namespace DAL
{
    public class WareHouseDAL
    {
        /// <summary>
        /// 添加仓库信息
        /// </summary>
        /// <param name="psm">仓库模型</param>
        /// <returns>返回添加仓库模型所影响的行数</returns>
        public static int AddWareHouse(WareHouseModel psm)
        {
            string cmd = @"insert into WareHouse(CountryCode,WareHouseName,WareHouseForShort,WareHousePrincipal,WareHouseAddress,WareHouseDescr,WareControl,CPCCode) values 
                        (@CountryCode,@WareHouseName,@WareHouseForShort,@WareHousePrincipal,@WareHouseAddress,@WareHouseDescr,@WareControl,@CPCCode)";

            SqlParameter[] param = new SqlParameter[] 
            {
                new SqlParameter("@CountryCode",psm.CountryCode),
                new SqlParameter("@WareHouseName",psm.WareHouseName),
                new SqlParameter("@WareHouseForShort",psm.WareHouseForShort),
                new SqlParameter("@WareHousePrincipal",psm.WareHousePrincipal),
                new SqlParameter("@WareHouseAddress",psm.WareHouseAddress),
                new SqlParameter("@WareHouseDescr",psm.WareHouseDescr),
                new SqlParameter("@WareControl",psm.WareControl),
                new SqlParameter("@CPCCode",psm.CPCCode)
            };

            return DBHelper.ExecuteNonQuery(cmd, param, CommandType.Text);
        }

        /// <summary>
        /// 删除指定仓库信息
        /// </summary>
        /// <param name="WareHouseID">仓库ID</param>
        /// <returns>返回删除指定仓库信息所影响的行数</returns>
        public static int DelWareHouseByWareHouseID(int WareHouseID)
        {
            SqlParameter[] param = new SqlParameter[] 
            {
                new SqlParameter("@WareHouseID",WareHouseID)
            };

            return DBHelper.ExecuteNonQuery("DelWareHouse", param, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 更新指定仓库信息
        /// </summary>
        /// <param name="psm">仓库模型</param>
        /// <returns>返回更新指定仓库信息所影响的行数</returns>
        public static int UpdWareHouseByWareHouseID(WareHouseModel psm)
        {
            string cmd = @"update WareHouse set CountryCode=@CountryCode,WareHouseName=@WareHouseName,WareHouseForShort=@WareHouseForShort,WareHousePrincipal=@WareHousePrincipal,
                        WareHouseAddress=@WareHouseAddress,WareHouseDescr=@WareHouseDescr,CPCCode=@CPCCode where WareHouseID=@WareHouseID";

            SqlParameter[] param = new SqlParameter[] 
            {
                new SqlParameter("@CountryCode",psm.CountryCode),
                new SqlParameter("@WareHouseName",psm.WareHouseName),
                new SqlParameter("@WareHouseForShort",psm.WareHouseForShort),
                new SqlParameter("@WareHousePrincipal",psm.WareHousePrincipal),
                new SqlParameter("@WareHouseAddress",psm.WareHouseAddress),
                new SqlParameter("@WareHouseDescr",psm.WareHouseDescr),              
                new SqlParameter("@WareHouseID",psm.WareHouseID),             
                new SqlParameter("@CPCCode",psm.CPCCode)
            };

            return DBHelper.ExecuteNonQuery(cmd, param, CommandType.Text);
        }

        /// <summary>
        /// 查询仓库信息
        /// </summary>
        /// <param name="WareHouseID"></param>
        /// <returns></returns>
        public static WareHouseModel GetWareHouseItem(int WareHouseID)
        {
            string cmd = "select WareHouseID,WareHouseName,WareHouseForShort,WareHousePrincipal,WareHouseAddress,WareHouseDescr,WareControl,CPCCode from WareHouse where WareHouseID=@WareHouseID";

            SqlParameter[] param = new SqlParameter[] 
            {
                new SqlParameter("@WareHouseID",WareHouseID)
            };

            SqlDataReader dr = DBHelper.ExecuteReader(cmd, param, CommandType.Text);

            dr.Read();

            WareHouseModel psm = new WareHouseModel();

            psm.WareHouseID = Convert.ToInt32(dr["WareHouseID"]);
            psm.WareHouseName = dr["WareHouseName"].ToString();
            psm.WareHouseForShort = dr["WareHouseForShort"].ToString();
            psm.WareHousePrincipal = dr["WareHousePrincipal"].ToString();
            psm.WareHouseAddress = dr["WareHouseAddress"].ToString();
            psm.WareHouseDescr = dr["WareHouseDescr"].ToString();
            psm.WareControl = Convert.ToInt32(dr["WareControl"]);
            psm.CPCCode = dr["CPCCode"].ToString();

            dr.Close();

            return psm;
        }

        /// <summary>
        /// 获取所有的仓库信息
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetWareHouseInfo()
        {
            return DBHelper.ExecuteDataTable("GetWareHouseInfo", CommandType.StoredProcedure);
        }

        /// <summary>
        /// 获取指定的仓库信息
        /// </summary>
        /// <param name="wareHouseID">仓库ID</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetWareHouseInfoByWareHouseID(int wareHouseID)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@wareHouseID",SqlDbType.Int)
            };
            sparams[0].Value = wareHouseID;

            return DBHelper.ExecuteDataTable("GetWareHouseInfoByWareHouseID", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 获取最大的权限编号
        /// </summary>
        /// <returns>返回最大的权限编号</returns>
        public static int GetMaxWareControlFromWareHouse()
        {
            return Convert.ToInt32(DBHelper.ExecuteScalar("GetMaxWareControlFromWareHouse", CommandType.StoredProcedure));
        }

        /// <summary>
        /// 通过仓库名称获取仓库名称的行数
        /// </summary>
        /// <param name="wareHouseName">仓库名称</param>
        /// <returns></returns>
        public static int GetWareHouseNameCountByWareHouseName(string wareHouseName)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@wareHouseName",SqlDbType.VarChar,50)
            };
            sparams[0].Value = wareHouseName;

            return Convert.ToInt32(DBHelper.ExecuteScalar("GetWareHouseNameCountByWareHouseName", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// 通过仓库ID和仓库名称获取仓库名称的行数
        /// </summary>
        /// <param name="depotSeatModel">仓库模型</param>
        /// <returns>返回通过仓库ID和仓库名称获取仓库名称的行数</returns>
        public static int GetWareHouseNameCountByWareHouseIDName(WareHouseModel wareHouseModel)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@wareHouseID",SqlDbType.Int),
                new SqlParameter("@wareHouseName",SqlDbType.VarChar,50)
            };
            sparams[0].Value = wareHouseModel.WareHouseID;
            sparams[1].Value = wareHouseModel.WareHouseName;

            return Convert.ToInt32(DBHelper.ExecuteScalar("GetWareHouseNameCountByWareHouseIDName", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// 获取仓库名(Outstock.aspx)
        /// </summary>
        /// <param name="storeOrderID"></param>
        /// <returns></returns>
        public static ListItem GetWareHouseName(string storeOrderID)
        {
            string sqlstr = "select wh.WareHouseName from StoreOrder so inner join WareHouse wh on so.WareHouseID=wh.WareHouseID where so.StoreOrderID=@storeOrderID";

            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@storeOrderID", storeOrderID) };

            SqlDataReader dr = DBHelper.ExecuteReader(sqlstr, param, CommandType.Text);

            if (dr.Read())
            {
                string _wareHouseName = dr["WareHouseName"].ToString();

                dr.Close();

                return new ListItem(_wareHouseName, _wareHouseName);
            }
            else
                return new ListItem("无", "无");
        }

        public static DataTable GetWareHouseName()
        {
            Hashtable table = (Hashtable)HttpContext.Current.Session["permission"];
            if (table != null)
            {

                string sqlstr = "select CountryCode,WareHouseID,WareHouseName,WareControl from WareHouse";

                DataTable newDt = new DataTable();

                DataColumn dc1 = new DataColumn("WareHouseID");
                DataColumn dc2 = new DataColumn("WareHouseName");
                DataColumn dc3 = new DataColumn("CountryCode");

                newDt.Columns.Add(dc1);
                newDt.Columns.Add(dc2);
                newDt.Columns.Add(dc3);

                DataTable dt = DBHelper.ExecuteDataTable(sqlstr);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (table.Contains(Convert.ToInt32(dt.Rows[i]["WareControl"])))
                    {
                        DataRow newDr = newDt.NewRow();
                        newDr["WareHouseID"] = dt.Rows[i]["WareHouseID"];
                        newDr["WareHouseName"] = dt.Rows[i]["WareHouseName"];
                        newDr["CountryCode"] = dt.Rows[i]["CountryCode"];
                        newDt.Rows.Add(newDr);
                    }
                }

                return newDt;
            }
            else
            {
                string sqlstr = "select CountryCode,WareHouseID,WareHouseName,WareControl from WareHouse";

                DataTable newDt = new DataTable();

                DataColumn dc1 = new DataColumn("WareHouseID");
                DataColumn dc2 = new DataColumn("WareHouseName");
                DataColumn dc3 = new DataColumn("CountryCode");

                newDt.Columns.Add(dc1);
                newDt.Columns.Add(dc2);
                newDt.Columns.Add(dc3);

                DataTable dt = DBHelper.ExecuteDataTable(sqlstr);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow newDr = newDt.NewRow();
                    newDr["WareHouseID"] = dt.Rows[i]["WareHouseID"];
                    newDr["WareHouseName"] = dt.Rows[i]["WareHouseName"];
                    newDr["CountryCode"] = dt.Rows[i]["CountryCode"];
                    newDt.Rows.Add(newDr);
                }

                return newDt;
            }

        }

        public static DataTable GetWareHouseName_Currency(string currency)
        {
            DataTable dt = GetWareHouseName();

            string countryCode = DBHelper.ExecuteScalar("select countryCode from country where RateId='" + currency + "'") + "";

            DataTable newDt = new DataTable();

            DataColumn dc1 = new DataColumn("WareHouseID");
            DataColumn dc2 = new DataColumn("WareHouseName");

            newDt.Columns.Add(dc1);
            newDt.Columns.Add(dc2);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //if (dt.Rows[i]["CountryCode"].ToString() == countryCode)
                //{
                //    DataRow newDr = newDt.NewRow();
                //    newDr["WareHouseID"] = dt.Rows[i]["WareHouseID"];
                //    newDr["WareHouseName"] = dt.Rows[i]["WareHouseName"];

                //    newDt.Rows.Add(newDr);
                //}
                DataRow newDr = newDt.NewRow();
                newDr["WareHouseID"] = dt.Rows[i]["WareHouseID"];
                newDr["WareHouseName"] = dt.Rows[i]["WareHouseName"];

                newDt.Rows.Add(newDr);
            }

            return newDt;


        }

        #region 读取仓库名称
        /// <summary>
        /// 读取仓库名称
        /// </summary>		
        /// <returns></returns>
        public static string GetWareHouseNameByID(string warehouseId)
        {
           
            try
            {
                return DBHelper.ExecuteDataTable(@"
					Select WareHouseName  From WareHouse  Where WareHouseID=" + warehouseId).Rows[0][0].ToString();
            }
            catch
            {
                return   "未审核";
            }
        }
        #endregion

        /// <summary>
        /// 读取产品基础数据的仓库信息
        /// </summary>		
        /// <returns></returns>
        public static DataTable GetProductWareHouseInfo()
        {
            return DBHelper.ExecuteDataTable("Select WareHouseID  as ID ,WareHouseName as Name, WareHouseDescr From WareHouse Order By WareHouseID ");
        }


        /// <summary>
        /// 获取所有供应商
        /// </summary>
        /// <returns></returns>
        public static IList<WareHouseModel> GetWareHouses()
        {
            string sql = "select WareHouseID,WareControl,WareHouseName from WareHouse";
            SqlDataReader dr = DBHelper.ExecuteReader(sql, CommandType.Text);
            IList<WareHouseModel> ps = null;
            if (dr.HasRows)
            {
                ps = new List<WareHouseModel>();
                while (dr.Read())
                {
                    WareHouseModel wm = new WareHouseModel();
                    wm.WareHouseID = dr.GetInt32(0);
                    wm.WareControl = dr.GetInt32(1);
                    wm.WareHouseName = dr.GetString(2);
                    ps.Add(wm);
                }
            }
            dr.Close();
            return ps;
        }

        /// <summary>
        /// 获取仓库xml字符串
        /// </summary>
        /// <returns></returns>
        public static string GetWareHouseXML()
        {
            string cmd = "select WareHouseID,WareHouseName from dbo.WareHouse";

            SqlDataReader dr = DBHelper.ExecuteReader(cmd);

            string xmlstr = "<?xml version='1.0' encoding='utf-8' ?><Root>";

            while (dr.Read())
            {
                xmlstr = xmlstr + "<Row><WareHouseID>" + dr["WareHouseID"] + "</WareHouseID><WareHouseName>" + dr["WareHouseName"] + "</WareHouseName></Row>";
            }
            xmlstr = xmlstr + "</Root>";

            dr.Close();

            return xmlstr;
        }

        /// <summary>
        /// Judge the WareHouseId whether has operation by Id before delete
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Return the counts of the WareHouse by Id</returns>
        public static int WareHouseIdWhetherHasOperation(int Id)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@Id",SqlDbType.Int)
            };
            sparams[0].Value = Id;

            return Convert.ToInt32(DBHelper.ExecuteScalar("WareHouseIdWhetherHasOperation", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// Judge the WareHouseId whether exist by Id before delete or update
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Return the counts of the WareHouse by Id</returns>
        public static int WareHouseIdIsExist(int Id)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@Id",SqlDbType.Int)
            };
            sparams[0].Value = Id;

            return Convert.ToInt32(DBHelper.ExecuteScalar("WareHouseIdIsExist", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// 获取仓库名
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Return the counts of the WareHouse by Id</returns>
        public static string WareHouseIdEName(int Id)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@warehouseid",SqlDbType.Int)
            };
            sparams[0].Value = Id;
            return (DBHelper.ExecuteScalar("select warehousename from warehouse where warehouseid=@warehouseid", sparams, CommandType.Text).ToString());
        }
        /// <summary>
        /// 判断当前登录的管理员是否有该仓库的权限
        /// </summary>
        /// <param name="number"></param>
        /// <param name="wareControl"></param>
        /// <returns></returns>
        public static bool WareHouseisPermission(string number, int wareControl)
        {
            string sql = "select a.PermissionId from ManagerPermission as a,Manage as b	where b.Number=@number and b.RoleID=a.ManagerID and a.PermissionID=@PermissionID";
            SqlParameter[] parm = new SqlParameter[] { 
            new SqlParameter("@number",number),
            new SqlParameter("@PermissionID",wareControl)
            };
            object obj = DBHelper.ExecuteScalar(sql, parm, CommandType.Text);
            if (obj == null)
                return false;
            else
                return true;
        }
        /// <summary>
        /// 根据仓库编号获得仓库权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int GetWareControlByWareHoseID(int id)
        {
            string sql = "select WareControl from WareHouse where WareHouseID=" + id + "";
            object obj = DBHelper.ExecuteScalar(sql);
            if (obj == null)
                return 0;
            else
                return int.Parse(obj.ToString());
        }

        /// <summary>
        /// Get the information of the WareHouse by Number and CountryCode
        /// </summary>
        /// <param name="number">Number</param>
        /// <param name="countryCode">CountryCode</param>
        /// <returns>Return DataTable Object</returns>
        public static DataTable GetWareHouseInfoByNumberCountryCode(string number, string countryCode)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
	            new SqlParameter("@number",SqlDbType.NVarChar,40),
	            new SqlParameter("@countryCode",SqlDbType.NVarChar,20)
            };
            sparams[0].Value = number;
            sparams[1].Value = countryCode;
            return DBHelper.ExecuteDataTable("GetWareHouseInfoByNumberCountryCode", sparams, CommandType.StoredProcedure);
        }
    }
}
