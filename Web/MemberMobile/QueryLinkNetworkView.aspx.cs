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
using BLL.Registration_declarations;

public partial class Member_QueryLinkNetworkView : BLL.TranslationBase
{
    BLL.CommonClass.CommonDataBLL commonDataBLL = new BLL.CommonClass.CommonDataBLL();
    BLL.Registration_declarations.DetialQueryBLL detialQueryBLL = new BLL.Registration_declarations.DetialQueryBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);


        if (!IsPostBack)
        {
            Session["Store"] = null;
            CommonDataBLL.BindQishuList(DropDownList_QiShu, false);
        }
        Translations();
    }

    private void Translations()
    {
        this.TranControls(this.btn_Submit, new string[][] { new string[] { "000434", "确定" } });
        this.TranControls(this.RadioButtonList_Type,
                            new string[][]
                            {	
                            new string []{"000463","推荐链路图"},
                            new string []{"000462","安置链路图"}													
                            
                            });

    }
    /// <summary>
    /// 选择网络图显示
    /// </summary>
    /// <param name="bianhao">起点编号</param>
    /// <param name="type">选择的网络图类型</param>
    private void getInfo(string bianhao, RadioButtonList type)
    {
        Session["jgbh"] = bianhao;
        Session["xqbh"] = bianhao;
        Session["jgqs"] = DropDownList_QiShu.SelectedValue;
        switch (type.SelectedValue)
        {
            case "wlt_tj":
                //推荐网络图
                Session["jglx"] = "tj";
                Response.Redirect("ShowNetworkView.aspx?SelectGrass=" + DropDownList_QiShu.SelectedIndex.ToString());
                break;
            case "wlt_az":
                //安置网络图
                Session["jglx"] = "az";
                Response.Redirect("ShowNetworkView.aspx?SelectGrass=" + DropDownList_QiShu.SelectedIndex.ToString());
                break;
            case "llt_tj":
                //推荐链路图
                Session["jglx"] = "tj";
                Session["M_L_TJ"] = bianhao;
                //Response.Redirect("ShowLinkView.aspx?bh=" + bianhao + "&type=tj&qs=" + DropDownList_QiShu.SelectedValue);
                Response.Redirect("LLT_TJ.aspx?ThNumber=" + bianhao + "&qs=" + DropDownList_QiShu.SelectedValue);
                break;
            case "llt_az":
                //安置链路图
                Session["jglx"] = "az";
                Session["M_L_AZ"] = bianhao;
                //Response.Redirect("ShowLinkView.aspx?bh=" + bianhao + "&type=az&qs=" + DropDownList_QiShu.SelectedValue);
                Response.Redirect("LLT_AZ.aspx?ThNumber=" + bianhao + "&qs=" + DropDownList_QiShu.SelectedValue);
                break;
        }
    }

    /// <summary>
    /// 取得要查询网络的起点编号
    /// </summary>
    /// <param name="isDian">是否是店调用</param>
    /// <returns>起点编号</returns>
    private string getBH(string sf)
    {
        string bh = txtBox_GLBH.Text;
        if (bh.Length == 0)
        {
            switch (sf)
            {
                case "D":
                    string storeid = Session["Store"].ToString();
                    bh = DetialQueryBLL.GetNumberByStoreID(storeid);//Session["Store"]
                    break;
                case "G":
                    bh = BLL.CommonClass.CommonDataBLL.getManageID(3); ;
                    break;
                case "H":
                    bh = Convert.ToString(Session["Member"]);
                    break;

            }
        }
        else
        {
            bh = txtBox_GLBH.Text;
        }

        return bh;
    }


    protected void btn_Submit_Click(object sender, System.EventArgs e)
    {
        getInfo(getBH(SfType.getType()), this.RadioButtonList_Type);

    }
}
