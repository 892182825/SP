using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using BLL.other.Company;
using System.Text.RegularExpressions;
public partial class MemberMobile_PhoneSettings_SettingsIndex : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        //Response.Cache.SetExpires(DateTime.Now);
        if (!IsPostBack) 
        {
            var Member = Session["Member"];
            if (Member != null)
            {
                MemberInfoModel member = MemInfoEditBLL.getMemberInfo(Member.ToString());
                if (member != null)
                {
                    if (!string.IsNullOrEmpty(member.MobileTele))
                    {
                        labtel.InnerText = Regex.Replace(member.MobileTele, "(\\d{3})\\d{6}(\\d{2})", "$1****$2");
                    }

                    if (!string.IsNullOrEmpty(member.PaperNumber))
                    {
                        if (member.PaperNumber.Length <= 8)
                        {
                            labname.InnerText = member.PaperNumber;
                        }
                        else
                        {
                            labname.InnerText = Common.ReplaceWithSpecialChar(member.PaperNumber, 3, 4, '*');
                        }

                    }


                    if (!string.IsNullOrEmpty(member.BankCard))
                    {

                        if (member.BankCard.Length <= 8)
                        {
                            labbankcard.InnerText = member.BankCard;
                        }
                        else
                        {
                           labbankcard.InnerText = Common.ReplaceWithSpecialChar(member.BankCard, 3, 4, '*');
                        }
                    }
                   
                    if (!string.IsNullOrEmpty(member.Zhifubao))
                    {
                        if (member.Zhifubao.Length <= 4)
                        {
                            labzhifubao.InnerText = member.Zhifubao;
                        }
                        else
                        {
                            labzhifubao.InnerText = Common.ReplaceWithSpecialChar(member.Zhifubao, 2, 2, '*');
                        }
                    }

                    if (!string.IsNullOrEmpty(member.Weixin))
                    {
                        if (member.Weixin.Length <= 6)
                        {
                            labweixin.InnerText = member.Weixin;
                        }
                        else
                        {
                            labweixin.InnerText = Common.ReplaceWithSpecialChar(member.Weixin, 2, 2, '*');
                        }
                    }
                }
            }
            else
            {
                Response.Redirect("../index.aspx");
            }
        }
    }
    protected void btn_reset_Click(object sender, EventArgs e)
    {
        Session.Remove("Member");
        Response.Redirect("../../logoutsj.aspx?tp=huiy");
    }
}