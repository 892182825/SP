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
using Model.Other;

public partial class Company_ShowNetworkViewDP : BLL.TranslationBase
{

    protected string width = "600";

    protected void Page_Load(object sender, EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.BalanceQueryNetworkViewDP);

        Response.Redirect("StoreNet.aspx");
        if (!IsPostBack)
        {
            //绑定期数
            BLL.CommonClass.CommonDataBLL.BindQishuList(this.DropDownList_Qishu, false);
            //获得用选定期数显示在下拉框
            int SelectGrass = Convert.ToInt32(this.DropDownList_Qishu.SelectedValue);
            if (Request.QueryString["SelectGrass"] != null)
            {
                SelectGrass = Convert.ToInt32(Request.QueryString["SelectGrass"]);
            }
            int StartIndex = SelectGrass;
            this.DropDownList_Qishu.SelectedIndex = StartIndex;
            if (Session["jgcw"] == null)
            {
                Session["jgcw"] = 3;
            }
            string number = getBH(getType());
            Session["jgbh"] = number;
            Session["xqbh"] = number;
            ////如果传来的会员编号不为空
            if (Request.QueryString["bh"] != null)
            {
                Session["jgbh"] = Request.QueryString["bh"];

            }
            if (Session["jgqs"] == null)
            {
                Session["jgqs"] = this.DropDownList_Qishu.SelectedValue;
            }
            Session["jglx"] = "tj";
            //如果编号，期数，上编其中一个为空则显示调用错误
            if (Session["jgbh"] == null || Session["jgqs"] == null || Session["jglx"] == null)
            {
                Response.Write(GetTran("000894", "调用错误"));
                Response.End();
            }
            this.TextBox1.Text = Convert.ToString(Session["jgcw"]);
            showData();
        }
        Translations();
    }

    /// <summary>
    /// 取得要查询网络的起点编号
    /// </summary>
    /// <param name="isDian">是否是店调用</param>
    /// <returns>起点编号</returns>
    private string getBH(string sf)
    {
        string bh = "";//需要修改Session["Store"].ToString().Trim();
        if (bh.Length == 0)
        {
            switch (sf)
            {
                case "D":
                    bh = Session["Store"].ToString();
                    break;
                case "G":
                    bh = BLL.CommonClass.CommonDataBLL.getManageID(1);//不用修改
                    break;
            }
        }
        else
        {
            bh = BLL.CommonClass.CommonDataBLL.getManageID(2);//需要修改Session["Store"].ToString().Trim();
        }
        return bh;
    }

    /// <summary>
    /// 获得session 里的店编号
    /// </summary>
    /// <returns></returns>
    public static string getType()
    {
        string sfType = "H";
        if (HttpContext.Current.Session["Store"] != null)
        {
            sfType = "D";
        }
        if (HttpContext.Current.Session["Company"] != null)
        {
            sfType = "G";
        }
        return sfType;
    }

    private void Translations()
    {
        this.TranControls(this.Button1, new string[][] { new string[] { "000844", "显示" } });
        this.TranControls(this.Button2, new string[][] { new string[] { "000857", "回到顶部" } });
    }
    /// <summary>
    ///  按钮点击事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        //捕获用户输入异常提示错误
        bool err = false;
        try
        {
            Convert.ToInt32(this.TextBox1.Text);
        }
        catch
        {
            err = true;
        }
        if (err)
        {
            Response.Write(GetTran("000899", "请输入正确的层位"));
        }
        else
        {
            Session["jgcw"] = Convert.ToInt32(this.TextBox1.Text);
        }
        showData();
    }

    /// <summary>
    /// 显示树
    /// </summary>
    private void showData()
    {
        this.Repeater1.DataSource = JiegouDP.wlt(Convert.ToInt32(this.DropDownList_Qishu.SelectedValue), Convert.ToInt32(Session["jgcw"]), isAnZhi(), Convert.ToString(Session["jgbh"]), "ShowNetworkViewDP.aspx?bh=");
        //this.Repeater1.DataSource = JiegouDP.wlt(3, 1, false, "", "ShowNetworkViewDP.aspx?bh=");
        this.Repeater1.DataBind();

        setWidth();
    }
    /// <summary>
    /// 设定线宽度
    /// </summary>
    private void setWidth()
    {
        width = Convert.ToString((300 + Convert.ToInt32(Session["jgcw"]) * 12 * 4));
        Page.DataBind();
    }
    /// <summary>
    /// 按钮点击事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button2_Click(object sender, EventArgs e)
    {
        Session["jgbh"] = Session["xqbh"];
        showData();

    }
    //判断是否安置
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
