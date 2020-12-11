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
using BLL.Registration_declarations;
using Model;
using System.Data.SqlClient;

public partial class Member_ShopingListAgain : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxMemShopCart));
        if (Session["fxMemberModel"] == null)
        {
            Response.Redirect("OrderAgainBegin.aspx");
        }
        if (!IsPostBack)
        {
            string page = "";

            BindDataFinal();
        }
    }

    public string GetMemBh()
    {
        return ((OrderFinalModel)Session["fxMemberModel"]).Number;
    }

    /// <summary>
    /// 添加的时，绑定信息
    /// </summary>
    private void AddBind()
    {
        //绑定产品树
        ProductTree myTree = new ProductTree();
        this.menuLabel.Text = myTree.getMenuShoppingFx(BLL.CommonClass.CommonDataBLL.getManageID(2));
    }

    /// <summary>
    ///  修改时绑定信息
    /// </summary>
    private void EditBind()
    {


        //绑定产品树
        ProductTree myTree = new ProductTree();

        this.menuLabel.Text = myTree.getEditMenuShopping(BLL.CommonClass.CommonDataBLL.getManageID(2), ViewState["ProductID"].ToString());

    }

    private void BindDataFinal()
    {
        AddBind();
        if ((Request.Params["pid"] != null) && (Request.Params["pid"].ToString() != ""))
        {
            string ProductID = "";
            ProductID = Request.Params["pid"];
            ViewState["ProductID"] = ProductID;
            //EditBind();

            BindData(ProductID);
        }
        else
        {

            BindData("");
        }
    }

    private void BindData(string productid)
    {
        string sql = "";

        if (productid != "")
        {
            string pid = productid + ",";
            string pidstr = productid + ",";
            while (1 == 1)
            {
                DataTable dt = DAL.DBHelper.ExecuteDataTable("select productid from product where pid in (" + pidstr.Substring(0, pidstr.Length - 1).ToString() + ") and issell=0");
                pidstr = "";
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        pidstr = pidstr + dt.Rows[i][0].ToString() + ",";
                        pid = pid + dt.Rows[i][0].ToString() + ",";
                    }
                }
                else
                {
                    break;
                }

            }


            sql = @"select productcode,ProductID,ProductName,CommonPrice,CommonPV,PreferentialPrice,PreferentialPV,Description,ProductImage,
currency.name as currency from Product,currency where Product.currency=currency.id and Product.isfold=0 and Product.pid in (" + pid.Substring(0, pid.Length - 1).ToString() + ")  and Product.issell=0 and (Product.Yongtu=0 or Product.Yongtu=2)";
        }
        else
        {
            sql = @"select productcode,ProductID,ProductName,CommonPrice,CommonPV,PreferentialPrice,PreferentialPV,Description,ProductImage,
currency.name as currency from Product,currency where Product.currency=currency.id and Product.isfold=0  and Product.issell=0 and (Product.Yongtu=0 or Product.Yongtu=2)";
        }

        if (ViewState["priceStu"] != null)
        {
            int priceStu = -1;
            int.TryParse(ViewState["priceStu"].ToString(), out priceStu);

            if (priceStu != -1)
            {
                switch (priceStu)
                {
                    case 0:
                        sql += " and PreferentialPrice>=0 and PreferentialPrice<=100";
                        break;
                    case 1:
                        sql += " and PreferentialPrice>=101 and PreferentialPrice<=500";
                        break;
                    case 2:
                        sql += " and PreferentialPrice>=501 and PreferentialPrice<=1000";
                        break;
                    case 3:
                        sql += " and PreferentialPrice>=1001 and PreferentialPrice<=2000";
                        break;
                    case 4:
                        sql += " and PreferentialPrice>=2001";
                        break;
                }
            }
        }

        if (ViewState["PvStu"] != null)
        {
            int priceStu = -1;
            int.TryParse(ViewState["PvStu"].ToString(), out priceStu);

            if (priceStu != -1)
            {
                switch (priceStu)
                {
                    case 0:
                        sql += " and PreferentialPV>=0 and PreferentialPV<=100";
                        break;
                    case 1:
                        sql += " and PreferentialPV>=101 and PreferentialPV<=500";
                        break;
                    case 2:
                        sql += " and PreferentialPV>=501 and PreferentialPV<=1000";
                        break;
                    case 3:
                        sql += " and PreferentialPV>=1001 and PreferentialPV<=2000";
                        break;
                    case 4:
                        sql += " and PreferentialPV>=2001";
                        break;
                }
            }
        }

        if (txtProName.Text.Trim() != "" && txtProName.Text != "请输入产品关键词")
        {
            sql += " and ProductName like'%" + txtProName.Text.Trim() + "%'";
        }

        if (txt1.Text.Trim() != "" && txt2.Text.Trim() != "")
        {
            int k1 = 0;
            int.TryParse(txt1.Text.Trim(), out k1);
            int k2 = 0;
            int.TryParse(txt2.Text.Trim(), out k2);
            sql += " and " + ddlProList.SelectedValue + ">=" + k1 + " and " + ddlProList.SelectedValue + "<=" + k2;
        }

        if (ddlSort.SelectedValue != "-1" && ddlSort.SelectedValue != "-2")
        {
            sql += " order by " + ddlSort.SelectedValue;
        }

        ucPagerMb1.PageSize = 16;
        ucPagerMb1.PageInit(sql, Repeater1.UniqueID);

        //显示数量

    }

    /// <summary>
    /// 得到图片路径
    /// </summary>
    /// <param name="strArgument"></param>
    /// <returns></returns>
    protected string FormatURL(object strArgument)
    {
        string result = "../ReadImage.aspx?ProductID=" + strArgument.ToString();
        if (result == "" || result == null)
        {
            result = "";
        }
        return result;
    }

    public string GetLiuLanPro()
    {
        //mtType:1公司 2店铺 3会员   odType:0注册 1复消
        string sql = "select top 7 * from MemShopCart,product where MemShopCart.proid=product.productid and memBh=@memBh and mType=3 and odType=1 order by id desc";
        SqlParameter[] sp = new SqlParameter[] {       
            new SqlParameter("@memBh",GetMemBh())
             

        };
        DataTable dt = DAL.DBHelper.ExecuteDataTable(sql, sp, CommandType.Text);
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        if (dt.Rows.Count == 0)
        {
            dt = DAL.DBHelper.ExecuteDataTable("select top 7 * from product where isfold=0 and issell=0 and (Product.Yongtu=0 or Product.Yongtu=2) order by productid desc");
        }

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            sb.Append(@"<dl>
            	<dd><img src='" + FormatURL(dt.Rows[i]["ProductID"]) + "' width='55' height='55' /></dd><dt>" + dt.Rows[i]["productname"].ToString() + "<br />金额：￥" + Convert.ToDouble(dt.Rows[i]["PreferentialPrice"]).ToString("0.00") + "<br />PV:" + Convert.ToDouble(dt.Rows[i]["PreferentialPV"]).ToString("0.00") + "</dt></dl>");
        }
        return sb.ToString();

    }

    protected void PriceAll_Click(object sender, EventArgs e)
    {
        Price0.Style.Clear();
        Price1.Style.Clear();
        Price2.Style.Clear();
        Price3.Style.Clear();
        Price4.Style.Clear();
        PriceAll.Style.Add("border", "1px solid #FC0");
        PriceAll.Style.Add("background-color", "#FFC");
        PriceAll.Style.Add("color", "#B20000");
        PriceAll.Style.Add("padding", "0px 2px 2px");
        PriceAll.Style.Add("text-decoration", "underline");

        ViewState["priceStu"] = "-1";

        BindDataFinal();
    }
    protected void Price0_Click(object sender, EventArgs e)
    {
        PriceAll.Style.Clear();
        Price1.Style.Clear();
        Price2.Style.Clear();
        Price3.Style.Clear();
        Price4.Style.Clear();
        Price0.Style.Add("border", "1px solid #FC0");
        Price0.Style.Add("background-color", "#FFC");
        Price0.Style.Add("color", "#B20000");
        Price0.Style.Add("padding", "0px 2px 2px");
        Price0.Style.Add("text-decoration", "underline");

        ViewState["priceStu"] = "0";

        BindDataFinal();
    }
    protected void Price1_Click(object sender, EventArgs e)
    {
        PriceAll.Style.Clear();
        Price0.Style.Clear();
        Price2.Style.Clear();
        Price3.Style.Clear();
        Price4.Style.Clear();
        Price1.Style.Add("border", "1px solid #FC0");
        Price1.Style.Add("background-color", "#FFC");
        Price1.Style.Add("color", "#B20000");
        Price1.Style.Add("padding", "0px 2px 2px");
        Price1.Style.Add("text-decoration", "underline");

        ViewState["priceStu"] = "1";

        BindDataFinal();
    }
    protected void Price2_Click(object sender, EventArgs e)
    {
        PriceAll.Style.Clear();
        Price0.Style.Clear();
        Price1.Style.Clear();
        Price3.Style.Clear();
        Price4.Style.Clear();
        Price2.Style.Add("border", "1px solid #FC0");
        Price2.Style.Add("background-color", "#FFC");
        Price2.Style.Add("color", "#B20000");
        Price2.Style.Add("padding", "0px 2px 2px");
        Price2.Style.Add("text-decoration", "underline");

        ViewState["priceStu"] = "2";

        BindDataFinal();
    }
    protected void Price3_Click(object sender, EventArgs e)
    {
        PriceAll.Style.Clear();
        Price0.Style.Clear();
        Price1.Style.Clear();
        Price2.Style.Clear();
        Price4.Style.Clear();
        Price3.Style.Add("border", "1px solid #FC0");
        Price3.Style.Add("background-color", "#FFC");
        Price3.Style.Add("color", "#B20000");
        Price3.Style.Add("padding", "0px 2px 2px");
        Price3.Style.Add("text-decoration", "underline");

        ViewState["priceStu"] = "3";

        BindDataFinal();
    }
    protected void Price4_Click(object sender, EventArgs e)
    {
        PriceAll.Style.Clear();
        Price0.Style.Clear();
        Price1.Style.Clear();
        Price2.Style.Clear();
        Price3.Style.Clear();
        Price4.Style.Add("border", "1px solid #FC0");
        Price4.Style.Add("background-color", "#FFC");
        Price4.Style.Add("color", "#B20000");
        Price4.Style.Add("padding", "0px 2px 2px");
        Price4.Style.Add("text-decoration", "underline");

        ViewState["priceStu"] = "4";

        BindDataFinal();
    }
    protected void PvAll_Click(object sender, EventArgs e)
    {
        Pv0.Style.Clear();
        Pv1.Style.Clear();
        Pv2.Style.Clear();
        Pv3.Style.Clear();
        Pv4.Style.Clear();
        PvAll.Style.Add("border", "1px solid #FC0");
        PvAll.Style.Add("background-color", "#FFC");
        PvAll.Style.Add("color", "#B20000");
        PvAll.Style.Add("padding", "0px 2px 2px");
        PvAll.Style.Add("text-decoration", "underline");

        ViewState["PvStu"] = "-1";

        BindDataFinal();
    }
    protected void Pv0_Click(object sender, EventArgs e)
    {
        PvAll.Style.Clear();
        Pv1.Style.Clear();
        Pv2.Style.Clear();
        Pv3.Style.Clear();
        Pv4.Style.Clear();
        Pv0.Style.Add("border", "1px solid #FC0");
        Pv0.Style.Add("background-color", "#FFC");
        Pv0.Style.Add("color", "#B20000");
        Pv0.Style.Add("padding", "0px 2px 2px");
        Pv0.Style.Add("text-decoration", "underline");

        ViewState["PvStu"] = "0";

        BindDataFinal();
    }
    protected void Pv1_Click(object sender, EventArgs e)
    {
        PvAll.Style.Clear();
        Pv0.Style.Clear();
        Pv2.Style.Clear();
        Pv3.Style.Clear();
        Pv4.Style.Clear();
        Pv1.Style.Add("border", "1px solid #FC0");
        Pv1.Style.Add("background-color", "#FFC");
        Pv1.Style.Add("color", "#B20000");
        Pv1.Style.Add("padding", "0px 2px 2px");
        Pv1.Style.Add("text-decoration", "underline");

        ViewState["PvStu"] = "1";

        BindDataFinal();
    }
    protected void Pv2_Click(object sender, EventArgs e)
    {
        PvAll.Style.Clear();
        Pv0.Style.Clear();
        Pv1.Style.Clear();
        Pv3.Style.Clear();
        Pv4.Style.Clear();
        Pv2.Style.Add("border", "1px solid #FC0");
        Pv2.Style.Add("background-color", "#FFC");
        Pv2.Style.Add("color", "#B20000");
        Pv2.Style.Add("padding", "0px 2px 2px");
        Pv2.Style.Add("text-decoration", "underline");

        ViewState["PvStu"] = "2";

        BindDataFinal();
    }
    protected void Pv3_Click(object sender, EventArgs e)
    {
        PvAll.Style.Clear();
        Pv0.Style.Clear();
        Pv1.Style.Clear();
        Pv2.Style.Clear();
        Pv4.Style.Clear();
        Pv3.Style.Add("border", "1px solid #FC0");
        Pv3.Style.Add("background-color", "#FFC");
        Pv3.Style.Add("color", "#B20000");
        Pv3.Style.Add("padding", "0px 2px 2px");
        Pv3.Style.Add("text-decoration", "underline");

        ViewState["PvStu"] = "3";

        BindDataFinal();
    }
    protected void Pv4_Click(object sender, EventArgs e)
    {
        PvAll.Style.Clear();
        Pv0.Style.Clear();
        Pv1.Style.Clear();
        Pv2.Style.Clear();
        Pv3.Style.Clear();
        Pv4.Style.Add("border", "1px solid #FC0");
        Pv4.Style.Add("background-color", "#FFC");
        Pv4.Style.Add("color", "#B20000");
        Pv4.Style.Add("padding", "0px 2px 2px");
        Pv4.Style.Add("text-decoration", "underline");

        ViewState["PvStu"] = "4";

        BindDataFinal();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        BindDataFinal();
    }
    protected void ddlSort_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDataFinal();
    }
}

