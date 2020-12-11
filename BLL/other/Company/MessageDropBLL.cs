using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Add Namespace
using DAL;

namespace BLL.other.Company
{
    public class MessageDropBLL
    {
        /// <summary>
        /// Recover the deleted message which from MessageReceive or MessageSend
        /// </summary>        
        /// <param name="Id">Id</param>
        /// <returns>Return affeted row counts</returns>
        public static int RecoverMessage(int Id)
        {
            return MessageDropDAL.RecoverMessage(Id);
        }
    }
}
