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
using BLL.Logistics;
using Model;
using Model.Other;
using BLL.other.Company;
using System.IO;
using System.Text;
using DAL;
using Standard.Classes;
using System.Collections.Generic;
using BLL;
using BLL.Registration_declarations;

public partial class Company_BillOutOrder : BLL.TranslationBase
{
    private string isButton = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.LogisticsBillOutOrder);

        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));

        if (!IsPostBack)
        {
            AddOrderBLL.BindCurrency_Rate(Dropdownlist2);
            Dropdownlist2.SelectedValue = CountryBLL.GetCurrency();

            ViewState["GridViewID"] = "GridView_BillOutOrder";

            this.DropCurrery.DataSource = CountryBLL.GetCountryModels();
            this.DropCurrery.DataTextField = "name";
            this.DropCurrery.DataValueField = "id";
            this.DropCurrery.DataBind();

            btn_Submit_Click(null, null);

            SetFanY();

            DropDownList_Items_SelectedIndexChanged(null, null);
        }
    }

    public void SetFanY()
    {
        this.DropDownList1.Items[0].Text = GetTran("007705", "未出库");
        this.DropDownList1.Items[1].Text = GetTran("007708", "部分出库");
        this.DropDownList1.Items[2].Text = GetTran("007709", "全部出库");
        this.TranControls(this.btn_Submit, new string[][] { new string[] { "000048", "查 询" } });

        this.TranControls(this.GridView_BillOutOrder,
                            new string[][]{	
                                new string []{"004198","出库"},	
                                new string []{"001017","出库"},													 
                                new string []{"000339","详细"},													 
                                new string []{"000098","订货店铺"},
                                new string []{"000079","订单号"},	
                                new string []{"000024","会员编号"},	
                                  //new string []{"","会员昵称"},	
                                  //new string []{"","会员姓名"},
                                new string []{"000328","是否发货"},
                                new string []{"000045","期数"},
                                new string []{"000106","订单类型"},
                                new string []{"000041","总金额"},
                                new string []{"000113","总积分"},
                                new string []{"000100","付款否"},
                                new string []{"000786","付款日期"},
                                new string []{"000383","收货人姓名"},
                                new string []{"000108","收货人国家"},
                                new string []{"000109","省份"},												 
                                new string []{"000110","城市"},
                                 new string []{"007711","县"},
                                new string []{"000112","收货地址"},
                                new string []{"000646","电话"},
                                new string []{"000118","重量"},
                                new string []{"000120","运费"},
                                new string []{"000067","订货日期"},
                                new string []{"007710","出库日期"},
                                new string []{"000744","查看备注"}

                                });

        this.TranControls(this.GridView2_BilloutOrder,
                            new string[][]{	
                                new string []{"001060","撤单"},													 
                                 new string []{"000339","详细"},														 
                                new string []{"000098","订货店铺"},
                                new string []{"000079","订单号"},	
                                new string []{"000024","会员编号"},	
                                  //new string []{"","会员昵称"},	
                                  //new string []{"","会员姓名"},
                                  //new string []{"000099","对应出库单号"},	
                                new string []{"000045","期数"},
                                new string []{"000106","订单类型"},
                                new string []{"000041","总金额"},
                                new string []{"000113","总积分"},
                                new string []{"000100","付款否"},
                                new string []{"000786","付款日期"},
                                new string []{"000383","收货人姓名"},
                                new string []{"000108","收货人国家"},
                                new string []{"000109","省份"},												 
                                new string []{"000110","城市"},
                                 new string []{"007711","县"},
                                new string []{"000112","收货地址"},
                                new string []{"000646","电话"},
                                new string []{"000118","重量"},
                                new string []{"000120","运费"},
                                new string []{"000067","订货日期"},
                                new string []{"007710","出库日期"},
                                new string []{"000744","查看备注"}

                                });


        this.TranControls(this.DropDownList_Items,
                            new string[][]{	
                                //new string []{"000098","订货店铺"},													 
                                new string []{"000079","订单号"},
                                new string []{"000107","姓名"},
                                new string []{"000108","收货人国家"},
                                new string []{"000109","省份"},												 
                                new string []{"000110","城市"},												 
                                new string []{"000112","收货地址"},
                                //new string []{"000114","邮政编码"},												 
                                new string []{"000115","联系电话"},
                                new string []{"000067","订货日期"},
                                new string []{"000041","总金额"},
                                new string []{"000024","会员编号"},
                                //new string []{"","会员姓名"},
                                //new string []{"","会员昵称"},
                                });

        //this.TranControls(this.DropDownList1,
        //                    new string[][]{	
        //                        new string []{"001007","未生成出库单"},	
        //                         new string []{"000000","部分生成出库单"},						 
        //                        new string []{"001008","已生成出库单（可撤）"}
        //                        });
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

    public string GetOrderType(string str)
    {
        if (str == "1")
            return GetTran("000391", "周转款订货");
        else if (str == "2")
            return GetTran("007900", "被动订货");
        else if (str == "0")
            return GetTran("000231", "在线订货");
        else
            return str;
    }

    public string StringFormat(string sf)
    {
        if (sf == "Y")
            return new TranslationBase().GetTran("000233");
        else
            return new TranslationBase().GetTran("000235");
    }

    public string GetBiaoZhunTime(string dt)
    {
        if (Convert.ToDateTime(dt) < Convert.ToDateTime("1910-01-01 00:00:00"))
            return "——";
        return Convert.ToDateTime(dt).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToShortDateString();
    }

    public string GetBiaoZhunTime(string dt, string ty)
    {
        return Convert.ToDateTime(dt).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToShortDateString();
    }

    /// <summary>
    /// 一般单据查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_Submit_Click(object sender, EventArgs e)
    {
        string condition = "";

        string currency = "";

        if (DropCurrery.SelectedItem != null)
            currency = DropCurrery.SelectedItem.Text;

        string tiaoj = DropDownList_Items.SelectedItem.Value;
        string fh = DropDownList_condition.Text;
        string keyword = "";

        if (DropDownList_Items.SelectedIndex == 7)
            keyword = txtBox_rq.Text.Trim();
        else
            keyword = TextBox4.Text.Trim().Replace("'", "").Replace("〖", "[").Replace("〗", "]");

        string status = DropDownList1.SelectedValue;


        if (String.IsNullOrEmpty(keyword))
        {
            condition = "so.IsCheckOut='Y' and so.isAuditing='Y'  and  (select country from city where cpccode=so.cpccode)='" + currency + "' and " + status;
        }
        else
        {
            string pjtj = "";

            if (fh == "like")
            {
                if (tiaoj == "so.InceptPerson")
                {
                    pjtj = tiaoj + " like '%" + Encryption.Encryption.GetEncryptionName(keyword) + "%'";
                }
                else if (tiaoj == "so.InceptAddress")
                {
                    pjtj = tiaoj + " like '%" + Encryption.Encryption.GetEncryptionAddress(keyword) + "%'";
                }
                else
                    pjtj = tiaoj + " like '%" + keyword + "%'";

            }
            else
            {
                if (tiaoj == "so.TotalMoney" || tiaoj == "so.Carriage")
                {
                    try
                    {
                        keyword = keyword.Replace(",", "");
                        Convert.ToDouble(keyword);
                    }
                    catch
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000443", "只能输入数字") + "')</script>");
                        return;
                    }
                }

                if (DropDownList_Items.SelectedIndex == 7)//用于日期等于
                {
                    if (DropDownList_condition.SelectedItem.Value.Trim() == "=")
                        pjtj = "dateadd(hour," + BLL.other.Company.WordlTimeBLL.ConvertAddHours() + "," + tiaoj + ")" + " >= '" + keyword + " 00:00:00' and " + "dateadd(hour," + BLL.other.Company.WordlTimeBLL.ConvertAddHours() + "," + tiaoj + ")" + " <= '" + keyword + " 23:59:59'";
                    else if (DropDownList_condition.SelectedItem.Value.Trim() == ">")
                        pjtj = "dateadd(hour," + BLL.other.Company.WordlTimeBLL.ConvertAddHours() + "," + tiaoj + ")" + DropDownList_condition.SelectedItem.Value + "'" + keyword + " 23:59:59'";
                    else if (DropDownList_condition.SelectedItem.Value.Trim() == ">=")
                        pjtj = "dateadd(hour," + BLL.other.Company.WordlTimeBLL.ConvertAddHours() + "," + tiaoj + ")" + DropDownList_condition.SelectedItem.Value + "'" + keyword + " 00:00:00'";
                    else if (DropDownList_condition.SelectedItem.Value.Trim() == "<")
                        pjtj = "dateadd(hour," + BLL.other.Company.WordlTimeBLL.ConvertAddHours() + "," + tiaoj + ")" + DropDownList_condition.SelectedItem.Value + "'" + keyword + " 00:00:00'";
                    else if (DropDownList_condition.SelectedItem.Value.Trim() == "<=")
                        pjtj = "dateadd(hour," + BLL.other.Company.WordlTimeBLL.ConvertAddHours() + "," + tiaoj + ")" + DropDownList_condition.SelectedItem.Value + "'" + keyword + " 23:59:59'";

                }
                else
                    pjtj = tiaoj + DropDownList_condition.SelectedItem.Value + "'" + keyword + "'";
            }

            condition = "so.IsCheckOut='Y' and so.isAuditing='Y' and  (select country from city where cpccode=so.cpccode)='" + currency + "' and " + pjtj + " and " + status;

        }

        condition = condition.Replace("〖Country〗", "(select country from city where cpccode=so.cpccode)");
        condition = condition.Replace("〖Province〗", "(select Province from city where cpccode=so.cpccode)");
        condition = condition.Replace("〖City〗", "(select City from city where cpccode=so.cpccode)");

        ViewState["condition"] = condition;

        if (DropDownList1.SelectedValue == "so.isGeneOutBill='A'")
        {
            ViewState["GridViewID"] = "GridView2_BillOutOrder";

            GridView_BillOutOrder.Visible = false;
            GridView2_BilloutOrder.Visible = true;

            Pager1.PageBind(0, 10, @"StoreOrder so  left outer join city on so.cpccode=city.cpccode left join memberorder m on so.storeorderid=m.orderid",
                            @"so.isSent,so.StoreID,so.StoreOrderID,so.ExpectNum, so.OrderType,m.number ,
                            so.TotalMoney,so.TotalPv,so.AuditingDate,so.IsCheckOut,so.PayMentDateTime,so.InceptPerson,so.InceptAddress,so.Telephone,so.Weight,so.Carriage,so.OrderDateTime,city.country,city.province,city.city,city.Xian,
                            so.Description ", condition, "so.StoreOrderID", "GridView2_BilloutOrder", "so.OrderDateTime", 0);

            Panel1.Visible = false;

            ViewState["SQLSTR"] = @"select so.isSent,so.StoreID,so.StoreOrderID,so.ExpectNum, so.OrderType,m.number ,
                            so.TotalMoney,so.TotalPv,so.AuditingDate,so.IsCheckOut,so.PayMentDateTime,so.InceptPerson,so.InceptAddress,so.Telephone,so.Weight,so.Carriage,so.OrderDateTime,city.country,city.province,city.city,city.Xian,
                            so.Description  from StoreOrder so  left outer join city on so.cpccode=city.cpccode left join memberorder m on so.storeorderid=m.orderid  
                                where " + condition + "  order by so.OrderDateTime desc,so.StoreOrderID desc";
        }
        else
        {
            ViewState["GridViewID"] = "GridView_BillOutOrder";

            GridView2_BilloutOrder.Visible = false;
            GridView_BillOutOrder.Visible = true;

            Pager1.PageBind(0, 10, @"StoreOrder so  left outer join city on so.cpccode=city.cpccode left join memberorder m on so.storeorderid=m.orderid",
                        @"m.number,so.isSent,so.StoreID,so.StoreOrderID,so.ExpectNum, so.OrderType,
                            so.TotalMoney,so.TotalPv,so.AuditingDate,so.IsCheckOut,so.PayMentDateTime,so.InceptPerson,so.InceptAddress,so.Telephone,so.Weight,so.Carriage,so.OrderDateTime,city.country,city.province,city.city,city.Xian,
                            so.Description ", condition, "so.StoreOrderID", "GridView_BillOutOrder", "so.OrderDateTime", 0);

            Panel1.Visible = true;

            ViewState["SQLSTR"] = @"select m.number,so.isSent,so.StoreID,so.StoreOrderID,so.ExpectNum, so.OrderType,
                            so.TotalMoney,so.TotalPv,so.AuditingDate,so.IsCheckOut,so.PayMentDateTime,so.InceptPerson,so.InceptAddress,so.Telephone,so.Weight,so.Carriage,so.OrderDateTime,city.country,city.province,city.city,city.Xian,
                            so.Description  from StoreOrder so  left outer join city on so.cpccode=city.cpccode left join memberorder m on so.storeorderid=m.orderid  
                                where " + condition + "  order by so.OrderDateTime desc,so.StoreOrderID desc";
        }

        SetFanY();

    }

    public string GetSentType(string isSent)
    {
        if (isSent == "N")
        {
            return "<font color='red'>未发货</font>";
        }
        else
        {
            return "已发货";
        }
    }

    public bool GetFF(string str)
    {
        if (str == "A")
            return false;
        return true;
    }

    /// <summary>
    /// 条件下拉框改变事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DropDownList_Items_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.DropDownList_Items.SelectedIndex < 7)
        { //副条件为包含
            this.DropDownList_condition.Items.Clear();
            this.DropDownList_condition.Items.Add(new ListItem(GetTran("000378", "包含"), "like"));

            TextBox4.Visible = true;
            txtBox_rq.Visible = false;
        }
        else if (this.DropDownList_Items.SelectedIndex == 7)
        { //副条件为包含
            this.DropDownList_condition.Items.Clear();
            this.DropDownList_condition.Items.Add(new ListItem(GetTran("000361", "大于"), " > "));
            this.DropDownList_condition.Items.Add(new ListItem(GetTran("000364", "大于等于"), " >= "));
            this.DropDownList_condition.Items.Add(new ListItem(GetTran("000367", "小于"), "  < "));
            this.DropDownList_condition.Items.Add(new ListItem(GetTran("000368", "小于等于"), "  <= "));
            this.DropDownList_condition.Items.Add(new ListItem(GetTran("000372", "等于"), "  = "));

            TextBox4.Visible = false;
            txtBox_rq.Visible = true;

        }
        else if (this.DropDownList_Items.SelectedIndex == 8)
        {//条件为范围查找
            this.DropDownList_condition.Items.Clear();
            this.DropDownList_condition.Items.Add(new ListItem(GetTran("000361", "大于"), " > "));
            this.DropDownList_condition.Items.Add(new ListItem(GetTran("000364", "大于等于"), " >= "));
            this.DropDownList_condition.Items.Add(new ListItem(GetTran("000367", "小于"), "  < "));
            this.DropDownList_condition.Items.Add(new ListItem(GetTran("000368", "小于等于"), "  <= "));
            this.DropDownList_condition.Items.Add(new ListItem(GetTran("000372", "等于"), "  = "));

            TextBox4.Visible = true;
            txtBox_rq.Visible = false;


        }
        else
        {
            this.DropDownList_condition.Items.Clear();
            this.DropDownList_condition.Items.Add(new ListItem(GetTran("000378", "包含"), "like"));

            TextBox4.Visible = true;
            txtBox_rq.Visible = false;
        }
    }

    /// <summary>
    /// 出库按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        isButton = "出库";
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string tj = "";

        if (ViewState["condition"] == null)
            tj = "IsCheckOut='Y' and isAuditing='Y' and IsProvOut=1 and mo.Ordertype!=3 and IsGeneOutBill='N' ";
        else
            tj = ViewState["condition"].ToString();

        string sqlcmd = "";

        DataTable dt = null;

        System.Text.StringBuilder sbexcel = new StringBuilder();


        if (ViewState["GridViewID"].ToString() == "GridView_BillOutOrder") //未生成出库单的gridview
        {
            sqlcmd = ViewState["SQLSTR"].ToString();

            dt = DBHelper.ExecuteDataTable(sqlcmd);

            dt.Columns.Add("OrderType_str", typeof(System.String));
            dt.Columns.Add("OrderDateTime_str", typeof(System.String));
            dt.Columns.Add("AuditingDate_str", typeof(System.String));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["OrderType_str"] = GetOrderType(dt.Rows[i]["OrderType"].ToString());

                dt.Rows[i]["StoreOrderID"] = "&nbsp;" + dt.Rows[i]["StoreOrderID"];
                dt.Rows[i]["InceptPerson"] = Encryption.Encryption.GetDecipherName(dt.Rows[i]["InceptPerson"].ToString());
                dt.Rows[i]["InceptAddress"] = Encryption.Encryption.GetDecipherAddress(dt.Rows[i]["InceptAddress"].ToString());
                dt.Rows[i]["Telephone"] = Encryption.Encryption.GetDecipherTele(dt.Rows[i]["Telephone"].ToString());

                dt.Rows[i]["OrderDateTime_str"] = GetBiaoZhunTime(dt.Rows[i]["OrderDateTime"].ToString());
                dt.Rows[i]["AuditingDate_str"] = GetBiaoZhunTime(dt.Rows[i]["AuditingDate"].ToString());
            }

            sbexcel = Excel.GetExcelTable(dt, GetTran("001187", "订单出库表"), new string[] {  
                "StoreID=订货服务中心", "StoreOrderID="+GetTran("000079","订单号"),"Number=会员编号",
                "ExpectNum="+GetTran("000045","期数"), "OrderType_str="+GetTran("000106","订单类型"), "TotalMoney="+GetTran("000041","总金额"), 
                "TotalPv="+GetTran("000113","总积分"), 
                  "InceptPerson="+GetTran("000383", "收货人姓名"),"country="+GetTran("000047","国家"),
                  "province="+GetTran("000109","省份"),"city="+GetTran("000110","城市"),"Xian=县", "InceptAddress="+GetTran("000112","收货地址"), 
                  "Telephone="+GetTran("000646","电话"), "Weight="+GetTran("000118","重量"),  
                  "OrderDateTime_str="+GetTran("000067","订货日期"),"AuditingDate_str=出库日期","Description=备注" });
        }
        else
        {
            sqlcmd = ViewState["SQLSTR"].ToString();

            dt = DBHelper.ExecuteDataTable(sqlcmd);

            dt.Columns.Add("OrderType_str", typeof(System.String));
            dt.Columns.Add("OrderDateTime_str", typeof(System.String));
            dt.Columns.Add("AuditingDate_str", typeof(System.String));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["OrderType_str"] = GetOrderType(dt.Rows[i]["OrderType"].ToString());

                dt.Rows[i]["StoreOrderID"] = "&nbsp;" + dt.Rows[i]["StoreOrderID"];
                dt.Rows[i]["InceptPerson"] = Encryption.Encryption.GetDecipherName(dt.Rows[i]["InceptPerson"].ToString());
                dt.Rows[i]["InceptAddress"] = Encryption.Encryption.GetDecipherAddress(dt.Rows[i]["InceptAddress"].ToString());
                dt.Rows[i]["Telephone"] = Encryption.Encryption.GetDecipherTele(dt.Rows[i]["Telephone"].ToString());

                dt.Rows[i]["OrderDateTime_str"] = GetBiaoZhunTime(dt.Rows[i]["OrderDateTime"].ToString());
                dt.Rows[i]["AuditingDate_str"] = GetBiaoZhunTime(dt.Rows[i]["AuditingDate"].ToString());
            }


            sbexcel = Excel.GetExcelTable(dt, GetTran("001187", "订单出库表"), new string[] {  
                "StoreID=订货服务中心", "StoreOrderID="+GetTran("000079","订单号"),"Number=会员编号",
                "ExpectNum="+GetTran("000045","期数"), "OrderType_str="+GetTran("000106","订单类型"), "TotalMoney="+GetTran("000041","总金额"), 
                "TotalPv="+GetTran("000113","总积分"), 
                  "InceptPerson="+GetTran("000383", "收货人姓名"),"country="+GetTran("000047","国家"),
                  "province="+GetTran("000109","省份"),"city="+GetTran("000110","城市"),"Xian=县", "InceptAddress="+GetTran("000112","收货地址"), 
                  "Telephone="+GetTran("000646","电话"), "Weight="+GetTran("000118","重量"),  
                  "OrderDateTime_str="+GetTran("000067","订货日期"),"AuditingDate_str=出库日期","Description=备注" });

        }

        Response.AppendHeader("Content-Disposition", "attachment;filename=FileName.xls");
        Response.ContentEncoding = Encoding.GetEncoding("utf-8");

        Response.Write(sbexcel.ToString());
        Response.Flush();
        Response.End();
    }

    //必须加这个方法，要不然会引发：类型“GridView”的控件“GridView1”必须放在具有 runat=server 的窗体标记内... 异常。
    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }

    public string GetRemark(string remark, string storeOrderID)
    {
        if (String.IsNullOrEmpty(remark))
        {
            return new TranslationBase().GetTran("000221", "无");
        }
        else
        {
            return "<a href='ShowStoreOrderNote.aspx?storeOrderID=" + storeOrderID + "'>" + GetTran("000440", "查看") + "</a>";
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (isButton == "出库")
        {

            if (DropCurrery.SelectedItem != null)
            {
                string currency = DropCurrery.SelectedItem.Value;

                string storeOrderID = ((Label)GridView_BillOutOrder.SelectedRow.FindControl("Lab_StoreOrderID")).Text;

                Response.Redirect("Outstock.aspx?StoreOrderID=" + storeOrderID + "&title=" + GetTran("001164", "请填写出库单") + "&enable=N&isBut=CK&source=ck&Country=" + currency);
            }
            else
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000058", "请选择国家") + "');</script>");

        }
        else if (isButton == "查看")
        {
            string storeOrderID = ((Label)GridView_BillOutOrder.SelectedRow.FindControl("Lab_StoreOrderID")).Text;
            Response.Redirect("ViewStoreOrderD.aspx?storeOrderID=" + storeOrderID);
        }
        else if (isButton == "s查看")
        {
            string storeOrderID = ((Label)GridView2_BilloutOrder.SelectedRow.FindControl("sLab_StoreOrderID")).Text;
            Response.Redirect("ViewStoreOrder.aspx?storeOrderID=" + storeOrderID);
        }
        else if (isButton == "撤单")
        {
            string storeorderid = ((Label)GridView2_BilloutOrder.SelectedRow.FindControl("sLab_StoreOrderID")).Text;

            DataTable dt = DBHelper.ExecuteDataTable("select docid from InventoryDoc where StoreOrderID='" + storeorderid + "'");
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                List<InventoryDocDetailsModel> l_ddm = new List<InventoryDocDetailsModel>();
                DataTable l_odm = InventoryDocDetailsBLL.GetInventoryDocDetailsByDocID(dr[0].ToString()); //BillOutOrderBLL.GetOrderDetailModelList(dr[0].ToString());
                foreach (DataRow row in l_odm.Rows)
                {
                    InventoryDocDetailsModel ddm = new InventoryDocDetailsModel();

                    ddm.ProductID = Convert.ToInt32(row["ProductId"]);
                    ddm.ProductQuantity = Convert.ToDouble(row["ProductQuantity"]);
                    ddm.UnitPrice = Convert.ToDouble(row["UnitPrice"]);
                    ddm.PV = Convert.ToDouble(row["PV"]);
                    ddm.ProductTotal = Convert.ToDouble(row["totalPrice"]);
                    ddm.MeasureUnit = "";

                    l_ddm.Add(ddm);
                }
                if (BillOutOrderBLL.SetQuashBillOutOrder(dr[0].ToString(), l_ddm, storeorderid))
                {
                    i++;
                }
            }

            if (i > 0)
            {
                GridView_BillOutOrder.Visible = false;
                GridView2_BilloutOrder.Visible = true;


                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("001168", "撤单成功！") + "');</script>");

                Pager1.PageBind(0, 10, " StoreOrder so  left outer join city on so.cpccode=city.cpccode ", @" isSent,StoreID,StoreOrderID,OutStorageOrderID,ExpectNum, OrderType,
                            TotalMoney,TotalPv,IsCheckOut,PayMentDateTime,InceptPerson,InceptAddress,Telephone,Weight,Carriage,OrderDateTime,city.country,city.province,city.city,
                            Description ", ViewState["condition"].ToString(), "StoreOrderID", "GridView2_BilloutOrder", "OrderDateTime", 0);
            }
            else
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("001171", "撤单失败！") + "');</script>");

        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        isButton = "查看";
    }
    protected void sLinkButton1_Click(object sender, EventArgs e)
    {
        isButton = "s查看";//撤单中的查看
    }
    protected void sLinkButton2_Click(object sender, EventArgs e)
    {
        isButton = "撤单";
    }
    protected void GridView_BillOutOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "xabc=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=xabc");
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            SetFanY();
        }
    }
    protected void GridView2_BilloutOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "xabc=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=xabc");
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            SetFanY();
        }
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (DropCurrery.SelectedItem != null)
        {
            string storeOrderID = "";
            string currency = DropCurrery.SelectedItem.Value;

            for (int i = 0; i < GridView_BillOutOrder.Rows.Count; i++)
            {
                if (((CheckBox)GridView_BillOutOrder.Rows[i].FindControl("CheckBox1")).Checked)
                {
                    storeOrderID = storeOrderID + ((Label)GridView_BillOutOrder.Rows[i].FindControl("Lab_StoreOrderID")).Text + ",";
                }
            }

            if (storeOrderID == "")
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请勾选要出库的单！');</script>");
            else
                Response.Redirect("Outstock.aspx?StoreOrderID=" + storeOrderID + "&title=" + GetTran("001164", "请填写出库单") + "&enable=N&isBut=CK&source=ck&Country=" + currency);
        }
        else
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000058", "请选择国家") + "');</script>");
    }
}