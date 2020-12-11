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
using BLL.other;

public partial class Member_MemberNetMap : BLL.TranslationBase
{
    BLL.CommonClass.CommonDataBLL commonDataBLL = new BLL.CommonClass.CommonDataBLL();
    BLL.Registration_declarations.DetialQueryBLL detialQueryBLL = new BLL.Registration_declarations.DetialQueryBLL();
    BLL.Registration_declarations.RegistermemberBLL registermemberBLL = new BLL.Registration_declarations.RegistermemberBLL();

    protected string PTitle;

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));

        if (!IsPostBack)
        {
            if (Request.QueryString["isAnzhi"] != null)
            {
                if (Request.QueryString["isAnzhi"].ToString() == "az")
                {
                    ViewState["isAnZhi_TuiJian"] = "az";

                    Button2.Visible = true;
                    Button4.Visible = true;

                    PTitle = GetTran("000395", "推荐网络图");
                }
                else
                {
                    ViewState["isAnZhi_TuiJian"] = "tj";
                    Button3.Text = GetTran("000420", "常用");

                    PTitle = GetTran("000366", "安置网络图");
                }
                Session["jglx"] = Request.QueryString["isAnzhi"].ToString();
            }
            else
            {
                if (Session["jglx"] == null)
                {

                    Session["jglx"] = "tj";
                }
            }

            Button1.Attributes["onclick"] = "document.getElementById('txt_PressKeyFlag').value='y';";
            CommonDataBLL.BindQishuList(DropDownList_QiShu, false);

            if (Request.QueryString["SelectGrass"] != null)
            {
                DropDownList_QiShu.SelectedValue = Request.QueryString["SelectGrass"].ToString();
            }
            if (Request.QueryString["net"] != null)
            {
                switch (Request.QueryString["net"].ToString().Trim())
                {
                    case "tj":
                        ViewState["isAnZhi_TuiJian"] = "tj";
                        Button3.Text = GetTran("000420", "常用");
                        break;
                    default:
                        ViewState["isAnZhi_TuiJian"] = "az";
                        break;
                }
            }

            if (Session["jgbh"] == null || Session["jgbh"].ToString() == "")
            {
                Session["jgbh"] = Session["Member"].ToString();
            }

            this.TextBox1.Text = getBH();
            ViewState["bh"] = getBH();

            #region 当前会员，是否有权限访问该网络的会员

            if (WTreeBLL.IsRoot(getBH(), this.DropDownList_QiShu.SelectedValue, GetLoginMember()) == false)
            {
                Response.Write("<script>alert('" + GetTran("000892", "您不能查看该网络") + "');</script>");
                return;
            }

            #endregion

            Session["jgbh"] = TextBox1.Text;

            this.WangLuoTu_Commom(getBH(), ViewState["isAnZhi_TuiJian"].ToString());

            SetDaoHang();
        }

        txt_PressKeyFlag.Text = "n";
        Translations();
    }

    /// <summary>
    /// 网络图导航
    /// </summary>
    private void SetDaoHang()
    {
        DataTable dt = null;
        if (ViewState["isAnZhi_TuiJian"].ToString() == "az")
        {
            dt = WTreeBLL.SetLianLuTu_C(GetLoginMember(), getBH(), DropDownList_QiShu.SelectedValue);
        }
        else
        {
            dt = WTreeBLL.SetLianLuTu_C_II(GetLoginMember(), getBH(), DropDownList_QiShu.SelectedValue);
        }

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            divDH.InnerHtml += "<a href='MemberNetMap.aspx?net=" + ViewState["isAnZhi_TuiJian"].ToString() + "&SelectGrass=" + DropDownList_QiShu.SelectedValue + "&bianhao=" + dt.Rows[i][1].ToString().Split(' ')[0] + "'>" + dt.Rows[i][1].ToString().Split(' ')[0] + "(" + dt.Rows[i][1].ToString().Split(' ')[1] + ")" + "</a> →";
        }
    }

    private void Translations()
    {
        this.TranControls(this.Button1, new string[][] { new string[] { "000434", "确定" } });
        this.TranControls(this.Button5, new string[][] { new string[] { "007308", "伸缩" } });
        Button3.Text = GetTran("000420", "常用") + "(1)";
        Button2.Text = GetTran("000420", "常用") + "(2)";
        Button4.Text = GetTran("000420", "常用") + "(3)";
    }

    protected void Button1_Click(object sender, System.EventArgs e)
    {
        if (this.TextBox1.Text.Trim() == "")
        {
            Response.Write("<script>alert('" + GetTran("007307", "请先填写网络起点编号") + "！');</script>");
            return;
        }
        if (DAL.MemberInfoDAL.SelectMemberExist(this.TextBox1.Text.Trim()) == false)
        {
            Response.Write("<script>alert('" + GetTran("000892", "您不能查看该网络") + "');</script>");
            return;
        }
        bool flag = registermemberBLL.isNet(Session["jglx"].ToString(), Convert.ToString(Session["jgbh"]), getBH());

        if (flag == false)
        {
            Response.Write("<script>alert('" + GetTran("000892", "您不能查看该网络") + "');</script>");
            return;
        }

        if (ViewState["isAnZhi_TuiJian"].ToString() == "az")
        {
            if (WTreeBLL.IsRoot(this.TextBox1.Text.Trim(), this.DropDownList_QiShu.SelectedValue, GetLoginMember()) == false)
            {
                Response.Write("<script>alert('" + GetTran("000892", "您不能查看该网络") + "');</script>");
                return;
            }
        }
        else
        {
            if (WTreeBLL.IsRoot_II(this.TextBox1.Text.Trim(), this.DropDownList_QiShu.SelectedValue, GetLoginMember()) == false)
            {
                Response.Write("<script>alert('" + GetTran("000892", "您不能查看该网络") + "');</script>");
                return;
            }
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

        SetDaoHang();
    }

    protected string GetLoginMember()
    {
        return Session["Member"].ToString();
    }

    protected void WangLuoTu_Commom(string TopBianhao, string isAnZhi_TuiJian)
    {
        //判断是否合法。
        if (DAL.MemberInfoDAL.SelectMemberExist(ViewState["bh"].ToString()) == false)
        {
            Response.Write("<script>alert('" + GetTran("000892", "您不能查看该网络") + "');</script>");
            return;
        }

        if (TopBianhao != ViewState["bh"].ToString())
        {
            string fatherBh = ViewState["bh"].ToString();
            string sonBh = TopBianhao;
            bool flag = registermemberBLL.isNet(ViewState["isAnZhi_TuiJian"].ToString(), fatherBh, sonBh);
            if (!flag)
            {
                Response.Write("<script>alert('" + GetTran("000892", "您不能查看该网络") + "');</script>");
                return;
            }
        }

        if (isAnZhi_TuiJian == "az")
        {
            Divt1.InnerHtml = JieGouNew2.Direct_Table_New(getBH(), Convert.ToInt32(this.DropDownList_QiShu.SelectedValue), 1);

        }
        else
        {
            Divt1.InnerHtml = JieGouNew2.Direct_Table_New(getBH(), Convert.ToInt32(this.DropDownList_QiShu.SelectedValue), 2);
        }
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
            bh = TextBox1.Text;
            if (bh.Length == 0)
            {
                bh = Convert.ToString(Session["Member"]);
            }
            else
            {
                bh = TextBox1.Text;
            }
        }
        else
        {
            if (Request.QueryString["bianhao"].ToString() == "")
            {
                bh = TextBox1.Text;
                if (bh.Length == 0)
                {
                    bh = Convert.ToString(Session["Member"]);
                }
                else
                {
                    bh = TextBox1.Text;
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

    protected void Button4_Click(object sender, EventArgs e)
    {
        Session["jgbh"] = this.TextBox1.Text.Trim().ToString();

        if (Session["jglx"] == null)
        {
            Session["jglx"] = "tj";
        }
        if (Session["jglx"].ToString() == "az")
        {
            Response.Redirect("SST_AZ.aspx?ExpectNum=" + DropDownList_QiShu.SelectedValue);
        }
        else
        {
            Response.Redirect("SST_TJ.aspx?ExpectNum=" + DropDownList_QiShu.SelectedValue);
        }
    }
}