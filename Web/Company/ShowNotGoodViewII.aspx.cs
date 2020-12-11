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
using System.Xml;
using BLL.other;

public partial class Company_ShowNotGoodViewII : BLL.TranslationBase
{

    //点击者的编号的父编号（父编号就是他的安置编号）
    public string Number = "";
    //自身编号，点击者的编号
    public string ThNumber = "";
    //可查看的网路（结束编号）
    public string EndNumber = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        translation();
        //可查看的网路
        LitMaxWl.Text = "";
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

            LitMaxWl.Text = LitMaxWl.Text + "<a href='SST_AZ.aspx?EndNumber=" + ky.Rows[i]["number"].ToString() + "' style='color:gray;font-weight:" + (firstky == ky.Rows[i]["number"].ToString() ? "bold" : "") + "'>" + ky.Rows[i]["number"] + "</a> / ";
        }

        if (!isQX)
            return;


        //
        if (Request.QueryString["number"] + "" == "")  // 首次加载
            Number = GetNumberParent(firstky);
        else
            Number = Request.QueryString["number"];

        if (Request.QueryString["thnumber"] + "" == "")  // 首次加载
            ThNumber = firstky;
        else
            ThNumber = Request.QueryString["thnumber"];


        EndNumber = firstky;



        //期数
        string Qs = Request.QueryString["ExpectNum"];
        if (String.IsNullOrEmpty(Qs))
            Qs = "1";
        //end



        if (!IsPostBack && Request.QueryString["action"] == null)
        {
            //获取链路图
            if (WTreeBLL.IsExistsNumber(ThNumber) && IsRoot(ThNumber, Qs, EndNumber))
            {
                SetLianLuTu(EndNumber, ThNumber, Qs);
            }

            //设置默认层
            string _cs = "2";
            if (Session["WLTCS_C_A"] + "" != "")
                _cs = Session["WLTCS_C_A"].ToString();

            cengs.SelectedValue = _cs;

            //加载表头
            DataTable dt = WTreeBLL.GetWangLuoT("1", "1");

            string title = "<tr id='tr" + Number + "' style='background-image:url(images/lmenu02.gif);height:25px'><td nimgcount='0' align='center' style='color:white'>" + GetTran("007323", "安置结构") + "</td>";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string _fn = GetTran(dt.Rows[i]["FieldName"].ToString(), "");

                title = title + "<td align='center' style='color:white'>" + _fn + "</td>";
            }

            litTitle.Text = title + "</tr>";

            dt.Dispose();

            BindQS();


        }
        else if (Request.QueryString["action"] != null)  //ajax 调用
        {
            string temp = "";
            string action = Request.QueryString["action"];

            switch (action)
            {
                case "GetTree":
                    temp = GetTree(Request.QueryString["nodeid"], Request.QueryString["ExpectNum"], Request.QueryString["thnumber"], Request.QueryString["model"], "1", "1", Session["Company"].ToString(),Session["LanguageCode"].ToString());//Request.QueryString["nodeid"]  为父元素的编号

                    Response.ContentType = "text/xml";
                    break;

                case "SetImage":
                    temp = SetImage(Request.QueryString["thnumber"], (Request.QueryString["img"]).ToLower(), Session["Company"].ToString());

                    Response.ContentType = "text/plain";//文本
                    break;


                case "SetColor":
                    temp = SetColor(Request.QueryString["thnumber"], Request.QueryString["model"], Request.QueryString["ExpectNum"], Request.QueryString["Tuannumber"], Session["Company"].ToString());

                    Response.ContentType = "text/plain";//文本
                    break;
            }

            Response.Write(temp);
            Response.End();
        }
    }

    private void translation() {
        TranControls(Button1, new string[][] { new string[] { "000177", "显示" } });
    }

    //返回父元素
    public string GetNumberParent(string ThNumber)
    {
        return WTreeBLL.GetNumberParent(ThNumber);
    }

    public string GetNumberParent_TJ(string ThNumber)
    {
        return WTreeBLL.GetNumberParent_II(ThNumber);
    }

    //获取树
    public string GetTree(string nodeid, string ExpectNum, string _thnumber, string model, string BrowserType, string IsPlacement, string ManageNum,string language) //nodeid 为父元素id
    {
        if (!WTreeBLL.IsExistsNumber(_thnumber))
        {
            return "<?xml version=\"1.0\" encoding=\"utf-8\"?><Root><Error>" + GetTran("007320", "您输入的会员编号不存在") + "！</Error></Root>";
        }

        if (IsRoot(Request.QueryString["thnumber"], ExpectNum, EndNumber) == false)
        {
            //判断所有的可用网络中是否含有权限。
            DataTable ky = WTreeBLL.GetKYWL(Session["Company"].ToString(), "1");
            string maxqs = WTreeBLL.GetMaxQS();
            string ts = GetTran("007461", " 您没有权限查看");

            for (int i = 0; i < ky.Rows.Count; i++)
            {
                //if (Request.QueryString["EndNumber"].ToString() != ky.Rows[i]["number"].ToString())
                {
                    if (IsRoot(Request.QueryString["thnumber"], maxqs, ky.Rows[i]["number"].ToString()))
                    {
                        ts = GetTran("007462", " 您查看的这期中没有这个会员，所以不能查看该网路！");
                        break;
                    }
                }
            }


            Session["WLTCS_C_A_QS"] = null;
            Session["WLTCS_C_A"] = null;

            return "<?xml version=\"1.0\" encoding=\"utf-8\"?><Root><Error>" + ts + "</Error></Root>";
        }
        else
        {
            return WTreeBLL.GetTree(nodeid, ExpectNum, _thnumber, model, BrowserType, IsPlacement, ManageNum,Session["LanguageCode"].ToString());
        }
    }

    //插入图片
    public string SetImage(string thnumber, string img, string ManageNum)
    {
        return WTreeBLL.SetImage(thnumber, img, ManageNum);
    }
    //变色
    public string SetColor(string thnumber, string model, string ExpectNum, string tuanNumber, string ManageNum)
    {
        return WTreeBLL.SetColor(thnumber, model, ExpectNum, tuanNumber, ManageNum);
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["WLTCS_C_A"] = cengs.SelectedValue;
    }

    //判断是否有权限查看该网咯
    public bool IsRoot(string StartNumber, string qs, string EndNumber)
    {
        return WTreeBLL.IsRoot(StartNumber, qs, EndNumber);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Session["WLTCS_C_A_QS"] = DDLQs.SelectedValue;

        string str = txNumber.Text.Trim();
        if (str != "")
        {
            //判断所有的可用网络中是否含有权限。
            DataTable ky = WTreeBLL.GetKYWL(Session["Company"].ToString(), "1");

            for (int i = 0; i < ky.Rows.Count; i++)
            {
                //if (Request.QueryString["EndNumber"].ToString() != ky.Rows[i]["number"].ToString())
                {
                    if (IsRoot(str, DDLQs.SelectedValue, ky.Rows[i]["number"].ToString()))
                    {
                        Response.Redirect("SST_AZ.aspx?number=" + GetNumberParent(str) + "&thNumber=" + str + "&ExpectNum=" + DDLQs.SelectedValue + "&EndNumber=" + ky.Rows[i]["number"]);
                    }
                }
            }
            //end


            Response.Redirect("SST_AZ.aspx?number=" + GetNumberParent(str) + "&thNumber=" + str + "&ExpectNum=" + DDLQs.SelectedValue + "&EndNumber=" + EndNumber);
        }

    }

    public void BindQS()
    {
        DataTable dt = WTreeBLL.BindQS();

        DDLQs.DataSource = dt;
        DDLQs.DataTextField = "ExpectNum";
        DDLQs.DataValueField = "ExpectNum";
        DDLQs.DataBind();

        if (Session["WLTCS_C_A_QS"] + "" != "")
            DDLQs.SelectedValue = Session["WLTCS_C_A_QS"].ToString();
    }

    public void SetLianLuTu(string EndNumber, string StartNumber, string Qs)
    {
        LitLLT.Text = WTreeBLL.SetLianLuTu(EndNumber, StartNumber, Qs);
    }
}
