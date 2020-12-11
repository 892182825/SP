using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DAL;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/*
 * 工资发放
 * 
 * **/

//张振
namespace BLL.MoneyFlows
{

    /// <summary>
    /// 工资发放
    /// </summary>
    public class ReleaseBLL
    {
        #region 工资发放
        /// <summary>
        /// 工资是否已经发放 --ds2012--www-b874dce8700——tianfeng
        /// </summary>
        /// <returns></returns>
        public static bool IsProvide(int ExpectNum)
        {
            return ReleaseDAL.IsProvide(ExpectNum);
        }
        /// <summary>
        /// 奖金发放——ds2012——tianfeng
        /// </summary>
        /// <param name="ExpectNum">期数</param>
        public static bool Provide(int ExpectNum)
        {
            return ReleaseDAL.Provide(ExpectNum);
        }
        /// <summary>
        /// 根据期数查询是否已发布——ds2012——tianfeng
        /// </summary>
        /// <param name="ExpectNum">期数</param>
        /// <returns></returns>
        public static int GetOutBonus(int ExpectNum)
        {
            return ReleaseDAL.GetOutBonus(ExpectNum);
        }
        /// <summary>
        /// 奖金是否发布——ds2012——tianfeng
        /// </summary>
        /// <param name="ExpectNum">期数</param>
        /// <param name="num">1.发布0.不发布</param>
        /// <returns></returns>
        public static int Release(int ExpectNum, int num)
        {
            return ReleaseDAL.Release(ExpectNum, num);
        }
        /// <summary>
        /// 撤销发放奖金 --ds2012--www-b874dce8700——tianfeng
        /// </summary>
        /// <param name="ExpectNum">期数</param>
        public static int Revert(int ExpectNum)
        {
            return ReleaseDAL.Revert(ExpectNum);
        }
        /// <summary>
        /// 判断是否是当前最大期 --ds2012--www-b874dce8700——tianfeng
        /// </summary>
        /// <param name="ExpectNum">期数</param>
        /// <returns></returns>
        public static int IsCurrently(int ExpectNum)
        {
            return ReleaseDAL.IsCurrently(ExpectNum);
        }
        /// <summary>
        /// 奖金撤销，不是当期最大期 --ds2012--www-b874dce8700——tianfeng
        /// </summary>
        /// <returns></returns>
        public static int Cancel(int ExpectNum)
        {
            return ReleaseDAL.Cancel(ExpectNum);
        }
        #endregion
        #region 工资退回
        /// <summary>
        /// 删除工资退回——ds2012——tianfeng
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="money">退回金额</param>
        /// <param name="MemeberNum">会员编号</param>
        /// <returns></returns>
        public static bool DelChongHong(int ID, double money, string MemeberNum)
        {
            return ReleaseDAL.DelChongHong(ID, money, MemeberNum);
        }

        /// <summary>
        /// 获取会员的某期的实发——ds2012——tianfeng
        /// </summary>
        /// <param name="number"></param>
        /// <param name="qs"></param>
        /// <returns></returns>
        public static double getChongHong(string number, int qs)
        {
            return ReleaseDAL.getChongHong(number, qs);
        }
        /// <summary>
        /// 添加“工资退回”——ds2012——tianfeng
        /// </summary>
        /// <param name="chonghong">工资退回对象</param>
        /// <returns></returns>
        public static bool AddChongHong(ChongHongModel chonghong)
        {
            return ReleaseDAL.AddChongHong(chonghong);
        }
        /// <summary>
        /// 根据会员编号查询会员信息——ds2012——tianfeng
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        public static MemberInfoModel GetMemberInfo(string Number)
        {
            return ReleaseDAL.GetMemberInfo(Number);
        }
        /// <summary>
        /// 获得最大期数 ds2012--www-b874dce8700
        /// </summary>
        /// <returns></returns>
        public static int GetMaxExpectNum()
        {
            return ReleaseDAL.GetMaxExpectNum();
        }
        /// <summary>
        /// 更新结算状态
        /// </summary>
        /// <returns></returns>
        public static void UPConfigflag(int ExpectNum)
        {
            ReleaseDAL.UPConfigflag(ExpectNum);
        }
        /// <summary>
        /// 更新结算次数
        /// </summary>
        /// <returns></returns>
        public static void UPConfigNum(int ExpectNum)
        {
            ReleaseDAL.UPConfigNum(ExpectNum);
        }
        public static string ChongHongBeizhu(int ID)
        {
            return ReleaseDAL.ChongHongBeizhu(ID);
        }

        public static string DeductReason(int ID)
        {
            return ReleaseDAL.DeductReason(ID);
        }

        #endregion
        #region 拨出率显示
        /// <summary>
        /// 根据期数读取总金额、总奖金
        /// </summary>
        /// <param name="rate">汇率</param>
        /// <param name="ExpectNum">期数</param>
        /// <returns></returns>
        public static IList<BochulvModel> GetTotalBonus(double rate, int ExpectNum)
        {
            return ReleaseDAL.GetTotalBonus(rate, ExpectNum);
        }

        /// <summary>
        /// --ds2012--www-b874dce8700
        /// </summary>
        /// <param name="rate"></param>
        /// <param name="StartExpect"></param>
        /// <param name="EndExpect"></param>
        /// <returns></returns>
        public static IList<BochulvModel> GetTotalBonus(double rate, string StartExpect, string EndExpect)
        {
            return ReleaseDAL.GetTotalBonus(rate, StartExpect, EndExpect);
        }
        /// <summary>
        /// 根据期数读取总PV、总奖金
        /// </summary>
        /// <param name="rate">汇率</param>
        /// <param name="ExpectNum">期数</param>
        /// <returns></returns>
        public static IList<BochulvModel> GetTotalPV(int ExpectNum)
        {
            return ReleaseDAL.GetTotalPV(ExpectNum);
        }

        /// <summary>
        /// --ds2012--www-b874dce8700
        /// </summary>
        /// <param name="StartExpectNum"></param>
        /// <param name="EndExpectNum"></param>
        /// <returns></returns>
        public static IList<BochulvModel> GetTotalPV(string StartExpectNum, string EndExpectNum)
        {
            return ReleaseDAL.GetTotalPV(StartExpectNum, EndExpectNum);
        }
        /// <summary>
        /// 读取总金额、总奖金
        /// </summary>
        /// <param name="rate">汇率</param>
        /// <returns></returns>
        public static void GetBonus(int MaxExpectNum, double rate, out double CurrentSolidSend, out double CurrentOneMoney)
        {
            ReleaseDAL.GetBonus(MaxExpectNum, rate, out CurrentSolidSend, out CurrentOneMoney);
        }

        /// <summary>
        /// --ds2012--www-b874dce8700
        /// </summary>
        /// <param name="StartExpect"></param>
        /// <param name="EndExpect"></param>
        /// <param name="rate"></param>
        /// <param name="CurrentSolidSend"></param>
        /// <param name="CurrentOneMoney"></param>
        public static void GetBonus(int StartExpect, int EndExpect, double rate, out double CurrentSolidSend, out double CurrentOneMoney)
        {
            ReleaseDAL.GetBonus(StartExpect, EndExpect, rate, out CurrentSolidSend, out CurrentOneMoney);
        }
        /// <summary>
        /// 读取总金额、总PV
        /// </summary>
        /// <param name="rate">汇率</param>
        /// <returns></returns>
        public static void GetPV(int MaxExpectNum, out double CurrentSolidSend, out double CurrentOneMoney)
        {
            ReleaseDAL.GetPV(MaxExpectNum, out CurrentSolidSend, out CurrentOneMoney);
        }

        /// <summary>
        /// --ds2012--www-b874dce8700
        /// </summary>
        /// <param name="StartExpect"></param>
        /// <param name="EndExpect"></param>
        /// <param name="CurrentSolidSend"></param>
        /// <param name="CurrentOneMoney"></param>
        public static void GetPV(int StartExpect, int EndExpect, out double CurrentSolidSend, out double CurrentOneMoney)
        {
            ReleaseDAL.GetPV(StartExpect, EndExpect, out CurrentSolidSend, out CurrentOneMoney);
        }
        /// <summary>
        /// 读取本期实发奖金和本个人消费金额
        /// </summary>
        /// <param name="rate">汇率</param>
        /// <param name="ExpectNum">期数</param>
        /// <param name="CurrentSolidSend">本期实发奖金</param>
        /// <param name="CurrentOneMoney">本个人消费金额</param>
        public static void GetTotalMoney(double rate, int ExpectNum, out double CurrentSolidSend, out double CurrentOneMoney)
        {
            ReleaseDAL.GetTotalMoney(rate, ExpectNum, out CurrentSolidSend, out CurrentOneMoney);
        }
        /// <summary>
        /// 读取本期实发奖金和本个人消费PV
        /// </summary>
        /// <param name="rate">汇率</param>
        /// <param name="ExpectNum">期数</param>
        /// <param name="CurrentSolidSend">本期实发奖金</param>
        /// <param name="CurrentOneMoney">本个人消费PV</param>
        public static void GetCurrentPV(int ExpectNum, out double CurrentSolidSend, out double CurrentOneMoney)
        {
            ReleaseDAL.GetCurrentPV(ExpectNum, out CurrentSolidSend, out CurrentOneMoney);
        }
        /// <summary>
        /// 获得当前单个奖金滴总额 --ds2012--www-b874dce8700
        /// </summary>
        /// <param name="Rate">汇率</param>
        /// <param name="ExpectNum">期数</param>
        /// <param name="hang"></param>
        /// <param name="zoushi"></param>
        /// <param name="zoushi"></param>
        /// <param name="CurrentOneMark"></param>
        public static void GetOneBonus(double Rate, int ExpectNum, int hang, string[,] zoushi, out string[,] zoushi1, out double CurrentOneMark)
        {
            ReleaseDAL.GetOneBonus(Rate, ExpectNum, hang, zoushi, out zoushi1, out CurrentOneMark);
        }
        #endregion
        #region 工资结算
        /// <summary>
        /// 获得要结算的最后10期的信息
        /// </summary>
        /// <returns></returns>
        public static IList<ConfigModel> GetConfigInfo()
        {
            return ReleaseDAL.GetConfigInfo();
        }
        /// <summary>
        /// 插入配置表
        /// </summary>
        /// <param name="ExpectNum">期数</param>
        /// <returns></returns>
        public static bool insertConfig(int ExpectNum)
        {
            return ReleaseDAL.insertConfig(ExpectNum);
        }
        public static IList<ConfigModel> GetConfigInfo1(int MaxQs)
        {
            return ReleaseDAL.GetConfigInfo1(MaxQs);
        }
        /// <summary>
        /// 创建新一期 --ds2012--www-b874dce8700
        /// </summary>
        /// <param name="ExpectNum">要创建的期数</param>
        /// <returns></returns>
        public static bool addNewQishu(int ExpectNum)
        {
            return ReleaseDAL.addNewQishu(ExpectNum);
        }
        /// <summary>
        /// 显示所有期数的结算链接。 --ds2012--www-b874dce8700
        /// </summary>
        /// <param name="startqishu">开始期数</param>
        /// <param name="maxqishu">最大期数</param>
        /// <param name="maxqishu">要显示的期数范围，从1到当前值。</param>
        public static IList<ConfigModel> showTotalQishuLink(int startqishu, int maxqishu)
        {
            return ReleaseDAL.showTotalQishuLink(startqishu, maxqishu);
        }
        /// <summary>
        /// 当期是否有未发放的奖金
        /// </summary>
        /// <param name="ExpectNum">当期</param>
        /// <returns></returns>
        public static bool IsNotProvideBonus(int ExpectNum)
        {
            return ReleaseDAL.IsNotProvideBonus(ExpectNum);
        }
        /// <summary>
        /// 更新发放状态
        /// </summary>
        /// <param name="ExpectNum">当期</param>
        /// <returns></returns>
        public static bool upProvideState(int ExpectNum)
        {
            return ReleaseDAL.upProvideState(ExpectNum);
        }
        /// <summary>
        /// 判断发放期数中是否存在所选期 --ds2012--www-b874dce8700
        /// </summary>
        /// <param name="ExpectNum">期数</param>
        /// <returns></returns>
        public static int IsSuperintendent(int ExpectNum)
        {
            return ReleaseDAL.IsSuperintendent(ExpectNum);
        }
        /// <summary>
        /// 检测是否存在没有会员编号的店铺 ds2012--www-b874dce8700
        /// </summary>
        public static int IsNumberExists()
        {
            return ReleaseDAL.IsNumberExists();
        }
        /// <summary>
        /// 撤销电子账户  --ds2012--www-b874dce8700
        /// </summary>
        /// <param name="p"></param>
        public static void Backout(int ExpectNum)
        {
            ReleaseDAL.Backout(ExpectNum);
        }
        public static int CancleBonus(int ExpectNum)
        {
            return ReleaseDAL.CancleBonus(ExpectNum);
        }
        /// <summary>
        /// 是否有错误的单子
        /// </summary>
        /// <param name="ExpectNum">期数</param>
        /// <returns></returns>
        public static int IsErrorOrder(int ExpectNum)
        {
            return ReleaseDAL.ISErrorOrdre(ExpectNum);
        }
        #endregion



        /// <summary>
        /// 绑定的汇率查询 --ds2012--www-b874dce8700
        /// </summary>
        /// <param name="rate">汇率</param>
        /// <returns></returns>
        public static double GetRate(int id)
        {
            return ReleaseDAL.GetRate(id);
        }







        /// <summary>
        /// 扣款原因
        /// </summary>
        /// <param name="Number">编号</param>
        /// <returns></returns>
        public static DataTable Reason(string Number)
        {
            return ReleaseDAL.Reason(Number);
        }

        public static string GetRemark(int id)
        {
            return ReleaseDAL.GetRemark(id);
        }
        /// <summary>
        /// 获得该会员的奖金,提现账户——ds2012——tianfeng
        /// </summary>
        /// <param name="number">会员编号</param>
        /// <returns></returns>
        public static double GetMemberBonus(string number)
        {
            return ReleaseDAL.GetMemberBonus(number);
        }
        /// <summary>
        /// 获得该会员的奖金,提现账户（带事务）——ds2012——tianfeng
        /// </summary>
        /// <param name="number">会员编号</param>
        /// <returns></returns>
        public static double GetMemberBonus(string number, SqlTransaction tran)
        {
            return ReleaseDAL.GetMemberBonus(number, tran);
        }
        /// <summary>
        /// 获得该会员的奖金,报单账户
        /// </summary>
        /// <param name="number">会员编号</param>
        /// <returns></returns>
        public static double GetMemberDeclarations(string number)
        {
            return ReleaseDAL.GetMemberDeclarations(number);
        }
        public static double GetMemberDeclarations(string number, SqlTransaction tran)
        {
            return ReleaseDAL.GetMemberDeclarations(number, tran);
        }
        /// <summary>
        /// 添加转账记录
        /// </summary>
        /// <param name="info"></param>
        public static void AddTransfer(MoneyTransferModel info, out int outid)
        {
            ReleaseDAL.AddTransfer(info, out outid);
        }

        /// <summary>
        /// 添加转账记录
        /// </summary>
        /// <param name="info"></param>
        public static bool AddTransfer(SqlTransaction tran, MoneyTransferModel info, out int outid)
        {
            return ReleaseDAL.AddTransfer(tran, info, out outid);
        }

        /// <summary>
        /// 判断是否有未结算的
        /// </summary>
        /// <param name="ExpectNum"></param>
        /// <returns></returns>
        public static bool GetIsExistsConfig(int ExpectNum)
        {
            return ReleaseDAL.GetIsExistsConfig(ExpectNum);
        }

        /// <summary>
        /// 获取系统开关的对应列表到临时表##setsys
        /// </summary>
        /// <returns></returns>
        public static bool GetSystemList() {
            return ReleaseDAL.GetSystemList();
        }

         /// <summary>
        /// 更新系统开关全部为空--CK
        /// </summary>
        /// <returns></returns>
        public static bool UpdateSystem() {
            return ReleaseDAL.UpdateSystem();
        }
                /// <summary>
        /// 还原原有系统开关--CK
        /// </summary>
        /// <param name="list">原有系统列表</param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static bool UpdateSystemID()
        {
            return ReleaseDAL.UpdateSystemID();
        }
        
         /// <summary>
        /// 验证是否删除系统开关临时表##setsys
        /// </summary>
        /// <returns></returns>
        public static bool CheckSetsys() {
            return ReleaseDAL.CheckSetsys();
        }
         /// <summary>
        /// 删除系统开关临时表##setsys--CK
        /// </summary>
        /// <returns></returns>
        public static bool DelSetsys() {
            return ReleaseDAL.DelSetsys();
        }
    }
}
