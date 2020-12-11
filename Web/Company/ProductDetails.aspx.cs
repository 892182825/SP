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

///Add Namespace
using Model.Other;
using BLL.other.Company;
using BLL.CommonClass;

/*
 * 创建者：  汪  华
 * 创建时间：2009-10-23
 * 完成时间：2009-10-23
 * 对应菜单：公司->报表中心->产品明细
 */

public partial class Company_ProductDetails : BLL.TranslationBase
{
    /// <summary>
    /// 为GridView排序事件申明变量和赋值
    /// </summary>
    protected static bool blTypeName = true;
    protected static bool blProductID = true;
    protected static bool blProductCode = true;
    protected static bool blProductName = true;
    protected static bool blProductSpecName = true;
    protected static bool blProductTypeName = true;
    protected static bool blProductUnitName = true;
    protected static bool blCountry = true;
    protected static bool blProductArea = true;
    protected static bool blPreferentialPrice = true;
    protected static bool blPreferentialPV = true;
    protected static bool blCommonPrice = true;
    protected static bool blCommonPV = true;
    protected static bool blAlertnessCount = true;
    protected static bool blIsCombineProduct = true;
    protected static bool blIsSell = true;

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        //权限设置
        Permissions.CheckManagePermission(EnumCompanyPermission.ReportProductDetail);

        ///设置GridView的样式
        gvProduct.Attributes.Add("style", "word-break:keep-all;word-wrap:normal");
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            this.lblMessage.Visible = false;
            Bind();
        }
        Translations_More();
    }

    /// <summary>
    /// Translation
    /// </summary>
    protected void Translations_More()
    {
        TranControls(gvProduct, new string[][]
                        {
                            new string[]{"001852", "产品类名"},                            
                            new string[]{"000263", "产品编码"},
                            new string[]{"000501", "产品名称"},
                            new string[]{"000880", "产品规格"},
                            new string[]{"000882", "产品型号"},
                            new string[]{"001868", "产品单位"},
                            new string[]{"001872", "产品所属国家"},
                            new string[]{"001877", "产品产地"},
                            new string[]{"001882", "优惠单价"},
                            new string[]{"001883", "优惠积分"},
                            new string[]{"001886", "普通单价"},
                            new string[]{"001888", "普通积分"},
                            new string[]{"000365", "预警数量"},
                            new string[]{"001890", "是否组合产品"},
                            new string[]{"001891", "是否销售"}  
                        }
                    );

    }

    private void Bind()
    {
        ///注意：拼SQL语句时，注意空格
        ViewState["orderColumnName"] = " a.ProductID ";
        ViewState["columnNames"] = " b.ProductName as TypeName, a.productid,a.ProductName,a.ProductCode,f.country,a.ProductArea,c.ProductSpecName,a.ProductTypeName,e.ProductUnitName, a.PreferentialPrice, a.PreferentialPv,a.CommonPrice,a.CommonPV,a.AlertnessCount,a.isCombineProduct,a.isSell ";

        ViewState["tableNames"] = " product as a,product as b,ProductSpec as c,ProductType as d,ProductUnit as e ,(select distinct Country from City) as f,Country as g ";
        ViewState["conditions"] = " a.isFold=0 and a.pid=b.productid and a.ProductSpecID=c.productSpecID and  a.productTypeID=d.productTypeID and a.SmallProductUnitID=e.productUnitID and a.countryCode=g.CountryCode and f.Country=g.[name]  ";

        ///通过联合查询获取产品详细信息
        DataTable dt = ProductDetailsBLL.GetMoreProductDetailsInfo();

        if (dt.Rows.Count < 1)
        {
            this.lblMessage.Text = GetTran("001928", "对不起,没有相关信息");
            this.lblMessage.Visible = true;
        }

        else
        {
            ViewState["sortTable"] = dt;
            this.gvProduct.DataSource = dt;
            this.gvProduct.DataBind();


            ///分页
            Pager pager = Page.FindControl("Pager1") as Pager;
            pager.PageBind(0, 10, ViewState["tableNames"].ToString(), ViewState["columnNames"].ToString(), ViewState["conditions"].ToString(), ViewState["orderColumnName"].ToString(), "gvProduct");
        }
    }

    protected string getstr(string str)
    {
        if (str == "0")
        {
            return str;
        }
        else
        {
            int index = str.IndexOf(".");
            if (index == -1)
            {
                return str + ".00";
            }
            else
            {
                if (str.Length < index + 3)
                {
                    return str.Substring(0, index + 2);
                }
                else
                {
                    return str.Substring(0, index + 3);
                }
            }
        }
    }

    /// <summary>
    /// Judge the product whether sell
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    protected string JudgeIsSell(string str)
    {
        if (str == "0")
        {
            return GetTran("000233", "是");
        }

        else
        {
            return GetTran("000235", "否");
        }
    }

    /// <summary>
    /// Judge the product whether CombineProduct
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    protected string JudgeIsCombineProduct(string str)
    {
        if (str == "0")
        {
            return GetTran("000235", "否");
        }

        else
        {
            return GetTran("000233", "是");
        }
    }

    /// <summary>
    /// 行绑定事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvProduct_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ///控制样式
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
        }

        if (e.Row.RowType == DataControlRowType.Header)
        {
            Translations_More();
        }
    }

    /// <summary>
    /// 导出Excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        //Out to excle the data of ProductDetailsInfo 
        DataTable dt = ProductDetailsBLL.OutToExcel_ProductDetailsInfo();
        if (dt.Rows.Count < 1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001712", "对不起，没有可以导出的数据!")));
            return;
        }

        else
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToString(dt.Rows[i]["isCombineProduct"]) == "0")
                {
                    dt.Rows[i]["isCombineProduct"] = GetTran("000235", "否");
                }

                else
                {
                    dt.Rows[i]["isCombineProduct"] = GetTran("000233", "是");
                }

                if (Convert.ToString(dt.Rows[i]["isSell"]) == "0")
                {
                    dt.Rows[i]["isSell"] = GetTran("000233", "是");
                }

                else
                {
                    dt.Rows[i]["isSell"] = GetTran("000235", "否");
                }
            }

            Excel.OutToExcel
                (dt, GetTran("001929", "产品明细"), new string[]
                        {
                            "TypeName="+GetTran("001852", "产品类名"), 
                            "ProductCode="+GetTran("000263", "产品编码"), 
                            "ProductName="+GetTran("000501", "产品名称"), 
                            
                            "ProductSpecName="+GetTran("000880", "产品规格"),
                            "ProductTypeName="+GetTran("000882", "产品型号"),
                            "ProductUnitName="+GetTran("001868", "产品单位"),

                            "country="+GetTran("001872", "产品所属国家"), 
                            "ProductArea="+GetTran("001877", "产品产地"), 
                            "PreferentialPrice="+GetTran("001882", "优惠单价"),
                            "PreferentialPv="+GetTran("001883", "优惠积分"),
                            "CommonPrice="+GetTran("001886", "普通单价"),
                            "CommonPV="+GetTran("001888", "普通积分"),
                            "AlertnessCount="+GetTran("000365", "预警数量"),
                            "isCombineProduct="+GetTran("001890", "是否组合产品"),
                            "isSell="+GetTran("001891", "是否销售") 
                        }
                    );
        }
    }

    /// <summary>
    /// 针对导出Excel重载
    /// </summary>
    /// <param name="control"></param>
    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }

    /// <summary>
    /// 排序事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvProduct_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataView dv = new DataView((DataTable)ViewState["sortTable"]);
        string sortString = e.SortExpression;
        switch (sortString.ToLower().Trim())
        {
            case "typename":
                if (blTypeName)
                {
                    dv.Sort = "TypeName desc";
                    blTypeName = false;
                    ViewState["orderColumnName"] = "b.ProductName desc";
                }

                else
                {
                    dv.Sort = "TypeName asc";
                    blTypeName = true;
                    ViewState["orderColumnName"] = "b.ProductName asc";
                }
                break;

            case "productid":
                if (blProductID)
                {
                    dv.Sort = "ProductID desc";
                    blProductID = false;
                    ViewState["orderColumnName"] = "a.ProductID desc";
                }

                else
                {
                    dv.Sort = "ProductID asc";
                    blProductID = true;
                    ViewState["orderColumnName"] = "a.ProductID asc";
                }
                break;

            case "productcode":
                if (blProductCode)
                {
                    dv.Sort = "ProductCode desc";
                    blProductCode = false;
                    ViewState["orderColumnName"] = "a.ProductCode desc";
                }

                else
                {
                    dv.Sort = "ProductCode asc";
                    blProductCode = true;
                    ViewState["orderColumnName"] = "a.ProductCode asc";
                }
                break;

            case "productname":
                if (blProductName)
                {
                    dv.Sort = "ProductName desc";
                    blProductName = false;
                    ViewState["orderColumnName"] = "a.ProductName desc";
                }

                else
                {
                    dv.Sort = "ProductName asc";
                    blProductName = true;
                    ViewState["orderColumnName"] = "a.ProductName asc";
                }
                break;

            case "productspecname":
                if (blProductSpecName)
                {
                    dv.Sort = "ProductSpecName desc";
                    blProductSpecName = false;
                    ViewState["orderColumnName"] = "c.ProductSpecName desc";
                }

                else
                {
                    dv.Sort = "ProductSpecName asc";
                    blProductSpecName = true;
                    ViewState["orderColumnName"] = "c.ProductSpecName asc";
                }
                break;

            case "producttypename":
                if (blProductTypeName)
                {
                    dv.Sort = "ProductTypeName desc";
                    blProductTypeName = false;
                    ViewState["orderColumnName"] = "a.ProductTypeName desc";
                }

                else
                {
                    dv.Sort = "ProductTypeName asc";
                    blProductTypeName = true;
                    ViewState["orderColumnName"] = "a.ProductTypeName asc";
                }
                break;

            case "productunitname":
                if (blProductUnitName)
                {
                    dv.Sort = "ProductUnitName desc";
                    blProductUnitName = false;
                    ViewState["orderColumnName"] = "e.ProductUnitName desc";
                }

                else
                {
                    dv.Sort = "ProductUnitName asc";
                    blProductUnitName = true;
                    ViewState["orderColumnName"] = "e.ProductUnitName asc";
                }
                break;

            case "country":
                if (blCountry)
                {
                    dv.Sort = "Country desc";
                    blCountry = false;
                    ViewState["orderColumnName"] = "f.Country desc";
                }

                else
                {
                    dv.Sort = "Country asc";
                    blCountry = true;
                    ViewState["orderColumnName"] = "f.Country asc";
                }
                break;

            case "productarea":
                if (blProductArea)
                {
                    dv.Sort = "ProductArea desc";
                    blProductArea = false;
                    ViewState["orderColumnName"] = "a.ProductArea desc";
                }

                else
                {
                    dv.Sort = "ProductArea asc";
                    blProductArea = true;
                    ViewState["orderColumnName"] = "a.ProductArea asc";
                }

                break;

            case "preferentialprice":
                if (blPreferentialPrice)
                {
                    dv.Sort = "PreferentialPrice desc";
                    blPreferentialPrice = false;
                    ViewState["orderColumnName"] = "a.PreferentialPrice desc";
                }

                else
                {
                    dv.Sort = "PreferentialPrice asc";
                    blPreferentialPrice = true;
                    ViewState["orderColumnName"] = "a.PreferentialPrice asc";
                }
                break;

            case "preferentialpv":
                if (blPreferentialPV)
                {
                    dv.Sort = "PreferentialPV desc";
                    blPreferentialPV = false;
                    ViewState["orderColumnName"] = "a.PreferentialPV desc";
                }

                else
                {
                    dv.Sort = "PreferentialPV asc";
                    blPreferentialPV = true; ;
                    ViewState["orderColumnName"] = "a.PreferentialPV asc";
                }
                break;

            case "commonprice":
                if (blCommonPrice)
                {
                    dv.Sort = "CommonPrice desc";
                    blCommonPrice = false;
                    ViewState["orderColumnName"] = "a.CommonPrice desc";
                }

                else
                {
                    dv.Sort = "CommonPrice asc";
                    blCommonPrice = true;
                    ViewState["orderColumnName"] = "a.CommonPrice asc";
                }

                break;

            case "commonpv":
                if (blCommonPV)
                {
                    dv.Sort = "CommonPV desc";
                    blCommonPV = false; ;
                    ViewState["orderColumnName"] = "a.CommonPV desc";
                }

                else
                {
                    dv.Sort = "CommonPV asc";
                    blCommonPV = true;
                    ViewState["orderColumnName"] = "a.CommonPV asc";
                }
                break;

            case "alertnesscount":
                if (blAlertnessCount)
                {
                    dv.Sort = "AlertnessCount desc";
                    blAlertnessCount = false;
                    ViewState["orderColumnName"] = "a.AlertnessCount desc";
                }

                else
                {
                    dv.Sort = "AlertnessCount asc";
                    blAlertnessCount = true;
                    ViewState["orderColumnName"] = "a.AlertnessCount asc";
                }
                break;

            case "iscombineproduct":
                if (blIsCombineProduct)
                {
                    dv.Sort = "IsCombineProduct desc";
                    blIsCombineProduct = false;
                    ViewState["orderColumnName"] = "a.IsCombineProduct desc";
                }

                else
                {
                    dv.Sort = "IsCombineProduct asc";
                    blIsCombineProduct = true;
                    ViewState["orderColumnName"] = "a.IsCombineProduct asc";
                }
                break;

            case "issell":
                if (blIsSell)
                {
                    dv.Sort = "IsSell desc";
                    blIsSell = false;
                    ViewState["orderColumnName"] = "a.IsSell desc";
                }

                else
                {
                    dv.Sort = "IsSell asc";
                    blIsSell = true;
                    ViewState["orderColumnName"] = "a.IsSell asc";
                }
                break;
        }

        this.gvProduct.DataSource = dv;
        this.gvProduct.DataBind();

        ///分页
        
        Pager pager = Page.FindControl("Pager1") as Pager;
        pager.PageBind(0, 10, ViewState["tableNames"].ToString(), ViewState["columnNames"].ToString(), ViewState["conditions"].ToString(), ViewState["orderColumnName"].ToString(), "gvProduct");
    }
}
