using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.MoneyFlows
{
    public class PayParames
    {
        public PayParames()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /*
         * 
         淘宝校验码：ko1erlo012544jip5c7saw1db4i1dkw3
pid：2088002358746856
用户名：沈一雄
注册邮箱：ppljsyx@163.com
         * */

        private string partnerid = "2088002358746856";   // pxw: 2088102648818093  // zq:2088002358746856
        /// <summary>
        /// 合作者身份ID
        /// </summary>
        public string PartnerID
        {
            get
            {
                return partnerid;
            }
        }

        private string key = "ko1erlo012544jip5c7saw1db4i1dkw3";  //pxw:z6rmc9eshh1qjul7v16qnspryfcd7fwa  //zq:ko1erlo012544jip5c7saw1db4i1dkw3
        /// <summary>
        /// 交易安全校验码
        /// </summary>
        public string Key
        {
            get
            {
                return key;
            }
        }

        private string seller_email = "pay_1688@yahoo.com.cn";//"ppljsyx@163.com";     // pxw:izuoankafei@163.com     //zq:ppljsyx@163.com
        /// <summary>
        /// 卖家帐号
        /// </summary>
        public string Seller_Email
        {
            get
            {
                return seller_email;
            }
        }
    }
}
