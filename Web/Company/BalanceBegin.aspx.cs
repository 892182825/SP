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
using BLL.MoneyFlows;
using BLL.CommonClass;
using System.IO;
using Model.Other;
using System.Collections.Generic;
using System.Data.SqlClient;


public partial class Company_BalanceBegin : BLL.TranslationBase
{
    List<string> list = new List<string>();
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        //检查相应权限
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.FinanceXitongjiesuan);

        //判断发放期数中是否存在所选期
        int count = ReleaseBLL.IsSuperintendent(Convert.ToInt32(Request.QueryString["qs"]));
        if (count > 0)
        {
            Response.Write("<script>alert('" + GetTran("001204", "发放期数中存在所选期的，请撤销！") + "');window.opener.location='SalaryGrant.aspx';window.close();</script>");
            return;
        }

        //----------------------------------------------

        //-----------------------------------------------

        //RunExe myRun = new RunExe();
        //if (myRun.IsRun(CommonDataBLL.JiesuanProgramFilename))
        DataTable dt = CommonDataBLL.GetJstype();

        if (dt.Rows.Count > 0)
        {
            ViewState["jsType"] = dt.Rows[0]["jstype"].ToString();
            ViewState["jsId"] = dt.Rows[0]["id"].ToString();

            if (ViewState["jsType"].ToString() == "0")
            {
                this.reCount.Visible = true;
                this.beginCount.Visible = false;
            }
            else
            {
                this.reCount.Visible = false;
                this.beginCount.Visible = true;

                messageLabel.Text = GetTran("001205", "点击上面按钮后，请注意提示信息！");
            }
        }
        else
        {
            ViewState["jsType"] = "1";

            this.reCount.Visible = false;
            this.beginCount.Visible = true;

            messageLabel.Text = GetTran("001205", "点击上面按钮后，请注意提示信息！");
        }


        this.beginCount.Attributes["onclick"] = "bClick(this);";

        if (Request.QueryString["qs"] != null) Session["nowqishu"] = Request.QueryString["qs"].Trim();
        else
        {
            this.messageLabel.Text = GetTran("001206", "参数错误！");
            Response.End();
        }
        Translations();

    }
    private void Translations()
    {
        this.TranControls(this.CheckJie,
                new string[][]{
                    new string []{"005946","保存计算差异"},
                    new string []{"001217","周结算"},
                    new string []{"001218","月结算"}
                    

                });
        this.TranControls(this.beginCount, new string[][] { new string[] { "001219", "开始结算" } });
        this.TranControls(this.reCount, new string[][] { new string[] { "001220", "停止程序" } });

        this.TranControls(this.lkbtn_JJYFTrue, new string[][] { new string[] { "001221", "已发" } });
        this.TranControls(this.lkbtn_JJGotTrue, new string[][] { new string[] { "001223", "已添加" } });

        this.TranControls(this.Button1, new string[][] { new string[] { "001225", "关闭窗口" } });
    }
    protected bool setExeParam() //设置外部程序参数配置文件
    {
        string id = CommonDataBLL.InsJsInfo(Convert.ToInt32(Session["nowqishu"].ToString()), CommonDataBLL.OperateIP, Session["Company"].ToString());
        ViewState["newjsid"] = id;

        string server = DAL.DBHelper.connString;
        char[] de = { ';', '=' };
        string[] dd = server.Split(de);

        string host = dd[1].Replace("(", "").Replace(")", "");

        string exeParam = "," + Session["nowqishu"].ToString() + "," +
            host + "," + dd[5] + "," + dd[7] + "," + dd[3] + "," + id + ",";
        exeParam = exeParam + "";

        //		添加结算时间
        exeParam = exeParam + this.jisuan();

        try
        {
            FileStream fs = new FileStream(Server.MapPath("jiesuan\\config.txt"), FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter streamWriter = new StreamWriter(fs);
            streamWriter.WriteLine(exeParam);
            streamWriter.Close();
            fs.Close();
        }
        catch (Exception ex)
        {
            this.messageLabel.Text = GetTran("001207", "文件操作产生错误！结算程序需要网站目录下\"jiesuan \"的写操作权限，请正确设置") + "<br>";
            this.messageLabel.Text += GetTran("001208", "(提示：开放Windows内置用户ASPNET对jiesuan目录的写权限)") + "<br>";
            this.messageLabel.Text += ex.Message;
            return false;
        }

        return true;

    }
    //-----------------------------------------------
    protected void beginCount_Click(object sender, System.EventArgs e)
    {
        if (Page.IsValid)
        {
            Application.UnLock();
            Application.Lock();
            //Application["jinzhi"] = "DHJ";
            Application.UnLock();

            BeginBalanceProg();
        }

    }
    protected void BeginBalanceProg()
    {
        //检测是否存在没有会员编号的店铺
        int exists = ReleaseBLL.IsNumberExists();
        if (exists > 0)
        {
            Response.Write(Transforms.ReturnAlert(GetTran("001210", "对不起，当前系统中还有店铺没有会员编号，请先在门店管理页面中为这些店添加正确的会员编号！")));
            Application["jinzhi"] = "F";
            Response.End();
        }
        bool re = ReleaseBLL.CheckSetsys();
        if (!re)
        {
            bool r = ReleaseBLL.DelSetsys();
            if (!r) {
                ScriptHelper.SetAlert(Page, "系统开关不确定！请先确认系统开关。");
                return;
            }

        }
        if (!ReleaseBLL.GetSystemList())
        {
            ScriptHelper.SetAlert(Page, "系统开关不确定！请先确认系统开关。");
            return;
        }

        bool res = ReleaseBLL.UpdateSystem();
        if (!res)
        {
            ScriptHelper.SetAlert(Page, "系统开关不确定！请先确认系统开关。");
            return;
        }
        RunExe myRun = new RunExe();

        //if (!myRun.IsRun(CommonDataBLL.JiesuanProgramFilename))

        if (ViewState["jsType"].ToString() != "0")
        {
            if (setExeParam())
            {
                if (myRun.RunIt(Server.MapPath("jiesuan\\" + CommonDataBLL.JiesuanProgramFilename + ".exe")))
                {
                    Response.Write("<script>location.href('CompanyBalancerunning.aspx?Qishu=QC888&qs=" + Request.QueryString["qs"] + "&id=" + ViewState["newjsid"].ToString() + "')</script>");
                }
                else
                {
                    Application["jinzhi"] = "F";
                    Response.Write("<script> alert('" + GetTran("001211", "程序启动失败") + "');</script>");
                }
            }
            else
            {

            }

        }
        else
        {

        }
    }

    protected void reCount_Click(object sender, EventArgs e)
    {
        RunExe myRun = new RunExe();
        myRun.Kill(CommonDataBLL.JiesuanProgramFilename);
        int a = CommonDataBLL.UpJstype(ViewState["jsId"].ToString());
        bool re = ReleaseBLL.CheckSetsys();
        if (!re) {
            bool res = ReleaseBLL.UpdateSystemID();
            if (!res)
            {
                Response.Write("<meta http-equiv=refresh content=0><script> alert('" + GetTran("001214", "程序停止失败") + "'); </script>");
                return;
            }
            else {
                bool r = ReleaseBLL.DelSetsys();
                if (!r) {
                Response.Write("<meta http-equiv=refresh content=0><script> alert('" + GetTran("001214", "程序停止失败") + "'); </script>");
                return;  
                }
            }
        }
		if(a>0)
        {
            Response.Write("<meta http-equiv=refresh content=0><script> alert('" + GetTran("001212", "程序停止成功") + "'); </script>");
        }
        else
        {
            Response.Write("<meta http-equiv=refresh content=0><script> alert('" + GetTran("001214", "程序停止失败") + "'); </script>");
        }
        Application["jinzhi"] = "F";
    }

    protected void Button1_Click(object sender, System.EventArgs e)
    {
        Application["jinzhi"] = "F";
        Response.Write("<script language='javascript'>window.close();</script>");
    }

    protected void lkbtn_JJYFTrue_Click(object sender, System.EventArgs e)
    {
        try
        {
            //撤销电子账户
            ReleaseBLL.Backout(Convert.ToInt32(Session["nowqishu"]));
            BeginBalanceProg();
        }
        catch
        {
            Response.Write(Transforms.ReturnAlert(GetTran("001215", "对不起，撤消电子帐户失败，请联系维护人员！")));
        }
    }

    protected void lkbtn_JJGotTrue_Click(object sender, System.EventArgs e)
    {
        Response.Redirect("SalaryGrant.aspx");
        try
        {
            //撤销电子账户
            int num = ReleaseBLL.CancleBonus(Convert.ToInt32(Session["nowqishu"].ToString()));
            if (num == 0)
            {
                Response.Write(Transforms.ReturnAlert(GetTran("001215", "对不起，撤消电子帐户失败，请联系维护人员！")));
            }
            BeginBalanceProg();
        }
        catch
        {
            Response.Write(Transforms.ReturnAlert(GetTran("001215", "对不起，撤消电子帐户失败，请联系维护人员！")));
        }

    }

    public String jisuan()
    {
        //获取结算时间
        //周结算 xunjiesuan 
        //月结算 yuejiesuan

        int xunjiesuan = 0;
        int yuejiesuan = 0;
        int chayi = 0;
        if (this.CheckJie.Items[1].Selected)
            xunjiesuan = 1;
        if (this.CheckJie.Items[2].Selected)
            yuejiesuan = 1;
        if (this.CheckJie.Items[0].Selected)
            chayi = 1;

        return chayi.ToString() + "," + xunjiesuan.ToString() + "," + yuejiesuan.ToString() + ",";
    }

}
