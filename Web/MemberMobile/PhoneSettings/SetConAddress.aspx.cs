using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using BLL.other.Member;
using BLL.CommonClass;
using DAL;
using Model;
using System.Data.SqlClient;
using System.Data;
public partial class MemberMobile_PhoneSettings_SetConAddress : BLL.TranslationBase
{

    protected string type = "", url = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        type = Request.QueryString["type"] == "" ? "" : Request.QueryString["type"];
        url = Request.QueryString["url"] == "" ? "" : Request.QueryString["url"];
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        if (!IsPostBack)
        {
            var member=Session["Member"];
            if (member != null) 
            {
                GetOldMemberData(member.ToString());
            }
        }
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        string MobileTele = Encryption.Encryption.GetEncryptionTele(Txtyddh.Text.Trim());
        UserControl_CountryCityCode_mobile countrycity = Page.FindControl("CountryCity1") as UserControl_CountryCityCode_mobile;
        string Country = countrycity.Country;
        string Province = countrycity.Province;
        string City = countrycity.City;
        string Xian = countrycity.Xian;
        string Address = Encryption.Encryption.GetEncryptionAddress(txtdizhi.Text.Trim());
        string CPCCode = DAL.CommonDataDAL.GetCPCCode(Country, Province, City, Xian);
        string Consignee = TxtyConsignee.Text.Trim();
        string conZipCode = Txtyb1.Text.Trim();
        var Member = Session["Member"];
        if (Member != null)
        {
            var query = @"select COUNT(0) from ConsigneeInfo where Number=@Number";
            SqlParameter[] querypara = {
                                      new SqlParameter("@Number",SqlDbType.VarChar),
                                  };
            querypara[0].Value = Member.ToString();
            var queryvalue = DBHelper.ExecuteScalar(query, querypara, CommandType.Text);
            if (queryvalue.ToString() == "0")
            {
                var insert = @"INSERT INTO [ConsigneeInfo]
                               ([Number],[Consignee],[MoblieTele],[CPCCode],[Address]
                               ,ConZipCode,IsDefault
                                )
                         VALUES
                               (@Number,@Consignee,@MoblieTele,@CPCCode,@Address,@ConZipCode,@IsDefault)";

                SqlParameter[] insertpara = {
                                      new SqlParameter("@Number",SqlDbType.VarChar),
                                      new SqlParameter("@Consignee",SqlDbType.VarChar),
                                      new SqlParameter("@MoblieTele",SqlDbType.VarChar),
                                      new SqlParameter("@CPCCode",SqlDbType.VarChar),
                                      new SqlParameter("@Address",SqlDbType.VarChar),
                                      new SqlParameter("@ConZipCode",SqlDbType.VarChar),
                                      new SqlParameter("@IsDefault",SqlDbType.Bit),
                                  };
                insertpara[0].Value = Member.ToString();
                insertpara[1].Value = Consignee ;
                insertpara[2].Value = MobileTele;
                insertpara[3].Value = CPCCode;
                insertpara[4].Value = Address;
                insertpara[5].Value = conZipCode;
                insertpara[6].Value = true;

                var insertvalue = DBHelper.ExecuteNonQuery(insert, insertpara, CommandType.Text);

                if (insertvalue > 0)
                {
                    if (url != null&& url == "AddLsOrder")
                    {
                        Response.Redirect("../AddLsOrderNew.aspx?type=" + Request["type"]);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('添加收货地址成功');</script>", false);
                        Response.Redirect("SettingsIndex.aspx?res=success&&type=fanhui");
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('添加收货地址失败');</script>", false);
                }
            }
            else 
            {
                int id = Convert.ToInt32(hiddid.Value);
                var value = updateConsignee(Member.ToString(), MobileTele, CPCCode, Address, Consignee, conZipCode, id);
                if (value > 0)
                {
                    if (url != null )
                    {
                        if (url == "AddLsOrder")
                        {
                            Response.Redirect("../AddLsOrderNew.aspx?type=" + Request["type"]);
                        }
                        if (url == "futou")
                        {
                            Response.Redirect("../ReCast.aspx");
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('修改收货地址成功');</script>", false);
                        Response.Redirect("SettingsIndex.aspx?res=success&&type=fanhui");
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('修改收货地址失败');</script>", false);
                }
            }
        }
    }

    private void GetOldMemberData(string number) 
    {
        ConsigneeInfo consigneeInfo = MemberInfoModifyBll.getconsigneeInfo(number, true);
            //getconsigneeInfo(number);
        if (consigneeInfo!=null)
        {
            Txtyddh.Text = Encryption.Encryption.GetDecipherTele(consigneeInfo.MoblieTele);
            UserControl_CountryCityCode_mobile countrycity = Page.FindControl("CountryCity1") as UserControl_CountryCityCode_mobile;
            //取得该会员地址
            CityModel MemberArea = DAL.CommonDataDAL.GetCPCCode(consigneeInfo.CPCCode);
            countrycity.SelectCountry(MemberArea.Country, MemberArea.Province, MemberArea.City, MemberArea.Xian);
            TxtyConsignee.Text = consigneeInfo.Consignee;
            txtdizhi.Text = consigneeInfo.Address;
            hiddid.Value=consigneeInfo.Id.ToString();
            Txtyb1.Text = consigneeInfo.ConZipCode;
        }
    }

    private int updateConsignee(string number, string mobileTele, string CPCCode, string Address, string Consignee,string conZipCode, int id)
    {
        var sql = @"update ConsigneeInfo set MoblieTele=@MobileTele,CPCCode=@CPCCode,[Address]=@Address,Consignee=@Consignee,ConZipCode=@conZipCode
where Number=@Number and ID=@ID";
        SqlParameter[] para = {
                                      new SqlParameter("@MobileTele",SqlDbType.VarChar),
                                      new SqlParameter("@CPCCode",SqlDbType.VarChar),
                                      new SqlParameter("@Address",SqlDbType.VarChar),
                                      new SqlParameter("@Consignee",SqlDbType.VarChar),
                                      new SqlParameter("@ConZipCode",SqlDbType.VarChar),
                                      new SqlParameter("@Number",SqlDbType.VarChar),
                                      new SqlParameter("@ID",SqlDbType.Int)
                                  };
        para[0].Value = mobileTele;
        para[1].Value = CPCCode;
        para[2].Value = Address;
        para[3].Value = Consignee;
        para[4].Value = conZipCode;
        para[5].Value = number;
        para[6].Value = id;
        var returnvalue = DBHelper.ExecuteNonQuery(sql, para, CommandType.Text);

        return returnvalue;
    }
}