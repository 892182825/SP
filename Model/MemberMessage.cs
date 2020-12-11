using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class MemberMessage
    {
        public MemberMessage()
        { 
        }

        private int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private string fromNumber;
        public string FromNumber
        {
            get { return fromNumber; }
            set { fromNumber = value; }
        }

        private string toNumber;
        public string ToNumber
        {
            get { return toNumber; }
            set { toNumber = value; }
        }

        private string messageIP;
        public string MessageIP
        {
            get { return messageIP; }
            set { messageIP = value; }
        }

        private string content;
        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        private DateTime messageDate;
        public DateTime MessageDate
        {
            get { return messageDate; }
            set { messageDate = value; }
        }
    }
}
