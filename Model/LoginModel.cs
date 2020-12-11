using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 *  创建人：  郑华超
 *  创建时间：2009.8.27
 *  文件名：LoginModel.cs
 *  功能：摘要说明 
 */
namespace Model
{
    [Serializable]
    /// <summary>
    /// LoginModel 摘要说明表
    /// </summary>
    public class LoginModel
    {
        #region 公共属性
        private int iD;
        /// <summary>
        /// ID
        /// </summary>
        public int ID
        {
            get { return iD; }
        }
        private string col1;
        /// <summary>
        /// col1
        /// </summary>
        public string Col1
        {
            get { return col1; }
            set { col1 = value; }
        }
        private string col2;
        /// <summary>
        /// col2	
        /// </summary>
        public string Col2
        {
            get { return col2; }
            set { col2 = value; }
        }
        #endregion

        #region 公共方法
        public LoginModel()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //

        }

        public LoginModel(int id)
        {
            this.iD = id;
        }
        #endregion

    }
}
