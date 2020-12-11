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
using BLL.other.Company;
using BLL.CommonClass;
using Model;
using System.Text;
using BLL.MoneyFlows;
using System.Collections.Generic;
using Model.Other;
using DAL;
public partial class Company_SetParams_WareHouseDepotSeat : BLL.TranslationBase
{
    /// <summary>
    /// 实例化仓库模型层
    /// </summary>
    WareHouseModel wareHouseModel = new WareHouseModel();

    /// <summary>
    /// 实例化库位模型层
    /// </summary>
    DepotSeatModel depotSeatModel = new DepotSeatModel();

    /// <summary>
    /// 为GridView声明变量和赋值
    /// </summary> 

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, "../index.aspx");

        ///设置GridView的样式
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        this.gvWareHouse.Attributes.Add("style", "word-break:keep-all;word-wrap:normal");
        if (!Page.IsPostBack)
        {
            this.tab1.Style.Add("display", "none");
            BindCountry_List();
            ViewState["AddOrEdit"] = 0;
            BindCountry();
            DataBindWareHouseInfo();
        }
        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.gvWareHouse,
                new string[][]{
                    new string []{"000015","操作"},
                    new string []{"000015","操作"},
                    new string []{"000253","仓库编号"},
                    new string []{"001690","所属国家"},
                    new string []{"000355","仓库名称"},
                    new string []{"001675","仓库简称"},
                    new string []{"001678","仓库负责人"},
                    new string []{"001679","仓库所在地"},
                    new string []{"001680","描述"},
                    new string []{"001683","权限控制"}
                });
        this.TranControls(this.Button1, new string[][] { new string[] { "001713", "添加仓库" } });
        this.TranControls(this.Button5, new string[][] { new string[] { "000421", "返回" } });
        this.TranControls(this.btnReset, new string[][] { new string[] { "001759", "清 空" } });
        TranControls(Button2, new string[][] { new string[] { "000340", "查询" } });
    }

    /// <summary>
    /// Bind the CountryInfo
    /// </summary>
    protected void BindCountry()
    {
        ddlCountry1.DataSource = SetParametersBLL.GetCountryIDCodeNameOrderByID();
        ddlCountry1.DataTextField = "Name";
        ddlCountry1.DataValueField = "CountryCode";
        ddlCountry1.DataBind();
    }
    //绑定国家
    private void BindCountry_List()
    {
        IList<CountryModel> list = RemittancesBLL.BindCountry_List();
        this.DropDownList1.DataSource = list;
        this.DropDownList1.DataTextField = "Name";
        this.DropDownList1.DataValueField = "CountryCode";
        this.DropDownList1.DataBind();
        this.DropDownList1.Items.Add(new ListItem(GetTran("000633", "全部"), "-1"));
        this.DropDownList1.SelectedValue = "-1";
    }

    /// <summary>
    /// 绑定仓库信息
    /// </summary>
    private void DataBindWareHouseInfo()
    {
        StringBuilder condition = new StringBuilder();
        condition.Append(" a.CountryCode=b.CountryCode ");
        if (DropDownList1.SelectedValue != "-1")
        {
            condition.Append(" and a.CountryCode='" + DropDownList1.SelectedValue + "' ");
        }
        if (txtWareHouseName_S.Text != "")
        {
            condition.Append(" and a.WareHouseName like '%" + txtWareHouseName_S.Text.Trim() + "%'");
        }
        if (txtWareHouseForShort_S.Text != "")
        {
            condition.Append(" and a.WareHouseForShort like '%" + txtWareHouseForShort_S.Text.Trim() + "%'");
        }
        if (txtWareHouseAddress_S.Text != "")
        {
            condition.Append(" and a.WareHouseAddress like '%" + txtWareHouseAddress_S.Text.Trim() + "%'");
        }
        string cloumns = " a.WareHouseID,b.[Name],a.CountryCode,a.WareHouseName,a.WareHouseForShort,a.WareHousePrincipal,a.WareHouseAddress,a.WareHouseDescr,a.WareControl ";
        string key = "a.WareHouseID";
        this.Pager1.ControlName = "gvWareHouse";
        this.Pager1.key = key;
        this.Pager1.PageColumn = cloumns;
        this.Pager1.Pageindex = 0;
        this.Pager1.PageTable = "WareHouse as a,Country as b";
        this.Pager1.Condition = condition.ToString();
        this.Pager1.PageSize = 10;
        this.Pager1.PageCount = 0;
        this.Pager1.PageBind();
        Translations();
    }

    /// <summary>
    /// 给仓库模型层赋值
    /// </summary>
    private void SetValueWareHouseModel()
    {
        int maxWareControl = SetParametersBLL.GetMaxWareControlFromWareHouse();
        wareHouseModel.WareHouseID = ViewState["WareHouseID"] == null ? 0 : int.Parse(ViewState["WareHouseID"].ToString());
        wareHouseModel.CountryCode = ddlCountry1.SelectedValue;
        wareHouseModel.WareHouseName = txtWareHuseName.Text.Trim();
        wareHouseModel.WareHouseForShort = txtWareHouseForShort.Text.Trim();
        wareHouseModel.WareHousePrincipal = txtWareHousePrincipal.Text.Trim();
        wareHouseModel.WareHouseAddress = txtWareHouseAddress.Text.Trim();
        wareHouseModel.WareHouseDescr = txtWareHouseDescr.Text.Trim();
        wareHouseModel.WareControl = maxWareControl + 1;
        string country = this.CountryCity1.Country;
        string province = this.CountryCity1.Province;
        string city = this.CountryCity1.City;
        string cpccode = CommonDataDAL.GetCPCCode(country, province, city);

        wareHouseModel.CPCCode = cpccode;

    }


    /// <summary>
    /// 行绑定事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvWareHouse_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex >= 0)
        {
            int primaryKey = Convert.ToInt32(this.gvWareHouse.DataKeys[e.Row.RowIndex].Value.ToString());
            ((LinkButton)e.Row.FindControl("lbtnWareHouseEdit")).CommandArgument = primaryKey.ToString();
            ((LinkButton)e.Row.FindControl("lbtnWareHouseDelete")).CommandArgument = primaryKey.ToString();
        }

        ///控制GridView样式
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
        }
    }
    /// 给库位模型层赋值
    /// </summary>
    private void SetValueDepotSeatModel()
    {
        int maxDepotSeatID = SetParametersBLL.GetMaxDepotSeatIDFromDepotSeat();
        depotSeatModel.WareHouseID = SetParametersBLL.GetIDENT_CURRENT();
        depotSeatModel.DepotSeatID = maxDepotSeatID + 1;
        depotSeatModel.SeatName = Seat.Text.Trim();
        depotSeatModel.Remark = "";
    }
    /// <summary>
    /// 添加仓库信息
    /// </summary>
    private void AddWareHouseInfo()
    {
        Application.UnLock();
        Application.Lock();
        try
        {
            //添加仓库信息
            ///获取指定的库位名称行数
            /// <summary>
            SetValueWareHouseModel();
            //添加仓库
            int addCount = SetParametersBLL.AddWareHouse(wareHouseModel);
            SetValueDepotSeatModel();
            //添加库位
            addCount += SetParametersBLL.AddDepotSeat(depotSeatModel);
            string manageId = BLL.CommonClass.CommonDataBLL.getManageID(1);
            DAL.ManagerPermissionDAL.AddManagerPermission(wareHouseModel.WareControl, DAL.ManageDAL.GetManageByNumber(manageId).RoleID, 0);
            if (DAL.ManageDAL.GetManageByNumber(Session["Company"].ToString()).RoleID != DAL.ManageDAL.GetManageByNumber(manageId).RoleID)
            {
                DAL.ManagerPermissionDAL.AddManagerPermission(wareHouseModel.WareControl, DAL.ManageDAL.GetManageByNumber(Session["Company"].ToString()).RoleID, 0);
            }
            if (addCount > 1)
            {
                Session.Remove("permission");
                Session["permission"] = BLL.other.Company.DeptRoleBLL.GetAllPermission(Session["Company"].ToString());
                Page.ClientScript.RegisterStartupScript(GetType(), "", "alert('"+ GetTran("001691", "添加仓库成功！") + "');window.location='WareHouseDepotSeat.aspx';", true);
                return;
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001692", "添加仓库失败,请联系管理员！")));
                return;
            }

        }
        catch
        {
        }
        finally
        {
            Application.UnLock();
        }
    }

    /// <summary>
    /// 更新仓库信息
    /// </summary>
    private void UpdWareHouseInfo()
    {
        BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("WareHouse", "WareHouseID");
        cl_h_info.AddRecord(wareHouseModel.WareHouseID);
        ///更新指定的仓库信息
        int updCount = SetParametersBLL.UpdWareHouseByWareHouseID(wareHouseModel);
        if (updCount > 0)
        {
            cl_h_info.AddRecord(wareHouseModel.WareHouseID);
            cl_h_info.ModifiedIntoLogs(ChangeCategory.company28, Session["Company"].ToString(), ENUM_USERTYPE.objecttype9);

            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001693", "修改仓库信息成功！")));
        }

        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001694", "修改仓库信息失败，请联系管理员！")));
        }
    }


    /// <summary>
    /// 添加(修改)仓库
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {

        SetValueWareHouseModel();
        //1表示修改事件
        if ((int)ViewState["AddOrEdit"] == 1)
        {
            //if (Seat.Text.Trim() == "")
            //{
            //    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001695", "对不起，库位不能为空！")));
            //    return;
            //}
            int isExistCount = SetParametersBLL.WareHouseIdIsExist(wareHouseModel.WareHouseID);
            if (isExistCount > 0)
            {
                if (txtWareHuseName.Text.Trim() == "")
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001697", "仓库名称不能为空!")));
                    return;
                }

                else
                {
                    //通过仓库ID和仓库名称获取仓库名称的行数
                    int getCount = SetParametersBLL.GetWareHouseNameCountByWareHouseIDName(wareHouseModel);
                    if (getCount > 0)
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001698", "该仓库名称已经存在！")));
                        return;
                    }

                    else
                    {
                        UpdWareHouseInfo();
                    }
                }
            }

            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001699", "对不起，该仓库不存在或者已经被删除！")));
                return;
            }
        }

        //0表示添加事件
        if ((int)ViewState["AddOrEdit"] == 0)
        {
            if (Seat.Text.Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001695", "对不起，库位不能为空！")));
                return;
            }
            if (txtWareHuseName.Text.Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001697", "仓库名称不能为空!")));
                return;
            }

            else
            {
                ///通过仓库名称获取仓库名称的行数
                int getCount = SetParametersBLL.GetWareHouseNameCountByWareHouseName(txtWareHuseName.Text.Trim());
                if (getCount > 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001700", "对不起，该仓库名称已经存在！")));
                    return;
                }

                else
                {
                    AddWareHouseInfo();
                }
            }
        }

        ///绑定仓库和库位信息
        DataBindWareHouseInfo();
        this.tab1.Style.Add("display", "none");
    }

    /// <summary>
    /// 修改仓库信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtnWareHouseEdit_Command(object sender, CommandEventArgs e)
    {
        bool isExists = DAL.WareHouseDAL.WareHouseisPermission(Session["Company"].ToString(), DAL.WareHouseDAL.GetWareControlByWareHoseID(int.Parse(e.CommandArgument.ToString())));
        if (!isExists)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("001702", "对不起，你没有权限") + "');</script>");
            return;
        }
        this.tab1.Style.Add("display", "");
        //this.tr1.Style.Add("display", "none");

        trWareHouse.Visible = true;
        ViewState["WareHouseID"] = e.CommandArgument;
        ViewState["AddOrEdit"] = 1;                 ///1表示是修改事件

        ///获取指定的仓库信息
        DataTable dt = SetParametersBLL.GetWareHouseInfoByWareHouseID(Convert.ToInt32(e.CommandArgument));

        if (dt.Rows.Count == 1)
        {
            ddlCountry1.SelectedValue = dt.Rows[0][0].ToString();
            txtWareHuseName.Text = dt.Rows[0][1].ToString();
            txtWareHouseForShort.Text = dt.Rows[0][2].ToString();
            txtWareHousePrincipal.Text = dt.Rows[0][3].ToString();
            txtWareHouseAddress.Text = dt.Rows[0][4].ToString();
            txtWareHouseDescr.Text = dt.Rows[0][5].ToString();
            string CPCCode = dt.Rows[0]["CPCCode"].ToString();
            CityModel cpccode = CommonDataDAL.GetCPCCode(CPCCode);
            this.CountryCity1.SelectCountry(cpccode.Country, cpccode.Province, cpccode.City, cpccode.Xian);
        }
    }

    /// <summary>
    /// Delete WareHouse by Id
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtnWareHouseDelete_Command(object sender, CommandEventArgs e)
    {
        bool isExists = DAL.WareHouseDAL.WareHouseisPermission(Session["Company"].ToString(), DAL.WareHouseDAL.GetWareControlByWareHoseID(int.Parse(e.CommandArgument.ToString())));
        if (!isExists)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("001702", "对不起，你没有权限") + "');</script>");
            return;
        }
        int wareHouseId = Convert.ToInt32(e.CommandArgument);
        //Judge the WareHouseId whether exist by Id before delete or update
        int isExistCount = SetParametersBLL.WareHouseIdIsExist(wareHouseId);
        if (isExistCount > 0)
        {
            //Judge the WareHouseId whether has operation by Id before delete
            int getCount = SetParametersBLL.WareHouseIdWhetherHasOperation(wareHouseId);
            if (getCount > 0)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001703", "对不起，该仓库已经发生了业务，因此不能删除!")));
                return;
            }

            else
            {
                BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("WareHouse", "WareHouseID");
                cl_h_info.AddRecord(wareHouseId);
                //删除指定仓库信息
                int delCount = SetParametersBLL.DelWareHouseByWareHouseID(wareHouseId);
                if (delCount > 0)
                {
                    cl_h_info.DeletedIntoLogs(ChangeCategory.company28, Session["Company"].ToString(), ENUM_USERTYPE.objecttype9);
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001704", "删除仓库成功!")));
                }

                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001705", "删除仓库失败，请联系管理员!")));
                    return;
                }
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001699", "对不起，该仓库不存在或者已经被删除!")));
            return;
        }

        DataBindWareHouseInfo();
    }

    /// <summary>
    /// 返回上级菜单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("../SetParameters.aspx");
    }

    /// <summary>
    /// 清除所有的文本框
    /// </summary>
    private void ResetAll()
    {
        //清空仓库信息
        txtWareHuseName.Text = "";
        txtWareHouseForShort.Text = "";
        txtWareHousePrincipal.Text = "";
        txtWareHouseAddress.Text = "";
        txtWareHouseDescr.Text = "";
        this.Seat.Text = "";
        this.ddlCountry1.SelectedIndex = 0;
        ViewState["AddOrEdit"] = 0;
        this.tr1.Style.Add("display", "");
    }

    /// <summary>
    /// 清空
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ResetAll();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        DataBindWareHouseInfo();
        this.tab1.Style.Add("display", "none");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        this.tab1.Style.Add("display", "block");
        ResetAll();
    }
}