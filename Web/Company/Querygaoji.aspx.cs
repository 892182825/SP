using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Xml.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using BLL.CommonClass;
using System.Text;
using Standard.Classes;
using BLL.Logistics;


public partial class Company_Querygaoji : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        this.DDLtable.Attributes.Add("onchange", "caxuaa()");
        Translations();
        if (!IsPostBack)
        {
            querytabe();
            queryproduct();
            string sqlstr = "select id,sqlname from querycaxun";
            SqlDataReader dr1 = DAL.DBHelper.ExecuteReader(sqlstr);
            while (dr1.Read())
            {
                ListItem list1 = new ListItem(dr1["sqlname"].ToString(), dr1["id"].ToString());
                DDLbaocunxiang.Items.Add(list1);
            }
            dr1.Close();
        }

    }

    private void Translations()
    {
        this.BtnCheckAll.Text=GetTran("000340", "查询");
       this.BtnCancelAll.Text=GetTran("007601", "用保存项查询" );
        this.TranControls(this.Btnview, new string[][] { new string[] { "007602", "查询结果" } });
        this.Addtable.Text = GetTran("007605", "增加");
        this.Deltable.Text = GetTran("000022", "删除");

        this.TranControls(this.DDLgltj, new string[][] { new string[] { "007609", "内连接" } ,
            new string[]{"007610","左连接"},
            new string[]{"007611","右连接"}
        }  );
        this.Addtable0.Text = GetTran("007605", "增加");
        this.Deltable0.Text = GetTran("000022", "删除");

        this.TranControls(this.DDLtjfu2, new string[][] { new string[] { "000851", "包含" } ,
            new string[]{"000853","不包含"}
        });

        this.Btntiaojian1.Text = GetTran("000719", "并且");
        this.Btntiaojian2.Text = GetTran("007557", "或者");
        this.Btntiaojian5.Text = GetTran("007614", "增加子条件");
        this.Button6.Text = GetTran("007615", "删除当前条件");


        this.BtnpaixuAdd.Text = GetTran("007605", "增加");
        this.Btnpaixudel.Text = GetTran("000022", "删除");
        this.RBLpaixu.Items[0].Text = GetTran("007621", "不排序");
        this.RBLpaixu.Items[1].Text = GetTran("007623", "升序");
        this.RBLpaixu.Items[2].Text = GetTran("007624", "降序");

        this.Btnback.Text = GetTran("007632", "返回查询条件设置页");
        this.savecaxun.Text = GetTran("007633", "保存查询条件");
        this.Btncaxun.Text = GetTran("000340", "查询"); 
       

    }

    //绑定商品名称
    private void queryproduct()
    {
        string sql = "select productid,productname from product where isfold=0 order by productcode";
        DataTable dt = DAL.DBHelper.ExecuteDataTable(sql);
        DDLproductname.DataSource = dt;
        DDLproductname.DataTextField = "productname";
        DDLproductname.DataValueField = "productid";
        DDLproductname.DataBind();
        DDLproductname.Items.Add(new ListItem(GetTran("000633", "全部"), "-1"));
    }
    //绑定要查询表
    private void querytabe()
    {
        string sql = "select tableen,tablecn from QuerTable";
        DataTable dt = DAL.DBHelper.ExecuteDataTable(sql);

        DDLtable.DataSource = dt;
        DDLtable.DataTextField = "tablecn";
        DDLtable.DataValueField = "tableen";
        DDLtable.DataBind();
    }
    protected void BtnCancelAll_Click(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        Panel2.Visible = true;
        savecaxun.Visible = false;
        Txtsavecx.Visible = false;

    }
   
    protected void BtnCheckAll_Click(object sender, EventArgs e)
    {
        Btnback.Visible = true ;
        savecaxun.Visible = true;
        Txtsavecx.Visible = true;
        GridView1.Visible = true;
        GridView2.Visible = false;
        bind();
    }

    //生成查询的sql语句
    private void Querysql(string[] sqlstr1)
    {
        string sqlstr = "";
        string columns1 = "";       //字段列表
        string columnsname = "";       //查询的字段名
        string tablename = "";         //查询的表名
        string Condition = "1=1 "; //查询的条件
        string paixu = "";

        string sqlstrcn = "";
        string columns1cn = "";       //字段列表显示用
        string tablenamecn = "";         //查询的表名显示用
        string Conditioncn = " "; //查询的条件显示用
        string paixucn = "";

        if (Txttablecn.Text.Trim() != "")
        {
            //查询的表名
            int b1 = Txttablecn.Text.Trim().IndexOf(",");
            if (Txttablecn.Text.Trim().IndexOf(",") == -1)
            {
                tablename = Txttableen.Text.Trim();
                tablenamecn = Txttablecn.Text.Trim();
            }
            else
            {
                tablename = Txtgltj.Text.Trim();
                tablenamecn = Txttablecn.Text.Trim();
            }
            //显示的字段
            if (ListBoxcn2.Items.Count != 0)
            {
                int i = 0;
                foreach (ListItem li in ListBoxcn2.Items)
                {
                    i++;
                    if (i == 1)
                    {
                        columns1 = "'" + li.Value.ToString() + "'";
                        columns1cn = li.Text.ToString() + " ";
                    }
                    else
                    {
                        columns1 += ",'" + li.Value.ToString() + "'";
                        columns1cn += "," + li.Text.ToString() + "";
                    }
                }
                sqlstr = "select case sqlint when 1 then columnsen+ ' as '+columnscn  else tablename+'.'+columnsen + ' as '+columnscn end  as columnsen from querycolumn where tablecolumn in (" + columns1 + ") order by id ";
                SqlDataReader dr1 = DAL.DBHelper.ExecuteReader(sqlstr);
                i = 0;
                while (dr1.Read())
                {
                    i++;
                    if (i == 1)
                    {
                        columnsname = dr1["columnsen"].ToString();
                    }
                    else
                    {
                        columnsname += "," + dr1["columnsen"].ToString();
                    }

                }
                dr1.Close();


                //查询条件
                if (Txttjcn.Text.Trim() != "")
                {
                    Condition += " and " + Txttjen.Text.Trim();
                    Conditioncn += Txttjcn.Text.Trim();
                }
                //排序方式
                if (Txtpaixucn.Text.Trim() != "")
                {
                    paixu = " order by " + Txtpaixuen.Text.Trim() + " " + RBLpaixu.SelectedValue.ToString();
                    paixucn = " "+ GetTran("007638"," 排序字段")+"   " + Txtpaixucn.Text.Trim() + " " + RBLpaixu.SelectedItem.Text.ToString();
                }
            }
            else
            {
                sqlstr1[0] = "1";
            }
        }
        else
        {
            sqlstr1[0] = "2"; ;
        }
        sqlstr = "select " + columnsname + " from " + tablename + " where " + Condition + paixu + " ";
        sqlstrcn = GetTran("007641", "显示的字段") + "《 " + columns1cn + "》 " + GetTran("007642", "查询表 ") + "《" + tablenamecn + "》 " + GetTran("001819", "条件 ") + "《" + Conditioncn + paixucn + " 》";
        sqlstr1[0] = columnsname;
        sqlstr1[1] = tablename;
        sqlstr1[2] = Condition;
        sqlstr1[3] = paixu;
        sqlstr1[4] = sqlstr;
        sqlstr1[5] = sqlstrcn;
    }

    protected void Addtable_Click(object sender, EventArgs e)
    {
        int ctype = Convert.ToInt32(DAL.DBHelper.ExecuteScalar("select tabletype from QuerTable where tableen='" + DDLtable.SelectedValue.ToString() + "'"));
        if (ctype == 1)
        {
            Txttablecn.Text = DDLtable.SelectedItem.Text.Trim();
            Txttableen.Text = DDLtable.SelectedValue.ToString();
        }
        else
        {
            if (Txttablecn.Text.Trim() != "")
            {
                Txttablecn.Text = Txttablecn.Text.Trim() + "," + DDLtable.SelectedItem.Text.Trim();
            }
            else
            {
                Txttablecn.Text = DDLtable.SelectedItem.Text.Trim();
            }
            if (Txttableen.Text.Trim() != "")
            {
                Txttableen.Text = Txttableen.Text.Trim() + "," + DDLtable.SelectedValue.ToString();
            }
            else
            {
                Txttableen.Text = DDLtable.SelectedValue.ToString();
            }
            querycolumnsguanlian();
        }

        //将选中表的字段写入字段列表框
        string sql = "select (tablenamecn+'.'+columnscn) as columnscn,case sqlint when 1 then columnsen  else tablename+'.'+columnsen end  as columnsen from querycolumn where  ishidden=0 and tablename='" + DDLtable.SelectedValue.ToString() + "'";
        SqlDataReader dr1 = DAL.DBHelper.ExecuteReader(sql);
        while (dr1.Read())
        {
            ListItem list1 = new ListItem(dr1["columnscn"].ToString(), dr1["columnsen"].ToString());
            ListBoxcn1.Items.Add(list1);
        }
        dr1.Close();

    }
    //绑定关联字段
    private void querycolumnsguanlian()
    {
        string sql = "select (tablenamecn+'.'+columnscn) as columnscn,(tablename+'.'+columnsen) as columnsen from querycolumn where  relevancetype=1 and tablename='" + DDLtable.SelectedValue.ToString() + "'";
        SqlDataReader dr1 = DAL.DBHelper.ExecuteReader(sql);
        while (dr1.Read())
        {
            ListItem list1 = new ListItem(dr1["columnscn"].ToString(), dr1["columnsen"].ToString());
            DDLuoncol.Items.Add(list1);
            DDLuoncol2.Items.Add(list1);

        }
        dr1.Close();

    }
    protected void Addtable0_Click(object sender, EventArgs e)
    {
        if (DDLuoncol.Items.Count != 0)
        {
            if (DDLuoncol.SelectedValue.ToString().Substring(0, DDLuoncol.SelectedValue.ToString().IndexOf(".")) != DDLuoncol2.SelectedValue.ToString().Substring(0, DDLuoncol2.SelectedValue.ToString().IndexOf(".")))
            {
                if (Txtglcn.Text.Trim() == "")
                {
                    Txtglcn.Text = DDLuoncol.SelectedItem.Text.Trim() + "=" + DDLuoncol2.SelectedItem.Text.Trim();
                    Txtglen.Text = DDLuoncol.SelectedValue.ToString() + "=" + DDLuoncol2.SelectedValue.ToString();
                }
                else
                {
                    Txtglcn.Text += " , " + DDLuoncol.SelectedItem.Text.Trim() + "=" + DDLuoncol2.SelectedItem.Text.Trim();
                    Txtglen.Text += " , " + DDLuoncol.SelectedValue.ToString() + "=" + DDLuoncol2.SelectedValue.ToString();
                }

                if (Txtgltj.Text.Trim() == "")
                {
                    Txtgltj.Text = DDLuoncol.SelectedValue.ToString().Substring(0, DDLuoncol.SelectedValue.ToString().IndexOf(".")) + " " + DDLgltj.SelectedValue + " " + DDLuoncol2.SelectedValue.ToString().Substring(0, DDLuoncol2.SelectedValue.ToString().IndexOf(".")) + " ON " + Txtglen.Text;
                }
                else
                {
                    Txtgltj.Text += " " + DDLgltj.SelectedValue + " " + DDLuoncol2.SelectedValue.ToString().Substring(0, DDLuoncol2.SelectedValue.ToString().IndexOf(".")) + " ON " + DDLuoncol.SelectedValue.ToString() + "=" + DDLuoncol2.SelectedValue.ToString();
                }
            }
            else
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("007643", "相同表不能关联") + "');", true);
                return;
            }
        }
        else
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("007644", "请先选择查询的表！") + "');", true);
            return;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (ListBoxcn1.Items.Count != 0)
        {
            foreach (ListItem li in ListBoxcn1.Items)
            {
                ListBoxcn2.Items.Add(li);
                DDLtj.Items.Add(li);
                DDLpaixu.Items.Add(li);
            }
            ListBoxcn1.Items.Clear();
        }
        else
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("007644", "请先选择查询的表！") + "');", true);
            return;
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (ListBoxcn1.Items.Count != 0)
        {
            if (ListBoxcn1.SelectedIndex != -1)
            {
                string aa = ListBoxcn1.SelectedItem.Text;
                string bb = ListBoxcn1.SelectedValue.ToString();
                ListItem list1 = new ListItem(aa, bb);
                ListItem list2 = new ListItem(bb, aa);
                ListBoxcn2.Items.Add(list1);
                ListBoxcn1.Items.Remove(list1);
                DDLtj.Items.Add(list1);
                DDLpaixu.Items.Add(list1);
            }
            else
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("007646", "请选定要显示的列表项！") + "');", true);
                return;
            }
        }
        else
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("007644", "请先选择查询的表！") + "');", true);
            return;
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        if (ListBoxcn2.Items.Count != 0)
        {
            if (ListBoxcn2.SelectedIndex != -1)
            {
                string aa = ListBoxcn2.SelectedItem.Text;
                string bb = ListBoxcn2.SelectedValue.ToString();
                ListItem list1 = new ListItem(aa, bb);
                // ListItem list2 = new ListItem(bb, aa);
                ListBoxcn1.Items.Add(list1);
                ListBoxcn2.Items.Remove(list1);
                DDLtj.Items.Remove(list1);
                DDLpaixu.Items.Remove(list1);
            }
            else
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("007647", "请先选定要移出的列表项！") + "');", true);
                return;
            }
        }
        else
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("007649", "没有可移出的列表项！") + "');", true);
            return;
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        if (ListBoxcn2.Items.Count != 0)
        {
            foreach (ListItem li in ListBoxcn2.Items)
            {
                ListBoxcn1.Items.Add(li);
            }
            ListBoxcn2.Items.Clear();
            DDLtj.Items.Clear();
            DDLpaixu.Items.Clear();
        }
        else
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("007649", "没有可移出的列表项！") + "');", true);
            return;
        }

    }
    protected void Btntiaojian5_Click(object sender, EventArgs e)
    {
        if (DDLtj.Items.Count != 0)
        {

            string ctype = Convert.ToString(DAL.DBHelper.ExecuteScalar("select columnstype from querycolumn where tablecolumn='" + DDLtj.SelectedValue + "'"));
            switch (ctype)
            {
                case "C": //字符型
                    if (Txttjvalue.Text.Trim() != "")
                    {

                        string aa = DDLtj.SelectedValue.ToString() + ' ' + DDLtjfu2.SelectedValue.ToString() + "  '%" + Txttjvalue.Text.Trim() + "%'";
                        string aa1 = DDLtj.SelectedItem.Text.Trim() + ' ' + DDLtjfu2.SelectedItem.Text.Trim() + "  " + Txttjvalue.Text.Trim() + " ";
                        Txttjcn.Text += aa1;
                        Txttjen.Text += aa;
                        Btntiaojian1.Enabled = true;
                        Btntiaojian2.Enabled = true;
                        Txttjvalue.Text = "";
                    }
                    else
                    {
                        System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("007651", "请输入条件值！") + "');", true);
                        return;
                    }
                    break;
                case "I": //整型或货币或单、双精度型
                    if (Txttjvalue.Text.Trim() != "")
                    {

                        string aa = DDLtj.SelectedValue.ToString() + ' ' + DDLtjfu1.SelectedValue.ToString() + "  " + Txttjvalue.Text.Trim();
                        string aa1 = DDLtj.SelectedItem.Text.Trim() + ' ' + DDLtjfu1.SelectedItem.Text.Trim() + "  " + Txttjvalue.Text.Trim();
                        Txttjcn.Text += aa1;
                        Txttjen.Text += aa;
                        Btntiaojian1.Enabled = true;
                        Btntiaojian2.Enabled = true;
                        Txttjvalue.Text = "";
                    }
                    else
                    {
                        System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("007651", "请输入条件值！") + "');", true);
                        return;
                    }
                    break;
                case "D": //日期型
                    if (DatePicker1.Text.Trim() != "")
                    {

                        string aa = DDLtj.SelectedValue.ToString() + ' ' + DDLtjfu1.SelectedValue.ToString() + "  '" + DatePicker1.Text.Trim() + "'";
                        string aa1 = DDLtj.SelectedItem.Text.Trim() + ' ' + DDLtjfu1.SelectedItem.Text.Trim() + "  '" + DatePicker1.Text.Trim() + "'";
                        Txttjcn.Text += aa1;
                        Txttjen.Text += aa;
                        Btntiaojian1.Enabled = true;
                        Btntiaojian2.Enabled = true;
                        DatePicker1.Text = "";
                    }
                    else
                    {
                        System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("007651", "请输入条件值！") + "');", true);
                        return;
                    }
                    break;
                case "B": //布尔型
                    if (Txttjvalue.Text.Trim() != "")
                    {

                        string aa = DDLtj.SelectedValue.ToString() + ' ' + DDLtjfu1.SelectedValue.ToString() + "  " + Txttjvalue.Text.Trim();
                        string aa1 = DDLtj.SelectedItem.Text.Trim() + ' ' + DDLtjfu1.SelectedItem.Text.Trim() + "  " + Txttjvalue.Text.Trim();
                        Txttjcn.Text += aa1;
                        Txttjen.Text += aa;
                        Btntiaojian1.Enabled = true;
                        Btntiaojian2.Enabled = true;
                        Txttjvalue.Text = "";
                    }
                    else
                    {
                        System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("007651", "请输入条件值！") + "');", true);
                        return;
                    }
                    break;
            }
   
        }
        else
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("007646", "请选定要显示的列表项！") + "');", true);
            return;
        }
    }
    protected void Btntiaojian1_Click(object sender, EventArgs e)
    {
        if (Txttjcn.Text.Trim() != "")
        {
            Txttjcn.Text = Txttjcn.Text.Trim() + "  " + GetTran("000719", "并且");
            Txttjen.Text = Txttjen.Text.Trim() + " and  ";
            Btntiaojian1.Enabled = false;
            Btntiaojian2.Enabled = false;
        }

    }
    protected void Btntiaojian2_Click(object sender, EventArgs e)
    {
        if (Txttjcn.Text.Trim() != "")
        {
            Txttjcn.Text = Txttjcn.Text.Trim() + "  " + GetTran("007557", "或者");
            Txttjen.Text = Txttjen.Text.Trim() + " or ";
            Btntiaojian1.Enabled = false;
            Btntiaojian2.Enabled = false;
        }

    }
    protected void Btntiaojian3_Click(object sender, EventArgs e)
    {
        Txttjcn.Text = Txttjcn.Text.Trim() + " (";
        Txttjen.Text = Txttjen.Text.Trim() + " (";
        Btntiaojian1.Enabled = false;
        Btntiaojian2.Enabled = false;

    }
    protected void Btntiaojian4_Click(object sender, EventArgs e)
    {
        if (Txttjcn.Text.Trim() != "")
        {
            Txttjcn.Text = Txttjcn.Text.Trim() + " )";
            Txttjen.Text = Txttjen.Text.Trim() + " )";
            Btntiaojian1.Enabled = true;
            Btntiaojian2.Enabled = true;
        }
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        Txttjcn.Text = "";
        Txttjen.Text = "";
        Btntiaojian1.Enabled = false;
        Btntiaojian2.Enabled = false;
    }
    protected void BtnpaixuAdd_Click(object sender, EventArgs e)
    {
        if (DDLpaixu.Items.Count != 0)
        {
            if (Txtpaixucn.Text.Trim() == "")
            {
                Txtpaixucn.Text = DDLpaixu.SelectedItem.Text.Trim();
                Txtpaixuen.Text = DDLpaixu.SelectedValue.ToString();
            }
            else
            {
                Txtpaixucn.Text = Txtpaixucn.Text.Trim() + "," + DDLpaixu.SelectedItem.Text.Trim();
                Txtpaixuen.Text = Txtpaixuen.Text.Trim() + "," + DDLpaixu.SelectedValue.ToString();
            }
        }
        else
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("007646", "请选定要显示的列表项！") + "');", true);
            return;
        }
    }
    protected void Deltable_Click(object sender, EventArgs e)
    {
        Txttablecn.Text = "";
        Txttableen.Text = "";
        DDLuoncol.Items.Clear();
        DDLuoncol2.Items.Clear();
        ListBoxcn1.Items.Clear();
        ListBoxcn2.Items.Clear();
        DDLtj.Items.Clear();
        DDLpaixu.Items.Clear();

    }
    protected void Deltable0_Click(object sender, EventArgs e)
    {
        Txtglcn.Text = "";
        Txtglen.Text = "";
        Txtgltj.Text = "";
    }
    protected void Btnpaixudel_Click(object sender, EventArgs e)
    {
        Txtpaixucn.Text = "";
        Txtpaixuen.Text = "";
    }
    protected void Btnback_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        Panel2.Visible = false;
    }
    protected void Btnview_Click(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        Panel2.Visible = true;
    }
    protected void DDLtj_SelectedIndexChanged(object sender, EventArgs e)
    {
        string ctype = Convert.ToString(DAL.DBHelper.ExecuteScalar("select columnstype from querycolumn where tablecolumn='" + DDLtj.SelectedValue + "'"));
        switch (ctype)
        {
            case "C": //字符型
                DDLtjfu2.Visible = true;
                DDLtjfu1.Visible = false;
                Txttjvalue.Visible = true;
                Txttjvalue.MaxLength = 200;
                DatePicker1.Visible = false;
                Txttjvalue.Text = "";
                DatePicker1.Text = "";
                break;
            case "I": //整型或货币或单、双精度型
                DDLtjfu2.Visible = false;
                DDLtjfu1.Visible = true;
                DatePicker1.Visible = false;
                Txttjvalue.Visible = true;
                Txttjvalue.MaxLength = 9;
                Txttjvalue.Text = "";
                DatePicker1.Text = "";
                break;
            case "D": //日期型
                DDLtjfu2.Visible = false;
                DDLtjfu1.Visible = true;
                DatePicker1.Visible = true;
                Txttjvalue.Visible = false;
                Txttjvalue.Text = "";
                DatePicker1.Text = "";
                break;
            case "B": //布尔型
                DDLtjfu2.Visible = false;
                DDLtjfu1.Visible = true;
                DatePicker1.Visible = false;
                Txttjvalue.Visible = true;
                Txttjvalue.MaxLength = 1;
                Txttjvalue.Text = "";
                DatePicker1.Text = "";
                break;

        }

    }
    protected void savecaxun_Click(object sender, EventArgs e)
    {
          if (Txtsavecx.Text.Trim() != "")
        {
            int ttype = Convert.ToInt32(DAL.DBHelper.ExecuteScalar("select tabletype from QuerTable where tableen='" + Txttableen.Text.Trim() + "'"));
            string inssql = "insert into querycaxun(sqlname,tablename,columns,condition,paixu,sqlstren,sqlstrcn,tabletype) values (@sqlname,@tablename,@columns,@condition,@paixu,@sqlstren,@sqlstrcn,@tabletype)";
            int ctype = Convert.ToInt32(DAL.DBHelper.ExecuteScalar("select count(*) from querycaxun where sqlname='" + Txtcaxun.Text.Trim() + "'"));
            if (ctype == 0)
            {
                string[] arsql = new string[6];
                Querysql(arsql);
                string columns = arsql[0];
                string tablename = arsql[1];
                string Condition = arsql[2];
                string paixu = arsql[3];
                string sqlstr = arsql[4];
                string sqlstrcn = arsql[5];

                SqlParameter[] para = new SqlParameter[] {
                       new SqlParameter("@sqlname", SqlDbType.NVarChar,100),
                       new SqlParameter("@tablename", SqlDbType.NVarChar,500),
                       new SqlParameter("@columns", SqlDbType.NVarChar,2000),
                       new SqlParameter("@condition", SqlDbType.NVarChar,500),
                       new SqlParameter("@paixu", SqlDbType.NVarChar,300),
                       new SqlParameter("@sqlstren", SqlDbType.NVarChar,4000),
                       new SqlParameter("@sqlstrcn", SqlDbType.NVarChar,4000),
                       new SqlParameter("@tabletype", SqlDbType.Int )
                        };
                para[0].Value = Txtsavecx.Text.Trim();
                para[1].Value = tablename.Replace("'", "‘");
                para[2].Value = columns.Replace("'", "‘");
                para[3].Value = Condition.Replace("'", "‘");
                para[4].Value = paixu.Replace("'", "‘");
                para[5].Value = sqlstr.Replace("'", "‘");
                para[6].Value = sqlstrcn.Replace("'", "‘");
                para[7].Value = ttype;
                try
                {
                    DAL.DBHelper.ExecuteNonQuery(inssql, para, CommandType.Text);
                    DDLbaocunxiang.Items.Clear();
                    string sqlstr1 = "select id,sqlname from querycaxun";
                    SqlDataReader dr1 = DAL.DBHelper.ExecuteReader(sqlstr1);
                    while (dr1.Read())
                    {
                        ListItem list1 = new ListItem(dr1["sqlname"].ToString(), dr1["id"].ToString());
                        DDLbaocunxiang.Items.Add(list1);
                    }
                    dr1.Close();
                    System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("001194", "保存成功！") + "');", true);
                }
                catch (Exception ex1)
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("006591", "保存失败！") + "');", true);
                    return;
                }
            }
            else
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("007652", "查询条件的名称重复！") + "');", true);
                return;
            }
        }
        else
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("007653", "请输入查询条件的名称！") + "');", true);
            return;
        }
    }
    protected void Btncaxun_Click(object sender, EventArgs e)
    {
        GridView1.Visible = false;
        GridView2.Visible = true;
        Btnback.Visible = false;
        savecaxun.Visible = false;
        Txtsavecx.Visible = false;
        bind1();
    }
    private void bind1()
    {
        int ctype = Convert.ToInt32(DAL.DBHelper.ExecuteScalar("select tabletype from querycaxun where id=" + DDLbaocunxiang.SelectedValue.ToString()));
        string colchar = "";
        string char1 = "";
        string prde = "";//存储过程名称
        string sql = "";
        string[] canshu = new string[7];
        canshu[0] = area.CountryName;
        canshu[1] = Date1.Text;
        canshu[2] = Date2.Text;
        canshu[3] = DDLyear.SelectedItem.Text.Trim();
        canshu[4] = DDLmonth.SelectedItem.Text.Trim();
        canshu[5] = Txtstoreid.Text.Trim();
        canshu[6] = DDLproductname.SelectedValue.ToString();
        if (Txtstoreid.Text.Trim() == "")
        {
            canshu[5] = "-1";
        }
        string canshu1 = "";
        string tb = "";
        int num = 0;
        bool flagtype = true;
        if (ctype == 1)
        {
            tb = Convert.ToString(DAL.DBHelper.ExecuteScalar("select tablename from querycaxun where id=" + DDLbaocunxiang.SelectedValue.ToString()));
            prde = Convert.ToString(DAL.DBHelper.ExecuteScalar("select procedurename from QuerTable where tableen='" + tb.ToString() + "'"));
            colchar = Convert.ToString(DAL.DBHelper.ExecuteScalar("select procedurearg from QuerTable where tableen='" + tb.ToString() + "'"));
            for (int i = 0; i < colchar.Length; i++)
            {
                if (colchar[i] == ',')
                {
                    num = Convert.ToInt32(char1);
                    canshu1 += "'" + canshu[num] + "',";
                    char1 = "";
                    if (canshu[num] == "")
                    {
                        flagtype = false;
                    }
                }
                else
                {
                    char1 += colchar[i];
                }

            }
            num = Convert.ToInt32(char1);
            canshu1 += "'" + canshu[num] + "'";
            if (canshu[num] == "")
            {
                flagtype = false;
            }
            if (flagtype == false)
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("007654", "请选择查询区间！") + "');", true);
                return;
            }
            sql = "exec " + prde + " " + canshu1;
            DAL.DBHelper.ExecuteNonQuery(sql);
        }

        string tablename = "";
        string columns = "";
        string Condition = "";
        string paixu = "";
        string sqlstren = "";
        string sqlstrcn = "";
        string sqlstrcx = "select tablename,columns,condition,paixu,sqlstren,sqlstrcn from querycaxun where id=" + DDLbaocunxiang.SelectedValue.ToString();
        SqlDataReader dr1 = DAL.DBHelper.ExecuteReader(sqlstrcx);
        while (dr1.Read())
        {
            tablename = dr1["tablename"].ToString();
            columns = dr1["columns"].ToString();
            Condition = dr1["condition"].ToString();
            paixu = dr1["paixu"].ToString();
            sqlstren = dr1["sqlstren"].ToString();
            sqlstrcn = dr1["sqlstrcn"].ToString();
        }
        dr1.Close();
        columns = columns.Replace("MemberInfoBalance", "MemberInfoBalance" + DropDownQiShu.ExpectNum);
        tablename = tablename.Replace("MemberInfoBalance", "MemberInfoBalance" + DropDownQiShu.ExpectNum);
        Condition = Condition.Replace("MemberInfoBalance", "MemberInfoBalance" + DropDownQiShu.ExpectNum);
        paixu = paixu.Replace("MemberInfoBalance", "MemberInfoBalance" + DropDownQiShu.ExpectNum);
        sqlstren = sqlstren.Replace("MemberInfoBalance", "MemberInfoBalance" + DropDownQiShu.ExpectNum);
        Btnview.Visible = true;
        Txtcaxun.Text = sqlstrcn;
        columns = columns.Replace("‘", "'");
        tablename = tablename.Replace("‘", "'");
        Condition = Condition.Replace("‘", "'");
        paixu = paixu.Replace("‘", "'");
        sqlstren = sqlstren.Replace("‘", "'");

        ////查询的

        //ViewState["SQLSTR"] = sqlstren;
        //string asg = ViewState["SQLSTR"].ToString();
        //Pager pager = Page.FindControl("Pager1") as Pager;
        //pager.Pageindex = 0;
        //pager.PageSize = 10;
        //pager.PageTable = tablename;
        //pager.Condition = Condition;
        //pager.PageColumn = columns;
        //pager.ControlName = "GridView2";
        //pager.key = " id  ";
        //pager.InitBindData = true;
        //pager.PageBind();
        DataTable dt = DAL.DBHelper.ExecuteDataTable(sqlstren);
        if (dt.Rows.Count > 0)
        {
            GridView2.DataSource = dt;
            GridView2.DataBind();
        }
        else
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("007655", "无此记录！") + "');", true);
            return;
        }
    }
 
    private void bind( )
    {
        int ctype = Convert.ToInt32(DAL.DBHelper.ExecuteScalar("select tabletype from QuerTable where tableen='" + Txttableen.Text.Trim() + "'"));
        string colchar = "";
        string char1 = "";
        string prde = "";//存储过程名称
        string sql = "";
        string[] canshu = new string[7];
        canshu[0] = area.CountryName;
        canshu[1] = Date1.Text;
        canshu[2] = Date2.Text;
        canshu[3] = DDLyear.SelectedItem.Text.Trim();
        canshu[4] = DDLmonth.SelectedItem.Text.Trim();
        canshu[5] = Txtstoreid.Text.Trim();
        canshu[6] = DDLproductname.SelectedValue.ToString();
        if (Txtstoreid.Text.Trim() == "")
        {
            canshu[5] = "-1";
        }
        bool flagtype = true;
        string canshu1 = "";
        int num = 0;
        if (ctype == 1)
        {
            prde = Convert.ToString(DAL.DBHelper.ExecuteScalar("select procedurename from QuerTable where tableen='" + Txttableen.Text.Trim() + "'"));
            colchar = Convert.ToString(DAL.DBHelper.ExecuteScalar("select procedurearg from QuerTable where tableen='" + Txttableen.Text.Trim() + "'"));
            for (int i = 0; i < colchar.Length; i++)
            {
                if (colchar[i] == ',')
                {
                    num = Convert.ToInt32(char1);
                    canshu1 += "'" + canshu[num] + "',";
                    char1 = "";
                    if (canshu[num] == "")
                    {
                        flagtype = false;
                    }
                }
                else
                {
                    char1 += colchar[i];
                }

            }
            if (char1 != "")
            {
                num = Convert.ToInt32(char1);
                canshu1 += "'" + canshu[num] + "'";
            }

            if (canshu[num] == "")
            {
                flagtype = false;
            }
            if (flagtype == false)
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("007654", "请选择查询区间！") + "');", true);
                return;
            }
            sql = "exec " + prde + " " + canshu1;
            DAL.DBHelper.ExecuteNonQuery(sql);
        }


        string[] arsql = new string[6];
        Querysql(arsql);
        string columns = arsql[0];
        string tablename = arsql[1];
        string Condition = arsql[2];
        string paixu = arsql[3];
        string sqlstr = arsql[4];
        string sqlstrcn = arsql[5];
        if (columns != "")
        {
            if (columns == "1")
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("007656", "请选择要查询的字段！") + "');", true);
                return;
            }
            else if (columns == "2")
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("007644", "请先选择查询的表！") + "');", true);
                return;
            }
            else
            {
                if (tablename.IndexOf("MemberInfoBalance") != -1)
                {
                    sqlstrcn +=GetTran("007664","  您查询的是:")+ DropDownQiShu.ExpectNum;
                }
                columns = columns.Replace("MemberInfoBalance", "MemberInfoBalance" + DropDownQiShu.ExpectNum);
                tablename = tablename.Replace("MemberInfoBalance", "MemberInfoBalance" + DropDownQiShu.ExpectNum);
                Condition = Condition.Replace("MemberInfoBalance", "MemberInfoBalance" + DropDownQiShu.ExpectNum);
                paixu = paixu.Replace("MemberInfoBalance", "MemberInfoBalance" + DropDownQiShu.ExpectNum);
                sqlstr = sqlstr.Replace("MemberInfoBalance", "MemberInfoBalance" + DropDownQiShu.ExpectNum);
                Panel1.Visible = false;
                Panel2.Visible = true;
                savecaxun.Visible = true;
                Txtsavecx.Visible = true;
                Btnview.Visible = true;
                Txtcaxun.Text = sqlstrcn;
                ////查询的
                //ViewState["SQLSTR"] = sqlstr;
                //string asg = ViewState["SQLSTR"].ToString();
                //Pager pager = Page.FindControl("Pager1") as Pager;
                //pager.Pageindex = 0;
                //pager.PageSize = 10;
                //pager.PageTable = tablename;
                //pager.Condition = Condition;
                //pager.PageColumn = columns;
                //pager.ControlName = "GridView1";
                //pager.key = paixu;
                //pager.InitBindData = true;
                //pager.PageBind();
                DataTable dt = DAL.DBHelper.ExecuteDataTable(sqlstr);
                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                else
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("007655", "无此记录！") + "');", true);
                    return;
                }

            }
        }

    }
    
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bind(); 
    }
    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        string aa="";
        for (int j = 0; j < GridView1.HeaderRow.Cells.Count; j++)
        {
            aa = GridView1.HeaderRow.Cells[j].Text.ToString();
            if ((aa == GetTran("000025", "会员姓名")) || (aa == GetTran("000192","推荐姓名")) || (aa ==GetTran("000097", "安置姓名")) || (aa ==GetTran("000086", "开户名")) || (aa ==GetTran("001910", "负责人姓名")) || (aa == GetTran("000040","机构名称")))
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridView1.Rows[i].Cells[j].Text = Encryption.Encryption.GetDecipherName(GridView1.Rows[i].Cells[j].Text.ToString().Trim());

                }
            }
            if ((aa ==GetTran("000031", "注册时间")) || (aa ==GetTran("005942", "报单时间")) || (aa ==GetTran("000967", "创建时间")) || (aa ==GetTran("007672", "最后登录时间")) || (aa ==GetTran("007974", "最近登录时间")))
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridView1.Rows[i].Cells[j].Text = Convert.ToDateTime(GridView1.Rows[i].Cells[j].Text).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString();
                }
            }
            if (aa ==GetTran("000105", "出生时间"))
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridView1.Rows[i].Cells[j].Text = Convert.ToDateTime(GridView1.Rows[i].Cells[j].Text).Date.ToShortDateString () ;
                }
            }
        }
    }

    protected void GridView2_DataBound(object sender, EventArgs e)
    {
        string aa = "";
        for (int j = 0; j < GridView2.HeaderRow.Cells.Count; j++)
        {
            aa = GridView2.HeaderRow.Cells[j].Text.ToString();
            if ((aa == GetTran("000025", "会员姓名")) || (aa == GetTran("000192", "推荐姓名")) || (aa == GetTran("000097", "安置姓名")) || (aa == GetTran("000086", "开户名")) || (aa == GetTran("001910", "负责人姓名")) || (aa == GetTran("000040", "机构名称")))
            {
                for (int i = 0; i < GridView2.Rows.Count; i++)
                {
                    GridView2.Rows[i].Cells[j].Text = Encryption.Encryption.GetDecipherName(GridView2.Rows[i].Cells[j].Text.ToString().Trim());

                }
            }
            if ((aa == GetTran("000031", "注册时间")) || (aa == GetTran("005942", "报单时间")) || (aa == GetTran("000967", "创建时间")) || (aa == GetTran("007672", "最后登录时间")) || (aa == GetTran("007974", "最近登录时间")))
            {
                for (int i = 0; i < GridView2.Rows.Count; i++)
                {
                    GridView2.Rows[i].Cells[j].Text = Convert.ToDateTime(GridView2.Rows[i].Cells[j].Text).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString();
                }
            }
            if (aa == GetTran("000105", "出生时间"))
            {
                for (int i = 0; i < GridView2.Rows.Count; i++)
                {
                    GridView2.Rows[i].Cells[j].Text = Convert.ToDateTime(GridView2.Rows[i].Cells[j].Text).Date.ToShortDateString();
                }
            }
        }

    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        bind1(); 
    }
}
