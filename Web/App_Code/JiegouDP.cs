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
using System.Data.SqlClient; 
/// <summary>
///JiegouDP 的摘要说明
/// </summary>
public class JiegouDP
{
    /// <summary>
    /// 连接字符串
    /// </summary>
    public static readonly string connString =DAL.DBHelper.connString;

    public JiegouDP()
    {

    }
    /// <summary>
    /// 生成网络图
    /// </summary>
    /// <param name="QiShu">期数</param>
    /// <param name="isAnZhi">是否是安置关系</param>
    /// <param name="bianhao">起点编号</param>
    /// <returns>返回表</returns>
    public static DataTable wlt(int QiShu, int showCW, bool isAnZhi, string bianhao, string page)
    {

        DataTable myDataTable = new DataTable("wangluo");
        DataColumn myDataColumn;
        DataRow myDataRow;

        //Create new DataColumn, set DataType, ColumnName and add to DataTable.    
        myDataColumn = new DataColumn();
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataColumn.ColumnName = "xinxi";
        myDataColumn.ReadOnly = false;
        myDataColumn.Unique = false;
        //Add the Column to the DataColumnCollection.
        myDataTable.Columns.Add(myDataColumn);

        //Create second column.
        myDataColumn = new DataColumn();
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataColumn.ColumnName = "bianhao";
        myDataColumn.AutoIncrement = false;
        myDataColumn.Caption = "bianhao";
        myDataColumn.ReadOnly = false;
        myDataColumn.Unique = false;
        // Add the column to the table.
        myDataTable.Columns.Add(myDataColumn);

        string TempStr = "";
        int TempVal = 1;
        string tempXianshi = "";
        string newinfo = "";

        SqlParameter[] para = {
									   new SqlParameter("@STOREID",SqlDbType.VarChar,20 ),
									   new SqlParameter("@QISHU",SqlDbType.Int ),
									   new SqlParameter("@SHOWCW",SqlDbType.Int ),
									   new SqlParameter("@ISTJ",SqlDbType.Bit )
								   };
        para[0].Value = bianhao;
        para[1].Value = QiShu;
        para[2].Value = showCW;
        if (isAnZhi)
        {
            para[3].Value = 0;
        }
        else
        {
            para[3].Value = 1;
        }

        SqlDataReader dr = DAL.DBHelper.ExecuteReader("DPNET", para, CommandType.StoredProcedure);

        while (dr.Read())
        {
            if (Convert.ToInt32(dr["LEVELS"]) == 0)
            {
                TempStr = "▲";
            }
            else
            {
                TempVal = Convert.ToInt32(dr["LEVELS"]) * 4;
                TempStr = (TempStr + Jiegou.dupText("^", TempVal));
                TempStr = TempStr.Substring(0, TempVal) + "|";
            }
            //TempStr=newText.dupText("&nbsp;&nbsp;",Convert.ToInt32( dr[myCengwei] )-1); GetDecipherName
            myDataRow = myDataTable.NewRow();
            string sName = Encryption.Encryption.GetDecipherName(dr["storename"].ToString());
            string mouseinfo = sName;

           // string Mobile = "";
            //				if (Convert.ToString(dr["MobileTele"]).Trim()!="")
            //				{
            //					Mobile = "[" + Convert.ToString(dr["MobileTele"])+"]";
            //				}
            //				else
            //				{
            //					Mobile = "[无]";
            //				}
            //				mouseinfo = Convert.ToString(dr["Province"]) + Convert.ToString(dr["City"]) + " 手机:[" +Mobile+"]";



            //生成信息

            if (Convert.ToInt32(dr["qishu"]) >= QiShu)
            {
                tempXianshi = TempStr.Replace(" ", "") + "~~" + "<font color=red class=ls><a href=" + page + dr["storeid"].ToString() + " title=" + mouseinfo + " >" + dr["storeid"].ToString() + "&nbsp;" + dr["Name"].ToString() + "</a>" + newinfo + "</font>";
                //tempXianshi=TempStr.Replace(" ","&nbsp;")+"__"+"<font color=red class=ls><a href="+page+dr["bianhao"].ToString()+" title="+mouseinfo+" >"+dr["bianhao"].ToString()+"&nbsp;"+dr["Name"].ToString()+"</a>"+newinfo+"["+dr["xgfenshu"]+"]</font>";

            }
            else
            {
                tempXianshi = TempStr.Replace(" ", "") + "~~" + "<font class=ls><a href=" + page + dr["storeid"].ToString() + " title=" + mouseinfo + " >" + dr["storeid"].ToString() + "&nbsp;" + dr["Name"].ToString() + "</a>" + newinfo + "</font>";

            }
            if (Convert.ToInt32(dr["LEVELS"]) == 0) 
            {
                if (Convert.ToInt32(dr["qishu"]) >= QiShu)
                {
                    tempXianshi = TempStr.Replace(" ", "") + "<font color=red class=ls><a href=" + page + dr["storeid"].ToString() + " title=" + mouseinfo + " >" + dr["storeid"].ToString() + "&nbsp;" + dr["Name"].ToString() + "</a>" + newinfo + "</font>";
                }
                else
                {
                    tempXianshi = TempStr.Replace(" ", "") + "<font class=ls><a href=" + page + dr["storeid"].ToString() + " title=" + mouseinfo + " >" + dr["storeid"].ToString() + "&nbsp;" + dr["Name"].ToString() + "</a>" + newinfo + "</font>"; 
                }
            }

            string t = tempXianshi.Replace("^", "<img src='../images/0150.gif'>");
            t = t.Replace("|", "<img src='../images/0044.gif'>");
            t = t.Replace("~", "<img src='../images/0111.gif'>");

            myDataRow["xinxi"] = t;
            myDataRow["bianhao"] = dr["storeid"];
            myDataTable.Rows.InsertAt(myDataRow, 0);
        }
        dr.Close();
        return myDataTable;
    }


}
