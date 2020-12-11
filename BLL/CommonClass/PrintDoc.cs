using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.CommonClass
{
    /// <summary>
    /// DocPrint 的摘要说明。
    /// </summary>

    /// <summary>
    /// 文档打印类型枚举
    /// </summary>
    public enum DocPrintTypeList
    {
        /// <summary>
        /// 入库单
        /// </summary>
        RuKuDan = 0,
        /// <summary>
        /// 出库单
        /// </summary>
        ChuKuDan = 1,
        /// <summary>
        /// 送货单
        /// </summary>
        SongHuoDan = 2,
        /// <summary>
        /// 退货单
        /// </summary>
        TuiHuoDan = 3,
        /// <summary>
        /// 空
        /// </summary>
        None = -1
    }
    public class DocPrint
    {
        private DocPrintTypeList _docPrintType;//要打印的文档类型

        private int _docTypeID;//文档类型ID
        private int _depotID;//仓库ID
        private string _docState;//文档状态
        private string _docMaker;//文档开出人
        private string _docMakeTimeSelected;//是否选择文档开出时间搜索条件
        private string _docMakeTimeValue;//开出时间值
        private string _docAuditer;//文档审核人
        private string _docAuditTimeSelected;//是否选择文档审核时间搜索条件
        private string _docAuditTimeValue;//审核时间值


        #region DocPrint构造函数
        /// <summary>
        /// 单据打印构造函数
        /// </summary>
        public DocPrint()
        {
            this._docTypeID = -1;
            this._depotID = -1;
            this._docState = "";
            this._docMaker = "";
            this._docMakeTimeSelected = "-1";
            this._docMakeTimeValue = "";
            this._docAuditer = "";
            this._docAuditTimeSelected = "-1";
            this._docAuditTimeValue = "";
            this._docPrintType = DocPrintTypeList.None;
        }

        /// <summary>
        /// 单据打印构造函数
        /// </summary>
        /// <param name="docTypeID">文档类型ID</param>
        /// <param name="depotID">仓库ID</param>
        /// <param name="docState">文档状态</param>
        /// <param name="docMaker">文档开出人</param>
        /// <param name="docMakeTimeSelected">是否选择文档开出时间搜索条件</param>
        /// <param name="docMakeTimeValue">开出时间值</param>
        /// <param name="docAuditer">文档审核人</param>
        /// <param name="docAuditTimeValue">审核时间值</param>
        /// <param name="docAuditTimeSelected">是否选择文档审核时间搜索条件</param>
        /// <param name="docPrintType">文档打印类型</param>
        public DocPrint(int docTypeID, int depotID, string docState, string docMaker, string docMakeTimeSelected, string docMakeTimeValue, string docAuditer, string docAuditTimeValue, string docAuditTimeSelected, DocPrintTypeList docPrintType)
        {
            this._docTypeID = docTypeID;
            this._depotID = depotID;
            this._docState = docState;
            this._docMaker = docMaker;
            this._docMakeTimeSelected = docMakeTimeSelected;
            this._docMakeTimeValue = docMakeTimeValue;
            this._docAuditer = docAuditer;
            this._docAuditTimeSelected = docAuditTimeSelected;
            this._docAuditTimeValue = docAuditTimeValue;
            this._docPrintType = docPrintType;

        }

        /// <summary>
        /// 单据打印构造函数
        /// </summary>
        /// <param name="docTypeID">文档类型ID</param>
        /// <param name="depotID">仓库ID</param>
        /// <param name="docState">文档状态</param>
        /// <param name="docMaker">文档开出人</param>
        /// <param name="docMakeTimeSelected">是否选择文档开出时间搜索条件</param>
        /// <param name="docMakeTimeValue">开出时间值</param>
        /// <param name="docAuditer">文档审核人</param>
        /// <param name="docAuditTimeValue">审核时间值</param>
        /// <param name="docAuditTimeSelected">是否选择文档审核时间搜索条件</param>
        /// <param name="docPrintType">文档打印类型</param>
        public DocPrint(int docTypeID, int depotID, string docState, string docMaker, string docMakeTimeSelected, string docMakeTimeValue, string docAuditer, string docAuditTimeValue, string docAuditTimeSelected, int docPrintType)
        {
            this._docTypeID = docTypeID;
            this._depotID = depotID;
            this._docState = docState;
            this._docMaker = docMaker;
            this._docMakeTimeSelected = docMakeTimeSelected;
            this._docMakeTimeValue = docMakeTimeValue;
            this._docAuditer = docAuditer;
            this._docAuditTimeSelected = docAuditTimeSelected;
            this._docAuditTimeValue = docAuditTimeValue;
            this._docPrintType = (DocPrintTypeList)docPrintType;

        }

        #endregion

        #region 获取或设置各个属性值

        /// <summary>
        /// 要打印的文档类型枚举值
        /// </summary>
        public DocPrintTypeList DocPrintTypeEnum
        {
            get
            {
                return this._docPrintType;
            }
            set
            {
                this._docPrintType = value;
            }
        }

        /// <summary>
        /// 要打印的文档类型整型值
        /// </summary>
        public int DocPrintTypeInt
        {
            get
            {
                return (int)this._docPrintType;
            }
            set
            {
                this._docPrintType = (DocPrintTypeList)value;
            }
        }
        /// <summary>
        /// 获取或设置文档类型ID
        /// </summary>
        public int DocTypeID
        {
            get
            {
                return this._docTypeID;
            }
            set
            {
                this._docTypeID = value;
            }
        }

        /// <summary>
        /// 获取或设置仓库ID
        /// </summary>
        public int DepotID
        {
            get
            {
                return this._depotID;
            }
            set
            {
                this._depotID = value;
            }
        }

        /// <summary>
        /// 获取或设置文档状态
        /// </summary>
        public string DocState
        {
            get
            {
                return this._docState;
            }
            set
            {
                this._docState = value;
            }
        }

        /// <summary>
        /// 获取或设置文档开出人
        /// </summary>
        public string DocMaker
        {
            get
            {
                return this._docMaker;
            }
            set
            {
                this._docMaker = value;
            }
        }

        /// <summary>
        /// 获取或设置文档审核人
        /// </summary>
        public string DocAuditer
        {
            get
            {
                return this._docAuditer;
            }
            set
            {
                this._docAuditer = value;
            }
        }

        /// <summary>
        /// 获取或设置文档开出时间
        /// </summary>
        public string DocMakeTime
        {
            get
            {
                return this._docMakeTimeValue;
            }
            set
            {
                this._docMakeTimeValue = value;
            }
        }

        /// <summary>
        /// 获取或设置文档审核时间
        /// </summary>
        public string DocAuditTime
        {
            get
            {
                return this._docAuditTimeValue;
            }
            set
            {
                this._docAuditTimeValue = value;
            }
        }

        /// <summary>
        /// 获取或设置文档审核时间搜索条件是否选中
        /// </summary>
        public string DocAuditTimeSelected
        {
            get
            {
                return this._docAuditTimeSelected;
            }
            set
            {
                this._docAuditTimeSelected = value;
            }
        }

        /// <summary>
        /// 获取或设置文档开出时间搜索条件是否选中
        /// </summary>
        public string DocMakeTimeSelected
        {
            get
            {
                return this._docMakeTimeSelected;
            }
            set
            {
                this._docMakeTimeSelected = value;
            }
        }

        #endregion

        #region 产生sql语句
        /// <summary>
        /// 产生SQL语句
        /// </summary>
        /// <returns>SQL语句</returns>
        public string GetSqlString()
        {
            string sql = "select DocID from InventoryDoc where ID>0";

            if (this._docTypeID != (-1))
            {
                sql = sql + " and DocTypeID=" + this._docTypeID.ToString();
            }
            if (this._depotID != (-1))
            {
                sql = sql + " and WareHouseID=" + this._depotID.ToString();
            }
            if (this._docState.Trim() != "-1")
            {
                switch (this._docState.Trim())
                {
                    case "0":
                        sql = sql + " and StateFlag=0 and CloseFlag=0";
                        break;
                    case "1":
                        sql = sql + " and StateFlag=1";
                        break;
                    case "2":
                        sql = sql + " and CloseFlag=1";
                        break;
                    case "3":
                        sql = sql + " and CloseFlag='N'";
                        break;
                    default:
                        sql = sql;
                        break;

                }
            }

            if (this._docMaker.Trim() != "不限制" && this._docMaker != "")
            {
                sql = sql + " and DocMaker='" + this._docMaker.Trim() + "'";
            }

            if (this._docMakeTimeSelected != "-1")
            {
                sql = sql + " and DATEADD(day, DATEDIFF(day,0,DocMakeTime ), 0) " + this.ConvertToLogic(this._docMakeTimeSelected.Trim()) + " '" + this._docMakeTimeValue + "'";
            }

            if (this._docAuditer.Trim() != "不限制" && this._docAuditer != "")
            {
                sql = sql + " and DocAuditer='" + this._docAuditer.Trim() + "'";
            }

            if (this._docAuditTimeSelected.Trim() != "-1")
            {
                //sql=sql+" and DATEADD(day, DATEDIFF(day,0,dat_docAuditTime ), 0) "+this.ConvertToLogic (this._docAuditTimeSelected.Trim ())+" '"+this._docAuditTimeValue+"'";
            }

            //sql="select  pro.ProductName,sum(del.num_productQuantity) as num_productQuantity ,sum(del.num_pv) as num_pv,sum(del.num_productTotal) as num_productTotal from opda_depotManageDoc as opda , product as pro , opda_docDetails as del where opda.cha_docID=del.cha_docID and del.cha_productID=pro.ProductID and opda.cha_docID in ("+sql+") group by pro.ProductName";
            //System.Web .HttpContext .Current .Response .Write (sql);
            return sql;
        }
        #endregion

        #region 转换函数
        private string ConvertToLogic(string str)
        {
            switch (str)
            {
                case "&gt":
                    return ">";
                case "&lt":
                    return "<";
            }
            return "=";
        }

        #endregion
    }
}

