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
using Model;
using DAL;
using System.Data.SqlClient;
using BLL.other.Company;


public partial class Member_MessageCascade : BLL.TranslationBase
{
    public string hybh;
    private readonly  MessageReceiveBLL bll = new MessageReceiveBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        hybh = Session["Member"].ToString();
        if (!IsPostBack)
        {
            string bh = Session["Member"].ToString();
            var id = Request.QueryString["id"];
            string dt_one = " select * from MessageSend where ID=" + id;
            //this.ucPagerMb1.PageInit(dt_one, "rep_km");
            DataTable dt = DAL.DBHelper.ExecuteDataTable(dt_one);
            rep_km.DataSource = dt;
            rep_km.DataBind(); 
            bll.UpdateIsRead("MessageReceive", id);
        }

    }


    protected string getloginRole(string str)    // 前台调用(接受对象的转换)
    {
        switch (str.Trim())
        {
            case "2":
                return GetTran("000599", "会员");
            case "1":
                return GetTran("000388", "店铺");
            default:
                return GetTran("000151", "管理员");
        }
    }
}
