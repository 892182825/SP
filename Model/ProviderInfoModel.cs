using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/**
 *  创建人：  郑华超
 *  创建时间：2009.8.27
 *  文件名：ProviderInfoModel.cs
 *  功能：产品供应商基本信息模型
 */
namespace Model
{
    /// <summary>
    /// 表4.1—67产品供应商基本信息表
    /// </summary>
    [Serializable]
    public class ProviderInfoModel
    {
        private int iD;

        /// <summary>
        /// 编号   Provider_ID
        /// </summary>
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
      
        private string number;
        /// <summary>
        /// //供应商编号Provider_IDNumber
        /// </summary>
        public string Number
        {
            get { return number; }
            set { number = value; }
        }
        private string name;

        /// <summary>
        /// //供应商名称 Provider_Name
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string forShort;

        /// <summary>
        /// //供应商简称Provider_JM
        /// </summary>
        public string ForShort
        {
            get { return forShort; }
            set { forShort = value; }
        }
        private string linkMan;

        /// <summary>
        /// //联系人Provider_LinkMan
        /// </summary>
        public string LinkMan
        {
            get { return linkMan; }
            set { linkMan = value; }
        }
        private string mobile;

        /// <summary>
        /// //联系人手机Provider_Mobile
        /// </summary>
        public string Mobile
        {
            get { return mobile; }
            set { mobile = value; }
        }
        private string telephone;

        /// <summary>
        /// //联系人电话Provider_Tel
        /// </summary>
        public string Telephone
        {
            get { return telephone; }
            set { telephone = value; }
        }
        private string fax;

        /// <summary>
        /// //联系人传真Provider_Fax
        /// </summary>
        public string Fax
        {
            get { return fax; }
            set { fax = value; }
        }
        private string email;

        /// <summary>
        /// //联系人电子邮箱Provider_Email
        /// </summary>
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        private string url;

       /// <summary>
        /// //供应商网址 Provider_Url
       /// </summary>
        public string Url
        {
            get { return url; }
            set { url = value; }
        }
        private string address;

        /// <summary>
        /// //供应商地址Provider_Address
        /// </summary>
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        private string bankName;

        /// <summary>
        /// //供应商开户行Provider_BankName
        /// </summary>
        public string BankName
        {
            get { return bankName; }
            set { bankName = value; }
        }
        private string bankAddress;

        /// <summary>
        /// //供应商开户行地址Provider_BankAddress
        /// </summary>
        public string BankAddress
        {
            get { return bankAddress; }
            set { bankAddress = value; }
        }
        private string bankNumber;

        /// <summary>
        /// //供应商银行卡号Provider_BankNumbersd
        /// </summary>
        public string BankNumber
        {
            get { return bankNumber; }
            set { bankNumber = value; }
        }
        private string dutyNumber;

        /// <summary>
        /// //Provider_DutyNumber
        /// </summary>
        public string DutyNumber
        {
            get { return dutyNumber; }
            set { dutyNumber = value; }
        }
        private string remark;

        /// <summary>
        /// //备注Provider_Memo
        /// </summary>
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
        private int status;

        /// <summary>
        /// //Provider_UnUsed状态
        /// </summary>
        public int Status
        {
            get { return status; }
            set { status = value; }
        }
        private string permissionMan;

        /// <summary>
        /// //权限人PermissionMan
        /// </summary>
        public string PermissionMan
        {
            get { return permissionMan; }
            set { permissionMan = value; }
        }
        private string operateIP;

        /// <summary>
        /// //操作者IPOperateIP
        /// </summary>
        public string OperateIP
        {
            get { return operateIP; }
            set { operateIP = value; }
        }
        private string operateNum;

        /// <summary>
        /// //操格者编号OperateBh
        /// </summary>
        public string OperateNum
        {
            get { return operateNum; }
            set { operateNum = value; }
        }

        public ProviderInfoModel() { }

        public ProviderInfoModel(int id) 
        {
            this.iD = id;
        
        }

    }
}
