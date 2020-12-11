using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class AlbumsModel
    {
        public AlbumsModel()
        { }

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string number;
        public string Number
        {
            get { return number; }
            set { number = value; }
        }

        private string albumsName;
        public string AlbumsName
        {
            get { return albumsName; }
            set { albumsName = value; }
        }

        private string viewPwd;
        public string ViewPwd
        {
            get { return viewPwd; }
            set { viewPwd = value; }
        }

        private int xuhao;
        public int Xuhao
        {
            get { return xuhao; }
            set { xuhao = value; }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private DateTime createDate;
        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }

        private string createIP;
        public string CreateIP
        {
            get { return createIP; }
            set { createIP = value; }
        }
    }
}
