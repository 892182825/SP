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

public partial class Company_SetParams_AddDepotSeat : BLL.TranslationBase
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
    private static bool blWareHouseID = true;
    private static bool blWareHouseName = true;

    private static bool blID = true;
    private static bool blDepotSeatID = true;
    private static bool blSeatName = true;
    private static bool blRemark = true;

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, "../index.aspx");
        ///设置GridView的样式
        this.gvDepotSeat.Attributes.Add("style", "word-break:keep-all;word-wrap:normal");
        if (!Page.IsPostBack)
        {
            this.tab1.Style.Add("display", "none");
            this.Button4.Value = "添 加";
            ViewState["WareHouseID"] = Request.QueryString["WareHouseID"];
            ViewState["WareHouseOrDepotSeat"] = 0;
            this.ddlWareHouse.Text = SetParametersBLL.GetWareHouseItem(int.Parse(ViewState["WareHouseID"].ToString())).WareHouseName;
            DataBindDepotSeatInfo();
        }
        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.gvDepotSeat,
                new string[][]{
                    new string []{"000015","操作"},
                    new string []{"001195","编号"},
                    new string []{"000253","仓库编号"},
                    new string []{"000877","库位编号"},
                    new string []{"000357","库位名称"},
                    new string []{"001738","库位备注"}
                });
        this.TranControls(this.Button1, new string[][] { new string[] { "007079", "添加库位" } });
        this.TranControls(this.Button2, new string[][] { new string[] { "000421", "返回" } });
        this.TranControls(this.btnReset, new string[][] { new string[] { "006812", "重置" } });


    }

    /// <summary>
    /// 绑定库位信息
    /// </summary>
    private void DataBindDepotSeatInfo()
    {
        ///获取库位信息       
        DataTable dtDepotSeat = StorageInBLL.GetDepotSeatInfoByWareHouaseID(int.Parse(ViewState["WareHouseID"].ToString()));
        ViewState["sortDepotSeat"] = dtDepotSeat;
        DataView dv = new DataView((DataTable)ViewState["sortDepotSeat"]);
        if (ViewState["sortDepotSeatstring"] == null)
            ViewState["sortDepotSeatstring"] = dtDepotSeat.Columns[0].ColumnName.Trim();
        dv.Sort = ViewState["sortDepotSeatstring"].ToString();
        this.gvDepotSeat.DataSource = dv;
        this.gvDepotSeat.DataBind();
    }



    /// <summary>
    /// 给库位模型层赋值
    /// </summary>
    private void SetValueDepotSeatModel()
    {
        int maxDepotSeatID = SetParametersBLL.GetMaxDepotSeatIDFromDepotSeat();
        depotSeatModel.ID = ViewState["ID"] == null ? 0 : Convert.ToInt32(ViewState["ID"]);
        depotSeatModel.WareHouseID = int.Parse(ViewState["WareHouseID"].ToString());
        depotSeatModel.DepotSeatID = maxDepotSeatID + 1;
        depotSeatModel.SeatName = txtSeatName.Text.Trim();
        depotSeatModel.Remark = txtRemark.Text.Trim();
    }

    /// <summary>
    /// 行绑定事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvDepotSeat_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex >= 0)
        {
            int primaryKey = Convert.ToInt32(this.gvDepotSeat.DataKeys[e.Row.RowIndex].Value.ToString());
            ((LinkButton)e.Row.FindControl("lbtnDepotSeatEdit")).CommandArgument = primaryKey.ToString();
            ((LinkButton)e.Row.FindControl("lbtnDepotSeatDelete")).CommandArgument = primaryKey.ToString();
        }

        ///控制GridView样式
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
        }
    }

    /// <summary>
    /// 添加库位信息
    /// </summary>
    private void AddDepotSeatInfo()
    {
        ///向库位表中插入记录
        int addCount = SetParametersBLL.AddDepotSeat(depotSeatModel);
        if (addCount > 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001763", "添加库位成功！")));
        }

        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001763", "添加库位成功！")));
        }
    }

    /// <summary>
    /// 更新库位信息
    /// </summary>
    private void UpdDepotSeatInfo()
    {
        ///更新指定的库位信息
        int updCount = SetParametersBLL.UpdDepotSeatByID(depotSeatModel);
        if (updCount > 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001766", "修改库位信息成功！")));
        }

        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001767", "修改库位信息失败，请联系管理员！")));
            return;
        }
    }

    /// <summary>
    /// 添加(修改)库位
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        SetValueDepotSeatModel();
        ///修改库位信息
        if ((int)ViewState["WareHouseOrDepotSeat"] == 1)
        {
            int isExistCount = SetParametersBLL.DepotSeatIdIsExist(depotSeatModel.ID);
            if (isExistCount > 0)
            {
                if (txtSeatName.Text.Trim() == "")
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001695", "对不起，库位不能为空！")));
                    return;
                }

                else
                {
                    ///获取指定的库位名称的行数
                    int getCount = SetParametersBLL.GetSeatNameCountByIDWareHouseIDSeatName(depotSeatModel);
                    if (getCount > 0)
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001771", "该库位名称已经存在！")));
                    }

                    else
                    {
                        UpdDepotSeatInfo();
                    }
                }
            }

            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001772", "对不起，该库位不存在或者已经被删除！")));
                return;
            }
        }
        else
        {
            if (txtSeatName.Text.Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001695", "对不起，库位不能为空！")));
            }

            else
            {
                ///获取指定的库位名称行数
                int getCount = SetParametersBLL.GetSeatNameCountByWareHouseIDSeatName(depotSeatModel);
                if (getCount > 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001771", "该库位名称已经存在！")));
                }

                else
                {
                    AddDepotSeatInfo();
                }
            }
        }
        DataBindDepotSeatInfo();
        this.tab1.Style.Add("display", "none");
    }

    /// <summary>
    /// 修改库位信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtnDepotSeatEdit_Command(object sender, CommandEventArgs e)
    {
        bool isExists = DAL.WareHouseDAL.WareHouseisPermission(Session["Company"].ToString(), DAL.WareHouseDAL.GetWareControlByWareHoseID(int.Parse(ViewState["WareHouseID"].ToString())));
        if (!isExists)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001702", "对不起，你没有权限！")));
            return;
        }
        this.tab1.Style.Add("display", "block");
        this.Button4.Value = "修 改";
        ViewState["ID"] = e.CommandArgument;
        ViewState["WareHouseOrDepotSeat"] = 1;     ///1表示修改库位

        ///获取库位的仓库信息
        DataTable dt = SetParametersBLL.GetDepotSeatInfoByID(Convert.ToInt32(e.CommandArgument));

        if (dt.Rows.Count == 1)
        {
            txtSeatName.Text = dt.Rows[0][1].ToString();
            txtRemark.Text = dt.Rows[0][2].ToString();
        }
    }
    /// <summary>
    /// 删除库位信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtnDepotSeatDelete_Command(object sender, CommandEventArgs e)
    {
        bool isExists = DAL.WareHouseDAL.WareHouseisPermission(Session["Company"].ToString(), DAL.WareHouseDAL.GetWareControlByWareHoseID(int.Parse(ViewState["WareHouseID"].ToString())));
        if (!isExists)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("001702", "对不起，你没有权限！") + "');</script>");
            return;
        }
        int depotSeatId = Convert.ToInt32(e.CommandArgument);
        //Judge the DepotSeatId whether exist by Id before delete or update
        int isExistCount = SetParametersBLL.DepotSeatIdIsExist(depotSeatId);
        if (isExistCount > 0)
        {
            //Judge the DepotSeatId whether has operation by Id before delete
            int getCount = SetParametersBLL.DepotSeatIdWhetherHasOperation(depotSeatId);
            if (getCount > 0)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001774", "对不起，该库位已经发生了业务，因此不能删除!")));
                return;
            }

            else
            {
                if (SetParametersBLL.getDepotSeatCountByWareHoseID(int.Parse(ViewState["WareHouseID"].ToString())) == 1)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001775", "请删除仓库!")));
                    return;
                }
                //删除指定库位信息
                int delCount = SetParametersBLL.DelDepotSeatByID(depotSeatId);
                if (delCount > 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001776", "删除库位成功!")));
                }

                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001777", "删除库位失败，请联系管理员!")));
                    return;
                }
            }
        }

        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001772", "对不起，该库位不存在或者已经被删除!")));
            return;
        }

        DataBindDepotSeatInfo();
        this.tab1.Style.Add("display", "none");
    }

    /// <summary>
    /// 返回上级菜单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("WareHouseDepotSeat.aspx");
    }


    /// <summary>
    /// 清除所有的文本框
    /// </summary>
    private void ResetAll()
    {
        this.Button4.Value = GetTran("006639", "添 加");
        ///清空库位信息
        txtSeatName.Text = "";
        txtRemark.Text = "";
        ViewState["WareHouseOrDepotSeat"] = 0;
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
    /// <summary>
    /// GridView排序事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvDepotSeat_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataView dv = new DataView((DataTable)ViewState["sortDepotSeat"]);
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

            case "warehouseid":
                if (blWareHouseID)
                {
                    dv.Sort = "WareHouseID desc";
                    blWareHouseID = false;
                }

                else
                {
                    dv.Sort = "WareHouseID asc";
                    blWareHouseID = true;
                }
                break;

            case "warehousename":
                if (blWareHouseName)
                {
                    dv.Sort = "WareHouseName desc";
                    blWareHouseName = false;
                }

                else
                {
                    dv.Sort = "WareHouseName asc";
                    blWareHouseName = true;
                }
                break;


            case "depotseatid":
                if (blDepotSeatID)
                {
                    dv.Sort = "DepotSeatID desc";
                    blDepotSeatID = false;
                }

                else
                {
                    dv.Sort = "DepotSeatID asc";
                    blDepotSeatID = true;
                }
                break;

            case "seatname":
                if (blSeatName)
                {
                    dv.Sort = "SeatName desc";
                    blSeatName = false;
                }

                else
                {
                    dv.Sort = "SeatName asc";
                    blSeatName = true;
                }
                break;

            case "remark":
                if (blRemark)
                {
                    dv.Sort = "Remark desc";
                    blRemark = false;
                }

                else
                {
                    dv.Sort = "Remark asc";
                    blRemark = true;
                }
                break;
        }

        this.gvDepotSeat.DataSource = dv;
        this.gvDepotSeat.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        this.tab1.Style.Add("display", "block");
        ResetAll();
    }
    protected void Linkbutton1_Click(object sender, EventArgs e)
    {
        btnAdd_Click(null, null);
    }
}
