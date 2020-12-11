using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Branch
{
    public class BranchModel
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        private string number;
        /// <summary>
        /// 分公司编号
        /// </summary>
        public string Number
        {
            get { return number; }
            set { number = value; }
        }
        private string name;
        /// <summary>
        /// 分公司名称
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string forshort;
        /// <summary>
        /// 分公司简称
        /// </summary>
        public string Forshort
        {
            get { return forshort; }
            set { forshort = value; }
        }
        private string linkman;
        /// <summary>
        /// 负责人
        /// </summary>
        public string Linkman
        {
            get { return linkman; }
            set { linkman = value; }
        }
        private string mobile;
        /// <summary>
        /// 负责人手机
        /// </summary>
        public string Mobile
        {
            get { return mobile; }
            set { mobile = value; }
        }
        private string telephone;
        /// <summary>
        /// 分公司电话
        /// </summary>
        public string Telephone
        {
            get { return telephone; }
            set { telephone = value; }
        }
        private string fax;
        /// <summary>
        /// 分公司传真
        /// </summary>
        public string Fax
        {
            get { return fax; }
            set { fax = value; }
        }
        private string email;
        /// <summary>
        /// 分公司邮箱
        /// </summary>
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        private string url;
        /// <summary>
        /// 分公司网址
        /// </summary>
        public string Url
        {
            get { return url; }
            set { url = value; }
        }
        private string cpccode;
        /// <summary>
        /// 分公司地址国家省份城市
        /// </summary>
        public string Cpccode
        {
            get { return cpccode; }
            set { cpccode = value; }
        }
        private string address;
        /// <summary>
        /// 分公司详细地址
        /// </summary>
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        private string bankcode;
        /// <summary>
        /// 分公司银行代码
        /// </summary>
        public string Bankcode
        {
            get { return bankcode; }
            set { bankcode = value; }
        }
        private string bcpccode;
        /// <summary>
        /// 银行国家省份城市代码
        /// </summary>
        public string Bcpccode
        {
            get { return bcpccode; }
            set { bcpccode = value; }
        }
        private string bankaddress;
        /// <summary>
        /// 银行详细地址
        /// </summary>
        public string Bankaddress
        {
            get { return bankaddress; }
            set { bankaddress = value; }
        }
        private string bankuser;
        /// <summary>
        /// 开户名
        /// </summary>
        public string Bankuser
        {
            get { return bankuser; }
            set { bankuser = value; }
        }
        private string banknumber;
        /// <summary>
        /// 银行卡号
        /// </summary>
        public string Banknumber
        {
            get { return banknumber; }
            set { banknumber = value; }
        }
        private string remark;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
        private int status;
        /// <summary>
        /// 是否有效
        /// </summary>
        public string Status
        {
            get { return Status; }
            set { Status = value; }
        }
        private string permissionman;
        /// <summary>
        /// 权限
        /// </summary>
        public string Permissionman
        {
            get { return permissionman; }
            set { permissionman = value; }
        }
        private string operaterip;
        /// <summary>
        /// 操作者IP
        /// </summary>
        public string Operaterip
        {
            get { return operaterip; }
            set { operaterip = value; }
        }
        private string operatenum;
        /// <summary>
        /// 操作者编号
        /// </summary>
        public string Operatenum
        {
            get { return operatenum; }
            set { operatenum = value; }
        }
        private DateTime regdate;
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime Regdate
        {
            get { return regdate; }
            set { regdate = value; }
        }
    }
}
