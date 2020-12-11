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
using Encryption;
using DAL;

 

public partial class DoSeecan11 : System.Web.UI.Page
{
    public static string pk = "20110812";
    int ot = 0;
    public static int oc = 0; 
   
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        if (!IsPostBack)
        {
            if (ViewState["tp"] == null)
            {
                ViewState["tp"] = 0;
            }
            if (ViewState["cp"] == null)
            {
                ViewState["cp"] = 1;
            }
            if (ViewState["day"] == null)
            {
                ViewState["day"] = " and  ((  remittancesdate >= '" + DateTime.Now.AddHours(12).Date.AddHours(-12).ToString() + "'   and   remittancesdate <= '" + DateTime.Now.AddHours(12).Date.AddHours(12).ToString() + "' and  memberflag=0   ) "
            + " or (      rp.memberSureTime >= '" + DateTime.Now.AddHours(12).Date.AddHours(-12).ToString() + "'   and     rp.memberSureTime <= '" + DateTime.Now.AddHours(12).Date.AddHours(12).ToString() + "' and  memberflag=1    )) ";
      
                showbtncolor(1);
            }
            //this.txtbegintime.Text = DateTime.Now.AddDays(-30).ToShortDateString();
            //this.txtendtime.Text = DateTime.Now.ToShortDateString();
          

            DatabondcardList();  DataBindgvlist();
        }
        bindpagecontrol();

    }


    /// <summary>
    /// /绑定下拉列表
    /// </summary>
    public void DatabondcardList()
    {

        DataTable dt = CompanyBankDAL.getdtallcompanybank();  //调用集合方法  
        this.ddllistcard.Items.Clear();
         ListItem lit=null;
        foreach (DataRow item in dt.Rows)
        {
            string account =item["bankbook"].ToString();
            string acctadress = item["bank"].ToString();
            string acctname = item["bankname"].ToString();
        string s = acctadress+" "+acctname +" 尾号"+account.Substring(account.Length-4);
            lit = new ListItem(s, account);
            this.ddllistcard.Items.Add(lit);
 
        }

        this.ddllistcard.Items.Insert(0,new ListItem("全部","-1") );
        this.ddllistcard.SelectedIndex = 0;
        this.ddllistcard.DataBind();

    }

    /// <summary>
    /// 绑定数据方法
    /// </summary>
    public void DataBindgvlist()
    {
        int page = 1;
        if (ViewState["cp"] != null) page = Convert.ToInt32(ViewState["cp"].ToString());



        string condition = " and   rp.flag in (0,2)   ";

        if (this.ddllistcard.SelectedValue != "-1")
        {
            condition += "  and Rembankbook='"+this.ddllistcard.SelectedValue.Trim()+"'  ";
        }
       
            if (this.txtname.Text.Trim() != "")
            {
                condition += " and   mb.name like  '%" + this.txtname.Text.Trim() + "%' ";
            }
          
                if (this.txtnumber.Text.Trim() != "")
                {
                    condition += "  and   mb.number like  '%" + this.txtnumber.Text.Trim() + "%' ";
                }
                else if (this.txtrmmoney.Text != "")
                {
                    condition += " and  rp.totalrmbmoney=" +this.txtrmmoney.Text.Trim();
                }

        if (this.txtbegintime.Text.Trim() != "" && this.txtbegintime.Text.Trim() != "")
        {

            condition += "  and   remittancesdate >='" + Convert.ToDateTime(this.txtbegintime.Text.Trim()).ToString() + "' ";

            condition += "  and  remittancesdate <='" + Convert.ToDateTime(this.txtendtime.Text.Trim()) .ToString() + "' ";
        }
        else {

            condition += ViewState["day"].ToString();
        }

        string endtime = DateTime.Now.ToString();
     //   ControlRemitService mrl = new ControlRemitService();        //创建webservice对象 
        DataTable dt = null;//  RemittancesDAL.GetRemitlist(condition, out   ot, out oc, page); ;  //调用集合方法  
        ViewState["tp"] = ot;
        ViewState["cp"] = oc;
        this.gvdormitlist.DataSource = dt;
        this.gvdormitlist.DataBind();
        bindpagecontrol();

    }

    protected void bindpagecontrol() {
       int totalpage=Convert.ToInt32( ViewState["tp"].ToString());
       int currpage = Convert.ToInt32(ViewState["cp"].ToString());
      
        Button btnlast = new Button();
        btnlast.Text = "<<"; btnlast.CssClass = "btnt";
        btnlast.EnableViewState = false;
       btnlast.Click += new EventHandler(btnlast_Click);
       btnlast.ID = "btnlast";
       spage.Controls.Clear();

       spage.Controls.Add(btnlast);

       int startp = 1;
       int endpage = totalpage;

       //if (totalpage > 10)
       //{
       //    startp = (currpage - 5) < 1 ? 1 : currpage - 5;

       //    endpage=

       // }


Button btnonepage =null;
       for (int i = startp; i <= endpage; i++)
       {
           btnonepage = new Button();
           btnonepage.Text = i.ToString(); btnonepage.CssClass = "btno";
           btnonepage.Click += new EventHandler(  btnonepage_Click );
           btnonepage.EnableViewState = false; 
           btnonepage.ID = "btn" + i;

           if (i == currpage)
           {
               btnonepage.CssClass = "btnc";
           }

           spage.Controls.Add(btnonepage);
       }


       Button btnnext = new Button();
       btnnext.Text = ">>"; btnnext.CssClass = "btnt";
       btnnext.EnableViewState = false; 
       btnnext.ID = "btnnext";
       btnnext.Click +=new EventHandler( btnnext_Click);
       spage.Controls.Add(btnnext);

       if (totalpage == 0 || totalpage==1)
       {
           btnlast.Enabled = false;
           btnnext.Enabled = false;
       }
       else if (totalpage == currpage)
       {
           btnlast.Enabled = true;
           btnnext.Enabled = false;
       }else if (  currpage==1 )
       {
           btnlast.Enabled = false;
           btnnext.Enabled = true;
       }

    
      

    }
    protected void gvdormitlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;
            Label lbldatemb = (Label)e.Row.FindControl("lbldatemb");

            LinkButton lkbtncomsure = (LinkButton)e.Row.FindControl("lkbtncomsure");  //会员确认后公司确认按钮
          //  LinkButton lkbtnshowphoto = (LinkButton)e.Row.FindControl("lkbtnshowphoto");
            LinkButton lkbtnmncomsur = (LinkButton)e.Row.FindControl("lkbtnmncomsur"); //会员为确认 公司确认按钮
            LinkButton lkbtnnoremit = (LinkButton)e.Row.FindControl("lkbtnnoremit");  //会员确认后 未到账按钮
            Label lblAccount =(Label) e.Row.FindControl("lblAccount");  //显示汇入账户信息
            LinkButton lkbtnnopay = (LinkButton)e.Row.FindControl("lkbtnnopay");

           // LinkButton lkbtnclosesound = (LinkButton)e.Row.FindControl("lkbtnclosesound");

           
       //     Button btncontroluse =(Button) e.Row.FindControl("btncontroluse");
           



            Label lblsplit = (Label)e.Row.FindControl("lblsplit");
            Label lblsplit1 = (Label)e.Row.FindControl("lblsplit1");
            //Label lblremittime = (Label)e.Row.FindControl("lblremittime");
            //lblremittime.Text = Convert.ToDateTime(drv["remittancesdate"]).AddHours(12).ToString(); ;

            string account = drv["Rembankbook"].ToString();
            string acctadress = drv["Rembankaddress"].ToString();
            acctadress = acctadress.Substring(0,4);
            string acctname = drv["Rembankname"].ToString();
            lblAccount.Text = "<a   href ='"+drv["onlineaddress"].ToString()+"' target='_blank'>"+ acctadress+" "+acctname +" "+account.Substring(account.Length-4)+"</a>";
          
            int isconfirm=Convert.ToInt32( drv["isconfirm"]);

            int sound = Convert.ToInt32(drv["sound"]);
            if (sound == 1)
            {
              //  lkbtnclosesound.Enabled = false;
            }
            if (isconfirm == 0)
            {
                lkbtnnopay.Enabled = true;
                //btncontroluse.Text = "停用";
                //btncontroluse.CssClass = "buts";
            }
            else if (isconfirm == 1)
            {
                lkbtnnopay.Enabled = false;

                // btncontroluse.Text = "启用";
                //btncontroluse.CssClass = "butt";
            }

            string filename = drv["filepicname"].ToString();
            filename = filename == "" ? "df.gif" : filename;

            string path = System.Configuration.ConfigurationManager.AppSettings["rooturl"];

           // lkbtnshowphoto.OnClientClick = "return  showphoto('" + path + "/upload/" + filename + "');";

            int mbflag = Convert.ToInt32(drv["memberflag"]);
            if (mbflag == 1)
            {


                lbldatemb.Text = Convert.ToDateTime(drv["memberSureTime"]).AddHours(12).ToString("yyyy-MM-dd HH:mm:ss");
                lkbtncomsure.OnClientClick = "return confirm('确定收到该笔汇款了吗？');";
                lkbtnnoremit.OnClientClick = "return confirm('确定收到该笔汇款了吗？');";
                lkbtncomsure.Visible = true;
                lkbtnnoremit.Visible = true;
                lblsplit.Visible = true;
                lkbtnnopay.Visible = true; lblsplit1.Visible = true;

            }
            else {
                lkbtnmncomsur.OnClientClick = "return confirm('确定收到该笔汇款了吗？');";
                lkbtnmncomsur.Visible = true;
            }
            
            


        }
    }
    protected void btnsrearch_Click(object sender, EventArgs e)
    {
        DataBindgvlist();
    }
    protected void gvdormitlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      //  ControlRemitService mrl = new ControlRemitService();        //创建webservice对象 
        string remstr = e.CommandArgument.ToString();
        int  res = 0;
       string f = "到账审核失败，请重新操作！";
       string s = "到账审核成功！";
        if (e.CommandName == "spa")
        {
            res = RemittancesDAL.PaymentChongzhi(remstr, 2); 
          
        }
        if (e.CommandName == "sup")
        {
            res = RemittancesDAL.PaymentChongzhi(remstr, 4); 
        }

        //未到账
        if (e.CommandName == "nop")
        {
            res = res = RemittancesDAL.PaymentChongzhi(remstr, 7); 
            f = "迟到账审核失败，请重新操作！";
            s = "迟到账审核成功，该会员已没有自助确认到账功能！";
        }


        if (e.CommandName == "nou")
        {
            string number = e.CommandArgument.ToString();
            res = RemittancesDAL.Doisuseconfirm(number);
            f = "设置未到账失败，请重新设置！";
            s = "设置未到账成功！";
        }

        //if (e.CommandName == "cls")
        //{
        //    string rmid = e.CommandArgument.ToString();
        //  rmid=  nbb.EncryptDES(rmid, pk);
        // res= mrl.Getupdatesound(rmid);
        // f = "无操作！";
        // s = "提示已关闭！";

        //}


        if (res == 0)
           {
               ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + f + "' ); </script>");
           }
           else {
               ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + s + "'); </script>");
               DataBindgvlist();   
        }
 
    }

    /// <summary>
    /// 上一页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnlast_Click(object sender, EventArgs e)
    {
        oc = Convert.ToInt32(ViewState["cp"]) - 1;
        ViewState["cp"] = oc;
        DataBindgvlist();
    }
    /// <summary>
    /// 下一页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnnext_Click(object sender, EventArgs e)
    {
        oc = Convert.ToInt32(ViewState["cp"]) + 1;
        ViewState["cp"] = oc;

        DataBindgvlist();
    }
    /// <summary>
    /// 指定页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnonepage_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        ViewState["cp"] = Convert.ToInt32(btn.Text);
        DataBindgvlist();
    }
    protected void lkbtnmore_Click(object sender, EventArgs e)
    {
        Response.Redirect("SecanDetails.aspx");
    }


    /// <summary>
    /// 前天
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnyytoday_Click(object sender, EventArgs e)
    {
        ViewState["day"] = " and ((   remittancesdate >= '" + DateTime.Now.AddHours(12).Date.AddHours(-60).ToString() + "'   and   remittancesdate <= '" + DateTime.Now.AddHours(12).Date.AddHours(-36).ToString() + "'  and  memberflag=0  ) "
            + " or(    rp.memberSureTime >= '" + DateTime.Now.AddHours(12).Date.AddHours(-60).ToString() + "'   and   rp.memberSureTime <= '" + DateTime.Now.AddHours(12).Date.AddHours(-36).ToString() + "' and  memberflag=1   )) ";
        showbtncolor(3);
        DataBindgvlist();
    }
    /// <summary>
    /// 昨天
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnyestoday_Click(object sender, EventArgs e)
    {
        ViewState["day"] = " and((    remittancesdate >= '" + DateTime.Now.AddHours(12).Date.AddHours(-36).ToString() + "'   and   remittancesdate <= '" + DateTime.Now.AddHours(12).Date.AddHours(-12).ToString() + "'  and  memberflag=0 )   "
            + " or(     rp.memberSureTime >= '" + DateTime.Now.AddHours(12).Date.AddHours(-36).ToString() + "'   and    rp.memberSureTime <= '" + DateTime.Now.AddHours(12).Date.AddHours(-12).ToString() + "'  and  memberflag=1  )) ";
 
        showbtncolor(2);
        DataBindgvlist();
    }
    /// <summary>
    /// 今天
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btntoday_Click(object sender, EventArgs e)
    {
        ViewState["day"] = " and  ((  remittancesdate >= '" + DateTime.Now.AddHours(12).Date.AddHours(-12).ToString() + "'   and   remittancesdate <= '" + DateTime.Now.AddHours(12).Date.AddHours(12).ToString() + "' and  memberflag=0   ) "
            + " or (      rp.memberSureTime >= '" + DateTime.Now.AddHours(12).Date.AddHours(-12).ToString() + "'   and     rp.memberSureTime <= '" + DateTime.Now.AddHours(12).Date.AddHours(12).ToString() + "'  and  memberflag=1  )) ";
      
        showbtncolor(1);
        DataBindgvlist();
    }

    public void showbtncolor(int t) 
    {
        this.btntoday.CssClass = "butt";
        this.btnyestoday.CssClass = "butt";
        this.btnyytoday.CssClass = "butt";
        switch (t)
        {
            case 1:
                this.btntoday.CssClass = "buts";
                break;
            case 2: 
                this.btnyestoday.CssClass = "buts";
                break;
            case 3:
                this.btnyytoday.CssClass = "buts";
                break;
            default:
                this.btntoday.CssClass = "buts";
                break;
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        DataBindgvlist();
    }
}
