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
using Model.Other;

public partial class ReFurbish : BLL.TranslationBase
{
    protected string msg;
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        //if (Convert.ToDateTime(Session["ReFurbish_Timeout"]) < DateTime.Now)
        //{
        //    this.closeWindow();//登陆超时  退出
        //    return;
        //}

        //if (Session["Company"] == null && Session["Store"] == null && Session["Member"] == null && Session["Branch"] == null)
        //{
        //    this.closeWindow();// Session 超时退出
        //    return;
        //}

        //if (Application["jinzhi"] != null)
        //{
        //    if (Session["Branch"] != null && (!BlackListBLL.GetSystem("B")))
        //    {
        //        this.LimitWindow();
        //        return;  //登陆设置 分公司登陆 退出
        //    }

        //    if (Session["Member"] != null && (!BlackListBLL.GetSystem("H")))
        //    {
        //        this.LimitWindow();
        //        return;  //登陆设置 会员登陆 退出
        //    }

        //    if (Session["Store"] != null && (!BlackListBLL.GetSystem("D")))
        //    {
        //        this.LimitWindow();
        //        return;  //登陆设置 店铺登陆 退出
        //    }

        //    if (Session["Company"] != null && Session["Company"].ToString() != "" && (!BlackListBLL.GetSystem("G")))
        //    {
        //        this.LimitWindow();
        //        return;  //登陆设置 管理员退出  除了''
        //    }

        //    if (Session["Company"] != null && Session["permission"] != null && Application["jinzhi"].ToString().IndexOf("J") >= 0) // 'J'是结算时的状态
        //    {
        //        Hashtable table = (Hashtable)HttpContext.Current.Session["permission"];
        //        if (!table.Contains(EnumCompanyPermission.FinanceJiesuan))
        //        {
        //            this.closeWindow();
        //            return;  //结算时 没有结算权限的管理员退出					
        //        }
        //    }
        //}

        ////会员被注销时，自动退出系统 
        //if (Session["Member"] != null)
        //{
        //    if (DAL.CommonDataDAL.GetIsActive(Session["Member"].ToString()))
        //    {
        //        this.closeWindow();
        //        return;
        //    }
        //}

        //string bianhao = "";
        //int UserType = -1;

        //if (Session["Member"] != null)
        //{
        //    bianhao = Session["Member"].ToString();
        //    UserType = 0;
        //}
        //else if (Session["Store"] != null)
        //{
        //    bianhao = Session["Store"].ToString();
        //    UserType = 4;
        //}
        //else if (Session["Company"] != null)
        //{
        //    bianhao = Session["Company"].ToString();
        //    UserType = 2;
        //}
        //else if (Session["Branch"] != null)
        //{
        //    bianhao = Session["Branch"].ToString();
        //    UserType = 3;
        //}

        //// 黑名单处理 开始

        //string[] SecPostion = Request.ServerVariables["REMOTE_ADDR"].ToString().Split('.');//客户IP地址

        ////string ipAddress = SecPostion[0] + "." + SecPostion[1];
        //string ipAddress = Request.ServerVariables["REMOTE_ADDR"].ToString();//客户IP地址
        //try
        //{
        //    if (bianhao != "" && 0 < BlackListBLL.GetLikeIPCount(ipAddress))
        //    {
        //        this.LimitWindow();
        //        return;
        //    }
        //}
        //catch
        //{
        //    return;
        //}
        ////限制区域登陆
        //try
        //{
        //    if (bianhao != "" && BlackListBLL.GetLikeAddress(bianhao))
        //    {
        //        this.LimitWindow();
        //        return;
        //    }
        //}
        //catch
        //{
        //    return;
        //}

        //if (bianhao == "" || UserType == -1) return;
        //try
        //{
        //    if (0 < BlackListBLL.GetLikeIPCount(UserType, bianhao))
        //    {
        //        this.LimitWindow();
        //        return;
        //    }
        //}
        //catch
        //{
        //    return;
        //}
        //// 黑名单处理 结束 	
        //if (BLL.CommonClass.Login.isDenyLogin())
        ////限时登陆
        //{
        //    this.LimitWindow();
        //    return;
        //}
        string type = Request.Params["type"];
        if (type != null)
        {
            string[] res = type.Split(',');
            if (res.Length > 1)
            {
                if (res[0] == "2")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('限制登录,请与管理员联系！," + res[1] + "');$('#tiaoz').show();", false);
                    return;
                }
                else if (res[0] == "1")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('登陆超时,请重新登陆...');$('#tiaoz').show();", false);
                    return;
                }
            }
            else
            {
                if (type == "2")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('限制登录,请与管理员联系！');$('#tiaoz').show();", false);
                    return;
                }
                else if (type == "1")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('登陆超时,请重新登陆...');$('#tiaoz').show();", true);
                    return;
                }

            }
        }
    }
}