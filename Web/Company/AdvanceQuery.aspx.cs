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
using BLL.CommonClass;
using System.Text;
using Model.Other;

public partial class Company_AdvanceQuery : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.BalanceDetialQuery);

        Response.Cache.SetExpires(DateTime.Now);

        if (!IsPostBack)
        {
            this.Panel1.Style.Add("display", "none");

            //绑定货币
            if (Session["Default_Currency"] == null) Session["Default_Currency"] = "";


            CommonDataBLL.BindCurrency_IDListBZ(this.DropCurrency, Session["Default_Currency"].ToString());

            double dpCurrency = AjaxClass.GetCurrency(Convert.ToInt32(DropCurrency.SelectedValue), Convert.ToInt32(DropCurrency.SelectedValue));

            Session["rate"] = dpCurrency;
            getTypeBind(Session["Company"].ToString());
            coll();
        }
        Translations();
    }
    #region 翻译
    private void Translations()
    {
        this.Button2.Text = GetTran("007555", "删除查询记录");
        this.Button1.Text = GetTran("007556", "添加查询记录");
        this.TranControls(this.DropDownRelation,
                new string[][]{
                    new string []{"000851","包含"},
                    new string []{"000853","不包含"},
                    new string []{"000361","大于"},
                    new string []{"000367","小于"},
                    new string []{"000364","大于等于"},
                    new string []{"000368","小于等于"},
                    new string []{"000372","等于"}
                });

        this.TranControls(this.DropDownList1, new string[][]{
                    new string []{"000719","并且"},
                    new string []{"007557","或者"}});


        this.TranControls(this.DropDownRelation2,
            new string[][]{
                    new string []{"000851","包含"},
                    new string []{"000853","不包含"},
                    new string []{"000361","大于"},
                    new string []{"000367","小于"},
                    new string []{"000364","大于等于"},
                    new string []{"000368","小于等于"},
                    new string []{"000372","等于"}
                });

        this.TranControls(this.DropDownOrderKind,
                new string[][]{
                    new string []{"000860","正序"},
                    new string []{"000862","返序"}

                });

        this.TranControls(this.BtnCancelAll, new string[][] { new string[] { "000866", "重 选" } });
        this.TranControls(this.BtnCheckAll, new string[][] { new string[] { "000869", "全 选" } });

    }
    #endregion
    //构建查询条件集合
    private void coll()
    {
        //增加查询项

        ArrayList retVal = QueryInfo.getList(Convert.ToDouble(Session["rate"]));

        //绑定到查询选择块中
        DataConditions.DataSource = retVal;
        DataConditions.DataBind();

        //绑定到查询条件下拉框中
        DropDownCondition.DataSource = retVal;
        DropDownCondition.DataTextField = "Name";
        DropDownCondition.DataValueField = "Key";
        DropDownCondition.DataBind();
        DropDownCondition2.DataSource = retVal;
        DropDownCondition2.DataTextField = "Name";
        DropDownCondition2.DataValueField = "Key";
        DropDownCondition2.DataBind();

        //绑定到排序方式下拉框中
        DropDownOrder.DataSource = retVal;
        DropDownOrder.DataTextField = "Name";
        DropDownOrder.DataValueField = "Key";
        DropDownOrder.DataBind();
    }
    protected void BtnQuery_Click(object sender, EventArgs e)
    {
        if (TextXiaoqu.Text != "")
        {
            ProcessRequest process = new ProcessRequest();
            if (process.ProcessSqlStr(this.TextXiaoqu.Text) == false)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000873", "编号有误") + "！');</script>");
                return;
            }
            if (QueryInfo.IsNumberExist(this.TextXiaoqu.Text, DropDownQiShu.ExpectNum) == false)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000710", "编号不存在") + "！');</script>");
                return;
            }
        }
        //根据用户的选择组合字符串
        string sql = GetSqlString();

        if (sql.Trim() != "")
        {

            //保存标准汇率的ID
            Session["currency"] = DropCurrency.SelectedValue;
            Session["gaojiSql"] = sql;

            Response.Redirect("AdvanceQueryView.aspx?CurGrass=" + DropDownQiShu.ExpectNum);
        }
    }
    //根据用户的选择组合字符串
    private string GetSqlString()
    {
        StringBuilder str = new StringBuilder();
        StringBuilder columnname = new StringBuilder();
        columnname.Append("");

        str.Append("SELECT distinct top 10 ");

        //如果用户什么都不选中，则不需要执行Sql语句
        bool needSelect = false;
        int count = 0;
        ArrayList coll = QueryInfo.getList(Convert.ToDouble(Session["rate"]));
        Hashtable table = new Hashtable();

        foreach (DataListItem item in DataConditions.Items)
        {
            CheckBox check = (CheckBox)item.FindControl("Check");
            if (check.Checked == true)
            {
                columnname.Append(check.Text + ",");
                str.Append(((HtmlInputHidden)item.FindControl("HidValue")).Value);
                str.Append(" as '");
                str.Append(check.Text);
                str.Append("',");

                QueryKey key = (QueryKey)coll[count];
                if (key.NeedCount == true)
                {
                    table.Add(key.Name, key.CountType);
                }

                needSelect = true;
            }

            count++;
        }
        //添加记录
        //QueryInfo.IsExist(columnname.ToString(), Session["Company"].ToString());


        //如果需要组合字符串（就是用户至少选择了一列要显示的数据），才组合字符串
        if (needSelect)
        {
            //移除最后一个逗号
            str.Remove(str.Length - 1, 1);

            string columns = str.ToString();
            string tables = "";
            string conditions = "";

            //选择要读取的表
            #region 表组合

            str.Append(" FROM ");
            if (columns.IndexOf("mi.") != -1)
            {
                tables += "MemberInfo mi,";
            }
            if (columns.IndexOf("mn.") != -1)
            {
                if (DropDownQiShu.ExpectNum != -1)
                {
                    Session["ADVQS"] = DropDownQiShu.ExpectNum;
                }
                else
                {
                    Session["ADVQS"] = CommonDataBLL.getMaxqishu();
                }
                tables += "MemberInfoBalance" + DropDownQiShu.ExpectNum + " mn,";
            }
            if (columns.IndexOf("cfg.") != -1)
            {
                tables += "Config cfg,";
            }
            if (columns.IndexOf("p.") != -1)
            {
                tables += "bsco_PaperType p,";
            }
            if (columns.IndexOf("lv.") != -1)
            {
                tables += "BSCO_level lv,";
                conditions += "lv.levelflag=0";
            }
            if (columns.IndexOf("d.") != -1)
            {
                tables += "Memberinfo d,";
            }
            if (columns.IndexOf("mp.") != -1)
            {
                tables += "Memberinfo mp,";
            }

            #endregion

            #region 条件组合

            if (conditions.Trim().Length == 0)
                conditions = "1=1";
            if (tables.IndexOf("mi,") != -1 && tables.IndexOf("mn,") != -1)
            {
                conditions += " and mi.Number=mn.number";
            }
            if (tables.IndexOf("cfg,") != -1)
            {
                conditions += " and cfg.ExpectNum=" + DropDownQiShu.ExpectNum;
            }
            if (tables.IndexOf("mi,") != -1 && tables.IndexOf("p,") != -1)
            {
                conditions += " and mi.papertypecode=p.papertypecode";
            }
            if (tables.IndexOf("mn,") != -1 && tables.IndexOf("lv,") != -1)
            {
                conditions += " and mn.level=lv.levelint";
            }
            if (tables.IndexOf("d,") != -1 && tables.IndexOf("mi,") != -1)
            {
                conditions += " and d.number=mi.direct";
            }
            if (tables.IndexOf("mp,") != -1 && tables.IndexOf("mi,") != -1)
            {
                conditions += " and mp.number=mi.placement";
            }
            if (tables.IndexOf("mi,") != -1 && tables.IndexOf("lv,") != -1)
            {
                conditions += " and mi.LevelInt=lv.LevelInt";
            }

            #endregion

            //辖区总览
            if (TextXiaoqu.Text != "")
            {
                tables += "(select distinct number from [GetTeams](1,'" + TextXiaoqu.Text + "')) H1,";
                if (tables.IndexOf("mn,") != -1 || tables.IndexOf("mi,") != -1)
                {
                    if (tables.IndexOf("mn,") != -1)
                        conditions += " and mn.number=H1.number";
                    else
                        conditions += " and mi.number=H1.number";
                }
            }


            tables = tables.Substring(0, tables.Length - 1);
            str.Append(tables);

            StringBuilder str1 = new StringBuilder();
            str1.Append(conditions);

            Boolean flagStr = true;
            TextContent.Text = TextContent.Text.Trim();
            if (TextContent.Text != "")
            {
                //如果用户填了查询内容，则增加查询条件
                str1.Append(" and ");

                //根据用户选择条件读取关系
                #region 条件一
                switch (DropDownRelation.SelectedValue)
                {
                    case "like":
                        str1.Append(DropDownCondition.SelectedValue);
                        str1.Append(" like '%");
                        str1.Append(TextContent.Text);
                        str1.Append("%'");
                        break;
                    case "not like":
                        str1.Append(DropDownCondition.SelectedValue);
                        str1.Append(" not like '%");
                        str1.Append(TextContent.Text);
                        str1.Append("%'");
                        break;
                    case ">":
                        str1.Append(DropDownCondition.SelectedValue);
                        str1.Append(" > ");
                        if (DropDownCondition.SelectedValue == "mi.RegisterDate" || DropDownCondition.SelectedValue == "dateadd(hh,8,mi.advtime)" || DropDownCondition.SelectedValue == "mi.Birthday" || DropDownCondition.SelectedValue == "cfg.[Date]")
                        {
                            str1.Append("'");
                            if (DropDownCondition.SelectedValue == "dateadd(hh,8,mi.advtime)")
                            {
                                str1.Append(DateTime.Parse(TextContent.Text).AddHours(-8).ToString());
                            }
                            else
                            {
                                str1.Append(TextContent.Text);
                            }
                            str1.Append("'");
                        }
                        else
                        {
                            str1.Append(TextContent.Text);
                        }
                        flagStr = CheckError(TextContent.Text);
                        break;
                    case "<":
                        str1.Append(DropDownCondition.SelectedValue);
                        str1.Append(" < ");
                        if (DropDownCondition.SelectedValue == "mi.RegisterDate" || DropDownCondition.SelectedValue == "dateadd(hh,8,mi.advtime)" || DropDownCondition.SelectedValue == "mi.Birthday" || DropDownCondition.SelectedValue == "cfg.[Date]")
                        {
                            str1.Append("'");
                            if (DropDownCondition.SelectedValue == "dateadd(hh,8,mi.advtime)")
                            {
                                str1.Append(DateTime.Parse(TextContent.Text).AddHours(-8).ToString());
                            }
                            else
                            {
                                str1.Append(TextContent.Text);
                            }
                            str1.Append("'");
                        }
                        else
                        {
                            str1.Append(TextContent.Text);
                        }

                        flagStr = CheckError(TextContent.Text);
                        break;
                    case ">=":
                        str1.Append(DropDownCondition.SelectedValue);
                        str1.Append(" >= ");
                        if (DropDownCondition.SelectedValue == "mi.RegisterDate" || DropDownCondition.SelectedValue == "dateadd(hh,8,mi.advtime)" || DropDownCondition.SelectedValue == "mi.Birthday" || DropDownCondition.SelectedValue == "cfg.[Date]")
                        {
                            str1.Append("'");
                            if (DropDownCondition.SelectedValue == "dateadd(hh,8,mi.advtime)")
                            {
                                str1.Append(DateTime.Parse(TextContent.Text).AddHours(-8).ToString());
                            }
                            else
                            {
                                str1.Append(TextContent.Text);
                            }
                            str1.Append("'");
                        }
                        else
                        {
                            str1.Append(TextContent.Text);
                        }

                        flagStr = CheckError(TextContent.Text);
                        break;
                    case "<=":
                        str1.Append(DropDownCondition.SelectedValue);
                        str1.Append(" <= ");
                        if (DropDownCondition.SelectedValue == "mi.RegisterDate" || DropDownCondition.SelectedValue == "dateadd(hh,8,mi.advtime)" || DropDownCondition.SelectedValue == "mi.Birthday" || DropDownCondition.SelectedValue == "cfg.[Date]")
                        {
                            str1.Append("'");
                            if (DropDownCondition.SelectedValue == "dateadd(hh,8,mi.advtime)")
                            {
                                str1.Append(DateTime.Parse(TextContent.Text).AddHours(-8).ToString());
                            }
                            else
                            {
                                str1.Append(TextContent.Text);
                            }
                            str1.Append("'");
                        }
                        else
                        {
                            str1.Append(TextContent.Text);
                        }

                        flagStr = CheckError(TextContent.Text);
                        break;
                    case "=":
                        str1.Append(DropDownCondition.SelectedValue);
                        str1.Append(" = ");
                        if (DropDownCondition.SelectedValue == "mi.RegisterDate" || DropDownCondition.SelectedValue == "dateadd(hh,8,mi.advtime)" || DropDownCondition.SelectedValue == "mi.Birthday" || DropDownCondition.SelectedValue == "cfg.[Date]")
                        {
                            str1.Append("'");
                            if (DropDownCondition.SelectedValue == "dateadd(hh,8,mi.advtime)")
                            {
                                str1.Append(DateTime.Parse(TextContent.Text).AddHours(-8).ToString());
                            }
                            else
                            {
                                str1.Append(TextContent.Text);
                            }
                            str1.Append("'");
                        }
                        else
                        {
                            str1.Append(TextContent.Text);
                        }

                        flagStr = CheckError(TextContent.Text);
                        break;
                }
                #endregion

                if (TextContent2.Text.Trim() != "")
                {
                    str1.Append(DropDownList1.SelectedValue);

                    #region 条件二
                    switch (DropDownRelation2.SelectedValue)
                    {
                        case "like":
                            str1.Append(DropDownCondition2.SelectedValue);
                            str1.Append(" like '%");
                            str1.Append(TextContent2.Text);
                            str1.Append("%'");
                            break;
                        case "not like":
                            str1.Append(DropDownCondition2.SelectedValue);
                            str1.Append(" not like '%");
                            str1.Append(TextContent2.Text);
                            str1.Append("%'");
                            break;
                        case ">":
                            str1.Append(DropDownCondition2.SelectedValue);
                            str1.Append(" > ");
                            if (DropDownCondition2.SelectedValue == "mi.RegisterDate" || DropDownCondition2.SelectedValue == "dateadd(hh,8,mi.advtime)" || DropDownCondition2.SelectedValue == "mi.Birthday" || DropDownCondition2.SelectedValue == "cfg.[Date]")
                            {
                                str1.Append("'");
                                if (DropDownCondition2.SelectedValue == "dateadd(hh,8,mi.advtime)")
                                {
                                    str1.Append(DateTime.Parse(TextContent2.Text).AddHours(-8).ToString());
                                }
                                else
                                {
                                    str1.Append(TextContent2.Text);
                                }
                                str1.Append("'");
                            }
                            else
                            {
                                str1.Append(TextContent2.Text);
                            }
                            flagStr = CheckError(TextContent2.Text);
                            break;
                        case "<":
                            str1.Append(DropDownCondition2.SelectedValue);
                            str1.Append(" < ");
                            if (DropDownCondition2.SelectedValue == "mi.RegisterDate" || DropDownCondition2.SelectedValue == "dateadd(hh,8,mi.advtime)" || DropDownCondition2.SelectedValue == "mi.Birthday" || DropDownCondition2.SelectedValue == "cfg.[Date]")
                            {
                                str1.Append("'");
                                if (DropDownCondition2.SelectedValue == "dateadd(hh,8,mi.advtime)")
                                {
                                    str1.Append(DateTime.Parse(TextContent2.Text).AddHours(-8).ToString());
                                }
                                else
                                {
                                    str1.Append(TextContent2.Text);
                                }
                                str1.Append("'");
                            }
                            else
                            {
                                str1.Append(TextContent2.Text);
                            }

                            flagStr = CheckError(TextContent2.Text);
                            break;
                        case ">=":
                            str1.Append(DropDownCondition2.SelectedValue);
                            str1.Append(" >= ");
                            if (DropDownCondition2.SelectedValue == "mi.RegisterDate" || DropDownCondition2.SelectedValue == "dateadd(hh,8,mi.advtime)" || DropDownCondition2.SelectedValue == "mi.Birthday" || DropDownCondition2.SelectedValue == "cfg.[Date]")
                            {
                                str1.Append("'");
                                if (DropDownCondition2.SelectedValue == "dateadd(hh,8,mi.advtime)")
                                {
                                    str1.Append(DateTime.Parse(TextContent2.Text).AddHours(-8).ToString());
                                }
                                else
                                {
                                    str1.Append(TextContent2.Text);
                                }
                                str1.Append("'");
                            }
                            else
                            {
                                str1.Append(TextContent2.Text);
                            }

                            flagStr = CheckError(TextContent2.Text);
                            break;
                        case "<=":
                            str1.Append(DropDownCondition2.SelectedValue);
                            str1.Append(" <= ");
                            if (DropDownCondition2.SelectedValue == "mi.RegisterDate" || DropDownCondition2.SelectedValue == "dateadd(hh,8,mi.advtime)" || DropDownCondition2.SelectedValue == "mi.Birthday" || DropDownCondition2.SelectedValue == "cfg.[Date]")
                            {
                                str1.Append("'");
                                if (DropDownCondition2.SelectedValue == "dateadd(hh,8,mi.advtime)")
                                {
                                    str1.Append(DateTime.Parse(TextContent2.Text).AddHours(-8).ToString());
                                }
                                else
                                {
                                    str1.Append(TextContent2.Text);
                                }
                                str1.Append("'");
                            }
                            else
                            {
                                str1.Append(TextContent2.Text);
                            }

                            flagStr = CheckError(TextContent2.Text);
                            break;
                        case "=":
                            str1.Append(DropDownCondition2.SelectedValue);
                            str1.Append(" = ");
                            if (DropDownCondition2.SelectedValue == "mi.RegisterDate" || DropDownCondition2.SelectedValue == "mi.Birthday" || DropDownCondition2.SelectedValue == "cfg.[Date]")
                            {
                                str1.Append("'");
                                if (DropDownCondition2.SelectedValue == "dateadd(hh,8,mi.advtime)")
                                {
                                    str1.Append(DateTime.Parse(TextContent2.Text).AddHours(-8).ToString());
                                }
                                else
                                {
                                    str1.Append(TextContent2.Text);
                                }
                                str1.Append("'");
                            }
                            else
                            {
                                str1.Append(TextContent2.Text);
                            }

                            flagStr = CheckError(TextContent2.Text);
                            break;
                    }
                    #endregion
                }
                if (flagStr == false)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("000878", "对不起，指定查询条件错误只有日期和数字才能进行大于、小于和等于的判断") + "')</script>");
                    return "";
                }
            }
            else
            {
                //判断用户是否进行关键词搜索
                TextBox1.Text = TextBox1.Text.Trim();

                if (TextBox1.Text != "")
                {
                    str1.Append(" and ");
                    str1.Append("(mi.Number like '%" + TextBox1.Text + "%' or ");
                    str1.Append("mi.Name like '%" + TextBox1.Text + "%' or ");
                    str1.Append("mi.PaperNumber like '%" + TextBox1.Text + "%') ");
                }
            }
            str.Append(" where " + str1.ToString());

            str.Append(" and ");
            str.Append(DropDownOrder.SelectedValue);
            str.Append(" not in");


            str.Append(" (select top |*| ");
            str.Append(DropDownOrder.SelectedValue);

            str.Append(" FROM " + tables);
            str.Append(" where " + str1.ToString());

            //增加排序功能
            str.Append(" order by ");
            str.Append(DropDownOrder.SelectedValue);
            str.Append(" ");
            str.Append(DropDownOrderKind.SelectedValue);

            str.Append(" )");
            //增加排序功能
            str.Append(" order by ");
            str.Append(DropDownOrder.SelectedValue);
            str.Append(" ");
            str.Append(DropDownOrderKind.SelectedValue);

            str = str.Replace("&lt;", "<");
            str = str.Replace("&gt;", ">");
            str = str.Replace("&lt", "<");

            StringBuilder str2 = new StringBuilder();
            str2.Append("select count(1) ");
            str2.Append(" FROM " + tables);
            str2.Append(" where " + str1);

            Session["AdvanceCount"] = str2;

            str = str.Replace("&lt;", "<");
            str = str.Replace("&gt;", ">");
            str = str.Replace("&lt", "<");

            return str.ToString().Replace("and dateadd(hh,8,MemberInfo.advtime)", "and MemberInfo.advtime");
        }
        else
        {
            return "";
        }
    }
    //检查用户指定的字段是否能够转换成为数字和
    private Boolean CheckError(string content)
    {
        int tempInt = 0;
        DateTime dt = DateTime.UtcNow;
        Boolean flag = int.TryParse(content, out tempInt);
        Boolean flag2 = DateTime.TryParse(content, out dt);
        if (flag == false && flag2 == false)
        {
            return false;
        }
        else
        {
            return true;
        }

    }
    protected void BtnCancelAll_Click(object sender, EventArgs e)
    {
        foreach (DataListItem item in DataConditions.Items)
        {
            if (((CheckBox)item.FindControl("Check")).Text != GetTran("000024", "会员编号"))
                ((CheckBox)item.FindControl("Check")).Checked = false;
        }
    }
    protected void BtnCheckAll_Click(object sender, EventArgs e)
    {
        foreach (DataListItem item in DataConditions.Items)
        {
            ((CheckBox)item.FindControl("Check")).Checked = true;
        }
    }
    protected void lkSubmit_Click(object sender, EventArgs e)
    {
        BtnQuery_Click(null, null);
    }
    protected void DropCurrency_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.coll();
        Session["Default_Currency"] = DropCurrency.SelectedItem.Text;
    }

    public void insert()
    {
        if (TextBox2.Text.Trim() == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "success", "alert('" + GetTran("007193", "请输入记录名！") + "');", true);
            return;
        }
        else
        {
            bool bb = QueryInfo.IsExistTypeName(TextBox2.Text.Trim(), Session["Company"].ToString());
            if (!bb)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "success", "alert('" + GetTran("007194", "该记录名已经存在！") + "');", true);
                return;
            }
        }
        StringBuilder columnname = new StringBuilder();     //获取查询列
        foreach (DataListItem item in DataConditions.Items)
        {
            CheckBox check = (CheckBox)item.FindControl("Check");
            if (check.Checked == true)
            {
                columnname.Append(check.Text + ",");
            }
        }

        StringBuilder sqlwhere = new StringBuilder();       //获取查询条件
        sqlwhere.Append("condition:" + DropDownCondition.SelectedValue + "," + DropDownRelation.SelectedValue + "," + TextContent.Text.Trim());
        if (TextContent2.Text.Trim() != "")
        {
            sqlwhere.Append(";" + DropDownList1.SelectedValue + ";" + "condition2:" + DropDownCondition2.SelectedValue + "," + DropDownRelation2.SelectedValue + "," + TextContent2.Text.Trim());
        }

        StringBuilder orderby = new StringBuilder();        //获取排序字段
        orderby.Append(DropDownOrder.SelectedValue + "," + DropDownOrderKind.SelectedValue);

        //添加记录
        bool b = QueryInfo.IsExist(columnname.ToString(), sqlwhere.ToString(), orderby.ToString(), Session["Company"].ToString(), TextBox2.Text);
        if (b)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "success", "alert('" + GetTran("000006", "添加成功！") + "');window.location='AdvanceQuery.aspx';", true);
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "success", "alert('" + GetTran("000007", "添加失败！") + "');", true);
        }
    }

    public void getTypeBind(string number)
    {
        DataTable dt = QueryInfo.QueryTypeName(number);
        DropDownList2.DataTextField = "type";
        DropDownList2.DataValueField = "id";
        DropDownList2.DataSource = dt;
        DropDownList2.DataBind();

        ListItem li = new ListItem(GetTran("000303", "请选择"), "-1");
        DropDownList2.Items.Insert(0, li);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        insert();
    }

    public void checkbox(string id)
    {
        DataTable dt = QueryInfo.QueryQueryColumnsInfo(id);
        if (dt.Rows.Count > 0)
        {
            #region 选中列
            string cloumns = dt.Rows[0]["ColumnName"].ToString();
            cloumns = cloumns.Substring(0, cloumns.Length - 1);
            string[] cloumn = cloumns.Split(',');
            foreach (DataListItem item in DataConditions.Items)
            {
                CheckBox check = (CheckBox)item.FindControl("Check");
                for (int i = 0; i < cloumn.Length; i++)
                {
                    if (check.Text == cloumn[i])
                    {
                        check.Checked = true;
                    }
                }
            }
            #endregion
            #region 选中条件

            string sqlwhere = dt.Rows[0]["sqlwhere"].ToString();
            string[] condition = sqlwhere.Split(';');
            if (condition[0].Length != 0)
            {
                condition[0] = condition[0].Substring(0, condition[0].Length - 1);
            }
            string[] condition1 = condition[0].Split(',');
            if (condition1.Length == 3)
            {
                TextContent.Text = condition1[2];
            }
            foreach (ListItem li1 in DropDownCondition.Items)
            {
                string aa = condition1[0].Substring(condition1[0].IndexOf(":"));
                if (li1.Value == condition1[0].Substring(condition1[0].IndexOf(":") + 1))
                {
                    li1.Selected = true;
                }
                else
                {
                    li1.Selected = false;
                }
            }
            foreach (ListItem li2 in DropDownRelation.Items)
            {
                if (li2.Value == condition1[1])
                {
                    li2.Selected = true;
                }
                else
                {
                    li2.Selected = false;
                }
            }
            if (condition.Length > 1)
            {
                foreach (ListItem li3 in DropDownList1.Items)
                {
                    if (li3.Value == condition[1])
                    {
                        li3.Selected = true;
                    }
                    else
                    {
                        li3.Selected = false;
                    }
                }

                string[] condition2 = condition[2].Split(',');
                if (condition2.Length == 3)
                {
                    TextContent2.Text = condition2[2];
                }
                foreach (ListItem li4 in DropDownCondition2.Items)
                {
                    if (li4.Value == condition2[0].Substring(condition2[0].IndexOf(":") + 1))
                    {
                        li4.Selected = true;
                    }
                    else
                    {
                        li4.Selected = false;
                    }
                }
                foreach (ListItem li5 in DropDownRelation2.Items)
                {
                    if (li5.Value == condition2[1])
                    {
                        li5.Selected = true;
                    }
                    else
                    {
                        li5.Selected = false;
                    }
                }
            }
            #endregion

            #region 排序
            string orderby = dt.Rows[0]["orderby"].ToString();
            string[] order = orderby.Split(',');
            foreach (ListItem item in DropDownOrder.Items)
            {
                if (item.Value == order[0])
                {
                    item.Selected = true;
                }
                else
                {
                    item.Selected = false;
                }
            }
            foreach (ListItem item in DropDownOrderKind.Items)
            {
                if (item.Value == order[1])
                {
                    item.Selected = true;
                }
                else
                {
                    item.Selected = false;
                }
            }
            #endregion
        }
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList2.SelectedValue != "-1")
        {
            foreach (DataListItem item in DataConditions.Items)
            {
                CheckBox check = (CheckBox)item.FindControl("Check");
                check.Checked = false;
            }
            checkbox(DropDownList2.SelectedValue);
        }
        else
        {
            coll();
        }
    }
    /// <summary>
    /// 删除查询记录
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (DropDownList2.SelectedValue != "-1")
        {
            int num = QueryInfo.deletequeryrecord(Convert.ToInt32(DropDownList2.SelectedValue));
            if (num > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "success", "alert('" + GetTran("000008", "删除成功") + "！');window.location='AdvanceQuery.aspx'", true);
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "success", "alert('" + GetTran("007212", "请先选择查询记录") + "');", true);
        }
    }
}