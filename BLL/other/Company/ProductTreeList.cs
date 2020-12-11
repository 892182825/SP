using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Model;
using DAL;
using BLL.CommonClass;
using System.Web.UI.WebControls;
/*
 *修改者：   汪  华(GetCustomersDataPage_Sort)
 *修改时间： 2009-10-04
 *修改者：   汪  华
 *修改时间： 2009-10-23 
 */

namespace BLL.other.Company
{
    public class ProductTreeList
    {
        CommonDataBLL commonDataBLL = new CommonDataBLL();
        #region 得到指定页码数据
        /// <summary>
        /// 得到指定页码数据
        /// </summary>
        /// <param name="PageIndex">页码</param>
        /// <param name="PageSize">页记录数</param>
        /// <param name="table">表名</param>
        ///<param name="columns">列</param>
        /// <param name="condition">条件</param>
        /// <param name="key">关键字</param>
        /// <param name="RecordCount">总记录数</param>
        ///<param name="PageCount">页数</param>
        /// <returns></returns>
        public DataTable GetDataTablePage_SmsBll(int PageIndex, int PageSize, string table, string columns, string condition, string key, out int RecordCount, out int PageCount)
        {
            return CommonDataBLL.GetDataTablePage_Sms(PageIndex, PageSize, table, columns, condition, key, out RecordCount, out PageCount);
        }

        public DataTable GetDataTablePage_SmsBllgroup(int PageIndex, int PageSize, string table, string columns, string condition, string key, string groupby,out int RecordCount, out int PageCount)
        {
            return CommonDataBLL.GetDataTablePage_Smsgroup(PageIndex, PageSize, table, columns, condition, key, groupby, out RecordCount, out PageCount);
        }
        public DataTable GetDataTablePage_SmsBll1(int PageIndex, int PageSize, string table, string columns, string condition, string key, out int RecordCount, out int PageCount)
        {
            return CommonDataBLL.GetDataTablePage_Sms1(PageIndex, PageSize, table, columns, condition, key, out RecordCount, out PageCount);
        }

        /// <summary>
        /// 分页（按照指定的条件排序后，再进行分页）（汪华）
        /// </summary>
        /// <param name="PageIndex">起始页索引</param>
        /// <param name="PageSize">每页大小</param>
        /// <param name="table">表名</param>
        /// <param name="columns">列名</param>
        /// <param name="condition">条件</param>
        /// <param name="key">排序关键字</param>
        /// <param name="ascOrDesc">按升序排还是按降序排</param>
        /// <param name="RecordCount">页数记录</param>
        /// <param name="PageCount"></param>
        /// <returns>页数总数</returns>
        public DataTable GetCustomersDataPage_Sort(int PageIndex, int PageSize, string table, string columns, string condition, string key, bool ascOrDesc, out int RecordCount, out int PageCount)
        {
            return CommonDataBLL.GetCustomersDataPage_Sort(PageIndex, PageSize, table, columns, condition, key, ascOrDesc, out RecordCount, out PageCount);
        }

        /// <summary>
        /// 分页（汪华2009-10-23完成）（SQL Server 2005 高效分页）
        /// </summary>
        /// <param name="pageIndex">指定当前为第几页</param>
        /// <param name="pageSize">每页多少条记录</param>
        /// <param name="tableNames">表名</param>
        /// <param name="columnNames">列名</param>
        /// <param name="conditions">查询条件</param>
        /// <param name="orderColumnName">排序字段(支持多字段)</param>
        /// <param name="totalRecord">返回总记录数</param>
        /// <param name="totalPage">返回总页数</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable PageByWangHua(int pageIndex, int pageSize, string tableNames, string columnNames, string conditions, string orderColumnName, out int totalRecord, out int totalPage)
        {
            return CommonDataBLL.PageByWangHua(pageIndex,pageSize,tableNames,columnNames,conditions,orderColumnName, out totalRecord, out totalPage);
        }

        #endregion
        #region 获取产品树
        /// <summary>
        /// 获取产品树
        /// </summary>
        /// <returns></returns>
        public DataTable GetProductTree(int countryID)
        { 
            ProductDAL product=new ProductDAL ();
            return product.GetProductTree(countryID);
        }
        #endregion

        #region
        //初始化产品映射表
//        private void InitPTable()
//        {
//            this.pListTable = new DataTable("ipTable");

//            DataColumn myDataColumn;

//            myDataColumn = new DataColumn();
//            myDataColumn.DataType = System.Type.GetType("System.Int32");
//            myDataColumn.ColumnName = "ID";
//            this.pListTable.Columns.Add(myDataColumn);

//            myDataColumn = new DataColumn();
//            myDataColumn.DataType = Type.GetType("System.Decimal");
//            myDataColumn.ColumnName = "price";
//            this.pListTable.Columns.Add(myDataColumn);

//            myDataColumn = new DataColumn();
//            myDataColumn.DataType = Type.GetType("System.String");
//            myDataColumn.ColumnName = "name";
//            this.pListTable.Columns.Add(myDataColumn);

//            myDataColumn = new DataColumn();
//            myDataColumn.DataType = Type.GetType("System.Int32");
//            myDataColumn.ColumnName = "num";
//            myDataColumn.DefaultValue = 0;
//            this.pListTable.Columns.Add(myDataColumn);

//            myDataColumn = new DataColumn();
//            myDataColumn.DataType = Type.GetType("System.String");
//            myDataColumn.ColumnName = "productcode";
//            this.pListTable.Columns.Add(myDataColumn);
//        }
//        /// <summary>
//        ///读取所有产品数据
//        /// </summary>
//        private void GetData(string storeid)
//        {
//            pTable = orderGoodsBLL.GetAllProducts(storeid);
//        }
//        /// <summary>
//        /// 店铺在线订货时生成产品树形菜单
//        /// </summary>
//        /// <param name="storeid"></param>
//        /// <param name="ZT"></param>
//        /// <returns></returns>
//        public string getMenuOrder(string storeid, int ZT)
//        {
//            this.InitPTable();
//            this.GetData(storeid);
//            if (pTable.Rows.Count != 0)
//            {
//                this.menuBuilder.Append("<div>" + commonDataBLL.GetClassTran("classes/ProductTree.cs_123", "产品列表") + "&nbsp;&nbsp;<div>");


//                this.MakePOrder(1, "", storeid, ZT);
//            }
//            else
//            {
//                this.menuBuilder.Append("<div>" + commonDataBLL.GetClassTran("classes/producttree.cs_128[1]", "产品列表") + "&nbsp;&nbsp;" + CommonDataBLL.GetClassTran("classes/producttree.cs_110[2]", "产品库中无记录！") + "<div>");
//            }

//            return this.menuBuilder.ToString();
//        }
//        /// <summary>
//        /// 店铺定货时产生非修改菜单
//        /// </summary>
////        private void MakePOrder(int pID, string pLayer, string storeid, int ZT)
////        {
////            int i;
////            DataRow[] myRows = this.pTable.Select("pID=" + pID.ToString(), "ProductID");
////            if (myRows.Length == 0) return;
////            for (i = 0; i < myRows.Length; i++)
////            {
////                this.mID++;
////                if ((int)myRows[i]["isFold"] == 0)
////                {
////                    DataRow myRow = this.pListTable.NewRow();
////                    myRow["ID"] = (int)myRows[i]["ProductID"];
////                    myRow["price"] = (decimal)myRows[i]["PreferentialPrice"];
////                    myRow["name"] = myRows[i]["ProductName"].ToString();
////                    this.pListTable.Rows.Add(myRow);

////                    string Sql = @"select isNull((-(ActualStorage+HasOrderCount)),0) as Quantity from D_kucun 
////								where storeid='" + storeid + "' and  ProductID=" + myRows[i]["ProductID"] + " and (ActualStorage+HasOrderCount)<0 ";
////                    DataTable dt = DBHelper.ExecuteDataTable(Sql);
////                    int Quantity = 0;
////                    if (dt.Rows.Count > 0)
////                    {
////                        if (ZT == 1)
////                        {
////                            Quantity = Convert.ToInt32(dt.Rows[0]["Quantity"].ToString());
////                        }
////                        else
////                        {
////                            Quantity = 0;
////                        }
////                    }
////                    else
////                    {
////                        Quantity = 0;
////                    }
////                    if (i == myRows.Length - 1)
////                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle src=" + this.imgLine2 + "><img align=absmiddle src=" + this.imgItem + ">");
////                    else
////                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle src=" + this.imgLine3 + "><img align=absmiddle src=" + this.imgItem + ">");

////                    this.menuBuilder.Append("<input maxLength=6 value=" + Quantity + " type=text class=priceBox  size=3 name=N" + myRows[i]["ProductID"] + "></input>");

////                    this.menuBuilder.Append("\n\t    " + myRows[i]["ProductName"].ToString() + " <font color=#669900>会员价：" + (Convert.ToDouble(myRows[i]["PreferentialPrice"].ToString())).ToString("f2") + "</font>\n");
////                    this.menuBuilder.Append("\n<a href=\"javascript:void(window.open('ProductIntro.aspx?ProductID=" + myRows[i]["ProductID"].ToString() + "','','width=450,height=240')) \">" + CommonDataBLL.GetClassTran("classes/producttree.cs_167", "查看") + "</a><br>\n");
////                }
////                else
////                {
////                    this.menuBuilder.Append("\n<div>");

////                    if (i == myRows.Length - 1)
////                    {
////                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=" + this.imgPlus2 + " class='menutop'>");

////                        this.menuBuilder.Append("<img align=absmiddle id=img" + this.mID + " src=" + this.imgFoldClose + ">" + myRows[i]["ProductName"].ToString() + "\n");

////                        this.menuBuilder.Append("\n</div>");

////                        this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='display:none;margin-top=-3'>");

////                        MakePOrder((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgEmpty + ">", storeid, ZT);

////                        this.menuBuilder.Append("\n</div>");

////                    }
////                    else
////                    {
////                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=" + this.imgPlus3 + " class='menutop'>");

////                        this.menuBuilder.Append("<img align=absmiddle id=img" + this.mID + " src=" + this.imgFoldClose + ">" + myRows[i]["ProductName"].ToString() + "\n");

////                        this.menuBuilder.Append("\n</div>");

////                        this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='display:none;margin-top=-3'>");

////                        MakePOrder((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgLine1 + ">", storeid, ZT);

////                        this.menuBuilder.Append("\n</div>");
////                    }
////                }
////            }
////        }

//        //初始化产品映射表
//        private void initPTable()
//        {
//            this.pListTable = new DataTable("ipTable");

//            DataColumn myDataColumn;

//            myDataColumn = new DataColumn();
//            myDataColumn.DataType = System.Type.GetType("System.Int32");
//            myDataColumn.ColumnName = "ID";
//            this.pListTable.Columns.Add(myDataColumn);

//            myDataColumn = new DataColumn();
//            myDataColumn.DataType = Type.GetType("System.Decimal");
//            myDataColumn.ColumnName = "price";
//            this.pListTable.Columns.Add(myDataColumn);

//            myDataColumn = new DataColumn();
//            myDataColumn.DataType = Type.GetType("System.String");
//            myDataColumn.ColumnName = "name";
//            this.pListTable.Columns.Add(myDataColumn);

//            myDataColumn = new DataColumn();
//            myDataColumn.DataType = Type.GetType("System.Int32");
//            myDataColumn.ColumnName = "num";
//            myDataColumn.DefaultValue = 0;
//            this.pListTable.Columns.Add(myDataColumn);

//            myDataColumn = new DataColumn();
//            myDataColumn.DataType = Type.GetType("System.String");
//            myDataColumn.ColumnName = "productcode";
//            this.pListTable.Columns.Add(myDataColumn);
//        }


//        //返回非修改菜单--------------------
//        public string getMenu()
//        {
//            this.initPTable();

//            this.getData();

//            if (pTable.Rows.Count != 0)
//            {
//                this.menuBuilder.Append("<div>" + commonDataBLL.GetClassTran("classes/producttree.cs_105", "产品列表") + "&nbsp;&nbsp;<div>");
//                this.makeP(1, "");
//            }
//            else
//            {
//                this.menuBuilder.Append("<div>" + commonDataBLL.GetClassTran("classes/producttree.cs_128[1]", "产品列表") + "&nbsp;&nbsp;" + CommonDataBLL.GetClassTran("classes/producttree.cs_110[2]", "产品库中无记录！") + "<div>");
//            }

//            return this.menuBuilder.ToString();
//        }
//        //返回非修改菜单组合产品用--------------------
//        public string getMenuZhuHe()
//        {
//            this.initPTable();

//            this.getData();

//            if (pTable.Rows.Count != 0)
//            {
//                this.menuBuilder.Append("<div>" + commonDataBLL.GetClassTran("producttree.cs_105", "产品列表") + "&nbsp;&nbsp;<div>");
//                this.makePZhuHe(1, "");
//            }
//            else
//            {
//                this.menuBuilder.Append("<div>" + commonDataBLL.GetClassTran("classes/producttree.cs_128[1]", "产品列表") + "&nbsp;&nbsp;" + CommonDataBLL.GetClassTran("classes/producttree.cs_110[2]", "产品库中无记录！") + "<div>");
//            }

//            return this.menuBuilder.ToString();
//        }
//        public string getMenu(string dian, string Currency)
//        {
//            this.initPTable();

//            this.getData(dian, Currency);

//            if (pTable.Rows.Count != 0)
//            {
//                this.menuBuilder.Append("<div>" + CommonDataBLL.GetClassTran("classes/producttree.cs_105", "产品列表") + "&nbsp;&nbsp;<div>");
//                this.makeP(1, "");
//            }
//            else
//            {
//                this.menuBuilder.Append("<div>" + CommonDataBLL.GetClassTran("classes/producttree.cs_128[1]", "产品列表") + "&nbsp;&nbsp;" + CommonDataBLL.GetClassTran("classes/producttree.cs_110[2]", "产品库中无记录！") + "<div>");
//            }

//            return this.menuBuilder.ToString();
//        }


//        public string getMenu(string dian)
//        {
//            this.initPTable();

//            this.getData(dian);

//            if (pTable.Rows.Count != 0)
//            {
//                this.menuBuilder.Append("<div>" + CommonDataBLL.GetClassTran("classes/producttree.cs_123", "产品列表") + "&nbsp;&nbsp;<div>");
//                this.makeP(1, "");
//            }
//            else
//            {
//                this.menuBuilder.Append("<div>" + CommonDataBLL.GetClassTran("classes/producttree.cs_128[1]", "产品列表") + "&nbsp;&nbsp;" + CommonDataBLL.GetClassTran("classes/producttree.cs_110[2]", "产品库中无记录！") + "<div>");
//            }

//            return this.menuBuilder.ToString();
//        }

//        //
//        //产生非修改菜单组合产品用
//        private void makePZhuHe(int pID, string pLayer)
//        {
//            int i;

//            DataRow[] myRows = this.pTable.Select("pID=" + pID.ToString(), "ProductID");

//            if (myRows.Length == 0) return;


//            for (i = 0; i < myRows.Length; i++)
//            {
//                this.mID++;

//                if ((int)myRows[i]["isFold"] == 0)
//                {
//                    /*PID, isFold , ProductID , ProductName , CommonPrice , CommonPV , PreferentialPrice , PreferentialPV, */

//                    DataRow myRow = this.pListTable.NewRow();
//                    myRow["ID"] = (int)myRows[i]["ProductID"];
//                    myRow["price"] = (decimal)myRows[i]["PreferentialPrice"];
//                    myRow["name"] = myRows[i]["ProductName"].ToString();
//                    myRow["productcode"] = myRows[i]["productcode"].ToString();
//                    this.pListTable.Rows.Add(myRow);

//                    if (i == myRows.Length - 1)
//                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle src=../" + this.imgLine2 + "><img align=absmiddle src=../" + this.imgItem + ">");
//                    else
//                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle src=../" + this.imgLine3 + "><img align=absmiddle src=../" + this.imgItem + ">");

//                    this.menuBuilder.Append("\n\t  " + myRows[i]["productcode"].ToString() + " \n");
//                    this.menuBuilder.Append("\n\t    " + myRows[i]["ProductName"].ToString() + " <font color=#669900> " + (Convert.ToDouble(myRows[i]["PreferentialPrice"].ToString())).ToString("f2") + "</font><br>\n");



//                }
//                else
//                {
//                    this.menuBuilder.Append("\n<div>");

//                    if (i == myRows.Length - 1)
//                    {
//                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=../" + this.imgPlus2 + " class='menutop'>");

//                        this.menuBuilder.Append("<img align=absmiddle id=img" + this.mID + " src=../" + this.imgFoldClose + ">" + myRows[i]["ProductName"].ToString() + "\n");

//                        this.menuBuilder.Append("\n</div>");

//                        this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='display:none;margin-top=-3'>");

//                        makePZhuHe((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=../" + this.imgEmpty + ">");

//                        this.menuBuilder.Append("\n</div>");

//                    }
//                    else
//                    {
//                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=../" + this.imgPlus3 + " class='menutop'>");

//                        this.menuBuilder.Append("<img align=absmiddle id=img" + this.mID + " src=../" + this.imgFoldClose + ">" + myRows[i]["ProductName"].ToString() + "\n");

//                        this.menuBuilder.Append("\n</div>");

//                        this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='display:none;margin-top=-3'>");

//                        makePZhuHe((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=../" + this.imgLine1 + ">");

//                        this.menuBuilder.Append("\n</div>");
//                    }


//                }

//            }


//        }


//        //
//        //店铺定货时产生非修改菜单
//        private void makePOrder(int pID, string pLayer, string dian, int ZT)
//        {
//            int i;

//            DataRow[] myRows = this.pTable.Select("pID=" + pID.ToString(), "ProductID");

//            if (myRows.Length == 0) return;


//            for (i = 0; i < myRows.Length; i++)
//            {
//                this.mID++;

//                if ((int)myRows[i]["isFold"] == 0)
//                {
//                    /*PID, isFold , ProductID , ProductName , CommonPrice , CommonPV , PreferentialPrice , PreferentialPV, */

//                    DataRow myRow = this.pListTable.NewRow();
//                    myRow["ID"] = (int)myRows[i]["ProductID"];
//                    myRow["price"] = (decimal)myRows[i]["PreferentialPrice"];
//                    myRow["name"] = myRows[i]["ProductName"].ToString();
//                    this.pListTable.Rows.Add(myRow);

//                    string Sql = @"select isNull((-(ActualStorage+HasOrderCount)),0) as Quantity from D_kucun 
//								where storeid='" + dian + "' and  ProductID=" + myRows[i]["ProductID"] + " and (ActualStorage+HasOrderCount)<0 ";
//                    DataTable dt = DBHelper.ExecuteDataTable(Sql);
//                    int Quantity = 0;
//                    if (dt.Rows.Count > 0)
//                    {
//                        if (ZT == 1)
//                        {
//                            Quantity = Convert.ToInt32(dt.Rows[0]["Quantity"].ToString());
//                        }
//                        else
//                        {
//                            Quantity = 0;
//                        }
//                    }
//                    else
//                    {
//                        Quantity = 0;
//                    }


//                    if (i == myRows.Length - 1)
//                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle src=" + this.imgLine2 + "><img align=absmiddle src=" + this.imgItem + ">");
//                    else
//                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle src=" + this.imgLine3 + "><img align=absmiddle src=" + this.imgItem + ">");

//                    this.menuBuilder.Append("<input maxLength=6 value=" + Quantity + " type=text class=priceBox  size=3 name=N" + myRows[i]["ProductID"] + "></input>");

//                    this.menuBuilder.Append("\n\t    " + myRows[i]["ProductName"].ToString() + " <font color=#669900>会员价：" + (Convert.ToDouble(myRows[i]["PreferentialPrice"].ToString())).ToString("f2") + "</font>\n");


//                    this.menuBuilder.Append("\n<a href=\"javascript:void(window.open('ProductIntro.aspx?ProductID=" + myRows[i]["ProductID"].ToString() + "','','width=450,height=240')) \">" + CommonDataBLL.GetClassTran("classes/producttree.cs_167", "查看") + "</a><br>\n");
//                    //						myRows[i]["ProductName"].ToString() + "\\n普通价："  + 						
//                    //						myRows[i]["CommonPrice"].ToString() + " 积分：" +
//                    //						myRows[i]["CommonPV"].ToString() + "\\n会员价：" +
//                    //						myRows[i]["PreferentialPrice"].ToString() + " 积分：" +  						
//                    //						myRows[i]["PreferentialPV"].ToString() + "')\" >查看</a><br>\n" );

//                }
//                else
//                {
//                    this.menuBuilder.Append("\n<div>");

//                    if (i == myRows.Length - 1)
//                    {
//                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=" + this.imgPlus2 + " class='menutop'>");

//                        this.menuBuilder.Append("<img align=absmiddle id=img" + this.mID + " src=" + this.imgFoldClose + ">" + myRows[i]["ProductName"].ToString() + "\n");

//                        this.menuBuilder.Append("\n</div>");

//                        this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='display:none;margin-top=-3'>");

//                        makePOrder((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgEmpty + ">", dian, ZT);

//                        this.menuBuilder.Append("\n</div>");

//                    }
//                    else
//                    {
//                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=" + this.imgPlus3 + " class='menutop'>");

//                        this.menuBuilder.Append("<img align=absmiddle id=img" + this.mID + " src=" + this.imgFoldClose + ">" + myRows[i]["ProductName"].ToString() + "\n");

//                        this.menuBuilder.Append("\n</div>");

//                        this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='display:none;margin-top=-3'>");

//                        makePOrder((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgLine1 + ">", dian, ZT);

//                        this.menuBuilder.Append("\n</div>");
//                    }


//                }

//            }


//        }

//        //产生非修改菜单
//        private void makeP(int pID, string pLayer)
//        {
//            int i;

//            DataRow[] myRows = this.pTable.Select("pID=" + pID.ToString(), "ProductID");

//            if (myRows.Length == 0) return;


//            for (i = 0; i < myRows.Length; i++)
//            {
//                this.mID++;

//                if ((int)myRows[i]["isFold"] == 0)
//                {
//                    /*PID, isFold , ProductID , ProductName , CommonPrice , CommonPV , PreferentialPrice , PreferentialPV, */

//                    DataRow myRow = this.pListTable.NewRow();
//                    myRow["ID"] = (int)myRows[i]["ProductID"];
//                    myRow["price"] = (decimal)myRows[i]["PreferentialPrice"];
//                    myRow["name"] = myRows[i]["ProductName"].ToString();
//                    this.pListTable.Rows.Add(myRow);

//                    if (i == myRows.Length - 1)
//                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle src=" + this.imgLine2 + "><img align=absmiddle src=" + this.imgItem + ">");
//                    else
//                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle src=" + this.imgLine3 + "><img align=absmiddle src=" + this.imgItem + ">");

//                    this.menuBuilder.Append("<input maxLength=6 value=0 type=text class=priceBox  size=3 name=N" + myRows[i]["ProductID"] + "></input>");

//                    this.menuBuilder.Append("\n" + myRows[i]["ProductName"].ToString());

//                    this.menuBuilder.Append("\n<a href=\"javascript:void(window.open('ProductIntro.aspx?ProductID=" + myRows[i]["ProductID"].ToString() + "','','width=450,height=240')) \">" + CommonDataBLL.GetClassTran("classes/producttree.cs_167", "查看") + "</a><br>\n");
//                    //						myRows[i]["ProductName"].ToString() + "\\n普通价："  + 						
//                    //						myRows[i]["CommonPrice"].ToString() + " 积分：" +
//                    //						myRows[i]["CommonPV"].ToString() + "\\n会员价：" +
//                    //						myRows[i]["PreferentialPrice"].ToString() + " 积分：" +  						
//                    //						myRows[i]["PreferentialPV"].ToString() + "')\" >查看</a><br>\n" );

//                }
//                else
//                {
//                    this.menuBuilder.Append("\n<div>");

//                    if (i == myRows.Length - 1)
//                    {
//                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=" + this.imgPlus2 + " class='menutop'>");

//                        this.menuBuilder.Append("<img align=absmiddle id=img" + this.mID + " src=" + this.imgFoldClose + ">" + myRows[i]["ProductName"].ToString() + "\n");

//                        this.menuBuilder.Append("\n</div>");

//                        this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='display:none;margin-top=-3'>");

//                        makeP((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgEmpty + ">");

//                        this.menuBuilder.Append("\n</div>");

//                    }
//                    else
//                    {
//                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=" + this.imgPlus3 + " class='menutop'>");

//                        this.menuBuilder.Append("<img align=absmiddle id=img" + this.mID + " src=" + this.imgFoldClose + ">" + myRows[i]["ProductName"].ToString() + "\n");

//                        this.menuBuilder.Append("\n</div>");

//                        this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='display:none;margin-top=-3'>");

//                        makeP((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgLine1 + ">");

//                        this.menuBuilder.Append("\n</div>");
//                    }


//                }

//            }


//        }

//        //返回消费后产品修改菜单*************************************************************************************************************
//        public string getPEditMenu(DataTable outTable)
//        {
//            this.pTable = outTable;

//            string outString;
//            this.makePEdit(1, "", out outString);

//            return outString;
//        }

//        //----------------------------------------------------------------------------------------------------
//        private bool makePEdit(int pID, string pLayer, out string menu)
//        {
//            int i;
//            bool parentShow = false;

//            StringBuilder tempBuild = new StringBuilder();

//            DataRow[] myRows = this.pTable.Select("pID=" + pID.ToString(), "ProductID");

//            if (myRows.Length == 0)
//            {
//                menu = "";
//                return false;
//            }

//            for (i = 0; i < myRows.Length; i++)
//            {
//                this.mID++;

//                if ((int)myRows[i]["isFold"] == 0)
//                {

//                    if (i == myRows.Length - 1)
//                        tempBuild.Append("\n" + pLayer + "<img align=absmiddle src=" + this.imgLine2 + "><img align=absmiddle src=" + this.imgItem + ">");
//                    else
//                        tempBuild.Append("\n" + pLayer + "<img align=absmiddle src=" + this.imgLine3 + "><img align=absmiddle src=" + this.imgItem + ">");

//                    if ((int)myRows[i]["num"] > 0) parentShow = true;

//                    tempBuild.Append("<input value=" +
//                        myRows[i]["num"].ToString() +
//                        " type=text class=priceBox  size=3 name=N" + myRows[i]["ProductID"] + "></input>");

//                    tempBuild.Append("\n" + myRows[i]["ProductName"].ToString());


//                    this.menuBuilder.Append("\n<a href=\"javascript:void(window.open('ProductIntro.aspx?ProductID=" + myRows[i]["ProductID"].ToString() + "','','width=450,height=240')) \">" + CommonDataBLL.GetClassTran("classes/producttree.cs_267", "查看") + "</a><br>\n");

//                    //					this.menuBuilder.Append("\n<a href=# onclick=\"alert('产品名：" + 
//                    //						myRows[i]["ProductName"].ToString() + "\\n普通价："  + 						
//                    //						myRows[i]["CommonPrice"].ToString() + " 积分：" +
//                    //						myRows[i]["CommonPV"].ToString() + "\\n会员价：" +
//                    //						myRows[i]["PreferentialPrice"].ToString() + " 积分：" +  						
//                    //						myRows[i]["PreferentialPV"].ToString() + "')\" >查看</a><br>\n" );

//                }
//                else
//                {
//                    string outMenu, display;

//                    tempBuild.Append("\n<div >");

//                    if (i == myRows.Length - 1)
//                    {
//                        if (makePEdit((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgEmpty + ">", out outMenu))
//                        {
//                            display = "";
//                            parentShow = true;
//                            tempBuild.Append("\n" + pLayer + "<img align=absmiddle onclick=menu(menu" + this.mID + ",img" + this.mID + ",this) id=plus" + this.mID + " src=" + this.imgMinus2 + " class='menutop'>");
//                        }
//                        else
//                        {
//                            display = "none";
//                            tempBuild.Append("\n" + pLayer + "<img align=absmiddle onclick=menu(menu" + this.mID + ",img" + this.mID + ",this) id=plus" + this.mID + " src=" + this.imgPlus2 + " class='menutop'>");
//                        }
//                    }
//                    else
//                    {
//                        if (makePEdit((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgLine1 + ">", out outMenu))
//                        {
//                            display = "";
//                            parentShow = true;
//                            tempBuild.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=" + this.imgMinus3 + " class='menutop'>");
//                        }
//                        else
//                        {
//                            display = "none";
//                            tempBuild.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=" + this.imgPlus3 + " class='menutop'>");
//                        }
//                    }
//                    tempBuild.Append("<img align=absmiddle id=img" + this.mID + " src=" + this.imgFoldClose + ">" + myRows[i]["ProductName"].ToString() + "\n");

//                    tempBuild.Append("\n</div>");

//                    tempBuild.Append("\n<div id=menu" + this.mID + " style='display:" + display + "' style=margin-top=-3 >");

//                    tempBuild.Append(outMenu);

//                    tempBuild.Append("\n</div>");

//                    this.mID++;

//                }

//            }

//            menu = tempBuild.ToString();

//            return parentShow;

//        }

//        //递归函数，产生消费后产品修改菜单--------------------------------------
//        private string makePEdit2(int pID, string pLayer)
//        {
//            int i, itemCount;

//            StringBuilder tempBuild = new StringBuilder();

//            this.needShow = false;

//            DataRow[] myRows = this.pTable.Select("PID = " + pID.ToString(), "ProductID");

//            for (i = 0, itemCount = 0; i < myRows.Length; i++)
//            {
//                this.mID++;

//                if ((int)myRows[i]["isFold"] == 0)
//                {
//                    itemCount++;

//                    if (i == myRows.Length - 1)
//                        tempBuild.Append("\n" + pLayer + "<img align=absmiddle src=" + this.imgLine2 + ">" + "<img align=absmiddle src=" + this.imgItem + ">");
//                    else
//                        tempBuild.Append("\n" + pLayer + "<img align=absmiddle src=" + this.imgLine3 + "><img align=absmiddle src=" + this.imgItem + ">");

//                    if ((int)myRows[i]["num"] > 0)
//                        this.needShow = true;


//                    tempBuild.Append("<input value=" +
//                        myRows[i]["num"].ToString() +
//                        " type=text class=priceBox  size=3 name=N" + myRows[i]["ProductID"] + "></input>");

//                    tempBuild.Append("\n" + myRows[i]["ProductName"].ToString());

//                    this.menuBuilder.Append("\n<a href=\"javascript:void(window.open('ProductIntro.aspx?ProductID=" + myRows[i]["ProductID"].ToString() + "','','width=450,height=240')) \">" + CommonDataBLL.GetClassTran("classes/producttree.cs_372", "查看") + "</a><br>\n");

//                    //					this.menuBuilder.Append("\n<a href=# onclick=\"alert('产品名：" + 
//                    //						myRows[i]["ProductName"].ToString() + "\\n普通价："  + 						
//                    //						myRows[i]["CommonPrice"].ToString() + " 积分：" +
//                    //						myRows[i]["CommonPV"].ToString() + "\\n会员价：" +
//                    //						myRows[i]["PreferentialPrice"].ToString() + " 积分：" +  						
//                    //						myRows[i]["PreferentialPV"].ToString() + "')\" >查看</a><br>\n" );
//                }
//                else
//                {
//                    string childrenMenu, display = "";

//                    tempBuild.Append("\n<div >");

//                    if (i == myRows.Length - 1)
//                        childrenMenu = makePEdit2((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgEmpty + ">");
//                    else
//                        childrenMenu = makePEdit2((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgLine1 + ">");


//                    display = needShow ? "" : "none";

//                    tempBuild.Append("\n" + pLayer +
//                        "<img align=absmiddle onclick=menu(menu" + this.mID + ",img" + this.mID + ",this) id=plus" + this.mID + " src=" + this.imgPlus3 + " class='menutop'>");

//                    tempBuild.Append("<img align=absmiddle id=img" + this.mID + " src=" + this.imgFoldClose + ">" + myRows[i]["ProductName"].ToString() + "\n");

//                    tempBuild.Append("\n</div>");

//                    tempBuild.Append("\n<div id=menu" + this.mID + " style=display:" + display + " style=margin-top=-3  >");

//                    tempBuild.Append(childrenMenu);

//                    tempBuild.Append("\n</div>");

//                    this.mID++;
//                }
//            }

//            return tempBuild.ToString();
//        }


//        //产生可修改菜单-------------------------------------------
//        private void makeP2(int pID, string pLayer)
//        {
//            int i;

//            DataRow[] myRows = this.pTable.Select(" PID= " + pID.ToString(), "ProductID");

//            for (i = 0; i < myRows.Length; i++)
//            {
//                this.mID++;

//                if ((int)myRows[i]["isFold"] == 0)
//                {
//                    if (i == myRows.Length - 1)
//                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle src=" + this.imgLine2 + ">" + "<img align=absmiddle src=" + this.imgItem + ">");
//                    else
//                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle src=" + this.imgLine3 + "><img align=absmiddle src=" + this.imgItem + ">");

//                    this.menuBuilder.Append("\n" + myRows[i]["ProductName"].ToString() + " *￥ " + myRows[i]["PreferentialPrice"].ToString() + "\n");

//                    this.menuBuilder.Append("\n<a style=color:#104E8B href=javascript:openAddWin('editItem'," + myRows[i]["ProductID"].ToString() + ")>" + CommonDataBLL.GetClassTran("classes/producttree.cs_436", "修改") + "</a>\n");
//                    this.menuBuilder.Append("\n<a style=color:#104E8B href=javascript:openAddWin('deleteItem'," + myRows[i]["ProductID"].ToString() + ")>" + CommonDataBLL.GetClassTran("classes/producttree.cs_437", "删除") + "</a><br>\n");

//                }
//                else
//                {
//                    this.menuBuilder.Append("\n<div >");

//                    this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=" + this.imgPlus2 + " class='menutop'>");

//                    this.menuBuilder.Append("<img align=absmiddle id=img" + this.mID + " src=../images/fc.gif>" + myRows[i]["ProductName"].ToString() + "\n");

//                    this.menuBuilder.Append("\n<a style=color:#104E8B href=javascript:openAddWin('addFold'," + myRows[i]["ProductID"].ToString() + ")>" + CommonDataBLL.GetClassTran("classes/producttree.cs_448", "添加新类") + "</a>\n");

//                    this.menuBuilder.Append("\n<a style=color:#104E8B href=javascript:openAddWin('add'," + myRows[i]["ProductID"].ToString() + ")>" + CommonDataBLL.GetClassTran("classes/producttree.cs_450", "添加新品") + "</a>\n");

//                    this.menuBuilder.Append("\n<a style=color:#104E8B href=javascript:openAddWin('editFold'," + myRows[i]["ProductID"].ToString() + ")>" + CommonDataBLL.GetClassTran("classes/producttree.cs_452", "修改") + "</a>\n");

//                    this.menuBuilder.Append("\n<a style=color:#104E8B href=javascript:openAddWin('deleteFold'," + myRows[i]["ProductID"].ToString() + ")>" + CommonDataBLL.GetClassTran("classes/producttree.cs_454", "删除") + "</a>\n");

//                    this.menuBuilder.Append("\n</div>");

//                    this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='display:none;margin-top=-3'>");

//                    makeP2((int)myRows[i]["ProductID"], pLayer + this.imgLine1);

//                    this.menuBuilder.Append("\n</div>");
//                }
//            }
//        }


//        //产生可修改菜单-------------------------------------------
//        private void makeP3(int pID, string pLayer)
//        {
//            int i;

//            DataRow[] myRows = this.pTable.Select(" PID = " + pID.ToString(), "ProductID");

//            if (myRows.Length == 0) return;

//            for (i = 0; i < myRows.Length; i++)
//            {
//                this.mID++;

//                if ((int)myRows[i]["isFold"] == 0)
//                {
//                    if (i == myRows.Length - 1)
//                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle src=" + this.imgLine2 + "><img align=absmiddle src=" + this.imgItem + ">");
//                    else
//                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle src=" + this.imgLine3 + "><img align=absmiddle src=" + this.imgItem + ">");

//                    this.menuBuilder.Append("\n" + myRows[i]["ProductName"].ToString().Trim() + " " + myRows[i]["PreferentialPrice"].ToString() + "\n");

//                    this.menuBuilder.Append("\n<a style=color:#075C79 href=javascript:openAddWin('editItem'," + myRows[i]["ProductID"].ToString() + ")>" + CommonDataBLL.GetClassTran("classes/producttree.cs_490", "修改") + "</a>\n");
//                    this.menuBuilder.Append("\n<a style=color:#075C79 href=javascript:openAddWin('deleteItem'," + myRows[i]["ProductID"].ToString() + ")>" + CommonDataBLL.GetClassTran("classes/producttree.cs_491", "删除") + "</a><br>\n");

//                }
//                else
//                {
//                    this.menuBuilder.Append("\n<div>");

//                    if (i == myRows.Length - 1)
//                    {
//                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=" + this.imgPlus2 + " class='menutop'>");

//                        this.menuBuilder.Append("<img align=absmiddle id=img" + this.mID + " src=" + this.imgFoldClose + ">" + myRows[i]["ProductName"].ToString() + "\n");

//                        this.menuBuilder.Append("\n<a style=color:#075C79 href=javascript:openAddWin('addFold'," + myRows[i]["ProductID"].ToString() + ")>" + CommonDataBLL.GetClassTran("classes/producttree.cs_504", "添加新类") + "</a>\n");

//                        this.menuBuilder.Append("\n<a style=color:#075C79 href=javascript:openAddWin('add'," + myRows[i]["ProductID"].ToString() + ")>" + CommonDataBLL.GetClassTran("classes/producttree.cs_506", "添加新品") + "</a>\n");

//                        this.menuBuilder.Append("\n<a style=color:#075C79 href=javascript:openAddWin('editFold'," + myRows[i]["ProductID"].ToString() + ")>" + CommonDataBLL.GetClassTran("classes/producttree.cs_508", "修改") + "</a>\n");

//                        this.menuBuilder.Append("\n<a style=color:#075C79 href=javascript:openAddWin('deleteFold'," + myRows[i]["ProductID"].ToString() + ")>" + CommonDataBLL.GetClassTran("classes/producttree.cs_510", "删除") + "</a>\n");

//                        this.menuBuilder.Append("\n</div>");

//                        this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='display:none;margin-top=-3' >");

//                        makeP3((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgEmpty + ">");

//                        this.menuBuilder.Append("\n</div>");

//                    }
//                    else
//                    {
//                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=" + this.imgPlus3 + " class='menutop'>");

//                        this.menuBuilder.Append("<img align=absmiddle id=img" + this.mID + " src=" + this.imgFoldClose + ">" + myRows[i]["ProductName"].ToString() + "\n");

//                        this.menuBuilder.Append("\n<a style=color:#075C79 href=javascript:openAddWin('addFold'," + myRows[i]["ProductID"].ToString() + ")>" + CommonDataBLL.GetClassTran("classes/producttree.cs_527", "添加新类") + "</a>\n");

//                        this.menuBuilder.Append("\n<a style=color:#075C79 href=javascript:openAddWin('add'," + myRows[i]["ProductID"].ToString() + ")>" + CommonDataBLL.GetClassTran("classes/producttree.cs_529", "添加新品") + "</a>\n");

//                        this.menuBuilder.Append("\n<a style=color:#075C79 href=javascript:openAddWin('editFold'," + myRows[i]["ProductID"].ToString() + ")>" + CommonDataBLL.GetClassTran("classes/producttree.cs_531", "修改") + "</a>\n");

//                        this.menuBuilder.Append("\n<a style=color:#075C79 href=javascript:openAddWin('deleteFold'," + myRows[i]["ProductID"].ToString() + ")>" + CommonDataBLL.GetClassTran("classes/producttree.cs_533", "删除") + "</a>\n");

//                        this.menuBuilder.Append("\n</div>");

//                        this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='display:none;margin-top=-3'>");

//                        makeP3((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgLine1 + ">");

//                        this.menuBuilder.Append("\n</div>");
//                    }
//                }
//            }
//        }


//        //设置修改时产品菜单
//        public string setEditMenu(string pinming)
//        {
//            this.getData();

//            char[] de = { '|' };
//            char[] de2 = { ':' };
//            string[] oldProducts = pinming.Split(de);

//            DataRow[] myRows;

//            string[] mm;
//            int i;

//            for (i = 0; i < oldProducts.Length; i++)
//            {
//                mm = oldProducts[i].Split(de2);

//                myRows = pTable.Select("name='" + mm[0].Trim() + "'");

//                if (myRows.Length > 0) myRows[0]["num"] = mm[1];
//            }

//            return this.getPEditMenu(this.pTable);
//        }


//        //带修改选项菜单
//        public string getEditMenu(int Country)
//        {
//            this.getData_ByCurrency(Country);
//            this.menuBuilder.Append("<div>" + CommonDataBLL.GetClassTran("classes/producttree.cs_579[1]", "产品列表") + "&nbsp;&nbsp;<a style=color:#075C79 href=javascript:openAddWin2('addFold',1," + Country.ToString() + ")> " + CommonDataBLL.GetClassTran("classes/producttree.cs_579[2]", "添加新类") + "</a>&nbsp;&nbsp;<a style=color:#075C79 href=javascript:openAddWin2('add',1," + Country.ToString() + ")> " + CommonDataBLL.GetClassTran("classes/producttree.cs_579[3]", "添加新品") + "</a><div>");
//            if (pTable.Rows.Count != 0)
//            {
//                this.makeP3(1, "");
//            }
//            else
//            {
//                this.menuBuilder.Append("<br>" + CommonDataBLL.GetClassTran("classes/producttree.cs_586", "产品库中无记录！") + "<div>");
//            }

//            return this.menuBuilder.ToString();
//        }


//        //返回产品映射表
//        public DataTable getProductsTable()
//        {
//            return this.pListTable;
//        }

//        private void getData() //读取所有产品数据
//        {
//            string ssQL = "";
//            if (System.Web.HttpContext.Current.Session["language"].ToString().ToLower() == "chinese" || System.Web.HttpContext.Current.Session["language"].ToString().ToUpper() == "中文")
//            {
//                ssQL = @"Select PID, isFold , ProductID , ProductName , CommonPrice ,productcode, CommonPV , PreferentialPrice , PreferentialPV, 
//								" + this._defaultProductNum + " As num From Product where isHidden=0";//Order By ProductID
//            }
//            else
//            {
//                int ID = Convert.ToInt32(DBHelper.ExecuteScalar("select id from m_language where name='" + System.Web.HttpContext.Current.Session["language"].ToString() + "'"));
//                ssQL = @"Select PID, isFold , ProductID , productcode,(select languagename from languageTolanguage where 
//							languageTolanguage.yuanid=Product.Productid and languageTolanguage.Columnsname='productname' and languageid=" + ID + @") as ProductName , CommonPrice , CommonPV , PreferentialPrice , PreferentialPV, 
//								" + this._defaultProductNum + " As num From Product where isHidden=0";//Order By ProductID
//            }


//            this.pTable = DBHelper.ExecuteDataTable(ssQL);
//        }

//        private void getData(string dian, string Currency) //读取所有产品数据
//        {
//            string Country = DBHelper.ExecuteScalar("Select m_currency.id from d_info,m_currency where storeid='" + dian + "' and d_info.currency=m_currency.ID").ToString();
//            //string ssQL=@"Select PID, isFold , ProductID , ProductName , CommonPrice , CommonPV , PreferentialPrice , PreferentialPV, 0 As num From Product WHERE ((ProductID IN (Select ProductID From D_Kucun Where  StoreID='" + dian + @"') or isFold=1) and currency="+currency+")";	//Order By ProductID
//            string ssQL = @"Select PID, isFold , ProductID , ProductName , CommonPrice , CommonPV , PreferentialPrice , PreferentialPV, 0 As num From Product WHERE isHidden=0 and Country=" + Country;	//Order By ProductID
//            this.pTable = DBHelper.ExecuteDataTable(ssQL);
//        }

//        private void getData_ByCurrency(int Country) //根据国家读取 产品数据
//        {
//            string ssQL = @"Select PID, isFold , ProductID , ProductName , CommonPrice , CommonPV , PreferentialPrice , PreferentialPV, 
//								" + this._defaultProductNum + " As num From Product where Country=" + Country.ToString();//Order By ProductID

//            this.pTable = DBHelper.ExecuteDataTable(ssQL);
//        }

//        private void getData(string dian) //读取指定店铺的产品数据
//        {
//            string Country = DBHelper.ExecuteScalar("Select m_country.id from d_info,m_country where storeid='" + dian + "' and d_info.d_country=m_country.name").ToString();

//            if (System.Web.HttpContext.Current.Session["language"].ToString().ToLower() == "chinese" || System.Web.HttpContext.Current.Session["language"].ToString().ToUpper() == "中文")
//            {

//                //string ssQL=@"Select PID, isFold , ProductID , ProductName , CommonPrice , CommonPV , PreferentialPrice , PreferentialPV, 0 As num From Product WHERE ((ProductID IN (Select ProductID From D_Kucun Where  StoreID='" + dian + @"') or isFold=1) and currency="+currency+")";	//Order By ProductID
//                string ssQL = @"Select PID, isFold , ProductID , ProductName , CommonPrice , CommonPV , PreferentialPrice , PreferentialPV, 0 As num From Product WHERE isHidden=0 and Country=" + Country;	//Order By ProductID
//                this.pTable = DBHelper.ExecuteDataTable(ssQL);
//            }
//            else
//            {
//                int ID = Convert.ToInt32(DBHelper.ExecuteScalar("select id from m_language where name='" + System.Web.HttpContext.Current.Session["language"].ToString() + "'"));
//                string ssQL = @"Select PID, isFold , ProductID , (select languagename from languageTolanguage where 
//							languageTolanguage.yuanid=Product.Productid and languageTolanguage.Columnsname='productname' and languageid=" + ID + @") as ProductName ,
//						 CommonPrice , CommonPV , PreferentialPrice , PreferentialPV, 0 As num From Product WHERE isHidden=0  and Country=" + Country;	//Order By ProductID
//                this.pTable = DBHelper.ExecuteDataTable(ssQL);
//            }

        //        }
        #endregion

        public DataTable GetDataTablePage_SmsBll(int p, int p_2, string p_3, string columns, string conod, string key, string orderkey2,int sort, out int rescordcount, out int pagecounts)
        {
            return CommonDataBLL.GetDataTablePage_Sms1(p, p_2, p_3, columns, conod, key, orderkey2,sort, out rescordcount, out pagecounts);
        }
    }
}
