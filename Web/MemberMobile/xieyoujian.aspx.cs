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
using System.Data.SqlClient;
/// <summary>
/// Add Namespace
/// </summary>
using Model;
using BLL.other.Store;
using BLL.other.Company;
using DAL;
using System.IO;

public partial class MemberMobile_xieyoujian : BLL.TranslationBase
{

    protected string msg;
    protected int MessagesendId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        if (!this.IsPostBack)
        {
            this.inite_Bianhao();
            this.BindRadioMsgClass();

        }

        TranControls(btnSave, new string[][]
                        {
                            new string[] { "000497","发 布"}
                        }
         );
        trans();

        //TranControls(btnCancle, new string[][]
        //                {
        //                    new string[] { "000839","取 消"}
        //                }
        //   );
    }
    protected void trans()
    {
        if (RadioListClass.Items.Count > 0)
        {
            TranControls(RadioListClass, new string[][] { new string[] { "008317", "普通邮件" } });
        }
    }
    private void inite_Bianhao()
    {
        this.txtNumber.Items.Add(new ListItem(GetTran("000842", "信息管理员"), BLL.CommonClass.CommonDataBLL.getManageID(1)));
    }
    private void BindRadioMsgClass()
    {
        System.Data.SqlClient.SqlDataReader dr = DAL.DBHelper.ExecuteReader("Select ID,ClassName From MsgClass");
        while (dr.Read())
        {
            ListItem list = new ListItem(dr["ClassName"].ToString(), dr["id"].ToString());
            this.RadioListClass.Items.Add(list);
        }
        dr.Close();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        MessageSendBLL msb = new MessageSendBLL();
        if (this.txtNumber.SelectedItem.Text.Trim() == "")
        {
            msg = "<Script language='javascript'>alert('" + GetTran("000848", "对不起，编号不能为空") + "！');</Script>";
            ClientScript.RegisterStartupScript(this.GetType(), "", msg);
            return;
        }
        string sqlStr = "";
        switch (drop_LoginRole.SelectedValue)
        {
            case "0": sqlStr = "Select count(0) From Manage where Number='" + this.txtNumber.SelectedValue.Trim() + "'"; break;
            case "1": sqlStr = "Select count(0) From MemberInfo where Number='" + this.txtNumber.SelectedValue.Trim() + "'"; break;
            case "2": sqlStr = "Select count(0) From StoreInfo where storeid='" + this.txtNumber.SelectedValue.Trim() + "'"; break;
        }
        if (msb.check(sqlStr) != 1)
        {
            msg = "<Script language='javascript'>alert('" + GetTran("000850", "对不起") + "，" + drop_LoginRole.SelectedItem.Text + "" + GetTran("000854", "的编号错误") + "！');</Script>";
            ClientScript.RegisterStartupScript(this.GetType(), "", msg);
            return;
        }
        //if (this.RadioListClass.SelectedValue.Equals(""))
        //{
        //    msg = "<script language='javascript'>alert('" + GetTran("007712", "请选择邮件分类") + "！！！');</script>";
        //    ClientScript.RegisterStartupScript(this.GetType(), "", msg);

        //    return;
        //}
        if (this.txtTitle.Text.Trim() == "")
        {
            msg = "<Script language='javascript'>alert('" + GetTran("000859", "对不起，标题信息不能为空") + "！');</Script>";
            ClientScript.RegisterStartupScript(this.GetType(), "", msg);
            return;
        }
        if (this.content1.Text.Trim().Length.Equals(0))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("007399", "请输入邮件内容") + "！！！');</script>");

            return;
        }
        if (this.content1.Text.Trim().Length > 400)
        {
            msg = "<script language='javascript'>alert('" + GetTran("000863", "您输入的信息过长") + "！！！');</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "", msg);

            return;
        }
       
        //if (uppic.FileName!=null)
        //{

        //    string fullFileName = uppic.PostedFile.FileName;
        //    //string fileName = fullFileName.Substring(fullFileName.LastIndexOf("\\") + 1);//图片名称
        //    string type = fullFileName.Substring(fullFileName.LastIndexOf(".") + 1).ToLower();
        //    if (type == "jpg" || type == "gif" || type == "bmp" || type == "png" || type == "jpeg")
        //    {
        //        HttpPostedFile upFile = uppic.PostedFile;//HttpPostedFile对象,用来读取上传图片的属性
        //        int fileLength = upFile.ContentLength;//记录文件的长度
        //        byte[] fileBytePicture = new byte[fileLength];//用图片的长度来初始化一个字节数组存储临时的图片文件
        //        Stream fileStream = upFile.InputStream;//建立文件流对象
        //        fileStream.Read(fileBytePicture, 0, fileLength);

        //        //Insert into db, picture字段，image类型
        //        //在此要注意，SQL语句中插入DB不能像varchar()类型，用单引号引起来，这样的话会报错，要使用参数自行插入，eg：
        //        //cmd.CommandText = @"INSERT INTO Category(Category,Picture)VALUES(@Category,@Picture)";
        //        //cmd.Parameters.AddWithValue("@Category", category);//string category     //cmd.Parameters.AddWithValue("@Picture", picture);// byte[] picture
        //    }



                //string filepath = uppic.PostedFile.FileName;  //得到的是文件的完整路径,包括文件名，如：C:\Documents and Settings\Administrator\My Documents\My Pictures\20022775_m.jpg 
                ////string filepath = FileUpload1.FileName;               //得到上传的文件名20022775_m.jpg 
                //string filename = filepath.Substring(filepath.LastIndexOf("\\") + 1);//20022775_m.jpg 
                //serverpath = Server.MapPath("~/MemberMobile/images/") + filename;//取得文件在服务器上保存的位置C:\Inetpub\wwwroot\WebSite1\images\20022775_m.jpg 
                //uppic.PostedFile.SaveAs(serverpath);//将上传的文件另存为 
        //}
        DateTime date = DateTime.Now;
        MessageSendModel msm = new MessageSendModel();
        ///表示会员
        msm.LoginRole = "0";
        msm.Receive = BLL.CommonClass.CommonDataBLL.getManageID(1);
        msm.InfoTitle = this.txtTitle.Text.ToString().Replace("<", "&lt;").Replace(">", "&gt;");
        msm.Content = this.content1.Text.Trim() ;
        msm.Sender = Session["Member"].ToString();
        msm.Senddate = date;
        msm.SenderRole = "2";
        msm.MessageClassID = 0;
        msm.MessageType = 'm';
        
        int i = 0;
        i = msb.MemberSendToManage(msm);
        if (i > 0)
        {
            msg = "<Script language='javascript'>alert('" + GetTran("007400", "邮件发送成功") + "！');</script>";
        }
        else
        {
            msg = "<Script language='javascript'>alert('" + GetTran("007401", "邮件发送失败") + "！');</script>";
        }
        ClientScript.RegisterStartupScript(this.GetType(), "", msg);

        btnCancle_Click(null, null);
    }

    protected void btnCancle_Click(object sender, EventArgs e)
    {
        this.txtTitle.Text = "";
        this.content1.Text = "";
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        btnSave_Click(null, null);
    }
    //protected void btnUp_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (uppic.PostedFile.FileName=="")
    //        {
    //            liter.Text = "上传失败";
    //            msg = "<Script language='javascript'>alert('" + GetTran("00000", "请添加图片") + "！');</script>";
    //            return; 
    //        }
    //        else
    //        {
    //            string filepath = uppic.PostedFile.FileName;  //得到的是文件的完整路径,包括文件名，如：C:\Documents and Settings\Administrator\My Documents\My Pictures\20022775_m.jpg 
    //            //string filepath = FileUpload1.FileName;               //得到上传的文件名20022775_m.jpg 
    //            string filename = filepath.Substring(filepath.LastIndexOf("\\") + 1);//20022775_m.jpg 
    //            string serverpath = Server.MapPath("~/MemberMobile/images/") + filename;//取得文件在服务器上保存的位置C:\Inetpub\wwwroot\WebSite1\images\20022775_m.jpg 
    //            uppic.PostedFile.SaveAs(serverpath);//将上传的文件另存为 
    //            this.liter.Text = "上传成功！"; 
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        msg = "<Script language='javascript'>alert('" + GetTran("00000", "上传失败") + "！');</script>";
    //        throw;
    //    }
    //}
    protected void btnUp_Click(object sender, EventArgs e)
    {

    }
}
