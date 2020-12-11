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
using BLL;
using System.Collections.Generic;
using BLL.Registration_declarations;
using System.Data.SqlClient;
using BLL.CommonClass;
using Model.Other;
using BLL.Logistics;
using BLL.other.Company;
using DAL;
using BLL.other.Store;

public partial class SetMemberPlace : System.Web.UI.Page
{
    RegistermemberBLL registermemberBLL = new RegistermemberBLL();
    protected LetUsOrder luo = new LetUsOrder();
    /// <summary>
    /// 页面加载事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetExpires(DateTime.Now);
       Permissions.ThreeRedirect(Page, "../member/" + Permissions.redirUrl);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        if (!IsPostBack)
        {
            
            BindDate();
         
        }
    }

    public void BindDate()
    {
        if (Request.QueryString["orderid"] != null)
        {
            string orderid = Request.QueryString["orderid"];
            MemberOrderModel mo = MemberOrderDAL.GetMemberOrder(orderid);
            MemberInfoModel mi = MemberInfoDAL.getMemberInfo(mo.Number);
            lblxjnumber.Text = mo.Number;
            lblname.Text = mi.PetName;
            lblphonenum.Text = mi.MobileTele;
            //txtplacement.Text = mi.Placement;
            hiddirect.Value = mi.Direct ;
            txtDirect.Text = mi.Direct;
            hidorderid.Value = orderid;
            decimal nowpc = Common.GetnowPrice();
            decimal jf = mo.TotalMoney / nowpc * 0.8m;
            lblkcjb.Text = jf.ToString("0.0000");
            decimal bdb = mo.TotalMoney *0.2m;
            lblttmoney.Text = bdb.ToString("0.00");

        }
         
       
       

    }
  

    /// <summary>
    /// 注册报单流程（包括判断）
    /// 调用逻辑层中的所有方法
    /// </summary>
    public void AddOrderAndInfoProcess()
    {
        string xjnumber = lblxjnumber.Text;
        string placement = this.hidplacemnet.Value;
        string direct = hiddirect.Value;
        if (placement == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('安置编号不能为空');</script>", false);
            return;
        }

        if (placement == xjnumber)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('安置编号不能与会员编号相同');</script>", false);
            return;
        }
        string GetError1 = new AjaxClass().CheckNumberNetAn(direct, placement);
        if (GetError1 != null && GetError1 != "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('安置编号必须在推荐编号的安置网络下面！');</script>", false);
            return;
        }
        if (Convert.ToInt32(DAL.DBHelper.ExecuteScalar("select COUNT(0) from MemberInfo where   MemberState in(0,2) and Number='" + CommonDataBLL.quanjiao(direct) + "'")) != 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('推荐编号未激活');</script>", false);
            return;
        }

        if (Convert.ToInt32(DAL.DBHelper.ExecuteScalar("select COUNT(0) from MemberInfo where MemberState in(0,2) and Number='" + CommonDataBLL.quanjiao(placement) + "'")) != 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请先激活当前会员的安置人，或重新设置安置编号！');</script>", false);
            return;
        }


        string placement_check = registermemberBLL.GetHavePlacedOrDriect(xjnumber, "", placement, direct);
        if (placement_check != null)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + placement_check + "');</script>", false);
            return;
        }

        string pass=txtpassword.Text;
        if (pass == "")
        { 
           ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请输入二级密码！');</script>", false);
            return;
        }

        string EnPass = Encryption.Encryption.GetEncryptionPwd(pass, direct);
        int n = PwdModifyBLL.check(direct, EnPass, 1);
        if (n <= 0) { 
          ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('二级密码不正确！');</script>", false);
            return;
        }



        string District = hidDistrict.Value;

        if (placement != "8888888888")
        {
            if (DBHelper.ExecuteScalar("select count(0) from memberinfo where placement='" + placement + "' and District=" + District + "  and  memberstate=1 ").ToString() != "0")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('安置人所选区位已有人安置！');</script>", false);
                return;
            }
        }
        string orderid = hidorderid.Value;
        int maxexp = BLL.CommonClass.CommonDataBLL.getMaxqishu();
        string curip = Request.UserHostAddress.ToString();
        int rec = -1;
        SqlConnection conn = null;
        SqlTransaction tran = null;
        try
        {
            conn = DBHelper.SqlCon();
            conn.Open();
            tran = conn.BeginTransaction();

            string sql = " update memberinfo  set  placement=@placement ,District=" + District + "   where  number=@number ";
            SqlParameter[] sps = new SqlParameter[] { 
         new SqlParameter("@placement",placement),
         new SqlParameter("@number",xjnumber) 
        };
              rec = DBHelper.ExecuteNonQuery(tran,sql, sps, CommandType.Text);

           
          
            rec = AddOrderDataDAL.OrderPayment(tran, direct, orderid, curip, 1, 1, 1, direct, "", 2, -1, 1, 1, "", 0, "");
            if (rec == 0)
            {
                tran.Commit(); 
                //ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('激活会员成功！');window.location.href='BrowseMemberOrders.aspx'</script>");
                 // Response.Redirect("../payserver/chosepaysjpay.aspx?blif=" + EncryKey.GetEncryptstr(orderid, 1, 1) + "");

            }else 
            if (rec == 2)
            {

                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('可用石斛积分不足，激活失败！');</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('安置激活失败，请重新输入安置编号！');</script>");
                this.hidplacemnet.Value = "";
                this.txtplacemnet.Text = "";
            }
        }
        catch (Exception)
        {
            tran.Rollback();
  ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('安置激活失败！');</script>");     
            this.hidplacemnet.Value = "";
                this.txtplacemnet.Text = "";
        }
        finally {
            tran.Dispose();
            conn.Close();
            conn.Dispose();
        }
        double jbb = Convert.ToDouble(lblkcjb.Text);
        ClientScript.RegisterStartupScript(this.GetType(), "", "<script> window.location.href='../payserver/payerror1.aspx?ef=" + EncryKey.Encrypt(rec.ToString() + "," + orderid + "," + jbb) + "';</script>");

           

           
        

       

    }
     
    protected void Button1_Click(object sender, EventArgs e)
    {

        AddOrderAndInfoProcess();
    }

    protected void txtplacemnet_TextChanged(object sender, EventArgs e)
    {
        hidplacemnet.Value = txtplacemnet.Text;
    }
}