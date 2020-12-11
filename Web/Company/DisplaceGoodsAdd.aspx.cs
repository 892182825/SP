using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

///Add Namespace
using Model;
using Model.Other;
using System.Collections.Generic;
using BLL.Logistics;
using System.Data.SqlClient;
using DAL;
using BLL.CommonClass;
using BLL;


public partial class Company_DisplaceGoodsAdd : BLL.TranslationBase 
{
    protected string msg;
    int TuiTotalCount = 0;
    int TotalCount = 0;
    DisplaceGoodBrowseBLL displaceGoodBrowseBLL = new DisplaceGoodBrowseBLL();
    ReturnedGoodsBLL returnedGoodsBLL = new ReturnedGoodsBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        //检查相应权限
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.DisplaceGoodsAdd);

        Translations();
    }
    protected void btnSaveOrder_Click(object sender, EventArgs e)
    {
        //读取订货信息
        if (Page.IsValid)
        {
            if (GetOrderInfo())
            {
                if (TuiTotalCount == 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("002008", "对不起,请注意输入退货信息") + "!');</script>");
                    return;
                }

                if (TotalCount == 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("002011", "对不起,请注意输入换货信息") + "!');</script>");
                    return;
                }
                displaceGoodBrowseBLL.ADDReplacement(GetReplacementModel(), (ArrayList)ViewState["list"]);
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("002012", "添加成功，等待审核") + "!');location.href='DisplaceGoodsBrowse.aspx';</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("002014", "对不起,请注意输入正确的换货信息") + "!');</script>");
            }
        }

       

    }

    private void Translations()
    {

        this.TranControls(this.Button1, new string[][] { new string[] { "001994", "绑定店的库存" } });
        this.TranControls(this.btnSaveOrder, new string[][] { new string[] { "001997", "确认换货" } });
        this.TranControls(this.btnViewTotal, new string[][] { new string[] { "001998", "查看金额积分" } });


    }


    /// <summary>
    /// 读取用户的订货信息
    /// </summary>
    /// <returns></returns>
    private bool GetOrderInfo()
    {
        //读取用户的订货信息			
        double zongPrice = 0;
        double zongPv = 0;
        double TuizongPrice = 0;
        double TuizongPv = 0;

        ArrayList list = new ArrayList();
        double price;
        double pv;
        int count;
        //退货的数量
        int tuiCount;
        int id;

        foreach (GridViewRow item in gvDisplaceGoodsAdd.Rows)
        {
            Label lblName = (Label)item.FindControl("lblname");
            //读取用户输入的数量
            if (((TextBox)item.FindControl("TxtPdtNum")).Text != "" && ((TextBox)item.FindControl("txtTuiQuantity")).Text != "")
            {
                try
                {
                    count = Convert.ToInt32(((TextBox)item.FindControl("TxtPdtNum")).Text);
                    tuiCount = Convert.ToInt32(((TextBox)item.FindControl("txtTuiQuantity")).Text);
                }
                catch
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("002016", "对不起,换货信息请输入数字") + "!');</script>");
                    return false;
                }
            }
            else
            {
                count = 0;
                tuiCount = 0;
            }
            if (count == 0 && tuiCount == 0)
            { }
            else
                if ((count > 0 || tuiCount > 0) && !(count > 0 && tuiCount > 0))
                {
                    price = Convert.ToDouble(((Label)item.FindControl("LblPrice")).Text);
                    pv = Convert.ToDouble(((Label)item.FindControl("LblPv")).Text);

                    id = Convert.ToInt32(((Label)item.FindControl("LblHuoid")).Text);

                    ReplacementDetailModel replacementDetailModel = new ReplacementDetailModel();
                    replacementDetailModel.ProductID = id;
                    replacementDetailModel.Price = price;
                    replacementDetailModel.PV = pv;
                    replacementDetailModel.OutQuantity = tuiCount;
                    replacementDetailModel.InQuantity = count;

                    replacementDetailModel.DisplaceOrderID = "";
                    list.Add(replacementDetailModel);
                    zongPrice += price * count;
                    zongPv += pv * count;

                    TuizongPrice += price * tuiCount;
                    TuizongPv += pv * tuiCount;

                    TuiTotalCount += tuiCount;
                    TotalCount += count;
                }
                else
                    if (count > 0 && tuiCount > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("002017", "对不起,同一产品不能即退货又换货") + "!');</script>");
                        return false;

                    }
                    else
                    {
                        return false;
                    }
        }

        //保存信息
        ViewState["list"] = list;
        ViewState["zongPrice"] = zongPrice;
        ViewState["zongPv"] = zongPv;

        ViewState["TuizongPrice"] = TuizongPrice;
        ViewState["TuizongPv"] = TuizongPv;

        return true;
    }


    protected void btnViewTotal_Click(object sender, EventArgs e)
    {
        //查看金额、积分
        if (GetOrderInfo())
        {
            lblTotalMoney.Text = ((ViewState["TuizongPrice"] != null) ? ViewState["TuizongPrice"].ToString() : "0");
            lblTotalIntegral.Text = ((ViewState["TuizongPv"] != null) ? ViewState["TuizongPv"].ToString() : "0");
            lblHuanMoney.Text = ((ViewState["zongPrice"] != null) ? ViewState["zongPrice"].ToString() : "0");
            lblHuanPv.Text = ((ViewState["zongPv"] != null) ? ViewState["zongPv"].ToString() : "0");
            if (TuiTotalCount == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("002008", "对不起,请注意输入退货信息") + "!');</script>");
                return;
            }

            if (TotalCount == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("002011", "对不起,请注意输入换货信息") + "!');</script>");

                return;
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("002014", "对不起,请注意输入正确的换货信息") + "!');</script>");
        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        string storeID = txtStoreId.Text.Trim();

        //保存店编号
        //Session["StoreID"] = storeID;
        if (DisplaceGoodBrowseBLL.CheckStoreId(storeID))
        {
            StoreInfoModel storeInfo = new ReturnedGoodsBLL().GetStorInfoByStoreid(storeID);

            if (storeInfo != null)
            {
                this.lblShopName.Text = Encryption.Encryption.GetDecipherName(storeInfo.StoreName);
                txtAddress.Text =storeInfo.City.Country+storeInfo.City.Province+storeInfo.City.City+ Encryption.Encryption.GetDecipherAddress(storeInfo.StoreAddress);		//绑定  收货地址
                txtpostalcode.Text = storeInfo.PostalCode.ToString();		//绑定  邮编
                txtTele.Text = Encryption.Encryption.GetDecipherTele(storeInfo.HomeTele) + "   " + Encryption.Encryption.GetDecipherTele(storeInfo.MobileTele);	//绑定  电话(负责人的电话)	
                txtInceptMan.Text = Encryption.Encryption.GetDecipherName(storeInfo.Name);
                GetStore(storeID);
                btnSaveOrder.Visible = true;
                btnViewTotal.Visible = true;
                //this.LabCurrency.Text = CommonData.getD_Currency(storeID);

            }
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("002019", "找不到店信息！请联系管理员") + "!');</script>");
        }

    }

    private void GetStore(string storeId)
    {
        this.gvDisplaceGoodsAdd.DataSource = new RefundmentOrderBrowseBLL().GetStoreStorage(storeId);
        this.gvDisplaceGoodsAdd.DataBind();
        gvDisplaceGoodsAdd.HeaderRow.Visible = false;
    }

    private ReplacementModel GetReplacementModel()
    {
        //获得当前操作的时间
        DateTime dt = DateTime.Now;
        //往换货单实体类中添加数据
        ReplacementModel replacementModel = new ReplacementModel();
        replacementModel.DisplaceOrderID = BLL.other.Company.InventoryDocBLL.GetReplacementID();
        replacementModel.StoreID = txtStoreId.Text;
        replacementModel.MakeDocDate = dt;
        replacementModel.ExpectNum = BLL.CommonClass.CommonDataBLL.GetMaxqishu();
        replacementModel.OutTotalMoney = Convert.ToDouble(ViewState["TuizongPrice"]);
        replacementModel.OutTotalPV = Convert.ToDouble(ViewState["TuizongPv"]);
        replacementModel.InTotalMoney = Convert.ToDouble(ViewState["zongPrice"]);
        replacementModel.InTotalPV = Convert.ToDouble(ViewState["zongPv"]);
        replacementModel.InceptAddress = Encryption.Encryption.GetEncryptionAddress(txtAddress.Text);
        replacementModel.InceptPerson = Encryption.Encryption.GetEncryptionName(txtInceptMan.Text);
        replacementModel.PostalCode = txtpostalcode.Text.Trim();
        replacementModel.Telephone = Encryption.Encryption.GetEncryptionTele(txtTele.Text);
        replacementModel.StateFlag = "N";
        replacementModel.CloseFlag = "N";
        replacementModel.Remark = txtMemo.Text.Trim();
        replacementModel.AuditingDate = dt;
        replacementModel.MakeDocPerson = Session["Company"].ToString();
        return replacementModel;
    }

}
