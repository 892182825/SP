using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Add Namespace
using System.Data;
using System.Data.SqlClient;
using Model;

/*
 *Creator:      WangHua
 *CreateDate:   2009-09-27 
 *FinishDate:   2010-01-25
 *Function：     File Operation
 */

namespace DAL
{
   public class ResourcesDAL
    {
       /// <summary>
       /// 删除文件
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public bool DelResurces(int id)
       {
           if (DBHelper.ExecuteNonQuery("DelResources", new SqlParameter("@ResID",id),CommandType.StoredProcedure)>0)
           {
               return true;
           }
           else
           {
               return false;
           }
       }

       /// <summary>
       /// 修改下载次数
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public bool UpdateResutcesResTime(int id)
       {
           if (DBHelper.ExecuteNonQuery("UpdateResourcesResTimes",new SqlParameter("@ResID",id),CommandType.StoredProcedure)>0)
           {
               return true;
           }
           else
           {
               return false;
           }
       }

       /// <summary>
       /// 检查文件名称是否存在 DS2012
       /// </summary>
       /// <param name="ResName"></param>
       /// <returns></returns>
       public bool CheckReourceResname(string ResName)
       {
           SqlDataReader reader = DBHelper.ExecuteReader("CheckReourceResname", new SqlParameter("@ResName", ResName), CommandType.StoredProcedure);
           if (reader.Read())
           {
               reader.Close();
               return true; 
           }
           else
           {
               reader.Close();
               return false;
              
           }
       }

       /// <summary>
       /// 根据条件查出数据 返回一个结果集 （导出excel）
       /// </summary>
       /// <param name="conction"></param>
       /// <returns></returns>
       public DataTable GetTableForExcel(string conction)
       {
           string sql = @"select ResName, FileName, ResDescription, ResSize, ResDateTime, ResTimes from Resources where "+conction;
           return DAL.DBHelper.ExecuteDataTable(sql);


       }

       /// <summary>
       /// 修改上传资料
       /// </summary>
       /// <param name="resource"></param>
       /// <returns></returns>
       public bool UpdateResource(ResourceModel resource)
       {

           SqlParameter[] param = new SqlParameter[] { 
            new SqlParameter("@ResID",resource.ResID),
            new SqlParameter("@ResName",resource.ResName),
            new SqlParameter("@filename",resource.FileName),
            new SqlParameter("@resdes",resource.ResDescription),
            new SqlParameter("@Ressize",resource.ResSize),
            new SqlParameter("@Resdatetime",resource.ResDateTime),
            new SqlParameter("@DownTarget",resource.DownTarget),
            new SqlParameter("@DownMenberLev",resource.DownMenberLev)

           };
           if (DBHelper.ExecuteNonQuery("UpdateResInfo", param, CommandType.StoredProcedure)>0)
           {
               return true;
           }
           return false;
       }

       /// <summary>
       /// 插入上传文件
       /// </summary>
       /// <param name="resource"></param>
       /// <returns></returns>
       public bool InsertResource(ResourceModel resource)
       {
           SqlParameter[] param = new SqlParameter[] { 
            new SqlParameter("@ResName",resource.ResName),
            new SqlParameter("@filename",resource.FileName),
            new SqlParameter("@ResDescription",resource.ResDescription),
            new SqlParameter("@Ressize",resource.ResSize),
            new SqlParameter("@Resdatetime",resource.ResDateTime),
            new SqlParameter("@CPPCode",resource.CPPCode),
            new SqlParameter("@DownTarget",resource.DownTarget),
            new SqlParameter("@DownMenberLev",resource.DownMenberLev)
           };
           if (DBHelper.ExecuteNonQuery("InsertFileName", param, CommandType.StoredProcedure) > 0)
           {
               return true;
           }
           return false;
       }
       /// <summary>
       /// 查询出老的文件名
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public string GetOldfilename(int id)
       {
           return DBHelper.ExecuteScalar("getOldFilename",new SqlParameter("@ResID",id), CommandType.StoredProcedure).ToString();
       }




       /// <summary>
       /// 检查上传的文件名字是否存在
       /// </summary>
       /// <param name="FileName"></param>
       /// <returns></returns>
       public bool CheckFileName(string FileName)
       {
           SqlParameter[] param = new SqlParameter[] { 
            new SqlParameter("@filename",FileName)
           };
           SqlDataReader reader= DBHelper.ExecuteReader("CheckFileName",param,CommandType.StoredProcedure);
           if (reader.Read())
           {
               return true;
           }
           reader.Close();
           return false;
        }

       /// <summary>
       /// 根据id查询Resource
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public ResourceModel GetResourcesById(int id)
       {
           SqlDataReader reader = DBHelper.ExecuteReader("GetResourcesById", new SqlParameter("@ResID", id), CommandType.StoredProcedure);
           ResourceModel res = null;
           while (reader.Read())
           {
               res=new ResourceModel();
               res.FileName = reader["FileName"].ToString();
               res.ResDateTime = Convert.ToDateTime(reader["ResDateTime"]);
               res.ResDescription = reader["ResDescription"].ToString();
               res.ResID = Convert.ToInt32(reader["ResID"]);
               res.ResName = reader["ResName"].ToString();
               res.ResSize = reader["ResSize"].ToString();
               res.ResTimes = Convert.ToInt32(reader["ResTimes"]);
           }
           reader.Close();
           return res;
       }

       public int updresources(string resid)
       {           
           string strSql = "update resources set ResTimes=ResTimes+1 where ResId=@num";
           SqlParameter spa = new SqlParameter("@num",SqlDbType.Int);
           spa.Value = Convert.ToInt32(resid); 
           
           return DBHelper.ExecuteNonQuery(strSql,spa,CommandType.Text);
       }

       /// <summary>
       /// 更新指定的下载次数
       /// </summary>
       /// <param name="resID">资源ID</param>
       /// <returns>返回更新指定的下载次数所影响的行数</returns>
       public static int UpdResourcesResTimesByResID(int resID)
       {
          SqlParameter[] sparams = new SqlParameter[]
          {
              new SqlParameter("@resID",SqlDbType.Int)
          };
          sparams[0].Value = resID;
          
          return DBHelper.ExecuteNonQuery("UpdResourcesResTimesByResID", sparams, CommandType.StoredProcedure);
       }       

       /// <summary>
       /// 获取资源表中的信息
       /// </summary>
       /// <param name="columnNames">列名</param>
       /// <param name="conditions">条件</param>
       /// <returns>返回DataTable对象</returns>
       public static DataTable GetResourcesInfoByConditions(string columnNames, string conditions)
       {
           SqlParameter[] sparams = new SqlParameter[]
           {
               new SqlParameter("@columnNames",SqlDbType.NVarChar,1000),
               new SqlParameter("@conditions",SqlDbType.NVarChar,1000)
           };
           sparams[0].Value = columnNames;
           sparams[1].Value = conditions;
           
           return DBHelper.ExecuteDataTable("GetResourcesInfoByConditions", sparams, CommandType.StoredProcedure);
       }
    }
}
