using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using DAL;
using BLL.CommonClass;
using BLL.other.Company;
using Model;
using Model.Other;

public partial class Company_StorageInBrowse : BLL.TranslationBase
{
    protected string msg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Permissions.CheckManagePermission(EnumCompanyPermission.StorageStorageInBrowse);

        if (!IsPostBack)
        {
            BindCountryList();
            FirstLoad();
            listCondition();

            Translations_More();
        }
    }

    /// <summary>
    /// 首次加载
    /// </summary>
    public void FirstLoad()
    {
        StringBuilder sb = new StringBuilder();
        string Currency = StorageInBrowseBLL.GetMoreCurrencyIDByCountryCode(Country.SelectedValue).ToString();
        int intDocType = QueryInStorageBLL.GetDocTypeIDByDocTypeName("RK");


        sb.Append(" D.DocTypeID=" + intDocType);
        sb.Append(" And " + this.Flag.SelectedValue.ToString() + " and d.Provider=p.ID ");

        string columns = "d.*,p.[Name]";
        string table = "InventoryDoc As D,ProviderInfo as p";
        string key = "D.ID";

        ViewState["SQLSTR"] = "select " + columns + " from " + table + " where " + sb.ToString() + " order by " + key + " desc";

        Pager pSorting = Page.FindControl("Pager1") as Pager;
        pSorting.PageBind(0, 10, table, columns, sb.ToString(), key, "gvInfo");
    }

    /// <summary>
    /// Translation
    /// </summary>
    protected void Translations_More()
    {
        TranControls(Btn_Search, new string[][] { new string[] { "000048", "查 询" } });

        TranControls(ddlCondition, new string[][] 
                        {
                            new string[] { "000045", "期数" }, 
                            new string[] { "002131", "入库制单人" },
                            new string[] { "002021", "业务员" },
                            new string[] { "002040", "购货地址" },
                            new string[] { "000041", "总金额" },
                            new string[] { "002134", "制单日期" }
                        }
                    );

        TranControls(Flag, new string[][] 
                        {
                            new string[] { "001009", "未审核" }, 
                            new string[] { "001011", "已审核" },
                            new string[] { "001069", "无效" }                           
                        }
                    );

        TranControls(gvInfo, new string[][] 
                        {
                            new string[] { "002164","审核入库单" }, 
                            new string[] { "000339","详细" },
                            new string[] { "002166","入库单号" }, 
                            new string[] { "000045","期数" },
                            new string[] { "000605","是否审核" }, 
                            new string[] { "001811","是否失效" },
                            new string[] { "000041","总金额" }, 
                            new string[] { "000113","总积分" }, 
                            new string[] { "002131","入库制单人" },
                            new string[] { "000655","审核人" }, 
                            new string[] { "001599","审核日期" },
                            new string[] { "002020","供应商" }, 
                            new string[] { "002021","业务员" },
                            new string[] { "002040","购货地址" }, 
                            new string[] { "000355","仓库名称" },
                            new string[] { "000357","库位名称" },
                            new string[] { "000658","批次" }, 
                            new string[] { "002167","入库单日期" },
                            new string[] { "000744","查看备注" }                           
                        }
                    );
    }

    protected void BindCountryList()
    {
        Country.DataSource = StorageInBrowseBLL.BindCountryList();
        Country.DataTextField = "Name";
        Country.DataValueField = "CountryCode";
        Country.DataBind();
    }

    protected void Btn_Search_Click(object sender, EventArgs e)
    {
        if (this.Flag.SelectedIndex != 0)
        {
            this.gvInfo.Columns[0].Visible = false;
        }
        else
        {
            this.gvInfo.Columns[0].Visible = true;
        }
        getDocDetiles();
        Translations_More();
    }

    public void getDocDetiles()
    {
        StringBuilder sb = new StringBuilder();
        string Currency = StorageInBrowseBLL.GetMoreCurrencyIDByCountryCode(Country.SelectedValue).ToString();
        int intDocType = QueryInStorageBLL.GetDocTypeIDByDocTypeName("RK");

        sb.Append(" D.DocTypeID=" + intDocType);


        if (ddlCondition.SelectedValue == "ExpectNum")
        {
            if (txtCondition.Text.Trim() != "")
            {
                int expectNum = 0;
                try
                {
                    expectNum = Convert.ToInt32(txtCondition.Text.Trim());
                }

                catch
                {
                    this.msg = Transforms.ReturnAlert(GetTran("006829", "期数输入错误!"));
                    return;
                }
                sb.Append(" and D." + ddlCondition.SelectedValue + "=" + expectNum.ToString());
            }
        }

        if (ddlCondition.SelectedIndex > 0 && ddlCondition.SelectedIndex <= 3)
        {
            if (txtCondition.Text.Trim() != "")
            {
                sb.Append(" and D." + ddlCondition.SelectedValue + " like '%" + txtCondition.Text.Trim() + "%'");
            }
        }

        if (ddlCondition.SelectedValue == "TotalMoney")
        {
            if (txtCondition.Text.Trim() != "")
            {
                try
                {
                    Convert.ToDouble(txtCondition.Text.Trim());
                }

                catch
                {
                    this.msg = Transforms.ReturnAlert(GetTran("002211", "输入错误!"));
                    return;
                }
                sb.Append(" and D." + ddlCondition.SelectedValue + " " + DropDownList1.SelectedValue + " " + Convert.ToDouble(txtCondition.Text.Trim().Replace("'", "")));
            }
        }

        if (ddlCondition.SelectedValue == "DocMakeTime")
        {
            if (txtCondition.Text.Trim() != "")
            {
                try
                {
                    Convert.ToDateTime(this.TextBox1.Text.Trim().Replace("'", ""));
                }

                catch
                {
                    this.msg = Transforms.ReturnAlert(GetTran("002211", "输入错误!"));
                    return;
                }
                sb.Append(" and DATEADD(day, DATEDIFF(day,0,D." + ddlCondition.SelectedValue + "), 0)" + DropDownList1.SelectedValue + " ' " + this.TextBox1.Text.Trim() + "'");
            }
        }

        sb.Append(" And " + Flag.SelectedValue.ToString() + " and d.Provider=p.ID ");

        string columns = "d.*,p.[Name]";
        string table = "InventoryDoc As D,ProviderInfo as p";
        string key = "D.ID";

        ViewState["SQLSTR"] = "select " + columns + " from " + table + " where " + sb.ToString() + " order by " + key + " desc";

        Pager pSorting = Page.FindControl("Pager1") as Pager;
        pSorting.PageBind(0, 10, table, columns, sb.ToString(), key, "gvInfo");
    }

    private void listCondition()
    {
        if (this.ddlCondition.SelectedIndex <= 3)
        {
            this.TextBox1.Visible = false;
            this.txtCondition.Visible = true;
            this.DropDownList1.Items.Clear();
            this.DropDownList1.Items.Add(new ListItem(GetTran("000378", "包括"), "like"));
        }

        if (ddlCondition.SelectedIndex > 3)
        {
            if (ddlCondition.SelectedIndex == 4)
            {
                this.TextBox1.Visible = false;
                this.txtCondition.Visible = true;
            }

            else
            {
                if (ddlCondition.SelectedIndex == 5)
                {
                    this.TextBox1.Visible = true;
                    this.txtCondition.Visible = false;
                }
            }

            this.DropDownList1.Items.Clear();
            this.DropDownList1.Items.Add(new ListItem(GetTran("000361", "大于"), ">"));
            this.DropDownList1.Items.Add(new ListItem(GetTran("000364", "大于等于"), ">="));
            this.DropDownList1.Items.Add(new ListItem(GetTran("000367", "小于"), "<"));
            this.DropDownList1.Items.Add(new ListItem(GetTran("000368", "小于等于"), "<="));
            this.DropDownList1.Items.Add(new ListItem(GetTran("000372", "等于"), "="));
        }
    }

    protected void ddlCondition_SelectedIndexChanged(object sender, EventArgs e)
    {
        listCondition();
    }

    protected void gvInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string[] Arg = Convert.ToString(e.CommandArgument).Split('_');
        string DocID = Arg[0];
        int isExist = StorageInBrowseBLL.DocIdIsExistByDocId(DocID);
        //Exist
        if (isExist > 0)
        {
            if (e.CommandName == "Auditing")
            {
                int isAuditing = StorageInBrowseBLL.IsAuditingByDocId(DocID, 1);
                //Effective(In other words,Auditing)
                if (isAuditing > 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("006888", "该入库单已经被审核！")));
                }
                //No Auditing
                else
                {
                    string DocAuditer = CommonDataBLL.GetNameByAdminID(Session["Company"].ToString());
                    DateTime DocAuditTime = MYDateTime1.GetCurrentDateTime();
                    string OperateIP = CommonDataBLL.OperateIP;
                    string OperateNum = CommonDataBLL.OperateBh;

                    //更新公司库存
                    string TempWareHouseID = Arg[1];
                    int changwei = Convert.ToInt32(Arg[2]);
                    int auditingCout = StorageInBrowseBLL.checkDoc(DocAuditer, DocAuditTime, OperateIP, OperateNum, DocID, TempWareHouseID, changwei);
                    if (auditingCout > 0)
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>if(confirm('" + GetTran("002214", "入库单审核成功，是否要打印此入库单?") + "'))window.open('docPrint.aspx?DocID=" + DocID + "');</script>");
                    }

                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002216", "入库单审核失败，请联系管理员!")));
                        return;
                    }
                }
            }
            else if (e.CommandName == "NoEffect")
            {
                int isEffect = StorageInBrowseBLL.IsAuditingByDocId(DocID, 0);
                //No effect
                if (isEffect <= 0)
                {
                    DateTime CloseDate = MYDateTime1.GetCurrentDateTime();
                    string OperateIP = CommonDataBLL.OperateIP;
                    string OperateNum = CommonDataBLL.OperateBh;
                    int noEffectCount = StorageInBrowseBLL.updDocTypeName(CloseDate, DocID, OperateIP, OperateNum);
                    if (noEffectCount > 0)
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002218", "此入库单审核无效成功!")));
                    }

                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002221", "此入库单审核无效失败，请联系管理员!")));
                    }
                }

                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("006890", "该入库单已经被审核无效！")));
                }

            }
            else if (e.CommandName == "Del")
            {
                ChangeLogs cl = new ChangeLogs("InventoryDoc", "DocID");
                cl.AddRecord(DocID);
                ChangeLogs cl2 = new ChangeLogs("InventoryDocDetails", "DocID");
                cl2.AddRecord(DocID);
                int delCount = StorageInBrowseBLL.delDoc(DocID);
                if (delCount > 0)
                {
                    cl.AddRecord(DocID);
                    cl.DeletedIntoLogs(ChangeCategory.company8, Session["Company"].ToString(), ENUM_USERTYPE.objecttype0);
                    cl2.AddRecord(DocID);
                    cl2.DeletedIntoLogs(ChangeCategory.company8, Session["Company"].ToString(), ENUM_USERTYPE.objecttype0);

                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002225", "入库单删除成功！")));
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002228", "入库单编辑失败，请联系管理员！")));
                    return;
                }
            }
            else if (e.CommandName == "Edit")
            {
                Response.Redirect("StorageInEdit.aspx?billID=" + DocID);
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("006894", "该入库单不存在！")));
        }
        Btn_Search_Click(null, null);
    }

    protected void gvInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
            int AudCount = 0, NullCount = 0, EdiCount = 0, DelCount = 0;
            AudCount = Permissions.GetPermissions(EnumCompanyPermission.StorageStorageInBrowseAuditing);
            DelCount = Permissions.GetPermissions(EnumCompanyPermission.StorageStorageInBrowseDelete);
            EdiCount = Permissions.GetPermissions(EnumCompanyPermission.StorageStorageInBrowseEdit);
            NullCount = Permissions.GetPermissions(EnumCompanyPermission.StorageStorageInBrowseNouse);
            if (AudCount.ToString() == "2203")
            {
                ((LinkButton)e.Row.FindControl("btnAuditing")).Enabled = true;
                ((LinkButton)e.Row.FindControl("btnAuditing")).Attributes.Add("onclick", "return confirm('" + GetTran("002234", "您确认要审核入库单吗？") + "');");
            }

            else
            {
                ((LinkButton)e.Row.FindControl("btnAuditing")).Enabled = false;
            }

            if (NullCount.ToString() == "2204")
            {
                ((LinkButton)e.Row.FindControl("btnnouse")).Enabled = true;
                ((LinkButton)e.Row.FindControl("btnnouse")).Attributes.Add("onclick", "return confirm('" + GetTran("002239", "您确认此入库单不予审核吗？") + "');");
            }

            else
            {
                ((LinkButton)e.Row.FindControl("btnnouse")).Enabled = false;

            }

            if (DelCount.ToString() == "2205")
            {
                ((LinkButton)e.Row.FindControl("btndelete")).Enabled = true;
                ((LinkButton)e.Row.FindControl("btndelete")).Attributes.Add("onclick", "return confirm('" + GetTran("002243", "您确认要删除此入库单吗？") + "');");
            }
            else
            {
                ((LinkButton)e.Row.FindControl("btndelete")).Enabled = false;
            }

            if (EdiCount.ToString() == "2206")
            {
                ((LinkButton)e.Row.FindControl("btnedit")).Enabled = true;
            }

            else
            {
                ((LinkButton)e.Row.FindControl("btnedit")).Enabled = false;
            }
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Attributes.Add("style", "background-image:url('images/tabledp.gif')");
        }
    }
    protected string GetWarehouseName(string WareHouseId)
    {
        string WarehouseName = StorageInBrowseBLL.GetWarehouseName(WareHouseId);
        return WarehouseName;
    }
    public static string GetDepotSeatName(string DepotSeatID)
    {
        string DepotSeatName = StorageInBrowseBLL.GetDepotSeatName(DepotSeatID);
        return DepotSeatName;
    }
    protected string SetVisible(string dd, string id, string _pageUrl)
    {
        if (dd.Length > 0)
        {
            string _openWin = "<a href =\"javascript:void(window.open('" + _pageUrl + "?ID=" + id + "','','left=300,top=150,width=250,height=150'))\">" + GetTran("000440", "查看") + "</a>";
            return _openWin;
        }
        else
        {
            return GetTran("000221", "无");
        }
    }

    public string GetCurrencyName(string Currency)
    {
        string CurrencyName = StorageInBrowseBLL.GetCurrencyNameByID(Currency);
        return CurrencyName;
    }

    protected string GetbyRegisterDate(string RegisterDate)
    {
        return Convert.ToDateTime(RegisterDate).AddHours(Convert.ToDouble(Session["WTH"])).ToString();
    }

    protected void image_download_Click1(object sender, EventArgs e)
    {
        //DataTable dt = StorageInBrowseBLL.OutToExcel_InventoryDoc_More(str);
        DataTable dt = DAL.DBHelper.ExecuteDataTable(ViewState["SQLSTR"].ToString());
        if (dt != null && dt.Rows.Count < 1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001712", "对不起,没有可以导出的数据！")));
            return;
        }
        else
        {
            dt.Columns.Add("StateFlagstr", typeof(System.String));
            dt.Columns.Add("CloseFlagstr", typeof(System.String));
            dt.Columns.Add("WareHouseName", typeof(System.String));
            dt.Columns.Add("SeatName", typeof(System.String));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //dt.Rows[i]["PayCurrency"] = GetCurrencyName(Convert.ToString(dt.Rows[i]["PayCurrency"]));

                if (Convert.ToString(dt.Rows[i]["StateFlag"]) == "0")
                {
                    dt.Rows[i]["StateFlagstr"] = GetTran("000235", "否");
                }
                else
                {
                    dt.Rows[i]["StateFlagstr"] = GetTran("000233", "是");
                }

                if (Convert.ToString(dt.Rows[i]["CloseFlag"]) == "0")
                {
                    dt.Rows[i]["CloseFlagstr"] = GetTran("000235", "否");
                }
                else
                {
                    dt.Rows[i]["CloseFlagstr"] = GetTran("000233", "是");
                }

                dt.Rows[i]["WareHouseName"] = GetWarehouseName(dt.Rows[i]["WareHouseID"].ToString());
                dt.Rows[i]["SeatName"] = GetWarehouseName(dt.Rows[i]["DepotSeatID"].ToString());
            }

            Excel.OutToExcel
            (dt, GetTran("002259", "入库单信息"), new string[]
                {
                    "DocID="+GetTran("002166","入库单号"), 
                    "ExpectNum="+GetTran("000045","期数"), 
                    "StateFlagstr="+GetTran("000605","是否审核"), 
                    "CloseFlagstr="+GetTran("001811","是否有效"), 
                    "TotalMoney="+GetTran("000041","总金额"), 
                    //"PayCurrency="+GetTran("000562","币种"),
                    "TotalPV="+GetTran("000113","总积分"), 
                    "DocMaker="+GetTran("002131","入库制单人"), 
                    "DocAuditer="+GetTran("000655","审核人"), 
                    "DocAuditTime="+GetTran("001599","审核日期"), 
                    "Name="+GetTran("002020","供应商"),
                    "OperationPerson="+GetTran("002021","业务员"),
                    "Address="+GetTran("002040","购货地址"),
                    "WareHouseName="+GetTran("000355","仓库名称"),
                    "SeatName="+GetTran("000357","库位名称"),
                    "BatchCode="+GetTran("000658","批次"),
                    "DocMakeTime="+GetTran("002167","入库单日期"),
                    "Note="+GetTran("000078","备注")
                }
            );
        }
    }
}