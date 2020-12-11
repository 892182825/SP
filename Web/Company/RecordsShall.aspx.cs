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
using System.Text;
using BLL.MoneyFlows;
using Model;
using BLL.CommonClass;

public partial class Company_RecordsShall : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        if (!IsPostBack)
        {
            bind();
        }
        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.DropDownList1,
               new string[][]{
                    new string []{"005959","未添加"},
                    new string []{"001223","已添加"}});
        this.TranControls(this.GridView1,
                new string[][]{
                    new string []{"000015","操作"},
                    new string []{"000024","会员编号"},
                    new string []{"000025","会员姓名"},
                    new string []{"005947","原总计"},
                    new string []{"005948","原扣税"},
                    new string []{"005949","原扣款"},
                    new string []{"005950","原补款"},

                    new string []{"000562","原实发"},
                    new string []{"005951","本次结算总计"},
                    new string []{"005952","本次结算扣税"},
                    new string []{"005953","本次结算扣款"},


                    new string []{"005954","本次结算补款"},
                    new string []{"005955","本次结算的实发"},
                    new string []{"005956","两次结算的差异额"},
                    new string []{"000045","期数"},

                    new string []{"005957","差异额是否添加"},
                    new string []{"005958","本次结算时间"}
                });
    }
    public void bind()
    {
        string table = "BonusDifference left join memberinfo on(memberinfo.number=BonusDifference.number)";
        string colums = "BonusDifference.*,memberinfo.Name";
        string key = "id";
        string condition = "cyflag="+this.DropDownList1.SelectedValue;
        if (this.TextBox1.Text != "")
            condition += " and BonusDifference.Number='" + this.TextBox1.Text + "'";
        this.Pager1.PageBind(0, 10, table, colums, condition, key, "GridView1");
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
            if (e.Row.Cells[15].Text == "1")
            {
                (e.Row.FindControl("LinkButton1") as LinkButton).Visible = false;
                (e.Row.FindControl("LinkBtnDelete") as LinkButton).Visible = false;
            }
            e.Row.Cells[2].Text = Encryption.Encryption.GetDecipherName(e.Row.Cells[2].Text);
            switch (e.Row.Cells[15].Text)
            {
                case "1": e.Row.Cells[15].Text = GetTran("000233", "是"); break;
                case "0": e.Row.Cells[15].Text = GetTran("000235", "否"); break;
            }
            try
            {
                e.Row.Cells[16].Text = DateTime.Parse(e.Row.Cells[16].Text).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToShortDateString();
            }
            catch
            {
            }
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string cname=e.CommandName.ToString();
        if (cname == "Lbtn")
        {
            if (DeductBLL.isExistsBonusDifference(Convert.ToInt32(e.CommandArgument)))
            {
                this.Page.RegisterStartupScript("", "<script>alert('" + GetTran("005965", "已经添加，不能再添加！") + "');</script>");
                return;
            }
            else
            {
                
                DeductBLL.upBonusDifference(Convert.ToInt32(e.CommandArgument));
                BonusDifferenceModel model=DeductBLL.GetBonusDifference(Convert.ToInt32(e.CommandArgument));
                MemberInfoModel memberinfo=DeductBLL.GetMemberInfo(model.Number);
                DeductModel deduct = new DeductModel();
                deduct.Number = model.Number;
                deduct.DeductMoney = model.Chayi;
                    if (model.Chayi >= 0)
                    {
                        deduct.DeductReason = GetTran("000024", "会员编号") + "：" + model.Number + "," + GetTran("000107", "姓名") + "：" + Encryption.Encryption.GetDecipherName(memberinfo.Name) + "," + GetTran("000252", "补款") + "：" + model.Chayi + ",第" + model.Qishu + GetTran("006033", "期结算后的差异");
                        deduct.IsDeduct = 1;
                    }
                    else
                    {
                        deduct.DeductReason = GetTran("000024", "会员编号") + "：" + model.Number + "," + GetTran("000107", "姓名") + "：" + Encryption.Encryption.GetDecipherName(memberinfo.Name) + "," + GetTran("000251", "扣款") + "：" + model.Chayi + "," + GetTran("000156", "第") + model.Qishu + GetTran("006033", "期结算后的差异");
                        deduct.IsDeduct = 0;
                    }
                    deduct.ExpectNum = BLL.CommonClass.CommonDataBLL.getMaxqishu();


                    deduct.OperateIP = CommonDataBLL.OperateIP;
                    deduct.OperateNum = CommonDataBLL.OperateBh;
                    DeductBLL.AddInfo(deduct);
                    this.Page.RegisterStartupScript("", "<script>alert('" + GetTran("001401", "操作成功！") + "');</script>");
                    bind();
            }
        }
        if (cname == "Del")
        {
            if (DeductBLL.isExistsBonusDifference(Convert.ToInt32(e.CommandArgument)))
            {
                this.Page.RegisterStartupScript("", "<script>alert('" + GetTran("005966", "已经添加，不能删除！") + "');</script>");
                return;
            }
            else
            {
                if (DeductBLL.isDelBonusDifference(Convert.ToInt32(e.CommandArgument)))
                {
                    DeductBLL.DelBonusDifference(Convert.ToInt32(e.CommandArgument));
                    this.Page.RegisterStartupScript("", "<script>alert('" + GetTran("000749", "删除成功！") + "');</script>");
                }
                else
                {
                    this.Page.RegisterStartupScript("", "<script>alert('" + GetTran("005997", "已经被删除，不能再删！") + "');</script>");
                }
                bind();
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        bind();
    }
    protected void lkSubmit_Click(object sender, EventArgs e)
    {
        Button1_Click(null,null);
    }
}
