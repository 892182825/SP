using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * 创建者：     孙延昊
 * 创建时间：   2009年8月27日 
 * 修改者：     汪  华(4个构造函数)
 * 修改时间：   2009-09-09
 * 文件名：     InventoryDocModel
 * 功能：       库存单据表
 * **/
namespace Model
{
    /// <summary>
    /// 库存单据表
    /// </summary>
    public class InventoryDocModel
    {
        public InventoryDocModel() { }

        #region 构造函数：全部属性赋值
		public InventoryDocModel(

			int id, 
			int docTypeID, 
			string docID, 
			System.DateTime docMakeTime, 
			System.DateTime docAuditTime, 
			string docMaker, 
			string docAuditer, 
			int provider, 
			string client, 
			int wareHouseID, 
            int depotSeatID,
			double totalMoney, 
			double totalPV, 
			System.DateTime docSecondAuditTime, 
			string motherID, 
			int expectNum, 
			string cause, 
			string note)
		{

	
			this.iD =id; 
			this.docTypeID = docTypeID; 
			this.docID = docID; 
			this.docMakeTime = docMakeTime; 
			this.docAuditTime = docAuditTime; 
			this.docMaker = docMaker; 
			this.docAuditer = docAuditer; 
			this.provider = provider; 
			this.client = client; 
			this.wareHouseID = wareHouseID;
            this.depotSeatID = depotSeatID;
			this.totalMoney = totalMoney; 
			this.totalPV = totalPV; 
			this.docSecondAuditTime = docSecondAuditTime; 
			this.expectNum = expectNum; 
			this.note = note; 
		}

		#endregion 

		#region 构造函数：新增单据时单据构造方法
		/// <summary>
		/// 新增单据时单据构造方法
		/// </summary>
		/// <param name="docTypeID">单据类型ID </param>
		/// <param name="docID">单据编码</param>
		/// <param name="docMakeTime">制单时间</param>
		/// <param name="docMaker">制单人</param>
		/// <param name="provider">供货商</param>
		/// <param name="client">客户编码[店]</param>
		/// <param name="wareHouseID">可选，仓库[入库用，出库可空]</param>
		/// <param name="totalMoney">单据总值</param>
		/// <param name="num_totalPV">单据积分</param>
		/// <param name="cha_motherID">母单号</param>
		/// <param name="int_qishu">期数</param>
		/// <param name="vch_Cause">制单缘由</param>
		/// <param name="nvch_note">备注</param>
		/// <param name="StateFlag">有效无效Y/N</param>
		public InventoryDocModel(	int docTypeID,string docID,System.DateTime docMakeTime, string docMaker,
            int provider, string client, int wareHouseID, int depotSeatID, double totalMoney, double totalPV, 			
			string motherID, int expectNum, string cause, string note ,int stateFlag)
		{			
			this.docTypeID = docTypeID; 
			this.docID =docID; 
			this.docMakeTime = docMakeTime;			
			this.docMaker = docMaker; 			
			this.provider = provider; 
			this.client = client; 
			this.wareHouseID = wareHouseID;
            this.depotSeatID = depotSeatID;
			this.totalMoney = totalMoney; 
			this.totalPV = totalPV; 			
            this.expectNum = expectNum; 
			this.note = note; 
			this.stateFlag = stateFlag; 
		}
	#endregion

		#region 构造函数：新增单据时单据构造方法
		/// <summary>
		/// 新增单据时单据构造方法
		/// </summary>
		/// <param name="docTypeID">单据类型ID </param>
		/// <param name="docID">单据编码</param>
		/// <param name="docMakeTime">制单时间</param>
		/// <param name="docMaker">制单人</param>
		/// <param name="provider">供货商</param>
		/// <param name="client">客户编码[店]</param>
		/// <param name="wareHouseID">可选，仓库[入库用，出库可空]</param>
		/// <param name="totalMoney">单据总值</param>
		/// <param name="totalPV">单据积分</param>
		/// <param name="motherID">母单号</param>
		/// <param name="expectNum">期数</param>
		/// <param name="cause">制单缘由</param>
		/// <param name="note">备注</param>
		/// <param name="stateFlag">有效无效Y/N</param>
		/// <param name="currency">币种</param>
	public InventoryDocModel(	int docTypeID,string docID,System.DateTime docMakeTime, string docMaker, 			
			int provider, string client, int wareHouseID, int depotSeatID,double totalMoney, double totalPV, 			
			string motherID, int expectNum, string cause, string note ,int stateFlag,int currency)
		{			
			this.docTypeID = docTypeID; 
			this.docID = docID; 
			this.docMakeTime = docMakeTime;			
			this.docMaker = docMaker; 			
			this.provider = provider; 
			this.client = client; 
			this.wareHouseID = wareHouseID;
            this.depotSeatID = depotSeatID;
            this.totalMoney = totalMoney; 
			this.totalPV = totalPV; 			
			this.expectNum = expectNum; 
			this.note = note; 
			this.stateFlag = stateFlag; 
		}

        //wujinjian
    public InventoryDocModel(string DocAuditer,string OperateIP,DateTime DocAuditTime,string OperateNum,int docTypeID, string docID, System.DateTime docMakeTime, string docMaker,
        int provider, string client, int wareHouseID, int depotSeatID, double totalMoney, double totalPV,
        string motherID, int expectNum, string cause, string note, int stateFlag, int currency)
    {
        this.docAuditer = DocAuditer;
        this.operateIP = OperateIP;
        this.docAuditTime = DocAuditTime;
        this.operateNum = OperateNum;
        this.docTypeID = docTypeID;
        this.docID = docID;
        this.docMakeTime = docMakeTime;
        this.docMaker = docMaker;
        this.provider = provider;
        this.client = client;
        this.wareHouseID = wareHouseID;
        this.depotSeatID = depotSeatID;
        this.totalMoney = totalMoney;
        this.totalPV = totalPV;
        this.expectNum = expectNum;
        this.note = note;
        this.stateFlag = stateFlag;
    }

		#endregion

		#region 构造函数：新增入库单据时单据构造方法
		/// <summary>
		/// 新增单据时单据构造方法
		/// </summary>
		/// <param name="docTypeID">单据类型ID </param>
		/// <param name="docID">单据编码</param>
		/// <param name="docMakeTime">制单时间</param>
		/// <param name="docMaker">制单人</param>
		/// <param name="provider">供货商</param>
		/// <param name="client">客户编码[店]</param>
		/// <param name="wareHouseID">可选，仓库[入库用，出库可空]</param>
		/// <param name="totalMoney">单据总值</param>
		/// <param name="totalPV">单据积分</param>
		/// <param name="motherID">母单号</param>
		/// <param name="expectNum">期数</param>
		/// <param name="cause">制单缘由</param>
		/// <param name="note">备注</param>
		/// <param name="stateFlag">有效无效Y/N</param>
		/// <param name="batchCode">批次</param>
		/// <param name="operationPerson">业务员</param>
		/// <param name="address">收货地址</param>
        public InventoryDocModel(int docTypeID, string docID, System.DateTime docMakeTime, string docMaker, 			
			int provider, string client, int wareHouseID,int depotSeatID, double totalMoney, double totalPV, 			
			string motherID, int expectNum, string cause, string note ,int stateFlag,
			string batchCode ,string operationPerson)
		{			
			this.docTypeID = docTypeID; 
			this.docID = docID; 
			this.docMakeTime = docMakeTime;			
			this.docMaker = docMaker;
            this.provider = provider; 
			this.client =client; 
			this.wareHouseID = wareHouseID;
            this.depotSeatID = depotSeatID;
			this.totalMoney =totalMoney; 
			this.totalPV = totalPV; 			
			this.expectNum = expectNum; 
			this.note = note; 
			this.stateFlag = stateFlag; 
			this.batchCode =batchCode;
			this.operationPerson =operationPerson ;
		}
		#endregion

        #region 构造函数：新增单据时单据构造方法
		/// <summary>
		/// 新增单据时单据构造方法
		/// </summary>
		/// <param name="int_docTypeID">单据类型ID </param>
		/// <param name="cha_docID">单据编码</param>
		/// <param name="dat_docMakeTime">制单时间</param>
		/// <param name="cha_docMaker">制单人</param>
		/// <param name="vch_provider">供货商</param>
		/// <param name="cha_client">客户编码[店]</param>
		/// <param name="warehouseID">可选，仓库[入库用，出库可空]</param>
		/// <param name="num_totalMoney">单据总值</param>
		/// <param name="num_totalPV">单据积分</param>
		/// <param name="cha_motherID">母单号</param>
		/// <param name="int_qishu">期数</param>
		/// <param name="vch_Cause">制单缘由</param>
		/// <param name="nvch_note">备注</param>
		/// <param name="StateFlag">有效无效Y/N</param>
		/// <param name="Currency">币种</param>
        public InventoryDocModel(/*int ID,*/ string DocID, System.DateTime DocMakeTime, string DocMaker, 			
			int Provider, string Client, int WareHouseID, double TotalMoney, double TotalPV, 			
			string MotherID, int ExpectNum, string Cause, string Note ,int StateFlag,int Currency)
		{			
			//this.iD = ID; 
			this.docID = DocID; 
			this.docMakeTime = DocMakeTime;			
			this.docMaker = DocMaker; 			
			this.provider = Provider; 
			this.client = Client; 
			this.wareHouseID = WareHouseID; 
			this.totalMoney = TotalMoney; 
			this.totalPV = TotalPV; 			
			this.expectNum = ExpectNum; 
			this.note = Note; 
			this.stateFlag = StateFlag; 
		}
		#endregion

        public InventoryDocModel(int id)
        {
            this.iD = id;
        }

        private int iD;

        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        private string docID;

        /// <summary>
        /// 单据编号
        /// </summary>
        public string DocID
        {
            get { return docID; }
            set { docID = value; }
        }
        
        private int docTypeID;
        /// <summary>
        /// 单据类型，由数字区分
        /// </summary>
        public int DocTypeID
        {
            get { return docTypeID; }
            set { docTypeID = value; }
        }
        private DateTime docMakeTime;
        /// <summary>
        /// 单据开出时间
        /// </summary>
        public DateTime DocMakeTime
        {
            get { return docMakeTime; }
            set { docMakeTime = value; }
        }
        private DateTime docAuditTime;
        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime DocAuditTime
        {
            get { return docAuditTime; }
            set { docAuditTime = value; }
        }
        private string docMaker;
        /// <summary>
        /// 单据开出人
        /// </summary>
        public string DocMaker
        {
            get { return docMaker; }
            set { docMaker = value; }
        }
        private string docAuditer;
        /// <summary>
        /// 单据审核人
        /// </summary>
        public string DocAuditer
        {
            get { return docAuditer; }
            set { docAuditer = value; }
        }

        private int provider;
        /// <summary>
        /// 供应商/源仓库
        /// </summary>
        public int Provider
        {
            get { return provider; }
            set { provider = value; }
        }

        private string client;
        /// <summary>
        /// 店铺编号
        /// </summary>
        public string Client
        {
            get { return client; }
            set { client = value; }
        }
        private int depotSeatID;

        public int DepotSeatID
        {
            get { return depotSeatID; }
            set { depotSeatID = value; }
        }
        private int wareHouseID;
        /// <summary>
        /// 目的仓库ID
        /// </summary>
        public int WareHouseID
        {
            get { return wareHouseID; }
            set { wareHouseID = value; }
        }
        private double totalMoney;
        /// <summary>
        /// 总钱数
        /// </summary>
        public double TotalMoney
        {
            get { return totalMoney; }
            set { totalMoney = value; }
        }
        private double totalPV;
        /// <summary>
        /// 总PV
        /// </summary>
        public double TotalPV
        {
            get { return totalPV; }
            set { totalPV = value; }
        }

        private DateTime docSecondAuditTime;
        /// <summary>
        /// 复核时间
        /// </summary>
        public DateTime DocSecondAuditTime
        {
            get { return docSecondAuditTime; }
            set { docSecondAuditTime = value; }
        }

        private int expectNum;
        /// <summary>
        /// 期数
        /// </summary>
        public int ExpectNum
        {
            get { return expectNum; }
            set { expectNum = value; }
        }
        
        private string note;
        /// <summary>
        /// 备注
        /// </summary>
        public string Note
        {
            get { return note; }
            set { note = value; }
        }
        private int stateFlag;
        /// <summary>
        /// 单据状态：有没有效
        /// </summary>
        public int StateFlag
        {
            get { return stateFlag; }
            set { stateFlag = value; }
        }
        private int closeFlag;
        /// <summary>
        /// 关闭此单据
        /// </summary>
        public int CloseFlag
        {
            get { return closeFlag; }
            set { closeFlag = value; }
        }
        private DateTime closeDate;
        /// <summary>
        /// 关闭单据日期
        /// </summary>
        public DateTime CloseDate
        {
            get { return closeDate; }
            set { closeDate = value; }
        }
        private string batchCode;
        /// <summary>
        /// 入库批次
        /// </summary>
        public string BatchCode
        {
            get { return batchCode; }
            set { batchCode = value; }
        }
        private string operationPerson;
        /// <summary>
        /// 业务员
        /// </summary>
        public string OperationPerson
        {
            get { return operationPerson; }
            set { operationPerson = value; }
        }
        private string originalDocID;
        /// <summary>
        /// 原始单据号
        /// </summary>
        public string OriginalDocID
        {
            get { return originalDocID; }
            set { originalDocID = value; }
        }
        private string address;
        /// <summary>
        /// 收货地
        /// </summary>
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        private int flag;
        /// <summary>
        /// 退款标识
        /// </summary>
        public int Flag
        {
            get { return flag; }
            set { flag = value; }
        }
        private decimal charged;
        /// <summary>
        /// 退货扣款额
        /// </summary>
        public decimal Charged
        {
            get { return charged; }
            set { charged = value; }
        }
        private string reason;
        /// <summary>
        /// 退货扣款原因
        /// </summary>
        public string Reason
        {
            get { return reason; }
            set { reason = value; }
        }
        private string operateIP;
        /// <summary>
        /// 操作人IP
        /// </summary>
        public string OperateIP
        {
            get { return operateIP; }
            set { operateIP = value; }
        }
        private string operateNum;
        /// <summary>
        /// 操作人编号
        /// </summary>
        public string OperateNum
        {
            get { return operateNum; }
            set { operateNum = value; }
        }

        private int inWareHouseID;

        public int InWareHouseID
        {
            get { return inWareHouseID; }
            set { inWareHouseID = value; }
        }

        private int inDepotSeatID;

        public int InDepotSeatID
        {
            get { return inDepotSeatID; }
            set { inDepotSeatID = value; }
        }

        public string Storeorderid { get; set; }
    }
}
