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
using System.Data.SqlClient;
using BLL.other;

public partial class Company_LeftMenu_W : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        if (!IsPostBack)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
            //this.ltlId.Text = "<font color=''>" + GetCompanyId() + "</font>";
            //this.ltlNme.Text = "<font color=''>" + MenuBLL.GetManageName(GetCompanyId()) + "</font>";
            //if (Convert.ToString(Session["UserLastLoginDate"]) != "" || Session["UserLastLoginDate"] != null)
            //{
            //    this.ltlTime.Text = "<font color=''>" + Convert.ToDateTime(Session["UserLastLoginDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "</font>";
            //}
            //else
            //{
            //    this.ltlTime.Text = "<font color=''>" + MenuBLL.GetLoginTime(GetCompanyId(), 1).ToString("yyyy-MM-dd HH:mm:ss") + "</font>";
            //}
        }
    }

    //private string GetCompanyId()
    //{
    //    if (Session["Company"] != null)
    //    {
    //        return Session["Company"].ToString();
    //    }
    //    else
    //    {
    //        return "";
    //    }
    //}

    public string GetMenu()
    {

        string html = @"  <ul>";
             
    



        string sql = @"select t.L001,m.MenuFile,m.id  from  menuLeft m left join  T_translation t on m.ID=t.primarykey  where t.tableName='menuleft'  and ParentID in (1,4,6)  and  isfold=1  order by m.sortid";
        
        DataTable dtfz = DBHelper.ExecuteDataTable(sql);
        foreach (DataRow item in dtfz.Rows)
        {
            int id = Convert.ToInt32(item["id"]);
            string mname = item["L001"].ToString();
           
            html += "   <li ><a  class='top'>"+ mname + "</a>";
            DataTable dttzz = DBHelper.ExecuteDataTable("select t.L001,m.MenuFile,m.id  from  menuLeft m left join  T_translation t on m.ID=t.primarykey  where t.tableName='menuleft'  and ParentID ="+id+"  and  isfold=1  order by m.sortid");

            if (dttzz.Rows.Count > 0) {
                html += "<ul class='ee'>";
                foreach (DataRow itemz in dttzz.Rows)
                {
                    string mname1 = itemz["L001"].ToString();
                    string mfile = itemz["MenuFile"].ToString();
                    html += "<li ><a target='mainframe' href = '" + mfile + "'> "+ mname1 + " </a></li>";
                }
               html += "</ul>";
            }  
   html += "</li>"; 
        } 
 html +=   "</ul>";

         

        return html;
    }


    public string GetTitle()
    {
        //return DBHelper.ExecuteScalar("select MenuName from menuleft where id='" + Request.QueryString["pid"] + "' and isfold='1' order by id asc").ToString();
        string field = System.Web.HttpContext.Current.Session["LanguageCode"].ToString();
        return GetFormatString(DBHelper.ExecuteScalar("select t." + field + " as MenuName from menuleft m left outer join t_translation t on m.id=t.primarykey  where m.id='" + Request.QueryString["pid"] + "' and m.isfold='1' and t.tableName='menuleft' order by m.id asc").ToString(), 13);
    }

    public string GetFormatString(string str, int len)
    {
        if (str.Length > len)
            return str.Substring(0, len) + "...";
        return str;
    }
}