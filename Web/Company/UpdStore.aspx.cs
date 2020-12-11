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
using BLL.other.Company;
using BLL.CommonClass;
using DAL;
using Model;
using Model.Other;
using Encryption;

public partial class Company_UpdStore : BLL.TranslationBase
{
    protected string msg = "";
    StoreInfoEditBLL sieb = new StoreInfoEditBLL();
    CommonDataBLL CommonData = new CommonDataBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Permissions.CheckManagePermission(EnumCompanyPermission.CustomerUpdateStore);
        Response.Cache.SetExpires(DateTime.Now);
        if (!IsPostBack)
        {
            DataTable dtct = StoreInfoEditBLL.bindCountry();
            foreach (DataRow item in dtct.Rows)
            {
                this.DropDownList1.Items.Add(new ListItem(item["name"].ToString(), item["name"].ToString()));
                this.StoreCountry.Items.Add(new ListItem(item["name"].ToString(), item["name"].ToString()));
            }

            //CommonDataBLL.BindCountry_Bank(this.DropDownList1.SelectedValue.ToString(), MemberBank1);
            if (Request.QueryString["id"] != null)
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);


                DataTable dt = DBHelper.ExecuteDataTable("select id,levelint,levelstr from bsco_level where levelflag=1 order by levelint");
                foreach (DataRow row in dt.Rows)
                {
                    string str = CommonDataBLL.GetLanguageStr(int.Parse(row["id"].ToString()), "BSCO_level", "levelstr");
                    StoreLevelInt.Items.Add(new ListItem(str, row["levelint"].ToString()));
                }
                //StoreLevelInt.DataSource = dt;
                //StoreLevelInt.DataTextField = "levelstr";
                //StoreLevelInt.DataValueField = "levelint";
                //StoreLevelInt.DataBind();
                StoreLevelInt.SelectedValue = DBHelper.ExecuteScalar("select top 1 levelint from bsco_level where levelflag=1 order by levelint").ToString();
                //读取详细信息


                //保存编号
                ViewState["id"] = id;
                getDetial(id);
            }

            // if(this.CountryCity1.City!="")            
            // Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>GetCCode_s2('" + this.CountryCity1.City + "')</script>");

        }

        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.Button2, new string[][] { new string[] { "000096", "返 回" } });
        this.TranControls(this.RegularExpressionValidator2, new string[][] { new string[] { "000076", "不能输入字母,请重输！" } });
        this.TranControls(this.RegularExpressionValidator1, new string[][] { new string[] { "000076", "不能输入字母,请重输！" } });
        this.TranControls(this.RegularExpressionValidator3, new string[][] { new string[] { "000076", "不能输入字母,请重输！" } });
        this.TranControls(this.RegularExpressionValidator4, new string[][] { new string[] { "000076", "不能输入字母,请重输！" } });
        this.TranControls(this.RegularExpressionValidator5, new string[][] { new string[] { "000076", "不能输入字母,请重输！" } });
        this.TranControls(this.RegularExpressionValidator6, new string[][] { new string[] { "000076", "不能输入字母,请重输！" } });
        this.TranControls(this.RegularExpressionValidator7, new string[][] { new string[] { "000257", "邮箱格式不正确" } });
        this.TranControls(this.RegularExpressionValidator8, new string[][] { new string[] { "000262", "网址填写不正确" } });
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {

        #region 部分验证
        int exists;
        //int exists1;
        // exists1 = (int)DBHelper.ExecuteScalar("select count(*) from MemberInfo where Number = '" + this.Number.Text.ToString() + "' ");
        if (this.Number.Text.Trim() == "")
        {
            msg = "<script>alert('" + GetTran("000787", "请输入编号!") + "');</script>";
            return;
        }
        exists = (int)DBHelper.ExecuteScalar("SELECT COUNT(*) FROM MemberInfo WHERE Number='" + this.Number.Text + "'", CommandType.Text);
        if (exists <= 0)
        {
            msg = "<script language='javascript'>alert('" + GetTran("000794", "对不起，该会员编号不存在！") + "')</script>";
            return;
        }
        if (this.StoreID.Text.Trim() == "")
        {
            msg = "<script language='javascript'>alert('" + GetTran("000796", "对不起，请输入店编号！") + "')</script>";
            return;
        }
        //检查店编号是否存在
        exists = (int)DBHelper.ExecuteScalar("SELECT COUNT(*) FROM StoreInfo WHERE StoreID='" + this.StoreID.Text.Trim() + "' and id<>" + ViewState["id"].ToString(), CommandType.Text);
        if (exists > 0)
        {
            msg = "<script language='javascript'>alert('" + GetTran("000798", "对不起，该店编号已经存在！") + "')</script>";
            return;
        }
        if (this.Remark.Text.ToString().Length > 150)
        {
            msg = "<script language='javascript'>alert('" + GetTran("006896", "对不起，备注应在150个字以内！") + "')</script>";
            return;
        }
        if (this.Address.Text.ToString().Length <= 0)
        {
            msg = "<script language='javascript'>alert('" + GetTran("001280", "地址不能为空！") + "')</script>";
            return;
        }

        DateTime dt1;
        try
        {
            dt1 = Convert.ToDateTime(this.RegisterDate.Text.ToString());
        }
        catch
        {
            msg = "<script language='javascript'>alert('" + GetTran("000799", "对不起，指定的开始时间错误，请检查！") + "')</script>";
            return;
        }

        int ExpectNum;
        try
        {
            ExpectNum = Convert.ToInt32(this.ExpectNum.Text.ToString());
        }
        catch
        {
            msg = "<script language='javascript'>alert('" + GetTran("000803", "对不起，办店期数不能为空且必需是数字！") + "')</script>";
            return;
        }
        if (ExpectNum > CommonDataBLL.GetMaxqishu())
        {
            msg = "<script language='javascript'>alert('" + GetTran("000807", "对不起，办店期数不能超过最大期数！") + "')</script>";
            return;
        }
        int StorageScalar = 0;
        if (this.StorageScalar.Text != "")
        {
            try
            {
                StorageScalar = Convert.ToInt32(this.StorageScalar.Text.Trim());
            }
            catch
            {
                msg = "<script language='javascript'>alert('" + GetTran("000814", "对不起，库存底线必须是数字！") + "')</script>";
                return;
            }
        }
        decimal FareArea = 0;
        if (this.FareArea.Text != "")
        {
            try
            {
                FareArea = Convert.ToDecimal(this.FareArea.Text.ToString());
            }
            catch
            {
                msg = "<script language='javascript'>alert('" + GetTran("000818", "对不起，面积必须是数字！") + "')</script>";
                return;
            }
        }

        decimal TotalInvestMoney = 0;
        if (this.TotalInvestMoney.Text.ToString() != "")
        {
            try
            {
                TotalInvestMoney = decimal.Parse(this.TotalInvestMoney.Text.ToString());
            }
            catch
            {
                msg = "<script language='javascript'>alert('" + GetTran("000820", "对不起，投资总额必须是数字！") + "')</script>";
                return;
            }
        }
        if (this.CountryCity1.Country == "请选择")
        {
            msg = "<script language='javascript'>alert('" + GetTran("000300", "请选择地址！") + "')</script>";
            return;
        }
        if (this.CountryCity1.Province == "请选择")
        {
            msg = "<script language='javascript'>alert('" + GetTran("000300", "请选择地址！") + "')</script>";
            return;
        }
        if (this.CountryCity1.City == "请选择")
        {
            msg = "<script language='javascript'>alert('" + GetTran("000300", "请选择地址！") + "')</script>";
            return;
        }
        #endregion

        #region 上传图片
        string dirName = "";
        string oldFilePath = "";// this.PhotoPath.PostedFile.FileName.Trim();
        string oldFileName = "";
        string newFileName = "";

        int photoW = 0, photoH = 0;
        //try
        //{
        //    if (oldFilePath != string.Empty)
        //    {
        //        //如果是编辑则删除旧图
        //        if (ViewState["PhotoPath"].ToString() != string.Empty)
        //        {
        //            if (System.IO.File.Exists(Server.MapPath("../upload/" + ViewState["PhotoPath"].ToString())))
        //            {
        //                System.IO.File.Delete(Server.MapPath("../upload/" + ViewState["PhotoPath"].ToString()));
        //            }
        //        }
        //        //检查目录是否存在
        //        dirName = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString();
        //        //if (!System.IO.Directory.Exists(Server.MapPath("../upload/" + dirName)))
        //        //{
        //        //    System.IO.Directory.CreateDirectory(Server.MapPath("../upload/" + dirName));
        //        //}

        //        oldFileName = System.IO.Path.GetFileName(oldFilePath);

        //        string fileExtName = System.IO.Path.GetExtension(oldFilePath);
        //        if (fileExtName.ToLower() != ".gif" && fileExtName.ToLower() != ".jpg" && fileExtName.ToLower() != ".jpeg")
        //        {
        //            Response.Write("<script>alert('" + GetTran("000823", "上传文件格式不正确!") + "');</script>");
        //            return;
        //        }
        //        if (this.PhotoPath.PostedFile.ContentLength > 51200)
        //        {
        //            Response.Write("<script>alert('" + GetTran("000824", "上传文件不能大于50K！") + "');</script>");
        //            return;
        //        }
        //        System.Random rd = new Random(0);
        //        newFileName = DateTime.Now.Year.ToString() + rd.Next(10).ToString()
        //            + DateTime.Now.Month.ToString() + rd.Next(10).ToString()
        //            + DateTime.Now.Day.ToString() + rd.Next(10).ToString()
        //            + DateTime.Now.Second.ToString()
        //            + fileExtName;

        //       // string newFilePath = Server.MapPath("..\\upload\\" + dirName) + "\\" + newFileName;
        //       // string newFilePath = Server.MapPath("..\\upload\\") + "\\" + newFileName;
        //        string newFilePath = Server.MapPath("..\\upload\\") + newFileName;
        //        this.PhotoPath.PostedFile.SaveAs(newFilePath);
        //        try
        //        {
        //            System.Drawing.Image myIma = System.Drawing.Image.FromFile(newFilePath);
        //            photoH = myIma.Height;
        //            photoW = myIma.Width;

        //        }
        //        catch
        //        { }
        //    }
        //}
        //catch (Exception ext)
        //{
        //    msg = "<script>alert('" + ext.Message + "')</script>";
        //    return;
        //}

        #endregion

        StoreInfoModel storeInfoMember = new StoreInfoModel();
        storeInfoMember.Number = this.Number.Text.ToString();
        storeInfoMember.Name = Encryption.Encryption.GetEncryptionName(this.Name.Text.ToString());
        storeInfoMember.StoreID = this.StoreID.Text.ToString();
        storeInfoMember.StoreName = Encryption.Encryption.GetEncryptionName(this.StoreName.Text.ToString());

        storeInfoMember.StoreAddress = Encryption.Encryption.GetEncryptionAddress(this.Address.Text.ToString());
        storeInfoMember.HomeTele = Encryption.Encryption.GetEncryptionTele(this.HomeTele.Text.ToString());
        storeInfoMember.OfficeTele = Encryption.Encryption.GetEncryptionTele(this.OfficeTele.Text.ToString());
        storeInfoMember.FaxTele = Encryption.Encryption.GetEncryptionTele(this.FaxTele.Text.ToString());
        storeInfoMember.MobileTele = Encryption.Encryption.GetEncryptionTele(this.MobileTele.Text.ToString());

        string cp1 = this.MemberBank1.SelectedValue.ToString();
        storeInfoMember.BankCode = cp1.ToString();
        storeInfoMember.BankCard = Encryption.Encryption.GetEncryptionCard(this.BankCard.Text.ToString());
        storeInfoMember.Bankbranchname = txtEbank.Text.Trim();
        storeInfoMember.Email = this.Email.Text.ToString();
        storeInfoMember.NetAddress = this.NetAddress.Text.ToString();
        storeInfoMember.Remark = this.Remark.Text.ToString();

        storeInfoMember.Direct = this.Label1.Text.ToString();
        storeInfoMember.ExpectNum = Convert.ToInt32(this.ExpectNum.Text.ToString());
        storeInfoMember.RegisterDate = dt1.ToUniversalTime();
        storeInfoMember.StoreLevelInt = Convert.ToInt32(this.StoreLevelInt.SelectedItem.Value.ToString());
        //storeInfoMember.StoreLevelStr = this.StoreLevelInt.SelectedItem.Value.ToString();

        storeInfoMember.FareArea = FareArea;
        storeInfoMember.TotalInvestMoney = TotalInvestMoney;
        storeInfoMember.PostalCode = this.PostolCode.Text.ToString();

        storeInfoMember.Language = Convert.ToInt32(this.Language.Text.ToString());
        storeInfoMember.OperateIp = CommonDataBLL.OperateIP;
        storeInfoMember.OperateNum = CommonDataBLL.OperateBh;
        storeInfoMember.ID = Convert.ToInt32(ViewState["id"]);
        string cp = DBHelper.ExecuteScalar("select cpccode from city where country='" + this.CountryCity1.Country + "' and province='" + this.CountryCity1.Province + "' and city='" + this.CountryCity1.City + "' and xian='" + this.CountryCity1.Xian + "'").ToString();
        storeInfoMember.CPCCode = cp.ToString();
        //if (this.PhotoPath.PostedFile.FileName.Trim() != string.Empty)
        //{
        //    //storeInfoMember.PhotoPath  = dirName + "\\" + newFileName;
        //    storeInfoMember.PhotoPath =  "upload/"+newFileName;
        //}
        //else
        //{
        storeInfoMember.PhotoPath = "";// ViewState["Image"].ToString();
        //}
        // storeInfoMember.PhotoH = photoH;
        //  storeInfoMember.PhotoW = photoW;
        int i = 0;
        BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("storeinfo", "(ltrim(rtrim(id)))");
        cl_h_info.AddRecord(Convert.ToInt32(ViewState["id"]));//不能放到事务中  数据前 
        i = sieb.updateStore(storeInfoMember);

        if (i > 0)
        {
            cl_h_info.AddRecord(Convert.ToInt32(ViewState["id"]));//不能放到事务中  修改数据后
            cl_h_info.ModifiedIntoLogs(BLL.CommonClass.ChangeCategory.company4, Session["Company"].ToString(), BLL.CommonClass.ENUM_USERTYPE.objecttype2);//不能放到事务中

            msg = "<script language='javascript'>alert('" + GetTran("000222", "修改成功！") + "');location.href='StoreInfoModify.aspx'</script>";
        }
        else
        {
            msg = "<script language='javascript'>alert('" + GetTran("000225", "修改失败！") + "');location.href='StoreInfoModify.aspx'</script>";
        }
    }
    //读取详细信息
    public void getDetial(int id)
    {
        StoreInfoModel storeInfo = StoreInfoEditBLL.GetStoreInfoById(id);
        this.Number.Text = storeInfo.Number.ToString();
        this.StoreID.Text = storeInfo.StoreID.ToString();
        ViewState["StoreID"] = storeInfo.StoreID.ToString();
        this.Name.Text = Encryption.Encryption.GetDecipherName(storeInfo.Name.ToString());
        this.StoreName.Text = Encryption.Encryption.GetDecipherName(storeInfo.StoreName.ToString());
        this.Address.Text = Encryption.Encryption.GetDecipherAddress(storeInfo.StoreAddress.ToString());
        this.HomeTele.Text = Encryption.Encryption.GetDecipherTele(storeInfo.HomeTele.ToString());
        this.OfficeTele.Text = Encryption.Encryption.GetDecipherTele(storeInfo.OfficeTele.ToString());
        this.MobileTele.Text = Encryption.Encryption.GetDecipherTele(storeInfo.MobileTele.ToString());
        this.txtDirect.Text = storeInfo.Direct.ToString();
        this.FaxTele.Text = Encryption.Encryption.GetDecipherTele(storeInfo.FaxTele.ToString());
        txtEbank.Text = storeInfo.Bankbranchname;
        //SqlDataReader dr1 = DBHelper.ExecuteReader("select BankName from memberbank where BankCode='" + storeInfo.BankCode.ToString() + "'");
        //if (dr1.Read())
        //{
        //    this.MemberBank1.SelectedValue = dr1["BankName"].ToString();
        //}
        storeInfo.Bank = new MemberBankModel();
        if (storeInfo.BankCode.ToString() == "000000")
        {
            this.DropDownList1.SelectedValue = DBHelper.ExecuteScalar("select top 1 name from country").ToString();
        }
        else
        {
            this.DropDownList1.SelectedValue = DBHelper.ExecuteScalar("select b.name from memberbank a,country b where a.countrycode=b.id and bankcode='" + storeInfo.BankCode.ToString() + "'").ToString();
        }
        CommonDataBLL.BindCountry_Bank(this.DropDownList1.SelectedValue.ToString(), MemberBank1);
        this.MemberBank1.SelectedValue = storeInfo.BankCode.ToString();

        //this.Bank.Text = storeInfo.Bank.ToString();
        this.BankCard.Text = Encryption.Encryption.GetDecipherCard(storeInfo.BankCard.ToString());
        this.NetAddress.Text = storeInfo.NetAddress.ToString();
        this.Email.Text = storeInfo.Email.ToString();
        this.Remark.Text = storeInfo.Remark.ToString();
        this.Label1.Text = storeInfo.Direct.ToString();//推荐店铺编号
        this.Language.Text = storeInfo.Language.ToString();
        this.ExpectNum.Text = storeInfo.ExpectNum.ToString();
        // SetDate(Convert.ToDateTime(reader["RegisterDate"]));

        this.RegisterDate.Text = storeInfo.RegisterDate.AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString();


        if (storeInfo.StoreLevelInt.ToString() != "0")
        {
            this.StoreLevelInt.SelectedValue = storeInfo.StoreLevelInt.ToString(); //绑定级别 
        }
        else
        {
            this.StoreLevelInt.SelectedValue = "0";
        }
        this.FareArea.Text = storeInfo.FareArea.ToString();
        this.TotalInvestMoney.Text = storeInfo.TotalInvestMoney.ToString("f2");

        this.PostolCode.Text = storeInfo.PostalCode.ToString();

        SqlDataReader dr = DBHelper.ExecuteReader("select country,province,city,xian from city where cpccode='" + storeInfo.CPCCode.ToString() + "'");
        if (dr.Read())
        {
            this.CountryCity1.State = false;
            this.CountryCity1.SelectCountry(dr["Country"].ToString(), dr["Province"].ToString(), dr["City"].ToString(), dr["xian"].ToString());
        }
        string country = DBHelper.ExecuteScalar("select top 1 country from city where cpccode like'" + storeInfo.SCPCCode.ToString() + "%'").ToString();
        SqlDataReader dr1 = StoreInfoEditBLL.bindCity(country);
        while (dr1.Read())
        {
            ListItem list2 = new ListItem(dr1["Province"].ToString(), dr1["Province"].ToString());
            this.StoreCity.Items.Add(list2);
        }
        dr.Close();
        dr1.Close();
        this.StoreCountry.SelectedValue = country;
        string province = DBHelper.ExecuteScalar("select top 1 province from city where country='" + country + "' and cpccode like'" + storeInfo.SCPCCode.ToString() + "%'").ToString();
        this.StoreCity.SelectedValue = province;

        ViewState["PhotoPath"] = storeInfo.PhotoPath.ToString();
        ViewState["PhotoH"] = 0;
        ViewState["PhotoW"] = 0;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("StoreInfoModify.aspx");
    }
    protected void lkSubmit_Click(object sender, EventArgs e)
    {
        Button1_Click1(null, null);
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        CommonDataBLL.BindCountry_Bank(this.DropDownList1.SelectedValue.ToString(), MemberBank1);
    }
}