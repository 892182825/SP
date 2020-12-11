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
using BLL.other;
using System.Collections.Generic;
using Model;
using BLL.CommonClass;
using BLL;

public partial class Company_GraphNet : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);

        //可查看的网路
        LitMaxWl.Text = "";
        DataTable ky = WTreeBLL.GetKYWL(Session["Company"].ToString(), "1");
        Translations();
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

            LitMaxWl.Text = LitMaxWl.Text + "<a href='GraphNet.aspx?EndNumber=" + ky.Rows[i]["number"].ToString() + "' style='color:gray;font-weight:" + (firstky == ky.Rows[i]["number"].ToString() ? "bold" : "") + "'>" + ky.Rows[i]["number"] + "</a> / ";
        }

        if (!isQX)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("007461", "您没有权限查看！") + "')</script>");
            //Server.Transfer("first.aspx");
             return;

        }

        ViewState["ky"] = firstky;

        string startBH = Request.QueryString["Number"] + "";
        if (startBH == "")
            startBH = firstky;

        if (!IsPostBack)
        {
            BindQS();
            DDLQs.SelectedValue = Request.QueryString["qs"];

            BindNet(startBH, firstky, DDLQs.SelectedValue);
        }
    }


    private void Translations()
    {
        //000420  007308    Button3
        this.TranControls(this.Button1, new string[][] { new string[] { "000177", "显示" } });
        this.Button2.Text = GetTran("000420", "常用") + "(1)";
        //this.Button3.Text = GetTran("000420", "常用") + "(2)";
        this.Button4.Text = GetTran("000420", "常用") + "(3)";
        this.TranControls(this.Button5, new string[][] { new string[] { "007308", "伸缩" } });







    }



    public void BindNet(string startBH, string EndBH,string qs)
    {
        if (WTreeBLL.IsRoot(startBH, qs, EndBH) == false)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("007461", "您没有权限查看！") + "')</script>");
            return;
        }

        SetLianLuTu(EndBH, startBH, qs);
        txNumber.Text = startBH;

        //横/竖 线div
        List<NumberClass> lhx = new List<NumberClass>();

        //第二层
        DataTable dt2 = WTreeBLL.GetGraphNet_AZ(startBH, qs, "0");

        List<NumberClass> ln2 = new List<NumberClass>();
        for (int i = 0; i < dt2.Rows.Count; i++)
        {
            NumberClass nc = new NumberClass();
            nc.Number = dt2.Rows[i]["Number"].ToString();
            nc.PetName = dt2.Rows[i]["PetName"].ToString();
            nc.MemberState = dt2.Rows[i]["MemberState"].ToString();

            nc.Left = i * nc.Width;
            nc.Top = 400;

            nc.Level = dt2.Rows[i]["JiBie"].ToString();
            nc.Z = dt2.Rows[i]["TotalNetRecord"].ToString();
            nc.X = dt2.Rows[i]["CurrentOneMark"].ToString();
            nc.Y = dt2.Rows[i]["syyj"].ToString();

            ln2.Add(nc);
        }

        //第三层
        List<NumberClass> ln3 = new List<NumberClass>();

        for (int i = 0; i < dt2.Rows.Count; i++)
        {
            DataTable dt3 = WTreeBLL.GetGraphNet_AZ(dt2.Rows[i]["Number"].ToString(), qs, "0");

            NumberClass hxdiv = new NumberClass();//第三层横线div

            for (int j = 0; j < dt3.Rows.Count; j++)
            {
                NumberClass nc = new NumberClass();
                nc.Number = dt3.Rows[j]["Number"].ToString();
                nc.PetName = dt3.Rows[j]["PetName"].ToString();
                nc.MemberState = dt3.Rows[j]["MemberState"].ToString();

                nc.Left = ln2[i].Left+j*nc.Width;
                nc.Top = 600;

                nc.Level = dt3.Rows[j]["JiBie"].ToString();
                nc.Z = dt3.Rows[j]["TotalNetRecord"].ToString();
                nc.X = dt3.Rows[j]["CurrentOneMark"].ToString();
                nc.Y = dt3.Rows[j]["syyj"].ToString();

                ln3.Add(nc);

                //横线
                if (j == 0)
                {
                    hxdiv.Left=nc.Left + 186 / 2;
                    hxdiv.Height = 2;
                    hxdiv.Top = nc.Top - 66 / 2;
                }
                else if (j == dt3.Rows.Count - 1)
                {
                    hxdiv.Width = nc.Left + 186 / 2 - hxdiv.Left;

                    lhx.Add(hxdiv);
                }

                //向上竖线
                NumberClass sxdiv3 = new NumberClass();
                sxdiv3.Left = nc.Left + 186 / 2;
                sxdiv3.Height = 33;
                sxdiv3.Top = nc.Top -33;
                sxdiv3.Width = 2;
                lhx.Add(sxdiv3);
                
            }

            //第二层根据第三层的个数进行向右移动
            if (dt3.Rows.Count > 0)
            {
                int _left=(dt3.Rows.Count-1)*186/2;
                for (int k = i; k < ln2.Count; k++)
                {
                    if (k == i) //自身
                        ln2[k].Left = ln2[k].Left + _left;
                    else
                        ln2[k].Left = ln2[k].Left + _left + _left;
                }
            }
        }

        //第一层
        int div1Left=0;
        if (ln2.Count > 1)
            div1Left = (ln2[ln2.Count - 1].Left - ln2[0].Left - 186) / 2 + ln2[0].Left + 186 / 2;
        else if (ln2.Count == 1)
            div1Left = ln2[0].Left;
        else
            div1Left = 0;

        DataTable dt1 = WTreeBLL.GetGraphNet_AZ(startBH, qs, "1");

        NumberClass div1nc = new NumberClass();
        div1nc.Number = dt1.Rows[0]["Number"].ToString();
        div1nc.PetName = dt1.Rows[0]["PetName"].ToString();
        div1nc.MemberState = dt1.Rows[0]["MemberState"].ToString();


        div1nc.Left = div1Left;
        div1nc.Top = 200;
        div1nc.Level = dt1.Rows[0]["JiBie"].ToString();
        div1nc.Z = dt1.Rows[0]["TotalNetRecord"].ToString();
        div1nc.X = dt1.Rows[0]["CurrentOneMark"].ToString();
        div1nc.Y = dt1.Rows[0]["syyj"].ToString();
        

        string div1 = "<div style='position:absolute;left:" + (div1nc.Left+50) + "px;top:" + div1nc.Top + "px;width:" + div1nc.Width + "px;height:" + div1nc.Height + "px;' align='center'>"
            + "<div style='width:166px;height:100%;border:gray solid 1px;;font-size:12px'>" 
            + GetTable(div1nc) + "</div></div>\r\n";
        
        //第一层下竖线
        if (ln2.Count > 0)
        {
            NumberClass _sxdiv1 = new NumberClass();
            _sxdiv1.Left = div1nc.Left + 186 / 2;
            _sxdiv1.Height = 33;
            _sxdiv1.Top = div1nc.Top + div1nc.Height;
            _sxdiv1.Width = 2;
            lhx.Add(_sxdiv1);
        }

        //第二层div横线
        if (ln2.Count > 1)
        {
            NumberClass hxdiv2 = new NumberClass();//横线div
            hxdiv2.Left = ln2[0].Left + 186 / 2;
            hxdiv2.Height = 2;
            hxdiv2.Top = ln2[0].Top - 66 / 2;
            hxdiv2.Width = ln2[ln2.Count - 1].Left + 186 / 2 - hxdiv2.Left;
            lhx.Add(hxdiv2);
        }


        //第二层竖线div
        for (int i = 0; i < ln2.Count; i++)
        {
            //上竖线
            NumberClass sxdiv = new NumberClass();
            sxdiv.Left = ln2[i].Left + 186 / 2;
            sxdiv.Height = 33;
            sxdiv.Top = ln2[i].Top - 33;
            sxdiv.Width = 2;
            lhx.Add(sxdiv);

            //下竖线
            if (WTreeBLL.IsExistsAZ(ln2[i].Number, qs))
            {
                NumberClass sxdiv2 = new NumberClass();
                sxdiv2.Left = ln2[i].Left + 186 / 2;
                sxdiv2.Height = 33;
                sxdiv2.Top = ln2[i].Top + ln2[i].Height;
                sxdiv2.Width = 2;
                lhx.Add(sxdiv2);
            }
        }

        //
        //显示内容div
        string netStr = div1;
        for (int i = 0; i < ln2.Count; i++)
        {
            netStr = netStr + "<div style='position:absolute;left:" + (ln2[i].Left+50) + "px;top:" + ln2[i].Top + "px;width:" + ln2[i].Width + "px;height:" + ln2[i].Height + "px;' align='center'>"
                + "<div style='width:166px;height:100%;border:gray solid 1px;;font-size:12px'>" + GetTable(ln2[i]) + "</div></div>\r\n";
        }
        for (int i = 0; i < ln3.Count; i++)
        {
            netStr = netStr + "<div style='position:absolute;left:" + (ln3[i].Left+50) + "px;top:" + ln3[i].Top + "px;width:" + ln3[i].Width + "px;height:" + ln3[i].Height + "px;' align='center'>"
                + "<div style='width:166px;height:100%;border:gray solid 1px;;font-size:12px'>" + GetTable(ln3[i]) + "</div></div>\r\n";
        }

        //显示横线div
        for (int i = 0; i < lhx.Count; i++)
        {
            netStr = netStr + "<div style=';font-size:12px;position:absolute;left:" + (lhx[i].Left + 50) + "px;top:" + lhx[i].Top + "px;width:" + lhx[i].Width + "px;height:" + lhx[i].Height + "px;background-color:rgb(100,181,208);overflow:hidden;'></div>\r\n";
        }

        network.Text = netStr;// + "<div style='position:absolute;left:0px;top:"+(ln3[0].Top+206)+"px'>1.点击节点上的图标，可以查看会员三层以内的下属。<br>2.灰色编号的为未激活会员。</div>";
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("GraphNet.aspx?EndNumber=" + ViewState["ky"] + "&Number=" + txNumber.Text.Trim() + "&qs=" + DDLQs.SelectedValue);
    }

    public void BindQS()
    {
        //DataTable dt = WTreeBLL.BindQS();

        //DDLQs.DataSource = dt;
        //DDLQs.DataTextField = "ExpectNum";
        //DDLQs.DataValueField = "ExpectNum";
        //DDLQs.DataBind();
        //CommonDataBLL.BindQishuList(this.DDLQs, false);

        IList<ConfigModel> configs = CommonDataBLL.GetVolumeLists();
        DDLQs.DataSource = configs;
        DDLQs.DataTextField = "date";
        DDLQs.DataValueField = "ExpectNum";
        DDLQs.Items.Add(new ListItem(new TranslationBase().GetTran("000633", "全部"), "-1"));
        DDLQs.DataBind();
    }

    public void SetLianLuTu(string EndNumber, string StartNumber, string Qs)
    {
        DataTable dt = WTreeBLL.SetLianLuTu_C(EndNumber, StartNumber, Qs);

        string str = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            str = str + "<a href='GraphNet.aspx?EndNumber=" + EndNumber + "&Number=" + dt.Rows[i]["Number"].ToString().Split(' ')[0] + "&qs=" + Qs + "' style='color:gray;'>" + dt.Rows[i]["Number"].ToString().Split(' ')[0] + "(" + dt.Rows[i]["Number"].ToString().Split(' ')[1] + ")" + "</a> >> ";
        }

        LitLLT.Text = str;
    }

    public string GetTable(NumberClass nc)
    {
        string tz = "onclick=\"window.location.href='GraphNet.aspx?EndNumber=" + ViewState["ky"] + "&Number=" + nc.Number + "&qs=" + DDLQs.SelectedValue+"'\"";
        string str = @"
        <table class='wlttab2' cellspacing='0' cellpadding='0' style='font-size:12px' >
			<tr>
				<td colspan='3' style='font-size:12px;background-image:url(Images/blue_25.png);background-position:-5px 0px;color:" + (nc.MemberState == "1" ? "white" : "rgb(163,163,163)") + ";font-weight:bold;cursor:pointer;' " + tz + @">
                    " + nc.Number+"("+nc.PetName+")"+@"
				</td>
			</tr>
            <!--<tr>
				<td colspan='3'>
                    " + nc.PetName + @"
				</td>
			</tr>-->
			<tr>
				<td colspan='3'>
                    " +nc.Level+ @"
				</td>
			</tr>
			<tr>
				<td style='background-color:rgb(238,239,243);font-size:12px;'>
                    &nbsp;
				</td>
				<td style='background-color:rgb(238,239,243);font-size:12px;'>
					左区
				</td>
				<td style='background-color:rgb(238,239,243);font-size:12px;'>
					右区
				</td>
			</tr>
			<tr>
				<td style='background-color:rgb(238,239,243);font-size:12px;'>
					总
				</td>
				<td>
                    " + WTreeBLL.GetYj("TotalNetRecord", DDLQs.SelectedValue, "1", nc.Number).ToString("0.00") + @"
				</td>
				<td>
                    " + WTreeBLL.GetYj("TotalNetRecord", DDLQs.SelectedValue, "2", nc.Number).ToString("0.00") + @"
				</td>
			</tr>
			<tr>
				<td style='background-color:rgb(238,239,243);font-size:12px;'>
					新
				</td>
				<td>
                    " + WTreeBLL.GetYj("CurrentOneMark", DDLQs.SelectedValue, "1", nc.Number).ToString("0.00") + @"
				</td>
				<td>
                    " + WTreeBLL.GetYj("CurrentOneMark", DDLQs.SelectedValue, "2", nc.Number).ToString("0.00") + @"
				</td>
			</tr>
			<tr>
				<td style='background-color:rgb(238,239,243);font-size:12px;'>
					余
				</td>
				<td>
                    " + WTreeBLL.GetYj("syyj", DDLQs.SelectedValue, "1", nc.Number).ToString("0.00") + @"
				</td>
				<td>
                    " + WTreeBLL.GetYj("syyj", DDLQs.SelectedValue, "2", nc.Number).ToString("0.00") + @"
				</td>
			</tr>
		</table>";

        return str;
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Response.Redirect("SST_AZ.aspx");
    }
}

public class NumberClass
{
    public string Number = "";
    public string PetName = "";
    public string MemberState = "";

    public int Left = 0;
    public int Top = 0;
    public int Width = 186;
    public int Height = 134;

    public string Level = "";
    public string Z = "";
    public string X = "";
    public string Y = "";
}