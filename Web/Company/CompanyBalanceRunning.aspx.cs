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
using Model.Other;
using BLL.CommonClass;
using BLL.MoneyFlows;
using System.Threading;
using BLL.other.Company;
using System.Data.SqlClient;
public partial class Company_CompanyBalanceRunning : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.FinanceXitongjiesuan, false);
        string Operatenum = CommonDataBLL.OperateBh;
        string Operateip = CommonDataBLL.OperateIP;
        int qishu = 0;
        if (Request.QueryString["qs"] != null)
        {
            qishu = Convert.ToInt32(Request.QueryString["qs"]);
        }
        if (qishu == BLL.CommonClass.CommonDataBLL.GetMaxqishu())
        {
            BlackListBLL.GetSystemClose(Operateip, Operatenum);
        }
		RunExe myRun = new RunExe();
        //if ( myRun.IsRun(  CommonDataBLL.JiesuanProgramFilename  ) )
        if (Request.QueryString["id"] != null)
        {
            ViewState["id"] = Request.QueryString["id"].ToString();
        }
        string jstype = CommonDataBLL.GetJstypeID(ViewState["id"].ToString());

        if (jstype == "0")
		{
			Response.Write("<body bgcolor=#616378><div id='mydiv'>");
			Response.Write("_");
			Response.Write("</div></body>");
			Response.Write("<script>mydiv.innerText = '';</script>");
			Response.Write("<script language=javascript>;");
			Response.Write("var dots = 0;var dotmax = 10;function ShowWait()");
            Response.Write("{var output; output = '" + GetTran("001234", "程序正在运行") + "';dots++;if(dots>=dotmax)dots=1;");
			Response.Write("for(var x = 0;x < dots;x++){output += '·';}mydiv.innerText =  output;}");
			Response.Write("function StartShowWait(){mydiv.style.visibility = 'visible'; ");
			Response.Write("window.setInterval('ShowWait()',1000);}");
//				Response.Write("function HideWait(){mydiv.style.visibility = 'hidden';");
//				Response.Write("window.clearInterval();}");
			Response.Write("StartShowWait();</script>");
			Response.Write ("<script>window.setTimeout('location=location',5000)</script>");
			Response.Flush();
			Thread.Sleep(10000);
		}
		else
		{
			string cuowu = "";
            //是否有错误单子
            //int err = ReleaseBLL.IsErrorOrder(Convert.ToInt32(Session["nowqishu"].ToString()));
			Application["jinzhi"]  = "F" ;
            bool re = ReleaseBLL.CheckSetsys();
            if (!re)
            {
                bool res = ReleaseBLL.UpdateSystemID();
                if (res) {
                    bool r = ReleaseBLL.DelSetsys();
                }
            }
            //if ( err>0 )
            //{
            //    cuowu = GetTran("001235", "但是本期中有错误单子。");
            //}
            if (jstype == "1")
            {
                cuowu = "正常结束。";
            }
            else if (jstype == "2")
            {
                cuowu = "错误报单结束。";
            }
            else if (jstype == "3")
            {
                cuowu = "异常结束。";
            }
            else
            {
                cuowu = "未启动。";
            }

            //if (cuowu == "")
            if (jstype == "1")
            {
                ReleaseBLL.UPConfigflag(int.Parse(Request.QueryString["qs"]));
            }
            ReleaseBLL.UPConfigNum(int.Parse(Request.QueryString["qs"]));
            Response.Write("<script language='javascript'> alert('" + GetTran("001236", "程序运行完毕！") + cuowu + "');window.close()</script>");
		}
        if (qishu == BLL.CommonClass.CommonDataBLL.GetMaxqishu())
        {
            BlackListBLL.GetSystemOpen(Operateip, Operatenum);
        }
	}
}
