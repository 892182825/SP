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
using BLL.Logistics;
using System.Collections.Generic;
using Model;
using DAL;
using Model.Other;
using System.Data.SqlClient;

public partial class Company_FaHuo : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.LogisticsCompanyConsignOut);

        this.DataBind();

        if (!IsPostBack)
        {
            string storeOrderID = Request.QueryString["StoreOrderID"].ToString();

            TextBox3.Text = GetTran("001711", "订单发货");

            //DataTable dt = BillOutOrderBLL.GetWareHouseName();

            DataTable dt = BillOutOrderBLL.GetWareHouseName_Currency("1");

            if (dt.Rows.Count == 0)
            {
                Response.Write("<script>alert('" + GetTran("001293", "你没有仓库权限，所以不能查看此页面") + "')</script>");
                Response.End();
                return;
            }
            string sql = "select inWareHouseID,inDepotSeatID,storeorderid from InventoryDoc where docid='" + storeOrderID + "'";

            DataTable wd = DBHelper.ExecuteDataTable(sql);
            ViewState["storeorderid"] = wd.Rows[0][2].ToString();
            DropDownList1.DataTextField = "WareHouseName";
            DropDownList1.DataValueField = "WareHouseID";
            DropDownList1.DataSource = dt;
            DropDownList1.DataBind();
            DropDownList1.SelectedValue = wd.Rows[0][0] == null ? "1" : wd.Rows[0][0].ToString();
            DropDownList1.Enabled = false;
            string wareHouseID = DropDownList1.SelectedItem.Value;

            DropDownList2.DataTextField = "SeatName";
            DropDownList2.DataValueField = "DepotSeatID";
            DropDownList2.DataSource = CompanyConsignBLL.GetDepotSeat(wareHouseID);
            DropDownList2.DataBind();
            DropDownList2.Enabled = false;
            DropDownList2.SelectedValue = wd.Rows[0][1] == null ? "1" : wd.Rows[0][1].ToString();
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
            TextBox2.Text = ViewState["storeorderid"].ToString();

            TextBox4.Text = admin;
            TextBox5.Text = storeOrderID;

            DataTable dtt = DBHelper.ExecuteDataTable("select Number,LogisticsCompany from Logistics");
            DropDownList3.DataTextField = "LogisticsCompany";
            DropDownList3.DataValueField = "Number";
            DropDownList3.DataSource = dtt;
            DropDownList3.DataBind();


            ////用于已出库的单不允许选择库位
            //SqlDataReader dr = DAL.DBHelper.ExecuteReader("select IsGeneOutBill,OutStorageOrderID from StoreOrder where StoreOrderID='" + Request.QueryString["StoreOrderID"] + "'");
            //if (dr.Read())
            //{
            //    if (dr["IsGeneOutBill"].ToString().ToLower() == "y")
            //    {
            //        SqlDataReader dr2 = DAL.DBHelper.ExecuteReader("select Note,inWareHouseID,inDepotSeatID from InventoryDoc where DocId='" + dr["OutStorageOrderID"] + "'");

            //        dr.Close();

            //        if (dr2.Read())
            //        {
            //            DropDownList1.SelectedValue = dr2["inWareHouseID"].ToString().Trim();


            //            string bdwareHouseID = DropDownList1.SelectedItem.Value;

            //            DropDownList2.DataSource = null;
            //            DropDownList2.DataTextField = "SeatName";
            //            DropDownList2.DataValueField = "DepotSeatID";
            //            DropDownList2.DataSource = CompanyConsignBLL.GetDepotSeat(bdwareHouseID);
            //            DropDownList2.DataBind();
            //        }

            //        TextBox6.Text = dr2["Note"].ToString();

            //        dr2.Close();

            //        DropDownList1.Enabled = false;
            //        DropDownList2.Enabled = false;
            //    }
            //}
        }
    }

    public void SetFH()
    {
        int hs = 0;
        hs = DBHelper.ExecuteNonQuery("update InventoryDoc set IsSend=1,trackingnum='" + txtCarry.Text + "' where docid='" + Request.QueryString["StoreOrderID"] + "'");
        hs += DBHelper.ExecuteNonQuery("update storeorder set issent='Y',ConsignmentDateTime='" + DateTime.UtcNow + "',ConveyanceCompany='" + DropDownList3.SelectedItem.Text + "',kuaididh='" + txtCarry.Text + "' from InventoryDoc where storeorder.storeorderid=InventoryDoc.storeorderid and InventoryDoc.docid='" + Request.QueryString["StoreOrderID"] + "'");

        if (hs == 2)
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>if(window.confirm('" + GetTran("001716", "发货成功,您要打印此发货单吗？") + "')) window.open('docPrint.aspx?DocID=" + Request.QueryString["StoreOrderID"] + "&type=FH');window.location.href='CompanyConsign.aspx';</script>");
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("001714", "发货失败！！") + "')</script>");
            return;
        }


        // 短信
        SqlConnection con = null;
        SqlTransaction tran = null;

        string sjhm = Request.QueryString["Telephone"].ToString();
        string orderid = ViewState["storeorderid"].ToString();
        string sjname = Request.QueryString["InceptPerson"].ToString();
        string hybianhao = Request.QueryString["StoreID"].ToString();

        try
        {
            con = DAL.DBHelper.SqlCon();
            con.Open();
            tran = con.BeginTransaction();

            bool bo = BLL.MobileSMS.SendMsgMode(tran, sjname, "订单号为：" + orderid, hybianhao, sjhm, orderid, Model.SMSCategory.sms_Delivery);
            if (bo)
                tran.Commit();
            else
                tran.Rollback();
        }
        catch (Exception ee)
        {
            if (tran != null)
                tran.Rollback();
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (DropDownList2.SelectedItem == null || DropDownList1.SelectedItem == null)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("001301", "请选择创库和库位！") + "')</script>");
        }
        else
        {
            string storeOrderID = Request.QueryString["StoreOrderID"].ToString();

            string outStorageOrderID = "CK" + OrderGoodsBLL.GetNewOrderID();
            ViewState["ckd"] = outStorageOrderID;
            //已发货
            if (DAL.DBHelper.ExecuteScalar("select IsSend from InventoryDoc where docid='" + Request.QueryString["StoreOrderID"] + "'").ToString() == "1")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('已经发货了！')</script>");
            }
            //已出库
            //else if (DAL.DBHelper.ExecuteScalar("select IsGeneOutBill from StoreOrder where StoreOrderID='" + Request.QueryString["StoreOrderID"] + "'").ToString().ToLower() == "y")
            //{
            //    outStorageOrderID = BLL.CommonClass.CommonDataBLL.getOutStorageOrderId(storeOrderID);
            //    ViewState["ckd"] = outStorageOrderID;
            //    SetFH();
            //}
            else //未出库
            {
                //StoreOrderModel som = BillOutOrderBLL.GetStoreOrderModel_II(storeOrderID);
                //List<OrderDetailModel> l_odm = BillOutOrderBLL.GetOrderDetailModelList(storeOrderID);

                //InventoryDocModel idm = new InventoryDocModel();

                //idm.Currency = Convert.ToInt32(Request.QueryString["Country"]);
                ////idm.DocAuditTime = som.AuditingDate;
                //idm.DocTypeID = Convert.ToInt32(DBHelper.ExecuteScalar("select DocTypeId from dbo.DocTypeTable where DocTypeCode='FH'"));  //
                //idm.DocMaker = TextBox4.Text;
                //idm.Client = som.StoreId;
                //idm.DepotSeatID = Convert.ToInt32(DropDownList2.SelectedItem.Value);
                //idm.WareHouseID = Convert.ToInt32(DropDownList1.SelectedItem.Value);
                ////idm.DepotSeatID = Convert.ToInt32(Session["depotSeatID"]);
                ////idm.WareHouseID = Convert.ToInt32(Session["wareHouseID"]);
                //idm.TotalMoney = Convert.ToDouble(som.TotalMoney);
                //idm.TotalPV = Convert.ToDouble(som.TotalPv);
                //idm.ExpectNum = som.ExpectNum;
                //idm.Cause = "FH";
                //idm.Note = TextBox6.Text.Trim();
                //idm.StateFlag = 1;
                //idm.CloseFlag = 0;
                //idm.OperationPerson = "";//业务员
                //idm.OriginalDocID = som.StoreorderId;
                //idm.Address = som.InceptAddress;

                //List<InventoryDocDetailsModel> l_ddm = new List<InventoryDocDetailsModel>();

                //for (int i = 0; i < l_odm.Count; i++)
                //{
                //    InventoryDocDetailsModel ddm = new InventoryDocDetailsModel();

                //    ddm.ProductID = l_odm[i].ProductId;
                //    ddm.ProductQuantity = Convert.ToDouble(l_odm[i].Quantity);
                //    ddm.UnitPrice = Convert.ToDouble(l_odm[i].Price);
                //    ddm.PV = Convert.ToDouble(l_odm[i].Pv);
                //    ddm.ProductTotal = Convert.ToDouble(l_odm[i].Quantity) * Convert.ToDouble(l_odm[i].Price);
                //    ddm.MeasureUnit = "";

                //    l_ddm.Add(ddm);
                //}

                //string rt = BillOutOrderBLL.OutOrder(storeOrderID, outStorageOrderID, idm, l_ddm);

                //if (rt == "1" || rt=="-88")
                //{
                SetFH();
                //}
                //else if (rt == "0")
                //    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("001304", "库存不够！") + "')</script>");
                //else if (rt == "-6")
                //    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("001309", "仓库库位上没有货！") + "')</script>");
                //else
                //    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("001714", "发货失败！！") + rt + "')</script>");
            }
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
        Response.Redirect("CompanyConsign.aspx");
    }
}