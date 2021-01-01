using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Standard.Classes;
using BLL.CommonClass;
using DAL;
using System.Collections.Generic;
using System.Text;
using BLL.other;
public partial class Company_MemberNetMap : BLL.TranslationBase
{
    BLL.CommonClass.CommonDataBLL commonDataBLL = new BLL.CommonClass.CommonDataBLL();
    BLL.Registration_declarations.RegistermemberBLL registermemberBLL = new BLL.Registration_declarations.RegistermemberBLL();

    protected void Page_Load(object sender, System.EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        // 在此处放置用户代码以初始化页面
        Response.Cache.SetExpires(DateTime.Now);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        DataTable dt = Jiegou.ShowTopNumber("9999999999", isAnZhi());
        TextBox1.Text = "9999999999";
        string firstky = Request.QueryString["EndNumber"] + "";
        bool isQX = true;
        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    if (i == 0)
        //    {
        //        if (firstky == "")
        //            firstky = dt.Rows[i]["number"].ToString();
        //    }

        //    if (firstky == dt.Rows[i]["number"].ToString())
        //    {
        //        isQX = true;
        //    }

            
        //}
        ViewState["ky"] = firstky;

        if (!isQX)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("007461", "您没有权限查看！") + "')</script>");
            //Server.Transfer("first.aspx");
            return;

        }
        if (!IsPostBack)
        {
            if (Request.QueryString["isAnzhi"] != null)
            {
                if (Request.QueryString["isAnzhi"].ToString() == "az")
                {

                    ViewState["isAnZhi_TuiJian"] = "az";
                    Session["jglx"] = "az";

                    Button5.Visible = true;
                    Button6.Visible = true;
                }
                else
                {
                    Button2.Text = GetTran("000420", "常用");
                    ViewState["isAnZhi_TuiJian"] = "tj";
                    Session["jglx"] = "tj";
                }
            }
            else
            {
                if (Session["jglx"] == null)
                {

                    Session["jglx"] = "tj";
                }

            }

            ShowNumber();
            Button1.Attributes["onclick"] = "document.getElementById('txt_PressKeyFlag').value='y';";
            CommonDataBLL.BindQishuList(this.DropDownList_QiShu, false);
            Session["jgbh"] = getBH();

            if (Request.QueryString["net"] != null)
            {
                switch (Request.QueryString["net"].ToString().Trim())
                {
                    case "tj":
                        ViewState["isAnZhi_TuiJian"] = "tj";
                        Button2.Text = GetTran("000420", "常用");
                        break;
                    default:
                        ViewState["isAnZhi_TuiJian"] = "az";
                        break;
                }
            }
            //else
            //{
            //    ViewState["isAnZhi_TuiJian"] = "az";
            //}

            string asg = Request.QueryString["bianhao"] == null ? "" : Request.QueryString["bianhao"].ToString();
            if (asg != "")
            {
                int count = BLL.CommonClass.CommonDataBLL.getCountNumber(asg);
                if (count == 0)
                {
                    
                    Response.Write("<script>alert('该会员编号不存在！');</script>");
                    return;
                }
                else
                {
                this.TextBox1.Text = asg;
                ViewState["bh"] = asg;
                this.DropDownList_QiShu.SelectedValue = Request.QueryString["SelectGrass"].ToString();
                Session["jglx"] = ViewState["isAnZhi_TuiJian"].ToString();

                this.WangLuoTu_Commom(asg, ViewState["isAnZhi_TuiJian"].ToString());
                }
            }
            else
            {
                
                if (dt.Rows.Count > 0)
                {
                    this.TextBox1.Text = dt.Rows[0]["number"].ToString();
                    ViewState["bh"] = TextBox1.Text;
                    this.DropDownList_QiShu.SelectedValue = CommonDataBLL.getMaxqishu().ToString();
                    int azTjFlag = 1;
                    azTjFlag = ViewState["isAnZhi_TuiJian"].ToString() == "tj" ? 2 : 1;
                    Divt1.InnerHtml = JieGouNew2.Direct_Table_New(ViewState["bh"].ToString(), Convert.ToInt32(this.DropDownList_QiShu.SelectedValue), azTjFlag);
                }
            }

            SetDaoHang(firstky);

        }
        txt_PressKeyFlag.Text = "n";
        Translations();
        Button1_Click(null, null);
    }

    /// <summary>
    /// 网络图导航
    /// </summary>
    private void SetDaoHang(string firstky)
    {
        /* if (Session["jglx"] == null)
         {
             Session["jglx"] = "tj";
         }
         if (TextBox1.Text != null && TextBox1.Text != "")
         {
             divDH.InnerHtml = GetTran("007032", "链路图") + "：";
             if (Session["DHNumbers"] == null)
             {
                 Session["DHNumbers"] = new string[2] { TextBox1.Text, "" };
                 divDH.InnerHtml += "<a href='MemberNetMap.aspx?net=" + ViewState["isAnZhi_TuiJian"].ToString() + "&SelectGrass=" + DropDownList_QiShu.SelectedValue + "&bianhao=" + TextBox1.Text + "'>" + CommonDataBLL.GetPetNameByNumber(TextBox1.Text) + "</a> →";
             }
             else
             {
                 string[] nums = Session["DHNumbers"] as string[];

                 if (nums[0] != TextBox1.Text)
                 {
                     if (nums[1] != TextBox1.Text)
                     {
                         nums[1] = TextBox1.Text;
                     }

                     IList<string> lists = Jiegou.GetNumberForTop(nums[0], Convert.ToInt32(DropDownList_QiShu.SelectedItem.Value), Session["jglx"].ToString() == "az");
                     int count = 0;
                     foreach (string str in lists)
                     {
                         if (nums[1] == str)
                             count++;
                     }

                     if (count == 0)
                         divDH.InnerHtml += "<a href='MemberNetMap.aspx?net=" + ViewState["isAnZhi_TuiJian"].ToString() + "&SelectGrass=" + DropDownList_QiShu.SelectedValue + "&bianhao=" + nums[1] + "'>" + CommonDataBLL.GetPetNameByNumber(nums[1]) + "</a> →";
                     else
                     {
                         string highNum = nums[1];
                         string numbers = "";
                         do
                         {
                             numbers += highNum + ",";
                             highNum = Jiegou.GetHighNumber(highNum, Session["jglx"].ToString() == "az");
                         } while (highNum != nums[0] && highNum != "8888888888" && highNum != "1111111111");
                         numbers += nums[0] + ",";

                         for (int i = numbers.Split(new char[] { ',' }).Length - 1; i >= 0; i--)
                         {
                             if (numbers.Split(new char[] { ',' })[i] != "")
                                 divDH.InnerHtml += "<a href='MemberNetMap.aspx?net=" + ViewState["isAnZhi_TuiJian"].ToString() + "&SelectGrass=" + DropDownList_QiShu.SelectedValue + "&bianhao=" + numbers.Split(new char[] { ',' })[i] + "'>" + CommonDataBLL.GetPetNameByNumber(numbers.Split(new char[] { ',' })[i]) + "</a> →";
                         }

                     }
                 }
                 else
                     divDH.InnerHtml += "<a href='MemberNetMap.aspx?net=" + ViewState["isAnZhi_TuiJian"].ToString() + "&SelectGrass=" + DropDownList_QiShu.SelectedValue + "&bianhao=" + nums[0] + "'>" + CommonDataBLL.GetPetNameByNumber(nums[0]) + "</a> →";

                 Session["DHNumbers"] = nums;
             }
         }
         else
         {
             Session["DHNumbers"] = "";
             Session["DHNumbers"] = null;
         }*/

        DataTable dt = null;
        HiddenField1.Value = Request.QueryString["EndNumber"] + "";
        if (HiddenField1.Value == "")
        {
            HiddenField1.Value =firstky;
        }
        if (ViewState["isAnZhi_TuiJian"].ToString() == "az")
        {
            dt = WTreeBLL.SetLianLuTu_C(HiddenField1.Value == "" ? SfType.getBH() : HiddenField1.Value, TextBox1.Text, DropDownList_QiShu.SelectedValue);
        }
        else
        {
            dt = WTreeBLL.SetLianLuTu_C_II(HiddenField1.Value == "" ? SfType.getBH() : HiddenField1.Value, TextBox1.Text, DropDownList_QiShu.SelectedValue);
        }
        divDH.InnerHtml = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            divDH.InnerHtml += "<a href='MemberNetMap.aspx?EndNumber=&net=" + ViewState["isAnZhi_TuiJian"].ToString() + "&SelectGrass=" + DropDownList_QiShu.SelectedValue + "&bianhao=" + dt.Rows[i][1].ToString().Split(' ')[0] + "'>" + dt.Rows[i][1].ToString().Split(' ')[0] + "(" + dt.Rows[i][1].ToString().Split(' ')[0] + ")" + "</a> →";
        }
    }

    private void ShowNumber()
    {
        this.Repeater2.DataSource = Jiegou.ShowNumber(Session["Company"].ToString(), isAnZhi());
        this.Repeater2.DataBind();
    }

    private bool isAnZhi()
    {
        bool temp = false;
        if (Convert.ToString(ViewState["isAnZhi_TuiJian"]) == "tj")
        {
            temp = true;
        }
        return temp;
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

    private void Translations()
    {
        this.TranControls(this.Button1, new string[][] { new string[] { "000844", "确定" } });
        //this.Button2.Text = GetTran("000420", "常用");
        this.Button4.Text = GetTran("007308", "伸缩");
        this.TranControls(this.Button1, new string[][] { new string[] { "000434", "确定" } });
        this.Button2.Text = GetTran("000420", "常用") + "(1)";
        this.Button5.Text = GetTran("000420", "常用") + "(2)";
        this.Button6.Text = GetTran("000420", "常用") + "(3)";
    }

    #region Web 窗体设计器生成的代码
    override protected void OnInit(EventArgs e)
    {
        //
        // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
        //
        InitializeComponent();
        base.OnInit(e);
    }

    /// <summary>
    /// 设计器支持所需的方法 - 不要使用代码编辑器修改
    /// 此方法的内容。
    /// </summary>
    private void InitializeComponent()
    {

    }
    #endregion

    protected void lkn_submit_Click(object sender, EventArgs e)
    {
        Button1_Click(null, null);
    }

    protected void Button1_Click(object sender, System.EventArgs e)
    {
        if (this.TextBox1.Text.Trim() == "")
        {
            Response.Write("<script>alert('请先填写网络起点编号！');</script>");
            return;
        }

        if (Convert.ToInt32(DBHelper.ExecuteScalar("select count(1) from MemberInfo where Number=@Number and ExpectNum<=@ExpectNum", new SqlParameter[] { new SqlParameter("@Number", TextBox1.Text.Trim()), new SqlParameter("@ExpectNum", DropDownList_QiShu.SelectedValue) }, CommandType.Text)) == 0)
        {
            TextBox1.Text = Session["Company"].ToString();
            Response.Write("<script>alert('该期数中没有这个会员！');</script>");
            return;
        }

        #region 判断查看网络图权限

        string tj = "";
        if (isAnZhi())
        {
            tj = " type=1 ";
        }
        else
        {
            tj = " type=0 ";
        }

        //DataTable dt = DAL.DBHelper.ExecuteDataTable("Select  number From ViewManage Where ManageId = '" + Session["Company"].ToString() + "' and " + tj);
        //int flagCount = 0;
        //foreach (DataRow row in dt.Rows)
        //{
        //    if (row["number"].ToString() == TextBox1.Text)
        //    {
        //        flagCount = 0;
        //        break;
        //    }
        //    if (!registermemberBLL.isNet(ViewState["isAnZhi_TuiJian"].ToString(), row["number"].ToString(), TextBox1.Text))
        //    {
        //        flagCount++;
        //    }
        //}
        //if (flagCount > 0)
        //{
        //    Response.Write("<script>alert('" + GetTran("000892", "您不能查看该网络") + "');</script>");
        //    return;
        //}

        #endregion

        int count = BLL.CommonClass.CommonDataBLL.getCountNumber(this.TextBox1.Text.Trim());
        if (count == 0)
        {
            TextBox1.Text = Session["Company"].ToString();
            Response.Write("<script>alert('该会员编号不存在！');</script>");
            return;
        }
        if (ViewState["isAnZhi_TuiJian"].ToString() == "az")
        {
            if (this.TextBox1.Text.Trim() != "")
            {
                Divt1.InnerHtml = JieGouNew2.Direct_Table_New(this.TextBox1.Text.Trim(), Convert.ToInt32(this.DropDownList_QiShu.SelectedValue), 1);
            }
            else
            {
                Divt1.InnerHtml = JieGouNew2.Direct_Table_New(ViewState["bh"].ToString(), Convert.ToInt32(this.DropDownList_QiShu.SelectedValue), 1);
            }
        }
        else
        {
            if (this.TextBox1.Text.Trim() != "")
            {
                Divt1.InnerHtml = JieGouNew2.Direct_Table_New(this.TextBox1.Text.Trim(), Convert.ToInt32(this.DropDownList_QiShu.SelectedValue), 2);
            }
            else
            {
                Divt1.InnerHtml = JieGouNew2.Direct_Table_New(ViewState["bh"].ToString(), Convert.ToInt32(this.DropDownList_QiShu.SelectedValue), 2);
            }
        }
        string icc = ViewState["ky"].ToString();
        SetDaoHang(icc);
    }

    protected void WangLuoTu_Commom(string TopBianhao, string isAnZhi_TuiJian)
    {
        //判断是否合法。
        if (TopBianhao != ViewState["bh"].ToString())
        {
            string fatherBh = ViewState["bh"].ToString();
            string sonBh = TopBianhao;
            bool flag = registermemberBLL.isNet(ViewState["isAnZhi_TuiJian"].ToString(), fatherBh, TextBox1.Text);
            if (!flag)
            {
                Response.Write("<script>alert('" + GetTran("000892", "您不能查看该网络") + "');</script>");
                return;
            }
        }

        if (isAnZhi_TuiJian == "az")
        {
            Divt1.InnerHtml = JieGouNew2.Direct_Table_New(Request.QueryString["bianhao"].ToString(), Convert.ToInt32(this.DropDownList_QiShu.SelectedValue), 1);

        }
        else
        {
            Divt1.InnerHtml = JieGouNew2.Direct_Table_New(Request.QueryString["bianhao"].ToString(), Convert.ToInt32(this.DropDownList_QiShu.SelectedValue), 2);
        }
        string icc = ViewState["ky"].ToString();

        SetDaoHang(icc);
    }

    /// <summary>
    /// 取得要查询网络的起点编号
    /// </summary>
    /// <param name="isDian">是否是店调用</param>
    /// <returns>起点编号</returns>
    private string getBH()
    {
        string bh = TextBox1.Text;
        if (bh.Length == 0)
        {
            bh = SfType.getBH();
        }
        else
        {
            bh = TextBox1.Text;
        }

        return bh;
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        Session["jgbh"] = this.TextBox1.Text.Trim().ToString();

        Session["xqbh"] = Session["jgbh"];
        Response.Redirect("ShowNetworkBiaoGeView.aspx?SelectGrass=" + DropDownList_QiShu.SelectedValue.ToString());
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Session["jgbh"] = this.TextBox1.Text.Trim().ToString();


        if (Session["jglx"] == null)
        {
            Session["jglx"] = "tj";
        }
        if (Session["jglx"].ToString() == "az")
        {
            //Response.Redirect("ShowNetworkViewNew.aspx?SelectGrass=" + 
            //this.DropDownList_QiShu.SelectedValue.ToString() + "&isanzhi=1");
            Response.Redirect("SST_AZ.aspx?ExpectNum=" + DropDownList_QiShu.SelectedValue);
        }
        else
        {
            //Response.Redirect("ShowNetworkViewNewTj.aspx?SelectGrass=" + 
            //DropDownList_QiShu.SelectedValue.ToString() + "&isanzhi=0");
            Response.Redirect("SST_TJ.aspx?ExpectNum=" + DropDownList_QiShu.SelectedValue);
        }
    }

    protected void btnZk_Click(object sender, EventArgs e)
    {
        Response.Redirect("ShowNetworkView.aspx?bh=" + this.TextBox1.Text.Trim().ToString() + "&SelectGrass=" + DropDownList_QiShu.SelectedIndex.ToString());
    }
}