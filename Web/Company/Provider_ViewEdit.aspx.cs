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
using System.Text;
using Model.Other;
using BLL.CommonClass;

public partial class Company_Provider_ViewEdit : BLL.TranslationBase
{
    protected string msg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Permissions.CheckManagePermission(EnumCompanyPermission.CustomerProviderViewEdit);
        if (!IsPostBack)
        {
            Pager1.PageBind(0, 10, "ProviderInfo", " ID,number,name,ForShort,linkman,Mobile,address,DutyNumber,remark "," 1=1 ", "ID", "givProviderInfo");
        }
        Translations_More();
    }

    /// <summary>
    /// 翻译
    /// </summary>
    protected void Translations_More()
    {
        TranControls(btnSearch, new string[][] { new string[] { "000440", "查 看" } });
        TranControls(givProviderInfo,new string[][]
                        {
                            new string[]{"000015", "操作"},
                            new string[]{"000927", "供应商编号"},
                            new string[]{"000931", "供应商名称"},
                            new string[]{"000958", "供应商简称"},
                            new string[]{"000960", "联系人"},
                            new string[]{"000052", "手机"},
                            new string[]{"000072", "地址"},
                            new string[]{"000962", "税号"},
                            new string[]{"000078", "备注"}
                        }
                    );
    }

    /// <summary>
    /// 模糊查询供货商信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string name = txtName.Text.Trim().Replace("'",""); ;
        string number = txtNumber.Text.Trim().Replace("'","");

        StringBuilder sb = new StringBuilder();
        sb.Append(" 1=1 ");
        if (name.Length > 0)
        {
            sb.Append("  and  Name like '%" + name + "%'");
        }
        if (number.Length > 0)
        {
            sb.Append(" and Number like '%" + number + "%'");
        }
        ViewState["wherepage"] = sb.ToString();
        Pager1.PageBind(0, 10, "ProviderInfo", " ID,number,name,ForShort,linkman,Mobile,address,DutyNumber,remark ", sb.ToString(), "ID", "givProviderInfo");
        Translations_More();
    }
    /// <summary>
    /// 行触发命令
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void givProviderInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string name = e.CommandName;
        int id = Convert.ToInt32(e.CommandArgument);

        int isExistCount = ProviderManageBLL.ProviderIdIsExist(id);
        if (isExistCount > 0)
        {
            if (name == "Del")
            {
                Permissions.CheckManagePermission(EnumCompanyPermission.CustomerProviderViewEditDelete);

                BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("ProviderInfo", "(ltrim(rtrim(id)))");
                cl_h_info.AddRecord(Convert.ToInt32(id));
                int getCount = ProviderManageBLL.ProviderIdWhetherHasOperation(id);
                if (getCount > 0)
                {
                    msg = Transforms.ReturnAlert(GetTran("001356", "对不起，该供应商已经发生了业务，因此不能删除！"));
                    return;
                }

                else
                {
                    int delCount = ProviderManageBLL.Delprivider(id);
                    if (delCount > 0)
                    {
                        cl_h_info.AddRecord(Convert.ToInt32(id));//不能放到事务中  修改数据后
                        cl_h_info.DeletedIntoLogs(BLL.CommonClass.ChangeCategory.company6, Session["Company"].ToString(), BLL.CommonClass.ENUM_USERTYPE.objecttype4);//不能放到事务中

                        msg = Transforms.ReturnAlert(GetTran("000749", "删除成功！"));
                    }

                    else
                    {
                        msg = Transforms.ReturnAlert(GetTran("001387", "删除失败，请联系管理员！"));
                        return;
                    }
                }            
            }

            else if (name == "Edit")
            {
                Response.Redirect("AddProvider.aspx?id=" + id);
            }
        }
        
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001146", "对不起，该供应商不存在或者已经被删除！")));            
        }
        btnSearch_Click(null, null);
    }

    protected void btndropexcil_Click(object sender, EventArgs e)
    {
        Excel.OutToExcel
            (ProviderManageBLL.GetProviderInfoToExcel(), 
                GetTran("001399", "供应商信息"),
                new string[]
                {
                    "Number=" + GetTran("000927", "供应商编号"), 
                    "Name="+GetTran("000931","供应商名称"), 
                    "ForShort="+GetTran("000958","供应商简称"), 
                    "Address="+GetTran("001232","供应商地址"), 
                    "LinkMan="+GetTran("000960","联系人"), 
                    "Mobile="+GetTran("000052","手机"),
                    "DutyNumber="+GetTran("000962","税号"),
                    "Remark="+GetTran("000078","备注")
                }
            );
    }

    /// <summary>
    /// GridView Style
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void givProviderInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
        }
    }
}