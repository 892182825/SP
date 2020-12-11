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
using BLL.other;
using BLL.CommonClass;
using DAL;
using Model.Other;
using System.Collections.Generic;

public partial class Company_ShowNotGoodView : BLL.TranslationBase
{
    CommonDataBLL CommonData = new CommonDataBLL();
    protected string width = "600";
    BLL.Registration_declarations.RegistermemberBLL registermemberBLL = new BLL.Registration_declarations.RegistermemberBLL();

    public string msg = "";

    protected void Page_Load(object sender, System.EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        //在此处放置用户代码以初始化页面
        //检查相应权限
        Response.Cache.SetExpires(DateTime.Now);

        // if (!Jiegou.isValid(Convert.ToString(Session["jgbh"]), getMemberBH(), isAnZhi(), Convert.ToInt32(Session["jgqs"])))

        Response.Redirect("ShowNotGoodViewII.aspx");

        if (!IsPostBack)
        {
            CommonDataBLL.BindQishuList(this.DropDownList1, false);
            ShowNumber();
            this.TextBox1.Text = "5";
            // int StartIndex = Convert.ToInt32(Request.QueryString["SelectGrass"]);
            //  DropDownList1.SelectedIndex = StartIndex;

            //  CommonDataBLL.DropDownListSelected(DropDownList1,Session["jgqs"].ToString().Trim());
            if (Request.QueryString["bh"] != null)
            {
                if (Request.QueryString["bh"].ToString() != "")
                {
                    this.txtBh.Text = Request.QueryString["bh"].ToString();
                    GetView();
                }
            }
            if (Request.QueryString["isAnzhi"] != null)
            {
                Session["jglx"] = Request.QueryString["isAnzhi"].ToString();
            }
            else
            {
                if (Session["jglx"] == null)
                {
                    Session["jglx"] = "tj";
                }
            }
            Session["jgqs"] = this.DropDownList1.SelectedValue;

            //------------------------
            SetDaoHang();

        }
        //固定会员的层位次数
        if (Session.Contents["bh"] != null)
        {
            this.TextBox1.Visible = false;
            this.lbl_msg.Visible = false;
        }
        Translations();
    }

    /// <summary>
    /// 网络图导航
    /// </summary>
    private void SetDaoHang()
    {
        if (txtBh.Text != null && txtBh.Text != "")
        {
            if (Session["DHNumbers"] == null)
            {
                Session["DHNumbers"] = txtBh.Text;
            }
            else
            {
                if (Session["DHNumbers"].ToString().IndexOf(txtBh.Text) == -1)
                    Session["DHNumbers"] = Session["DHNumbers"].ToString() + "," + txtBh.Text;
                string[] nums = Session["DHNumbers"].ToString().Split(new char[] { ',' });
                foreach (string num in nums)
                {
                    if (num != txtBh.Text)
                    {
                        IList<string> lists = Jiegou.GetNumberForTop(Session["jgbh"].ToString(), Convert.ToInt32(DropDownList1.SelectedValue), Session["jglx"].ToString() == "az");
                        int count = 0;
                        foreach (string str in lists)
                        {
                            if (num == str)
                                count++;
                        }

                        if (count == 0)
                            divDH.InnerHtml += "<a href='ShowNetWorkView.aspx?net=" + Session["jglx"].ToString() + "&SelectGrass=" + DropDownList1.SelectedValue + "&bh=" + num + "'>" + CommonDataBLL.GetPetNameByNumber(num) + "</a> →";
                    }
                }
            }
        }
        else
        {
            Session["DHNumbers"] = "";
            Session["DHNumbers"] = null;
        }
    }

    private void Translations()
    {
        this.TranControls(this.Button1, new string[][] { new string[] { "000844", "显示" } });
        this.TranControls(this.Button2, new string[][] { new string[] { "000857", "回到顶部" } });
    }

    protected void Button1_Click(object sender, System.EventArgs e)
    {

        // string aaa = Session["jgbh"].ToString();
        string bianhao = this.txtBh.Text.Trim();
        string manageId = BLL.CommonClass.CommonDataBLL.getManageID(3);
        if (bianhao == "")
        {
            msg = "<script>alert('请输入编号！')</script>";
            return;
        }

        if (!BLL.CommonClass.CommonDataBLL.GetRole(Session["Company"].ToString(), bianhao, isAnZhi()))
        {
            msg = "<script>alert('" + GetTran("000438", "对不起，您没有查看该会员网络的权限") + "！');</script>";
            return;
        }
        if (bianhao == "")
        {
            Session["jgbh"] = manageId;
            Session["xqbh"] = manageId;
        }
        else
        {
            Session["jgbh"] = bianhao;

            Session["xqbh"] = bianhao;
        }

        string r = Convert.ToString(Session["jgbh"]);

        string s = SfType.getBH();

        bool y = isAnZhi();
        if (y)//验证是否有权限
        {
            Permissions.CheckManagePermission(EnumCompanyPermission.BalanceQueryAnZhiNetworkView);
        }
        else
        {
            Permissions.CheckManagePermission(EnumCompanyPermission.BalanceQueryTuiJianNetworkView);
        }
        int u = Convert.ToInt32(Session["jgqs"]);
        bool flag = registermemberBLL.isNet(Session["jglx"].ToString(), SfType.getBH(), Convert.ToString(Session["jgbh"]));

        if (flag == false)
        {
            Response.Write(GetTran("000892", "您不能查看该网络"));
            return;
        }


        Session["jgqs"] = this.DropDownList1.SelectedValue;

        if (Session["jgqs"] == null || Session["jglx"] == null)
        {
            Response.Write(GetTran("000894", "调用错误"));
            Response.End();
        }

        if (Request.QueryString["bh"] != null)
        {
            Session["jgbh"] = Request.QueryString["bh"];
        }
        else
        {
            Session["jgbh"] = manageId;
        }

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

        Session["jgbh"] = Session["xqbh"];
        showData();

        SetDaoHang();
    }

    protected void lkn_submit_Click(object sender, EventArgs e)
    {
        Button1_Click(null, null);
    }

    private void GetView()
    {
        string aaa = Session["jgbh"].ToString();
        string r = Convert.ToString(Session["jgbh"]);

        string s = SfType.getBH();

        bool y = isAnZhi();
        if (y)//验证是否有权限
        {
            Permissions.CheckManagePermission(EnumCompanyPermission.BalanceQueryAnZhiNetworkView);
        }
        else
        {
            Permissions.CheckManagePermission(EnumCompanyPermission.BalanceQueryTuiJianNetworkView);
        }
        int u = Convert.ToInt32(Session["jgqs"]);
        bool flag = registermemberBLL.isNet(Session["jglx"].ToString(), SfType.getBH(), Convert.ToString(Session["jgbh"]));

        if (flag == false)
        {
            Response.Write(GetTran("000892", "您不能查看该网络"));
            return;
        }

        string bianhao = this.txtBh.Text.Trim();
        string manageId = BLL.CommonClass.CommonDataBLL.getManageID(3);
        if (bianhao == "")
        {
            Session["jgbh"] = manageId;
            Session["xqbh"] = manageId;
        }
        else
        {
            Session["jgbh"] = bianhao;
            if (Request.QueryString["type"] == null)
            {
                Session["xqbh"] = bianhao;
            }
        }
        Session["jgqs"] = this.DropDownList1.SelectedValue;

        if (Session["jgqs"] == null || Session["jglx"] == null)
        {
            Response.Write(GetTran("000894", "调用错误"));
            Response.End();
        }

        if (Request.QueryString["bh"] != null)
        {
            Session["jgbh"] = Request.QueryString["bh"];
        }
        else
        {
            Session["jgbh"] = manageId;
        }

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

        if (Request.QueryString["type"] == null)
        {
            Session["jgbh"] = Session["xqbh"];
        }
        showData();
    }

    protected void Button2_Click(object sender, System.EventArgs e)
    {
        Session["jgbh"] = Session["xqbh"];
        this.txtBh.Text = "";
        showData();
    }

    protected string GetUrl(string url, string para)
    {
        return url + para;
    }

    private void showData()
    {
        this.Repeater1.DataSource = Jiegou.wltForEngTwo(Convert.ToInt32(DropDownList1.SelectedValue), Convert.ToInt32(Session["jgcw"]), isAnZhi(), Convert.ToString(Session["jgbh"]), "ShowNetworkView.aspx?type=1&bh=");

        this.Repeater1.DataBind();
        setWidth();
    }

    private void ShowNumber()
    {
        this.Repeater2.DataSource = Jiegou.ShowNumber(Session["Company"].ToString(), isAnZhi());
        this.Repeater2.DataBind();
    }


    private void setWidth()
    {
        width = Convert.ToString((300 + Convert.ToInt32(Session["jgcw"]) * 12 * 4));
        //Page.DataBind();
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
