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
using Model.Other;
using System.Data.SqlClient;
using DAL;
using BLL.CommonClass;
using BLL.other.Company;
using BLL.MoneyFlows;

public partial class Company_BalanceRunning_Proc : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.FinanceXitongjiesuan, false);

        if (!IsPostBack && Request.QueryString["action"] + "" != "ajax")
        {
            string isP = IsExistsPid();

            if (isP != "-1")
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script type='text/javascript'>pgLoad('div2')</script>");
        }

        if (Request.QueryString["action"] + "" == "ajax")
        {
            string err = "-1";
            string Operatenum = CommonDataBLL.OperateBh;
            string Operateip = CommonDataBLL.OperateIP;
            BlackListBLL.GetSystemClose(Operateip, Operatenum);
            try
            {
                //如果正在结算，就先停止结算再结算
                string isP = IsExistsPid();
                if (isP != "-1")
                    DBHelper.ExecuteNonQuery("kill " + isP);
                string id = CommonDataBLL.InsJsInfo(Convert.ToInt32(Request.QueryString["qs"]), CommonDataBLL.OperateIP, Session["Company"].ToString());

                SqlParameter[] param = new SqlParameter[] 
                {
                    new SqlParameter("@qs",Request.QueryString["qs"]),
                    new SqlParameter("@id",id),
                    new SqlParameter("@err",SqlDbType.Int)
                };

                param[2].Direction = ParameterDirection.Output;

                DBHelper.ExecuteNonQuery("[jsjj_yj]", param, CommandType.StoredProcedure);

                err = param[2].Value.ToString();

                if (err == "0")
                {
                    ReleaseBLL.UPConfigflag(int.Parse(Request.QueryString["qs"]));
                }
            }
            catch
            { }
            finally
            {
                BlackListBLL.GetSystemOpen(Operateip, Operatenum);
            }

            Response.Write(err);
            Response.End();
        }
    }

    //判断结算的存储过程是否在运行
    public string IsExistsPid()
    {
        DataTable dt = DBHelper.ExecuteDataTable("select spid from master.dbo.sysprocesses where spid>50 and dbid= db_id('SLYS')");

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            try
            {
                string info = DBHelper.ExecuteDataTable("dbcc inputbuffer(" + dt.Rows[i]["spid"] + ")").Rows[0]["EventInfo"] + "";

                if (info.ToLower().Trim().StartsWith("generatebonusa") || info.ToLower().Trim().StartsWith("exec generatebonusa")) //结算存储过程名
                    return dt.Rows[i]["spid"] + "";
            }
            catch
            { }
        }

        return "-1";
    }

    //结束结算进程
    protected void Button1_Click1(object sender, EventArgs e)
    {
        string isP = IsExistsPid();
        if (isP != "-1")
            DBHelper.ExecuteNonQuery("kill " + isP);
    }
}
