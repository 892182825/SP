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
using BLL.other;
using Model.Other;

public partial class Company_CommonlyNetwork : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Permissions.ComRedirect(Page, Permissions.redirUrl);
            Response.Redirect("CommonlyNetworkII.aspx");
            CommonDataBLL.BindQishuList(DropDownList_QiShu, false);

            DataTable ky = WTreeBLL.GetKYWL(Session["Company"].ToString(), "1");

            string firstky = Request.QueryString["EndNumber"] + "";
            bool isQX = false;
            for (int i = 0; i < ky.Rows.Count; i++)
            {
                if (i == 0)
                {
                    if (firstky == "")
                        firstky = ky.Rows[i]["number"].ToString();
                }

                if (firstky == ky.Rows[i]["number"].ToString())
                {
                    isQX = true;
                }

                LitMaxWl.Text = LitMaxWl.Text + "<a href='CommonlyNetwork.aspx?EndNumber=" + ky.Rows[i]["number"].ToString() + "' style='color:gray;font-weight:" + (firstky == ky.Rows[i]["number"].ToString() ? "bold" : "") + "'>" + ky.Rows[i]["number"] + "</a> / ";
            }

            if (!isQX)
                return;



            ViewState["dc"] = firstky;

            string qsnumber = Request.QueryString["qsNumber"];
            if (string.IsNullOrEmpty(qsnumber))
                TextBox1.Text = ViewState["dc"].ToString();
            else
                TextBox1.Text = qsnumber;

            GetCYWLT();

            
        }

        Translations();
    }



    private void Translations()
    {
        //000420  007308    Button3
        this.TranControls(this.Button1, new string[][] { new string[] { "000434", "确定" } });
        this.Button3.Text = GetTran("000420", "常用") + "(1)";
        this.Button2.Text = GetTran("000420", "常用") + "(2)";
        this.TranControls(this.Button5, new string[][] { new string[] { "007308", "伸缩" } });







    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        GetCYWLT();
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        Response.Redirect("SST_AZ.aspx");
    }

    public void GetCYWLT()
    {


        string qsnumber = TextBox1.Text.Trim();

        string ParentNumber = WTreeBLL.GetNumberParent(qsnumber);

        string qs = DropDownList_QiShu.SelectedValue;

        if (!WTreeBLL.IsRoot(qsnumber, qs, ViewState["dc"].ToString()))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('你不能查看该网络！')</script>");
            return;
        }


        //链路图
        LitLLT.Text = WTreeBLL.SetLianLuTu_CYWL(ViewState["dc"].ToString(), qsnumber, qs);

        //第一层
        CYWLTModel cm = WTreeBLL.GetCYWLTModel(ParentNumber, qs, WTreeBLL.GetNumberQuShu(qsnumber));

        if (cm != null)
        {
            dyc1.Text = "<a style='color:rgb(43,128,45)' href='CommonlyNetwork.aspx?qsNumber=" + cm.Number + "&EndNumber=" + ViewState["dc"] + "'>" + cm.Number + "</a>";
            dyc2.Text = cm.PetName;
            dyc3.Text = cm.Level;
            dyc4.Text = cm.ZY;
            dyc5.Text = cm.XY;
            dyc6.Text = cm.SY;
            dyc7.Text = cm.Left;
            dyc8.Text = cm.Right;
        }

        //第二层
        CYWLTModel cml2 = WTreeBLL.GetCYWLTModel(cm == null ? "" : cm.Number, qs, "1");

        if (cml2 != null)
        {
            dec1.Text = "<a style='color:rgb(43,128,45)' href='CommonlyNetwork.aspx?qsNumber=" + cml2.Number + "&EndNumber=" + ViewState["dc"] + "'>" + cml2.Number + "</a>";
            dec2.Text = cml2.PetName;
            dec3.Text = cml2.Level;
            dec4.Text = cml2.ZY;
            dec5.Text = cml2.XY;
            dec6.Text = cml2.SY;
            dec7.Text = cml2.Left;
            dec8.Text = cml2.Right;
        }

        CYWLTModel cmr2 = WTreeBLL.GetCYWLTModel(cm == null ? "" : cm.Number, qs, "2");

        if (cmr2 != null)
        {
            dec9.Text = "<a style='color:rgb(43,128,45)' href='CommonlyNetwork.aspx?qsNumber=" + cmr2.Number + "&EndNumber=" + ViewState["dc"] + "'>" + cmr2.Number + "</a>";
            dec10.Text = cmr2.PetName;
            dec11.Text = cmr2.Level;
            dec12.Text = cmr2.ZY;
            dec13.Text = cmr2.XY;
            dec14.Text = cmr2.SY;
            dec15.Text = cmr2.Left;
            dec16.Text = cmr2.Right;
        }

        //第三层
        CYWLTModel cml3_1 = WTreeBLL.GetCYWLTModel(cml2 == null ? "" : cml2.Number, qs, "1");

        if (cml3_1 != null)
        {
            dsc1.Text = "<a style='color:rgb(43,128,45)' href='CommonlyNetwork.aspx?qsNumber=" + cml3_1.Number + "&EndNumber=" + ViewState["dc"] + "'>" + cml3_1.Number + "</a>";
            dsc2.Text = cml3_1.PetName;
            dsc3.Text = cml3_1.Level;
            dsc4.Text = cml3_1.ZY;
            dsc5.Text = cml3_1.XY;
            dsc6.Text = cml3_1.SY;
            dsc7.Text = cml3_1.Left;
            dsc8.Text = cml3_1.Right;
        }

        CYWLTModel cml3_2 = WTreeBLL.GetCYWLTModel(cml2 == null ? "" : cml2.Number, qs, "2");

        if (cml3_2 != null)
        {
            dsc9.Text = "<a style='color:rgb(43,128,45)' href='CommonlyNetwork.aspx?qsNumber=" + cml3_2.Number + "&EndNumber=" + ViewState["dc"] + "'>" + cml3_2.Number + "</a>";
            dsc10.Text = cml3_2.PetName;
            dsc11.Text = cml3_2.Level;
            dsc12.Text = cml3_2.ZY;
            dsc13.Text = cml3_2.XY;
            dsc14.Text = cml3_2.SY;
            dsc15.Text = cml3_2.Left;
            dsc16.Text = cml3_2.Right;
        }

        CYWLTModel cmr3_1 = WTreeBLL.GetCYWLTModel(cmr2 == null ? "" : cmr2.Number, qs, "1");

        if (cmr3_1 != null)
        {
            dsc17.Text = "<a style='color:rgb(43,128,45)' href='CommonlyNetwork.aspx?qsNumber=" + cmr3_1.Number + "&EndNumber=" + ViewState["dc"] + "'>" + cmr3_1.Number + "</a>";
            dsc18.Text = cmr3_1.PetName;
            dsc19.Text = cmr3_1.Level;
            dsc20.Text = cmr3_1.ZY;
            dsc21.Text = cmr3_1.XY;
            dsc22.Text = cmr3_1.SY;
            dsc23.Text = cmr3_1.Left;
            dsc24.Text = cmr3_1.Right;
        }

        CYWLTModel cmr3_2 = WTreeBLL.GetCYWLTModel(cmr2 == null ? "" : cmr2.Number, qs, "2");

        if (cmr3_2 != null)
        {
            dsc25.Text = "<a style='color:rgb(43,128,45)' href='CommonlyNetwork.aspx?qsNumber=" + cmr3_2.Number + "&EndNumber=" + ViewState["dc"] + "'>" + cmr3_2.Number + "</a>";
            dsc26.Text = cmr3_2.PetName;
            dsc27.Text = cmr3_2.Level;
            dsc28.Text = cmr3_2.ZY;
            dsc29.Text = cmr3_2.XY;
            dsc30.Text = cmr3_2.SY;
            dsc31.Text = cmr3_2.Left;
            dsc32.Text = cmr3_2.Right;
        }

    }
}
