using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Text;
using BLL.CommonClass;
using System.Collections.Generic;

using System.Collections;
using DAL;

/// <summary>
///JieGouNew2 的摘要说明
/// </summary>
public class JieGouNew2
{
    public static int height1 = 20;//编号高度
    public static int height2 = 17;//级别高度
    public static int height3 = 20;//总余新高度
    public static int tdPadding = 3;
    public static int jianJu = 20;//每个子Table的间距
    public static int regDiv = 60;//添加注册人按钮的宽度

    public static string strAzTj;

    public static string qs1;

    public JieGouNew2()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    public static string GetOneTable(DataTable dt, string bh, int isAnTj)
    {
        BLL.TranslationBase tran = new BLL.TranslationBase();

        StringBuilder sb = new StringBuilder();

        string[] int1 = new string[6000];

        string[] int2 = new string[6000];

        string[] int3 = new string[6000];

        DataRow[] row;

        row = dt.Select("Number='" + bh + "'", "Xuhao");


        if (row[0]["number"].ToString().IndexOf("空点") == -1)
        {

            string[] strList = row[0]["DownChar"].ToString().Split('|');


            if (row[0]["DownChar"].ToString().Trim() == "")
            {
                strList = new string[] { "-2", "-2" };
            }

            int qs = strList.Length - 1;


            int k = 0;

            for (int i = 0; i < qs; i++)
            {
                string[] strList2 = strList[i].Split(',');

                for (int u = 0; u < strList2.Length; u++)
                {

                    if (strList2.Length > 0)
                    {
                        if (strList2[0].ToString().Trim() != "")
                        {
                            int1[k] = strList2[0].ToString();
                        }
                    }
                    if (strList2.Length > 1)
                    {
                        if (strList2[1].ToString().Trim() != "")
                        {
                            int2[k] = strList2[1].ToString();
                        }
                    }

                    if (strList2.Length > 2)
                    {
                        if (strList2[2].ToString().Trim() != "")
                        {
                            int3[k] = strList2[2].ToString();
                        }
                    }

                    k++;

                    break;


                }
            }

            int rsp = 3;

            //isAnTj == 2 为推荐


            sb.Append("<table width='" + row[0]["Lenth"].ToString() + "' height='100%' cellspacing='0' cellpadding='0' bordercolor='#cccccc' border='1' bordercolordark='#ffffff' class='Tb22'><tbody><tr><td height='" + height1 + "' bgcolor='#649d2e' align='center' colspan='" + (qs + 1) + "'><a href='MemberNetMap.aspx?net=" + strAzTj + "&amp;SelectGrass=" + qs1 + "&amp;bianhao=" + bh + "'><font color='#fff'>" + bh + "</font></a></td></tr><tr><td height='" + height2 + "' align='center'  colspan='" + (qs + 1) + "'>" + GetLevelStr(row[0]["LevelInt"].ToString()) + "</td></tr><tr><td  bgcolor='#edf1f8' align='center' ><tr><td height='" + height3 + "' style=' width: 30px;'>&nbsp;</td>");

            for (int i = 0; i < qs; i++)
            {
                sb.Append("<td >");
                sb.Append((i + 1) + (isAnTj == 2 ? tran.GetTran("007332", "线") : tran.GetTran("007331", "区")));
                sb.Append("</td>");
            }


            sb.Append("</tr><tr><td style='padding:" + tdPadding + "'>" + tran.GetTran("007324", "总") + "</td>");

            for (int i = 0; i < qs; i++)
            {

                if (int1[i] == "-1")
                {
                    sb.Append("<td  rowspan=" + rsp + "     >");
                    sb.Append("<font style='font-size:12px;'>" + tran.GetTran("007334", "临<br/>时") + "</font>");
                }
                else if (int1[i] == "-2")
                {
                    sb.Append("<td  rowspan=" + rsp + "   >");
                    //sb.Append("<font style='font-size:12px;'>" + GetRetStr(strAzTj, bh) + "</font>");
                }
                else
                {
                    sb.Append("<td >");
                    sb.Append(int1[i].ToString());
                }
                sb.Append("</td>");
            }
            sb.Append("</tr>");

            sb.Append("</tr>");

            if (isAnTj == 1)//安置
            {
                sb.Append("<tr><td style='padding:" + tdPadding + "'>" + tran.GetTran("007326", "余") + "</td>");
                for (int i = 0; i < qs; i++)
                {
                    if (int1[i] != "-1" && int1[i] != "-2")
                    {
                        sb.Append("<td >");
                        sb.Append(int2[i].ToString());
                        sb.Append("</td>");
                    }
                }
                sb.Append("</tr>");
            }

            sb.Append("<tr><td style='padding:" + tdPadding + "'>" + tran.GetTran("007325", "新") + "</td>");

            for (int i = 0; i < qs; i++)
            {
                if (int1[i] != "-1" && int1[i] != "-2")
                {
                    sb.Append("<td >");
                    sb.Append(int3[i].ToString());
                    sb.Append("</td>");
                }
            }

            sb.Append("</td></tr></tbody></table>");
        }
        else
        {
            sb.Append("<table width='" + row[0]["Lenth"].ToString() + "' height='100%' cellspacing='0' cellpadding='0'    align='center'><tbody><tr><td width='100%' height='" + height1 + "' align='center' colspan='2' valign='top'><table width='" + regDiv + "' cellspacing='0' cellpadding='0'  bordercolor='#cccccc' border='1' bordercolordark='#ffffff' height='30' class='H2'><tbody><tr><td align='center'>" + GetRetStr2(strAzTj, bh) + "</td></tr></tbody></table></td></tr></tbody></table>");
        }


        return sb.ToString();

    }

    public static string GetLevelStr(string lvStr)
    {
        return PublicClass.GetAllLevelStr(2, Convert.ToInt32(lvStr));
    }

    public static string GetRetStr(string strAzTj, string bh)
    {
        SqlParameter[] param = new SqlParameter[] { new SqlParameter("@rt", SqlDbType.Int), new SqlParameter("@placement", bh) };
        param[0].Direction = ParameterDirection.ReturnValue;
        DBHelper.ExecuteNonQuery("procAZDistrict", param, CommandType.StoredProcedure);
        string rt = param[0].Value.ToString();
        BLL.TranslationBase tran = new BLL.TranslationBase();
        string str555 = "";
        if (HttpContext.Current.Session["Company"] != null)
        {
            str555 = (strAzTj == "az" ? " <a href='../RegisterMember/RegisterMember.aspx?LoginType=1&net=" + strAzTj + "&amp;SelectGrass=" + qs1 + "&amp;az=" + bh + "&qushu=" + rt + "'> " + tran.GetTran("007335", "注<br/>册") + "  </a>" : tran.GetTran("007314", "空<br/>位"));
        }
        if (HttpContext.Current.Session["Store"] != null)
        {
            str555 = (strAzTj == "az" ? " <a href='../RegisterMember/RegisterMember.aspx?LoginType=2&net=" + strAzTj + "&amp;SelectGrass=" + qs1 + "&amp;az=" + bh + "&qushu=" + rt + "'>" + tran.GetTran("007335", "注<br/>册") + "  </a>" : tran.GetTran("007314", "空<br/>位"));
        }
        if (HttpContext.Current.Session["Member"] != null)
        {
            str555 = (strAzTj == "az" ? " <a href='../RegisterMember/RegisterMember.aspx?LoginType=3&net=" + strAzTj + "&amp;SelectGrass=" + qs1 + "&amp;az=" + bh + "&qushu=" + rt + "'  target='_top'>" + tran.GetTran("007335", "注<br/>册") + "  </a>" : tran.GetTran("007314", "空<br/>位"));
        }

        return str555;

    }

    public static string GetRetStr2(string strAzTj, string bh)
    {
        BLL.TranslationBase tran = new BLL.TranslationBase();
        string str555 = "";// "<a href='../RegisterMember/RegisterMember.aspx?net=" + strAzTj + "&num=" + bh + "' target='_top'><font color='#000' style='font-size:10pt'>注册</font></a>";
        if (HttpContext.Current.Session["Company"] != null)
        {
            str555 = "<a href='../RegisterMember/RegisterMember.aspx?net=" + strAzTj + "&num=" + bh + "&qushu=1' target='_self'><font color='#000' style='font-size:10pt'>" + tran.GetTran("007297", "注册") + "</font></a>";
        }
        if (HttpContext.Current.Session["Store"] != null)
        {
            str555 = "<a href='../RegisterMember/RegisterMember.aspx?net=" + strAzTj + "&num=" + bh + "&qushu=1' target='_self'><font color='#000' style='font-size:10pt'>" + tran.GetTran("007297", "注册") + "</font></a>";
        }
        if (HttpContext.Current.Session["Member"] != null)
        {
            str555 = "<a href='../RegisterMember/RegisterMember.aspx?net=" + strAzTj + "&num=" + bh + "&qushu=1' target='_top'><font color='#000' style='font-size:10pt'>" + tran.GetTran("007297", "注册") + "</font></a>";
        }

        return str555;

    }

    public static string GetFirst(DataTable dt, string bh, int isAnTj)
    {
        if (dt.Rows.Count == 0)
            return "";

        StringBuilder sb2 = new StringBuilder();


        DataRow[] row;

        row = dt.Select("number='" + bh + "'", "Xuhao");

        DataRow[] row2;

        row2 = dt.Select("cengshu=2", "Xuhao");//第二层

        string toWidth = (Convert.ToInt32(row[row.Length - 1]["LeftDistance"].ToString()) + Convert.ToInt32(row[row.Length - 1]["Lenth"].ToString())).ToString();

        sb2.Append("<table width='" + toWidth + "' cellspacing='0' cellpadding='0'><tr>");


        int tableWidth = Convert.ToInt32(row[0]["Lenth"].ToString());//总余新宽度
        int tPLeft = Convert.ToInt32(row[0]["Said"].ToString());//距离左边的宽度
        int leftWidth = 0;
        int rightWidth = 0;

        if (tableWidth % 2 == 1)
        {
            leftWidth = tableWidth / 2;
            rightWidth = tableWidth / 2 - 1;
        }
        else
        {
            leftWidth = tableWidth / 2 - 2;
            rightWidth = tableWidth / 2;
        }


        sb2.Append("<td width='" + tPLeft + "'><table width='100%'><tbody><tr><td>&nbsp;</td></tr></tbody></table></td><td width='" + tableWidth + "' align='center'>" + GetOneTable(dt, bh, isAnTj) + "</td></tr>");

        sb2.Append("<tr><td width='" + tPLeft + "'><table width='100%'><tbody><tr><td>&nbsp;</td></tr></tbody></table></td><td width='" + (Convert.ToInt32(toWidth) - tPLeft) + "'><table width='100%' cellspacing='0' cellpadding='0' border='0' ><tbody><tr><td width='" + leftWidth + "' height=\"18\"></td><td width=\"2\" background=\"images/images02_02.gif\"  > </td><td width='" + rightWidth + "'></td></tr></tbody></table></td>");
        sb2.Append("</tr></table>");

        return sb2.ToString();

    }

    public static string GetTwo(DataTable dt, int isAnTj)
    {
        StringBuilder sb2 = new StringBuilder();

        DataRow[] row;

        row = dt.Select("cengshu=2", "Xuhao");//第二层

        int firstWidth = 0;

        int endWidth = 0;

        int w2 = 0;

        int firstPLeft = 0;

        int leftWidth = 0;
        int rightWidth = 0;

        string toWidth = (Convert.ToInt32(row[row.Length - 1]["LeftDistance"].ToString()) + Convert.ToInt32(row[row.Length - 1]["Lenth"].ToString())).ToString(); //JieGouNew.GetFinalWidth(dt).ToString();


        //画横线
        sb2.Append("<table width='" + toWidth + "' cellspacing='0' cellpadding='0'><tr>");


        for (int i = 0; i < row.Length; i++)
        {
            if (i == 0)
            {
                firstWidth = Convert.ToInt32(row[i]["Lenth"].ToString());
                firstPLeft = Convert.ToInt32(row[i]["Said"].ToString());
            }

            if (i == row.Length - 1)
            {
                endWidth = Convert.ToInt32(row[i]["Lenth"].ToString());
            }

            w2 += Convert.ToInt32(row[i]["Lenth"].ToString()) + Convert.ToInt32(row[i]["Said"].ToString());
        }


        if (row.Length > 1)
        {

            leftWidth = firstWidth / 2 + firstPLeft - 1;

            rightWidth = endWidth / 2 - 1;

            int midWidth = Convert.ToInt32(toWidth) - leftWidth - rightWidth + 2;

            if (firstWidth % 2 == 1)
            {
                leftWidth = firstWidth / 2 + firstPLeft;
                midWidth -= 1;
            }

            if (endWidth % 2 == 1)
            {
                rightWidth = endWidth / 2;
                midWidth -= 1;
            }

            sb2.Append("<td  width='100%'><table width='100%' cellspacing='0' cellpadding='0' border='0' ><tbody><tr><td width='" + leftWidth + "' height=\"2\"></td><td width='" + midWidth + "' background=\"images/images03_05.gif\"  > </td><td width='" + rightWidth + "'></td></tr></tbody></table></td>");
        }
        else
        {
            sb2.Append("<td  width='100%'><table width='100%' cellspacing='0' cellpadding='0' border='0' ><tbody><tr><td width='100%' height=\"2\" colspan='3'></td></tr></tbody></table></td>");
        }


        sb2.Append("</tr></table>");

        DataRow[] row3;

        DataRow[] row4;

        row3 = dt.Select("cengshu=3", "Xuhao");//第三层



        //画第二层竖线和总余新同时画第三层第一条竖线
        sb2.Append("<table width='" + toWidth + "' cellspacing='0' cellpadding='0'><tr>");

        if (row3.Length > 0)
        {
            for (int i = 0; i < row.Length; i++)
            {
                firstWidth = Convert.ToInt32(row[i]["Lenth"].ToString());
                firstPLeft = Convert.ToInt32(row[i]["Said"].ToString());

                if (firstWidth % 2 == 1)
                {
                    leftWidth = firstWidth / 2;
                    rightWidth = firstWidth / 2 - 1;
                }
                else
                {
                    leftWidth = firstWidth / 2 - 2;
                    rightWidth = firstWidth / 2;
                }

                sb2.Append("<td width='" + (firstWidth + firstPLeft) + "'><table width='100%' cellspacing='0' cellpadding='0' border='0' ><tbody><tr><td width='" + firstPLeft + "'><table width='100%'><tbody><tr><td>&nbsp;</td></tr></tbody></table></td><td width='" + firstWidth + "'><table width='100%' cellspacing='0' cellpadding='0' border='0' ><tbody><tr><td width='" + leftWidth + "' height=\"18\"></td><td width=\"2\" background=\"images/images02_02.gif\"  > </td><td width='" + rightWidth + "'></td></tr></tbody></table></td></tr><tr><td width='" + firstPLeft + "'><table width='100%'><tbody><tr><td>&nbsp;</td></tr></tbody></table></td><td width='" + firstWidth + "'>" + GetOneTable(dt, row[i]["number"].ToString(), isAnTj) + "</td></tr><tr><td width='" + firstPLeft + "'><table width='100%'><tbody><tr><td>&nbsp;</td></tr></tbody></table></td><td width='" + firstWidth + "'><table width='100%' cellspacing='0' cellpadding='0' border='0' ><tbody><tr><td width='" + leftWidth + "' height=\"18\"></td><td width=\"2\" background=\"images/images02_02.gif\"  > </td><td width='" + rightWidth + "'></td></tr></tbody></table></td></tr></tbody></table></td>");
            }
        }
        else
        {
            for (int i = 0; i < row.Length; i++)
            {
                firstWidth = Convert.ToInt32(row[i]["Lenth"].ToString());
                firstPLeft = Convert.ToInt32(row[i]["Said"].ToString());

                if (firstWidth % 2 == 1)
                {
                    leftWidth = firstWidth / 2;
                    rightWidth = firstWidth / 2 - 1;
                }
                else
                {
                    leftWidth = firstWidth / 2 - 2;
                    rightWidth = firstWidth / 2;
                }

                sb2.Append("<td width='" + (firstWidth + firstPLeft) + "'><table width='100%' cellspacing='0' cellpadding='0' border='0' ><tbody><tr><td width='" + firstPLeft + "'><table width='100%'><tbody><tr><td>&nbsp;</td></tr></tbody></table></td><td width='" + firstWidth + "'><table width='100%' cellspacing='0' cellpadding='0' border='0' ><tbody><tr><td width='" + leftWidth + "' height=\"18\"></td><td width=\"2\" background=\"images/images02_02.gif\"  > </td><td width='" + rightWidth + "'></td></tr></tbody></table></td></tr><tr><td width='" + firstPLeft + "'><table width='100%'><tbody><tr><td>&nbsp;</td></tr></tbody></table></td><td width='" + firstWidth + "'>" + GetOneTable(dt, row[i]["number"].ToString(), isAnTj) + "</td>");
            }
        }

        sb2.Append("</tr></table>");

        return sb2.ToString();
    }

    public static string GetThree(DataTable dt, int isAnTj)
    {
        StringBuilder sb2 = new StringBuilder();

        DataRow[] row3;

        row3 = dt.Select("cengshu=3", "Xuhao");//第三层

        if (row3.Length == 0)
        {
            return "";
        }

        DataRow[] row2;

        row2 = dt.Select("cengshu=2", "Xuhao");//第二层

        DataRow[] rowTeam;//第三层团队

        int firstWidth = 0;

        int endWidth = 0;

        int w2 = 0;

        int firstPLeft = 0;

        int firstPLeft1 = 0;

        int toWidth = JieGouNew2.GetFinalWidth(dt);

        int midWidth = 0;

        int leftWidth = 0;

        int rightWidth = 0;

        int toWidth2 = 0;

        if (row2.Length > 0)
        {
            toWidth2 = Convert.ToInt32(row2[row2.Length - 1]["LeftDistance"].ToString()) + Convert.ToInt32(row2[row2.Length - 1]["Lenth"].ToString());
        }

        //画横线
        int toWidth3 = (Convert.ToInt32(row3[row3.Length - 1]["LeftDistance"].ToString()) + Convert.ToInt32(row3[row3.Length - 1]["Lenth"].ToString()));

        sb2.Append("<table width='" + toWidth3 + "' cellspacing='0' cellpadding='0'><tr>");

        for (int i = 0; i < row2.Length; i++)
        {
            rowTeam = dt.Select("shangji='" + row2[i]["number"] + "'", "Xuhao");

            w2 = 0;

            for (int k = 0; k < rowTeam.Length; k++)
            {
                firstPLeft = Convert.ToInt32(rowTeam[k]["Said"].ToString());

                if (firstPLeft == 0)
                {
                    firstPLeft = jianJu;
                }

                w2 += firstPLeft + Convert.ToInt32(rowTeam[k]["Lenth"].ToString());


                if (k == 0)
                {
                    firstWidth = Convert.ToInt32(rowTeam[k]["Lenth"].ToString());

                    firstPLeft1 = Convert.ToInt32(rowTeam[k]["Said"].ToString());
                }

                if (k == rowTeam.Length - 1)
                {
                    endWidth = Convert.ToInt32(rowTeam[k]["Lenth"].ToString());
                }
            }

            leftWidth = firstPLeft1 + (firstWidth / 2) - 2;

            rightWidth = (endWidth / 2) - 2;

            midWidth = w2 - leftWidth - rightWidth - firstPLeft1 + 4;

            if (rowTeam.Length > 1)
            {
                sb2.Append("<td  width='" + w2 + "'><table width='100%' cellspacing='0' cellpadding='0' border='0' ><tbody><tr><td width='" + leftWidth + "' height=\"2\"></td><td width='" + midWidth + "' background=\"images/images03_05.gif\"  > </td><td width='" + rightWidth + "'></td></tr></tbody></table></td>");
            }
            else
            {
                leftWidth = w2 / 3;

                midWidth = w2 / 3;

                rightWidth = w2 - leftWidth - midWidth;

                sb2.Append("<td  width='" + w2 + "'><table width='100%' cellspacing='0' cellpadding='0' border='0' ><tbody><tr><td width='" + w2 + "' height=\"2\"></td><td width='" + midWidth + "' > </td><td width='" + rightWidth + "'></td></tr></tbody></table></td>");
            }
        }


        sb2.Append("</tr></table>");

        //画竖线和第三层总余新
        sb2.Append("<table width='" + toWidth3 + "' cellspacing='0' cellpadding='0'><tr>");

        int tw2 = 0;

        for (int i = 0; i < row2.Length; i++)
        {
            rowTeam = dt.Select("shangji='" + row2[i]["number"] + "'", "Xuhao");

            w2 = 0;

            tw2 = Convert.ToInt32(row2[i]["Lenth"]);

            for (int k = 0; k < rowTeam.Length; k++)
            {
                firstWidth = Convert.ToInt32(rowTeam[k]["Lenth"].ToString());
                firstPLeft = Convert.ToInt32(rowTeam[k]["Said"].ToString());

                if (firstPLeft == 0)
                {
                    firstPLeft = jianJu;
                }

                if (firstWidth % 2 == 1)
                {
                    leftWidth = firstWidth / 2;
                    rightWidth = firstWidth / 2 - 1;
                }
                else
                {
                    leftWidth = firstWidth / 2 - 2;
                    rightWidth = firstWidth / 2;
                }

                sb2.Append("<td width='" + (Convert.ToInt32(rowTeam[k]["Lenth"].ToString()) + Convert.ToInt32(rowTeam[k]["Said"].ToString())) + "' valign='top'>");
                sb2.Append("<table width='100%' cellspacing='0' cellpadding='0' border='0' >");
                sb2.Append("<tbody>");

                sb2.Append("<tr><td width='" + firstPLeft + "'><table width='100%'><tbody><tr><td>&nbsp;</td></tr></tbody></table></td><td width='" + rowTeam[k]["Lenth"].ToString() + "'><table width='100%' cellspacing='0' cellpadding='0' border='0' ><tbody><tr><td width='" + leftWidth + "' height=\"18\"></td><td width=\"2\" background=\"images/images02_02.gif\"  > </td><td width='" + rightWidth + "'></td></tr></tbody></table></td></tr>");

                sb2.Append("<tr><td width='" + firstPLeft + "' ><table width='100%'><tbody><tr><td>&nbsp;</td></tr></tbody></table></td><td width='" + rowTeam[k]["Lenth"].ToString() + "'>" + GetOneTable(dt, rowTeam[k]["number"].ToString(), isAnTj) + "</td></tr>");

                sb2.Append("</tbody></table></td>");

            }
        }

        sb2.Append("</tr></table>");

        return sb2.ToString();
    }

    //计算总宽度
    public static int GetFinalWidth(DataTable dt)
    {
        if (dt.Rows.Count == 0)
            return 0;

        int width1 = -2;
        int width2 = -1;
        int width3 = 0;

        DataRow[] row;

        row = dt.Select("cengshu=3", "Xuhao");//第三层

        if (row.Length != 0)
        {
            width3 = Convert.ToInt32(row[row.Length - 1]["LeftDistance"].ToString()) + Convert.ToInt32(row[row.Length - 1]["Lenth"].ToString());
        }

        row = dt.Select("cengshu=2", "Xuhao");//第二层

        if (row.Length != 0)
        {
            width2 = Convert.ToInt32(row[row.Length - 1]["LeftDistance"].ToString()) + Convert.ToInt32(row[row.Length - 1]["Lenth"].ToString());
        }

        row = dt.Select("cengshu=1", "Xuhao");//第二层

        if (row.Length != 0)
        {
            width1 = Convert.ToInt32(row[row.Length - 1]["LeftDistance"].ToString()) + Convert.ToInt32(row[row.Length - 1]["Lenth"].ToString());
        }

        if (width3 > width2)
        {
            width2 = width3;
        }
        if (width2 > width1)
        {
            width1 = width2;
        }

        return width1;
    }

    public static string GetAll(DataTable dt, string bh, int isAnTj)
    {
        string strAll = "";

        strAll = GetFirst(dt, bh, isAnTj) + GetTwo(dt, isAnTj) + GetThree(dt, isAnTj);

        return strAll;
    }

    //1:安置；2:推荐
    public static string Direct_Table_New(string bh, int qs, int isAnTj)
    {
        if (isAnTj == 1)
        {
            strAzTj = "az";
        }
        else
        {
            strAzTj = "tj";
        }

        SqlParameter[] spa = new SqlParameter[]{
            new SqlParameter("@number",bh),
            new SqlParameter("@ExpectNum",qs),
            new SqlParameter("@type",isAnTj)
        };
        qs1 = qs.ToString();
        DataTable dtNew = DAL.DBHelper.ExecuteDataTable("ShowNet", spa, CommandType.StoredProcedure);

        string toWidth = (JieGouNew2.GetFinalWidth(dtNew)).ToString();

        return "<table align='center' width='" + toWidth + "' style='margin-left:20px;'><tr><td align='left'>" + JieGouNew2.GetAll(dtNew, bh, isAnTj) + "<td/><tr></table>";

    }
}