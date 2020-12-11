using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using BLL.other;

public partial class LLT_AZ : BLL.TranslationBase
{
    protected string StartNumber = "";
    protected string EndNumber = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);

        EndNumber = Request.QueryString["endNumber"] + "";
        if (EndNumber == "")
            EndNumber = SfType.getBH().ToString();

        StartNumber = Request.QueryString["ThNumber"];

        #region 当前会员，是否有权限访问该网络的会员

        if (EndNumber != Session["Member"].ToString())
        {
            Response.Write("<script>alert('" + GetTran("000892", "您不能查看该网络") + "');window.location.href='First.aspx';</script>");
            return;
        }

        if (WTreeBLL.IsRoot(StartNumber, WTreeBLL.GetMaxQS(), Session["Member"].ToString()) == false)
        {
            Response.Write("<script>alert('" + GetTran("000892", "您不能查看该网络") + "');window.location.href='First.aspx';</script>");
            return;
        }

        #endregion

        if (!IsPostBack)
        {
            BindQS();

            BindData();
            Translations();
        }
    }

    //判断是否有权限查看该网咯
    public bool IsRoot(string StartNumber, string qs, string EndNumber)
    {
        return WTreeBLL.IsRoot(StartNumber, qs, EndNumber);
    }

    public void BindData()
    {
        string qs = DDLQs.SelectedValue;

        if (IsRoot(StartNumber, qs, SfType.getBH().ToString()) == false)
        {
            string maxqs = WTreeBLL.GetMaxQS();
            string ts = GetTran("007315", "您没有权限") + "！";

            if (IsRoot(StartNumber, maxqs, SfType.getBH().ToString()))
            {
                ts = GetTran("007319", "您查看的这期中没有这个会员，所以不能查看该网路") + "！";
            }

            ClientScript.RegisterStartupScript(this.GetType(), "", "<script type='text/javascript'>alert('" + ts + "');window.history.go(-1);</script>");
            return;
        }

        object[] obj = WTreeBLL.GetLLTTree(qs, EndNumber, StartNumber, "3", "1");

        SqlDataReader dr = (SqlDataReader)obj[0];

        while (dr.Read())
        {
            GridView1.Columns[0].HeaderText = GetTran("007323", "安置结构");
            if (dr["Field"].ToString() == "PetName")
            {
                GridView1.Columns[1].HeaderText = GetTran(dr["FieldName"].ToString(), "");

                if (dr["IsVisible"].ToString() == "0")
                    GridView1.Columns[1].Visible = false;
            }

            if (dr["Field"].ToString() == "DaiShu")
            {
                GridView1.Columns[4].HeaderText = GetTran(dr["FieldName"].ToString(), "");

                if (dr["IsVisible"].ToString() == "0")
                    GridView1.Columns[4].Visible = false;
            }
            else if (dr["Field"].ToString() == "TJ")
            {
                GridView1.Columns[2].HeaderText = GetTran(dr["FieldName"].ToString(), "");

                if (dr["IsVisible"].ToString() == "0")
                    GridView1.Columns[2].Visible = false;
            }
            else if (dr["Field"].ToString() == "JiBie")
            {
                GridView1.Columns[3].HeaderText = GetTran(dr["FieldName"].ToString(), "");

                if (dr["IsVisible"].ToString() == "0")
                    GridView1.Columns[3].Visible = false;
            }
            else if (dr["Field"].ToString() == "XinGe")
            {
                GridView1.Columns[6].HeaderText = GetTran(dr["FieldName"].ToString(), "");

                if (dr["IsVisible"].ToString() == "0")
                    GridView1.Columns[6].Visible = false;
            }
            else if (dr["Field"].ToString() == "XinWang")
            {
                GridView1.Columns[7].HeaderText = GetTran(dr["FieldName"].ToString(), "");

                if (dr["IsVisible"].ToString() == "0")
                    GridView1.Columns[7].Visible = false;
            }

            else if (dr["Field"].ToString() == "XinRen")
            {
                GridView1.Columns[8].HeaderText = GetTran(dr["FieldName"].ToString(), "");

                if (dr["IsVisible"].ToString() == "0")
                    GridView1.Columns[8].Visible = false;
            }
            else if (dr["Field"].ToString() == "ZongRen")
            {
                GridView1.Columns[9].HeaderText = GetTran(dr["FieldName"].ToString(), "");

                if (dr["IsVisible"].ToString() == "0")
                    GridView1.Columns[9].Visible = false;
            }
            else if (dr["Field"].ToString() == "ZongFen")
            {
                GridView1.Columns[10].HeaderText = GetTran(dr["FieldName"].ToString(), "");

                if (dr["IsVisible"].ToString() == "0")
                    GridView1.Columns[10].Visible = false;
            }

        }
        dr.Close();
        dr.Dispose();

        GridView1.DataSource = (DataTable)obj[1];
        GridView1.DataBind();

        SetLianLuTu(StartNumber, Session["M_L_AZ"].ToString(), qs, SfType.getBH());
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        BindData();
    }

    public string GetAzOrTz(string direct, string placement, string isAz)
    {
        if (isAz == "1")
        {
            return direct == placement ? "" : placement;//安置
        }
        else
        {
            return direct == placement ? "" : direct;
        }
    }

    public void BindQS()
    {
        DataTable dt = WTreeBLL.BindQS();

        DDLQs.DataSource = dt;
        DDLQs.DataTextField = "ExpectNum";
        DDLQs.DataValueField = "ExpectNum";
        DDLQs.DataBind();

        DDLQs.SelectedValue = Request.QueryString["qs"];
    }

    public void SetLianLuTu(string EndNumber, string _StartNumber, string Qs, string ysEndNumber)
    {
        LitLLT.Text = "";

        LitLLT.Text = WTreeBLL.SetLianLuTu_L(EndNumber, _StartNumber, Qs, ysEndNumber);
    }
    private void Translations()
    {
        this.TranControls(this.Button1, new string[][] { new string[] { "000177", "显示" } });
    }
}