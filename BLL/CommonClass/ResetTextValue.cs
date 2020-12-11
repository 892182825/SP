using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Add Namespace
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/*
 * Creator:     WangHua 
 * CreateTime:  2010-03-30
 * Function:    Reset the value of the all text
 */

namespace BLL.CommonClass
{
    public class ResetTextValue
    {
        public static void ClearTextValue(Page p)
        {                          
            foreach(Control c in p.Controls)            
            {                
                foreach (Control childc in c.Controls)
                {
                    if (childc is TextBox)
                    {
                        ((TextBox)childc).Text = "";
                    }
                }
            }
        }
    }
}
