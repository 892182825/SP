using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

/**
 *  创建人：  郑华超
 *  创建时间：2009.8.27
 *  文件名：QueryFieldModel.cs
 *  功能：高级查询模型
 */
namespace Model
{
    /// <summary>
    /// 高级查询控制表
    /// </summary>
   public class QueryFieldModel
    {
        private int iD;

       /// <summary>
       /// 编号
       /// </summary>
        public int ID 
        {
            get { return iD; }
        }
        private string fieldName;

       /// <summary>
       /// //表和字段称FieldName
       /// </summary>
        public string FieldName
        {
            get { return fieldName; }
            set { fieldName = value; }
        }
        private string fieldExtend;

       /// <summary>
       /// //对应的中文FieldExtend
       /// </summary>
        public string FieldExtend
        {
            get { return fieldExtend; }
            set { fieldExtend = value; }
        }
        private int storeSelect;

       /// <summary>
       /// 店铺必选StoreSelects
       /// </summary>
        public int StoreSelect//
        {
            get { return storeSelect; }
            set { storeSelect = value; }
        }
        private int memberSelect;

       /// <summary>
        /// 会员必选MemberSelect
       /// </summary>
        public int MemberSelect//
        {
            get { return memberSelect; }
            set { memberSelect = value; }
        }

        public QueryFieldModel() 
        {
        
        }

        public QueryFieldModel(int id)
        {
            this.iD = id;
        }
    }
   

}
