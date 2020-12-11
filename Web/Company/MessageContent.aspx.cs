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
using BLL.other.Company;
using DAL;
using System.Data.SqlClient;


public partial class Company_MessageContent : BLL.TranslationBase
{
    private readonly BLL.other.Company.MessageReceiveBLL bll = new MessageReceiveBLL();
    protected void Page_Load(object sender, EventArgs e)
    {     
        // 在此处放置用户代码以初始化页面
      

        TranControls(Button2, new string[][] 
                        {
                            new string[] { "000096","返 回"}                             
                        }
         );
        bind();
    }
    public string roleName(int id)
    {
        string role = "";
        switch (id)
        {
            case 0: role = GetTran("000151", "管理员"); break;
            case 1: role = GetTran("000388", "店铺"); break;
            case 2: role = GetTran("000599", "会员"); break;
        }
        return role;
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.QueryString["source"].ToString());
    }
    public void bind()
    {
        string tableName = Request.QueryString["T"].ToString().ToUpper();
        int id = Convert.ToInt32(Request.QueryString["id"].ToString());
      
        SqlDataReader dr = bll.GetGongGao(tableName,id);
        string sqlid = "";

        if (dr.Read())
        {
            Text_Date.Text = DateTime.Parse(dr["Senddate"].ToString()).AddHours(Convert.ToDouble(Session["WTH"])).ToString();

            if (dr["Receive"].ToString().Trim() == "*")
            {
                string loginRole=dr["loginRole"].ToString();
                if (loginRole == "0         ")
                {
                    Text_Recive.Text = "管理员";
                }
                else if (loginRole == "1         ")
                {
                    Text_Recive.Text = "服务机构";
                }
                else if (loginRole == "2         ")
                {
                    Text_Recive.Text = "会员";
                }
            }
            else
                Text_Recive.Text = dr["Receive"].ToString();

            Text_Title.Text = dr["Infotitle"].ToString();
            Text_Send.Text = dr["sender"].ToString();
            DetailSpan.InnerHtml = dr["Content"].ToString();

            sqlid = dr["id"].ToString();
        }
        dr.Close();
        bll.UpdateIsRead(tableName,sqlid);
        bll.UpdateIsRead("MessageSend", sqlid);
    }
}
