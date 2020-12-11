using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Model;
using System.Data;

namespace BLL.other.Company
{
    public class ResourcesBLL
    {
          /// <summary>
       /// 修改上传资料
       /// </summary>
       /// <param name="resource"></param>
       /// <returns></returns>
        public bool UpdateResource(ResourceModel resource)
        {
            BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("Resources", "ltrim(rtrim(ResID))");

            cl_h_info.AddRecord(resource.ResID);

            ResourcesDAL dao = new ResourcesDAL();
            bool boo= dao.UpdateResource(resource);

            cl_h_info.AddRecord(resource.ResID);
            cl_h_info.ModifiedIntoLogs(BLL.CommonClass.ChangeCategory.company18, resource.ResID.ToString(), BLL.CommonClass.ENUM_USERTYPE.objecttype10);

            return boo;
        
        }
         /// <summary>
       /// 检查上传的文件名字是否存在
       /// </summary>
       /// <param name="FileName"></param>
       /// <returns></returns>
        public bool CheckFileName(string FileName)
        {
            ResourcesDAL dao = new ResourcesDAL();
            return dao.CheckFileName(FileName);
        }
         /// <summary>
       /// 查询出老的文件名
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        public string GetOldfilename(int id)
        {
            ResourcesDAL dao = new ResourcesDAL();
            return dao.GetOldfilename(id);
        }
        /// <summary>
       /// 插入上传文件
       /// </summary>
       /// <param name="resource"></param>
       /// <returns></returns>
        public bool InsertResource(ResourceModel resource)
        {
             ResourcesDAL dao = new ResourcesDAL();
             return dao.InsertResource(resource);
        }
        /// <summary>
        /// 检查文件名称是否存在 DS2012
        /// </summary>
        /// <param name="ResName"></param>
        /// <returns></returns>
        public bool CheckReourceResname(string ResName)
        {
            ResourcesDAL dao = new ResourcesDAL();
            return dao.CheckReourceResname(ResName);
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DelResurces(int id)
        {
            BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("Resources", "ltrim(rtrim(ResID))");
            cl_h_info.AddRecord(id);
            cl_h_info.DeletedIntoLogs(BLL.CommonClass.ChangeCategory.company18, id.ToString(), BLL.CommonClass.ENUM_USERTYPE.objecttype10);

            ResourcesDAL dao = new ResourcesDAL();
            return dao.DelResurces(id);
        }
        /// <summary>
        /// 修改下载次数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateResutcesResTime(int id)
        {
            ResourcesDAL dao = new ResourcesDAL();
            return dao.UpdateResutcesResTime(id);
        }
        /// <summary>
        /// 根据id查询Resource
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResourceModel GetResourcesById(int id)
        {
            ResourcesDAL dao = new ResourcesDAL();
            return dao.GetResourcesById(id);

        }

           /// <summary>
       /// 根据条件查出数据 返回一个结果集 （导出excel）
       /// </summary>
       /// <param name="conction"></param>
       /// <returns></returns>
        public DataTable GetTableForExcel(string conction)
        {
            ResourcesDAL dao = new ResourcesDAL();
            return dao.GetTableForExcel(conction);
        }
    }
}
