using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using DAL;

/// <summary>
///ProcessRequest 的摘要说明
/// </summary>
public class ProcessRequest
{
    public ProcessRequest()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    #region SQL注入式攻击代码分析

    /**/
    /// <summary>
    /// 处理用户提交的请求
    /// </summary>
    public void StartProcessRequest()
    {
        string sqlErrorPage = "../sqlErrorPage.aspx";
        try
        {
            string getkeys = "";
            if (System.Web.HttpContext.Current.Request.QueryString != null)
            {

                int x = 0;
                string Keys = "";
                for (int i = 0; i < System.Web.HttpContext.Current.Request.QueryString.Count; i++)
                {
                    getkeys = System.Web.HttpContext.Current.Request.QueryString.Keys[i];
                    if (!ProcessSqlStr(System.Web.HttpContext.Current.Request.QueryString[getkeys].ToLower()))
                    {
                        x++;
                        Keys += "  " + getkeys + "=" + System.Web.HttpContext.Current.Request.QueryString[getkeys].ToString();
                    }
                }

                if (x > 0)
                {

                    string ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    string page = System.Web.HttpContext.Current.Request.ServerVariables["URL"].ToString();

                    //if (page.IndexOf("docprintwupin.aspx") > 0 || page.IndexOf("docprintbilloutchanpin.ascx") > 0 || page.IndexOf("docPrint.aspx") > 0 || page.IndexOf("docprintzren.ascx") > 0 || page.IndexOf("docprintzhengpin.ascx") > 0 || page.IndexOf("docprintbilloutwupin.ascx") > 0 || page.IndexOf("DocPrintBillOut.ascx") > 0)
                    //{

                    //}
                    //else
                    //{

                        SqlParameter[] paras = new SqlParameter[4];
                        paras[0] = new SqlParameter("@page", page);
                        paras[1] = new SqlParameter("@ip", ip);
                        paras[2] = new SqlParameter("@time", DateTime.Now);
                        paras[3] = new SqlParameter("@key", Keys);
                        DBHelper.ExecuteNonQuery("insert into SqlError(Page,Ip,ErrorTime,Keys) values(@page,@ip,@time,@key) ", paras, CommandType.Text);

                        System.Web.HttpContext.Current.Response.Redirect(sqlErrorPage);
                        System.Web.HttpContext.Current.Response.End();
                   // }

                }

            }

            //if (System.Web.HttpContext.Current.Request.Form != null)
            //{
            //    for (int i = 0; i < System.Web.HttpContext.Current.Request.Form.Count; i++)
            //    {
            //        getkeys = System.Web.HttpContext.Current.Request.Form.Keys[i];
            //        if (!ProcessSqlStr(System.Web.HttpContext.Current.Request.Form[getkeys].ToLower()))
            //        {
            //            System.Web.HttpContext.Current.Response.Redirect(sqlErrorPage);
            //            System.Web.HttpContext.Current.Response.End();
            //        }
            //    }
            //}

        }
        catch (Exception ex)
        {
            string eee = ex.Message;
            return;
        }
    }
    /**/
    /// <summary>
    /// 分析用户请求是否正常
    /// </summary>
    /// <param name="Str">传入用户提交数据</param>
    /// <returns>返回是否含有SQL注入式攻击代码</returns>
    public bool ProcessSqlStr(string Str)
    {
        bool ReturnValue = true;
        try
        {
            if (Str != "" && Str != null)
            {
                string SqlStr = "";
                if (SqlStr == "" || SqlStr == null)
                {
                    SqlStr = "'|and|or|exec|insert|select|delete|update|count|*|chr|mid|master|truncate|char|declare|xp_cmdshell";
                }
                string[] anySqlStr = SqlStr.Split('|');
                foreach (string ss in anySqlStr)
                {
                    if (Str.IndexOf(ss) >= 0)
                    {
                        ReturnValue = false;
                    }
                }
            }
        }
        catch
        {
            ReturnValue = false;
        }
        return ReturnValue;
    }
    #endregion
}
