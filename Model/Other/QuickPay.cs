using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;

using System.Text;

namespace Model.QuickPay
{
    public class quickPayParames
    {

        string phyPath = AppDomain.CurrentDomain.BaseDirectory;		
        private static string curPath = System.Configuration.ConfigurationManager.AppSettings["curUrl"].Trim();

        /// <summary>
        /// 当前项目访问页目录路径
        /// </summary>
        public string CurPath
        {
            get
            {
                return curPath;
            }
            set
            {
                curPath = value;
            }
        }
        public quickPayParames()
        {

        }

        #region  协议参数
        private string inputCharset = "1";
        /// <summary>
        ///  可为空 固定选择值：1、2、3    1 代表UTF-8; 2 代表GBK; 3 代表GB2312 ,默认值为1 
        /// </summary>
        public string InputCharset
        {
            get { return inputCharset; }
            set { inputCharset = value; }
        }
        private string pageUrl = curPath + "/receive.aspx";  //curPath+"/receive.aspx";
        /// <summary>
        /// 可为空  接 受支付结果的页面地址, 需要是绝对地址，与bgUrl 不能同时为空当 bgUrl 为空时，快钱直接将支付结果GET到pageUrl当 bgUrl 不为空时，按照bgUrl 的方式返回
        /// </summary>
        public string PageUrl
        {
            get { return pageUrl; }
            set { pageUrl = value; }
        }
        private string bgUrl = curPath + "/receive.aspx";
        /// <summary>
        /// 可为空  接 受支付结果的页面地址, 需要是绝对地址，与pageUrl 不能同时为空快钱将支付结果发送到bgUrl 对应的地址，并且获取商户按照约定格式输出的地址，显示页面给用户
        /// </summary>
        public string BgUrl
        {
            get { return bgUrl; }
            set { bgUrl = value; }
        }
        private string version = "v2.0";
        /// <summary>
        /// //不可空 网关版本 固定值：v2.0 注意为小写字母
        /// </summary>
        public string Version
        {
            get { return version; }
            set { version = value; }
        }
        private string language = "1";
        /// <summary>
        /// //不可空 网关页面显示语言种类,固定值：1,1 代表中文显示,
        /// </summary>    
        public string Language
        {
            get { return language; }
            set { language = value; }
        }
        private string signType = "4";
        /// <summary>
        /// 不可空 签名类型 固定值：1代表MD5签名 4代表 证书签名，默认证书
        /// </summary>
        public string SignType
        {
            get { return signType; }
            set { signType = value; }
        }

        #endregion

        #region  买卖双方信息参数
        private string merchantAcctId = string.Empty;//ml:1001874567501，PKI：1001154056101，MD5：1001153656201 TestKey:ZUZNJB8MF63GA83J
        /// <summary>
        /// 不可空 人民币账号 数字串,本参数用来指定接收款项的人民币账号,请登录快钱系统获取用户编号，用户编号后加01即为人民币网关账户号。
        /// </summary>
        public string MerchantAcctId
        {
            get { return merchantAcctId; }
            set { merchantAcctId = value; }
        }

        private string payerName = "payerName";
        /// <summary>
        /// 可为空 支付人姓名 英文或中文字符
        /// </summary>
        public string PayerName
        {
            get { return payerName; }
            set { payerName = value; }
        }

        private string payerContactType = "1";
        /// <summary>
        /// 可为空 支付人联系方式类型 固定值：1,	1 代表电子邮件方式
        /// </summary>
        public string PayerContactType
        {
            get { return payerContactType; }
            set { payerContactType = value; }
        }
        private string payerContact = "";
        /// <summary>
        /// 可为空 支付人联系方式    字符串,根据payerContactType 的方式填写对应字符
        /// </summary>
        public string PayerContact
        {
            get { return payerContact; }
            set { payerContact = value; }
        }
        #endregion


        #region 业务参数

        private string orderId = "";
        /// <summary>
        /// 不可空 50 商户订单号, 字符串只允许使用字母、数字、- 、_,并以字母或数字开头每商户提交的订单号，必须在自身账户交易中唯一
        /// </summary>
        public string OrderId
        {
            get { return orderId; }
            set { orderId = value; }
        }

        private int orderAmount = 0;
        /// <summary>
        /// 不可空 10 商户订单金额,整型数字,以分为单位。比方10 元，提交时金额应为1000
        /// </summary>
        public int OrderAmount
        {
            get { return orderAmount; }
            set { orderAmount = value; }
        }

        private string orderTime = DateTime.Now.ToString("yyyyMMddHHmmss");
        /// <summary>
        /// 不可空 ，默认当前时间 14 商户订单提交时间,数字串，一共14 位格式为：年[4 位]月[2 位]日[2 位]时[2 位]分[2 位]秒[2位]例如：20071117020101
        /// </summary>
        public string OrderTime
        {
            get { return orderTime; }
            set { orderTime = value; }
        }
        private string productName = "productName";
        /// <summary>
        /// 可为空 256 商品名称,英文或中文字符串
        /// </summary>
        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }
        private int productNum = 1;
        /// <summary>
        /// 可为空  8 商品数量,整型数字
        /// </summary>
        public int ProductNum
        {
            get { return productNum; }
            set { productNum = value; }
        }

        private string productId = "";
        /// <summary>
        /// 可为空 20 商品代码,字母、数字或- 、_ 的组合如商户发布了优惠券，并只想对指定的某商品或某类商品进行优惠时，请将此参数与发布优惠券时设置的
        /// “适用商品”保持一致。只可填写一个代码。如果不使用优惠券，本参数不用填写
        /// </summary>
        public string ProductId
        {
            get { return productId; }
            set { productId = value; }
        }
        private string productDesc = "";
        /// <summary>
        /// 可为空 400 商品描述,英文或中文字符串
        /// </summary>
        public string ProductDesc
        {
            get { return productDesc; }
            set { productDesc = value; }
        }

        private string ext1 = "";
        /// <summary>
        /// 可为空 128 扩展字段1 ,英文或中文字符串,	支付完成后，按照原样返回给商户，当前系统用于区分汇款类型，不可为空
        /// </summary>   
        public string Ext1
        {
            get { return ext1; }
            set { ext1 = value; }
        }

        private string ext2 = "";
        /// <summary>
        /// 可为空 128 扩展字段2 ,英文或中文字符串,	支付完成后，按照原样返回给商户
        /// </summary>
        public string Ext2
        {
            get { return ext2; }
            set { ext2 = value; }
        }

        private string payType = "00";
        /// <summary>
        /// 不可空 2   支付方式 固定选择值：00、10、11、12、13、14,00 代表显示快钱各支付方式列表；10 代表只显示银行卡支付方式；
        /// //11 代表只显示电话银行支付方式；12代表只显示快钱账户支付方式；13 代表只显示线下支
        /// </summary>
        public string PayType
        {
            get { return payType; }
            set { payType = value; }
        }

        private string bankId = "";
        /// <summary>
        /// //可为空 8   银行代码,可为空银行的代码，仅在银行直连时使用。银行代码表见参考资料**银行直连功能需单独申请，默认不开通。
        /// </summary>
        public string BankId
        {
            get { return bankId; }
            set { bankId = value; }
        }

        private string redoFlag = "0";
        /// <summary>
        ///  可为空 1   同一订单禁,止重复提交,标志,private string pageUrl="";固定选择值： 1、0,1 代表同一订单号只允许提交1 次；0 表示同一订单号在没有支付成功的前提下可重复提交多次。
        ///  //默认为0。建议实物购物车结算类商户采用0；虚拟产品类商户采用1；
        /// </summary>
        public string RedoFlag
        {
            get { return redoFlag; }
            set { redoFlag = value; }
        }
        private string pid = "";
        /// <summary>
        /// 可为空 30 合作伙伴在快钱的用户编号。数字串，用户登录快钱首页后可查询到。仅适用于快钱合作伙伴中系统及平台提供商。
        /// </summary>
        public string Pid
        {
            get { return pid; }
            set { pid = value; }
        }
        private string signMsg = "";
        /// <summary>
        /// 不可空 256 签名字符串，以上所有非空参数及其值与密钥组合，经MD5 加密生成并转化为大写的32 位字符串
        /// </summary>
        public string SignMsg
        {
            get
            {
                return signMsg;
            }
            set { signMsg = value; }
        }



        private string key =string .Empty;//ml:5W6R7MLAUGCQE5XS；TEST:ZUZNJB8MF63GA83J
        /// <summary>
        /// 人民币网关密钥，区分大小写.请与快钱联系索取
        /// </summary>
        public string Key
        {
            get { return key; }
            set{key=value;}
        }
        #endregion

        #region 快钱返回的交易参数
        private string dealId = "";
        /// <summary>
        /// 获取快钱交易号,		///获取该交易在快钱的交易号
        /// </summary>
        public string DealId
        {
            get { return dealId; }
            set { dealId = value; }
        }

        //
        ///
        private string bankDealId = "";

        /// <summary>
        /// 获取银行交易号,如果使用银行卡支付时，在银行的交易号。如不是通过银行支付，则为空
        /// </summary>
        public string BankDealId
        {
            get { return bankDealId; }
            set { bankDealId = value; }
        }


        private string dealTime = "";
        /// <summary>
        /// 获取在快钱交易时间,///14位数字。年[4位]月[2位]日[2位]时[2位]分[2位]秒[2位]
        /// 如；20080101010101
        /// </summary>
        public string DealTime
        {
            get { return dealTime; }
            set { dealTime = value; }
        }


        private int payAmount = 0;
        /// <summary>
        /// 获取实际支付金额	///单位为分		///比方 2 ，代表0.02元
        /// </summary>
        public int PayAmount
        {
            get { return payAmount; }
            set { payAmount = value; }
        }


        private int fee = 0;
        /// <summary>
        /// //获取交易手续费：	///单位为分	///比方 2 ，代表0.02元
        /// </summary>
        public int Fee
        {
            get { return fee; }
            set { fee = value; }
        }


        private string payResult = "11";
        /// <summary>
        /// 获取处理结果，10代表 成功; 11代表 失败
        /// </summary>
        public string PayResult
        {
            get { return payResult; }
            set { payResult = value; }
        }


        private string errCode = "";
        /// <summary>
        /// 获取返回的错误代码详细见文档错误代码列表
        /// </summary>
        public string ErrCode
        {
            get { return errCode; }
            set { errCode = value; }
        }

        #endregion  

        
    }

    public class BankEntity
    {
        public BankEntity()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 银行代码及对应银行名
        /// </summary>
        /// <returns></returns>
        public static ArrayList ALBankList()
        {
            ArrayList ar = new ArrayList();
            ListItem li = new ListItem("中国工商银行", "ICBC");
            ar.Add(li);
            li = new ListItem("中国建设银行", "CCB");
            ar.Add(li);
            li = new ListItem("中国农业银行", "ABC");
            ar.Add(li);
            li = new ListItem("中国银行", "CCB");
            ar.Add(li);
            li = new ListItem("深圳发展银行", "SDB");
            ar.Add(li);
            li = new ListItem("招商银行", "CMB");
            ar.Add(li);
            li = new ListItem("交通银行", "BCOM");
            ar.Add(li);
            li = new ListItem("华夏银行", "HXB");
            ar.Add(li);
            li = new ListItem("上海浦东发展银行", "SPDB");
            ar.Add(li);
            li = new ListItem("CMBC", "中国民生银行");
            ar.Add(li);
            li = new ListItem("广东发展银行", "GDB");
            ar.Add(li);
            li = new ListItem("中信银行", "CITIC");
            ar.Add(li);
            li = new ListItem("兴业银行", "CIB");
            ar.Add(li);
            li = new ListItem("广州市农村信用合作社", "GZRCC");
            ar.Add(li);
            li = new ListItem("广州市商业银行", "GZCB");
            ar.Add(li);
            li = new ListItem("中国光大银行", "CEB");
            ar.Add(li);
            li = new ListItem("渤海银行", "CBHB");
            ar.Add(li);
            li = new ListItem("北京银行", "BOB");
            ar.Add(li);
            li = new ListItem("北京农村商业银行", "BJRCB");
            ar.Add(li);
            li = new ListItem("上海农村商业银行", "SHRCC");
            ar.Add(li);
            li = new ListItem("宁波银行", "NBCB");
            ar.Add(li);
            li = new ListItem("南京银行", "NJCB");
            ar.Add(li);
            li = new ListItem("杭州银行", "HZB");
            ar.Add(li);
            li = new ListItem("平安银行", "PAB");
            ar.Add(li);
            li = new ListItem("东亚银行", "BEA");
            ar.Add(li);
            return ar;
        }
    }

   
}
