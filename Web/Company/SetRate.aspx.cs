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
using Model;
using Model.Other;
using BLL.other.Company;
using BLL.CommonClass;
using BLL.Logistics;
using System.Collections.Generic;

/*
 * 创建者：     汪  华
 * 创建时间：   2009-09-11
 * 对应菜单：   系统管理->各汇率设置
 */

public partial class Company_SetRate : BLL.TranslationBase 
{
    /// <summary>
    /// 为GridView排序事件声明变量和赋值
    /// </summary>
    private static bool blID = true;
    private static bool blCurrencyName = true;
    private static bool blRate = true;
    protected string msg;

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        ///检查相应权限
        Response.Cache.SetExpires(DateTime.Now);        
        Permissions.CheckManagePermission(EnumCompanyPermission.SystemCurrencyRate);

        ///设置GridView的样式
        gvCurrency.Attributes.Add("style", "word-break:keep-all;word-wrap:normal");        

        if (!IsPostBack)
        {
            btnUpdateRate.Attributes["realvalue"] = "0";
            btnUpdateRate.Attributes["onclick"] = "if(navigator.userAgent.toLowerCase().indexOf('ie')!=-1){this.realvalue   ++;if(this.realvalue==1){if (confirm('" + GetTran("006592", "您确定同步吗？") + "')) {return true;} else {this.realvalue=0; return false;}}else{alert('" + GetTran("006593", "数据同步中...请稍后...") + "');return false}}else{int(this.btnReg.getAttribute['realvalue'])   ++;if(int(this.btnReg.getAttribute['realvalue'])==1){if (confirm(\"" + GetTran("006592", "您确定同步吗？") + "\")) {return true;} else {int(this.btnReg.getAttribute['realvalue'])=0; return false;}}else{alert(\"" + GetTran("006593", "数据同步中...请稍后...") + "\");return false}}";

            this.btnAddNewCurrency.Attributes.Add("OnClick", "return confirm('" + GetTran("002093", "你确定添加该货币吗?") + "');");

            GetCurrencyInfo();
        }
        Translations();
    }

    /// <summary>
    /// 是否启用
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    protected object GetFlag(object obj)
    {
        string flag="";
        if (obj != null)
        {
            if (obj.ToString() == "1")
            {
                flag = GetTran("000233", "是");
            }
            else if(obj.ToString()=="0")
            {
                flag = GetTran("000235", "否");
            }
        }
        return flag;
    }

    /// <summary>
    /// 是否启用
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    protected object GetFlagEdit(object obj)
    {
        bool flag = false;
        if (obj != null)
        {
            if (obj.ToString() == "1")
            {
                flag = true;
            }
            else if (obj.ToString() == "0")
            {
                flag = false;
            }
        }
        return flag;
    }

    private void Translations() 
    {
        this.TranControls(this.btnUpdateRate, new string[][] { new string[] { "006596", "同步汇率" } });
        this.TranControls(this.btnAddNewCurrency, new string[][] { new string[] { "002047", "添 加" } });
        this.TranControls(this.gvCurrency,
                new string[][]{
                    new string []{"000036","编辑"},
                    new string []{"002053","汇率编号"},
                    new string []{"006610","货币全称"},
                    new string []{"002041","货币名称"},
                    new string []{"002056","汇率"},
                    new string []{"006609","是否启用"}
                });
        this.TranControls(this.rdSetCurr, new string[][]{
            new string[]{"007968","系统汇率"},
            new string[]{"007969","即时汇率"}
        });
        this.TranControls(this.btnSetCurr, new string[][] { new string[] { "007970", "设置汇率" } });
    }
    /// <summary>
    /// 获取汇率信息
    /// </summary>
    private void GetCurrencyInfo()
    {
        if (Application["CurrencySetting"] == null)
        {
            rdSetCurr.SelectedValue = "0";
        }
        else if (Application["CurrencySetting"].ToString() == "Currency")
        {
            rdSetCurr.SelectedValue = "1";
        }
        ///获取汇率相关信息
        this.Pager1.PageBind(0, 10, "Currency", " id,[Name] as CurrencyName,Rate,bzflag as Flag ,quancheng", " 1=1 ", " id ", "gvCurrency");

        Translations();
    }    

    /// <summary>
    /// 针对导出Excel重载
    /// </summary>
    /// <param name="control"></param>
    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }   
    
    /// <summary>
    /// 导出汇率至Excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExcelCurrency_Click(object sender, EventArgs e)
    {
        DataTable dt=SetRateBLL.OutToExcel_Currency();
        if(dt.Rows.Count<1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001712","没有可以导出的数据!")));
            return;            
        }

        else
        {
            Excel.OutToExcel(dt, GetTran("002064", "汇率信息"), new string[] { "Name=" + GetTran("002041", "货币名称"), "Rate=" + GetTran("002056", "汇率") });            
        }        
    }
   
    /// <summary>
    /// 添加汇率
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddNewCurrency_Click(object sender, EventArgs e)
    {
        //if (txtNewCurrency.Text.Trim() == "")
        //{
        //    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002071", "货币名称不能为空!")));
        //    return;
        //}

        /////通过汇率名称获取行数
        //int getCount = SetRateBLL.GetCurrencyCountByCurrencyName(this.txtNewCurrency.Text.Trim());
        //if (getCount > 0)
        //{
        //    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002072", "货币名称已经存在!")));
        //    return;
        //}

        //int id = 0;
        /////向汇率表中插入记录
        //int addCount=SetRateBLL.AddCurrency(this.txtNewCurrency.Text.Trim(),out id);
        //if (addCount > 0)
        //{
        //    new Languages().AddNewTranslationRecord("Currency", "Name", id, this.txtNewCurrency.Text, this.txtNewCurrency.Text);
        //    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002074", "添加货币成功!")));
        //}

        //else
        //{
        //    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002075", "添加货币失败，请联系管理员!")));
        //}       

        /////获取汇率表中部分汇率ID和币种名称 
        //txtNewCurrency.Text = "";
        //GetCurrencyInfo();
    }
    
    /// <summary>
    /// 获取汇率表的主键ID
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvCurrency_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowIndex!=-1)
        {
            int currencyID=Convert.ToInt32(this.gvCurrency.DataKeys[e.Row.RowIndex].Value.ToString());
        }

        ///控制GridView样式
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {            
            Translations();
        }
    }
    
    /// <summary>
    /// 删除汇率记录
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvCurrency_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DelCurrency")
        {
            if (SetRateBLL.GetCountryByRateID(Convert.ToInt32(e.CommandArgument)) < 1)
            {
                ///删除指定的汇率记录
                int delCount = SetRateBLL.DelCurrencyByID(Convert.ToInt32(e.CommandArgument));
                if (delCount > 0)
                {
                    new Languages().RemoveTranslationRecord("Currency", Convert.ToInt32(e.CommandArgument));
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002077", "删除汇率成功!")));
                }

                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002080", "删除汇率失败,请联系管理员!")));
                }
            }
            else 
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002083", "删除汇率失败,该汇率已经有相关联的国家!")));
            }
            GetCurrencyInfo();
        }
    }    

    /// <summary>
    /// 取消汇率编辑
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvCurrency_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        this.gvCurrency.EditIndex = -1;
        GetCurrencyInfo();
    }
    
    /// <summary>
    /// 更新汇率编辑
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvCurrency_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int index = gvCurrency.EditIndex;
        int id = Convert.ToInt32(gvCurrency.Rows[index].Cells[1].Text.Trim());

        ///通过币种名称联合查询获取行数
        int getCount = SetRateBLL.GetMoreCurrencyCountByCurrencyName(gvCurrency.Rows[index].Cells[3].Text.Trim());
        if (getCount > 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002085", "对不起,此货币已经使用,不能修改!")));
            return;
        }
        int EdCount_Money = 0;        
        
        EdCount_Money = Permissions.GetPermissions(EnumCompanyPermission.SystemCurrencyRateCountryEdit);

        if (EdCount_Money.ToString() == "6219")
        {    
            ///实例化构造函数
            ChangeLogs cl = new ChangeLogs("Currency", "ltrim(rtrim(str(ID)))");           
            
            TextBox TextBox_rate = (TextBox)gvCurrency.Rows[index].FindControl("TextBox_rate");                 
            cl.AddRecord(id);
            ///根据汇率ID更改汇率
            decimal rate;
            try
            { 
                rate= Convert.ToDecimal(TextBox_rate.Text.Trim());
                if (rate < 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("006949", "对不起！您输入的汇率不能小于零!")));
                    return;
                }
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002086", "对不起！您输入的汇率格式不正确!")));
                return;
            }

            int flag = 0;
            if ((gvCurrency.Rows[index].FindControl("chkFlag") as CheckBox).Checked)
            {
                flag = 1;
            }

            int updCount=SetRateBLL.UpdCurrencyRateByID(rate, id,flag);
            if (updCount > 0)
            {
                cl.AddRecord(id);
                cl.ModifiedIntoLogs(ChangeCategory.company36, Session["Company"].ToString(), ENUM_USERTYPE.objecttype9);

                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002087", "汇率更新成功!")));                
            }

            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002089", "汇率更新失败，请联系管理员!")));
                return;
            }

            
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002090", "对不起！您没有更新权限!")));
            return;
        }        

        gvCurrency.EditIndex = -1;
        GetCurrencyInfo();      
    }
   
    /// <summary>
    /// 汇率编辑
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvCurrency_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvCurrency.EditIndex = e.NewEditIndex;
        this.GetCurrencyInfo();
    }  
    
    /// <summary>
    /// GridView排序事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvCurrency_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataView dv = new DataView((DataTable)ViewState["sortrage"]);
        string sortString = e.SortExpression;

        switch (sortString.ToLower().Trim())
        { 
            case "id":
                if (blID)
                {
                    dv.Sort = "ID desc";
                    blID = false;
                }

                else
                {
                    dv.Sort = "ID asc";
                    blID = true;
                }
                break;

            case "currencyname":
                if (blCurrencyName)
                {
                    dv.Sort = "CurrencyName desc";
                    blCurrencyName = false;
                }

                else
                {
                    dv.Sort = "CurrencyName asc";
                    blCurrencyName = true;
                }
                break;

            case "rate":
                if (blRate)
                {
                    dv.Sort = "Rate desc";
                    blRate = false;
                }

                else
                {
                    dv.Sort = "Rate asc";
                    blRate = true;
                }
                break;           
        }
        this.gvCurrency.DataSource = dv;
        this.gvCurrency.DataBind();
    }


    protected void btnUpdateRate_Click(object sender, EventArgs e)
    {
        IList<CurrencyModel> list = new List<CurrencyModel>();
        string curr = SetRateBLL.GetDefaultCurrency();
        foreach (DataRow dr in SetRateBLL.GetAllCurrency().Rows)
        {
            CurrencyModel cm = new CurrencyModel();
            cm.JianCheng= dr["jiecheng"].ToString();
            net.webservicex.www.CurrencyConvertor CC = new net.webservicex.www.CurrencyConvertor();
            cm.Rate = CC.ConversionRate((net.webservicex.www.Currency)Enum.Parse(typeof(net.webservicex.www.Currency), cm.JianCheng), (net.webservicex.www.Currency)Enum.Parse(typeof(net.webservicex.www.Currency), curr));
            list.Add(cm);
        }

        if (SetRateBLL.UpdCurrencyRateAll(list))
        {
            msg = "<script>alert('" + GetTran("006594", "同步成功！") + "');</script>";
            GetCurrencyInfo();
        }
        else 
        {
            msg = "<script>alert('" + GetTran("006595", "同步失败！") + "');</script>";
        }
    }

    protected void btnSetCurr_Click(object sender, EventArgs e)
    {
        if (rdSetCurr.SelectedValue == "0")
        {
            Application.Remove("CurrencySetting");

            if(Application["CurrencySetting"] != null)
            {
                Application["CurrencySetting"] = null;
            }
        }
        else
        {
            Application["CurrencySetting"] = "Currency";
        }
    }
}
