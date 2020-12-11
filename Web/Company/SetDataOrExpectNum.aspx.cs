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
using System.Text;
using Model.Other;
using BLL.CommonClass;
using BLL.other.Company;

/*
 * 创建者：     汪  华
 * 创建时间：   2009-09-10
 */

public partial class SetDataOrExpectNum : BLL.TranslationBase
{
    /// <summary>
    /// 为GridView排序事件声明变量和赋值
    /// </summary>
    public static bool blExpectNum = true;
    public static bool blDate = true;
    protected string msgstr;

    /// <summary>
    /// 实例化公共类业务逻辑层
    /// </summary>
    CommonDataBLL commonDataBLL = new CommonDataBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        ///设置时间
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.SystemQishuVSdate);

        ///设置GridView的样式
        gvExpectNumDate.Attributes.Add("style", "word-break:keep-all;word-wrap:normal");

        if (!Page.IsPostBack)
        {            
            RadioQishuDate.SelectedIndex  = CommonDataBLL.GetShowQishuDate();
            getInfo();
        }        
    }

    /// <summary>
    /// Translation
    /// </summary>
    protected void Translations_More()
    {        

        TranControls(btnSave, new string[][]
                        {
                            new string[]{"001124","保 存"}                            
                        }
                   );        

        TranControls(RadioQishuDate,new string[][]
                        {
                            new string[]{"000045","期数"},
                            new string[]{"001546","时间"},
                             new string[]{"007001","期数加时间"},
                        }
                    );   ;

        TranControls(gvExpectNumDate,new string[][]
                        {
                            new string[]{"000045","期数"},                            
                            new string[]{"001546","时间"},
                            new string[]{"000559","开始时间"},
                            new string[]{"005932","结束时间"},
                            new string[]{"000036","编辑"}                         
                        }
                    );
    }
  
    private void getInfo()
    {
        ///从结算表中获取所有的日期和期数
        DataTable dt = SetDataOrExpectNumBLL.GetAllExpectNumDateFromConfig();
        ViewState["sorttable"] = dt;
        DataView dv = new DataView(dt);
        if (ViewState["sortstring"] == null)
            ViewState["sortstring"] = "ExpectNum asc";
        dv.Sort = ViewState["sortstring"].ToString();

        this.gvExpectNumDate.DataSource = dv;
        this.gvExpectNumDate.DataBind();
        Translations_More();
    } 

    /// <summary>
    /// 针对导出Excel重载
    /// </summary>
    /// <param name="control"></param>
    public override void VerifyRenderingInServerForm(Control control)
    {
        
    }
    
    /// <summary>
    /// 导出Excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        DataTable dt = SetDataOrExpectNumBLL.GetAllExpectNumDateFromConfig();
        if (dt.Rows.Count < 1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001712", "对不起，没有可以导出的数据!")));
            return;
        }

        else
        {
            Excel.OutToExcel(dt, GetTran("001745", "期数对应日期信息"), new string[] { "ExpectNum=" + GetTran("000045", "期数"), "Date=" + GetTran("000613", "日期") });
        }
    }


    /// <summary>
    /// 取消编辑
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvExpectNumDate_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

        this.gvExpectNumDate.EditIndex = -1;
        this.getInfo();
    }
    
    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvExpectNumDate_RowEditing(object sender, GridViewEditEventArgs e)
    {        
        this.gvExpectNumDate.EditIndex = e.NewEditIndex;
        this.getInfo();
    }
    
    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvExpectNumDate_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {        
        int EdCount = 0;
        ///修改期数时间权限
        EdCount = Permissions.GetPermissions(EnumCompanyPermission.SystemQishuVSdateEdit);       
                
        if (EdCount.ToString() == "6222")
        {
            int index = e.RowIndex;
            string Date = ((TextBox)gvExpectNumDate.Rows[index].FindControl("TextBox1")).Text.Trim();
            int expectNum = Convert.ToInt32(gvExpectNumDate.Rows[index].Cells[0].Text.Trim());
                        
            ChangeLogs cl = new ChangeLogs("config", "ltrim(rtrim(str(ExpectNum)))");
            CommonDataBLL commonDataBLL = new CommonDataBLL();

            cl.AddRecord(expectNum);           
             
            try
            {
                string stardate =((TextBox)gvExpectNumDate.Rows[index].FindControl("txtStar")).Text.Trim();
                string enddate = ((TextBox)gvExpectNumDate.Rows[index].FindControl("txtEnd")).Text.Trim();

                if (Date == "" || stardate == "" || enddate == "")
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001720", "对不起，时间不能为空!")));
                    return;
                }

                else
                {
                    ///根据期数更改结算表中的日期              
                    int updCount = SetDataOrExpectNumBLL.UpdDateByExpectNum(Date, expectNum, stardate, enddate);
                    if (updCount > 0)
                    {
                        cl.AddRecord(expectNum);
                        cl.ModifiedIntoLogs(ChangeCategory.company37, Session["Company"].ToString(), ENUM_USERTYPE.objecttype9);
                        Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001723", "修改时间成功!")));
                    }

                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001727", "修改时间失败，请联系管理员!")));
                        return;
                    }
                    
                    gvExpectNumDate.EditIndex = -1;
                    this.getInfo();
                }        
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001922", "日期格式有问题")));
                return;            
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001734", "对不起,您没有修改权限!"))); 
            return;
        }
    }

    /// <summary>
    /// GridView行绑定事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvExpectNumDate_RowDataBound(object sender, GridViewRowEventArgs e)
    {      
        ///控制GridView样式
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");  
        }
    }
    
    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //Rights
        if (Permissions.GetPermissions(EnumCompanyPermission.SystemQishuVSdate) != 6221)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001734", "对不起,您没有修改权限!")));
            return;
        }

        else
        { 
            //更改数据字典的值
            int updCount = SetDataOrExpectNumBLL.UpdFileValueByCode(Convert.ToInt32(this.RadioQishuDate.SelectedValue));
            if (updCount > 0)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("000222", "修改成功!")));
            }

            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001740", "修改失败，请联系管理员!")));
                return;
            }
        }
    }
    
    /// <summary>
    /// GridView排序事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvExpectNumDate_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataView dv = new DataView((DataTable)ViewState["sorttable"]);
        string sortString=e.SortExpression;

        switch(sortString.ToLower().Trim())
        {
            case "expectnum":
                if(blExpectNum)
                {
                    dv.Sort="ExpectNum desc";
                    blExpectNum=false;
                }

                else
                {
                    dv.Sort="ExpectNum asc";
                    blExpectNum=true;
                }
                break;

            case "date":
                if(blDate)
                {
                    dv.Sort="Date desc";
                    blDate=false;
                }

                else
                {
                    dv.Sort="Date asc";
                    blDate=true;
                }
                break;
        }
        this.gvExpectNumDate.DataSource=dv;
        this.gvExpectNumDate.DataBind();
    }
}
