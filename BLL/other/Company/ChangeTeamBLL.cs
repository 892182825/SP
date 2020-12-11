using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BLL.CommonClass;
using Model.Other;
using System.Web;
using System.Data.SqlClient;
using System.Data;


namespace BLL.other.Company
{
    public class ChangeTeamBLL
    {
        /// <summary>
        /// 调整网络方法
        /// </summary>
        /// <param name="number">会员编号</param>
        /// <param name="placement">新安置</param>
        /// <param name="direct">新推荐</param>
        /// <param name="oldplacement">原安置</param>
        /// <param name="olddirect">原推荐</param>
        /// <param name="newqushu"></param>
        /// <param name="flag"></param>
        /// <param name="isPass"></param>
        /// <returns></returns>
        public static string ChenageNet(string number, string placement, string direct, string oldplacement, string olddirect, int newqushu, int flag, out bool isPass)
        {

            string topMemberID = BLL.CommonClass.CommonDataBLL.GetTopManageID(3);

            int xs = 0;
            //if (placement != topMemberID)
            //{
            //    xs = 3;
            //}
            //@bianhao--调网编号
            //@srcAnZhi--原安置  
            //@dirAnZhi--新安置	
            //@srcTuiJian--原推荐  
            //@dirTuiJian--新推荐  
            //@azXianShu--0:表示无限制，大于0则表示最大的线数 
            //@info output --失败时返回错误信息，反之返回'OK'	
            string info = "";
            /*
            @bianhao 	nvarchar(20), 	--调网编号
            @srcAnZhi	nvarchar(20),	--原安置
            @dirAnZhi	nvarchar(20),	--新安置
            @srcTuiJian	nvarchar(20),	--原推荐
            @dirTuiJian	nvarchar(20),   --新推荐
            @azXianShu	int,		--0:表示无限制，大于0则表示最大的线数
            @info		nvarchar(200) output --失败时返回错误信息，反之返回'OK'	*/
            //info = TempHistoryDAL.ChangeCheck(number, placement, direct, oldplacement, olddirect, newqushu, xs, info);
            //if (info != "OK")
            //{
            //    isPass = false;
            //    return info;
            //}


            int maxExpectNum = CommonDataBLL.GetMaxqishu();
            //调层位序号
            //@bianhao varchar(20), --编号  
            //@old varchar(20),--原位置
            //@new varchar(20),--新位置
            //@IsAz bit, --0：推荐；1：安置
            //@qishu int	--调网期
            //TempHistoryDAL.ExecuteUpdateNew(number, placement, direct, tran, maxExpectNum, flag);

            DateTime nowTime = DateTime.UtcNow;

            int j = 0;
            //调安置
            //if (oldplacement.Trim() != placement.Trim())
            //{
            //    j = TempHistoryDAL.ExecuteUpdateNet(number, oldplacement, placement, 1, maxExpectNum, newqushu, CommonDataBLL.OperateBh, nowTime);
            //}
            //调推荐
            if (olddirect.Trim() != direct.Trim())
            {
                j = TempHistoryDAL.ExecuteUpdateNet(number, olddirect, direct, 0, maxExpectNum, newqushu, CommonDataBLL.OperateBh, nowTime);
            }

            if (j >= 0)
            {
                isPass = true;
                return new BLL.TranslationBase().GetTran("007134", "调网成功") + "!";
            }
            else
            {
                isPass = false;
                return new BLL.TranslationBase().GetTran("007135", "调网失败") + "!";
            }
        }

        /// <summary>
        /// 调整网络方法
        /// </summary>
        /// <param name="number">会员编号</param>
        /// <param name="placement">新安置</param>
        /// <param name="direct">新推荐</param>
        /// <param name="oldplacement">原安置</param>
        /// <param name="olddirect">原推荐</param>
        /// <returns></returns>
        public static string ChenageNet(string number, string placement, string direct, string oldplacement, string olddirect, int flag, out bool isPass)
        {

            string topMemberID = BLL.CommonClass.CommonDataBLL.GetTopManageID(3);

            int newqushu = 1;
            //调安置
            if (oldplacement.Trim() != placement.Trim())
            {
                newqushu = AddOrderDataDAL.GetDistrict(placement, 1);
            }

            int xs = 0;
            if (placement != topMemberID)
            {
                xs = 3;
            }
            //@bianhao--调网编号
            //@srcAnZhi--原安置  
            //@dirAnZhi--新安置	
            //@srcTuiJian--原推荐  
            //@dirTuiJian--新推荐  
            //@azXianShu--0:表示无限制，大于0则表示最大的线数 
            //@info output --失败时返回错误信息，反之返回'OK'	
            string info = "";
            /*
            @bianhao 	nvarchar(20), 	--调网编号
            @srcAnZhi	nvarchar(20),	--原安置
            @dirAnZhi	nvarchar(20),	--新安置
            @srcTuiJian	nvarchar(20),	--原推荐
            @dirTuiJian	nvarchar(20),   --新推荐
            @azXianShu	int,		--0:表示无限制，大于0则表示最大的线数
            @info		nvarchar(200) output --失败时返回错误信息，反之返回'OK'	*/
            info = TempHistoryDAL.ChangeCheck(number, placement, direct, oldplacement, olddirect, newqushu, xs, info);
            if (info != "OK")
            {
                isPass = false;
                return info;
            }


            int maxExpectNum = CommonDataBLL.GetMaxqishu();
            //调层位序号
            //@bianhao varchar(20), --编号  
            //@old varchar(20),--原位置
            //@new varchar(20),--新位置
            //@IsAz bit, --0：推荐；1：安置
            //@qishu int	--调网期
            //TempHistoryDAL.ExecuteUpdateNew(number, placement, direct, tran, maxExpectNum, flag);

            DateTime nowTime = DateTime.UtcNow;

            int j = 0;
            //调安置
            if (oldplacement.Trim() != placement.Trim())
            {
                j = TempHistoryDAL.ExecuteUpdateNet(number, oldplacement, placement, 1, maxExpectNum, newqushu, CommonDataBLL.OperateBh, nowTime);
            }
            //调推荐
            if (olddirect.Trim() != direct.Trim())
            {
                j = TempHistoryDAL.ExecuteUpdateNet(number, olddirect, direct, 0, maxExpectNum, newqushu, CommonDataBLL.OperateBh, nowTime);
            }

            if (j >= 0)
            {
                isPass = true;
                return new BLL.TranslationBase().GetTran("007134", "调网成功") + "!";
            }
            else
            {
                isPass = false;
                return new BLL.TranslationBase().GetTran("007135", "调网失败") + "!";
            }
        }

        /// <summary>
        /// 修改推荐或者安置编号
        /// </summary>
        /// <param name="number">会员编号</param>
        /// <param name="placement">新安置</param>
        /// <param name="direct">新推荐</param>
        /// <param name="oldplacement">原安置</param>
        /// <param name="olddirect">原推荐</param>
        /// <returns></returns>
        public static string UpdateNet(string number, string placement, string direct, string oldplacement, string olddirect, int qushu, int flag, out bool isPass)
        {
            BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("MemberInfo", "Number");//申明日志对象
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    int xs = 0;

                    string info = "";

                    try
                    {
                        /*
                           @bianhao 	nvarchar(20), 	--调网编号
                           @srcAnZhi	nvarchar(20),	--原安置
                           @dirAnZhi	nvarchar(20),	--新安置
                           @srcTuiJian	nvarchar(20),	--原推荐
                           @dirTuiJian	nvarchar(20),   --新推荐
                           @azXianShu	int,		--0:表示无限制，大于0则表示最大的线数
                           @info		nvarchar(200) output --失败时返回错误信息，反之返回'OK'	
                        */
                        info = TempHistoryDAL.ChangeCheck(number, placement, direct, oldplacement, olddirect, qushu, tran, xs, info);
                        if (info != "OK")
                        {
                            isPass = false;
                            return info;
                        }

                        int maxExpectNum = CommonDataBLL.GetNumberRegExpect(number);
                        //调层位序号
                        //@bianhao varchar(20), --编号  
                        //@old varchar(20),--原位置
                        //@new varchar(20),--新位置
                        //@IsAz bit, --0：推荐；1：安置
                        //@qishu int	--调网期
                        TempHistoryDAL.UpdateNet(number, placement, direct, tran, maxExpectNum, flag);
                        int count = TempHistoryDAL.UpdateMemberinfo(number, placement, direct, tran);

                        if (count == 0)
                        {
                            isPass = false;
                            tran.Rollback();
                            return "修改失败！";
                        }

                        count = TempHistoryDAL.UpdateConfig(maxExpectNum, tran);

                        if (count == 0)
                        {
                            isPass = false;
                            tran.Rollback();
                            return "修改失败！";
                        }

                        cl_h_info.AddRecordtran(tran, number);
                        if (System.Web.HttpContext.Current.Session["Company"] != null)
                        {
                            cl_h_info.ModifiedIntoLogstran(tran, CommonClass.ChangeCategory.Order, number, BLL.CommonClass.ENUM_USERTYPE.objecttype5);
                        }
                        else if (System.Web.HttpContext.Current.Session["Store"] != null)
                        {
                            cl_h_info.ModifiedIntoLogstran(tran, CommonClass.ChangeCategory.Order, number, BLL.CommonClass.ENUM_USERTYPE.objecttype5);
                        }
                    }
                    catch (Exception ex)
                    {
                        isPass = false;
                        string assdgs = ex.Message;
                        tran.Rollback();
                        return assdgs;
                    }

                    tran.Commit();
                    isPass = true;
                    return "修改成功!";
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }


        /// <summary>
        /// 获取指定编号安置人数
        /// </summary>
        /// <param name="placement"></param>
        /// <returns></returns>
        public static int GetPlacementCount(string placement, string number)
        {
            return MemberInfoDAL.GetPlaceCount(placement, number);
        }

        /// <summary>
        /// 获取指定编号推荐人数
        /// </summary>
        /// <param name="direct"></param>
        /// <returns></returns>
        public static int GetDirectCount(string direct)
        {
            return MemberInfoDAL.GetDirectCount(direct);
        }

        /// <summary>
        /// 判断指定编号是否存在
        /// </summary>
        /// <returns>true 不存在 false 存在</returns>
        public static bool CheckNum(string number)
        {
            return MemberInfoDAL.GetMemberInfoCount(number) == 0;
        }

        /// <summary>
        /// 判断指定编号是否存在
        /// </summary>
        /// <returns>true 不存在 false 存在</returns>
        public static int GetQishu(string number)
        {
            return MemberInfoDAL.GetMemberQishu(number);
        }

        /// <summary>
        /// 获取指定编号信息
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static System.Data.DataTable GetMemberInfoDataTable(string number)
        {
            return MemberInfoDAL.GetMemberInfoDataTable(number);
        }

        public static int GetFlag(string number)
        {
            return TempHistoryDAL.GetFlag(number);
        }

        /// <summary>
        /// 检测指定会员在指定期数之前是否进入系统
        /// </summary>
        /// <param name="number">编号</param>
        /// <param name="expectNum">期数</param>
        /// <returns></returns>
        public static bool CheckNum(string number, int expectNum)
        {
            return MemberInfoDAL.GetMemberInfoCount(number, expectNum) == 0;
        }

        /// <summary>
        /// 获取网络信息
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static DataTable GetNetMessage(string number)
        {
            return TempHistoryDAL.GetNetMessage(number);
        }
        /// <summary>
        /// 指定位置是否有人安置
        /// </summary>
        /// <param name="placement"></param>
        /// <param name="qushu"></param>
        /// <returns></returns>
        public static int CheckMemAtQushu(string placement, string qushu)
        {
            return MemberInfoDAL.CheckMemAtQushu(placement, qushu);
        }
    }
}