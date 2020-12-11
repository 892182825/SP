using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using BLL.Logistics;
using BLL.other.Company;
using BLL.CommonClass;
using DAL;
namespace BLL.Registration_declarations
{
    public class MemberTree2:BLL.TranslationBase
    {
        private int mID = 1;
        private StringBuilder menuBuilder;
        private DataTable pListTable;
        private DataTable pTable;

        private bool needShow = false;
        public string imgLine1 = "images/line1.gif";
        public string imgLine2 = "images/line2.gif";
        public string imgLine3 = "images/line3.gif";

        public string imgPlus2 = "images/plus2.gif";
        public string imgPlus3 = "images/plus3.gif";

        public string imgEmpty = "images/EMPTY.GIF";

        public string imgFoldClose = "images/foldclose.gif";
        public string imgFoldOpen = "images/foldopen.gif";
        public string imgItem = "images/item.gif";

        public string imgMinus2 = "images/minus2.gif";
        public string imgMinus3 = "images/minus3.gif";

        private string _defaultProductNum = "0";

        public MemberTree2()
        {
            this.menuBuilder = new StringBuilder();
        }


        #region 当修改的时候把已经订购的产品展开，以方便下次修改
        DataTable order_Table;//订单表
        /// <summary>
        /// 会员报单号
        /// </summary>
        public string Orderid
        {
            set
            {
                order_Table = DAL.DBHelper.ExecuteDataTable("select  productid   from   MemberDetails       where   orderid='" + value + "'");
            }

        }
        /// <summary>
        /// 店铺订单号
        /// </summary>
        public string StoreOrderid
        {
            set
            {
                order_Table = DAL.DBHelper.ExecuteDataTable("select  productid  from   D_orderdetail   where  StoreOrderid='" + value + "'");
            }

        }
        private DataTable Order_Table
        {
            get
            {
                return order_Table;
            }
        }
        /// <summary>
        /// 判断报单明细表中的产品是否在产品类的下面，在的返回-True,否则返回－False
        /// </summary>
        /// <param name="productid">产品编号</param>
        /// <returns>True/False</returns>
        private bool IsProduct(string productid)
        {

            DataTable dt = DAL.DBHelper.ExecuteDataTable("select productid  from  product  where  pid='" + productid + "'");

            if ((int)DAL.DBHelper.ExecuteScalar("select count(1) from  product  where  pid='" + productid + "' ") != 0 && (int)DAL.DBHelper.ExecuteScalar("select distinct  isfold  from  product  where  pid='" + productid + "' ") == 0)
            {
                foreach (DataRow row in this.order_Table.Rows)
                {
                    DataRow[] row_product = dt.Select("productid='" + row["productid"].ToString() + "'", "ProductID");

                    if (row_product.Length == 0)
                        continue;
                    else
                        return true;
                }
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (IsProduct(row["productid"].ToString()) == true)
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 如果是修改会员报单的话，则把报单号--OrderID传过来，如果修改的是店铺订单的话，则把订单号－StoreOrderid传过来
        /// </summary>
        /// <param name="dian"></param>
        /// <returns></returns>
        public string getEditMenu(string dian)
        {
            this.initPTable();

            this.getData(dian);

            if (pTable.Rows.Count != 0)
            {
                this.menuBuilder.Append("<div>产品列表nbsp;&nbsp;<div>");
                //this.menuBuilder.Append("<div>" + CommonData.GetClassTran("classes/producttree.cs_123", "产品列表") + "&nbsp;&nbsp;<div>");
                this._makeP(1, "");
            }
            else
            {
                this.menuBuilder.Append("<div>" + "产品列表" + "&nbsp;&nbsp;" + "产品库中无记录！" + "<div>");
                //this.menuBuilder.Append("<div>" + CommonData.GetClassTran("classes/producttree.cs_128[1]", "产品列表") + "&nbsp;&nbsp;" + CommonData.GetClassTran("classes/producttree.cs_110[2]", "产品库中无记录！") + "<div>");
            }

            return this.menuBuilder.ToString();
        }
        //产生修改菜单
        private void _makeP(int pID, string pLayer)
        {
            int i;

            DataRow[] myRows = this.pTable.Select("pID=" + pID.ToString(), "ProductID");

            if (myRows.Length == 0) return;

            for (i = 0; i < myRows.Length; i++)
            {
                this.mID++;

                if ((int)myRows[i]["isFold"] == 0)
                {
                    /*PID, isFold , ProductID , ProductName , CommonPrice , CommonPV , PreferentialPrice , PreferentialPV, */

                    DataRow myRow = this.pListTable.NewRow();
                    myRow["ID"] = (int)myRows[i]["ProductID"];
                    myRow["price"] = (decimal)myRows[i]["PreferentialPrice"];
                    myRow["name"] = (string)myRows[i]["ProductName"];
                    this.pListTable.Rows.Add(myRow);

                    if (i == myRows.Length - 1)
                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle src=" + this.imgLine2 + "><img align=absmiddle src=" + this.imgItem + ">");
                    else
                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle src=" + this.imgLine3 + "><img align=absmiddle src=" + this.imgItem + ">");

                    this.menuBuilder.Append("<input maxLength=6 value=0 type=text class=priceBox  size=3 name=N" + myRows[i]["ProductID"] + "></input>");

                    this.menuBuilder.Append("\n" + myRows[i]["ProductName"].ToString());

                    this.menuBuilder.Append("\n<a href=\"javascript:void(window.open('ProductIntro.aspx?ProductID=" + myRows[i]["ProductID"].ToString() + "','','width=450,height=240')) \">" + "查看" + "</a><br>\n"); //CommonData.GetClassTran("classes/producttree.cs_167", "查看")

                }
                else
                {
                    this.menuBuilder.Append("\n<div>");

                    if (i == myRows.Length - 1)
                    {
                        if (this.IsProduct(myRows[i]["ProductID"].ToString()) == true)
                        {
                            this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=" + this.imgMinus2 + " class='menutop'>");

                            this.menuBuilder.Append("<img align=absmiddle id=img" + this.mID + " src=" + this.imgFoldOpen + ">" + myRows[i]["ProductName"].ToString() + "\n");

                            this.menuBuilder.Append("\n</div>");

                            this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='margin-top=-3'>");
                        }
                        else
                        {
                            this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=" + this.imgPlus2 + " class='menutop'>");

                            this.menuBuilder.Append("<img align=absmiddle id=img" + this.mID + " src=" + this.imgFoldClose + ">" + myRows[i]["ProductName"].ToString() + "\n");

                            this.menuBuilder.Append("\n</div>");

                            this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='display:none;margin-top=-3'>");
                        }

                        _makeP((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgEmpty + ">");

                        this.menuBuilder.Append("\n</div>");
                    }
                    else
                    {
                        if (this.IsProduct(myRows[i]["ProductID"].ToString()) == true)
                        {
                            this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=" + this.imgMinus3 + " class='menutop'>");

                            this.menuBuilder.Append("<img align=absmiddle id=img" + this.mID + " src=" + this.imgFoldOpen + ">" + myRows[i]["ProductName"].ToString() + "\n");

                            this.menuBuilder.Append("\n</div>");

                            this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='margin-top=-3'>");
                        }
                        else
                        {
                            this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=" + this.imgPlus3 + " class='menutop'>");

                            this.menuBuilder.Append("<img align=absmiddle id=img" + this.mID + " src=" + this.imgFoldClose + ">" + myRows[i]["ProductName"].ToString() + "\n");

                            this.menuBuilder.Append("\n</div>");

                            this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='display:none;margin-top=-3'>");
                        }

                        _makeP((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgLine1 + ">");

                        this.menuBuilder.Append("\n</div>");
                    }
                }
            }
        }

        #endregion

        #region 获取或设置默认产品数量

        /// <summary>
        /// 获取或设置默认产品数量
        /// </summary>
        public string DefaultProductNum
        {
            get
            {
                return this._defaultProductNum;
            }
            set
            {
                this._defaultProductNum = value;
            }
        }
        #endregion

        //初始化产品映射表
        private void initPTable()
        {
            this.pListTable = new DataTable("ipTable");

            DataColumn myDataColumn;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "ID";
            this.pListTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Decimal");
            myDataColumn.ColumnName = "price";
            this.pListTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "name";
            this.pListTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "num";
            myDataColumn.DefaultValue = 0;
            this.pListTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "productcode";
            this.pListTable.Columns.Add(myDataColumn);
        }


        //返回非修改菜单--------------------
        //public string getMenu()
        //{
        //    this.initPTable();

        //    this.getData();

        //    if (pTable.Rows.Count != 0)
        //    {
        //        this.menuBuilder.Append("<div>" + CommonData.GetClassTran("classes/producttree.cs_105", "产品列表") + "&nbsp;&nbsp;<div>");
        //        this.makeP(1, "");
        //    }
        //    else
        //    {
        //        this.menuBuilder.Append("<div>" + CommonData.GetClassTran("classes/producttree.cs_128[1]", "产品列表") + "&nbsp;&nbsp;" + CommonData.GetClassTran("classes/producttree.cs_110[2]", "产品库中无记录！") + "<div>");
        //    }

        //    return this.menuBuilder.ToString();
        //}


        //--------------------

        /// <summary>
        /// 组合产品树（汪华）（2009-10-15所写）
        /// </summary>
        /// <returns>返回非修改菜单组合产品用</returns>
        public string getMenuZhuHe()
        {
            this.initPTable();

            this.getData();

            if (pTable.Rows.Count != 0)
            {
                //this.menuBuilder.Append("<div>" + CommonDataBLL.GetClassTran("App_code/ProductTree.cs_105", "产品列表") + "&nbsp;&nbsp;<div>");
                this.menuBuilder.Append("<div>" + "产品列表" + "&nbsp;&nbsp;<div>");
                this.makePZhuHe(1, "");
            }
            else
            {
                this.menuBuilder.Append("<div>" + "产品列表" + "&nbsp;&nbsp;" + "产品库中无记录！" + "<div>");
            }

            return this.menuBuilder.ToString();
        }


        public string getMenu(string dian)
        {
            this.initPTable();

            this.getData(dian);

            if (pTable.Rows.Count != 0)
            {
                this.menuBuilder.Append("<div>" + "产品列表" + "&nbsp;&nbsp;<div>");
                this.makeP(1, "");
            }
            else
            {
                this.menuBuilder.Append("<div>" + "产品列表" + "&nbsp;&nbsp;" + "产品库中无记录！" + "<div>");
            }

            return this.menuBuilder.ToString();
        }


        //产生非修改菜单组合产品用(汪华)
        private void makePZhuHe(int pID, string pLayer)
        {
            int i;
            DataRow[] myRows = this.pTable.Select("pID=" + pID.ToString(), "ProductID");

            if (myRows.Length == 0) return;

            for (i = 0; i < myRows.Length; i++)
            {
                this.mID++;

                if ((int)myRows[i]["isFold"] == 0)
                {
                    /*PID, isFold , ProductID , ProductName , CommonPrice , CommonPV , PreferentialPrice , PreferentialPV, */

                    DataRow myRow = this.pListTable.NewRow();
                    myRow["ID"] = (int)myRows[i]["ProductID"];
                    myRow["price"] = (decimal)myRows[i]["PreferentialPrice"];
                    myRow["name"] = myRows[i]["ProductName"].ToString();
                    myRow["productcode"] = myRows[i]["productcode"].ToString();
                    this.pListTable.Rows.Add(myRow);

                    if (i == myRows.Length - 1)
                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle src=" + this.imgLine2 + "><img align=absmiddle src=" + this.imgItem + ">");
                    else
                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle src=" + this.imgLine3 + "><img align=absmiddle src=" + this.imgItem + ">");

                    this.menuBuilder.Append("\n\t  " + myRows[i]["productcode"].ToString() + " \n");
                    this.menuBuilder.Append("\n\t    " + myRows[i]["ProductName"].ToString() + " <font color=#669900> " + (Convert.ToDouble(myRows[i]["PreferentialPrice"].ToString())).ToString("f2") + "</font><br>\n");



                }
                else
                {
                    this.menuBuilder.Append("\n<div>");

                    if (i == myRows.Length - 1)
                    {
                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=" + this.imgPlus2 + " class='menutop'>");

                        this.menuBuilder.Append("<img align=absmiddle id=img" + this.mID + " src=" + this.imgFoldClose + ">" + myRows[i]["ProductName"].ToString() + "\n");

                        this.menuBuilder.Append("\n</div>");

                        this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='display:none;margin-top=-3'>");

                        makePZhuHe((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgEmpty + ">");

                        this.menuBuilder.Append("\n</div>");

                    }
                    else
                    {
                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=" + this.imgPlus3 + " class='menutop'>");

                        this.menuBuilder.Append("<img align=absmiddle id=img" + this.mID + " src=" + this.imgFoldClose + ">" + myRows[i]["ProductName"].ToString() + "\n");

                        this.menuBuilder.Append("\n</div>");

                        this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='display:none;margin-top=-3'>");

                        makePZhuHe((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgLine1 + ">");

                        this.menuBuilder.Append("\n</div>");
                    }
                }
            }
        }

        private void makeP(int pID, string pLayer)
        {
            int i;

            DataRow[] myRows = this.pTable.Select("pID=" + pID.ToString(), "ProductID");

            if (myRows.Length == 0) return;


            for (i = 0; i < myRows.Length; i++)
            {
                this.mID++;

                if ((int)myRows[i]["isFold"] == 0)
                {
                    /*PID, isFold , ProductID , ProductName , CommonPrice , CommonPV , PreferentialPrice , PreferentialPV, */

                    DataRow myRow = this.pListTable.NewRow();
                    myRow["ID"] = (int)myRows[i]["ProductID"];
                    myRow["price"] = (decimal)myRows[i]["PreferentialPrice"];
                    myRow["name"] = myRows[i]["ProductName"].ToString();
                    this.pListTable.Rows.Add(myRow);

                    if (i == myRows.Length - 1)
                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle src=" + this.imgLine2 + "><img align=absmiddle src=" + this.imgItem + ">");
                    else
                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle src=" + this.imgLine3 + "><img align=absmiddle src=" + this.imgItem + ">");

                    this.menuBuilder.Append("<input maxLength=6 value=0 type=text class=priceBox  size=3 name=N" + myRows[i]["ProductID"] + " onblur='getTable()'></input>");

                    this.menuBuilder.Append("\n" + myRows[i]["ProductName"].ToString());

                    this.menuBuilder.Append("\n<a href=\"javascript:void(window.open('../ProductIntro.aspx?ProductID=" + myRows[i]["ProductID"].ToString() + "','','width=450,height=240')) \">" + "查看" + "</a><br>\n");//CommonData.GetClassTran("classes/producttree.cs_167", "查看")
                    //						myRows[i]["ProductName"].ToString() + "\\n普通价："  + 						
                    //						myRows[i]["CommonPrice"].ToString() + " 积分：" +
                    //						myRows[i]["CommonPV"].ToString() + "\\n会员价：" +
                    //						myRows[i]["PreferentialPrice"].ToString() + " 积分：" +  						
                    //						myRows[i]["PreferentialPV"].ToString() + "')\" >查看</a><br>\n" );

                }
                else
                {
                    this.menuBuilder.Append("\n<div>");

                    if (i == myRows.Length - 1)
                    {
                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=" + this.imgPlus2 + " class='menutop'>");

                        this.menuBuilder.Append("<img align=absmiddle id=img" + this.mID + " src=" + this.imgFoldClose + ">" + myRows[i]["ProductName"].ToString() + "\n");

                        this.menuBuilder.Append("\n</div>");

                        this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='display:none;margin-top=-3'>");

                        makeP((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgEmpty + ">");

                        this.menuBuilder.Append("\n</div>");

                    }
                    else
                    {
                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=" + this.imgPlus3 + " class='menutop'>");

                        this.menuBuilder.Append("<img align=absmiddle id=img" + this.mID + " src=" + this.imgFoldClose + ">" + myRows[i]["ProductName"].ToString() + "\n");

                        this.menuBuilder.Append("\n</div>");

                        this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='display:none;margin-top=-3'>");

                        makeP((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgLine1 + ">");

                        this.menuBuilder.Append("\n</div>");
                    }


                }

            }


        }

        private void makeP3(int pID, string pLayer)
        {
            int i;

            DataRow[] myRows = this.pTable.Select("PID = " + pID.ToString(), "ProductID");

            if (myRows.Length == 0) return;

            for (i = 0; i < myRows.Length; i++)
            {
                this.mID++;

                if ((int)myRows[i]["isFold"] == 0)
                {
                    if (i == myRows.Length - 1)
                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle src=" + this.imgLine2 + "><img align=absmiddle src=" + this.imgItem + ">");

                    else
                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle src=" + this.imgLine3 + "><img align=absmiddle src=" + this.imgItem + ">");


                    this.menuBuilder.Append("\n" + myRows[i]["ProductName"].ToString().Trim() + " " + myRows[i]["PreferentialPrice"].ToString() + "\n");

                    this.menuBuilder.Append("\n<a style=color:#075C79 href=javascript:openAddWin('editItem'," + myRows[i]["ProductID"].ToString() + ")>" + CommonDataBLL.GetClassTran("App_code/ProductTree.cs_490", "修改") + "</a>\n");
                    this.menuBuilder.Append("\n<a style=color:#075C79 href=javascript:openAddWin('deleteItem'," + myRows[i]["ProductID"].ToString() + ")>" + CommonDataBLL.GetClassTran("App_code/ProductTree.cs_491", "删除") + "</a><br>\n");

                }
                else
                {
                    this.menuBuilder.Append("\n<div>");

                    if (i == myRows.Length - 1)
                    {
                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=" + this.imgPlus2 + " class='menutop'>");

                        this.menuBuilder.Append("<img align=absmiddle id=img" + this.mID + " src=" + this.imgFoldClose + ">" + myRows[i]["ProductName"].ToString() + "\n");

                        this.menuBuilder.Append("\n<a style=color:#075C79 href=javascript:openAddWin('addFold'," + myRows[i]["ProductID"].ToString() + ")>" + CommonDataBLL.GetClassTran("App_code/ProductTree.cs_504", "添加新类") + "</a>\n");

                        this.menuBuilder.Append("\n<a style=color:#075C79 href=javascript:openAddWin('add'," + myRows[i]["ProductID"].ToString() + ")>" + CommonDataBLL.GetClassTran("App_code/ProductTree.cs_506", "添加新品") + "</a>\n");

                        this.menuBuilder.Append("\n<a style=color:#075C79 href=javascript:openAddWin('editFold'," + myRows[i]["ProductID"].ToString() + ")>" + CommonDataBLL.GetClassTran("App_code/ProductTree.cs_508", "修改") + "</a>\n");

                        this.menuBuilder.Append("\n<a style=color:#075C79 href=javascript:openAddWin('deleteFold'," + myRows[i]["ProductID"].ToString() + ")>" + CommonDataBLL.GetClassTran("App_code/ProductTree.cs_510", "删除") + "</a>\n");

                        this.menuBuilder.Append("\n</div>");

                        this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='display:none;margin-top=-3' >");

                        makeP3((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgEmpty + ">");

                        this.menuBuilder.Append("\n</div>");

                    }
                    else
                    {
                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=" + this.imgPlus3 + " class='menutop'>");

                        this.menuBuilder.Append("<img align=absmiddle id=img" + this.mID + " src=" + this.imgFoldClose + ">" + myRows[i]["ProductName"].ToString() + "\n");

                        this.menuBuilder.Append("\n<a style=color:#075C79 href=javascript:openAddWin('addFold'," + myRows[i]["ProductID"].ToString() + ")>" + CommonDataBLL.GetClassTran("App_code/ProductTree.cs_527", "添加新类") + "</a>\n");

                        this.menuBuilder.Append("\n<a style=color:#075C79 href=javascript:openAddWin('add'," + myRows[i]["ProductID"].ToString() + ")>" + CommonDataBLL.GetClassTran("App_code/ProductTree.cs_529", "添加新品") + "</a>\n");

                        this.menuBuilder.Append("\n<a style=color:#075C79 href=javascript:openAddWin('editFold'," + myRows[i]["ProductID"].ToString() + ")>" + CommonDataBLL.GetClassTran("App_code/ProductTree.cs_531", "修改") + "</a>\n");

                        this.menuBuilder.Append("\n<a style=color:#075C79 href=javascript:openAddWin('deleteFold'," + myRows[i]["ProductID"].ToString() + ")>" + CommonDataBLL.GetClassTran("App_code/ProductTree.cs_533", "删除") + "</a>\n");

                        this.menuBuilder.Append("\n</div>");

                        this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='display:none;margin-top=-3'>");

                        makeP3((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgLine1 + ">");

                        this.menuBuilder.Append("\n</div>");
                    }
                }
            }
        }

        /// <summary>
        /// 带修改选项菜单(汪华)(2009-10-16修改)
        /// </summary>
        /// <param name="Country"></param>
        /// <returns></returns>
        public string getEditMenu(int Country)
        {
            this.getData_ByCurrency(Country);
            //this.menuBuilder.Append("<div>" + CommonDataBLL.GetClassTran("App_code/ProductTree.cs_579[1]", "产品列表") + "&nbsp;&nbsp;<a style=color:#075C79 href=javascript:openAddWin2('addFold',1," + Country.ToString() + ")> " + CommonDataBLL.GetClassTran("App_code/ProductTree.cs_579[2]", "添加新类") + "</a>&nbsp;&nbsp;<a style=color:#075C79 href=javascript:openAddWin2('add',1," + Country.ToString() + ")> " + CommonDataBLL.GetClassTran("App_code/ProductTree.cs_579[3]", "添加新品") + "</a><div>");
            this.menuBuilder.Append("<div>" + "产品列表" + "&nbsp;&nbsp;<a style=color:#075C79 href=javascript:openAddWin2('addFold',1," + Country.ToString() + ")> " + "添加新类" + "</a>&nbsp;&nbsp;<a style=color:#075C79 href=javascript:openAddWin2('add',1," + Country.ToString() + ")> " + "添加新品" + "</a><div>");
            if (pTable.Rows.Count != 0)
            {
                this.makeP3(1, "");
            }
            else
            {
                this.menuBuilder.Append("<br>" + "产品库中无记录！" + "<div>");
            }

            return this.menuBuilder.ToString();
        }


        //返回产品映射表
        public DataTable getProductsTable()
        {
            return this.pListTable;
        }

        private void getData() //读取所有产品数据
        {
            string ssQL = "";
            if (System.Web.HttpContext.Current.Session["language"].ToString().ToLower() == "chinese" || System.Web.HttpContext.Current.Session["language"].ToString().ToUpper() == "中文")
            {
                ssQL = @"Select PID, isFold , ProductID , ProductName , CommonPrice ,productcode, CommonPV , PreferentialPrice , PreferentialPV, 
								" + this._defaultProductNum + " As num From Product where isSell=0";//Order By ProductID
            }
            else
            {
                int ID = Convert.ToInt32(DAL.DBHelper.ExecuteScalar("select id from language where name='" + System.Web.HttpContext.Current.Session["language"].ToString() + "'"));
                ssQL = @"Select PID, isFold , ProductID , productcode,(select languagename from LanguageTrans where 
							LanguageTrans.OldID=Product.Productid and LanguageTrans.Columnsname='productname' and languageid=" + ID + @") as ProductName , CommonPrice , CommonPV , PreferentialPrice , PreferentialPV, 
								" + this._defaultProductNum + " As num From Product where isSell=0";//Order By ProductID
            }


            this.pTable = DAL.DBHelper.ExecuteDataTable(ssQL);
        }

        private void getData(string dian, string Currency) //读取所有产品数据
        {
            string Country = DAL.DBHelper.ExecuteScalar("Select m_currency.id from d_info,m_currency where storeid='" + dian + "' and d_info.currency=m_currency.ID").ToString();
            //string ssQL=@"Select PID, isFold , ProductID , ProductName , CommonPrice , CommonPV , PreferentialPrice , PreferentialPV, 0 As num From Product WHERE ((ProductID IN (Select ProductID From D_Kucun Where  StoreID='" + dian + @"') or isFold=1) and currency="+currency+")";	//Order By ProductID
            string ssQL = @"Select PID, isFold , ProductID , ProductName , CommonPrice , CommonPV , PreferentialPrice , PreferentialPV, 0 As num From Product WHERE isHidden=0 and Country=" + Country;	//Order By ProductID
            this.pTable = DAL.DBHelper.ExecuteDataTable(ssQL);
        }

        private void getData_ByCurrency(int Country) //根据国家读取 产品数据
        {
            string ssQL = @"Select PID, isFold , ProductID , ProductName , CommonPrice , CommonPV , PreferentialPrice , PreferentialPV, 
								" + this._defaultProductNum + " As num From Product where Country=" + Country.ToString();//Order By ProductID

            this.pTable = DAL.DBHelper.ExecuteDataTable(ssQL);
        }

        private void getData(string dian) //读取指定店铺的产品数据
        {
            //string Country = DAL.DBHelper.ExecuteScalar("Select m_country.id from d_info,m_country where storeid='" + dian + "' and d_info.d_country=m_country.name").ToString();

            if (System.Web.HttpContext.Current.Session["language"].ToString().ToLower() == "chinese" || System.Web.HttpContext.Current.Session["language"].ToString().ToUpper() == "中文")
            {

                string ssQL = @"Select PID, isFold , ProductID , ProductName , CommonPrice , CommonPV , PreferentialPrice , PreferentialPV, 0 As num From Product WHERE isSell=0 ";	//Order By ProductID
                this.pTable = DAL.DBHelper.ExecuteDataTable(ssQL);
            }
            //        else
            //        {
            //            int ID = Convert.ToInt32(DAL.DBHelper.ExecuteScalar("select id from m_language where name='" + System.Web.HttpContext.Current.Session["language"].ToString() + "'"));
            //            string ssQL = @"Select PID, isFold , ProductID , (select languagename from languageTolanguage where 
            //							languageTolanguage.yuanid=Product.Productid and languageTolanguage.Columnsname='productname' and languageid=" + ID + @") as ProductName ,
            //						 CommonPrice , CommonPV , PreferentialPrice , PreferentialPV, 0 As num From Product WHERE isHidden=0  and Country=" + Country;	//Order By ProductID
            //            this.pTable = DAL.DBHelper.ExecuteDataTable(ssQL);
            //        }
        }
        OrderGoodsBLL orderGoodsBLL = new OrderGoodsBLL();
        AddNewProductBLL addNewProductBLL = new AddNewProductBLL();

     
        private void InitPTable()
        {
            this.pListTable = new DataTable("ipTable");

            DataColumn myDataColumn;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "ID";
            this.pListTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Decimal");
            myDataColumn.ColumnName = "price";
            this.pListTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "name";
            this.pListTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "num";
            myDataColumn.DefaultValue = 0;
            this.pListTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "productcode";
            this.pListTable.Columns.Add(myDataColumn);
        }
        /// <summary>
        ///读取所有产品数据
        /// </summary>
        private void GetData(string storeid)
        {
            pTable = OrderGoodsBLL.GetAllProducts(storeid);
        }
        /// <summary>
        /// 店铺在线订货时生成产品树形菜单
        /// </summary>
        /// <param name="storeid">店鋪編號</param>
        /// <param name="ZT">選擇的支付方式</param>
        /// <returns></returns>
        public string getMenuOrder(string storeid, int ZT)
        {
            this.InitPTable();
            this.GetData(storeid);
            if (pTable.Rows.Count != 0)
            {
                this.menuBuilder.Append("<div>产品列表&nbsp;&nbsp;<div>");

                this.MakePOrder(1, "", storeid, ZT);
            }
            else
            {
                this.menuBuilder.Append("<div>产品列表&nbsp;&nbsp;产品库中无记录！" + "<div>");
            }

            return this.menuBuilder.ToString();
        }

        /// <summary>
        ///修改订单时生成产品树形菜单
        /// </summary>
        /// <param name="storeid">店铺编号</param>
        /// <param name="orderid">订单编号</param>
        /// <param name="ZT">选择支付方式</param>
        /// <returns></returns>
        public string getEditMenuOrder(string storeid, string orderid, int ZT)
        {
            this.InitPTable();
            this.GetData(storeid);
            if (pTable.Rows.Count != 0)
            {
                this.menuBuilder.Append("<div>产品列表&nbsp;&nbsp;<div>");

                this.EditMakePOrder(1, "", storeid, ZT, orderid);
            }
            else
            {
                this.menuBuilder.Append("<div>产品列表&nbsp;&nbsp;产品库中无记录！" + "<div>");
            }

            return this.menuBuilder.ToString();
        }

        /// <summary>
        /// 修改订单时产生非修改菜单
        /// </summary>
        private void EditMakePOrder(int pID, string pLayer, string storeid, int ZT, string orderid)
        {
            int i;
            DataRow[] myRows = this.pTable.Select("pID=" + pID.ToString(), "ProductID");
            if (myRows.Length == 0) return;
            for (i = 0; i < myRows.Length; i++)
            {
                this.mID++;
                if ((int)myRows[i]["isFold"] == 0)
                {
                    DataRow myRow = this.pListTable.NewRow();
                    myRow["ID"] = (int)myRows[i]["ProductID"];
                    myRow["price"] = (decimal)myRows[i]["PreferentialPrice"];
                    myRow["name"] = myRows[i]["ProductName"].ToString();
                    this.pListTable.Rows.Add(myRow);
                    //获取出实际库存和在线数量的总和，然后赋值给变量Quantity
                    DataTable dt = OrderGoodsBLL.GetAllTityByStoreidAndProductid(storeid, Convert.ToInt32(myRows[i]["ProductID"]));
                    int Quantity = 0;
                    //if (dt.Rows.Count > 0)
                    //{
                    //    if (ZT == 1)
                    //    {
                    //        Quantity = Convert.ToInt32(dt.Rows[0]["Quantity"].ToString());
                    //    }
                    //    else
                    //    {
                    //        Quantity = 0;
                    //    }
                    //}
                    //else
                    //{
                    //    Quantity = 0;
                    //}
                    if (i == myRows.Length - 1)
                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle src=" + this.imgLine2 + "><img align=absmiddle src=" + this.imgItem + ">");
                    else
                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle src=" + this.imgLine3 + "><img align=absmiddle src=" + this.imgItem + ">");

                    this.menuBuilder.Append("<input maxLength=6 value=" + Quantity + " type=text class=priceBox onkeyup=\"Bind()\"  size=3 name=N" + myRows[i]["ProductID"] + "></input>");

                    this.menuBuilder.Append("\n\t    " + myRows[i]["ProductName"].ToString() + " <font color=black>会员价：" + (Convert.ToDouble(myRows[i]["PreferentialPrice"].ToString())).ToString("f2") + "；积分：" + (Convert.ToDouble(myRows[i]["PreferentialPV"].ToString())).ToString("f2") + "</font>\n");
                    this.menuBuilder.Append("\n<a href=\"javascript:void(window.open('../ProductIntro.aspx?ProductID=" + myRows[i]["ProductID"].ToString() + "','','width=450,height=240')) \"> </a><br>\n");//查看
                }
                else
                {
                    this.menuBuilder.Append("\n<div>");

                    if (i == myRows.Length - 1)
                    {
                        if (CheckOrderNum(myRows[i]["ProductID"].ToString(), orderid))
                        {
                            this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=" + this.imgMinus2 + " class='menutop'>");

                            this.menuBuilder.Append("<img align=absmiddle id=img" + this.mID + " src=" + this.imgFoldOpen + ">" + myRows[i]["ProductName"].ToString() + "\n");

                            this.menuBuilder.Append("\n</div>");

                            this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='margin-top=-3'>");
                        }
                        else
                        {
                            this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=" + this.imgPlus2 + " class='menutop'>");

                            this.menuBuilder.Append("<img align=absmiddle id=img" + this.mID + " src=" + this.imgFoldClose + ">" + myRows[i]["ProductName"].ToString() + "\n");

                            this.menuBuilder.Append("\n</div>");

                            this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='display:none;margin-top=-3'>");
                        }

                        MakePOrder((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgEmpty + ">", storeid, ZT);
                        this.menuBuilder.Append("\n</div>");

                    }
                    else
                    {
                        if (CheckOrderNum(myRows[i]["ProductID"].ToString(), orderid))
                        {
                            this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=" + this.imgMinus3 + " class='menutop'>");

                            this.menuBuilder.Append("<img align=absmiddle id=img" + this.mID + " src=" + this.imgFoldOpen + ">" + myRows[i]["ProductName"].ToString() + "\n");

                            this.menuBuilder.Append("\n</div>");

                            this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='margin-top=-3'>");
                        }
                        else
                        {
                            this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=" + this.imgPlus3 + " class='menutop'>");

                            this.menuBuilder.Append("<img align=absmiddle id=img" + this.mID + " src=" + this.imgFoldClose + ">" + myRows[i]["ProductName"].ToString() + "\n");

                            this.menuBuilder.Append("\n</div>");

                            this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='display:none;margin-top=-3'>");
                        }

                        MakePOrder((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgLine1 + ">", storeid, ZT);
                        this.menuBuilder.Append("\n</div>");
                    }
                }
            }
        }

        private bool CheckOrderNum(string pid, string orderid)
        {
            DataTable dt = DBHelper.ExecuteDataTable("select productid from product where pid=" + pid);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (DBHelper.ExecuteScalar("select count(id) from orderdetail where  storeorderid='" + orderid + "' and  productid=" + dt.Rows[i]["productid"].ToString()).ToString() != "0")
                {
                    return true;
                }
                else
                {
                    continue;
                }
            }

            return false;
        }


        private bool CheckStockNum(string pid, string storeid)
        {
            DataTable dt = DBHelper.ExecuteDataTable("select productid from product where pid=" + pid);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (DBHelper.ExecuteScalar("select isnull(sum(ActualStorage+HasOrderCount),0) from stock where (ActualStorage+HasOrderCount)<0 and storeid='" + storeid + "' and  productid=" + dt.Rows[i]["productid"].ToString()).ToString() != "0")
                {
                    return true;
                }
                else
                {
                    continue;
                }
            }

            return false;
        }



        /// <summary>
        /// 店铺定货时产生非修改菜单
        /// </summary>
        private void MakePOrder(int pID, string pLayer, string storeid, int ZT)
        {
            int i;
            DataRow[] myRows = this.pTable.Select("pID=" + pID.ToString(), "ProductID");
            if (myRows.Length == 0) return;
            for (i = 0; i < myRows.Length; i++)
            {
                this.mID++;
                if ((int)myRows[i]["isFold"] == 0)
                {
                    DataRow myRow = this.pListTable.NewRow();
                    myRow["ID"] = (int)myRows[i]["ProductID"];
                    myRow["price"] = (decimal)myRows[i]["PreferentialPrice"];
                    myRow["name"] = myRows[i]["ProductName"].ToString();
                    this.pListTable.Rows.Add(myRow);
                    //获取出实际库存和在线数量的总和，然后赋值给变量Quantity
                    DataTable dt = OrderGoodsBLL.GetAllTityByStoreidAndProductid(storeid, Convert.ToInt32(myRows[i]["ProductID"]));
                    int Quantity = 0;
                    if (dt.Rows.Count > 0)
                    {
                        if (ZT == 1)
                        {
                            Quantity = Convert.ToInt32(dt.Rows[0]["Quantity"].ToString());
                        }
                        else
                        {
                            Quantity = 0;
                        }
                    }
                    else
                    {
                        Quantity = 0;
                    }
                    if (i == myRows.Length - 1)
                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle src=" + this.imgLine2 + "><img align=absmiddle src=" + this.imgItem + ">");
                    else
                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle src=" + this.imgLine3 + "><img align=absmiddle src=" + this.imgItem + ">");

                    this.menuBuilder.Append("<input maxLength=6 value=" + Quantity + " type=text class=priceBox onkeyup=\"Bind()\"  size=3 name=N" + myRows[i]["ProductID"] + "></input>");

                    this.menuBuilder.Append("\n\t    " + myRows[i]["ProductName"].ToString() + " <font color=black>会员价：" + (Convert.ToDouble(myRows[i]["PreferentialPrice"].ToString())).ToString("f2") + "；积分：" + (Convert.ToDouble(myRows[i]["PreferentialPV"].ToString())).ToString("f2") + "</font>\n");
                    this.menuBuilder.Append("\n<a href=\"javascript:void(window.open('../ProductIntro.aspx?ProductID=" + myRows[i]["ProductID"].ToString() + "','','width=450,height=240')) \"> </a><br>\n");
                }
                else
                {
                    this.menuBuilder.Append("\n<div>");

                    if (i == myRows.Length - 1)
                    {
                        if (CheckStockNum(myRows[i]["ProductID"].ToString(), storeid))
                        {
                            this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=" + this.imgMinus2 + " class='menutop'>");

                            this.menuBuilder.Append("<img align=absmiddle id=img" + this.mID + " src=" + this.imgFoldOpen + ">" + myRows[i]["ProductName"].ToString() + "\n");

                            this.menuBuilder.Append("\n</div>");

                            this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='margin-top=-3'>");
                        }
                        else
                        {
                            this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=" + this.imgPlus2 + " class='menutop'>");

                            this.menuBuilder.Append("<img align=absmiddle id=img" + this.mID + " src=" + this.imgFoldClose + ">" + myRows[i]["ProductName"].ToString() + "\n");

                            this.menuBuilder.Append("\n</div>");

                            this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='display:none;margin-top=-3'>");
                        }

                        MakePOrder((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgEmpty + ">", storeid, ZT);
                        this.menuBuilder.Append("\n</div>");

                    }
                    else
                    {
                        if (CheckStockNum(myRows[i]["ProductID"].ToString(), storeid))
                        {
                            this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=" + this.imgMinus3 + " class='menutop'>");

                            this.menuBuilder.Append("<img align=absmiddle id=img" + this.mID + " src=" + this.imgFoldOpen + ">" + myRows[i]["ProductName"].ToString() + "\n");

                            this.menuBuilder.Append("\n</div>");

                            this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='margin-top=-3'>");
                        }
                        else
                        {
                            this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=" + this.imgPlus3 + " class='menutop'>");

                            this.menuBuilder.Append("<img align=absmiddle id=img" + this.mID + " src=" + this.imgFoldClose + ">" + myRows[i]["ProductName"].ToString() + "\n");

                            this.menuBuilder.Append("\n</div>");

                            this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='display:none;margin-top=-3'>");
                        }

                        MakePOrder((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgLine1 + ">", storeid, ZT);
                        this.menuBuilder.Append("\n</div>");
                    }
                }
            }
        }

        public string getEditMenu11(string dian)
        {
            this.initPTable();

            this.getData10(dian);

            if (pTable.Rows.Count != 0)
            {
                this.menuBuilder.Append("<div>产品列表<div>");
                //this.menuBuilder.Append("<div>" + CommonData.GetClassTran("classes/producttree.cs_123", "产品列表") + "&nbsp;&nbsp;<div>");
                this.makeP10(1, "");
            }
            else
            {
                this.menuBuilder.Append("<div>" + "产品列表" + "&nbsp;&nbsp;" + "产品库中无记录！" + "<div>");
                //this.menuBuilder.Append("<div>" + CommonData.GetClassTran("classes/producttree.cs_128[1]", "产品列表") + "&nbsp;&nbsp;" + CommonData.GetClassTran("classes/producttree.cs_110[2]", "产品库中无记录！") + "<div>");
            }

            return this.menuBuilder.ToString();
        }

        private void getData10(string storId) //读取指定店铺的产品数据
        {
            string ssQL = @"declare @num varchar(20)
        select @num=scpccode from storeInfo where storeId='" + storId + "' set @num=substring(@num,1, 2)     Select a.PID, a.isFold , a.ProductID , a.ProductName , a.CommonPrice , a.CommonPV , a.PreferentialPrice , a.PreferentialPV, 0 As num From Product a where isSell=0 and countryCode=@num";	//Order By ProductID
            this.pTable = DAL.DBHelper.ExecuteDataTable(ssQL);
        }

        private void makeP10(int pID, string pLayer)
        {
            int i;

            DataRow[] myRows = this.pTable.Select("pID=" + pID.ToString(), "ProductID");

            if (myRows.Length == 0) return;


            for (i = 0; i < myRows.Length; i++)
            {
                this.mID++;

                if ((int)myRows[i]["isFold"] == 0)
                {
                    /*PID, isFold , ProductID , ProductName , CommonPrice , CommonPV , PreferentialPrice , PreferentialPV, */

                    DataRow myRow = this.pListTable.NewRow();
                    myRow["ID"] = (int)myRows[i]["ProductID"];
                    myRow["price"] = (decimal)myRows[i]["PreferentialPrice"];
                    myRow["name"] = myRows[i]["ProductName"].ToString();
                    this.pListTable.Rows.Add(myRow);

                    if (i == myRows.Length - 1)
                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle src=" + this.imgLine2 + "><img align=absmiddle src=" + this.imgItem + ">");
                    else
                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle src=" + this.imgLine3 + "><img align=absmiddle src=" + this.imgItem + ">");

                    this.menuBuilder.Append("<input maxLength=6 value=0 type=text class=priceBox  size=3 name=N" + myRows[i]["ProductID"] + " onblur='getTable()'" + "></input>");

                    this.menuBuilder.Append("\n" + myRows[i]["ProductName"].ToString());

                    this.menuBuilder.Append("\n<a href=\"javascript:void(window.open('../ProductIntro.aspx?ProductID=" + myRows[i]["ProductID"].ToString() + "','','width=450,height=240')) \">" + "查看" + "</a><br>\n");//CommonData.GetClassTran("classes/producttree.cs_167", "查看")


                }
                else
                {
                    this.menuBuilder.Append("\n<div>");

                    if (i == myRows.Length - 1)
                    {
                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=" + this.imgPlus2 + " class='menutop'>");

                        this.menuBuilder.Append("<img align=absmiddle id=img" + this.mID + " src=" + this.imgFoldClose + ">" + myRows[i]["ProductName"].ToString() + "\n");

                        this.menuBuilder.Append("\n</div>");

                        this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='display:none;margin-top=-3'>");

                        makeP((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgEmpty + ">");

                        this.menuBuilder.Append("\n</div>");

                    }
                    else
                    {
                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=" + this.imgPlus3 + " class='menutop'>");

                        this.menuBuilder.Append("<img align=absmiddle id=img" + this.mID + " src=" + this.imgFoldClose + ">" + myRows[i]["ProductName"].ToString() + "\n");

                        this.menuBuilder.Append("\n</div>");

                        this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='display:none;margin-top=-3'>");

                        makeP10((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgLine1 + ">");

                        this.menuBuilder.Append("\n</div>");
                    }
                }
            }
        }


        /// <summary>
        /// 自由注册专用
        /// </summary>
        /// <param name="dian"></param>
        /// <returns></returns>
        public string getEditMenu12(string dian)
        {
            this.initPTable();

            this.getData10(dian);

            if (pTable.Rows.Count != 0)
            {
                this.menuBuilder.Append("<div>" + this.GetTran("000542", "产品列表") + "<div>");
                //this.menuBuilder.Append("<div>" + CommonData.GetClassTran("classes/producttree.cs_123", "产品列表") + "&nbsp;&nbsp;<div>");
                this.makeP12(1, "");
            }
            else
            {
                this.menuBuilder.Append("<div>" + this.GetTran("000542", "产品列表") + "&nbsp;&nbsp;" + this.GetTran("000549", "产品库中无记录！") + "<div>");
                //this.menuBuilder.Append("<div>" + CommonData.GetClassTran("classes/producttree.cs_128[1]", "产品列表") + "&nbsp;&nbsp;" + CommonData.GetClassTran("classes/producttree.cs_110[2]", "产品库中无记录！") + "<div>");
            }

            return this.menuBuilder.ToString();
        }

        private void makeP12(int pID, string pLayer)
        {
            int i;

            DataRow[] myRows = this.pTable.Select("pID=" + pID.ToString(), "ProductID");

            if (myRows.Length == 0) return;


            for (i = 0; i < myRows.Length; i++)
            {
                this.mID++;

                if ((int)myRows[i]["isFold"] == 0)
                {
                    /*PID, isFold , ProductID , ProductName , CommonPrice , CommonPV , PreferentialPrice , PreferentialPV, */

                    DataRow myRow = this.pListTable.NewRow();
                    myRow["ID"] = (int)myRows[i]["ProductID"];
                    myRow["price"] = (decimal)myRows[i]["PreferentialPrice"];
                    myRow["name"] = myRows[i]["ProductName"].ToString();
                    this.pListTable.Rows.Add(myRow);

                    if (i == myRows.Length - 1)
                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle src=" + this.imgLine2 + "><img align=absmiddle src=" + this.imgItem + ">");
                    else
                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle src=" + this.imgLine3 + "><img align=absmiddle src=" + this.imgItem + ">");

                    this.menuBuilder.Append("<input maxLength=6 value=0 type=text class=priceBox  size=3 name=N" + myRows[i]["ProductID"] + " onblur='getTable()'" + "></input>");

                    this.menuBuilder.Append("\n" + myRows[i]["ProductName"].ToString());

                    this.menuBuilder.Append("\n<a href=\"javascript:void(window.open('ProductIntro.aspx?ProductID=" + myRows[i]["ProductID"].ToString() + "','','width=450,height=240')) \">" + "查看" + "</a><br>\n");//CommonData.GetClassTran("classes/producttree.cs_167", "查看")


                }
                else
                {
                    this.menuBuilder.Append("\n<div>");

                    if (i == myRows.Length - 1)
                    {
                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=" + this.imgPlus2 + " class='menutop'>");

                        this.menuBuilder.Append("<img align=absmiddle id=img" + this.mID + " src=" + this.imgFoldClose + ">" + myRows[i]["ProductName"].ToString() + "\n");

                        this.menuBuilder.Append("\n</div>");

                        this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='display:none;margin-top=-3'>");

                        makeP12((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgEmpty + ">");

                        this.menuBuilder.Append("\n</div>");

                    }
                    else
                    {
                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=" + this.imgPlus3 + " class='menutop'>");

                        this.menuBuilder.Append("<img align=absmiddle id=img" + this.mID + " src=" + this.imgFoldClose + ">" + myRows[i]["ProductName"].ToString() + "\n");

                        this.menuBuilder.Append("\n</div>");

                        this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='display:none;margin-top=-3'>");

                        makeP12((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgLine1 + ">");

                        this.menuBuilder.Append("\n</div>");
                    }
                }
            }
        }

    }
}
