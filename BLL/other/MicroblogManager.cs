using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace BLL
{
    public class MicroblogManager
    {

        public static int inserMicroblog(string content,string number,string ip)
        {
            return MicroblogService.inserMicroblog(content, number,ip);
        }


        public static int inserFriendGroup(string counfz, string Description, string number, string ip)
        {
            return MicroblogService.inserFriendGroup(counfz, Description, number, ip);
        }

        public static int delMicroblog(int id)
        {
            return MicroblogService.delMicroblog(id);
        }
    }
}
