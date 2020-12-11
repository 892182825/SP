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

using Model.Other;
using BLL.MoneyFlows;
using System.Text;
using Encryption;
using Model;
using BLL.CommonClass;
using DAL;
using System.Collections.Generic;
using BLL.Logistics;

public partial class Company_DeductXF : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        //Permissions.CheckManagePermission(EnumCompanyPermission.xiaofeijifen);

        Response.Cache.SetExpires(DateTime.Now);
        if (!IsPostBack)
        {
            BtnConfirm_Click(null, null);
        }
        Translations_More();
    }

    protected void Translations_More()
    {


        TranControls(gvdeduct, new string[][]
                        {
                            new string[] {"000000","<input type=\"checkbox\" id=\"checkAll\" onclick=\"doSelect(this);\" />"},
                            new string[] { "000000","ID"},
                            new string[] { "000000","状态"},
                            new string[] { "001195","编号"},
                            new string[] { "000000","手机号"},
                            new string[] { "000000","申请时间"},
                            new string[] { "000000","兑换金额"},
                            new string[] { "000000","兑换总额"},
                            new string[] { "000000","备注"},
                            



                        }
            );

        //TranControls(DropDownList2, new string[][]
        //                {
        //                    new string[] { "001871","条件不限"},
        //                    new string[] { "001195","编号"},
        //                    new string[] { "000000","消费积分"},
        //                }
        // );
    }
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnConfirm_Click(object sender, EventArgs e)
    {
        //int expct = this.DropDownQiShu.ExpectNum;
        string search = string.Empty;
        string mark = string.Empty;
        string mark2 = string.Empty;
        if (TxtSearch.Text != "")
        {

            search = TxtSearch.Text;
        }
        if ( Txtjf1.Text != "" )
        {
            double txMoney = 0;
            if (!double.TryParse(this.Txtjf1.Text.Trim(), out txMoney))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("005869", "金额只能是数字！") + "');</script>");
                return;
            }
            mark = Txtjf1.Text;

        }
        if ( Txtjf2.Text != "")
        {
            double txMoney = 0;
            if (!double.TryParse(this.Txtjf2.Text.Trim(), out txMoney))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("005869", "金额只能是数字！") + "');</script>");
                return;
            }
            mark2 = Txtjf2.Text;

        }

        string sql = " 1=1 ";
        if (txtregTimeStart.Text != "")
        {
            sql += "  and XFTime>='" + txtregTimeStart.Text + "'";
        }
        if (txtregTimeEnd.Text != "")
        {
            sql += "  and XFTime<='" + txtregTimeEnd.Text + " 23:59:59'";
        }


        if (mark != string.Empty)
        {
            sql = sql + " and XFON >=" + mark.Trim() + " ";
        }
        if (mark2 != string.Empty)
        {
            sql = sql + " and XFON <=" + mark2.Trim() + " ";
        }
        else if (search != string.Empty)
        {
            sql = sql + " and  Number  like '%" + search + "%' ";
        }
        if (strzt.SelectedValue != "-1")
        {
            sql = sql + " and  XFState=" + strzt.SelectedValue + " ";
        }

        ViewState["sql"] = "select case when XFState = '0' then '已提交' when XFState = '1' then '处理中' when XFState ='2' then '完成'  when XFState ='3' then '拒绝'  end  as zt,* from MemberCashXF where" + sql + " order by XFTime desc";
        Pager pager = Page.FindControl("Pager1") as Pager;
        pager.PageBind(0, 10, "MemberCashXF", "case when XFState = '0' then '已提交' when XFState = '1' then '处理中' when XFState ='2' then '完成'  when XFState ='3' then '拒绝'  end  as zt,*",
            sql, "id", "gvdeduct");
        Translations_More();
    }
    /// <summary>
    /// 解密姓名
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public string getname(object name)
    {
        return Encryption.Encryption.GetDecipherName(name.ToString());
    }
    //获取格林时间
    public string Getdate(object Vdate)
    {
        DateTime Dtime = Convert.ToDateTime(Vdate);
        return Dtime.AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToShortDateString();
    }
    protected string GetModify(string dd, string id)//, string _pageUrl
    {

        if (dd.Length > 0)
        {
            string sql = dd.ToString() + "&qishu=" + id.ToString();
            return sql;

        }
        else
        {
            return GetTran("000221", "无");
        }
    }
    /// <summary>
    /// 导出EXCEL
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {

        
        Response.AppendHeader("Content-Disposition", "attachment;filename=FileName.xls");
        Response.ContentEncoding = Encoding.UTF8;

        string cmd = ViewState["sql"].ToString();
        DataTable dt = DAL.DBHelper.ExecuteDataTable(cmd);
        
        Excel.OutToExcel(dt, GetTran("000000", "申请充值FTC"), new string[] { "zt=" + GetTran("000000", "状态"), "Number=" + GetTran("000000", "编号"), "ipon=" + GetTran("000000", "手机号"), "XFTime=" + GetTran("000000", "申请时间"), "XFON=" + GetTran("000000", "兑换金额"), "XFEN=" + GetTran("000000", "兑换总额"), "ps=" + GetTran("000000", "备注") });
       
        
       
    }

    protected void lkSubmit_Click(object sender, EventArgs e)
    {
        //用IList存储选择了哪个复选框。
        IList<string> zcbmList = new List<string>();
        for (int i = 0; i <= gvdeduct.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)(gvdeduct.Rows[i].FindControl("CheckBox1"));
            if (cbox.Checked == true)
            {
                //用循环查出来哪个复选框选中，并把这个行的第二列的值存到IList中。我这里第二列是唯一标示列　　　　　　　　
                zcbmList.Add(gvdeduct.Rows[i].Cells[1].Text);

            }

        }
        if (zcbmList.Count == 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script >alert('请先选择需要处理的用户！')</script>");
            return;
        }
        string sql1 = "ID in(";
        foreach (var zcbm in zcbmList)
        {
            if (zcbmList.IndexOf(zcbm) < zcbmList.Count - 1)
            {
                sql1 += "'" + zcbm + "',";
            }
            else
            {
                sql1 += "'" + zcbm + "')";
            }

        }
        
        string sqll = "";
        if (plsh.SelectedValue == "0")
        {
            sqll = "update MemberCashXF set XFState=0,ps='"+beizhu.Text+"' where " + sql1 + "";
        }
        if (plsh.SelectedValue == "1")
        {
            sqll = "update MemberCashXF set XFState=1,ps='" + beizhu.Text + "' where " + sql1 + "";
        }
        if (plsh.SelectedValue == "2")
        {
            sqll = "update MemberCashXF set XFState=2,ps='" + beizhu.Text + "' where " + sql1 + "";
            string tk = "update memberinfo set pointBIn=pointBIn+XFON from memberinfo,MemberCashXF where memberinfo.number=MemberCashXF.number and MemberCashXF." + sql1 + "  and MemberCashXF.XFState not in(2,3)";
            DBHelper.ExecuteNonQuery(tk);
            foreach (var zcbm in zcbmList)
            {
                string sqq = "select * from MemberCashXF where ID=" + zcbm + " and XFState not in(2,3)";
                DataTable dt_one = DAL.DBHelper.ExecuteDataTable(sqq);
                double ipn = Convert.ToDouble(dt_one.Rows[0]["XFON"].ToString());
                D_AccountBLL.AddAccountWithdraw(zcbm, ipn, D_AccountSftype.MemberTypeBd, D_Sftype.baodanFTC, D_AccountKmtype.RechargeByOnline, DirectionEnum.AccountsIncreased, "同意申请充值FTC,充值到账:" + ipn + "");

            }
        }
        if (plsh.SelectedValue == "3")
        {
            sqll = "update MemberCashXF set XFState=3,ps='" + beizhu.Text + "' where " + sql1 + "";
            
           

        }

        int con= DBHelper.ExecuteNonQuery(sqll);
        if (con >= 1)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script >alert('处理成功！')</script>");
        }
        else {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script >alert('处理失败！')</script>");
        }

        BtnConfirm_Click(null, null);
        
    }
}