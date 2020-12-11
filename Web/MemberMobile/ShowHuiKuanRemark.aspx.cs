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
using Model;
using BLL.CommonClass;

public partial class Member_ShowHuiKuanRemark : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //检查相应权限
        Permissions.MemRedirect(Page, Permissions.redirUrl);

        if (Request.QueryString["strtype"] != null)
        {
            this.lb.Text = "<table width=700px style='height:500px;margth-left:30%' cellpadding=1 border=0><tr><td align=center><b>" + GetTran("005837", "备注信息查看") + "</b></td></tr><tr style='height:150px'><td>&nbsp;</td></tr>";
            this.lb.Text = this.lb.Text + "<tr><td>" + CommonDataBLL.GetMemberWithdraw(Request.QueryString["id"].Trim()) + "</td></tr><tr><td align=center><br><a href=# onclick='javascript:window.opener=null;window.close()'>" + GetTran("001225", "关闭窗口") + "</a></td></tr></table>";

        }
        else
        {
            if (Request.QueryString["id"] != null && Request.QueryString["type"].ToString() == "0")
            {
                this.lb.Text = "<table width=700px style='height:500px margth-left:30%' cellpadding=1 border=0><tr><td align=center><b>" + GetTran("005837", "备注信息查看") + "</b></td></tr><tr style='height:150px'><td>&nbsp;</td></tr>";
                this.lb.Text = this.lb.Text + "<tr><td>" + CommonDataBLL.GetStoreNameFromStoreID(Request.QueryString["id"].Trim()) + "</td></tr><tr><td align=center><br><a href=# onclick='javascript:window.opener=null;window.close()'>" + GetTran("001225", "关闭窗口") + "</a></td></tr></table>";
            }//根据会员的转账ID得到备注信息
            else if (Request.QueryString["id"] != null && Request.QueryString["type"].ToString() == "2") {
                this.lb.Text = "<table width=700px style='height:500px margth-left:30%' cellpadding=1 border=0><tr><td align=center><b>" + GetTran("005837", "备注信息查看") + "</b></td></tr><tr style='height:150px'><td>&nbsp;</td></tr>";
                this.lb.Text = this.lb.Text + "<tr><td>" + CommonDataBLL.GetMemberECTMark(Request.QueryString["id"].Trim()) + "</td></tr><tr><td align=center><br><a href=# onclick='javascript:window.opener=null;window.close()'>" + GetTran("001225", "关闭窗口") + "</a></td></tr></table>";
            }
            else
            {
                this.lb.Text = "<table width=700px style='height:500px margth-left:30%' cellpadding=1 border=0><tr><td align=center><b>" + GetTran("005837", "备注信息查看") + "</b></td></tr><tr style='height:150px'><td>&nbsp;</td></tr>";
                this.lb.Text = this.lb.Text + "<tr><td>" + CommonDataBLL.GetMemberNameFromMemberID(Request.QueryString["id"].Trim()) + "</td></tr><tr><td align=center><br><a href=# onclick='javascript:window.opener=null;window.close()'>" + GetTran("001225", "关闭窗口") + "</a></td></tr></table>";
            }
        }
    }
}
