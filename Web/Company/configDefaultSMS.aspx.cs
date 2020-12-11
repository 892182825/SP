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
using Model.Other;
using System.Text.RegularExpressions;

public partial class Company_configDefaultSMS :BLL.TranslationBase 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.SystemBscoManage);
        if (!IsPostBack)
        {
            this.PanelReg.Visible = false;
            this.PanelRemitance.Visible = false;
            this.PanelSent.Visible = false;
            this.PanelSetreceivables.Visible = false;
            this.PaneStorePassFind.Visible = false;
            this.PaneLoginPassFind.Visible = false;
            PanelList.Visible = true;
            DefaultBind();            
        }
        Translations();
    }

    private void Translations()
    {
        this.TranControls(this.btnSetreg, new string[][] { new string[] { "005793", "设置" } });
        this.TranControls(this.btnSend, new string[][] { new string[] { "005793", "设置" } });
        this.TranControls(this.btnSetremittance, new string[][] { new string[] { "005793", "设置" } });
        this.TranControls(this.btnSetreceivables, new string[][] { new string[] { "005793", "设置" } });

        this.TranControls(this.btnRegSet, new string[][] { new string[] { "005793", "设置" } });
        this.TranControls(this.btnSetSend, new string[][] { new string[] { "005793", "设置" } });
        this.TranControls(this.btnSetRemit, new string[][] { new string[] { "005793", "设置" } });
        this.TranControls(this.btnSetRev, new string[][] { new string[] { "005793", "设置" } });
        this.TranControls(this.btnMemberFindSet, new string[][] { new string[] { "005793", "设置" } });
        this.TranControls(this.btnStoreFindSet, new string[][] { new string[] { "005793", "设置" } });
        this.TranControls(this.btnLoginPassFind, new string[][] { new string[] { "005793", "设置" } });
        this.TranControls(this.btnStorePassFind, new string[][] { new string[] { "005793", "设置" } });



        this.TranControls(this.btnBackReg, new string[][] { new string[] { "000421", "返回" } });
        this.TranControls(this.btnBackReg0, new string[][] { new string[] { "000421", "返回" } });
        this.TranControls(this.btnBackReg1, new string[][] { new string[] { "000421", "返回" } });
        this.TranControls(this.btnBackReg2, new string[][] { new string[] { "000421", "返回" } });
        this.TranControls(this.Button3, new string[][] { new string[] { "000421", "返回" } });
        this.TranControls(this.Button4, new string[][] { new string[] { "000421", "返回" } });

        this.TranControls(this.btnResetreg, new string[][] { new string[] { "006812", "重置" } });
        this.TranControls(this.btnResetSend, new string[][] { new string[] { "006812", "重置" } });
        this.TranControls(this.btnResetremittance, new string[][] { new string[] { "006812", "重置" } });
        this.TranControls(this.btnResetreceivables, new string[][] { new string[] { "006812", "重置" } });
        this.TranControls(this.btnStoreResFind, new string[][] { new string[] { "006812", "重置" } });
        this.TranControls(this.btnresetresceLoginPassFind, new string[][] { new string[] { "006812", "重置" } });

        this.TranControls(this.rbtnReg,
              new string[][]{
                    new string []{"000233","是"},
                    new string []{"000235","否"}});
        this.TranControls(this.rbtnSend ,
              new string[][]{
                    new string []{"000233","是"},
                    new string []{"000235","否"}});
        this.TranControls(this.rbtnRemittance ,
              new string[][]{
                    new string []{"000233","是"},
                    new string []{"000235","否"}});
        this.TranControls(this.rbtnReceivables,
              new string[][]{
                    new string []{"000233","是"},
                    new string []{"000235","否"}});
        this.TranControls(this.rbtLoginPassFind,
             new string[][]{
                    new string []{"000233","是"},
                    new string []{"000235","否"}});
        this.TranControls(this.rbtStorePassFind,
             new string[][]{
                    new string []{"000233","是"},
                    new string []{"000235","否"}});
    }

    /// <summary>
    /// 默认数据绑定
    /// </summary>
    private void DefaultBind()
    {
        string Ystr = GetTran("000233", "是");
        string Nstr = GetTran("000235", "否");
        DataTable dt = new BLL.MobileSMS().GetDefaultConfig();
        //注册通知
        DataRow[] dr = dt.Select("configCode=1");
        string TempOpenFlag = string.Empty;
        if (dr.Length > 0)
        {
            this.txtDefaultSMSReg.Text = dr[0]["DefaultContent"].ToString();
            this.lblRegContent.Text = dr[0]["DefaultContent"].ToString();
            TempOpenFlag = "0";
            TempOpenFlag=dr[0]["IsOpen"].ToString();
            if (TempOpenFlag == "1")
                this.lblIsOpenReg.Text = Ystr;
            else
                this.lblIsOpenReg.Text = Nstr;
            foreach(ListItem li in rbtnReg .Items )
            {
                if(li.Value .Trim ()==TempOpenFlag)
                {
                    li.Selected =true;
                }
            }           
        }
        //发货通知
        dr = dt.Select("configCode=2");
        if (dr.Length > 0)
        {
            this.txtDefaultSmsSend .Text = dr[0]["DefaultContent"].ToString();
            this.lblSendContent.Text = dr[0]["DefaultContent"].ToString();
            TempOpenFlag = "0";
            TempOpenFlag = dr[0]["IsOpen"].ToString();
            if (TempOpenFlag == "1")
                this.lblIsOpenSend.Text = Ystr;
            else
                this.lblIsOpenSend.Text = Nstr;
            foreach (ListItem li in rbtnSend.Items)
            {
                if (li.Value.Trim() == TempOpenFlag)
                {
                    li.Selected = true;
                }
            }           
        }
        //汇款审核通知
        dr = dt.Select("configCode=3");
        if (dr.Length > 0)
        {
            this.txtDefaultSmsRemittance.Text = dr[0]["DefaultContent"].ToString();
            this.lblRemitContent .Text = dr[0]["DefaultContent"].ToString();   
            TempOpenFlag ="0";
            TempOpenFlag = dr[0]["IsOpen"].ToString();
            if (TempOpenFlag == "1")
                this.lblIsOpenRemit.Text = Ystr;
            else
                this.lblIsOpenRemit.Text = Nstr;
            foreach (ListItem li in rbtnRemittance.Items)
            {
                if (li.Value.Trim() == TempOpenFlag)
                {
                    li.Selected = true;
                }
            }         
        }
        //应收帐款通知
        dr = dt.Select("configCode=4");
        if (dr.Length > 0)
        {
            this.txtDefaultSMSReceivables.Text = dr[0]["DefaultContent"].ToString();
            this.lblRevContent .Text = dr[0]["DefaultContent"].ToString();
            TempOpenFlag = "0";
            TempOpenFlag = dr[0]["IsOpen"].ToString();
            if (TempOpenFlag == "1")
                this.lblIsOpenRev.Text = Ystr;
            else
                this.lblIsOpenRev.Text = Nstr;
            foreach (ListItem li in this.rbtnReceivables.Items)
            {
                if (li.Value.Trim() == TempOpenFlag)
                {
                    li.Selected = true;
                }
            }                 
        }

        //会员找回密码
        dr = dt.Select("configCode=8");
        if (dr.Length > 0)
        {
            this.txtDefaultSMSFind.Text = dr[0]["DefaultContent"].ToString();
            this.lblMemberFind.Text = dr[0]["DefaultContent"].ToString();
            TempOpenFlag = "0";
            TempOpenFlag = dr[0]["IsOpen"].ToString();
            if (TempOpenFlag == "1")
                this.lblMemberOpen.Text = Ystr;
            else
                this.lblMemberOpen.Text = Nstr;
            foreach (ListItem li in this.rbtLoginPassFind.Items)
            {
                if (li.Value.Trim() == TempOpenFlag)
                {
                    li.Selected = true;
                }
            }
        }

        //店铺找回密码
        dr = dt.Select("configCode=9");
        if (dr.Length > 0)
        {
            this.txtStorePassFind.Text = dr[0]["DefaultContent"].ToString();
            this.lblStoreFind.Text = dr[0]["DefaultContent"].ToString();
            TempOpenFlag = "0";
            TempOpenFlag = dr[0]["IsOpen"].ToString();
            if (TempOpenFlag == "1")
                this.lblStoreOpen.Text = Ystr;
            else
                this.lblStoreOpen.Text = Nstr;
            foreach (ListItem li in this.rbtStorePassFind.Items)
            {
                if (li.Value.Trim() == TempOpenFlag)
                {
                    li.Selected = true;
                }
            }
        }
    }
    
   
    /// <summary>
    /// 发货通知设置
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSend_Click(object sender, EventArgs e)
    {
        try
        {

            string defaultContent = BLL.CommonClass.ValidData.InputText(this.txtDefaultSmsSend .Text.Trim());
            if (defaultContent.Length > 200)
            {
                BLL.CommonClass.Transforms.JSAlert(GetTran("006808", "预设内容在200字符以内。！"));
                return;                
            }
            Regex rx = new Regex(@".*\[Name\].*\[profile\].*", RegexOptions.IgnoreCase);
            if (!rx.IsMatch(defaultContent))
            {
                BLL.CommonClass.Transforms.JSAlert(GetTran("006919", "格式错误"));
                return;
            }
            int isOpen = 0;
            if (this.rbtnSend .SelectedItem.Value.Trim() == "1")
            {
                isOpen = 1;
            }
            else if (this.rbtnSend.SelectedItem.Value.Trim() == "0")
            {
                isOpen = 0;
            }
            bool flag = BLL.MobileSMS.ConfigSMS(2, defaultContent, isOpen);
            if (flag)
            {
                BLL.CommonClass.Transforms.JSAlert(GetTran("005820", "设置成功！"));
                btnBackReg_Click(null, null);
                DefaultBind();
            }
            else
            {
                BLL.CommonClass.Transforms.JSAlert(GetTran("005821", "设置失败！"));
            }
        }
        catch (Exception ex)
        {
            BLL.CommonClass.Transforms.JSAlert("设置出错：" + ex.Message);
        }
    }


    private void SetLbtnColor(LinkButton lbtn)
    {
        foreach (Control lb1 in ((Panel)FindControl("Panels")).Controls )   
        {
            foreach (Control lb in lb1.Controls)
            {
                if (lb.GetType().ToString() == "System.Web.UI.WebControls.LinkButton")
                {
                    if (lb.ID == lbtn.ID)
                    {
                        ((LinkButton)lb).ForeColor = System.Drawing.Color.Red;
                        
                    }
                    else
                    {
                        ((LinkButton)lb).ForeColor = System.Drawing.Color.FromName("#075C79");
                    }
                }
            }
        }
    }

    /// <summary>
    /// 注册通知设置
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSetreg_Click(object sender, EventArgs e)
    {
        try
        {
            string defaultContent = BLL.CommonClass.ValidData.InputText(this.txtDefaultSMSReg.Text.Trim());
            if (defaultContent.Length > 200)
            {
                BLL.CommonClass.Transforms.JSAlert(GetTran("006808", "预设内容在200字符以内。！"));
                return;
            }

            Regex rx = new Regex(@".*\[Name\].*\[profile\].*", RegexOptions.IgnoreCase);
            if (!rx.IsMatch(defaultContent))
            {
                BLL.CommonClass.Transforms.JSAlert(GetTran("006919", "格式错误"));
                return;
            }
            int isOpen = 0;
            if (this.rbtnReg.SelectedItem.Value.Trim() == "1")
            {
                isOpen = 1;
            }
            else if (this.rbtnReg.SelectedItem.Value.Trim() == "0")
            {
                isOpen = 0;
            }
            bool flag = BLL.MobileSMS.ConfigSMS(1, defaultContent, isOpen);
            if (flag)
            {
                BLL.CommonClass.Transforms.JSAlert(GetTran("005820", "设置成功！"));
                DefaultBind();
                btnBackReg_Click(null, null);

            }
            else
            {
                BLL.CommonClass.Transforms.JSAlert(GetTran("005821", "设置失败！"));
            }
        }
        catch (Exception ex)
        {
            BLL.CommonClass.Transforms.JSAlert(GetTran("006809", "处理出错") + "：" + ex.Message);
        }
    }

    /// <summary>
    /// 汇款审核通知设置
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSetremittance_Click(object sender, EventArgs e)
    {
        try
        {
            string defaultContent = BLL.CommonClass.ValidData.InputText(this.txtDefaultSmsRemittance.Text.Trim());
            if (defaultContent.Length > 200)
            {
                BLL.CommonClass.Transforms.JSAlert(GetTran("006808", "预设内容在200字符以内。！"));
                return;
            }

            Regex rx = new Regex(@".*\[Name\].*\[profile\].*", RegexOptions.IgnoreCase);
            if (!rx.IsMatch(defaultContent))
            {
                BLL.CommonClass.Transforms.JSAlert(GetTran("006919", "格式错误"));
                return;
            }
            int isOpen = 0;
            if (this.rbtnRemittance.SelectedItem.Value.Trim() == "1")
            {
                isOpen = 1;
            }
            else if (this.rbtnRemittance.SelectedItem.Value.Trim() == "0")
            {
                isOpen = 0;
            }
            bool flag = BLL.MobileSMS.ConfigSMS(3, defaultContent, isOpen);
            if (flag)
            {
                BLL.CommonClass.Transforms.JSAlert(GetTran("005820", "设置成功！"));
                DefaultBind();
                btnBackReg_Click(null, null);
            }
            else
            {
                BLL.CommonClass.Transforms.JSAlert(GetTran("005821", "设置失败！"));
            }
        }
        catch (Exception ex)
        {
            BLL.CommonClass.Transforms.JSAlert(GetTran("006809", "处理出错") + "：" + ex.Message);
        }
    }

    /// <summary>
    /// 应收账款通知设置
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSetreceivables_Click(object sender, EventArgs e)
    {
        try
        {
            string defaultContent = BLL.CommonClass.ValidData.InputText(this.txtDefaultSMSReceivables.Text.Trim());
            if (defaultContent.Length > 200)
            {
                BLL.CommonClass.Transforms.JSAlert(GetTran("006808", "预设内容在200字符以内。！"));
                return;
            }

            Regex rx = new Regex(@".*\[Name\].*\[profile\].*", RegexOptions.IgnoreCase);
            if (!rx.IsMatch(defaultContent))
            {
                BLL.CommonClass.Transforms.JSAlert(GetTran("006919", "格式错误"));
                return;
            }
            int isOpen = 0;
            if (this.rbtnReceivables.SelectedItem.Value.Trim() == "1")
            {
                isOpen = 1;
            }
            else if (this.rbtnReceivables.SelectedItem.Value.Trim() == "0")
            {
                isOpen = 0;
            }
            bool flag = BLL.MobileSMS.ConfigSMS(4, defaultContent, isOpen);
            if (flag)
            {
                BLL.CommonClass.Transforms.JSAlert(GetTran("005820", "设置成功！"));
                DefaultBind();
                btnBackReg_Click(null, null);
            }
            else
            {
                BLL.CommonClass.Transforms.JSAlert(GetTran ("005821","设置失败！"));
            }
        }
        catch (Exception ex)
        {
            BLL.CommonClass.Transforms.JSAlert(GetTran("006809", "处理出错") + "：" + ex.Message);
        }
    }

    /// <summary>
    /// 会员找回密码设置
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLoginPassFind_Click(object sender, EventArgs e)
    {
        try
        {
            string defaultContent = BLL.CommonClass.ValidData.InputText(this.txtDefaultSMSFind.Text.Trim());
            if (defaultContent.Length > 200)
            {
                BLL.CommonClass.Transforms.JSAlert(GetTran("006808", "预设内容在200字符以内。！"));
                return;
            }

            Regex rx = new Regex(@".*\[Name\].*\[profile\].*", RegexOptions.IgnoreCase);
            if (!rx.IsMatch(defaultContent))
            {
                BLL.CommonClass.Transforms.JSAlert(GetTran("006919", "格式错误"));
                return;
            }
            int isOpen = 0;
            if (this.rbtLoginPassFind.SelectedItem.Value.Trim() == "1")
            {
                isOpen = 1;
            }
            else if (this.rbtLoginPassFind.SelectedItem.Value.Trim() == "0")
            {
                isOpen = 0;
            }
            bool flag = BLL.MobileSMS.ConfigSMS(8, defaultContent, isOpen);
            if (flag)
            {
                BLL.CommonClass.Transforms.JSAlert(GetTran("005820", "设置成功！"));
                DefaultBind();
                btnBackReg_Click(null, null);
            }
            else
            {
                BLL.CommonClass.Transforms.JSAlert(GetTran("005821", "设置失败！"));
            }
        }
        catch (Exception ex)
        {
            BLL.CommonClass.Transforms.JSAlert(GetTran("006809", "处理出错") + "：" + ex.Message);
        }
    }

    protected void btnStorePassFind_Click(object sender, EventArgs e)
    {
        try
        {
            string defaultContent = BLL.CommonClass.ValidData.InputText(this.txtStorePassFind.Text.Trim());
            if (defaultContent.Length > 200)
            {
                BLL.CommonClass.Transforms.JSAlert(GetTran("006808", "预设内容在200字符以内。！"));
                return;
            }

            Regex rx = new Regex(@".*\[Name\].*\[profile\].*", RegexOptions.IgnoreCase);
            if (!rx.IsMatch(defaultContent))
            {
                BLL.CommonClass.Transforms.JSAlert(GetTran("006919", "格式错误"));
                return;
            }
            int isOpen = 0;
            if (this.rbtStorePassFind.SelectedItem.Value.Trim() == "1")
            {
                isOpen = 1;
            }
            else if (this.rbtStorePassFind.SelectedItem.Value.Trim() == "0")
            {
                isOpen = 0;
            }
            bool flag = BLL.MobileSMS.ConfigSMS(9, defaultContent, isOpen);
            if (flag)
            {
                BLL.CommonClass.Transforms.JSAlert(GetTran("005820", "设置成功！"));
                DefaultBind();
                btnBackReg_Click(null, null);
            }
            else
            {
                BLL.CommonClass.Transforms.JSAlert(GetTran("005821", "设置失败！"));
            }
        }
        catch (Exception ex)
        {
            BLL.CommonClass.Transforms.JSAlert(GetTran("006809", "处理出错") + "：" + ex.Message);
        }
    }

    protected void btnResetreg_Click(object sender, EventArgs e)
    {
        this.txtDefaultSMSReg.Text = string.Empty;
    }

    protected void btnResetSend_Click(object sender, EventArgs e)
    {
        this.txtDefaultSmsSend.Text = string.Empty;
    }

    protected void btnResetremittance_Click(object sender, EventArgs e)
    {
        this.txtDefaultSmsRemittance.Text = string.Empty;
    }
    protected void btnResetreceivables_Click(object sender, EventArgs e)
    {
        this.txtDefaultSMSReceivables.Text = string.Empty;
    }

    protected void btnresetresceLoginPassFind_Click(object sender, EventArgs e)
    {
        this.txtDefaultSMSFind.Text = string.Empty;
    }


    protected void btnStoreResFind_Click(object sender, EventArgs e)
    {
        this.txtStorePassFind.Text = string.Empty;
    }
    protected void btnRegSet_Click(object sender, EventArgs e)
    {
        this.PanelReg.Visible = true ;
        this.PanelRemitance.Visible = false;
        this.PanelSent.Visible = false;
        this.PanelSetreceivables.Visible = false;
        this.PaneLoginPassFind.Visible = false;
        this.PaneStorePassFind.Visible = false;
        PanelList.Visible = false;
    }
    protected void btnSetSend_Click(object sender, EventArgs e)
    {
        this.PanelReg.Visible = false;
        this.PanelRemitance.Visible = false;
        this.PanelSent.Visible =true;
        this.PanelSetreceivables.Visible = false;
        this.PaneLoginPassFind.Visible = false;
        this.PaneStorePassFind.Visible = false;
        PanelList.Visible = false;
    }
    protected void btnSetRemit_Click(object sender, EventArgs e)
    {
        this.PanelReg.Visible = false;
        this.PanelRemitance.Visible = true;
        this.PanelSent.Visible = false;
        this.PanelSetreceivables.Visible = false;
        this.PaneLoginPassFind.Visible = false;
        this.PaneStorePassFind.Visible = false;
        PanelList.Visible = false;
    }
    protected void btnSetRev_Click(object sender, EventArgs e)
    {
        this.PanelReg.Visible = false;
        this.PanelRemitance.Visible = false;
        this.PanelSent.Visible = false;
        this.PanelSetreceivables.Visible =true ;
        this.PaneLoginPassFind.Visible = false;
        this.PaneStorePassFind.Visible = false;
        PanelList.Visible = false;
    }


    protected void btnMemberFindSet_Click(object sender, EventArgs e)
    {
        this.PanelReg.Visible = false;
        this.PanelRemitance.Visible = false;
        this.PanelSent.Visible = false;
        this.PanelSetreceivables.Visible = false;
        this.PaneLoginPassFind.Visible = true;
        this.PaneStorePassFind.Visible = false;
        PanelList.Visible = false;
    }

    protected void btnStoreFindSet_Click(object sender, EventArgs e)
    {
        this.PanelReg.Visible = false;
        this.PanelRemitance.Visible = false;
        this.PanelSent.Visible = false;
        this.PanelSetreceivables.Visible = false;
        this.PaneLoginPassFind.Visible = false;
        this.PaneStorePassFind.Visible = true;
        PanelList.Visible = false;
    }

    protected void btnBackReg_Click(object sender, EventArgs e)
    {
        this.PanelReg.Visible = false;
        this.PanelRemitance.Visible = false;
        this.PanelSent.Visible = false;
        this.PanelSetreceivables.Visible = false;
        this.PaneLoginPassFind.Visible = false;
        this.PaneStorePassFind.Visible = false;
        PanelList.Visible = true;
    }

 
}
