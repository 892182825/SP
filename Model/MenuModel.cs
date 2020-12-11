using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class MenuModel
    {
        private int iD;
        /// <summary>
        ///  菜单编号
        /// </summary>
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        private string menuName;
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName
        {
            get { return menuName; }
            set { menuName = value; }
        }
        private string menuFile;
        /// <summary>
        /// 菜单路径
        /// </summary>
        public string MenuFile
        {
            get { return menuFile; }
            set { menuFile = value; }
        }
        private string imgFile;
        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImgFile
        {
            get { return imgFile; }
            set { imgFile = value; }
        }
        private int menuType;
        /// <summary>
        /// 菜单类型（公司，店铺，会员）
        /// </summary>
        public int MenuType
        {
            get { return menuType; }
            set { menuType = value; }
        }
        private int parentID;
        /// <summary>
        /// 父菜单编号
        /// </summary>
        public int ParentID
        {
            get { return parentID; }
            set { parentID = value; }
        }

        private int sortid;


        public int Sortid
        {
            get { return sortid; }
            set { sortid = value; }
        }

        private int isfold;

        public int Isfold
        {
            get { return isfold; }
            set { isfold = value; }
        }

       
    }
}
