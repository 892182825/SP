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
/// <summary>
///ScriptHelper 的摘要说明
/// </summary>
public class ScriptHelper
{
    public ScriptHelper()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    /// <summary>
    /// 弹出一个提示框
    /// </summary>
    /// <param name="message"></param>
    public static void SetAlert(System.Web.UI.Control control, string message)
    {
        try
        {
            Literal lit = (Literal)control;
            lit.Text = "<script>alert(\"" + message + "\");</script>";
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(control, control.GetType(), "aaa", "<script>alert(\"" + message + "\");</script>", false);
        }
        //HttpContext.Current.Response.Write("<script>alert(\""+message+"\");</script>");
    }
    /// <summary>
    /// 弹出一个对话框，并打开传入的URL
    /// </summary>
    /// <param name="control"></param>
    /// <param name="message"></param>
    /// <param name="URL"></param>
    public static void SetAlert(System.Web.UI.Control control,string message, string URL)
    {
        try
        {
            Literal lit = (Literal)control;
            lit.Text = "<script>alert(\"" + message + "\");location.href='" + URL + "'</script>";
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(control, control.GetType(), "aaa", "<script>alert(\"" + message + "\");location.href='" + URL + "'</script>", false);
        }
        
        //HttpContext.Current.Response.Redirect(URL);
    }
    /// <summary>
    /// 弹出一个提示框 --ds2012--www-b874dce8700
    /// </summary>
    /// <param name="page"></param>
    /// <param name="message"></param>
    public static void SetAlert(Page page, string message)
    {
       page.ClientScript.RegisterStartupScript(page.GetType(), "aaa", "<script>alert(\"" + message + "\");</script>", false);
    }
    /// <summary>
    /// 弹出一个对话框，并打开传入的URL
    /// </summary>
    /// <param name="control"></param>
    /// <param name="message"></param>
    /// <param name="URL"></param>
    public static void SetAlert(Page page, string message, string URL)
    {
        ScriptManager.RegisterClientScriptBlock(page,page.GetType(), "aaa", "<script>alert(\"" + message + "\");location.href='" + URL + "'</script>", false);
        //HttpContext.Current.Response.Redirect(URL);
    }
    /// <summary>
    /// 弹出一个对话框，并打开传入的URL
    /// </summary>
    /// <param name="control"></param>
    /// <param name="message"></param>
    /// <param name="URL"></param>
    public static void SetAlert(out string msg, string message, string URL)
    {
        msg = "<script>alert(\"" + message + "\");location.href='" + URL + "'</script>";
    }

    /// <summary>
    /// 弹出一个提示框
    /// </summary>
    /// <param name="page"></param>
    /// <param name="message"></param>
    public static void SetAlert(out string msg, string message)
    {
        msg="<script>alert(\"" + message + "\");</script>";
    }
    /// <summary>
    /// 执行javascript方法
    /// </summary>
    /// <param name="page"></param>
    /// <param name="script"></param>
    /// <param name="addScriptTags"></param>
    public static void SetScript(Page page, string script,bool addScriptTags)
    {
        ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "", script, addScriptTags);
    }
    /// <summary>
    /// 执行javascript方法
    /// </summary>
    /// <param name="page"></param>
    /// <param name="script"></param>
    /// <param name="addScriptTags"></param>
    public static void SetScript(System.Web.UI.Control control,string script,bool addScriptTags)
    {
        ScriptManager.RegisterClientScriptBlock(control, control.GetType(), "", script, addScriptTags);
    }
}
