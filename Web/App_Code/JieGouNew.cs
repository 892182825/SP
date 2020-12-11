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

/// <summary>
///JieGouNew 的摘要说明
/// </summary>
public class JieGouNew
{
    public static int height1 = 20;//编号高度
    public static int height2 = 17;//级别高度
    public static int height3 = 20;//总余新高度
    public static int tdPadding = 3;
    public static int jianJu = 20;//每个子Table的间距
    public static int regDiv = 60;//添加注册人按钮的宽度

    public static string strAzTj;

    public JieGouNew()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    public static string GetOneTable(DataTable dt, string bh)
    
    {
        StringBuilder sb = new StringBuilder();

        //List<double[]> objList = new List<double[]>();

        string[] int1 = new string[6000];

        string[] int2 = new string[6000];

        string[] int3 = new string[6000];

        DataRow[] row;

        row = dt.Select("Number='" + bh + "'", "Xuhao");

        if (row[0]["DownChar"].ToString().Trim()!="")
        {



            string[] strList = row[0]["DownChar"].ToString().Split('|');

            //string[] strList2 = row[0]["con"].ToString().Split('|');



            int qs = strList.Length-1;

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
            //sb.Append("<table width='" + row[0]["Lenth"].ToString() + "' height='100%' cellspacing='0' cellpadding='0' bordercolor='#cccccc' border='1'  bordercolordark='#ffffff'><tbody><tr><td height='" + height1 + "' bgcolor='#00cc00' align='center' colspan='2'><a href='MemberNetMap.aspx?net="+strAzTj+"&amp;SelectGrass=3&amp;bianhao=" + bh + "'><font color='#333333'>" + bh + "</font></a></td></tr><tr><td height='" + height2 + "' align='center' style='font-size: 12px;' colspan='2'>" + row[0]["lv"].ToString() + "</td></tr><tr><td  bgcolor='#edf1f8' align='center' colspan='2'><table cellspacing='0' cellpadding='0' border='1' width='100%' style='font-size: 10px; text-align: center; border-style: solid;'><tbody><tr><td '" + height3 + "' style='border-style: solid; width: 30px;'>&nbsp;</td>");

            sb.Append("<table width='" + row[0]["Lenth"].ToString() + "' height='100%' cellspacing='0' cellpadding='0' bordercolor='#cccccc' border='1'  bordercolordark='#ffffff' ><tbody><tr><td height='" + height1 + "' bgcolor='#00cc00' align='center' colspan='2'><a href='MemberNetMap.aspx?net=" + strAzTj + "&amp;SelectGrass=3&amp;bianhao=" + bh + "'><font color='#333333'>" + bh + "</font></a></td></tr><tr><td height='" + height2 + "' align='center' style='font-size: 12px;' colspan='2'>普通</td></tr><tr><td  bgcolor='#edf1f8' align='center' colspan='2'><table cellspacing='0' cellpadding='0' border='1' width='100%' style='font-size: 10px; text-align: center; '><tbody><tr><td '" + height3 + "' style='border-style: solid; width: 30px;'>&nbsp;</td>");

            for (int i = 0; i < qs; i++)
            {
                sb.Append("<td style='border-style: solid;'>");
                sb.Append((i + 1) + "区");
                sb.Append("</td>");
            }


            sb.Append("</tr><tr><td style='border-style: solid;padding:" + tdPadding + "'>总</td>");

            for (int i = 0; i < qs; i++)
            {

                if (int1[i] == "-1")
                {
                    sb.Append("<td style='border-style: solid;' rowspan=" + rsp + "     >");
                    sb.Append("<font style='font-size:12px;'>临<br/>时</font>");
                }
                else if (int1[i] == "-2")
                {
                    sb.Append("<td style='border-style: solid;' rowspan=3   >");
                    sb.Append("<font style='font-size:12px;'>" + (strAzTj == "az" ? " <a href='Registernewnext.aspx?net=" + strAzTj + "&amp;az=" + bh + "'> 注<br/>册  </a>" : "空<br/>位") + "</font>");
                }
                else
                {
                    sb.Append("<td style='border-style: solid;'>");
                    sb.Append(int1[i].ToString());
                }
                sb.Append("</td>");
            }
            sb.Append("</tr>");

            sb.Append("</tr><tr><td style='border-style: solid;padding:" + tdPadding + "'>余</td>");

            for (int i = 0; i < qs; i++)
            {
                if (int1[i] != "-1" && int1[i] != "-2")
                {
                    sb.Append("<td style='border-style: solid;'>");
                    sb.Append(int2[i].ToString());
                    sb.Append("</td>");
                }
            }

            sb.Append("</tr><tr><td style='border-style: solid;padding:" + tdPadding + "'>新</td>");

            for (int i = 0; i < qs; i++)
            {
                if (int1[i] != "-1" && int1[i] != "-2")
                {
                    sb.Append("<td style='border-style: solid;'>");
                    sb.Append(int3[i].ToString());
                    sb.Append("</td>");
                }
            }

            sb.Append("</tr></tbody></table></td></tr></tbody></table>");
        }
        else
        {
            sb.Append("<table width='" + row[0]["Lenth"].ToString() + "' height='100%' cellspacing='0' cellpadding='0'    align='center'><tbody><tr><td width='100%' height='" + height1 + "' align='center' colspan='2' valign='top'><table width='" + regDiv + "' cellspacing='0' cellpadding='0'  bordercolor='#cccccc' border='1' bordercolordark='#ffffff' height='30'><tbody><tr><td align='center'><a href='MemberNetMap.aspx?net="+strAzTj+"&amp;SelectGrass=3&amp;bianhao=" + bh + "'><font color='#000'> 注册 </font></a></td></tr></tbody></table></td></tr></tbody></table>");
        }


        return sb.ToString();

    }

    public static string GetFirst(DataTable dt, string bh)
    {
        StringBuilder sb2 = new StringBuilder();


        DataRow[] row;

        row = dt.Select("number='" + bh + "'", "Xuhao");

        DataRow[] row2;

        row2 = dt.Select("cengshu=2", "Xuhao");//第二层

        string toWidth = (Convert.ToInt32(row[row.Length - 1]["LeftDistance"].ToString()) + Convert.ToInt32(row[row.Length - 1]["Lenth"].ToString())).ToString();

        sb2.Append("<table width='" + toWidth + "' cellspacing='0' cellpadding='0'><tr>");

       
        int tableWidth = Convert.ToInt32(row[0]["Lenth"].ToString());//总余新宽度
        int tPLeft = Convert.ToInt32(row[0]["Said"].ToString());//距离左边的宽度
        int leftWidth = tPLeft + tableWidth / 2;

        //if (row2.Length==1)
        //{
            sb2.Append("<td width='" + tPLeft + "'><table width='100%'><tbody><tr><td>&nbsp;</td></tr></tbody></table></td><td width='" + tableWidth + "' align='center'>" + GetOneTable(dt, bh) + "</td></tr>");

            sb2.Append("<tr><td width='" + tPLeft + "'><table width='100%'><tbody><tr><td>&nbsp;</td></tr></tbody></table></td><td width='" + (Convert.ToInt32(toWidth) - tPLeft) + "'><table width='100%' cellspacing='0' cellpadding='0' border='0' ><tbody><tr><td width='" + (tableWidth / 2 + 1) + "' height=\"18\"></td><td width=\"2\" background=\"images/images02_02.gif\"  > </td><td width='" + ((tableWidth / 2 - 1)) + "'></td></tr></tbody></table></td>");
            sb2.Append("</tr></table>");
        //}
        //else
        //{
        //    sb2.Append("<td width='" + (toWidth) + "' align='center'>" + GetOneTable(dt, bh) + "</td></tr>");

        //    sb2.Append("<tr><td width='" + (toWidth) + "'><table width='100%' cellspacing='0' cellpadding='0' border='0' ><tbody><tr><td width='" + leftWidth + "' height=\"18\"></td><td width=\"2\" background=\"images/images02_02.gif\"  > </td><td width='" + (tableWidth + tPLeft * 2 - leftWidth) + "'></td></tr></tbody></table></td>");
        //    sb2.Append("</tr></table>");
        //}


        

        return sb2.ToString();


    }

    public static string GetTwo(DataTable dt)
    {
        StringBuilder sb2 = new StringBuilder();

        DataRow[] row;

        row = dt.Select("cengshu=2", "Xuhao");//第二层

        int firstWidth = 0;

        int endWidth = 0;

        int w2 = 0;

        int firstPLeft = 0;

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
            int leftWidth = firstWidth / 2 + firstPLeft-1;

            int rightWidth = endWidth / 2-1;

            int midWidth = Convert.ToInt32(toWidth) - leftWidth - rightWidth + 2;

            sb2.Append("<td  width='100%'><table width='100%' cellspacing='0' cellpadding='0' border='0' ><tbody><tr><td width='" + leftWidth + "' height=\"2\"></td><td width='" + midWidth + "' background=\"images/images03_05.gif\"  > </td><td width='" + rightWidth + "'></td></tr></tbody></table></td>");
        }
        else
        {
            sb2.Append("<td  width='100%'><table width='100%' cellspacing='0' cellpadding='0' border='0' ><tbody><tr><td width='100%' height=\"2\" colspan='3'></td></tr></tbody></table></td>");
        }


        sb2.Append("</tr></table>");

        DataRow[] row3;

        row3 = dt.Select("cengshu=3", "Xuhao");//第三层

        

        //画第二层竖线和总余新同时画第三层第一条竖线
        sb2.Append("<table width='" + toWidth + "' cellspacing='0' cellpadding='0'><tr>");

        if (row3.Length > 0)
        {

       

        for (int i = 0; i < row.Length; i++)
        {
            firstWidth = Convert.ToInt32(row[i]["Lenth"].ToString());
            firstPLeft = Convert.ToInt32(row[i]["Said"].ToString());

            int leftWidth = firstWidth / 2  - 1;

            int rightWidth = firstWidth / 2 - 1;

            sb2.Append("<td width='" + (firstWidth + firstPLeft) + "'><table width='100%' cellspacing='0' cellpadding='0' border='0' ><tbody><tr><td width='" + firstPLeft + "'><table width='100%'><tbody><tr><td>&nbsp;</td></tr></tbody></table></td><td width='" + firstWidth + "'><table width='100%' cellspacing='0' cellpadding='0' border='0' ><tbody><tr><td width='" + leftWidth + "' height=\"18\"></td><td width=\"2\" background=\"images/images02_02.gif\"  > </td><td width='" + rightWidth + "'></td></tr></tbody></table></td></tr><tr><td width='" + firstPLeft + "'><table width='100%'><tbody><tr><td>&nbsp;</td></tr></tbody></table></td><td width='" + firstWidth + "'>" + GetOneTable(dt, row[i]["number"].ToString()) + "</td></tr><tr><td width='" + firstPLeft + "'><table width='100%'><tbody><tr><td>&nbsp;</td></tr></tbody></table></td><td width='" + firstWidth + "'><table width='100%' cellspacing='0' cellpadding='0' border='0' ><tbody><tr><td width='" + leftWidth + "' height=\"18\"></td><td width=\"2\" background=\"images/images02_02.gif\"  > </td><td width='" + rightWidth + "'></td></tr></tbody></table></td></tr></tbody></table></td>");

        }

        }
        else
        {
            for (int i = 0; i < row.Length; i++)
            {
                firstWidth = Convert.ToInt32(row[i]["Lenth"].ToString());
                firstPLeft = Convert.ToInt32(row[i]["Said"].ToString());

                int leftWidth = firstWidth / 2 - 1;

                int rightWidth = firstWidth / 2 - 1;

                sb2.Append("<td width='" + (firstWidth + firstPLeft) + "'><table width='100%' cellspacing='0' cellpadding='0' border='0' ><tbody><tr><td width='" + firstPLeft + "'><table width='100%'><tbody><tr><td>&nbsp;</td></tr></tbody></table></td><td width='" + firstWidth + "'><table width='100%' cellspacing='0' cellpadding='0' border='0' ><tbody><tr><td width='" + leftWidth + "' height=\"18\"></td><td width=\"2\" background=\"images/images02_02.gif\"  > </td><td width='" + rightWidth + "'></td></tr></tbody></table></td></tr><tr><td width='" + firstPLeft + "'><table width='100%'><tbody><tr><td>&nbsp;</td></tr></tbody></table></td><td width='" + firstWidth + "'>" + GetOneTable(dt, row[i]["number"].ToString()) + "</td>");

            }
        }

        sb2.Append("</tr></table>");


        return sb2.ToString();

    }


    public static string GetThree(DataTable dt)
    {
        StringBuilder sb2 = new StringBuilder();

        DataRow[] row3;

        row3 = dt.Select("cengshu=3", "Xuhao");//第三层

        DataRow[] row2;

        row2 = dt.Select("cengshu=2", "Xuhao");//第二层

        DataRow[] rowTeam;//第三层团队

        int firstWidth = 0;

        int endWidth = 0;

        int w2 = 0;

        int firstPLeft = 0;

        int toWidth = JieGouNew.GetFinalWidth(dt);

        int midWidth = 0;

        int leftWidth = 0;

        int rightWidth = 0;

        int toWidth2 = 0;

        if (row2.Length > 0)
        {
            toWidth2 = Convert.ToInt32(row2[row2.Length - 1]["LeftDistance"].ToString()) + Convert.ToInt32(row2[row2.Length - 1]["Lenth"].ToString());
        }

        //画横线

        if (toWidth2 > toWidth)
        {
            sb2.Append("<table width='" + toWidth2 + "' cellspacing='0' cellpadding='0'><tr>");
        }
        else
        {
            sb2.Append("<table width='" + toWidth + "' cellspacing='0' cellpadding='0'><tr>");
        }

        for (int i = 0; i < row2.Length; i++)
        {
            rowTeam = dt.Select("shangji='" + row2[i]["number"] + "'", "Xuhao");

            w2=0;

            for (int k = 0; k < rowTeam.Length; k++)
            {
                firstPLeft = Convert.ToInt32(rowTeam[k]["Said"].ToString());

                if (firstPLeft == 0)
                {
                    firstPLeft = jianJu;
                }

                w2 += firstPLeft + Convert.ToInt32(rowTeam[k]["Lenth"].ToString());

                
                    if (k==0)
	                {
                        firstWidth = Convert.ToInt32(rowTeam[k]["Lenth"].ToString());
	                }

                    if (k==rowTeam.Length-1)
                    {
                        endWidth = Convert.ToInt32(rowTeam[k]["Lenth"].ToString());
                    }
                    
                
            }

            leftWidth = firstPLeft + firstWidth / 2;

            rightWidth = endWidth / 2;

            midWidth = w2 - leftWidth - rightWidth;

            if (rowTeam.Length > 1)
            {
                sb2.Append("<td  width='" +  w2 + "'><table width='100%' cellspacing='0' cellpadding='0' border='0' ><tbody><tr><td width='" + leftWidth + "' height=\"2\"></td><td width='" + midWidth + "' background=\"images/images03_05.gif\"  > </td><td width='" + rightWidth + "'></td></tr></tbody></table></td>");
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
        sb2.Append("<table width='100%'cellspacing='0' cellpadding='0'><tr>");

        for (int i = 0; i < row2.Length; i++)
        {
            rowTeam = dt.Select("shangji='" + row2[i]["number"] + "'", "Xuhao");

            w2 = 0;

            for (int k = 0; k < rowTeam.Length; k++)
            {
                firstWidth = Convert.ToInt32(rowTeam[k]["Lenth"].ToString());
                firstPLeft = Convert.ToInt32(rowTeam[k]["Said"].ToString());

                if (firstPLeft==0)
                {
                    firstPLeft = jianJu;
                }

                leftWidth = firstWidth / 2-1;

                rightWidth = firstWidth / 2-1;

                sb2.Append("<td width='" + (firstWidth + firstPLeft) + "' valign='top'>");
                  sb2.Append("<table width='100%' cellspacing='0' cellpadding='0' border='0' >");
                    sb2.Append("<tbody>");
                    sb2.Append("<tr><td width='" + firstPLeft + "'><table width='100%'><tbody><tr><td>&nbsp;</td></tr></tbody></table></td><td width='" + firstWidth + "'><table width='100%' cellspacing='0' cellpadding='0' border='0' ><tbody><tr><td width='" + leftWidth + "' height=\"18\"></td><td width=\"2\" background=\"images/images02_02.gif\"  > </td><td width='" + rightWidth + "'></td></tr></tbody></table></td></tr>");
       
               
                    sb2.Append("<tr><td width='" + firstPLeft + "' ><table width='100%'><tbody><tr><td>&nbsp;</td></tr></tbody></table></td><td width='" + firstWidth + "'>" + GetOneTable(dt, rowTeam[k]["number"].ToString()) + "</td></tr>");

                    

                sb2.Append("</tbody></table></td>");
            }
        }
        

        sb2.Append("</tr></table>");


        return sb2.ToString();

    }

    //计算总宽度
    public static int GetFinalWidth(DataTable dt)
    {
        DataRow[] row;

        row = dt.Select("cengshu=3", "Xuhao");//第三层

        //int firstWidth = 0;

        //int firstPLeft = 0;

        //int totalWidth = 0;

        //for (int i = 0; i < row.Length; i++)
        //{
        //    firstWidth = Convert.ToInt32(row[i]["Lenth"].ToString());
        //    firstPLeft = Convert.ToInt32(row[i]["Said"].ToString());

        //    totalWidth += firstWidth + firstPLeft;
        //}

        //return totalWidth + jianJu;

        if (row.Length==0)
        {
            row = dt.Select("cengshu=2", "Xuhao");//第二层

            if (row.Length==0)
            {
                row = dt.Select("cengshu=1", "Xuhao");//第一层
                return Convert.ToInt32(row[row.Length - 1]["LeftDistance"].ToString()) + Convert.ToInt32(row[row.Length - 1]["Lenth"].ToString());
            }
            else
            {
                return Convert.ToInt32(row[row.Length - 1]["LeftDistance"].ToString()) + Convert.ToInt32(row[row.Length - 1]["Lenth"].ToString());
            }
        }

        return Convert.ToInt32(row[row.Length - 1]["LeftDistance"].ToString()) + Convert.ToInt32(row[row.Length - 1]["Lenth"].ToString());

    }

    public static string GetAll(DataTable dt,string bh)
    {
        string strAll = "";

        //+ GetThree(dt)

        strAll = GetFirst(dt, bh) + GetTwo(dt) + GetThree(dt);

        

        //strAll = GetThree(dt);

        return strAll;

    
    }

    //1:安置；2:推荐
    public static string Direct_Table_New(string bh,int qs,int isAnTj)
    {
        if (isAnTj==1)
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

        DataTable dtNew = DAL.DBHelper.ExecuteDataTable("ShowNet", spa, CommandType.StoredProcedure);

        string toWidth = (JieGouNew.GetFinalWidth(dtNew)).ToString();

        return "<table align='center' width='" + toWidth + "' style='margin-left:20px;'><tr><td align='left'>" + JieGouNew.GetAll(dtNew, bh) + "<td/><tr></table>";
    
    }


}
