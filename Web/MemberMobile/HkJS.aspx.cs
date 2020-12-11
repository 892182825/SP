using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MemberMobile_HkJS : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    protected void btn_Click(object sender, EventArgs e)
    {

        var id = Request.QueryString["id"];
        var HkJs = txtEnote.Text;
        string sql = "update Withdraw set HkJs='"+ HkJs + "' where id=" + id;
        DataTable dt = DAL.DBHelper.ExecuteDataTable(sql);
        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("009105", "汇款解释提交成功") + "！');location.href='DetailDCS.aspx'</script>", false);

    }
}