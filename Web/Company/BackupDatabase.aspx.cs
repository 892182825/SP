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
using System.Diagnostics;
using System.Threading;

/// <summary>
/// Add Namespace
/// </summary>
using System.IO;
using BLL.CommonClass;
using BLL.other.Company;
using Model.Other;
using Model;
using System.Text.RegularExpressions;

public partial class Company_BackupDatabase : BLL.TranslationBase
{
    protected string strSerPath;

    /// <summary>
    /// 为GridView排序事件声明变量和赋值
    /// </summary>
    protected static bool blDataBackupID = true;
    protected static bool blDataBackupTime = true;
    protected static bool blPathFileName = true;
    protected static bool blOperatorNum = true;   

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        //检查相应权限
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.SystemBackup);

        ///设置GridView的样式
        gvBackupDatabase.Attributes.Add("style", "word-break:keep-all;word-wrap:normal");

        if (!IsPostBack)
        {
            BackupDatabaseDataBind();
            PageHelper.SetConfirmWindow(this, btnCheckPath, GetTran("004216", "您确定要备份当前数据库吗？"));
        }
        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.gvBackupDatabase,
               new string[][]{
                    new string []{"000015","操作"},
                    new string []{"000012", "序号"},
                    new string []{"004190","备份时间"},
                    new string []{"004189", "路径及文件名"},
                    new string []{"000662", "操作员"}});
        this.TranControls(this.btnCheckPath, new string[][] { new string[] { "004253", "开始备份" } });
        this.TranControls(this.btnToDataBackup, new string[][] { new string[] { "004254", "转到数据备份" } });
    }

	/// <summary>
	/// 该备份在路径是根目录的情况下进行
	/// </summary>
    protected void BackupInRoot()
	{
		Backup();
	}

	/// <summary>
	/// 备份数据库
	/// </summary>
    protected void Backup()
	{
        ///设定要备份的数据库名
        string strDataBaseName = DataBackupBLL.GetDataBaseName();

        ///备份数据库
        int backupCount = DataBackupBLL.BackupDatabaseInfo(strDataBaseName, "D:\\DBBackup\\" + strDataBaseName + ".bak");

		try
		{
			if(backupCount != 0)
            {
                //Process.Start("D:\\DBBackup\\dbcmd.bat"); //调用bat命令文件，将备份的bak数据库文件打包成rar格式的
                //Thread.Sleep(6000); //程序睡眠6秒中

                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("004215", "备份已经成功！")));
                lblMessage.Text = "";
                lblMessage.Text = GetTran("004214", "已经将系统数据库备份为：") + strDataBaseName + ".bak";// +" 建立的时间为： " + File.GetLastWriteTime(strDataBaseName + ".bak");
                BackupLog(strDataBaseName + ".bak");
			}
		}
		catch
		{
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("004213", "备份失败,请联系管理员")));
            return;
		}
	}

	/// <summary>
    /// 记录备份日志
	/// </summary>
	/// <param name="s">文件的完整路径</param>
    protected void BackupLog(string s)
	{
        ///根据管理员编号得到管理员姓名
        string strOperator = DataBackupBLL.GetNameByAdminID(Session["Company"].ToString());
        DateTime strDataBackupTime = DateTime.Now;// File.GetLastWriteTime(s);
        
        BackupDatabaseModel backupDatabaseModel = new BackupDatabaseModel();
        backupDatabaseModel.DataBackupTime =strDataBackupTime;
        backupDatabaseModel.OperatorNum = strOperator;
        backupDatabaseModel.PathFileName = s;

        ///向数据库备份路径表中插入记录
        int addCount = DataBackupBLL.AddBackupDatabase(backupDatabaseModel);

        if (addCount != 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("004212", "备份日志记录成功!")));
            BackupDatabaseDataBind();
        }

        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("004210", "备份日志记录失败，请联络管理员！")));
            return;
        }       
	}

    string table = "BackupDatabase";
    string condition = "1<2";
    string key = "DataBackupID";
    string columns = "DataBackupID,DataBackupTime,PathFileName,OperatorNum";

	/// <summary>
	/// 绑定数据
	/// </summary>
	protected void BackupDatabaseDataBind()
	{
        DataTable dt = DataBackupBLL.GetBackupDatabaseInfo();

        gvBackupDatabase.DataSource = dt;
        gvBackupDatabase.DataBind();

        Pager page = Page.FindControl("uclPager") as Pager;
        page.PageBind(0, 10, table, columns, condition, key, "gvBackupDatabase");        
	}
    
    /// <summary>
    /// 删除选定的数据库备份
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtnDelete_Command(object sender, CommandEventArgs e)
    {
        ///当GridView有数据时才能删除
        if (gvBackupDatabase.Rows.Count != 0)
        {
            int index = gvBackupDatabase.PageIndex;
            //注意：一定要正确取到文件名和路径
            string strPathFileName = gvBackupDatabase.Rows[index].Cells[3].Text;

            try
            {
                File.Delete(strPathFileName);
                ///删除指定文件路径名数据记录
                int delCount = DataBackupBLL.DelBackupDatabaseByFilePathName(strPathFileName);
                if (delCount != 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("000749", "删除成功！")));
                    BackupDatabaseDataBind();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("000417", "删除失败！")));
                }
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001387", "删除失败，请联系管理员！")));
                return;
            }
        }      
    }

    /// <summary>
    /// 下载
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtnDownFile_Command(object sender, CommandEventArgs e)
    {       
        string downFile = e.CommandName.Trim();
        if (downFile == "DownFile")
        {
            int index = gvBackupDatabase.PageIndex;
            string filepath = gvBackupDatabase.Rows[index].Cells[3].Text;
            string psth = "http://192.168.1.253/DBBackup/" + filepath;
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + filepath);
            Response.AddHeader("Content-Length", psth);
            Response.ContentType = "dat/DAT";
            Response.WriteFile(Server.UrlPathEncode(psth));
            Response.End();

            //int index = gvBackupDatabase.PageIndex;
            //string filepath = gvBackupDatabase.Rows[index].Cells[3].Text;
            //string psth = "~/Company/upLoadRes/";

            //filepath = Server.MapPath(psth) + filepath;
            //FileInfo file = new FileInfo(filepath);

            //if (file.Exists)
            //{
            //    Response.Clear();
            //    // add the header that specifies the default filename for the Download/SaveAs dialog
            //    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
            //    // add the header that specifies the file size, so that the browser
            //    // can show the download progress
            //    Response.AddHeader("Content-Length", file.Length.ToString());
            //    // specify that the response is a stream that cannot be read by the
            //    // client and must be downloaded
            //    // Response.ContentType = "application/octet-stream";
            //    Response.ContentType = "dat/DAT";
            //    // send the file stream to the client
            //    Response.WriteFile(file.FullName);
            //    // stop the execution of this page
            //    Response.End();
            //}
            //else
            //{
            //    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("004209", "您所下载的文件不存在，可能被手动删除或者被移动到别的地方！")));
            //}
        }
    }

    /// <summary>
    /// 开始备份数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCheckPath_Click(object sender, EventArgs e)
    {
        BackupInRoot();      
    }

    /// <summary>
    /// GridView排序事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvBackupDatabase_Sorting(object sender, GridViewSortEventArgs e)
    {
        ///按升序排还是按降序排
        bool flag = true;
        DataView dv = new DataView(DataBackupBLL.GetBackupDatabaseInfo());
        string sortString = e.SortExpression;
        switch (sortString.ToLower().Trim())
        { 
            case "databackupid":
                if (blDataBackupID)
                {
                    dv.Sort = "DataBackupID desc";
                    flag=blDataBackupID= false;
                }

                else
                {
                    dv.Sort = "DataBackupID asc";
                    flag=blDataBackupID= true;
                }
                break;

            case "databackuptime":
                if (blDataBackupTime)
                {
                    dv.Sort = "DataBackupTime desc";
                    flag=blDataBackupTime= false;
                }

                else
                {
                    dv.Sort = "DataBackupTime asc";
                    flag=blDataBackupTime= true;
                }
                break;

            case "pathfilename":
                if (blPathFileName)
                {
                    dv.Sort = "PathFileName desc";
                    flag = blPathFileName = false;
                }

                else
                {
                    dv.Sort = "PathFileName asc";
                    flag=blPathFileName= true;
                }
                break;

            case "operatornum":
                if (blOperatorNum)
                {
                    dv.Sort = "OperatorNum desc";
                    flag=blOperatorNum= false;
                }

                else
                {
                    dv.Sort = "OperatorNum asc";
                    flag = blOperatorNum = true;
                }
                break;
        }
        this.gvBackupDatabase.DataSource = dv;
        this.gvBackupDatabase.DataBind(); 
        ///按关键字排序分页
        Pager page = Page.FindControl("uclPager") as Pager;
        page.PagingSort(0, 10, table, columns, condition, sortString.ToLower().Trim(), flag, "gvBackupDatabase");        
    }

    /// <summary>
    /// 设置GridView样式
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvBackupDatabase_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ///控制GridView样式
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
        }
    }

    /// <summary>
    /// 转到数据备份
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnToDataBackup_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Company/DataBackUp.aspx");
    }
}
