<%@ WebHandler Language="C#" Class="GradingBind" %>

using System;
using System.Web;
using System.Data;
using BLL.CommonClass;
using System.Text;
public class GradingBind : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string number = context.Request.QueryString["number"] == "" ? "" : context.Request.QueryString["number"];
        string type = context.Request.QueryString["type"] == "" ? "" : context.Request.QueryString["type"];
        string company = context.Request.QueryString["company"] == "" ? "" : context.Request.QueryString["company"];
        try
        {
            if (number != "" && type != "")
            {
                context.Response.Write(DataBind(number, type, company));
            }
            else
            {
                context.Response.Write("0,无,无");
            }
        }
        catch { context.Response.Write("0,无,无"); }

    }

    private string DataBind(string number, string type, string company)
    {
        string ret = "-3,无,无";
        if (number == "")
        {
            ret = "-1,无,无";//编号不能为空
        }
        else
        {
            if (type == "Member")
            {
                DataTable dt = CommonDataBLL.GetBalanceLevel(number);
                if (dt.Rows.Count == 0)
                {
                    ret = "-2,无,无";//编号不存在
                }
                else
                {
                    int i = Convert.ToInt32(dt.Rows[0]["level"].ToString());
                    string HowMuch = DAL.DBHelper.ExecuteScalar("select isnull(levelstr,'无') as levelstr from bsco_level where levelint='" + i.ToString() + "' and levelflag=0").ToString();
                    string name = BLL.other.Company.StoreRegisterBLL.GetMemberName(number);
                    ret = i.ToString() + "," + HowMuch + "," + name;
                }
            }
            else if (type == "Store")
            {
                DataTable dt = CommonDataBLL.GetLevel("StoreInfo", number);
                if (dt.Rows.Count == 0)
                {
                    ret = "-2" + ",无";
                }
                else
                {
                    string howMuch = null;
                    int i = Convert.ToInt32(dt.Rows[0]["StoreLevelInt"].ToString());
                    howMuch = DAL.DBHelper.ExecuteScalar("select isnull(levelstr,'无') as levelstr from bsco_level where levelint='" + i.ToString() + "' and levelflag=1").ToString();
                    string name = DAL.DBHelper.ExecuteScalar("select isnull(name,'无') as name from storeinfo where storeid='" + number + "'").ToString();
                    ret = i.ToString() + "," + howMuch + "," + name;
                }
            }
            else
            {
                ret = "-3,无,无";
            }
        }
        return ret;
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}