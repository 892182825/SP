using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

/// <summary>
/// Facility 的摘要说明

/// </summary>
public class Facility
{
	public Facility()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public static string GetUserID()
    {
        string id = "";
        if (System.Web.HttpContext.Current.Request.QueryString["id"]!=null)
        {
            id = System.Web.HttpContext.Current.Request.QueryString["id"].ToString();
            if (!string.IsNullOrEmpty(id))
            {
                if (id.EndsWith("#"))
                    return id.Substring(0, id.Length - 1);
                else
                {
                    return id;
                }
            }
        }
        if (System.Web.HttpContext.Current.Session["Member"]!=null)
        {
            id = System.Web.HttpContext.Current.Session["Member"].ToString();
        }
        
        return id;

    }

    public static string GetUserID2()
    {

        string id = System.Web.HttpContext.Current.Request.QueryString["id"];
        if (!string.IsNullOrEmpty(id))
        {
            if (id.EndsWith("#"))
                return id.Substring(0, id.Length - 1);
        }
        
        return id;

    }

    public static string GetColor()
    {
        SqlParameter[] param = new SqlParameter[] 
        {
            new SqlParameter("@uid",GetUserID ())
        };

        return (DAL.DBHelper.ExecuteScalar("select ZhuJianBackGround from UserWangZhan where UserID=@uid", param, CommandType.Text) + "").Replace("*", "#");
    }

    //获取试用天数
    public static int GetPara1()
    {
        int para1 = 7;
        DataTable dt2 = DAL.DBHelper.ExecuteDataTable("select para1 from ParaTb");
        if (!string.IsNullOrEmpty(dt2.Rows[0][0].ToString()))
        {
            para1 = Convert.ToInt32(dt2.Rows[0][0].ToString());
        }

        return para1;
    }

    //获取个人空间形象照
    public static string GetImg()
    {
        string strSrc = "Images/default.gif";
        SqlParameter[] param = new SqlParameter[] 
        {
            new SqlParameter("@uid",GetUserID ())
        };

       object obj= DAL.DBHelper.ExecuteScalar("select spaceImg from UserWangZhan where UserID=@uid", param, CommandType.Text);

       if (obj!=null)
       {
           if (string.IsNullOrEmpty(obj.ToString()))
           {
               strSrc = "photo/" + obj.ToString();
           }
       }

       return strSrc;
    
    }

    public static string GetImg(string bh)
    {
        string strSrc = "Images/default.gif";
        SqlParameter[] param = new SqlParameter[] 
        {
            new SqlParameter("@uid",bh)
        };

        object obj = DAL.DBHelper.ExecuteScalar("select spaceImg from UserWangZhan where UserID=@uid", param, CommandType.Text);

        if (obj != null)
        {
            if (!string.IsNullOrEmpty(obj.ToString()))
            {
                strSrc = "photo/" + obj.ToString();
            }
        }

        return strSrc;

    }
}
