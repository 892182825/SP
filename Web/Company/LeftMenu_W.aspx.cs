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
        string pid = Request.QueryString["pid"].ToString();

        //SqlDataReader dr = DBHelper.ExecuteReader("select * from menuleft where ParentID='" + pid + "' and isfold='1' order by id asc");
        int isShow = 1;
        DataTable dt = DBHelper.ExecuteDataTable("select top 1 isnull(isShow,1) from MenuManage");
        if (dt.Rows.Count > 0)
        {
            isShow = Convert.ToInt32(dt.Rows[0][0]);
        }
        string field = System.Web.HttpContext.Current.Session["LanguageCode"].ToString();
        string strSql = "";
        if (isShow == 0)
        {
            strSql = "select imgfile,t." + field + " as MenuName,m.id from menuleft m left outer join t_translation t on m.id=t.primarykey  where m.MenuName!='预设短信' and m.ParentID='" + pid + "' and m.isfold='1' and t.tableName='menuleft' and m.id in (select menuid from manage g,permission_Name n,ManagerPermission p where p.managerid=g.roleid and p.permissionid=n.permission_id  and g.number='" + Session["Company"].ToString() + "') order by m.sortid asc";
        }
        else
        {
            strSql = "select imgfile,t." + field + " as MenuName,m.id from menuleft m left outer join t_translation t on m.id=t.primarykey  where m.MenuName!='预设短信' and m.ParentID='" + pid + "' and m.isfold='1' and t.tableName='menuleft' order by m.sortid asc";
        }
        SqlDataReader dr = DBHelper.ExecuteReader(strSql);
        //style='width:174px;height:25px;overflow:hidden;font-size:11pt;color:rgb(25,101,130);font-weight:bold;padding-left:10px;cursor:pointer;font-family:Arial'
        int menui = 1;
        string strmenu = "";
        while (dr.Read())
        {
            if (menui != 1)
            {
                strmenu = strmenu + @"<div id='divtitle" + menui + @"'class='title' onclick='isShowMenu(this)' title='" + dr["MenuName"] + "'>" + dr["imgfile"] + GetFormatString(dr["MenuName"].ToString(), 13) + @"</div>
            <ul style='display:none' id='divbody" + menui + @"' class='menuson'>
            【※】
            </ul>";

            }
            else
            {

                strmenu = strmenu + @"<div id='divtitle" + menui + @"'class='title' onclick='isShowMenu(this)' title='" + dr["MenuName"] + "'>" + dr["imgfile"] + GetFormatString(dr["MenuName"].ToString(), 13) + @"</div>
            <ul id='divbody" + menui + @"' class='menuson'>
            【※】
            </ul>";
            }

            //<div id='divbody" + menui + @"' style='width:174px;display:none;'>
            //【※】
            //</div>
            //SqlDataReader drChild = DBHelper.ExecuteReader("select * from menuleft where ParentID='" + dr["ID"] + "' and isfold='1' order by id asc");
            SqlDataReader drChild;
            if (isShow == 0)
            {
                drChild = DBHelper.ExecuteReader("select imgfile,t." + field + " as MenuName,m.MenuFile from menuleft m left outer join t_translation t on m.id=t.primarykey  where m.MenuName!='预设短信' and  m.ParentID='" + dr["ID"] + "' and m.isfold='1' and t.tableName='menuleft' and m.id in (select menuid from manage g,permission_Name n,ManagerPermission p where p.managerid=g.roleid and p.permissionid=n.permission_id  and g.number='" + Session["Company"].ToString() + "')  order by m.sortid asc");
            }
            else
            {
                drChild = DBHelper.ExecuteReader("select imgfile,t." + field + " as MenuName,m.MenuFile from menuleft m left outer join t_translation t on m.id=t.primarykey  where m.MenuName!='预设短信' and m.ParentID='" + dr["ID"] + "' and m.isfold='1' and t.tableName='menuleft' order by m.sortid asc");
            }
            string strtable = "";
            if (menui != 1)
            {
                strtable = @"<ul style='display:none' class='menuson' id='table" + menui + @"'>";
            }
            else
            {

                strtable = @"<ul class='menuson' id='table" + menui + @"'>";
            }

            while (drChild.Read())
            {
                string newmails = "";
                //<div id='divbodyMessageReceive' style='width:174px;display:none;'><table>
                if (drChild["MenuName"].ToString().Equals("邮件管理"))
                {
                    newmails = DAL.DBHelper.ExecuteScalar("select count(1) from MessageReceive where ReadFlag=0 and DropFlag=0 and LoginRole=0 and ClassID in(select ClassID from MsgClassAdmin where Admin=@user) and ID not in(select MessageID from dbo.F_DroppedReceiveByOperator(@user,0))", new SqlParameter("@user", Session["Company"].ToString()), CommandType.Text).ToString();
                    newmails = "(" + newmails + ")";
                    string mail_menu = @"<ul id='divtitleMessageReceive'  onclick='isShowMenu(this);ShowMessageReceive();' title='" + drChild["MenuName"] + "'>" + drChild["imgfile"] + GetFormatString(drChild["MenuName"].ToString(), 13) + newmails + @"</ul>";

                    System.Data.DataTable dt2 = DAL.DBHelper.ExecuteDataTable("select a.ID,a.ClassName,isnull(b.Amount,0) Amount,'ManageMessage_Recive.aspx?ClassID='+cast(a.ID as varchar(10)) Page from (select n.ID,n.ClassName from MsgClassAdmin m left join MsgClass n on n.ID=m.ClassID where Admin=@user) a left join (select ClassID,count(1) amount from MessageReceive where DropFlag=0 and ReadFlag=0 and LoginRole=0 and ID not in(select MessageID from dbo.F_DroppedReceiveByOperator(@user,0)) group by ClassID) b on a.ID=b.ClassID", new SqlParameter[] { new SqlParameter("@user", Session["Company"]) }, CommandType.Text);
                    foreach (DataRow row in dt2.Rows)
                    {
                        mail_menu += @"<li class='active'><cite></cite>
			                            <a target='mainframe' onclick='setColor(this)' style='font-family:Arial;text-decoration:none;color:#333333;'  href='" + row["Page"] + @"' title='" + row["ClassName"] + "'> " + drChild["imgfile"] + GetFormatString(row["ClassName"].ToString(), 7) + "(" + row["Amount"] + ")" + @"</a>
		                        <i></i></li>";
                    }
                    mail_menu += @"</ul>";
                    strtable += mail_menu;
                }
                //+ drChild["imgfile"]
                else
                {
                    strtable = strtable + @"<li class='jj'><cite></cite>
					                            <a target='mainframe' onclick='setColor(this)' style='font-family:Arial;text-decoration:none;color:#333333;' href='" + drChild["MenuFile"] + @"' title='" + drChild["MenuName"] + "'> " + drChild["ImgFile"].ToString() + GetFormatString(drChild["MenuName"].ToString(), 7) + @"</a>
				                            <i></i></li>";
                }
            }

            strtable = strtable + "</ul>";

            drChild.Close();

            strmenu = strmenu.Replace("【※】", strtable);

            menui++;
        }
        dr.Close();

        return strmenu;
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