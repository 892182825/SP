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

using BLL.CommonClass;
using BLL.Registration_declarations;
using System.Collections.Generic;

public partial class Member_ShowNetworkBiaoGeView : BLL.TranslationBase
{
    BLL.Registration_declarations.DetialQueryBLL detialQueryBLL = new BLL.Registration_declarations.DetialQueryBLL();
    BLL.Registration_declarations.RegistermemberBLL registermemberBLL = new BLL.Registration_declarations.RegistermemberBLL();

    protected void Page_Load(object sender, System.EventArgs e)
    {
        //检查相应权限
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        //Permissions.CheckMemberPermission();

        if (Request.QueryString["bh"] != null)
        {
            Session["jgbh"] = Request.QueryString["bh"];
        }

        //检查编号是否合法
        bool flag = registermemberBLL.isNet(Session["jglx"].ToString(), this.getBH(SfType.getType()), Convert.ToString(Session["jgbh"]));
        // if (!Jiegou.isValid(Convert.ToString(Session["jgbh"]), SfType.getType(),isAnZhi(), Convert.ToInt32(Session["jgqs"])))
        if (!flag)
        {
            Response.Write("你不能查看该网络！");
            return;
        }


        if (!IsPostBack)
        {
            if (Session["jgbh"] == null || Session["jgqs"] == null || Session["jglx"] == null)
            {
                Response.Write("调用错误！");
                Response.End();
            }
            //if (Request.QueryString["flag"] != null)
            //{
            //    this.wanluo.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a  href=ShowNetworkBiaoGeView.aspx?bh=" + Request.QueryString["flag"] + ">" + Request.QueryString["flag"] + "</a>";
            //}
            //else
            //{
            //    this.wanluo.InnerHtml = "";
            //}
            showData();

            SetDaoHang();
        }
    }

    /// <summary>
    /// 网络图导航
    /// </summary>
    private void SetDaoHang()
    {
        divBack.InnerHtml = "<img src='images/bgback.gif' style=\"cursor:pointer;\" onclick=\"javascript:window.location.href='ShowNetWorkView.aspx?net=" + Session["jglx"].ToString() + "&SelectGrass=" + Session["jgqs"].ToString() + "&bh=" + Session["jgbh"].ToString() + "'\" />";

        wanluo.InnerHtml = GetTran("007032", "链路图") + "：";
        if (Session["DHNumbers"] == null)
        {
            Session["DHNumbers"] = new string[2] { Session["jgbh"].ToString(), "" };
            wanluo.InnerHtml += "<a href='ShowNetworkBiaoGeView.aspx?net=" + Session["jglx"].ToString() + "&SelectGrass=" + Session["jgqs"].ToString() + "&bh=" + Session["jgbh"].ToString() + "'>" + CommonDataBLL.GetPetNameByNumber(Session["jgbh"].ToString()) + "</a> →";
        }
        else
        {
            string[] nums = Session["DHNumbers"] as string[];

            if (nums[0] != Session["jgbh"].ToString())
            {
                if (nums[1] != Session["jgbh"].ToString())
                {
                    nums[1] = Session["jgbh"].ToString();
                }

                IList<string> lists = Jiegou.GetNumberForTop(nums[0], Convert.ToInt32(Session["jgqs"].ToString()), Session["jglx"].ToString() == "az");
                int count = 0;
                foreach (string str in lists)
                {
                    if (nums[1] == str)
                        count++;
                }

                if (count == 0)
                    wanluo.InnerHtml += "<a href='ShowNetworkBiaoGeView.aspx?net=" + Session["jglx"].ToString() + "&SelectGrass=" + Session["jgqs"].ToString() + "&bh=" + nums[1] + "'>" + CommonDataBLL.GetPetNameByNumber(nums[1]) + "</a> →";
                else
                {
                    string highNum = nums[1];
                    string numbers = "";
                    do
                    {
                        numbers += highNum + ",";
                        highNum = Jiegou.GetHighNumber(highNum, Session["jglx"].ToString() == "az");
                    } while (highNum != nums[0]);
                    numbers += nums[0] + ",";

                    for (int i = numbers.Split(new char[] { ',' }).Length - 1; i >= 0; i--)
                    {
                        if (numbers.Split(new char[] { ',' })[i] != "")
                            wanluo.InnerHtml += "<a href='ShowNetworkBiaoGeView.aspx?net=" + Session["jglx"].ToString() + "&SelectGrass=" + Session["jgqs"].ToString() + "&bh=" + numbers.Split(new char[] { ',' })[i] + "'>" + CommonDataBLL.GetPetNameByNumber(numbers.Split(new char[] { ',' })[i]) + "</a> →";
                    }

                }
            }
            else
                wanluo.InnerHtml += "<a href='ShowNetworkBiaoGeView.aspx?net=" + Session["jglx"].ToString() + "&SelectGrass=" + Session["jgqs"].ToString() + "&bh=" + nums[0] + "'>" + CommonDataBLL.GetPetNameByNumber(nums[0]) + "</a> →";

            Session["DHNumbers"] = nums;
        }

    }

    protected string GetLoginMember()
    {
        return Session["Member"].ToString();
    }

    private void showData()
    {
        if (isAnZhi())
        {
            Session["jgcw"] = 3;
        }
        else
        {
            Session["jgcw"] = 2;
        }
        int StartIndex = Convert.ToInt32(Session["jgqs"]);
        //Jiegou.wltDatable(this.Table1, 1, 3, false, "", "ShowNetworkBiaoGeView.aspx?flag=1");
        Jiegou.wltDatableMember(Table1, StartIndex, Convert.ToInt32(Session["jgcw"]), isAnZhi(), Convert.ToString(Session["jgbh"]), "ShowNetworkBiaoGeView.aspx?flag=" + Session["xqbh"].ToString() + "&bh=");
    }
    /// <summary>
    /// 取得要查询网络的起点编号
    /// </summary>
    /// <param name="isDian">是否是店调用</param>
    /// <returns>起点编号</returns>
    private string getBH(string sf)
    {
        string bh = "";
        switch (sf)
        {
            case "D":
                string storeid = Session["Store"].ToString();
                bh = DetialQueryBLL.GetNumberByStoreID(storeid);//Session["Store"]
                break;
            case "G":
                bh = BLL.CommonClass.CommonDataBLL.getManageID(1);
                break;
            case "H":
                bh = Convert.ToString(Session["Member"]);
                break;
        }

        return bh;
    }
    private bool isAnZhi()
    {
        bool temp = true;
        if (Convert.ToString(Session["jglx"]) == "tj")
        {
            temp = false;
        }
        return temp;
    }
}
