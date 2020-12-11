using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



public partial class UserControl_STop : System.Web.UI.UserControl 
{
    protected void Page_Load(object sender, EventArgs e)
    {



    }

    public string GetTran(string keyCode, string defaultText)
    {
        return BLL.Translation.Translate(keyCode, defaultText);
    }


}

  
