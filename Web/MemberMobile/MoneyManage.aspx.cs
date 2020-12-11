using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BLL.MoneyFlows;
using Model;
using Model.Other;
using BLL.CommonClass;
using Encryption;
using BLL.Logistics;
using System.Data;
using System.Data.SqlClient;
using DAL;

public partial class Member_MoneyManage : BLL.TranslationBase
{
    public int bzCurrency = 0;
    public double huilv;
    protected void Page_Load(object sender, EventArgs e)
    {
        //获取标准币种
        bzCurrency = CommonDataBLL.GetStandard();
       // huilv = (AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString())));
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        //判断会员账户是否被冻结
        if (MemberInfoDAL.CheckState(Session["Member"].ToString())) { Page.ClientScript.RegisterStartupScript(GetType(), null, "<script language='javascript'>alert('您的账户被冻结，不能使用电子转账');window.location.href='First.aspx';</script>"); return; }       

        if (!IsPostBack)
        {
            try
            {
                bind();
                double Cash = 0;//石斛积分账户
                double Declarations = 0;//报单账户
                ECRemitDetailBLL.GetCashDeclarations(Session["Member"].ToString(), out Cash, out Declarations);
                shky.Text ="FTC可用："+Convert.ToString(Cash);
               // bdky.Text = "注册积分可用："+Convert.ToString(Declarations);
                
            }
            catch
            {
                Response.Redirect("../PassWordManage/CheckAdv.aspx?type=member&url=MoneyManage");
            }


            lbUsername.Text = Encryption.Encryption.GetDecipherName(DAL.DBHelper.ExecuteScalar("select isnull(Name,'') as Name from memberinfo where number='" + Session["Member"].ToString() + "'").ToString());
        }
        //translation();
    }

    //private void translation() {
    //    //TranControls(cb_check, new string[][] { new string[] { "007690", "我已收到该会员支付的上数金额" }, new string[] { "007691", "我未收到该会员支付的上数金额(如选此项将不能执行转帐)" } });
    //    TranControls(btnE, new string[][] { new string[] { "005842", "转账" } });
    //}

    public string getname(object name)
    {
        return Encryption.Encryption.GetDecipherName(name.ToString());
    }

    /// <summary>
    /// 绑定数据
    /// </summary>
    private void bind()
    {
            double Cash = 0;//现金账户
            double Declarations = 0;//报单账户//GetTran("001768", "现金账户余额") + "：" + 
            ECRemitDetailBLL.GetCashDeclarations(Session["Member"].ToString(), out Cash, out Declarations);
            int level2 = 0;
            double djq = 0;
            string ssql = "select level2,fuxiaoin-fuxiaoout+fuxiaothin-fuxiaothout as djq from MemberInfoBalance" + CommonDataBLL.getMaxqishu() + " a ,MemberInfo b where  a.number=b.Number and a.number='" + Session["Member"].ToString() + "'";
            DataTable dts = DAL.DBHelper.ExecuteDataTable(ssql);
            if (dts.Rows.Count > 0)
            {
                level2 = Convert.ToInt16(dts.Rows[0]["Level2"].ToString());
                djq = Convert.ToDouble(dts.Rows[0]["djq"].ToString());
            }
            double jdjy = 0;
           
            if (level2 > 0)
            {
                if (Convert.ToDouble(Cash) + djq > 30000)
                {
                    jdjy = (Convert.ToDouble(Cash) + djq) - 30000;
                    if (jdjy > Convert.ToDouble(Cash))
                    {
                        jdjy = Convert.ToDouble(Cash);
                    }


                }
                else
                {
                    jdjy = 0;

                }
                Cash = jdjy;
            }
            Literal1.Text = "<div class='changeLt' >FTC余额：</div>" + "<div class='changeRt'>" + (Cash).ToString("f2") + " </div>";
       // this.lbEmoney.Text = GetTran("001768", "现金账户余额")+" : "+(Cash * huilv).ToString("f2");
            //获取会员编号
            this.lbEnum.Text = Session["Member"].ToString();
            lbUsername.Text = Encryption.Encryption.GetDecipherName(DAL.DBHelper.ExecuteScalar("select isnull(name,'') as name from memberinfo where number='" + Session["Member"].ToString() + "'").ToString());

            rad_Outzh.Items.Insert(0, new ListItem("现金账户", ((int)OutAccountType.MemberCash).ToString()));
            //rad_Outzh.Items.Insert(1, new ListItem( GetTran("007252", "消费账户"), ((int)OutAccountType.MemberCons).ToString()));
            rad_Outzh.SelectedValue = ((int)OutAccountType.MemberCash).ToString();
            lit_xianjin.Text = "现金账户";
            //lit_xiaofei.Text = GetTran("007252", "消费账户");
    }
    /// <summary>
    /// 转账
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnE_Click(object sender, EventArgs e)
    {
        //设置特定值防止重复提交
        hid_fangzhi.Value = "0";

        //判断会员账户是否被冻结
        if (MemberInfoDAL.CheckState(Session["Member"].ToString())) { Page.ClientScript.RegisterStartupScript(GetType(), null, "<script language='javascript'>alert('您的账户被冻结，不能使用电子转账');window.location.href='First.aspx';</script>"); return; }    

        ECTransferDetailModel detailmodel = new ECTransferDetailModel();

        try { 

        //验证金额是否合法
        double money = 0.0;
        if (!double.TryParse(this.txtEmoney.Text.Trim(), out money))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('金额必须是数字，请重新输入！');</script>");
            return;
        }
        //验证是否输入金额
        if (this.txtEmoney.Text.Length <= 0 || money <= 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('转出金额必须大于0！');</script>");
            return;
        }
        //验证输入金额是否是10的倍数
           decimal mony=Convert.ToDecimal(this.txtEmoney.Text);
           if (mony % 10 != 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('转出金额必须是10的倍数');</script>");
            return;
        }
        double Cash = 0;//可用FTC账户
        double Declarations = 0;//消费账户
        ECRemitDetailBLL.GetCashDeclarations(Session["Member"].ToString(), out Cash, out Declarations);
        int level2 = 0;
        double djq = 0;
        string ssql = "select level2,fuxiaoin-fuxiaoout+fuxiaothin-fuxiaothout as djq from MemberInfoBalance" + CommonDataBLL.getMaxqishu() + " a ,MemberInfo b where  a.number=b.Number and a.number='" + Session["Member"].ToString() + "'";
        DataTable dts = DAL.DBHelper.ExecuteDataTable(ssql);
        if (dts.Rows.Count > 0)
        {
            level2 = Convert.ToInt16(dts.Rows[0]["Level2"].ToString());
            djq = Convert.ToDouble(dts.Rows[0]["djq"].ToString());
        }
        double jdjy = 0;

        if (level2 > 0)
        {
            if (Convert.ToDouble(Cash) + djq > 30000)
            {
                jdjy = (Convert.ToDouble(Cash) + djq) - 30000;
                if (jdjy > Convert.ToDouble(Cash))
                {
                    jdjy = Convert.ToDouble(Cash);
                }


            }
            else
            {
                jdjy = 0;

            }
            Cash = jdjy;
        }
        //验证转账金额最大值
        if (RadioButtonList1.SelectedValue == "1" && money > Cash)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('转出金额必须小于当前FTC可用账户最大可转金额！');</script>");
            return;
        }
        else if (RadioButtonList1.SelectedValue == "3" && money > Declarations)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000000", "转出金额必须小于当前消费账户最大可转金额！") + "');</script>");
            return;
        }
        else if (RadioButtonList1.SelectedValue == "2")
        {
            double bd = 0;
            string sql = "select pointBIn-pointBOut as bd from MemberInfo where number='" + Session["Member"].ToString() + "'";
            DataTable shj = DBHelper.ExecuteDataTable(sql);
            if (shj.Rows.Count > 0)
            {
                bd =Convert.ToDouble(shj.Rows[0][0].ToString());
                if (money > bd)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000000", "转出金额必须小于当前报单账户最大可转金额！") + "');</script>");
                    return;
                }
                
            }
        }
        else if (RadioButtonList1.SelectedValue == "4")
        {
            double bd = 0;
            double xj = 0;
            string sql = "select zzye-xuhao as bd,Jackpot-Out as xj from MemberInfo where number='" + Session["Member"].ToString() + "'";
            DataTable shj = DBHelper.ExecuteDataTable(sql);
            if (shj.Rows.Count > 0)
            {
                bd = Convert.ToDouble(shj.Rows[0][0].ToString());
                xj = Convert.ToDouble(shj.Rows[0][1].ToString());
                if (money > bd)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000000", "转出金额必须小于当前保险账户最大可转金额！") + "');</script>");
                    return;
                }
                if (xj > 10)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000000", "可用FTC使用完才可以操作保险账户转账！") + "');</script>");
                    return;
                }

            }
        }
        string number = "";
        if (RadioButtonList1.SelectedValue == "1" || RadioButtonList1.SelectedValue == "4")
        {
            string sql = "select number from MemberInfo where MobileTele='" + txt_InNumber.Text + "'";
            DataTable shj = DBHelper.ExecuteDataTable(sql);
            if (shj.Rows.Count > 0)
            {
                number = shj.Rows[0][0].ToString();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('无此编号，请检查后再重新输入111！')</script>");
                return;
            }


            string GetError1 = new AjaxClass().CheckNumberNetTui(number, lbEnum.Text.Trim());
            string GetError2 = new AjaxClass().CheckNumberNetTui(lbEnum.Text.Trim(), number);

            if (RadioButtonList1.SelectedValue == "1" && ((GetError1 != null && GetError1 != "") && (GetError2 != null && GetError2 != "")))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('可用FTC只能转给同一网络下的用户！');</script>", false);
                return;
            }
            if (RadioButtonList1.SelectedValue == "4" && ((GetError1 != null && GetError1 != "") && (GetError2 != null && GetError2 != "")))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('可用FTC只能转给同一网络下的用户！');</script>", false);
                return;
            }
            //验证转入编号
            if (number == "")
            {
                ScriptHelper.SetAlert(Page, "编号不能为空！");
                return;
            }
        }
        else
        {
            number = Session["Member"].ToString();
        }
        
        
        string lll = Request.Form["rad_Inzh"];
        string llll = rad_Outzh.SelectedValue;
        //验证会员是否自己给自己转入现金账户
        if (txt_InNumber.Text.Trim() == lbEnum.Text.Trim() && RadioButtonList1.SelectedValue == "1" )
        {
            ScriptHelper.SetAlert(Page,"自己不能转入自己的FTC账户");
            return;
        }
        //验证会员是否自己给自己转入消费账户
        //if (txt_InNumber.Text.Trim() == lbEnum.Text.Trim() && RadioButtonList1.SelectedValue == "3")
        //{
        //    ScriptHelper.SetAlert(Page, GetTran("000000", "自己不能转入自己的消费积分账户"));
        //    return;
        //}

        //验证会员转入账户是否服务机构订货款和转入编号是否服务机构编号
        //if (Request.Form["rad_Inzh"] == "2" && (int)DAL.DBHelper.ExecuteScalar("select count(0) as count from storeinfo where storeid= '" + txt_InNumber.Text.Trim() + "'") <= 0)
        //{
        //    ScriptHelper.SetAlert(Page, GetTran("007696", "转入服务机构订货款，转入编号必须是服务机构编号"));
        //    return;
        //}
        //else if (Request.Form["rad_Inzh"] != "2" && (int)DAL.DBHelper.ExecuteScalar("select count(0) as count from memberinfo where number= '" + txt_InNumber.Text.Trim() + "'") <= 0)
        //{
        //    ScriptHelper.SetAlert(Page, GetTran("007697", "转入编号不正确，请重新填写!"));
        //    return;
        //}

        //验证备注do
        if (this.txtEnote.Text.Length > 500)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('您输入的字符超出最大范围！');</script>");
            return;
        }
        //验证密码 非空
        //if (this.txtEpwd.Text.Trim() == "")
        //{
        //    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("006662", "请输入二级密码！") + "');</script>");
        //    return;
        //}

        ////验证电子账户密码是否正确
        //string word = Encryption.Encryption.GetEncryptionPwd(this.txtEpwd.Text.Trim(), this.lbEnum.Text.Trim());
        //int blean = ECRemitDetailBLL.ValidatePwd(Session["Member"].ToString(), word);
        //if (blean == 1)
        //{
        //    System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("001554", "电子账户密码不正确！") + "');", true);
        //    return;
        //}
        //else if (blean == 2)
        //{
        //    System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("007162", "对不起，您连续5次输入密码错，请2小时候在登录！") + "');", true);
        //    return;
        //}
        //是否收到款项
        if (!cboyishd.Checked)
        {
            ScriptHelper.SetAlert(Page,"请先确认是否收到该会员支付的上数金额！");
            return;
        }


        detailmodel.OutNumber = Session["Member"].ToString();
        detailmodel.OutMoney = double.Parse(this.txtEmoney.Text.Trim());
        detailmodel.ExpectNum = CommonDataBLL.getMaxqishu();
        
        detailmodel.OperateIP = CommonDataBLL.OperateIP;// 获取ip
        detailmodel.OperateNumber = Session["Member"].ToString();// 获取当前会员编号
        detailmodel.Remark = this.txtEnote.Text.Trim();
        if (RadioButtonList1.SelectedValue == "1")
        {
            detailmodel.InNumber = number;
            detailmodel.outAccountType = OutAccountType.MemberCash;
            detailmodel.inAccountType = InAccountType.MemberCash;
        }
        if (RadioButtonList1.SelectedValue == "2")
        {

            detailmodel.InNumber = number;
            detailmodel.outAccountType = OutAccountType.MemberBD;
            detailmodel.inAccountType = InAccountType.MemberCash;
        }
        if (RadioButtonList1.SelectedValue == "3")
        {

            detailmodel.InNumber = number;
            detailmodel.outAccountType = OutAccountType.MemberCons;
            detailmodel.inAccountType = InAccountType.MemberCash;
        }
        if (RadioButtonList1.SelectedValue == "4")
        {

            detailmodel.InNumber = number;
            detailmodel.outAccountType = OutAccountType.MemberTypeFx;
            detailmodel.inAccountType = InAccountType.MemberTypeFx;
        }
        }
        catch
        {

            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请输入正确的参数！');</script>");
        }
        SqlTransaction tran = null;
        SqlConnection conn = DAL.DBHelper.SqlCon();
        conn.Open();
        tran = conn.BeginTransaction();
        try
        {
            

            if (detailmodel.outAccountType == OutAccountType.MemberCash && detailmodel.inAccountType == InAccountType.MemberCash)
            {
                string number = "";
                string sql = "select number from MemberInfo where MobileTele='" + txt_InNumber.Text + "'";
                DataTable shj = DBHelper.ExecuteDataTable(sql);
                if (shj.Rows.Count > 0)
                {
                    number = shj.Rows[0][0].ToString();
                    detailmodel.InNumber = number;
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('无此编号，请检查后再重新输入222！')</script>");
                    return;
                }
                //对会员现金账户转入会员现金账户
                if (ECRemitDetailBLL.AddMoneyManageTran(detailmodel, 1, 2, tran) == 0)
                {
                    //string mot = "";
                    //string sqll = "select MobileTele from MemberInfo where number='" + lbEnum.Text + "'";
                    //DataTable xsj = DBHelper.ExecuteDataTable(sqll);
                    //if (xsj.Rows.Count > 0)
                    //{
                    //    mot = xsj.Rows[0][0].ToString();
                    //}
                    int ret = D_AccountBLL.AddAccountWithdrawTran(Session["Member"].ToString(), detailmodel.OutMoney, D_AccountSftype.MemberType, D_Sftype.BounsAccount, D_AccountKmtype.AccountTransfer, DirectionEnum.AccountReduced, "会员可用FTC账户转入" + txt_InNumber.Text.Trim() + "~会员保险账户", tran);
                    string st = "INSERT INTO MemberAccount(Number,HappenTime,HappenMoney,BalanceMoney,Direction,SfType,KmType,Remark) SELECT number,GETutcDATE()," + detailmodel.OutMoney + ",zzye-xuhao,0,8,4,'会员可用钱包转出" + detailmodel.OutMoney + "到" + txt_InNumber.Text + "保险钱包。' from memberinfo where Number='" + detailmodel.InNumber + "'";
                    int ret1 = DBHelper.ExecuteNonQuery(tran,st);
                    if (ret > 0 && ret1 > 0)
                    {
                        tran.Commit();
                        conn.Close();
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('转帐成功！');$('#tourl').show();document.getElementById('tourl').href='AccountDetail.aspx?type=AccountXJ';</script>");
                    }
                    else
                    {
                        tran.Rollback();
                        conn.Close();
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('转帐失败2222！');</script>");
                    }

                }
                
                else
                {
                    tran.Rollback();
                    conn.Close();
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('转帐失败1111！');</script>");
                }

            }
            if (detailmodel.outAccountType == OutAccountType.MemberBD && detailmodel.inAccountType == InAccountType.MemberCash)
            {

                //对会员现金账户转入会员消费账户
                if (ECRemitDetailBLL.AddMoneyManageTran(detailmodel, 3, 2, tran) == 0)
                {
                    //string mot = "";
                    //string sqll = "select MobileTele from MemberInfo where number='" + lbEnum.Text + "'";
                    //DataTable xsj = DBHelper.ExecuteDataTable(sqll);
                    //if (xsj.Rows.Count > 0)
                    //{
                    //    mot = xsj.Rows[0][0].ToString();
                    //}
                    int ret = D_AccountBLL.AddAccountWithdrawTran(Session["Member"].ToString(), detailmodel.OutMoney, D_AccountSftype.MemberTypeBd, D_Sftype.baodanFTC, D_AccountKmtype.AccountTransfer, DirectionEnum.AccountReduced, "会员报单账户转入" + detailmodel.InNumber + "~会员保险账户", tran);
                    int ret1 = D_AccountBLL.AddAccountTran(detailmodel.InNumber, detailmodel.OutMoney, D_AccountSftype.zzye, D_Sftype.zzye, D_AccountKmtype.RechargeByTransfer, DirectionEnum.AccountsIncreased, lbEnum.Text + "会员报单账户为" + detailmodel.InNumber + "会员保险账户转入" + detailmodel.OutMoney, tran);
                    if (ret > 0 && ret1 > 0)
                    {
                        tran.Commit();
                        conn.Close();
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('转帐成功！');$('#tourl').show();document.getElementById('tourl').href='AccountDetail.aspx?type=AccountXJ';</script>");
                    }
                    else
                    {
                        tran.Rollback();
                        conn.Close();
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('转帐失败2222！');</script>");
                    }

                }
                else
                {
                    tran.Rollback();
                    conn.Close();
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('转帐失败1111！');</script>");
                }
            }

            if (detailmodel.outAccountType == OutAccountType.MemberCons && detailmodel.inAccountType == InAccountType.MemberCash)
            {
               
                //对会员消费账户转入会员消费账户
                if (ECRemitDetailBLL.AddMoneyManageTran(detailmodel, 0, 2, tran) == 0)
                {
                    //string mot = "";
                    //string sqll = "select MobileTele from MemberInfo where number='" + lbEnum.Text + "'";
                    //DataTable xsj = DBHelper.ExecuteDataTable(sqll);
                    //if (xsj.Rows.Count > 0)
                    //{
                    //    mot = xsj.Rows[0][0].ToString();
                    //}
                    int ret = D_AccountBLL.AddAccountWithdrawTran(Session["Member"].ToString(), detailmodel.OutMoney, D_AccountSftype.MemberCoshType, D_Sftype.EleAccount, D_AccountKmtype.AccountTransfer, DirectionEnum.AccountReduced, "会员消费账户转入" + txt_InNumber.Text.Trim() + "~会员可用FTC账户", tran);
                    int ret1 = D_AccountBLL.AddAccountTran(Session["Member"].ToString(), detailmodel.OutMoney, D_AccountSftype.zzye, D_Sftype.zzye, D_AccountKmtype.RechargeByTransfer, DirectionEnum.AccountsIncreased, lbEnum.Text + "会员消费账户为" + txt_InNumber.Text.Trim() + "会员可用FTC账户转入" + detailmodel.OutMoney, tran);
                    if (ret > 0 && ret1 > 0)
                    {
                        tran.Commit();
                        conn.Close();
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('转帐成功！');$('#tourl').show();document.getElementById('tourl').href='AccountDetail.aspx?type=AccountXJ';</script>");
                    }
                    else
                    {
                        tran.Rollback();
                        conn.Close();
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('转帐失败2222！');</script>");
                    }

                }
                else
                {
                    tran.Rollback();
                    conn.Close();
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('转帐失败1111！');</script>");
                }
            }
            if (RadioButtonList1.SelectedValue == "4")
            {

                //对会员保险账户转入会员保险账户
               
                    string mot = "";
                    int ret = 0;
                    int ret1 = 0;
                    int ret2 = 0;
                    int ret3 = 0;
                    string sqll = "select number from MemberInfo where MobileTele='" + txt_InNumber.Text + "'";
                    DataTable xsj = DBHelper.ExecuteDataTable(sqll);
                    if (xsj.Rows.Count > 0)
                    {
                        mot = xsj.Rows[0][0].ToString();

                        ret = DBHelper.ExecuteNonQuery(tran, "update memberinfo set xuhao+=" + detailmodel.OutMoney + " where number='" + Session["Member"].ToString() + "'");
                        ret1 = DBHelper.ExecuteNonQuery(tran,"update memberinfo set zzye+=" + detailmodel.OutMoney + "  where number='" + mot + "'");
                        ret2 = DBHelper.ExecuteNonQuery(tran, "INSERT INTO MemberAccount(Number,HappenTime,HappenMoney,BalanceMoney,Direction,SfType,KmType,Remark) SELECT number,GETutcDATE()," + detailmodel.OutMoney + ",zzye-xuhao,1,8,4,'会员保险钱包转出" + detailmodel.OutMoney + "到" + txt_InNumber.Text + "保险钱包。' from memberinfo where Number='" + Session["Member"].ToString() + "'");
                        ret3 = DBHelper.ExecuteNonQuery(tran, "INSERT INTO MemberAccount(Number,HappenTime,HappenMoney,BalanceMoney,Direction,SfType,KmType,Remark) SELECT number,GETutcDATE()," + detailmodel.OutMoney + ",zzye-xuhao,0,8,4,'会员保险钱包转出" + detailmodel.OutMoney + "到" + txt_InNumber.Text + "保险钱包。' from memberinfo where Number='" + mot + "'");

                    }
                    if (ret > 0 && ret1 > 0)
                    {
                        tran.Commit();
                        conn.Close();
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('转帐成功！');$('#tourl').show();document.getElementById('tourl').href='AccountDetail.aspx?type=AccountXJ';</script>");
                    }
                    else
                    {
                        tran.Rollback();
                        conn.Close();
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('转帐失败2222！');</script>");
                    }

               
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('转帐失败444！');</script>");
            }
                

                

                this.txtEmoney.Text = string.Empty;
                this.txt_InNumber.Text = string.Empty;
                txtEnote.Text = string.Empty;
            
        }
        catch {
            //tran.Rollback();
            conn.Close();
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('转帐失败！123');</script>");
        }
        

    }

    //更具不同选项，转入账户也不同
    protected void rad_Outzh_SelectedIndexChanged(object sender, EventArgs e)
    {
        double Cash = 0;//现金账户
        double Declarations = 0;//报单账户
        ECRemitDetailBLL.GetCashDeclarations(Session["Member"].ToString(), out Cash, out Declarations);
        lit_xiaofei.Text = "";
        lit_xianjin.Text = "";
        lit_fuwujigou.Text = "";
        if (rad_Outzh.SelectedValue == ((int)OutAccountType.MemberCons).ToString()  && (int)DAL.DBHelper.ExecuteScalar("select count(0) as count from storeinfo where number= '" + lbEnum.Text.Trim() + "'") <= 0)
        {
            rad_xiaofei.Visible = true;
            rad_xianjin.Visible = false;
            rad_fuwujigou.Visible = false;
            lit_xiaofei.Text = "消费账户";
            Literal1.Text = "<div class='changeLt' >消费账户余额：</div>" + "<div class='changeRt'>" + (Declarations * huilv).ToString("0.00") + " </div>";
          //  lbEmoney.Text = GetTran("007391", "消费账户余额 ") + "：" + (Declarations*huilv).ToString("0.00");
        }
        else if (rad_Outzh.SelectedValue == ((int)OutAccountType.MemberCons).ToString() && (int)DAL.DBHelper.ExecuteScalar("select count(0) as count from storeinfo where number= '" + lbEnum.Text.Trim() + "'") > 0)
        {
            rad_xiaofei.Visible = true;
            rad_xianjin.Visible = false;
            rad_fuwujigou.Visible = true;
            lit_xiaofei.Text ="消费账户";
            lit_fuwujigou.Text ="服务机构订货款";
            Literal1.Text = "<div class='changeLt' >消费账户余额 ：</div>" + "<div class='changeRt'>" + (Declarations * huilv).ToString("0.00") + " </div>";
          //  lbEmoney.Text = GetTran("007391", "消费账户余额 ") + "：" + (Declarations * huilv).ToString("0.00");
            rad_xiaofei.Checked = true;
            rad_xianjin.Checked = false;
            rad_fuwujigou.Checked = false;
        }
        else if (rad_Outzh.SelectedValue == ((int)OutAccountType.MemberCash).ToString())
        {
            rad_xiaofei.Visible = true;
            rad_xianjin.Visible = true;
            rad_fuwujigou.Visible = false;
            lit_xianjin.Text ="现金账户";
            lit_xiaofei.Text = "消费账户";
            Literal1.Text = "<div class='changeLt' >现金账户余额：</div>" + "<div class='changeRt'>" + (Cash * huilv).ToString("f2") + " </div>";
           // lbEmoney.Text = GetTran("001768","现金账户余额") + "：" + (Cash*huilv).ToString("0.00");
            rad_xianjin.Checked = true;
            rad_xiaofei.Checked = false;
            rad_fuwujigou.Checked = false;
        }
        
    }

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "2")
        {
            txt_InNumber.Visible = false;
            lab_nicheng.Visible = false;
            string sql = "select pointBIn-pointBOut as bd from MemberInfo where number='" + Session["Member"].ToString() + "'";
            DataTable shj = DBHelper.ExecuteDataTable(sql);
            if (shj.Rows.Count > 0)
            {
                shky.Text = "报单账户余额：" + shj.Rows[0][0].ToString();
             

            }
        }
        if (RadioButtonList1.SelectedValue == "1")
        {
            txt_InNumber.Visible = true;
            lab_nicheng.Visible = true;
            double Cash = 0;//可用账户
            double Declarations = 0;//消费账户
            ECRemitDetailBLL.GetCashDeclarations(Session["Member"].ToString(), out Cash, out Declarations);
            shky.Text = "可用FTC余额：" + Convert.ToString(Cash);
        }
        if (RadioButtonList1.SelectedValue == "3")
        {
            txt_InNumber.Visible = false;
            lab_nicheng.Visible = false;
            double Cash = 0;//可用账户
            double Declarations = 0;//消费账户
            ECRemitDetailBLL.GetCashDeclarations(Session["Member"].ToString(), out Cash, out Declarations);
            shky.Text = "消费积分余额：" + Convert.ToString(Declarations);
            // bdky.Text = "注册积分可用："+Convert.ToString(Declarations);
        }

        if (RadioButtonList1.SelectedValue == "4")
        {
            txt_InNumber.Visible = true;
            lab_nicheng.Visible = true;
            string sql = "select zzye-xuhao as bd from MemberInfo where number='" + Session["Member"].ToString() + "'";
            DataTable shj = DBHelper.ExecuteDataTable(sql);
            if (shj.Rows.Count > 0)
            {
                shky.Text = "保险账户余额：" + shj.Rows[0][0].ToString();


            }
        }
    }
}
