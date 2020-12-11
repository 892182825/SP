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

///Add Namespace
using BLL.CommonClass;
using BLL.other.Member;
using System.IO;
using System.Globalization;
using System.Text;
using Standard.Classes;

public partial class Member_DownLoadFiles : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckMemberPermission();

        ///设置GridView的样式
        gvResources.Attributes.Add("style", "word-break:keep-all;word-wrap:normal");

        if (!Page.IsPostBack)
        {
            btnSearch_Click(null, null);
        }

        Translations_More();
    }

    protected void Translations_More()
    {
        TranControls(gvResources, new string[][] 
                        {
                            new string[] { "000245","下载"}, 
                            new string[] { "000272","资料编号"}, 
                            new string[] { "000204","资料名称"}, 
                            new string[] { "000278","对应文件名"}, 
                            new string[] { "000280","资料简介"}, 
                            new string[] { "000282","文件大小"},
                            new string[] { "000287","下载次数"}
                        }
                    );

        TranControls(btnSearch, new string[][] 
                        {
                            new string[] { "000048","搜 索"}                             
                        }
            );
    }

    /// <summary>
    /// 初始化查询条件
    /// </summary>
    private void GetShopList()
    {
        int Lev = Convert.ToInt32(BLL.CommonClass.CommonDataBLL.GetBalanceLevel(Session["Member"].ToString()).Rows[0][0]);

        string condition = "1<2 and DownTarget <> 1 and (DownMenberLev=0 or DownMenberLev=" + Lev + ")";

        if (this.txt_member.Text != "")
        {
            condition += " and ResName like '%" + this.txt_member.Text.Trim().Replace("'", "") + "%'";
        }

        if (this.Txt_Name.Text != "")
        {
            condition += " and FileName like '%" + this.Txt_Name.Text.Trim().Replace("'", "") + "%'";
        }

        DataTable dt = DownLoadFilesBLL.GetResourcesInfoByConditions("*", condition);

        gvResources.DataSource = dt;
        gvResources.DataBind();

        Pager1.PageBind(0, 10, " Resources", "ResID, ResName, FileName, ResDescription, ResSize, ResDateTime, ResTimes", condition, "ResID", "gvResources");
    }

    /// <summary>
    /// GridView行事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvResources_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow row = (GridViewRow)((Image)e.CommandSource).NamingContainer;

        string filename = "";

        string rid = e.CommandArgument.ToString();
        object o_fn = DAL.DBHelper.ExecuteScalar("select FileName from Resources where ResID=" + rid);
        if (o_fn != null)
            filename = o_fn.ToString();
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert("您下载的文件已不存在！"));
            return;
        }

        if (e.CommandName.ToLower().Trim() == "download")
        {
            string dlDir = "../Company/upLoadRes/";
            string path = Server.MapPath(dlDir + filename);
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(file.Name).Replace('+', ' '));
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.WriteFile(file.FullName);

                DownLoadFilesBLL.UpdResourcesResTimesByResID(Convert.ToInt32(rid));
                Response.End();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("000666", "您下载的文件不在文件夹中")));
            }
        }
    }
//    public void DownFile(string filePath, string fileName)
//    {
//        // filePath  文件路径 例如：/File/记录.xlsx 
//        // fileName  文件名称 例如：记录.xlsx （要后缀哦） 6 Encoding encoding; // 申明编码 7string outputFileName; // 输出名字 8 Debug.Assert(HttpContext.ApplicationInstance.Request.UserAgent != null, "HttpContext.ApplicationInstance.Request.UserAgent != null");
//        string browser = HttpContext.ApplicationInstance.Request.UserAgent.ToUpper();
//        // 微软的浏览器和ie过滤11if (browser.Contains("MS") && browser.Contains("IE"))
//        {
//            outputFileName = HttpUtility.UrlEncode(filePath);
//            encoding = Encoding.Default;
//        }
//        //火狐17elseif (browser.Contains("FIREFOX"))
//        {
//            outputFileName = fileName;
//            encoding = Encoding.GetEncoding("GB2312");
//        }
//else{
//            outputFileName = HttpUtility.UrlEncode(fileName);
//            encoding = Encoding.Default;
//        }
//        string absoluFilePath = Server.MapPath(filePath); //获取上传文件路径29 FileStream fs = new FileStream(absoluFilePath, FileMode.Open);
//        byte[] bytes = newbyte[(int)fs.Length];
//        fs.Read(bytes, 0, bytes.Length);
//        fs.Close(); //关闭流，释放资源33HttpContext.ApplicationInstance.Response.Clear();
//        HttpContext.ApplicationInstance.Response.Buffer = true;
//        HttpContext.ApplicationInstance.Response.ContentEncoding = encoding;
//        HttpContext.ApplicationInstance.Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", string.IsNullOrEmpty(outputFileName) ? DateTime.Now.ToString("yyyyMMddHHmmssfff") : outputFileName));
//        Response.BinaryWrite(bytes);
//        Response.Flush();
//        HttpContext.ApplicationInstance.Response.End();
//    }


    /// <summary>
    /// 搜索
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetShopList();

    }

    /// <summary>
    /// 行绑定事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvResources_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ///控制样式
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
        }

    }
}