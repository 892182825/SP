using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DAL;
using System.Data.SqlClient;

namespace BLL.other.Company
{
    public class EncryptionBLL
    {
        /// <summary>
        /// 获取所有设置
        /// </summary>
        /// <returns>设置参数值</returns>
        public static IList<EncryptionSetting> GetAllSetting()
        {
            return EncryptionDAL.SelectAll();
        }

        /// <summary>
        /// 更新--加密--参数设置
        /// </summary>
        /// <param name="ess">修改后的参数</param>
        /// <returns>返回是否成功</returns>
        public static bool EditSetting(IList<EncryptionSetting> ess)
        {
            bool reState = false;
            IList<EncryptionSetting> essOld = GetAllSetting();

            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();//打开连接
                SqlTransaction tran = conn.BeginTransaction();//开启事务

                try
                {
                    foreach (EncryptionSetting es in ess)
                    {
                        foreach (EncryptionSetting esOld in essOld)
                        {
                            if (es.EncryptionKey == esOld.EncryptionKey && es.EncryptionValue != esOld.EncryptionValue)
                            {
                                if (es.EncryptionKey == "--Name--")
                                {
                                    EncryptionDAL.UpdateName(tran, es.EncryptionValue);
                                }
                                else if (es.EncryptionKey == "--Tele--")
                                {
                                    EncryptionDAL.UpdateTele(tran, es.EncryptionValue);
                                }
                                else if (es.EncryptionKey == "--Address--")
                                {
                                    EncryptionDAL.UpdateAddress(tran, es.EncryptionValue);
                                }
                                else if (es.EncryptionKey == "--Card--")
                                {
                                    EncryptionDAL.UpdateCard(tran, es.EncryptionValue);
                                }
                                else if (es.EncryptionKey == "--Number--")
                                {
                                    EncryptionDAL.UpdateNumber(tran, es.EncryptionValue);
                                }
                            }
                        }
                    }

                    tran.Commit();
                    reState = true;
                }
                catch (Exception ex)
                {
                    string aa = ex.Message;
                    tran.Rollback();
                }
                finally
                {
                    conn.Close();
                }
            }

            return reState;
        }


    }
}
