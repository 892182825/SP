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
using Model.Other;
using Model;
using DAL;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Standard.Classes;
using BLL.other.Company;
using BLL.Registration_declarations;

public partial class Company_CompanyConsign : BLL.TranslationBase
{
    private string isButton = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.LogisticsCompanyConsign);

        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));

        if (!IsPostBack)
        {
            AddOrderBLL.BindCurrency_Rate(Dropdownlist4);
            Dropdownlist4.SelectedValue = CountryBLL.GetCurrency();

            Session["GridViewID"] = "GridView1_CompanyConsign";

            this.DropCurrency.DataSource = CountryBLL.GetCountryModels();
            this.DropCurrency.DataTextField = "name";
            this.DropCurrency.DataValueField = "id";
            this.DropCurrency.DataBind();


            btn_Submit_Click(null, null);

            SetFanY();
            DropDownList_Items_SelectedIndexChanged(null, null);
        }
    }

    public void SetFanY()
    {
        this.TranControls(this.btn_Submit, new string[][] { new string[] { "000048", "查 询" } });
        this.TranControls(this.btn_MoreSent, new string[][] { new string[] { "001486", "多选发货" } });

        this.TranControls(this.GridView1_CompanyConsign,
                            new string[][]{	
                                new string []{"001336","多选"},													 
                                new string []{"001340","发货"},		
                                new string []{"000339","详细"},											 
                                new string []{"000024","订货店铺"},
                                new string []{"002158","出库单号"},
                                 new string []{"001867","订单号"},	
                                 new string []{"000024","会员编号"},	
                                  //new string []{"","会员昵称"},	
                                  //new string []{"","会员姓名"},
                                new string []{"000067","订货日期"},
                                new string []{"000045","期数"},
                                new string []{"000786","付款日期"},
                                new string []{"000106","订单类型"},
                                new string []{"000041","总金额"},
                                new string []{"000113","总积分"},
                                new string []{"000118","重量"},
                                new string []{"000120","运费"},
                                new string []{"000383","收货人姓名"},
                                new string []{"000108","收货人国家"},
                                new string []{"000109","省份"},												 
                                new string []{"000110","城市"},
                                new string []{"007711","县"},
                                new string []{"000112","收货地址"},
                                new string []{"000073","邮编"},
                                new string []{"000115","联系电话"},
                                new string []{"001345", "发货方式"},
                                new string []{"000386", "仓库"},
                                new string []{"000390", "库位"}

                                });

        this.TranControls(this.GridView2_CompanyConsign,
                            new string[][]{				
								new string []{"000339","详细"},				 
                                new string []{"000024","订货店铺"},
                                
                                  new string []{"000079","订单号"},
                                new string []{"002158","出库单号"},	
                                new string []{"000024","会员编号"},	
                                  //new string []{"","会员昵称"},	
                                  //new string []{"","会员姓名"},
                                new string []{"000067","订货日期"},
                                new string []{"000045","期数"},
                                new string []{"000786","付款日期"},
                                new string []{"000106","订单类型"},
                                new string []{"000041","总金额"},
                                new string []{"000113","总积分"},
                                new string []{"000118","重量"},
                                new string []{"000120","运费"},
                                new string []{"",""},
                                new string []{"000383","收货人姓名"},
                                new string []{"000108","收货人国家"},
                                new string []{"000109","省份"},												 
                                new string []{"000110","城市"},
                                new string []{"007711","县"},
                                new string []{"000112","收货地址"},
                                new string []{"000073","邮编"},
                                new string []{"000115","联系电话"},
                                new string []{"001345", "发货方式"},
                                new string []{"000121", "物流公司"},
                                new string []{"000386", "仓库"},
                                new string []{"000390", "库位"}

                                        });


        this.TranControls(this.DropDownList1,
                            new string[][]{	
												 
                                new string []{"000079","订单号"},

                                new string []{"000000","出库单号"},
                                new string []{"000107","姓名"},
                                new string []{"000108","收货人国家"},
                                new string []{"000109","省份"},												 
                                new string []{"000110","城市"},												 
                                new string []{"000112","收货地址"},
                                new string []{"000114","邮政编码"},												 
                                new string []{"000115","联系电话"},
                                 new string []{"000041","总金额"},
                                new string []{"000067","订货日期"},
                                new string []{"000024","会员编号"},
                                //new string []{"","会员姓名"},
                                //new string []{"","会员昵称"},
                                });

        this.TranControls(this.DropDownList_Items,
                            new string[][]{	
                                new string []{"001370","未发货"},													 
                                new string []{"001371","已发货"}
                                });
    }

    public string SetDocIDsFormat(string str)
    {
        if (str.EndsWith(","))
        {
            str = str.Substring(0, str.Length - 1);

            str = str.Replace(",", "<br/>");

            return str;
        }
        return str;
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

    public string GetBiaoZhunTime(string dt, string ty)
    {
        return Convert.ToDateTime(dt).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToShortDateString();
    }

    /// <summary>
    /// 条件下拉框改变事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DropDownList_Items_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.DropDownList1.SelectedIndex <= 8)
        { //副条件为包含
            this.DropDownList_condition.Items.Clear();
            this.DropDownList_condition.Items.Add(new ListItem(GetTran("000378", "包含"), "like"));

            TextBox4.Visible = true;
            txtBox_rq.Visible = false;
        }
        else if (this.DropDownList1.SelectedIndex == 10)
        {//条件为范围查找
            this.DropDownList_condition.Items.Clear();
            this.DropDownList_condition.Items.Add(new ListItem(GetTran("000361", "大于"), " > "));
            this.DropDownList_condition.Items.Add(new ListItem(GetTran("000364", "大于等于"), " >= "));
            this.DropDownList_condition.Items.Add(new ListItem(GetTran("000367", "小于"), "  < "));
            this.DropDownList_condition.Items.Add(new ListItem(GetTran("000368", "小于等于"), "  <= "));
            this.DropDownList_condition.Items.Add(new ListItem(GetTran("000372", "等于"), "  = "));

            TextBox4.Visible = false;
            txtBox_rq.Visible = true;


        }
        else if (this.DropDownList1.SelectedIndex == 9)
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
    /// 选中合单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button4_Click(object sender, EventArgs e)
    {

    }

    public string getFh(string str)
    {
        if (str == "")
            return GetTran("000461", "立即发货");
        return str;
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_Submit_Click(object sender, EventArgs e)
    {
        string currency = "";

        if (DropCurrency.SelectedItem != null)
            currency = DropCurrency.SelectedItem.Text;

        string tiaoj = DropDownList1.SelectedItem.Value;
        string fh = DropDownList_condition.Text;
        string keyword = "";

        if (DropDownList1.SelectedIndex == 10)
            keyword = txtBox_rq.Text.Trim();
        else
            keyword = TextBox4.Text.Trim().Replace("'", "").Replace("〖", "[").Replace("〗", "]");

        string isSent = DropDownList_Items.Text;

        string condition = "";

        if (String.IsNullOrEmpty(keyword))
        {
            condition = "so.IsCheckOut='Y' and so.isAuditing='Y' and  (select country from city where cpccode=so.cpccode)='" + currency + "' and " + isSent;
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
                else if (tiaoj == "ity.Address")
                {
                    pjtj = tiaoj + " like '%" + Encryption.Encryption.GetEncryptionAddress(keyword) + "%'";
                }
                else
                    pjtj = tiaoj + " like '%" + keyword + "%'";

            }
            else
            {
                if (tiaoj == "ity.TotalMoney" || tiaoj == "Carriage")
                {
                    try
                    {
                        keyword = keyword.Replace(",", "");
                        Convert.ToDouble(keyword);
                        if (keyword.Length > 8)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("006912", "输入金额太大！") + " ')</script>");
                            return;
                        }
                    }
                    catch
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000443") + "')</script>");
                        return;
                    }
                }

                if (DropDownList1.SelectedIndex == 10)//用于日期等于
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

            condition = "so.IsCheckOut='Y' and so.isAuditing='Y' and (select country from city where cpccode=so.cpccode)='" + currency + "' and " + pjtj + " and " + isSent;

        }

        condition = condition.Replace("〖Country〗", "(select country from city where cpccode=so.cpccode)");
        condition = condition.Replace("〖Province〗", "(select Province from city where cpccode=so.cpccode)");
        condition = condition.Replace("〖City〗", "(select City from city where cpccode=so.cpccode)");

        //2009-11-13
        ViewState["condition"] = condition;

        if (DropDownList_Items.SelectedItem.Value.Trim() == "ity.IsSend=0")
        {
            Session["GridViewID"] = "GridView1_CompanyConsign";

            GridView1_CompanyConsign.Visible = true;
            GridView2_CompanyConsign.Visible = false;

            btn_Select.Enabled = true;
            Panel1.Visible = true;

            Pager1.PageBind(0, 10, @"  InventoryDoc ity
                            left outer join dbo.StoreOrder so on so.StoreOrderid=ity.StoreOrderid
                            left outer join WareHouse wh on wh.WareHouseID=ity.inWareHouseID left outer join DepotSeat ds
                            on ds.DepotSeatID=ity.inDepotSeatID and ity.inWareHouseID=ds.WareHouseID left outer join city on so.cpccode=city.cpccode 
                                    left outer join MemberOrder mo on so.storeorderid=mo.OrderId
                                    left outer join MemberInfo mi on mo.Number=mi.Number",
                            @"mi.Number,mi.Name,mi.Name as PetName,so.StoreID,so.StoreOrderID,ity.docid,so.IsAuditing,so.OrderDateTime,so.ExpectNum,so.PayMentDateTime,city.country,city.province,city.city,city.Xian,
                            so.IsCheckOut,so.IsSent,so.OrderType,
                            ity.TotalMoney,ity.TotalPV,ity.client,so.Weight,so.Carriage,so.ForeCastArriveDateTime,so.InceptPerson,
                            ity.Address,so.PostalCode,so.Telephone,so.ConveyanceMode,so.ConveyanceCompany,
                            wh.WareHouseName,ds.SeatName", condition, "ity.docid", "GridView1_CompanyConsign", "ity.docmaketime", 0);

            ViewState["SQLSTR"] = @"select mi.Number,mi.Name,mi.Name as PetName,so.StoreID,so.StoreOrderID,ity.docid,so.IsAuditing,so.OrderDateTime,so.ExpectNum,so.PayMentDateTime,city.country,city.province,city.city,city.Xian,
                            so.IsCheckOut,so.IsSent,so.OrderType,
                            ity.TotalMoney,ity.TotalPV,ity.client,so.Weight,so.Carriage,so.ForeCastArriveDateTime,so.InceptPerson,
                            ity.Address,so.PostalCode,so.Telephone,so.ConveyanceMode,so.ConveyanceCompany,
                            wh.WareHouseName,ds.SeatName  from InventoryDoc ity
                            left outer join dbo.StoreOrder so on so.StoreOrderid=ity.StoreOrderid
                            left outer join WareHouse wh on wh.WareHouseID=ity.inWareHouseID left outer join DepotSeat ds
                            on ds.DepotSeatID=ity.inDepotSeatID and ity.inWareHouseID=ds.WareHouseID left outer join city on so.cpccode=city.cpccode 
                                    left outer join MemberOrder mo on so.storeorderid=mo.OrderId
                                    left outer join MemberInfo mi on mo.Number=mi.Number  where " + condition + "  order by ity.docmaketime desc,ity.docid desc";
        }
        else
        {
            Session["GridViewID"] = "GridView2_CompanyConsign";

            GridView1_CompanyConsign.Visible = false;
            GridView2_CompanyConsign.Visible = true;

            btn_Select.Enabled = false;
            Panel1.Visible = false;

            Pager1.PageBind(0, 10, @" InventoryDoc ity
                            left outer join StoreOrder so on so.StoreOrderid=ity.StoreOrderid
                            left outer join WareHouse wh on wh.WareHouseID=ity.inWareHouseID left outer join DepotSeat ds
                            on ds.DepotSeatID=ity.inDepotSeatID and ity.inWareHouseID=ds.WareHouseID left outer join city on so.cpccode=city.cpccode 
                                   
                                    left outer join MemberOrder mo on so.storeorderid=mo.OrderId
                                    left outer join MemberInfo mi on mo.Number=mi.Number",
                            @"mi.Number,mi.Name,mi.Name as PetName,so.StoreID,so.StoreOrderID,ity.client,ity.docid,so.IsAuditing,so.OrderDateTime,so.ExpectNum,so.PayMentDateTime,city.country,city.province,city.city,city.Xian,
                            so.IsCheckOut,so.IsSent,so.OrderType,
                            ity.TotalMoney,ity.TotalPV,so.Weight,so.Carriage,so.ForeCastArriveDateTime,so.InceptPerson,
                            ity.Address,so.PostalCode,so.Telephone,so.ConveyanceMode,so.ConveyanceCompany,
                            wh.WareHouseName,ds.SeatName", condition, "ity.docid", "GridView2_CompanyConsign", "ity.docmaketime", 0);

            ViewState["SQLSTR"] = @"select mi.Number,mi.Name,mi.Name as PetName,so.StoreID,so.StoreOrderID,ity.client,ity.docid,so.IsAuditing,so.OrderDateTime,so.ExpectNum,so.PayMentDateTime,city.country,city.province,city.city,city.Xian,
                            so.IsCheckOut,so.IsSent,so.OrderType,
                            ity.TotalMoney,ity.TotalPV,so.Weight,so.Carriage,so.ForeCastArriveDateTime,so.InceptPerson,
                            ity.Address,so.PostalCode,so.Telephone,so.ConveyanceMode,so.ConveyanceCompany,
                            wh.WareHouseName,ds.SeatName  from InventoryDoc ity
                            left outer join StoreOrder so on so.StoreOrderid=ity.StoreOrderid
                            left outer join WareHouse wh on wh.WareHouseID=ity.inWareHouseID left outer join DepotSeat ds
                            on ds.DepotSeatID=ity.inDepotSeatID and ity.inWareHouseID=ds.WareHouseID left outer join city on so.cpccode=city.cpccode 
                                   
                                    left outer join MemberOrder mo on so.storeorderid=mo.OrderId
                                    left outer join MemberInfo mi on mo.Number=mi.Number  where " + condition + "  order by ity.docmaketime desc,ity.docid desc";
        }

        SetFanY();
    }

    /// <summary>
    /// 多选发货
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button5_Click(object sender, EventArgs e)
    {
        List<StoreOrderModel> lsom = new List<StoreOrderModel>();

        int j = 0;
        string docids = "";
        for (int i = 0; i < this.GridView1_CompanyConsign.Rows.Count; i++)
        {
            if ((this.GridView1_CompanyConsign.Rows[i].FindControl("CkBox_More") as CheckBox).Checked)
            {
                docids += "'" + ((Label)GridView1_CompanyConsign.Rows[i].FindControl("Lab_OutStorageOrderID")).Text + "',";

                j++;
            }
        }
        if (j == 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("001494", "请选中复选框") + "')</script>");
            return;
        }
        else
        {
            docids = docids.Substring(0, docids.Length - 1);
            string sql = "update InventoryDoc set issend=1 where docid in (" + docids + ")";
            DBHelper.ExecuteNonQuery("update storeorder set issent='Y',ConsignmentDateTime='" + DateTime.UtcNow + "' from InventoryDoc where storeorder.storeorderid=InventoryDoc.storeorderid and InventoryDoc.docid in (" + docids + ")");
            if (DBHelper.ExecuteNonQuery(sql) > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("001517", "批量发货成功！") + "')</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("007424", "批量发货失败！") + "')</script>");
            }
        }
        btn_Submit_Click(null, null);
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

    /// <summary>
    /// 合单多选发货
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button6_Click(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// 合单查询按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click1(object sender, EventArgs e)
    {

    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (isButton == GetTran("001340", "发货"))
        {
            string storeOrderID = ((Label)GridView1_CompanyConsign.SelectedRow.FindControl("Lab_OutStorageOrderID")).Text;

            string telephone = ((Label)GridView1_CompanyConsign.SelectedRow.FindControl("Lab_Telephone")).Text;
            string storeID = ((Label)GridView1_CompanyConsign.SelectedRow.FindControl("Lab_StoreID")).Text;
            string inceptPerson = ((Label)GridView1_CompanyConsign.SelectedRow.FindControl("Lab_InceptPerson")).Text;


            ViewState["storeOrderID"] = storeOrderID;

            string weight = ((Label)GridView1_CompanyConsign.SelectedRow.FindControl("Lab_Weight")).Text;

            List<StoreOrderModel> lsom = new List<StoreOrderModel>();

            StoreOrderModel som = new StoreOrderModel();
            som.StoreorderId = storeOrderID;
            som.Weight = Convert.ToDecimal(weight);
            som.OutStorageOrderID = ((Label)GridView1_CompanyConsign.SelectedRow.FindControl("Lab_OutStorageOrderID")).Text;

            lsom.Add(som);

            //不出库也可以发货
            Response.Redirect("FAhuo.aspx?StoreOrderID=" + storeOrderID + "&Telephone=" + telephone + "&StoreID=" + storeID + "&InceptPerson=" + inceptPerson);

        }
        else if (isButton == GetTran("000339", "详细"))
        {
            string storeOrderID = ((Label)GridView1_CompanyConsign.SelectedRow.FindControl("Lab_OutStorageOrderID")).Text;

            Response.Redirect("ShowBillDetailsB.aspx?DocID=" + storeOrderID);
        }
    }

    public void SetUniteConsignment(List<string> docIDs)
    {
        List<StoreOrderModel> lsom = new List<StoreOrderModel>();

        for (int i = 0; i < docIDs.Count; i++)
        {
            StoreOrderModel som = CompanyConsignBLL.GetStoreOrder(docIDs[i]);

            lsom.Add(som);
        }

        SetConsignment(lsom, "more");
    }


    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        isButton = GetTran("001340", "发货");
    }

    public void SetConsignment(List<StoreOrderModel> lsom, string mode)
    {
        string isSucceed = CompanyConsignBLL.Consign(lsom);  //

        string _OutStorageOrderID = ((StoreOrderModel)lsom[0]).OutStorageOrderID;

        if (isSucceed == "Y")
        {
            if (mode == "one")
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>if(window.confirm('" + GetTran("001515", "发货成功，您要打印此单据吗？") + "')) window.open('docPrint.aspx?docID=" + _OutStorageOrderID + "');window.location.href='CompanyConsign.aspx';</script>");
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("001517", "批量发货成功！") + "')</script>");

                btn_Submit_Click(null, null);
            }

        }
        else
        {
            if (mode == "one")
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>if(confirm('订单号：" + isSucceed + " ，  没有生成出库单,确定出库？')) location.href='Outstock.aspx?StoreOrderID=" + isSucceed + "&title=请填写出库单：&enable=N&isBut=CK&source=fh'</script>");
            else
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("001518", "订单：") + "" + isSucceed + " " + GetTran("001520", ",未生成出库单！") + "')</script>");
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        DataTable dt = DBHelper.ExecuteDataTable(ViewState["SQLSTR"].ToString());

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dt.Rows[i]["StoreOrderID"] = "&nbsp;" + dt.Rows[i]["StoreOrderID"];
            dt.Rows[i]["InceptPerson"] = Encryption.Encryption.GetDecipherName(dt.Rows[i]["InceptPerson"].ToString());
            dt.Rows[i]["Address"] = Encryption.Encryption.GetDecipherAddress(dt.Rows[i]["Address"].ToString());
            dt.Rows[i]["Telephone"] = Encryption.Encryption.GetDecipherTele(dt.Rows[i]["Telephone"].ToString());

            dt.Rows[i]["OrderDateTime"] = GetBiaoZhunTime(dt.Rows[i]["OrderDateTime"].ToString(), "ty");
        }

        StringBuilder sb = null;
        if (DropDownList_Items.SelectedValue == "so.IsSent='N' ")
        {
            sb = Excel.GetExcelTable(dt, GetTran("001523", "订单发货表"), new string[] { 
                "StoreOrderID="+GetTran("000079","订单号"), "docid="+GetTran("000099","对应出库单号"),"Number=会员编号", "OrderDateTime="+GetTran("000067","订货日期"), "ExpectNum="+GetTran("000045","期数"), 
            "OrderType="+GetTran("000106","订单类型"), "TotalMoney="+GetTran("000041","总金额"), "TotalPV="+GetTran("000113","总积分"), "Weight="+GetTran("000118","重量"), "InceptPerson="+GetTran("000383", "收货人姓名"), "country="+GetTran("000108", "收货人国家"), "province="+GetTran("000109","省份"), 
            "city="+GetTran("000110","城市"),"Xian=县", "Address="+GetTran("000112","收货地址"), "PostalCode="+GetTran("000073","邮编"), "Telephone="+GetTran("000115","联系电话"), "ConveyanceMode="+GetTran("001345", "发货方式"),  "WareHouseName="+GetTran("000386", "仓库"), "SeatName="+GetTran("000390", "库位") });

        }
        else
        {
            sb = Excel.GetExcelTable(dt, GetTran("001523", "订单发货表"), new string[] {  
                "StoreOrderID="+GetTran("000079","订单号"), "docid="+GetTran("000099","对应出库单号"),"Number=会员编号", "OrderDateTime="+GetTran("000067","订货日期"), "ExpectNum="+GetTran("000045","期数"), 
            "OrderType="+GetTran("000106","订单类型"), "TotalMoney="+GetTran("000041","总金额"), "TotalPV="+GetTran("000113","总积分"), "Weight="+GetTran("000118","重量"),  "InceptPerson="+GetTran("000383", "收货人姓名"), "country="+GetTran("000108", "收货人国家"), "province="+GetTran("000109","省份"), 
            "city="+GetTran("000110","城市"),"Xian=县", "Address="+GetTran("000112","收货地址"), "PostalCode="+GetTran("000073","邮编"), "Telephone="+GetTran("000115","联系电话"), "ConveyanceMode="+GetTran("001345", "发货方式"), "ConveyanceCompany="+GetTran("000121", "物流公司"), "WareHouseName="+GetTran("000386", "仓库"), "SeatName="+GetTran("000390", "库位") });
        }

        Response.AppendHeader("Content-Disposition", "attachment;filename=FileName.xls");
        Response.ContentEncoding = Encoding.UTF8;

        Response.Write(sb.ToString());

        Response.Flush();
        Response.End();
    }

    //必须加这个方法，要不然会引发：类型“GridView”的控件“GridView1”必须放在具有 runat=server 的窗体标记内... 异常。
    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }
    protected void hButton3_Click(object sender, EventArgs e)
    {
    }

    protected void Label2_Click(object sender, EventArgs e)
    {
        isButton = GetTran("000339", "详细");
    }

    protected void hLinkButton1_Click(object sender, EventArgs e)
    {
        //isButton = "h发货";
    }
    protected void hLbtn_Delete_Click(object sender, EventArgs e)
    {
        //isButton = "h删除";
    }
    protected void GridView1_CompanyConsign_RowDataBound(object sender, GridViewRowEventArgs e)
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
    protected void GridView2_CompanyConsign_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "dd=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=dd");
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            SetFanY();
        }
    }
    protected void GridView2_CompanyConsign_SelectedIndexChanged(object sender, EventArgs e)
    {
        string storeOrderID = ((Label)GridView2_CompanyConsign.SelectedRow.FindControl("aa")).Text;

        Response.Redirect("ShowBillDetailsB.aspx?DocID=" + storeOrderID);
    }
}