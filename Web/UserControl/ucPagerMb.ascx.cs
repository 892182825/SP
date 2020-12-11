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
public partial class UserControl_ucPagerMb : UserControlBase
{
    protected BLL.TranslationBase tran = new TranslationBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        Translations();
    }
    //protected override void CreateChildControls()
    //{
    //    btn1.Click += btn_Click;
    //    btn2.Click += btn_Click;
    //    btn3.Click += btn_Click;
    //    btn4.Click += btn_Click;
    //    btn5.Click += btn_Click;
    //    btn6.Click += btn_Click;
    //    base.CreateChildControls();
    //}
    private void Translations()
    {

        tran.TranControls(this.lbtnFirst, new string[][] { new string[] { "001478", "第一页" } });
        tran.TranControls(this.lbtnPre, new string[][] { new string[] { "001477", "上一页" } });
        tran.TranControls(this.lbtnNext, new string[][] { new string[] { "001480", "下一页" } });
        tran.TranControls(this.lbtnLast, new string[][] { new string[] { "001481", "尾页" } });

    }

    /// <summary>
    /// 根据存储过程分页查询
    /// </summary>
    /// <param name="tablename">表名</param>
    /// <param name="columens">列名</param>
    /// <param name="condition">查询条件</param>
    /// <param name="key">排序字段 必须是唯一标识< /param>
    /// <param name="dgdata">控件名称 支持GridView Repeater DataList DataGrid 还可再添加</param>
    /// <param name="ascOrDesc">排序字段 0表示降序 1表示升序</param>
    /// <param name="comm">表示按照sql语句还是存储过程分页</param>
    public void PageInit(string tablename, string columens, string condition, string key, string dgdata, AscAndDesc ascOrDesc)
    {
        TableName = tablename;
        Columns = columens;
        Condition = " 1=1 " + condition;
        Key = key;
        DGData = dgdata;
        AscOrDesc = (int)ascOrDesc;
        CommType = CommtypePage.proced;
        string sort = string.Empty;
        if (ascOrDesc == AscAndDesc.desc)
            sort = " desc";
        else
            sort = " asc ";
        this.Sql = "select " + columens + " from " + tablename + " where 1=1  " + condition + " order by " + key + " " + sort;
        PageInit();
    }


    /// <summary>
    /// 根据存储过程分页查询  可以排序
    /// </summary>
    /// <param name="tablename">表</param>
    /// <param name="columens">列</param>
    /// <param name="condition">查询条件</param>
    /// <param name="key">关键字</param>
    /// <param name="order">排序字段</param>
    /// <param name="dgdata">控件名称 支持GridView Repeater DataList DataGrid 还可再添加</param>
    /// <returns></returns>
    public void PageInitSort(string tablename, string columens, string condition, string key, string order, string dgdata)
    {
        TableName = tablename;
        Columns = columens;
        Condition = " 1=1 " + condition;
        Key = key;
        DGData = dgdata;
        Order = order;
        CommType = CommtypePage.Sort;
        this.Sql = "select " + columens + " from " + tablename + " where 1=1  " + condition + " order by " + key + " " + order;
        PageInit();

    }

    /// <summary>
    /// 根据sql语句分页
    /// </summary>
    /// <param name="sql">传入的sql语句</param>
    /// <param name="dgdata">控件名称 支持GridView Repeater DataList DataGrid 还可再添加</param>
    public void PageInit(string sql, string dgdata)
    {
        Sql = sql;
        DGData = dgdata;
        CommType = CommtypePage.sql;
        PageInit();
    }




    /// <summary>
    /// 绑定数据方法
    /// </summary>
    private void PageInit()
    {


        int recordCount = 0;
        int pageCount = 0;
        DataTable dt = null;
        if (PageIndex < 1)
        {
            PageIndex = 1;
        }
        if (CommType == CommtypePage.proced)
        {
            dt = PagerFunction.GetDataTablePage((PageIndex - 1), PageSize, TableName, Columns, Condition, Key, AscOrDesc, out recordCount, out pageCount);
        }
        else if (CommType == CommtypePage.sql)
        {
            dt = PagerFunction.GetPagerDataBySql((PageIndex - 1), PageSize, Sql, out recordCount, out pageCount);
        }
        else if (CommType == CommtypePage.Sort)
        {
            dt = DAL.DataSourceDAL.GetDataPage_DataTable(TableName, Key, Columns, PageSize, PageIndex - 1, Condition, Key, Order, out recordCount, out pageCount);
        }
        /*
               if (dt == null || dt.Rows.Count == 0)
               {
                   divControls.Visible = false;
                   divmessage.Visible = true;
               }
               else
               {
                   divControls.Visible = true;
                   divmessage.Visible = false;
               }*/
        RecordCount = recordCount;
        PageCount = pageCount;
       
        if (PageCount==1)
        {
            this.Visible = true;
        }
       
        

        if (PageIndex > PageCount)
        {
            PageIndex = PageCount;
        }

        //if (this.Page.Master.FindControl(DGData) == null)
        //{
        //    return;
        //}
        if (Page.FindControl(DGData) == null)
        {
            return;
        }


        PageBindshow(DGData, dt);


        //绑定数据后判定分页按钮状态


        int r = PageIndex % 5;
        int minPage = 0;
        int maxPage = 0;
        if (r == 0 || r == 1)
        {
            minPage = PageIndex;
            maxPage = (((PageIndex + 5) > PageCount) ? PageCount : (PageIndex + 5)) + 1;
        }
        else
        {
            minPage = PageIndex - r + 1;
            maxPage = minPage + 6;
            maxPage = (maxPage > PageCount) ? (PageCount + 1) : maxPage;
        }

        lbtn1.Visible = false;
        lbtn2.Visible = false;
        lbtn3.Visible = false;
        lbtn4.Visible = false;
        lbtn5.Visible = false;
        lbtn6.Visible = false;
        btn1.Visible = false;
        btn2.Visible = false;
        btn3.Visible = false;
        btn4.Visible = false;
        btn5.Visible = false;
        btn6.Visible = false;
        if (PageCount <= 6 || PageIndex<4)
        {
            if (PageCount >= 1)
            {
                if (PageIndex != 1)
                {
                    btn1.Visible = true;
                    btn1.Text = "<span>1</span>";
                }
                else
                {
                    lbtn1.Visible = true;
                    lbtn1.Text = "<span>1</span>";
                }
            }
            if (PageCount >= 2)
            {
                if (PageIndex != 2)
                {
                    btn2.Visible = true;
                    btn2.Text = "<span>2</span>";
                }
                else
                {
                    lbtn2.Visible = true;
                    lbtn2.Text = "<span>2</span>";
                }
            }
            if (PageCount >= 3)
            {
                if (PageIndex != 3)
                {
                    btn3.Visible = true;
                    btn3.Text = "<span>3</span>";
                }
                else
                {
                    lbtn3.Visible = true;
                    lbtn3.Text = "<span>3</span>";
                }
            }
            if (PageCount >= 4)
            {
                if (PageIndex != 4)
                {
                    btn4.Visible = true;
                    btn4.Text = "<span>4</span>";
                }
                else
                {
                    lbtn4.Visible = true;
                    lbtn4.Text = "<span>4</span>";
                }
            }
            if (PageCount >= 5)
            {
                if (PageIndex != 5)
                {
                    btn5.Visible = true;
                    btn5.Text = "<span>5</span>";
                }
                else
                {
                    lbtn5.Visible = true;
                    lbtn5.Text = "<span>5</span>";
                }
            }
            if (PageCount >= 6)
            {
                if (PageIndex != 6)
                {
                    btn6.Visible = true;
                    btn6.Text = "<span>6</span>";
                }
                else
                {
                    lbtn6.Visible = true;
                    lbtn6.Text = "<span>6</span>";
                }
            }
                 
        }
        else
        {
            if (PageIndex+3<=pageCount)
            {
                for (int i = PageIndex - 2; i <= PageIndex + 3; i++)
                {
                    if (i == (PageIndex - 2))
                    {
                        btn1.Visible = true;
                        btn1.Text = "<span>" + i.ToString() + "</span>";
                    }
                    if (i == (PageIndex - 1))
                    {
                        btn2.Visible = true;
                        btn2.Text = "<span>" + i.ToString() + "</span>";
                    }
                    if (i == PageIndex)
                    {
                        btn3.Visible = false;
                        lbtn3.Visible = true;
                        lbtn3.Text = "<span>" + i.ToString() + "</span>";
                    }
                    if (i == (PageIndex + 1))
                    {
                        btn4.Visible = true;
                        btn4.Text = "<span>" + i.ToString() + "</span>";
                    }
                    if (i == (PageIndex + 2))
                    {
                        btn5.Visible = true;
                        btn5.Text = "<span>" + i.ToString() + "</span>";
                    }
                    if (i == (PageIndex + 3))
                    {
                        btn6.Visible = true;
                        btn6.Text = "<span>" + i.ToString() + "</span>";
                    }
                }
            }
            else
            {
                for (int i = pageCount; i >= pageCount - 5; i--)
                {
                    if (i == pageCount)
                    {
                        if (i==PageIndex)
                        {
                            btn6.Visible = false;
                            lbtn6.Visible = true;
                            lbtn6.Text = "<span>" + i.ToString() + "</span>";
                        }
                        else
                        {
                            btn6.Visible = true;
                            btn6.Text = "<span>" + i.ToString() + "</span>";
                        }
                        
                    }
                    if (i == (pageCount-1))
                    {
                        if (i == PageIndex)
                        {
                            btn5.Visible = false;
                            lbtn5.Visible = true;
                            lbtn5.Text = "<span>" + i.ToString() + "</span>";
                        }
                        else
                        {
                            btn5.Visible = true;
                            btn5.Text = "<span>" + i.ToString() + "</span>";
                        }

                    }
                    if (i == (pageCount - 2))
                    {
                        if (i == PageIndex)
                        {
                            btn4.Visible = false;
                            lbtn4.Visible = true;
                            lbtn4.Text = "<span>" + i.ToString() + "</span>";
                        }
                        else
                        {
                            btn4.Visible = true;
                            btn4.Text = "<span>" + i.ToString() + "</span>";
                        }

                    }
                    if (i == (pageCount - 3))
                    {
                        if (i == PageIndex)
                        {
                            btn3.Visible = false;
                            lbtn3.Visible = true;
                            lbtn3.Text = "<span>" + i.ToString() + "</span>";
                        }
                        else
                        {
                            btn3.Visible = true;
                            btn3.Text = "<span>" + i.ToString() + "</span>";
                        }

                    }
                    if (i == (pageCount - 4))
                    {
                        if (i == PageIndex)
                        {
                            btn2.Visible = false;
                            lbtn2.Visible = true;
                            lbtn2.Text = "<span>" + i.ToString() + "</span>";
                        }
                        else
                        {
                            btn2.Visible = true;
                            btn2.Text = "<span>" + i.ToString() + "</span>";
                        }

                    }
                    if (i == (pageCount - 5))
                    {
                        if (i == PageIndex)
                        {
                            btn1.Visible = false;
                            lbtn1.Visible = true;
                            lbtn1.Text = "<span>" + i.ToString() + "</span>";
                        }
                        else
                        {
                            btn1.Visible = true;
                            btn1.Text = "<span>" + i.ToString() + "</span>";
                        }

                    }
                }
            }
            
        }

        #region 控件的显示隐藏

        //int num = 1;
        //for (int i = minPage; i < maxPage; i++)
        //{
        //    if (num == 1)
        //    {
        //        btn1.Text = "<span>"+i.ToString()+"</span>";
        //        if (PageIndex == i)
        //        {
                   

        //              btn1.Enabled = false;
        //            //btn1.Attributes.Add("style", "border:1px solid #333;color:red;background-color:Transparent");
        //            btn2.Enabled = true;
                    
        //            btn3.Enabled = true;
                    
        //            btn4.Enabled = true;
                    
        //            btn5.Enabled = true;
                    
        //            btn6.Enabled = true;
                    
        //        }
        //    }
        //    else if (num == 2)
        //    {
        //        btn2.Text = "<span>" + i.ToString() + "</span>";
        //        if (PageIndex == i)
        //        {
                    
        //            // btn2.Enabled = false;
                    
        //            btn1.Enabled = true;
                   
        //            btn3.Enabled = true;
                   
        //            btn4.Enabled = true;
                    
        //            btn5.Enabled = true;
                    
        //            btn6.Enabled = true;
                   
        //        }
        //    }
        //    else if (num == 3)
        //    {
        //        btn3.Text = "<span>" + i.ToString() + "</span>";
        //        if (PageIndex == i)
        //        {
        //            //  btn3.Enabled = false;
                    
        //            btn2.Enabled = true;
                    
        //            btn1.Enabled = true;
                    
        //            btn4.Enabled = true;
                    
        //            btn5.Enabled = true;
                   
        //            btn6.Enabled = true;
                    
        //        }
        //    }
        //    else if (num == 4)
        //    {
        //        btn4.Text = "<span>" + i.ToString() + "</span>";
        //        if (PageIndex == i)
        //        {
        //            // btn4.Enabled = false;
                  
        //            btn2.Enabled = true;
                    
        //            btn3.Enabled = true;
                    
        //            btn1.Enabled = true;
                    
        //            btn5.Enabled = true;
                    
        //            btn6.Enabled = true;
                    
        //        }
        //    }
        //    else if (num == 5)
        //    {
        //        btn5.Text = "<span>" + i.ToString() + "</span>";
        //        if (PageIndex == i)
        //        {
        //            //btn5.Enabled = false;
                    
        //            btn2.Enabled = true;
                   
        //            btn3.Enabled = true;
                   
        //            btn4.Enabled = true;
                    
        //            btn1.Enabled = true;
                    
        //            btn6.Enabled = true;
                   
        //        }
        //    }
        //    else if (num == 6)
        //    {
        //        btn6.Text = "<span>" + i.ToString() + "</span>";
        //        if (PageIndex == i)
        //        {
        //            // btn6.Enabled = false;
                   
        //            btn2.Enabled = true;
                    
        //            btn3.Enabled = true;
                    
        //            btn4.Enabled = true;
                    
        //            btn5.Enabled = true;
                    
        //            btn1.Enabled = true;
                    
        //        }
        //    }
        //    num++;

        //}

        //int count = 6 - num + 1;
        //if (count == 0)
        //{
            
        //    btn1.Visible = false;
        //    btn2.Visible = true;
        //    btn3.Visible = true;
        //    btn4.Visible = true;
        //    btn5.Visible = true;
        //    btn6.Visible = true;
        //}
        //else if (count == 1)
        //{
        //    btn1.Visible = true;
        //    btn2.Visible = true;
        //    btn3.Visible = true;
        //    btn4.Visible = true;
        //    btn5.Visible = true;
        //    btn6.Visible = false;
        //}
        //else if (count == 2)
        //{
        //    btn1.Visible = true;
        //    btn2.Visible = true;
        //    btn3.Visible = true;
        //    btn4.Visible = true;
        //    btn5.Visible = false;
        //    btn6.Visible = false;
        //}
        //else if (count == 3)
        //{
        //    btn1.Visible = true;
        //    btn2.Visible = true;
        //    btn3.Visible = true;
        //    btn4.Visible = false;
        //    btn5.Visible = false;
        //    btn6.Visible = false;
        //}
        //else if (count == 4)
        //{
        //    btn1.Visible = true;
        //    btn2.Visible = true;
        //    btn3.Visible = false;
        //    btn4.Visible = false;
        //    btn5.Visible = false;
        //    btn6.Visible = false;
        //}
        //else if (count == 5)
        //{

        //    btn1.Visible = true;
        //    btn2.Visible = false;
        //    btn3.Visible = false;
        //    btn4.Visible = false;
        //    btn5.Visible = false;
        //    btn6.Visible = false;
        //}
        //else if (count == 6)
        //{            
        //    btn1.Visible = false;
        //    btn2.Visible = false;
        //    btn3.Visible = false;
        //    btn4.Visible = false;
        //    btn5.Visible = false;
        //    btn6.Visible = false;
        //}

        //if (PageIndex % 6 == 1)
        //{

        //    lbtn1.Visible = true;
        //    btn1.Visible = false;
        //}
        //else if (PageIndex % 6 == 2)
        //{

        //    lbtn2.Visible = true;
        //    btn2.Visible = false;
        //}
        //else if (PageIndex % 6 == 3)
        //{

        //    lbtn3.Visible = true;
        //    btn3.Visible = false;
        //}
        //else if (PageIndex % 6 == 4)
        //{

        //    lbtn4.Visible = true;
        //    btn4.Visible = false;
        //}
        //else if (PageIndex % 6 == 5)
        //{

        //    lbtn5.Visible = true;
        //    btn5.Visible = false;
        //}
        //else if (PageIndex % 6 == 0)
        //{

        //    lbtn6.Visible = true;
        //    btn6.Visible = false;
        //}

        #endregion


        int pagemax = ((PageIndex + 10) > PageCount) ? PageCount : PageIndex + 10;
        ddlPages.Items.Clear();
        for (int i = 1; i <= PageCount; i++)
        {
            ddlPages.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
        if (ddlPages.Items.Count != 0)
        {
            ddlPages.Enabled = true;
            foreach (ListItem li in this.ddlPages.Items)
            {
                if (li.Value.Trim() == PageIndex.ToString())
                {
                    li.Selected = true;
                    break;
                }
            }
        }
        else
        {
            ddlPages.Enabled = false;
        }

        //SetPagingButton(PageIndex != 1, PageIndex != PageCount);

        btn1.Enabled = true;

        btn2.Enabled = true;

        btn3.Enabled = true;

        btn4.Enabled = true;

        btn5.Enabled = true;

        btn6.Enabled = true;

    }

    private void PageBindshow(string ControlName, DataTable table)
    {
        if (Page.FindControl(ControlName) is GridView)
        {
            GridView giv = Page.FindControl(ControlName) as GridView;
            giv.DataSource = table;
            giv.DataBind();
            if (CellTable == 1) { Unite(giv); }

        }
        else if (Page.FindControl(ControlName) is Repeater)
        {
            Repeater rep = Page.FindControl(ControlName) as Repeater;
            rep.DataSource = table;
            int s = table.Rows.Count;
            rep.DataBind();
        }
        else if (Page.FindControl(ControlName) is DataList)
        {
            DataList dts = Page.FindControl(ControlName) as DataList;
            dts.DataSource = table;
            dts.DataBind();
        }
        else if (Page.FindControl(ControlName) is DataGrid)
        {
            DataGrid giv = Page.FindControl(ControlName) as DataGrid;
            giv.DataSource = table;
            giv.DataBind();
        }
    }

    /// <summary>
    /// GridView  合并单元格  只用于商品的上架下架
    /// </summary>
    /// <param name="gv"></param>
    protected void Unite(GridView gv)
    {

        string LastType1;
        int LastCell;
        if (gv.Rows.Count > 0)
        {
            LastType1 = (gv.Rows[0].Cells[0].FindControl("lblcommodityid") as Label).Text.Trim();
            gv.Rows[0].Cells[0].RowSpan = 1;
            gv.Rows[0].Cells[1].RowSpan = 1;
            gv.Rows[0].Cells[2].RowSpan = 1;
            gv.Rows[0].Cells[3].RowSpan = 1;
            LastCell = 0;

            for (int i = 1; i < gv.Rows.Count; i++)
            {
                string txt = (gv.Rows[i].Cells[0].FindControl("lblcommodityid") as Label).Text.Trim();
                if (txt == LastType1)
                {
                    gv.Rows[i].Cells[0].Visible = false;
                    gv.Rows[LastCell].Cells[0].RowSpan++;

                    gv.Rows[i].Cells[1].Visible = false;
                    gv.Rows[LastCell].Cells[1].RowSpan++;

                    gv.Rows[i].Cells[2].Visible = false;
                    gv.Rows[LastCell].Cells[2].RowSpan++;

                    gv.Rows[i].Cells[3].Visible = false;
                    gv.Rows[LastCell].Cells[3].RowSpan++;

                }
                else
                {

                    LastType1 = txt;
                    LastCell = i;
                    gv.Rows[i].Cells[0].RowSpan = 1;
                    gv.Rows[i].Cells[1].RowSpan = 1;
                    gv.Rows[i].Cells[2].RowSpan = 1;
                    gv.Rows[i].Cells[3].RowSpan = 1;
                }
            }
        }
    }



    public int CellTable
    {
        get { return ViewState["celltable"] == null ? 0 : (int)ViewState["celltable"]; }
        set { ViewState["celltable"] = value; }
    }


    /// <summary>
    /// 设置排序方向 0表示降序 1表示升序
    /// </summary>
    public int AscOrDesc
    {
        get { return ViewState["ascOrDesc"] == null ? 0 : (int)ViewState["ascOrDesc"]; }
        set { ViewState["ascOrDesc"] = value; }
    }

    /// <summary>
    /// 分页sql语句
    /// </summary>
    public string Sql
    {
        get { return ViewState["sql"].ToString(); }
        set { ViewState["sql"] = value; }
    }

    /// <summary>
    /// 用存储过程分页还是sql语句分页
    /// </summary>
    public CommtypePage CommType
    {
        get { return (CommtypePage)ViewState["sqltype"]; }
        set { ViewState["sqltype"] = value; }
    }

    /// <summary>
    /// 数据库表名  
    /// </summary>
    public string TableName
    {
        get { return ViewState["TalbleName"].ToString(); }
        set { ViewState["TalbleName"] = value; }
    }
    /// <summary>
    /// 主键或唯一标识
    /// </summary>
    public string Key
    {
        get { return ViewState["Key"].ToString(); }
        set { ViewState["Key"] = value; }
    }
    /// <summary>
    /// 数据库排序字段名
    /// </summary>
    public string Order
    {
        get { return ViewState["Order"].ToString(); }
        set { ViewState["Order"] = value; }
    }

    /// <summary>
    /// 数据绑定控件
    /// </summary>
    public string DGData
    {
        set { this.ViewState["dg"] = value; }
        get { return ViewState["dg"].ToString(); }
    }


    /// <summary>
    /// 要查询的列名 查询全部 可以写*
    /// </summary>
    public string Columns
    {
        get { return ViewState["Columns"].ToString(); }
        set { ViewState["Columns"] = value; }
    }

    /// <summary>
    /// 页大小

    /// </summary>
    public int PageSize
    {
        get { return int.Parse(hidPageSize.Value); }
        set { hidPageSize.Value = value.ToString(); }
    }


    /// <summary>
    /// 分页查询条件
    /// </summary>
    public string Condition
    {
        get { return hidQueryStr.Value; }
        set { hidQueryStr.Value = value; }
    }

    /// <summary>
    /// 当前页数
    /// </summary>
    public int PageIndex
    {
        get { return int.Parse(this.hidPageIndex.Value); }
        set { this.hidPageIndex.Value = value.ToString(); }
    }

    /// <summary>
    /// 总页数

    /// </summary>
    public int PageCount
    {
        get { return int.Parse(hidPageCount.Value); }
        set { hidPageCount.Value = value.ToString(); }
    }

    /// <summary>
    /// 查询出的总计录数
    /// </summary>
    public int RecordCount
    {
        get { return int.Parse(hidRecord.Value); }
        set { hidRecord.Value = value.ToString(); }
    }

    //#endregion

    #region  分页方法

    /// <summary>
    /// 当前按钮是否可用
    /// </summary>
    /// <param name="penabled"> 首页 上一页 状态</param>
    /// <param name="nenabled"> 下一页 尾页 状态</param>
    public void SetPagingButton(bool penabled, bool nenabled)
    {
        lbtnFirst.Enabled = penabled;
        lbtnPre.Enabled = penabled;
        lbtnNext.Enabled = nenabled;
        lbtnLast.Enabled = nenabled;
    }


    /// <summary>
    /// 第一页

    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtnFirst_Click(object sender, EventArgs e)
    {
        PageIndex = 1;
        PageInit();
    }

    /// <summary>
    /// 上一页

    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtnPre_Click(object sender, EventArgs e)
    {
        int cPageIndex = PageIndex - 1;
        PageIndex = cPageIndex > 0 ? cPageIndex : PageIndex;

        string btn111 = "btn" + (Convert.ToString((PageIndex) % 6) == "0" ? "6" : Convert.ToString((PageIndex) % 6));
        string btn222 = "lbtn" + (Convert.ToString((PageIndex) % 6) == "0" ? "6" : Convert.ToString((PageIndex) % 6));
        LinkButton lb = this.FindControl(btn111) as LinkButton;
        Label lb2 = this.FindControl(btn222) as Label;

        lb2.Text = "<span >" + PageIndex.ToString() + "</span>";
        lbtn1.Visible = false;
        lbtn2.Visible = false;
        lbtn3.Visible = false;
        lbtn4.Visible = false;
        lbtn5.Visible = false;
        lbtn6.Visible = false;
        lb.Visible = false;
        lb2.Visible = true;

        PageInit();
    }

    /// <summary>
    /// 下一页

    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtnNext_Click(object sender, EventArgs e)
    {
        int cPageIndex = PageIndex + 1;
        PageIndex = (cPageIndex < PageCount) ? cPageIndex : PageCount;

        string btn111 = "btn" + (Convert.ToString((PageIndex) % 6) == "0" ? "6" : Convert.ToString((PageIndex) % 6));
        string btn222 = "lbtn" + (Convert.ToString((PageIndex) % 6) == "0" ? "6" : Convert.ToString((PageIndex) % 6));
        LinkButton lb = this.FindControl(btn111) as LinkButton;
        Label lb2 = this.FindControl(btn222) as Label;

        lb2.Text = "<span >" + PageIndex.ToString() + "</span>";
        lbtn1.Visible = false;
        lbtn2.Visible = false;
        lbtn3.Visible = false;
        lbtn4.Visible = false;
        lbtn5.Visible = false;
        lbtn6.Visible = false;
        lb.Visible = false;
        lb2.Visible = true;

        PageInit();
    }

    /// <summary>
    /// 最后一页

    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtnLast_Click(object sender, EventArgs e)
    {
        PageIndex = PageCount;
        PageInit();
    }
    /// <summary>
    /// 选中页数分页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlPages_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            PageIndex = int.Parse(this.ddlPages.SelectedValue);
        }
        catch (Exception)
        {
            PageIndex = 1;
        }
        PageInit();
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        LinkButton but = (LinkButton)sender;
        PageIndex = Convert.ToInt32(but.Text.Replace("<span>", "").Replace("</span>", ""));
        string btn111 = "btn" + (Convert.ToString((PageIndex) % 6) == "0" ? "6" : Convert.ToString((PageIndex) % 6));
        string btn222 = "lbtn" + (Convert.ToString((PageIndex) % 6) == "0" ? "6" : Convert.ToString((PageIndex) % 6));
        LinkButton lb = this.FindControl(btn111) as LinkButton;
        Label lb2 = this.FindControl(btn222) as Label;
        
        lb2.Text = "<span >" + PageIndex.ToString() + "</span>";
        lbtn1.Visible = false;
        lbtn2.Visible = false;
        lbtn3.Visible = false;
        lbtn4.Visible = false;
        lbtn5.Visible = false;
        lbtn6.Visible = false;
        lb.Visible = false;
        lb2.Visible = true;

        PageInit();

        
    }
    #endregion
    protected void btn1_Click(object sender, EventArgs e)
    {
        
        string str = btn1.Text.Replace("<span>", "").Replace("</span>", "");
        
        PageIndex = Convert.ToInt32(str);

        string btn111 = "btn" + (Convert.ToString((PageIndex) % 6) == "0" ? "6" : Convert.ToString((PageIndex) % 6));
        string btn222 = "lbtn" + (Convert.ToString((PageIndex) % 6) == "0" ? "6" : Convert.ToString((PageIndex) % 6));
        LinkButton lb = this.FindControl(btn111) as LinkButton;
        Label lb2 = this.FindControl(btn222) as Label;

        lb2.Text = "<span >" + PageIndex.ToString() + "</span>";
        lbtn1.Visible = false;
        lbtn2.Visible = false;
        lbtn3.Visible = false;
        lbtn4.Visible = false;
        lbtn5.Visible = false;
        lbtn6.Visible = false;
        lb.Visible = false;
        lb2.Visible = true;

        PageInit();

    }

    protected void btn2_Click(object sender, EventArgs e)
    {
                      
        string str = btn2.Text.Replace("<span>", "").Replace("</span>", "");      
        PageIndex = Convert.ToInt32(str);

        string btn111 = "btn" + (Convert.ToString((PageIndex) % 6) == "0" ? "6" : Convert.ToString((PageIndex) % 6));
        string btn222 = "lbtn" + (Convert.ToString((PageIndex) % 6) == "0" ? "6" : Convert.ToString((PageIndex) % 6));
        LinkButton lb = this.FindControl(btn111) as LinkButton;
        Label lb2 = this.FindControl(btn222) as Label;

        lb2.Text = "<span >" + PageIndex.ToString() + "</span>";
        lbtn1.Visible = false;
        lbtn2.Visible = false;
        lbtn3.Visible = false;
        lbtn4.Visible = false;
        lbtn5.Visible = false;
        lbtn6.Visible = false;
        lb.Visible = false;
        lb2.Visible = true;

        PageInit();
    }
    protected void btn3_Click(object sender, EventArgs e)
    {                      
        string str = btn3.Text.Replace("<span>", "").Replace("</span>", "");
        
        PageIndex = Convert.ToInt32(str);

        string btn111 = "btn" + (Convert.ToString((PageIndex) % 6) == "0" ? "6" : Convert.ToString((PageIndex) % 6));
        string btn222 = "lbtn" + (Convert.ToString((PageIndex) % 6) == "0" ? "6" : Convert.ToString((PageIndex) % 6));
        LinkButton lb = this.FindControl(btn111) as LinkButton;
        Label lb2 = this.FindControl(btn222) as Label;

        lb2.Text = "<span >" + PageIndex.ToString() + "</span>";
        lbtn1.Visible = false;
        lbtn2.Visible = false;
        lbtn3.Visible = false;
        lbtn4.Visible = false;
        lbtn5.Visible = false;
        lbtn6.Visible = false;
        lb.Visible = false;
        lb2.Visible = true;

        PageInit();
    }
    protected void btn4_Click(object sender, EventArgs e)
    {

        string str = btn4.Text.Replace("<span>", "").Replace("</span>", "");
        PageIndex = Convert.ToInt32(str);

        string btn111 = "btn" + (Convert.ToString((PageIndex) % 6) == "0" ? "6" : Convert.ToString((PageIndex) % 6));
        string btn222 = "lbtn" + (Convert.ToString((PageIndex) % 6) == "0" ? "6" : Convert.ToString((PageIndex) % 6));
        LinkButton lb = this.FindControl(btn111) as LinkButton;
        Label lb2 = this.FindControl(btn222) as Label;

        lb2.Text = "<span >" + PageIndex.ToString() + "</span>";
        lbtn1.Visible = false;
        lbtn2.Visible = false;
        lbtn3.Visible = false;
        lbtn4.Visible = false;
        lbtn5.Visible = false;
        lbtn6.Visible = false;
        lb.Visible = false;
        lb2.Visible = true;

        PageInit();
    }
    protected void btn5_Click(object sender, EventArgs e)
    {

        
        string str = btn5.Text.Replace("<span>", "").Replace("</span>", "");

        PageIndex = Convert.ToInt32(str);

        string btn111 = "btn" + (Convert.ToString((PageIndex) % 6) == "0" ? "6" : Convert.ToString((PageIndex) % 6));
        string btn222 = "lbtn" + (Convert.ToString((PageIndex) % 6) == "0" ? "6" : Convert.ToString((PageIndex) % 6));
        LinkButton lb = this.FindControl(btn111) as LinkButton;
        Label lb2 = this.FindControl(btn222) as Label;

        lb2.Text = "<span >" + PageIndex.ToString() + "</span>";
        lbtn1.Visible = false;
        lbtn2.Visible = false;
        lbtn3.Visible = false;
        lbtn4.Visible = false;
        lbtn5.Visible = false;
        lbtn6.Visible = false;
        lb.Visible = false;
        lb2.Visible = true;

        PageInit();
    }
    protected void btn6_Click(object sender, EventArgs e)
    {

        
        string str = btn6.Text.Replace("<span>", "").Replace("</span>", "");

        PageIndex = Convert.ToInt32(str);

        string btn111 = "btn" + (Convert.ToString((PageIndex) % 6) == "0" ? "6" : Convert.ToString((PageIndex) % 6));
        string btn222 = "lbtn" + (Convert.ToString((PageIndex) % 6) == "0" ? "6" : Convert.ToString((PageIndex) % 6));
        LinkButton lb = this.FindControl(btn111) as LinkButton;
        Label lb2 = this.FindControl(btn222) as Label;

        lb2.Text = "<span >" + PageIndex.ToString() + "</span>";
        lbtn1.Visible = false;
        lbtn2.Visible = false;
        lbtn3.Visible = false;
        lbtn4.Visible = false;
        lbtn5.Visible = false;
        lbtn6.Visible = false;
        lb.Visible = false;
        lb2.Visible = true;

        PageInit();
    }
    
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        int pId1 = 1;
        int.TryParse(txtPn.Text.Trim(), out pId1);
        PageIndex = pId1;

        if (PageIndex>PageCount)
        {
            PageIndex = PageCount;
        }
        if (PageIndex<1)
        {
            PageIndex = 1;
        }

        string btn111 = "btn" + (Convert.ToString((PageIndex) % 6) == "0" ? "6" : Convert.ToString((PageIndex) % 6));
        string btn222 = "lbtn" + (Convert.ToString((PageIndex) % 6) == "0" ? "6" : Convert.ToString((PageIndex) % 6));
        LinkButton lb = this.FindControl(btn111) as LinkButton;
        Label lb2 = this.FindControl(btn222) as Label;

        lb2.Text = "<span >" + PageIndex.ToString() + "</span>";
        lbtn1.Visible = false;
        lbtn2.Visible = false;
        lbtn3.Visible = false;
        lbtn4.Visible = false;
        lbtn5.Visible = false;
        lbtn6.Visible = false;
        lb.Visible = false;
        lb2.Visible = true;

        PageInit();
    }
}