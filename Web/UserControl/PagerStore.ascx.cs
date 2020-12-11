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

//Add Namespace
using BLL.other.Company;
using System.Text;
using BLL;

/*
 * 创建者：  汪  华
 * 创建时间：2009-11-18
 * 完成时间：2009-11-18 
 * 功能：    支持排序的分页（支持多字段任意排序,不要求排序字段唯一）
 * 注意：    传参是一定要注意：如果是首页，pageIndex为1
 */

public partial class PagerStore : System.Web.UI.UserControl
{
    /// <summary>
    /// 初始登陆时是否绑定数据（是为true，否为false），默认为false 
    /// </summary>
    public string msg = "";
    public bool InitBindData = false;
    BLL.TranslationBase bs = new TranslationBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            btnFirst.Text = new BLL.TranslationBase().GetTran("001478", "首页");
            btnPrevious.Text = new BLL.TranslationBase().GetTran("001477", "上一页");
            btnNext.Text = new BLL.TranslationBase().GetTran("001480", "下一页");
            btnGo.Text = new BLL.TranslationBase().GetTran("001481", "尾页");


            ViewState["PageSize"] = ViewState["PageSize"] == null || ViewState["PageSize"].ToString() == "" ? 10 : int.Parse(ViewState["PageSize"].ToString());
            this.txtTS.Text = ViewState["PageSize"].ToString();
            if (this.InitBindData)
            {
                ViewState["pageIndex"] = ViewState["pageIndex"] == null || ViewState["pageIndex"].ToString() == "" ? "1" : ViewState["pageIndex"].ToString();
                ViewState["pageSize"] = ViewState["pageSize"] == null || ViewState["pageSize"].ToString() == "" ? 10 : int.Parse(ViewState["pageSize"].ToString());
                this.txtTS.Text = ViewState["pageSize"].ToString();
            }
            else
            {
                pageTable.Visible = false;
            }
        }

        Translate();
    }

    public void Pager() { }
      /// <summary>
    /// 翻译方法
    /// </summary>
    public void Translate()
    {

        this.btn_submit.Text = bs.GetTran("000064", "确认");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pageIndex">指定当前为第几页</param>
    /// <param name="pageSize">每页多少条记录</param>
    /// <param name="tableNames">表名</param>
    /// <param name="columnNames">列名</param>
    /// <param name="conditions">查询条件</param>
    /// <param name="orderColumnName">排序字段</param>
    /// <param name="controlName">控件ID</param>
    public void Pager(int pageIndex, int pageSize, string tableNames, string columnNames, string conditions, string orderColumnName, string controlName)
    {
        this.pageIndex = pageIndex;
        this.pageSize = pageSize;
        this.tableNames = tableNames;
        this.columnNames = columnNames;
        this.conditions = conditions;
        this.orderColumnName = orderColumnName;
        this.controlName = controlName;
    }

    /// <summary>
    /// 指定当前为第几页
    /// </summary>
    public int pageIndex
    {
        get { return Convert.ToInt32(ViewState["pageIndex"]); }
        set { ViewState["pageIndex"] = value; }
    }

    /// <summary>
    /// 每页多少条记录
    /// </summary>
    public int pageSize
    {
        get { return Convert.ToInt32(ViewState["pageSize"]); }
        set { ViewState["pageSize"] = value; }
    }

    /// <summary>
    /// 表名
    /// </summary>
    public string tableNames
    {
        get { return ViewState["tableNames"].ToString(); }
        set { ViewState["tableNames"] = value; }
    }

    /// <summary>
    /// 列名  
    /// </summary>
    public string columnNames
    {
        get { return ViewState["columnNames"].ToString(); }
        set { ViewState["columnNames"] = value; }
    }

    /// <summary>
    /// 查询条件
    /// </summary>
    public string conditions
    {
        get { return ViewState["conditions"].ToString(); }
        set { ViewState["conditions"] = value; }
    }

    /// <summary>
    /// 排序字段
    /// </summary>
    public string orderColumnName
    {
        get { return ViewState["orderColumnName"].ToString(); }
        set { ViewState["orderColumnName"] = value; }
    }

    /// <summary>
    /// 控件ID
    /// </summary>
    public string controlName
    {
        get { return ViewState["controlName"].ToString(); }
        set { ViewState["controlName"] = value; }
    }

    /// <summary>
    /// 总页数
    /// </summary>
    public int totalPage
    {
        get { return int.Parse(ViewState["totalPage"].ToString()); }
        set { ViewState["totalPage"] = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="controlName">控件ID</param>
    /// <param name="table">表</param>
    private void PageBindShow(string controlName, DataTable table)
    {
        if (Page.FindControl(controlName) is GridView)
        {
            GridView giv = Page.FindControl(controlName) as GridView;
            giv.DataSource = table;
            giv.DataBind();
        }
        if (Page.FindControl(controlName) is Repeater)
        {
            Repeater rep = Page.FindControl(controlName) as Repeater;
            rep.DataSource = table;
            rep.DataBind();
        }
        if (Page.FindControl(controlName) is DataList)
        {
            DataList dts = Page.FindControl(controlName) as DataList;
            dts.DataSource = table;
            dts.DataBind();
        }
        if (Page.FindControl(controlName) is DataGrid)
        {
            DataGrid giv = Page.FindControl(controlName) as DataGrid;
            giv.DataSource = table;
            giv.DataBind();
        }
    }

    /// <summary>
    /// 排序分页
    /// </summary>
    /// <param name="pageIndex">指定当前为第几页</param>
    /// <param name="pageSize">每页多少条记录</param>
    /// <param name="tableNames">表名</param>
    /// <param name="columnNames">列名</param>
    /// <param name="conditions">查询条件</param>
    /// <param name="orderColumnName">排序字段</param>
    /// <param name="controlName">控件ID</param>
    public void PageSorting(int pageIndex, int pageSize, string tableNames, string columnNames, string conditions, string orderColumnName, string controlName)
    {
        this.pageIndex = pageIndex;
        this.pageSize = pageSize;
        this.tableNames = tableNames;
        this.controlName = columnNames;
        this.conditions = conditions;
        this.orderColumnName = orderColumnName;
        this.controlName = controlName;

        ViewState["pageIndex"] = pageIndex;
        ViewState["pageSize"] = pageSize;
        ViewState["tableNames"] = tableNames;
        ViewState["columnNames"] = columnNames;
        ViewState["conditions"] = conditions;
        ViewState["orderColumnName"] = orderColumnName;
        ViewState["controlName"] = controlName;

        int totalRecord = 0;
        int totalPage = 0;

        DataTable dt = ProductTreeList.PageByWangHua(Convert.ToInt32(ViewState["pageIndex"]), Convert.ToInt32(ViewState["pageSize"]), ViewState["tableNames"].ToString(), ViewState["columnNames"].ToString(), ViewState["conditions"].ToString(), ViewState["orderColumnName"].ToString(), out totalRecord, out totalPage);

        ViewState["totalPage"] = totalPage;
        ViewState["totalRecord"] = totalRecord;

        StringBuilder sb = new StringBuilder();
        if (dt.Rows.Count > 0)
        {
            PageBindShow(controlName, dt);

            sb.Append(bs.GetTran("001045", "共") + "<b> " + totalRecord + "</b> " + bs.GetTran("001049", "条记录"));
            sb.Append(bs.GetTran("000156", " 第") + "<b> " + int.Parse(ViewState["pageIndex"].ToString()) + "</b> " + bs.GetTran("001055", "页"));
            sb.Append(bs.GetTran("001045", "共") + " <b> " + totalPage + "</b> " + bs.GetTran("001055","页"));
            txt_page.Text = ViewState["pageIndex"].ToString();
            int size = Convert.ToInt32(ViewState["pageSize"]);
            if (totalRecord > size)
            {
                pageTable.Visible = true;
            }
            else
            {
                pageTable.Visible = false;
            }
            InitBindData = true;
        }
        else
        {
            PageBindShow(controlName, dt);
            sb.Append(bs.GetTran("001595", "没有查询到数据"));
            pageTable.Visible = false;
        }
        this.lbl.Text = sb.ToString();
        CheckBtn();
    }

    protected void lkbtn_Login_Click(object sender, EventArgs e)
    {
        try
        {

            if ( Convert.ToInt32(this.txtTS.Text.Trim()) > 0)
            {
                ViewState["pageSize"] = this.txtTS.Text;
                ViewState["pageIndex"] = 1;
                PageSorting();
                CheckBtn();
            }

            else
            {
                msg="<script>alert('对不起，您输入的数字无效！！！');</script>";
              //  return;
            }
        }
        catch
        {
            msg = "<script>alert('对不起，请输入数字！！！');</script>";
           // return;
        }
       
        
    }

    /// <summary>
    /// 首页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        ViewState["pageIndex"] = 1;
        PageSorting();
        CheckBtn();
    }

    /// <summary>
    /// 上一页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        if (pageIndex == 1)
        {
            ViewState["pageIndex"] = 1;
        }
        else
        {
            ViewState["pageIndex"] = pageIndex - 1;
        }
        PageSorting();
        CheckBtn();
    }

    /// <summary>
    /// 下一页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNext_Click(object sender, EventArgs e)
    {
        if ((pageIndex + 1) > totalPage)
        {
            ViewState["pageIndex"] = totalPage - 1;
        }
        else
        {
            ViewState["pageIndex"] = pageIndex + 1;
        }
        PageSorting();
        CheckBtn();
    }

    /// <summary>
    /// 尾页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGo_Click(object sender, EventArgs e)
    {
        ViewState["pageIndex"] = totalPage;
        PageSorting();
        CheckBtn();
    }

    ///// <summary>
    ///// 页数下拉框
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void ddlPageList_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    int count = 0;
    //    count = Convert.ToInt32(this.ddlPageList.SelectedValue);
    //    ViewState["pageIndex"] = count;
    //    PageSorting();
    //    CheckBtn();
    //}

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        int count = 0;
        int.TryParse(txt_page.Text.Trim(), out count);
        pageIndex = count;
        if (pageIndex > totalPage) {
            pageIndex = totalPage;
        }
        if (pageIndex < 1) {
            pageIndex = 1;
        }
        ViewState["pageIndex"] = pageIndex;
        PageSorting();
        CheckBtn();
    }

    private void PageSorting()
    {
        PageSorting(Convert.ToInt32(ViewState["pageIndex"]), Convert.ToInt32(ViewState["pageSize"]), ViewState["tableNames"].ToString(), ViewState["columnNames"].ToString(), ViewState["conditions"].ToString(), ViewState["orderColumnName"].ToString(), ViewState["controlName"].ToString());
    }

    /// <summary>
    /// 验证按钮
    /// </summary>
    private void CheckBtn()
    {
        ///首页
        if (pageIndex == 1)
        {
            btnFirst.Enabled = false;
            btnPrevious.Enabled = false;
            btnNext.Enabled = true;
            btnGo.Enabled = true;
        }

        ///尾页
        else if (pageIndex == totalPage)
        {
            btnFirst.Enabled = true;
            btnPrevious.Enabled = true;
            btnNext.Enabled = false;
            btnGo.Enabled = true;
        }

        ///其他页
        else
        {
            btnFirst.Enabled = true;
            btnNext.Enabled = true;
            btnGo.Enabled = true;
            btnPrevious.Enabled = true;
        }
    }
}
