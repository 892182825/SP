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
using DAL;
using System.Data.SqlClient;
using System.Collections.Generic;
using BLL.other.Company;
using BLL.CommonClass;
using BLL.other;

public partial class TWAjax : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ThreeRedirect(Page, "Member/" + Permissions.redirUrl);
        if (Request.QueryString["mode"] == "tw")
        {
            string cw = Request.QueryString["cw"];
            string xh = Request.QueryString["xh"];
            string btwbianhao = Request.QueryString["btbianhao"];
            string tdbianhao = Request.QueryString["tdbianhao"];
            string qs = Request.QueryString["qs"];

            string rt = IsTW(cw, xh, btwbianhao, tdbianhao, qs);

            if (rt == "0")
            {
                Response.Write(SetTW(btwbianhao, tdbianhao, cw));
            }
            else
                Response.Write(rt);
        } //刷新
        else if (Request.QueryString["mode"] == "sxaz")
        {
           
            string wltstr = WangLuoTu(Request.QueryString["startbh"], "", Convert.ToInt32(Request.QueryString["isanzhi"]), Convert.ToInt32(Request.QueryString["cs"]), Convert.ToInt32(Request.QueryString["qs"]), "");
            Response.Write(wltstr);
        }

        else if (Request.QueryString["mode"] == "sxtj")
        {
            string wltstr = WangLuoTuII(Request.QueryString["startbh"], "", Convert.ToInt32(Request.QueryString["isanzhi"]), Convert.ToInt32(Request.QueryString["cs"]), Convert.ToInt32(Request.QueryString["qs"]), "");
            Response.Write(wltstr);
        }
        else if (Request.QueryString["mode"] == "sx2")
        {
            //string wltstr = Jiegou.wltForEng(Convert.ToInt32(Request.QueryString["qs"]), Convert.ToInt32(Request.QueryString["cs"]), isAnZhi(), Request.QueryString["startbh"], "ShowNetworkView.aspx?type=1&bh=");
            //Response.Write(wltstr);
        }
        
        Response.End();

    }

    protected bool isAnZhi()
    {
        if (Convert.ToInt32(Request.QueryString["isanzhi"]) == 0)
        {
            return false;
        }
        return true;
    }

    //参数分别为：层位，序号，被调网的编号，要调到谁下面的编号，期数
    public string IsTW(string cw,string xh,string btwbianhao,string tdbianhao,string qs)
    {
        string sqlstr="select number," + cw + "," + xh + " from MemberInfoBalance"+qs+" where number='"+btwbianhao+"' order by ordinal1";

        SqlConnection con = new SqlConnection(DAL.DBHelper.connString);
        con.Open();

        SqlCommand cmd = new SqlCommand(sqlstr,con);
        SqlDataReader dr=cmd.ExecuteReader();

        List<string> lsnumber = new List<string>();

        int parentcw = 0;
        int parentxh = 0;

        if (dr.Read())
        {
            lsnumber.Add(dr["number"].ToString());

            parentcw = Convert.ToInt32(dr[cw]);
            parentxh = Convert.ToInt32(dr[xh]);
        }

        dr.Close();

        while (true)
        {
            parentxh++;
            cmd.CommandText = "select number," + cw + "," + xh + " from MemberInfoBalance" + qs + " where "+xh+"='" + parentxh + "' order by ordinal1";
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                if (Convert.ToInt32(dr[cw]) > parentcw)
                    lsnumber.Add(dr["number"].ToString());
                else
                    break;

                dr.Close();
            }
            else
            {
                dr.Close();
                break;
            }
        }

        cmd.Dispose();
        con.Close();

        if (lsnumber.Contains(tdbianhao))
            return "1";//存在，不能调网，不能调在自己的团队下
        else
            return "0";
    }

    public string SetTW(string btwbianhao, string tdbianhao,string model)
    {
        if (ChangeTeamBLL.GetQishu(btwbianhao) == CommonDataBLL.getMaxqishu())
             return "2";//当期会员不需调网，请到报单浏览处修改

        if (model == "LayerBit1")//安置
        {
            int flag_xiou = ChangeTeamBLL.GetPlacementCount(tdbianhao, btwbianhao);
            if (flag_xiou >= 3)
                return "3";//此安置编号下已经安置了三个人
        }

        //调网
        bool ispass = false;
        string msg = "";
        SqlDataReader dr=DBHelper.ExecuteReader("select Placement,Direct from MemberInfo where Number='" + btwbianhao + "'");
        string oldplacement = "";
        string oldDirect = "";

        if (dr.Read())
        {
            oldplacement = dr["Placement"].ToString();
            oldDirect = dr["Direct"].ToString();
        }
        dr.Close();

        if (model == "LayerBit1")//安置
        {
            msg = ChangeTeamBLL.ChenageNet(btwbianhao, tdbianhao, oldDirect, oldplacement, oldDirect, ChangeTeamBLL.GetFlag(btwbianhao), out ispass);
        }
        else
        {
            msg = ChangeTeamBLL.ChenageNet(btwbianhao, oldplacement, tdbianhao, oldplacement, oldDirect, ChangeTeamBLL.GetFlag(btwbianhao), out ispass);
        }

        return msg;
    }
     
    //安置
    protected string WangLuoTu(string bianhao, string tree, int state, int cengshu, int qishu, string storeid)
    {
        //获得存储过程产生的树
        int type = 0;//公司查看网络图
        DataTable table = new TreeViewBLL().GetExtendTreeView_NewAz(bianhao, tree, cengshu, qishu, type, 0);
        string str = "";
        //循环拼出树
        str = "<table id='tab_tr' border=\"0\" cellspacing=\"0\" cellpadding=\"0\"  class='tree_grid'>";
        str += "<tr>"
               + "<th align='center'>会员编号</th>"
               + "<th align='center' >层数</th>"
               + "<th align='center'>级别</th>"
               + "<th align='right'>新个分数</th>"
               + "<th align='right'>新网分数</th>"
               + "<th align='right'>新网人数</th>"
               + "<th align='right' >总网人数</th>"
               + "<th align='right' >总网分数</th>"

               + "</tr>";
        int count = 0;
        foreach (DataRow row in table.Rows)
        {
            count++;

            string strStyle = "";
            if (count % 2 == 0)
            {
                strStyle = "background-color:#F1F4F8"; ;
            }
            else
            {
                strStyle = " background-color:#FAFAFA";
            }
            str += "<tr style='" + strStyle + "' class=\"tr\"  id='tr" + row["bh"].ToString() + "'  onmousedown=\"down_tw(event,this)\">";
            str += "<td valing='middle'>" + row["htmltree"].ToString().Replace("─", "<img src='../images/011.gif'  align=absmiddle  border=0 />").Replace("☆", "<img src='../images/013.gif'  align=absmiddle  border=0 />").Replace("★", "<img src='../images/014.gif'  align=absmiddle  border=0 />").Replace("├", "<img src='../images/006.gif'  align=absmiddle  border=0 />").Replace("└", "<img src='../images/003.gif'  align=absmiddle  border=0 />").Replace("~", "<img src='../images/015.gif'  align=absmiddle  border=0 />").Replace("|", "<img src='../images/004.gif'  align=absmiddle  border=0 />") + "<img src='../images/1.png' class='img' align=absmiddle border=0 /></td>";
            str += "<td align='center' valing='middle' title='层数'>" + row["cw"].ToString() + "</td>";
            str += "<td align='left' valing='middle' title='级别'>" + row["level"].ToString() + "</td>";
            str += "<td align='right' valing='middle' title='新个分数'>" + row["CurrentOneMark"].ToString() + "</td>";
            str += "<td align='right' valing='middle' title='新网分数'>" + row["CurrentTotalNetRecord"].ToString() + "</td>";
            str += "<td align='right' valing='middle' title='新网人数'>" + row["CurrentNewNetNum"].ToString() + "</td>";
            str += "<td align='right' valing='middle' title='总网人数'>" + row["TotalNetNum"].ToString() + "</td>";
            str += "<td align='right' valing='middle' title='总网分数'>" + row["TotalNetRecord"].ToString() + "</td>";

            str += "</tr>";
        }
        str += "</table>";

        return str;
    }

    //推荐
    protected string WangLuoTuII(string bianhao, string tree, int state, int cengshu, int qishu, string storeid)
    {

        //获得存储过程产生的树
        int type = 0;//公司查看网络图
        DataTable table = new TreeViewBLL().GetExtendTreeView_NewTj(bianhao, tree, cengshu, qishu, type, 0);
        string str = "";
        //循环拼出树
        str = "<table id='tab_tr' border=\"0\" cellspacing=\"0\" cellpadding=\"0\"  class='tree_grid'>";
        str += "<tr>"
               + "<th align='center'>会员编号</th>"
               + "<th align='center' >层数</th>"
               + "<th align='center'>级别</th>"
               + "<th align='right'>新个分数</th>"
               + "<th align='right'>新网分数</th>"
               + "<th align='right'>新网人数</th>"
               + "<th align='right' >总网人数</th>"
               + "<th align='right' >总网分数</th>"

               + "</tr>";
        int count = 0;
        foreach (DataRow row in table.Rows)
        {
            count++;

            string strStyle = "";
            if (count % 2 == 0)
            {
                strStyle = "background-color:#F1F4F8";
            }
            else
            {
                strStyle = " background-color:#FAFAFA";
            }
            str += "<tr style='" + strStyle + "' class=\"tr\" id='tr" + row["bh"].ToString() + "'  onmousedown=\"down_tw(event,this)\">";
            str += "<td valing='middle'>" + row["htmltree"].ToString().Replace("─", "<img src='../images/011.gif'  align=absmiddle  border=0 />").Replace("☆", "<img src='../images/013.gif'  align=absmiddle  border=0 />").Replace("★", "<img src='../images/014.gif'  align=absmiddle  border=0 />").Replace("├", "<img src='../images/006.gif'  align=absmiddle  border=0 />").Replace("└", "<img src='../images/003.gif'  align=absmiddle  border=0 />").Replace("~", "<img src='../images/015.gif'  align=absmiddle  border=0 />").Replace("|", "<img src='../images/004.gif'  align=absmiddle  border=0 />") + "<img src='../images/1.png' class='img' align=absmiddle border=0 /></td>";
            str += "<td align='center' valing='middle' title='层数'>" + row["cw"].ToString() + "</td>";
            str += "<td align='left' valing='middle' title='级别'>" + row["level"].ToString() + "</td>";
            str += "<td align='right' valing='middle' title='新个分数'>" + row["CurrentOneMark"].ToString() + "</td>";
            str += "<td align='right' valing='middle' title='新网分数'>" + row["DCurrentTotalNetRecord"].ToString() + "</td>";
            str += "<td align='right' valing='middle' title='新网人数'>" + row["DCurrentNewNetNum"].ToString() + "</td>";
            str += "<td align='right' valing='middle' title='总网人数'>" + row["DTotalNetNum"].ToString() + "</td>";
            str += "<td align='right' valing='middle' title='总网分数'>" + row["DTotalNetRecord"].ToString() + "</td>";

            str += "</tr>";
        }
        str += "</table>";
        return str;

    }
}
