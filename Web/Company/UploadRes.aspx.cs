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
using BLL.other.Company;
using System.IO;
using System.IO.IsolatedStorage;
using BLL;
using System.Diagnostics;
using Microsoft.Win32;
public partial class Company_UploadRes : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        //AjaxPro.Utility.RegisterTypeForAjax(typeof(Company_UploadRes));
        //ajaxPro注册
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        TranControls(btnUpLoad, new string[][] 
                        {
                            new string[] { "001396","开始上传"}                             
                        }
          );
        TranControls(CustomValidator1, new string[][] 
                        {
                            new string[] { "001402","上传文件格式不对，或者文件的名字过长"}                             
                        }
          );
        TranControls(btncheckName, new string[][] 
                        {
                            new string[] { "001516","文件是否存在"}                             
                        }
          );

        if (!IsPostBack)
        {
            Response.Cache.SetExpires(DateTime.Now);
            Permissions.CheckManagePermission(EnumCompanyPermission.ManageResource_S);
            if (Request["action"] != null)
            {
                if (Request["id"] != null)
                {
                    showOldResInfo(Convert.ToInt32(Request["id"]));
                    showfile.Visible = false;
                    this.btnUpLoad.Text = GetTran("001403", "确定修改");
                    Literal1.Text = GetTran("004057", "资料修改");
                    btnUpLoad.Attributes.Add("onclick", "return Check()");
                    this.ShowCountry.Visible = false;
                }
            }
            else
            {
                Literal1.Text = GetTran("005142", "资料上传");
            }

            bindcountry();
            BindLev();
        }


    }
    private void showOldResInfo(int resid)
    {
        try
        {
            ResourcesBLL resource = new ResourcesBLL();
            ResourceModel res = resource.GetResourcesById(resid);
            ViewState["ResID"] = resid;
            this.txtResName.Text = res.ResName;
            this.txtjianjie.Text = res.ResDescription;
        }
        catch (Exception)
        {
        }
    }

    protected void btnUpLoad_Click(object sender, EventArgs e)
    {
        ResourcesBLL resources = new ResourcesBLL();

        string mfileName = "";
        if (this.txtjianjie.Text.Trim().Length > 50)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "alert('" + GetTran("001502", "输入的简介超过50个字符") + "')");
            return;
        }

        if (this.txtResName.Text.Trim() == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("006938", "资料名称和资料简介不能为空！") + "')</script>");
            return;
        }

        if (!System.IO.Directory.Exists(Server.MapPath("~/Company/upLoadRes/")))
        {
            System.IO.Directory.CreateDirectory(Server.MapPath("~/Company/upLoadRes/") + "\\");
        }

        if (Request.QueryString["action"] != "edit")
        {
            string filepath = upFile.PostedFile.FileName;//获取要上传的文件的说有字符，包括文件的路径，文件的名称文件的扩展名称

            //验证上传文件后缀名
            string s = filepath.Substring(filepath.LastIndexOf(".") + 1);
            s = s.ToLower();
            if (s != "rar" && s != "zip")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('您上传的文件格式不正确，请重新上传！')</script>");
                return;
            }


            try
            {
                string mPath = Server.MapPath("~/Company/upLoadRes/");
                mfileName = filepath.Substring(filepath.LastIndexOf("\\") + 1);

                if (mfileName.Length > 30)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('资料文件名不得大于30个字符！')</script>");
                }
                else
                {
                    int sizeLength = upFile.PostedFile.ContentLength;
                    if (sizeLength > 4 * 1024 * 1024)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "", "alert('上传文件不能超过4MB')", true);
                        return;
                    }
                    string ressize;//文件大小
                    if (sizeLength <= 1203)
                    {
                        ressize = upFile.PostedFile.ContentLength.ToString() + "B";
                    }
                    else
                    {
                        ressize = Convert.ToString(upFile.PostedFile.ContentLength / 1204) + "K";
                    }

                    string resname = this.txtResName.Text.Trim();
                    string filename = filepath.Substring(filepath.LastIndexOf("\\") + 1);
                    string resdes = this.txtjianjie.Text.Trim();


                    string realName = DateTime.Now.Millisecond.ToString() + mfileName;
                    upFile.PostedFile.SaveAs(mPath + "\\" + realName);

                    if (CheckFileForm(mPath + "\\" + realName) <= 0)
                    {
                        File.Delete(mPath + "\\" + realName);
                        ClientScript.RegisterStartupScript(this.GetType(), "", "alert('" + GetTran("000823", "上传文件格式不正确！") + "');", true);
                        return;
                    }//图片文件上传后进行压缩再删除原有图片
                    else if (CheckFileForm(mPath + "\\" + realName) == 2)
                    {
                        ZipClass zc = new ZipClass();
                        string err = "";
                        string zipPath1 = mPath + realName;
                        string zipPath = zipPath1.Replace(zipPath1.Substring(zipPath1.LastIndexOf(".") + 1), "rar");
                        realName = realName.Replace(realName.Substring(realName.LastIndexOf(".") + 1), "rar");
                        bool res = zc.ZipFile(zipPath1, zipPath, out err);
                        if (res)
                        {
                            File.Delete(zipPath1);
                        }
                        else
                        {
                            labUpInfo.Text = err;
                            return;
                        }
                    }

                    RecordResInfo(resname, realName, resdes, ressize, DateTime.UtcNow.ToLongDateString());
                    this.txtjianjie.Text = "";
                    this.txtResName.Text = "";
                }
            }
            catch (Exception)
            {
                //throw;
                labUpInfo.Text = "上传失败！该资料的操作权限不够，请设置权限再进行上传！";
            }
        }
        else
        {
            string resname = this.txtResName.Text.Trim();
            string resdes = this.txtjianjie.Text.Trim();

            UpdateResInfo(resname, "", resdes, "");
            this.labUpInfo.Text = GetTran("001505", "资料修改成功") + "！";
        }

    }
    private void UpdateResInfo(string resname, string filename, string resdes, string ressize)
    {
        ResourceModel res = new ResourceModel();
        res.ResID = Convert.ToInt32(ViewState["ResID"].ToString());
        res.ResDateTime = DateTime.Now.ToUniversalTime();
        res.ResDescription = resdes;
        res.ResName = resname;
        res.ResSize = ressize;
        res.FileName = filename;
        res.DownTarget = Convert.ToInt32(this.DropDownList2.SelectedValue);
        res.DownMenberLev = Convert.ToInt32(this.DropDownList3.SelectedValue);
        ResourcesBLL resource = new ResourcesBLL();
        resource.UpdateResource(res);
    }
    /// <summary>
    /// 上传文件方法
    /// </summary>
    /// <param name="resname"></param>
    /// <param name="filename"></param>
    /// <param name="resdes"></param>
    /// <param name="ressize"></param>
    /// <param name="datetime"></param>
    private void RecordResInfo(string resname, string filename, string resdes, string ressize, string datetime)
    {
        ResourceModel res = new ResourceModel();
        res.ResDateTime = DateTime.Now.ToUniversalTime();
        res.ResDescription = resdes;
        res.ResName = resname;
        res.ResSize = ressize;
        res.FileName = filename;
        res.CPPCode = Convert.ToInt32(this.DropDownList1.SelectedValue);
        res.DownMenberLev = Convert.ToInt32(this.DropDownList3.SelectedValue);
        res.DownTarget = Convert.ToInt32(this.DropDownList2.SelectedValue);
        ResourcesBLL resource = new ResourcesBLL();
        if (resource.InsertResource(res))
        {
            labUpInfo.Text = GetTran("001506", "上传成功") + "！！！";
        }
        else
        {
            labUpInfo.Text = GetTran("001508", "上传失败") + "！！！";
        }
    }
    /// <summary>
    /// 检查文件名字是否存在
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btncheckName_Click(object sender, EventArgs e)
    {
        CheckFileNames();

        if (this.DropDownList2.SelectedValue != "2")
        {

            this.Lev.Attributes.Remove("style");
            this.Lev.Attributes.Add("style", "display:none");

        }
        else
        {
            this.Lev.Attributes.Remove("style");
            this.Lev.Attributes.Add("style", "display=''");
        }
    }

    /// <summary>
    /// 判断文件的真是格式 通过二进制读取文件头
    /// </summary>
    /// <returns></returns>
    private int CheckFileForm(string sfileurl)
    {
        int ret = 0;
        //各种文件格式 的二进制文件头
        //4946/104116 txt
        // *7173        gif 
        // *255216      jpg
        // *13780       png
        // *6677        bmp
        // *239187      txt,aspx,asp,sql
        // *208207      xls.doc.ppt
        // *6063        xml
        // *6033        htm,html
        // *4742        js
        // *8075        xlsx,zip,pptx,mmap,zip
        // *8297        rar   
        // *01          accdb,mdb
        // *7790        exe,dll           
        // *5666        psd 
        // *255254      rdp 
        // *10056       bt种子 
        // *64101       bat 
        // *4059        sgf
        string files = string.Empty;
        byte buffer;
        FileStream fs = new FileStream(sfileurl, FileMode.Open, FileAccess.Read);
        BinaryReader br = new BinaryReader(fs);
        buffer = br.ReadByte();
        files += buffer.ToString();
        buffer = br.ReadByte();
        files += buffer.ToString();
        br.Close();
        fs.Close();
        //判断真是文件的格式
        if (files == "8297" || files == "8075")
        {
            ret = 1;//rar或zip文件
        }
        else if (files == "255216" || files == "7173" || files == "6677" || files == "13780")
        {
            ret = 2;//图片文件
        }
        else
        {
            ret = 0;
        }
        return ret;

    }

    /// <summary>
    /// 检查文件名字
    /// </summary>
    /// <returns></returns>
    /// 
    private bool CheckFileNames()
    {
        if (this.txtResName.Text.Trim() == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "alert('" + GetTran("001510", "资料名不能为空") + "！')", true);
            return false;
        }
        ResourcesBLL resoucres = new ResourcesBLL();
        if (resoucres.CheckReourceResname(DisposeString.DisString(this.txtResName.Text.Trim().Trim())))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "alert('" + GetTran("001512", "存在同名资料") + "！')", true);
            return false;
        }
        return true;
    }

    private void bindcountry()
    {
        DataTable dt = StoreInfoEditBLL.bindCountry();
        this.DropDownList1.DataSource = dt;
        this.DropDownList1.DataTextField = "name";
        this.DropDownList1.DataValueField = "countrycode";
        this.DropDownList1.DataBind();
    }

    public void BindLev()
    {
        DataTable dt = DAL.CommonDataDAL.GetLeverList();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            this.DropDownList3.Items.Add(new ListItem(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));

        }

    }
    //压缩文件
    public void RARsave(string patch, string rarPatch, string rarName)
    {
        String the_rar;
        RegistryKey the_Reg;
        Object the_Obj;
        String the_Info;
        ProcessStartInfo the_StartInfo;
        Process the_Process;
        try
        {
            the_Reg = Registry.ClassesRoot.OpenSubKey(@"ApplicationsWinRAR.exeShellOpenCommand");
            the_Obj = the_Reg.GetValue("");
            the_rar = the_Obj.ToString();
            the_Reg.Close();
            the_rar = the_rar.Substring(1, the_rar.Length - 7);
            Directory.CreateDirectory(patch);
            //命令参数
            //the_Info = " a    " + rarName + "  " + @"C:Test70821.txt"; //文件压缩
            the_Info = " a    " + rarName + "  " + patch + "  -r"; ;
            the_StartInfo = new ProcessStartInfo();
            the_StartInfo.FileName = the_rar;
            the_StartInfo.Arguments = the_Info;
            the_StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //打包文件存放目录
            the_StartInfo.WorkingDirectory = rarPatch;
            the_Process = new Process();
            the_Process.StartInfo = the_StartInfo;
            the_Process.Start();
            the_Process.WaitForExit();
            the_Process.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //解压文件
    public string unRAR(string unRarPatch, string rarPatch, string rarName)
    {
        String the_rar;
        RegistryKey the_Reg;
        Object the_Obj;
        String the_Info;
        ProcessStartInfo the_StartInfo;
        Process the_Process;
        try
        {
            the_Reg = Registry.ClassesRoot.OpenSubKey(@"ApplicationsWinRAR.exeShellOpenCommand");
            the_Obj = the_Reg.GetValue("");
            the_rar = the_Obj.ToString();
            the_Reg.Close();
            the_rar = the_rar.Substring(1, the_rar.Length - 7);
            Directory.CreateDirectory(Server.MapPath(unRarPatch));
            the_Info = "e   " + rarName + "  " + Server.MapPath(unRarPatch) + " -y";
            the_StartInfo = new ProcessStartInfo();
            the_StartInfo.FileName = the_rar;
            the_StartInfo.Arguments = the_Info;
            the_StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            the_StartInfo.WorkingDirectory = Server.MapPath(rarPatch);//获取压缩包路径
            the_Process = new Process();
            the_Process.StartInfo = the_StartInfo;
            the_Process.Start();
            the_Process.WaitForExit();
            the_Process.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return Server.MapPath(unRarPatch);
    }
}