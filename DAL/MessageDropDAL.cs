using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Add Namespace
using System.Data;
using System.Data.SqlClient;

/*
 * Creator:     WangHua
 * CreateDate:  2010-04-07
 * FinishDate:  2010-04-07
 */

namespace DAL
{
    public class MessageDropDAL
    {
        /// <summary>
        /// Recover the deleted message which from MessageReceive or MessageSend
        /// </summary>        
        /// <param name="Id">Id</param>
        /// <returns>Return affeted row counts</returns>
        public static int RecoverMessage(int Id)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@Id",SqlDbType.Int)               
            };

            sparams[0].Value = Id;

            //
            return DBHelper.ExecuteNonQuery("RecoverMessage", sparams, CommandType.StoredProcedure);
        }
    }
}

