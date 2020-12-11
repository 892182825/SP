using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.CommonClass;

public partial class MemberMobile_TxDetailDCS : BLL.TranslationBase
{
    public int bzCurrency = 0;
    public double huilv;
    protected void Page_Load(object sender, EventArgs e)
    {
        //获取标准币种
        bzCurrency = CommonDataBLL.GetStandard();
        huilv = (AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString())));
        var hybh = Session["Member"].ToString();
        var hkid = Request.QueryString["hkid"];
        string dt_one = "select id,hkid,Ppje,Khname,bankcard,bankname,shenHestate,WithdrawMoney,HkDj,hkpzImglj from withdraw where shenhestate in(2,11) and IsJL=1 and Khname is not  null and number='" + hybh + "' order by WithdrawTime desc";
        if (!IsPostBack)
        {
            DataTable dt = DAL.DBHelper.ExecuteDataTable(dt_one);
            rep_km.DataSource = dt;
            rep_km.DataBind();
        }
        
    }

    protected void rep_km_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

        switch (e.CommandName)
        {
            //已到账
            case "yidz":
                string KeywordCode1 = e.CommandArgument.ToString();//提现表的id  
                DataTable dt_one = DAL.DBHelper.ExecuteDataTable("select Ppje,hkid,WithdrawSXF,number,Wyj,Isjs from withdraw where id=" + KeywordCode1 + " and IsJL=1");
                string Ppje = Math.Round(Convert.ToDouble(dt_one.Rows[0]["Ppje"]), 2).ToString("0.00");//匹配金额
                string hkid = dt_one.Rows[0]["hkid"].ToString();//汇款id
                string WithdrawSXF = dt_one.Rows[0]["WithdrawSXF"].ToString();//冻结的手续费
                string Wyj = dt_one.Rows[0]["Wyj"].ToString();//违约金
                string number = dt_one.Rows[0]["number"].ToString();//提现会员
                string Isjs = dt_one.Rows[0]["Isjs"].ToString();//解释（是否超时）

                DataTable dt_one1 = DAL.DBHelper.ExecuteDataTable("select RemitNumber from  remittances where ID=" + hkid);//查询充值方
                string RemitNumber = dt_one1.Rows[0]["RemitNumber"].ToString();//充值会员

                DataTable dt_one2 = DAL.DBHelper.ExecuteDataTable("select Jackpot,Out,membership from  memberinfo where Number='" + RemitNumber + "'");//查询充值方的现金账户
                //查询充值方的现金账户余额
                decimal HkZhye = decimal.Parse(dt_one2.Rows[0]["Jackpot"].ToString()) - decimal.Parse(dt_one2.Rows[0]["Out"].ToString());

                DataTable dt_one3 = DAL.DBHelper.ExecuteDataTable("select Jackpot,Out,membership from  memberinfo where Number='" + number + "'");//查询提现方的现金账户
                //查询提现方的现金账户余额
                decimal TxZhye = decimal.Parse(dt_one3.Rows[0]["Jackpot"].ToString()) - decimal.Parse(dt_one3.Rows[0]["Out"].ToString());

                DataTable dt_one4 = DAL.DBHelper.ExecuteDataTable("select ExpectNum from  config");
                string Maxqs = dt_one4.Rows[0]["ExpectNum"].ToString();//最大期数

                var Issure = WithdrawMoney(Ppje, hkid, WithdrawSXF, number, RemitNumber, KeywordCode1, HkZhye, TxZhye, Wyj, Isjs, Maxqs, huilv);
                if (Issure)
                {
                    //发送系统邮件
                    string sendnumber = Session["Member"].ToString();

                    DataTable dtt = DAL.DBHelper.ExecuteDataTable(" select top 1  WithdrawMoney,bankcard,bankname   from  Withdraw where hkid= " + hkid);
                    if (dtt != null && dtt.Rows.Count > 0)
                    {

                        double wm = Convert.ToDouble(dtt.Rows[0]["WithdrawMoney"]);
                        string bkcd = dtt.Rows[0]["bankcard"].ToString();
                        string bkname = dtt.Rows[0]["bankname"].ToString();
                        string content = "<b style='margin:20px; '>汇款查收提醒</b> <p> 会员" + sendnumber + " 确认收到您向" + bkname + " " + bkcd + "转账汇款" + wm.ToString("0.00") + " ,请到您现金账户查收。</p> <p>系统邮件</p>";
                        SendEmail.SendSystemEmail(sendnumber, "2", content, RemitNumber);
                    }

                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("009050", "到账成功") + "！');location.href='TxDetailYDZ.aspx'</script>", false);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("009051", "到账失败") + "！');location.href='TxDetailDCS.aspx'</script>", false);
                }

                break;

            //通知查收
            case "Hfxx":
                string KeywordCode = e.CommandArgument.ToString();//取得参数  
                string url = "TxDetailHfxx.aspx?id=" + KeywordCode;
                Response.Redirect(url);
                break;
            //查看汇款凭证
            case "sc":
                string KeywordCode2 = e.CommandArgument.ToString();//取得参数  
                string url2 = "TxDetailCkhkpz.aspx?id=" + KeywordCode2;
                Response.Redirect(url2);
                break;

        }
    }

    protected void rep_km_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            LinkButton b = e.Item.FindControl("Button1") as LinkButton;
            Button jieshi = e.Item.FindControl("jieshi") as Button;
            Button jieshi1 = e.Item.FindControl("jieshi1") as Button;
            LinkButton b2 = e.Item.FindControl("Button2") as LinkButton;
            HiddenField h = e.Item.FindControl("HiddenField1") as HiddenField;
            Button jieshi2 = e.Item.FindControl("jieshi2") as Button;
            HiddenField h3 = e.Item.FindControl("HiddenField3") as HiddenField;
            Panel pn1 = e.Item.FindControl("hkpzid") as Panel ;
            if (h3.Value=="")
            {
                pn1.Visible = false;
            }
            string dt_one = "select w.id,w.hkid,w.bankcard,w.bankname,w.shenHestate,w.WithdrawMoney,m.name,RemittancesDate,w.auditingtime,w.Isjs,w.TxJs from memberinfo m, withdraw w,remittances r where m.Number = w.number and w.hkid = r.ID and  (w.shenhestate=11 or w.shenhestate=2) and hkid='" + h.Value + "' order by auditingtime desc";
            DataTable dt = DAL.DBHelper.ExecuteDataTable(dt_one);
            var Isjs = dt.Rows[0]["Isjs"].ToString();
            var TxJs = dt.Rows[0]["TxJs"].ToString();
            if (h.Value == "0")
            {
                b2.Visible = false;
            }
            if (h.Value != "0")
            {
                b2.Visible = true;
            }
            if (Isjs == "0")
            {
                jieshi.Visible = true;
                jieshi1.Visible = false;
                jieshi2.Visible = false;

            }
            if (Isjs == "1")
            {
                if (TxJs != "")
                {
                    jieshi.Visible = false;
                    jieshi1.Visible = false;
                    jieshi2.Visible = true;

                }
                else
                {
                    jieshi.Visible = false;
                    jieshi1.Visible = true;
                    jieshi2.Visible = false;

                }
            }
        }
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            LinkButton ib = e.Item.FindControl("Button1") as LinkButton;
            ib.Text = GetTran("008310", "已收到");
            LinkButton ib2 = e.Item.FindControl("Button2") as LinkButton;
            ib2.Text = GetTran("008311", "汇方信息");
            Button ib3 = e.Item.FindControl("jieshi") as Button;
            ib3.Text = GetTran("009025", "解释");
            Button ib4 = e.Item.FindControl("jieshi2") as Button;
            ib4.Text = GetTran("008312", "解释成功");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Ppje"></param>
    /// <param name="hkid"></param>
    /// <param name="WithdrawSXF"></param>
    /// <param name="number"></param>
    /// <param name="RemitNumber"></param>
    /// <returns></returns>
    public static bool WithdrawMoney(string Ppje, string hkid, string WithdrawSXF, string number, string RemitNumber, string KeywordCode1, decimal HkZhye, decimal TxZhye, string Wyj, string Isjs, string Maxqs, double huilv)
    {
        using (SqlConnection conn = new SqlConnection(DBHelper.connString))
        {
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();

            try
            {
                var Hktime = DateTime.Now.AddHours(-8);
                if (!SetMemberShip1(tran, decimal.Parse(Ppje.ToString()), RemitNumber))
                {
                    tran.Rollback();
                    return false;
                }
                if (!SetMemberShip2(tran, decimal.Parse(Ppje.ToString()), decimal.Parse(WithdrawSXF.ToString()), number, decimal.Parse(Wyj.ToString()), Isjs))
                {
                    tran.Rollback();
                    return false;
                }
                if (!SetMemberShip6(tran, int.Parse(hkid), Maxqs))
                {
                    tran.Rollback();
                    return false;
                }

                if (!SetMemberShip3(tran, int.Parse(KeywordCode1), Hktime))
                {
                    tran.Rollback();
                    return false;
                }

                if (!SetMemberShip4(tran, RemitNumber, Hktime, decimal.Parse(Ppje.ToString()), HkZhye, huilv))
                {
                    tran.Rollback();
                    return false;
                }

                if (!SetMemberShip5(tran, number, Hktime, decimal.Parse(Ppje.ToString()), TxZhye, Isjs, decimal.Parse(WithdrawSXF.ToString()), decimal.Parse(Wyj.ToString()), huilv))
                {
                    tran.Rollback();
                    return false;
                }

                tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
    }

    /// <summary>
    /// 向充值方的现金总入加上匹配金额
    /// </summary>
    /// <param name="tran"></param>
    /// <param name="Ppje"></param>
    /// <param name="RemitNumber"></param>
    /// <returns></returns>
    public static bool SetMemberShip1(SqlTransaction tran, decimal Ppje, string RemitNumber)
    {
        string strSql = "update memberinfo set jackpot = jackpot + @Ppje where Number = @RemitNumber";
        SqlParameter[] para = {
                                      new SqlParameter("@Ppje",SqlDbType.Money),
                                      new SqlParameter("@RemitNumber",SqlDbType.NVarChar,50),

                                  };
        para[0].Value = Ppje;
        para[1].Value = RemitNumber;


        int count = (int)DBHelper.ExecuteNonQuery(tran, strSql, para, CommandType.Text);
        if (count <= 0)
            return false;
        return true;
    }

    /// <summary>
    /// 操作提现方的账户
    /// </summary>
    /// <param name="tran"></param>
    /// <param name="Ppje"></param>
    /// <param name="WithdrawSXF"></param>
    /// <param name="number"></param>
    /// <param name="Wyj"></param>
    /// <param name="Isjs"></param>
    /// <returns></returns>
    public static bool SetMemberShip2(SqlTransaction tran, decimal Ppje, decimal WithdrawSXF, string number, decimal Wyj, string Isjs)
    {
        int count = 0;
        string strSql = "";
        if (Isjs == "0")
        {
            strSql = "update memberinfo set Out=Out+@Ppje+@WithdrawSXF,membership=membership-( @Ppje + @WithdrawSXF+@Wyj) where Number=@number";
            SqlParameter[] para = {
                                      new SqlParameter("@Ppje",SqlDbType.Money),
                                      new SqlParameter("@WithdrawSXF",SqlDbType.Money),
                                       new SqlParameter("@number",SqlDbType.NVarChar,50),
                                       new SqlParameter("@Wyj",SqlDbType.Money),

                                  };
            para[0].Value = Ppje;
            para[1].Value = WithdrawSXF;
            para[2].Value = number;
            para[3].Value = Wyj;

            count = (int)DBHelper.ExecuteNonQuery(tran, strSql, para, CommandType.Text);

        }
        else
        {
            strSql = "update memberinfo set Out=Out+@Ppje+@WithdrawSXF+@Wyj,membership=membership-( @Ppje + @WithdrawSXF+@Wyj) where Number=@number";
            SqlParameter[] para = {
                                      new SqlParameter("@Ppje",SqlDbType.Money),
                                      new SqlParameter("@WithdrawSXF",SqlDbType.Money),
                                       new SqlParameter("@number",SqlDbType.NVarChar,50),
                                       new SqlParameter("@Wyj",SqlDbType.Money),

                                  };
            para[0].Value = Ppje;
            para[1].Value = WithdrawSXF;
            para[2].Value = number;
            para[3].Value = Wyj;

            count = (int)DBHelper.ExecuteNonQuery(tran, strSql, para, CommandType.Text);

        }


        if (count <= 0)
            return false;
        return true;
    }

    /// <summary>
    /// 修改提现记录的审核状态
    /// </summary>
    /// <param name="tran"></param>
    /// <param name="id"></param>
    /// <param name="Maxqs"></param>
    /// <returns></returns>
    public static bool SetMemberShip6(SqlTransaction tran, int id, string Maxqs)
    {
        string strSql = "update Remittances set IsGSQR=1,PayExpectNum=@Maxqs where ID=@id";
        SqlParameter[] para = {
                                      new SqlParameter("@id",SqlDbType.Int),
                                      new SqlParameter("@Maxqs",SqlDbType.Int),
                                  };
        para[0].Value = id;
        para[1].Value = int.Parse(Maxqs);
        int count = (int)DBHelper.ExecuteNonQuery(tran, strSql, para, CommandType.Text);
        if (count <= 0)
            return false;
        return true;
    }

    /// <summary>
    /// 修改提现记录的审核状态
    /// </summary>
    /// <param name="tran"></param>
    /// <param name="id"></param>
    /// <param name="Hktime"></param>
    /// <returns></returns>
    public static bool SetMemberShip3(SqlTransaction tran, int id, DateTime Hktime)
    {
        /*
        DataTable dt_one2 = DAL.DBHelper.ExecuteDataTable("select txid_ys from Withdraw where id=" + id);
        string txid_ys = dt_one2.Rows[0]["txid_ys"].ToString();

        string strSql = "update withdraw set shenhestate=20,WithdrawTime=@Hktime,TxDj=1,isAuditing=2 where id=@id or id=" + txid_ys;
        SqlParameter[] para = {
            new SqlParameter("@id",SqlDbType.Int),
            new SqlParameter("@Hktime",SqlDbType.DateTime),
        };
        para[0].Value = id;
        para[1].Value = Hktime;
        int count = (int)DBHelper.ExecuteNonQuery(tran, strSql, para, CommandType.Text);
        if (count <= 0)
            return false;
        return true;*/


        //发送系统邮件
     


        string strSql = "update withdraw set shenhestate=20,WithdrawTime=@Hktime,TxDj=1,isAuditing=2 where id=@id";
        SqlParameter[] para = {
            new SqlParameter("@id",SqlDbType.Int),
            new SqlParameter("@Hktime",SqlDbType.DateTime),
        };
        para[0].Value = id;
        para[1].Value = Hktime;
        int count = (int)DBHelper.ExecuteNonQuery(tran, strSql, para, CommandType.Text);
        if (count <= 0)
            return false;
        return true;
    }

    /// <summary>
    /// 添加汇款方的对账记录
    /// </summary>
    /// <param name="tran"></param>
    /// <param name="RemitNumber"></param>
    /// <param name="Hktime"></param>
    /// <param name="Ppje"></param>
    /// <param name="HkZhye"></param>
    /// <param name="huilv"></param>
    /// <returns></returns>
    public static bool SetMemberShip4(SqlTransaction tran, string RemitNumber, DateTime Hktime, decimal Ppje, decimal HkZhye, double huilv)
    {
        string strSql = "insert into MemberAccount values(@RemitNumber,@Hktime,@Ppje,@HkZhye+@Ppje,0,1,25,'向会员" + RemitNumber + "现金账户充值了" + Ppje * Convert.ToDecimal(huilv) + "元')";
        SqlParameter[] para = {
                                      new SqlParameter("@RemitNumber",SqlDbType.NVarChar,50),
                                       new SqlParameter("@Hktime",SqlDbType.DateTime,8),
                                       new SqlParameter("@Ppje",SqlDbType.Money),
                                       new SqlParameter("@HkZhye",SqlDbType.Money),
        };
        para[0].Value = RemitNumber;
        para[1].Value = Hktime;
        para[2].Value = Ppje;
        para[3].Value = HkZhye;
        int count = (int)DBHelper.ExecuteNonQuery(tran, strSql, para, CommandType.Text);
        if (count <= 0)
            return false;
        return true;
    }

    /// <summary>
    /// 添加提现方的对账记录
    /// </summary>
    /// <param name="tran"></param>
    /// <param name="number"></param>
    /// <param name="Hktime"></param>
    /// <param name="Ppje"></param>
    /// <param name="TxZhye"></param>
    /// <param name="Isjs"></param>
    /// <param name="WithdrawSXF"></param>
    /// <param name="Wyj"></param>
    /// <param name="huilv"></param>
    /// <returns></returns>
    public static bool SetMemberShip5(SqlTransaction tran, string number, DateTime Hktime, decimal Ppje, decimal TxZhye, string Isjs, decimal WithdrawSXF, decimal Wyj, double huilv)
    {
        int count = 0;
        string strSql = "";
        if (Isjs == "0")
        {
            strSql = "insert into MemberAccount values(@number,@Hktime,@Ppje+@WithdrawSXF,@TxZhye-(@Ppje+@WithdrawSXF),1,1,26,'向会员" + number + "的现金账户提现了" + Math.Round((Ppje + WithdrawSXF) * Convert.ToDecimal(huilv)).ToString("#0.00") + "元')";
            SqlParameter[] para = {
                new SqlParameter("@number",SqlDbType.NVarChar,50),
                new SqlParameter("@Hktime",SqlDbType.DateTime,8),
                new SqlParameter("@Ppje",SqlDbType.Money),
                new SqlParameter("@TxZhye",SqlDbType.Money),
                new SqlParameter("@WithdrawSXF",SqlDbType.Money),
                new SqlParameter("@Wyj",SqlDbType.Money),
            };
            para[0].Value = number;
            para[1].Value = Hktime;
            para[2].Value = Ppje;
            para[3].Value = TxZhye;
            para[4].Value = WithdrawSXF;
            para[5].Value = Wyj;
            count = (int)DBHelper.ExecuteNonQuery(tran, strSql, para, CommandType.Text);


        }
        else
        {

            strSql = "insert into MemberAccount values(@number,@Hktime,@Ppje+@WithdrawSXF+@Wyj,@TxZhye-(@Ppje+@WithdrawSXF+@Wyj),1,1,26,'向会员" + number + "的现金账户提现了" + Ppje * Convert.ToDecimal(huilv) + "元')";
            SqlParameter[] para = {
                new SqlParameter("@number",SqlDbType.NVarChar,50),
                new SqlParameter("@Hktime",SqlDbType.DateTime,8),
                new SqlParameter("@Ppje",SqlDbType.Money),
                new SqlParameter("@TxZhye",SqlDbType.Money),
                new SqlParameter("@WithdrawSXF",SqlDbType.Money),
                new SqlParameter("@Wyj",SqlDbType.Money),
            };
            para[0].Value = number;
            para[1].Value = Hktime;
            para[2].Value = Ppje;
            para[3].Value = TxZhye;
            para[4].Value = WithdrawSXF;
            para[5].Value = Wyj;
            count = (int)DBHelper.ExecuteNonQuery(tran, strSql, para, CommandType.Text);

        }

        if (count <= 0)
            return false;
        return true;
    }
}