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
using Model;
using BLL;
using BLL.MoneyFlows;
using BLL.CommonClass;
using Model.Other;
using System.Collections.Generic;
using System.Text;
using DAL;
using Standard.Classes;
using System.Data.SqlClient;
using BLL.Logistics;

public partial class Company_AuditingStoreAccount : BLL.TranslationBase 
{
    
    ProcessRequest process = new ProcessRequest();
    protected void Page_Load(object sender, EventArgs e)
    {
        //判断是否是正常登录
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Expires = 0;
        //检查相应权限
        Permissions.CheckManagePermission(EnumCompanyPermission.FinanceAuditingStoreAccount);
        //清楚缓存
        Response.Cache.SetExpires(DateTime.Now.ToUniversalTime());
        //注册ajax
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));

        ViewState["bdk"] = 0;
        ViewState["zzk"] = 0;
        ViewState["jmj"] = 0;
        ViewState["yj"] = 0;
        ViewState["qt"] = 0;
        ViewState["bylj"] = 0;
        ViewState["zfje"] = 0;
        if (!IsPostBack)
        {
            //绑定查询条件
            searchType_stateChanged();
            ddl_type_SelectedIndexChanged(null,null);
            this.DropDownList1.Items.Add(new ListItem(GetTran("000633", "全部"), "-1"));
            this.DropDownList1.Items.Add(new ListItem(GetTran("007733", "未核实"), "1,2"));
            this.DropDownList1.Items.Add(new ListItem(GetTran("007734", "已核实"), "0"));         
            this.Datepicker1.Text = CommonDataBLL.GetDateBegin().ToString();
            this.Datepicker2.Text = CommonDataBLL.GetDateEnd().ToString();
            BtnConfirm_Click(null, null);

        }
        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.ddl_type, new string[][] { new string[] { "000633", "全部" }, new string[] { "000388", "服务机构" }, new string[] { "000599", "会员" } });
        TranControls(BtnConfirm, new string[][] { new string[] { "000340", "查询" } });

        this.TranControls(this.GridView1,
                new string[][]{
                    new string []{"000644","核实"},
                    new string []{"001195","编号"},
                    new string []{"000107","名称"},
                    new string []{"007735","汇款类型"},
                    new string []{"001593","状态"},
                    new string []{"000115","联系电话"},
                    new string []{"001970","汇款金额"},
                    new string []{"000698","付款方式"},
                    new string []{"000595","确认方式"},
                    new string []{"000738","付款用途"},
                    new string []{"007257","登记时间"},
                    new string []{"001155","审核时间"},
                    new string []{"000780","审核f期数"},
                    new string []{"000078","备注"},
                    new string []{"007258","财务备注"},
                    new string []{"000022","删除"}
                });
    }
    /// <summary>
    /// 设置条件
    /// </summary>
    private void searchType_stateChanged()
    {
        if (ddl_type.SelectedValue != "-1")
        {
            DropDownListCondition.Items.Clear();
            DropDownListCondition.Visible = true;
            if (DropDownCate.SelectedValue == "Remittances.RemitMoney" || DropDownCate.SelectedValue == "Remittances.PayexpectNum")
            {
                DropDownListCondition.Items.Add(new ListItem(GetTran("000802", "数值等于"), " = "));
                DropDownListCondition.Items.Add(new ListItem(GetTran("000804", "数值大于"), " > "));
                DropDownListCondition.Items.Add(new ListItem(GetTran("000808", "数值小于"), " < "));
                DropDownListCondition.Items.Add(new ListItem(GetTran("000810", "数值大于等于"), " >= "));
                DropDownListCondition.Items.Add(new ListItem(GetTran("000813", "数值小于等于"), " <= "));
                DropDownListCondition.Items.Add(new ListItem(GetTran("000816", "数值不等于"), " <> "));
                TextBox1.Visible = true;
                return;
            }
            if (this.DropDownCate.SelectedValue == "Remittances.[Use]" && ddl_type.SelectedValue == "remittances.RemitStatus=0")
            {
                DropDownListCondition.Items.Add(new ListItem(GetTran("007253", "服务机构订货款"), "=10"));
                DropDownListCondition.Items.Add(new ListItem(GetTran("007383", "服务机构周转款"), "=11"));
                TextBox1.Visible = false;
                return;
            }
            else if (this.DropDownCate.SelectedValue == "Remittances.[Use]" && ddl_type.SelectedValue == "remittances.RemitStatus=1")
            {
                DropDownListCondition.Items.Add(new ListItem(GetTran("007259", "会员现金账户"), "=0"));
                DropDownListCondition.Items.Add(new ListItem(GetTran("007260", "会员消费账户"), "=1"));
                //DropDownListCondition.Items.Add(new ListItem(GetTran("008117", "会员复消账户"), "=2"));
                TextBox1.Visible = false;
                return;
            }
            else
            {

                DropDownListCondition.Items.Add(new ListItem(GetTran("000821", "包含字符"), " like "));
                DropDownListCondition.Items.Add(new ListItem(GetTran("000822", "不包含字符"), " not like "));
                TextBox1.Visible = true;
            }
        }

    }
    /// <summary>
    /// 绑定汇款记录
    /// </summary>
    private void GetShopList()
    { 
        StringBuilder condition = new StringBuilder();
        string table = " Remittances ";
        condition.Append(" 1=1 and payway = 2");
        string BeginRiQi = "";
        string EndRiQi = "";
        if (this.Datepicker1.Text != "")
        {
            try
            {
                DateTime time=DateTime.Parse(this.Datepicker1.Text);
            }

            catch
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000827", "时间格式不正确！") + "')</script>");                
                return;
            }
            BeginRiQi = this.Datepicker1.Text.Trim().ToString();
            DisposeString.DisString(BeginRiQi, "'", "");
            if (this.Datepicker2.Text != "")
            {
                try
                {
                    DateTime time = DateTime.Parse(this.Datepicker2.Text);
                }

                catch
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000827", "时间格式不正确！") + "')</script>");                    
                    return;
                }
                EndRiQi = this.Datepicker2.Text.Trim().ToString();
                DisposeString.DisString(EndRiQi, "'", "");
                EndRiQi = (DateTime.Parse(EndRiQi).AddHours(23).AddMinutes(59).AddSeconds(59)).ToString();
                condition.Append(" and ReceivablesDate>= '" + BeginRiQi + "' and ReceivablesDate<='" + EndRiQi + "'");
            }
            else
            {
                condition.Append(" and ReceivablesDate>= '" + BeginRiQi + "'");
            }
        }

        else
        {
            if (this.Datepicker2.Text != "")
            {
                try
                {
                    DateTime time = DateTime.Parse(this.Datepicker2.Text);
                }
                catch
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000827", "时间格式不正确！") + "')</script>");                    
                    return;
                }
                EndRiQi = DateTime.Parse(this.Datepicker2.Text.Trim()).AddHours(23).AddMinutes(59).AddSeconds(59).ToString();
                condition.Append(" and ReceivablesDate<='" + EndRiQi + "'");
            }
        }

        if (this.DropDownList1.SelectedValue != "-1")
        {
            if(this.DropDownList1.SelectedValue=="true")
                condition.Append(" and ConfirmType in (" + this.DropDownList1.SelectedValue.Trim() + ")  ");
            else
                condition.Append(" and ConfirmType in (" + this.DropDownList1.SelectedValue.Trim() + ") ");
        }
        if (ddl_type.SelectedValue != "-1")
        {
            condition.Append(" and " + ddl_type.SelectedValue);
            if (DropDownCate.SelectedValue == "Remittances.RemitMoney" || DropDownCate.SelectedValue == "Remittances.PayexpectNum")
            {
                try
                {
                    double valid = Convert.ToDouble(TextBox1.Text.Trim());
                    if (this.TextBox1.Text.Length > 30)
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("006915", "对不起，请正确输入！") + "')</script>");
                        return;
                    }
                }
                catch
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000829", "对不起，你的查询条件必须是数字！") + "')</script>");
                    return;
                }
            }

            if (DropDownListCondition.SelectedValue.IndexOf("like") > 0)
            {
                if (this.TextBox1.Text.Trim().Length > 0)
                {
                    string tiaojian = TextBox1.Text;
                    condition.Append(" and " + DropDownCate.SelectedValue + DropDownListCondition.SelectedValue + "'%" + tiaojian + "%'");
                }
            }
            else
            {
                if (DropDownCate.SelectedValue == "Remittances.RemitMoney")
                {
                    try
                    {
                        Convert.ToDouble(this.TextBox1.Text.Trim());
                    }
                    catch
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000829", "对不起，你的查询条件必须是数字！") + "')</script>");
                        return;
                    }
                    condition.Append(" and " + DropDownCate.SelectedValue + DropDownListCondition.SelectedValue + Convert.ToDouble(TextBox1.Text));
                }
                else if (DropDownCate.SelectedValue == "Remittances.PayexpectNum")
                {
                    int ou = 0;
                    if (int.TryParse(TextBox1.Text.Trim(), out ou))
                    {
                        condition.Append(" and " + DropDownCate.SelectedValue + DropDownListCondition.SelectedValue + TextBox1.Text);
                    }
                    else
                    {
                        ScriptHelper.SetAlert(Page, GetTran("002095", "金额输入有误"));
                    }

                }
                else if (DropDownCate.SelectedValue == "Remittances.[Use]") {
                    condition.Append(" and " + DropDownCate.SelectedValue + DropDownListCondition.SelectedValue + TextBox1.Text);
                }
            }
        }

        string cloumns = "remittances.RemittancesDate,remittances.ConfirmType, Remittances.RemittancesMoney,Remittances.RemittancesCurrency, Remittances.Sender,Remittances.Managers,Remittances.ImportBank, Remittances.PayWay,Remittances.RemitNumber,Remittances.RemitStatus,Remittances.[Use],Remittances.StandardCurrency,Remittances.ConfirmType, Remittances.SenderID, Remittances.RemitMoney,Remittances.PayExpectNum,Remittances.Id,Remittances.ReceivablesDate,Remittances.PayExpectNum,Remittances.isgsqr,Remittances.Remark "; 
        string key = "Remittances.id";
        ViewState["key"] = key;
        ViewState["PageColumn"] = cloumns;
        ViewState["table"] = table;
        ViewState["condition"] = condition.ToString();
        this.GridView1.DataSourceID = null;
        this.Pager11.ControlName = "GridView1";
        this.Pager11.key = key;
        this.Pager11.PageColumn = cloumns;
        this.Pager11.Pageindex = 0;
        this.Pager11.PageTable = table;
        this.Pager11.Condition = condition.ToString();
        this.Pager11.PageSize = 10;
        this.Pager11.PageCount = 0;
        this.Pager11.PageBind();

        Translations();
    }
    /// <summary>
    /// 获取姓名
    /// </summary>
    /// <param name="type"></param>
    /// <param name="number"></param>
    /// <returns></returns>
    protected string GetName(string type,string number) {
        try
        {
            if (type == "1" || type=="2")
            {
                return Encryption.Encryption.GetDecipherName(DAL.DBHelper.ExecuteScalar("select isnull(name,'') as name from memberinfo where number='" + number + "'").ToString());
            }
            else if (type == "0")
            {
                return Encryption.Encryption.GetDecipherName(DAL.DBHelper.ExecuteScalar("select isnull(name,'') as name from storeinfo where storeid='" + number + "'").ToString());
            }
        }
        catch
        {
            return "";
        }
        return "";
    }

    /// <summary>
    /// 获取移动电话
    /// </summary>
    /// <param name="type"></param>
    /// <param name="number"></param>
    /// <returns></returns>
    protected string GetMobile(string type, string number) {
        try
        {
            if (type == "1" || type=="2")
            {
                return Encryption.Encryption.GetDecipherTele(DAL.DBHelper.ExecuteScalar("select isnull(MobileTele,'') as MobileTele from memberinfo where number='" + number + "'").ToString());
            }
            else if (type == "0")
            {
                return Encryption.Encryption.GetDecipherTele(DAL.DBHelper.ExecuteScalar("select isnull(MobileTele,'') as MobileTele from storeinfo where storeid='" + number + "'").ToString());
            }
            return "";
        }
        catch { return ""; }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
            
            switch (e.Row.Cells[8].Text)
            {
                case "10": e.Row.Cells[8].Text = GetTran("007253", "店铺订货款"); break;
                case "11": e.Row.Cells[8].Text = GetTran("007383", "店铺周转款"); break;
            }

            if (((HtmlInputHidden)e.Row.FindControl("ConfirmType")).Value != "0")
            {
                ((LinkButton)e.Row.FindControl("LinkButton1")).Visible = true;
                ((LinkButton)e.Row.FindControl("LinkButton1")).Attributes["onclick"] = "return confirm('" + GetTran("007740", "确定收到对应金额") + "？')";
            }

        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Attributes.Add("style", "background-image:url('images/tabledp.gif')");
            e.Row.Cells[0].Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + e.Row.Cells[0].Text + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
        }
        Translations();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Application.Lock();
        if (e.CommandName.ToString() == "Lbtn")
        {
            int AudCount = 0;
            //获取审核的权限
            AudCount = Permissions.GetPermissions(EnumCompanyPermission.FinanceAuditingStoreAccountAuditing);
            if (AudCount != 4102)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000847", "对不起，您没有权限！") + "');</script>");
                return;
            }
            string[] strs = e.CommandArgument.ToString().Split(',');
            if (strs[1] == "0")
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("000849", "不能重复审核！") + "')</script>");
                return;
            }
            else {
                string sql = "update Remittances set ConfirmType=0 where id=@id";
                SqlParameter[] para={new SqlParameter("@id",strs[0])};
                int ret = DAL.DBHelper.ExecuteNonQuery(sql, para, CommandType.Text);
                if (ret > 0) {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("000858", "审核成功！") + "')</script>");
                }
            } 
            //刷新数据
            BtnConfirm_Click(null, null);
        }
        Application.UnLock();
    }
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnConfirm_Click(object sender, EventArgs e)
    {
        GetShopList();
    }
    //查看备注
    protected string SetVisible(string dd, string id)
    {
        if (dd.Length > 0)
        {
            string _openWin = "";
            _openWin = "<a href =\"javascript:void(window.open('ShowHuiKuanRemark.aspx?id=" + id + "&type=0','','width=500,height=130'))\">" + GetTran("000440", "查看") + "</a>";
            return _openWin;
        }
        else
        {
            return GetTran("000221", "无");
        }
    }
    protected void DropDownCate_SelectedIndexChanged(object sender, EventArgs e)
    {
        searchType_stateChanged();
    }
    /// <summary>
    /// 导出excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        string cmd = "";
        cmd = "select Remittances.RemitNumber,id,IsGSQR, RemitStatus,cast(Remittances.RemitMoney as numeric(10,2)) as RemitMoney,Remittances.StandardCurrency,Remittances.[Use],CONVERT(varchar(100), Remittances.ReceivablesDate, 23) as ReceivablesDate,Remittances.PayExpectNum, Remittances.PayWay,Remittances.ConfirmType,Remittances.ImportBank,Remittances.Managers, Remittances.Sender, Remittances.SenderID,Remittances.Remark,Remittances.RemittancesCurrency,cast(Remittances.RemittancesMoney  as numeric(10,2)) as RemittancesMoney,Remittances.id, case IsGSQR when '0' then '未审核' when '1' then '审核' else '未知' end  from remittances   where " + ViewState["condition"] + ""; 
        DataTable dt1 = DBHelper.ExecuteDataTable(cmd);
        if (dt1 == null)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("000053", "没有数据，不能导出Excel！") + "')</script>");
            return;
        }
        if (dt1.Rows.Count < 1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("000053", "没有数据，不能导出Excel！") + "')</script>");
            return;
        }

        DataTable dt = new DataTable();
        dt = dt1.Clone();
        dt.Columns[2].DataType = typeof(String);
        dt.Columns[3].DataType = typeof(String);
        dt.Columns[6].DataType = typeof(String);
        dt.Columns[7].DataType = typeof(String);
        dt.Columns[10].DataType = typeof(String);
        dt.Columns[11].DataType = typeof(String);
        dt.Columns[12].DataType = typeof(String);
        dt.Columns[13].DataType = typeof(String);
        foreach (DataRow r in dt1.Rows)
        {
            DataRow newrow = dt.NewRow();
            newrow["RemitNumber"] = r["RemitNumber"];
            newrow["RemitMoney"] = r["RemitMoney"];
            newrow["StandardCurrency"] = r["StandardCurrency"];
            newrow["Use"] = r["Use"];
            newrow["ReceivablesDate"] = r["ReceivablesDate"];
            newrow["PayExpectNum"] = r["PayExpectNum"];
            newrow["PayWay"] = r["PayWay"];
            newrow["ConfirmType"] = r["ConfirmType"];
            newrow["ImportBank"] = r["ImportBank"];
            newrow["Managers"] = r["Managers"];
            newrow["Sender"] = Encryption.Encryption.GetDecipherName(r["Sender"].ToString());//解密汇款人姓名
            newrow["SenderID"] = Encryption.Encryption.GetDecipherNumber(r["SenderID"].ToString());//解密汇款人身份证
            newrow["Remark"] = r["Remark"];
            newrow["RemittancesCurrency"] = r["RemittancesCurrency"];
            newrow["RemittancesMoney"] = r["RemittancesMoney"];
            newrow["id"] = r["id"];
            dt.Rows.Add(newrow);
        }
        foreach (DataRow row in dt.Rows)
        {
            row[3] = GetUse(row[3].ToString());
            //判断店铺汇款是否审核
            if (row[6].ToString() == "2")
            {
                Object obj = RemittancesBLL.IsGSQR(int.Parse(row[15].ToString()));
                bool blean = Convert.ToBoolean(obj);
                if (blean == false)
                {
                    row[4] = "";
                }
            }
            //获得支付方式
            //row[6] = D_AccountBLL.GetPaymentstr((PaymentEnum)int.Parse(row[6].ToString()));
        }
        Excel.OutToExcel(dt, GetTran("000388", "店铺"),
            new string[] {"RemitNumber=" + GetTran("000000", "编号"), "IsGSQR="+GetTran("001593","状态"),"RemitMoney=" + GetTran("001970", "汇款金额"), 
                    "Use=" + GetTran("000588", "用途"), 
                    "ReceivablesDate=" + GetTran("000901", "付款时间"), "PayexpectNum=" + GetTran("000739", "付款期数"), 
                    "PayWay=" + GetTran("000698", "付款方式"), "ImportBank=" + GetTran("000601", "汇入银行"),
                    "Managers=" + GetTran("000519", "经办人"), "Sender=" + GetTran("000602", "汇款人"), 
                    "SenderID=" + GetTran("000782", "汇款人身份证"), "Remark=" + GetTran("000078", "备注"), 
                    "RemittancesMoney=" + GetTran("000789", "支付金额") 
                });
        StringBuilder sb = Excel.GetExcelTable(dt, GetTran("000599", "会员"), new string[] { "RemitNumber=" + GetTran("000024", "会员编号"), "RemitMoney=" + GetTran("000896", "付款金额"), "StandardCurrency=" + GetTran("000562", "币种"), "ReceivablesDate=" + GetTran("000901", "付款时间"), "PayexpectNum=" + GetTran("000739", "付款期数"), "PayWay=" + GetTran("000698", "付款方式"), "ImportBank=" + GetTran("000601", "汇入银行"), "Managers=" + GetTran("000519", "经办人"), "Sender=" + GetTran("000602", "汇款人"), "SenderID=" + GetTran("000743", "汇款人身份证"), "Remark=" + GetTran("000078", "备注"), "RemittancesCurrency=" + GetTran("000185", "支付币种"), "RemittancesMoney=" + GetTran("000789", "支付金额") });

        Response.Write(sb.ToString());

        Response.Flush();
        Response.End();

    }
    /// <summary>
    /// 获取确认方式
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    protected string GetConfirmType(string type) {
        string res = "";
        switch (type) {
            case "0": res = GetTran("000644", "核实");
                break;
            case "1": res = GetTran("000643", "传真");
                break;
            case "2": res = GetTran("000646", "电话");
                break;
        }
        return res;
    }
    /// <summary>
    /// 获取用途类型
    /// </summary>
    /// <param name="Use"></param>
    /// <returns></returns>
    protected string GetUse(string Use) {
        string res = "";
        switch (Use) { 
            case "0":
                res = GetTran("007259", "会员现金账户");
                break;
            case "1":
                res = GetTran("007260", "会员消费账户");
                break;
            case "2":
                res = GetTran("008117", "会员复消账户");
                break;
            case "10":
                res = GetTran("007253", "服务机构订货款");
                break;
            case"11":
                res = GetTran("007383", "服务机构周转款 ");
                break;
            default:
                res = "";
                break;

        }
        return res;
    }
    /// <summary>
    /// 获取付款方式
    /// </summary>
    /// <param name="PayWay"></param>
    /// <returns></returns>
    protected string GetPayWay(string PayWay) {
        string res = "";
        switch (PayWay) { 
            case "0":
                res = GetTran("005963", "在线支付");
                break;
            case "1":
                res = GetTran("007594", "普通汇款");
                break;
            case "2":
                res = GetTran("007741", "人工确认");
                break;
            default:
                res = "";
                break;

        }
        return res;
    }
    protected void ddl_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_type.SelectedValue != "-1")
        {
            DropDownCate.Items.Clear();
            if (ddl_type.SelectedValue == "remittances.RemitStatus=0")
            {
                DropDownCate.Visible = true;
                DropDownCate.Items.Insert(0, new ListItem(GetTran("000037", "服务机构编号"), "Remittances.RemitNumber"));
                DropDownCate.Items.Insert(1, new ListItem(GetTran("000738", "付款用途"), "Remittances.[Use]"));
                DropDownCate.Items.Insert(2, new ListItem(GetTran("001970", "汇款金额"), "Remittances.RemitMoney"));
                DropDownCate.Items.Insert(3, new ListItem(GetTran("007263", "汇款期数"), "Remittances.PayexpectNum"));

            }
            else if (ddl_type.SelectedValue == "remittances.RemitStatus=1")
            {
                DropDownCate.Visible = true;
                DropDownCate.Items.Insert(0, new ListItem(GetTran("000024", "会员编号"), "Remittances.RemitNumber"));
                DropDownCate.Items.Insert(1, new ListItem(GetTran("000738", "付款用途"), "Remittances.[Use]"));
                DropDownCate.Items.Insert(2, new ListItem(GetTran("001970", "汇款金额"), "Remittances.RemitMoney"));
                DropDownCate.Items.Insert(3, new ListItem(GetTran("007263", "汇款期数"), "Remittances.PayexpectNum"));
            }
        }
        else {
            DropDownCate.Visible = false;
            DropDownListCondition.Visible = false;
        }
        searchType_stateChanged();
    }
}
