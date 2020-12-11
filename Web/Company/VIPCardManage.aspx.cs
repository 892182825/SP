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

using BLL.MoneyFlows;
using Model;
using Model.Other;
using BLL.CommonClass;
public partial class Company_VIPCardManage : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.BalanceVIPCardManage);

        Response.Cache.SetExpires(DateTime.Now);
        if (!IsPostBack)
        { 
            btn_Query_Click1(null, null);
        }
        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.DataGrid1,
               new string[][]{
                    new string []{"001462","范围编号"},
                    new string []{"001466", "取消分配"},
                    new string []{"001467", "重新分配"},
                    new string []{"000037", "店编号"},
                    new string []{"001469", "起始卡号"},
                    new string []{"001470", "结束卡号"},
                    new string []{"001472", "分配状态"}});
        this.TranControls(this.dropSortBy,
                new string[][]{
                    new string []{"001462","范围编号"},
                    new string []{"000037", "店编号"}});
        this.TranControls(this.DataGrid2,
                    new string[][]{
                    new string []{"001462", "范围编号"},
                    new string []{"000037", "店编号"},
                    new string []{"001469", "起始卡号"},
                    new string []{"001469", "结束卡号"},
                    new string []{"001472", "分配状态"}});
       this.TranControls(this.btn_Query, new string[][] { new string[] { "000048", "查 询" } });
       this.TranControls(this.Button1, new string[][] { new string[] { "001521", "新增卡号范围" } });

    }

    /// <summary>
    /// 绑定
    /// </summary>
    public void bind()
    {
        string condition = string.Empty;
        string mark = string.Empty;
        if (txt_StoreID.Text.Trim() == "")
        {

            mark = "LIKE";
        }
        else
        {
            mark = "=";
        }
        condition = DisposeString.DisString(this.txt_StoreID.Text.Trim(), "<,>,',-", "&lt;,&gt;,&#39;,&nbsp;", ",");
        //VIPCardManageBll cardbll = new VIPCardManageBll();
        //this.DataGrid1.DataSource = cardbll.GetCardRange(condition, mark);
        //this.DataBind();
        string sql = " 1=1 ";
        if (mark == "LIKE")
        {
            sql = sql + "  and  StoreID  like '%" + condition + "%'";
        }
        else
        {
            sql = sql + "  and  StoreID = '" + condition + "'";
        }
        string isfenpei = GetTran("005603", "已分配");
        string notfenpei = GetTran("005601", "未分配");
        this.Pager1.ControlName = "DataGrid1";
        this.Pager1.key = "id";
        this.Pager1.PageColumn = "  id,RangeID,StoreID,BeginCard,EndCard,inuse,CASE inuse WHEN 1 THEN '" + isfenpei + "' ELSE '" +notfenpei+ "' END AS _inuse  ";
        this.Pager1.Pageindex = 0;
        this.Pager1.PageTable = "VIPCardRange";
        this.Pager1.Condition = sql;
        this.Pager1.PageSize = 10;
        this.Pager1.PageCount = 0;
        this.Pager1.PageBind();

        ViewState["SQLSTR"] = "select id,RangeID,StoreID,BeginCard,EndCard,inuse,CASE inuse WHEN 1 THEN '" + isfenpei + "' ELSE '" + notfenpei + "' END AS _inuse from VIPCardRange where " + sql + " order by id desc";
        Translations();
    }
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_Query_Click1(object sender, EventArgs e)
    {
        bind();
        int MaxVIPCard = CommonDataBLL.getMaxqishu();
        int CurrentBeginCard = MaxVIPCard + 1;
        txt_BeginCard.Text = CurrentBeginCard.ToString();
        txt_EndCard.Text = txt_BeginCard.Text;
        txt_CardCount.Text = (Convert.ToInt32(txt_EndCard.Text) - Convert.ToInt32(txt_BeginCard.Text) + 1).ToString();
        Translations();
    }
    /// <summary>
    /// 新增卡号
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {

         int RangeID = CommonDataBLL.getMaxqishu() + 1;
        string ToStoreID = "";
        string inuse = "0";
        int count = VIPCardManageBll.valiStore(DisposeString.DisString(this.txt_ToStoreID.Text.Trim(), "<,>,',-", "&lt;,&gt;,&#39;,&nbsp;", ","));
        if (count == 0)
        {
            ToStoreID = "";
            inuse = "0";
            ScriptHelper.SetAlert(Page, GetTran("000388", "店铺") + txt_ToStoreID.Text.Trim() + GetTran("001584", "不存在！"));
            return;
        }
        else
        {
            ToStoreID = txt_ToStoreID.Text.Trim();
            inuse = "1";
        }
        VIPCardRangeModel vipmodel = new VIPCardRangeModel();
        vipmodel.RangeID = RangeID;
        vipmodel.StoreID = ToStoreID;
        vipmodel.BeginCard =Convert.ToInt32(txt_BeginCard.Text.Trim());
        vipmodel.EndCard = Convert.ToInt32(txt_EndCard.Text.Trim());
        vipmodel.InUse =Convert.ToInt32(inuse);
        int vipcard = VIPCardManageBll.Addvipcard(vipmodel);
        if (vipcard > 0)
        {
            ScriptHelper.SetAlert(Page, GetTran("001583", "新增卡号范围成功，") + ((inuse == "1") ? GetTran("001581", "分配到店铺") + ToStoreID : GetTran("001580", "未分配到店铺")));
        }
        else
        {
            ScriptHelper.SetAlert(Page, GetTran("001541", "操作失败！！！"));
            return;
        }
       
        btn_Query_Click1(null, null);
        Translations();
    }

    /// <summary>
    /// 导出EXCEL
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Download_Click(object sender, EventArgs e)
    {
        string sql = ViewState["SQLSTR"].ToString();
        DataTable dt = DAL.DBHelper.ExecuteDataTable(sql);
        //foreach (DataRow row in dt.Rows)
        //{
        //    row["Name"] = Encryption.Encryption.GetDecipherName(row["Name"].ToString());
        //    row["StoreName"] = Encryption.Encryption.GetDecipherName(row["StoreName"].ToString());

        //    try
        //    {
        //        row["RegisterDate"] = Convert.ToDateTime(row["RegisterDate"]).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString();
        //    }
        //    catch { }

        //}

        Excel.OutToExcel(dt, GetTran("001440", "编号分配"), new string[] { "RangeID=" + GetTran("001462", "范围编号"), "StoreID=" + GetTran("000037", "服务机构编号"), "BeginCard=" + GetTran("001469", "起始卡号"), "EndCard=" + GetTran("001470", "结束卡号"), "_inuse=" + GetTran("001472", "分配状态") });
 


        //Translations();
        //Response.Clear();
        //Response.Buffer = true;
        //Response.Charset = "utf-8";
        //Response.AppendHeader("Content-Disposition", "attachment;filename=HelloAdmin.xls;charset=utf-8");
        //Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");//设置输出流为简体中文
        //Response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。 
        //this.EnableViewState = false;
        //System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("ZH-CN", true);
        //System.IO.StringWriter oStringWriter = new System.IO.StringWriter(myCItrad);
        //System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
        //this.DataGrid1.Columns[1].Visible = false;
        //this.DataGrid1.Columns[2].Visible = false;
        //this.DataGrid1.RenderControl(oHtmlTextWriter);
        //Response.Write(oStringWriter.ToString());
        //Response.End();
    }

    protected void DataGrid1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
            int DeCount = 0, EdCount = 0;
            DeCount = Permissions.GetPermissions(Model.Other.EnumCompanyPermission.BalanceVIPCardManageAdd);
            EdCount = Permissions.GetPermissions(Model.Other.EnumCompanyPermission.BalanceVIPCardManageReset);
            LinkButton lkbtn_update = (LinkButton)e.Row.FindControl("lkbtn_update");
            if (EdCount.ToString() == "131")
            {
                lkbtn_update.Visible = true;
                lkbtn_update.Attributes["onmousedown"] = "return JudgeWhenUpdate(this);";
            }
            else
            {
                lkbtn_update.Visible = false;
            }
            if (DeCount.ToString() == "130")
            {
                ((LinkButton)e.Row.FindControl("lkbtn_delete")).Visible = true;
            }
            else
            {
                ((LinkButton)e.Row.FindControl("lkbtn_delete")).Visible = false;
            }
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Attributes.Add("style", "background-image:url('images/tabledp.gif')");
        }
        Translations();
    }
    protected void DataGrid1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow Row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
        if (e.CommandName.ToString().Trim() == "Sort")
            return;

        int RangeID;
        try
        {
            RangeID = Convert.ToInt32(Row.Cells[0].Text);
        }
        catch
        {
            RangeID = 0;
        }

        int BeginCard;
        try
        {
            BeginCard = Convert.ToInt32(Row.Cells[4].Text);
        }
        catch
        {
            BeginCard = 0;
        }

        int EndCard;
        try
        {
            EndCard = Convert.ToInt32(Row.Cells[5].Text);
        }
        catch
        {
            EndCard = 0;
        }

        string ToStoreID = ((TextBox)Row.FindControl("txt_ToStore")).Text.Trim();
        int count1 = VIPCardManageBll.BetweenCard(Convert.ToInt32(BeginCard.ToString()), Convert.ToInt32(EndCard.ToString()));
        switch (e.CommandName.Trim())
        {
            case "DELETE":
                if (count1 > 0)
                {
                    ScriptHelper.SetAlert(Page, GetTran("001578", "该卡号段中的一些卡号已分配到会员，不能取消分配"));
                    return;
                }
                VIPCardManageBll.Uptvipcard(Convert.ToInt32(RangeID.ToString().Trim()));
                ScriptHelper.SetAlert(Page, GetTran("001577", "卡号范围“") + RangeID.ToString() + GetTran("001576", "”取消分配成功！"));
                break;
            case "UPDATE":
                if (count1 > 0)
                {
                    ScriptHelper.SetAlert(Page, GetTran("001573", "该卡号段中的一些卡号已分配到会员，不能重新分配"));
                    return;
                }
                int count2 = VIPCardManageBll.valiStore(ToStoreID);
                if (count2 == 0)
                {
                    ScriptHelper.SetAlert(Page, GetTran("001571", "店铺“") + ToStoreID + GetTran("001569", "”不存在，不能将卡号范围重分配到该店铺"));
                    return;
                }
                VIPCardManageBll.Uptvipcardstore(Convert.ToInt32(ToStoreID), RangeID.ToString().Trim());
                ScriptHelper.SetAlert(Page, GetTran("001568", "卡号范围“") + RangeID.ToString() + GetTran("001565", "”重分配到店铺“") + ToStoreID + GetTran("001563", "”成功！"));
                break;
        }
        btn_Query_Click1(null, null);
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }
}
