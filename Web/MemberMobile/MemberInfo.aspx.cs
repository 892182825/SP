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
using BLL.other.Member;
using BLL.CommonClass;
using DAL;
using Model;
using System.Text;
using BLL.other.Company;
using System.Drawing;
using System.Collections.Generic;
using BLL.Registration_declarations;
using System.IO;
using BLL.other.Store;
using System.Data.SqlClient;

public partial class Member_MemberInfo : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        translation();

        if (!IsPostBack)
        {
            try
            {
                string res = Request.QueryString["res"] == "" ? "" : Request.QueryString["res"];
                if (Request.UrlReferrer.ToString().IndexOf("/PassWordManage/CheckAdv.aspx?type=member&url=MemberInfo") < 0 || res != "success")
                {
                    Response.Redirect("../PassWordManage/CheckAdv.aspx?type=member&url=MemberInfo");
                    return;
                }
                else
                {
                    table2.Visible = true;
                }
            }
            catch
            {
                Response.Redirect("../PassWordManage/CheckAdv.aspx?type=member&url=MemberInfo");
            }

            if (Session["Member"].ToString() != string.Empty)
            {
                BindCountry();
                ddlCountry.SelectedIndex = 1;
                GetOldMemberData(Session["Member"].ToString());

                int countryId = new AddOrderBLL().GetCountryId(ddlCountry.SelectedItem.Text);
                //获得语言
                Session["language"] = Session["language"] == null ? ("chinese") : (Session["language"].ToString());
                this.DdlBank.DataSource = new AddOrderBLL().GetBank(countryId, Session["language"].ToString());
                this.DdlBank.DataTextField = "BankName";
                this.DdlBank.DataValueField = "BankCode";
                this.DdlBank.DataBind();

                if (ViewState["bankcode"] != null)
                {
                    foreach (ListItem li in DdlBank.Items)
                    {
                        if (li.Value == ViewState["bankcode"].ToString())
                        {
                            li.Selected = true;
                            break;
                        }
                    }
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), null, "<script language='javascript'>alert('" + GetTran("001206", "参数错误！") + GetTran("007703", "重新登陆") + "！');location.href('../index.aspx')</script>", false);
                return;
            }

        }
    }

    private void translation()
    {
        TranControls(go, new string[][] { new string[] { "000434", "确定" } });
    }

    private void GetOldMemberData(string bianhao)
    {
        //MemberInfoModifyBll mf = new MemberInfoModifyBll();
        MemberInfoModel member = MemberInfoModifyBll.getMemberInfo(bianhao);
        if (member != null)
        {
            this.LblBh.Text = member.Number;
            this.LblBh.ForeColor = Color.Silver;
            //解密姓名
            this.labName.Text = Encryption.Encryption.GetDecipherName(CommonDataBLL.quanjiao(member.Name));
            Txtlm.Text = member.PetName;
            Txtyb1.Text = member.PostalCode;

            //解密地址
            this.txtdizhi.Text = Encryption.Encryption.GetDecipherAddress(member.Address);

            Txtjtdh.Text = Encryption.Encryption.GetDecipherTele(member.HomeTele);
            if (this.Txtjtdh.Text.Trim() == "")
            {
                this.Txtjtdh.Text = GetTran("000028", "电话号码");
                this.Txtjtdh.Style.Add("color", "gray");
            }

            Txtbgdh.Text = Encryption.Encryption.GetDecipherTele(member.OfficeTele);
            if (this.Txtbgdh.Text.Trim() == "")
            {
                this.Txtbgdh.Text = GetTran("000028", "电话号码");
                this.Txtbgdh.Style.Add("color", "gray");
            }
            Txtyddh.Text = Encryption.Encryption.GetDecipherTele(member.MobileTele);

            Txtczdh.Text = Encryption.Encryption.GetDecipherTele(member.FaxTele);
            if (this.Txtczdh.Text.Trim() == "")
            {
                this.Txtczdh.Text = GetTran("000028", "电话号码");
                this.Txtczdh.Style.Add("color", "gray");
            }

            IList<string> strBankInfo = new GroupRegisterBLL().GetBankValue(member.BankCode);
            if (strBankInfo.Count > 0)
            {
                foreach (ListItem li in ddlCountry.Items)
                {
                    if (li.Value == strBankInfo[1].Substring(0, 2))
                    {
                        li.Selected = true;
                        break;
                    }
                }
                ViewState["bankcode"] = strBankInfo[1];
            }
            txtbankbrachname.Text = member.Bankbranchname;
            if (member.BCPCCode != "")
            {
                List<string> BankArea = new RegistermemberBLL().ChoseArea(member.BCPCCode);
                //解密银行地址并且读出银行详细地址
                UserControl_CountryCity countrycity1 = Page.FindControl("CountryCity2") as UserControl_CountryCity;
                //取得该会员地址
                CityModel bankArea = DAL.CommonDataDAL.GetCPCCode(member.BCPCCode);
                countrycity1.SelectCountry(bankArea.Country, bankArea.Province, bankArea.City, bankArea.Xian);
            }
            List<string> GetCardType = new GroupRegisterBLL().GetCardType(member.PaperType.PaperTypeCode);
            lblzhengjian.Text = GetCardType[0];
            UserControl_CountryCity countrycity = Page.FindControl("CountryCity1") as UserControl_CountryCity;
            //取得该会员地址
            CityModel MemberArea = DAL.CommonDataDAL.GetCPCCode(member.CPCCode);
            countrycity.SelectCountry(MemberArea.Country, MemberArea.Province, MemberArea.City, MemberArea.Xian);
            //生日需要修改
            lblBirthday.Text = (Convert.ToDateTime(member.Birthday).Year + "-" + (Convert.ToDateTime(member.Birthday)).Month + "-" + (Convert.ToDateTime(member.Birthday)).Day);

            //解密开户名
            this.labkaihuming.Text = Encryption.Encryption.GetDecipherName(member.BankBook) == "" ? Encryption.Encryption.GetDecipherName(member.Name) : Encryption.Encryption.GetDecipherName(member.BankBook);
            //卡号解密
            this.labkahao.Text = Encryption.Encryption.GetDecipherCard(member.BankCard);
            if (Convert.ToInt32(member.Sex) == 0)
            {
                this.labSex.Text = GetTran("000095", "女");
            }
            else
            {
                this.labSex.Text = GetTran("000094", "男");
            }

            //解密证件号
            this.labzhengjianhaoma.Text = Encryption.Encryption.GetDecipherNumber(member.PaperNumber);
            Txtbz.Text = member.Remark;
        }
    }
    protected void go_Click(object sender, System.EventArgs e)
    {
        //删除会员图片
        string dirName = "";
        string oldFileName = "";
        string newFileName = "";
        string newFilePath = "";
        int photoW = 0, photoH = 0;

        #region
        //if (this.myFile.PostedFile.FileName.Trim() != string.Empty)
        //{
        //    //删除旧图
        //    if (Session["Member"].ToString() != "")
        //    {
        //        delMemberPic(Session["Member"].ToString());
        //    }
        //    else
        //    {
        //        //Response.Write("<script>alert('参数错误，请重新登陆！');location.href('index.aspx')</script>");
        //        msg = "<script language='javascript'>alert('参数错误！重新登陆！');location.href('../index.aspx')</script>";
        //        return;
        //    }
        //    #region 上传新图

        //    //string oldFilePath = this.myFile.PostedFile.FileName.Trim();

        //    //try
        //    //{
        //    //    if (oldFilePath != string.Empty)
        //    //    {
        //    //        //检查目录是否存在
        //    //        dirName = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString();
        //    //        if (!System.IO.Directory.Exists(Server.MapPath("../upload/" + dirName)))
        //    //        {
        //    //            System.IO.Directory.CreateDirectory(Server.MapPath("../upload/" + dirName));
        //    //        }

        //    //        oldFileName = System.IO.Path.GetFileName(oldFilePath);

        //    //        string fileExtName = System.IO.Path.GetExtension(oldFilePath);
        //    //        if (fileExtName.ToLower() != ".gif" && fileExtName.ToLower() != ".jpg" && fileExtName.ToLower() != ".jpeg")
        //    //        {
        //    //           // Response.Write("<script>alert('上传文件格式不正确！')</script>");
        //    //            msg = "<script language='javascript'>alert('上传文件格式不正确！');</script>";
        //    //            return;
        //    //        }
        //    //        if (myFile.PostedFile.ContentLength > 102400)
        //    //        {
        //    //            //Response.Write("<script>alert('上传文件不能大于100K！')</script>");
        //    //            msg = "<script language='javascript'>alert('上传文件不能大于100K！');</script>";
        //    //            return;
        //    //        }

        //    //        System.Random rd = new Random(0);
        //    //        newFileName = DateTime.Now.Year.ToString() + rd.Next(10).ToString()
        //    //            + DateTime.Now.Month.ToString() + rd.Next(10).ToString()
        //    //            + DateTime.Now.Day.ToString() + rd.Next(10).ToString()
        //    //            + DateTime.Now.Second.ToString()
        //    //            + fileExtName;

        //    //        newFilePath = Server.MapPath("..\\upload\\" + dirName) + "\\" + newFileName;
        //    //        this.myFile.PostedFile.SaveAs(newFilePath);

        //    //        try
        //    //        {
        //    //            System.Drawing.Image myIma = System.Drawing.Image.FromFile(newFilePath);
        //    //            photoH = myIma.Height;
        //    //            photoW = myIma.Width;

        //    //        }
        //    //        catch
        //    //        { }

        //    //        ViewState["photopath"] = dirName + "\\" + newFileName;
        //    //        ViewState["photoH"] = photoH.ToString();
        //    //        ViewState["photoW"] = photoW.ToString();

        //    //    }
        //    //}
        //    //catch (Exception ext)
        //    //{
        //    //    Response.Write("<script>alert('" + ext.ToString() + "')</script>");
        //    //    return;
        //    //}
        //    #endregion
        //    #region 上传图片
        //    string oldFilePath = this.myFile.PostedFile.FileName.Trim();
        //    //string oldFilePath = "";

        //    try
        //    {
        //        if (oldFilePath != string.Empty)
        //        {
        //            if (!Directory.Exists(Server.MapPath("../Store/H_image"))) //如果文件夹不存在则创建
        //            {
        //                Directory.CreateDirectory(Server.MapPath("../Store/H_image"));
        //            }

        //            //检查目录是否存在
        //            dirName = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString();

        //            oldFileName = System.IO.Path.GetFileName(oldFilePath);

        //            string fileExtName = System.IO.Path.GetExtension(oldFilePath);
        //            if (fileExtName.ToLower() != ".gif" && fileExtName.ToLower() != ".jpg" && fileExtName.ToLower() != ".jpeg" && fileExtName.ToLower() != ".bmp" && fileExtName.ToLower() != ".png")
        //            {
        //                System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("000823", "上传文件格式不正确！") + "');", true);
        //                return;
        //            }
        //            if (this.myFile.PostedFile.ContentLength > 51200)
        //            {
        //                System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("000824", "上传文件不能大于50K！") + "');", true);
        //                return;
        //            }
        //            //System.Drawing.Image img = System.Drawing.Image.FromStream(PhotoPath1.PostedFile.InputStream);
        //            //int width = img.Width;
        //            //int hight = img.Height;
        //            //if (width > 50 || hight > 50)
        //            //{
        //            //    Response.Write("<script>alert('" + GetTran("006034", "图片宽度和高度太大！") + "');</script>");
        //            //    this.Button1.Enabled = true;
        //            //    return "";
        //            //}
        //            System.Random rd = new Random(0);
        //            newFileName = DateTime.Now.Year.ToString() + rd.Next(10).ToString()
        //                + DateTime.Now.Month.ToString() + rd.Next(10).ToString()
        //                + DateTime.Now.Day.ToString() + rd.Next(10).ToString()
        //                + DateTime.Now.Second.ToString()
        //                + fileExtName;
        //            newFilePath = Server.MapPath("..\\Store\\H_image\\") + newFileName;

        //            string LevelIcon = "../Store/" + new MemberInfoModifyBll().GetMemberPhoto(Session["Member"].ToString());
        //            if (System.IO.File.Exists(Server.MapPath(LevelIcon)))
        //            {
        //                System.IO.File.Delete(Server.MapPath(LevelIcon));
        //            }

        //            this.myFile.PostedFile.SaveAs(newFilePath);
        //            try
        //            {
        //                System.Drawing.Image myIma = System.Drawing.Image.FromFile(newFilePath);
        //                photoH = myIma.Height;
        //                photoW = myIma.Width;

        //            }
        //            catch
        //            { }
        //            filepath = @"\Store\H_image\" + newFileName;
        //            try
        //            {
        //                System.Drawing.Image myIma = System.Drawing.Image.FromFile(newFilePath);
        //                photoH = myIma.Height;
        //                photoW = myIma.Width;

        //            }
        //            catch (Exception ex1)
        //            {
        //                System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("006895", "图片格式转换错误！") + "');", true);
        //                if (System.IO.File.Exists(Server.MapPath(filepath)))
        //                {
        //                    System.IO.File.Delete(Server.MapPath(filepath));
        //                }
        //                return;
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        return;
        //    }
        //    #endregion
        //}
        //else
        //{
        //}
        #endregion
        //生日
        DateTime birthday;
        try
        {
            //更改至标准时间
            birthday = Convert.ToDateTime(lblBirthday.Text);
        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(GetType(), null, "<script language='javascript'>alert('" + GetTran("000148", "对不起，请选择正确的出生日期！") + "');</script>", false);
            return;
        }
        int sex = 0;
        if (labSex.Text.Trim() == GetTran("000094", "男"))
        {
            sex = 1;
        }
        else
        {
            sex = 0;
        }

        if (Txtbz.Text.Length > 500)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), null, "<script language='javascript'>alert('" + GetTran("007704", "备注内容不能超过500字") + "！');</script>", false);
            return;
        }
        //姓名加密
        string name = Encryption.Encryption.GetEncryptionName(labName.Text.Trim());
        string Petname = Txtlm.Text.Trim();
        //string Birthday = birthday;
        //string sex = sex;
        //加密电话

        string HomeTele = Encryption.Encryption.GetEncryptionTele(Txtjtdh.Text.Trim());

        string OfficeTele = Encryption.Encryption.GetEncryptionTele(Txtbgdh.Text.Trim());

        string MobileTele = Encryption.Encryption.GetEncryptionTele(Txtyddh.Text.Trim());

        string FaxTele = Encryption.Encryption.GetEncryptionTele(Txtczdh.Text.Trim());

        UserControl_CountryCity countrycity = Page.FindControl("CountryCity1") as UserControl_CountryCity;
        string Country = countrycity.Country;
        string Province = countrycity.Province;
        string City = countrycity.City;
        string Xian = countrycity.Xian;
        if (Country == "请选择" || Province == "请选择" || City == "请选择" || Xian == "请选择" || Country == "" || Province == "" || City == "" || Xian == "")
        {
            ScriptHelper.SetAlert(Page, "请选择地址！");
            return;
        }
        string CPCCode = DAL.CommonDataDAL.GetCPCCode(Country, Province, City, Xian);
        //加密地址
        string Address = Encryption.Encryption.GetEncryptionAddress(txtdizhi.Text.Trim());
        if (Address.Trim().Length == 0)
        {
            ScriptHelper.SetAlert(Page, "请填写详细地址！");
            return;
        }

        string PostalCode = Txtyb1.Text.Trim();

        string PaperType = new GroupRegisterBLL().GetPaperTypeCode(lblzhengjian.Text.Trim());
        //证件加密
        string PaperNumber = Encryption.Encryption.GetEncryptionNumber(labzhengjianhaoma.Text.Trim());
        if (this.Txtbz.Text.Length > 500)
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("006708", "对不起，备注输入的字符太多,最多500个字符！") + "');", true);
            return;
        };
        string Remark = Txtbz.Text.Trim();
        //int Healthy = Convert.ToInt32(MemberHealth.SelectedValue);
        string photopath = "";
        //需要修改标准时间
        DateTime Birthday = Convert.ToDateTime(birthday);

        #region 银行信息

        UserControl_CountryCity countrycity1 = Page.FindControl("CountryCity2") as UserControl_CountryCity;
        string Country1 = countrycity1.Country;
        string Province1 = countrycity1.Province;
        string City1 = countrycity1.City;
        string Xian1 = countrycity1.Xian;
        if (Country1 == "请选择" || Province1 == "请选择" || City1 == "请选择" || Xian1 == "请选择" || Country1 == "" || Province1 == "" || City1 == "" || Xian1 == "")
        {
            ScriptHelper.SetAlert(Page, "请选择开户行地址！");
            return;
        }
        string bcpccode = DAL.CommonDataDAL.GetCPCCode(Country1, Province1, City1, Xian1);
        string bankcode = DdlBank.SelectedValue;
        string bankbrachname = txtbankbrachname.Text.Trim();
        string bankcard = Encryption.Encryption.GetEncryptionCard(labkahao.Text.Trim());
        string bankaddress = "";

        #endregion

        BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("Memberinfo", "ltrim(rtrim(number))");
        cl_h_info.AddRecord(Session["Member"].ToString());
        MemberInfoModifyBll mf = new MemberInfoModifyBll();
        if (mf.updateMember(Session["Member"].ToString(), name, Petname, Birthday, sex, HomeTele, OfficeTele, MobileTele, FaxTele, Country, Province, City, Address, PostalCode, PaperType, PaperNumber, Remark, photopath, photoW, photoH, CPCCode, bcpccode, bankaddress, bankcard, bankcode, bankbrachname))
        {
            cl_h_info.AddRecord(Session["Member"].ToString());
            cl_h_info.ModifiedIntoLogs(BLL.CommonClass.ChangeCategory.member3, Session["Member"].ToString(), BLL.CommonClass.ENUM_USERTYPE.objecttype5);
            //ScriptHelper.SetAlert(Page,"修改成功！！！");
            Page.ClientScript.RegisterStartupScript(GetType(), null, "<script language='javascript'>alert('" + GetTran("000222", "修改成功！") + "');</script>", false);
        }
        else
        {
            //ScriptHelper.SetAlert(Page, "修改失败！！！");
            Page.ClientScript.RegisterStartupScript(GetType(), null, "<script language='javascript'>alert('" + GetTran("000225", "修改失败！") + "');</script>", false);
        }
        GetOldMemberData(Session["Member"].ToString());
    }
    private void delMemberPic(string bianhao)
    {
        MemberInfoModifyBll mb = new MemberInfoModifyBll();
        object obj = mb.GetMemberPhoto(Session["Member"].ToString());
        if (obj != null)
        {
            if (obj.ToString() != "")
            {
                string path2 = Server.MapPath("..\\H_image") + "\\" + obj.ToString();

                System.IO.FileInfo fi2 = new System.IO.FileInfo(path2);
                if (fi2.Exists)
                {
                    fi2.Delete();
                }
            }
        }
    }

    #region
    /*
    MemInfoEditBLL meb = new MemInfoEditBLL();
    CommonDataBLL cd = new CommonDataBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {

                //保存编号
                ViewState["Number"] = Request.QueryString["id"].ToString();
                //SetExpectNum(cd.getMaxqishu());
                SetExpectNum(1);
                getMemberInfo(Request.QueryString["id"]);

            }
            else
            {
                Response.Write("<script>alert('参数错误!!');window.location.href='QueryMemberInfo.aspx';</script>");
            }
        }
    }
    protected void BtnUpdate_Click1(object sender, EventArgs e)
    {
        string oldChangeInfo = "";
        bool flag = false;
        StringBuilder changeInfo = new StringBuilder();


        MemberInfoModel mem = meb.getMemberInfo(ViewState["Number"].ToString());

        oldChangeInfo = mem.ChangeInfo.ToString();
        changeInfo.Append(mem.ChangeInfo.ToString());

        changeInfo.Append("管理员 ");
        changeInfo.Append(Session["Company"]);
        changeInfo.Append(" 在 ");
        changeInfo.Append(DateTime.Now.ToString());
        changeInfo.Append(" 第 ");
        changeInfo.Append(Session["ExpectNum"].ToString());
        changeInfo.Append(" 期 ");
        changeInfo.Append(" 修改了如下内容：");

        //判断用户是否修改了姓名
        if (this.Name.Text != mem.Name.ToString())
        {
            flag = true;
            changeInfo.Append("\n修改了姓名，原姓名：");
            changeInfo.Append(mem.Name.ToString());
            changeInfo.Append("；新姓名：");
            changeInfo.Append(this.Name.Text.ToString());
        }
        //判断用户是否修改了昵称
        if (this.PetName.Text != mem.PetName.ToString())
        {
            flag = true;
            changeInfo.Append("\n修改了昵称，原昵称：");
            changeInfo.Append(mem.PetName.ToString());
            changeInfo.Append("；新昵称：");
            changeInfo.Append(this.PetName.Text.ToString());
        }

        //判断用户是否修改了日期
        string str = mem.Birthday.ToString();
        int i = str.IndexOf(":");
        // str = str.Remove(i - 2, 8);
        if (this.Birthday.Text != str)
        {
            flag = true;
            changeInfo.Append("\n修改了生日，原生日：");
            changeInfo.Append(str);
            changeInfo.Append("；新生日：");
            changeInfo.Append(this.Birthday.Text.ToString());
        }

        //判断用户是否修改了邮编
        if (this.PostolCode.Text != mem.PostalCode.ToString())
        {
            flag = true;
            changeInfo.Append("\n修改了邮编，原邮编：");
            changeInfo.Append(mem.PostalCode.ToString());
            changeInfo.Append("；新邮编：");
            changeInfo.Append(this.PostolCode.Text.ToString());
        }

        //判断用户是否修改了家庭电话
        if (this.HomeTele.Text != mem.HomeTele.ToString())
        {
            flag = true;
            changeInfo.Append("\n修改了家庭电话，原家庭电话：");
            changeInfo.Append(mem.HomeTele.ToString());
            changeInfo.Append("；新家庭电话：");
            changeInfo.Append(this.HomeTele.Text.ToString());
        }

        //判断用户是否修改了办公电话
        if (this.OfficeTele.Text != mem.OfficeTele.ToString())
        {
            flag = true;
            changeInfo.Append("\n修改了办公电话，原办公电话：");
            changeInfo.Append(mem.OfficeTele.ToString());
            changeInfo.Append("；新办公电话：");
            changeInfo.Append(this.OfficeTele.Text.ToString());
        }

        //判断用户是否修改了移动电话
        if (this.MoblieTele.Text != mem.MobileTele.ToString())
        {
            flag = true;
            changeInfo.Append("\n修改了移动电话，原移动电话：");
            changeInfo.Append(mem.MobileTele.ToString());
            changeInfo.Append("；新移动电话：");
            changeInfo.Append(this.MoblieTele.Text.ToString());
        }

        //判断用户是否修改了传真电话
        if (this.FaxTele.Text != mem.FaxTele.ToString())
        {
            flag = true;
            changeInfo.Append("\n修改了传真电话，原传真电话：");
            changeInfo.Append(mem.FaxTele.ToString());
            changeInfo.Append("；新传真电话：");
            changeInfo.Append(this.FaxTele.Text.ToString());
        }

        //判断用户是否修改了省份
        if (this.Province.Text != mem.Province.ToString())
        {
            flag = true;
            changeInfo.Append("\n修改了省份，原省份：");
            changeInfo.Append(mem.Province.ToString());
            changeInfo.Append("；新省份：");
            changeInfo.Append(this.Province.Text.ToString());
            //changeInfo.Append("；原市：");
            //changeInfo.Append(reader["city"]);
            //changeInfo.Append("；新市：");
            //changeInfo.Append(Guojia1.City);
        }
        //判断用户是否修改了市
        if (this.City.Text != mem.City.ToString())
        {
            flag = true;
            changeInfo.Append("\n修改了市，原市：");
            changeInfo.Append(mem.City.ToString());
            changeInfo.Append("；新市：");
            changeInfo.Append(this.City.Text.ToString());
        }
        //判断用户是否修改了地址
        if (this.Address.Text != mem.Address.ToString())
        {
            flag = true;
            changeInfo.Append("\n修改了地址，原地址：");
            changeInfo.Append(mem.Address.ToString());
            changeInfo.Append("；新地址：");
            changeInfo.Append(this.Address.Text.ToString());
        }

        //判断是否更改了银行的帐号和证件类型
        if ((this.PaperType.Text.ToString().Trim() != mem.PaperType.ToString().Trim()) || (this.PaperNumber.Text.Trim() != mem.PaperNumber.ToString()))
        {
            if (this.PaperType.ToString().Trim() != "")
            {
                flag = true;
                changeInfo.Append("\n修改了证件类型或证件号码，原证件类型：");
                changeInfo.Append(mem.PaperType.ToString());
                changeInfo.Append("，原证件号码：");
                changeInfo.Append(mem.PaperNumber.ToString());
                changeInfo.Append("；新证件类型：");
                changeInfo.Append(this.PaperType.ToString());
                changeInfo.Append("，新证件号码：");
                changeInfo.Append(this.PaperNumber.ToString());
            }
        }

        //判断用户是否修改了开户行
        if (this.Bank.Text.ToString() != mem.Bank.ToString())
        {
            flag = true;
            changeInfo.Append("\n修改了开户行，原开户行：");
            changeInfo.Append(mem.Bank.ToString());
            changeInfo.Append("；新开户行：");
            changeInfo.Append(this.Bank.Text.ToString());
        }


        //判断用户是否修改了银行帐号
        if (this.BankNum.Text.ToString() != mem.BankCard.ToString())
        {
            flag = true;
            changeInfo.Append("\n修改了银行帐号，原银行帐号：");
            changeInfo.Append(mem.BankCard.ToString());
            changeInfo.Append("；新银行帐号：");
            changeInfo.Append(this.BankNum.Text.ToString());
            changeInfo.Append("\n");
        }

        //判断用户是否修改了银行开户名
        if (this.BankBook.Text.ToString() != mem.BankBook.ToString())
        {
            flag = true;
            changeInfo.Append("\n修改了银行开户名，原银行开户名：");
            changeInfo.Append(mem.BankBook.ToString());
            changeInfo.Append("；新银行开户名：");
            changeInfo.Append(this.BankBook.Text.ToString());
            changeInfo.Append("\n");
        }

        string Number = this.Number.Text.ToString();
        string Placement = this.Placement.Text.ToString();
        string Direct = this.Recommended.Text.ToString();
        string Name = this.Name.Text.ToString();
        string PetName = this.PetName.Text.ToString();

        DateTime Birthday = DateTime.Parse(this.Birthday.Text.ToString());
        int Sex = 0;
        if (this.Sex.SelectedValue.ToString() == "0")
        {
            Sex = 0;
        }
        else
        {
            Sex = 1;
        }
        string PostalCode = this.PostolCode.Text.ToString();
        string HomeTele = this.HomeTele.Text.ToString();
        string OfficeTele = this.OfficeTele.Text.ToString();

        string FaxTele = this.FaxTele.Text.ToString();
        string MobileTele = this.MoblieTele.Text.ToString();
        string Country = this.Country.Text.ToString();
        string Province = this.Province.Text.ToString();
        string City = this.City.Text.ToString();

        string Address = this.Address.Text.ToString();
        string PaperNumber = this.PaperNumber.Text.ToString();
        string PaperType = this.PaperType.Text.ToString();
        string Bank = this.Bank.Text.ToString();
        string BankAddress = this.BankAdderss.Text.ToString();

        string BankCountry = this.BankCountry.Text.ToString();
        string BankProvince = this.BankProvince.Text.ToString();
        string BankCity = this.BankCity.Text.ToString();
        string BankCard = this.BankNum.Text.ToString();
        string BankBook = this.BankBook.Text.ToString();

        int ExpectNum = Convert.ToInt32(this.ExpectNum.SelectedValue.ToString());
        string Remark = this.Remark.Text.ToString();
        string OrderId = this.OrderID.Text.ToString();
        string StoreId = this.StoreID.Text.ToString();
        string ChangeInfo = "";
        if (flag == true)
        {
            ChangeInfo = changeInfo.ToString();
        }
        else
        {
            ChangeInfo = oldChangeInfo;
        }

        string OperateIp = HttpContext.Current.Request.UserHostAddress.ToString();
        //string OperaterNum = cd.OperateBh;
        string OperaterNum = "";

        updMemberInfo(Number, Placement, Direct, Name, PetName, Birthday, Sex, PostalCode, HomeTele, OfficeTele, MobileTele, FaxTele, Country, Province, City, Address, PaperType, PaperNumber, BankCountry, BankProvince, BankCity, Bank, BankAddress, BankCard, BankBook, ExpectNum, Remark, OrderId, StoreId, ChangeInfo, OperateIp, OperaterNum);

    }

    public void getMemberInfo(string Number)
    {
        MemberInfoModel member = meb.getMemberInfo(Number);

        this.Number.Text = member.Number.ToString();
        this.Placement.Text = member.Placement.ToString();
        this.Recommended.Text = member.Direct.ToString();
        this.Name.Text = member.Name.ToString();
        this.PetName.Text = member.PetName.ToString();
        this.Birthday.Text = member.Birthday.ToString();
        if (Convert.ToInt32(member.Sex) == 0)
        {
            this.Sex.SelectedValue = "0";
        }
        else
        {
            this.Sex.SelectedValue = "1";
        }
        this.PostolCode.Text = member.PostalCode.ToString();
        this.HomeTele.Text = member.HomeTele.ToString();
        this.OfficeTele.Text = member.OfficeTele.ToString();
        this.FaxTele.Text = member.FaxTele.ToString();
        this.MoblieTele.Text = member.MobileTele.ToString();
        this.Country.Text = member.Country.ToString();
        this.Province.Text = member.Province.ToString();
        this.City.Text = member.City.ToString();
        this.Address.Text = member.Address.ToString();
        this.PaperNumber.Text = member.PaperNumber.ToString();
        this.PaperType.Text = member.PaperType.ToString();
        this.Bank.Text = member.Bank.ToString();
        this.BankAdderss.Text = member.BankAddress.ToString();
        this.BankCountry.Text = member.BankCountry.ToString();
        this.BankProvince.Text = member.BankProvince.ToString();
        this.BankCity.Text = member.BankCity.ToString();
        this.BankNum.Text = member.BankCard.ToString();
        this.BankBook.Text = member.BankBook.ToString();
        // this.ExpectNum.SelectedValue = "-1";
        foreach (ListItem item3 in this.ExpectNum.Items)
        {
            string ExpectNum = member.ExpectNum.ToString();
            if (item3.Text == ExpectNum)
            {
                item3.Selected = true;
                break;
            }
        }
        this.Remark.Text = member.Remark.ToString();
        this.OrderID.Text = member.OrderID.ToString();
        this.StoreID.Text = member.StoreID.ToString();
        Session["ExpectNum"] = member.ExpectNum.ToString();
    }

    public void updMemberInfo(string Number, string Placement, string Direct, string Name, string PetName, DateTime Birthday, int Sex, string PostalCode, string HomeTele, string OfficeTele, string MobileTele, string FaxTele, string Country, string Province, string City, string Address, string PaperType, string PaperNumber, string BankCountry, string BankProvince, string BankCity, string Bank, string BankAddress, string BankCard, string BankBook, int ExpectNum, string Remark, string OrderId, string StoreId, string ChangeInfo, string OperateIp, string OperaterNum)
    {
        int i = 0;
        i = meb.updateMember(Number, Placement, Direct, Name, PetName, Birthday, Sex, PostalCode, HomeTele, OfficeTele, MobileTele, FaxTele, Country, Province, City, Address, PaperType, PaperNumber, BankCountry, BankProvince, BankCity, Bank, BankAddress, BankCard, BankBook, ExpectNum, Remark, OrderId, StoreId, ChangeInfo, OperateIp, OperaterNum);
        if (i > 0)
        {
            Page.RegisterStartupScript("", "<script language='javascript'>alert('修改成功！')</script>");
            getMemberInfo(ViewState["Number"].ToString());
        }
        else
        {
            Page.RegisterStartupScript("", "<script language='javascript'>alert('修改失败！')</script>");
        }
    }
    //根据最大期数，绑定指定的项到期数下拉框
    public void SetExpectNum(int maxExpectNum)
    {
        for (int i = 1; i <= maxExpectNum; i++)
            this.ExpectNum.Items.Add(new ListItem(i.ToString(), i.ToString()));

        this.ExpectNum.SelectedIndex = -1;
        foreach (ListItem item in this.ExpectNum.Items)
        {
            if (item.Value == maxExpectNum.ToString())
            {
                item.Selected = true;
                break;
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("QueryMemberInfo.aspx");
    }
    protected void go_Click(object sender, EventArgs e)
    {

    }
     * */
    #endregion

    /// <summary>
    /// 绑定国家
    /// </summary>
    public void BindCountry()
    {
        this.ddlCountry.DataSource = StoreInfoEditBLL.bindCountry();
        this.ddlCountry.DataValueField = "countrycode";
        this.ddlCountry.DataTextField = "name";
        this.ddlCountry.DataBind();
        ListItem li = new ListItem(GetTran("000303", "请选择"), "-1");
        ddlCountry.Items.Insert(0, li);
    }
    
    /// <summary>
    ///  根据国家选择银行
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        CommonDataBLL.BindCountry_Bank(this.ddlCountry.SelectedValue, this.DdlBank);
        ListItem li = new ListItem(GetTran("000303", "请选择"), "-1");
        DdlBank.Items.Insert(0, li);
    }
}