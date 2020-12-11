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


public partial class Company_MessageCascade : BLL.TranslationBase
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
        if (Request.QueryString["id"] != null)
        {
            ViewState["MessageID"] = Request.QueryString["id"].ToString();
        }
        bind();
    }
    //public string roleName(int id)
    //{
    //    string role = "";
    //    switch (id)
    //    {
    //        case 0: role = GetTran("000151", "管理员"); break;
    //        case 1: role = GetTran("000388", "店铺"); break;
    //        case 2: role = GetTran("000599", "会员"); break;
    //    }
    //    return role;
    //}

    protected void Button2_Click(object sender, EventArgs e)
    {
        //ClientScript.RegisterStartupScript(this.GetType(), "", "<script>history.goback(-1);</script>");

        Response.Redirect("ManageMessage_Recive.aspx");
    }
    public void bind()
    {
        this.Table1.Rows.Clear();
        this.Table1.BorderWidth = 1;
        TableCell cell;
        TableRow row;
        
        cell = new TableCell();
        cell.ColumnSpan = 2;
        cell.Text = GetTran("008363", "相关邮件列表");
        cell.HorizontalAlign = HorizontalAlign.Center;
        cell.Style.Add(HtmlTextWriterStyle.BackgroundImage, "images/menudp.GIF");
        cell.ForeColor = System.Drawing.Color.White;
        cell.Font.Bold = true;
        row = new TableRow();
        row.Cells.Add(cell);
        this.Table1.Rows.Add(row);

        SqlDataReader dr = DAL.DBHelper.ExecuteReader("GetMessageCascade", new SqlParameter("@id", Convert.ToInt32(ViewState["MessageID"])), CommandType.StoredProcedure);

        while (dr.Read())
        {
            string SenderRole = "";
            string LoginRole = "";
            switch (dr["SenderRole"].ToString().Trim())
            {
                case "0": SenderRole = GetTran("000151", "管理员"); break;
                case "1": SenderRole = GetTran("000388", "服务机构"); break;
                case "2": SenderRole = GetTran("000599", "会员"); break;
            }
            switch (dr["LoginRole"].ToString().Trim())
            {
                case "0": LoginRole = GetTran("000151", "管理员"); break;
                case "1": LoginRole = GetTran("000388", "服务机构"); break;
                case "2": LoginRole = GetTran("000599", "会员"); break;
            }

            row = new TableRow();
            row.Style.Add(HtmlTextWriterStyle.BackgroundImage, "images/companycascade.gif");
            row.ForeColor = System.Drawing.Color.White;

            cell = new TableCell();
            cell.Text = dr["SendDate"].ToString();
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = GetTran("001721", "发件人") + ":" + dr["Sender"].ToString() + "(" + SenderRole + ")    " + GetTran("001684", "收件人") + ":" + dr["Receive"].ToString() + "(" + LoginRole + ")";
            row.Cells.Add(cell);

            this.Table1.Rows.Add(row);

            row = new TableRow();

            cell = new TableCell();
            cell.Font.Bold = true;
            cell.HorizontalAlign = HorizontalAlign.Right;
            cell.Text = GetTran("000781", "标题") + ":";
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = dr["InfoTitle"].ToString();
            row.Cells.Add(cell);

            this.Table1.Rows.Add(row);

            row = new TableRow();

            cell = new TableCell();
            cell.Font.Bold = true;
            cell.HorizontalAlign = HorizontalAlign.Right;
            cell.Text = GetTran("000013", "内容") + ":";
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = dr["Content"].ToString();
            row.Cells.Add(cell);

            this.Table1.Rows.Add(row);


        }
        dr.Close();
    }
}
