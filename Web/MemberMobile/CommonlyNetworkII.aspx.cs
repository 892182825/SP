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

public partial class Member_CommonlyNetworkII : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Permissions.MemRedirect(Page, Permissions.redirUrl);

            CommonDataBLL.BindQishuList(DropDownList_QiShu, false);


            ViewState["dc"] = SfType.getBH().ToString();

            string qsnumber = Request.QueryString["qsNumber"];
            if (string.IsNullOrEmpty(qsnumber))
                TextBox1.Text = ViewState["dc"].ToString();
            else
                TextBox1.Text = qsnumber;

            Performance.ExpectNum = Convert.ToInt32(DropDownList_QiShu.SelectedValue);
            Performance.Number = TextBox1.Text.Trim();

            GetCYWLT();
            Translations();
        }
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
        clearText();

        string qsnumber = TextBox1.Text.Trim();

        string qs = DropDownList_QiShu.SelectedValue;

        if (!WTreeBLL.IsRoot(qsnumber, qs, ViewState["dc"].ToString()))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('你不能查看该网络！')</script>");
            return;
        }


        //链路图
        LitLLT.Text = WTreeBLL.SetLianLuTu_CYWLII(ViewState["dc"].ToString(), qsnumber, qs);

        //第一层
        CYWLTModel cm = WTreeBLL.GetCYWLTModelII(qsnumber, qs,"1","");

        if (cm != null)
        {
            dyc1.Text = "<a style='color:red' href='CommonlyNetworkII.aspx?qsNumber=" + cm.Number + "&EndNumber=" + ViewState["dc"] + "'>" + cm.Number + "</a>";
            dyc2.Text = cm.PetName;
            dyc3.Text = cm.Level;
            dyc4.Text = cm.ZY;
            dyc5.Text = cm.XY;
            dyc6.Text = cm.SY;
            dyc7.Text = cm.Left;
            dyc8.Text = cm.Right;
        }

        //第二层
        CYWLTModel cml2 = WTreeBLL.GetCYWLTModelII(cm == null ? "" : cm.Number, qs, "2","");

        if (cml2 != null)
        {
            dec1.Text = "<a style='color:red' href='CommonlyNetworkII.aspx?qsNumber=" + cml2.Number + "&EndNumber=" + ViewState["dc"] + "'>" + cml2.Number + "</a>";
            dec2.Text = cml2.PetName;
            dec3.Text = cml2.Level;
            dec4.Text = cml2.ZY;
            dec5.Text = cml2.XY;
            dec6.Text = cml2.SY;
            dec7.Text = cml2.Left;
            dec8.Text = cml2.Right;
        }

        CYWLTModel cmr2 = WTreeBLL.GetCYWLTModelII(cm == null ? "" : cm.Number, qs, "2", cml2==null?"":cml2.Number);

        if (cmr2 != null)
        {
            dec9.Text = "<a style='color:red' href='CommonlyNetworkII.aspx?qsNumber=" + cmr2.Number + "&EndNumber=" + ViewState["dc"] + "'>" + cmr2.Number + "</a>";
            dec10.Text = cmr2.PetName;
            dec11.Text = cmr2.Level;
            dec12.Text = cmr2.ZY;
            dec13.Text = cmr2.XY;
            dec14.Text = cmr2.SY;
            dec15.Text = cmr2.Left;
            dec16.Text = cmr2.Right;
        }

        //第三层
        CYWLTModel cml3_1 = WTreeBLL.GetCYWLTModelII(cml2 == null ? "" : cml2.Number, qs, "3","");

        if (cml3_1 != null)
        {
            dsc1.Text = "<a style='color:red' href='CommonlyNetworkII.aspx?qsNumber=" + cml3_1.Number + "&EndNumber=" + ViewState["dc"] + "'>" + cml3_1.Number + "</a>";
            dsc2.Text = cml3_1.PetName;
            dsc3.Text = cml3_1.Level;
            dsc4.Text = cml3_1.ZY;
            dsc5.Text = cml3_1.XY;
            dsc6.Text = cml3_1.SY;
            dsc7.Text = cml3_1.Left;
            dsc8.Text = cml3_1.Right;
        }

        CYWLTModel cml3_2 = WTreeBLL.GetCYWLTModelII(cml2 == null ? "" : cml2.Number, qs, "3", cml3_1==null?"":cml3_1.Number);

        if (cml3_2 != null)
        {
            dsc9.Text = "<a style='color:red' href='CommonlyNetworkII.aspx?qsNumber=" + cml3_2.Number + "&EndNumber=" + ViewState["dc"] + "'>" + cml3_2.Number + "</a>";
            dsc10.Text = cml3_2.PetName;
            dsc11.Text = cml3_2.Level;
            dsc12.Text = cml3_2.ZY;
            dsc13.Text = cml3_2.XY;
            dsc14.Text = cml3_2.SY;
            dsc15.Text = cml3_2.Left;
            dsc16.Text = cml3_2.Right;
        }

        CYWLTModel cmr3_1 = WTreeBLL.GetCYWLTModelII(cmr2 == null ? "" : cmr2.Number, qs, "3","");

        if (cmr3_1 != null)
        {
            dsc17.Text = "<a style='color:red' href='CommonlyNetworkII.aspx?qsNumber=" + cmr3_1.Number + "&EndNumber=" + ViewState["dc"] + "'>" + cmr3_1.Number + "</a>";
            dsc18.Text = cmr3_1.PetName;
            dsc19.Text = cmr3_1.Level;
            dsc20.Text = cmr3_1.ZY;
            dsc21.Text = cmr3_1.XY;
            dsc22.Text = cmr3_1.SY;
            dsc23.Text = cmr3_1.Left;
            dsc24.Text = cmr3_1.Right;
        }

        CYWLTModel cmr3_2 = WTreeBLL.GetCYWLTModelII(cmr2 == null ? "" : cmr2.Number, qs, "3", cmr3_1==null?"":cmr3_1.Number);

        if (cmr3_2 != null)
        {
            dsc25.Text = "<a style='color:red' href='CommonlyNetworkII.aspx?qsNumber=" + cmr3_2.Number + "&EndNumber=" + ViewState["dc"] + "'>" + cmr3_2.Number + "</a>";
            dsc26.Text = cmr3_2.PetName;
            dsc27.Text = cmr3_2.Level;
            dsc28.Text = cmr3_2.ZY;
            dsc29.Text = cmr3_2.XY;
            dsc30.Text = cmr3_2.SY;
            dsc31.Text = cmr3_2.Left;
            dsc32.Text = cmr3_2.Right;
        }

    }

    public void clearText()
    {
        dyc1.Text = "--";
        dyc2.Text = "--";
        dyc3.Text = "--";
        dyc4.Text = "--";
        dyc5.Text = "--";
        dyc6.Text = "--";
        dyc7.Text = "--";
        dyc8.Text = "--";

        dec1.Text = "--";
        dec2.Text = "--";
        dec3.Text = "--";
        dec4.Text = "--";
        dec5.Text = "--";
        dec6.Text = "--";
        dec7.Text = "--";
        dec8.Text = "--";

        dec9.Text = "--";
        dec10.Text = "--";
        dec11.Text = "--";
        dec12.Text = "--";
        dec13.Text = "--";
        dec14.Text = "--";
        dec15.Text = "--";
        dec16.Text = "--";

        dsc1.Text = "--";
        dsc2.Text = "--";
        dsc3.Text = "--";
        dsc4.Text = "--";
        dsc5.Text = "--";
        dsc6.Text = "--";
        dsc7.Text = "--";
        dsc8.Text = "--";

        dsc9.Text = "--";
        dsc10.Text = "--";
        dsc11.Text = "--";
        dsc12.Text = "--";
        dsc13.Text = "--";
        dsc14.Text = "--";
        dsc15.Text = "--";
        dsc16.Text = "--";

        dsc17.Text = "--";
        dsc18.Text = "--";
        dsc19.Text = "--";
        dsc20.Text = "--";
        dsc21.Text = "--";
        dsc22.Text = "--";
        dsc23.Text = "--";
        dsc24.Text = "--";

        dsc25.Text = "--";
        dsc26.Text = "--";
        dsc27.Text = "--";
        dsc28.Text = "--";
        dsc29.Text = "--";
        dsc30.Text = "--";
        dsc31.Text = "--";
        dsc32.Text = "--";

    }

    private void Translations()
    {
        this.TranControls(this.Button1, new string[][] { new string[] { "000434", "确定" } });
        this.TranControls(this.Button5, new string[][] { new string[] { "007308", "伸缩" } });
        Button2.Text = GetTran("000420", "常用") + "(1)";
        Button3.Text = GetTran("000420", "常用") + "(2)";
        Button4.Text = GetTran("000420", "常用") + "(3)";
    }
}
