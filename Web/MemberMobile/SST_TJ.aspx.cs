using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Xml;
using BLL.other;
using DAL;

public partial class _SST_AZ : BLL.TranslationBase
{
    //点击者的编号的父编号（父编号就是他的安置编号）
    protected string Number = "";
    //自身编号，点击者的编号
    protected string ThNumber = "";
    //可查看的网路（结束编号）
    protected string EndNumber = "";


    public string jibie;
    public string nicheng;
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);



        if (Request.QueryString["topnum"] == null)  // 首次加载
            ThNumber =  Session["member"].ToString();
        else
            ThNumber = Request.QueryString["topnum"];


        EndNumber = Session["member"].ToString();
        string Qs = BLL.CommonClass.CommonDataBLL.getMaxqishu().ToString();
          //获取链路图
            if (WTreeBLL.IsExistsNumber(ThNumber) && IsRoot(ThNumber, Qs, EndNumber))
            {
                SetLianLuTu(EndNumber, ThNumber, Qs);
                Loadnumlist(ThNumber, EndNumber);
            }
       
        
         
    }
    public void Loadnumlist(string topnum ,string endnum)
    {  string Qs = BLL.CommonClass.CommonDataBLL.getMaxqishu().ToString();
    DataTable dtt = GetTree("0", Qs, topnum, "", "0", "0", endnum);
    string str = "";
        foreach (DataRow item in dtt.Rows)
    {
        string number = item["MobileTele"].ToString();
        string petname = item["name"].ToString();
        string tjrs = item["ZongRen"].ToString();
        string TotalNetRecord = item["DTotalNetRecord"].ToString();
       // str += "  <li><a href='SST_TJ.aspx?topnum=" + number + "' ><div>" + number + "</div><div>" + petname + "</div><div style='line-height: 20px;text-align:right;'>" + tjrs + "人<br/>" + TotalNetRecord + "</div></a></li>";
        str += "  <li><a ><div>" + number + "</div><div>" + petname + "</div><div style='line-height: 20px;text-align:right;'>" + tjrs + "人<br/>" + TotalNetRecord + "</div></a></li>";

    }
        if (str == "") { 
           str += "  <li><a   ><div>无</div><div>无</div><div>0人</div></a></li>";
         //  str += "  <li><a   ><div>无</div><div>无 </div><div>0人</div></a></li>";
        }

        litmemberlist.Text = str;
    }
    private void translation()
    {
        //TranControls(Button1, new string[][] { new string[] { "000177", "显示" } });
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
    public DataTable GetTree(string nodeid, string ExpectNum, string _thnumber, string model, string BrowserType, string IsPlacement, string ManageNum) //nodeid 为父元素id
    {
        if (!WTreeBLL.IsExistsNumber(_thnumber))
        {
            return  null;
        }

        if (IsRoot(_thnumber, ExpectNum, EndNumber) == false)
        {
            //判断所有的可用网络中是否含有权限。
            string maxqs = WTreeBLL.GetMaxQS();
            string ts = GetTran("007461", " 您没有权限查看");

            if (IsRoot(_thnumber, maxqs, EndNumber))
            {
                ts = GetTran("007462", " 您查看的这期中没有这个会员，所以不能查看该网路！");

            }

            Session["WLTCS_M_A_QS"] = null;
            Session["WLTCS_M_A"] = null;

            return null;
        }
        else
        {
            //string sql = "select MobileTele from MemberInfo where number='" + Session["member"].ToString() + "'";
            //DataTable shj = DBHelper.ExecuteDataTable(sql);
            //_thnumber = shj.Rows[0][0].ToString();
            return WTreeBLL.GetTreePhone(nodeid, ExpectNum, _thnumber, model, BrowserType, "0", ManageNum, Session["LanguageCode"].ToString());
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
       // Session["WLTCS_M_A"] = cengs.SelectedValue;
    }

    //判断是否有权限查看该网咯
    public bool IsRoot(string StartNumber, string qs, string EndNumber)
    {
        return WTreeBLL.IsRoot(StartNumber, qs, EndNumber);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
       // Session["WLTCS_M_A_QS"] = DDLQs.SelectedValue;

       // string str = txNumber.Text.Trim();
        //if (str != "")
        //{
        //    Response.Redirect("SST_AZ.aspx?number=" + GetNumberParent(str) + "&thNumber=" + str + "&ExpectNum=" + DDLQs.SelectedValue);
        //}
    }

    public void BindQS()
    {
        DataTable dt = WTreeBLL.BindQS();

        //DDLQs.DataSource = dt;
        //DDLQs.DataTextField = "ExpectNum";
        //DDLQs.DataValueField = "ExpectNum";
        //DDLQs.DataBind();

        //if (Session["WLTCS_M_A_QS"] + "" != "")
        //    DDLQs.SelectedValue = Session["WLTCS_M_A_QS"].ToString();
    }

    public void SetLianLuTu(string EndNumber, string StartNumber, string Qs)
    {
        LitLLT.Text = WTreeBLL.SetLianLuTu_IIPhone(EndNumber, StartNumber, Qs);
    }
}