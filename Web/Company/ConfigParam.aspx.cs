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
using Model;
using Model.Other;
using System.Collections.Generic;
using System.Data.SqlClient;
using DAL;

public partial class Company_BauseParam : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(Model.Other.EnumCompanyPermission.FinanceJiesuanchanshu);
        if (!IsPostBack)
        {
            IList<ConfigModel> configs = CommonDataBLL.GetVolumeLists();
            ddlExpectNum.DataSource = configs;
            ddlExpectNum.DataTextField = "date";
            ddlExpectNum.DataValueField = "ExpectNum";
            ddlExpectNum.DataBind();
            ddlExpectNum.SelectedValue = CommonDataBLL.GetMaxqishu().ToString();
            InitPage();
        }
        ViewState["ExpectNum"] = Convert.ToInt32(ddlExpectNum.SelectedValue);
        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.btnEdit, new string[][] { new string[] { "000092", "修改" } });
    }
    private void InitPage()
    {
        ConfigModel model = ConfigSetBLL.GetConfig(Convert.ToInt32(ddlExpectNum.SelectedValue));
        if (model == null)
        {
            Response.Redirect("ConfigParam.aspx", true);
        }
        this.txtPara1.Text = model.Para1.ToString();
        this.txtPara2.Text = model.Para2.ToString();
        this.txtPara3.Text = model.Para3.ToString();
        this.txtPara4.Text = model.Para4.ToString();
        this.txtPara5.Text = model.Para5.ToString();
        this.txtPara6.Text = model.Para6.ToString();
        this.txtPara7.Text = model.Para7.ToString();
        this.txtPara8.Text = model.Para8.ToString();
        this.txtPara9.Text = model.Para9.ToString();
        this.txtPara10.Text = model.Para10.ToString();
        this.txtPara11.Text = model.Para11.ToString();
        this.txtPara12.Text = model.Para12.ToString();
        this.txtPara13.Text = model.Para13.ToString();
        this.txtPara14.Text = model.Para14.ToString();
        this.txtPara15.Text = model.Para15.ToString();
        this.txtPara16.Text = model.Para16.ToString();

        this.txtPara17.Text = model.Para17.ToString();
        this.txtPara18.Text = model.Para18.ToString();
        this.txtPara19.Text = model.Para19.ToString();
        this.txtPara19.Text = model.Para19.ToString();

        this.txtPara20.Text = model.Para20.ToString();
        this.txtPara21.Text = model.Para21.ToString();
        this.txtPara22.Text = model.Para22.ToString();
        this.txtPara23.Text = model.Para23.ToString();
        this.txtPara24.Text = model.Para24.ToString();
        this.txtPara25.Text = model.Para25.ToString();
        this.txtPara26.Text = model.Para26.ToString();
        this.txtPara27.Text = model.Para27.ToString();
        this.txtPara28.Text = model.Para28.ToString();
        this.txtPara29.Text = model.Para29.ToString();

        //SqlDataReader sdr = DAL.DBHelper.ExecuteReader("Select Top 1 * From jykz ORDER BY ID");
        //while (sdr.Read())
        //{
        //    this.txtPara20.Text = sdr["mrMinjf"].ToString().Trim();
        //    this.txtPara21.Text = sdr["mrMaxjf"].ToString();
        //    this.txtPara22.Text = sdr["mcMinjf"].ToString();
        //    this.txtPara23.Text = sdr["mcMaxjf"].ToString();
        //    this.txtPara24.Text = sdr["Mrzs"].ToString();
        //}
        //sdr.Close();
        //sdr.Dispose();
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ConfigModel model = null;
        //decimal mrmin=300;
        //decimal mrmax=30000;
        //decimal mcmin=300;
        //decimal mcmax=30000;
        //decimal mrzs = 0.5m;
        try
        {
            model = new ConfigModel();
            model.ExpectNum = (int)ViewState["ExpectNum"];
            model.Para1 = double.Parse(this.txtPara1.Text);
            model.Para2 = double.Parse(this.txtPara2.Text);
            model.Para3 = double.Parse(this.txtPara3.Text);
            model.Para4 = double.Parse(this.txtPara4.Text);
            model.Para5 = double.Parse(this.txtPara5.Text);
            model.Para6 = double.Parse(this.txtPara6.Text);
            model.Para7 = double.Parse(this.txtPara7.Text);
            model.Para8 = double.Parse(this.txtPara8.Text);
            model.Para9 = double.Parse(this.txtPara9.Text);
            model.Para10 = double.Parse(this.txtPara10.Text);
            model.Para11 = double.Parse(this.txtPara11.Text);
            model.Para12 = double.Parse(this.txtPara12.Text);
            model.Para13 = double.Parse(this.txtPara13.Text);
            model.Para14 = double.Parse(this.txtPara14.Text);
            model.Para15 = double.Parse(this.txtPara15.Text);
            model.Para16 = double.Parse(this.txtPara16.Text);

            model.Para17= double.Parse(this.txtPara17.Text);
            model.Para18 = double.Parse(this.txtPara18.Text);
            model.Para19 = double.Parse(this.txtPara19.Text);

            model.Para20 = double.Parse(this.txtPara20.Text);
            model.Para21 = double.Parse(this.txtPara21.Text);
            model.Para22 = double.Parse(this.txtPara22.Text);
            model.Para23 = double.Parse(this.txtPara23.Text);
            model.Para24 = double.Parse(this.txtPara24.Text);
            model.Para25 = double.Parse(this.txtPara25.Text);
            model.Para26 = double.Parse(this.txtPara26.Text);
            model.Para27 = double.Parse(this.txtPara27.Text);
            model.Para28 = double.Parse(this.txtPara28.Text);
            model.Para29 = double.Parse(this.txtPara29.Text);

        }
        catch (FormatException)
        {
            ScriptHelper.SetAlert(Page, GetTran("000969", "请输入数值"));
            InitPage();
            return;
        }
        if (CheckNum(model.Para1) && CheckNum(model.Para2) && CheckNum(model.Para3) && CheckNum(model.Para4) && CheckNum(model.Para5) 
            && CheckNum(model.Para6) && CheckNum(model.Para7) && CheckNum(model.Para8) && CheckNum(model.Para9) && CheckNum(model.Para10) 
            && CheckNum(model.Para11) && CheckNum(model.Para12) && CheckNum(model.Para13) && CheckNum(model.Para14) && CheckNum(model.Para15)
            && CheckNum(model.Para16) && CheckNum(model.Para17) && CheckNum(model.Para18) && CheckNum(model.Para19))
        {
        }
        else
        {
            ScriptHelper.SetAlert(Page, GetTran("001274", "请输入正数"));
            InitPage();
            return;
        }
        ChangeLogs cl = new ChangeLogs("config", "ExpectNum");
        cl.AddRecord((int)ViewState["ExpectNum"]);
        int n = ConfigSetBLL.UpdateConfig(model);
        //SqlParameter[] paras = new SqlParameter[]{
        //      new SqlParameter("@Para1",mrmin),
        //      new SqlParameter("@Para2",mrmax),
        //      new SqlParameter("@Para3",mcmin),
        //      new SqlParameter("@Para4",mcmax),
        //      new SqlParameter("@Para5",mrzs),
        //    };
        //string sql = @"UPDATE jykz SET mrMinjf =@Para1,mrMaxjf=@Para2,mcMinjf=@Para3,mcMaxjf=@Para4,Mrzs=@Para5";
        //int m = DBHelper.ExecuteNonQuery(sql, paras, CommandType.Text);
        if (n == 1  )
        {
            cl.ModifiedIntoLogs(ChangeCategory.company15, GetTran("000156", "第") + model.ExpectNum.ToString() + GetTran("000157", "期"), ENUM_USERTYPE.objecttype3);
            ScriptHelper.SetAlert(Page, GetTran("000001", "修改成功"));
        }
        else
        {
            ScriptHelper.SetAlert(Page, GetTran("000002", "修改失败") + "。");
        }
        InitPage();
    }

    protected bool CheckNum(double i)
    {
        return i >= 0;
    }

    protected void ddlExpectNum_SelectedIndexChanged(object sender, EventArgs e)
    {
        InitPage();
    }
}