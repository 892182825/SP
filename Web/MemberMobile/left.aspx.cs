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
using BLL.other;

public partial class Member_left : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        if (!IsPostBack)
        {
            if (MenuBLL.GetMenuCount(3, GetMenuParentId()) > 10)
            {
                this.div_id.InnerHtml = MenuBLL.GetMenu(3, GetMenuParentId());
                this.divMore.Style.Add("display", "");
                this.divSome.Style.Add("display", "none");
                //this.titleName.InnerText = MenuBLL.GetTitleName(3, GetMenuParentId());
                this.Literal1.Text = MenuBLL.GetTitleName(3, GetMenuParentId());
            }
            else
            {
                //this.divSome.InnerHtml = MenuBLL.GetMenu(3, GetMenuParentId());
                string strMenu = MenuBLL.GetMenu(3, GetMenuParentId());
                if (strMenu.Contains("收件箱"))
                {
                    SqlParameter[] array_para = new SqlParameter[]{new SqlParameter("@number",Session["Member"]),
                                                                 new SqlParameter("@messagetype",'m'),
                                                                 new SqlParameter("@numbertype",2),
                                                                 new SqlParameter("@count",SqlDbType.Int)};
                    array_para[3].Direction = ParameterDirection.Output;
                    DAL.DBHelper.ExecuteNonQuery("[GetNumberOfUnreadReceive]", array_para, CommandType.StoredProcedure);
                    string newmails = array_para[3].Value.ToString();
                    strMenu = strMenu.Replace("收件箱", "收件箱(" + newmails + ")");
                }
                this.Literal2.Text = strMenu;

                this.divSome.Style.Add("display", "");
                this.divMore.Style.Add("display", "none");
            }

            this.ltlNumber.Text = "<font color='red'>" + GetMemberId() + "</font>";
            this.ltlNme.Text = "<font color='red'>" + Encryption.Encryption.GetDecipherName(MenuBLL.GetMemberName(GetMemberId())) + "</font>";
            //if (Convert.ToString(Session["UserLastLoginDate"]) != "" || Session["UserLastLoginDate"] != null)
            //{
            //    if (Session["UserLastLoginDate"].ToString() == "1900-01-01 00:00:00.000")
            //    {
            //        this.ltlTime.Text = "<font color='red'>" + MenuBLL.GetLoginTime(GetMemberId(), 3).ToString("yyyy-MM-dd HH:mm:ss") + "</font>";
            //    }
            //    else
            //    {
            //        this.ltlTime.Text = "<font color='red'>" + Convert.ToDateTime(Session["UserLastLoginDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "</font>";
            //    }
            //}
            //else
            //{
            if (MenuBLL.GetLoginTime(GetMemberId(), 3).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString("yyyy-MM-dd") == "1900-01-01")
            {
                this.ltlTime.Text = "<font color='red'>首次登录</font>";
            }
            else
            {
                this.ltlTime.Text = "<font color='red'>" + MenuBLL.GetLoginTime(GetMemberId(), 3).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString("yyyy-MM-dd HH:mm:ss") + "</font>";
            }
            //}
        }
    }


    /// <summary>
    /// 根据URL获取大菜单目录
    /// </summary>
    /// <returns></returns>
    private int GetMenuParentId()
    {
        if (Request.QueryString["pid"] == null)
        {
            return 160;
        }
        else
        {
            return int.Parse(Request.QueryString["pid"].ToString());
        }
    }

    private string GetMemberId()
    {
        if (Session["Member"] != null)
        {
            return Session["Member"].ToString();
        }
        else
        {
            return "";
        }
    }

}
