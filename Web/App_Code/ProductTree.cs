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

//Add Namespace
using System.Data.SqlClient;
using DAL;
using BLL.Logistics;
using BLL.other.Company;
using BLL.CommonClass;
using System.Text;
using System.Collections;
using Model;

/*
 * 修改者：汪华
 * 修改时间：2009-09-04
 */
/// <summary>
///ProductTree 的摘要说明
/// </summary>
public class ProductTree : BLL.TranslationBase
{
    private int mID = 1;
    private StringBuilder menuBuilder;
    private DataTable pListTable;
    private DataTable pTable;

    public string imgLine1 = "../Company/images/line1.gif";
    public string imgLine2 = "../Company/images/line2.gif";
    public string imgLine3 = "../Company/images/line3.gif";

    public string imgPlus2 = "../Company/images/plus2.gif";
    public string imgPlus3 = "../Company/images/plus3.gif";

    public string imgEmpty = "../Company/images/EMPTY.GIF";

    public string imgFoldClose = "../Company/images/foldclose.gif";
    public string imgFoldOpen = "../Company/images/foldopen.gif";
    public string imgItem = "../Company/images/item.gif";

    public string imgMinus2 = "../Company/images/minus2.gif";
    public string imgMinus3 = "../Company/images/minus3.gif";

    private string _defaultProductNum = "0";

    public ProductTree()
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
            //this.menuBuilder.Append("<div>" + CommonData.GetClassTran("classes/producttree.cs_123", BLL.Translation.Translate("000542", "产品列表")) + "&nbsp;&nbsp;<div>");
            this._makeP(1, "");
        }
        else
        {
            this.menuBuilder.Append("<div>" + BLL.Translation.Translate("000542", "产品列表") + "&nbsp;&nbsp;" + BLL.Translation.Translate("000549", "产品库中无记录！") + "<div>");
            //this.menuBuilder.Append("<div>" + CommonData.GetClassTran("classes/producttree.cs_128[1]", BLL.Translation.Translate("000542", "产品列表")) + "&nbsp;&nbsp;" + CommonData.GetClassTran("classes/producttree.cs_110[2]", BLL.Translation.Translate("000549", "产品库中无记录！")) + "<div>");
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

                //this.menuBuilder.Append("\n <img  onmousemove=\"javascript:ShowProductDiv(this,'" + myRows[i]["ProductID"].ToString() + "');\"  onmouseout=\"javascript:HideProductDiv(this);\" src=\"../Company/images/fdj.gif\" /> <br>\n");
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
        myDataColumn.DataType = Type.GetType("System.Decimal");
        myDataColumn.ColumnName = "pv";
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
    /// 会员系统复消树状产品列表
    /// </summary>
    /// <param name="dian"></param>
    /// <returns></returns>
    public string getMemberProductList(string dian)
    {
        this.initPTable();

        this.getData10Again(dian);

        if (pTable.Rows.Count != 0)
        {
            //this.menuBuilder.Append("<div>" + BLL.Translation.Translate("000542", "产品列表") + "&nbsp;&nbsp;<div>");
            this.makeP10(1, "");
        }
        else
        {
            //this.menuBuilder.Append("<div>" + CommonData.GetClassTran("classes/producttree.cs_128[1]", BLL.Translation.Translate("000542", "产品列表")) + "&nbsp;&nbsp;" + CommonData.GetClassTran("classes/producttree.cs_110[2]", BLL.Translation.Translate("000549", "产品库中无记录！")) + "<div>");
            //+ BLL.Translation.Translate("000542", "产品列表") + "&nbsp;&nbsp;" 
            this.menuBuilder.Append("<div>" + BLL.Translation.Translate("000549", "产品库中无记录！") + "<div>");
        }

        return this.menuBuilder.ToString();
    }




    /// <summary>
    /// 组合产品树（汪华）（2009-10-15所写）
    /// </summary>
    /// <param name="countryCode">CountryId</param>
    /// <returns>返回非修改菜单组合产品用</returns>
    public string getMenuZhuHe(string countryCode)
    {
        this.initPTable();

        this.getCombineProduct(countryCode);

        if (pTable.Rows.Count != 0)
        {
        
            this.menuBuilder.Append("<div>" + BLL.Translation.Translate("000542", "产品列表") + "&nbsp;&nbsp;<div>");
            this.makePZhuHe(1, "");
        }
        else
        {
            this.menuBuilder.Append("<div>" + BLL.Translation.Translate("000542", "产品列表") + "&nbsp;&nbsp;" + BLL.Translation.Translate("000549", "产品库中无记录！") + "<div>");
        }

        return this.menuBuilder.ToString();
    }

    public string getMenu(string dian)
    {
        this.initPTable();

        this.getData(dian);

        if (pTable.Rows.Count != 0)
        {
            this.menuBuilder.Append("<div>" + BLL.Translation.Translate("000542", "产品列表") + "&nbsp;&nbsp;<div>");
            this.makeP(1, "");
        }
        else
        {
            //this.menuBuilder.Append("<div>" + CommonData.GetClassTran("classes/producttree.cs_128[1]", BLL.Translation.Translate("000542", "产品列表")) + "&nbsp;&nbsp;" + CommonData.GetClassTran("classes/producttree.cs_110[2]", BLL.Translation.Translate("000549", "产品库中无记录！")) + "<div>");
            this.menuBuilder.Append("<div>" + BLL.Translation.Translate("000542", "产品列表") + "&nbsp;&nbsp;" + BLL.Translation.Translate("000549", "产品库中无记录！") + "<div>");
        }

        return this.menuBuilder.ToString();
    }


    public string getEditMenu11(string dian)
    {
        this.initPTable();

        this.getData10(dian);

        if (pTable.Rows.Count != 0)
        {
            this.menuBuilder.Append("<div>" + this.GetTran("000542", "产品列表") + "<div>");
            this.makeP10(1, "");
        }
        else
        {
            this.menuBuilder.Append("<div>" + BLL.Translation.Translate("000542", "产品列表") + "&nbsp;&nbsp;" + BLL.Translation.Translate("000549", "产品库中无记录！") + "<div>");
        }

        return this.menuBuilder.ToString();
    }



    public string getEditMenuShopp(string dian, ArrayList list)
    {
        this.initPTable();

        this.getData10Again(dian);

        if (pTable.Rows.Count != 0)
        {
            this.menuBuilder.Append("<div>" + BLL.Translation.Translate("000542", "产品列表") + "<div>");
            this.EditMakePOrderShopp(1, "", dian, 1, list); ;
        }
        else
        {
            this.menuBuilder.Append("<div>" + BLL.Translation.Translate("000542", "产品列表") + "&nbsp;&nbsp;" + BLL.Translation.Translate("000549", "产品库中无记录！") + "<div>");
        }

        return this.menuBuilder.ToString();
    }

    public string getEditMenu10(string dian, string orderId)
    {
        this.initPTable();

        this.getData10(dian,orderId);

        if (pTable.Rows.Count != 0)
        {
            this.menuBuilder.Append("<div>" + BLL.Translation.Translate("000542", "产品列表") + "<div>");
            this.EditMakePOrder10(1, "", dian, 1, orderId); ;
        }
        else
        {
            this.menuBuilder.Append("<div>" + BLL.Translation.Translate("000542", "产品列表") + "&nbsp;&nbsp;" + BLL.Translation.Translate("000549", "产品库中无记录！") + "<div>");
        }

        return this.menuBuilder.ToString();
    }

    public string getEditMenu10Again(string dian, string orderId)
    {
        this.initPTable();

        this.getData10Again(dian, orderId);

        if (pTable.Rows.Count != 0)
        {
            this.menuBuilder.Append("<div>" + BLL.Translation.Translate("000542", "产品列表") + "<div>");
            this.EditMakePOrder10(1, "", dian, 1, orderId); ;
        }
        else
        {
            this.menuBuilder.Append("<div>" + BLL.Translation.Translate("000542", "产品列表") + "&nbsp;&nbsp;" + BLL.Translation.Translate("000549", "产品库中无记录！") + "<div>");
        }

        return this.menuBuilder.ToString();
    }
    public string getEditMenuShopping(string dian, string productid)
    {
        this.initPTable();

        this.getDataShopping(dian);

        if (pTable.Rows.Count != 0)
        {
            this.menuBuilder.Append("<div>" + BLL.Translation.Translate("000542", "产品列表") + "<div>");
            this.getEditMenuShopping(1, "", dian, 1, productid); ;
        }
        else
        {
            this.menuBuilder.Append("<div>" + BLL.Translation.Translate("000542", "产品列表") + "&nbsp;&nbsp;" + BLL.Translation.Translate("000549", "产品库中无记录！") + "<div>");
        }

        return this.menuBuilder.ToString();
    }

    public string getMenuShopping(string dian)
    {
        this.initPTable();

        this.getDataShopping(dian);

        if (pTable.Rows.Count != 0)
        {
            this.menuBuilder.Append("<div>" + BLL.Translation.Translate("000542", "产品列表") + "&nbsp;&nbsp;<div>");
            this.makePShopping(1, "");
        }
        else
        {
            //this.menuBuilder.Append("<div>" + CommonData.GetClassTran("classes/producttree.cs_128[1]", BLL.Translation.Translate("000542", "产品列表")) + "&nbsp;&nbsp;" + CommonData.GetClassTran("classes/producttree.cs_110[2]", BLL.Translation.Translate("000549", "产品库中无记录！")) + "<div>");
            this.menuBuilder.Append("<div>" + BLL.Translation.Translate("000542", "产品列表") + "&nbsp;&nbsp;" + BLL.Translation.Translate("000549", "产品库中无记录！") + "<div>");
        }

        return this.menuBuilder.ToString();
    }

    public string getMenuShoppingFx(string dian)
    {
        this.initPTable();

        this.getDataShopping(dian);

        if (pTable.Rows.Count != 0)
        {
            this.menuBuilder.Append("<div>" + BLL.Translation.Translate("000542", "产品列表") + "&nbsp;&nbsp;<div>");
            this.makePShoppingFx(1, "");
        }
        else
        {
            //this.menuBuilder.Append("<div>" + CommonData.GetClassTran("classes/producttree.cs_128[1]", BLL.Translation.Translate("000542", "产品列表")) + "&nbsp;&nbsp;" + CommonData.GetClassTran("classes/producttree.cs_110[2]", BLL.Translation.Translate("000549", "产品库中无记录！")) + "<div>");
            this.menuBuilder.Append("<div>" + BLL.Translation.Translate("000542", "产品列表") + "&nbsp;&nbsp;" + BLL.Translation.Translate("000549", "产品库中无记录！") + "<div>");
        }

        return this.menuBuilder.ToString();
    }


    public string getMenu10(string dian)
    {
        this.initPTable();

        this.getData10(dian);

        if (pTable.Rows.Count != 0)
        {
            this.menuBuilder.Append("<div>" + BLL.Translation.Translate("000542", "产品列表") + "&nbsp;&nbsp;<div>");
            this.makeP10(1, "");
        }
        else
        {
            //this.menuBuilder.Append("<div>" + CommonData.GetClassTran("classes/producttree.cs_128[1]", BLL.Translation.Translate("000542", "产品列表")) + "&nbsp;&nbsp;" + CommonData.GetClassTran("classes/producttree.cs_110[2]", BLL.Translation.Translate("000549", "产品库中无记录！")) + "<div>");
            this.menuBuilder.Append("<div>" + BLL.Translation.Translate("000542", "产品列表") + "&nbsp;&nbsp;" + BLL.Translation.Translate("000549", "产品库中无记录！") + "<div>");
        }

        return this.menuBuilder.ToString();
    }


    public string getMenu10Again(string dian)
    {
        this.initPTable();

        this.getData10Again(dian);

        if (pTable.Rows.Count != 0)
        {
            this.menuBuilder.Append("<div>" + BLL.Translation.Translate("000542", "产品列表") + "&nbsp;&nbsp;<div>");
            this.makeP10(1, "");
        }
        else
        {
            //this.menuBuilder.Append("<div>" + CommonData.GetClassTran("classes/producttree.cs_128[1]", BLL.Translation.Translate("000542", "产品列表")) + "&nbsp;&nbsp;" + CommonData.GetClassTran("classes/producttree.cs_110[2]", BLL.Translation.Translate("000549", "产品库中无记录！")) + "<div>");
            this.menuBuilder.Append("<div>" + BLL.Translation.Translate("000542", "产品列表") + "&nbsp;&nbsp;" + BLL.Translation.Translate("000549", "产品库中无记录！") + "<div>");
        }

        return this.menuBuilder.ToString();
    }

    public string getMenuStore(string dian)
    {
        this.initPTable();

        this.getData10(dian);

        if (pTable.Rows.Count != 0)
        {
            this.menuBuilder.Append("<div>" + BLL.Translation.Translate("000542", "产品列表") + "&nbsp;&nbsp;<div>");
            this.makePStore(1, "");
        }
        else
        {
          
            this.menuBuilder.Append("<div>" + BLL.Translation.Translate("000542", "产品列表") + "&nbsp;&nbsp;" + BLL.Translation.Translate("000549", "产品库中无记录！") + "<div>");
        }

        return this.menuBuilder.ToString();
    }

    public string getMenuStore(string dian,int mType,int oType)
    {
        this.initPTable();

        this.getData10(dian);

        if (pTable.Rows.Count != 0)
        {
            this.menuBuilder.Append("<div>" + BLL.Translation.Translate("000542", "产品列表") + "&nbsp;&nbsp;<div>");
            this.makePStore(1, "",mType,oType);
        }
        else
        {

            this.menuBuilder.Append("<div>" + BLL.Translation.Translate("000542", "产品列表") + "&nbsp;&nbsp;" + BLL.Translation.Translate("000549", "产品库中无记录！") + "<div>");
        }

        return this.menuBuilder.ToString();
    }

    public string getMenuStoreFx(string dian, int mType, int oType)
    {
        this.initPTable();

        this.getData10(dian);

        if (pTable.Rows.Count != 0)
        {
            this.menuBuilder.Append("<div>" + BLL.Translation.Translate("000542", "产品列表") + "&nbsp;&nbsp;<div>");
            this.makePStoreFx(1, "", mType, oType);
        }
        else
        {

            this.menuBuilder.Append("<div>" + BLL.Translation.Translate("000542", "产品列表") + "&nbsp;&nbsp;" + BLL.Translation.Translate("000549", "产品库中无记录！") + "<div>");
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
                myRow["pv"] = (decimal)myRows[i]["PreferentialPV"];
                myRow["name"] = myRows[i]["ProductName"].ToString();
                myRow["productcode"] = myRows[i]["productcode"].ToString();
                this.pListTable.Rows.Add(myRow);

                if (i == myRows.Length - 1)
                    this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle src=" + this.imgLine2 + "><img align=absmiddle src=" + this.imgItem + ">");
                else
                    this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle src=" + this.imgLine3 + "><img align=absmiddle src=" + this.imgItem + ">");

                this.menuBuilder.Append("\n\t  <span style='cursor:pointer' onmouseover=\"this.style.color='red';this.style.textDecoration='underline';\" onmouseout=\"this.style.color='';this.style.textDecoration='';\" onclick=\"createTable('" + myRows[i]["ProductID"] + "')\">" + myRows[i]["productcode"].ToString() + " </span>\n");
                this.menuBuilder.Append("\n\t    " + myRows[i]["ProductName"].ToString() + " <font color=#669900> " + (Convert.ToDouble(myRows[i]["PreferentialPrice"].ToString())).ToString("f2") + "\n\t " + (Convert.ToDouble(myRows[i]["PreferentialPv"].ToString())).ToString("f2") + "</font><br>\n");

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

    private void makePShopping(int pID, string pLayer)
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

                this.menuBuilder.Append("<input maxLength=6 value=0 type=text class=priceBox onkeyup=\"Bind()\" size=3 name=N" + myRows[i]["ProductID"] + "></input>");

                this.menuBuilder.Append("\n" + myRows[i]["ProductName"].ToString());

                this.menuBuilder.Append("\n<a href=\"javascript:void(window.open('../ProductIntro.aspx?ProductID=" + myRows[i]["ProductID"].ToString() + "','','width=450,height=240')) \">" + this.GetTran("000440", "查看") + "</a><br>\n");//CommonData.GetClassTran("classes/producttree.cs_167", "查看")
     

            }
            else
            {
                this.menuBuilder.Append("\n<div style='height:auto;'>");

                if (i == myRows.Length - 1)
                {
                    this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=" + this.imgPlus2 + " class='menutop' >");

                    this.menuBuilder.Append("<img align=absmiddle id=img" + this.mID + " src=" + this.imgFoldClose + "><a style='padding-top:3px;' href=\"ShopingList.aspx?pid=" + myRows[i]["ProductID"].ToString() + "\">" + myRows[i]["ProductName"].ToString() + "</a>\n");
                    this.menuBuilder.Append("\n</div>");

                    this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='display:none;margin-top=-3'>");

                    makePShopping((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgEmpty + ">");

                    this.menuBuilder.Append("\n</div>");

                }
                else
                {
                    this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=" + this.imgPlus3 + " class='menutop'>");

                    this.menuBuilder.Append("<img align=absmiddle id=img" + this.mID + " src=" + this.imgFoldClose + "><a style='padding-top:3px;' href=\"ShopingList.aspx?pid=" + myRows[i]["ProductID"].ToString() + "\">" + myRows[i]["ProductName"].ToString() + "</a>\n");

                    this.menuBuilder.Append("\n</div>");

                    this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='display:none;margin-top=-3'>");

                    makePShopping((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgLine1 + ">");

                    this.menuBuilder.Append("\n</div>");
                }
            }
        }
    }

    private void makePShoppingFx(int pID, string pLayer)
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

                this.menuBuilder.Append("<input maxLength=6 value=0 type=text class=priceBox onkeyup=\"Bind()\" size=3 name=N" + myRows[i]["ProductID"] + "></input>");

                this.menuBuilder.Append("\n" + myRows[i]["ProductName"].ToString());

                this.menuBuilder.Append("\n<a href=\"javascript:void(window.open('../ProductIntro.aspx?ProductID=" + myRows[i]["ProductID"].ToString() + "','','width=450,height=240')) \">" + this.GetTran("000440", "查看") + "</a><br>\n");//CommonData.GetClassTran("classes/producttree.cs_167", "查看")


            }
            else
            {
                this.menuBuilder.Append("\n<div style='height:auto;'>");

                if (i == myRows.Length - 1)
                {
                    this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=" + this.imgPlus2 + " class='menutop' >");

                    this.menuBuilder.Append("<img align=absmiddle id=img" + this.mID + " src=" + this.imgFoldClose + "><a style='padding-top:3px;' href=\"ShopingListAgain.aspx?pid=" + myRows[i]["ProductID"].ToString() + "\">" + myRows[i]["ProductName"].ToString() + "</a>\n");
                    this.menuBuilder.Append("\n</div>");

                    this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='display:none;margin-top=-3'>");

                    makePShoppingFx((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgEmpty + ">");

                    this.menuBuilder.Append("\n</div>");

                }
                else
                {
                    this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=" + this.imgPlus3 + " class='menutop'>");

                    this.menuBuilder.Append("<img align=absmiddle id=img" + this.mID + " src=" + this.imgFoldClose + "><a style='padding-top:3px;' href=\"ShopingListAgain.aspx?pid=" + myRows[i]["ProductID"].ToString() + "\">" + myRows[i]["ProductName"].ToString() + "</a>\n");

                    this.menuBuilder.Append("\n</div>");

                    this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='display:none;margin-top=-3'>");

                    makePShoppingFx((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgLine1 + ">");

                    this.menuBuilder.Append("\n</div>");
                }
            }
        }
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

                this.menuBuilder.Append("<input maxLength=6 value=0 type=text class='shopCenRform' onkeyup=\"Bind();setpids(this);\" size=3 name=N" + myRows[i]["ProductID"] + "></input>");

                this.menuBuilder.Append("\n" + myRows[i]["ProductName"].ToString());

                this.menuBuilder.Append("\n<a href=\"#\" onmousemove=\"javascript:ShowProductDiv(this,'" + myRows[i]["ProductID"].ToString() + "');\"  onmouseout=\"javascript:HideProductDiv(this);\" ) >" + this.GetTran("000440", "查看") + "</a><br>\n");//CommonData.GetClassTran("classes/producttree.cs_167", "查看")
                //				

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

                    makeP10((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgEmpty + ">");

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

    private void makePStore(int pID, string pLayer)
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

                this.menuBuilder.Append("<input maxLength=6 value=0 type=text class=priceBox  size=3 name=N" + myRows[i]["ProductID"] + " onkeyup='Bind();setpids(this);GetCart(this," + myRows[i]["ProductID"] + ");'></input>");

                this.menuBuilder.Append("\n" + myRows[i]["ProductName"].ToString());

                this.menuBuilder.Append("\n<a href=\"#\" onmousemove=\"javascript:ShowProductDiv(this,'" + myRows[i]["ProductID"].ToString() + "');\"  onmouseout=\"javascript:HideProductDiv(this);\" ) >" + this.GetTran("000440", "查看") + "</a><br>\n");//CommonData.GetClassTran("classes/producttree.cs_167", "查看") 
             

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

                    makePStore((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgEmpty + ">");

                    this.menuBuilder.Append("\n</div>");

                }
                else
                {
                    this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=" + this.imgPlus3 + " class='menutop'>");

                    this.menuBuilder.Append("<img align=absmiddle id=img" + this.mID + " src=" + this.imgFoldClose + ">" + myRows[i]["ProductName"].ToString() + "\n");

                    this.menuBuilder.Append("\n</div>");

                    this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='display:none;margin-top=-3'>");

                    makePStore((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgLine1 + ">");

                    this.menuBuilder.Append("\n</div>");
                }
            }
        }
    }

    private void makePStore(int pID, string pLayer,int mType,int oType)
    {
        int i;

        DataTable dt;

        MemberInfoModel mim = ((MemberInfoModel)HttpContext.Current.Session["mbreginfo"]);

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

                dt = DBHelper.ExecuteDataTable("select top 1 * from memshopcart where memBh='" + mim.Number + "' and mType='" + mType + "' and odType='" + oType + "' and proId=" + myRows[i]["ProductID"].ToString());

                if (dt.Rows.Count>0)
                {
                    this.menuBuilder.Append("<input maxLength=6 value=" + dt.Rows[0]["proNum"]+ " type=text class=priceBox  size=3 name=N" + myRows[i]["ProductID"] + " onkeyup='Bind();setpids(this);GetCart(this," + myRows[i]["ProductID"] + ");'></input>");
                }
                else
                {
                    this.menuBuilder.Append("<input maxLength=6 value=0 type=text class=priceBox  size=3 name=N" + myRows[i]["ProductID"] + " onkeyup='Bind();setpids(this);GetCart(this," + myRows[i]["ProductID"] + ");'></input>");
                }
               
                this.menuBuilder.Append("\n" + myRows[i]["ProductName"].ToString());

                this.menuBuilder.Append("\n <img  onmousemove=\"javascript:ShowProductDiv(this,'" + myRows[i]["ProductID"].ToString() + "');\"  onmouseout=\"javascript:HideProductDiv(this);\" src=\"../Company/images/fdj.gif\" /> <br>\n");

                //this.menuBuilder.Append("\n<a href=\"#\" onmousemove=\"javascript:ShowProductDiv(this,'" + myRows[i]["ProductID"].ToString() + "');\"  onmouseout=\"javascript:HideProductDiv(this);\" ) >" + this.GetTran("000440", "查看") + "</a><br>\n");//CommonData.GetClassTran("classes/producttree.cs_167", "查看") 


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

                    makePStore((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgEmpty + ">", mType,oType);

                    this.menuBuilder.Append("\n</div>");

                }
                else
                {
                    this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=" + this.imgPlus3 + " class='menutop'>");

                    this.menuBuilder.Append("<img align=absmiddle id=img" + this.mID + " src=" + this.imgFoldClose + ">" + myRows[i]["ProductName"].ToString() + "\n");

                    this.menuBuilder.Append("\n</div>");

                    this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='display:none;margin-top=-3'>");

                    makePStore((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgLine1 + ">", mType, oType);

                    this.menuBuilder.Append("\n</div>");
                }
            }
        }
    }

    private void makePStoreFx(int pID, string pLayer, int mType, int oType)
    {
        int i;

        DataTable dt;

        OrderFinalModel mim = ((OrderFinalModel)HttpContext.Current.Session["fxMemberModel"]);

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

                dt = DBHelper.ExecuteDataTable("select top 1 * from memshopcart where memBh='" + mim.Number + "' and mType='" + mType + "' and odType='" + oType + "' and proId=" + myRows[i]["ProductID"].ToString());

                if (dt.Rows.Count > 0)
                {
                    this.menuBuilder.Append("<input maxLength=6 value=" + dt.Rows[0]["proNum"] + " type=text class=priceBox  size=3 name=N" + myRows[i]["ProductID"] + " onkeyup='Bind();setpids(this);GetCart(this," + myRows[i]["ProductID"] + ");'></input>");
                }
                else
                {
                    this.menuBuilder.Append("<input maxLength=6 value=0 type=text class=priceBox  size=3 name=N" + myRows[i]["ProductID"] + " onkeyup='Bind();setpids(this);GetCart(this," + myRows[i]["ProductID"] + ");'></input>");
                }

                this.menuBuilder.Append("\n" + myRows[i]["ProductName"].ToString());

                this.menuBuilder.Append("\n <img  onmousemove=\"javascript:ShowProductDiv(this,'" + myRows[i]["ProductID"].ToString() + "');\"  onmouseout=\"javascript:HideProductDiv(this);\" src=\"../Company/images/fdj.gif\" /> <br>\n");
                //this.menuBuilder.Append("\n<a href=\"#\" onmousemove=\"javascript:ShowProductDiv(this,'" + myRows[i]["ProductID"].ToString() + "');\"  onmouseout=\"javascript:HideProductDiv(this);\" ) >" + this.GetTran("000440", "查看") + "</a><br>\n");//CommonData.GetClassTran("classes/producttree.cs_167", "查看") 


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

                    makePStoreFx((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgEmpty + ">", mType, oType);

                    this.menuBuilder.Append("\n</div>");

                }
                else
                {
                    this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=" + this.imgPlus3 + " class='menutop'>");

                    this.menuBuilder.Append("<img align=absmiddle id=img" + this.mID + " src=" + this.imgFoldClose + ">" + myRows[i]["ProductName"].ToString() + "\n");

                    this.menuBuilder.Append("\n</div>");

                    this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='display:none;margin-top=-3'>");

                    makePStoreFx((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgLine1 + ">", mType, oType);

                    this.menuBuilder.Append("\n</div>");
                }
            }
        }
    }


    private void getEditMenuShopping(int pID, string pLayer, string storeid, int ZT, string productid)
    {
        int i;
        DataRow[] myRows = this.pTable.Select("pID=" + pID.ToString(), "ProductID");
        if (myRows.Length == 0) return;
        for (i = 0; i < myRows.Length; i++)
        {
            this.mID++;
            if ((int)myRows[i]["isFold"] == 0)
            {

            }
            else
            {
                this.menuBuilder.Append("\n<div>");

                if (i == myRows.Length - 1)
                {
                    if (CheckOrderNumShopping(myRows[i]["ProductID"].ToString(), productid))
                    {
                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=" + this.imgMinus2 + " class='menutop'>");

                        this.menuBuilder.Append("<img align=absmiddle id=img" + this.mID + " src=" + this.imgFoldOpen + "><a href=\"ShoppingCart.aspx?pid=" + myRows[i]["ProductID"].ToString() + "\">" + myRows[i]["ProductName"].ToString() + "</a>\n");

                        this.menuBuilder.Append("\n</div>");

                        this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='margin-top=-3'>");
                    }
                    else
                    {
                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=" + this.imgPlus2 + " class='menutop'>");

                        this.menuBuilder.Append("<img align=absmiddle id=img" + this.mID + " src=" + this.imgFoldClose + "><a href=\"ShoppingCart.aspx?pid=" + myRows[i]["ProductID"].ToString() + "\">" + myRows[i]["ProductName"].ToString() + "</a>\n");

                        this.menuBuilder.Append("\n</div>");

                        this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='display:none;margin-top=-3'>");
                    }

                    getEditMenuShopping((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgEmpty + ">", storeid, ZT, productid);
                    this.menuBuilder.Append("\n</div>");

                }
                else
                {
                    if (CheckOrderNumShopping(myRows[i]["ProductID"].ToString(), productid))
                    {
                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=" + this.imgMinus3 + " class='menutop'>");

                        this.menuBuilder.Append("<img align=absmiddle id=img" + this.mID + " src=" + this.imgFoldOpen + "><a href=\"ShoppingCart.aspx?pid=" + myRows[i]["ProductID"].ToString() + "\">" + myRows[i]["ProductName"].ToString() + "</a>\n");

                        this.menuBuilder.Append("\n</div>");

                        this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='margin-top=-3'>");
                    }
                    else
                    {
                        this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=" + this.imgPlus3 + " class='menutop'>");

                        this.menuBuilder.Append("<img align=absmiddle id=img" + this.mID + " src=" + this.imgFoldClose + "><a href=\"ShoppingCart.aspx?pid=" + myRows[i]["ProductID"].ToString() + "\">" + myRows[i]["ProductName"].ToString() + "</a>\n");

                        this.menuBuilder.Append("\n</div>");

                        this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='display:none;margin-top=-3'>");
                    }

                    getEditMenuShopping((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgLine1 + ">", storeid, ZT, productid);
                    this.menuBuilder.Append("\n</div>");
                }
            }
        }
    }


    private void EditMakePOrderShopp(int pID, string pLayer, string storeid, int ZT, ArrayList list)
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
              //  DataTable dt = OrderGoodsBLL.GetAllTityByStoreidAndProductid(storeid, Convert.ToInt32(myRows[i]["ProductID"]));
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

                this.menuBuilder.Append("<input maxLength=6 value=" + Quantity + " onkeyup='Bind();setpids(this);' type=text class=priceBox   size=3 name=N" + myRows[i]["ProductID"] + "></input>");
                this.menuBuilder.Append(myRows[i]["ProductName"].ToString());
                // this.menuBuilder.Append("\n\t    " + myRows[i]["ProductName"].ToString() + " <font color=black>会员价：" + (Convert.ToDouble(myRows[i]["PreferentialPrice"].ToString())).ToString("f2") + "；积分：" + (Convert.ToDouble(myRows[i]["PreferentialPV"].ToString())).ToString("f2") + "</font>\n");
                this.menuBuilder.Append("\n<a href=\"#\" onmousemove=\"javascript:ShowProductDiv(this,'" + myRows[i]["ProductID"].ToString() + "');\"  onmouseout=\"javascript:HideProductDiv(this);\" ) >" + this.GetTran("000440", "查看") + " </a><br>\n");//查看
            }
            else
            {
                this.menuBuilder.Append("\n<div>");

                if (i == myRows.Length - 1)
                {
                    if (CheckOrderNumShopp(myRows[i]["ProductID"].ToString(), list))
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

                    EditMakePOrderShopp((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgEmpty + ">", storeid, ZT, list);
                    this.menuBuilder.Append("\n</div>");

                }
                else
                {
                    if (CheckOrderNumShopp(myRows[i]["ProductID"].ToString(), list))
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

                    EditMakePOrderShopp((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgLine1 + ">", storeid, ZT, list);
                    this.menuBuilder.Append("\n</div>");
                }
            }
        }
    }

    private void EditMakePOrder10(int pID, string pLayer, string storeid, int ZT, string orderid)
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

                this.menuBuilder.Append("<input maxLength=6 value=" + Quantity + " onkeyup=\"Bind();setpids(this);\" type=text class=priceBox       size=3 name=N" + myRows[i]["ProductID"] + "></input>");
                this.menuBuilder.Append(myRows[i]["ProductName"].ToString());
                // this.menuBuilder.Append("\n\t    " + myRows[i]["ProductName"].ToString() + " <font color=black>会员价：" + (Convert.ToDouble(myRows[i]["PreferentialPrice"].ToString())).ToString("f2") + "；积分：" + (Convert.ToDouble(myRows[i]["PreferentialPV"].ToString())).ToString("f2") + "</font>\n");
                this.menuBuilder.Append("\n<a href=\"#\" onmousemove=\"javascript:ShowProductDiv(this,'" + myRows[i]["ProductID"].ToString() + "');\"  onmouseout=\"javascript:HideProductDiv(this);\" ) >" + this.GetTran("000440", "查看") + " </a><br>\n");//查看
            }
            else
            {
                this.menuBuilder.Append("\n<div>");

                if (i == myRows.Length - 1)
                {
                    if (CheckOrderNum10(myRows[i]["ProductID"].ToString(), orderid))
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

                    EditMakePOrder10((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgEmpty + ">", storeid, ZT, orderid);
                    this.menuBuilder.Append("\n</div>");

                }
                else
                {
                    if (CheckOrderNum10(myRows[i]["ProductID"].ToString(), orderid))
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

                    EditMakePOrder10((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgLine1 + ">", storeid, ZT, orderid);
                    this.menuBuilder.Append("\n</div>");
                }
            }
        }
    }
    //产生非修改菜单
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

                this.menuBuilder.Append("<input maxLength=6 value=0 type=text class=priceBox  size=3 name=N" + myRows[i]["ProductID"] + " onkeyup='getTable()'></input>");

                this.menuBuilder.Append("\n" + myRows[i]["ProductName"].ToString());

                this.menuBuilder.Append("\n<a href=\"javascript:void(window.open('../ProductIntro.aspx?ProductID=" + myRows[i]["ProductID"].ToString() + "','','width=450,height=240')) \">" + this.GetTran("000440", "查看") + "</a><br>\n");//CommonData.GetClassTran("classes/producttree.cs_167", "查看")
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

    //产生可修改菜单(汪华)（2009-10-16）---------------------------------DS2012
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
                    this.menuBuilder.Append("\n" + pLayer + "<img align=\"absmiddle\" src=\"" + this.imgLine2 + "\" /><img align=\"absmiddle\" src=\"" + this.imgItem + "\" />");

                else
                    this.menuBuilder.Append("\n" + pLayer + "<img align=\"absmiddle\" src=" + this.imgLine3 + "><img align=\"absmiddle\" src=\"" + this.imgItem + "\" />");

                if ((int)myRows[i]["isSell"] == 1)
                {
                    this.menuBuilder.Append("\n<font color='gray'>" + myRows[i]["ProductName"].ToString().Trim() + " " + myRows[i]["PreferentialPrice"].ToString() + " " + myRows[i]["PreferentialPV"].ToString() + "</font>\n");
                }
                else
                {
                    this.menuBuilder.Append("\n" + myRows[i]["ProductName"].ToString().Trim() + " " + myRows[i]["PreferentialPrice"].ToString() + " " + myRows[i]["PreferentialPV"].ToString() + "\n");
                }
                this.menuBuilder.Append("\n<a style=\"color:#075C79\" href=\"javascript:openAddWin('editItem'," + myRows[i]["ProductID"].ToString() + ")\">" + BLL.Translation.Translate("000259", "修改") + "</a>\n");
                this.menuBuilder.Append("\n<a style=\"color:#075C79\" href=\"javascript:openAddWin('deleteItem'," + myRows[i]["ProductID"].ToString() + ")\">" + BLL.Translation.Translate("000022", "删除") + "</a><br>\n");

            }
            else
            {
                this.menuBuilder.Append("\n<div>");
                if (i == myRows.Length - 1)
                {
                    this.menuBuilder.Append("\n" + pLayer + "<img align=\"absmiddle\" onclick=\"menuTree(menu" + this.mID + ",img" + this.mID + ",this)\" id=plus" + this.mID + " src=" + this.imgPlus2 + " class='menutop'>");

                    this.menuBuilder.Append("<img align=\"absmiddle\" id=\"img" + this.mID + "\" src=\"" + this.imgFoldClose + "\" />" + myRows[i]["ProductName"].ToString() + "\n");

                    this.menuBuilder.Append("\n<a style=\"color:#075C79\" href=\"javascript:openAddWin2('addFold'," + myRows[i]["ProductID"].ToString() + ",86)\">" + BLL.Translation.Translate("003228", "添加新类") + "</a>\n");

                    this.menuBuilder.Append("\n<a style=\"color:#075C79\" href=\"javascript:openAddWin2('add'," + myRows[i]["ProductID"].ToString() + ",86)\">" + BLL.Translation.Translate("005055", "添加新品") + "</a>\n");

                    this.menuBuilder.Append("\n<a style=\"color:#075C79\" href=\"javascript:openAddWin('editFold'," + myRows[i]["ProductID"].ToString() + ")\">" + BLL.Translation.Translate("000259", "修改") + "</a>\n");

                    this.menuBuilder.Append("\n<a style=\"color:#075C79\" href=\"javascript:openAddWin('deleteFold'," + myRows[i]["ProductID"].ToString() + ")\">" + BLL.Translation.Translate("000022", "删除") + "</a>\n");

                    this.menuBuilder.Append("\n</div>");

                    this.menuBuilder.Append("\n<div id=menu" + this.mID + " style=\"display:none;margin-top=-3\" >");

                    makeP3((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgEmpty + ">");

                    this.menuBuilder.Append("\n</div>");

                }
                else
                {
                    this.menuBuilder.Append("\n" + pLayer + "<img align=\"absmiddle\" onclick=\"menuTree(menu" + this.mID + ",img" + this.mID + ",this)\" id=plus" + this.mID + " src=" + this.imgPlus3 + " class=\"menutop\" />");

                    this.menuBuilder.Append("<img align=\"absmiddle\" id=\"img" + this.mID + "\" src=\"" + this.imgFoldClose + "\" />" + myRows[i]["ProductName"].ToString() + "\n");

                    this.menuBuilder.Append("\n<a style=\"color:#075C79\" href=\"javascript:openAddWin2('addFold'," + myRows[i]["ProductID"].ToString() + ",86)\">" + BLL.Translation.Translate("003228", "添加新类") + "</a>\n");

                    this.menuBuilder.Append("\n<a style=\"color:#075C79\" href=\"javascript:openAddWin2('add'," + myRows[i]["ProductID"].ToString() + ",86)\">" + BLL.Translation.Translate("005055", "添加新品") + "</a>\n");

                    this.menuBuilder.Append("\n<a style=\"color:#075C79\" href=\"javascript:openAddWin('editFold'," + myRows[i]["ProductID"].ToString() + ")\">" + BLL.Translation.Translate("000259", "修改") + "</a>\n");

                    this.menuBuilder.Append("\n<a style=\"color:#075C79\" href=\"javascript:openAddWin('deleteFold'," + myRows[i]["ProductID"].ToString() + ")\">" + BLL.Translation.Translate("000022", "删除") + "</a>\n");

                    this.menuBuilder.Append("\n</div>");

                    this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='display:none;margin-top=-3'>");

                    makeP3((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgLine1 + ">");

                    this.menuBuilder.Append("\n</div>");
                }
            }
        }
    }

    /// <summary>
    /// 带修改选项菜单(汪华)(2009-10-16修改)   ---DS2012
    /// 产品列表
    /// </summary>
    /// <param name="countryCode"></param>
    /// <returns></returns>
    public string GetProductTree(string countryCode)
    {
        this.getData_ByCountryCode(countryCode);

        this.menuBuilder.Append("<div>" + BLL.Translation.Translate("000542", "产品列表") + "&nbsp;&nbsp;<a style=\"color:#075C79\" href=\"javascript:openAddWin2('addFold',1," + countryCode + ")\"> " + BLL.Translation.Translate("003228", "添加新类") + "</a>&nbsp;&nbsp;<a style=\"color:#075C79\" href=\"javascript:openAddWin2('add',1," + countryCode + ")\"> " + BLL.Translation.Translate("006851", "添加新品") + "</a><div>");
        if (pTable.Rows.Count != 0)
        {
            this.makeP3(1, "");
        }
        else
        {
            this.menuBuilder.Append("<br>" + BLL.Translation.Translate("000549", "产品库中无记录！") + "<div>");
        }

        return this.menuBuilder.ToString();
    }


    public string GetSMSProductTree(string countryCode)
    {
        this.getData_BySMSCountryCode(countryCode);
        //this.menuBuilder.Append("<div>" + CommonDataBLL.GetClassTran("App_code/ProductTree.cs_579[1]", BLL.Translation.Translate("000542", "产品列表")) + "&nbsp;&nbsp;<a style=color:#075C79 href=javascript:openAddWin2('addFold',1," + Country.ToString() + ")> " + CommonDataBLL.GetClassTran("App_code/ProductTree.cs_579[2]", "添加新类") + "</a>&nbsp;&nbsp;<a style=color:#075C79 href=javascript:openAddWin2('add',1," + Country.ToString() + ")> " + CommonDataBLL.GetClassTran("App_code/ProductTree.cs_579[3]", "添加新品") + "</a><div>");
        //this.menuBuilder.Append("<div>" + BLL.Translation.Translate("000542", "产品列表") + "&nbsp;&nbsp;<a style=color:#075C79 href=javascript:openAddWin2('addFold',1," + Country.ToString() + ")> " + "添加新类" + "</a>&nbsp;&nbsp;<a style=color:#075C79 href=javascript:openAddWin2('add',1," + Country.ToString() + ")> " + "添加新品" + "</a><div>");
        this.menuBuilder.Append("<div>" + BLL.Translation.Translate("007020", "常用短信列表") + "&nbsp;&nbsp;<a \"style=color:#075C79\" href=\"javascript:openAddWin2('addFold',1," + countryCode + ")\"> " + BLL.Translation.Translate("003228", "添加新类") + "</a>&nbsp;&nbsp;<a style=\"color:#075C79\" href=\"javascript:openAddWin2('add',1," + countryCode + ")\"> " + BLL.Translation.Translate("007021", "添加短息类别") + "</a><div>");
        if (pTable.Rows.Count != 0)
        {
            this.makePSMS(1, "");
        }
        else
        {
            this.menuBuilder.Append("<br>" + BLL.Translation.Translate("000841", "没有相关记录！") + "<div>");
        }

        return this.menuBuilder.ToString();
    }

    public string GetSMSProductTreeXZ(string countryCode)
    {
        this.getData_BySMSCountryCode(countryCode);
        
        this.menuBuilder.Append("<div>" + BLL.Translation.Translate("007020", "常用短信列表") + "<div>");
        if (pTable.Rows.Count != 0)
        {
            this.makePSMSXZ(1, "");
        }
        else
        {
            this.menuBuilder.Append("<br>" + BLL.Translation.Translate("000841", "没有相关记录！") + "<div>");
        }

        return this.menuBuilder.ToString();
    }

    private void makePSMS(int pID, string pLayer)
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
                    this.menuBuilder.Append("\n" + pLayer + "<img align=\"absmiddle\" src=\"" + this.imgLine2 + "\" /><img align=\"absmiddle\" src=\"" + this.imgItem + "\" />");

                else
                    this.menuBuilder.Append("\n" + pLayer + "<img align=\"absmiddle\" src=" + this.imgLine3 + "><img align=\"absmiddle\" src=\"" + this.imgItem + "\" />");


                this.menuBuilder.Append("\n<a style=\"color:#075C79\" href=\"javascript:openSMScontent('add'," + myRows[i]["ProductID"].ToString() + ")\">" + myRows[i]["ProductName"].ToString().Trim() + "</a>\n");

                this.menuBuilder.Append("\n<a style=\"color:#075C79\" href=\"javascript:openAddWin('addsms'," + myRows[i]["ProductID"].ToString() + ")\">" + BLL.Translation.Translate("007022", "短息预设") + "</a>\n");

                this.menuBuilder.Append("\n<a style=\"color:#075C79\" href=\"javascript:openAddWin('editItem'," + myRows[i]["ProductID"].ToString() + ")\">" + BLL.Translation.Translate("000259", "修改") + "</a>\n");
                this.menuBuilder.Append("\n<a style=\"color:#075C79\" href=\"javascript:openAddWin('deleteItem'," + myRows[i]["ProductID"].ToString() + ")\">" + BLL.Translation.Translate("000022", "删除") + "</a><br>\n");

            }
            else
            {
                this.menuBuilder.Append("\n<div>");

                if (i == myRows.Length - 1)
                {
                    this.menuBuilder.Append("\n" + pLayer + "<img align=\"absmiddle\" onclick=\"menuTree(menu" + this.mID + ",img" + this.mID + ",this)\" id=plus" + this.mID + " src=" + this.imgPlus2 + " class='menutop'>");

                    //this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick=\"alert('zhc" + this.mID + "')\" id=plus" + this.mID + " src=" + this.imgPlus2 + " class='menutop'>");

                    this.menuBuilder.Append("<img align=\"absmiddle\" id=\"img" + this.mID + "\" src=\"" + this.imgFoldClose + "\" />" + myRows[i]["ProductName"].ToString() + "\n");

                   // this.menuBuilder.Append("\n<a style=\"color:#075C79\" href=\"javascript:openAddWin('addFold'," + myRows[i]["ProductID"].ToString() + ")\">" + BLL.Translation.Translate("003228", "添加新类") + "</a>\n");

                    this.menuBuilder.Append("\n<a style=\"color:#075C79\" href=\"javascript:openAddWin('add'," + myRows[i]["ProductID"].ToString() + ")\">" + BLL.Translation.Translate("007021", "添加短息类别") + "</a>\n");

                    this.menuBuilder.Append("\n<a style=\"color:#075C79\" href=\"javascript:openAddWin('editFold'," + myRows[i]["ProductID"].ToString() + ")\">" + BLL.Translation.Translate("000259", "修改") + "</a>\n");

                    this.menuBuilder.Append("\n<a style=\"color:#075C79\" href=\"javascript:openAddWin('deleteFold'," + myRows[i]["ProductID"].ToString() + ")\">" + BLL.Translation.Translate("000022", "删除") + "</a>\n");

                    this.menuBuilder.Append("\n</div>");

                    this.menuBuilder.Append("\n<div id=menu" + this.mID + " style=\"display:none;margin-top=-3\" >");

                    makePSMS((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgEmpty + ">");

                    this.menuBuilder.Append("\n</div>");

                }
                else
                {
                    this.menuBuilder.Append("\n" + pLayer + "<img align=\"absmiddle\" onclick=\"menuTree(menu" + this.mID + ",img" + this.mID + ",this)\" id=plus" + this.mID + " src=" + this.imgPlus3 + " class=\"menutop\" />");

                    this.menuBuilder.Append("<img align=\"absmiddle\" id=\"img" + this.mID + "\" src=\"" + this.imgFoldClose + "\" />" + myRows[i]["ProductName"].ToString() + "\n");

                   // this.menuBuilder.Append("\n<a style=\"color:#075C79\" href=\"javascript:openAddWin('addFold'," + myRows[i]["ProductID"].ToString() + ")\">" + BLL.Translation.Translate("003228", "添加新类") + "</a>\n");

                    this.menuBuilder.Append("\n<a style=\"color:#075C79\" href=\"javascript:openAddWin('add'," + myRows[i]["ProductID"].ToString() + ")\">" + BLL.Translation.Translate("007021", "添加短息类别") + "</a>\n");

                    this.menuBuilder.Append("\n<a style=\"color:#075C79\" href=\"javascript:openAddWin('editFold'," + myRows[i]["ProductID"].ToString() + ")\">" + BLL.Translation.Translate("000259", "修改") + "</a>\n");

                    this.menuBuilder.Append("\n<a style=\"color:#075C79\" href=\"javascript:openAddWin('deleteFold'," + myRows[i]["ProductID"].ToString() + ")\">" + BLL.Translation.Translate("000022", "删除") + "</a>\n");

                    this.menuBuilder.Append("\n</div>");

                    this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='display:none;margin-top=-3'>");

                    makePSMS((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgLine1 + ">");

                    this.menuBuilder.Append("\n</div>");
                }
            }
        }
    }


    private void makePSMSXZ(int pID, string pLayer)
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
                    this.menuBuilder.Append("\n" + pLayer + "<img align=\"absmiddle\" src=\"" + this.imgLine2 + "\" /><img align=\"absmiddle\" src=\"" + this.imgItem + "\" />");

                else
                    this.menuBuilder.Append("\n" + pLayer + "<img align=\"absmiddle\" src=" + this.imgLine3 + "><img align=\"absmiddle\" src=\"" + this.imgItem + "\" />");


                this.menuBuilder.Append("\n<a style=\"color:#075C79\" href=\"javascript:openSMScontent('add'," + myRows[i]["ProductID"].ToString() + ")\">" + myRows[i]["ProductName"].ToString().Trim() + "</a>\n");

                //this.menuBuilder.Append("\n<a style=\"color:#075C79\" href=\"javascript:openAddWin('addsms'," + myRows[i]["ProductID"].ToString() + ")\">" + BLL.Translation.Translate("007022", "短息预设") + "</a>\n");

                //this.menuBuilder.Append("\n<a style=\"color:#075C79\" href=\"javascript:openAddWin('editItem'," + myRows[i]["ProductID"].ToString() + ")\">" + BLL.Translation.Translate("000259", "修改") + "</a>\n");
                //this.menuBuilder.Append("\n<a style=\"color:#075C79\" href=\"javascript:openAddWin('deleteItem'," + myRows[i]["ProductID"].ToString() + ")\">" + BLL.Translation.Translate("000022", "删除") + "</a>\n");
                this.menuBuilder.Append("\n<br>\n");
            }
            else
            {
                this.menuBuilder.Append("\n<div>");

                if (i == myRows.Length - 1)
                {
                    this.menuBuilder.Append("\n" + pLayer + "<img align=\"absmiddle\" onclick=\"menuTree(menu" + this.mID + ",img" + this.mID + ",this)\" id=plus" + this.mID + " src=" + this.imgPlus2 + " class='menutop'>");

                    //this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick=\"alert('zhc" + this.mID + "')\" id=plus" + this.mID + " src=" + this.imgPlus2 + " class='menutop'>");

                    this.menuBuilder.Append("<img align=\"absmiddle\" id=\"img" + this.mID + "\" src=\"" + this.imgFoldClose + "\" />" + myRows[i]["ProductName"].ToString() + "\n");

                    //this.menuBuilder.Append("\n<a style=\"color:#075C79\" href=\"javascript:openAddWin('addFold'," + myRows[i]["ProductID"].ToString() + ")\">" + BLL.Translation.Translate("003228", "添加新类") + "</a>\n");

                    //this.menuBuilder.Append("\n<a style=\"color:#075C79\" href=\"javascript:openAddWin('add'," + myRows[i]["ProductID"].ToString() + ")\">" + BLL.Translation.Translate("007021", "添加短息类别") + "</a>\n");

                    //this.menuBuilder.Append("\n<a style=\"color:#075C79\" href=\"javascript:openAddWin('editFold'," + myRows[i]["ProductID"].ToString() + ")\">" + BLL.Translation.Translate("000259", "修改") + "</a>\n");

                    //this.menuBuilder.Append("\n<a style=\"color:#075C79\" href=\"javascript:openAddWin('deleteFold'," + myRows[i]["ProductID"].ToString() + ")\">" + BLL.Translation.Translate("000022", "删除") + "</a>\n");

                    this.menuBuilder.Append("\n</div>");

                    this.menuBuilder.Append("\n<div id=menu" + this.mID + " style=\"display:none;margin-top=-3\" >");

                    makePSMSXZ((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgEmpty + ">");

                    this.menuBuilder.Append("\n</div>");

                }
                else
                {
                    this.menuBuilder.Append("\n" + pLayer + "<img align=\"absmiddle\" onclick=\"menuTree(menu" + this.mID + ",img" + this.mID + ",this)\" id=plus" + this.mID + " src=" + this.imgPlus3 + " class=\"menutop\" />");

                    this.menuBuilder.Append("<img align=\"absmiddle\" id=\"img" + this.mID + "\" src=\"" + this.imgFoldClose + "\" />" + myRows[i]["ProductName"].ToString() + "\n");

                    //this.menuBuilder.Append("\n<a style=\"color:#075C79\" href=\"javascript:openAddWin('addFold'," + myRows[i]["ProductID"].ToString() + ")\">" + BLL.Translation.Translate("003228", "添加新类") + "</a>\n");

                    //this.menuBuilder.Append("\n<a style=\"color:#075C79\" href=\"javascript:openAddWin('add'," + myRows[i]["ProductID"].ToString() + ")\">" + BLL.Translation.Translate("007021", "添加短息类别") + "</a>\n");

                    //this.menuBuilder.Append("\n<a style=\"color:#075C79\" href=\"javascript:openAddWin('editFold'," + myRows[i]["ProductID"].ToString() + ")\">" + BLL.Translation.Translate("000259", "修改") + "</a>\n");

                    //this.menuBuilder.Append("\n<a style=\"color:#075C79\" href=\"javascript:openAddWin('deleteFold'," + myRows[i]["ProductID"].ToString() + ")\">" + BLL.Translation.Translate("000022", "删除") + "</a>\n");

                    this.menuBuilder.Append("\n</div>");

                    this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='display:none;margin-top=-3'>");

                    makePSMSXZ((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgLine1 + ">");

                    this.menuBuilder.Append("\n</div>");
                }
            }
        }
    }


    //返回产品映射表
    public DataTable getProductsTable()
    {
        return this.pListTable;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="countryCode">CountryId</param>
    private void getCombineProduct(string countryCode) //读取所有产品数据
    {
        //        string ssQL = "";
        //        if (System.Web.HttpContext.Current.Session["language"].ToString().ToLower() == "chinese" || System.Web.HttpContext.Current.Session["language"].ToString().ToUpper() == "中文")
        //        {
        //            ssQL = @"Select PID, isFold , ProductID , ProductName , CommonPrice ,productcode, CommonPV , PreferentialPrice , PreferentialPV, 
        //								" + this._defaultProductNum + " As num From Product where isSell=0 and IsCombineProduct=0 and countryCode='" + countryCode + "'";//Order By ProductID
        //        }
        //        else
        //        {
        //            int ID = Convert.ToInt32(DAL.DBHelper.ExecuteScalar("select id from language where name='" + System.Web.HttpContext.Current.Session["language"].ToString() + "'"));
        //            ssQL = @"Select PID, isFold , ProductID , productcode,(select languagename from LanguageTrans where 
        //							LanguageTrans.OldID=Product.Productid and LanguageTrans.Columnsname='productname' and languageid=" + ID + @") as ProductName , CommonPrice , CommonPV , PreferentialPrice , PreferentialPV, 
        //								" + this._defaultProductNum + " As num From Product where isSell=0 and IsCombineProduct=0 and countryCode='" + countryCode + "'";//Order By ProductID
        //        }

        string ssQL = "";

        ssQL = @"Select PID, isFold , ProductID , ProductName , CommonPrice ,productcode, CommonPV , PreferentialPrice , PreferentialPV, 
								" + this._defaultProductNum + " As num From Product where isSell=0 and IsCombineProduct=0 and countryCode='" + countryCode + "'";//Order By ProductID


        this.pTable = DAL.DBHelper.ExecuteDataTable(ssQL);
    }

    private void getData(string dian, string Currency) //读取所有产品数据
    {
        string Country = DAL.DBHelper.ExecuteScalar("Select m_currency.id from d_info,m_currency where storeid='" + dian + "' and d_info.currency=m_currency.ID").ToString();
        //string ssQL=@"Select PID, isFold , ProductID , ProductName , CommonPrice , CommonPV , PreferentialPrice , PreferentialPV, 0 As num From Product WHERE ((ProductID IN (Select ProductID From D_Kucun Where  StoreID='" + dian + @"') or isFold=1) and currency="+currency+")";	//Order By ProductID
        string ssQL = @"Select PID, isFold , ProductID , ProductName , CommonPrice , CommonPV , PreferentialPrice , PreferentialPV, 0 As num From Product WHERE isHidden=0 and Country=" + Country;	//Order By ProductID
        this.pTable = DAL.DBHelper.ExecuteDataTable(ssQL);
    }

    private void getData_ByCountryCode(string CountryCode) //根据国家编码读取 产品数据 ---DS2012
    {
        string ssQL = @"Select PID,isSell, isFold , ProductID , ProductName , CommonPrice , CommonPV , PreferentialPrice , PreferentialPV, 
								" + this._defaultProductNum + " As num From Product where CountryCode='" + CountryCode + "'";//Order By ProductID

        this.pTable = DAL.DBHelper.ExecuteDataTable(ssQL);
    }

    private void getData_BySMSCountryCode(string CountryCode) //根据国家编码读取 产品数据
    {
        string ssQL = @"Select PID, isFold , ProductID , ProductName From smscontent where CountryCode='" + CountryCode + "'";//Order By ProductID

        this.pTable = DAL.DBHelper.ExecuteDataTable(ssQL);
    }

    private void getData(string dian) //读取指定店铺的产品数据
    {
        string ssQL = @"
        declare @currency int
        select @currency=currency from storeinfo where defaultstore=1
        Select PID, isFold , ProductID , ProductName , CommonPrice , CommonPV , PreferentialPrice , PreferentialPV, 0 As num From Product WHERE isSell=0 and currency=@currency";	//Order By ProductID
        this.pTable = DAL.DBHelper.ExecuteDataTable(ssQL);
    }

    private void getDataShopping(string storId) //读取指定店铺的产品类的数据
    {
        string ssQL = @"declare @num varchar(20)
        select @num=scpccode from storeInfo where storeId='" + storId + "' set @num=substring(@num,1, 2)     Select a.PID, a.isFold , a.ProductID , a.ProductName , a.CommonPrice , a.CommonPV , a.PreferentialPrice , a.PreferentialPV, 0 As num From Product a where isSell=0 and isfold!=0 and countryCode=@num and (yongtu=0 or yongtu=2)";	//Order By ProductID
        this.pTable = DAL.DBHelper.ExecuteDataTable(ssQL);
    }

    private void getData10(string storId) //读取指定店铺的产品数据
    {
        string ssQL = @"declare @num varchar(20)
        select @num=scpccode from storeInfo where storeId='" + storId + "' set @num=substring(@num,1, 2)     Select a.PID, a.isFold , a.ProductID , a.ProductName , a.CommonPrice , a.CommonPV , a.PreferentialPrice , a.PreferentialPV, 0 As num From Product a where (isSell=0 or (issell=1 and a.productid in (select productid from stock where actualstorage>0 and storeid='" + storId + "')))   and countryCode=@num And (Yongtu = 0 Or Yongtu = 1) ";	//Order By ProductID
        this.pTable = DAL.DBHelper.ExecuteDataTable(ssQL);
    }

    private void getData10Again(string storeId)
    {
        string ssQL = @"declare @num varchar(20)
        select @num=scpccode from storeInfo where storeId='" + storeId + "' set @num=substring(@num,1, 2)     Select a.PID, a.isFold , a.ProductID , a.ProductName , a.CommonPrice , a.CommonPV , a.PreferentialPrice , a.PreferentialPV, 0 As num From Product a where (isSell=0 or (issell=1 and a.productid in (select productid from stock where actualstorage>0 and storeid='" + storeId + "')))   and countryCode=@num And (Yongtu = 0 Or Yongtu = 2) ";	//Order By ProductID
        this.pTable = DAL.DBHelper.ExecuteDataTable(ssQL); 
    }

    private void getData10(string storId,string orderid) //读取指定店铺的产品数据
    {
        string ssQL = @"declare @num varchar(20)
        select @num=scpccode from storeInfo where storeId='" + storId + "' set @num=substring(@num,1, 2)     Select a.PID, a.isFold , a.ProductID , a.ProductName , a.CommonPrice , a.CommonPV , a.PreferentialPrice , a.PreferentialPV, 0 As num From Product a where (isSell=0 or (issell=1 and a.productid in (select productid from stock where (actualstorage+(select quantity from memberdetails where orderid='" + orderid + "' and productid=stock.productid) )>0 and storeid='" + storId + "')))   and countryCode=@num And (Yongtu = 0 Or Yongtu = 1) ";	//Order By ProductID
        this.pTable = DAL.DBHelper.ExecuteDataTable(ssQL);
    }

    private void getData10Again(string storId, string orderid) //读取指定店铺的产品数据
    {
        string ssQL = @"declare @num varchar(20)
        select @num=scpccode from storeInfo where storeId='" + storId + "' set @num=substring(@num,1, 2)     Select a.PID, a.isFold , a.ProductID , a.ProductName , a.CommonPrice , a.CommonPV , a.PreferentialPrice , a.PreferentialPV, 0 As num From Product a where (isSell=0 or (issell=1 and a.productid in (select productid from stock where (actualstorage+(select quantity from memberdetails where orderid='" + orderid + "' and productid=stock.productid) )>0 and storeid='" + storId + "')))   and countryCode=@num And (Yongtu = 0 Or Yongtu = 2) ";	//Order By ProductID
        this.pTable = DAL.DBHelper.ExecuteDataTable(ssQL);
    }
   
    OrderGoodsBLL orderGoodsBLL = new OrderGoodsBLL();
    AddNewProductBLL addNewProductBLL = new AddNewProductBLL();

    //初始化产品映射表
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

    private void GetData1(string storeid, double huilv)
    {
        pTable = OrderGoodsBLL.GetAllProducts1(storeid, huilv);
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
            this.menuBuilder.Append("<div>" + BLL.Translation.Translate("000542", "产品列表") + "&nbsp;&nbsp;<div>");

            this.MakePOrder(1, "", storeid, ZT);
        }
        else
        {
            this.menuBuilder.Append("<div>" + BLL.Translation.Translate("000542", "产品列表") + "&nbsp;&nbsp;" + BLL.Translation.Translate("000549", "产品库中无记录！") + "<div>");
        }

        return this.menuBuilder.ToString();
    }


    /// <summary>
    /// 店铺在线订货时生成产品树形菜单
    /// </summary>
    /// <param name="storeid">店鋪編號</param>
    /// <param name="ZT">選擇的支付方式</param>
    /// <returns></returns>
    public string getMenuOrder1(string storeid, int ZT, double huilv)
    {
        this.InitPTable();
        this.GetData1(storeid, huilv);
        if (pTable.Rows.Count != 0)
        {
            this.menuBuilder.Append("<div>" + BLL.Translation.Translate("000542", "产品列表") + "&nbsp;&nbsp;<div>");

            this.MakePOrder(1, "", storeid, ZT);
        }
        else
        {
            this.menuBuilder.Append("<div>" + BLL.Translation.Translate("000542", "产品列表") + "&nbsp;&nbsp;" + BLL.Translation.Translate("000549", "产品库中无记录！") + "<div>");
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
            this.menuBuilder.Append("<div>" + BLL.Translation.Translate("000542", "产品列表") + "&nbsp;&nbsp;<div>");

            this.EditMakePOrder(1, "", storeid, ZT, orderid);
        }
        else
        {
            this.menuBuilder.Append("<div>" + BLL.Translation.Translate("000542", "产品列表") + "&nbsp;&nbsp;" + BLL.Translation.Translate("000549", "产品库中无记录！") + "<div>");
        }

        return this.menuBuilder.ToString();
    }

    public string getEditMenuOrder1(string storeid, string orderid, int ZT, double huilv)
    {
        this.InitPTable();
        this.GetData1(storeid, huilv);
        if (pTable.Rows.Count != 0)
        {
            this.menuBuilder.Append("<div>" + BLL.Translation.Translate("000542", "产品列表") + "&nbsp;&nbsp;<div>");

            this.EditMakePOrder(1, "", storeid, ZT, orderid);
        }
        else
        {
            this.menuBuilder.Append("<div>" + BLL.Translation.Translate("000542", "产品列表") + "&nbsp;&nbsp;" + BLL.Translation.Translate("000549", "产品库中无记录！") + "<div>");
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
                // DataTable dt = OrderGoodsBLL.GetAllTityByStoreidAndProductid(storeid, Convert.ToInt32(myRows[i]["ProductID"]));
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

                this.menuBuilder.Append("\n\t    " + myRows[i]["ProductName"].ToString() + " <font color=black>" + BLL.Translation.Translate("000322", "金额") + "：" + (Convert.ToDouble(myRows[i]["PreferentialPrice"].ToString())).ToString("f2") + "；" + BLL.Translation.Translate("000414", "积分") + "：" + (Convert.ToDouble(myRows[i]["PreferentialPV"].ToString())).ToString("f2") + "</font>\n");
                this.menuBuilder.Append("\n<a href=\"#\" onmousemove=\"javascript:ShowProductDiv(this,'" + myRows[i]["ProductID"].ToString() + "');\"  onmouseout=\"javascript:HideProductDiv(this);\" ) >s" + BLL.Translation.Translate("000440", "查看") + " </a><br>\n");//查看
            }
            else
            {
                this.menuBuilder.Append("\n<div>");

                if (i == myRows.Length - 1)
                {
                    if (CheckGoodsNum(myRows[i]["ProductID"].ToString(), orderid))
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
                    if (CheckGoodsNum(myRows[i]["ProductID"].ToString(), orderid))
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

    private bool CheckGoodsNum(string pid, string orderid)
    {
        DataTable dt = DBHelper.ExecuteDataTable("select productid from product where pid=" + pid);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (DBHelper.ExecuteScalar("select count(id) from OrderGoodsDetail where  OrderGoodsid='" + orderid + "' and  productid=" + dt.Rows[i]["productid"].ToString()).ToString() != "0")
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


    private bool CheckOrderNumShopp(string pid, ArrayList list)
    {
        bool shopp = false;
        DataTable dt = DBHelper.ExecuteDataTable("select productid from product where pid=" + pid);

        foreach (MemberDetailsModel memberDetailsModel in list)
        {
            int listpid = Convert.ToInt32(DBHelper.ExecuteScalar("select pid from product where productid=" + memberDetailsModel.ProductId));
            if (listpid == Convert.ToInt32(pid))
            {
                shopp = true;
            }  
        }


        return shopp;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pid"></param>
    /// <param name="orderid"></param>
    /// <returns></returns>
    private bool CheckOrderNum10(string pid, string orderid)
    {
        DataTable dt = DBHelper.ExecuteDataTable("select productid from product where pid=" + pid);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (DBHelper.ExecuteScalar("select count(id) from memberDetails where  orderId='" + orderid + "' and  productid=" + dt.Rows[i]["productid"].ToString()).ToString() != "0")
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

    private bool CheckOrderNumShopping(string pid, string productid)
    {
        bool isTS = false;
        string proid = DBHelper.ExecuteScalar("select pid from Product where productid=" + productid).ToString();


        if (proid == "1")
        {
            isTS = false;
        }
        else if (pid == productid && pid != "1")
        {
            isTS = true;
        }
        else
        {
            while (proid != "1")
            {

                if (pid == proid)
                {
                    isTS = true;
                    break;
                }
                else if (proid == "1")
                {
                    break;
                }
                proid = DBHelper.ExecuteScalar("select pid from Product where productid=" + proid).ToString();
            }
        }

        return isTS;



    }



    private bool CheckStockNum(string pid, string storeid)
    {
        //DataTable dt=DBHelper.ExecuteDataTable("select productid from product where pid="+pid);
        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    if (DBHelper.ExecuteScalar("select isnull(sum(ActualStorage+HasOrderCount),0) from stock where (ActualStorage+HasOrderCount)<0 and storeid='"+storeid+"' and  productid=" + dt.Rows[i]["productid"].ToString()).ToString() != "0")
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        continue;
        //    }
        //}

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
                //DataTable dt = OrderGoodsBLL.GetAllTityByStoreidAndProductid(storeid, Convert.ToInt32(myRows[i]["ProductID"]));
                //int Quantity = 0;
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

                this.menuBuilder.Append("<input maxLength=6 value=0 type=text class=priceBox onkeyup=\"Bind()\"  size=3 name=N" + myRows[i]["ProductID"] + "></input>");

                this.menuBuilder.Append("\n\t    " + myRows[i]["ProductName"].ToString() + " <font color=black>" + BLL.Translation.Translate("000322", "金额") + "：" + (Convert.ToDouble(myRows[i]["PreferentialPrice"].ToString())).ToString("f2") + "；" + BLL.Translation.Translate("000414", "积分") + "：" + (Convert.ToDouble(myRows[i]["PreferentialPV"].ToString())).ToString("f2") + "</font>\n");
                this.menuBuilder.Append("\n<a href=\"#\" onmousemove=\"javascript:ShowProductDiv(this,'" + myRows[i]["ProductID"].ToString() + "');\"  onmouseout=\"javascript:HideProductDiv(this);\" ) >" + BLL.Translation.Translate("000440", "查看") + " </a><br>\n");
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


    private void MakePOrder2(int pID, string pLayer, string storeid, int ZT)
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

                // this.menuBuilder.Append("\n\t    " + myRows[i]["ProductName"].ToString() + " <font color=black>会员价：" + (Convert.ToDouble(myRows[i]["PreferentialPrice"].ToString())).ToString("f2") + "；积分：" + (Convert.ToDouble(myRows[i]["PreferentialPV"].ToString())).ToString("f2") + "</font>\n");
                this.menuBuilder.Append("\n<a href=\"javascript:void(window.open('../ProductIntro.aspx?ProductID=" + myRows[i]["ProductID"].ToString() + "','','width=450,height=240')) \">" + BLL.Translation.Translate("000440", "查看") + " </a><br>\n");
            }
            else
            {
                this.menuBuilder.Append("\n<div>");

                if (i == myRows.Length - 1)
                {
                    if (CheckOrderNum10(myRows[i]["ProductID"].ToString(), storeid))
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

                    MakePOrder2((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgEmpty + ">", storeid, ZT);
                    this.menuBuilder.Append("\n</div>");

                }
                else
                {
                    if (CheckOrderNum10(myRows[i]["ProductID"].ToString(), storeid))
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

                    MakePOrder2((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgLine1 + ">", storeid, ZT);
                    this.menuBuilder.Append("\n</div>");
                }
            }
        }
    }
    /// <summary>
    /// 获取产品列表  ---DS2012
    /// </summary>
    /// <param name="countyrCode"></param>
    private void getProduct(string countyrCode)
    {
        SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@countryCode",SqlDbType.NVarChar,10),
            };

        sparams[0].Value = countyrCode;
        this.pTable = DBHelper.ExecuteDataTable("GetMoreProductInfoByCountryCode", sparams, CommandType.StoredProcedure);
    }
    /// <summary>
    /// 根据国家编号获取产品列表  ---DS2012
    /// </summary>
    /// <param name="countryCode"></param>
    /// <returns></returns>
    public string GetCountryProduct(string countryCode)
    {
        this.initPTable();

        this.getProduct(countryCode);

        if (pTable.Rows.Count != 0)
        {
            this.menuBuilder.Append("<div>" + this.GetTran("000542", "产品列表") + "<div>");
            this.makeProductList(1, "");
        }
        else
        {
            this.menuBuilder.Append("<div>" + BLL.Translation.Translate("000542", "产品列表") + "&nbsp;&nbsp;" + BLL.Translation.Translate("000549", "产品库中无记录！") + "<div>");
        }

        return this.menuBuilder.ToString();
    }
    /// <summary>
    /// 根据国家编号和库位ID获取产品列表  ---DS2012
    /// </summary>
    /// <param name="countryCode"></param>
    /// <returns></returns>
    public string GetDepotSeatProduct(string countryCode, string WareHouseID, string DepotSeatID)
    {
        this.initPTable();
        SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@Country",countryCode),
                new SqlParameter("@WareHouseID",WareHouseID),
                new SqlParameter("@DepotSeatID",DepotSeatID)
            };
        this.pTable = DBHelper.ExecuteDataTable("GetProductEQueantityMenu", param, CommandType.StoredProcedure);


        if (pTable.Rows.Count != 0)
        {
            this.menuBuilder.Append("<div>" + this.GetTran("000542", "产品列表") + "<div>");
            this.makeProductListNew("");
        }
        else
        {
            this.menuBuilder.Append("<div>" + BLL.Translation.Translate("000542", "产品列表") + "&nbsp;&nbsp;" + BLL.Translation.Translate("000549", "产品库中无记录！") + "<div>");
        }

        return this.menuBuilder.ToString();
    }
    /// <summary>
    /// 产品树状菜单   --DS2012
    /// </summary>
    /// <param name="pID"></param>
    /// <param name="pLayer"></param>
    private void makeProductList(int pID, string pLayer)
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

                //this.menuBuilder.Append("<input maxLength=6 value=0 type=text class=priceBox onkeyup=\"Bind();setpids(this);\" size=3 name=N" + myRows[i]["ProductID"] + "></input>");
                this.menuBuilder.Append("\n\t  <span style='cursor:pointer' onmouseover=\"this.style.color='red';this.style.textDecoration='underline';\" onmouseout=\"this.style.color='';this.style.textDecoration='';\" onclick=\"AjShopp('" + myRows[i]["ProductID"] + "')\">" + myRows[i]["productcode"].ToString() + " </span>\n");

                this.menuBuilder.Append("\n" + myRows[i]["ProductName"].ToString());

                this.menuBuilder.Append("\n<a href=\"#\" onmousemove=\"javascript:ShowProductDiv(this,'" + myRows[i]["ProductID"].ToString() + "');\"  onmouseout=\"javascript:HideProductDiv(this);\" ) >" + this.GetTran("000440", "查看") + "</a><br>\n");

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

                    makeProductList((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgEmpty + ">");

                    this.menuBuilder.Append("\n</div>");

                }
                else
                {
                    this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle onclick='menu(menu" + this.mID + ",img" + this.mID + ",this)' id=plus" + this.mID + " src=" + this.imgPlus3 + " class='menutop'>");

                    this.menuBuilder.Append("<img align=absmiddle id=img" + this.mID + " src=" + this.imgFoldClose + ">" + myRows[i]["ProductName"].ToString() + "\n");

                    this.menuBuilder.Append("\n</div>");

                    this.menuBuilder.Append("\n<div id=menu" + this.mID + " style='display:none;margin-top=-3'>");

                    makeProductList((int)myRows[i]["ProductID"], pLayer + "<img align=absmiddle src=" + this.imgLine1 + ">");

                    this.menuBuilder.Append("\n</div>");
                }
            }
        }
    }

    /// <summary>
    /// 产品树状菜单   --DS2012
    /// </summary>
    /// <param name="pID"></param>
    /// <param name="pLayer"></param>
    private void makeProductListNew(string pLayer)
    {
        int i;

        //DataRow[] myRows = this.pTable.Select("pID=" + pID.ToString(), "ProductID");

        if (this.pTable.Rows.Count == 0) return;


        for (i = 0; i < this.pTable.Rows.Count; i++)
        {
            this.mID++;
            /*PID, isFold , ProductID , ProductName , CommonPrice , CommonPV , PreferentialPrice , PreferentialPV, */

            DataRow myRow = this.pListTable.NewRow();
            myRow["ID"] = (int)this.pTable.Rows[i]["ProductID"];
            myRow["price"] = (decimal)this.pTable.Rows[i]["PreferentialPrice"];
            myRow["name"] = this.pTable.Rows[i]["ProductName"].ToString();
            this.pListTable.Rows.Add(myRow);

            if (i == this.pTable.Rows.Count - 1)
                this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle src=" + this.imgLine2 + "><img align=absmiddle src=" + this.imgItem + ">");
            else
                this.menuBuilder.Append("\n" + pLayer + "<img align=absmiddle src=" + this.imgLine3 + "><img align=absmiddle src=" + this.imgItem + ">");

            //this.menuBuilder.Append("<input maxLength=6 value=0 type=text class=priceBox onkeyup=\"Bind();setpids(this);\" size=3 name=N" + myRows[i]["ProductID"] + "></input>");
            this.menuBuilder.Append("\n\t  <span style='cursor:pointer' onmouseover=\"this.style.color='red';this.style.textDecoration='underline';\" onmouseout=\"this.style.color='';this.style.textDecoration='';\" onclick=\"AjShopp('" + this.pTable.Rows[i]["ProductID"] + "')\">" + this.pTable.Rows[i]["productcode"].ToString() + " </span>\n");

            this.menuBuilder.Append("\n" + this.pTable.Rows[i]["ProductName"].ToString());

            this.menuBuilder.Append("\n<a href=\"#\" onmousemove=\"javascript:ShowProductDiv(this,'" + this.pTable.Rows[i]["ProductID"].ToString() + "');\"  onmouseout=\"javascript:HideProductDiv(this);\" ) >" + this.GetTran("000440", "查看") + "</a><br>\n");

            
        }
    }
}
