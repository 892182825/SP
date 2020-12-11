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
using DAL;
using BLL.CommonClass;
using System.Collections.Generic;
using BLL.MoneyFlows;
using System.Text.RegularExpressions;
using System.Text;
using System.IO;
using Model.Other;

public partial class RemSecan : BLL.TranslationBase
{
    protected bool isact = false;
    protected int bzCurrency = 0;
    protected DataTable tab = new DataTable();
    protected int id;
    protected string str;

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        //获取标准币种
        bzCurrency = CommonDataBLL.GetStandard();
        if (!IsPostBack)
        {

            if (Session["Default_Currency"] == null) Session["Default_Currency"] = bzCurrency;

            LoadDatabyorderid();
            //ucPager1.Visible = false;
            translation();
        }
    }

    private void translation()
    {
        this.TranControls(this.ddlpaystate, new string[][] { new string[] { "000633", "全部" }, new string[] { "007370", "未到账" }, new string[] { "007371", "已到账" } });
        this.TranControls(this.btnnopay, new string[][] { new string[] { "000340", "查询" } });
        //    this.TranControls(this.gvSecanRemits, new string[][]{
        //        new string[]{"001892","汇款人编号"},
        //        new string[]{"000777","汇款人姓名"},
        //        new string[]{"001970","汇款金额"},
        //        new string[]{"007372","汇款申请时间"},
        //        new string[]{"007374","到账情况"},
        //        new string[]{"007375","到账时间"},
        //        new string[]{"007376","剩余有效期 "},
        //        new string[]{"000015","操作"}

        //    });
        //    this.TranControls(this.btnupload, new string[][] { new string[] { "001396", "开始上传" } });

    }

    /// <summary>
    /// 加载数据
    /// </summary>
    public void LoadDatabyorderid()
    {
        if (Session["Member"] != null)
        {
            string cond = "";
            if (txtstdate.Text == "" || txtenddate.Text == "")
            {
                cond = " mp.Remittancesid=rp.Remittancesid and rp.number=mi.number and mp.remitstatus=1 and mp.shenhestate<>-1 and rp.isusepay=1 and (rp.number='" + Session["Member"].ToString() + "') ";
            }
            else
            {
                DateTime date1 = DateTime.Parse(txtstdate.Text);
                DateTime date2 = DateTime.Parse(txtenddate.Text);

                cond = " mp.Remittancesid=rp.Remittancesid and rp.number=mi.number and mp.remitstatus=1 and mp.shenhestate<>-1 and rp.isusepay=1 and (rp.number='" + Session["Member"].ToString() + "' and mp.remittancesdate>='" + date1 + "' and mp.remittancesdate<='" + date2.AddDays(1) + "')";
            }

            if (ddlpaystate.SelectedValue != "-1")
            {
                cond = cond + " and rp.flag=" + ddlpaystate.SelectedValue;
            }
            string clonum = " mi.[name],rp.number,rp.totalrmbmoney,rp.totalmoney,rp.flag,mp.remittancesdate,mp.Remittancesid,rp.memberflag,rp.suremoney,rp.filepicname,rp.ID,rp.CompanySureTime ";
            string table = " remtemp rp,Remittances mp,(select number,name from memberinfo where number='" + Session["Member"].ToString() + "' or direct='" + Session["Member"].ToString() + "') mi ";
            
            this.ucPager1.PageBind(0, 10, table, clonum, cond, " remittancesdate ", "rep");
        }
    }

    protected string GetWithdrawMoney(string WithdrawMoney)
    {
        string money = "";
        if (AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString())) == 0)
        {
            money = "0.00";
        }
        else
        {
            money = (double.Parse(WithdrawMoney) * AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()))).ToString("f2");
        }
        return money;
    }


    protected void gvSecanRemits_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;

            Label lblmoney = (Label)e.Row.FindControl("lblmoney");
            Label lblleavetime = (Label)e.Row.FindControl("lblleavetime");
            Label lblrecstatue = (Label)e.Row.FindControl("lblrecstatue");

            lblmoney.Text = Convert.ToDouble(GetWithdrawMoney(drv["totalrmbmoney"].ToString())).ToString();

            Label lblconinfo = (Label)e.Row.FindControl("lblconinfo");
            Button btnconfirmrmd = (Button)e.Row.FindControl("btnconfirmrmd");
            this.TranControls(btnconfirmrmd, new string[][] { new string[] { "007618", "款已汇出" } });
            lblconinfo.Text = GetTran("007622", "已充值");

            //Button lkbtnupload = ( Button)e.Row.FindControl("lkbtnupload");

            //lkbtnupload.OnClientClick = "return showdiv('" + drv["Remittancesid"].ToString() + "','" + drv["filepicname"].ToString() + "')";

            //int isc = MemberInfoDAL.Getisconfirmuse(Session["member"].ToString());
            int timesd = Convert.ToInt32(DateTime.Now.Subtract(Convert.ToDateTime(drv["remittancesdate"]).AddHours(Convert.ToDouble(Session["WTH"]))).TotalHours);
            TimeSpan ts = ((Convert.ToDateTime(drv["remittancesdate"]).AddHours(Convert.ToDouble(Session["WTH"]))).AddHours(48).Subtract(DateTime.Now));
            if (timesd > 48)
            {

                lblleavetime.Text = GetTran("007625", "已过期");
                btnconfirmrmd.Enabled = false;
                btnconfirmrmd.OnClientClick = "alert('" + GetTran("007626", "此汇款单已超时，不能进行操作！") + "')";
            }
            else
            {

                lblleavetime.Text = (ts.Days * 24 + ts.Hours).ToString() + GetTran("007628", "小时") + ts.Minutes.ToString() + GetTran("007003", "分") + ts.Seconds.ToString() + GetTran("007629", "秒");
            }

            //if (Convert.ToInt32(drv["flag"]) !=0 )
            //{
            //    lkbtnupload.Visible = false;
            //}
            if (Convert.ToInt32(drv["flag"]) == 1)
            {
                lblrecstatue.Text = GetTran("007341", "完成");
                //lblleavetime.Text = DateTime.Parse(drv["CompanySureTime"].ToString()).ToString();
                lblleavetime.Text = GetTran("007371", "已到账");

            }
            else
                if (Convert.ToInt32(drv["flag"]) == 4)
                {
                    lblrecstatue.Text = GetTran("007627", "迟到账");
                    lblleavetime.Text = "";
                }
                else
                {
                    lblrecstatue.Text = getst(Convert.ToDateTime(drv["remittancesdate"]));
                    if (Convert.ToInt32(drv["memberflag"]) != 1)
                    {



                        //  isact = MemberInfoDAL.IsNoGamerActive(Session["Member"].ToString());
                        //if (isact)
                        //{

                        lblconinfo.Visible = false;
                        //lkbtnupload.Visible = true;
                        btnconfirmrmd.Visible = true;

                        //if (isc == 1)
                        //{
                        //    btnconfirmrmd.Enabled = false;
                        //    btnconfirmrmd.OnClientClick = "alert('您的信誉不够，公司已禁止此操作功能，请及时与公司联系，以保证此次汇款能够及时生效！')";
                        //}
                        //else
                        //{
                        //    // btnconfirmrmd.OnClientClick = "return confirm('此操作功能能够及时实现汇款生效，请保证使用此功能之前已经完成向公司汇款，否则公司将取消您使用此功能的资格！');";
                        //}

                        //}
                        //else
                        //{

                        //    lblconinfo.Text = "无操作";

                        //}
                    }
                    else
                    {
                        double smoney = Math.Floor(Convert.ToDouble(drv["suremoney"]));
                        double allmoney = Math.Floor(Convert.ToDouble(drv["totalmoney"]));

                        if (smoney < allmoney)
                        {
                            lblconinfo.Text = GetTran("007254", "处理中");
                        }


                        //if (isc == 1)
                        //{
                        //    lkbtnupload.Visible = true;
                        //}
                    }

                }

        }
    }
    public string getzt(int time, TimeSpan ts)
    {

        //int timesd = Convert.ToInt32(DateTime.Now.Subtract(Convert.ToDateTime(drv["remittancesdate"]).AddHours(Convert.ToDouble(Session["WTH"]))).TotalHours);

        if (time > 48)
        {

            return GetTran("007625", "已过期");

        }
        else
        {

            return (ts.Days * 24 + ts.Hours).ToString() + GetTran("007628", "小时") + ts.Minutes.ToString() + GetTran("007003", "分") + ts.Seconds.ToString() + GetTran("007629", "秒");
        }

    }
    public string getst(DateTime dtime)
    {
        int timesd = Convert.ToInt32(DateTime.Now.Subtract(dtime).TotalHours);
        if (timesd < 144)
            return GetTran("007370", "未到账");
        else
            return GetTran("007370", "未到账");

    }
    /// <summary>
    /// 行命令事件
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvSecanRemits_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string rimid = e.CommandArgument.ToString();
        if (e.CommandName == "cof")
        {
            //Response.Redirect("Tochangmoney.aspx?rmid="+rimid);

            //double xinyuedu = D_AccountDAL.getAccountxinyuedu(Session["member"].ToString())  ;
            //double curremtmoney= RemittancesDAL.GetnewRemitmoneybyrmid(rimid);
            //if (xinyuedu >= curremtmoney)
            //{
            //int isc = MemberInfoDAL.Getisconfirmuse(Session["member"].ToString());
            //if (isc == 0)
            //{
            string opnumber = "";
            string opip = Request.UserHostAddress.ToString();

            int ges = AddOrderDataDAL.PaymentRemitmoney(rimid, opnumber, opip, 1);
            if (ges == 0 || ges == 7)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script> alert('" + GetTran("007630", "汇款充值确认操作成功") + "！');</script>");
                LoadDatabyorderid();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script> alert('" + GetTran("007631", "汇款充值确认失败，再次进行确认") + "！');</script>");
            }

            //}
            //else
            //{
            //    ClientScript.RegisterStartupScript(this.GetType(), "", "<script> alert('由于您之前的不当操作导致您的信誉额度不足，不能进行此操作！');</script>");
            //}
            //}
            //else
            //{
            //    ClientScript.RegisterStartupScript(this.GetType(), "", "<script> alert('由于您之前的不当操作导致您的信誉额度不足，不能进行此操作！');</script>");
            //}
        }
        if (e.CommandName == "upd")
        {
            // Response.Redirect("Tochangmoney.aspx?rmid=" + rimid);
        }
    }
    protected void btnupload_Click(object sender, EventArgs e)
    {
        //if (FileUppic.FileName != "" && hidremid.Value != "0")
        //{

        //    string imgname = hidremid.Value;
        //    string path = Server.MapPath("upload");
        //    string filename = FileUppic.FileName;
        //    string fix = filename.Substring(filename.LastIndexOf('.') + 1).ToLower();

        //    if (!(fix == "jpg" || fix == "png" || fix == "jpeg" || fix == "gif" || fix == "bmp"))
        //    {
        //        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('文件格式不正确，只允许上传jpeg/jpg/png/gif/bmp格式的图片！');</script>");
        //        return;
        //    }

        //    try
        //    {
        //        string fn = imgname + "." + fix;
        //        string lastpath = path + "/" + fn;
        //        FileUppic.SaveAs(lastpath);
        //         AddOrderDataDAL.uploadpicOfRemittance(imgname, fn);

        //        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('上传成功！');</script>");
        //        LoadDatabyorderid();

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }

        //}
    }
    /// <summary>
    /// 待充值
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        LoadDatabyorderid();
    }
}