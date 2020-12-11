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

//Add Namespace
using Model.Other;
using BLL.other.Company;

/*
 * 创建者：     汪  华
 * 创建时间：   2009-09-24
 */

public partial class Company_ViewChangeLog : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        ///检查相应的权限        
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.SystemChangeLogsQuery);

        ///设置span的样式
        spanConten.Attributes.Add("style", "word-break:keep-all;word-wrap:normal;white-space:nowrap");

        if (!IsPostBack)
        {
            int id = Convert.ToInt32(Request.QueryString["ID"]);
            string str = LogsManageBLL.GetChangeLogsRemarkByID(id);
            SqlDataReader dr = DAL.DBHelper.ExecuteReader("Select Remark from ChangeLogs where ID=" + id);
            if (dr.Read())
            {
                string sgasg = dr["remark"].ToString();
            }
            dr.Close();
            int seindex = str.IndexOf("操作类型",0);
            if (seindex > 0)
            {
                string seub = str.Substring(0,seindex);
                string etemp = str.Substring(seindex+4);
                str = seub + GetTran("003163").ToString() + etemp;
            }
            int index = 0;
            do
            {
                int i = 0;
                int n = 0;
                string sta = string.Empty;
                string end = string.Empty;
                i = str.IndexOf("<%key('", index);
                string sub = str.Substring(i + 7, 6);
                if (i > 0)
                {
                    sta = str.Substring(0, i);
                    sta = sta + GetTran(sub).ToString();
                    n = str.IndexOf("%>", i);
                    end = str.Substring(n + 2);
                    str = sta + end;
                }
                index = n;
            } while (index > 0);
            spanConten.InnerHtml = str;
        }
    }
}
















