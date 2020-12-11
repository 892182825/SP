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
using BLL;

//Add Namespace
using Model.Other;
using BLL.CommonClass;
using BLL.other.Company;
using Encryption;

/*
 * 创建者：     汪  华
 * 创建时间：   2009-09-10
 * 对应菜单：   系统管理->密码修改
 */

public partial class Company_PassModify : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        //检查相应权限
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.SystemPwdModify);
        if (Session["Company"] == null)
        {
            Response.End();
        }
        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.btnSubmit, new string[][] { new string[] { "000434", "确 定" } });
        this.TranControls(this.btnReset, new string[][] { new string[] { "000499", "重 填" } });


    }   
    
    /// <summary>
    /// 提交
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtOldPassword.Text.Trim() != "" && txtNewPassword.Text.Trim() != "" && txtInputAgainNewPassword.Text.Trim() != "")
        {
            if (txtNewPassword.Text.Trim().Length < 4 || txtNewPassword.Text.Trim().Length > 10)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001787", "密码长度在4到10之间!")));
            }

            else
            {
                ///老密码加密
                string oldPass = Encryption.Encryption.GetEncryptionPwd(txtOldPassword.Text.Trim(), Session["Company"].ToString());

                ///通过管理员编号和登录密码获取行数
                int check = PassModifyBLL.GetCountByNumAndLoginPass(Session["Company"].ToString(), oldPass);
                if (check == 1)
                {
                    if (txtNewPassword.Text.Trim() == txtInputAgainNewPassword.Text.Trim())
                    {
                        ///新密码加密
                        string newPass = Encryption.Encryption.GetEncryptionPwd(txtNewPassword.Text.Trim(), Session["Company"].ToString());

                        string userid = Session["Company"].ToString().Trim();
                        // 添加修改记录日志
                        ChangeLogs cl = new ChangeLogs("Manage", "Number");
                        string[] columns = { "LoginPass" };

                        cl.AddRecord(userid, columns);

                        ///更改管理员登录密码
                        int updCount=PassModifyBLL.UpdManageLoginPass(Session["Company"].ToString().Trim(), newPass);
                       
                        if (updCount > 0)
                        {
                            cl.AddRecord(userid, columns);
                            cl.ModifiedIntoLogs(ChangeCategory.company27, userid, ENUM_USERTYPE.objecttype6);

                            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001362", "密码修改成功!")));
                        }                       

                        else
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001790", "密码修改失败，请联系管理员!")));

                        }
                    }

                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001792", "两次输入的新密码不一样!")));
                    }                                 
                }

                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001794", "原密码错误!")));
                    return;
                }
            }            
        }

        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001796", "原密码，新密码和确认新密码都不能为空!")));
            return;
        }
    }
    
    /// <summary>
    /// 清空
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtOldPassword.Text = "";
        txtNewPassword.Text = "";
        txtInputAgainNewPassword.Text = "";      
    }
}
