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


public partial class detest : System.Web.UI.Page
{
    Encryption.NumberByBit nb = new Encryption.NumberByBit();
 
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //"blif=" + EncryKey.GetEncryptstr(orderid, 1,1 );
       //string  bil = EncryKey.GetEncryptstr(this.TextBox1.Text,1,1) ;
       //Session["member"]="8888888888";
       //Response.Redirect("../payserver/chosepay.aspx?blif=" + bil);


 
       
    }
 
    protected void Button2_Click(object sender, EventArgs e)
    {
        //this.Label1.Text = EncryKey.GetDecrypt(this.Label1.Text)[0];
    } 

 
   
}
