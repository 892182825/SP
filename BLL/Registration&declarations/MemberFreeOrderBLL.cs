using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DAL;
using Model;
using System.Collections;

namespace BLL.Registration_declarations
{
    public class MemberFreeOrderBLL
    {
        /// <summary>
        ///  会员自由注册记录到MemberOrder和MemberDetails
        /// </summary>
        /// <param name="model">用户注册定单</param>
        /// <param name="list">用户注册定单列表</param>
        /// <returns></returns>
        public static bool AddOrder(MemberOrderModel model, IList<MemberDetailsModel> list)
        {
            bool state = false;
            int record = 0;
            using (SqlConnection conn = new SqlConnection(DAL.DBHelper.connString))
            {
                //打开连接
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    record += new AddFreeOrderDAL().AddOrder(model, tran);

                    foreach (MemberDetailsModel item in list)
                    {
                        record += new AddOrderDataDAL().insert_MemberOrderDetails(model, item, tran);
                    }

                    if (record == 0)
                    {

                        tran.Rollback();
                        conn.Close();
                    }
                    else
                    {
                        //更信结算表,注意此时保单时未确认的所以flag为0
                        new AddOrderDataDAL().Js_addfuxiao(model.Number, Convert.ToDouble(model.TotalMoney), model.OrderExpect, 0, tran);

                        state = true;
                        tran.Commit();
                    }
                }
                catch
                {
                    tran.Rollback();
                }
                finally
                {
                    conn.Close();
                }

            }


            return state;

        }
    }
}
