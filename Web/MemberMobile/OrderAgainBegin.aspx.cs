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
using Model;

public partial class Member_OrderAgainBegin : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        if (!IsPostBack)
        {
            GetBindAddress();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Txtbh.Text.Trim() == "")
        {
            ScriptHelper.SetAlert(Page, "购货服务机构不能为空！");
            return;
        }
        if (Txtyddh.Text.Trim() == "")
        {
            ScriptHelper.SetAlert(Page, "移动电话不能为空！");
            return;
        }

        //编号是否存在
        if (!BLL.Registration_declarations.MemberOrderAgainBLL.CheckStore(Txtbh.Text.Trim()))
        {
            ScriptHelper.SetAlert(Page, "对不起,该服务机构不存在！");
            //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert('" + GetTran("001549", "对不起,该编号不存在！") + "');", true);
            return;
        }
      
        //服务机构是否注销
        if (DAL.DBHelper.ExecuteScalar("select iszx from storeinfo where storeid='" + Txtbh.Text.Trim() + "'").ToString() == "1")
        {
            ScriptHelper.SetAlert(Page, "对不起，该服务机构已经被公司注销，不可以复消报单！");
            return;
        }

        if (panel2.Visible)
        {
            //if (CountryCity2.Country=="请选择" || CountryCity2.Province=="请选择" || CountryCity2.City=="请选择")
            //{
            //    ScriptHelper.SetAlert(Page, "请选择收货地址！");
            //    return;
            //}
            if (rbtAddress.SelectedItem.Text == "新地址")
            {
                if (Txtdz.Text.Trim() == "")
                {
                    ScriptHelper.SetAlert(Page, "收货详细地址不能为空！");
                    return;
                }
            }
            
        }

        OrderFinalModel odm = new OrderFinalModel();
        odm.Number = Session["Member"].ToString();
        odm.MobileTele = Txtyddh.Text.Trim();
        odm.HomeTele = txtOtherPhone.Text.Trim();
        odm.Type = Convert.ToInt32(this.ddth.SelectedValue);
        odm.SendWay = Convert.ToInt32(this.DDLSendType.SelectedValue);
        odm.StoreID = Txtbh.Text.Trim(); 
        odm.CarryMoney = 0;
        if (Txtdz.Text.Trim()!="")
        {
            odm.ConCity.Country = this.CountryCity2.Country;
            odm.ConCity.Province = this.CountryCity2.Province;
            odm.ConCity.City = this.CountryCity2.City;
            odm.ConAddress = Encryption.Encryption.GetEncryptionAddress(this.Txtdz.Text);
            odm.CPCCode = DAL.CommonDataDAL.GetCPCCode(CountryCity2.Country, CountryCity2.Province, CountryCity2.City);
        }
        else
        {
            string addressRbt = this.rbtAddress.SelectedItem.Text.Trim();
            string gas523 = this.rbtAddress.SelectedValue;
            string[] addr = addressRbt.Split(' ');
            odm.ConCity.Country = addr[0].ToString();
            odm.ConCity.Province = addr[1].ToString();
            odm.ConCity.City = addr[2].ToString();
            odm.ConAddress = Encryption.Encryption.GetEncryptionAddress(addr[3].ToString());
            odm.CPCCode = DAL.CommonDataDAL.GetCPCCode(addr[0].ToString(), addr[1].ToString(), addr[2].ToString());
        }
        Session["fxMemberModel"] = odm;

        Response.Redirect("ShopingListAgain.aspx");

    }

    protected void rbtAddress_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.rbtAddress.SelectedItem.Text == "新地址")
        {
            this.panel2.Visible = true;
            //this.Txtyb.Text = "";
        }
        else
        {
            string addres = this.rbtAddress.SelectedItem.Text.Trim();
            string[] addr = addres.Split(' ');
            //this.Txtyb.Text = DAL.CommonDataDAL.GetZipCode(addr[0], addr[1], addr[2]);
            this.panel2.Visible = false;
        }
    }

    private void GetBindAddress()
    {
        DataTable dt = BLL.CommonClass.CommonDataBLL.GetBindAddress(Session["Member"].ToString());
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string code = dt.Rows[i][0].ToString().Substring(0, 8);
                string addres = BLL.CommonClass.CommonDataBLL.GetAddressByCode(code);
                addres += " " + Encryption.Encryption.GetDecipherAddress(dt.Rows[i][0].ToString().Substring(8, dt.Rows[i][0].ToString().Length - 8));
                this.rbtAddress.Items.Add(addres);
            }
            //this.panel2.Visible = false;
            this.panel1.Visible = true;
            this.rbtAddress.Items.Add("新地址");

            this.rbtAddress.SelectedIndex = 0;
        }
        else
        {
            this.rbtAddress.Items.Add("新地址");

            this.rbtAddress.SelectedIndex = 0;
            this.panel2.Style.Add("display","");
            this.panel1.Visible = false;
        }

    }
}
