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
using System.Data.SqlClient;
using BLL.Registration_declarations;
using BLL.CommonClass;
using BLL.other;
using Standard.Classes;
using DAL;
using System.Text;
using BLL.Logistics;

public partial class Member_Receiving : BLL.TranslationBase
{
    public int bzCurrency = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        //获取标准币种
        bzCurrency = CommonDataBLL.GetStandard();
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);

        if (!this.IsPostBack)
        {
            CommonDataBLL.BindQishuList(ddlQiShu, true);
            GetShopList();
        }
        Transllations();
    }


    private void Transllations()
    {
        this.TranControls(this.btnSearch, new string[][] { new string[] { "000340", "查询" } });
        this.TranControls(this.rdbtnType, new string[][] { new string[] { "000633", "全部" }, new string[] { "007538", "已收获" }, new string[] { "007539", "未收货" } });
        this.TranControls(this.Button1, new string[][] { new string[] { "8240", "确认收货" } });
        this.TranControls(this.gvorder,
            new string[][] {
                new string[] { "000015", "操作" } ,
                new string[] { "000811", "明细" } ,
                new string[] { "002158", "发货单号" } ,
                new string[] { "000079", "订单号" } ,
                 new string[] { "007206", "快递单号" } ,
                new string[] { "000045", "期数" } ,

                new string[] { "000322", "金额" } ,
                new string[] { "000414", "积分" } ,
                new string[] { "001429", "报单日期" } ,

            });
    }

    /// <summary>
    /// 转换日期
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    protected object GetOrderDate(object obj)
    {
        try
        {
            obj = Convert.ToDateTime(obj).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours());
        }
        catch { }
        return obj;
    }

    private void GetShopList()
    {

        string number = Session["Member"].ToString();
        string sql = "o.storeorderid=s.storeorderid and o.issend=1 and o.doctypeid=2 and s.sendway=1 and  o.client='" + number + "' ";

        if (ddlQiShu.SelectedValue != "-1")
        {
            sql += "and o.ExpectNum=" + ddlQiShu.SelectedValue;
        }
        if (txtDate.Text != "" || txtDateEnd.Text != "")
        {
            sql += " and  o.docmaketime between '" + txtDate.Text + "' and '" + txtDateEnd.Text + "' ";
        }
        if (rdbtnType.SelectedValue != "-1")
        {
            sql += " and o.isreceived=" + rdbtnType.SelectedValue;
        }

        this.Pager1.ControlName = "rep";
        this.Pager1.key = "o.docid";
        this.Pager1.PageColumn = " o.isreceived,o.ExpectNum,o.docid,s.storeorderid,s.orderdatetime,o.client,o.TotalMoney,o.Totalpv,s.kuaididh,s.ConveyanceCompany,o.id,o.DocID ";
        this.Pager1.Pageindex = 0;
        this.Pager1.PageTable = " InventoryDoc o,storeorder s";
        this.Pager1.Condition = sql;
        this.Pager1.PageSize = 10;
        this.Pager1.PageCount = 0;
        this.Pager1.PageBind();

    }



    protected void gvorder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            HiddenField hf = e.Row.FindControl("HiddenField1") as HiddenField;
            if (hf.Value.Trim() == "1")
            {
                CheckBox cb = e.Row.FindControl("CB") as CheckBox;
                cb.Visible = false;
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetShopList();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string orderids = "";
        int count = 0;

        HiddenField HF = (HiddenField)FindControl("HF");
        orderids += HF.Value + ",";
        count++;

        if (count == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('请选择要确认的货单');", true);
            return;
        }

        if (new AffirmConsignBLL().Submit(orderids))
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('确认成功');", true);
            GetShopList();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('确认失败');", true);
            GetShopList();
        }
    }


    protected void rep_km_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "qrsh":
                string KeywordCode1 = e.CommandArgument.ToString();

                string orderids = "";
                orderids = KeywordCode1;
                if (new AffirmConsignBLL().Submit1(orderids))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('" + GetTran("008371", "确认成功") + "');", true);
                    GetShopList();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('" + GetTran("008372", "确认失败") + "');", true);
                    GetShopList();
                }
                break;
        }

    }


    protected void rep_km_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HiddenField h = e.Item.FindControl("HF") as HiddenField;
            LinkButton b1 = e.Item.FindControl("Button1") as LinkButton;
            b1.Text = GetTran("004025", "收货确认");
            Label lb1= e.Item.FindControl("ysh") as Label;
            lb1.Text = GetTran("007538", "已收货");

             
            string dt_one = "select o.IsReceived from InventoryDoc o, storeorder s where o.storeorderid = s.storeorderid and o.issend = 1 and o.doctypeid = 2 and s.sendway = 1 and o.DocID = '" + h.Value + "'";
            DataTable dt = DAL.DBHelper.ExecuteDataTable(dt_one);
            var IsReceived = dt.Rows[0]["IsReceived"].ToString();
            if (IsReceived == "1")
            {
                b1.Visible = false;
                lb1.Visible = true;
            }
            else {

                b1.Visible = true;
                lb1.Visible = false;
            }


        }


    }


}
