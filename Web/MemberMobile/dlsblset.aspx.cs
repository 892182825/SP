using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class MemberMobile_dlsblset : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        int countdls = Convert.ToInt32(DAL.DBHelper.ExecuteScalar("select count(0) from dlssettb where number='" + Session["Member"] + "'"));

        if (countdls <= 0)
        {
            Response.Redirect("first.aspx", true);
        }
    }

    protected void sub_Click(object sender, EventArgs e)
    {
        string opbh = Session["Member"].ToString();
        int qs = DAL.CommonDataDAL.getMaxqishu();
        if (txtbh.Text.Trim() == "")
        {
            ScriptHelper.SetAlert(Page, "会员编号不能为空！");
            return;
        }

        if (txtbh.Text.Trim().ToLower() == opbh.ToLower())
        {
            ScriptHelper.SetAlert(Page, "会员编号不能为登录编号！");
            return;
        }

        SqlParameter[] sp = new SqlParameter[] {
            new SqlParameter("@bh",txtbh.Text.Trim()),
        };
        int count = Convert.ToInt32(DAL.DBHelper.ExecuteScalar("select count(0) from memberinfo where number=@bh", sp, CommandType.Text));

        if (count == 0)
        {
            ScriptHelper.SetAlert(Page, "会员编号不存在！");
            return;
        }

        string nowbh = txtbh.Text.Trim();
        DataTable zjxj = DAL.DBHelper.ExecuteDataTable("select * from memberinfo where direct='" + opbh + "'");
        string tdbh = "";
        Boolean flag2 = true;
        Boolean flag3 = true;
        while (true)
        {
            if (nowbh == "8888888888")
            {
                break;
            }
            if (nowbh.ToLower() == opbh.ToLower())
            {
                flag2 = false;
                break;
            }

            nowbh = DAL.DBHelper.ExecuteScalar("select direct from memberinfo where number='" + nowbh + "'").ToString();

            if (flag3)
            {
                for (int i = 0; i < zjxj.Rows.Count; i++)
                {
                    if (nowbh.ToLower() == zjxj.Rows[i]["Number"].ToString().ToLower())
                    {
                        flag3 = false;
                        tdbh = zjxj.Rows[i]["Number"].ToString();
                        break;
                    }
                }
            }

        }

        if (flag2)
        {
            ScriptHelper.SetAlert(Page, "会员编号必须在您的团队内！");
            return;
        }

        if (txtBl.Text.Trim() == "")
        {
            ScriptHelper.SetAlert(Page, "比例不能为空！");
            return;
        }

        double bl = 0;

        Boolean flag = double.TryParse(txtBl.Text.Trim(), out bl);

        if (!flag)
        {
            ScriptHelper.SetAlert(Page, "比例必须是数值！");
            return;
        }

        if (bl < 0.01 || bl > 0.2)
        {
            ScriptHelper.SetAlert(Page, "比例必须是大于等于0.01并且小于等于0.2的小数！");
            return;
        }

        count = Convert.ToInt32(DAL.DBHelper.ExecuteScalar("select count(0) from Dls_bl where number='" + txtbh.Text.Trim() + "' and qs=" + qs, sp, CommandType.Text));

        if (count > 0)
        {
            ScriptHelper.SetAlert(Page, "该会员编号已设置，请不要重复设置！");
            return;
        }

        double nowbl = Convert.ToDouble(DAL.DBHelper.ExecuteScalar(@";with cte as
	(
		select number,Direct as TFlag from memberinfo where number = '" + tdbh + @"' 
		union all
		select a.number,a.Direct
		from memberinfo a join cte as f on a.Direct = f.number 
	)
	select isnull(SUM(d.bl),0) from cte ,Dls_bl d  where cte.Number=d.number and d.QS=" + qs));

        if (nowbl + bl > 0.2)
        {
            ScriptHelper.SetAlert(Page, "整个团队的比例相加不能超过0.2！");
            return;
        }

        count = DAL.DBHelper.ExecuteNonQuery("insert into Dls_bl select '" + txtbh.Text.Trim() + "'," + bl + "," + qs + ",0,'" + opbh + "'");

        if (count > 0)
        {
            txtbh.Text = "";
            txtBl.Text = "";
            hid_fangzhi.Value = "0";
            ScriptHelper.SetAlert(Page, "设置成功！");

        }
        else {
            ScriptHelper.SetAlert(Page, "设置失败！");
        }
    }
}