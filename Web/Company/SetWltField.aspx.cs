using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using BLL.other;

public partial class SetWltField : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(6313);

        if (!IsPostBack && Request.QueryString["action"] == null)
        {
            Repeater1.DataSource = WTreeBLL.GetWLTField("1", "0");
            Repeater1.DataBind();
            Repeater4.DataSource = WTreeBLL.GetWLTField("1", "1");
            Repeater4.DataBind();

            Repeater2.DataSource = WTreeBLL.GetWLTField("2", "0");
            Repeater2.DataBind();
            Repeater5.DataSource = WTreeBLL.GetWLTField("2", "1");
            Repeater5.DataBind();

            Repeater3.DataSource = WTreeBLL.GetWLTField("3", "0");
            Repeater3.DataBind();
            Repeater6.DataSource = WTreeBLL.GetWLTField("3", "1");
            Repeater6.DataBind();

        }

        if (Request.QueryString["action"] == "save")
        {
            string f = Request.QueryString["field"];
            string v = Request.QueryString["visible"];
            string id = Request.QueryString["id"];

            //if (id == "1")
            //{
            //    if (f.IndexOf("|") == -1)
            //    {
            //        Response.Write(GetTran("007953", "修改失败,必须要有“|”符号！"));
            //        Response.End();
            //        return;
            //    }
            //}

            int hs = WTreeBLL.UpdWLTField(f, v, id);

            if (hs == 2)
                Response.Write(GetTran("000222", "修改成功！"));
            else
                Response.Write(GetTran("000225", "修改失败！"));

            Response.End();
        }
    }
}