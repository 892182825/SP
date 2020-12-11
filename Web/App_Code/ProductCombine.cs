using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DAL;

/// <summary>
///ProductCombine 的摘要说明
/// </summary>
public class ProductCombine
{
    public ProductCombine()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    /// <summary>
    /// 通过ID获取组合产品的产品信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static DataTable GetCombine(string id)
    {
        string sql = "select ProductID,productcode,productName,PreferentialPrice,PreferentialPV,quantity,zje=PreferentialPrice*quantity,zjf= PreferentialPV*quantity from dbo.ProductCombineDetail pc "
                                                        +" join product p on pc.subproductid=p.productid where CombineProductID=@id order by id asc";

        return DBHelper.ExecuteDataTable(sql, new SqlParameter[] { new SqlParameter("@id", id) }, CommandType.Text);
    }

    /// <summary>
    /// 通过产品ID和组合产品ID删除一个产品
    /// </summary>
    /// <param name="Cid">组合产品ID</param>
    /// <param name="Pid">产品ID</param>
    /// <returns></returns>
    public static bool DelProductById(string Cid, string Pid)
    {
        string sql = "delete from ProductCombineDetail where CombineProductID=@cid and subproductID=@pid";

        return DBHelper.ExecuteNonQuery(sql, new SqlParameter[] { new SqlParameter("@cid", Cid), new SqlParameter("@pid", Pid) }, CommandType.Text) == 1;
    }

    /// <summary>
    /// 组合产品中是否存在某种产品
    /// </summary>
    /// <param name="Cid">组合产品ID</param>
    /// <param name="Pid">产品ID</param>
    /// <returns></returns>
    public static bool IsExistProductById(int Cid, string Pid)
    {
        string sql = "select count(1) from ProductCombineDetail where CombineProductID=@cid and subproductID=@pid";

        return Convert.ToInt32(DBHelper.ExecuteScalar(sql, new SqlParameter[] { new SqlParameter("@cid", Cid), new SqlParameter("@pid", Pid) }, CommandType.Text)) >= 1;
    }

    /// <summary>
    /// 组合产品中是某种产品数量加1
    /// </summary>
    /// <param name="Cid">组合产品ID</param>
    /// <param name="Pid">产品ID</param>
    /// <returns></returns>
    public static bool UpdateProductById(int Cid, string Pid)
    {
        string sql = "update dbo.ProductCombineDetail set quantity=quantity+1 where CombineProductID=@cid and subProductID=@pid";

        return DBHelper.ExecuteNonQuery(sql, new SqlParameter[] { new SqlParameter("@cid", Cid), new SqlParameter("@pid", Pid) }, CommandType.Text) > 0;
    }

    /// <summary>
    /// 组合产品中是某种产品数量加1
    /// </summary>
    /// <param name="Cid">组合产品ID</param>
    /// <param name="Pid">产品ID</param>
    /// <returns></returns>
    public static int InsertProductById(int Cid, string Pid)
    {
        string sql = "insert into ProductCombineDetail(CombineProductID,subproductID,quantity,remark) values(@cid,@pid,'1','')";

        return DBHelper.ExecuteNonQuery(sql, new SqlParameter[] { new SqlParameter("@cid", Cid), new SqlParameter("@pid", Pid) }, CommandType.Text);
    }

    /// <summary>
    /// 获取国家列表
    /// </summary>
    /// <returns></returns>
    public static string GetCountryXML()
    {
        return DBHelper.ExecuteScalar("select ID,CountryCode,[Name] from Country order by ID for xml raw('hang'),elements,root('root')").ToString();
    }
    /// <summary>
    /// 获取国家列表
    /// </summary>
    /// <returns></returns>
    public static DataTable GetCountry()
    {
        return DBHelper.ExecuteDataTable("select ID,CountryCode,[Name] from Country order by ID");
    }
    /// <summary>
    /// 根据国家ID获取组合产品列表
    /// </summary>
    /// <returns></returns>
    public static string GetCombineProductXML(string id)
    {
        object obj = DBHelper.ExecuteScalar("select ProductID,ProductName From Product as P Where isCombineProduct = 1 and countryCode=@id for xml raw('hang'),elements,root('root')", new SqlParameter("@id", id), CommandType.Text);
        if (obj == DBNull.Value || obj == null)
        {
            return "<root><hang><ProductID>0</ProductID><ProductName>无</ProductName></hang></root>";
        }
        return obj.ToString();
    }
    /// <summary>
    /// 根据国家ID获取组合产品列表
    /// </summary>
    /// <returns></returns>
    public static DataTable GetCombineProduct(string id)
    {
        DataTable dt = DBHelper.ExecuteDataTable("select ProductID,ProductName From Product as P Where isCombineProduct = 1 and countryCode=@id ",new SqlParameter[]{ new SqlParameter("@id", id)}, CommandType.Text);
        return dt;
    }

    /// <summary>
    /// 更改组合产品中是某种产品数量
    /// </summary>
    /// <param name="Cid">组合产品ID</param>
    /// <param name="Pid">产品ID</param>
    /// <returns></returns>
    public static bool UpdateProductCountById(string count,string Cid, string Pid)
    {
        string sql = "update dbo.ProductCombineDetail set quantity=@count where CombineProductID=@cid and subProductID=@pid";

        return DBHelper.ExecuteNonQuery(sql, new SqlParameter[] { new SqlParameter("@count",count),new SqlParameter("@cid", Cid), new SqlParameter("@pid", Pid) }, CommandType.Text) == 1;
    }
}
