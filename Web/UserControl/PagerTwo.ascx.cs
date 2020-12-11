
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
using BLL;
using System.Text;
/// <summary>
/// 分页控件
/// 使用方法一：1.必须给PageSize,PageTable,PageColumn,PageCondition,key,Gridviews赋值
/// 调用PageBind方法
/// 方法二：使用PageBind(参数列表直接赋值)
/// </summary>
public partial class PagerTwo : System.Web.UI.UserControl
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
            ViewState["PageSize"] = ViewState["PageSize"] == null || ViewState["PageSize"].ToString() == "" ? 10 : int.Parse(ViewState["PageSize"].ToString());
            this.txtTS.Text = ViewState["PageSize"].ToString();
            if (this.InitBindData)
            {
                ViewState["PageIndex"] = ViewState["PageIndex"] == null || ViewState["PageIndex"].ToString() == "" ? "1" : ViewState["PageIndex"].ToString();
                ViewState["PageSize"] = ViewState["PageSize"] == null || ViewState["PageSize"].ToString() == "" ? 10 : int.Parse(ViewState["PageSize"].ToString());
                this.txtTS.Text = ViewState["PageSize"].ToString();
            }
            else {
                pageS.Visible = false;
            }
        }
    }
    public PagerTwo()
    { }

    public PagerTwo(int pageindex, int pagesize, string pagetable, string pageColumn, string condition, string key, string controlname)
    {
        this.Pageindex = pageindex;
        this.PageSize = pagesize;
        this.PageTable = pagetable;
        this.PageColumn = pageColumn;
        this.Condition = condition;
        this.key = key;
        this.ControlName = controlname;
    }
    public int Pageindex
    {
        get { return Convert.ToInt32(ViewState["PageIndex"]); }
        set { ViewState["PageIndex"] = value; }
    }
    /// <summary>
    /// 设置显示的行数
    /// </summary>
    public int PageSize
    {
        get { return Convert.ToInt32(ViewState["PageSize"]); }
        set { ViewState["PageSize"] = value; }
    }
    /// <summary>
    /// 设置查询表名
    /// </summary>
    public string PageTable
    {
        get { return ViewState["PageTable"].ToString(); }
        set { ViewState["PageTable"] = value; }
    }
    /// <summary>
    /// 设置查询的列名
    /// </summary>
    public string PageColumn
    {
        get { return ViewState["PageColumn"].ToString(); }
        set { ViewState["PageColumn"] = value; }
    }
    /// <summary>
    /// 设置查询条件
    /// </summary>
    public string Condition
    {
        get { return ViewState["Conodition"].ToString(); }
        set { ViewState["Conodition"] = value; }
    }
    /// <summary>
    /// 设置主键
    /// </summary>
    public string key
    {
        get { return ViewState["key"].ToString(); }
        set { ViewState["key"] = value; }
    }


    /// <summary>
    /// 设置排序键
    /// </summary>
    public string OrderKey
    {
        get { return ViewState["orderkey"] == null ? "" : ViewState["orderkey"].ToString(); }
        set { ViewState["orderkey"] = value; }
    }

    /// <summary>
    /// 设置排序方向
    /// </summary>
    public int SortType
    {
        get { return ViewState["sortType"] == null ? 0 : (int)ViewState["sortType"]; }
        set { ViewState["sortType"] = value; }
    }

    /// <summary>
    /// 按升序排还是按降序排
    /// </summary>
    public bool AscOrDesc
    {
        get { return Convert.ToBoolean(ViewState["AscOrDesc"]); }
        set { ViewState["AscOrdesc"] = value; }
    }

    /// <summary>
    /// 设置控件
    /// </summary>
    public string ControlName
    {
        get { return ViewState["ControlName"].ToString(); }
        set { ViewState["ControlName"] = value; }
    }
    public int PageCount
    {
        get { return int.Parse(ViewState["PageCount"].ToString()); }
        set { ViewState["PageCount"] = value; }
    }
    private void PageBindshow(string ControlName, DataTable table)
    {
        if (Page.FindControl(ControlName) is GridView)
        {
            GridView giv = Page.FindControl(ControlName) as GridView;
            giv.DataSource = table;
            giv.DataBind();
        }
        if (Page.FindControl(ControlName) is Repeater)
        {
            Repeater rep = Page.FindControl(ControlName) as Repeater;
            rep.DataSource = table;
            rep.DataBind();
        }
        if (Page.FindControl(ControlName) is DataList)
        {
            DataList dts = Page.FindControl(ControlName) as DataList;
            dts.DataSource = table;
            dts.DataBind();
        }
        if (Page.FindControl(ControlName)is DataGrid)
        {
            DataGrid giv = Page.FindControl(ControlName) as DataGrid;
            giv.DataSource = table;
            giv.DataBind();
        }
    }
    public void PageBind()
    {
        BLL.other.Company.ProductTreeList dao = new BLL.other.Company.ProductTreeList();
        string columns = ViewState["PageColumn"] == null || ViewState["PageColumn"].ToString() == "" ? "*" : ViewState["PageColumn"].ToString();
        string conod = ViewState["Conodition"] == null || ViewState["Conodition"].ToString() == "" ? "1=1" : ViewState["Conodition"].ToString();
        string orderkey = ViewState["key"] == null || ViewState["key"].ToString() == "" ? "1" : ViewState["key"].ToString();
        int rescordcount = 0;
        int pagecounts = 0;
        DataTable tab = dao.GetDataTablePage_SmsBll(Convert.ToInt32(ViewState["PageIndex"]), Convert.ToInt32(ViewState["PageSize"]), ViewState["PageTable"].ToString(), columns, conod, orderkey, out rescordcount, out pagecounts);
        ViewState["PageCount"] = pagecounts;
        StringBuilder sb = new StringBuilder();
        if (tab.Rows.Count>0)
        {
            PageBindshow(ViewState["ControlName"].ToString(), tab);
            sb.Append(bs.GetTran("001045", "共") + " <b>" + rescordcount + "</b> " + bs.GetTran("001049", "条记录"));
            sb.Append(bs.GetTran("000156", "第") + "   <b>" + (int.Parse(ViewState["PageIndex"].ToString()) + 1) + "</b>  " + bs.GetTran("001055", "页"));
            sb.Append(bs.GetTran("001045", "共") + "<b> " + pagecounts + "</b>"+bs.GetTran("001055", "页"));
            dropPageList.Items.Clear();
            for (int i = 0; i < pagecounts; i++)
			{
                dropPageList.Items.Add(new ListItem((i+1).ToString(), (i).ToString()));
			}
           dropPageList.SelectedIndex = int.Parse(ViewState["PageIndex"].ToString());
          //  dropPageList.SelectedItem.Value =ViewState["PageIndex"].ToString();
            if (rescordcount > Convert.ToInt32(ViewState["PageSize"]))
            {
                 pageS.Visible = true;
            }
            else
            {
                 pageS.Visible = false;
            }
            InitBindData = true;
        }
        else
        {
            PageBindshow(ViewState["ControlName"].ToString(), tab);
            sb.Append(bs.GetTran("001595", "没有查询到数据"));
            pageS.Visible = false;
        }
        this.lbl.Text = sb.ToString();
        CheckBtn();
    }

    public void PageBind_sort()
    {
        BLL.other.Company.ProductTreeList dao = new BLL.other.Company.ProductTreeList();
        string columns = ViewState["PageColumn"] == null || ViewState["PageColumn"].ToString() == "" ? "*" : ViewState["PageColumn"].ToString();
        string conod = ViewState["Conodition"] == null || ViewState["Conodition"].ToString() == "" ? "1=1" : ViewState["Conodition"].ToString();
        string orderkey = ViewState["key"] == null || ViewState["key"].ToString() == "" ? "1" : ViewState["key"].ToString();
        string orderkey2 = (ViewState["orderkey"] == null || ViewState["orderkey"].ToString() == "") ? "id" : ViewState["orderkey"].ToString();
        int sortType = ViewState["sortType"] == null?0:(int)ViewState["sortType"] ;
        int rescordcount = 0;
        int pagecounts = 0;
        DataTable tab = dao.GetDataTablePage_SmsBll(Convert.ToInt32(ViewState["PageIndex"]), Convert.ToInt32(ViewState["PageSize"]), ViewState["PageTable"].ToString(), columns, conod, orderkey,orderkey2,sortType, out rescordcount, out pagecounts);
        ViewState["PageCount"] = pagecounts;
        StringBuilder sb = new StringBuilder();
        if (tab.Rows.Count > 0)
        {
            PageBindshow(ViewState["ControlName"].ToString(), tab);
            sb.Append(bs.GetTran("001045", "共") + " <b>" + rescordcount + "</b> " + bs.GetTran("001049", "条记录"));
            sb.Append(bs.GetTran("000156", "第") + "   <b>" + (int.Parse(ViewState["PageIndex"].ToString()) + 1) + "</b>  " + bs.GetTran("001055", "页"));
            sb.Append(bs.GetTran("001045", "共") + "<b> " + pagecounts + "</b>" + bs.GetTran("001055", "页"));
            dropPageList.Items.Clear();
            for (int i = 0; i < pagecounts; i++)
            {
                dropPageList.Items.Add(new ListItem((i + 1).ToString(), (i).ToString()));
            }
            dropPageList.SelectedIndex = int.Parse(ViewState["PageIndex"].ToString());
            //  dropPageList.SelectedItem.Value =ViewState["PageIndex"].ToString();
            if (rescordcount > Convert.ToInt32(ViewState["PageSize"]))
            {
                pageS.Visible = true;
            }
            else
            {
                pageS.Visible = false;
            }
            InitBindData = true;
        }
        else
        {
            PageBindshow(ViewState["ControlName"].ToString(), tab);
            sb.Append(bs.GetTran("001595", "没有查询到数据"));
            pageS.Visible = false;
        }
        this.lbl.Text = sb.ToString();
        CheckBtn();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pageIdex"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageTable"></param>
    /// <param name="pageColumn"></param>
    /// <param name="codition"></param>
    /// <param name="key"></param>
    /// <param name="controlName"></param>
    /// <param name="orderKey2">排序列名</param>
    /// <param name="sortType">排序方向0 降序 1 升序</param>
    public void PageBind(int pageIdex, int pageSize, string pageTable, string pageColumn, string codition, string key, string controlName,string orderKey2,int sortType)
    {
        this.Pageindex = pageIdex;
        this.PageSize = pageSize;
        this.PageTable = pageTable;
        this.PageColumn = pageColumn;
        this.Condition = codition;
        this.key = key;
        this.ControlName = controlName;
        this.OrderKey = orderKey2;
        this.SortType = sortType;
        BLL.other.Company.ProductTreeList dao = new BLL.other.Company.ProductTreeList();
        string columns = ViewState["PageColumn"] == null || ViewState["PageColumn"].ToString() == "" ? "*" : ViewState["PageColumn"].ToString();
        string conod = ViewState["Conodition"] == null || ViewState["Conodition"].ToString() == "" ? "1=1" : ViewState["Conodition"].ToString();
        string orderkey = ViewState["key"] == null || ViewState["key"].ToString() == "" ? "1" : ViewState["key"].ToString();
        int rescordcount = 0;
        int pagecounts = 0;
        DataTable tab = dao.GetDataTablePage_SmsBll(Convert.ToInt32(ViewState["PageIndex"]), Convert.ToInt32(ViewState["PageSize"]), ViewState["PageTable"].ToString(), columns, conod, orderkey,orderKey2,sortType, out rescordcount, out pagecounts);
        ViewState["PageCount"] = pagecounts;
        StringBuilder sb = new StringBuilder();
        if (tab.Rows.Count > 0)
        {
            PageBindshow(ViewState["ControlName"].ToString(), tab);
            sb.Append(bs.GetTran("001045", "共") + "<b> " + rescordcount + "</b> " + bs.GetTran("001049", "条记录"));
            sb.Append(bs.GetTran("000156", "第") + "  <b> " + (int.Parse(ViewState["PageIndex"].ToString()) + 1) + "</b>   " + bs.GetTran("001055", "页"));
            sb.Append(bs.GetTran("001045", "共") + " <b> " + pagecounts + "</b> " + bs.GetTran("001055", "页"));
            dropPageList.Items.Clear();
            for (int i = 0; i < pagecounts; i++)
            {
                dropPageList.Items.Add(new ListItem((i + 1).ToString(), (i).ToString()));
            }
            dropPageList.SelectedIndex = int.Parse(ViewState["PageIndex"].ToString());
            //dropPageList.SelectedItem.Value =ViewState["PageIndex"].ToString();
            int size = Convert.ToInt32(ViewState["PageSize"]);
            if (rescordcount > size)
            {
                pageS.Visible = true;
            }
            else
            {
                pageS.Visible = false;
            }
            InitBindData = true;
        }
        else
        {
            PageBindshow(ViewState["ControlName"].ToString(), tab);
            sb.Append(bs.GetTran("001595", "没有查询到数据"));
            pageS.Visible = false;
        }
        this.lbl.Text = sb.ToString();
        CheckBtn();
    }

    public void PageBind(int pageIdex, int pageSize, string pageTable, string pageColumn, string codition, string key, string controlName)
    {
        this.Pageindex = pageIdex;
        this.PageSize = pageSize;
        this.PageTable = pageTable;
        this.PageColumn = pageColumn;
        this.Condition = codition;
        this.key = key;
        this.ControlName = controlName;
        BLL.other.Company.ProductTreeList dao = new BLL.other.Company.ProductTreeList();
        string columns = ViewState["PageColumn"] == null || ViewState["PageColumn"].ToString() == "" ? "*" : ViewState["PageColumn"].ToString();
        string conod = ViewState["Conodition"] == null || ViewState["Conodition"].ToString() == "" ? "1=1" : ViewState["Conodition"].ToString();
        string orderkey = ViewState["key"] == null || ViewState["key"].ToString() == "" ? "1" : ViewState["key"].ToString();
        int rescordcount = 0;
        int pagecounts = 0;
        DataTable tab = dao.GetDataTablePage_SmsBll(Convert.ToInt32(ViewState["PageIndex"]), Convert.ToInt32(ViewState["PageSize"]), ViewState["PageTable"].ToString(), columns, conod, orderkey, out rescordcount, out pagecounts);
        ViewState["PageCount"] = pagecounts;
        StringBuilder sb = new StringBuilder();
        if (tab.Rows.Count >0)
        {
            PageBindshow(ViewState["ControlName"].ToString(), tab);
            sb.Append(bs.GetTran("001045", "共") + "<b> " + rescordcount + "</b> " + bs.GetTran("001049", "条记录"));
            sb.Append(bs.GetTran("000156", "第") + "   <b> " + (int.Parse(ViewState["PageIndex"].ToString()) + 1) + "</b>  " + bs.GetTran("001055", "页"));
            sb.Append(bs.GetTran("001045", "共") + " <b> " + pagecounts + "</b> " + bs.GetTran("001055", "页"));
            dropPageList.Items.Clear();
            for (int i = 0; i < pagecounts; i++)
            {
                dropPageList.Items.Add(new ListItem((i + 1).ToString(), (i).ToString()));
            }
            dropPageList.SelectedIndex = int.Parse(ViewState["PageIndex"].ToString());
            //dropPageList.SelectedItem.Value =ViewState["PageIndex"].ToString();
            int size = Convert.ToInt32(ViewState["PageSize"]);
            if (rescordcount > size)
            {
                pageS.Visible = true;
            }
            else 
            {
                pageS.Visible = false;
            } 
            InitBindData = true;
        }
        else
        {
            PageBindshow(ViewState["ControlName"].ToString(), tab);
            sb.Append(bs.GetTran("001595", "没有查询到数据"));
            pageS.Visible = false;
        }
        this.lbl.Text = sb.ToString();
        CheckBtn();
    }

    
    /// <summary>
    /// 汪华(20091004所写)按照关键字排序（为GridView排序事件服务）
    /// </summary>
    /// <param name="pageIdex"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageTable"></param>
    /// <param name="pageColumn"></param>
    /// <param name="codition"></param>
    /// <param name="key"></param>
    /// <param name="ascOrDesc"></param>
    /// <param name="controlName"></param>
    public void PagingSort(int pageIdex, int pageSize, string pageTable, string pageColumn, string codition, string key, bool ascOrDesc, string controlName)
    {
        this.Pageindex = pageIdex;
        this.PageSize = pageSize;
        this.PageTable = pageTable;
        this.PageColumn = pageColumn;
        this.Condition = codition;
        this.key = key;
        this.AscOrDesc = ascOrDesc;        
        this.ControlName = controlName;
        BLL.other.Company.ProductTreeList dao = new BLL.other.Company.ProductTreeList();
        string columns = ViewState["PageColumn"] == null || ViewState["PageColumn"].ToString() == "" ? "*" : ViewState["PageColumn"].ToString();
        string conod = ViewState["Conodition"] == null || ViewState["Conodition"].ToString() == "" ? "1=1" : ViewState["Conodition"].ToString();
        string orderkey = ViewState["key"] == null || ViewState["key"].ToString() == "" ? "1" : ViewState["key"].ToString();        
        int rescordcount = 0;
        int pagecounts = 0;
        DataTable tab= dao.GetCustomersDataPage_Sort(Convert.ToInt32(ViewState["PageIndex"]),Convert.ToInt32(ViewState["PageSize"]),ViewState["PageTable"].ToString(), columns, conod, orderkey,ascOrDesc,out rescordcount,out pagecounts);
        ViewState["PageCount"] = pagecounts;
        StringBuilder sb = new StringBuilder();
        if (tab.Rows.Count > 0)
        {
            PageBindshow(ViewState["ControlName"].ToString(), tab);
            sb.Append(bs.GetTran("001045", "共") + "<b> " + rescordcount + "</b> " + bs.GetTran("001049", "条记录"));
            sb.Append(bs.GetTran("000156", "第") + "   <b> " + (int.Parse(ViewState["PageIndex"].ToString()) + 1) + "</b>   " + bs.GetTran("001055", "页"));
            sb.Append(bs.GetTran("001045", "共") + " <b> " + pagecounts + "</b> 页");
            dropPageList.Items.Clear();
            for (int i = 0; i < pagecounts; i++)
            {
                dropPageList.Items.Add(new ListItem((i + 1).ToString(), (i).ToString()));
            }
            dropPageList.SelectedIndex = int.Parse(ViewState["PageIndex"].ToString());
            //dropPageList.SelectedItem.Value =ViewState["PageIndex"].ToString();
            int size = Convert.ToInt32(ViewState["PageSize"]);
            if (rescordcount > size)
            {
                pageS.Visible = true;
            }
            else
            {
                pageS.Visible = false;
            }
            InitBindData = true;
        }
        else
        {
            PageBindshow(ViewState["ControlName"].ToString(), tab);
            sb.Append(bs.GetTran("001595", "没有查询到数据"));
            pageS.Visible = false;
        }
        this.lbl.Text = sb.ToString();
        CheckBtn();
    }

    protected void lkbtn_Login_Click(object sender, EventArgs e)
    {
        try
        {

            if (Convert.ToInt32(this.txtTS.Text.Trim()) > 0)
            {
                ViewState["PageSize"] = this.txtTS.Text;
                

                ViewState["PageIndex"] = 0;
                if (this.ControlName == "GridView1NmaeNiHao")
                {
                    PageBind1();
                }
                else
                {
                    if (this.OrderKey != "")
                        PageBind_sort();
                    else
                        PageBind();
                }
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
            msg="<script>alert('对不起，请输入数字！！！');</script>";
            // return;
        }


    }

    //首页
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        ViewState["PageIndex"] = 0;
        if (this.ControlName == "GridView1NmaeNiHao")
        {
            PageBind1();
        }
        else
        {
            if (this.OrderKey != "") 
                PageBind_sort();
            else       
                PageBind();
        }
        CheckBtn();
    }
    /// <summary>
    /// 验证按钮
    /// </summary>
    private void CheckBtn()
    {
        if (Pageindex == 0)
        {
            btnFirst.Enabled = false;
            btnNext.Enabled = true;
            btnOmega.Enabled = true;
            btnPrevious.Enabled = false;
        }
        else if (Pageindex == PageCount-1)
        {
            btnFirst.Enabled = true;
            btnNext.Enabled = false;
            btnOmega.Enabled = false;
            btnPrevious.Enabled = true;
        }
        else
        {
            btnFirst.Enabled = true;
            btnNext.Enabled = true;
            btnOmega.Enabled = true;
            btnPrevious.Enabled = true;
        }
    }
    //上页
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        if (Pageindex == 0)
        {
            ViewState["PageIndex"] = 0;
        }
        else
        {
            ViewState["PageIndex"] = Pageindex - 1;
        }
        if (this.ControlName == "GridView1NmaeNiHao")
        {
            PageBind1();
        }
        else
        {
            if (this.OrderKey == "")
                PageBind();
            else
                PageBind_sort();
        }
        CheckBtn();
    }
    //下一页
    protected void btnNext_Click(object sender, EventArgs e)
    {
        if ((Pageindex+1) >= PageCount)
        {
            ViewState["PageIndex"] = PageCount-1;
        }
        else
        {
            ViewState["PageIndex"] = Pageindex + 1;
        }
        if (this.ControlName == "GridView1NmaeNiHao")
        {
            PageBind1();
        }
        else
        {
            if (this.OrderKey == "")
                PageBind();
            else
                PageBind_sort();
        }
        CheckBtn();
    }

    //最后页
    protected void btnOmega_Click(object sender, EventArgs e)
    {
        ViewState["PageIndex"] = PageCount-1;
        if (this.ControlName == "GridView1NmaeNiHao")
        {
            PageBind1();
        }
        else
        {
            if (this.OrderKey == "")
                PageBind();
            else
                PageBind_sort();
        }
        CheckBtn();
    }
    #region
    /*
    protected void btngo_Click(object sender, EventArgs e)
    {
        int count = 0;
        try
        {
            count = Convert.ToInt32(this.txtGo.Text.Trim());
        }
        catch
        {
            ScriptHelper.SetAlert(this.txtGo,"您输入的数据不对！！！");
            return;
        }
        if (count < 0)
        {
          //  ViewState["PageIndex"] = 0;
            ScriptHelper.SetAlert(this.txtGo, "您输入的数据不对！！！");
            return;
        }
        else if (count > PageCount)
        {
            //ViewState["PageIndex"] = PageCount-1;
            ScriptHelper.SetAlert(this.txtGo, "您输入的数据不对！！！");
            return;
        }
        else
        {
            ViewState["PageIndex"] = count-1;
        }
        PageBind();
        CheckBtn();
    }
     * */
    #endregion
    protected void dropPageList_SelectedIndexChanged(object sender, EventArgs e)
    {
        int count = 0;
        count = Convert.ToInt32(this.dropPageList.SelectedValue);
        ViewState["PageIndex"] = count;
        if (this.ControlName == "GridView1NmaeNiHao")
        {
            PageBind1();
        }
        else
        {
            if (this.OrderKey == "")
                PageBind();
            else
                PageBind_sort();
        }
        CheckBtn();
    }


    public void PageBind1()
    {
        BLL.other.Company.ProductTreeList dao = new BLL.other.Company.ProductTreeList();
        string columns = ViewState["PageColumn"] == null || ViewState["PageColumn"].ToString() == "" ? "*" : ViewState["PageColumn"].ToString();
        string conod = ViewState["Conodition"] == null || ViewState["Conodition"].ToString() == "" ? "1=1" : ViewState["Conodition"].ToString();
        string orderkey = ViewState["key"] == null || ViewState["key"].ToString() == "" ? "1" : ViewState["key"].ToString();
        int rescordcount = 0;
        int pagecounts = 0;
        DataTable tab = dao.GetDataTablePage_SmsBll1(Convert.ToInt32(ViewState["PageIndex"]), Convert.ToInt32(ViewState["PageSize"]), ViewState["PageTable"].ToString(), columns, conod, orderkey, out rescordcount, out pagecounts);
        ViewState["PageCount"] = pagecounts;
        StringBuilder sb = new StringBuilder();
        if (tab.Rows.Count > 0)
        {
            PageBindshow(ViewState["ControlName"].ToString(), tab);
            sb.Append(bs.GetTran("001045", "共") + " <b>" + rescordcount + "</b> " + bs.GetTran("001049", "条记录"));
            sb.Append(bs.GetTran("000156", "第") + "   <b>" + (int.Parse(ViewState["PageIndex"].ToString()) + 1) + "</b>  " + bs.GetTran("001055", "页"));
            sb.Append(bs.GetTran("001045", "共") + "<b> " + pagecounts + "</b>" + bs.GetTran("001055", "页"));
            dropPageList.Items.Clear();
            for (int i = 0; i < pagecounts; i++)
            {
                dropPageList.Items.Add(new ListItem((i + 1).ToString(), (i).ToString()));
            }
            dropPageList.SelectedIndex = int.Parse(ViewState["PageIndex"].ToString());
            //  dropPageList.SelectedItem.Value =ViewState["PageIndex"].ToString();
            if (rescordcount > Convert.ToInt32(ViewState["PageSize"]))
            {
                pageS.Visible = true;
            }
            else
            {
                pageS.Visible = false;
            }
            InitBindData = true;
        }
        else
        {
            PageBindshow(ViewState["ControlName"].ToString(), tab);
            sb.Append(bs.GetTran("001595", "没有查询到数据"));
            pageS.Visible = false;
        }
        this.lbl.Text = sb.ToString();
        CheckBtn();
    }
}
