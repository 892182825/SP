using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DAL;
using System.Data.SqlClient;
using BLL.other.Company;

public partial class Company_CombineProduct_Ajax : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        string pid = Request.Form["pid"];

        if (Request.Form["tp"] == "1")
        {
            
            string isyw = "";//是否发生业务

            if (CombineProductBLL.CheckWheatherChangeCombineProduct(Convert.ToInt32(pid)))
                isyw = "<isyw>y</isyw>";//可以修改组合产品
            else
                isyw = "<isyw>n</isyw>";

            string dataxml = "<root>";

            DataTable dt = ProductCombine.GetCombine(pid);

            foreach (DataRow dr in dt.Rows)
            {
                dataxml = dataxml + "<hang><productid>" + dr["productid"] + "</productid><productcode>" + dr["productcode"] + "</productcode><productname>" + dr["productname"] + "</productname><preferentialprice>" + dr["preferentialprice"] + "</preferentialprice><preferentialpv>" + dr["preferentialpv"] + "</preferentialpv><quantity>" + dr["quantity"] + "</quantity><zje>" + dr["zje"] + "</zje><zjf>" + dr["zjf"] + "</zjf></hang>";
            }

            dataxml = dataxml + "</root>";

            dataxml = "<?xml version='1.0' encoding='utf-8' ?>" + dataxml.Replace("</root>", isyw + "</root>");

            Response.ContentType = "text/xml";
            Response.Write(dataxml);
        }
        else if(Request.Form["tp"]=="2")
        {
            if (ProductCombine.DelProductById(Request.Form["zhpid"].ToString(), pid))
                Response.Write("c");
            else
                Response.Write("s");
        }
        else if (Request.Form["tp"] == "3")
        {
            int aa = Convert.ToInt32(Request.Form["zhpid"]);

            if (CombineProductBLL.CheckWheatherChangeCombineProduct(aa))
            {

                if (ProductCombine.IsExistProductById(aa, pid))
                {
                    if (ProductCombine.UpdateProductById(aa, pid))
                    {
                        Response.Write("e");
                    }
                }
                else
                {
                    int hs = ProductCombine.InsertProductById(aa, pid); 

                    if (hs == 1)
                        Response.Write("c");
                    else
                        Response.Write("s");
                }
            }
            else
            {
                Response.Write("i");
            }
        }

        else if (Request.Form["tp"] == "7")
        {
            if (ProductCombine.UpdateProductCountById(Request.Form["zhpid"].Split('*')[0].ToString(), Request.Form["zhpid"].Split('*')[1].ToString(), pid))
                Response.Write("c");
            else
                Response.Write("s");
        }
        Response.End();
    }
}
