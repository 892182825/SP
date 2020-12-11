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
using Model;
using BLL.CommonClass;
using System.Collections.Generic;

public partial class LLT_AZ : BLL.TranslationBase
{
    public string StartNumber = "";
    public string EndNumber = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);

        //可查看的网路
        //DataTable ky = WTreeBLL.GetKYWL(Session["Company"].ToString(), "1");

        //string firstky = Request.QueryString["EndNumber"] + "";
        //bool isQX = false;
        //for (int i = 0; i < ky.Rows.Count; i++)
        //{
        //    if (i == 0)
        //    {
        //        if (firstky == "")
        //            firstky = ky.Rows[i]["number"].ToString();
        //    }

        //    if (firstky == ky.Rows[i]["number"].ToString())
        //    {
        //        isQX = true;
        //    }

        //    LitMaxWl.Text = LitMaxWl.Text + "<a href='SST_AZ.aspx?EndNumber=" + ky.Rows[i]["number"].ToString() + "' style='color:gray;font-weight:" + (firstky == ky.Rows[i]["number"].ToString() ? "bold" : "") + "'>" + ky.Rows[i]["number"] + "</a> / ";
        //}

        //if (!isQX)
        //    return;

        EndNumber = Request.QueryString["EndNumber"] + "";
        if (EndNumber == "")
            EndNumber = Session["W_DDBH"].ToString();

        StartNumber = Request.QueryString["ThNumber"];
        Translations(); 

        if (!IsPostBack)
        {
            
            BindQS();

            BindData(); 
        }
        
    }


    private void Translations()
    {

        this.TranControls(this.Button1, new string[][] { new string[] { "000177", " 显示 " } });
        this.GridView1.Columns[0].HeaderText = GetTran("007323", "安置结构");
        //this.TranControls(this.btnExit, new string[][] { new string[] { "006812", "重置" } });
    }


    //判断是否有权限查看该网咯
    public bool IsRoot(string StartNumber, string qs, string EndNumber)
    {
        return WTreeBLL.IsRoot(StartNumber, qs, EndNumber);
    }

    public string GetAzOrTz(string direct, string placement,string isAz)
    {
        if (isAz == "1") //安置
        {
            return direct == placement ? "" : placement;
        }
        else
        {
            return direct == placement ? "" : direct;
        }
    }

    public void BindData()
    {
        string qs = DDLQs.SelectedValue;

        if (IsRoot(StartNumber, qs, Session["W_DDBH"].ToString()) == false)
        {
            string maxqs = WTreeBLL.GetMaxQS();
            string ts =GetTran("007461", "您没有权限查看该网络图！");

            if (IsRoot(StartNumber, maxqs, Session["W_DDBH"].ToString()))
            {
                ts = GetTran("007462","您查看的这期中没有这个会员，所以不能查看该网路！");
            }

            ClientScript.RegisterStartupScript(this.GetType(), "", "<script type='text/javascript'>alert('"+ts+"');window.history.go(-1);</script>");
            return;
        }

        object[] obj = WTreeBLL.GetLLTTree(qs, EndNumber, StartNumber, "1", "1");

        SqlDataReader dr = (SqlDataReader)obj[0];

        while (dr.Read())
        {
            if (dr["Field"].ToString() == "PetName")
            {
                GridView1.Columns[1].HeaderText = GetTran(dr["FieldName"].ToString());

                if (dr["IsVisible"].ToString() == "0")
                    GridView1.Columns[1].Visible = false;
            }

            if (dr["Field"].ToString() == "DaiShu")
            {
                GridView1.Columns[4].HeaderText = GetTran(dr["FieldName"].ToString());

                if (dr["IsVisible"].ToString() == "0")
                    GridView1.Columns[4].Visible = false;
            }
            else if (dr["Field"].ToString() == "TJ")
            {
                GridView1.Columns[2].HeaderText = GetTran(dr["FieldName"].ToString());

                if (dr["IsVisible"].ToString() == "0")
                    GridView1.Columns[2].Visible = false;
            }
            else if (dr["Field"].ToString() == "JiBie")
            {
                GridView1.Columns[3].HeaderText = GetTran(dr["FieldName"].ToString());

                if (dr["IsVisible"].ToString() == "0")
                    GridView1.Columns[3].Visible = false;
            }
            else if (dr["Field"].ToString() == "XinGe")
            {
                GridView1.Columns[6].HeaderText = GetTran(dr["FieldName"].ToString());

                if (dr["IsVisible"].ToString() == "0")
                    GridView1.Columns[6].Visible = false;
            }
            else if (dr["Field"].ToString() == "XinWang")
            {
                GridView1.Columns[7].HeaderText = GetTran(dr["FieldName"].ToString());

                if (dr["IsVisible"].ToString() == "0")
                    GridView1.Columns[7].Visible = false;
            }

            else if (dr["Field"].ToString() == "XinRen")
            {
                GridView1.Columns[8].HeaderText = GetTran(dr["FieldName"].ToString());

                if (dr["IsVisible"].ToString() == "0")
                    GridView1.Columns[8].Visible = false;
            }
            else if (dr["Field"].ToString() == "ZongRen")
            {
                GridView1.Columns[9].HeaderText = GetTran(dr["FieldName"].ToString());

                if (dr["IsVisible"].ToString() == "0")
                    GridView1.Columns[9].Visible = false;
            }
            else if (dr["Field"].ToString() == "ZongFen")
            {
                GridView1.Columns[10].HeaderText = GetTran(dr["FieldName"].ToString());

                if (dr["IsVisible"].ToString() == "0")
                    GridView1.Columns[10].Visible = false;
            }

        }
        dr.Close();

        GridView1.DataSource = (DataTable)obj[1]; 
        GridView1.DataBind();

        SetLianLuTu(StartNumber, Session["C_L_AZ"].ToString(), qs, Session["W_DDBH"].ToString());
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        BindData();
    }

    public void BindQS()
    {
        IList<ConfigModel> configs = CommonDataBLL.GetVolumeLists();
        DDLQs.DataSource = configs;
        DDLQs.DataTextField = "date";
        DDLQs.DataValueField = "ExpectNum";
        DDLQs.DataBind();

        DDLQs.SelectedValue = Request.QueryString["qs"];
    }

    public void SetLianLuTu(string EndNumber, string _StartNumber, string Qs, string ysEndNumber)
    {
        LitLLT.Text = "";

        LitLLT.Text = WTreeBLL.SetLianLuTu_L(EndNumber, _StartNumber, Qs, ysEndNumber);
    }

}
