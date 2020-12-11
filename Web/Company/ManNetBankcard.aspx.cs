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
using DAL;
using Model;
using System.Collections.Generic;

public partial class Company_ManNetBankcard : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        //Permissions.CheckManagePermission(Model.Other.EnumCompanyPermission.ManNetBankcard);
        if (!IsPostBack)
        {
            bindlistdata();
            Bindsetlog();
            translation();
        }
    }

    private void translation()
    {
        TranControls(rbtltype, new string[][] { new string[] { "007777", "设置网络" }, new string[] { "007778", "设置个人" } });
        TranControls(btnsetcardnumber, new string[][] { new string[] { "005793", "设置" } });
        TranControls(btnsearch, new string[][] { new string[] { "000340", "查询" } });
        TranControls(gvsetbanklog, new string[][]{
            new string[]{"000024","会员编号"},
            new string[]{"000025","会员姓名"},
            new string[]{"007770","设置类型"},
            new string[]{"007790","设置账号"},
            new string[]{"007791","排除网络编号"},
            new string[]{"007272","操作人"},
            new string[]{"003259","操作时间"}
        });
    }

    public void Bindsetlog()
    {

        string condition = "     ml.number=mb.number  and  ml.bankid=cb.id ";
        if (this.txtnumber.Text != "")
        {
            condition += "  and ml.number like '%" + this.txtnumber.Text.Trim() + "%'";
        }
        //if (this.txtname.Text != "")
        //{
        //    condition += " and  mb.name like '%" + this.txtname.Text.Trim() + "%'";
        //}

        //condition += " and " + BLL.CommonClass.CommonDataBLL.GetComNumber("mb");


        this.Pager1.ControlName = "gvsetbanklog";
        this.Pager1.key = "ml.id ";
        this.Pager1.PageColumn = "  ml.id ,ml.number,ml.numberstr,  ml.modifytype ,ml. operatenum ,ml.modifytime , cb.bank,cb.bankbook,cb.bankname,mb.name";
        this.Pager1.Pageindex = 0;
        this.Pager1.PageTable = "ModifyBank_log ml, memberinfo mb,  companybank cb ";
        this.Pager1.Condition = condition;
        this.Pager1.PageSize = 10;
        this.Pager1.PageCount = 0;
        this.Pager1.OrderKey = "  ml.modifytime    ";
        this.Pager1.AscOrDesc = true;
        this.Pager1.PageBind();

    }

    /// <summary>
    /// 绑定下拉框银行列表
    /// </summary>
    public void bindlistdata()
    {
        List<CompanyBankModel> listbank = CompanyBankDAL.GetCompanyBanks();
        this.ddlbanklist.Items.Clear();
        this.ddlbanklist.Items.Add(new ListItem(GetTran("007792", "-------请--选--择-------"), "-1"));
        foreach (CompanyBankModel item in listbank)
        {
            string sstr = item.Bank.Substring(0, item.Bank.IndexOf("银行") + 2) + "--" + item.BankBook + "--" + item.Bankname;
            ListItem li = new ListItem(sstr, item.ID.ToString());
            this.ddlbanklist.Items.Add(li);
        }
        this.ddlbanklist.SelectedIndex = 0;
        this.ddlbanklist.DataBind();
    }

    protected void btnsetcardnumber_Click(object sender, EventArgs e)
    {
        //Permissions.CheckManagePermission(Model.Other.EnumCompanyPermission.ManNetBankcard_Add);
        if (this.txttopnumber.Text == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("007793", "请输入网络编号") + "！');</script>");
            return;
        }

        int count = Convert.ToInt32(DBHelper.ExecuteScalar("select count(0) from memberinfo where number='" + this.txttopnumber.Text.Trim() + "' "));
        if (count <= 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000710", "编号不存在") + "！');</script>");
            return;
        }
        //判断登录者是否有更改此会员的权限
        //bool isTrue = false;
        //isTrue = BLL.CommonClass.CommonDataBLL.GetRole(Session["Company"].ToString(), txttopnumber.Text.Trim());
        //if (!isTrue)
        //{
        //    ClientScript.RegisterStartupScript(GetType(), "success", "alert('" + GetTran("007794", "对不起，您没有更改此会员的权限") + "');", true);
        //    return;
        //}
        if (this.ddlbanklist.SelectedIndex == 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("007795", "请选择汇入银行卡号") + "！');</script>");
            return;
        }


        try
        {
            int rc = AddMemberInfomDAL.SetNetCardid(this.txttopnumber.Text.Trim(), Convert.ToInt32(this.ddlbanklist.SelectedValue), this.txtnosetnumber.Text.Trim(), Session["Company"].ToString(), Convert.ToInt32(this.rbtltype.SelectedValue));
            if (rc > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("005820", "设置成功！") + "');  window.location.href='ManNetBankcard.aspx';</script>");

                return;
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("006922", "设置失败") + "'); window.location.href='ManNetBankcard.aspx';</script>");

                return;
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("006922", "设置失败") + "');  window.location.href='ManNetBankcard.aspx';</script>");

            return;

        }

    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        Bindsetlog();
    }

    protected string getName(object obj)
    {
        return Encryption.Encryption.GetDecipherName(obj.ToString());

    }
    protected void gvsetbanklog_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;

            Label lbltype = (Label)e.Row.FindControl("lbltype");
            Label lblbankinfo = (Label)e.Row.FindControl("lblbankinfo");
            Label lblnosetnumbers = (Label)e.Row.FindControl("lblnosetnumbers");

            int type = Convert.ToInt32(drv["modifytype"].ToString());
            string bank = drv["bank"].ToString();
            string card = drv["bankbook"].ToString();
            string cardname = drv["bankname"].ToString();
            string acount = ""; ;
            if (bank.Length > 4)
                acount += bank.Substring(0, 4) + "  " + cardname + "  " + card.Substring(card.Length - 4);
            else
                acount += bank + "  " + cardname + "  " + card;
            lblbankinfo.Text = acount;
            if (type == 1)
            {
                lbltype.Text = GetTran("007778", "设置个人");
            }
            else
            {
                lbltype.Text = GetTran("007777", "设置网络");
            }

            string nstr = drv["numberstr"].ToString();
            if (nstr == "")
            {
                lblnosetnumbers.Text = GetTran("007796", "无排除编号");
            }
            else
            {
                lblnosetnumbers.Text = nstr.Length > 20 ? nstr.Substring(0, 20) + "..." : nstr;
            }
        }
    }
}