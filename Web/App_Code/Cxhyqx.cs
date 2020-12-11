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
/// Cxhyqx 的摘要说明
/// </summary>
public class Cxhyqx
{
    public Cxhyqx()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }


    public static bool GetCxhyqx(string number, int qxid)
    {

        string sql2 = "select  * from memberinfo where number='" + number + "' and MemberState=1 ";
        DataTable dt2 = DBHelper.ExecuteDataTable(sql2);
        if (dt2.Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
       
    }

}