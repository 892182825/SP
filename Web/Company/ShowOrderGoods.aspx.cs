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
using BLL.other.Company;
using BLL.Logistics;
using System.IO;
using System.Text;
using DAL;
using Standard.Classes;
using Model.Other;
using Encryption;
using BLL;
using BLL.Registration_declarations;
public partial class Company_ShowOrderGoods : BLL.TranslationBase
{
   private string isButton = "";
    private int type = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        if (!IsPostBack)
        {
            Response.Cache.SetExpires(DateTime.Now);
            Permissions.CheckManagePermission(EnumCompanyPermission.LogisticsBrowseStoreOrders);
            //
            this.DropCurrency.DataSource = CountryBLL.GetCountryModels();
            this.DropCurrency.DataTextField = "name";
            this.DropCurrency.DataValueField = "id";
            this.DropCurrency.DataBind();

            listCondition();

            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int lastDay = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

            /*txtBox_OrderDateTimeStart.Text = txtBox_ConsignmentDateTimeStart.Text = year + "-" + month + "-" + "01";
            txtBox_OrderDateTimeEnd.Text = txtBox_ConsignmentDateTimeEnd.Text = year + "-" + month + "-" + lastDay;*/
            if (Request.QueryString["type"] != null)
            {
                type = 1;
            }
            //Button1_Click(null, null);
            getbtCon();
        }
        
        SetFanY();

        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
    }

    public void SetFanY()
    {
        this.TranControls(this.butt_Query, new string[][] { new string[] { "000048", "查 询" } });

        this.TranControls(this.GridView_Order,
                            new string[][]{	
                                new string []{"000022","删除"},													 
                                new string []{"000339","详细"},													 
                                new string []{"000098","订货店铺"},
                                new string []{"000079","订单号"},	
                                new string []{"000045","期数"},
                                new string []{"001345","发货方式"},
                                new string []{"000100","付款否"},
                                new string []{"000106","订单类型"},
                                new string []{"000107","姓名"},
                                new string []{"000108","收货人国家"},												 
                                //new string []{"000109","省份"},												 
                                //new string []{"000110","城市"},												 
                                new string []{"000112","收货地址"},
                                new string []{"000073","邮编"},
                                new string []{"000041","总金额"},
                                new string []{"000113","总积分"},
                                new string []{"000115","联系电话"},
                                new string []{"000118","重量"},
                                new string []{"000120","运费"},
                                new string []{"000067","订货日期"},
                                new string []{"000078","备注"}
                                });

        this.TranControls(this.DropDownList_Items,
                            new string[][]{	
                                new string []{"000098","订货店铺"},													 
                                new string []{"000079","订单号"},			
                                //new string []{"000114","邮政编码"},	
                                //new string []{"000328","是否发货"},
                                //new string []{"000121","物流公司"},
                                new string []{"000107","姓名"},
                                new string []{"000108","收货人国家"},
                                new string []{"000109","省份"},												 
                                new string []{"000110","城市"},												 
                                new string []{"000112","收货地址"},												 
                                new string []{"000115","联系电话"},

                                new string []{"000041","总金额"},
                                new string []{"000045","期数"}
                                });
    }

    public string SetTimeFormat(string timestr)
    {
        return timestr.Split(' ')[0];
    }

    public string SetFormatString(string str, int len)
    {
        if (str.Length >= len)
            return str.Substring(0, len) + "...";
        return str;
    }

    public string GetBiaoZhunTime(string dt)
    {
        if (Convert.ToDateTime(dt) < Convert.ToDateTime("1910-01-01 00:00:00"))
            return "——";

        return Convert.ToDateTime(dt).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToShortDateString();
    }

    //用于导出
    public string GetBiaoZhunTime(string dt,string ty)
    {
        return Convert.ToDateTime(dt).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToShortDateString();
    }

    protected void DropDownList_Items_SelectedIndexChanged(object sender, EventArgs e)
    {
        listCondition();

    }

    private void listCondition()
    {
        if (DropDownList_Items.SelectedIndex <= 10)
        {
            DropDownList_condition.Items.Clear();
            DropDownList_condition.Items.Add(new ListItem(GetTran("000378","包含"), "like"));

            txtBox_keyWords.Visible = true;
            txtBox_rq.Visible = false;
        }
        if (DropDownList_Items.SelectedIndex >= 11)
        {
            DropDownList_condition.Items.Clear();
            DropDownList_condition.Items.Add(new ListItem(GetTran("000361","大于"), ">"));
            DropDownList_condition.Items.Add(new ListItem(GetTran("000364","大于等于"), ">="));
            DropDownList_condition.Items.Add(new ListItem(GetTran("000367","小于"), "<"));
            DropDownList_condition.Items.Add(new ListItem(GetTran("000368","小于等于"), "<="));
            DropDownList_condition.Items.Add(new ListItem(GetTran("000372","等于"), "="));

            txtBox_keyWords.Visible = true;
            txtBox_rq.Visible = false;
        }
        /*if (DropDownList_Items.SelectedIndex == 8 || DropDownList_Items.SelectedIndex == 9 || DropDownList_Items.SelectedIndex == 10)
        {
            txtBox_keyWords.Visible = false;
            txtBox_rq.Visible = true;
        }*/
    }

    public string GetOrderType(string str)
    {
        if (str == "1")
            return GetTran("000391", "周转货");
        else if (str == "2")
            return GetTran("007900", "被动订货");
        else if (str == "0")
            return GetTran("000402", "报单订货");
        else
            return str;
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

        string storeOrderID = ((Label)GridView_Order.SelectedRow.FindControl("Lab_StoreOrderID")).Text;

        if (isButton == "sel")
        {
            Response.Redirect("OrderGoodsDetail.aspx?storeOrderID=" + storeOrderID);
        }
        else
            if (isButton == "del")
            {

                if (OrdersBrowseBLL.DelStoreOrderItem(storeOrderID))
                {
                    string condition = "";
                    if (ViewState["condition"] == null)
                        condition = "1=1";
                    else
                        condition = ViewState["condition"].ToString();

                    //刷新
                    Pager1.PageBind(0, 10, "StoreOrder  so  left outer join city on so.cpccode=city.cpccode ", @"StoreID,OrderGoodsID,ExpectNum,IsCheckOut,
                             OrderType,InceptPerson,city.country,city.province,city.city,
                            InceptAddress,PostalCode,TotalMoney,TotalPV,Telephone,Weight,Carriage
                            ,OrderDateTime,Description", condition, "OrderGoodsID", "GridView_Order", "OrderDateTime", 0);

                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000008", "删除成功") + "')</script>");
                   
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000417", "删除失败") + "')</script>");
                }
            }
    }

    public string GetRemark(string remark, string storeOrderID)
    {
        if (String.IsNullOrEmpty(remark))
        {
            return new TranslationBase().GetTran("000221");
        }
        else
        {
            return "<a href='ShowStoreOrderNote.aspx?storeOrderID=" + storeOrderID + "'>" + GetTran("000440","查看") + "</a>";
        }
    }

    public string GetShouHuoWT(string remark, string storeOrderID)
    {
        if (String.IsNullOrEmpty(remark))
        {
            return new TranslationBase().GetTran("000221");
        }
        else
        {
            return "<a href='ShowShouHuoWT.aspx?storeOrderID=" + storeOrderID + "'>" + GetTran("000440", "查看") + "</a>";
        }
    }

    public string StringFormat(string sf)
    {
        if (sf == "Y")
            return new TranslationBase().GetTran("000233");
        else
            return new TranslationBase().GetTran("000235");
    }

    public bool IsDelete(string isCheckOut)
    {
        if (isCheckOut == "N")
            return true;
        else
            return false;

    }
    protected void lkb_Click(object sender, EventArgs e)
    {
        isButton = "sel";
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        isButton = "del";
    }

    protected void getbtCon()
    {
        ////
        string condition = "";

        string currency = "";

        if (DropCurrency.SelectedItem != null)
            currency = DropCurrency.SelectedItem.Text;

        string tiaoj = DropDownList_Items.SelectedItem.Value;
        string fh = DropDownList_condition.Text;
        string keyword = "";
        //if (DropDownList_Items.SelectedIndex == 8 || DropDownList_Items.SelectedIndex == 9 || DropDownList_Items.SelectedIndex == 10)
        //    keyword = txtBox_rq.Text.Trim();
        //else
            keyword = txtBox_keyWords.Text.Trim().Replace("'", "").Replace("〖", "[").Replace("〗", "]");
        string totalDataStart = txtBox_OrderDateTimeStart.Text.Trim();
        string totalDataEnd = txtBox_OrderDateTimeEnd.Text.Trim();
        string consignmentDateStart = txtBox_ConsignmentDateTimeStart.Text.Trim();
        string consignmentDateEnd = txtBox_ConsignmentDateTimeEnd.Text.Trim();


        if (String.IsNullOrEmpty(keyword))
        {
            condition = "(select country from city where cpccode=so.cpccode)='" + currency + "' and fahuoorder=''";


        }
        else
        {
            string pjtj = "";

            if (fh == "like")
            {
                if (tiaoj == "InceptPerson")
                {
                    pjtj = tiaoj + " like '%" + Encryption.Encryption.GetEncryptionName(keyword) + "%'";
                }
                else if (tiaoj == "InceptAddress")
                {
                    pjtj = tiaoj + " like '%" + Encryption.Encryption.GetEncryptionAddress(keyword) + "%'";
                }
                else
                {
                    pjtj = tiaoj + " like '%" + keyword + "%'";
                }

            }
            else
            {
                if (tiaoj == "TotalMoney" || tiaoj == "Carriage")
                {
                    try
                    {
                        keyword = keyword.Replace(",", "");
                        Convert.ToDouble(keyword);
                    }
                    catch
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('"+GetTran("000443","只能输入数字")+"')</script>");
                        return;
                    }
                }
                pjtj = tiaoj + DropDownList_condition.SelectedItem.Value + "'" + keyword + "'";
            }
            condition = "(select country from city where cpccode=so.cpccode)='" + currency + "' and " + pjtj;
        }

        try
        {
            if (totalDataStart != "")
            {
                Convert.ToDateTime(totalDataStart);
                condition = condition + " and dateadd(hour,"+BLL.other.Company.WordlTimeBLL.ConvertAddHours()+",OrderDateTime)>='" + totalDataStart + " 00:00:00'";
            }
            if (totalDataEnd != "")
            {
                Convert.ToDateTime(totalDataEnd);
                condition = condition + " and dateadd(hour," + BLL.other.Company.WordlTimeBLL.ConvertAddHours() + ",OrderDateTime)<='" + totalDataEnd + " 23:59:59'";
            }

            if (consignmentDateStart != "")
            {
                Convert.ToDateTime(consignmentDateStart);
                condition = condition + " and dateadd(hour," + BLL.other.Company.WordlTimeBLL.ConvertAddHours() + ",ConsignmentDateTime)>='" + consignmentDateStart + " 00:00:00'";
            }
            if (consignmentDateEnd != "")
            {
                Convert.ToDateTime(consignmentDateEnd);
                condition = condition + " and dateadd(hour," + BLL.other.Company.WordlTimeBLL.ConvertAddHours() + ",ConsignmentDateTime)<='" + consignmentDateEnd + " 23:59:59'";
            }
        }
        catch
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('"+GetTran("000450","日期格式错误，请重新输入！")+"')</script>");
            return;
        }

        //首页链接过来的调用这个
        if (type == 1)
        {
            if (Request.QueryString["type"] != null)
            {
                condition = condition + " and isCheckOut='N'";
            }
        }
        condition += " and IsCheckOut='Y' ";
        if (this.DDLSendWay.SelectedValue != "-1")
        {
            condition += " And SendWay=" + DDLSendWay.SelectedValue;
        }

        condition = condition.Replace("〖Country〗", "(select country from city where cpccode=so.cpccode)");
        condition = condition.Replace("〖Province〗", "(select Province from city where cpccode=so.cpccode)");
        condition = condition.Replace("〖City〗", "(select City from city where cpccode=so.cpccode)");


        ViewState["condition"] = condition;

        //Response.Write(condition);

        Pager1.PageBind(0, 10, "ordergoods so  left outer join city on so.cpccode=city.cpccode ", @"StoreID,OrderGoodsID,ExpectNum,IsCheckOut,
                             OrderType,InceptPerson,city.country,city.province,city.city,
                            (city.province+city.city+InceptAddress) as InceptAddress,PostalCode,TotalMoney,TotalPV,Telephone,Weight,Carriage
                            ,OrderDateTime,Description,case SendWay when 0 then '0' else '1' end as SendWay", condition, "OrderGoodsID", "GridView_Order", "OrderDateTime", 0);

        /*
        GridView_Order.DataSource=DBHelper.ExecuteDataTable(@"select feedback,StoreID,StoreOrderID,OutStorageOrderID,ExpectNum,IsCheckOut,IsSent,IsReceived,
                            case OrderType when 1 then N'การ หมุนเวียน สินค้า' when 0 then N'การ หมุนเวียน สินค้า' when 2 
then N'การ หมุนเวียน สินค้า' end as OrderType,InceptPerson,city.country,city.province,city.city,
                            InceptAddress,PostalCode,TotalMoney,TotalPV,Telephone,Weight,Carriage,ConveyanceCompany
                            ,OrderDateTime,ConsignmentDateTime,Description from 
StoreOrder so  left outer join city on so.cpccode=city.cpccode ");
        GridView_Order.DataBind();
        */


    }

    public string GetSendWay(string sendWay)
    {
        if (sendWay == "0")
            return "公司发货到店铺";

        return "公司直接发货给会员";
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        type = 0;

        getbtCon();

        SetFanY();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        string tj = "";
        if (ViewState["condition"] == null)
            tj = "1=1 and fahuoorder=''";
        else
            tj = ViewState["condition"].ToString();

        string sqlcmd = @"select StoreID,OrderGoodsID+' ' as OrderGoodsID,ExpectNum,case IsCheckOut when 'Y' then N'" + GetTran("000233", "是") + @"' when 'N' then N'" + GetTran("000235") + @"' end as 'IsCheckOut',
                        case OrderType when 1 then N'" + GetTran("000391", "周转货") + @"' when 0 then N'" + GetTran("000000", "在线订货") + @"' when 2 then N'" + GetTran("000000", "被动订货") + @"' end as OrderType,InceptPerson,
                        InceptAddress,PostalCode,convert(numeric(15,2),TotalMoney) as TotalMoney,TotalPV,Telephone,Weight,convert(numeric(15,2),Carriage) as Carriage
                        ,OrderDateTime,city.country,city.province,city.city, case so.SendWay when 0 then '0' else '1' end as SendWay
                        from dbo.OrderGoods so left outer join city on so.cpccode=city.cpccode  where " + tj + " order by OrderDateTime desc";

        DataTable dt = DBHelper.ExecuteDataTable(sqlcmd);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dt.Rows[i]["InceptPerson"] = Encryption.Encryption.GetDecipherName(dt.Rows[i]["InceptPerson"].ToString());
            dt.Rows[i]["InceptAddress"] = Encryption.Encryption.GetDecipherAddress(dt.Rows[i]["InceptAddress"].ToString());
            dt.Rows[i]["Telephone"] = Encryption.Encryption.GetDecipherTele(dt.Rows[i]["Telephone"].ToString());

            dt.Rows[i]["OrderDateTime"] = GetBiaoZhunTime(dt.Rows[i]["OrderDateTime"].ToString(),"ty");
            if (dt.Rows[i]["SendWay"].ToString() == "0")
            {
                dt.Rows[i]["SendWay"] = "公司发货到店铺";
            }
            else
            {
                dt.Rows[i]["SendWay"] = "公司直接发货给会员";
            }
        }

        Excel.OutToExcel1(dt, GetTran("000000", "为要货订单浏览"), new string[] { "StoreID="+GetTran("000098","订货店铺"), "OrderGoodsID="+GetTran("000079","订单号"), 
            "ExpectNum="+GetTran("000045","期数"), 
             "OrderType="+GetTran("000106","订单类型"), "InceptPerson="+GetTran("000107","姓名"),"country="+GetTran("000047","国家"),
            "province="+GetTran("000109","省份"),"city="+GetTran("000110","城市"), "InceptAddress="+GetTran("000112","收货地址"), "PostalCode="+GetTran("000073","邮编"), 
            "TotalMoney="+GetTran("000041","总金额"), "TotalPV="+GetTran("000113","总积分"), "Telephone="+GetTran("000115","联系电话"), "Weight="+GetTran("000118","重量"), 

             "OrderDateTime="+GetTran("000067","订货日期"), "SendWay="+GetTran("001345","发货方式") });
    }

    //必须加这个方法，要不然会引发：类型“GridView”的控件“GridView1”必须放在具有 runat=server 的窗体标记内... 异常。
    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }
    protected void GridView_Order_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "xabc=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=xabc");



            int Delete = 0;
            Delete = (int)Permissions.GetPermissions(Model.Other.EnumCompanyPermission.LogisticsBrowseStoreOrdersDelete);
            if (Delete == 0)
            {
                if (((LinkButton)e.Row.FindControl("lButt_Delete")).Visible)
                    ((LinkButton)e.Row.FindControl("lButt_Delete")).Visible = false;
            }
            else
            {
                if (((LinkButton)e.Row.FindControl("lButt_Delete")).Visible)
                    ((LinkButton)e.Row.FindControl("lButt_Delete")).Visible = true;
            }
        }

        if (e.Row.RowType == DataControlRowType.Header)
        {
            SetFanY();
        }
    }

}
