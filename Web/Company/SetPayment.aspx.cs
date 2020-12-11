using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

///Add Namespace
using System.Globalization;
using System.IO;
using System.Text;
using Model;
using BLL.other.Company;
using BLL.CommonClass;
using DAL;
using Model.Other;

/*
 * 创建者：     陈 伟

 * 创建时间：   2010-01-22
 * 对应菜单:    系统管理->支付设置
 */

public partial class Company_SetPayment : BLL.TranslationBase
{

    /// <summary>
    /// 实例化支付方式模型

    /// </summary>
    ProductColorModel productColorModel = new ProductColorModel();
    public string msg = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        ///设置GridView的样式
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.SystemPayment);
        if (!Page.IsPostBack)
        {
            DataBindPayment();
        }
        Translations();
    }

    private void Translations()
    {
        this.TranControls(this.btnAdd, new string[][] { new string[] { "000434", "确定" } });
        TranControls(btnsetwangyin, new string[][] { new string[] { "005793", "设置" } });
        TranControls(cbkwangyinqiyong, new string[][] { new string[] { "007851", "启用" } });

    }

    /// <summary>
    /// 绑定数据
    /// </summary>
    protected void DataBindPayment()
    {
        ///服务机构“注册报单”支付方式
        DataTable cbl1 = SetParametersBLL.GetPayment(1);
        if (cbl1.Rows.Count > 0)
        {
            this.CheckBoxList1.DataSource = cbl1;
            this.CheckBoxList1.DataTextField = "paymentName";//绑定的字段名
            this.CheckBoxList1.DataValueField = "id";//绑定的值

            this.CheckBoxList1.DataBind();
            for (int i = 0; i < cbl1.Rows.Count; i++)
            {
                if (cbl1.Rows[i][2].ToString() == "1")
                {
                    CheckBoxList1.Items[i].Selected = true;
                }
            }
        }

        ///服务机构“复消报单”支付方式
        DataTable cbl2 = SetParametersBLL.GetPayment(2);
        if (cbl2.Rows.Count > 0)
        {
            this.CheckBoxList2.DataSource = cbl2;
            this.CheckBoxList2.DataTextField = "paymentName";//绑定的字段名
            this.CheckBoxList2.DataValueField = "id";//绑定的值

            this.CheckBoxList2.DataBind();
            for (int i = 0; i < cbl2.Rows.Count; i++)
            {
                if (cbl2.Rows[i][2].ToString() == "1")
                {
                    CheckBoxList2.Items[i].Selected = true;
                }
            }
        }

        ///服务机构“在线订货”支付方式
        DataTable cbl3 = SetParametersBLL.GetPayment(3);
        if (cbl3.Rows.Count > 0)
        {
            this.CheckBoxList3.DataSource = cbl3;
            this.CheckBoxList3.DataTextField = "paymentName";//绑定的字段名
            this.CheckBoxList3.DataValueField = "id";//绑定的值

            this.CheckBoxList3.DataBind();
            for (int i = 0; i < cbl3.Rows.Count; i++)
            {
                if (cbl3.Rows[i][2].ToString() == "1")
                {
                    CheckBoxList3.Items[i].Selected = true;
                }
            }
        }

        ///服务机构“在线支付  充值”方式
        DataTable cbl4 = SetParametersBLL.GetPayment(4);
        if (cbl4.Rows.Count > 0)
        {
            this.CheckBoxList4.DataSource = cbl4;
            this.CheckBoxList4.DataTextField = "paymentName";//绑定的字段名
            this.CheckBoxList4.DataValueField = "id";//绑定的值

            this.CheckBoxList4.DataBind();
            for (int i = 0; i < cbl4.Rows.Count; i++)
            {
                if (cbl4.Rows[i][2].ToString() == "1")
                {
                    CheckBoxList4.Items[i].Selected = true;
                }
            }
        }

        ///会员“注册报单”支付方式
        DataTable cbl5 = SetParametersBLL.GetPayment(5);
        if (cbl5.Rows.Count > 0)
        {
            this.CheckBoxList5.DataSource = cbl5;
            this.CheckBoxList5.DataTextField = "paymentName";//绑定的字段名
            this.CheckBoxList5.DataValueField = "id";//绑定的值

            this.CheckBoxList5.DataBind();
            for (int i = 0; i < cbl5.Rows.Count; i++)
            {
                if (cbl5.Rows[i][2].ToString() == "1")
                {
                    CheckBoxList5.Items[i].Selected = true;
                }
            }
        }

        ///会员“复消报单”支付方式
        DataTable cbl6 = SetParametersBLL.GetPayment(6);
        if (cbl6.Rows.Count > 0)
        {
            this.CheckBoxList6.DataSource = cbl6;
            this.CheckBoxList6.DataTextField = "paymentName";//绑定的字段名
            this.CheckBoxList6.DataValueField = "id";//绑定的值

            this.CheckBoxList6.DataBind();
            for (int i = 0; i < cbl6.Rows.Count; i++)
            {
                if (cbl6.Rows[i][2].ToString() == "1")
                {
                    CheckBoxList6.Items[i].Selected = true;
                }
            }
        }

        ///会员“在线支付  充值”方式
        DataTable cbl7 = SetParametersBLL.GetPayment(7);
        if (cbl7.Rows.Count > 0)
        {
            this.CheckBoxList7.DataSource = cbl7;
            this.CheckBoxList7.DataTextField = "paymentName";//绑定的字段名
            this.CheckBoxList7.DataValueField = "id";//绑定的值

            this.CheckBoxList7.DataBind();
            for (int i = 0; i < cbl7.Rows.Count; i++)
            {
                if (cbl7.Rows[i][2].ToString() == "1")
                {
                    CheckBoxList7.Items[i].Selected = true;
                }
            }
        }


        ///网银支付直连接口选择
        DataTable rod8 = SetParametersBLL.GetPayment(8);
        if (rod8.Rows.Count > 0)
        {
            this.rdowangyinlist.DataSource = rod8;
            this.rdowangyinlist.DataTextField = "paymentName";//绑定的字段名
            this.rdowangyinlist.DataValueField = "id";//绑定的值

            this.rdowangyinlist.DataBind();
            for (int i = 0; i < rod8.Rows.Count; i++)
            {
                if (rod8.Rows[i][2].ToString() == "1")
                {
                    rdowangyinlist.Items[i].Selected = true;
                }
            }
        }

        ///网银支付直连接口选择
        DataTable rod9 = SetParametersBLL.GetPayment(9);
        if (rod9.Rows.Count > 0)
        {
            if (rod9.Rows[0][2].ToString() == "1")
            {
                cbkwangyinqiyong.Checked = true;
            }
        }


    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        int count = 0;
        //count = CheckMsg(CheckBoxList1);
        //if (count < 1)
        //{
        //    msg = "<script language='javascript'>alert('服务机构“注册报单”支付方式！');</script>";
        //    return;
        //}

        count = CheckMsg(CheckBoxList2);
        if (count < 1)
        {
            msg = "<script language='javascript'>alert('" + GetTran("007956", "服务机构“复消报单”支付方式") + "！');</script>";
            return;
        }
        count = CheckMsg(CheckBoxList3);
        if (count < 1)
        {
            msg = "<script language='javascript'>alert('" + GetTran("007957", "服务机构“在线订货”支付方式") + "！');</script>";
            return;
        }
        count = CheckMsg(CheckBoxList4);
        if (count < 1)
        {
            msg = "<script language='javascript'>alert('" + GetTran("007958", "服务机构“在线支付--充值”方式") + "！');</script>";
            return;
        }
        //count = CheckMsg(CheckBoxList5);
        //if (count < 1)
        //{
        //    msg = "<script language='javascript'>alert('会员“注册报单”支付方式！');</script>";
        //    return;
        //}
        count = CheckMsg(CheckBoxList6);
        if (count < 1)
        {
            msg = "<script language='javascript'>alert('" + GetTran("007960", "会员“复消报单”支付方式") + "！');</script>";
            return;
        }
        count = CheckMsg(CheckBoxList7);
        if (count < 1)
        {
            msg = "<script language='javascript'>alert('" + GetTran("007961", "会员“在线支付--充值”方式") + "！');</script>";
            return;
        }

        using (SqlConnection conn = new SqlConnection(DBHelper.connString))
        {
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            try
            {
                count = 0;
                count = UpdatePayment(CheckBoxList1, 1, tran);
                if (count < 1)
                {
                    msg = "<script language='javascript'>alert('" + GetTran("000302", "提交失败") + "');</script>";
                    tran.Rollback();
                    return;
                }

                count = UpdatePayment(CheckBoxList2, 2, tran);
                if (count < 1)
                {
                    msg = "<script language='javascript'>alert('" + GetTran("000302", "提交失败") + "');</script>";
                    tran.Rollback();
                    return;
                }
                count = UpdatePayment(CheckBoxList3, 3, tran);
                if (count < 1)
                {
                    msg = "<script language='javascript'>alert('" + GetTran("000302", "提交失败") + "');</script>";
                    tran.Rollback();
                    return;
                }
                count = UpdatePayment(CheckBoxList4, 4, tran);
                if (count < 1)
                {
                    msg = "<script language='javascript'>alert('" + GetTran("000302", "提交失败") + "');</script>";
                    tran.Rollback();
                    return;
                }
                count = UpdatePayment(CheckBoxList5, 5, tran);
                if (count < 1)
                {
                    msg = "<script language='javascript'>alert('" + GetTran("000302", "提交失败") + "');</script>";
                    tran.Rollback();
                    return;
                }
                count = UpdatePayment(CheckBoxList6, 6, tran);
                if (count < 1)
                {
                    msg = "<script language='javascript'>alert('" + GetTran("000302", "提交失败") + "');</script>";
                    tran.Rollback();
                    return;
                }
                count = UpdatePayment(CheckBoxList7, 7, tran);
                if (count < 1)
                {
                    msg = "<script language='javascript'>alert('" + GetTran("000302", "提交失败") + "');</script>";
                    tran.Rollback();
                    return;
                }
                tran.Commit();
                msg = "<script language='javascript'>alert('" + GetTran("001881", "提交成功") + "');</script>";
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                tran.Rollback();
                msg = "<script language='javascript'>alert('" + GetTran("000302", "提交失败") + "');</script>";
                return;
            }
            finally
            {
                conn.Close();
            }

        }
    }

    private int UpdatePayment(CheckBoxList cbl, int isStore, SqlTransaction tran)
    {
        int count = 0;
        for (int i = 0; i < cbl.Items.Count; i++)
        {
            int availability = 0;
            if (cbl.Items[i].Selected)
            {
                availability = 1;
            }


            count += DocTypeTableDAL.UpdPaymentType(availability, Convert.ToInt32(cbl.Items[i].Value), isStore, tran);
        }
        return count;
    }

    private int CheckMsg(CheckBoxList cbl)
    {
        int count = 0;
        for (int i = 0; i < cbl.Items.Count; i++)
        {
            if (cbl.Items[i].Selected)
            {
                count++;
            }
        }
        return count;
    }
    protected void btnsetwangyin_Click(object sender, EventArgs e)
    {
        int ck = cbkwangyinqiyong.Checked ? 1 : 0;

        int count = DocTypeTableDAL.UpdPaymentwangyinType(Convert.ToInt32(this.rdowangyinlist.SelectedValue), 8, ck);
        if (count > 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("005820", "设置成功！") + "');</script>");
        }
    }
}
