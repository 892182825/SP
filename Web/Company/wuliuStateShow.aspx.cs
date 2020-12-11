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

using Model.Other;
using System.Data.SqlClient;
using BLL;
using BLL.Registration_declarations;


public partial class Company_wuliuStateShow : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        if (!IsPostBack)
        {
            Response.Cache.SetExpires(DateTime.Now);
            Permissions.CheckManagePermission(EnumCompanyPermission.LogisticswuliuStateShow);

            this.DropCurrency.DataSource = CountryBLL.GetCountryModels();
            this.DropCurrency.DataTextField = "name";
            this.DropCurrency.DataValueField = "id";
            this.DropCurrency.DataBind();

            listCondition();

            Button1_Click(null, null);


            /*int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int lastDay = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

            txtBox_OrderDateTimeStart.Text = txtBox_ConsignmentDateTimeStart.Text = year + "-" + month + "-" + "01";
            txtBox_OrderDateTimeEnd.Text = txtBox_ConsignmentDateTimeEnd.Text = year + "-" + month + "-" + lastDay;*/

            SetFanY();
        }

        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
    }

    public void SetFanY()
    {
        this.TranControls(this.btn_Submit, new string[][] { new string[] { "000048", "查 询" } });

        this.TranControls(this.GridView_WuliuStateShow,
                            new string[][]{													 
                                new string []{"000339","详细"},													 
                                //new string []{"000098","订货店铺"},
                                new string []{"000079","订单号"},	
                                new string []{"000099","对应出库单号"},
                                 new string []{"007206","快递单号"},
                                new string []{"000045","期数"},
                                new string []{"000100","付款否"},
                                new string []{"000102","发货否"},
                                new string []{"000539", "是否到达"},
                                new string []{"000383", "收货人姓名"},
                                new string []{"000108","收货人国家"},												 
                                new string []{"000109","省份"},												 
                                new string []{"000110","城市"},												 
                                new string []{"000112","收货地址"},
                                new string []{"000073","邮编"},
                                new string []{"000115","联系电话"},
                                new string []{"000041","总金额"},
                                new string []{"000113","总积分"},
                                new string []{"000118","重量"},
                                new string []{"000121","物流公司"},
                                new string []{"000067","订货日期"},
                                new string []{"000070","发货日期"},
                                });

        this.TranControls(this.DropDownList_Items,
                            new string[][]{	
                                //new string []{"000098","订货店铺"},													 
                                new string []{"000079","订单号"},		
                                new string []{"000114","邮政编码"},	
                                new string []{"000328","是否发货"},
                                new string []{"000121","物流公司"},
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

    public string SetFormatString(string str,int len)
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

    public string GetBiaoZhunTime(string dt,string ty)
    {
        return Convert.ToDateTime(dt).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToShortDateString();
    }

    private void listCondition()
    {
        if (DropDownList_Items.SelectedIndex <= 10 )
        {
            DropDownList_condition.Items.Clear();
            DropDownList_condition.Items.Add(new ListItem(GetTran("000378", "包含"), "like"));

            txtBox_keyWords.Visible = true;
            txtBox_rq.Visible = false;
        }
        else
        {
            DropDownList_condition.Items.Clear();
            DropDownList_condition.Items.Add(new ListItem(GetTran("000361", "大于"), ">"));
            DropDownList_condition.Items.Add(new ListItem(GetTran("000364", "大于等于"), ">="));
            DropDownList_condition.Items.Add(new ListItem(GetTran("000367", "小于"), "<"));
            DropDownList_condition.Items.Add(new ListItem(GetTran("000368", "小于等于"), "<="));
            DropDownList_condition.Items.Add(new ListItem(GetTran("000372", "等于"), "="));

            txtBox_keyWords.Visible = true;
            txtBox_rq.Visible = false;
        }

        //if (DropDownList_Items.SelectedIndex == 8 || DropDownList_Items.SelectedIndex == 9 || DropDownList_Items.SelectedIndex == 10)
        //{
        //    txtBox_keyWords.Visible = false;
        //    txtBox_rq.Visible = true;
        //}
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        
        string condition = "";

        string currency = "";

        if (DropCurrency.SelectedItem != null)
            currency = DropCurrency.SelectedItem.Text;

        string tiaoj = DropDownList_Items.SelectedItem.Value;
        string fh = DropDownList_condition.Text;
        //string keyword = txtBox_keyWords.Text.Trim();

        string keyword = "";
        //if (DropDownList_Items.SelectedIndex == 11 || DropDownList_Items.SelectedIndex == 9 || DropDownList_Items.SelectedIndex == 10)
        //    keyword = txtBox_rq.Text.Trim();
        //else
            keyword = txtBox_keyWords.Text.Trim().Replace("'", "").Replace("〖", "[").Replace("〗", "]");

        string totalDataStart = txtBox_OrderDateTimeStart.Text.Trim();
        string totalDataEnd = txtBox_OrderDateTimeEnd.Text.Trim();
        string consignmentDateStart = txtBox_ConsignmentDateTimeStart.Text.Trim();
        string consignmentDateEnd = txtBox_ConsignmentDateTimeEnd.Text.Trim();

        if (String.IsNullOrEmpty(keyword))
        {
            condition = "(select country from city where cpccode=so.cpccode)='" + currency + "' and 1=1";
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
                    pjtj = tiaoj + " like '%" + keyword + "%'";

            }
            else
            {
                if (tiaoj == "TotalMoney" || tiaoj == "Carriage")
                {
                    try
                    {
                        keyword = keyword.Replace(",", "");
                        Convert.ToDouble(keyword);
                        if (keyword.Length > 8)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('金额输入太大！')</script>");
                            return;
                        }
                    }
                    catch
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000443", "只能输入数字") + "')</script>");
                        return;
                    }
                }

                if (tiaoj == "ExpectNum")
                {
                    try
                    {
                        Convert.ToInt32(keyword);
                    }
                    catch
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000443", "只能输入数字") + "')</script>");
                        return;
                    }
                }
                pjtj = tiaoj + DropDownList_condition.SelectedItem.Value + "'" + keyword + "'";
            }
            condition = "(select country from city where cpccode=so.cpccode)='" + currency + "' and " + pjtj + " and 1=1";
        }

        try
        {
            if (totalDataStart != "")
            {
                Convert.ToDateTime(totalDataStart);
                condition = condition + " and dateadd(hour," + BLL.other.Company.WordlTimeBLL.ConvertAddHours() + ",OrderDateTime)>='" + totalDataStart + " 00:00:00'";
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
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000450", "日期格式错误，请重新输入！") + "')</script>");
            return;
        }

        
        condition = condition.Replace("〖Country〗", "(select country from city where cpccode=so.cpccode)");
        condition = condition.Replace("〖Province〗", "(select Province from city where cpccode=so.cpccode)");
        condition = condition.Replace("〖City〗", "(select City from city where cpccode=so.cpccode)");

        ViewState["condition"] = condition;

        Pager1.PageBind(0, 10, "dbo.StoreOrder so left outer join city on so.cpccode=city.cpccode", @"StoreID, StoreOrderID, ExpectNum,IsCheckOut,IsSent,IsReceived
                            ,InceptPerson,InceptAddress,PostalCode,kuaididh,Telephone,TotalMoney,city.country,city.province,city.city,ConveyanceCompany
                            ,TotalPV,Telephone,Weight,OrderDateTime,ConsignmentDateTime", condition, "StoreOrderID", "GridView_WuliuStateShow", "OrderDateTime", 0);

        SetFanY();
    }
    protected void DropDownList_Items_SelectedIndexChanged(object sender, EventArgs e)
    {
        listCondition();
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string storeOrderID = ((Label)GridView_WuliuStateShow.SelectedRow.FindControl("lab_StoreOrderID")).Text;

        Response.Redirect("ViewStoreOrder.aspx?storeOrderID=" + storeOrderID);
    }

    public string StringFormat(string sf)
    {
        if (sf == "Y")
            return new TranslationBase().GetTran("000233");
        else
            return new TranslationBase().GetTran("000235");
    }
    protected void Button2_Click(object sender, EventArgs e)
    {/*HttpUtility.UrlPathEncode("文件")*/

        string tj = "";
        if (ViewState["condition"] == null)
            tj = "1=1";
        else
            tj = ViewState["condition"].ToString();

        string sqlcmd = @"select StoreID, StoreOrderID, ExpectNum,case IsCheckOut when 'Y' then N'" + GetTran("000233", "是") + @"' when 'N' then N'" + GetTran("000235") + @"' end as IsCheckOut,case IsSent when 'Y' then N'" + GetTran("000233", "是") + @"' when 'N' then N'" + GetTran("000235") + @"' end as IsSent,case IsReceived when 'Y' then N'" + GetTran("000233", "是") + @"' when 'N' then N'" + GetTran("000235") + @"' end as IsReceived 
                        ,InceptPerson,city.country,city.province,city.city,InceptAddress,PostalCode,Telephone,TotalMoney,ConveyanceCompany
                        ,TotalPV,Weight,OrderDateTime,ConsignmentDateTime from dbo.StoreOrder so left outer join 
                        city on so.cpccode=city.cpccode where " + tj + " order by OrderDateTime desc";

        DataTable dt = DBHelper.ExecuteDataTable(sqlcmd);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dt.Rows[i]["StoreOrderID"] = "&nbsp;" + dt.Rows[i]["StoreOrderID"];
            dt.Rows[i]["InceptPerson"] = Encryption.Encryption.GetDecipherName(dt.Rows[i]["InceptPerson"].ToString());
            dt.Rows[i]["InceptAddress"] = Encryption.Encryption.GetDecipherAddress(dt.Rows[i]["InceptAddress"].ToString());
            dt.Rows[i]["Telephone"] = Encryption.Encryption.GetDecipherTele(dt.Rows[i]["Telephone"].ToString());

            dt.Rows[i]["OrderDateTime"] = GetBiaoZhunTime(dt.Rows[i]["OrderDateTime"].ToString(),"ty");
            dt.Rows[i]["ConsignmentDateTime"] = GetBiaoZhunTime(dt.Rows[i]["ConsignmentDateTime"].ToString(), "ty");
        }

        Excel.OutToExcel(dt, "发货跟踪表", new string[] {"StoreOrderID="+GetTran("000079","订单号"), "ExpectNum="+GetTran("000045","期数"), "IsSent="+GetTran("000102","发货否"), 
            "IsReceived="+GetTran("000539", "是否到达"), "InceptPerson="+GetTran("000383", "收货人姓名"), "country="+GetTran("000108", "收货人国家"), "province="+GetTran("000109","省份"), "city="+GetTran("000110","城市"), "InceptAddress="+GetTran("000112","收货地址"), "PostalCode="+GetTran("000073","邮编"), "Telephone="+GetTran("000115","联系电话"), "TotalMoney="+GetTran("000041","总金额"), 
            "TotalPV="+GetTran("000113","总积分"), "Weight="+GetTran("000118","重量"), "ConveyanceCompany="+GetTran("000121","物流公司"),"OrderDateTime="+GetTran("000067","订货日期"), "ConsignmentDateTime="+GetTran("000070","发货日期") });


        /*System.Text.StringBuilder sbexcel = Excel.GetExcelTable(dt, "发货跟踪表", new string[] { "StoreID=订货店铺", "StoreOrderID=订单号", "OutStorageOrderID=对应出库单号", "ExpectNum=期数", "IsCheckOut=是否付款", "IsSent=是否发货", 
            "IsReceived=是否到达", "InceptPerson=收获人姓名", "country=收货人国家", "province=省份", "city=城市", "InceptAddress=收获人地址", "PostalCode=收货人邮编", "Telephone=收货人电话", "TotalMoney=订货总金额", "Name=币种", 
            "TotalPV=订货总积分", "Weight=重量", "ConveyanceCompany=物流公司","OrderDateTime=订货日期", "ConsignmentDateTime=发货时间" });

        Response.AppendHeader("Content-Disposition", "attachment;filename=FileName.xls");
        Response.ContentEncoding = Encoding.GetEncoding("gb2312");

        Response.Write(sbexcel.ToString());
        Response.Flush();
        Response.End();*/

    }

    //必须加这个方法，要不然会引发：类型“GridView”的控件“GridView1”必须放在具有 runat=server 的窗体标记内... 异常。
    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }
    protected void GridView_WuliuStateShow_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "xabc=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=xabc");

            
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            SetFanY();
        }
    }


    protected void GridView_WuliuStateShow_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "select")
        {
            Response.Redirect("ViewStoreOrder.aspx?storeOrderID=" + e.CommandArgument.ToString());
        }
    }
}
