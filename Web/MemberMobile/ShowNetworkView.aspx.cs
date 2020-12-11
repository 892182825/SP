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
using System.Collections.Generic;

public partial class Member_ShowNetworkView : BLL.TranslationBase
{
    BLL.Registration_declarations.RegistermemberBLL registermemberBLL = new BLL.Registration_declarations.RegistermemberBLL();
    BLL.CommonClass.CommonDataBLL commonDataBLL = new BLL.CommonClass.CommonDataBLL();
    BLL.Registration_declarations.DetialQueryBLL detialQueryBLL = new BLL.Registration_declarations.DetialQueryBLL();
    protected string width = "600";
    public string msg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        //Permissions.CheckMemberPermission();//验证是否已登录
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));

        if (Request.QueryString["bh"] != null)
        {
            Session["jgbh"] = Request.QueryString["bh"];
        }

        if (!IsPostBack)
        {

            if (Request.QueryString["isAnzhi"] != null)
            {
                if (Request.QueryString["isAnzhi"].ToString() == "az")
                {
                    this.Button6.Style.Add("display", "none");
                    this.Button7.Style.Add("display", "");
                }
                else
                {
                    this.Button6.Style.Add("display", "");
                    this.Button7.Style.Add("display", "none");
                }
            }
            else
            {
                if (Session["jglx"] == null)
                {
                    this.Button6.Style.Add("display", "");
                    this.Button7.Style.Add("display", "none");
                }
                else
                {
                    if (Session["jglx"].ToString() == "az")
                    {
                        this.Button6.Style.Add("display", "none");
                        this.Button7.Style.Add("display", "");
                    }
                    else
                    {
                        this.Button6.Style.Add("display", "");
                        this.Button7.Style.Add("display", "none");
                    }
                }
            }

            if (Request.QueryString["cengshu"] == null)
            {
                if (Session["jgcw"] == null)
                {
                    this.TextBox1.Text = "3";
                }
                else
                {
                    this.TextBox1.Text = Session["jgcw"].ToString();
                }
            }
            else
            {
                this.TextBox1.Text = Request.QueryString["cengshu"].ToString();
            }
            // int StartIndex = Convert.ToInt32(Request.QueryString["SelectGrass"]);
            //  DropDownList1.SelectedIndex = StartIndex;

            CommonDataBLL.BindQishuList(this.DropDownList1, false);
            
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


            if (Request.QueryString["bh"] != null)
            {
                if (Request.QueryString["bh"].ToString() != "")
                {
                    this.txtBh.Text = Request.QueryString["bh"].ToString();
                    GetView();
                }
            }
            else
            {
                this.txtBh.Text = Session["Member"].ToString();
                Button1_Click(null, null);
            }

            Session["jgqs"] = this.DropDownList1.SelectedValue;
            SetDaoHang();
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
            divDH.InnerHtml = GetTran("007032", "链路图") + "：";
            if (Session["DHNumbers"] == null)
            {
                Session["DHNumbers"] = new string[2] { txtBh.Text, "" };
                divDH.InnerHtml += "<a href='ShowNetWorkView.aspx?net=" + Session["jglx"].ToString() + "&SelectGrass=" + DropDownList1.SelectedValue + "&bh=" + txtBh.Text + "'>" + CommonDataBLL.GetPetNameByNumber(txtBh.Text) + "</a> →";
            }
            else
            {
                string[] nums = Session["DHNumbers"] as string[];

                if (nums[0] != txtBh.Text)
                {
                    if (nums[1] != txtBh.Text)
                    {
                        nums[1] = txtBh.Text;
                    }

                    IList<string> lists = Jiegou.GetNumberForTop(nums[0], Convert.ToInt32(DropDownList1.SelectedValue), Session["jglx"].ToString() == "az");
                    int count = 0;
                    foreach (string str in lists)
                    {
                        if (nums[1] == str)
                            count++;
                    }

                    if (count == 0)
                        divDH.InnerHtml += "<a href='ShowNetWorkView.aspx?net=" + Session["jglx"].ToString() + "&SelectGrass=" + DropDownList1.SelectedValue + "&bh=" + nums[1] + "'>" + CommonDataBLL.GetPetNameByNumber(nums[1]) + "</a> →";
                    else
                    {
                        string highNum = nums[1];
                        string numbers = "";
                        do
                        {
                            numbers += highNum + ",";
                            highNum = Jiegou.GetHighNumber(highNum, Session["jglx"].ToString() == "az");
                        } while (highNum != nums[0]);
                        numbers += nums[0] + ",";

                        for (int i = numbers.Split(new char[] { ',' }).Length - 1; i >= 0; i--)
                        {
                            if (numbers.Split(new char[] { ',' })[i] != "")
                                divDH.InnerHtml += "<a href='ShowNetWorkView.aspx?net=" + Session["jglx"].ToString() + "&SelectGrass=" + DropDownList1.SelectedValue + "&bh=" + numbers.Split(new char[] { ',' })[i] + "'>" + CommonDataBLL.GetPetNameByNumber(numbers.Split(new char[] { ',' })[i]) + "</a> →";
                        }

                    }
                }
                else
                    divDH.InnerHtml += "<a href='ShowNetWorkView.aspx?net=" + Session["jglx"].ToString() + "&SelectGrass=" + DropDownList1.SelectedValue + "&bh=" + nums[0] + "'>" + CommonDataBLL.GetPetNameByNumber(nums[0]) + "</a> →";

                Session["DHNumbers"] = nums;
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


    protected string GetUrl(string url, string para)
    {
        return url + para;
    }
    /// <summary>
    /// 显示网络图
    /// </summary>
    private void showData()
    {
        //检查编号是否合法
        bool flag = registermemberBLL.isNet(Session["jglx"].ToString(), Convert.ToString(Session["jgbh"]), this.getBH());
        //if (!jiegou.isValid(Convert.ToString(Session["jgbh"]), sfType.getMemberBH(), isAnZhi(), Convert.ToInt32(Session["jgqs"])))
        if (!flag)
        {
            Button1.Enabled = false;
            Button2.Enabled = false;
            Response.Write(GetTran("000892", "您不能查看该网络"));
            return;
        }



        //Jiegou.wltForEng(1, 4, false, "", "ShowNetworkView.aspx?bh=");
        this.Repeater1.DataSource = Jiegou.wltForEngMember(Convert.ToInt32(DropDownList1.SelectedValue), Convert.ToInt32(Session["jgcw"])+1, isAnZhi(), Convert.ToString(Session["jgbh"]), "ShowNetworkView.aspx?bh=");
        this.Repeater1.DataBind();
        setWidth();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string bianhao = this.txtBh.Text.Trim();
        if (bianhao == "")
        {
            msg = "<script>alert('请输入编号！')</script>";
            return;
        }

        Session["jgbh"] = bianhao;

        Session["xqbh"] = bianhao;

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
            Session["jgbh"] = this.txtBh.Text.Trim();
        }

        string s = getBH();

        bool flag = registermemberBLL.isNet(Session["jglx"].ToString(), Convert.ToString(Session["jgbh"]), getBH());

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
            Session["jgbh"] = this.txtBh.Text.Trim();
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
        int cengshu = Convert.ToInt32(GetCengshu());
        string loginBh = GetLoginMember();
        int bhCw = AjaxClass.GetLogoutCw1(this.txtBh.Text.Trim(), Convert.ToInt32(this.DropDownList1.SelectedValue), true);
        int loginCw = AjaxClass.GetLogoutCw1(loginBh, Convert.ToInt32(this.DropDownList1.SelectedValue), true);
        int showCs = AjaxClass.GetShowCengS1(0);

        if (showCs - (bhCw - loginCw) < cengshu)
        {
            cengshu = showCs - (bhCw - loginCw);
        }
        Session["jgcw"] = cengshu;

        showData();

        SetDaoHang();
    }

    protected string GetCengshu()
    {
        int cengshu = 0;
        if (this.TextBox1.Text.Trim() == "")
            cengshu = 3;
        else
        {
            try
            {
                cengshu = Convert.ToInt32(this.TextBox1.Text.Trim());
            }
            catch
            {
                cengshu = 3;
            }
        }

        return cengshu.ToString();
    }

    protected string GetLoginMember()
    {
        return Session["Member"].ToString();
    }

    private void GetView()
    {
        string aaa = Session["jgbh"].ToString();
        string r = Convert.ToString(Session["jgbh"]);

        string s = getBH();

        int u = Convert.ToInt32(Session["jgqs"]);
        bool flag = registermemberBLL.isNet(Session["jglx"].ToString(), Convert.ToString(Session["jgbh"]), getBH());

        if (flag == false)
        {
            Response.Write(GetTran("000892", "您不能查看该网络"));
            return;
        }

        string bianhao = this.txtBh.Text.Trim();

        Session["jgbh"] = bianhao;
        if (Request.QueryString["type"] == null)
        {
            Session["xqbh"] = bianhao;
        }
        Session["jgqs"] = this.DropDownList1.SelectedValue;

        if (Session["jgqs"] == null || Session["jglx"] == null)
        {
            Response.Write(GetTran("000894", "调用错误"));
            Response.End();
        }

        Session["jgbh"] = Request.QueryString["bh"];

        bool err = false;
        try
        {
            if (this.TextBox1.Text == "")
            {
                this.TextBox1.Text = "3";
            }
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
        int cengshu = Convert.ToInt32(GetCengshu());
        string loginBh = GetLoginMember();
        int bhCw = AjaxClass.GetLogoutCw1(this.txtBh.Text.Trim(), Convert.ToInt32(this.DropDownList1.SelectedValue), true);
        int loginCw = AjaxClass.GetLogoutCw1(loginBh, Convert.ToInt32(this.DropDownList1.SelectedValue), true);
        int showCs = AjaxClass.GetShowCengS1(0);

        if (showCs - (bhCw - loginCw) < cengshu)
        {
            cengshu = showCs - (bhCw - loginCw);
        }
        Session["jgcw"] = cengshu;
        if (Request.QueryString["type"] == null)
        {
            Session["jgbh"] = Session["xqbh"];
        }
        showData();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Session["jgbh"] = Session["xqbh"];
        showData();

    }
    private void setWidth()
    {
        width = Convert.ToString((300 + Convert.ToInt32(Session["jgcw"]) * 12 * 4));
        Page.DataBind();
    }
    /// <summary>
    /// 取得要查询网络的起点编号
    /// </summary>
    /// <param name="isDian">是否是店调用</param>
    /// <returns>起点编号</returns>
    private string getBH()
    {
        string bh = "";
        if (Request.QueryString["bianhao"] == null)
        {
            bh = this.txtBh.Text;
            if (bh.Length == 0)
            {
                bh = Convert.ToString(Session["Member"]);
            }
            else
            {
                bh = txtBh.Text;
            }
        }
        else
        {
            if (Request.QueryString["bianhao"].ToString() == "")
            {
                bh = txtBh.Text;
                if (bh.Length == 0)
                {
                    bh = Convert.ToString(Session["Member"]);
                }
                else
                {
                    bh = txtBh.Text;
                }
            }
            else
                bh = Request.QueryString["bianhao"].ToString();
        }

        return bh;
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

    protected string isAnZhiNew()
    {
        if (Convert.ToString(Session["jglx"]) == "tj")
        {
            return "0";
        }
        return "1";
    }

    protected void btnCY_Click(object sender, EventArgs e)
    {
        Response.Redirect("MemberNetMap.aspx?net=" + Session["jglx"] + "&bianhao=" + this.txtBh.Text.Trim().ToString() + "&SelectGrass=" + this.DropDownList1.SelectedValue);
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Session["jgbh"] = this.txtBh.Text.Trim();
        Session["xqbh"] = Session["jgbh"];
        Response.Redirect("ShowNetworkBiaoGeView.aspx?SelectGrass=" + DropDownList1.SelectedValue.ToString());
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Session["jgbh"] = this.txtBh.Text.Trim();
        Session["xqbh"] = Session["jgbh"];
        if (Session["jglx"].ToString() == "az")
        {
            Response.Redirect("ShowNetworkViewNew.aspx?SelectGrass=" + DropDownList1.SelectedValue.ToString() + "&isanzhi=1");
        }
        else
        {
            Response.Redirect("ShowNetworkViewNewTj.aspx?SelectGrass=" + DropDownList1.SelectedValue.ToString() + "&isanzhi=0");
        }
    }

    protected void Button7_Click(object sender, EventArgs e)
    {
        Session["jgbh"] = this.txtBh.Text.Trim();
        Session["jglx"] = "tj";
        this.Button6.Style.Add("display", "none");
        this.Button7.Style.Add("display", "");
        Response.Redirect("ShowNetworkView.aspx?isAnzhi=tj&cengshu=" + this.TextBox1.Text.Trim() + "&bh=" + this.txtBh.Text.Trim() + "&SelectGrass=" + this.DropDownList1.SelectedValue.ToString());
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        Session["jgbh"] = this.txtBh.Text.Trim();
        Session["jglx"] = "az";
        this.Button6.Style.Add("display", "none");
        this.Button7.Style.Add("display", "");
        Response.Redirect("ShowNetworkView.aspx?isAnzhi=az&cengshu=" + this.TextBox1.Text.Trim() + "&bh=" + this.txtBh.Text.Trim() + "&SelectGrass=" + this.DropDownList1.SelectedValue.ToString());
    }
}
