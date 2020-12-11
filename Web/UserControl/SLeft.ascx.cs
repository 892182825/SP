using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using BLL.other;

public partial class UserControl_SLeft : System.Web.UI.UserControl
{
    protected string menuParentid = "";
    protected string menuUrl = "";
    public string GetTran(string keyCode, string defaultText)
    {
        return BLL.Translation.Translate(keyCode, defaultText);
    }

    private string GetStoreId()
    {
        if (Session["Store"] != null)
        {
            return Session["Store"].ToString();
        }
        else
        {
            return "";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ThreeRedirect(Page, Permissions.redirUrl);

        this.ltlId.Text = "<font color=''>" + GetStoreId() + "</font>";
        this.ltlNme.Text = "<font color=''>" + MenuBLL.GetStoreName(GetStoreId()) + "</font>";
        if (Convert.ToString(Session["UserLastLoginDate"]) != "" || Session["UserLastLoginDate"] != null)
        {
            this.ltlTime.Text = "<font color=''>" + Convert.ToDateTime(Session["UserLastLoginDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "</font>";
        }
        else
        {
            this.ltlTime.Text = "<font color=''>" + MenuBLL.GetLoginTime(GetStoreId(), 1).ToString("yyyy-MM-dd HH:mm:ss") + "</font>";
        }
        if (!IsPostBack)
        {
            ///动态加载菜单
            LoadMenu();
        }
    }
    /// <summary>
    /// 动态加载菜单
    /// </summary>
    public void LoadMenu()
    {
        StringBuilder sb = new StringBuilder();

        //string field = System.Web.HttpContext.Current.Session["LanguageCode"].ToString();
        //SqlParameter[] paras = new SqlParameter[1];
        //paras[0] = new SqlParameter("@tid", tid);
        //StringBuilder sql = new StringBuilder();
        //sql.Append("select t." + field + " as menuName,m.isfold from menuleft m,t_translation t where m.id = t.primarykey and t.tablename = 'menuleft' and  m.id=@tid");
        //return DBHelper.ExecuteScalar(sql.ToString(), paras, CommandType.Text).ToString();


        ///第一级菜单  左侧菜单栏
        DataTable dt_one;
        if (Session["LanguageCode"].ToString() == "L001")
        {
            dt_one = DAL.DBHelper.ExecuteDataTable("select sortid,MenuName,ImgFile from menu where sortid in(56,57,58,59,60) order by ID ");
        }
        else
        {
            dt_one = DAL.DBHelper.ExecuteDataTable("select sortid,L002 as MenuName ,ImgFile from menu join T_translation on menu.MenuName=T_translation.description where sortid in(56,57,58,59,60)and T_translation.tableName='menu' and T_translation.columnName='menuname'and T_translation.primarykey in(56,57,58,59,60)order by ID ");
        }

        string thispage = getthispage();
        string thispagepram = getthispageparm();

        DataTable dt_two;
        int j = 0;
        for (int i = 0; i < dt_one.Rows.Count; i++)
        {
            sb.Append(@"<li>
					<a href='javascript:;'>
                         <i class='glyphicon " + dt_one.Rows[i]["ImgFile"] + @" imgFont'></i>
						<span>" + dt_one.Rows[i]["MenuName"] + @" </span>
					</a>
					<ol class='list-unstyled current'>");
            String str = "";
            if (Session["LanguageCode"].ToString() == "L001")
            {
                dt_two = DAL.DBHelper.ExecuteDataTable("select ID,ParentID,MenuName,MenuFile,ImgFile from menu where ParentID=" + dt_one.Rows[i]["sortid"] + " and isfold=0 and sortid!=78 order by ID ");
            }
            else
            {
                dt_two = DAL.DBHelper.ExecuteDataTable("select distinct ID,L002 as MenuName,ImgFile,MenuFile,m.ParentID from T_translation t,menu m where t.description=m.MenuName and m.ParentID in(" + dt_one.Rows[i]["sortid"] + ")  and isfold=0 and sortid!=78  and t.tableName='menu' order by ID ");
            }
            for (j = 0; j < dt_two.Rows.Count; j++)
            {
                if (dt_two.Rows[j]["MenuFile"].ToString().ToLower().IndexOf(thispage) != -1 && thispage != "accountdetail.aspx") //当前页面
                {
                    sb.Append(@"<li>
	                    <a href='" + dt_two.Rows[j]["MenuFile"] + @"' style='color:#428bca;'>
                     <i class='glyphicon " + dt_two.Rows[j]["ImgFile"] + @" imgFont'></i>     
		                   <span>" + dt_two.Rows[j]["MenuName"] + @"</span>
	                    </a>
                      </li>");

                    menuParentid = dt_two.Rows[j]["ParentID"].ToString();
                    menuUrl = thispage; 
                }
                else
                {
                    if ((dt_two.Rows[j]["MenuFile"].ToString().ToLower() == "../store/shownetworkviewdp.aspx" && thispage == "storenet.aspx")
                        || (dt_two.Rows[j]["MenuFile"].ToString().ToLower() == "../store/shownetworkviewdp.aspx" && thispage == "storenet.aspx")
                        || (dt_two.Rows[j]["MenuFile"].ToString().ToLower() == "../store/pwdmodify.aspx" && thispagepram == "checkadv.aspx?type=store&url=pwdmodify")
                        || (dt_two.Rows[j]["MenuFile"].ToString().ToLower() == "../store/storeinfomodify.aspx" && thispagepram == "checkadv.aspx?type=store&url=storeinfomodify")
                        || (dt_two.Rows[j]["MenuFile"].ToString().ToLower() == "../accountdetail/accountdetail.aspx?type=accountzz" && thispagepram == "accountdetail.aspx?type=accountzz")
                        || (dt_two.Rows[j]["MenuFile"].ToString().ToLower() == "../accountdetail/accountdetail.aspx?type=accountdh" && thispagepram == "accountdetail.aspx?type=accountdh")
                        || (dt_two.Rows[j]["MenuFile"].ToString().ToLower() == "../store/orderagainbegin.aspx?type=sj" && thispagepram == "shopinglist.aspx?type=sj")
                        || (dt_two.Rows[j]["MenuFile"].ToString().ToLower() == "../store/orderagainbegin.aspx" && thispagepram == "shopinglist.aspx?type=new"))
                    {
                        sb.Append(@"<li>
	                    <a href='" + dt_two.Rows[j]["MenuFile"] + @"' style='color:#428bca;'>
                     <i class='glyphicon " + dt_two.Rows[j]["ImgFile"] + @" imgFont'></i>     
		                   <span>" + dt_two.Rows[j]["MenuName"] + @"</span>
	                    </a>
                      </li>");

                        menuParentid = dt_two.Rows[j]["ParentID"].ToString();
                        menuUrl = thispage;
                    }
                    else
                    {
                        sb.Append(@"<li>
	                    <a  href='" + dt_two.Rows[j]["MenuFile"] + @"'  >
                     <i class='glyphicon " + dt_two.Rows[j]["ImgFile"] + @" imgFont'></i>     
		                   <span>" + dt_two.Rows[j]["MenuName"] + @"</span>
	                    </a>
                      </li>");
                    }
                }

                str = "<span>" + (String)dt_two.Rows[j]["MenuFile"] + "</span>";
            }

            sb.Append(@"</ol></li>");
        }
        //返回菜单信息给前台页面
        Literal1.Text = sb.ToString();

    }
    protected string getthispage()
    {
        if (Session["Store"] != null)
        {
            //获取请求地址url
            string url = Request.Url.ToString();
            //获取页面名称
            string pageName = url.Substring(url.LastIndexOf('/') + 1).ToLower();
            //判断是否带参数了
            int spcialIndex = pageName.IndexOf('?');
            if (spcialIndex != -1)
                pageName = pageName.Substring(0, spcialIndex).ToLower();

            return pageName;
        }
        else
            return "";
    }
    protected string getthispageparm()
    {
        if (Session["Store"] != null)
        {
            //获取请求地址url
            string url = Request.Url.ToString();
            //获取页面名称
            string pageName = url.Substring(url.LastIndexOf('/') + 1).ToLower();

            return pageName;
        }
        else
            return "";
    }
}