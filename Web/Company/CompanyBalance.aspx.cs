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
using BLL.MoneyFlows;
using Model.Other;
using System.Collections.Generic;
using DAL;
using System.Drawing;
using System.Diagnostics; 
using System.IO;
using System.Data.SqlClient;
public partial class Company_CompanyBalance : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Permissions.CheckManagePermission(EnumCompanyPermission.FinanceJiesuan); //�����ӦȨ��

        this.addNewQishu.Attributes["onclick"] = "return confirm('" + GetTran("001139", "ȷ��Ҫ������һ����?") + "')";
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        Translations();
        if (!IsPostBack)
        {
            int MaxQs = ReleaseBLL.GetMaxExpectNum();
            if (MaxQs <= 10)
            {
                this.TextBox1.Text = "1";//������ʼ��
                this.TextBox2.Text = Convert.ToString(MaxQs);
            }
            else
            {
                this.TextBox1.Text = Convert.ToString(MaxQs - 10);
                this.TextBox2.Text = Convert.ToString(MaxQs);
            }
            this.Label1.Text = "(" + GetTran("001140", "��ǰ����Ϊ") + "<font color=red>" + MaxQs.ToString() + "</font>��" + GetTran("001141", "������") + "1-" + MaxQs.ToString() + GetTran("001142", "��Χ�ڵ�����") + ")";

            this.showTotalQishuLink1();


            //�Զ������Ƿ�����
            if (ReleaseDAL.GetzidongjsQy() == "1")
                readerData();
            else
            {
                TextBox5.Enabled = false; //���������ı���
                DropDownList1.Enabled = false; //ʱ ������
                DropDownList2.Enabled = false;
                DropDownList3.Enabled = false;
                Button3.Enabled = false;
                jszq.Enabled = false;
                ClientScript.RegisterStartupScript(GetType(), "", "<script>enabledCK();</script>");
            }
        }
        Translations();

        nowtimeid.Text = DateTime.Now.ToString();
    }

    public void readerData()
    {
        DataTable dt = ReleaseDAL.Getzidongjiesuan();
        if (dt.Rows.Count > 0)
        {
            string[] sj = dt.Rows[0]["jiesuantime"].ToString().Split(' ');

            TextBox5.Text = sj[0] + "-" + sj[1];

            string[] fm = sj[1].Split(':');

            DropDownList1.SelectedValue = fm[0];
            DropDownList2.SelectedValue = fm[1].Replace("AM", "");
            DropDownList3.SelectedValue = fm[2];

            jszq.Text = dt.Rows[0]["jiesuanZQ"].ToString();//��������

            if (dt.Rows[0]["isCNewQi"].ToString() == "1")
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>window.onload=function(){setCk()};</script>");
            }

            shijid.Text = dt.Rows[0]["jiesuantime"].ToString();
        }


        CheckBox2.Checked = true;
    }

    private void Translations()
    {
        this.TranControls(this.GridView1,
                new string[][]{
                    new string []{"001179","��������"},
                    new string []{"001181","��������"},
                    new string []{"001182","�Ƿ��ѽ���"},
                    new string []{"001184","�������"},
                    new string []{"000015","����"}

                });
        this.TranControls(this.Button1, new string[][] { new string[] { "000434", "ȷ ��" } });
        this.TranControls(this.btnjslog, new string[][] { new string[] { "007096", "������־" } });
        this.TranControls(this.addNewQishu, new string[][] { new string[] { "001173", "������һ��" } });
        TranControls(Button3, new string[][] { new string[] { "000434", "ȷ ��" } });

    }
    private void showTotalQishuLink1()
    {
        this.GridView1.DataSource = ReleaseBLL.GetConfigInfo();
        this.GridView1.DataBind();
        Translations();
    }
    /// <summary>
    /// ��ʾ���������Ľ������ӡ�
    /// </summary>
    /// <param name="maxqishu">Ҫ��ʾ��������Χ����1����ǰֵ��</param>
    private void showTotalQishuLink(int startqishu, int maxqishu)
    {
        IList<ConfigModel> list = ReleaseBLL.showTotalQishuLink(startqishu, maxqishu);
        foreach (ConfigModel info in list)
        {
            try
            {

                info.Date = DateTime.Parse(info.Date).AddHours(Convert.ToDouble(Session["WTH"])).ToShortDateString();
            }
            catch
            {

            }

        }
        this.GridView1.DataSource = list;
        this.GridView1.DataBind();
    }


    /// <summary>
    /// ������һ��
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void addNewQishu_Click(object sender, EventArgs e)
    {
        int AudCount = Permissions.GetPermissions(EnumCompanyPermission.FinanceNewQi);
        if (AudCount != 4202)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000847", "�Բ�����û��Ȩ�ޣ�") + "');</script>");
            return;
        }


        int temp = ReleaseBLL.GetMaxExpectNum();
        try
        {
            Application.UnLock();
            Application.Lock();
            //������һ��
            ReleaseBLL.addNewQishu(temp + 1);

            RecycleApplication();
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("001143", "��һ�ڴ�����ɣ������µ�¼��") + "');top.location.href('index.aspx')</script>");
        }
        catch(Exception eps)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("001144", "������һ��ʱ�����������´�����") + "')</script>");
        }
        finally
        {
            Application.UnLock();
            Application["jinzhi"] = "F";
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        int StartQishu = 0, EndQishu = 0;
        try
        {
            StartQishu = Convert.ToInt32(this.TextBox1.Text.Trim());
            EndQishu = Convert.ToInt32(this.TextBox2.Text.Trim());
        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("006915", "�Բ�������ȷ���룡") + "')</script>");
            return;
        }

        if (StartQishu <= 0 || EndQishu <= 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("001147", "������������������") + "')</script>");
            return;
        }

        if (EndQishu > ReleaseBLL.GetMaxExpectNum())
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("001150", "���ܴ������������") + "')</script>");
            return;
        }

        if (StartQishu <= EndQishu)
        {
            showTotalQishuLink(StartQishu, EndQishu);
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("001152", "����ȷ�����ֹ˳��") + "')</script>");
            return;
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
            //string qishu = ((HtmlInputHidden)e.Row.FindControl("HidQishu")).Value;
            //HyperLink link = (HyperLink)e.Row.FindControl("HyperJieSuan");
            //System.Web.UI.HtmlControls.HtmlGenericControl div = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Row.FindControl("jiesuan");
            //if (ReleaseBLL.GetIsExistsConfig(int.Parse(qishu)))
            //{
            //    if (ReleaseBLL.IsNotProvideBonus(int.Parse(qishu)) == false)//�����Ƿ���δ���ŵĽ���
            //    {
            //        //û��
            //        div.InnerHtml = "<a href='javascript:openCountWin(" + qishu + ")'>" + GetTran("001154", "�����") + qishu + GetTran("000157", "��") + "</a>";
            //    }
            //    else
            //    {
            //        div.InnerHtml = "<a href=\"SalaryGrant.aspx\" onClick=\"return confirm('" + GetTran("000156", "��") + qishu + GetTran("001159", "����δ���ŵĽ���,��[����]���ٽ���...") + "');\">" + GetTran("001154", "�����") + qishu + GetTran("000157", "��") + "</a>";
            //        ReleaseBLL.upProvideState(int.Parse(qishu));
            //    }
            //}

            try
            {
                e.Row.Cells[0].Text = DateTime.Parse(e.Row.Cells[0].Text).AddHours(Convert.ToDouble(Session["WTH"])).ToShortDateString();
            }
            catch
            {

            }

            if (e.Row.Cells[2].Text == "0")
                e.Row.Cells[2].Text = GetTran("000235", "��");
            else
                e.Row.Cells[2].Text = GetTran("000233", "��");
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            //�󶨱�ͷ����ͼ
            e.Row.Attributes.Add("style", "background-image:url('images/tabledp.gif')");
            Translations();
        }
    }

    /// <summary>
    /// ���յ�ǰӦ�ó�����Դ
    /// </summary>
    /// <returns></returns>
    public bool RecycleApplication()
    {
        bool success = true;
        try
        {
            HttpRuntime.UnloadAppDomain();
        }
        catch (Exception ex)
        {
            success = false;
        }
        return success;
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string rq = TextBox5.Text.Trim();

        string hour = DropDownList1.SelectedValue;
        string min = DropDownList2.SelectedValue;
        string sec = DropDownList3.SelectedValue;

        int isjs = 0;

        if (Textbox4.Text == "y")
            isjs = 1; //����ʱ������һ��ѡ��

        if (DateTime.Compare(Convert.ToDateTime(rq + " " + hour + ":" + min + ":" + sec), DateTime.Now) < 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("007128", "ѡ�����ڲ���С�ڵ�ǰʱ�䣡") + "');</script>");
            return;
        }

        string zqts = jszq.Text.Trim();

        try
        {
            Convert.ToInt32(zqts);
        }
        catch
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('�Զ���������ֻ��Ϊ������');</script>");
            return;
        }

        if (Convert.ToInt32(zqts) < 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('�Զ��������ڲ���Ϊ������');</script>");
            return;
        }

        int a = ReleaseDAL.UpdJiesuantime(rq + " " + hour + ":" + min + ":" + sec, isjs, zqts);

        if (a == 1)
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('���óɹ���');window.location.href=window.location.href</script>");
        else
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('����ʧ�ܣ�');</script>");


    }
    protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        string isqy = "0";

        if (CheckBox2.Checked)
            isqy = "1";

        //�����Զ������Ƿ�����
        if (ReleaseDAL.UpdJiesuanQy(isqy) == 1)
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('���óɹ���');window.location.href=window.location.href;</script>");
        else
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('����ʧ�ܣ�');window.location.href=window.location.href;</script>");
    }
    protected void btnjslog_Click(object sender, EventArgs e)
    {
        Response.Redirect("jslog.aspx");
    }
}