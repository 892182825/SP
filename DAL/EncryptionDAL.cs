using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;
using System.Data.SqlClient;
using Encryption;

namespace DAL
{
    public class EncryptionDAL
    {
        /// <summary>
        /// 获取--加密--参数设置
        /// </summary>
        /// <returns>返回所有设置</returns>
        public static IList<EncryptionSetting> SelectAll()
        {
           DataTable dt = DBHelper.ExecuteDataTable("select * from encryptionsetting");
           IList<EncryptionSetting> list = new List<EncryptionSetting>();
           foreach (DataRow dr in dt.Rows)
           {
               EncryptionSetting es = new EncryptionSetting();
               es.Id = Convert.ToInt32(dr["id"]);
               es.EncryptionKey = Convert.ToString(dr["encryptionkey"]);
               es.EncryptionValue = Convert.ToInt32(dr["encryptionvalue"]);
               es.Remark = Convert.ToString(dr["remark"]);
               list.Add(es);
           }
           return list;
        }

        /// <summary>
        /// 获取指定的--加密--参数设置
        /// </summary>
        /// <param name="key">参数键</param>
        /// <returns>指定的参数设置状态</returns>
        public static EncryptionSetting SelectByKey(string key)
        {
            DataTable dt = DBHelper.ExecuteDataTable("select top 1 * from encryptionsetting where encryptionkey=@key", new SqlParameter[] { new SqlParameter("@key", key) }, CommandType.Text);
            EncryptionSetting es = new EncryptionSetting();
            es.Id = Convert.ToInt32(dt.Rows[0]["id"]);
            es.EncryptionKey = Convert.ToString(dt.Rows[0]["encryptionkey"]);
            es.EncryptionValue = Convert.ToInt32(dt.Rows[0]["encryptionvalue"]);
            es.Remark = Convert.ToString(dt.Rows[0]["remark"]);
            return es;
        }

        /// <summary>
        /// 更新指定--加密--参数设置
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>返回是否成功</returns>
        public static bool UpdateSetting(string key, int value)
        {
            int count = DBHelper.ExecuteNonQuery("update encryptionsetting set encryptionvalue=@value where encryptionkey=@key", new SqlParameter[] { new SqlParameter("@value", value), new SqlParameter("@key", key) }, CommandType.Text);
            if (count == 1)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 更新指定--加密--参数设置
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void UpdateSetting(SqlTransaction tran, string key, int value)
        {
            DBHelper.ExecuteNonQuery(tran, "update encryptionsetting set encryptionvalue=@value where encryptionkey=@key", new SqlParameter[] { new SqlParameter("@value", value), new SqlParameter("@key", key) }, CommandType.Text);
        }

        /// <summary>
        /// 更新所有姓名
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="type">类型：1为更新成加密，0为更新成不加密</param>
        public static void UpdateName(SqlTransaction tran,int type)
        {
            Encryption.Encryption.ValueState = "1";
            DataTable dtMemberInfo = DBHelper.ExecuteDataTable(tran, "select Number,[name],BankBook from MemberInfo");
            DataTable dtMemberOrder = DBHelper.ExecuteDataTable(tran, "select Consignee,OrderID from MemberOrder");
            DataTable dtStoreInfo = DBHelper.ExecuteDataTable(tran, "select StoreId,[Name],StoreName from StoreInfo");
            DataTable dtStoreOrder=DBHelper.ExecuteDataTable(tran,"select StoreOrderID,InceptPerson from StoreOrder");
            DataTable dtRemittances = DBHelper.ExecuteDataTable(tran, "select Sender,ID from Remittances");
            DataTable dtUnauditedStoreInfo = DBHelper.ExecuteDataTable(tran, "select StoreID,[Name],StoreName from UnauditedStoreInfo");
            DataTable dtDetailMoney = DBHelper.ExecuteDataTable(tran, "select ID,Bankbook from DetailMoney");
            DataTable dtECRemitDetail = DBHelper.ExecuteDataTable(tran, "select ID,Remitter from ECRemitDetail ");
            DataTable dtReplacement = DBHelper.ExecuteDataTable(tran, "select ID,InceptPerson from Replacement");
            DataTable dtMemberOff = DBHelper.ExecuteDataTable(tran, "select id,name from memberoff");

            if (type == 1)
            {
                //更新MemberInfo
                foreach (DataRow dr in dtMemberInfo.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[3];
                    paras[0] = new SqlParameter("@name", Encryption.Encryption.GetEncryptionName(dr["name"].ToString()));
                    paras[1] = new SqlParameter("@bankbook", Encryption.Encryption.GetEncryptionName(dr["bankbook"].ToString()));
                    paras[2] = new SqlParameter("@Number", dr["Number"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update MemberInfo set [Name]=@name,BankBook=@bankbook where Number=@Number", paras, CommandType.Text);
                }
                //更新MemberOrder
                foreach (DataRow dr in dtMemberOrder.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@Consignee", Encryption.Encryption.GetEncryptionName(dr["Consignee"].ToString()));
                    paras[1] = new SqlParameter("@OrderID", dr["OrderID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update MemberOrder set Consignee=@Consignee where OrderID=@OrderID ", paras, CommandType.Text);
                }
                //更新StoreInfo
                foreach (DataRow dr in dtStoreInfo.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[3];
                    paras[0] = new SqlParameter("@Name", Encryption.Encryption.GetEncryptionName(dr["Name"].ToString()));
                    paras[1] = new SqlParameter("@StoreName", Encryption.Encryption.GetEncryptionName(dr["StoreName"].ToString()));
                    paras[2] = new SqlParameter("@StoreId", dr["StoreId"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update StoreInfo set StoreName=@StoreName,[Name]=@name where StoreId=@StoreId ", paras, CommandType.Text);
                }
                //更新StoreOrder
                foreach (DataRow dr in dtStoreOrder.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@InceptPerson", Encryption.Encryption.GetEncryptionName(dr["InceptPerson"].ToString()));
                    paras[1] = new SqlParameter("@StoreOrderID", dr["StoreOrderID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update StoreOrder set InceptPerson=@InceptPerson where StoreOrderID=@StoreOrderID ", paras, CommandType.Text);
                }
                //更新Remittances
                foreach (DataRow dr in dtRemittances.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@Sender", Encryption.Encryption.GetEncryptionName(dr["Sender"].ToString()));
                    paras[1] = new SqlParameter("@ID", dr["ID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update Remittances set Sender=@Sender where ID=@ID ", paras, CommandType.Text);
                }
                //更新UnauditedStoreInfo
                foreach (DataRow dr in dtUnauditedStoreInfo.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[3];
                    paras[0] = new SqlParameter("@Name", Encryption.Encryption.GetEncryptionName(dr["Name"].ToString()));
                    paras[1] = new SqlParameter("@StoreName", Encryption.Encryption.GetEncryptionName(dr["StoreName"].ToString()));
                    paras[2] = new SqlParameter("@StoreID", dr["StoreID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update UnauditedStoreInfo set StoreName=@StoreName,[Name]=@name where StoreID=@StoreID ", paras, CommandType.Text);
                }
                //更新DetailMoney
                foreach (DataRow dr in dtDetailMoney.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@Bankbook", Encryption.Encryption.GetEncryptionName(dr["Bankbook"].ToString()));
                    paras[1] = new SqlParameter("@ID", dr["ID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update DetailMoney set Bankbook=@Bankbook where ID=@ID ", paras, CommandType.Text);
                }
                //更新ECRemitDetail
                foreach (DataRow dr in dtECRemitDetail.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@Remitter", Encryption.Encryption.GetEncryptionName(dr["Remitter"].ToString()));
                    paras[1] = new SqlParameter("@ID", dr["ID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update ECRemitDetail set Remitter=@Remitter where ID=@ID ", paras, CommandType.Text);
                }
                //更新Replacement
                foreach (DataRow dr in dtReplacement.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@InceptPerson", Encryption.Encryption.GetEncryptionName(dr["InceptPerson"].ToString()));
                    paras[1] = new SqlParameter("@ID", dr["ID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update Replacement set InceptPerson=@InceptPerson where ID=@ID ", paras, CommandType.Text);
                }
                //更新MemberOff
                foreach (DataRow dr in dtMemberOff.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@name", Encryption.Encryption.GetEncryptionName(dr["name"].ToString()));
                    paras[1] = new SqlParameter("@ID", dr["ID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update memberoff set name=@name where ID=@ID ", paras, CommandType.Text);
                }
            }
            else
            {
                //更新MemberInfo
                foreach (DataRow dr in dtMemberInfo.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[3];
                    paras[0] = new SqlParameter("@name", Encryption.Encryption.GetDecipherName(dr["name"].ToString()));
                    paras[1] = new SqlParameter("@bankbook", Encryption.Encryption.GetDecipherName(dr["bankbook"].ToString()));
                    paras[2] = new SqlParameter("@Number", dr["Number"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update MemberInfo set [Name]=@name,BankBook=@bankbook where Number=@Number", paras, CommandType.Text);
                }
                //更新MemberOrder
                foreach (DataRow dr in dtMemberOrder.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@Consignee", Encryption.Encryption.GetDecipherName(dr["Consignee"].ToString()));
                    paras[1] = new SqlParameter("@OrderID", dr["OrderID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update MemberOrder set Consignee=@Consignee where OrderID=@OrderID ", paras, CommandType.Text);
                }
                //更新StoreInfo
                foreach (DataRow dr in dtStoreInfo.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[3];
                    paras[0] = new SqlParameter("@Name", Encryption.Encryption.GetDecipherName(dr["Name"].ToString()));
                    paras[1] = new SqlParameter("@StoreName", Encryption.Encryption.GetDecipherName(dr["StoreName"].ToString()));
                    paras[2] = new SqlParameter("@StoreId", dr["StoreId"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update StoreInfo set StoreName=@StoreName,[Name]=@name where StoreId=@StoreId ", paras, CommandType.Text);
                }
                //更新StoreOrder
                foreach (DataRow dr in dtStoreOrder.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@InceptPerson", Encryption.Encryption.GetDecipherName(dr["InceptPerson"].ToString()));
                    paras[1] = new SqlParameter("@StoreOrderID", dr["StoreOrderID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update StoreOrder set InceptPerson=@InceptPerson where StoreOrderID=@StoreOrderID ", paras, CommandType.Text);
                }
                //更新Remittances
                foreach (DataRow dr in dtRemittances.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@Sender", Encryption.Encryption.GetDecipherName(dr["Sender"].ToString()));
                    paras[1] = new SqlParameter("@ID", dr["ID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update Remittances set Sender=@Sender where ID=@ID ", paras, CommandType.Text);
                }
                //更新UnauditedStoreInfo
                foreach (DataRow dr in dtUnauditedStoreInfo.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[3];
                    paras[0] = new SqlParameter("@Name", Encryption.Encryption.GetDecipherName(dr["Name"].ToString()));
                    paras[1] = new SqlParameter("@StoreName", Encryption.Encryption.GetDecipherName(dr["StoreName"].ToString()));
                    paras[2] = new SqlParameter("@StoreID", dr["StoreID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update UnauditedStoreInfo set StoreName=@StoreName,[Name]=@name where StoreID=@StoreID ", paras, CommandType.Text);
                }
                //更新DetailMoney
                foreach (DataRow dr in dtDetailMoney.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@Bankbook", Encryption.Encryption.GetDecipherName(dr["Bankbook"].ToString()));
                    paras[1] = new SqlParameter("@ID", dr["ID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update DetailMoney set Bankbook=@Bankbook where ID=@ID ", paras, CommandType.Text);
                }
                //更新ECRemitDetail
                foreach (DataRow dr in dtECRemitDetail.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@Remitter", Encryption.Encryption.GetDecipherName(dr["Remitter"].ToString()));
                    paras[1] = new SqlParameter("@ID", dr["ID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update ECRemitDetail set Remitter=@Remitter where ID=@ID ", paras, CommandType.Text);
                }
                //更新Replacement
                foreach (DataRow dr in dtReplacement.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@InceptPerson", Encryption.Encryption.GetDecipherName(dr["InceptPerson"].ToString()));
                    paras[1] = new SqlParameter("@ID", dr["ID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update Replacement set InceptPerson=@InceptPerson where ID=@ID ", paras, CommandType.Text);
                }
                //更新MemberOff
                foreach (DataRow dr in dtMemberOff.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@name", Encryption.Encryption.GetDecipherName(dr["name"].ToString()));
                    paras[1] = new SqlParameter("@ID", dr["ID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update memberoff set name=@name where ID=@ID ", paras, CommandType.Text);
                }
                
            }

            //更新 设置
            SqlParameter[] parasSet = new SqlParameter[2];
            parasSet[0] = new SqlParameter("@EncryptionValue", type);
            parasSet[1] = new SqlParameter("@EncryptionKey", "--Name--");
            DBHelper.ExecuteNonQuery(tran, "update EncryptionSetting set EncryptionValue=@EncryptionValue where EncryptionKey=@EncryptionKey", parasSet, CommandType.Text);

            Encryption.Encryption.ValueState = "";
        }

        /// <summary>
        /// 更新所有地址
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="type">类型：1为更新成加密，0为更新成不加密</param>
        public static void UpdateAddress(SqlTransaction tran, int type)
        {
            Encryption.Encryption.ValueState = "1";
            DataTable dtMemberInfo = DBHelper.ExecuteDataTable(tran, "select Number,Address,BankAddress from MemberInfo");
            DataTable dtMemberOrder = DBHelper.ExecuteDataTable(tran, "select ConAddress,OrderID from MemberOrder");
            DataTable dtStoreInfo = DBHelper.ExecuteDataTable(tran, "select StoreId,StoreAddress from StoreInfo");
            DataTable dtStoreOrder = DBHelper.ExecuteDataTable(tran, "select StoreOrderID,InceptAddress from StoreOrder");
            DataTable dtUnauditedStoreInfo = DBHelper.ExecuteDataTable(tran, "select StoreID,StoreAddress from UnauditedStoreInfo");
            DataTable dtReplacement = DBHelper.ExecuteDataTable(tran, "select ID,InceptAddress from Replacement");

            if (type == 1)
            {
                //更新MemberInfo
                foreach (DataRow dr in dtMemberInfo.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[3];
                    paras[0] = new SqlParameter("@Address", Encryption.Encryption.GetEncryptionAddress(dr["Address"].ToString()));
                    paras[1] = new SqlParameter("@BankAddress", Encryption.Encryption.GetEncryptionAddress(dr["BankAddress"].ToString()));
                    paras[2] = new SqlParameter("@Number", dr["Number"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update MemberInfo set Address=@Address,BankAddress=@BankAddress where Number=@Number", paras, CommandType.Text);
                }
                //更新MemberOrder
                foreach (DataRow dr in dtMemberOrder.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@ConAddress", Encryption.Encryption.GetEncryptionAddress(dr["ConAddress"].ToString()));
                    paras[1] = new SqlParameter("@OrderID", dr["OrderID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update MemberOrder set ConAddress=@ConAddress where OrderID=@OrderID ", paras, CommandType.Text);
                }
                //更新StoreInfo
                foreach (DataRow dr in dtStoreInfo.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@StoreAddress", Encryption.Encryption.GetEncryptionAddress(dr["StoreAddress"].ToString()));
                    paras[1] = new SqlParameter("@StoreId", dr["StoreId"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update StoreInfo set StoreAddress=@StoreAddress where StoreId=@StoreId ", paras, CommandType.Text);
                }
                //更新StoreOrder
                foreach (DataRow dr in dtStoreOrder.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@InceptAddress", Encryption.Encryption.GetEncryptionAddress(dr["InceptAddress"].ToString()));
                    paras[1] = new SqlParameter("@StoreOrderID", dr["StoreOrderID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update StoreOrder set InceptAddress=@InceptAddress where StoreOrderID=@StoreOrderID ", paras, CommandType.Text);
                }
                //更新UnauditedStoreInfo
                foreach (DataRow dr in dtUnauditedStoreInfo.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@StoreAddress", Encryption.Encryption.GetEncryptionAddress(dr["StoreAddress"].ToString()));
                    paras[1] = new SqlParameter("@StoreID", dr["StoreID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update UnauditedStoreInfo set StoreAddress=@StoreAddress where StoreID=@StoreID ", paras, CommandType.Text);
                }
                //更新Replacement
                foreach (DataRow dr in dtReplacement.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@InceptAddress", Encryption.Encryption.GetEncryptionAddress(dr["InceptAddress"].ToString()));
                    paras[1] = new SqlParameter("@ID", dr["ID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update Replacement set InceptAddress=@InceptAddress where ID=@ID ", paras, CommandType.Text);
                }
            }
            else
            {
                //更新MemberInfo
                foreach (DataRow dr in dtMemberInfo.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[3];
                    paras[0] = new SqlParameter("@Address", Encryption.Encryption.GetDecipherAddress(dr["Address"].ToString()));
                    paras[1] = new SqlParameter("@BankAddress", Encryption.Encryption.GetDecipherAddress(dr["BankAddress"].ToString()));
                    paras[2] = new SqlParameter("@Number", dr["Number"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update MemberInfo set Address=@Address,BankAddress=@BankAddress where Number=@Number", paras, CommandType.Text);
                }
                //更新MemberOrder
                foreach (DataRow dr in dtMemberOrder.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@ConAddress", Encryption.Encryption.GetDecipherAddress(dr["ConAddress"].ToString()));
                    paras[1] = new SqlParameter("@OrderID", dr["OrderID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update MemberOrder set ConAddress=@ConAddress where OrderID=@OrderID ", paras, CommandType.Text);
                }
                //更新StoreInfo
                foreach (DataRow dr in dtStoreInfo.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@StoreAddress", Encryption.Encryption.GetDecipherAddress(dr["StoreAddress"].ToString()));
                    paras[1] = new SqlParameter("@StoreId", dr["StoreId"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update StoreInfo set StoreAddress=@StoreAddress where StoreId=@StoreId ", paras, CommandType.Text);
                }
                //更新StoreOrder
                foreach (DataRow dr in dtStoreOrder.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@InceptAddress", Encryption.Encryption.GetDecipherAddress(dr["InceptAddress"].ToString()));
                    paras[1] = new SqlParameter("@StoreOrderID", dr["StoreOrderID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update StoreOrder set InceptAddress=@InceptAddress where StoreOrderID=@StoreOrderID ", paras, CommandType.Text);
                }
                //更新UnauditedStoreInfo
                foreach (DataRow dr in dtUnauditedStoreInfo.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@StoreAddress", Encryption.Encryption.GetDecipherAddress(dr["StoreAddress"].ToString()));
                    paras[1] = new SqlParameter("@StoreID", dr["StoreID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update UnauditedStoreInfo set StoreAddress=@StoreAddress where StoreID=@StoreID ", paras, CommandType.Text);
                }
                //更新Replacement
                foreach (DataRow dr in dtReplacement.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@InceptAddress", Encryption.Encryption.GetDecipherAddress(dr["InceptAddress"].ToString()));
                    paras[1] = new SqlParameter("@ID", dr["ID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update Replacement set InceptAddress=@InceptAddress where ID=@ID ", paras, CommandType.Text);
                }
            }
            //更新 设置
            SqlParameter[] parasSet = new SqlParameter[2];
            parasSet[0] = new SqlParameter("@EncryptionValue", type);
            parasSet[1] = new SqlParameter("@EncryptionKey", "--Address--");
            DBHelper.ExecuteNonQuery(tran, "update EncryptionSetting set EncryptionValue=@EncryptionValue where EncryptionKey=@EncryptionKey", parasSet, CommandType.Text);

            Encryption.Encryption.ValueState = "";
        }

        /// <summary>
        /// 更新所有电话号码
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="type">类型：1为更新成加密，0为更新成不加密</param>
        public static void UpdateTele(SqlTransaction tran, int type)
        {
            Encryption.Encryption.ValueState = "1";
            DataTable dtMemberInfo = DBHelper.ExecuteDataTable(tran, "select Number,HomeTele,OfficeTele,MobileTele,FaxTele from MemberInfo");
            DataTable dtMemberOrder = DBHelper.ExecuteDataTable(tran, "select ConTelphone,ConMobilPhone,OrderID from MemberOrder");
            DataTable dtStoreInfo = DBHelper.ExecuteDataTable(tran, "select StoreId,HomeTele,OfficeTele,MobileTele,FaxTele from StoreInfo");
            DataTable dtStoreOrder = DBHelper.ExecuteDataTable(tran, "select StoreOrderID,Telephone from StoreOrder");
            DataTable dtUnauditedStoreInfo = DBHelper.ExecuteDataTable(tran, "select StoreID,HomeTele,OfficeTele,MobileTele,FaxTele from UnauditedStoreInfo");
            DataTable dtReplacement = DBHelper.ExecuteDataTable(tran, "select ID,Telephone from Replacement");
            DataTable dtMemberOff = DBHelper.ExecuteDataTable(tran, "select id,mobiletele from memberoff");

            if (type == 1)
            {
                //更新MemberInfo
                foreach (DataRow dr in dtMemberInfo.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[5];
                    paras[0] = new SqlParameter("@HomeTele", Encryption.Encryption.GetEncryptionTele(dr["HomeTele"].ToString()));
                    paras[1] = new SqlParameter("@OfficeTele", Encryption.Encryption.GetEncryptionTele(dr["OfficeTele"].ToString()));
                    paras[2] = new SqlParameter("@MobileTele", Encryption.Encryption.GetEncryptionTele(dr["MobileTele"].ToString()));
                    paras[3] = new SqlParameter("@FaxTele", Encryption.Encryption.GetEncryptionTele(dr["FaxTele"].ToString()));
                    paras[4] = new SqlParameter("@Number", dr["Number"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update MemberInfo set HomeTele=@HomeTele,OfficeTele=@OfficeTele,MobileTele=@MobileTele,FaxTele=@FaxTele where Number=@Number", paras, CommandType.Text);
                }
                //更新MemberOrder
                foreach (DataRow dr in dtMemberOrder.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[3];
                    paras[0] = new SqlParameter("@ConTelphone", Encryption.Encryption.GetEncryptionTele(dr["ConTelphone"].ToString()));
                    paras[1] = new SqlParameter("@ConMobilPhone", Encryption.Encryption.GetEncryptionTele(dr["ConMobilPhone"].ToString()));
                    paras[2] = new SqlParameter("@OrderID", dr["OrderID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update MemberOrder set ConTelphone=@ConTelphone,ConMobilPhone=@ConMobilPhone where OrderID=@OrderID ", paras, CommandType.Text);
                }
                //更新StoreInfo
                foreach (DataRow dr in dtStoreInfo.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[5];
                    paras[0] = new SqlParameter("@HomeTele", Encryption.Encryption.GetEncryptionTele(dr["HomeTele"].ToString()));
                    paras[1] = new SqlParameter("@OfficeTele", Encryption.Encryption.GetEncryptionTele(dr["OfficeTele"].ToString()));
                    paras[2] = new SqlParameter("@MobileTele", Encryption.Encryption.GetEncryptionTele(dr["MobileTele"].ToString()));
                    paras[3] = new SqlParameter("@FaxTele", Encryption.Encryption.GetEncryptionTele(dr["FaxTele"].ToString()));
                    paras[4] = new SqlParameter("@StoreId", dr["StoreId"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update StoreInfo set HomeTele=@HomeTele,OfficeTele=@OfficeTele,MobileTele=@MobileTele,FaxTele=@FaxTele where StoreId=@StoreId ", paras, CommandType.Text);
                }
                //更新StoreOrder
                foreach (DataRow dr in dtStoreOrder.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@Telephone", Encryption.Encryption.GetEncryptionTele(dr["Telephone"].ToString()));
                    paras[1] = new SqlParameter("@StoreOrderID", dr["StoreOrderID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update StoreOrder set Telephone=@Telephone where StoreOrderID=@StoreOrderID ", paras, CommandType.Text);
                }
                //更新UnauditedStoreInfo
                foreach (DataRow dr in dtUnauditedStoreInfo.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[5];
                    paras[0] = new SqlParameter("@HomeTele", Encryption.Encryption.GetEncryptionTele(dr["HomeTele"].ToString()));
                    paras[1] = new SqlParameter("@OfficeTele", Encryption.Encryption.GetEncryptionTele(dr["OfficeTele"].ToString()));
                    paras[2] = new SqlParameter("@MobileTele", Encryption.Encryption.GetEncryptionTele(dr["MobileTele"].ToString()));
                    paras[3] = new SqlParameter("@FaxTele", Encryption.Encryption.GetEncryptionTele(dr["FaxTele"].ToString()));
                    paras[4] = new SqlParameter("@StoreID", dr["StoreID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update UnauditedStoreInfo set HomeTele=@HomeTele,MobileTele=@MobileTele,FaxTele=@FaxTele where StoreID=@StoreID ", paras, CommandType.Text);
                }
                //更新Replacement
                foreach (DataRow dr in dtReplacement.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@Telephone", Encryption.Encryption.GetEncryptionTele(dr["Telephone"].ToString()));
                    paras[1] = new SqlParameter("@ID", dr["ID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update Replacement set Telephone=@Telephone where ID=@ID ", paras, CommandType.Text);
                }
                //更新MemberOff
                foreach (DataRow dr in dtMemberOff.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@mobiletele", Encryption.Encryption.GetEncryptionTele(dr["mobiletele"].ToString()));
                    paras[1] = new SqlParameter("@ID", dr["ID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update memberoff set mobiletele=@mobiletele where ID=@ID ", paras, CommandType.Text);
                }
            }
            else
            {
                //更新MemberInfo
                foreach (DataRow dr in dtMemberInfo.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[5];
                    paras[0] = new SqlParameter("@HomeTele", Encryption.Encryption.GetDecipherTele(dr["HomeTele"].ToString()));
                    paras[1] = new SqlParameter("@OfficeTele", Encryption.Encryption.GetDecipherTele(dr["OfficeTele"].ToString()));
                    paras[2] = new SqlParameter("@MobileTele", Encryption.Encryption.GetDecipherTele(dr["MobileTele"].ToString()));
                    paras[3] = new SqlParameter("@FaxTele", Encryption.Encryption.GetDecipherTele(dr["FaxTele"].ToString()));
                    paras[4] = new SqlParameter("@Number", dr["Number"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update MemberInfo set HomeTele=@HomeTele,OfficeTele=@OfficeTele,MobileTele=@MobileTele,FaxTele=@FaxTele where Number=@Number", paras, CommandType.Text);
                }
                //更新MemberOrder
                foreach (DataRow dr in dtMemberOrder.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[3];
                    paras[0] = new SqlParameter("@ConTelphone", Encryption.Encryption.GetDecipherTele(dr["ConTelphone"].ToString()));
                    paras[1] = new SqlParameter("@ConMobilPhone", Encryption.Encryption.GetDecipherTele(dr["ConMobilPhone"].ToString()));
                    paras[2] = new SqlParameter("@OrderID", dr["OrderID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update MemberOrder set ConTelphone=@ConTelphone,ConMobilPhone=@ConMobilPhone where OrderID=@OrderID ", paras, CommandType.Text);
                }
                //更新StoreInfo
                foreach (DataRow dr in dtStoreInfo.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[5];
                    paras[0] = new SqlParameter("@HomeTele", Encryption.Encryption.GetDecipherTele(dr["HomeTele"].ToString()));
                    paras[1] = new SqlParameter("@OfficeTele", Encryption.Encryption.GetDecipherTele(dr["OfficeTele"].ToString()));
                    paras[2] = new SqlParameter("@MobileTele", Encryption.Encryption.GetDecipherTele(dr["MobileTele"].ToString()));
                    paras[3] = new SqlParameter("@FaxTele", Encryption.Encryption.GetDecipherTele(dr["FaxTele"].ToString()));
                    paras[4] = new SqlParameter("@StoreId", dr["StoreId"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update StoreInfo set HomeTele=@HomeTele,OfficeTele=@OfficeTele,MobileTele=@MobileTele,FaxTele=@FaxTele where StoreId=@StoreId ", paras, CommandType.Text);
                }
                //更新StoreOrder
                foreach (DataRow dr in dtStoreOrder.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@Telephone", Encryption.Encryption.GetDecipherTele(dr["Telephone"].ToString()));
                    paras[1] = new SqlParameter("@StoreOrderID", dr["StoreOrderID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update StoreOrder set Telephone=@Telephone where StoreOrderID=@StoreOrderID ", paras, CommandType.Text);
                }
                //更新UnauditedStoreInfo
                foreach (DataRow dr in dtUnauditedStoreInfo.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[5];
                    paras[0] = new SqlParameter("@HomeTele", Encryption.Encryption.GetDecipherTele(dr["HomeTele"].ToString()));
                    paras[1] = new SqlParameter("@OfficeTele", Encryption.Encryption.GetDecipherTele(dr["OfficeTele"].ToString()));
                    paras[2] = new SqlParameter("@MobileTele", Encryption.Encryption.GetDecipherTele(dr["MobileTele"].ToString()));
                    paras[3] = new SqlParameter("@FaxTele", Encryption.Encryption.GetDecipherTele(dr["FaxTele"].ToString()));
                    paras[4] = new SqlParameter("@StoreID", dr["StoreID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update UnauditedStoreInfo set HomeTele=@HomeTele,MobileTele=@MobileTele,FaxTele=@FaxTele where StoreID=@StoreID ", paras, CommandType.Text);
                }
                //更新Replacement
                foreach (DataRow dr in dtReplacement.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@Telephone", Encryption.Encryption.GetDecipherTele(dr["Telephone"].ToString()));
                    paras[1] = new SqlParameter("@ID", dr["ID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update Replacement set Telephone=@Telephone where ID=@ID ", paras, CommandType.Text);
                }
                //更新MemberOff
                foreach (DataRow dr in dtMemberOff.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@mobiletele", Encryption.Encryption.GetDecipherTele(dr["mobiletele"].ToString()));
                    paras[1] = new SqlParameter("@ID", dr["ID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update memberoff set mobiletele=@mobiletele where ID=@ID ", paras, CommandType.Text);
                }
            }
            //更新 设置
            SqlParameter[] parasSet = new SqlParameter[2];
            parasSet[0] = new SqlParameter("@EncryptionValue", type);
            parasSet[1] = new SqlParameter("@EncryptionKey", "--Tele--");
            DBHelper.ExecuteNonQuery(tran, "update EncryptionSetting set EncryptionValue=@EncryptionValue where EncryptionKey=@EncryptionKey", parasSet, CommandType.Text);

            Encryption.Encryption.ValueState = "";
        }

        /// <summary>
        /// 更新所有卡号
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="type">类型：1为更新成加密，0为更新成不加密</param>
        public static void UpdateCard(SqlTransaction tran, int type)
        {
            Encryption.Encryption.ValueState = "1";
            DataTable dtMemberInfo = DBHelper.ExecuteDataTable(tran, "select Number,BankCard from MemberInfo");
            DataTable dtStoreInfo = DBHelper.ExecuteDataTable(tran, "select StoreId,BankCard from StoreInfo");
            DataTable dtRemittances = DBHelper.ExecuteDataTable(tran, "select RemittancesAccount,ImportNumber,ID from Remittances");
            DataTable dtUnauditedStoreInfo = DBHelper.ExecuteDataTable(tran, "select StoreID,BankCard from UnauditedStoreInfo");
            DataTable dtDetailMoney = DBHelper.ExecuteDataTable(tran, "select ID,BankCard from DetailMoney");
            DataTable dtECRemitDetail = DBHelper.ExecuteDataTable(tran, "select ID,AbouchementCard,IdentityCard from ECRemitDetail ");

            if (type == 1)
            {
                //更新MemberInfo
                foreach (DataRow dr in dtMemberInfo.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@BankCard", Encryption.Encryption.GetEncryptionCard(dr["BankCard"].ToString()));
                    paras[1] = new SqlParameter("@Number", dr["Number"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update MemberInfo set BankCard=@BankCard where Number=@Number", paras, CommandType.Text);
                }
                //更新StoreInfo
                foreach (DataRow dr in dtStoreInfo.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@BankCard", Encryption.Encryption.GetEncryptionCard(dr["BankCard"].ToString()));
                    paras[1] = new SqlParameter("@StoreId", dr["StoreId"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update StoreInfo set BankCard=@BankCard where StoreId=@StoreId ", paras, CommandType.Text);
                }
                //更新Remittances
                foreach (DataRow dr in dtRemittances.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[3];
                    paras[0] = new SqlParameter("@RemittancesAccount", Encryption.Encryption.GetEncryptionCard(dr["RemittancesAccount"].ToString()));
                    paras[1] = new SqlParameter("@ImportNumber", Encryption.Encryption.GetEncryptionCard(dr["ImportNumber"].ToString()));
                    paras[2] = new SqlParameter("@ID", dr["ID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update Remittances set ImportNumber=@ImportNumber,RemittancesAccount=@RemittancesAccount where ID=@ID ", paras, CommandType.Text);
                }
                //更新UnauditedStoreInfo
                foreach (DataRow dr in dtUnauditedStoreInfo.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@BankCard", Encryption.Encryption.GetEncryptionCard(dr["BankCard"].ToString()));
                    paras[1] = new SqlParameter("@StoreID", dr["StoreID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update UnauditedStoreInfo set BankCard=@BankCard where StoreID=@StoreID ", paras, CommandType.Text);
                }
                //更新DetailMoney
                foreach (DataRow dr in dtDetailMoney.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@BankCard", Encryption.Encryption.GetEncryptionCard(dr["BankCard"].ToString()));
                    paras[1] = new SqlParameter("@ID", dr["ID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update DetailMoney set BankCard=@BankCard where ID=@ID ", paras, CommandType.Text);
                }
                //更新ECRemitDetail
                foreach (DataRow dr in dtECRemitDetail.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@AbouchementCard", Encryption.Encryption.GetEncryptionCard(dr["AbouchementCard"].ToString()));
                    paras[1] = new SqlParameter("@ID", dr["ID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update ECRemitDetail set AbouchementCard=@AbouchementCard where ID=@ID ", paras, CommandType.Text);
                }
            }
            else
            {
                //更新MemberInfo
                foreach (DataRow dr in dtMemberInfo.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@BankCard", Encryption.Encryption.GetDecipherCard(dr["BankCard"].ToString()));
                    paras[1] = new SqlParameter("@Number", dr["Number"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update MemberInfo set BankCard=@BankCard where Number=@Number", paras, CommandType.Text);
                }
                //更新StoreInfo
                foreach (DataRow dr in dtStoreInfo.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@BankCard", Encryption.Encryption.GetDecipherCard(dr["BankCard"].ToString()));
                    paras[1] = new SqlParameter("@StoreId", dr["StoreId"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update StoreInfo set BankCard=@BankCard where StoreId=@StoreId ", paras, CommandType.Text);
                }
                //更新Remittances
                foreach (DataRow dr in dtRemittances.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[3];
                    paras[0] = new SqlParameter("@RemittancesAccount", Encryption.Encryption.GetDecipherCard(dr["RemittancesAccount"].ToString()));
                    paras[1] = new SqlParameter("@ImportNumber", Encryption.Encryption.GetDecipherCard(dr["ImportNumber"].ToString()));
                    paras[2] = new SqlParameter("@ID", dr["ID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update Remittances set ImportNumber=@ImportNumber,RemittancesAccount=@RemittancesAccount where ID=@ID ", paras, CommandType.Text);
                }
                //更新UnauditedStoreInfo
                foreach (DataRow dr in dtUnauditedStoreInfo.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@BankCard", Encryption.Encryption.GetDecipherCard(dr["BankCard"].ToString()));
                    paras[1] = new SqlParameter("@StoreID", dr["StoreID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update UnauditedStoreInfo set BankCard=@BankCard where StoreID=@StoreID ", paras, CommandType.Text);
                }
                //更新DetailMoney
                foreach (DataRow dr in dtDetailMoney.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@BankCard", Encryption.Encryption.GetDecipherCard(dr["BankCard"].ToString()));
                    paras[1] = new SqlParameter("@ID", dr["ID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update DetailMoney set BankCard=@BankCard where ID=@ID ", paras, CommandType.Text);
                }
                //更新ECRemitDetail
                foreach (DataRow dr in dtECRemitDetail.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@AbouchementCard", Encryption.Encryption.GetDecipherCard(dr["AbouchementCard"].ToString()));
                    paras[1] = new SqlParameter("@ID", dr["ID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update ECRemitDetail set AbouchementCard=@AbouchementCard where ID=@ID ", paras, CommandType.Text);
                }
            }
            //更新 设置
            SqlParameter[] parasSet = new SqlParameter[2];
            parasSet[0] = new SqlParameter("@EncryptionValue", type);
            parasSet[1] = new SqlParameter("@EncryptionKey", "--Card--");
            DBHelper.ExecuteNonQuery(tran, "update EncryptionSetting set EncryptionValue=@EncryptionValue where EncryptionKey=@EncryptionKey", parasSet, CommandType.Text);

            Encryption.Encryption.ValueState = "";
        }

       
        /// <summary>
        /// 更新所有证件号码
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="type">类型：1为更新成加密，0为更新成不加密</param>
        public static void UpdateNumber(SqlTransaction tran,int type)
        {
            Encryption.Encryption.ValueState = "1";
            DataTable dtMemberInfo = DBHelper.ExecuteDataTable(tran, "select Number,PaperNumber from MemberInfo");
            DataTable dtRemittances = DBHelper.ExecuteDataTable(tran, "select SenderID,ID from Remittances");
            DataTable dtMemberOff = DBHelper.ExecuteDataTable(tran, "select id,papernumber from memberoff");

            if (type == 1)
            {
                //更新MemberInfo
                foreach (DataRow dr in dtMemberInfo.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@PaperNumber", Encryption.Encryption.GetEncryptionNumber(dr["PaperNumber"].ToString()));
                    paras[1] = new SqlParameter("@Number", dr["Number"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update MemberInfo set PaperNumber=@PaperNumber where Number=@Number", paras, CommandType.Text);
                }
                //更新Remittances
                foreach (DataRow dr in dtRemittances.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@SenderID", Encryption.Encryption.GetEncryptionNumber(dr["SenderID"].ToString()));
                    paras[1] = new SqlParameter("@ID", dr["ID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update Remittances set SenderID=@SenderID where ID=@ID ", paras, CommandType.Text);
                }
                //更新MemberOff
                foreach (DataRow dr in dtMemberOff.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@papernumber", Encryption.Encryption.GetEncryptionNumber(dr["papernumber"].ToString()));
                    paras[1] = new SqlParameter("@ID", dr["ID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update memberoff set papernumber=@papernumber where ID=@ID ", paras, CommandType.Text);
                }
            }
            else
            {
                //更新MemberInfo
                foreach (DataRow dr in dtMemberInfo.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@PaperNumber", Encryption.Encryption.GetDecipherNumber(dr["PaperNumber"].ToString()));
                    paras[1] = new SqlParameter("@Number", dr["Number"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update MemberInfo set PaperNumber=@PaperNumber where Number=@Number", paras, CommandType.Text);
                }
                //更新Remittances
                foreach (DataRow dr in dtRemittances.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@SenderID", Encryption.Encryption.GetDecipherNumber(dr["SenderID"].ToString()));
                    paras[1] = new SqlParameter("@ID", dr["ID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update Remittances set SenderID=@SenderID where ID=@ID ", paras, CommandType.Text);
                }
                //更新MemberOff
                foreach (DataRow dr in dtMemberOff.Rows)
                {
                    SqlParameter[] paras = new SqlParameter[2];
                    paras[0] = new SqlParameter("@papernumber", Encryption.Encryption.GetDecipherNumber(dr["papernumber"].ToString()));
                    paras[1] = new SqlParameter("@ID", dr["ID"].ToString());
                    DBHelper.ExecuteNonQuery(tran, "update memberoff set papernumber=@papernumber where ID=@ID ", paras, CommandType.Text);
                }
            }
            //更新 设置
            SqlParameter[] parasSet = new SqlParameter[2];
            parasSet[0] = new SqlParameter("@EncryptionValue", type);
            parasSet[1] = new SqlParameter("@EncryptionKey", "--Number--");
            DBHelper.ExecuteNonQuery(tran, "update EncryptionSetting set EncryptionValue=@EncryptionValue where EncryptionKey=@EncryptionKey", parasSet, CommandType.Text);

            Encryption.Encryption.ValueState = "";
        }
        
    }
}