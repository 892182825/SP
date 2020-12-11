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
using Model.Other;
using BLL.CommonClass;
using System.Collections.Generic;
//using BLL.CommonClass;

public partial class Company_ShowView1 : BLL.TranslationBase
{
    BLL.Registration_declarations.RegistermemberBLL registermemberBLL = new BLL.Registration_declarations.RegistermemberBLL();
    TreeViewBLL treeViewBLL = new TreeViewBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);

        if (Request.QueryString["bh"] != null)
        {
            Session["jgbh"] = Request.QueryString["bh"];

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

            // if (!Jiegou.isValid(Convert.ToString(Session["jgbh"]), getMemberBH(), isAnZhi(), Convert.ToInt32(Session["jgqs"])))
            if (flag == false)
            {
                Response.Write(GetTran("000892", "您不能查看该网络"));
                return;
            }
        }
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
        Session["jgqs"] = this.dropdownlist_qishu.SelectedValue;

        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        if (!Page.IsPostBack)
        {
            BLL.CommonClass.CommonDataBLL.BindQishuList(dropdownlist_qishu, false);
            ShowNumber();

            if (Request.QueryString["cengshu"] == null)
            {
                if (Session["jgcw"] == null)
                {
                    this.txtceng.Text = "3";
                }
                else
                {
                    this.txtceng.Text = Session["jgcw"].ToString();
                }
            }
            else
            {
                this.txtceng.Text = Request.QueryString["cengshu"].ToString();
            }
            if (Session["jgqs"] == null || Session["jglx"] == null)
            {
                Response.Write(GetTran("000894", "调用错误"));
                Response.End();
            }

            if (Request.QueryString["SelectGrass"] != null)
            {
                dropdownlist_qishu.SelectedIndex = Convert.ToInt32(Request.QueryString["SelectGrass"].ToString());
            }
            if (Request.QueryString["isanzhi"] != null)
            {
                ViewState["isanzhi"] = Request.QueryString["isanzhi"];
            }

            if (Request.QueryString["bh"] != null)
            {
                Session["jgbh"] = Request.QueryString["bh"];
            }
            else
            {
                if (this.txtbianhao.Text.Trim() == "")
                {
                    Session["jgbh"] = "8888888888";
                }
                else
                {
                    Session["jgbh"] = this.txtbianhao.Text.Trim();
                }
            }

            if (Session["jgbh"] != null)
            {
                if (Session["jgbh"].ToString() != "")
                {
                    this.txtbianhao.Text = Session["jgbh"].ToString();
                    int cengshu = 0;
                    if (this.txtceng.Text.Trim() == "")
                        cengshu = 3;
                    else
                    {
                        try
                        {
                            cengshu = Convert.ToInt32(this.txtceng.Text.Trim());
                        }
                        catch
                        {
                            Response.Write("<script>alert('" + GetTran("000897", "显示层数输入格式错误") + "！');</script>");
                            return;
                        }
                    }
                    this.WangLuoTu(Session["jgbh"].ToString(), "",1, cengshu, Convert.ToInt32(dropdownlist_qishu.SelectedValue), "");

                    //
                    ViewState["qs"] = dropdownlist_qishu.SelectedValue;
                    ViewState["cengshu"] = cengshu;
                    ViewState["startbh"] = txtbianhao.Text.Trim();
                    //end
                }
            }

            //------------------------
            SetDaoHang();
        }
        Translations();


    }

    /// <summary>
    /// 网络图导航
    /// </summary>
    private void SetDaoHang()
    {
        if (txtbianhao.Text != null && txtbianhao.Text != "")
        {
            divDH.InnerHtml = GetTran("007032", "链路图") + "：";

            if (Session["DHNumbers"] == null)
            {
                Session["DHNumbers"] = new string[2] { txtbianhao.Text, "" };
                divDH.InnerHtml += "<a href='ShowNetworkViewNew.aspx?cengshu=" + txtceng.Text + "&SelectGrass=" + dropdownlist_qishu.SelectedValue + "&bh=" + txtbianhao.Text + "&isanzhi=1'>" + CommonDataBLL.GetPetNameByNumber(txtbianhao.Text) + "</a> →";
            }
            else
            {
                string[] nums = Session["DHNumbers"] as string[];

                if (nums[0] != txtbianhao.Text)
                {
                    if (nums[1] != txtbianhao.Text)
                    {
                        nums[1] = txtbianhao.Text;
                    }

                    IList<string> lists = Jiegou.GetNumberForTop(nums[0], Convert.ToInt32(dropdownlist_qishu.SelectedValue), Session["jglx"].ToString() == "az");
                    int count = 0;
                    foreach (string str in lists)
                    {
                        if (nums[1] == str)
                            count++;
                    }

                    if (count == 0)
                        divDH.InnerHtml += "<a href='ShowNetworkViewNew.aspx?cengshu=" + txtceng.Text + "&SelectGrass=" + dropdownlist_qishu.SelectedValue + "&bh=" + nums[1] + "&isanzhi=1'>" + CommonDataBLL.GetPetNameByNumber(nums[1]) + "</a> →";
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
                                divDH.InnerHtml += "<a href='ShowNetworkViewNew.aspx?cengshu=" + txtceng.Text + "&SelectGrass=" + dropdownlist_qishu.SelectedValue + "&bh=" + numbers.Split(new char[] { ',' })[i] + "&isanzhi=1'>" + CommonDataBLL.GetPetNameByNumber(numbers.Split(new char[] { ',' })[i]) + "</a> →";
                        }

                    }
                }
                else
                    divDH.InnerHtml += "<a href='ShowNetworkViewNew.aspx?cengshu=" + txtceng.Text + "&SelectGrass=" + dropdownlist_qishu.SelectedValue + "&bh=" + nums[0] + "&isanzhi=1'>" + CommonDataBLL.GetPetNameByNumber(nums[0]) + "</a> →";

                Session["DHNumbers"] = nums;
            }

        }
        else
        {
            Session["DHNumbers"] = "";
            Session["DHNumbers"] = null;
        }
    }

    private void ShowNumber()
    {
        this.Repeater2.DataSource = Jiegou.ShowNumber(Session["Company"].ToString(), isAnZhi());
        this.Repeater2.DataBind();
    }

    protected string GetManageId()
    {
        if (Session["Company"] != null)
        {
            return Session["Company"].ToString();
        }
        return "";
    }


    protected string GetCengshu()
    {
        int cengshu = 0;
        if (this.txtceng.Text.Trim() == "")
            cengshu = 3;
        else
        {
            try
            {
                cengshu = Convert.ToInt32(this.txtceng.Text.Trim());
            }
            catch
            {
                cengshu = 3;
            }
        }
        return cengshu.ToString();
    }

    private void Translations()
    {
        this.TranControls(this.button1, new string[][] { new string[] { "000844", "显示" } });
        this.TranControls(this.button2, new string[][] { new string[] { "000857", "回到顶部" } });
    }

    /// <param name="bianhao">会员编号</param>
    /// <param name="tree"></param>
    /// <param name="state">是否安置状态</param>
    /// <param name="cengshu">层数</param>
    /// <param name="qishu">期数</param>
    /// <param name="storeid">店id</param>
    protected void WangLuoTu(string bianhao, string tree, int state, int cengshu, int qishu, string storeid)
    {
        //获得存储过程产生的树
        int type = 0;//公司查看网络图
        DataTable table = treeViewBLL.GetExtendTreeView_NewAz1(bianhao, tree, cengshu, qishu, type, 0);
        string str = "";
        //循环拼出树
        str = "<table id='tab_tr' border=\"0\" cellspacing=\"0\" cellpadding=\"0\"  class='tree_grid'>";
        str += "<tr>"
               + "<th align='center'>会员编号</th>"
               + "<th align='center' >层数</th>"
             //  + "<th align='center'>级别</th>"
               + "<th align='right'>新个分数</th>"
               + "<th align='right'>新网分数</th>"
               + "<th align='right'>新网人数</th>"
               + "<th align='right' >总网人数</th>"
               + "<th align='right' >总网分数</th>"

               + "</tr>";
        int count = 0;
        foreach (DataRow row in table.Rows)
        {
            count++;

            string strStyle = "";
            if (count % 2 == 0)
            {
                strStyle = "background-color:#F1F4F8";//rgb(243,243,243)";
            }
            else
            {
                strStyle = " background-color:#FAFAFA";
            }
            str += "<tr style='" + strStyle + "'  class=\"tr\"  id='tr" + row["ID"].ToString() + "' onmouseover=\"overIt(this)\" onmouseout=\"outIt(this)\" onmousedown=\"down_tw(event,this)\">";
            str += "<td valing='middle'>" + row[0].ToString().Replace("─", "<img src='../images/011.gif'  align=absmiddle  border=0 />").Replace("☆", "<img src='../images/013.gif'  align=absmiddle  border=0 />").Replace("★", "<img src='../images/014.gif'  align=absmiddle  border=0 />").Replace("├", "<img src='../images/006.gif'  align=absmiddle  border=0 />").Replace("└", "<img src='../images/003.gif'  align=absmiddle  border=0 />").Replace("~", "<img src='../images/015.gif'  align=absmiddle  border=0 />").Replace("|", "<img src='../images/004.gif'  align=absmiddle  border=0 />") + "<img src='../images/1.png' class='img' align=absmiddle border=0 /></td>";
          //  str += "<td align='center' valing='middle' title='层数'>" + row[10].ToString() + "</td>";
            str += "<td align='left' valing='middle' title='级别'>" + row["Level"].ToString() + "</td>";
            str += "<td align='right' valing='middle' title='新个分数'>" + row["CurrentOneMark"].ToString() + "</td>";
            str += "<td align='right' valing='middle' title='新网分数'>" + row["CurrentTotalNetRecord"].ToString() + "</td>";
            str += "<td align='right' valing='middle' title='新网人数'>" + row["CurrentNewNetNum"].ToString() + "</td>";
            str += "<td align='right' valing='middle' title='总网人数'>" + row["TotalNetNum"].ToString() + "</td>";
            str += "<td align='right' valing='middle' title='总网分数'>" + row["TotalNetRecord"].ToString() + "</td>";

            str += "</tr>";
        }
        str += "</table>";
        this.statr0.InnerHtml = str;
        //this.statr0.InnerHtml = str.Replace("＿", "<img src='../images/02.gif' />").Replace("~", "<img src='../images/03.gif' />").Replace("|", "<img src='../images/04.gif' />");

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

    protected void lkn_submit1_Click(object sender, EventArgs e)
    {
        button1_Click(null, null);
    }

    protected void button1_Click(object sender, EventArgs e)
    {
        int cengshu = 0;
        string bianhao = "";
        if (this.txtbianhao.Text.Trim() == "")
        {
            Response.Write("<script>alert('请先填写网络起点ID！');</script>");
            return;
        }
        else
            bianhao = this.txtbianhao.Text.Trim();

        if (this.txtceng.Text.Trim() == "")
            cengshu = 3;
        else
        {
            try
            {
                cengshu = Convert.ToInt32(this.txtceng.Text.Trim());
            }
            catch
            {
                Response.Write("<script>alert('" + GetTran("000897", "显示层数输入格式错误") + "！');</script>");
                return;
            }
        }

        ViewState["qs"] = dropdownlist_qishu.SelectedValue;
        ViewState["cengshu"] = cengshu;
        ViewState["startbh"] = txtbianhao.Text.Trim();

        this.WangLuoTu(bianhao, "",1, cengshu, Convert.ToInt32(dropdownlist_qishu.SelectedValue), "");
    }
    protected void button2_Click(object sender, EventArgs e)
    {

    }

    protected void btnCY_Click(object sender, EventArgs e)
    {
        Response.Redirect("MemberNetMap.aspx?net=" + Session["jglx"] + "&bianhao=" + this.txtbianhao.Text.Trim() + "&SelectGrass=" + this.dropdownlist_qishu.SelectedValue);
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Session["jgbh"] = this.txtbianhao.Text.Trim();

        Session["xqbh"] = Session["jgbh"];
        Response.Redirect("ShowNetworkBiaoGeView.aspx?SelectGrass=" + dropdownlist_qishu.SelectedValue.ToString());

    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Response.Redirect("ShowNetworkView.aspx?bh=" + this.txtbianhao.Text.Trim() + "&SelectGrass=" + dropdownlist_qishu.SelectedValue.ToString());
    }

    protected void Button7_Click(object sender, EventArgs e)
    {
        Session["jgbh"] = this.txtbianhao.Text.Trim();
        Session["jglx"] = "tj";
        Session["xqbh"] = Session["jgbh"];
        Response.Redirect("ShowNetworkViewNewTj.aspx?cengshu=" + this.txtceng.Text.Trim() + "&SelectGrass=" + dropdownlist_qishu.SelectedValue.ToString() + "&isanzhi=0");
    }

    protected void Button8_Click(object sender, EventArgs e)
    {

    }
}
