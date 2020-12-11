using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using BLL.other.Company;
using Model.Other;
using BLL.CommonClass;
using DAL;
using Standard.Classes;

public partial class Company_MessageClassSetting : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.ManageMessageSend);

        if (!IsPostBack)
        {
            this.BindGrvClass();
            this.BindGrvClassAdmin();
            this.BindDropClass();
            this.BindDropAdmin();
        }
        Translations_More();
    }


    protected void Translations_More()
    {
        TranControls(givShowMessage, new string[][] 
                        {
                             new string[] { "000015","操作"}, 
                            new string[] { "007882","类别编号"}, 
                            new string[] { "007883","类别名称"}, 
                            new string[] { "000151","管理员"}
                        }
                    );
        TranControls(GridView1, new string[][]{
            new string[]{"007882","类别编号"},
            new string[]{"007883","类别名称"},
            new string[]{"000015","操作"}
         });
        TranControls(Button3, new string[][] { new string[] { "000421", "返回" } });
        TranControls(Button2, new string[][] { new string[] { "005901", "保存" } });
       
        TranControls(this.BtnAddClass, new string[][] 
                        {
                            new string[] { "006639","添 加"}                             
                        }
          );
    }

    private void BindDropClass()
    {
        this.DropClass.Items.Clear();
        SqlDataReader dr = DBHelper.ExecuteReader(" select ID,ClassName From MsgClass");
        while (dr.Read())
        {
            ListItem list = new ListItem(dr["ClassName"].ToString(), dr["id"].ToString());

            this.DropClass.Items.Add(list);
        }
        dr.Close();
    }

    private void BindDropAdmin()
    {
        DataTable dt = ManagerBLL.GetAllManageNumber();
        foreach (DataRow item in dt.Rows)
        {
            ListItem list = new ListItem(item["Number"].ToString(), item["Number"].ToString());
            this.DropAdmin.Items.Add(list);
        }
       
    }
    public string GetBiaoZhunTime(string dt)
    {
        return Convert.ToDateTime(dt).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToShortDateString();
    }
    private void BindGrvClassAdmin()
    {

        Pager page = Page.FindControl("Pager1") as Pager;
        page.PageBind(0, 10, "MsgClassAdmin a left join MsgClass b on a.ClassID=b.ID", "a.ID,a.ClassID,a.Admin,b.ClassName", "", "ID", "givShowMessage");
        Translations_More();
    }
    
    private void BindGrvClass()
    {
        this.GridView1.DataSource=DAL.DBHelper.ExecuteDataTable("select ID,ClassName from MsgClass order by ID");
        this.GridView1.DataBind();
    }
    protected void givShowMessage_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string name = e.CommandName;
        int id = Convert.ToInt32(e.CommandArgument);
        if ("Del"==name)
        {
            //MessageSendBLL message = new MessageSendBLL();
            int rtn = DAL.DBHelper.ExecuteNonQuery("Delete from MsgClassAdmin where ID=@id", new SqlParameter("@id", id), CommandType.Text);
            if (rtn>0)
            {
                ClientScript.RegisterStartupScript(this.GetType(),"","<script>alert('"+GetTran("000008", "删除成功") + "！')</script>");
            }
            else
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000009", "删除失败") + "！')</script>");
        }
        this.BindGrvClassAdmin();
    }
    


    protected void givShowMessage_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");



            int Delete = 0;
            Delete = (int)Permissions.GetPermissions(Model.Other.EnumCompanyPermission.ManageMessageSendDelete);
            if (Delete == 0)
            {
                ((LinkButton)e.Row.FindControl("Button1")).Visible = false;
            }
            else
            {
                ((LinkButton)e.Row.FindControl("Button1")).Visible = true;
            }
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Translations_More();
        }
    }
    protected void BtnAddClass_Click(object sender, EventArgs e)
    {
        if (this.TxtClassName.Text.Trim().Equals(""))
        {
            ScriptHelper.SetAlert(BtnAddClass, GetTran("007885", "请输入邮件类别名称") + "！！！");
            return;
        }            
        int cnt=Convert.ToInt16(DAL.DBHelper.ExecuteScalar("select ID from MsgClass where ClassName=@classname",new SqlParameter("@classname",this.TxtClassName.Text.Trim()),CommandType.Text));
        if (cnt>0)
        {
            ScriptHelper.SetAlert(BtnAddClass, GetTran("007886", "此邮件类别已存在") + "！！！");
            return;
        }
        int rtn = DAL.DBHelper.ExecuteNonQuery("insert into MsgClass(ClassName) values(@classname)", new SqlParameter("@classname", this.TxtClassName.Text.Trim()), CommandType.Text);
        if (rtn > 0)
        {
            ScriptHelper.SetAlert(BtnAddClass, GetTran("007887", "添加新邮件类别成功") + "！！！");
            this.BindGrvClass();
            this.BindDropClass();
            this.TxtClassName.Text = "";
        }
        else
        {
            ScriptHelper.SetAlert(BtnAddClass, GetTran("000007", "添加失败") + "！！！");

        }

    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        this.GridView1.EditIndex = -1;
        this.BindGrvClass();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
         this.GridView1.EditIndex = e.NewEditIndex;
         this.BindGrvClass();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        if (e.RowIndex >= this.GridView1.Rows.Count)
        {
            return;
        }
        string id = this.GridView1.DataKeys[e.RowIndex][0].ToString();
        int rtn = DAL.DBHelper.ExecuteNonQuery("Delete from MsgClass where ID=@id", new SqlParameter("@id", id), CommandType.Text);
        if (rtn > 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000008", "删除成功") + "！')</script>");
            this.BindGrvClass();
            this.BindGrvClassAdmin();
            this.BindDropClass();
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000009", "删除操作失败") + "！')</script>");

        }


    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow )
        {
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                ((LinkButton)e.Row.FindControl("lbDelete")).Attributes.Add("onclick", "javascript:return confirm('" + GetTran("kwl", "与此类别相关的邮件和权限将会一并删除，是否确定删除？") + "');");
        }
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string id = this.GridView1.DataKeys[e.RowIndex][0].ToString();
                //Dim mpn As String = CType(Me.GridView1.Rows(e.RowIndex).FindControl("TxtMPN_GV"), TextBox).Text
        string classname=((TextBox)this.GridView1.Rows[e.RowIndex].FindControl("TxtClassName_GV")).Text;
        int rtn = DAL.DBHelper.ExecuteNonQuery("update MsgClass set ClassName=@classname where ID=@id", new SqlParameter[]{new SqlParameter("@id", id),new SqlParameter("@classname",classname)}, CommandType.Text);
        if (rtn > 0)
        {
            this.GridView1.EditIndex = -1;
            this.BindGrvClass();
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("007888", "更新操作失败") + "！')</script>");

        }

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        int cnt = Convert.ToInt16(DAL.DBHelper.ExecuteScalar("select ID from MsgClassAdmin where ClassID=@classid and Admin=@admin", new SqlParameter[]{new SqlParameter("@classid", this.DropClass.SelectedValue),new SqlParameter("@admin",this.DropAdmin.SelectedValue)}, CommandType.Text));
        if (cnt > 0)
        {
            ScriptHelper.SetAlert(BtnAddClass, GetTran("007889", "此绑定已存在") + "！！！");
            return;
        }

         int rtn = DAL.DBHelper.ExecuteNonQuery("insert into MsgClassAdmin(ClassID,Admin) values(@classid,@admin)", new SqlParameter[]{new SqlParameter("@classid", this.DropClass.SelectedValue),new SqlParameter("@admin",this.DropAdmin.SelectedValue)}, CommandType.Text);
        if (rtn > 0)
        {
            this.GridView1.EditIndex = -1;
            this.BindGrvClassAdmin();
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("007890", "更新操作失败") + "！')</script>");

        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("ManageMessage_Recive.aspx");

    }
}
