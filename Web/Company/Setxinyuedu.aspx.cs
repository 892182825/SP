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

public partial class Setxinyuedu : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
      //  Permissions.CheckManagePermission(Model.Other.EnumCompanyPermission.Setxinyuedu);
        if (!IsPostBack)
        {
            bindListLog();
        }
        translation();
    }

    private void translation() {
        TranControls(btnsearch, new string[][]{
            new string[]{"000340","查询"}
        });
        TranControls(btnsetcardnumber, new string[][] { new string[]{"005793", "设置"} });
        TranControls(gvclog, new string[][]{
            new string[]{"000024","会员编号"},
            new string[]{"000107","姓名"},
            new string[]{"007272","操作人"},
            new string[]{"007843","有效期至"},
            new string[]{"007844","设置额度"}
        });
    }

    public void bindListLog()
    {
       


        string condition = "     cl.number=mb.number  ";
        if (this.txtnumber.Text != "")
        {
            condition += "  and cl.number like '%"+this.txtnumber.Text.Trim()+"%'";
        }
        if (this.txtname.Text != "")
        {
            condition += " and  mb.name like '%"+this.txtname.Text.Trim()+"%'";
        }
        condition += " and " + BLL.CommonClass.CommonDataBLL.GetComNumber("mb");

       

        this.Pager1.ControlName = "gvclog";
        this.Pager1.key = "cl.id";
        this.Pager1.PageColumn = " cl.id, cl.number,cl.douser,cl.CreditLimit, cl.querydate,mb.name ,cl.CreditLimitdate";
        this.Pager1.Pageindex = 0;
        this.Pager1.PageTable = " CreditLimitLog cl,memberinfo mb ";
        this.Pager1.Condition = condition;
        this.Pager1.PageSize = 10;
        this.Pager1.PageCount = 0;
        this.Pager1.OrderKey = "  cl.querydate  ";
        this.Pager1.AscOrDesc = true; 
        this.Pager1.PageBind();

      
    }


    protected void btnsetcardnumber_Click(object sender, EventArgs e)
    {
      //  Permissions.CheckManagePermission(Model.Other.EnumCompanyPermission.Setxinyuedu_Add);       
        if (this.txttopnumber.Text == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000787", "请输入编号!") + "');</script>");
            return;
         }
       
        int count=Convert.ToInt32( DBHelper.ExecuteScalar("select count(0) from memberinfo where number='"+this.txttopnumber.Text.Trim()+"' "));
        if (count <= 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("001549", "对不起,该编号不存在！") + "');</script>");
            return;
        }
        //判断登录者是否有更改此会员的权限
        //bool isTrue = false;
        //isTrue = BLL.CommonClass.CommonDataBLL.GetRole(" number = '"+Session["Company"].ToString()+"'", txttopnumber.Text.Trim());
        //if (!isTrue)
        //{
        //    ClientScript.RegisterStartupScript(GetType(), "success", "alert('" + GetTran("007131", "对不起，您没有更改此会员的权限！") + "');", true);
        //    return;
        //}
        if (txtxinyue.Text == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("007846", "请输入您要设置的信誉额") + "！');</script>");
            return;
        }
        if (txtlimitdate.Text == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("007847", "请选择您要设置的信誉额有效期") + "！');</script>");
            return;
        }


        try
        {

            int rc = AddMemberInfomDAL.Setxinyuedu(this.txttopnumber.Text.Trim(), Convert.ToDouble(txtxinyue.Text.Trim()), Session["company"].ToString(), Request.UserHostAddress.ToString(), Convert.ToDateTime(txtlimitdate.Text.Trim()));
            
            if (rc > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("005820", "设置成功！") + "');  window.location.href='Setxinyuedu.aspx';</script>");
      
                return;
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("006922", "设置失败！") + "'); window.location.href='Setxinyuedu.aspx';</script>");
            
                return;
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("006922", "设置失败！") + "');  window.location.href='Setxinyuedu.aspx';</script>");
           
                return;
           
        }

      
    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        bindListLog();
    }
}
