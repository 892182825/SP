using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using Model;
using System.Collections;
namespace DAL
{
    /*
    * 退货款管理
    */
    public class ReturnedGoodsMoneyDAL
    {

        /// <summary>
        /// 获取单据类型的ID
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int GetDocTypeID(string type)
        {
            string sql = "select DocTypeID from  DocTypeTable Where DocTypeName=@num";
            SqlParameter[] spa1 = new SqlParameter[] { 
                new SqlParameter("@num", SqlDbType.VarChar, 20)                              
              };
            spa1[0].Value = type;
            
            return int.Parse(DBHelper.ExecuteScalar(sql,spa1,CommandType.Text).ToString());
        }
        /// <summary>
        /// 获取币种 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static string GetMoneyType(string TyepID)
        {
            string sql = "select a.name from Currency a,Country b,Product c where a.id=b.RateID and b.id=c.CountryCode and c.ProductID=@num";
            SqlParameter[] spa1 = new SqlParameter[] { 
                new SqlParameter("@num", SqlDbType.Int)                              
              };
            spa1[0].Value = Convert.ToInt32(TyepID);
            object obj = DBHelper.ExecuteScalar(sql,spa1,CommandType.Text);
            if (obj == null)
                return "";
            else
                return obj.ToString();
        }
        /// <summary>
        /// 获取产品名称
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static string GetProductName(string productID)
        {
            string sql = "Select ProductName  ProductName  From Product Where ProductID =@num";
            SqlParameter[] spa1 = new SqlParameter[] { 
                new SqlParameter("@num", SqlDbType.Int)                              
              };
            spa1[0].Value = Convert.ToInt32(productID);
            return DBHelper.ExecuteDataTable(sql,spa1,CommandType.Text).Rows[0][0].ToString();
        }
        /// <summary>
        /// 条件查询
        /// </summary>
        public List<InventoryDocModel> GetInventoryDocModel(InventoryDocModel Inventory, string mark, int page, out int count)
        {
            // string sql = "SelectFrom  where 1=1 ";
            string table = "InventoryDoc";
            string columns = "  ID,Client,DocID,ExpectNum,StateFlag,CloseFlag,Flag,TotalMoney,Charged,TotalPV,DocMakeTime,Note ";
            string sql = "  1=1 ";
            count = 0;
            List<InventoryDocModel> list = new List<InventoryDocModel>();
            //Hashtable has = new Hashtable();

            ////国家
            //if (Inventory.Currency >= 0)
            //{
            //    sql = sql + " and Currency=" + Inventory.Currency;
            //    //has.Add("@Currency", Inventory.Currency);
            //}
            //类型
            if (Inventory.Flag >= 0)
            {
                sql = sql + " and Flag=" + Inventory.Flag;
                //has.Add("@Flag", Inventory.Flag);
            }
            //店编号
            if (Inventory.Client != null)
            {
                sql = sql + " and Client like '%" + Inventory.Client + "'";
                //has.Add("@Client", Inventory.Client);
            }
            //期数
            else if (Inventory.ExpectNum > 0 && mark != null)
            {
                sql = sql + " and ExpectNum" + mark + Inventory.ExpectNum;
                //has.Add("@ExpectNum", Inventory.ExpectNum);
            }
            //总钱
            else if (Inventory.TotalMoney > 0 && mark != null)
            {
                sql = sql + " and TotalMoney" + mark + Inventory.TotalMoney;
                //has.Add("@TotalMoney", Inventory.TotalMoney);
            }
            //时间 
            else if (Inventory.DocMakeTime != DateTime.Parse("0001-1-1 0:00:00") && mark != null)
            {

                if (mark == "=")
                {
                    string endtiem = Inventory.DocMakeTime.ToString("yyyy-MM-dd") + " 23:59:59";
                    sql = sql + " and DocMakeTime between '" + Inventory.DocMakeTime + "' and '" + endtiem + "'";
                }
                else
                {
                    sql = sql + " and DocMakeTime" + mark + "'" + Inventory.DocMakeTime + "'";
                }
                //has.Add("@DocMakeTime", Inventory.DocMakeTime);
            }
            //SqlParameter[] parameter = new SqlParameter[has.Count];
            //int i = 0;
            //foreach (DictionaryEntry num in has)
            //{
            //    parameter[i] = new SqlParameter(num.Key.ToString(), num.Value.ToString());
            //    i++;
            //} 
            SqlParameter[] parameter ={ 
                new SqlParameter("@PageIndex",SqlDbType.Int),
                 new SqlParameter("@PageSize",SqlDbType.Int),
                 new SqlParameter("@table",SqlDbType.VarChar,1000),
                 new SqlParameter("@columns",SqlDbType.VarChar,2000),
                 new SqlParameter("@condition",SqlDbType.VarChar,2000),
                 new SqlParameter("@key",SqlDbType.VarChar,50),
                 new SqlParameter("@RecordCount",SqlDbType.Int),
                 new SqlParameter("@PageCount",SqlDbType.Int)
            };
            parameter[0].Value = page;
            parameter[1].Value = 10;
            parameter[2].Value = table;
            parameter[3].Value = columns;
            parameter[4].Value = sql;
            parameter[5].Value = "id";
            parameter[6].Direction = ParameterDirection.Output;
            parameter[7].Direction = ParameterDirection.Output;
            SqlDataReader read = DBHelper.ExecuteReader("GetCustomersDataPage_Sms", parameter, CommandType.StoredProcedure);
            while (read.Read())
            {
                InventoryDocModel model = new InventoryDocModel(int.Parse(read["ID"].ToString()));
                model.DocID = read["DocID"].ToString();
                model.Client = read["Client"].ToString();
                model.ExpectNum = int.Parse(read["ExpectNum"].ToString());
                model.StateFlag = int.Parse(read["StateFlag"].ToString());
                model.CloseFlag = int.Parse(read["CloseFlag"].ToString());
                model.Flag = int.Parse(read["Flag"].ToString());
                model.TotalMoney = double.Parse(read["TotalMoney"].ToString());
                model.Charged = decimal.Parse(read["Charged"].ToString());
                model.TotalPV = double.Parse(read["TotalPV"].ToString());
                model.DocMakeTime = DateTime.Parse(read["DocMakeTime"].ToString());
                model.Note = read["Note"].ToString();
                list.Add(model);
            }
            read.Close();
            count = Convert.ToInt32(parameter[6].Value.ToString());
            return list.Count <= 0 ? null : list;
        }

        /// <summary>
        /// 退货单详细查询
        /// Select *  From InventoryDocDetails Where DocID=@cha_docID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static List<InventoryDocDetailsModel> GetInventoryDoctails(string ID)
        {
            string sql = "Select *  From InventoryDocDetails Where DocID=@DocID";
            SqlParameter[] parameter = new SqlParameter[] { 
                new SqlParameter("DocID",SqlDbType.VarChar,20)
            };
            parameter[0].Value = ID;
            List<InventoryDocDetailsModel> list = new List<InventoryDocDetailsModel>();
            SqlDataReader read = DBHelper.ExecuteReader(sql, parameter, CommandType.Text);
            while (read.Read())
            {
                InventoryDocDetailsModel model = new InventoryDocDetailsModel(int.Parse(read["docDetailsID"].ToString()));
                model.DocID = read["DocID"].ToString();
                model.ProductID = int.Parse(read["ProductID"].ToString());
                model.ProductQuantity = double.Parse(read["ProductQuantity"].ToString());
                model.ExpectNum = int.Parse(read["ExpectNum"].ToString());
                model.UnitPrice = double.Parse(read["UnitPrice"].ToString());
                model.ProductID = int.Parse(read["ProductID"].ToString());
                model.PV = double.Parse(read["PV"].ToString());
                list.Add(model);
            }
            read.Close();
            return list.Count <= 0 ? null : list;
        }
        /// <summary>
        /// 填写退货退款单 ——ds2012——tianfeng
        /// </summary>
        /// <param name="DocID">订单号</param>
        /// <param name="StoreID">店编号</param>
        /// <returns></returns>
        public static Boolean UPtInventoryDoc(string DocID, string StoreID, int flag, double money, string reason, Boolean bol,string opbh,string opip,int qishu)
        {
            SqlParameter[] parameter ={ 
                new SqlParameter("@DocID",SqlDbType.VarChar,20),
                 new SqlParameter("@StoreID",SqlDbType.VarChar,20),
                  new SqlParameter("@Flag",SqlDbType.Int),
                   new SqlParameter("@Charged",SqlDbType.Money,20),
                    new SqlParameter("@Reason",SqlDbType.VarChar,250),
                    new SqlParameter("@error",SqlDbType.VarChar,20)
            };
            parameter[0].Value = DocID;
            parameter[1].Value = StoreID;
            parameter[2].Value = flag;
            parameter[3].Value = money;
            parameter[4].Value = reason;
            parameter[5].Direction = ParameterDirection.Output;

            double totalFmoney = GetInventoryTotalMoney(DocID);

            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        //修改
                        double Fmoney = GetInventoryMoney(tran, DocID);

                        string huidan = "HK" + Model.Other.MYDateTime.ToYYMMDDHHmmssString();
                        //判断汇单号是否存在:true存在,false不存在
                        bool isExist = RemittancesDAL.isMemberExistsHuiDan(huidan);
                        while (isExist)
                        {
                            huidan = "HK" + Model.Other.MYDateTime.ToYYMMDDHHmmssString();
                            isExist = RemittancesDAL.isMemberExistsHuiDan(huidan);
                        }

                        if (bol)
                        {
                           
                            AddOrderDataDAL.AddDataTORemittances(tran, huidan, StoreID, (Fmoney * (-1)), DocID, opip, opbh, qishu);
                            DBHelper.ExecuteNonQuery(tran, "update storeinfo set totalaccountmoney=totalaccountmoney - @money where storeid=@storeid", new SqlParameter[2] { new SqlParameter("@money", Fmoney), new SqlParameter("@storeid", StoreID) }, CommandType.Text);
                            D_AccountDAL.AddStoreAccount(StoreID, Fmoney, D_AccountSftype.StoreDingHuokuan, S_Sftype.dianhuo, D_AccountKmtype.ReturnCharge, DirectionEnum.AccountReduced, "返回上次店铺退货单款" + DocID, tran);
                            
                            DBHelper.ExecuteNonQuery(tran, "UPtInventoryDoc", parameter, CommandType.StoredProcedure);
                          
                            AddOrderDataDAL.AddDataTORemittances(tran, "", StoreID, totalFmoney, DocID, opip, opbh, qishu);
                            DBHelper.ExecuteNonQuery(tran, "update storeinfo set totalaccountmoney=totalaccountmoney + @money where storeid=@storeid", new SqlParameter[2] { new SqlParameter("@money", totalFmoney), new SqlParameter("@storeid", StoreID) }, CommandType.Text);
                            D_AccountDAL.AddStoreAccount(StoreID, totalFmoney, D_AccountSftype.StoreDingHuokuan, S_Sftype.dianhuo, D_AccountKmtype.ReturnRebate, DirectionEnum.AccountsIncreased, "店铺退货单退款" + DocID, tran);

                            AddOrderDataDAL.AddDataTORemittances(tran, huidan, StoreID, (money * (-1)), DocID, opip, opbh, qishu);
                            DBHelper.ExecuteNonQuery(tran, "update storeinfo set totalaccountmoney=totalaccountmoney - @money where storeid=@storeid", new SqlParameter[2] { new SqlParameter("@money", money), new SqlParameter("@storeid", StoreID) }, CommandType.Text);
                            D_AccountDAL.AddStoreAccount(StoreID, money, D_AccountSftype.StoreDingHuokuan, S_Sftype.dianhuo, D_AccountKmtype.ReturnCharge, DirectionEnum.AccountReduced, "店铺退货单扣款" + DocID, tran);
                            
                        }
                        else
                        {
                            //添加
                            DBHelper.ExecuteNonQuery(tran, "UPtInventoryDoc", parameter, CommandType.StoredProcedure);

                            AddOrderDataDAL.AddDataTORemittances(tran, huidan, StoreID, totalFmoney, DocID, opip, opbh, qishu);
                            DBHelper.ExecuteNonQuery(tran, "update storeinfo set totalaccountmoney=totalaccountmoney + @money where storeid=@storeid", new SqlParameter[2] { new SqlParameter("@money", totalFmoney), new SqlParameter("@storeid", StoreID) }, CommandType.Text);
                            D_AccountDAL.AddStoreAccount(StoreID, totalFmoney, D_AccountSftype.StoreDingHuokuan, S_Sftype.dianhuo, D_AccountKmtype.ReturnRebate, DirectionEnum.AccountsIncreased, "店铺退货单退款" + DocID, tran);

                            AddOrderDataDAL.AddDataTORemittances(tran, huidan, StoreID, (money * (-1)), DocID, opip, opbh, qishu);
                            DBHelper.ExecuteNonQuery(tran, "update storeinfo set totalaccountmoney=totalaccountmoney - @money where storeid=@storeid", new SqlParameter[2] { new SqlParameter("@money", money), new SqlParameter("@storeid", StoreID) }, CommandType.Text);
                            D_AccountDAL.AddStoreAccount(StoreID, money, D_AccountSftype.StoreDingHuokuan, S_Sftype.dianhuo, D_AccountKmtype.ReturnRebate, DirectionEnum.AccountReduced, "店铺退货单扣款" + DocID, tran);
                            
                        }

                        tran.Commit();

                    }
                    catch (Exception ex1)
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


            return int.Parse(parameter[5].Value.ToString()) == 0 ? true : false;
        }
        /// <summary>
        /// 查看备注
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static string GetNote(string docid)
        {
            string sql = "select Note from InventoryDoc where docid=@docid";
            SqlParameter[] parameter = new SqlParameter[] { 
               
                new SqlParameter("@docid",SqlDbType.VarChar,20)
            };
            parameter[0].Value = docid;
            return DBHelper.ExecuteScalar(sql,parameter,CommandType.Text).ToString();
        }

        /// <summary>
        /// 获取退货款信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="docID"></param>
        /// <returns></returns>
        public static InventoryDocModel GetInventory(int ID, string docID)
        {
            InventoryDocModel model = null;
            string sql = "select * from inventorydoc where id=@id and docid=@docid";
            SqlParameter[] parameter = new SqlParameter[] { 
                new SqlParameter("@id",SqlDbType.Int,20),
                new SqlParameter("@docid",SqlDbType.VarChar,20)
            };
            parameter[0].Value = ID;
            parameter[1].Value = docID;
            InventoryDocDetailsModel list = new InventoryDocDetailsModel();
            SqlDataReader read = DBHelper.ExecuteReader(sql, parameter, CommandType.Text);
            while (read.Read())
            {
                model = new InventoryDocModel(int.Parse(read["ID"].ToString()));
                model.DocID = read["DocID"].ToString();
                model.Client = read["Client"].ToString();
                model.TotalMoney = double.Parse(read["TotalMoney"].ToString());
                model.Charged = decimal.Parse(read["Charged"].ToString());
                model.Reason = read["Reason"].ToString();
            }
            read.Close();
            return model;
        }

        /// <summary>
        /// 获取是否已退款 退款额
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="docID"></param>
        /// <returns></returns>
        public static double GetInventoryMoney(string docID)
        {
            double money = 0.00;
            string sql = "select TotalMoney-Charged as [money] from inventorydoc where docid=@docid";
            SqlParameter[] parameter = new SqlParameter[] { 
                new SqlParameter("@docid",SqlDbType.VarChar,20)
            };
            parameter[0].Value = docID;
            SqlDataReader read = DBHelper.ExecuteReader(sql, parameter, CommandType.Text);
            while (read.Read())
            {
                money = double.Parse(read["money"].ToString());
            }
            read.Close();
            return money;
        }
        /// <summary>
        /// 获取是否已退款 退款额——ds2012——tianfeng
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="docID"></param>
        /// <returns></returns>
        public static double GetInventoryMoney(SqlTransaction tran, string docID)
        {
            double money = 0.00;
            string sql = "select TotalMoney-Charged as [money] from inventorydoc where docid=@docid";
            SqlParameter[] parameter = new SqlParameter[] { 
                new SqlParameter("@docid",SqlDbType.VarChar,20)
            };
            parameter[0].Value = docID;
            object read = DBHelper.ExecuteScalar(tran,sql, parameter, CommandType.Text);

            money = double.Parse(read.ToString());
            return money;
        }
        /// <summary>
        /// 获取是否已退款 退款额——ds2012——tianfeng
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="docID"></param>
        /// <returns></returns>
        public static double GetInventoryTotalMoney(string docID)
        {
            double money = 0.00;
            string sql = "select TotalMoney from inventorydoc where docid=@docid";
            SqlParameter[] parameter = new SqlParameter[] { 
                new SqlParameter("@docid",SqlDbType.VarChar,20)
            };
            parameter[0].Value = docID;
            SqlDataReader read = DBHelper.ExecuteReader(sql, parameter, CommandType.Text);
            while (read.Read())
            {
                money = double.Parse(read["TotalMoney"].ToString());
            }
            read.Close();
            return money;
        }


        /// <summary>
        /// 获取退货款信息是否有效——ds2012——tianfeng
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="docID"></param>
        /// <returns></returns>
        public static Boolean GetInventoryState(string docid)
        {

            string sql = "select stateflag from inventorydoc where stateflag='1' and closeflag='0' and docid=@docid";
            SqlParameter[] parameter = new SqlParameter[] { 
                new SqlParameter("@docid",SqlDbType.VarChar,50)
            };
            parameter[0].Value = docid;
            bool bol = Convert.ToBoolean(DBHelper.ExecuteScalar(sql, parameter, CommandType.Text) != null ? true : false);
            return bol;
        }
    }
}
