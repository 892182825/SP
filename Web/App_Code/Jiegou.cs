using System;
using System.Data.SqlClient;
using System.Data;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using Microsoft.Office.Interop.Owc11;
using System.IO;
using Microsoft.Office.Interop;
using OWC11 = Microsoft.Office.Interop.Owc11;
using System.Text;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Collections;
using System.Web.UI.WebControls;
using BLL.CommonClass;
using DAL;
using BLL.other.Company;
using BLL.other.Member;
using BLL.Registration_declarations;
using BLL.other;
using System.Xml;
using BLL.MoneyFlows;
using BLL.Logistics;
using Model.Other;
using System.Web;
using Model;
using System.Collections.Generic;

/// <summary>
///Jiegou 的摘要说明
/// </summary>
public class Jiegou : BLL.TranslationBase
{
    public Jiegou()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    /// <summary>
    /// 常用推荐网络图
    /// </summary>
    /// <param name="Div_TuiJian">ＤＩＶ</param>
    /// <param name="TopBianhao">首点编号</param>
    /// <param name="ExpectNum">期数</param>
    /// <returns>返回Table形式的字符串</returns>
    public static void Direct_Table(System.Web.UI.HtmlControls.HtmlGenericControl Div_TuiJian, string TopBianhao, string qishu, bool isAnZhi)
    {
        string temp = string.Empty;//字段（推荐编号或安置编号）
        temp = isAnZhi == true ? "Placement" : "Direct";

        string str = "select b.number,a.level,b.ExpectNum,b.Direct  from MemberInfoBalance" + qishu + " a,memberInfo b where a.number=b.number";
        DataTable dt = DAL.DBHelper.ExecuteDataTable(str);

        DataTable dt_tuijian;
        DataTable dt_tuijian1;
        DataRow[] row;
        int div_count = 0;
        int count = 0;
        int i = 0;
        int p = 0;
        int k = 0;
        int jb = 0;

        string jibie = "";
        string colors = "";
        string sql = "";
        string tuijian = "";

        if (TopBianhao == "")
        {
            tuijian = DAL.DBHelper.ExecuteScalar("select number from manage where defaultmanager = 1").ToString();
        }
        else
            tuijian = TopBianhao;

        sql = "select  *  from   memberInfo   where  " + temp + "='" + tuijian + "' and  ExpectNum<='" + qishu + "' ";
        dt_tuijian = DAL.DBHelper.ExecuteDataTable(sql);
        count = dt_tuijian.Rows.Count;

        div_count += count;
        for (i = 0; i < dt_tuijian.Rows.Count; i++)
        {
            dt_tuijian1 = DAL.DBHelper.ExecuteDataTable("select *  from  memberInfo  where  " + temp + "='" + dt_tuijian.Rows[i]["number"] + "' and  ExpectNum<='" + qishu + "' and  Direct<> (select top 1 number from manage where defaultmanager = 1)");
            div_count += dt_tuijian1.Rows.Count;
        }

        string[] bianhaohtml = new string[div_count + 10];
        string[] jibiehtml = new string[div_count + 10];
        string[] color = new string[div_count + 10];
        for (p = 0; p < div_count; p++)
        {
            bianhaohtml[p] = "&nbsp;";
            jibiehtml[p] = "&nbsp";
            color[p] = "#b3b3b3";
        }
        BLL.CommonClass.CommonDataBLL commonDataBLL = new BLL.CommonClass.CommonDataBLL();
        #region 一层
        row = dt.Select("number='" + TopBianhao + "'", "number");
        if (row.Length > 0)
        {
            if (row[0]["number"].ToString().Trim() == "") { jibie = ""; }
            else
            {
                jb = row[0]["level"] == System.DBNull.Value ? 0 : int.Parse(row[0]["level"].ToString());
                //jb = row[0]["level"] == System.DBNull.Value ? 0 : int.Parse(row[0]["level"].ToString());
                jibie = GetLevel(jb.ToString(), 0);
            }

            colors = "#00cc00";
            if (row[0]["ExpectNum"].ToString() == CommonDataBLL.getMaxqishu().ToString())
            {
                colors = "red";
            }
            if (isAnZhi)
                bianhaohtml[0] = "<a href=MemberNetMap.aspx?net=az&SelectGrass=" + qishu + "&bianhao=" + row[0]["number"].ToString() + "><font color=#333333>" + row[0]["number"].ToString() + "</font></a>";
            else
                bianhaohtml[0] = "<a href=MemberNetMap.aspx?net=tj&SelectGrass=" + qishu + "&bianhao=" + row[0]["number"].ToString() + "><font color=#333333>" + row[0]["number"].ToString() + "</font></a>";
            jibiehtml[0] = jibie;
            color[0] = colors;
        }
        #endregion
        #region 二层
        for (i = 0; i < dt_tuijian.Rows.Count; i++)
        {
            string helper = "number='" + dt_tuijian.Rows[i]["number"] + "'";
            row = dt.Select(helper, "number");
            if (row.Length > 0)
            {

                if (row[0]["number"].ToString().Trim() == "") { jibie = ""; }
                else
                {
                    string helper2 = "" + row[0]["level"];
                    jb = row[0]["level"] == System.DBNull.Value ? 0 : int.Parse(row[0]["level"].ToString());
                    //jb = row[0]["level"] == System.DBNull.Value ? 0 : int.Parse(row[0]["level"].ToString());
                    jibie = GetLevel(jb.ToString(), 0);
                }
                colors = "#00cc00";
                if (row[0]["ExpectNum"].ToString() == CommonDataBLL.getMaxqishu().ToString())
                {
                    colors = "red";
                }
                string helper33 = row[0]["number"] + "";
                if (isAnZhi)
                    bianhaohtml[i + 1] = "<a href=MemberNetMap.aspx?net=az&SelectGrass=" + qishu + "&bianhao=" + row[0]["number"].ToString() + "><font color=#333333>" + row[0]["number"].ToString() + "</font></a>";
                else
                    bianhaohtml[i + 1] = "<a href=MemberNetMap.aspx?net=tj&SelectGrass=" + qishu + "&bianhao=" + row[0]["number"].ToString() + "><font color=#333333>" + row[0]["number"].ToString() + "</font></a>";
                jibiehtml[i + 1] = jibie;
                color[i + 1] = colors;
            }
        }
        #endregion

        string strhtml1 = "";
        int _count1 = dt_tuijian.Rows.Count;//第二层线的个数(上线推荐的个数)
        int _count2 = 0;
        int width1 = 0;                     //第二层线的宽度
        int _count3 = 0;
        int width3 = 0;
        int width4 = 0;
        int _count4 = 0;
        for (i = 0; i < _count1; i++)
        {
            if (_count1 > 1)
            {
                dt_tuijian1 = DAL.DBHelper.ExecuteDataTable("select *  from  memberInfo  where  " + temp + "='" + dt_tuijian.Rows[i]["number"] + "' and  ExpectNum<='" + qishu + "'");
                _count4 = dt_tuijian1.Rows.Count;
                if (_count4 > 1)
                {
                    _count2 += _count4;
                    _count3 += _count4;
                }
                else
                {
                    _count2++;
                    _count3++;
                }
            }
            else
                _count2 = _count1;

            if (i == 0)
            {
                //width3第二层线头部的宽度
                if (_count4 <= 1)
                    width3 = 50;
                else
                    width3 = _count4 * 50;
            }
            if (i == (_count1 - 1))
            {
                //width4第二层线头部的宽度
                if (_count4 <= 1)
                    width4 = 50;
                else
                    width4 = _count4 * 50;
            }
        }
        width1 = _count2 * 100 - width3 - width4;//第二层线中间部分的宽度


        strhtml1 = "<table  border=\"0\" cellspacing=\"0\" cellpadding=\"0\"> " +
                    "<tr align=\"center\" valign=\"top\">" +
                        "<td height=\"63\" colspan=\"" + _count1 + "\" align=\"center\">" +
                            "<table width=\"100\" height=\"63\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">" +
                                "<tr>" +
                                    "<td height=\"47\" colspan=\"3\" valign=\"top\">" +
                                        "<TABLE  borderColor=#CCCCCC cellSpacing=0  borderColorDark=#ffffff cellPadding=0 height=100%  width=100 border=1>" +
                                            "<TR>" +
                                                "<TD align=\"center\" bgColor=" + color[0] + " colSpan=\"2\" height=\"20\">" + bianhaohtml[0] + "</TD>" +
                                            "</TR>" +
                                            "<TR>" +
                                                "<TD align=\"center\" bgColor=\"#ffffcc\" colSpan=\"2\" height=\"20\"><font size=2px><img height='18' src='" + jibiehtml[0] + "'></font></TD>" +//" + jibiehtml[0] + "
                                            "</TR>" +
                                            "<TR>" +
                                                "<TD align=\"center\" colSpan=\"2\" height=\"17\"><IMG height=\"18\" src=\"images/00.gif\" width=\"18\"></TD>" +
                                            "</TR>" +
                                        "</TABLE>" +
                                    "</td>" +
                                "</tr>";
        if (_count1 > 0)
        {
            strhtml1 += "<tr>" +
                           "<td width=\"49\" height=\"16\"></td>" +
                           "<td width=\"2\" background=\"images/images02_02.gif\"  > </td>" +
                           "<td width=\"49\"></td>" +
                       "</tr>";

        }
        strhtml1 += "</table>" +
                "</td>" +
            "</tr>";
        if (_count1 > 0)
        {
            if (_count2 > 1)
            {
                strhtml1 += "<tr align=\"center\" valign=\"top\">" +
                            "<td colspan=\"" + _count1 + "\" height=\"2\" >" +
                                "<table  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" +
                                    "<tr>" +
                                        "<td height=\"2\"  width=\"" + width3 + "\" ></td>" +
                                        "<td height=\"2\"  width=\"" + width1 + "\"  background=\"images/images03_05.gif\"></td>" +
                                        "<td height=\"2\"  width=\"" + width4 + "\"  ></td>" +
                                    "</tr>" +
                                "</table>" +
                            "</td>" +
                        "</tr>";
            }
            else
            {

                strhtml1 += "<tr align=\"center\" valign=\"top\">" +
                            "<td colspan=\"" + _count1 + "\" height=\"2\" align=\"center\">" +
                                "<table  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" +
                                    "<tr>" +
                                        "<td height=\"2\" width=\"2\"  background=\"images/images03_05.gif\"></td>" +
                                    "</tr>" +
                                "</table>" +
                            "</td>" +
                        "</tr>";
            }
        }

        strhtml1 += "<tr valign=\"top\">";

        int t = 0;
        int _count = 0;
        int width2 = 0;
        for (i = 0; i < dt_tuijian.Rows.Count; i++)
        {
            dt_tuijian1 = DAL.DBHelper.ExecuteDataTable("select *  from  memberInfo  where  " + temp + "='" + dt_tuijian.Rows[i]["number"] + "'  and  ExpectNum<='" + qishu + "'");
            _count = dt_tuijian1.Rows.Count;
            width2 = (_count - 1) * 100;//第三层线的宽度
            strhtml1 += "<td  align=\"center\">" +
                        "<table  border=\"0\" cellspacing=\"0\" cellpadding=\"0\" >" +
                            "<tr align=\"center\" valign=\"top\">" +
                                "<td height=\"79\" colspan=\"3\">" +
                                    "<table  height=\"79\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">" +
                                        "<tr>" +
                                            "<td width=\"49\" height=\"16\"></td>" +
                                            "<td width=\"2\"  background=\"images/images02_02.gif\"></td>" +
                                            "<td width=\"49\"></td>" +
                                        "</tr>" +
                                        "<tr>" +
                                            "<td height=\"47\" colspan=\"3\" valign=\"top\">" +
                                                 "<TABLE  borderColor=#CCCCCC cellSpacing=0  borderColorDark=#ffffff cellPadding=0 height=100%  width=100 border=1>" +
                                                    "<TR>" +
                                                        "<TD align=\"center\" bgColor=" + color[i + 1] + " colSpan=\"2\" height=\"20\">" + bianhaohtml[i + 1] + "</TD>" +
                                                    "</TR>" +
                                                    "<TR>" +
                                                        "<TD align=\"center\" bgColor=\"#ffffcc\" colSpan=\"2\" height=\"20\"><font size=2px><img height='18' src='" + jibiehtml[i + 1] + "'></font></TD>" +
                                                    "</TR>" +
                                                    "<TR>" +
                                                        "<TD align=\"center\" colSpan=\"2\" height=\"17\"><IMG height=\"18\" src=\"images/00.gif\" width=\"18\"></TD>" +
                                                    "</TR>" +
                                                "</TABLE>" +
                                            "</td>" +
                                        "</tr>";
            if (_count > 0)
            {
                strhtml1 += "<tr>" +
                                "<td width=\"49\" height=\"16\"></td>" +
                                "<td width=\"2\"  background=\"images/images02_02.gif\"></td>" +
                                "<td width=\"49\"></td>" +
                            "</tr>";
            }

            strhtml1 += "</table>" +
                    "</td>" +
                 "</tr>";

            if (_count > 0)
            {
                if (_count == 1)
                {
                    strhtml1 += "<tr>" +
                                "<td width=\"49\" height=\"2\"></td>" +
                                "<td background=\"images/images03_05.gif\"></td>" +
                                "<td width=\"49\"></td>" +
                                "</tr>";
                }
                else
                {
                    strhtml1 += "<tr>" +
                                "<td width=\"49\" height=\"2\"></td>" +
                                "<td width=\"" + width2 + "\" background=\"images/images03_05.gif\"></td>" +
                                "<td width=\"49\"></td>" +
                                "</tr>";
                }
                strhtml1 += "<tr vAlign=\"top\">" +
                             "<td colSpan=\"3\" align=\"center\">" +
                                "<table cellSpacing=\"0\" cellPadding=\"0\"  border=\"0\">" +
                                    "<tr vAlign=\"top\">";
            }
            //if(t==1)
            //count+=dt_tuijian1.Rows.Count;
            for (int j = 0; j < dt_tuijian1.Rows.Count; j++)
            {
                row = dt.Select("number='" + dt_tuijian1.Rows[j]["number"] + "'", "number");
                if (row.Length > 0)
                {
                    jb = row[0]["level"] == System.DBNull.Value ? 0 : int.Parse(row[0]["level"].ToString());
                    jibie = GetLevel(jb.ToString(), 0);
                    colors = "#00cc00";
                    if (row[0]["number"].ToString().Trim() == "") { jibie = ""; }
                    if (row[0]["ExpectNum"].ToString() == CommonDataBLL.getMaxqishu().ToString())
                    {
                        colors = "red";
                    }
                    if (isAnZhi)
                        bianhaohtml[count + 1 + j] = "<a href=MemberNetMap.aspx?net=az&SelectGrass=" + qishu + "&bianhao=" + row[0]["number"].ToString() + "><font color=#333333>" + row[0]["number"].ToString() + "</font></a>";
                    else
                        bianhaohtml[count + 1 + j] = "<a href=MemberNetMap.aspx?net=tj&SelectGrass=" + qishu + "&bianhao=" + row[0]["number"].ToString() + "><font color=#333333>" + row[0]["number"].ToString() + "</font></a>";
                    jibiehtml[count + 1 + j] = jibie;
                    color[count + 1 + j] = colors;

                    strhtml1 += "<td  height=\"63\" align=\"center\">" +
                                    "<table cellSpacing=\"0\" cellPadding=\"0\"  border=\"0\">" +
                                        "<tr>" +
                                            "<td width=\"49\" height=\"16\"></td>" +
                                            "<td width=\"2\" background=\"images/images02_02.gif\"></td>" +
                                            "<td width=\"49\" ></td>" +
                                        "</tr>" +
                                        "<tr>" +
                                            "<td vAlign=\"top\" colSpan=\"3\" height=\"47\">" +
                                                "<TABLE  borderColor=#CCCCCC cellSpacing=0  borderColorDark=#ffffff cellPadding=0 height=100%  width=100 border=1>" +
                                                    "<TR>" +
                                                        "<TD align=\"center\" bgColor=" + colors + " colSpan=\"2\" height=\"20\">" + bianhaohtml[count + 1 + j] + "</TD>" +
                                                    "</TR>" +
                                                    "<TR>" +
                                                        "<TD align=\"center\" bgColor=\"#ffffcc\" colSpan=\"2\" height=\"20\"><font size=2px><img height='18' src='" + jibie + "'></font></TD>" +
                                                    "</TR>" +
                                                    "<TR>" +
                                                        "<TD align=\"center\" colSpan=\"2\" height=\"17\"><IMG height=\"18\" src=\"images/00.gif\" width=\"18\"></TD>" +
                                                    "</TR>" +
                                                "</TABLE>" +
                                            "</td>" +
                                        "</tr>" +
                                    "</table>" +
                                "</td>";
                }

            }
            t = 1;
            if (_count > 0)
            {
                strhtml1 += "</tr>" +
                    "</table>" +
                    "</td>" +
                    "</tr>";
            }

            strhtml1 += "</table>" +
                       "</td>";


        }
        strhtml1 += "</td>" +
                  "</tr>" +
                  "</table>" +
                  "</td>";
        Div_TuiJian.InnerHtml = strhtml1 + "</tr></table>";
    }


    public static void Direct_TableMember(System.Web.UI.HtmlControls.HtmlGenericControl Div_TuiJian, string TopBianhao, string qishu, bool isAnZhi)
    {
        string temp = string.Empty;//字段（推荐编号或安置编号）
        temp = isAnZhi == true ? "Placement" : "Direct";

        string str = "select b.number,a.level,b.ExpectNum,b.Direct  from MemberInfoBalance" + qishu + " a,memberInfo b where a.number=b.number";
        DataTable dt = DAL.DBHelper.ExecuteDataTable(str);

        DataTable dt_tuijian;
        DataTable dt_tuijian1;
        DataRow[] row;
        int div_count = 0;
        int count = 0;
        int i = 0;
        int p = 0;
        int k = 0;
        int jb = 0;

        string jibie = "";
        string colors = "";
        string sql = "";
        string tuijian = "";

        if (TopBianhao == "")
        {
            tuijian = DAL.DBHelper.ExecuteScalar("select number from manage where defaultmanager = 1").ToString();
        }
        else
            tuijian = TopBianhao;

        sql = "select  *  from   memberInfo   where  " + temp + "='" + tuijian + "' and  ExpectNum<='" + qishu + "' ";
        dt_tuijian = DAL.DBHelper.ExecuteDataTable(sql);
        count = dt_tuijian.Rows.Count;

        div_count += count;
        for (i = 0; i < dt_tuijian.Rows.Count; i++)
        {
            dt_tuijian1 = DAL.DBHelper.ExecuteDataTable("select *  from  memberInfo  where  " + temp + "='" + dt_tuijian.Rows[i]["number"] + "' and  ExpectNum<='" + qishu + "' and  Direct<> (select top 1 number from manage where defaultmanager = 1)");
            div_count += dt_tuijian1.Rows.Count;
        }

        string[] bianhaohtml = new string[div_count + 10];
        string[] jibiehtml = new string[div_count + 10];
        string[] color = new string[div_count + 10];
        for (p = 0; p < div_count; p++)
        {
            bianhaohtml[p] = "&nbsp;";
            jibiehtml[p] = "&nbsp";
            color[p] = "#b3b3b3";
        }
        BLL.CommonClass.CommonDataBLL commonDataBLL = new BLL.CommonClass.CommonDataBLL();
        #region 一层
        row = dt.Select("number='" + TopBianhao + "'", "number");
        if (row.Length > 0)
        {
            if (row[0]["number"].ToString().Trim() == "") { jibie = ""; }
            else
            {
                jb = row[0]["level"] == System.DBNull.Value ? 0 : int.Parse(row[0]["level"].ToString());
                //jb = row[0]["level"] == System.DBNull.Value ? 0 : int.Parse(row[0]["level"].ToString());
                jibie = GetLevel(jb.ToString(), 0);
            }

            colors = "#00cc00";
            if (row[0]["ExpectNum"].ToString() == CommonDataBLL.getMaxqishu().ToString())
            {
                colors = "red";
            }
            if (isAnZhi)
                bianhaohtml[0] = "<a href='#')\"><font color=#333333>" + row[0]["number"].ToString() + "</font></a>";
            else
                bianhaohtml[0] = "<a href='#')\"><font color=#333333>" + row[0]["number"].ToString() + "</font></a>";
            jibiehtml[0] = jibie;
            color[0] = colors;
        }
        #endregion
        #region 二层
        for (i = 0; i < dt_tuijian.Rows.Count; i++)
        {
            string helper = "number='" + dt_tuijian.Rows[i]["number"] + "'";
            row = dt.Select(helper, "number");
            if (row.Length > 0)
            {

                if (row[0]["number"].ToString().Trim() == "") { jibie = ""; }
                else
                {
                    string helper2 = "" + row[0]["level"];
                    jb = row[0]["level"] == System.DBNull.Value ? 0 : int.Parse(row[0]["level"].ToString());
                    //jb = row[0]["level"] == System.DBNull.Value ? 0 : int.Parse(row[0]["level"].ToString());
                    jibie = GetLevel(jb.ToString(), 0);
                }
                colors = "#00cc00";
                if (row[0]["ExpectNum"].ToString() == CommonDataBLL.getMaxqishu().ToString())
                {
                    colors = "red";
                }
                string helper33 = row[0]["number"] + "";
                if (isAnZhi)
                    bianhaohtml[i + 1] = "<a href='#' onclick=\"ShowView('az'," + qishu + ",'" + row[0]["number"].ToString() + "',2)\"><font color=#333333>" + row[0]["number"].ToString() + "</font></a>";
                else
                    bianhaohtml[i + 1] = "<a href='#' onclick=\"ShowView('tj'," + qishu + ",'" + row[0]["number"].ToString() + "',2)\"><font color=#333333>" + row[0]["number"].ToString() + "</font></a>";
                jibiehtml[i + 1] = jibie;
                color[i + 1] = colors;
            }
        }
        #endregion

        string strhtml1 = "";
        int _count1 = dt_tuijian.Rows.Count;//第二层线的个数(上线推荐的个数)
        int _count2 = 0;
        int width1 = 0;                     //第二层线的宽度
        int _count3 = 0;
        int width3 = 0;
        int width4 = 0;
        int _count4 = 0;
        for (i = 0; i < _count1; i++)
        {
            if (_count1 > 1)
            {
                dt_tuijian1 = DAL.DBHelper.ExecuteDataTable("select *  from  memberInfo  where  " + temp + "='" + dt_tuijian.Rows[i]["number"] + "' and  ExpectNum<='" + qishu + "'");
                _count4 = dt_tuijian1.Rows.Count;
                if (_count4 > 1)
                {
                    _count2 += _count4;
                    _count3 += _count4;
                }
                else
                {
                    _count2++;
                    _count3++;
                }
            }
            else
                _count2 = _count1;

            if (i == 0)
            {
                //width3第二层线头部的宽度
                if (_count4 <= 1)
                    width3 = 50;
                else
                    width3 = _count4 * 50;
            }
            if (i == (_count1 - 1))
            {
                //width4第二层线头部的宽度
                if (_count4 <= 1)
                    width4 = 50;
                else
                    width4 = _count4 * 50;
            }
        }
        width1 = _count2 * 100 - width3 - width4;//第二层线中间部分的宽度


        strhtml1 = "<table  border=\"0\" cellspacing=\"0\" cellpadding=\"0\"> " +
                    "<tr align=\"center\" valign=\"top\">" +
                        "<td height=\"63\" colspan=\"" + _count1 + "\" align=\"center\">" +
                            "<table width=\"100\" height=\"63\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">" +
                                "<tr>" +
                                    "<td height=\"47\" colspan=\"3\" valign=\"top\">" +
                                        "<TABLE  borderColor=#CCCCCC cellSpacing=0  borderColorDark=#ffffff cellPadding=0 height=100%  width=100 border=1>" +
                                            "<TR>" +
                                                "<TD align=\"center\" bgColor=" + color[0] + " colSpan=\"2\" height=\"20\">" + bianhaohtml[0] + "</TD>" +
                                            "</TR>" +
                                            "<TR>" +
                                                "<TD align=\"center\" bgColor=\"#ffffcc\" colSpan=\"2\" height=\"20\"><font size=2px><img height='18' src='" + jibiehtml[0] + "'></font></TD>" +//" + jibiehtml[0] + "
                                            "</TR>" +
                                            "<TR>" +
                                                "<TD align=\"center\" colSpan=\"2\" height=\"17\"><IMG height=\"18\" src=\"images/00.gif\" width=\"18\"></TD>" +
                                            "</TR>" +
                                        "</TABLE>" +
                                    "</td>" +
                                "</tr>";
        if (_count1 > 0)
        {
            strhtml1 += "<tr>" +
                           "<td width=\"49\" height=\"16\"></td>" +
                           "<td width=\"2\" background=\"images/images02_02.gif\"  > </td>" +
                           "<td width=\"49\"></td>" +
                       "</tr>";

        }
        strhtml1 += "</table>" +
                "</td>" +
            "</tr>";
        if (_count1 > 0)
        {
            if (_count2 > 1)
            {
                strhtml1 += "<tr align=\"center\" valign=\"top\">" +
                            "<td colspan=\"" + _count1 + "\" height=\"2\" >" +
                                "<table  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" +
                                    "<tr>" +
                                        "<td height=\"2\"  width=\"" + width3 + "\" ></td>" +
                                        "<td height=\"2\"  width=\"" + width1 + "\"  background=\"images/images03_05.gif\"></td>" +
                                        "<td height=\"2\"  width=\"" + width4 + "\"  ></td>" +
                                    "</tr>" +
                                "</table>" +
                            "</td>" +
                        "</tr>";
            }
            else
            {

                strhtml1 += "<tr align=\"center\" valign=\"top\">" +
                            "<td colspan=\"" + _count1 + "\" height=\"2\" align=\"center\">" +
                                "<table  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" +
                                    "<tr>" +
                                        "<td height=\"2\" width=\"2\"  background=\"images/images03_05.gif\"></td>" +
                                    "</tr>" +
                                "</table>" +
                            "</td>" +
                        "</tr>";
            }
        }

        strhtml1 += "<tr valign=\"top\">";

        int t = 0;
        int _count = 0;
        int width2 = 0;
        for (i = 0; i < dt_tuijian.Rows.Count; i++)
        {
            dt_tuijian1 = DAL.DBHelper.ExecuteDataTable("select *  from  memberInfo  where  " + temp + "='" + dt_tuijian.Rows[i]["number"] + "'  and  ExpectNum<='" + qishu + "'");
            _count = dt_tuijian1.Rows.Count;
            width2 = (_count - 1) * 100;//第三层线的宽度
            strhtml1 += "<td  align=\"center\">" +
                        "<table  border=\"0\" cellspacing=\"0\" cellpadding=\"0\" >" +
                            "<tr align=\"center\" valign=\"top\">" +
                                "<td height=\"79\" colspan=\"3\">" +
                                    "<table  height=\"79\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">" +
                                        "<tr>" +
                                            "<td width=\"49\" height=\"16\"></td>" +
                                            "<td width=\"2\"  background=\"images/images02_02.gif\"></td>" +
                                            "<td width=\"49\"></td>" +
                                        "</tr>" +
                                        "<tr>" +
                                            "<td height=\"47\" colspan=\"3\" valign=\"top\">" +
                                                 "<TABLE  borderColor=#CCCCCC cellSpacing=0  borderColorDark=#ffffff cellPadding=0 height=100%  width=100 border=1>" +
                                                    "<TR>" +
                                                        "<TD align=\"center\" bgColor=" + color[i + 1] + " colSpan=\"2\" height=\"20\">" + bianhaohtml[i + 1] + "</TD>" +
                                                    "</TR>" +
                                                    "<TR>" +
                                                        "<TD align=\"center\" bgColor=\"#ffffcc\" colSpan=\"2\" height=\"20\"><font size=2px><img height='18' src='" + jibiehtml[i + 1] + "'></font></TD>" +
                                                    "</TR>" +
                                                    "<TR>" +
                                                        "<TD align=\"center\" colSpan=\"2\" height=\"17\"><IMG height=\"18\" src=\"images/00.gif\" width=\"18\"></TD>" +
                                                    "</TR>" +
                                                "</TABLE>" +
                                            "</td>" +
                                        "</tr>";
            if (_count > 0)
            {
                strhtml1 += "<tr>" +
                                "<td width=\"49\" height=\"16\"></td>" +
                                "<td width=\"2\"  background=\"images/images02_02.gif\"></td>" +
                                "<td width=\"49\"></td>" +
                            "</tr>";
            }

            strhtml1 += "</table>" +
                    "</td>" +
                 "</tr>";

            if (_count > 0)
            {
                if (_count == 1)
                {
                    strhtml1 += "<tr>" +
                                "<td width=\"49\" height=\"2\"></td>" +
                                "<td background=\"images/images03_05.gif\"></td>" +
                                "<td width=\"49\"></td>" +
                                "</tr>";
                }
                else
                {
                    strhtml1 += "<tr>" +
                                "<td width=\"49\" height=\"2\"></td>" +
                                "<td width=\"" + width2 + "\" background=\"images/images03_05.gif\"></td>" +
                                "<td width=\"49\"></td>" +
                                "</tr>";
                }
                strhtml1 += "<tr vAlign=\"top\">" +
                             "<td colSpan=\"3\" align=\"center\">" +
                                "<table cellSpacing=\"0\" cellPadding=\"0\"  border=\"0\">" +
                                    "<tr vAlign=\"top\">";
            }
            //if(t==1)
            //count+=dt_tuijian1.Rows.Count;
            for (int j = 0; j < dt_tuijian1.Rows.Count; j++)
            {
                row = dt.Select("number='" + dt_tuijian1.Rows[j]["number"] + "'", "number");
                if (row.Length > 0)
                {
                    jb = row[0]["level"] == System.DBNull.Value ? 0 : int.Parse(row[0]["level"].ToString());
                    jibie = GetLevel(jb.ToString(), 0);
                    colors = "#00cc00";
                    if (row[0]["number"].ToString().Trim() == "") { jibie = ""; }
                    if (row[0]["ExpectNum"].ToString() == CommonDataBLL.getMaxqishu().ToString())
                    {
                        colors = "red";
                    }
                    if (isAnZhi)
                        bianhaohtml[count + 1 + j] = "<a href='#' onclick=\"ShowView('tj'," + qishu + ",'" + row[0]["number"].ToString() + "',1)\"><font color=#333333>" + row[0]["number"].ToString() + "</font></a>";
                    else
                        bianhaohtml[count + 1 + j] = "<a href='#' onclick=\"ShowView('tj'," + qishu + ",'" + row[0]["number"].ToString() + "',1)\"><font color=#333333>" + row[0]["number"].ToString() + "</font></a>";
                    jibiehtml[count + 1 + j] = jibie;
                    color[count + 1 + j] = colors;

                    strhtml1 += "<td  height=\"63\" align=\"center\">" +
                                    "<table cellSpacing=\"0\" cellPadding=\"0\"  border=\"0\">" +
                                        "<tr>" +
                                            "<td width=\"49\" height=\"16\"></td>" +
                                            "<td width=\"2\" background=\"images/images02_02.gif\"></td>" +
                                            "<td width=\"49\" ></td>" +
                                        "</tr>" +
                                        "<tr>" +
                                            "<td vAlign=\"top\" colSpan=\"3\" height=\"47\">" +
                                                "<TABLE  borderColor=#CCCCCC cellSpacing=0  borderColorDark=#ffffff cellPadding=0 height=100%  width=100 border=1>" +
                                                    "<TR>" +
                                                        "<TD align=\"center\" bgColor=" + colors + " colSpan=\"2\" height=\"20\">" + bianhaohtml[count + 1 + j] + "</TD>" +
                                                    "</TR>" +
                                                    "<TR>" +
                                                        "<TD align=\"center\" bgColor=\"#ffffcc\" colSpan=\"2\" height=\"20\"><font size=2px><img height='18' src='" + jibie + "'></font></TD>" +
                                                    "</TR>" +
                                                    "<TR>" +
                                                        "<TD align=\"center\" colSpan=\"2\" height=\"17\"><IMG height=\"18\" src=\"images/00.gif\" width=\"18\"></TD>" +
                                                    "</TR>" +
                                                "</TABLE>" +
                                            "</td>" +
                                        "</tr>" +
                                    "</table>" +
                                "</td>";
                }

            }
            t = 1;
            if (_count > 0)
            {
                strhtml1 += "</tr>" +
                    "</table>" +
                    "</td>" +
                    "</tr>";
            }

            strhtml1 += "</table>" +
                       "</td>";


        }
        strhtml1 += "</td>" +
                  "</tr>" +
                  "</table>" +
                  "</td>";
        Div_TuiJian.InnerHtml = strhtml1 + "</tr></table>";
    }

    #region
    ///// <summary>
    ///// 常用安置网络图
    ///// </summary>
    ///// <param name="Div_AnZhi">ＤＩＶ</param>
    ///// <param name="TopBianhao">首点编号</param>
    ///// <param name="ExpectNum">期数</param>
    ///// <returns>返回Table形式的字符串</returns>
    //public static void WangLuoTu_AnZhi(System.Web.UI.HtmlControls.HtmlGenericControl Div_AnZhi, string TopBianhao, string qishu)
    //{

    //    string str = "select b.number,a.jibie,b.qishu,b.tuijian  from h_jiesuan" + qishu + " a,memberInfo b where a.number=b.number";
    //    DataTable dt = DAL.DAL.DBHelper.ExecuteDataTable(str);

    //    DataTable dt_anzhi;
    //    DataTable dt_anzhi1;
    //    DataRow[] row;
    //    int div_count = 0;
    //    int count = 0;
    //    int i = 0;
    //    int p = 0;
    //    int k = 0;
    //    int jb = 0;

    //    string jibie = "";
    //    string colors = "";
    //    string sql = "";
    //    string anzhi = "";

    //    if (TopBianhao == "")
    //    {
    //        anzhi = "";
    //    }
    //    else
    //        anzhi = TopBianhao;

    //    sql = "select  *  from   memberInfo   where  anzhi='" + anzhi + "' and  ExpectNum<='" + qishu + "' ";
    //    dt_anzhi = DAL.DAL.DBHelper.ExecuteDataTable(sql);
    //    count = dt_anzhi.Rows.Count;

    //    div_count += count;
    //    for (i = 0; i < dt_anzhi.Rows.Count; i++)
    //    {
    //        dt_anzhi1 = DAL.DAL.DBHelper.ExecuteDataTable("select *  from  memberInfo  where  anzhi='" + dt_anzhi.Rows[i]["number"] + "' and  ExpectNum<='" + qishu + "' and  anzhi<>''");
    //        div_count += dt_anzhi1.Rows.Count;
    //    }

    //    string[] bianhaohtml = new string[div_count + 1]; ;
    //    string[] jibiehtml = new string[div_count + 1];
    //    string[] color = new string[div_count + 1];
    //    for (p = 0; p < div_count; p++)
    //    {
    //        bianhaohtml[p] = "&nbsp;";
    //        jibiehtml[p] = "&nbsp";
    //        color[p] = "#b3b3b3";
    //    }
    //    #region 一层
    //    row = dt.Select("number='" + TopBianhao + "'", "number");
    //    if (row.Length > 0)
    //    {
    //        if (row[0]["number"].ToString().Trim() == "")
    //        {
    //            jibie = "";
    //        }
    //        else
    //        {
    //            jb = row[0]["level"] == System.DBNull.Value ? 0 : int.Parse(row[0]["level"].ToString());
    //            jibie = Business.GetLevelForEng(jb.ToString());
    //        }

    //        colors = "#00cc00";
    //        if (row[0]["ExpectNum"].ToString() == CommonData.getMaxqishu().ToString().Trim())
    //        {
    //            colors = "red";
    //        }

    //        bianhaohtml[0] = "<a href=MemberNetMap.aspx?net=az&SelectGrass=" + qishu + "&number=" + row[0]["number"].ToString() + "><font color=#333333>" + row[0]["number"].ToString() + "</font></a>";
    //        jibiehtml[0] = jibie;
    //        color[0] = colors;
    //    }
    //    #endregion
    //    #region 二层
    //    for (i = 0; i < dt_anzhi.Rows.Count; i++)
    //    {
    //        row = dt.Select("number='" + dt_anzhi.Rows[i]["number"] + "'", "number");
    //        if (row.Length > 0)
    //        {
    //            if (row[0]["number"].ToString().Trim() == "")
    //            {
    //                jibie = "";
    //            }
    //            else
    //            {
    //                jb = row[0]["level"] == System.DBNull.Value ? 0 : int.Parse(row[0]["level"].ToString());
    //                jibie = Business.GetLevelForEng(jb.ToString());
    //            }

    //            colors = "#00cc00";
    //            if (row[0]["ExpectNum"].ToString() == CommonData.getMaxqishu().ToString().Trim())
    //            {
    //                colors = "red";
    //            }

    //            bianhaohtml[i + 1] = "<a href=MemberNetMap.aspx?net=az&SelectGrass=" + qishu + "&number=" + row[0]["number"].ToString() + "><font color=#333333>" + row[0]["number"].ToString() + "</font></a>";
    //            jibiehtml[i + 1] = jibie;
    //            color[i + 1] = colors;
    //        }
    //    }
    //    #endregion

    //    string strhtml1 = "";
    //    int _count1 = dt_anzhi.Rows.Count;//第二层线的个数(上线推荐的个数)
    //    int _count2 = 0;
    //    int width1 = 0;//第二层线的宽度
    //    int _count3 = 0;
    //    int width3 = 0;
    //    int width4 = 0;
    //    int _count4 = 0;
    //    for (i = 0; i < _count1; i++)
    //    {
    //        if (_count1 > 1)
    //        {
    //            dt_anzhi1 = DAL.DAL.DBHelper.ExecuteDataTable("select *  from  memberInfo  where  anzhi='" + dt_anzhi.Rows[i]["number"] + "' and  ExpectNum<='" + qishu + "'");
    //            _count4 = dt_anzhi1.Rows.Count;
    //            if (_count4 > 1)
    //            {
    //                _count2 += _count4;
    //                _count3 += _count4;
    //            }
    //            else
    //            {
    //                _count2++;
    //                _count3++;
    //            }
    //        }
    //        else
    //            _count2 = _count1;

    //        if (i == 0)
    //        {
    //            //width3第二层线头部的宽度
    //            if (_count4 <= 1)
    //                width3 = 50;
    //            else
    //                width3 = _count4 * 50;
    //        }
    //        if (i == (_count1 - 1))
    //        {
    //            //width4第二层线头部的宽度
    //            if (_count4 <= 1)
    //                width4 = 50;
    //            else
    //                width4 = _count4 * 50;
    //        }
    //    }
    //    width1 = _count2 * 100 - width3 - width4;//第二层线中间部分的宽度


    //    strhtml1 = "<table  border=\"0\" cellspacing=\"0\" cellpadding=\"0\"> " +
    //                "<tr align=\"center\" valign=\"top\">" +
    //                    "<td height=\"63\" colspan=\"" + _count1 + "\" align=\"center\">" +
    //                        "<table width=\"100\" height=\"63\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">" +
    //                            "<tr>" +
    //                                "<td height=\"47\" colspan=\"3\" valign=\"top\">" +
    //                                    "<TABLE  borderColor=#CCCCCC cellSpacing=0  borderColorDark=#ffffff cellPadding=0 height=100%  width=100 border=1>" +
    //                                        "<TR>" +
    //                                            "<TD align=\"center\" bgColor=" + color[0] + " colSpan=\"2\" height=\"20\">" + bianhaohtml[0] + "</TD>" +
    //                                        "</TR>" +
    //                                        "<TR>" +
    //                                            "<TD align=\"center\" bgColor=\"#ffffcc\" colSpan=\"2\" height=\"20\">" + jibiehtml[0] + "</TD>" +
    //                                        "</TR>" +
    //                                        "<TR>" +
    //                                            "<TD align=\"center\" colSpan=\"2\" height=\"17\"><IMG height=\"18\" src=\"images/00.gif\" width=\"18\"></TD>" +
    //                                        "</TR>" +
    //                                    "</TABLE>" +
    //                                "</td>" +
    //                            "</tr>";
    //    if (_count1 > 0)
    //    {
    //        strhtml1 += "<tr>" +
    //                       "<td width=\"49\" height=\"16\"></td>" +
    //                       "<td background=\"images/images_02.gif\"  > </td>" +
    //                       "<td width=\"49\"></td>" +
    //                   "</tr>";

    //    }
    //    strhtml1 += "</table>" +
    //            "</td>" +
    //        "</tr>";
    //    if (_count1 > 0)
    //    {
    //        if (_count2 > 1)
    //        {
    //            strhtml1 += "<tr align=\"center\" valign=\"top\">" +
    //                        "<td colspan=\"" + _count1 + "\" height=\"2\" >" +
    //                            "<table  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" +
    //                                "<tr>" +
    //                                    "<td height=\"2\" width=\"" + width3 + "\" ></td>" +
    //                                    "<td height=\"2\" width=\"" + width1 + "\"  background=\"images/images03_05.gif\"></td>" +
    //                                    "<td height=\"2\" width=\"" + width4 + "\"  ></td>" +
    //                                "</tr>" +
    //                            "</table>" +
    //                        "</td>" +
    //                    "</tr>";
    //        }
    //        else
    //        {

    //            strhtml1 += "<tr align=\"center\" valign=\"top\">" +
    //                        "<td colspan=\"" + _count1 + "\" height=\"2\" align=\"center\">" +
    //                            "<table  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" +
    //                                "<tr>" +
    //                                    "<td height=\"2\" width=\"2\"  background=\"images/images03_05.gif\"></td>" +
    //                                "</tr>" +
    //                            "</table>" +
    //                        "</td>" +
    //                    "</tr>";
    //        }
    //    }

    //    strhtml1 += "<tr valign=\"top\">";

    //    int t = 0;
    //    int _count = 0;
    //    int width2 = 0;
    //    for (i = 0; i < dt_anzhi.Rows.Count; i++)
    //    {
    //        dt_anzhi1 = DAL.DAL.DBHelper.ExecuteDataTable("select *  from  memberInfo  where  anzhi='" + dt_anzhi.Rows[i]["number"] + "'  and  ExpectNum<='" + qishu + "'");
    //        _count = dt_anzhi1.Rows.Count;
    //        width2 = (_count - 1) * 100;//第三层线的宽度
    //        strhtml1 += "<td  align=\"center\">" +
    //                    "<table  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" +
    //                        "<tr align=\"center\" valign=\"top\">" +
    //                            "<td height=\"79\" colspan=\"3\">" +
    //                                "<table  height=\"79\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">" +
    //                                    "<tr>" +
    //                                        "<td width=\"49\" height=\"16\"></td>" +
    //                                        "<td width=\"2\"  background=\"images/images_02.gif\"></td>" +
    //                                        "<td width=\"49\"></td>" +
    //                                    "</tr>" +
    //                                    "<tr>" +
    //                                        "<td height=\"47\" colspan=\"3\" valign=\"top\">" +
    //                                             "<TABLE  borderColor=#CCCCCC cellSpacing=0  borderColorDark=#ffffff cellPadding=0 height=100%  width=100 border=1>" +
    //                                                "<TR>" +
    //                                                    "<TD align=\"center\" bgColor=" + color[i + 1] + " colSpan=\"2\" height=\"20\">" + bianhaohtml[i + 1] + "</TD>" +
    //                                                "</TR>" +
    //                                                "<TR>" +
    //                                                    "<TD align=\"center\" bgColor=\"#ffffcc\" colSpan=\"2\" height=\"20\">" + jibiehtml[i + 1] + "</TD>" +
    //                                                "</TR>" +
    //                                                "<TR>" +
    //                                                    "<TD align=\"center\" colSpan=\"2\" height=\"17\"><IMG height=\"18\" src=\"images/00.gif\" width=\"18\"></TD>" +
    //                                                "</TR>" +
    //                                            "</TABLE>" +
    //                                        "</td>" +
    //                                    "</tr>";
    //        if (_count > 0)
    //        {
    //            strhtml1 += "<tr>" +
    //                            "<td width=\"49\" height=\"16\"></td>" +
    //                            "<td width=\"2\"  background=\"images/images_02.gif\"></td>" +
    //                            "<td width=\"49\"></td>" +
    //                        "</tr>";
    //        }

    //        strhtml1 += "</table>" +
    //                "</td>" +
    //             "</tr>";

    //        if (_count > 0)
    //        {
    //            if (_count == 1)
    //            {
    //                strhtml1 += "<tr>" +
    //                            "<td width=\"49\" height=\"2\"></td>" +
    //                            "<td background=\"images/images03_05.gif\"></td>" +
    //                            "<td width=\"49\"></td>" +
    //                            "</tr>";
    //            }
    //            else
    //            {
    //                strhtml1 += "<tr>" +
    //                            "<td width=\"49\" height=\"2\"></td>" +
    //                            "<td width=\"" + width2 + "\" background=\"images/images03_05.gif\"></td>" +
    //                            "<td width=\"49\"></td>" +
    //                            "</tr>";
    //            }
    //            strhtml1 += "<tr vAlign=\"top\">" +
    //                         "<td colSpan=\"3\" align=\"center\">" +
    //                            "<table  cellSpacing=\"0\" cellPadding=\"0\"  border=\"0\">" +
    //                                "<tr vAlign=\"top\">";
    //        }
    //        //if(t==1)
    //        //count+=dt_tuijian1.Rows.Count;
    //        for (int j = 0; j < dt_anzhi1.Rows.Count; j++)
    //        {
    //            row = dt.Select("number='" + dt_anzhi1.Rows[j]["number"] + "'", "number");
    //            if (row.Length > 0)
    //            {
    //                if (row[0]["number"].ToString().Trim() == "")
    //                {
    //                    jibie = "";
    //                }
    //                else
    //                {
    //                    jb = row[0]["level"] == System.DBNull.Value ? 0 : int.Parse(row[0]["level"].ToString());
    //                    jibie = Business.GetLevelForEng(jb.ToString());
    //                }

    //                colors = "#00cc00";
    //                if (row[0]["ExpectNum"].ToString() == CommonData.getMaxqishu().ToString().Trim())
    //                {
    //                    colors = "red";
    //                }

    //                bianhaohtml[count + 1 + j] = "<a href=MemberNetMap.aspx?net=az&SelectGrass=" + qishu + "&number=" + row[0]["number"].ToString() + "><font color=#333333>" + row[0]["number"].ToString() + "</font></a>";
    //                jibiehtml[count + 1 + j] = jibie;
    //                color[count + 1 + j] = colors;

    //                strhtml1 += "<td  height=\"63\" align=\"center\">" +
    //                                "<table cellSpacing=\"0\" cellPadding=\"0\"  border=\"0\">" +
    //                                    "<tr>" +
    //                                        "<td width=\"49\" height=\"16\"></td>" +
    //                                        "<td width=\"2\"  background=\"images/images_02.gif\"></td>" +
    //                                        "<td width=\"49\" ></td>" +
    //                                    "</tr>" +
    //                                    "<tr>" +
    //                                        "<td vAlign=\"top\" colSpan=\"3\" height=\"47\">" +
    //                                            "<TABLE  borderColor=#CCCCCC cellSpacing=0  borderColorDark=#ffffff cellPadding=0 height=100%  width=100 border=1>" +
    //                                                "<TR>" +
    //                                                    "<TD align=\"center\" bgColor=" + colors + " colSpan=\"2\" height=\"20\">" + bianhaohtml[count + 1 + j] + "</TD>" +
    //                                                "</TR>" +
    //                                                "<TR>" +
    //                                                    "<TD align=\"center\" bgColor=\"#ffffcc\" colSpan=\"2\" height=\"20\">" + jibie + "</TD>" +
    //                                                "</TR>" +
    //                                                "<TR>" +
    //                                                    "<TD align=\"center\" colSpan=\"2\" height=\"17\"><IMG height=\"18\" src=\"images/00.gif\" width=\"18\"></TD>" +
    //                                                "</TR>" +
    //                                            "</TABLE>" +
    //                                        "</td>" +
    //                                    "</tr>" +
    //                                "</table>" +
    //                            "</td>";
    //            }

    //        }
    //        t = 1;
    //        if (_count > 0)
    //        {
    //            strhtml1 += "</tr>" +
    //                "</table>" +
    //                "</td>" +
    //                "</tr>";
    //        }

    //        strhtml1 += "</table>" +
    //                   "</td>";


    //    }
    //    strhtml1 += "</td>" +
    //              "</tr>" +
    //              "</table>" +
    //              "</td>";
    //    Div_AnZhi.InnerHtml = strhtml1 + "</tr></table>";
    //}
    #endregion

    /// <summary>
    /// 获取上级
    /// </summary>
    /// <param name="isAnZhi">是否是安置</param>
    /// <returns>返回anzhi或者tuijian</returns>
    public static string getShangJi(bool isPlacement)
    {

        string temp = "";
        if (isPlacement)
        {
            temp = "Placement";
        }
        else
        {
            temp = "Direct";
        }
        return temp;
    }

    /// <summary>
    /// 辅助类
    /// </summary>
    /// <param name="Str">字符串</param>
    /// <param name="Times"></param>
    /// <returns></returns>
    public static string dupText(string Str, int Times)
    {
        StringBuilder sb = new StringBuilder("");

        for (int i = 1; i <= Times; i++)
        {
            sb.Append(Str);
        }
        return Convert.ToString(sb);
    }

    /// <summary>
    /// 得到会员编号
    /// </summary>
    /// <returns></returns>
    public static string getMemberBH()
    {
        string temp = "";

        switch (getType())
        {
            case "H":
                temp = Convert.ToString(HttpContext.Current.Session["Member"]);
                break;
            case "D":
                temp = Convert.ToString(DAL.DBHelper.ExecuteScalar("select number from storeInfo where StoreID='" + HttpContext.Current.Session["Store"].ToString() + "'"));
                break;
            case "G":
                temp = DAL.DBHelper.ExecuteScalar("select top 1 number from manage where defaultmanager=1").ToString();
                break;
        }
        return temp;
        return "";
    }

    public static string getType()
    {
        string sfType = "H";
        if (HttpContext.Current.Session["Store"] != null)
        {
            sfType = "D";
        }
        if (HttpContext.Current.Session["Company"] != null)
        {
            sfType = "G";
        }
        return sfType;
    }


    public static DataTable ShowNumber(string manageId, bool isAnzhi)
    {
        DataTable myDataTable = new DataTable("wangluo");
        // Declare variables for DataColumn and DataRow objects.
        DataColumn myDataColumn;
        DataRow myDataRow;
        //DataSet myDataSet;

        // Create new DataColumn, set DataType, ColumnName and add to DataTable.    
        myDataColumn = new DataColumn();
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataColumn.ColumnName = "wlt";
        myDataColumn.ReadOnly = false;
        myDataColumn.Unique = false;
        // Add the Column to the DataColumnCollection.
        myDataTable.Columns.Add(myDataColumn);

        string tj = "";
        if (isAnzhi)
        {
            tj = " type=1 ";
        }
        else
        {
            tj = " type=0 ";
        }

        SqlDataReader dr = DAL.DBHelper.ExecuteReader("Select number From ViewManage Where ManageId = '" + manageId + "' and " + tj);
        string strNumber = "";
        while (dr.Read())
        {
            myDataRow = myDataTable.NewRow();
            strNumber = "<span  style='cursor:hand' id='top" + dr["number"].ToString() + "' onclick='ShowView(this);'>" + dr["number"].ToString() + "</span>";
            myDataRow["wlt"] = strNumber;

            myDataTable.Rows.InsertAt(myDataRow, 0);
        }
        dr.Close();
        return myDataTable;
    }

    public static DataTable ShowTopNumber(string manageId, bool isAnzhi)
    {
        string tj = "";
        if (isAnzhi)
        {
            tj = " type=1 ";
        }
        else
        {
            tj = " type=0 ";
        }

        DataTable dt = DAL.DBHelper.ExecuteDataTable("Select top 1 number From ViewManage Where ManageId = '" + manageId + "' and " + tj);

        return dt;
    }

    /// <summary>
    /// 生成网络图
    /// </summary>
    /// <param name="QiShu">期数</param>
    /// <param name="isAnZhi">是否是安置关系</param>
    /// <param name="number">起点编号</param>
    /// <returns>返回表</returns>
    public static string[] wltForEng(int QiShu, int showCW, bool isAnZhi, string bianhao, string page)
    {
        string shangji = Jiegou.getShangJi(isAnZhi);//生成查询的关系（anzhi或者tuijian ）

        StringBuilder sb = new StringBuilder();
        StringBuilder sb1 = new StringBuilder();
        StringBuilder sb2 = new StringBuilder();
        StringBuilder sb3 = new StringBuilder();
        StringBuilder sb4 = new StringBuilder();
        StringBuilder sb5 = new StringBuilder();
        StringBuilder sb6 = new StringBuilder();
        StringBuilder sb7 = new StringBuilder();

        DataTable myDataTable = new DataTable("wangluo");
        // Declare variables for DataColumn and DataRow objects.
        DataColumn myDataColumn;
        DataRow myDataRow;
        //DataSet myDataSet;

        // Create new DataColumn, set DataType, ColumnName and add to DataTable.    
        myDataColumn = new DataColumn();
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataColumn.ColumnName = "xinxi";
        myDataColumn.ReadOnly = false;
        myDataColumn.Unique = false;
        // Add the Column to the DataColumnCollection.
        myDataTable.Columns.Add(myDataColumn);

        // Create second column.
        myDataColumn = new DataColumn();
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataColumn.ColumnName = "number";
        myDataColumn.AutoIncrement = false;
        myDataColumn.Caption = "number";
        myDataColumn.ReadOnly = false;
        myDataColumn.Unique = false;
        // Add the column to the table.
        myDataTable.Columns.Add(myDataColumn);
        //生成记录
        //获取序号范围
        int startXH = 0;
        int endXH = 0;
        int baseCengwei = 0;
        string myXuhao, myCengwei;
        if (isAnZhi)
        {
            myXuhao = "Ordinal1";
            myCengwei = "LayerBit1";
        }
        else
        {
            myXuhao = "Ordinal2";
            myCengwei = "LayerBit2";
        }
        string TempStr = "";
        int TempVal = 2;

        string tempXianshi = "";

        //取得序号范围
        Jiegou.getXHFW(bianhao, isAnZhi, QiShu, out startXH, out endXH, out baseCengwei);

        string selectsql = "select isnull(I.isActive,0) as isActive,j.number,isnull(j.level,0) as level,layerbit1,ordinal1,layerbit2,ordinal2,isnull(j.TotalNetNum,0) as TotalNetNum,isnull(j.TotalNetRecord,0) as TotalNetRecord,isnull(j.CurrentOneMark,0) as CurrentOneMark,isnull(j.CurrentTotalNetRecord,0) as CurrentTotalNetRecord,isnull(j.CurrentNewNetNum,0) as CurrentNewNetNum,I.PetName,I.MobileTele,I.ExpectNum from MemberInfoBalance" + QiShu.ToString() + " as J,memberInfo as I where J.number=I.number and J." + myXuhao + ">=" + startXH.ToString() + " and J." + myXuhao + "<=" + endXH.ToString() + " and J." + myCengwei + "<" + (baseCengwei + showCW).ToString() + " order by J." + myXuhao + " DESC";
        SqlDataReader dr = DAL.DBHelper.ExecuteReader(selectsql);

        string _WU = "无";
        string _SHOUJI = "手机:";

        int type = 0;
        if (System.Web.HttpContext.Current.Session["Company"] != null)
        {
            type = 0;
        }
        else if (System.Web.HttpContext.Current.Session["Store"] != null)
        {
            type = 1;
        }
        else if (System.Web.HttpContext.Current.Session["Member"] != null)
        {
            type = 2;
        }
        string NetWorkDisplayStatus = "";
        DataTable dt = BLL.CommonClass.CommonDataBLL.GetNetWorkDisplayStatus(type);
        foreach (DataRow row in dt.Rows)
        {
            NetWorkDisplayStatus += row["flag"].ToString();
        }
        bool Display_Level = NetWorkDisplayStatus.Substring(0, 1) == "1";
        bool Display_CurrentOneMark = NetWorkDisplayStatus.Substring(1, 1) == "1";
        bool Display_CurrentTotalNetRecord = NetWorkDisplayStatus.Substring(2, 1) == "1";
        bool Display_CurrentNewNetNum = NetWorkDisplayStatus.Substring(3, 1) == "1";
        bool Display_TotalNetNum = NetWorkDisplayStatus.Substring(4, 1) == "1";
        bool Display_TotalNetRecord = NetWorkDisplayStatus.Substring(5, 1) == "1";

        int i = 0;
        //需要翻译
        while (dr.Read())
        {
            i++;

            string ag = dr["number"].ToString();
            string sgas = dr[myCengwei].ToString();

            TempVal = (Convert.ToInt32(dr[myCengwei]) - Convert.ToInt32(baseCengwei) + 1) * 3;
            TempStr = (TempStr + Jiegou.dupText("^", TempVal));
            if (TempVal<=0)
            {
                TempVal = TempStr.Length;
            }
            TempStr = TempStr.Substring(0, TempVal - 1) + "^^|";


            myDataRow = myDataTable.NewRow();
            string mouseinfo = "";

            string Mobile = "";
            if (Convert.ToString(dr["MobileTele"]).Trim() != "")
            {
                Mobile = "[" + Convert.ToString(dr["MobileTele"]) + "]";
            }
            else
            {
                Mobile = "[" + _WU + "]";
            }
            mouseinfo = _SHOUJI + "[" + Mobile + "]";





            string _bianhao = bianhao;

            if (System.Web.HttpContext.Current.Session["bh"] != null)
            {
                _bianhao = System.Web.HttpContext.Current.Session["bh"].ToString().Trim();
            }
            if (System.Web.HttpContext.Current.Session["Store"] != null)
            {
                _bianhao = System.Web.HttpContext.Current.Session["Store"].ToString().Trim();
            }
            if (System.Web.HttpContext.Current.Session["Company"] != null)
            {
                _bianhao = System.Web.HttpContext.Current.Session["Company"].ToString().Trim();
            }

            //生成信息


            string Font = "";


            if (dr["isActive"] == "0")
            {
                Font = "color=Silver";
                if (Convert.ToInt32(dr["ExpectNum"]) >= QiShu)
                {
                    Font = "color=#FF8686";
                }
            }
            else
            {
                Font = "color=black";
                if (Convert.ToInt32(dr["ExpectNum"]) >= QiShu)
                {
                    Font = "color=#E30000";
                }
            }


            tempXianshi = TempStr.Replace(" ", "&nbsp;")
                + "~~" + "<a href=" + page + dr["number"].ToString() + " id=" + dr["number"].ToString() + " onMouseUp=popUp(this.id," + QiShu + ",event) title='编号' onclick='GetBind(" + dr["number"].ToString() + ")'>" + "<font " + Font + " >"
                + dr["number"].ToString() + "</font>&nbsp;<font color=black>" + dr["PetName"].ToString() + "</font></a>"
                ;

            string t = tempXianshi.Replace("^", "<img src='../images/0150.gif'  align=absmiddle  border=0 />");
            t = t.Replace("|", "<img src='../images/0044.gif'  align=absmiddle  border=0 />");
            t = t.Replace("~", "<img src='../images/0111.gif'  align=absmiddle  border=0 />");

            t = t
                + ((Display_CurrentOneMark) ? ("<a title='" + BLL.Translation.Translate("000028", "电话号码") + "'>[" + dr["MobileTele"].ToString() + "]</a>") : "");

            t = "<p style='margin-bottom: -19px'>" + t + "</p>";

            if (i > 0 && i <= 5000)
            {
                sb.Insert(0, t);
            }
            else if (i > 5000 && i <= 10000)
            {
                sb1.Insert(0, t);
            }
            else if (i > 10000 && i <= 15000)
            {
                sb2.Insert(0, t);
            }
            else if (i > 15000 && i <= 20000)
            {
                sb3.Insert(0, t);
            }
            else if (i > 20000 && i <= 25000)
            {
                sb4.Insert(0, t);
            }
            else if (i > 25000 && i <= 30000)
            {
                sb5.Insert(0, t);
            }
            else if (i > 30000 && i <= 35000)
            {
                sb6.Insert(0, t);
            }
            else
            {
                sb7.Insert(0, t);
            }


        }
        dr.Close();


        string[] strRes = new string[] { sb7.ToString(), sb6.ToString(), sb5.ToString(), sb4.ToString(), sb3.ToString(), sb2.ToString(), sb1.ToString(), sb.ToString() };
        return strRes;
    }

    /// <summary>
    /// 生成网络图
    /// </summary>
    /// <param name="QiShu">期数</param>
    /// <param name="isAnZhi">是否是安置关系</param>
    /// <param name="number">起点编号</param>
    /// <returns>返回表</returns>
    public static DataTable wltForEngTwo(int QiShu, int showCW, bool isAnZhi, string bianhao, string page)
    {
        string shangji = Jiegou.getShangJi(isAnZhi);//生成查询的关系（anzhi或者tuijian ）

        DataTable myDataTable = new DataTable("wangluo");
        // Declare variables for DataColumn and DataRow objects.
        DataColumn myDataColumn;
        DataRow myDataRow;
        //DataSet myDataSet;

        // Create new DataColumn, set DataType, ColumnName and add to DataTable.    
        myDataColumn = new DataColumn();
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataColumn.ColumnName = "xinxi";
        myDataColumn.ReadOnly = false;
        myDataColumn.Unique = false;
        // Add the Column to the DataColumnCollection.
        myDataTable.Columns.Add(myDataColumn);

        // Create second column.
        myDataColumn = new DataColumn();
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataColumn.ColumnName = "number";
        myDataColumn.AutoIncrement = false;
        myDataColumn.Caption = "number";
        myDataColumn.ReadOnly = false;
        myDataColumn.Unique = false;
        // Add the column to the table.
        myDataTable.Columns.Add(myDataColumn);
        //生成记录
        //获取序号范围
        int startXH = 0;
        int endXH = 0;
        int baseCengwei = 0;
        string myXuhao, myCengwei;
        if (isAnZhi)
        {
            myXuhao = "Ordinal1";
            myCengwei = "LayerBit1";
        }
        else
        {
            myXuhao = "Ordinal2";
            myCengwei = "LayerBit2";
        }
        string TempStr = "";
        int TempVal = 2;

        string tempXianshi = "";

        //取得序号范围
        Jiegou.getXHFW(bianhao, isAnZhi, QiShu, out startXH, out endXH, out baseCengwei);

        string selectsql = "select j.isbuliang,j.number,isnull(j.level,0) as level,layerbit1,ordinal1,layerbit2,ordinal2,isnull(j.TotalNetNum,0) as TotalNetNum,isnull(j.TotalNetRecord,0) as TotalNetRecord,isnull(j.CurrentOneMark,0) as CurrentOneMark,isnull(j.CurrentTotalNetRecord,0) as CurrentTotalNetRecord,isnull(j.CurrentNewNetNum,0) as CurrentNewNetNum,I.PetName,I.MobileTele,I.ExpectNum from MemberInfoBalance" + QiShu.ToString() + " as J,memberInfo as I where J.number=I.number and J." + myXuhao + ">=" + startXH.ToString() + " and J." + myXuhao + "<=" + endXH.ToString() + " and J." + myCengwei + "<" + (baseCengwei + showCW).ToString() + " order by J." + myXuhao + " DESC";
        SqlDataReader dr = DAL.DBHelper.ExecuteReader(selectsql);
        string sql = "select J.*,I.PetName,I.MobileTele,I.ExpectNum from MemberInfoBalance" + QiShu.ToString() + " as J,memberInfo as I where J.number=I.number and J." + myXuhao + ">=" + startXH.ToString() + " and J." + myXuhao + "<=" + endXH.ToString() + " and J." + myCengwei + "<" + (baseCengwei + showCW).ToString() + " order by J." + myXuhao + " DESC";
        string _WU = "无";
        string _SHOUJI = "手机:";
        //需要翻译
        while (dr.Read())
        {
            string ag = dr["number"].ToString();
            string sgas = dr[myCengwei].ToString();

            TempVal = (Convert.ToInt32(dr[myCengwei]) - Convert.ToInt32(baseCengwei) + 1) * 3;
            TempStr = (TempStr + Jiegou.dupText("^", TempVal));
            if (TempVal<=0)
            {
                TempVal = TempStr.Length;
            }
            TempStr = TempStr.Substring(0, TempVal - 1) + "^^|";

            myDataRow = myDataTable.NewRow();
            string mouseinfo = "";

            string Mobile = "";
            if (Convert.ToString(dr["MobileTele"]).Trim() != "")
            {
                Mobile = "[" + Convert.ToString(dr["MobileTele"]) + "]";
            }
            else
            {
                Mobile = "[" + _WU + "]";
            }
            mouseinfo = _SHOUJI + "[" + Mobile + "]";

            int type = 0;
            if (System.Web.HttpContext.Current.Session["Company"] != null)
            {
                type = 0;
            }
            else if (System.Web.HttpContext.Current.Session["Store"] != null)
            {
                type = 1;
            }
            else if (System.Web.HttpContext.Current.Session["Member"] != null)
            {
                type = 2;
            }

            string NetWorkDisplayStatus = "";
            DataTable dt = BLL.CommonClass.CommonDataBLL.GetNetWorkDisplayStatus(type);
            foreach (DataRow row in dt.Rows)
            {
                NetWorkDisplayStatus += row["flag"].ToString();
            }
            bool Display_Level = NetWorkDisplayStatus.Substring(0, 1) == "1";
            bool Display_CurrentOneMark = NetWorkDisplayStatus.Substring(1, 1) == "1";
            bool Display_CurrentTotalNetRecord = NetWorkDisplayStatus.Substring(2, 1) == "1";
            bool Display_CurrentNewNetNum = NetWorkDisplayStatus.Substring(3, 1) == "1";
            bool Display_TotalNetNum = NetWorkDisplayStatus.Substring(4, 1) == "1";
            bool Display_TotalNetRecord = NetWorkDisplayStatus.Substring(5, 1) == "1";

            string _bianhao = bianhao;

            if (System.Web.HttpContext.Current.Session["bh"] != null)
            {
                _bianhao = System.Web.HttpContext.Current.Session["bh"].ToString().Trim();
            }
            if (System.Web.HttpContext.Current.Session["Store"] != null)
            {
                _bianhao = System.Web.HttpContext.Current.Session["Store"].ToString().Trim();
            }
            if (System.Web.HttpContext.Current.Session["Company"] != null)
            {
                _bianhao = System.Web.HttpContext.Current.Session["Company"].ToString().Trim();
            }

            //生成信息
            string Font = "color=blue";
            if (Convert.ToInt32(dr["isbuliang"]) == 1)
            {
                Font = "color='rgb(228,228,0)'";
            }

            tempXianshi = TempStr.Replace(" ", "&nbsp;")
                + "~~" + "<a href=" + page + dr["number"].ToString() + " title='编号' onclick='GetBind(" + dr["number"].ToString() + ")'>" + "<font  " + Font + " >"
                + dr["number"].ToString() + "</font>&nbsp;<font color=black>" + dr["PetName"].ToString() + "</font></a>"
                ;

            string t = tempXianshi.Replace("^", "<img src='../images/0150.gif'  align=absmiddle  border=0 />");
            t = t.Replace("|", "<img src='../images/0044.gif'  align=absmiddle  border=0 />");
            t = t.Replace("~", "<img src='../images/0111.gif'  align=absmiddle  border=0 />");

            t = t
                + ((Display_CurrentOneMark) ? ("<a title='" + BLL.Translation.Translate("000028", "电话号码") + "'>[" + dr["MobileTele"].ToString() + "]</a>") : "");
            //+ ((Display_CurrentTotalNetRecord) ? ("<a title='" + BLL.Translation.Translate("000936", "新网分数") + "'>[" + Convert.ToInt32(dr["CurrentTotalNetRecord"]).ToString() + "]</a>") : "")
            //+ ((Display_CurrentNewNetNum) ? ("<a title='" + BLL.Translation.Translate("000934", "新网人数") + "'>[" + Convert.ToInt32(dr["CurrentNewNetNum"]).ToString() + "]</a>") : "")
            //+ ((Display_TotalNetNum) ? ("<a title='" + BLL.Translation.Translate("000933", "总网人数") + "'>[" + Convert.ToInt32(dr["TotalNetNum"]).ToString() + "]</a>") : "")
            //+ ((Display_TotalNetRecord) ? ("<a title='" + BLL.Translation.Translate("000937", "总网分数") + "'>[" + Convert.ToInt32(dr["TotalNetRecord"]).ToString() + "]</a>") : "");

            myDataRow["xinxi"] = t;

            myDataRow["number"] = dr["number"];
            myDataTable.Rows.InsertAt(myDataRow, 0);
        }
        dr.Close();
        return myDataTable;
    }

    /// <summary>
    /// 生成网络图
    /// </summary>
    /// <param name="QiShu">期数</param>
    /// <param name="isAnZhi">是否是安置关系</param>
    /// <param name="number">起点编号</param>
    /// <returns>返回表</returns>
    public static DataTable wltForEngMember(int QiShu, int showCW, bool isAnZhi, string bianhao, string page)
    {
        string shangji = Jiegou.getShangJi(isAnZhi);//生成查询的关系（anzhi或者tuijian ）

        DataTable myDataTable = new DataTable("wangluo");
        // Declare variables for DataColumn and DataRow objects.
        DataColumn myDataColumn;
        DataRow myDataRow;
        //DataSet myDataSet;

        // Create new DataColumn, set DataType, ColumnName and add to DataTable.    
        myDataColumn = new DataColumn();
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataColumn.ColumnName = "xinxi";
        myDataColumn.ReadOnly = false;
        myDataColumn.Unique = false;
        // Add the Column to the DataColumnCollection.
        myDataTable.Columns.Add(myDataColumn);

        // Create second column.
        myDataColumn = new DataColumn();
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataColumn.ColumnName = "number";
        myDataColumn.AutoIncrement = false;
        myDataColumn.Caption = "number";
        myDataColumn.ReadOnly = false;
        myDataColumn.Unique = false;
        // Add the column to the table.
        myDataTable.Columns.Add(myDataColumn);
        //生成记录
        //获取序号范围
        int startXH = 0;
        int endXH = 0;
        int baseCengwei = 0;
        string myXuhao, myCengwei;
        if (isAnZhi)
        {
            myXuhao = "Ordinal1";
            myCengwei = "LayerBit1";
        }
        else
        {
            myXuhao = "Ordinal2";
            myCengwei = "LayerBit2";
        }
        string TempStr = "";
        int TempVal = 2;

        string tempXianshi = "";

        //取得序号范围
        Jiegou.getXHFW(bianhao, isAnZhi, QiShu, out startXH, out endXH, out baseCengwei);

        string selectsql = "select j.number,isnull(j.level,0) as level,layerbit1,ordinal1,layerbit2,ordinal2,isnull(j.TotalNetNum,0) as TotalNetNum,isnull(j.TotalNetRecord,0) as TotalNetRecord,isnull(j.CurrentOneMark,0) as CurrentOneMark,isnull(j.CurrentTotalNetRecord,0) as CurrentTotalNetRecord,isnull(j.CurrentNewNetNum,0) as CurrentNewNetNum,I.PetName,I.MobileTele,I.ExpectNum from MemberInfoBalance" + QiShu.ToString() + " as J,memberInfo as I where J.number=I.number and J." + myXuhao + ">=" + startXH.ToString() + " and J." + myXuhao + "<=" + endXH.ToString() + " and J." + myCengwei + "<" + (baseCengwei + showCW).ToString() + " order by J." + myXuhao + " DESC";
        SqlDataReader dr = DAL.DBHelper.ExecuteReader(selectsql);
        //	SqlDataReader dr=DAL.DBHelper.ExecuteReader("select J.*,I.xingming,I.dianhua3,I.qishu from h_jiesuan"+QiShu.ToString()+" as J,memberInfo_ce as I where J.bianhao=I.bianhao and J."+myXuhao+">="+startXH.ToString()+" and J."+myXuhao+"<="+endXH.ToString()+" and J."+myCengwei+"<"+(baseCengwei+showCW).ToString()+" order by J."+myXuhao+" DESC");
        string sql = "select J.*,I.PetName,I.MobileTele,I.ExpectNum from MemberInfoBalance" + QiShu.ToString() + " as J,memberInfo as I where J.number=I.number and J." + myXuhao + ">=" + startXH.ToString() + " and J." + myXuhao + "<=" + endXH.ToString() + " and J." + myCengwei + "<" + (baseCengwei + showCW).ToString() + " order by J." + myXuhao + " DESC";
        string _WU = "无";
        string _SHOUJI = "手机:";
        //需要翻译
        //string _WU = CommonData.GetClassTran("classes/jiegou.cs_108", "无");
        //string _SHOUJI = CommonData.GetClassTran("classes/jiegou.cs_110", "手机:");
        while (dr.Read())
        {
            string ag = dr["number"].ToString();
            string sgas = dr[myCengwei].ToString();

            TempVal = (Convert.ToInt32(dr[myCengwei]) - Convert.ToInt32(baseCengwei) + 1) * 3;
            TempStr = (TempStr + Jiegou.dupText("^", TempVal));
            TempStr = TempStr.Substring(0, TempVal - 1) + "^^|";

            //TempStr=newText.dupText("&nbsp;&nbsp;",Convert.ToInt32( dr[myCengwei] )-1);
            myDataRow = myDataTable.NewRow();
            string mouseinfo = "";

            string Mobile = "";
            if (Convert.ToString(dr["MobileTele"]).Trim() != "")
            {
                Mobile = "[" + Convert.ToString(dr["MobileTele"]) + "]";
            }
            else
            {
                Mobile = "[" + _WU + "]";
            }
            mouseinfo = _SHOUJI + "[" + Mobile + "]";
            //string NetWorkDisplayStatus = System.Web.HttpContext.Current.Application["NetWorkDisplayStatus"].ToString().Trim();
            int type = 0;
            if (System.Web.HttpContext.Current.Session["Company"] != null)
            {
                type = 0;
            }
            else if (System.Web.HttpContext.Current.Session["Store"] != null)
            {
                type = 1;
            }
            else if (System.Web.HttpContext.Current.Session["Member"] != null)
            {
                type = 2;
            }

            string NetWorkDisplayStatus = "";
            DataTable dt = BLL.CommonClass.CommonDataBLL.GetNetWorkDisplayStatus(type);
            foreach (DataRow row in dt.Rows)
            {
                NetWorkDisplayStatus += row["flag"].ToString();
            }
            bool Display_Level = NetWorkDisplayStatus.Substring(0, 1) == "1";
            bool Display_CurrentOneMark = NetWorkDisplayStatus.Substring(1, 1) == "1";
            bool Display_CurrentTotalNetRecord = NetWorkDisplayStatus.Substring(2, 1) == "1";
            bool Display_CurrentNewNetNum = NetWorkDisplayStatus.Substring(3, 1) == "1";
            bool Display_TotalNetNum = NetWorkDisplayStatus.Substring(4, 1) == "1";
            bool Display_TotalNetRecord = NetWorkDisplayStatus.Substring(5, 1) == "1";

            string _bianhao = bianhao;

            if (System.Web.HttpContext.Current.Session["bh"] != null)
            {
                _bianhao = System.Web.HttpContext.Current.Session["bh"].ToString().Trim();
            }
            if (System.Web.HttpContext.Current.Session["Store"] != null)
            {
                _bianhao = System.Web.HttpContext.Current.Session["Store"].ToString().Trim();
            }
            if (System.Web.HttpContext.Current.Session["Company"] != null)
            {
                _bianhao = System.Web.HttpContext.Current.Session["Company"].ToString().Trim();
            }

            //生成信息
            int isactive = (int)DAL.DBHelper.ExecuteScalar("select isnull(isActive,0) from memberinfo where number='" + dr["number"].ToString() + "'");

            string Font = "";

            if (isactive == 0)
            {
                Font = "color=Silver";
                if (Convert.ToInt32(dr["ExpectNum"]) >= QiShu)
                {
                    Font = "color=#FF8686";
                }
            }
            else
            {
                Font = "color=black";
                if (Convert.ToInt32(dr["ExpectNum"]) >= QiShu)
                {
                    Font = "color=#E30000";
                }
            }

            int ISAZ = 0;
            if (isAnZhi == true)
            {
                ISAZ = 1;
            }
            else
            {
                ISAZ = 0;
            }
            tempXianshi = TempStr.Replace(" ", "&nbsp;")
                + "~~" + "<a href='#' style='vertical-align:baseline'  onMouseUp=popUp(this.id," + QiShu + ",event) id='" + dr["number"].ToString() + "' onclick=\"ShowView(" + ISAZ + "," + QiShu + ",'" + dr["number"].ToString() + "')\" title='编号' >" + "<font  " + Font + " >"
                + dr["number"].ToString() + "</font>&nbsp;<font color=black>" + dr["PetName"].ToString() + "</font></a>"
                ;

            string t = tempXianshi.Replace("^", "<img src='../images/0150.gif'  align=absmiddle  border=0 />");
            t = t.Replace("|", "<img src='../images/0044.gif'  align=absmiddle  border=0 />");
            t = t.Replace("~", "<img src='../images/0111.gif'  align=absmiddle  border=0 />");

            t = t
                + ((Display_CurrentOneMark) ? ("<a title='" + BLL.Translation.Translate("000028", "电话号码") + "'>[" + dr["MobileTele"].ToString() + "]</a>") : "");
            //    + ((Display_CurrentTotalNetRecord) ? ("<a title='" + BLL.Translation.Translate("000936", "新网分数") + "'>[" + Convert.ToInt32(dr["CurrentTotalNetRecord"]).ToString() + "]</a>") : "")
            //    + ((Display_CurrentNewNetNum) ? ("<a title='" + BLL.Translation.Translate("000934", "新网人数") + "'>[" + Convert.ToInt32(dr["CurrentNewNetNum"]).ToString() + "]</a>") : "")
            //    + ((Display_TotalNetNum) ? ("<a title='" + BLL.Translation.Translate("000933", "总网人数") + "'>[" + Convert.ToInt32(dr["TotalNetNum"]).ToString() + "]</a>") : "")
            //    + ((Display_TotalNetRecord) ? ("<a title='" + BLL.Translation.Translate("000937", "总网分数") + "'>[" + Convert.ToInt32(dr["TotalNetRecord"]).ToString() + "]</a>") : "");

            myDataRow["xinxi"] = t;

            myDataRow["number"] = dr["number"];
            myDataTable.Rows.InsertAt(myDataRow, 0);
        }
        dr.Close();
        return myDataTable;
    }


    /// <summary>
    /// 求某编号的序号范围 ： 
    /// </summary>
    /// <param name="number">编号</param>
    /// <param name="anzhi">null为推荐，否则为安置</param>
    /// <param name="qishu">期数</param>
    /// <param name="sXuhao">起始序号</param>
    /// <param name="eXuhao">终止序号</param>
    public static void getXHFW(string bianhao, bool isAnZhi, int qishu, out int sXuhao, out int eXuhao, out int Cengwei)
    {
        string myXuhao, myCengwei;
        Cengwei = 9999;
        if (isAnZhi)
        {
            myXuhao = "Ordinal1";
            myCengwei = "LayerBit1";
        }
        else
        {
            myXuhao = "Ordinal2";
            myCengwei = "LayerBit2";
        }
        //获取最大序号
        object maxNum = DAL.DBHelper.ExecuteScalar("SELECT MAX(" + myXuhao + ") as mShu FROM MemberInfoBalance" + qishu.ToString(), CommandType.Text);
        if (maxNum == System.DBNull.Value)
        {
            eXuhao = 0;
        }
        else
        {
            eXuhao = Convert.ToInt32(maxNum);
        }

        sXuhao = eXuhao + 1;
        //获取输入会员的层位和序号
        SqlDataReader dr;
        string sql = "SELECT   isnull(" + myCengwei + ",0)  as  " + myCengwei + " , isnull(" + myXuhao + ",0)  as  " + myXuhao + " FROM MemberInfoBalance" + qishu.ToString() + " WHERE number = '" + bianhao + "'";
        dr = DAL.DBHelper.ExecuteReader(sql, CommandType.Text);
        if (dr.Read())
        {
            Cengwei = Convert.ToInt32(dr[myCengwei]);
            sXuhao = Convert.ToInt32(dr[myXuhao]);
        }
        dr.Close();

        //确定终止序号
        int lsXuhao = Convert.ToInt32(DAL.DBHelper.ExecuteScalar("SELECT " + myXuhao + " FROM MemberInfoBalance" + qishu.ToString() + " WHERE " + myCengwei + "<=" + Cengwei.ToString() + " AND " + myXuhao + ">" + sXuhao.ToString() + " ORDER BY " + myXuhao + " ASC", CommandType.Text));
        if (lsXuhao > 0)
        {
            eXuhao = lsXuhao - 1;
        }
    }


    /// <summary>
    /// 获取上级编号
    /// </summary>
    /// <param name="bianhao">编号</param>
    /// <param name="isAnZhi">是否是安置</param>
    /// <returns></returns>
    public static string GetHighNumber(string bianhao, bool isAnZhi)
    {
        string myCol = "";
        if (isAnZhi)
            myCol = "Placement";
        else
            myCol = "Direct";

        string sql = "select top 1 " + myCol + " from memberinfo where number=@num";
        SqlParameter[] para = {
                                new SqlParameter("@num", SqlDbType.NVarChar,30)
                              };
        para[0].Value = bianhao;

        return DAL.DBHelper.ExecuteScalar(sql, para, CommandType.Text).ToString();
    }

    /// <summary>
    /// 获取编号网络下的所有会员编号
    /// </summary>
    /// <returns></returns>
    public static IList<string> GetNumberForTop(string bianhao, int qishu, bool isAnZhi)
    {
        string myXuhao, myCengwei;
        int Cengwei = 9999;
        int eXuhao = 0, sXuhao = 0;
        if (isAnZhi)
        {
            myXuhao = "Ordinal1";
            myCengwei = "LayerBit1";
        }
        else
        {
            myXuhao = "Ordinal2";
            myCengwei = "LayerBit2";
        }
        //获取最大序号
        object maxNum = DAL.DBHelper.ExecuteScalar("SELECT MAX(" + myXuhao + ") as mShu FROM MemberInfoBalance" + qishu.ToString(), CommandType.Text);
        if (maxNum == System.DBNull.Value)
        {
            eXuhao = 0;
        }
        else
        {
            eXuhao = Convert.ToInt32(maxNum);
        }

        sXuhao = eXuhao + 1;
        //获取输入会员的层位和序号
        SqlDataReader dr;
        string sql = "SELECT   isnull(" + myCengwei + ",0)  as  " + myCengwei + " , isnull(" + myXuhao + ",0)  as  " + myXuhao + " FROM MemberInfoBalance" + qishu.ToString() + " WHERE number = '" + bianhao + "'";
        dr = DAL.DBHelper.ExecuteReader(sql, CommandType.Text);
        if (dr.Read())
        {
            Cengwei = Convert.ToInt32(dr[myCengwei]);
            sXuhao = Convert.ToInt32(dr[myXuhao]);
        }
        dr.Close();

        //确定终止序号
        int lsXuhao = Convert.ToInt32(DAL.DBHelper.ExecuteScalar("SELECT " + myXuhao + " FROM MemberInfoBalance" + qishu.ToString() + " WHERE " + myCengwei + "<=" + Cengwei.ToString() + " AND " + myXuhao + ">" + sXuhao.ToString() + " ORDER BY " + myXuhao + " ASC", CommandType.Text));
        if (lsXuhao > 0)
        {
            eXuhao = lsXuhao - 1;
        }

        IList<string> lists = new List<string>();
        DataTable dt = DAL.DBHelper.ExecuteDataTable("select number from MemberInfoBalance" + qishu.ToString() + " where " + myXuhao + " between " + sXuhao + " and " + eXuhao + "");
        foreach (DataRow drow in dt.Rows)
        {
            lists.Add(drow["number"].ToString());
        }

        return lists;
    }


    #region 生成表状网络图


    /// <summary>
    /// 生成表状网络图
    /// </summary>
    /// <param name="QiShu">期数</param>
    /// <param name="isAnZhi">是否是安置关系</param>
    /// <param name="number">起点编号</param>
    /// <returns>返回表</returns>
    public static void wltDatableMember(System.Web.UI.WebControls.Table Table1, int QiShu, int showCW, bool isAnZhi, string bianhao, string page)
    {
        string shangji = Jiegou.getShangJi(isAnZhi);//生成查询的关系（anzhi或者tuijian ）
        //获取序号范围			
        int ISAZ = 0;

        string tempXianshi = "";
        if (isAnZhi == true)
        {
            ISAZ = 1;
        }
        else
        {
            ISAZ = 0;
        }
        //---------------------------------------------------------------------------------------------------------------------
        SqlParameter[] para =
						{
							new SqlParameter ("@number" , SqlDbType .VarChar , 20),
							new SqlParameter ("@ISAZ" , SqlDbType.Bit ),
							new SqlParameter ("@ExpectNum"   , SqlDbType .Int, 4),
							new SqlParameter ("@cws",SqlDbType.Int,4)
						};
        para[0].Value = bianhao;
        para[1].Value = ISAZ;
        para[2].Value = QiShu;
        para[3].Value = showCW;

        DataTable wltdt = DAL.DBHelper.ExecuteDataTable("NetTB", para, CommandType.StoredProcedure);
        String SQL = "select number, LayerBit2-1,Ordinal2-1 from  MemberInfoBalance1";
        string SQL2 = null;
        if (ISAZ == 0)
        {
            SQL2 = "select number from MemberInfo where  Direct=";
        }
        else
        {
            SQL2 = "select number from MemberInfo where Placement=";
        }

        DataTable tabel2 = DAL.DBHelper.ExecuteDataTable(SQL);
        //bool door = true;
        string[] hasParent = new string[wltdt.Rows.Count];
        string[] parent = new string[wltdt.Rows.Count];

        int count2 = wltdt.Rows.Count;
        int t;
        int[] maxXh = new int[count2];

        if (wltdt.Rows.Count > 0)
        {
            for (int i = 0; i < wltdt.Rows.Count; i++)
            {
                maxXh[i] = Convert.ToInt32(wltdt.Rows[i]["xh"].ToString());
            }
        }


        // 冒泡
        for (int i = 0; i <= wltdt.Rows.Count - 1; i++)
        {
            for (int j = i + 1; j <= wltdt.Rows.Count - 1; j++)
            {
                if (maxXh[i] <= maxXh[j])
                { t = maxXh[i]; maxXh[i] = maxXh[j]; maxXh[j] = t; }
            }
        }
        //------------------------------------------------------------------------------------------------------------------
        int hang, lie;
        hang = maxXh[0] + 1;
        string[,] zoushi;
        string[,] reg;
        //获取有孩子的数组

        if (isAnZhi == true)
        {
            lie = 3;	// 总共3列
            zoushi = new string[hang, lie];
            reg = new string[hang, lie];
            // 初始化
            for (int m = 0; m < hang; m++)
            {
                for (int n = 0; n < lie; n++)
                {
                    zoushi[m, n] = "&nbsp;";
                }
            }
            // 赋值－标题－总共6列
            zoushi[0, 0] = "&nbsp;";
            zoushi[0, 1] = "&nbsp;";
            zoushi[0, 2] = "&nbsp;";
        }
        else
        {
            lie = 3; // 总共2列
            zoushi = new string[hang, lie];
            reg = new string[hang, lie];
            // 初始化
            for (int m = 0; m < hang; m++)
            {
                for (int n = 0; n < lie; n++)
                {
                    zoushi[m, n] = "&nbsp;";
                }
            }
            // 赋值－标题－总共6列
            zoushi[0, 0] = "&nbsp;";
            zoushi[0, 1] = "&nbsp;";
            zoushi[0, 2] = "&nbsp;";
        }

        string Font = "";
        bool judge2 = true;

        int needCellNullCount = 0;
        for (int i = 0; i < wltdt.Rows.Count; i++)
        {
            int m, n;

            m = Convert.ToInt32(wltdt.Rows[i]["xh"].ToString());
            n = Convert.ToInt32(wltdt.Rows[i]["cw"].ToString());

            if ((wltdt.Rows[i]["bh"].ToString() != "----" && needCellNullCount <= 0)) //|| isParent)
            {
                string result = wltdt.Rows[i]["register_ExpectNum"] == null || wltdt.Rows[i]["register_ExpectNum"].ToString() == "" ? ("1") : (wltdt.Rows[i]["register_ExpectNum"].ToString());
                if (Convert.ToInt32(result) >= QiShu)
                {
                    Font = "color=red";
                }
                else
                    Font = "";

                //tempXianshi = "<a href=" + page + wltdt.Rows[i]["bh"].ToString() + "><font class=ls " + Font + "  >" + wltdt.Rows[i]["bh"].ToString() + "</font>&nbsp;</a>";
                tempXianshi = "<a href='#' onclick=\"ShowView(" + ISAZ + "," + QiShu + ",'" + wltdt.Rows[i]["bh"].ToString() + "')\"><font class=ls " + Font + "  >" + wltdt.Rows[i]["bh"].ToString() + "</font>&nbsp;</a>";

                tempXianshi = tempXianshi
                            + "&nbsp;&nbsp;<img height='18' src='" + GetLevel(wltdt.Rows[i]["jibie"].ToString(), 0) + "' /><br>"//Jiegou.GetLevel(wltdt.Rows[i]["jibie"].ToString()) +
                            + "<a title='" + BLL.Translation.Translate("000937", "总网分数") + "'>[" + (Convert.ToDouble(wltdt.Rows[i]["zwfenshu"].ToString())).ToString() + "]</a>"  //(Convert.ToDouble(wltdt.Rows[i]["zwfenshu"].ToString())).ToString("f2") + "]</a>"//需要翻译CommonData.GetClassTran("classes/jiegou.cs_610", "累计市场业绩")
                    //+"<a title='新增网络业绩'>["+(Convert.ToDouble(yedt.Rows[0]["CurrentTotalNetRecord"].ToString())).ToString()+"]</a>"
                    //+"<a title='新网人数'>["+(Convert.ToDouble(yedt.Rows[0]["CurrentNewNetNum"].ToString())).ToString()+"]</a>"
                    //+"<a title='总网人数'>["+(Convert.ToDouble(yedt.Rows[0]["TotalNetNum"].ToString())).ToString()+"]</a>"
                            + "";
            }
            else
            {
                tempXianshi = "";
            }
            zoushi[m, n] = tempXianshi;
        }

        for (int m = 0; m < hang; m++)
        {
            TableRow myRow2 = new TableRow();
            for (int i = 0; i < lie; i++)
            {
                TableCell myCell = new TableCell();
                if (zoushi[m, i].ToString() == "----")
                {
                    myCell.Text = "<a href=" + reg[m, i] + ">" + zoushi[m, i].ToString() + "</a>";
                    myCell.Height = 50;
                    myCell.Width = 170;
                    myCell.HorizontalAlign = Table1.HorizontalAlign;
                    myCell.Wrap = true;
                    myCell.Attributes.Add("style", "background-color:#ffffff");
                }
                else
                {
                    myCell.Text = zoushi[m, i].ToString();
                    myCell.Height = 50;
                    myCell.Width = 170;
                    myCell.HorizontalAlign = Table1.HorizontalAlign;
                    myCell.Wrap = true;
                    myCell.Attributes.Add("style", "background-color:#ffffff");
                }

                myRow2.Cells.Add(myCell);
            }
            Table1.Rows.Add(myRow2);
        }
    }


    /// <summary>
    /// 生成表状网络图
    /// </summary>
    /// <param name="QiShu">期数</param>
    /// <param name="isAnZhi">是否是安置关系</param>
    /// <param name="number">起点编号</param>
    /// <returns>返回表</returns>
    public static void wltDatable(System.Web.UI.WebControls.Table Table1, int QiShu, int showCW, bool isAnZhi, string bianhao, string page)
    {
        string shangji = Jiegou.getShangJi(isAnZhi);//生成查询的关系（anzhi或者tuijian ）
        //获取序号范围			
        int ISAZ = 0;

        string tempXianshi = "";
        if (isAnZhi == true)
        {
            ISAZ = 1;
        }
        else
        {
            ISAZ = 0;
        }
        //---------------------------------------------------------------------------------------------------------------------
        SqlParameter[] para =
						{
							new SqlParameter ("@number" , SqlDbType .VarChar , 20),
							new SqlParameter ("@ISAZ" , SqlDbType.Bit ),
							new SqlParameter ("@ExpectNum"   , SqlDbType .Int, 4),
							new SqlParameter ("@cws",SqlDbType.Int,4)
						};
        para[0].Value = bianhao;
        para[1].Value = ISAZ;
        para[2].Value = QiShu;
        para[3].Value = showCW;

        DataTable wltdt = DAL.DBHelper.ExecuteDataTable("NetTB", para, CommandType.StoredProcedure);
        String SQL = "select number, LayerBit2-1,Ordinal2-1 from  MemberInfoBalance1";
        string SQL2 = null;
        if (ISAZ == 0)
        {
            SQL2 = "select number from MemberInfo where  Direct=";
        }
        else
        {
            SQL2 = "select number from MemberInfo where Placement=";
        }

        DataTable tabel2 = DAL.DBHelper.ExecuteDataTable(SQL);
        //bool door = true;
        string[] hasParent = new string[wltdt.Rows.Count];
        string[] parent = new string[wltdt.Rows.Count];

        int count2 = wltdt.Rows.Count;
        int t;
        int[] maxXh = new int[count2];

        if (wltdt.Rows.Count > 0)
        {
            for (int i = 0; i < wltdt.Rows.Count; i++)
            {
                maxXh[i] = Convert.ToInt32(wltdt.Rows[i]["xh"].ToString());
            }
        }


        // 冒泡
        for (int i = 0; i <= wltdt.Rows.Count - 1; i++)
        {
            for (int j = i + 1; j <= wltdt.Rows.Count - 1; j++)
            {
                if (maxXh[i] <= maxXh[j])
                { t = maxXh[i]; maxXh[i] = maxXh[j]; maxXh[j] = t; }
            }
        }
        //------------------------------------------------------------------------------------------------------------------
        int hang, lie;
        hang = maxXh[0] + 1;
        string[,] zoushi;
        string[,] reg;
        //获取有孩子的数组

        if (isAnZhi == true)
        {
            lie = 3;	// 总共3列
            zoushi = new string[hang, lie];
            reg = new string[hang, lie];
            // 初始化
            for (int m = 0; m < hang; m++)
            {
                for (int n = 0; n < lie; n++)
                {
                    zoushi[m, n] = "&nbsp;";
                }
            }
            // 赋值－标题－总共6列
            zoushi[0, 0] = "&nbsp;";
            zoushi[0, 1] = "&nbsp;";
            zoushi[0, 2] = "&nbsp;";
        }
        else
        {
            lie = 3; // 总共2列
            zoushi = new string[hang, lie];
            reg = new string[hang, lie];
            // 初始化
            for (int m = 0; m < hang; m++)
            {
                for (int n = 0; n < lie; n++)
                {
                    zoushi[m, n] = "&nbsp;";
                }
            }
            // 赋值－标题－总共6列
            zoushi[0, 0] = "&nbsp;";
            zoushi[0, 1] = "&nbsp;";
            zoushi[0, 2] = "&nbsp;";
        }

        string Font = "";
        bool judge2 = true;

        int needCellNullCount = 0;
        for (int i = 0; i < wltdt.Rows.Count; i++)
        {
            int m, n;

            m = Convert.ToInt32(wltdt.Rows[i]["xh"].ToString());
            n = Convert.ToInt32(wltdt.Rows[i]["cw"].ToString());

            if ((wltdt.Rows[i]["bh"].ToString() != "----" && needCellNullCount <= 0)) //|| isParent)
            {
                string result = wltdt.Rows[i]["register_ExpectNum"] == null || wltdt.Rows[i]["register_ExpectNum"].ToString() == "" ? ("1") : (wltdt.Rows[i]["register_ExpectNum"].ToString());
                if (Convert.ToInt32(result) >= QiShu)
                {
                    Font = "color=red";
                }
                else
                    Font = "";

                tempXianshi = "<a href=" + page + wltdt.Rows[i]["bh"].ToString() + "><font class=ls " + Font + "  >" + wltdt.Rows[i]["bh"].ToString() + "</font>&nbsp;</a>";

                tempXianshi = tempXianshi
                            + "&nbsp;&nbsp;<img height='18' src='" + GetLevel(wltdt.Rows[i]["jibie"].ToString(), 0) + "' /><br>"//Jiegou.GetLevel(wltdt.Rows[i]["jibie"].ToString()) +
                            + "<a title='" + BLL.Translation.Translate("000937", "总网分数") + "'>[" + (Convert.ToDouble(wltdt.Rows[i]["zwfenshu"].ToString())).ToString() + "]</a>"  //(Convert.ToDouble(wltdt.Rows[i]["zwfenshu"].ToString())).ToString("f2") + "]</a>"//需要翻译CommonData.GetClassTran("classes/jiegou.cs_610", "累计市场业绩")
                    //+"<a title='新增网络业绩'>["+(Convert.ToDouble(yedt.Rows[0]["CurrentTotalNetRecord"].ToString())).ToString()+"]</a>"
                    //+"<a title='新网人数'>["+(Convert.ToDouble(yedt.Rows[0]["CurrentNewNetNum"].ToString())).ToString()+"]</a>"
                    //+"<a title='总网人数'>["+(Convert.ToDouble(yedt.Rows[0]["TotalNetNum"].ToString())).ToString()+"]</a>"
                            + "";

            }
            else
            {
                tempXianshi = "";
            }
            zoushi[m, n] = tempXianshi;
        }

        for (int m = 0; m < hang; m++)
        {
            TableRow myRow2 = new TableRow();
            for (int i = 0; i < lie; i++)
            {
                TableCell myCell = new TableCell();
                if (zoushi[m, i].ToString() == "----")
                {
                    myCell.Text = "<a href=" + reg[m, i] + ">" + zoushi[m, i].ToString() + "</a>";
                    myCell.Height = 50;
                    myCell.Width = 170;
                    myCell.HorizontalAlign = Table1.HorizontalAlign;
                    myCell.Wrap = true;
                    myCell.Attributes.Add("style", "background-color:#ffffff");
                }
                else
                {
                    myCell.Text = zoushi[m, i].ToString();
                    myCell.Height = 50;
                    myCell.Width = 170;
                    myCell.HorizontalAlign = Table1.HorizontalAlign;
                    myCell.Wrap = true;
                    myCell.Attributes.Add("style", "background-color:#ffffff");
                }

                myRow2.Cells.Add(myCell);
            }
            Table1.Rows.Add(myRow2);
        }
    }
    public static void wltDatableForEng(System.Web.UI.WebControls.Table Table1, int QiShu, int showCW, bool isAnZhi, string bianhao, string page)
    {
        string shangji = Jiegou.getShangJi(isAnZhi);//生成查询的关系（anzhi或者tuijian）
        //获取序号范围			
        int ISAZ = 0;

        string tempXianshi = "";
        if (isAnZhi == true)
        {
            ISAZ = 1;
        }
        else
        {
            ISAZ = 0;
        }
        //---------------------------------------------------------------------------------------------------------------------
        SqlParameter[] para =
						{
							new SqlParameter ("@bianhao" , SqlDbType .VarChar , 20),
							new SqlParameter ("@ISAZ" , SqlDbType.Bit ),
							new SqlParameter ("@QiShu"   , SqlDbType .Int, 4),
							new SqlParameter ("@cws",SqlDbType.Int,4)
						};
        para[0].Value = bianhao;
        para[1].Value = ISAZ;
        para[2].Value = QiShu;
        para[3].Value = showCW;

        DataTable wltdt = DAL.DBHelper.ExecuteDataTable("NetTB", para, CommandType.StoredProcedure);
        int count = wltdt.Rows.Count;
        int t;
        int[] maxXh = new int[count];
        if (wltdt.Rows.Count > 0)
        {
            for (int i = 0; i < wltdt.Rows.Count; i++)
            {
                maxXh[i] = Convert.ToInt32(wltdt.Rows[i]["xh"].ToString());
            }
        }
        for (int i = 0; i <= count - 1; i++)
        {
            for (int j = i + 1; j <= count - 1; j++)
            {
                if (maxXh[i] <= maxXh[j])
                { t = maxXh[i]; maxXh[i] = maxXh[j]; maxXh[j] = t; }
            }
        }
        //------------------------------------------------------------------------------------------------------------------
        int hang, lie;
        hang = maxXh[0] + 1;
        string[,] zoushi;
        string[,] reg;
        if (isAnZhi == true)
        {
            lie = 3;	// 总共3列
            zoushi = new string[hang, lie];
            reg = new string[hang, lie];
            // 初始化
            for (int m = 0; m < hang; m++)
            {
                for (int n = 0; n < lie; n++)
                {
                    zoushi[m, n] = "&nbsp;";
                }
            }
            // 赋值－标题－总共6列
            zoushi[0, 0] = "&nbsp;";
            zoushi[0, 1] = "&nbsp;";
            zoushi[0, 2] = "&nbsp;";
        }
        else
        {
            lie = 2; // 总共2列
            zoushi = new string[hang, lie];
            reg = new string[hang, lie];
            // 初始化
            for (int m = 0; m < hang; m++)
            {
                for (int n = 0; n < lie; n++)
                {
                    zoushi[m, n] = "&nbsp;";
                }
            }
            // 赋值－标题－总共6列
            zoushi[0, 0] = "&nbsp;";
            zoushi[0, 1] = "&nbsp;";
        }

        string Font = "";
        for (int i = 0; i < wltdt.Rows.Count; i++)
        {
            int m = Convert.ToInt32(wltdt.Rows[i]["xh"].ToString());
            int n = Convert.ToInt32(wltdt.Rows[i]["cw"].ToString());

            if (wltdt.Rows[i]["bh"].ToString() != "----")
            {
                if (Convert.ToInt32(wltdt.Rows[i]["register_qishu"].ToString()) >= QiShu)
                {
                    Font = "color=red";
                }
                else
                    Font = "";

                tempXianshi = "<a href=" + page + wltdt.Rows[i]["bh"].ToString() + "><font class=ls " + Font + "  >" + wltdt.Rows[i]["bh"].ToString() + "</font>&nbsp;</a>";


                tempXianshi = tempXianshi
                    + "&nbsp;&nbsp;" + GetLevel(wltdt.Rows[i]["jibie"].ToString(), 0) //+ Jiegou.GetLevelForEng(wltdt.Rows[i]["jibie"].ToString()) + "<br>"
                    + "<a title='" + BLL.Translation.Translate("000937", "总网分数") + "'>[" + (Convert.ToDouble(wltdt.Rows[i]["zwfenshu"].ToString())).ToString("f2") + "]</a>"
                    //+"<a title='新增网络业绩'>["+(Convert.ToDouble(yedt.Rows[0]["CurrentTotalNetRecord"].ToString())).ToString()+"]</a>"
                    //+"<a title='新网人数'>["+(Convert.ToDouble(yedt.Rows[0]["CurrentNewNetNum"].ToString())).ToString()+"]</a>"
                    //+"<a title='总网人数'>["+(Convert.ToDouble(yedt.Rows[0]["TotalNetNum"].ToString())).ToString()+"]</a>"
                    + "";
            }
            else
            {
                tempXianshi = "&nbsp;";
            }
            zoushi[m, n] = tempXianshi;
        }

        for (int m = 0; m < hang; m++)
        {
            TableRow myRow2 = new TableRow();
            for (int i = 0; i < lie; i++)
            {
                TableCell myCell = new TableCell();
                if (zoushi[m, i].ToString() == "----")
                {
                    myCell.Text = "<a href=" + reg[m, i] + ">" + zoushi[m, i].ToString() + "</a>";
                    myCell.Height = 50;
                    myCell.Width = 170;
                    myCell.HorizontalAlign = Table1.HorizontalAlign;
                    myCell.Wrap = true;
                }
                else
                {
                    myCell.Text = zoushi[m, i].ToString();
                    myCell.Height = 50;
                    myCell.Width = 170;
                    myCell.HorizontalAlign = Table1.HorizontalAlign;
                    myCell.Wrap = true;
                }

                myRow2.Cells.Add(myCell);
            }
            Table1.Rows.Add(myRow2);
        }
    }
    #endregion

    #region 生成链路图
    /// <summary>
    /// 生成链路图
    /// </summary>
    /// <param name="QiShu">期数</param>
    /// <param name="isAnZhi">是否是安置关系</param>
    /// <param name="number">起点编号</param>
    /// <param name="page">地址</param>
    /// <returns>返回表</returns>
    public static DataTable Link_Map(int QiShu, bool isAnZhi, string bianhao, string page)
    {
        string shangji = Jiegou.getShangJi(isAnZhi);//生成查询的关系（anzhi或者tuijian ）

        DataTable myDataTable = new DataTable("lianxing");
        //Declare variables for DataColumn and DataRow objects.
        DataColumn myDataColumn;
        DataRow myDataRow;
        DataSet myDataSet;

        //Create new DataColumn, set DataType, ColumnName and add to DataTable.    
        myDataColumn = new DataColumn();
        myDataColumn.DataType = System.Type.GetType("System.Int32");
        myDataColumn.ColumnName = "id";
        myDataColumn.ReadOnly = true;
        myDataColumn.Unique = true;
        //Add the Column to the DataColumnCollection.
        myDataTable.Columns.Add(myDataColumn);

        //Create second column.
        myDataColumn = new DataColumn();
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataColumn.ColumnName = "xinxi";
        myDataColumn.AutoIncrement = false;
        myDataColumn.Caption = "number";
        myDataColumn.ReadOnly = false;
        myDataColumn.Unique = false;
        //Add the column to the table.
        myDataTable.Columns.Add(myDataColumn);


        //Make the ID column the primary key column.
        DataColumn[] PrimaryKeyColumns = new DataColumn[1];
        PrimaryKeyColumns[0] = myDataTable.Columns["id"];
        myDataTable.PrimaryKey = PrimaryKeyColumns;

        //Instantiate the DataSet variable.
        myDataSet = new DataSet();
        //Add the new DataTable to the DataSet.
        myDataSet.Tables.Add(myDataTable);

        //Create three new DataRow objects and add them to the DataTable
        //public  string connString = "Data Source=.;Initial Catalog=DSIS2009;Integrated Security=True";
        using (SqlConnection conn = new SqlConnection(DAL.DBHelper.connString))
        {
            conn.Open();
            string sql;
            int i = 0;
            bool mark = false;//标记是否要退出循环
            bool isTop = false;

            int type = 0;
            if (System.Web.HttpContext.Current.Session["Company"] != null)
            {
                type = 0;
            }
            else if (System.Web.HttpContext.Current.Session["Store"] != null)
            {
                type = 1;
            }
            else if (System.Web.HttpContext.Current.Session["Member"] != null)
            {
                type = 2;
            }

            string NetWorkDisplayStatus = "";
            DataTable dt = BLL.CommonClass.CommonDataBLL.GetNetWorkDisplayStatus(type);
            foreach (DataRow row in dt.Rows)
            {
                NetWorkDisplayStatus += row["flag"].ToString();
            }

            bool Display1 = NetWorkDisplayStatus.Substring(0, 1) == "1";
            bool Display2 = NetWorkDisplayStatus.Substring(1, 1) == "1";
            bool Display3 = NetWorkDisplayStatus.Substring(2, 1) == "1";
            bool Display4 = NetWorkDisplayStatus.Substring(3, 1) == "1";
            bool Display5 = NetWorkDisplayStatus.Substring(4, 1) == "1";
            bool Display6 = NetWorkDisplayStatus.Substring(5, 1) == "1";

            string tempXianshi = "";
            while (bianhao.Length > 0)
            {
                sql = "select H2.*,memberInfo.ExpectNum,memberInfo.number,memberInfo.PetName,memberInfo.Name,memberInfo." + shangji + @" as shangji,h2.level from memberInfo,MemberInfoBalance" + QiShu.ToString() + " as H2 where memberInfo.number = H2.number and memberInfo.number = '" + bianhao + "'";

                SqlCommand cmd = new SqlCommand(sql);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                cmd.CommandText = sql;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    //生成信息
                    string Font = "";
                    tempXianshi = "";
                    if (Convert.ToInt32(dr["ExpectNum"]) >= QiShu)
                    {
                        Font = "color=red";
                        tempXianshi = "<IMG  src=\"images/pers02.jpg\" >";
                    }
                    else
                    {
                        tempXianshi = "<IMG  src=\"images/pers01.jpg\" >";
                    }

                    tempXianshi += "<font " + Font + " class=ls><a href=" + page + dr["number"].ToString() + " title='" + BLL.Translation.Translate("004196", "编号和呢称") + "'>" + dr["number"].ToString() + "&nbsp;" + dr["PetName"].ToString() + "</a>";
                    //tempXianshi += "<font " + Font + " class=ls><a href=" + page + dr["number"].ToString() + " title='" + CommonData.GetClassTran("classes/jiegou.cs_443", "编号和呢称") + "'>" + dr["number"].ToString() + "&nbsp;" + dr["PetName"].ToString() + "</a>";


                    //if (dr["number"].ToString().Trim() == DAL.DBHelper.ExecuteScalar("select top 1 number from manage where defaultmanager = 1").ToString())
                    //{
                    //    tempXianshi += ((Display1) ? ("<a title='" + BLL.Translation.Translate("000046", "级别") + "'>" + BLL.Translation.Translate("001824", "公司") + "</a>&nbsp;") : "") + "</a>";
                    //    //tempXianshi += ((Display_jibie) ? ("<a title='" + CommonData.GetClassTran("classes/jiegou.cs_448", "级别") + "'></a>&nbsp;") : "") + "</a>";
                    //}
                    //else
                    tempXianshi += ((Display1) ? ("<a title='" + BLL.Translation.Translate("000046", "级别") + "'><img height='18' src='" + GetLevel(dr["level"].ToString(), 0) + "'/></a>&nbsp;") : "") + "</a>";

                    tempXianshi = tempXianshi
                        + ((Display2) ? dr["CurrentOneMark"].ToString().Length > 0 ? ("<a title='" + BLL.Translation.Translate("000939", "新个分数") + "'>[" + Convert.ToDecimal(dr["CurrentOneMark"]).ToString("f0") + "]</a>") : "" : "")
                        + ((Display3) ? dr["CurrentTotalNetRecord"].ToString().Length > 0 ? ("<a title='" + BLL.Translation.Translate("000936", "新网分数") + "'>  [" + Convert.ToDecimal(dr["CurrentTotalNetRecord"]).ToString("f0") + "]</a>") : "" : "")
                        + ((Display4) ? dr["CurrentNewNetNum"].ToString().Length > 0 ? ("<a title='" + BLL.Translation.Translate("000934", "新网人数") + "'>[" + Convert.ToDecimal(dr["CurrentNewNetNum"]).ToString("f0") + "]</a>") : "" : "")
                        + ((Display5) ? dr["TotalNetNum"].ToString().Length > 0 ? ("<a title='" + BLL.Translation.Translate("000933", "总网人数") + "'>[" + Convert.ToDecimal(dr["TotalNetNum"]).ToString("f0") + "]</a>") : "" : "")
                        + ((Display6) ? dr["TotalNetRecord"].ToString().Length > 0 ? ("<a title='" + BLL.Translation.Translate("000937", "总网分数") + "'>[" + Convert.ToDecimal(dr["TotalNetRecord"]).ToString("f0") + "]</a>") : "" : "")
                        + "</font>";

                    myDataRow = myDataTable.NewRow();
                    i += 1;
                    myDataRow["id"] = i;
                    myDataRow["xinxi"] = tempXianshi;
                    myDataTable.Rows.InsertAt(myDataRow, 0);
                    if (type == 0)
                    {
                        isTop = BLL.CommonClass.CommonDataBLL.GetViewManage1(System.Web.HttpContext.Current.Session["Company"].ToString(), isAnZhi, bianhao);
                    }
                    //判断是否退出循环
                    if (bianhao == Jiegou.getMemberBH() || bianhao == DAL.DBHelper.ExecuteScalar("select top 1 number from manage where defaultmanager = 1").ToString() || bianhao == "1111111111")
                    {
                        mark = true;
                    }
                    else
                    {
                        if (!isTop)
                        {
                            //查找上级
                            bianhao = dr["shangji"].ToString();

                            if (bianhao.Length > 0)
                            {
                                myDataRow = myDataTable.NewRow();
                                i += 1;
                                myDataRow["id"] = i;
                                myDataRow["xinxi"] = "&nbsp;&nbsp;&nbsp;<IMG  src=\"images/arrow_top.jpg\" >"; ;
                                myDataTable.Rows.InsertAt(myDataRow, 0);
                            }
                        }
                    }
                }
                else
                {
                    bianhao = "";
                }
                dr.Close();
                //如果已经到达小区顶点就退出．
                if (mark)
                {
                    break;
                }
                if (isTop)
                    break;
            }
            conn.Close();
        }
        return myDataTable;
    }

    #endregion

    #region 判断是否是合法编号
    /// <summary>
    /// 是否是合法编号（该编号是否属于调用他的网络）
    /// </summary>
    /// <param name="sonBH">下级编号</param>
    /// <param name="fatherBH">上级编号</param>
    /// <param name="isAnZhi">是否是安置关系</param>
    /// <param name="qishu">期数</param>
    /// <returns>返回是否是合法</returns>
    public static bool isValid(string sonBH, string fatherBH, bool isAnZhi, int qishu)
    {
        bool temp = false;//是否是合法
        if (sonBH.Equals(fatherBH))//如果两者不相等则进行下一步判断，否则就是合法的．
        {
            return true;
        }
        string nettype = (isAnZhi == true) ? "Placement" : "Direct";
        string manageId = BLL.CommonClass.CommonDataBLL.getManageID(3);
        if (nettype == "Placement")
        {
            sonBH = DAL.MemberInfoDAL.GetPlacement(sonBH, qishu.ToString());
            while (sonBH.Length > 0)            //往上找，判断是不是在father的网络内，在就是合法，不在是非法．
            {
                if (sonBH == fatherBH)
                {
                    temp = true; break;
                }
                if (sonBH == manageId || sonBH == "1111111111")
                {
                    temp = false; break;
                }
                sonBH = DAL.MemberInfoDAL.GetPlacement(sonBH, qishu.ToString());
            }

        }
        else
        {
            sonBH = DAL.MemberInfoDAL.GetDirect(sonBH, qishu.ToString());
            while (sonBH.Length > 0)            //往上找，判断是不是在father的网络内，在就是合法，不在是非法．
            {
                if (sonBH == fatherBH)
                {
                    temp = true; break;
                }
                if (sonBH == manageId || sonBH == "1111111111")
                {
                    temp = false; break;
                }
                sonBH = DAL.MemberInfoDAL.GetDirect(sonBH, qishu.ToString());
            }

        }

        return temp;
    }

    #endregion

    /// <summary>
    /// 对应级别。
    /// </summary>
    /// <param name="level">根据级别对应资格</param>
    public static string GetLevel(string level, int levelType)
    {
        string chaBuMoney = "";
        chaBuMoney = DAL.CommonDataDAL.GetLevel(level, levelType);
        return chaBuMoney;
    }

    /// <summary>
    /// 对应级别。
    /// </summary>
    /// <param name="level">根据级别对应资格</param>
    public static string GetLevel1(string level, int levelType)
    {
        string chaBuMoney = "";
        chaBuMoney = DAL.CommonDataDAL.GetLevel1(level, levelType);
        return chaBuMoney;
    }






















    /// <summary>
    /// 常用推荐网络图
    /// </summary>
    /// <param name="Div_TuiJian">ＤＩＶ</param>
    /// <param name="TopBianhao">首点编号</param>
    /// <param name="ExpectNum">期数</param>
    /// <returns>返回Table形式的字符串</returns>
    public static void Direct_Table2(System.Web.UI.HtmlControls.HtmlGenericControl Div_TuiJian, string TopBianhao, string qishu, bool isAnZhi)
    {
        string temp = string.Empty;//字段（推荐编号或安置编号）
        temp = isAnZhi == true ? "Placement" : "Direct";

        string str = "select b.number,a.level,b.ExpectNum,b.Direct  from MemberInfoBalance" + qishu + " a,memberInfo b where a.number=b.number";
        DataTable dt = DAL.DBHelper.ExecuteDataTable(str);

        DataTable dt_tuijian;
        DataTable dt_tuijian1;
        DataRow[] row;
        int div_count = 0;
        int count = 0;
        int i = 0;
        int p = 0;
        int k = 0;
        int jb = 0;

        string jibie = "";
        string colors = "";
        string sql = "";
        string tuijian = "";

        if (TopBianhao == "")
        {
            tuijian = DAL.DBHelper.ExecuteScalar("select number from manage where defaultmanager = 1").ToString();
        }
        else
            tuijian = TopBianhao;

        sql = "select  *  from   memberInfo   where  " + temp + "='" + tuijian + "' and  ExpectNum<='" + qishu + "' ";
        dt_tuijian = DAL.DBHelper.ExecuteDataTable(sql);
        count = dt_tuijian.Rows.Count;

        div_count += count;
        for (i = 0; i < dt_tuijian.Rows.Count; i++)
        {
            dt_tuijian1 = DAL.DBHelper.ExecuteDataTable("select *  from  memberInfo  where  " + temp + "='" + dt_tuijian.Rows[i]["number"] + "' and  ExpectNum<='" + qishu + "' and  Direct<> (select top 1 number from manage where defaultmanager = 1)");
            div_count += dt_tuijian1.Rows.Count;
        }

        string[] bianhaohtml = new string[div_count + 10];
        string[] jibiehtml = new string[div_count + 10];
        string[] color = new string[div_count + 10];
        for (p = 0; p < div_count; p++)
        {
            bianhaohtml[p] = "&nbsp;";
            jibiehtml[p] = "&nbsp";
            color[p] = "#b3b3b3";
        }
        BLL.CommonClass.CommonDataBLL commonDataBLL = new BLL.CommonClass.CommonDataBLL();
        #region 一层
        row = dt.Select("number='" + TopBianhao + "'", "number");
        if (row.Length > 0)
        {
            if (row[0]["number"].ToString().Trim() == "") { jibie = ""; }
            else
            {
                jb = row[0]["level"] == System.DBNull.Value ? 0 : int.Parse(row[0]["level"].ToString());
                //jb = row[0]["level"] == System.DBNull.Value ? 0 : int.Parse(row[0]["level"].ToString());
                jibie = GetLevel(jb.ToString(), 0);
            }

            colors = "#00cc00";
            if (row[0]["ExpectNum"].ToString() == CommonDataBLL.getMaxqishu().ToString())
            {
                colors = "red";
            }
            if (isAnZhi)
                bianhaohtml[0] = "<a href=MemberNetMap.aspx?net=az&SelectGrass=" + qishu + "&bianhao=" + row[0]["number"].ToString() + "><font color=#333333>" + row[0]["number"].ToString() + "</font></a>";
            else
                bianhaohtml[0] = "<a href=MemberNetMap.aspx?net=tj&SelectGrass=" + qishu + "&bianhao=" + row[0]["number"].ToString() + "><font color=#333333>" + row[0]["number"].ToString() + "</font></a>";
            jibiehtml[0] = jibie;
            color[0] = colors;
        }
        #endregion
        #region 二层
        for (i = 0; i < dt_tuijian.Rows.Count; i++)
        {
            string helper = "number='" + dt_tuijian.Rows[i]["number"] + "'";
            row = dt.Select(helper, "number");
            if (row.Length > 0)
            {

                if (row[0]["number"].ToString().Trim() == "") { jibie = ""; }
                else
                {
                    string helper2 = "" + row[0]["level"];
                    jb = row[0]["level"] == System.DBNull.Value ? 0 : int.Parse(row[0]["level"].ToString());
                    //jb = row[0]["level"] == System.DBNull.Value ? 0 : int.Parse(row[0]["level"].ToString());
                    jibie = GetLevel(jb.ToString(), 0);
                }
                colors = "#00cc00";
                if (row[0]["ExpectNum"].ToString() == CommonDataBLL.getMaxqishu().ToString())
                {
                    colors = "red";
                }
                string helper33 = row[0]["number"] + "";
                if (isAnZhi)
                    bianhaohtml[i + 1] = "<a href=MemberNetMap.aspx?net=az&SelectGrass=" + qishu + "&bianhao=" + row[0]["number"].ToString() + "><font color=#333333>" + row[0]["number"].ToString() + "</font></a>";
                else
                    bianhaohtml[i + 1] = "<a href=MemberNetMap.aspx?net=tj&SelectGrass=" + qishu + "&bianhao=" + row[0]["number"].ToString() + "><font color=#333333>" + row[0]["number"].ToString() + "</font></a>";
                jibiehtml[i + 1] = jibie;
                color[i + 1] = colors;
            }
        }
        #endregion

        string strhtml1 = "";
        int _count1 = dt_tuijian.Rows.Count;//第二层线的个数(上线推荐的个数)
        int _count2 = 0;
        int width1 = 0;                     //第二层线的宽度
        int _count3 = 0;
        int width3 = 0;
        int width4 = 0;
        int _count4 = 0;
        for (i = 0; i < _count1; i++)
        {
            if (_count1 > 1)
            {
                dt_tuijian1 = DAL.DBHelper.ExecuteDataTable("select *  from  memberInfo  where  " + temp + "='" + dt_tuijian.Rows[i]["number"] + "' and  ExpectNum<='" + qishu + "'");
                _count4 = dt_tuijian1.Rows.Count;
                if (_count4 > 1)
                {
                    _count2 += _count4;
                    _count3 += _count4;
                }
                else
                {
                    _count2++;
                    _count3++;
                }
            }
            else
                _count2 = _count1;

            if (i == 0)
            {
                //width3第二层线头部的宽度
                if (_count4 <= 1)
                    width3 = 50;
                else
                    width3 = _count4 * 50;
            }
            if (i == (_count1 - 1))
            {
                //width4第二层线头部的宽度
                if (_count4 <= 1)
                    width4 = 50;
                else
                    width4 = _count4 * 50;
            }
        }
        width1 = _count2 * 100 - width3 - width4;//第二层线中间部分的宽度


        strhtml1 = "<table  border=\"0\" cellspacing=\"0\" cellpadding=\"0\"> " +
                    "<tr align=\"center\" valign=\"top\">" +
                        "<td height=\"63\" colspan=\"" + _count1 + "\" align=\"center\">" +
                            "<table width=\"100\" height=\"63\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">" +
                                "<tr>" +
                                    "<td height=\"47\" colspan=\"3\" valign=\"top\">" +
                                        "<TABLE  borderColor=#CCCCCC cellSpacing=0  borderColorDark=#ffffff cellPadding=0 height=100%  width=100 border=1>" +
                                            "<TR>" +
                                                "<TD align=\"center\" bgColor=" + color[0] + " colSpan=\"2\" height=\"20\">" + bianhaohtml[0] + "</TD>" +
                                            "</TR>" +
                                            "<TR>" +
                                                "<TD align=\"center\" bgColor=\"#ffffcc\" colSpan=\"2\" height=\"20\"><font size=2px><img height='18' src='" + jibiehtml[0] + "'></font></TD>" +//" + jibiehtml[0] + "
                                            "</TR>" +
                                            "<TR>" +
                                                "<TD align=\"center\" colSpan=\"2\" height=\"17\"><input type=\"button\" id=\"selected\" value=\"请选择\" onclick=\"OnChose('df')\"/></TD>" +
                                            "</TR>" +
                                        "</TABLE>" +
                                    "</td>" +
                                "</tr>";
        if (_count1 > 0)
        {
            strhtml1 += "<tr>" +
                           "<td width=\"49\" height=\"16\"></td>" +
                           "<td width=\"2\" background=\"images/images02_02.gif\"  > </td>" +
                           "<td width=\"49\"></td>" +
                       "</tr>";

        }
        strhtml1 += "</table>" +
                "</td>" +
            "</tr>";
        if (_count1 > 0)
        {
            if (_count2 > 1)
            {
                strhtml1 += "<tr align=\"center\" valign=\"top\">" +
                            "<td colspan=\"" + _count1 + "\" height=\"2\" >" +
                                "<table  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" +
                                    "<tr>" +
                                        "<td height=\"2\"  width=\"" + width3 + "\" ></td>" +
                                        "<td height=\"2\"  width=\"" + width1 + "\"  background=\"images/images03_05.gif\"></td>" +
                                        "<td height=\"2\"  width=\"" + width4 + "\"  ></td>" +
                                    "</tr>" +
                                "</table>" +
                            "</td>" +
                        "</tr>";
            }
            else
            {

                strhtml1 += "<tr align=\"center\" valign=\"top\">" +
                            "<td colspan=\"" + _count1 + "\" height=\"2\" align=\"center\">" +
                                "<table  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" +
                                    "<tr>" +
                                        "<td height=\"2\" width=\"2\"  background=\"images/images03_05.gif\"></td>" +
                                    "</tr>" +
                                "</table>" +
                            "</td>" +
                        "</tr>";
            }
        }

        strhtml1 += "<tr valign=\"top\">";

        int t = 0;
        int _count = 0;
        int width2 = 0;
        for (i = 0; i < dt_tuijian.Rows.Count; i++)
        {
            dt_tuijian1 = DAL.DBHelper.ExecuteDataTable("select *  from  memberInfo  where  " + temp + "='" + dt_tuijian.Rows[i]["number"] + "'  and  ExpectNum<='" + qishu + "'");
            _count = dt_tuijian1.Rows.Count;
            width2 = (_count - 1) * 100;//第三层线的宽度
            strhtml1 += "<td  align=\"center\">" +
                        "<table  border=\"0\" cellspacing=\"0\" cellpadding=\"0\" >" +
                            "<tr align=\"center\" valign=\"top\">" +
                                "<td height=\"79\" colspan=\"3\">" +
                                    "<table  height=\"79\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">" +
                                        "<tr>" +
                                            "<td width=\"49\" height=\"16\"></td>" +
                                            "<td width=\"2\"  background=\"images/images02_02.gif\"></td>" +
                                            "<td width=\"49\"></td>" +
                                        "</tr>" +
                                        "<tr>" +
                                            "<td height=\"47\" colspan=\"3\" valign=\"top\">" +
                                                 "<TABLE  borderColor=#CCCCCC cellSpacing=0  borderColorDark=#ffffff cellPadding=0 height=100%  width=100 border=1>" +
                                                    "<TR>" +
                                                        "<TD align=\"center\" bgColor=" + color[i + 1] + " colSpan=\"2\" height=\"20\">" + bianhaohtml[i + 1] + "</TD>" +
                                                    "</TR>" +
                                                    "<TR>" +
                                                        "<TD align=\"center\" bgColor=\"#ffffcc\" colSpan=\"2\" height=\"20\"><font size=2px><img height='18' src='" + jibiehtml[i + 1] + "'></font></TD>" +
                                                    "</TR>" +
                                                    "<TR>" +
                                                        "<TD align=\"center\" colSpan=\"2\" height=\"17\"><input type=\"button\" id=\"selected\" value=\"请选择\"  onclick=\"OnChose('" + dt_tuijian.Rows[i]["number"] + "')\"/></TD>" +
                                                    "</TR>" +
                                                "</TABLE>" +
                                            "</td>" +
                                        "</tr>";
            if (_count > 0)
            {
                strhtml1 += "<tr>" +
                                "<td width=\"49\" height=\"16\"></td>" +
                                "<td width=\"2\"  background=\"images/images02_02.gif\"></td>" +
                                "<td width=\"49\"></td>" +
                            "</tr>";
            }

            strhtml1 += "</table>" +
                    "</td>" +
                 "</tr>";

            if (_count > 0)
            {
                if (_count == 1)
                {
                    strhtml1 += "<tr>" +
                                "<td width=\"49\" height=\"2\"></td>" +
                                "<td background=\"images/images03_05.gif\"></td>" +
                                "<td width=\"49\"></td>" +
                                "</tr>";
                }
                else
                {
                    strhtml1 += "<tr>" +
                                "<td width=\"49\" height=\"2\"></td>" +
                                "<td width=\"" + width2 + "\" background=\"images/images03_05.gif\"></td>" +
                                "<td width=\"49\"></td>" +
                                "</tr>";
                }
                strhtml1 += "<tr vAlign=\"top\">" +
                             "<td colSpan=\"3\" align=\"center\">" +
                                "<table cellSpacing=\"0\" cellPadding=\"0\"  border=\"0\">" +
                                    "<tr vAlign=\"top\">";
            }
            if (t == 1)
                count += dt_tuijian1.Rows.Count;
            t = 1;
            if (_count > 0)
            {
                strhtml1 += "</tr>" +
                    "</table>" +
                    "</td>" +
                    "</tr>";
            }

            strhtml1 += "</table>" +
                       "</td>";


        }
        strhtml1 += "</td>" +
                  "</tr>" +
                  "</table>" +
                  "</td>";
        Div_TuiJian.InnerHtml = strhtml1 + "</tr></table>";
    }

    /// <summary>
    /// 网络图中 在DIV中生成两个点位  
    /// </summary>
    /// <returns></returns>
    public static string CreateTableInDiv(DataTable dtFloor, int floorCount,string topNum)
    {
        string resultHtml = "";
        int count = 1;
        foreach (DataRow row in dtFloor.Rows)
        {
            if (count < 3)
            {
      
                string divClass = "box left";
                string divStyle = " style='float:left;' ";
                if (count == 1 && row["District"].ToString() == "2")
                {
                    resultHtml += "<div class='" + divClass + "' " + divStyle + ">";
                    resultHtml += "<table class='tb'>";
                    resultHtml += "<tr>";
                    resultHtml += "<td align='center' onmousemove='ChangeColor(1,this);' onmouseout='ChangeColor(0,this);' onclick='ChangeLeftOrRight(\"" + topNum + "\"," + count + ",0);' >" + (GetTran1("007297", "注册")) + "</td>";
                    resultHtml += "</tr>";
                    resultHtml += "</table>";
                    resultHtml += "</div>";

                    count++;
                }

                string chageLine = " onmouseover='ChangeLine(\"" + row["Number"].ToString() + "\");' ";
                divStyle = " ";
                string tdClick = " ";
                if (count == 2)
                {
                    divClass = "box right";
                }
                if (floorCount == 3 && count == 1)
                {
                    divStyle = " style='float:left;' ";
                    tdClick = " onclick='ClickThreeFloor(\"" + row["Number"].ToString() + "\")'; ";
                }
                else if (floorCount == 3 && count == 2)
                {
                    divStyle = " style='float:right;' ";
                    tdClick = " onclick='ClickThreeFloor(\"" + row["Number"].ToString() + "\")'; ";
                }
                string jb = row["levelstr"].ToString();
                resultHtml += "<div class='" + divClass + "' " + divStyle + " >";
                resultHtml += "<table class='tb' " + chageLine + " >";
                resultHtml += "<tr>";
                resultHtml += "<td colspan='2' onmousemove='ChangeColor(1,this);' onmouseout='ChangeColor(0,this);' " + tdClick + " >" + (GetTran1("001195", "编号")) + "：" + row["Number"].ToString() + "</td>";
                resultHtml += "</tr>";
                resultHtml += "<tr>";
                resultHtml += "<td colspan='2' onmousemove='ChangeColor(1,this);' onmouseout='ChangeColor(0,this);'>" + (GetTran1("001400", "昵称")) + "：" + row["petName"].ToString() + "</td>";
                resultHtml += "</tr>";
                resultHtml += "<tr>";
                resultHtml += "<td colspan='2' onmousemove='ChangeColor(1,this);' onmouseout='ChangeColor(0,this);'> " +  jb  + "</td>";
                resultHtml += "</tr>";
                resultHtml += "<tr>";
                resultHtml += "<td style='width:50%' onmousemove='ChangeColor(1,this);' onmouseout='ChangeColor(0,this);' onclick='ChangeLeftOrRight(\"" + row["Number"].ToString() + "\",1,1);' >" + (GetTran1("8114", "极左")) + "</td><td onmousemove='ChangeColor(1,this);' onmouseout='ChangeColor(0,this);' onclick='ChangeLeftOrRight(\"" + row["Number"].ToString() + "\",2,1);' >" + (GetTran1("8115", "极右")) + "</td>";
                resultHtml += "</tr>";
                resultHtml += "</table>";
                resultHtml += "</div>";
            }
            else
            {
                break;
            }
            count++;
        }
        for (int i = count; i < 3; i++)
        {
            string divClass = "box left";
            string divStyle = " ";
            if (i == 2)
            {
                divClass = "box right";
            }
            if (floorCount == 3 && i == 1)
            {
                divStyle = " style='float:left;' ";
            }
            else if (floorCount == 3 && i == 2)
            {
                divStyle = " style='float:right;' ";
            }

            resultHtml += "<div class='" + divClass + "' " + divStyle + ">";
            resultHtml += "<table class='tb'>";
            resultHtml += "<tr>";
            resultHtml += "<td align='center' onmousemove='ChangeColor(1,this);' onmouseout='ChangeColor(0,this);' onclick='ChangeLeftOrRight(\"" + topNum + "\"," + i + ",0);' >" + (GetTran1("007297", "注册")) + "</td>";
            resultHtml += "</tr>";
            resultHtml += "</table>";
            resultHtml += "</div>";
        }

        return resultHtml;
    }

}
