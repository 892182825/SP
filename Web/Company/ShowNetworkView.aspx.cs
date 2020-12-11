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

public partial class Company_ShowNetworkView : BLL.TranslationBase
{
    CommonDataBLL CommonData = new CommonDataBLL();
    protected string width = "600";
    BLL.Registration_declarations.RegistermemberBLL registermemberBLL = new BLL.Registration_declarations.RegistermemberBLL();

    public string msg = "";

    public string[] strRes;

    protected void Page_Load(object sender, System.EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        //在此处放置用户代码以初始化页面
        //检查相应权限
        Response.Cache.SetExpires(DateTime.Now);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
       // if (!Jiegou.isValid(Convert.ToString(Session["jgbh"]), getMemberBH(), isAnZhi(), Convert.ToInt32(Session["jgqs"])))
        
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
            CommonDataBLL.BindQishuList(this.DropDownList1, false);
            
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

          //  CommonDataBLL.DropDownListSelected(DropDownList1,Session["jgqs"].ToString().Trim());
            if (Request.QueryString["isAnzhi"] != null)
            {
                string assgd = Request.QueryString["isAnzhi"].ToString();
                Session["jglx"] = Request.QueryString["isAnzhi"].ToString();
            }
            else
            {
                if (Session["jglx"] == null)
                {
                    Session["jglx"] = "tj";
                }
            }
            ShowNumber();
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
                DataTable dt = Jiegou.ShowTopNumber(Session["Company"].ToString(), isAnZhi());
                if (dt.Rows.Count > 0)
                {
                    this.txtBh.Text = dt.Rows[0]["number"].ToString();
                    GetView1();
                }
            }

            ViewState["qs"] = this.DropDownList1.SelectedValue;
            ViewState["cengshu"] = this.TextBox1.Text.Trim();
            ViewState["startbh"] = txtBh.Text.Trim();
           
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
            divDH.InnerHtml = GetTran("007032", "链路图") + "：";

            if (Session["DHNumbers"] == null)
            {
                Session["DHNumbers"] = new string[2] { txtBh.Text, "" };
                divDH.InnerHtml += "<a href='ShowNetWorkView.aspx?net=" + Session["jglx"].ToString() + "&SelectGrass=" + DropDownList1.SelectedValue + "&bh=" + txtBh.Text + "'>" + CommonDataBLL.GetPetNameByNumber(txtBh.Text) + "</a> →";
            }
            else
            {
                string str3=Session["DHNumbers"].ToString();
                string[] nums =new string[str3.Length];
                for (int i = 0; i < str3.Length; i++)
                {
                    nums[i]=str3[1].ToString();
                }
                 

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

                        for (int i = numbers.Split(new char[] { ',' }).Length-1; i >= 0; i--)
                        {
                            if(numbers.Split(new char[] { ',' })[i]!="")
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

        string manageId = BLL.CommonClass.CommonDataBLL.getManageID(3);
        string bianhao = this.txtBh.Text.Trim();
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

    private void GetView1()
    {
        string bianhao = this.txtBh.Text.Trim();
        string manageId = BLL.CommonClass.CommonDataBLL.getManageID(3);

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
            if (this.txtBh.Text.Trim() == "")
            {
                Session["jgbh"] = manageId;
            }
            else
            {
                Session["jgbh"] = this.txtBh.Text.Trim();
            }
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
        
        strRes = Jiegou.wltForEng(Convert.ToInt32(DropDownList1.SelectedValue), Convert.ToInt32(Session["jgcw"]), isAnZhi(), Convert.ToString(Session["jgbh"]), "ShowNetworkView.aspx?type=1&bh=");
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
        string asg = Session["jglx"].ToString();
        if (Convert.ToString(Session["jglx"]) == "tj")
        {
            temp = false;
        }
        return temp;
    }

    protected string isAnzhi()
    {
        if (Convert.ToString(Session["jglx"]) == "tj")
        {
            return "0";
        }
        return "1";
    }

    protected string GetManageId()
    {
        if (Session["Company"] != null)
        {
            return Session["Company"].ToString();
        }
        return "";
    }


    protected string isAnzhi2()
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
        //if (Session["jgbh"] == null)
        //{
            Session["jgbh"] = this.txtBh.Text.Trim();
        //}
        Session["xqbh"] = Session["jgbh"];
        if (Session["jglx"].ToString() == "az")
        {
            //Response.Redirect("ShowNetworkViewNew.aspx?SelectGrass=" + DropDownList1.SelectedValue.ToString() + "&isanzhi=1");

            Response.Redirect("SST_AZ.aspx?ExpectNum=" + DropDownList1.SelectedValue);
        }
        else
        {
            //Response.Redirect("ShowNetworkViewNewTj.aspx?SelectGrass=" + DropDownList1.SelectedValue.ToString() + "&isanzhi=0");

            Response.Redirect("SST_TJ.aspx?ExpectNum=" + DropDownList1.SelectedValue);
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