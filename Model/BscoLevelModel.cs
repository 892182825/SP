using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class BscoLevelModel
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private int levelint;

        public int Levelint
        {
            get { return levelint; }
            set { levelint = value; }
        }
        private string levelstr;

        public string Levelstr
        {
            get { return levelstr; }
            set { levelstr = value; }
        }
        private int flag;

        public int Flag
        {
            get { return flag; }
            set { flag = value; }
        }
        private string iCOPath;

        public string ICOPath
        {
            get { return iCOPath; }
            set { iCOPath = value; }
        }
    }
}
