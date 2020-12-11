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
using Standard;
using BLL;
using BLL.other;

public partial class Company_ShowView : BLL.TranslationBase
{
    BLL.Registration_declarations.RegistermemberBLL registermemberBLL = new BLL.Registration_declarations.RegistermemberBLL();
    TreeViewBLL treeViewBLL = new TreeViewBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["bh"] != null)
        {
            Session["jgbh"] = Request.QueryString["bh"];

            string r = Convert.ToString(Session["jgbh"]);

            string s = sfType.getMemberBH();
            bool y = isAnZhi();
            if (y)
            {
                //Permissions.CheckManagePermission(Standard.Config.ENUM_COMPANY_PERMISSION.JIESUAN_QueryAnZhiNetworkView);
            }
            else
            {
                //Permissions.CheckManagePermission(Standard.Config.ENUM_COMPANY_PERMISSION.JIESUAN_QueryTuiJianNetworkView);
            }

            //this.DropDownList_QiShu.SelectedValue=Session["jgqs"].ToString();
            int u = Convert.ToInt32(Session["jgqs"]);
            bool flag = registermemberBLL.isNet(Session["jglx"].ToString(), SfType.getBH(), Convert.ToString(Session["jgbh"]));
            if (!flag)
            {
                Response.Write("你不能查看该网络！");
                return;
            }
        }
        if (Session["jgbh"] == null)
        {
            Session["jgbh"] = "8888888888";
        }
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        if (!Page.IsPostBack)
        {
            BLL.CommonClass.CommonDataBLL.BindQishuList(DropDownList_QiShu, false);

            if (Session["jgcw"] == null)
            {
                Session["jgcw"] = 5;
            }
            if (Session["jgbh"] == null || Session["jgqs"] == null || Session["jglx"] == null)
            {
                //Response.Write("调用错误！");
                //Response.End();
            }

            if (Request.QueryString["SelectGrass"] != null)
            {
                DropDownList_QiShu.SelectedIndex = Convert.ToInt32(Request.QueryString["SelectGrass"].ToString());
            }
            if (Request.QueryString["isanzhi"] != null)
            {
                ViewState["isanzhi"] = Request.QueryString["isanzhi"];
            }


            this.WangLuoTu(Session["jgbh"].ToString(), "", 1, 3, Convert.ToInt32(DropDownList_QiShu.SelectedValue), "");
        }
    }

    protected void WangLuoTu(string bianhao, string tree, int state, int cengshu, int qishu, string storeid)
    {
        SqlParameter[] paraJB ={
									   new SqlParameter("@ID",     SqlDbType.VarChar,20),
									   new SqlParameter("@TREE",   SqlDbType.VarChar,400),
									   new SqlParameter("@ISAZ",   SqlDbType.Int),
				                       new SqlParameter("@CS ",    SqlDbType.Int),
				                       new SqlParameter("@ExpectNum ", SqlDbType.Int),
				                       new SqlParameter("@storeID",SqlDbType.VarChar,20)
								  };
        paraJB[0].Value = bianhao;
        paraJB[1].Value = tree;
        paraJB[2].Value = state;
        paraJB[3].Value = cengshu;
        paraJB[4].Value = qishu;
        paraJB[5].Value = storeid;
        DataTable dt = DAL.DBHelper.ExecuteDataTable("JS_TreeNet", paraJB, CommandType.StoredProcedure);

        string str = "";
        foreach (DataRow row in dt.Rows)
        {
            str += row[0].ToString();
        }
        this.statr0.InnerHtml = str;
    }

    protected void Button1_Click(object sender, System.EventArgs e)
    {
        int cengshu = 0;
        string bianhao = "";
        if (this.txtbianhao.Text.Trim() == "")
            bianhao = "8888888888";
        else
            bianhao = this.txtbianhao.Text.Trim();

        if (this.txtceng.Text.Trim() == "")
            cengshu = 1;
        else
            cengshu = Convert.ToInt32(this.txtceng.Text.Trim());
        this.WangLuoTu(bianhao, "",1, cengshu, Convert.ToInt32(DropDownList_QiShu.SelectedValue), "");
    }

    private bool isAnZhi()
    {
        bool temp = true;
        if (Convert.ToString(Session["jglx"]) == "tj")
        {
            temp = false;
        }
        return temp;
    }
}
