using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;
using DAL;


/**
 * 创建者;刘文
 * 创建时间：2009-8-27
 * 修改者：汪华
 * 修改时间:2009-09-07
 * 文件名：MemInfoSearchBLL
 * 功能：会员信息查询
 * **/
namespace BLL.other.Company
{
    public  class MemInfoSearchBLL
    {

        MemberInfoDAL memberInfoDAL = new MemberInfoDAL();

        /// <summary>
        /// 根据查询条件获取会员
        /// </summary>
        public DataTable getMemberAll(int PageIndex, int PageSize, string table, string columns, string condition, string key, out int RecordCount, out int PageCount)
        {
            return memberInfoDAL.getMemberAll(PageIndex, PageSize, table, columns, condition, key, out  RecordCount, out  PageCount);
        }
        /// <summary>
        /// 根据Number获得单个会员的信息
        /// </summary>
        /// <param name="Number">会员编号</param>
        /// <returns></returns>
        public MemberInfoModel getMemberInfo(string Number)
        {
            return MemberInfoDAL.getMemberInfo(Number);
        }
    }
}
