using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Web;


//Add Namespace
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using Model;

using System.Configuration;
/*
 * 修改人：     汪  华
 * 修改时间：   2009-09-04
 */


namespace BLL.CommonClass
{
    /// <summary>
    /// 类名：CommonDataBLL
    /// 用途：公共类
    /// 创建者：
    /// </summary>
    public class CommonDataBLL
    {
        public static string SelfPathH = ConfigurationSettings.AppSettings["SelfPathH"];
        public static string SelfPathD = ConfigurationSettings.AppSettings["SelfPathD"];
        public static string SelfPathG = ConfigurationSettings.AppSettings["SelfPathG"];
        public static string OtherPath = ConfigurationSettings.AppSettings["OtherPath"];

        //获取当前访问程序的根路径
        public static string baseUrl = "";
        static CommonDataBLL()
        {
            try
            {
                baseUrl = HttpContext.Current.Request.Url.ToString().Replace(HttpContext.Current.Request.RawUrl, "") + HttpContext.Current.Request.ApplicationPath;
            }
            catch (Exception eps)
            {

            }
            finally
            {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        //获取所有级别
        public static DataTable GetAllLevel()
        {
            return DBHelper.ExecuteDataTable("select * from BSCO_level");
        }

        public static string GetComNumber()
        {
            string manageID = HttpContext.Current.Session["Company"].ToString();
            int count = 0;
            string number = GetWLNumber(manageID, out count);
            if (count == 0)
            {
                return "('')";
            }

            return number;
        }

        public static string GetComNumber(string tbName)
        {
            string manageID = HttpContext.Current.Session["Company"].ToString();
            int count = 0;
            string number = GetWLNumber(manageID, out count, tbName);
            if (count == 0)
            {
                return tbName + ".number in ('')";
            }

            return number;
        }

        public static string GetBiZhong(int id)
        {
            return CommonDataDAL.GetBiZhong(id);
        }

        public static string Getjsremark(string id)
        {
            try
            {
                string sql = "select [remark] from jiesuaninfo where [ID]=@id";
                SqlParameter[] para =
                {
                    new SqlParameter("@id",SqlDbType.Int)
                };
                para[0].Value = Convert.ToInt32(id);

                return DBHelper.ExecuteDataTable(sql, para, CommandType.Text).Rows[0][0].ToString();
            }
            catch
            {
                return GetTran("001584", "不存在");

            }

        }
        /// <summary>
        /// 最后一次结算状态,最后一次结算记录的ID,用于调用停止结算 --ds2012--www-b874dce8700
        /// </summary>
        /// <returns></returns>
        public static DataTable GetJstype()
        {
            return DBHelper.ExecuteDataTable("select top 1 jstype,id from jiesuaninfo order by id desc");
        }

        /// <summary>
        /// 最后一次结算状态,最后一次结算记录的ID,用于调用停止结算
        /// </summary>
        /// <returns></returns>
        public static string GetJstypeID(string id)
        {
            string sql = "select top 1 jstype from jiesuaninfo where id=@id";
            SqlParameter[] para = {
                                    new SqlParameter("@id",SqlDbType.Int)
                                   };
            para[0].Value = id;
            return DBHelper.ExecuteScalar(sql, para, CommandType.Text).ToString();
        }

        /// <summary>
        /// 插入新的结算记录，获取这条记录的id --ds2012--www-b874dce8700
        /// </summary>
        /// <param name="qishu"></param>
        /// <param name="operateIP"></param>
        /// <param name="operateBh"></param>
        /// <returns></returns>
        public static string InsJsInfo(int qishu, string operateIP, string operateBh)
        {
            string sql = "insert into jiesuaninfo(qishu,jstype,OperateIP,OperateBh) values(@qishu,@jstype,@OperateIP,@OperateBh)";
            SqlParameter[] para = {
                                    new SqlParameter("@qishu",SqlDbType.Int),
                                    new SqlParameter("@jstype",SqlDbType.Int),
                                    new SqlParameter("@OperateIP",SqlDbType.VarChar,30),
                                    new SqlParameter("@OperateBh",SqlDbType.VarChar,30)
                                   };
            para[0].Value = qishu;
            para[1].Value = 0;
            para[2].Value = operateIP;
            para[3].Value = operateBh;

            DBHelper.ExecuteNonQuery(sql, para, CommandType.Text);

            string id = DBHelper.ExecuteScalar("select top 1 id from jiesuaninfo order by id desc").ToString();

            return id;
        }


        public static int UpJstype(string id)
        {
            string sql = "update jiesuaninfo set jstype=4 where id=@id";

            SqlParameter[] para = {
                                    new SqlParameter("@id",SqlDbType.Int)
                                   };
            para[0].Value = id;
            return DBHelper.ExecuteNonQuery(sql, para, CommandType.Text);
        }

        /// <summary>
        /// 删除未审核的报单
        /// </summary>
        public static void DelOrderByState()
        {
            string date = DateTime.Now.AddDays(-2).Year.ToString() + "-" + DateTime.Now.AddDays(-2).Month + "-" + DateTime.Now.AddDays(-2).Day + " " + DateTime.Now.AddDays(-2).Hour + ":" + DateTime.Now.AddDays(-2).Minute + ":" + DateTime.Now.AddDays(-2).Second;


            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    DataTable dt = DBHelper.ExecuteDataTable(tran, "select orderid,isagain,number from memberorder where orderdate<='" + date + "' and defraystate=0");
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["isfuxiao"].ToString() == "0")
                        {
                            //修改上级
                            DataTable dth = DBHelper.ExecuteDataTable(tran, "select number,placement,direct from memberinfo where placement='" + dr["number"].ToString() + "' or direct='" + dr["number"].ToString() + "'");
                            string shangji = DBHelper.ExecuteScalar(tran, "select direct from memberinfo where bianhao='" + dr["number"].ToString() + "'", new SqlParameter[0], CommandType.Text).ToString();
                            foreach (DataRow drh in dth.Rows)
                            {
                                string tuijian = drh["placement"].ToString();
                                string anzhi = drh["direct"].ToString();

                                if (tuijian == dr["number"].ToString())
                                {
                                    SqlParameter[] parm ={new SqlParameter("@bianhao",SqlDbType.VarChar,30),
                                                            new SqlParameter("@old",SqlDbType.VarChar,30),
                                                            new SqlParameter("@new",SqlDbType.VarChar,30),
                                                            new SqlParameter("@IsAz",SqlDbType.Bit,2),
                                                            new SqlParameter("@qishu",SqlDbType.Int)
                                                        };

                                    parm[0].Value = drh["number"].ToString();
                                    parm[1].Value = drh["placement"].ToString();
                                    parm[2].Value = shangji;
                                    parm[3].Value = 0;
                                    parm[4].Value = CommonDataBLL.getMaxqishu();


                                    DBHelper.ExecuteNonQuery(tran, "js_UpdateNet_w", parm, CommandType.StoredProcedure);


                                    tuijian = shangji;
                                }
                                else if (anzhi == dr["number"].ToString())
                                {
                                    SqlParameter[] parm ={new SqlParameter("@bianhao",SqlDbType.VarChar,30),
                                                            new SqlParameter("@old",SqlDbType.VarChar,30),
                                                            new SqlParameter("@new",SqlDbType.VarChar,30),
                                                            new SqlParameter("@IsAz",SqlDbType.Bit,2),
                                                            new SqlParameter("@qishu",SqlDbType.Int)
                                                        };

                                    parm[0].Value = drh["number"].ToString();
                                    parm[1].Value = drh["direct"].ToString();
                                    parm[2].Value = anzhi;
                                    parm[3].Value = 1;
                                    parm[4].Value = CommonDataBLL.getMaxqishu();


                                    DBHelper.ExecuteNonQuery(tran, "js_UpdateNet_w", parm, CommandType.StoredProcedure);


                                    anzhi = shangji;
                                }

                                DBHelper.ExecuteNonQuery(tran, "update memberinfo set placement='" + tuijian + "', direct='" + anzhi + "' where number='" + drh["number"].ToString() + "'");


                            }

                            //删除并且备份
                            if (Convert.ToInt32(DBHelper.ExecuteNonQuery(tran, "insert into memberinfo_back select * from memberinfo where number='" + dr["number"].ToString() + "'")) > 0)
                                DBHelper.ExecuteNonQuery(tran, "delete from memberinfo where orderid='" + dr["number"].ToString() + "'");
                            if (Convert.ToInt32(DBHelper.ExecuteNonQuery(tran, "insert into memberorder_back select * from memberorder where orderid='" + dr["orderid"].ToString() + "'")) > 0)
                                DBHelper.ExecuteNonQuery(tran, "delete from memberorder where orderid='" + dr["orderid"].ToString() + "'");
                            if (Convert.ToInt32(DBHelper.ExecuteNonQuery(tran, "insert into memberdetails_back select * from memberdetail where orderid='" + dr["orderid"].ToString() + "'")) > 0)
                                DBHelper.ExecuteNonQuery(tran, "delete from memberdetails where orderid='" + dr["orderid"].ToString() + "'");
                        }
                        else if (dr["isfuxiao"].ToString() == "1")
                        {
                            //删除并且备份
                            if (Convert.ToInt32(DBHelper.ExecuteNonQuery(tran, "insert into memberorder_back select * from memberorder where orderid='" + dr["orderid"].ToString() + "'")) > 0)
                                DBHelper.ExecuteNonQuery(tran, "delete from memberorder where orderid='" + dr["orderid"].ToString() + "'");
                        }
                    }

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    string ee = ex.Message;
                    return;
                }
            }
        }

        /// <summary>
        /// 获取协议
        /// </summary>
        /// <returns></returns>
        public static string GetAgreement()
        {
            return CommonDataDAL.GetAgreement();
        }

        public static string GetAgreement(int CountryCode, string LanguageCode)
        {
            return CommonDataDAL.GetAgreement(CountryCode, LanguageCode);
        }

        /// <summary>
        /// 编号获取会员昵称
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string GetPetNameByNumber(string number)
        {
            return MemberInfoDAL.GetPetNameByNumber(number);
        }

        /// <summary>
        /// 更新协议内容
        /// </summary>
        /// <param name="agreement">协议内容</param>
        /// <returns></returns>
        public static int UpdateAgreement(string agreement)
        {
            return CommonDataDAL.UpdateAgreement(agreement);
        }

        public static int UpdateAgreement(string Agreement, int CountryCode, string LanguageCode)
        {
            return CommonDataDAL.UpdateAgreement(Agreement, CountryCode, LanguageCode);
        }

        public static int InsertAgreement(string Agreement, int CountryCode, string LanguageCode)
        {
            return CommonDataDAL.InsertAgreement(Agreement, CountryCode, LanguageCode);
        }

        public static int RegisterAgreement(string Agreement, int CountryCode, string LanguageCode)
        {
            int Count = CommonDataDAL.GetAgreementByCode(CountryCode, LanguageCode);
            int Num = 0;
            if (Count > 0)
            {
                Num = UpdateAgreement(Agreement, CountryCode, LanguageCode);
            }
            else
                Num = InsertAgreement(Agreement, CountryCode, LanguageCode);

            return Num;
        }

        public static int RegisterAgreement(string Agreement, int CountryCode, string LanguageCode, int aType)
        {
            int Count = CommonDataDAL.GetAgreementByCode(CountryCode, LanguageCode, aType);
            int Num = 0;
            if (Count > 0)
            {
                Num = CommonDataDAL.UpdateAgreement(Agreement, CountryCode, LanguageCode, aType);
            }
            else
                Num = CommonDataDAL.InsertAgreement(Agreement, CountryCode, LanguageCode, aType);

            return Num;
        }

        /// <summary>
        /// 根据产品ID获取产品信息
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        public static DataTable GetProductById(string productid)
        {
            return ProductDAL.SelectProductById(productid);
        }

        public static DataTable GetGongGao(string isNum)
        {
            return CommonDataDAL.GetGongGao(isNum);
        }

        public static int isYueDu(string bh, string isNum)
        {
            return CommonDataDAL.isYueDu(bh, isNum);
        }

        public static int isYD(string bh, string isNum)
        {
            return CommonDataDAL.isYD(bh, isNum);
        }


        /// <summary>
        /// 获取店铺联系方式
        /// </summary>
        /// <returns></returns>
        public static DataTable GetStorePhone(string storeid)
        {
            return StoreInfoDAL.SelectStorePhone(storeid);
        }

        /// <summary>
        /// 获取店铺联系方式
        /// </summary>
        /// <returns></returns>
        public static DataTable GetMemberPhone(string number)
        {
            return MemberInfoDAL.GetMemberPhone(number);
        }

        /// <summary>
        /// 更新级别图标
        /// </summary>
        /// <param name="id"></param>
        /// <param name="path"></param>
        public static void upLevelIcon(int id, string path)
        {
            string sql = "update BSCO_level set ICOPath=@path where id=@id";
            SqlParameter[] parm = new SqlParameter[] {
            new SqlParameter("@path",SqlDbType.NVarChar,100),new SqlParameter("@id",SqlDbType.Int)};
            parm[0].Value = path;
            parm[1].Value = id;
            DBHelper.ExecuteNonQuery(sql, parm, CommandType.Text);
        }
        /// <summary>
        /// 根据级别图标id获得图标路径
        /// </summary>
        /// <param name="id"></param>
        public static string GetLevelIconByID(int id)
        {
            string sql = "select isnull(ICOPath,'') from BSCO_level where id=@id";
            SqlParameter[] parm = new SqlParameter[] {
            new SqlParameter("@id",SqlDbType.Int)};
            parm[0].Value = id;
            return DBHelper.ExecuteScalar(sql, parm, CommandType.Text).ToString();
        }

        /// <summary>
        /// 读取系统的最大期数。
        /// write by 郑华超
        /// </summary>
        /// <returns></returns>
        public static int GetMaxqishu()
        {
            try
            {
                if (HttpContext.Current.Application["maxqishu"] == null)
                {
                    HttpContext.Current.Application["maxqishu"] = CommonDataDAL.GetMaxExpect();
                }
                else
                {
                }
                return (int)HttpContext.Current.Application["maxqishu"];
            }
            catch
            {
                return (int)CommonDataDAL.GetMaxExpect();
            }
        }

        public static string GetTopManageID(int type)
        {
            return DAL.CommonDataDAL.GetManageID(type);
        }

        /// <summary>
        /// 拆分产品信息--订单信息
        /// 注：把组合产品拆分成单品
        /// </summary>
        /// <param name="ods">原产品信息</param>
        /// <returns>拆分后产品信息</returns>
        public static IList<OrderDetailModel> GetNewOrderDetail(IList<OrderDetailModel> ods)
        {
            IList<OrderDetailModel> orderdetails = new List<OrderDetailModel>();
            foreach (OrderDetailModel od in ods)
            {
                if (ProductDAL.GetIsCombine(od.ProductId))
                {
                    IList<ProductCombineDetailModel> comDetails = ProductCombineDetailDAL.GetCombineDetil(od.ProductId);
                    foreach (ProductCombineDetailModel comDetail in comDetails)
                    {
                        int count = 0;
                        foreach (OrderDetailModel detail in orderdetails)
                        {
                            if (detail.ProductId == comDetail.SubProductID)
                            {
                                detail.Quantity = (comDetail.Quantity * od.Quantity) + detail.Quantity;
                                count++;
                            }
                        }
                        if (count == 0)
                        {
                            OrderDetailModel orderdetail = new OrderDetailModel();
                            orderdetail.Quantity = comDetail.Quantity * od.Quantity;
                            orderdetail.ProductId = comDetail.SubProductID;
                            orderdetails.Add(orderdetail);
                        }
                    }
                }
                else
                {
                    int count = 0;
                    foreach (OrderDetailModel detail in orderdetails)
                    {
                        if (detail.ProductId == od.ProductId)
                        {
                            detail.Quantity = od.Quantity + detail.Quantity;
                            count++;
                        }
                    }
                    if (count == 0)
                    {
                        OrderDetailModel orderdetail = new OrderDetailModel();
                        orderdetail.Quantity = od.Quantity;
                        orderdetail.ProductId = od.ProductId;
                        orderdetails.Add(orderdetail);
                    }

                }
            }
            return orderdetails;
        }

        /// <summary>
        /// 拆分产品信息--报单信息
        /// 注：把组合产品拆分成单品
        /// </summary>
        /// <param name="ods">原产品信息</param>
        /// <returns>拆分后产品信息</returns>
        public static IList<MemberDetailsModel> GetNewOrderDetail(IList<MemberDetailsModel> ods)
        {
            IList<MemberDetailsModel> orderdetails = new List<MemberDetailsModel>();
            foreach (MemberDetailsModel od in ods)
            {
                if (ProductDAL.GetIsCombine(od.ProductId))
                {
                    IList<ProductCombineDetailModel> comDetails = ProductCombineDetailDAL.GetCombineDetil(od.ProductId);
                    foreach (ProductCombineDetailModel comDetail in comDetails)
                    {
                        int count = 0;
                        foreach (MemberDetailsModel detail in orderdetails)
                        {
                            if (detail.ProductId == comDetail.SubProductID)
                            {
                                detail.Quantity = (comDetail.Quantity * od.Quantity) + detail.Quantity;
                                count++;
                            }
                        }
                        if (count == 0)
                        {
                            MemberDetailsModel orderdetail = new MemberDetailsModel();
                            orderdetail.Quantity = comDetail.Quantity * od.Quantity;
                            orderdetail.ProductId = comDetail.SubProductID;
                            orderdetails.Add(orderdetail);
                        }
                    }
                }
                else
                {
                    int count = 0;
                    foreach (MemberDetailsModel detail in orderdetails)
                    {
                        if (detail.ProductId == od.ProductId)
                        {
                            detail.Quantity = od.Quantity + detail.Quantity;
                            count++;
                        }
                    }
                    if (count == 0)
                    {
                        MemberDetailsModel orderdetail = new MemberDetailsModel();
                        orderdetail.Quantity = od.Quantity;
                        orderdetail.ProductId = od.ProductId;
                        orderdetails.Add(orderdetail);
                    }

                }
            }

            return orderdetails;
        }

        /// <summary>
        /// 拆分产品信息--报单信息
        /// 注：把组合产品拆分成单品
        /// </summary>
        /// <param name="ods">原产品信息</param>
        /// <returns>拆分后产品信息</returns>
        public static IList<MemberDetailsModel> GetNewOrderDetail1(IList<MemberDetailsModel> ods)
        {
            IList<MemberDetailsModel> orderdetails = new List<MemberDetailsModel>();
            foreach (MemberDetailsModel od in ods)
            {
                if (ProductDAL.GetIsCombine(od.ProductId))
                {
                    IList<ProductCombineDetailModel> comDetails = ProductCombineDetailDAL.GetCombineDetil(od.ProductId);
                    foreach (ProductCombineDetailModel comDetail in comDetails)
                    {
                        int count = 0;
                        foreach (MemberDetailsModel detail in orderdetails)
                        {
                            if (detail.ProductId == comDetail.SubProductID)
                            {
                                detail.Quantity = (comDetail.Quantity * od.Quantity) + detail.Quantity;
                                detail.NotEnoughProduct = (comDetail.Quantity * od.NotEnoughProduct) + detail.NotEnoughProduct;
                                count++;
                            }
                        }
                        if (count == 0)
                        {
                            MemberDetailsModel orderdetail = new MemberDetailsModel();
                            orderdetail.Quantity = comDetail.Quantity * od.Quantity;
                            orderdetail.ProductId = comDetail.SubProductID;
                            orderdetail.NotEnoughProduct = comDetail.Quantity * od.NotEnoughProduct;
                            orderdetail.StoreId = od.StoreId;
                            orderdetail.Pv = od.Pv;
                            orderdetail.Price = od.Price;
                            orderdetail.OrderId = od.OrderId;
                            orderdetail.Number = od.Number;
                            orderdetail.ExpectNum = od.ExpectNum;
                            orderdetail.LackTotalNumber = od.LackTotalNumber;
                            orderdetails.Add(orderdetail);
                        }
                    }
                }
                else
                {
                    int count = 0;
                    foreach (MemberDetailsModel detail in orderdetails)
                    {
                        if (detail.ProductId == od.ProductId)
                        {
                            detail.Quantity = od.Quantity + detail.Quantity;
                            detail.NotEnoughProduct = od.NotEnoughProduct + detail.NotEnoughProduct;
                            count++;
                        }
                    }
                    if (count == 0)
                    {
                        MemberDetailsModel orderdetail = new MemberDetailsModel();
                        orderdetail.Quantity = od.Quantity;
                        orderdetail.ProductId = od.ProductId;
                        orderdetail.NotEnoughProduct = od.NotEnoughProduct;
                        orderdetail.StoreId = od.StoreId;
                        orderdetail.Pv = od.Pv;
                        orderdetail.Price = od.Price;
                        orderdetail.OrderId = od.OrderId;
                        orderdetail.Number = od.Number;
                        orderdetail.ExpectNum = od.ExpectNum;
                        orderdetail.LackTotalNumber = od.LackTotalNumber;
                        orderdetails.Add(orderdetail);
                    }

                }
            }

            return orderdetails;
        }


        /// <summary>
        /// 获取最大期数——pl ---ds2012 --- www-b874dce8700
        /// </summary>
        /// <returns></returns>
        public static int getMaxqishu()
        {
            return (int)CommonDataDAL.GetMaxExpect();
        }
        /// <summary>
        /// 获取石斛积分价格
        /// </summary>
        /// <returns></returns>
        public static object GetMaxDayPrice()
        {
            return CommonDataDAL.GetMaxDayPrice();
        }
        public static DataTable GetBindAddress(string number)
        {
            return DAL.CommonDataDAL.GetBindAddress(number);
        }

        public static DataTable GetBindStoreAddress(string storeId)
        {
            return DAL.CommonDataDAL.GetBindStoreAddress(storeId);
        }

        public static DataTable GetBindOrderAddress(string StoreId)
        {
            return DAL.CommonDataDAL.GetBindOrderAddress(StoreId);
        }

        public static string GetAddressByCode(string code)
        {
            return DAL.CommonDataDAL.GetAddressByCode(code);
        }

        /// <summary>
        /// 根据管理员编号找出可查看的会员范围
        /// </summary>
        /// <param name="manageID">管理员编号</param>
        /// <returns>会员编号</returns>
        public static string GetWLNumber(string manageID, out int count)
        {
            return DAL.CommonDataDAL.GetWLNumber(manageID, out count);
        }

        public static string GetWLNumber(string manageID, out int count, string tName)
        {
            return DAL.CommonDataDAL.GetWLNumber(manageID, out count, tName);
        }
        /// <summary>
        /// 获取对应编号的序号
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string GetNumberXuhao(string number)
        {
            return DAL.CommonDataDAL.GetNumberXuhao(number);
        }



        public static bool GetRole(string tj, string number)
        {
            return DAL.CommonDataDAL.GetRole(tj, number);
        }

        /// <summary>
        /// 检查对应会员的权限
        /// </summary>
        /// <param name="number">会员编号</param>
        /// <param name="xuhao">管理者序号</param>
        /// <returns></returns>
        public static bool CheckRole(string number, string xuhao)
        {
            return DAL.CommonDataDAL.CheckRole(number, xuhao);
        }

        public static bool GetRole_WX(string manageID, string number)
        {
            bool isBool = false;
            if (manageID != CommonDataBLL.GetTopManageID(1))
            {
                string sql = "select number,type from viewmanage where manageid='" + manageID + "'";
                //查询管理员所管理的团队起点编号
                DataTable dtViewManage = DBHelper.ExecuteDataTable(sql);
                DataTable dtNumbers = new DataTable();
                DataRow[] rowNums = null;
                if (dtViewManage != null && dtViewManage.Rows.Count > 0)
                {
                    SqlParameter[] paras = { new SqlParameter("@Number", SqlDbType.NVarChar, 20), new SqlParameter("@Qishu", SqlDbType.Int), new SqlParameter("@type", SqlDbType.Int) };
                    foreach (DataRow row in dtViewManage.Rows)
                    {
                        paras[0].Value = row["number"].ToString();
                        paras[1].Value = CommonDataBLL.GetMaxqishu();
                        paras[2].Value = row["type"].ToString() == "0" ? "2" : "1";
                        //查询起点编号的团队网络
                        dtNumbers = DBHelper.ExecuteDataTable("SeekNetNumber_WX", paras, CommandType.StoredProcedure);
                        if (dtNumbers != null && dtNumbers.Rows.Count > 0)
                        {
                            rowNums = dtNumbers.Select(" number='" + number + "'");
                            if (rowNums.Count() > 0)
                            {
                                isBool = true;
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                isBool = true;
            }
            return isBool;
        }


        /// <summary>
        /// 根据管理员编号找出可查看的会员范围
        /// </summary>
        /// <param name="manageID">管理员编号</param>
        /// <returns>会员编号</returns>
        public static string GetWLNumber1(string manageID, out int count)
        {
            return DAL.CommonDataDAL.GetWLNumber1(manageID, out count);
        }

        /// <summary>
        /// 判断会员编号是否存在
        /// </summary>
        /// <returns></returns>
        public static int getCountNumber(string number)
        {
            return (int)DAL.CommonDataDAL.getCountNumber(number);
        }

        public static bool SetResetEmail(string email, string username, string pwd, string emailAddress)
        {
            return DAL.CommonDataDAL.SetResetEmail(email, username, pwd, emailAddress);
        }

        public static DataTable GetEmail()
        {
            return DAL.CommonDataDAL.GetEmail();
        }

        public static bool SetMemberTotalDefray(SqlTransaction tran, string number, double totalMoney)
        {
            return DAL.CommonDataDAL.SetMemberTotalDefray(tran, number, totalMoney);
        }

        public static bool SetMemberTotalRemittances(SqlTransaction tran, string number, double totalMoney)
        {
            return DAL.CommonDataDAL.SetMemberTotalRemittances(tran, number, totalMoney);
        }

        public static bool SetMemberTotalDefray(string number, double totalMoney)
        {
            return DAL.CommonDataDAL.SetMemberTotalDefray(number, totalMoney);
        }

        public static string getManageName(string manageNumber)
        {
            return DAL.CommonDataDAL.getManageName(manageNumber);
        }

        public static string getOutStorageOrderId(string storeOrderId)
        {
            return DAL.CommonDataDAL.getOutStorageOrderId(storeOrderId);
        }

        public static int GetMemberOrder(string date, string storeId)
        {
            return (int)DAL.CommonDataDAL.GetMemberOrder(date, storeId);
        }

        public static int GetDefrayState(string OrderId)
        {
            return (int)DAL.CommonDataDAL.GetDefrayState(OrderId);
        }

        public static int GetStoreOrder(string storeId)
        {
            return (int)DAL.CommonDataDAL.GetStoreOrder(storeId);
        }

        public static DataTable GetMessageSend()
        {
            return DAL.CommonDataDAL.GetMessageSend();
        }

        public static DataTable GetMessageReceive(string storeId)
        {
            return DAL.CommonDataDAL.GetMessageReceive(storeId);
        }

        public static string getManageID(int type)
        {
            return DAL.CommonDataDAL.GetManageID(type);
        }

        public static int GetRegisterQishu(string number)
        {
            return DAL.CommonDataDAL.GetRegisterQishu(number);
        }

        public static bool SetSendType(string OrderId)
        {
            return DAL.CommonDataDAL.SetSendType(OrderId);
        }

        public static int getDeliveryflag(string OrderID)
        {
            return (int)DAL.CommonDataDAL.getDeliveryflag(OrderID);
        }

        public static string getGrantState()
        {
            return (string)DAL.CommonDataDAL.getGrantState();
        }

        public static bool SetGrantState(string grantState)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    if (!DAL.CommonDataDAL.SetGrantState(tran, grantState))
                    {
                        tran.Rollback();
                        return false;
                    }

                    if (!DAL.CommonDataDAL.SetGrantMenu(tran, grantState))
                    {
                        tran.Rollback();
                        return false;
                    }

                    tran.Commit();
                    return true;
                }
                catch
                {
                    tran.Rollback();
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        /// <summary>
        ///会员提现设置 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="WithdrawMinMoney">提现最低金额</param>
        /// <param name="WithdrawSXF">提现手续费比例</param>
        public static bool SetGrantTx(string WithdrawMinMoney,string WithdrawSXF)
        {
            using (SqlConnection conn = DAL.DBHelper.SqlCon())
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    if (!DAL.CommonDataDAL.SetGrantTx(tran, WithdrawMinMoney, WithdrawSXF))
                    {
                        tran.Rollback();
                        return false;
                    }
                    
                    tran.Commit();
                    return true;
                }
                catch
                {
                    tran.Rollback();
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public static bool Jinliu(string Dhktime, string Dcstime, string sfgc, string hfgc, string tkje, string hkje,string txtEnote)
        {
            using (SqlConnection conn = DAL.DBHelper.SqlCon())
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    if (!DAL.CommonDataDAL.Jinliu(tran, Dhktime,  Dcstime,  sfgc,  hfgc,  tkje,  hkje, txtEnote))
                    {
                        tran.Rollback();
                        return false;
                    }

                    tran.Commit();
                    return true;
                }
                catch
                {
                    tran.Rollback();
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public static string GetElectronicaccountid(string number)
        {
            return DAL.CommonDataDAL.GetElectronicaccountid(number);
        }

        public static DataTable GetNetWorkDisplayStatus(int type)
        {
            return DAL.CommonDataDAL.GetNetWorkDisplayStatus(type);
        }

        public static DataTable GetNetWorkDisplayStatus1(int type)
        {
            return DAL.CommonDataDAL.GetNetWorkDisplayStatus1(type);
        }

        /// <summary>
        /// 获取会员可提现余额
        /// </summary>
        /// <returns></returns>
        public static string GetLeftMoney(string number)
        {
            return DAL.CommonDataDAL.GetLeftMoney(number);
        }

        /// <summary>
        /// 获取最低提现金额
        /// </summary>
        /// <returns></returns>
        public static string GetMinTxMoney()
        {
            return DAL.CommonDataDAL.GetMinTxMoney();
        }

        /// <summary>
        ///获取手续费
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
        public static string GetLeftsxfMoney(string money)
        {
            return DAL.CommonDataDAL.GetLeftsxfMoney(money);
        }
        /// <summary>
        /// 获取违约金
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>

        public static string GetLefwyjMoney(string money)
        {
            return DAL.CommonDataDAL.GetLefwyjMoney(money);
        }

        public static string GetLeftMoney1(string number)
        {
            return DAL.CommonDataDAL.GetLeftMoney1(number);
        }

        /// <summary>
        /// 获取店长编号
        /// </summary>
        /// <returns></returns>
        public static string getStoreNumber(string storId)
        {
            return DAL.CommonDataDAL.getStoreNumber(storId).ToString();
        }

        /// <summary>
        /// 根据会员编号读取所属店铺
        /// </summary>
        /// <returns></returns>
        public static string getNumberStoreID(string number)
        {
            return DAL.CommonDataDAL.getNumberStoreID(number);
        }

        /// <summary>
        /// 获取产品的剩余库存
        /// </summary>
        /// <returns></returns>
        public static int GetLeftLogicProductInventory(int productID)
        {
            return (int)DAL.CommonDataDAL.GetLeftLogicProductInventory(productID);
        }

        /// <summary>
        /// 读取证件编码
        /// </summary>
        /// <returns></returns>
        public static string getPaperType(int id)
        {
            return DAL.CommonDataDAL.getPaperType(id);
        }

        /// <summary>
        /// 判断管理员是否已有此网络的查看权限
        /// </summary>
        /// <returns></returns>
        public static int getNumberRole(string number, string manageID, int type)
        {
            return (int)DAL.CommonDataDAL.getNumberRole(number, manageID, type);
        }

        /// <summary>
        /// 添加管理员可以查看团队的网络图
        /// </summary>
        /// <returns></returns>
        public static int AddViewNumber(string number, string manageID, int type)
        {
            return (int)DAL.CommonDataDAL.AddViewNumber(number, manageID, type);
        }



        /// <summary>
        /// 绑定支付方式控件
        /// </summary>
        /// <param name="rbt">控件名</param>
        /// <param name="isStore">使用类型：0注册，报单。1店汇款。2店铺在线订货。</param>
        public static void GetPaymentType(RadioButtonList rbt, int isStore)
        {
            MemOrderLineDAL.GetPaymentType(rbt, isStore);
        }
        /// <summary>
        /// 绑定支付方式控件
        /// </summary>
        /// <param name="rbt">控件名</param>
        /// <param name="isStore">使用类型：0注册，报单。1店汇款。2店铺在线订货。</param>
        public static void GetPaymentType2(DropDownList dll, int isStore)
        {
            MemOrderLineDAL.GetPaymentType2(dll, isStore);
        }
        /// <summary>
        /// 获取支付方式名称
        /// </summary>
        /// <returns>返回获取支付方式名称</returns>
        public static string GetpaymentName(int payID, int isStore)
        {
            return MemOrderLineDAL.GetpaymentName(payID, isStore);
        }

        /// <summary>
        /// 获取支付方式名称
        /// </summary>
        /// <returns>返回获取支付方式名称</returns>
        public static int GetNumberRegExpect(string number)
        {
            return DAL.CommonDataDAL.getNumberRegExpect(number);
        }

        /// <summary>
        /// 绑定证件类型
        /// </summary>
        /// <param name="ddl">待绑定控件</param>
        public static void BindPaperType(DropDownList ddl)
        {//select papertypecode,papertype,id from bsco_PaperType
            SqlDataReader dr = DBHelper.ExecuteReader("select * from T_translation t join BSCO_PaperType b on t.primarykey=b.id  and  t.description in (select PaperType from BSCO_PaperType)");
            while (dr.Read())
            {
                //ListItem list2 = new ListItem(dr["papertype"].ToString(), dr["papertypecode"].ToString().Trim());
                ListItem list2 = new ListItem(CommonDataBLL.GetLanguageStr(int.Parse(dr["id"].ToString()), "bsco_PaperType", "papertype"), dr["papertypecode"].ToString().Trim());
                ddl.Items.Add(list2);
            }
            dr.Close();
        }

        /// <summary>
        /// 读取系统的最大期数。GetSjBh
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static int getMaxqishu(HttpContext context)
        {
            if (context.Application["maxqishu"] == null)
                context.Application["maxqishu"] = CommonDataDAL.GetMaxExpect();
            return int.Parse(context.Application["maxExperNum"].ToString());
        }

        /// <summary>
        /// 读取系统的最大期数。——ds2012——tianfeng
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static int getMaxqishu(SqlTransaction tran)
        {
            return Convert.ToInt32(CommonDataDAL.GetMaxExpect(tran));
        }


        //全角转半角
        public static string quanjiao(string quanjiao)
        {
            string QJstr = quanjiao;
            char[] c = QJstr.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                byte[] b = System.Text.Encoding.Unicode.GetBytes(c, i, 1);
                if (b.Length == 2)
                {
                    if (b[1] == 255)
                    {
                        b[0] = (byte)(b[0] + 32);
                        b[1] = 0;
                        c[i] = System.Text.Encoding.Unicode.GetChars(b)[0];
                    }
                }
            }
            string strNew = new string(c);

            return strNew;

        }


        /// <summary>
        /// 操作IP地址
        /// </summary>
        public static string OperateIP
        {
            get
            {
                //获取IP地址
                HttpContext.Current.Session["OperateIP"] = HttpContext.Current.Request.UserHostAddress;
                return HttpContext.Current.Session["OperateIP"].ToString();
            }
            set
            {
                HttpContext.Current.Session["OperateIP"] = value;
            }
        }


        /// <summary>
        /// 操作人员编号
        /// </summary>
        public static string OperateBh
        {
            get
            {
                if (HttpContext.Current.Session["Company"] != null)
                    return HttpContext.Current.Session["Company"].ToString();
                if (HttpContext.Current.Session["Store"] != null)
                    return HttpContext.Current.Session["Store"].ToString();
                if (HttpContext.Current.Session["Member"] != null)
                    return HttpContext.Current.Session["Member"].ToString();
                return "";
            }

        }
        /// <summary>
        /// 会员电子钱包余额
        /// </summary>
        /// <returns></returns>
        public static object EctIsEnough(string EctNumber)
        {
            return CommonDataDAL.EctBalance(EctNumber);
        }
        /// <summary>
        /// 店铺产品库存量
        /// </summary>
        /// <param name="storeId">店铺编号</param>
        /// <param name="productId">产品编号</param>
        /// <returns></returns>
        public static object StoreStock(string storeId, int productId)
        {
            return CommonDataDAL.StoreStock(storeId, productId);
        }

        /// <summary>
        /// 获取上级编号
        /// </summary>
        /// <returns>上级编号</returns>
        public static string UpBianhao(string number)
        {
            return DAL.CommonDataDAL.UpBianhao(number);
        }

        /// <summary>
        /// 店铺剩余金额
        /// </summary>
        /// <param name="storeId">店铺编号</param>
        /// <returns></returns>
        public static object StoreLaveAmount(string storeId)
        {
            return CommonDataDAL.StoreLaveAmount(storeId);
        }
        /// <summary>
        /// 更新店铺库存
        /// </summary>
        /// <param name="storeId">店铺编号</param>
        /// <param name="productId">产品编号</param>
        /// <param name="count">产品数量</param>
        /// <returns></returns>
        public static bool UPStoreStock(SqlTransaction tran, string storeId, int productId, int count)
        {
            return CommonDataDAL.UPStoreStock(tran, storeId, productId, count);
        }
        /// <summary>
        /// 更新会员电子钱包
        /// </summary>
        /// <param name="ectnumber">电子钱包编号</param>
        /// <param name="amount">修改金额</param>
        /// <returns></returns>E:\项目\BLL\CommonClass\CommonDataBLL.cs
        public static bool UPMemberEct(string ectnumber, decimal amount)
        {
            return CommonDataDAL.UPMemberEct(ectnumber, amount);
        }

        /// <summary>
        /// 获取电子账户余额
        /// </summary>
        /// <param name="EctNumber"></param>
        /// <returns></returns>
        public static object EctBalance(string EctNumber)
        {
            return new AddOrderDataDAL().HaveMoney(EctNumber);
        }
        /// <summary>
        /// 更新会员报单表
        /// </summary>
        /// <param name="orderId">报单编号</param>
        /// <param name="volume">期数</param>
        /// <returns></returns>
        public static bool ConfirmMembersOrder(SqlTransaction tran, string orderId, int maxQs)
        {
            return CommonDataDAL.ConfirmMembersOrder(tran, orderId, maxQs);
        }

        /// <summary>
        /// 更新会员报单表
        /// </summary>
        /// <param name="orderId">报单编号</param>
        /// <param name="volume">期数</param>
        /// <returns></returns>
        public static bool ConfirmMembersOrder(SqlTransaction tran, string orderId, int maxQs, decimal enoughproductMoney, decimal lackproductmoney)
        {
            return CommonDataDAL.ConfirmMembersOrder(tran, orderId, maxQs, enoughproductMoney, lackproductmoney);
        }

        /// <summary>
        /// 更新会员报单明细
        /// </summary>
        /// <param name="orderId">报单编号</param>
        /// <param name="volume">期数</param>
        /// <returns></returns>
        public static bool ConfirmMembersDetails(SqlTransaction tran, int productId, string orderId, decimal NotEnoughProduct)
        {
            return CommonDataDAL.ConfirmMembersDetails(tran, productId, orderId, NotEnoughProduct);
        }

        /// <summary>
        /// 更新结算表
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="number">会员编号</param>
        /// <param name="xgfenshu">新个分数</param>
        /// <param name="maxQs">当前期数</param>
        /// <returns></returns>
        public static bool UPMemberInfoBalance(SqlTransaction tran, string number, decimal xgfenshu, int maxQs)
        {
            return CommonDataDAL.UPMemberInfoBalance(tran, number, xgfenshu, maxQs);
        }

        public static int GetMaxExpectNum(HttpContext context)
        {
            if (context.Application["maxqishu"] == null)
            {
                context.Application["maxqishu"] = CommonDataDAL.GetMaxExpect();
            }
            return int.Parse(context.Application["maxqishu"].ToString());
        }
        /// <summary>
        /// 结算程序文件名(不要扩展名) --ds2012--www-b874dce8700
        /// </summary>
        public static string JiesuanProgramFilename
        {
            get
            {
                string FileName = "ds2010";	//结算程序文件名(不要扩展名);
                return FileName.ToLower();
            }
        }
        /// <summary>
        /// 返回真实文件地址
        /// </summary>
        /// <param name="path">输入路径</param>
        /// <param name="dep">开始深度 .默认0 , 小于5</param>
        /// <returns></returns>
        public static string GetPath(string path, int dep)
        {
            if (dep > 5) return path;
            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(path)) == true)
                return path;
            else
                return GetPath("../" + path, dep + 1);
        }


        public static void CommonData()
        {
        }

        private static void LoadFromDataBase(string _FileName, string language)
        {
            if (System.Web.HttpContext.Current.Application["Load_" + language + "_" + _FileName] == null)	//第一次加载页面时从数据库读入 Application
            {
                System.Data.SqlClient.SqlDataReader dread = DBHelper.ExecuteReader("SELECT location,text FROM TranTo" + language + " WHERE filename='" + _FileName + "'");
                System.Collections.Hashtable htb = (System.Collections.Hashtable)System.Web.HttpContext.Current.Application["Dict_" + language];
                while (dread.Read())
                {
                    htb.Add(dread.GetString(0), dread.GetString(1));
                }
                dread.Close();
                System.Web.HttpContext.Current.Application.UnLock();
                System.Web.HttpContext.Current.Application.Lock();
                System.Web.HttpContext.Current.Application["Dict_" + language] = htb;
                System.Web.HttpContext.Current.Application["Load_" + language + "_" + _FileName] = true;
                System.Web.HttpContext.Current.Application.UnLock();
            }
        }
        public static void LoadClassesTran()
        {
            //string language = System.Web.HttpContext.Current.Session["language"].ToString();
            //if (System.Web.HttpContext.Current.Application["Dict_" + language] == null)
            //{
            //    System.Web.HttpContext.Current.Application["Dict_" + language] = new System.Collections.Hashtable();
            //}
            //if (System.Web.HttpContext.Current.Application["LoadClasses_" + language] == null)
            //{
            //    LoadFromDataBase("classes/commondata.cs", language);
            //    LoadFromDataBase("Classes/jiegou.cs", language);
            //    LoadFromDataBase("Classes/ProductTree.cs", language);
            //    LoadFromDataBase("classes/AjaxClass.cs", language);
            //    System.Web.HttpContext.Current.Application["LoadClasses_" + language] = true;
            //}
        }
        /// <summary>
        /// 读取翻译后的内容，管理员使用时不翻译
        /// </summary>
        /// <returns>翻译后的内容</returns>
        public static string GetTran(string Location, string DefaultText)
        {
            //if (false && System.Web.HttpContext.Current.Session["glbh"] != null)
            //{
            //    return DefaultText;
            //}
            //else
            //{
            //    string language = System.Web.HttpContext.Current.Session["language"].ToString();
            //    System.Collections.Hashtable htb = (System.Collections.Hashtable)System.Web.HttpContext.Current.Application["Dict_" + language];
            //    if (htb.Contains(Location))
            //    {
            //        return htb[Location].ToString();	//返回翻译的字符串
            //    }
            //    else
            //    {
            return DefaultText;	//返回默认字符串
            //    }
            //}
        }

        //判断是否是数字
        /// <summary>
        /// 
        /// </summary>
        public static string GetClassTran(string Location, string DefaultText)
        {
            //string[] arr = Location.Split(new Char[] { '_' });
            //string _filename = arr[0];

            //if (false && HttpContext.Current.Session["glbh"] != null)
            //{
            //    return DefaultText;
            //}
            //else
            //{
            //    //取不到session，谁改的
            //    string language;
            //    if (System.Web.HttpContext.Current.Session["language"] != null)
            //    {
            //        language = System.Web.HttpContext.Current.Session["language"].ToString();
            //    }
            //    else
            //    {
            //        language = "chinese";
            //    }
            //    System.Data.SqlClient.SqlDataReader dread = DBHelper.ExecuteReader("SELECT location,text FROM TranTo" + language + " WHERE filename='" + _filename + "'");
            //    //System.Collections.Hashtable htb = (System.Collections.Hashtable)System.Web.HttpContext.Current.Application["Dict_"+language];
            //    System.Collections.Hashtable htb = new System.Collections.Hashtable();
            //    while (dread.Read())
            //    {
            //        htb.Add(dread.GetString(0), dread.GetString(1));
            //    }
            //    dread.Close();

            //    if (htb.Contains(Location))
            //    {
            //        return htb[Location].ToString();	//返回翻译的字符串
            //    }
            //    else
            //    {
            return DefaultText;	//返回默认字符串
            //    }
            //}

        }

        public static bool isNumber(string str)
        {
            try
            {
                Convert.ToDecimal(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 绑定货币(货币名称 -- 汇率)列表
        /// </summary>
        /// <param name="list">要添加期数的控件</param>		
        public static void BindCurrency_RateList(DropDownList list, string Default_Currency)
        {
            if (System.Web.HttpContext.Current.Session["language"] == null)
                System.Web.HttpContext.Current.Session["language"] = "chinese";

            if (System.Web.HttpContext.Current.Session["language"].ToString().ToLower() == "chinese" || System.Web.HttpContext.Current.Session["language"].ToString().ToUpper() == "中文")
            {
                SqlDataReader dr = DBHelper.ExecuteReader("Select rate,name From Currency order by id");
                while (dr.Read())
                {
                    ListItem list2 = new ListItem(dr["name"].ToString(), dr["rate"].ToString());
                    //				if(Default_Currency.Trim()=="" && dr["name"].ToString()=="美元")
                    //					list2.Selected=true;
                    if (Default_Currency == dr["name"].ToString())
                        list2.Selected = true;
                    list.Items.Add(list2);
                }
                dr.Close();
            }
            else
            {
                int ID = Convert.ToInt32(DBHelper.ExecuteScalar("select id from Language where name='" + System.Web.HttpContext.Current.Session["language"].ToString() + "'"));
                string Sql = @"select rate,(select languagename from LanguageTrans where 
							LanguageTrans.OldID=Currency.id and 
							LanguageTrans.Columnsname='name' and languageid=" + ID + @") as name
							from Currency";
                DataTable dt = DBHelper.ExecuteDataTable(Sql);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ListItem listE = new ListItem(dt.Rows[i]["name"].ToString(), dt.Rows[i]["rate"].ToString());

                        list.Items.Add(listE);
                    }
                }
            }


        }

        /// <summary>
        /// 绑定货币(货币名称 -- 汇率ID )列表
        /// </summary>
        /// <param name="list">要添加货币的控件(绑定一个标准币种)</param>		
        public static void BindCurrency_IDListBZ(DropDownList list, string Default_Currency)
        {
            SqlDataReader dr = DBHelper.ExecuteReader("Select ID,name From currency where id=standardmoney");
            while (dr.Read())
            {
                ListItem list2 = new ListItem(dr["name"].ToString(), dr["id"].ToString());
                //				if(Default_Currency.Trim()=="" && dr["name"].ToString()=="美元")
                //					list2.Selected=true;
                if (Default_Currency == dr["name"].ToString())
                    list2.Selected = true;
                list.Items.Add(list2);
            }
            dr.Close();
        }

        /// <summary>
        /// 绑定货币(货币名称 -- 汇率ID )列表
        /// </summary>
        /// <param name="list">要添加货币的控件</param>		
        public static void BindCurrency_IDList(DropDownList list, string Default_Currency)
        {
            SqlDataReader dr = DBHelper.ExecuteReader("Select ID,name From currency where bzflag=1 order by id");
            while (dr.Read())
            {
                ListItem list2 = new ListItem(dr["name"].ToString(), dr["id"].ToString());
                //				if(Default_Currency.Trim()=="" && dr["name"].ToString()=="美元")
                //					list2.Selected=true;
                if (Default_Currency == dr["id"].ToString())
                    list2.Selected = true;
                list.Items.Add(list2);
            }
            dr.Close();
        }
        /// <summary>
        /// 绑定货币(国家名称 -- 汇率)列表
        /// </summary>
        /// <param name="list">要添加期数的控件</param>		
        public static void BindCountry_RateList(DropDownList list, string Default_Country)
        {
            SqlDataReader dr = DBHelper.ExecuteReader("Select cu.rate,co.name From currency cu,country co where co.rate_id=cu.id order by co.name");
            while (dr.Read())
            {
                ListItem list2 = new ListItem(dr["name"].ToString(), dr["rate"].ToString());
                if (Default_Country == dr["name"].ToString())
                    list2.Selected = true;
                list.Items.Add(list2);
            }
            dr.Close();

        }
        
        public static void BindCountry_RateList(DropDownList list)
        {
            BindCountry_RateList(list, "Chinese");
        }

        /// <summary>
        /// 获取标准币种
        /// </summary>
        /// <returns></returns>
        public static int GetStandard()
        {
            string sql = "Select ID From currency where StandardMoney=id";
            int ret = (int)DBHelper.ExecuteScalar(sql, CommandType.Text);
            return ret;

        }
        /// <summary>
        /// 获取会员币种id
        /// </summary>
        /// <returns></returns>
        public static int GetMember(string number)
        {
            string sql = "select Currency from memberinfo where Number=@number";
            int ret =0;
            SqlParameter[] sp = { new SqlParameter("@number",number)};
            ret =(int)DBHelper.ExecuteScalar(sql, sp, CommandType.Text);
            return ret;
        }
        public static int Getstore(string number)
        {
            string sql = "select Currency from StoreInfo where StoreID=@number";
            int ret = 0;
            SqlParameter[] sp = { new SqlParameter("@number", number) };
            ret = (int)DBHelper.ExecuteScalar(sql, sp, CommandType.Text);
            return ret;
        }

        /// <summary>
        /// 获取币种名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetCurrencyName(int id)
        {
            string sql = "select isnull(name,'') as name from currency where id=@id";
            string ret = "";
            SqlParameter[] sp = { new SqlParameter("@id", id) };
            ret = DBHelper.ExecuteScalar(sql, sp, CommandType.Text).ToString();
            return ret;

        }
        /// <summary>
        /// 修改会员币种
        /// </summary>
        /// <param name="id">修改的币种</param>
        /// <param name="number">会员账号</param>
        /// <returns>用户币种改变的行数</returns>
        public static int UpdateCurrencyName(string name, string number)
        {
            string sql = "update memberinfo set Currency=@name where number=@number";
            int ret = 0;
            SqlParameter[] sp = { new SqlParameter("@name", name), new SqlParameter("@number", number) };           
            ret = DBHelper.ExecuteNonQuery(sql, sp, CommandType.Text);
            return ret;
        }
        /// <summary>
        /// 绑定货币(国家名称 -- 币种ID )列表
        /// </summary>
        /// <param name="list">要添加期数的控件</param>		
        public static void BindCountry_IDList(DropDownList list, string Default_Country)
        {
            SqlDataReader dr = DBHelper.ExecuteReader("Select co.ID,co.name From Currency cu,Country co where co.rateid=cu.id order by co.id");
            while (dr.Read())
            {
                ListItem list2 = new ListItem(dr["name"].ToString(), dr["id"].ToString());
                if (Default_Country == dr["name"].ToString())
                    list2.Selected = true;
                list.Items.Add(list2);
            }
            dr.Close();
        }
        /// <summary>
        /// 绑定货币(国家名称 -- 国家ID )列表
        /// </summary>
        /// <param name="list">要添加期数的控件</param>		
        public static void BindCountry_List(DropDownList list, string Default_Country)
        {
            SqlDataReader dr = DBHelper.ExecuteReader("Select id,name From Country order by id  ");
            while (dr.Read())
            {
                ListItem list2 = new ListItem(dr["name"].ToString(), dr["id"].ToString());
                if (Default_Country == dr["name"].ToString())
                    list2.Selected = true;
                list.Items.Add(list2);
            }
            dr.Close();
        }

        #region 根据订单编号获取币种名称
        /// <summary>
        ///  根据订单编号获取币种名称
        /// </summary>
        /// <param name="OrderID">订单编号编号</param>		
        public static string getOrderID_Currency(string OrderID)
        {
            string sqlStr = "";
            if (System.Web.HttpContext.Current.Session["language"].ToString().ToLower() == "chinese" || System.Web.HttpContext.Current.Session["language"].ToString().ToUpper() == "中文")
            {
                sqlStr = "Select currency.name from MemberOrder,currency where MemberOrder.orderID='" + OrderID + "' and MemberOrder.PayCurrency=currency.id";
            }
            else
            {
                int ID = Convert.ToInt32(DBHelper.ExecuteScalar("select id from language where name='" + System.Web.HttpContext.Current.Session["language"].ToString() + "'"));
                sqlStr = @"Select (select languagename from LanguageTrans where 
						LanguageTrans.OldID=currency.id and LanguageTrans.Columnsname='name' and LanguageID =" + ID + @") as
						name from MemberOrder,currency where MemberOrder.orderID='" + OrderID + @"' and 
						 MemberOrder.PayCurrency=currency.id";
            }

            string currency = "";
            currency = DBHelper.ExecuteScalar(sqlStr).ToString();
            return currency;
        }
        #endregion
        #region 通用分页数据读取函数
        /// <summary>
        /// 通用分页数据读取函数 
        /// </summary>
        /// <param name="_tblName">表名，可以是多个表，但不能用别名</param>
        /// <param name="_strKey">主键，可以为空，但@Order为空时该值不能为空</param>
        /// <param name="_fldName">要取出的字段，可以是多个表的字段，可以为空，为空表示select *</param>
        /// <param name="_pageSize">每页记录数</param>
        /// <param name="_page">当前页，0表示第1页</param>
        /// <param name="_strWhere">条件，可以为空，不用填 where</param>
        /// <param name="_group">分组依据，可以为空，不用填 group by</param>
        /// <param name="_fldSort">排序，可以为空，为空默认按主键升序排列，不用填 order by</param>
        /// <param name="_PageCount">返回多少页</param>
        /// <param name="_RecordCount">返回多少条记录</param>
        /// <returns></returns>
        public static DataTable GetDataPage_DataTable(string _tblName, string _strKey, string _fldName, int _pageSize, int _page, string _strWhere, string _group, string _fldSort, out int _RecordCount, out int _PageCount)
        {

            _RecordCount = 0;
            _PageCount = 0;
            SqlParameter[] parm0 = {   new SqlParameter("@TableNames",SqlDbType.VarChar,2000),
                                       new SqlParameter("@PrimaryKey",SqlDbType.VarChar,100),
                                       new SqlParameter("@Fields",SqlDbType.VarChar,2000),
                                       new SqlParameter("@PageSize",SqlDbType.Int),
                                       new SqlParameter("@CurrentPage",SqlDbType.Int),
                                       new SqlParameter("@Filter",SqlDbType.VarChar,2000),
                                       new SqlParameter("@Group",SqlDbType.VarChar,200),
                                       new SqlParameter("@Order",SqlDbType.VarChar,200),
                                       new SqlParameter("@RecordCount",SqlDbType.Int),
                                       new SqlParameter("@PageCount",SqlDbType.Int)
                                   };

            parm0[0].Value = _tblName;
            parm0[1].Value = _strKey;
            parm0[2].Value = _fldName;
            parm0[3].Value = _pageSize;
            parm0[4].Value = _page;
            parm0[5].Value = _strWhere;
            parm0[6].Value = _group;
            parm0[7].Value = _fldSort;
            parm0[8].Value = _RecordCount;
            parm0[9].Value = _PageCount;

            parm0[8].Direction = System.Data.ParameterDirection.Output;
            parm0[9].Direction = System.Data.ParameterDirection.Output;


            DataTable dt = DBHelper.ExecuteDataTable("DataPage", parm0, CommandType.StoredProcedure);
            _RecordCount = Convert.ToInt32(parm0[8].Value);
            _PageCount = Convert.ToInt32(parm0[9].Value);

            return dt;

        }
        #endregion
        public static void BindCountry_IDList(DropDownList list)
        {
            BindCountry_IDList(list, "Chinese");
        }

        /// <summary>
        /// 得到期数的显示状态( 0:表示按期数显示，1：表示按日期显示)
        /// </summary>
        public static int GetShowQishuDate()
        {
            return DataDictionaryDAL.GetFileValueByCode();
        }

        /// <summary>
        /// 绑定银行(银行名称-银行id)列表  ---DS2012
        /// </summary>
        /// <param name="list">要绑定的控件</param>	
        public static void BindCountry_Bank(string GuojiaName, DropDownList list)
        {
            list.Items.Clear();
            SqlDataReader dr = DBHelper.ExecuteReader("Select a.bankname,a.BankID,a.BankCode From MemberBank a,Country b where a.countrycode=b.id and b.name='" + GuojiaName + "' and bankcode<>'000000'");
            ///list.Items.Add("请选择开户行");
            list.Items.Add(new ListItem("请选择开户行", "000000"));
            while (dr.Read())
            {
                ListItem list2 = new ListItem(dr["bankname"].ToString(), dr["BankCode"].ToString());
                
                list.Items.Add(list2);
            }
            dr.Close();
        }
        /// <summary>
        /// 绑定银行(银行名称-银行id)列表
        /// </summary>
        /// <param name="list">要绑定的控件</param>	
        public static DataTable BindCountry_Bank(string CountryCode)
        {
            DataTable table = new DataTable();
            table.Columns.Add("bankname");
            table.Columns.Add("BankCode");
            SqlDataReader dr1 = DBHelper.ExecuteReader("Select bankname,BankCode from memberbank where BankCode='000000'");
            while (dr1.Read())
            {
                DataRow row = table.NewRow();
                row["bankname"] = dr1["bankname"].ToString();
                row["BankCode"] = dr1["BankCode"].ToString();
                table.Rows.Add(row);
            }
            dr1.Close();
            SqlDataReader dr = DBHelper.ExecuteReader("Select a.bankname,a.BankID,a.BankCode From MemberBank a,Country b where a.countrycode=b.id and b.name='" + CountryCode + "' and bankcode<>'000000'");
            while (dr.Read())
            {
                DataRow row = table.NewRow();
                row["bankname"] = dr["bankname"].ToString();
                row["BankCode"] = dr["BankCode"].ToString();
                table.Rows.Add(row);
            }
            dr.Close();
            return table;
        }

        /// <summary>
        ///  根据产品编号获取币种名称
        /// </summary>
        /// <param name="产品编号">产品编号</param>	

        public static string getP_CurrencyName(string productid)
        {
            string CurrencyName = "";
            if (productid != "" && productid != null)
            {
                CurrencyName = Convert.ToString(DBHelper.ExecuteScalar("select a.name from m_currency a,m_country b,product c where a.id=b.Rate_id and b.id=c.country and c.productid='" + productid + "'"));
            }

            return CurrencyName;
        }

        #region 根据会员编号获取币种名称
        /// <summary>
        ///  根据会员编号获取币种名称 
        /// </summary>
        /// <param name="bianhao">会员编号</param>		
        public static string getH_Currency(string bianhao)
        {
            string sqlStr = "Select m_currency.Name from h_info,d_info,m_currency where h_info.bianhao='" + bianhao + "' and h_info.storeid=d_info.storeid and d_info.currency=m_currency.ID";
            string currency = "";
            object obj = DBHelper.ExecuteScalar(sqlStr);
            if (obj != null)
                currency = obj.ToString();
            return currency;
        }
        #endregion

        #region 根据店铺编号获取币种名称
        /// <summary>
        ///  根据店铺编号获取币种名称
        /// </summary>
        /// <param name="storeID">店铺编号</param>		
        public static string getD_Currency(string storeID)
        {
            string sqlStr = "";
            if (System.Web.HttpContext.Current.Session["language"].ToString().ToLower() == "chinese" || System.Web.HttpContext.Current.Session["language"].ToString().ToUpper() == "中文")
            {
                sqlStr = "Select m_currency.name from d_info,m_currency where d_info.storeid='" + storeID + "' and d_info.currency=m_currency.id";
            }
            else
            {
                int ID = Convert.ToInt32(DBHelper.ExecuteScalar("select id from m_language where name='" + System.Web.HttpContext.Current.Session["language"].ToString() + "'"));
                sqlStr = @"Select (select languagename from languageTolanguage where 
						languageTolanguage.yuanid=m_currency.id and languageTolanguage.Columnsname='name' and languageid=" + ID + @") as
						name from d_info,m_currency where d_info.storeid=(select top 1 storeid From storeinfo where defaultstore=1) and
						d_info.currency=m_currency.id";
            }

            string currency = "";
            currency = DBHelper.ExecuteScalar(sqlStr).ToString();
            return currency;
        }
        #endregion
        #region 根据店铺编号获取币种id
        /// <summary>
        ///  根据店铺编号获取币种id
        /// </summary>
        /// <param name="storeID">店铺编号</param>		
        public static string getD_Currencyid(string storeID)
        {
            string sqlStr = "Select m_currency.id from d_info,m_currency where d_info.storeid='" + storeID + "' and d_info.currency=m_currency.id";
            string currency = "";
            currency = DBHelper.ExecuteScalar(sqlStr).ToString();
            return currency;
        }
        #endregion

        #region 根据会员编号获取汇率
        /// <summary>
        ///  根据会员编号获取汇率名称 
        /// </summary>
        /// <param name="bianhao">会员编号</param>		
        public static string getH_rate(string bianhao)
        {
            string sqlStr = "Select m_currency.rate from h_info,d_info,m_currency where h_info.bianhao='" + bianhao + "' and h_info.storeid=d_info.storeid and d_info.currency=m_currency.ID";
            string rate = "";
            rate = DBHelper.ExecuteScalar(sqlStr).ToString();
            return rate;
        }
        #endregion

        #region 根据店铺编号获取获取汇率
        /// <summary>
        ///  根据店铺编号获取获取汇率
        /// </summary>
        /// <param name="storeID">店铺编号</param>		
        public static string getD_rate(string storeID)
        {
            string sqlStr = "Select Currency.rate from StoreInfo,Currency where StoreInfo.storeid='" + storeID + "' and StoreInfo.currency=Currency.ID";
            string rate = "";
            rate = DBHelper.ExecuteScalar(sqlStr).ToString();
            return rate;
        }
        #endregion


        /// <summary>
        /// 会员级别绑定
        /// </summary>
        /// <param name="List">控件</param>
        /// <param name="needAll">是否需要全部选项</param>
        public static void BindMemberJibie(DropDownList List, bool needAll)
        {
            List.Items.Clear();
            string languageCode = System.Web.HttpContext.Current.Session["languageCode"].ToString();
            string ExSql = "select bl.levelint,case when t." + languageCode + " is null then  bl.levelStr else t." + languageCode + " end as LeverName "
                + " from bsco_level bl left outer join T_translation t on bl.id=t.primarykey and t.tableName='bsco_level' "
                + " where bl.levelflag=0 order by bl.levelint ";
            DataTable dt = DAL.DBHelper.ExecuteDataTable(ExSql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                List.Items.Add(new ListItem(dt.Rows[i]["LeverName"].ToString(), dt.Rows[i]["levelint"].ToString()));
            }
            if (needAll)
            {
                List.Items.Add(new ListItem(new TranslationBase().GetTran("000633", "全部"), "-1"));
            }
        }

        /// <summary>
        /// 店铺级别绑定
        /// </summary>
        /// <param name="List">控件</param>
        /// <param name="needAll">是否需要全部选项</param>
        public static void BindStoreJibie(DropDownList List, bool needAll)
        {
            List.Items.Clear();
            string languageCode = System.Web.HttpContext.Current.Session["languageCode"].ToString();
            string ExSql = "select bl.levelint,case when t." + languageCode + " is null then  bl.levelStr else t." + languageCode + " end as LeverName "
                + " from bsco_level bl left outer join T_translation t on bl.id=t.primarykey and t.tableName='bsco_level' "
                + " where bl.levelflag=1 order by bl.levelint ";
            DataTable dt = DAL.DBHelper.ExecuteDataTable(ExSql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                List.Items.Add(new ListItem(dt.Rows[i]["LeverName"].ToString(), dt.Rows[i]["levelint"].ToString()));
            }
            if (needAll)
            {
                List.Items.Add(new ListItem(new TranslationBase().GetTran("000633", "全部"), "-1"));
            }
        }



        /// <summary>
        /// 绑定期数列表 IsSuance ---ds2012---www-b874dce8700
        /// </summary>
        /// <param name="list">要添加期数的控件</param>
        /// <param name="needQuanBu">是否需要全部选项</param>
        public static void BindQishuList(DropDownList list, bool needQuanBu)
        {
            needQuanBu = true;
            BindQishuList(list, needQuanBu, CommonDataDAL.getMaxqishu());

        }

        /// <summary>
        /// 绑定期数列表  ---ds2012---www-b874dce8700
        /// </summary>
        /// <param name="list">要添加期数的控件</param>
        /// <param name="needQuanBu">是否需要全部选项</param>
        public static void BindQishuList(DropDownList list, bool needQuanBu, bool IsSuance)
        {
            needQuanBu = true;
            BindQishuList(list, needQuanBu, CommonDataDAL.getMaxqishu(), IsSuance);

        }



        /// <summary>
        /// 绑定期数列表  ---ds2012---www-b874dce8700
        /// </summary>
        /// <param name="list">要添加期数的控件</param>
        /// <param name="needQuanBu">是否需要全部选项</param>
        /// <param name="selectQishu">默认选中的期数</param>
        public static void BindQishuList(DropDownList list, bool needQuanBu, int selectQishu)
        {
          
            int maxqishu = CommonDataDAL.getMaxqishu();

            int showQISHUorRIQI = CommonDataDAL.GetShowQishuDate();// 0:表示按期数显示，1：表示按日期显示

            SqlDataReader dr = DBHelper.ExecuteReader("SELECT ExpectNum,Convert(char(10),[Date],120) as [Date],stardate,enddate FROM CONFIG ORDER BY ExpectNum ");
            //循环遍历，添加期数选项
            while (dr.Read())
            {
                if (showQISHUorRIQI == 0)
                {

                    list.Items.Add(new ListItem(new TranslationBase().GetTran("000156", "第") + " " + dr["ExpectNum"].ToString() + " " + new TranslationBase().GetTran("000157", "期"), dr["ExpectNum"].ToString()));
                }
                else if (showQISHUorRIQI == 1)
                {
                    list.Items.Add(new ListItem(dr["Date"].ToString(), dr["ExpectNum"].ToString()));

                }
                else
                {
                    list.Items.Add(new ListItem(new TranslationBase().GetTran("000156", "第") + " " + dr["ExpectNum"].ToString() + " " + new TranslationBase().GetTran("000157", "期") + " (" + dr["stardate"].ToString() + "" + new TranslationBase().GetTran("000068", "至") + "" + dr["enddate"].ToString() + ")", dr["ExpectNum"].ToString()));
                }
            }
            dr.Close();
            dr.Dispose();

            if (needQuanBu)
            {
                list.Items.Add(new ListItem(new TranslationBase().GetTran("000633", "全部"), "-1"));
            }

            list.SelectedIndex = -1;
            ListItem item = list.Items.FindByValue(selectQishu.ToString());
            if (item != null)
            {
                item.Selected = true;
            }
        }

        public static void BindQishuListB(int regQishu, DropDownList list, bool needQuanBu)
        {
            BindQishuListB(regQishu, list, needQuanBu, CommonDataDAL.getMaxqishu());
        }


        /// <summary>
        /// 绑定期数列表 
        /// </summary>
        /// <param name="list">要添加期数的控件</param>
        /// <param name="needQuanBu">是否需要全部选项</param>
        /// <param name="selectQishu">默认选中的期数</param>
        public static void BindQishuListB(int regQishu, DropDownList list, bool needQuanBu, int selectQishu)
        {
            int maxqishu = CommonDataDAL.getMaxqishu();

            int showQISHUorRIQI = CommonDataDAL.GetShowQishuDate();// 0:表示按期数显示，1：表示按日期显示

            string sql = "SELECT ExpectNum,Convert(char(10),[Date],120) as [Date],stardate,enddate FROM CONFIG Where ExpectNum>=@RegQishu ORDER BY ExpectNum ";
            SqlParameter[] para = {
                                      new SqlParameter("@RegQishu",SqlDbType.Int)
                                  };
            para[0].Value = regQishu;
            SqlDataReader dr = DBHelper.ExecuteReader(sql, para, CommandType.Text);
            //循环遍历，添加期数选项
            while (dr.Read())
            {
                if (showQISHUorRIQI == 0)
                {

                    list.Items.Add(new ListItem(new TranslationBase().GetTran("000156", "第") + " " + dr["ExpectNum"].ToString() + " " + new TranslationBase().GetTran("000157", "期"), dr["ExpectNum"].ToString()));
                }
                else if (showQISHUorRIQI == 1)
                {
                    list.Items.Add(new ListItem(dr["Date"].ToString(), dr["ExpectNum"].ToString()));

                }
                else
                {
                    list.Items.Add(new ListItem(new TranslationBase().GetTran("000156", "第") + " " + dr["ExpectNum"].ToString() + " " + new TranslationBase().GetTran("000157", "期") + " (" + dr["stardate"].ToString() + "" + new TranslationBase().GetTran("000068", "至") + "" + dr["enddate"].ToString() + ")", dr["ExpectNum"].ToString()));
                }
            }
            dr.Close();

            if (needQuanBu)
            {
                list.Items.Add(new ListItem(new TranslationBase().GetTran("000633", "全部"), "-1"));
            }

            list.SelectedIndex = -1;
            ListItem item = list.Items.FindByValue(selectQishu.ToString());
            if (item != null)
            {
                item.Selected = true;
            }
        }

        /// <summary>
        /// 绑定期数列表 
        /// </summary>
        /// <param name="list">要添加期数的控件</param>
        /// <param name="needQuanBu">是否需要全部选项</param>
        /// <param name="selectQishu">默认选中的期数</param>
        public static void BindQishuListNew(DropDownList list)
        {
            int maxqishu = CommonDataDAL.getMaxqishu();

            int showQISHUorRIQI = CommonDataDAL.GetShowQishuDate();// 0:表示按期数显示，1：表示按日期显示

            SqlDataReader dr = DBHelper.ExecuteReader("SELECT ExpectNum,Convert(char(10),[Date],120) as [Date] FROM CONFIG Where ExpectNum<>" + maxqishu + " ORDER BY ExpectNum ");
            //循环遍历，添加期数选项
            while (dr.Read())
            {
                if (showQISHUorRIQI == 0)
                {

                    list.Items.Add(new ListItem(new TranslationBase().GetTran("000156", "第") + " " + dr["ExpectNum"].ToString() + " " + new TranslationBase().GetTran("000157", "期"), dr["ExpectNum"].ToString()));
                }
                else
                {
                    list.Items.Add(new ListItem(dr["Date"].ToString(), dr["ExpectNum"].ToString()));

                }
            }
            dr.Close();

            list.Items.Add(new ListItem(new TranslationBase().GetTran("000633", "全部"), "-1"));

            list.SelectedIndex = -1;
            ListItem item = list.Items.FindByValue((maxqishu - 1).ToString());
            if (item != null)
            {
                item.Selected = true;
            }
        }


        /// <summary>
        /// 绑定期数列表 
        /// </summary>
        /// <param name="list">要添加期数的控件</param>
        /// <param name="needQuanBu">是否需要全部选项</param>
        /// <param name="selectQishu">默认选中的期数</param>
        public static void BindQishuList(DropDownList list, bool needQuanBu, int selectQishu, bool IsSuance)
        {
            int maxqishu = CommonDataDAL.getMaxqishu();

            int showQISHUorRIQI = CommonDataDAL.GetShowQishuDate();// 0:表示按期数显示，1：表示按日期显示
            string ExSql = string.Empty;
            if (IsSuance)
                ExSql = "SELECT ExpectNum,Convert(char(10),[Date],120) as [Date] FROM CONFIG where IsSuance=1  ORDER BY ExpectNum ";
            else
                ExSql = "SELECT ExpectNum,Convert(char(10),[Date],120) as [Date] FROM CONFIG where IsSuance!=1  ORDER BY ExpectNum ";
            SqlDataReader dr = DBHelper.ExecuteReader(ExSql);
            //循环遍历，添加期数选项
            while (dr.Read())
            {
                if (showQISHUorRIQI == 0)
                {

                    list.Items.Add(new ListItem(new TranslationBase().GetTran("000156", "第") + " " + dr["ExpectNum"].ToString() + " " + new TranslationBase().GetTran("000157", "期"), dr["ExpectNum"].ToString()));
                }
                else
                {
                    list.Items.Add(new ListItem(dr["Date"].ToString(), dr["ExpectNum"].ToString()));

                }
            }
            dr.Close();

            if (needQuanBu)
            {
                list.Items.Add(new ListItem(new TranslationBase().GetTran("000633", "全部"), "-1"));
            }

            list.SelectedIndex = -1;
            ListItem item = list.Items.FindByValue(selectQishu.ToString());
            if (item != null)
            {
                item.Selected = true;
            }
        }

        /// <summary>
        /// 绑定管理员可查看的网络
        /// </summary>
        /// <param name="list">要添加的控件</param>        
        /// <param name="manageID">管理员编号</param>
        public static void GetViewManage(DropDownList list, string manageID, bool isAnzhi)
        {
            int type = 0;
            if (isAnzhi)
            {
                type = 1;
            }
            string strSql = "SELECT id,number FROM ViewManage where manageID = @manageID and type = @type ORDER BY id";
            SqlParameter[] para = {
                                      new SqlParameter("@manageID",SqlDbType.NVarChar,20),
                                      new SqlParameter("@type",SqlDbType.Int)
                                  };
            para[0].Value = manageID;
            para[1].Value = type;
            SqlDataReader dr = DBHelper.ExecuteReader(strSql, para, CommandType.Text);
            //循环遍历，添加期数选项
            while (dr.Read())
            {
                list.Items.Add(new ListItem(dr["number"].ToString(), dr["id"].ToString()));
            }
            dr.Close();
        }

        /// <summary>
        /// 绑定管理员可查看的网络
        /// </summary>
        /// <param name="list">要添加的控件</param>        
        /// <param name="manageID">管理员编号</param>
        public static bool GetViewManage1(string manageID, bool isAnzhi, string number)
        {
            int type = 0;
            if (isAnzhi)
            {
                type = 1;
            }
            string strSql = "SELECT count(0) FROM ViewManage where manageID = @manageID and type = @type and number=@number";
            SqlParameter[] para = {
                                      new SqlParameter("@manageID",SqlDbType.NVarChar,20),
                                      new SqlParameter("@type",SqlDbType.Int),
                                      new SqlParameter("@number",SqlDbType.NVarChar,30)
                                  };
            para[0].Value = manageID;
            para[1].Value = type;
            para[2].Value = number;

            int count = (int)DBHelper.ExecuteScalar(strSql, para, CommandType.Text);
            if (count == 0)
                return false;
            return true;
        }


        /// <summary>
        /// 判断该管理员是否有权限查看此会员的网络图
        /// </summary>               
        /// <param name="manageID">管理员编号</param>
        /// <returns>返回时候有权限</returns>
        public static bool GetRole(string manageID, string number, bool isAnzhi)
        {
            int count = (int)DBHelper.ExecuteScalar("select count(id) from memberinfo where number='" + number + "'");
            if (count == 0)
            {
                return false;
            }
            string field = "Placement";
            int type = 1;
            if (!isAnzhi)
            {
                field = "Direct";
                type = 0;
            }
            bool isBool = false;

            string bianhao = number;
            while (true)
            {
                string strSql = "select count(id) from viewmanage where number=@number and manageID = @manageID and type =@type";
                SqlParameter[] para = {
                                       new SqlParameter("@number",SqlDbType.NVarChar,20),
                                       new SqlParameter("@manageID",SqlDbType.NVarChar,20),
                                       new SqlParameter("@type",SqlDbType.Int)
                                   };
                para[0].Value = bianhao;
                para[1].Value = manageID;
                para[2].Value = type;

                var GetNumber = (int)DBHelper.ExecuteScalar(strSql, para, CommandType.Text);

                if (GetNumber > 0)
                {
                    isBool = true;
                    break;
                }


                if (bianhao == DAL.DBHelper.ExecuteScalar("select top 1 number from manage where defaultmanager=1").ToString() || bianhao == "1111111111")
                {
                    isBool = false;
                    break;
                }


                string strSql1 = "select " + field + " from memberinfo where number = @number";
                SqlParameter[] para1 = {
                                      new SqlParameter("@number",SqlDbType.NVarChar,20)
                                  };
                para1[0].Value = bianhao;
                bianhao = DBHelper.ExecuteScalar(strSql1, para1, CommandType.Text).ToString();
            }
            return isBool;
        }
        /// <summary>
        /// 判断该管理员是否有权限查看此会员的推荐安置权限
        /// </summary>               
        /// <param name="manageID">管理员编号</param>
        /// <returns>返回时候有权限</returns>
        public static bool GetRole2(string number, bool isAnZhi)
        {
            string str = "IsViewPermissions";
            if (!isAnZhi)
            {
                str = "IsRecommended";
            }
            string sql = "select " + str + " from Manage where Number='" + number + "'";
            object obj = DBHelper.ExecuteScalar(sql);
            if (obj != null || obj != DBNull.Value)
            {
                if (Convert.ToInt32(obj) == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }
        ///<summary>
        /// 列表的数据绑定
        /// </summary>
        /// <param name="list">实现了ListControl控件</param>
        /// <param name="maxqishu">最大期</param>
        /// <param name="needQuanBu">是否显示全部</param>
        /// <param name="showMaxCount">从最大期开始最多显示几期</param>
        public static void BindQishuList(DropDownList list, int maxqishu, bool needQuanBu, int showMaxCount)
        {
            DateTime dt = DateTime.Now;

            //循环遍历，添加期数选项
            for (int i = maxqishu; i > maxqishu - showMaxCount; i--)
            {
                list.Items.Add(new ListItem(dt.Year + GetClassTran("classes/commondata.cs_135[1]", "年") + dt.Month + GetClassTran("classes/commondata.cs_135[2]", "月"), i.ToString()));
                dt = dt.AddMonths(-1);
            }

            if (needQuanBu)
            {
                list.Items.Add(new ListItem(GetClassTran("classes/commondata.cs_141", "全部"), "-1"));
            }
        }


        /// <summary>
        /// 列表的选择
        /// </summary>
        /// <param name="list">DropDownList控件</param>
        /// <param name="selectedValue">要选中的值</param>
        public static void DropDownListSelected(DropDownList list, string selectedValue)
        {
            list.SelectedIndex = -1;

            foreach (System.Web.UI.WebControls.ListItem item in list.Items)
            {
                if (item.Value == selectedValue)
                {
                    item.Selected = true;
                    return;
                }
            }
        }
        public static void DropDownListSelectedByText(DropDownList list, string selectedText)
        {
            list.SelectedIndex = -1;

            foreach (System.Web.UI.WebControls.ListItem item in list.Items)
            {
                if (item.Text.Trim() == selectedText.Trim())
                {
                    item.Selected = true;
                    return;
                }
            }
        }
        /// <summary>
        /// 列表的选择
        /// </summary>
        /// <param name="list">RadioButtonList控件</param>
        /// <param name="selectedValue">要选中的值</param>
        public static void RadioButtonListSelected(RadioButtonList list, string selectedValue)
        {
            list.SelectedIndex = -1;

            foreach (System.Web.UI.WebControls.ListItem item in list.Items)
            {
                if (item.Value == selectedValue)
                {
                    item.Selected = true;
                    return;
                }
            }
        }
        public static void RadioButtonListSelectedByText(RadioButtonList list, string selectedText)
        {
            list.SelectedIndex = -1;

            foreach (System.Web.UI.WebControls.ListItem item in list.Items)
            {
                if (item.Text.Trim() == selectedText.Trim())
                {
                    item.Selected = true;
                    return;
                }
            }
        }
        /// <summary>
        /// 列表的选择
        /// </summary>
        /// <param name="list">实现了ListControl控件</param>
        /// <param name="selectedValue">要选中的值</param>
        public static void ListControlSelected(System.Web.UI.WebControls.ListControl list, string selectedValue)
        {
            list.SelectedIndex = -1;

            foreach (System.Web.UI.WebControls.ListItem item in list.Items)
            {
                if (item.Value == selectedValue)
                {
                    item.Selected = true;
                    return;
                }
            }
        }
        public static void ListControlSelectedByText(System.Web.UI.WebControls.ListControl list, string selectedText)
        {
            list.SelectedIndex = -1;

            foreach (System.Web.UI.WebControls.ListItem item in list.Items)
            {
                if (item.Text.Trim() == selectedText.Trim())
                {
                    item.Selected = true;
                    return;
                }
            }
        }
        /// <summary>
        /// 验证生份证号码是否与输入信息相符
        /// </summary>
        /// <param name="cid">身份证号码</param>
        /// <param name="country" >国家</param>
        /// <param name="zone">省市</param>
        /// <param name="birthday">出生年月</param>
        /// <param name="sex">性别</param>
        /// <returns>结果信息,验证通过返回1</returns>
        public static string CheckCidInfo(string cid, string country, string zone, string birthday, string sex)
        {
            if (cid.Length == 15)
            {
                cid = Card15To18(cid);

            }
            string[] aCity = new string[] { null, null, null, null, null, null, null, null, null, null, null, "北京", "天津", "河北", "山西", "内蒙古", null, null, null, null, null, "辽宁", "吉林", "黑龙江", null, null, null, null, null, null, null, "上海", "江苏", "浙江", "安徽", "福建", "江西", "山东", null, null, null, "河南", "湖北", "湖南", "广东", "广西", "海南", null, null, null, "重庆", "四川", "贵州", "云南", "西藏", null, null, null, null, null, null, "陕西", "甘肃", "青海", "宁夏", "新疆", null, null, null, null, null, "台湾", null, null, null, null, null, null, null, null, null, "香港", "澳门", null, null, null, null, null, null, null, null, "国外" };
            double iSum = 0;


            //验证身份证号码是否是18位数字组成
            System.Text.RegularExpressions.Regex rg = new System.Text.RegularExpressions.Regex(@"^\d{17}(\d|X)$");
            System.Text.RegularExpressions.Match mc = rg.Match(cid);
            if (!mc.Success)
            {
                return GetClassTran("classes/commondata.cs_228", "身份证号码不正确，请重新输入");
            }
            cid = cid.ToLower();
            cid = cid.Replace("x", "a");

            //验证身份证号码地区部分是否正确
            if (aCity[int.Parse(cid.Substring(0, 2))] == null)
            {
                return GetClassTran("classes/commondata.cs_236", "身份证号码地区部分不正确");
            }

            //验证生份证号码是否与地区符合
            int xx = int.Parse(cid.Substring(0, 2));
            //			if(zone.Trim() != "重庆")
            //			{
            //				if(aCity[xx].Trim() != zone)
            //				{
            //					return "地区与身份证号码不符";
            //				}
            //			}

            //验证生份证号码日期部分是否正确
            try
            {
                DateTime.Parse(cid.Substring(6, 4) + "-" + cid.Substring(10, 2) + "-" + cid.Substring(12, 2));
            }
            catch
            {
                return GetClassTran("classes/commondata.cs_256", "身份证号码出生日期部分不正确");
            }

            //验证生份证号码是否与出生日期相符
            string cidNum = cid.Substring(6, 4) + "-" + cid.Substring(10, 2) + "-" + cid.Substring(12, 2);
            if (DateTime.Parse(cidNum) != DateTime.Parse(birthday))
            {
                return GetClassTran("classes/commondata.cs_263", "出生日期与身份证号不符");
            }
            if (birthday.Trim() != cidNum)
            {
                //return GetTran("classes/commondata.cs_263","出生日期与身份证号不符");
            }

            //对身份证号码进行校验
            for (int i = 17; i >= 0; i--)
            {
                iSum += (System.Math.Pow(2, i) % 11) * int.Parse(cid[17 - i].ToString(), System.Globalization.NumberStyles.HexNumber);

            }
            if (iSum % 11 != 1)
                return (GetClassTran("classes/commondata.cs_274", "非法证号"));

            //判断身份证号码是否与性别相符 1 男 0 女
            string cidSex = (int.Parse(cid.Substring(16, 1)) % 2 == 1 ? "1" : "0");
            if (cidSex.Trim() != sex)
            {
                //System.Web .HttpContext .Current .Response .Write (cidSex+"||||"+sex);
                return GetClassTran("classes/commondata.cs_281", "身份证号码与性别不符");
            }


            //验证通过后返回1
            return "1";
        }


        /// 
        /// 升级15位身份证号码至18位
        /// 
        /// 15位号码
        /// 18位号码
        public static string Card15To18(string oldidcard)
        {
            int[] wi = new int[18] { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2, 1 };
            char[] ch = new char[11] { '1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2' };
            string newidcard = String.Empty;

            // 简单的输入字符串长度判断
            if (oldidcard.Length != 15)
            {
                return "0";
            }

            newidcard = oldidcard.Insert(6, "19"); //插入"19"到原串中，生成17位串
            int aw = 0;

            // 计算 ∑(ai×wi)
            for (int i = 0; i <= 16; i++)
            {
                aw += int.Parse(newidcard[i].ToString()) * wi[i];
            }

            aw = aw % 11;
            newidcard += ch[aw]; //查表添加最后一位

            return newidcard;

        }

        /// 返回加入的新排序表达式的重复排序表达式(2006316添加)
        /// </summary>
        /// <param name="srcExpression">原重复排序表达式</param>
        /// <param name="addExpression">要加入的新排序表达式</param>
        /// <returns>加入的新排序表达式的重复排序表达式</returns>
        public static string RepeatSort(string srcExpression, string addExpression)
        {
            string CurrentSort = srcExpression.Trim();
            if (CurrentSort == "")
            {
                return addExpression + " ASC";
            }
            string[] Sorts = CurrentSort.Split(",".ToCharArray());
            bool needAdd = true;	//原排序表达式中没有，则需要新增
            System.Collections.ArrayList al = new System.Collections.ArrayList();
            for (int i = 0; i < Sorts.Length; i++)
            {
                Sorts[i] = Sorts[i].Trim();
                //排序字段名
                string Name = Sorts[i].Substring(0, Sorts[i].IndexOf(" ")).Trim();
                //排序方式
                string Method = Sorts[i].Substring(Sorts[i].LastIndexOf(" ") + 1).Trim();
                if (Name.ToLower() == addExpression.Trim().ToLower())
                {
                    needAdd = false;
                    //如果排序表达式中有要排序的字段则改变相应的排序方式
                    switch (Method.ToLower())
                    {
                        case "desc":
                            Sorts[i] = Name + " ASC";
                            break;
                        case "asc":
                        default:
                            Sorts[i] = Name + " Desc";
                            break;
                    }
                    //新增的排序表达式置于复排序表达式的最前面
                    al.Clear();
                    al.Add(Sorts[i]);
                    for (int j = 0; j < Sorts.Length; j++)
                    {
                        if (j != i)
                        {
                            al.Add(Sorts[j]);
                        }
                    }

                }
            }
            //如果al没有项，则al完全赋值为Sorts
            if (al.Count == 0)
            {
                for (int i = 0; i < Sorts.Length; i++)
                {
                    al.Add(Sorts[i]);
                }
            }
            string NewSortExpression = "";
            for (int i = 0; i < al.Count; i++)
            {
                NewSortExpression += al[i].ToString().Trim() + ",";
            }
            if (needAdd)
            {
                //默认升序排序
                NewSortExpression = addExpression + " ASC," + NewSortExpression;
            }
            //去除两边的逗号
            NewSortExpression = CommaTrim(NewSortExpression);
            return NewSortExpression;
        }
        /// <summary>
        /// 去除字符串两边的逗号
        /// </summary>
        /// <param name="src">原字符串</param>
        /// <returns>去除两边逗号的新字符串</returns>
        public static string CommaTrim(string src)
        {
            int sp, ep;
            for (sp = 0; sp <= src.Length - 1; sp++)
            {
                if (src.Substring(sp, 1) != ",")
                {
                    break;
                }
            }
            for (ep = src.Length - 1; ep >= 0; ep--)
            {
                if (src.Substring(ep, 1) != ",")
                {
                    break;
                }
            }
            ep++;
            string obj = "";
            if (ep > sp)
            {
                obj = src.Substring(sp, ep - sp);
            }
            return obj;

        }

        #region 动态取表数据
        /// <summary>
        /// 得到表的数据
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="searchSQL">where 条件</param>
        /// <returns></returns>
        public static SqlDataReader GetDrContentByDynamicSQL(string tablename, string searchSQL)
        {
            string sSQL = "Select * From " + tablename + " Where " + searchSQL;
            return DBHelper.ExecuteReader(sSQL);

        }
        /// <summary>
        /// 得到两个表的数据
        /// </summary>
        /// <param name="tablename1">表名</param>
        /// <param name="tablename2">表名</param>
        /// <param name="searchSQL">where 条件</param>
        /// <returns></returns>
        public static SqlDataReader GetDrContentByDynamicSQL(string tablename1, string tablename2, string searchSQL)
        {
            string sSQL = "Select * From " + tablename1 + "," + tablename2 + " Where " + searchSQL;
            return DBHelper.ExecuteReader(sSQL);

        }

        /// <summary>
        /// 得到表的数据
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="searchSQL">where 条件</param>
        /// <returns></returns>
        public static DataTable GetDtContentByDynamicSQL(string tablename, string searchSQL)
        {
            string sSQL = "Select * From " + tablename + " Where " + searchSQL;
            return DBHelper.ExecuteDataTable(sSQL);

        }
        /// <summary>
        /// 得到两个表的数据
        /// </summary>
        /// <param name="tablename1">表名</param>
        /// <param name="tablename2">表名</param>
        /// <param name="searchSQL">where 条件</param>
        /// <returns></returns>
        public static DataTable GetDtContentByDynamicSQL(string tablename1, string tablename2, string searchSQL)
        {
            string sSQL = "Select * From " + tablename1 + "," + tablename2 + " Where " + searchSQL;
            return DBHelper.ExecuteDataTable(sSQL);

        }

        /// <summary>
        /// 得到指定页码数据
        /// </summary>
        /// <param name="PageIndex">页码</param>
        /// <param name="PageSize">页记录数</param>
        /// <param name="table">表名</param>
        ///<param name="columns">列</param>
        /// <param name="condition">条件</param>
        /// <param name="key">关键字</param>
        /// <param name="RecordCount">总记录数</param>
        ///<param name="PageCount">页数</param>
        /// <returns></returns>
        public static DataTable GetDataTablePage(int PageIndex, int PageSize, string table, string columns, string condition, string key, out int RecordCount, out int PageCount)
        {

            RecordCount = 0;
            PageCount = 0;
            SqlParameter[] parm0 = {new SqlParameter("@PageIndex",SqlDbType.Int),
                                       new SqlParameter("@PageSize",SqlDbType.Int),
                                       new SqlParameter("@table",SqlDbType.VarChar,1000),
                                       new SqlParameter("@columns",SqlDbType.NVarChar,2000),
                                       new SqlParameter("@condition",SqlDbType.NVarChar,2000),
                                       new SqlParameter("@key",SqlDbType.VarChar,50),
                                       new SqlParameter("@RecordCount",SqlDbType.Int),
                                       new SqlParameter("@PageCount",SqlDbType.Int)
                                   };

            parm0[0].Value = PageIndex;
            parm0[1].Value = PageSize;
            parm0[2].Value = table;
            parm0[3].Value = columns;
            parm0[4].Value = condition;
            parm0[5].Value = key;
            parm0[6].Value = RecordCount;
            parm0[7].Value = PageCount;

            parm0[6].Direction = System.Data.ParameterDirection.Output;
            parm0[7].Direction = System.Data.ParameterDirection.Output;


            DataTable dt = DBHelper.ExecuteDataTable("GetCustomersDataPage", parm0, CommandType.StoredProcedure);
            RecordCount = Convert.ToInt32(parm0[6].Value);
            PageCount = Convert.ToInt32(parm0[7].Value);

            return dt;

        }
        /// <summary>
        /// 分页（按照指定的条件排序后，再进行分页）
        /// </summary>
        /// <param name="PageIndex">起始页索引</param>
        /// <param name="PageSize">每页大小</param>
        /// <param name="table">表名</param>
        /// <param name="columns">列名</param>
        /// <param name="condition">条件</param>
        /// <param name="key">排序关键字</param>
        /// <param name="ascOrDesc">按升序排还是按降序排</param>
        /// <param name="RecordCount">页数记录</param>
        /// <param name="PageCount"></param>
        /// <returns>页数总数</returns>
        public static DataTable GetCustomersDataPage_Sort(int PageIndex, int PageSize, string table, string columns, string condition, string key, bool ascOrDesc, out int RecordCount, out int PageCount)
        {
            RecordCount = 0;
            PageCount = 0;
            SqlParameter[] parm0 = {
                                       new SqlParameter("@PageIndex",SqlDbType.Int),
                                       new SqlParameter("@PageSize",SqlDbType.Int),
                                       new SqlParameter("@table",SqlDbType.VarChar,2000),
                                       new SqlParameter("@columns",SqlDbType.NVarChar,2000),
                                       new SqlParameter("@condition",SqlDbType.NVarChar,2000),
                                       new SqlParameter("@key",SqlDbType.VarChar,50),
                                       new SqlParameter("@AscOrDesc",SqlDbType.Bit),
                                       new SqlParameter("@RecordCount",SqlDbType.Int),
                                       new SqlParameter("@PageCount",SqlDbType.Int)
                                   };

            parm0[0].Value = PageIndex;
            parm0[1].Value = PageSize;
            parm0[2].Value = table;
            parm0[3].Value = columns;
            parm0[4].Value = condition;
            parm0[5].Value = key;
            parm0[6].Value = ascOrDesc;

            parm0[7].Value = RecordCount;
            parm0[8].Value = PageCount;

            parm0[7].Direction = System.Data.ParameterDirection.Output;
            parm0[8].Direction = System.Data.ParameterDirection.Output;


            DataTable dt = DBHelper.ExecuteDataTable("GetCustomersDataPage_Sort", parm0, CommandType.StoredProcedure);
            RecordCount = Convert.ToInt32(parm0[7].Value);
            PageCount = Convert.ToInt32(parm0[8].Value);

            return dt;
        }
        /// <summary>
        /// 得到指定页码数据
        /// </summary>
        /// <param name="PageIndex">页码</param>
        /// <param name="PageSize">页记录数</param>
        /// <param name="table">表名</param>
        ///<param name="columns">列</param>
        /// <param name="condition">条件</param>
        /// <param name="key">关键字</param>
        /// <param name="RecordCount">总记录数</param>
        ///<param name="PageCount">页数</param>
        /// <returns></returns>
        public static DataTable GetDataTablePage_gr(int PageIndex, int PageSize, string table, string columns, string condition, string key, out int RecordCount, out int PageCount)
        {

            RecordCount = 0;
            PageCount = 0;
            SqlParameter[] parm0 = {new SqlParameter("@PageIndex",SqlDbType.Int),
                                       new SqlParameter("@PageSize",SqlDbType.Int),
                                       new SqlParameter("@table",SqlDbType.VarChar,100),
                                       new SqlParameter("@columns",SqlDbType.NVarChar,1000),
                                       new SqlParameter("@condition",SqlDbType.NVarChar,1000),
                                       new SqlParameter("@key",SqlDbType.VarChar,50),
                                       new SqlParameter("@RecordCount",SqlDbType.Int),
                                       new SqlParameter("@PageCount",SqlDbType.Int)
                                   };

            parm0[0].Value = PageIndex;
            parm0[1].Value = PageSize;
            parm0[2].Value = table;
            parm0[3].Value = columns;
            parm0[4].Value = condition;
            parm0[5].Value = key;
            parm0[6].Value = RecordCount;
            parm0[7].Value = PageCount;

            parm0[6].Direction = System.Data.ParameterDirection.Output;
            parm0[7].Direction = System.Data.ParameterDirection.Output;


            DataTable dt = DBHelper.ExecuteDataTable("GetCustomersDataPage_gr", parm0, CommandType.StoredProcedure);
            RecordCount = Convert.ToInt32(parm0[6].Value);
            PageCount = Convert.ToInt32(parm0[7].Value);

            return dt;
        }

        /// <summary>
        /// 分页（汪华2009-10-23完成）（SQL Server 2005 高效分页）
        /// </summary>
        /// <param name="pageIndex">指定当前为第几页</param>
        /// <param name="pageSize">每页多少条记录</param>
        /// <param name="tableNames">表名</param>
        /// <param name="columnNames">列名</param>
        /// <param name="conditions">查询条件</param>
        /// <param name="orderColumnName">排序字段(支持多字段)</param>
        /// <param name="totalRecord">返回总记录数</param>
        /// <param name="totalPage">返回总页数</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable PageByWangHua(int pageIndex, int pageSize, string tableNames, string columnNames, string conditions, string orderColumnName, out int totalRecord, out int totalPage)
        {
            totalRecord = 0;
            totalPage = 0;
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@pageIndex",SqlDbType.Int),
                new SqlParameter("@pageSize",SqlDbType.Int),
                new SqlParameter("@tableNames",SqlDbType.NVarChar,2000),
                new SqlParameter("@columnNames",SqlDbType.NVarChar,4000),
                new SqlParameter("@conditions",SqlDbType.NVarChar,4000),
                new SqlParameter("@orderColumnName",SqlDbType.NVarChar,500),
                new SqlParameter("@totalRecord",SqlDbType.Int),
                new SqlParameter("@totalPage",SqlDbType.Int)
            };

            sparams[0].Value = pageIndex;
            sparams[1].Value = pageSize;
            sparams[2].Value = tableNames;
            sparams[3].Value = columnNames;
            sparams[4].Value = conditions;
            sparams[5].Value = orderColumnName;
            sparams[6].Direction = ParameterDirection.Output;
            sparams[7].Direction = ParameterDirection.Output;

            DataTable dt = DBHelper.ExecuteDataTable("PageByWangHua", sparams, CommandType.StoredProcedure);

            totalRecord = Convert.ToInt32(sparams[6].Value);
            totalPage = Convert.ToInt32(sparams[7].Value);
            return dt;
        }



        /// <summary>
        /// 查询Lanaguage表，查询语言
        /// 宋俊
        /// </summary>
        /// <returns></returns>
        public static DataTable GetLanaguage()
        {
            return CommonDataDAL.GetLanaguage();
        }

        /// <summary>
        /// 该方法报错（汪华）（2009-10-18）（注释）
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCurrency()
        {
            return CommonDataDAL.getCurrency();
            // return null;
        }
        #region 根据管理员的编号得到管理员姓名
        /// <summary>
        /// 根据管理员的编号得到管理员姓名
        /// </summary>
        /// 刘文
        /// <param name="bianhao"></param>
        /// <returns></returns>
        public static string GetNameByAdminID(string Number)
        {
            string name = string.Empty;
            string sSQL = "Select Name From Manage Where Number = '" + Number + "'";
            SqlDataReader reader = DBHelper.ExecuteReader(sSQL);
            while (reader.Read())
            {
                name = reader[0].ToString();
            }
            reader.Close();

            return (name.Length == 0 ? "管理员" : name);
        }
        #endregion
        #region 读取仓库编号的信息
        /// <summary>
        /// 读取仓库编号的信息
        /// </summary>		
        /// <returns></returns>
        public static DataTable GetWareHouseInfo()
        {
            return DBHelper.ExecuteDataTable(@"
					Select '-1' as WareHouseID  , '全部' as WareHouseName
					union
					Select WareHouseID,WareHouseName  From WareHouse  Order By WareHouseID
					");
        }
        #endregion
        /// <summary>
        /// 得到指定页码数据
        /// </summary>
        /// <param name="PageIndex">页码</param>
        /// <param name="PageSize">页记录数</param>
        /// <param name="table">表名</param>
        ///<param name="columns">列</param>
        /// <param name="condition">条件</param>
        /// <param name="key">关键字</param>
        /// <param name="RecordCount">总记录数</param>
        ///<param name="PageCount">页数</param>
        /// <returns></returns>
        public static DataTable GetDataTablePage_Sms(int PageIndex, int PageSize, string table, string columns, string condition, string key, out int RecordCount, out int PageCount)
        {

            RecordCount = 0;
            PageCount = 0;
            SqlParameter[] parm0 = {new SqlParameter("@PageIndex",SqlDbType.Int),
                                       new SqlParameter("@PageSize",SqlDbType.Int),
                                       new SqlParameter("@table",SqlDbType.VarChar,2000),
                                       new SqlParameter("@columns",SqlDbType.NVarChar,2000),
                                       new SqlParameter("@condition",SqlDbType.NVarChar,2000),
                                       new SqlParameter("@key",SqlDbType.VarChar,50),
                                       new SqlParameter("@RecordCount",SqlDbType.Int),
                                       new SqlParameter("@PageCount",SqlDbType.Int)
                                   };

            parm0[0].Value = PageIndex;
            parm0[1].Value = PageSize;
            parm0[2].Value = table;
            parm0[3].Value = columns;
            parm0[4].Value = condition;
            parm0[5].Value = key;
            parm0[6].Value = RecordCount;
            parm0[7].Value = PageCount;

            parm0[6].Direction = System.Data.ParameterDirection.Output;
            parm0[7].Direction = System.Data.ParameterDirection.Output;


            DataTable dt = DBHelper.ExecuteDataTable("GetCustomersDataPage_Sms", parm0, CommandType.StoredProcedure);
            RecordCount = Convert.ToInt32(parm0[6].Value);
            PageCount = Convert.ToInt32(parm0[7].Value);

            return dt;
        }

        /// <summary>
        /// 得到指定页码数据
        /// </summary>
        /// <param name="PageIndex">页码</param>
        /// <param name="PageSize">页记录数</param>
        /// <param name="table">表名</param>
        ///<param name="columns">列</param>
        /// <param name="condition">条件</param>
        /// <param name="key">关键字</param>
        /// <param name="RecordCount">总记录数</param>
        ///<param name="PageCount">页数</param>
        /// <returns></returns>
        public static DataTable GetDataTablePage_Smsgroup(int PageIndex, int PageSize, string table, string columns, string condition, string key, string group, out int RecordCount, out int PageCount)
        {

            RecordCount = 0;
            PageCount = 0;
            SqlParameter[] parm0 = {new SqlParameter("@PageIndex",SqlDbType.Int),
                                       new SqlParameter("@PageSize",SqlDbType.Int),
                                       new SqlParameter("@table",SqlDbType.VarChar,2000),
                                       new SqlParameter("@columns",SqlDbType.NVarChar,2000),
                                       new SqlParameter("@condition",SqlDbType.NVarChar,2000),
                                       new SqlParameter("@key",SqlDbType.VarChar,50),
                                         new SqlParameter("@group",SqlDbType.VarChar,2000),
                                       new SqlParameter("@RecordCount",SqlDbType.Int),
                                       new SqlParameter("@PageCount",SqlDbType.Int)
                                   };

            parm0[0].Value = PageIndex;
            parm0[1].Value = PageSize;
            parm0[2].Value = table;
            parm0[3].Value = columns;
            parm0[4].Value = condition;
            parm0[5].Value = key;
            parm0[6].Value = group;
            parm0[7].Value = RecordCount;
            parm0[8].Value = PageCount;

            parm0[7].Direction = System.Data.ParameterDirection.Output;
            parm0[8].Direction = System.Data.ParameterDirection.Output;


            DataTable dt = DBHelper.ExecuteDataTable("GetCustomersDataPage_Smsgroup", parm0, CommandType.StoredProcedure);
            RecordCount = Convert.ToInt32(parm0[7].Value);
            PageCount = Convert.ToInt32(parm0[8].Value);

            return dt;
        }
        public static DataTable GetDataTablePage_Sms1(int PageIndex, int PageSize, string table, string columns, string condition, string key, out int RecordCount, out int PageCount)
        {

            RecordCount = 0;
            PageCount = 0;
            SqlParameter[] parm0 = {new SqlParameter("@PageIndex",SqlDbType.Int),
                                       new SqlParameter("@PageSize",SqlDbType.Int),
                                       new SqlParameter("@table",SqlDbType.VarChar,2000),
                                       new SqlParameter("@columns",SqlDbType.NVarChar,2000),
                                       new SqlParameter("@condition",SqlDbType.NVarChar,2000),
                                       new SqlParameter("@key",SqlDbType.VarChar,50),
                                       new SqlParameter("@RecordCount",SqlDbType.Int),
                                       new SqlParameter("@PageCount",SqlDbType.Int)
                                   };

            parm0[0].Value = PageIndex;
            parm0[1].Value = PageSize;
            parm0[2].Value = table;
            parm0[3].Value = columns;
            parm0[4].Value = condition;
            parm0[5].Value = key;
            parm0[6].Value = RecordCount;
            parm0[7].Value = PageCount;

            parm0[6].Direction = System.Data.ParameterDirection.Output;
            parm0[7].Direction = System.Data.ParameterDirection.Output;


            DataTable dt = DBHelper.ExecuteDataTable("GetCustomersDataPage_gr", parm0, CommandType.StoredProcedure);
            RecordCount = Convert.ToInt32(parm0[6].Value);
            PageCount = Convert.ToInt32(parm0[7].Value);

            return dt;
        }
        public static DataTable datalist(string sql)
        {
            DataTable dt = new DataTable();
            dt = DBHelper.ExecuteDataTable(sql, CommandType.Text);
            return dt;
        }

        #region 得到产品编码
        /// <summary>
        /// 得到产品编码
        /// </summary>
        /// <returns>得到产品编码</returns>
        public static string GetProductNameCode(string productId)
        {

            string _sSQL = @"Select ProductCode  ProductName  From Product Where ProductID =" + productId;

            try
            {
                return DBHelper.ExecuteDataTable(_sSQL).Rows[0][0].ToString();
            }
            catch
            {
                return "错误";
            }

        }
        #endregion

        #region 得到产品名称
        /// <summary>
        /// 得到产品名称
        /// </summary>
        /// <returns>得到产品名称</returns>
        public static string GetProductNameByID(string productId)
        {

            string _sSQL = @"Select ProductName  ProductName  From Product Where ProductID =" + productId;

            try
            {
                return DBHelper.ExecuteDataTable(_sSQL).Rows[0][0].ToString();
            }
            catch
            {
                return "错误";
            }

        }
        #endregion
        #endregion

        /// <summary>
        /// 根据当前显示格式显示期数信息
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static System.Collections.Hashtable GetVolumeList()
        {
            System.Collections.Hashtable htb = new System.Collections.Hashtable();
            IList<ConfigModel> configs = CommonDataDAL.GetExpectNum();
            int pVal = CommonDataDAL.GetShowQishuDate();
            if (pVal == 0)
            {
                foreach (ConfigModel config in configs)
                {
                    htb.Add(new TranslationBase().GetTran("000156", "第") + " " + config.ExpectNum + " " + new TranslationBase().GetTran("000157", "期"), config.ExpectNum);
                }
            }
            else
            {
                foreach (ConfigModel config in configs)
                {
                    htb.Add(config.Date, config.ExpectNum);
                }
            }
            return htb;
        }

        /// <summary>
        /// 根据当前显示格式显示期数信息
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static IList<ConfigModel> GetVolumeLists()
        {
            IList<ConfigModel> configs = CommonDataDAL.GetExpectNum();
            int pVal = CommonDataDAL.GetShowQishuDate();
            if (pVal == 0)
            {
                foreach (ConfigModel config in configs)
                {
                    config.Date = new TranslationBase().GetTran("000156", "第") + " " + config.ExpectNum + " " + new TranslationBase().GetTran("000157", "期");
                }
            }
            else if (pVal == 2)
            {
                foreach (ConfigModel config in configs)
                {
                    config.Date = new TranslationBase().GetTran("000156", "第") + " " + config.ExpectNum + " " + new TranslationBase().GetTran("000157", "期") + " (" + config.Stardate + "" + new TranslationBase().GetTran("000068", "至") + "" + config.Enddate + ")";
                }
            }
            return configs;
        }


        /// <summary>
        /// The begin date is the previous month of current month
        /// </summary>
        /// <returns></returns>
        public static string GetDateBegin()
        {
            return DateTime.Now.AddMonths(-1).ToShortDateString();
        }

        /// <summary>
        /// The end date is the date now
        /// </summary>
        /// <returns></returns>
        public static string GetDateEnd()
        {
            return DateTime.Now.ToShortDateString();
        }

        public static string GetDate()
        {
            return DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd");
        }


        /// <summary>
        /// 更新店铺级别
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="StoreId">店编号</param>
        /// <param name="level">级别</param>
        /// <param name="levelstr">级别名称</param>
        /// <returns></returns>
        public static bool UPStoreLevel(SqlTransaction tran, string StoreId, int level)
        {
            return CommonDataDAL.UPStoreLevel(tran, StoreId, level);
        }

        /// <summary>
        /// 获得会员或店铺的级别
        /// </summary>
        /// <param name="table">表明</param>
        /// <param name="number">编号</param>
        /// <returns></returns>
        public static DataTable GetLevel(string table, string number)
        {
            return CommonDataDAL.GetLevel(table, number);
        }
        /// <summary>
        /// 获得会员或店铺的级别
        /// </summary>
        /// <param name="table">表明</param>
        /// <param name="number">编号</param>
        /// <returns></returns>
        public static string GetLevelStr(string levelint)
        {
            String language="";
            //if (str == "L001")
            //{
                language = DAL.DBHelper.ExecuteScalar("select levelstr from bsco_level where levelflag=0  and levelint=" + levelint).ToString();
            //}
            //else if (str == "L002")
            //{
            //    language = DAL.DBHelper.ExecuteScalar("select T.L002 from bsco_level,T_translation T where levelflag=0 and levelstr=T.L001 and levelint=" + levelint).ToString();
            //}
            return language;
        }

        /// <summary>
        /// 获得会员或店铺的级别
        /// </summary>
        /// <param name="table">表明</param>
        /// <param name="number">编号</param>
        /// <returns></returns>
        public static DataTable GetLevel2(string table, string number)
        {
            return CommonDataDAL.GetLevel2(table, number);
            //return null;
        }

        public static DataTable GetBalanceLevel(string number)
        {
            return CommonDataDAL.GetBalanceLevel(number);
        }

        /// <summary>
        /// 修改会员级别
        /// </summary>
        /// <param name="number">会员编号</param>
        /// <param name="level">级别</param>
        /// <param name="levelstr">级别名称</param>
        /// <returns></returns>
        public static bool UPdateMemberLevel(SqlTransaction tran, string number, int level, string levelstr)
        {
            return CommonDataDAL.UPdateMemberLevel(tran, number, level, levelstr);
        }
        /// <summary>
        /// 更新结算表中的级别
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="number">编号</param>
        /// <param name="NowLevel">级别</param>
        /// <returns></returns>
        public static bool UPJiesuanLevel(SqlTransaction tran, string number, int NowLevel)
        {
            return CommonDataDAL.UPJiesuanLevel(tran, number, NowLevel);
        }
        /// <summary>
        /// 进入级别表
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="number">编号</param>
        /// <param name="level">级别</param>
        /// <param name="volume">期数</param>
        /// <returns></returns>
        public static bool UPdateIntoLevel(SqlTransaction tran, string number, int level, int volume, DateTime inputdate)
        {
            return CommonDataDAL.UPdateIntoLevel(tran, number, level, volume, inputdate);
        }
        /// <summary>
        /// 会员手工定级--CK
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UPdateIntoLevel(GradingModel model)
        {
            return CommonDataDAL.UPdateIntoLevel(model);
        }

        #region 根据汇款ID得到备注信息
        /// <summary>
        /// 根据店铺汇款ID得到备注信息
        /// </summary>
        /// <param name="id">汇款ID</param>
        /// <returns>备注信息</returns>
        public static string GetStoreNameFromStoreID(string id)
        {
            try
            {
                return DBHelper.ExecuteDataTable("select [Remark] from Remittances where [ID]=" + id).Rows[0][0].ToString();
            }
            catch
            {
                return "不存在此店铺编号";
            }

        }

        /// <summary>
        /// 根据会员的转账ID得到备注信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetMemberECTMark(string id)
        {
            try
            {
                return DBHelper.ExecuteScalar("select isnull(remark,'') as remark from ECTransferDetail where id=" + id).ToString();
            }
            catch { return "不存在此店铺编号"; }
        }

        /// <summary>
        /// 根据会员提现申请ID得到备注信息
        /// </summary>
        /// <param name="id">会员提现申请ID</param>
        /// <returns>备注信息</returns>
        public static string GetMemberWithdraw(string id)
        {
            try
            {
                return DBHelper.ExecuteDataTable("select [Remark] from Withdraw where [ID]=" + id).Rows[0][0].ToString();
            }
            catch
            {
                return "不存在此申请信息";
            }

        }

        /// <summary>
        /// 根据会员汇款ID得到备注信息
        /// </summary>
        /// <param name="id">汇款ID</param>
        /// <returns>备注信息</returns>
        public static string GetMemberNameFromMemberID(string id)
        {
            try
            {
                return DBHelper.ExecuteDataTable("select [Remark] from Remittances where [ID]=" + id).Rows[0][0].ToString();
            }
            catch
            {
                return "不存在此会员编号";
            }

        }
        #endregion

        public static DataTable GetDataTablePage_Sms1(int PageIndex, int PageSize, string table, string columns, string condition, string key, string orderkey2, int sort, out int RecordCount, out int PageCount)
        {
            RecordCount = 0;
            PageCount = 0;
            SqlParameter[] parm0 = {new SqlParameter("@PageIndex",SqlDbType.Int),
                                       new SqlParameter("@PageSize",SqlDbType.Int),
                                       new SqlParameter("@table",SqlDbType.VarChar,2000),
                                       new SqlParameter("@columns",SqlDbType.NVarChar,2000),
                                       new SqlParameter("@condition",SqlDbType.NVarChar,2000),
                                       new SqlParameter("@key",SqlDbType.VarChar,50),
                                       new SqlParameter("@orderKey",SqlDbType.VarChar,50),
                                       new SqlParameter("@sort",SqlDbType.Int,4),
                                       new SqlParameter("@RecordCount",SqlDbType.Int),
                                       new SqlParameter("@PageCount",SqlDbType.Int)
                                   };

            parm0[0].Value = PageIndex;
            parm0[1].Value = PageSize;
            parm0[2].Value = table;
            parm0[3].Value = columns;
            parm0[4].Value = condition;
            parm0[5].Value = key;
            parm0[6].Value = orderkey2;
            parm0[7].Value = sort;

            parm0[8].Value = RecordCount;
            parm0[9].Value = PageCount;

            parm0[8].Direction = System.Data.ParameterDirection.Output;
            parm0[9].Direction = System.Data.ParameterDirection.Output;


            DataTable dt = DBHelper.ExecuteDataTable("GetCustomersDataPage_Sort_1", parm0, CommandType.StoredProcedure);
            RecordCount = Convert.ToInt32(parm0[8].Value);
            PageCount = Convert.ToInt32(parm0[9].Value);

            return dt;
        }

        #region 报表中心
        public static void setlanguage()
        {
            if (HttpContext.Current.Session["language"] == null)
            {
                HttpContext.Current.Session["language"] = DBHelper.ExecuteScalar("Select Top 1 TableName From Language ORDER BY ID").ToString().Replace("TranTo", "");
                HttpContext.Current.Session["LanguegeSelect"] = DBHelper.ExecuteScalar("Select Top 1 Name From Language ORDER BY ID").ToString();
                CommonDataBLL.LoadClassesTran();
            }
            else//add by Helen
            {
                HttpContext.Current.Session["language"] = DBHelper.ExecuteScalar("Select TableName From Language where Name='" + HttpContext.Current.Session["LanguegeSelect"].ToString() + "'").ToString().Replace("TranTo", "");
            }
        }
        public static DataTable GetStoreTotal(string begin, string end)
        {
            return DBHelper.ExecuteDataTable("select  remitnumber,amt=sum(RemitMoney) from Remittances where IsGSQR=1 and remitnumber not in ('9999999999') and [Use]!='' and ReceivablesDate>='" + begin + "' and ReceivablesDate<='" + end + "' and RemitStatus=0 group by  remitnumber ");
        }
        public static DataTable getDataByStoreid(string begin, string end)
        {
            SqlParameter[] parm ={
                                            new SqlParameter("@BeginDate",SqlDbType.DateTime),
                                            new SqlParameter("@EndDate",SqlDbType.DateTime)
                                        };
            parm[0].Value = begin;
            parm[1].Value = end;

            return DBHelper.ExecuteDataTable("D_huikuan_getDataByStoreid", parm, CommandType.StoredProcedure);
        }
        public static DataTable GetStoreProvince(string begin, string end)
        {
            return DBHelper.ExecuteDataTable("select (select province from city where city.CPCCode=b.CPCCode) as province,amt=sum(a.RemitMoney) from Remittances a ,StoreInfo b where a.remitnumber=b.storeid and a.remitnumber not in (select top 1 storeid from storeinfo where defaultstore=1) and a.isgsqr=1 and [Use]!='' and ReceivablesDate>='" + begin + "' and ReceivablesDate<='" + end + "' group by b.CPCCode");
        }
        public static DataTable getDataByCity(string begin, string end)
        {
            SqlParameter[] parm ={
                                            new SqlParameter("@BeginDate",SqlDbType.DateTime),
                                            new SqlParameter("@EndDate",SqlDbType.DateTime)


                                        };
            parm[0].Value = begin;
            parm[1].Value = end;
            return DBHelper.ExecuteDataTable("D_huikuan_getDataByCity", parm, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 服务机构汇款情况，按按国家汇总
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static DataTable getDataByCountry(string begin, string end)
        {
            SqlParameter[] parm ={
                                            new SqlParameter("@BeginDate",SqlDbType.DateTime),
                                            new SqlParameter("@EndDate",SqlDbType.DateTime)


                                        };
            parm[0].Value = begin;
            parm[1].Value = end;
            return DBHelper.ExecuteDataTable("D_huikuan_getDataByCountry", parm, CommandType.StoredProcedure);
        }
        public static DataTable RemittancesSumDetail(string number, string begin, string end)
        {
            SqlParameter[] parm ={
                                    new SqlParameter("@Storeid",SqlDbType.VarChar),
                                     new SqlParameter("@BeginDate",SqlDbType.DateTime),
                                     new SqlParameter("@EndDate",SqlDbType.DateTime)
                                };
            parm[0].Value = number;
            parm[1].Value = begin;
            parm[2].Value = end;
            return DBHelper.ExecuteDataTable("d_huikuan_SumDetail", parm, CommandType.StoredProcedure);
        }
        public static DataTable RemittancesSumDetailByDate(string begin, string end)
        {
            string sql = "select *,StoreInfo.name from Remittances,StoreInfo where StoreInfo.StoreID=Remittances.StoreID and RemittancesDate between @BeginDate and @EndDate";
            SqlParameter[] parm ={
                                     new SqlParameter("@BeginDate",SqlDbType.DateTime),
                                     new SqlParameter("@EndDate",SqlDbType.DateTime)
                                };
            parm[0].Value = begin;
            parm[1].Value = end;
            return DBHelper.ExecuteDataTable(sql, parm, CommandType.Text);
        }
        public static bool RemittancesByStoreID(string StoreID)
        {
            string sql = "select count(1) from Remittances where StoreID=@StoreID";
            SqlParameter[] parm ={
                                     new SqlParameter("@StoreID",SqlDbType.NVarChar,50)
                                };
            parm[0].Value = StoreID;
            if (Convert.ToInt32(DBHelper.ExecuteScalar(sql, parm, CommandType.Text)) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 绑定货币(货币名称 -- 汇率)列表
        /// </summary>
        /// <param name="list">要添加期数的控件</param>		
        public static void BindCurrency_Rate(DropDownList list, string laguage)
        {
            List<CurrencyModel> currencyModelList = null;
            if (laguage.ToLower() == "chinese" || laguage.ToString() == "中文")
            {
                currencyModelList = GetRateAndName();
            }
            else
            {
                currencyModelList = GetRateAndName(laguage);
            }
            foreach (CurrencyModel model in currencyModelList)
            {
                ListItem list2 = new ListItem(model.Name, model.ID.ToString());
                list.Items.Add(list2);
            }

        }
        /// <summary>
        /// 得到汇率名和汇率 
        /// </summary>
        /// <returns></returns>
        public static List<CurrencyModel> GetRateAndName()
        {
            List<CurrencyModel> list = new List<CurrencyModel>();
            CurrencyModel currencyModel = null;
            string SQL = "Select rate,name,ID From currency order by id";
            SqlDataReader reader = DBHelper.ExecuteReader(SQL);
            while (reader.Read())
            {
                currencyModel = new CurrencyModel();
                currencyModel.Name = reader["Name"].ToString();
                currencyModel.ID = Convert.ToInt32(reader["ID"]);
                list.Add(currencyModel);
            }
            reader.Close();
            return list;
        }
        /// <summary>
        /// 得到汇率名和汇率 
        /// </summary>
        /// <returns></returns>
        public static List<CurrencyModel> GetRateAndName(string language)
        {

            int ID = Convert.ToInt32(DBHelper.ExecuteScalar("select id from language where name='" + language + "'"));
            List<CurrencyModel> list = new List<CurrencyModel>();
            CurrencyModel currencyModel = null;
            string SQL = @"select ID,rate,(select languagename from LanguageTrans where 
							LanguageTrans.OldID=currency.id and 
							LanguageTrans.Columnsname='name' and languageid=@ID) as name
							from currency";

            SqlParameter[] para =
            {
              new SqlParameter("@ID",ID)
            };
            SqlDataReader reader = DBHelper.ExecuteReader(SQL, para, CommandType.Text);
            while (reader.Read())
            {
                currencyModel = new CurrencyModel();
                currencyModel.Name = reader["Name"].ToString();
                currencyModel.ID = Convert.ToInt32(reader["ID"]);
                list.Add(currencyModel);
            }
            reader.Close();
            return list;
        }
        public static string GetCityCode(string Country, string Province, string City)
        {
            string sql = string.Format("select CPCCode from City where Country='{0}' and Province='{1}' and City='{2}'", Country, Province, City);
            object obj = DBHelper.ExecuteScalar(sql);
            if (obj == null)
            {
                return "";
            }
            else
            {
                return obj.ToString();
            }
        }
        public static string GetCityCode(string Country, string Province, string City, string Xian)
        {
            string sql = string.Format("select CPCCode from City where Country='{0}' and Province='{1}' and City='{2}' and Xian='{3}'", Country, Province, City, Xian);
            object obj = DBHelper.ExecuteScalar(sql);
            if (obj == null)
            {
                return "";
            }
            else
            {
                return obj.ToString();
            }
        }
        #endregion

        public static string cut(string str)
        {
            string str1 = "";
            if (str.Length > 5)
            {
                str1 = str.Substring(0, 5) + "...";
            }
            else
            {
                str1 = str;
            }
            return str1;
        }
        /// <summary>
        /// 获取币种——ds2012——tianfeng
        /// </summary>
        /// <param name="primarykey"></param>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static string GetLanguageStr(int primarykey, string tableName, string columnName)
        {
            string languageCode = HttpContext.Current.Session["languageCode"].ToString();

            string sql = "select case when " + languageCode + " is null then c." + columnName + " else " + languageCode + " end as [name] from  " + tableName + " c left outer join T_translation t on c.id=t.primarykey and t.columnName=@columnName and t.tableName=@tableName where c.id=@primarykey";
            SqlParameter[] parm = new SqlParameter[]{
                new SqlParameter("@columnName",SqlDbType.NVarChar,50),
                new SqlParameter("@tableName",SqlDbType.NVarChar,50),
                new SqlParameter("@primarykey",SqlDbType.Int)
            };
            parm[0].Value = columnName;
            parm[1].Value = tableName;
            parm[2].Value = primarykey;
            object obj = DBHelper.ExecuteScalar(sql, parm, CommandType.Text);
            if (obj == null)
            {
                return "";
            }
            else
            {
                return obj.ToString();
            }
        }

        // <summary>
        /// 判断随即获取的编号是否已存在
        /// </summary>
        /// <returns></returns>
        public static int getNumber(string number)
        {
            return DAL.CommonDataDAL.getNumber(number);
        }

        ///<summary>
        ///随即生成编号
        ///</summary>
        ///<return>编号</return>
        public static string GetMemberNumber()
        {
            #region 方式一
            System.Random r = new Random(Convert.ToInt32(DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString()));
        loop:
            StringBuilder temp = new StringBuilder();
            int i = 0;
            while (i < 8)
            {
                int a = 0;
                while (true) { a = r.Next(0, 9); if (a != 4)break; }
                temp.Append(a);
                i++;
            }

            if (temp.ToString().IndexOf("0") == 0) { goto loop; }
            else
            {
                int flag = DAL.CommonDataDAL.getNumber(temp.ToString());
                if (flag == 0)
                {
                    return temp.ToString();
                }
                else
                {
                    goto loop;
                }
            }
            #endregion

            #region 方式二
            /*
            System.Random r = new Random();

            string number = r.Next(11111111, 99999999).ToString();

            string[] str = new string[] { "0", "1", "2", "3", "5", "6", "8", "9" };

            number = number.Replace("4", str[r.Next(0, 7)]);

            number = number.Replace("7", str[r.Next(0, 7)]);

            return number;*/

            #endregion

            #region 方式三

            //object o_number = DAL.DBHelper.ExecuteScalar("select top 1 bh from mbh where not exists(select ID from MemberInfo where Number=bh) and head=(select MIN(head) from mbh) and isdel=0 order by newid()");
            //if (o_number != null)
            //    return o_number.ToString();
            //else
            //    return "重新刷新";

            #endregion
        }
        /// <summary>
        /// 查询要导出的店铺信息（2011-11-07 ——ds2012）
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public static DataTable GetStoreQueryToExcel(string condition)
        {
            return StoreInfoDAL.GetStoreQueryToExcel(condition);
        }
        /// <summary>
        /// 获取店铺级别名称（2011-11-07 ——ds2012）
        /// </summary>
        /// <param name="storeid">店铺编号</param>
        /// <returns></returns>
        public static string GetStoreLevelStr(string storeid)
        {
            return CommonDataDAL.GetStoreLevelStr(storeid);
        }
        /// <summary>
        /// 获取店铺所属国家（2011-11-07）
        /// </summary>
        /// <param name="cpccode">国家代号</param>
        /// <returns></returns>
        public static DataTable getStoreCountry(string cpccode)
        {
            return StoreInfoDAL.GetStoreCountry(cpccode);
        }
        /// <summary>
        /// 获取标准汇率 (2010-11-07)
        /// </summary>
        /// <returns></returns>
        public static int getStandCurrency()
        {
            object objCurrency = DBHelper.ExecuteScalar("select rate from Currency where id=standardMoney");
            if (objCurrency == null || objCurrency == DBNull.Value)
            {
                return 1;
            }
            else
            {
                int currenry;
                if (int.TryParse(objCurrency.ToString(), out currenry))
                {
                    return currenry;
                }
                else
                {
                    return 1;
                }
            }
        }
        /// <summary>
        /// 获取会员密码重置信息信息(2011-11-07)
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static DataTable GetMemberPassResetToExcel(string condition)
        {
            return MemberInfoDAL.GetMemberPassResetToExcel(condition);
        }
        /// <summary>
        /// 获取国家城市信息（2011-11-07）
        /// </summary>
        /// <param name="cpccode"></param>
        /// <returns></returns>
        public static CityModel GetCPCCode(string cpccode)
        {
            return CommonDataDAL.GetCPCCode(cpccode);
        }
        /// <summary>
        /// 获取会员名单信息(2011-11-07)
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static DataTable GetMemberQueryToExcel(string condition)
        {
            return MemberInfoDAL.GetMemberQueryToExcel(condition);
        }
        ///获取银行(银行名称-银行id)列表  ---DS2012
        /// </summary>
        /// <param name="list">要绑定的控件</param> 
        public static DataTable GetCountry_Bank(string GuojiaName)
        {
            SqlParameter[] sps = new SqlParameter[] { new SqlParameter("@cname", GuojiaName) };
            DataTable dt = DBHelper.ExecuteDataTable("Select a.bankname,a.BankID,a.BankCode From MemberBank a,Country b where a.countrycode=b.id and b.name=@cname and bankcode<>'000000'", sps, CommandType.Text);
            return dt;
        }
        /// <summary>
        /// 设置会员级别
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="number">会员编号</param>
        /// <param name="orderid">订单号</param>
        /// <returns></returns>
        public static bool SetMemberLevel(SqlTransaction tran, string number, string orderid)
        {
            return MemberInfoDAL.SetMemberLevel(tran, number, orderid);
        }

        /// <summary>
        /// 进入店铺级别表
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="StoreId">店编号</param>
        /// <param name="level">级别</param>
        /// <param name="levelstr">级别名称</param>
        /// <returns></returns>
        public static bool UPIntoStoreLevel(SqlTransaction tran, string StoreId, int level, string levelstr, int volume)
        {
            return CommonDataDAL.UPIntoStoreLevel(tran, StoreId, level, levelstr, volume);
        }

        /// <summary>
        /// 根据国家ID绑定银行
        /// </summary>
        /// <param name="CountryID">国家ID</param>
        /// <param name="list">要绑定的控件</param>
        public static void BindCountryBankByCountryID(int CountryID, DropDownList list)
        {
            list.Items.Clear();
            SqlDataReader dr = DBHelper.ExecuteReader("Select a.bankname,a.BankID,a.BankCode From MemberBank a,Country b where a.countrycode=b.id and b.id=" + CountryID + " and bankcode<>'000000'");
            while (dr.Read())
            {
                ListItem list2 = new ListItem(dr["bankname"].ToString(), dr["BankCode"].ToString());
                list.Items.Add(list2);
            }
            dr.Close();
        }

        /// <summary> 
        /// 根据国家代码绑定银行 CountryCode
        /// </summary>
        /// <param name="CountryID">国家ID</param>
        /// <param name="list">要绑定的控件</param>
        public static void BindCountryBankByCountryCode(string CountryCode, DropDownList list)
        {
            list.Items.Clear();
            SqlDataReader dr = DBHelper.ExecuteReader("Select a.bankname,a.BankID,a.BankCode From MemberBank a,Country b where a.countrycode=b.id and b.CountryCode='" + CountryCode + "' and bankcode<>'000000'");
            while (dr.Read())
            {
                ListItem list2 = new ListItem(dr["bankname"].ToString(), dr["BankCode"].ToString());
                list.Items.Add(list2);
            }
            dr.Close();
        }

        /// <summary>
        /// 根据国家简称绑定银行 CountryForShort
        /// </summary>
        /// <param name="CountryID">国家ID</param>
        /// <param name="list">要绑定的控件</param>
        public static void BindCountryBankByCountryShortName(string CountryForShort, DropDownList list)
        {
            list.Items.Clear();
            SqlDataReader dr = DBHelper.ExecuteReader("Select a.bankname,a.BankID,a.BankCode From MemberBank a,Country b where a.countrycode=b.id and b.CountryForShort='" + CountryForShort + "' and bankcode<>'000000'");
            while (dr.Read())
            {
                ListItem list2 = new ListItem(dr["bankname"].ToString(), dr["BankCode"].ToString());
                list.Items.Add(list2);
            }
            dr.Close();
        }

        /// <summary>
        /// 根据银行代码，获取银行信息bankname,BankID,BankCode,CountryID,CountryCode,countryCode,CountryForShort,CountryName
        /// </summary>
        /// <param name="BankCode"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable GetCountryBankByBankCode(string BankCode)
        {
            DataTable dt = new DataTable();
            string sql = @"select 
                        bankname,BankID,BankCode,CountryID,CountryCode,countryCode,CountryForShort,CountryName
                            from V_CountryBank a where a.BrankCode='" + BankCode + "'";
            dt = DBHelper.ExecuteDataTable(sql);
            return dt;
        }
        /// <summary>
        /// 获取所有科目类型
        /// </summary>
        ///<param name="type">类型</param>
        public static DataTable GetKmtype(string type)
        {
            DataTable dt = new DataTable();
            string sql = "";
            if (type == "AccountXJ")
            {
                sql = "select * from AccountSubject where AccountXJ=1";
            }
            else if (type == "AccountXF") { sql = "select * from AccountSubject where AccountXF=1"; }
            else if (type == "AccountDH") { sql = "select * from AccountSubject where AccountDh=1"; }
            else if (type == "AccountZZ") { sql = "select * from AccountSubject where AccountZZ=1"; }
            else if (type == "AccountFX") { sql = "select * from AccountSubject where AccountXF=1"; }
            else if (type == "AccountFXth") { sql = "select * from AccountSubject where AccountXF=1"; }
            if (sql != "")
            {
                dt = DAL.DBHelper.ExecuteDataTable(sql, CommandType.Text);
            }
            return dt;
        }

    }
}