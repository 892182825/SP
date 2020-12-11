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
using BLL.Registration_declarations;
using System.Collections.Generic;
using Model.Other;
using BLL.CommonClass;

public partial class Company_AdvanceQueryView : BLL.TranslationBase
{
    protected Hashtable table;
    protected Hashtable StringOfNumtable;  //类型是String的数字串
    protected static bool blsort = true;
    protected DataTable dt2 = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        //检查管理员权限
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.BalanceDetialQuery);

        if (!IsPostBack)
        {
            if (Session["Default_Currency"] == null) Session["Default_Currency"] = "";

            CommonDataBLL.BindCurrency_IDList(this.DropCurrency, Session["Default_Currency"].ToString());

            if (Session["gaojiSql"] != null && Session["AdvanceCount"] != null)
            {
                //接收传递过来的字符串
                string sql = Session["gaojiSql"].ToString();
                string a_count = Session["AdvanceCount"].ToString();
                //销毁这个变量
                Session["gaojiSql"] = null;
                Session["AdvanceCount"] = null;

                ViewState["sql"] = sql;
                sql = sql.Replace("|*|", " " + (PageIndex - 1) * 10 + " ");
                //保存字符串

                ViewState["AdvanceCount"] = a_count;
                if (sql != "")
                {
                    GetList(sql, a_count);
                }

                CurrentGrass.Text = GetTran("000156", "第") + " " + Request.QueryString["CurGrass"].ToString().Trim() + " " + GetTran("000157", "期");
            }
            else
            {
                Response.Redirect("AdvanceQuery.aspx");
            }
        }
    }
    private void GetList(string sql, string a_count)
    {
        sql = sql.Replace("|*|", " " + (PageIndex - 1) * 10 + " ");
        DataTable dt;

        try
        {
            dt = QueryInfo.GetWeaveInfo(sql);
            RCount = QueryInfo.GetCount(a_count);
            int k = dt.Rows.Count;
        }
        catch (Exception)
        {
            ScriptHelper.SetAlert(Page, GetTran("001039", "提供参数异常，无法进行有效查询"), "AdvanceQuery.aspx");
            return;
        }
        if (RCount % 10 == 0)
        {
            PageCount = RCount / 10;
        }
        else
        {
            PageCount = RCount / 10 + 1;
        }

        //在此处理加和
        table = new Hashtable();

        StringOfNumtable = new Hashtable();

        ArrayList keyList = QueryInfo.getList(Convert.ToDouble(Session["rate"]));

        //如果存在需要统计的列，则加到统计列表中
        foreach (QueryKey key in keyList)
        {
            if (dt.Columns.Contains(key.Name) && key.NeedCount == true)
            {
                table.Add(key.Name, key.CountType);
            }
            if (dt.Columns.Contains(key.Name) && key.DispType.ToLower() == "string")
            {
                StringOfNumtable.Add(key.Name, key.DispType);
            }
        }

        int pageIndex = GridView1.PageIndex;
        int pageSize = GridView1.PageSize;

        if (dt.Rows.Count > 0)
        {
            int count = 0;
            //循环统计列
            foreach (DataColumn col in dt.Columns)
            {
                if (table.Contains(col.ColumnName))
                {
                    #region "检查看是否存在列，并是否需统计"
                    //如果包含统计列
                    string colName = col.ColumnName;

                    switch (table[colName].ToString())
                    {
                        case "double":
                            double retval = 0;

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (i < dt.Rows.Count)
                                {
                                    if (dt.Rows[i][colName] != DBNull.Value)
                                        retval += Convert.ToDouble(dt.Rows[i][colName]);
                                }
                                else
                                    break;
                            }
                            table[count] = retval.ToString("f2");
                            break;
                        case "int":
                            int valInt = 0;

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (i < dt.Rows.Count)
                                {
                                    if (dt.Rows[i][colName] != DBNull.Value)
                                        valInt += Convert.ToInt32(dt.Rows[i][colName]);
                                }
                                else
                                    break;
                            }
                            table[count] = valInt;
                            break;

                    }
                    table.Remove(colName);
                    #endregion
                }

                if (StringOfNumtable.Contains(col.ColumnName))
                {
                    #region "检查看是否存在数字字符列"
                    //如果包含统计列
                    string colName = col.ColumnName;

                    switch (StringOfNumtable[colName].ToString())
                    {
                        case "string":
                            StringOfNumtable[count] = "string";
                            break;

                    }
                    StringOfNumtable.Remove(colName);
                    #endregion
                }
                count++;
            }


            GridView1.DataSource = dt;
            GridView1.DataBind();
            if (this.dropPageList.Items.Count == 0)
            {
                for (int i = 0; i < PageCount; i++)
                {
                    dropPageList.Items.Add(new ListItem((i + 1).ToString(), (i).ToString()));
                }
            }
            this.Label1.Text = GetTran("001045", "共") + " " + RCount + " " + GetTran("001049", "条记录 ") + " " + GetTran("000156", "第") + " " + (PageIndex).ToString() + " " + GetTran("001055", "页") + " " + PageCount + GetTran("001055", "页");
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("000760", "对不起，找不到指定条件的记录 ") + "')</script>");
        }
    }

    private void GetList(string sql, bool showAll)
    {

        sql = sql.Replace("|*|", " " + (1 - 1) * 10 + " ");
        sql = sql.Replace("top 10", "");
        DataTable dt;
        try
        {
            dt = QueryInfo.GetWeaveInfo(sql);
        }

        catch
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("001063", "对不起，您指定的查询条件错误，请检查 ") + "！')</script>");
            return;
        }

        //在此处理加和
        table = new Hashtable();
        StringOfNumtable = new Hashtable();
        ArrayList keyList = QueryInfo.getList(Convert.ToDouble(Session["rate"]));

        //如果存在需要统计的列，则加到统计列表中
        foreach (QueryKey key in keyList)
        {
            if (dt.Columns.Contains(key.Name) && key.NeedCount == true)
            {
                table.Add(key.Name, key.CountType);
            }
            if (dt.Columns.Contains(key.Name) && key.DispType.ToLower() == "string")
            {
                StringOfNumtable.Add(key.Name, key.DispType);
            }
        }

        int pageIndex = this.GridView1.PageIndex;
        int pageSize = GridView1.PageSize;
        if (showAll)
        {
            pageIndex = 0;
            pageSize = dt.Rows.Count;
        }

        if (dt.Rows.Count > 0)
        {
            int count = 0;
            //循环统计列
            foreach (DataColumn col in dt.Columns)
            {
                if (table.Contains(col.ColumnName))
                {
                    #region "检查看是否存在列，并是否需统计"
                    //如果包含统计列
                    string colName = col.ColumnName;

                    switch (table[colName].ToString())
                    {
                        case "double":
                            double retval = 0;

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (i < dt.Rows.Count)
                                {
                                    if (dt.Rows[i][colName] != DBNull.Value)
                                        retval += Convert.ToDouble(dt.Rows[i][colName]);
                                }
                                else
                                    break;
                            }
                            table[count] = retval;
                            break;
                        case "int":
                            int valInt = 0;

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (i < dt.Rows.Count)
                                {
                                    if (dt.Rows[i][colName] != DBNull.Value)
                                        valInt += Convert.ToInt32(dt.Rows[i][colName]);
                                }
                                else
                                    break;
                            }
                            table[count] = valInt;
                            break;
                    }
                    table.Remove(colName);
                    #endregion
                }

                if (StringOfNumtable.Contains(col.ColumnName))
                {
                    #region "检查看是否存在数字字符列"
                    //如果包含统计列
                    string colName = col.ColumnName;

                    switch (StringOfNumtable[colName].ToString())
                    {
                        case "string":
                            StringOfNumtable[count] = "string";
                            break;
                    }
                    StringOfNumtable.Remove(colName);
                    #endregion
                }
                count++;
            }


            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("000760", "对不起，找不到指定条件的记录 ") + "')</script>");
        }
    }
    protected void BtnFirstPage_Click(object sender, EventArgs e)
    {
        GridView1.PageIndex = 0;

        //重新绑定数据
        GetList(ViewState["sql"].ToString(), ViewState["AdvanceCount"].ToString());
    }
    protected void BtnLastPage_Click(object sender, EventArgs e)
    {
        GridView1.PageIndex = GridView1.PageCount - 1;

        //重新绑定数据
        GetList(ViewState["sql"].ToString(), ViewState["AdvanceCount"].ToString());
    }
    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        ArrayList keyList = QueryInfo.getList(Convert.ToDouble(Session["rate"]));

        int count2 = 0;
        foreach (DataControlField column in GridView1.Columns)
        {
            column.HeaderStyle.Width = ((QueryKey)keyList[count2]).Width;
            count2++;
        }

        GridView1.Attributes.Add("style", "tumblr:" + (50 * count2));

    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            ArrayList keyList = QueryInfo.getList(Convert.ToDouble(Session["rate"]));
            int count2 = 0;
            foreach (DataControlField column in GridView1.Columns)
            {
                column.HeaderStyle.Width = ((QueryKey)keyList[count2]).Width;
                count2++;
            }
        }
        string sql = ViewState["sql"].ToString();
        sql = sql.Replace("|*|", " " + (1 - 1) * 10 + " ");
        DataTable dt = DAL.DBHelper.ExecuteDataTable(sql);

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            string sql = ViewState["sql"].ToString();
            sql = sql.Replace("|*|", " " + (1 - 1) * 10 + " ");
            dt2 = DAL.DBHelper.ExecuteDataTable(sql);
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
            IDictionaryEnumerator myEnumerator = StringOfNumtable.GetEnumerator();

            foreach (TableCell cell in e.Row.Cells)
            {
                cell.Text = cell.Text.Replace("&lt;", "<");
                cell.Text = cell.Text.Replace("&gt;", ">");
                cell.Text = cell.Text.Replace("&lt", "<");
                cell.Text = cell.Text.Replace("&nbsp;", "");
                cell.Text = cell.Text.Replace("&amp;nbsp;", "");
                try
                {
                    DateTime time = DateTime.Parse(cell.Text);

                }
                catch
                {
                }

            }


            for (int i = 0; i < dt2.Columns.Count; i++)
            {
                string name = dt2.Columns[i].ColumnName;
                if (name == GetTran("000000", "安置人姓名"))
                {
                    e.Row.Cells[i].Text = Encryption.Encryption.GetDecipherName(e.Row.Cells[i].Text);
                }
                else if (name == GetTran("000000", "推荐人姓名"))
                {
                    e.Row.Cells[i].Text = Encryption.Encryption.GetDecipherName(e.Row.Cells[i].Text);
                }
                else if (name == GetTran("000025", "会员姓名"))
                {
                    e.Row.Cells[i].Text = Encryption.Encryption.GetDecipherName(e.Row.Cells[i].Text);
                }
                else if (name == GetTran("000085", "性别"))
                {
                    if (e.Row.Cells[i].Text == "0")
                    {
                        e.Row.Cells[i].Text = "女";
                    }
                    else
                    {
                        e.Row.Cells[i].Text = "男";
                    }
                }
                else if (name == GetTran("000086", "开户名"))
                {
                    e.Row.Cells[i].Text = Encryption.Encryption.GetDecipherName(e.Row.Cells[i].Text);
                }
                else if (name == GetTran("000065", "家庭电话"))
                {
                    e.Row.Cells[i].Text = Encryption.Encryption.GetDecipherTele(e.Row.Cells[i].Text);
                }
                else if (name == GetTran("000044", "办公电话"))
                {
                    e.Row.Cells[i].Text = Encryption.Encryption.GetDecipherTele(e.Row.Cells[i].Text);
                }
                else if (name == GetTran("000069", "移动电话"))
                {
                    e.Row.Cells[i].Text = Encryption.Encryption.GetDecipherTele(e.Row.Cells[i].Text);
                }
                else if (name == GetTran("000071", "传真电话"))
                {
                    e.Row.Cells[i].Text = Encryption.Encryption.GetDecipherTele(e.Row.Cells[i].Text);
                }
                else if (name == GetTran("000083", "证件号码"))
                {
                    e.Row.Cells[i].Text = Encryption.Encryption.GetDecipherNumber(e.Row.Cells[i].Text);
                }
                else if (name == GetTran("000923", "卡号"))
                {
                    e.Row.Cells[i].Text = Encryption.Encryption.GetDecipherCard(e.Row.Cells[i].Text);
                }
                else if (name == GetTran("000111", "银行账号"))
                {
                    e.Row.Cells[i].Text = Encryption.Encryption.GetDecipherName(e.Row.Cells[i].Text);
                }
                else if (name == GetTran("000920", "详细地址"))
                {
                    e.Row.Cells[i].Text = Encryption.Encryption.GetDecipherAddress(e.Row.Cells[i].Text);
                }
                else if (name == GetTran("000057", "注册日期"))
                {
                    e.Row.Cells[i].Text = PageBase.GetbyDT(e.Row.Cells[i].Text);
                }
                else if (name == GetTran("000105", "出生日期"))
                {
                    e.Row.Cells[i].Text = Convert.ToDateTime(e.Row.Cells[i].Text).ToString("yyyy-MM-dd");
                }
                else if (name == GetTran("000916", "国家省份城市"))
                {
                    CityModel model = DAL.CommonDataDAL.GetCPCCode(e.Row.Cells[i].Text.Trim());
                    e.Row.Cells[i].Text = model.Country + model.Province + model.City;
                }
                else if (name == GetTran("000087", "开户银行"))
                {
                    e.Row.Cells[i].Text = DAL.CommonDataDAL.GetBankName(e.Row.Cells[i].Text.Trim());
                }
                else if (name == "新人激励奖")
                {
                    if (AjaxClass.GetCurrency(Convert.ToInt32(Session["currency"]), Convert.ToInt32(DropCurrency.SelectedValue)) == 0)
                    {
                        e.Row.Cells[i].Text = "0";
                    }
                    else
                    {
                        if (e.Row.Cells[i].Text != "")
                        {
                            e.Row.Cells[i].Text = (Convert.ToDouble(e.Row.Cells[i].Text) / AjaxClass.GetCurrency(Convert.ToInt32(Session["currency"]), Convert.ToInt32(DropCurrency.SelectedValue))).ToString("f2");
                        }
                        else
                        {
                            e.Row.Cells[i].Text = "0";
                        }
                    }
                }
                else if (name == "组织奖")
                {
                    if (AjaxClass.GetCurrency(Convert.ToInt32(Session["currency"]), Convert.ToInt32(DropCurrency.SelectedValue)) == 0)
                    {
                        e.Row.Cells[i].Text = "0";
                    }
                    else
                    {
                        if (e.Row.Cells[i].Text != "")
                        {
                            e.Row.Cells[i].Text = (Convert.ToDouble(e.Row.Cells[i].Text) / AjaxClass.GetCurrency(Convert.ToInt32(Session["currency"]), Convert.ToInt32(DropCurrency.SelectedValue))).ToString("f2");
                        }
                        else
                        {
                            e.Row.Cells[i].Text = "0";
                        }
                    }
                }
                else if (name == " 辅导奖")
                {
                    if (AjaxClass.GetCurrency(Convert.ToInt32(Session["currency"]), Convert.ToInt32(DropCurrency.SelectedValue)) == 0)
                    {
                        e.Row.Cells[i].Text = "0";
                    }
                    else
                    {
                        if (e.Row.Cells[i].Text != "")
                        {
                            e.Row.Cells[i].Text = (Convert.ToDouble(e.Row.Cells[i].Text) / AjaxClass.GetCurrency(Convert.ToInt32(Session["currency"]), Convert.ToInt32(DropCurrency.SelectedValue))).ToString("f2");
                        }
                        else
                        {
                            e.Row.Cells[i].Text = "0";
                        }
                    }
                }
                else if (name == "互动奖")
                {
                    if (AjaxClass.GetCurrency(Convert.ToInt32(Session["currency"]), Convert.ToInt32(DropCurrency.SelectedValue)) == 0)
                    {
                        e.Row.Cells[i].Text = "0";
                    }
                    else
                    {
                        if (e.Row.Cells[i].Text != "")
                        {
                            e.Row.Cells[i].Text = (Convert.ToDouble(e.Row.Cells[i].Text) / AjaxClass.GetCurrency(Convert.ToInt32(Session["currency"]), Convert.ToInt32(DropCurrency.SelectedValue))).ToString("f2");
                        }
                        else
                        {
                            e.Row.Cells[i].Text = "0";
                        }
                    }
                }
                else if (name == "销售提成")
                {
                    if (AjaxClass.GetCurrency(Convert.ToInt32(Session["currency"]), Convert.ToInt32(DropCurrency.SelectedValue)) == 0)
                    {
                        e.Row.Cells[i].Text = "0";
                    }
                    else
                    {
                        if (e.Row.Cells[i].Text != "")
                        {
                            e.Row.Cells[i].Text = (Convert.ToDouble(e.Row.Cells[i].Text) / AjaxClass.GetCurrency(Convert.ToInt32(Session["currency"]), Convert.ToInt32(DropCurrency.SelectedValue))).ToString("f2");
                        }
                        else
                        {
                            e.Row.Cells[i].Text = "0";
                        }
                    }
                }
                else if (name == "销售分红")
                {
                    if (AjaxClass.GetCurrency(Convert.ToInt32(Session["currency"]), Convert.ToInt32(DropCurrency.SelectedValue)) == 0)
                    {
                        e.Row.Cells[i].Text = "0";
                    }
                    else
                    {
                        if (e.Row.Cells[i].Text != "")
                        {
                            e.Row.Cells[i].Text = (Convert.ToDouble(e.Row.Cells[i].Text) / AjaxClass.GetCurrency(Convert.ToInt32(Session["currency"]), Convert.ToInt32(DropCurrency.SelectedValue))).ToString("f2");
                        }
                        else
                        {
                            e.Row.Cells[i].Text = "0";
                        }
                    }
                }
                else if (name == "管理奖")
                {
                    if (AjaxClass.GetCurrency(Convert.ToInt32(Session["currency"]), Convert.ToInt32(DropCurrency.SelectedValue)) == 0)
                    {
                        e.Row.Cells[i].Text = "0";
                    }
                    else
                    {
                        if (e.Row.Cells[i].Text != "")
                        {
                            e.Row.Cells[i].Text = (Convert.ToDouble(e.Row.Cells[i].Text) / AjaxClass.GetCurrency(Convert.ToInt32(Session["currency"]), Convert.ToInt32(DropCurrency.SelectedValue))).ToString("f2");
                        }
                        else
                        {
                            e.Row.Cells[i].Text = "0";
                        }
                    }
                }
                else if (name == "物流中心补贴")
                {
                    if (AjaxClass.GetCurrency(Convert.ToInt32(Session["currency"]), Convert.ToInt32(DropCurrency.SelectedValue)) == 0)
                    {
                        e.Row.Cells[i].Text = "0";
                    }
                    else
                    {
                        if (e.Row.Cells[i].Text != "")
                        {
                            e.Row.Cells[i].Text = (Convert.ToDouble(e.Row.Cells[i].Text) / AjaxClass.GetCurrency(Convert.ToInt32(Session["currency"]), Convert.ToInt32(DropCurrency.SelectedValue))).ToString("f2");
                        }
                        else
                        {
                            e.Row.Cells[i].Text = "0";
                        }
                    }
                }
                else if (name == "网上报单中心补贴")
                {
                    if (AjaxClass.GetCurrency(Convert.ToInt32(Session["currency"]), Convert.ToInt32(DropCurrency.SelectedValue)) == 0)
                    {
                        e.Row.Cells[i].Text = "0";
                    }
                    else
                    {
                        if (e.Row.Cells[i].Text != "")
                        {
                            e.Row.Cells[i].Text = (Convert.ToDouble(e.Row.Cells[i].Text) / AjaxClass.GetCurrency(Convert.ToInt32(Session["currency"]), Convert.ToInt32(DropCurrency.SelectedValue))).ToString("f2");
                        }
                        else
                        {
                            e.Row.Cells[i].Text = "0";
                        }
                    }
                }
                else if (name == GetTran("000247", "总计"))
                {
                    if (AjaxClass.GetCurrency(Convert.ToInt32(Session["currency"]), Convert.ToInt32(DropCurrency.SelectedValue)) == 0)
                    {
                        e.Row.Cells[i].Text = "0";
                    }
                    else
                    {
                        if (e.Row.Cells[i].Text != "")
                        {
                            e.Row.Cells[i].Text = (Convert.ToDouble(e.Row.Cells[i].Text) / AjaxClass.GetCurrency(Convert.ToInt32(Session["currency"]), Convert.ToInt32(DropCurrency.SelectedValue))).ToString("f2");
                        }
                        else
                        {
                            e.Row.Cells[i].Text = "0";
                        }
                    }
                }
                else if (name == GetTran("000249", "扣税"))
                {
                    if (AjaxClass.GetCurrency(Convert.ToInt32(Session["currency"]), Convert.ToInt32(DropCurrency.SelectedValue)) == 0)
                    {
                        e.Row.Cells[i].Text = "0";
                    }
                    else
                    {
                        if (e.Row.Cells[i].Text != "")
                        {
                            e.Row.Cells[i].Text = (Convert.ToDouble(e.Row.Cells[i].Text) / AjaxClass.GetCurrency(Convert.ToInt32(Session["currency"]), Convert.ToInt32(DropCurrency.SelectedValue))).ToString("f2");
                        }
                        else
                        {
                            e.Row.Cells[i].Text = "0";
                        }
                    }
                }
                else if (name == GetTran("000251", "扣款"))
                {
                    if (AjaxClass.GetCurrency(Convert.ToInt32(Session["currency"]), Convert.ToInt32(DropCurrency.SelectedValue)) == 0)
                    {
                        e.Row.Cells[i].Text = "0";
                    }
                    else
                    {
                        if (e.Row.Cells[i].Text != "")
                        {
                            e.Row.Cells[i].Text = (Convert.ToDouble(e.Row.Cells[i].Text) / AjaxClass.GetCurrency(Convert.ToInt32(Session["currency"]), Convert.ToInt32(DropCurrency.SelectedValue))).ToString("f2");
                        }
                        else
                        {
                            e.Row.Cells[i].Text = "0";
                        }
                    }
                }
                else if (name == GetTran("000254", "实发"))
                {
                    if (AjaxClass.GetCurrency(Convert.ToInt32(Session["currency"]), Convert.ToInt32(DropCurrency.SelectedValue)) == 0)
                    {
                        e.Row.Cells[i].Text = "0";
                    }
                    else
                    {
                        if (e.Row.Cells[i].Text != "")
                        {
                            e.Row.Cells[i].Text = (Convert.ToDouble(e.Row.Cells[i].Text) / AjaxClass.GetCurrency(Convert.ToInt32(Session["currency"]), Convert.ToInt32(DropCurrency.SelectedValue))).ToString("f2");
                        }
                        else
                        {
                            e.Row.Cells[i].Text = "0";
                        }
                    }
                }
                else if (name == GetTran("000951", "总计累计"))
                {
                    if (AjaxClass.GetCurrency(Convert.ToInt32(Session["currency"]), Convert.ToInt32(DropCurrency.SelectedValue)) == 0)
                    {
                        e.Row.Cells[i].Text = "0";
                    }
                    else
                    {
                        if (e.Row.Cells[i].Text != "")
                        {
                            e.Row.Cells[i].Text = (Convert.ToDouble(e.Row.Cells[i].Text) / AjaxClass.GetCurrency(Convert.ToInt32(Session["currency"]), Convert.ToInt32(DropCurrency.SelectedValue))).ToString("f2");
                        }
                        else
                        {
                            e.Row.Cells[i].Text = "0";
                        }
                    }
                }
                else if (name == GetTran("000953", "实发累计"))
                {
                    if (AjaxClass.GetCurrency(Convert.ToInt32(Session["currency"]), Convert.ToInt32(DropCurrency.SelectedValue)) == 0)
                    {
                        e.Row.Cells[i].Text = "0";
                    }
                    else
                    {
                        if (e.Row.Cells[i].Text != "")
                        {
                            e.Row.Cells[i].Text = (Convert.ToDouble(e.Row.Cells[i].Text) / AjaxClass.GetCurrency(Convert.ToInt32(Session["currency"]), Convert.ToInt32(DropCurrency.SelectedValue))).ToString("f2");
                        }
                        else
                        {
                            e.Row.Cells[i].Text = "0";
                        }
                    }
                }
                else if (name == GetTran("007598", "董事"))
                {
                    string sql = "select levelstr from bsco_level where levelflag=2 and levelint=" + e.Row.Cells[i].Text;
                    e.Row.Cells[i].Text = DAL.DBHelper.ExecuteScalar(sql) == null ? " " : DAL.DBHelper.ExecuteScalar(sql).ToString();
                }
                else if (name == GetTran("007273", "资格"))
                {
                    string sql = "select levelstr from bsco_level where levelflag=0 and levelint=" + e.Row.Cells[i].Text;
                    e.Row.Cells[i].Text = DAL.DBHelper.ExecuteScalar(sql) == null ? " " : DAL.DBHelper.ExecuteScalar(sql).ToString();
                }
                else if (name == GetTran("007460", "总监"))
                {
                    string sql = "select levelstr from bsco_level where levelflag=3 and levelint=" + e.Row.Cells[i].Text;
                    e.Row.Cells[i].Text = DAL.DBHelper.ExecuteScalar(sql) == null ? " " : DAL.DBHelper.ExecuteScalar(sql).ToString();
                }
                else if (name == GetTran("000936", "新网分数") || name == GetTran("000937", "总网分数") || name == GetTran("000939", "新个分数") ||
                    name == GetTran("000940", "总个分数") || name == GetTran("000942", "未付款的总个分数") || name == GetTran("000944", "未付款的总网分数"))
                {
                    e.Row.Cells[i].Text = Convert.ToDouble(e.Row.Cells[i].Text.ToString()).ToString("f2");
                }

            }

            while (myEnumerator.MoveNext())
            {
                e.Row.Cells[(int)myEnumerator.Key].Attributes.Add("style", "vnd.ms-excel.numberformat:@");

            }
            IDictionaryEnumerator myEnumerator1 = table.GetEnumerator();
            while (myEnumerator1.MoveNext())
            {
                e.Row.Cells[(int)myEnumerator1.Key].HorizontalAlign = HorizontalAlign.Right;
            }
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            IDictionaryEnumerator myEnumerator = table.GetEnumerator();

            while (myEnumerator.MoveNext())
            {


                if (AjaxClass.GetCurrency(Convert.ToInt32(Session["currency"]), Convert.ToInt32(DropCurrency.SelectedValue)) == 0)
                {
                    e.Row.Cells[(int)myEnumerator.Key].Text = "0";
                }
                else
                {
                    e.Row.Cells[(int)myEnumerator.Key].Text = (Convert.ToDouble(myEnumerator.Value) / AjaxClass.GetCurrency(Convert.ToInt32(Session["currency"]), Convert.ToInt32(DropCurrency.SelectedValue))).ToString("f2");
                }
                e.Row.Cells[(int)myEnumerator.Key].HorizontalAlign = HorizontalAlign.Right;
                string name = dt2.Columns[(int)myEnumerator.Key].ColumnName;
                if (name == GetTran("000933", "总网人数") || name == GetTran("000934", "新网人数"))
                {
                    e.Row.Cells[(int)myEnumerator.Key].Text = Convert.ToDouble(e.Row.Cells[(int)myEnumerator.Key].Text).ToString();
                }
            }

            e.Row.Cells[0].Text = GetTran("000630", "合计");

            if (GridView1.PageIndex == GridView1.PageCount - 1)
            {
                GridView1.ShowFooter = true;
            }
            else
            {
                GridView1.ShowFooter = false;
            }
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {

                e.Row.Cells[i].Text = "&nbsp;&nbsp;&nbsp;" + e.Row.Cells[i].Text + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

            }

            e.Row.Attributes.Add("style", "background-image:url('images/tabledp.gif')");


        }

    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortstr = "";
        if (blsort)
        {
            sortstr = e.SortExpression + " desc";
            blsort = false;
        }
        else
        {
            sortstr = e.SortExpression + " asc";
            blsort = true;
        }
        System.Data.DataView dv = new DataView(DAL.DBHelper.ExecuteDataTable(ViewState["sql"].ToString()));
        dv.Sort = sortstr;
        this.GridView1.DataSource = dv;
        this.GridView1.DataBind();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GridView1.PageIndex = e.NewPageIndex;

        //重新绑定数据
        GetList(ViewState["sql"].ToString(), ViewState["AdvanceCount"].ToString());
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    /// <summary>
    /// 首页方法
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        if (PageIndex != 1 || PageIndex < 0)
        {
            PageIndex = 1;
            this.dropPageList.SelectedValue = PageIndex - 1 + "";
            GetList(ViewState["sql"].ToString(), ViewState["AdvanceCount"].ToString());
        }
    }
    /// <summary>
    /// 上一页方法
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPre_Click(object sender, EventArgs e)
    {
        if (PageIndex > 0 && PageIndex != 1)
        {
            PageIndex--;
            this.dropPageList.SelectedValue = PageIndex - 1 + "";
            GetList(ViewState["sql"].ToString(), ViewState["AdvanceCount"].ToString());
        }
    }
    /// <summary>
    /// 下一页方法
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (PageIndex < PageCount && PageIndex != PageCount)
        {
            PageIndex++;
            this.dropPageList.SelectedValue = PageIndex - 1 + "";
            GetList(ViewState["sql"].ToString(), ViewState["AdvanceCount"].ToString());
        }
    }
    /// <summary>
    /// 尾页方法
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLast_Click(object sender, EventArgs e)
    {
        if (PageIndex != PageCount || PageIndex > PageCount)
        {
            PageIndex = PageCount;
            this.dropPageList.SelectedValue = PageIndex - 1 + "";
            GetList(ViewState["sql"].ToString(), ViewState["AdvanceCount"].ToString());
        }
    }

    public int PageIndex
    {
        get { return Convert.ToInt32(this.pageIndex.Value); }
        set { this.pageIndex.Value = value.ToString(); }
    }

    public int RCount
    {
        get { return Convert.ToInt32(this.rCount.Value); }
        set { this.rCount.Value = value.ToString(); }
    }
    public int PageCount
    {
        get { return Convert.ToInt32(this.pageCount.Value); }
        set { this.pageCount.Value = value.ToString(); }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string sql = ViewState["sql"].ToString();
        sql = sql.Replace("|*|", " " + (PageIndex - 1) * 10 + " ");
        DataTable dt = DAL.DBHelper.ExecuteDataTable(sql);
        if (dt == null)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language=>alert('" + GetTran("001085", "没有数据可导出") + "')</script>");
            return;
        }

        if (dt.Rows.Count < 1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language=>alert('" + GetTran("001085", "没有数据可导出") + "')</script>");
            return;
        }

        this.GridView1.AllowPaging = false;
        this.GridView1.AllowSorting = false;
        GetList(ViewState["sql"].ToString(), true);

        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "utf-8";
        Response.AppendHeader("Content-Disposition", "attachment;filename=HelloAdmin.xls;charset=utf-8");
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");//设置输出流为简体中文
        Response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。 
        this.EnableViewState = false;
        System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("ZH-CN", true);
        System.IO.StringWriter oStringWriter = new System.IO.StringWriter(myCItrad);
        System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
        this.GridView1.RenderControl(oHtmlTextWriter);
        Response.Write(oStringWriter.ToString());
        Response.End();

        GridView1.AllowPaging = true;
        this.GridView1.AllowSorting = true;
    }
    protected void dropPageList_SelectedIndexChanged(object sender, EventArgs e)
    {
        PageIndex = int.Parse(this.dropPageList.SelectedValue) + 1;
        GetList(ViewState["sql"].ToString(), ViewState["AdvanceCount"].ToString());
    }
    protected void DropCurrency_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetList(ViewState["sql"].ToString(), ViewState["AdvanceCount"].ToString());
    }
}