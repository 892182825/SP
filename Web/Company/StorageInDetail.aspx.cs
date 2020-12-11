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
using BLL.CommonClass;
using DAL;
using Model.Other;

public partial class Company_StorageInDetail : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        gvProduct.Attributes.Add("style", "word-break:keep-all;word-wrap:normal");
       

        if (Request.QueryString["ID"] == null)
        {            
            Page.ClientScript.RegisterStartupScript(GetType(),"",Transforms.ReturnAlert("入库单号不存在!"));
            Response.End();
        }

        if (!IsPostBack)
        {
             DataTable dt = StorageInBrowseBLL.getStoageInDetails(Convert.ToString(Request.QueryString["ID"]));
             ViewState["dt"] = dt;
             this.gvProduct.DataSource = dt;
             this.gvProduct.DataBind();


             DataTable dts = StorageInBrowseBLL.getStoageIn(Request.QueryString["ID"]);

             this.gvInfo.DataSource = dts;
             this.gvInfo.DataBind();

             
        }
        Translations_More();
    }

    
   
    protected string SetVisible(string dd, string id, string _pageUrl)
    {
        if (dd.Length > 0)
        {
            string _openWin = "<a href =\"javascript:void(window.open('" + _pageUrl + "?ID=" + id + "','','left=300,top=150,width=250,height=150'))\">" + GetTran("000440", "查看") + "</a>";
            return _openWin;
        }
        else
        {
            return GetTran("000221", "无");
        }
    }

    protected void gvInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string DocID = Convert.ToString(e.CommandArgument);
        int isExist = StorageInBrowseBLL.DocIdIsExistByDocId(DocID);
        //Exist
        if (isExist > 0)
        {
            if (e.CommandName == "Auditing")
            {
                int isAuditing = StorageInBrowseBLL.IsAuditingByDocId(DocID, 1);
                //Effective(In other words,Auditing)
                if (isAuditing > 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("006888", "该入库单已经被审核！")));
                }

                //No Auditing
                else
                {
                    string DocAuditer = CommonDataBLL.GetNameByAdminID(Session["Company"].ToString());
                    DateTime DocAuditTime = MYDateTime1.GetCurrentDateTime();
                    string OperateIP = CommonDataBLL.OperateIP;
                    string OperateNum = CommonDataBLL.OperateBh;

                    //更新公司库存
                    GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                    string TempWareHouseID = ((HtmlInputHidden)row.FindControl("hidwarehouseId")).Value;
                    string aaa = ((HtmlInputHidden)row.FindControl("changwei")).Value;
                    int changwei = Convert.ToInt32(aaa);
                    int auditingCout = StorageInBrowseBLL.checkDoc(DocAuditer, DocAuditTime, OperateIP, OperateNum, DocID, TempWareHouseID, changwei);
                    if (auditingCout > 0)
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>if(confirm('" + GetTran("002214", "入库单审核成功，是否要打印此入库单?") + "'))window.open('docPrint.aspx?DocID=" + DocID + "');</script>");
                    }

                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002216", "入库单审核失败，请联系管理员!")));
                        return;
                    }
                }
            }

            else if (e.CommandName == "NoEffect")
            {
                int isEffect = StorageInBrowseBLL.IsAuditingByDocId(DocID, 0);
                //No effect
                if (isEffect <= 0)
                {
                    DateTime CloseDate = MYDateTime1.GetCurrentDateTime();
                    string OperateIP = CommonDataBLL.OperateIP;
                    string OperateNum = CommonDataBLL.OperateBh;
                    int noEffectCount = StorageInBrowseBLL.updDocTypeName(CloseDate, DocID, OperateIP, OperateNum);
                    if (noEffectCount > 0)
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002218", "此入库单审核无效成功!")));
                    }

                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002221", "此入库单审核无效失败，请联系管理员!")));
                    }
                }

                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("006890", "该入库单已经被审核无效！")));
                }
            }

            else if (e.CommandName == "Del")
            {
                int delCount = StorageInBrowseBLL.delDoc(DocID);
                if (delCount > 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002225", "入库单删除成功！")));
                }

                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002228", "入库单编辑失败，请联系管理员！")));
                    return;
                }
            }

            else if (e.CommandName == "Edit")
            {
                Response.Redirect("StorageInEdit.aspx?billID=" + DocID);
            }
        }

        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("006894", "该入库单不存在！")));
        }
        Response.Redirect("StorageInBrowse.aspx");
    }

    protected void gvInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
            int AudCount = 0, NullCount = 0, EdiCount = 0, DelCount = 0;
            AudCount = Permissions.GetPermissions(EnumCompanyPermission.StorageStorageInBrowseAuditing);
            DelCount = Permissions.GetPermissions(EnumCompanyPermission.StorageStorageInBrowseDelete);
            EdiCount = Permissions.GetPermissions(EnumCompanyPermission.StorageStorageInBrowseEdit);
            NullCount = Permissions.GetPermissions(EnumCompanyPermission.StorageStorageInBrowseNouse);
            if (AudCount.ToString() == "2203")
            {
                ((LinkButton)e.Row.FindControl("btnAuditing")).Enabled = true;
                ((LinkButton)e.Row.FindControl("btnAuditing")).Attributes.Add("onclick", "return confirm('" + GetTran("002234", "您确认要审核入库单吗？") + "');");
            }

            else
            {
                ((LinkButton)e.Row.FindControl("btnAuditing")).Enabled = false;
            }

            if (NullCount.ToString() == "2204")
            {
                ((LinkButton)e.Row.FindControl("btnnouse")).Enabled = true;
                ((LinkButton)e.Row.FindControl("btnnouse")).Attributes.Add("onclick", "return confirm('" + GetTran("002239", "您确认此入库单不予审核吗？") + "');");
            }

            else
            {
                ((LinkButton)e.Row.FindControl("btnnouse")).Enabled = false;
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002242", "对不起，你没有入库单不予审核权限!")));
            }

            if (DelCount.ToString() == "2205")
            {
                ((LinkButton)e.Row.FindControl("btndelete")).Enabled = true;
                ((LinkButton)e.Row.FindControl("btndelete")).Attributes.Add("onclick", "return confirm('" + GetTran("002243", "您确认要删除此入库单吗？") + "');");
            }
            else
            {
                ((LinkButton)e.Row.FindControl("btndelete")).Enabled = false;
            }

            if (EdiCount.ToString() == "2206")
            {
                ((LinkButton)e.Row.FindControl("btnedit")).Enabled = true;
            }

            else
            {
                ((LinkButton)e.Row.FindControl("btnedit")).Enabled = false;
            }
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Attributes.Add("style", "background-image:url('images/tabledp.gif')");
        }

        if (e.Row.RowType == DataControlRowType.Header)
        {
            Translations_More();
        }
    }

    public string GetCurrencyName(string Currency)
    {
        string CurrencyName = CurrencyDAL.GetJieCheng(int.Parse( Currency));
        return CurrencyName;
    }

    protected string GetWarehouseName(string WareHouseId)
    {
        string WarehouseName = StorageInBrowseBLL.GetWarehouseName(WareHouseId);
        return WarehouseName;
    }
    public static string GetDepotSeatName(string DepotSeatID)
    {
        string DepotSeatName = StorageInBrowseBLL.GetDepotSeatName(DepotSeatID);
        return DepotSeatName;
    }

    /// <summary>
    /// Translation
    /// </summary>
    protected void Translations_More()
    {

        TranControls(gvInfo, new string[][] 
                        {
                            new string[] { "002164","审核入库单" }, 
                            new string[] { "000339","详细" },
                            new string[] { "002166","入库单号" }, 
                            new string[] { "000045","期数" },
                            new string[] { "000605","是否审核" }, 
                            new string[] { "001811","是否失效" },
                            new string[] { "000041","总金额" }, 
                            new string[] { "000113","总积分" }, 
                            new string[] { "002131","入库制单人" },
                            new string[] { "000655","审核人" }, 
                            new string[] { "001599","审核日期" },
                            new string[] { "002020","供应商" }, 
                            new string[] { "002021","业务员" },
                            new string[] { "002040","购货地址" }, 
                            new string[] { "000355","仓库名称" },
                            new string[] { "000357","库位名称" },
                            new string[] { "000658","批次" }, 
                            new string[] { "002167","入库单日期" },
                            new string[] { "000744","查看备注" }                           
                        }
                   );   

        TranControls(gvProduct,new string[][]
                        {
                            new string[]{"000263","产品编码"},
                            new string[]{"000501","产品名称"},
                            new string[]{"000505","数量"},
                            new string[]{"000518","单位"},
                            new string[]{"000045","期数"},
                            new string[]{"000503","单价"},
                            new string[]{"000414","积分"}
                        }
                    );
    }

    protected string GetProductName(string productId)
	{
        CommonDataBLL cd = new CommonDataBLL();
        return CommonDataBLL.GetProductNameByID(productId);  
	}

    protected string GetProductcode(string productid)
    {
        CommonDataBLL cd = new CommonDataBLL();
        return CommonDataBLL.GetProductNameCode(productid);
    }

    /// <summary>
    /// Override
    /// </summary>
    /// <param name="control"></param>
    public override void VerifyRenderingInServerForm(Control control)
    {
    }

    protected void gvProduct_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Attributes.Add("style", "background-image:url('images/tabledp.gif')");
        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        this.gvProduct.AllowSorting = false;
        this.gvProduct.AllowPaging = false;
        DataTable dt = (DataTable)ViewState["dt"];
        this.gvProduct.DataSource = dt.DefaultView;
        this.gvProduct.DataBind();

        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "GB2312";
        Response.AppendHeader("Content-Disposition", "attachment;filename=HelloAdmin.xls");
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");//设置输出流为简体中文

        Response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。 
        this.EnableViewState = false;
        System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("ZH-CN", true);
        System.IO.StringWriter oStringWriter = new System.IO.StringWriter(myCItrad);
        System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
        this.gvProduct.RenderControl(oHtmlTextWriter);
        Response.Write(oStringWriter.ToString());
        Response.End();
    }
}
