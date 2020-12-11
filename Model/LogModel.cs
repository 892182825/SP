#region 版权信息
/*---------------------------------------------------------
 * copyright (C) 2009 shanghai qianchuang Tech. Co.,Ltd.
 *         上海乾创信息科技有限公司    版权所有
 * 文件名：LogModel.cs
 * 文件功能描述：系统登录日志实体类
 *
 *
 * 创建标识：董晨东 2009/08/26
 * 
 * 修改标识：
 * 
 * 修改描述：
 * 
 * 
 * 
 * 
 * 
 //----------------------------------------- **/
#endregion

using System;
namespace Model
{
    /// <summary>
    /// LogModel 的摘要说明
    /// </summary>
    public class LogModel
    {
        #region 公共属性

        private string number;	  //	varchar	
        /// <summary>
        /// 登录编号(bianhao)
        /// </summary>
        public string Number
        {
            get { return number; }
            set { number = value; }
        }
        private string logo;//	varchar	
        /// <summary>
        /// 标识(biaoshi)
        /// </summary>
        public string Logo
        {
            get { return logo; }
            set { logo = value; }
        }
        private string permissions;	//	varchar	
        /// <summary>
        /// 权限(quanxian)
        /// </summary>
        public string Permissions
        {
            get { return permissions; }
            set { permissions = value; }
        }
        private string categories;	//	varchar	
        /// <summary>
        /// 类别：会员、店铺、管理员(leibie)
        /// </summary>
        public string Categories
        {
            get { return categories; }
            set { categories = value; }
        }
        private string level;	//	varchar	
        /// <summary>
        /// 级别(jibie)
        /// </summary>
        public string Level
        {
            get { return level; }
            set { level = value; }
        }


        #endregion
        #region 公共方法
        /// <summary>
        /// 
        /// </summary>
        public LogModel()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #endregion

    }
}