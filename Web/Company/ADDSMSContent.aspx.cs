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
using BLL.other.Company;
using BLL.CommonClass;
using System.Data.SqlClient;
using DAL;

public partial class Company_ADDSMSContent : BLL.TranslationBase
{
    protected string msg;
    protected string action;
    protected int id;

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        if (!IsPostBack)
        {

            if (Request.QueryString["action"] == "")
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("003202", "程序调用错误，请联系管理员!")));
                Response.End();
            }

            this.action = Request.QueryString["action"].Trim();
            ViewState["action"] = Request.QueryString["action"].Trim();
            //产品ID
            this.id = Convert.ToInt32(Request.QueryString["id"].Trim().Replace("N", ""));
            ViewState["ID"] = this.id;


            ViewState["CountryCode"] = "";

            //接收的国家编码
            if (Request.QueryString["countryCode"] != null)
            {
                ViewState["CountryCode"] = Request.QueryString["countryCode"].Trim();
              
            }

            else
            {
         
                //通过产品ID获取国家ID
                ViewState["CountryCode"] = SMScontentBLL.getSMScountrycode(this.id).ToString();
            }


            if (this.action == "addFold")
            {

                this.lblMessage.Text = GetTran("003212", "当前") + "：" + GetTran("007062", "添加新类");
               

                this.tr1.Visible = true;
                this.tr2.Visible = false;
                this.tr3.Visible = false;
            }

            else if (this.action == "add")
            {

                this.lblMessage.Text = GetTran("003212", "当前") + "：" + GetTran("007063", "添加短信类别");
               

                this.tr1.Visible = false;
                this.tr2.Visible = true;
                this.tr3.Visible = false;
            }

            else if (this.action == "addsms")
            {

                this.lblMessage.Text = GetTran("003212", "当前") + "：" + GetTran("007064", "添加短信");
               

                this.tr1.Visible = false;
                this.tr2.Visible = false;
                this.tr3.Visible = true;
            }

            else if (this.action == "editFold")
            {
                this.lblMessage.Text = GetTran("003212", "当前") + "：" + GetTran("007061", "编辑类别");
                this.tr1.Visible = true;
                this.tr2.Visible = false;
                this.tr3.Visible = false;

                this.txtlb.Text = SMScontentBLL.getSMSproductName(this.id);
            }

            else if (this.action == "editItem")
            {
                this.lblMessage.Text = GetTran("003212", "当前") + "：" + GetTran("007060", "编辑短信类别");
                this.tr1.Visible = false;
                this.tr2.Visible = true;
                this.tr3.Visible = false;

                this.txtSMSName.Text = SMScontentBLL.getSMSproductName(this.id);
            }

            else if (this.action == "editItemSMS")
            {
                this.lblMessage.Text = GetTran("003212", "当前") + "：" + GetTran("007059", "编辑短信");

                this.tr1.Visible = false;
                this.tr2.Visible = false;
                this.tr3.Visible = true;


                this.txtBM.Text = SMScontentBLL.getSMSproductName(this.id);
            }

            else if (this.action == "deleteItemSMS")
            {
                this.tr1.Visible = false;
                this.tr2.Visible = false;
                this.tr3.Visible = false;
                this.doAddButtton.Visible = false;

                deleteItem(this.id);
            }

            else if (this.action == "deleteItem")
            {
                this.tr1.Visible = false;
                this.tr2.Visible = false;
                this.tr3.Visible = false;
                this.doAddButtton.Visible = false;

                deleteItem(this.id);
            }

            else if (this.action == "deleteFold")
            {
                this.tr1.Visible = false;
                this.tr2.Visible = false;
                this.tr3.Visible = false;
                this.doAddButtton.Visible = false;

                deleteItem(this.id);
            }



        }
    }


    protected void doAddButtton_Click(object sender, EventArgs e)
    {
        if (ViewState["action"].ToString() == "addFold")
        {
            int a = SMScontentBLL.insertSMSContent(Convert.ToInt32(ViewState["ID"]), 1, this.txtlb.Text, ViewState["CountryCode"].ToString(), "1");
            lblMessage.Text = GetTran("001194", "保存成功！");
        }

        else if (ViewState["action"].ToString() == "add")
        {
            int a = SMScontentBLL.insertSMSContent(Convert.ToInt32(ViewState["ID"]), 0, this.txtSMSName.Text, ViewState["CountryCode"].ToString(), "1");
            lblMessage.Text = GetTran("001194", "保存成功！");
        }

        else if (ViewState["action"].ToString() == "addsms")
        {
            int a = SMScontentBLL.insertSMSContent(Convert.ToInt32(ViewState["ID"]), 2, this.txtBM.Text, ViewState["CountryCode"].ToString(), "1");
            lblMessage.Text = GetTran("001194", "保存成功！");
        }

        else if (ViewState["action"].ToString() == "editFold")
        {
            int a = SMScontentBLL.updateSMSproductName(Convert.ToInt32(ViewState["ID"]), this.txtlb.Text);
            lblMessage.Text = GetTran("000222", "修改成功！");
        }

        else if (ViewState["action"].ToString() == "editItem")
        {
            int a = SMScontentBLL.updateSMSproductName(Convert.ToInt32(ViewState["ID"]), this.txtSMSName.Text);
            lblMessage.Text = GetTran("000222", "修改成功！");
        }

        else if (ViewState["action"].ToString() == "editItemSMS")
        {
            int a = SMScontentBLL.updateSMSproductName(Convert.ToInt32(ViewState["ID"]), this.txtBM.Text);
            lblMessage.Text = GetTran("000222", "修改成功！");
        }
    }


    private void deleteItem(int id)
    {
        
        using (SqlConnection conn = new SqlConnection(DBHelper.connString))
        {
            conn.Open();
            using (SqlTransaction tran = conn.BeginTransaction())
            {
                try
                {
                    ChangeLogs cl = new ChangeLogs("smscontent", "ltrim(rtrim(str(ProductID)))");
                    cl.AddRecordtran(tran, id.ToString());

                    SMScontentBLL.deleteSMScontent(tran, id);
                    cl.AddRecordtran(tran, id.ToString());
                    cl.DeletedIntoLogs(ChangeCategory.company23, Session["Company"].ToString(), ENUM_USERTYPE.objecttype10);                          
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    lblMessage.Text = GetTran("007147", "短信删除失败，请联系管理员!");
                    return;
                }
            }
        }

        lblMessage.Text = GetTran("007146", "短信删除成功");
    }   
}
