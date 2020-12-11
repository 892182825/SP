using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.CommonClass;

public partial class MemberMobile_DetailDCS : BLL.TranslationBase
{
    public int bzCurrency = 0;
    public double huilv;
    protected void Page_Load(object sender, EventArgs e)
    {
        bzCurrency = CommonDataBLL.GetStandard();
        huilv = (AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString())));
        var hkid = Request.QueryString["hkid"];
        var hybh = Session["Member"].ToString();
        if (!IsPostBack)
        {
            //获取标准币种
            
            //string dt_one = "select w.id,w.hkid, w.bankcard,w.bankname,w.shenHestate,w.WithdrawMoney,m.name,w.auditingtime from memberinfo m, withdraw w,remittances r where m.Number=w.number and w.hkid=r.ID and (w.shenhestate=11 or w.shenhestate=2) order by auditingtime desc";
            //DataTable dt = DAL.DBHelper.ExecuteDataTable(dt_one);
            //Hkid.Value = hkid;
            //rep_km.DataSource = dt;
            //rep_km.DataBind();

            string dt_one = "select * from (select w.id,w.hkid,w.bankcard,w.bankname,w.shenHestate,w.WithdrawMoney,m.name,RemittancesDate,w.auditingtime,m.Number from memberinfo m, withdraw w,remittances r where m.Number = w.number and w.hkid = r.ID and  (w.shenhestate=11 or w.shenhestate=2)   and m.Number!='" + hybh + "' and r.RemitNumber='" + hybh + "' union select id,ID as hkid,ImportNumber as bankcard,ImportBank as bankname,shenhestate,RemitMoney as WithdrawMoney,name,RemittancesDate,ReceivablesDate as auditingtime,RemitNumber as Number from Remittances where isjl = 1 and  (shenhestate=11 or shenhestate=2) and RemitNumber='" + hybh + "'  ) as t order by auditingtime desc";
            // 
            DataTable dt = DAL.DBHelper.ExecuteDataTable(dt_one);
            Hkid.Value = hkid;
            var count = dt.Rows.Count;
            rep_km.DataSource = dt;
            rep_km.DataBind();
        }
        
    }

    protected void rep_km_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Button b = e.Item.FindControl("Button1") as Button;
            Button jieshi = e.Item.FindControl("jieshi") as Button;
            Button jieshi1 = e.Item.FindControl("jieshi1") as Button;
            jieshi1.Text = GetTran("009025", "解释");
            jieshi.Text = GetTran("009025", "解释");
            Button jieshi2 = e.Item.FindControl("jieshi2") as Button;
            HiddenField h = e.Item.FindControl("HiddenField1") as HiddenField;
            HiddenField h2 = e.Item.FindControl("HiddenField2") as HiddenField;
            HyperLink hl = e.Item.FindControl("HyperLink1") as HyperLink;
            HyperLink h3 = e.Item.FindControl("HyperLink2") as HyperLink;
            HiddenField h4 = e.Item.FindControl("HiddenField3") as HiddenField;
            hl.NavigateUrl = "DCSXX.aspx?hkid=" + h.Value + " &hkmoney=" + h2.Value + " &id=" + h4.Value;
            Label l = e.Item.FindControl("Label1") as Label;
            string dt_one = "select * from (select w.id,w.hkid,w.bankcard,w.bankname,w.shenHestate,w.WithdrawMoney,m.name,RemittancesDate,w.auditingtime,w.Isjs,w.HkJs from memberinfo m, withdraw w,remittances r where m.Number = w.number and w.hkid = r.ID and  (w.shenhestate=11 or w.shenhestate=2) and hkid='" + h.Value + "'   union select id,ID as hkid,ImportNumber as bankcard,ImportBank as bankname,shenhestate,RemitMoney as WithdrawMoney,name,RemittancesDate,ReceivablesDate as auditingtime,Isjs,remark as HkJs from Remittances where isjl = 1  and  (shenhestate=11 or shenhestate=2) and ID='" + h.Value + "'  ) as t order by auditingtime desc";
            DataTable dt = DAL.DBHelper.ExecuteDataTable(dt_one);
            var shenHestate = dt.Rows[0]["shenHestate"].ToString();
            var Isjs = dt.Rows[0]["Isjs"].ToString();
            var HkJs = dt.Rows[0]["HkJs"].ToString();
            if (shenHestate == "11")
            {
                hl.Visible = false;
                h3.Visible = true;
            }
            if (shenHestate != "11")
            {

                hl.Visible = true;
                h3.Visible = false;

            }
            if (Isjs == "0")
            {
                jieshi.Visible = true;
                jieshi1.Visible = false;
                jieshi2.Visible = false;

            }
            if (Isjs == "1")
            {
                if (HkJs != "")
                {
                    jieshi.Visible = false;
                    jieshi1.Visible = false;
                    jieshi2.Visible = true;

                }
                else
                {
                    jieshi.Visible = false;
                    jieshi1.Visible = true;
                    jieshi2.Visible = false;

                }
            }
        }
    }
    //protected void Btn_Js(object sender, EventArgs e)
    //{
    //    var hkid = Request.QueryString["hkid"];

    //    string url = "HkJS.aspx?hkid=" + hkid;
    //    Response.Redirect(url);
    //}

    protected void rep_km_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName=="sc")
        {
            string arg = e.CommandArgument.ToString();//取得参数

            if (!System.IO.Directory.Exists(Server.MapPath("~/hkpzimg/")))
            {
                System.IO.Directory.CreateDirectory(Server.MapPath("~/hkpzimg/") + "\\");
            }

            FileUpload fu= e.Item.FindControl("FileUpload1") as FileUpload;

            string filepath = fu.PostedFile.FileName;//获取要上传的文件的说有字符，包括文件的路径，文件的名称文件的扩展名称

            //验证上传文件后缀名
            string s = filepath.Substring(filepath.LastIndexOf(".") + 1);
            if (s.Trim()=="")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('您选择您要上传的汇款凭证！')</script>");
                return;
            }
            s = s.ToLower();
            if (s != "jpg" && s != "jpeg" && s != "png" && s != "gif" && s != "bmp")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('您上传的汇款凭证必须是图片，请重新上传！')</script>");
                return;
            }

            string mPath = Server.MapPath("~/hkpzimg/");
            string mfileName = filepath.Substring(filepath.LastIndexOf("\\") + 1);

            int sizeLength = fu.PostedFile.ContentLength;
            if (sizeLength > 1 * 1024 * 1024)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('上传的图片不能超过1MB')", true);
                return;
            }

            string realName = DateTime.Now.ToString("yyyyMMdd")+ DateTime.Now.ToString("HHmmss") + mfileName;
            string errstr = "";
            try
            {
                fu.PostedFile.SaveAs(mPath + "\\" + realName);
            }
            catch (Exception ex)
            {
                errstr = ex.ToString();
            }

            if (errstr == "")
            {
                int rs = DAL.DBHelper.ExecuteNonQuery("update withdraw set hkpzImglj='" + realName + "' where id=" + arg);
                if(rs==0)
                rs += DAL.DBHelper.ExecuteNonQuery("update Remittances set hkpzImglj='" + realName + "' where id=" + arg);

                if (rs <= 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('上传汇款凭证失败！')", true);
                    return;
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('上传汇款凭证成功！')", true);
                    
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('上传汇款凭证失败！')", true);
                return;
            }
           
        }
    }
}