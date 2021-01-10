using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.CommonClass;

public partial class Sellbuydetails : BLL.TranslationBase
{
     
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        if (!IsPostBack)
        {
            if (Request.QueryString["rmid"] != null) 
            {
                int rmid = Convert.ToInt32(Request.QueryString["rmid"]);
                ViewState["rmid"] = rmid;
              LoadRminfoList( rmid);
            }
        }


    }

    public void LoadRminfoList(int rmid) {
        string sqls = "select  actype,    r.hkpzImglj,r.priceJB, m.number,  m.name ,RemittancesDate as  trantime, investJB ,remitmoney as ttpriec,Ispipei,shenhestate,ReceivablesDate  as strartime   from remittances r left join memberinfo  m on r.RemitNumber=m.number  where  r.id=@rmid";
        SqlParameter[] sps = new SqlParameter[] { 
         new SqlParameter("@rmid",rmid)
        };
        DataTable dtt = DAL.DBHelper.ExecuteDataTable(sqls, sps,CommandType.Text);
        if (dtt != null&&dtt.Rows.Count>0 )
        {DataRow dr=dtt.Rows[0];
         
           double jbsl=Convert.ToDouble(dr["investJB"]);
            int shstate=Convert.ToInt32(dr["shenhestate"]);
            int actype = Convert.ToInt32(dr["actype"]);
            string xb = "";
            if (actype == 1) xb = "水星";
            if (actype == 2) xb = "金星";
            if (actype == 3) xb = "土星";
            if (actype == 4) xb = "木星";
            if (actype == 5) xb = "火星";

            lblttbuy.Text= jbsl.ToString("0.00");
            litstate.Text=GetSHState(shstate);
            string buyhtml=@"  
                  <li><div class='fdiv'>挂单时间</div><div class='sdiv'>" + Convert.ToDateTime(dr["trantime"]).AddHours(8).ToString("yyyy-MM-dd HH:mm:ss") + " </div></li> <li><div class='fdiv'>买入"+ xb + @"币</div><div class='sdiv'>" + jbsl.ToString("0.0000") + "</div></li>  <li><div class='fdiv'>买入价格</div><div class='sdiv'>" + Convert.ToDecimal(dr["priceJB"]).ToString("0.0000") + "</div></li>   <li><div class='fdiv'>买入市值</div><div class='sdiv'>" + Convert.ToDouble(dr["ttpriec"]).ToString("0.0000") + " USDT</div></li> "; 
            if (shstate == 1) {
                buyhtml += "<li> </li>";
                buyhtml += "<li><div class='fdiv'>状态</div><div class='sdiv'> 已成交</div></li><li><div class='fdiv'>交易时间</div><div class='sdiv'>" + Convert.ToDateTime(dr["strartime"]) .ToString("yyyy-MM-dd HH:mm:ss") + " </div></li>";
               
            }

            litbuyinfo.Text = buyhtml;
            litdjdut.Text = GetSHStatejdt(shstate);

          
            //获取凭证图片名
        //    string imgsrc = "../hkpzimg/"+dr["hkpzImglj"].ToString();
        //    string imgs = dr["hkpzImglj"].ToString();
        //    string sqlss = "select w.shenhestate, w.hkjs, w.id,m.Number,m.MobileTele,m.Name,isnull( w.bankcard,'') as bankcard,isnull( bankname,'') as bankname ,isnull(khName,'') as name,drawcardtype, isnull(alino,'') as alino , isnull(weixno,'') as weixno ,WithdrawMoney  from Withdraw w left join memberinfo m on w.number=m.Number where hkid=@rmid  order by id desc ";
        //    string wdlist = "";
        //    SqlParameter[] sps1 = new SqlParameter[] { 
        // new SqlParameter("@rmid",rmid)
        //};
//            DataTable dtw = DAL.DBHelper.ExecuteDataTable(sqlss, sps1, CommandType.Text);
//            if (dtw != null && dtw.Rows.Count > 0)
//            {
//                for (int i = 0; i < dtw.Rows.Count; i++)
//                { 
//                    DataRow drw = dtw.Rows[i];
//                    int dtype = Convert.ToInt32(drw["drawcardtype"]);
//                    int wid = Convert.ToInt32(drw["id"]);
//                    string hkjs = drw["hkjs"].ToString();
//                    string number = drw["number"].ToString();
//                    string name = drw["name"].ToString();
//    string phone = drw["MobileTele"].ToString();
//                    string bankname = drw["bankname"].ToString();
//                    string bankcard = drw["bankcard"].ToString();
//                    string alino = drw["alino"].ToString();
//                    string weixno = drw["weixno"].ToString();
//                    int sst =Convert.ToInt32( drw["shenhestate"]);
//                    double dbttp=Convert.ToDouble(drw["WithdrawMoney"]);
//                    wdlist += " <ul class='sellif' > ";
//                    if (dtw.Rows.Count > 1) wdlist += " <li class='title'><div class='fdiv'>第" + (i + 1) + "笔</div><div class='sdiv'>交易对方信息  </div> </li>";
               
//                    wdlist += " <li class='title'><div class='fdiv'> 交易状态</div><div class='sdiv'>" + GetSHState(sst) + "</div> </li>";

//                    wdlist += " <li    ><div >请向以下账户中汇入<span style='font-size:20px;color:red;'>" + dbttp .ToString("0.00")+ "</span> 元</div></li>";

//                    if (dtype == 0) wdlist += "  <li style='height:70px;'><div class='fdiv'>收款账户</div><div class='sdiv'>" + name + "  " + bankname + " " + bankcard + "    </div> </li>";
//                    if (dtype == 1) wdlist += "  <li><div class='fdiv'>收款账户</div><div class='sdiv'>" + name + "  支付宝 " + alino + "    </div> </li>";
//                    if (dtype == 2) wdlist += "  <li><div class='fdiv'>收款账户</div><div class='sdiv'>" + name + "  微信 " + weixno + "    </div> </li>";
//                        wdlist += " <li class='title' style='height:70px;'  ><div class='fdiv'> 对方账号</div><div class='sdiv'>" + number+"<br/>" +name+"-"+phone+ "</div> </li>";
//if (hkjs!=""||imgs!="")
//    wdlist += "<li><div class='fdiv'>汇款说明</div><div class='sdiv'>" + hkjs + "</div></li><li><div class='fdiv'>汇款凭证</div><div class='sdiv'><a href='#' onclick=\"showimg('" + imgsrc + "')\">查看凭证</a></div></li>";
//if (hkjs == ""&&imgs=="")
//    wdlist += "<li  style='height:70px;'><div class='fdiv'><input type='button'  onclick=\"addhksm('" + wid + "');\" value='买入汇款说明' class='btn btn-primary btn-lg'  /></div><div class='sdiv'></div></li>";


//                    wdlist += "</ul>";


//                }


//            }
//            else
//            {
//                wdlist = "  <ul class='sellif'><li>未匹配 请到交易中心操作确认汇出</li></ul>";
            
//            } 
           // lblwdrlist.Text = wdlist; 
        }

    }

public  string   GetSHState(int st){
    string res = "";
    if (st == 0) res = "待交易";  
    if (st ==1) res = "<span style='color:green;'>买入已成</span>"; 
    return res;
}

public string GetSHStatejdt(int st)
{
    string res = "<div style='width:50%;'> </div>";
    if (st == 0) res = "<div style='width:50%;'> </div>";
    
    if (st == 1) res = "<div style='width:100%; '> </div>";
    return res;
}


protected void Button1_Click(object sender, EventArgs e)
{
    int wid = Convert.ToInt32(hidid.Value);
    if (wid > 0)
    {
        string hkjs = this.txthkdesc.Text.Trim();
        string sqls = "update  withdraw  set  isjs=1, hkjs=@hkjs  where id=@id";
        SqlParameter[] sps = new SqlParameter[] { 
        new SqlParameter("@hkjs",hkjs),
        new SqlParameter("@id",wid)
        };
        int r = DAL.DBHelper.ExecuteNonQuery(sqls,sps,CommandType.Text);
        if (r > 0)
        {

            Boolean fileOK = false;
            //获取上传的文件名
            string fileName = this.FileUpload1.FileName;
            //获取物理路径
            String path = Server.MapPath("~/hkpzimg/");

            //判断上传控件是否上传文件
            if (FileUpload1.HasFile)
            {
                //判断上传文件的扩展名是否为允许的扩展名".gif", ".png", ".jpeg", ".jpg" ,".bmp"
                String fileExtension = System.IO.Path.GetExtension(fileName).ToLower();
                String[] Extensions = { ".gif", ".png", ".jpeg", ".jpg", ".bmp" };
                for (int i = 0; i < Extensions.Length; i++)
                {
                    if (fileExtension == Extensions[i])
                    {
                        fileOK = true;
                    }
                }

                //根据时间重命名
                string EditFileName = DateTime.Now.ToString("yyyyMMdd HH:mm:ss fff").Replace(" ", "").Replace(":", "");
                fileName = EditFileName + fileExtension;
            }
            //如果上传文件扩展名为允许的扩展名，则将文件保存在服务器上指定的目录中
            if (fileOK)
            {
                try
                {

                    int numid = Convert.ToInt32(ViewState["rmid"]);
                    string sqlss = "update  remittances  set  hkpzImglj='" + fileName + "'where id=@id";
                    SqlParameter[] spss = new SqlParameter[] { 
        new SqlParameter("@id",numid)
        };
                    int rr = DAL.DBHelper.ExecuteNonQuery(sqlss, spss, CommandType.Text);
                    if (rr > 0)
                    {
                        this.FileUpload1.PostedFile.SaveAs(path + fileName);
                        MessageBox("提交成功");

                    }
                    //LoadRminfoList(Convert.ToInt32(ViewState["rmid"]));

                }
                catch (Exception ex)
                {
                    MessageBox("文件不能上传，原因：" + ex.Message);
                }
            }
            else
            {
                MessageBox("不能上传这种类型的文件");
            }
           // ClientScript.RegisterStartupScript(this.GetType(),"","<script>alert('提交说明成功');</script>");

        } 
        LoadRminfoList(Convert.ToInt32( ViewState["rmid"])) ;

    }

}

      protected void btnFileUpload_Click(object sender, EventArgs e)
     {
        
     }
 
     protected void MessageBox(string str)
     {
         Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script>alert('"+str+"');</script>"); 
     }
}