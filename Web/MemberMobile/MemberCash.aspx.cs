using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using BLL.MoneyFlows;
using Model.Other;
using BLL.CommonClass;
using BLL.Logistics;
using System.Data.SqlClient;
using System.Data;
using DAL;
public partial class Member_MemberCash : BLL.TranslationBase
{
    protected int bzCurrency = 0,id = 0;
    protected double huilv;
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));

        //获取标准币种
        bzCurrency = CommonDataBLL.GetStandard();
        //huilv = (AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString())));
       

        ///判断会员账户是否被冻结
        //if (DAL.MemberInfoDAL.CheckState(Session["Member"].ToString())) { Page.ClientScript.RegisterStartupScript(GetType(), null, "<script language='javascript'>alert('您的账户被冻结，不能使用提现申请');window.location.href='First.aspx';</script>"); return; }
        if (!IsPostBack)
        {
            if (Session["Default_Currency"] == null) Session["Default_Currency"] = bzCurrency;
            this.number.Text = Session["Member"].ToString();

            GetActMoney();//账户余额
            //getBankInfo(Session["Member"].ToString()); //绑定用户银行信息

            try
            {
                getOldBind(Session["Member"].ToString());
            }
            catch
            {
                ViewState["edit"] = "0";
                //btn_reset.Visible = false;
            }

        }
        //translation();
    }

    //private void translation()
    //{
    //    TranControls(Button1, new string[][] { new string[] { "007290", "提现申请" } });
    //    //TranControls(btn_reset, new string[][] { new string[] { "000421", "返回" } });
    //}

    /// <summary>
    /// 绑定提现信息
    /// </summary>
    /// <param name="id"></param>
    public void getOldBind(string idd)
    {
       // getBankInfo(Session["Member"].ToString());
        DataTable dt = BLL.Registration_declarations.RegistermemberBLL.QueryWithdraw(idd);
        if (dt.Rows.Count > 0)
        {
            id = Convert.ToInt32(dt.Rows[0]["id"]);
            //money.Text = Convert.ToDouble(dt.Rows[0]["WithdrawMoney"].ToString()).ToString("f2");

            //rmoney.Text = (Convert.ToDouble(dt.Rows[0]["WithdrawMoney"]) + Convert.ToDouble(BLL.CommonClass.CommonDataBLL.GetLeftMoney(Session["Member"].ToString()))).ToString("f2");

            //Label1.Text = dt.Rows[0]["bankname"].ToString();
            //Label3.Text = dt.Rows[0]["bankcard"].ToString();
            remark.Text = dt.Rows[0]["remark"].ToString();
            ViewState["edit"] = "1";

        }
        else
        {
            ViewState["edit"] = "0";
        }
    }

    /// <summary>
    /// 账户余额
    /// </summary>
    private void GetActMoney()
    {
        

        string leftMoney = BLL.CommonClass.CommonDataBLL.GetLeftMoney(Session["Member"].ToString());
        
        int level2 = 0;
        double djq = 0;
        string ssql = "select level2,fuxiaoin-fuxiaoout+fuxiaothin-fuxiaothout as djq from MemberInfoBalance" + CommonDataBLL.getMaxqishu() + " a ,MemberInfo b where  a.number=b.Number and a.number='" + Session["Member"].ToString() + "'";
        DataTable dts = DAL.DBHelper.ExecuteDataTable(ssql);
        if (dts.Rows.Count > 0)
        {
           level2= Convert.ToInt16(dts.Rows[0]["Level2"].ToString());
           djq = Convert.ToDouble(dts.Rows[0]["djq"].ToString());
        }
        double jdjy=0;
        //rmoney.Text = leftMoney;
        if (level2>0)
        {
            if (Convert.ToDouble(leftMoney) + djq > 10000)
            {
                jdjy =(Convert.ToDouble(leftMoney) + djq)- 10000;
                if (jdjy > Convert.ToDouble(leftMoney))
                {
                    jdjy = Convert.ToDouble(leftMoney);
                }
                
                
            }
            else
            {
                jdjy = 0;
                
            }
           // rmoney.Text = jdjy.ToString();
        }
        
        double b2 = Convert.ToDouble(DAL.MemberInfoDAL.Sxfparameter());
        Label6.Text = b2.ToString();
        string sql = "select isnull(Jackpot-Out,0) as usdt from memberinfo  where  number='" + Session["Member"].ToString() + "'";
        DataTable dt = DAL.DBHelper.ExecuteDataTable(sql);
        if (dt.Rows.Count > 0)
        {
            Label1.Text = dt.Rows[0]["usdt"].ToString();
        }
        //double bl = Convert.ToDouble(DAL.MemberInfoDAL.JFparameter("sfgc")) / 100;

        //Wyj.Value = String.Format("{0:F}", Convert.ToDouble(money.Text) * bl);
    }

    //private void GetLeftsxfMoney()
    //{
    //    string sxf = BLL.CommonClass.CommonDataBLL.GetLeftsxfMoney(money.Text);
    //    rmoney.Text = leftMoney;
    //}

    /// <summary>
    /// 绑定用户银行信息
    /// </summary>
    /// <param name="number"></param>
    //public void getBankInfo(string number)
    //{
    //    string sql = "select name,bankname,bankaddress,bankbranchname,bankcard,bankbook,Name from memberinfo m,memberbank mb where m.bankcode=mb.bankcode and number='" + Session["Member"].ToString() + "'";
    //    DataTable dt = DAL.DBHelper.ExecuteDataTable(sql);
    //    if (dt.Rows.Count > 0)
    //    {
    //        Label1.Text = dt.Rows[0]["bankname"].ToString() + dt.Rows[0]["bankbranchname"].ToString();
    //        //Label3.Text = Encryption.Encryption.GetEncryptionCard(dt.Rows[0]["bankcard"].ToString());
    //        Label3.Text = dt.Rows[0]["bankcard"].ToString();
    //        Label2.Text = Encryption.Encryption.GetDecipherName(dt.Rows[0]["name"].ToString());
    //    }
    //    else
    //    {
    //        Label1.Text = GetTran("007674", "未填写");
    //        Label2.Text = GetTran("007674", "未填写");
    //        Label3.Text = GetTran("007674", "未填写");
    //    }
    //}

    //提交保存
    protected void Button1_Click(object sender, EventArgs e)
    {
        ///设置特定值防止重复提交
        hid_fangzhi.Value = "0";


        ///判断会员账户是否被冻结
        if (DAL.MemberInfoDAL.CheckState(Session["Member"].ToString())) { Page.ClientScript.RegisterStartupScript(GetType(), null, "<script language='javascript'>alert('您的账户被冻结，不能使用提现申请');window.location.href='First.aspx';</script>"); return; }


        #region 为空验证
        DataTable dt_one = DAL.DBHelper.ExecuteDataTable("select Name,MobileTele,b.level from memberinfo a ,memberinfobalance1 b where a.number=b.number and a.Number='" + Session["Member"].ToString() + "'");

        

        double txMoney = 0; //提现金额
        if (!double.TryParse(this.money.Text.Trim(), out txMoney))
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('金额只能是数字！');", true);
            return;
        }
        int level=Convert.ToInt32( dt_one.Rows[0]["level"].ToString());
        if (DropDownList1.SelectedValue=="1")
        {
        
        //if (txMoney > 60 && level == 3)
        //{
        //    System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('3000配套提现金额不能大于60！');", true);
        //    return;
        //}
        //if (txMoney > 40 && level == 2)
        //{
        //    System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('1000配套提现金额不能大于40！');", true);
        //    return;
        //}
        //if (txMoney > 20 && level == 1)
        //{
        //    System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('500配套提现金额不能大于20！');", true);
        //    return;
        //}
        if (txMoney > 1000 )
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('提现金额不能大于1000！');", true);
            return;
        }
        //if (txMoney < 20)
        //{
        //    System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('提现金额不能小于20！');", true);
        //    return;
        //}
        }

        string bianhao = Session["Member"].ToString();

       

        #endregion


        double wyj = 0; //违约金
        double sxf = 0;  //手续费
        double xjye = Convert.ToDouble(BLL.CommonClass.CommonDataBLL.GetLeftMoney(bianhao)); //现金账户余额【单位：美元】

        try
        {
            string hkxz = " select value from JLparameter where jlcid=5";
            DataTable dthkxz = DAL.DBHelper.ExecuteDataTable(hkxz);
            string value = dthkxz.Rows[0]["value"].ToString();

            if (txMoney % 10 != 0)
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('提现金额只能为10的倍数');", true);
                this.money.Text = "";
                return;
            }
            if (RadioButtonList1.SelectedValue == "2")
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('重要提示！提现到商城默认充值到当前账户手机号的商城账户，如未注册商城，系统会稍后自动注册并充值');", true);
            }
            
            
        }
        catch(Exception eps)
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert(金额只能是数字');", true);
            return;
        }

        if (this.remark.Text.Length > 500)
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('对不起，备注输入的字符太多,最多500个字符！');", true);
            return;
        }

        object o_one1 = DAL.DBHelper.ExecuteScalar("select value from JLparameter where jlcid=3");
        string value1 = "0";//提现违约金比例
        if(o_one1!=null)
            value1 = o_one1.ToString();

        object o_one2 = DAL.DBHelper.ExecuteScalar("select WithdrawSXF from WithdrawSz");
        string sxfbl = "0.002";//提现手续费比例
        if (o_one2 != null)
            sxfbl = o_one2.ToString();

        int isjl = 0;
       
            if (txMoney > xjye)
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('金额超出最大可转金额！');", true);
                return;
            }
      
            if (txMoney > Convert.ToDouble(Label1.Text.Trim()))
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('金额超出最大可转金额！');", true);
                return;
            }
        isjl =Convert.ToInt32(DropDownList1.SelectedValue);

         WithdrawModel wDraw = new WithdrawModel();
       // wDraw.Number = Session["Member"].ToString();
        wDraw.ApplicationExpecdtNum = BLL.CommonClass.CommonDataBLL.getMaxqishu();

        wDraw.WithdrawMoney = txMoney;
        wDraw.WithdrawTime = DateTime.Now.ToUniversalTime();
        wDraw.OperateIP = BLL.CommonClass.CommonDataBLL.OperateIP;
        wDraw.Remark = this.remark.Text.Trim();
        wDraw.Bankcard = "";
        wDraw.Bankname = "";
        wDraw.Wyj =Convert.ToDouble(RadioButtonList1.SelectedValue);  //违约金
       
            wDraw.WithdrawSXF = 0;//手续费
        
        wDraw.IsJL = isjl;
        wDraw.blmoney =0; //提现手续费比例
        wDraw.Wyjbl = 0;//违约金比例
        string HyName = dt_one.Rows[0]["Name"].ToString();//订单类型
        string MobileTele = dt_one.Rows[0]["MobileTele"].ToString();//订单类型
        wDraw.Number = Session["Member"].ToString();
        wDraw.Bankcard = MobileTele;
        wDraw.Khname = HyName;
        MemberInfoModel memberinfo = new MemberInfoModel();
        memberinfo.Memberships =Convert.ToDecimal(this.money.Text.Trim());
        memberinfo.Number = Session["Member"].ToString();

        bool isSure = false;
        if (ViewState["edit"].ToString() == "0")
        {
            //if (DropDownList1.SelectedValue == "1")
            //{
            //    isSure = BLL.Registration_declarations.RegistermemberBLL.WithdrawMoney(wDraw);
            //}
            //else
            //{
                SqlTransaction tran = null;
                SqlConnection con = DBHelper.SqlCon();
                try
                {
                    con.Open();
                    tran = con.BeginTransaction();
                    if (!DAL.ECTransferDetailDAL.MemberCash(tran, wDraw))
                    {
                        tran.Rollback();
                        System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('提现申请失败！');", true);
                        return;
                    }

                    string strSql = "Update MemberInfo Set Out = Out + @Money Where number=@Number";
                    SqlParameter[] para1 = {
                                       new SqlParameter("@Money",SqlDbType.Money),
                                       new SqlParameter("@Number",SqlDbType.NVarChar,50)
                                   };
                    para1[0].Value = wDraw.WithdrawMoney;
                    para1[1].Value = wDraw.Number;
                    int count = (int)DBHelper.ExecuteNonQuery(tran, strSql, para1, CommandType.Text);
                    if (count == 0)
                    {
                        tran.Rollback();
                        isSure = false;
                    }


                    tran.Commit();
                    isSure = true;
                }
                catch
                {
                    tran.Rollback();
                    isSure = false;
                }
                finally
                {
                    con.Close();
                }
            //}
        }
        else
        {
            getOldBind(Session["Member"].ToString());
            wDraw.Id = id;
            //if (BLL.Registration_declarations.RegistermemberBLL.GetAuditState(wDraw.Id) == 3)
            //{
            //    Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('该申请单账号错误，不可以修改！');</script>");
            //    return;
            //}
            if (BLL.Registration_declarations.RegistermemberBLL.GetAuditState(wDraw.Id) == 1)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('已提交申请不可以重复提交！');</script>");
                return;
            }

            if (BLL.Registration_declarations.RegistermemberBLL.GetAuditState(wDraw.Id) == 0)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('已提交申请不可以重复提交！');</script>");
                return;
            }

          
                SqlTransaction tran = null;
                SqlConnection con = DBHelper.SqlCon();
                try
                {
                    con.Open();
                    tran = con.BeginTransaction();
                    if (!DAL.ECTransferDetailDAL.MemberCash(tran, wDraw))
                    {
                        tran.Rollback();
                        System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('提现申请失败！');", true);
                        return;
                    }

                    string strSql = "Update MemberInfo Set Out = Out + @Money Where number=@Number";
                    SqlParameter[] para1 = {
                                       new SqlParameter("@Money",SqlDbType.Money),
                                       new SqlParameter("@Number",SqlDbType.NVarChar,50)
                                   };
                    para1[0].Value = wDraw.WithdrawMoney;
                    para1[1].Value = wDraw.Number;
                    int count = (int)DBHelper.ExecuteNonQuery(tran, strSql, para1, CommandType.Text);
                    if (count == 0)
                    {
                        tran.Rollback();
                        isSure = false;
                    }


                    tran.Commit();
                    isSure = true;
                }
                catch
                {
                    tran.Rollback();
                    isSure = false;
                }
                finally
                {
                    con.Close();
                }
            
            ViewState["edit"] = "0";
            //isSure = BLL.Registration_declarations.RegistermemberBLL.updateWithdraw(wDraw);
        }


        if (isSure)
        {
            if (ViewState["edit"].ToString() == "0")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('提现申请完成！');setInterval('myInterval()',5000);function myInterval(){window.location.href='First.aspx'};</script>", false);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('修改成功！');location.href='first.aspx'</script>", false);
            }
            this.money.Text = "";
            this.remark.Text = "";
        }
        else
        {
            if (ViewState["edit"].ToString() == "0")
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('提现申请失败！');", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('修改失败！');</script>", false);
            }
        }
    }

    protected void btn_reset_Click(object sender, EventArgs e)
    {
        Response.Redirect("MemberWithdraw.aspx");
    }
   
}