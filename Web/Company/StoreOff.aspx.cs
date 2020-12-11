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
using System.Data.SqlClient;
using BLL.other.Company;
using BLL.CommonClass;
using Model.Other;
using DAL;
using Model;

public partial class Company_StoreOff : BLL.TranslationBase 
{
    protected string msg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.StoreOffView);

        if (!this.IsPostBack)
        {

            if (Session["Company"] != null)
            {
                string ManageID = Session["Company"].ToString();
                txtOperatorNo.Text = ManageID;
                txtOperatorName.Text = DataBackupBLL.GetNameByAdminID(ManageID);
            }
            if (Request.QueryString["storeid"] != null)
            {
                this.txtStoreid.Text = Request.QueryString["storeid"].ToString();
            }
        }
        Translations();
    }

    private void Translations()
    {

        this.TranControls(btn_Select, new string[][] { new string[] { "000440", "查看" } });
        this.TranControls(this.Button1, new string[][] { new string[] { "000421", "返回" } });


    }


    protected void btnquery_Click(object sender, EventArgs e)
    {
        string storeid = txtStoreid.Text.Trim();
        //判断会员是否存在
        if (!StoreRegisterConfirmBLL.CheckStoreId(storeid))
        {
            LabelResponse.Text = "<font color='red'>" + GetTran("000388", "服务机构") + "" + storeid + "" + GetTran("000801", "不存在，请重新输入") + "！</font>";

            return;
        }

        if (StoreOffBLL.getStoreZX(storeid) > 0)
        {
            int con1 = StoreOffBLL.getStoreISzx(storeid);
            //判断会员是否已注销
            if (con1 == 2)
            {
                LabelResponse.Text = "<font color='red'>" + GetTran("000388", "服务机构") + "" + storeid + "" + GetTran("001310", "已经注销，不需要再次注销了") + "！</font>";

                return;
            }
        }

        DateTime nowTime = DateTime.Now.AddHours(Convert.ToDouble(Session["WTH"]));
        string offReason = txtMemberOffreason.Text;

        StoreOffModel st = new StoreOffModel();
        st.Storeid = storeid;
        st.Zxqishu = CommonDataBLL.getMaxqishu();
        st.Zxfate = DateTime.UtcNow;
        st.OffReason = txtMemberOffreason.Text;
        st.OperatorNo = txtOperatorNo.Text;
        st.OperatorName = txtOperatorName.Text;

      
        DataTable dt_one = DAL.DBHelper.ExecuteDataTable("select StoreState from StoreInfo where  StoreID='" + txtStoreid.Text + "'");
        string StoreState = dt_one.Rows[0]["StoreState"].ToString();//汇款id
        if (StoreState == "0")
        {
            msg = "<script>alert('该服务机构未激活，无法注销！');</script>";
            return;
        }
        else
        {
            int insertCon = StoreOffBLL.getInsertStoreZX(st);
            if (insertCon > 0)
            {

                msg = "<script language='javascript'>alert('" + GetTran("007983", "注销服务机构成功") + "！');window.location.href='StoreOffView.aspx';</script>";
                return;
            }

        }



      

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("StoreOffView.aspx");
    }
    protected void lkSubmit_Click(object sender, EventArgs e)
    {
        btnquery_Click(null, null);
    }
    protected void btn_Select_Click(object sender, EventArgs e)
    {
        string storeid = txtStoreid.Text;
        if (storeid.Length <= 0)
        {
            msg = "<script language='javascript'>alert('" + GetTran("007984", "对不起，服务机构编号不能为空！") + "')</script>";
            return;
        }
        if (!StoreRegisterConfirmBLL.CheckStoreId(storeid))
        {
            msg = "<script language='javascript'>alert('" + GetTran("000288", "对不起,该会员编号不存在") + "！')</script>";
            return;
        }

        Response.Redirect("StoreInfoDetail.aspx?storeid=" + storeid);
    }
}
