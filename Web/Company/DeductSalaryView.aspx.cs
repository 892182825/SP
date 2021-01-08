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

using BLL.MoneyFlows;
using Model;
using Model.Other;
using BLL.CommonClass;
using Encryption;
using DAL;

public partial class Company_DeductSalaryView : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        Permissions.CheckManagePermission(EnumCompanyPermission.FinanceKoukuanAddbu);
        Response.Cache.SetExpires(DateTime.Now);
        Translations_More();
    }


    protected void Translations_More()
    {
        TranControls(Button1, new string[][] 
            {
                new string[] { "000321","提 交"}
            }
        );
        TranControls(Button3, new string[][] 
            {
                new string[] { "000096","返 回"}
            }
        );
        TranControls(rad_Deduct, new string[][] { new string[] { "000251", "扣款" }, new string[] { "000252", "补款" } });
    }


    /// <summary>
    /// 提交
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        this.question.Text = question.Text.Trim();
        this.money.Text = money.Text.Trim();

        //设置特定值防止重复提交
        hid_fangzhi.Value = "0";

        if (this.rad_Deduct.SelectedValue == "") {
            ScriptHelper.SetAlert(Page, GetTran("007748", "请选择补扣款项"));
            return;
        }
        string number = "";
        string sql = "select number from MemberInfo where MobileTele='" + txtbh.Text + "'";
        DataTable shj = DBHelper.ExecuteDataTable(sql);
        if (shj.Rows.Count > 0)
        { 
        number = shj.Rows[0][0].ToString();
        }
        else
        {
        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("002096", "无此编号，请检查后再重新输入") + "！')</script>");
                return;
        }
        

        if (number != "" && this.question.Text != "" && this.money.Text != "")
        {
            string vquestion;
            
            //判断会员是否存在
            if (DeductBLL.IsExist(number))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("002096", "无此编号，请检查后再重新输入") + "！')</script>");
                return;
            }
            double d;
            if (!double.TryParse(money.Text, out d))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("002095", "金额输入有误") + "！')</script>");
                return;
            }
            if (d <= 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script >alert('" + GetTran("001539", "操作失败，输入的金额不能小于0") + "！')</script>");
                return;
            }
            else
            {
                int iskou = int.Parse(rad_Deduct.SelectedValue); //0为扣，1为补
                
                vquestion = this.question.Text.Trim();
                DeductModel model = new DeductModel();
                model.Number = DisposeString.DisString(number.Trim(), "<,>,',-", "&lt;,&gt;,&#39;,&nbsp;", ",");
                model.DeductMoney = d;
                model.DeductReason = vquestion;
                model.ExpectNum = CommonDataBLL.getMaxqishu();
                model.IsDeduct = iskou;
                model.Actype = 1;
                model.OperateIP = CommonDataBLL.OperateIP;
                model.OperateNum = CommonDataBLL.OperateBh;
                try
                {
                    if (DeductBLL.AddDeduct(model))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script >alert('" + GetTran("000006", "添加成功") + "！');window.location.href='DeductSalary.aspx';</script>");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script >alert('" + GetTran("000007", "添加失败") + "！')</script>");
                    }
                }
                catch (Exception)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script >alert('" + GetTran("001507", "操作失败") + "！')</script>");
                }
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script >alert('" + GetTran("002094", "所有项必填，请重新输入") + "!')</script>");
        }
        clear();
    }
    public void clear()
    {
        this.txtbh.Text = string.Empty;
        this.question.Text = string.Empty;
        this.money.Text = string.Empty;
        this.rad_Deduct.SelectedValue = "0";
    }
    /// <summary>
    /// 返回
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("DeductSalary.aspx");
    }
}

