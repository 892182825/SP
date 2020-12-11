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
using BLL.CommonClass;

public partial class Member_ShowLinkView : BLL.TranslationBase
{
    BLL.CommonClass.CommonDataBLL commonDataBLL = new BLL.CommonClass.CommonDataBLL();
    BLL.Registration_declarations.DetialQueryBLL detialQueryBLL = new BLL.Registration_declarations.DetialQueryBLL();
    BLL.Registration_declarations.RegistermemberBLL registermemberBLL = new BLL.Registration_declarations.RegistermemberBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        //检查店铺权限
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
       // Permissions.CheckMemberPermission();//验证是否已登录

        //判断是否正确调用（bh，type，qs值是否已经传到）
        if (Request.QueryString["bh"] == null || Request.QueryString["type"] == null || Request.QueryString["qs"] == null)
        {
            Response.Write("调用错误！");
            Response.End();
        }

        if (!IsPostBack)
        {
            CommonDataBLL.BindQishuList(DropDownList1, false);

            this.DropDownList1.SelectedValue = Request.QueryString["qs"];
            bool flag = registermemberBLL.isNet(Session["jglx"].ToString(), this.getBH(SfType.getType()), Convert.ToString(Session["jgbh"]));

           // if (jiegou.isValid(Request.QueryString["bh"].ToString(), sfType.getMemberBH(), isAnzhi(Request.QueryString["type"]), Convert.ToInt32(Request.QueryString["qs"])))
            if(flag)
            {
                string url = "ShowLinkView.aspx?type=" + Request.QueryString["type"] + "&qs=" + Request.QueryString["qs"] + "&bh=";
                DataTable dt = Jiegou.Link_Map(Convert.ToInt32(Convert.ToInt32(Request.QueryString["qs"])), isAnzhi(Request.QueryString["type"]), Request.QueryString["bh"], url);
                ViewState["bh"] = Request.QueryString["bh"];
                this.Repeater1.DataSource = new DataView(dt, "", "id desc", DataViewRowState.CurrentRows);
                this.Repeater1.DataBind();
            }
            else
            {
                Response.Write(GetTran("000892", "您不能查看该网络"));
                Response.End();
            }
        }
        Translations();
    }

    private void Translations()
    {
        this.TranControls(this.Button1, new string[][] { new string[] { "000177", "显示" } });
        this.TranControls(this.Button2, new string[][] { new string[] { "000857", "回到顶部" } });
    }

    private string getBH(string sf)
    {
        string bh = "";
            switch (sf)
            {
                case "D":
                    string storeid =  Session["Store"].ToString();
                    bh = BLL.Registration_declarations.DetialQueryBLL.GetNumberByStoreID(storeid);
                    break;
                case "G":
                    bh = BLL.CommonClass.CommonDataBLL.getManageID(1);
                    break;
                case "H":
                    bh =  Convert.ToString(Session["Member"]);
                    break;
            }

        return bh;
    }
   
    /// <summary>
    /// 判断是否是安置图
    /// </summary>
    /// <param name="type">类型（tj或者az）</param>
    /// <returns>是否是安置</returns>
    private bool isAnzhi(string type)
    {
        bool temp = true;
        if (type == "tj")
        {
            temp = false;
        }
        return temp;
    }

    protected void Button1_Click(object sender, System.EventArgs e)
    {
        string url = "ShowLinkView.aspx?type=" + Session["jglx"].ToString() + "&qs=" + Session["jgqs"].ToString() + "&bh=";
        //Jiegou.Link_Map(1, false, "4444444444", "showLinkView.aspx?");
        this.Repeater1.DataSource = Jiegou.Link_Map(Convert.ToInt32(DropDownList1.SelectedValue), isAnzhi(Session["jglx"].ToString()),Session["jgbh"].ToString(), url);
        this.Repeater1.DataBind();

    }

    protected void Button2_Click(object sender, System.EventArgs e)
    {
        string url = "ShowLinkView.aspx?type=" + Session["jglx"].ToString() + "&qs=" + Session["jgqs"].ToString() + "&bh=";
        this.Repeater1.DataSource = Jiegou.Link_Map(Convert.ToInt32(Session["jgqs"].ToString()), isAnzhi(Session["jglx"].ToString()), Session["jgbh"].ToString(), url);
        this.Repeater1.DataBind();
    }

}
