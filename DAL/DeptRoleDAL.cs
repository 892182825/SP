using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Model.Other;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using DAL.Other;

/*
 *  创建人：  孙延昊
 *  创建时间：2009.8.27
 *  文件名：DeptRoleDAL.cs
 *  功能：添加，修改，删除，获取角色信息、权限
 * **/
namespace DAL
{
    public class DeptRoleDAL
    {
        #region 获取所有角色
        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns>所有角色信息</returns>
        public static IList<DeptRoleModel> GetDeptRoles(PaginationModel pageInfo)
        {
            IList<DeptRoleModel> deptRoles = null;
            SqlParameter[] paras = new SqlParameter[4];
            paras[0] = new SqlParameter("@rowCount", SqlDbType.Int);
            paras[1] = new SqlParameter("@pageIndex", SqlDbType.Int);
            paras[2] = new SqlParameter("@dataCount", SqlDbType.Int);
            paras[3] = new SqlParameter("@pageCount", SqlDbType.Int);
            paras[0].SqlValue = pageInfo.RowCount;
            paras[1].SqlValue = pageInfo.PageIndex;
            paras[2].SqlValue = pageInfo.DataCount;
            paras[3].SqlValue = pageInfo.PageCount;
            SqlDataReader reader = DBHelper.ExecuteReader("GetAllDeptRoles", paras, CommandType.StoredProcedure);
            if (reader.HasRows)
            {
                deptRoles = new List<DeptRoleModel>(0);
                while (reader.Read())
                {
                    deptRoles.Add(GetDeptRoles(reader));
                }
            }
            reader.Close();
            return deptRoles;
        }
        #endregion

        /// <summary>
        /// 根据部门获取所有角色
        /// </summary>
        /// <param name="companyDeptId">部门编号</param>
        /// <returns></returns>
        public static IList<DeptRoleModel> GetAllDeptRoleName(int companyDeptId)
        {
            //初始化角色集合
            IList<DeptRoleModel> deptRoles = null;
            //构造获取角色存储过程所需参数
            SqlParameter[] paras = new SqlParameter[1];
            paras[0] = GetSqlParameter("@comDeptID", SqlDbType.Int, companyDeptId);
            SqlDataReader reader = DBHelper.ExecuteReader("GetDeptRolesByComDeptId", paras, CommandType.StoredProcedure);
            if (reader.HasRows)
            {
                deptRoles = new List<DeptRoleModel>();
                while (reader.Read())
                {
                    DeptRoleModel deptRole = new DeptRoleModel();
                    deptRole.DeptID = reader.GetInt32(0);
                    deptRole.Name = reader.GetString(1);
                    deptRoles.Add(deptRole);
                }
            }
            reader.Close();
            return deptRoles;
        }

        public static SqlParameter GetSqlParameter(string paraName, SqlDbType sqlDbType, out SqlParameter para, object obj)
        {
            para = new SqlParameter(paraName, sqlDbType);
            para.Value = obj;
            return para;
        }

        public static SqlParameter GetSqlParameter(string paraName, SqlDbType sqlDbType, object obj)
        {
            SqlParameter para = new SqlParameter(paraName, sqlDbType);
            para.Value = obj;
            return para;
        }

        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public static IList<DeptRoleModel> GetDeptRoles(PaginationModel pageInfo, int managerId)
        {
            IList<DeptRoleModel> deptRoles = null;
            SqlParameter[] paras = new SqlParameter[5];
            paras[0] = new SqlParameter("@rowCount", SqlDbType.Int);
            paras[1] = new SqlParameter("@pageIndex", SqlDbType.Int);
            paras[2] = new SqlParameter("@dataCount", SqlDbType.Int);
            paras[3] = new SqlParameter("@pageCount", SqlDbType.Int);
            paras[4] = new SqlParameter("@managerId", SqlDbType.Int);
            paras[0].SqlValue = pageInfo.RowCount;
            paras[1].SqlValue = pageInfo.PageIndex;
            paras[2].SqlValue = pageInfo.DataCount;
            paras[3].SqlValue = pageInfo.PageCount;
            paras[4].SqlValue = managerId;
            SqlDataReader reader = DBHelper.ExecuteReader("GetAllDeptRoles", paras, CommandType.StoredProcedure);

            if (reader.HasRows)
            {
                deptRoles = new List<DeptRoleModel>(0);
                while (reader.Read())
                {
                    deptRoles.Add(GetDeptRoles(reader));
                }
            }
            reader.Close();
            return deptRoles;
        }
        

        #region 构建角色对象
        /// <summary>
        /// 构建角色对象
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static DeptRoleModel GetDeptRoles(SqlDataReader reader)
        {
            DeptRoleModel deptRole = new DeptRoleModel(reader.GetInt32(0));
            deptRole.Name = reader.GetString(1);
            deptRole.DeptID = reader.GetInt32(2);
            deptRole.Adddate = reader.GetDateTime(3);
            deptRole.PermissionManID = reader.GetInt32(4);
            deptRole.Allot = reader.GetInt32(5);

            reader.Close();
            return deptRole;
        }
        #endregion

        /// <summary>
        /// 判断是否可以进行角色权限添加
        /// </summary>
        /// <param name="deptRoleID"></param>
        /// <returns></returns>
        public static bool GetAllot(int deptRoleID)
        {
            bool allot = false;
            SqlParameter[] paras = new SqlParameter[1];
            paras[0] = new SqlParameter("@deptRoleId", SqlDbType.Int);
            paras[0].SqlValue = deptRoleID;
            object obj = DBHelper.ExecuteScalar("GetrolePermissionAllot", paras, CommandType.StoredProcedure);
            allot = obj == null ? false : obj.ToString() == "1";
            return allot;
        }

        #region 获取权限树
        /// <summary>
        /// 当前用户权限hash表
        /// </summary>
        /// <param name="htb"></param>
        /// <returns></returns>
        public static string GetRPTree(System.Collections.Hashtable htb,string number)
        {
            #region 权限树
            StringBuilder hstr = new StringBuilder();
//            //if (htb.Contains((int)EnumCompanyPermission.))
//            #region 客户管理
//            hstr.Append( "<div><IMG class=\"menutop\" id=\"plus1\" onclick=\"menu(menu1,img1,this)\" src=\"images/plus3.gif\"");
//         hstr.Append("align=\"absMiddle\"><IMG id=\"img1\" src=\"images/foldclose.gif\" align=\"absMiddle\">客户管理");
//         hstr.Append("</div><div id=\"menu1\" style=\"MARGIN-TOP: -3px); DISPLAY: none\">");
//            if (htb.Contains((int)EnumCompanyPermission.CustomerMembermoidy))
//            {
//                hstr.Append( "<div><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG class=\"menutop\" id=\"plus10\" onclick=\"menu(menu10,img10,this)\" src=\"images/plus3.gif\"");
//                     hstr.Append("align=\"absMiddle\"><IMG id=\"img10\" src=\"images/foldclose.gif\" align=\"absMiddle\"><input id=\"checkbox1\" type=\"checkbox\" value=\"0\" name=\"qxCheckBox\">会员资料编辑</div>");
//            }
//            if (htb.Contains((int)EnumCompanyPermission.CustomerModifyMemberInfoKaHao))
//            {
//                hstr.Append( "<div id=\"menu10\" style=\"MARGIN-TOP: -3px); DISPLAY: none\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox7\" onclick=\"checkpid('checkbox1',this);\" type=\"checkbox\" value=\"1\" name=\"qxCheckBox\">会员基本信息修改<br>");
//            }
//            if (htb.Contains((int)EnumCompanyPermission.CustomerModifyMemberInfoZheHao))
//            {
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox8\" onclick=\"checkpid('checkbox1',this);\" type=\"checkbox\" value=\"2\" name=\"qxCheckBox\">会员高级信息修改</div>");
//            }
//            if (htb.Contains((int)EnumCompanyPermission.CustomerStoreManage))
//            {
//                hstr.Append( "<div><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG class=\"menutop\" id=\"plus11\" onclick=\"menu(menu11,img11,this)\" src=\"images/plus3.gif\"");
//                     hstr.Append("align=\"absMiddle\"><IMG id=\"img11\" src=\"images/foldclose.gif\" align=\"absMiddle\"><input id=\"checkbox4\" type=\"checkbox\" value=\"3\" name=\"qxCheckBox\">店铺信息编辑</div> <div id=\"menu11\" style=\"MARGIN-TOP: -3px); DISPLAY: none\">");
//                if (htb.Contains((int)EnumCompanyPermission.CustomerStoreManage))
//                {
//                    hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox9\" onclick=\"checkpid('checkbox4',this);\" type=\"checkbox\" value=\"4\" name=\"qxCheckBox\">店铺账号编辑<br>");
//                    if (htb.Contains((int)EnumCompanyPermission.CustomerStoreManage))
//                    {
//                        hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox10\" onclick=\"checkpid('checkbox4',this);\" type=\"checkbox\" value=\"5\"");
//                 hstr.Append("name=\"qxCheckBox\">编辑店铺<br>");
//                    }
//                    if (htb.Contains((int)EnumCompanyPermission.CustomerStoreManageDelete))
//                    {
//                        hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox11\" onclick=\"checkpid('checkbox4',this);\" type=\"checkbox\" value=\"6\"");
//                     hstr.Append("name=\"qxCheckBox\">删除店铺<br>");
//                    }
//                }
//                hstr.Append( "</div>");
//            }
//            if (htb.Contains((int)EnumCompanyPermission.CustomerAuditingStoreRegister))
//            {
//                hstr.Append( "<div><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG class=\"menutop\" id=\"plus20\" onclick=\"menu(menu20,img20,this)\" src=\"images/plus3.gif\"");
//                 hstr.Append("align=\"absMiddle\"><IMG id=\"img20\" src=\"images/foldclose.gif\" align=\"absMiddle\"><input id=\"checkbox57\" type=\"checkbox\" value=\"7\" name=\"qxCheckBox\">店铺注册确认</div>");
//                 hstr.Append("<div id=\"menu20\" style=\"MARGIN-TOP: -3px); DISPLAY: none\">");
//                if (htb.Contains((int)EnumCompanyPermission.CustomerAuditingStoreRegisterDelete))
//                {
//                    hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox71\" onclick=\"checkpid('checkbox57',this);\" type=\"checkbox\" value=\"8\"");
//                     hstr.Append("name=\"qxCheckBox\">删除店铺<br>");
//                }
//                if (htb.Contains((int)EnumCompanyPermission.CustomerAuditingStoreRegisterAuditing))
//                {
//                    hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox72\" onclick=\"checkpid('checkbox57',this);\" type=\"checkbox\" value=\"9\"");
//                     hstr.Append("name=\"qxCheckBox\">审核店铺<br>");
//                }
//                hstr.Append( "</div>");
//            }
//            if (htb.Contains((int)EnumCompanyPermission.CustomerProviderViewEdit))
//            {
//                hstr.Append( "<div><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG class=\"menutop\" id=\"plus14\" onclick=\"menu(menu14,img14,this)\" src=\"images/plus3.gif\"");
//                hstr.Append(" align=\"absMiddle\"><IMG id=\"img14\" src=\"images/foldclose.gif\" align=\"absMiddle\"><input id=\"checkbox30\" type=\"checkbox\" value=\"10\" name=\"qxCheckBox\">供应商管理</div>");
//                hstr.Append("<div id=\"menu14\" style=\"MARGIN-TOP: -3px); DISPLAY: none\">");
//                if (htb.Contains((int)EnumCompanyPermission.CustomerProviderViewEditEdit))
//                {
//                    hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox31\" onclick=\"checkpid('checkbox30',this);\" type=\"checkbox\" value=\"11\"");
//                 hstr.Append("name=\"qxCheckBox\">修改供应商信息<br>");
//                }
//                if (htb.Contains((int)EnumCompanyPermission.CustomerProviderViewEditDelete))
//                {
//                    hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox32\" onclick=\"checkpid('checkbox30',this);\" type=\"checkbox\" value=\"12\"");
//                 hstr.Append("name=\"qxCheckBox\">删除供应商信息<br>");
//                }
//                hstr.Append( "</div>");
//            }
//            if (htb.Contains((int)EnumCompanyPermission.CustomerProviderViewEdit))
//            {
//                hstr.Append( "<div><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG class=\"menutop\" id=\"plus15\" onclick=\"menu(menu15,img15,this)\" src=\"images/plus3.gif\"");
//               hstr.Append("align=\"absMiddle\"><IMG id=\"img15\" src=\"images/foldclose.gif\" align=\"absMiddle\"><input id=\"checkbox40\" type=\"checkbox\" value=\"13\" name=\"qxCheckBox\">第三方物流公司</div>");
//               hstr.Append("<div id=\"menu15\" style=\"MARGIN-TOP: -3px); DISPLAY: none\">");
//                if (htb.Contains((int)EnumCompanyPermission.CustomerProviderViewEditEdit))
//                {
//                    hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox46\" onclick=\"checkpid('checkbox40',this);\" type=\"checkbox\" value=\"14\"");
//                   hstr.Append("name=\"qxCheckBox\">修改第三方物流公司<br>");
//                }
//                if (htb.Contains((int)EnumCompanyPermission.CustomerProviderViewEditDelete))
//                {
//                    hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox47\" onclick=\"checkpid('checkbox40',this);\" type=\"checkbox\" value=\"15\"");
//                 hstr.Append("name=\"qxCheckBox\">删除第三方物流公司<br> ");
//                }
//                hstr.Append( "</div>");
//            }
//            if (htb.Contains((int)EnumCompanyPermission.CustomerQureyMember))
//            {
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox2\" type=\"checkbox\" value=\"16\" name=\"qxCheckBox\">会员资料查询<br>");
//            }

//            if (htb.Contains((int)EnumCompanyPermission.CustomerQueryMemberPassword))
//            {
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox3\" type=\"checkbox\" value=\"17\" name=\"qxCheckBox\">会员密码重置<br>");
//            }
//            if (htb.Contains((int)EnumCompanyPermission.CustomerQueryStore))
//            {
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox5\" type=\"checkbox\" value=\"18\" name=\"qxCheckBox\">店铺信息查询<br>");
//            }
//            if (htb.Contains((int)EnumCompanyPermission.CustomerQueryStorePassword))
//            {
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox6\" type=\"checkbox\" value=\"19\" name=\"qxCheckBox\">店铺密码重置<br>");
//            }
//            if (htb.Contains((int)EnumCompanyPermission.CustomerQueryMemberStoreReset))
//            {
//                hstr.Append("<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox141\" type=\"checkbox\" value=\"141\" name=\"qxCheckBox\">会员店铺重置<br>");
//            }
//            hstr.Append( "</div>");
//            #endregion
//            //客户管理结束
//            #region 库存管理
//            hstr.Append( "<div><IMG class=\"menutop\" id=\"plus2\" onclick=\"menu(menu2,img2,this)\" src=\"images/plus3.gif\"");
//             hstr.Append("align=\"absMiddle\"><IMG id=\"img2\" src=\"images/foldclose.gif\" align=\"absMiddle\">库存管理</div>");
//             hstr.Append("<div id=\"menu2\" style=\"MARGIN-TOP: -3px); DISPLAY: none\">");
//            if (htb.Contains((int)EnumCompanyPermission.StorageProductTreeManager))
//            {
//                hstr.Append( "<div><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG class=\"menutop\" id=\"plus12\" onclick=\"menu(menu12,img12,this)\" src=\"images/plus3.gif\"");
//               hstr.Append("align=\"absMiddle\"><IMG id=\"img12\" src=\"images/foldclose.gif\" align=\"absMiddle\"><input id=\"checkbox12\" type=\"checkbox\" value=\"20\" name=\"qxCheckBox\">增加新品</div>");
//               hstr.Append("<div id=\"menu12\" style=\"MARGIN-TOP: -3px); DISPLAY: none\">");
//                if (htb.Contains((int)EnumCompanyPermission.StorageProductTreeManageAddStyle))
//                {
//                    hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox13\" onclick=\"checkpid('checkbox12',this);\" type=\"checkbox\" value=\"21\"");
//               hstr.Append("name=\"qxCheckBox\">添加新类<br>");
//                }
//                if (htb.Contains((int)EnumCompanyPermission.StorageProductTreeManagerAddProduct))
//                {
//                    hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox14\" onclick=\"checkpid('checkbox12',this);\" type=\"checkbox\" value=\"22\"");
//                 hstr.Append("name=\"qxCheckBox\">添加新品<br>");
//                }
//                if (htb.Contains((int)EnumCompanyPermission.StorageProductTreeManagerEditStyle))
//                {
//                    hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox15\" onclick=\"checkpid('checkbox12',this);\" type=\"checkbox\" value=\"23\"");
//                    hstr.Append("name=\"qxCheckBox\">修改类<br>");
//                }
//                if (htb.Contains((int)EnumCompanyPermission.StorageProductTreeManagerDeleteStyle))
//                {
//                    hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox16\" onclick=\"checkpid('checkbox12',this);\" type=\"checkbox\" value=\"24\"");
//                    hstr.Append("name=\"qxCheckBox\">删除类<br>");
//                }
//                if (htb.Contains((int)EnumCompanyPermission.StorageProductTreeManagerEditProduct))
//                {
//                    hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox17\" onclick=\"checkpid('checkbox12',this);\" type=\"checkbox\" value=\"25\"");
//                    hstr.Append("name=\"qxCheckBox\">修改产品<br>");
//                }
//                if (htb.Contains((int)EnumCompanyPermission.StorageProductTreeManagerDeleteProduct))
//                {
//                    hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox18\" onclick=\"checkpid('checkbox12',this);\" type=\"checkbox\" value=\"26\"");
//                    hstr.Append("name=\"qxCheckBox\">删除产品<br>");
//                }
//                hstr.Append( "</div>");
//            }
//            if (htb.Contains((int)EnumCompanyPermission.StorageStorageInBrowse))
//            {
//                hstr.Append( "<div><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG class=\"menutop\" id=\"plus13\" onclick=\"menu(menu13,img13,this)\" src=\"images/plus3.gif\"");
//              hstr.Append("align=\"absMiddle\"><IMG id=\"img13\" src=\"images/foldclose.gif\" align=\"absMiddle\"><input id=\"checkbox21\" type=\"checkbox\" value=\"27\" name=\"qxCheckBox\">入库审批</div>");
//              hstr.Append("<div id=\"menu13\" style=\"MARGIN-TOP: -3px); DISPLAY: none\">");
//                if (htb.Contains((int)EnumCompanyPermission.StorageStorageInBrowseAuditing))
//                {
//                    hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox33\" onclick=\"checkpid('checkbox21',this);\" type=\"checkbox\" value=\"28\"");
//                   hstr.Append("name=\"qxCheckBox\">审核入库单<br>");
//                }
//                if (htb.Contains((int)EnumCompanyPermission.StorageStorageInBrowseNouse))
//                {
//                    hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox34\" onclick=\"checkpid('checkbox21',this);\" type=\"checkbox\" value=\"29\"");
//                   hstr.Append("name=\"qxCheckBox\">无效入库单<br>");
//                }
//                if (htb.Contains((int)EnumCompanyPermission.StorageStorageInBrowseDelete))
//                {
//                    hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox35\" onclick=\"checkpid('checkbox21',this);\" type=\"checkbox\" value=\"30\"");
//                   hstr.Append("name=\"qxCheckBox\">删除入库单<br>");
//                }
//                if (htb.Contains((int)EnumCompanyPermission.StorageStorageInBrowseEdit))
//                {
//                    hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox36\" onclick=\"checkpid('checkbox21',this);\" type=\"checkbox\" value=\"31\"");
//                   hstr.Append("name=\"qxCheckBox\">编辑入库单<br>");
//                }
//                hstr.Append( "</div>");
//            }
//            if (htb.Contains((int)EnumCompanyPermission.StorageAdminCombineProduct))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox19\" type=\"checkbox\" value=\"32\" name=\"qxCheckBox\">组合产品<br>");
//            if (htb.Contains((int)EnumCompanyPermission.StorageStorageIn))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox20\" type=\"checkbox\" value=\"33\" name=\"qxCheckBox\">采购入库<br>");
//            if (htb.Contains((int)EnumCompanyPermission.StorageInLibSearch))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox22\" type=\"checkbox\" value=\"34\" name=\"qxCheckBox\">入库查询<br>");
//            if (htb.Contains((int)EnumCompanyPermission.StorageQueryOutStorageOrders))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox23\" type=\"checkbox\" value=\"35\" name=\"qxCheckBox\">出库查询<br>");
//            if (htb.Contains((int)EnumCompanyPermission.StorageKcglKcqk))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox24\" type=\"checkbox\" value=\"36\" name=\"qxCheckBox\">库存查询<br>");
//            if (htb.Contains((int)EnumCompanyPermission.StorageBrowseBills))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox25\" type=\"checkbox\" value=\"37\" name=\"qxCheckBox\">库存单据<br>");
//            if (htb.Contains((int)EnumCompanyPermission.StorageAddReportDamage))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox26\" type=\"checkbox\" value=\"38\" name=\"qxCheckBox\">库存报损<br>");
//            if (htb.Contains((int)EnumCompanyPermission.StorageAddReportProfit))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox27\" type=\"checkbox\" value=\"39\" name=\"qxCheckBox\">库存报益<br>");
//            if (htb.Contains((int)EnumCompanyPermission.StorageAddReWareHouse))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox28\" type=\"checkbox\" value=\"40\" name=\"qxCheckBox\">产品调拨<br>");
//            //                 <!--<IMG src=\"images/line1.gif\" align=absMiddle ><IMG src=\"images/line3.gif\" align=absMiddle ><input id=checkbox29 type=checkbox 
//            //      value=32 name=qxCheckBox>库存报益<br 
//            //      >-->
//            hstr.Append( "</div>");
//            #endregion
//            //库存管理结束
//            #region 物流管理
//            hstr.Append( "<div><IMG class=\"menutop\" id=\"plus3\" onclick=\"menu(menu3,img3,this)\" src=\"images/plus3.gif\"");
//           hstr.Append("align=\"absMiddle\"><IMG id=\"img3\" src=\"images/foldclose.gif\" align=\"absMiddle\">物流管理</div>");
//           hstr.Append("<div id=\"menu3\" style=\"MARGIN-TOP: -3px); DISPLAY: none\">");
//            if (htb.Contains((int)EnumCompanyPermission.LogisticsBrowseStoreOrders))
//            {
//                hstr.Append( "<div><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG class=\"menutop\" id=\"plus19\" onclick=\"menu(menu19,img19,this)\" src=\"images/plus3.gif\"");
//               hstr.Append("align=\"absMiddle\"><IMG id=\"img19\" src=\"images/foldclose.gif\" align=\"absMiddle\"><input id=\"checkbox37\" type=\"checkbox\" value=\"41\" name=\"qxCheckBox\">订单编辑<br></div>");
//               hstr.Append("<div id=\"menu19\" style=\"MARGIN-TOP: -3px); DISPLAY: none\">");
//                if (htb.Contains((int)EnumCompanyPermission.LogisticsBrowseStoreOrdersDelete))
//                {
//                    hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox54\" onclick=\"checkpid('checkbox37',this);\" type=\"checkbox\" value=\"42\"");
//                  hstr.Append("name=\"qxCheckBox\">删除订单<br>");
//                }
//                hstr.Append( "</div>");
//            }
//            if (htb.Contains((int)EnumCompanyPermission.LogisticsBillOutOrder))
//            {
//                hstr.Append( "<div><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG class=\"menutop\" id=\"plus18\" onclick=\"menu(menu18,img18,this)\" src=\"images/plus3.gif\"");
//               hstr.Append("align=\"absMiddle\"><IMG id=\"img18\" src=\"images/foldclose.gif\" align=\"absMiddle\"><input id=\"checkbox38\" type=\"checkbox\" value=\"43\" name=\"qxCheckBox\">订单出库<br></div>");
//               hstr.Append("<div id=\"menu18\" style=\"MARGIN-TOP: -3px); DISPLAY: none\">");
//                if (htb.Contains((int)EnumCompanyPermission.LogisticsBillOutOrderOut))
//                {
//                    hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox55\" onclick=\"checkpid('checkbox38',this);\" type=\"checkbox\" value=\"44\"");
//                   hstr.Append("name=\"qxCheckBox\">生成出库单<br>");
//                }
//                if (htb.Contains((int)EnumCompanyPermission.LogisticsBillOutOrderDelete))
//                {
//                    hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox56\" onclick=\"checkpid('checkbox38',this);\" type=\"checkbox\" value=\"45\"");
//                 hstr.Append("name=\"qxCheckBox\">撤消出库单<br>");
//                }
//                hstr.Append( "</div>");
//            }
//            if (htb.Contains((int)EnumCompanyPermission.LogisticsRefundmentOrderBrowse))
//            {
//                hstr.Append( "<div><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG class=\"menutop\" id=\"plus16\" onclick=\"menu(menu16,img16,this)\" src=\"images/plus3.gif\"");
//               hstr.Append("align=\"absMiddle\"><IMG id=\"img16\" src=\"images/foldclose.gif\" align=\"absMiddle\"><input id=\"checkbox42\" type=\"checkbox\" value=\"46\" name=\"qxCheckBox\">退货处理</div>");
//               hstr.Append("<div id=\"menu16\" style=\"MARGIN-TOP: -3px); DISPLAY: none\">");
//                if (htb.Contains((int)EnumCompanyPermission.LogisticsRefundmentOrderBrowseAuditing))
//                {
//                    hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox48\" onclick=\"checkpid('checkbox42',this);\" type=\"checkbox\" value=\"47\"");
//                  hstr.Append("name=\"qxCheckBox\">审核退货单<br>");
//                }
//                if (htb.Contains((int)EnumCompanyPermission.LogisticsRefundmentOrderBrowseNullity))
//                {
//                    hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox49\" onclick=\"checkpid('checkbox42',this);\" type=\"checkbox\" value=\"48\"");
//                 hstr.Append("name=\"qxCheckBox\">无效退货单<br>");
//                }
//                hstr.Append( "</div>");
//            }
//            if (htb.Contains((int)EnumCompanyPermission.LogisticsDisplaceGoodsBrowse))
//            {
//                hstr.Append("<div><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG class=\"menutop\" id=\"plus17\" onclick=\"menu(menu17,img17,this)\" src=\"images/plus3.gif\"");
//                hstr.Append("align=\"absMiddle\"><IMG id=\"img17\" src=\"images/foldclose.gif\" align=\"absMiddle\"><input id=\"checkbox43\" type=\"checkbox\" value=\"49\" name=\"qxCheckBox\">换货处理</div>");
//                hstr.Append("<div id=\"menu17\" style=\"MARGIN-TOP: -3px); DISPLAY: none\">");
//                if (htb.Contains((int)EnumCompanyPermission.LogisticsDisplaceGoodsBrowseAuditing))
//                {
//                    hstr.Append("<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox50\" onclick=\"checkpid('checkbox43',this);\" type=\"checkbox\" value=\"50\" ");
//                    hstr.Append("name=\"qxCheckBox\">审核换货单<br>");
//                }
//                if (htb.Contains((int)EnumCompanyPermission.LogisticsDisplaceGoodsBrowseNullity))
//                {
//                    hstr.Append("<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox51\" onclick=\"checkpid('checkbox43',this);\" type=\"checkbox\" value=\"51\" ");
//                    hstr.Append("name=\"qxCheckBox\">无效换货单<br>");
//                }
//                if (htb.Contains((int)EnumCompanyPermission.LogisticsDisplaceGoodsBrowseEdit))
//                {
//                    hstr.Append("<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox52\" onclick=\"checkpid('checkbox43',this);\" type=\"checkbox\" value=\"52\" ");
//                    hstr.Append("name=\"qxCheckBox\">编辑换货单<br>");
//                }
//                if (htb.Contains((int)EnumCompanyPermission.LogisticsDisplaceGoodsBrowseDelete))
//                {
//                    hstr.Append(" <IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox53\" onclick=\"checkpid('checkbox43',this);\" type=\"checkbox\" value=\"53\" ");
//                    hstr.Append("name=\"qxCheckBox\">删除换货单<br>");
//                }
//                hstr.Append("</div>");
//            }
//            if (htb.Contains((int)EnumCompanyPermission.LogisticsCompanyConsign))
//            {
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox39\" type=\"checkbox\" value=\"54\" name=\"qxCheckBox\">订单发货<br>");
//            }
//            if (htb.Contains((int)EnumCompanyPermission.LogisticsDisplaceGoodsBrowse))
//            {
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox41\" type=\"checkbox\" value=\"55\" name=\"qxCheckBox\">订单跟踪<br>");
//            }
//            //<!--<IMG src=\"images/line1.gif\" align=absMiddle ><IMG src=\"images/line3.gif\" align=absMiddle ><input id=checkbox44 type=checkbox 
//            //      value=47 name=qxCheckBox>产品销售统计<br 
//            //      >-->
//            hstr.Append( "</div>");
//            #endregion
//            //物流管理结束
//            #region 财务管理
//            hstr.Append( "<div><IMG class=\"menutop\" id=\"plus4\" onclick=\"menu(menu4,img4,this)\" src=\"images/plus3.gif\"");
//         hstr.Append("align=\"absMiddle\"><IMG id=\"img4\" src=\"images/foldclose.gif\" align=\"absMiddle\">财务管理</div>");
//         hstr.Append("<div id=\"menu4\" style=\"MARGIN-TOP: -3px); DISPLAY: none\">");
//            if (htb.Contains((int)EnumCompanyPermission.FinanceAuditingStoreAccount))
//            {
//                hstr.Append( "<div><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG class=\"menutop\" id=\"plus21\" onclick=\"menu(menu21,img21,this)\" src=\"images/plus3.gif\"");
//             hstr.Append("align=\"absMiddle\"><IMG id=\"img21\" src=\"images/foldclose.gif\" align=\"absMiddle\"><input id=\"checkbox58\" type=\"checkbox\" value=\"56\" name=\"qxCheckBox\">预收帐款</div>");
//             hstr.Append("<div id=\"menu21\" style=\"MARGIN-TOP: -3px); DISPLAY: none\">");
//             if (htb.Contains((int)EnumCompanyPermission.FinanceAuditingStoreAccountAuditing))
//             {
//                 hstr.Append("<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox73\" onclick=\"checkpid('checkbox58',this);\" type=\"checkbox\" value=\"57\"");
//                 hstr.Append("name=\"qxCheckBox\">审核店铺款<br>");
//             }
//                //                    <!--<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox74\" onclick=\"checkpid('checkbox58',this);\" type=\"checkbox\" value=\"58\"
//                //                        name=\"qxCheckBox\">汇款入账--><br>
//                hstr.Append( "</div>");
//            }
//            if (htb.Contains((int)EnumCompanyPermission.FinanceKoukuanMingxi))
//            {
//                hstr.Append( "<div><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG class=\"menutop\" id=\"plus22\" onclick=\"menu(menu22,img22,this)\" src=\"images/plus3.gif\"");
//             hstr.Append("align=\"absMiddle\"><IMG id=\"img22\" src=\"images/foldclose.gif\" align=\"absMiddle\"><input id=\"checkbox65\" type=\"checkbox\" value=\"59\" name=\"qxCheckBox\">扣补款管理</div>");
//           hstr.Append("<div id=\"menu22\" style=\"MARGIN-TOP: -3px); DISPLAY: none\"><IMG src=\"images/line1.gif\" align=\"absMiddle\">");
//                if (htb.Contains((int)EnumCompanyPermission.FinanceKoukuanAddkou))
//                {
//                    hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox75\" onclick=\"checkpid('checkbox65',this);\" type=\"checkbox\" value=\"60\"");
//                   hstr.Append(" name=\"qxCheckBox\">添加扣款<br>");
//                }
//                if (htb.Contains((int)EnumCompanyPermission.FinanceKoukuanAddbu))
//                {
//                    hstr.Append("<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox76\" onclick=\"checkpid('checkbox65',this);\" type=\"checkbox\" value=\"61\"");
//                    hstr.Append(" name=\"qxCheckBox\">添加补款<br>");
//                }
//                hstr.Append( "</div>");
//            }
//            if (htb.Contains((int)EnumCompanyPermission.FinanceGrantReward))
//            {
//                hstr.Append( "<div><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG class=\"menutop\" id=\"plus23\" onclick=\"menu(menu23,img23,this)\" src=\"images/plus3.gif\"");
//                 hstr.Append("align=\"absMiddle\"><IMG id=\"img23\" src=\"images/foldclose.gif\" align=\"absMiddle\"><input id=\"checkbox67\" type=\"checkbox\" value=\"62\" name=\"qxCheckBox\">工资汇兑</div>");
//         hstr.Append("<div id=\"menu23\" style=\"MARGIN-TOP: -3px); DISPLAY: none\">");
//                if (htb.Contains((int)EnumCompanyPermission.FinanceGrantRewardFafang))
//                {
//                    hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox78\" onclick=\"checkpid('checkbox67',this);\" type=\"checkbox\" value=\"63\"");
//                   hstr.Append(" name=\"qxCheckBox\">工资发放<br>");
//                }
//                if (htb.Contains((int)EnumCompanyPermission.FinanceGrantRewardDaochu))
//                {
//                    hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox79\" onclick=\"checkpid('checkbox67',this);\" type=\"checkbox\" value=\"64\"");
//                   hstr.Append(" name=\"qxCheckBox\">工资导出<br>");
//                }
//                hstr.Append( "</div>");
//            }
//            if (htb.Contains((int)EnumCompanyPermission.FinanceHuiDuiChongHong))
//            {
//                hstr.Append( "<div><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG class=\"menutop\" id=\"plus24\" onclick=\"menu(menu24,img24,this)\" src=\"images/plus3.gif\"");
//                hstr.Append("align=\"absMiddle\"><IMG id=\"img24\" src=\"images/foldclose.gif\" align=\"absMiddle\"><input id=\"checkbox64\" type=\"checkbox\" value=\"65\" name=\"qxCheckBox\">工资退回</div>");
//         hstr.Append("<div id=\"menu24\" style=\"MARGIN-TOP: -3px); DISPLAY: none\">");
//                if (htb.Contains((int)EnumCompanyPermission.FinanceTianJiaChongHong))
//                {
//                    hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox80\" onclick=\"checkpid('checkbox64',this);\" type=\"checkbox\" value=\"66\"");
//                                     hstr.Append(" name=\"qxCheckBox\">会员工资退回<br>");
//                }
//                hstr.Append( "</div>");
//            }
//            if (htb.Contains((int)EnumCompanyPermission.FinanceAddStoreTotalAccount))
//                hstr.Append( " <IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox59\" type=\"checkbox\" value=\"67\" name=\"qxCheckBox\">应收账款<br>");
//            if (htb.Contains((int)EnumCompanyPermission.FinanceChAgainOrder))
//                hstr.Append( " <IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox60\" type=\"checkbox\" value=\"58\" name=\"qxCheckBox\">订单支付<br>");
//            if (htb.Contains((int)EnumCompanyPermission.FinanceMoneymanage))
//                hstr.Append( "  <IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox61\" type=\"checkbox\" value=\"69\" name=\"qxCheckBox\">电子转账<br>");
//            if (htb.Contains((int)EnumCompanyPermission.FinanceReceiveConfirm))
//                hstr.Append( "   <IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox62\" type=\"checkbox\" value=\"70\" name=\"qxCheckBox\">转账管理<br>");
//            if (htb.Contains((int)EnumCompanyPermission.FinanceJiangjinshezhi))
//                hstr.Append( "   <IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox63\" type=\"checkbox\" value=\"71\" name=\"qxCheckBox\">工资发放<br>");
//            if (htb.Contains((int)EnumCompanyPermission.FinanceSetCompanyAccount))
//                hstr.Append( " <IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox66\" type=\"checkbox\" value=\"72\" name=\"qxCheckBox\">账户管理<br>");
//            if (htb.Contains((int)EnumCompanyPermission.FinanceTuihuokuanManage))
//                hstr.Append( " <IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox68\" type=\"checkbox\" value=\"73\" name=\"qxCheckBox\">退货款管理<br>");
//            if (htb.Contains((int)EnumCompanyPermission.FinanceFinanceStat))
//                hstr.Append( " <IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox69\" type=\"checkbox\" value=\"74\" name=\"qxCheckBox\">拨出率显示<br>");
//            //if (htb.Contains((int)EnumCompanyPermission.FinanceHuiDuiChongHong))
//            //    hstr.Append( " <IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox70\" type=\"checkbox\" value=\"75\" name=\"qxCheckBox\">拨出率调控<br>");
//            //if (htb.Contains((int)EnumCompanyPermission.FinanceHuiDuiChongHong))
//            //    hstr.Append( " <IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox77\" type=\"checkbox\" value=\"76\" name=\"qxCheckBox\">购物券管理<br>");
//            hstr.Append( "</div>");
//            #endregion
//            //财务管理结束
//            #region 报表中心
//            hstr.Append( "<div><IMG class=\"menutop\" id=\"plus40\" onclick=\"menu(menu40,img40,this)\" src=\"images/plus3.gif\"");
//             hstr.Append("align=\"absMiddle\"><IMG id=\"img40\" src=\"images/foldclose.gif\" align=\"absMiddle\">报表中心</div>");
//             hstr.Append("<div id=\"menu40\" style=\"MARGIN-TOP: -3px); DISPLAY: none\">");
//            if (htb.Contains((int)EnumCompanyPermission.ReportMoneyReport))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox142\" type=\"checkbox\" value=\"142\" name=\"qxCheckBox\">店铺汇款总汇<br>");
//            if (htb.Contains((int)EnumCompanyPermission.ReportMoneyDetailReport))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox143\" type=\"checkbox\" value=\"143\" name=\"qxCheckBox\">店铺汇款明细<br>");
//            if (htb.Contains((int)EnumCompanyPermission.ReportOrderReport))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox144\" type=\"checkbox\" value=\"144\" name=\"qxCheckBox\">店铺订单总汇<br>");
//            if (htb.Contains((int)EnumCompanyPermission.ReportOrderDetail))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox145\" type=\"checkbox\" value=\"145\" name=\"qxCheckBox\">店铺订单明细<br>");
//            if (htb.Contains((int)EnumCompanyPermission.ReportBaodanReport))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox146\" type=\"checkbox\" value=\"146\" name=\"qxCheckBox\">会员销售总汇<br>");
//            if (htb.Contains((int)EnumCompanyPermission.ReportBaodanDetailReport))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox147\" type=\"checkbox\" value=\"147\" name=\"qxCheckBox\">会员销售明细<br>");
//            if (htb.Contains((int)EnumCompanyPermission.ReportProductDetail))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox148\" type=\"checkbox\" value=\"148\" name=\"qxCheckBox\">产品明细表<br>");
//            if (htb.Contains((int)EnumCompanyPermission.ReportStockReport1))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox149\" type=\"checkbox\" value=\"149\" name=\"qxCheckBox\">仓库各产品明细<br>");
//            if (htb.Contains((int)EnumCompanyPermission.ReportStockReport3))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox150\" type=\"checkbox\" value=\"150\" name=\"qxCheckBox\">产品各仓库明细<br>");
//            if (htb.Contains((int)EnumCompanyPermission.ReportStockDetailReport))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox151\" type=\"checkbox\" value=\"151\" name=\"qxCheckBox\">产品销售明细<br>");
//            if (htb.Contains((int)EnumCompanyPermission.ReportStockReport2))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox152\" type=\"checkbox\" value=\"152\" name=\"qxCheckBox\">店铺各产品明细<br>");
//            if (htb.Contains((int)EnumCompanyPermission.ReportStockReport4))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox153\" type=\"checkbox\" value=\"153\" name=\"qxCheckBox\">产品各店铺明细<br>");
//            if (htb.Contains((int)EnumCompanyPermission.ReportMemberDetail2))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox154\" type=\"checkbox\" value=\"154\" name=\"qxCheckBox\">会员名单<br>");
//            if (htb.Contains((int)EnumCompanyPermission.ReportMemberReport))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox155\" type=\"checkbox\" value=\"155\" name=\"qxCheckBox\">会员分布情况<br>");
//            if (htb.Contains((int)EnumCompanyPermission.ReportStoreDetail2))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox156\" type=\"checkbox\" value=\"156\" name=\"qxCheckBox\">店铺名单<br>");
//            if (htb.Contains((int)EnumCompanyPermission.ReportStorereport))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox157\" type=\"checkbox\" value=\"157\" name=\"qxCheckBox\">店铺分布情况<br>");
//            hstr.Append( "</div>");
//            #endregion
//            //报表中心结束
//            #region 结算中心
//            hstr.Append("<div><IMG class=\"menutop\" id=\"plus5\" onclick=\"menu(menu5,img5,this)\" src=\"images/plus3.gif\"");
//                        hstr.Append(" align=\"absMiddle\"><IMG id=\"img5\" src=\"images/foldclose.gif\" align=\"absMiddle\">结算中心</div>");
//                 hstr.Append("<div id=\"menu5\" style=\"MARGIN-TOP: -3px); DISPLAY: none\">");
//            if (htb.Contains((int)EnumCompanyPermission.BalanceBrowseMemberOrders))
//            {
//                hstr.Append( "<div><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG class=\"menutop\" id=\"plus25\" onclick=\"menu(menu25,img25,this)\" src=\"images/plus3.gif\"");
//               hstr.Append("align=\"absMiddle\"><IMG id=\"img25\" src=\"images/foldclose.gif\" align=\"absMiddle\"><input id=\"checkbox82\" type=\"checkbox\" value=\"77\" name=\"qxCheckBox\">报单浏览</div>");
//         hstr.Append("<div id=\"menu25\" style=\"MARGIN-TOP: -3px); DISPLAY: none\">");
//                if (htb.Contains((int)EnumCompanyPermission.BalanceBrowseMemberOrdersEdit))
//                {
//                    hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox95\" onclick=\"checkpid('checkbox82',this);\" type=\"checkbox\" value=\"78\"");
//                                    hstr.Append(" name=\"qxCheckBox\">修改会员<br>");
//                }
//                if (htb.Contains((int)EnumCompanyPermission.BalanceBrowseMemberOrdersDelete))
//                {
//                    hstr.Append("<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox96\" onclick=\"checkpid('checkbox82',this);\" type=\"checkbox\" value=\"79\"");
//                    hstr.Append(" name=\"qxCheckBox\">删除会员<br>");
//                }
//                hstr.Append( "</div>");
//            }
//            if (htb.Contains((int)EnumCompanyPermission.BalanceVIPCardManage))
//            {
//                hstr.Append( "<div><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG class=\"menutop\" id=\"plus26\" onclick=\"menu(menu26,img26,this)\" src=\"images/plus3.gif\"");
//                 hstr.Append("align=\"absMiddle\"><IMG id=\"img26\" src=\"images/foldclose.gif\" align=\"absMiddle\"><input id=\"checkbox90\" type=\"checkbox\" value=\"80\" name=\"qxCheckBox\">编号分配</div>");
//                 hstr.Append("<div id=\"menu26\" style=\"MARGIN-TOP: -3px); DISPLAY: none\">");
//                 if (htb.Contains((int)EnumCompanyPermission.BalanceVIPCardManageReset))
//                 {
//                     hstr.Append("<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox97\" onclick=\"checkpid('checkbox90',this);\" type=\"checkbox\" value=\"81\"");
//                     hstr.Append(" name=\"qxCheckBox\">取消分配卡号<br>");
//                 }
//                 if (htb.Contains((int)EnumCompanyPermission.BalanceVIPCardManageAdd))
//                 {
//                     hstr.Append("<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox98\" onclick=\"checkpid('checkbox90',this);\" type=\"checkbox\" value=\"82\"");
//                     hstr.Append(" name=\"qxCheckBox\">重新分配到店铺<br>");
//                 }
//                hstr.Append( "</div>");
//            }
//            if (htb.Contains((int)EnumCompanyPermission.BalanceQueryNetworkViewDP))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox81\" type=\"checkbox\" value=\"83\" name=\"qxCheckBox\">店铺网络图<br>");
//            if (htb.Contains((int)EnumCompanyPermission.BalanceQueryTuiJianNetworkView))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox83\" type=\"checkbox\" value=\"84\" name=\"qxCheckBox\">推荐网络图<br>");
//            if (htb.Contains((int)EnumCompanyPermission.BalanceQueryAnZhiNetworkView))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox84\" type=\"checkbox\" value=\"85\" name=\"qxCheckBox\">安置网络图<br>");
//            if (htb.Contains((int)EnumCompanyPermission.BalanceQueryLinkView))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox85\" type=\"checkbox\" value=\"86\" name=\"qxCheckBox\">链路网络图<br>");
//            if (htb.Contains((int)EnumCompanyPermission.BalanceDetialQuery))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox86\" type=\"checkbox\" value=\"87\" name=\"qxCheckBox\">高级查询<br>");
//            if (htb.Contains((int)EnumCompanyPermission.BalanceCompanyBalance))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox87\" type=\"checkbox\" value=\"88\" name=\"qxCheckBox\">工资结算<br>");
//            if (htb.Contains((int)EnumCompanyPermission.BalanceConfig))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox88\" type=\"checkbox\" value=\"89\" name=\"qxCheckBox\">结算参数<br>");
//            if (htb.Contains((int)EnumCompanyPermission.BalanceDefault))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox89\" type=\"checkbox\" value=\"90\" name=\"qxCheckBox\">网络调整<br>");
//            if (htb.Contains((int)EnumCompanyPermission.BalanceMemberLevelModify))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox91\" type=\"checkbox\" value=\"91\" name=\"qxCheckBox\">会员定级<br>");
//            if (htb.Contains((int)EnumCompanyPermission.BalanceStoreLevelModify))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox92\" type=\"checkbox\" value=\"92\" name=\"qxCheckBox\">店铺定级<br>");
//            if (htb.Contains((int)EnumCompanyPermission.BalanceRegisterMember1))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox93\" type=\"checkbox\" value=\"93\" name=\"qxCheckBox\">特殊会员注册<br>");
//            if (htb.Contains((int)EnumCompanyPermission.BalanceMemberOrderAgain1))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox94\" type=\"checkbox\" value=\"94\" name=\"qxCheckBox\">特殊会员报单<br>");
//            if (htb.Contains((int)EnumCompanyPermission.Balancetk_Config))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox130\" type=\"checkbox\" value=\"130\" name=\"qxCheckBox\">调控结算参数<br>");
//            if (htb.Contains((int)EnumCompanyPermission.Balancetk_Team))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox131\" type=\"checkbox\" value=\"131\" name=\"qxCheckBox\">调控团队<br>");
//            if (htb.Contains((int)EnumCompanyPermission.Balancetk_CompanyBalance))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox132\" type=\"checkbox\" value=\"132\" name=\"qxCheckBox\">调控结算<br>");
//            hstr.Append( "</div>");
//            #endregion
//            //结算中心结束
//            #region 信息管理
//            hstr.Append( "<div><IMG class=\"menutop\" id=\"plus6\" onclick=\"menu(menu6,img6,this)\" src=\"images/plus3.gif\"");
//            hstr.Append(" align=\"absMiddle\"><IMG id=\"img6\" src=\"images/foldclose.gif\" align=\"absMiddle\">信息管理</div>");
//     hstr.Append("<div id=\"menu6\" style=\"MARGIN-TOP: -3px); DISPLAY: none\">");
//            if (htb.Contains((int)EnumCompanyPermission.ManageMessageRecive))
//            {
//                hstr.Append( "<div><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG class=\"menutop\" id=\"plus27\" onclick=\"menu(menu27,img27,this)\" src=\"images/plus3.gif\"");
//                hstr.Append(" align=\"absMiddle\"><IMG id=\"img27\" src=\"images/foldclose.gif\" align=\"absMiddle\"><input id=\"checkbox101\" type=\"checkbox\" value=\"95\" name=\"qxCheckBox\">收件箱</div>");
//     hstr.Append("<div id=\"menu27\" style=\"MARGIN-TOP: -3px); DISPLAY: none\">");
//                if (htb.Contains((int)EnumCompanyPermission.ManageMessageReciveDelete))
//                {
//                    hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox109\" onclick=\"checkpid('checkbox101',this);\" type=\"checkbox\" value=\"96\"");
//                                   hstr.Append(" name=\"qxCheckBox\">删除收件箱<br>");
//                }
//                if (htb.Contains((int)EnumCompanyPermission.ManageMessageSendDelete))
//                {
//                    hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox110\" onclick=\"checkpid('checkbox101',this);\" type=\"checkbox\" value=\"97\"");
//                 hstr.Append("name=\"qxCheckBox\">删除发件箱<br>");
//                }
//                if (htb.Contains((int)EnumCompanyPermission.ManageMessageSendDelete))
//                {
//                    hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox111\" onclick=\"checkpid('checkbox101',this);\" type=\"checkbox\" value=\"98\"");
//                 hstr.Append("name=\"qxCheckBox\">删除废件箱<br>");
//                }
//                hstr.Append( "</div>");
//            }
//            if (htb.Contains((int)EnumCompanyPermission.ManageMessage1))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox99\" type=\"checkbox\" value=\"99\" name=\"qxCheckBox\">内部公告信息发布<br>");
//            if (htb.Contains((int)EnumCompanyPermission.ManageMessage))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox100\" type=\"checkbox\" value=\"100\" name=\"qxCheckBox\">写邮件<br>");
//            if (htb.Contains((int)EnumCompanyPermission.ManageMessageSend))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox102\" type=\"checkbox\" value=\"101\" name=\"qxCheckBox\">发件箱<br>");
//            if (htb.Contains((int)EnumCompanyPermission.ManageMessageDrop))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox103\" type=\"checkbox\" value=\"102\" name=\"qxCheckBox\">废件箱<br>");
//            if (htb.Contains((int)EnumCompanyPermission.GroupSend))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox104\" type=\"checkbox\" value=\"103\" name=\"qxCheckBox\">短信群发<br>");
//            if (htb.Contains((int)EnumCompanyPermission.ReceiveBox))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox106\" type=\"checkbox\" value=\"105\" name=\"qxCheckBox\">接收查询<br>");
//            if (htb.Contains((int)EnumCompanyPermission.SendBox))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox107\" type=\"checkbox\" value=\"106\" name=\"qxCheckBox\">发送查询<br>");
//            if (htb.Contains((int)EnumCompanyPermission.RightSet))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><INPUT id=\"Checkbox29\" type=\"checkbox\" value=\"115\" name=\"qxCheckBox\">权限设置<br>");
//            if (htb.Contains((int)EnumCompanyPermission.InstructionsListx))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><INPUT id=\"Checkbox44\" type=\"checkbox\" value=\"116\" name=\"qxCheckBox\">指令集合<br>");
//            if (htb.Contains((int)EnumCompanyPermission.WaitSms))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox108\" type=\"checkbox\" value=\"107\" name=\"qxCheckBox\">待发短信<br>");
//            if (htb.Contains((int)EnumCompanyPermission.ManageResource))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox160\" type=\"checkbox\" value=\"108\" name=\"qxCheckBox\">资料上传<br>");
//            if (htb.Contains((int)EnumCompanyPermission.ManageQueryGongGao))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox161\" type=\"checkbox\" value=\"109\" name=\"qxCheckBox\">公告查询");
//            hstr.Append( "</div>");
//            #endregion
//            //信息管理结束
//            #region 系统管理
//            hstr.Append( "<div><IMG class=\"menutop\" id=\"plus7\" onclick=\"menu(menu7,img7,this)\" src=\"images/plus3.gif\"");
//            hstr.Append(" align=\"absMiddle\"><IMG id=\"img7\" src=\"images/foldclose.gif\" align=\"absMiddle\">系统管理</div>");
//     hstr.Append("<div id=\"menu7\" style=\"MARGIN-TOP: -3px); DISPLAY: none\">");
//            if (htb.Contains((int)EnumCompanyPermission.SystemIntoDict))
//            {
//                hstr.Append( "<div><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG class=\"menutop\" id=\"plus28\" onclick=\"menu(menu28,img28,this)\" src=\"images/plus3.gif\"");
//                hstr.Append(" align=\"absMiddle\"><IMG id=\"img28\" src=\"images/foldclose.gif\" align=\"absMiddle\"><input id=\"checkbox117\" type=\"checkbox\" value=\"110\" name=\"qxCheckBox\">多语言翻译</div><div id=\"menu28\" style=\"MARGIN-TOP: -3px); DISPLAY: none\">");
//                if (htb.Contains((int)EnumCompanyPermission.SystemIntoDictEdit))
//                {
//                    hstr.Append("<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox120\" onclick=\"checkpid('checkbox117',this);\" type=\"checkbox\" value=\"111\"");
//                    hstr.Append(" name=\"qxCheckBox\">翻译多语言<br>");
//                }
//                hstr.Append( " </div>");
//            }
//            if (htb.Contains((int)EnumCompanyPermission.SystemCurrencyRate))
//            {
//                hstr.Append( "<div><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG class=\"menutop\" id=\"plus29\" onclick=\"menu(menu29,img29,this)\" src=\"images/plus3.gif\"");
//                hstr.Append(" align=\"absMiddle\"><IMG id=\"img29\" src=\"images/foldclose.gif\" align=\"absMiddle\"><input id=\"checkbox118\" type=\"checkbox\" value=\"112\" name=\"qxCheckBox\">汇率设置</div><div id=\"menu29\" style=\"MARGIN-TOP: -3px); DISPLAY: none\">");
//                if (htb.Contains((int)EnumCompanyPermission.SystemCurrencyRateCountryEdit))
//                {
//                    hstr.Append("<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox121\" onclick=\"checkpid('checkbox118',this);\" type=\"checkbox\" value=\"113\"");
//                    hstr.Append(" name=\"qxCheckBox\">编辑汇率<br>");
//                }
//                if (htb.Contains((int)EnumCompanyPermission.SystemCurrencyRateCountryDelete))
//                {
//                    hstr.Append("<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox122\" onclick=\"checkpid('checkbox118',this);\" type=\"checkbox\" value=\"114\"");
//                    hstr.Append(" name=\"qxCheckBox\">编辑国家或地区<br>");
//                }
//                hstr.Append( "</div>");
//            }
//            //                <div><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG class=\"menutop\" id=\"plus30\" onclick=\"menu(menu30,img30,this)\" src=\"images/plus3.gif\"
//            //                        align=\"absMiddle\"><IMG id=\"img30\" src=\"images/foldclose.gif\" align=\"absMiddle\"><input id=\"checkbox114\" type=\"checkbox\" value=\"115\" name=\"qxCheckBox\">进销存相关设置</div>
//            //                <div id=\"menu30\" style=\"MARGIN-TOP: -3px); DISPLAY: none\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox124\" onclick=\"checkpid('checkbox114',this);\" type=\"checkbox\" value=\"116\"
//            //                        name=\"qxCheckBox\">添加进销存相关设置<br>
//            //                    <IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox125\" onclick=\"checkpid('checkbox114',this);\" type=\"checkbox\" value=\"117\"
//            //                        name=\"qxCheckBox\">修改进销存相关设置<br>
//            //                    <IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox126\" onclick=\"checkpid('checkbox114',this);\" type=\"checkbox\" value=\"118\"
//            //                        name=\"qxCheckBox\">删除进销存相关设置<br>
//            //                </div>
//            if (htb.Contains((int)EnumCompanyPermission.SystemPwdModify))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox112\" type=\"checkbox\" value=\"119\" name=\"qxCheckBox\">密码修改<br>");
//            if (htb.Contains((int)EnumCompanyPermission.SystemBackup))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox113\" type=\"checkbox\" value=\"120\" name=\"qxCheckBox\">数据备份<br>");
//            if (htb.Contains((int)EnumCompanyPermission.SystemChangeLogsQuery))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox115\" type=\"checkbox\" value=\"121\" name=\"qxCheckBox\">系统日志管理<br>");
//            if (htb.Contains((int)EnumCompanyPermission.SystemBscoManage))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox116\" type=\"checkbox\" value=\"122\" name=\"qxCheckBox\">参数设置<br>");
//            if (htb.Contains((int)EnumCompanyPermission.SystemQishuVSdate))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox119\" type=\"checkbox\" value=\"123\" name=\"qxCheckBox\">期数显示<br>");
//            if (htb.Contains((int)EnumCompanyPermission.SystemQishuVSdateEdit))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox123\" type=\"checkbox\" value=\"124\" name=\"qxCheckBox\">修改期数时间");
//            hstr.Append( "</div>");
//            #endregion
//            //系统管理结束
//            #region 安全控制
//            hstr.Append( "<div><IMG class=\"menutop\" id=\"plus8\" onclick=\"menu(menu8,img8,this)\" src=\"images/plus3.gif\"");
//                        hstr.Append(" align=\"absMiddle\"><IMG id=\"img8\" src=\"images/foldclose.gif\" align=\"absMiddle\">安全控制");
//                 hstr.Append("</div>");
//                 hstr.Append("<div id=\"menu8\" style=\"MARGIN-TOP: -3px); DISPLAY: none\">");
//                 if (htb.Contains((int)EnumCompanyPermission.SafeRightManage))
//                 {
//                     hstr.Append("<div><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG class=\"menutop\" id=\"plus31\" onclick=\"menu(menu31,img31,this)\" src=\"images/plus3.gif\"");
//                     hstr.Append(" align=\"absMiddle\"><IMG id=\"img31\" src=\"images/foldclose.gif\" align=\"absMiddle\"><input id=\"checkbox127\" type=\"checkbox\" value=\"125\" name=\"qxCheckBox\">管理员权限分配</div>");
//                     hstr.Append("<div id=\"menu31\" style=\"MARGIN-TOP: -3px); DISPLAY: none\">");
//                     if (htb.Contains((int)EnumCompanyPermission.SafeRightManageEdit))
//                     {
//                         hstr.Append("<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox140\" onclick=\"checkpid('checkbox127',this);\" type=\"checkbox\" value=\"126\"");
//                         hstr.Append(" name=\"qxCheckBox\">编辑管理员权限<br>");
//                     }
//                     if (htb.Contains((int)EnumCompanyPermission.SafeRightManageDelete))
//                     {
//                         hstr.Append("<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox141\" onclick=\"checkpid('checkbox127',this);\" type=\"checkbox\" value=\"127\"");
//                         hstr.Append(" name=\"qxCheckBox\">删除管理员权限<br>");
//                     }
//                     if (htb.Contains((int)EnumCompanyPermission.SafeRightDpetManage))
//                     {
//                         hstr.Append("<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\"><input id=\"checkbox141\" onclick=\"checkpid('checkbox127',this);\" type=\"checkbox\" value=\"140\"");
//                         hstr.Append(" name=\"qxCheckBox\">系统部门管理(操作部门信息)<br>");
//                     }
//                     hstr.Append("</div>");
//                 }
//            if (htb.Contains((int)EnumCompanyPermission.SafeLoginSetting))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox128\" type=\"checkbox\" value=\"128\" name=\"qxCheckBox\">系统总开关<br>");
//            if (htb.Contains((int)EnumCompanyPermission.SafeDetailQuerySetting))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox129\" type=\"checkbox\" value=\"129\" name=\"qxCheckBox\">查询权限控制<br>");
////                <IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox130\" type=\"checkbox\" value=\"130\" name=\"qxCheckBox\">应急措施<br>
////                <IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox131\" type=\"checkbox\" value=\"131\" name=\"qxCheckBox\">财务安全处理<br>
////                <IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox132\" type=\"checkbox\" value=\"132\" name=\"qxCheckBox\">服务器安全处理<br>
//            if (htb.Contains((int)EnumCompanyPermission.SafeLoginSettingArea))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox133\" type=\"checkbox\" value=\"133\" name=\"qxCheckBox\">地区登录限制<br>");
//            if (htb.Contains((int)EnumCompanyPermission.SafeLoginSettingIP))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox134\" type=\"checkbox\" value=\"134\" name=\"qxCheckBox\">IP登录限制<br>");
//            if (htb.Contains((int)EnumCompanyPermission.SafeLoginSettingMember))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox135\" type=\"checkbox\" value=\"135\" name=\"qxCheckBox\">会员登录限制<br>");
//            if (htb.Contains((int)EnumCompanyPermission.SafeLoginSettingStore))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox136\" type=\"checkbox\" value=\"136\" name=\"qxCheckBox\">店铺登录限制<br>");
//            if (htb.Contains((int)EnumCompanyPermission.SafeLoginSettingStoreArea))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox137\" type=\"checkbox\" value=\"137\" name=\"qxCheckBox\">店辖会员登录限制<br>");
//            if (htb.Contains((int)EnumCompanyPermission.SafeLoginSettingNetWork))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox138\" type=\"checkbox\" value=\"138\" name=\"qxCheckBox\">网络团队登录限制<br>");
//            if (htb.Contains((int)EnumCompanyPermission.SafeGlobalOrderSettings))
//                hstr.Append( "<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\"><input id=\"checkbox139\" type=\"checkbox\" value=\"139\" name=\"qxCheckBox\">报单设置<br>");
//            hstr.Append( "</div>");
//            #endregion
//            //安全控制结束
//            #region 仓库管理
//            hstr.Append("<div><IMG class=\"menutop\" id=\"plus9\" onclick=\"menu(menu9,img9,this)\" src=\"images/plus3.gif\"");
//            hstr.Append("align=\"absMiddle\"><IMG id=\"img9\" src=\"images/foldclose.gif\" align=\"absMiddle\">仓库控制</div>");
//            hstr.Append("<div id=\"menu9\" style=\"MARGIN-TOP: -3px); DISPLAY: none\">");
//            string sql = "select WareControl,WareHouseName from WareHouse";
//            SqlDataReader dr = DBHelper.ExecuteReader(sql, CommandType.Text);
//            if (number == "")
//            {
//                while (dr.Read())
//                {
//                    hstr.Append("<IMG src=images/line1.gif align=absMiddle ><IMG src=images/line3.gif align=absMiddle ><input id=checkbox"); hstr.Append(dr.GetInt32(0).ToString()); hstr.Append(" type=checkbox value="); hstr.Append(dr.GetInt32(0).ToString()); hstr.Append(" name=qxCheckBox>"); hstr.Append(dr.GetString(1).ToString()); hstr.Append("<br>");
//                }
//            }
//            else
//            {
//                while (dr.Read())
//                {
//                    if (htb.Contains(dr.GetInt32(0)))
//                    {
//                        hstr.Append("<IMG src=images/line1.gif align=absMiddle ><IMG src=images/line3.gif align=absMiddle ><input id=checkbox"); hstr.Append(dr.GetInt32(0).ToString());
//                        hstr.Append(" type=checkbox value="); hstr.Append(dr.GetInt32(0).ToString()); hstr.Append(" name=qxCheckBox>"); hstr.Append(dr.GetString(1).ToString()); hstr.Append("<br>");
//                    }
//                }

//            }
//            dr.Close();
//            hstr.Append( "</div>");
//            #endregion
            #endregion

            return hstr.ToString();
        }
        #endregion


        public static string GetWareHousePer(string number,Hashtable htb,int roles)
        {
            string field = System.Web.HttpContext.Current.Session["LanguageCode"].ToString();
            string strSql ="select top 1 " + field + " from T_translation where keycode='005832'";
            string wareHouseStr = DAL.DBHelper.ExecuteScalar(strSql).ToString();//DBHelper.ExecuteScalar(strSql, null, CommandType.Text).ToString();
            StringBuilder hstr = new StringBuilder();
           
            string sql = "select WareControl,WareHouseName from WareHouse w,ManagerPermission m where w.warecontrol = m.permissionid and m.managerid=@roles";
            SqlParameter[] para = {
                                      new SqlParameter("@roles",SqlDbType.Int)
                                  };
            para[0].Value = roles;
            SqlDataReader dr = DBHelper.ExecuteReader(sql, para,CommandType.Text);
            int count = 0;
            if (number == DAL.DBHelper.ExecuteScalar("select top 1 number from manage where defaultmanager=1").ToString())
            {
                while (dr.Read())
                {
                    if (count == 0)
                    {
                        hstr.Append("<div id=\"menu__9\"><IMG class=\"menutop\" id=\"plus_9\" onclick=\"menu('menu_9','img_9',this)\" src=\"images/plus3.gif\"");
                        hstr.Append("align=\"absMiddle\"><IMG id=\"img_9\" src=\"images/foldclose.gif\" align=\"absMiddle\"><input id=\"checkbox_9\" type=\"checkbox\" onclick=\"CheckChildren('menu_9','checkbox_9','_9')\" value=\"7000\" name=\"qxCheckBox\">" + wareHouseStr + "</div>");
                        hstr.Append("<div id=\"menu_9\" style=\"MARGIN-TOP: -3px); DISPLAY: none\">");
                    }
                    hstr.Append("<IMG src=images/line1.gif align=absMiddle ><IMG src=images/line3.gif align=absMiddle ><input id=checkbox"); hstr.Append(dr.GetInt32(0).ToString()); hstr.Append(" type=checkbox onclick=\"checkpid('checkbox"); hstr.Append("_9"); hstr.Append("',this,'"); hstr.Append("_9"); hstr.Append("');\" value="); hstr.Append(dr.GetInt32(0).ToString()); hstr.Append(" name=qxCheckBox>"); hstr.Append(dr.GetString(1).ToString()); hstr.Append("<br>");
                    count++;
                }
            }
            else
            {
                while (dr.Read())
                {
                    if (count == 0)
                    {
                        hstr.Append("<div id=\"menu__9\"><IMG class=\"menutop\" id=\"plus_9\" onclick=\"menu('menu_9','img_9',this)\" src=\"images/plus3.gif\"");
                        hstr.Append("align=\"absMiddle\"><IMG id=\"img_9\" src=\"images/foldclose.gif\" align=\"absMiddle\"><input id=\"checkbox_9\" type=\"checkbox\" onclick=\"CheckChildren('menu_9','checkbox_9','_9')\" value=\"7000\" name=\"qxCheckBox\">" + wareHouseStr + "</div>");
                        hstr.Append("<div id=\"menu_9\" style=\"MARGIN-TOP: -3px); DISPLAY: none\">");
                    }
                    if (htb.Contains(dr.GetInt32(0)))
                    {
                        string asg = "<IMG src=images/line1.gif align=absMiddle ><IMG src=images/line3.gif align=absMiddle ><input id=checkbox";
                        asg += dr.GetInt32(0).ToString();
                        
                        hstr.Append("<IMG src=images/line1.gif align=absMiddle ><IMG src=images/line3.gif align=absMiddle ><input id=checkbox"); hstr.Append(dr.GetInt32(0).ToString());
                        hstr.Append(" type='checkbox' onclick=\"checkpid('checkbox"); hstr.Append("_9"); hstr.Append("',this,'"); hstr.Append("_9"); hstr.Append("');\" value="); hstr.Append(dr.GetInt32(0).ToString()); hstr.Append(" name=qxCheckBox>"); hstr.Append(dr.GetString(1).ToString()); hstr.Append("<br>");
                    }
                    count++;
                }

            }
            dr.Close();
            hstr.Append("</div>");
            return hstr.ToString();
        }


        #region 获取角色权限
        /// <summary>
        /// 获取角色权限
        /// </summary>
        /// <param name="deptRoleID">角色ID</param>
        /// <returns>角色权限</returns>
        public static System.Collections.Hashtable GetAllPermission(int deptRoleID)
        {
            System.Collections.Hashtable htb = new System.Collections.Hashtable();
            SqlParameter[] paras = new SqlParameter[1];
            paras[0] = new SqlParameter("@Roleid", SqlDbType.Int);
            paras[0].SqlValue = deptRoleID;
            DataTable dt = DBHelper.ExecuteDataTable("GetrolePermission", paras, CommandType.StoredProcedure);
            foreach (DataRow row in dt.Rows)
            {
                htb.Add(row[0], row[1]);
            }
            return htb;
        }
        #endregion

        #region 获取角色权限
        /// <summary>
        /// DS2012
        /// 获取角色权限
        /// </summary>
        /// <param name="managerNum">登录管理员编号</param>
        /// <returns></returns>
        public static System.Collections.Hashtable GetAllPermission(string managerNum)
        {
            System.Collections.Hashtable htb = new System.Collections.Hashtable();
            SqlParameter[] paras = new SqlParameter[2];
            paras[0] = new SqlParameter("@Roleid", SqlDbType.Int);
            paras[0].SqlValue = 0;
            paras[1] = new SqlParameter("@managerNum", SqlDbType.VarChar);
            paras[1].SqlValue = managerNum;
            SqlDataReader reader = DBHelper.ExecuteReader("GetrolePermission", paras, CommandType.StoredProcedure);
            while(reader.Read())
            {
                htb.Add(reader.GetInt32(0), reader.GetInt32(1));
            }
            reader.Close();
            return htb;
        }
        #endregion



        #region
        //ID
        //Name
        //DeptID
        //AddDate
        //PermissionManID
        //Allot
        #endregion

        #region 添加系统角色及权限
        /// <summary>
        /// ds2012
        /// 添加系统角色及权限
        /// </summary>
        /// <param name="ids">权限标识字符串 类似("1,2,3,4,5,")</param>
        /// <param name="deptRole"></param>
        public static void AddDeptRole(string ids, DeptRoleModel deptRole)
        {
            SqlParameter[] paras = new SqlParameter[] { 
                GetSqlParameter("@ids",SqlDbType.VarChar,ids),
                GetSqlParameter("@name",SqlDbType.VarChar,deptRole.Name),
                GetSqlParameter("@deptId",SqlDbType.Int,deptRole.DeptID),
                GetSqlParameter("@permissionManId",SqlDbType.Int,deptRole.PermissionManID),
                GetSqlParameter("@allot",SqlDbType.Int,deptRole.Allot),
                GetSqlParameter("@addDate",SqlDbType.DateTime,deptRole.Adddate),
                GetSqlParameter("@state",SqlDbType.Int,0),
                GetSqlParameter("@pRoleId",SqlDbType.Int,deptRole.ParentId)
            };
            DBHelper.ExecuteNonQuery("AddDeptRole", paras, CommandType.StoredProcedure);
        }
        #endregion

        #region 修改系统角色及权限
        /// <summary>
        /// ds2012
        /// 修改系统角色及权限
        /// </summary>
        /// <param name="ids">权限标识字符串 </param>
        /// <param name="deptRole">权限对象</param>
        public static void UptRoleDept(string ids, DeptRoleModel deptRole)
        {
            SqlParameter[] paras = new SqlParameter[] { 
                GetSqlParameter("@ids",SqlDbType.VarChar,ids),
                GetSqlParameter("@id",SqlDbType.Int,deptRole.Id),
                GetSqlParameter("@name",SqlDbType.VarChar,deptRole.Name),
                GetSqlParameter("@deptId",SqlDbType.Int,deptRole.DeptID),
                GetSqlParameter("@permissionManId",SqlDbType.Int,deptRole.PermissionManID),
                GetSqlParameter("@allot",SqlDbType.Int,deptRole.Allot)
            };
            //调用存储过程UtpDeptRole执行修改操作
            DBHelper.ExecuteNonQuery("UptDeptRole", paras, CommandType.StoredProcedure);
        }
        #endregion

        /// <summary>
        /// ds2012
        /// 根据角色编号，获取角色的相关信息
        /// </summary>
        /// <param name="roleID">角色编号</param>
        /// <returns></returns>
        public static DeptRoleModel GetRoleDeptByID(int roleID)
        {
            DeptRoleModel deptRoleModel = null;
            SqlDataReader reader = null;
            try
            {
                reader = DBHelper.ExecuteReader("GetDeptRoleByRoleId", new SqlParameter("@roleId", roleID), CommandType.StoredProcedure);
                if (reader.HasRows)
                {
                    reader.Read();
                    deptRoleModel = new DeptRoleModel();
                    deptRoleModel = GetDeptRoles(reader);
                }
            }
            catch (SqlException)
            {
                if(reader!=null)
                reader.Close();
                return null;
            }
            reader.Close();
            return deptRoleModel;
        }

        /// <summary>
        /// DS2012
        /// 根据角色编号获取该角色下的管理员人数
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static int GetCountByRoleId(int p)
        {
            int count = 0;
            SqlParameter para = GetSqlParameter("@roleid", SqlDbType.Int, p);
            count = int.Parse(DBHelper.ExecuteScalar("GetManageCountByRoleID", para, CommandType.StoredProcedure).ToString());
            return count;
        }

        /// <summary>
        /// ds2012
        /// 删除角色
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public static string DelDeptRole(int roleid)
        {
            //删除角色信息sql语句
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
               
                try
                {
                    string sql = "delete from DeptRole where id = @roleid";
                    SqlParameter[] para = {
                                      new SqlParameter("@roleid",SqlDbType.Int)
                                  };
                    para[0].Value = roleid;

                    int i = DBHelper.ExecuteNonQuery(tran, sql, para, CommandType.Text);

                    sql = "delete from ManagerPermission where ManagerId=@ManagerId";
                    SqlParameter[] para1 = {
                                          new SqlParameter("@ManagerId",SqlDbType.Int)
                                      };
                    para1[0].Value = roleid;
                    int j = DBHelper.ExecuteNonQuery(tran, sql, para1, CommandType.Text);

                    tran.Commit();
                    return "删除角色成功.";
                }
                catch (SqlException ex)
                {
                    string gasg = ex.Message;
                    tran.Rollback();
                    return "删除系统角色产生异常.";
                }
                finally
                {
                    conn.Close();
                }
            }

        }

        /// <summary>
        /// 根据角色名和编号获取角色信息
        /// </summary>
        /// <param name="deptRoleName">角色名</param>
        /// <param name="deptRoleID">角色编号</param>
        /// <returns></returns>
        public static DeptRoleModel GetDeptRole(string deptRoleName, int deptRoleID)
        {
            DeptRoleModel deptRole = null;
            if (deptRoleID == 0)
            {
                string sql = "select * from DeptRole where [name] = @name ";
                SqlParameter para = new SqlParameter("@name", deptRoleName);
                SqlDataReader reader = DBHelper.ExecuteReader(sql, para, CommandType.Text);
                if (reader.Read())
                {
                    deptRole = GetDeptRoles(reader);
                }
                reader.Close();
            }
            else
            {
                string sql = "select * from deptRole where [name]=@name and id <> @id";
                SqlParameter[] para = new SqlParameter[] { 
                    GetSqlParameter("@name", SqlDbType.VarChar, deptRoleName), 
                    GetSqlParameter("@id", SqlDbType.Int, deptRoleID) 
                };
                SqlDataReader reader = DBHelper.ExecuteReader(sql,para, CommandType.Text);
                if (reader.Read())
                {
                    deptRole = GetDeptRoles(reader);
                }
                reader.Close();
            }
            return deptRole;
        }
        /// <summary>
        /// 根据部门获取部门下的所有角色信息
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static IList<DeptRoleModel> GetRoleDeptByComDept(int p)
        {
            //创建角色泛型集合
            IList<DeptRoleModel> roles = null;
            SqlParameter para = GetSqlParameter("@comDeptId", SqlDbType.Int, p);
            SqlDataReader reader = DBHelper.ExecuteReader("GetDeptRolesByComDeptId", para, CommandType.StoredProcedure);
            if (reader.HasRows)
            {
                roles = new List<DeptRoleModel>();
                while (reader.Read())
                {
                    DeptRoleModel deptRole = new DeptRoleModel(reader.GetInt32(0));
                    deptRole.Name = reader.GetString(1);
                    roles.Add(deptRole);
                }
            }
            reader.Close();
            return roles;
        }

        /// <summary>
        /// 获取角色数据
        /// </summary>
        /// <param name="pageInfo">分页帮助类</param>
        /// <param name="tableName">表名</param>
        /// <param name="columns">列名</param>
        /// <param name="key">分页列名</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static DataTable GetDeptRolesDataTable(PaginationModel pageInfo, string tableName, string columns, string key, string condition)
        {
            return SqlDataReaderHelp.GetDataTable(pageInfo, tableName, key, columns, condition);
        }

        /// <summary>
        /// 根据角色获取角色可以编辑的所有角色列表
        /// </summary>
        /// <param name="number"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public static IList<DeptRoleModel> GetDeptRoles(string number, PaginationModel pageInfo)
        {
            return null;   
        }

        /// <summary>
        /// 获取某角色的所有可修改角色编号
        /// </summary>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        public static string GetDeptRoles(int RoleID)
        {
            return "";
        }
        /// <summary>
        /// DS2012 
        /// 获取当前登录管理员可以修改的角色编号
        /// </summary>
        /// <param name="number">当前管理员的编号</param>
        /// <returns></returns>
        public static string GetDeptRoleIds(string number)
        {
            string sp= "GetDeptRolesByRoleId";
            string ids = "";
            SqlParameter para = new SqlParameter("@number",number);
            SqlDataReader dr = DBHelper.ExecuteReader(sp, para, CommandType.StoredProcedure);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ids += dr.GetInt32(0) + ",";
                }
            }
            dr.Close();
            if (ids.Length == 0)
                return ids;
            else
                return ids.Substring(0, ids.Length-1);
        }

        public static int GetViewManage(string manageID)
        {
            string strSql = "select Count(id) from viewmanage where manageid=@manageID";
            SqlParameter[] para = {
                                      new SqlParameter("@manageID",SqlDbType.NVarChar,30)
                                  };
            para[0].Value = manageID;
            int count = (int)DBHelper.ExecuteScalar(strSql, para, CommandType.Text);

            return count;
        }

        /// <summary>
        /// ds2012
        /// 根据管理员编号判断当期用户是否分配权限给下级
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool GetAllot(string number)
        {
            string sql = "select 1 from manage m inner join deptrole d on m.roleid = d.id where m.number = @number and d.allot = 1";
            SqlParameter para = new SqlParameter("@number",number);
            SqlDataReader dr = DBHelper.ExecuteReader(sql,para,CommandType.Text);
            if (dr.HasRows)
            {
                dr.Close();
                return true;
            }
            dr.Close();
            return false;
        }

        /// <summary>
        /// 根据角色及当前登录管理可修改角色获取所有制定部门下的角色
        /// </summary>
        /// <param name="p">部门id</param>
        /// <param name="ids">当前登录管理可管理角色的集合</param>
        /// <returns></returns>
        public static IList<DeptRoleModel> GetRoleDeptByComDept(int p, string ids)
        {
            string sql = "select Id,Name from DeptRole where deptid =@comDeptId and id in ("+ids+")";
            //创建角色泛型集合
            IList<DeptRoleModel> roles = null;
            SqlParameter para = new SqlParameter("@comDeptId",p);
            SqlDataReader reader = DBHelper.ExecuteReader(sql, para, CommandType.Text);
            if (reader.HasRows)
            {
                roles = new List<DeptRoleModel>();
                while (reader.Read())
                {
                    DeptRoleModel deptRole = new DeptRoleModel(reader.GetInt32(0));
                    deptRole.Name = reader.GetString(1);
                    roles.Add(deptRole);
                }
            }
            reader.Close();
            return roles;
        }
        /// <summary>
        /// DS2012
        /// 根据角色编号加载权限树
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public static DataTable GetPermission(int roleid)
        {
            string field = System.Web.HttpContext.Current.Session["LanguageCode"].ToString();
            SqlParameter[] para = { new SqlParameter("@LanguageCode", field), new SqlParameter("@id", roleid) };
            return DBHelper.ExecuteDataTable("[GetPermission]", para, CommandType.StoredProcedure);
        }
    }
}
