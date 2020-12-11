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

///Add Namespace
using System.Globalization;
using System.IO;
using System.Text;
using Model;
using Model.Other;
using BLL.CommonClass;
using BLL.other.Company;

/*
 * 创建者：     汪  华
 * 创建时间：   2009-09-17
 * 对应菜单:    系统管理->日志管理
 */


public partial class Company_LogsManagement : BLL.TranslationBase
{
    /// <summary>
    /// 为排序事件声明变量及赋值
    /// </summary>
    public static bool blID = true;
    public static bool blType = true;
    public static bool blTime = true;
    public static bool blSip = true;
    public static bool blCategory = true;
    public static bool blFrom = true;
    public static bool blFromUserType = true;
    public static bool blTo = true;
    public static bool blToUserType = true;
    public static bool blExpectNum = true;

    protected string msgstr;
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        ///检查相应的权限
        Permissions.CheckManagePermission(EnumCompanyPermission.SystemChangeLogsQuery);

        ///设置GridView的样式
        gvDetailsByDay.Attributes.Add("style", "word-break:keep-all;word-wrap:normal");

        //对其查询的列名进行赋值
        columnNames = " ID, case Type when 0 then '" + GetTran("000022", "删除") + "' when 1 then '" + GetTran("000259", "修改") + "' else '' end as Type,Convert(Datetime,[Time]) as [Time], SIP,Category, [From], FromUserType,Case [To] when '' then '" + GetTran("000221", "无") + "' else [To] end as [To], ToUserType, ExpectNum";
   
        if (!IsPostBack)
        {
            CommonDataBLL.BindQishuList(DropDownListqishu, true);
            InitExpect();
            ViewState["pagesort"] = 0;   //首次加载，默认为降序排列
            btnQuery_Click(null, null);
        }
        Translations_More(); 
        
    }
    /// <summary>
    /// 控件翻译
    /// </summary>
    protected void Translations_More()
    {
        TranControls(btnQuery, new string[][] { new string[] { "000048", "查 询" } });
        TranControls(btnReset, new string[][] { new string[] { "001081", "清 除" } });
        TranControls(btnBack, new string[][] { new string[] { "003237", "备份日志" } });
        TranControls(btnDel, new string[][] { new string[] { "003238", "删除日志" } });

        TranControls(ddlChangeType, new string[][] 
                        { 
                                new string[] { "000633", "全部" }, 
                                new string[] { "000022", "删除" },
                                new string[] { "000259", "修改" }
                        }
                    );

        TranControls(ddlToType, new string[][] 
                        { 
                                new string[] { "000633", "全部" }, 
                                new string[] { "003223", "公司(管理员)" },
                               
                                new string[] { "000599", "会员" }
                        }
                    );

        TranControls(ddlChangeCategory, new string[][] 
                        { 
                                new string[] { "000633", "全部" }, 
                                new string[] { "000599", "会员" }, 
                                
                                new string[] { "001824", "公司" }, 
                                new string[] { "000638", "报单" }, 
                                new string[] { "003229", "权限(角色)" },
                                new string[] { "003231", "密码" }, 
                                new string[] { "002186", "产品" }
                        }
                    );

        TranControls(ddlOperatorType, new string[][] 
                        { 
                                new string[] { "000633", "全部" }, 
                                new string[] { "003223", "公司(管理员)" },
                                
                                new string[] { "000599", "会员" }
                        }
                    );



        TranControls(gvDetailsByDay, new string[][] 
                        { 
                                new string[] { "000012", "序号" }, 
                                new string[] { "003163", "操作类型" }, 
                                new string[] { "001546", "时间" },
                                new string[] { "000961", "IP地址" }, 
                                new string[] { "003182", "操作部位" }, 
                                new string[] { "003197", "操作者" },
                                new string[] { "003199", "操作者类型" }, 
                                new string[] { "003245", "操作的对象" },
                                new string[] { "003203", "操作对象类型" },
                                new string[] { "000045", "期数" },
                                new string[] { "000035", "详细信息" },
                        }
            );
    }

    /// <summary>
    /// 加载期数
    /// </summary>
    protected void InitExpect()
    {
        this.ddlExpectNum.ExpectNum = CommonDataBLL.GetMaxqishu();
    }


    string queryConditions = "1<2";
    string columnNames = "";
    string orderColumnName = "ID";
    //记录字段的排序
    int pagesort = 0;

    /// <summary>
    /// 给参数赋值
    /// </summary>
    protected void SetValueParameters()
    {
        //每次查询根据ID进行排序
        ViewState["orderColumnName"] = orderColumnName;

        DataTable dt = LogsManageBLL.ChangeLogsQuery(columnNames.ToString(), queryConditions.ToString());
        //给排序事件提供方便
        DataView dv = new DataView(dt);

        gvDetailsByDay.DataSource = dv;
        gvDetailsByDay.DataBind();

        //分页
        this.Pager1.PageBind(0, 10, "ChangeLogs", columnNames, queryConditions, ViewState["orderColumnName"].ToString(), "gvDetailsByDay");                    
    }

    #region 获取操作者或对象类型
    public string GetType(string eName)
    {
        switch (eName)
        {
            case "Company":
                return GetTran("000151", "管理员");
            case "Store":
                return GetTran("000388", "店铺");
            case "Member":
                return GetTran("000599", "服务机构");
            default:
                return GetTran("000151", "管理员");
        }
    }
    #endregion

    public string GetToUserType(string eName)
    {
        switch (eName)
        {

            case "objecttype0":
                return GetTran("000000", "仓库");
            case "objecttype1":
                return GetTran("000000", "产品");
            case "objecttype2":
                return GetTran("000000", "服务机构");
            case "objecttype3":
                return GetTran("000000", "公司");
            case "objecttype4":
                return GetTran("000000", "供应商");
            case "objecttype5":
                return GetTran("000000", "会员");
            case "objecttype6":
                return GetTran("000000", "密码");
            case "objecttype7":
                return GetTran("000000", "权限");
            case "objecttype8":
                return GetTran("000000", "物流公司");
            case "objecttype9":
                return GetTran("000000", "系统参数");
            case "objecttype10":
                return GetTran("000000", "信息");
            case "Company":
                return GetTran("000151", "公司");
            case "Store":
                return GetTran("000388", "店铺");
            case "Member":
                return GetTran("000599", "服务机构");
            default:
                return GetTran("000151", "管理员");
        }
    }


    public string GetCategory(string num)
    { 
        switch(num)
        {
            case "0":
                return GetTran("000000", "会员信息编辑");
            case "1":
                return GetTran("000000", "会员密码重置");
            case "2":
                return GetTran("000000", "批量修改");
            case "3":
                return GetTran("000000", "报单浏览");
            case "4":
                return GetTran("000000", "机构信息编辑");
            case "5":
                return GetTran("000000", "机构密码重置");
            case "6":
                return GetTran("000000", "供应商管理");
            case "7":
                return GetTran("000000", "产品修改");
            case "8":
                return GetTran("000000", "入库审核");
            case "9":
                return GetTran("000000", "第三方物流管理");
            case "10":
                return GetTran("000000", "订单支付");
            case "11":
                return GetTran("000000", "预收帐款");
            case "12":
                return GetTran("000000", "退货款管理");
            case "13":
                return GetTran("000000", "帐户管理");
            case "14":
                return GetTran("000000", "提现审核");
            case "15":
                return GetTran("000000", "结算参数");
            case "16":
                return GetTran("000000", "调控参数");
            case "17":
                return GetTran("000000", "奖金退回");
            case "18":
                return GetTran("000000", "资料管理");
            case "19":
                return GetTran("000000", "公告查询");
            case "20":
                return GetTran("000000", "收件箱");
            case "21":
                return GetTran("000000", "发件箱");
            case "22":
                return GetTran("000000", "废件箱");
            case "23":
                return GetTran("000000", "预设短信");
            case "24":
                return GetTran("000000", "部门管理");
            case "25":
                return GetTran("000000", "角色管理");
            case "26":
                return GetTran("000000", "管理员管理");
            case "27":
                return GetTran("000000", "密码修改");
            case "28":
                return GetTran("000000", "参数设置");
            case "29":
                return GetTran("000000", "系统开关");
            case "30":
                return GetTran("000000", "查询控制");
            case "31":
                return GetTran("000000", "支付设置");
            case "32":
                return GetTran("000000", "数据加密设置");
            case "33":
                return GetTran("000000", "短信网关设置");
            case "34":
                return GetTran("000000", "短信内容预设");
            case "35":
                return GetTran("000000", "各语种翻译");
            case "36":
                return GetTran("000000", "各汇率设置");
            case "37":
                return GetTran("000000", "日期时间设置");
            case "38":
                return GetTran("000000", "国家和地区设置");
            case "39":
                return GetTran("000000", "注册浏览");
            case "40":
                return GetTran("000000", "复消浏览");
            case "41":
                return GetTran("000000", "注册确认");
            case "42":
                return GetTran("000000", "复消确认");
            case "43":
                return GetTran("000000", "批量注册检测");
            case "44":
                return GetTran("000000", "收件箱");
            case "45":
                return GetTran("000000", "发件箱");
            case "46":
                return GetTran("000000", "废件箱");
            case "47":
                return GetTran("000000", "密码修改");
            case "48":
                return GetTran("000000", "服务机构资料修改");
            case "49":
                return GetTran("000000", "订单编辑");
            case "50":
                return GetTran("000000", "注册报单浏览");
            case "51":
                return GetTran("000000", "充值浏览");
            case "52":
                return GetTran("000000", "密码修改");
            case "53":
                return GetTran("000000", "会员资料修改");
            case "54":
                return GetTran("000000", "收件箱");
            case "55":
                return GetTran("000000", "发件箱");
            case "56":
                return GetTran("000000", "废件箱");
            case "57":
                return GetTran("000000", "会员");
            case "58":
                return GetTran("000000", "店铺");
            case "59":
                return GetTran("000000", "公司");
            case "60":
                return GetTran("000000", "批量修改或报单浏览");
            case "61":
                return GetTran("000000", "权限(角色)");
            case "62":
                return GetTran("000000", "密码");
            case "63":
                return GetTran("000000", "产品");
            case "64":
                return GetTran("000000", "奖金公布");
            case "65":
                return GetTran("000000", "短信删除");
            default:
                return "";
       
        }
    }

    /// <summary>
    /// 导出Excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        getCondition();
        DataTable dt = LogsManageBLL.ChangeLogsQuery(columnNames, queryConditions);

        if (dt.Rows.Count < 1)
        {
            this.msgstr = "<script language=>alert('" + GetTran("001085", "没有数据可导出") + "')</script>";
            return;
        }

        else
        {
            Excel.OutToExcel(OutToExcel_Logs(dt), GetTran("003257", "日志信息"), new string[] { "Type=" + GetTran("003163", "操作类型"), "Time=" + GetTran("003259", "操作时间"), "SIP=" + GetTran("000961", "IP地址"), "Category=" + GetTran("003182", "操作类别"), "From=" + GetTran("004065", "来源"), "FromUserType=" + GetTran("004067", "用户类型来源"), "To=" + GetTran("004068", "去向"), "ToUserType=" + GetTran("004070", "用户类型去向"), "ExpectNum=" + GetTran("000045", "期数") });
        }
    }

    
    /// <summary>
    /// 清除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnReset_Click(object sender, EventArgs e)
    {
        (ddlExpectNum.Controls[0] as DropDownList).SelectedValue = "-1";
        ddlChangeType.SelectedValue = "-1";
        ddlToType.SelectedValue = "-1";
        ddlChangeCategory.SelectedValue = "-1";
        ddlOperatorType.SelectedValue = "-1";
        ResetTextValue.ClearTextValue(Page);          
    }

    private DataTable OutToExcel_Logs(DataTable oldDt)
    {
        DataTable dt = new DataTable();
        dt = oldDt.Clone();
        //Update column data type,
        foreach(DataColumn col in dt.Columns)
        {
            if (col.ColumnName == "Type")
            { 
                col.DataType=typeof(String);
            }

            if (col.ColumnName == "Category")
            { 
                col.DataType=typeof(String);
            }
        }

        for (int i = 0; i < oldDt.Rows.Count; i++)
        {
            DataRow newRow = dt.NewRow();
            newRow.ItemArray = oldDt.Rows[i].ItemArray;
            dt.Rows.Add(newRow);

            if (dt.Rows[i]["Type"].ToString() == "0")
            {
                dt.Rows[i]["Type"] = GetTran("000259", "修改");
            }

            if (dt.Rows[i]["Type"].ToString() == "1")
            {
                dt.Rows[i]["Type"] = GetTran("000022", "删除");
            }
        }
        return dt;
    }

    /// <summary>
    /// 备份日志
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBack_Click(object sender, EventArgs e)
    {
        int backupCount = Permissions.GetPermissions(EnumCompanyPermission.SystemChangeLogsBackup);
        if (backupCount != 6115)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("004077", "对不起，您没有备份日志的权限!")));
            return;
        }

        else
        {
            ///向日志备份表中插入相关记录
            LogsManageBLL.AddBackupChangeLogs();
            ///获取日志备份表中信息
            DataTable dt = LogsManageBLL.GetBackupChangeLogsInfo();

            if (dt.Rows.Count < 0)
            {
                this.msgstr = "<script language=>alert('" + GetTran("001085", "没有数据可导出") + "')</script>";
                return;
            }

            else
            {
                Excel.OutToExcel(OutToExcel_Logs(dt), GetTran("003257", "日志信息"), new string[] { "Type=" + GetTran("003163", "操作类型"), "Time=" + GetTran("003259", "操作时间"), "SIP=" + GetTran("000961", "IP地址"), "Category=" + GetTran("003182", "操作类别"), "From=" + GetTran("004065", "来源"), "FromUserType=" + GetTran("004067", "用户类型来源"), "To=" + GetTran("004068", "去向"), "ToUserType=" + GetTran("004070", "用户类型去向"), "ExpectNum=" + GetTran("000045", "期数") });
            }
        }
    }
    
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        getCondition();
        ///给参数赋值
        SetValueParameters();        
    }

    /// <summary>
    /// 删除系统日志
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDel_Click(object sender, EventArgs e)
    {
        int deleteCount = Permissions.GetPermissions(EnumCompanyPermission.SystemChangeLogsDelete);
        if (deleteCount != 6115)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("004087", "对不起，您没有删除日志的权限!")));
            return;
        }

        else
        {
            Response.Redirect("DelLogs.aspx");
        }       
    }    

    /// <summary>
    /// 字段排序
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvDetailsByDay_Sorting(object sender, GridViewSortEventArgs e)
    {

        DataView dv = new DataView(LogsManageBLL.ChangeLogsQuery(columnNames.ToString(), queryConditions.ToString()));
        string sortstr = e.SortExpression;

        switch (sortstr.ToLower().Trim())
        {
            case "id":
                if (blID)
                {
                    dv.Sort = "id desc";
                    blID = false;
                    ViewState["orderColumnName"] = "id";
                    pagesort = 0;
                }
                else
                {
                    dv.Sort = "id asc";
                    blID = true;
                    ViewState["orderColumnName"] = " id";
                    pagesort = 1;
                }
                break;

            case "type":
                if (blType)
                {
                    dv.Sort = "type desc";
                    blType = false;
                    ViewState["orderColumnName"] = " type ";
                    pagesort = 0;
                }
                else
                {
                    dv.Sort = "type asc";
                    blType = true;
                    ViewState["orderColumnName"] = " type ";
                    pagesort = 1;
                }
                break;

            case "time":
                if (blTime)
                {
                    dv.Sort = "time desc";
                    blTime = false;
                    ViewState["orderColumnName"] = " time ";
                    pagesort = 0;
                }
                else
                {
                    dv.Sort = "time asc";
                    blTime = true;
                    ViewState["orderColumnName"] = " time ";
                    pagesort = 1;
                }
                break;

            case "sip":
                if (blSip)
                {
                    dv.Sort = "sip desc";
                    blSip = false;
                    ViewState["orderColumnName"] = " sip ";
                    pagesort = 0;
                }
                else
                {
                    dv.Sort = "sip asc";
                    blSip = true;
                    ViewState["orderColumnName"] = " sip ";
                    pagesort = 1;
                }
                break;

            case "category":
                if (blCategory)
                {
                    dv.Sort = "category desc";
                    blCategory = false;
                    ViewState["orderColumnName"] = " category ";
                    pagesort = 0;
                }
                else
                {
                    dv.Sort = "category asc";
                    blCategory = true;
                    ViewState["orderColumnName"] = " category ";
                    pagesort = 1;
                }
                break;

            case "[from]":
                if (blFrom)
                {
                    dv.Sort = "[from] desc";
                    blFrom = false;
                    ViewState["orderColumnName"] = " [from] ";
                    pagesort = 0;
                }
                else
                {
                    dv.Sort = "[from] asc";
                    blFrom = true;
                    ViewState["orderColumnName"] = " [from] ";
                    pagesort = 1;
                }
                break;

            case "fromusertype":
                if (blFromUserType)
                {
                    dv.Sort = "FromUserType desc";
                    blFromUserType = false;
                    ViewState["orderColumnName"] = " FromUserType ";
                    pagesort = 0;
                }

                else
                {
                    dv.Sort = "FromUserType asc";
                    blFromUserType = true;
                    ViewState["orderColumnName"] = " FromUserType ";
                    pagesort = 1;
                }
                break;

            case "[to]":
                if (blTo)
                {
                    dv.Sort = "[to] desc";
                    ViewState["orderColumnName"] = " [to] ";
                    pagesort = 0;
                    blTo = false;
                }
                else
                {
                    dv.Sort = "[to] asc";
                    blTo = true;
                    ViewState["orderColumnName"] = " [to] ";
                    pagesort = 1;
                }
                break;

            case "tousertype":
                if (blToUserType)
                {
                    dv.Sort = "ToUserType desc";
                    blToUserType = false;
                    ViewState["orderColumnName"] = " ToUserType ";
                    pagesort = 0;
                }

                else
                {
                    dv.Sort = "ToUserType asc";
                    blToUserType = true;
                    ViewState["orderColumnName"] = " ToUserType ";
                    pagesort = 1;
                }
                break;

            case "expectnum":
                if (blExpectNum)
                {
                    dv.Sort = "ExpectNum desc";
                    blExpectNum = false;
                    ViewState["orderColumnName"] = " ExpectNum ";
                    pagesort = 0;
                }

                else
                {
                    dv.Sort = "ExpectNum asc";
                    blExpectNum = true;
                    ViewState["orderColumnName"] = " ExpectNum ";
                    pagesort = 1;
                }
                break;
        }
        ViewState["pagesort"] = pagesort;
        //分页
        Pager page = Page.FindControl("Pager1") as Pager;
        if (ViewState["orderColumnName"].ToString() != "id")
        {
            page.PageBind(0, 10, "ChangeLogs", columnNames, queryConditions, "ID", "gvDetailsByDay", ViewState["orderColumnName"].ToString(), int.Parse(ViewState["pagesort"].ToString()));
        }
        else
        {
            page.PageBind(0, 10, "ChangeLogs", columnNames, queryConditions, "[to]", "gvDetailsByDay", ViewState["orderColumnName"].ToString(), int.Parse(ViewState["pagesort"].ToString()));
        }
    }

    /// <summary>
    /// GridView行绑定
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvDetailsByDay_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //控制GridView样式
        if (e.Row.RowType == DataControlRowType.DataRow)
        {            
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
        }
    }
    /// <summary>
    /// 加上时间差
    /// </summary>
    /// <param name="inputdate"></param>
    /// <returns></returns>
    protected string GetbyDateTime(string inputdate)
    {
        return Convert.ToDateTime(inputdate).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString("yyyy-MM-dd HH:mm:ss");
    }
    /// <summary>
    /// 拼接条件
    /// </summary>
    public void getCondition()
    {
        ///时间条件
        if (txtDate.Text.Trim() != "")
        {
            ///注意And前有空格
            queryConditions += @" and DateAdd(day,DateDiff(day,0,[Time]),0)='" + txtDate.Text.Trim().Replace("'", "") + "'";
        }

        ///期数
        if (DropDownListqishu.SelectedValue != "-1")
        {
            queryConditions += @" and ExpectNum='" + DropDownListqishu.SelectedValue + "'";
        }

        ///操作类型
        if (ddlChangeType.SelectedValue != "-1")
        {
            queryConditions += @" and Type='" + ddlChangeType.SelectedValue + "'";
        }

        ///操作的对象类别
        if (ddlToType.SelectedValue != "-1")
        {
            queryConditions += @" and ToUserType='" + ddlToType.SelectedValue + "'";
        }

        ///操作的类别
        if (ddlChangeCategory.SelectedValue != "-1")
        {
            queryConditions += @" and Category='" + ddlChangeCategory.SelectedValue + "'";
        }

        /// 操作者的类别：
        if (ddlOperatorType.SelectedValue != "-1")
        {
            queryConditions += @" and FromUserType = '" + ddlOperatorType.SelectedValue + "'";
        }

        /// 操作者的帐号
        if (txtLoginID.Text.Trim() != "")
        {
            queryConditions += @" and [From] = '" + txtLoginID.Text.Trim().Replace("'", "") + "'";
        }

        ///操作对象的帐号
        if (txtToID.Text.Trim() != "")
        {
            queryConditions += @" and [To] = '" + txtToID.Text.Trim().Replace("'", "") + "'";
        }

        /// 操作者的IP地址
        if (txtOperatorIP.Text.Trim() != "")
        {
            queryConditions += @" and SIP = '" + txtOperatorIP.Text.Trim().Replace("'", "") + "'";
        }
    }
}
