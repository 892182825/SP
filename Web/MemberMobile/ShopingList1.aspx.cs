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
using DAL;
using System.Text;
using System.Collections.Generic;
using BLL.CommonClass;

public partial class Member_ShopingList : BLL.TranslationBase
{
    public int bzCurrency = 0;
    protected LetUsOrder luo = new LetUsOrder();
    public int countrycode = 86;  
    DataTable dt2 = new DataTable();
    public string  defc = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //获取标准币种
        bzCurrency = CommonDataBLL.GetStandard();
        Permissions.ThreeRedirect(Page, "../Member/" + Permissions.redirUrl);
        luo.SetVlaue();
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxMemShopCart));
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        if (!IsPostBack)
        {
            string str = Session["languageCode"].ToString();
            if (str == "L002")
            {
                countrycode = 1;
            }

            if (Request["type"] == null)
            {
                Session["mbreginfo"] = null;
                Session["EditOrderID"] = null;
                Session["fxMemberModel"] = null;
            }
            if (Session["Default_Currency"] != null)
            {
                defc = Session["Default_Currency"].ToString();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请先登录！'); location.href='index.aspx';</script>");

            }

            ///公司系统、店铺系统进入，会员系统的头部、尾部控件隐藏
            if (Session["UserType"] != null && Session["UserType"].ToString() != "3") // 公司/店铺 系统
            {
                if (Session["UserType"].ToString() == "2")
                {
                    top.Visible = false;
                    bottom.Visible = false;
                    STop1.Visible = true;
                    SLeft1.Visible = true;
                }
                else
                {
                    top.Visible = false;
                    bottom.Visible = false;
                    STop1.Visible = false;
                    SLeft1.Visible = false;
                }


                Session["OrderType"] = luo.OrderType;
                Session["LUOrder"] = luo.MemBh + "," + luo.OrderType;
            }
            else
            {
                top.Visible = true;
                bottom.Visible = true;
                STop1.Visible = false;
                SLeft1.Visible = false;
                //注册
                if (HttpContext.Current.Request["type"] != null && HttpContext.Current.Request["type"].ToString() == "reg")
                {
                    if (Session["Member"] != null) //会员注册
                    {
                        Session["OrderType"] = 21;
                        Session["LUOrder"] = GetMemBh() + ",21";
                    }
                }
                //复消
                else if (HttpContext.Current.Request["type"] != null && HttpContext.Current.Request["type"].ToString() == "new")
                {
                   
                    if (HttpContext.Current.Session["Store"] != null)
                    {
                        Session["OrderType"] = 12;
                        Session["LUOrder"] = GetMemBh() + ",12";
                    }
                    else if (HttpContext.Current.Session["Member"] != null)
                    {
                        Session["OrderType"] = 12;
                        Session["LUOrder"] = GetMemBh() + ",12";
                    }
                }

                //升级
                else if (HttpContext.Current.Request["type"] != null && HttpContext.Current.Request["type"].ToString() == "Sj")
                {

                    if (HttpContext.Current.Session["Store"] != null)
                    {
                        Session["OrderType"] = 13;
                        Session["LUOrder"] = GetMemBh() + ",13";
                    }
                    else if (HttpContext.Current.Session["Member"] != null)
                    {
                        Session["OrderType"] = 23;
                        Session["LUOrder"] = GetMemBh() + ",23";
                    }
                }

                //复消提货
                else if (HttpContext.Current.Request["type"] != null && HttpContext.Current.Request["type"].ToString() == "Fxth")
                {
                    if (HttpContext.Current.Session["Member"] != null)
                    {
                        Session["OrderType"] = 25;
                        Session["LUOrder"] = GetMemBh() + ",25";
                    }
                }
            }

            //非注册，清楚注册session
            if (HttpContext.Current.Request["type"] != null && HttpContext.Current.Request["type"].ToString() != "reg")
            {
                Session.Remove("mbreginfo");
                Session["fxMemberModel"] = MemberInfoDAL.getMemberInfo(GetMemBh());
            }

            BindDataFinal();
            Translations();
        }
    }

    private void Translations()
    {
        this.TranControls(this.Button1, new string[][] { new string[] { "000011", "搜索" } });
        this.TranControls(this.ImageButton1, new string[][] { new string[] { "000064", "000064" } });
        this.TranControls(this.ddlSort, new string[][] { new string[] { "007382", "默认排序" }, new string[] { "007384", "价格从低到高" }, new string[] { "007385", "价格从高到低" }, new string[] { "007388", "PV价格从高到低" }, new string[] { "007389", "PV从低到高" } });
        this.TranControls(this.ddlProList, new string[][] { new string[] { "002084", "价格" }, new string[] { "000000", "PV" } });
        txtProName.Text = GetTran("007373", "请输入产品关键词");
       

    }

    public string GetMemBh()
    {
        /*
        if (Session["LUOrder"] != null)
        {
            string[] str = Session["LUOrder"].ToString().Split(',');
            return str[0];

        }
        //if (Session["OrderType"] != null)
        //{
        //    if (Session["OrderType"].ToString() == "21" || Session["OrderType"].ToString() == "11" || Session["OrderType"].ToString() == "31")
        //    {
        //        return ((MemberInfoModel)Session["mbreginfo"]).Number;
        //    }
        //    else
        //    {
        //        return Session["Member"].ToString();
        //    }
        //}
        else
        {
            return Session["Member"].ToString();
        }*/

        if (Session["OrderType"] != null)
        {
            if (Session["OrderType"].ToString() == "21" || Session["OrderType"].ToString() == "11" || Session["OrderType"].ToString() == "31")
            {
                if (Session["mbreginfo"] != null)
                    return ((MemberInfoModel)Session["mbreginfo"]).Number;
                else if (Session["fxMemberModel"] != null)
                    return ((MemberInfoModel)Session["fxMemberModel"]).Number;
                else if (Session["LUOrder"] != null)
                {
                    string[] str = Session["LUOrder"].ToString().Split(',');
                    return str[0];
                }
            }
            else
            {
                if (Session["Member"] != null)
                    return Session["Member"].ToString();
                else if (Session["LUOrder"] != null)
                {
                    string[] str = Session["LUOrder"].ToString().Split(',');
                    return str[0];
                }
            }
        }
        return Session["Member"].ToString();
    }

    /// <summary>
    /// 添加的时，绑定信息
    /// </summary>
    private void AddBind()
    {
        //绑定产品树
        //ProductTree myTree = new ProductTree();
        //this.menuLabel.Text = myTree.getMenuShopping(BLL.CommonClass.CommonDataBLL.getManageID(2));
    }

    /// <summary>
    ///  修改时绑定信息
    /// </summary>
    private void EditBind()
    {
        //绑定产品树
        ProductTree myTree = new ProductTree();
        //this.menuLabel.Text = myTree.getEditMenuShopping(BLL.CommonClass.CommonDataBLL.getManageID(2), ViewState["ProductID"].ToString());
    }

    private void BindDataFinal()
    {
        AddBind();
        if ((Request.Params["pid"] != null) && (Request.Params["pid"].ToString() != ""))
        {
            string ProductID = "";
            ProductID = Request.Params["pid"];
            ViewState["ProductID"] = ProductID;

            //string sql = "select productid,productname from product where isfold=1 and productid=" + ProductID;
            //DataTable dt = DBHelper.ExecuteDataTable(sql);
            //rptFold.DataSource = dt;
            //rptFold.DataBind();
            if (Convert.ToInt32(DBHelper.ExecuteScalar("select count(productid) from product where countrycode=" + countrycode + " and isfold=1 and pid=" + ProductID)) > 0)
            {
                Session["Pid"] = ProductID;
                string sql = "";
                if (ProductID == "1")
                {
                    sql = "select productid,productname from product  where countrycode=" + countrycode + " and isfold=1 and pid=" + ProductID;
                }
                else
                {
                    sql = "select productid,productname from product where countrycode=" + countrycode + " and isfold=1 and pid=" + ProductID;
                }
                DataTable dt = DBHelper.ExecuteDataTable(sql);
                rptFold.DataSource = dt;
                rptFold.DataBind();
            }
            else
            {
                string sql = "";
                if (Session["Pid"] == null)
                {
                    sql = "select productid,productname from product where countrycode=" + countrycode + " and isfold=1 and pid=1";
                }
                else
                {
                    sql = "select productid,productname from product where countrycode=" + countrycode + " and isfold=1 and productid=" + ProductID;
                }
                DataTable dt = DBHelper.ExecuteDataTable(sql);
                rptFold.DataSource = dt;
                rptFold.DataBind();
            }

            List<string> newlist = new List<string>();

            string str = "<a href='shopinglist.aspx?pid=" + ProductID + "'> " + DBHelper.ExecuteScalar("select productname from product where productid=" + ProductID) + "</a>";

            newlist.Add(str);
            newlist.AddRange(getList(ProductID));

            for (int i = 0; i < newlist.Count; i++)
            {
                list.InnerHtml += newlist[newlist.Count - i - 1] + ">>";
            }

            BindData(ProductID);
        }
        else
        {

            list.InnerHtml = "<a href='shopinglist.aspx?pid=" + 1 + "'> " + DBHelper.ExecuteScalar("select productname from product where productid=1").ToString() + "</a> >>";
            string sql = "select productid,productname from product where countrycode=" + countrycode + " and isfold=1 and pid=1";
            dt2 = DBHelper.ExecuteDataTable(sql);
            rptFold.DataSource = dt2;//产品目录
            rptFold.DataBind();
            BindData("");
        }
    }

    public List<string> getList(string productid)
    {
        List<string> strlist = new List<string>();
        string pid = DBHelper.ExecuteScalar("select pid from product where productid=" + productid).ToString();
        if (pid != "0")
        {
            string str = "<a href='shopinglist.aspx?pid=" + pid + "'> " + DBHelper.ExecuteScalar("select productname from product where productid=" + pid) + "</a>";
            strlist.Add(str);
            strlist.AddRange(getList(pid));
        }
        return strlist;
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
currency.name as currency from Product,currency where Product.countrycode=" + countrycode + " and  Product.currency=currency.id and Product.isfold=0 and Product.pid in (" + pid.Substring(0, pid.Length - 1).ToString() + ")  and Product.issell=0";
        }
        else
        {
            sql = @"select productcode,ProductID,ProductName,CommonPrice,CommonPV,PreferentialPrice,PreferentialPV,Description,ProductImage,
currency.name as currency from Product,currency where Product.countrycode=" + countrycode + " and  Product.currency=currency.id and Product.isfold=0  and Product.issell=0";
        }

        if (luo.OrderType == 31 || luo.OrderType == 11 || luo.OrderType == 21)
        {
            sql += " and (Product.Yongtu=0 or Product.Yongtu=1)";
        }
        else
        {
            sql += " and (Product.Yongtu=0 or Product.Yongtu=2)";
        }


        if (txtProName.Text.Trim() != "" && txtProName.Text != GetTran("007373", "请输入产品关键词"))
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
        else
        {
            sql += " order by productid desc";
        }

           //DataTable tab= DBHelper.ExecuteDataTable(sql);
           //rep.DataSource = tab;
           //rep.DataBind();
           ucPagerMb1.PageSize = 16;
           ucPagerMb1.PageInit(sql, rep.UniqueID);
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
        string sql = "select top 7 * from MemShopCart,product where MemShopCart.proid=product.productid and memBh=@memBh and mType=" + Session["UserType"].ToString() + " and odType=" + Session["LUOrder"].ToString().Split(',')[1] + " order by id desc";
        SqlParameter[] sp = new SqlParameter[] {
            new SqlParameter("@memBh",GetMemBh())


        };
        DataTable dt = DAL.DBHelper.ExecuteDataTable(sql, sp, CommandType.Text);
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        if (dt.Rows.Count == 0)
        {
            //dt = DAL.DBHelper.ExecuteDataTable("select top 7 * from product where isfold=0 and issell=0 and (Product.Yongtu=0 or Product.Yongtu=2) order by productid desc");
        }

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            //            sb.Append(@"<dl>
            //            	<dd><img src='" + FormatURL(dt.Rows[i]["ProductID"]) + "' width='55' height='55' /></dd><dt>" + dt.Rows[i]["productname"].ToString() + "<br />金额：￥" + Convert.ToDouble(dt.Rows[i]["PreferentialPrice"]).ToString("0.00") + "<br />PV:" + Convert.ToDouble(dt.Rows[i]["PreferentialPV"]).ToString("0.00") + "</dt></dl>");

            sb.Append(@" <div class='prcClassImg'><h6 class='pImages' style='margin-right: 30px;'><img src='" + FormatURL(dt.Rows[i]["ProductID"]) + "' width='64' height='54' /></h6> <ul>	<li><a href='#'>" + dt.Rows[i]["productname"].ToString() + "</a></li>"
                    + "<li><a href='#'>" + GetTran("002084", "价格") + "：￥" + Convert.ToDouble(dt.Rows[i]["PreferentialPrice"]).ToString("0.00") + GetTran("000564", "元") + "</a></li> <li><a href='#'>PV：" + Convert.ToDouble(dt.Rows[i]["PreferentialPV"]).ToString("0.00") + "</a></li> </ul> <div style='clear:both'></div>  </div>");
        }
        return sb.ToString();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        BindDataFinal();
    }
    protected void ddlSort_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDataFinal();
    }

    protected void rptFold_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //if (e.Item.ItemType == ListItemType.Item)
        //{
        //    HiddenField HFPid = e.Item.FindControl("HFPid") as HiddenField;

        //    string sql = "select productid,productname from product where isfold=1  ";

        //    Repeater childfold = e.Item.FindControl("childfold") as Repeater;

        //    DataTable dt = DBHelper.ExecuteDataTable(sql);
            
        //    childfold.DataSource = dt;
        //    childfold.DataBind();
        //}
    }
}