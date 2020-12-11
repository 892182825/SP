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

public partial class Company_ManagModify : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl); //权限
      //  Permissions.CheckManagePermission(Model.Other.EnumCompanyPermission.ManagModify);
        if (!IsPostBack)
        {
            Binddatalist();
            translation();
        }
    }

    private void translation() {
        TranControls(btnaddnew, new string[][]{
            new string[]{"007903","添加新操作员"}
        });
        TranControls(gvcontrolors, new string[][]{
            new string[]{"007904","登录名"},
            new string[]{"000014","添加时间"},
            new string[]{"007672","最后登录时间"},
            new string[]{"007905","最后登录ip"},
            new string[]{"000015","操作"}
        });
        TranControls(btnadd, new string[][]{
            new string[]{"007909","添加操作员"}
        });
        TranControls(btnmodify, new string[][] { new string[] { "007914", "修改操作员" } });
    }

    public void Binddatalist()
    {
        string sqlstr = "select * from orgman  where  deletestate =0";

        DataTable dt = DAL.DBHelper.ExecuteDataTable(sqlstr);
        this.gvcontrolors.DataSource = dt;
        this.gvcontrolors.DataBind();
    }
    protected void gvcontrolors_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;

            LinkButton lkbtnsetnouse = (LinkButton)e.Row.FindControl("lkbtnsetnouse");

            int isuse = Convert.ToInt32(drv["isnouse"]);

           if (isuse == 0)
           {
               lkbtnsetnouse.Text = GetTran("007906", "禁用");
               lkbtnsetnouse.OnClientClick = "  return  confirm('" + GetTran("007916", "确定要禁用该操作员吗？") + "');  ";
           }
           else if (isuse == 1)
           {
               lkbtnsetnouse.Text = GetTran("007851","启用");
               lkbtnsetnouse.OnClientClick = "  return  confirm('" + GetTran("007917", "确定要启用该操作员吗？") + "');  ";
            }
        }
    }

    ///
    protected void gvcontrolors_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string  uid =  e.CommandArgument.ToString();
        int res=0;
        if (e.CommandName == "mdf") { 
            //Permissions.CheckManagePermission(Model.Other.EnumCompanyPermission.ManagModify_Modify);
            int id = Convert.ToInt32(uid);
            loaddate(id); }
        if (e.CommandName == "use")
        {
          
            //Permissions.CheckManagePermission(Model.Other.EnumCompanyPermission.ManagModify_Stop); 
            res = RemittancesDAL.doOrgman(uid, "", "", 4);
        if (res == 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("001401", "操作成功！") + "')</script>");
            Binddatalist();
            return;
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("001541", "操作失败！") + "')</script>");
            return;
        }

        }
        if (e.CommandName == "del")
        {
            //Permissions.CheckManagePermission(Model.Other.EnumCompanyPermission.ManagModify_Del); 
            res = RemittancesDAL.doOrgman(uid, "", "", 3);
            if (res == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000749", "删除成功！") + "')</script>");
                
               
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000417", "删除失败！") + "')</script>");
                
            }
            backdata();
            Binddatalist(); translation(); return;
        }
      

    }
    /// <summary>
    /// 加载名称
    /// </summary>
    /// <param name="id"></param>
    public void loaddate( int id ) 
    {
       
        string sqlstr = "select username from  orgman where id="+id;
        string name = DAL.DBHelper.ExecuteScalar(sqlstr).ToString();
        this.txtusername.Text = name;
        lblinfo.Text = GetTran("007914", "修改操作员") + name;
        this.hdfid.Value = id.ToString();
        this.panaddmodify.Visible = true;
        this.btnmodify.Visible = true;
    }
    /// <summary>
    /// 添加操作员
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnaddnew_Click(object sender, EventArgs e)
    {
        //Permissions.CheckManagePermission(Model.Other.EnumCompanyPermission.ManagModify_Add);
        this.panaddmodify.Visible = true;
        this.btnadd.Visible = true;
    }
    /// <summary>
    /// 执行添加操作员
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnadd_Click(object sender, EventArgs e)
    {
        if (!Getvalidate()) return;
        if (getishave(this.txtusername.Text.Trim()))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("007915", "操作员登录名重复") + "！')</script>");
            return  ;

        }
        string pass = Encryption.Encryption.GetEncryptionPwd(this.txtpass.Text.Trim(), this.txtusername.Text.Trim()); //加密密码
      int  res=  RemittancesDAL.doOrgman(this.txtusername.Text, pass, "", 1);
      if (res == 0)
      {
          ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000891", "添加成功!") + "')</script>");
        
       
      }
      else {
          ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000007", "添加失败") + "!')</script>");
        
      }
      backdata();
      Binddatalist();   return;

    }
    public void backdata() {
        this.panaddmodify.Visible = false;
        this.txtpass.Text = string.Empty;
        this.txtusername.Text = string.Empty;
        this.hdfid.Value = "0";
    }

    /// <summary>
    /// 验证非空
    /// </summary>
    public bool Getvalidate() {
        if (this.txtusername.Text == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("007918", "操作员登录名不能为空") + "!')</script>");
            return false;
        }

        if (this.txtpass.Text == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("007919", "操作员密码不能为空") + "！')</script>");
            return false;
        }
    
        return true;
    }

    public bool getishave(string uname ) 
    {
        string sqlstring = "select count(0) from orgman where username ='" + uname + "'  and  deletestate=0 ";
        int res = Convert.ToInt32( DAL.DBHelper.ExecuteScalar(sqlstring));
        return res > 0;
    }
    /// <summary>
    /// 修改操作员密码或名称
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnmodify_Click(object sender, EventArgs e)
    {
        if (!Getvalidate()) return; 
        string pass = Encryption.Encryption.GetEncryptionPwd(this.txtpass.Text.Trim(), this.txtusername.Text.Trim()); //加密密码
       string id=this.hdfid.Value.ToString();
       if (id != "0")
       {
           string sql = "update orgman set username='" + this.txtusername.Text.Trim() + "' ,  userpass='" + pass + "' where id=" + id;
           int res =Convert.ToInt32( DAL.DBHelper.ExecuteNonQuery(sql));
           if (res > 0)
           {
               ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000222", "修改成功！") + "')</script>");
             
           }
           else
           {
               ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000225", "修改失败！") + "')</script>");
               
           }  Binddatalist();
               backdata();
                
               return;
       }
    }
}
