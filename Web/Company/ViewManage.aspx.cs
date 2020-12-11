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
using BLL.other.Company;
using Model;


public partial class Company_ViewManage : System.Web.UI.Page
{
    public string msg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        if (!this.IsPostBack)
        {
            PageSet();
        }
    }

    protected void PageSet()
    {
        string manageID = Request.QueryString["manageID"].ToString();
        int ids = DeptRoleBLL.GetViewManage(manageID);
        if (ids == 0)
        {
            this.GridView1.DataSource = null;
            this.GridView1.DataBind();
            this.Pager1.Visible = false;
            return;
        }
        this.Pager1.PageBind(0, 10, " ViewManage ", " id,manageid,number,type ", " manageID='"+manageID+"' ", " id ", "GridView1");
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            int id = Convert.ToInt32(e.CommandArgument);
            string managerID = Request.QueryString["manageID"].ToString();
            BLL.CommonClass.ChangeLogs cl = new BLL.CommonClass.ChangeLogs("viewmanage", "id");
            cl.AddRecord(id);
            if (ManagerBLL.DelViewManage(id) > 0)
            {
                cl.DeletedIntoLogs(BLL.CommonClass.ChangeCategory.company25, "管理员:" + managerID, BLL.CommonClass.ENUM_USERTYPE.objecttype7);
                msg = "<script>alert('删除成功！');</script>";
            }
            PageSet();
        }
        
    }

    protected string GetNetType(string type)
    {
        string strType = "";
        switch (type)
        {
            case "1":
                strType = "安置网络";
                break;
            case "0":
                strType = "推荐网络";
                break;
            default:
                strType = "安置网络";
                break;
        }
        return strType;
    }

    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        string manageID = Request.QueryString["manageID"].ToString();
        if (this.txtNumber.Text.Trim() == "")
        {
            msg = "<script>alert('请输入团队顶点编号！');</script>";
            return;
        }

        if (BLL.CommonClass.CommonDataBLL.getCountNumber(this.txtNumber.Text.Trim()) == 0)
        {
            msg = "<script>alert('该团队顶点编号不存在！');</script>";
            return;
        }

        if (BLL.CommonClass.CommonDataBLL.getNumberRole(this.txtNumber.Text.Trim(), manageID,Convert.ToInt32(this.rbtType.SelectedValue.Trim().ToString())) > 0)
        {
            msg = "<script>alert('该管理员已有此网络的查看权限，不能重复添加！');</script>";
            return;
        }

        
        if (BLL.CommonClass.CommonDataBLL.AddViewNumber(this.txtNumber.Text.Trim(), manageID,Convert.ToInt32(this.rbtType.SelectedValue.Trim().ToString())) == 0)
        {
            msg = "<script>alert('添加失败！');</script>";
            return;
        }
        else 
        {
            msg = "<script>alert('添加成功！');</script>";
        }
        PageSet();
        BtnClear_Click(null, null);
    }
    protected void Btn1_Click(object sender, EventArgs e)
    {
        this.Div1.Visible = true;
        this.Btn1.Visible = false;
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        this.txtNumber.Text = "";
        this.Div1.Visible = false;
        this.Btn1.Visible = true;
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC';");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg;");
        }
    }
    protected void BtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ManagerManage.aspx");
    }
}
