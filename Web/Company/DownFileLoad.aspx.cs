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
using BLL.CommonClass;
using System.IO;
using System.Drawing;

public partial class Company_DownFileLoad : BLL.TranslationBase 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.Button1, new string[][] { new string[] { "000434", "确 定" } });
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        this.Button1.Enabled = false;
           #region 上传图片
        string dirName = "";
        string oldFilePath = this.PhotoPath.PostedFile.FileName.Trim();
        string oldFileName = "";
        string newFileName = "";

        int photoW = 0, photoH = 0;
        try
        {
            if (oldFilePath != string.Empty)
            {
                if (!Directory.Exists(Server.MapPath("../upLevelicon"))) //如果文件夹不存在则创建
                {
                    Directory.CreateDirectory(Server.MapPath("../upLevelicon"));
                }

                
                //检查目录是否存在
                dirName = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString();

                oldFileName = System.IO.Path.GetFileName(oldFilePath);

                string fileExtName = System.IO.Path.GetExtension(oldFilePath);
                if (fileExtName.ToLower() != ".icon" && fileExtName.ToLower() != ".jpg" && fileExtName.ToLower() != ".gif" && fileExtName.ToLower() != ".ico")
                {
                    Response.Write("<script>alert('" + GetTran("000823", "上传文件格式不正确！") + "');</script>");
                    this.Button1.Enabled = true;
                    return;
                }
                if (this.PhotoPath.PostedFile.ContentLength > 5120)
                {
                    Response.Write("<script>alert('" + GetTran("001648", "上传文件不能大于5K！") + "');</script>");
                    this.Button1.Enabled = true;
                    return;
                }

                System.Random rd = new Random(0);
                newFileName = DateTime.Now.Year.ToString() + rd.Next(10).ToString()
                    + DateTime.Now.Month.ToString() + rd.Next(10).ToString()
                    + DateTime.Now.Day.ToString() + rd.Next(10).ToString()
                    + DateTime.Now.Second.ToString()
                    + fileExtName;
                string newFilePath = Server.MapPath("..\\upLevelicon\\") + newFileName;

                string LevelIcon = CommonDataBLL.GetLevelIconByID(int.Parse(Request.QueryString["levelid"]));
                if (System.IO.File.Exists(Server.MapPath(LevelIcon)))
                {
                    System.IO.File.Delete(Server.MapPath(LevelIcon));
                }

                this.PhotoPath.PostedFile.SaveAs(newFilePath);
                try
                {
                    System.Drawing.Image myIma = System.Drawing.Image.FromFile(newFilePath);
                    photoH = myIma.Height;
                    photoW = myIma.Width;

                }
                catch
                { }
                string filepath = @"..\upLevelicon\"+newFileName;
                try
                {
                    System.Drawing.Image img = System.Drawing.Image.FromStream(PhotoPath.PostedFile.InputStream);
                    int width = img.Width;
                    int hight = img.Height;
                    if (width > 50 || hight > 50)
                    {
                        Response.Write("<script>alert('" + GetTran("006034", "图片宽度和高度太大！") + "');</script>");
                        this.Button1.Enabled = true;
                        return;
                    }

                }
                catch (Exception ex1)
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("006895", "图片格式转换错误！") + "');", true);
                    if (System.IO.File.Exists(Server.MapPath(filepath)))
                    {
                        System.IO.File.Delete(Server.MapPath(filepath));
                    }
                    this.Button1.Enabled = true;
                    return;
                }
        CommonDataBLL.upLevelIcon(int.Parse(Request.QueryString["levelid"]), filepath);

        Response.Write("<script>alert('" + GetTran("001649", "上传成功！") + "');returnValue=true;this.close();</script>");
            }
        }
        catch 
        {
           
            return;
        }
this.Button1.Enabled = true;
        #endregion
        
    }
}
