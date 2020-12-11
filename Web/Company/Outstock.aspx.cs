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
using System;
using BLL.Logistics;
using Model;
using System.Collections.Generic;
using Model.Other;
using System.Data.SqlClient;
using DAL;

public partial class Company_Outstock : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.LogisticsBillOutOrderOut);

        if (!IsPostBack)
        {
            loadData();
        }
    }

    protected void loadData()
    {
        string storeOrderID = Request.QueryString["StoreOrderID"];
        Label1.Text = Request.QueryString["title"].ToString();
        string isEnabledDropDownList = Request.QueryString["enable"].ToString().Replace("'", "").Replace("-", "").Replace("/*", "");
        string isButton = Request.QueryString["isBut"].ToString().Replace("'", "").Replace("-", "").Replace("/*", "");

        if (isEnabledDropDownList == "Y")
        {
            DropDownList1.Enabled = false;
            DropDownList2.Enabled = false;

            fhck.Visible = false;
        }

        if (isButton == "CK")
        {
            TextBox3.Text = GetTran("001294", "订单出库");
        }
        else
        {
            Button1.Visible = false;
            TextBox3.Text = GetTran("001711", "订单发货");
        }

        if (storeOrderID.EndsWith(","))
            storeOrderID = storeOrderID.Substring(0, storeOrderID.Length - 1);

        string allStoreOrderID = ("'" + storeOrderID + "'").Replace(",", "','");

        string sql = "select w.WareHouseid,d.DepotSeatid from storeorder s join city c on s.cpccode=c.cpccode join DepotSeat d on c.DepotSeatid=d.DepotSeatid join WareHouse w on d.WareHouseid=w.WareHouseid where storeorderid in(" + allStoreOrderID + ")";

        DataTable dt = BillOutOrderBLL.GetWareHouseName_Currency("1");

        if (dt.Rows.Count == 0)
        {
            Response.Write("<script>alert('" + GetTran("001293", "你没有仓库权限，所以不能查看此页面") + "')</script>");
            Response.End();
            return;
        }
        DropDownList1.DataSource = dt;
        DropDownList1.DataBind();

        string wareHouseID = DropDownList1.SelectedItem.Value;

        DropDownList2.DataSource = CompanyConsignBLL.GetDepotSeat(wareHouseID);
        DropDownList2.DataBind();

        string admin = Session["Company"].ToString();

        string[] arrtime = DateTime.Now.ToString("yyyy-MM-dd").Split('-');

        string timestr = arrtime[0];

        if (arrtime[1].Length == 1)
            timestr = timestr + "0" + arrtime[1];
        else
            timestr = timestr + arrtime[1];

        if (arrtime[2].Length == 1)
            timestr = timestr + "0" + arrtime[2];
        else
            timestr = timestr + arrtime[2];

        TextBox1.Text = timestr;
        TextBox2.Text = storeOrderID;

        TextBox4.Text = admin;
        TextBox5.Text = storeOrderID;

        string bind = "select p.productid,productcode,productname,quantity,price,pv ,outbillquantity,(quantity-outbillquantity) as [count] from orderdetail o " +
                        " join product p on o.productid=p.productid where storeorderid='" + Request.QueryString["StoreOrderID"] + "'";
        DataTable dt2 = DBHelper.ExecuteDataTable(bind);
        Repeater1.DataSource = dt2;
        Repeater1.DataBind();

        Translations();
    }

    private void Translations()
    {
        this.Button1.Text = GetTran("001290", "确定出库");
        this.Button3.Text = GetTran("000096", "返 回");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (DropDownList2.SelectedItem == null || DropDownList1.SelectedItem == null)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("001301", "请选择创库和库位！") + "')</script>");
            this.Button1.Text = GetTran("001290", "确定出库");
        }
        else
        {
            string err = "";
            string PrepareProduct_PrepareTime = DateTime.Now.ToString();
            string PrepareProduct_OperateNum = Session["Company"].ToString();
            string PrepareProduct_OperateIP = Request.UserHostAddress;
            string outStorageOrderID = "";
            string GroupCK = "";

            string[] arrstoreOrderID = Request.QueryString["StoreOrderID"].ToString().Replace("'", "").Replace("-", "").Replace("/*", "").Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            for (int k = 0; k < arrstoreOrderID.Length; k++)
            {
                outStorageOrderID = "CK" + OrderGoodsBLL.GetNewOrderID();
                GroupCK = GroupCK + outStorageOrderID + ",";

                StoreOrderModel som = BillOutOrderBLL.GetStoreOrderModel_II(arrstoreOrderID[k]);
                List<OrderDetailModel> l_odm = BillOutOrderBLL.GetOrderDetailModelList(arrstoreOrderID[k]);

                string sql = "select isnull(m.number,'') number,s.storeid,s.sendway from storeorder s left join ordergoods o on s.storeorderid=o.fahuoorder left join memberorder m on o.memberorderid=m.orderid where s.storeorderid='" + arrstoreOrderID[k] + "'";
                DataTable dt = DBHelper.ExecuteDataTable(sql);
                string sendway = dt.Rows[0]["sendway"].ToString();
                string number = dt.Rows[0]["number"].ToString();
                string storeid = dt.Rows[0]["storeid"].ToString();
                InventoryDocModel idm = new InventoryDocModel();

                //idm.DocAuditTime = som.AuditingDate;
                idm.DocTypeID = Convert.ToInt32(DAL.DBHelper.ExecuteScalar("select DocTypeId from dbo.DocTypeTable where DocTypeCode='CK'"));   //出库单
                idm.DocMaker = TextBox4.Text;
                if (sendway == "1")
                {
                    idm.Client = number;
                }
                else
                {
                    idm.Client = storeid;
                }
                idm.DepotSeatID = Convert.ToInt32(DropDownList2.SelectedItem.Value);
                idm.WareHouseID = Convert.ToInt32(DropDownList1.SelectedItem.Value);
                idm.ExpectNum = som.ExpectNum;
                idm.Note = TextBox6.Text.Trim();
                idm.StateFlag = 1;
                idm.CloseFlag = 0;
                idm.OperationPerson = "";//业务员
                idm.OriginalDocID = arrstoreOrderID[k];
                idm.Address = som.InceptAddress;
                idm.Storeorderid = arrstoreOrderID[k];

                List<InventoryDocDetailsModel> l_ddm = new List<InventoryDocDetailsModel>();
                double totalmoney = 0;
                double totalpv = 0;
                foreach (RepeaterItem item in Repeater1.Items)
                {
                    HiddenField HF = (HiddenField)item.FindControl("HF");
                    Label lblPrice = (Label)item.FindControl("lblPrice");
                    Label lblPV = (Label)item.FindControl("lblPV");
                    Label txtCount1 = (Label)item.FindControl("txtCount1");
                    TextBox txtCount = (TextBox)item.FindControl("txtCount");
                    int count = 0;

                    int.TryParse(txtCount.Text, out count);

                    if (count > 0)
                    {
                        if (Convert.ToInt32(txtCount.Text) > Convert.ToInt32(txtCount1.Text))
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('" + GetTran("007716", "出库数量不能大于剩余数量！") + "');", true);
                            this.Button1.Text = GetTran("001290", "确定出库");
                            return;
                        }

                        if (Convert.ToInt32(DAL.DBHelper.ExecuteScalar("select COUNT(0) from OrderDetail where StoreOrderID='" + arrstoreOrderID[k] + "' and ProductID=" + Convert.ToInt32(HF.Value) + " and (Quantity-OutBillQuantity)<" + count)) > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('" + GetTran("007716", "出库数量不能大于剩余数量！") + "');", true);
                            this.Button1.Text = GetTran("001290", "确定出库");
                            loadData();
                            return;
                        }

                        InventoryDocDetailsModel ddm = new InventoryDocDetailsModel();

                        ddm.ProductID = Convert.ToInt32(HF.Value);
                        ddm.ProductQuantity = Convert.ToInt32(txtCount.Text);
                        ddm.UnitPrice = Convert.ToDouble(lblPrice.Text);
                        ddm.PV = Convert.ToDouble(lblPV.Text);
                        ddm.ProductTotal = Convert.ToDouble(txtCount.Text) * Convert.ToDouble(lblPrice.Text);
                        ddm.MeasureUnit = "";
                        totalmoney += Convert.ToDouble(txtCount.Text) * Convert.ToDouble(lblPrice.Text);
                        totalpv += Convert.ToDouble(txtCount.Text) * Convert.ToDouble(lblPV.Text);
                        l_ddm.Add(ddm);
                    }
                }
                idm.TotalMoney = totalmoney;
                idm.TotalPV = totalpv;
                if (l_ddm.Count <= 0)
                {
                    string getcount = " select sum(quantity-outbillquantity) from orderdetail where storeorderid='" + arrstoreOrderID[k] + "'";
                    object ckk=DBHelper.ExecuteScalar(getcount, CommandType.Text);
                    if(ckk.ToString()==null||ckk.ToString()=="")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('没有商品暂时没法出库！')", true);
                    }
                    else
                    {
                        if (int.Parse(ckk.ToString()) <= 0)
                    {
                        DBHelper.ExecuteNonQuery("update storeorder set IsGeneOutBill='A' where storeorderid='" + arrstoreOrderID[k] + "'");
                    }
                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('" + GetTran("007721", "请输入出库数量！") + "')", true);
                    this.Button1.Text = GetTran("001290", "确定出库");
                    return;
                }

                string rt = BillOutOrderBLL.OutOrder(arrstoreOrderID[k], outStorageOrderID, idm, l_ddm);

                if (rt == "1")
                {
                    int hs = 0;

                    string getcount = " select sum(quantity-outbillquantity) from orderdetail where storeorderid='" + arrstoreOrderID[k] + "'";
                    if (int.Parse(DBHelper.ExecuteScalar(getcount, CommandType.Text).ToString()) <= 0)
                    {
                        hs = DBHelper.ExecuteNonQuery("update storeorder set IsGeneOutBill='A' where storeorderid='" + arrstoreOrderID[k] + "'");
                    }
                    else
                    {
                        hs = DBHelper.ExecuteNonQuery("update storeorder set IsGeneOutBill='Y' where storeorderid='" + arrstoreOrderID[k] + "'");
                    }
                    if (hs == 1)
                    {
                        //PrepareProduct_DocIDs = PrepareProduct_DocIDs + arrstoreOrderID[k] + ",";
                        //PrepareProduct_TotalMoney = PrepareProduct_TotalMoney + Convert.ToDouble(idm.TotalMoney);
                        //PrepareProduct_TotalPv = PrepareProduct_TotalPv + Convert.ToDouble(idm.TotalPV);
                    }
                    else
                        err = err + GetTran("000079", "订单号：") + arrstoreOrderID[k] + GetTran("007722", " 的产品出库失败！");

                }
                else if (rt == "0")
                    err = err + GetTran("000079", "订单号：") + arrstoreOrderID[k] + GetTran("007723", " 的产品库存不够！");
                else if (rt == "-6")
                    err = err + GetTran("000079", "订单号：") + arrstoreOrderID[k] + GetTran("007724", " 的产品仓库库位上没有货！");
                else if (rt == "-5")
                    err = err + GetTran("000079", "订单号：") + arrstoreOrderID[k] + GetTran("007725", " 的产品更新出库数量错误！");
                else if (rt == "-88")
                    err = err + GetTran("000079", "订单号：") + arrstoreOrderID[k] + GetTran("007726", " 的产品已经出库！");
                else
                    err = err + GetTran("000079", "订单号：") + arrstoreOrderID[k] + GetTran("007727", " 的产品出库失败！");
            }

            if (err == "")
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>if(window.confirm('" + GetTran("001297", "生成的出库单号为：") + outStorageOrderID + GetTran("001298", ",您要打印此出库单吗？") + "')) window.open('docPrint.aspx?docID=" + outStorageOrderID + "');window.location.href='BillOutOrder.aspx';</script>");
            else
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + err + "');window.location.href='BillOutOrder.aspx'</script>");

            this.Button1.Text = GetTran("001290", "确定出库");
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string wareHouseID = DropDownList1.SelectedItem.Value;

        DropDownList2.DataSource = CompanyConsignBLL.GetDepotSeat(wareHouseID);
        DropDownList2.DataBind();
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["source"].ToString() == "ck")
            Response.Redirect("BillOutOrder.aspx");
        else
            Response.Redirect("CompanyConsign.aspx");
    }
}