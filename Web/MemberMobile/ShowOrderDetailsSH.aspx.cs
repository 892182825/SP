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
using BLL.CommonClass;
using DAL;

public partial class Member_ShowOrderDetails : BLL.TranslationBase
{
     public int bzCurrency = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        //获取标准币种
        bzCurrency = CommonDataBLL.GetStandard();
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        //检查店铺权限
        Response.Cache.SetExpires(DateTime.Now);
        // Permissions.CheckMemberPermission();//检测是否已登录

        if (!IsPostBack)
        {
            if (Request.QueryString["byy"] != null)
            {
                ViewState["url"] = Request.UrlReferrer.ToString();
                string orderid = Request.QueryString["byy"];
                if (orderid.Length > 0)
                    BindData(orderid);

                //BLL.Registration_declarations.MemberOrderBLL memberOrderBLL = new BLL.Registration_declarations.MemberOrderBLL();
                string sql = "select o.ExpectNum,o.docid,oo.storeorderid,oo.orderdatetime,o.client,o.TotalMoney,o.Totalpv,oo.ConsignmentDateTime,oo.kuaididh from" +
                    "  InventoryDoc o,storeorder oo where o.storeorderid=oo.storeorderid  and o.docid='" + orderid + "'";
                DataTable dt = DBHelper.ExecuteDataTable(sql); //memberOrderBLL.GetMemberByOrderID(orderid);

                rep.DataSource = dt;
                rep.DataBind();
            }
        }
        Transllations();
    }

    protected void gvorder_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor");
        }
       

    }



    protected object GetOrderDate(object obj)
    {
        try
        {
            obj = Convert.ToDateTime(obj).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours());
        }
        catch { }
        return obj;
    }

    private void Transllations()
    {
        this.TranControls(this.gvorder,
           new string[][] { 
                new string[] { "000045", "期数" } ,
                new string[] { "000079", "发货单号" } ,
                new string[] { "000024", "会员编号" } ,
                new string[] { "000322", "金额" } ,
                new string[] { "000414", "积分" } ,
                new string[] { "001429", "报单日期" } ,
                new string[] { "000070", "发货日期" } 
            });

        this.TranControls(this.myDatGrid,
            new string[][] { 
                new string[] { "000079", "订单号" } ,
                  new string[] { "000558", "产品编号" } ,
                new string[] { "000501", "产品名称" } ,
              
                new string[] { "000505", "数量" } ,
                new string[] { "000503", "单价" } ,
                new string[] { "000414", "积分" } ,
                new string[] { "000041", "总金额" } 

            });
        this.TranControls(this.btnE, new string[][] { new string[] { "000096", "返 回" } });
    }

    private void BindData(string orderid)
    {
        //BLL.Registration_declarations.MemberOrderBLL memberOrderBLL = new BLL.Registration_declarations.MemberOrderBLL();
        string sql = "select i.*,p.productname,p.productcode from InventoryDocDetails i join product p on i.productid=p.productid where i.docid='" + orderid + "'";
        rep2.DataSource = DBHelper.ExecuteDataTable(sql); //memberOrderBLL.GetMemberDetailsByOrderID(orderid);
        rep2.DataBind();

    }

    protected void myDatGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor");
    }

    protected void btnE_Click(object sender, EventArgs e)
    {
        if (ViewState["url"] == null)
        {

        }
        else
        {
            Response.Redirect(ViewState["url"].ToString(), true);
        }
    }
}
