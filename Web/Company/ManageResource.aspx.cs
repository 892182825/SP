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
using System.IO; 
using BLL.other.Company;
using Model;
using System.Text;
using Model.Other;
using DAL;
using Standard.Classes; 
using BLL.CommonClass;
public partial class Company_ManageResource : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.ManageResource);

        givshowFile.Attributes.Add("style", "word-break:keep-all;word-wrap:normal");
        if (!IsPostBack)
        {
            PageBind();
        }
    }

    protected void Translations_More()
    {
        TranControls(givshowFile, new string[][] 
                        {
                            new string[] { "000245","下载"}, 
                            new string[] { "000259","修改"},
                            new string[] { "000022","删除"},
                            new string[] { "000272","资料编号"}, 
                            new string[] { "000204","资料名称"}, 
                            new string[] { "000278","对应文件名"}, 
                            new string[] { "000280","资料简介"},   new string[] { "000282","文件大小"},
                           new string[] { "000283","上次修改日期"}, 
                          
                            new string[] { "000287","下载次数"}
                        }
                    );

        TranControls(Button1, new string[][] 
                        {
                            new string[] { "000048","搜 索"}                             
                        }
            );
        TranControls(btnDownExcel, new string[][] 
                        {
                            new string[] { "001378","导出Excel"}                             
                        }
            );


    }

    //绑定页面
    private void PageBind()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" 1=1 ");
        if (txtName.Text.Trim().Replace("'", "").Length > 0)
        {
            sb.Append(" and ResName like '%" + DisposeString.DisString(txtName.Text.Trim().Replace("'", "")) + "%'");
        }
        if (txtfilename.Text.Trim().Replace("'", "").Length > 0)
        {
            sb.Append(" and FileName like '%" + DisposeString.DisString(txtfilename.Text.Trim().Replace("'", "")) + "%'");
        }
        ViewState["pagewhere"] = sb.ToString() + " order by resid desc";
        Pager pager = Page.FindControl("Pager1") as Pager;
        pager.PageBind(0, 10, "Resources", "ResID, ResName, FileName, ResDescription, ResSize, ResDateTime, ResTimes", sb.ToString(), "ResID", "givshowFile");
        Translations_More();
    }

    public string GetBiaoZhunTime(string dt)
    {
        return Convert.ToDateTime(dt).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToShortDateString();
    }

    protected void givshowFile_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument.ToString());
        ResourcesBLL resource = new ResourcesBLL();
        ResourceModel resuouces = resource.GetResourcesById(id);
        if (resuouces == null)
        {
            ScriptHelper.SetAlert(givshowFile, GetTran("001332", "该资料可能已经不存在") + "！！！");
            return;
        }

        if (e.CommandName.ToLower().Trim() == "del")
        {
            try
            {
                //从文件夹中删除指定的文件
                string path = Server.MapPath(@"..\company\upLoadRes\") + resuouces.FileName;

                System.IO.FileInfo file = new System.IO.FileInfo(path);
                if (file.Exists)
                {
                    file.Delete();
                }

                //删除数据库中文件的信息

                resource.DelResurces(Convert.ToInt32(id));
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('" + GetTran("000008", "删除成功") + "！');</script>");
                PageBind();
            }
            catch
            {

            }
        }
        else if (e.CommandName.ToLower().Trim() == "download")
        {
            try
            {
                string dlDir = "upLoadRes/";
                string path = Server.MapPath(dlDir + resuouces.FileName);
                System.IO.FileInfo file = new System.IO.FileInfo(path);
                if (file.Exists)
                {
                    //修改最后下载时间
                    //  ResourcesBLL resource = new ResourcesBLL();
                    resource.UpdateResutcesResTime(Convert.ToInt32(id));

                    PageBind();

                    Response.Clear();
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(file.Name).Replace('+', ' '));
                    Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.AddHeader("Content-Transfer-Encoding", "binary");
                    Response.ContentType = "application/octet-stream";
                    Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
                    Response.WriteFile(file.FullName);
                    Response.Flush();
                    Response.End();
                    string wheret = ViewState["pagewhere"].ToString();
                    Pager pager = Page.FindControl("Pager1") as Pager;
                    pager.PageBind(0, 10, "Resources", "ResID, ResName, FileName, ResDescription, ResSize, ResDateTime, ResTimes", wheret, "ResID", "givshowFile");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('" + GetTran("000666", "您下载的文件不在文件夹中") + "！');</script>");
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                PageBind();
            }
        }
    }

    protected void btnDownExcel_Click(object sender, EventArgs e)
    {
        Response.AppendHeader("Content-Disposition", "attachment;filename=registerStore.xls");
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");

        ResourcesBLL resource = new ResourcesBLL();
        DataTable dt = resource.GetTableForExcel(ViewState["pagewhere"].ToString());

        StringBuilder sb = Excel.GetExcelTable(dt, GetTran("000653", "资料下载表"), new string[] { "ResName=" + GetTran("000204", "资料名称"), "FileName=" + GetTran("000278", "对应文件名"), "ResDescription=" + GetTran("000280", "资料简介"), "ResSize=" + GetTran("000282", "文件大小"), "ResTimes=" + GetTran("000287", "下载次数") });
        Response.Write(sb.ToString());
        Response.Flush();
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void givshowFile_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");

            int xz = 0;
            xz = (int)Permissions.GetPermissions(Model.Other.EnumCompanyPermission.XZManageResource);
            if (xz == 0)
            {
                ((LinkButton)e.Row.FindControl("LinkButton3")).Visible = false;
            }
            else
            {
                ((LinkButton)e.Row.FindControl("LinkButton3")).Visible = true;
            }
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Translations_More();
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        PageBind();
    }
}