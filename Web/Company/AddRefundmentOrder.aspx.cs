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
using Model;
using Model.Other;
using System.Collections.Generic;
using BLL.CommonClass;

public partial class Company_AddRefundmentOrder : BLL.TranslationBase
{
    protected string msg;
    ReturnedGoodsBLL returnedGoodsBLL = new ReturnedGoodsBLL();
    int TuiTotalCount = 0;                         
    int TotalCount = 0;
  
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page,Permissions.redirUrl);
        //权限
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.AddRefundmentOrder);

        Translations();
    }

    /// <summary>
    /// 翻译
    /// </summary>
    private void Translations()
    {
        this.TranControls(this.btnBindStore, new string[][] { new string[] { "001994", "绑定店的库存" } });
        this.TranControls(this.btnSaveOrder, new string[][] { new string[] { "001996", "确认退单" } });
        this.TranControls(this.btnSearch, new string[][] { new string[] { "001998", "查看金额积分" } });
    }
    /// <summary>
    /// 根据输入的店铺编号绑定店铺信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBindStore_Click(object sender, EventArgs e)
    {
        string storeID = txtStoreId.Text.Trim();
        if (DisplaceGoodBrowseBLL.CheckStoreId(storeID))
        {
            StoreInfoModel storeinfo = new StoreInfoModel();
            storeinfo = returnedGoodsBLL.GetStorInfoByStoreid(storeID);
            if (storeinfo != null)
            {
                lblShopName.Text = Encryption.Encryption.GetDecipherName(storeinfo.StoreName);                             //店铺名称
                lbladdress.Text = storeinfo.City.Country + storeinfo.City.Province + storeinfo.City.City + Encryption.Encryption.GetDecipherAddress(storeinfo.StoreAddress);			                //收货地址
                lblpostalcode.Text = storeinfo.PostalCode;		                    //邮编
                lbltele.Text = Encryption.Encryption.GetDecipherTele(storeinfo.HomeTele) + "   " + Encryption.Encryption.GetDecipherTele(storeinfo.MobileTele);	//电话(负责人的电话)	
                lblinceptman.Text = Encryption.Encryption.GetDecipherName( storeinfo.Name);                                 //店长姓名
                //绑定店铺仓库里的产品信息
                BindGridViewList(this.txtStoreId.Text.Trim().ToString());
                Session["storeid"] = txtStoreId.Text.Trim();
                btnSaveOrder.Visible = true;
                btnSearch.Visible = true;
                ViewState["currency"] = ReturnedGoodsBLL.GetStoreCurrency(txtStoreId.Text);
            }
        }
        else
        {
            //msg = Transforms.ReturnAlert("找不到店信息！请联系管理员！");
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("001779", "找不到店信息！请联系管理员！") + "')</script>");
            //Response.Write(msg);
            txtStoreId.Text = string.Empty;
            txtStoreId.Focus();
        }
        if (GetOrderInfo())
        {
            lblTotalMoney.Text = ((ViewState["zongPrice"] != null) ? ViewState["zongPrice"].ToString() : "0");
            lblTotalIntegral.Text = ((ViewState["zongPv"] != null) ? ViewState["zongPv"].ToString() : "0");
        }
    }

   
    /// <summary>
    /// 绑定店铺仓库里的产品信息
    /// </summary>
    /// <param name="storeid"></param>
    protected void BindGridViewList(string storeid)
    {
        gdvRefundment.DataSource = returnedGoodsBLL.GetStrockSByStoreid(storeid);
        gdvRefundment.DataBind();
    }
    /// <summary>
    /// 读取用户的订货信息
    /// </summary>
    /// <returns></returns>
    private bool GetOrderInfo()
    {
        //读取用户的订货信息
        ViewState["zongPrice"] = 0;
        decimal zongPrice = 0;
        decimal zongPv = 0;
        ArrayList list = new ArrayList();
        decimal price;
        decimal pv;
        int count;
        int id;

        if (gdvRefundment.Rows.Count <= 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("002013", "对不起，库存中无可退的货！") + "')</script>");
            return false;
        }

        foreach (GridViewRow item in gdvRefundment.Rows)
        {
            Label lblName = (Label)item.FindControl("lblname");
            //读取用户输入的数量
            if (((TextBox)item.FindControl("TxtPdtNum")).Text != "")
            {
                try
                {
                    count = Convert.ToInt32(((TextBox)item.FindControl("TxtPdtNum")).Text);
                }
                catch
                {
                    //msg = ReturnAlert("对不起，订货信息请输入数字！");
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("002022", "对不起，订货信息请输入数字！") + "')</script>");
                    return false;
                }
            }
            else
            {
                count = 0;
            }
            if (count > 0)
            {
                price = Convert.ToDecimal(((Label)item.FindControl("LblPrice")).Text);
                pv = Convert.ToDecimal(((Label)item.FindControl("LblPv")).Text);

                RadioButtonList radioList = ((RadioButtonList)item.FindControl("RadioBtnCate"));
                //根据用户所选单位计算出实际数量
                count = count * Convert.ToInt32(radioList.SelectedValue);
                id = Convert.ToInt32(((Label)item.FindControl("LblHuoid")).Text);

                //判断店铺是否有足够库存
                int left = returnedGoodsBLL.GetCertainProductLeftStoreCount(id.ToString(), txtStoreId.Text);

                if ((left - count) < 0)
                {
                    //msg = ReturnAlert("对不起，此店的库存不够：" + ((Label)item.FindControl("lblname")).Text + " 的库存数只有：" + left);
                    ClientScript.RegisterStartupScript(this.GetType(), "", "" + GetTran("002023", "对不起，此店的库存不够：") + "" + ((Label)item.FindControl("lblname")).Text + "" + GetTran("002024", "的库存数只有：") + " " + left);
                    return false;
                }
                //保存退货的产品明细信息
                InventoryDocDetailsModel inventoryDocDetailsModel = new InventoryDocDetailsModel();
                inventoryDocDetailsModel.DocID = "";
                inventoryDocDetailsModel.ProductID = id;
                inventoryDocDetailsModel.ProductQuantity = Convert.ToDouble(count);
                inventoryDocDetailsModel.UnitPrice = Convert.ToDouble(price);
                inventoryDocDetailsModel.MeasureUnit = "";
                inventoryDocDetailsModel.PV = Convert.ToDouble(pv);
                inventoryDocDetailsModel.ExpectNum = BLL.CommonClass.CommonDataBLL.getMaxqishu();
                //inventoryDocDetailsModel.SelectedIndex = 1;
                inventoryDocDetailsModel.ProductTotal = Convert.ToDouble(price * count);
                list.Add(inventoryDocDetailsModel);

                zongPrice += price * count;
                zongPv += pv * count;
            }
        }

        //保存信息
        ViewState["list"] = list;
        ViewState["zongPrice"] = zongPrice;
        ViewState["zongPv"] = zongPv;
        return true;
    }


    

    /// 返回能够弹出提示框的 javascript 代码
    /// </summary>
    /// <param name="content">要提示的内容</param>
    /// <returns>javascript 代码</returns>
    public string ReturnAlert(string content)
    {
        string retVal;
        retVal = "<script language='javascript'>alert('" + content.Replace("'", "").Replace("\r", "").Replace("\n", "").Replace("\t", "") + "');</script>";
        return retVal;
    }
    /// <summary>
    /// 查看指定店铺的金额和积分
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //查看金额、积分
        if (GetOrderInfo())
        {
            lblTotalMoney.Text = ((ViewState["zongPrice"] != null) ? ViewState["zongPrice"].ToString() : "0");
            lblTotalIntegral.Text = ((ViewState["zongPv"] != null) ? ViewState["zongPv"].ToString() : "0");
        }
    }
    /// <summary>
    /// 添加一条退货单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSaveOrder_Click(object sender, EventArgs e)
    {
        //读取订货信息
        if (Page.IsValid)
        {
            if (GetOrderInfo())
            {
                SaveInventoryDocAndDocDetails();
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("002025", "操作成功,等待审核!") + "');location.href('RefundmentOrderBrowse.aspx');</script>");
            }
        }
    }
    /// <summary>
    /// 添加退货单据和产品明细信息
    /// </summary>
    protected void SaveInventoryDocAndDocDetails()
    {
        //获得当前操作的时间
        DateTime dt = DateTime.Now.ToUniversalTime();//转换时间
        //获得单据编号
        string docId = returnedGoodsBLL.GetDocId(EnumOrderFormType.ReturnOutStorage);
        //往退货单据实体类中添加数据
        InventoryDocModel inventoryDocModel = new InventoryDocModel();
        inventoryDocModel.DocMakeTime = dt;
        if (ViewState["currency"]==null)
        {
            msg = "<script>alert('" + GetTran("002026", "店铺数据不存在") + "')</script>";
            return;
        }
        //inventoryDocModel.Currency = int.Parse(ViewState["currency"].ToString());
        inventoryDocModel.DocID = docId;
        inventoryDocModel.OriginalDocID = "";
        inventoryDocModel.DocMaker = CommonDataBLL.OperateBh;
        inventoryDocModel.Provider = 0;
        inventoryDocModel.Client = this.txtStoreId.Text.Trim();
        inventoryDocModel.WareHouseID = 0;
        inventoryDocModel.TotalMoney = Convert.ToDouble(ViewState["zongPrice"].ToString());
        inventoryDocModel.TotalPV = Convert.ToDouble(ViewState["zongPv"].ToString());
        inventoryDocModel.ExpectNum = BLL.CommonClass.CommonDataBLL.getMaxqishu();
        //inventoryDocModel.Cause = "TH";
        inventoryDocModel.Note = this.txtMemo.Text.Trim();
        inventoryDocModel.StateFlag = 0;
        inventoryDocModel.BatchCode = "";
        inventoryDocModel.OperationPerson = this.TextBox1.Text.Trim();
        inventoryDocModel.Address = this.lbladdress.Text.Trim();
        //inventoryDocModel.MotherID = "";
        inventoryDocModel.OperateIP = Request.UserHostAddress;
        inventoryDocModel.OperateNum = CommonDataBLL.OperateBh;
        inventoryDocModel.InDepotSeatID = returnedGoodsBLL.GetStoreRate(txtStoreId.Text);

        returnedGoodsBLL.InsertInventoryDoc(inventoryDocModel, CommonDataBLL.OperateBh, txtStoreId.Text, (ArrayList)ViewState["list"]);
        
    }
    protected void gdvRefundment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            RadioButtonList RadioBtnCate = (RadioButtonList)e.Row.FindControl("RadioBtnCate");
            RadioBtnCate.SelectedIndex = 0;

            HtmlInputHidden HidBigSmallMultiPle = (HtmlInputHidden)e.Row.FindControl("HidBigSmallMultiPle");
            HtmlInputHidden HidBigUnitName = (HtmlInputHidden)e.Row.FindControl("HidBigUnitName");
            HtmlInputHidden HidSmallUnitName = (HtmlInputHidden)e.Row.FindControl("HidSmallUnitName");

            RadioBtnCate.Items[0].Value = HidBigSmallMultiPle.Value;
            RadioBtnCate.Items[1].Value = "1";
            RadioBtnCate.Items[0].Text = ((HtmlInputHidden)e.Row.FindControl("HidBigUnitName")).Value + "(" + GetTran("002029", "数量：") + "" + HidBigSmallMultiPle.Value + ")";
            RadioBtnCate.Items[1].Text = ((HtmlInputHidden)e.Row.FindControl("HidSmallUnitName")).Value + "(" + GetTran("002029", "数量：") + "1)";

            RequiredFieldValidator rf = (RequiredFieldValidator)e.Row.FindControl("rf");
            RangeValidator rv = (RangeValidator)e.Row.FindControl("rv");

            TextBox tb = (TextBox)e.Row.FindControl("TxtPdtNum");
            Label lbUnit = (Label)e.Row.FindControl("LblPrice");
            Label lbPV = (Label)e.Row.FindControl("LblPv");
            //				Label lblmessage =(Label)e.Item.FindControl ("lblmessage");

            HtmlInputHidden HidNeedNumber = (HtmlInputHidden)e.Row.FindControl("HidNeedNumber");

            //				tb.ReadOnly =true ;				
            RadioBtnCate.SelectedIndex = 1;
            RadioBtnCate.Enabled = false;

            rv.Type = ValidationDataType.Integer;
            rv.MinimumValue = "0";
            rv.MaximumValue = tb.Text;
            rv.ErrorMessage = GetTran("002030", "库存在0与") + tb.Text + GetTran("002031", "之间!");

            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Attributes.Add("style", "background-image:url('images/tabledp.gif')");
        }  
    }
}
