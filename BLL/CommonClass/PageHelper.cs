using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

///Add Namespace
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/*
 * 修改者：  汪  华
 * 修改时间：2009-09-24 
 */

namespace BLL.CommonClass
{
    public class PageHelper
    {
        /// <summary>
        /// 设置 Web Control 焦点函数
        /// </summary>
        /// <param name="page">当前Web页对象</param>
        /// <param name="control">要设置获得焦点的控件对象</param>
        public static void SetFocus(Page page, WebControl control)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "focus", "<script>document.all['" + control.ClientID + "'].focus();</script>");
        }

        /// <summary>
        /// 返回能够弹出提示框的 javascript 代码
        /// </summary>
        /// <param name="content">要提示的内容</param>
        /// <returns>javascript 代码</returns>
        public static string ReturnAlert(string content)
        {
            string retVal;

            retVal = "<script language='javascript'>alert('" + content + "')</script>";

            return retVal;
        }

        /// <summary>
        /// 弹出警告(提示)框函数
        /// </summary>
        /// <param name="page">Web页对象</param>
        /// <param name="content">要提示的内容</param>
        /// <returns>正常返回值true</returns>
        public static bool ShowMessagebox(Page page, string content)
        {
            // HttpContext.Current.Response.Write(ReturnAlert(content));
            page.Response.Write(ReturnAlert(content));
            return true;
        }

        /// <summary>
        /// 页面重定向,不关闭当前页面
        /// </summary>
        /// <param name="page">Web页对象</param>
        /// <param name="urlString">要定向的链接地址</param>
        /// <returns></returns>
        public static bool Redirect(Page page, string urlString)
        {
            page.Response.Write("<script language='javascript'>location.href='" + urlString + "'</script>");

            return true;
        }

        /// <summary>
        /// 点击时给出提示，确定才继续执行
        /// </summary>
        /// <param name="page">Web页对象</param>
        /// <param name="control">要设置获得提示的控件对象</param>
        /// <param name="content">要提示的内容</param>
        /// <returns></returns>
        public static bool SetConfirmWindow(Page page, WebControl control, string content)
        {
            string strScript = "return window.confirm('" + content + "');";
            control.Attributes.Add("onClick", strScript);

            return true;
        }

        /// <summary>
        /// 返回网页对话框函数
        /// </summary>
        /// <param name="page">Web页对象</param>
        /// <param name="control">要设置弹出Web页对话框的控件对象</param>
        /// <param name="url">url</param>
        /// <returns></returns>
        public static bool WebDialog(Page page, WebControl control, string url)
        {
            StringBuilder scriptBuilder = new StringBuilder();

            //	showModalDialog("pub.aspx",window,"dialogWidth:300px;dialogHeight:300px");
            scriptBuilder.Append("showModalDialog('");
            scriptBuilder.Append(url);
            scriptBuilder.Append("',");
            scriptBuilder.Append("window,");
            scriptBuilder.Append("'dialogWidth:300px;dialogHeight:300px'");

            string strScript = "showModalDialog('";
            strScript += url;
            strScript += "', window,'dialogWidth:245px;dialogHeight:378px;status:0;scroll:0;help:0;')";
            //string strScript = "showModalDialog('Hello.aspx', window,'dialogWidth:245px;dialogHeight:378px;status:0;scroll:0;help:0;')";

            control.Attributes["onclick"] = strScript;

            return true;
        }

        /// <summary>
        /// 为指定控件,设置单击弹出模态网页对话框,待测
        /// </summary>
        /// <param name="page"></param>
        /// <param name="control"></param>
        /// <param name="url">url</param>
        /// <param name="width">宽度:300px</param>
        /// <param name="height">高度:300px</param>
        /// <returns></returns>
        public static bool SetWebDialog(Page page, WebControl control, string url, string width, string height)
        {
            StringBuilder scriptBuilder = new StringBuilder();

            // ref:	showModalDialog("pub.aspx",window,"dialogWidth:300px;dialogHeight:300px");
            scriptBuilder.Append("showModalDialog('");
            scriptBuilder.Append(url);
            scriptBuilder.Append("',");
            scriptBuilder.Append("window,");
            scriptBuilder.Append("'dialogWidth:");
            scriptBuilder.Append(width);
            scriptBuilder.Append("px;dialogHeight:");
            scriptBuilder.Append(height);
            scriptBuilder.Append("px;");
            scriptBuilder.Append("status:0;scroll:0;help:0;')");

            control.Attributes["onclick"] = scriptBuilder.ToString();

            return true;
        }

        /// <summary>
        /// 添加查询串
        /// </summary>
        /// <param name="page">页面对象</param>
        /// <param name="queryString"></param>
        /// <param name="v"></param>
        public static string AddQueryString(Page page, string queryString, string v)
        {
            string urlResult = string.Empty;

            if (page.Request.QueryString[queryString] != null)
            {
                urlResult = page.Request.Url.ToString().Replace(queryString + "=" + page.Request.QueryString[queryString],
                    queryString + "=" + v);
            }
            else
            {
                if (page.Request.QueryString.Count > 0)
                    urlResult = page.Request.Url + "&" + queryString + "=" + v;
                else
                    urlResult = page.Request.Url + "?" + queryString + "=" + v;
            }

            return urlResult;
        }

        #region function CloseWebPage: 关闭Web页窗口函数
        /// <summary>
        /// 关闭Web页窗口函数
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static bool CloseWebPage(Page page)
        {
            page.Response.Write("<script language='javascript'>window.opener=null;window.close();</script>");

            return true;
        }
        #endregion
    }
}
